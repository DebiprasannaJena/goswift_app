using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spire.Pdf.Fields;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using RestSharp;



public partial class TestNSDL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            LblMsg4.Text = "";
            PANValidationNSDL obj = new PANValidationNSDL();
            //string panResponse = obj.GetPANStatusFromNSDL("AAAPW9785A", "VINITA BHANUSHALI", "02/09/1928");
            string panResponse = obj.GetPANStatusFromNSDL(TxtPan.Text.Trim(), TxtName.Text, TxtDob.Text);

            LblMsg4.Text = panResponse;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    public string MakeRandom(int pl)
    {
        Thread.Sleep(10);
        string possibles = "0123456789";
        char[] passwords = new char[pl];
        Random rd = new Random();

        for (int i = 0; i < pl; i++)
        {
            passwords[i] = possibles[rd.Next(0, possibles.Length)];
        }
        return new string(passwords);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write(NSDLResponse(TextBox1.Text));
    }

    #region NSDL Integration

    //Sample NSDL Reponse :- 1^ACYPC7626G^E^CHELLIAH^SINNAPPU^^Shri^22/01/2015^^^

    public string NSDLResponse(string strPan)
    {
        string strData = ConfigurationManager.AppSettings["PanUserID"].ToString() + "^" + strPan;
        string strPwd = ConfigurationManager.AppSettings["PanPWD"].ToString();
        string strURL = ConfigurationManager.AppSettings["PanURL"].ToString();
        string sign = Singnature(strData, Server.MapPath("ncodeipicol.pfx"), strPwd);

        ///Post data
        StringBuilder postData = new StringBuilder();
        postData.Append("data=" + strData);
        postData.Append("&signature=" + System.Web.HttpUtility.UrlEncode(sign));

        ///Encode the post data
        System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
        byte[] data = encoding.GetBytes(postData.ToString());

        ///Prepare web request...
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(strURL);
        myRequest.Method = "POST";
        myRequest.ContentType = "application/x-www-form-urlencoded";
        myRequest.ContentLength = data.Length;
        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

        //Send the data.
        Stream newStream = myRequest.GetRequestStream();
        newStream.Write(data, 0, data.Length);

        //Now, we read the response (the string), and output it.  
        HttpWebResponse WebResp = (HttpWebResponse)myRequest.GetResponse();
        Stream Answer = WebResp.GetResponseStream();
        StreamReader _Answer = new StreamReader(Answer);

        string Response = _Answer.ReadToEnd();

        newStream.Close();
        return Response;
    }
    public String Singnature(string strData, string strFile, string strPwd)
    {
        X509Certificate2 m = new X509Certificate2(strFile, strPwd);
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        byte[] bytes = encoding.GetBytes(strData);
        byte[] sig = Sign(bytes, m);
        String sigi = Convert.ToBase64String(sig);
        return sigi;
    }
    public static byte[] Sign(byte[] data, X509Certificate2 certificate)
    {
        if (data == null)
            throw new ArgumentNullException("data");
        if (certificate == null)
            throw new ArgumentNullException("certificate");

        // setup the data to sign
        ContentInfo content = new ContentInfo(data);
        SignedCms signedCms = new SignedCms(content, false);
        CmsSigner signer = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, certificate);
        // create the signature
        signedCms.ComputeSignature(signer);
        return signedCms.Encode();
    }
    public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    #endregion

    protected void Btn_SAML_Request_Click(object sender, EventArgs e)
    {
        //// Construct the SAML request XML
        //string samlRequestXml = ConstructSamlRequest();

        //// Encode the SAML request as base64-url-encoded string
        //string base64UrlEncodedSamlRequest = Base64UrlEncode(samlRequestXml);

        //Response.Write("Base64-URL Encoded SAML Request:");
        //Response.Write(base64UrlEncodedSamlRequest);


        //public static async Task Main(string[] args)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var realm = "your-realm";
        //        var samlRequest = "base64-url-encoded-saml-request"; // Constructed SAML request

        //        var url = $"http://keycloak-server/auth/realms/{realm}/protocol/saml?samlRequest={samlRequest}";

        //        // Send the request
        //        var response = await client.GetAsync(url);

        //        // Process the response
        //        var responseBody = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine(responseBody);
        //    }
        //}


        //  // Construct the SAML request XML
        //  string samlRequestXml = ConstructSamlRequest();

        //  // Encode the SAML request as base64
        //  string base64EncodedSamlRequest = Base64Encode(samlRequestXml);

        //  // URL-encode the base64-encoded SAML request
        //  string urlEncodedSamlRequest = UrlEncode(base64EncodedSamlRequest);

        //  // Specify the IdP endpoint URL
        //  string idpEndpointUrl = "https://ssoinvestodisha.odisha.gov.in/auth/realms/goswift-dev/protocol/saml";

        //  // Construct the complete URL with the SAML request
        //// string requestUrl = $"{idpEndpointUrl}?SAMLRequest={urlEncodedSamlRequest}";

        //  string requestUrl = idpEndpointUrl + "?SAMLRequest=" + urlEncodedSamlRequest;

        //  System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        //  ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

        //  HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
        //  myRequest.Method = "POST";
        //  myRequest.ContentType = "application/x-www-form-urlencoded";
        //  //myRequest.ContentLength = data.Length;
        //  ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);




        //  //// Send the HTTP GET request to the IdP endpoint
        //  ////HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
        //  ////request.Method = "GET";

        //  // Get the response
        //  HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse();
        //  Stream dataStream = response.GetResponseStream();
        //  StreamReader reader = new StreamReader(dataStream);
        //  string responseFromServer = reader.ReadToEnd();

        //  // Display the response
        //  Response.Write(responseFromServer);

        //  // Clean up the streams and response
        //  reader.Close();
        //  dataStream.Close();
        //  response.Close();


        /*---------------------------------------------------------------------------*/

        //try
        //{
        //    // Construct the SAML request XML
        //    string samlRequestXml = ConstructSamlRequest();

        //    // Set the IdP endpoint URL
        //    string idpEndpointUrl = "https://ssoinvestodisha.odisha.gov.in/auth/realms/goswift-dev/protocol/saml";

        //    // Send the HTTP POST request to the IdP endpoint
        //    string responseFromServer = SendHttpPostRequest(idpEndpointUrl, samlRequestXml);

        //    // Display the response
        //    Console.WriteLine(responseFromServer);
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Error: " + ex.Message);
        //}


        var strTokenUrl = "https://ssoinvestodisha.odisha.gov.in/realms/goswift-dev/protocol/openid-connect/token";

        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        /////Generate Access Token
        //var client1 = new RestClient(strTokenUrl);
        //client1.Timeout = -1;
        //client1.FollowRedirects = false;
        //var request1 = new RestRequest(Method.POST);      
        //request1.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        //request1.AddParameter("grant_type", "password");
        //request1.AddParameter("username", "AABCT8879J_505000");
        //request1.AddParameter("password", "csmpl@123");
        //request1.AddParameter("client_secret", "YW6bBp2frLz9Hu6koDXkfXWXtslvyFzF");
        //request1.AddParameter("client_id", "nsws-rev-openid");
        //IRestResponse responseToken = client1.Execute(request1);

        ////var cookies = responseToken.Headers.GetValues("Set-Cookie");


        ////  GetAccessToken("https://dev-nsws.investindia.gov.in/auth/realms/madhyam", "n5b450HLa74tp2o7LYQbAHNZlpY9emKs");

        //string strAccessToke = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseToken.Content)["access_token"].ToString();

        ////GetSamlAssertion(strAccessToke);



        // Create a request to the Keycloak token endpoint
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ssoinvestodisha.odisha.gov.in/realms/goswift-dev/protocol/openid-connect/token");

        // Set the request method to POST
        request.Method = "POST";

        // Set the content type and encoding
        request.ContentType = "application/x-www-form-urlencoded";

        // Construct the request body with login credentials
        string requestBody = "username=AABCT8879J_505000&password=csmpl@123&grant_type=password&client_id=nsws-rev-openid&client_secret=YW6bBp2frLz9Hu6koDXkfXWXtslvyFzF";

        // Convert the request body to a byte array
        byte[] requestBodyBytes = Encoding.UTF8.GetBytes(requestBody);

        // Set the content length
        request.ContentLength = requestBodyBytes.Length;

        // Write the request body to the request stream
        using (Stream requestStream = request.GetRequestStream())
        {
            requestStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
        }

        // Get the response from the server
        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            // Read cookies from the response headers
            string cookies = response.Headers.Get("Set-Cookie");

            // Display the cookies
            Response.Write("Cookies: " + cookies);
        }








    }



    private readonly WebClient _webClient = new WebClient();

    public string GetAccessToken(string clientId, string clientSecret)
    {
        //var tokenEndpoint = "http://your-keycloak-domain/auth/realms/your-realm-name/protocol/openid-connect/token";
        var tokenEndpoint = "https://ssoinvestodisha.odisha.gov.in/realms/goswift-dev/protocol/openid-connect/token";

        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

        var data = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", clientId },
            { "client_secret", clientSecret }
        };

        var responseBytes = _webClient.UploadValues(tokenEndpoint, "POST", ToQueryString(data));
        var responseContent = Encoding.UTF8.GetString(responseBytes);
        var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

        return tokenResponse["access_token"];
    }

    public string GetSamlAssertion(string accessToken)
    {
        //var samlEndpoint = "http://your-keycloak-domain/auth/realms/your-realm-name/protocol/saml";
        var samlEndpoint = "https://ssoinvestodisha.odisha.gov.in/realms/goswift-dev/protocol/saml";


        _webClient.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + accessToken);

        var samlAssertion = _webClient.DownloadString(samlEndpoint);

        return samlAssertion;
    }

    private NameValueCollection ToQueryString(Dictionary<string, string> dict)
    {
        var queryString = new NameValueCollection();
        foreach (var kvp in dict)
        {
            queryString.Add(kvp.Key, kvp.Value);
        }
        return queryString;
    }











    private static string SendHttpPostRequest(string url, string postData)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/xml";

        using (Stream dataStream = request.GetRequestStream())
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            dataStream.Write(byteArray, 0, byteArray.Length);
        }

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (Stream dataStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }



    private static string ConstructSamlRequest()
    {
        // Constructing a basic SAML request XML (replace with actual SAML request)
        string samlRequestXml = @"
            <samlp:AuthnRequest xmlns:samlp=""urn:oasis:names:tc:SAML:2.0:protocol""
                ID=""identifier""
                Version=""2.0""
                IssueInstant=""2022-02-22T12:30:45Z""
                Destination=""https://ssoinvestodisha.odisha.gov.in/saml/acs""
                AssertionConsumerServiceURL=""https://ssoinvestodisha.odisha.gov.in/saml/acs"">
                <saml:Issuer xmlns:saml=""urn:oasis:names:tc:SAML:2.0:assertion"">https://ssoinvestodisha.odisha.gov.in</saml:Issuer>
            </samlp:AuthnRequest>";

        return samlRequestXml;
    }

    private static string Base64UrlEncode(string input)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        string base64String = Convert.ToBase64String(inputBytes);
        string base64UrlEncodedString = base64String.Replace('+', '-').Replace('/', '_').TrimEnd('=');
        return base64UrlEncodedString;
    }

    private static string Base64Encode(string plainText)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    private static string UrlEncode(string value)
    {
        return Uri.EscapeDataString(value);
    }



    protected void Btn_Get_Cookies_Click(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        //// Create a WebRequest instance
        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ssoinvestodisha.odisha.gov.in");

        //// Get the response
        //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //// Get cookies from the response headers
        //string cookiesHeader = response.Headers[HttpResponseHeader.SetCookie];

        //// Split the cookies string to individual cookies
        //string[] cookies = cookiesHeader.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

        //// Process cookies here
        //foreach (var cookie in cookies)
        //{
        //    Console.WriteLine(cookie);
        //    // Extract the KEYCLOAK_IDENTITY and KEYCLOAK_IDENTITY_legacy cookies as needed
        //}



        // Create a WebRequest instance
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://ssoinvestodisha.odisha.gov.in/admin/master/console/");

        // Create a CookieContainer to hold the cookies
        CookieContainer cookies = new CookieContainer();
        request.CookieContainer = cookies;

        // Get the response
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        // Get cookies from the CookieContainer
        CookieCollection responseCookies = cookies.GetCookies(new Uri("https://ssoinvestodisha.odisha.gov.in/admin/master/console/"));

        // Process cookies here
        foreach (Cookie cookie in responseCookies)
        {
            Console.WriteLine(cookie.Name + "=" + cookie.Value);
            // Extract the KEYCLOAK_IDENTITY and KEYCLOAK_IDENTITY_legacy cookies as needed
        }









    }


}