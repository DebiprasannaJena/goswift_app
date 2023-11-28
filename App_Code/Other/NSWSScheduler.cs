using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Net;
using System.Configuration;
using RestSharp;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using DWHServiceReference;
using System.Security.Cryptography;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using Common.Persistence.Data;

/// <summary>
/// Summary description for NSWSScheduler
/// </summary>
public class NSWSScheduler
{
    SqlConnection objConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    /*---------------------------------------------------------------------------------------------*/
    /////Get the api-key, access-id, and access-secret of NSWS APIs from the web configuration file.
    /*---------------------------------------------------------------------------------------------*/
    string strAccessId = ConfigurationManager.AppSettings["NswsApiAccessId"].ToString();
    string strAccessSecret = ConfigurationManager.AppSettings["NswsApiAccessSecret"].ToString();

    string strApiKeyPullDoc = ConfigurationManager.AppSettings["NswsApiKeyPullDoc"].ToString();
    string strApiKeyPushDocWithUrl = ConfigurationManager.AppSettings["NswsApiKeyPushDocWithUrl"].ToString();
    string strApiKeyPostLicence = ConfigurationManager.AppSettings["NswsApiKeyPostLicence"].ToString();
    string strApiKeyPushLicence = ConfigurationManager.AppSettings["NswsApiKeyPushLicence"].ToString();

    /*---------------------------------------------------------------------------------------------*/

    public NSWSScheduler()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Send the dymanic redirection url to NSWS portal and update the sent status in GOSWIFT portal.
    /// </summary>
    public void UpdateRedirectionURL()
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataTable objdt = new DataTable();
        string results = string.Empty;

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }

        try
        {
            objCommand.CommandText = "USP_NSWS_FETCH_DATA";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "A");

            objDa.SelectCommand = objCommand;
            objDa.Fill(objdt);

            if (objdt.Rows.Count > 0)
            {
                Util.LogRequestResponse("NSWSScheduler", "UpdateRedirectionURL", "[RECORD_FOUND]:- " + objdt.Rows.Count.ToString());

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    string strInvUserId = Convert.ToString(objdt.Rows[i]["VCH_INV_USERID"]);
                    string strSWSId = Convert.ToString(objdt.Rows[i]["VCH_INV_SWS_ID_NSWS"]);
                    string strRedirectionUrl = Convert.ToString(objdt.Rows[i]["VCH_REDIRECT_URL_NSWS"]);

                    if (i > 0)
                    {
                        Thread.Sleep(50000); //// Wait for around 1 min
                    }

                    /*-----------------------------------------------------------------------------------------------------*/
                    ///Send the redirection URL to NSWS portal.
                    ///Generate access token and use it to call the Redirection API to send the dynamic url. 
                    /*-----------------------------------------------------------------------------------------------------*/
                    ///Get the API Address from the Web.Config
                    string strTokenUrl = ConfigurationManager.AppSettings["NswsTokenGenerationUrl"].ToString();
                    string strRedirectApiUrl = ConfigurationManager.AppSettings["NswsRedirectionApiUrl"].ToString();

                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                    ///Generate Access Token
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

                    /*----------------------------------------------------------------------------*/
                    ///Write the Token Response in the Log File.
                    /*----------------------------------------------------------------------------*/
                    Util.LogRequestResponse("NSWSScheduler", "UpdateRedirectionURL", "[NSWS_TOKEN_RESPONSE]:- " + responseToken.Content.ToString());

                    if (responseToken.Content.ToString() != "")
                    {
                        ///Get the Access Token
                        string strAccessToke = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseToken.Content)["access_token"].ToString();

                        ///Call Redirection API Here
                        string strRedirectionJson = "{\"licenseId\": \"\",\"stateId\": \"12\",\"departmentId\": \"\",\"redirectionUrl\": \"" + strRedirectionUrl + "\",\"swsId\": \"" + strSWSId + "\"}";

                        /*----------------------------------------------------------------------------*/
                        ///Write the Request JSON string in the Log file.
                        /*----------------------------------------------------------------------------*/
                        Util.LogRequestResponse("NSWSScheduler", "UpdateRedirectionURL", "[REQUEST_JSON_STRING_UPDATE_REDIRECTION_URL]:- " + strRedirectionJson);

                        var client2 = new RestClient(strRedirectApiUrl);
                        client2.Timeout = -1;
                        var request2 = new RestRequest(Method.POST);
                        request2.AddHeader("Authorization", "Bearer " + strAccessToke);
                        request2.AddHeader("Content-Type", "application/json");
                        request2.AddParameter("application/json", strRedirectionJson, ParameterType.RequestBody);
                        IRestResponse responseRedirectApi = client2.Execute(request2);

                        /*----------------------------------------------------------------------------*/
                        ///Write the Redirection URL API Response in the Log File.
                        /*----------------------------------------------------------------------------*/
                        Util.LogRequestResponse("NSWSScheduler", "UpdateRedirectionURL", "[RESPONSE_JSON_STRING_UPDATE_REDIRECTION_URL]:- " + responseRedirectApi.Content.ToString());

                        if (responseRedirectApi.Content.ToString() != "")
                        {
                            string strStatus = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseRedirectApi.Content)["status"].ToString();
                            if (strStatus.ToUpper() == "TRUE")
                            {
                                objCommand = new SqlCommand();
                                objCommand.CommandText = "USP_NSWS_USER_REGISTRATION";
                                objCommand.CommandType = CommandType.StoredProcedure;
                                objCommand.Connection = objConn;

                                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "C"); //// Update Redirection URL Sent Status
                                objCommand.Parameters.AddWithValue("@P_VCH_INV_USER_ID", strInvUserId);

                                SqlParameter par = new SqlParameter("@P_VCH_OUT_MSG", SqlDbType.VarChar, 100);
                                par.Direction = ParameterDirection.Output;
                                objCommand.Parameters.Add(par);

                                objCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            else
            {
                Util.LogRequestResponse("NSWSScheduler", "UpdateRedirectionURL", "[NO_RECORDS_FOUND]");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "NSWSScheduler");
        }
        finally
        {
            objConn.Close();
            objCommand = null;
            objdt = null;
        }
    }

    /// <summary>
    /// Pull State CAF details from NSWS portal and save it in GOSWIFT database.
    /// Prepoulate these details in PEAL form.
    /// </summary>
    /// <param name="strInvestorSwsId"></param>
    /// <returns></returns>
    public string PullAndInsertStateCAF(string strInvestorSwsId)
    {
        string strRetrunVal = "";
        SqlCommand objCommand = new SqlCommand();

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

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            /*-----------------------------------------------------------------------------------------------------*/
            ///Token Generation
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
            if (responseToken.Content.ToString() != "")
            {
                ///Get the Access Token
                string strAccessToke = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseToken.Content)["access_token"].ToString();

                /*-----------------------------------------------------------------------------------------------------*/
                ///Consume CAF API (12 is the State Id for Odisha for NSWS)
                /*-----------------------------------------------------------------------------------------------------*/
                string strPullCafUrl = ConfigurationManager.AppSettings["NswsPullCafUrl"].ToString();
                strPullCafUrl = strPullCafUrl + "/" + strInvestorSwsId + "/12";

                /*----------------------------------------------------------------------------*/
                ///Write the Input CAF Request with Token in the Log File.
                /*----------------------------------------------------------------------------*/
                Util.LogRequestResponse("NSWSScheduler", "PullAndInsertStateCAF", "[REQUEST_JSON_STRING_PULL_CAF]:- " + strPullCafUrl + " ------- [TOKEN_USED]:-  " + strAccessToke);

                /*-----------------------------------------------------------------------------------------------------*/
                ///Pull the NSWS State CAF and Insert into GOSWIFT DB.
                /*-----------------------------------------------------------------------------------------------------*/
                var client1 = new RestClient(strPullCafUrl);
                client1.Timeout = -1;
                var request1 = new RestRequest(Method.GET);
                request1.AddHeader("Authorization", "Bearer " + strAccessToke);
                request1.AlwaysMultipartFormData = true;
                IRestResponse responseCAF = client1.Execute(request1);

                /*----------------------------------------------------------------------------*/
                ///Write the State CAF API Response in the Log File.
                /*----------------------------------------------------------------------------*/
                Util.LogRequestResponse("NSWSScheduler", "PullAndInsertStateCAF", "[RESPONSE_JSON_STRING_PULL_CAF]:- " + responseCAF.Content.ToString());

                ///Deserialize the CAF Response 
                NSWSAPIReponseCls.NSWSCAFResponse objApp = new NSWSAPIReponseCls.NSWSCAFResponse();
                objApp = JsonConvert.DeserializeObject<NSWSAPIReponseCls.NSWSCAFResponse>(responseCAF.Content);

                ///Find the "Root" value from JSON.
                bool status = objApp.status;
                string msg = objApp.message.ToString();

                if (status == true)
                {
                    ///Find the "Data" value from JSON.
                    NSWSAPIReponseCls.Data objData = new NSWSAPIReponseCls.Data();
                    objData = objApp.data;

                    string investorSWSId = objData.investorSWSId;
                    long dateOfInitiation = objData.dateOfInitiation;

                    ///Find the "Section" value from JSON.
                    List<NSWSAPIReponseCls.Section> objSec = new List<NSWSAPIReponseCls.Section>();
                    //objSec = objData.sections.ToList();
                    objSec = objData.sections.OrderBy(x => int.Parse(x.serialNumber)).ToList();
                    for (int i = 0; i < objSec.Count; i++)
                    {
                        string sectionName = objSec[i].name;
                        string sectionKey = objSec[i].sectionKey;
                        int sectionSerialNo = Convert.ToInt32(objSec[i].serialNumber);

                        List<NSWSAPIReponseCls.Field> objFields = new List<NSWSAPIReponseCls.Field>();
                        //objFields = objSec[i].fields.ToList();
                        objFields = objSec[i].fields.OrderBy(x => int.Parse(x.serialNumber)).ToList();
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
                                    if (inputVal == "")
                                    {
                                        inputVal = "0";
                                    }
                                    intExperience = Convert.ToInt32(inputVal);
                                }
                                else if (fieldKey == "F-11") //Board of Director Details ** this value is applicable for 
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
                                    if (!string.IsNullOrEmpty(inputVal))
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
                                else if (fieldName == "Udyog Aadhaar") //*This fieldName not found
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
                                    if (inputVal == "")
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
                    ///Insert the CAF details to database (DML Operation)
                    /*-------------------------------------------------------------------------*/
                    objCommand.CommandText = "USP_NSWS_STATE_CAF_AED";
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Connection = objConn;

                    objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "A");
                    objCommand.Parameters.AddWithValue("@P_vchInvestorSWSId", strInvestorSwsId);
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
                    Util.LogRequestResponse("NSWSScheduler", "PullAndInsertStateCAF", "[NO_RECORDS_FOUND_FOR_INVESTOR_SWS_ID]:- " + strInvestorSwsId);
                }
            }

            return strRetrunVal;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "NSWSScheduler");
            Util.LogRequestResponse("NSWSScheduler", "PullAndInsertStateCAF", "[EXCEPTION_OCCURED_FOR_INVESTOR_SWS_ID]:- " + strInvestorSwsId + " [FIELD_NAME]:- " + fieldName);
        }
        finally
        {
            objConn.Close();
            objCommand = null;
        }

        return strRetrunVal;
    }

    /// <summary>
    /// Push License/Service status details to NSWS portal like Approved,Rejected.
    /// After pushing status to NSWS portal update the status in LOG table.
    /// </summary>
    public void PushLicenseStatus()
    {
        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }

        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataTable objdt = new DataTable();

            objCommand.CommandText = "USP_NSWS_FETCH_DATA";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "GAS"); ////Get Application Status

            objDa.SelectCommand = objCommand;
            objDa.Fill(objdt);

            if (objdt.Rows.Count > 0)
            {
                Util.LogRequestResponse("NSWSScheduler", "PushLicenseStatus", "[RECORD_FOUND]:- " + objdt.Rows.Count.ToString());

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        Thread.Sleep(50000); //// Wait for around 1 min
                    }

                    string strApplicationNo = Convert.ToString(objdt.Rows[i]["VCH_APPLICATION_UNQ_KEY"]);
                    int intServiceId = Convert.ToInt32(objdt.Rows[i]["INT_SERVICEID"]);
                    int intStatus = Convert.ToInt32(objdt.Rows[i]["INT_STATUS"]);
                    string strApplyDate = Convert.ToString(objdt.Rows[i]["DTM_APPLY_DATE"]);
                    string strNswsDeptId = Convert.ToString(objdt.Rows[i]["VCH_NSWS_DEPT_ID"]);
                    string strNswsServiceId = Convert.ToString(objdt.Rows[i]["VCH_NSWS_SERVICE_ID"]);
                    string strNswsSwsId = Convert.ToString(objdt.Rows[i]["VCH_INV_SWS_ID_NSWS"]);
                    string strActionDate = Convert.ToString(objdt.Rows[i]["DTM_ACTION_DATE"]);

                    long lngLicenseReqDate = UnixTimeHelper.ToUnixTime(Convert.ToDateTime(strApplyDate));

                    /*-----------------------------------------------------------------------------------------------*/
                    ///If application status is Pending then use Push License Details API to push data.
                    ///If application status is Approved/Rejected then use Post License Status API to send license/service status.
                    /*-----------------------------------------------------------------------------------------------*/
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                    if (intStatus == 1) ////Pending
                    {
                        string strJSON = "[{"
                                        + " \"licenseId\":\"" + strNswsServiceId + "\","
                                        + " \"licenseVer\":\"1\","
                                        + " \"swsId\":\"" + strNswsSwsId + "\","
                                        + " \"investorReqId\":\"" + strApplicationNo.Substring(0, 8) + "\","
                                        + " \"licenseReqDate\":\"" + lngLicenseReqDate + "\","
                                        + " \"ministeryId\":\"S001\","
                                        + " \"departmentId\":\"" + strNswsDeptId + "\""
                                        + " }]";

                        /*----------------------------------------------------------------------------*/
                        ///Write the Request JSON string in Log File
                        /*----------------------------------------------------------------------------*/
                        Util.LogRequestResponse("NSWSScheduler", "PushLicenseStatus", "[REQUEST_JSON_STRING_PUSH_LICENSE_STATUS]:- " + strJSON);

                        /*----------------------------------------------------------------------------*/
                        ///Call Push License Details API to send pending/applied status to the NSWS portal
                        /*----------------------------------------------------------------------------*/
                        string strPushLicenseUrl = ConfigurationManager.AppSettings["NswsPushLicenseApiUrl"].ToString();
                        var client = new RestClient(strPushLicenseUrl);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("access-id", strAccessId);
                        request.AddHeader("access-secret", strAccessSecret);
                        request.AddHeader("api-key", strApiKeyPushLicence);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", strJSON, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);

                        /*----------------------------------------------------------------------------*/
                        ///Write the Response got from NSWS API, in the Log File.
                        /*----------------------------------------------------------------------------*/
                        Util.LogRequestResponse("NSWSScheduler", "PushLicenseStatus", "[RESPONSE_JSON_STRING_PUSH_LICENSE_STATUS]:- " + response.Content.ToString());

                        if (response.Content.ToString() != "")
                        {
                            string strStatus = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content)["status"].ToString();
                            if (strStatus == "200")
                            {
                                ///Insert application status in Log table.
                                InsertAppStatusPushLog(strApplicationNo, intStatus, strJSON, response.Content.ToString());
                            }
                        }
                    }
                    else if (intStatus == 2 || intStatus == 3) //// Approved or Rejected
                    {
                        string strStatus = "";
                        if (intStatus == 2) ////Approved
                        {
                            strStatus = "A";
                        }
                        else if (intStatus == 3)////Rejected
                        {
                            strStatus = "R";
                        }

                        string strLicReqNum = strNswsSwsId + "-" + strNswsServiceId + "-" + Convert.ToString(lngLicenseReqDate);
                        var strJSON = "{"
                                    + " \"departmentId\":\"" + strNswsDeptId + "\","
                                    + " \"ministryStateId\":\"S001\","
                                    + " \"licenseStastusList\":[{\"licenseReqNum\":\"" + strLicReqNum + "\",\"licenseStatus\":\"" + strStatus + "\"}]"
                                    + " }";

                        /*----------------------------------------------------------------------------*/
                        ///Write the Request JSON string in Log File
                        /*----------------------------------------------------------------------------*/
                        Util.LogRequestResponse("NSWSScheduler", "PushLicenseStatus", "[REQUEST_JSON_STRING_PUSH_LICENSE_STATUS]:- " + strJSON);

                        /*----------------------------------------------------------------------------*/
                        ///Call Post License Details API to send approval/rejection status to the NSWS portal
                        /*----------------------------------------------------------------------------*/
                        string strPostLicenseUrl = ConfigurationManager.AppSettings["NswsPostLicenseApiUrl"].ToString();
                        var client = new RestClient(strPostLicenseUrl);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("access-id", strAccessId);
                        request.AddHeader("access-secret", strAccessSecret);
                        request.AddHeader("api-key", strApiKeyPostLicence);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", strJSON, ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);

                        /*----------------------------------------------------------------------------*/
                        ///Write the Response got from NSWS API, in the Log File.
                        /*----------------------------------------------------------------------------*/
                        Util.LogRequestResponse("NSWSScheduler", "PushLicenseStatus", "[RESPONSE_JSON_STRING_PUSH_LICENSE_STATUS] :- " + response.Content.ToString());

                        if (response.Content.ToString() != "")
                        {
                            string strStatus1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content)["status"].ToString();
                            if (strStatus1 == "200")
                            {
                                ///Insert application status in Log table.
                                InsertAppStatusPushLog(strApplicationNo, intStatus, strJSON, response.Content.ToString());
                            }
                        }
                    }
                }
            }
            else
            {
                Util.LogRequestResponse("NSWSScheduler", "PushLicenseStatus", "[NO_RECORDS_FOUND]");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "NSWSScheduler");
        }
        finally
        {
            objConn.Close();
        }
    }

    /// <summary>
    /// Insert application status in Log table. 
    /// </summary>
    /// <param name="strApplicationNo"></param>
    /// <param name="intStatus"></param>
    private void InsertAppStatusPushLog(string strApplicationNo, int intStatus, string strRequestJson, string strResponseJson)
    {
        try
        {
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandText = "USP_NSWS_USER_REGISTRATION";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "D");
            objCommand.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", strApplicationNo);
            objCommand.Parameters.AddWithValue("@P_INT_STATUS", intStatus);
            objCommand.Parameters.AddWithValue("@P_VCH_REQUEST_JSON", strRequestJson);
            objCommand.Parameters.AddWithValue("@P_VCH_RESPONSE_JSON", strResponseJson);

            objCommand.Parameters.AddWithValue("P_VCH_OUT_MSG", SqlDbType.VarChar);
            objCommand.Parameters["P_VCH_OUT_MSG"].Direction = ParameterDirection.Output;

            objCommand.ExecuteNonQuery();
            string strRetrunVal = objCommand.Parameters["P_VCH_OUT_MSG"].Value.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Push License/Service query raise and response status to NSWS portal.
    /// After pushing status to NSWS portal update the status in LOG table.
    /// </summary>
    public void PushQueryStatus()
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataTable objdt = new DataTable();
        string results = string.Empty;

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }
        try
        {
            objCommand.CommandText = "USP_NSWS_FETCH_DATA";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "GQS");

            objDa.SelectCommand = objCommand;
            objDa.Fill(objdt);

            if (objdt.Rows.Count > 0)
            {
                Util.LogRequestResponse("NSWSScheduler", "PushQueryStatus", "[RECORD_FOUND]:- " + objdt.Rows.Count.ToString());

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        Thread.Sleep(50000); //// Wait for around 1 min
                    }

                    string strApplicationNo = Convert.ToString(objdt.Rows[i]["VCH_APPLICATION_UNQ_KEY"]);
                    int intServiceId = Convert.ToInt32(objdt.Rows[i]["INT_SERVICEID"]);
                    string strApplyDate = Convert.ToString(objdt.Rows[i]["DTM_APPLY_DATE"]);
                    string strQueryDate = Convert.ToString(objdt.Rows[i]["DTM_QUERY_DATE"]);
                    string strNswsDeptId = Convert.ToString(objdt.Rows[i]["VCH_NSWS_DEPT_ID"]);
                    string strNswsServiceId = Convert.ToString(objdt.Rows[i]["VCH_NSWS_SERVICE_ID"]);
                    string strNswsSwsId = Convert.ToString(objdt.Rows[i]["VCH_INV_SWS_ID_NSWS"]);
                    string strActionDate = Convert.ToString(objdt.Rows[i]["DTM_QUERY_DATE"]);
                    int intNoOfTimes = Convert.ToInt32(objdt.Rows[i]["intNoOfTimes"]);
                    string strQueryType = Convert.ToString(objdt.Rows[i]["vchQuerytype"]);
                    int intQueryStatus = Convert.ToInt32(objdt.Rows[i]["intStatus"]);

                    long lngLicenseReqDate = UnixTimeHelper.ToUnixTime(Convert.ToDateTime(strApplyDate));

                    string strStatus = "";
                    if (strQueryType == "Q")
                    {
                        strStatus = "Q";
                    }
                    else if (strQueryType == "A")
                    {
                        strStatus = "I";
                    }

                    string strLicReqNum = strNswsSwsId + "-" + strNswsServiceId + "-" + Convert.ToString(lngLicenseReqDate);

                    var strJSON = "{"
                                + " \"departmentId\":\"" + strNswsDeptId + "\","
                                + " \"ministryStateId\":\"S001\","
                                + " \"licenseStastusList\":[{\"licenseReqNum\":\"" + strLicReqNum + "\",\"licenseStatus\":\"" + strStatus + "\"}]"
                                + " }";

                    /*----------------------------------------------------------------------------*/
                    ///Write the Request JSON string in the Log file.
                    /*----------------------------------------------------------------------------*/
                    Util.LogRequestResponse("NSWSScheduler", "PushQueryStatus", "[REQUEST_JSON_STRING_PUSH_QUERY]:- " + strJSON);

                    /*----------------------------------------------------------------------------*/
                    ///Call the Post License Details API to send query raise and response status to the NSWS portal.
                    ///Query Raise   :- Clarification Raise    (GOSWIFT STATUS=Q and NSWS STATUS=Q)
                    ///Query Response:- Clarification Received (GOSWIFT STATUS=A and NSWS STATUS=I)
                    /*----------------------------------------------------------------------------*/
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                    string strPostLicenseUrl = ConfigurationManager.AppSettings["NswsPostLicenseApiUrl"].ToString();
                    var client = new RestClient(strPostLicenseUrl);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("access-id", strAccessId);
                    request.AddHeader("access-secret", strAccessSecret);
                    request.AddHeader("api-key", strApiKeyPostLicence);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", strJSON, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    /*----------------------------------------------------------------------------*/
                    ///Write the Response got from NSWS API, in the Log File.
                    /*----------------------------------------------------------------------------*/
                    Util.LogRequestResponse("NSWSScheduler", "PushQueryStatus", "[RESPONSE_JSON_STRING_PUSH_QUERY]:- " + response.Content.ToString());

                    if (response.Content.ToString() != "")
                    {
                        string strResponseStatus = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content)["status"].ToString();
                        if (strResponseStatus == "200")
                        {
                            ///Insert application status in Log table.
                            InsertQueryStatusPushLog(strApplicationNo, intQueryStatus, intNoOfTimes, strQueryType, strJSON, response.Content.ToString());
                        }
                    }
                }
            }
            else
            {
                Util.LogRequestResponse("NSWSScheduler", "PushQueryStatus", "[NO_RECORDS_FOUND]");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "NSWSScheduler");
        }
        finally
        {
            objConn.Close();
            objCommand = null;
            objdt = null;
        }
    }

    /// <summary>
    /// Insert query status in the Log table. 
    /// </summary>
    /// <param name="strApplicationNo"></param>
    /// <param name="intQueryStatus"></param>
    private void InsertQueryStatusPushLog(string strApplicationNo, int intQueryStatus, int intNoOfTimes, string strQueryType, string strRequestJson, string strResponseJson)
    {
        try
        {
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandText = "USP_NSWS_USER_REGISTRATION";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "E");
            objCommand.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", strApplicationNo);
            objCommand.Parameters.AddWithValue("@P_INT_STATUS", intQueryStatus);
            objCommand.Parameters.AddWithValue("@P_INT_QRY_TIMES", intNoOfTimes);
            objCommand.Parameters.AddWithValue("@P_VCH_QRY_TYPE", strQueryType);
            objCommand.Parameters.AddWithValue("@P_VCH_REQUEST_JSON", strRequestJson);
            objCommand.Parameters.AddWithValue("@P_VCH_RESPONSE_JSON", strResponseJson);

            objCommand.Parameters.AddWithValue("P_VCH_OUT_MSG", SqlDbType.VarChar);
            objCommand.Parameters["P_VCH_OUT_MSG"].Direction = ParameterDirection.Output;

            objCommand.ExecuteNonQuery();
            string strRetrunVal = objCommand.Parameters["P_VCH_OUT_MSG"].Value.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Push Approval Docuement (Certificate) to NSWS
    /// </summary>
    public void PushDocument()
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataTable objdt = new DataTable();
        string results = string.Empty;

        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }

        try
        {
            objCommand.CommandText = "USP_NSWS_FETCH_DATA";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "D"); ////Get Document Sent Status

            objDa.SelectCommand = objCommand;
            objDa.Fill(objdt);

            if (objdt.Rows.Count > 0)
            {
                Util.LogRequestResponse("NSWSScheduler", "PushDocument", "[RECORD_FOUND]:- " + objdt.Rows.Count.ToString());

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        Thread.Sleep(50000); ////Wait for around 1 min
                    }

                    string strApplicationNo = Convert.ToString(objdt.Rows[i]["VCH_APPLICATION_UNQ_KEY"]);
                    int intServiceId = Convert.ToInt32(objdt.Rows[i]["INT_SERVICEID"]);
                    int intStatus = Convert.ToInt32(objdt.Rows[i]["INT_STATUS"]);
                    string strApplyDate = Convert.ToString(objdt.Rows[i]["DTM_APPLY_DATE"]);
                    string strNswsDeptId = Convert.ToString(objdt.Rows[i]["VCH_NSWS_DEPT_ID"]);
                    string strNswsServiceId = Convert.ToString(objdt.Rows[i]["VCH_NSWS_SERVICE_ID"]);
                    string strNswsSwsId = Convert.ToString(objdt.Rows[i]["VCH_INV_SWS_ID_NSWS"]);
                    string strActionDate = Convert.ToString(objdt.Rows[i]["DTM_ACTION_DATE"]);
                    string strFileName = Convert.ToString(objdt.Rows[i]["VCH_CERTIFICATE_FILENAME"]);

                    long lngLicenseReqDate = UnixTimeHelper.ToUnixTime(Convert.ToDateTime(strApplyDate));

                    /*-----------------------------------------------------------------------------------------------*/
                    ///If application status is Pending then use Push License Details API to push data.
                    ///If application status is Approved/Rejected then use Post License Status API to send license/service status.
                    /*-----------------------------------------------------------------------------------------------*/
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                    string strFileDownloadPage = ConfigurationManager.AppSettings["NSWSFileDownloadPage"].ToString();
                    string strDocFilePath = strFileDownloadPage + "?AppNo=" + strApplicationNo;
                    string strApprovalId = strNswsSwsId + "-" + strNswsServiceId + "-" + Convert.ToString(lngLicenseReqDate);

                    var strJSON = "{"
                                + " \"documentId\":\"MDOC000099\","
                                + " \"documentName\":\"License Certificate\","
                                + " \"approvalId\":\"" + strApprovalId + "\","
                                + " \"swsId\":\"" + strNswsSwsId + "\","
                                + " \"investorReqId\":\"INVREQ0011\","
                                + " \"mnstryDprtmntId\":\"S001\","
                                + " \"inputFileURL\":\"" + strDocFilePath + "\","
                                + " \"fileName\":\"" + strFileName + "\""
                                + " }";

                    /*----------------------------------------------------------------------------*/
                    ///Write the Request JSON string in Log File
                    /*----------------------------------------------------------------------------*/
                    Util.LogRequestResponse("NSWSScheduler", "PushDocument", "[REQUEST_JSON_STRING_PUSH_DOCUMENT]:- " + strJSON);

                    /*----------------------------------------------------------------------------*/
                    ///Call Post License Details API to send approval/rejection status to the NSWS portal
                    /*----------------------------------------------------------------------------*/
                    string strPushDocUrl = ConfigurationManager.AppSettings["NswsPushDocWithURLApiUrl"].ToString();
                    var client = new RestClient(strPushDocUrl);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("access-id", strAccessId);
                    request.AddHeader("access-secret", strAccessSecret);
                    request.AddHeader("api-key", strApiKeyPushDocWithUrl);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", strJSON, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    /*----------------------------------------------------------------------------*/
                    ///Write the Response got from NSWS API, in the Log File.
                    /*----------------------------------------------------------------------------*/
                    Util.LogRequestResponse("NSWSScheduler", "PushDocument", "[RESPONSE_JSON_STRING_PUSH_DOCUMENT]:- " + response.Content.ToString());

                    if (response.Content.ToString() != "")
                    {
                        string strContentId = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content)["contentId"].ToString();

                        string[] arrVal = new string[4];
                        arrVal = strContentId.Split('-');

                        int intLastVal = Convert.ToInt32(arrVal[3]);

                        if (intLastVal > 0)
                        {
                            ///Update approval document push status in Log table.
                            UpdateApprovalDocPushLog(strApplicationNo, strJSON, response.Content.ToString());
                        }
                    }
                }
            }
            else
            {
                Util.LogRequestResponse("NSWSScheduler", "PushDocument", "[NO_RECORDS_FOUND]");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "NSWSScheduler");
        }
        finally
        {
            objConn.Close();
            objCommand = null;
            objdt = null;
        }
    }

    /// <summary>
    /// Update Approval Document Sent Status in the Log table
    /// </summary>
    /// <param name="strApplicationNo"></param>
    private void UpdateApprovalDocPushLog(string strApplicationNo, string strRequestJson, string strResponseJson)
    {
        try
        {
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandText = "USP_NSWS_USER_REGISTRATION";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "F");
            objCommand.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", strApplicationNo);
            objCommand.Parameters.AddWithValue("@P_VCH_REQUEST_JSON", strRequestJson);
            objCommand.Parameters.AddWithValue("@P_VCH_RESPONSE_JSON", strResponseJson);

            objCommand.Parameters.AddWithValue("P_VCH_OUT_MSG", SqlDbType.VarChar);
            objCommand.Parameters["P_VCH_OUT_MSG"].Direction = ParameterDirection.Output;

            objCommand.ExecuteNonQuery();
            string strRetrunVal = objCommand.Parameters["P_VCH_OUT_MSG"].Value.ToString();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// This method is used to push investors who registered through the NSWS portal but were unable to complete the push procedure to the DWH site.
    /// This will send a failed registered user to DWH and create a dynamic URL that will be sent to NSWS.
    /// No URL is sent to NSWS in this process.The UpdateRedirectionURL() method is used to send redirection URLs to the NSWS portal.
    /// </summary>
    public void PushInvestorToDWH()
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataTable objdt = new DataTable();

        string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString();
        if (objConn.State == ConnectionState.Closed)
        {
            objConn.Open();
        }

        try
        {
            objCommand.CommandText = "USP_NSWS_FETCH_DATA";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = objConn;
            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "E");
            objDa.SelectCommand = objCommand;
            objDa.Fill(objdt);

            if (objdt.Rows.Count > 0)
            {
                Util.LogRequestResponse("NSWSScheduler", "PushInvestorToDWH", "[RECORD_FOUND]:- " + objdt.Rows.Count.ToString());

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    string strInvUserId = Convert.ToString(objdt.Rows[i]["VCH_INV_USERID"]);
                    string strSWSId = Convert.ToString(objdt.Rows[i]["VCH_INV_SWS_ID_NSWS"]);
                    string strParentUnqId = "";
                    string strPan = Convert.ToString(objdt.Rows[i]["VCH_PAN"]);
                    int intParentId = Convert.ToInt32(objdt.Rows[i]["INT_PARENT_ID"]);

                    #region Get Parent Id for this User 
                    //*************Here Get Parent Id for this User  
                    if (intParentId > 0)
                    {
                        SqlCommand objCommandChild = new SqlCommand();
                        SqlDataAdapter objDaChild = new SqlDataAdapter();
                        DataTable objdtChild = new DataTable();

                        objCommandChild.CommandText = "USP_NSWS_FETCH_DATA";
                        objCommandChild.CommandType = CommandType.StoredProcedure;
                        objCommandChild.Connection = objConn;
                        objCommandChild.Parameters.AddWithValue("@P_VCH_ACTION", "F");
                        objCommandChild.Parameters.AddWithValue("@P_VCH_PAN", strPan);

                        objDaChild.SelectCommand = objCommandChild;
                        objDaChild.Fill(objdtChild);

                        if (objdtChild.Rows.Count > 0)
                        {
                            strParentUnqId = Convert.ToString(objdtChild.Rows[0]["VCH_UNIQUEID"]);
                        }
                        //**************************************************
                    }
                    #endregion

                    #region Push Records to DWH and Assign value DWH_Model
                    /*--------------------------------------------------------------------*/
                    ///Push Records to DWH
                    /*--------------------------------------------------------------------*/
                    /// Service Initialization
                    DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                    DWH_Model objEnt = new DWH_Model();
                    //int intStatus = 0;
                    //string OutMessage = "";
                    //object param1 = new object();
                    object param2 = new object();
                    //object param3 = new object();

                    /*--------------------------------------------------------------------*/
                    ///Assign value to property                        
                    /*--------------------------------------------------------------------*/
                    objEnt.VCHINDUSTRYNAME = Convert.ToString(objdt.Rows[i]["VCH_INDUSTRY_NAME"]);
                    objEnt.VCHEMAILID = Convert.ToString(objdt.Rows[i]["VCH_EMAIL"]);
                    objEnt.VCHMOBILENO = Convert.ToString(objdt.Rows[i]["VCH_OFF_MOBILE"]);
                    objEnt.INTSALUTATION = 0;
                    objEnt.VCHPROMOTERFNAME = Convert.ToString(objdt.Rows[i]["VCH_CONTACT_FIRSTNAME"]);
                    objEnt.VCHPROMOTERMNAME = "";
                    objEnt.VCHPROMOTERLNAME = "";
                    objEnt.VCHADDRESS = Convert.ToString(objdt.Rows[i]["VCH_ADDRESS"]);
                    objEnt.VCHUSERNAME = Convert.ToString(objdt.Rows[i]["VCH_INV_USERID"]); //// User Id Generated from GOSWIFT                       
                    objEnt.INTDISTRICT = Convert.ToInt32(objdt.Rows[i]["INT_DISTRICT"]);
                    objEnt.INTBLOCK = Convert.ToInt32(objdt.Rows[i]["INT_BLOCK"]);
                    objEnt.INTSECTOR = Convert.ToInt32(objdt.Rows[i]["INT_SECTOR"]);
                    objEnt.INTSUBSECTOR = Convert.ToInt32(objdt.Rows[i]["INT_SUBSECTOR"]);
                    objEnt.INTPARENTID = Convert.ToInt32(objdt.Rows[i]["INT_PARENT_ID"]);
                    objEnt.VCHPANNO = Convert.ToString(objdt.Rows[i]["VCH_PAN"]);
                    objEnt.VCHEINIEM = Convert.ToString(objdt.Rows[i]["VCH_EIN_IEM"]);
                    objEnt.VCHLICENCENOTYPE = Convert.ToString(objdt.Rows[i]["VCH_LICENCE_NO_TYPE"]);
                    objEnt.VCHLICENCEDOC = Convert.ToString(objdt.Rows[i]["VCH_LICENCE_DOC"]);
                    objEnt.INTUSERLEVEL = 1;
                    objEnt.VCHUSERUNIQUEID = strParentUnqId;
                    objEnt.VCHCORADDRESS = Convert.ToString(objdt.Rows[i]["VCH_SITELOCATION"]);
                    objEnt.VCHTINNO = Convert.ToString(objdt.Rows[i]["VCH_GSTIN"]);
                    objEnt.INTINDUSTRYCATEGORY = Convert.ToInt32(objdt.Rows[i]["INT_CATEGORY"]);
                    objEnt.INTAPPROVALLEVEL = 2; //// Second Level or Final Approval                         
                    objEnt.VCHINVNSWSID = Convert.ToString(objdt.Rows[i]["VCH_INV_SWS_ID_NSWS"]);  //// Investor SWS Id Received from NSWS
                    #endregion

                    #region Generate Encryption Key and DML opertion through service (DWH)
                    /*--------------------------------------------------------------------*/
                    ///Generate Encryption Key (Security key to access Data Warehouse servce methods)
                    /*--------------------------------------------------------------------*/
                    string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                    string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);
                    if (i > 0)
                    {
                        Thread.Sleep(50000); //// Wait for around 1 min
                    }

                    /*--------------------------------------------------------------------*/
                    ///DML opertion through service (DWH)
                    /*--------------------------------------------------------------------*/
                    string strReturnVal = objSrvRef.UserRegistration(objEnt, strSecurityKey);
                    Util.LogRequestResponse("NSWSScheduler", "RESPONSE_FROM_DWH", "[DWH_SERVICE_RETURN_VALUE] "+ strReturnVal);
                    if (strReturnVal != "")
                    {
                        string[] strArrRetVal = strReturnVal.Split('_');

                        if (strArrRetVal[0] == "1")//User name already exists.
                        {
                            Util.LogRequestResponse("NSWSScheduler", "RESPONSE_FROM_DWH", "[USER_NAME_ALREADY_EXISTS]");
                        }
                        else if (strArrRetVal[0] == "4") //// Successfully data pushed to DWH
                        {
                            /*--------------------------------------------------------------------*/
                            ///After successfully pushing data to DWH,Generate dynamic url using PAN and SWS Id.
                            ///Then update the redirection URL and the UNIQUE ID returned by DWH, in GOSWIFT db. 
                            ///After updating the Unique SSO Id and Redirection URL in GOSWIFT portal, Send the "Success" status to NSWS portal along with Unique/SSO Id.
                            /*--------------------------------------------------------------------*/
                            string strUniqueIdDWH = strArrRetVal[1]; ///// Unique Id or SSO Id Received from DWH

                            /*--------------------------------------------------------------------*/
                            ///Generate Dynamic Url Here
                            /*--------------------------------------------------------------------*/
                            string strGoswiftLandingPage = ConfigurationManager.AppSettings["NSWSLandingPage"].ToString();
                            string strValToEncrypt = strPan + "|" + strSWSId;

                            EncryptDecryptQueryString objEncDec = new EncryptDecryptQueryString();
                            string strEncVal = objEncDec.Encrypt(strValToEncrypt, "gR35GrvT");
                            string strGoswiftRedirectUrl = strGoswiftLandingPage + "?nparam=" + strEncVal;

                            Util.LogRequestResponse("NSWSScheduler", "UNIQUEID_REDIRECTURL", "[DATA_UPDATE_ON_GOSWIFT_DATABASE] " + strUniqueIdDWH + ": "+ strGoswiftRedirectUrl);

                            /*--------------------------------------------------------------------*/
                            ///Update Unique Id and Dynamic Redirection URL in GOSWIFT DB.
                            /*--------------------------------------------------------------------*/
                            object[] objArray1 = new object[] {
                                                                    "@P_VCH_ACTION", "UID",
                                                                    "@P_VCH_UNIQUE_ID",strUniqueIdDWH,
                                                                    "@P_VCH_INV_USER_ID",objEnt.VCHUSERNAME,
                                                                    "@P_VCH_REDIRECT_URL_NSWS",strGoswiftRedirectUrl
                                                                   };
                            string RetValue1 = SqlHelper.ExecuteNonQuery(connectionString, "USP_NSWS_USER_REGISTRATION", out param2, objArray1).ToString();
                            if (param2.ToString() == "1")
                            {
                                //intStatus = 0;
                                //OutMessage = "Oops,Some Error Occured.Please Contact Administrator.";
                                Util.LogRequestResponse("NSWSScheduler", "UNIQUE_ID_UPDATE_IN_GOSWIFT", "[SUCCESSFULLY_UPDATED_UNIQUE_ID_IN_GOSWIFT_PORTAL]");
                            }
                            else if (param2.ToString() == "2")
                            {
                                //intStatus = 0;
                                //OutMessage = "Oops,Some Error Occured.Please Contact Administrator.";
                                Util.LogRequestResponse("NSWSScheduler", "UNIQUE_ID_UPDATE_IN_GOSWIFT", "[FAILURE_TO_UPDATE_UNIQUE_ID_IN_GOSWIFT_PORTAL]");
                            }
                        }
                        else
                        {
                            Util.LogRequestResponse("NSWSScheduler", "RESPONSE_FROM_DWH", "[UNABLE_TO_PUSH_DATA_IN_DWH]:- " + strReturnVal);
                        }
                    }
                    else
                    {
                        Util.LogRequestResponse("NSWSScheduler", "RESPONSE_FROM_DWH", "[BLANK_RESPONSE_FROM_DWH]:- " + strReturnVal);
                    }
                    #endregion
                }
            }
            else
            {
                Util.LogRequestResponse("NSWSScheduler", "PushInvestorToDWH", "[NO_RECORDS_FOUND]");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "NSWSScheduler");
        }
        finally
        {
            objConn.Close();
        }
    }
}