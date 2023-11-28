using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.IO;
using System.Text;
using System.Net;

/// <summary>
/// Summary description for TreasuryPaymentTracking
/// </summary>
public class TreasuryPaymentTracking
{
    public TreasuryPaymentTracking()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetPaymentStatusFromTreasury(string strOrderNo, decimal decAmount)
    {
        string results = "";
        try
        {
            ///Input Data
            string strDataToSend = "IND|" + strOrderNo + "|" + decAmount;

            /*--------------------------------------------------------*/
            /// Write input request to the treasury in log file.
            /*--------------------------------------------------------*/
            Util.LogRequestResponse("PaymentScheduler", "GetPaymentStatusFromTreasury", "[INPUT_REQUEST]:- " + strDataToSend);

            ///Find Checksum
            string strChecksum = HmacSHA256(strDataToSend);

            ///Concatenate the checksum value with the data to be sent 
            string sDataToSendWithCheckSum = strDataToSend + "|" + strChecksum;

            ///Encrypt the data
            string sDataToSendWithCheckSumEncryption = Encrypt(sDataToSendWithCheckSum);
            string sDataToSendWithCheckSumEncryptionUrlEncode = HttpUtility.UrlEncode(sDataToSendWithCheckSumEncryption);

            string uri = "" + ConfigurationManager.AppSettings["VerificationTreasuryUrl"].ToString() + "?deptCode=IND&msg=" + sDataToSendWithCheckSumEncryptionUrlEncode;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.ContentType = "application/json";
            webRequest.UserAgent = ".NET Framework 4.0";
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            webRequest.KeepAlive = false;
            webRequest.Method = "POST";

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string strTreasuryResponse = reader.ReadToEnd();

            ///Data decrypted
            string strDecryptResponse = Decrypt(strTreasuryResponse);

            /*--------------------------------------------------------*/
            ///Write output response coming from treasury
            /*--------------------------------------------------------*/
            Util.LogRequestResponse("PaymentScheduler", "GetPaymentStatusFromTreasury", "[OUTPUT_RESPONSE][SUCCESS]:- " + strDecryptResponse);

            //Data URL Decode
            //string URLDecode = HttpUtility.UrlDecode(strDecryptResponse);

            ///Pipe separated data split into array
            string[] arrResult = strDecryptResponse.Split('|');

            ///Add all data of result to another pipe separated string except checksum value
            string strPipeSeparatedDataTreasury = "";
            for (int j = 0; j < arrResult.Length - 1; j++)
            {
                strPipeSeparatedDataTreasury += arrResult[j] + "|";
            }

            ///Remove the last pipe
            strPipeSeparatedDataTreasury = strPipeSeparatedDataTreasury.Remove(strPipeSeparatedDataTreasury.Length - 1, 1);

            ///Store the checkum value received from treasury department
            string strChecksumValueFromTreasury = arrResult[arrResult.Length - 1].ToString();

            ///Calculate the checksum based on actual data recevied from treasury except checksum
            string strConvertedChecksumValue = HmacSHA256(strPipeSeparatedDataTreasury);

            ///If calualted checksum and received checksum value is same
            if (strChecksumValueFromTreasury == strConvertedChecksumValue)
            {
                results = strPipeSeparatedDataTreasury;
            }
        }
        catch (Exception ex)
        {
            Util.LogRequestResponse("PaymentScheduler", "GetPaymentStatusFromTreasury", "[OUTPUT_RESPONSE][FAILURE]:- " + ex.Message.ToString());
            Util.LogError(ex, "PaymentScheduler");
            throw ex;
        }

        return results;
    }

    private static byte[] GetFileBytes(String filename)
    {
        if (!File.Exists(filename))
            return null;
        Stream stream = new FileStream(filename, FileMode.Open);
        int datalen = (int)stream.Length;
        byte[] filebytes = new byte[datalen];
        stream.Seek(0, SeekOrigin.Begin);
        stream.Read(filebytes, 0, datalen);
        stream.Close();
        return filebytes;
    }
    private string HmacSHA256(string message)
    {
        //secret = secret ?? "";
        string Path = ConfigurationManager.AppSettings["IFMSKeyPath"].ToString();
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = GetFileBytes(Path); //encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }
    public string Encrypt(string plainText)
    {
        string Path = ConfigurationManager.AppSettings["IFMSKeyPath"].ToString();
        System.Text.UTF32Encoding UTF32 = new System.Text.UTF32Encoding();
        AesManaged tdes = new AesManaged();
        tdes.Key = GetFileBytes(Path);
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform crypt = tdes.CreateEncryptor();
        byte[] plain = Encoding.UTF8.GetBytes(plainText);
        byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
        return Convert.ToBase64String(cipher);

    }
    public string Decrypt(String value)
    {
        string Path = ConfigurationManager.AppSettings["IFMSKeyPath"].ToString();
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        AesManaged tdes = new AesManaged();
        tdes.Key = GetFileBytes(Path);
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform crypt = tdes.CreateDecryptor();
        byte[] plain = Convert.FromBase64String(value);
        byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
        String encryptedText = Encoding.UTF8.GetString(cipher);
        return encryptedText;
    }
}