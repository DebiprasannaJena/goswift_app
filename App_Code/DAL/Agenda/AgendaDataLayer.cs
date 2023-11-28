using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using EntityLayer.Agenda;
using System.Data;

namespace DataAcessLayer.Agenda
{
    public class AgendaDataLayer
    {
        string str_Retvalue = "";
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        #region Add Land details
        public string AddAgenda(int Status, string vchProposalNo, string Remark, string URL)
        {
           
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Add_Agenda";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", "A");
                cmd.Parameters.AddWithValue("@PstrStatus", Status);
                cmd.Parameters.AddWithValue("@PvchProposalNo", vchProposalNo);
                cmd.Parameters.AddWithValue("@PvchRemark", Remark);
                cmd.Parameters.AddWithValue("@PvchUrl", URL);
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

        #region Send Agenda details
        public List<AgendaDet> GetAgendaDet(AgendaDet objAgenda)
        {
            List<AgendaDet> list = new List<AgendaDet>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PEAL_PROMOTER_AED";
                cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("@PvchAction", "K");
                cmd.Parameters.AddWithValue("@PvchAction", objAgenda.strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", objAgenda.vchProposalNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (objAgenda.strAction == "K")
                        {
                            AgendaDet objPropLand = new AgendaDet();
                            objPropLand.ProjectTitle = Convert.ToString(sqlReader["ProjectTitle"]);
                            objPropLand.CompanyName = Convert.ToString(sqlReader["CompanyName"]);
                            objPropLand.sector = Convert.ToInt32(sqlReader["sector"]);
                            objPropLand.DateofApplication = Convert.ToString(sqlReader["DateofApplication"]);
                            objPropLand.Type = Convert.ToString(sqlReader["Type"]);
                            objPropLand.EnvironmentCategory = Convert.ToString(sqlReader["EnvironmentCategory"]);
                            objPropLand.BriefProposal = Convert.ToString(sqlReader["BriefProposal"]);
                            objPropLand.Land = Convert.ToString(sqlReader["Land"]);
                            objPropLand.Water = Convert.ToString(sqlReader["Water"]);
                            objPropLand.Power = Convert.ToString(sqlReader["Power"]);
                            objPropLand.Source = Convert.ToInt32(sqlReader["Source"]);
                            objPropLand.DirectEmployment = Convert.ToInt32(sqlReader["DirectEmployment"]);
                            objPropLand.ContractualEmployment = Convert.ToInt32(sqlReader["ContractualEmployment"]);
                            objPropLand.ImplementationPeriod = Convert.ToString(sqlReader["ImplementationPeriod"]);
                            objPropLand.FinancingDescription = Convert.ToString(sqlReader["FinancingDescription"]);
                            objPropLand.NodId = Convert.ToInt32(objAgenda.NodId);
                            list.Add(objPropLand);
                            objPropLand = null;
                        }
                        else if (objAgenda.strAction == "B")
                        {
                            AgendaDet objPropLand = new AgendaDet();
                            objPropLand.District = Convert.ToInt32(sqlReader["District"]);
                            objPropLand.Location = Convert.ToString(sqlReader["Location"]);
                            list.Add(objPropLand);
                            objPropLand = null;
                        }
                        else if (objAgenda.strAction == "J")
                        {
                            AgendaDet objPropLand = new AgendaDet();
                            objPropLand.Product = Convert.ToString(sqlReader["Product"]);
                            objPropLand.Capacity = Convert.ToString(sqlReader["Capacity"]);
                            list.Add(objPropLand);
                            objPropLand = null;
                        }
                        else if (objAgenda.strAction == "Q")
                        {
                            AgendaDet objPropLand = new AgendaDet();
                            objPropLand.DirectorsName = Convert.ToString(sqlReader["DirectorsName"]);
                            list.Add(objPropLand);
                            objPropLand = null;
                        }
                        else if (objAgenda.strAction == "R")
                        {
                            AgendaDet objPropLand = new AgendaDet();
                            objPropLand.BusinessIntrest = Convert.ToString(sqlReader["BusinessIntrest"]);
                            list.Add(objPropLand);
                            objPropLand = null;
                        }
                        else if (objAgenda.strAction == "N")
                        {
                            AgendaDet objPropLand = new AgendaDet();
                            objPropLand.Description = Convert.ToInt32(sqlReader["Description"]);
                            objPropLand.Cost = Convert.ToString(sqlReader["Cost"]);
                            list.Add(objPropLand);
                            objPropLand = null;
                        }
                        else if (objAgenda.strAction == "O")
                        {
                            AgendaDet objPropLand = new AgendaDet();
                            objPropLand.Description = Convert.ToInt32(sqlReader["Description"]);
                            objPropLand.Cost = Convert.ToString(sqlReader["Cost"]);
                            list.Add(objPropLand);
                            objPropLand = null;
                        }
                        else if (objAgenda.strAction == "W")
                        {
                            AgendaDet objPropLand = new AgendaDet();
                            objPropLand.Description = Convert.ToInt32(sqlReader["Description"]);
                            objPropLand.Amount = Convert.ToString(sqlReader["Amount"]);
                            objPropLand.Percentage = Convert.ToString(sqlReader["Percentage"]);
                            list.Add(objPropLand);
                            objPropLand = null;
                        }
                        else if (objAgenda.strAction == "X")
                        {
                            AgendaDet objPropLand = new AgendaDet();
                            objPropLand.CompanyName = Convert.ToString(sqlReader["CompanyName"]);
                            objPropLand.Particulars = Convert.ToString(sqlReader["Particulars"]);
                            objPropLand.TurnOver = Convert.ToString(sqlReader["TurnOver"]);
                            objPropLand.ProfitAfterTax = Convert.ToString(sqlReader["ProfitAfterTax"]);
                            objPropLand.NetWorth = Convert.ToString(sqlReader["NetWorth"]);
                            objPropLand.FinancialInfo = Convert.ToInt32(sqlReader["FinancialInfo"]);
                            objPropLand.ReserveSurplus = Convert.ToString(sqlReader["ReserveSurplus"]);
                            list.Add(objPropLand);
                            objPropLand = null;
                        }
                        else if (objAgenda.strAction == "Z")
                        {
                            AgendaDet objPropLand = new AgendaDet();
                            objPropLand.document_name = Convert.ToString(sqlReader["document_name"]);
                            objPropLand.document_Link = Convert.ToString(sqlReader["document_Link"]);
                        
                            list.Add(objPropLand);
                            objPropLand = null;
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
        public string AddAgendaDetails(int Status, string vchProposalNo, string Remark, string URL)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Add_Agenda";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", "A");
                cmd.Parameters.AddWithValue("@PstrStatus", Status);
                cmd.Parameters.AddWithValue("@PvchProposalNo", vchProposalNo);
                cmd.Parameters.AddWithValue("@PvchRemark", Remark);
                cmd.Parameters.AddWithValue("@PvchUrl", URL);
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
    }
}
