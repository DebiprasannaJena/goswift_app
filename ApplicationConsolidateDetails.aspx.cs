using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BPAS;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using RestSharp;
using System.Web.Script.Serialization;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using EntityLayer.Proposal;
using EntityLayer.Service;
using BusinessLogicLayer.Service;

public partial class ApplicationConsolidateDetails : SessionCheck
{
    #region Globalvariable

    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    SqlConnection con = new SqlConnection(connectionString);
    SqlCommand cmd;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            int intHOACount = 0;
            int intCheckStatus = 0;
            string servid = string.Empty;
            for (int i = 0; i < GrdServiceDetails.Rows.Count; i++)
            {
                CheckBox ChkBxSelect = (CheckBox)GrdServiceDetails.Rows[i].FindControl("ChkBxSelect");
                HiddenField hdnserviceid = (HiddenField)GrdServiceDetails.Rows[i].FindControl("hdnserviceid");
                HiddenField HidHOACount = (HiddenField)GrdServiceDetails.Rows[i].FindControl("Hid_HOA_Count");
                HiddenField Hid_Disable_Status = (HiddenField)GrdServiceDetails.Rows[i].FindControl("Hid_Disable_Status");

                if (ChkBxSelect.Checked)
                {
                    servid += Hid_Disable_Status.Value + "|";
                    intCheckStatus = 1;
                    if (Convert.ToInt32(hdnserviceid.Value) == 16)
                    {
                        intHOACount = intHOACount + Convert.ToInt32(HidHOACount.Value) + 1;
                    }
                    else
                    {
                        intHOACount = intHOACount + Convert.ToInt32(HidHOACount.Value);
                    }
                }
            }
            if (intHOACount > 6)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Click", "jAlert('<strong>Services having maximum 6 head of account can be selected at a time !</strong>');", true);
                return;
            }
            else if (intCheckStatus == 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Click", "jAlert('<strong>Please select at least one check box to proceed !</strong>');", true);
                return;
            }
            else
            {
                if (Session["SvcMasterData"] != null)
                {
                    DataTable dt = (DataTable)Session["SvcMasterData"];
                    DataTable dtchk = new DataTable();
                    dtchk.Columns.Add("vchApplicationKey", typeof(string));
                    dtchk.Columns.Add("intServiceId", typeof(string));
                    if (dt.Rows.Count > 0)
                    {
                        if (servid.Length > 0)
                        {
                            servid = servid.Substring(0, servid.Length - 1);
                        }
                        string[] datakey = servid.Split('|');
                        for (int j = 0; j < datakey.Length; j++)
                        {
                            DataRow drupdate = dt.AsEnumerable().Where(r => ((string)r["vchApplicationKey"]).Equals(datakey[j])).FirstOrDefault();
                            dtchk.Rows.Add(drupdate["vchApplicationKey"], drupdate["intServiceId"]);
                        }
                        Session["SvcPaymentData"] = dtchk;
                    }
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectScript", "window.parent.location = 'ServicePayment.aspx?ReqMode=M'", true);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }

    #region FunctionUsed

    private void BindGrid()
    {
        try
        {
            ServiceBusinessLayer objService = new ServiceBusinessLayer();
            //List<ProposalDet> objProposalList = new List<ProposalDet>();

            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "D";
            objProp.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);

            List<ServiceDetails> ServiceDetail = objService.GetAllDraftedApplicationDetails(Session["UserId"].ToString()).ToList();
            List<EntityLayer.Service.ServiceDetails> ObjInternalSvc = ServiceDetail.Where(n => n.str_checkStatus == "0").ToList();


            if (Session["SvcMasterData"] != null)
            {
                DataTable dt = (DataTable)Session["SvcMasterData"];

                if (dt.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                    {
                        string apkey = dt.Rows[i1]["vchApplicationKey"].ToString();
                        EntityLayer.Service.ServiceDetails ObjInternalSvcA = ObjInternalSvc.Where(n => n.str_ApplicationNo == apkey).FirstOrDefault();
                        DataRow drupdate = dt.AsEnumerable().Where(r => ((string)r["vchApplicationKey"]).Equals(dt.Rows[i1]["vchApplicationKey"].ToString())).FirstOrDefault();
                        if (ObjInternalSvcA != null)
                        {
                            drupdate["decAmount"] = ObjInternalSvcA.Dec_Amount;
                            drupdate["intHoaAccount"] = ObjInternalSvcA.intHOACount;
                        }
                        else
                        {
                            if (apkey == "")
                            {
                                // dt.Rows.Remove(drupdate);
                            }
                        }
                    }

                    GrdServiceDetails.DataSource = dt;
                    GrdServiceDetails.DataBind();

                    GrdServiceDetails.FooterRow.Cells[5].Text = "Total";
                    GrdServiceDetails.FooterRow.Cells[6].Text = dt.AsEnumerable().Sum(x => Convert.ToDecimal(x.Field<string>("decAmount"))).ToString();
                    GrdServiceDetails.FooterRow.Cells[6].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }

    #endregion
    protected void GrdServiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label LblApplicationNo = (Label)e.Row.FindControl("LblApplicationNo");
            if (LblApplicationNo.Text == "")
            {
                e.Row.Visible = false;
            }

            Label LblAmount = (Label)e.Row.FindControl("LblAmount");
            HiddenField hdncomplete = (HiddenField)e.Row.FindControl("hdncomplete");
            CheckBox ChkBxSelect = (CheckBox)e.Row.FindControl("ChkBxSelect");
            if ((LblAmount.Text == "0.00" || LblAmount.Text == "0") && hdncomplete.Value == "1")
            {
                ChkBxSelect.Checked = true;
                ChkBxSelect.Enabled = false;
            }
        }
    }
}