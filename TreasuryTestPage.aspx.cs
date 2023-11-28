using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityLayer.Proposal;
using System.Data.SqlClient;
using BusinessLogicLayer.Proposal;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;

public partial class TreasuryTestPage : System.Web.UI.Page
{
    SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    ProposalBAL objService = new ProposalBAL();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //public string ServicePaymentSchedule()
    //{
    //    string strRes = "";
    //    SqlCommand objCommand = new SqlCommand();
    //    SqlDataAdapter objDa = new SqlDataAdapter();
    //    DataTable objds = new DataTable();

    //    if (objConn.State == ConnectionState.Closed)
    //    {
    //        objConn.Open();
    //    }
    //    try
    //    {
            
    //        string ordernumber = "ES2020073041000025-310720052725";
    //        string amount = "1000.00";
    //        String sDataToSend;
    //        sDataToSend = "IND|" + ordernumber + "|" + amount;
    //        //Find Checksum
    //        string INCRRIPT = HmacSHA256(sDataToSend);
    //        //Concatenate calculated checksum in Data to send
    //        string sDataToSendWithCheckSum = sDataToSend + "|" + INCRRIPT;
    //        //Encrypt the data
    //        string sDataToSendWithCheckSumEncription = encrypt(sDataToSendWithCheckSum);

    //        strRes=GetData(sDataToSendWithCheckSumEncription);
    //        //string Url = "https://uat.odishatreasury.gov.in/echallan/depts2sresponse";
    //        //WebClient webClient = new WebClient();

           

    //        //string DATA = "IND|" + ordernumber + "|" + amount + "";
    //        //string INCRRIPT = HmacSHA256(DATA, "c3c6e92a");
    //        //string datta = DATA + "|" + INCRRIPT;
    //        //datta = encrypt(datta, "c3c6e92a");

    //        //string Incript = "?deptCode=IND&msg=";
    //        //Incript += HttpUtility.UrlEncode(datta); 
    //        //Url += Incript;

    //        //string Result = GetData(Url);

    //        //string DecriptResult = decrypt(Result);
    //        //string[] result = DecriptResult.Split('|');

    //        //string deptcode = "IND";
    //        //string uri = "https://uat.odishatreasury.gov.in/echallan/depts2sresponse?deptCode="+ deptcode + "&msg=" + datta;
    //        //ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
    //        //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
    //        //webRequest.ContentType = "application/json";
    //        //// To allow TLS1.0, TLS1.1, TLS1.2
    //        //ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
    //        //webRequest.KeepAlive = false;
    //        //webRequest.Method = "POST";
    //        //HttpWebResponse response;
    //        //try
    //        //{
    //        //    response = (HttpWebResponse)webRequest.GetResponse();
    //        //    StreamReader reader = new StreamReader(response.GetResponseStream());
    //        //    string body = reader.ReadToEnd();
    //        //}
    //        //catch (WebException wex)
    //        //{
    //        //    response = (HttpWebResponse)wex.Response;
    //        //}
    //    }
    //    catch (Exception ex)
    //    {
    //        strRes = ex.Message.ToString();
    //    }
    //    finally
    //    {
    //        objConn.Close();
    //        objCommand = null;
    //        objds = null;
    //    }
    //    return strRes;
    //}

    /// <summary>
    /// ////// For PEAL
    /// </summary>
    //public string PealPaymentSchedule()
    //{
    //    string strRes = "";
    //    SqlCommand objCommand = new SqlCommand();
    //    SqlDataAdapter objDa = new SqlDataAdapter();
    //    DataTable objds = new DataTable();

    //    if (objConn.State == ConnectionState.Closed)
    //    {
    //        objConn.Open();
    //    }
    //    try
    //    {
    //        //objCommand.CommandText = "USP_FETCH_PAYMENT_ORDER";
    //        //objCommand.CommandType = CommandType.StoredProcedure;
    //        //objCommand.Connection = objConn;

    //        //objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "P");

    //        //objDa.SelectCommand = objCommand;
    //        //objDa.Fill(objds);

    //        //if (objds.Rows.Count > 0)
    //        //{
    //        //    for (int i = 0; i < objds.Rows.Count; i++)
    //        //    {
    //        //        string ordernumber = Convert.ToString(objds.Rows[i]["vchOrderNo"]);
    //        //        string amount = Convert.ToString(objds.Rows[i]["vchChallanAmount"]);

    //        //        TreasuryService.IndVerificationService objTreasurySrv = new TreasuryService.IndVerificationService();
    //        //        string retVal = objTreasurySrv.getStatus(ordernumber, amount);

    //        //        if (retVal.Contains('|'))
    //        //        {
    //        //            string strBankTransStatus = retVal.Split('|')[7].Split('=')[1].ToString();
    //        //            if (strBankTransStatus == "S")
    //        //            {
    //        //                PromoterDet objPromoter = new PromoterDet();
    //        //                objPromoter.strAction = "A";

    //        //                objPromoter.bankname = retVal.Split('|')[0].Split('=')[1].ToString();
    //        //                objPromoter.ifsccode = retVal.Split('|')[1].Split('=')[1].ToString();
    //        //                objPromoter.dealername = retVal.Split('|')[2].Split('=')[1].ToString();
    //        //                objPromoter.ordernumber = retVal.Split('|')[3].Split('=')[1].ToString();
    //        //                objPromoter.bankcode = retVal.Split('|')[4].Split('=')[1].ToString();
    //        //                objPromoter.treasuryrefno = retVal.Split('|')[5].Split('=')[1].ToString();
    //        //                objPromoter.banktranstimestamp = retVal.Split('|')[6].Split('=')[1].ToString();
    //        //                objPromoter.banktransstatus = retVal.Split('|')[7].Split('=')[1].ToString();

    //        //                string result = objService.PEALServiceOrderUpdate(objPromoter);

    //        //                strRes = result;
    //        //            }
    //        //        }
    //        //    }
    //        //}
    //        string ordernumber = "0408201048175002020022014001";
    //        //string ordernumber = "2504190929005002019041247002";
    //        string amount = "20000.00";            
    //        string sDataToSend;
    //        sDataToSend = "IND|" + ordernumber + "|" + amount;
    //        //Find Checksum
    //        string INCRRIPT = HmacSHA256(sDataToSend);
    //        //Concatenate calculated checksum in Data to send
    //        string sDataToSendWithCheckSum = sDataToSend + "|" + INCRRIPT;
    //        //Encrypt the data
    //        string sDataToSendWithCheckSumEncription = encrypt(sDataToSendWithCheckSum);

    //        strRes = GetData(HttpUtility.UrlEncode(sDataToSendWithCheckSumEncription));
    //    }
    //    catch (Exception ex)
    //    {
    //        strRes = ex.Message.ToString();
    //    }
    //    finally
    //    {
    //        objConn.Close();
    //        objCommand = null;
    //        objds = null;
    //    }

    //    return strRes;
    //}


    protected void btnSave_Click(object sender, EventArgs e)
    {
       PaymentScheduler ob = new PaymentScheduler();
       ob.ServicePaymentSchedule();       
    }
    protected void btnPeal_Click(object sender, EventArgs e)
    {
        PaymentScheduler ob = new PaymentScheduler();
        ob.PealPaymentSchedule();       
    }
    //public string GetData(string sDataToSendWithCheckSumEncription)
    //{

    //    string uri = "https://uat.odishatreasury.gov.in/echallan/depts2sresponse?deptCode=IND&msg=" + sDataToSendWithCheckSumEncription;
    //    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
    //    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
    //    webRequest.ContentType = "application/json";
    //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00); 
    //    webRequest.KeepAlive = false;
    //    webRequest.Method = "POST";
    //    HttpWebResponse response;
    //    string results = "";
    //    try
    //    {
    //        response = (HttpWebResponse)webRequest.GetResponse();
    //        StreamReader reader = new StreamReader(response.GetResponseStream());
    //        string sbody = reader.ReadToEnd();

    //        //Data decrypted
    //        string deresult = decrypt(sbody);
    //        //Pipe eparated data plit into array
    //        string DecodeValue = HttpUtility.UrlDecode(deresult);
    //        string[] result = DecodeValue.Split('|');

    //        //Add all data of reult to another pipe separated string except checksum value
    //        string bchecksumdata = "";
    //        for (int j = 0; j < result.Length - 1; j++)
    //        {
    //            bchecksumdata += result[j] + "|";
    //        }

    //        //Remove last paipe

    //        bchecksumdata = bchecksumdata.Remove(bchecksumdata.Length - 1, 1);

    //        string lblbeforchecksum, checksumvalue, convchecksumvalue;
    //        lblbeforchecksum = bchecksumdata;
    //        // Store the checkum value received from treasury department
    //        checksumvalue = result[result.Length - 1].ToString();

    //        //Calculate the checksum based on actual data recevied from trasury except checksum
    //        convchecksumvalue = HmacSHA256(bchecksumdata);

    //        //If calualted checksum and received checksum value is same
    //        if (checksumvalue == convchecksumvalue)
    //        {
    //            //Pass array with 44 index to the function  and store the data in databse
    //            // uUpdatePaymentStatu(result);
    //            //uDisplayData(result);
    //            results = lblbeforchecksum;
    //        }

    //    }

    //    catch (WebException wex)
    //    {
    //        response = (HttpWebResponse)wex.Response;
    //    }
    //    return results;

    //}
    //public string GetData(string url)
    //{

    //    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
    //    HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
    //    webRequest.ContentType = "application/json";
    //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
    //    webRequest.KeepAlive = false;
    //    webRequest.Method = "POST";
    //    HttpWebResponse response;
    //    try
    //    {
    //        response = (HttpWebResponse)webRequest.GetResponse();
    //        StreamReader reader = new StreamReader(response.GetResponseStream());
    //        string body = reader.ReadToEnd();
    //        return body;
    //    }
    //    catch (WebException wex)
    //    {
    //        response = (HttpWebResponse)wex.Response;
    //    }
    //    return "";
    //}

    //private static byte[] GetFileBytes(String filename)
    //{
    //    if (!File.Exists(filename))
    //        return null;
    //    Stream stream = new FileStream(filename, FileMode.Open);
    //    int datalen = (int)stream.Length;
    //    byte[] filebytes = new byte[datalen];
    //    stream.Seek(0, SeekOrigin.Begin);
    //    stream.Read(filebytes, 0, datalen);
    //    stream.Close();
    //    return filebytes;
    //}

    //private string HmacSHA256(string message)
    //{
    //    //secret = secret ?? "";
    //    var encoding = new System.Text.ASCIIEncoding();
    //    byte[] keyByte = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key"); //encoding.GetBytes(secret);
    //    byte[] messageBytes = encoding.GetBytes(message);
    //    using (var hmacsha256 = new HMACSHA256(keyByte))
    //    {
    //        byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
    //        return Convert.ToBase64String(hashmessage);
    //    }
    //}

    //public string encrypt(string plainText)
    //{

    //    System.Text.UTF32Encoding UTF32 = new System.Text.UTF32Encoding();
    //    AesManaged tdes = new AesManaged();
    //    tdes.Key = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
    //    tdes.Mode = CipherMode.ECB;
    //    tdes.Padding = PaddingMode.PKCS7;
    //    ICryptoTransform crypt = tdes.CreateEncryptor();
    //    byte[] plain = Encoding.UTF8.GetBytes(plainText);
    //    byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
    //    return Convert.ToBase64String(cipher);

    //}

    //public String decrypt(String value)
    //{
    //    System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
    //    AesManaged tdes = new AesManaged();
    //    tdes.Key = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
    //    tdes.Mode = CipherMode.ECB;
    //    tdes.Padding = PaddingMode.PKCS7;
    //    ICryptoTransform crypt = tdes.CreateDecryptor();
    //    byte[] plain = Convert.FromBase64String(value);
    //    byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
    //    String encryptedText = Encoding.UTF8.GetString(cipher);
    //    return encryptedText;
    //}
}