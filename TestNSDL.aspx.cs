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

public partial class TestNSDL : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
}