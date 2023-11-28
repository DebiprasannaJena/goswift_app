//******************************************************************************************************************
// File Name             :   SSOLoginNSWS.aspx.cs
// Description           :   This page is used as an intermediate page between GOSWIFT and NSWS for Single Sign On (SSO). [ NSWS- Nationa Single Window System]
// Created by            :   Sushant Jena
// Created on            :   01-Apr-2021
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DWHServiceReference;
using System.Configuration;
using EntityLayer.Login;
using BusinessLogicLayer.Login;
using System.Data;
using System.Security.Cryptography;
using System.IO;

public partial class SSOLoginNSWS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /*------------------------------------------------------------------------------------------------------------------*/
        /// This page is acting as an intermidate page between GOSWIFT and NSWS for single Sign On.
        /// Here,Instead of Username and Password,the Investor SWS Id and PAN will be used for login authentication.
        /// For logging into the GOSWIFT portal, the investor must have to login in NSWS portal first and then consume GOSWIFT service to generate a dynamic URL and then redirect to this page using that dynamic URL.
        /// Once the credential found in the Query-String, is validated by DWH, then the session will be created and the user will be allowed for accessing the internal pages of GOSWIFT portal.
        /// The work process is like below:
        /// (1). Get the query string value from the URL and decrypt it. After decryption, find the PAN,Investor SWS Id and URL expiry time (10 Min).
        /// (2). Authenticate the above PAN and Investor SWS Id using the DWH API method (NswsLoginAuthentication).
        /// (3). If successfully authenticated from DWH,then get the Investor id and other required details from GOSWIFT portal using the unique key returned from DWH.
        /// (4). Generate the required session and redirect to the GOSWIFT proposal page (Home page for GOSWIFT).
        /*------------------------------------------------------------------------------------------------------------------*/

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

        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["nparam"]))
            {
                /*------------------------------------------------------------------------------------------------------------------*/
                /// Get the query string value from URL
                /*------------------------------------------------------------------------------------------------------------------*/
                string strQSVal = Request.QueryString["nparam"];

                /*------------------------------------------------------------------------------------------------------------------*/
                /// Decrypt the above query string value using the same key,which was used in the case of Encryption.
                /// N.B.:- Encrypted url is generated through API (Check API method "NSWSLoginAuthentication" for service "NSWSService.svc" )
                /// If the url get tampered or decryption key is wrong, then it will throw an exception during decryption.
                /// Here for each exception it will throw a text as "Invalid" and the actual log will be recorded in error log.See the "DecryptQueryString" method.
                /*------------------------------------------------------------------------------------------------------------------*/
                string strDecryptVal = DecryptQueryString(strQSVal.Replace(" ", "+"), "gR35GrvT");
                if (strDecryptVal == "Invalid") //// Url Tampered
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>BAD URL !</strong>');</script>", false);
                    return;
                }
                else
                {
                    /*------------------------------------------------------------------------------------------------------------------*/
                    ///Split the decrypted value to find out PAN and Investor SWS Id
                    /*------------------------------------------------------------------------------------------------------------------*/
                    string[] arr = strDecryptVal.Split('|');
                    if (arr.Length != 2)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>Something went wrong,Please try again !</strong>');</script>", false);
                        return;
                    }
                    else if (arr.Length == 2)
                    {
                        string strQSPAN = Convert.ToString(arr[0]);
                        string strInvSWSId = Convert.ToString(arr[1]);
                    
                        /*------------------------------------------------------------------------------------------------------------------*/
                        ///Go for the Login process after verifying the legality of the above URL.
                        ///Here the login validation will be carried out using PAN and Investor SWS Id (Got from NSWS during Registration) instead of Login Id and Password.
                        /*------------------------------------------------------------------------------------------------------------------*/

                        ///DWH Service Initialization
                        DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                        List<DWH_Model> objList = new List<DWH_Model>();

                        ///Generate Encryption Key (Security key to access Data Warehouse service methods)   
                        string strDWHSecurityKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                        string strAccessKey = objSrvRef.KeyEncryption(strDWHSecurityKey);

                        ///Login Process
                        string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string strBrowserType = Request.Browser.Type;                        

                        objList = objSrvRef.NswsLoginAuthentication(strQSPAN, strInvSWSId, strAccessKey, strIP, strBrowserType).ToList();
                        if (objList.Count > 0)
                        {
                            ///Write
                            Util.LogRequestResponse("NSWSLogin", "NSWS_SSO_LOGIN", "[REQUEST_TO_DWH]:- " + strQSPAN + "-" + strInvSWSId + "-" + strAccessKey + "-" + strIP + "-" + strBrowserType);

                            string strReturnVal = objList[0].MSGOUT.ToString();
                            if (strReturnVal == "11") ////Invalid Credential
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>You are not authorized to access this application !</strong>');</script>", false);
                                return;
                            }
                            else if (strReturnVal == "10") /////Login Success
                            {
                                /*---------------------------------------------------------------*/
                                ///Get retured values from DWH after successfully validating login credentials
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

                                /*---------------------------------------------------------------*/
                                /// If login success then get investor id from GOSWIFT database by matching with unique id of Data Warehouse.
                                /*---------------------------------------------------------------*/
                                LoginDetails objLogindt = new LoginDetails();
                                LoginBusinessLayer objBAL = new LoginBusinessLayer();

                                objLogindt.strAction = "V2";
                                objLogindt.strUniqueId = strUniqueKey;

                                DataTable dt = new DataTable();
                                dt = objBAL.viewInvestorDetails(objLogindt);
                                if (dt.Rows.Count > 0)
                                {
                                    /*---------------------------------------------------------------*/
                                    ///If unique id matched then assign session and redirect to respective home page.                        
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
                                    Session["DWHUserId"] = strSSOUserId.ToString();
                                    Session["SkipCount"] = 0;
                                    Session["PAN"] = strPAN;
                                    Session["EINIEM"] = strEINIEM;
                                    Session["NswsInvSwsId"] = strInvSWSId;
                                    Session["IndustryType"] = "1";///Industry

                                    Session["brs"] = s;

                                    /*---------------------------------------------------------------*/
                                    ///Get redirect url from return result and redirect to that page.
                                    /*---------------------------------------------------------------*/
                                    string strRedirectUrl = objList[0].VCHREDIRECTURL.ToString();

                                    string strParameters = strUniqueKey + "," + intLogId.ToString() + "," + strSSOUserId.ToString();
                                    strParameters = objSrvRef.KeyEncryption(strParameters);
                                    Response.Redirect(strRedirectUrl + "?strUniqueKey=" + strParameters, false);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>Unique key mismatch,Contact administrator !</strong>');</script>", false);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>Oops ! Something went wrong,Please try again !</strong>');</script>", false);
                            return;
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "NSWSLogin");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>Oops ! Something went wrong,Please try again later !</strong>');</script>", false);
        }
    }

    private byte[] key = { };
    private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
    private string DecryptQueryString(string stringToDecrypt, string sEncryptionKey)
    {
        byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "NSWSLogin");
            return "Invalid";
        }
    }
}