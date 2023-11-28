using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using EntityLayer.Master;
using System.Data;
using System.Configuration;

namespace DataAcessLayer.Master
{
    public class MasterDatalayer
    {
        //SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        SqlConnection con =new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        string str_Retvalue = "";
        public string SectorData(MasterDetails objSector)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_SECTOR_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSector.strAction);
                cmd.Parameters.AddWithValue("@P_INT_SECTOR_CODE", objSector.SectorCode);
                cmd.Parameters.AddWithValue("@P_VCH_SECTOR_NAME", objSector.strSectorName);
                cmd.Parameters.AddWithValue("@P_VCH_SECTOR_DESCRIPTION", objSector.strSectorDescription);
                cmd.Parameters.AddWithValue("@P_INT_SECTOR_PRIORITY", objSector.SectorPriority);
                cmd.Parameters.AddWithValue("@P_INT_POLICY_REFERENCE", objSector.intPolicyReference);
                cmd.Parameters.AddWithValue("@P_DTM_POLICY_REFERENCE", objSector.Policyreference);
                cmd.Parameters.AddWithValue("@P_INT_CREATED_BY", objSector.IntCreatedBy);
                cmd.Parameters.AddWithValue("@P_BIT_DELETED_FLAG", objSector.IntDeletedStatus);
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
                con.Close();
                cmd.Dispose();
            }
            return str_Retvalue;
        }
        //public List<SectorDetails> ViewSectorDetails(SectorDetails objSector)
        //{
        //    List<SectorDetails> list = new List<SectorDetails>();
        //    SqlDataReader sqlReader = null;
        //    SqlCommand cmd = new SqlCommand();
        //    try
        //    {
        //        cmd.Connection = con;
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.CommandText = "USP_SECTOR_DETAILS";
        //        cmd.Parameters.Clear();
        //        cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSector.strAction);
        //        cmd.Parameters.AddWithValue("@P_INT_SECTOR_ID", objSector.SectorId);
        //        if (con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }
        //        sqlReader = cmd.ExecuteReader();
        //        if (sqlReader.HasRows)
        //        {
        //            if (sqlReader.Read())
        //            {
        //                objSector.SectorCode = Convert.ToInt32(sqlReader["INT_SECTOR_CODE"]);
        //                objSector.strSectorName = Convert.ToString(sqlReader["VCH_SECTOR_NAME"]);
        //                objSector.strSectorDescription = Convert.ToString(sqlReader["VCH_SECTOR_DESCRIPTION"]);
        //                objSector.SectorPriority = Convert.ToInt32(sqlReader["INT_SECTOR_PRIORITY"]);
        //                objSector.PolicyReference = Convert.ToInt32(sqlReader["INT_POLICY_REFERENCE"]);
        //                objSector.PolicyReference = Convert.ToDateTime(sqlReader["DTM_POLICY_REFERENCE"]);
        //                list.Add(objSector);
        //            }
        //            sqlReader.Close();
        //        }
        //    }
        //    catch (NullReferenceException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    { 
        //        throw ex; 
        //    }
        //    finally 
        //    { 
        //        cmd = null; 
        //    }
        //    return list;
        //}
        public DataTable ViewSectorDetails(string Id, string action)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            

            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_SECTOR_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
                cmd.Parameters.AddWithValue("@P_INT_SECTOR_ID", Id);
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
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
            return dt;
        }
        public DataSet BindDDl(MasterDetails objSector)
        {
            //using (SqlConnection con = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSector.strAction);
                cmd.CommandText = "USP_SECTOR_DETAILS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
     
        
    }
}