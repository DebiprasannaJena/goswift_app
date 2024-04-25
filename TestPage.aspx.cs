using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using RestSharp;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class TestPage : System.Web.UI.Page
{
    //SWPDashboard objSWP = new SWPDashboard();
    //DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    //CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();
    SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        //string strSuppliedKey = "?åLˆ'KX¾p ;™¶%M8º}ÌqE-ƒU§©	;±½";

        //byte[] key = { };
        //key = Encoding.ASCII.GetBytes(strSuppliedKey);       

        //string strplaintext = "SUSHANT JENA|9090243166|65|123456789";
        //string strenc = encrypt(strplaintext, key);

        //DWHServiceReference.DWHServiceHostClient validate = new DWHServiceReference.DWHServiceHostClient();
        //string strEncryptionKey = "2f00df86-01f4-4869-a770-2f362ff1afa4";
        //string strAccessKey = validate.KeyEncryption(strEncryptionKey);  

        //DWHServiceReference.DWHServiceHostClient dd = new DWHServiceReference.DWHServiceHostClient();
        //string Key = dd.GetApplicationKey(7, strAccessKey);

        //string url = validate.AppRedirectURL(Key, "D48225DB-3125-438B-AB59-6580099F1737", validate.URLEncryption("2f00df86-01f4-4869-a770-2f362ff1afa4"), "1", "1243");
        //string[] splitUrl = url.Split('?');
        //string param = splitUrl[1].Remove(0, 6);
        //String token = validate.GetTokeFromQueryString(param, validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]));

        decryptnew("0cYZNh3EHVIk8SgBHKA9X9Fuu3Z0LMgoMtfjLdWB7K2c8eKZU60iaRVMbiMh3PSL0mVbQieDrsYr7gGme95eNw%3D%3D");

    }


    static string encrypt(string plainText, byte[] Key)
    {
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        byte[] encrypted;
        string base64encrypted;
        using (AesManaged aesAlg = new AesManaged())
        {
            aesAlg.Key = Key;
            aesAlg.Mode = CipherMode.ECB;
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }
        base64encrypted = Convert.ToBase64String(encrypted, 0, encrypted.Length);
        return base64encrypted.Replace("/", "-");
    }

    protected void Btn_Payment_Service_Test_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    TreasuryService.IndVerificationService objTreasurySrv = new TreasuryService.IndVerificationService();
        //    string ordernumber = Txt_Order_No.Text.Trim();
        //    string amount = Txt_Challan_Amount.Text;

        //    string retVal = objTreasurySrv.getStatus(ordernumber, amount);

        //    Lbl_Payment_Service_Response.Text = retVal;
        //    Lbl_Payment_Service_Response.ForeColor = System.Drawing.Color.Blue;
        //}
        //catch (Exception ex)
        //{
        //    Lbl_Payment_Service_Response.Text = ex.Message.ToString();
        //    Lbl_Payment_Service_Response.ForeColor = System.Drawing.Color.Red;
        //}
        Lbl_Payment_Service_Response.Text = "New Treasury Code Only Restfull";
        Lbl_Payment_Service_Response.ForeColor = System.Drawing.Color.Red;
    }


    /////Test Treasury Payment RestFul Service     
    protected void Btn_Pay_REST_Click(object sender, EventArgs e)
    {
        Lbl_Msg_Restful.Text = "";
        string strResult = "";
        try
        {
            string sDataToSend;
            sDataToSend = "IND|" + Txt_Order_No_REST.Text + "|" + Txt_Challan_Amount_REST.Text;
            //Find Checksum
            string INCRRIPT = HmacSHA256(sDataToSend);
            //Concatenate calculated checksum in Data to send
            string sDataToSendWithCheckSum = sDataToSend + "|" + INCRRIPT;
            //Encrypt the data
            string sDataToSendWithCheckSumEncription = encrypt(sDataToSendWithCheckSum);
            string sDataToSendWithCheckSumEncriptionurlencode = HttpUtility.UrlEncode(sDataToSendWithCheckSumEncription);
            strResult = GetData(sDataToSendWithCheckSumEncriptionurlencode);
            Lbl_Msg_Restful.Text = strResult;
            Lbl_Msg_Restful.ForeColor = System.Drawing.Color.Blue;
        }
        catch
        {
            Lbl_Msg_Restful.Text = strResult;
            Lbl_Msg_Restful.ForeColor = System.Drawing.Color.Red;
        }

    }

    /////Test CRM RestFul Service (For GOSWIFT Child Service)
    protected void Btn_CRM_Service_Click(object sender, EventArgs e)
    {
        string URL = "http://192.168.201.66/swp_service/CRMService.svc/getServiceInfo";

        // string DATA = @"{""strSSOId"":""d48225db-3125-438b-ab59-6580099f1737"",""strSecurityKey"":""4FA78FE4-EB41-40DB-9BE2-7DE6DF6B99CC""}";

        string DATA = "{\"strSSOId\":\"" + Txt_SSO_Id.Text + "\",\"strSecurityKey\":\"4FA78FE4-EB41-40DB-9BE2-7DE6DF6B99CC\"}";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = DATA.Length;
        StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
        requestWriter.Write(DATA);
        requestWriter.Close();

        try
        {
            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();
            Lbl_CRM_Response.Text = response;
            responseReader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /////Test CRM RestFul Service (For GOSWIFT PEAL)    
    protected void Btn_CRM_PEAL_Click(object sender, EventArgs e)
    {
        string URL = "http://192.168.201.66/swp_service/CRMService.svc/getPEALInfo";
        //string DATA = @"{""strSSOId"":""d48225db-3125-438b-ab59-6580099f1737"",""strSecurityKey"":""4FA78FE4-EB41-40DB-9BE2-7DE6DF6B99CC""}";
        string DATA = "{\"strSSOId\":\"" + Txt_SSO_Id.Text + "\",\"strSecurityKey\":\"4FA78FE4-EB41-40DB-9BE2-7DE6DF6B99CC\"}";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = DATA.Length;
        StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
        requestWriter.Write(DATA);
        requestWriter.Close();

        try
        {
            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();
            Lbl_CRM_Response.Text = response;
            responseReader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public string GetData(string sDataToSendWithCheckSumEncription)
    {
        string results = "";
        string uri = "https://www.odishatreasury.gov.in/echallan/depts2sresponse?deptCode=IND&msg=" + sDataToSendWithCheckSumEncription;
        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(uri);
        webRequest.ContentType = "application/json";
        webRequest.UserAgent = ".NET Framework 4.0";
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
        webRequest.KeepAlive = false;
        webRequest.Method = "POST";
        HttpWebResponse response;
        try
        {
            response = (HttpWebResponse)webRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string sbody = reader.ReadToEnd();
            //Data decrypted
            string deresult = decrypt(sbody);
            //Data URL Decode
            //string URLDecode = HttpUtility.UrlDecode(deresult);
            //Pipe eparated data plit into array
            string[] result = deresult.Split('|');
            //Add all data of reult to another pipe separated string except checksum value
            string bchecksumdata = "";
            for (int j = 0; j < result.Length - 1; j++)
            {
                bchecksumdata += result[j] + "|";
            }

            //Remove last paipe

            bchecksumdata = bchecksumdata.Remove(bchecksumdata.Length - 1, 1);

            string lblbeforchecksum, checksumvalue, convchecksumvalue;
            lblbeforchecksum = bchecksumdata;
            // Store the checkum value received from treasury department
            checksumvalue = result[result.Length - 1].ToString();

            //Calculate the checksum based on actual data recevied from trasury except checksum
            convchecksumvalue = HmacSHA256(bchecksumdata);
            //If calualted checksum and received checksum value is same
            if (checksumvalue == convchecksumvalue)
            {
                results = lblbeforchecksum;
            }
        }
        catch (Exception ex)
        {
            results = ex.Message;
            Util.LogError(ex, "Testpage");
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
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key"); //encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }

    public string encrypt(string plainText)
    {

        System.Text.UTF32Encoding UTF32 = new System.Text.UTF32Encoding();
        AesManaged tdes = new AesManaged();
        tdes.Key = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform crypt = tdes.CreateEncryptor();
        byte[] plain = Encoding.UTF8.GetBytes(plainText);
        byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
        return Convert.ToBase64String(cipher);

    }

    public String decrypt(String value)
    {
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        AesManaged tdes = new AesManaged();
        tdes.Key = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform crypt = tdes.CreateDecryptor();
        byte[] plain = Convert.FromBase64String(value);
        byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
        String encryptedText = Encoding.UTF8.GetString(cipher);
        return encryptedText;
    }

    public String decryptnew(String value)
    {
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        AesManaged tdes = new AesManaged();
        string key = "S!aM#s$PgA&pP(lI&cA@t!)Io#Np@R$d";
        byte[] barrImg = Encoding.ASCII.GetBytes(key); //(byte[])key;
        tdes.Key = barrImg; //GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform crypt = tdes.CreateDecryptor();
        // byte[] plain = Convert.FromBase64String(value);
        // byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
        //  String encryptedText = Encoding.UTF8.GetString(cipher);
        String encryptedText = null;
        return encryptedText;
    }


    protected void Btn_Excise_OSBC_SignUp_Click(object sender, EventArgs e)
    {
        try
        {
            Label1.Text = "";
            HyperLink1.Text = "";

            ExciseOSBCServiceReference.OSBCSoftSoapClient objEx = new ExciseOSBCServiceReference.OSBCSoftSoapClient();
            ExciseOSBCServiceReference.SupDetails objEntity = new ExciseOSBCServiceReference.SupDetails();

            objEntity.Application_No = "EX123456789";
            objEntity.GoSwiftUserID = "123";
            objEntity.MobileNo = "8249761028";
            objEntity.Name = "Sushant";
            objEntity.Email = "sushant@ggmail.com";
            objEntity.Sector_Type = "NA";
            objEntity.Sector_Subtype = "NA";
            objEntity.ServiceID = "69";
            objEntity.Source = "GOSWIFT";
            objEntity.Active_Status = "Yes";

            string strReturnVal = objEx.AESEncryptForSignUP(objEntity);
            Label1.Text = "Encrypted String :-   " + strReturnVal;

            //   https://osbc.co.in/HTTP_PUBLIC/GoSwift/GoSwiftLanding.aspx

            HyperLink1.NavigateUrl = "http://117.239.112.221/HTTP_PUBLIC/GoSwift/GoSwiftLanding.aspx?encData=" + strReturnVal;
            HyperLink1.Text = "http://117.239.112.221/HTTP_PUBLIC/GoSwift/GoSwiftLanding.aspx?encData=" + strReturnVal;

            // Response.Redirect("http://117.239.112.221/goSwiftLanding.aspx?encData=" + strReturnVal);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string URL = "http://192.168.201.129/SWP_Service/PollutionControl.svc/GetUserProfilesPM";

        string DATA = "{\"StrProposalID\":\"0\",\"strUserID\":\"81\"}";

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
        request.Method = "POST";
        request.ContentType = "application/json";
        request.ContentLength = DATA.Length;
        StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
        requestWriter.Write(DATA);
        requestWriter.Close();

        try
        {
            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();
            Label2.Text = response;
            responseReader.Close();
        }
        catch (Exception ex)
        {
            Label2.Text = ex.Message.ToString();
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {

        try
        {
            string strFromDate = "2020-01-01";// DateTime.Now.ToString("yyyy-MM-dd");
            string strToDate = DateTime.Now.ToString("yyyy-MM-dd");

            var client = new RestClient("http://192.168.201.135:7001/OMVD_LIVE/mobileProxy");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            // request.AddHeader("Cookie", "PHPSESSID=26o25isrnbn3k3nu49i2mgq5bp");
            request.AddParameter("application/json", "{\r\n    \"method\":\"allFeedbacks\",\r\n    \"strFromDate\":\"" + strFromDate + "\",\r\n    \"strToDate\":\"" + strToDate + "\",\r\n    \"intReadStatus\":\"0\"\r\n}\r\n\r\n", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Response.Write(response.Content);
            // Console.WriteLine(response.Content);

            string JSON = response.Content;
            var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
            string strRetStatus = dict["status"].ToString();
            //var xx = dict["returnData"]["Value"].ToString();

            //var dict1 = dict.SelectMany(d => d).ToDictionary(p => p.Key, p => p.Value);
            //var adminId = dict["Administrator"];



            if (strRetStatus == "200")
            {
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(JSON);

                string xx = myDeserializedClass.status.ToString();

                if (xx == "200")
                {
                    for (int i = 0; i < myDeserializedClass.returnData.Count; i++)
                    {
                        string strId = myDeserializedClass.returnData[i].id.ToString();
                        string strName = myDeserializedClass.returnData[i].strName.ToString();
                        string strEmail = myDeserializedClass.returnData[i].strEmail.ToString();
                        string strMobileNo = myDeserializedClass.returnData[i].strMobileNo.ToString();
                        string strSubject = myDeserializedClass.returnData[i].strSubject.ToString();
                        string strFeedback = myDeserializedClass.returnData[i].strFeedback.ToString();
                        string createdonDate = myDeserializedClass.returnData[i].createdonDate.ToString();
                        string readStatus = myDeserializedClass.returnData[i].readStatus.ToString();


                    }
                }

            }

        }
        catch (Exception)
        {
            throw;
        }

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //byte[] data;
        //using (WebClient client = new WebClient())
        //{
        //    string strAddress = "E:\\Sushant\\355443543545435.pdf";
        //    data = client.DownloadData(strAddress);
        //}

        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=labtest.pdf");
        //Response.Buffer = true;
        //ms.WriteTo(Response.OutputStream);
        //Response.End();



        string filePath = "E:\\Sushant\\355443543545435.pdf";
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition: inline", "filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();


        //string actualFilePath = HttpContext.Current.Server.MapPath(filePath);
        // FileInfo fi = new FileInfo(actualFilePath);
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ReturnData
    {
        public string id { get; set; }
        public string strName { get; set; }
        public string strEmail { get; set; }
        public string strMobileNo { get; set; }
        public string strSubject { get; set; }
        public string strFeedback { get; set; }
        public string createdonDate { get; set; }
        public string readStatus { get; set; }
    }

    public class Root
    {
        public int status { get; set; }
        public string msg { get; set; }
        public List<ReturnData> returnData { get; set; }
    }

    protected void BtnGetFileName_Click(object sender, EventArgs e)
    {
        //string strPath = Server.MapPath("~/Portal/ApprovalDocs/");
        //string[] filePaths = Directory.GetFiles(Server.MapPath(TxtFilePath.Text));        

        //GrdFileNames.DataSource = filePaths;
        //GrdFileNames.DataBind();


        string Path = Server.MapPath(TxtFilePath.Text);
        Path = "E:\\Sushant";
        DirectoryInfo Directory = new DirectoryInfo(Path);
        FileInfo[] FilesPaths = Directory.GetFiles().OrderByDescending(x => x.Length).ToArray();

        ArrayList arrList = new ArrayList();
        foreach (FileInfo file2 in FilesPaths)
        {
            if (file2.Extension.ToLower() == ".jpg" || file2.Extension == ".jpeg" || file2.Extension == ".gif" || file2.Extension == ".png" || file2.Extension == ".pdf" || file2.Extension == ".txt" || file2.Extension == ".xls")
            {
                arrList.Add(file2);
            }
        }

        GrdFileNames.DataSource = arrList;
        GrdFileNames.DataBind();

    }

    protected void BtnDownload_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.Parent.Parent;

        string strFileName = row.Cells[1].Text.ToString();
        string strFileFullPath = row.Cells[6].Text.ToString();

        Response.Redirect("DownloadFileFromRemoteServer.ashx?fileName=" + strFileName + "&fileFullPath=" + strFileFullPath);

    }
    protected void Btn_Paresharam_Click(object sender, EventArgs e)
    {
        var client = new RestClient("http://61.2.215.77:8085/pareshram/goswift/UpdatePaymentInfo");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Cookie", "JSESSIONID=B4BD28574FD5383EF7951353F54A418B");
        var body = @"{""UserId"":""6418"",""GoSwiftApplicationID"":""2022060239000260"",""BankTranscatioID"":""TST335CE7CDD2"",""TYPE"":""CC"",""AMOUNT"":""675.00"",""STATUS"":""1""}";
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        Response.Write(response.Content.ToString());
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        //if (tblScoreCard.Rows.Count > 0)
        //{
        //    var x = tblScoreCard.Rows[0].Cells[0].InnerHtml;
        //    var y = tblScoreCard.Rows[0].Cells[1].InnerHtml;
        //    var z = tblScoreCard.Rows[0].Cells[2].InnerHtml;
        //}

        //// Control FF = FindControl("gvPGSubject");

        //// HtmlControl htmlControl = (HtmlControl)form1.FindControl("gvPGSubject");

        ////HtmlTable tbl = (HtmlTable)form1.FindControl("gvPGSubject");
        ////var xxx = tbl.Rows[0].Cells[0].FindControl("gvPGSubject");

        ////HtmlControl dd=(HtmlControl)

        //if (gvPGSubject.Rows.Count > 0)
        //{

        //    var x = gvPGSubject.Rows[0].Cells[0].InnerHtml;
        //}
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        string keyvalue = "252e80b4e5d9cfc8b369ad98dcc87b5e";

        // string body = @"{""firstName"":""Anil"",""lastName"":""testu"",""mobile"":""8979787889"",""goSwiftAapplicationId"":""2023051525000002""}";
        //string body = "{\"serviceid\":\"16\",\"goSwiftApplicationId\":\"2023051525000002\",\"name\":\"Anil\",\"pan\":\"BSQPJ1951W\",\"email\":\"debiprasannajena401@gmail.com\",\"mobile\":\"8979787889\"}";

        var body = @"{""serviceId"":""16"",""name"":""Anil"",""pan"":""BSQPJ1951W"",""email"":""debiprasannajena401@gmail.com"",""mobile"":""8979787889"",""goSwiftApplicationId"":""2023051525000002""}";


        //EncryptValue = "{\"serviceid\":\"" + str_FormId.ToString() + "\",\"goSwiftApplicationId\":\"" + output + "\",\"name\":\"" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "\",\"pan\":\"" + dt.Rows[0]["VCH_PAN"].ToString() + "\",\"email\":\"" + dt.Rows[0]["VCH_EMAIL"].ToString() + "\",\"mobile\":\"" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "\"}";


        var datatoken = "{\"serviceId\":\"" + "16" + "\",\"name\":\"" + "Anil" + "\",\"pan\":\"" + "BSQPJ1951W" + "\",\"email\":\"" + "debiprasannajena401@gmail.com" + "\",\"mobile\":\"" + "8979787889" + "\",\"goSwiftApplicationId\":\"" + "2023051525000002" + "\"}";

        var withtoken = "{\r\n    \"data\":{\"serviceId\":\"" + "16" + "\",\"name\":\"" + "Anil" + "\",\"pan\":\"" + "BSQPJ1951W" + "\",\"email\":\"" + "debiprasannajena401@gmail.com" + "\",\"mobile\":\"" + "8979787889" + "\",\"goSwiftApplicationId\":\"" + "2023051525000002" + "\"},\"token\":\"" + "ee3fe57436c239e3d2d155f91d6986ac0f16095d332251ab59cfbab5568c5b82" + "\"\r\n}";


        //{"serviceId":"16","name":"Anil","pan":"BSQPJ1951W","email":"debiprasannajena401@gmail.com","mobile":"8979787889","goSwiftApplicationId":"2023051525000002"}



        //  string requestData = "";
        //string generatetoken = GeenerateToken(requestData, keyvalue);
        string generatetoken = GeenerateToken(datatoken, keyvalue);
        //Label5.Text = generatetoken;


        // Response.Redirect("https://mobidyut.com:8095/NewConnection/NewServiceConnectionGoSwift?data=" + body + "&token=" + generatetoken);




        var client = new RestClient("https://mobidyut.com:8095/NewConnection/NewServiceConnectionGoSwift");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Cookie", "ASP.NET_SessionId=4hat4ggly4rwdyyw4vvgexpu");
        var body1 = @"{
" + "\n" +
@"    ""data"":{""serviceId"":""16"",""name"":""Anil"",""pan"":""BSQPJ1951W"",""email"":""debiprasannajena401@gmail.com"",""mobile"":""8979787889"",""goSwiftApplicationId"":""2023051525000002""},""token"":""ee3fe57436c239e3d2d155f91d6986ac0f16095d332251ab59cfbab5568c5b82""
" + "\n" +
@"}";


        request.AddParameter("application/json", withtoken, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);

        Response.Write(response.Content);



    }
    public string GeenerateToken(string requestData, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(requestData);

        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

    protected void btn_cin_Click(object sender, EventArgs e)
    {


        var client = new RestClient("http://182.79.115.45:8280");
        client.Timeout = -1;

        var request = new RestRequest("/token", Method.POST);
        request.AddHeader("Authorization", "Basic ME4wUDBtQm1NdGVGcTNZX1c5cjdZRkxQZWswYTpwQmVWd3hzTjdJWnVfcEdKUzk1MFZoUmxjQVlh");
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("grant_type", "password");
        request.AddParameter("username", "admin");
        request.AddParameter("password", "admin");
        IRestResponse response = client.Execute(request);
        if (response.Content.ToString() != "")
        {
            string strAccessToke = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content)["access_token"].ToString();
            //Lbl_cin.InnerText = strAccessToke;


            var client1 = new RestClient("http://182.79.115.45:8280");
            client1.Timeout = -1;
            string cin = "U01100AP2018PTC107442";
            var request1 = new RestRequest("/cin/service/integration/1.0.0?CIN=" + txt_cin.Text, Method.GET);
            request1.AddHeader("Authorization", "Bearer " + strAccessToke);
            IRestResponse response1 = client.Execute(request1);

            //Console.WriteLine(response.Content);

            string message = JsonConvert.DeserializeObject<Dictionary<string, object>>(response1.Content)["message"].ToString();

            // base64 encode

            byte[] bytesToEncode = System.Text.Encoding.UTF8.GetBytes(response1.Content);

            // Perform Base64 encoding
            string encodedString = Convert.ToBase64String(bytesToEncode);

            // Lbl_cin.InnerText = encodedString;






            byte[] decodedBytes = Convert.FromBase64String(encodedString);

            // Convert the decoded byte array back to string
            string decodedString = Encoding.UTF8.GetString(decodedBytes);

            Lbl_cin.InnerText = decodedString;



            //Lbl_cin.InnerText = message;
        }








    }

    protected void Btn_Peal_Data_Push_Click(object sender, EventArgs e)
    {


        //        var client = new RestClient("http://192.168.10.168/mosarkar/App/API/mosarkarservice.php");
        //        var request = new RestRequest(Method.POST);
        //        request.AddHeader("Content-Type", "application/json");
        //        request.AddHeader("Authorization", "Basic bW9zYXJrYXJfcG9ydGFsOm1vc2Fya2FyIzI4QDIwMjA=");
        //        request.AddHeader("Cookie", "PHPSESSID=90pqnlhdnkcjsfksgn1qocurn2");

        //        // Create JSON body
        //        var jsonBody = @"
        //                  {
        //                       ""method"": ""OutboundDataSubmit"",
        //                       ""dept_code"": ""IND@16"",
        //                       ""service_code"": ""IND@16@SLS"",
        //         ""data"": [
        //        {
        //            ""district_id"": 2,
        //            ""name"": ""Star Packing"",
        //            ""mobile"": ""7978911963"",
        //            ""age"": 0,
        //            ""gender"": 0,
        //            ""department_institution_id"": 1170,
        //            ""registration_date"": ""2021-11-15"",
        //            ""registration_no"": ""2022082062"",
        //            ""other_info"": {
        //                ""payment_date"": ""15-05-2023"",
        //                ""installment"": ""1st installment""
        //            }
        //        }
        //    ]
        //}";

        //        // Add JSON body to request
        //        request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

        //        IRestResponse response = client.Execute(request);
        //        Response.Write(response.Content.ToString());
        //        Lbl_Msg.Text = response.Content;

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }
        try
        {
            //SqlCommand objCommand = new SqlCommand();
            //SqlDataAdapter objDa = new SqlDataAdapter();
            //DataTable objds = new DataTable();
            //objCommand.CommandText = "USP_FETCH_PROPOSAL_DETAILS";
            //objCommand.CommandType = CommandType.StoredProcedure;
            //objCommand.Connection = objConn;
            //objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "PDS");
            //objDa.SelectCommand = objCommand;
            //objDa.Fill(objds);


            //int RowCount = objds.Rows.Count;

            //for (int i = 0; i < objds.Rows.Count; i += RowCount)
            //{
            //    // Process the  DataTable
            //    foreach (DataRow row in objds.Rows)
            //    {
            //        string CompName = Convert.ToString(row["vchCompName"]);
            //        string MobileNo = Convert.ToString(row["vchCorMobileNo"]);

            //        string RegistrationDateStr = Convert.ToString(row["CreatedOn"]);
            //        string formattedDate = Convert.ToDateTime(RegistrationDateStr).ToString("yyyy-MM-dd");


            //        string RegistrationNo = Convert.ToString(row["vchProposalNo"]);
            //        string ConstitutionType = Convert.ToString(row["ConstitutionType"]);
            //        string Email = Convert.ToString(row["vchEmail"]);

            //        string uri = ConfigurationManager.AppSettings["MoSarkarurl"].ToString();


            //        var client = new RestClient(uri);
            //        var request = new RestRequest(Method.POST);
            //        request.AddHeader("Content-Type", "application/json");
            //        request.AddHeader("Authorization", "Basic bW9zYXJrYXJfcG9ydGFsOm1vc2Fya2FyIzI4QDIwMjA=");
            //        request.AddHeader("Cookie", "PHPSESSID=90pqnlhdnkcjsfksgn1qocurn2");
            //        var body = "{" + FormatJSON("method", "OutboundDataSubmit") + "," + FormatJSON("dept_code", "IND@16") + "," + FormatJSON("service_code", "IND@16@SLS") + "," + "\"data\": [" + "{" + FormatJSON("district_id", "20") + "," + FormatJSON("name", CompName) + "," + FormatJSON("mobile", MobileNo) + "," + FormatJSON("age", "0") + "," + FormatJSON("gender", "0") + "," + FormatJSON("department_institution_id", "1170") + "," + FormatJSON("registration_date", formattedDate) + "," + FormatJSON("registration_no", RegistrationNo) + "," + "\"other_info\": {" + FormatJSON("Constitution_Type", ConstitutionType) + "," + FormatJSON("email", Email) + "}" + "}" + "]" + "}";
            //        // Add JSON body to request
            //        request.AddParameter("application/json", body, ParameterType.RequestBody);

            //        IRestResponse response = client.Execute(request);
            //        Response.Write(response.Content.ToString());
            //        string Data= response.Content;


            // Lbl_Msg.Text = response.Content;

            //}
            //}

            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataTable objds = new DataTable();
            objCommand.CommandText = "USP_FETCH_PROPOSAL_DETAILS";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;
            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "PDS");
            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            // Create a list to store all data
            List<object> data = new List<object>();

            // Process all rows from the DataTable
            foreach (DataRow row in objds.Rows)
            {
                string CompName = Convert.ToString(row["vchCompName"]);
                string MobileNo = Convert.ToString(row["vchCorMobileNo"]);
                string RegistrationDate = Convert.ToDateTime(row["CreatedOn"]).ToString("yyyy-MM-dd");
                string RegistrationNo = Convert.ToString(row["vchProposalNo"]);
                string ConstitutionType = Convert.ToString(row["ConstitutionType"]);
                string Email = Convert.ToString(row["vchEmail"]);

                // Create  the current record
                var record = new
                {
                    district_id= "20",
                    name = CompName,
                    mobile = MobileNo,
                    age= "0",
                    gender = "0",
                    department_institution_id ="1170",
                    registration_date = RegistrationDate,
                    registration_no = RegistrationNo,
                    other_info = new
                    {
                        Constitution_Type = ConstitutionType,
                        email = Email
                    }
                };
                // Add the current record to the data
                data.Add(record);
            }
            // Construct the complete JSON body for the request
            var body = new
            {
                method = "OutboundDataSubmit",
                dept_code = "IND@16",
                service_code = "IND@16@SLS",
                data = data
            };
            string uri = ConfigurationManager.AppSettings["MoSarkarurl"].ToString();
            // Serialize the body object to JSON without formatting
            string jsonBody = JsonConvert.SerializeObject(body, Formatting.None);
            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic bW9zYXJrYXJfcG9ydGFsOm1vc2Fya2FyIzI4QDIwMjA=");
            request.AddHeader("Cookie", "PHPSESSID=90pqnlhdnkcjsfksgn1qocurn2");
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            Response.Write(response.Content.ToString());
           
            Lbl_Msg_Peal.Text = response.Content;
        }
        catch (Exception ex)
        {
           
            Util.LogError(ex, "PealDetailsDataSchedular");
        }
    }
  
}