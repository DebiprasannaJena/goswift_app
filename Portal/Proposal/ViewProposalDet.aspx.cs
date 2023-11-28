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
using System.Configuration;

public partial class Mastermodule_ViewProposalDet : SessionCheck
{
    ProposalBAL objService = new ProposalBAL();
    AgendaDet objAgenda = new AgendaDet();
    AgendaBAL objAgendaService = new AgendaBAL();

    ServiceDetails objServiceEntity = new ServiceDetails();
    DepartmentSMSClass objDepartmntSms = new DepartmentSMSClass();
    ProposalDet objProposal = new ProposalDet();
    PromoterDet objprom = new PromoterDet();

    string strRetval = "";
    string filepath = "";
    string filepathidco = "";

    bool smsStatus;
    bool mailStatus;
    bool MailStatus = true;
    string PEALCertificatefilepath = "";
    string ScoreCardPath = "";
    int intRecordCount = 0; //// Added by Sushant Jena On Dt:-19-Feb-2020
    int intRetVal = 0;
    bool IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                fillGridview();
                BindStatus();
                //(START) this section of code checks if the page postback is due to genuine submit by user or by pressing "refresh"
                ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
                Session["SessionId"] = ViewState["ViewStateId"].ToString();
                //(END) this section of code checks if the page postback is due to genuine submit by user or by pressing "refresh"
                // dvPEALCerti.Visible = false;
                BindStatus1();
              
                //dvscore.Visible = false;
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
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForward");
        }
    }

    #region FunctionUsed

    //private void BindNodalOffcr()
    //{
    //    try
    //    {
    //        CICGService.CICGService AddAgenda = new CICGService.CICGService();
    //        drpNodalOffcr.DataSource = AddAgenda.FillNodalOfficer();
    //        drpNodalOffcr.DataTextField = "Name";
    //        drpNodalOffcr.DataValueField = "Id";
    //        drpNodalOffcr.DataBind();
    //        ListItem list = new ListItem();
    //        list.Text = "--Select--";
    //        list.Value = "0";
    //        drpNodalOffcr.Items.Insert(0, list);
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "ProposalForward");
    //    }
    //}
    private void BindStatus1()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "SM";
            objProp.vchProposalNo = " ";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

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
            Util.LogError(ex, "ProposalForward");
        }
    }
    private void BindStatus()
    {
        try
        {
            List<ProposalDet> objProjList = new List<ProposalDet>();
            ProposalDet objProp = new ProposalDet();

            objProp.strAction = "S";
            objProjList = objService.PopulateStatus(objProp).ToList();

            drpStatus.DataSource = objProjList;
            drpStatus.DataTextField = "strStatus";
            drpStatus.DataValueField = "intStatus";
            drpStatus.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpStatus.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForward");
        }
    }
    private void fillGridview()
    {
        try
        {
            objProposal.strAction = "M";
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objProposal.compName = txtCompanyName.Text.Trim();
            objProposal.intStsdet = Convert.ToInt32(drpStatusDet.SelectedValue);
            List<ProposalDet> objProposalList = objService.getProposalDetails(objProposal).ToList();

            gvService.DataSource = objProposalList;
            gvService.DataBind();

            intRecordCount = objProposalList.Count;
            intRetVal = gvService.Rows.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForward");
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
            LinkButton btn = new LinkButton();
            //btn = (LinkButton)e.Row.FindControl("LinkButton1");
            //if (gvService.DataKeys[e.Row.RowIndex].Values[0].ToString() == "1")
            //{
            HiddenField hdnTextVal1 = (HiddenField)e.Row.FindControl("hdnTextVal1");
            HiddenField hdnLandReq = (HiddenField)e.Row.FindControl("hdnLandReqIDCO");
            LinkButton lnkBtn = (e.Row.FindControl("LinkButton2") as LinkButton);
            LinkButton lnkBtnAMS = (e.Row.FindControl("LinkButtonAMS") as LinkButton);
            Label lblAMS = (e.Row.FindControl("lblAMS") as Label);
            Label lblIdco = (e.Row.FindControl("lblIdco") as Label);
            string strProposalNo = hdnTextVal1.Value;// gvService.DataKeys[e.Row.RowIndex].Values[2].ToString();
            if (e.Row.Cells[2].Text == "1")
            {
                e.Row.Cells[2].Text = "Large Industries";
                lnkBtnAMS.Visible = true;
                dvscore.Visible = false;
                hdnProjectType.Value = "1";

            }
            else if (e.Row.Cells[2].Text == "2")
            {
                e.Row.Cells[2].Text = "MSME";
                hdnProjectType.Value = "2";
                lnkBtnAMS.Visible = false;
                if (Convert.ToInt32(Session["Userid"]) == 166)
                {
                    dvscore.Visible = false;
                    hdnProjectType.Value = "1";
                }
                else if (Convert.ToInt32(Session["Userid"]) == 167)
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
                if (gvService.DataKeys[e.Row.RowIndex].Values[9].ToString() == "1")
                {
                    lblAMS.Visible = true;
                    lnkBtnAMS.Visible = false;
                }
                else
                {

                    lnkBtnAMS.Visible = true;
                    lblAMS.Visible = false;
                }
            }
            else
            {
                lnkBtnAMS.Visible = false;
                lblAMS.Visible = true;
                if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[11]) == "2")
                {
                    lblAMS.Text = "Approved";
                }
                if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[11]) == "3")
                {
                    lblAMS.Text = "Rejected";
                }
                if (e.Row.Cells[2].Text == "MSME")
                {

                    //lblAMS.Visible = false;
                    lblAMS.Text = "NA";
                    //e.Row.Cells[6].Visible = false;
                    gvService.Columns[6].Visible = false;
                }
                if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[5]) == "2")
                {
                    if (Convert.ToDecimal(hdnLandReq.Value) > 0)//Query Raised
                    {
                        if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[7].ToString()) == "0")
                        {
                            if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[10].ToString()) == "4")
                            {
                                lnkBtn.Visible = false;
                                lblIdco.Visible = true;
                                lblIdco.Text = Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[12].ToString());//"Approved";
                            }
                            else if (Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[10].ToString()) == "1")
                            {
                                lnkBtn.Visible = false;
                                lblIdco.Visible = true;
                                lblIdco.Text = Convert.ToString(gvService.DataKeys[e.Row.RowIndex].Values[12].ToString()); //"Forwarded";
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
            // }
            //else
            //{ btn.Visible = false; }


            HyperLink hprlnkproposal = (HyperLink)e.Row.FindControl("hypLink");
            hprlnkproposal.NavigateUrl = "../Proposal/ProposalDetails.aspx?Pno=" + hdnTextVal1.Value;
            //Added By Pranay Kumar on 11-Sept-2017 for Show/Hide of Raised Query Button
            int intQueryStatus = Convert.ToInt32(gvService.DataKeys[e.Row.RowIndex].Values[3]);
            LinkButton lbtnRaise = (e.Row.FindControl("lbtnRaise") as LinkButton);

            if (intQueryStatus == 1)//Query Raised
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

            List<ProposalDet> objProposalList = new List<ProposalDet>();
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "QD";
            objProp.strProposalNo = hdnTextVal1.Value;
            objProposalList = objService.getRaisedQueryDetails(objProp).ToList();
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
            //List<LandDet> objProjList1 = new List<LandDet>();
            //LandDet objProp1 = new LandDet();
            //objProp1.strAction = "R";
            //objProp1.vchProposalNo = hdnTextVal1.Value;
            //objProjList1 = objService.GETMobileNo(objProp1).ToList();
            //if (objProjList1.Count > 0)
            //{
            //    txtLandRequired.Text = Convert.ToString(objProjList1[0].LandAprvByIPICOL);
            //}
        }
            
      }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForward");
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
            if (docUpload.HasFile)
            {
                if (Path.GetExtension(docUpload.FileName) != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
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

            //added by nibedita behera for PEAL certificate file upload in Take action on 05-09-2017

            if (docPEALCerti.HasFile)
            {
                if (Path.GetExtension(docPEALCerti.FileName) != ".pdf")
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


            if (docScoreCard.HasFile)
            {
                if (Path.GetExtension(docScoreCard.FileName) != ".pdf")
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

            objProposal.strAction = "A";
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objProposal.vchProposalno = hdnproposalno.Value;
            objProposal.intStatus = Convert.ToInt32(drpStatus.SelectedValue);
            objProposal.strFileName = Uploadname;
            objProposal.strPEALCertificate = UploadPEALCertificatefile;
            objProposal.strScorecard = ScoreCardPath;
            objProposal.strRemarks = txtRemarks.Text;
            objProposal.decExtendLand = 0;

            string strRetVal = objService.ProposalTakeAction(objProposal);

            fillGridview();

            if (strRetVal == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('Record Saved Successfully.');</script>", true);
                CommonHelperCls comm = new CommonHelperCls();
                List<LandDet> objProjList = new List<LandDet>();
                LandDet objProp = new LandDet();
                if (Convert.ToInt32(drpStatus.SelectedValue) == 2)
                {
                    objProp.strAction = "N";
                    objProp.vchProposalNo = hdnCreted.Value;
                    objProp.ApplicationNo = hdnproposalno.Value;
                    objProjList = objService.GETMobileNo(objProp).ToList();
                    string mobile = "";
                    string smsContent = "";
                    string[] toEmail = new string[1];
                    if (objProjList.Count > 0)
                    {
                        mobile = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[0]);
                        smsContent = Convert.ToString(objProjList[0].SMSContent);
                        toEmail[0] = Convert.ToString(objProjList[0].Email);
                        string strSubject = "SWP: Application Approved";
                        string strBody = "Department has approved the application for New Proposal of M/s " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[1]) + " ,Please Log into https://invest.odisha.gov.in for further details. ";
                        //comm.sendMail(strSubject, strBody, toEmail, true);
                        //comm.SendSms(mobile, strBody);

                        mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                        smsStatus = comm.SendSmsNew(mobile, strBody);
                        // FOR SMS and Mail Status Update

                        string str = comm.UpdateMailSMSStaus("PEAL", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), "1053", Convert.ToInt32(drpStatus.SelectedValue), hdnproposalno.Value, strBody, strBody, mailStatus, smsStatus);
                        // FOR SMS and Mail Status Update
                    }
                }
                else if (Convert.ToInt32(drpStatus.SelectedValue) == 3)
                {
                    objProp.strAction = "O";
                    objProp.vchProposalNo = hdnCreted.Value;
                    objProp.ApplicationNo = hdnproposalno.Value;
                    objProjList = objService.GETMobileNo(objProp).ToList();
                    string mobile = "";
                    string smsContent = "";
                    string[] toEmail = new string[1];
                    if (objProjList.Count > 0)
                    {
                        mobile = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[0]);
                        smsContent = Convert.ToString(objProjList[0].SMSContent);
                        toEmail[0] = Convert.ToString(objProjList[0].Email);
                        string strSubject = "SWP: Application Reject";
                        string strBody = "Department has rejected the application for New Proposal of M/s " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[1]) + " ,Please Log into https://invest.odisha.gov.in for further details. ";
                        //comm.sendMail(strSubject, strBody, toEmail, true);
                        //comm.SendSms(mobile, strBody);


                        mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                        smsStatus = comm.SendSmsNew(mobile, strBody);
                        // FOR SMS and Mail Status Update

                        string str = comm.UpdateMailSMSStaus("PEAL", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), "1053", Convert.ToInt32(drpStatus.SelectedValue), hdnproposalno.Value, strBody, strBody, mailStatus, smsStatus);
                        // FOR SMS and Mail Status Update
                    }
                }
                else if (Convert.ToInt32(drpStatus.SelectedValue) == 7)
                {
                    objProp.strAction = "O";
                    objProp.vchProposalNo = hdnCreted.Value;
                    objProp.ApplicationNo = hdnproposalno.Value;
                    objProjList = objService.GETMobileNo(objProp).ToList();
                    string mobile = "";
                    string smsContent = "";
                    string[] toEmail = new string[1];
                    if (objProjList.Count > 0)
                    {
                        mobile = Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[0]);
                        smsContent = Convert.ToString(objProjList[0].SMSContent);
                        toEmail[0] = Convert.ToString(objProjList[0].Email);
                        string strSubject = "SWP: Application Deferred";
                        string strBody = "Department has deferred the application for New Proposal of M/s " + Convert.ToString(objProjList[0].MobileNo.ToString().Split('_')[1]) + " ,Please Log into https://invest.odisha.gov.in for further details. ";
                        //comm.sendMail(strSubject, strBody, toEmail, true);
                        //comm.SendSms(mobile, strBody);


                        mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
                        smsStatus = comm.SendSmsNew(mobile, strBody);
                        // FOR SMS and Mail Status Update

                        string str = comm.UpdateMailSMSStaus("PEAL", mobile, toEmail[0].ToString(), strSubject, Session["UserId"].ToString(), "1053", Convert.ToInt32(drpStatus.SelectedValue), hdnproposalno.Value, strBody, strBody, mailStatus, smsStatus);
                        // FOR SMS and Mail Status Update
                    }
                }
                ClearField();
            }
            else if (strRetVal == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('Action can not be taken Successfully;</script>');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForward");
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
    private void ClearField2()
    {
        drpNodalOffcr.SelectedValue = "0";
    }

    private void IntegrationPEALWithIDCO()
    {
        CNET objCnet = new CNET();
        ProposalBAL objService = new ProposalBAL();
        string inputJson = "";
        string serviceUrl = "";
        try
        {
            ///// Get proposal details to be sent.
            objCnet.vchProposalNo = hdnproposalno1.Value;
            /*------------------------------------------------------------*/
            //// Write the input proposal number for each request.            
            Util.LogRequestResponse("ForwardToIdco", "ProposalNumber", hdnproposalno1.Value);
            /*------------------------------------------------------------*/

            List<CNET> objProposalList1 = objService.GetCNETCompanyDetails(objCnet).ToList();


            /////Convert the proposal details to the JSON string.
            inputJson = (new JavaScriptSerializer()).Serialize(objProposalList1);
            inputJson = inputJson.TrimStart('[').TrimEnd(']');

            /*------------------------------------------------------------*/
            //// Write the input json string for each request.            
            Util.LogRequestResponse("ForwardToIdco", "PEALJSONDATA", inputJson);
            /*------------------------------------------------------------*/

            serviceUrl = ConfigurationManager.AppSettings["CNETForwardIdco"]; //"http://erp.idco.in/rest/sendDataFromSWPtoERP";

            /*------------------------------------------------------------*/
            //// Write the input URL for each request.            
            Util.LogRequestResponse("ForwardToIdco", "IDCOURL", serviceUrl);
            /*------------------------------------------------------------*/



            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            Util.LogRequestResponse("ForwardToIdco", "BEFOREPOST","");
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";
            //httpRequest.Timeout = 15000;
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

                    if (strResult.IndexOf("failure_message") != -1)   //// If application gets failure then store the failure message in Log table (T_PEAL_CNET_GETDETAILS) and also send the failure message through email.
                    {
                        /*----------------------------------------------------------------------------*/
                        ///// Store the failure message received from IDCO.
                        /*----------------------------------------------------------------------------*/
                        objprom.vch_unique_application_id_from_swp = hdnproposalno1.Value;
                        objprom.vch_Error_Msg = JObject.Parse(strResult)["failure_message"].ToString();
                        objprom.vch_validation_Msg = JObject.Parse(strResult)["validation_message"].ToString();
                        objprom.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                        objprom.vch_Input_String = inputJson;
                        objprom.intIdcoReturnStatus = Convert.ToInt32(JObject.Parse(strResult)["status"].ToString());
                        string strRetVal = objService.ProposalCNETData(objprom);

                        /*----------------------------------------------------------------------------*/
                        //// Send email to intended users.
                        //// Also store the email content in mail log table(t_ortps_tbl)
                        /*----------------------------------------------------------------------------*/

                        /////Get email Ids to whom email to be sent.
                        ProposalBAL objService2 = new ProposalBAL();
                        List<PromoterDet> objProposalList2 = objService2.GetIDCOEmailDetails().ToList();

                        /////Compoase Email
                        CommonHelperCls comm = new CommonHelperCls();
                        string[] UAtoEmail = new string[1];
                        string strSubjectMsg = "Issue Raised for Proposal No.(" + hdnproposalno1.Value + ") during Forward to IDCO (Failure Message Got From Idco)";
                        string strBody = JObject.Parse(strResult)["failure_message"].ToString() + " :: " + JObject.Parse(strResult)["validation_message"].ToString() + "<br/><br/><strong>JSON DATA</strong>::<br/><br/>" + inputJson;

                        string EmailIDs = "";
                        if (objProposalList2.Count > 0)
                        {
                            EmailIDs = Convert.ToString(objProposalList2[0].vchEmail.ToString());
                        }

                        string str1 = "";
                        if (EmailIDs.Contains(','))
                        {
                            string[] RAEmailArry = EmailIDs.Split(',');
                            for (int i = 0; RAEmailArry.Length > i; i++)
                            {
                                if (RAEmailArry[i] != "" && RAEmailArry[i] != null)
                                {
                                    /////Send Email and Store in Email Log
                                    UAtoEmail[0] = RAEmailArry[i].ToString();
                                    MailStatus = comm.sendMail(strSubjectMsg, strBody, UAtoEmail, true);
                                    str1 = comm.UpdateMailSMSStaus("Issue", "", RAEmailArry[i].ToString(), "Issue Raised", "1", "1", 1, "1", strBody, strBody, smsStatus, MailStatus);
                                }
                            }
                        }
                        else
                        {
                            /////Send Email and Store in Email Log
                            UAtoEmail[0] = EmailIDs;
                            MailStatus = comm.sendMail(strSubjectMsg, strBody, UAtoEmail, true);
                            str1 = comm.UpdateMailSMSStaus("Issue", "", EmailIDs, "Issue Raised", "1", "1", 1, "1", strBody, strBody, smsStatus, MailStatus);
                        }
                    }
                    //else
                    //{
                    //    objprom.vch_oas_cafno = JObject.Parse(strResult)["cafno"].ToString();
                    //    objprom.vch_unique_application_id_from_swp = JObject.Parse(strResult)["unique_application_id_from_swp"].ToString();
                    //    objprom.vch_industry_code = JObject.Parse(strResult)["industry_code"].ToString();
                    //    objprom.vch_success_message = JObject.Parse(strResult)["success_message"].ToString();
                    //    objprom.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                    //    objprom.vch_Input_String = inputJson;
                    //    string strRetVal = objService.ProposalCNETData(objprom);
                    //    objprom = new PromoterDet();
                    //    objprom.vchProposalNo = JObject.Parse(strResult)["unique_application_id_from_swp"].ToString();
                    //    objprom.intForwardIDCO = 1;
                    //    objprom.strAction = "N";
                    //    string retIDCOVal = objService.ProposalIDCOtatusUpdate(objprom);
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            CommonHelperCls comm = new CommonHelperCls();
            string[] UAtoEmail = new string[1];
            string strSubjectMsg = "Issue Raised for Proposal No.(" + hdnproposalno1.Value + ") during Forward to IDCO (Exception Occured)";
            string strBody = ex.Message.ToString() + "<br/><br/><strong>JSON DATA</strong>::<br/><br/>" + inputJson;

            ProposalBAL objService2 = new ProposalBAL();
            List<PromoterDet> objProposalList2 = objService2.GetIDCOEmailDetails().ToList();

            string EmailIDs = "";
            if (objProposalList2.Count > 0)
            {
                EmailIDs = Convert.ToString(objProposalList2[0].vchEmail.ToString());
            }

            string str1 = "";
            if (EmailIDs.Contains(','))
            {
                string[] RAEmailArry = EmailIDs.Split(',');
                for (int i = 0; RAEmailArry.Length > i; i++)
                {
                    if (RAEmailArry[i] != "" && RAEmailArry[i] != null)
                    {
                        /////Send Email and Store in Email Log
                        UAtoEmail[0] = RAEmailArry[i].ToString();
                        MailStatus = comm.sendMail(strSubjectMsg, strBody, UAtoEmail, true);
                        str1 = comm.UpdateMailSMSStaus("Issue", "", RAEmailArry[i].ToString(), "Issue Raised", "1", "1", 1, "1", strBody, strBody, smsStatus, MailStatus);
                    }
                }
            }
            else
            {
                /////Send Email and Store in Email Log
                UAtoEmail[0] = EmailIDs;
                MailStatus = comm.sendMail(strSubjectMsg, strBody, UAtoEmail, true);
                str1 = comm.UpdateMailSMSStaus("Issue", "", EmailIDs, "Issue Raised", "1", "1", 1, "1", strBody, strBody, smsStatus, MailStatus);
            }

            Util.LogError(ex, "ProposalForward");            
        }
        finally
        {

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
                string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + btnSubmit.CommandArgument.ToString() + "_Query1" + ".pdf", DateTime.Now);

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
                    List<ProposalDet> objProposalList = new List<ProposalDet>();
                    ProposalDet objProp = new ProposalDet();

                    objProp.strAction = "S";
                    objProp.strProposalNo = btnQuerySubmit.CommandArgument.ToString();
                    objProp.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                    objProposalList = objService.getRaisedQueryDetails(objProp).ToList();
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
                        //comm.sendMail(strSubject, strBody, toEmail, true);
                        //comm.SendSms(mobile, smsContent);


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
            Util.LogError(ex, "ProposalForward");
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
            objProposal.strAction = "K";
            objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objProposal.vchProposalno = hdnproposalno1.Value;
            objProposal.strIdcoDocs = Uploadname;
            objProposal.decExtendLand = Convert.ToDecimal(txtLandRecomendBySLFC.Text.Trim());
            string strRetVal = objService.ForwardLandToIDCO(objProposal);

            fillGridview();
            /*------------------------------------------------------------*/
            //// get respons from land data updated of proposal number for each request.            
            Util.LogRequestResponse("ForwardToIdco", "ForwardLandToIDCO", strRetVal);
            /*------------------------------------------------------------*/
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
            Util.LogError(ex, "ProposalForward");
        }
        finally
        {
            objProposal = null;
        }
    }
    protected void btnSubmitAMS_Click(object sender, EventArgs e)
    {
        try
        {
            objAgenda = new AgendaDet();
            string Proposal = "", ProjLoc = "", ProdCapcity = "", BrdOfDirectors = "", BIOfCompny = "", ProjCostDtls = "", RawMatSource = "", FinDetails = "", FinPerformance = "", UId = "";
            string doclist = "";

            objAgenda.vchProposalNo = hdnproposalnoAMS.Value;
            objAgenda.strAction = "K";
            objAgenda.NodId = Convert.ToInt32(drpNodalOffcr.SelectedValue);
            List<AgendaDet> objProposalList = objAgendaService.GetAgendaDet(objAgenda).ToList();

            Proposal = SerializeToXMLString(objProposalList);

            objAgenda.strAction = "B";
            List<AgendaDet> objProposalList1 = objAgendaService.GetAgendaDet(objAgenda).ToList();
            ProjLoc = SerializeToXMLString(objProposalList1);

            objAgenda.strAction = "J";
            List<AgendaDet> objProposalList2 = objAgendaService.GetAgendaDet(objAgenda).ToList();
            ProdCapcity = SerializeToXMLString(objProposalList2);

            objAgenda.strAction = "Q";
            List<AgendaDet> objProposalList5 = objAgendaService.GetAgendaDet(objAgenda).ToList();
            BrdOfDirectors = SerializeToXMLString(objProposalList5);

            objAgenda.strAction = "R";
            List<AgendaDet> objProposalList6 = objAgendaService.GetAgendaDet(objAgenda).ToList();
            BIOfCompny = SerializeToXMLString(objProposalList6);

            objAgenda.strAction = "N";
            List<AgendaDet> objProposalList7 = objAgendaService.GetAgendaDet(objAgenda).ToList();
            ProjCostDtls = SerializeToXMLString(objProposalList7);

            objAgenda.strAction = "O";
            List<AgendaDet> objProposalList8 = objAgendaService.GetAgendaDet(objAgenda).ToList();
            RawMatSource = SerializeToXMLString(objProposalList8);

            objAgenda.strAction = "W";
            List<AgendaDet> objProposalList9 = objAgendaService.GetAgendaDet(objAgenda).ToList();
            FinDetails = SerializeToXMLString(objProposalList9);

            objAgenda.strAction = "X";
            List<AgendaDet> objProposalList10 = objAgendaService.GetAgendaDet(objAgenda).ToList();
            FinPerformance = SerializeToXMLString(objProposalList10);

            objAgenda.strAction = "Z";
            List<AgendaDet> objProposalList11 = objAgendaService.GetAgendaDet(objAgenda).ToList();
            doclist = SerializeToXMLString(objProposalList11);

            UId = hdnproposalnoAMS.Value;
            CICGService.CICGService AddAgenda = new CICGService.CICGService();
            string retVal = AddAgenda.AddAgenda(Proposal, ProjLoc, ProdCapcity, BrdOfDirectors, BIOfCompny, ProjCostDtls, RawMatSource, FinDetails, FinPerformance, doclist, UId);

            if (retVal == "1")
            {
                objprom = new PromoterDet();
                objprom.intFowardAMS = 1;
                objprom.vchProposalNo = hdnproposalnoAMS.Value;
                objprom.strNodalOfcrName = drpNodalOffcr.SelectedItem.Text;
                objprom.intNodalOfcrID = Convert.ToInt32(drpNodalOffcr.SelectedValue);
                objprom.strAction = "M";
                string retAMSVal = objService.ProposalAMStatusUpdate(objprom);
            }

            ClearField2();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalForward");
        }
    }

    public static string SerializeToXMLString<T>(T toSerialize)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
        StringWriter textWriter = new StringWriter();
        xmlSerializer.Serialize(textWriter, toSerialize);
        return textWriter.ToString();
    }
}