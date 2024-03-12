using RestSharp;
using System;
using System.Configuration;
using System.Linq;
using System.Net;

public partial class SAMLprecheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string strPan = Request.QueryString["txtpan"].ToString();
            string strEmail = Request.QueryString["email"].ToString();
            string strEntityType = Request.QueryString["entitytype"].ToString();
            string strCinNumber = Request.QueryString["cinnumber"].ToString();            

            Util.LogRequestResponse("SamlPreCheckApi", "GetQueryStringdata", "[txtpan]:- " + strPan + ",[email]:- " + strEmail + ",[entitytype]:- " + strEntityType + ",[cinnumber]:- " + strCinNumber);

            /*---------------------------------------------------------------------------------*/
            ///Get the Precheck API Address from web.config file.
            /*---------------------------------------------------------------------------------*/
            string strPreCheckApiUrl = ConfigurationManager.AppSettings["NswsPreCheckApiUrl"].ToString();


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            // ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            /*---------------------------------------------------------------------------------*/
            ///Generate JSON string for API request body.
            /*---------------------------------------------------------------------------------*/
            var body = @"{" + FormatJSON("pan", strPan)
                      + "," + FormatJSON("cin", strCinNumber)
                      + "," + FormatJSON("entityType", strEntityType)
                      + "," + FormatJSON("email", strEmail)
                      + "}";

            var client = new RestClient(strPreCheckApiUrl)
            {
                Timeout = 15000
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);









            Util.LogRequestResponse("SamlPreCheckApi", "GetResponseFromSamlApi", "[Response]:- " + response.Content);

            //string x = response.Headers["Location"].ToString();

            

            string location = (string)response.Headers
                          .Where(x => x.Name == "Location")
                          .Select(x => x.Value)
                          .FirstOrDefault();

            Response.Write(response.Content);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorSAMLprecheck");
        }
    }

    string FormatJSON(string name, string value)
    {
        return "\"" + name + "\":" + "\"" + value + "\"";
    }
}