using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SAMLprecheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           

            string txtpan = Request.QueryString["txtpan"].ToString();
            string email = Request.QueryString["email"].ToString();
            string entitytype = Request.QueryString["entitytype"].ToString();
            string cinnumber = Request.QueryString["cinnumber"].ToString();

            Util.LogRequestResponse("SAMLPrecheckapi", "GetQueryStringdata", "[txtpan]:- " + txtpan+ ",[email]:- "+ email+ ",[entitytype]:- "+ entitytype+ ",[cinnumber]:- "+ cinnumber);

            var client = new RestClient("https://dev-nsws.investindia.gov.in/auth/realms/madhyam/userOrg/redirect/user/odisha?clientId=portal-dev");

            client.Timeout = 15000;

            var request = new RestRequest(Method.POST);

            request.AddHeader("Content-Type", "application/json");

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;



            var body = @"{" + FormatJSON("pan", txtpan) + "," + FormatJSON("cin", cinnumber) + "," + FormatJSON("entityType", entitytype) + "," + FormatJSON("email", email) + "}";

            request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Util.LogRequestResponse("SAMLPrecheckapi", "GetResponseFromSAMLAPI", "[Response]:- " + response.Content);

            Response.Write(response.Content);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorSAMLprecheck");
        }
    }


    string FormatJSON(string name, string value)
    {
        //return "\"\"" + name + "\"\": " + "\"\"" + value + "\"\"";

        return "\"" + name + "\":" + "\"" + value + "\"";
    }
}