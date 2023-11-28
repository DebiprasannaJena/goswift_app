using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntityLayer.Mastersector;
using System.Configuration;

namespace DataAcessLayer.MasterSector
{
    public class MasterSectorDataLayer
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        string str_Retvalue = "";
        public string MasterSectorData (MasterSectorDetails objMasterSector)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_SECTOR_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objMasterSector.strAction);
                cmd.Parameters.AddWithValue("@P_INT_SECTOR_ID", objMasterSector.SectorId);
                cmd.Parameters.AddWithValue("@P_INT_SECTOR_CODE", objMasterSector.SectorCode);
                cmd.Parameters.AddWithValue("@P_VCH_SECTOR_NAME", objMasterSector.strSectorName);
                cmd.Parameters.AddWithValue("@P_VCH_SECTOR_DESCRIPTION", objMasterSector.strSectorDescription);
                cmd.Parameters.AddWithValue("@P_INT_SECTOR_PRIORITY", objMasterSector.SectorPriority);
                cmd.Parameters.AddWithValue("@P_INT_POLICY_REFERENCE", objMasterSector.intPolicyReference);
                cmd.Parameters.AddWithValue("@P_DTM_POLICY_REFERENCE", objMasterSector.Policyreference);
                cmd.Parameters.AddWithValue("@P_INT_CREATED_BY", objMasterSector.IntCreatedBy);
                cmd.Parameters.AddWithValue("@P_BIT_DELETED_FLAG", objMasterSector.IntDeletedStatus);
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
        public List<MasterDdl> BindDDl(MasterDdl objDdl)
        {
            List<MasterDdl> list = new List<MasterDdl>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SECTOR_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objDdl.strAction);
                //cmd.Parameters.AddWithValue("@P_INT_SECTOR_ID", objDdl.intPolicyId);
                //cmd.Parameters.AddWithValue("@P_INT_SECTOR_ID", objDdl.strpolicyname);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                MasterDdl objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new MasterDdl();
                        objInner.intPolicyId = Convert.ToInt32(sqlReader["INT_POLICY_ID"]);
                        objInner.strpolicyname = Convert.ToString(sqlReader["VCH_POLICY_NAME"]);

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
                cmd = null;
            }
            return list;
        }
        public List<MasterGrid> BindDropdown(MasterGrid objGdv)
        {
            List<MasterGrid> list = new List<MasterGrid>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SECTOR_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objGdv.strAction);
                //cmd.Parameters.AddWithValue("@P_INT_SECTOR_ID", objDdl.intPolicyId);
                //cmd.Parameters.AddWithValue("@P_INT_SECTOR_CODE", objDdl.SectorCode);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                MasterGrid objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new MasterGrid();
                        objInner.SectorId = Convert.ToInt32(sqlReader["INT_SECTOR_ID"]);
                        objInner.SectorCode = Convert.ToInt32(sqlReader["INT_SECTOR_CODE"]);

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
                cmd = null;
            }
            return list;
        }
        public List<Gridviewgrd> BindDropdowngrd(Gridviewgrd objgrd)
        {
            List<Gridviewgrd> list1 = new List<Gridviewgrd>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SECTOR_DETAILS";
                cmd.Parameters.Clear();                
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objgrd.strAction);
                //cmd.Parameters.AddWithValue("@P_INT_SECTOR_ID", objgrd.SectorId);
                cmd.Parameters.AddWithValue("@P_INT_SECTOR_CODE", objgrd.SectorCode);
                cmd.Parameters.AddWithValue("@P_VCH_SECTOR_NAME", objgrd.strSectorName);
                //cmd.Parameters.AddWithValue("@P_VCH_SECTOR_DESCRIPTION", objgrd.strSectorDescription);
                cmd.Parameters.AddWithValue("@P_INT_SECTOR_PRIORITY", objgrd.SectorPriority);
                //cmd.Parameters.AddWithValue("@P_INT_POLICY_REFERENCE", objgrd.Policyreference);
                
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                Gridviewgrd objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new Gridviewgrd();
                        objInner.SectorId = Convert.ToInt32(sqlReader["INT_SECTOR_ID"]);
                        objInner.SectorCode = Convert.ToInt32(sqlReader["INT_SECTOR_CODE"]);
                        objInner.strSectorName = Convert.ToString(sqlReader["VCH_SECTOR_NAME"]);
                        objInner.strSectorDescription = Convert.ToString(sqlReader["VCH_SECTOR_DESCRIPTION"]);
                        objInner.SectorPriority = Convert.ToInt32(sqlReader["INT_SECTOR_PRIORITY"]);
                        objInner.intPolicyReference = Convert.ToInt32(sqlReader["INT_POLICY_REFERENCE"]);
                        list1.Add(objInner);
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
                cmd = null;
            }
            return list1;
        }
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


        public MasterSectorDetails EditData(int sectorId)
        {
            //List<Gridviewgrd> list1 = new List<Gridviewgrd>();
            MasterSectorDetails objInner = new MasterSectorDetails(); 
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SECTOR_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "E");
                cmd.Parameters.AddWithValue("@P_INT_SECTOR_ID", sectorId);
               

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                         
                        objInner.SectorId = Convert.ToInt32(sqlReader["INT_SECTOR_ID"]);
                        objInner.SectorCode = Convert.ToInt32(sqlReader["INT_SECTOR_CODE"]);
                        objInner.strSectorName = Convert.ToString(sqlReader["VCH_SECTOR_NAME"]);
                        objInner.strSectorDescription = Convert.ToString(sqlReader["VCH_SECTOR_DESCRIPTION"]);
                        objInner.SectorPriority = Convert.ToInt32(sqlReader["INT_SECTOR_PRIORITY"]);
                        objInner.intPolicyReference = Convert.ToInt32(sqlReader["INT_POLICY_REFERENCE"]);
                        objInner.Policyreference = Convert.ToDateTime(sqlReader["DTM_POLICY_REFERENCE"]);
                        //list1.Add(objInner);
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
                cmd = null;
            }
            return objInner;
        }
        
        //public DataSet BindDDl(MasterDetails objSector)
        //{
        //    //using (SqlConnection con = new SqlConnection(con))
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        con.Open();
        //        cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSector.strAction);
        //        cmd.CommandText = "USP_SECTOR_DETAILS";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection = con;
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        return ds;
        //    }
        //}
     
    }

}
