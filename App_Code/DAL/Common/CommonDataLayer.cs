using System;
using EntityLayer.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;

namespace DataAcessLayer.Common
{
    public class CommonDataLayer
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        string str_Retvalue = "";
        public string ManageFeedback(Feedback objFeedback)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_FEEDBACK";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objFeedback.strAction);
                cmd.Parameters.AddWithValue("@PvchFirstName", objFeedback.vchFirstName);
                cmd.Parameters.AddWithValue("@PvchLastName", objFeedback.vchLastName);
                cmd.Parameters.AddWithValue("@PvchEmail", objFeedback.vchEmail);
                cmd.Parameters.AddWithValue("@PvchMobileNo", objFeedback.vchMobileNo);
                cmd.Parameters.AddWithValue("@PvchSubject", objFeedback.vchSubject);
                cmd.Parameters.AddWithValue("@PvchFeedback", objFeedback.vchFeedback);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objFeedback.intCreatedBy);
                cmd.Parameters.AddWithValue("@PbitDeletedFlag", objFeedback.bitDeletedFlag);
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

        public List<Feedback> ViewFeedback(Feedback objectFeedback)
        {
            List<Feedback> list = new List<Feedback>();
            
            SqlCommand cmd1 = new SqlCommand();
            try
            {
                
                cmd1.Connection = con;
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.CommandText = "USP_FEEDBACK";
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd1.Parameters.AddWithValue("@P_CHAR_ACTION", objectFeedback.strAction);
                cmd1.Parameters.AddWithValue("@PvchName", objectFeedback.StrFullName);
                cmd1.Parameters.AddWithValue("@PvchEmail", objectFeedback.vchEmail);
                cmd1.Parameters.AddWithValue("@PvchMobileNo", objectFeedback.vchMobileNo);
                cmd1.Parameters.AddWithValue("@PvchFromDate", objectFeedback.StrFromDate);
                cmd1.Parameters.AddWithValue("@PvchToDate", objectFeedback.StrToDate);
                cmd1.Parameters.AddWithValue("@PvchSubject", objectFeedback.vchSubject);
                SqlDataReader sqlReader = cmd1.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        Feedback objFeedback = new Feedback();
                        objFeedback.intFeedbackId = Convert.ToInt32(sqlReader["intFeedbackId"]);
                        objFeedback.vchFirstName = Convert.ToString(sqlReader["vchFirstName"]);
                        objFeedback.vchEmail = Convert.ToString(sqlReader["vchEmail"]);
                        objFeedback.vchMobileNo = Convert.ToString(sqlReader["vchMobileNo"]);
                        objFeedback.vchSubject = Convert.ToString(sqlReader["vchSubject"]);
                        objFeedback.vchFeedback = Convert.ToString(sqlReader["vchFeedback"]);
                        objFeedback.StrDate = Convert.ToString(sqlReader["Date"]);
                        list.Add(objFeedback);
                    }
                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd1 = null; }
            return list;
        }

        public List<Gallery> ViewGallery(Gallery objectGallery)
        {
            List<Gallery> list = new List<Gallery>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Gallery";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objectGallery.strAction);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        Gallery objGallery = new Gallery();
                        objGallery.intImageId = Convert.ToInt32(sqlReader["intImageId"]);
                        objGallery.vchImage = Convert.ToString(sqlReader["vchImage"]);
                        objGallery.vchImgDescription = Convert.ToString(sqlReader["vchImgDescription"]);
                       
                        list.Add(objGallery);
                    }
                    sqlReader.Close();
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return list;
        }


        public string ManageGallery(Gallery objGallery)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_Gallery";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objGallery.strAction);
                cmd.Parameters.AddWithValue("@PvchImgDescription", objGallery.vchImgDescription);
                cmd.Parameters.AddWithValue("@PvchImage", objGallery.vchImage);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objGallery.intCreatedBy);
                cmd.Parameters.AddWithValue("@PbitDeletedFlag", objGallery.bitDeletedFlag);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters.AddWithValue("@PINTIMAGEID", objGallery.intImageId);
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

        public Gallery EditGallery(int ImageId)
        {
            Gallery objGallery = new Gallery();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Gallery";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "E");
                cmd.Parameters.AddWithValue("@PintImageId", ImageId);


                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objGallery.intImageId = Convert.ToInt32(sqlReader["intImageId"]);
                        objGallery.vchImgDescription = Convert.ToString(sqlReader["vchImgDescription"]);
                        objGallery.vchImage = Convert.ToString(sqlReader["vchImage"]);
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
            return objGallery;
        }

        public string DeleteGalleryData(Gallery objGallery)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_Gallery";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objGallery.strAction);
                cmd.Parameters.AddWithValue("@PintImageId", objGallery.intImageId);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objGallery.intCreatedBy);
                cmd.Parameters.AddWithValue("@PbitDeletedFlag", objGallery.bitDeletedFlag);
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

        public DataTable FillUnitType(string Action)
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ACTIONCODE", Action);
                cmd.CommandText = "USP_INCT_FILL_UNIT";
                dt.Load(cmd.ExecuteReader());
                return dt;
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
                dt = null;
            }
        }
        #region Added By Prakash Swain
        #region Fill Patent Category
        public DataTable FillPatentCategory()
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INCT_FILL_PATENT_CATEGORY";
                cmd.Parameters.Add("@P_VCH_ACTION", SqlDbType.VarChar);
                cmd.Parameters["@P_VCH_ACTION"].Value = "A";
                dt.Load(cmd.ExecuteReader());
                return dt;
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
        }
        #endregion
        #region Fill Patent Sub Category
        public DataTable FillPatentSubCategory(int CategoryId)
        {
            con.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "exec USP_INCT_FILL_PATENT_CATEGORY @P_VCH_ACTION='B', @P_INT_CATEGORY_ID='" + CategoryId + "'";
                dt.Load(cmd.ExecuteReader());
                return dt;
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
        }
        #endregion
        #endregion
    }

}
