using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Common.Persistence.Data;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
/// <summary>
/// Summary description for IncentiveMasterDataLayer
/// </summary>

namespace DataAcessLayer.Incentive
{
    public class IncentiveMasterDataLayer
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

        string ddd = System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString();

        string Str_RetValue = "";
        DataTable ObjDt = new DataTable();
        string StrSql = string.Empty;
        int int_Return_Val = 0;
        string str_Return_Val = string.Empty;
        object param = null;

        #region Created by Suman on 21-08-2017


        public DropDownList BindDropdown(DropDownList ddlDrop, IncentiveMaster objIncentive)
        {
            SqlDataReader sqlReader = null;
            IncentiveMaster objInner;
            List<IncentiveMaster> list = new List<IncentiveMaster>();
            try
            {

                object[] arr = {
                             "@P_ACTION", objIncentive.Action,
                             "@P_PARAM", objIncentive.Param,
                             "@P_PARAM_1", objIncentive.Param_1,
                             "@P_PARAM_2", objIncentive.Param_2,
                             "@P_PARAM_3", objIncentive.Param_3
                           };
                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_FILLDROPDOWN", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new IncentiveMaster();
                        objInner.ID = sqlReader["ID"].ToString();
                        objInner.Name = (sqlReader["NAME"]).ToString();
                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
                //to fill dropdown
                ddlDrop.DataTextField = "NAME";
                ddlDrop.DataValueField = "ID";
                ddlDrop.DataSource = list;
                ddlDrop.DataBind();
                ddlDrop.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objInner = null;
            }
            return ddlDrop;
        }

        /// <summary>
        /// function to show the application for the PC form (view of Incentive PC)
        /// </summary>
        /// <param name="objSearch">PcSearch object</param>
        /// <returns>datatable with all the details</returns>
        public DataSet Incentive_PcForm_Large_View(PcSearch objSearch)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet dsPcDetails = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_Large_PcFormView";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;
                objCommand.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
                objCommand.Parameters.AddWithValue("@pIntPageIndex", objSearch.intPageIndex);
                objCommand.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
                objCommand.Parameters.AddWithValue("@pIntApplicationFor", objSearch.intAppFor);
                objCommand.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
                objCommand.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);
                objDa.SelectCommand = objCommand;
                objDa.Fill(dsPcDetails);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
                objDa = null;
            }
            return dsPcDetails;
        }
        public int Incentive_PcDetails_Approve(Incentive_PCMaster objIncentive)
        {

            object[] arr = {
                               "@pChrActionCode",objIncentive.strActionCode,
                               "@pVchAppNo", objIncentive.intAppNo,
                                "@pIntCreatedBy", objIncentive.intCreatedBy,
                                "@pIntApplyFlag",objIncentive.intApplyFlag,
                               "@pIntOut", 0 };
            int_Return_Val = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_PcFormAED", out param, arr);
            return int_Return_Val;
        }
        public int Incentive_PcDetailsLarge_Approve(Incentive_PCMaster objIncentive)
        {

            object[] arr = {
                               "@pChrActionCode",objIncentive.strActionCode,
                               "@pVchAppNo", objIncentive.intAppNo,
                                "@pIntCreatedBy", objIncentive.intCreatedBy,
                                "@pIntApplyFlag",objIncentive.intApplyFlag,
                               "@pIntOut", 0 };
            int_Return_Val = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_PC_LARGE_DETAILS_AED", out param, arr);
            return int_Return_Val;
        }
        public int PcPrintDetailsLarge_AED(CertificateDetails objCertificateDetails)
        {
            int intStatus = 0;
            SqlCommand objCommand = new SqlCommand()
            {
                CommandText = "PcPrintDetails_Large_AED",
                CommandType = CommandType.StoredProcedure,
                Connection = conn
            };
            try
            {
                objCommand.Parameters.AddWithValue("@pIntAppNo", objCertificateDetails.intAppNo);
                objCommand.Parameters.AddWithValue("@pVchPlaceNew", objCertificateDetails.strPlaceNew);
                objCommand.Parameters.AddWithValue("@pVchDateNew", objCertificateDetails.strDateNew);
                objCommand.Parameters.AddWithValue("@pVchFileNew", objCertificateDetails.strFileNew);
                objCommand.Parameters.AddWithValue("@pVchActionCode", objCertificateDetails.strActionCode);
                objCommand.Parameters.AddWithValue("@pIntCreatedBy", objCertificateDetails.intCreatedBy);
                objCommand.Parameters.AddWithValue("@pVchPlaceAmd", objCertificateDetails.strPlaceAmd);
                objCommand.Parameters.AddWithValue("@pVchDateAmd", objCertificateDetails.strDateAmd);
                objCommand.Parameters.AddWithValue("@pVchFileAmd", objCertificateDetails.strFileAmd);
                objCommand.Parameters.AddWithValue("@pVchDateChangeCat", objCertificateDetails.strDateChangeCat);
                objCommand.Parameters.AddWithValue("@vchRISignature", objCertificateDetails.strIRSignature);
                objCommand.Parameters.AddWithValue("@pVchPcFilePath", objCertificateDetails.strPdfName);
                objCommand.Parameters.AddWithValue("@pvchProductAmdRemarks", objCertificateDetails.strProdEmd);
                objCommand.Parameters.AddWithValue("@pvchPlantAmdRemarks", objCertificateDetails.strPlantEmd);
                SqlParameter objParam = new SqlParameter() { ParameterName = "@pIntOut", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int, Size = 8 };
                objCommand.Parameters.Add(objParam);
                object obj = new object();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                objCommand.ExecuteNonQuery();
                obj = objParam.Value;
                if (obj != null)
                {
                    intStatus = Convert.ToInt32(obj);
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { objCommand = null; }
            return intStatus;
        }
        public string AddReason(Incentive_PCMaster objProperties)
        {
            string strQuery = string.Empty;
            try
            {
                object[] objArray = new object[] { "@pVchActionCode", objProperties.strActionCode, "@vchIndustryCode", objProperties.strIndustryCode, "@intIncentiveId", objProperties.intAppNo, "@vchReason", objProperties.IRRemark, "@vchReasonFile", objProperties.strFileSanction, "@pdtmPCDate", objProperties.dtmFFCI, "@pIntCreatedBy", objProperties.intCreatedBy, "@pIntOut", "0" };
                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_REASON_AED", out param, objArray);
            }

            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return param.ToString();
        }
        public DataSet Incentive_LateReason_View(Incentive_PCMaster objIncentive)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet dsPcDetails = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_LATEREASONVIEW";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;
                objCommand.Parameters.AddWithValue("@pChrActionCode", objIncentive.strActionCode);
                objCommand.Parameters.AddWithValue("@pIntInvestorId", objIncentive.intCreatedBy);
                objCommand.Parameters.AddWithValue("@IncentiveId", objIncentive.intGeneral);
                objDa.SelectCommand = objCommand;
                objDa.Fill(dsPcDetails);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
                objDa = null;
            }
            return dsPcDetails;
        }

        public int PC_Large_AED(Incentive_PCMaster objProperties)
        {
            int intStatus = 0;
            SqlCommand objCommand = new SqlCommand();
            try
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "USP_PC_LARGE_DETAILS_AED";
                objCommand.Connection = conn;
                objCommand.Parameters.AddWithValue("@pDecWorkingCapital", objProperties.decWorkingCapital);
                objCommand.Parameters.AddWithValue("@pVchInvestMode", objProperties.strInvestMode);
                objCommand.Parameters.AddWithValue("@pDtmFFCI", objProperties.dtmFFCI);
                objCommand.Parameters.AddWithValue("@pIntOwnerCode", objProperties.intOwnerCode);
                objCommand.Parameters.AddWithValue("@pVchOwnerName", objProperties.strOwnerName);
                objCommand.Parameters.AddWithValue("@pIntOrgType", objProperties.intOrgType);
                objCommand.Parameters.AddWithValue("@pVchOffcWebsite", objProperties.strOffcWebsite);
                objCommand.Parameters.AddWithValue("@pVchOffcEmail", objProperties.strOffcEmail);
                objCommand.Parameters.AddWithValue("@pVchOffcFaxNo", objProperties.strOffcFaxNo);
                objCommand.Parameters.AddWithValue("@pVchOffcPhNo", objProperties.strOffcPhNo);
                objCommand.Parameters.AddWithValue("@pVchOffcAddr", objProperties.strOffcAddr);
                objCommand.Parameters.AddWithValue("@pVchUnitLoc", objProperties.strUnitLoc);
                objCommand.Parameters.AddWithValue("@pVchWebsite", objProperties.strWebsite);
                objCommand.Parameters.AddWithValue("@pVchEmail", objProperties.strEmail);
                objCommand.Parameters.AddWithValue("@pVchFaxNo", objProperties.strFaxNo);
                objCommand.Parameters.AddWithValue("@pVchPhNo", objProperties.strPhNo);
                objCommand.Parameters.AddWithValue("@pVchAddr", objProperties.strAddr);
                objCommand.Parameters.AddWithValue("@pIntUnitType", objProperties.intUnitType);
                objCommand.Parameters.AddWithValue("@pIntUnitCat", objProperties.intUnitCat);
                objCommand.Parameters.AddWithValue("@pVchCompName", objProperties.strCompName);
                objCommand.Parameters.AddWithValue("@pVchUAN", objProperties.strUAN);
                objCommand.Parameters.AddWithValue("@pVchEINEMIIPMTNo", objProperties.strEINEMIIPMTNo);
                objCommand.Parameters.AddWithValue("@pVchAppNo", objProperties.intAppNo);
                objCommand.Parameters.AddWithValue("@pVchAppFor", objProperties.intAppFor);
                objCommand.Parameters.AddWithValue("@pIntDisabled", objProperties.intDisabled);
                objCommand.Parameters.AddWithValue("@pIntWomen", objProperties.intWomen);
                objCommand.Parameters.AddWithValue("@pIntManaregailSkill", objProperties.intManaregailSkill);
                objCommand.Parameters.AddWithValue("@pIntSupervisor", objProperties.intSupervisor);
                objCommand.Parameters.AddWithValue("@pIntSkilled", objProperties.intSkilled);
                objCommand.Parameters.AddWithValue("@pIntSemiSkilled", objProperties.intSemiSkilled);
                objCommand.Parameters.AddWithValue("@pIntUnskilled", objProperties.intUnskilled);
                objCommand.Parameters.AddWithValue("@pIntScTotal", objProperties.intScTotal);
                objCommand.Parameters.AddWithValue("@pIntStTotal", objProperties.intStTotal);
                objCommand.Parameters.AddWithValue("@pIntPwrReq", objProperties.intIsPwrReq);
                objCommand.Parameters.AddWithValue("@pVchChngIn", objProperties.strChngIn);
                objCommand.Parameters.AddWithValue("@pDtmProdComm", objProperties.dtmProdComm);
                objCommand.Parameters.AddWithValue("@pDecContractDemand", objProperties.decContractDemand);
                objCommand.Parameters.AddWithValue("@pVchProductName", objProperties.strProductName);
                objCommand.Parameters.AddWithValue("@pVchProductCode", objProperties.strProductCode);
                objCommand.Parameters.AddWithValue("@pIntApplyFlag", objProperties.intApplyFlag);
                objCommand.Parameters.AddWithValue("@pStrXml", objProperties.strXml);
                objCommand.Parameters.AddWithValue("@pChrActionCode", objProperties.strActionCode);
                objCommand.Parameters.AddWithValue("@pintSectorId", objProperties.intSectorId);
                objCommand.Parameters.AddWithValue("@pintSubSectorId", objProperties.intSubSectorId);
                objCommand.Parameters.AddWithValue("@pintDistrict", objProperties.intDistrict);
                objCommand.Parameters.AddWithValue("@pintBlock", objProperties.intBlock);
                objCommand.Parameters.AddWithValue("@pdecLandInvestment", objProperties.decLandInvestment);
                objCommand.Parameters.AddWithValue("@pdecBuilding", objProperties.decBuilding);
                objCommand.Parameters.AddWithValue("@pdecPlant", objProperties.decPlant);
                objCommand.Parameters.AddWithValue("@pdecOthers", objProperties.decOthers);
                objCommand.Parameters.AddWithValue("@pvchFileOwnerTypeDocument", objProperties.strFileOwnerTypeDocument);
                objCommand.Parameters.AddWithValue("@pvchFileFirstSaleBill ", objProperties.strFileFirstSaleBill);
                objCommand.Parameters.AddWithValue("@pvchFileorgTypeDocument ", objProperties.strFileorgTypeDocument);
                objCommand.Parameters.AddWithValue("@pvchFileLand", objProperties.strFileLand);
                objCommand.Parameters.AddWithValue("@pvchFilePlant", objProperties.strFilePlant);
                objCommand.Parameters.AddWithValue("@pvchFileSanction ", objProperties.strFileSanction);
                objCommand.Parameters.AddWithValue("@pvchFileProject ", objProperties.strFileProject);
                objCommand.Parameters.AddWithValue("@pvchFileClearence ", objProperties.strFileClearence);
                objCommand.Parameters.AddWithValue("@pvchFilePower  ", objProperties.strFilePower);
                objCommand.Parameters.AddWithValue("@pvchFileEmployement ", objProperties.strFileEmployement);
                objCommand.Parameters.AddWithValue("@pvchOfficeMobCode ", objProperties.strOfficeMobCode);
                objCommand.Parameters.AddWithValue("@pvchOfficeFaxCode", objProperties.strOfficeFaxCode);
                objCommand.Parameters.AddWithValue("@pvchEntMobCode", objProperties.strEntMobCode);
                objCommand.Parameters.AddWithValue("@pintGeneral", objProperties.intGeneral);
                objCommand.Parameters.AddWithValue("@pvchEntFaxCode ", objProperties.strEntFaxCode);
                objCommand.Parameters.AddWithValue("@pVchIndustryCode", objProperties.strIndustryCode);
                objCommand.Parameters.AddWithValue("@pXmlFiles", objProperties.strFileXML);
                objCommand.Parameters.AddWithValue("@pIntCreatedBy", objProperties.intCreatedBy);
                objCommand.Parameters.AddWithValue("@pVchfileproducts", objProperties.strFileProducts);
                objCommand.Parameters.AddWithValue("@pdecEquity", objProperties.decEquity);
                objCommand.Parameters.AddWithValue("@pdecLoan", objProperties.decLoan);
                objCommand.Parameters.AddWithValue("@pdecFdi", objProperties.decFdiComp);
                objCommand.Parameters.AddWithValue("@pvchFileOwnerCategory", objProperties.strFileOwnerCategory);
                objCommand.Parameters.AddWithValue("@pdtmPowerConn", objProperties.strDateConnection);
                objCommand.Parameters.AddWithValue("@pintDirectEmp", objProperties.intTotalEmployee);
                objCommand.Parameters.AddWithValue("@pintContractual", objProperties.intContractual);
                objCommand.Parameters.AddWithValue("@pVchOthers", objProperties.strOthersOrg);
                objCommand.Parameters.AddWithValue("@pIntSalutation", objProperties.intSalutation);
                objCommand.Parameters.AddWithValue("@pvchIEMFile", objProperties.strIEMFile);
                objCommand.Parameters.AddWithValue("@pvchVATFile", objProperties.VATFile);
                objCommand.Parameters.AddWithValue("@pvchBuildingFile", objProperties.strBuildFile);
                objCommand.Parameters.AddWithValue("@pvchAgreementFile", objProperties.strAgreementFile);
                objCommand.Parameters.AddWithValue("@pvchProductionFile", objProperties.strProductionFile);
                objCommand.Parameters.AddWithValue("@pvchRawMaterialPost", objProperties.strRMPostFile);
                objCommand.Parameters.AddWithValue("@pvchRawMaterialPre", objProperties.strRMPreFile);
                objCommand.Parameters.AddWithValue("@pvchRawmaterialInvoiceFile", objProperties.strRMInoviceFile);
                objCommand.Parameters.AddWithValue("@pvchFactoryLicFile", objProperties.strFactoryLicFile);
                objCommand.Parameters.AddWithValue("@vchSaleInvoiceFile", objProperties.strSaleInvoiceFile);
                objCommand.Parameters.AddWithValue("@vchInvestmentCommercialFile", objProperties.strInvestCommercialFile);
                objCommand.Parameters.AddWithValue("@pVchPCNo", objProperties.strPCNo);
                objCommand.Parameters.AddWithValue("@pDtmIssueDate", objProperties.dtmIssueDate);
                objCommand.Parameters.AddWithValue("@pDtmAmendedOn", objProperties.dtmAmendedOn);
                objCommand.Parameters.AddWithValue("@pvchGSTIN", objProperties.GSTIN);
                objCommand.Parameters.AddWithValue("@pIntChangeIn", objProperties.intChangeIn);
                objCommand.Parameters.AddWithValue("@pDtmEinIssuance", objProperties.dtmEinIssuance);
                objCommand.Parameters.AddWithValue("@pdtmIRScheduleOn", objProperties.dtmIRScheduleOn);
                objCommand.Parameters.AddWithValue("@pvchIRRemark", objProperties.IRRemark);
                objCommand.Parameters.AddWithValue("@XmlMachinery", objProperties.strXmlMachinery);
                objCommand.Parameters.AddWithValue("@intTechnical", objProperties.intTechnical);
                objCommand.Parameters.AddWithValue("@pIntInvType", objProperties.intInvType);
                objCommand.Parameters.AddWithValue("@bitPlantModified", objProperties.BitPlantModified);
                objCommand.Parameters.AddWithValue("@bitProdModified", objProperties.BitProdModified);
                objCommand.Parameters.AddWithValue("@pVchBoilerFile", objProperties.strBoilerFile);
                objCommand.Parameters.AddWithValue("@pIntApproved", objProperties.intApproved);
                objCommand.Parameters.AddWithValue("@pIntGeneratePc", objProperties.intGeneratePc);
                objCommand.Parameters.AddWithValue("@pVchPcPdfPath", objProperties.strPdfName);
                objCommand.Parameters.AddWithValue("@pIntOffline", objProperties.intOfflinePc);
                SqlParameter objParam = new SqlParameter("@pIntOut", SqlDbType.Int, 8);
                objParam.Direction = ParameterDirection.Output;
                objCommand.Parameters.Add(objParam);
                conn.Open();
                objCommand.ExecuteNonQuery();
                object obj = new object();
                obj = objParam.Value;
                if (obj != null)
                {
                    intStatus = Convert.ToInt32(obj);
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { objCommand = null; }
            return intStatus;
        }
        public int IRFormLarge_AED(IRDetails objIrDetails, Incentive_PCMaster objIncentive)
        {
            int intStatus = 0;
            SqlCommand objCommand = new SqlCommand()
            {
                CommandText = "USP_IRDetails_Large_AED",
                CommandType = CommandType.StoredProcedure,
                Connection = conn
            };
            try
            {
                objCommand.Parameters.AddWithValue("@pIntAppNo", objIrDetails.intAppNo);
                objCommand.Parameters.AddWithValue("@pdtmInspectionReport", objIrDetails.strInspectionReport);
                objCommand.Parameters.AddWithValue("@pVchControlMeasures", objIrDetails.ControlMeasures);
                objCommand.Parameters.AddWithValue("@pVchIndSafety", objIrDetails.IndSafety);
                objCommand.Parameters.AddWithValue("@pVchPowerLoad", objIrDetails.PowerLoad);
                objCommand.Parameters.AddWithValue("@pVchCppDetails", objIrDetails.CppDetails);
                objCommand.Parameters.AddWithValue("@pVchRemarks", objIrDetails.strRemarks);
                objCommand.Parameters.AddWithValue("@pVchSuggestions", objIrDetails.strSuggestions);
                objCommand.Parameters.AddWithValue("@pChrActionCode", objIncentive.strActionCode);
                objCommand.Parameters.AddWithValue("@pIntCreatedBy", objIrDetails.inCreatedBy);
                objCommand.Parameters.AddWithValue("@pStrXmlOfficer", objIrDetails.strXmlOfficer);
                objCommand.Parameters.AddWithValue("@pStrXmlProducts", objIrDetails.strXmlProducts);
                objCommand.Parameters.AddWithValue("@pStrXmlCapitalInvestment", objIrDetails.strXmlCapitalInvestment);
                objCommand.Parameters.AddWithValue("@pStrXmlTermPlan", objIrDetails.strXmlTermPlan);
                objCommand.Parameters.AddWithValue("@pStrXmlWorkingCapital", objIrDetails.strXmlWorkingCapital);
                objCommand.Parameters.AddWithValue("@pStrXmlApproval", objIrDetails.strXmlApproval);
                objCommand.Parameters.AddWithValue("@pStrXmlClearence", objIrDetails.strXmlClearence);
                objCommand.Parameters.AddWithValue("@pStrXmlProblems", objIrDetails.strXmlProblems);
                objCommand.Parameters.AddWithValue("@pStrXmlApplied", objIrDetails.strXmlApplied);
                objCommand.Parameters.AddWithValue("@pStrXmlOther", objIrDetails.strXmlOther);
                objCommand.Parameters.AddWithValue("@pVchAppFor  ", objIncentive.intAppFor);
                objCommand.Parameters.AddWithValue("@pVchEINEMIIPMTNo  ", objIncentive.strEINEMIIPMTNo);
                objCommand.Parameters.AddWithValue("@pVchCompName  ", objIncentive.strCompName);
                objCommand.Parameters.AddWithValue("@pIntUnitCat  ", objIncentive.intUnitCat);
                objCommand.Parameters.AddWithValue("@pIntUnitType  ", objIncentive.intUnitType);
                objCommand.Parameters.AddWithValue("@pVchAddr  ", objIncentive.strAddr);
                objCommand.Parameters.AddWithValue("@pVchPhNo", objIncentive.strPhNo);
                objCommand.Parameters.AddWithValue("@pVchFaxNo", objIncentive.strFaxNo);
                objCommand.Parameters.AddWithValue("@pVchEmail ", objIncentive.strEmail);
                objCommand.Parameters.AddWithValue("@pVchWebsite  ", objIncentive.strWebsite);
                objCommand.Parameters.AddWithValue("@pVchUnitLoc  ", objIncentive.strUnitLoc);
                objCommand.Parameters.AddWithValue("@pVchOffcAddr  ", objIncentive.strOffcAddr);
                objCommand.Parameters.AddWithValue("@pVchOffcPhNo", objIncentive.strOffcPhNo);
                objCommand.Parameters.AddWithValue("@pVchOffcFaxNo", objIncentive.strOffcFaxNo);
                objCommand.Parameters.AddWithValue("@pVchOffcEmail", objIncentive.strOffcEmail);
                objCommand.Parameters.AddWithValue("@pVchOffcWebsite  ", objIncentive.strOffcWebsite);
                objCommand.Parameters.AddWithValue("@pIntOrgType  ", objIncentive.intOrgType);
                objCommand.Parameters.AddWithValue("@pVchOwnerName  ", objIncentive.strOwnerName);
                objCommand.Parameters.AddWithValue("@pIntOwnerCode  ", objIncentive.intOwnerCode);
                objCommand.Parameters.AddWithValue("@pDtmFFCI", objIncentive.dtmFFCI);
                objCommand.Parameters.AddWithValue("@pIntManaregailSkill  ", objIncentive.intManaregailSkill);
                objCommand.Parameters.AddWithValue("@pIntSupervisor  ", objIncentive.intSupervisor);
                objCommand.Parameters.AddWithValue("@pIntSkilled ", objIncentive.intSkilled);
                objCommand.Parameters.AddWithValue("@pIntSemiSkilled  ", objIncentive.intSemiSkilled);
                objCommand.Parameters.AddWithValue("@pIntUnskilled ", objIncentive.intUnskilled);
                objCommand.Parameters.AddWithValue("@pIntScTotal  ", objIncentive.intScTotal);
                objCommand.Parameters.AddWithValue("@pIntStTotal ", objIncentive.intStTotal);
                objCommand.Parameters.AddWithValue("@pIntWomen ", objIncentive.intWomen);
                objCommand.Parameters.AddWithValue("@pIntDisabled ", objIncentive.intDisabled);
                objCommand.Parameters.AddWithValue("@pvchOfficeMobCode ", objIncentive.strOfficeMobCode);
                objCommand.Parameters.AddWithValue("@pvchOfficeFaxCode", objIncentive.strOfficeFaxCode);
                objCommand.Parameters.AddWithValue("@pvchEntMobCode", objIncentive.strEntMobCode);
                objCommand.Parameters.AddWithValue("@pintGeneral", objIncentive.intGeneral);
                objCommand.Parameters.AddWithValue("@pvchEntFaxCode ", objIncentive.strEntFaxCode);
                objCommand.Parameters.AddWithValue("@pVchSignature", objIrDetails.strSignature);
                objCommand.Parameters.AddWithValue("@pdtmPowerConn", objIncentive.strDateConnection);
                objCommand.Parameters.AddWithValue("@pintDirectEmp", objIncentive.intTotalEmployee);
                objCommand.Parameters.AddWithValue("@pintContractual", objIncentive.intContractual);
                objCommand.Parameters.AddWithValue("@pVchOthers", objIncentive.strOthersOrg);
                objCommand.Parameters.AddWithValue("@pIntSalutation", objIncentive.intSalutation);
                objCommand.Parameters.AddWithValue("@pDtmCommisioning", objIrDetails.strCommisioningDate);
                objCommand.Parameters.AddWithValue("@pDtmPlantInvest", objIrDetails.strPlantInvestDate);
                objCommand.Parameters.AddWithValue("@pDtmProductionComm", objIncentive.dtmProdComm);
                objCommand.Parameters.AddWithValue("@XmlMachinery", objIrDetails.strXmlMachinery);
                objCommand.Parameters.AddWithValue("@vchproductCode", objIrDetails.strProductCode);
                objCommand.Parameters.AddWithValue("@vchProductName", objIrDetails.strProductName);
                objCommand.Parameters.AddWithValue("@intCheck", objIrDetails.intCheck);
                objCommand.Parameters.AddWithValue("@intUnit", objIrDetails.intUnit);
                objCommand.Parameters.AddWithValue("@pIntDistrict", objIncentive.intDistrict);
                objCommand.Parameters.AddWithValue("@pIntBlock", objIncentive.intBlock);
                SqlParameter objParam = new SqlParameter() { ParameterName = "@pIntOut", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int, Size = 8 };
                objCommand.Parameters.Add(objParam);
                object obj = new object();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                objCommand.ExecuteNonQuery();
                obj = objParam.Value;
                if (obj != null)
                {
                    intStatus = Convert.ToInt32(obj);
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { objCommand = null; }
            return intStatus;
        }

        #endregion

        #region MyRegion Sushant

        public string Policy_Master_AED(Policy_Master_Entity objPlcEntity)
        {
            try
            {
                object[] objArray = new object[]
                {
                 "@P_CHAR_ACTION", objPlcEntity.strAction
                ,"@P_INT_PLC_ID", objPlcEntity.intPolicyId
                ,"@P_VCH_POLICY_CODE", objPlcEntity.strPolicyCode
                ,"@P_VCH_PLC_NAME", objPlcEntity.strPolicyName
                ,"@P_INT_SEC_TAG_ID", objPlcEntity.intSectorId
                ,"@P_INT_SUB_SEC_TAG_ID", objPlcEntity.intSubSectorId
                ,"@P_DTM_EFFECTIVE_DATE", objPlcEntity.strEffectiveDate
                ,"@P_VCH_AMENDMENT_DOC", objPlcEntity.strAmendmentDoc
                ,"@P_VCH_PLC_DOC", objPlcEntity.strPolicyDocs
                ,"@P_VCH_DESC", objPlcEntity.strDecription
                ,"@P_INT_PLC_CAT", objPlcEntity.intPolicyCat
                ,"@P_INT_CREATED_BY", objPlcEntity.intCreatedBy
                ,"@P_INT_PAGE_NO", objPlcEntity.intPageNo
                ,"@P_INT_PAGE_SIZE", objPlcEntity.intPageSize
                ,"@P_XML_TBL_SECTION_DETAILS", objPlcEntity.listSectionItem.SerializeToXMLString()
                ,"@P_VCH_PLC_IDS", objPlcEntity.strPlcIds
                ,"@P_OUT_MSG", "OUT"
                };

                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_POLICY_MASTER_AED", out param, objArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return param.ToString();
        }
        public DataSet Policy_Master_View(Policy_Master_Entity objPlcEntity)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_POLICY_MASTER_VIEW";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", objPlcEntity.strAction);
                objCommand.Parameters.AddWithValue("@P_INT_PLC_ID", objPlcEntity.intPolicyId);
                objCommand.Parameters.AddWithValue("@P_INT_PAGE_NO", objPlcEntity.intPageNo);
                objCommand.Parameters.AddWithValue("@P_INT_PAGE_SIZE", objPlcEntity.intPageSize);

                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
            }
            return objds;
        }

        public string Sector_Master_AED(Sector_Master_Entity objSectorEntity)
        {
            try
            {
                object[] objArray = new object[]
                {
                 "@P_CHAR_ACTION", objSectorEntity.strAction
                ,"@P_INT_PLC_ID", objSectorEntity.intPolicyId
                ,"@P_INT_SEC_TAG_ID"  ,objSectorEntity.intSecTagId
                ,"@P_INT_SECTOR_ID" ,objSectorEntity.intSectorId
                ,"@P_INT_SUB_SECTOR_ID" ,objSectorEntity.intSubSecTagId
                ,"@P_BIT_SECTORAL_POLICY"  ,objSectorEntity.bitSectoralPolicy
                ,"@P_BIT_PRIORITY_IPR"  ,objSectorEntity.bitPriorityIPR
                ,"@P_VCH_DESC"  ,objSectorEntity.strDescription
                ,"@P_INT_CREATED_By" ,objSectorEntity.intCreatedBy
                ,"@P_VCH_SEC_TAG_IDS" ,objSectorEntity.strSectorTagIds
                ,"@P_OUT_MSG" , "OUT"
                };

                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_SECTOR_MASTER_AED", out param, objArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return param.ToString();
        }
        public DataSet Sector_Master_View(Sector_Master_Entity objSectorEntity)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_SECTOR_MASTER_VIEW";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", objSectorEntity.strAction);
                objCommand.Parameters.AddWithValue("@P_INT_SEC_TAG_ID", objSectorEntity.intSecTagId);
                objCommand.Parameters.AddWithValue("@P_INT_PAGE_NO", objSectorEntity.intPageNo);
                objCommand.Parameters.AddWithValue("@P_INT_PAGE_SIZE", objSectorEntity.intPageSize);

                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
            }
            return objds;
        }

        public string OG_Master_AED(OG_Master_Entity objOGEntity)
        {
            try
            {
                object[] arr = {
                                "@P_INT_OG_ID", objOGEntity.intOGId,
                                "@P_INT_PLC_ID", objOGEntity.intPlcId,
                                "@P_VCH_OG_NAME", objOGEntity.strOGName,
                                "@P_VCH_OG_DOC", objOGEntity.strOGDoc,
                                "@P_DTM_OG_EFFC_DATE", objOGEntity.strOGEffcDate,
                                "@P_VCH_SECTION_NO", objOGEntity.strSectionNo,
                                "@P_VCH_DESC", objOGEntity.strDesc,
                                "@P_INT_CREATED_BY", objOGEntity.intCreatedBy,
                                "@P_CH_ACTION", objOGEntity.strAction,
                                "@P_VCH_OG_IDS", objOGEntity.strOGIds,
                                "@P_OUT_MSG","OUT"
                            };
                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_OG_MASTER_AED", out param, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param.ToString();
        }
        public DataSet OG_Master_View(OG_Master_Entity objOGEntity)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_OG_MASTER_VIEW";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", objOGEntity.strAction);
                objCommand.Parameters.AddWithValue("@P_INT_OG_ID", objOGEntity.intOGId);
                objCommand.Parameters.AddWithValue("@P_INT_PLC_ID", objOGEntity.intPlcId);
                objCommand.Parameters.AddWithValue("@P_INT_PAGE_NO", objOGEntity.intPageNo);
                objCommand.Parameters.AddWithValue("@P_INT_PAGE_SIZE", objOGEntity.intPageSize);

                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
            }
            return objds;
        }

        public string Inct_Name_Master_AED(Incentive_Master_Entity objInctEntity)
        {
            try
            {
                object[] arr = {
                            "@P_CH_ACTION", objInctEntity.strAction,
                            "@P_INT_INCT_ID", objInctEntity.intInctId,
                            "@P_VCH_INCT_NAME", objInctEntity.strInctName,
                            "@P_INT_OG_ID", objInctEntity.intOGId,
                            "@P_INT_DISBURSE_TYPE", objInctEntity.intDisburseType,
                            "@P_INT_AVAIL_TYPE", objInctEntity.intAvailType,
                            "@P_INT_INCT_NATURE", objInctEntity.intInctNature,
                            "@P_INT_MAX_LIMIT", objInctEntity.intMaxLimit,
                            "@P_INT_MAX_LIMIT_PRIORITY", objInctEntity.intMaxLimitPriority,
                            "@P_INT_MAX_LIMIT_PIONEER", objInctEntity.intMaxLimitPioneer,
                            "@P_INT_TIME_FRAME", objInctEntity.intTimeFrame,
                            "@P_VCH_PERIODICITY", objInctEntity.strPeriodicity,
                            "@P_INT_IS_PROVISIONAL", objInctEntity.intIsProvisional,
                            "@P_VCH_SHORT_CODE", objInctEntity.strShortCode,
                            "@P_VCH_PROV_FILE_NAME", objInctEntity.strProvFileName,
                            "@P_INT_CREATED_BY", objInctEntity.intCreatedBy,
                            "@P_VCH_INCT_IDS", objInctEntity.strInctIds,
                            "@P_OUT_MSG", "OUT"
                           };

                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_INCENTIVE_MASTER_AED", out param, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param.ToString();
        }
        public DataSet Inct_Name_Master_View(Incentive_Master_Entity objInctEntity)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_INCENTIVE_MASTER_VIEW";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_INT_OG_ID", objInctEntity.intOGId);
                objCommand.Parameters.AddWithValue("@P_INT_INCT_ID", objInctEntity.intInctId);
                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", objInctEntity.strAction);
                objCommand.Parameters.AddWithValue("@P_INT_PAGE_NO", objInctEntity.intPageNo);
                objCommand.Parameters.AddWithValue("@P_INT_PAGE_SIZE", objInctEntity.intPageSize);

                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
            }
            return objds;
        }

        public IList<Inct_Applied_With_PC_Entity> View_Inct_List_With_PC(Inct_Applied_With_PC_Entity objOGEntity)
        {
            List<Inct_Applied_With_PC_Entity> list = new List<Inct_Applied_With_PC_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Inct_Applied_With_PC_Entity objInner;

                object[] arr = {
                                "@P_INT_INVESTER_ID", objOGEntity.intInvestorId,
                                "@P_CH_ACTION", objOGEntity.strAction,
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_APPLIED_LIST_WITH_PC_VIEW", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new Inct_Applied_With_PC_Entity();
                        objInner.intInctId = Convert.ToInt32(sqlReader["intInctId"]);
                        objInner.strInctName = (sqlReader["vchInctName"]).ToString();
                        objInner.strFormId = (sqlReader["nvchFormId"]).ToString();
                        objInner.strOGName = (sqlReader["vchOGName"]).ToString();
                        objInner.strOGDoc = (sqlReader["vchOGDoc"]).ToString();
                        objInner.strPlcName = (sqlReader["vchPlcName"]).ToString();
                        objInner.strDisburseType = (sqlReader["vchDisburseType"]).ToString();
                        objInner.strAvailType = (sqlReader["vchAvailType"]).ToString();
                        objInner.strInctNature = (sqlReader["vchInctNature"]).ToString();
                        objInner.strProvFileName = (sqlReader["vchProvFileName"]).ToString();

                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        public IList<Inct_Applied_With_PC_Entity> View_Policy_List_With_PC(Inct_Applied_With_PC_Entity objOGEntity)
        {
            List<Inct_Applied_With_PC_Entity> list = new List<Inct_Applied_With_PC_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Inct_Applied_With_PC_Entity objInner;

                object[] arr = {
                                "@P_INT_INVESTER_ID", objOGEntity.intInvestorId,
                                "@P_CH_ACTION", objOGEntity.strAction,
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_APPLIED_LIST_WITH_PC_VIEW", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new Inct_Applied_With_PC_Entity();
                        objInner.strPlcName = (sqlReader["vchPlcName"]).ToString();
                        objInner.strPlcCat = (sqlReader["intPlcCat"]).ToString();

                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        public IList<Inct_Applied_With_PC_Entity> View_Summary_With_PC(Inct_Applied_With_PC_Entity objEntity)
        {
            List<Inct_Applied_With_PC_Entity> list = new List<Inct_Applied_With_PC_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Inct_Applied_With_PC_Entity objInner;

                object[] arr = {
                                "@P_CH_ACTION", objEntity.strAction,
                                "@P_INT_INVESTER_ID", objEntity.intInvestorId
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_APPLIED_LIST_WITH_PC_VIEW", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new Inct_Applied_With_PC_Entity();
                        objInner.strCompName = (sqlReader["vchCompName"]).ToString();
                        objInner.dtmFFCI = sqlReader["dtmFFCI"].ToString();
                        objInner.dtmProdComm = sqlReader["dtmProdComm"].ToString();
                        objInner.strUnitCat = (sqlReader["vchUnitCat"]).ToString();
                        objInner.strIndustryCode = (sqlReader["vchIndustryCode"]).ToString();

                        objInner.intSectorId = (sqlReader["intSectorId"]).ToString();
                        objInner.strSectorName = (sqlReader["VCH_SECTOR"]).ToString();
                        objInner.strTotCapInvest = (sqlReader["decFFCIAmt"]).ToString();
                        objInner.strPlantMachInvest = (sqlReader["decPlantMachAmt"]).ToString();
                        objInner.strDistCat = (sqlReader["chCatName"]).ToString();
                        objInner.strRating = (sqlReader["vchSubCatName"]).ToString();

                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        public IList<Inct_Basic_Unit_Details_WPC_Entity> Inct_Application_Count(Inct_Basic_Unit_Details_WPC_Entity objOGEntity)
        {
            List<Inct_Basic_Unit_Details_WPC_Entity> list = new List<Inct_Basic_Unit_Details_WPC_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Inct_Basic_Unit_Details_WPC_Entity objInner;

                object[] arr = {
                                "@P_USER_ID", objOGEntity.strUserID,
                                "@P_VCH_FILTER_MODE", objOGEntity.strFilterMode
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_APP_COUNT", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new Inct_Basic_Unit_Details_WPC_Entity();

                        objInner.intDraftCount = Convert.ToInt32(sqlReader["Drafted_App"]);
                        objInner.intApprovedCount = Convert.ToInt32(sqlReader["Approved_App"]);
                        objInner.intScrutinyCount = Convert.ToInt32(sqlReader["Scrutiny_App"]);
                        objInner.intRejectedCount = Convert.ToInt32(sqlReader["Rejected_App"]);
                        objInner.intTotalAppCount = Convert.ToInt32(sqlReader["Total_App"]);
                        objInner.intDisburseCount = Convert.ToInt32(sqlReader["Disburse_App"]);

                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }

        public IList<Inct_Drafted_Application_Entity> View_Drafted_Application(Inct_Drafted_Application_Entity objDAEntity)
        {
            List<Inct_Drafted_Application_Entity> list = new List<Inct_Drafted_Application_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Inct_Drafted_Application_Entity objInner;

                object[] arr = {
                                "@P_USER_ID", objDAEntity.strUserID,
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_DRAFTED_APPLICATION", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new Inct_Drafted_Application_Entity();

                        objInner.intInctUniqueId = Convert.ToInt32(sqlReader["INTINCUNQUEID"]);
                        objInner.intInctId = Convert.ToInt32(sqlReader["intInctId"]);
                        objInner.strInctName = sqlReader["vchInctName"].ToString();
                        objInner.dtmCreatedOn = Convert.ToDateTime(sqlReader["DTMCREATEDBY"]);
                        objInner.strFormId = sqlReader["nvchFormId"].ToString();
                        objInner.strIndustryCode = sqlReader["VCH_IND_CODE"].ToString();

                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        public string Drafted_Application_AED(Inct_Drafted_Application_Entity objDAEntity) ///// Added on Dt 02-05-2018 by Sushant Jena
        {
            try
            {
                object[] objArray = new object[]
                {
                 "@P_INT_UNIQUE_ID", objDAEntity.intInctUniqueId
                ,"@P_INT_USER_ID", objDAEntity.strUserID
                ,"@P_OUT_MSG", "OUT"
                };

                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_DRAFTED_APPLICATION_AED", out param, objArray);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return param.ToString();
        }

        public string Check_Time_Frame(Inct_Applied_With_PC_Entity objEntity)
        {
            string strQuery = string.Empty;
            try
            {
                object[] arr = {
                                 "@P_INT_USER_ID", objEntity.intInvestorId,
                                 "@P_INT_INCT_ID", objEntity.intInctId,
                                 "@P_INT_FY", objEntity.intFinancialYear,
                                 "@P_VCH_INCT_FLOW", objEntity.strInctFlow,
                                 "@P_OUT_MSG", "OUT"
                               };
                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_CHECK_TIME_FRAME", out param, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return param.ToString();
        }
        public string Validate_Inct_Apply(Validate_Inct_Apply_Entity objOGEntity)
        {
            try
            {
                object[] arr = {
                                "@P_USER_ID", objOGEntity.intUserID,
                                "@P_OUT_MSG","OUT",
                                };

                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_VALIDATE_USER_APPLY", out param, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param.ToString();
        }
        public string Basic_Unit_Details_AED(Basic_Unit_Details_Entity objOGEntity)
        {
            try
            {
                object[] arr = {
                                "@P_INT_USER_ID", objOGEntity.intCreatedBy,
                                "@P_vchEnterpriseName", objOGEntity.strEnterpriseName,
                                "@P_intOrganisationType", objOGEntity.intOrganisationType,
                                "@P_vchIndustryAddress", objOGEntity.strIndustryAddress,
                                "@P_intUnitCat", objOGEntity.intUnitCat,
                                "@P_intUnitType", objOGEntity.intUnitType,
                                "@P_vchDocCode", objOGEntity.strDocCode,
                                "@P_vchUnitTypeDoc", objOGEntity.strUnitTypeDoc,
                                "@P_intPriority", objOGEntity.intPriority,
                                "@P_intPioneer", objOGEntity.intPioneer,
                                "@P_vchPioneerCertificateDocCode", objOGEntity.strPioneerCertificateCode,
                                "@P_vchPioneerCertificate", objOGEntity.strPioneerCertificate,
                                "@P_vchRegisteredOfcAddress", objOGEntity.strRegisteredOfcAddress,
                                "@P_vchManagingPartnerGender", objOGEntity.strManagingPartnerGender,
                                "@P_vchManagingPartnerName", objOGEntity.strManagingPartnerName,
                                "@P_vchCertOfRegdDocCode", objOGEntity.strCertOfRegdDocCode,
                                "@P_vchCertOfRegdDocFileName", objOGEntity.strCertOfRegdDocFileName,
                                "@P_vchEINNO", objOGEntity.strEINNO,
                                "@P_dtmEIN", objOGEntity.dtmEIN,
                                "@P_vchPcNoBefore", objOGEntity.strPcNoBefore,
                                "@P_dtmProdCommBefore", objOGEntity.dtmProdCommBefore,
                                "@P_dtmPCIssueDateBefore", objOGEntity.dtmPCIssueDateBefore,
                                "@P_vchProdCommCertBeforeCode", objOGEntity.strProdCommCertBeforeCode,
                                "@P_vchProdCommCertBefore", objOGEntity.strProdCommCertBefore,
                                "@P_vchPcNoAfter", objOGEntity.strPcNoAfter,
                                "@P_dtmProdCommAfter", objOGEntity.dtmProdCommAfter,
                                "@P_dtmPCIssueDateAfter", objOGEntity.dtmPCIssueDateAfter,
                                "@P_vchProdCommCertAfterCode", objOGEntity.strProdCommCertAfterCode,
                                "@P_vchProdCommCertAfter", objOGEntity.strProdCommCertAfter,
                                "@P_intDistrictCode", objOGEntity.intDistrictCode,
                                "@P_intSectorId", objOGEntity.intSectorId,
                                "@P_intSubSectorId", objOGEntity.intSubSectorId,
                                "@P_vchDerivedSector", objOGEntity.strDerivedSector,
                                "@P_bitSectoralPolicy", objOGEntity.bitSectoralPolicy,
                                "@P_bitPriorityIPR", objOGEntity.bitPriorityIPR,
                                "@P_vchGSTIN", objOGEntity.strGSTIN,
                                "@P_intIsAncillary", objOGEntity.intIsAncillary,
                                "@P_vchAncillary", objOGEntity.strAncillaryFileName,
                                "@P_vchAncillaryDocCode", objOGEntity.strAncillaryDocCode,

                                "@P_intCompNature", objOGEntity.intCompNature,
                                "@P_intPriorityUser", objOGEntity.intPriorityUser,

                                "@P_dtmFFCIDateBefore", objOGEntity.dtmFFCIDateBefore,
                                "@P_vchFFCIDocBeforeCode", objOGEntity.strFFCIDocBeforeCode,
                                "@P_vchFFCIDocBefore", objOGEntity.strFFCIDocBefore,
                                "@P_decLandAmtBefore", objOGEntity.decLandAmtBefore,
                                "@P_decBuildingAmtBefore", objOGEntity.decBuildingAmtBefore,
                                "@P_decPlantMachAmtBefore", objOGEntity.decPlantMachAmtBefore,
                                "@P_decOtheFixedAssetAmtBefore", objOGEntity.decOtheFixedAssetAmtBefore,
                                "@P_decTotalAmtBefore", objOGEntity.decTotalAmtBefore,
                                "@P_vchProjectDocBeforeCode", objOGEntity.strProjectDocBeforeCode,
                                "@P_vchProjectDocBefore", objOGEntity.strProjectDocBefore,
                                "@P_dtmFFCIDateAfter", objOGEntity.dtmFFCIDateAfter,
                                "@P_vchFFCIDocAfterCode", objOGEntity.strFFCIDocAfterCode,
                                "@P_vchFFCIDocAfter", objOGEntity.strFFCIDocAfter,
                                "@P_decLandAmtAfter", objOGEntity.decLandAmtAfter,
                                "@P_decBuildingAmtAfter", objOGEntity.decBuildingAmtAfter,
                                "@P_decPlantMachAmtAfter", objOGEntity.decPlantMachAmtAfter,
                                "@P_decOtheFixedAssetAmtAfter", objOGEntity.decOtheFixedAssetAmtAfter,
                                "@P_decTotalAmtAfter", objOGEntity.decTotalAmtAfter,
                                "@P_vchProjectDocAfterCode", objOGEntity.strProjectDocAfterCode,
                                "@P_vchProjectDocAfter", objOGEntity.strProjectDocAfter,

                                "@P_intDirectEmpBefore", objOGEntity.intDirectEmpBefore,
                                "@P_intContractualEmpBefore", objOGEntity.intContractualEmpBefore,
                                "@P_vchEmpDocBefore", objOGEntity.strEmpDocBefore,
                                "@P_vchEmpDocBeforeCode", objOGEntity.strEmpDocBeforeCode,
                                "@P_intManagerialBefore", objOGEntity.intManagerialBefore,
                                "@P_intSupervisorBefore", objOGEntity.intSupervisorBefore,
                                "@P_intSkilledBefore", objOGEntity.intSkilledBefore,
                                "@P_intSemiSkilledBefore", objOGEntity.intSemiSkilledBefore,
                                "@P_intUnskilledBefore", objOGEntity.intUnskilledBefore,
                                "@P_intTotalEmpBefore", objOGEntity.intTotalEmpBefore,
                                "@P_intGeneralBefore", objOGEntity.intGeneralBefore,
                                "@P_intSCBefore", objOGEntity.intSCBefore,
                                "@P_intSTBefore", objOGEntity.intSTBefore,
                                "@P_intTotalEmpCastBefore", objOGEntity.intTotalEmpCastBefore,
                                "@P_intWomenBefore", objOGEntity.intWomenBefore,
                                "@P_intDisabledBefore", objOGEntity.intDisabledBefore,
                                "@P_intDirectEmpAfter", objOGEntity.intDirectEmpAfter,
                                "@P_intContractualEmpAfter", objOGEntity.intContractualEmpAfter,
                                "@P_vchEmpDocAfterCode", objOGEntity.strEmpDocAfterCode,
                                "@P_vchEmpDocAfter", objOGEntity.strEmpDocAfter,
                                "@P_intManagerialAfter", objOGEntity.intManagerialAfter,
                                "@P_intSupervisorAfter", objOGEntity.intSupervisorAfter,
                                "@P_intSkilledAfter", objOGEntity.intSkilledAfter,
                                "@P_intSemiSkilledAfter", objOGEntity.intSemiSkilledAfter,
                                "@P_intUnskilledAfter", objOGEntity.intUnskilledAfter,
                                "@P_intTotalEmpAfter", objOGEntity.intTotalEmpAfter,
                                "@P_intGeneralAfter", objOGEntity.intGeneralAfter,
                                "@P_intSCAfter", objOGEntity.intSCAfter,
                                "@P_intSTAfter", objOGEntity.intSTAfter,
                                "@P_intTotalEmpCastAfter", objOGEntity.intTotalEmpCastAfter,
                                "@P_intWomenAfter", objOGEntity.intWomenAfter,
                                "@P_intDisabledAfter", objOGEntity.intDisabledAfter,

                                "@P_decEquity", objOGEntity.decEquity,
                                "@P_decLoanBankFI", objOGEntity.decLoanBankFI,
                                "@P_vchTermLoanDocCode", objOGEntity.strTermLoanDocCode,
                                "@P_vchTermLoanDoc", objOGEntity.strTermLoanDoc,
                                "@P_decFDIComponet", objOGEntity.decFDIComponet,

                                "@P_XML_TBL_PROD_ITEM_BE", objOGEntity.ProductionItem_BE.SerializeToXMLString(),
                                "@P_XML_TBL_PROD_ITEM_AF", objOGEntity.ProductionItem_AF.SerializeToXMLString(),
                                "@P_XML_TBL_WORKING_LOAN", objOGEntity.WorkingCapitalLoan.SerializeToXMLString(),
                                "@P_XML_TBL_TERM_LOAN", objOGEntity.TermLoan.SerializeToXMLString(),
                                "@P_OUT_MSG", "OUT"
                                };

                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_BASIC_DETAILS_AED", out param, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param.ToString();
        }
        public DataSet Basic_Unit_Details_V(Basic_Unit_Details_Entity objEntity)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_BASIC_DETAILS_VIEW";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;
                objCommand.Parameters.AddWithValue("@P_USER_ID", objEntity.intCreatedBy);

                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
            }
            return objds;
        }
        public DataSet dynamic_name_doc_bind()
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_DOC_BIND";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
            }
            return objds;
        }
        public DataSet Bind_Inct_With_Eligible(Basic_Unit_Details_Entity objEntity)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_CHECK_ELIGIBILITY";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_INT_USER_ID", objEntity.intCreatedBy);
                objCommand.Parameters.AddWithValue("@P_VCH_INCT_FLOW", objEntity.strInctFlow);

                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
            }
            return objds;
        }
        public RadioButtonList BindRadioButton(RadioButtonList Rad_Btn, IncentiveMaster objIncentive)
        {
            SqlDataReader sqlReader = null;
            IncentiveMaster objInner;
            List<IncentiveMaster> list = new List<IncentiveMaster>();
            try
            {

                object[] arr = {
                                "@P_ACTION", objIncentive.Action,
                                "@P_PARAM", objIncentive.Param,
                                "@P_PARAM_1", objIncentive.Param_1,
                                "@P_PARAM_2", objIncentive.Param_2,
                                "@P_PARAM_3", objIncentive.Param_3
                                };
                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_FILLDROPDOWN", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new IncentiveMaster();
                        objInner.ID = sqlReader["ID"].ToString();
                        objInner.Name = (sqlReader["NAME"]).ToString();
                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
                //to fill RadioButton
                Rad_Btn.DataTextField = "NAME";
                Rad_Btn.DataValueField = "ID";
                Rad_Btn.DataSource = list;
                Rad_Btn.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objInner = null;
            }
            return Rad_Btn;
        }
        public DataSet BindDerivedSector(IncentiveMaster objEntity)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_FillDropDown";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;
                objCommand.Parameters.AddWithValue("@P_ACTION", objEntity.Action);
                objCommand.Parameters.AddWithValue("@P_PARAM_2", objEntity.Param_2);
                objCommand.Parameters.AddWithValue("@P_PARAM_3", objEntity.Param_3);

                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
            }
            return objds;
        }
        public DataTable ValidateCertification(Inct_Applied_With_PC_Entity objEntity)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataTable objdt = new DataTable();
            try
            {
                objCommand.CommandText = "USP_INCT_VALIDATE_CERTIFICATION";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_INCT_ID", objEntity.intInctId);
                objCommand.Parameters.AddWithValue("@P_USER_ID", objEntity.intInvestorId);

                objDa.SelectCommand = objCommand;
                objDa.Fill(objdt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
            }
            return objdt;
        }

        public string Inct_EC_Delay_Reason_AED(Inct_EC_Delay_Reason_Entity objEntity)
        {
            try
            {
                object[] arr = {
                                "@P_VCH_INDUSTRY_CODE", objEntity.strIndustryCode,
                                "@P_VCH_ENTERPRISE_NAME", objEntity.strEnterpriseName,
                                "@P_INT_UNIT_CAT", objEntity.intUnitCat,
                                "@P_INT_UNIT_TYPE",objEntity.intUnitType,
                                "@P_VCH_FFCI_DATE",objEntity.strFFCIDate,
                                "@P_VCH_PROD_COMM_DATE",objEntity.strProdCommDate,
                                "@P_VCH_REASON", objEntity.strDelayReason,
                                "@P_INT_USER_ID", objEntity.intCreatedBy,
                                "@P_XML_TBL_EC_DELAY_DOC", objEntity.ECDelayDoc.SerializeToXMLString(),
                                "@P_OUT_MSG", "OUT"
                                 };

                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_EC_DELAY_AED", out param, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param.ToString();
        }
        public DataSet Inct_EC_Delay_Reason_VIEW(Inct_EC_Delay_Reason_Entity objEntity)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_EC_DELAY_VIEW";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;
                objCommand.Parameters.AddWithValue("@P_INT_USER_ID", objEntity.intCreatedBy);
                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", objEntity.strAction);
                objCommand.Parameters.AddWithValue("@P_INT_DELAY_ID", objEntity.INT_DELAY_ID);
                objCommand.Parameters.AddWithValue("@P_INT_STATUS", objEntity.intStatus);
                objCommand.Parameters.AddWithValue("@P_VCH_ENT_NAME", objEntity.strEnterpriseName);

                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
            }
            return objds;
        }

        /// <summary>
        /// For Delay Reason Apporval----ApproveDelayReason.aspx
        /// </summary>
        /// Date 19.12.2017
        /// <param name="objDAEntity"></param>
        /// <returns></returns>
        public string DelayReason_Approval(Inct_EC_Delay_Reason_Entity objDAEntity)
        {
            object[] arr = {
                            "@P_VCH_ACTION",objDAEntity.strAction,
                            "@P_intDelayId", objDAEntity.INT_DELAY_ID,
                            "@P_vchRemark", objDAEntity.vchRemark,
                            "@P_intStatus", objDAEntity.intStatus ,
                            "@P_intTimeAllowed", objDAEntity.intTimeAllowed ,
                            "@P_vchECLetter", objDAEntity.vchECLetter ,
                            "@P_intCreatedBy", objDAEntity.intCreatedBy
                            };
            int_Return_Val = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_DELAY_REASON_APPROVE", out param, arr);
            return param.ToString();
        }

        #endregion

        #region Ritika

        public DataSet BindDropdown(string strActionCode)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_FillDropDown";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;
                objCommand.Parameters.AddWithValue("@P_ACTION", strActionCode);
                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { objCommand = null; }
            return objds;
        }

        /// <summary>
        /// Function to insert details for the PC Form
        /// </summary>
        /// <param name="objProperties">Incentive_PCMaster object</param>
        /// <returns>int - status as to whether record was updated or not</returns>


        /// <summary>
        /// function to show the application for the PC form (view of Incentive PC)
        /// </summary>
        /// <param name="objSearch">PcSearch object</param>
        /// <returns>datatable with all the details</returns>
        public DataSet Incentive_PcForm_View(PcSearch objSearch)
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet dsPcDetails = new DataSet();
            try
            {
                objCommand.CommandText = "USP_INCT_PcFormView";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;
                objCommand.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
                objCommand.Parameters.AddWithValue("@pIntPageIndex", objSearch.intPageIndex);
                objCommand.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
                objCommand.Parameters.AddWithValue("@pIntApplicationFor", objSearch.intAppFor);
                objCommand.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
                objCommand.Parameters.AddWithValue("@pIntAppNo", objSearch.vchAppNos);
                objCommand.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);
                objCommand.Parameters.AddWithValue("@pStrUnitName", objSearch.strUnitName);
                objCommand.Parameters.AddWithValue("@pDistId", objSearch.intDistId);
                objCommand.Parameters.AddWithValue("@pUserId", objSearch.UserId);
                objDa.SelectCommand = objCommand;
                objDa.Fill(dsPcDetails);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCommand = null;
                objDa = null;
            }
            return dsPcDetails;
        }


        /// <summary>
        /// Function to get the details of all the PC applied
        /// </summary>
        /// <param name="objSearch">PcSearch</param>
        /// <returns>List of object PcApplied</returns>
        public List<PcApplied> ViewPcAppliedDetails(PcSearch objSearch)
        {
            List<PcApplied> lstPcApplied = new List<PcApplied>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                PcApplied objInner;
                object[] arr = { "@pChrActionCode", objSearch.strActionCode
                                   , "@pIntPageIndex", objSearch.intPageIndex
                                   , "@pIntPageSize", objSearch.intPageSize
                                   , "@pIntApplicationFor", objSearch.intAppFor
                                   , "@pIntAppNo", objSearch.intAppNo};
                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_PcFormView", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new PcApplied();
                        objInner.rowcnt = Convert.ToInt32(sqlReader["rowcnt"]);
                        objInner.requestType = sqlReader["requestType"].ToString();
                        objInner.intAppNo = Convert.ToInt32(sqlReader["intAppNo"]);
                        objInner.vchAppNo = sqlReader["vchAppNo"].ToString();
                        objInner.strCompName = (sqlReader["vchCompName"]).ToString();
                        objInner.strPhNo = sqlReader["intApplyFlag"].ToString();
                        objInner.strUnitCategory = sqlReader["unitCategory"].ToString();
                        objInner.strOrganizationType = sqlReader["organizationType"].ToString();
                        objInner.intApproved = Convert.ToInt32(sqlReader["intApproved"]);
                        objInner.strApplied = sqlReader["strApplied"].ToString();
                        lstPcApplied.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstPcApplied;
        }

        public int PcPrintDetails_AED(CertificateDetails objCertificateDetails)
        {
            int intStatus = 0;
            SqlCommand objCommand = new SqlCommand()
            {
                CommandText = "PcPrintDetails_AED",
                CommandType = CommandType.StoredProcedure,
                Connection = conn
            };
            try
            {
                objCommand.Parameters.AddWithValue("@pIntAppNo", objCertificateDetails.intAppNo);
                objCommand.Parameters.AddWithValue("@pVchPlaceNew", objCertificateDetails.strPlaceNew);
                objCommand.Parameters.AddWithValue("@pVchDateNew", objCertificateDetails.strDateNew);
                objCommand.Parameters.AddWithValue("@pVchFileNew", objCertificateDetails.strFileNew);
                objCommand.Parameters.AddWithValue("@pVchActionCode", objCertificateDetails.strActionCode);
                objCommand.Parameters.AddWithValue("@pIntCreatedBy", objCertificateDetails.intCreatedBy);
                objCommand.Parameters.AddWithValue("@pVchPlaceAmd", objCertificateDetails.strPlaceAmd);
                objCommand.Parameters.AddWithValue("@pVchDateAmd", objCertificateDetails.strDateAmd);
                objCommand.Parameters.AddWithValue("@pVchFileAmd", objCertificateDetails.strFileAmd);
                objCommand.Parameters.AddWithValue("@pVchDateChangeCat", objCertificateDetails.strDateChangeCat);
                objCommand.Parameters.AddWithValue("@vchRISignature", objCertificateDetails.strIRSignature);
                objCommand.Parameters.AddWithValue("@pVchPcFilePath", objCertificateDetails.strPdfName);
                objCommand.Parameters.AddWithValue("@pvchProductAmdRemarks", objCertificateDetails.strProdEmd);
                objCommand.Parameters.AddWithValue("@pvchPlantAmdRemarks", objCertificateDetails.strPlantEmd);
                SqlParameter objParam = new SqlParameter() { ParameterName = "@pIntOut", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int, Size = 8 };
                objCommand.Parameters.Add(objParam);
                object obj = new object();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                objCommand.ExecuteNonQuery();
                obj = objParam.Value;
                if (obj != null)
                {
                    intStatus = Convert.ToInt32(obj);
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { objCommand = null; }
            return intStatus;
        }

        /// <summary>
        /// Insert ir details
        /// </summary>
        /// <param name="objIrDetails">IRDetails object</param>
        /// <param name="objIncentive">Incentive_PCMaster object</param>
        /// <returns>status</returns>
        public int IRForm_AED(IRDetails objIrDetails, Incentive_PCMaster objIncentive)
        {
            int intStatus = 0;
            SqlCommand objCommand = new SqlCommand()
            {
                CommandText = "USP_IRDetails_AED",
                CommandType = CommandType.StoredProcedure,
                Connection = conn
            };
            try
            {
                objCommand.Parameters.AddWithValue("@pIntAppNo", objIrDetails.intAppNo);
                objCommand.Parameters.AddWithValue("@pdtmInspectionReport", objIrDetails.strInspectionReport);
                objCommand.Parameters.AddWithValue("@pVchControlMeasures", objIrDetails.ControlMeasures);
                objCommand.Parameters.AddWithValue("@pVchIndSafety", objIrDetails.IndSafety);
                objCommand.Parameters.AddWithValue("@pVchPowerLoad", objIrDetails.PowerLoad);
                objCommand.Parameters.AddWithValue("@pVchCppDetails", objIrDetails.CppDetails);
                objCommand.Parameters.AddWithValue("@pVchRemarks", objIrDetails.strRemarks);
                objCommand.Parameters.AddWithValue("@pVchSuggestions", objIrDetails.strSuggestions);
                objCommand.Parameters.AddWithValue("@pChrActionCode", objIncentive.strActionCode);
                objCommand.Parameters.AddWithValue("@pIntCreatedBy", objIrDetails.inCreatedBy);
                objCommand.Parameters.AddWithValue("@pStrXmlOfficer", objIrDetails.strXmlOfficer);
                objCommand.Parameters.AddWithValue("@pStrXmlProducts", objIrDetails.strXmlProducts);
                objCommand.Parameters.AddWithValue("@pStrXmlCapitalInvestment", objIrDetails.strXmlCapitalInvestment);
                objCommand.Parameters.AddWithValue("@pStrXmlTermPlan", objIrDetails.strXmlTermPlan);
                objCommand.Parameters.AddWithValue("@pStrXmlWorkingCapital", objIrDetails.strXmlWorkingCapital);
                objCommand.Parameters.AddWithValue("@pStrXmlApproval", objIrDetails.strXmlApproval);
                objCommand.Parameters.AddWithValue("@pStrXmlClearence", objIrDetails.strXmlClearence);
                objCommand.Parameters.AddWithValue("@pStrXmlProblems", objIrDetails.strXmlProblems);
                objCommand.Parameters.AddWithValue("@pStrXmlApplied", objIrDetails.strXmlApplied);
                objCommand.Parameters.AddWithValue("@pStrXmlOther", objIrDetails.strXmlOther);
                objCommand.Parameters.AddWithValue("@pVchAppFor  ", objIncentive.intAppFor);
                objCommand.Parameters.AddWithValue("@pVchEINEMIIPMTNo  ", objIncentive.strEINEMIIPMTNo);
                objCommand.Parameters.AddWithValue("@pVchCompName  ", objIncentive.strCompName);
                objCommand.Parameters.AddWithValue("@pIntUnitCat  ", objIncentive.intUnitCat);
                objCommand.Parameters.AddWithValue("@pIntUnitType  ", objIncentive.intUnitType);
                objCommand.Parameters.AddWithValue("@pVchAddr  ", objIncentive.strAddr);
                objCommand.Parameters.AddWithValue("@pVchPhNo", objIncentive.strPhNo);
                objCommand.Parameters.AddWithValue("@pVchFaxNo", objIncentive.strFaxNo);
                objCommand.Parameters.AddWithValue("@pVchEmail ", objIncentive.strEmail);
                objCommand.Parameters.AddWithValue("@pVchWebsite  ", objIncentive.strWebsite);
                objCommand.Parameters.AddWithValue("@pVchUnitLoc  ", objIncentive.strUnitLoc);
                objCommand.Parameters.AddWithValue("@pVchOffcAddr  ", objIncentive.strOffcAddr);
                objCommand.Parameters.AddWithValue("@pVchOffcPhNo", objIncentive.strOffcPhNo);
                objCommand.Parameters.AddWithValue("@pVchOffcFaxNo", objIncentive.strOffcFaxNo);
                objCommand.Parameters.AddWithValue("@pVchOffcEmail", objIncentive.strOffcEmail);
                objCommand.Parameters.AddWithValue("@pVchOffcWebsite  ", objIncentive.strOffcWebsite);
                objCommand.Parameters.AddWithValue("@pIntOrgType  ", objIncentive.intOrgType);
                objCommand.Parameters.AddWithValue("@pVchOwnerName  ", objIncentive.strOwnerName);
                objCommand.Parameters.AddWithValue("@pIntOwnerCode  ", objIncentive.intOwnerCode);
                objCommand.Parameters.AddWithValue("@pDtmFFCI", objIncentive.dtmFFCI);
                objCommand.Parameters.AddWithValue("@pIntManaregailSkill  ", objIncentive.intManaregailSkill);
                objCommand.Parameters.AddWithValue("@pIntSupervisor  ", objIncentive.intSupervisor);
                objCommand.Parameters.AddWithValue("@pIntSkilled ", objIncentive.intSkilled);
                objCommand.Parameters.AddWithValue("@pIntSemiSkilled  ", objIncentive.intSemiSkilled);
                objCommand.Parameters.AddWithValue("@pIntUnskilled ", objIncentive.intUnskilled);
                objCommand.Parameters.AddWithValue("@pIntScTotal  ", objIncentive.intScTotal);
                objCommand.Parameters.AddWithValue("@pIntStTotal ", objIncentive.intStTotal);
                objCommand.Parameters.AddWithValue("@pIntWomen ", objIncentive.intWomen);
                objCommand.Parameters.AddWithValue("@pIntDisabled ", objIncentive.intDisabled);
                objCommand.Parameters.AddWithValue("@pvchOfficeMobCode ", objIncentive.strOfficeMobCode);
                objCommand.Parameters.AddWithValue("@pvchOfficeFaxCode", objIncentive.strOfficeFaxCode);
                objCommand.Parameters.AddWithValue("@pvchEntMobCode", objIncentive.strEntMobCode);
                objCommand.Parameters.AddWithValue("@pintGeneral", objIncentive.intGeneral);
                objCommand.Parameters.AddWithValue("@pvchEntFaxCode ", objIncentive.strEntFaxCode);
                objCommand.Parameters.AddWithValue("@pVchSignature", objIrDetails.strSignature);
                objCommand.Parameters.AddWithValue("@pdtmPowerConn", objIncentive.strDateConnection);
                objCommand.Parameters.AddWithValue("@pintDirectEmp", objIncentive.intTotalEmployee);
                objCommand.Parameters.AddWithValue("@pintContractual", objIncentive.intContractual);
                objCommand.Parameters.AddWithValue("@pVchOthers", objIncentive.strOthersOrg);
                objCommand.Parameters.AddWithValue("@pIntSalutation", objIncentive.intSalutation);
                objCommand.Parameters.AddWithValue("@pDtmCommisioning", objIrDetails.strCommisioningDate);
                objCommand.Parameters.AddWithValue("@pDtmPlantInvest", objIrDetails.strPlantInvestDate);
                objCommand.Parameters.AddWithValue("@XmlMachinery", objIrDetails.strXmlMachinery);
                objCommand.Parameters.AddWithValue("@vchproductCode", objIrDetails.strProductCode);
                objCommand.Parameters.AddWithValue("@vchProductName", objIrDetails.strProductName);
                objCommand.Parameters.AddWithValue("@intCheck", objIrDetails.intCheck);
                objCommand.Parameters.AddWithValue("@intUnit", objIrDetails.intUnit);
                objCommand.Parameters.AddWithValue("@pIntDistrict", objIncentive.intDistrict);
                objCommand.Parameters.AddWithValue("@pIntBlock", objIncentive.intBlock);
                SqlParameter objParam = new SqlParameter() { ParameterName = "@pIntOut", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int, Size = 8 };
                objCommand.Parameters.Add(objParam);
                object obj = new object();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                objCommand.ExecuteNonQuery();
                obj = objParam.Value;
                if (obj != null)
                {
                    intStatus = Convert.ToInt32(obj);
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { objCommand = null; }
            return intStatus;
        }

        /// <summary>
        /// Function to insert details for the PC Form
        /// </summary>
        /// <param name="objProperties">Incentive_PCMaster object</param>
        /// <returns>int - status as to whether record was updated or not</returns>
        public int Incentive_PcDetails_AED(Incentive_PCMaster objProperties)
        {
            int intStatus = 0;
            SqlCommand objCommand = new SqlCommand();
            try
            {
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "USP_INCT_PcFormAED";
                objCommand.Connection = conn;
                objCommand.Parameters.AddWithValue("@pDecWorkingCapital", objProperties.decWorkingCapital);
                objCommand.Parameters.AddWithValue("@pVchInvestMode", objProperties.strInvestMode);
                objCommand.Parameters.AddWithValue("@pDtmFFCI", objProperties.dtmFFCI);
                objCommand.Parameters.AddWithValue("@pIntOwnerCode", objProperties.intOwnerCode);
                objCommand.Parameters.AddWithValue("@pVchOwnerName", objProperties.strOwnerName);
                objCommand.Parameters.AddWithValue("@pIntOrgType", objProperties.intOrgType);
                objCommand.Parameters.AddWithValue("@pVchOffcWebsite", objProperties.strOffcWebsite);
                objCommand.Parameters.AddWithValue("@pVchOffcEmail", objProperties.strOffcEmail);
                objCommand.Parameters.AddWithValue("@pVchOffcFaxNo", objProperties.strOffcFaxNo);
                objCommand.Parameters.AddWithValue("@pVchOffcPhNo", objProperties.strOffcPhNo);
                objCommand.Parameters.AddWithValue("@pVchOffcAddr", objProperties.strOffcAddr);
                objCommand.Parameters.AddWithValue("@pVchUnitLoc", objProperties.strUnitLoc);
                objCommand.Parameters.AddWithValue("@pVchWebsite", objProperties.strWebsite);
                objCommand.Parameters.AddWithValue("@pVchEmail", objProperties.strEmail);
                objCommand.Parameters.AddWithValue("@pVchFaxNo", objProperties.strFaxNo);
                objCommand.Parameters.AddWithValue("@pVchPhNo", objProperties.strPhNo);
                objCommand.Parameters.AddWithValue("@pVchAddr", objProperties.strAddr);
                objCommand.Parameters.AddWithValue("@pIntUnitType", objProperties.intUnitType);
                objCommand.Parameters.AddWithValue("@pIntUnitCat", objProperties.intUnitCat);
                objCommand.Parameters.AddWithValue("@pVchCompName", objProperties.strCompName);
                objCommand.Parameters.AddWithValue("@pVchUAN", objProperties.strUAN);
                objCommand.Parameters.AddWithValue("@pVchEINEMIIPMTNo", objProperties.strEINEMIIPMTNo);
                objCommand.Parameters.AddWithValue("@pVchAppNo", objProperties.intAppNo);
                objCommand.Parameters.AddWithValue("@pVchAppFor", objProperties.intAppFor);
                objCommand.Parameters.AddWithValue("@pIntDisabled", objProperties.intDisabled);
                objCommand.Parameters.AddWithValue("@pIntWomen", objProperties.intWomen);
                objCommand.Parameters.AddWithValue("@pIntManaregailSkill", objProperties.intManaregailSkill);
                objCommand.Parameters.AddWithValue("@pIntSupervisor", objProperties.intSupervisor);
                objCommand.Parameters.AddWithValue("@pIntSkilled", objProperties.intSkilled);
                objCommand.Parameters.AddWithValue("@pIntSemiSkilled", objProperties.intSemiSkilled);
                objCommand.Parameters.AddWithValue("@pIntUnskilled", objProperties.intUnskilled);
                objCommand.Parameters.AddWithValue("@pIntScTotal", objProperties.intScTotal);
                objCommand.Parameters.AddWithValue("@pIntStTotal", objProperties.intStTotal);
                objCommand.Parameters.AddWithValue("@pIntPwrReq", objProperties.intIsPwrReq);
                objCommand.Parameters.AddWithValue("@pVchChngIn", objProperties.strChngIn);
                objCommand.Parameters.AddWithValue("@pDtmProdComm", objProperties.dtmProdComm);
                objCommand.Parameters.AddWithValue("@pDecContractDemand", objProperties.decContractDemand);
                objCommand.Parameters.AddWithValue("@pVchProductName", objProperties.strProductName);
                objCommand.Parameters.AddWithValue("@pVchProductCode", objProperties.strProductCode);
                objCommand.Parameters.AddWithValue("@pIntApplyFlag", objProperties.intApplyFlag);
                objCommand.Parameters.AddWithValue("@pStrXml", objProperties.strXml);
                objCommand.Parameters.AddWithValue("@pChrActionCode", objProperties.strActionCode);
                objCommand.Parameters.AddWithValue("@pintSectorId", objProperties.intSectorId);
                objCommand.Parameters.AddWithValue("@pintSubSectorId", objProperties.intSubSectorId);
                objCommand.Parameters.AddWithValue("@pintDistrict", objProperties.intDistrict);
                objCommand.Parameters.AddWithValue("@pintBlock", objProperties.intBlock);
                objCommand.Parameters.AddWithValue("@pdecLandInvestment", objProperties.decLandInvestment);
                objCommand.Parameters.AddWithValue("@pdecBuilding", objProperties.decBuilding);
                objCommand.Parameters.AddWithValue("@pdecPlant", objProperties.decPlant);
                objCommand.Parameters.AddWithValue("@pdecOthers", objProperties.decOthers);
                objCommand.Parameters.AddWithValue("@pvchFileOwnerTypeDocument", objProperties.strFileOwnerTypeDocument);
                objCommand.Parameters.AddWithValue("@pvchFileFirstSaleBill ", objProperties.strFileFirstSaleBill);
                objCommand.Parameters.AddWithValue("@pvchFileorgTypeDocument ", objProperties.strFileorgTypeDocument);
                objCommand.Parameters.AddWithValue("@pvchFileLand", objProperties.strFileLand);
                objCommand.Parameters.AddWithValue("@pvchFilePlant", objProperties.strFilePlant);
                objCommand.Parameters.AddWithValue("@pvchFileSanction ", objProperties.strFileSanction);
                objCommand.Parameters.AddWithValue("@pvchFileProject ", objProperties.strFileProject);
                objCommand.Parameters.AddWithValue("@pvchFileClearence ", objProperties.strFileClearence);
                objCommand.Parameters.AddWithValue("@pvchFilePower  ", objProperties.strFilePower);
                objCommand.Parameters.AddWithValue("@pvchFileEmployement ", objProperties.strFileEmployement);
                objCommand.Parameters.AddWithValue("@pvchOfficeMobCode ", objProperties.strOfficeMobCode);
                objCommand.Parameters.AddWithValue("@pvchOfficeFaxCode", objProperties.strOfficeFaxCode);
                objCommand.Parameters.AddWithValue("@pvchEntMobCode", objProperties.strEntMobCode);
                objCommand.Parameters.AddWithValue("@pintGeneral", objProperties.intGeneral);
                objCommand.Parameters.AddWithValue("@pvchEntFaxCode ", objProperties.strEntFaxCode);
                objCommand.Parameters.AddWithValue("@pVchIndustryCode", objProperties.strIndustryCode);
                objCommand.Parameters.AddWithValue("@pXmlFiles", objProperties.strFileXML);
                objCommand.Parameters.AddWithValue("@pIntCreatedBy", objProperties.intCreatedBy);
                objCommand.Parameters.AddWithValue("@pVchfileproducts", objProperties.strFileProducts);
                objCommand.Parameters.AddWithValue("@pdecEquity", objProperties.decEquity);
                objCommand.Parameters.AddWithValue("@pdecLoan", objProperties.decLoan);
                objCommand.Parameters.AddWithValue("@pdecFdi", objProperties.decFdiComp);
                objCommand.Parameters.AddWithValue("@pvchFileOwnerCategory", objProperties.strFileOwnerCategory);
                objCommand.Parameters.AddWithValue("@pdtmPowerConn", objProperties.strDateConnection);
                objCommand.Parameters.AddWithValue("@pintDirectEmp", objProperties.intTotalEmployee);
                objCommand.Parameters.AddWithValue("@pintContractual", objProperties.intContractual);
                objCommand.Parameters.AddWithValue("@pVchOthers", objProperties.strOthersOrg);
                objCommand.Parameters.AddWithValue("@pIntSalutation", objProperties.intSalutation);
                objCommand.Parameters.AddWithValue("@pDtmIssueDate", objProperties.dtmIssueDate);
                objCommand.Parameters.AddWithValue("@pDtmAmendedOn", objProperties.dtmAmendedOn);
                objCommand.Parameters.AddWithValue("@pvchGSTIN", objProperties.GSTIN);
                objCommand.Parameters.AddWithValue("@pIntChangeIn", objProperties.intChangeIn);
                objCommand.Parameters.AddWithValue("@pDtmEinIssuance", objProperties.dtmEinIssuance);
                objCommand.Parameters.AddWithValue("@pdtmIRScheduleOn", objProperties.dtmIRScheduleOn);
                objCommand.Parameters.AddWithValue("@pvchIRRemark", objProperties.IRRemark);
                objCommand.Parameters.AddWithValue("@XmlMachinery", objProperties.strXmlMachinery);
                objCommand.Parameters.AddWithValue("@intTechnical", objProperties.intTechnical);
                objCommand.Parameters.AddWithValue("@pIntInvType", objProperties.intInvType);
                objCommand.Parameters.AddWithValue("@bitPlantModified", objProperties.BitPlantModified);
                objCommand.Parameters.AddWithValue("@bitProdModified", objProperties.BitProdModified);
                objCommand.Parameters.AddWithValue("@pVchBoilerFile", objProperties.strBoilerFile);
                objCommand.Parameters.AddWithValue("@pIntApproved", objProperties.intApproved);
                objCommand.Parameters.AddWithValue("@pIntGeneratePc", objProperties.intGeneratePc);
                objCommand.Parameters.AddWithValue("@pVchPcPdfPath", objProperties.strPdfName);
                objCommand.Parameters.AddWithValue("@pVchPCNo", objProperties.strPCNo);
                objCommand.Parameters.AddWithValue("@pIntOffline", objProperties.intOfflinePc);
                SqlParameter objParam = new SqlParameter("@pIntOut", SqlDbType.Int, 8)

                {
                    Direction = ParameterDirection.Output
                };
                objCommand.Parameters.Add(objParam);
                conn.Open();
                objCommand.ExecuteNonQuery();
                object obj = new object();
                obj = objParam.Value;
                if (obj != null)
                {
                    intStatus = Convert.ToInt32(obj);
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { objCommand = null; }
            return intStatus;
        }

        /// <summary>
        /// Function to get the OSPCB certificate details for the investor when applying for PC
        /// </summary>
        /// <param name="objSearch">InctSearch object</param>
        /// <returns>List of string type</returns>
        public List<string> ViewInctOSPCBDetails(InctSearch objSearch)
        {
            List<string> lstFiles = new List<string>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                object[] arr = {
                                  "@pIntInvestorId",objSearch.intUserUnitType
                                , "@pChrActionCode", objSearch.strActionCode
                                , "@pIntDocumentType", objSearch.intUnitType
                               };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_OSPCB_Details", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        lstFiles.Add(sqlReader["VCH_CERTIFICATE_FILENAME"].ToString());
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstFiles;
        }

        #endregion

        #region gouri

        public IList<Inct_Application_Details_Entity> View_Application_ApprveFetch(Inct_Application_Details_Entity objDAEntity)
        {
            List<Inct_Application_Details_Entity> list = new List<Inct_Application_Details_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Inct_Application_Details_Entity objInner;

                object[] arr = {
                                "@P_VCH_ACTION",objDAEntity.strAction,
                                "@P_INTINCUNQUEID",objDAEntity.INTINCUNQUEID
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_APPLICATION_APPROVE_FETCH", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new Inct_Application_Details_Entity();

                        objInner.strAppNo = sqlReader["ApplicationNum"].ToString();
                        objInner.strUnitName = sqlReader["vchCompName"].ToString();
                        objInner.strInctName = sqlReader["vchInctName"].ToString();
                        objInner.VchInctNum = sqlReader["VCHINCENTIVENO"].ToString();
                        objInner.dtmCreatedOn = Convert.ToDateTime(sqlReader["DTMCREATEDBY"]);
                        objInner.strStatus = sqlReader["vchStatus"].ToString();
                        objInner.intStatus = Convert.ToInt16(sqlReader["INTSTATUS"].ToString());
                        objInner.strApplicationNum = sqlReader["ApplicationNum"].ToString();
                        objInner.strFormPreviewId = sqlReader["nvchFormPreviewId"].ToString();
                        objInner.strSanFileName = sqlReader["vchSanFileName"].ToString();

                        objInner.INTINCUNQUEID = Convert.ToInt16(sqlReader["INTINCUNQUEID"].ToString());
                        objInner.IsProvisional = Convert.ToInt16(sqlReader["IsProvisional"].ToString());
                        objInner.intAvailType = Convert.ToInt16(sqlReader["intAvailType"].ToString());
                        objInner.DocName = sqlReader["DocName"].ToString();
                        objInner.EMAILId = sqlReader["VCH_EMAIL"].ToString();
                        objInner.MOBILENo = sqlReader["VCH_OFF_MOBILE"].ToString();
                        objInner.intDisburseType = Convert.ToInt16(sqlReader["intDisburseType"].ToString()); // Fetching disburse type in Sanction take action page for viewing sanctioned amount
                        objInner.SanctionedAmount = Convert.ToDecimal(sqlReader["Sanctionedamount"].ToString()); // Fetching sanctioned amount in Sanction take action page for viewing sanctioned amount
                        if (sqlReader.GetSchemaTable().Select("ColumnName='TotalCount'").Length > 0)
                        {
                            objInner.intTotalCount = Convert.ToInt32(sqlReader["TotalCount"]);
                        }

                        if (sqlReader.GetSchemaTable().Select("ColumnName='Serial'").Length > 0)
                        {
                            objInner.intSerialNo = Convert.ToInt32(sqlReader["Serial"]);
                        }

                        objInner.DisburseNo = (!string.IsNullOrEmpty(sqlReader["DisburseNo"].ToString())) ? Convert.ToInt64(sqlReader["DisburseNo"]) : 0;
                        objInner.DisburseAmount = (!string.IsNullOrEmpty(sqlReader["DisburseAmount"].ToString())) ? Convert.ToDecimal(sqlReader["DisburseAmount"]) : 0;
                        objInner.DisburseDate = (!string.IsNullOrEmpty(sqlReader["DisburseDate"].ToString())) ? sqlReader["DisburseDate"].ToString() : "";
                        objInner.DisburseTime = (!string.IsNullOrEmpty(sqlReader["DisburseTime"].ToString())) ? sqlReader["DisburseTime"].ToString() : "";
                        objInner.intDisburseStatus = (!string.IsNullOrEmpty(sqlReader["bitDisbursement"].ToString())) ? Convert.ToInt32(sqlReader["bitDisbursement"]) : 0;
                        objInner.DisbursementDocument = (!string.IsNullOrEmpty(sqlReader["vchDisbursementDocument"].ToString())) ? sqlReader["vchDisbursementDocument"].ToString() : "";
                        objInner.BankName = (!string.IsNullOrEmpty(sqlReader["vchBankName"].ToString())) ? sqlReader["vchBankName"].ToString() : "";
                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        public string Incentive_Approval(Inct_Application_Details_Entity objDAEntity)
        {

            object[] arr = {
                                   "@P_VCH_ACTION",objDAEntity.strAction,
                                   "@P_INTINCUNQUEID", objDAEntity.INTINCUNQUEID, //for applicaition no
                                    "@vchRemark", objDAEntity. Remark, //for remark
                                    "@intStatus", objDAEntity.intStatus,
                                    "@isProvisional",objDAEntity.IsProvisional,
                                    "@vchSanFileName",objDAEntity.strSanFileName,//for file upload
                                    "@vchProvisionalCertificate",objDAEntity.ProvisionalCertificate,
                                    "@P_VCHPROVDOCCODE",objDAEntity.VCHPROVDOCCODE,
                                    "@P_VCHSANCDOCCODE",objDAEntity.VCHSANCDOCCODE,
                                    "@P_intDisburseNo",objDAEntity.DisburseNo,
                                    "@P_intDisburseAmount",objDAEntity.DisburseAmount,
                                    "@P_VCHDisburseDate",objDAEntity.DisburseDate,
                                    "@P_VCHDisburseTime",objDAEntity.DisburseTime,
                                    "@vchDisbursementDocument",objDAEntity.DisbursementDocument,
                                    "@vchBankName",objDAEntity.BankName,
                                    "@P_intSanctionedAmount",objDAEntity.SanctionedAmount
                                   };
            int_Return_Val = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_APPLICATION_APPROVE_FETCH", out param, arr);
            return param.ToString();
        }

        #endregion

        #region "Added By Pranay Kumar"
        #region "Fetch Incentives Query Details"
        public List<QueryMgntDtls> getInctRaisedQueryDetails(QueryMgntDtls objQueryMgtDtls)
        {
            List<QueryMgntDtls> list = new List<QueryMgntDtls>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                QueryMgntDtls objQuery;

                object[] arr = {
                                "@P_CHAR_ACTION", objQueryMgtDtls.strAction,
                                "@PintCreatedBy", objQueryMgtDtls.intCreatedBy,
                                "@PvchIncentivesNo", objQueryMgtDtls.strIncentiveUnqNo,
                                "@P_INT_QUERY_ID", objQueryMgtDtls.intQueryId,
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCENTIVES_QUERY_MANAGEMENTDTLS", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objQuery = new QueryMgntDtls();
                        if (objQueryMgtDtls.strAction == "QD")
                        {
                            objQuery.strIncentiveUnqNo = Convert.ToString(sqlReader["vchProposalNo"]);
                            objQuery.strRemarks = Convert.ToString(sqlReader["VCHREMARKS"]);
                            objQuery.intStatus = Convert.ToInt32(sqlReader["INTSTATUS"]);
                            objQuery.strFileName = Convert.ToString(sqlReader["VCHFILENAME"]);
                            objQuery.dtmCreatedOn = Convert.ToString(sqlReader["DTMCREATEDON"]);
                            objQuery.strActionToBeTakenBY = Convert.ToString(sqlReader["FULLNAME"]);
                            objQuery.strQuerytype = Convert.ToString(sqlReader["VCHQUERYTYPE"]);
                            objQuery.intQueryId = Convert.ToInt32(sqlReader["intQueryId"]);
                            objQuery.strQueryDesc = Convert.ToString(sqlReader["QUERY_DESC"]);
                            objQuery.strQueryUnqNo = Convert.ToString(sqlReader["VCH_QUERY_UNQ_NO"]);
                        }
                        else if (objQueryMgtDtls.strAction == "S")
                        {
                            objQuery.strEmailID = Convert.ToString(sqlReader["VCH_MAIL_ID"]);
                            objQuery.strMobileNo = Convert.ToString(sqlReader["VCH_MOB_NO"]);
                            objQuery.strEmailSubject = Convert.ToString(sqlReader["VCH_MAIL_SUB"]);
                            objQuery.strEmailBody = Convert.ToString(sqlReader["VCH_MAIL_BODY"]);
                            objQuery.strSMSContent = Convert.ToString(sqlReader["VCH_SMS_CONTENT"]);
                        }
                        else if (objQueryMgtDtls.strAction == "QF")
                        {
                            objQuery.strFileName = Convert.ToString(sqlReader["VCH_FILE_NAME"]);
                            objQuery.strRemarks = Convert.ToString(sqlReader["VCH_FILE_CONTENT"]);
                        }
                        else if (objQueryMgtDtls.strAction == "SH")
                        {
                            objQuery.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            objQuery.intExtendedStatus = Convert.ToInt32(sqlReader["INT_EXTENDED"]);
                            objQuery.strStatus = Convert.ToString(sqlReader["strStatus"]);

                        }
                        list.Add(objQuery);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        #endregion
        #region "Addition of Raise Query"
        public string IncentivesRaiseQuery(QueryMgntDtls objQueryMgtDtls)
        {
            List<QueryMgntDtls> list = new List<QueryMgntDtls>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INCENTIVES_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objQueryMgtDtls.strAction);
                cmd.Parameters.AddWithValue("@PintQueryId", objQueryMgtDtls.intQueryId);
                cmd.Parameters.AddWithValue("@PvchIncentivesNo", objQueryMgtDtls.strIncentiveUnqNo);
                cmd.Parameters.AddWithValue("@PvchRemarks", objQueryMgtDtls.strRemarks);
                cmd.Parameters.AddWithValue("@PintStatus", objQueryMgtDtls.intStatus);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objQueryMgtDtls.intCreatedBy);
                cmd.Parameters.AddWithValue("@PvchFileName", objQueryMgtDtls.strFileName);
                cmd.Parameters.AddWithValue("@P_XML_TABLE", objQueryMgtDtls.strXML);
                cmd.Parameters.AddWithValue("@P_INT_OUT", SqlDbType.VarChar);
                cmd.Parameters["@P_INT_OUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_INT_OUT"].Value.ToString();
                return Str_RetValue;
            }
            catch (NullReferenceException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        #endregion
        #region "WHEN USER WANTS TO EXTEND THE QUERY DATE"
        public int ExtendDate(string strAction, int intIncentivesUnqNo)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            int intStatus = 0;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INCENTIVES_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@PvchIncentivesNo", intIncentivesUnqNo);
                cmd.Parameters.AddWithValue("@P_INT_OUT", SqlDbType.VarChar);
                cmd.Parameters["@P_INT_OUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                intStatus = Convert.ToInt32(cmd.Parameters["@P_INT_OUT"].Value);
                return intStatus;
            }
            catch (NullReferenceException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        #endregion

        #region "Fetch PC-Large Query Details"
        public List<QueryMgntDtls> getPCLargeRaisedQueryDetails(QueryMgntDtls objQueryMgtDtls)
        {
            List<QueryMgntDtls> list = new List<QueryMgntDtls>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                QueryMgntDtls objQuery;

                object[] arr = {
                                "@P_CHAR_ACTION", objQueryMgtDtls.strAction,
                                "@PintCreatedBy", objQueryMgtDtls.intCreatedBy,
                                "@PvchAppNo", objQueryMgtDtls.strApplicationNum,
                                "@P_INT_QUERY_ID", objQueryMgtDtls.intQueryId,
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_PC_LARGE_QUERY_MANAGEMENTDTLS", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objQuery = new QueryMgntDtls();
                        if (objQueryMgtDtls.strAction == "QD")
                        {
                            objQuery.strApplicationNum = Convert.ToString(sqlReader["vchAppNo"]);
                            objQuery.strRemarks = Convert.ToString(sqlReader["VCHREMARKS"]);
                            objQuery.intStatus = Convert.ToInt32(sqlReader["INTSTATUS"]);
                            objQuery.strFileName = Convert.ToString(sqlReader["VCHFILENAME"]);
                            objQuery.dtmCreatedOn = Convert.ToString(sqlReader["DTMCREATEDON"]);
                            objQuery.strActionToBeTakenBY = Convert.ToString(sqlReader["FULLNAME"]);
                            objQuery.strQuerytype = Convert.ToString(sqlReader["VCHQUERYTYPE"]);
                            objQuery.intQueryId = Convert.ToInt32(sqlReader["intQueryId"]);
                            objQuery.strQueryDesc = Convert.ToString(sqlReader["QUERY_DESC"]);
                            objQuery.strQueryUnqNo = Convert.ToString(sqlReader["VCH_QUERY_UNQ_NO"]);
                        }
                        else if (objQueryMgtDtls.strAction == "S")
                        {
                            objQuery.strEmailID = Convert.ToString(sqlReader["VCH_MAIL_ID"]);
                            objQuery.strMobileNo = Convert.ToString(sqlReader["VCH_MOB_NO"]);
                            objQuery.strEmailSubject = Convert.ToString(sqlReader["VCH_MAIL_SUB"]);
                            objQuery.strEmailBody = Convert.ToString(sqlReader["VCH_MAIL_BODY"]);
                            objQuery.strSMSContent = Convert.ToString(sqlReader["VCH_SMS_CONTENT"]);
                        }
                        else if (objQueryMgtDtls.strAction == "QF")
                        {
                            objQuery.strFileName = Convert.ToString(sqlReader["VCH_FILE_NAME"]);
                            objQuery.strRemarks = Convert.ToString(sqlReader["VCH_FILE_CONTENT"]);
                        }
                        else if (objQueryMgtDtls.strAction == "SH")
                        {
                            objQuery.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            objQuery.intExtendedStatus = Convert.ToInt32(sqlReader["INT_EXTENDED"]);
                            objQuery.strStatus = Convert.ToString(sqlReader["strStatus"]);
                        }
                        else if (objQueryMgtDtls.strAction == "QS")
                        {
                            objQuery.strApplicationNum = Convert.ToString(sqlReader["vchAppFormattedNo"]);
                            objQuery.strQueryUnqNo = Convert.ToString(sqlReader["VCH_APP_NO"]);
                            objQuery.strQuerytype = Convert.ToString(sqlReader["CURRENT_QUERY_STATUS"]);
                        }
                        list.Add(objQuery);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        #endregion
        #region "Addition of Raise Query in PC Large"
        public string PCLargeRaiseQuery(QueryMgntDtls objQueryMgtDtls)
        {
            List<QueryMgntDtls> list = new List<QueryMgntDtls>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PC_LARGE_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objQueryMgtDtls.strAction);
                cmd.Parameters.AddWithValue("@PintQueryId", objQueryMgtDtls.intQueryId);
                cmd.Parameters.AddWithValue("@PvchAppNo", objQueryMgtDtls.strApplicationNum);
                cmd.Parameters.AddWithValue("@PvchRemarks", objQueryMgtDtls.strRemarks);
                cmd.Parameters.AddWithValue("@PintStatus", objQueryMgtDtls.intStatus);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objQueryMgtDtls.intCreatedBy);
                cmd.Parameters.AddWithValue("@PvchFileName", objQueryMgtDtls.strFileName);
                cmd.Parameters.AddWithValue("@P_XML_TABLE", objQueryMgtDtls.strXML);
                cmd.Parameters.AddWithValue("@P_INT_OUT", SqlDbType.VarChar);
                cmd.Parameters["@P_INT_OUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_INT_OUT"].Value.ToString();
                return Str_RetValue;
            }
            catch (NullReferenceException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        #endregion
        #region "WHEN USER WANTS TO EXTEND THE QUERY DATE"
        public int PCLargeExtendDate(string strAction, int intAppNo)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            int intStatus = 0;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PC_LARGE_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@PvchAppNo", intAppNo);
                cmd.Parameters.AddWithValue("@P_INT_OUT", SqlDbType.VarChar);
                cmd.Parameters["@P_INT_OUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                intStatus = Convert.ToInt32(cmd.Parameters["@P_INT_OUT"].Value);
                return intStatus;
            }
            catch (NullReferenceException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        #endregion

        #region "Query Management for PC-MSME"
        #region "Fetch PC-MSME Query Details"
        public List<QueryMgntDtls> getPCMSMERaisedQueryDetails(QueryMgntDtls objQueryMgtDtls)
        {
            List<QueryMgntDtls> list = new List<QueryMgntDtls>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                QueryMgntDtls objQuery;

                object[] arr = {
                                "@P_CHAR_ACTION", objQueryMgtDtls.strAction,
                                "@PintCreatedBy", objQueryMgtDtls.intCreatedBy,
                                "@PvchAppNo", objQueryMgtDtls.strApplicationNum,
                                "@P_INT_QUERY_ID", objQueryMgtDtls.intQueryId,
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_PC_MSME_QUERY_MANAGEMENTDTLS", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objQuery = new QueryMgntDtls();
                        if (objQueryMgtDtls.strAction == "QD")
                        {
                            objQuery.strApplicationNum = Convert.ToString(sqlReader["vchAppNo"]);
                            objQuery.strRemarks = Convert.ToString(sqlReader["VCHREMARKS"]);
                            objQuery.intStatus = Convert.ToInt32(sqlReader["INTSTATUS"]);
                            objQuery.strFileName = Convert.ToString(sqlReader["VCHFILENAME"]);
                            objQuery.dtmCreatedOn = Convert.ToString(sqlReader["DTMCREATEDON"]);
                            objQuery.strActionToBeTakenBY = Convert.ToString(sqlReader["FULLNAME"]);
                            objQuery.strQuerytype = Convert.ToString(sqlReader["VCHQUERYTYPE"]);
                            objQuery.intQueryId = Convert.ToInt32(sqlReader["intQueryId"]);
                            objQuery.strQueryDesc = Convert.ToString(sqlReader["QUERY_DESC"]);
                            objQuery.strQueryUnqNo = Convert.ToString(sqlReader["VCH_QUERY_UNQ_NO"]);
                        }
                        else if (objQueryMgtDtls.strAction == "S")
                        {
                            objQuery.strEmailID = Convert.ToString(sqlReader["VCH_MAIL_ID"]);
                            objQuery.strMobileNo = Convert.ToString(sqlReader["VCH_MOB_NO"]);
                            objQuery.strEmailSubject = Convert.ToString(sqlReader["VCH_MAIL_SUB"]);
                            objQuery.strEmailBody = Convert.ToString(sqlReader["VCH_MAIL_BODY"]);
                            objQuery.strSMSContent = Convert.ToString(sqlReader["VCH_SMS_CONTENT"]);
                        }
                        else if (objQueryMgtDtls.strAction == "QF")
                        {
                            objQuery.strFileName = Convert.ToString(sqlReader["VCH_FILE_NAME"]);
                            objQuery.strRemarks = Convert.ToString(sqlReader["VCH_FILE_CONTENT"]);
                        }
                        else if (objQueryMgtDtls.strAction == "SH")
                        {
                            objQuery.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            objQuery.intExtendedStatus = Convert.ToInt32(sqlReader["INT_EXTENDED"]);
                            objQuery.strStatus = Convert.ToString(sqlReader["strStatus"]);
                        }
                        else if (objQueryMgtDtls.strAction == "QS")
                        {
                            objQuery.strApplicationNum = Convert.ToString(sqlReader["vchAppFormattedNo"]);
                            objQuery.strQueryUnqNo = Convert.ToString(sqlReader["VCH_APP_NO"]);
                            objQuery.strQuerytype = Convert.ToString(sqlReader["CURRENT_QUERY_STATUS"]);
                        }
                        list.Add(objQuery);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        #endregion
        #region "Addition of Raise Query in PC MSME"
        public string PCMSMERaiseQuery(QueryMgntDtls objQueryMgtDtls)
        {
            List<QueryMgntDtls> list = new List<QueryMgntDtls>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PC_MSME_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objQueryMgtDtls.strAction);
                cmd.Parameters.AddWithValue("@PintQueryId", objQueryMgtDtls.intQueryId);
                cmd.Parameters.AddWithValue("@PvchAppNo", objQueryMgtDtls.strApplicationNum);
                cmd.Parameters.AddWithValue("@PvchRemarks", objQueryMgtDtls.strRemarks);
                cmd.Parameters.AddWithValue("@PintStatus", objQueryMgtDtls.intStatus);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objQueryMgtDtls.intCreatedBy);
                cmd.Parameters.AddWithValue("@PvchFileName", objQueryMgtDtls.strFileName);
                cmd.Parameters.AddWithValue("@P_XML_TABLE", objQueryMgtDtls.strXML);
                cmd.Parameters.AddWithValue("@P_INT_OUT", SqlDbType.VarChar);
                cmd.Parameters["@P_INT_OUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_INT_OUT"].Value.ToString();
                return Str_RetValue;
            }
            catch (NullReferenceException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        #endregion
        #region "WHEN USER WANTS TO EXTEND THE QUERY DATE"
        public int PCMSMEExtendDate(string strAction, int intAppNo)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            int intStatus = 0;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PC_MSME_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@PvchAppNo", intAppNo);
                cmd.Parameters.AddWithValue("@P_INT_OUT", SqlDbType.VarChar);
                cmd.Parameters["@P_INT_OUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                intStatus = Convert.ToInt32(cmd.Parameters["@P_INT_OUT"].Value);
                return intStatus;
            }
            catch (NullReferenceException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        #endregion
        #endregion

        #endregion

        public IList<Inct_Application_Details_Entity> View_Application_Details(Inct_Application_Details_Entity objDAEntity)
        {
            List<Inct_Application_Details_Entity> list = new List<Inct_Application_Details_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Inct_Application_Details_Entity objInner;

                object[] arr = {
                                "@P_USER_ID", objDAEntity.strUserID,
                                "@P_VCH_ACTION",objDAEntity.strAction,
                                "@P_INT_INCT_ID",objDAEntity.intInctId,
                                "@P_INT_STATUS",objDAEntity.intStatus,
                                "@P_VCH_APP_NO",objDAEntity.strAppNo,
                                "@P_INT_PAGE_NO",objDAEntity.intPageNo,
                                "@P_INT_PAGE_SIZE",objDAEntity.intPageSize,
                                "@P_INTINCUNQUEID",objDAEntity.INTINCUNQUEID
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_APPLICATION_DETAILS_V", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new Inct_Application_Details_Entity();

                        objInner.strAppNo = sqlReader["ApplicationNum"].ToString();
                        objInner.strUnitName = sqlReader["vchCompName"].ToString();
                        objInner.strInctName = sqlReader["vchInctName"].ToString();
                        objInner.VchInctNum = sqlReader["VCHINCENTIVENO"].ToString();
                        objInner.dtmCreatedOn = Convert.ToDateTime(sqlReader["DTMCREATEDBY"]);
                        objInner.strStatus = sqlReader["vchStatus"].ToString();
                        objInner.intStatus = Convert.ToInt16(sqlReader["INTSTATUS"].ToString());
                        objInner.strApplicationNum = sqlReader["ApplicationNum"].ToString();
                        objInner.strFormPreviewId = sqlReader["nvchFormPreviewId"].ToString();
                        objInner.strSanFileName = sqlReader["vchSanFileName"].ToString();

                        objInner.INTINCUNQUEID = Convert.ToInt16(sqlReader["INTINCUNQUEID"].ToString());
                        if (sqlReader.GetSchemaTable().Select("ColumnName='TotalCount'").Length > 0)
                        {
                            objInner.intTotalCount = Convert.ToInt32(sqlReader["TotalCount"]);
                        }

                        if (sqlReader.GetSchemaTable().Select("ColumnName='Serial'").Length > 0)
                        {
                            objInner.intSerialNo = Convert.ToInt32(sqlReader["Serial"]);
                        }
                        //Added By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button  
                        if (sqlReader.GetSchemaTable().Select("ColumnName='intQueryStatus'").Length > 0)
                        {
                            objInner.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                        }
                        if (sqlReader.GetSchemaTable().Select("ColumnName='CURRENT_QUERY_STATUS'").Length > 0)
                        {
                            objInner.strQueryStatus = Convert.ToString(sqlReader["CURRENT_QUERY_STATUS"]);
                        }

                        objInner.intDisburseStatus = (!string.IsNullOrEmpty(sqlReader["bitDisbursement"].ToString())) ? Convert.ToInt32(sqlReader["bitDisbursement"]) : 0;
                        objInner.intDisburseType = Convert.ToInt32(sqlReader["intDisburseType"]);

                        //Ended By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button         
                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }

        #region "Ritika's Dashboard"

        /// <summary>
        /// Function to get the details of incentive application as per the linkbutton selected in dasboard
        /// </summary>
        /// <param name="objSearch">InctSearch</param>
        /// <returns>List of object InctApplicationDetails_Entity</returns>
        public List<InctApplicationDetails_Entity> ViewInctApplicationDetailsRpt(InctSearch objSearch)
        {
            List<InctApplicationDetails_Entity> lstInctEntity = new List<InctApplicationDetails_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                InctApplicationDetails_Entity objInner;
                object[] arr = { "@pChrActionCode", objSearch.strActionCode
                                   , "@pIntPageIndex", objSearch.intPageIndex
                                   , "@pIntPageSize", objSearch.intPageSize
                                   , "@pVchApplicationNo", objSearch.strApplicationNo
                                   , "@pIntStatus", objSearch.intStatus
                                   , "@pVchUnitName", objSearch.strUnitName
                                   , "@pIntPriority", objSearch.intPriority
                                   , "@pIntUnitType", objSearch.intUnitType
                                   , "@pIntDistrict", objSearch.intDistrict
                                   , "@pIntYear", objSearch.intYear
                               , "@pIntUserUnitType",objSearch.intUserUnitType
                               , "@pIntPolicyType",objSearch.intPolicyType};

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_ApplicationDetailsRpt", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new InctApplicationDetails_Entity();
                        objInner.intRowCount = Convert.ToInt32(sqlReader["rowcnt"]);
                        objInner.strApplicationNum = sqlReader["applicationNum"].ToString();
                        objInner.strInctName = sqlReader["vchInctName"].ToString();
                        objInner.strUnitName = (sqlReader["vchEnterPriseName"]).ToString();
                        objInner.strSectorName = sqlReader["sectorName"].ToString();
                        objInner.strPriority = sqlReader["BitPriority"].ToString();
                        objInner.strUnitType = sqlReader["UnitType"].ToString();
                        objInner.strStatus = sqlReader["appstatus"].ToString();
                        objInner.strAppliedOn = sqlReader["DTMCREATEDBY"].ToString();
                        lstInctEntity.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstInctEntity;
        }

        /// <summary>
        /// Function to get the details of incentive application in dasboard
        /// </summary>
        /// <param name="objSearch">InctSearch</param>
        /// <returns>List of object InctDashBoard_Entity</returns>
        public List<InctDashBoard_Entity> ViewInctDashBoardDetails(InctSearch objSearch)
        {
            List<InctDashBoard_Entity> lstInctEntity = new List<InctDashBoard_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                InctDashBoard_Entity objInner;
                object[] arr = {  "@P_INTFINYEAR", objSearch.intYear
                                   , "@P_INTDISTRID", objSearch.intDistrict
                                   , "@P_INTCATGRID", objSearch.intUnitType
                                    , "@P_INTUNITTYP", objSearch.intStatus
                                     , "@P_INTPOLICID", objSearch.intPolicyType
                               };

                sqlReader = SqlHelper.ExecuteReader(conn, "usp_INCT_DBDET", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new InctDashBoard_Entity();
                        objInner.intParentId = Convert.ToInt32(sqlReader["intparentid"]);
                        objInner.intMainId = Convert.ToInt32(sqlReader["intmileid"]);
                        objInner.strName = sqlReader["vchmainmilename"].ToString();
                        objInner.intCount = Convert.ToInt32(sqlReader["numberofapp"]);
                        objInner.decAmount = Convert.ToDecimal(sqlReader["numamount"]);
                        lstInctEntity.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstInctEntity;
        }

        #endregion

        #region MisReports
        /// <summary>
        /// function to get the details for the Incentive Claimwise report
        /// </summary>
        /// <param name="objSearch">InctSearch entity - it will get policytype,financial year and action code</param>
        /// <returns>List of InctWiseClaimReport entity object</returns>
        public List<InctWiseClaimReport> View_MIS_IncentiveWiseClaimDetails(InctSearch objSearch)
        {
            List<InctWiseClaimReport> lstInctEntity = new List<InctWiseClaimReport>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                InctWiseClaimReport objInner;
                object[] arr = { "@pChrActionCode", objSearch.strActionCode
                               , "@pIntYear", objSearch.intYear
                               , "@pIntPolicyType",objSearch.intPolicyType
                               , "@pIntPageIndex", objSearch.intPageIndex
                               , "@pIntPageSize", objSearch.intPageSize
                               };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_MIS_IncentiveWiseDetails", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new InctWiseClaimReport();
                        objInner.strIncentiveName = sqlReader["vchInctName"].ToString();
                        objInner.strIncentiveType = sqlReader["vchName"].ToString();
                        objInner.strPolicyName = sqlReader["policy_name"].ToString();
                        objInner.intIncentiveId = Convert.ToInt32(sqlReader["intInctId"]);
                        objInner.intDraftedCount = Convert.ToInt32(sqlReader["cnt_drafted"]);
                        objInner.intAppliedCount = Convert.ToInt32(sqlReader["cnt_Applied"]);
                        objInner.intPendingCount = Convert.ToInt32(sqlReader["cnt_Pending"]);
                        objInner.intApprovedCount = Convert.ToInt32(sqlReader["cnt_Approved"]);
                        objInner.intRejectedCount = Convert.ToInt32(sqlReader["cnt_rejected"]);
                        objInner.intDisbursedCount = Convert.ToInt32(sqlReader["cnt_Disbursed"]);
                        objInner.decApprovedAmt = Convert.ToDecimal(sqlReader["ApprovedAmount"]);
                        objInner.decClaimedAmt = Convert.ToDecimal(sqlReader["ClaimedAmount"]);
                        objInner.decDisbursedAmt = Convert.ToDecimal(sqlReader["disbursedAmount"]);
                        objInner.decPendingAmt = Convert.ToDecimal(sqlReader["PendingAmount"]);
                        objInner.decRejectedAmt = Convert.ToDecimal(sqlReader["RejectedAmount"]);
                        objInner.intRowCount = Convert.ToInt32(sqlReader["row_cnt"]);
                        lstInctEntity.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstInctEntity;
        }

        /// <summary>
        /// function to get the details for the Incentive Claimwise report
        /// </summary>
        /// <param name="objSearch">InctSearch entity - it will get policytype,financial year and action code</param>
        /// <returns>List of InctWiseClaimReport entity object</returns>
        public List<InctWiseClaimReport> View_MIS_ApplicantUnitWiseClaimDetails(InctSearch objSearch)
        {
            List<InctWiseClaimReport> lstInctEntity = new List<InctWiseClaimReport>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                InctWiseClaimReport objInner;
                object[] arr = { "@pChrActionCode", objSearch.strActionCode
                               , "@pIntYear", objSearch.intYear
                               , "@pIntIncentiveType",objSearch.intUnitType //it contains the incentive type to show unit wise claim details for the incentive
                               , "@pIntPageIndex", objSearch.intPageIndex
                               , "@pIntPageSize", objSearch.intPageSize
                               };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_MIS_IncentiveWiseDetails", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new InctWiseClaimReport();
                        objInner.strIncentiveName = sqlReader["inct_name"].ToString();
                        objInner.strUnitName = sqlReader["vchEnterpriseName"].ToString();
                        objInner.strUnitType = sqlReader["vchunitCategory"].ToString();
                        objInner.intIncentiveId = Convert.ToInt32(sqlReader["intUnitId"]);
                        objInner.intDraftedCount = Convert.ToInt32(sqlReader["cnt_drafted"]);
                        objInner.intAppliedCount = Convert.ToInt32(sqlReader["cnt_Applied"]);
                        objInner.intPendingCount = Convert.ToInt32(sqlReader["cnt_Pending"]);
                        objInner.intApprovedCount = Convert.ToInt32(sqlReader["cnt_Approved"]);
                        objInner.intRejectedCount = Convert.ToInt32(sqlReader["cnt_rejected"]);
                        objInner.intDisbursedCount = Convert.ToInt32(sqlReader["cnt_Disbursed"]);
                        objInner.decApprovedAmt = Convert.ToDecimal(sqlReader["ApprovedAmount"]);
                        objInner.decClaimedAmt = Convert.ToDecimal(sqlReader["ClaimedAmount"]);
                        objInner.decDisbursedAmt = Convert.ToDecimal(sqlReader["disbursedAmount"]);
                        objInner.decPendingAmt = Convert.ToDecimal(sqlReader["PendingAmount"]);
                        objInner.decRejectedAmt = Convert.ToDecimal(sqlReader["RejectedAmount"]);
                        objInner.intRowCount = Convert.ToInt32(sqlReader["row_cnt"]);
                        lstInctEntity.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstInctEntity;
        }


        /// <summary>
        /// Function to get the unitwise details for a particular incentive and the status
        /// </summary>
        /// <param name="objSearch">InctSearch object</param>
        /// <returns>List of InctApplicationStatusRpt_Entity</returns>
        public List<InctApplicationStatusRpt_Entity> View_Mis_InctWiseStatusDetails(InctSearch objSearch)
        {
            List<InctApplicationStatusRpt_Entity> lstInctEntity = new List<InctApplicationStatusRpt_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                InctApplicationStatusRpt_Entity objInner;
                object[] arr = { "@pChrActionCode", objSearch.strActionCode
                               , "@pIntYear", objSearch.intYear
                               , "@pIntIncentiveType",objSearch.intUnitType //it contains the incentive type to show unit wise claim details for the incentive
                               , "@pIntPageIndex", objSearch.intPageIndex
                               , "@pIntPageSize", objSearch.intPageSize
                               , "@pIntStatus", objSearch.intStatus
                               };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_MIS_IncentiveWiseDetails", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new InctApplicationStatusRpt_Entity();
                        objInner.strUnitName = sqlReader["vchEnterpriseName"].ToString();
                        objInner.strAppliedOn = sqlReader["dtmAppliedOn"].ToString();
                        objInner.decClaimAmount = Convert.ToDecimal(sqlReader["DECCLAIMREIMBURSEMENT"].ToString());
                        objInner.strPendingAt = sqlReader["vchusername"].ToString(); ;
                        objInner.strDisbursementDate = sqlReader["DisburseDate"].ToString();
                        objInner.decDisbursementAmount = Convert.ToDecimal(sqlReader["DisburseAmount"].ToString());
                        objInner.intRowCount = Convert.ToInt32(sqlReader["row_cnt"]);
                        objInner.strIncentiveName = sqlReader["vchInctname"].ToString();
                        objInner.strStatus = sqlReader["status"].ToString();
                        lstInctEntity.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstInctEntity;
        }

        /// <summary>
        /// Function to show unit wise incentive wise claim details report
        /// </summary>
        /// <param name="objSearch">InctSearch object</param>
        /// <returns>List of InctWiseClaimReport object</returns>
        public List<InctWiseClaimReport> View_MIS_UnitWiseIncentiveClaimDetails(InctSearch objSearch)
        {
            List<InctWiseClaimReport> lstInctEntity = new List<InctWiseClaimReport>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                InctWiseClaimReport objInner;
                object[] arr = { "@pChrActionCode", objSearch.strActionCode
                               , "@pIntYear", objSearch.intYear
                               , "@pIntIncentiveType",objSearch.intUnitType //it contains the incentive type to show unit wise claim details for the incentive
                               , "@pIntPageIndex", objSearch.intPageIndex
                               , "@pIntPageSize", objSearch.intPageSize
                               };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_MIS_IncentiveWiseDetails", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new InctWiseClaimReport();
                        objInner.strIncentiveName = sqlReader["vchInctName"].ToString();
                        objInner.strUnitName = sqlReader["vchEnterpriseName"].ToString();
                        objInner.intIncentiveId = Convert.ToInt32(sqlReader["intinctid"]);
                        objInner.intDraftedCount = Convert.ToInt32(sqlReader["cnt_drafted"]);
                        objInner.intAppliedCount = Convert.ToInt32(sqlReader["cnt_Applied"]);
                        objInner.intPendingCount = Convert.ToInt32(sqlReader["cnt_Pending"]);
                        objInner.intApprovedCount = Convert.ToInt32(sqlReader["cnt_Approved"]);
                        objInner.intRejectedCount = Convert.ToInt32(sqlReader["cnt_rejected"]);
                        objInner.intDisbursedCount = Convert.ToInt32(sqlReader["cnt_Disbursed"]);
                        objInner.decApprovedAmt = Convert.ToDecimal(sqlReader["ApprovedAmount"]);
                        objInner.decClaimedAmt = Convert.ToDecimal(sqlReader["ClaimedAmount"]);
                        objInner.decDisbursedAmt = Convert.ToDecimal(sqlReader["disbursedAmount"]);
                        objInner.decPendingAmt = Convert.ToDecimal(sqlReader["PendingAmount"]);
                        objInner.decRejectedAmt = Convert.ToDecimal(sqlReader["RejectedAmount"]);
                        objInner.intRowCount = Convert.ToInt32(sqlReader["row_cnt"]);
                        lstInctEntity.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return lstInctEntity;
        }


        public string Thrust_Priority_AED(Basic_Unit_Details_Entity objOGEntity)
        {
            try
            {
                object[] arr = {
                                "@P_VCHINCENTIVENO",objOGEntity.strInctFlow,
                                "@P_VCHPROPOSALNO",objOGEntity.strPcNoAfter,
                                "@P_VCHUNITCODE",objOGEntity.strIndustryCode,
                                "@P_INT_USER_ID", objOGEntity.intCreatedBy,
                                "@P_vchEnterpriseName", objOGEntity.strEnterpriseName,
                                "@P_intOrganisationType", objOGEntity.intOrganisationType,
                                "@P_vchIndustryAddress", objOGEntity.strIndustryAddress,
                                "@P_intUnitCat", objOGEntity.intUnitCat,
                                "@P_vchRegisteredOfcAddress", objOGEntity.strRegisteredOfcAddress,
                                "@P_vchManagingPartnerGender", objOGEntity.strManagingPartnerGender,
                                "@P_vchManagingPartnerName", objOGEntity.strManagingPartnerName,
                                "@P_vchEINNO", objOGEntity.strEINNO,
                                "@P_dtmEIN", objOGEntity.dtmEIN,
                                "@P_vchPcNoBefore", objOGEntity.strPcNoBefore,
                                "@P_dtmProdCommBefore", objOGEntity.dtmProdCommBefore,
                                "@P_dtmFFCIDateBefore", objOGEntity.dtmFFCIDateBefore,
                                "@P_vchUAMNo", objOGEntity.strUAMNo,
                                "@P_dtmUAM", objOGEntity.dtmUAMdate,
                                "@P_intEIMorUAMType", objOGEntity.intEIMorUAMtype,
                                "@P_decLandAmtBefore", objOGEntity.decLandAmtBefore,
                                "@P_decBuildingAmtBefore", objOGEntity.decBuildingAmtBefore,
                                "@P_decPlantMachAmtBefore", objOGEntity.decPlantMachAmtBefore,
                                "@P_decOtheFixedAssetAmtBefore", objOGEntity.decOtheFixedAssetAmtBefore,
                                "@P_decTotalAmtBefore", objOGEntity.decTotalAmtBefore,
                                "@P_dtmFFCIDateAfter", objOGEntity.dtmFFCIDateAfter,
                                "@P_decLandAmtAfter", objOGEntity.decLandAmtAfter,
                                "@P_decBuildingAmtAfter", objOGEntity.decBuildingAmtAfter,
                                "@P_decPlantMachAmtAfter", objOGEntity.decPlantMachAmtAfter,
                                "@P_decOtheFixedAssetAmtAfter", objOGEntity.decOtheFixedAssetAmtAfter,
                                "@P_decTotalAmtAfter", objOGEntity.decTotalAmtAfter,
                                "@P_decEquity", objOGEntity.decEquity,
                                "@P_intProjectClearnce", objOGEntity.intProjectClearance,
                                "@P_intPPThrustStatus", objOGEntity.intProvisnalPriorityThrustStatus,
                                "@P_intIPRInctiveAvel", objOGEntity.intIPRinctiveAvel,
                                "@P_vchClearnceSWM", objOGEntity.strClearnceswm,
                                "@P_XML_TBL_PROD_ITEM_BE", objOGEntity.ProductionItem_BE.SerializeToXMLString(),
                                "@P_XML_TBL_WORKING_LOAN", objOGEntity.WorkingCapitalLoan.SerializeToXMLString(),
                                "@P_XML_TBL_TERM_LOAN", objOGEntity.TermLoan.SerializeToXMLString(),
                                "@P_decElectricalInstAmtBefore", objOGEntity.decElectricalInstAmtBefore,
                                "@P_decElectricalInstAmtAfter", objOGEntity.decElectricalInstAmtAfter,
                                "@P_decLoadingAmtBefore", objOGEntity.decLoadUnloadAmtBefore,
                                "@P_decLoadingAmtAfter", objOGEntity.decLoadUnloadAmtAfter,
                                "@P_decMarginMoneyAmtBefore", objOGEntity.decMarginMoneyForworkingAmtBefore,
                                "@P_decMarginMoneyAmtAfter", objOGEntity.decMarginMoneyForworkingAmtAfter,
                                "@P_vchPworofAttorneyPreDocCode", objOGEntity.strPworofAttorneyPreDocCode,
                                "@P_vchPworofAttorneyPre", objOGEntity.strPworofAttorneyPre,
                                "@P_vchCertificateofregistrationDocCodepre", objOGEntity.strCertificateofregistrationDocCodepre,
                                "@P_vchCertificateofregistrationpre", objOGEntity.strCertificateofregistrationpre,
                                "@P_vchApproveDPRDocCodePre", objOGEntity.strApproveDPRDocCodePre,
                                "@P_vchApproveDPRPre", objOGEntity.strApproveDPRPre,
                                "@P_vchEINapprovalDocCodePre", objOGEntity.strEINapprovalDocCodePre,
                                "@P_vchEINapprovalPre", objOGEntity.strEINapprovalPre,
                                "@P_vchBalacingEquipmentDocCodePre", objOGEntity.strBalacingEquipmentDocCodePre,
                                "@P_vchBalacingEquipmentPre", objOGEntity.strBalacingEquipmentPre,
                                "@P_vchCapitalInvstDocCodePre", objOGEntity.strCapitalInvstDocCodePre,
                                "@P_vchCapitalInvstPre", objOGEntity.strCapitalInvstPre,
                                "@P_vchInvestmentplantmachinaryDocCodePre", objOGEntity.strInvestmentplantmachinaryDocCodePre,
                                "@P_vchInvestmentplantmachinaryPre", objOGEntity.strInvestmentplantmachinaryPre,
                                "@P_vchInvestmentplantmachinaryDocCodePre", objOGEntity.strInvestmentplantmachinaryDocCodePre,
                                "@P_vchInvestmentplantmachinaryPre", objOGEntity.strInvestmentplantmachinaryPre,
                                "@P_vchProposedprodDocCodePre", objOGEntity.strProposedprodDocCodePre,
                                "@P_vchProposedprodPre", objOGEntity.strProposedprodPre,
                                "@P_vchPresentStageImplentDocCodePre", objOGEntity.strPresentStageImplentDocCodePre,
                                "@P_vchPresentStageImplentPre", objOGEntity.strPresentStageImplentPre,
                                "@P_vchMigrantIndustryunitDocCodePre", objOGEntity.strMigrantIndustryunitDocCodePre,
                                "@P_vchMigrantIndustryunitPre", objOGEntity.strMigrantIndustryunitPre,
                                "@P_vchfixedcapitalinvstDocCodePre", objOGEntity.strfixedcapitalinvstDocCodePre,
                                "@P_vchfixedcapitalinvstPre", objOGEntity.strfixedcapitalinvstPre,
                                "@P_vchPriorityorThrustsectorDocCodePre", objOGEntity.strPriorityorThrustsectorDocCodePre,
                                "@P_vchPriorityorThrustsectorPre", objOGEntity.strPriorityorThrustsectorPre,
                                "@P_vchPworofAttorneyPostDocCode", objOGEntity.strPworofAttorneyPostDocCode,
                                "@P_vchPworofAttorneyPost", objOGEntity.strPworofAttorneyPost,
                                "@P_vchPPorThrustStatusCertPostDocCode", objOGEntity.strPPorThrustStatusCertPostDocCode,
                                "@P_vchPPorThrustStatusCertPost", objOGEntity.strPPorThrustStatusCertPost,
                                "@P_vchCertificateofregistrationDocCodepost", objOGEntity.strCertificateofregistrationDocCodepost,
                                "@P_vchCertificateofregistrationpost", objOGEntity.strCertificateofregistrationpost,
                                "@P_vchApproveDPRDocCodePost", objOGEntity.strApproveDPRDocCodePost,
                                "@P_vchApproveDPRPost", objOGEntity.strApproveDPRPost,
                                "@P_vchPcDocCodePost", objOGEntity.strPcDocCodePost,
                                "@P_vchPcPost", objOGEntity.strPcPost,
                                "@P_vchSanctionbankorFIDocCodePost", objOGEntity.strSanctionbankorFIDocCodePost,
                                "@P_vchSanctionbankorFIPost", objOGEntity.strSanctionbankorFIPost,
                                "@P_vchCapitalInvstDocCodePost", objOGEntity.strCapitalInvstDocCodePost,
                                "@P_vchCapitalInvstPost", objOGEntity.strCapitalInvstPost,
                                "@P_vchInvestmentplantmachinaryDocCodePost", objOGEntity.strInvestmentplantmachinaryDocCodePost,
                                "@P_vchInvestmentplantmachinaryPost", objOGEntity.strInvestmentplantmachinaryPost,
                                "@P_vchBalacingEquipmentDocCodePost", objOGEntity.strBalacingEquipmentDocCodePost,
                                "@P_vchBalacingEquipmentPost", objOGEntity.strBalacingEquipmentPost,
                                "@P_vchServiceProvideDocCodePost", objOGEntity.strServiceProvideDocCodePost,
                                "@P_vchServiceProvidePost", objOGEntity.strServiceProvidePost,
                                "@P_vchPriorityorThrustsectorDocCodePost", objOGEntity.strPriorityorThrustsectorDocCodePost,
                                "@P_vchPriorityorThrustsectorPost", objOGEntity.strPriorityorThrustsectorPost,
                                "@P_vchClearancefromPCBDocCodePost", objOGEntity.strClearancefromPCBDocCodePost,
                                "@P_vchClearancefromPCBPost", objOGEntity.strClearancefromPCBPost,
                                "@P_vchMigrantIndustryunitDocCodePost", objOGEntity.strMigrantIndustryunitDocCodePost,
                                "@P_vchMigrantIndustryunitPost", objOGEntity.strMigrantIndustryunitPost,
                                "@P_vchfixedcapitalinvstDocCodePost", objOGEntity.strfixedcapitalinvstDocCodePost,
                                "@P_vchfixedcapitalinvstPost", objOGEntity.strfixedcapitalinvstPost,
                                "@P_vchEmpoweredCommitteeDocCodePost", objOGEntity.strEmpoweredCommitteeDocCodePost,
                                "@P_vchEmpoweredCommitteePost", objOGEntity.strEmpoweredCommitteePost,

                                "@P_intDirectEmpAfter", objOGEntity.intDirectEmpAfter,
                                "@P_vchPhoneNo", objOGEntity.strPhoneNo,
                                "@P_vchEmail", objOGEntity.strEmail,

                                "@P_OUT_MSG", "OUT"
                                };
                
                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_ThrustOrPriorityIPR_2022_AED", out param, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param.ToString();
        }

        public IList<Inct_Application_Details_Entity> IPR2022View_Application_Details(Inct_Application_Details_Entity objDAEntity)
        {
            List<Inct_Application_Details_Entity> list = new List<Inct_Application_Details_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Inct_Application_Details_Entity objInner;

                object[] arr = {
                                "@P_USER_ID", objDAEntity.strUserID,
                                "@P_VCH_ACTION",objDAEntity.strAction,
                                "@P_VCH_APP_NO",objDAEntity.strAppNo,
                                "@P_INT_STATUS",objDAEntity.intStatus,
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_APPLICATION_DETAILS_V", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new Inct_Application_Details_Entity();

                        objInner.strAppNo = sqlReader["ApplicationNum"].ToString();
                        objInner.strUnitName = sqlReader["vchCompName"].ToString();
                        objInner.strInctName = sqlReader["vchInctName"].ToString();
                        objInner.VchInctNum = sqlReader["VCHINCENTIVENO"].ToString();
                        objInner.dtmCreatedOn = Convert.ToDateTime(sqlReader["DTMCREATEDBY"]);
                        objInner.strStatus = sqlReader["vchStatus"].ToString();
                        objInner.intStatus = Convert.ToInt16(sqlReader["INTSTATUS"].ToString());
                        objInner.strApplicationNum = sqlReader["ApplicationNum"].ToString();
                        objInner.INTINCUNQUEID = Convert.ToInt16(sqlReader["INTINCUNQUEID"].ToString());
                        objInner.strFormPreviewId = sqlReader["nvchFormPreviewId"].ToString();
                        objInner.strSanFileName = sqlReader["vchSanFileName"].ToString();
                        objInner.Remark = sqlReader["vchRemark"].ToString();

                        if (sqlReader.GetSchemaTable().Select("ColumnName='TotalCount'").Length > 0)
                        {
                            objInner.intTotalCount = Convert.ToInt32(sqlReader["TotalCount"]);
                        }

                        if (sqlReader.GetSchemaTable().Select("ColumnName='Serial'").Length > 0)
                        {
                            objInner.intSerialNo = Convert.ToInt32(sqlReader["Serial"]);
                        }
                        
                        if (sqlReader.GetSchemaTable().Select("ColumnName='intQueryStatus'").Length > 0)
                        {
                            objInner.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                        }
                        if (sqlReader.GetSchemaTable().Select("ColumnName='CURRENT_QUERY_STATUS'").Length > 0)
                        {
                            objInner.strQueryStatus = Convert.ToString(sqlReader["CURRENT_QUERY_STATUS"]);
                        }

                        //objInner.intDisburseStatus = (!string.IsNullOrEmpty(sqlReader["bitDisbursement"].ToString())) ? Convert.ToInt32(sqlReader["bitDisbursement"]) : 0;
                        //objInner.intDisburseType = Convert.ToInt32(sqlReader["intDisburseType"]);

                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }

        public string IPR2022Incentive_Approval(Inct_Application_Details_Entity objDAEntity)
        {

            object[] arr = {
                                    "@P_VCH_ACTION",objDAEntity.strAction,
                                    "@P_INTINCUNQUEID", objDAEntity.INTINCUNQUEID, //for applicaition no
                                    "@vchRemark", objDAEntity. Remark, //for remark
                                    "@intStatus", objDAEntity.intStatus,   
                                    "@vchSanFileName",objDAEntity.strSanFileName,//for file upload
                                    "@P_VCHPROVDOCCODE",objDAEntity.VCHPROVDOCCODE,
                                    "@P_intActionTakenBy",objDAEntity.intActionTakenBy,
                                    "@P_intSectorStatus",objDAEntity.intSectorStatus,
                                   
                                   };
            int_Return_Val = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_INCT_APPLICATION_APPROVE_FETCH", out param, arr);
            return param.ToString();
        }


        public IList<Inct_Application_Details_Entity> DI_ViewIPR2022_Application_Details(Inct_Application_Details_Entity objDAEntity)
        {
            List<Inct_Application_Details_Entity> list = new List<Inct_Application_Details_Entity>();
            SqlDataReader sqlReader = null;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Inct_Application_Details_Entity objInner;

                object[] arr = {
                                "@P_USER_ID", objDAEntity.strUserID,
                                "@P_VCH_ACTION",objDAEntity.strAction,
                                "@P_VCH_APP_NO",objDAEntity.strAppNo,
                                "@P_INT_STATUS",objDAEntity.intStatus,
                                };

                sqlReader = SqlHelper.ExecuteReader(conn, "USP_INCT_APPLICATION_DETAILS_V", arr);
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new Inct_Application_Details_Entity();

                        objInner.strAppNo = sqlReader["ApplicationNum"].ToString();
                        objInner.strUnitName = sqlReader["vchCompName"].ToString();
                        objInner.strInctName = sqlReader["vchInctName"].ToString();
                        objInner.VchInctNum = sqlReader["VCHINCENTIVENO"].ToString();
                        objInner.dtmCreatedOn = Convert.ToDateTime(sqlReader["DTMCREATEDBY"]);
                        objInner.strStatus = sqlReader["vchStatus"].ToString();
                        objInner.intStatus = Convert.ToInt16(sqlReader["INTSTATUS"].ToString());
                        objInner.strApplicationNum = sqlReader["ApplicationNum"].ToString();
                        objInner.INTINCUNQUEID = Convert.ToInt16(sqlReader["INTINCUNQUEID"].ToString());
                        objInner.strFormPreviewId = sqlReader["nvchFormPreviewId"].ToString();
                        objInner.strSanFileName = sqlReader["vchSanFileName"].ToString();
                        objInner.Remark = sqlReader["vchRemark"].ToString();

                        list.Add(objInner);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }


        public string Thrust_Priority_Draft(Basic_Unit_Details_Entity objOGEntity)
        {
            try
            {
                object[] arr = {

                                "@P_VCHINCENTIVENO",objOGEntity.strIncentiveNumber,
                                "@P_VCHPROPOSALNO",objOGEntity.strPcNoAfter,
                                "@P_VCHUNITCODE",objOGEntity.strIndustryCode,
                                "@P_INT_USER_ID", objOGEntity.intCreatedBy,
                                "@P_vchEnterpriseName", objOGEntity.strEnterpriseName,
                                "@P_intOrganisationType", objOGEntity.intOrganisationType,
                                "@P_vchIndustryAddress", objOGEntity.strIndustryAddress,
                                "@P_intUnitCat", objOGEntity.intUnitCat,
                                "@P_vchRegisteredOfcAddress", objOGEntity.strRegisteredOfcAddress,
                                "@P_vchManagingPartnerGender", objOGEntity.strManagingPartnerGender,
                                "@P_vchManagingPartnerName", objOGEntity.strManagingPartnerName,
                                "@P_vchEINNO", objOGEntity.strEINNO,
                                "@P_dtmEIN", objOGEntity.dtmEIN,
                                "@P_vchPcNoBefore", objOGEntity.strPcNoBefore,
                                "@P_dtmProdCommBefore", objOGEntity.dtmProdCommBefore,
                                "@P_dtmFFCIDateBefore", objOGEntity.dtmFFCIDateBefore,
                                "@P_vchUAMNo", objOGEntity.strUAMNo,
                                "@P_dtmUAM", objOGEntity.dtmUAMdate,
                                "@P_intEIMorUAMType", objOGEntity.intEIMorUAMtype,
                                "@P_decLandAmtBefore", objOGEntity.decLandAmtBefore,
                                "@P_decBuildingAmtBefore", objOGEntity.decBuildingAmtBefore,
                                "@P_decPlantMachAmtBefore", objOGEntity.decPlantMachAmtBefore,
                                "@P_decOtheFixedAssetAmtBefore", objOGEntity.decOtheFixedAssetAmtBefore,
                                "@P_decTotalAmtBefore", objOGEntity.decTotalAmtBefore,
                                "@P_dtmFFCIDateAfter", objOGEntity.dtmFFCIDateAfter,
                                "@P_decLandAmtAfter", objOGEntity.decLandAmtAfter,
                                "@P_decBuildingAmtAfter", objOGEntity.decBuildingAmtAfter,
                                "@P_decPlantMachAmtAfter", objOGEntity.decPlantMachAmtAfter,
                                "@P_decOtheFixedAssetAmtAfter", objOGEntity.decOtheFixedAssetAmtAfter,
                                "@P_decTotalAmtAfter", objOGEntity.decTotalAmtAfter,
                                "@P_decEquity", objOGEntity.decEquity,
                                "@P_intProjectClearnce", objOGEntity.intProjectClearance,
                                "@P_intPPThrustStatus", objOGEntity.intProvisnalPriorityThrustStatus,
                                "@P_intIPRInctiveAvel", objOGEntity.intIPRinctiveAvel,
                                "@P_vchClearnceSWM", objOGEntity.strClearnceswm,
                                "@P_XML_TBL_PROD_ITEM_BE", objOGEntity.ProductionItem_BE.SerializeToXMLString(),
                                "@P_XML_TBL_WORKING_LOAN", objOGEntity.WorkingCapitalLoan.SerializeToXMLString(),
                                "@P_XML_TBL_TERM_LOAN", objOGEntity.TermLoan.SerializeToXMLString(),
                                "@P_decElectricalInstAmtBefore", objOGEntity.decElectricalInstAmtBefore,
                                "@P_decElectricalInstAmtAfter", objOGEntity.decElectricalInstAmtAfter,
                                "@P_decLoadingAmtBefore", objOGEntity.decLoadUnloadAmtBefore,
                                "@P_decLoadingAmtAfter", objOGEntity.decLoadUnloadAmtAfter,
                                "@P_decMarginMoneyAmtBefore", objOGEntity.decMarginMoneyForworkingAmtBefore,
                                "@P_decMarginMoneyAmtAfter", objOGEntity.decMarginMoneyForworkingAmtAfter,
                                "@P_vchPworofAttorneyPreDocCode", objOGEntity.strPworofAttorneyPreDocCode,
                                "@P_vchPworofAttorneyPre", objOGEntity.strPworofAttorneyPre,
                                "@P_vchCertificateofregistrationDocCodepre", objOGEntity.strCertificateofregistrationDocCodepre,
                                "@P_vchCertificateofregistrationpre", objOGEntity.strCertificateofregistrationpre,
                                "@P_vchApproveDPRDocCodePre", objOGEntity.strApproveDPRDocCodePre,
                                "@P_vchApproveDPRPre", objOGEntity.strApproveDPRPre,
                                "@P_vchEINapprovalDocCodePre", objOGEntity.strEINapprovalDocCodePre,
                                "@P_vchEINapprovalPre", objOGEntity.strEINapprovalPre,
                                "@P_vchBalacingEquipmentDocCodePre", objOGEntity.strBalacingEquipmentDocCodePre,
                                "@P_vchBalacingEquipmentPre", objOGEntity.strBalacingEquipmentPre,
                                "@P_vchCapitalInvstDocCodePre", objOGEntity.strCapitalInvstDocCodePre,
                                "@P_vchCapitalInvstPre", objOGEntity.strCapitalInvstPre,
                                "@P_vchInvestmentplantmachinaryDocCodePre", objOGEntity.strInvestmentplantmachinaryDocCodePre,
                                "@P_vchInvestmentplantmachinaryPre", objOGEntity.strInvestmentplantmachinaryPre,
                                "@P_vchInvestmentplantmachinaryDocCodePre", objOGEntity.strInvestmentplantmachinaryDocCodePre,
                                "@P_vchInvestmentplantmachinaryPre", objOGEntity.strInvestmentplantmachinaryPre,
                                "@P_vchProposedprodDocCodePre", objOGEntity.strProposedprodDocCodePre,
                                "@P_vchProposedprodPre", objOGEntity.strProposedprodPre,
                                "@P_vchPresentStageImplentDocCodePre", objOGEntity.strPresentStageImplentDocCodePre,
                                "@P_vchPresentStageImplentPre", objOGEntity.strPresentStageImplentPre,
                                "@P_vchMigrantIndustryunitDocCodePre", objOGEntity.strMigrantIndustryunitDocCodePre,
                                "@P_vchMigrantIndustryunitPre", objOGEntity.strMigrantIndustryunitPre,
                                "@P_vchfixedcapitalinvstDocCodePre", objOGEntity.strfixedcapitalinvstDocCodePre,
                                "@P_vchfixedcapitalinvstPre", objOGEntity.strfixedcapitalinvstPre,
                                "@P_vchPriorityorThrustsectorDocCodePre", objOGEntity.strPriorityorThrustsectorDocCodePre,
                                "@P_vchPriorityorThrustsectorPre", objOGEntity.strPriorityorThrustsectorPre,
                                "@P_vchPworofAttorneyPostDocCode", objOGEntity.strPworofAttorneyPostDocCode,
                                "@P_vchPworofAttorneyPost", objOGEntity.strPworofAttorneyPost,
                                "@P_vchPPorThrustStatusCertPostDocCode", objOGEntity.strPPorThrustStatusCertPostDocCode,
                                "@P_vchPPorThrustStatusCertPost", objOGEntity.strPPorThrustStatusCertPost,
                                "@P_vchCertificateofregistrationDocCodepost", objOGEntity.strCertificateofregistrationDocCodepost,
                                "@P_vchCertificateofregistrationpost", objOGEntity.strCertificateofregistrationpost,
                                "@P_vchApproveDPRDocCodePost", objOGEntity.strApproveDPRDocCodePost,
                                "@P_vchApproveDPRPost", objOGEntity.strApproveDPRPost,
                                "@P_vchPcDocCodePost", objOGEntity.strPcDocCodePost,
                                "@P_vchPcPost", objOGEntity.strPcPost,
                                "@P_vchSanctionbankorFIDocCodePost", objOGEntity.strSanctionbankorFIDocCodePost,
                                "@P_vchSanctionbankorFIPost", objOGEntity.strSanctionbankorFIPost,
                                "@P_vchCapitalInvstDocCodePost", objOGEntity.strCapitalInvstDocCodePost,
                                "@P_vchCapitalInvstPost", objOGEntity.strCapitalInvstPost,
                                "@P_vchInvestmentplantmachinaryDocCodePost", objOGEntity.strInvestmentplantmachinaryDocCodePost,
                                "@P_vchInvestmentplantmachinaryPost", objOGEntity.strInvestmentplantmachinaryPost,
                                "@P_vchBalacingEquipmentDocCodePost", objOGEntity.strBalacingEquipmentDocCodePost,
                                "@P_vchBalacingEquipmentPost", objOGEntity.strBalacingEquipmentPost,
                                "@P_vchServiceProvideDocCodePost", objOGEntity.strServiceProvideDocCodePost,
                                "@P_vchServiceProvidePost", objOGEntity.strServiceProvidePost,
                                "@P_vchPriorityorThrustsectorDocCodePost", objOGEntity.strPriorityorThrustsectorDocCodePost,
                                "@P_vchPriorityorThrustsectorPost", objOGEntity.strPriorityorThrustsectorPost,
                                "@P_vchClearancefromPCBDocCodePost", objOGEntity.strClearancefromPCBDocCodePost,
                                "@P_vchClearancefromPCBPost", objOGEntity.strClearancefromPCBPost,
                                "@P_vchMigrantIndustryunitDocCodePost", objOGEntity.strMigrantIndustryunitDocCodePost,
                                "@P_vchMigrantIndustryunitPost", objOGEntity.strMigrantIndustryunitPost,
                                "@P_vchfixedcapitalinvstDocCodePost", objOGEntity.strfixedcapitalinvstDocCodePost,
                                "@P_vchfixedcapitalinvstPost", objOGEntity.strfixedcapitalinvstPost,
                                "@P_vchEmpoweredCommitteeDocCodePost", objOGEntity.strEmpoweredCommitteeDocCodePost,
                                "@P_vchEmpoweredCommitteePost", objOGEntity.strEmpoweredCommitteePost,
                                "@P_intDirectEmpAfter", objOGEntity.intDirectEmpAfter,
                                "@P_vchPhoneNo", objOGEntity.strPhoneNo,
                                "@P_vchEmail", objOGEntity.strEmail,
                                "@P_OUT_MSG", "OUT"
                                };

                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_ThrustOrPriorityIPR_2022_Draft", out param, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param.ToString();
        }

        public string Stamp_Duty_Exemption_AED(Basic_Unit_Details_Entity objOGEntity)
        {
            try
            {
                object[] arr = {
                                "@P_VCHINCENTIVENO",objOGEntity.strInctFlow,
                                "@P_VCHPROPOSALNO",objOGEntity.strPcNoAfter,
                                "@P_VCHUNITCODE",objOGEntity.strIndustryCode,
                                "@P_INT_USER_ID", objOGEntity.intCreatedBy,
                                "@P_vchEnterpriseName", objOGEntity.strEnterpriseName,
                                "@P_intOrganisationType", objOGEntity.intOrganisationType,                             
                                "@P_intUnitCat", objOGEntity.intUnitCat,
                                "@P_vchRegisteredOfcAddress", objOGEntity.strRegisteredOfcAddress,
                                "@P_vchManagingPartnerGender", objOGEntity.strManagingPartnerGender,
                                "@P_vchManagingPartnerName", objOGEntity.strManagingPartnerName,
                                "@P_vchEINNO", objOGEntity.strEINNO,
                                "@P_dtmEIN", objOGEntity.dtmEIN,
                                "@P_dtmProdCommBefore", objOGEntity.dtmProdCommBefore,
                                "@P_dtmFFCIDateBefore", objOGEntity.dtmFFCIDateBefore,
                                "@P_vchProposedLocation",objOGEntity.strProposedLocation,
                                "@P_vchPrsentStatus",objOGEntity.strPrsentStatus,
                                "@P_vchDeedoragreement",objOGEntity.strDeed,
                                "@P_vchClearnceSWM", objOGEntity.strClearnceswm,
                                "@P_decSdeClaimed",objOGEntity.decSdeClaimed,
                                "@P_decAmountAvailed",objOGEntity.decAmountAvailed,
                                "@P_decDeferentialClaim", objOGEntity.decDeferentialClaim,
                                "@P_XML_TBL_PROD_ITEM_BE", objOGEntity.ProductionItem_BE.SerializeToXMLString(),
                                "@P_vchEINorPEALapprovalDocCode", objOGEntity.strEINorPEALapprovalDocCode,
                                "@P_vchEINorPEALapproval", objOGEntity.strEINorPEALapproval,
                                "@P_vchPworofAttorneyDocCode", objOGEntity.strPworofAttorneyDocCode,
                                "@P_vchPworofAttorney", objOGEntity.strPworofAttorney,
                                "@P_vchCertificateofregistrationDocCode", objOGEntity.strCertificateofregistrationDocCode,
                                "@P_vchCertificateofregistration", objOGEntity.strCertificateofregistration,
                                "@P_vchfixedcapitalinvstDocCode", objOGEntity.strfixedcapitalinvstDocCode,
                                "@P_vchfixedcapitalinvstDocCode", objOGEntity.strfixedcapitalinvst,
                                "@P_vchAppraisalThrustorPriorityDocCode", objOGEntity.strAppraisalThrustorPriorityDocCode,
                                "@P_vchAppraisalThrustorPriority", objOGEntity.strAppraisalThrustorPriority,
                                "@P_vchCertficateofcommproductionDocCode", objOGEntity.strCertficateofcommproductionDocCode,
                                "@P_vchCertficateofcommproduction", objOGEntity.strCertficateofcommproduction,
                                "@P_vchCertficateofmigrationunitDocCode", objOGEntity.strCertficateofmigrationunitDocCode,
                                "@P_vchCertficateofmigrationunit", objOGEntity.strCertficateofmigrationunit,
                                "@P_vchPrivateindustDocCode", objOGEntity.strPrivateindustDocCode,
                                "@P_vchPrivateindust", objOGEntity.strPrivateindust,
                                "@P_vchDeedorAgreementDocCode", objOGEntity.strDeedorAgreementDocCode,
                                "@P_vchDeedorAgreementDoc", objOGEntity.strDeedorAgreement,
                                "@P_vchSupportoftransferunitDocCode", objOGEntity.strSupportoftransferunitDocCode,
                                "@P_vchSupportoftransferunit", objOGEntity.strSupportoftransferunit,
                                "@P_vchProvisionsenunciatedDocCode", objOGEntity.strProvisionsenunciatedDocCode,
                                "@P_vchProvisionsenunciated", objOGEntity.strProvisionsenunciated,
                                "@P_vchValidstatutoryclearancesDocCode", objOGEntity.strValidstatutoryclearancesDocCode,
                                "@P_vchValidstatutoryclearances", objOGEntity.strValidstatutoryclearances,
                                "@P_vchStamppaperdulyDocCode", objOGEntity.strStamppaperdulyDocCode,
                                "@P_vchStamppaperduly", objOGEntity.strStamppaperduly,
                                "@P_vchProvisionalPrioritycetificateDocCode", objOGEntity.strProvisionalPrioritycetificateDocCode,
                                "@P_vchProvisionalPrioritycetificate", objOGEntity.strProvisionalPrioritycetificate,
                                "@P_vchEmpoweredCommitteeDocCode", objOGEntity.strEmpoweredCommitteeDocCode,
                                "@P_vchEmpoweredCommittee", objOGEntity.strEmpoweredCommittee,
                                "@P_OUT_MSG", "OUT"
                                };

                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_StampDutyExemptionIPR_2022_AED", out param, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param.ToString();
        }

        public string Stamp_Duty_Exemption_Draft(Basic_Unit_Details_Entity objOGEntity)
        {
            try
            {
                object[] arr = {
                                "@P_VCHINCENTIVENO",objOGEntity.strInctFlow,
                                "@P_VCHPROPOSALNO",objOGEntity.strPcNoAfter,
                                "@P_VCHUNITCODE",objOGEntity.strIndustryCode,
                                "@P_INT_USER_ID", objOGEntity.intCreatedBy,
                                "@P_vchEnterpriseName", objOGEntity.strEnterpriseName,
                                "@P_intOrganisationType", objOGEntity.intOrganisationType,
                                "@P_intUnitCat", objOGEntity.intUnitCat,
                                "@P_vchRegisteredOfcAddress", objOGEntity.strRegisteredOfcAddress,
                                "@P_vchManagingPartnerGender", objOGEntity.strManagingPartnerGender,
                                "@P_vchManagingPartnerName", objOGEntity.strManagingPartnerName,
                                "@P_vchEINNO", objOGEntity.strEINNO,
                                "@P_dtmEIN", objOGEntity.dtmEIN,
                                "@P_dtmProdCommBefore", objOGEntity.dtmProdCommBefore,
                                "@P_dtmFFCIDateBefore", objOGEntity.dtmFFCIDateBefore,
                                "@P_vchProposedLocation",objOGEntity.strProposedLocation,
                                "@P_vchPrsentStatus",objOGEntity.strPrsentStatus,
                                "@P_vchDeedoragreement",objOGEntity.strDeed,
                                "@P_vchClearnceSWM", objOGEntity.strClearnceswm,
                                "@P_decSdeClaimed",objOGEntity.decSdeClaimed,
                                "@P_decAmountAvailed",objOGEntity.decAmountAvailed,
                                "@P_decDeferentialClaim", objOGEntity.decDeferentialClaim,
                                "@P_XML_TBL_PROD_ITEM_BE", objOGEntity.ProductionItem_BE.SerializeToXMLString(),
                                "@P_vchEINorPEALapprovalDocCode", objOGEntity.strEINorPEALapprovalDocCode,
                                "@P_vchEINorPEALapproval", objOGEntity.strEINorPEALapproval,
                                "@P_vchPworofAttorneyDocCode", objOGEntity.strPworofAttorneyDocCode,
                                "@P_vchPworofAttorney", objOGEntity.strPworofAttorney,
                                "@P_vchCertificateofregistrationDocCode", objOGEntity.strCertificateofregistrationDocCode,
                                "@P_vchCertificateofregistration", objOGEntity.strCertificateofregistration,
                                "@P_vchfixedcapitalinvstDocCode", objOGEntity.strfixedcapitalinvstDocCode,
                                "@P_vchfixedcapitalinvstDocCode", objOGEntity.strfixedcapitalinvst,
                                "@P_vchAppraisalThrustorPriorityDocCode", objOGEntity.strAppraisalThrustorPriorityDocCode,
                                "@P_vchAppraisalThrustorPriority", objOGEntity.strAppraisalThrustorPriority,
                                "@P_vchCertficateofcommproductionDocCode", objOGEntity.strCertficateofcommproductionDocCode,
                                "@P_vchCertficateofcommproduction", objOGEntity.strCertficateofcommproduction,
                                "@P_vchCertficateofmigrationunitDocCode", objOGEntity.strCertficateofmigrationunitDocCode,
                                "@P_vchCertficateofmigrationunit", objOGEntity.strCertficateofmigrationunit,
                                "@P_vchPrivateindustDocCode", objOGEntity.strPrivateindustDocCode,
                                "@P_vchPrivateindust", objOGEntity.strPrivateindust,
                                "@P_vchDeedorAgreementDocCode", objOGEntity.strDeedorAgreementDocCode,
                                "@P_vchDeedorAgreementDoc", objOGEntity.strDeedorAgreement,
                                "@P_vchSupportoftransferunitDocCode", objOGEntity.strSupportoftransferunitDocCode,
                                "@P_vchSupportoftransferunit", objOGEntity.strSupportoftransferunit,
                                "@P_vchProvisionsenunciatedDocCode", objOGEntity.strProvisionsenunciatedDocCode,
                                "@P_vchProvisionsenunciated", objOGEntity.strProvisionsenunciated,
                                "@P_vchValidstatutoryclearancesDocCode", objOGEntity.strValidstatutoryclearancesDocCode,
                                "@P_vchValidstatutoryclearances", objOGEntity.strValidstatutoryclearances,
                                "@P_vchStamppaperdulyDocCode", objOGEntity.strStamppaperdulyDocCode,
                                "@P_vchStamppaperduly", objOGEntity.strStamppaperduly,
                                "@P_vchProvisionalPrioritycetificateDocCode", objOGEntity.strProvisionalPrioritycetificateDocCode,
                                "@P_vchProvisionalPrioritycetificate", objOGEntity.strProvisionalPrioritycetificate,
                                "@P_vchEmpoweredCommitteeDocCode", objOGEntity.strEmpoweredCommitteeDocCode,
                                "@P_vchEmpoweredCommittee", objOGEntity.strEmpoweredCommittee,
                                "@P_OUT_MSG", "OUT"
                                };

                int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_StampDutyExemptionIPR_2022_Draft", out param, arr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return param.ToString();
        }

    }
}
        #endregion


       


    
    

   
