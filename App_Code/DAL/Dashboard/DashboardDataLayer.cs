using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using System.Data;

namespace DataAcessLayer.DashboardDataLayer
{
    public class DashboardDataLayer
    {
        string str_Retvalue = "";
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

        #region"ADDED BY SUROJ KUMAR PRADHAN TO FETCH PROPOSAL DTLS FOR DASHBOARD"
        public List<SWPDashboard> GetDashboardProposalDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_VCHSECTORID", objDashboardInfo.intSecId);
                cmd.Parameters.AddWithValue("@P_INTTYPE", objDashboardInfo.intType);
                cmd.Parameters.AddWithValue("@P_INT_DISTRICTID", objDashboardInfo.intDistrictid);
                cmd.Parameters.AddWithValue("@P_INT_YEAR", objDashboardInfo.intYearId);
                cmd.Parameters.AddWithValue("@P_INT_QUARTER", objDashboardInfo.intQuarter);
                cmd.Parameters.AddWithValue("@P_DISTRICTDTLS", objDashboardInfo.strDistrictDtl);
                cmd.Parameters.AddWithValue("@P_FINACIALYR", objDashboardInfo.strFinacialYear);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "PB")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strApplied = Convert.ToString(sqlReader["APPLIED"]);
                            objDashboardInfo1.strApproved = Convert.ToString(sqlReader["APPROVED"]);
                            objDashboardInfo1.strRejected = Convert.ToString(sqlReader["REJECTED"]);
                            objDashboardInfo1.strUnderEvaltion = Convert.ToString(sqlReader["QUERY"]);
                            objDashboardInfo1.strTotCapitalPropApplied = Convert.ToString(sqlReader["TOTCAPITALAPPLIED"]);
                            objDashboardInfo1.strTotNoCapitalPropApplied = Convert.ToString(sqlReader["TOTPROPEMPAPPLIED"]);
                            objDashboardInfo1.strTotCapitalPropApproved = Convert.ToString(sqlReader["TOTCAPITALAPPROVE"]);
                            objDashboardInfo1.strTotNoCapitalPropApproved = Convert.ToString(sqlReader["TOTPROPEMPAPPROVED"]);
                            objDashboardInfo1.strUnderEvaltion = Convert.ToString(sqlReader["UnderEvalution"]);
                            objDashboardInfo1.QraiseTotal = Convert.ToString(sqlReader["QRaiseTotal"]);
                            objDashboardInfo1.strDeferred = Convert.ToString(sqlReader["Deferred"]);
                            objDashboardInfo1.strPealOrtpsaCrossedState = Convert.ToString(sqlReader["intORTPSACrossed"]); ///// Added by Sushant Jena on Dt. 23-May-2018

                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "PDD")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strDistApplied = Convert.ToString(sqlReader["APPLIED"]);
                            objDashboardInfo1.strDistApproved = Convert.ToString(sqlReader["APPROVED"]);
                            objDashboardInfo1.strDistRejected = Convert.ToString(sqlReader["REJECTED"]);
                            objDashboardInfo1.strDistUnderEvaltion = Convert.ToString(sqlReader["UnderEvalution"]);
                            objDashboardInfo1.QraiseTotal = Convert.ToString(sqlReader["QRaiseTotal"]);
                            objDashboardInfo1.strDistDeferred = Convert.ToString(sqlReader["Deferred"]);
                            objDashboardInfo1.strPealOrtpsaCrossedDist = Convert.ToString(sqlReader["intORTPSACrossed"]); ///// Added by Sushant Jena on Dt. 23-May-2018

                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "PSE")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strDistApplied = Convert.ToString(sqlReader["APPLIED"]);
                            objDashboardInfo1.strDistApproved = Convert.ToString(sqlReader["APPROVED"]);
                            objDashboardInfo1.strDistRejected = Convert.ToString(sqlReader["REJECTED"]);
                            objDashboardInfo1.strDistUnderEvaltion = Convert.ToString(sqlReader["UnderEvalution"]);
                            objDashboardInfo1.strDistDeferred = Convert.ToString(sqlReader["Deferred"]);
                            objDashboardInfo1.QraiseTotal = Convert.ToString(sqlReader["QRaiseTotal"]);
                            objDashboardInfo1.strPealOrtpsaCrossedITandTourism = Convert.ToString(sqlReader["intORTPSACrossed"]); ///// Added by Sushant Jena on Dt. 23-May-2018

                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "PT")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strDistApplied = Convert.ToString(sqlReader["APPLIED"]);
                            objDashboardInfo1.strDistApproved = Convert.ToString(sqlReader["APPROVED"]);
                            objDashboardInfo1.strDistRejected = Convert.ToString(sqlReader["REJECTED"]);
                            objDashboardInfo1.strDistUnderEvaltion = Convert.ToString(sqlReader["UnderEvalution"]);
                            objDashboardInfo1.strDistDeferred = Convert.ToString(sqlReader["Deferred"]);
                            objDashboardInfo1.QraiseTotal = Convert.ToString(sqlReader["QRaiseTotal"]);
                            objDashboardInfo1.strPealOrtpsaCrossedState = Convert.ToString(sqlReader["intORTPSACrossed"]); ///// Added by Sushant Jena on Dt. 28-May-2018

                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "PD")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strTotNoCapitalPropApproved = Convert.ToString(sqlReader["TOTPROPEMPAPPROVED"]);
                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "SU")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.intDistrictid = Convert.ToInt16(sqlReader["intDistrict"]);
                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "PCI")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strTotCapitalPropApproved = Convert.ToString(sqlReader["TOTCAPITALAPPROVE"]);
                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "UE")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strUnderEvaltion = Convert.ToString(sqlReader["PROPOSAL"]);
                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "RA")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strPealApproved = Convert.ToString(sqlReader["PROPOSAL"]);
                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "RP")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strPealRecived = Convert.ToString(sqlReader["PROPOSAL"]);
                            list.Add(objDashboardInfo1);
                        }
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
        #endregion

        #region "Added by nibedita behera on 18-07-2017"
        public List<SWPDashboard> GetDashboardAPAAtatus(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INT_YEAR", objDashboardInfo.intYearId);
                cmd.Parameters.AddWithValue("@P_INT_DISTRICTID", objDashboardInfo.intDistrictId);
                cmd.Parameters.AddWithValue("@P_INT_MONTH", objDashboardInfo.intMonthId);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "AP")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strChngReqApplied = Convert.ToString(sqlReader["TotalApplied"]);
                            objDashboardInfo1.strChngReqDispose = Convert.ToString(sqlReader["TotalDisposed"]);
                            objDashboardInfo1.strChngReqPendAtUnit = Convert.ToString(sqlReader["TotalPendingUnit"]);
                            objDashboardInfo1.strChngReqPendingAtIDCO = Convert.ToString(sqlReader["TotalPendingIdco"]);
                            objDashboardInfo1.strChngReqCrossthirty = Convert.ToString(sqlReader["TotalMajorPendingIdco"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "AM")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strChngReqApplied = Convert.ToString(sqlReader["TotalApplied"]);
                            objDashboardInfo1.strChngReqDispose = Convert.ToString(sqlReader["TotalDisposed"]);
                            objDashboardInfo1.strChngReqPendAtUnit = Convert.ToString(sqlReader["TotalPendingUnit"]);
                            objDashboardInfo1.strChngReqPendingAtIDCO = Convert.ToString(sqlReader["TotalPendingIdco"]);
                            objDashboardInfo1.strChngReqCrossthirty = Convert.ToString(sqlReader["TotalMajorPendingIdco"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "DIC")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strChngReqApplied = Convert.ToString(sqlReader["TotalApplied"]);
                            objDashboardInfo1.strChngReqDispose = Convert.ToString(sqlReader["TotalDisposed"]);
                            objDashboardInfo1.strChngReqPendAtUnit = Convert.ToString(sqlReader["TotalPendingUnit"]);
                            objDashboardInfo1.strChngReqPendingAtIDCO = Convert.ToString(sqlReader["TotalPendingIdco"]);
                            objDashboardInfo1.strChngReqCrossthirty = Convert.ToString(sqlReader["TotalMajorPendingIdco"]);
                            list.Add(objDashboardInfo1);
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

        #region "Added by nibedita behera on 19-07-2017"
        public List<SWPDashboard> GetDashboardPEALStatusDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intUserid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.PEALIndays = Convert.ToInt32(sqlReader["appliedindays"]);
                        objDashboardInfo1.StrStatus = Convert.ToString(sqlReader["strStatus"]);
                        list.Add(objDashboardInfo1);
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

        #region "Added by nibedita behera on 20-07-2017"
        public List<SWPDashboard> GetDashboardAPAAGrid(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_INT_YEAR", objDashboardInfo.intYearId);
                cmd.Parameters.AddWithValue("@P_INT_DISTRICTID", objDashboardInfo.intDistrictId);
                cmd.Parameters.AddWithValue("@P_INT_MONTH", objDashboardInfo.intMonthId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.ApplicationName = Convert.ToString(sqlReader["ApplicationName"]);
                        objDashboardInfo1.IEName = Convert.ToString(sqlReader["IEName"]);
                        objDashboardInfo1.PartyName = Convert.ToString(sqlReader["PartyName"]);
                        objDashboardInfo1.PendingDays = Convert.ToString(sqlReader["PendingDays"]);
                        objDashboardInfo1.RequestDate = Convert.ToString(sqlReader["RequestDate"]);
                        list.Add(objDashboardInfo1);
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

        public string AddAppaDetals(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_TOTAPPLIED", objDashboardInfo.intTotalApplied);
                cmd.Parameters.AddWithValue("@P_TOTDISPOSED", objDashboardInfo.intTotalDisposed);
                cmd.Parameters.AddWithValue("@P_TOTMAJORPENDING", objDashboardInfo.intTotalMajorPending);
                cmd.Parameters.AddWithValue("@P_TOTPENDINGIDCO", objDashboardInfo.intTotalPendingIdco);
                cmd.Parameters.AddWithValue("@P_TOTPENDINGUNIT", objDashboardInfo.intTotalPendingUnit);
                cmd.Parameters.AddWithValue("@P_INT_OUT", SqlDbType.VarChar);
                cmd.Parameters["@P_INT_OUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                str_Retvalue = cmd.Parameters["@P_INT_OUT"].Value.ToString();
                return str_Retvalue;
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

        public List<SWPDashboard> CheckAppastatus(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", "PI");
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.intStatus = Convert.ToInt16(sqlReader["Userstatus"]);
                        objDashboardInfo1.intDistrictid = Convert.ToInt16(sqlReader["districtid"]);

                        list.Add(objDashboardInfo1);
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
        public List<SWPDashboard> GetSPMGdepartmentwiseStatus(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SPMG_DEPTCNT_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_DEPTID", objDashboardInfo.intDeptId);
                cmd.Parameters.AddWithValue("@P_YEAR ", objDashboardInfo.intYearId);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {

                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.strApplied = Convert.ToString(sqlReader["Received"]);
                        objDashboardInfo1.strApproved = Convert.ToString(sqlReader["Resolved"]);
                        objDashboardInfo1.strPending = Convert.ToString(sqlReader["Pending"]);
                        objDashboardInfo1.intDaysPass = Convert.ToString(sqlReader["Pendingmore"]);
                        list.Add(objDashboardInfo1);

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

        #region "Added by romalin Panda on 22-09-2017"

        public List<SWPDashboard> GetDashboardServiceStatusDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);
                cmd.Parameters.AddWithValue("@P_INTDEPTID", objDashboardInfo.intDeptId);
                cmd.Parameters.AddWithValue("@P_INTSERVICEID", objDashboardInfo.intServiceId);
                cmd.Parameters.AddWithValue("@P_INCSTATUS", objDashboardInfo.strincRESStatus);
                cmd.Parameters.AddWithValue("@P_INPEALSTATUS", objDashboardInfo.intPealstatus);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_INTTYPE", objDashboardInfo.intType);
                cmd.Parameters.AddWithValue("@P_INT_DISTRICTID", objDashboardInfo.intDistrictid);
                cmd.Parameters.AddWithValue("@P_INT_YEAR", objDashboardInfo.intYearId);
                cmd.Parameters.AddWithValue("@P_INT_MONTH", objDashboardInfo.intMonthId);
                cmd.Parameters.AddWithValue("@P_INT_QUARTER", objDashboardInfo.intQuarter);
                cmd.Parameters.AddWithValue("@range", objDashboardInfo.intDaysPass);
                cmd.Parameters.AddWithValue("@P_DISTRICTDTLS", objDashboardInfo.strDistrictDtl);
                cmd.Parameters.AddWithValue("@P_FINACIALYR", objDashboardInfo.strFinacialYear);
                cmd.Parameters.AddWithValue("@P_INT_PEALUSERSTATUS", objDashboardInfo.intPealuserstatus);//ADDED BY SUROJ KUMAR PRADHAN TO SHOW PEAL DETAILS USERID WISE
                cmd.Parameters.AddWithValue("@P_INT_PEALUSEROPTION", objDashboardInfo.intPealuseroption);
                cmd.Parameters.AddWithValue("@P_Status", objDashboardInfo.StrStatus);
                cmd.Parameters.AddWithValue("@P_OPTION", objDashboardInfo.strOption);
                cmd.Parameters.AddWithValue("@P_PEALDISTWISE", objDashboardInfo.intPealdistwise);
                cmd.Parameters.AddWithValue("@P_VCHSECTORID", objDashboardInfo.intSecId);
                cmd.Parameters.AddWithValue("@P_VCH_FILTER_MODE", objDashboardInfo.strFilterMode);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "D" || objDashboardInfo.strAction == "EUC" || objDashboardInfo.strAction == "EUCD")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strApplied = Convert.ToString(sqlReader["APPLIED"]);
                            objDashboardInfo1.strApproved = Convert.ToString(sqlReader["APPROVED"]);
                            objDashboardInfo1.strRejected = Convert.ToString(sqlReader["REJECTED"]);
                            objDashboardInfo1.strPending = Convert.ToString(sqlReader["PENDING"]);
                            objDashboardInfo1.intDaysPass = Convert.ToString(sqlReader["DATEEXCEED"]);
                            objDashboardInfo1.QraiseTotal = Convert.ToString(sqlReader["QRaiseTotal"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "DM" || objDashboardInfo.strAction == "MEUC")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strPending = Convert.ToString(sqlReader["PENDING"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "DGM")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strApplied = Convert.ToString(sqlReader["APPLIED"]);
                            objDashboardInfo1.strApproved = Convert.ToString(sqlReader["APPROVED"]);
                            objDashboardInfo1.strPending = Convert.ToString(sqlReader["PENDING"]);
                            objDashboardInfo1.strRejected = Convert.ToString(sqlReader["Rejected"]);
                            objDashboardInfo1.QraiseTotal = Convert.ToString(sqlReader["QRaiseTotal"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "S")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strApplied = Convert.ToString(sqlReader["APPLIED"]);
                            objDashboardInfo1.strApproved = Convert.ToString(sqlReader["APPROVED"]);
                            objDashboardInfo1.strRejected = Convert.ToString(sqlReader["REJECTED"]);
                            objDashboardInfo1.strPending = Convert.ToString(sqlReader["PENDING"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "DI")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();

                            objDashboardInfo1.strPending = Convert.ToString(sqlReader["PENDING"]);
                            objDashboardInfo1.strRejected = Convert.ToString(sqlReader["REJECTED"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "FI")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.intincApplied = Convert.ToInt16(sqlReader["APPLIED"]);
                            objDashboardInfo1.intincSanctioned = Convert.ToInt16(sqlReader["SANCTIONED"]);
                            objDashboardInfo1.intincPending = Convert.ToInt16(sqlReader["PENDING"]);
                            objDashboardInfo1.intincRejected = Convert.ToInt16(sqlReader["REJECTED"]);
                            objDashboardInfo1.intincdistrubed = Convert.ToInt16(sqlReader["DISTRUBED"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "IS")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.StrincType = Convert.ToString(sqlReader["INCTYPE"]);
                            objDashboardInfo1.StrincSector = Convert.ToString(sqlReader["INCSECTOR"]);
                            objDashboardInfo1.strincStatus = Convert.ToString(sqlReader["INCSTATUS"]);
                            objDashboardInfo1.intincNoDays = Convert.ToInt16(sqlReader["INCNODAYS"]);

                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "SI")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strApplied = Convert.ToString(sqlReader["APPLIED"]);
                            objDashboardInfo1.strPending = Convert.ToString(sqlReader["PENDING"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "PPD")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.intSlNo = Convert.ToInt16(sqlReader["SerialNo"]);
                            objDashboardInfo1.decFee = Convert.ToDecimal(sqlReader["FEE"]);
                            objDashboardInfo1.strComapnyName = Convert.ToString(sqlReader["COMPANYNAME"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "PED")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strComapnyName = Convert.ToString(sqlReader["COMPANYNAME"]);
                            objDashboardInfo1.intEmployeement = Convert.ToInt16(sqlReader["ToatlEmployeement"]);
                            objDashboardInfo1.intDirectEmployee = Convert.ToInt16(sqlReader["DirectEmployee"]);
                            objDashboardInfo1.intContractualEmployee = Convert.ToInt16(sqlReader["ContractEmployee"]);
                            objDashboardInfo1.strDistrictName = Convert.ToString(sqlReader["DistrictName"]);
                            objDashboardInfo1.intExhistingemployee = Convert.ToInt16(sqlReader["ExistEmployee"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "PID")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strComapnyName = Convert.ToString(sqlReader["COMPANYNAME"]);
                            objDashboardInfo1.intEmployeement1 = Convert.ToString(sqlReader["TotCapitalApprove"]);
                            objDashboardInfo1.strDistrictName = Convert.ToString(sqlReader["DistrictName"]);

                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "DE" || objDashboardInfo.strAction == "TEUC" || objDashboardInfo.strAction == "TEUCDP")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.UniqueKey = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                            objDashboardInfo1.strServiceName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                            objDashboardInfo1.VCH_DEPT_NAME = Convert.ToString(sqlReader["Deptname"]);
                            objDashboardInfo1.strComapnyName = Convert.ToString(sqlReader["companyname"]);
                            objDashboardInfo1.intDaysPass = Convert.ToString(sqlReader["cntexceed"]);
                            objDashboardInfo1.strDistrictName = Convert.ToString(sqlReader["DISTNAME"]);
                            objDashboardInfo1.strDistApproved = Convert.ToString(sqlReader["DTM_CREATEDON"]);
                            objDashboardInfo1.ApplnLandORTPS = Convert.ToString(sqlReader["dtm_EndOfORTPS_Timeline"]);
                            objDashboardInfo1.InvestorName = Convert.ToString(sqlReader["vchFullname"]);
                            objDashboardInfo1.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);

                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "PA")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.intSlNo = Convert.ToInt16(sqlReader["Row#"]);
                            objDashboardInfo1.strDistrictName = Convert.ToString(sqlReader["DISTRICTNAME"]);
                            objDashboardInfo1.strComapnyName = Convert.ToString(sqlReader["COMPANYNAME"]);
                            objDashboardInfo1.VCH_DAYS = Convert.ToString(sqlReader["Days"]);
                            objDashboardInfo1.vchCapitalInvestment = Convert.ToString(sqlReader["CapitalInvestment"]);
                            objDashboardInfo1.vchEmployement = Convert.ToString(sqlReader["Employement"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "PGM")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.intSlNo = Convert.ToInt16(sqlReader["Row#"]);
                            objDashboardInfo1.strDistrictName = Convert.ToString(sqlReader["DISTRICTNAME"]);
                            objDashboardInfo1.strComapnyName = Convert.ToString(sqlReader["COMPANYNAME"]);
                            objDashboardInfo1.VCH_DAYS = Convert.ToString(sqlReader["Days"]);
                            objDashboardInfo1.vchCapitalInvestment = Convert.ToString(sqlReader["CapitalInvestment"]);
                            objDashboardInfo1.vchEmployement = Convert.ToString(sqlReader["Employement"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "Q")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.intSlNo = Convert.ToInt16(sqlReader["sl#"]);
                            objDashboardInfo1.strDistrictName = Convert.ToString(sqlReader["dept"]);
                            objDashboardInfo1.strComapnyName = Convert.ToString(sqlReader["Type of Query"]);
                            objDashboardInfo1.intDaysPass = Convert.ToString(sqlReader["daydiff"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "DGI")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.UniqueKey = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                            objDashboardInfo1.Distirict = Convert.ToString(sqlReader["Districtname"]);
                            objDashboardInfo1.strComapnyName = Convert.ToString(sqlReader["companyname"]);
                            objDashboardInfo1.intDaysPass = Convert.ToString(sqlReader["cntexceed"]);
                            objDashboardInfo1.ApplicationName = Convert.ToString(sqlReader["QueryType"]);
                            objDashboardInfo1.strServiceName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "DP" || objDashboardInfo.strAction == "DEUC" || objDashboardInfo.strAction == "EUCDP") //// Action DEUC added by Sushant Jena On Dt.27-Mar-2019 for Energy Utility Details
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.UniqueKey = Convert.ToString(sqlReader["APPCNT"]);
                            objDashboardInfo1.VCH_DEPT_NAME = Convert.ToString(sqlReader["DeptName"]);
                            objDashboardInfo1.strDistrictName = Convert.ToString(sqlReader["Districtname"]);
                            objDashboardInfo1.strComapnyName = Convert.ToString(sqlReader["companyname"]);
                            objDashboardInfo1.strServiceName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                            objDashboardInfo1.EndDate = Convert.ToString(sqlReader["dtm_EndOfORTPS_Timeline"]);
                            objDashboardInfo1.strDistApproved = Convert.ToString(sqlReader["DTM_CREATEDON"]);
                            objDashboardInfo1.InvestorName = Convert.ToString(sqlReader["vchFullname"]);
                            objDashboardInfo1.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "PC2")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.intSlNo = Convert.ToInt16(sqlReader["Row#"]);
                            objDashboardInfo1.strUnitname = Convert.ToString(sqlReader["COMPANYNAME"]);
                            objDashboardInfo1.intDaysPass = Convert.ToString(sqlReader["Days"]);

                            list.Add(objDashboardInfo1);
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
        #region "Added by Romalin panda on 22-sep-2017"
        public List<SWPDashboard> GetSPMGDashboardService(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_FINACIALYR", objDashboardInfo.UniqueKey);
                cmd.Parameters.AddWithValue("@P_INT_YEAR", objDashboardInfo.intYearId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    if (objDashboardInfo.strAction == "SP")
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.intSPMGRaised = Convert.ToInt32(dt.Rows[0]["RAISED"].ToString());
                        objDashboardInfo1.intSPMGResolved = Convert.ToInt32(dt.Rows[0]["RESOLVED"].ToString());
                        objDashboardInfo1.intSPMGPending = Convert.ToInt32(dt.Rows[0]["PENDING"].ToString());
                        objDashboardInfo1.intSPMGIssuePending = Convert.ToInt32(dt.Rows[0]["ISSUEPENDING"].ToString());
                        list.Add(objDashboardInfo1);
                    }
                    if (objDashboardInfo.strAction == "SPMGDIC")
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.intSPMGRaised = Convert.ToInt32(dt.Rows[0]["RAISED"].ToString());
                        objDashboardInfo1.intSPMGResolved = Convert.ToInt32(dt.Rows[0]["RESOLVED"].ToString());
                        objDashboardInfo1.intSPMGPending = Convert.ToInt32(dt.Rows[0]["PENDING"].ToString());
                        objDashboardInfo1.intSPMGIssuePending = Convert.ToInt32(dt.Rows[0]["ISSUEPENDING"].ToString());
                        list.Add(objDashboardInfo1);
                    }
                    if (objDashboardInfo.strAction == "SPMGALL")
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.intSPMGRaised = Convert.ToInt32(dt.Rows[0]["RAISED"].ToString());
                        objDashboardInfo1.intSPMGResolved = Convert.ToInt32(dt.Rows[0]["RESOLVED"].ToString());
                        objDashboardInfo1.intSPMGPending = Convert.ToInt32(dt.Rows[0]["PENDING"].ToString());
                        objDashboardInfo1.intSPMGIssuePending = Convert.ToInt32(dt.Rows[0]["ISSUEPENDING"].ToString());
                        list.Add(objDashboardInfo1);
                    }
                    if (objDashboardInfo.strAction == "SPMGI")
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.intSPMGRaised = Convert.ToInt32(dt.Rows[0]["RAISED"].ToString());
                        objDashboardInfo1.intSPMGResolved = Convert.ToInt32(dt.Rows[0]["RESOLVED"].ToString());
                        objDashboardInfo1.intSPMGPending = Convert.ToInt32(dt.Rows[0]["PENDING"].ToString());
                        objDashboardInfo1.intSPMGIssuePending = Convert.ToInt32(dt.Rows[0]["ISSUEPENDING"].ToString());
                        list.Add(objDashboardInfo1);
                    }
                }
                conn.Close();
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        #endregion
        #region "Added by Romalin anda on 22-sep-2017"
        public List<SWPDashboard> GetDashboardServiceStatusInnerDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_INTDEPTID", objDashboardInfo.intDeptId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "S")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strApplied = Convert.ToString(sqlReader["APPLIED"]);
                            objDashboardInfo1.strApproved = Convert.ToString(sqlReader["APPROVED"]);
                            objDashboardInfo1.strRejected = Convert.ToString(sqlReader["REJECTED"]);
                            objDashboardInfo1.strPending = Convert.ToString(sqlReader["PENDING"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "SI")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strApplied = Convert.ToString(sqlReader["APPLIED"]);
                            objDashboardInfo1.strPending = Convert.ToString(sqlReader["PENDING"]);
                            list.Add(objDashboardInfo1);
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

        #region Added by nibedita behera on 22-09-2017

        public List<SWPDashboard> GetDashboardCSRDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT ISNULL(TotalProject,0) TotalProject,ISNULL(TotalAmount,0) TotalAmount FROM V_CSR_Projects WHERE investorId=" + objDashboardInfo.intInvestorId;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {

                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.TotalProject = Convert.ToString(sqlReader["TotalProject"]);
                        objDashboardInfo1.TotalAmount = Convert.ToString(sqlReader["TotalAmount"]);
                        list.Add(objDashboardInfo1);

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

        public List<SWPDashboard> GetDashCSRDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);
                cmd.Parameters.AddWithValue("@P_DISTRICTDTLS", objDashboardInfo.Distirict);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "VA")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.TotalProject = Convert.ToString(sqlReader["TotalProject"]);
                            objDashboardInfo1.TotalAmount = Convert.ToString(sqlReader["TotalAmount"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "VM")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.TotalAmount = Convert.ToString(sqlReader["TotalAmount"]);
                            list.Add(objDashboardInfo1);
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
        public List<SWPDashboard> GetDashboardCSRCatDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);
                //cmd.Parameters.AddWithValue("@P_INT_DISTRICTID", objDashboardInfo.intDistrictid);
                //cmd.Parameters.AddWithValue("@P_INT_YEAR", objDashboardInfo.intYearId);
                //cmd.Parameters.AddWithValue("@P_INT_MONTH", objDashboardInfo.intMonthId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.AmountSpent = Convert.ToString(sqlReader["AmountSpent"]);
                        objDashboardInfo1.CategoryName = Convert.ToString(sqlReader["CategoryName"]);
                        list.Add(objDashboardInfo1);
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

        public List<SWPDashboard> GetDashboardPEALDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);
                cmd.Parameters.AddWithValue("@P_VCH_FILTER_MODE", objDashboardInfo.strFilterMode); //// Added by Sushant Jena On Dt.16-Aug-2018

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.StrStatus = Convert.ToString(sqlReader["STATUSNAME"]);
                        objDashboardInfo1.strApplied = Convert.ToString(sqlReader["APPLIEDINDAYS"]);
                        objDashboardInfo1.strPending = Convert.ToString(sqlReader["vchProposalNo"]);
                        objDashboardInfo1.strPealQuerystatus = Convert.ToString(sqlReader["intQueryStatus"]);
                        list.Add(objDashboardInfo1);
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
        public List<SWPDashboard> GetDashboardPEALFORMDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);
                cmd.Parameters.AddWithValue("@P_VCH_FILTER_MODE", objDashboardInfo.strFilterMode); //// Added by Sushant Jena On Dt.14-Aug-2018

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.strPending = Convert.ToString(sqlReader["PENDING"]);
                        objDashboardInfo1.strRejected = Convert.ToString(sqlReader["REJECTED"]);
                        list.Add(objDashboardInfo1);
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

        public List<SWPDashboard> GetDashboardCICGGrid(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_INT_YEAR", objDashboardInfo.intYearId);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "CICG")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.IndustryName = Convert.ToString(sqlReader["IndustryName"]);
                            objDashboardInfo1.InspectorName = Convert.ToString(sqlReader["InspectorName"]);
                            objDashboardInfo1.TOTALHOUR = Convert.ToString(sqlReader["TOTALHOUR"]);
                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "CICGStatus")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.Block = Convert.ToString(sqlReader["Block"]);
                            objDashboardInfo1.CICGDate = Convert.ToString(sqlReader["CICGDate"]);
                            objDashboardInfo1.Distict = Convert.ToString(sqlReader["Distict"]);
                            objDashboardInfo1.EndDate = Convert.ToString(sqlReader["EndDate"]);
                            objDashboardInfo1.IndustryName = Convert.ToString(sqlReader["IndustryName"]);
                            objDashboardInfo1.InspectingDept = Convert.ToString(sqlReader["InspectingDept"]);
                            objDashboardInfo1.InspectionDate = Convert.ToString(sqlReader["InspectionDate"]);
                            objDashboardInfo1.InspectorName = Convert.ToString(sqlReader["InspectorName"]);
                            objDashboardInfo1.InspectorRemark = Convert.ToString(sqlReader["InspectorRemark"]);
                            objDashboardInfo1.RescheduledDate = Convert.ToString(sqlReader["RescheduledDate"]);
                            objDashboardInfo1.StartDate = Convert.ToString(sqlReader["StartDate"]);
                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "VCINVDET")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.IndustryName = Convert.ToString(sqlReader["Department"]);
                            objDashboardInfo1.InspectorName = Convert.ToString(sqlReader["InspectorName"]);
                            objDashboardInfo1.TOTALHOUR = Convert.ToString(sqlReader["NoOfDays"]);
                            list.Add(objDashboardInfo1);
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
        #region "Added by Romalin panda on 23-sep-2017"
        public List<SWPDashboard> GetCICGDashboardService(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_INTDEPTID", objDashboardInfo.intDeptId);
                cmd.Parameters.AddWithValue("@P_INT_MONTH", objDashboardInfo.intMonthId);
                cmd.Parameters.AddWithValue("@P_INT_YEAR", objDashboardInfo.intYearId);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "VCI")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.INT_INS_SCHEDULED = Convert.ToInt32(sqlReader["INT_INS_SCHEDULED"]);
                            objDashboardInfo1.INT_INS_COMPLETED = Convert.ToInt32(sqlReader["INT_INS_COMPLETED"]);
                            objDashboardInfo1.INT_UNATTENDED_INS = Convert.ToInt32(sqlReader["INT_UNATTENDED_INS"]);
                            objDashboardInfo1.INT_REPORT_PENDING = Convert.ToInt32(sqlReader["INT_REPORT_PENDING"]);
                            objDashboardInfo1.INT_REPORTNOT_UPLOADED = Convert.ToInt32(sqlReader["INT_REPORTNOT_UPLOADED"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "VCINV")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.INT_INS_SCHEDULED = Convert.ToInt32(sqlReader["INT_INS_SCHEDULED"]);
                            objDashboardInfo1.INT_INS_COMPLETED = Convert.ToInt32(sqlReader["INT_INS_COMPLETED"]);
                            objDashboardInfo1.INT_UNATTENDED_INS = Convert.ToInt32(sqlReader["INT_UNATTENDED_INS"]);
                            objDashboardInfo1.INT_REPORTNOT_UPLOADED = Convert.ToInt32(sqlReader["INT_REPORTNOT_UPLOADED"]);
                            list.Add(objDashboardInfo1);
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


        #region "Added by Romalin panda on 22-sep-2017"
        public List<SWPDashboard> GetSPMGDetailService(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);
                cmd.Parameters.AddWithValue("@P_INT_YEAR", objDashboardInfo.intYearId);
                cmd.Parameters.AddWithValue("@P_Status", objDashboardInfo.StrStatus);
                cmd.Parameters.AddWithValue("@P_INTDEPTID", objDashboardInfo.intDeptId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "SPD")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.ProjectName = (sqlReader["Project_Name"]).ToString();
                            objDashboardInfo1.Project_Department = (sqlReader["Project_Department"]).ToString();
                            objDashboardInfo1.Type_Of_Issue = (sqlReader["Type_Of_Issue"]).ToString();
                            objDashboardInfo1.Issue_Date = (sqlReader["Issue_Date"]).ToString();
                            objDashboardInfo1.Issue_Description = (sqlReader["Issue_Description"]).ToString();
                            objDashboardInfo1.IssueCategory = (sqlReader["Issue_Category"]).ToString();
                            objDashboardInfo1.Name_Of_Investor = (sqlReader["Name_Of_Investor"]).ToString();
                            objDashboardInfo1.Pending_Department = (sqlReader["Pending_Department"]).ToString();
                            objDashboardInfo1.Pending_Department_Type = (sqlReader["Pending_Department_Type"]).ToString();
                            objDashboardInfo1.PendingDays = (sqlReader["Pending_Days"]).ToString();
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "SPDDIC")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.ProjectName = (sqlReader["Project_Name"]).ToString();
                            objDashboardInfo1.Project_Department = (sqlReader["Project_Department"]).ToString();
                            objDashboardInfo1.Type_Of_Issue = (sqlReader["Type_Of_Issue"]).ToString();
                            objDashboardInfo1.Issue_Date = (sqlReader["Issue_Date"]).ToString();
                            objDashboardInfo1.Issue_Description = (sqlReader["Issue_Description"]).ToString();
                            objDashboardInfo1.IssueCategory = (sqlReader["Issue_Category"]).ToString();
                            objDashboardInfo1.Name_Of_Investor = (sqlReader["Name_Of_Investor"]).ToString();
                            objDashboardInfo1.Pending_Department = (sqlReader["Pending_Department"]).ToString();
                            objDashboardInfo1.Pending_Department_Type = (sqlReader["Pending_Department_Type"]).ToString();
                            objDashboardInfo1.PendingDays = (sqlReader["Pending_Days"]).ToString();
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "SPMGI")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.ProjectName = (sqlReader["Project_Name"]).ToString();
                            objDashboardInfo1.Project_Department = (sqlReader["Project_Department"]).ToString();
                            objDashboardInfo1.Type_Of_Issue = (sqlReader["Type_Of_Issue"]).ToString();
                            objDashboardInfo1.Issue_Date = (sqlReader["Issue_Date"]).ToString();
                            objDashboardInfo1.Issue_Description = (sqlReader["Issue_Description"]).ToString();
                            objDashboardInfo1.IssueCategory = (sqlReader["Issue_Category"]).ToString();
                            objDashboardInfo1.Name_Of_Investor = (sqlReader["Name_Of_Investor"]).ToString();
                            objDashboardInfo1.Pending_Department = (sqlReader["Pending_Department"]).ToString();
                            objDashboardInfo1.Pending_Department_Type = (sqlReader["Pending_Department_Type"]).ToString();
                            objDashboardInfo1.PendingDays = (sqlReader["Pending_Days"]).ToString();
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "SPMGD")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.ProjectName = (sqlReader["Project_Name"]).ToString();
                            objDashboardInfo1.Project_Department = (sqlReader["Project_Department"]).ToString();
                            objDashboardInfo1.Type_Of_Issue = (sqlReader["Type_Of_Issue"]).ToString();
                            objDashboardInfo1.Issue_Date = (sqlReader["Issue_Date"]).ToString();
                            objDashboardInfo1.Issue_Description = (sqlReader["Issue_Description"]).ToString();
                            objDashboardInfo1.IssueCategory = (sqlReader["Issue_Category"]).ToString();
                            objDashboardInfo1.Name_Of_Investor = (sqlReader["Name_Of_Investor"]).ToString();
                            objDashboardInfo1.Pending_Department = (sqlReader["Pending_Department"]).ToString();
                            objDashboardInfo1.Pending_Department_Type = (sqlReader["Pending_Department_Type"]).ToString();
                            objDashboardInfo1.PendingDays = (sqlReader["Pending_Days"]).ToString();
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "DEPT")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.ProjectName = (sqlReader["Project_Name"]).ToString();
                            objDashboardInfo1.Project_Department = (sqlReader["Project_Department"]).ToString();
                            objDashboardInfo1.Type_Of_Issue = (sqlReader["Type_Of_Issue"]).ToString();
                            objDashboardInfo1.Issue_Date = (sqlReader["Issue_Date"]).ToString();
                            objDashboardInfo1.Issue_Description = (sqlReader["Issue_Description"]).ToString();
                            objDashboardInfo1.IssueCategory = (sqlReader["Issue_Category"]).ToString();
                            objDashboardInfo1.Name_Of_Investor = (sqlReader["Name_Of_Investor"]).ToString();
                            objDashboardInfo1.Pending_Department = (sqlReader["Pending_Department"]).ToString();
                            objDashboardInfo1.Pending_Department_Type = (sqlReader["Pending_Department_Type"]).ToString();
                            objDashboardInfo1.PendingDays = (sqlReader["Pending_Days"]).ToString();
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "DEPTDIST")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.ProjectName = (sqlReader["Project_Name"]).ToString();
                            objDashboardInfo1.Project_Department = (sqlReader["Project_Department"]).ToString();
                            objDashboardInfo1.Type_Of_Issue = (sqlReader["Type_Of_Issue"]).ToString();
                            objDashboardInfo1.Issue_Date = (sqlReader["Issue_Date"]).ToString();
                            objDashboardInfo1.Issue_Description = (sqlReader["Issue_Description"]).ToString();
                            objDashboardInfo1.IssueCategory = (sqlReader["Issue_Category"]).ToString();
                            objDashboardInfo1.Name_Of_Investor = (sqlReader["Name_Of_Investor"]).ToString();
                            objDashboardInfo1.Pending_Department = (sqlReader["Pending_Department"]).ToString();
                            objDashboardInfo1.Pending_Department_Type = (sqlReader["Pending_Department_Type"]).ToString();
                            objDashboardInfo1.PendingDays = (sqlReader["Pending_Days"]).ToString();
                            list.Add(objDashboardInfo1);
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
        #region "Added by Romalin panda on 06-Oct-2017"
        public List<SWPDashboard> GetServiceQuery(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard_SERVICE_Query";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_DEPTID", "0");
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                cmd.Parameters.AddWithValue("@P_MONTH", "0");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {

                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.intTotalQueryRaised = Convert.ToInt32(sqlReader["Query Rasied"]);
                        objDashboardInfo1.intTotalQueryResponse = Convert.ToInt32(sqlReader["Query Resolved"]);
                        //objDashboardInfo1.intTotQuerynotRecTimeline = Convert.ToInt32(sqlReader["Query Revert"]);
                        objDashboardInfo1.intTotQuerynotRecTimeline = Convert.ToInt32(sqlReader["Query Past"]);
                        objDashboardInfo1.intTotalQuery = Convert.ToInt32(sqlReader["Count of Queries"]);
                        objDashboardInfo1.intTotalQueryPending = Convert.ToInt32(sqlReader["Avg Time Taken"]);
                        list.Add(objDashboardInfo1);
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

        #region added by nibedita behera on 07-10-2017
        //public override DataTable FillFinacialYear(string SAction)
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    try
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "USP_Dashboard";
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@P_ACTION", SAction);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable objdt = new DataTable();
        //        da.Fill(objdt);
        //        return objdt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }
        //}
        public List<SWPDashboard> FillFinacialYear(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "FS")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.intYearId = Convert.ToInt32(sqlReader["FIYearID"]);
                            objDashboardInfo1.Year = (sqlReader["FIYear"]).ToString();
                            list.Add(objDashboardInfo1);
                        }
                        else
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.intYearId = Convert.ToInt32(sqlReader["ID"]);
                            objDashboardInfo1.Year = (sqlReader["FIYear"]).ToString();
                            list.Add(objDashboardInfo1);
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
        #region "Added by romalin Panda on 22-09-2017"
        public List<SWPDashboard> GetDashboardServiceIncentiveDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INCT_APP_COUNT_DASHBOARD";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USER_ID", objDashboardInfo.intUserid);
                //cmd.Parameters.AddWithValue("@P_INT_YEAR", objDashboardInfo.intYearId);
                cmd.Parameters.AddWithValue("@P_INT_QTR", objDashboardInfo.intQuarter);
                cmd.Parameters.AddWithValue("@P_INT_DISTRICT_CODE", objDashboardInfo.intDistrictid);
                cmd.Parameters.AddWithValue("@P_VCH_FINACIAL_YEAR", objDashboardInfo.strFinacialYear);//added by suroj on 26-10-17 to check finacial yr
                cmd.Parameters.AddWithValue("@P_VCH_FILTER_MODE", objDashboardInfo.strFilterMode); //// Added by sushant jena On Dt.16-Aug-2018

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "B")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.INCAPLLIED = Convert.ToString(sqlReader["Drafted_App"]);
                            objDashboardInfo1.INCSANCTIONED = Convert.ToString(sqlReader["Approved_App"]);
                            objDashboardInfo1.INCPENDING = Convert.ToString(sqlReader["Scrutiny_App"]);
                            objDashboardInfo1.INCREJECTED = Convert.ToString(sqlReader["Rejected_App"]);
                            objDashboardInfo1.strInctPending30Days = Convert.ToString(sqlReader["Pending_More_30_Days"]); //// Added by Sushant Jena on 03-Apr-2018 
                            objDashboardInfo1.strIncMean = Convert.ToString(sqlReader["Incentive_Mean"]); //Added by Bhagyashree Das on 07-Dec-2020
                            objDashboardInfo1.strIncMedian = Convert.ToString(sqlReader["Incentive_Median"]); //Added by Bhagyashree Das on 07-Dec-2020

                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "C")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strINCCompanyname = Convert.ToString(sqlReader["vchEnterpriseName"]);
                            objDashboardInfo1.Distict = Convert.ToString(sqlReader["vchDistrictName"]);// added by nibedita behera on 06-12-2017 for district name
                            objDashboardInfo1.strIncentiveSector = Convert.ToString(sqlReader["VCH_SECTOR"]);
                            objDashboardInfo1.strIncentiveStatus = Convert.ToString(sqlReader["vchStatus"]);
                            objDashboardInfo1.intDaysPass = Convert.ToString(sqlReader["Days"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "D")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strINCCompanyname = Convert.ToString(sqlReader["vchEnterpriseName"]);
                            objDashboardInfo1.Distict = Convert.ToString(sqlReader["vchDistrictName"]);// added by nibedita behera on 06-12-2017 for district name
                            objDashboardInfo1.strIncentiveSector = Convert.ToString(sqlReader["VCH_SECTOR"]);
                            objDashboardInfo1.strIncentiveStatus = Convert.ToString(sqlReader["vchStatus"]);
                            objDashboardInfo1.intDaysPass = Convert.ToString(sqlReader["Days"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "E")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strINCCompanyname = Convert.ToString(sqlReader["vchEnterpriseName"]);
                            objDashboardInfo1.Distict = Convert.ToString(sqlReader["vchDistrictName"]);// added by nibedita behera on 06-12-2017 for district name
                            objDashboardInfo1.strIncentiveSector = Convert.ToString(sqlReader["VCH_SECTOR"]);
                            objDashboardInfo1.strIncentiveStatus = Convert.ToString(sqlReader["vchStatus"]);
                            objDashboardInfo1.intDaysPass = Convert.ToString(sqlReader["Days"]);
                            list.Add(objDashboardInfo1);
                        }
                        if (objDashboardInfo.strAction == "F")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strINCCompanyname = Convert.ToString(sqlReader["vchEnterpriseName"]);
                            objDashboardInfo1.Distict = Convert.ToString(sqlReader["vchDistrictName"]);// added by nibedita behera on 06-12-2017 for district name
                            objDashboardInfo1.strIncentiveSector = Convert.ToString(sqlReader["VCH_SECTOR"]);
                            objDashboardInfo1.strIncentiveStatus = Convert.ToString(sqlReader["vchStatus"]);
                            objDashboardInfo1.intDaysPass = Convert.ToString(sqlReader["Days"]);
                            list.Add(objDashboardInfo1);
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

        public int GetDepartment(string userid)
        {

            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select [dbo].[UDF_GetdeptId](" + userid + ")";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                int val = Convert.ToInt32(cmd.ExecuteScalar());

                //sqlReader.Close();
                return val;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }

        /// <summary>
        /// Added By Pritiprangya Pattanaik on 15-jan-2018 to get SPMG Department id
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        public int GetSPMGDepartment(string deptid)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select isnull(intDeptid,0) as intDeptid from t_spmg_department where intMapDeptId=" + deptid;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                int val = Convert.ToInt32(cmd.ExecuteScalar());
                return val;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }

        public List<SWPDashboard> GetInvestorPealDtls(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                //cmd.Parameters.AddWithValue("@P_USER_ID", objDashboardInfo.intUserid);

                cmd.Parameters.AddWithValue("@P_VCH_PROPOSALNO", objDashboardInfo.strPealProposalno);
                cmd.Parameters.AddWithValue("@P_INT_QUERYSTATUS", objDashboardInfo.strPealQuerystatus);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "PCS")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strPealDays = Convert.ToInt16(sqlReader["days"]);
                            objDashboardInfo1.strPealStatus = Convert.ToString(sqlReader["status"]);
                            objDashboardInfo1.strPealRemark = Convert.ToString(sqlReader["vchRemarks"]);


                            list.Add(objDashboardInfo1);
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

        #region "Added by nibedita behera on 18-07-2017"
        public List<SWPDashboard> GETAMS(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_OPTION", objDashboardInfo.strOption);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "AMS")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.strUnitname = Convert.ToString(sqlReader["UNITNAME"]);
                            objDashboardInfo1.strNodalPersonName = Convert.ToString(sqlReader["PERSONNAME"]);
                            objDashboardInfo1.intPendingdays = Convert.ToInt16(sqlReader["AMSDAYS"]);
                            objDashboardInfo1.IntProposeid = Convert.ToInt16(sqlReader["PID"]);
                            list.Add(objDashboardInfo1);
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

        //added by nibedita behera on 09-11-2017 for cicg dept id
        public string GetCIFDepartmentid(string userid)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select [dbo].[UDF_GetcCIFdeptId](" + userid + ")";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string val = Convert.ToString(cmd.ExecuteScalar());

                return val;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
        }

        public List<SWPDashboard> BindCICGDepartment(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.intDeptId = Convert.ToInt32(sqlReader["CICGDeptId"]);
                        objDashboardInfo1.VCH_DEPT_NAME = sqlReader["CICGDept"].ToString();
                        list.Add(objDashboardInfo1);
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

        #region "Added by nibedita behera on 13-12-2017 for land details"
        public List<SWPDashboard> GETLandDetails(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_LandDetails_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_STATUS", objDashboardInfo.intStatus);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                cmd.Parameters.AddWithValue("@P_DIST_ID", objDashboardInfo.intDistrictid);
                cmd.Parameters.AddWithValue("@P_DIST_ID_IPC", objDashboardInfo.intDeptId);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objDashboardInfo.strAction == "LANDV")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.LandAssessment = Convert.ToString(sqlReader["LANDASSESMENT"]);
                            objDashboardInfo1.PropNoForLand = Convert.ToString(sqlReader["PROPOSALSENT"]);
                            objDashboardInfo1.AreaAllotLand = Convert.ToString(sqlReader["AREAALLOTED"]);
                            objDashboardInfo1.ApplnLandORTPS = Convert.ToString(sqlReader["ORTPSATIME"]);
                            objDashboardInfo1.ApplnLandAllotedByIDCO = Convert.ToString(sqlReader["AllotedByIDCO"]);

                            list.Add(objDashboardInfo1);
                        }
                        else if (objDashboardInfo.strAction == "LANDVDet")
                        {
                            SWPDashboard objDashboardInfo1 = new SWPDashboard();
                            objDashboardInfo1.PropNoForLand = Convert.ToString(sqlReader["vchProposalNo"]);
                            objDashboardInfo1.strComapnyName = Convert.ToString(sqlReader["Name Of The Company"]);
                            objDashboardInfo1.AreaAllotLand = Convert.ToString(sqlReader["Land (In Acres)"]);
                            objDashboardInfo1.RequestDate = Convert.ToString(sqlReader["Application Date"]);
                            objDashboardInfo1.Distirict = Convert.ToString(sqlReader["District"]);
                            list.Add(objDashboardInfo1);
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

        #region PEAL Service INCENTIVE Query
        public List<SWPDashboard> GetPEALQuery(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard_Query";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                cmd.Parameters.AddWithValue("@P_VCH_FROM_DATE", objDashboardInfo.strFromDate); //Added by Bhagyashree Das on Dt. 03-Feb-2021
                cmd.Parameters.AddWithValue("@P_VCH_TO_DATE", objDashboardInfo.strToDate); //Added by Bhagyashree Das on Dt. 03-Feb-2021
                cmd.Parameters.AddWithValue("@Int_District_Id", objDashboardInfo.intDistrictid);
                cmd.Parameters.AddWithValue("@Int_SectorId", objDashboardInfo.intSecId);
                cmd.Parameters.AddWithValue("@INTPROJECTTYPE", objDashboardInfo.intType);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.strPEALQueryRaised = Convert.ToString(sqlReader["QueryRasiedPeal"]);
                        objDashboardInfo1.strPEALQueryResolved = Convert.ToString(sqlReader["QueryResolvedPeal"]);
                        objDashboardInfo1.strPEALQueryPending = Convert.ToString(sqlReader["QueryPendingPeal"]);
                        objDashboardInfo1.strPEALQueryPendingPast = Convert.ToString(sqlReader["QueryPastPeal"]);
                        objDashboardInfo1.strPEALQueryAvg = Convert.ToString(sqlReader["AvgTimeTakenPeal"]);
                        objDashboardInfo1.strMaxRaiseQuery = Convert.ToString(sqlReader["QueryMaxTimePeal"]);
                        objDashboardInfo1.strMinRaiseQuery = Convert.ToString(sqlReader["QueryMinTimePeal"]);
                        objDashboardInfo1.strMedianRaiseQuery = Convert.ToString(sqlReader["QueryMedianTimePeal"]);
                        list.Add(objDashboardInfo1);
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

        public List<SWPDashboard> GetServicesQuery(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard_Query";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@Int_District_Id", objDashboardInfo.intDistrictid);
                cmd.Parameters.AddWithValue("@Int_Department_Id", objDashboardInfo.intDeptId);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                cmd.Parameters.AddWithValue("@P_VCH_FROM_DATE", objDashboardInfo.strFromDate); //Added by Bhagyashree Das on Dt. 03-Feb-2021
                cmd.Parameters.AddWithValue("@P_VCH_TO_DATE", objDashboardInfo.strToDate); //Added by Bhagyashree Das on Dt. 03-Feb-2021

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.strTotalQueryRaised = Convert.ToString(sqlReader["QueryRasiedService"]);
                        objDashboardInfo1.strTotalQueryResponse = Convert.ToString(sqlReader["QueryResolvedService"]);
                        objDashboardInfo1.strTotalQueryPending = Convert.ToString(sqlReader["QueryPendingService"]);
                        objDashboardInfo1.strTotQuerynotRecTimeline = Convert.ToString(sqlReader["QueryPastService"]);
                        objDashboardInfo1.strAvgRaiseQuery = Convert.ToString(sqlReader["AvgTimeTakenService"]);
                        objDashboardInfo1.strMaxRaiseQuery = Convert.ToString(sqlReader["QueryMaxTimeService"]);
                        objDashboardInfo1.strMinRaiseQuery = Convert.ToString(sqlReader["QueryMinTimeService"]);
                        objDashboardInfo1.strMedianRaiseQuery = Convert.ToString(sqlReader["QueryMedianTimeService"]);
                        list.Add(objDashboardInfo1);
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

        public List<SWPDashboard> GetiNCENTIVEQuery(SWPDashboard objDashboardInfo)
        {
            List<SWPDashboard> list = new List<SWPDashboard>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard_Query";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@Int_District_Id", objDashboardInfo.intDistrictid);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                cmd.Parameters.AddWithValue("@P_VCH_FROM_DATE", objDashboardInfo.strFromDate); //Added by Bhagyashree Das on Dt. 03-Feb-2021
                cmd.Parameters.AddWithValue("@P_VCH_TO_DATE", objDashboardInfo.strToDate); //Added by Bhagyashree Das on Dt. 03-Feb-2021

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                        objDashboardInfo1.strTotalQueryRaised = Convert.ToString(sqlReader["QueryRasiedInctv"]);
                        objDashboardInfo1.strTotalQueryResponse = Convert.ToString(sqlReader["QueryResolvedInctv"]);
                        objDashboardInfo1.strTotalQueryPending = Convert.ToString(sqlReader["QueryPendingInctv"]);
                        objDashboardInfo1.strTotQuerynotRecTimeline = Convert.ToString(sqlReader["QueryPastInctv"]);
                        objDashboardInfo1.strAvgRaiseQuery = Convert.ToString(sqlReader["AvgTimeTakenInctv"]);
                        objDashboardInfo1.strMaxRaiseQuery = Convert.ToString(sqlReader["QueryMaxTimeIncentive"]);
                        objDashboardInfo1.strMinRaiseQuery = Convert.ToString(sqlReader["QueryMinTimeIncentive"]);
                        objDashboardInfo1.strMedianRaiseQuery = Convert.ToString(sqlReader["QueryMedianTimeIncentive"]);
                        list.Add(objDashboardInfo1);
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

        public DataTable GetPEALQueryDetails(SWPDashboard objDashboardInfo)
        {
            // List<SWPDashboard> list = new List<SWPDashboard>();
            DataTable dt = new DataTable();
            //SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard_Query";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                // cmd.Parameters.AddWithValue("@P_DEPTID", objDashboardInfo.intDeptId);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                cmd.Parameters.AddWithValue("@STATUS", objDashboardInfo.intStatus);
                cmd.Parameters.AddWithValue("@intProjectType", objDashboardInfo.intType);
                cmd.Parameters.AddWithValue("@Int_SectorId", objDashboardInfo.intSecId);
                cmd.Parameters.AddWithValue("@Int_District_Id", objDashboardInfo.intDistrictid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                // sqlReader = cmd.ExecuteReader();

                //if (sqlReader.HasRows)
                //{
                //    while (sqlReader.Read())
                //    {
                //        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                //        objDashboardInfo1.QueryapplicationNo = Convert.ToString(sqlReader["vchProposalNo"]);
                //        objDashboardInfo1.QueryRemarks = Convert.ToString(sqlReader["vchRemarks"]);
                //        objDashboardInfo1.QueryApplicationDate = Convert.ToString(sqlReader["APPN dATE"]);
                //        objDashboardInfo1.QueryDate = Convert.ToString(sqlReader["QUERY DATE"]);
                //        objDashboardInfo1.QueryInvestorName = Convert.ToString(sqlReader["vchCompName"]);
                //        objDashboardInfo1.strPealQuerystatus = Convert.ToString(sqlReader["Query"]);
                //        objDashboardInfo1.QueryAvgTime = Convert.ToString(sqlReader["Avg_time"]);
                //        list.Add(objDashboardInfo1);
                //    }
                //}
                //sqlReader.Close();
                //return list;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return dt;

        }

        public DataTable GetServicesQueryDetails(SWPDashboard objDashboardInfo)
        {
            //List<SWPDashboard> list = new List<SWPDashboard>();
            DataTable dt = new DataTable();
            //SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard_Query";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_YEAR", objDashboardInfo.Year);
                cmd.Parameters.AddWithValue("@STATUS", objDashboardInfo.intStatus);
                cmd.Parameters.AddWithValue("@Int_District_Id", objDashboardInfo.intDistrictid);
                cmd.Parameters.AddWithValue("@Int_Department_Id", objDashboardInfo.intDeptId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                //sqlReader = cmd.ExecuteReader();

                //if (sqlReader.HasRows)
                //{
                //    while (sqlReader.Read())
                //    {
                //        SWPDashboard objDashboardInfo1 = new SWPDashboard();
                //        objDashboardInfo1.QueryapplicationNo = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                //        objDashboardInfo1.QueryRemarks = Convert.ToString(sqlReader["vchRemarks"]);
                //        objDashboardInfo1.QueryApplicationDate = Convert.ToString(sqlReader["APPN dATE"]);
                //        objDashboardInfo1.QueryDate = Convert.ToString(sqlReader["QUERY DATE"]);
                //        objDashboardInfo1.QueryInvestorName = Convert.ToString(sqlReader["VCH_INVESTOR_NAME"]);
                //        objDashboardInfo1.strPealQuerystatus = Convert.ToString(sqlReader["Query"]);
                //        objDashboardInfo1.QueryAvgTime = Convert.ToString(sqlReader["Avg_time"]);
                //        list.Add(objDashboardInfo1);
                //    }
                //}
                //sqlReader.Close();
                //return list;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return dt;
        }
        #endregion

        #region Added by Sushant Jena on Dt.09-Mar-2018

        /////// Get Department Name        
        public string GetDeptName(int deptId)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT [dbo].[UDF_GET_DEPT_NAME](" + deptId + ")";

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string val = Convert.ToString(cmd.ExecuteScalar());

                //sqlReader.Close();
                return val;
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

        #region Added by Sushant Jena

        /// <summary>
        /// Added by Sushant Jena on Dt.14-Aug-2018
        /// To get child unit name with parent unit name
        /// </summary>
        /// <param name="objDashboardInfo"></param>
        /// <returns></returns>
        public DataTable getInvestorChildUnit(SWPDashboard objDashboardInfo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }
            return dt;
        }

        #endregion

        public DataTable getInvestorGrievance(SWPDashboard objDashboardInfo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }
            return dt;
        }

        public DataTable getDepartmentGrievance(SWPDashboard objDashboardInfo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                cmd.Parameters.AddWithValue("@P_INT_DISTRICTID", objDashboardInfo.intDistrictId);
                cmd.Parameters.AddWithValue("@P_FINACIALYR", objDashboardInfo.strFinacialYear);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }
            return dt;
        }

        public DataTable getGrievanceDetails(SWPDashboard objDashboardInfo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INVESTORID", objDashboardInfo.intInvestorId);
                cmd.Parameters.AddWithValue("@P_Status", objDashboardInfo.StrStatus);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }
            return dt;
        }

        public DataTable DepartmentGrievanceDetails(SWPDashboard objDashboardInfo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Dashboard";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objDashboardInfo.strAction);
                cmd.Parameters.AddWithValue("@P_INT_DISTRICTID", objDashboardInfo.intDistrictId);
                cmd.Parameters.AddWithValue("@P_FINACIALYR", objDashboardInfo.strFinacialYear);
                cmd.Parameters.AddWithValue("@P_Status", objDashboardInfo.StrStatus);
                cmd.Parameters.AddWithValue("@P_USERID", objDashboardInfo.intUserid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }
            return dt;
        }
    }
}
