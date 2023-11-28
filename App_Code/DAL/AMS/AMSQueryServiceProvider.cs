/*
 * Class Name : AMSQueryServiceProvider
 * File Name : AMSQueryServiceProvider.cs
 * Created By : Ritika Lath
 * Created On : 22nd Feb 2018
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EntityLayer.Proposal;

/// <summary>
/// Summary description for AMSQueryServiceProvider
/// </summary>
public class AMSQueryServiceProvider : IAMSQueryServiceProvider
{
    #region "Member Variable"
    string ConnectionString = "AdminAppConnectionProd";
    SqlConnection gSqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    #endregion

    public AMSQueryServiceProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region AMS Query Management System
    public override int AMS_QueryManagement_AED(AMSNodalDetails objNodalDetails)
    {
        int intRetValue = 0;
        SqlCommand objCommand  = new SqlCommand();
        try
        {
            objCommand.Connection = gSqlConn;
            objCommand.CommandText = "USP_AMS_QueryDetails_AED";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@pVchAction", objNodalDetails.strActionCode);
            objCommand.Parameters.AddWithValue("@pVchProposalNo", objNodalDetails.strProposalNo);
            objCommand.Parameters.AddWithValue("@pIntCreatedBy", objNodalDetails.intCreatedBy);
            objCommand.Parameters.AddWithValue("@pVchRemarks", objNodalDetails.strRemarks);
            objCommand.Parameters.AddWithValue("@pVchFileName", objNodalDetails.strFileName);
            objCommand.Parameters.AddWithValue("@pIntQueryConfigId", objNodalDetails.intQueryConfigId);
            objCommand.Parameters.AddWithValue("@pIntUserId", objNodalDetails.intUserId);
            objCommand.Parameters.AddWithValue("@pBitNoQuery", objNodalDetails.BitNoQuery);
            objCommand.Parameters.AddWithValue("@pVchGmRemarks", objNodalDetails.strGmComments);
            SqlParameter objOutParam = new SqlParameter("@pIntOut", SqlDbType.Int);
            objOutParam.Direction = ParameterDirection.Output;
            objCommand.Parameters.Add(objOutParam);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            objCommand.ExecuteNonQuery();
            object obj = objOutParam.Value;
            if (obj != null && obj != DBNull.Value)
            {
                intRetValue = Convert.ToInt32(obj);
            }
        }
        catch
        {

        }
        finally
        {
            gSqlConn.Close();
        }
        return intRetValue;
    }

    public override List<AMSNodalDetails> AMS_QueryManagement_View(AMS_Search objSearch)
    {
        List<AMSNodalDetails> list = new List<AMSNodalDetails>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_AMS_QueryDetails_View";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@pChrActionCode", objSearch.strActionCode);
            cmd.Parameters.AddWithValue("@pIntUserId", objSearch.intUserId);
            cmd.Parameters.AddWithValue("@pIntQueryConfigId", objSearch.intServiceId);
            cmd.Parameters.AddWithValue("@pIntPageIndex", objSearch.intIntPageIndex);
            cmd.Parameters.AddWithValue("@pIntPageSize", objSearch.intPageSize);
            cmd.Parameters.AddWithValue("@pVchProposalNo", objSearch.strProposalNo);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    AMSNodalDetails objNodalDetails = new AMSNodalDetails();
                    if (string.Equals(objSearch.strActionCode, "view", StringComparison.OrdinalIgnoreCase))
                    {
                        objNodalDetails.intQueryConfigId = Convert.ToInt32(sqlReader["INT_QUERY_CONFIG_ID"]);
                        objNodalDetails.isDatePassed = Convert.ToInt32(sqlReader["isDatePassed"]);
                        objNodalDetails.isUpdated = Convert.ToInt32(sqlReader["isUpdated"]);
                        objNodalDetails.strForwardedDate = Convert.ToString(sqlReader["DTM_NODAL_FWDED"]);
                        objNodalDetails.strProposalNo = Convert.ToString(sqlReader["VCH_PROPOSAL_NO"]);
                        objNodalDetails.strReplyDate = Convert.ToString(sqlReader["DTM_NODAL_REPLY_CONFIG"]);
                        objNodalDetails.intRowCount = Convert.ToInt32(sqlReader["row_cnt"]);
                        objNodalDetails.strInvName = Convert.ToString(sqlReader["vchCompName"]);
                        objNodalDetails.strInvAppliedDate = Convert.ToString(sqlReader["dtm_Payment_date"]);
                        objNodalDetails.strQueryTime = Convert.ToString(sqlReader["strQueryTime"]);
                        objNodalDetails.intUserType = Convert.ToInt32(sqlReader["intType"]);
                    }
                    else if (string.Equals(objSearch.strActionCode, "e", StringComparison.OrdinalIgnoreCase))
                    {
                        objNodalDetails.strRemarks = Convert.ToString(sqlReader["VCH_REMARKS"]);
                        objNodalDetails.strFileName = Convert.ToString(sqlReader["VCH_FILE_NAME"]);
                        objNodalDetails.BitNoQuery = Convert.ToBoolean(sqlReader["BIT_NO_QUERY"]);
                    }
                    else if (string.Equals(objSearch.strActionCode, "gm", StringComparison.OrdinalIgnoreCase))
                    {
                        objNodalDetails.strForwardedDate = Convert.ToString(sqlReader["DTM_NODAL_FWDED"]);
                        objNodalDetails.strProposalNo = Convert.ToString(sqlReader["VCH_PROPOSAL_NO"]);
                        objNodalDetails.strReplyDate = Convert.ToString(sqlReader["DTM_INV_FWDED"]);
                        objNodalDetails.intRowCount = Convert.ToInt32(sqlReader["row_cnt"]);
                        objNodalDetails.intInvReplyStatus = Convert.ToInt32(sqlReader["invReplyStatus"]);
                        objNodalDetails.intConfigId = Convert.ToInt32(sqlReader["INT_ID"]);
                        objNodalDetails.strInvName = Convert.ToString(sqlReader["vchCompName"]);
                        objNodalDetails.strInvAppliedDate = Convert.ToString(sqlReader["dtm_Payment_date"]);
                        objNodalDetails.strQueryTime = Convert.ToString(sqlReader["strQueryTime"]);
                    }
                    else if (string.Equals(objSearch.strActionCode, "slfc", StringComparison.OrdinalIgnoreCase) || string.Equals(objSearch.strActionCode, "nod", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.Equals(objSearch.strActionCode, "slfc", StringComparison.OrdinalIgnoreCase))
                        {
                            objNodalDetails.intInvReplyStatus = Convert.ToInt32(sqlReader["invReplyStatus"]);
                            objNodalDetails.strGmComments = Convert.ToString(sqlReader["gm_comments"]);
                            objNodalDetails.strGmFileName = Convert.ToString(sqlReader["gm_file"]);
                            objNodalDetails.strInvName = Convert.ToString(sqlReader["vchCompName"]);

                        }
                        objNodalDetails.strGmComments = Convert.ToString(sqlReader["VCH_GM_NODAL_COMMENTS"]);
                        objNodalDetails.BitNoQuery = Convert.ToBoolean(sqlReader["BIT_NO_QUERY"]);
                        objNodalDetails.strRemarks = Convert.ToString(sqlReader["VCH_REMARKS"]);
                        objNodalDetails.strFileName = Convert.ToString(sqlReader["VCH_FILE_NAME"]);
                        objNodalDetails.strInvReplyDate = Convert.ToString(sqlReader["DTM_INV_RESPONSE"]);
                        objNodalDetails.strInvResponse = Convert.ToString(sqlReader["VCH_INV_REPLY"]);
                        objNodalDetails.strUsername = Convert.ToString(sqlReader["VCHUSERNAME"]);
                        objNodalDetails.intUserId = Convert.ToInt32(sqlReader["INT_USER_ID"]);
                        objNodalDetails.strNoQuery = Convert.ToString(sqlReader["strNoQueries"]);

                    }
                    else if (string.Equals(objSearch.strActionCode, "sd", StringComparison.OrdinalIgnoreCase))
                    {
                        objNodalDetails.strInvReplyDate = Convert.ToString(sqlReader["DTM_INV_RESPONSE"]);
                        objNodalDetails.strInvResponse = Convert.ToString(sqlReader["VCH_INV_REPLY"]);
                        objNodalDetails.strGmComments = Convert.ToString(sqlReader["VCH_GM_NODAL_COMMENTS"]);
                    }
                    else if (string.Equals(objSearch.strActionCode, "o", StringComparison.OrdinalIgnoreCase))
                    {
                        objNodalDetails.intUserId = Convert.ToInt32(sqlReader["int_user_id"]);
                    }
                    else if (string.Equals(objSearch.strActionCode, "doc", StringComparison.OrdinalIgnoreCase))
                    {
                        objNodalDetails.strFileName = Convert.ToString(sqlReader["VCH_FILE_NAME"]);
                    }
                    list.Add(objNodalDetails);
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
    /// Function created for view and take action page - AMS Version
    /// Created By Ritika lath on 10th April 2018
    /// </summary>
    /// <param name="objProposal">object of type ProposalDet</param>
    /// <returns>List of Type Proposal Det</returns>
    public override List<ProposalDet> getProposalDetails_AMS(ProposalDet objProposal)
    {

        List<ProposalDet> list = new List<ProposalDet>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_PROPOSAL_APPROVALDET_AMS";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objProposal.strAction);
            cmd.Parameters.AddWithValue("@P_INT_USERID", objProposal.intCreatedBy);
            cmd.Parameters.AddWithValue("@PCompanyName", objProposal.compName);
            cmd.Parameters.AddWithValue("@PintStatus", objProposal.intStsdet);
            if (gSqlConn.State == ConnectionState.Closed)
            {
                gSqlConn.Open();
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
                    objProp.intFowardAMS = Convert.ToInt32(sqlReader["intFowardAMS"]);
                    objProp.intForwardIDCO = Convert.ToInt32(sqlReader["intForwardIDCO"]);
                    objProp.strDeptMailContent = Convert.ToString(sqlReader["AMSStatus"]);
                    objProp.IdcoStatus = Convert.ToString(sqlReader["IDCOstatus"]);

                    //added by Ritika lath but commented out for live upload
                    objProp.intAmsQueryStatus = Convert.ToInt32(sqlReader["intAmsQueryStatus"]);
                    objProp.intNodalOfficerId = Convert.ToInt32(sqlReader["nodalOfficerId"]);

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
}