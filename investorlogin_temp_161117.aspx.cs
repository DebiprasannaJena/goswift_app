#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   inestorlogin.aspx.cs
// Description           :   Investor Login
// Created by            :   AMit Sahoo
// Created On            :   20 July 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//                                                        2-Sep-2017                      Sanghamitra Samal          For Refresh and Get the Captcha Value
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion
#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Data;
using System.Net.Mail;
using System.Text;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using BusinessLogicLayer.Login;
using EntityLayer.Login;
using System.Web.Services;
using System.Web;
using System.Xml.Linq;
using System.Web.SessionState;
using System.Reflection;
#endregion

public partial class website_inestorlogin : System.Web.UI.Page
{

    #region Variables
    LoginBusinessLayer objService = new LoginBusinessLayer();
    LoginDetails objLogin = new LoginDetails();
    List<LoginDetails> objloginlist = new List<LoginDetails>();
    List<LoginDetails> objloginlistSSO = new List<LoginDetails>();
    List<LoginDetails> objlogindetailSSO = new List<LoginDetails>();
    string strUserId, strPassword, strRandomPassword;
    private static website_inestorlogin m_Singleton = new website_inestorlogin();
    InvestorBusinessLayer objRegInvService = new InvestorBusinessLayer();
    InvestorDetails objInvDet = new InvestorDetails();
    InvestorRegistration objRegService = new InvestorRegistration();
    string strssomsg = "";
    string strprojname = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    #endregion

    #region Page_load
    protected void Page_Load(object sender, EventArgs e)
    {
        //// var QdId= Request.QueryString["Id"];   
        //this.Session["CaptchaImageText"] = GenerateRandomCode();
        //imgCap.Src = "cap.aspx?time=" + DateTime.Now.ToLongTimeString();
        String originalPathSSO = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        if (originalPathSSO.Contains("?param"))
        {
            if (Request.QueryString["param"].ToString() != "" || Request.QueryString["param"].ToString() == null)
            {
                Response.Redirect("SSOValidation.ashx?param=" + Request.QueryString["param"].ToString(), true);
            }
        } 
        
    }
    #endregion
    //Added by Sanghamitra Samal on 2-Sep-2017 for Refresh the Captcha Value
    //Web Method to Refresh the Captcha Value
    [WebMethod]
    public static string RefrshCaptcha()
    {
        return m_Singleton.GenerateRandomCode();
    }

    #region Button_Click
    protected void Button1_Click(object sender, EventArgs e)
    {
        //this.Session["CaptchaImageText"] = GenerateRandomCode();
        //imgCap.Src = "cap.aspx?time=" + DateTime.Now.ToLongTimeString();
        //if (Session["CaptchaImageText"] != null)
        //{
        //    //txtusr.Text = Session["CaptchaImageText"].ToString();
        //    HiddenField1.Value = Session["CaptchaImageText"].ToString();
        //}
    }
    private void GenerateNewSessionId()
    {
        //Get the unique session identifier for the session.
        System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
        //Get the old session ID (Current)
        string oldId = manager.GetSessionID(Context);
        //Create new session Id
        string newId = manager.CreateSessionID(Context);
        bool isAdd = false, isRedir = false;
        //Save the newly created session identifier to the HTTP response
        manager.SaveSessionID(Context, newId, out isRedir, out isAdd);
        //Gets the object for current HTTP request
        HttpApplication ctx = (HttpApplication)HttpContext.Current.ApplicationInstance;
        //Gets the collection of modules for the current application
        HttpModuleCollection mods = ctx.Modules;
        //Get the System.Web.IHttpModule object with the specified name from the System.Web.HttpModuleCollection
        System.Web.SessionState.SessionStateModule ssm = (SessionStateModule)mods.Get("Session");
        //Searche for the fields defined for the current System.Type, using the specified binding constraints
        System.Reflection.FieldInfo[] fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        SessionStateStoreProviderBase store = null;
        System.Reflection.FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;

        //Override the session field value with the newly created value.
        foreach (System.Reflection.FieldInfo field in fields)
        {
            if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
            if (field.Name.Equals("_rqId")) rqIdField = field;
            if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
            if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
        }
        object lockId = rqLockIdField.GetValue(ssm);
        if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(Context, oldId, lockId);
        rqStateNotFoundField.SetValue(ssm, true);
        rqIdField.SetValue(ssm, newId);
    }
    /// <summary>
    /// Login Button Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_click(object sender, EventArgs e)
    {
        //if (HiddenField1.Value!= captchaID.Text)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Invalid Captcha!');", true);
        //    return;
        //}
        //else
        //{
         Captcha1.ValidateCaptcha(captchaID.Text.Trim());
        Boolean bt = Captcha1.UserValidated;
        //e.IsValid = Captcha1.UserValidated;
        //if (e.IsValid)
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Captcha!');", true);
        //}
        if (bt == false)
        {
            captchaID.Text = "";
            captchaID.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid Captcha !',  '" + strprojname + "'); </script>", false);           
            return;
        }
        else
        {
            GenerateNewSessionId();
            string strClientIp = null;
            strClientIp = Request.ServerVariables["HTTP_X_FOWARDED_FOR"];
            if (strClientIp == string.Empty || strClientIp == null)
            {
                strClientIp = Request.ServerVariables["REMOTE_ADDR"];
            }


            HttpBrowserCapabilities browserCapabilities = Request.Browser;

            //getting client browser name
            string browserName = browserCapabilities.Browser;
            //if (chkSSO.Checked == true)
            //{
               
            //}
            //else{

                 objloginlist = objService.SWPLogin("LOG", txtuserID.Text, CommonHelperCls.GenerateHash(txtPassword.Text)).ToList();

            if (txtPassword.Text != "")
            {
                if (objloginlist.Count > 0)
                {
                    foreach (LoginDetails objLogin in objloginlist)
                    {
                        strUserId = objLogin.strUserID;
                        strPassword = objLogin.strPassword;
                        Session["UserId"] = strUserId;
                        Session["InvestorId"] = objLogin.intInvestorId;
                        Session["UserName"] = objLogin.strUserName;
                        Session["RegDate"] = objLogin.strRegDate;
                        Session["LastLoginTime"] = string.Format("{0:hh:mm:ss tt}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                        Session["GSTIN"] = objLogin.strGSTIN;
                        Session["UID"] = objLogin.strUID;
                        Session["LogId"] = "NA";
                        Session["SSOUserId"] = 0;
                        Session["IndustryName"] = objLogin.strInvName;

                    }
                                    try
                                    {
                                        SSOService.ValidateService objSSO = new SSOService.ValidateService();
                                        strssomsg = objSSO.CheckAuthentication(txtuserID.Text.Trim().ToString(), CommonHelperCls.GenerateHash(txtPassword.Text), objSSO.URLEncryption(System.Configuration.ConfigurationManager.AppSettings["SSOKey"].ToString()), strClientIp, browserName);
                                    }
                                    catch(Exception ex)
                                    {
                       
                                    }
                        if (strssomsg.Contains(",") == true)
                    {
                        if (strssomsg.Split(',')[0].ToString() != "5" && strssomsg.Split(',')[0].ToString() != "4" )                        
                        {
                            string[] arrout = strssomsg.Split(',');
                            if (arrout.Length > 2)
                            {
                                Session["LogId"] = arrout[4].ToString();
                                Session["SSOUserId"] = arrout[5].ToString();
                                //if (arrout[3].ToString() == "L")
                                //{
                                //    Response.Redirect("SSOlogout.aspx");
                                //}
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('You do not have any permission !');", true);
                            ClearData();
                        }
                    }
                    ClearData();
                    if (Request.QueryString["returnurl"] != null)
                    {
                        Response.Redirect(Request.QueryString["returnurl"].ToString());
                    }
                    else
                    {
                        Response.Redirect("Proposals.aspx");
                    
                    }
                    //String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                    //if (originalPath.Contains("?id"))
                    //{
                    //    string strval = Request.QueryString["id"].ToString();
                    //    if (strval != "")
                    //    {
                    //        if (Request.QueryString["id"].ToString() == "1")
                    //        {
                    //            Response.Redirect("Proposals.aspx");
                    //        }
                    //        else if (Request.QueryString["id"].ToString() == "2")
                    //        {
                    //            Response.Redirect("Proposals.aspx");
                    //        }
                    //        else if (Request.QueryString["id"].ToString() == "3")
                    //        {
                    //            Response.Redirect("incentives/incentiveoffered.aspx");
                    //        }
                    //        else
                    //        {
                    //            Response.Redirect("Proposals.aspx");
                    //        }
                    //    }
                    //}
                    //else if (originalPath.Contains("?serviceid"))
                    //{
                    //    Response.Redirect("ServiceDetails.aspx?Srvcid=" + Request.QueryString["serviceid"].ToString(), true);
                    //}


                    //else
                    //{
                    //    Response.Redirect("Proposals.aspx");
                    //}
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Invalid User Id or Password !');", true);
                    //ClearData();
                    SSOLogin();
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Invalid User Id or Password !');", true);
                //ClearData();

                SSOLogin();
            }
                }
      //  }
        //Session["UserId"] = "1";
        //Session["UserName"] = "Sandeep Sharma";
        //Session["Password"] = "Password";

        //if (Request.QueryString["Id"] == "1")
        //{
        //    Response.Redirect("PromoterDetails.aspx");
        //}
        //else
        //{
        //    Response.Redirect("InvesterProfile.aspx");

        //}
    }

    void SSOLogin()
    {

        string strClientIp = null;
        strClientIp = Request.ServerVariables["HTTP_X_FOWARDED_FOR"];
        if (strClientIp == string.Empty || strClientIp == null)
        {
            strClientIp = Request.ServerVariables["REMOTE_ADDR"];
        }


        HttpBrowserCapabilities browserCapabilities = Request.Browser;

        //getting client browser name
        string browserName = browserCapabilities.Browser;
        String characters = "abcdefghijklmnopqrstuvwxyzABCDEFHIJKLMNOPQRSTUVWXYZ";
        Random random = new Random();
        string s = "";
        int unique;
        int i = 0;
        while (i < 10)
        {

            if (i % 2 == 0)
            {
                s += random.Next(10).ToString();
            }
            else
            {
                unique = random.Next(52);
                if (unique < characters.Length)
                {
                    s = String.Concat(s, characters[unique]);
                }
            }
            i += 1;
        }

        string EncryptPass = CommonHelperCls.GenerateHash(txtPassword.Text) + s;


        try
        {
            SSOService.ValidateService objSSO = new SSOService.ValidateService();
            strssomsg = objSSO.LoginUserAuthentication(txtuserID.Text.Trim().ToString(), CommonHelperCls.GenerateHash(EncryptPass), s, objSSO.URLEncryption(System.Configuration.ConfigurationManager.AppSettings["SSOKey"].ToString()), strClientIp, browserName);
        }
        catch (Exception ex)
        {
          //  throw ex;
        }
        if (!string.IsNullOrEmpty(strssomsg))
        {

            var xdoc = XDocument.Parse(strssomsg);
            var items = xdoc.Descendants("RESULT").FirstOrDefault().Value;

            if (items == "1")
            {
                Session["UID"] = xdoc.Descendants("VCHUSERUNIQUEID").FirstOrDefault().Value;
                Session["LogId"] = xdoc.Descendants("LOGID").FirstOrDefault().Value;
                Session["SSOUserId"] = xdoc.Descendants("INTUSERID").FirstOrDefault().Value;

                objloginlistSSO = objService.SWPLogin("S", Session["UID"].ToString(), "").ToList();
                if (objloginlistSSO.Count <= 0)
                {
                    InvestorInfo objInvestorInfo = new InvestorInfo();
                    objInvestorInfo.VCH_CONTACT_FIRSTNAME = xdoc.Descendants("VCHPROMOTERNAME").FirstOrDefault().Value;
                    //objInvestorInfo.VCH_CONTACT_MIDDLENAME = xdoc.Descendants("VCHPROMOTERNAME").FirstOrDefault().Value.Split(' ')[1];
                    //objInvestorInfo.VCH_CONTACT_LASTNAME = xdoc.Descendants("VCHPROMOTERNAME").FirstOrDefault().Value.Split(' ')[2];
                    objInvestorInfo.VCH_EMAIL = xdoc.Descendants("VCHEMAILID").FirstOrDefault().Value;
                    objInvestorInfo.VCH_INV_NAME = xdoc.Descendants("VCHINDUSTRYNAME").FirstOrDefault().Value;
                    objInvestorInfo.VCH_INV_PASSWORD = xdoc.Descendants("VCHPASSWORD").FirstOrDefault().Value;
                    objInvestorInfo.VCH_INV_USERID = xdoc.Descendants("VCHEMAILID").FirstOrDefault().Value;
                    objInvestorInfo.VCH_OFF_MOBILE = xdoc.Descendants("MOBILENO").FirstOrDefault().Value;
                    objInvestorInfo.VCH_SEC_ANSWER = "";
                    objInvestorInfo.VCH_ADDRESS = xdoc.Descendants("VCHCORADDRESS").FirstOrDefault().Value;
                    objInvestorInfo.INT_COUNTRY = 1;
                    objInvestorInfo.INT_SALUTATION = 0;
                    objInvestorInfo.INT_SEC_QUES = 0;
                    objInvestorInfo.INT_SMS_STATUS = 0;
                    objInvestorInfo.INT_EMAIL_STATUS = 0;
                    objInvestorInfo.INT_TERM_STATUS = 0;
                    objInvestorInfo.VCH_INDUSTRY_NAME = "0";
                    objInvestorInfo.INT_INDUSTRY_GROUP_ID = 0;
                    objInvestorInfo.VCH_GSTIN = "";
                    objInvestorInfo.INT_DISTRICT = Convert.ToInt32(xdoc.Descendants("INTDISTRICT").FirstOrDefault().Value);
                    objInvestorInfo.INT_BLOCK = Convert.ToInt32(xdoc.Descendants("INTBLOCK").FirstOrDefault().Value);
                    objInvestorInfo.INT_CATEGORY = Convert.ToInt32(xdoc.Descendants("INTINDUSTRYCATEGORY").FirstOrDefault().Value);
                    objInvestorInfo.INT_SECTOR = Convert.ToInt32(xdoc.Descendants("INTSECTOR").FirstOrDefault().Value);
                    objInvestorInfo.INT_SUBSECTOR = Convert.ToInt32(xdoc.Descendants("INTSUBSECTOR").FirstOrDefault().Value);
                    objInvestorInfo.DEC_INVESTAMOUNT = 0;
                    objInvestorInfo.VCH_SITELOCATION = xdoc.Descendants("VCHADDRESS").FirstOrDefault().Value;
                    objInvestorInfo.VCH_UID = xdoc.Descendants("VCHUSERUNIQUEID").FirstOrDefault().Value;
                    objInvestorInfo.VCH_GSTIN = xdoc.Descendants("VCHPANNO").FirstOrDefault().Value;
                    objInvestorInfo.VCH_INDUSTRY_CODE = xdoc.Descendants("VCHINDUSTRYCODE").FirstOrDefault().Value;
                   // VCHINDUSTRYCODE
                    string strResult = objRegInvService.InvestorRegistration(objInvestorInfo, "SSOI");
                }

                objlogindetailSSO = objService.SWPLogin("S", Session["UID"].ToString(), "").ToList();

                if (objlogindetailSSO.Count > 0)
                {
                    foreach (LoginDetails objLoginSSO in objlogindetailSSO)
                    {
                        strUserId = objLoginSSO.strUserID;
                        strPassword = objLoginSSO.strPassword;
                        Session["UserId"] = strUserId;
                        Session["InvestorId"] = objLoginSSO.intInvestorId;
                        Session["UserName"] = objLoginSSO.strUserName;
                        Session["RegDate"] = objLoginSSO.strRegDate;
                        Session["LastLoginTime"] = string.Format("{0:hh:mm:ss tt}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                        Session["GSTIN"] = objLoginSSO.strGSTIN;
                        Session["IndustryName"] = objLoginSSO.strInvName;
                        //if (xdoc.Descendants("LOGSTATUS").FirstOrDefault().Value == "L")
                        //{
                        //    Response.Redirect("SSOlogout.aspx");
                        //}
                    }
                }

                //Response.Redirect("Proposals.aspx");

                if (Request.QueryString["returnurl"] != null)
                {
                    Response.Redirect(Request.QueryString["returnurl"].ToString());
                }
                else
                {
                    Response.Redirect("Proposals.aspx");

                }
                //String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                //if (originalPath.Contains("?serviceid"))
                //{
                //    Response.Redirect("ServiceDetails.aspx?Srvcid=" + Request.QueryString["serviceid"].ToString(), true);
                //}
                //else
                //{
                //    Response.Redirect("Proposals.aspx");
                //}
            }
            else if (items == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Invalid User Id !');", true);
                ClearData();
            }
            else if (items == "3")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Password does not Match !');", true);
                ClearData();
            }
            else if (items == "5")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('You have not permission to access the Application !');", true);
                ClearData();
            }
        }

    }
    /// <summary>
    /// Forgot Password Button Click Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            objLogin.strAction = "F";
            objLogin.strUserID = txtuserID.Text.Trim();
            objLogin.strEmail = txtEmailID.Text.Trim();
            objloginlist = objService.getUserDetails(objLogin).ToList();

            dt = CommonHelperCls.ConvertToDataTable(objloginlist);
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>jAlert('Invalid User Name');</script>", false);
                return;
            }
            else if (dt.Rows[0]["strEmail"].ToString() == txtEmailID.Text.Trim())
            {
                strPassword = DynamicPwd();
                strRandomPassword = CommonHelperCls.GenerateHash(strPassword);
                updatePassword(txtuserID.Text, txtEmailID.Text, "amit.sahoo@csmpl.com", strRandomPassword, strPassword);
                ClearData();
            }

            else
            {
                //  ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Please enter');</script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region CommonFunction
    protected void updatePassword(string userID, string toMail, string fromMail, string password, string orgPassword)
    {
        string result = "";
        try
        {
            objLogin.strAction = "U";
            objLogin.strUserID = txtuserID.Text.Trim();
            objLogin.strEmail = txtEmailID.Text.Trim();
            objLogin.strPassword = password;
            result = objService.UpdatePassword(objLogin);
            sendMail(userID, toMail, fromMail, orgPassword);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    protected void sendMail(string userID, string toMail, string fromMail, string password)
    {
        try
        {
            string email = toMail;
            MailMessage objResult = new MailMessage();
            MailAddress fromAddress = new MailAddress(fromMail);
            SmtpClient smtp = new SmtpClient();
            StringBuilder sb = new StringBuilder();
            string strBody = "";

            objResult.IsBodyHtml = true;
            objResult.From = fromAddress;
            objResult.To.Add(toMail);
            objResult.Subject = "Your Password for Login to Investor Login";
            objResult.CC.Add("amit.sahoo@csmpl.com");
            objResult.CC.Add("amit.sahoo@csmpl.com");
            objResult.Bcc.Add("amit.sahoo@csmpl.com");
            strBody = "<html><body><table><tr><td>Your password to login to Investor Login is " + password + "</td></tr></table>";
            sb.Append(strBody);


            strBody += "<table><tr><td colspan='2'>Regards,</td></tr><tr><td>Administrator of SWP</td></tr></table></body></html>";
            objResult.Body = strBody;


            smtp.Host = "localhost";
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = true;
            smtp.Send(objResult);
            ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>jAlert('Password has been sent to your mail. Please check');</script>", false);

        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('" + ex.Message.ToString() + "');</script>", false);
            ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>jAlert('Password Changed and  has been sent to your mail id');</script>", false);
            return;
        }
    }
    public string DynamicPwd()
    {
        Random random = new Random();
        StringBuilder strbuilder = new StringBuilder();
        char ch = '\0';
        int intCounter = 0;
        for (intCounter = 0; intCounter <= 5; intCounter++)
        {
            ch = Convert.ToChar(random.Next(65, 90));
            //Generate a random character between A To Z
            strbuilder.Append(ch);
        }
        object strRandomPwd = Convert.ToString(strbuilder);
        //Store the radom  pwd
        return Convert.ToString(strRandomPwd);

    }
    private Random random = new Random();
    private string GenerateRandomCode()
    {
        string s = "";
        for (int i = 0; i < 6; i++)
            s = String.Concat(s, this.random.Next(10).ToString());
        this.Session["CaptchaImageText"] = s;
        return s;
    }

    public void ClearData()
    {
        txtuserID.Text = "";
        txtPassword.Text = "";
        //txtEmailID.Text = "";
        captchaID.Text = "";
    }
    #endregion
}