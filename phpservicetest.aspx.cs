using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Web.Script.Serialization;


public partial class phpservicetest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       // GetDefaulterList();
        Method1();
      return;
      ///  SPMG();
    }

    public void SPMG()
    {
        ServicePointManager.Expect100Continue = true;

        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        string strrandomgen = MakeRandom(10);

        var plainran = Encoding.UTF8.GetBytes(strrandomgen);
        string randno = Convert.ToBase64String(plainran);

        TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        double unixTime = span.TotalSeconds;
        var plainut = Encoding.UTF8.GetBytes(unixTime.ToString());

        string plunixtime = Convert.ToBase64String(plainut);

        string ranpss = strrandomgen + "$CSDa2017@shbo@rD$Ipic)L" + unixTime.ToString();

        SHA256 mySHA256 = SHA256Managed.Create();

        //  hashValue = mySHA256.ComputeHash(fileStream);

        string finalstr = GetSha256FromString(ranpss);
        // var plainranpss = Encoding.UTF8.GetBytes(ranpss);

        byte[] bytes = Encoding.UTF8.GetBytes(finalstr);
        string ranpss1 = Convert.ToBase64String(bytes);
        #region test
        // byte[] bytes1 = Encoding.Default.GetBytes(ranpss);
        //string myString1 = Encoding.UTF8.GetString(bytes1);
        // string finalstr = sha256(ranpss1);
        Response.Write("Random no<br />");
        Response.Write(randno);
        Response.Write("<br />");
        Response.Write("Timestamp<br />");

        Response.Write(plunixtime);
        Response.Write("<br />");
        Response.Write("PasswordDigest<br />");
        Response.Write(ranpss1);

        string serviceUrl = "https://esuvidha.gov.in/odisha/restservices/RestServer.php?view=issuesummary";
        object input = new
        {
            //Parameters that need to pass//
            //serviceId = 21,
            //applicationnumber = "2017072145",
            //Str_Invstrname = "Prasun",
            //applicationStatus = 1,
            //userId = "investor1",
            //industrycode = "PROPOS0012",
            //Paymentstatus = 0,
            //Paymenttransactionid = "ydfgyu982dfh",
            //Dec_PaymentAmnt = 5000,
            //ULBCode = "ULB001"

            RandomNonce = randno,
            TimeStamp = plunixtime,
            PasswordDigest = ranpss1,
            FinancialYear = "2017"

        };
        string inputJson = (new JavaScriptSerializer()).Serialize(input);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
        request.Method = "POST";
        //SampleModel model = new SampleModel();
        //model.PostData = "Test";
        request.ContentType = "application/json";

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        HttpWebResponse response;
        using (var sw = new StreamWriter(request.GetRequestStream()))
        {
            string json = serializer.Serialize(inputJson);
            sw.Write(json);
            sw.Flush();
            //response = (HttpWebResponse)request.GetResponse();
        }




        //HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
        //httpRequest.Accept = "application/json";
        //httpRequest.ContentType = "application/json";
        //httpRequest.Method = "POST";

        //byte[] bytes1 = Encoding.UTF8.GetBytes(inputJson);

        //using (StreamWriter stream = new StreamWriter( httpRequest.GetRequestStream()))
        //{
        //    stream.Write(inputJson);
        //    //stream.Write(bytes1, 0, bytes1.Length);
        //   stream.Close();
        //}

        //using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
        //{
        //    using (Stream stream = httpResponse.GetResponseStream())
        //    {
        //        Label1.Text = (new StreamReader(stream)).ReadToEnd();
        //    }
        //}
        #endregion

        //OSDAResponse objResponse = OSDAProxy.CourseMasterAdd("2017", randno, plunixtime, ranpss1);
    }

    public void Method1()
    {
        try
        {
            string strrandomgen = MakeRandom(10);

            var plainran = Encoding.UTF8.GetBytes(strrandomgen);
            string randno = Convert.ToBase64String(plainran);

            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            double unixTime = span.TotalSeconds;
            var plainut = Encoding.UTF8.GetBytes(unixTime.ToString());

            string plunixtime = Convert.ToBase64String(plainut);

            string ranpss = strrandomgen + "$CSDa2017@shbo@rD$Ipic)L" + unixTime.ToString();

            SHA256 mySHA256 = SHA256Managed.Create();

            //  hashValue = mySHA256.ComputeHash(fileStream);

            string finalstr = GetSha256FromString(ranpss);
            // var plainranpss = Encoding.UTF8.GetBytes(ranpss);

            byte[] bytes = Encoding.UTF8.GetBytes(finalstr);
            string ranpss1 = Convert.ToBase64String(bytes);


            string serviceUrl = "https://esuvidha.gov.in/odisha/restservices/RestServer.php?view=issuesummary";
            object input = new
            {


                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
                FinancialYear = "2017"

            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);



            var httpWebRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(input);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Response.Write("Method 1:" + result);
            }
        }
        catch (Exception ex)
        {
            Response.Write("Method 1:" + ex.Message);
        }
    }
    public static string GetSha256FromString(string strData)
    {
        var message = Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }
   
    public string MakeRandom(int pl)
    {
        string possibles = "0123456789";
        char[] passwords = new char[pl];
        Random rd = new Random();

        for (int i = 0; i < pl; i++)
        {
            passwords[i] = possibles[rd.Next(0, possibles.Length)];
        }
        return new string(passwords);
    }
    public string GetDefaulterList()
    {
        string strrandomgen = MakeRandom(10);
        var plainran = Encoding.UTF8.GetBytes(strrandomgen);
        string randno = Convert.ToBase64String(plainran);
        TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        double unixTime = span.TotalSeconds;
        var plainut = Encoding.UTF8.GetBytes(unixTime.ToString());
        string plunixtime = Convert.ToBase64String(plainut);
        string ranpss = strrandomgen + "$CSDa2017@shbo@rD$Ipic)L" + unixTime.ToString();
        SHA256 mySHA256 = SHA256Managed.Create();
        //  hashValue = mySHA256.ComputeHash(fileStream);
        string finalstr = GetSha256FromString(ranpss);
        // var plainranpss = Encoding.UTF8.GetBytes(ranpss);

        byte[] bytes = Encoding.UTF8.GetBytes(finalstr);
        string ranpss1 = Convert.ToBase64String(bytes);
        string strDefaulterList = string.Empty;
        // Create a request for the URL. 
        object input = new
        {
            RandomNonce = randno,
            TimeStamp = plunixtime,
            PasswordDigest = ranpss1,
            FinancialYear = "2017"

        };
        string inputJson = (new JavaScriptSerializer()).Serialize(input);
        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };

        string nicServicePath = "https://esuvidha.gov.in/odisha/restservices/RestServer.php?view=issuesummary";
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        System.Net.WebRequest request = System.Net.WebRequest.Create(nicServicePath);

        request.Method = "POST";
        request.ContentType = "application/json"; //place MIME type here
        request.ContentLength = inputJson.Length;
        string json = serializer.Serialize(inputJson);
        if (!string.IsNullOrEmpty(json))
        {
            var encoding = new UTF8Encoding();
            bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(json);
            request.ContentLength = bytes.Length;

            using (var writeStream = request.GetRequestStream())
            {
                writeStream.Write(bytes, 0, bytes.Length);
            }
        }
        
       
       
        // If required by the server, set the credentials.
        request.Credentials = System.Net.CredentialCache.DefaultCredentials;
        request.Timeout = int.MaxValue;
        // Get the response.
        System.Net.WebResponse response = request.GetResponse();

        // Display the status.
        Console.WriteLine(((System.Net.HttpWebResponse)response).StatusDescription);

        // Get the stream containing content returned by the server.
        System.IO.Stream dataStream = response.GetResponseStream();

        // Open the stream using a StreamReader for easy access.
        System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);

        // Read the content.
        strDefaulterList = reader.ReadToEnd();

        // Clean up the streams and the response.
        reader.Close();
        response.Close();

        return strDefaulterList;
    }
}