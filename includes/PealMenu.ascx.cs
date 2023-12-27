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
using System.Net;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public partial class includes_PealMenu : System.Web.UI.UserControl
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

        try
        {
            /*-----------------------------------------------------------*/
            ///// Display Manage Menu If the Unit is Main Unit (Parent Unit)
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

            othermenulist.Visible = false;

            /*-----------------------------------------------------------*/

            //SSOService.ValidateService validate = new SSOService.ValidateService();
            //SSOService.Registration[] App;
            //App = validate.GetOtherApps(validate.URLEncryption(ConfigurationManager.AppSettings["SSOKey"]));

            DWHServiceReference.DWHServiceHostClient validate = new DWHServiceReference.DWHServiceHostClient();
            DWHServiceReference.Registration[] App;
            App = validate.GetOtherAppsForUser(validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]), Session["UID"].ToString(), "");

            if (App != null)
            {
                if (App.Length != 0)
                {
                    for (int i = 0; i < App.Length; i++)
                    {
                        othermenulist.Visible = true;
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        OtherApps.Controls.Add(li);
                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        anchor.Attributes.Add("href", "../AppRedirect.aspx?Key=" + App[i].AppKey + "&AppName=" + App[i].AppAlias + "");
                        anchor.Attributes.Add("target", "_blank");
                        anchor.InnerText = App[i].AppAlias;
                        li.Controls.Add(anchor);
                    }
                }
            }
        }
        catch
        {
        }
    }

    protected void LinkBtnNswsRedirect_Click(object sender, EventArgs e)
    {
        ServiceModalPopup.Show();
    }

    protected void LinkPopupclose_Click(object sender, EventArgs e)
    {
        ServiceModalPopup.Hide();
    }

    protected void BtnNo_Click(object sender, EventArgs e)
    {
        ServiceModalPopup.Hide();
    }

    string FormatJSON(string name, string value)
    {
        //return "\"\"" + name + "\"\": " + "\"\"" + value + "\"\"";

        return "\"" + name + "\":" + "\"" + value + "\"";
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {


        try
        {
            LoginDetails objLogindt = new LoginDetails();
            LoginBusinessLayer objBAL = new LoginBusinessLayer();

            objLogindt.strAction = "IUP";
            objLogindt.strUniqueId = Session["UID"].ToString();

            Util.LogRequestResponse("SAMLInvestormenu", "Investorid", "[strUniqueId]:- " + Session["UID"].ToString());

            DataTable dt = objBAL.viewInvestorDetails(objLogindt);
            if (dt.Rows[0]["VCH_CIN_NUMBER"].ToString() == "" && dt.Rows[0]["INT_ENTITY_TYPE"].ToString() == "0")
            {
                // Response.Redirect("~/EditInvestorProfile.aspx", false);

                InformationModalpopup2.Show();
            }
            else
            {
                var client = new RestClient("https://dev-nsws.investindia.gov.in/auth/realms/madhyam/userOrg/redirect/user/odisha?clientId=portal-dev");

                client.Timeout = 15000;

                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                string txtpan = dt.Rows[0]["VCH_PAN"].ToString();
                string email = dt.Rows[0]["VCH_EMAIL"].ToString();
                string entitytype = dt.Rows[0]["INT_ENTITY_TYPE"].ToString();
                string cinnumber = dt.Rows[0]["VCH_CIN_NUMBER"].ToString();

                var body = @"{" + FormatJSON("pan", txtpan) + "," + FormatJSON("cin", cinnumber) + "," + FormatJSON("entityType", entitytype) + "," + FormatJSON("email", email) + "}";

                Util.LogRequestResponse("SAMLInvestormenu", "InputRequestToSAMLAPI", "[InputJsonData]:- " + body);

                request.AddParameter("application/json", body, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                Util.LogRequestResponse("SAMLInvestormenu", "GetResponseFromSAMLAPI", "[ResponseStatusCode]:- " + response.StatusCode.ToString());

                Util.LogRequestResponse("SAMLInvestormenu", "GetResponseFromSAMLAPI", "[ResponseContent]:- " + response.Content);

                if (response.Content.ToString() != "")
                {

                    if (response.StatusCode.ToString() == "BadRequest")
                    {

                        var data = (JObject)JsonConvert.DeserializeObject(response.Content.ToString());


                        string[] splitdata = response.Content.ToString().Split(':');

                        string removedata = splitdata[0].ToString().Remove(0, 1);


                        string panInformation = "";

                        if (removedata == "\"pan\"")
                        {
                            panInformation = data["pan"].Value<string>();

                            ServiceModalPopup.Hide();
                            lbl_message.InnerText = panInformation;
                            InformationModalpopup.Show();

                        }
                        else if (removedata == "\"email\"")
                        {
                            panInformation = data["email"].Value<string>();

                            ServiceModalPopup.Hide();
                            lbl_message.InnerText = panInformation;
                            InformationModalpopup.Show();


                        }
                        else if (removedata == "\"cin\"")
                        {
                            panInformation = data["cin"].Value<string>();

                            ServiceModalPopup.Hide();
                            lbl_message.InnerText = "Invalid CIN Number" + panInformation;
                            InformationModalpopup.Show();


                        }
                        else if (removedata == "\"error\"")
                        {
                            panInformation = data["error"].Value<string>();

                            ServiceModalPopup.Hide();
                            lbl_message.InnerText = panInformation;
                            InformationModalpopup.Show();


                        }
                        else if (removedata == "\"companyName\"")
                        {
                            panInformation = data["companyName"].Value<string>();

                            ServiceModalPopup.Hide();
                            lbl_message.InnerText = panInformation;
                            InformationModalpopup.Show();


                        }
                        else if (removedata == "\"llpin\"")
                        {
                            panInformation = data["llpin"].Value<string>();

                            ServiceModalPopup.Hide();
                            lbl_message.InnerText = panInformation;
                            InformationModalpopup.Show();


                        }


                    }
                    else
                    {
                        Response.Redirect("~/SAMLprecheck.aspx?txtpan=" + txtpan + "&email=" + email + "&entitytype=" + entitytype + "&cinnumber=" + cinnumber, false);
                    }


                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OtherMenu");
        }

    }



    protected void Btn_ok_Click(object sender, EventArgs e)
    {
        InformationModalpopup.Hide();
    }

    protected void LinkBtnPopupclose_Click(object sender, EventArgs e)
    {
        InformationModalpopup.Hide();
    }

    protected void Popupclose_Click(object sender, EventArgs e)
    {
        InformationModalpopup2.Hide();
    }

    protected void Btn_no_Click(object sender, EventArgs e)
    {
        InformationModalpopup.Hide();
        InformationModalpopup2.Hide();
    }

    protected void Btn_yes_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/EditInvestorProfile.aspx", false);
    }
}