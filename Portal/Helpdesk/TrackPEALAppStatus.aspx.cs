using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.util;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_Helpdesk_TrackPEALAppStatus : System.Web.UI.Page
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
           // DivNoRecordAMS.Visible = false;
        }
    }

    /// <summary>
    /// Search Button  
    /// </summary>
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        DivDetails.Visible = true;
        try
        {
            Lbl_Msg_Restful.Text = "";
            BindData();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalTracking");
        }
    }

    /// <summary>
    /// Data bind from data set with label and gridview 
    /// </summary>
    public void BindData()
    {
        ProposalDet objentity = new ProposalDet();
        ProposalBAL objService = new ProposalBAL();

        try
        {
            objentity.strAction = "TPA";
            objentity.strProposalNo = txtProposalID.Text;

            DataSet ds = objService.GetProposalTrackDetails(objentity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DivNoRecord.Visible = false;
                Divapplicationdetails.Visible = true;

                Hypr_ProposalNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchProposalNo"]);
                Hypr_ProposalNo.NavigateUrl = "~/Portal/Proposal/ProposalDetails.aspx?Pno=" + Convert.ToString(ds.Tables[0].Rows[0]["vchProposalNo"]);
                Lbl_Company_Name.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchIndustryName"]);
                Lbl_Constitution_Name.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchConstitution"]);
                Lbl_Project_Type.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchProjectType"]);
                Lbl_Application_for.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchApplicationFor"]);
                Lbl_Current_Status.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchStatusName"]);

                if (Convert.ToString( ds.Tables[0].Rows[0]["vchActionToBeTakenBy"]) != "")
                {
                    Lbl_Action_Tobe_Taken_By.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchActionToBeTakenBy"]);
                }
                else
                {
                    Lbl_Action_Tobe_Taken_By.Text = "-NA-";
                }

                /*---------------------------------------------------------------------------*/

                if (Convert.ToString( ds.Tables[0].Rows[0]["vchActionTakenBy"]) != "")
                {
                    Lbl_Action_Taken_By.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchActionTakenBy"]);
                }
                else
                {
                    Lbl_Action_Taken_By.Text = "-NA-";
                }

                /*---------------------------------------------------------------------------*/

                if (Convert.ToString(ds.Tables[0].Rows[0]["vchPaymentStatus"]).ToUpper() == "PAID")
                {
                    Lbl_Payment_Status.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchPaymentStatus"]);
                    Lbl_Payment_Status.ForeColor = System.Drawing.Color.Green;
                }
                else if (Convert.ToString(ds.Tables[0].Rows[0]["vchPaymentStatus"]).ToUpper() == "UNPAID")
                {
                    Lbl_Payment_Status.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchPaymentStatus"]);
                    Lbl_Payment_Status.ForeColor = System.Drawing.Color.Red;
                }

                /*---------------------------------------------------------------------------*/

                Lbl_Payment_Amount.Text = Convert.ToString(ds.Tables[0].Rows[0]["decFee"]);
                Lbl_Apply_By.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchCompName"]);

                /*---------------------------------------------------------------------------*/

                if (Convert.ToString(ds.Tables[0].Rows[0]["dtmPaymentDate"]) != "")
                {
                    Lbl_Payment_Date.Text = DateTime.Parse(Convert.ToString(ds.Tables[0].Rows[0]["dtmPaymentDate"])).ToString("dd-MMM-yyyy");
                }
                else
                {
                    Lbl_Payment_Date.Text = "-NA-";
                }

                /*---------------------------------------------------------------------------*/

                if (Convert.ToString(ds.Tables[0].Rows[0]["dtmEndOfORTPSTimeline"]) != "")
                {
                    Lbl_Ortps_Timeline.Text = DateTime.Parse(Convert.ToString(ds.Tables[0].Rows[0]["dtmEndOfORTPSTimeline"])).ToString("dd-MMM-yyyy");
                }
                else
                {
                    Lbl_Ortps_Timeline.Text = "-NA-";
                }

                /*---------------------------------------------------------------------------*/

                if (Convert.ToString(ds.Tables[0].Rows[0]["dtmRaiseQueryDate"]) != "")
                {
                    Lbl_Querey_Date.Text = DateTime.Parse(ds.Tables[0].Rows[0]["dtmRaiseQueryDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    Lbl_Querey_Date.Text = "-NA-";
                }

                /*---------------------------------------------------------------------------*/

                Lbl_LandRequFromGovt.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchLandRequired"]);

                /*---------------------------------------------------------------------------*/

                if (Convert.ToString(ds.Tables[0].Rows[0]["dtmRevertQueryDate"]) != "")
                {
                    Lbl_Revert_Query_Date.Text = DateTime.Parse(ds.Tables[0].Rows[0]["dtmRevertQueryDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    Lbl_Revert_Query_Date.Text = "-NA-";
                }
            }
            else
            {
                DivNoRecord.Visible = true;
                Divapplicationdetails.Visible = false;
                Lbl_Norecord.Text = "No record found.";
            }

            /*---------------------------------------------------------------------------*/
            ///Application Landing Status (Forward To IDCO)
            /*---------------------------------------------------------------------------*/
            if (ds.Tables[1].Rows.Count > 0)
            {
                //divLandDetails.Visible = true;
                //DivNolandrecord.Visible = false;

                GrdLandDetails.DataSource = ds.Tables[1];
                GrdLandDetails.DataBind();
            }
            else
            {
                //divLandDetails.Visible = false;
                //DivNolandrecord.Visible = true;

                GrdLandDetails.DataSource = ds.Tables[1];
                GrdLandDetails.DataBind();
            }

            /*---------------------------------------------------------------------------*/
            ///Land Allotment Progress Status
            /*---------------------------------------------------------------------------*/
            if (ds.Tables[2].Rows.Count > 0)
            {
                //divLandDetails.Visible = true;
                //DivNolandrecord.Visible = false;

                GridLandIdco.DataSource = ds.Tables[2];
                GridLandIdco.DataBind();
            }
            else
            {
                //divLandDetails.Visible = false;
                //DivNolandrecord.Visible = true;

                GridLandIdco.DataSource = ds.Tables[2];
                GridLandIdco.DataBind();
            }

            /*---------------------------------------------------------------------------*/
            ///Payment Details
            /*---------------------------------------------------------------------------*/
            if (ds.Tables[3].Rows.Count > 0)
            {
                GrdPaymentDetails.DataSource = ds.Tables[3];
                GrdPaymentDetails.DataBind();
            }
            else
            {
                GrdPaymentDetails.DataSource = ds.Tables[3];
                GrdPaymentDetails.DataBind();
            }

            /*---------------------------------------------------------------------------*/
            ///Query Details
            /*---------------------------------------------------------------------------*/
            if (ds.Tables[4].Rows.Count > 0)
            {
                GrdQuereyDetails.DataSource = ds.Tables[4];
                GrdQuereyDetails.DataBind();
            }
            else
            {
                GrdQuereyDetails.DataSource = ds.Tables[4];
                GrdQuereyDetails.DataBind();
            }

            /*---------------------------------------------------------------------------*/
            ///Action Details
            /*---------------------------------------------------------------------------*/
            if (ds.Tables[5].Rows.Count > 0)
            {
                GrdActionDetails.DataSource = ds.Tables[5];
                GrdActionDetails.DataBind();
            }
            else
            {
                GrdActionDetails.DataSource = ds.Tables[5];
                GrdActionDetails.DataBind();
            }

            /*---------------------------------------------------------------------------*/
            ///AMS Details
            /*---------------------------------------------------------------------------*/

            //if (ds.Tables[6].Rows.Count > 0)
            //{
            //    DivNoRecordAMS.Visible = false;
            //    DivAMSdetails.Visible = true;
            //    if (Convert.ToString( ds.Tables[6].Rows[0]["vchStatusName"]) != "")
            //    {
            //        Lbl_AMSstatus.Text = Convert.ToString(ds.Tables[6].Rows[0]["vchStatusName"]);
            //        Lbl_AMSstatus.ForeColor = System.Drawing.Color.Green;
            //    }
            //    else
            //    {
            //        Lbl_AMSstatus.Text = "-NA-";
            //    }
            //    if ( Convert.ToString( ds.Tables[6].Rows[0]["vchNodalOfficerName"]) != "")
            //    {
            //        Lbl_Nodalofficer.Text = Convert.ToString(ds.Tables[6].Rows[0]["vchNodalOfficerName"]);
            //    }
            //    else
            //    {
            //        Lbl_Nodalofficer.Text = "-NA-";
            //    }

            //}
            //else
            //{
            //    DivNoRecordAMS.Visible = true;
            //    DivAMSdetails.Visible = false;
            //    Lbl_NorecordAMS.Text = "No record found.";
            //}
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GrdPaymentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hdnpaymentstatus = (HiddenField)e.Row.FindControl("HdnPaymStatus");
            Label Lblpaymsta = (Label)e.Row.FindControl("Lbl_PaymentStatus");
            if (Hdnpaymentstatus.Value == "0")
            {
                Lblpaymsta.ForeColor = System.Drawing.Color.Red;

            }
            else if (Hdnpaymentstatus.Value == "1")
            {
                Lblpaymsta.ForeColor = System.Drawing.Color.Green;
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
    protected void GrdActionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink refordoc = (HyperLink)e.Row.FindControl("hprlnkreferdoc");
            HyperLink certif = (HyperLink)e.Row.FindControl("hprlnkCertif");
            HyperLink scorecard = (HyperLink)e.Row.FindControl("hprlnkscorecard");

            HiddenField hdnfileval = (HiddenField)e.Row.FindControl("hdnfileval");
            HiddenField hdnCertif = (HiddenField)e.Row.FindControl("hdnCertif");
            HiddenField hdnScoreCard = (HiddenField)e.Row.FindControl("hdnScoreCard");

            Label lblReferdoc = (Label)e.Row.FindControl("lblReferdoc");
            Label lblCertif = (Label)e.Row.FindControl("lblCertif");
            Label lblScoreCard = (Label)e.Row.FindControl("lblScoreCard");

            /*---------------------------------------------------------------------------*/

            if (hdnfileval.Value == "")
            {
                lblReferdoc.Text = "NA";
                lblReferdoc.Visible = true;
                lblReferdoc.ForeColor = System.Drawing.Color.Red;
                refordoc.Visible = false;
            }
            else
            {
                refordoc.ToolTip = hdnfileval.Value;
                refordoc.Target = "_Blank";
                refordoc.NavigateUrl = "~/Proposal/ApprovalDocs/" + hdnfileval.Value;
            }

            /*---------------------------------------------------------------------------*/

            if (hdnCertif.Value == "")
            {
                lblCertif.Text = "NA";
                lblCertif.Visible = true;
                lblCertif.ForeColor = System.Drawing.Color.Red;
                certif.Visible = false;
            }
            else
            {
                certif.ToolTip = hdnCertif.Value;
                certif.Target = "_Blank";
                certif.NavigateUrl = "~/Proposal/PEALCertificate/" + hdnCertif.Value;
            }

            /*---------------------------------------------------------------------------*/

            if (hdnScoreCard.Value == "")
            {
                lblScoreCard.Text = "NA";
                lblScoreCard.Visible = true;
                lblScoreCard.ForeColor = System.Drawing.Color.Red;
                scorecard.Visible = false;
            }
            else
            {
                scorecard.ToolTip = hdnScoreCard.Value;
                scorecard.Target = "_Blank";
                scorecard.NavigateUrl = "~/Proposal/ScoreCard/" + hdnScoreCard.Value;
            }
        }
    }

    /// <summary>
    /// Get transaction information  from treasury 
    /// </summary>
    protected void BtnPaymentTransaction_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        Label Lbl_OrderNo = (Label)row.FindControl("Lbl_OrderNo");
        Label Lbl_Amount = (Label)row.FindControl("Lbl_ChallanAmount");
        TreasuryPaymentTracking objTreasury = new TreasuryPaymentTracking();

        Lbl_Msg_Restful.Text = "";

        try
        {
            string strResult = objTreasury.GetPaymentStatusFromTreasury(Lbl_OrderNo.Text, Convert.ToDecimal(Lbl_Amount.Text));
            Lbl_Msg_Restful.Text = strResult;
            Lbl_Msg_Restful.ForeColor = System.Drawing.Color.Blue;
        }
        catch (Exception ex)
        {
            Lbl_Msg_Restful.Text = ex.Message.ToString();
            Lbl_Msg_Restful.ForeColor = System.Drawing.Color.Red;
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
            Util.LogError(ex, "ProposalTracking");
        }
        finally
        {
            Response.End();
        }
    }
}