using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Web;
using System.Linq;
using System.Data;
using AdminApp.Model;
using RestSharp.Validation;

/// <summary>
/// Summary description for PANValidationNSDL
/// </summary>
public class PANValidationNSDL
{
    readonly SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    public PANValidationNSDL()
    {
    }

    /// <summary>
    /// This method is used to validate PAN by using PAN as a single parameter.
    /// As of March 2024, this method has become obsolete.
    /// </summary>
    /// <param name="strPAN"></param>
    /// <returns></returns>
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

    /// <summary>
    /// This method validates PAN by utilizing PAN, PAN holder's name, and date of birth as parameters.
    /// This method is applicable from March 2024 onwards.
    /// </summary>
    /// <param name="strPAN"></param>
    /// <param name="strName"></param>
    /// <param name="strDob"></param>
    /// <returns></returns>
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

            Util.LogRequestResponse("PanValidation", "GetPANStatusFromNSDL", "[RequestCredentials]:- " + strPanUserId + " - " + strURL + " - " + strPFXPassword + " - " + strCertificateName);

            /*---------------------------------------------------------------------------------*/
            ///Generate JSON string for digital sign.
            /*---------------------------------------------------------------------------------*/
            string strData = "[{" + FormatJSON("pan", strPAN)
                            + "," + FormatJSON("name", strName)
                            + "," + FormatJSON("fathername", "")
                            + "," + FormatJSON("dob", strDob)
                            + "}]";

            Util.LogRequestResponse("PanValidation", "GetPANStatusFromNSDL", "[RequestData]:- " + strData);

            /*---------------------------------------------------------------------------------*/
            ///Get the signature using pfx file
            /*---------------------------------------------------------------------------------*/
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            X509Certificate2 m = new X509Certificate2(HttpContext.Current.Server.MapPath("~/PFX/") + strCertificateName, strPFXPassword);
            byte[] bytes = encoding.GetBytes(strData);
            byte[] sig = Sign(bytes, m);
            string strSignature = Convert.ToBase64String(sig);

            Util.LogRequestResponse("PanValidation", "GetPANStatusFromNSDL", "[ConvertedSignature]:- " + strSignature);

            /*---------------------------------------------------------------------------------*/
            ///Prepare the data to be posted to NSDL server.
            /*---------------------------------------------------------------------------------*/
            string strRandomNo = MakeRandom(3);
            string strRequestTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"); ///"2024-02-08T15:17:11"  
            string strTransactionId = strPanUserId + ":" + DateTime.Now.ToString("yyyyMMddHHmmssfffff") + strRandomNo;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            ServicePointManager.Expect100Continue = true;

            var client = new RestClient(strURL)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("User_ID", strPanUserId);
            request.AddHeader("Records_count", "1");
            request.AddHeader("Request_time", strRequestTime); ///"2024-01-08-11.35.50.511211"
            request.AddHeader("Transaction_ID", strTransactionId);///"V0175701:1234567890ABcdefGHIJH"
            request.AddHeader("Version", "4");
            request.AddHeader("Content-Type", "application/json");

            var requestJsonBody = @"{"
                                    + "\"inputData\":" + strData
                                    + ","
                                    + FormatJSON("signature", strSignature)
                                    + "}";

            Util.LogRequestResponse("PanValidation", "GetPANStatusFromNSDL", "[RequestJsonBody]:- " + requestJsonBody);

            request.AddParameter("application/json", requestJsonBody, ParameterType.RequestBody);
            IRestResponse objRestResponse = client.Execute(request);

            if (objRestResponse.StatusCode == HttpStatusCode.OK)
            {
                var strResponseContent = objRestResponse.Content.ToString();

                Util.LogRequestResponse("PanValidation", "GetPANStatusFromNSDL", "[ResponseStatusCode]:- " + objRestResponse.StatusCode + " -[ResponseContent]:- " + strResponseContent);

                /*---------------------------------------------------------------------------------*/
                ///Log the request and response data.
                /*---------------------------------------------------------------------------------*/
                string strResDobStatus = "";
                string strResNameStatus = "";
                string strResPanStatus = "";
                string strResPan = "";
                string strResSeedingStatus = "";

                var jsonObject = JObject.Parse(strResponseContent);
                string strResponseCode = (string)jsonObject["response_Code"];

                JArray outputData = (JArray)jsonObject["outputData"];
                if (strResponseCode == "1" && outputData.Count > 0)
                {
                    foreach (JObject item in outputData.OfType<JObject>())
                    {
                        strResDobStatus = (string)item["dob"];
                        strResNameStatus = (string)item["name"];
                        strResPanStatus = (string)item["pan_status"];
                        strResPan = (string)item["pan"];
                        strResSeedingStatus = (string)item["seeding_status"];
                    }
                }

                /*---------------------------------------------------------------------------------*/

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = "USP_NSDL_PAN_VALIDATION_LOG_AED"
                };
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ReqUserId", strPanUserId);
                cmd.Parameters.AddWithValue("@ReqTransactionId", strTransactionId);
                cmd.Parameters.AddWithValue("@ReqTime", strRequestTime);
                cmd.Parameters.AddWithValue("@ReqRecordCount", 1);
                cmd.Parameters.AddWithValue("@ReqApiVersion", 4);
                cmd.Parameters.AddWithValue("@ReqPan", strPAN);
                cmd.Parameters.AddWithValue("@ReqAppName", strName);
                cmd.Parameters.AddWithValue("@ReqDob", strDob);
                cmd.Parameters.AddWithValue("@ReqSignatureValue", strSignature);
                cmd.Parameters.AddWithValue("@ResUserId", strPanUserId);
                cmd.Parameters.AddWithValue("@ResRecordCount", 1);
                cmd.Parameters.AddWithValue("@ResTime", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
                cmd.Parameters.AddWithValue("@ResTransactionId", strTransactionId);
                cmd.Parameters.AddWithValue("@ResApiVersion", 4);
                cmd.Parameters.AddWithValue("@ResCode", Convert.ToInt32(strResponseCode));
                cmd.Parameters.AddWithValue("@ResPan", strResPan);
                cmd.Parameters.AddWithValue("@ResPanStatus", strResPanStatus);
                cmd.Parameters.AddWithValue("@ResAppName", strResNameStatus);
                cmd.Parameters.AddWithValue("@ResDob", strResDobStatus);
                cmd.Parameters.AddWithValue("@ResSeedingStatus", strResSeedingStatus);
                cmd.ExecuteNonQuery();

                return strResponseContent;
            }
            else
            {
                Util.LogRequestResponse("PanValidation", "GetPANStatusFromNSDL", "[ResponseStatusCode]:- " + objRestResponse.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PanValidation");
        }

        return strResponse;
    }

    /// <summary>
    /// This method is used to generate digital signatures for byte data.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="certificate"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
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

        /*-----------------------------------------------*/
        /// Setup the data to sign
        /*-----------------------------------------------*/
        ContentInfo content = new ContentInfo(data);
        SignedCms signedCms = new SignedCms(content, false);
        CmsSigner signer = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, certificate);

        /*-----------------------------------------------*/
        /// Create the signature
        /*-----------------------------------------------*/
        signedCms.ComputeSignature(signer);
        return signedCms.Encode();
    }

    /// <summary>
    /// Method used to accept all SSL certificates.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="certificate"></param>
    /// <param name="chain"></param>
    /// <param name="sslPolicyErrors"></param>
    /// <returns></returns>
    private bool AcceptAllCertifications(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }

    /// <summary>
    /// This method is used to convert two strings in JSON string format.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    string FormatJSON(string name, string value)
    {
        return "\"" + name + "\":" + "\"" + value + "\"";
    }

    /// <summary>
    /// This method is used generate numeric random number.
    /// </summary>
    /// <param name="intLength"></param>
    /// <returns></returns>
    public string MakeRandom(int intLength)
    {
        Thread.Sleep(10);
        string possibles = "0123456789";
        char[] passwords = new char[intLength];
        Random rd = new Random();

        for (int i = 0; i < intLength; i++)
        {
            passwords[i] = possibles[rd.Next(0, possibles.Length)];
        }
        return new string(passwords);
    }
}