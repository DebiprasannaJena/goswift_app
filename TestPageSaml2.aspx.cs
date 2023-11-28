using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestPageSaml2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
      
        if (!IsPostBack)
        {
            //txtpan.Text = "BSQPJ1955A";
            //txtcin.Text = "U67190TN2014PTC055555";
            //seltype.SelectedValue = "3";
            //txtemail.Text = "demotest@test.com";

          
           //string strnswssaml= Request.QueryString["nswssaml"].ToString();

           // Response.Write(response.Content);
        }
    }

    string FormatJSON(string name, string value)
    {
        //return "\"\"" + name + "\"\": " + "\"\"" + value + "\"\"";

        return "\"" + name + "\":" + "\"" + value + "\"";
    }
    protected void btnsaml_Click(object sender, EventArgs e)
    {


        var client = new RestClient("https://dev-nsws.investindia.gov.in/auth/realms/madhyam/userOrg/redirect/user/odisha?clientId=portal-dev");

        //var request = new RestRequest("https://dev-nsws.investindia.gov.in/auth/realms/madhyam/userOrg/redirect/user/odisha?clientId=portal-dev", Method.POST);

        // var client = new RestClient(options,);

        // var request = new RestRequest("https://dev-nsws.investindia.gov.in/auth/realms/madhyam/userOrg/redirect/user/odisha?clientId=portal-dev", Method.POST);

        client.Timeout = 15000;

        var request = new RestRequest(Method.POST);

        request.AddHeader("Content-Type", "application/json");

        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;



        // request.AddHeader("Cookie", "AUTH_SESSION_ID=cc9d9426-4743-4313-ae65-b99fe23b6417; AUTH_SESSION_ID_LEGACY=cc9d9426-4743-4313-ae65-b99fe23b6417");





         var body = @"{" + FormatJSON("pan", txtpan.Text) + "," + FormatJSON("cin", txtcin.Text) + "," + FormatJSON("entityType", seltype.SelectedItem.Value) + "," + FormatJSON("email", txtemail.Text) +"}";


        


        request.AddParameter("application/json", body, ParameterType.RequestBody);

        // request.AddBody(body, DataFormat.Json.ToString());

        IRestResponse response = client.Execute(request);

        // Console.WriteLine(response.Content);

        if (response.Content.ToString() != "")
        {
            if (response.StatusCode.ToString() == "BadRequest")
            {

                Lblstatus.Text = response.StatusCode.ToString();

                lblcontent.Text = response.Content.ToString();

                //string removedata = response.Content.ToString().Remove(0, 2);

                // string remove = removedata.Remove(3, 1);



                var data = (JObject)JsonConvert.DeserializeObject(response.Content.ToString());



                string[] splitdata = response.Content.ToString().Split(':');

                string removedata = splitdata[0].ToString().Remove(0, 1);


                string panInformation = "";
                 if (removedata == "\"pan\"")
                {
                    panInformation = data["pan"].Value<string>();

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + panInformation + "');", true);
                }

               else if (removedata == "\"cin\"")
                {
                    panInformation = data["cin"].Value<string>();

                    panInformation = "Invalid CIN Number :" + panInformation;


                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + panInformation + "');", true);
                }

                else if (removedata == "\"error\"")
                {
                    panInformation = data["error"].Value<string>();

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + panInformation + "');", true);
                }
                else if (removedata == "\"companyName\"")
                {
                    panInformation = data["companyName"].Value<string>();

                    panInformation = "Invalid Company Name :" + panInformation;

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + panInformation + "');", true);
                }
                else if (removedata == "\"llpin\"")
                {
                    panInformation = data["llpin"].Value<string>();

                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + panInformation + "');", true);
                }

            }
            else
            {
                Response.Write(response.Content);
            }

        }



       
    }
}