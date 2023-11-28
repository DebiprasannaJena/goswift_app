using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for MisReportServiceProvider
/// </summary>
public class MisReportServiceProvider : IMisReportServiceProvider
{
    #region "Member Variable"
    string ConnectionString = "AdminAppConnectionProd";
    SqlConnection gSqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    #endregion

    public MisReportServiceProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    #region Child Services Mis Report
    /// <summary>
    /// Function to get the details of the main grid for the CHild Services report
    /// </summary>
    /// <param name="objSearch">RptSearch object</param>
    /// <returns>List of type MIS_ChildServiceRpt</returns>
    public override List<MIS_ChildServiceRpt> View_ChildServices_MISReport(RptSearch objSearch)
    {
        List<MIS_ChildServiceRpt> list = new List<MIS_ChildServiceRpt>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_ChildServices_MISReport";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@intServiceId", objSearch.intServiceId);
            cmd.Parameters.AddWithValue("@intDepartmentId", objSearch.intDepartmentId);
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntYear", objSearch.intYear);
            cmd.Parameters.AddWithValue("@pIntDistrictId", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    MIS_ChildServiceRpt objChildService = new MIS_ChildServiceRpt();
                    objChildService.strParentName = Convert.ToString(sqlReader["parentName"]);
                    objChildService.strDeptName = Convert.ToString(sqlReader["nvchlevelname"]);
                    objChildService.intTotalApplication = Convert.ToInt32(sqlReader["cnt_Total"]);
                    objChildService.intAvgDaysApproval = Convert.ToInt32(sqlReader["cntAverage"]);
                    objChildService.intAvgDaysApprovalTotal = Convert.ToInt32(sqlReader["cntAverageTotal"]);
                    objChildService.intTotalApproved = Convert.ToInt32(sqlReader["cnt_approved"]);
                    objChildService.intTotalPending = Convert.ToInt32(sqlReader["cnt_pending"]);
                    objChildService.intTotalRejected = Convert.ToInt32(sqlReader["cnt_rejected"]);
                    objChildService.intTotalQueryRaised = Convert.ToInt32(sqlReader["cnt_QueryRaised"]);
                    objChildService.intTotalORTPSAtimelinePassed = Convert.ToInt32(sqlReader["cnt_ortps_Timeline"]);
                    objChildService.intKey = Convert.ToInt32(sqlReader["intleveldetailid"]);
                    objChildService.intCarryFwdPending = Convert.ToInt32(sqlReader["cnt_CarryFwd_Pending"]);
                    objChildService.intAllTotalPending = Convert.ToInt32(sqlReader["cnt_AllTotalPending"]);
                    if (string.Equals(objSearch.strActionCode, "s", StringComparison.OrdinalIgnoreCase))
                    {
                        objChildService.strDistName = Convert.ToString(sqlReader["distname"]);
                    }
                    list.Add(objChildService);
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

    public override List<MIS_ChildServiceRpt> View_ChildServices_MISReport_New(RptSearch objSearch)
    {
        List<MIS_ChildServiceRpt> list = new List<MIS_ChildServiceRpt>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_ChildServices_MISReport_Logic2";

            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@intServiceId", objSearch.intServiceId);
            cmd.Parameters.AddWithValue("@intDepartmentId", objSearch.intDepartmentId);
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntYear", objSearch.intYear);
            cmd.Parameters.AddWithValue("@pIntDistrictId", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);

            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }

            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    MIS_ChildServiceRpt objChildService = new MIS_ChildServiceRpt();

                    objChildService.strParentName = Convert.ToString(sqlReader["parentName"]);
                    objChildService.strDeptName = Convert.ToString(sqlReader["nvchlevelname"]);
                    objChildService.intTotalApplication = Convert.ToInt32(sqlReader["cnt_Total"]);
                    objChildService.intAvgDaysApproval = Convert.ToInt32(sqlReader["cntAverage"]);
                    objChildService.intAvgDaysApprovalTotal = Convert.ToInt32(sqlReader["cntAverageTotal"]);
                    objChildService.intTotalApproved = Convert.ToInt32(sqlReader["cnt_approved"]);
                    objChildService.intTotalPending = Convert.ToInt32(sqlReader["cnt_pending"]);
                    objChildService.intTotalRejected = Convert.ToInt32(sqlReader["cnt_rejected"]);
                    objChildService.intTotalQueryRaised = Convert.ToInt32(sqlReader["cnt_QueryRaised"]);
                    objChildService.intTotalORTPSAtimelinePassed = Convert.ToInt32(sqlReader["cnt_ortps_Timeline"]);
                    objChildService.intKey = Convert.ToInt32(sqlReader["intleveldetailid"]);
                    objChildService.intCarryFwdPending = Convert.ToInt32(sqlReader["cnt_CarryFwd_Pending"]);
                    objChildService.intAllTotalPending = Convert.ToInt32(sqlReader["cnt_AllTotalPending"]);
                    objChildService.intTotalDeferred = Convert.ToInt32(sqlReader["cnt_deferred"]); //// Added by Sushant Jena On Dated: 08-May-2020
                    objChildService.intTotalForwarded = Convert.ToInt32(sqlReader["cnt_forwarded"]);//// Added by Sushant Jena On Dated: 08-May-2020
                    objChildService.decMedian = Convert.ToInt32(sqlReader["cnt_median"]);//// Added by Sushant Jena On Dated: 25-Nov-2020
                    objChildService.intORTPSAtimeline = Convert.ToInt32(sqlReader["int_OLTPSTIMELINE"]);//Added by Bhagyashree Das on Dated: 21-Dec-2020
                    objChildService.intMinApprovalDays = Convert.ToInt32(sqlReader["minApprovalDays"]);//Added by Bhagyashree Das on Dated: 21-Dec-2020
                    objChildService.intMaxApprovalDays = Convert.ToInt32(sqlReader["maxApprovalDays"]);//Added by Bhagyashree Das on Dated: 21-Dec-2020

                    if (string.Equals(objSearch.strActionCode, "s", StringComparison.OrdinalIgnoreCase))
                    {
                        objChildService.strDistName = Convert.ToString(sqlReader["distname"]);
                    }

                    list.Add(objChildService);
                }
            }

            sqlReader.Close();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MIS");
        }
        finally
        {
            cmd = null;
        }

        return list;
    }

    /// <summary>
    /// Function to get the details for the application for the CHild Services report
    /// The same function is used to get the query raised report for the Child services main report
    /// </summary>
    /// <param name="objSearch"></param>
    /// <returns></returns>
    public override List<Mis_ChildServiceDtls> View_DetailsChildServices_MISReport(RptSearch objSearch)
    {
        List<Mis_ChildServiceDtls> list = new List<Mis_ChildServiceDtls>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_ChildServices_MISReport";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@intServiceId", objSearch.intServiceId);
            cmd.Parameters.AddWithValue("@intDepartmentId", objSearch.intDepartmentId);
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntYear", objSearch.intYear);
            cmd.Parameters.AddWithValue("@pIntDistrictId", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);

            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Mis_ChildServiceDtls objChildService = new Mis_ChildServiceDtls();
                    objChildService.ProposalNo = Convert.ToString(sqlReader["VCH_PROPOSALID"]);
                    objChildService.ServiceName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                    objChildService.strDepartment = Convert.ToString(sqlReader["nvchlevelname"]);
                    objChildService.strCompany = Convert.ToString(sqlReader["VCH_INV_NAME"]);
                    objChildService.strBlock = Convert.ToString(sqlReader["vchBlockName"]);
                    objChildService.FirstResponse = string.IsNullOrEmpty(Convert.ToString(sqlReader["first_response"])) ? "NA" : Convert.ToString(sqlReader["first_response"]);
                    objChildService.FirstTimeQuery = string.IsNullOrEmpty(Convert.ToString(sqlReader["first_Query"])) ? "NA" : Convert.ToString(sqlReader["first_Query"]);
                    objChildService.SecondResponse = string.IsNullOrEmpty(Convert.ToString(sqlReader["second_response"])) ? "NA" : Convert.ToString(sqlReader["second_response"]);
                    objChildService.SecondQuery = string.IsNullOrEmpty(Convert.ToString(sqlReader["second_Query"])) ? "NA" : Convert.ToString(sqlReader["second_Query"]);
                    objChildService.intRowCount = Convert.ToInt32(sqlReader["intRowCount"]);
                    objChildService.strDistName = Convert.ToString(sqlReader["distname"]);

                    list.Add(objChildService);
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


    public override List<Mis_ChildServiceDtls> View_ChildServices_ApplicationDtls_MISReport(RptSearch objSearch)
    {
        List<Mis_ChildServiceDtls> list = new List<Mis_ChildServiceDtls>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_ChildServices_MISReport";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@intServiceId", objSearch.intServiceId);
            cmd.Parameters.AddWithValue("@intDepartmentId", objSearch.intDepartmentId);
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntYear", objSearch.intYear);
            cmd.Parameters.AddWithValue("@pIntStatus", objSearch.intStatus);
            cmd.Parameters.AddWithValue("@pIntDistrictId", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Mis_ChildServiceDtls objChildService = new Mis_ChildServiceDtls();
                    objChildService.ProposalNo = Convert.ToString(sqlReader["VCH_PROPOSALID"]);
                    objChildService.ServiceName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                    objChildService.strCompany = Convert.ToString(sqlReader["VCH_INVESTOR_NAME"]);
                    objChildService.strBlock = Convert.ToString(sqlReader["vchBlockName"]);
                    objChildService.strApplicationNo = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                    objChildService.strApplicationDate = Convert.ToString(sqlReader["dtm_Payment_date"]);
                    objChildService.decPaymentAmt = Convert.ToDecimal(sqlReader["NUM_PAYMENT_AMOUNT"]);
                    objChildService.strDepartment = Convert.ToString(sqlReader["nvchlevelname"]);
                    objChildService.strDistName = Convert.ToString(sqlReader["distname"]);
                    //if query raised details are being fetched
                    if (string.Equals(objSearch.strActionCode, "sq", StringComparison.OrdinalIgnoreCase))
                    {
                        objChildService.FirstResponse = string.IsNullOrEmpty(Convert.ToString(sqlReader["first_response"])) ? "NA" : Convert.ToString(sqlReader["first_response"]);
                        objChildService.FirstTimeQuery = string.IsNullOrEmpty(Convert.ToString(sqlReader["first_Query"])) ? "NA" : Convert.ToString(sqlReader["first_Query"]);
                        objChildService.SecondResponse = string.IsNullOrEmpty(Convert.ToString(sqlReader["second_response"])) ? "NA" : Convert.ToString(sqlReader["second_response"]);
                        objChildService.SecondQuery = string.IsNullOrEmpty(Convert.ToString(sqlReader["second_Query"])) ? "NA" : Convert.ToString(sqlReader["second_Query"]);
                    }
                    objChildService.intRowCount = Convert.ToInt32(sqlReader["intRowCount"]);
                    objChildService.strRemarks = Convert.ToString(sqlReader["vchRemarks"]);
                    list.Add(objChildService);
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

    public override List<Mis_ChildServiceDtls> View_ChildServices_ApplicationDtls_MISReport_New(RptSearch objSearch)
    {
        List<Mis_ChildServiceDtls> list = new List<Mis_ChildServiceDtls>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_ChildServices_MISReport_Logic2";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@intServiceId", objSearch.intServiceId);
            cmd.Parameters.AddWithValue("@intDepartmentId", objSearch.intDepartmentId);
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntYear", objSearch.intYear);
            cmd.Parameters.AddWithValue("@pIntStatus", objSearch.intStatus);
            cmd.Parameters.AddWithValue("@pIntDistrictId", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Mis_ChildServiceDtls objChildService = new Mis_ChildServiceDtls();
                    objChildService.ProposalNo = Convert.ToString(sqlReader["VCH_PROPOSALID"]);
                    objChildService.ServiceName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                    objChildService.strCompany = Convert.ToString(sqlReader["VCH_INVESTOR_NAME"]);
                    objChildService.strBlock = Convert.ToString(sqlReader["vchBlockName"]);
                    objChildService.strApplicationNo = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                    objChildService.strApplicationDate = Convert.ToString(sqlReader["dtm_Payment_date"]);
                    objChildService.decPaymentAmt = Convert.ToDecimal(sqlReader["NUM_PAYMENT_AMOUNT"]);
                    objChildService.strDepartment = Convert.ToString(sqlReader["nvchlevelname"]);
                    objChildService.strDistName = Convert.ToString(sqlReader["distname"]);
                    if (string.Equals(objSearch.strActionCode, "sd", StringComparison.OrdinalIgnoreCase))
                    {
                        objChildService.strORTPSATimelineDate = Convert.ToString(sqlReader["dtm_EndOfORTPS_Timeline"]);
                        objChildService.strUsername = Convert.ToString(sqlReader["vchFullname"]);
                    }

                    //if query raised details are being fetched
                    if (string.Equals(objSearch.strActionCode, "sq", StringComparison.OrdinalIgnoreCase))
                    {
                        objChildService.FirstResponse = string.IsNullOrEmpty(Convert.ToString(sqlReader["first_response"])) ? "NA" : Convert.ToString(sqlReader["first_response"]);
                        objChildService.FirstTimeQuery = string.IsNullOrEmpty(Convert.ToString(sqlReader["first_Query"])) ? "NA" : Convert.ToString(sqlReader["first_Query"]);
                        objChildService.SecondResponse = string.IsNullOrEmpty(Convert.ToString(sqlReader["second_response"])) ? "NA" : Convert.ToString(sqlReader["second_response"]);
                        objChildService.SecondQuery = string.IsNullOrEmpty(Convert.ToString(sqlReader["second_Query"])) ? "NA" : Convert.ToString(sqlReader["second_Query"]);
                    }
                    objChildService.intRowCount = Convert.ToInt32(sqlReader["intRowCount"]);
                    objChildService.strRemarks = Convert.ToString(sqlReader["vchRemarks"]);
                    list.Add(objChildService);
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

    public override Dictionary<int, string> ViewDepartmentListByUser(RptSearch objSearch)
    {
        Dictionary<int, string> dcDept = new Dictionary<int, string>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_ChildServices_MISReport";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@intDepartmentId", objSearch.intDepartmentId);
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    dcDept.Add(Convert.ToInt32(sqlReader["intleveldetailid"]), Convert.ToString(sqlReader["nvchlevelname"]));
                }

            }
            sqlReader.Close();
            return dcDept;
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
    }

    #region Peal MIS Report
    /// <summary>
    /// Function to get the main MIS report for the peal primary link
    /// </summary>
    /// <param name="objSearch">PealSearch object</param>
    /// <returns>List of type PealMisReport</returns>
    public override List<PealMisReport> PealUserwiseMisRpt(PealSearch objSearch)
    {

        List<PealMisReport> list = new List<PealMisReport>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_PEAL_MIS_RPT_DET";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@P_PROJECT_TYPE", objSearch.intProjectType);
            cmd.Parameters.AddWithValue("@P_INT_SECID", objSearch.intSectorId);
            cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@P_INT_INVEST_AMT", objSearch.intInvestmentAmt);
            cmd.Parameters.AddWithValue("@P_INT_APP_YR", objSearch.intYearOfApplication);
            cmd.Parameters.AddWithValue("@PDtmToDate", objSearch.strToDate);
            cmd.Parameters.AddWithValue("@PDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pIntUserId", objSearch.intUserId);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {

                    PealMisReport objProp = new PealMisReport();
                    objProp.intDistrictId = Convert.ToInt32(sqlReader["intDistrictId"]);
                    objProp.strDistrictName = Convert.ToString(sqlReader["VCHDISTRICTNAME"]);
                    objProp.cnt_Total = Convert.ToInt32(sqlReader["cnt_Total"]);
                    objProp.cnt_Pending = Convert.ToInt32(sqlReader["cnt_Pending"]);
                    objProp.cnt_Approved = Convert.ToInt32(sqlReader["cnt_Approved"]);
                    objProp.cnt_rejected = Convert.ToInt32(sqlReader["cnt_rejected"]);
                    objProp.cnt_Query = Convert.ToInt32(sqlReader["cnt_Query"]);
                    objProp.cnt_Proposed_Emp = Convert.ToInt32(sqlReader["cnt_Proposed_Emp"]);
                    objProp.total_Capital_Investment = Convert.ToDecimal(sqlReader["total_Capital_Investment"]);
                    objProp.cnt_landAssessment = Convert.ToInt32(sqlReader["cnt_landAssessment"]);
                    objProp.cnt_landAllotment = Convert.ToInt32(sqlReader["cnt_landAllotment"]);
                    objProp.cnt_AvgDaysApproval = Convert.ToInt32(sqlReader["cnt_AvgDaysApproval"]);
                    objProp.cnt_AvgDaysAllotment = Convert.ToInt32(sqlReader["cnt_AvgDaysAllotment"]);
                    objProp.cnt_Total_AvgDaysAllotment = Convert.ToInt32(sqlReader["cnt_Total_AvgDaysAllotment"]);
                    objProp.cnt_Total_AvgDaysApproval = Convert.ToInt32(sqlReader["cnt_Total_AvgDaysApproval"]);
                    objProp.cnt_Total_ORTPSAtimeline = Convert.ToInt32(sqlReader["TotalORPSAtimeline"]);
                    objProp.cnt_deferred = Convert.ToInt32(sqlReader["cnt_deferred"]);
                    objProp.cnt_Land_Allotment_ORTPSA = Convert.ToInt32(sqlReader["ORTPSA_allotment_Timeline"]);
                    objProp.cnt_CarryFwd_pending = Convert.ToInt32(sqlReader["cnt_CarryForwardPending"]);
                    objProp.int_Total_Pending = Convert.ToInt32(sqlReader["cnt_TotalPending"]);
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

    /// <summary>
    /// Function to get the details of application for the PEAL MIS Main report
    /// </summary>
    /// <param name="objSearch">PealSearch object</param>
    /// <returns>list of type Mis_ChildServiceDtls</returns>
    public override List<Mis_ChildServiceDtls> PealMisRpt_Details(PealSearch objSearch)
    {

        List<Mis_ChildServiceDtls> list = new List<Mis_ChildServiceDtls>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_PEAL_MIS_RPT_DET";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@P_PROJECT_TYPE", objSearch.intProjectType);
            cmd.Parameters.AddWithValue("@P_INT_SECID", objSearch.intSectorId);
            cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@P_INT_INVEST_AMT", objSearch.intInvestmentAmt);
            cmd.Parameters.AddWithValue("@P_INT_APP_YR", objSearch.intYearOfApplication);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntUserId", objSearch.intUserId);
            cmd.Parameters.AddWithValue("@pIntStatus", objSearch.intStatus);
            cmd.Parameters.AddWithValue("@PDtmToDate", objSearch.strToDate);
            cmd.Parameters.AddWithValue("@PDtmFromDate", objSearch.strFromDate);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Mis_ChildServiceDtls objChildService = new Mis_ChildServiceDtls();
                    objChildService.ProposalNo = Convert.ToString(sqlReader["vchProposalNo"]);
                    objChildService.strApplicationDate = Convert.ToString(sqlReader["dtm_Applied_date"]);
                    objChildService.strApprovalDate = Convert.ToString(sqlReader["dtmApprovalDate"]);
                    objChildService.strORTPSATimelineDate = Convert.ToString(sqlReader["dtm_EndOfORTPS_Timeline"]);
                    objChildService.strCompany = Convert.ToString(sqlReader["vchCompName"]);
                    objChildService.ServiceName = Convert.ToString(sqlReader["vchDistrictName"]);
                    objChildService.strBlock = Convert.ToString(sqlReader["vchBlockName"]);
                    objChildService.strSector = Convert.ToString(sqlReader["VCH_SECTOR"]);
                    objChildService.strSubSector = Convert.ToString(sqlReader["vchSubSectorName"]);
                    objChildService.intPropEmployment = Convert.ToInt32(sqlReader["INTTOTALPROP"]);
                    objChildService.decInvestment = Convert.ToDecimal(sqlReader["DECTOTCAPITALINVESTMENT"]);
                    objChildService.intRowCount = Convert.ToInt32(sqlReader["row_cnt"]);
                    list.Add(objChildService);
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

    /// <summary>
    /// Function to get the details of query raised for the PEAL MIS Main report
    /// </summary>
    /// <param name="objSearch">PealSearch object</param>
    /// <returns>list of type Mis_ChildServiceDtls</returns>
    public override List<Mis_ChildServiceDtls> PealMisQueryRpt_Details(PealSearch objSearch)
    {
        List<Mis_ChildServiceDtls> list = new List<Mis_ChildServiceDtls>();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_PEAL_MIS_RPT_DET";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@P_PROJECT_TYPE", objSearch.intProjectType);
            cmd.Parameters.AddWithValue("@P_INT_SECID", objSearch.intSectorId);
            cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@P_INT_INVEST_AMT", objSearch.intInvestmentAmt);
            cmd.Parameters.AddWithValue("@P_INT_APP_YR", objSearch.intYearOfApplication);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntUserId", objSearch.intUserId);
            cmd.Parameters.AddWithValue("@PDtmToDate", objSearch.strToDate);
            cmd.Parameters.AddWithValue("@PDtmFromDate", objSearch.strFromDate);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            da.SelectCommand = cmd;
            da.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int cnt = 0; cnt < dt.Rows.Count; cnt++)
                {
                    DataRow dRow = dt.Rows[cnt];
                    Mis_ChildServiceDtls objChildService = new Mis_ChildServiceDtls();
                    objChildService.ProposalNo = Convert.ToString(dRow["vchProposalNo"]);
                    objChildService.ServiceName = Convert.ToString(dRow["vchDistrictName"]);
                    objChildService.strSector = Convert.ToString(dRow["VCH_SECTOR"]);
                    objChildService.strCompany = Convert.ToString(dRow["vchCompName"]);
                    objChildService.strBlock = Convert.ToString(dRow["vchBlockName"]);
                    objChildService.FirstResponse = string.IsNullOrEmpty(Convert.ToString(dRow["first_response"])) ? "NA" : Convert.ToString(dRow["first_response"]);
                    objChildService.FirstTimeQuery = string.IsNullOrEmpty(Convert.ToString(dRow["first_Query"])) ? "NA" : Convert.ToString(dRow["first_Query"]);
                    objChildService.SecondResponse = string.IsNullOrEmpty(Convert.ToString(dRow["second_response"])) ? "NA" : Convert.ToString(dRow["second_response"]);
                    objChildService.SecondQuery = string.IsNullOrEmpty(Convert.ToString(dRow["second_Query"])) ? "NA" : Convert.ToString(dRow["second_Query"]);
                    objChildService.intRowCount = Convert.ToInt32(dRow["intRowCount"]);
                    list.Add(objChildService);
                }

            }
            return list;
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
    }

    /// <summary>
    /// Function to get the details of application for the PEAL MIS Main report
    /// </summary>
    /// <param name="objSearch">PealSearch object</param>
    /// <returns>list of type Mis_ChildServiceDtls</returns>
    public override List<Mis_ChildServiceDtls> PEAL_MisReportLogic2_Details(PealSearch objSearch)
    {

        List<Mis_ChildServiceDtls> list = new List<Mis_ChildServiceDtls>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_PEAL_MisReport_Logic2";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@P_PROJECT_TYPE", objSearch.intProjectType);
            cmd.Parameters.AddWithValue("@P_INT_SECID", objSearch.intSectorId);
            cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@P_INT_INVEST_AMT", objSearch.intInvestmentAmt);
            cmd.Parameters.AddWithValue("@P_INT_APP_YR", objSearch.intYearOfApplication);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntUserId", objSearch.intUserId);
            cmd.Parameters.AddWithValue("@pIntStatus", objSearch.intStatus);
            cmd.Parameters.AddWithValue("@PDtmToDate", objSearch.strToDate);
            cmd.Parameters.AddWithValue("@PDtmFromDate", objSearch.strFromDate);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Mis_ChildServiceDtls objChildService = new Mis_ChildServiceDtls();
                    objChildService.ProposalNo = Convert.ToString(sqlReader["vchProposalNo"]);
                    objChildService.strApplicationDate = Convert.ToString(sqlReader["dtm_Applied_date"]);
                    objChildService.strApprovalDate = Convert.ToString(sqlReader["dtmApprovalDate"]);
                    objChildService.strORTPSATimelineDate = Convert.ToString(sqlReader["dtm_EndOfORTPS_Timeline"]);
                    objChildService.strCompany = Convert.ToString(sqlReader["vchCompName"]);
                    objChildService.ServiceName = Convert.ToString(sqlReader["vchDistrictName"]);
                    objChildService.strBlock = Convert.ToString(sqlReader["vchBlockName"]);
                    objChildService.strSector = Convert.ToString(sqlReader["VCH_SECTOR"]);
                    objChildService.strSubSector = Convert.ToString(sqlReader["vchSubSectorName"]);
                    objChildService.intPropEmployment = Convert.ToInt32(sqlReader["INTTOTALPROP"]);
                    objChildService.decInvestment = Convert.ToDecimal(sqlReader["DECTOTCAPITALINVESTMENT"]);
                    objChildService.intRowCount = Convert.ToInt32(sqlReader["row_cnt"]);
                    objChildService.decTotalLandRequired = Convert.ToDecimal(sqlReader["decExtendLand"]);
                    objChildService.decLandRecommendedToIdco = Convert.ToDecimal(sqlReader["decRecomendLand"]);
                    objChildService.strRemarks = Convert.ToString(sqlReader["vchRemarks"]);// add by anil
                    list.Add(objChildService);
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


    /// <summary>
    /// Function to get the main MIS report for the peal primary link
    /// </summary>
    /// <param name="objSearch">PealSearch object</param>
    /// <returns>List of type PealMisReport</returns>
    public override List<PealMisReport> PEAL_MisReportLogic2(PealSearch objSearch)
    {

        List<PealMisReport> list = new List<PealMisReport>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_PEAL_MisReport_Logic2";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@P_PROJECT_TYPE", objSearch.intProjectType);
            cmd.Parameters.AddWithValue("@P_INT_SECID", objSearch.intSectorId);
            cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@P_INT_INVEST_AMT", objSearch.intInvestmentAmt);
            cmd.Parameters.AddWithValue("@P_INT_APP_YR", objSearch.intYearOfApplication);
            cmd.Parameters.AddWithValue("@PDtmToDate", objSearch.strToDate);
            cmd.Parameters.AddWithValue("@PDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pIntUserId", objSearch.intUserId);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {

                    PealMisReport objProp = new PealMisReport();
                    objProp.intDistrictId = Convert.ToInt32(sqlReader["intDistrictId"]);
                    objProp.strDistrictName = Convert.ToString(sqlReader["VCHDISTRICTNAME"]);
                    objProp.cnt_Total = Convert.ToInt32(sqlReader["cnt_Total"]);
                    objProp.cnt_Pending = Convert.ToInt32(sqlReader["cnt_Pending"]);
                    objProp.cnt_Approved = Convert.ToInt32(sqlReader["cnt_Approved"]);
                    objProp.cnt_rejected = Convert.ToInt32(sqlReader["cnt_rejected"]);
                    objProp.cnt_Query = Convert.ToInt32(sqlReader["cnt_Query"]);
                    objProp.cnt_Proposed_Emp = Convert.ToInt32(sqlReader["cnt_Proposed_Emp"]);
                    objProp.total_Capital_Investment = Convert.ToDecimal(sqlReader["total_Capital_Investment"]);
                    objProp.cnt_landAssessment = Convert.ToInt32(sqlReader["cnt_landAssessment"]);
                    objProp.cnt_landAllotment = Convert.ToInt32(sqlReader["cnt_landAllotment"]);
                    objProp.cnt_AvgDaysApproval = Convert.ToInt32(sqlReader["cnt_AvgDaysApproval"]);
                    objProp.cnt_AvgDaysAllotment = Convert.ToInt32(sqlReader["cnt_AvgDaysAllotment"]);
                    objProp.cnt_Total_AvgDaysAllotment = Convert.ToInt32(sqlReader["cnt_Total_AvgDaysAllotment"]);
                    objProp.cnt_Total_AvgDaysApproval = Convert.ToInt32(sqlReader["cnt_Total_AvgDaysApproval"]);
                    objProp.cnt_Total_ORTPSAtimeline = Convert.ToInt32(sqlReader["TotalORPSAtimeline"]);
                    objProp.cnt_deferred = Convert.ToInt32(sqlReader["cnt_deferred"]);
                    objProp.cnt_Land_Allotment_ORTPSA = Convert.ToInt32(sqlReader["ORTPSA_allotment_Timeline"]);
                    objProp.cnt_CarryFwd_pending = Convert.ToInt32(sqlReader["cnt_CarryForwardPending"]);
                    objProp.int_Total_Pending = Convert.ToInt32(sqlReader["cnt_TotalPending"]);
                    objProp.intORTPSAtimeline = 30;//Added by Bhagyashree Das on Dated: 23-Dec-2020
                    objProp.intMinApprovalDays = Convert.ToInt32(sqlReader["minApprovalDays"]);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                    objProp.intMaxApprovalDays = Convert.ToInt32(sqlReader["maxApprovalDays"]);//Added by Bhagyashree Das on Dated: 23-Dec-2020
                    objProp.cnt_median = Convert.ToInt32(sqlReader["cnt_median"]);//Added by Manoj Kumar Behera on Dated:06-Jan-2021
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

    #region Userwise report
    /// <summary>
    /// Function to get the details user wise for a particular department 
    /// </summary>
    /// <param name="objSearch">RptSearch object</param>
    /// <returns>List of type MIS_ChildServiceRpt</returns>
    public override List<MIS_ChildServiceRpt> View_ChildServices_UserWise_MISReport(RptSearch objSearch)
    {
        List<MIS_ChildServiceRpt> list = new List<MIS_ChildServiceRpt>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_UserWiseChildServiceRpt";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@pIntServiceId", objSearch.intServiceId);
            cmd.Parameters.AddWithValue("@pIntDepartmentId", objSearch.intDepartmentId);
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntYear", objSearch.intYear);
            cmd.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    MIS_ChildServiceRpt objChildService = new MIS_ChildServiceRpt();
                    objChildService.strParentName = Convert.ToString(sqlReader["nvchlevelname"]);
                    objChildService.strDeptName = Convert.ToString(sqlReader["vchusername"]);
                    objChildService.intTotalApplication = Convert.ToInt32(sqlReader["cnt_Total"]);
                    objChildService.intAvgDaysApproval = Convert.ToInt32(sqlReader["cntAverage"]);
                    objChildService.intTotalApproved = Convert.ToInt32(sqlReader["cnt_approved"]);
                    objChildService.intTotalPending = Convert.ToInt32(sqlReader["cnt_pending"]);
                    objChildService.intTotalRejected = Convert.ToInt32(sqlReader["cnt_rejected"]);
                    objChildService.intTotalQueryRaised = Convert.ToInt32(sqlReader["cnt_QueryRaised"]);
                    objChildService.intKey = Convert.ToInt32(sqlReader["intuserid"]);
                    objChildService.intRowCount = Convert.ToInt32(sqlReader["intRowCount"]);
                    objChildService.intAvgDaysApprovalTotal = Convert.ToInt32(sqlReader["CNTAverage_total"]);
                    list.Add(objChildService);
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

    /// <summary>
    /// Function to get the details for the application for the CHild Services report
    /// The same function is used to get the query raised report for the Child services main report
    /// </summary>
    /// <param name="objSearch"></param>
    /// <returns></returns>
    public override List<Mis_ChildServiceDtls> View_ChildServices_UserWiseDetails_MISReport(RptSearch objSearch)
    {
        List<Mis_ChildServiceDtls> list = new List<Mis_ChildServiceDtls>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_UserWiseChildServiceRpt";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@pIntServiceId", objSearch.intServiceId);
            cmd.Parameters.AddWithValue("@pIntDepartmentId", objSearch.intDepartmentId);
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntYear", objSearch.intYear);
            cmd.Parameters.AddWithValue("@pIntUserId", objSearch.intUserId);
            cmd.Parameters.AddWithValue("@pIntStatus", objSearch.intStatus);
            cmd.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Mis_ChildServiceDtls objChildService = new Mis_ChildServiceDtls();
                    objChildService.ProposalNo = Convert.ToString(sqlReader["VCH_PROPOSALID"]);
                    objChildService.ServiceName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                    objChildService.strDepartment = Convert.ToString(sqlReader["nvchlevelname"]);
                    objChildService.strCompany = Convert.ToString(sqlReader["VCH_INVESTOR_NAME"]);
                    objChildService.strBlock = Convert.ToString(sqlReader["vchBlockName"]);
                    objChildService.strUsername = Convert.ToString(sqlReader["username"]);
                    objChildService.strApplicationNo = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                    objChildService.strApplicationDate = Convert.ToString(sqlReader["dtm_Payment_date"]);
                    objChildService.decPaymentAmt = Convert.ToDecimal(sqlReader["NUM_PAYMENT_AMOUNT"]);

                    //if query raised details are being fetched
                    if (string.Equals(objSearch.strActionCode, "sq", StringComparison.OrdinalIgnoreCase))
                    {
                        objChildService.FirstResponse = string.IsNullOrEmpty(Convert.ToString(sqlReader["first_response"])) ? "NA" : Convert.ToString(sqlReader["first_response"]);
                        objChildService.FirstTimeQuery = string.IsNullOrEmpty(Convert.ToString(sqlReader["first_Query"])) ? "NA" : Convert.ToString(sqlReader["first_Query"]);
                        objChildService.SecondResponse = string.IsNullOrEmpty(Convert.ToString(sqlReader["second_response"])) ? "NA" : Convert.ToString(sqlReader["second_response"]);
                        objChildService.SecondQuery = string.IsNullOrEmpty(Convert.ToString(sqlReader["second_Query"])) ? "NA" : Convert.ToString(sqlReader["second_Query"]);
                    }
                    else
                    {
                        objChildService.strApprovalDate = Convert.ToString(sqlReader["dtmApprovalDate"]);
                    }
                    objChildService.intRowCount = Convert.ToInt32(sqlReader["intRowCount"]);
                    list.Add(objChildService);
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

    #region Child Services Districtwise report
    public override List<MIS_ChildServiceRpt> View_ChildServices_District_Rpt(RptSearch objSearch)
    {
        List<MIS_ChildServiceRpt> list = new List<MIS_ChildServiceRpt>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_ChildServices_Districtwise_Rpt";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@intServiceId", objSearch.intServiceId);
            cmd.Parameters.AddWithValue("@intDepartmentId", objSearch.intDepartmentId);
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntDistrictId", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    MIS_ChildServiceRpt objChildService = new MIS_ChildServiceRpt();
                    objChildService.strDeptName = Convert.ToString(sqlReader["vchDistrictName"]);
                    objChildService.intTotalApplication = Convert.ToInt32(sqlReader["cnt_Total"]);
                    objChildService.intAvgDaysApproval = Convert.ToInt32(sqlReader["cntAverage"]);
                    objChildService.intAvgDaysApprovalTotal = Convert.ToInt32(sqlReader["cntAverageTotal"]);
                    objChildService.intTotalApproved = Convert.ToInt32(sqlReader["cnt_approved"]);
                    objChildService.intTotalPending = Convert.ToInt32(sqlReader["cnt_pending"]);
                    objChildService.intTotalRejected = Convert.ToInt32(sqlReader["cnt_rejected"]);
                    objChildService.intTotalQueryRaised = Convert.ToInt32(sqlReader["cnt_QueryRaised"]);
                    objChildService.intTotalORTPSAtimelinePassed = Convert.ToInt32(sqlReader["cnt_ortps_Timeline"]);
                    objChildService.intKey = Convert.ToInt32(sqlReader["intDistrictId"]);
                    objChildService.intCarryFwdPending = Convert.ToInt32(sqlReader["cnt_CarryFwd_Pending"]);
                    list.Add(objChildService);
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

    public override List<Mis_ChildServiceDtls> View_ChildServices_District_Details_Rpt(RptSearch objSearch)
    {
        List<Mis_ChildServiceDtls> list = new List<Mis_ChildServiceDtls>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_ChildServices_Districtwise_Rpt";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@intServiceId", objSearch.intServiceId);
            cmd.Parameters.AddWithValue("@intDepartmentId", objSearch.intDepartmentId);
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pIntDistrictId", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@pIntStatus", objSearch.intStatus);
            cmd.Parameters.AddWithValue("@pDtmFromDate", objSearch.strFromDate);
            cmd.Parameters.AddWithValue("@pDtmToDate", objSearch.strToDate);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Mis_ChildServiceDtls objChildService = new Mis_ChildServiceDtls();
                    objChildService.ProposalNo = Convert.ToString(sqlReader["VCH_PROPOSALID"]);
                    objChildService.ServiceName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                    objChildService.strDepartment = Convert.ToString(sqlReader["nvchlevelname"]);
                    objChildService.strBlock = Convert.ToString(sqlReader["vchBlockName"]);

                    objChildService.strDistName = Convert.ToString(sqlReader["DISTNAME"]);

                    //if query raised details are being fetched
                    if (string.Equals(objSearch.strActionCode, "dq", StringComparison.OrdinalIgnoreCase))
                    {
                        objChildService.FirstResponse = string.IsNullOrEmpty(Convert.ToString(sqlReader["first_response"])) ? "NA" : Convert.ToString(sqlReader["first_response"]);
                        objChildService.FirstTimeQuery = string.IsNullOrEmpty(Convert.ToString(sqlReader["first_Query"])) ? "NA" : Convert.ToString(sqlReader["first_Query"]);
                        objChildService.SecondResponse = string.IsNullOrEmpty(Convert.ToString(sqlReader["second_response"])) ? "NA" : Convert.ToString(sqlReader["second_response"]);
                        objChildService.SecondQuery = string.IsNullOrEmpty(Convert.ToString(sqlReader["second_Query"])) ? "NA" : Convert.ToString(sqlReader["second_Query"]);
                        objChildService.strCompany = string.IsNullOrEmpty(Convert.ToString(sqlReader["VCH_INV_NAME"])) ? "NA" : Convert.ToString(sqlReader["VCH_INV_NAME"]);
                    }
                    else if (string.Equals(objSearch.strActionCode, "dd", StringComparison.OrdinalIgnoreCase))
                    {
                        objChildService.strCompany = Convert.ToString(sqlReader["VCH_INVESTOR_NAME"]);
                        objChildService.strApplicationNo = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                        objChildService.strApplicationDate = Convert.ToString(sqlReader["dtm_Payment_date"]);
                        objChildService.decPaymentAmt = Convert.ToDecimal(sqlReader["NUM_PAYMENT_AMOUNT"]);
                    }
                    objChildService.intRowCount = Convert.ToInt32(sqlReader["intRowCount"]);
                    list.Add(objChildService);
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

    //Added by Bhagyashree Das on 24-12-2020
    #region Incentive MIS Report

    public override List<IncentiveMisReport> GetIncentiveMISReportDtls(RptSearch objDashboardInfo)
    {
        List<IncentiveMisReport> list = new List<IncentiveMisReport>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_INCT_INCENTIVE_MISREPORT";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_VCH_ACTION", "I");
            cmd.Parameters.AddWithValue("@P_INT_DISTRICT_CODE", objDashboardInfo.intDistrictId);
            cmd.Parameters.AddWithValue("@P_DTM_FROM_DATE", objDashboardInfo.strFromDate);
            cmd.Parameters.AddWithValue("@P_DTM_TO_DATE", objDashboardInfo.strToDate);

            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    if (objDashboardInfo.strActionCode == "I")
                    {
                        IncentiveMisReport objDashboardInfo1 = new IncentiveMisReport();
                        objDashboardInfo1.INCAPLLIED = Convert.ToString(sqlReader["int_Disbursed"]);
                        objDashboardInfo1.INCSANCTIONED = Convert.ToString(sqlReader["int_Sanctioned"]);
                        objDashboardInfo1.INCPENDING = Convert.ToString(sqlReader["int_Pending"]);
                        objDashboardInfo1.INCREJECTED = Convert.ToString(sqlReader["int_Rejected"]);
                        objDashboardInfo1.strIncMean = Convert.ToString(sqlReader["dec_Mean"]);
                        objDashboardInfo1.strIncMedian = Convert.ToString(sqlReader["dec_Median"]);
                        objDashboardInfo1.Department = Convert.ToString(sqlReader["vch_DeptName"]); ;
                        objDashboardInfo1.Incentive = Convert.ToString(sqlReader["vch_InctName"]);
                        objDashboardInfo1.Timeline = Convert.ToInt32(sqlReader["int_Timeline"]);
                        objDashboardInfo1.minSactionDays = Convert.ToInt32(sqlReader["int_MinSanctionDys"]);
                        objDashboardInfo1.maxSactionDays = Convert.ToInt32(sqlReader["int_MaxSanctionDays"]);

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

    public override List<MIS_InvestorRpt> View_ChildServices_Investor_MISReport(InvestorRptSearch objSearch)
    {
        List<MIS_InvestorRpt> list = new List<MIS_InvestorRpt>();
        SqlDataReader sqlReader1 = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_InvestorDetails_Report";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchActionCode", objSearch.strActionCode);
            //cmd.Parameters.AddWithValue("@IntPageIndex", objSearch.intIntPageIndex);
            //cmd.Parameters.AddWithValue("@intPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@intDistrictId", objSearch.intDistrictId);
            cmd.Parameters.AddWithValue("@intBlockId", objSearch.intBlockId);
            cmd.Parameters.AddWithValue("@intSectorId", objSearch.intSectorId);
            cmd.Parameters.AddWithValue("@intSubsectorId", objSearch.intSubsectorId);
            cmd.Parameters.AddWithValue("@intCompanyId", objSearch.intCompanyId);
            cmd.Parameters.AddWithValue("@vchPanNo", objSearch.strPanNo);
            cmd.Parameters.AddWithValue("@intIndustryCategory", objSearch.IntIndustyType); // Add anil sahoo
            cmd.Parameters.AddWithValue("@vchRegdSource", objSearch.StrRegdSource);   // Add anil sahoo
            cmd.Parameters.AddWithValue("@vchFROM_DATE", objSearch.StrFromDate); // Add anil sahoo
            cmd.Parameters.AddWithValue("@vchTO_DATE", objSearch.StrToDate);  // Add anil sahoo
            cmd.Parameters.AddWithValue("@vchLICENCE_NO_TYPE", objSearch.StrLicenceNoType); //Add Dharmasis sahoo
            cmd.Parameters.AddWithValue("@intCATEGORY", objSearch.IntCategory);             //Add Dharmasis sahoo
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader1 = cmd.ExecuteReader();
            if (sqlReader1.HasRows)
            {
                while (sqlReader1.Read())
                {
                    MIS_InvestorRpt objChildService = new MIS_InvestorRpt();
                    objChildService.strInvestorName = Convert.ToString(sqlReader1["VCH_INV_NAME"]);
                    objChildService.strUserId = Convert.ToString(sqlReader1["VCH_INV_USERID"]);
                    objChildService.strEmailId = Convert.ToString(sqlReader1["VCH_EMAIL"]);
                    objChildService.strContactPersn = Convert.ToString(sqlReader1["vch_contact_firstname"]);
                    objChildService.strAddress = Convert.ToString(sqlReader1["VCH_ADDRESS"]);
                    objChildService.strMobile = Convert.ToString(sqlReader1["VCH_OFF_MOBILE"]);
                    objChildService.VCH_EIN_IEM = Convert.ToString(sqlReader1["VCH_EIN_IEM"]);
                    objChildService.DTM_CREATED_ON = Convert.ToString(sqlReader1["DTM_CREATED_ON"]);
                    objChildService.vchDistrictName = Convert.ToString(sqlReader1["vchDistrictName"]);
                    objChildService.vchBlockName = Convert.ToString(sqlReader1["vchBlockName"]);
                    objChildService.vchSectorName = Convert.ToString(sqlReader1["vchSectorName"]); //Added  ABT
                    objChildService.vchSubSectorName = Convert.ToString(sqlReader1["vchSubSectorName"]); //Added  ABT
                    objChildService.StrIndustyType = Convert.ToString(sqlReader1["vchIndustryType"]); // Add anil sahoo
                    objChildService.StrRegdSource = Convert.ToString(sqlReader1["vchRegdSource"]);  // Add anil sahoo
                    objChildService.IntIndustryType = Convert.ToInt32(sqlReader1["INT_INDUSTRY_TYPE"]);  // Add anil sahoo
                    objChildService.StrLicenceNoType = Convert.ToString(sqlReader1["VCH_LICENCE_NO_TYPE"]);  // Add anil sahoo
                    objChildService.strInvLevel = Convert.ToString(sqlReader1["Vch_InvLevel"]);               // Add Dharmasis sahoo strInvLevel
                    list.Add(objChildService);
                }

            }
            sqlReader1.Close();
            return list;
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
    }

    public override List<rtnIndustry> View_Industry(IndustrySearch objSearch)
    {
        List<rtnIndustry> list = new List<rtnIndustry>();
        SqlDataReader sqlReader1 = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_InvestorDetails_Report";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchActionCode", objSearch.strActionCode);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader1 = cmd.ExecuteReader();
            if (sqlReader1.HasRows)
            {
                while (sqlReader1.Read())
                {
                    rtnIndustry objrtnIndustry = new rtnIndustry();
                    objrtnIndustry.INT_INVESTOR_ID = Convert.ToInt32(sqlReader1["INT_INVESTOR_ID"]);
                    objrtnIndustry.VCH_INV_NAME = Convert.ToString(sqlReader1["VCH_INV_NAME"]);
                    list.Add(objrtnIndustry);
                }

            }
            sqlReader1.Close();
            return list;
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
    }
    //Created by Manoj Kumar Behera 
    public override DataTable FillDeptUserName(IndustrySearch objSearch)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Event_Log_Exception_Reports";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_VCH_ACTION", objSearch.strActionCode);
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
            gSqlConn.Close();
        }
        return dt;
    }
    public override DataTable FillUserDetails(IndustrySearch objSearch)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Event_Log_Exception_Reports";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_VCH_ACTION", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@P_INT_USER_ID", objSearch.INT_USER_ID);
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
            gSqlConn.Close();
        }
        return dt;
    }

    public override DataTable GetGrievanceMISReportDtls(GrievanceMisSearch objSearch)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }

        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_MISREPORT";

            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSearch.StrActionCode);
            cmd.Parameters.AddWithValue("@P_INT_GRIV_TYPE_ID", objSearch.IntGrivTypeId);
            cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objSearch.IntDistrictId);
            cmd.Parameters.AddWithValue("@P_DTM_FROM_DATE", objSearch.StrFromDate);
            cmd.Parameters.AddWithValue("@P_DTM_TO_DATE", objSearch.StrToDate);

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
            gSqlConn.Close();
        }
        return dt;
    }
    public override DataTable GetStatusWiseGrievanceMISReportDtls(GrievanceMisSearch objSearch)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_MISREPORT";

            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSearch.StrActionCode);
            cmd.Parameters.AddWithValue("@P_INT_GRIV_TYPE_ID", objSearch.IntGrivTypeId);
            cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objSearch.IntDistrictId);
            cmd.Parameters.AddWithValue("@P_DTM_FROM_DATE", objSearch.StrFromDate);
            cmd.Parameters.AddWithValue("@P_DTM_TO_DATE", objSearch.StrToDate);
            cmd.Parameters.AddWithValue("@P_INT_STATUS", objSearch.IntStatus);
            cmd.Parameters.AddWithValue("@P_INT_PAGE_INDEX", objSearch.IntIntPageIndex);
            cmd.Parameters.AddWithValue("@P_INT_PAGE_SIZE", objSearch.IntPageSize);

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
            gSqlConn.Close();
        }
        return dt;
    }

    //Created by Debiprasanna 05-09-22
    // Function to get the details of District Wise Investor report
    public override List<MIS_InvestorRpt> View_DistWise_Investor_Report(InvestorRptSearch objSearch)
    {
        List<MIS_InvestorRpt> list = new List<MIS_InvestorRpt>();
        SqlDataReader sqlReader1 = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_InvestorDetails_Report";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@intDistrictId", objSearch.intDistrictId);

            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader1 = cmd.ExecuteReader();
            if (sqlReader1.HasRows)
            {
                while (sqlReader1.Read())
                {
                    MIS_InvestorRpt objChildService = new MIS_InvestorRpt();
                    objChildService.strInvestorName = Convert.ToString(sqlReader1["VCH_INV_NAME"]);
                    objChildService.strUserId = Convert.ToString(sqlReader1["VCH_INV_USERID"]);
                    objChildService.strEmailId = Convert.ToString(sqlReader1["VCH_EMAIL"]);
                    objChildService.strContactPersn = Convert.ToString(sqlReader1["VCH_CONTACT_FIRSTNAME"]);
                    objChildService.strAddress = Convert.ToString(sqlReader1["VCH_ADDRESS"]);
                    objChildService.strMobile = Convert.ToString(sqlReader1["VCH_OFF_MOBILE"]);
                    objChildService.VCH_EIN_IEM = Convert.ToString(sqlReader1["VCH_EIN_IEM"]);
                    objChildService.DTM_CREATED_ON = Convert.ToString(sqlReader1["DTM_CREATED_ON"]);
                    objChildService.vchDistrictName = Convert.ToString(sqlReader1["vchDistrictName"]);
                    objChildService.vchBlockName = Convert.ToString(sqlReader1["vchBlockName"]);
                    objChildService.vchSectorName = Convert.ToString(sqlReader1["VCH_SECTOR"]);
                    objChildService.vchSubSectorName = Convert.ToString(sqlReader1["vchSubSectorName"]);
                    objChildService.StrIndustyType = Convert.ToString(sqlReader1["vchIndustryType"]);
                    objChildService.StrRegdSource = Convert.ToString(sqlReader1["VCH_REGD_SOURCE"]);
                    objChildService.StrLicenceNoType = Convert.ToString(sqlReader1["VCH_LICENCE_NO_TYPE"]);
                    objChildService.strInvLevel = Convert.ToString(sqlReader1["vch_Inv_Level"]);

                    list.Add(objChildService);
                }

            }
            sqlReader1.Close();
            return list;
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
    }
    //Created by Debiprasanna 12-09-22
    // Function to get the details of Otp pending status report
    public override List<MIS_InvestorRpt> View_Pending_Otp_Report(InvestorRptSearch objSearch)
    {
        List<MIS_InvestorRpt> list = new List<MIS_InvestorRpt>();
        SqlDataReader sqlReader1 = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_InvestorDetails_Report";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@vchFROM_DATE", objSearch.StrFromDate);
            cmd.Parameters.AddWithValue("@vchTO_DATE", objSearch.StrToDate);


            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader1 = cmd.ExecuteReader();
            if (sqlReader1.HasRows)
            {
                while (sqlReader1.Read())
                {
                    MIS_InvestorRpt objChildService = new MIS_InvestorRpt();
                    objChildService.strInvestorName = Convert.ToString(sqlReader1["VCH_INV_NAME"]);
                    objChildService.strUserId = Convert.ToString(sqlReader1["VCH_INV_USERID"]);
                    objChildService.strEmailId = Convert.ToString(sqlReader1["VCH_EMAIL"]);
                    objChildService.strContactPersn = Convert.ToString(sqlReader1["VCH_CONTACT_FIRSTNAME"]);
                    objChildService.strAddress = Convert.ToString(sqlReader1["VCH_ADDRESS"]);
                    objChildService.strMobile = Convert.ToString(sqlReader1["VCH_OFF_MOBILE"]);
                    objChildService.DTM_CREATED_ON = Convert.ToString(sqlReader1["DTM_CREATED_ON"]);
                    objChildService.strPanNo = Convert.ToString(sqlReader1["VCH_PAN"]);
                    objChildService.IntOtpStatus = Convert.ToInt32(sqlReader1["INT_OTP_STATUS"]);

                    list.Add(objChildService);
                }

            }
            sqlReader1.Close();
            return list;
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
    }
    //Created by Debiprasanna 21-09-22
    // Function to get the details of Industry Wise Application Report
    //public override List<IndustrywiseApplicationReport> View_Industrywise_Application_Report(InvestorRptSearch objSearch)
    //{
    //    List<IndustrywiseApplicationReport> list = new List<IndustrywiseApplicationReport>();
    //    SqlDataReader sqlReader1 = null;

    //    SqlCommand cmd = new SqlCommand();
    //    try
    //    {
    //        cmd.Connection = gSqlConn;
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.CommandText = "USP_InvestorDetails_Report";
    //        cmd.Parameters.Clear();
    //        cmd.Parameters.AddWithValue("@vchActionCode", objSearch.strActionCode);
    //        cmd.Parameters.AddWithValue("@P_VCH_INV_NAME", objSearch.strInvestorName);
    //        cmd.Parameters.AddWithValue("@intCATEGORY", objSearch.IntCategory);
    //        cmd.Parameters.AddWithValue("@vchFROM_DATE", objSearch.StrFromDate);
    //        cmd.Parameters.AddWithValue("@vchTO_DATE", objSearch.StrToDate);

    //        if (gSqlConn.State == ConnectionState.Closed)
    //        {
    //            gSqlConn.Open();
    //        }
    //        sqlReader1 = cmd.ExecuteReader();
    //        if (sqlReader1.HasRows)
    //        {
    //            while (sqlReader1.Read())
    //            {
    //                IndustrywiseApplicationReport objChildService = new IndustrywiseApplicationReport();

    //                objChildService.strInvestorName = Convert.ToString(sqlReader1["VCH_INV_NAME"]);
    //                objChildService.Str_USER_ID = Convert.ToString(sqlReader1["VCH_INV_USERID"]);
    //                objChildService.strInvLevel = Convert.ToString(sqlReader1["VCH_INV_LEVEL"]);
    //                objChildService.Int_PROPOSAL_Count = Convert.ToInt32(sqlReader1["INT_PROPOSAL_COUNT"]);
    //                objChildService.Int_PROPOSAL_Count = Convert.ToInt32(sqlReader1["INT_SERVICE_COUNT"]);
    //                objChildService.Int_INCENTIVE_Count = Convert.ToInt32(sqlReader1["INT_INCENTIVE_COUNT"]);
    //                objChildService.Int_GRIEVANCE_Count = Convert.ToInt32(sqlReader1["INT_GRIEVANCE_COUNT"]);
    //                objChildService.Int_LARGE_PC_Count = Convert.ToInt32(sqlReader1["INT_LARGE_PC_COUNT"]);
    //                objChildService.Int_SMALL_PC_Count = Convert.ToInt32(sqlReader1["INT_SMALL_PC_COUNT"]);
    //                objChildService.Int_PPC_Count = Convert.ToInt32(sqlReader1["INT_PPC_COUNT"]);

    //                list.Add(objChildService);
    //            }

    //        }
    //        sqlReader1.Close();
    //        return list;
    //    }
    //    catch (NullReferenceException ex) { throw ex; }
    //    catch (Exception ex)
    //    { throw ex; }
    //    finally { cmd = null; }
    //}



    public override List<Financialreport> View_FinancilaReport(Financialreport objSearch)
    {
        List<Financialreport> list = new List<Financialreport>();
        SqlDataReader sqlReader1 = null;

        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_ChildServices_MISReport_Logic2";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@P_Intstatus", objSearch.intStatus);
            cmd.Parameters.AddWithValue("@P_dec_fee", objSearch.decInvestamount);
            cmd.Parameters.AddWithValue("@P_dec_InvestAmount", objSearch.decFee);
            cmd.Parameters.AddWithValue("@P_VCH_FROM_DATE", objSearch.StrFromDate);
            cmd.Parameters.AddWithValue("@P_VCH_TO_DATE", objSearch.StrToDate);


            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader1 = cmd.ExecuteReader();
            if (sqlReader1.HasRows)
            {
                while (sqlReader1.Read())
                {
                    Financialreport objFinancial = new Financialreport();

                    objFinancial.strProposalnumber = Convert.ToString(sqlReader1["vchProposalNo"]);
                    objFinancial.strInvestment_Amount = Convert.ToString(sqlReader1["Investment_Amount"]);
                    objFinancial.decFee = Convert.ToDecimal(sqlReader1["Fees"]);
                    objFinancial.strStatus = Convert.ToString(sqlReader1["Status"]);
                   

                    list.Add(objFinancial);
                }

            }
            sqlReader1.Close();
            return list;
        }
        
        catch (Exception ex)
        { 
            throw ex;
        }
       
    }

    public override List<SMSCountReport> View_SMSCounrReport(SMSCountReport objSearch)
    {
        List<SMSCountReport> list = new List<SMSCountReport>();
        SqlDataReader sqlReader1 = null;

        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_InvestorDetails_Report";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@vchFROM_DATE", objSearch.StrFromDate);
            cmd.Parameters.AddWithValue("@vchTO_DATE", objSearch.StrToDate);
            cmd.Parameters.AddWithValue("@P_VCH_TYPE", objSearch.StrSMSType);


            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader1 = cmd.ExecuteReader();
            if (sqlReader1.HasRows)
            {
                while (sqlReader1.Read())
                {
                    SMSCountReport objSMScount = new SMSCountReport();

                    objSMScount.intNumberofSMS = Convert.ToInt32(sqlReader1["NumberofSMS"]);
                    objSMScount.StrSMSType = Convert.ToString(sqlReader1["VCH_TYPE"]);
                    
                    list.Add(objSMScount);
                }

            }
            sqlReader1.Close();
            return list;
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }

    public override List<SMSCountReport> View_SMSType(SMSCountReport objSearch)
    {
        List<SMSCountReport> list = new List<SMSCountReport>();
        SqlDataReader sqlReader1 = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_InvestorDetails_Report";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchActionCode", objSearch.strActionCode);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader1 = cmd.ExecuteReader();
            if (sqlReader1.HasRows)
            {
                while (sqlReader1.Read())
                {
                    SMSCountReport objSMScount = new SMSCountReport();
                    objSMScount.intSMSTypeId = Convert.ToInt32(sqlReader1["INT_ID"]);
                    objSMScount.StrSMSType = Convert.ToString(sqlReader1["VCH_TYPE"]);
                    list.Add(objSMScount);
                }

            }
            sqlReader1.Close();
            return list;
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
    }


    //public override List<SMSCountReport> View_ChildSMSCounrReport(SMSCountReport objSearch)
    //{
    //    List<SMSCountReport> list = new List<SMSCountReport>();
    //    SqlDataReader sqlReader1 = null;

    //    SqlCommand cmd = new SqlCommand();
    //    try
    //    {
    //        cmd.Connection = gSqlConn;
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.CommandText = "USP_InvestorDetails_Report";
    //        cmd.Parameters.Clear();
    //        cmd.Parameters.AddWithValue("@vchActionCode", objSearch.strActionCode);
    //        cmd.Parameters.AddWithValue("@P_VCH_TYPE", objSearch.StrSMSType);


    //        if (gSqlConn.State == ConnectionState.Closed)
    //        {
    //            gSqlConn.Open();
    //        }
    //        sqlReader1 = cmd.ExecuteReader();
    //        if (sqlReader1.HasRows)
    //        {
    //            while (sqlReader1.Read())
    //            {
    //                SMSCountReport objSMScount = new SMSCountReport();

    //                objSMScount.strApplicationno = Convert.ToString(sqlReader1["VCH_APPLICATION_NO"]);
    //                objSMScount.strMobileno = Convert.ToString(sqlReader1["VCH_MOBILE_NO"]);
    //                objSMScount.strSMS_Conent = Convert.ToString(sqlReader1["VCH_SMS_CONT1"]);
    //                objSMScount.StrCreatedon = Convert.ToString(sqlReader1["DTM_CREATED_ON"]);



    //                list.Add(objSMScount);
    //            }

    //        }
    //        sqlReader1.Close();
    //        return list;
    //    }

    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}

    public override DataTable View_ChildSMSCounrReport(string SMSType)
    {
        if (gSqlConn.State == ConnectionState.Closed)
           {
               gSqlConn.Open();
             }
            try
            {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_InvestorDetails_Report";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchActionCode", "SCDR");
            cmd.Parameters.AddWithValue("@P_VCH_TYPE", SMSType);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

}
