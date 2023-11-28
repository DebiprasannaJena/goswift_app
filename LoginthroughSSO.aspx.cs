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

public partial class LoginthroughSSO : System.Web.UI.Page
{
    #region Variables
    LoginBusinessLayer objService = new LoginBusinessLayer();
   
    
    List<LoginDetails> objloginlistSSO = new List<LoginDetails>();
    List<LoginDetails> objlogindetailSSO = new List<LoginDetails>();
    string strUserId , strPassword;
    InvestorBusinessLayer objRegInvService = new InvestorBusinessLayer();
    
    string strssomsg = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            String originalPathSSO = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            if (originalPathSSO.Contains("?param"))
            {
                if (Request.QueryString["param"].ToString() != "" || Request.QueryString["param"].ToString() == null)
                {
                    Response.Redirect("SSOValidation.ashx?param=" + Request.QueryString["param"].ToString(), true);
                }
            }
        }
        catch(Exception ex)
        {
            throw ex.InnerException;
        }
    }

       
    /// <summary>
    /// Login Button Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogin_click(object sender, EventArgs e)
    {
        try
        {
            Captcha1.ValidateCaptcha(captchaID.Text.Trim());
            Boolean bt = Captcha1.UserValidated;

            if (!bt)
            {
                captchaID.Text = "";
                captchaID.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid Captcha !', 'SWP'); </script>", false);
                return;
            }
            else
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
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Some error occured !', 'SWP'); </script>", false);
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
                            objInvestorInfo.VCH_CONTACT_FIRSTNAME = xdoc.Descendants("VCHPROMOTERNAME").FirstOrDefault().Value.Split(' ')[0];
                            objInvestorInfo.VCH_CONTACT_MIDDLENAME = xdoc.Descendants("VCHPROMOTERNAME").FirstOrDefault().Value.Split(' ')[1];
                            objInvestorInfo.VCH_CONTACT_LASTNAME = xdoc.Descendants("VCHPROMOTERNAME").FirstOrDefault().Value.Split(' ')[2];
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

                            }
                        }

                        Response.Redirect("InvesterDashboard.aspx");
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('You do not have permission to access the Application !');", true);
                        ClearData();
                    }
                }

            }
        }
        catch(Exception ex)
        {
            throw ex.InnerException;
        }
              
    }
     


    public void ClearData()
    {
        txtuserID.Text = "";
        txtPassword.Text = "";
       
        captchaID.Text = "";
    }
    #endregion
}