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

public partial class includes_investormenu : System.Web.UI.UserControl
{
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

    protected void BtnConsentYes_Click(object sender, EventArgs e)
    {
        try
        {
            /*---------------------------------------------------------------------------------*/
            ///In order to login to NSWS portal through SSO follow the below steps:
            ///(1) Generate access token from keycloak server using the OIDC(open-id) protocol.
            ///(2) After getting token, generate the browser-session by call the API present in the jar file present in the provider folder of keycloak server.
            ///(3) Please note that the jar file is provided by NSWS team.In case of any changes or error in this file,contact to NSWS team.
            ///(4) After succesfully generating browser-session, initiate the pre-check API.
            ///(5) Follow the latest pre-check API docuement for detailed functionalities of pre-check API.
            /*---------------------------------------------------------------------------------*/

            string strUserId = Session["UserId"].ToString();///AABCT8879J_505000 
            string strUniqueId = Session["UID"].ToString();

            LoginDetails objLogindt = new LoginDetails();
            LoginBusinessLayer objBAL = new LoginBusinessLayer();

            objLogindt.strAction = "IUP";
            objLogindt.strUniqueId = strUniqueId;

            Util.LogRequestResponse("SAMLInvestorMenu", "RedirectionToNsws", "[ForUniqueId]:- " + strUniqueId + " [ForUserId]:- " + strUserId);

            DataTable dt = objBAL.viewInvestorDetails(objLogindt);
            if (dt.Rows[0]["VCH_CIN_NUMBER"].ToString() == "" && dt.Rows[0]["INT_ENTITY_TYPE"].ToString() == "0")
            {
                ModalPopupConsent.Hide();
                ModalPopupValidation.Show();
            }
            else
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                //var strTokenUrl = "https://ssoinvestodisha.odisha.gov.in/realms/goswift-dev/protocol/openid-connect/token";
                //var strBrowserSessionUrl = "https://ssoinvestodisha.odisha.gov.in/realms/goswift-dev/browser-session/init?publicClient=nsws-rev-openid";

                var strTokenUrl = "http://localhost:8080/realms/rlm-goswift-dev/protocol/openid-connect/token";
                var strBrowserSessionUrl = "http://localhost:8080/realms/rlm-goswift-dev/browser-session/init?publicClient=client-nsws-openid2";

                string strPwd = "csmpl@123";///Default password used for keycloak users
                string strClientId = "client-nsws-openid";///nsws-rev-openid
                string strClientSecret = "jKBBf2m9zuUI16F5ynYsU786H6hD4qiN"; //"YW6bBp2frLz9Hu6koDXkfXWXtslvyFzF"

                /*---------------------------------------------------------------------------------*/
                ///Generate Token using OIDC protocol.
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
                request1.AddParameter("password", strPwd);
                request1.AddParameter("client_id", strClientId);
                request1.AddParameter("client_secret", strClientSecret);
                IRestResponse responseToken = client1.Execute(request1);

                if (responseToken.StatusCode == HttpStatusCode.OK)
                {
                    string strAccessToken = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseToken.Content)["access_token"].ToString();
                    Util.LogRequestResponse("SamlInvestorMenu", "RedirectionToNsws", "[AccessTokenReceivedSuccess]:- " + strAccessToken);

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
                        Util.LogRequestResponse("SamlInvestorMenu", "RedirectionToNsws", "[BrowserSessionReceivedSuccess]:- " + "Browser Session Found");

                        /*---------------------------------------------------------------------------------*/
                        ///Initiate Pre-check API call
                        /*---------------------------------------------------------------------------------*/
                        string strPan = dt.Rows[0]["VCH_PAN"].ToString();
                        string strEmail = dt.Rows[0]["VCH_EMAIL"].ToString();
                        string strEntityType = dt.Rows[0]["INT_ENTITY_TYPE"].ToString();
                        string strCinNumber = dt.Rows[0]["VCH_CIN_NUMBER"].ToString();
                        string strCompanyName = dt.Rows[0]["VCH_INV_NAME"].ToString();

                        var reqBody = "";
                        if (strEntityType == "1") ///Incorporated Company
                        {
                            reqBody = @"{" + FormatJSON("pan", strPan)
                                     + "," + FormatJSON("cin", strCinNumber)
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

                        Util.LogRequestResponse("SamlInvestorMenu", "RedirectionToNsws", "[InputJsonDataForPreCheckApi]:- " + reqBody);

                        /*---------------------------------------------------------------------------------*/
                        ///Get the Precheck API Address from web.config file.
                        /*---------------------------------------------------------------------------------*/
                        string strPreCheckApiUrl = ConfigurationManager.AppSettings["NswsPreCheckApiUrl"].ToString();                 

                        var client = new RestClient(strPreCheckApiUrl)
                        {
                            Timeout = 15000,
                            FollowRedirects = false
                        };
                        var request3 = new RestRequest(Method.POST);
                        request3.AddHeader("Content-Type", "application/json");
                        request3.AddParameter("application/json", reqBody, ParameterType.RequestBody);
                        IRestResponse responsePreCheck = client.Execute(request3);

                        Util.LogRequestResponse("SamlInvestorMenu", "RedirectionToNsws", "[ResponseStatusCodeFromPreCheckApi]:- " + responsePreCheck.StatusCode);

                        if (responsePreCheck.StatusCode == HttpStatusCode.BadRequest)
                        {
                            var varResponseContent = (JObject)JsonConvert.DeserializeObject(responsePreCheck.Content.ToString());
                            string[] arrSplitResponse = responsePreCheck.Content.ToString().Split(':');
                            string strResKeyVal = arrSplitResponse[0].ToString().Remove(0, 1);

                            Util.LogRequestResponse("SamlInvestorMenu", "RedirectionToNsws", "[ResponseContentFromPreCheckApi]:- " + responsePreCheck.Content.ToString());

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

                            Util.LogRequestResponse("SAMLInvestorMenu", "RedirectionToNsws", "[ResponseHeaderLocationFromPreCheckApi]:- " + strResponseLocation);

                            ModalPopupConsent.Hide();
                            string strScript = "window.open('" + strResponseLocation + "', '_blank');";
                            ScriptManager.RegisterStartupScript(BtnValidationYes, this.GetType(), "RedirectScript", strScript, true);
                        }
                    }
                    else
                    {
                        Util.LogRequestResponse("SamlInvestorMenu", "RedirectionToNsws", "[BrowserSessionResponseError]-" + "[ResponseStatusCode]:- " + responseRedirectApi.StatusCode + " [ResponseContent]:- " + responseRedirectApi.Content.ToString());

                        ModalPopupConsent.Hide();
                        LblMsg.InnerText = "Something went wrong, Please try again.";
                        ModalPopupAlert.Show();
                    }
                }
                else
                {
                    Util.LogRequestResponse("SamlInvestorMenu", "RedirectionToNsws", "[AccessTokenResponseError]:- " + responseToken.Content.ToString());

                    ModalPopupConsent.Hide();
                    LblMsg.InnerText = "Something went wrong, Please try again.";
                    ModalPopupAlert.Show();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "SAMLInvestorMenu");
        }
    }

    protected void BtnConsentNo_Click(object sender, EventArgs e)
    {
        ModalPopupConsent.Hide();
    }

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