using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using Ionic.Zip;
using EntityLayer.Service;
using Common;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using BusinessLogicLayer.HelpDesk;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class Portal_HelpDesk_ViewIssueRegistration : SessionCheck
{
    #region "Global Variable"
    /// <summary>
    /// Pradeep kumar sahoo
    /// All global variable declared here
    /// </summary>
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    string ApplicationPath = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"];
    DataTable dt = new DataTable();
    HelpDeskBusinessLayer objlayer = new HelpDeskBusinessLayer();
    IssueRegistration objswp = new IssueRegistration();
    ServiceDetails objService1 = new ServiceDetails();
    CommonHelperCls comm = new CommonHelperCls();
    DepartmentSMSClass objDepartmntSms = new DepartmentSMSClass();
    string FormHeader = "";
    string FormFooter = "";
    int intAllignment = 0;
    int intRetVal = 0;
    int intRetVal1 = 0;
    string strUnqId = "";
    string strRslvedEmailMsg = "Dear User,<br/><br/> Greetings from GOSWIFT! <br/>  We are happy to assist you. We would like to inform you that as your Ticket No.<strong> [ISSUENO]</strong> with issue <strong>'[Description]'</strong> has been resolved. <br/>So, we are closing your request Ticket No. [ISSUENO] from our end.<br/>  If you have any queries, please reach out to us.";
    string strDiscrdEmailMsg = "Dear User,<br/><br/> Greetings from GOSWIFT! <br/><br/>  We are happy to assist you. We would like to inform you that as your issue has discarded due to [remarks]. So, we are closing your request Ticket No. [ISSUENO] from our end.<br>  If you have any queries, please reach out to us.";
    string strRslvedSMSMsg = "Dear User,We are happy to assist you. We would like to inform you that as your issue has resolved. So, we are closing your request Ticket No:[ISSUENO] from our end.";
    string strDiscrdSMSMsg = "Dear User,  We are happy to assist you. We would like to inform you that as your issue has resolved. So, we are closing your request Ticket No:[ISSUENO] from our end.";
    string strSubjectMsg = "Ticket No#-[ISSUENO] | [CATEGORY] | GOSWIFT-HelpDesk | [STATUS]";

    string strRslvedEmailMsgATAEsc = "Dear User,<br/><br/> Greetings from GOSWIFT! <br/>  We would like to inform you Ticket No.<strong> [ISSUENO] </strong> raised by [Investor] with issue <strong>'[Description]'</strong> has been resolved. <br/>So, we are closing Ticket No. <strong>[ISSUENO] </strong> from our end.";
    string strRslvedSMSMsgATAEsc = "Dear User, We would like to inform you that  request Ticket No. [ISSUENO] raised by [Investor] has been resolved. So, we are closing ticket from our end.";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindDept();
            //AutoselectDept();
            //fillGridview();
            DropdwonBind(ddlStatus, 10);
            BindGridDetails();
            //txtFromdate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            //txtTodate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
      
            
        }
    }



    private void BindGridDetails()
    {
        try
        {
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objlist = new List<IssueRegistration>();
            objswp.Action = "V";
            objswp.status = Convert.ToInt32(ddlStatus.SelectedValue);
            objswp.vch_Type = ddltype.SelectedValue;
            objswp.vchIssueNo = txtIssue.Text;
            objswp.vchFromDate = txtFromdate.Text;
            objswp.vchToDate = txtTodate.Text;
            objswp.int_CategoryId =Convert.ToInt32(ddlCategory.SelectedValue);
            objswp.int_SubcategoryId = Convert.ToInt32(ddlSubcategory.SelectedValue);
            objlist = objlayer.ViewIssueRegistration(objswp);
            gvService.DataSource = objlist;
            gvService.DataBind();
            txtFromdate.Text = txtFromdate.Text;
            txtTodate.Text = txtTodate.Text;
            intRetVal = objlist.Count();
            DisplayPaging();
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }

    }
   

    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkbtn = e.Row.FindControl("lnkbtn") as LinkButton;
                Label lblmob3 = e.Row.FindControl("lblmob3") as Label;
                HiddenField hdnStatus = e.Row.FindControl("hdnStatus") as HiddenField;
                GridView gvIntimateSent=e.Row.FindControl("gvIntimateSent") as GridView;
                Label lblmobMM344 = e.Row.FindControl("lblmobMM344") as Label;
                if (lblmobMM344.Text == "01-01-1900 00:00:00")
                {
                    lblmobMM344.Text = "";
                }
                if (lblmob3.Text == "1")
                {
                    lblmob3.Text = "Pending";
                    lnkbtn.Visible = true;
                }
                if (lblmob3.Text == "2")
                {
                    lblmob3.Text = "Resolved";
                    lnkbtn.Visible = true;
                }
                if (lblmob3.Text == "3")
                {
                    lblmob3.Text = "Inprogress";
                    lnkbtn.Visible = true;
                }
                if (lblmob3.Text == "4")
                {
                    lblmob3.Text = "Discard";
                    lnkbtn.Visible = true;
                }
                if (lblmob3.Text == "5")
                {
                    lblmob3.Text = "Reopen";
                    lnkbtn.Visible = true;
                }
                DropDownList drpStatus = e.Row.FindControl("drpStatus") as DropDownList;
                DropdwonBind(drpStatus, Convert.ToInt32(hdnStatus.Value));

                GridView gvSummary = (GridView)e.Row.FindControl("grdView");
                objlayer = new HelpDeskBusinessLayer();
                List<IssueRegistration> objProjList = new List<IssueRegistration>();
                IssueRegistration objProp = new IssueRegistration();

                objProp.Action = "V2";
                objProjList = objlayer.ViewFile(objProp).ToList();
                if (objProjList != null && objProjList.Count > 0)
                    gvSummary.DataSource = objProjList; ;
                gvSummary.DataBind();
                intRetVal1 = objProjList.Count;

                //LinkButton btn = new LinkButton();
                //btn = (LinkButton)e.Row.FindControl("LinkButton1");
                HiddenField hdnEmail = (HiddenField)e.Row.FindControl("hdnEmail");
                ViewState["Email"] = hdnEmail.Value.ToString();
                //HyperLink hyRcvd = (HyperLink)e.Row.FindControl("hlnkTkn");
                //hyRcvd.NavigateUrl = "ServiceDetailsView.aspx?vchIssueNo=" + gvService.DataKeys[e.Row.RowIndex].Values[1];

                //HyperLink hplnkCertificate = (HyperLink)e.Row.FindControl("hplnkCertificate");
                //if (gvService.DataKeys[e.Row.RowIndex].Values[2].ToString() != "" && gvService.DataKeys[e.Row.RowIndex].Values[2].ToString() != null && gvService.DataKeys[e.Row.RowIndex].Values[2].ToString()!="NA")
                //{
                //    hplnkCertificate.NavigateUrl = "../ApprovalDocs/" + gvService.DataKeys[e.Row.RowIndex].Values[2].ToString();
                //    hplnkCertificate.Target = "_blank";
                //}
                //else
                //{
                //    hplnkCertificate.Text = "-NA-";
                //    hplnkCertificate.NavigateUrl = "#";
                //}
                HyperLink hplnk = (HyperLink)e.Row.FindControl("hplnk");
                //if (gvService.DataKeys[e.Row.RowIndex].Values[2].ToString() != "" && gvService.DataKeys[e.Row.RowIndex].Values[2].ToString() != "NA")
                //{
                //    hplnk.NavigateUrl = "../ApprovalDocs/" + gvService.DataKeys[e.Row.RowIndex].Values[2].ToString();
                //    hplnk.Target = "_blank";
                //}
                //else
                //{
                //    hplnk.Text = "-NA-";
                //    hplnk.NavigateUrl = "#";

                //}
                objProp.int_IssueId = Convert.ToInt32(gvService.DataKeys[e.Row.RowIndex].Values[0].ToString());
                objProp.Action = "IN";
                gvIntimateSent.DataSource = objlayer.ViewIssueintimation(objProp);
                gvIntimateSent.DataBind();
                //if (gvService.DataKeys[e.Row.RowIndex].Values[2].ToString() == "")
                //{
                //    hplnk.Visible = false;
                //}
                //else
                //{
                //    hplnk.Visible = true;
                //}

            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        BindGridDetails();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        BindGridDetails();
    }
    #region "Gridview Row created"
    /// <summary>
    /// Added By Subhasmita Behera on 31-Jul-2017 for grid view row created of approval status
    /// Code approval status row created
    /// </summary>
    /// 
    protected void gvService_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {

                GridView HeaderGrid = (GridView)sender;

                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell = new TableCell();

                HeaderCell.Font.Bold = true;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Style["background-color"] = "#F5F5F5";


                HeaderCell = new TableCell();

                HeaderCell.ForeColor = System.Drawing.Color.Black;
                HeaderCell.Font.Bold = true;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.Style["background-color"] = "#F5F5F5";

                HeaderCell = new TableCell();

                HeaderCell.Font.Bold = true;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;

                HeaderCell.Style["background-color"] = "#F5F5F5";


                gvService.Controls[0].Controls.AddAt(0, HeaderGridRow);
            
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }

    #endregion
    #region "Approval add"
    /// <summary>
    /// Added By Subhasmita Behera on 01-Aug-2017 for add approval details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            List<IssueRegistration> rtnList = new List<IssueRegistration>();
            List<IssueRegistration> rtnList1 = new List<IssueRegistration>();
            System.Net.Mail.Attachment data=null;
            string Uploadname = "";
            string Retval = "";
            bool smsStatus1 = true;
            bool MailStatus1 = true;
            Button btnSubmit = (Button)sender;
            GridViewRow grdrw1 = (GridViewRow)btnSubmit.Parent.Parent;
            DropDownList drpStatus = (DropDownList)grdrw1.FindControl("drpStatus");
            TextBox txtRemark = (TextBox)grdrw1.FindControl("txtRemark");
            HiddenField hdnEmail = (HiddenField)grdrw1.FindControl("hdnEmail");
            HiddenField hdnIssueNo = (HiddenField)grdrw1.FindControl("hdnIssueNo");
            HiddenField hdnMobile=(HiddenField)grdrw1.FindControl("hdnMobile");
            HiddenField hdnCategory = (HiddenField)grdrw1.FindControl("hdnCategory");
            HiddenField hdnUsername = (HiddenField)grdrw1.FindControl("hdnUsername");
            HiddenField hdnSubCategory = (HiddenField)grdrw1.FindControl("hdnSubCategoryId");
            HiddenField hdnIssuedetis = (HiddenField)grdrw1.FindControl("hdnIssuedetis");
            string email = hdnEmail.Value;

            HiddenField hdnIssueid = (HiddenField)grdrw1.FindControl("hdnIssueid");
            GridView gvSummary = (GridView)grdrw1.FindControl("grdView");
            //bool sts = false;
            string ss = "";

            //string Email="";
            if (gvSummary.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in gvSummary.Rows)
                {
                    CheckBox chk = (CheckBox)gvr.FindControl("CheckBox1");
                    //HiddenField hdnEmail = (HiddenField)gvr.FindControl("hdnEmail");
                    if (chk.Checked)
                    {
                        HiddenField hdn = (HiddenField)gvr.FindControl("hdnId");
                        HiddenField hdnServiceID = (HiddenField)gvr.FindControl("hdnServiceID");
                        HiddenField hdnUserManual = (HiddenField)gvr.FindControl("hdnUserManual");
                        ss += hdnServiceID.Value + ",";
                        string path = Server.MapPath(hdnUserManual.Value);

                        string strSubject = "User Manual Report  (" + DateTime.Now.ToString("dd-MMM-yyyy") + "),SWP";
                        string strBody = "Please find the attachment";
                        try
                        {
                            if (File.Exists(path))
                                data = new System.Net.Mail.Attachment(path);
                            //string toEmail = string.Empty;
                            string[] toEmail = new string[1];

                            toEmail[0] = hdnEmail.Value;
                            SendEmail(strSubject, strBody, data, toEmail, true);
                        }
                        catch (Exception)
                        { 
                        }

                    }
                }
            }


            FileUpload docUpload = (FileUpload)btnSubmit.FindControl("docUpload");
            string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + docUpload.FileName, DateTime.Now);
            if (docUpload.HasFile)
            {

                if (Path.GetExtension(docUpload.FileName) != ".pdf")
                {
                    string strmsg11 = "alert('Only .pdf file accepted!');";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Click", strmsg11, true);
                    return;
                }
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("../ApprovalDocs"));
            if (!string.IsNullOrEmpty(docUpload.FileName))
            {
                if (dir.Exists)
                {
                    docUpload.SaveAs(Server.MapPath("../ApprovalDocs/" + filepath));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("../ApprovalDocs"));
                    docUpload.SaveAs(Server.MapPath("../ApprovalDocs/" + filepath));

                }
                Uploadname = filepath;
            }
            else { Uploadname = ""; }


            HelpDeskBusinessLayer objlayer = new HelpDeskBusinessLayer();
            objswp.Action = "V1";
            objswp.status = Convert.ToInt16(drpStatus.SelectedValue);
            objswp.int_IssueId = Convert.ToInt16(hdnIssueid.Value);
            objswp.strRemark = txtRemark.Text;

            objswp.vch_fileUploadpopup = Uploadname;
            if (ss.Length != 0)
                objswp.ServiceName = ss.ToString().Remove(ss.Length - 1);
            else
                objswp.ServiceName = "";

            rtnList = objlayer.AddIssueRegister(objswp);
            objswp.Action = "V3";
            objswp.int_SubcategoryId =Convert.ToInt32(hdnSubCategory.Value);
            objswp.int_IssueId = Convert.ToInt16(hdnIssueid.Value);
            rtnList1 = objlayer.ViewEscalationEmailRegistration(objswp);
            if (rtnList.Count > 0)
            {
                string smsMsg = "";
                string EmailMsg = "";

                string smsMsgATA = "";
                string EmailMsgATA = "";

                if (drpStatus.SelectedValue == "2")
                {
                    strSubjectMsg = strSubjectMsg.Replace("[ISSUENO]", hdnIssueNo.Value).Replace("[STATUS]", "Resolved").Replace("[CATEGORY]", hdnCategory.Value);

                    smsMsg = strRslvedSMSMsg.Replace("[ISSUENO]", hdnIssueNo.Value);
                    //smsMsg = strRslvedSMSMsg.Replace("[Description]", hdnIssuedetis.Value);
                    EmailMsg = strRslvedEmailMsg.Replace("[ISSUENO]", hdnIssueNo.Value).Replace("[Description]", hdnIssuedetis.Value);

                    smsMsgATA = strRslvedSMSMsgATAEsc.Replace("[ISSUENO]", hdnIssueNo.Value).Replace("[Investor]", hdnUsername.Value);
                    EmailMsgATA = strRslvedEmailMsgATAEsc.Replace("[ISSUENO]", hdnIssueNo.Value).Replace("[Description]", hdnIssuedetis.Value).Replace("[Investor]", hdnUsername.Value);

                    //smsMsgATA = strRslvedSMSMsgATAEsc.Replace("[Investor]", hdnUsername.Value);
                    //EmailMsgATA = strRslvedEmailMsgATAEsc.Replace("[Investor]", hdnUsername.Value);

                    string[] tomail = new string[1];
                    tomail[0] = email;
                    MailStatus1 = comm.sendMail(strSubjectMsg, EmailMsg, tomail, true);//Repoting authority
                    smsStatus1 = comm.SendSmsNew(hdnMobile.Value, smsMsg);



                    string[] RAtoEmail = new string[1];
                    if (rtnList1.Count > 0)
                    {
                        for (int j = 0; j < rtnList1.Count; j++)
                        {
                            if (rtnList1[j].Email.Contains(','))
                            {
                                string[] RAEmailArry = rtnList1[j].Email.Split(',');
                              
                                string str1;
                                for (int i = 0; RAEmailArry.Length > i; i++)
                                {
                                    if (RAEmailArry[i] != "" && RAEmailArry[i] != null)
                                    {
                                        RAtoEmail[0] = RAEmailArry[i].ToString();

                                        //smsMsgATA = strRslvedSMSMsgATAEsc.Replace("[ISSUENO]", hdnIssueNo.Value);
                                        //EmailMsgATA = strRslvedEmailMsgATAEsc.Replace("[ISSUENO]", hdnIssueNo.Value);

                                        //smsMsgATA = strRslvedSMSMsgATAEsc.Replace("[Investor]", hdnUsername.Value);
                                        //EmailMsgATA = strRslvedEmailMsgATAEsc.Replace("[Investor]", hdnUsername.Value);
                                        MailStatus1 = comm.sendMail(strSubjectMsg, EmailMsgATA, RAtoEmail, true);
                                        string str = comm.UpdateMailSMSStaus("Issue Action Taken", hdnMobile.Value, email, "Issue Resolved", "1", "1", 1, "1", EmailMsgATA, smsMsgATA, smsStatus1, MailStatus1);
                                    }
                                    if (rtnList1[j].VchMobile.Contains(','))
                                    {
                                        string[] RAPhoneArry = rtnList1[j].VchMobile.Split(',');
                                        if (RAPhoneArry[i] != "" && RAPhoneArry[i] != null)
                                        {
                                            smsStatus1 = comm.SendSmsNew(RAPhoneArry[i].ToString(), smsMsgATA);
                                        }
                                    }
                                    else
                                    {
                                        smsStatus1 = comm.SendSmsNew(rtnList1[j].VchMobile.ToString(), smsMsgATA);
                                    }
                                    i = i++;

                                }

                            }
                            else
                            {
                                RAtoEmail[0] = rtnList1[j].Email.ToString();
                                MailStatus1 = comm.sendMail(strSubjectMsg, EmailMsgATA, RAtoEmail, true);
                                smsStatus1 = comm.SendSmsNew(rtnList1[j].VchMobile.ToString(), smsMsgATA);
                                string str = comm.UpdateMailSMSStaus("Issue Action Taken", hdnMobile.Value, email, "Issue Resolved", "1", "1", 1, "1", EmailMsgATA, smsMsgATA, smsStatus1, MailStatus1);
                            }
                        }
                    }

                   
                }
                else if (drpStatus.SelectedValue == "4")
                {
                    strSubjectMsg = strSubjectMsg.Replace("[ISSUENO]", hdnIssueNo.Value).Replace("[STATUS]", "Discard").Replace("[CATEGORY]", hdnCategory.Value);
                    EmailMsg = strDiscrdEmailMsg.Replace("[ISSUENO]", hdnIssueNo.Value).Replace("[remarks]", txtRemark.Text);
                    smsMsg = strDiscrdSMSMsg.Replace("[ISSUENO]", hdnIssueNo.Value);
                   // smsMsg = strDiscrdEmailMsg.Replace("[remarks]", txtRemark.Text);
                }
                if (drpStatus.SelectedValue == "4")
                {
                    string[] tomail = new string[1];
                    tomail[0] = email;
                    //MailStatus1 = comm.sendMail(strSubjectMsg, EmailMsg, tomail, true);//Repoting authority
                    //smsStatus1 = comm.SendSmsNew(hdnMobile.Value, smsMsg);

                    MailStatus1 = comm.sendMail(strSubjectMsg, EmailMsg, tomail, true);//Repoting authority
                    smsStatus1 = comm.SendSmsNew(rtnList[0].VchMobile.ToString(), smsMsg);
                    string str = comm.UpdateMailSMSStaus("Issue Action Taken", hdnMobile.Value, email, "Issue Escalated", "1", "1", 1, "1", EmailMsg, smsMsg, smsStatus1, MailStatus1);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Saved Succesfully', '" + Messages.TitleOfProject + "', function () {location.href = ' ViewIssueRegistration.aspx';}); </script>", false);
            }

        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }


    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvService.PageIndex = 0;
            gvService.AllowPaging = false;
            BindGridDetails();
        }
        else
        {
            lbtnAll.Text = "All";
            gvService.AllowPaging = true;
            BindGridDetails();
        }
    }
    private void DisplayPaging()
    {
        if (gvService.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvService.PageIndex + 1 == gvService.PageCount)
            {
                lblPaging.Text = "Results <b>" + ((Label)gvService.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)gvService.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)gvService.Rows[0].FindControl("lblsl")).Text) + gvService.PageSize - 1) + "</b> of <b>" + intRetVal + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    protected void btnShare_Click(object sender, EventArgs e)
    {
        Button btnShare = (Button)sender;
        GridView gvSummary = (GridView)btnShare.FindControl("grdView");
        gvSummary.Visible = true;
    }
    public bool SendEmail(string strSubject, string strBody, Attachment attchfile, string[] toEmail, bool enbleSSl)
    {
        bool Res = false;
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smptp"].ToString());
            mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"].ToString());
            for (int i = 0; i < toEmail.Length; i++)
            {
                mail.To.Add(toEmail[i]);
            }
            mail.Subject = strSubject;
            mail.Body = strBody;
            if (attchfile != null)
            {
                mail.Attachments.Add(attchfile);
            }
            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smptpPort"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromEmailPassword"].ToString());
            SmtpServer.EnableSsl = enbleSSl;
            mail.IsBodyHtml = true;

            //Warning: do not use this in production code!
            ServicePointManager.ServerCertificateValidationCallback =
    delegate(object s, X509Certificate certificate,
             X509Chain chain, SslPolicyErrors sslPolicyErrors)
    { return true; };

            //These are need tobe comment in PROD server
            // END 
            SmtpServer.Send(mail);
            Res = true;
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
        //catch
        //{
        //    Res = false;
        //}
        return Res;
    }

    #endregion
    private void FillComplaintLog(int intComplaintID, GridView gvControl)
    {
        try
        {
            int received = 0;
            int pending = 0;
            int resolved = 0;
            int discarded = 0;
            int forwarded = 0;
            int reopen = 0;
            int escalate = 0;

            received = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Open New"].ToString());
            pending = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Pending"].ToString());
            resolved = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Resolved"].ToString());
            discarded = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Reject"].ToString());
            forwarded = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Forward"].ToString());
            reopen = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Reopen"].ToString());
            escalate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Escalate"].ToString());

            //gvControl.DataSource = objService.ViewComplaintLog(intComplaintID, received, pending, resolved, discarded, escalate, forwarded, reopen);
            //gvControl.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
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
                    string FileName = "../../QueryFiles/Services/" + Convert.ToString(arrFileName[i]);
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
    public void DropdwonBind(DropDownList ddl1,int intStatusId)
    {
        ddl1.Items.Clear();
        if (intStatusId == 2)
        {
            
            ddl1.Items.Insert(0, new ListItem("Select", "0"));
            ddl1.Items.Insert(1, new ListItem("Reopen", "5"));
        }
        else if (intStatusId == 4)
        {
            ddl1.Items.Insert(0, new ListItem("Select", "0"));
            ddl1.Items.Insert(1, new ListItem("Reopen", "5"));
        }
        else if (intStatusId == 10)
        {
            ddl1.Items.Insert(0, new ListItem("Select", "0"));
            ddl1.Items.Insert(1, new ListItem("Resolved", "2"));
            ddl1.Items.Insert(2, new ListItem("Inprogress", "3"));
            ddl1.Items.Insert(3, new ListItem("Discard", "4"));
            ddl1.Items.Insert(4, new ListItem("Reopen", "5"));
            ddl1.Items.Insert(5, new ListItem("Pending", "6"));  
      
        }
        else
        {
            ddl1.Items.Insert(0, new ListItem("Select", "0"));
            ddl1.Items.Insert(1, new ListItem("Resolved", "2"));
            ddl1.Items.Insert(2, new ListItem("Inprogress", "3"));
            ddl1.Items.Insert(3, new ListItem("Discard", "4"));
            ddl1.Items.Insert(4, new ListItem("Reopen", "5"));
            ddl1.Items.Insert(5, new ListItem("Pending", "6")); 

        }
      
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCategory(Convert.ToInt32(ddltype.SelectedValue));
    }

    public void BindCategory(int intTypeId)
    {
        objswp = new IssueRegistration();
        objlayer = new HelpDeskBusinessLayer();
        List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
        try
        {
            objswp.Action = "C";
            objswp.intTypeId = intTypeId;
            objHelpdesk = objlayer.BindCategory(objswp).ToList();
            ddlCategory.DataTextField = "vch_CategoryName";
            ddlCategory.DataValueField = "int_CategoryId";
            ddlCategory.DataSource = objHelpdesk;
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubCategory();
    }
    public void BindSubCategory()
    {

        objswp = new IssueRegistration();
        objlayer = new HelpDeskBusinessLayer();
        List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();

        try
        {
            objswp.Action = "S";
            objswp.int_CategoryId = Convert.ToInt32(ddlCategory.SelectedValue.ToString());
            objHelpdesk = objlayer.BindSubCategory(objswp).ToList();
            ddlSubcategory.DataTextField = "vch_SubCategoryName";
            ddlSubcategory.DataValueField = "int_SubcategoryId";
            ddlSubcategory.DataSource = objHelpdesk;
            ddlSubcategory.DataBind();
            ddlSubcategory.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
}
