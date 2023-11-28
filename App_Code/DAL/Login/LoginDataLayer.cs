using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLayer.Login;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataAcessLayer.Login
{
    public class LoginDataLayer
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        string Str_RetValue = "";
        //public List<LoginDetails> LoginData(LoginDetails objLogin)

        public List<LoginDetails> SWPLogin(string strAction, string strUserId, string strPWD)
        {

            List<LoginDetails> list = new List<LoginDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@P_VCH_USERID", strUserId);
                cmd.Parameters.AddWithValue("@P_VCH_PASSWORD", strPWD);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    if (sqlReader.Read())
                    {
                        LoginDetails objLogin = new LoginDetails();
                        objLogin.strUserID = sqlReader["VCH_INV_USERID"].ToString();
                        objLogin.strPassword = sqlReader["VCH_INV_PASSWORD"].ToString();
                        objLogin.intInvestorId = Convert.ToInt32(sqlReader["INT_INVESTOR_ID"].ToString());
                        objLogin.strUserName = sqlReader["USERNAME"].ToString();
                        objLogin.strRegDate = sqlReader["REGDATE"].ToString();
                        objLogin.strGSTIN = sqlReader["VCH_GSTIN"].ToString();
                        objLogin.strUID = sqlReader["VCH_UNIQUEID"].ToString();
                        objLogin.strInvName = sqlReader["VCH_INV_NAME"].ToString();
                        list.Add(objLogin);
                    }
                    sqlReader.Close();
                }
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; sqlReader.Close(); }

        }

        public List<LoginDetails> getUserDetails(LoginDetails objLogin)
        {

            List<LoginDetails> list = new List<LoginDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_USERID", objLogin.strUserID);
                cmd.Parameters.AddWithValue("@P_VCH_EMAIL", objLogin.strEmail);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    if (sqlReader.Read())
                    {
                        objLogin.strUserID = sqlReader["VCH_INV_USERID"].ToString();
                        objLogin.strEmail = sqlReader["VCH_EMAIL"].ToString();
                        objLogin.strMobile = sqlReader["MOBILE"].ToString();
                        objLogin.strInvName = sqlReader["VCH_INV_NAME"].ToString();
                        list.Add(objLogin);
                    }
                    sqlReader.Close();
                }
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }

        public string UpdatePassword(LoginDetails objLogin)
        {
            List<LoginDetails> list = new List<LoginDetails>();
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_USERID", objLogin.strUserID);
                cmd.Parameters.AddWithValue("@P_VCH_EMAIL", objLogin.strEmail);
                cmd.Parameters.AddWithValue("@P_VCH_PASSWORD", objLogin.strPassword);
                cmd.ExecuteNonQuery();
                Str_RetValue = "2";
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; con.Close(); }
            return Str_RetValue;
        }

        public List<LoginDetails> ViewChngPwd(LoginDetails objLogin)
        {

            List<LoginDetails> list = new List<LoginDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_USERID", objLogin.strUserID);
                cmd.Parameters.Add("@P_VCH_OLD_PASSWORD", SqlDbType.VarChar).Value = objLogin.strOldPassword;
                cmd.Parameters.Add("@P_VCH_NEW_PASSWORD", SqlDbType.VarChar).Value = objLogin.strNewPassword;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    if (sqlReader.Read())
                    {
                        objLogin.strUserID = sqlReader["VCH_INV_USERID"].ToString();
                        objLogin.strOldPassword = sqlReader["VCH_INV_PASSWORD"].ToString();
                        objLogin.strMobile = sqlReader["MOBILE"].ToString();
                        list.Add(objLogin);
                    }
                    sqlReader.Close();
                }
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }

        public string ManageChngPwd(LoginDetails objLogin)
        {
            List<LoginDetails> list = new List<LoginDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_USERID", objLogin.strUserID);
                cmd.Parameters.Add("@P_VCH_OLD_PASSWORD", SqlDbType.VarChar).Value = objLogin.strOldPassword;
                cmd.Parameters.Add("@P_VCH_NEW_PASSWORD", SqlDbType.VarChar).Value = objLogin.strNewPassword;
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.Int);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                Str_RetValue = "2";
                conn.Close();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return Str_RetValue;
        }

        public List<LoginDetails> getDeptUserDetails(LoginDetails objLogin)
        {

            List<LoginDetails> list = new List<LoginDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UserMaster_View";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CHRACTIONCODE", objLogin.strAction);
                //cmd.Parameters.AddWithValue("@P_VCH_USERID", objLogin.strUserID);
                cmd.Parameters.AddWithValue("@P_VCH_EMAIL", objLogin.strEmail);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    if (sqlReader.Read())
                    {
                        objLogin.strUserID = sqlReader["intUserId"].ToString();
                        objLogin.strEmail = sqlReader["vchEmail"].ToString();
                        objLogin.strMobile = sqlReader["vchMobTel"].ToString();
                        list.Add(objLogin);
                    }
                    sqlReader.Close();
                }
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }

        public string UpdateDeptPassword(LoginDetails objLogin)
        {
            List<LoginDetails> list = new List<LoginDetails>();
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UserMaster_View";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CHRACTIONCODE", objLogin.strAction);
                //cmd.Parameters.AddWithValue("@P_VCH_USERID", objLogin.strUserID);
                cmd.Parameters.AddWithValue("@P_VCH_EMAIL", objLogin.strEmail);
                cmd.Parameters.AddWithValue("@VCHPASSWORD", objLogin.strPassword);
                cmd.ExecuteNonQuery();
                Str_RetValue = "2";
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; con.Close(); }
            return Str_RetValue;
        }

        public List<LoginDetails> ViewDeptChngPwd(LoginDetails objLogin)
        {

            List<LoginDetails> list = new List<LoginDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UserMaster_View";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CHRACTIONCODE", objLogin.strAction);
                cmd.Parameters.AddWithValue("@INTUSERID", objLogin.strUserID);
                cmd.Parameters.Add("@P_VCH_OLD_PASSWORD", SqlDbType.VarChar).Value = objLogin.strOldPassword;
                cmd.Parameters.Add("@P_VCH_NEW_PASSWORD", SqlDbType.VarChar).Value = objLogin.strNewPassword;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    if (sqlReader.Read())
                    {
                        objLogin.strUserID = sqlReader["INTUSERID"].ToString();
                        objLogin.strOldPassword = sqlReader["vchPassWord"].ToString();
                        objLogin.strMobile = sqlReader["vchMobTel"].ToString();

                        list.Add(objLogin);
                    }
                    sqlReader.Close();
                }
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }
        public List<LoginDetails> ViewEmailAndMobile(LoginDetails objLogin)
        {

            List<LoginDetails> list = new List<LoginDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UserMaster_View";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CHRACTIONCODE", objLogin.strAction);
                cmd.Parameters.AddWithValue("@VCHUSERNAME", objLogin.strUserName);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    if (sqlReader.Read())
                    {
                        objLogin.strUserID = sqlReader["INTUSERID"].ToString();
                        objLogin.strEmail = sqlReader["vchEmail"].ToString();
                        objLogin.strMobile = sqlReader["vchMobTel"].ToString();

                        list.Add(objLogin);
                    }
                    sqlReader.Close();
                }
                return list;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

        }


        public string ManageDeptChngPwd(LoginDetails objLogin)
        {
            List<LoginDetails> list = new List<LoginDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UserMaster_View";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CHRACTIONCODE", objLogin.strAction);
                cmd.Parameters.AddWithValue("@INTUSERID", objLogin.strUserID);
                cmd.Parameters.Add("@P_VCH_OLD_PASSWORD", SqlDbType.VarChar).Value = objLogin.strOldPassword;
                cmd.Parameters.Add("@P_VCH_NEW_PASSWORD", SqlDbType.VarChar).Value = objLogin.strNewPassword;
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.Int);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                conn.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return Str_RetValue;
        }

        public string ManageMobileAndEmail(LoginDetails objLogin)
        {
            List<LoginDetails> list = new List<LoginDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UserMaster_View";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CHRACTIONCODE", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_EMAIL", SqlDbType.VarChar).Value = objLogin.strEmail;
                cmd.Parameters.Add("@VCHMOBILENO", SqlDbType.VarChar).Value = objLogin.strMobile;
                cmd.Parameters.Add("@VCHUSERNAME", SqlDbType.VarChar).Value = objLogin.strUserName;
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.Int);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                Str_RetValue = "2";
                conn.Close();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return Str_RetValue;
        }
        public string DeptEditProfile(LoginDetails objLogin)
        {
            List<LoginDetails> list = new List<LoginDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_UserMaster_View";
                cmd.Parameters.Clear();
                conn.Open();
                cmd.Parameters.AddWithValue("@CHRACTIONCODE", objLogin.strAction);
                cmd.Parameters.AddWithValue("@INTUSERID", objLogin.strUserID);
                cmd.Parameters.Add("@VCHFULLNAME", SqlDbType.VarChar).Value = objLogin.strFullname;
                cmd.Parameters.Add("@VCHMOBILENO", SqlDbType.VarChar).Value = objLogin.strMobile;
                cmd.Parameters.Add("@P_VCH_EMAIL", SqlDbType.VarChar).Value = objLogin.strEmail;
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.Int);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                Str_RetValue = "2";
                //conn.Close();
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return Str_RetValue;
        }
        public string ManageTokenNumber(string strAction, string stremail, string strtoken, string strtokentime)
        {
            List<LoginDetails> list = new List<LoginDetails>();
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@P_VCH_tOKENNO", strtoken);
                cmd.Parameters.AddWithValue("@P_VCH_TOKENTIME", strtokentime);
                cmd.Parameters.AddWithValue("@P_VCH_EMAIL", stremail);
                cmd.Parameters.AddWithValue("@P_VCH_USERID", stremail);
                cmd.ExecuteNonQuery();
                Str_RetValue = "2";
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; con.Close(); }
            return Str_RetValue;
        }
        public List<LoginDetails> GetInvDetails(LoginDetails objLogin)
        {
            List<LoginDetails> list = new List<LoginDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_tOKENNO", objLogin.strTokenno);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    if (sqlReader.Read())
                    {
                        objLogin.strUserID = sqlReader["VCH_INV_USERID"].ToString();
                        objLogin.strOldPassword = sqlReader["VCH_INV_PASSWORD"].ToString();
                        objLogin.strTokenno = sqlReader["VCH_TOKENTIME"].ToString();
                        objLogin.strMobile = sqlReader["VCH_OFF_MOBILE"].ToString();
                        objLogin.strUID = sqlReader["VCH_UNIQUEID"].ToString();
                        objLogin.intInvestorId = Convert.ToInt32(sqlReader["INT_INVESTOR_ID"].ToString());
                        objLogin.IntIndustryType = Convert.ToInt32(sqlReader["INT_INDUSTRY_TYPE"].ToString());

                        list.Add(objLogin);
                    }
                    sqlReader.Close();
                }
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
        public DataTable GetTokenDetails(string action, string tokenno)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
                cmd.Parameters.AddWithValue("@P_VCH_tOKENNO", tokenno);
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
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
        public string UpdateLoginFailedStatus(LoginDetails objLogin)
        {
            List<LoginDetails> list = new List<LoginDetails>();
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_USERID", objLogin.strUserID);
                cmd.Parameters.AddWithValue("@P_VCH_TOKENTIME", objLogin.strlogtime);
                cmd.ExecuteNonQuery();
                Str_RetValue = "2";
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; con.Close(); }
            return Str_RetValue;
        }
        public DataTable GetLogFailedDetails(LoginDetails objLogin)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_USERID", objLogin.strEmail);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
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


        public DataTable LoginGOSWIFT(LoginDetails objLogin)
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
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_USERID", objLogin.strUserID);
                cmd.Parameters.AddWithValue("@P_VCH_PASSWORD", objLogin.strPassword);
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

        /// <summary>
        /// Added by Sushant Jena on Dt.31-May-2018
        /// To check whether a user have PAN or Not.
        /// </summary>
        /// <param name="objLogin"></param>
        /// <returns>
        /// "1" - User have a PAN 
        /// "2" - Invalid Used
        /// "3" - User doesnot have a PAN
        /// </returns>
        public string checkPanAvailStatus(LoginDetails objLogin)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CHECK_PAN_EXIST";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_USER_ID", objLogin.strUserID);

                SqlParameter par1 = new SqlParameter("@P_VCH_OUT_MSG", SqlDbType.VarChar, 10);
                par1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par1);

                cmd.ExecuteNonQuery();

                string strResult = Convert.ToString(cmd.Parameters["@P_VCH_OUT_MSG"].Value);

                return strResult;
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
        }

        /// <summary>
        /// Added By Sushant Jena On Dt.01-Jun-2018
        /// To Update PAN of an user
        /// </summary>
        /// <param name="objLogin"></param>
        /// <returns>
        /// 1 - PAN Updated Sucessfully
        /// 2 - Internal Error
        /// </returns>
        public string PAN_AED(LoginDetails objLogin)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CHECK_PAN_EXIST";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_USER_ID", objLogin.strUserID);
                cmd.Parameters.AddWithValue("@P_VCH_INVESTOR_ID", objLogin.intInvestorId);
                cmd.Parameters.AddWithValue("@P_VCH_PAN", objLogin.strPAN);
                cmd.Parameters.AddWithValue("@P_VCH_EIN_IEM", objLogin.strEINIEM);
                cmd.Parameters.AddWithValue("@P_VCH_LICENCE_NO_TYPE", objLogin.strLicenseNoType);
                cmd.Parameters.AddWithValue("@P_VCH_LICENCE_DOC", objLogin.strLicenseDoc);

                SqlParameter par1 = new SqlParameter("@P_VCH_OUT_MSG", SqlDbType.VarChar, 2000);
                par1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par1);

                cmd.ExecuteNonQuery();

                string strResult = Convert.ToString(cmd.Parameters["@P_VCH_OUT_MSG"].Value);

                return strResult;
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
        }

        /// <summary>
        /// Added by Sushant Jena On Dt.24-Aug-2018
        /// To Recovery user id through PAN or Email
        /// Section:- Forget User Id
        /// </summary>
        /// <param name="objLogin"></param>
        /// <returns>
        /// 1-PAN or Email Exist
        /// 2-Invalid Id Provided
        /// </returns>
        public DataTable forgotUserId(LoginDetails objLogin)
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
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_EMAIL", objLogin.strEmail);
                cmd.Parameters.AddWithValue("@P_VCH_PAN", objLogin.strPAN);
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

        /// <summary>
        /// Added by Sushant Jena On Dt.14-Sep-2018
        /// To view profile details of a investor
        /// Section:- Change User Id Alias Name
        /// </summary>
        /// <param name="objLogin"></param>
        /// <returns></returns>
        public DataTable viewInvestorDetails(LoginDetails objLogin)
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
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objLogin.intInvestorId);
                cmd.Parameters.AddWithValue("@P_VCH_UNIQUE_ID", objLogin.strUniqueId);

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

        /// <summary>
        /// Added by Sushant Jena On Dt.14-Sep-2018
        /// Create User Id Alias Name
        /// Section:- Change User Id Alias Name
        /// </summary>
        /// <param name="objLogin"></param>
        /// <returns></returns>
        public string createAliasName(LoginDetails objLogin)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objLogin.intInvestorId);
                cmd.Parameters.AddWithValue("@P_VCH_USERID_ALIAS", objLogin.strUserIdAlias);
                cmd.Parameters.AddWithValue("@P_INT_ALIAS_NAME_COUNT", objLogin.intAliasNameCount);

                SqlParameter par1 = new SqlParameter("@P_OUT_MSG", SqlDbType.VarChar, 2000);
                par1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par1);

                cmd.ExecuteNonQuery();

                string strResult = Convert.ToString(cmd.Parameters["@P_OUT_MSG"].Value);

                return strResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
                conn.Dispose();
            }
        }


        /// <summary>
        /// Added by Sushant Jena On Dt.21-Nov-2018
        /// Reset Alias Name
        /// Section:- During first login
        /// </summary>
        /// <param name="objLogin"></param>
        /// <returns></returns>
        public string resetAliasName(LoginDetails objLogin)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objLogin.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objLogin.intInvestorId);

                SqlParameter par1 = new SqlParameter("@P_OUT_MSG", SqlDbType.VarChar, 2000);
                par1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par1);

                cmd.ExecuteNonQuery();

                string strResult = Convert.ToString(cmd.Parameters["@P_OUT_MSG"].Value);

                return strResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                conn.Close();
                conn.Dispose();
            }
        }
    }
}