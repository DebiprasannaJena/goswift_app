using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.ServiceModel;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Web.Script.Serialization;
using System.Net;
using System.Text;
using System.Web.Services;
using Ionic.Zip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EntityLayer.Service;
using EntityLayer.Agenda;
using System.Xml.Serialization;
using BusinessLogicLayer.Agenda;
using Common;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;

public partial class Mastermodule_ViewProposal : SessionCheck
{
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();

  
    ProposalBAL objService = new ProposalBAL();
    ServiceDetails objServiceEntity = new ServiceDetails();
    DepartmentSMSClass objDepartmntSms = new DepartmentSMSClass();
    ProposalDet objProposal = new ProposalDet();
    PromoterDet objprom = new PromoterDet();

    string filepath = "";
    string filepathidco = "";
    bool smsStatus;
    bool mailStatus;
    bool MailStatus = true;
    string PEALCertificatefilepath = "";
    string ScoreCardPath = "";
    int intRetVal = 0;
    int intRecordCount = 0; //// Added by Sushant Jena On Dt:-19-Feb-2020
    bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

       
        if (!IsPostBack)
        {
            txtActionDate.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);

            fillGridview();
            BindStatus();
            //(START) this section of code checks if the page postback is due to genuine submit by user or by pressing "refresh"
            ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
            Session["SessionId"] = ViewState["ViewStateId"].ToString();
            //(END) this section of code checks if the page postback is due to genuine submit by user or by pressing "refresh"
      
            BindStatus1();
           
        }
        else
        {
            //(START) this section of code checks if the page postback is due to genuine submit by user or by pressing "refresh"
            if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
            {
                IsPageRefresh = true;
            }

            Session["SessionId"] = System.Guid.NewGuid().ToString();
            ViewState["ViewStateId"] = Session["SessionId"].ToString();
            //(END) this section of code checks if the page postback is due to genuine submit by user or by pressing "refresh"
        }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "ProposalForwardDepart");
        }
    }

    #region FunctionUsed

  
    private void BindStatus1()
    {
        try
        {
          
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "SM";
            objProp.vchProposalNo = " ";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            drpStatusDet.DataSource = objProjList;
            drpStatusDet.DataTextField = "vchStatusName";
            drpStatusDet.DataValueField = "intStatusId";
            drpStatusDet.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpStatusDet.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForwardDepart");
        }
    }
    private void BindStatus()
    {       
        ProposalDet objProp = new ProposalDet();
        objProp.strAction = "S";
        List<ProposalDet> objProjList = objService.PopulateStatus(objProp).ToList();
        drpStatus.DataSource = objProjList;
        drpStatus.DataTextField = "strStatus";
        drpStatus.DataValueField = "intStatus";
        drpStatus.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        drpStatus.Items.Insert(0, list);
    }
    public void fillGridview()
    {
        try
        {
            objProposal.strAction = "L";
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objProposal.compName = txtCompanyName.Text.Trim();
            objProposal.intStsdet = Convert.ToInt32(drpStatusDet.SelectedValue);
            objProposal.strProposalNo = txtProposalNo.Text.Trim();
            List<ProposalDet> objProposalList = objService.getProposalDetails(objProposal).ToList();

            gvService.DataSource = objProposalList;
            gvService.DataBind();

            intRetVal = gvService.Rows.Count;
            intRecordCount = objProposalList.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForwardDepart");
        }
        finally
        {
            objProposal = null;
        }
    }

    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtRemarks.Text = "";
        drpStatus.SelectedIndex = 0;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        fillGridview();
    }

    #region "Display Google Paging"

    private void DisplayPaging()
    {
        if (gvService.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvService.PageIndex + 1 == gvService.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + intRecordCount + "</b> Of <b>" + intRecordCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvService.Rows[0].Cells[0].Text) + (gvService.PageSize - 1)) + "</b> Of <b>" + intRecordCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }

    #endregion

    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);

           
            LinkButton btn = (LinkButton)e.Row.FindControl("LinkButton1");
            HiddenField hdndistId = (HiddenField)e.Row.FindControl("hdnDistId");
            string distVal = hdndistId.Value;
            
            HiddenField hdnTextVal1 = (HiddenField)e.Row.FindControl("hdnTextVal1");
            HiddenField hdnLandReq = (HiddenField)e.Row.FindControl("hdnLandReqIDCO");
            LinkButton lnkBtn = (e.Row.FindControl("LinkButton2") as LinkButton);
          
            Label lblIdco = (e.Row.FindControl("lblIdco") as Label);
            string strProposalNo = hdnTextVal1.Value;
            if (e.Row.Cells[2].Text == "1")
            {
                e.Row.Cells[2].Text = "Large Industries";
               
                dvscore.Visible = false;
                hdnProjectType.Value = "1";

            }
            else if (e.Row.Cells[2].Text == "2")
            {
                e.Row.Cells[2].Text = "MSME";
                hdnProjectType.Value = "2";

                    if (Convert.ToInt32(Session["Userid"]) == 166)
                    {
                        dvscore.Visible = false;
                        hdnProjectType.Value = "1";
                    }
                    if (Convert.ToInt32(Session["Userid"]) == 167)
                    {
                            dvscore.Visible = false;
                            hdnProjectType.Value = "1";
                    }
                    else
                    {
                        dvscore.Visible = true;
                    }
            }

            if (gvService.DataKeys[e.Row.RowIndex].Values[6].ToString() == "Pending at SLFC")
            {
                lblIdco.Visible = false;
                btn.Visible = false;
                lnkBtn.Visible = false;
               
            }
            else
            {   
                if (e.Row.Cells[2].Text == "MSME")
                {  
                    gvService.Columns[6].Visible = true;
                }
                if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[5]) == "2")
                {
                    if (Convert.ToDecimal(hdnLandReq.Value) > 0) //Query Raised
                    {
                        if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[13].ToString()) == "1")
                        {
                            lnkBtn.Visible = false;
                            lblIdco.Visible = true;
                            if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[12].ToString()) != "")
                            {
                                lblIdco.Text = Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[12].ToString());
                            }

                        }
                        else if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[7].ToString()) == "0")
                        {
                            if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[10].ToString()) == "4")
                            {
                                lnkBtn.Visible = false;
                                lblIdco.Visible = true;
                                lblIdco.Text = Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[12].ToString());
                            }
                           if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[10].ToString()) == "1")
                            {
                                lnkBtn.Visible = false;
                                lblIdco.Visible = true;
                                lblIdco.Text = Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[12].ToString()); 
                            }
                            else
                            {
                                lnkBtn.Visible = true;
                                lblIdco.Visible = false;
                            }
                        }
                        else
                        {
                            lnkBtn.Visible = false;
                            lblIdco.Visible = false;
                        }
                    }
                    else
                    {
                        lnkBtn.Visible = false;
                        lblIdco.Visible = false;
                    }
                }
                else
                {
                    lnkBtn.Visible = false;
                    lblIdco.Visible = false;
                }
                if (gvService.DataKeys[e.Row.RowIndex].Values[1].ToString() == Convert.ToString(Session["UserId"]))                  
                {
                    // Status 7- Means Deffered and user can take further action
                    // Status 1- Means Applied and user can take further action
                    if ((Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[5]) == "7") || (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[5]) == "1"))
                    {
                        btn.Visible = true;
                    }
                    else
                    {
                        btn.Visible = false;
                    }

                }
                else
                {
                    btn.Visible = false;
                }
            }
           

            HyperLink hprlnkproposal = (HyperLink)e.Row.FindControl("hypLink");
            hprlnkproposal.NavigateUrl = "../Proposal/ProposalDetails.aspx?Pno=" + hdnTextVal1.Value;
            //Added By Pranay Kumar on 11-Sept-2017 for Show/Hide of Raised Query Button
            int intQueryStatus = Convert.ToInt32(gvService.DataKeys[e.Row.RowIndex].Values[3]);
            LinkButton lbtnRaise = (e.Row.FindControl("lbtnRaise") as LinkButton);

            if (intQueryStatus == 1) //Query Raised
            {
                lbtnRaise.Visible = true;
            }
            else if (intQueryStatus == 0) //IF Query Date is Blank
            {
                lbtnRaise.Visible = false;
            }
            else if (intQueryStatus == 2) //If Query Date is Expired
            {
                lbtnRaise.Visible = false;

            }

           
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "QD";
            objProp.strProposalNo = hdnTextVal1.Value;
            List<ProposalDet> objProposalList = objService.getRaisedQueryDetails(objProp).ToList();
            HtmlGenericControl QueryHist = (HtmlGenericControl)e.Row.FindControl("QueryHist");
            HtmlGenericControl QueryHist1 = (HtmlGenericControl)e.Row.FindControl("QueryHist1");
            if (objProposalList.Count > 0)
            {
                string strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th>Query Reference No.</th><th>User Name</th><th> Query Details</th><th>Date</th><th>Files</th></tr>";
                for (int i = 0; i < objProposalList.Count; i++)
                {
                    if (objProposalList[i].strFileName == null || objProposalList[i].strFileName == "")
                    {
                        strHTMlQuery = strHTMlQuery + "<tr><td>" + objProposalList[i].strQueryStatus + "</td><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a target='' href='#'>--</a>" + "</td></tr>";
                    }
                    else
                    {
                        strHTMlQuery = strHTMlQuery + "<tr><td>" + objProposalList[i].strQueryStatus + "</td><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a href='../../QueryFiles/" + objProposalList[i].strFileName + "'>Download</a>" + "</td></tr>";
                    }
                }
                strHTMlQuery = strHTMlQuery + "</table>";

                QueryHist.InnerHtml = strHTMlQuery;
                QueryHist1.InnerHtml = strHTMlQuery;
            }
            //Ended By Pranay Kumar on 10-Sept-2017 for Show/Hide of Raised Query Button
            string strCurrQueryStatus = Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[4]);
            LinkButton lbtnQueryDtls = (e.Row.FindControl("lbtnQueryDtls") as LinkButton);
            if (strCurrQueryStatus == "--")
            {
                lbtnQueryDtls.Visible = false;
                lbtnQueryDtls.Text = "--";
            }
            else if (strCurrQueryStatus == "Completed")
            {
                lbtnQueryDtls.CssClass = "btn btn-success btn-sm";
                lbtnQueryDtls.Text = "<i class='fa fa-eye' aria-hidden='true'></i>";
            }
            else if (strCurrQueryStatus == "QUERY RAISED")
            {
                lbtnQueryDtls.Text = strCurrQueryStatus;
                lbtnQueryDtls.CssClass = "btn btn-success btn-sm";
            }
            else if (strCurrQueryStatus == "QUERY RESPONDED")
            {
                lbtnQueryDtls.Text = strCurrQueryStatus;
                lbtnQueryDtls.CssClass = "label-warning label label-default";
                lbtnQueryDtls.Visible = false;
                lbtnRaise.Text = strCurrQueryStatus;
                lbtnRaise.CssClass = "label-warning label label-default";
            }
           
          }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "ProposalForwardDepart");
        }
    }
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        fillGridview();
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvService.PageIndex = 0;
            gvService.AllowPaging = false;
            fillGridview();
        }
        else
        {
            lbtnAll.Text = "All";
            gvService.AllowPaging = true;
            fillGridview();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            #region Validation

            if (drpStatus.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('<strong>Please select status !</strong>', 'GOSWIFT');</script>", false);
                drpStatus.Focus();
                return;
            }
            if (txtRemarks.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('<strong>Remarks can not be left blank !</strong>', 'GOSWIFT');</script>", false);
                drpStatus.Focus();
                return;
            }
            if (txtActionDate.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('<strong>Approval / Reject / Deferred date can not be left blank !</strong>', 'GOSWIFT');</script>", false);
                drpStatus.Focus();
                return;
            }
            if (Convert.ToDateTime(txtActionDate.Text) > DateTime.Now)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('<strong>Approval / Reject / Deferred date should not be greater than current date !</strong>', 'GOSWIFT');</script>", false);
                drpStatus.Focus();
                return;
            }

            #endregion

            /*-------------------------------------------------------------------------------------*/
            ///// Reference Document
            /*-------------------------------------------------------------------------------------*/
            if (docUpload.HasFile)
            {
                if (Path.GetExtension(docUpload.FileName).ToLower() != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted !');", true);
                    return;
                }

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Proposal/ApprovalDocs/"));
                filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + hdnproposalno.Value + "_" + Session["UserId"] + ".pdf", DateTime.Now);
                if (!string.IsNullOrEmpty(docUpload.FileName))
                {
                    if (dir.Exists)
                    {
                        docUpload.SaveAs(Server.MapPath("~/Proposal/ApprovalDocs/" + filepath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Proposal/ApprovalDocs"));
                        docUpload.SaveAs(Server.MapPath("~/Proposal/ApprovalDocs/" + filepath));
                    }
                }
            }

            string Uploadname = filepath;

            /*-------------------------------------------------------------------------------------*/
            ///// PEAL Certificate Document          
            /*-------------------------------------------------------------------------------------*/
            if (docPEALCerti.HasFile)
            {
                if (Path.GetExtension(docPEALCerti.FileName).ToLower() != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "<script>jAlert('Only .pdf file accepted!');</script>", true);
                    return;
                }

                System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(Server.MapPath("~/Proposal/PEALCertificate/"));
                PEALCertificatefilepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + hdnproposalno.Value + "_" + Session["UserId"] + ".pdf", DateTime.Now);
                if (!string.IsNullOrEmpty(docPEALCerti.FileName))
                {
                    if (dir1.Exists)
                    {
                        docPEALCerti.SaveAs(Server.MapPath("~/Proposal/PEALCertificate/" + PEALCertificatefilepath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Proposal/PEALCertificate"));
                        docPEALCerti.SaveAs(Server.MapPath("~/Proposal/PEALCertificate/" + PEALCertificatefilepath));
                    }
                }
            }

            /*-------------------------------------------------------------------------------------*/
            ///// Score Card Document
            /*-------------------------------------------------------------------------------------*/
            if (docScoreCard.HasFile)
            {
                if (Path.GetExtension(docScoreCard.FileName).ToLower() != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "<script>jAlert('Only .pdf file accepted!');</script>", true);
                    return;
                }

                System.IO.DirectoryInfo dir3 = new System.IO.DirectoryInfo(Server.MapPath("~/Proposal/ScoreCard/"));
                ScoreCardPath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + hdnproposalno.Value + "_" + Session["UserId"] + ".pdf", DateTime.Now);
                if (!string.IsNullOrEmpty(docScoreCard.FileName))
                {
                    if (dir3.Exists)
                    {
                        docScoreCard.SaveAs(Server.MapPath("~/Proposal/ScoreCard/" + ScoreCardPath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Proposal/ScoreCard"));
                        docScoreCard.SaveAs(Server.MapPath("~/Proposal/ScoreCard/" + ScoreCardPath));
                    }
                }
            }

            string UploadScoreCard = ScoreCardPath;
            string UploadPEALCertificatefile = PEALCertificatefilepath;

            /*-------------------------------------------------------------------------------------*/

            objProposal.strAction = "A";
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objProposal.vchProposalno = hdnproposalno.Value;
            objProposal.intStatus = Convert.ToInt32(drpStatus.SelectedValue);
            objProposal.strFileName = Uploadname;
            objProposal.strPEALCertificate = UploadPEALCertificatefile;
            objProposal.strScorecard = ScoreCardPath;
            objProposal.strRemarks = txtRemarks.Text;
            objProposal.decExtendLand = 0;
            objProposal.vchIndustryCode = ddlIDCOName.SelectedValue;
            objProposal.intLandReqd = Convert.ToInt32(ddlLandacquiredIDCO.SelectedValue);
            objProposal.strUpdatedOn = txtActionDate.Text; ////Added by Sushant Jena on Dt:-11-Feb-2021

            /// DML Operation
            string strRetVal = objService.ProposalTakeAction(objProposal);
            if (strRetVal == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Record Saved Successfully.</strong>', 'GOSWIFT');</script>", false);

                CommonHelperCls comm = new CommonHelperCls();
                List<LandDet> objProjList = new List<LandDet>();
                LandDet objProp = new LandDet();

                /*----------------------------------------------------------------*/
                /// Get Mobile Number and Email Id of the Investor.
                /*----------------------------------------------------------------*/
                objProp.strAction = "N";
                objProp.vchProposalNo = hdnCreted.Value;
                objProp.ApplicationNo = hdnproposalno.Value;
                objProjList = objService.GETMobileNo(objProp).ToList();

                if (objProjList.Count > 0)
                {
                    string strMobileNo = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[0]);
                    string strUnitName = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[1]);
                    string[] toEmail = new string[1];
                    toEmail[0] = Convert.ToString(objProjList[0].Email);

                    /*----------------------------------------------------------------*/
                    /// Get SMS and Email Contents
                    /*----------------------------------------------------------------*/
                    string strSMSContent = "";
                    string strTemplateId = "";
                    string strMsgType = "";

                    InvestorDetails objInvDet = new InvestorDetails();
                    InvestorRegistration objInvService = new InvestorRegistration();
                    objInvDet.strAction = "ST4";
                    DataTable dtcontent = objInvService.GetSMSContent(objInvDet);
                    if (dtcontent.Rows.Count > 0)
                    {
                        string strEvent = dtcontent.Rows[0]["vchEvent"].ToString();
                        strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString();
                        strTemplateId = dtcontent.Rows[0]["vchTemplateId"].ToString();
                        strMsgType = dtcontent.Rows[0]["vchMsgType"].ToString();
                    }

                    //Department has[STATUS] the application for New Proposal of M / s[INDUSTRY NAME], Please Log into https://investodisha.gov.in for further details.

                    /*----------------------------------------------------------------*/
                    /// Prepare Final SMS and Email Content
                    /*----------------------------------------------------------------*/
                    string strSubject = "";
                    string strEmailContent = "";

                    if (Convert.ToInt32(drpStatus.SelectedValue) == 2) ////Approved
                    {
                        strSubject = "SWP: Application Approved";
                        strEmailContent = "Dear Investor"
                                         + "</br>"
                                         + "Department has approved the application for New Proposal of M/s " + strUnitName + " ,Please Log into https://investodisha.gov.in for further details.";

                        strSMSContent = strSMSContent.Replace("[STATUS]", "approved");
                        strSMSContent = strSMSContent.Replace("[INDUSTRY NAME]", strUnitName);
                    }
                    else if (Convert.ToInt32(drpStatus.SelectedValue) == 3) ////Rejected
                    {
                        strSubject = "SWP: Application Reject";
                        strEmailContent = "Dear Investor"
                                         + "</br>"
                                         + "Department has rejected the application for New Proposal of M/s " + strUnitName + " ,Please Log into https://investodisha.gov.in for further details.";

                        strSMSContent = strSMSContent.Replace("[STATUS]", "rejected");
                        strSMSContent = strSMSContent.Replace("[INDUSTRY NAME]", strUnitName);
                    }
                    else if (Convert.ToInt32(drpStatus.SelectedValue) == 7) ////Deferred
                    {
                        strSubject = "SWP: Application Deferred";
                        strEmailContent = "Dear Investor"
                                         + "</br>"
                                         + "Department has deferred the application for New Proposal of M/s " + strUnitName + " ,Please Log into https://investodisha.gov.in for further details.";

                        strSMSContent = strSMSContent.Replace("[STATUS]", "deferred");
                        strSMSContent = strSMSContent.Replace("[INDUSTRY NAME]", strUnitName);
                    }

                    /*----------------------------------------------------------------*/
                    /// Send SMS
                    /*----------------------------------------------------------------*/
                    bool smsStatus = comm.SendSmsWithTemplate(strMobileNo, strSMSContent, strTemplateId, strMsgType);

                    /*----------------------------------------------------------------*/
                    /// Send Email
                    /*----------------------------------------------------------------*/
                    bool mailStatus = comm.sendMail(strSubject, strEmailContent, toEmail, true);

                    /*----------------------------------------------------------------*/
                    /// Update SMS and Email Status in Transaction Table
                    /*----------------------------------------------------------------*/
                    string str = comm.UpdateMailSMSStaus("PEAL", strMobileNo, toEmail[0], strSubject, "0", "0", 0, "0", strSMSContent, strEmailContent, smsStatus, mailStatus);

                    /*----------------------------------------------------------------*/
                    /// In the case of PEAL "Approval" Send a Promotional SMS to the investor.
                    /// The promotional SMS will not be sent if the PEAL is rejected or deferred.
                    /*----------------------------------------------------------------*/
                    if (Convert.ToInt32(drpStatus.SelectedValue) == 2) ////Approved
                    {
                        comm.SendPromotionalSMS(strMobileNo, "AFTER_PEAL_APPROVAL");
                    }
                }

                //if (Convert.ToInt32(drpStatus.SelectedValue) == 2) ////Approved
                //{
                //    objProp.strAction = "N";
                //    objProp.vchProposalNo = hdnCreted.Value;
                //    objProp.ApplicationNo = hdnproposalno.Value;
                //    objProjList = objService.GETMobileNo(objProp).ToList();

                //    string mobile = "";
                //    string smsContent = "";
                //    string[] toEmail = new string[1];

                //    if (objProjList.Count > 0)
                //    {
                //        mobile = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[0]);
                //        smsContent = Convert.ToString(objProjList[0].SMSContent);
                //        toEmail[0] = Convert.ToString(objProjList[0].Email);
                //        string strSubject = "SWP: Application Approved";
                //        string strBody = "Department has approved the application for New Proposal of M/s " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[1]) + " ,Please Log into https://investodisha.gov.in for further details. ";

                //        mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                //        smsStatus = comm.SendSmsNew(mobile, strBody);

                //        // FOR SMS and Mail Status Update
                //        string str = comm.UpdateMailSMSStaus("PEAL", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), "1053", Convert.ToInt32(drpStatus.SelectedValue), hdnproposalno.Value, strBody, strBody, mailStatus, smsStatus);
                //    }
                //}
                //else if (Convert.ToInt32(drpStatus.SelectedValue) == 3) ////Rejected
                //{
                //    objProp.strAction = "O";
                //    objProp.vchProposalNo = hdnCreted.Value;
                //    objProp.ApplicationNo = hdnproposalno.Value;
                //    objProjList = objService.GETMobileNo(objProp).ToList();

                //    string mobile = "";
                //    string smsContent = "";
                //    string[] toEmail = new string[1];

                //    if (objProjList.Count > 0)
                //    {
                //        mobile = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[0]);
                //        smsContent = Convert.ToString(objProjList[0].SMSContent);
                //        toEmail[0] = Convert.ToString(objProjList[0].Email);
                //        string strSubject = "SWP: Application Reject";
                //        string strBody = "Department has rejected the application for New Proposal of M/s " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[1]) + " ,Please Log into https://investodisha.gov.in for further details. ";

                //        mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                //        smsStatus = comm.SendSmsNew(mobile, strBody);

                //        // FOR SMS and Mail Status Update
                //        string str = comm.UpdateMailSMSStaus("PEAL", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), "1053", Convert.ToInt32(drpStatus.SelectedValue), hdnproposalno.Value, strBody, strBody, mailStatus, smsStatus);
                //    }
                //}
                //else if (Convert.ToInt32(drpStatus.SelectedValue) == 7)////Deferred
                //{
                //    objProp.strAction = "O";
                //    objProp.vchProposalNo = hdnCreted.Value;
                //    objProp.ApplicationNo = hdnproposalno.Value;
                //    objProjList = objService.GETMobileNo(objProp).ToList();

                //    string mobile = "";
                //    string smsContent = "";
                //    string[] toEmail = new string[1];

                //    if (objProjList.Count > 0)
                //    {
                //        mobile = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[0]);
                //        smsContent = Convert.ToString(objProjList[0].SMSContent);
                //        toEmail[0] = Convert.ToString(objProjList[0].Email);
                //        string strSubject = "SWP: Application Deferred";
                //        string strBody = "Department has deferred the application for New Proposal of M/s " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[1]) + " ,Please Log into https://investodisha.gov.in for further details. ";

                //        mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                //        smsStatus = comm.SendSmsNew(mobile, strBody);

                //        // FOR SMS and Mail Status Update
                //        string str = comm.UpdateMailSMSStaus("PEAL", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), "1053", Convert.ToInt32(drpStatus.SelectedValue), hdnproposalno.Value, strBody, strBody, mailStatus, smsStatus);
                //    }
                //}

                ClearField();
            }
            else if (strRetVal == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('Action can not be taken Successfully;</script>');", true);
            }
            else if (strRetVal == "11")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('FAILLLLLLLLLL;</script>');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('000000000000;</script>');", true);
            }

            fillGridview();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForwardDepart");
        }
        finally
        {
            objProposal = null;
        }
    }

    private void ClearField()
    {
        drpStatus.SelectedValue = "0";
        txtRemarks.Text = "";
    }
    private void ClearField1()
    {
        txtLandRecomendBySLFC.Text = "0";
    }
  
    private void IntegrationPEALWithIDCO()
    {
        CNET objCnet = new CNET();

        string inputJson = "";


        string serviceUrl = "";
        try
        {
            objCnet.vchProposalNo = hdnproposalno1.Value;
            List<CNET> objProposalList1 = objService.GetCNETCompanyDetails(objCnet).ToList();

            inputJson = (new JavaScriptSerializer()).Serialize(objProposalList1);
            inputJson = inputJson.TrimStart('[').TrimEnd(']');

            ProposalBAL objService1 = new ProposalBAL();
            ProposalBAL objService2 = new ProposalBAL();
            List<PromoterDet> objProposalList2 = objService2.GetIDCOEmailDetails().ToList();
            PromoterDet objpromo = new PromoterDet();
            objpromo.vchProposalNo = hdnproposalno1.Value;
            /*------------------------------------------------------------*/
            //// Write the input proposal number for each request.            
            Util.LogRequestResponse("ForwardToIdco", "ProposalNumber", hdnproposalno1.Value);
            /*------------------------------------------------------------*/

            string retIDCOVal1 = objService1.IDCOBtntatusUpdate(objpromo);

            /*------------------------------------------------------------*/
            //// Write the input json string for each request.            
            Util.LogRequestResponse("ForwardToIdco", "PEALJSONDATA", inputJson);
            /*------------------------------------------------------------*/

            serviceUrl = ConfigurationManager.AppSettings["CNETForwardIdco"]; 

            /*------------------------------------------------------------*/
            //// Write the input URL for each request.            
            Util.LogRequestResponse("ForwardToIdco", "IDCOURL", serviceUrl);
            /*------------------------------------------------------------*/


            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            Util.LogRequestResponse("ForwardToIdco", "BEFOREPOST", "");
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";
           

            Util.LogRequestResponse("ForwardToIdco", "POST", "PASS POST METHOD ");
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                Util.LogRequestResponse("ForwardToIdco", "streamWriter1", "PASS streamWriter ");
                //initiate the request
                var resToWrite = inputJson;
                streamWriter.Write(resToWrite);
                streamWriter.Flush();
                streamWriter.Close();
                Util.LogRequestResponse("ForwardToIdco", "streamWriter2", "Close");
            }

            /*------------------------------------------------------------*/
            //// Write the input json string for each request.            
            Util.LogRequestResponse("ForwardToIdco", "IntegrationPEALWithIDCO", serviceUrl + " <<----->> " + inputJson);
            /*------------------------------------------------------------*/
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    ///// Write the response string for each request.
                    Util.LogRequestResponse("ForwardToIdco", "IntegrationPEALWithIDCOResponse", "For Proposal No:- " + hdnproposalno1.Value + "-----" + strResult.IndexOf("failure_message").ToString() + "------" + strResult);

                    if (strResult.IndexOf("failure_message") != -1) //If idco end response come failure message then insert error response in table ,if responce come succes update record via swp_service api "UpdatePEALIngrationWithIDCOStatus"
                    {
                        CommonHelperCls comm = new CommonHelperCls();
                        objprom.vch_unique_application_id_from_swp = hdnproposalno1.Value;
                        objprom.vch_Error_Msg = JObject.Parse(strResult)["failure_message"].ToString();
                        objprom.vch_validation_Msg = JObject.Parse(strResult)["validation_message"].ToString();
                        objprom.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                        objprom.vch_Input_String = inputJson;
                        objService.ProposalCNETData(objprom);

                        string[] UAtoEmail = new string[1];
                        string strSubjectMsg = "Issue Raised for Proposal(" + hdnproposalno1.Value + ") during Forward to IDCO";
                        string strBody = JObject.Parse(strResult)["failure_message"].ToString() + " :: " + JObject.Parse(strResult)["validation_message"].ToString() + "<br/><br/><strong>JSON DATA</strong>::<br/><br/>" + inputJson;
                        string EmailIDs = "";
                        if (objProposalList2.Count > 0)
                        {
                            EmailIDs = Convert.ToString(objProposalList2[0].vchEmail.ToString());
                        }

                        
                        if (EmailIDs.Contains(','))
                        {
                            string[] RAEmailArry = EmailIDs.Split(',');
                            for (int i = 0; RAEmailArry.Length > i; i++)
                            {
                                if (RAEmailArry[i] != "" && RAEmailArry[i] != null)
                                {
                                    UAtoEmail[0] = RAEmailArry[i].ToString();
                                    MailStatus = comm.sendMail(strSubjectMsg, strBody, UAtoEmail, true);
                                    comm.UpdateMailSMSStaus("Issue", "", RAEmailArry[i].ToString(), "Issue Raised", "1", "1", 1, "1", strBody, strBody, smsStatus, MailStatus);
                                }
                            }
                        }
                        else
                        {
                            UAtoEmail[0] = EmailIDs;
                            MailStatus = comm.sendMail(strSubjectMsg, strBody, UAtoEmail, true);
                            comm.UpdateMailSMSStaus("Issue", "", EmailIDs, "Issue Raised", "1", "1", 1, "1", strBody, strBody, smsStatus, MailStatus);
                        }
                    }
                  
                }
            }
        }
        catch (Exception ex)
        {
            CommonHelperCls comm = new CommonHelperCls();
            string[] UAtoEmail = new string[1];
            string strSubjectMsg = "Issue Raised for Proposal No.(" + hdnproposalno1.Value + ") during Forward to IDCO (E)";
            string strBody = ex.Message.ToString() + "<br/><br/><strong>JSON DATA</strong>::<br/><br/>" + inputJson;
            ProposalBAL objService2 = new ProposalBAL();
            List<PromoterDet> objProposalList2 = objService2.GetIDCOEmailDetails().ToList();

            string EmailIDs = "";
            if (objProposalList2.Count > 0)
            {
                EmailIDs = Convert.ToString(objProposalList2[0].vchEmail.ToString());
            }

            
            if (EmailIDs.Contains(','))
            {
                string[] RAEmailArry = EmailIDs.Split(',');
                for (int i = 0; RAEmailArry.Length > i; i++)
                {
                    if (RAEmailArry[i] != "" && RAEmailArry[i] != null)
                    {
                        UAtoEmail[0] = RAEmailArry[i].ToString();
                        MailStatus = comm.sendMail(strSubjectMsg, strBody, UAtoEmail, true);
                         comm.UpdateMailSMSStaus("Issue", "", RAEmailArry[i].ToString(), "Issue Raised", "1", "1", 1, "1", strBody, strBody, smsStatus, MailStatus);
                    }
                }
            }
            else
            {
                UAtoEmail[0] = EmailIDs;
                MailStatus = comm.sendMail(strSubjectMsg, strBody, UAtoEmail, true);
                comm.UpdateMailSMSStaus("Issue", "", EmailIDs, "Issue Raised", "1", "1", 1, "1", strBody, strBody, smsStatus, MailStatus);
            }

            Util.LogError(ex, "ProposalForwardDepart");
          
        }
       
    }

    #region "Added By Pranay Kumar on 11-Sept-2017"
    protected void btnQuerySubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsPageRefresh == false)
            {
                Button btnQuerySubmit = (Button)sender;
                TextBox txtq1 = (TextBox)btnQuerySubmit.FindControl("txtq1");
                FileUpload FileUpload1 = (FileUpload)btnQuerySubmit.FindControl("FileUpload1");
                objProposal.strAction = "Q";
                objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                objProposal.strProposalNo = btnQuerySubmit.CommandArgument.ToString();
                objProposal.intStatus = 5;
                objProposal.strRemarks = txtq1.Text;
                string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_FFF_tt}" + "_" + btnSubmit.CommandArgument.ToString() + "_Query1" + ".pdf", DateTime.Now);

                if (FileUpload1.HasFile)
                {

                    if (Path.GetExtension(FileUpload1.FileName) != ".pdf")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
                        return;
                    }
                }
                
                
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/QueryFiles/"));
                if (!string.IsNullOrEmpty(FileUpload1.FileName))
                {
                    if (dir.Exists)
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/QueryFiles/" + filepath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/QueryFiles"));
                        FileUpload1.SaveAs(Server.MapPath("~/QueryFiles/" + filepath));

                    }
                }
                else { filepath = ""; }
                objProposal.strFileName = filepath;
                string strRetVal = objService.ProposalRaiseQuery(objProposal);
                fillGridview();
                if (strRetVal == "2")
                {
                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Query Raised Successfully.')</script>;", false);
                    //FOR SENDING MAIL & SMS
                    CommonHelperCls comm = new CommonHelperCls();                   
                    ProposalDet objProp = new ProposalDet();

                    objProp.strAction = "S";
                    objProp.strProposalNo = btnQuerySubmit.CommandArgument.ToString();
                    objProp.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                    List<ProposalDet> objProposalList = objService.getRaisedQueryDetails(objProp).ToList();
                    string mobile = "";
                    string smsContent = "";
                    string strSubject = "";
                    string strBody = "";
                    string strDeptSMSSubject = "";
                    string strDeptSMSBody = "";
                    string strDeptMailSubject = "";
                    string[] toEmail = new string[1];

                    if (objProposalList.Count > 0)
                    {
                        mobile = Convert.ToString(objProposalList[0].MobileNo);
                        smsContent = Convert.ToString(objProposalList[0].strSMSContent);
                        toEmail[0] = Convert.ToString(objProposalList[0].EmailID);
                        strSubject = Convert.ToString(objProposalList[0].EmailSubject);
                        strBody = Convert.ToString(objProposalList[0].EmailBody);
                        strDeptSMSSubject = Convert.ToString(objProposalList[0].strDeptSMSSub);
                        strDeptSMSBody = Convert.ToString(objProposalList[0].strDeptSMSContent);
                        strDeptMailSubject = Convert.ToString(objProposalList[0].strDeptMailContent);
                       

                        mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                        smsStatus = comm.SendSmsNew(mobile, smsContent);
                        // FOR SMS and Mail Status Update

                        string str = comm.UpdateMailSMSStaus("PEALQuery", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), "1053", 5, btnQuerySubmit.CommandArgument.ToString(), smsContent, strBody, mailStatus, smsStatus);
                    }
                    //For Sending SMS TO HOD
                    objProp.strAction = "T";
                    objProp.strProposalNo = btnQuerySubmit.CommandArgument.ToString();
                    objProposalList = objService.getRaisedQueryDetails(objProp).ToList();

                    if (objProposalList.Count > 0)
                    {
                        if (objProposalList[0].intNoOfTimes >= 2) // for fetching how many times query raised by dept
                        {
                            objServiceEntity.INT_SERVICEID = 500; //service id for peal is 500
                            objServiceEntity.strSubject = strDeptSMSSubject;
                            objServiceEntity.strBody = strDeptMailSubject;
                            objServiceEntity.smsContent = strDeptSMSBody;

                            objDepartmntSms.DepartmentSendSms(objServiceEntity);
                        }
                    }

                }
                else if (strRetVal == "4")
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Action can not be taken Successfully.')</script>;", false); }
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForwardDepart");
        }
        finally
        {
            objProposal = null;
        }
    }

    #endregion
    #region "ZIP DOWNLOAD"
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("QueryFiles");
            if (hdnFileNames.Value != "")
            {
                string[] arrFileName = hdnFileNames.Value.Split(',');
                for (int i = 0; i <= arrFileName.Count() - 1; i++)
                {
                    string FileName = "../../QueryFiles/" + Convert.ToString(arrFileName[i]);
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
            Response.End();
        }
    }

    #endregion

    protected void btnSubmitIdco_Click(object sender, EventArgs e)
    {
        try
        {
            if (docIdcoDoc.HasFile)
            {
                if (Path.GetExtension(docIdcoDoc.FileName) != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
                    return;
                }

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Proposal/IDCODocs/"));
                filepathidco = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + hdnproposalno1.Value + "_" + Session["UserId"] + ".pdf", DateTime.Now);
                if (!string.IsNullOrEmpty(docIdcoDoc.FileName))
                {
                    if (dir.Exists)
                    {
                        docIdcoDoc.SaveAs(Server.MapPath("~/Proposal/IDCODocs/" + filepathidco));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Proposal/IDCODocs"));
                        docIdcoDoc.SaveAs(Server.MapPath("~/Proposal/IDCODocs/" + filepathidco));
                    }
                }
            }

            string Uploadname = filepathidco;

            objProposal.strAction = "I";
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objProposal.vchProposalno = hdnproposalno1.Value;
            objProposal.strIdcoDocs = Uploadname;
            objProposal.decExtendLand = Convert.ToDecimal(txtLandRecomendBySLFC.Text.Trim());

            string strRetVal = objService.ForwardLandToIDCO(objProposal);

            /*------------------------------------------------------------*/
            //// get respons from land data updated of proposal number for each request.            
            Util.LogRequestResponse("ForwardToIdco", "ForwardLandToIDCO", strRetVal);
            /*------------------------------------------------------------*/

            fillGridview();

            if (Convert.ToInt32(strRetVal) == 3)
            {
                IntegrationPEALWithIDCO();
                /*------------------------------------------------------------*/
                //// send request to IDCO service of proposal number for each request.            
                Util.LogRequestResponse("ForwardToIdco", "sendRequestGOSWIFTToIDCO", "Request send to IDCO service method");
                /*------------------------------------------------------------*/
                ClearField1();
            }
            else if (strRetVal == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('Action can not be taken Successfully;</script>');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForwardDepart");
        }
        finally
        {
            objProposal = null;
        }
    }
  

    public static string SerializeToXMLString<T>(T toSerialize)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
        StringWriter textWriter = new StringWriter();
        xmlSerializer.Serialize(textWriter, toSerialize);
        return textWriter.ToString();
    }
   

    [WebMethod]
    public static List<ListItem> FillIndustry(string id)
    {
        List<ListItem> branches = new List<ListItem>();
        try
        {
            string query = "select intIndustrialEstateId,vchIndustrialName from M_INDUSTRIAL_ESTATE where intDirstrictId=" + id + "";
            string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            branches.Add(new ListItem
                            {
                                Value = sdr["intIndustrialEstateId"].ToString(),
                                Text = sdr["vchIndustrialName"].ToString()
                            });
                        }
                    }
                    con.Close();

                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForwardDepart");
        }
        return branches;
    }
}