using RestSharp;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;

/// <summary>
/// Summary description for PANValidationNSDL
/// </summary>
public class PANValidationNSDL
{
    public PANValidationNSDL()
    {

    }

    public string GetPANStatusFromNSDL(string strPAN)
    {
        string strResponse = "";

        try
        {
            ///Get the PAN credential details from web.config file.
            string strData = ConfigurationManager.AppSettings["PanUserID"].ToString() + "^" + strPAN;
            string strURL = ConfigurationManager.AppSettings["PanURL"];
            string strPFXPassword = ConfigurationManager.AppSettings["PanPWD"];
            string strCertificateName = ConfigurationManager.AppSettings["Certificatename"];

            ///Get the signature using pfx file
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            X509Certificate2 m = new X509Certificate2(HttpContext.Current.Server.MapPath("~/PFX/") + strCertificateName, strPFXPassword);
            byte[] bytes = encoding.GetBytes(strData);
            byte[] sig = Sign(bytes, m);
            String sigi = Convert.ToBase64String(sig);

            ///Prepare the data to be posted to NSDL server.
            StringBuilder postData = new StringBuilder();
            postData.Append("data=" + strData);
            postData.Append("&signature=" + System.Web.HttpUtility.UrlEncode(sigi));
            postData.Append("&version=" + 2);
            byte[] data = encoding.GetBytes(postData.ToString());
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            ///Post data to NSDL server.
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(strURL);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            ServicePointManager.Expect100Continue = true;

            ///Send the data.
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);

            ///Now, read the response (the string), and output it.  
            HttpWebResponse WebResp = (HttpWebResponse)myRequest.GetResponse();
            Stream Answer = WebResp.GetResponseStream();
            StreamReader _Answer = new StreamReader(Answer);
            strResponse = _Answer.ReadToEnd();

            newStream.Close();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PanValidation");
        }

        return strResponse;
    }

    public string GetPANStatusFromNSDL(string strPAN, string strName, string strDob)
    {
        string strResponse = "";

        try
        {
            /*---------------------------------------------------------------------------------*/
            ///Get the PAN credential details from web.config file.
            /*---------------------------------------------------------------------------------*/
            string strPanUserId = ConfigurationManager.AppSettings["PanUserID"].ToString();
            string strURL = ConfigurationManager.AppSettings["PanURL"];
            string strPFXPassword = ConfigurationManager.AppSettings["PanPWD"];
            string strCertificateName = ConfigurationManager.AppSettings["Certificatename"];

            /*---------------------------------------------------------------------------------*/
            ///Generate JSON string for digital sign.
            /*---------------------------------------------------------------------------------*/
            string strData = "[{" + FormatJSON("pan", strPAN)
                            + "," + FormatJSON("name", strName)
                            + "," + FormatJSON("fathername", "")
                            + "," + FormatJSON("dob", strDob)
                            + "}]";

            /*---------------------------------------------------------------------------------*/
            ///Get the signature using pfx file
            /*---------------------------------------------------------------------------------*/
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            X509Certificate2 m = new X509Certificate2(HttpContext.Current.Server.MapPath("~/PFX/") + strCertificateName, strPFXPassword);
            byte[] bytes = encoding.GetBytes(strData);
            byte[] sig = Sign(bytes, m);
            string strSignature = Convert.ToBase64String(sig);

            /*---------------------------------------------------------------------------------*/
            ///Prepare the data to be posted to NSDL server.
            /*---------------------------------------------------------------------------------*/
            var randomNo = MakeRandom(3);
            var requestTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"); ///"2024-02-08T15:17:11"  
            var transactionId = strPanUserId + ":" + DateTime.Now.ToString("yyyyMMddHHmmssfffff") + randomNo;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


            var client = new RestClient(strURL)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("User_ID", strPanUserId);
            request.AddHeader("Records_count", "1");
            request.AddHeader("Request_time", requestTime); ///"2024-01-08-11.35.50.511211"
            request.AddHeader("Transaction_ID", transactionId);///"V0175701:1234567890ABcdefGHIJH"
            request.AddHeader("Version", "4");
            request.AddHeader("Content-Type", "application/json");

            var requestJsonBody = @"{"
                                    + "\"inputData\":" + strData
                                    + ","
                                    + FormatJSON("signature", strSignature)
                                    + "}";


            request.AddParameter("application/json", requestJsonBody, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var output = response.Content.ToString();

            return output;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PanValidation");
        }

        return strResponse;
    }
    public static byte[] Sign(byte[] data, X509Certificate2 certificate)
    {
        if (data == null)
        {
            throw new ArgumentNullException("data");
        }

        if (certificate == null)
        {
            throw new ArgumentNullException("certificate");
        }

        // setup the data to sign
        ContentInfo content = new ContentInfo(data);
        SignedCms signedCms = new SignedCms(content, false);
        CmsSigner signer = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, certificate);
        // create the signature
        signedCms.ComputeSignature(signer);
        return signedCms.Encode();
    }
    private bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    string FormatJSON(string name, string value)
    {
        return "\"" + name + "\":" + "\"" + value + "\"";
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
}