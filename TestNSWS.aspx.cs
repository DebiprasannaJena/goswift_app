using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using RestSharp;
using Newtonsoft.Json;

using System.Net;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

public partial class TestNSWS : System.Web.UI.Page
{
    CommonDashboardFunction objCommDash = new CommonDashboardFunction();
    SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());


    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime time = Convert.ToDateTime("2021-04-09 19:02:41.700");
        long unixTime = UnixTimeHelper.ToUnixTime(time);
        //var totalSeconds = (long)(time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        //Response.Write(unixTime);

        //Response.Write("<br/>");
        //Response.Write("------------------------------");
        //Response.Write("<br/>");

        //DateTime sTime = new DateTime(1970, 1, 1).Add(TimeSpan.FromSeconds(unixTime));
        //Response.Write(sTime);


    }

    //public long ToUnixTime(DateTime time)
    //{
    //    var totalSeconds = (long)(time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    //    return totalSeconds;
    //}


    protected void BtnToken_Click(object sender, EventArgs e)
    {
        try
        {
            Lbl_Token.Text = "";
            Lbl_Token_Addrees.Text = "";

            string strTokenUrl = ConfigurationManager.AppSettings["NswsTokenGenerationUrl"].ToString();

            Lbl_Token_Addrees.Text = "TOKEN_ADDRESS:-  " + strTokenUrl;

            /*-----------------------------------------------------------------------------------------------------*/

            ///// Generate Access Token
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var client1 = new RestClient(strTokenUrl);
            client1.Timeout = -1;
            var request1 = new RestRequest(Method.POST);
            //string strAuthKey = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("sws_state:643790eb-2b2a-4187-8c43-54a663b840eb"));
            //request1.AddHeader("Authorization", strAuthKey);
            request1.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request1.AddParameter("grant_type", "password");
            request1.AddParameter("username", "odisha");
            request1.AddParameter("password", "Odisha@nsws");
            request1.AddParameter("client_secret", "643790eb-2b2a-4187-8c43-54a663b840eb");
            request1.AddParameter("client_id", "sws_state");
            IRestResponse responseToken = client1.Execute(request1);

            Lbl_Token_Response.Text = "TOKEN_RESPONSE:-  " + responseToken.Content.ToString();

            if (responseToken.Content.ToString() != "")
            {
                ////Get the Access Token
                string strAccessToke = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseToken.Content)["access_token"].ToString();

                Lbl_Token.Text = "TOKEN:- " + strAccessToke;
            }
        }
        catch (Exception ex)
        {
            Lbl_Token.Text = "EXCEPTION:- " + ex.Message.ToString();
        }
    }

    protected void BtnTokenGeneration_Click(object sender, EventArgs e)
    {
        try
        {
            string strInvestorSwsId = Txt_SWS_Id.Text.Trim();

            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072 | (SecurityProtocolType)768 | SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;


            /*-----------------------------------------------------------------------------------------------------*/
            ////Token Generation
            /*-----------------------------------------------------------------------------------------------------*/
            string strTokenUrl = ConfigurationManager.AppSettings["NswsTokenGenerationUrl"].ToString();
            var client = new RestClient(strTokenUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            //string strAuthKey = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("sws_state:643790eb-2b2a-4187-8c43-54a663b840eb"));
            //request.AddHeader("Authorization", strAuthKey);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", "odisha");
            request.AddParameter("password", "Odisha@nsws");
            request.AddParameter("client_secret", "643790eb-2b2a-4187-8c43-54a663b840eb");
            request.AddParameter("client_id", "sws_state");
            IRestResponse responseToken = client.Execute(request);

            //Response.Write("TOKEN:- " + responseToken.Content.ToString());

            if (responseToken.Content.ToString() != "")
            {

                ///Get the Access Token
                string strAccessToke = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseToken.Content)["access_token"].ToString();

                /*-----------------------------------------------------------------------------------------------------*/
                ///Consume CAF API (12 is the State Id for Odisha for NSWS)
                /*-----------------------------------------------------------------------------------------------------*/
                string strPullCafUrl = ConfigurationManager.AppSettings["NswsPullCafUrl"].ToString();
                strPullCafUrl = strPullCafUrl + "/" + strInvestorSwsId + "/12";                

                /*-----------------------------------------------------------------------------------------------------*/
                ///Pull the NSWS State CAF and Insert into GOSWIFT DB.
                /*-----------------------------------------------------------------------------------------------------*/
                var client1 = new RestClient(strPullCafUrl);
                client1.Timeout = -1;
                var request1 = new RestRequest(Method.GET);
                request1.AddHeader("Authorization", "Bearer " + strAccessToke);
                request1.AlwaysMultipartFormData = true;
                IRestResponse responseCAF = client1.Execute(request1);

                Response.Write("STATUS CODE:- " + responseCAF.StatusCode);
                Response.Write("<br/>");

                ///Deserialize the CAF Response 
                NSWSAPIReponseCls.NSWSCAFResponse objApp = new NSWSAPIReponseCls.NSWSCAFResponse();
                objApp = JsonConvert.DeserializeObject<NSWSAPIReponseCls.NSWSCAFResponse>(responseCAF.Content);

                ///Find the "Root" value from JSON.
                bool status = objApp.status;
                string msg = objApp.message.ToString();   

                string xx = responseCAF.Content.ToString();

                Response.Write(xx);


                //if (status == true)
                //{
                //    Response.Write("*************************************************************************************************************");
                //    Response.Write("<br/>");
                //    Response.Write("STATUS:- " + status.ToString());
                //    Response.Write("<br/>");
                //    Response.Write("MESSAGE:- " + msg);
                //    Response.Write("<br/>");
                //    Response.Write("*************************************************************************************************************");
                //    Response.Write("<br/>");

                //    NSWSAPIReponseCls.Data objData = new NSWSAPIReponseCls.Data();                   
                //    objData = objApp.data;

                //    string investorSWSId = objData.investorSWSId;
                //    long dateOfInitiation = objData.dateOfInitiation;

                //    ///Find the "Section" value from JSON.
                //    List<NSWSAPIReponseCls.Section> objSec = new List<NSWSAPIReponseCls.Section>();
                //    //objSec = objData.sections.ToList();
                //    objSec = objData.sections.OrderBy(x => int.Parse(x.serialNumber)).ToList();                   

                //    for (int i = 0; i < objSec.Count; i++)
                //    {
                //        string sectionName = objSec[i].name;
                //        string sectionKey = objSec[i].sectionKey;
                //        string secSerialNumber = objSec[i].serialNumber;                       

                //        List<NSWSAPIReponseCls.Field> objFields = new List<NSWSAPIReponseCls.Field>();
                //        //objFields = objSec[i].fields.ToList();
                //        objFields = objSec[i].fields.OrderBy(x => int.Parse(x.serialNumber)).ToList();

                //        Response.Write("=======================================================================================================================");
                //        Response.Write("<br/>");
                //        Response.Write("Section Name:- " + sectionName);
                //        Response.Write("<br/>");
                //        Response.Write("Section Key:- " + sectionKey);
                //        Response.Write("<br/>");
                //        Response.Write("Section SL No:- " + secSerialNumber);
                //        Response.Write("<br/>");
                //        Response.Write("=======================================================================================================================");
                //        Response.Write("<br/>");

                //        for (int j = 0; j < objFields.Count; j++)
                //        {
                //            string fieldName = objFields[j].name.ToString();
                //            string inputVal = Convert.ToString(objFields[j].inputValue);
                //            string fieldKey = Convert.ToString(objFields[j].fieldKey);
                //            string serialNumber = Convert.ToString(objFields[j].serialNumber);

                //            Response.Write("fieldName:- " + fieldName);
                //            Response.Write("<br/>");
                //            Response.Write("inputValue:- " + inputVal);
                //            Response.Write("<br/>");
                //            Response.Write("fieldKey:- " + fieldKey);
                //            Response.Write("<br/>");
                //            Response.Write("serialNumber:- " + serialNumber);
                //            Response.Write("<br/>");

                //            Response.Write("----------------------------------------------------");
                //            Response.Write("<br/>");

                //            if (objFields[j].subFields != null)
                //            {
                //                List<NSWSAPIReponseCls.SubField> objSubFields = new List<NSWSAPIReponseCls.SubField>();
                //                objSubFields = objFields[j].subFields.ToList();                                

                //                for (int k = 0; k < objSubFields.Count; k++)
                //                {
                //                    string subFieldName = objSubFields[k].name.ToString();
                //                    string subInputVal = Convert.ToString(objSubFields[k].inputValue);

                //                    Response.Write("sub-FieldName:- " + subFieldName);
                //                    Response.Write("<br/>");
                //                    Response.Write("sub-InputValue:- " + subInputVal);
                //                    Response.Write("<br/>");
                //                    Response.Write("----------------------XXXXXXXXXXXXXXX--------------------------");
                //                    Response.Write("<br/>");
                //                }
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    Response.Write("*************************************************************************************************************");
                //    Response.Write("<br/>");
                //    Response.Write("STATUS:- " + status.ToString());
                //    Response.Write("<br/>");
                //    Response.Write("MESSAGE:- " + msg);
                //    Response.Write("<br/>");
                //    Response.Write("*************************************************************************************************************");
                //    Response.Write("<br/>");
                //}
            }
        }
        catch (Exception ex)
        {
            Lbl_Token.Text = ex.Message.ToString();
        }
    }

    protected void BtnRedirectionUrl_Click(object sender, EventArgs e)
    {
        //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        //////Token Generation
        //var client = new RestClient("https://sso-uat-nsws.investindia.gov.in/auth/realms/madhyam/protocol/openid-connect/token");
        //client.Timeout = -1;
        //var request = new RestRequest(Method.POST);
        //string strAuthKey = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("sws_state:643790eb-2b2a-4187-8c43-54a663b840eb"));
        //request.AddHeader("Authorization", strAuthKey);
        //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        //request.AddParameter("grant_type", "password");
        //request.AddParameter("username", "odisha");
        //request.AddParameter("password", "Odisha@nsws");
        //IRestResponse response = client.Execute(request);
        //// Response.Write(response.Content);
        //if (response.Content.ToString() != "")
        //{
        //    string strAccessToke = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content)["access_token"].ToString();

        //    /*-----------------------------------------------------------------------------------------------------*/
        //    ///// Consume Redirection API
        //    /*-----------------------------------------------------------------------------------------------------*/
        //    var client1 = new RestClient("https://uat-nsws.investindia.gov.in/gateway/form-builder/caf/redirection");
        //    client1.Timeout = -1;
        //    var request1 = new RestRequest(Method.POST);
        //    request1.AddHeader("Authorization", "Bearer " + strAccessToke);
        //    request1.AddHeader("Content-Type", "application/json");
        //    request1.AddParameter("application/json", "{​​​​\r\n  \"departmentId\":null,\r\n  \"licenseId\":null,\r\n  \"redirectionUrl\":\"http://117.247.252.241/swp/ProposalInstruction.aspx\",\r\n  \"stateId\":\"12\",\r\n   \"swsId\":\"949\"\r\n}", ParameterType.RequestBody);
        //    IRestResponse response1 = client1.Execute(request1);

        //    Response.Write("STATUS CODE:- " + response1.StatusCode);
        //    Response.Write("<br/>");
        //}
    }

    protected void BtnPullDoc_Click(object sender, EventArgs e)
    {
        //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        //var client = new RestClient("https://uat-nsws-mnstrportal.investindia.gov.in/nsws_document/getDocument");
        //client.Timeout = -1;
        //var request = new RestRequest(Method.POST);
        //request.AddHeader("access-id", "MIN_TEST_0");
        //request.AddHeader("access-secret", "MintesT@1234");
        //request.AddHeader("api-key", "Min1@GD03");
        //request.AddHeader("Content-Type", "application/json");
        //request.AddHeader("Cookie", "JSESSIONID=E65E20B80644D1F520ACAB99AA01FA44");
        ////request.AddParameter("application/json", "{\r\n\"contentId\":[\"SWSI202111-DOC202133-1\"]\r\n}", ParameterType.RequestBody);
        //request.AddParameter("application/json", "{\r\n\"contentId\":[\"1117-1617089359-1\"]\r\n}", ParameterType.RequestBody);
        //IRestResponse response = client.Execute(request);
        ////Response.Write(response.Content);

        //TextBox1.Text = response.Content;

        //if (response.Content.ToString() != "")
        //{
        //    PullApiDoc objApp = new PullApiDoc();
        //    objApp = JsonConvert.DeserializeObject<PullApiDoc>(response.Content);
        //    //List<PullApiDoc> objApp = new List<PullApiDoc>();
        //    //objApp = JsonConvert.DeserializeObject<List<PullApiDoc>>(response.Content);

        //    // List<PullApiDoc> objApp = JsonConvert.DeserializeObject<List<PullApiDoc>>(response.Content);

        //    string status = objApp.status;
        //    string message = objApp.message;

        //    List<DocResponseFile> objRes = new List<DocResponseFile>();
        //    objRes = objApp.response.ToList();

        //    string strFileName = objRes[0].fileName;
        //    string strFileResponse = objRes[0].fileResponse;

        //    byte[] data = Convert.FromBase64String(strFileResponse);
        //    string decodedString = Encoding.UTF8.GetString(data);

        //    strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_LICNDOC.pdf";
        //    string strPath = "D://CSMPROJECTS//SWPGoo//SWP_NEW//Document//RegdDoc//";

        //    if (data.Length > 0)
        //    {
        //        if (IsFileValid(data))
        //        {
        //            /////Save the file to destination folder
        //            FileStream fileStream = null;
        //            if (!string.IsNullOrEmpty(strPath))
        //            {
        //                fileStream = new FileStream(strPath + strFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //                using (System.IO.FileStream fs = fileStream)
        //                {
        //                    fs.Write(data, 0, data.Length);
        //                }
        //            }
        //        }
        //    }
        //}
    }

    protected void BtnTest2_Click(object sender, EventArgs e)
    {
        ////ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;          
        //var client = new RestClient("http://uat-nsws.investindia.gov.in/gateway/form-builder/caf/public/611/12");
        //client.Timeout = -1;
        //var request = new RestRequest(Method.GET);
        //request.AddHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJPLW9pNGV6WEJTYjAwZEtPWjdOVVVING93akZNTHhUc2VfS2poTzJocUdrIn0.eyJleHAiOjE2MTY2NzYxOTIsImlhdCI6MTYxNjY3NTg5MiwianRpIjoiMzVjYzNlZWUtZDc3OC00NWY3LWIxN2ItYWIxMzE0ZGM5ZWI1IiwiaXNzIjoiaHR0cHM6Ly9zc28tdWF0LW5zd3MuaW52ZXN0aW5kaWEuZ292LmluL2F1dGgvcmVhbG1zL21hZGh5YW0iLCJhdWQiOiJhY2NvdW50Iiwic3ViIjoiYTdmZWY4OTYtMDk3MS00MmI2LTgzOGQtNDBmYzg5MTsafsafsdfdfasfdffJkZjM2IiwidHlwIjoiQmVhcmVyIiwiYXpwIjoic3dzX3N0YXRlIiwic2Vzc2lvbl9zdGF0ZSI6ImMxZWNiYTA0LTA5YmItNGE3Ni1hZTYwLWFmOTA2OGI3ZDBhZSIsImFjciI6IjEiLCJhbGxvd2VkLW9yaWdpbnMiOlsiaHR0cDovL2xvY2FsaG9zdCJdLCJyZWFsbV9hY2Nlc3MiOnsicm9sZXMiOlsib2ZmbGluZV9hY2Nlc3MiLCJ1bWFfYXV0aG9yaXphdGlvbiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoiZW1haWwgcHJvZmlsZSIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwibmFtZSI6Ik9kaXNoYSIsInByZWZlcnJlZF91c2VybmFtZSI6Im9kaXNoYSIsImdpdmVuX25hbWUiOiJPZGlzaGEifQ.X2p2hf3gXVeJDedOqkrz8x75Jr_Q-5GJFskEOPYxBVb13JxF9eSPPWhglsfaF_PBMuYofZAfp78gJqu27ZNi-2KVqfWC-gdq7svHrepeHEARrp5NejJUtYQ-9IesrsmcaUWABb6XznM4mACaWahpVKRCo8VNd3xQWaqjjrzH-5doJXEUmi65bcKb7CCnzQx5fzxdpQLuc6g5Fg9Cw7IPgmB3K2VODiNX5zZX10Sk9gwanh_lMhoJtxVQYY5k2Ts9Q_cbuTGVDdtvuOsKJIyxRMWyATNx-QEBvW-sdfdsfdsfdfdsfdsfdfdsfdfdsfdsfdsfdsfdsfdsfdsfdsfdsfdsdsfdsfymr6wnamgt0qxaNkg6FXMg-PeN1lYiGtF1YBZNDK4e54_Pm4BtQ");
        //request.AddParameter("text/plain", "", ParameterType.RequestBody);
        //IRestResponse response = client.Execute(request);
        //Response.Write(response.Content);
    }

    protected void BtnRunScheduler_Click(object sender, EventArgs e)
    {
        //NSWSScheduler objNSWS = new NSWSScheduler();
        //objNSWS.UpdateRedirectionURL();
    }


    protected void BtnPushLicense_Click(object sender, EventArgs e)
    {
        //NSWSScheduler objsch = new NSWSScheduler();
        //objsch.PushLicenseStatus();
        //objsch.PushQueryStatus();
    }

    protected void Btn_Validate_CAF_JSON_Click(object sender, EventArgs e)
    {
        string strRetrunVal = "";
        SqlCommand objCommand = new SqlCommand();

        string investorSWSId = "";
        string fieldName = "";

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }

        try
        {
            #region VariableDeclaration

            string strCompanyName = "";
            string strCorpAddress = "";
            string strCorpCountry = "";
            string strCorpState = "";
            string strCorpCity = "";
            string strCorpPhoneNo = "";
            string strCorpFaxNo = "";
            string strCorpEmailId = "";
            string strCorpPIN = "";
            string strCorrAddress = "";
            string strCorrCountry = "";
            string strCorrState = "";
            string strCorrCity = "";
            string strCorrPhoneNo = "";
            string strCorrFaxNo = "";
            string strCorrEmailId = "";
            string strCorrPIN = "";
            string strContactPerson = "";
            string strConstCompany = "";
            int intYearOfEstablishment = 0;
            string strPlaceOfIncorp = "";
            string strGSTINNo = "";
            string strGSTINDoc = "";
            string strPANNo = "";
            string strPANDoc = "";
            string strMemorandumDoc = "";
            string strCertOfIncorpDoc = "";
            string strProjectType = "";
            string strApplicationType = "";
            string strBoDName = "";
            string strBoDDesignation = "";
            string strEduQualification = "";
            int intExperience = 0;
            decimal decAnnualTurnOver = 0;
            decimal decProfitAfterTax = 0;
            decimal decResvSurp = 0;
            decimal decShareCapital = 0;
            decimal decNetWorth = 0;
            string strAuditDoc1 = "";
            string strAuditDoc2 = "";
            string strAuditDoc3 = "";
            string strNetWorthDoc = "";
            string strExistingIndustryName = "";
            string strExistingDistrict = "";
            string strExistingBlock = "";
            string strExistingLandAllotted = "";
            decimal decExistingExtentOfLand = 0;
            string strExistingActivity = "";
            string strExistingSector = "";
            string strExistingSubSector = "";
            string strExistingCapacity = "";

            string strUnitName = "";
            string strInvestmentLevel = "";

            string strEINIEMType = "";
            string strEINNo = "";
            string strEINDoc = "";
            string strIEMNo = "";
            string strIEMDoc = "";
            string strPCNo = "";
            string strPCDoc = "";
            string strUAadhaarNo = "";
            string strUAadhaarDoc = "";

            string strSector = "";
            string strSubSector = "";
            string strIsPriority = "";
            string strProductName = "";
            decimal decPropAnnualCapacity = 0;
            string strUnitCapacity = "";
            decimal decLandIncLandDev = 0;
            decimal decBuildingndConstruction = 0;
            decimal decPlantAndMachinery = 0;
            decimal decOthers = 0;
            decimal decTotCapitalInvestment = 0;
            int intPeriodToCommenceProduction = 0;
            string strPollutionCategory = "";
            decimal decEquityContribution = 0;
            decimal decBankAndInstitutionalFin = 0;
            decimal decTotFinance = 0;
            decimal decFDI = 0;
            string strFDIDoc = "";
            string strIRR = "";
            string strDSCR = "";
            int intGroundBreaking = 0;
            int intCivilCompletion = 0;
            int intMajorEquipmentErection = 0;
            int intStartOfCommercialProd = 0;
            string strUdyogAadhaarDoc = "";
            string strFeasibilityReportDoc = "";
            string strBoardResolutionDoc = "";
            int intManagerialProp = 0;
            int intSupervisoryProp = 0;
            int intSkilledProp = 0;
            int intSemiSkilledProp = 0;
            int intUnSkilledProp = 0;
            int intTotalEmpProp = 0;
            int intPropDirectEmployment = 0;
            int intPropContractualEmployment = 0;
            string strProjOtherLocIndia = "";
            string strProjOutsideIndia = "";
            string strLandRequiredGovt = "";
            string strLandDistrict = "";
            string strLandBlock = "";
            decimal decExtentOfLand = 0;
            string strLandRequiredIDCO = "";
            string strIDCOInustrialName = "";
            string strLandAcquiredIDCO = "";
            string strPowerSource = "";
            decimal decPowerDemandGrid = 0;
            decimal decPowerDrawalCPP = 0;
            decimal decCapacityOfCppPlant = 0;
            decimal decPowerProducerIpp = 0;
            string strWaterSource = "";
            decimal decWaterRequiredProposed = 0;
            decimal decWaterRequiredProduction = 0;
            string strOtherWater = "";
            string strQuntRecyclingWaste = "";
            string strWasteConversion = "";
            string strWasteTreatTech = "";
            string strBoarofdDirector = "";

            #endregion
            ///Deserialize the CAF Response 
            NSWSAPIReponseCls.NSWSCAFResponse objApp = new NSWSAPIReponseCls.NSWSCAFResponse();
            objApp = JsonConvert.DeserializeObject<NSWSAPIReponseCls.NSWSCAFResponse>(Txt_CAF_JSON.Text);

            ///Find the "Root" value from JSON.
            bool status = objApp.status;
            string msg = objApp.message.ToString();

            if (status == true)
            {
                ///// Find the "Data" value from JSON.
                NSWSAPIReponseCls.Data objData = new NSWSAPIReponseCls.Data();
                objData = objApp.data;

                investorSWSId = objData.investorSWSId;
                long dateOfInitiation = objData.dateOfInitiation;

                ///// Find the "Section" value from JSON.
                List<NSWSAPIReponseCls.Section> objSec = new List<NSWSAPIReponseCls.Section>();
                //objSec = objData.sections.ToList();
                objSec = objData.sections.OrderBy(x => int.Parse(x.serialNumber)).ToList();
                for (int i = 0; i < objSec.Count; i++)
                {
                    string sectionName = objSec[i].name;
                    string sectionKey = objSec[i].sectionKey;
                    int sectionSerialNo = Convert.ToInt32(objSec[i].serialNumber);

                    List<NSWSAPIReponseCls.Field> objFields = new List<NSWSAPIReponseCls.Field>();
                    objFields = objSec[i].fields.ToList();
                    //objFields = objSec[i].fields.OrderBy(x => int.Parse(x.serialNumber)).ToList();
                    for (int j = 0; j < objFields.Count; j++)
                    {
                        fieldName = objFields[j].name.ToString();
                        string inputVal = Convert.ToString(objFields[j].inputValue);
                        string fieldKey = objFields[j].fieldKey.ToString();
                        int fieldSerialNo = Convert.ToInt32(objFields[j].serialNumber);

                        if (sectionKey == "S-1") //Company Information
                        {
                            #region Company Information

                            if (fieldKey == "F-0") //Name of the Company/Enterprise
                            {
                                strCompanyName = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-2") //Corporate Office Address
                        {
                            #region Corporate Office Address

                            if (fieldKey == "F-1")//Address
                            {
                                #region CorporateAddressSubFields

                                List<NSWSAPIReponseCls.SubField> objSubFields = new List<NSWSAPIReponseCls.SubField>();
                                objSubFields = objFields[j].subFields.ToList();

                                for (int k = 0; k < objSubFields.Count; k++)
                                {
                                    string subFieldName = objSubFields[k].name.ToString();
                                    string subInputVal = Convert.ToString(objSubFields[k].inputValue);
                                    string subFieldKey = objSubFields[k].fieldKey.ToString();

                                    if (subFieldKey == "F-22678")//Address
                                    {
                                        strCorpAddress = subInputVal;
                                    }
                                    else if (subFieldKey == "F-20690")//Country
                                    {
                                        strCorpCountry = subInputVal;
                                    }
                                    else if (subFieldKey == "F-70174")//State
                                    {
                                        strCorpState = subInputVal;
                                    }
                                    else if (subFieldKey == "F-30407")//City
                                    {
                                        strCorpCity = subInputVal;
                                    }
                                    else if (subFieldKey == "F-47395")//Pin Code
                                    {
                                        strCorpPIN = subInputVal;
                                    }
                                }

                                #endregion
                            }
                            else if (fieldKey == "F-5")//Phone Number
                            {
                                if (inputVal != "" && inputVal != null)
                                {
                                    strCorpPhoneNo = JsonConvert.DeserializeObject<Dictionary<string, object>>(inputVal)["phoneNumber"].ToString();
                                }
                            }
                            else if (fieldKey == "F-6")//Fax Number
                            {
                                if (inputVal != "" && inputVal != null)
                                {
                                    strCorpFaxNo = JsonConvert.DeserializeObject<Dictionary<string, object>>(inputVal)["phoneNumber"].ToString();
                                }
                            }
                            else if (fieldKey == "F-7")//Email Address
                            {
                                strCorpEmailId = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-3") //Correspondence Address
                        {
                            #region Correspondence Address

                            if (fieldKey == "F-111")//Constitution of Company/Enterprise
                            {
                                strConstCompany = inputVal;
                            }
                            else if (fieldKey == "F-15")//Fax Number
                            {
                                if (inputVal != "" && inputVal != null)
                                {
                                    strCorrFaxNo = JsonConvert.DeserializeObject<Dictionary<string, object>>(inputVal)["phoneNumber"].ToString();
                                }
                            }
                            else if (fieldKey == "F-19434")//Mobile Number 
                            {
                                if (inputVal != "" && inputVal != null)
                                {
                                    strCorrPhoneNo = JsonConvert.DeserializeObject<Dictionary<string, object>>(inputVal)["phoneNumber"].ToString();
                                }
                            }
                            else if (fieldKey == "F-16")//Email Address
                            {
                                strCorrEmailId = inputVal;
                            }
                            else if (fieldKey == "F-9")//Name of Contact Person
                            {
                                strContactPerson = inputVal;
                            }
                            else if (fieldKey == "F-2")//Address
                            {
                                #region CorrespondenceAddressSubField

                                List<NSWSAPIReponseCls.SubField> objSubFields = new List<NSWSAPIReponseCls.SubField>();
                                objSubFields = objFields[j].subFields.ToList();

                                for (int k = 0; k < objSubFields.Count; k++)
                                {
                                    string subFieldName = objSubFields[k].name.ToString();
                                    string subInputVal = Convert.ToString(objSubFields[k].inputValue);
                                    string subFieldKey = objSubFields[k].fieldKey.ToString();

                                    if (subFieldKey == "F-83364")//Address 1
                                    {
                                        strCorrAddress = subInputVal;
                                    }
                                    else if (subFieldKey == "F-69945")//Country
                                    {
                                        strCorrCountry = subInputVal;
                                    }
                                    else if (subFieldKey == "F-46323")//State
                                    {
                                        strCorrState = subInputVal;
                                    }
                                    else if (subFieldKey == "F-6502")//City
                                    {
                                        strCorrCity = subInputVal;
                                    }
                                    else if (subFieldKey == "F-29759")//Pin Code
                                    {
                                        strCorrPIN = subInputVal;
                                    }
                                }

                                #endregion
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-4") //Entrepreneur Registration Details *
                        {
                            #region Entrepreneur Registration Details

                            if (fieldKey == "F-3") //Year of Establishment
                            {
                                intYearOfEstablishment = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-20")//Place of incorporation
                            {
                                strPlaceOfIncorp = inputVal;
                            }
                            else if (fieldKey == "F-21")//GSTIN Number
                            {
                                strGSTINNo = inputVal;
                            }
                            else if (fieldKey == "F-22") // doc GSTIN DOCUMENT
                            {
                                strGSTINDoc = inputVal;
                            }
                            else if (fieldKey == "F-4") //Enter Pan Number
                            {
                                strPANNo = inputVal;
                            }
                            else if (fieldKey == "F-24") //PAN upload document
                            {
                                strPANDoc = inputVal;
                            }
                            else if (fieldKey == "F-25") //Memorandum & Articles of Association
                            {
                                strMemorandumDoc = inputVal;
                            }
                            else if (fieldKey == "F-26") //Certificate of incorporation/Registration/ Partnership Deed
                            {
                                strCertOfIncorpDoc = inputVal;
                            }
                            else if (fieldKey == "Project Type")  //project type Value come under Project Information -> Invesment tLevel
                            {
                                strProjectType = inputVal;
                            }
                            else if (fieldKey == "F-10") //Application Type
                            {
                                strApplicationType = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-5") // Details of the Management **
                        {
                            #region Details of the Management



                            if (fieldName == "Board of Directors' name/Designation Shareholding Pattern")
                            {
                                strBoDName = inputVal;
                            }
                            else if (fieldName == "Educational Qualification of one of the Directors Technical Qualification of one of the Directors")
                            {
                                strEduQualification = inputVal;
                            }
                            else if (fieldKey == "F-31") //Experience in Years
                            {
                                if(inputVal=="")
                                {
                                    inputVal = "0";
                                }
                                intExperience = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey== "F-11") //Board of Director Details ** this value is applicable for 
                            {
                                #region Board Director Details store in json string 

                                List<NSWSAPIReponseCls.SubField> objSubFields = new List<NSWSAPIReponseCls.SubField>();
                                objSubFields = objFields[j].subFields.ToList();
                                strBoarofdDirector = JsonConvert.SerializeObject(objSubFields);

                                #endregion
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-6") //Financial Status
                        {
                            #region Financial Status

                            if (fieldKey == "F-32") //Annual turn over (In Lakh)
                            {
                                decAnnualTurnOver = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-33") // Profit after tax(In Lakh)
                            {
                                decProfitAfterTax = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-34") //Reserve and surplus (In Lakh)
                            {
                                decResvSurp = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-35") //Share capital (In Lakh)
                            {
                                decShareCapital = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-36") //Net worth (In Lakh)
                            {
                                decNetWorth = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-37") //Audited Financial Statements for First Year
                            {
                                strAuditDoc1 = inputVal;
                            }
                            else if (fieldKey == "F-38") //Audited Financial Statements for Second Year
                            {
                                strAuditDoc2 = inputVal;
                            }
                            else if (fieldKey == "F-39") //Audited Financial Statements for Third Year
                            {
                                strAuditDoc3 = inputVal;
                            }
                            else if (fieldKey == "F-40") //Net Worth Certified by CA
                            {
                                strNetWorthDoc = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-7") //Existing Industry Details
                        {
                            #region Existing Industry Details

                            if (fieldKey == "F-41") //Existing Industry Name
                            {
                                strExistingIndustryName = inputVal;
                            }
                            else if (fieldKey == "F-42") //District
                            {
                                strExistingDistrict = inputVal;
                            }
                            else if (fieldKey == "F-43")  //Block
                            {
                                strExistingBlock = inputVal;
                            }
                            else if (fieldKey == "F-44") //Whether land allotted by IDCO
                            {
                                strExistingLandAllotted = inputVal;
                            }
                            else if (fieldKey == "F-45") //Extent of land (in acres)
                            {
                                if(!string.IsNullOrEmpty(inputVal))
                                {
                                    decExistingExtentOfLand = Convert.ToDecimal(inputVal);
                                }
                                else
                                {
                                    decExistingExtentOfLand = 0;
                                }
                                
                            }
                            else if (fieldKey == "F-46") //Nature of Activity
                            {
                                strExistingActivity = inputVal;
                            }
                            else if (fieldKey == "F-47") //Sector
                            {
                                strExistingSector = inputVal;
                                
                            }
                            else if (fieldKey == "F-48") //Sub-sector
                            {
                                strExistingSubSector = inputVal;
                                
                            }
                            else if (fieldKey == "F-49") //Capacity
                            {
                                strExistingCapacity = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-8") ////Page-2  Project Information  **
                        {
                            #region Project Information

                            if (fieldKey == "F-50") //Name of Unit
                            {
                                strUnitName = inputVal;
                            }
                            else if (fieldKey == "F-51") //Investment Level
                            {
                                strInvestmentLevel = inputVal;
                            }
                            else if (fieldKey == "F-52") //EIN / IEM / Udyog Aadhaar / Production Certificate / Udyam Registration
                            {
                                strEINIEMType = inputVal;
                            }
                            else if (fieldKey == "F-53") //EIN number
                            {
                                strEINNo = inputVal;
                            }
                            else if (fieldKey == "F-54") //IEM number
                            {
                                strIEMNo = inputVal;
                            }
                            else if (fieldKey == "F-55") //Production Certificate Number
                            {
                                strPCNo = inputVal;
                            }
                            else if (fieldKey == "F-8") //Enter Aadhar number
                            {
                                strUAadhaarNo = inputVal;
                            }
                            else if (fieldKey == "F-57") //Upload EIN Document
                            {
                                strEINDoc = inputVal;
                            }
                            else if (fieldKey == "F-58") //Upload IEM Document
                            {
                                strIEMDoc = inputVal;
                            }
                            else if (fieldKey == "F-59") //Upload Production Certificate Document
                            {
                                strPCDoc = inputVal;
                            }
                            else if (fieldKey == "F-60") //Upload Udyog Aadhaar Document
                            {
                                strUAadhaarDoc = inputVal;
                            }
                            else if (fieldKey == "F-17") //Sector of activity /* Now will come sector
                            {
                                strSector = inputVal;
                            }
                            else if (fieldKey == "F-96088") //Sub sector
                            {
                                strSubSector = inputVal;
                            }
                            else if (fieldKey == "F-62") //Is the Project coming under Priority Sector
                            {
                                strIsPriority = inputVal;
                            }
                            else if (fieldKey == "F-18") //Product Details ****Product Name -Now Come from Under Subfield
                            {

                                #region  Product Details

                                List<NSWSAPIReponseCls.SubField> objSubFields = new List<NSWSAPIReponseCls.SubField>();
                                objSubFields = objFields[j].subFields.ToList();

                                for (int k = 0; k < objSubFields.Count; k++)
                                {
                                    string subFieldName = objSubFields[k].name.ToString();
                                    string subInputVal = Convert.ToString(objSubFields[k].inputValue);
                                    string subFieldKey = objSubFields[k].fieldKey.ToString();

                                    if (subFieldKey == "F-28165") //Product Name
                                    {
                                        strProductName = subInputVal;
                                       
                                    }
                                    else if (subFieldKey == "F-61117") //Product Annual Capacity
                                    {                                     
                                        decPropAnnualCapacity = Convert.ToDecimal(subInputVal);
                                    }                                    
                                }

                                #endregion

                                //strProductName = inputVal;
                            }
                            else if (fieldName == "Proposed annual capacity") //* Not use
                            {
                                decPropAnnualCapacity = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldName == "Unit for capacity") // //* Not use
                            {
                                strUnitCapacity = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-9")//// Page-2 //Proposed Capital Investment
                        {
                            #region Proposed Capital Investment

                            if (fieldKey == "F-65") //Land including land development (In Lakh)
                            {
                                decLandIncLandDev = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-66") //Building & civil construction (In Lakh)
                            {
                                decBuildingndConstruction = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-67") //Other (In Lakh)
                            {
                                decOthers = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-68") //Total Capital Investment (In Lakh)
                            {
                                decTotCapitalInvestment = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-69") //Plant & machinery (In Lakh)
                            {
                                decPlantAndMachinery = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-12") //Period to commence commercial production (in months)
                            {
                                intPeriodToCommenceProduction = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-13") //Pollution category
                            {
                                strPollutionCategory = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-10")//// Page-2  //Means of Finance for Capital Investment
                        {
                            #region Means of Finance for Capital Investment

                            if (fieldKey == "F-72") //Equity contribution (In Lakh)
                            {
                                decEquityContribution = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-73") //Bank/institutional finance (In Lakh)
                            {
                                decBankAndInstitutionalFin = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-74") //Total (In Lakh)
                            {
                                decTotFinance = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-75") //Foreign Director Investment (FDI) (if any) (In Lakh)
                            {
                                decFDI = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-76") //In case of FDI please upload relevant document
                            {
                                strFDIDoc = inputVal;
                            }
                            else if (fieldKey == "F-77") //IRR
                            {
                                strIRR = inputVal;
                            }
                            else if (fieldKey == "F-78") //DSCR
                            {
                                strDSCR = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-11") //// Page-2 //Project Implementation Schedule  *
                        {
                            #region Project Implementation Schedule

                            if (fieldKey == "F-79") //Ground breaking (months)
                            {
                                intGroundBreaking = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-80") //Civil and structural completion
                            {
                                intCivilCompletion = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-81") //Major equipment erection
                            {
                                intMajorEquipmentErection = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-82") //Start of Commercial Production
                            {
                                intStartOfCommercialProd = Convert.ToInt32(inputVal);
                            }
                            else if (fieldName == "Udyog Aadhaar")   //*This fieldName not found
                            {
                                strUdyogAadhaarDoc = inputVal;
                            }
                            else if (fieldKey == "F-84") //Feasibility report
                            {
                                strFeasibilityReportDoc = inputVal;
                            }
                            else if (fieldKey == "F-85") //Board resolution to take up the project
                            {
                                strBoardResolutionDoc = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-12") //// Page-2 //Employment Potential
                        {
                            #region Employment Potential

                            if (fieldKey == "F-86") //Managerial
                            {
                                intManagerialProp = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-87") //Supervisory
                            {
                                intSupervisoryProp = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-88") //Skilled
                            {
                                intSkilledProp = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-89") //Semi Skilled
                            {
                                intSemiSkilledProp = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-90") //Unskilled
                            {
                                intUnSkilledProp = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-91") //Total Employment
                            {
                                intTotalEmpProp = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-92") //Proposed direct employment (On company payroll)
                            {
                                intPropDirectEmployment = Convert.ToInt32(inputVal);
                            }
                            else if (fieldKey == "F-93") //Proposed contractual employment
                            {
                                intPropContractualEmployment = Convert.ToInt32(inputVal);
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-13") //// Page-2 //Projects at other Locations
                        {
                            #region Projects at other Locations

                            if (fieldKey == "F-94") // Does the company have projects at other locations in India ?
                            {
                                strProjOtherLocIndia = inputVal;
                            }
                            else if (fieldKey == "F-95") //Is there any unit outside India
                            {
                                strProjOutsideIndia = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-14") //// Page-3  Proposed location of land
                        {
                            #region Proposed location of land

                            if (fieldKey == "F-96") //Land required from government
                            {
                                strLandRequiredGovt = inputVal;
                            }
                            else if (fieldKey == "F-97") //District
                            {
                                strLandDistrict = inputVal;
                            }
                            else if (fieldKey == "F-98") //Block
                            {
                                strLandBlock = inputVal;
                            }
                            else if (fieldKey == "F-99") //Extent of land required(in acre)
                            {
                                decExtentOfLand = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-100") //Whether land is required in IDCO Industrial Estate
                            {
                                strLandRequiredIDCO = inputVal;
                            }
                            else if (fieldKey == "F-101") //Name of the IDCO Industrial Estate
                            {
                                strIDCOInustrialName = inputVal;
                            }
                            else if (fieldKey == "F-102") //Whether land to be acquired by IDCO
                            {
                                strLandAcquiredIDCO = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-15")//// Page-3 Power requirement during production
                        {
                            #region Power requirement during production

                            if (fieldKey == "F-103") //Source of supply
                            {
                                strPowerSource = inputVal;
                            }
                            else if (fieldKey == "F-104") //Power demand from GRID (in KW)
                            {
                                if (inputVal == "")
                                {
                                    inputVal = "0";
                                }
                                decPowerDemandGrid = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-2568") //Power drawal from IPP (in KW)
                            {
                                if (inputVal == "")
                                {
                                    inputVal = "0";
                                }
                                decPowerProducerIpp = Convert.ToDecimal(inputVal);
                            }

                            else if (fieldKey == "F-105") //Power drawal from CPP(in KW)
                            {
                                if(inputVal=="")
                                {
                                    inputVal = "0";
                                }
                                decPowerDrawalCPP = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-106") //Capacity of the CPP plant (in KW)
                            {
                                if (inputVal == "")
                                {
                                    inputVal = "0";
                                }
                                decCapacityOfCppPlant = Convert.ToDecimal(inputVal);
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-16")////Page-3  Water requirement
                        {
                            #region Water requirement

                            if (fieldKey == "F-109") //Sources of water for production
                            {
                                strWaterSource = inputVal;                                                       
                            }
                            else if (fieldKey == "F-110") //Proposed total water requirement (in cusec)
                            {
                                decWaterRequiredProposed = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-112") //Water required for production (in cusec)
                            {
                                decWaterRequiredProduction = Convert.ToDecimal(inputVal);
                            }
                            else if (fieldKey == "F-113") //In Case of Other, please specify
                            {
                                strOtherWater = inputVal;
                            }

                            #endregion
                        }
                        else if (sectionKey == "S-17") //// Page-3 Waste Water Management
                        {
                            #region Waste Water Management

                            if (fieldKey == "F-114") //Quantum of recycling of waste water
                            {
                                strQuntRecyclingWaste = inputVal;
                            }
                            else if (fieldKey == "F-115") //Waste conservation measures
                            {
                                strWasteConversion = inputVal;
                            }
                            else if (fieldKey == "F-116") //Waste water treatment technology and management of solid/hazardous waste
                            {
                                strWasteTreatTech = inputVal;
                            }

                            #endregion
                        }
                    }
                }

                /*-------------------------------------------------------------------------*/
                ////Insert to database here (DML Operation)
                /*-------------------------------------------------------------------------*/
                objCommand.CommandText = "USP_NSWS_STATE_CAF_AED";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = objConn;

                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "A");
                objCommand.Parameters.AddWithValue("@P_vchInvestorSWSId", investorSWSId);
                objCommand.Parameters.AddWithValue("@P_vchCompanyName", strCompanyName);
                objCommand.Parameters.AddWithValue("@P_vchCorpAddress", strCorpAddress);
                objCommand.Parameters.AddWithValue("@P_vchCorpCountry", strCorpCountry);
                objCommand.Parameters.AddWithValue("@P_vchCorpState", strCorpState);
                objCommand.Parameters.AddWithValue("@P_vchCorpCity", strCorpCity);
                objCommand.Parameters.AddWithValue("@P_vchCorpPhoneNo", strCorpPhoneNo);
                objCommand.Parameters.AddWithValue("@P_vchCorpFaxNo", strCorpFaxNo);
                objCommand.Parameters.AddWithValue("@P_vchCorpEmailId", strCorpEmailId);
                objCommand.Parameters.AddWithValue("@P_vchCorpPIN", strCorpPIN);
                objCommand.Parameters.AddWithValue("@P_vchCorrAddress", strCorrAddress);
                objCommand.Parameters.AddWithValue("@P_vchCorrCountry", strCorrCountry);
                objCommand.Parameters.AddWithValue("@P_vchCorrState", strCorrState);
                objCommand.Parameters.AddWithValue("@P_vchCorrCity", strCorrCity);
                objCommand.Parameters.AddWithValue("@P_vchCorrPhoneNo", strCorrPhoneNo);
                objCommand.Parameters.AddWithValue("@P_vchCorrFaxNo", strCorrFaxNo);
                objCommand.Parameters.AddWithValue("@P_vchCorrEmailId", strCorrEmailId);
                objCommand.Parameters.AddWithValue("@P_vchCorrPIN", strCorrPIN);
                objCommand.Parameters.AddWithValue("@P_vchContactPerson", strContactPerson);
                objCommand.Parameters.AddWithValue("@P_vchConstCompany", strConstCompany);
                objCommand.Parameters.AddWithValue("@P_intYearOfEstablishment", intYearOfEstablishment);
                objCommand.Parameters.AddWithValue("@P_vchPlaceOfIncorp", strPlaceOfIncorp);
                objCommand.Parameters.AddWithValue("@P_vchGSTINNo", strGSTINNo);
                objCommand.Parameters.AddWithValue("@P_vchGSTINDoc", strGSTINDoc);
                objCommand.Parameters.AddWithValue("@P_vchPANNo", strPANNo);
                objCommand.Parameters.AddWithValue("@P_vchPANDoc", strPANDoc);
                objCommand.Parameters.AddWithValue("@P_vchMemorandumDoc", strMemorandumDoc);
                objCommand.Parameters.AddWithValue("@P_vchCertOfIncorpDoc", strCertOfIncorpDoc);
                objCommand.Parameters.AddWithValue("@P_vchProjectType", strProjectType);
                objCommand.Parameters.AddWithValue("@P_vchApplicationType", strApplicationType);
                objCommand.Parameters.AddWithValue("@P_vchBoDName", strBoDName);
                objCommand.Parameters.AddWithValue("@P_vchBoDDesignation", strBoDDesignation);
                objCommand.Parameters.AddWithValue("@P_intExperience", intExperience);
                objCommand.Parameters.AddWithValue("@P_decAnnualTurnOver", decAnnualTurnOver);
                objCommand.Parameters.AddWithValue("@P_decProfitAfterTax", decProfitAfterTax);
                objCommand.Parameters.AddWithValue("@P_decResvSurp", decResvSurp);
                objCommand.Parameters.AddWithValue("@P_decShareCapital", decShareCapital);
                objCommand.Parameters.AddWithValue("@P_decNetWorth", decNetWorth);
                objCommand.Parameters.AddWithValue("@P_vchAuditDoc1", strAuditDoc1);
                objCommand.Parameters.AddWithValue("@P_vchAuditDoc2", strAuditDoc2);
                objCommand.Parameters.AddWithValue("@P_vchAuditDoc3", strAuditDoc3);
                objCommand.Parameters.AddWithValue("@P_vchNetWorthDoc", strNetWorthDoc);
                objCommand.Parameters.AddWithValue("@P_vchExistingIndustryName", strExistingIndustryName);
                objCommand.Parameters.AddWithValue("@P_vchExistingDistrict", strExistingDistrict);
                objCommand.Parameters.AddWithValue("@P_vchExistingBlock", strExistingBlock);
                objCommand.Parameters.AddWithValue("@P_vchExistingLandAllotted", strExistingLandAllotted);
                objCommand.Parameters.AddWithValue("@P_decExistingExtentOfLand", decExistingExtentOfLand);
                objCommand.Parameters.AddWithValue("@P_vchExistingActivity", strExistingActivity);
                objCommand.Parameters.AddWithValue("@P_vchExistingSector", strExistingSector);
                objCommand.Parameters.AddWithValue("@P_vchExistingSubSector", strExistingSubSector);
                objCommand.Parameters.AddWithValue("@P_vchExistingCapacity", strExistingCapacity);
                objCommand.Parameters.AddWithValue("@P_vchUnitName", strUnitName);
                objCommand.Parameters.AddWithValue("@P_vchInvestmentLevel", strInvestmentLevel);
                objCommand.Parameters.AddWithValue("@P_vchEINIEMType", strEINIEMType);
                objCommand.Parameters.AddWithValue("@P_vchEINNo", strEINNo);
                objCommand.Parameters.AddWithValue("@P_vchEINDoc", strEINDoc);
                objCommand.Parameters.AddWithValue("@P_vchIEMNo", strIEMNo);
                objCommand.Parameters.AddWithValue("@P_vchIEMDoc", strIEMDoc);
                objCommand.Parameters.AddWithValue("@P_vchPCNo", strPCNo);
                objCommand.Parameters.AddWithValue("@P_vchPCDoc", strPCDoc);
                objCommand.Parameters.AddWithValue("@P_vchUAadhaarNo", strUAadhaarNo);
                objCommand.Parameters.AddWithValue("@P_vchUAadhaarDoc", strUdyogAadhaarDoc);
                objCommand.Parameters.AddWithValue("@P_vchSector", strSector);
                objCommand.Parameters.AddWithValue("@P_vchSubSector", strSubSector);
                objCommand.Parameters.AddWithValue("@P_vchIsPriority", strIsPriority);
                objCommand.Parameters.AddWithValue("@P_vchProductName", strProductName);
                objCommand.Parameters.AddWithValue("@P_decPropAnnualCapacity", decPropAnnualCapacity);
                objCommand.Parameters.AddWithValue("@P_vchUnitCapacity", strUnitCapacity);
                objCommand.Parameters.AddWithValue("@P_decLandIncLandDev", decLandIncLandDev);
                objCommand.Parameters.AddWithValue("@P_decBuildingndConstruction", decBuildingndConstruction);
                objCommand.Parameters.AddWithValue("@P_decPlantAndMachinery", decPlantAndMachinery);
                objCommand.Parameters.AddWithValue("@P_decOthers", decOthers);
                objCommand.Parameters.AddWithValue("@P_decTotCapitalInvestment", decTotCapitalInvestment);
                objCommand.Parameters.AddWithValue("@P_intPeriodToCommenceProduction", intPeriodToCommenceProduction);
                objCommand.Parameters.AddWithValue("@P_vchPollutionCategory", strPollutionCategory);
                objCommand.Parameters.AddWithValue("@P_decEquityContribution", decEquityContribution);
                objCommand.Parameters.AddWithValue("@P_decBankAndInstitutionalFin", decBankAndInstitutionalFin);
                objCommand.Parameters.AddWithValue("@P_decTotFinance", decTotFinance);
                objCommand.Parameters.AddWithValue("@P_decFDI", decFDI);
                objCommand.Parameters.AddWithValue("@P_vchFDIDoc", strFDIDoc);
                objCommand.Parameters.AddWithValue("@P_vchIRR", strIRR);
                objCommand.Parameters.AddWithValue("@P_vchDSCR", strDSCR);
                objCommand.Parameters.AddWithValue("@P_intGroundBreaking", intGroundBreaking);
                objCommand.Parameters.AddWithValue("@P_intCivilCompletion", intCivilCompletion);
                objCommand.Parameters.AddWithValue("@P_intMajorEquipmentErection", intMajorEquipmentErection);
                objCommand.Parameters.AddWithValue("@P_intStartOfCommercialProd", intStartOfCommercialProd);
                objCommand.Parameters.AddWithValue("@P_vchUdyogAadhaarDoc", strUdyogAadhaarDoc);
                objCommand.Parameters.AddWithValue("@P_vchFeasibilityReportDoc", strFeasibilityReportDoc);
                objCommand.Parameters.AddWithValue("@P_vchBoardResolutionDoc", strBoardResolutionDoc);
                objCommand.Parameters.AddWithValue("@P_intManagerialProp", intManagerialProp);
                objCommand.Parameters.AddWithValue("@P_intSupervisoryProp", intSupervisoryProp);
                objCommand.Parameters.AddWithValue("@P_intSkilledProp", intSkilledProp);
                objCommand.Parameters.AddWithValue("@P_intSemiSkilledProp", intSemiSkilledProp);
                objCommand.Parameters.AddWithValue("@P_intUnSkilledProp", intUnSkilledProp);
                objCommand.Parameters.AddWithValue("@P_intTotalEmpProp", intTotalEmpProp);
                objCommand.Parameters.AddWithValue("@P_intPropDirectEmployment", intPropDirectEmployment);
                objCommand.Parameters.AddWithValue("@P_intPropContractualEmployment", intPropContractualEmployment);
                objCommand.Parameters.AddWithValue("@P_vchProjOtherLocIndia", strProjOtherLocIndia);
                objCommand.Parameters.AddWithValue("@P_vchProjOutsideIndia", strProjOutsideIndia);
                objCommand.Parameters.AddWithValue("@P_vchLandRequiredGovt", strLandRequiredGovt);
                objCommand.Parameters.AddWithValue("@P_vchLandDistrict", strLandDistrict);
                objCommand.Parameters.AddWithValue("@P_vchLandBlock", strLandBlock);
                objCommand.Parameters.AddWithValue("@P_decExtentOfLand", decExtentOfLand);
                objCommand.Parameters.AddWithValue("@P_vchLandRequiredIDCO", strLandRequiredIDCO);
                objCommand.Parameters.AddWithValue("@P_vchIDCOInustrialName", strIDCOInustrialName);
                objCommand.Parameters.AddWithValue("@P_vchLandAcquiredIDCO", strLandAcquiredIDCO);
                objCommand.Parameters.AddWithValue("@P_vchPowerSource", strPowerSource);
                objCommand.Parameters.AddWithValue("@P_decPowerDemandGrid", decPowerDemandGrid);
                objCommand.Parameters.AddWithValue("@P_decPowerDrawalCPP", decPowerDrawalCPP);
                objCommand.Parameters.AddWithValue("@P_decCapacityOfCppPlant", decCapacityOfCppPlant);
                objCommand.Parameters.AddWithValue("@P_decPowerProducerIpp", decPowerProducerIpp);
                objCommand.Parameters.AddWithValue("@P_vchWaterSource", strWaterSource);
                objCommand.Parameters.AddWithValue("@P_decWaterRequiredProposed", decWaterRequiredProposed);
                objCommand.Parameters.AddWithValue("@P_decWaterRequiredProduction", decWaterRequiredProduction);
                objCommand.Parameters.AddWithValue("@P_vchOtherWater", strOtherWater);
                objCommand.Parameters.AddWithValue("@P_vchQuntRecyclingWaste", strQuntRecyclingWaste);
                objCommand.Parameters.AddWithValue("@P_vchWasteConversion", strWasteConversion);
                objCommand.Parameters.AddWithValue("@P_vchWasteTreatTech", strWasteTreatTech);
                objCommand.Parameters.AddWithValue("@P_vchBoardofDirector", strBoarofdDirector);
                objCommand.Parameters.AddWithValue("P_VCH_OUT_MSG", SqlDbType.VarChar);
                objCommand.Parameters["P_VCH_OUT_MSG"].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                strRetrunVal = objCommand.Parameters["P_VCH_OUT_MSG"].Value.ToString();

            }
            else
            {
                Util.LogRequestResponse("NSWSScheduler", "PullAndInsertStateCAF", "No Records Found for Investor SWS Id:- " + investorSWSId);
            }
        }
        catch (Exception ex)
        {

            Lbl_Error_Msg_CAF.Text = ex.Message.ToString();
            Lbl_Error_Line_Msg_CAF.Text = "[ERROR_OCCURED FOR FIELD_NAME]:- " + fieldName;
        }
    }


    protected void Btn_Pull_CAF_JSON_Test_Click(object sender, EventArgs e)
    {
        NSWSScheduler obj = new NSWSScheduler();
        obj.PullAndInsertStateCAF(Txt_CAF_JSON.Text);
    }




    private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
    private bool IsFileValid(byte[] filebyte)
    {
        if (filebyte.Take(7).SequenceEqual(PDF))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public class DocResponseFile
    {
        public string fileName { get; set; }
        public string fileResponse { get; set; }
    }

    public class PullApiDoc
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<DocResponseFile> response { get; set; }
    }


    public class SubField
    {
        public string name { get; set; }
        public string inputValue { get; set; }
    }

    public class Fields
    {
        public string name { get; set; }
        public string inputValue { get; set; }
        public string fieldKey { get; set; }
        public string serialNumber { get; set; }
        public List<SubField> subFields { get; set; }
    }
    public class Sections
    {
        public string name { get; set; }
        public IList<Fields> fields { get; set; }
        public string sectionKey { get; set; }
        public string serialNumber { get; set; }



    }
    public class Data
    {
        public long dateOfInitiation { get; set; }
        public string investorSWSId { get; set; }
        public IList<Sections> sections { get; set; }
    }
    public class NSWSCAFResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

}

//public static class UnixTimeHelper
//{
//    public static long ToUnixTime(this DateTime time)
//    {
//        var totalSeconds = (long)(time.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
//        return totalSeconds;
//    }
//}