using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for PANValidationNSDL
/// </summary>
public class PANValidationNSDL
{
    public PANValidationNSDL()
    {
        //
        // TODO: Add constructor logic here
        //
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
            throw ex;
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
}