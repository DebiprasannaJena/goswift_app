/*
 * Created By : Ritika Lath
 * Created On : 29th March 2018
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EntityLayer.Proposal;

namespace DataAcessLayer.Proposal
{
    public class PealPaymentProvider
    {
        SqlConnection gConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

        #region Created by Ritika Lath for Update Payment Status page

        /// <summary>
        /// Function to view details regarding proposal when updating the payment details from portal
        /// </summary>
        /// <param name="strActionCode">Action Code</param>
        /// <param name="strProposalNo">Proposal No</param>
        /// <returns></returns>
        public List<PEAL_Update_Payment_Entity> Peal_UpdatePaymentStatus_View(string strActionCode, string strProposalNo)
        {
            List<PEAL_Update_Payment_Entity> list = new List<PEAL_Update_Payment_Entity>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = gConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_UpdatePaymentStatus";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@pVchAction", strActionCode);
                cmd.Parameters.AddWithValue("@pVchProposalNo", strProposalNo);
                if (gConnection.State == ConnectionState.Closed)
                {
                    gConnection.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        PEAL_Update_Payment_Entity objEntity = new PEAL_Update_Payment_Entity();
                        if (string.Equals(strActionCode, "p", StringComparison.OrdinalIgnoreCase))
                        {
                            objEntity.strProposalNo = Convert.ToString(sqlReader["vchProposalNo"]);
                        }
                        else if (string.Equals(strActionCode, "pd", StringComparison.OrdinalIgnoreCase))
                        {
                            objEntity.strInvestorName = Convert.ToString(sqlReader["vchCompname"]);
                            objEntity.strAppliedDate = Convert.ToString(sqlReader["dtmCreatedOn"]);
                        }
                        list.Add(objEntity);
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
        /// Function to update the payment and the correspoonding changes for the same for a particular proposal
        /// </summary>
        /// <param name="objEntity">PEAL_Update_Payment_Entity object</param>
        /// <returns>status as to whether record was updated or not</returns>
        public int Peal_UpdatePaymentStatus_AED(PEAL_Update_Payment_Entity objEntity)
        {
            int intRetValue = 0;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = gConnection;
                cmd.CommandText = "USP_PEAL_UpdatePaymentStatus";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pVchAction", objEntity.strAction);
                cmd.Parameters.AddWithValue("@pVchProposalNo", objEntity.strProposalNo);
                cmd.Parameters.AddWithValue("@pVchChallanRefId", objEntity.strChallanRefId);
                cmd.Parameters.AddWithValue("@pVchTransactionId", objEntity.strBankTransactionId);
                cmd.Parameters.AddWithValue("@pDecChallanNo", objEntity.decChallanAmt);
                cmd.Parameters.AddWithValue("@pDtmPaymentDate", objEntity.strPaymentDate);
                cmd.Parameters.AddWithValue("@pIntCreatedBy", objEntity.intCreatedBy);
                cmd.Parameters.AddWithValue("@pVchOrderNo", objEntity.strOrderNo);
                cmd.Parameters.AddWithValue("@pIntReqId", objEntity.intReqId);
                cmd.Parameters.AddWithValue("@vchSectorIdIT", System.Configuration.ConfigurationManager.AppSettings["SectorIdIT"]);
                cmd.Parameters.AddWithValue("@vchSectorIdTourism", System.Configuration.ConfigurationManager.AppSettings["SectorIdTOURISM"]);
                cmd.Parameters.AddWithValue("@intITdeptId", System.Configuration.ConfigurationManager.AppSettings["DeptIdIT"]);
                cmd.Parameters.AddWithValue("@intTourismdeptId", System.Configuration.ConfigurationManager.AppSettings["DeptIdTOURISM"]);
                cmd.Parameters.AddWithValue("@intIPICOLId", System.Configuration.ConfigurationManager.AppSettings["DeptIdIPICOL"]);
                cmd.Parameters.AddWithValue("@pIntOut", SqlDbType.VarChar);
                cmd.Parameters["@pIntOut"].Direction = ParameterDirection.Output;
                if (gConnection.State == ConnectionState.Closed)
                {
                    gConnection.Open();
                }
                cmd.ExecuteNonQuery();
                object obj = cmd.Parameters["@pIntOut"].Value;
                if (obj != null)
                {
                    intRetValue = Convert.ToInt32(obj);
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                cmd = null;
                gConnection.Close();
            }
            return intRetValue;
        }
        #endregion
    }
}