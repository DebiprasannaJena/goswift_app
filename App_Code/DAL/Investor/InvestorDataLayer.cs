using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using EntityLayer.Investor;
using System.Data;

namespace DataAcessLayer.Investor
{
    public class InvestorDataLayer
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        string Str_RetValue = "";
        public string InvestorData(InvestorDetails objInvestor)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ACTION", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@VCH_INV_NAME", objInvestor.strIndName);
                cmd.Parameters.AddWithValue("@INT_COUNTRY", objInvestor.Country);
                cmd.Parameters.AddWithValue("@INT_SALUTATION", objInvestor.Salutation);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_FIRSTNAME", objInvestor.strContactFirstName);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_MIDDLENAME", objInvestor.strContactMiddleName);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_LASTNAME", objInvestor.strContactLastName);
                cmd.Parameters.AddWithValue("@VCH_OFF_MOBILE", objInvestor.MobNo);
                cmd.Parameters.AddWithValue("@VCH_ADDRESS", objInvestor.strAddress);
                cmd.Parameters.AddWithValue("@VCH_INV_USERID", objInvestor.strUserID);
                cmd.Parameters.AddWithValue("@VCH_INV_PHOTO", objInvestor.Photo);
                cmd.Parameters.AddWithValue("@INT_EMAIL_STATUS", objInvestor.EmailStatus);
                cmd.Parameters.AddWithValue("@VCH_EMAIL", objInvestor.strEmail); ///// added by Sushant Jena on Dt.15-Oct-2018
                cmd.Parameters.AddWithValue("@INT_SMS_STATUS", objInvestor.SmsStatus);
                cmd.Parameters.AddWithValue("@INT_TERM_STATUS", objInvestor.TermStatus);
                cmd.Parameters.AddWithValue("@INT_CREATED_BY", objInvestor.IntCreatedBy);
                cmd.Parameters.AddWithValue("@INT_INDUSTRY_GROUP_ID", objInvestor.IntIndGroupID);
                cmd.Parameters.AddWithValue("@VCH_INDUSTRY_NAME", objInvestor.strIndGroupName);
                cmd.Parameters.AddWithValue("@INT_STATUS", objInvestor.IntIndStatus);
                cmd.Parameters.AddWithValue("@INT_DISTRICT", objInvestor.intDistrictId);
                cmd.Parameters.AddWithValue("@INT_BLOCK", objInvestor.intBlockId);
                cmd.Parameters.AddWithValue("@INT_SECTOR", objInvestor.intSectorId);
                cmd.Parameters.AddWithValue("@INT_SUBSECTOR", objInvestor.intSubSectorId);
                cmd.Parameters.AddWithValue("@VAR_SITELOCATION", objInvestor.strSiteAddress);
                cmd.Parameters.AddWithValue("@DEC_INVESTAMOUNT", objInvestor.decInvstAmount);
                cmd.Parameters.AddWithValue("@INT_CATEGORY", objInvestor.intCategoryId);
                cmd.Parameters.AddWithValue("@INT_ENTITY_TYPE", objInvestor.intEntitytype); // add by anil
                cmd.Parameters.AddWithValue("@VCH_CIN_NUMBER", objInvestor.strCINnumber); // add by anil
                cmd.Parameters.AddWithValue("@VCH_REG_ADDRESS_2", objInvestor.StrRegAddress_2);
                cmd.Parameters.AddWithValue("@INT_REG_COUNTRY", objInvestor.IntRegCountry);
                cmd.Parameters.AddWithValue("@VCH_REG_STATE", objInvestor.StrRegState);
                cmd.Parameters.AddWithValue("@VCH_REG_CITY", objInvestor.StrRegCity);
                cmd.Parameters.AddWithValue("@VCH_REG_PIN", objInvestor.StrRegPincode);
                cmd.Parameters.AddWithValue("@VCH_SL_ADDRESS_2", objInvestor.StrSlAddress_2);
                cmd.Parameters.AddWithValue("@INT_SL_COUNTRY", objInvestor.IntSlCountry);
                cmd.Parameters.AddWithValue("@VCH_SL_STATE", objInvestor.StrSlState);
                cmd.Parameters.AddWithValue("@VCH_SL_CITY", objInvestor.StrSlCity);
                cmd.Parameters.AddWithValue("@VCH_SL_PIN", objInvestor.StrSlPincode);
                cmd.Parameters.AddWithValue("@VCH_LLPIN_NUMBER", objInvestor.StrLLPINumber);

                cmd.Parameters.AddWithValue("@VCHMSG_OUT", SqlDbType.VarChar);
                cmd.Parameters["@VCHMSG_OUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@VCHMSG_OUT"].Value.ToString();
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
            return Str_RetValue;
        }

        public IList<InvestorDetails> ViewInvestorDetailsWeb(InvestorDetails objInvestor)
        {
            IList<InvestorDetails> list = new List<InvestorDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_INV_USERID", objInvestor.strUserID);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    if (sqlReader.Read())
                    {
                        objInvestor.strIndName = Convert.ToString(sqlReader["VCH_INV_NAME"]);
                        objInvestor.Country = Convert.ToInt32(sqlReader["INT_COUNTRY"]);
                        objInvestor.strEmail = Convert.ToString(sqlReader["VCH_EMAIL"]);
                        objInvestor.Salutation = Convert.ToInt32(sqlReader["INT_SALUTATION"]);
                        objInvestor.strContactFirstName = Convert.ToString(sqlReader["VCH_CONTACT_FIRSTNAME"]);
                        objInvestor.strContactMiddleName = Convert.ToString(sqlReader["VCH_CONTACT_MIDDLENAME"]);
                        objInvestor.strContactLastName = Convert.ToString(sqlReader["VCH_CONTACT_LASTNAME"]);
                        objInvestor.MobNo = Convert.ToString(sqlReader["VCH_OFF_MOBILE"]);
                        objInvestor.strAddress = Convert.ToString(sqlReader["VCH_ADDRESS"]);
                        objInvestor.strUserID = Convert.ToString(sqlReader["VCH_INV_USERID"]);
                        objInvestor.Photo = Convert.ToString(sqlReader["VCH_INV_PHOTO"]);
                        list.Add(objInvestor);
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

        public List<InvestorDetails> ViewInvestorDetailsPortal(InvestorDetails objInvestor)
        {
            List<InvestorDetails> list = new List<InvestorDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ACTION", objInvestor.strAction);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        InvestorDetails objInv = new InvestorDetails();
                        objInv.strIndName = Convert.ToString(sqlReader["VCH_INV_NAME"]);
                        objInv.strContactFirstName = Convert.ToString(sqlReader["VCH_CONTACT_FIRSTNAME"]);
                        objInv.strContactMiddleName = Convert.ToString(sqlReader["VCH_CONTACT_MIDDLENAME"]);
                        objInv.strContactLastName = Convert.ToString(sqlReader["VCH_CONTACT_LASTNAME"]);
                        objInv.MobNo = Convert.ToString(sqlReader["VCH_OFF_MOBILE"]);
                        objInv.strAddress = Convert.ToString(sqlReader["VCH_ADDRESS"]);
                        objInv.strUserID = Convert.ToString(sqlReader["VCH_INV_USERID"]);
                        objInv.strSecAnswer = Convert.ToString(sqlReader["CONTACTNAME"]);
                        objInv.IntIndGroupID = Convert.ToInt32(sqlReader["INT_INDUSTRY_GROUP_ID"]);
                        objInv.strIndGroupName = Convert.ToString(sqlReader["VCH_INDUSTRY_NAME"]);
                        objInv.IntIndStatus = Convert.ToInt32(sqlReader["INT_STATUS"]);
                        objInv.IntInvestorId = Convert.ToInt32(sqlReader["INT_INVESTOR_ID"]);
                        objInv.StrRemarks = Convert.ToString(sqlReader["VCH_REMARKS"]);
                        objInv.intOTPStatus = Convert.ToInt32(sqlReader["INT_OTP_STATUS"]);
                        list.Add(objInv);
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

        public DataTable ViewInvestorDetails(string UserId, string action)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
                cmd.Parameters.AddWithValue("@P_VCH_INV_USERID", UserId);
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}
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

        public string ApproveIndGroup(InvestorDetails objInvestor)
        {
            List<InvestorDetails> list = new List<InvestorDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_APPROVAL";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_INDUSTRY_NAME", objInvestor.strIndGroupName);
                cmd.ExecuteNonQuery();
                Str_RetValue = "2"; ;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; conn.Close(); }
            return Str_RetValue;
        }

        public string RejectIndGroup(InvestorDetails objInvestor)
        {
            List<InvestorDetails> list = new List<InvestorDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_APPROVAL";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_INDUSTRY_NAME", objInvestor.strIndGroupName);
                cmd.ExecuteNonQuery();
                Str_RetValue = "2"; ;
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; conn.Close(); }
            return Str_RetValue;
        }

        public string ViewRejectIndGroup(InvestorDetails objInvestor)
        {
            List<InvestorDetails> list = new List<InvestorDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Connection = conn;
                //conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_INDUSTRY_NAME", objInvestor.strIndGroupName);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; conn.Close(); }
            return Str_RetValue;
        }

        #region Populate Dropdown
        /// <summary>
        /// Created By  :   AMit Sahoo
        /// Created On  :   2nd Aug 2017
        /// To Populate Industry Group in Dropdownlist
        /// </summary>
        /// <param name="objInvestor"></param>
        /// <returns></returns>
        public IList<InvestorDetails> PopulateIndustryGroups()
        {
            IList<InvestorDetails> objList = new List<InvestorDetails>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "IG");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        InvestorDetails objInvDet = new InvestorDetails();
                        objInvDet.strIndGroupName = Convert.ToString(sqlReader["VCH_INDUSTRY_NAME"]);
                        objInvDet.IntIndGroupID = Convert.ToInt32(sqlReader["INT_INDUSTRY_GROUP_ID"]);
                        objList.Add(objInvDet);
                    }
                    sqlReader.Close();
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return objList;
        }
        #endregion

        public string InvestorRegistration(InvestorInfo objInvestor, string strAction)
        {
            string strReturnValue = "";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@Action", strAction);
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", objInvestor.INT_INVESTOR_ID);
                cmd.Parameters.AddWithValue("@INT_PARENTID", objInvestor.INT_PARENTID);
                cmd.Parameters.AddWithValue("@VCH_PANNO", objInvestor.VCH_PANNO);
                cmd.Parameters.AddWithValue("@VCH_INV_NAME", objInvestor.VCH_INV_NAME);
                cmd.Parameters.AddWithValue("@INT_COUNTRY", objInvestor.INT_COUNTRY);
                cmd.Parameters.AddWithValue("@VCH_EMAIL", objInvestor.VCH_EMAIL);
                cmd.Parameters.AddWithValue("@INT_SALUTATION", objInvestor.INT_SALUTATION);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_FIRSTNAME", objInvestor.VCH_CONTACT_FIRSTNAME);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_MIDDLENAME", objInvestor.VCH_CONTACT_MIDDLENAME);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_LASTNAME", objInvestor.VCH_CONTACT_LASTNAME);
                cmd.Parameters.AddWithValue("@VCH_OFF_MOBILE", objInvestor.VCH_OFF_MOBILE);
                cmd.Parameters.AddWithValue("@VCH_ADDRESS", objInvestor.VCH_ADDRESS);
                cmd.Parameters.AddWithValue("@VCH_INV_USERID", objInvestor.VCH_INV_USERID);
                cmd.Parameters.AddWithValue("@VCH_INV_PASSWORD", objInvestor.VCH_INV_PASSWORD);
                cmd.Parameters.AddWithValue("@INT_SEC_QUES", objInvestor.INT_SEC_QUES);
                cmd.Parameters.AddWithValue("@VCH_SEC_ANSWER", objInvestor.VCH_SEC_ANSWER);
                cmd.Parameters.AddWithValue("@INT_EMAIL_STATUS", objInvestor.INT_EMAIL_STATUS);
                cmd.Parameters.AddWithValue("@INT_SMS_STATUS", objInvestor.INT_SMS_STATUS);
                cmd.Parameters.AddWithValue("@INT_TERM_STATUS", objInvestor.INT_TERM_STATUS);
                cmd.Parameters.AddWithValue("@VCH_INV_PHOTO", objInvestor.VCH_INV_PHOTO);
                cmd.Parameters.AddWithValue("@INT_CREATED_BY", objInvestor.INT_CREATED_BY);
                cmd.Parameters.AddWithValue("@INT_INDUSTRY_GROUP_ID", objInvestor.INT_INDUSTRY_GROUP_ID);
                cmd.Parameters.AddWithValue("@VCH_INDUSTRY_NAME", objInvestor.VCH_INDUSTRY_NAME);
                cmd.Parameters.AddWithValue("@INT_STATUS", objInvestor.INT_STATUS);
                cmd.Parameters.AddWithValue("@VCH_GSTIN", objInvestor.VCH_GSTIN);
                cmd.Parameters.AddWithValue("@INT_DISTRICT", objInvestor.INT_DISTRICT);
                cmd.Parameters.AddWithValue("@INT_BLOCK", objInvestor.INT_BLOCK);
                cmd.Parameters.AddWithValue("@INT_SECTOR", objInvestor.INT_SECTOR);
                cmd.Parameters.AddWithValue("@INT_SUBSECTOR", objInvestor.INT_SUBSECTOR);
                cmd.Parameters.AddWithValue("@VAR_SITELOCATION", objInvestor.VCH_SITELOCATION);
                cmd.Parameters.AddWithValue("@DEC_INVESTAMOUNT", objInvestor.DEC_INVESTAMOUNT);
                cmd.Parameters.AddWithValue("@INT_CATEGORY", objInvestor.INT_CATEGORY);
                cmd.Parameters.AddWithValue("@VCH_Unique_InvestorId", objInvestor.VCH_UID);
                cmd.Parameters.AddWithValue("@INT_APPROVAL_STATUS", objInvestor.INT_APPROVALSTATUS);
                cmd.Parameters.AddWithValue("@VCH_EIM", objInvestor.VCH_EIN_IEM);
                cmd.Parameters.AddWithValue("@VCH_IND_CODE", objInvestor.VCH_INDUSTRY_CODE);
                cmd.Parameters.AddWithValue("@VCH_LICENCE_NO_TYPE", objInvestor.VCH_LICENCE_NO_TYPE);
                cmd.Parameters.AddWithValue("@VCH_LICENCE_DOC", objInvestor.VCH_LICENCE_DOC);
                cmd.Parameters.AddWithValue("@INT_USER_LEVEL", objInvestor.intUserLevel);
                cmd.Parameters.AddWithValue("@VCH_USER_DESIGNATION", objInvestor.strDesignation);
                cmd.Parameters.AddWithValue("@VCH_PROP_NAME", objInvestor.StrVCH_PROP_NAME);
                cmd.Parameters.AddWithValue("@INT_INDUSTRY_TYPE", objInvestor.INT_INDUSTRY_TYPE); // add by anil sahoo
                cmd.Parameters.AddWithValue("@INT_ENTITY_TYPE", objInvestor.intEntitytype);// add by anil sahoo
                cmd.Parameters.AddWithValue("@VCH_CIN_NUMBER", objInvestor.strCINnumber);// add by anil sahoo

                SqlParameter par1 = new SqlParameter("@VCHMSG_OUT", SqlDbType.VarChar, 100);
                par1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par1);

                cmd.ExecuteNonQuery();

                strReturnValue = Convert.ToString(cmd.Parameters["@VCHMSG_OUT"].Value);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }

            return strReturnValue;
        }
        public string InvApprovalDetails(InvestorDetails objInvestor, string strAction, int InvId, string strRemark)
        {
            List<InvestorDetails> list = new List<InvestorDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd.Connection = conn;
                //conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_APPROVAL";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", InvId);
                cmd.Parameters.AddWithValue("@P_VCH_REMARKS", strRemark);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = (string)cmd.Parameters["@P_OUT_MSG"].Value;


                //cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                //cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                // cmd.ExecuteNonQuery();
                // Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; conn.Close(); }
            return Str_RetValue;
        }
        public string strOTRStatus(InvestorDetails objInvestor, string strAction, string InvId)
        {

            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", strAction);
                cmd.Parameters.AddWithValue("@VCH_INV_USERID", InvId);
                SqlParameter par;
                par = cmd.Parameters.Add("@VCHMSG_OUT", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = (string)cmd.Parameters["@VCHMSG_OUT"].Value;


                //cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                //cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                // cmd.ExecuteNonQuery();
                // Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; conn.Close(); }
            return Str_RetValue;
        }
        public DataTable BindDistrict(string action)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", action);
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
        public DataTable FillBlock(string action, int districtid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", action);
                cmd.Parameters.AddWithValue("@INT_DISTRICT", districtid);
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

        public List<InvestorDetails> ViewInvestorDetailsToInsertInSSO(InvestorDetails objInvestor)
        {
            List<InvestorDetails> list = new List<InvestorDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ACTION", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", objInvestor.intInvId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        InvestorDetails objInv = new InvestorDetails();
                        objInv.strIndName = Convert.ToString(sqlReader["VCH_INV_NAME"]);
                        objInv.strContactFirstName = Convert.ToString(sqlReader["VCH_CONTACT_FIRSTNAME"]);
                        objInv.strContactMiddleName = Convert.ToString(sqlReader["VCH_CONTACT_MIDDLENAME"]);
                        objInv.strContactLastName = Convert.ToString(sqlReader["VCH_CONTACT_LASTNAME"]);
                        objInv.MobNo = Convert.ToString(sqlReader["VCH_OFF_MOBILE"]);
                        objInv.strAddress = Convert.ToString(sqlReader["VCH_ADDRESS"]);
                        objInv.strUserID = Convert.ToString(sqlReader["VCH_INV_USERID"]);
                        objInv.strSecAnswer = Convert.ToString(sqlReader["CONTACTNAME"]);
                        objInv.IntIndGroupID = Convert.ToInt32(sqlReader["INT_INDUSTRY_GROUP_ID"]);
                        objInv.strIndGroupName = Convert.ToString(sqlReader["VCH_INDUSTRY_NAME"]);
                        objInv.IntIndStatus = Convert.ToInt32(sqlReader["INT_STATUS"]);
                        objInv.IntInvestorId = Convert.ToInt32(sqlReader["INT_INVESTOR_ID"]);
                        objInv.StrRemarks = Convert.ToString(sqlReader["VCH_REMARKS"]);
                        objInv.intOTPStatus = Convert.ToInt32(sqlReader["INT_OTP_STATUS"]);
                        objInv.intDistrictId = Convert.ToInt32(sqlReader["INT_DISTRICT"]);
                        objInv.intBlockId = Convert.ToInt32(sqlReader["INT_BLOCK"]);
                        objInv.intSectorId = Convert.ToInt32(sqlReader["INT_SECTOR"]);
                        objInv.intSubSectorId = Convert.ToInt32(sqlReader["INT_SUBSECTOR"]);
                        objInv.intCategoryId = Convert.ToInt32(sqlReader["INT_CATEGORY"]);
                        objInv.decInvstAmount = Convert.ToInt32(sqlReader["DEC_INVESTAMOUNT"]);
                        objInv.strSiteAddress = Convert.ToString(sqlReader["VCH_SITELOCATION"]);
                        objInv.strEmail = Convert.ToString(sqlReader["VCH_EMAIL"]);
                        objInv.strPassword = Convert.ToString(sqlReader["VCH_INV_PASSWORD"]);
                        objInv.strPanno = Convert.ToString(sqlReader["VCHPAN_NO"]);
                        objInv.strfullname = Convert.ToString(sqlReader["CONTACTNAME"]);
                        list.Add(objInv);
                    }
                    sqlReader.Close();
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; sqlReader.Close(); }
            return list;
        }
        public string strUpdateUniueInvId(string strAction, string InvId, string strInvestorUId, string strUnitCode)
        {

            SqlCommand cmd = new SqlCommand();
            try
            {

                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", strAction);
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", InvId);
                cmd.Parameters.AddWithValue("@VCH_Unique_InvestorId", strInvestorUId);
                cmd.Parameters.AddWithValue("@VCH_IND_CODE", strUnitCode);


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlParameter par;
                par = cmd.Parameters.Add("@VCHMSG_OUT", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = (string)cmd.Parameters["@VCHMSG_OUT"].Value;


                //cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                //cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                // cmd.ExecuteNonQuery();
                // Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; conn.Close(); }
            return Str_RetValue;
        }
        public DataTable GetSMSContent(InvestorDetails objprop)
        {
            DataTable dt = new DataTable(); ;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objprop.strAction);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
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
        public DataTable GetInvestorName(InvestorDetails objprop)
        {
            DataTable dt = new DataTable(); ;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_LOGIN";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objprop.strAction);
                //cmd.Parameters.AddWithValue("@P_VCH_EMAIL", objprop.strEmail);
                cmd.Parameters.AddWithValue("@P_VCH_USERID", objprop.strUserID);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
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
        public DataTable GetInvestorLoginDetails(InvestorDetails objprop)
        {
            DataTable dt = new DataTable(); ;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_APPROVAL";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objprop.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objprop.IntInvestorId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
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

        public DataTable PanCheckValidation(string strPanNo, string strAction)
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
                cmd.CommandText = "USP_CHECK_PAN_REGD";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_VCH_ACTION", strAction);
                cmd.Parameters.AddWithValue("@P_VCH_PAN", strPanNo);

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
        public DataTable CheckSecondLevelUser(InvestorInfo objInvEntity)
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
                cmd.CommandText = "USP_CHECK_PAN_REGD";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objInvEntity.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objInvEntity.INT_INVESTOR_ID);

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

        public string InvestorRegistrationNew(InvestorInfo objInvestor, string strAction)
        {
            string result = "";

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS_NEW";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", strAction);
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", objInvestor.INT_INVESTOR_ID);
                cmd.Parameters.AddWithValue("@VCH_INV_NAME", objInvestor.VCH_INV_NAME);
                cmd.Parameters.AddWithValue("@VCH_PANNO", objInvestor.VCH_PANNO);
                cmd.Parameters.AddWithValue("@INT_COUNTRY", objInvestor.INT_COUNTRY);
                cmd.Parameters.AddWithValue("@VCH_EMAIL", objInvestor.VCH_EMAIL);
                cmd.Parameters.AddWithValue("@INT_PARENTID", objInvestor.INT_PARENTID);
                cmd.Parameters.AddWithValue("@INT_SALUTATION", objInvestor.INT_SALUTATION);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_FIRSTNAME", objInvestor.VCH_CONTACT_FIRSTNAME);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_MIDDLENAME", objInvestor.VCH_CONTACT_MIDDLENAME);
                cmd.Parameters.AddWithValue("@VCH_CONTACT_LASTNAME", objInvestor.VCH_CONTACT_LASTNAME);
                cmd.Parameters.AddWithValue("@VCH_OFF_MOBILE", objInvestor.VCH_OFF_MOBILE);
                cmd.Parameters.AddWithValue("@VCH_ADDRESS", objInvestor.VCH_ADDRESS);
                cmd.Parameters.AddWithValue("@VCH_INV_USERID", objInvestor.VCH_INV_USERID);
                cmd.Parameters.AddWithValue("@VCH_EIM", objInvestor.VCH_EIN_IEM);
                cmd.Parameters.AddWithValue("@INT_APPROVAL_STATUS", objInvestor.INT_APPROVALSTATUS);
                cmd.Parameters.AddWithValue("@VCH_INV_PASSWORD", objInvestor.VCH_INV_PASSWORD);
                cmd.Parameters.AddWithValue("@INT_SEC_QUES", objInvestor.INT_SEC_QUES);
                cmd.Parameters.AddWithValue("@VCH_SEC_ANSWER", objInvestor.VCH_SEC_ANSWER);
                cmd.Parameters.AddWithValue("@INT_EMAIL_STATUS", objInvestor.INT_EMAIL_STATUS);
                cmd.Parameters.AddWithValue("@INT_SMS_STATUS", objInvestor.INT_SMS_STATUS);
                cmd.Parameters.AddWithValue("@INT_TERM_STATUS", objInvestor.INT_TERM_STATUS);
                cmd.Parameters.AddWithValue("@VCH_INV_PHOTO", objInvestor.VCH_INV_PHOTO);
                cmd.Parameters.AddWithValue("@INT_CREATED_BY", objInvestor.INT_CREATED_BY);
                cmd.Parameters.AddWithValue("@INT_INDUSTRY_GROUP_ID", objInvestor.INT_INDUSTRY_GROUP_ID);
                cmd.Parameters.AddWithValue("@VCH_INDUSTRY_NAME", objInvestor.VCH_INDUSTRY_NAME);
                cmd.Parameters.AddWithValue("@INT_STATUS", objInvestor.INT_STATUS);
                cmd.Parameters.AddWithValue("@VCH_GSTIN", objInvestor.VCH_GSTIN);
                cmd.Parameters.AddWithValue("@INT_DISTRICT", objInvestor.INT_DISTRICT);
                cmd.Parameters.AddWithValue("@INT_BLOCK", objInvestor.INT_BLOCK);
                cmd.Parameters.AddWithValue("@INT_SECTOR", objInvestor.INT_SECTOR);
                cmd.Parameters.AddWithValue("@INT_SUBSECTOR", objInvestor.INT_SUBSECTOR);
                cmd.Parameters.AddWithValue("@VAR_SITELOCATION", objInvestor.VCH_SITELOCATION);
                cmd.Parameters.AddWithValue("@DEC_INVESTAMOUNT", objInvestor.DEC_INVESTAMOUNT);
                cmd.Parameters.AddWithValue("@INT_CATEGORY", objInvestor.INT_CATEGORY);
                cmd.Parameters.AddWithValue("@VCH_Unique_InvestorId", objInvestor.VCH_UID);
                cmd.Parameters.AddWithValue("@VCH_IND_CODE", objInvestor.VCH_INDUSTRY_CODE);
                cmd.ExecuteNonQuery();
                result = "1";
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


            return result;

        }
        public DataTable ApprovalDetails(InvestorInfo objInvestor, string strAction)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS_View";
                cmd.Parameters.AddWithValue("@Action", strAction);
                cmd.Parameters.AddWithValue("@int_parent_id", objInvestor.INT_INVESTOR_ID);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null; conn.Close();
            }
            return dt;
        }
        public int InvestorStatus(InvestorInfo objInvestor)
        {
            return 1;
        }

        #region Added by Sushant Jena

        public DataTable UserManagementView(InvestorDetails objEntity)
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
                cmd.CommandText = "USP_INV_USER_MANAGEMENT_VIEW";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objEntity.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objEntity.IntInvestorId);
                cmd.Parameters.AddWithValue("@P_INT_PARENT_ID", objEntity.IntParentId);
                cmd.Parameters.AddWithValue("@P_VCH_PAN", objEntity.strPanno);
                cmd.Parameters.AddWithValue("@P_VCH_INV_NAME", objEntity.strIndName);
                cmd.Parameters.AddWithValue("@P_VCH_INV_USERID", objEntity.strUserID);

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
        public string UserManagementAED(InvestorDetails objEntity)
        {
            SqlCommand cmd = new SqlCommand();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INV_USER_MANAGEMENT_AED";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objEntity.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objEntity.IntInvestorId);
                cmd.Parameters.AddWithValue("@P_VCH_READ_USER_PERMISSION", objEntity.strReadUserPermission);
                cmd.Parameters.AddWithValue("@P_VCH_WRITE_USER_PERMISSION", objEntity.strWriteUserPermission);
                cmd.Parameters.AddWithValue("@P_VCH_REJECTION_CAUSE", objEntity.strRejectionCause);
                cmd.Parameters.AddWithValue("@P_INT_CREATED_BY", objEntity.IntCreatedBy);
                cmd.Parameters.AddWithValue("@P_VCH_UNIQUE_ID", objEntity.strUniqueId);
                cmd.Parameters.AddWithValue("@P_VCH_LICENCE_DOC", objEntity.strLicenceDoc);
                cmd.Parameters.AddWithValue("@P_VCH_APPROVALREMARK", objEntity.strApprovalRemarks);

                SqlParameter par1 = new SqlParameter("@P_VCH_OUT_MSG", SqlDbType.VarChar, 100);
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

        public string checkEmailAvail(InvestorInfo objInvestor)
        {
            string strReturnStatus = "";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@VCH_EMAIL", objInvestor.VCH_EMAIL);
                cmd.Parameters.AddWithValue("@Action", objInvestor.strAction);

                SqlParameter par1 = new SqlParameter("@VCHMSG_OUT", SqlDbType.VarChar, 100);
                par1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par1);

                cmd.ExecuteNonQuery();
                strReturnStatus = cmd.Parameters["@VCHMSG_OUT"].Value.ToString();

                return strReturnStatus;
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

        public string getUniqueId(InvestorInfo objInvestor)
        {
            string strReturnStatus = "";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@Action", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", objInvestor.INT_INVESTOR_ID);

                SqlParameter par1 = new SqlParameter("@VCHMSG_OUT", SqlDbType.VarChar, 100);
                par1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par1);

                cmd.ExecuteNonQuery();
                strReturnStatus = cmd.Parameters["@VCHMSG_OUT"].Value.ToString();

                return strReturnStatus;
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
        public string updateUniqueId(InvestorInfo objInvestor)
        {
            string strReturnStatus = "";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@Action", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@VCH_INV_USERID", objInvestor.VCH_INV_USERID);
                cmd.Parameters.AddWithValue("@VCH_UNIQUE_INVESTORID", objInvestor.strUniqueId);

                SqlParameter par1 = new SqlParameter("@VCHMSG_OUT", SqlDbType.VarChar, 100);
                par1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par1);

                cmd.ExecuteNonQuery();
                strReturnStatus = cmd.Parameters["@VCHMSG_OUT"].Value.ToString();

                return strReturnStatus;
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
        public DataTable viewDraftedUsers(InvestorInfo objInvestor)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS_View";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@Action", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objInvestor.INT_INVESTOR_ID);

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
                conn.Dispose();
            }
            return dt;
        }
        public DataTable viewUnitDetails(InvestorInfo objInvestor)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS_View";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@Action", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objInvestor.INT_INVESTOR_ID);

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

        public DataTable fillUserList2ndLevel(InvestorInfo objInvestor)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS_View";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@Action", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objInvestor.INT_INVESTOR_ID);

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

        public string ApplicationPermissionAED(InvestorInfo objInvestor)
        {
            string strReturnStatus = "";

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@Action", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", objInvestor.INT_INVESTOR_ID);
                cmd.Parameters.AddWithValue("@VCH_APP_IDS", objInvestor.strAppIds);
                cmd.Parameters.AddWithValue("@INT_CREATED_BY", objInvestor.INT_CREATED_BY);

                SqlParameter par1 = new SqlParameter("@VCHMSG_OUT", SqlDbType.VarChar, 100);
                par1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par1);

                cmd.ExecuteNonQuery();
                strReturnStatus = cmd.Parameters["@VCHMSG_OUT"].Value.ToString();

                return strReturnStatus;
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
        public DataTable ApplicationPermissionView(InvestorInfo objInvestor)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS_View";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@Action", objInvestor.strAction);
                cmd.Parameters.AddWithValue("@P_INT_INVESTOR_ID", objInvestor.INT_INVESTOR_ID);

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


        public DataTable BindEntityType(string action)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", action);
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }
           
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            finally
            {
                cmd = null;
            }
            return dt;
        }


        public DataTable BindRegdCountry(string action)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", action);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();
            }

            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            finally
            {
                cmd = null;
            }
            return dt;
        }

        public DataTable BindRegdState(string action, int countryid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_INVESTOR_REGISTRATION_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", action);
                cmd.Parameters.AddWithValue("@INT_COUNTRYID", countryid);              
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

        #endregion
    }
}
