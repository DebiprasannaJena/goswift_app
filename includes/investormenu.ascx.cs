using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.HtmlControls;
using EntityLayer.Login;
using BusinessLogicLayer.Login;
using System.Data;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading;
using Spire.Pdf.Fields;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using Microsoft.Web.Services3.Addressing;
using Microsoft.Web.Services3.Referral;
using Microsoft.Web.Services3.Security;
using Spire.License;
using System.Activities.Expressions;
using System.Net.PeerToPeer.Collaboration;
using System.Runtime.Remoting.Contexts;
using Ionic.Zlib;
using System.Diagnostics;
using Microsoft.Web.Services3.Security.Utility;

public partial class includes_investormenu : System.Web.UI.UserControl
{
    string strUserId = "";
    string strUniqueId = "";

    readonly string strTokenUrl = ConfigurationManager.AppSettings["NswsRevTokenUrl"].ToString();
    readonly string strBrowserSessionUrl = ConfigurationManager.AppSettings["NswsRevBrowserSessionUrl"].ToString();
    readonly string strDefaultUserPwd = ConfigurationManager.AppSettings["NswsRevDefaultUserPwd"].ToString();
    readonly string strTokenClientId = ConfigurationManager.AppSettings["NswsRevTokenClientId"].ToString();
    readonly string strTokenClientSecret = ConfigurationManager.AppSettings["NswsRevTokenClientSecret"].ToString();
    readonly string strIntrospectUrl = ConfigurationManager.AppSettings["NswsRevIntrospectUrl"].ToString();
    readonly string strPreCheckApiUrl = ConfigurationManager.AppSettings["NswsRevPreCheckApiUrl"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null || Session["IndustryType"] == null)
        {
            Response.Redirect("~/LogOut.aspx", false);
        }

        if (Convert.ToString(Session["IndustryType"]) == "2") ///If It's a Non-Indsutry type then logout from the system.
        {
            Response.Redirect("~/LogOut.aspx", false);
        }

        strUserId = Convert.ToString(Session["UserId"]);
        strUniqueId = Convert.ToString(Session["UID"]);

        /*---------------------------------------------------------------------------------------------------*/

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

        //if (Session["brs"].ToString() != s)
        //{
        //    Response.Redirect("~/LogOut.aspx", false);
        //}       

        try
        {
            /*-----------------------------------------------------------*/
            /// Display Manage Menu If the Unit is Main Unit (Parent Unit)
            /*-----------------------------------------------------------*/
            if (Session["ParentId"] != null)
            {
                if (Convert.ToInt32(Session["ParentId"]) == 0 && Convert.ToInt32(Session["UserLevel"]) == 1)
                {
                    managemenu.Visible = true;
                }
                else
                {
                    managemenu.Visible = false;
                }
            }
            else
            {
                managemenu.Visible = false;
            }

            /*-----------------------------------------------------------*/
            ///Add other menus for accessing external portals.
            /*-----------------------------------------------------------*/
            othermenulist.Visible = false;

            DWHServiceReference.DWHServiceHostClient validate = new DWHServiceReference.DWHServiceHostClient();
            DWHServiceReference.Registration[] App;
            App = validate.GetOtherAppsForUser(validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]), Session["UID"].ToString(), "");

            if (App != null && App.Length != 0)
            {
                for (int i = 0; i < App.Length; i++)
                {
                    othermenulist.Visible = true;
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    OtherApps.Controls.Add(li);
                    HtmlGenericControl anchor = new HtmlGenericControl("a");
                    anchor.Attributes.Add("href", "AppRedirect.aspx?Key=" + App[i].AppKey + "&AppName=" + App[i].AppAlias + "");
                    anchor.Attributes.Add("target", "_blank");
                    anchor.InnerText = App[i].AppAlias;
                    li.Controls.Add(anchor);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OtherMenu");
        }
    }

    protected void LnkBtnNswsRedirect_Click(object sender, EventArgs e)
    {
        ModalPopupConsent.Show();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnConsentYes_Click(object sender, EventArgs e)
    {
        try
        {
            /*---------------------------------------------------------------------------------*/
            ///To log in to the NSWS portal via Single-Sign-On(SSO), follow below steps:
            ///(1)Get an access token from the Keycloak server using the OIDC (OpenID Connect) protocol.
            ///(2)Upon receiving the token, create a browser session by invoking the API located in the jar file within the provider folder of the Keycloak server.
            ///(3)Note that the jar file is supplied by the NSWS team.For any modifications or issues with this file, please contact the NSWS team.
            ///(4)Retrieve the cookies from the browser session API and assign them to the browser.
            ///(5)After successfully assigning the cookies to the browser, initiate the pre-check API.
            ///(6)Refer to the latest pre-check API documentation for comprehensive details regarding the functionalities of the pre-check API.
            /*---------------------------------------------------------------------------------*/

            Util.LogRequestResponse("SamlInvestorMenu", "BtnConsentYes_Click", "[ForUniqueId]:- " + strUniqueId + " [ForUserId]:- " + strUserId);


            /*---------------------------------------------------------------------------------*/
            ///Get User Information.
            /*---------------------------------------------------------------------------------*/
            LoginDetails objLogindt = new LoginDetails();
            LoginBusinessLayer objBAL = new LoginBusinessLayer();

            objLogindt.strAction = "IUP";
            objLogindt.strUniqueId = strUniqueId;
            System.Data.DataTable dt = objBAL.viewInvestorDetails(objLogindt);

            if (dt.Rows.Count <= 0)
            {
                LblMsg.InnerText = "Unable to fetch user information, Please try again.";
                ModalPopupConsent.Hide();
                ModalPopupAlert.Show();
                return;
            }

            string strPan = dt.Rows[0]["VCH_PAN"].ToString();
            string strEmail = dt.Rows[0]["VCH_EMAIL"].ToString();
            string strEntityType = dt.Rows[0]["INT_ENTITY_TYPE"].ToString();
            string strCinNumber = dt.Rows[0]["VCH_CIN_NUMBER"].ToString();
            string strLlpinNumber = dt.Rows[0]["VCH_LLPIN_NUMBER"].ToString();
            string strCompanyName = dt.Rows[0]["VCH_INV_NAME"].ToString();
            string strRegAddress1 = dt.Rows[0]["VCH_ADDRESS"].ToString();
            string strRegAddress2 = dt.Rows[0]["VCH_REG_ADDRESS_2"].ToString();
            string strRegCountry = dt.Rows[0]["INT_REG_COUNTRY"].ToString();
            string strRegState = dt.Rows[0]["VCH_REG_STATE"].ToString();
            string strRegCity = dt.Rows[0]["VCH_REG_CITY"].ToString();
            string strRegPin = dt.Rows[0]["VCH_REG_PIN"].ToString();
            string strSlAddress1 = dt.Rows[0]["VCH_SITELOCATION"].ToString();
            string strSlAddress2 = dt.Rows[0]["VCH_SL_ADDRESS_2"].ToString();
            string strSlCountry = dt.Rows[0]["INT_SL_COUNTRY"].ToString();
            string strSlState = dt.Rows[0]["VCH_SL_STATE"].ToString();
            string strSlCity = dt.Rows[0]["VCH_SL_CITY"].ToString();
            string strSlPin = dt.Rows[0]["VCH_SL_PIN"].ToString();

            /*---------------------------------------------------------------------------------*/
            ///Validate Inputs
            /*---------------------------------------------------------------------------------*/
            if (
                (
                strEntityType == "0"
                || strRegAddress1 == ""
                || strRegAddress2 == ""
                || strRegCountry == ""
                || strRegState == ""
                || strRegCity == ""
                || strRegPin == ""
                || strSlAddress1 == ""
                || strSlAddress2 == ""
                || strSlCountry == ""
                || strSlState == ""
                || strSlCity == ""
                || strSlPin == ""
                )
                 || (strEntityType == "1" && strCinNumber == "")
                 || (strEntityType == "2" && strLlpinNumber == "")
             )
            {
                ModalPopupConsent.Hide();
                ModalPopupValidation.Show();
                return;
            }

            /*---------------------------------------------------------------------------------*/
            ///If the access token is available, proceed to introspect the token and verify its active status.
            ///If the token is active, there's no necessity to generate a new access token. Proceed to create a browser session using the active token.
            ///if the token is either absent or inactive, generate a fresh token and then create a browser session.
            ///Following the completion of the above steps, initiate the pre-check API and subsequently redirect to the NSWS portal.               
            /*---------------------------------------------------------------------------------*/

            if (Session["NswsAccessToken"] != null)
            {
                string strAccessToken = Convert.ToString(Session["NswsAccessToken"]);

                /*---------------------------------------------------------------------------------*/
                ///Introspect Token.
                /*---------------------------------------------------------------------------------*/
                string[] ArrIntroResponse = IntrospectAccessToken(strAccessToken);

                if (ArrIntroResponse[0] == "1") ///Success
                {
                    string strActiveStatus = Convert.ToString(ArrIntroResponse[1]);
                    if (strActiveStatus.ToUpper() == "TRUE") ///If the token is still Active
                    {
                        /*---------------------------------------------------------------------------------*/
                        ///Generate browser-session for above user.
                        /*---------------------------------------------------------------------------------*/
                        string strBrowserResponse = GenerateBrowserSession(strAccessToken);
                        if (strBrowserResponse == "1")
                        {
                            /*---------------------------------------------------------------------------------*/
                            ///Initiate Pre-check API call
                            /*---------------------------------------------------------------------------------*/
                            InitiateNswsPreCheckApi(strPan, strEmail, strEntityType, strCinNumber, strLlpinNumber, strCompanyName);
                        }
                        else
                        {
                            LblMsg.InnerText = "Something went wrong, Please try again.";
                            ModalPopupConsent.Hide();
                            ModalPopupAlert.Show();
                        }
                    }
                    else if (strActiveStatus.ToUpper() == "FALSE") ///If the token is Inactive
                    {
                        /*---------------------------------------------------------------------------------*/
                        ///Generate Access Token
                        /*---------------------------------------------------------------------------------*/
                        string[] arrTokenResponse = GenerateAccessToken();
                        if (Convert.ToString(arrTokenResponse[0]) == "1")
                        {
                            strAccessToken = Convert.ToString(arrTokenResponse[1]);

                            /*---------------------------------------------------------------------------------*/
                            ///Generate browser-session for above user.
                            /*---------------------------------------------------------------------------------*/
                            string strBrowserResponse = GenerateBrowserSession(strAccessToken);
                            if (strBrowserResponse == "1")
                            {
                                /*---------------------------------------------------------------------------------*/
                                ///Initiate Pre-check API call
                                /*---------------------------------------------------------------------------------*/
                                InitiateNswsPreCheckApi(strPan, strEmail, strEntityType, strCinNumber, strLlpinNumber, strCompanyName);
                            }
                            else
                            {
                                LblMsg.InnerText = "Something went wrong, Please try again.";
                                ModalPopupConsent.Hide();
                                ModalPopupAlert.Show();
                            }
                        }
                        else
                        {
                            LblMsg.InnerText = "Something went wrong, Please try again.";
                            ModalPopupConsent.Hide();
                            ModalPopupAlert.Show();
                        }
                    }
                }
                else
                {
                    LblMsg.InnerText = "Something went wrong, Please try again.";
                    ModalPopupConsent.Hide();
                    ModalPopupAlert.Show();
                }
            }
            else
            {
                /*---------------------------------------------------------------------------------*/
                ///Generate Access Token
                /*---------------------------------------------------------------------------------*/
                string[] arrTokenResponse = GenerateAccessToken();
                if (Convert.ToString(arrTokenResponse[0]) == "1")
                {
                    string strAccessToken = Convert.ToString(arrTokenResponse[1]);

                    /*---------------------------------------------------------------------------------*/
                    ///Generate browser-session for above user.
                    /*---------------------------------------------------------------------------------*/
                    string strBrowserResponse = GenerateBrowserSession(strAccessToken);
                    if (strBrowserResponse == "1")
                    {
                        /*---------------------------------------------------------------------------------*/
                        ///Initiate Pre-check API and Redirect to NSWS portal
                        /*---------------------------------------------------------------------------------*/
                        InitiateNswsPreCheckApi(strPan, strEmail, strEntityType, strCinNumber, strLlpinNumber, strCompanyName);
                    }
                    else
                    {
                        LblMsg.InnerText = "Something went wrong, Please try again.";
                        ModalPopupConsent.Hide();
                        ModalPopupAlert.Show();
                    }
                }
                else
                {
                    LblMsg.InnerText = "Something went wrong, Please try again.";
                    ModalPopupConsent.Hide();
                    ModalPopupAlert.Show();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SamlInvestorMenu");
        }
    }

    /// <summary>
    /// Method used to generate access token.
    /// </summary>
    /// <returns></returns>
    private string[] GenerateAccessToken()
    {
        string[] arrResponse = new string[2];

        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            /*---------------------------------------------------------------------------------*/
            ///Generate Access Token using OIDC protocol.
            /*---------------------------------------------------------------------------------*/
            var client1 = new RestClient(strTokenUrl)
            {
                Timeout = -1,
                FollowRedirects = false
            };
            var request1 = new RestRequest(Method.POST);
            request1.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request1.AddParameter("grant_type", "password");
            request1.AddParameter("username", strUserId);
            request1.AddParameter("password", strDefaultUserPwd);
            request1.AddParameter("client_id", strTokenClientId);
            request1.AddParameter("client_secret", strTokenClientSecret);
            IRestResponse responseToken = client1.Execute(request1);

            if (responseToken.StatusCode == HttpStatusCode.OK)
            {
                string strAccessToken = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseToken.Content)["access_token"].ToString();
                string strRefreshToken = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseToken.Content)["refresh_token"].ToString();

                Util.LogRequestResponse("SamlInvestorMenu", "GenerateAccessToken", "[AccessTokenReceivedSuccess]:- " + strAccessToken);

                /*---------------------------------------------------------------------------------*/
                ///Assign the refresh token value to the session variable, which will be used during the logout of application.
                /*---------------------------------------------------------------------------------*/
                Session["NswsRefreshToken"] = strRefreshToken;
                Session["NswsAccessToken"] = strAccessToken;

                arrResponse[0] = "1"; ///Success
                arrResponse[1] = strAccessToken; ///Access Token
            }
            else
            {
                Util.LogRequestResponse("SamlInvestorMenu", "GenerateAccessToken", "[AccessTokenResponseError] " + "[ResponseStatusCode]:- " + responseToken.StatusCode + "[ResponseContent]:- " + responseToken.Content.ToString());

                arrResponse[0] = "0"; ///Failure               
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SamlInvestorMenu");
        }

        return arrResponse;
    }

    /// <summary>
    /// Method used to generate browser session and assign the cookies to the browser
    /// </summary>
    /// <param name="strAccessToken"></param>
    /// <returns></returns>
    private string GenerateBrowserSession(string strAccessToken)
    {
        string strReturnStatus = "";

        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            /*---------------------------------------------------------------------------------*/
            ///Generate browser-session for above user.
            /*---------------------------------------------------------------------------------*/
            var client2 = new RestClient(strBrowserSessionUrl)
            {
                Timeout = -1,
                FollowRedirects = false
            };
            var request2 = new RestRequest(Method.GET);
            request2.AddHeader("Authorization", "Bearer " + strAccessToken);
            request2.AddHeader("Content-Type", "application/json");
            IRestResponse responseRedirectApi = client2.Execute(request2);

            if (responseRedirectApi.StatusCode == HttpStatusCode.NoContent)
            {
                Util.LogRequestResponse("SamlInvestorMenu", "GenerateBrowserSession", "[BrowserSessionReceivedSuccess]:- " + "Browser Session Found");

                /*---------------------------------------------------------------------------------*/
                ///Get cookies from response and set cookies on the browser.
                /*---------------------------------------------------------------------------------*/
                List<RestResponseCookie> lstCookies = (List<RestResponseCookie>)responseRedirectApi.Cookies;
                foreach (var varCookie in lstCookies)
                {
                    string strCookiesName = varCookie.Name;
                    string strCookiesValue = varCookie.Value;

                    ///Clear the cookies.
                    ClearCookie(strCookiesName);

                    //Create a new HttpCookie instance to assign cookies.
                    System.Web.HttpCookie objCookie = new System.Web.HttpCookie(strCookiesName)
                    {
                        Value = strCookiesValue,
                        Expires = DateTime.Now.AddDays(1)
                    };

                    //Add the cookie to the browser
                    Response.Cookies.Add(objCookie);
                }

                strReturnStatus = "1"; ///Success
            }
            else
            {
                Util.LogRequestResponse("SamlInvestorMenu", "GenerateBrowserSession", "[BrowserSessionResponseError]-" + "[ResponseStatusCode]:- " + responseRedirectApi.StatusCode + " [ResponseContent]:- " + responseRedirectApi.Content.ToString());

                strReturnStatus = "0"; ///Failure

                ModalPopupConsent.Hide();
                LblMsg.InnerText = "Something went wrong, Please try again.";
                ModalPopupAlert.Show();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SamlInvestorMenu");
        }

        return strReturnStatus;
    }

    /// <summary>
    /// Method used to introspect access token and active status of token
    /// </summary>
    /// <param name="strAccessToken"></param>
    /// <returns></returns>
    private string[] IntrospectAccessToken(string strAccessToken)
    {
        string[] ArrReturnStatus = new string[2];
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            /*---------------------------------------------------------------------------------*/
            ///Introspect Token.
            /*---------------------------------------------------------------------------------*/
            var clientIntro = new RestClient(strIntrospectUrl)
            {
                Timeout = -1,
                FollowRedirects = false
            };
            var requestIntro = new RestRequest(Method.POST);
            requestIntro.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            requestIntro.AddParameter("token", strAccessToken);
            requestIntro.AddParameter("client_id", strTokenClientId);
            requestIntro.AddParameter("client_secret", strTokenClientSecret);
            IRestResponse responseIntro = clientIntro.Execute(requestIntro);

            if (responseIntro.StatusCode == HttpStatusCode.OK)
            {
                string strActiveStatus = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseIntro.Content)["active"].ToString();

                ArrReturnStatus[0] = "1"; ///Success
                ArrReturnStatus[1] = strActiveStatus; ///Token Active Status

                Util.LogRequestResponse("SamlInvestorMenu", "IntrospectAccessToken", "[IntrospectResponseSuccess]" + "[TokenActiveStatus]:- " + strActiveStatus);
            }
            else
            {
                ArrReturnStatus[0] = "0"; ///Failure

                Util.LogRequestResponse("SamlInvestorMenu", "IntrospectAccessToken", "[IntrospectResponseError]" + "[ResponseStatusCode]:- " + responseIntro.StatusCode + " [ResponseContent]:- " + responseIntro.Content.ToString());
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SamlInvestorMenu");
        }

        return ArrReturnStatus;
    }

    /// <summary>
    /// Method used to call pre-check API and redirect to NSWS portal.
    /// </summary>
    /// <param name="strPan"></param>
    /// <param name="strEmail"></param>
    /// <param name="strEntityType"></param>
    /// <param name="strCinNumber"></param>
    /// <param name="strLlpinNumber"></param>
    /// <param name="strCompanyName"></param>
    private void InitiateNswsPreCheckApi(string strPan, string strEmail, string strEntityType, string strCinNumber, string strLlpinNumber, string strCompanyName)
    {
        try
        {
            var reqBody = "";
            if (strEntityType == "1") ///Incorporated Company
            {
                reqBody = @"{" + FormatJSON("pan", strPan)
                         + "," + FormatJSON("cin", strCinNumber)
                         + "," + FormatJSON("entityType", strEntityType)
                         + "," + FormatJSON("email", strEmail)
                         + "}";
            }
            else if (strEntityType == "2") ///Limited Liability Partnership
            {
                reqBody = @"{" + FormatJSON("pan", strPan)
                         + "," + FormatJSON("llpin", strLlpinNumber)
                         + "," + FormatJSON("entityType", strEntityType)
                         + "," + FormatJSON("email", strEmail)
                         + "}";
            }
            else
            {
                reqBody = @"{" + FormatJSON("pan", strPan)
                         + "," + FormatJSON("companyName", strCompanyName)
                         + "," + FormatJSON("entityType", strEntityType)
                         + "," + FormatJSON("email", strEmail)
                         + "}";
            }

            Util.LogRequestResponse("SamlInvestorMenu", "InitiateNswsPreCheckApi", "[InputJsonDataForPreCheckApi]:- " + reqBody);

            /*---------------------------------------------------------------------------------*/
            ///Get the Precheck API Address from web.config file.
            /*---------------------------------------------------------------------------------*/

            var client = new RestClient(strPreCheckApiUrl)
            {
                Timeout = 15000,
                FollowRedirects = false
            };
            var request3 = new RestRequest(Method.POST);
            request3.AddHeader("Content-Type", "application/json");
            request3.AddParameter("application/json", reqBody, ParameterType.RequestBody);
            IRestResponse responsePreCheck = client.Execute(request3);

            Util.LogRequestResponse("SamlInvestorMenu", "InitiateNswsPreCheckApi", "[ResponseStatusCodeFromPreCheckApi]:- " + responsePreCheck.StatusCode);

            if (responsePreCheck.StatusCode == HttpStatusCode.BadRequest)
            {
                var varResponseContent = (JObject)JsonConvert.DeserializeObject(responsePreCheck.Content.ToString());
                string[] arrSplitResponse = responsePreCheck.Content.ToString().Split(':');
                string strResKeyVal = arrSplitResponse[0].ToString().Remove(0, 1);

                Util.LogRequestResponse("SamlInvestorMenu", "InitiateNswsPreCheckApi", "[ResponseContentFromPreCheckApi]:- " + responsePreCheck.Content.ToString());

                string strReponseInfo = "";
                if (strResKeyVal == "\"pan\"")
                {
                    strReponseInfo = varResponseContent["pan"].Value<string>();

                    ModalPopupConsent.Hide();
                    LblMsg.InnerText = strReponseInfo;
                    ModalPopupAlert.Show();
                }
                else if (strResKeyVal == "\"email\"")
                {
                    strReponseInfo = varResponseContent["email"].Value<string>();

                    ModalPopupConsent.Hide();
                    LblMsg.InnerText = strReponseInfo;
                    ModalPopupAlert.Show();
                }
                else if (strResKeyVal == "\"cin\"")
                {
                    strReponseInfo = varResponseContent["cin"].Value<string>();

                    ModalPopupConsent.Hide();
                    LblMsg.InnerText = strReponseInfo;
                    ModalPopupAlert.Show();
                }
                else if (strResKeyVal == "\"error\"")
                {
                    strReponseInfo = varResponseContent["error"].Value<string>();

                    ModalPopupConsent.Hide();
                    LblMsg.InnerText = strReponseInfo;
                    ModalPopupAlert.Show();
                }
                else if (strResKeyVal == "\"companyName\"")
                {
                    strReponseInfo = varResponseContent["companyName"].Value<string>();

                    ModalPopupConsent.Hide();
                    LblMsg.InnerText = strReponseInfo;
                    ModalPopupAlert.Show();
                }
                else if (strResKeyVal == "\"llpin\"")
                {
                    strReponseInfo = varResponseContent["llpin"].Value<string>();

                    ModalPopupConsent.Hide();
                    LblMsg.InnerText = strReponseInfo;
                    ModalPopupAlert.Show();
                }
                else if (strResKeyVal == "\"entityType\"")
                {
                    strReponseInfo = varResponseContent["entityType"].Value<string>();

                    ModalPopupConsent.Hide();
                    LblMsg.InnerText = strReponseInfo;
                    ModalPopupAlert.Show();
                }
            }
            else if (responsePreCheck.StatusCode == HttpStatusCode.Found)
            {
                string strResponseLocation = (string)responsePreCheck.Headers
                                            .Where(x => x.Name == "Location")
                                            .Select(x => x.Value)
                                            .FirstOrDefault();

                Util.LogRequestResponse("SamlInvestorMenu", "InitiateNswsPreCheckApi", "[ResponseHeaderLocationFromPreCheckApi]:- " + strResponseLocation);

                ModalPopupConsent.Hide();
                /*---------------------------------------------------------------------------------*/
                ///Redirect to NSWS portal.
                /*---------------------------------------------------------------------------------*/
                string strScript = "window.open('" + strResponseLocation + "', '_blank');";
                ScriptManager.RegisterStartupScript(BtnValidationYes, this.GetType(), "RedirectScript", strScript, true);

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SamlInvestorMenu");
        }
    }

    /// <summary>
    /// Method used to clear the existing cookies in the browser.
    /// </summary>
    /// <param name="cookieName"></param>
    protected void ClearCookie(string cookieName)
    {
        if (Request.Cookies[cookieName] != null)
        {
            System.Web.HttpCookie cookie = new System.Web.HttpCookie(cookieName)
            {
                Expires = DateTime.Now.AddDays(-1) // Expire the cookie by setting its expiration date in the past.
            };
            Response.Cookies.Add(cookie);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnConsentNo_Click(object sender, EventArgs e)
    {
        ModalPopupConsent.Hide();
    }

    /// <summary>
    /// Method used to create JSON string
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    string FormatJSON(string name, string value)
    {
        return "\"" + name + "\":" + "\"" + value + "\"";
    }

    protected void BtnAlertOk_Click(object sender, EventArgs e)
    {
        ModalPopupAlert.Hide();
    }

    protected void LnkBtnConsentPopupClose_Click(object sender, EventArgs e)
    {
        ModalPopupConsent.Hide();
    }

    protected void LnkBtnAlertPopupClose_Click(object sender, EventArgs e)
    {
        ModalPopupAlert.Hide();
    }

    protected void LnkBtnValidationPopupClose_Click(object sender, EventArgs e)
    {
        ModalPopupValidation.Hide();
    }

    protected void BtnValidationNo_Click(object sender, EventArgs e)
    {
        ModalPopupAlert.Hide();
        ModalPopupValidation.Hide();
    }
    protected void BtnValidationYes_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/EditInvestorProfile.aspx", false);
    }
}