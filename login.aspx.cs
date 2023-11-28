using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Data;
using System.Text;
using BusinessLogicLayer.Login;
using EntityLayer.Login;
using System.Web.Services;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using DWHServiceReference;
using RestSharp;
using System.Net;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;

public partial class Login : System.Web.UI.Page
{
    #region Variables

    ////Get Project Name    
    readonly string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    string strDWHSecurityKey = "";
    string DeptId = ""; //ADDED BY MANOJ KUMAR BEHERA
    string Type = "";
    
    string strRedirectUrl = "";
    #endregion

    #region PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string s = "Browser Capabilities\n"
            + "Type = " + browser.Type + "\n"
            + "Name = " + browser.Browser + "\n"
            + "Version = " + browser.Version + "\n"
            + "Major Version = " + browser.MajorVersion + "\n"
            + "Minor Version = " + browser.MinorVersion + "\n"
            + "Platform = " + browser.Platform + "\n"
            + "Is Beta = " + browser.Beta + "\n"
            + "Is Crawler = " + browser.Crawler + "\n"
            + "Is AOL = " + browser.AOL + "\n"
            + "Is Win16 = " + browser.Win16 + "\n"
            + "Is Win32 = " + browser.Win32 + "\n"
            + "Supports Frames = " + browser.Frames + "\n"
            + "Supports Tables = " + browser.Tables + "\n"
            + "Supports Cookies = " + browser.Cookies + "\n"
            + "Supports VBScript = " + browser.VBScript + "\n"
            + "Supports JavaScript = " + browser.EcmaScriptVersion.ToString() + "\n"
            + "Supports Java Applets = " + browser.JavaApplets + "\n"
            + "Supports ActiveX Controls = " + browser.ActiveXControls + "\n"
            + "Supports JavaScript Version = " + browser["JavaScriptVersion"] + "\n";

        Session["brs"] = s;

        this.Txt_User_Id.Focus();

        Page.Form.DefaultButton = Btn_Login.ClientID;
        if (!Page.IsPostBack)
        {
            Txt_Captcha.Attributes.Add("autocomplete", "off");
            Txt_User_Id.Attributes.Add("autocomplete", "off");
        }

        string originalPathSSO = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        if (originalPathSSO.Contains("?param") && (Request.QueryString["param"].ToString() != "" || Request.QueryString["param"].ToString() == null))
        {
            
                Response.Redirect("SSOValidation.ashx?param=" + Request.QueryString["param"].ToString(), true);
            
        }

        /*----------------------------------------------------------------------*/
        //CSR VALIDATION PAGE FOR RETURN DEPT ID ACCORDING TO SUGGEST BY TAPAN SIR AND DEVELOPED BY MANOJ KUMAR BEHEREA

        //BEGIN

        if (originalPathSSO.Contains("&DeptId") && (Request.QueryString["DeptId"].ToString() != "" || Request.QueryString["DeptId"].ToString() == null))
        {
            
                DeptId = Request.QueryString["DeptId"].ToString();
                Type = Request.QueryString["RType"].ToString();

            //Session["Type"] = Type;
            //Session["DeptId"] = DeptId;

        }

        //END

        /*----------------------------------------------------------------------*/

        /*----------------------------------------------------------------------*/
        /// If login requested from GOSWIFT application then find DWH security key from web config key value.
        /// If login requested other then GOSWIFT application then find DWH security key from Request QueryString and Decrypt the key.
        /*----------------------------------------------------------------------*/
        if (Request.QueryString["DWHSecurityKey"] != null)
        {
            DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
            strDWHSecurityKey = objSrvRef.KeyDecryption(Request.QueryString["DWHSecurityKey"].ToString().Replace(" ", "+"));
        }
        else
        {
            strDWHSecurityKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
        }

        if (ConfigurationManager.AppSettings["Notice"].ToString() == "On")
        {
            DynamicScrolingText();
        }
    }

    #endregion

    #region ButtonClick

    /////// Button Click Login
    protected void Btn_Login_Click(object sender, EventArgs e)
    {
        try
        {
            /*-----------------------------------------------------------------*/
            /// Validate Captcha Section
            /*-----------------------------------------------------------------*/
            #region ValidateCaptcha

            if ((Txt_Captcha.Text).Any(char.IsLower))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid Captcha !</strong>', '" + strProjName + "'); </script>", false);
                Txt_Captcha.Text = "";
                Txt_Captcha.Focus();
                return;
            }

            Captcha1.ValidateCaptcha(Txt_Captcha.Text.Trim());
            Boolean bt = Captcha1.UserValidated;
            if (!bt)
            {
                Txt_Captcha.Text = "";
                Txt_Captcha.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid Captcha !</strong>',  '" + strProjName + "'); </script>", false);
                return;
            }

            #endregion

            /*--------------------------------------------------------------------------------------------------*/
            /// If the user is an Industrial user then use DWH (Data Warehouse) for Single Sign On (SSO).
            /// If the user is a Non-Industrial user the use GOSWIFT database for login.
            /// Note that the INDUSTRIAL user will come under SSO process, whereas the NON-INDUSTRIAL user will not.
            /*--------------------------------------------------------------------------------------------------*/

            
           
            if (Rbl_Industry_type.SelectedItem.Value == "1")////Industry
                {
                //if (ConfigurationManager.AppSettings["DirectLogin"].ToString() == "Off")  //with use of DWH service Login
                //{
                    /*-----------------------------------------------------------------*/
                    /// Service Initialization
                    /*-----------------------------------------------------------------*/
                    DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                      

                        /*-----------------------------------------------------------------*/
                        /// Generate Encryption Key (Security key to access Data Warehouse service methods)   
                        /*-----------------------------------------------------------------*/
                        string strEncryptionKey = strDWHSecurityKey;
                        string strAccessKey = objSrvRef.KeyEncryption(strEncryptionKey);

                        /*-----------------------------------------------------------------*/
                        /// Login Process
                        /*-----------------------------------------------------------------*/
                        string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string strBrowserType = Request.Browser.Type;
                        string strPasswordEnc = CommonHelperCls.GenerateHash(Txt_Password.Text);
                     List<DWH_Model> objList = objSrvRef.LoginUserAuthentication(Txt_User_Id.Text, strPasswordEnc, strAccessKey, strIP, strBrowserType).ToList();
                        if (objList.Count > 0)
                        {
                            string strReturnVal = objList[0].MSGOUT.ToString();
                            if (strReturnVal == "11") //// INVALID_USER
                            {
                                 ///goswift
                                 ///approval status =1 msg wiat for approval
                                 ///approval status=3 invalid user id pwd

                                LoginDetails objLoginent = new LoginDetails();
                                LoginBusinessLayer objloginbal = new LoginBusinessLayer();
                                
                                objLoginent.strAction = "IU";
                                objLoginent.strUserID = Txt_User_Id.Text.ToString();
                                
                              
                               DataTable userdata = objloginbal.LoginGOSWIFT(objLoginent);
                                
                                if (userdata.Rows[0]["RETURN_STATUS"].ToString() == "1")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Your user id is pending for approval by the Dept to take action.The Dept will take action     within 24 to 48 hrs(official days) to activate the registration after which you will  be able to login into the portal !</strong>'); </script>", false);
                                    return;
                                }
                                else if (userdata.Rows[0]["RETURN_STATUS"].ToString() == "3")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid User id or Password. If you are a departmental user then you need to log in using     the     appropriate portal for Departmental log in !</strong>'); </script>", false);
                                    return;
                                }
                                
                            
                            }
                            else if (strReturnVal == "12" || strReturnVal == "14") //// APP_ID_MISMATCH   or  APPLICATION_UN_AUTHORIZED
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>You are not authorized to access this application !</strong>'); </script>", false);
                                return;
                            }
                            else if (strReturnVal == "13") //// PASSWORD_MISMATCH
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid User id or Password. If you are a departmental user then you need to log in using the     appropriate portal for Departmental log in !</strong>'); </script>", false);
                                return;
                            }
                            
                            else if (strReturnVal == "15")//// AUTHORIZATION_LOCKED
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Authorization locked !</strong>'); </script>", false);
                                return;
                            }
                            else if (strReturnVal == "10") ///// Login Success
                            {
                                /*---------------------------------------------------------------*/
                                /// Get retured values from DWH after successfully validating login credentials
                                /*---------------------------------------------------------------*/
                                string intUserLevel = objList[0].INTUSERLEVEL.ToString();

                                string[] strArrUniqueKey = new string[2];
                                strArrUniqueKey = objList[0].VCHUSERUNIQUEID.ToString().Split('~');
                                string strUniqueKey = "";
                                if (intUserLevel == "1")
                                {
                                    strUniqueKey = strArrUniqueKey[0].ToString(); //// Own Unique Id
                                }
                                else if (intUserLevel == "2")
                                {
                                    strUniqueKey = strArrUniqueKey[1].ToString(); //// Parent Unique Id
                                    Session["OwnUniqueId"] = strArrUniqueKey[0].ToString();
                                }

                                string strInvName = objList[0].VCHINDUSTRYNAME;
                                string strOldUserName = objList[0].VCHALIASUSERNAME;
                                string strNewUserName = objList[0].VCHUSERNAME;
                                int intLogId = Convert.ToInt32(objList[0].LogId);
                                string strPAN = objList[0].VCHPANNO.ToString();
                                string strEINIEM = objList[0].VCHEINIEM.ToString();
                                int strSSOUserId = objList[0].INTUSERID;
                               int intEntityType = Convert.ToInt32( objList[0].INTENTITYTYPE);  //add by anil 
                               string strCIN = objList[0].VCHCINNUMBER.ToString();  // add by anil

                        /*---------------------------------------------------------------*/
                        /// After successfully validating login credential from DWH, Get the investor details from the GOSWIFT database by matching with unique id returned from the Data Warehouse. 
                        /// These details will be assigned to the Session variable and will be used in subsequent pages.
                        /*---------------------------------------------------------------*/
                              LoginDetails objLogindt = new LoginDetails();
                                LoginBusinessLayer objBAL = new LoginBusinessLayer();

                                objLogindt.strAction = "V2";
                                objLogindt.strUniqueId = strUniqueKey;

                               
                               DataTable dt = objBAL.viewInvestorDetails(objLogindt);
                                if (dt.Rows.Count > 0)
                                {
                                    /*---------------------------------------------------------------*/
                                    /// If unique id matched then assign session and redirect to respective home page                        
                                    /*---------------------------------------------------------------*/
                                    Session["UserId"] = dt.Rows[0]["VCH_INV_USERID"].ToString();
                                    Session["InvestorId"] = dt.Rows[0]["INT_INVESTOR_ID"].ToString();
                                    Session["ParentId"] = dt.Rows[0]["INT_PARENT_ID"].ToString();

                                    Session["UserLevel"] = intUserLevel;
                                    Session["UID"] = strUniqueKey;
                                    Session["UserName"] = strNewUserName;
                                    Session["LastLoginTime"] = string.Format("{0:hh:mm:ss tt}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                                    Session["SSOUserId"] = strSSOUserId.ToString();
                                    Session["IndustryName"] = strInvName;
                                    Session["LogId"] = intLogId.ToString();

                                    //ADDED BY MANOJ KUMAR BEHERA
                                    Session["DWHUserId"] = strSSOUserId.ToString();
                                    Session["SkipCount"] = 0;
                                    //End OF NEW SESSION BY MANOJ KUMAR BEHERA

                                   
                                    
                                     
                                    Session["PAN"] = strPAN;
                                    Session["EINIEM"] = strEINIEM;
                                    Session["IndustryType"] = "1";///Industry
                                    Session["EntityType"] = intEntityType.ToString();
                                    Session["CINNUMBER"] = strCIN.ToString();

                            /*---------------------------------------------------------------*/
                            /// If either PAN or EIN/IEM number is not available, 
                            /// then prompt user to fill these details first and also redirect to update these details.
                            /*---------------------------------------------------------------*/
                            string strCheckPanEnableStatus = System.Configuration.ConfigurationManager.AppSettings["CheckPAN"].ToString();
                                    if (strCheckPanEnableStatus == "ON")
                                    {
                                        if ((strPAN == "" || strEINIEM == "") && intUserLevel == "1")
                                        {
                                            Session["userPan"] = dt.Rows[0]["VCH_INV_USERID"].ToString();
                                            string message = @"<strong>You do not have a valid PAN or EIN/IEM number updated in system.Please click the Ok button to update these details !</strong>";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + message + "','" + dt.Rows[0]["VCH_INV_USERID"].ToString() + "');</script>", false);
                                            return;
                                        }
                                       
                                
                               
                                    }
                                  
                                /*---------------------------------------------------------------*/
                                /// Get redirect url from return result and redirect to that page.
                                /*---------------------------------------------------------------*/
                                strRedirectUrl = objList[0].VCHREDIRECTURL.ToString();
                            Session["RedirectUrl"] = objList[0].VCHREDIRECTURL.ToString();
                            Util.LogRequestResponse("Login", "GetRedirectionUrlFromDWH", "[Url]:- " +strRedirectUrl);
                            string strParameters = strUniqueKey + "," + intLogId.ToString() + "," + strSSOUserId.ToString();
                            Util.LogRequestResponse("Login", "GenerateParameterForEncription", "[ActualParameter]:- " + strParameters);
                            strParameters = objSrvRef.KeyEncryption(strParameters);
                            Util.LogRequestResponse("Login", "GenerateAfterEncription", "[EncriptParameter]:- " + strParameters);
                            strParameters = strParameters.Replace("+", "%2B");
                           
                            Session["SkipParameters"] = strParameters;
                            Util.LogRequestResponse("Login", "GenerateAfterEncription", "[EncriptParameterRepalce+]:- " + strParameters);

                            /*---------------------------------------------------------------*/
                            /// Check alias name created by user or not
                            /*---------------------------------------------------------------*/
                            int intAliasNameCount = Convert.ToInt32(objList[0].INTALIASNAMECOUNT);
                                    if (intAliasNameCount == 0 && strOldUserName != "")
                                    {
                                        /*---------------------------------------------------------------*/
                                        /// Reset the alias user name to NULL in Industry DB as well as in GOSWIFT DB
                                        /*---------------------------------------------------------------*/
                                        /// In DWH
                                        DWH_Model objEnt = new DWH_Model();
                                        objEnt.VCHUSERUNIQUEID = strUniqueKey;
                                        string strResult1 = objSrvRef.ResetAliasUserName(objEnt, strAccessKey);

                                        /// In GO-SWIFT DB
                                        objLogindt.strAction = "RAN";
                                        objLogindt.intInvestorId = Convert.ToInt32(dt.Rows[0]["INT_INVESTOR_ID"]);
                                        string strResult2 = objBAL.resetAliasName(objLogindt);

                                        /// Alert old and new user id then redirect
                                        string message = @"<strong>Dear M/S " + strInvName + ",Your user name for GOSWIFT has been updated from <span style=color:red>" + strOldUserName + "</span> to <span style=color:red>" + strNewUserName + "</span>.Please log in using the new user name and create alternate user name with Edit Profile -> Create Alternate User Name.You can then log in using the   alternate   user name !</strong>";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect2('" + message + "','" + strRedirectUrl + "?strUniqueKey=" + strParameters + "');</script>", false);
                                    }
                                  //else if (intEntityType == 0 && strCIN == "")
                                  //      {
                                  //        BindInvestorInfo();
                                  //      }
                            
                                    else
                                    {
                                        if (DeptId != "")
                                        {
                                            Response.Redirect(strRedirectUrl + "?strUniqueKey=" + strParameters + "&DeptId=" + DeptId + "&RType=" + Type, false);
                                        }
                                        else
                                        {
                                             // txtCIN.Text = "";
                                             // ModalPopupExtender2.Hide();
                                    Response.Redirect(strRedirectUrl + "?strUniqueKey=" + strParameters, false);
                                        }

                                        //if (Convert.ToString(objList[0].INTAPPROVALLEVEL) == "2")////Final level approval done.
                                        //{
                                        //    //Response.Redirect(strRedirectUrl + "?strUniqueKey=" + strParameters, false);
                                        //    // CHANGES BY MANOJ KUMAR BEHERA FOR CSR PROJECT ID RETURN.
                                        //    if (DeptId != "")
                                        //    {
                                        //        Response.Redirect(strRedirectUrl + "?strUniqueKey=" + strParameters + "&DeptId=" + DeptId, false);
                                        //    }
                                        //    else
                                        //    {
                                        //        Response.Redirect(strRedirectUrl + "?strUniqueKey=" + strParameters, false);
                                        //    }


                                        //    if (DeptId != "")
                                        //    {
                                        //        Response.Redirect(strRedirectUrl + "?strUniqueKey=" + strParameters + "&DeptId=" + DeptId + "&RType=" + Type, false);
                                        //    }
                                        //    else
                                        //    {
                                        //        Response.Redirect(strRedirectUrl + "?strUniqueKey=" + strParameters, false);
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>User has not been approved yet, Please contact administrator !</strong>'); </   script>",  false);
                                        //    return;
                                        //}
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Unique key mismatch,Contact administrator !</strong>'); </script>", false);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong,Please try again !</strong>'); </script>", false);
                            return;
                        }

                    //}
                    //else //with out use of DWH service Login
                    //{
                    //    LoginDetails objLogindt = new LoginDetails();
                    //    objLogindt.strAction = "IUQ";
                    //    objLogindt.strUserID = Txt_User_Id.Text;
                    //    string strPasswordEnc = CommonHelperCls.GenerateHash(Txt_Password.Text);
                    //    objLogindt.strPassword = strPasswordEnc;

                    //    LoginBusinessLayer objBAL = new LoginBusinessLayer();
                    //    DataTable data = new DataTable();
                    //    data = objBAL.LoginGOSWIFT(objLogindt);
                    //    if (data.Rows.Count > 0)
                    //    {
                    //        string strReturnStatus = data.Rows[0]["RETURN_STATUS"].ToString();
                    //        if (strReturnStatus == "2") //SUCCESS LOGIN
                    //        {
                    //            /*---------------------------------------------------------------*/
                    //            /// Get retured values from DWH after successfully validating login credentials
                    //            /*---------------------------------------------------------------*/
                    //            string intUserLevel = data.Rows[0]["INT_USER_LEVEL"].ToString();

                    //            string[] strArrUniqueKey = new string[2];
                    //            strArrUniqueKey = data.Rows[0]["VCH_UNIQUEID"].ToString().Split('~');
                    //            string strUniqueKey = "";
                    //            if (intUserLevel == "1")
                    //            {
                    //                strUniqueKey = strArrUniqueKey[0].ToString(); //// Own Unique Id
                    //            }
                    //            else if (intUserLevel == "2")
                    //            {
                    //                strUniqueKey = strArrUniqueKey[1].ToString(); //// Parent Unique Id
                    //                Session["OwnUniqueId"] = strArrUniqueKey[0].ToString();
                    //            }

                    //            string strInvName = data.Rows[0]["VCH_INV_NAME"].ToString();
                    //            string strOldUserName = data.Rows[0]["VCH_INV_USERID_ALIAS"].ToString();
                    //            string strNewUserName = data.Rows[0]["VCH_INV_USERID"].ToString();
                    //            int intLogId = Convert.ToInt32(0);
                    //            string strPAN = data.Rows[0]["VCH_PAN"].ToString();
                    //            string strEINIEM = data.Rows[0]["VCH_EIN_IEM"].ToString();
                    //            int strSSOUserId = Convert.ToInt32(data.Rows[0]["INT_SSO_USER_ID"].ToString());


                    //            /*---------------------------------------------------------------*/
                    //            /// After successfully validating login credential from DWH, Get the investor details from the GOSWIFT database by matching with unique id returned from the Data Warehouse. 
                    //            /// These details will be assigned to the Session variable and will be used in subsequent pages.
                    //            /*---------------------------------------------------------------*/

                    //            LoginDetails objLogin = new LoginDetails();
                    //            objLogin.strAction = "V2";
                    //            objLogin.strUniqueId = strUniqueKey;

                    //            DataTable dt = new DataTable();
                    //            dt = objBAL.viewInvestorDetails(objLogin);
                    //            if (dt.Rows.Count > 0)
                    //            {
                    //                /*---------------------------------------------------------------*/
                    //                /// If unique id matched then assign session and redirect to respective home page                        
                    //                /*---------------------------------------------------------------*/
                    //                Session["UserId"] = dt.Rows[0]["VCH_INV_USERID"].ToString();
                    //                Session["InvestorId"] = dt.Rows[0]["INT_INVESTOR_ID"].ToString();
                    //                Session["ParentId"] = dt.Rows[0]["INT_PARENT_ID"].ToString();

                    //                Session["UserLevel"] = intUserLevel;
                    //                Session["UID"] = strUniqueKey;
                    //                Session["UserName"] = strNewUserName;
                    //                Session["LastLoginTime"] = string.Format("{0:hh:mm:ss tt}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                    //                Session["SSOUserId"] = strSSOUserId.ToString();
                    //                Session["IndustryName"] = strInvName;
                    //                Session["LogId"] = intLogId.ToString();

                    //                //ADDED BY MANOJ KUMAR BEHERA
                    //                Session["DWHUserId"] = strSSOUserId.ToString();
                    //                Session["SkipCount"] = 0;
                    //                //End OF NEW SESSION BY MANOJ KUMAR BEHERA

                    //                //Session["LogId"] = "NA";
                    //                //Session["RegDate"] = objList[0]..ToString();
                    //                //Session["GSTIN"] = objList[0].VCHTINNO.ToString(); 
                    //                Session["PAN"] = strPAN;
                    //                Session["EINIEM"] = strEINIEM;
                    //                Session["IndustryType"] = "1";///Industry

                    //                                              ///*---------------------------------------------------------------*/
                    //                                              /// After successfully login update login failed status (Reset login failed)
                    //                                              ///*---------------------------------------------------------------*/
                    //                //LoginDetails objLoginchk = new LoginDetails();
                    //                //objLoginchk.strAction = "ULogF";
                    //                //objLoginchk.strUserID = txtuserID.Text.ToString().Trim();
                    //                //objLoginchk.strlogtime = "0";
                    //                //string strcheck = objBAL.UpdateLoginFailedStatus(objLoginchk);

                    //                /*---------------------------------------------------------------*/
                    //                /// If either PAN or EIN/IEM number is not available, 
                    //                /// then prompt user to fill these details first and also redirect to update these details.
                    //                /*---------------------------------------------------------------*/
                    //                string strCheckPanEnableStatus = System.Configuration.ConfigurationManager.AppSettings["CheckPAN"].ToString();
                    //                if (strCheckPanEnableStatus == "ON")
                    //                {
                    //                    if ((strPAN == "" || strEINIEM == "") && intUserLevel == "1")
                    //                    {
                    //                        Session["userPan"] = dt.Rows[0]["VCH_INV_USERID"].ToString();
                    //                        string message = @"<strong>You do not have a valid PAN or EIN/IEM number updated in system.Please click the Ok button to update these details !</strong>";
                    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + message + "','" + dt.Rows[0]["VCH_INV_USERID"].ToString() + "');</script>", false);
                    //                        return;
                    //                    }
                    //                }


                    //                /*---------------------------------------------------------------*/
                    //                /// Get redirect url from return result and redirect to that page.
                    //                /*---------------------------------------------------------------*/
                    //                // string strRedirectUrl = "proposals.aspx";


                    //                /*---------------------------------------------------------------*/
                    //                /// Check alias name created by user or not
                    //                /*---------------------------------------------------------------*/
                    //                int intAliasNameCount = Convert.ToInt32(data.Rows[0]["INT_ALIAS_NAME_COUNT"].ToString());

                    //                if (intAliasNameCount == 0 && strOldUserName != "")
                    //                {
                    //                    /// Due to DWH service down no alias name update occurs . 
                    //                }
                    //                else
                    //                {
                    //                    if (DeptId != "")
                    //                    {
                    //                        //  Response.Redirect(strRedirectUrl + "?strUniqueKey=" + strParameters + "&DeptId=" + DeptId + "&RType=" + Type, false);
                    //                    }
                    //                    else
                    //                    {
                    //                        Response.Redirect("Proposals.aspx", false);
                    //                    }
                    //                }



                    //            }

                    //        }
                    //        else if (strReturnStatus == "3")
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid user id or password !!</strong>'); </script>", false);
                    //            return;
                    //        }
                    //        else if (strReturnStatus == "4")
                    //        {
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Your login credential has not been approved. !!</strong>'); </script>", false);
                    //            return;
                    //        }
                    //    }
                    //}
                }
            else if (Rbl_Industry_type.SelectedItem.Value == "2")////Non Industry
                {
                    LoginDetails objLogindt = new LoginDetails();
                    LoginBusinessLayer objBAL = new LoginBusinessLayer();

                    objLogindt.strAction = "ILOGIN";
                    objLogindt.strUserID = Txt_User_Id.Text.ToString();
                    objLogindt.strPassword = CommonHelperCls.GenerateHash(Txt_Password.Text);

                    ///DQL Operation
                    DataTable dtloginfail = objBAL.LoginGOSWIFT(objLogindt);
                    if (dtloginfail.Rows.Count > 0)
                    {
                        string strReturnStatus = dtloginfail.Rows[0]["RETURN_STATUS"].ToString();
                        if (strReturnStatus == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Your account is blocked for 2 hours !!</strong>'); </script>", false);
                            return;
                        }
                        else if (strReturnStatus == "2")
                        {
                            /*---------------------------------------------------------------*/
                            /// If Login success then assign session and then proceed
                            /*---------------------------------------------------------------*/
                            Session["UserId"] = Convert.ToString(dtloginfail.Rows[0]["VCH_INV_USERID"]);
                            Session["InvestorId"] = Convert.ToString(dtloginfail.Rows[0]["INT_INVESTOR_ID"]);
                            Session["IndustryName"] = Convert.ToString(dtloginfail.Rows[0]["VCH_INV_NAME"]);
                            Session["IndustryType"] = "2"; ///Non-Industry  

                            //Session["UserName"] = Convert.ToString(dtloginfail.Rows[0]["USERNAME"]);
                            //Session["RegDate"] = Convert.ToString(dtloginfail.Rows[0]["REGDATE"]);
                            //Session["LastLoginTime"] = string.Format("{0:hh:mm:ss tt}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));                        
                            //Session["UID"] = Convert.ToString(dtloginfail.Rows[0]["VCH_UNIQUEID"]);
                            //Session["LogId"] = "NA";
                            //Session["SSOUserId"] = 0;
                            //string strPAN = Convert.ToString(dtloginfail.Rows[0]["VCH_PAN"]);                        
                            //string intUserLevel = Convert.ToString(dtloginfail.Rows[0]["INT_USER_LEVEL"]);
                            //Session["PAN"] = strPAN;

                            ClearData();

                            Response.Redirect("GrievanceNonIndustry.aspx", false);
                        }
                        else if (strReturnStatus == "3")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid user id or password !!</strong>'); </script>", false);
                            return;
                        }
                        else if (strReturnStatus == "4")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Your user id is pending for approval by the Dept to take action.The Dept will take action     within 24 to 48 hrs(official days) to activate the registration after which you will  be able to login into the portal !!</strong>'); </script>", false);
                            return;
                        }
                    }
                }

            
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorLogin");
        }
        
    }

    #endregion

    #region CommonFunction

    private void ClearData()
    {
        Txt_User_Id.Text = "";
        Txt_Password.Text = "";
        Txt_Captcha.Text = "";
    }

    #endregion

    #region For redirect NewIndustry Reg.
    protected void BtnIndustryReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("InvestorRegistrationUser.aspx");
    }
    #endregion

    #region anil sahoo

    #region For redirect NewNonIndustry Reg.
    protected void BtnNonIndustryReg_Click(object sender, EventArgs e)
    {
        ServiceModalPopup.Hide();
        ModalPopupExtender2.Show();

       
    }
    #endregion

    protected void LinkBtnRegistrationUser_Click(object sender, EventArgs e)
    {
        ServiceModalPopup.Show();
    }

    /// <summary>
    /// For Dynamic Scroling notifiaction 
    /// </summary>
    void DynamicScrolingText()
    {
        int? LOGIN_PAGE = null;
        string strOut = "";

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_AddNotice";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Action", "S");

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlDataReader sqlReader = cmd.ExecuteReader();

            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    strOut = sqlReader["VCH_NOTIFICATION"].ToString();
                    LOGIN_PAGE = Convert.ToInt32(sqlReader["INT_LOGIN_PAGE"].ToString());
                }

                if (LOGIN_PAGE == 0)
                {
                    string strHtmlText = "<marquee onmouseover='this.stop()' onmouseout='this.start()'> " + strOut + "</marquee>";

                    divScrollingText.Visible = true;
                    divScrollingText.InnerHtml = strHtmlText.ToString();
                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorLogin");
        }
    }

    #endregion



    protected void Linkclose_Click(object sender, EventArgs e)
    {
        Session["SkipCount"] = 1;
        ModalPopupExtender1.Hide();
       // Skip();
    }

   

    protected void btnUpdate_Click(object sender, EventArgs e)
    {


        try
        {
            /*-----------------------------------------------------------------*/
            /////// Service Initialization
            DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
            DWH_Model objEnt = new DWH_Model();

            objEnt.VCHUSERNAME = Session["UserId"].ToString();
            objEnt.INTENTITYTYPE = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue); //add by anil
            objEnt.VCHCINNUMBER = Convert.ToString(txtCIN.Text.Trim());// add by anil

            /*----------------------------------------------------------------*/
            /////// Generate Encryption Key (Security key to access Data Warehouse servce methods)
            string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
            string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

            /*-----------------------------------------------------------------*/
            /////// DML opertion through service
            string strReturnVal = objSrvRef.ProfileUpdateCINEntity(objEnt, strSecurityKey);
            if (strReturnVal != "")
            {
                if (strReturnVal == "5") ////// Success
                {
                    InvestorDetails objInvDet = new InvestorDetails();
                    InvestorBusinessLayer objService = new InvestorBusinessLayer();
                    objInvDet.strAction = "PU";
                    objInvDet.strUserID = Session["InvestorId"].ToString();
                    objInvDet.intEntitytype = Convert.ToInt32(DrpDwn_Entity_Type.SelectedValue);
                    objInvDet.strCINnumber = Convert.ToString(txtCIN.Text.Trim());
                    string strRetval = Convert.ToString(objService.InvestorData(objInvDet));
                    if (strRetval == "2")
                    {
                        Skip();
                    }
                    else if (strRetval == "3")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Internal server error,Please try after sometime !');", true);
                    }
                    else if (strRetval == "1")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('CIN numebr already exists !');", true);
                    }
                }
                else if (strReturnVal == "11")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid user id !</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }
                else if (strReturnVal == "4")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>CIN numebr already exists !</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }
            }


        }
        catch(Exception ex)
        {
            throw ex.InnerException;
        }
        
    }

    private void BindInvestorInfo()
    {
        FillEntityType();
        ModalPopupExtender1.Show();
    }

    public void FillEntityType()
    {
        try
        {
            string action = "";

            InvestorRegistration objRegService = new InvestorRegistration();
            action = "FE";
            DataTable dtentitytype = objRegService.BindDistrict(action);
            DrpDwn_Entity_Type.DataSource = dtentitytype;
            DrpDwn_Entity_Type.DataTextField = "vchEntityName";
            DrpDwn_Entity_Type.DataValueField = "intEntityCode";
            DrpDwn_Entity_Type.DataBind();
            DrpDwn_Entity_Type.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    private void Skip()
    {
        try
        {
            if (DeptId != "")
            {
                Response.Redirect(Session["RedirectUrl"].ToString() + "?strUniqueKey=" + Session["SkipParameters"].ToString() + "&DeptId=" + DeptId + "&RType=" + Type, false);
            }
            else
            {
                // txtCIN.Text = "";
                // ModalPopupExtender2.Hide();
                Response.Redirect(Session["RedirectUrl"].ToString() + "?strUniqueKey=" + Session["SkipParameters"].ToString(), false);
            }
        }
        catch(Exception ex)
        {
            throw ex.InnerException;
        }
    }




  
    protected void LinkclosePopup_Click(object sender, EventArgs e)
    {
        Session["SkipCount"] = 1;
        ModalPopupExtender1.Hide();
    }



    protected void btnHide_Click(object sender, EventArgs e)
    {
        Session["SkipCount"] = 1;
        ModalPopupExtender1.Hide();
        Skip();
    }
}