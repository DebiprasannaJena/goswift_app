using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using System.Text;

public partial class PaymentThankYou : System.Web.UI.Page
{
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    DataAcessLayer.Service.ServiceDataLayer objData = new DataAcessLayer.Service.ServiceDataLayer();
    string strApplicationKey = "";
    bool smsStatus;
    bool mailStatus;

    bool smsStatus1;
    bool mailStatus1;

    StringBuilder sbApplicationKey = new StringBuilder();

    public string ServiceId
    {
        set
        {
            this.hdServiceId.Value = value;
        }
    }
    public string ApplicationUniqueID
    {
        set
        {
            this.hdApplicationUniqueID.Value = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblNote.Visible = false;
        lblNote35.Visible = false;

        if (!IsPostBack)
        {
            if (Request.QueryString["ReqMode"] != "" && Request.QueryString["ReqMode"] != null) ///// Added by Manoj Kumar Behera On Dt:-23-Feb-2021 for multiple services
            {
                if (Session["SvcMasterData"] != null)
                {
                    DataTable dt = (DataTable)Session["SvcMasterData"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Key = Convert.ToString(dt.Rows[i]["vchApplicationKey"]);
                            if (i == 0)
                            {
                                sbApplicationKey.Append("" + Key + "");
                            }
                            else
                            {
                                sbApplicationKey.Append(", " + Key + "");
                            }

                            objData.UpdatePaymentStatus(Key, "NA", "NA", 0, "NA");
                        }

                        test.Visible = false;
                        lblPaymentStatus.Text = "Thank you for registration";
                        lblReference.Text = "Please keep your application no.: " + sbApplicationKey.ToString() + " for your future reference.";

                        Img1.Visible = true;
                        Img2.Visible = false;

                        MultipleSendSmsAndEmail();
                    }
                }
            }
            else if (Request.QueryString["ApplicationKey"] != null)
            {
                ApplicationUniqueID = Request.QueryString["ApplicationKey"].ToString();
                ServiceId = objData.getServiceId(Feedback1.ApplicationUniqueID);
                test.Visible = false;
                lblPaymentStatus.Text = "Thank you for registration";
                lblReference.Text = "Please keep your application no.: " + Request.QueryString["ApplicationKey"].ToString() + " for your future reference.";

                if (Convert.ToInt32(GetFormId(Request.QueryString["ApplicationKey"].ToString())) == 34)
                {
                    lblNote.Visible = true;
                }
                if (Convert.ToInt32(GetFormId(Request.QueryString["ApplicationKey"].ToString())) == 35)
                {
                    lblNote35.Visible = true;
                }
                Img1.Visible = true;
                Img2.Visible = false;
                objData.UpdatePaymentStatus(Request.QueryString["ApplicationKey"].ToString(), "NA", "NA", 0, "NA");

                SendSmSaNDMail();
            }
            else if (Request.QueryString["ReqMode"] == "" && Request.QueryString["ReqMode"] == null)
            {
                ProjectInfo objProject = new ProjectInfo();
                ProposalBAL objService = new ProposalBAL();
                string lreqId = string.Empty;
                string lOrdNo = string.Empty;
                System.Text.StringBuilder displayValues = new System.Text.StringBuilder();
                System.Collections.Specialized.NameValueCollection
                    postedValues = Request.Form;
                String nextKey;
                String lStrMsg = string.Empty;
                for (int i = 0; i < postedValues.AllKeys.Length; i++)
                {
                    nextKey = postedValues.AllKeys[i];
                    if (nextKey.Substring(0, 2) != "__")
                    {
                        if (i != 0)
                        {
                            displayValues.Append("&");
                        }
                        displayValues.Append(nextKey);
                        displayValues.Append("=");
                        displayValues.Append(postedValues[i]);
                        if (nextKey == "otherParameters")
                        {
                            lOrdNo = postedValues[i].ToString().Substring(postedValues[i].ToString().IndexOf('=') + 1, (postedValues[i].ToString().IndexOf('!') - postedValues[i].ToString().IndexOf('=') - 1));
                            lreqId = postedValues[i].Split(new[] { "!~!" }, StringSplitOptions.None)[1].Split('=')[1].ToString();
                        }
                        if (nextKey == "config")
                        {

                        }
                        if (nextKey == "challanAmount")
                        {
                            objProject.vchChallanAmt = postedValues[i].ToString();
                        }
                        if (nextKey == "bankTransactionId")
                        {
                            objProject.vchbankTransactionId = postedValues[i].ToString();
                        }
                        if (nextKey == "challanRefId")
                        {
                            objProject.vchbankchallanRefId = postedValues[i].ToString();
                        }
                        if (nextKey == "bankTransactionStatus")
                        {
                            objProject.vchbankTransactionStatus = postedValues[i].ToString();
                        }
                        if (nextKey == "bankTransactionStatus")
                        {
                            objProject.vchbankTransactionStatus = postedValues[i].ToString();
                        }
                        if (nextKey == "bankTransTimeStamp")
                        {
                            objProject.vchbankTransTimeStamp = postedValues[i].ToString();
                        }
                        if (nextKey == "ReqID")
                        {
                            objProject.vchReqID = lreqId;
                        }
                        lStrMsg = lStrMsg + "|" + nextKey + "|" + postedValues[i].ToString();
                    }
                }

                Session["ResponseMessage"] = lStrMsg;
                string[] Fstrmsg = lStrMsg.ToString().Split('|');
                string chalanAmt = "";
                string BnkTransid = "";
                string ChallanRefid = "";
                string BnkTranStatus = "";
                string BnkTranTimestamp = "";
                try
                {
                    chalanAmt = Fstrmsg[4].ToString();
                    BnkTransid = Fstrmsg[6].ToString();
                    ChallanRefid = Fstrmsg[8].ToString();
                    BnkTranStatus = Fstrmsg[10].ToString();
                    BnkTranTimestamp = Fstrmsg[12].ToString();
                }
                catch
                {
                    Random rdm = new Random();
                    string rndNumbermob;
                    rndNumbermob = rdm.Next(1000000, 9999999).ToString();

                    chalanAmt = "1.00";
                    BnkTransid = rndNumbermob;

                    Random rdm1 = new Random();
                    string rndNumbermob1;
                    rndNumbermob1 = rdm.Next(1000000, 9999999).ToString();


                    ChallanRefid = rndNumbermob1;
                    BnkTranStatus = "S";
                    BnkTranTimestamp = DateTime.Now.ToLongDateString();
                }

                string refid = string.Empty;
                objProject.ReqID = lreqId;
                objProject.OrderNo = lOrdNo;
                strApplicationKey = lreqId;

                string Uid = lreqId.ToString().Replace("ES", "");

                if (BnkTranStatus == "S")
                {
                    test.Visible = true;
                    Img1.Visible = true;
                    Img2.Visible = false;
                    lblPaymentStatus.Text = "Your Payment Successful";
                    lblchallanAmt.Text = chalanAmt;
                    lblchallanrefid.Text = ChallanRefid;
                    lblTrancId.Text = BnkTransid;
                    lblReferenceId.Visible = true;
                    lblReferenceId.Text = "Please keep your application no.: " + strApplicationKey + " for your future reference.";
                    ApplicationUniqueID = strApplicationKey;
                    ServiceId = objData.getServiceId(strApplicationKey);

                    if (Convert.ToInt32(GetFormId(strApplicationKey.ToString())) == 34)
                    {
                        lblNote.Visible = true;
                    }
                    if (Convert.ToInt32(GetFormId(strApplicationKey.ToString())) == 35)
                    {
                        lblNote35.Visible = true;
                    }
                    objData.UpdatePaymentStatus(Uid, BnkTransid, ChallanRefid, 1, lOrdNo);
                    SendSmSaNDMail();
                }
            }
            if (Session["ReferId"] != null)
            {

            }
            HttpContext _context = HttpContext.Current;
        }
    }

    public string InvestMobileNo()
    {
        int intInvestorId = Convert.ToInt32(Session["InvestorId"]);
        string queryCnt = "select top(1) VCH_OFF_MOBILE from M_INVESTOR_DETAILS where INT_INVESTOR_ID=" + intInvestorId + "";
        string strInvestorMobileNo = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                strInvestorMobileNo = cmd1.ExecuteScalar().ToString();
                con.Close();
            }
        }
        return strInvestorMobileNo;
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
            strBody += "<br>From,<br>Single Window Portal<br>-------------------------------------------------------------------<br>This is a system generated Mail.Please don't Reply to this mail.<br>";
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
    public string InvestorMail()
    {
        int intInvestorId = Convert.ToInt32(Session["InvestorId"]);
        string queryCnt = "select top(1) VCH_EMAIL from M_INVESTOR_DETAILS where INT_INVESTOR_ID=" + intInvestorId + "";
        string strInvestorEmail = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                strInvestorEmail = cmd1.ExecuteScalar().ToString();
                con.Close();
            }
        }
        return strInvestorEmail;
    }
    public string InvestorName()
    {
        int intInvestorId = Convert.ToInt32(Session["InvestorId"]);
        string queryCnt = "select top(1) VCH_INV_NAME from M_INVESTOR_DETAILS where INT_INVESTOR_ID=" + intInvestorId + "";
        string strInvestorEmail = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                strInvestorEmail = cmd1.ExecuteScalar().ToString();
                con.Close();
            }
        }
        return strInvestorEmail;
    }
    public void SendSmSaNDMail()
    {
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
        string strSubject = "";
        string SMSContent = "";
        string strATASubject = "";
        string mobileNo = InvestMobileNo();
        CommonHelperCls comm = new CommonHelperCls();
        string strServiceName = "";
        string strATAContent = "";
        string strInvesName = InvestorName();
        objServicelist = objServiceDet.GetEmailAndMobile("IM", strApplicationKey);
        if (mobileNo != "")
        {
            InvestorDetails objInvDet = new InvestorDetails();
            InvestorRegistration objInvService = new InvestorRegistration();

            objInvDet.strAction = "DS";
            DataTable dtcontent = objInvService.GetSMSContent(objInvDet);
            objInvDet.strAction = "AT";
            DataTable dtATAContent = objInvService.GetSMSContent(objInvDet);
            if (dtcontent.Rows.Count > 0)
            {
                strSubject = dtcontent.Rows[0]["vchEvent"].ToString();
                if (Request.QueryString["ApplicationKey"] != null)
                {
                    strATAContent = dtATAContent.Rows[0]["vchSMSContent"].ToString().Replace("[InvestorName]", strInvesName.ToString());
                    strServiceName = ServiceName(Request.QueryString["ApplicationKey"].ToString());
                    SMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[InvestorName]", strInvesName.ToString());
                    SMSContent = SMSContent.Replace("[ServiceName]", strServiceName);
                    strATASubject = dtATAContent.Rows[0]["vchEvent"].ToString().Replace("[ServiceName]", strServiceName.Trim());
                    strApplicationKey = Request.QueryString["ApplicationKey"].ToString();
                }
                else
                {
                    strATAContent = dtATAContent.Rows[0]["vchSMSContent"].ToString().Replace("[InvestorName]", strInvesName.ToString());
                    strServiceName = ServiceName(strApplicationKey.ToString().ToString());
                    SMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[InvestorName]", strInvesName.ToString());
                    SMSContent = SMSContent.Replace("[ServiceName]", strServiceName);
                    SMSContent = SMSContent.Replace("&", "And");
                    strATASubject = dtATAContent.Rows[0]["vchEvent"].ToString().Replace("[ServiceName]", strServiceName.Trim());
                }

                smsStatus = comm.SendSmsNew(mobileNo, SMSContent);
                objServicelist = objServiceDet.GetEmailAndMobile("TM", strApplicationKey);
                if (objServicelist[0].strMobileno.ToString() != "NA")
                {
                    smsStatus1 = comm.SendSmsNew(objServicelist[0].strMobileno.ToString(), strATAContent);
                }
                if (objServicelist[0].Email.ToString() != "NA")
                {
                    string[] ATAtoEmail = new string[1];
                    ATAtoEmail[0] = objServicelist[0].Email.ToString();
                    mailStatus1 = sendMail(strATASubject, strATAContent, ATAtoEmail, true);
                }
            }
        }
        ////////-----------------------------------Mail-------------
        if (InvestorMail() != null)
        {
            string[] toEmail = new string[1];
            toEmail[0] = InvestorMail().ToString().Trim();
            mailStatus = sendMail(strSubject, SMSContent, toEmail, true);
        }
        ///---------------------------------------------------------
        ///

        //Added BY Pritiprangya on 25-Oct-2017

        // FOR SMS and Mail Status Update
        string str = comm.UpdateMailSMSStaus("Service", mobileNo, (InvestorMail().ToString().Trim()), strSubject, Session["InvestorId"].ToString(), GetFormId(strApplicationKey.ToString()).ToString(), 1, strApplicationKey.ToString(), SMSContent, SMSContent, smsStatus, mailStatus);
        str = comm.UpdateMailSMSStaus("Service", objServicelist[0].strMobileno.ToString(), objServicelist[0].Email.ToString(), strATASubject, Session["InvestorId"].ToString(), GetFormId(strApplicationKey.ToString()).ToString(), 1, strApplicationKey.ToString(), strATAContent, strATAContent, smsStatus1, mailStatus1);
        // FOR SMS and Mail Status Update
    }
    public string ServiceName(string applicationkey)
    {
        string strSMSContent = "";
        if (applicationkey != "")
        {
            string queryCnt = "select VCH_SERVICENAME from T_APPLICATION_TBL t inner join M_SERVICEMASTER_TBL s on t.INT_SERVICEID=s.INT_SERVICEID  where VCH_APPLICATION_UNQ_KEY='" + applicationkey + "'";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd1 = new SqlCommand(queryCnt))
                {
                    List<ListItem> customers = new List<ListItem>();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = con;
                    con.Open();
                    strSMSContent = cmd1.ExecuteScalar().ToString();
                    con.Close();
                }
            }
        }
        return strSMSContent;
    }
    public int GetFormId(string applicationkey)
    {
        int strFormId = 0;
        if (applicationkey != "")
        {
            string queryCnt = " select INT_SERVICEID from T_APPLICATION_TBL where VCH_APPLICATION_UNQ_KEY='" + applicationkey + "'";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd1 = new SqlCommand(queryCnt))
                {
                    List<ListItem> customers = new List<ListItem>();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = con;
                    con.Open();
                    strFormId = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
                    con.Close();
                }
            }
        }
        return strFormId;
    }
    public void MultipleSendSmsAndEmail()
    {
        if (Session["SvcMasterData"] != null)
        {
            DataTable dt = (DataTable)Session["SvcMasterData"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strApplicationKey = Convert.ToString(dt.Rows[i]["vchApplicationKey"]);

                    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
                    ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
                    string strSubject = "";
                    string SMSContent = "";
                    string strATASubject = "";
                    string mobileNo = InvestMobileNo();
                    CommonHelperCls comm = new CommonHelperCls();
                    string strServiceName = "";
                    string strATAContent = "";
                    string strInvesName = InvestorName();
                    objServicelist = objServiceDet.GetEmailAndMobile("IM", strApplicationKey);
                    if (mobileNo != "")
                    {
                        InvestorDetails objInvDet = new InvestorDetails();
                        InvestorRegistration objInvService = new InvestorRegistration();

                        objInvDet.strAction = "DS";
                        DataTable dtcontent = objInvService.GetSMSContent(objInvDet);
                        objInvDet.strAction = "AT";
                        DataTable dtATAContent = objInvService.GetSMSContent(objInvDet);
                        if (dtcontent.Rows.Count > 0)
                        {
                            strSubject = dtcontent.Rows[0]["vchEvent"].ToString();

                            strATAContent = dtATAContent.Rows[0]["vchSMSContent"].ToString().Replace("[InvestorName]", strInvesName.ToString());
                            strServiceName = ServiceName(strApplicationKey.ToString().ToString());
                            SMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[InvestorName]", strInvesName.ToString());
                            SMSContent = SMSContent.Replace("[ServiceName]", strServiceName);
                            SMSContent = SMSContent.Replace("&", "And");
                            strATASubject = dtATAContent.Rows[0]["vchEvent"].ToString().Replace("[ServiceName]", strServiceName.Trim());

                            smsStatus = comm.SendSmsNew(mobileNo, SMSContent);
                            objServicelist = objServiceDet.GetEmailAndMobile("TM", strApplicationKey);
                            if (objServicelist[0].strMobileno.ToString() != "NA")
                            {
                                smsStatus1 = comm.SendSmsNew(objServicelist[0].strMobileno.ToString(), strATAContent);
                            }
                            if (objServicelist[0].Email.ToString() != "NA")
                            {
                                string[] ATAtoEmail = new string[1];
                                ATAtoEmail[0] = objServicelist[0].Email.ToString();
                                mailStatus1 = sendMail(strATASubject, strATAContent, ATAtoEmail, true);
                            }
                        }
                    }
                    ////////-----------------------------------Mail-------------
                    if (InvestorMail() != null)
                    {
                        string[] toEmail = new string[1];
                        toEmail[0] = InvestorMail().ToString().Trim();
                        mailStatus = sendMail(strSubject, SMSContent, toEmail, true);
                    }
                    ///---------------------------------------------------------
                    ///

                    //Added BY Pritiprangya on 25-Oct-2017

                    // FOR SMS and Mail Status Update
                    string str = comm.UpdateMailSMSStaus("Service", mobileNo, (InvestorMail().ToString().Trim()), strSubject, Session["InvestorId"].ToString(), GetFormId(strApplicationKey.ToString()).ToString(), 1, strApplicationKey.ToString(), SMSContent, SMSContent, smsStatus, mailStatus);
                    str = comm.UpdateMailSMSStaus("Service", objServicelist[0].strMobileno.ToString(), objServicelist[0].Email.ToString(), strATASubject, Session["InvestorId"].ToString(), GetFormId(strApplicationKey.ToString()).ToString(), 1, strApplicationKey.ToString(), strATAContent, strATAContent, smsStatus1, mailStatus1);
                    // FOR SMS and Mail Status Update
                }
            }
        }
    }
}
