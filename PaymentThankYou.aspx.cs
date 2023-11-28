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
using System.Security.Cryptography;
using System.IO;

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

    public string OrderID
    {
        set
        {
            this.hdOrderId.Value = value;
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
                            string Completedstatus = Convert.ToString(dt.Rows[i]["intCompletedStatus"]);
                            if (Key != "" && Completedstatus == "1")
                            {
                                if (sbApplicationKey.Length <= 0)
                                {
                                    sbApplicationKey.Append("" + Key + "");
                                }
                                else
                                {
                                    sbApplicationKey.Append(", " + Key + "");
                                }

                                objData.UpdatePaymentStatus(Key, "NA", "NA", 0, "NA");
                            }
                        }

                        test.Visible = false;
                        lblPaymentStatus.Text = "Thank you for registration";
                        lblReference.Text = "Please keep your application no.: " + sbApplicationKey.ToString() + " for your future reference.";

                        Img1.Visible = true;
                        Img2.Visible = false;

                        OrderID = "";

                        
                    }
                }
                else
                {

                    ProjectInfo objProject = new ProjectInfo();
                                   
                   
                    System.Collections.Specialized.NameValueCollection
                        postedValues = Request.Form;                   
                    String lStrMsg = string.Empty;

                    string chalanAmt = "";
                    string BnkTransid = "";
                    string ChallanRefid = "";
                    string BnkTranStatus = "";
                    string BnkTranTimestamp = "";
                   

                    lStrMsg = decrypt(postedValues[0].ToString());


                    Session["ResponseMessage"] = lStrMsg;
                    string[] Fstrmsg = lStrMsg.ToString().Split('|');

                                           
                        BnkTransid = Fstrmsg[39].ToString();
                        chalanAmt= Fstrmsg[20].ToString();
                        ChallanRefid = Fstrmsg[36].ToString();
                        BnkTranStatus = Fstrmsg[40].ToString();
                        BnkTranTimestamp = Fstrmsg[42].ToString();
                       
                    
                    objProject.OrderNo = Fstrmsg[1].ToString();

                    if (BnkTranStatus == "S")
                    {
                        DataTable dt = GetApplicationNumber(Fstrmsg[1].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string Key = Convert.ToString(dt.Rows[i]["vchapplicationno"]);
                                if (i == 0)
                                {
                                    sbApplicationKey.Append("" + Key + "");
                                }
                                else
                                {
                                    sbApplicationKey.Append(", " + Key + "");
                                }
                                objData.UpdatePaymentStatus(Key, BnkTransid, ChallanRefid, 1, Fstrmsg[1].ToString());
                            }
                            test.Visible = true;
                            Img1.Visible = true;
                            Img2.Visible = false;
                            lblPaymentStatus.Text = "Your Payment Successful";
                            lblchallanAmt.Text = chalanAmt;
                            lblchallanrefid.Text = ChallanRefid;
                            lblTrancId.Text = BnkTransid;
                            lblReferenceId.Visible = true;
                            lblReferenceId.Text = "Please keep your application no.: " + sbApplicationKey.ToString() + " for your future reference.";
                            OrderID= Fstrmsg[1].ToString();
                          
                        }                       
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
                Img1.Visible = true;
                Img2.Visible = false;
                objData.UpdatePaymentStatus(Request.QueryString["ApplicationKey"].ToString(), "NA", "NA", 0, "NA");
                strApplicationKey= Request.QueryString["ApplicationKey"].ToString();
                OrderID = "";
              
            }

            else
            {
                ProjectInfo objProject = new ProjectInfo();
                   
              
                System.Collections.Specialized.NameValueCollection
                    postedValues = Request.Form;               
                String lStrMsg = string.Empty;

                string chalanAmt = "";
                string BnkTransid = "";
                string ChallanRefid = "";
                string BnkTranStatus = "";
                string BnkTranTimestamp = "";

                lStrMsg = decrypt(postedValues[0].ToString());


                Session["ResponseMessage"] = lStrMsg;
                string[] Fstrmsg = lStrMsg.ToString().Split('|');

                
                    BnkTransid = Fstrmsg[39].ToString();
                    chalanAmt = Fstrmsg[20].ToString();
                    ChallanRefid = Fstrmsg[36].ToString();
                    BnkTranStatus = Fstrmsg[40].ToString();
                    BnkTranTimestamp = Fstrmsg[42].ToString();
                           

              
                objProject.OrderNo = Fstrmsg[1].ToString();
                string[] Uid = Fstrmsg[1].ToString().Split('-');
                strApplicationKey = Uid[0].ToString().Replace("ES", "");

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
                    objData.UpdatePaymentStatus(strApplicationKey, BnkTransid, ChallanRefid, 1, Fstrmsg[1].ToString());
                    OrderID = "";
                   
                }
            }
            
            
        }
    }

    public string InvestMobileNo(string Applicationkey)
    {        
        string queryCnt = "SELECT TOP(1) A.VCH_OFF_MOBILE from M_INVESTOR_DETAILS A,T_APPLICATION_TBL B where A.INT_INVESTOR_ID=B.INT_CREATEDBY AND B.VCH_APPLICATION_UNQ_KEY='" + Applicationkey + "'";
        string strInvestorMobileNo = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
               
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                strInvestorMobileNo = cmd1.ExecuteScalar().ToString();
                con.Close();
            }
        }
        return strInvestorMobileNo;
    }
    public string InvestorMail(string ApplicationKey)
    {        
        string queryCnt = "SELECT TOP(1) A.VCH_EMAIL from M_INVESTOR_DETAILS A,T_APPLICATION_TBL B where A.INT_INVESTOR_ID=B.INT_CREATEDBY AND B.VCH_APPLICATION_UNQ_KEY='" + ApplicationKey + "'";
        string strInvestorEmail = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
               
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                strInvestorEmail = cmd1.ExecuteScalar().ToString();
                con.Close();
            }
        }
        return strInvestorEmail;
    }
    public string InvestorName(string ApplicationKey)
    {       
        string queryCnt = "SELECT TOP(1) A.VCH_INV_NAME from M_INVESTOR_DETAILS A,T_APPLICATION_TBL B where A.INT_INVESTOR_ID=B.INT_CREATEDBY AND B.VCH_APPLICATION_UNQ_KEY='" + ApplicationKey + "'";
        string strInvestorEmail = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
                
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                strInvestorEmail = cmd1.ExecuteScalar().ToString();
                con.Close();
            }
        }
        return strInvestorEmail;
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
    private DataTable GetApplicationNumber(string ordno)
    {
        DataTable dt = new DataTable();
        if (ordno != "")
        {
            string queryCnt = "SELECT vchapplicationno FROM T_Service_Order WHERE vchOrderNo='" + ordno + "'";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd1 = new SqlCommand(queryCnt))
                {
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Connection = con;
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
                    adapter.Fill(dt);
                    con.Close();
                }
            }
        }
        return dt;
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
    public void SendSmSaNDMail()
    {
       
        ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
        string strSubject = "";
        string SMSContent = "";
        string strATASubject = "";
        string mobileNo = InvestMobileNo(strApplicationKey);
        CommonHelperCls comm = new CommonHelperCls();
        string strServiceName = "";
        string strATAContent = "";
        string strInvesName = InvestorName(strApplicationKey);
        List<ServiceDetails> objServicelist = objServiceDet.GetEmailAndMobile("IM", strApplicationKey);
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
        if (InvestorMail(strApplicationKey) != null)
        {
            string[] toEmail = new string[1];
            toEmail[0] = InvestorMail(strApplicationKey).ToString().Trim();
            mailStatus = sendMail(strSubject, SMSContent, toEmail, true);
        }
        ///---------------------------------------------------------
        ///

        //Added BY Pritiprangya on 25-Oct-2017

        // FOR SMS and Mail Status Update
        string str = comm.UpdateMailSMSStaus("Service", mobileNo, (InvestorMail(strApplicationKey).ToString().Trim()), strSubject,Convert.ToString(Session["InvestorId"]), GetFormId(strApplicationKey.ToString()).ToString(), 1, strApplicationKey.ToString(), SMSContent, SMSContent, smsStatus, mailStatus);
        str = comm.UpdateMailSMSStaus("Service", objServicelist[0].strMobileno.ToString(), objServicelist[0].Email.ToString(), strATASubject,Convert.ToString(Session["InvestorId"]), GetFormId(strApplicationKey.ToString()).ToString(), 1, strApplicationKey.ToString(), strATAContent, strATAContent, smsStatus1, mailStatus1);
        // FOR SMS and Mail Status Update
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
                    string Completedstatus = Convert.ToString(dt.Rows[i]["intCompletedStatus"]);

                    if (strApplicationKey != "" && Completedstatus == "1")
                    {

                      
                        ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
                        string strSubject = "";
                        string SMSContent = "";
                        string strATASubject = "";
                        string mobileNo = InvestMobileNo(strApplicationKey);
                        CommonHelperCls comm = new CommonHelperCls();
                        string strServiceName = "";
                        string strATAContent = "";
                        string strInvesName = InvestorName(strApplicationKey);
                        List<ServiceDetails> objServicelist = objServiceDet.GetEmailAndMobile("IM", strApplicationKey);
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
                        if (InvestorMail(strApplicationKey) != null)
                        {
                            string[] toEmail = new string[1];
                            toEmail[0] = InvestorMail(strApplicationKey).ToString().Trim();
                            mailStatus = sendMail(strSubject, SMSContent, toEmail, true);
                        }
                        ///---------------------------------------------------------
                        ///

                        //Added BY Pritiprangya on 25-Oct-2017

                        // FOR SMS and Mail Status Update
                        string str = comm.UpdateMailSMSStaus("Service", mobileNo, (InvestorMail(strApplicationKey).ToString().Trim()), strSubject, Convert.ToString(Session["InvestorId"]), GetFormId(strApplicationKey.ToString()).ToString(), 1, strApplicationKey.ToString(), SMSContent, SMSContent, smsStatus, mailStatus);
                        str = comm.UpdateMailSMSStaus("Service", objServicelist[0].strMobileno.ToString(), objServicelist[0].Email.ToString(), strATASubject, Convert.ToString(Session["InvestorId"]), GetFormId(strApplicationKey.ToString()).ToString(), 1, strApplicationKey.ToString(), strATAContent, strATAContent, smsStatus1, mailStatus1);
                        // FOR SMS and Mail Status Update
                    }
                }
            }
        }
    }
    public void MultipleSendSmsAndEmailWithoutSession(string orderno)
    {
        DataTable dt = GetApplicationNumber(orderno);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strApplicationKey = Convert.ToString(dt.Rows[i]["vchapplicationno"]);

                List<ServiceDetails> objServicelist = new List<ServiceDetails>();
                ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
                string strSubject = "";
                string SMSContent = "";
                string strATASubject = "";
                string mobileNo = InvestMobileNo(strApplicationKey);
                CommonHelperCls comm = new CommonHelperCls();
                string strServiceName = "";
                string strATAContent = "";
                string strInvesName = InvestorName(strApplicationKey);
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
                if (InvestorMail(strApplicationKey) != null)
                {
                    string[] toEmail = new string[1];
                    toEmail[0] = InvestorMail(strApplicationKey).ToString().Trim();
                    mailStatus = sendMail(strSubject, SMSContent, toEmail, true);
                }
                ///---------------------------------------------------------
                ///

                //Added BY Pritiprangya on 25-Oct-2017

                // FOR SMS and Mail Status Update
                string str = comm.UpdateMailSMSStaus("Service", mobileNo, (InvestorMail(strApplicationKey).ToString().Trim()), strSubject, Convert.ToString(Session["InvestorId"]), GetFormId(strApplicationKey.ToString()).ToString(), 1, strApplicationKey.ToString(), SMSContent, SMSContent, smsStatus, mailStatus);
                str = comm.UpdateMailSMSStaus("Service", objServicelist[0].strMobileno.ToString(), objServicelist[0].Email.ToString(), strATASubject, Convert.ToString(Session["InvestorId"]), GetFormId(strApplicationKey.ToString()).ToString(), 1, strApplicationKey.ToString(), strATAContent, strATAContent, smsStatus1, mailStatus1);
                // FOR SMS and Mail Status Update
            }
        }
    }
    public String decrypt(String value)
    {
        string Path = ConfigurationSettings.AppSettings["IFMSKeyPath"];
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        AesManaged tdes = new AesManaged();
        tdes.Key = GetFileBytes(Path);
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform crypt = tdes.CreateDecryptor();
        byte[] plain = Convert.FromBase64String(value);
        byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
        String encryptedText = Encoding.UTF8.GetString(cipher);
        return encryptedText;
    }
    private static byte[] GetFileBytes(String filename)
    {
        if (!File.Exists(filename))
            return null;
        Stream stream = new FileStream(filename, FileMode.Open);
        int datalen = (int)stream.Length;
        byte[] filebytes = new byte[datalen];
        stream.Seek(0, SeekOrigin.Begin);
        stream.Read(filebytes, 0, datalen);
        stream.Close();
        return filebytes;
    }
    private string HmacSHA256(string message)
    {
        //secret = secret ?? "";
        string Path = ConfigurationSettings.AppSettings["IFMSKeyPath"];
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = GetFileBytes(Path); //encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }
    
}
