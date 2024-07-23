using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using EntityLayer.Proposal;
using System.Data;

namespace DataAcessLayer.Proposal
{
    public class ProposalDataLayer
    {
        string str_Retvalue = "";
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

        #region Added By Pradeep Kumar sahoo
        #region Add Land details
        public string ProposalLandAED(LandDet objProposal)
        {
            List<LandDet> list = new List<LandDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_LandAndUtility";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@PAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PintLandId", objProposal.intLandId);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);
                cmd.Parameters.AddWithValue("@PbitLandRequired", objProposal.bitLandRequired);
                cmd.Parameters.AddWithValue("@PintDistrictId", objProposal.intDistrictId);
                cmd.Parameters.AddWithValue("@PintRecomDistrictId", objProposal.intRecomendDistrict);
                cmd.Parameters.AddWithValue("@PintBlockId", objProposal.intBlockId);
                cmd.Parameters.AddWithValue("@PintRecomBlockId", objProposal.intRecomendBlock);
                cmd.Parameters.AddWithValue("@PdecExtendLand", objProposal.decExtendLand);
                cmd.Parameters.AddWithValue("@PsintLandRequiredIDCO", objProposal.sintLandRequiredIDCO);
                cmd.Parameters.AddWithValue("@PvchIDCOInustrialName", objProposal.vchIDCOInustrialName);
                cmd.Parameters.AddWithValue("@PsintLandAcquiredIDCO", objProposal.sintLandAcquiredIDCO);
                cmd.Parameters.AddWithValue("@PbitGridSource", objProposal.bitGridSource);
                cmd.Parameters.AddWithValue("@PbitCppSource", objProposal.bitCppSource);
                cmd.Parameters.AddWithValue("@PdecPowerDemandGrid", objProposal.decPowerDemandGrid);
                cmd.Parameters.AddWithValue("@PdecPowerDrawalCPP", objProposal.decPowerDrawalCPP);
                cmd.Parameters.AddWithValue("@PdecCapacityofCPPPlant", objProposal.decCapacityofCPPPlant);
                cmd.Parameters.AddWithValue("@PdecWaterRequireExist", objProposal.decWaterRequireExist);
                cmd.Parameters.AddWithValue("@PdecWaterReqireProposed", objProposal.decWaterReqireProposed);
                cmd.Parameters.AddWithValue("@PdecWaterRequirProduct", objProposal.decWaterRequirProduct);
                cmd.Parameters.AddWithValue("@PvchSurfaceWater", objProposal.vchSurfaceWater);
                cmd.Parameters.AddWithValue("@PvchIdcoSupply", objProposal.vchIdcoSupply);
                cmd.Parameters.AddWithValue("@PvchRainWtrHarvesting", objProposal.vchRainWtrHarvesting);
                cmd.Parameters.AddWithValue("@Pvchother", objProposal.vchother);
                cmd.Parameters.AddWithValue("@PvchOtherSpecify", objProposal.vchOtherSpecify);
                //cmd.Parameters.AddWithValue("@PbitAdoptionWater", objProposal.bitAdoptionWater);
                cmd.Parameters.AddWithValue("@PvchQuntRecyllingWaste", objProposal.vchQuntRecyllingWaste);
                cmd.Parameters.AddWithValue("@PvchWasteConserFile", objProposal.vchWasteConserFile);
                cmd.Parameters.AddWithValue("@PvchWaterHazardousFile", objProposal.vchWaterHazardousFile);
                cmd.Parameters.AddWithValue("@PvchProjectLayout", objProposal.strProjectLayOut);
                cmd.Parameters.AddWithValue("@PvchLandUseStmt", objProposal.strProjectLandStmt);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@PbitIppSource", objProposal.BitIppSource); ///// Added by Sushant Jena On Dt 24-Aug-2021
                cmd.Parameters.AddWithValue("@PdecPowerProducerIPP", objProposal.DecPowerProducerIpp); ///// Added by Sushant Jena On Dt 24-Aug-2021

                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                str_Retvalue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }

            return str_Retvalue;
        }

        public string UpdateLandApproval(LandDet objProposal)
        {
            List<LandDet> list = new List<LandDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_LAND_CHANG_DIST_BLOCK";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);
                cmd.Parameters.AddWithValue("@PintDistrictId", objProposal.intDistrictId);
                cmd.Parameters.AddWithValue("@PintBlockId", objProposal.intBlockId);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@vchSectorIdIT", System.Configuration.ConfigurationManager.AppSettings["SectorIdIT"]);
                cmd.Parameters.AddWithValue("@vchSectorIdTourism", System.Configuration.ConfigurationManager.AppSettings["SectorIdTOURISM"]);
                cmd.Parameters.AddWithValue("@intITdeptId", System.Configuration.ConfigurationManager.AppSettings["DeptIdIT"]);
                cmd.Parameters.AddWithValue("@intTourismdeptId", System.Configuration.ConfigurationManager.AppSettings["DeptIdTOURISM"]);
                cmd.Parameters.AddWithValue("@intIPICOLId", System.Configuration.ConfigurationManager.AppSettings["DeptIdIPICOL"]);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                str_Retvalue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();

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
                cmd.Dispose();
            }
            return str_Retvalue;
        }
        #endregion
        #region Get Land details
        public List<LandDet> ViewLandDetails(LandDet objLand)
        {

            List<LandDet> list = new List<LandDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_LandAndUtility";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objLand.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objLand.vchProposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        LandDet objlndDet = new LandDet();

                        objlndDet.bitLandRequired = Convert.ToBoolean(sqlReader["bitLandRequired"]);
                        objlndDet.intDistrictId = Convert.ToInt32(sqlReader["intDistrictId"]);
                        objlndDet.intBlockId = Convert.ToInt32(sqlReader["intBlockId"]);
                        objlndDet.decExtendLand = Convert.ToDecimal(sqlReader["decExtendLand"]);
                        objlndDet.sintLandRequiredIDCO = Convert.ToInt32(sqlReader["sintLandRequiredIDCO"]);
                        objlndDet.vchIDCOInustrialName = Convert.ToString(sqlReader["vchIDCOInustrialName"]);
                        objlndDet.sintLandAcquiredIDCO = Convert.ToInt32(sqlReader["sintLandAcquiredIDCO"]);
                        objlndDet.bitGridSource = Convert.ToBoolean(sqlReader["bitGridSource"]);
                        objlndDet.bitCppSource = Convert.ToBoolean(sqlReader["bitCppSource"]);
                        objlndDet.decPowerDemandGrid = Convert.ToDecimal(sqlReader["decPowerDemandGrid"]);
                        objlndDet.decPowerDrawalCPP = Convert.ToDecimal(sqlReader["decPowerDrawalCPP"]);
                        objlndDet.decCapacityofCPPPlant = Convert.ToDecimal(sqlReader["decCapacityofCPPPlant"]);
                        objlndDet.decWaterRequireExist = Convert.ToDecimal(sqlReader["decWaterRequireExist"]);
                        objlndDet.decWaterReqireProposed = Convert.ToDecimal(sqlReader["decWaterReqireProposed"]);
                        objlndDet.decWaterRequirProduct = Convert.ToDecimal(sqlReader["decWaterRequirProduct"]);
                        objlndDet.vchSurfaceWater = Convert.ToString(sqlReader["vchSurfaceWater"]);
                        objlndDet.vchIdcoSupply = Convert.ToString(sqlReader["vchIdcoSupply"]);
                        objlndDet.vchRainWtrHarvesting = Convert.ToString(sqlReader["vchRainWtrHarvesting"]);
                        objlndDet.vchother = Convert.ToString(sqlReader["vchother"]);
                        objlndDet.vchOtherSpecify = Convert.ToString(sqlReader["vchOtherSpecify"]);
                        objlndDet.vchQuntRecyllingWaste = Convert.ToString(sqlReader["vchQuntRecyllingWaste"]);
                        objlndDet.vchWasteConserFile = Convert.ToString(sqlReader["vchWasteConserFile"]);
                        objlndDet.vchWaterHazardousFile = Convert.ToString(sqlReader["vchWaterHazardousFile"]);
                        objlndDet.strProjectLandStmt = Convert.ToString(sqlReader["vchLandUseStmt"]);
                        objlndDet.strProjectLayOut = Convert.ToString(sqlReader["vchProjectLayout"]);

                        objlndDet.BitIppSource = Convert.ToBoolean(sqlReader["bitIppSource"]); ///// Added by Sushant Jena On Dt 24-Aug-2021
                        objlndDet.DecPowerProducerIpp = Convert.ToDecimal(sqlReader["decPowerProducerIPP"]); ///// Added by Sushant Jena On Dt 24-Aug-2021
                      
                        list.Add(objlndDet);
                    }

                }
                sqlReader.Close();
                return list;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }

        }
        #endregion
        public List<LandDet> Industrial(LandDet objprop)
        {
            List<LandDet> objList = new List<LandDet>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_LandAndUtility";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objprop.strAction);
                cmd.Parameters.AddWithValue("@PintDistrictId", objprop.vchProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        LandDet objlndDet = new LandDet();
                        if (objprop.strAction == "I")
                        {
                            objlndDet.vchIndustrialName = Convert.ToString(sqlReader["vchIndustrialName"]);
                            objlndDet.intIndustrialEstateId = Convert.ToInt32(sqlReader["intIndustrialEstateId"]);
                        }
                        objList.Add(objlndDet);

                    }

                }
                sqlReader.Close();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return objList;
        }

        public List<LandDet> GETMobileNo(LandDet objprop)
        {
            List<LandDet> objList = new List<LandDet>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_LandAndUtility";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objprop.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objprop.vchProposalNo);
                cmd.Parameters.AddWithValue("@P_PNO", objprop.ApplicationNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        LandDet objlndDet = new LandDet();
                        if (objprop.strAction == "M")
                        {
                            objlndDet.MobileNo = Convert.ToString(sqlReader["vchCorMobileNo"]);
                            objlndDet.SMSContent = Convert.ToString(sqlReader["SMSContent"]);
                            objlndDet.Email = Convert.ToString(sqlReader["vchEmail"]);
                        }
                        if (objprop.strAction == "N")
                        {
                            objlndDet.MobileNo = Convert.ToString(sqlReader["vchCorMobileNo"]);
                            objlndDet.SMSContent = Convert.ToString(sqlReader["SMSContent"]);
                            objlndDet.Email = Convert.ToString(sqlReader["vchEmail"]);
                        }
                        if (objprop.strAction == "O")
                        {
                            objlndDet.MobileNo = Convert.ToString(sqlReader["vchCorMobileNo"]);
                            objlndDet.SMSContent = Convert.ToString(sqlReader["SMSContent"]);
                            objlndDet.Email = Convert.ToString(sqlReader["vchEmail"]);
                        }
                        if (objprop.strAction == "R")
                        {
                            objlndDet.LandAprvByIPICOL = Convert.ToString(sqlReader["decExtendLand"]);

                        }
                        objList.Add(objlndDet);

                    }

                }
                sqlReader.Close();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return objList;
        }
        public List<LandDet> GETMobileNoOfUser(LandDet objprop)
        {
            List<LandDet> objList = new List<LandDet>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_LandAndUtility";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objprop.strAction);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objprop.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_PNO", objprop.ApplicationNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        LandDet objlndDet = new LandDet();

                        if (objprop.strAction == "P")
                        {
                            objlndDet.MobileNo = Convert.ToString(sqlReader["vchMobTel"]);
                            objlndDet.SMSContent = Convert.ToString(sqlReader["SMSContent"]);
                            objlndDet.Email = Convert.ToString(sqlReader["vchEmail"]);
                        }
                        if (objprop.strAction == "D")
                        {
                            objlndDet.MobileNo = Convert.ToString(sqlReader["vchMobTel"]);
                            objlndDet.SMSContent = Convert.ToString(sqlReader["SMSContent"]);
                            objlndDet.Email = Convert.ToString(sqlReader["vchEmail"]);
                        }

                        objList.Add(objlndDet);

                    }

                }
                sqlReader.Close();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return objList;
        }
        public List<LandDet> GETApplicationNoByOrderNo(LandDet objprop)
        {
            List<LandDet> objList = new List<LandDet>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_LandAndUtility";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objprop.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_ORDERNO", objprop.OrderNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        LandDet objlndDet = new LandDet();
                        if (objprop.strAction == "Q")
                        {
                            objlndDet.ApplicationNo = Convert.ToString(sqlReader["vchApplicationNo"]);
                        }

                        objList.Add(objlndDet);
                    }

                }
                sqlReader.Close();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return objList;
        }

        //public string IntermidiateService(PromoterDet objprop)
        //public List<LandDet> IntermidiateService(PromoterDet objprop)
        // {
        //     List<PromoterDet> objList = new List<PromoterDet>();
        //     SqlCommand cmd = new SqlCommand();
        //     try
        //     {
        //         cmd.Connection = conn;
        //         cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //         cmd.CommandText = "USP_InterMidiate";
        //         cmd.Parameters.Clear();
        //         cmd.Parameters.AddWithValue("@PAction", objprop.strAction);
        //         cmd.Parameters.AddWithValue("@PvchProposalNo", objprop.vchProposalNo);
        //         SqlParameter OutPut = new SqlParameter("@P_OUT_MSG", SqlDbType.VarChar, 100) { Direction = ParameterDirection.Output };
        //         cmd.Parameters.Add(OutPut);
        //         if (conn.State == ConnectionState.Closed)
        //         {
        //             conn.Open();
        //         }
        //         //Open the connection and execute the query
        //         cmd.ExecuteNonQuery();
        //         obllist = OutPut.Value.ToString();
        //     }
        //     catch (NullReferenceException ex) { throw ex; }
        //     catch (Exception ex)
        //     { throw ex; }
        //     finally { cmd = null; }
        //     return str_Retvalue;
        // }
        public List<PromoterDet> Intermidiate(PromoterDet objprop)
        {
            List<PromoterDet> objList = new List<PromoterDet>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_InterMidiate";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objprop.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objprop.vchProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        PromoterDet objlndDet = new PromoterDet();
                        if (objprop.strAction == "C")
                        {
                            objlndDet.vchProposalNo = Convert.ToString(sqlReader["vchProposalNo"]);

                        }
                        objList.Add(objlndDet);

                    }

                }


            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return objList;
        }

        #endregion

        public string ProposalESIGNPromoterAED(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);
                cmd.Parameters.AddWithValue("@P_AGREEMENTID", objProposal.vchCompName);
                cmd.Parameters.AddWithValue("@P_ACCESSTOKEN", objProposal.vchAddress);
                cmd.Parameters.AddWithValue("@P_vcheSignFileLink", objProposal.vchCorAdd);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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

        #region Added By Subhasmita Behera on 27-Jul-2017

        #region Add promoter details

        public string ProposalPromoterAED(PromoterDet objProposal)
        {
            string Str_RetValue = "";
            List<PromoterDet> list = new List<PromoterDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);
                cmd.Parameters.AddWithValue("@PvchCompName", objProposal.vchCompName);
                cmd.Parameters.AddWithValue("@PvchAddress", objProposal.vchAddress);
                cmd.Parameters.AddWithValue("@PintCountry", objProposal.intCountry);
                cmd.Parameters.AddWithValue("@PintState", objProposal.intState);
                cmd.Parameters.AddWithValue("@PvchCity", objProposal.vchCity);
                cmd.Parameters.AddWithValue("@PintPin", objProposal.intPin);
                cmd.Parameters.AddWithValue("@PvchPhoneNo", objProposal.vchPhoneNo);
                cmd.Parameters.AddWithValue("@PvchFaxNo", objProposal.vchFaxNo);
                cmd.Parameters.AddWithValue("@PvchEmail", objProposal.vchEmail);
                cmd.Parameters.AddWithValue("@PbitAddresSameAsCorp", objProposal.bitAddresSameAsCorp);
                cmd.Parameters.AddWithValue("@PvchContactPerson", objProposal.vchContactPerson);
                cmd.Parameters.AddWithValue("@PvchCorAdd", objProposal.vchCorAdd);
                cmd.Parameters.AddWithValue("@PintCorCountry", objProposal.intCorCountry);
                cmd.Parameters.AddWithValue("@PintCorState", objProposal.intCorState);
                cmd.Parameters.AddWithValue("@PvchCorCity", objProposal.vchCorCity);
                cmd.Parameters.AddWithValue("@PintCorPin", objProposal.intCorPin);
                cmd.Parameters.AddWithValue("@PvchCorMobileNo", objProposal.vchCorMobileNo);
                cmd.Parameters.AddWithValue("@PvchCorFaxNo", objProposal.vchCorFaxNo);
                cmd.Parameters.AddWithValue("@PvchCorEmail", objProposal.vchCorEmail);
                cmd.Parameters.AddWithValue("@PintConstitution", objProposal.intConstitution);
                cmd.Parameters.AddWithValue("@PvchOtheConstituition", objProposal.vchOtheConstituition);
                cmd.Parameters.AddWithValue("@PintYearOfIncorporation", objProposal.intYearOfIncorporation);
                cmd.Parameters.AddWithValue("@PvchPlaceIncor", objProposal.vchPlaceIncor);
                cmd.Parameters.AddWithValue("@PvchGSTIN", objProposal.vchGSTIN);
                cmd.Parameters.AddWithValue("@PintProjectType", objProposal.intProjectType);
                cmd.Parameters.AddWithValue("@PintApplicationFor", objProposal.intApplicationFor);
                cmd.Parameters.AddWithValue("@PintNumberOfPartner", objProposal.intNumberOfPartner);
                cmd.Parameters.AddWithValue("@PvchManagPartner", objProposal.vchManagPartner);
                cmd.Parameters.AddWithValue("@PdecAnnulTurnOvr1", objProposal.decAnnulTurnOvr1);
                cmd.Parameters.AddWithValue("@PdecAnnulTurnOvr2", objProposal.decAnnulTurnOvr2);
                cmd.Parameters.AddWithValue("@PdecAnnulTurnOvr3", objProposal.decAnnulTurnOvr3);
                cmd.Parameters.AddWithValue("@PdecProfitAftrTx1", objProposal.decProfitAftrTx1);
                cmd.Parameters.AddWithValue("@PdecProfitAftrTx2", objProposal.decProfitAftrTx2);
                cmd.Parameters.AddWithValue("@PdecProfitAftrTx3", objProposal.decProfitAftrTx3);
                cmd.Parameters.AddWithValue("@PdecNetWorth1", objProposal.decNetWorth1);
                cmd.Parameters.AddWithValue("@PdecNetWorth2", objProposal.decNetWorth2);
                cmd.Parameters.AddWithValue("@PdecNetWorth3", objProposal.decNetWorth3);
                cmd.Parameters.AddWithValue("@PdecResvSurp1", objProposal.decResvSurp1);
                cmd.Parameters.AddWithValue("@PdecResvSurp2", objProposal.decResvSurp2);
                cmd.Parameters.AddWithValue("@PdecResvSurp3", objProposal.decResvSurp3);
                cmd.Parameters.AddWithValue("@PdecShareCap1", objProposal.decShareCap1);
                cmd.Parameters.AddWithValue("@PdecShareCap2", objProposal.decShareCap2);
                cmd.Parameters.AddWithValue("@PdecShareCap3", objProposal.decShareCap3);
                cmd.Parameters.AddWithValue("@PintEduQualif", objProposal.intEduQualif);
                cmd.Parameters.AddWithValue("@PintTecQualif", objProposal.intTecQualif);
                cmd.Parameters.AddWithValue("@PintExpInYr", objProposal.intExpInYr);
                cmd.Parameters.AddWithValue("@PintExisDistrict", objProposal.intExisDistrict);
                cmd.Parameters.AddWithValue("@PintExisBlock", objProposal.intExisBlock);
                cmd.Parameters.AddWithValue("@PintAllotedBy", objProposal.intAllotedBy);
                cmd.Parameters.AddWithValue("@PvchlandInAcres", objProposal.vchlandInAcres);
                cmd.Parameters.AddWithValue("@PvchNatureAct", objProposal.vchNatureAct);

                cmd.Parameters.AddWithValue("@PintSectorId", objProposal.intSectorId);
                cmd.Parameters.AddWithValue("@PintSubSectorId", objProposal.intSubSectorId);
                cmd.Parameters.AddWithValue("@PvchCapacity", objProposal.vchCapacity);

                cmd.Parameters.AddWithValue("@PintCapacityUnit", objProposal.intCapacityUnit);
                cmd.Parameters.AddWithValue("@PvchOther", objProposal.vchOther);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@PintUpdatedBy", objProposal.intUpdatedBy);
                cmd.Parameters.AddWithValue("@PintPromoterId", objProposal.intPromoterId);
                cmd.Parameters.AddWithValue("@P_XML_Data", objProposal.strXML_Data);
                cmd.Parameters.AddWithValue("@P_XML_BD_Data", objProposal.strXML_BD_Data);
                cmd.Parameters.AddWithValue("@P_XML_RWM_Data", objProposal.strXML_RWM_Data);
                cmd.Parameters.AddWithValue("@PintFyn1", objProposal.intFyn1);
                cmd.Parameters.AddWithValue("@PintFyn2", objProposal.intFyn2);
                cmd.Parameters.AddWithValue("@PintFyn3", objProposal.intFyn3);
                cmd.Parameters.AddWithValue("@PvchPanfile", objProposal.vchPanfile);
                cmd.Parameters.AddWithValue("@PvchGSTNfile", objProposal.vchGSTNfile);
                cmd.Parameters.AddWithValue("@PvchMemorandumfile", objProposal.vchMemorandumfile);
                cmd.Parameters.AddWithValue("@PvchCertificateincorpfile", objProposal.vchCertificateincorpfile);
                cmd.Parameters.AddWithValue("@PvchEduQualifile", objProposal.vchEduQualifile);
                cmd.Parameters.AddWithValue("@PvchTechniQualifile", objProposal.vchTechniQualifile);
                cmd.Parameters.AddWithValue("@PvchExpFile", objProposal.vchExpFile);
                cmd.Parameters.AddWithValue("@PvchAuditFile", objProposal.vchAuditFile);
                cmd.Parameters.AddWithValue("@PvchNetWorthfile", objProposal.vchNetWorthfile);
                cmd.Parameters.AddWithValue("@PvchNameOfPromoter", objProposal.vchNameOfPromoter);
                cmd.Parameters.AddWithValue("@PvchExisIndName", objProposal.vchExisIndName);
                cmd.Parameters.AddWithValue("@PintCordist", objProposal.intCordist);
                cmd.Parameters.AddWithValue("@PvchOtherState", objProposal.vchOtherState);
                cmd.Parameters.AddWithValue("@PvchOtherStateCor", objProposal.vchOtherStateCor);
                cmd.Parameters.AddWithValue("@PvchAuditFileSecondYr", objProposal.vchAuditFileSecondYrs);
                cmd.Parameters.AddWithValue("@PvchAuditFileThrdYr", objProposal.vchAuditFileThrdYrs);
                cmd.Parameters.AddWithValue("@PvchPHN", objProposal.intISDPHNo);
                cmd.Parameters.AddWithValue("@PvchFXN", objProposal.intISDFXNo);
                cmd.Parameters.AddWithValue("@PvchMOBN", objProposal.intISDMOBo);
                cmd.Parameters.AddWithValue("@PvchFaxCorDet", objProposal.intFaxCordet);
                cmd.Parameters.AddWithValue("@PintPhoneStateCode", objProposal.PhoneStateCode);
                cmd.Parameters.AddWithValue("@PintTagto", objProposal.Tagtodet);
                cmd.Parameters.AddWithValue("@PvchAuditFileFourthYr", objProposal.strIncomeTaxReturn);
                //cmd.Parameters.AddWithValue("@P_XML_GC_NET_WORTH", objProposal.strXmlGcNetWorth); //// Added by Sushant Jena On Dt:27-Aug-2019

                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                return Str_RetValue;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        public string ProposalAMStatusUpdate(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_Declartion_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);
                cmd.Parameters.AddWithValue("@intAMSForwardSts", objProposal.intFowardAMS);
                cmd.Parameters.AddWithValue("@vchNodalOfficerName", objProposal.strNodalOfcrName);
                cmd.Parameters.AddWithValue("@intNodalOfcrId", objProposal.intNodalOfcrID);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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
        public string ProposalIDCOtatusUpdate(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_Declartion_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);
                cmd.Parameters.AddWithValue("@intIDCOForwardSts", objProposal.intForwardIDCO);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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

        #region Get promoter details

        public List<PromoterDet> GetCompanyDetails(PromoterDet objProposal)
        {

            List<PromoterDet> list = new List<PromoterDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        PromoterDet objProp = new PromoterDet();
                        objProp.vchCompName = Convert.ToString(sqlReader["vchCompName"]);
                        objProp.vchAddress = Convert.ToString(sqlReader["vchAddress"]);
                        objProp.intCountry = Convert.ToInt32(sqlReader["intCountry"]);
                        objProp.intState = Convert.ToInt32(sqlReader["intState"]);
                        objProp.vchCity = Convert.ToString(sqlReader["vchCity"]);
                        objProp.intPin = Convert.ToInt32(sqlReader["intPin"]);
                        objProp.vchPhoneNo = Convert.ToString(sqlReader["vchPhoneNo"]);
                        objProp.vchFaxNo = Convert.ToString(sqlReader["vchFaxNo"]);
                        objProp.vchEmail = Convert.ToString(sqlReader["vchEmail"]);
                        objProp.bitAddresSameAsCorp = Convert.ToInt32(sqlReader["bitAddresSameAsCorp"]);
                        objProp.vchContactPerson = Convert.ToString(sqlReader["vchContactPerson"]);
                        objProp.vchCorAdd = Convert.ToString(sqlReader["vchCorAdd"]);
                        objProp.intCorCountry = Convert.ToInt32(sqlReader["intCorCountry"]);
                        objProp.intCorState = Convert.ToInt32(sqlReader["intCorState"]);
                        objProp.vchCorCity = Convert.ToString(sqlReader["vchCorCity"]);
                        objProp.intCorPin = Convert.ToInt32(sqlReader["intCorPin"]);
                        objProp.vchCorMobileNo = Convert.ToString(sqlReader["vchCorMobileNo"]);
                        objProp.vchCorFaxNo = Convert.ToString(sqlReader["vchCorFaxNo"]);
                        objProp.vchCorEmail = Convert.ToString(sqlReader["vchCorEmail"]);
                        objProp.intConstitution = Convert.ToInt32(sqlReader["intConstitution"]);
                        objProp.vchOtheConstituition = Convert.ToString(sqlReader["vchOtheConstituition"]);
                        objProp.intYearOfIncorporation = Convert.ToString(sqlReader["intYearOfIncorporation"]);
                        objProp.vchPlaceIncor = Convert.ToString(sqlReader["vchPlaceIncor"]);
                        objProp.vchGSTIN = Convert.ToString(sqlReader["vchGSTIN"]);
                        objProp.intProjectType = Convert.ToInt32(sqlReader["intProjectType"]);
                        objProp.intApplicationFor = Convert.ToInt32(sqlReader["intApplicationFor"]);
                        objProp.intNumberOfPartner = Convert.ToInt32(sqlReader["intNumberOfPartner"]);
                        objProp.vchManagPartner = Convert.ToString(sqlReader["vchManagPartner"]);
                        objProp.decAnnulTurnOvr1 = Convert.ToString(sqlReader["decAnnulTurnOvr1"]);
                        objProp.decAnnulTurnOvr2 = Convert.ToString(sqlReader["decAnnulTurnOvr2"]);
                        objProp.decAnnulTurnOvr3 = Convert.ToString(sqlReader["decAnnulTurnOvr3"]);
                        objProp.decProfitAftrTx1 = Convert.ToString(sqlReader["decProfitAftrTx1"]);
                        objProp.decProfitAftrTx2 = Convert.ToString(sqlReader["decProfitAftrTx2"]);
                        objProp.decProfitAftrTx3 = Convert.ToString(sqlReader["decProfitAftrTx3"]);
                        objProp.decNetWorth1 = Convert.ToString(sqlReader["decNetWorth1"]);
                        objProp.decNetWorth2 = Convert.ToString(sqlReader["decNetWorth2"]);
                        objProp.decNetWorth3 = Convert.ToString(sqlReader["decNetWorth3"]);
                        objProp.decResvSurp1 = Convert.ToString(sqlReader["decResvSurp1"]);
                        objProp.decResvSurp2 = Convert.ToString(sqlReader["decResvSurp2"]);
                        objProp.decResvSurp3 = Convert.ToString(sqlReader["decResvSurp3"]);
                        objProp.decShareCap1 = Convert.ToString(sqlReader["decShareCap1"]);
                        objProp.decShareCap2 = Convert.ToString(sqlReader["decShareCap2"]);
                        objProp.decShareCap3 = Convert.ToString(sqlReader["decShareCap3"]);
                        objProp.intEduQualif = Convert.ToInt32(sqlReader["intEduQualif"]);
                        objProp.intTecQualif = Convert.ToInt32(sqlReader["intTecQualif"]);
                        objProp.intExpInYr = Convert.ToInt32(sqlReader["intExpInYr"]);
                        objProp.vchExisIndName = Convert.ToString(sqlReader["vchExisIndName"]);
                        objProp.intExisDistrict = Convert.ToInt32(sqlReader["intExisDistrict"]);
                        objProp.intExisBlock = Convert.ToInt32(sqlReader["intExisBlock"]);
                        objProp.intAllotedBy = Convert.ToInt32(sqlReader["intAllotedBy"]);
                        objProp.vchlandInAcres = Convert.ToString(sqlReader["vchlandInAcres"]);
                        objProp.vchNatureAct = Convert.ToString(sqlReader["vchNatureAct"]);
                        objProp.intSectorId = Convert.ToInt32(sqlReader["intSectorId"]);
                        objProp.intSubSectorId = Convert.ToInt32(sqlReader["intSubSectorId"]);
                        objProp.vchCapacity = Convert.ToString(sqlReader["vchCapacity"]);
                        objProp.intCapacityUnit = Convert.ToInt32(sqlReader["intCapacityUnit"]);
                        objProp.vchOther = Convert.ToString(sqlReader["vchOther"]);
                        objProp.intPromoterId = Convert.ToInt32(sqlReader["intPromoterId"]);
                        objProp.intFyn1 = Convert.ToInt32(sqlReader["intFyn1"]);
                        objProp.intFyn2 = Convert.ToInt32(sqlReader["intFyn2"]);
                        objProp.intFyn3 = Convert.ToInt32(sqlReader["intFyn3"]);
                        objProp.intCordist = Convert.ToInt32(sqlReader["intCordist"]);
                        objProp.vchNameOfPromoter = Convert.ToString(sqlReader["vchNameOfPromoter"]);
                        objProp.vchOtherState = Convert.ToString(sqlReader["vchOtherState"]);
                        objProp.vchOtherStateCor = Convert.ToString(sqlReader["vchOtherStateCor"]);
                        objProp.intISDPHNo = Convert.ToInt32(sqlReader["intISDPHNo"]);
                        objProp.intISDFXNo = Convert.ToInt32(sqlReader["intISDFXNo"]);
                        objProp.intISDMOBo = Convert.ToInt32(sqlReader["intISDMOBo"]);
                        objProp.intFaxCordet = Convert.ToInt32(sqlReader["intFaxCordet"]);
                        objProp.PhoneStateCode = Convert.ToInt32(sqlReader["PhoneStateCode"]);
                        objProp.Tagtodet = Convert.ToInt32(sqlReader["Tagtodet"]);
                        list.Add(objProp);
                    }
                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        public List<PromoterDet> GetPromoterNameDetails(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        PromoterDet objProp = new PromoterDet();
                        objProp.vchNameOfPromoter = Convert.ToString(sqlReader["vchNameOfPromoter"]);
                        objProp.intProId = Convert.ToInt32(sqlReader["intProId"]);
                        list.Add(objProp);
                    }
                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        public List<PromoterDet> GetNameDesgDetails(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        PromoterDet objProp = new PromoterDet();
                        objProp.vchName = Convert.ToString(sqlReader["vchName"]);
                        objProp.vchDesignation = Convert.ToString(sqlReader["vchDesignation"]);
                        objProp.intProId1 = Convert.ToInt32(sqlReader["intProId1"]);
                        list.Add(objProp);
                    }
                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        public List<PromoterDet> GetRawMetrialDetails(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        PromoterDet objProp = new PromoterDet();
                        objProp.vchRawMaterial = Convert.ToString(sqlReader["vchRawMaterial"]);
                        objProp.vchRawMeterialSrc = Convert.ToString(sqlReader["vchRawMeterialSrc"]);
                        objProp.intProId2 = Convert.ToInt32(sqlReader["intProId2"]);
                        list.Add(objProp);
                    }
                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        public List<PromoterDet> GetEnclosureDetails(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        PromoterDet objProp = new PromoterDet();
                        objProp.vchPanfile = Convert.ToString(sqlReader["vchPanfile"]);
                        objProp.vchGSTNfile = Convert.ToString(sqlReader["vchGSTNfile"]);
                        objProp.vchMemorandumfile = Convert.ToString(sqlReader["vchMemorandumfile"]);
                        objProp.vchCertificateincorpfile = Convert.ToString(sqlReader["vchCertificateincorpfile"]);
                        objProp.vchEduQualifile = Convert.ToString(sqlReader["vchEduQualifile"]);
                        objProp.vchTechniQualifile = Convert.ToString(sqlReader["vchTechniQualifile"]);
                        objProp.vchExpFile = Convert.ToString(sqlReader["vchExpFile"]);
                        objProp.vchAuditFile = Convert.ToString(sqlReader["vchAuditFile"]);
                        objProp.vchNetWorthfile = Convert.ToString(sqlReader["vchNetWorthfile"]);
                        objProp.vchAuditFileSecondYrs = Convert.ToString(sqlReader["vchAuditFileSecondYrs"]);
                        objProp.vchAuditFileThrdYrs = Convert.ToString(sqlReader["vchAuditFileThrdYrs"]);
                        objProp.strIncomeTaxReturn = Convert.ToString(sqlReader["vchIncomeTaxReturn"]);
                        list.Add(objProp);
                    }
                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }

        public DataTable GetGcNewWorthDetails(ProjectInfo objProposal) //// Added by Sushant Jena On Dt:27-Aug-2019
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            return dt;
        }
        public DataTable GetTotalNetWorth(PromoterDet objProposal)//// Added by Sushant Jena On Dt:27-Aug-2019
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            return dt;
        }

        #endregion

        public List<CNET> GetCNETCompanyDetails(CNET objProposal)
        {
            List<CNET> list = new List<CNET>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                CNET objProp = new CNET();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", "V");
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {

                        string guid = System.Guid.NewGuid().ToString();
                        objProp.vchProposalNo = objProposal.vchProposalNo;
                        objProp.gui_start_value_from_swp = guid;
                        objProp.gui_end_value_from_swp = guid;
                        //objProp.industry_tahasil = "aaa";
                        objProp.project_coming_under = Convert.ToString(sqlReader["project_coming_under"]);
                        objProp.company_project_type = Convert.ToString(sqlReader["company_project_type"]);
                        objProp.total_area = Convert.ToString(sqlReader["total_area"]);

                        objProp.unique_application_id_from_swp = Convert.ToString(sqlReader["unique_application_id_from_swp"]);
                        objProp.industry_code = Convert.ToString(sqlReader["industry_code"]);
                        objProp.company_name = Convert.ToString(sqlReader["company_name"]);
                        objProp.company_address = Convert.ToString(sqlReader["company_address"]);
                        objProp.company_country = Convert.ToString(sqlReader["company_country"]);
                        objProp.company_state = Convert.ToString(sqlReader["company_state"]);
                        objProp.company_city = Convert.ToString(sqlReader["company_city"]);
                        objProp.company_phnumber = Convert.ToString(sqlReader["company_phnumber"]);
                        if (sqlReader["company_faxnumber"] == "")
                        {
                            objProp.company_faxnumber = "0";
                        }
                        else
                        {
                            objProp.company_faxnumber = Convert.ToString(sqlReader["company_faxnumber"]);
                        }
                        objProp.company_email = Convert.ToString(sqlReader["company_email"]);
                        objProp.company_pincode = Convert.ToString(sqlReader["company_pincode"]);
                        objProp.company_gst_no = Convert.ToString(sqlReader["company_gst_no"]);
                        objProp.company_application_for = Convert.ToString(sqlReader["company_application_for"]);
                        objProp.company_year_of_incorporation = Convert.ToString(sqlReader["company_year_of_incorporation"]);
                        objProp.company_place_of_incorporation = Convert.ToString(sqlReader["company_place_of_incorporation"]);
                        objProp.educational_qualification = Convert.ToString(sqlReader["educational_qualification"]);
                        objProp.technical_qualification = Convert.ToString(sqlReader["technical_qualification"]);
                        objProp.company_contact_person_name = Convert.ToString(sqlReader["company_contact_person_name"]);
                        objProp.company_contact_person_address = Convert.ToString(sqlReader["company_contact_person_address"]);
                        objProp.company_contact_person_country = Convert.ToString(sqlReader["company_contact_person_country"]);
                        objProp.company_contact_person_state = Convert.ToString(sqlReader["company_contact_person_state"]);
                        objProp.company_contact_person_city = Convert.ToString(sqlReader["company_contact_person_city"]);
                        objProp.company_contact_person_mobno = Convert.ToString(sqlReader["company_contact_person_mobno"]);
                        objProp.company_contact_person_email = Convert.ToString(sqlReader["company_contact_person_email"]);
                        objProp.company_contact_person_pincode = Convert.ToString(sqlReader["company_contact_person_pincode"]);
                        objProp.company_constitution_type = Convert.ToString(sqlReader["companyConstitutionType"]);
                        objProp.company_no_partner = Convert.ToString(sqlReader["company_no_partner"]);
                        objProp.company_managing_partner_name = Convert.ToString(sqlReader["company_managing_partner_name"]);
                        objProp.company_curr_annual_turnover = Convert.ToString(sqlReader["company_curr_annual_turnover"]);
                        objProp.company_prev_annual_turnover = Convert.ToString(sqlReader["company_prev_annual_turnover"]);
                        objProp.company_prev_to_last_annual_turnover = Convert.ToString(sqlReader["company_prev_to_last_annual_turnover"]);
                        objProp.company_curr_profit_tax = Convert.ToString(sqlReader["company_curr_profit_tax"]);
                        objProp.company_prev_profit_tax = Convert.ToString(sqlReader["company_prev_profit_tax"]);
                        objProp.company_prev_to_last_profit_tax = Convert.ToString(sqlReader["company_prev_to_last_profit_tax"]);
                        objProp.company_curr_net_worth = Convert.ToString(sqlReader["company_curr_net_worth"]);
                        objProp.company_prev_net_worth = Convert.ToString(sqlReader["company_prev_net_worth"]);
                        objProp.company_prev_to_last__net_worth = Convert.ToString(sqlReader["company_prev_to_last__net_worth"]);
                        objProp.company_curr_surplus = Convert.ToString(sqlReader["company_curr_surplus"]);
                        objProp.company_prev_surplus = Convert.ToString(sqlReader["company_prev_surplus"]);
                        objProp.company_prev_to_last_surplus = Convert.ToString(sqlReader["company_prev_to_last_surplus"]);
                        objProp.company_curr_share_capital = Convert.ToString(sqlReader["company_curr_share_capital"]);
                        objProp.company_prev_share_capital = Convert.ToString(sqlReader["company_prev_share_capital"]);
                        objProp.company_prev_to_last_share_capital = Convert.ToString(sqlReader["company_prev_to_last_share_capital"]);
                        objProp.eim_iem_il = Convert.ToString(sqlReader["eim_iem_il"]);
                        objProp.sector = Convert.ToString(sqlReader["sector"]);
                        objProp.sub_sector = Convert.ToString(sqlReader["sub_sector"]);
                        //objProp.proposed_annual_capacity = Convert.ToString(sqlReader["proposed_annual_capacity"]);
                        //objProp.unit_proposed_annual_capacity = Convert.ToString(sqlReader["unit_proposed_annual_capacity"]);
                        objProp.comercial_production_period = Convert.ToString(sqlReader["comercial_production_period"]);
                        objProp.polution_category = Convert.ToString(sqlReader["polution_category"]);
                        objProp.ground_breaking = Convert.ToString(sqlReader["ground_breaking"]);
                        objProp.civil_structural = Convert.ToString(sqlReader["civil_structural"]);
                        objProp.major_equipment_erection = Convert.ToString(sqlReader["major_equipment_erection"]);
                        objProp.start_of_comercial_production = Convert.ToString(sqlReader["start_of_comercial_production"]);
                        objProp.land_development_cost = Convert.ToString(sqlReader["land_development_cost"]);
                        objProp.building_cost = Convert.ToString(sqlReader["building_cost"]);
                        objProp.plant_machinary_cost = Convert.ToString(sqlReader["plant_machinary_cost"]);
                        objProp.others_cost = Convert.ToString(sqlReader["others_cost"]);
                        objProp.total_cost = Convert.ToString(sqlReader["total_cost"]);
                        if (Convert.ToString(sqlReader["irr"]) == "")
                        {
                            objProp.irr = "0";
                        }
                        else
                        {
                            objProp.irr = Convert.ToString(sqlReader["irr"]);
                        }
                        if (Convert.ToString(sqlReader["dscr"]) == "")
                        {
                            objProp.dscr = "0";
                        }
                        else
                        {

                            objProp.dscr = Convert.ToString(sqlReader["dscr"]);
                        }
                        objProp.equity_contribution = Convert.ToString(sqlReader["equity_contribution"]);
                        objProp.bank_finance = Convert.ToString(sqlReader["bank_finance"]);
                        objProp.fdi_investment = Convert.ToString(sqlReader["fdi_investment"]);
                        objProp.ext_manager = Convert.ToString(sqlReader["ext_manager"]);
                        objProp.proposed_manager = Convert.ToString(sqlReader["proposed_manager"]);
                        objProp.ext_superviser = Convert.ToString(sqlReader["ext_superviser"]);
                        objProp.proposed_superviser = Convert.ToString(sqlReader["proposed_superviser"]);
                        objProp.ext_skilled = Convert.ToString(sqlReader["ext_skilled"]);
                        objProp.proposed_skilled = Convert.ToString(sqlReader["proposed_skilled"]);
                        objProp.ext_semiskilled = Convert.ToString(sqlReader["ext_semiskilled"]);
                        objProp.proposed_semiskilled = Convert.ToString(sqlReader["proposed_semiskilled"]);
                        objProp.ext_unskilled = Convert.ToString(sqlReader["ext_unskilled"]);
                        objProp.proposed_unskilled = Convert.ToString(sqlReader["proposed_unskilled"]);
                        objProp.ext_total = Convert.ToString(sqlReader["ext_total"]);
                        objProp.proposed_total = Convert.ToString(sqlReader["proposed_total"]);
                        objProp.proposed_direct_employeement = Convert.ToString(sqlReader["proposed_direct_employeement"]);
                        objProp.proposed_contractual_employeement = Convert.ToString(sqlReader["proposed_contractual_employeement"]);
                        objProp.other_location = Convert.ToString(sqlReader["other_location"]);
                        objProp.outside_location = Convert.ToString(sqlReader["outside_location"]);
                        objProp.industry_district = Convert.ToString(sqlReader["industry_district"]);
                        objProp.industry_tahasil = Convert.ToString(sqlReader["industry_tahasil"]);
                        objProp.land_industry_estate_area = Convert.ToString(sqlReader["land_industry_estate_area"]);
                        objProp.power_demand = Convert.ToString(sqlReader["power_demand"]);
                        objProp.power_drawl = Convert.ToString(sqlReader["power_drawl"]);
                        objProp.power_capacity = Convert.ToString(sqlReader["power_capacity"]);
                        objProp.existing_water_requirement = Convert.ToString(sqlReader["existing_water_requirement"]);
                        objProp.proposed_water_requirement = Convert.ToString(sqlReader["proposed_water_requirement"]);
                        objProp.water_requirement_for_production = Convert.ToString(sqlReader["water_requirement_for_production"]);
                        if (Convert.ToString(sqlReader["quantum_waste_water"]) == "")
                        {
                            objProp.quantum_waste_water = "0";
                        }
                        else
                        {
                            objProp.quantum_waste_water = Convert.ToString(sqlReader["quantum_waste_water"]);
                        }
                        objProp.specification = Convert.ToString(sqlReader["specification"]);
                        objProp.exprience_in_year = Convert.ToString(sqlReader["exprience_in_year"]);
                        objProp.recommended_land_ipicol = Convert.ToString(sqlReader["recommended_land_ipicol"]);
                        objProp.industry_tahasil = "NA";

                        objProp.nodal_agency_code = Convert.ToString(sqlReader["nodal_agency_code"]).Trim();
                        objProp.project_code = Convert.ToString(sqlReader["project_code"]).Trim();
                        objProp.pan = Convert.ToString(sqlReader["pan"]).Trim();
                        objProp.district_code = Convert.ToString(sqlReader["district_code"]).Trim();

                        list.Add(objProp);
                    }
                    sqlReader.Close();
                    objProp.company_other_constitution = GetBoardOfDir(objProposal.vchProposalNo);
                    objProp.company_promoter = GetPromoName(objProposal.vchProposalNo);
                    objProp.raw_material = GetRawMet(objProposal.vchProposalNo);
                    objProp.project_location = GetProjLoc(objProposal.vchProposalNo);
                    objProp.project_locationDet = GetProjLocDet(objProposal.vchProposalNo);
                    objProp.industry_estate_area = GetIndustAreaName(objProposal.vchProposalNo);
                    objProp.industry_land_bank = GetBankName(objProposal.vchProposalNo);
                    objProp.sources_of_power = GetSourcePower(objProposal.vchProposalNo);
                    objProp.sources_of_water = GetSourceWater(objProposal.vchProposalNo);
                    objProp.document_list = GetDocList(objProposal.vchProposalNo);
                    objProp.product_details = GetPRoList(objProposal.vchProposalNo);
                }

                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        public List<BoardDirector> GetBoardOfDir(string strPorposalNo)
        {
            List<BoardDirector> list = new List<BoardDirector>();
            SqlDataReader sqlReader2 = null;
            SqlCommand cmd1 = new SqlCommand();
            try
            {
                cmd1.Connection = conn;
                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                cmd1.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd1.Parameters.Clear();
                cmd1.Parameters.AddWithValue("@PvchAction", "F");
                cmd1.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader2 = cmd1.ExecuteReader();
                if (sqlReader2.HasRows)
                {
                    while (sqlReader2.Read())
                    {
                        BoardDirector objProp = new BoardDirector();
                        objProp.director_designation = Convert.ToString(sqlReader2["director_designation"]);
                        objProp.director_name = Convert.ToString(sqlReader2["director_name"]);
                        list.Add(objProp);
                    }
                }
                sqlReader2.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd1 = null; }

        }
        public List<CNETPromo> GetPromoName(string strPorposalNo)
        {
            List<CNETPromo> list = new List<CNETPromo>();
            SqlDataReader sqlReader3 = null;
            SqlCommand cmd3 = new SqlCommand();
            try
            {
                cmd3.Connection = conn;
                cmd3.CommandType = System.Data.CommandType.StoredProcedure;
                cmd3.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd3.Parameters.Clear();
                cmd3.Parameters.AddWithValue("@PvchAction", "M");
                cmd3.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader3 = cmd3.ExecuteReader();
                if (sqlReader3.HasRows)
                {
                    while (sqlReader3.Read())
                    {
                        CNETPromo objProp = new CNETPromo();
                        objProp.promoter_name = Convert.ToString(sqlReader3["promoter_name"]);
                        list.Add(objProp);
                    }
                }
                sqlReader3.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd3 = null; }

        }
        public List<CNETRawMet> GetRawMet(string strPorposalNo)
        {
            List<CNETRawMet> list = new List<CNETRawMet>();
            SqlDataReader sqlReader4 = null;
            SqlCommand cmd4 = new SqlCommand();
            try
            {
                cmd4.Connection = conn;
                cmd4.CommandType = System.Data.CommandType.StoredProcedure;
                cmd4.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd4.Parameters.Clear();
                cmd4.Parameters.AddWithValue("@PvchAction", "G");
                cmd4.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader4 = cmd4.ExecuteReader();
                if (sqlReader4.HasRows)
                {
                    while (sqlReader4.Read())
                    {
                        CNETRawMet objProp = new CNETRawMet();
                        objProp.raw_material_name = Convert.ToString(sqlReader4["raw_material_name"]);
                        objProp.material_source = Convert.ToString(sqlReader4["material_source"]);
                        list.Add(objProp);
                    }
                }
                sqlReader4.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd4 = null; }

        }
        public List<CNETProjectLocation> GetProjLoc(string strPorposalNo)
        {
            List<CNETProjectLocation> list = new List<CNETProjectLocation>();
            SqlDataReader sqlReader5 = null;
            SqlCommand cmd5 = new SqlCommand();
            try
            {
                cmd5.Connection = conn;
                cmd5.CommandType = System.Data.CommandType.StoredProcedure;
                cmd5.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd5.Parameters.Clear();
                cmd5.Parameters.AddWithValue("@PvchAction", "H");
                cmd5.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader5 = cmd5.ExecuteReader();
                if (sqlReader5.HasRows)
                {
                    while (sqlReader5.Read())
                    {
                        CNETProjectLocation objProp = new CNETProjectLocation();
                        objProp.unitName = Convert.ToString(sqlReader5["unitName"]);
                        objProp.product_name = Convert.ToString(sqlReader5["product_name"]);
                        objProp.total_capacity = Convert.ToString(sqlReader5["total_capacity"]);
                        objProp.state = Convert.ToString(sqlReader5["state"]);
                        objProp.district = Convert.ToString(sqlReader5["district"]);
                        list.Add(objProp);
                    }
                }
                sqlReader5.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd5 = null; }

        }
        public List<CNETProjLocDet> GetProjLocDet(string strPorposalNo)
        {
            List<CNETProjLocDet> list = new List<CNETProjLocDet>();
            SqlDataReader sqlReader6 = null;
            SqlCommand cmd6 = new SqlCommand();
            try
            {
                cmd6.Connection = conn;
                cmd6.CommandType = System.Data.CommandType.StoredProcedure;
                cmd6.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd6.Parameters.Clear();
                cmd6.Parameters.AddWithValue("@PvchAction", "I");
                cmd6.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader6 = cmd6.ExecuteReader();
                if (sqlReader6.HasRows)
                {
                    while (sqlReader6.Read())
                    {
                        CNETProjLocDet objProp = new CNETProjLocDet();
                        objProp.unitName = Convert.ToString(sqlReader6["unitName"]);
                        objProp.product_name = Convert.ToString(sqlReader6["product_name"]);
                        objProp.total_capacity = Convert.ToString(sqlReader6["total_capacity"]);
                        objProp.Country = Convert.ToString(sqlReader6["country"]);
                        objProp.City = Convert.ToString(sqlReader6["CityName"]);
                        list.Add(objProp);
                    }
                }
                sqlReader6.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd6 = null; }

        }
        public List<CNETArea> GetIndustAreaName(string strPorposalNo)
        {
            List<CNETArea> list = new List<CNETArea>();
            SqlDataReader sqlReader7 = null;
            SqlCommand cmd7 = new SqlCommand();
            try
            {
                cmd7.Connection = conn;
                cmd7.CommandType = System.Data.CommandType.StoredProcedure;
                cmd7.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd7.Parameters.Clear();
                cmd7.Parameters.AddWithValue("@PvchAction", "N");
                cmd7.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader7 = cmd7.ExecuteReader();
                if (sqlReader7.HasRows)
                {
                    while (sqlReader7.Read())
                    {
                        CNETArea objProp = new CNETArea();
                        objProp.area_name = Convert.ToString(sqlReader7["area_name"]);
                        list.Add(objProp);
                    }
                }
                sqlReader7.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd7 = null; }

        }
        public List<CNETBank> GetBankName(string strPorposalNo)
        {
            List<CNETBank> list = new List<CNETBank>();
            SqlDataReader sqlReader8 = null;
            SqlCommand cmd8 = new SqlCommand();
            try
            {
                cmd8.Connection = conn;
                cmd8.CommandType = System.Data.CommandType.StoredProcedure;
                cmd8.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd8.Parameters.Clear();
                cmd8.Parameters.AddWithValue("@PvchAction", "O");
                cmd8.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader8 = cmd8.ExecuteReader();
                if (sqlReader8.HasRows)
                {
                    while (sqlReader8.Read())
                    {
                        CNETBank objProp = new CNETBank();
                        objProp.bank_name = Convert.ToString(sqlReader8["bank_name"]);
                        list.Add(objProp);
                    }
                }
                sqlReader8.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd8 = null; }

        }
        public List<CNETSourcePower> GetSourcePower(string strPorposalNo)
        {
            List<CNETSourcePower> list = new List<CNETSourcePower>();
            SqlDataReader sqlReader9 = null;
            SqlCommand cmd9 = new SqlCommand();
            try
            {
                cmd9.Connection = conn;
                cmd9.CommandType = System.Data.CommandType.StoredProcedure;
                cmd9.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd9.Parameters.Clear();
                cmd9.Parameters.AddWithValue("@PvchAction", "J");
                cmd9.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader9 = cmd9.ExecuteReader();
                if (sqlReader9.HasRows)
                {
                    while (sqlReader9.Read())
                    {
                        CNETSourcePower objProp = new CNETSourcePower();
                        objProp.source_of_power = Convert.ToString(sqlReader9["source_of_power"]);
                        list.Add(objProp);
                    }
                }
                sqlReader9.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd9 = null; }

        }
        public List<CNETSourceWater> GetSourceWater(string strPorposalNo)
        {
            List<CNETSourceWater> list = new List<CNETSourceWater>();
            SqlDataReader sqlReader10 = null;
            SqlCommand cmd10 = new SqlCommand();
            try
            {
                cmd10.Connection = conn;
                cmd10.CommandType = System.Data.CommandType.StoredProcedure;
                cmd10.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd10.Parameters.Clear();
                cmd10.Parameters.AddWithValue("@PvchAction", "K");
                cmd10.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader10 = cmd10.ExecuteReader();
                if (sqlReader10.HasRows)
                {
                    while (sqlReader10.Read())
                    {
                        CNETSourceWater objProp = new CNETSourceWater();
                        objProp.source_of_water = Convert.ToString(sqlReader10["source_of_water"]);
                        list.Add(objProp);
                    }
                }
                sqlReader10.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd10 = null; }

        }
        public List<CNETDocument> GetDocList(string strPorposalNo)
        {
            List<CNETDocument> list = new List<CNETDocument>();
            SqlDataReader sqlReader11 = null;
            SqlCommand cmd11 = new SqlCommand();
            try
            {
                cmd11.Connection = conn;
                cmd11.CommandType = System.Data.CommandType.StoredProcedure;
                cmd11.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd11.Parameters.Clear();
                cmd11.Parameters.AddWithValue("@PvchAction", "L");
                cmd11.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader11 = cmd11.ExecuteReader();
                if (sqlReader11.HasRows)
                {
                    while (sqlReader11.Read())
                    {
                        CNETDocument objProp = new CNETDocument();
                        objProp.document_name = Convert.ToString(sqlReader11["document_name"]);
                        objProp.document_status = Convert.ToString(sqlReader11["document_status"]);
                        objProp.document_Link = Convert.ToString(sqlReader11["document_Link"]);
                        list.Add(objProp);
                    }
                }
                sqlReader11.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd11 = null; }

        }
        public List<CNETProductName> GetPRoList(string strPorposalNo)
        {
            List<CNETProductName> list = new List<CNETProductName>();
            SqlDataReader sqlReader11 = null;
            SqlCommand cmd11 = new SqlCommand();
            try
            {
                cmd11.Connection = conn;
                cmd11.CommandType = System.Data.CommandType.StoredProcedure;
                cmd11.CommandText = "USP_PEAL_PROMOTER_CNET";
                cmd11.Parameters.Clear();
                cmd11.Parameters.AddWithValue("@PvchAction", "P");
                cmd11.Parameters.AddWithValue("@PvchProposalNo", strPorposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader11 = cmd11.ExecuteReader();
                if (sqlReader11.HasRows)
                {
                    while (sqlReader11.Read())
                    {
                        CNETProductName objProp = new CNETProductName();
                        objProp.product_name = Convert.ToString(sqlReader11["vchProduct"]);
                        objProp.proposed_annual_capacity = Convert.ToString(sqlReader11["decProductAnnualCapacity"]);
                        objProp.unit_proposed_annual_capacity = Convert.ToString(sqlReader11["vchUnitName"]);
                        list.Add(objProp);
                    }
                }
                sqlReader11.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd11 = null; }

        }
        public string ProposalCNETData(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_CNET_SUCC_DET";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@PvchAction", "A");
                cmd.Parameters.AddWithValue("@Pvch_oas_cafno", objProposal.vch_oas_cafno);
                cmd.Parameters.AddWithValue("@Pvch_unique_application_id_from_swp", objProposal.vch_unique_application_id_from_swp);
                cmd.Parameters.AddWithValue("@Pvch_industry_code", objProposal.vch_industry_code);
                cmd.Parameters.AddWithValue("@Pvch_success_message", objProposal.vch_success_message);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@Pvch_Error_Msg", objProposal.vch_Error_Msg);
                cmd.Parameters.AddWithValue("@Pvch_validation_Msg", objProposal.vch_validation_Msg);
                cmd.Parameters.AddWithValue("@Pvch_Input_String", objProposal.vch_Input_String);
                //cmd.Parameters.AddWithValue("@P_INT_IDCO_RETURN_STATUS", objProposal.intIdcoReturnStatus);

                SqlParameter par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                return Str_RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        #endregion

        #region Add Proposal details
        public List<ProposalDet> getProposalDetails(ProposalDet objProposal)
        {
            List<ProposalDet> list = new List<ProposalDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PROPOSAL_APPROVALDET";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@P_INT_USERID", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@PCompanyName", objProposal.compName);
                cmd.Parameters.AddWithValue("@PintStatus", objProposal.intStsdet);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.strProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ProposalDet objProp = new ProposalDet();
                        objProp.intProposalId = Convert.ToInt32(sqlReader["intProposalId"]);
                        objProp.strFileName = Convert.ToString(sqlReader["bgintProposalNo"]);
                        objProp.decAmount = Convert.ToDecimal(sqlReader["decAmount"]);
                        objProp.decLoadDemand = Convert.ToDecimal(sqlReader["decLand"]);
                        objProp.strRemarks = Convert.ToString(sqlReader["vchCompName"]);
                        objProp.strActionToBeTakenBY = Convert.ToString(sqlReader["UserActionTobeTaken"]);
                        objProp.strActionTakenBY = Convert.ToString(sqlReader["UserActionTakenBy"]);
                        objProp.intActionToBeTakenBy = Convert.ToInt32(sqlReader["Action To be taken"]);
                        objProp.strStatus = Convert.ToString(sqlReader["INT_STATUS_NAME"]);
                        objProp.intidcoCnt = Convert.ToInt32(sqlReader["intidcoCnt"]);
                        objProp.dtmCreatedOn = Convert.ToString(sqlReader["dtmCreatedOn"]);
                        //Added By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button
                        objProp.intStatus = Convert.ToInt32(sqlReader["intStatus"]);
                        objProp.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                        objProp.decExtendLand = Convert.ToDecimal(sqlReader["decExtendLand"]);
                        //Ended By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button  
                        objProp.strQueryStatus = Convert.ToString(sqlReader["CURRENT_QUERY_STATUS"]);
                        objProp.intCreatedBy = Convert.ToInt32(sqlReader["intCreatedBy"]);
                        objProp.decRecomendLand = Convert.ToString(sqlReader["decRecomendLand"]);
                       // objProp.intFowardAMS = Convert.ToInt32(sqlReader["intFowardAMS"]);
                        objProp.intForwardIDCO = Convert.ToInt32(sqlReader["intForwardIDCO"]);
                        //objProp.strDeptMailContent = Convert.ToString(sqlReader["AMSStatus"]);
                        objProp.IdcoStatus = Convert.ToString(sqlReader["IDCOstatus"]);
                        objProp.IdcoBtnStatus = Convert.ToString(sqlReader["intIDCObtnClickStatus"]);
                        objProp.strIdcoDocs = Convert.ToString(sqlReader["vchIdcoDocs"]);
                        //objProp.strLandUnit = Convert.ToString(sqlReader["vchLandUnit"]); ///// Added by Sushant Jena On Dt:-18-Feb-2020

                        //added by Ritika lath but commented out for live upload
                        //objProp.intAmsQueryStatus = Convert.ToInt32(sqlReader["intAmsQueryStatus"]);
                        //objProp.intNodalOfficerId = Convert.ToInt32(sqlReader["nodalOfficerId"]);

                        list.Add(objProp);
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }


        public List<ProposalDet> getProposalDetailsMIS(ProposalDet objProposal)
        {

            List<ProposalDet> list = new List<ProposalDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PROPOSAL_MIS_RPT";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@P_INT_USERID", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_INT_APPROVE_STS", objProposal.intsts);
                cmd.Parameters.AddWithValue("@P_INT_APPLICATION_FOR", objProposal.intApplicFor);
                cmd.Parameters.AddWithValue("@P_Date_From", objProposal.strFrom);
                cmd.Parameters.AddWithValue("@P_Emp_To", objProposal.strEmployeemntTo);
                cmd.Parameters.AddWithValue("@P_Date_To", objProposal.strTo);
                cmd.Parameters.AddWithValue("@P_ProposedCAI_To", objProposal.strProposedInvTo);
                cmd.Parameters.AddWithValue("@P_INT_DIST", objProposal.intDist);
                cmd.Parameters.AddWithValue("@P_INT_SECID", objProposal.intBlockId);
                cmd.Parameters.AddWithValue("@P_INT_SUBSECID", objProposal.intStsdet);
                cmd.Parameters.AddWithValue("@P_INT_PRIORITY_SEC", objProposal.intDistId);
                cmd.Parameters.AddWithValue("@P_ADate_From", objProposal.strApplicationFrom);
                cmd.Parameters.AddWithValue("@P_ADate_To", objProposal.strApplicationTo);
                cmd.Parameters.AddWithValue("@P_INT_QueryRaised", objProposal.intQueryRaisedValue);
                cmd.Parameters.AddWithValue("@P_LTO_From", objProposal.strATOFrom);
                cmd.Parameters.AddWithValue("@P_LTO_To", objProposal.strATOTo);
                cmd.Parameters.AddWithValue("@P_INT_LandReq", objProposal.intLandReqd);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objProposal.strAction == "V")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.intProposalId = Convert.ToInt32(sqlReader["intProposalId"]);
                            objProp.strFileName = Convert.ToString(sqlReader["bgintProposalNo"]);
                            objProp.decAmount = Convert.ToDecimal(sqlReader["decAmount"]);
                            objProp.decLoadDemand = Convert.ToDecimal(sqlReader["decLand"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["vchCompName"]);
                            objProp.strActionToBeTakenBY = Convert.ToString(sqlReader["UserActionTobeTaken"]);
                            objProp.strActionTakenBY = Convert.ToString(sqlReader["UserActionTakenBy"]);
                            objProp.intActionToBeTakenBy = Convert.ToInt32(sqlReader["Action To be taken"]);
                            objProp.strStatus = Convert.ToString(sqlReader["INT_STATUS_NAME"]);
                            objProp.dtmCreatedOn = Convert.ToString(sqlReader["dtmCreatedOn"]);
                            //Added By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button
                            objProp.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            objProp.decExtendLand = Convert.ToDecimal(sqlReader["decExtendLand"]);
                            //Ended By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button  
                            objProp.strAppliedDistBlock = Convert.ToString(sqlReader["nvchDistrictName"]);
                            objProp.strQueryStatus = Convert.ToString(sqlReader["CURRENT_QUERY_STATUS"]);
                            objProp.intForwardIDCO = Convert.ToInt32(sqlReader["intTotalProp"]);
                            objProp.compName = Convert.ToString(sqlReader["decTotCapitalInvestment"]);
                            objProp.strIdcoDocs = Convert.ToString(sqlReader["VCH_SECTOR"]);
                            objProp.strFrom = Convert.ToString(sqlReader["vchSubSectorName"]);
                            objProp.strDeptSMSContent = Convert.ToString(sqlReader["ProritySector"]);
                            objProp.ActionAuthority = Convert.ToString(sqlReader["decAnnulTurnOvr1"]);
                            objProp.EmailBody = Convert.ToString(sqlReader["LandReqd"]);
                            list.Add(objProp);
                        }
                        if (objProposal.strAction == "L")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.intProposalId = Convert.ToInt32(sqlReader["intProposalId"]);
                            objProp.strFileName = Convert.ToString(sqlReader["bgintProposalNo"]);
                            objProp.decAmount = Convert.ToDecimal(sqlReader["decAmount"]);
                            objProp.decLoadDemand = Convert.ToDecimal(sqlReader["decLand"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["vchCompName"]);
                            objProp.strActionToBeTakenBY = Convert.ToString(sqlReader["UserActionTobeTaken"]);
                            objProp.strActionTakenBY = Convert.ToString(sqlReader["UserActionTakenBy"]);
                            objProp.intActionToBeTakenBy = Convert.ToInt32(sqlReader["Action To be taken"]);
                            objProp.strStatus = Convert.ToString(sqlReader["INT_STATUS_NAME"]);
                            objProp.dtmCreatedOn = Convert.ToString(sqlReader["dtmCreatedOn"]);
                            //Added By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button
                            objProp.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            objProp.decExtendLand = Convert.ToDecimal(sqlReader["decExtendLand"]);
                            //Ended By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button  
                            objProp.strAppliedDistBlock = Convert.ToString(sqlReader["nvchDistrictName"]);
                            objProp.strQueryStatus = Convert.ToString(sqlReader["CURRENT_QUERY_STATUS"]);
                            objProp.intForwardIDCO = Convert.ToInt32(sqlReader["intTotalProp"]);
                            objProp.compName = Convert.ToString(sqlReader["decTotCapitalInvestment"]);
                            list.Add(objProp);
                        }
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        public List<ProposalDet> getAdminProposalDetails(ProposalDet objProposal)
        {

            List<ProposalDet> list = new List<ProposalDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PROPOSAL_APPROVALDET";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@P_INT_USERID", objProposal.intCreatedBy);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ProposalDet objProp = new ProposalDet();
                        objProp.strProposalNo = Convert.ToString(sqlReader["bgintProposalNo"]);
                        objProp.decAmount = Convert.ToDecimal(sqlReader["decAmount"]);
                        objProp.decLoadDemand = Convert.ToDecimal(sqlReader["decLand"]);
                        objProp.strRemarks = Convert.ToString(sqlReader["vchCompName"]);
                        //Added by nibedita for Applied district and block on 16-09-2017
                        objProp.strAppliedDistBlock = Convert.ToString(sqlReader["AppliedDistrictBlock"]);
                        objProp.intDistId = Convert.ToInt32(sqlReader["intDistId"]);
                        objProp.intBlockId = Convert.ToInt32(sqlReader["intBlockId"]);
                        objProp.strActionToBeTakenBY = Convert.ToString(sqlReader["UserActionTobeTaken"]);
                        objProp.strActionTakenBY = Convert.ToString(sqlReader["UserActionTakenBy"]);
                        objProp.intActionToBeTakenBy = Convert.ToInt32(sqlReader["Action To be taken"]);
                        objProp.strStatus = Convert.ToString(sqlReader["INT_STATUS_NAME"]);
                        objProp.dtmCreatedOn = Convert.ToString(sqlReader["dtmCreatedOn"]);
                        objProp.intCreatedBy = Convert.ToInt32(sqlReader["intCreatedBy"]);
                        //Added By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button
                        //objProp.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                        //Ended By Pranay Kumar on 10-Sept-2017 for Show/Hide Raise Query Button                       
                        list.Add(objProp);
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }


        public string ProposalTakeAction(ProposalDet objProposal)
        {
            List<ProposalDet> list = new List<ProposalDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PROPOSAL_APPROVALDET";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@P_INT_USERID", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_INT_PROPOSALNO", objProposal.vchProposalno);
                cmd.Parameters.AddWithValue("@P_INT_APP_STATUS", objProposal.intStatus);
                cmd.Parameters.AddWithValue("@P_VCH_FILENAME", objProposal.strFileName);
                //added by nibedita behera for PEAL certificate file upload in Take action on 05-09-2017
                cmd.Parameters.AddWithValue("@P_VCH_PEALCERTIFICATE", objProposal.strPEALCertificate);
                cmd.Parameters.AddWithValue("@P_VCH_REMARKS", objProposal.strRemarks);
                cmd.Parameters.AddWithValue("@vchSectorIdIT", System.Configuration.ConfigurationManager.AppSettings["SectorIdIT"]);
                cmd.Parameters.AddWithValue("@vchSectorIdTourism", System.Configuration.ConfigurationManager.AppSettings["SectorIdTOURISM"]);
                cmd.Parameters.AddWithValue("@intITdeptId", System.Configuration.ConfigurationManager.AppSettings["DeptIdIT"]);
                cmd.Parameters.AddWithValue("@intTourismdeptId", System.Configuration.ConfigurationManager.AppSettings["DeptIdTOURISM"]);
                cmd.Parameters.AddWithValue("@intIPICOLId", System.Configuration.ConfigurationManager.AppSettings["DeptIdIPICOL"]);
                cmd.Parameters.AddWithValue("@PdecExtendLand", objProposal.decExtendLand);
                cmd.Parameters.AddWithValue("@P_VCH_ScoreCard", objProposal.strScorecard);
                cmd.Parameters.AddWithValue("@P_VCH_ACTION_DATE", objProposal.strUpdatedOn);////Added by Sushant Jena on Dt:-11-Feb-2021
                cmd.Parameters.AddWithValue("@P_INT_OUT", SqlDbType.VarChar);
                cmd.Parameters["@P_INT_OUT"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                string Str_RetValue = cmd.Parameters["@P_INT_OUT"].Value.ToString();
                return Str_RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        public string ForwardLandToIDCO(ProposalDet objProposal)
        {
            List<ProposalDet> list = new List<ProposalDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PROPOSAL_APPROVALDET";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@P_INT_PROPOSALNO", objProposal.vchProposalno);
                cmd.Parameters.AddWithValue("@P_VCH_Idco_Docs", objProposal.strIdcoDocs);
                cmd.Parameters.AddWithValue("@PdecExtendLand", objProposal.decExtendLand);
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
        public List<ProposalDet> PopulateStatus(ProposalDet objprop)
        {
            List<ProposalDet> objList = new List<ProposalDet>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PROPOSAL_APPROVALDET";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objprop.strAction);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ProposalDet objproposal = new ProposalDet();
                        if (objprop.strAction == "S")
                        {
                            objproposal.strStatus = Convert.ToString(sqlReader["vchStatusName"]);
                            objproposal.intStatus = Convert.ToInt32(sqlReader["intStatusId"]);
                        }

                        objList.Add(objproposal);
                    }
                }
                sqlReader.Close();
                return objList;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        #endregion
        #region Raise Query
        public string ProposalRaiseQuery(ProposalDet objProposal)
        {
            List<ProposalDet> list = new List<ProposalDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PROPOSAL_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PintQueryId", objProposal.intQueryId);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.strProposalNo);
                cmd.Parameters.AddWithValue("@PvchRemarks", objProposal.strRemarks);
                cmd.Parameters.AddWithValue("@PintStatus", objProposal.intStatus);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@PvchFileName", objProposal.strFileName);
                //Added By Pranay Kumar on 14-Sept-2017 for addition of multiple files
                cmd.Parameters.AddWithValue("@P_XML_TABLE", objProposal.strPEALCertificate);
                //Ended By Pranay Kumar on 14-Sept-2017 for addition of multiple files
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

        public List<ProposalDet> getRaisedQueryDetails(ProposalDet objProposal)
        {
            List<ProposalDet> list = new List<ProposalDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PROPOSAL_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.strProposalNo);
                cmd.Parameters.AddWithValue("@P_INT_QUERY_ID", objProposal.intQueryId);
                cmd.Parameters.AddWithValue("@PvchRemarks", objProposal.strRemarks);
                cmd.Parameters.AddWithValue("@P_VCH_FILTER_MODE", objProposal.strFilterMode);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objProposal.strAction == "V")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strProposalNo = Convert.ToString(sqlReader["vchProposalno"]);
                            objProp.strActionTakenBY = Convert.ToString(sqlReader["vchIndName"]);
                            objProp.strActionToBeTakenBY = Convert.ToString(sqlReader["vchPromoter"]);
                            objProp.strQuerytype = Convert.ToString(sqlReader["industryType"]);
                            objProp.strStatus = Convert.ToString(sqlReader["strStatus"]);
                            objProp.strPEALCertificate = Convert.ToString(sqlReader["PEALCertificate"]);
                            //Added By Pranay Kumar with Dicussion with Santosh Sir on 10-Sept-2017 for fetching Query Status
                            objProp.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            objProp.intExtendedStatus = Convert.ToInt32(sqlReader["INT_EXTENDED"]);
                            //Ended By Pranay Kumar with Dicussion with Santosh Sir on 10-Sept-2017 for fetching Query Status
                            objProp.ActionAuthority = Convert.ToString(sqlReader["UserActionTakenBy"]);
                            objProp.dtmCreatedOn = Convert.ToString(sqlReader["dtmCreatedOn"]);
                            objProp.intpaymentStatus = Convert.ToInt16(sqlReader["intpaymentStatus"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["CURRENT_QUERY_STATUS"]);
                            //objProp.compName = Convert.ToString(sqlReader["esignStatus"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "E")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strProposalNo = Convert.ToString(sqlReader["vchProposalno"]);
                            objProp.strQuerytype = Convert.ToString(sqlReader["vchQuerytype"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["vchRemarks"]);
                            objProp.strStatus = Convert.ToString(sqlReader["intStatus"]);
                            objProp.strFileName = Convert.ToString(sqlReader["vchFileName"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "F")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strProposalNo = Convert.ToString(sqlReader["vchProposalno"]);
                            objProp.strQuerytype = Convert.ToString(sqlReader["Querytype"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["vchRemarks"]);
                            objProp.strStatus = Convert.ToString(sqlReader["intStatus"]);
                            objProp.strFileName = Convert.ToString(sqlReader["vchFileName"]);
                            list.Add(objProp);
                        }
                        //Added for Draft 
                        else if (objProposal.strAction == "D")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strProposalNo = Convert.ToString(sqlReader["vchProposalno"]);
                            objProp.strActionTakenBY = Convert.ToString(sqlReader["vchIndName"]);
                            //objProp.strActionToBeTakenBY = Convert.ToString(sqlReader["vchPromoter"]);
                            objProp.dtmCreatedOn = Convert.ToString(sqlReader["Lastupdateon"]);
                            objProp.strQuerytype = Convert.ToString(sqlReader["industryType"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "QD")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strProposalNo = Convert.ToString(sqlReader["vchProposalNo"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["VCHREMARKS"]);
                            objProp.intStatus = Convert.ToInt32(sqlReader["INTSTATUS"]);
                            objProp.strFileName = Convert.ToString(sqlReader["VCHFILENAME"]);
                            objProp.dtmCreatedOn = Convert.ToString(sqlReader["DTMCREATEDON"]);
                            objProp.strActionToBeTakenBY = Convert.ToString(sqlReader["FULLNAME"]);
                            objProp.strQuerytype = Convert.ToString(sqlReader["VCHQUERYTYPE"]);
                            objProp.intQueryId = Convert.ToInt32(sqlReader["intQueryId"]);
                            objProp.strTo = Convert.ToString(sqlReader["QUERY_DESC"]);
                            objProp.strQueryStatus = Convert.ToString(sqlReader["VCH_QUERY_UNQ_NO"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "SH")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            objProp.intExtendedStatus = Convert.ToInt32(sqlReader["INT_EXTENDED"]);
                            objProp.strStatus = Convert.ToString(sqlReader["strStatus"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "QF")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strFileName = Convert.ToString(sqlReader["VCH_FILE_NAME"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["VCH_FILE_CONTENT"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "S")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.EmailID = Convert.ToString(sqlReader["VCH_MAIL_ID"]);
                            objProp.MobileNo = Convert.ToString(sqlReader["VCH_MOB_NO"]);
                            objProp.EmailSubject = Convert.ToString(sqlReader["VCH_MAIL_SUB"]);
                            objProp.EmailBody = Convert.ToString(sqlReader["VCH_MAIL_BODY"]);
                            objProp.strSMSContent = Convert.ToString(sqlReader["VCH_SMS_CONTENT"]);
                            objProp.strDeptSMSSub = Convert.ToString(sqlReader["VCH_DEPT_SMS_SUB"]);
                            objProp.strDeptSMSContent = Convert.ToString(sqlReader["VCH_DEPT_SMS_BODY"]);
                            objProp.strDeptMailContent = Convert.ToString(sqlReader["VCH_DEPT_MAIL_BODY"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "T")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.intNoOfTimes = Convert.ToInt32(sqlReader["intNoOfTimes"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "K")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.EmailSubject = Convert.ToString(sqlReader["CNT"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "N")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.vchProposalno = Convert.ToString(sqlReader["vchProposalNo"]);
                            objProp.vchcafno = Convert.ToString(sqlReader["vchcafno"]);
                            objProp.vchIndustryCode = Convert.ToString(sqlReader["vchIndustryCode"]);
                            objProp.vchProcessingFeeRealizationStatus = Convert.ToString(sqlReader["vchProcessingFeeRealizationStatus"]);
                            objProp.vchPaymentRealizationReferenceNo = Convert.ToString(sqlReader["vchPaymentRealizationReferenceNo"]);
                            objProp.vchDemandNoteLink = Convert.ToString(sqlReader["vchDemandNoteLink"]);
                            objProp.vchDemandReceipt = Convert.ToString(sqlReader["vchDemandReceipt"]);
                            objProp.vchAllotmentOrderLink = Convert.ToString(sqlReader["vchAllotmentOrderLink"]);
                            objProp.dtmCreatedOn = Convert.ToString(sqlReader["dtmCreatedOn"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "VA")////View Action Deatils for PEAL By Satya
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strStatus = Convert.ToString(sqlReader["intStatus"]);
                            objProp.strFileName = Convert.ToString(sqlReader["vchFileName"]);
                            objProp.strPEALCertificate = Convert.ToString(sqlReader["vchPEALCertificate"]);
                            objProp.strScorecard = Convert.ToString(sqlReader["vchScoreCard"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["vchRemarks"]);
                            objProp.strUpdatedOn = Convert.ToString(sqlReader["dtmUpdatedOn"]);
                            objProp.strCreatedBy = Convert.ToInt32(sqlReader["intCreatedBy"]);
                            list.Add(objProp);
                        }
                    }
                }

                sqlReader.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }
        }
        #endregion

        #region Project Information

        public string ProjectInfoAED(ProjectInfo objproj)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROJECTDTL_AED";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@PvchAction", objproj.strAction);
                cmd.Parameters.AddWithValue("@PintProposedId", objproj.intProposedId);
                cmd.Parameters.AddWithValue("@PvchProposalno", objproj.vchProposalNo);
                cmd.Parameters.AddWithValue("@PvchNameOfUnit", objproj.vchNameOfUnit);
                cmd.Parameters.AddWithValue("@PvchEINnIEMnIL", objproj.vchEINnIEMnIL);
                cmd.Parameters.AddWithValue("@PintSectorId", objproj.intSectorId);
                cmd.Parameters.AddWithValue("@PintSubSectorId", objproj.intSubSectorId);
                cmd.Parameters.AddWithValue("@PdecProposedAnnualCapacity", objproj.decProposedAnnualCapacity);
                cmd.Parameters.AddWithValue("@PvchUnit", objproj.vchUnit);
                cmd.Parameters.AddWithValue("@PvchOthUnit", objproj.vchOtherUnit);
                cmd.Parameters.AddWithValue("@PdecLandIncLandDev", objproj.decLandIncLandDev);
                cmd.Parameters.AddWithValue("@PdecBuildingndConstruction", objproj.decBuildingndConstruction);
                cmd.Parameters.AddWithValue("@PdecPlantndMachinery", objproj.decPlantndMachinery);
                cmd.Parameters.AddWithValue("@PdecOthers", objproj.decOthers);
                cmd.Parameters.AddWithValue("@PdecTotCapitalInvestment", objproj.decTotCapitalInvestment);
                cmd.Parameters.AddWithValue("@PintPeriodToCommenceProduction", objproj.intPeriodToCommenceProduction);
                cmd.Parameters.AddWithValue("@PvchProjectComingUnder", objproj.vchProjectComingUnder);
                cmd.Parameters.AddWithValue("@PvchPollutionCategory", objproj.vchPollutionCategory);
                cmd.Parameters.AddWithValue("@PintGroundBreaking", objproj.intGroundBreaking);
                cmd.Parameters.AddWithValue("@PintCivilndStructuralCompln", objproj.intCivilndStructuralCompln);
                cmd.Parameters.AddWithValue("@PintMajorEquipmentErect", objproj.intMajorEquipmentErect);
                cmd.Parameters.AddWithValue("@PintStartOfCommercialProd", objproj.intStartOfCommercialProd);
                cmd.Parameters.AddWithValue("@PdecEquityContribution", objproj.decEquityContribution);
                cmd.Parameters.AddWithValue("@PdecBankndInstitutionalFin", objproj.decBankndInstitutionalFin);
                cmd.Parameters.AddWithValue("@PdecTotFinance", objproj.decTotFinance);
                cmd.Parameters.AddWithValue("@PdecForeignInvestment", objproj.decForeignInvestment);
                cmd.Parameters.AddWithValue("@PvchIRR", objproj.vchIRR);
                cmd.Parameters.AddWithValue("@PvchDSCR", objproj.vchDSCR);
                cmd.Parameters.AddWithValue("@PintMangerExist", objproj.intMangerExist);
                cmd.Parameters.AddWithValue("@PintManagerProp", objproj.intManagerProp);
                cmd.Parameters.AddWithValue("@PintSupervisorExist", objproj.intSupervisorExist);
                cmd.Parameters.AddWithValue("@PintSupervisorProp", objproj.intSupervisorProp);
                cmd.Parameters.AddWithValue("@PintSkilledExist", objproj.intSkilledExist);
                cmd.Parameters.AddWithValue("@PintSkilledProp", objproj.intSkilledProp);
                cmd.Parameters.AddWithValue("@PintSemiSkilledExist", objproj.intSemiSkilledExist);
                cmd.Parameters.AddWithValue("@PintSemiSkilledProp", objproj.intSemiSkilledProp);
                cmd.Parameters.AddWithValue("@PintUnSkilledExist", objproj.intUnSkilledExist);
                cmd.Parameters.AddWithValue("@PintUnSkilledProp", objproj.intUnSkilledProp);
                cmd.Parameters.AddWithValue("@PintTotalExist", objproj.intTotalExist);
                cmd.Parameters.AddWithValue("@PintTotalProp", objproj.intTotalProp);
                cmd.Parameters.AddWithValue("@PintPropDirectEmployment", objproj.intPropDirectEmployment);
                cmd.Parameters.AddWithValue("@PintPropContractualEmployment", objproj.intPropContractualEmployment);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objproj.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_XML_PROJ_OTHERLOC", objproj.strProjLocation);
                cmd.Parameters.AddWithValue("@P_XML_PROJ_OTHERTOP5", objproj.strOtherUnits);
                cmd.Parameters.AddWithValue("@P_XML_PROJ_PRODUCTDET", objproj.strProductDetails);
                cmd.Parameters.AddWithValue("@PvchIndustryInterprenur", objproj.vchIndustryInterprenur);
                cmd.Parameters.AddWithValue("@PvchManufacturingProcessFlow", objproj.vchManufacturingProcessFlow);
                cmd.Parameters.AddWithValue("@PvchFeasibilityReport", objproj.vchFeasibilityReport);
                cmd.Parameters.AddWithValue("@PvchBoardResolution", objproj.vchBoardResolution);
                cmd.Parameters.AddWithValue("@PvchSourceOfFinance", objproj.vchSourceOfFinance);

                //cmd.Parameters.AddWithValue("@PintEinNo", objproj.intEinNo);
                cmd.Parameters.AddWithValue("@PintEinNo", objproj.intEinNoderr);
                cmd.Parameters.AddWithValue("@P_XML_GC_NET_WORTH", objproj.strXmlGcNetWorth); //// Added by Sushant Jena On Dt:05-Mar-2021

                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                return Str_RetValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
        }
        public List<ProjectInfo> getProjectInfoDetails(ProjectInfo objprop)
        {
            List<ProjectInfo> list = new List<ProjectInfo>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROJECTDTL_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objprop.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalno", objprop.vchProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ProjectInfo objProp = new ProjectInfo();
                        objProp.intProposedId = Convert.ToInt32(sqlReader["intProposedId"]);
                        objProp.vchProposalNo = Convert.ToString(sqlReader["vchProposalNo"]);
                        objProp.vchNameOfUnit = Convert.ToString(sqlReader["vchNameOfUnit"]);
                        objProp.vchEINnIEMnIL = Convert.ToString(sqlReader["vchEINnIEMnIL"]);
                        objProp.intSectorId = Convert.ToInt32(sqlReader["intSectorId"]);
                        objProp.intSubSectorId = Convert.ToInt32(sqlReader["intSubSectorId"]);
                        objProp.vchSectorName = Convert.ToString(sqlReader["VCH_SECTOR"]);
                        objProp.vchSubSectorName = Convert.ToString(sqlReader["vchSubSectorName"]);
                        objProp.decProposedAnnualCapacity = Convert.ToDecimal(sqlReader["decProposedAnnualCapacity"]);
                        objProp.vchUnit = Convert.ToString(sqlReader["vchUnit"]);
                        objProp.CapacityUnit = Convert.ToString(sqlReader["CapacityUnit"]);
                        objProp.vchOtherUnit = Convert.ToString(sqlReader["vchOtherUnit"]);
                        objProp.decLandIncLandDev = Convert.ToDecimal(sqlReader["decLandIncLandDev"]);
                        objProp.decBuildingndConstruction = Convert.ToDecimal(sqlReader["decBuildingndConstruction"]);
                        objProp.decPlantndMachinery = Convert.ToDecimal(sqlReader["decPlantndMachinery"]);
                        objProp.decOthers = Convert.ToDecimal(sqlReader["decOthers"]);
                        objProp.decTotCapitalInvestment = Convert.ToDecimal(sqlReader["decTotCapitalInvestment"]);
                        objProp.intPeriodToCommenceProduction = Convert.ToInt32(sqlReader["intPeriodToCommenceProduction"]);
                        objProp.vchProjectComingUnder = Convert.ToString(sqlReader["vchProjectComingUnder"]);
                        objProp.vchPollutionCategory = Convert.ToString(sqlReader["vchPollutionCategory"]);
                        objProp.intGroundBreaking = Convert.ToInt32(sqlReader["intGroundBreaking"]);
                        objProp.intCivilndStructuralCompln = Convert.ToInt32(sqlReader["intCivilndStructuralCompln"]);
                        objProp.intMajorEquipmentErect = Convert.ToInt32(sqlReader["intMajorEquipmentErect"]);
                        objProp.intStartOfCommercialProd = Convert.ToInt32(sqlReader["intStartOfCommercialProd"]);
                        objProp.decEquityContribution = Convert.ToDecimal(sqlReader["decEquityContribution"]);
                        objProp.decBankndInstitutionalFin = Convert.ToDecimal(sqlReader["decBankndInstitutionalFin"]);
                        objProp.decTotFinance = Convert.ToDecimal(sqlReader["decTotFinance"]);
                        objProp.decForeignInvestment = Convert.ToDecimal(sqlReader["decForeignInvestment"]);
                        objProp.vchIRR = Convert.ToString(sqlReader["vchIRR"]);
                        objProp.vchDSCR = Convert.ToString(sqlReader["vchDSCR"]);
                        objProp.intMangerExist = Convert.ToInt32(sqlReader["intMangerExist"]);
                        objProp.intManagerProp = Convert.ToInt32(sqlReader["intManagerProp"]);
                        objProp.intSupervisorExist = Convert.ToInt32(sqlReader["intSupervisorExist"]);
                        objProp.intSupervisorProp = Convert.ToInt32(sqlReader["intSupervisorProp"]);
                        objProp.intSkilledExist = Convert.ToInt32(sqlReader["intSkilledExist"]);
                        objProp.intSkilledProp = Convert.ToInt32(sqlReader["intSkilledProp"]);
                        objProp.intSemiSkilledExist = Convert.ToInt32(sqlReader["intSemiSkilledExist"]);
                        objProp.intSemiSkilledProp = Convert.ToInt32(sqlReader["intSemiSkilledProp"]);
                        objProp.intUnSkilledExist = Convert.ToInt32(sqlReader["intUnSkilledExist"]);
                        objProp.intUnSkilledProp = Convert.ToInt32(sqlReader["intUnSkilledProp"]);
                        objProp.intTotalExist = Convert.ToInt32(sqlReader["intTotalExist"]);
                        objProp.intTotalProp = Convert.ToInt32(sqlReader["intTotalProp"]);
                        objProp.intPropDirectEmployment = Convert.ToInt32(sqlReader["intPropDirectEmployment"]);
                        objProp.intPropContractualEmployment = Convert.ToInt32(sqlReader["intPropContractualEmployment"]);

                        objProp.vchIndustryInterprenur = Convert.ToString(sqlReader["vchIndustryInterprenur"]);
                        objProp.vchManufacturingProcessFlow = Convert.ToString(sqlReader["vchManufacturingProcessFlow"]);
                        objProp.vchFeasibilityReport = Convert.ToString(sqlReader["vchFeasibilityReport"]);
                        objProp.vchBoardResolution = Convert.ToString(sqlReader["vchBoardResolution"]);
                        objProp.vchSourceOfFinance = Convert.ToString(sqlReader["vchSourceOfFinance"]);
                        objProp.intEinNoderr = Convert.ToInt32(sqlReader["intEinNoderr"]);
                        objProp.intConstitution = Convert.ToInt32(sqlReader["intConstitution"]); //// Added by Sushant Jena On Dt:-27-Aug-2019

                        list.Add(objProp);
                    }
                }
                sqlReader.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }

        }
        public List<ProjectInfo> getProjectLOCDetails(ProjectInfo objprop)
        {

            List<ProjectInfo> list = new List<ProjectInfo>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PEAL_PROJECTDTL_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objprop.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalno", objprop.vchProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ProjectInfo objProp = new ProjectInfo();
                        objProp.intProjectId = Convert.ToInt32(sqlReader["intProjectId"]);
                        objProp.vchProposalNo = Convert.ToString(sqlReader["vchProposalNo"]);
                        objProp.vchUnitName = Convert.ToString(sqlReader["vchUnitName"]);
                        objProp.vchProduct = Convert.ToString(sqlReader["vchProduct"]);
                        objProp.vchTotCapacity = Convert.ToString(sqlReader["vchTotCapacity"]);
                        objProp.intStateId = Convert.ToInt32(sqlReader["intStateId"]);
                        objProp.intDistId = Convert.ToInt32(sqlReader["intDistId"]);
                        objProp.vchStateName = Convert.ToString(sqlReader["vchStateName"]);
                        objProp.vchDistName = Convert.ToString(sqlReader["vchDistName"]);
                        list.Add(objProp);
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        public List<ProjectInfo> getOtherUnitlDetails(ProjectInfo objprop)
        {

            List<ProjectInfo> list = new List<ProjectInfo>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PEAL_PROJECTDTL_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objprop.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalno", objprop.vchProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ProjectInfo objProp = new ProjectInfo();
                        objProp.intUnitId = Convert.ToInt32(sqlReader["intUnitId"]);
                        objProp.vchProposalNo = Convert.ToString(sqlReader["vchProposalNo"]);
                        objProp.vchUnitName = Convert.ToString(sqlReader["vchUnitName"]);
                        objProp.vchProduct = Convert.ToString(sqlReader["vchProduct"]);
                        objProp.vchTotCapacity = Convert.ToString(sqlReader["vchTotCapacity"]);
                        objProp.intCountryId = Convert.ToInt32(sqlReader["intCountryId"]);
                        objProp.vchCountryName = Convert.ToString(sqlReader["vchCountryName"]);
                        objProp.vchCityName = Convert.ToString(sqlReader["vchCityName"]);
                        list.Add(objProp);
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }

        public List<ProjectInfo> getProductNameDetails(ProjectInfo objprop)
        {

            List<ProjectInfo> list = new List<ProjectInfo>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PEAL_PROJECTDTL_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objprop.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalno", objprop.vchProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ProjectInfo objProp = new ProjectInfo();
                        objProp.intUnitId = Convert.ToInt32(sqlReader["intUnitid"]);
                        objProp.vchProposalNo = Convert.ToString(sqlReader["vchProposalNo"]);
                        objProp.vchProposedUnit = Convert.ToString(sqlReader["vchProposedUnit"]);
                        objProp.vchProductName = Convert.ToString(sqlReader["vchProductName"]);
                        objProp.vchProposedAnnualCapacity = Convert.ToString(sqlReader["vchProposedAnnualCapacity"]);
                        objProp.intProductid = Convert.ToInt32(sqlReader["intProductid"]);
                        list.Add(objProp);
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }


        public List<ProjectInfo> PopulateProjDropdowns(ProjectInfo objprop)
        {
            List<ProjectInfo> objList = new List<ProjectInfo>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PEAL_PROJECTDTL_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objprop.strAction);
                cmd.Parameters.AddWithValue("@PintProposedId", objprop.vchProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ProjectInfo objprojDet = new ProjectInfo();
                        if (objprop.strAction == "SE")
                        {
                            objprojDet.vchSectorName = Convert.ToString(sqlReader["VCH_SECTOR_NAME"]);
                            objprojDet.intSectorId = Convert.ToInt32(sqlReader["int_sector_id"]);
                        }
                        if (objprop.strAction == "SU")
                        {
                            objprojDet.vchSectorName = Convert.ToString(sqlReader["VCH_SECTOR_NAME"]);
                            objprojDet.intSectorId = Convert.ToInt32(sqlReader["int_sector_id"]);
                        }
                        if (objprop.strAction == "ST")
                        {
                            objprojDet.vchStateName = Convert.ToString(sqlReader["vchStateName"]);
                            objprojDet.intStateId = Convert.ToInt32(sqlReader["intStateId"]);
                        }
                        if (objprop.strAction == "DT")
                        {
                            objprojDet.vchDistName = Convert.ToString(sqlReader["vchDistName"]);
                            objprojDet.intDistId = Convert.ToInt32(sqlReader["intDistId"]);
                        }
                        if (objprop.strAction == "SD")
                        {
                            objprojDet.vchDistName = Convert.ToString(sqlReader["vchDistName"]);
                            objprojDet.intDistId = Convert.ToInt32(sqlReader["intDistId"]);
                        }
                        if (objprop.strAction == "CT")
                        {
                            objprojDet.vchCountryName = Convert.ToString(sqlReader["vchCountryName"]);
                            objprojDet.intCountryId = Convert.ToInt32(sqlReader["intCountryId"]);
                        }
                        if (objprop.strAction == "UT")
                        {
                            objprojDet.vchUnitName = Convert.ToString(sqlReader["vchUnitName"]);
                            objprojDet.intUnitId = Convert.ToInt32(sqlReader["intUnitId"]);
                        }
                        if (objprop.strAction == "BL")
                        {
                            objprojDet.vchBlockName = Convert.ToString(sqlReader["vchDistName"]);
                            objprojDet.intBlockId = Convert.ToInt32(sqlReader["intDistId"]);
                        }

                        if (objprop.strAction == "TE")
                        {
                            objprojDet.vchTeheshilName = Convert.ToString(sqlReader["vchTeheshilName"]);
                            objprojDet.intTeheshilId = Convert.ToInt32(sqlReader["intTeheshilId"]);
                        }

                        if (objprop.strAction == "SS")
                        {
                            objprojDet.vchserviceName = Convert.ToString(sqlReader["vchserviceName"]);
                            objprojDet.intserviceid = Convert.ToInt32(sqlReader["intserviceid"]);
                        }

                        //added by Ritika Lath to get the department list
                        if (objprop.strAction == "DD")
                        {
                            objprojDet.vchserviceName = Convert.ToString(sqlReader["vchserviceName"]);
                            objprojDet.intserviceid = Convert.ToInt32(sqlReader["intserviceid"]);
                        }

                        //added by Ritika Lath to get the directorate list
                        if (objprop.strAction == "di")
                        {
                            objprojDet.vchserviceName = Convert.ToString(sqlReader["vchserviceName"]);
                            objprojDet.intserviceid = Convert.ToInt32(sqlReader["intserviceid"]);
                        }

                        if (objprop.strAction == "DIS")
                        {
                            objprojDet.discomename = Convert.ToString(sqlReader["DISCOMENAME"]);
                            objprojDet.discomeid = Convert.ToInt32(sqlReader["DISCOMEID"]);
                        }

                        if (objprop.strAction == "TA")
                        {
                            objprojDet.vchQf = Convert.ToString(sqlReader["VchQualification"]);
                            objprojDet.intQid = Convert.ToInt32(sqlReader["intQualifId"]);
                        }
                        if (objprop.strAction == "TB")
                        {
                            objprojDet.vchTQ = Convert.ToString(sqlReader["vchTechnical"]);
                            objprojDet.intTid = Convert.ToInt32(sqlReader["intTechId"]);
                        }
                        if (objprop.strAction == "CC")
                        {
                            objprojDet.vchCNTISDNo = Convert.ToString(sqlReader["vchCNTISDNo"]);
                            objprojDet.intCnt = Convert.ToInt32(sqlReader["intCnt"]);
                        }
                        if (objprop.strAction == "SM")
                        {
                            objprojDet.vchStatusName = Convert.ToString(sqlReader["vchStatusName"]);
                            objprojDet.intStatusId = Convert.ToInt32(sqlReader["intStatusId"]);
                        }
                        if (objprop.strAction == "SR")
                        {
                            objprojDet.intPriority = Convert.ToInt32(sqlReader["BIT_PRIORITY"]);
                        }
                        if (objprop.strAction == "ci")
                        {
                            objprojDet.vchStatusName = Convert.ToString(sqlReader["FIYear"]);
                            objprojDet.intStatusId = Convert.ToInt32(sqlReader["FIYearID"]);
                        }
                        objList.Add(objprojDet);
                    }

                }
                sqlReader.Close();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return objList;
        }

        #endregion
        #region Add Declartion details
        public string Declartion(Declartion objdec)
        {
            List<Declartion> list = new List<Declartion>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_Declartion_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objdec.strAction);
                cmd.Parameters.AddWithValue("@PbitDeclartion", objdec.intDeclartion);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objdec.intCreatedBy);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objdec.vchProposalno);
                cmd.Parameters.AddWithValue("@vchSectorIdIT", System.Configuration.ConfigurationManager.AppSettings["SectorIdIT"]);
                cmd.Parameters.AddWithValue("@vchSectorIdTourism", System.Configuration.ConfigurationManager.AppSettings["SectorIdTOURISM"]);
                cmd.Parameters.AddWithValue("@intITdeptId", System.Configuration.ConfigurationManager.AppSettings["DeptIdIT"]);
                cmd.Parameters.AddWithValue("@intTourismdeptId", System.Configuration.ConfigurationManager.AppSettings["DeptIdTOURISM"]);
                cmd.Parameters.AddWithValue("@intIPICOLId", System.Configuration.ConfigurationManager.AppSettings["DeptIdIPICOL"]);

                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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
        public List<Declartion> GetDeclartionData(Declartion objDec)
        {

            List<Declartion> list = new List<Declartion>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_Declartion_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objDec.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objDec.vchProposalno);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        Declartion objProp = new Declartion();
                        objProp.intDeclartion = Convert.ToInt32(sqlReader["bitDeclartion"]);
                        list.Add(objProp);
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        #endregion
        #region Added by nibedita behera on 18-08-2017 for View PEAL Report
        //company information
        public List<PromoterDet> ViewCompanyInformation(PromoterDet objCompInfo)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objCompInfo.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objCompInfo.vchProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        PromoterDet objPropPromo = new PromoterDet();

                        objPropPromo.vchCompName = Convert.ToString(sqlReader["vchCompName"]);
                        objPropPromo.vchAddress = Convert.ToString(sqlReader["vchAddress"]);
                        objPropPromo.vchCountryName = Convert.ToString(sqlReader["Country"]);
                        objPropPromo.vchStateName = Convert.ToString(sqlReader["States"]);
                        objPropPromo.vchCity = Convert.ToString(sqlReader["vchCity"]);
                        objPropPromo.vchPhoneNo = Convert.ToString(sqlReader["vchPhoneNo"]);
                        objPropPromo.vchFaxNo = Convert.ToString(sqlReader["vchFaxNo"]);
                        objPropPromo.vchEmail = Convert.ToString(sqlReader["vchEmail"]);
                        objPropPromo.intPin = Convert.ToInt32(sqlReader["intPin"]);
                        objPropPromo.vchContactPerson = Convert.ToString(sqlReader["vchContactPerson"]);
                        objPropPromo.vchCorAdd = Convert.ToString(sqlReader["vchCorAdd"]);
                        objPropPromo.vchCorCountryName = Convert.ToString(sqlReader["CorrCountry"]);
                        objPropPromo.vchCorStateName = Convert.ToString(sqlReader["CorrState"]);
                        objPropPromo.vchCorCity = Convert.ToString(sqlReader["vchCorCity"]);
                        objPropPromo.vchCorMobileNo = Convert.ToString(sqlReader["vchCorMobileNo"]);
                        objPropPromo.vchCorFaxNo = Convert.ToString(sqlReader["vchCorFaxNo"]);
                        objPropPromo.vchCorEmail = Convert.ToString(sqlReader["vchCorEmail"]);
                        objPropPromo.intCorPin = Convert.ToInt32(sqlReader["intCorPin"]);
                        objPropPromo.vchConstitution = Convert.ToString(sqlReader["Constitution"]);
                        objPropPromo.vchOtheConstituition = Convert.ToString(sqlReader["vchOtheConstituition"]);
                        objPropPromo.intYearOfIncorporation = Convert.ToString(sqlReader["intYearOfIncorporation"]);
                        objPropPromo.vchPlaceIncor = Convert.ToString(sqlReader["vchPlaceIncor"]);
                        objPropPromo.vchGSTIN = Convert.ToString(sqlReader["vchGSTIN"]);
                        objPropPromo.intProjectType = Convert.ToInt32(sqlReader["intProjectType"]);
                        objPropPromo.intApplicationFor = Convert.ToInt32(sqlReader["intApplicationFor"]);
                        objPropPromo.intNumberOfPartner = Convert.ToInt32(sqlReader["intNumberOfPartner"]);
                        objPropPromo.vchManagPartner = Convert.ToString(sqlReader["vchManagPartner"]);
                        objPropPromo.decAnnulTurnOvr1 = Convert.ToString(sqlReader["decAnnulTurnOvr1"]);
                        objPropPromo.decAnnulTurnOvr2 = Convert.ToString(sqlReader["decAnnulTurnOvr2"]);
                        objPropPromo.decAnnulTurnOvr3 = Convert.ToString(sqlReader["decAnnulTurnOvr3"]);
                        objPropPromo.decProfitAftrTx1 = Convert.ToString(sqlReader["decProfitAftrTx1"]);
                        objPropPromo.decProfitAftrTx2 = Convert.ToString(sqlReader["decProfitAftrTx2"]);
                        objPropPromo.decProfitAftrTx3 = Convert.ToString(sqlReader["decProfitAftrTx3"]);
                        objPropPromo.decNetWorth1 = Convert.ToString(sqlReader["decNetWorth1"]);
                        objPropPromo.decNetWorth2 = Convert.ToString(sqlReader["decNetWorth2"]);
                        objPropPromo.decNetWorth3 = Convert.ToString(sqlReader["decNetWorth3"]);
                        objPropPromo.decResvSurp1 = Convert.ToString(sqlReader["decResvSurp1"]);
                        objPropPromo.decResvSurp2 = Convert.ToString(sqlReader["decResvSurp2"]);
                        objPropPromo.decResvSurp3 = Convert.ToString(sqlReader["decResvSurp3"]);
                        objPropPromo.decShareCap1 = Convert.ToString(sqlReader["decShareCap1"]);
                        objPropPromo.decShareCap2 = Convert.ToString(sqlReader["decShareCap2"]);
                        objPropPromo.decShareCap3 = Convert.ToString(sqlReader["decShareCap3"]);
                        objPropPromo.intEduQualif = Convert.ToInt32(sqlReader["intEduQualif"]);
                        objPropPromo.intTecQualif = Convert.ToInt32(sqlReader["intTecQualif"]);
                        objPropPromo.intExpInYr = Convert.ToInt32(sqlReader["intExpInYr"]);
                        objPropPromo.vchExisIndName = Convert.ToString(sqlReader["vchExisIndName"]);
                        objPropPromo.vchDistrictName = Convert.ToString(sqlReader["nvchDistrictName"]);
                        objPropPromo.vchBlockName = Convert.ToString(sqlReader["BlockName"]);
                        objPropPromo.intAllotedBy = Convert.ToInt32(sqlReader["intAllotedBy"]);
                        objPropPromo.vchlandInAcres = Convert.ToString(sqlReader["vchlandInAcres"]);
                        objPropPromo.vchNatureAct = Convert.ToString(sqlReader["vchNatureAct"]);
                        objPropPromo.vchSector = Convert.ToString(sqlReader["VCH_SECTOR"]);
                        objPropPromo.vchSubSector = Convert.ToString(sqlReader["vchSubSectorName"]);
                        objPropPromo.vchCapacity = Convert.ToString(sqlReader["vchCapacity"]);
                        objPropPromo.vchCapacityUnit = Convert.ToString(sqlReader["CapacityUnit"]);
                        objPropPromo.vchOther = Convert.ToString(sqlReader["vchOther"]);
                        objPropPromo.intFyn1 = Convert.ToInt32(sqlReader["intFyn1"]);
                        objPropPromo.intFyn2 = Convert.ToInt32(sqlReader["intFyn2"]);
                        objPropPromo.intFyn3 = Convert.ToInt32(sqlReader["intFyn3"]);
                        objPropPromo.vchNameOfPromoter = Convert.ToString(sqlReader["vchNameOfPromoter"]);
                        objPropPromo.vchOtherState = Convert.ToString(sqlReader["vchOtherState"]);
                        objPropPromo.vchOtherStateCor = Convert.ToString(sqlReader["vchOtherStateCor"]);
                        objPropPromo.VCHISDPHNo = Convert.ToString(sqlReader["ISDPH"]);
                        objPropPromo.VCHISDFXNo = Convert.ToString(sqlReader["ISDFax"]);
                        objPropPromo.VCHISDMOBo = Convert.ToString(sqlReader["ISDMOB"]);
                        objPropPromo.VCHISDFAXCor = Convert.ToString(sqlReader["ISDFAXCor"]);
                        objPropPromo.PhoneStateCode = Convert.ToInt32(sqlReader["PhoneStateCode"]);
                        objPropPromo.dtmApplicationDate = Convert.ToDateTime(sqlReader["dtmCreatedOn"]); //// Added by Sushant Jena On Dt:-19-Aug-2019

                        list.Add(objPropPromo);
                    }
                }
                sqlReader.Close();
                return list;
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
                cmd = null;
            }
        }

        //View Land and utility

        public List<LandDet> ViewLandUtility(LandDet objLand)
        {
            List<LandDet> list = new List<LandDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_LandAndUtility";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objLand.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objLand.vchProposalNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        LandDet objPropLand = new LandDet();

                        objPropLand.bitLandRequired = Convert.ToBoolean(sqlReader["bitLandRequired"]);
                        objPropLand.nvchDistrictName = Convert.ToString(sqlReader["nvchDistrictName"]);
                        objPropLand.vchBlockName = Convert.ToString(sqlReader["BlockName"]);
                        objPropLand.decExtendLand = Convert.ToDecimal(sqlReader["decExtendLand"]);
                        objPropLand.sintLandRequiredIDCO = Convert.ToInt32(sqlReader["sintLandRequiredIDCO"]);
                        objPropLand.vchIDCOInustrialName = Convert.ToString(sqlReader["vchIDCOInustrialName"]);
                        objPropLand.vchIDCOInustrial = Convert.ToString(sqlReader["vchIDCOInustrial"]);
                        objPropLand.sintLandAcquiredIDCO = Convert.ToInt32(sqlReader["sintLandAcquiredIDCO"]);
                        objPropLand.decPowerDemandGrid = Convert.ToDecimal(sqlReader["decPowerDemandGrid"]);
                        objPropLand.decPowerDrawalCPP = Convert.ToDecimal(sqlReader["decPowerDrawalCPP"]);
                        objPropLand.decCapacityofCPPPlant = Convert.ToDecimal(sqlReader["decCapacityofCPPPlant"]);
                        objPropLand.decWaterRequireExist = Convert.ToDecimal(sqlReader["decWaterRequireExist"]);
                        objPropLand.decWaterReqireProposed = Convert.ToDecimal(sqlReader["decWaterReqireProposed"]);
                        objPropLand.decWaterRequirProduct = Convert.ToDecimal(sqlReader["decWaterRequirProduct"]);
                        objPropLand.vchSurfaceWater = Convert.ToString(sqlReader["vchSurfaceWater"]);
                        objPropLand.vchIdcoSupply = Convert.ToString(sqlReader["vchIdcoSupply"]);
                        objPropLand.vchRainWtrHarvesting = Convert.ToString(sqlReader["vchRainWtrHarvesting"]);
                        objPropLand.vchother = Convert.ToString(sqlReader["vchother"]);
                        objPropLand.vchOtherSpecify = Convert.ToString(sqlReader["vchOtherSpecify"]);
                        objPropLand.vchQuntRecyllingWaste = Convert.ToString(sqlReader["vchQuntRecyllingWaste"]);
                        objPropLand.vchWasteConserFile = Convert.ToString(sqlReader["vchWasteConserFile"]);
                        objPropLand.vchWaterHazardousFile = Convert.ToString(sqlReader["vchWaterHazardousFile"]);
                        objPropLand.strProjectLandStmt = Convert.ToString(sqlReader["vchLandUseStmt"]);
                        objPropLand.strProjectLayOut = Convert.ToString(sqlReader["vchProjectLayout"]);
                        //objPropLand.strLandUnit = Convert.ToString(sqlReader["vchLandUnit"]);///// Added by Sushant Jena On Dt:-18-Feb-2020                        
                        objPropLand.DecPowerProducerIpp = Convert.ToDecimal(sqlReader["decPowerProducerIPP"]); ///// Added by Sushant Jena On Dt 24-Aug-2021
                        objPropLand.DecRecomendLand = Convert.ToDecimal(sqlReader["decRecomendLand"]);  /// Add  by Anil Sahoo
                        objPropLand.StrLandRecomFile = Convert.ToString(sqlReader["vchIdcoDocs"]); /// Add by Anil Sahoo
                        list.Add(objPropLand);
                    }
                }

                sqlReader.Close();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }
        }

        #endregion
        #region Raise Query For SERvice
        public string ServiceProposalRaiseQuery(ProposalDet objProposal)
        {
            List<ProposalDet> list = new List<ProposalDet>();
            //conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SERVICE_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PintQueryId", objProposal.intQueryId);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.strProposalNo);
                cmd.Parameters.AddWithValue("@PvchRemarks", objProposal.strRemarks);
                cmd.Parameters.AddWithValue("@PintStatus", objProposal.intStatus);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@PvchFileName", objProposal.strFileName);
                //Added By Pranay Kumar on 19-Sept-2017 for addition of multiple files
                cmd.Parameters.AddWithValue("@P_XML_TABLE", objProposal.strPEALCertificate);
                //Ended By Pranay Kumar on 19-Sept-2017 for addition of multiple files
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
                //conn.Close();
                cmd.Dispose();
            }
        }
        public List<ProposalDet> ServicegetRaisedQueryDetails(ProposalDet objProposal)
        {

            List<ProposalDet> list = new List<ProposalDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SERVICE_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objProposal.intCreatedBy);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.strProposalNo);
                cmd.Parameters.AddWithValue("@PintServiceId", objProposal.intNoOfTimes);
                cmd.Parameters.AddWithValue("@P_INT_QUERY_ID", objProposal.intQueryId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objProposal.strAction == "V")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strProposalNo = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                            objProp.strActionTakenBY = Convert.ToString(sqlReader["vchIndName"]);
                            objProp.strActionToBeTakenBY = Convert.ToString(sqlReader["vchPromoter"]);
                            objProp.strQuerytype = Convert.ToString(sqlReader["industryType"]);
                            objProp.strStatus = Convert.ToString(sqlReader["strStatus"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "E")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strProposalNo = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                            objProp.strQuerytype = Convert.ToString(sqlReader["vchQuerytype"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["vchRemarks"]);
                            objProp.strStatus = Convert.ToString(sqlReader["intStatus"]);
                            objProp.strFileName = Convert.ToString(sqlReader["vchFileName"]);
                            list.Add(objProp);
                        }
                        //Added for Query details
                        else if (objProposal.strAction == "F")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strProposalNo = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                            objProp.strQuerytype = Convert.ToString(sqlReader["Querytype"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["vchRemarks"]);
                            objProp.strStatus = Convert.ToString(sqlReader["intStatus"]);
                            objProp.strFileName = Convert.ToString(sqlReader["vchFileName"]);
                            list.Add(objProp);
                        }
                        //Added BY Pranay Kumar on 12-Sept-2017 for Showing Query History Details
                        else if (objProposal.strAction == "QD")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strApplicationKey = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["VCHREMARKS"]);
                            objProp.intStatus = Convert.ToInt32(sqlReader["INTSTATUS"]);
                            objProp.strFileName = Convert.ToString(sqlReader["VCHFILENAME"]);
                            objProp.dtmCreatedOn = Convert.ToString(sqlReader["DTMCREATEDON"]);
                            objProp.strActionToBeTakenBY = Convert.ToString(sqlReader["FULLNAME"]);
                            objProp.strQuerytype = Convert.ToString(sqlReader["VCHQUERYTYPE"]);
                            objProp.intQueryId = Convert.ToInt32(sqlReader["intQueryId"]);
                            objProp.strTo = Convert.ToString(sqlReader["QUERY_DESC"]);
                            objProp.strQueryStatus = Convert.ToString(sqlReader["VCH_QUERY_UNQ_NO"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "SH")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            objProp.intExtendedStatus = Convert.ToInt32(sqlReader["INT_EXTENDED"]);
                            objProp.strStatus = Convert.ToString(sqlReader["strStatus"]);
                            list.Add(objProp);
                        }
                        //Ended BY Pranay Kumar on 12-Sept-2017 for Showing Query History Details
                        else if (objProposal.strAction == "QF")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.strFileName = Convert.ToString(sqlReader["VCH_FILE_NAME"]);
                            objProp.strRemarks = Convert.ToString(sqlReader["VCH_FILE_CONTENT"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "S")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.EmailID = Convert.ToString(sqlReader["VCH_MAIL_ID"]);
                            objProp.MobileNo = Convert.ToString(sqlReader["VCH_MOB_NO"]);
                            objProp.EmailSubject = Convert.ToString(sqlReader["VCH_MAIL_SUB"]);
                            objProp.EmailBody = Convert.ToString(sqlReader["VCH_MAIL_BODY"]);
                            objProp.strSMSContent = Convert.ToString(sqlReader["VCH_SMS_CONTENT"]);
                            objProp.strDeptSMSSub = Convert.ToString(sqlReader["VCH_DEPT_SMS_SUB"]);
                            objProp.strDeptSMSContent = Convert.ToString(sqlReader["VCH_DEPT_SMS_BODY"]);
                            objProp.strDeptMailContent = Convert.ToString(sqlReader["VCH_DEPT_MAIL_BODY"]);
                            list.Add(objProp);
                        }
                        else if (objProposal.strAction == "T")
                        {
                            ProposalDet objProp = new ProposalDet();
                            objProp.intNoOfTimes = Convert.ToInt32(sqlReader["intNoOfTimes"]);
                            list.Add(objProp);
                        }
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        #endregion

        #region"Added by suroj"
        public List<ProjectInfo> GetProposalDtls(ProjectInfo objCompInfo)
        {
            List<ProjectInfo> list = new List<ProjectInfo>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PROPOSAL_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objCompInfo.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_PROPOSALNO", objCompInfo.vchProposalNo);
                cmd.Parameters.AddWithValue("@P_INT_SAID", objCompInfo.intSAid);
                cmd.Parameters.AddWithValue("@P_VCH_APPLICANTNO", objCompInfo.vchApplicantNo);
                cmd.Parameters.AddWithValue("@P_INT_TYPEID", objCompInfo.intTypeid);
                cmd.Parameters.AddWithValue("@P_VCH_ORDERNO", objCompInfo.vchOrderNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {

                        if (objCompInfo.strAction == "S")
                        {
                            ProjectInfo objCompInfo1 = new ProjectInfo();
                            objCompInfo1.vchProposalNo = Convert.ToString(sqlReader["vchproposalNo"]);
                            objCompInfo1.vchCompanyName = Convert.ToString(sqlReader["vchCompanyName"]);
                            list.Add(objCompInfo1);
                        }

                        if (objCompInfo.strAction == "S1")
                        {
                            ProjectInfo objCompInfo1 = new ProjectInfo();
                            objCompInfo1.intserviceid = Convert.ToInt16(sqlReader["intServiceid"]);
                            objCompInfo1.vchserviceName = Convert.ToString(sqlReader["vchServiceName"]);
                            list.Add(objCompInfo1);
                        }
                        if (objCompInfo.strAction == "S2")
                        {
                            ProjectInfo objCompInfo1 = new ProjectInfo();
                            objCompInfo1.vchAccountHead = Convert.ToString(sqlReader["AccountHead"]);
                            objCompInfo1.decPaymentAmt = Convert.ToDecimal(sqlReader["PaymentAmount"]);
                            objCompInfo1.vchDescription = Convert.ToString(sqlReader["vchDesc"]);

                            list.Add(objCompInfo1);
                        }
                        if (objCompInfo.strAction == "S3")
                        {
                            ProjectInfo objCompInfo1 = new ProjectInfo();
                            objCompInfo1.vchOrderNo = Convert.ToString(sqlReader["vchOrderNo"]);
                            objCompInfo1.vchReqID = Convert.ToString(sqlReader["vchReqid"]);
                            list.Add(objCompInfo1);
                        }
                        if (objCompInfo.strAction == "S4")
                        {
                            ProjectInfo objCompInfo1 = new ProjectInfo();
                            objCompInfo1.vchServiceType = Convert.ToString(sqlReader["vchServiceType"]);
                            objCompInfo1.intserviceid = Convert.ToInt16(sqlReader["intServiceType"]);
                            list.Add(objCompInfo1);
                        }
                        if (objCompInfo.strAction == "S5")
                        {
                            ProjectInfo objCompInfo1 = new ProjectInfo();
                            objCompInfo1.vchReqID = Convert.ToString(sqlReader["vchReqid"]);

                            list.Add(objCompInfo1);
                        }
                        if (objCompInfo.strAction == "S6")
                        {
                            ProjectInfo objCompInfo1 = new ProjectInfo();
                            objCompInfo1.vchAccountHead = Convert.ToString(sqlReader["HEADNAME"]);
                            objCompInfo1.decPaymentAmt = Convert.ToDecimal(sqlReader["AMOUNT"]);
                            objCompInfo1.vchDescription = Convert.ToString(sqlReader["PAYMENTDESCRIPTION"]);
                            list.Add(objCompInfo1);
                        }
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        #endregion

        public string AddProposalDtls(ProjectInfo objCompInfo)
        {
            List<ProjectInfo> list = new List<ProjectInfo>();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PROPOSAL_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objCompInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INT_SAID", objCompInfo.intserviceid);
                cmd.Parameters.AddWithValue("@P_VCH_APPLICANTNO", objCompInfo.vchApplicantNo);
                cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", objCompInfo.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_INT_TYPEID", objCompInfo.intTypeid);
                cmd.Parameters.AddWithValue("@P_VCH_CHALLANAMT", objCompInfo.vchChallanAmt);
                cmd.Parameters.AddWithValue("@P_VCH_BANKTRANSID", objCompInfo.vchbankTransactionId);
                cmd.Parameters.AddWithValue("@P_VCH_CHALLANREFID", objCompInfo.vchbankchallanRefId);
                cmd.Parameters.AddWithValue("@P_VCH_BANKTRANSSTATUS", objCompInfo.vchbankTransactionStatus);
                cmd.Parameters.AddWithValue("@P_VCH_CHALLANDPDATE", objCompInfo.vchbankTransTimeStamp);
                cmd.Parameters.AddWithValue("@P_VCH_ORDERNO", objCompInfo.vchOrderNo);
                cmd.Parameters.AddWithValue("@vchSectorIdIT", System.Configuration.ConfigurationManager.AppSettings["SectorIdIT"]);
                cmd.Parameters.AddWithValue("@vchSectorIdTourism", System.Configuration.ConfigurationManager.AppSettings["SectorIdTOURISM"]);
                cmd.Parameters.AddWithValue("@intITdeptId", System.Configuration.ConfigurationManager.AppSettings["DeptIdIT"]);
                cmd.Parameters.AddWithValue("@intTourismdeptId", System.Configuration.ConfigurationManager.AppSettings["DeptIdTOURISM"]);
                cmd.Parameters.AddWithValue("@intIPICOLId", System.Configuration.ConfigurationManager.AppSettings["DeptIdIPICOL"]);
                //cmd.Parameters.AddWithValue("@P_INT_OUT", SqlDbType.VarChar);
                //cmd.Parameters["@P_INT_OUT"].Direction = ParameterDirection.Output;
                //cmd.ExecuteNonQuery();
                //str_Retvalue = cmd.Parameters["@P_INT_OUT"].Value.ToString();
                //return str_Retvalue;
                SqlParameter par;
                par = cmd.Parameters.Add("@P_INT_OUT", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
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
                //conn.Close();
                cmd.Dispose();
            }
        }

        #region "Added By Pranay Kumar on 10-Sept-2017"
        #region "Check Raise Query Date withing Date Limit"
        public int CheckRaiseQStatus(ProposalDet ObjPropasal)
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
                cmd.CommandText = "USP_PROPOSAL_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", ObjPropasal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", ObjPropasal.strProposalNo);
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
        #region "WHEN USER WANTS TO EXTEND THE QUERY DATE"
        public int intExtendDate(string strAction, int intProposalNo)
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
                cmd.CommandText = "USP_PROPOSAL_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", intProposalNo);
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
        public DataTable GetChildData(String Query, int val)
        {
            try
            {
                Query = "select intParentID as intReportID,vchChildName, vchURL from M_MIS_MasterList where intDeletedFlag=0 and intParentID=" + val;
                DataTable dt = new DataTable();
                dt = ExecuteCommand(Query, CommandType.Text); //calling the connectionfactory methode from DAL  
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetParentData(String Query)
        {
            try
            {
                Query = "select intReportID,vchChildName,vchURL from M_MIS_MasterList where intParentID=0 and intDeletedFlag=0";
                DataTable dt = new DataTable();
                dt = ExecuteCommand(Query, CommandType.Text); //calling the connectionfactory methode from DAL  
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ExecuteCommand(string Text, CommandType CmdType)
        {
            try
            {
                conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand();
                //Opening Connection  
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                cmd = new SqlCommand(Text, conn);
                cmd.CommandType = CmdType; //Assign the SqlString Type to Command Object  

                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                //Loading all data in a datatable from datareader  
                dt.Load(dr);
                //Closing the connection  
                conn.Close();
                return dt;
            }
            catch
            {
                throw;
            }

        }
        #endregion
        public List<ProposalDet> GetMISAllStatus(ProposalDet objprop)
        {

            List<ProposalDet> list = new List<ProposalDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_MIS_RPT_DET";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objprop.strAction);
                cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objprop.intDistId);
                cmd.Parameters.AddWithValue("@P_INT_SECID", objprop.intStsdet);
                cmd.Parameters.AddWithValue("@P_PROJECT_TYPE", objprop.intsts);
                cmd.Parameters.AddWithValue("@P_INT_INVEST_AMT", objprop.intBlockId);
                cmd.Parameters.AddWithValue("@P_INT_APP_YR", objprop.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_INT_APPRV_YR", objprop.intApplicFor);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ProposalDet objProp = new ProposalDet();
                        objProp.INTDISTRICTID = Convert.ToInt32(sqlReader["DISTID"]);
                        objProp.VCHDISTRICTNAME = Convert.ToString(sqlReader["VCHDISTRICTNAME"]);
                        objProp.TOTALAPPLICATION = Convert.ToString(sqlReader["TOTALAPPLICATION"]);
                        objProp.APPROVED = Convert.ToString(sqlReader["APPROVED"]);
                        objProp.QUERY1 = Convert.ToString(sqlReader["QUERY1"]);
                        objProp.QUERY2 = Convert.ToString(sqlReader["QUERY2"]);
                        objProp.PENDING = Convert.ToString(sqlReader["PENDING"]);
                        objProp.REJECTED = Convert.ToString(sqlReader["REJECTED"]);
                        objProp.EXISTING = Convert.ToString(sqlReader["EXISTING"]);
                        objProp.PROPOSED = Convert.ToString(sqlReader["PROPOSED"]);
                        objProp.TOTALCAPITALINVESTMENT = Convert.ToString(sqlReader["TOTALCAPITALINVESTMENT"]);
                        objProp.STRLANDALLOTED = Convert.ToString(sqlReader["land_alloted"]);
                        objProp.AVERAGE_APPROVAL = Convert.ToString(sqlReader["average_Approval"]);
                        list.Add(objProp);
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        public string ProposalEnclosurUpdate(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_UPDATE_ENCLOSURE_FILES";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposanNo", objProposal.vchProposalNo);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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
        public List<ProposalDet> GetMISAllDetailsStatus(ProposalDet objprop)
        {

            List<ProposalDet> list = new List<ProposalDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_MIS_RPT_DET";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objprop.strAction);
                cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objprop.intDistId);
                cmd.Parameters.AddWithValue("@P_INT_SECID", objprop.intStsdet);
                cmd.Parameters.AddWithValue("@P_PROJECT_TYPE", objprop.intsts);
                cmd.Parameters.AddWithValue("@P_INT_INVEST_AMT", objprop.intBlockId);
                cmd.Parameters.AddWithValue("@P_INT_APP_YR", objprop.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_INT_APPRV_YR", objprop.intApplicFor);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ProposalDet objProp = new ProposalDet();
                        objProp.vchProposalno = Convert.ToString(sqlReader["VCHPROPOSALNO"]);
                        objProp.VCHDISTRICTNAME = Convert.ToString(sqlReader["VCHDISTRICTNAME"]);
                        objProp.TOTALAPPLICATION = Convert.ToString(sqlReader["PROJECTTYPE"]);
                        objProp.APPROVED = Convert.ToString(sqlReader["VCHCOMPNAME"]);
                        objProp.REJECTED = Convert.ToString(sqlReader["VCHADDRESS"]);
                        objProp.EXISTING = Convert.ToString(sqlReader["VCH_SECTOR"]);
                        objProp.PROPOSED = Convert.ToString(sqlReader["SUBSECTOR"]);
                        objProp.TOTALCAPITALINVESTMENT = Convert.ToString(sqlReader["DECTOTCAPITALINVESTMENT"]);
                        objProp.QUERY1 = Convert.ToString(sqlReader["INTTOTALEXIST"]);
                        objProp.QUERY2 = Convert.ToString(sqlReader["INTTOTALPROP"]);
                        list.Add(objProp);
                    }

                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }

        public List<LandDet> GetAMSUserId(LandDet objLand)
        {
            List<LandDet> list = new List<LandDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_AMSUSERID_DET";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objLand.strAction);
                cmd.Parameters.AddWithValue("@PintUserID", objLand.intCreatedBy);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        LandDet objPropLand = new LandDet();
                        objPropLand.intUpdatedBy = Convert.ToInt32(sqlReader["intAmsUserid"]);
                        list.Add(objPropLand);
                    }
                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        public string PEALServiceOrderUpdate(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PEAL_SERVICE_PAYMENT_ORDER_UPDATE";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@Pbankname", objProposal.bankname);
                cmd.Parameters.AddWithValue("@Pifsccode", objProposal.ifsccode);
                cmd.Parameters.AddWithValue("@Pdealername", objProposal.dealername);
                cmd.Parameters.AddWithValue("@Pordernumber", objProposal.ordernumber);
                cmd.Parameters.AddWithValue("@Pbankcode", objProposal.bankcode);
                cmd.Parameters.AddWithValue("@Ptreasuryrefno", objProposal.treasuryrefno);
                cmd.Parameters.AddWithValue("@Pbanktranstimestamp", objProposal.banktranstimestamp);
                cmd.Parameters.AddWithValue("@Pbanktransstatus", objProposal.banktransstatus);
                cmd.Parameters.AddWithValue("@PAmount", objProposal.Amount);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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
        public List<PromoterDet> GetIDCOMISDetailsRPT(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_USER_IDCO_MIS_RPT";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@P_PROPOSALNO", objProposal.vchProposalNo);
                cmd.Parameters.AddWithValue("@P_INT_SECID", objProposal.intSectorId);
                cmd.Parameters.AddWithValue("@P_INT_INVEST_AMT", objProposal.intSubSectorId);
                cmd.Parameters.AddWithValue("@P_UNITNMAE", objProposal.vchCompName);
                cmd.Parameters.AddWithValue("@P_INT_DIST_ID", objProposal.intCordist);
                cmd.Parameters.AddWithValue("@P_INT_NODAL_AGANCY", objProposal.intNodalOfcrID);
                cmd.Parameters.AddWithValue("@P_Date_From", objProposal.dtmFromDate);
                cmd.Parameters.AddWithValue("@P_Date_To", objProposal.dtmToDate);
                cmd.Parameters.AddWithValue("@P_Application_No", objProposal.vchApplication);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        PromoterDet objProp = new PromoterDet();
                        objProp.vchProposalNo = Convert.ToString(sqlReader["VCHPROPOSALNO"]);
                        objProp.vchCompName = Convert.ToString(sqlReader["VCHCOMPNAME"]);
                        objProp.vchDistrictName = Convert.ToString(sqlReader["VCHDISTRICTNAME"]);
                        objProp.vchSector = Convert.ToString(sqlReader["VCH_SECTOR"]);
                        objProp.strNodalOfcrName = Convert.ToString(sqlReader["VCHNODALOFFICERNAME"]);
                        objProp.dtmForwardDate = Convert.ToString(sqlReader["DTMFORWARDAMSDATE"]);
                        objProp.decAnnulTurnOvr1 = Convert.ToString(sqlReader["DECEXTENDLAND"]);
                        objProp.decAnnulTurnOvr2 = Convert.ToString(sqlReader["DECTOTCAPITALINVESTMENT"]);
                        objProp.vchOtherState = Convert.ToString(sqlReader["IDCOSTATUS"]);
                        objProp.vchPanfile = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                        objProp.strLandRecommendedtoidco = Convert.ToString(sqlReader["decRecomendLand"]);
                        objProp.strLandRecommendedbyidco = Convert.ToString(sqlReader["decIdcoRecomendLand"]);
                        objProp.strApprovalorderlink = Convert.ToString(sqlReader["vchAllotmentOrderLink"]);
                        objProp.strPayStatus = Convert.ToString(sqlReader["vchPayStatus"]);
                        list.Add(objProp);
                    }
                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }

        public string IDCOBtntatusUpdate(PromoterDet objProposal)
        {
            List<PromoterDet> list = new List<PromoterDet>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_Declartion_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", "P");
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.vchProposalNo);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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
        public List<PromoterDet> GetIDCOEmailDetails()
        {
            List<PromoterDet> list = new List<PromoterDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_Declartion_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", "O");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        PromoterDet objProp = new PromoterDet();
                        objProp.vchEmail = Convert.ToString(sqlReader["vchEmail"]);
                        list.Add(objProp);
                    }
                }
                sqlReader.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }

        #region Added By Satya On 16-04-2019 for Industry List

        public DataTable IndustryListDetails(ProposalDet objProposal)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_MISIndustryListDetails";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objProposal.IntInvestorId);
                cmd.Parameters.AddWithValue("@P_INT_APPROVAL_STATUS", objProposal.IntApprovalStatus);
                cmd.Parameters.AddWithValue("@P_VCH_PAN", objProposal.strPanno);
                cmd.Parameters.AddWithValue("@P_VCH_INV_NAME", objProposal.strIndName);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            return dt;
        }

        #endregion
        #region Added By Manoj On 08-05-2019 for Industry List
        public DataTable ALLSERVICEDETAILS(ProposalDet objProposal)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_ALL_SERVICEDTL_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PDistrictid", objProposal.INTDISTRICTID);
                cmd.Parameters.AddWithValue("@PBlockid", objProposal.intBlockId);
                cmd.Parameters.AddWithValue("@PINT_INVESTOR_ID", objProposal.IntInvestorId);
                cmd.Parameters.AddWithValue("@CINT_INVESTOR_ID", objProposal.intNodalOfficerId);
                cmd.Parameters.AddWithValue("@MINT_INVESTOR_ID", objProposal.VCH_INVESTOR_ID);
                cmd.Parameters.AddWithValue("@VCH_FROMDATE", objProposal.strATOFrom);
                cmd.Parameters.AddWithValue("@VCH_TODATE", objProposal.strATOTo);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            return dt;
        }

        #endregion
        #region Added By Manoj On 22-05-2019 for Industry List
        public DataTable INVESTORINFODISPLAY(ProposalDet objProposal)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_INFO_UPDATE";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", objProposal.IntInvestorId);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_FIRSTNAME", objProposal.VCH_CONTACT_FIRSTNAME);
                cmd.Parameters.AddWithValue("@VCH_EMAIL", objProposal.VCH_EMAIL);
                cmd.Parameters.AddWithValue("@VCH_OFF_MOBILE", objProposal.VCH_OFF_MOBILE);
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            return dt;
        }
        public string INVESTORINFOUPDATE(ProposalDet objProposal)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_INFO_UPDATE";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", objProposal.IntInvestorId);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_FIRSTNAME", objProposal.VCH_CONTACT_FIRSTNAME);
                cmd.Parameters.AddWithValue("@VCH_EMAIL", objProposal.VCH_EMAIL);
                cmd.Parameters.AddWithValue("@VCH_OFF_MOBILE", objProposal.VCH_OFF_MOBILE);
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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

        /// <summary>
        /// Added by Sushant Jena On Dt:- 19-Apr-2021
        /// This method is used to fetch NSWS State CAF Data
        /// </summary>
        /// <param name="objProposal"></param>
        /// <returns></returns>
        public DataTable GetCAFDetailsNSWS(PromoterDet objProposal)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_NSWS_FETCH_DATA";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_INV_SWS_ID", objProposal.strInvestorSWSId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            return dt;
        }

        #region add  anil sahoo

         public   DataSet GetProposalTrackDetails(ProposalDet objProposal)
        {
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TRACK_PEAL_APP_STATUS";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_APP_ID", objProposal.strProposalNo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            return ds;
        }
        #endregion
        #region add  Debiprasanna
        // Function to get the view of Query Date 
        //Add By Debiprasanna
        public DataTable QueryDateUpdate(ProposalDet objProposal)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@PvchAction", "UQD");
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.strProposalNo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            return dt;
        }
        #endregion
        // Function to get the Update Of Query Date
        //Add By Debiprasanna
        public string UpdateQueryDate(ProposalDet objProposal) 
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", objProposal.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objProposal.strProposalNo);
                cmd.Parameters.AddWithValue("@PdtmRaiseQuery", objProposal.VCH_RAISE_QUERY);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                str_Retvalue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();

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
                cmd.Dispose();
            }
            return str_Retvalue;
        }

        //Add By Debiprasanna
        public DataSet GetPCTrackDetails(ProposalDet objProposal)
        {
            DataSet ds = new DataSet();

            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TRACK_PEAL_APP_STATUS";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_APP_ID", objProposal.vchAppFormattedNo);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
            }
            return ds;
        }
    }
}




    



