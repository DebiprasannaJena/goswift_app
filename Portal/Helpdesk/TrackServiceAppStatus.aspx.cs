using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Configuration;
using System.Data.SqlClient;
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using Ionic.Zip;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

public partial class Portal_HelpDesk_TrackServiceAppStatus : SessionCheck
{

    DataSet ds = new DataSet();
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

    /// <summary>
    /// Search Button  
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DivDetails.Visible = true;
        Lbl_Msg_Restful.Text = "";
        FillGridviewData();
    }

    /// <summary>
    /// Data bind from data set with label and gridview 
    /// </summary>
    public void FillGridviewData()
    {
        TrackService entity = new TrackService();
        ServiceBusinessLayer objService = new ServiceBusinessLayer();

        try
        {
            entity.StrAction = "GSA";
            entity.Str_Application_Id = txtServiceID.Text.Trim();
            ds = objService.TrackServiceAppliactionDetail(entity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DivNoRecord.Visible = false;
                Divapplicationdetails.Visible = true;

                Hypr_ApplicationNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCH_APPLICATION_UNQ_KEY"]);
                Hypr_ApplicationNo.NavigateUrl = "~/Portal/Service/ServiceDetailsView.aspx?ApplicationNo=" + Convert.ToString(ds.Tables[0].Rows[0]["VCH_APPLICATION_UNQ_KEY"]) + "&ServiceId=" + Convert.ToString(ds.Tables[0].Rows[0]["INT_SERVICEID"]);

                Lbl_Searvice_Name.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCH_SERVICENAME"]);
                Lbl_Investor_Name.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCH_INVESTOR_NAME"]);

                if (Convert.ToString(ds.Tables[0].Rows[0]["VCH_PROPOSALID"]) != "")
                {
                    Lbl_Proposal_Id.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCH_PROPOSALID"]);
                }
                else
                {
                    Lbl_Proposal_Id.Text = "-NA-";
                }

                Lbl_created_on.Text = DateTime.Parse(ds.Tables[0].Rows[0]["DTM_CREATEDON"].ToString()).ToString("dd-MMM-yyyy");
                Lbl_current_status.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCH_STATUS"]);

                if (Convert.ToString(ds.Tables[0].Rows[0]["vch_ACTION_TAKEN_BY"]) != "")
                {
                    Lbl_Action_Taken_By.Text = Convert.ToString(ds.Tables[0].Rows[0]["vch_ACTION_TAKEN_BY"]);
                }
                else
                {
                    Lbl_Action_Taken_By.Text = "-NA-";
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["vch_ACTION_TOBE_TAKEN_BY"]) != "")
                {
                    Lbl_Action_Tobe_Taken_By.Text = Convert.ToString(ds.Tables[0].Rows[0]["vch_ACTION_TOBE_TAKEN_BY"]);
                }
                else
                {
                    Lbl_Action_Tobe_Taken_By.Text = "-NA-";
                }

                Lbl_Payment_Status.Text = Convert.ToString(ds.Tables[0].Rows[0]["vch_PAYMENT_STATUS"]);
                Lbl_Payment_Amount.Text = Convert.ToString(ds.Tables[0].Rows[0]["NUM_PAYMENT_AMOUNT"]);
                Lbl_apply_By.Text = Convert.ToString(ds.Tables[0].Rows[0]["vch_CREATEDBY"]);

                if (Convert.ToString(ds.Tables[0].Rows[0]["dtm_Payment_date"]) != "")
                {
                    Lbl_Payment_date.Text = DateTime.Parse(ds.Tables[0].Rows[0]["dtm_Payment_date"].ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    Lbl_Payment_date.Text = "-NA-";
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["DTM_RAISE_QUERY"]) != "")
                {
                    Lbl_Querey_date.Text = DateTime.Parse(ds.Tables[0].Rows[0]["DTM_RAISE_QUERY"].ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    Lbl_Querey_date.Text = "-NA-";
                }

                if (Convert.ToString(ds.Tables[0].Rows[0]["dtm_EndOfORTPS_Timeline"]) != "")
                {
                    Lbl_ortps_timeline.Text = DateTime.Parse(ds.Tables[0].Rows[0]["dtm_EndOfORTPS_Timeline"].ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    Lbl_ortps_timeline.Text = "-NA-";
                }

                //ADDED BY DHARMASIS SAHOO
                if (Convert.ToString(ds.Tables[0].Rows[0]["DTM_REVERT_QUERY"]) != "")
                {
                    Lbl_Revert_Query.Text = DateTime.Parse(ds.Tables[0].Rows[0]["DTM_REVERT_QUERY"].ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    Lbl_Revert_Query.Text = "-NA-";
                }
            }
            else
            {
                DivNoRecord.Visible = true;
                Divapplicationdetails.Visible = false;
                Lbl_Norecord.Text = "No record found .";
            }

            /*---------------------------------------------------------------------------*/
            ///Payment Details
            /*---------------------------------------------------------------------------*/
            if (ds.Tables[1].Rows.Count > 0)
            {
                GrdPaymentDetails.DataSource = ds.Tables[1];
                GrdPaymentDetails.DataBind();
            }
            else
            {
                GrdPaymentDetails.DataSource = ds.Tables[1];
                GrdPaymentDetails.DataBind();
            }

            /*---------------------------------------------------------------------------*/
            ///Query Details
            /*---------------------------------------------------------------------------*/
            if (ds.Tables[2].Rows.Count > 0)
            {
                GrdQuereyDetails.DataSource = ds.Tables[2];
                GrdQuereyDetails.DataBind();

            }
            else
            {
                GrdQuereyDetails.DataSource = ds.Tables[2];
                GrdQuereyDetails.DataBind();
            }

            /*---------------------------------------------------------------------------*/
            ///Action Details
            /*---------------------------------------------------------------------------*/
            if (ds.Tables[3].Rows.Count > 0)
            {
                GrdActionDetails.DataSource = ds.Tables[3];
                GrdActionDetails.DataBind();
            }
            else
            {
                GrdActionDetails.DataSource = ds.Tables[3];
                GrdActionDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ServiceAppTracking");
        }
    }

    /// <summary>
    /// View action  document   
    /// </summary>
    protected void GrdActionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hprApprodoc = (HyperLink)e.Row.FindControl("hprApprodoc");
            HyperLink hprReferndoc = (HyperLink)e.Row.FindControl("hprReferndoc");
            HyperLink hprInspectdoc = (HyperLink)e.Row.FindControl("hprInspectdoc");
            HyperLink hprRestordoc = (HyperLink)e.Row.FindControl("hprRestordoc");

            HiddenField hdnFilevalcert = (HiddenField)e.Row.FindControl("hdnfilevalcert");
            HiddenField hdnFileval = (HiddenField)e.Row.FindControl("hdnfileval");
            HiddenField hdnInspectionDocu = (HiddenField)e.Row.FindControl("hdnInspectionDocu");
            HiddenField hdnRestorationDocu = (HiddenField)e.Row.FindControl("hdnRestorationDocu");

            Label lblapproval = (Label)e.Row.FindControl("lblapproval");
            Label lblReferdoc = (Label)e.Row.FindControl("lblReferdoc");
            Label lblinspdoc = (Label)e.Row.FindControl("lblinspdoc");
            Label lblrestdoc = (Label)e.Row.FindControl("lblrestdoc");

            if (hdnFilevalcert.Value == "")
            {
                lblapproval.Visible = true;
                lblapproval.Text = "NA";
                lblapproval.ForeColor = System.Drawing.Color.Red;
                hprApprodoc.Visible = false;
            }
            else
            {
                hprApprodoc.ToolTip = hdnFilevalcert.Value;
                hprApprodoc.Target = "_Blank";
                hprApprodoc.NavigateUrl = "~/Portal/ApprovalDocs/" + hdnFilevalcert.Value;
            }

            /*---------------------------------------------------------------------------*/

            if (hdnFileval.Value == "")
            {
                lblReferdoc.Visible = true;
                lblReferdoc.Text = "NA";
                lblReferdoc.ForeColor = System.Drawing.Color.Red;
                hprReferndoc.Visible = false;
            }
            else
            {
                hprReferndoc.ToolTip = hdnFileval.Value;
                hprReferndoc.Target = "_Blank";
                hprReferndoc.NavigateUrl = "~/Portal/ApprovalDocs/" + hdnFileval.Value;
            }

            /*---------------------------------------------------------------------------*/

            if (hdnInspectionDocu.Value == "")
            {
                lblinspdoc.Visible = true;
                lblinspdoc.Text = "NA";
                lblinspdoc.ForeColor = System.Drawing.Color.Red;
                hprInspectdoc.Visible = false;
            }
            else
            {
                hprInspectdoc.ToolTip = hdnInspectionDocu.Value;
                hprInspectdoc.Target = "_Blank";
                hprInspectdoc.NavigateUrl = "~/Portal/ApprovalDocs/" + hdnInspectionDocu.Value;
            }

            /*---------------------------------------------------------------------------*/

            if (hdnRestorationDocu.Value == "")
            {
                lblrestdoc.Visible = true;
                lblrestdoc.Text = "NA";
                lblrestdoc.ForeColor = System.Drawing.Color.Red;
                hprRestordoc.Visible = false;
            }
            else
            {
                hprRestordoc.ToolTip = hdnRestorationDocu.Value;
                hprRestordoc.Target = "_Blank";
                hprRestordoc.NavigateUrl = "~/Portal/ApprovalDocs/" + hdnRestorationDocu.Value;
            }
        }
    }

    /// <summary>
    /// Get transaction information  from treasury 
    /// </summary>
    protected void Btn_transaction_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
        Label Lbl_Orderno = (Label)row.FindControl("Lbl_OrderNo");
        Label Lbl_Amount = (Label)row.FindControl("Lbl_ChallanAmount");
        TreasuryPaymentTracking objTreasury = new TreasuryPaymentTracking();

        Lbl_Msg_Restful.Text = "";

        try
        {
            string strResult = objTreasury.GetPaymentStatusFromTreasury(Lbl_Orderno.Text, Convert.ToDecimal(Lbl_Amount.Text));
            Lbl_Msg_Restful.Text = strResult;
            Lbl_Msg_Restful.ForeColor = System.Drawing.Color.Blue;
        }
        catch (Exception ex)
        {
            Lbl_Msg_Restful.Text = ex.Message.ToString();
            Lbl_Msg_Restful.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void GrdPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hiddenField = (HiddenField)e.Row.FindControl("HdnPaymStatus");
            Label label = (Label)e.Row.FindControl("Lbl_PaymentStatus");
            if (hiddenField.Value == "0")
            {
                label.ForeColor = System.Drawing.Color.Red;
            }
            else if (hiddenField.Value == "1")
            {
                label.ForeColor = System.Drawing.Color.Green;
            }
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

    /// <summary>
    /// Download  Query file in zip  format 
    /// </summary>
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
                        string FileName = "~/QueryFiles/Services/" + Convert.ToString(arrFileName[i]);
                        string filePath = Server.MapPath(FileName);
                        if (File.Exists(filePath))
                        {
                            zip.AddFile(filePath, "QueryFiles");
                        }
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
            Util.LogError(ex, "ServiceAppTracking");
        }
        finally
        {
            Response.End();
        }
    }
}