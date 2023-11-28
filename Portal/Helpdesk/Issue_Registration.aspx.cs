using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Net;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Configuration;
using System.Web.Services;
using BusinessLogicLayer.Service;
using EntityLayer.Service;
using System.Globalization;
using System.Data.SqlClient;
using System.Web.UI;
using BusinessLogicLayer.HelpDesk;
using DataAcessLayer.HelpDeskDataLayer;
using BusinessLogicLayer.HelpDesk;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Web;

public partial class Portal_HelpDesk_Issue_Registration : SessionCheck
{
    bool status = true;
    bool smsStatus = true;
    bool smsStatus1 = true;
    bool MailStatus = true;
    bool MailStatus1 = true;
    bool MailStatus2 = true;
    bool smsStatus2 = true;
    string str_Retvalue = "";
    int retval = 0;
    string Uploadname = "";
    IssueRegistration objswp = new IssueRegistration();
    HelpDeskBusinessLayer objlayer = new HelpDeskBusinessLayer();
    CommonHelperCls comm = new CommonHelperCls();

    string strMsg = "Dear [Investor] <br/><br/> Thank you for writing to GOSWIFT Helpdesk.<br/> Kindly note the query reference number for this correspondence is: <strong> [ISSUENO] </strong>.<br/> We wish to inform you that we are looking into your queries raised regarding [CATEGORY] & shall respond to you soon.<br/> Please follow up on the ticket number <strong> [ISSUENO] </strong> for future queries on this request.<br/><br/>  <strong>Description:</strong> [Description]  <br/><br/>Assuring you of our best services at all times.";
    string strSMSRegdContent = "Dear [Investor],  Kindly note the query reference number for this correspondence is:  [ISSUENO].";
    string strEmailToATA = "Dear User,<br/><br/>  The Issue has been raised by [Investor] successfully.<br/>  Log into https://investodisha.gov.in and check the Ticket No: [ISSUENO] for further details.<br/><br/>  <strong>Description:</strong> [Description]";
    string strSubjectMsg = "Ticket No#-[ISSUENO] | [CATEGORY] | GOSWIFT-HelpDesk | Registered";
    string strSubjectMsgT = "[CATEGORY] | Registered";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindType();
            userdetails.Visible = false;
            useraddress.Visible = false;
            DvDept.Visible = false;
            divInvest.Visible = false;
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    public void IsDirectoryCreated(string prefix)
    {
        string apppath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        if (!Directory.Exists(apppath + "/File/" + prefix))
        {
            Directory.CreateDirectory(apppath + "/File/" + prefix);
        }
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
    public void BindDepartment()
    {
        try
        {
            objswp = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
            objswp.Action = "M";
            objHelpdesk = objlayer.BindDepartment(objswp).ToList();
            ddldepartment.DataTextField = "vch_deptName";
            ddldepartment.DataValueField = "int_DeptId";
            ddldepartment.DataSource = objHelpdesk;
            ddldepartment.DataBind();
            ddldepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    public void BindUser()
    {
        try
        {
            objswp = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
            objswp.Action = "US";
            objswp.int_DeptId = Convert.ToInt32(ddldepartment.SelectedValue);
            objHelpdesk = objlayer.BindUser(objswp).ToList();
            ddlUser.DataTextField = "vch_UserName";
            ddlUser.DataValueField = "int_UserId";
            ddlUser.DataSource = objHelpdesk;
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    public void BindInvestor()
    {
        try
        {
            objswp = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
            objswp.Action = "I";
            objswp.int_DeptId = Convert.ToInt32(ddldepartment.SelectedValue);
            objHelpdesk = objlayer.BindInvestor(objswp).ToList();
            ddlInvestor.DataTextField = "vch_investName";
            //ddlInvestor.DataValueField = "vchInveUserId";// change by anil
            ddlInvestor.DataValueField = "int_investId";
            ddlInvestor.DataSource = objHelpdesk;
            ddlInvestor.DataBind();
            ddlInvestor.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }

    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUser();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlType.SelectedValue == "1")
            {
                DvDept.Visible = true;
                useraddress.Visible = false;
                divInvest.Visible = false;
                userdetails.Visible = false;

            }
            else if (ddlType.SelectedValue == "2")
            {
                userdetails.Visible = false;
                DvDept.Visible = false;
                useraddress.Visible = false;
                divInvest.Visible = true;
            }
            else
            {
                userdetails.Visible = true;
                DvDept.Visible = false;
                useraddress.Visible = true;
                divInvest.Visible = false;
            }
            BindCategory(Convert.ToInt32(ddlType.SelectedValue));
            BindDepartment();
            BindInvestor();
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
    public void clear()
    {
        try
        {
            ddlPriority.SelectedValue = "0";
            ddlType.SelectedValue = "0";
            ddlCategory.SelectedValue = "0";
            ddlSubcategory.SelectedValue = "0";
            ddldepartment.SelectedValue = "0";
            ddlInvestor.SelectedValue = "0";
            ddlType.SelectedValue = "0";
            ddlUser.Items.Clear();
            txtIssue.Text = "";
            txtOName.Text = "";
            txtMobile.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            drpStatus.SelectedValue = "0";
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        try
        {
            string strInvestorName = "";
            string strText = "";
            List<IssueRegistration> rtnList = new List<IssueRegistration>();
            objswp = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            strMsg = strMsg.Replace("[Description]", txtIssue.Text.ToString());
            if (ddlType.SelectedValue.ToString() == "3")
            {
                strMsg = strMsg.Replace("[Investor]", txtOName.Text.ToString());

                strSMSRegdContent = strSMSRegdContent.Replace("[Investor]", txtOName.Text.ToString());
                strInvestorName = txtOName.Text.ToString();
            }
            else if (ddlType.SelectedValue.ToString() == "2")
            {
                strMsg = strMsg.Replace("[Investor]", ddlInvestor.SelectedItem.Text.ToString());

                strSMSRegdContent = strSMSRegdContent.Replace("[Investor]", ddlInvestor.SelectedItem.Text.ToString());
                strInvestorName = ddlInvestor.SelectedItem.Text.ToString();
            }
            else
            {
                strMsg = strMsg.Replace("[Investor]", ddldepartment.SelectedItem.Text.ToString());

                strSMSRegdContent = strSMSRegdContent.Replace("[Investor]", ddldepartment.SelectedItem.Text.ToString());
                strInvestorName = ddldepartment.SelectedItem.Text.ToString();
            }
            strMsg = strMsg.Replace("[CATEGORY]", ddlCategory.SelectedItem.Text.ToString());
            strSubjectMsg = strSubjectMsg.Replace("[CATEGORY]", ddlCategory.SelectedItem.Text.ToString());
            strSubjectMsgT= strSubjectMsgT.Replace("[CATEGORY]", ddlCategory.SelectedItem.Text.ToString());
            objswp.Action = "A";
            objswp.vch_Type = ddlType.SelectedValue;
            objswp.int_CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
            objswp.int_SubcategoryId = Convert.ToInt32(ddlSubcategory.SelectedValue);
            objswp.int_DeptId = Convert.ToInt32(ddldepartment.SelectedValue);
            objswp.int_investId = Convert.ToInt32(ddlInvestor.SelectedValue);
            //objswp.vchInveUserId = Convert.ToString(ddlInvestor.SelectedValue);
            objswp.int_UserId = Convert.ToInt16(string.IsNullOrEmpty(ddlUser.SelectedValue) ? default(string) : ddlUser.SelectedValue.ToString());
            //Convert.ToInt32(ddlUser.SelectedValue);
            if (ddlType.SelectedValue == "3")
            {
                objswp.OtherName = txtOName.Text.Trim();
            }
            else if (ddlType.SelectedValue == "2")
            {
                objswp.OtherName = ddlInvestor.SelectedItem.ToString();
            }
            else if (ddlType.SelectedValue == "1")
            {
                objswp.OtherName = ddlUser.SelectedItem.ToString();
            }
            else
            {
                objswp.OtherName = "";
            }
            objswp.Address = txtAddress.Text.Trim();
            objswp.VchMobile = txtMobile.Text.Trim();
            objswp.Email = txtEmail.Text.Trim();
            objswp.status = 1;
            objswp.vch_IssueDetails = txtIssue.Text.Trim();
            objswp.vch_IssueSource = drpStatus.SelectedValue;
            objswp.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            objswp.intUpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + FileUpload1.FileName, DateTime.Now);
            if (FileUpload1.HasFile)
            {

                if (Path.GetExtension(FileUpload1.FileName) != ".pdf")
                {
                    string strmsg11 = "alert('Only .pdf file accepted!');";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Click", strmsg11, true);
                    return;
                }
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("../ApprovalDocs"));
            if (!string.IsNullOrEmpty(FileUpload1.FileName))
            {
                if (dir.Exists)
                {
                    FileUpload1.SaveAs(Server.MapPath("../ApprovalDocs/" + filepath));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("../ApprovalDocs"));
                    FileUpload1.SaveAs(Server.MapPath("../ApprovalDocs/" + filepath));

                }
                Uploadname = filepath;
            }
            else { Uploadname = ""; }
            objswp.vch_FIleUpload = Uploadname;
            objswp.vch_Priority = ddlPriority.SelectedItem.ToString();
            rtnList = objlayer.AddIssueRegister(objswp);
            if (rtnList.Count() > 0)
            {
                //---------------who raised issue---------------
                strMsg = strMsg.Replace("[ISSUENO]", rtnList[0].vchIssueNo);

                strSubjectMsg = strSubjectMsg.Replace("[ISSUENO]", rtnList[0].vchIssueNo);
                strSMSRegdContent = strSMSRegdContent.Replace("[ISSUENO]", rtnList[0].vchIssueNo);
                strEmailToATA = strEmailToATA.Replace("[ISSUENO]", rtnList[0].vchIssueNo);
                strEmailToATA = strEmailToATA.Replace("[Description]", rtnList[0].vch_IssueDetails);
                string[] toEmail = new string[1];
                toEmail[0] = rtnList[0].vchUserEmail.ToString();
                MailStatus1 = comm.sendMail(strSubjectMsg, strMsg, toEmail, true);
                smsStatus1 = comm.SendSmsNew(rtnList[0].vchUserMobileNo, strSMSRegdContent);
                //-----------who raised issue end------------------------------
                //---------RAEmail Send-----------------------
                string[] RAtoEmail = new string[1];
                if (rtnList[0].Email.Contains(','))
                {
                    string[] RAEmailArry = rtnList[0].Email.Split(',');
                    string[] RAPhoneArry = rtnList[0].VchMobile.Split(',');
                    string str1;
                    for (int i = 0; RAEmailArry.Length > i; i++)
                    {
                        if (RAEmailArry[i] != "" && RAEmailArry[i] != null)
                        {
                            RAtoEmail[0] = RAEmailArry[i].ToString();
                            MailStatus = comm.sendMail(strSubjectMsg, HttpUtility.HtmlDecode(rtnList[0].vchEmailContent).Replace("[ISSUENO]", rtnList[0].vchIssueNo).Replace("[Investor]", strInvestorName).Replace("[Description]", txtIssue.Text.ToString()), RAtoEmail, true);//Repoting authority
                            str1 = comm.UpdateMailSMSStaus("Issue", RAPhoneArry[i], RAEmailArry[i].ToString(), "Issue Raised", "1", "1", 1, "1", rtnList[0].vchMobileContent.ToString(), rtnList[0].VCHATAEMAIL.ToString(), smsStatus, MailStatus);

                        }
                        if (RAPhoneArry[i] != "" && RAPhoneArry[i] != null)
                        {
                            smsStatus = comm.SendSmsNew(RAPhoneArry[i].ToString(), rtnList[0].vchMobileContent.Replace("[ISSUENO]", rtnList[0].vchIssueNo).Replace("[Investor]", strInvestorName));
                        }
                        i = i++;

                    }
                }
                else
                {
                    RAtoEmail[0] = rtnList[0].Email.ToString();
                    MailStatus = comm.sendMail(strSubjectMsg, HttpUtility.HtmlDecode(rtnList[0].vchEmailContent).Replace("[ISSUENO]", rtnList[0].vchIssueNo).Replace("[Investor]", strInvestorName).Replace("[Description]", txtIssue.Text.ToString()), RAtoEmail, true);//Repoting authority
                    smsStatus = comm.SendSmsNew(rtnList[0].VchMobile.ToString(), rtnList[0].vchMobileContent.Replace("[ISSUENO]", rtnList[0].vchIssueNo).Replace("[Investor]", strInvestorName));
                    strText = strText + "Mailid:- " + rtnList[0].Email.ToString() + " EMail Content:- " + HttpUtility.HtmlDecode(rtnList[0].vchEmailContent).Replace("[ISSUENO]", rtnList[0].vchIssueNo).Replace("[Investor]", strInvestorName);
                }
                //------------------RAEmail Send end---------------------------------
                //-----------------ATAEMAIL-------------------------
                string[] ATAtoEmail = new string[1];
                ATAtoEmail[0] = rtnList[0].VCHATAEMAIL.ToString();
                MailStatus2 = comm.sendMail(strSubjectMsg, HttpUtility.HtmlDecode(rtnList[0].vchEmailContent).Replace("[ISSUENO]", rtnList[0].vchIssueNo).Replace("[Investor]", strInvestorName).Replace("[Description]", txtIssue.Text.ToString()), ATAtoEmail, true);
                smsStatus2 = comm.SendSmsNew(rtnList[0].VCHATAMOBILENO, rtnList[0].vchMobileContent.Replace("[ISSUENO]", rtnList[0].vchIssueNo).Replace("[Investor]", strInvestorName));
                strText = strText + "Email:-" + rtnList[0].VCHATAEMAIL.ToString() + "Email Content:-" + HttpUtility.HtmlDecode(rtnList[0].vchEmailContent).Replace("[ISSUENO]", rtnList[0].vchIssueNo).Replace("[Investor]", strInvestorName);
                //------------------------ATAEMAILend---------------------

                // sendMail("Issue Raised", rtnList[0].vchEmailContent.ToString(), ATAtoEmail, true);//ATA authority


                string str = comm.UpdateMailSMSStaus(strSubjectMsgT, rtnList[0].vchUserMobileNo, rtnList[0].vchUserEmail.ToString(), "Issue Raised", "1", "1", 1, "1", strSMSRegdContent, strMsg, smsStatus1, MailStatus1);
                str = comm.UpdateMailSMSStaus("Issue", rtnList[0].VCHATAMOBILENO, rtnList[0].VCHATAEMAIL.ToString(), "Issue Raised", "1", "1", 1, "1", HttpUtility.HtmlDecode(rtnList[0].vchEmailContent).Replace("[ISSUENO]", rtnList[0].vchIssueNo).Replace("[Investor]", strInvestorName), rtnList[0].vchMobileContent.Replace("[ISSUENO]", rtnList[0].vchIssueNo).Replace("[Investor]", strInvestorName), smsStatus2, MailStatus2);
                // Response.Write( strText);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('Data Saved successfully !');", true);//
                clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('Escalation not configured  for this category and subcategory !');", true);
                clear();

            }
        }

        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    public bool sendMail(string strSubject, string strBody, string[] toEmail, bool enbleSSl)
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
            strBody += "<br>From,<br>Single Window Portal<br>-------------------------------------------------------------------<br>Regards <br>GOSWIFT Helpdesk Team <br>This is a system generated Mail.Please don't Reply to this mail.<br>";
            mail.Body = strBody;

            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smptpPort"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromEmailPassword"].ToString());
            SmtpServer.EnableSsl = enbleSSl;
            mail.IsBodyHtml = true;
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

            SmtpServer.Send(mail);
            Res = true;
        }
        catch
        {
            Res = false;
        }
        return Res;

    }
    public void BindType()
    {
        try
        {
            objswp = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
            objswp.Action = "T";
            objHelpdesk = objlayer.BindType(objswp).ToList();
            ddlType.DataTextField = "strTypeName";
            ddlType.DataValueField = "intTypeId";
            ddlType.DataSource = objHelpdesk;
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    protected void ddlInvestor_SelectedIndexChanged(object sender, EventArgs e)
    {
        objswp = new IssueRegistration();
        objlayer = new HelpDeskBusinessLayer();

        List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
        objswp.Action = "B";
        //objswp.vchInveUserId =Convert.ToString(ddlInvestor.SelectedValue);
        objswp.int_investId = Convert.ToInt32(ddlInvestor.SelectedValue);
        objHelpdesk = objlayer.GetEmailID(objswp).ToList();
        if (objHelpdesk.Count > 0)
        {
            txtEmail.Text = objHelpdesk[0].Email.ToString();
        }
    }
    protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        objswp = new IssueRegistration();
        objlayer = new HelpDeskBusinessLayer();

        List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
        objswp.Action = "D";
        objswp.int_investId = Convert.ToInt32(ddlUser.SelectedValue);
        objHelpdesk = objlayer.GetEmailID(objswp).ToList();
        if (objHelpdesk.Count > 0)
        {
            txtEmail.Text = objHelpdesk[0].Email.ToString();
        }
    }
}