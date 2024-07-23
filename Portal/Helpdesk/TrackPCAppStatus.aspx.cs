using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using Ionic.Zip;
using System.Data;
using System.Globalization;

public partial class Portal_Helpdesk_TrackPCAppStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/portal/SessionRedirect.aspx", false);
            return;
        }
        if (!IsPostBack)
        {
            DivDetails.Visible = false;
            DivNoRecord.Visible = false;
        }
    }
    public void BindData()
    {
        ProposalDet objentity = new ProposalDet();
        ProposalBAL objService = new ProposalBAL();        
        try
        {
            objentity.strAction = "TPCA";
            objentity.vchAppFormattedNo = TxtPcNumberID.Text;

            DataSet ds = objService.GetPCTrackDetails(objentity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DivNoRecord.Visible = false;
                Div_Applicationdetails.Visible = true;
                Hypr_PcNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchAppFormattedNo"]);
                Lbl_Company_Name.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchCompName"]);
                Lbl_Constitution_Name.Text = Convert.ToString(ds.Tables[0].Rows[0]["OrgType"]);
                Lbl_Project_Type.Text = Convert.ToString(ds.Tables[0].Rows[0]["CatagoryName"]);
                if(Lbl_Project_Type.Text == "Large")
                {
                    Hypr_PcNo.NavigateUrl = "~/portal/Incentive/IncentiveDetailsLarge.aspx?id=" + Convert.ToString(ds.Tables[0].Rows[0]["vchAppNo"]);
                }
                else
                {
                    Hypr_PcNo.NavigateUrl = "~/portal/Incentive/IncentiveDetails.aspx?id=" + Convert.ToString(ds.Tables[0].Rows[0]["vchAppNo"]);
                }
                Lbl_Application_For.Text = Convert.ToString(ds.Tables[0].Rows[0]["Appfor"]);
                Lbl_Current_Status.Text = Convert.ToString(ds.Tables[0].Rows[0]["strCurrentstatus"]);
                Lbl_Industry_Code.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchIndustryCode"]);
                Lbl_Application_No.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchAppNo"]);
                if (Convert.ToString(ds.Tables[0].Rows[0]["DTM_RAISE_QUERY"]) != "")
                {
                    Lbl_Raise_Query_Date.Text = Convert.ToString(ds.Tables[0].Rows[0]["DTM_RAISE_QUERY"]);                  
                }
                else
                {
                    Lbl_Raise_Query_Date.Text = "-NA-";
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["DTM_REVERT_QUERY"]) != "")
                {
                    Lbl_Revert_Query_Date.Text = Convert.ToString(ds.Tables[0].Rows[0]["DTM_REVERT_QUERY"]);                 
                }
                else
                {
                    Lbl_Revert_Query_Date.Text = "-NA-";
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["dtmCreatedOn"]) != "")
                {
                    Lbl_PC_Apply_Date.Text = Convert.ToString(ds.Tables[0].Rows[0]["dtmCreatedOn"]);                 
                }
                else
                {
                    Lbl_PC_Apply_Date.Text = "-NA-";
                }

                /*---------------------------------------------------------------------------*/
                ///Query Details
                /*---------------------------------------------------------------------------*/
                GrdQuereyDetails.DataSource = ds.Tables[0];
                GrdQuereyDetails.DataBind();
            }
             else
             {
                    DivNoRecord.Visible = true;
                    Div_Applicationdetails.Visible = false;
                    Lbl_Norecord.Text = "No record found.";
             }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        DivDetails.Visible = true;
        try
        {
            BindData();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PCTracking");
        }
    }

    protected void GrdQuereyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hdnstatus = (HiddenField)e.Row.FindControl("Hdnstatus");
            Label Lblstatus = (Label)e.Row.FindControl("Lbl_Status");

            LinkButton LnkBtnQueryFile = (LinkButton)e.Row.FindControl("LnkBtnQueryFile");
            Label LblQueryFile = (Label)e.Row.FindControl("LblQueryFile");

            if (Hdnstatus.Value == "5")
            {
                Lblstatus.ForeColor = System.Drawing.Color.Red;
            }
            else if (Hdnstatus.Value == "6")
            {
                Lblstatus.ForeColor = System.Drawing.Color.Green;
            }

            if (GrdQuereyDetails.DataKeys[e.Row.RowIndex].Values[0].ToString() != "-NA-" && GrdQuereyDetails.DataKeys[e.Row.RowIndex].Values[0].ToString() != "")
            {
                LnkBtnQueryFile.Visible = true;
            }
            else
            {
                LnkBtnQueryFile.Visible = false;
                LblQueryFile.Visible = true;
            }
        }
    }

    protected void LnkBtnQueryFile_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnk.NamingContainer;
        HiddenField HdnQueryFile = row.FindControl("HdnQueryFile") as HiddenField;

        try
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("QueryFiles");
                if (HdnQueryFile.Value != "")
                {
                    string[] arrFileName = HdnQueryFile.Value.Split(',');
                    for (int i = 0; i <= arrFileName.Count() - 1; i++)
                    {
                        string FileName = "~/QueryFiles/" + Convert.ToString(arrFileName[i]);
                        string filePath = Server.MapPath(FileName);
                        zip.AddFile(filePath, "QueryFiles");
                    }
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("QueryFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PCTracking");
        }
        finally
        {
            Response.End();
        }
    }
}