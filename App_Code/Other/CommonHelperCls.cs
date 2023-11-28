#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   CommonHelperCls.cs
// Description           :   Repository of Common Functions
// Created by            :   AMit Sahoo
// Created On            :   17 July 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Text;
using System.Security.Cryptography;
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mail;
//using EntityLayer.Investor;

/// <summary>
/// Summary description for CommonHelperCls
/// </summary>
public class CommonHelperCls
{
    SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    public CommonHelperCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void BindDropDown(DropDownList ddlList, string strValue, string strText, string strQuery)
    {
        try
        {
            string cs = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;

            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(strQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddlList.DataSource = ds.Tables[0];
            ddlList.DataTextField = strText;
            ddlList.DataValueField = strValue;
            ddlList.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlList.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// Added by Amit Sahoo to Check if Username exists or not
    /// </summary>
    /// <param name="strUserID"></param>
    /// <returns></returns>
    public string CheckUsername(string strUserID)
    {
        try
        {
            string Str_RetValue = "";
            string cs = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_INVESTOR_DETAILS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "CU");
                cmd.Parameters.AddWithValue("@P_VCH_INV_USERID", strUserID);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.Int);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                con.Close();
            }

            return Str_RetValue;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string CheckDuplicateEmailAndMobile(string strEmail, string strMobile)
    {
        try
        {
            string Str_RetValue = "0";
            string cs = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            //SqlCommand com = new SqlCommand("SELECT * FROM M_POR_INVESTOR_REGISTRATION where VCH_EMAIL='" + strEmail + "' and VCH_OFF_MOBILE='" + strMobile + "'", con);
            SqlCommand com = new SqlCommand("SELECT * FROM M_INVESTOR_DETAILS where BIT_DELETED_FLAG =0 and INT_OTP_STATUS=1 and  VCH_OFF_MOBILE='" + strMobile + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Str_RetValue = "1";
            }
            return Str_RetValue;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Added by Amit Sahoo Sending SMS
    /// </summary>
    /// <param name="strMobNo"></param>
    /// <param name="Sms"></param>
    public void SendSms(string strMobNo, string Sms)
    {
        string Res = "";
        try
        {
            /*----------------------------------------------------------------------------*/
            //Changes made by Sushant Jena on Dated: 28-Jan-2021.
            //The message signature changed from "GOSWFT" to "IPICOL".
            //The dlt_entity_id is added in SMS URL.
            /*----------------------------------------------------------------------------*/
            string fb_url = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=goswift.sms&pin=Np%40%236745&message=" + Sms + "&mnumber=91" + strMobNo + "&=IPICOL&dlt_entity_id=1001936451134336346";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fb_url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();
            Res = objText;
        }
        catch (Exception ex)
        {
            Res = ex.Message;
        }
    }

    public bool SendSmsNew(string strMobNo, string Sms)
    {
        bool Res = false;
        try
        {
            /*----------------------------------------------------------------------------*/
            //Changes made by Sushant Jena on Dated: 28-Jan-2021.
            //The message signature changed from "GOSWFT" to "IPICOL".
            //The dlt_entity_id is added in SMS URL.
            /*----------------------------------------------------------------------------*/
            string fb_url = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=goswift.sms&pin=Np%40%236745&message=" + Sms + "&mnumber=91" + strMobNo + "&signature=IPICOL&dlt_entity_id=1001936451134336346";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fb_url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();
            Res = true;
        }
        catch (Exception ex)
        {
            Res = false;
        }
        return Res;
    }

    public string UpdateMailSMSStaus(string strmsgType, string mob, string strMailID, string strMailSubject, string UserId, string ServiceId, int intstatus, string strAppNo, string strSMSContent, string strMailContent, bool SMSStatus, bool EmailStatus)
    {
        try
        {
            string Str_RetValue = "";
            string cs = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                int intSMSStatus;
                int intEmailStatus;

                if (EmailStatus == true)
                    intEmailStatus = 1;
                else intEmailStatus = 2;
                if (SMSStatus == true)
                    intSMSStatus = 1;
                else intSMSStatus = 2;
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_Ortps_SMSConfiguration_ALL", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", "I");
                cmd.Parameters.AddWithValue("@P_VCH_TYPE", strmsgType);
                cmd.Parameters.AddWithValue("@P_INT_SERVICE_ID", ServiceId);
                cmd.Parameters.AddWithValue("@P_INT_STATUS_ID", intstatus);
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", strAppNo);
                cmd.Parameters.AddWithValue("@P_VCH_MOBILE_NO", mob);
                cmd.Parameters.AddWithValue("@P_VCH_EMAIL_ID", strMailID);
                cmd.Parameters.AddWithValue("@P_VCH_SMS_CONT1", strSMSContent);
                cmd.Parameters.AddWithValue("@P_VCH_EMAIL_CONT1", strMailContent);
                cmd.Parameters.AddWithValue("@P_INT_SMS_STATUS1", intSMSStatus);
                cmd.Parameters.AddWithValue("@P_INT_EMAIL_STATUS1", intEmailStatus);
                cmd.Parameters.AddWithValue("@P_INT_USER_ID", UserId);
                cmd.Parameters.AddWithValue("@P_vch_Subject", strMailSubject);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                con.Close();
            }

            return Str_RetValue;
        }
        catch (Exception ex)
        {
            throw ex;
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
            strBody += "<br>From,<br>Single Window Portal<br>-------------------------------------------------------------------<br>This is a system generated Mail.Please don't Reply to this mail.<br>";
            mail.Body = strBody;
            //if (attchfile != null)
            //{
            //    mail.Attachments.Add(attchfile);
            //}
            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smptpPort"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromEmailPassword"].ToString());
            SmtpServer.EnableSsl = false;
            mail.IsBodyHtml = true;

            //Warning: do not use this in production code!
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

            //These are need tobe comment in PROD server
            // END 
            SmtpServer.Send(mail);
            Res = true;
        }
        catch
        {
            Res = false;
        }
        return Res;

    }

    ///// Added by Sushant Jena On 20-03-2018 for CC and BCC mail
    public bool sendMailScheduler(string strSubject, string strBody, string[] toEmail, string[] ccEmail, string[] bccEmail, bool enbleSSl)
    {
        bool Res = false;
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smptp"].ToString());
            mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"].ToString());

            ////// Add To Mail
            for (int i = 0; i < toEmail.Length; i++)
            {
                mail.To.Add(toEmail[i]);
            }

            ////// Add CC Mail
            for (int j = 0; j < ccEmail.Length; j++)
            {
                mail.CC.Add(ccEmail[j]);
            }

            ////// Add BCC Mail
            for (int k = 0; k < bccEmail.Length; k++)
            {
                mail.Bcc.Add(bccEmail[k]);
            }

            mail.Subject = strSubject;
            //strBody += "<br>From,<br>Single Window Portal<br>-------------------------------------------------------------------<br>This is a system generated Mail.Please don't Reply to this mail.<br>";
            mail.Body = strBody;
            //if (attchfile != null)
            //{
            //    mail.Attachments.Add(attchfile);
            //}
            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smptpPort"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromEmailPassword"].ToString());
            SmtpServer.EnableSsl = false;
            mail.IsBodyHtml = true;

            //Warning: do not use this in production code!
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

            //These are need tobe comment in PROD server
            // END 
            SmtpServer.Send(mail);
            Res = true;
        }
        catch
        {
            Res = false;
        }
        return Res;
    }

    /// <summary>
    /// Added by Amit Sahoo for OTP Creation
    /// </summary>
    /// <param name="name"></param>
    /// <param name="mob"></param>
    /// <param name="strAction"></param>
    /// <param name="UserId"></param>
    /// <returns></returns>
    public string AddOTP(string name, string mob, string strAction, string UserId)
    {
        string rndNumbermob;
        try
        {
            Random rdm = new Random();
            rndNumbermob = rdm.Next(1000, 9999).ToString();
            string Str_RetValue = "";
            string cs = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_INVESTOR_DETAILS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "I");
                cmd.Parameters.AddWithValue("@P_VCH_IND_NAME", name);
                cmd.Parameters.AddWithValue("@P_VCH_INV_USERID", UserId);
                cmd.Parameters.AddWithValue("@P_VCH_OFF_MOBILE", mob);
                cmd.Parameters.AddWithValue("@P_VCH_MOB_OTP", rndNumbermob);
                cmd.Parameters.AddWithValue("@P_BIT_DELETED_FLAG", 0);

                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                return Str_RetValue;

                //cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                //cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                //cmd.ExecuteNonQuery();
                //Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                con.Close();
            }

            return Str_RetValue;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Added by Amit Sahoo for matching OTP
    /// </summary>
    /// <param name="strname"></param>
    /// <returns></returns>
    public string CheckOTP(string strname)
    {
        try
        {
            string Str_RetValue = "";
            string cs = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_INVESTOR_DETAILS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "G");
                cmd.Parameters.AddWithValue("@P_VCH_INV_USERID", strname);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.Int);
                Str_RetValue = (string)cmd.ExecuteScalar();
                con.Close();
            }

            return Str_RetValue;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static DataTable getOTPTIME(string UserId, string action)
    {
        DataTable dt = new DataTable();
        CommonHelperCls ob = new CommonHelperCls();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        try
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_INVESTOR_DETAILS";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);

            cmd.Parameters.AddWithValue("@P_VCH_INV_USERID", UserId);
            if (conn.State == ConnectionState.Closed) { conn.Open(); }
            SqlDataAdapter da = new SqlDataAdapter(cmd); da.Fill(dt); return dt;
        }
        catch (Exception ex) { throw ex; }
        finally { cmd = null; conn.Close(); }
    }

    /// <summary>
    /// Added by Amit Sahoo to Convert List to DataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    #region ConvertToDataTable

    public static DataTable ConvertToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        if (table.Columns.Contains("ExtensionData"))
        {
            table.Columns.Remove("ExtensionData");
        }
        return table;

    }
    #endregion

    /// <summary>
    /// Added by Amit Sahoo to encrypt password
    /// </summary>
    /// <param name="SourceText"></param>
    /// <returns></returns>
    public static string GenerateHash(string SourceText)
    {
        UTF8Encoding Ue = new UTF8Encoding();
        string pwdString = null;
        MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
        byte[] ByteHash = Md5.ComputeHash(Ue.GetBytes(SourceText));
        pwdString = BitConverter.ToString(ByteHash);
        pwdString = pwdString.Replace("-", null);
        return pwdString;
    }

    public static DataTable getOTP(string action, string IndName)
    {
        DataTable dt = new DataTable();
        CommonHelperCls ob = new CommonHelperCls();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        try
        {

            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_INVESTOR_DETAILS";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "G");
            cmd.Parameters.AddWithValue("@P_VCH_IND_NAME", IndName);
            // cmd.Parameters.AddWithValue("@P_VCH_INV_USERID", UserId);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally { cmd = null; conn.Close(); }

    }

    public static DataTable ViewInvestorDetails(string UserId, string action)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

        try
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_INVESTOR_DETAILS";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
            cmd.Parameters.AddWithValue("@P_VCH_INV_USERID", UserId);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            return dt;
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }

    }

    public string CheckIndGroup(string strname)
    {
        try
        {
            string Str_RetValue = "";
            string cs = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_INVESTOR_DETAILS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "IGE");
                cmd.Parameters.AddWithValue("@P_VCH_INDUSTRY_NAME", strname);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                return Str_RetValue;
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            //con.Close();
        }

    }

    public static DataTable ViewSiteDetails(string ProposalNo, string action)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_PEAL_SITE_DETAILS";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PvchAction", action);
            cmd.Parameters.AddWithValue("@PvchProposalNo", ProposalNo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
        return dt;
    }

    public static DataTable ViewSiteGridDetails(string ProposalNo, string action)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

        try
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_PEAL_SITE_DETAILS";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PvchAction", action);
            cmd.Parameters.AddWithValue("@PvchProposalNo", ProposalNo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
        return dt;
    }

    public string CheckPANIsExist(string PANNo)
    {
        try
        {

            string Str_RetValue = "";
            string cs = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("USP_INVESTOR_DETAILS", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "IKE");
                cmd.Parameters.AddWithValue("@P_VCH_PAN", PANNo);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                con.Close();
            }

            return Str_RetValue;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Added by Sushant Jena On Dt:-13-07-2021
    /// New SMS method with Template Id and Odia SMS.
    /// </summary>
    /// <param name="strMobNo"></param>
    /// <param name="strSmsContent"></param>
    /// <param name="strTemplateId"></param>
    /// <param name="strTemplateName"></param>
    /// <param name="strMsgType"></param>
    /// <returns></returns>
    public bool SendSmsWithTemplate(string strMobNo, string strSmsContent, string strTemplateId, string strMsgType)
    {
        bool Res = false;
        try
        {
            Random objRan = new Random();
            int intRanNo = objRan.Next(100, 999);

            /*-------------------------------------------------------------------*/
            //// For English SMS use "HttpLink" and for Odia SMS use "MLink"
            /*-------------------------------------------------------------------*/
            //string fb_url = "https://smsgw.sms.gov.in/failsafe/HttpLink?username=goswift.sms&pin=Np%40%236745&message=" + strSmsContent + "&mnumber=91" + strMobNo + "&signature=GOSWIT&dlt_entity_id=1001936451134336346&dlt_template_id=" + strTemplateId + "";

            //if (strMsgType.ToUpper() == "ODIA") //// For Odia SMS, Use Mlink.
            //{
            //    fb_url = "https://smsgw.sms.gov.in/failsafe/MLink?username=goswift.sms&pin=Np%40%236745&message=" + strSmsContent + "&mnumber=91" + strMobNo + "&signature=GOSWIT&dlt_entity_id=1001936451134336346&dlt_template_id=" + strTemplateId + "";
            //}

            string fb_url = "https://smsgw.sms.gov.in/failsafe/MLink?username=goswift.sms&pin=Np%40%236745&message=" + strSmsContent + "&mnumber=91" + strMobNo + "&signature=GOSWIT&dlt_entity_id=1001936451134336346&dlt_template_id=" + strTemplateId + "";

            /*-------------------------------------------------------------------*/
            ///// Write the Request Response Log
            /*-------------------------------------------------------------------*/
            Util.LogRequestResponse("SendSMS", "SendSmsWithTemplate", Convert.ToString(intRanNo) + " - " + fb_url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fb_url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();
            Res = true;

            /*-------------------------------------------------------------------*/
            ///// Write the Request Response Log
            /*-------------------------------------------------------------------*/
            Util.LogRequestResponse("SendSMS", "SendSmsWithTemplate", Convert.ToString(intRanNo) + " - " + objText);
        }
        catch (Exception ex)
        {
            Res = false;
            Util.LogError(ex, "SendSMS");
        }
        return Res;
    }

    /// <summary>
    /// Added by Sushant Jena On Dt:-13-07-2021
    /// Send Promotional SMS
    /// </summary>
    /// <param name="strMobNo"></param>
    /// <param name="strEvent"></param>
    public void SendPromotionalSMS(string strMobNo, string strEvent)
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataTable objdt = new DataTable();

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }

        try
        {
            objCommand.CommandText = "USP_FETCH_PROMOTIONAL_SMS_EMAIL";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "A");
            objCommand.Parameters.AddWithValue("@P_VCH_EVENT", strEvent);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objdt);

            if (objdt.Rows.Count > 0)
            {
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        System.Threading.Thread.Sleep(2000);
                    }

                    string strSMSContent = Convert.ToString(objdt.Rows[i]["vchSMSContent"]);
                    string strMsgType = Convert.ToString(objdt.Rows[i]["vchMsgType"]);
                    string strTemplateId = Convert.ToString(objdt.Rows[i]["vchTemplateId"]);

                    /*-------------------------------------------------------------------*/
                    //// Send SMS with Template Id
                    /*-------------------------------------------------------------------*/
                    bool smsStatus = SendSmsWithTemplate(strMobNo, strSMSContent, strTemplateId, strMsgType);

                    /*-------------------------------------------------------------------*/
                    //// Update SMS and Email Status in Transaction Table
                    /*-------------------------------------------------------------------*/
                    UpdateMailSMSStaus("Promotional", strMobNo, "", strEvent, "0", "0", 0, "0", strSMSContent, "", smsStatus, false);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PromotionalSMS");
        }
        finally
        {
            objConn.Close();
            objCommand = null;
            objdt = null;
        }
    }
}
