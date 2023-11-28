using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Dashboard;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;


//using System.Security.Cryptography.Pkcs;
using System.Configuration;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Threading;
using RestSharp;

//using AIMServiceReference;

public partial class TestPageSPMG : System.Web.UI.Page
{
    SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());


    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = "";
       // DIVNO.Visible = false;
    }

    protected void Btn_AIM_Test_Click(object sender, EventArgs e) // first lavel 
    {
        try
        {
            Util.LogRequestResponse("AIMintegration", "RequestDateTime First Level:",DateTime.Now.ToString());
            AIMServiceReference.UserRegistrationClient user = new AIMServiceReference.UserRegistrationClient();
            AIMServiceReference.userregistration_propEntity objEnity = new AIMServiceReference.userregistration_propEntity();

            Util.LogRequestResponse("AIMintegration", "Data initialize ", "Start");
            objEnity.SITELOCATION = "Near Laxmisagar";          
            objEnity.CATEGORY = "2";
            objEnity.INV_NAME = "Raju Test";
            objEnity.Enterprenenur_Name = "Mr Raju Pvt.";
            objEnity.DISTRICT = "15";
            objEnity.BLOCK = "155";
            objEnity.ADDRESS = "Laxmi Sagar";
            objEnity.CELLPHONE_NO = "7327014294";
            objEnity.E_MAIL = "raju1994@gmail.com";
            objEnity.INV_USERID = "AOFPJ9734F_341012";
            objEnity.PAN = "AOFPJ9734F";
            objEnity.SECTOR = "29";
            objEnity.SUBSECTOR = "112";
            objEnity.GSTIN = "";
            objEnity.LICENCE_NO_TYPE = "";
            objEnity.EIN_PC = "";
            objEnity.UNIQUEID = "7407F984-1B73-4831-A2AB-3DD554935815";
            objEnity.Status = "F";

            var json = new JavaScriptSerializer().Serialize(objEnity);

            Util.LogRequestResponse("AIMintegration", "Data Sent :", json);


            int x = user.insertrecord(objEnity); //Outmsg-Returns 1 for success  & 2 for duplicate user & 0 for failure

            //62BB774E-148F-4250-8930-A14E897AED8B
            //7407F984-1B73-4831-A2AB-3DD554935815
            //95ED1B4C-96C8-4E36-97CF-C96AA0F6A7D8

            Lbk_Msg_AIM.Text = x.ToString();
        }
        catch (Exception ex)
        {
            Lbk_Msg_AIM.Text = ex.ToString();
            Util.LogError(ex, "AIMError");
        }
    }
    protected void Btn_AIM_Test_Update_Click(object sender, EventArgs e)
    {
        try
        {
            Util.LogRequestResponse("AIMintegration", "RequestDateTime Second Level:", DateTime.Now.ToString());
            AIMServiceReference.UserRegistrationClient user = new AIMServiceReference.UserRegistrationClient();
            AIMServiceReference.userregistration_propEntity objEnity = new AIMServiceReference.userregistration_propEntity();
            Util.LogRequestResponse("AIMintegration", "Data initialize ", "Start");

            objEnity.SITELOCATION = "Near Laxmisagar";
            objEnity.CATEGORY = "2";
            objEnity.INV_NAME = "BISKFARM";
            objEnity.BLOCK = "155";
            objEnity.INV_USERID = "AOFPJ9734F_341012";
            objEnity.PAN = "AOFPJ9734F";
            objEnity.SECTOR = "29";
            objEnity.SUBSECTOR = "112";
            objEnity.GSTIN = "658485";
            objEnity.LICENCE_NO_TYPE = "PC";
            objEnity.EIN_PC = "0171100024";
            objEnity.UNIQUEID = "7407F984-1B73-4831-A2AB-3DD554935815";
            objEnity.Status = "A";

            var json = new JavaScriptSerializer().Serialize(objEnity);

            Util.LogRequestResponse("AIMintegration", "Data Sent :", json);

            int y = user.UPDATERECORD(objEnity); //Outmsg-Returns 1 for success and 0 for failure

            Lbk_Msg_AIM_update.Text = y.ToString();
        }
        catch (Exception ex)
        {
            Lbk_Msg_AIM_update.Text = ex.Message.ToString();
            Util.LogError(ex, "AIMError");
            //throw ex;
        }
    }

    protected void Btn_Download_File_Click(object sender, EventArgs e)
    {
        ////Response.Redirect("TestDownloadFileHandler.ashx?id=1");

        //System.Net.WebClient client = new System.Net.WebClient();
        //string FolderPath = @"E:\docfile"; //System.Configuration.ConfigurationManager.AppSettings["AddPath"].ToString();
        //string Retval, RetStatus, Filename;

        //try
        //{
        //    string strURL = FolderPath;
        //    /// ======= Check and create folder in ILMS Document folder ============
        //    String guid = Guid.NewGuid().ToString();
        //    Filename = guid + ".pdf";
        //    strURL = strURL + "" + "\\";

        //    string strPath = "~/Document/RegdDoc1/";
        //    strURL = Path.Combine(Server.MapPath(strPath), Filename);

        //    if (!Directory.Exists(Server.MapPath(strPath)))
        //    {
        //        Directory.CreateDirectory(Server.MapPath(strPath));
        //    }


        //    // Save file to disk ========================
        //    //client.DownloadFile(new System.Uri("http://orissalms.in/genericdownloadFile.do?ai=" + filename + "&tp=CPC"), strURL);
        //    //client.DownloadFile(new System.Uri("http://localhost/swp_new/TestDownloadFileHandler.ashx?id=1"), strURL);



        //    byte[] data;
        //    using (WebClient cc = new WebClient())
        //    {
        //        //data = cc.DownloadData("http://localhost/swp_new/TestDownloadFileHandler.ashx?id=1");
        //        data = cc.DownloadData("http://164.100.141.243/DownloadPDF.ashx?PARAM=5941ACD3-6D9C-4832-8E80-5FB6AFDFDB19");
        //    }

        //    string fName = Path.GetFileName("http://localhost/swp_new/TestDownloadFileHandler.ashx?id=1");

        //    //if (data.Length > 0)
        //    //{
        //    //    if (IsFileValid(data))
        //    //    {
        //    //        File.WriteAllBytes(Server.MapPath(strPath + Filename), data);
        //    //    }
        //    //}

        //    // string ss = IsFileValid(data, "");

        //    //Stream stream = data.GetLength;
        //    //FileStream fs = stream as FileStream;
        //    //if (fs != null) Console.WriteLine(fs.Name);


        //    //FileStream fs = File.OpenRead("http://localhost/swp_new/TestDownloadFileHandler.ashx?id=1");

        //    //byte[] bytes = new byte[fs.Length];
        //    //fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
        //    //string dsfdsss = fs.Name;
        //    //fs.Close();







        //    File.WriteAllBytes(Server.MapPath(strPath + Filename), data);

        //    // Save file to database ========================
        //    //obj.Action = "UP";
        //    //obj.RegistrationID = Convert.ToInt32(CaseRegId);
        //    //obj.UploadDocument = filename + ".pdf";
        //    //obj.CreatedBy = 0;//convert.ToInt32(HttpContext.Current.Session["UserId"]);
        //    //Retval = objBusiness.ManageCaseRegistration(obj);


        //}
        //catch (Exception ex)
        //{
        //    RetStatus = "404";
        //}

    }

    //private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };


    private static readonly byte[] BMP = { 66, 77 };
    private static readonly byte[] DOC = { 207, 17, 224, 161, 177, 26, 225 };///--- modified from 8no of element to 7
    private static readonly byte[] XLS = { 208 };//-- added on 20th Nov 2017 by GS Chhotray
    private static readonly byte[] EXE_DLL = { 77, 90 };
    private static readonly byte[] GIF = { 71, 73, 70, 56 };
    private static readonly byte[] ICO = { 0, 0, 1, 0 };
    private static readonly byte[] JPG = { 255, 216, 255 };
    private static readonly byte[] MP3 = { 255, 251, 48 };
    private static readonly byte[] OGG = { 79, 103, 103, 83, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0 };
    private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
    private static readonly byte[] PNG = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
    private static readonly byte[] RAR = { 82, 97, 114, 33, 26, 7, 0 };
    private static readonly byte[] SWF = { 70, 87, 83 };
    private static readonly byte[] TIFF = { 73, 73, 42, 0 };
    private static readonly byte[] TORRENT = { 100, 56, 58, 97, 110, 110, 111, 117, 110, 99, 101 };
    private static readonly byte[] TTF = { 0, 1, 0, 0, 0 };
    private static readonly byte[] WAV_AVI = { 82, 73, 70, 70 };
    private static readonly byte[] WMV_WMA = { 48, 38, 178, 117, 142, 102, 207, 17, 166, 217, 0, 170, 0, 98, 206, 108 };
    private static readonly byte[] ZIP_DOCX = { 80, 75, 3, 4 };
    private static readonly byte[] EXE_SIGNATURE = { 77, 90 };


    private string IsFileValid(byte[] file, string fileName)
    {
        //if (filebyte.Take(7).SequenceEqual(PDF))
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}


        string mime = "application/octet-stream"; //DEFAULT UNKNOWN MIME TYPE

        //  string fileName = file.GetType("").ToString();

        //Ensure that the filename isn't empty or null
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return mime;
        }

        //Get the file extension
        string extension = Path.GetExtension(fileName) == null
                               ? string.Empty
                               : Path.GetExtension(fileName).ToUpper();

        //Get the MIME Type
        if (file.Take(2).SequenceEqual(BMP))
        {
            mime = "image/bmp";
        }
        else if (file.Take(7).SequenceEqual(DOC))
        {
            mime = "application/msword";
        }
        else if (file.Take(1).SequenceEqual(XLS))
        {
            mime = "application/msxls";//-- added on 20th Nov 2017 by GS Chhotray
        }
        else if (file.Take(2).SequenceEqual(EXE_DLL))
        {
            mime = "application/x-msdownload"; //both use same mime type
        }
        else if (file.Take(4).SequenceEqual(GIF))
        {
            mime = "image/gif";
        }
        else if (file.Take(4).SequenceEqual(ICO))
        {
            mime = "image/x-icon";
        }
        else if (file.Take(3).SequenceEqual(JPG))
        {
            mime = "image/jpeg";
        }
        else if (file.Take(3).SequenceEqual(MP3))
        {
            mime = "audio/mpeg";
        }
        else if (file.Take(14).SequenceEqual(OGG))
        {
            if (extension == ".OGX")
            {
                mime = "application/ogg";
            }
            else if (extension == ".OGA")
            {
                mime = "audio/ogg";
            }
            else
            {
                mime = "video/ogg";
            }
        }
        else if (file.Take(7).SequenceEqual(PDF))
        {
            mime = "application/pdf";
        }
        else if (file.Take(16).SequenceEqual(PNG))
        {
            mime = "image/png";
        }
        else if (file.Take(7).SequenceEqual(RAR))
        {
            mime = "application/x-rar-compressed";
        }
        else if (file.Take(3).SequenceEqual(SWF))
        {
            mime = "application/x-shockwave-flash";
        }
        else if (file.Take(4).SequenceEqual(TIFF))
        {
            mime = "image/tiff";
        }
        else if (file.Take(11).SequenceEqual(TORRENT))
        {
            mime = "application/x-bittorrent";
        }
        else if (file.Take(5).SequenceEqual(TTF))
        {
            mime = "application/x-font-ttf";
        }
        else if (file.Take(4).SequenceEqual(WAV_AVI))
        {
            mime = extension == ".AVI" ? "video/x-msvideo" : "audio/x-wav";
        }
        else if (file.Take(16).SequenceEqual(WMV_WMA))
        {
            mime = extension == ".WMA" ? "audio/x-ms-wma" : "video/x-ms-wmv";
        }
        else if (file.Take(4).SequenceEqual(ZIP_DOCX))
        {
            mime = extension == ".DOCX" ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" : "application/x-zip-compressed";
        }
        else if (file.SequenceEqual(EXE_SIGNATURE))
        {
            mime = "application/x-bittorrent";
        }
        return mime;




        //string[] allowedImageTyps = { "application/pdf", "application/x-zip-compressed" };
        //string[] allowedExtension = { ".pdf", ".zip" };
        //StringCollection imageTypes = new StringCollection();
        //StringCollection imageExtension = new StringCollection();
        //imageTypes.AddRange(allowedImageTyps);
        //imageExtension.AddRange(allowedExtension);
        //string strFiletype =   MimeType.GetMimeType(filebyte, filebyte.GetType);
        //string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);// 
        //int count = FileUpload1.FileName.Count(f => f == '.');

        //string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
        //CommonFunctions cmmf = new CommonFunctions();

        //if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        //AIMDocumentScheduler ss = new AIMDocumentScheduler();
        //ss.GetEinPcDocumentFromAIM();
    }

    protected void Btn_CMS_Click(object sender, EventArgs e)
    {
        string[] strScheduleHour = { "16", "24" };
        string[] strScheduleMinute = { "00", "15", "30", "45" };

        string strCurrentHour = DateTime.Now.Hour.ToString();
        string strCurrentMinute = DateTime.Now.Minute.ToString();

        if (strScheduleHour.Contains(strCurrentHour) && strScheduleMinute.Contains(strCurrentMinute)) //// 4 PM and 10 PM
        {
            //CmsOrtpsaPosting ss = new CmsOrtpsaPosting();
            //ss.SendServiceInfoToCMS();

            Response.Write("hiiiiiiiiiiiiii");
        }
    }

    protected void Btn_CMS_1st_Time_Push_Click(object sender, EventArgs e)
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }

        try
        {
            objCommand.CommandText = "USP_CMS_ORTPSA_DETAILS";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "A");

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Tables[0].Rows.Count > 0)
            {
                string strModuleName = "CmsOrtpsaScheduler";
                string strMethodName = "SendServiceInfoToCMS";
                string strReqData = "";
                string strResData = "";

                for (int i = 0; i < objds.Tables[0].Rows.Count; i++)
                {
                    string strDistCode = Convert.ToString(objds.Tables[0].Rows[i]["vchCMSDistrictId"]);
                    string strOfficeCode = Convert.ToString(objds.Tables[0].Rows[i]["vchCMSOfficeCode"]);
                    string strOfficeAddress = Convert.ToString(objds.Tables[0].Rows[i]["vchDomainUName"]);
                    string strOfficerName = Convert.ToString(objds.Tables[0].Rows[i]["vchFullName"]);
                    string strApplicantName = Convert.ToString(objds.Tables[0].Rows[i]["vchCompName"]);
                    string strApplicantAddress = Convert.ToString(objds.Tables[0].Rows[i]["vchAddress"]);
                    string strMobileNo = Convert.ToString(objds.Tables[0].Rows[i]["vchPhoneNo"]);
                    string strApplicationDate = Convert.ToString(objds.Tables[0].Rows[i]["dtmPealApplyDate"]);
                    string strApplicationLastDate = Convert.ToString(objds.Tables[0].Rows[i]["dtmUpdatedOn"]);
                    string strApplicantStatus = Convert.ToString(objds.Tables[0].Rows[i]["intApprovalStatus"]);
                    string strAckNo = Convert.ToString(objds.Tables[0].Rows[i]["vchProposalNo"]);
                    string strIdcoStatus = Convert.ToString(objds.Tables[0].Rows[i]["PIDCOStatus"]);
                    string strIdcoStatusCode = Convert.ToString(objds.Tables[0].Rows[i]["vchstatuscode"]);
                    int intApprovalStatus = Convert.ToInt32(objds.Tables[0].Rows[i]["intApprovalStatus"]);
                    int intIDCObtnClickStatus = Convert.ToInt32(objds.Tables[0].Rows[i]["intIDCObtnClickStatus"]);

                    /*-----------------------------------------------------------------*/

                    string strServiceCode = "87";
                    int intSentStatus = 1;

                    /*-----------------------------------------------------------------*/

                    if (i > 0)
                    {
                        Thread.Sleep(2000); //// Wait for 2 sec
                    }

                    /*-----------------------------------------------------------------*/
                    ////Check whether the sent status is present for respective proposal or not.
                    ////If not present then sent request else goto next loop.
                    /*-----------------------------------------------------------------*/
                    DataRow[] dRow = objds.Tables[1].Select("vchProposalNo = '" + strAckNo + "' and intStatus='" + intSentStatus + "'");
                    if (dRow.Length == 0)
                    {
                        string strServiceUrl = "http://central.ortpsa.in/API/api_ind.php?"
                                             + "circuit=public"
                                             + "&fuseaction=app_entry"
                                             + "&apikey=9c2768c10b87736f5b307b36ed5238c28c15b38c"
                                             + "&deptcode=16"
                                             + "&dcode=" + strDistCode
                                             + "&scode=" + strServiceCode
                                             + "&ocode=" + strOfficeCode
                                             + "&oaddress=" + strOfficeAddress
                                             + "&doname=" + strOfficerName
                                             + "&appname=" + strApplicantName
                                             + "&appaddress=" + strApplicantAddress
                                             + "&phone=" + strMobileNo
                                             + "&entry_date=" + strApplicationDate
                                             + "&last_date=" + strApplicationLastDate
                                             + "&astatus=" + strApplicantStatus
                                             + "&ack_no=" + strAckNo;


                        strReqData = "(Request):-" + strServiceUrl;

                        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(strServiceUrl));
                        httpRequest.Accept = "application/json";
                        httpRequest.ContentType = "application/json";
                        httpRequest.Method = "GET";
                        using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                        {
                            using (Stream stream = httpResponse.GetResponseStream())
                            {
                                string strResult = (new StreamReader(stream)).ReadToEnd();
                                strResData = "(Response):-" + strResult;

                                if (strResult == "1")
                                {
                                    objCommand = new SqlCommand();
                                    objCommand.CommandText = "USP_CMS_ORTPSA_DETAILS";
                                    objCommand.CommandType = CommandType.StoredProcedure;
                                    objCommand.Connection = objConn;

                                    objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "B");
                                    objCommand.Parameters.AddWithValue("@P_VCH_PROPOSAL_NO", strAckNo);
                                    objCommand.Parameters.AddWithValue("@P_INT_STATUS", intSentStatus);

                                    objCommand.ExecuteNonQuery();
                                }
                            }
                        }

                        ///// Write Request Response Log
                        Util.LogRequestResponse(strModuleName, strMethodName, strReqData);
                        Util.LogRequestResponse(strModuleName, strMethodName, strResData);
                    }
                    else
                    {
                        ///// Write Request Response Log
                        Util.LogRequestResponse(strModuleName, strMethodName, "Status Info for Proposal No: " + strAckNo + " and Sent Status: " + intSentStatus + " has already sent.");
                    }
                }
            }
            else
            {
                Util.LogRequestResponse("CmsOrtpsaScheduler", "SendServiceInfoToCMS", "No record found" + objds.Tables[0].Rows.Count.ToString());
            }

            Label1.Text = "Completed..";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "CmsOrtpsaScheduler");
        }
        finally
        {
            objConn.Close();
            objCommand = null;
            objds = null;
        }
    }

    protected void Btn_AIM_MIS_Report_Click(object sender, EventArgs e)
    {
        try
        {
            Util.LogRequestResponse("AIMintegration", "RequestDateTime MIS:", DateTime.Now.ToString());

            AIMServiceReference.UserRegistrationClient user = new AIMServiceReference.UserRegistrationClient();
            Util.LogRequestResponse("AIMintegration", "Data initialize ", "Start");

            DataTable dt1 = new DataTable();
        dt1 = user.COUNTINCENTIVE("17", "2018-19").MISTABLE;
            GrdCountIncentive.DataSource = dt1;
            GrdCountIncentive.DataBind();

        DataTable dt2 = new DataTable();
        dt2 = user.STATUSWISEINCENTIVEDETAILS(2, "17", "2018-19").MISTABLE;
            GrdStatusWiseIncentive_2.DataSource = dt2;
            GrdStatusWiseIncentive_2.DataBind();

        DataTable dt3 = new DataTable();
        dt3 = user.DICWISECOUNTINCENTIVE("189", "2017-18").MISTABLE;
            GrdDICWisecountIncentive.DataSource = dt3;
            GrdDICWisecountIncentive.DataBind();

        DataTable dt4 = new DataTable();
        dt4 = user.STATUSWISEINCENTIVEDETAILS(1, "198", "2017").MISTABLE;
            GrdStatusWiseIncentive_1.DataSource = dt4;
            GrdStatusWiseIncentive_1.DataBind();
        }
        catch(Exception ex)
        {
           // Util.LogRequestResponse("AIMDocScheduler", "GetEinPcDocumentFromAIM", "Exception" + ex.Message.ToString());
            Util.LogError(ex, "AIMError");
        }

    }



    protected void Btn_AIM_Test_CheckEIN_Click(object sender, EventArgs e)  // check EIN /PC by AIM database  after login by user with UNIQUE ID  then if not exist autometicaly redirect to AIM page .
    {
        try
        {

        Util.LogRequestResponse("AIMintegration", "RequestDateTime CheckEIN:", DateTime.Now.ToString());
        AIMServiceReference.UserRegistrationClient user = new AIMServiceReference.UserRegistrationClient();
        AIMServiceReference.userregistration_propEntity objEnity = new AIMServiceReference.userregistration_propEntity();
            Util.LogRequestResponse("AIMintegration", "Data initialize ", "Start");

            objEnity.UNIQUEID = "7407F984-1B73-4831-A2AB-3DD554935815";

            var json = new JavaScriptSerializer().Serialize(objEnity);

            Util.LogRequestResponse("AIMintegration", "Data Sent :", json);

            int outmsg = user.CheckEINPC(objEnity);
        if(outmsg==1)
        {
            Lbk_Msg_CheckEIN.Text = "EIN Number Exist on Unique Id";
        }
        else if(outmsg == 2)
        {
            Lbk_Msg_CheckEIN.Text = "EIN Number not Exist Redirect to AIM Portal";
        }

        }
        catch(Exception ex)
        {
            Lbk_Msg_CheckEIN.Text = ex.Message.ToString();
            Util.LogError(ex, "AIMError");
        }
    }

    // FOR json string format
    string FormatJSON(string name, string value)
    {
        //return "\"\"" + name + "\"\": " + "\"\"" + value + "\"\"";

        return "\"" + name + "\":" + "\"" + value + "\"";
    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        string decChallanAmt = "168.75";

        string ApplicationNo = TxtApplicationno.Text;
        string UserId = "6419";
        string BnkTransid = "TST337C607A79";
        string uri = ConfigurationManager.AppSettings["ePareshramPaymentStatusUpdate"].ToString();

        try { 

        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        // var client = new RestClient("http://61.2.215.77:8085/pareshram/goswift/UpdatePaymentInfo");
        var client = new RestClient("http://61.2.45.238:8081/pareshram/goswift/UpdatePaymentInfo"); //staging url 
        client.Timeout =Convert.ToInt32(TxtTimeout.Text);
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
      //  request.AddHeader("Cookie", "JSESSIONID=8AEF5EA82C3A974C204C66F758AAB184");



        var body = @"{" + FormatJSON("UserId", UserId) + "," + FormatJSON("GoSwiftApplicationID", ApplicationNo) + "," + FormatJSON("BankTranscatioID", BnkTransid) + "," + FormatJSON("TYPE", "CC") + "," + FormatJSON("AMOUNT", decChallanAmt) + "," + FormatJSON("STATUS", "1") + "}";

       // Util.LogRequestResponse("PaymentScheduler", "ExternalServicePaymentSchedule", "[URL GENERATE][from Eshram]:- " + uri + "" + body.ToString());

        request.AddParameter("application/json", body, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        ///*----------------------------------------------------------------------------*/
        /////Write the  URL API Response in the Log File.
        ///*----------------------------------------------------------------------------*/
       // Util.LogRequestResponse("PaymentScheduler", "ExternalServicePaymentSchedule", "[RESPONSE_JSON_STRING_PAYMENT_UPDATE]:- " + response.Content.ToString());
            lblid.Text = response.Content.ToString();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}