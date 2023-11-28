using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;


using System.Data;

/// <summary>
/// Summary description for HelpDeskDataLayer
/// </summary>
/// 
namespace DataAcessLayer.HelpDeskDataLayer
{
    public class HelpDeskDataLayer
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        string str_Retvalue = "";
        public List<IssueRegistration> BindCategory(IssueRegistration objDdl)
        {
            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PintType", objDdl.intTypeId);
                cmd.Parameters.AddWithValue("@PAction", objDdl.Action);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                IssueRegistration objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new IssueRegistration();
                        objInner.int_CategoryId = Convert.ToInt32(sqlReader["catid"]);
                        objInner.vch_CategoryName = Convert.ToString(sqlReader["catName"]);

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
        public List<IssueRegistration> BindType(IssueRegistration objDdl)
        {
            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objDdl.Action);
                cmd.Parameters.AddWithValue("@PintType", objDdl.intTypeId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                IssueRegistration objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new IssueRegistration();
                        objInner.intTypeId = Convert.ToInt32(sqlReader["Int_IssueType"]);
                        objInner.strTypeName = Convert.ToString(sqlReader["Int_IssueName"]);

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
        public List<IssueRegistration> BindSubCategory(IssueRegistration objDdl)
        {
            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objDdl.Action);
                cmd.Parameters.AddWithValue("@PintCategoryId", objDdl.int_CategoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                IssueRegistration objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new IssueRegistration();
                        objInner.int_SubcategoryId = Convert.ToInt32(sqlReader["int_SubcategoryId"]);
                        objInner.vch_SubCategoryName = Convert.ToString(sqlReader["vch_SubcategoryName"]);

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
        public List<IssueRegistration> BindDepartment(IssueRegistration objDdl)
        {
            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objDdl.Action);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                IssueRegistration objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new IssueRegistration();
                        objInner.int_DeptId = Convert.ToInt32(sqlReader["intLevelDetailId"]);
                        objInner.vch_deptName = Convert.ToString(sqlReader["nvchLevelName"]);

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
        public List<IssueRegistration> BindUser(IssueRegistration objDdl)
        {
            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objDdl.Action);
                cmd.Parameters.AddWithValue("@intLevelDetailId", objDdl.int_DeptId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                IssueRegistration objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new IssueRegistration();
                        objInner.int_UserId = Convert.ToInt32(sqlReader["intUserId"]);
                        objInner.vch_UserName = Convert.ToString(sqlReader["vchUserName"]);

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
        public List<IssueRegistration> BindInvestor(IssueRegistration objDdl)
        {
            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objDdl.Action);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                IssueRegistration objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new IssueRegistration();
                       // objInner.int_investId = Convert.ToInt32(sqlReader["INT_INVESTOR_ID"]);
                        objInner.vch_investName = Convert.ToString(sqlReader["VCH_INV_NAME"]);
                        objInner.vchInveUserId = Convert.ToString(sqlReader["VCH_INV_USERID"]); // add by anil

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
        public List<IssueRegistration> AddIssueRegister(IssueRegistration objMasterSector)
        {
            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            // con.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objMasterSector.Action);
                cmd.Parameters.AddWithValue("@PvchType", objMasterSector.vch_Type);
                cmd.Parameters.AddWithValue("@PintCategoryId", objMasterSector.int_CategoryId);
                cmd.Parameters.AddWithValue("@PintSubCategoryId", objMasterSector.int_SubcategoryId);
                cmd.Parameters.AddWithValue("@PintDepartmentId", objMasterSector.int_DeptId);
                cmd.Parameters.AddWithValue("@PintUserId", objMasterSector.int_UserId);
               // cmd.Parameters.AddWithValue("@PINT_INVESTOR_ID", objMasterSector.int_investId);
                cmd.Parameters.AddWithValue("@p_OName", objMasterSector.OtherName);
                cmd.Parameters.AddWithValue("@p_VchADDRESS", objMasterSector.Address);
                cmd.Parameters.AddWithValue("@p_VchMobileno", objMasterSector.VchMobile);
                cmd.Parameters.AddWithValue("@p_Email", objMasterSector.Email);
                cmd.Parameters.AddWithValue("@PvchIssueDetails", objMasterSector.vch_IssueDetails);
                cmd.Parameters.AddWithValue("@PvchIssueSources", objMasterSector.vch_IssueSource);
                cmd.Parameters.AddWithValue("@PvchfileUpload", objMasterSector.vch_FIleUpload);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objMasterSector.intCreatedBy);
                cmd.Parameters.AddWithValue("@intUpdatedBy", objMasterSector.intUpdatedBy);
                cmd.Parameters.AddWithValue("@P_intstatus", objMasterSector.status);
                cmd.Parameters.AddWithValue("@PintIssueId", objMasterSector.int_IssueId);
                cmd.Parameters.AddWithValue("@Pvch_Remark", objMasterSector.strRemark);
                cmd.Parameters.AddWithValue("@Pvch_serviceName", objMasterSector.ServiceName);
                cmd.Parameters.AddWithValue("@Pvch_Priority", objMasterSector.vch_Priority);
                cmd.Parameters.AddWithValue("@P_vch_fileUploadpopup", objMasterSector.vch_fileUploadpopup);
                cmd.Parameters.AddWithValue("@PvchInveUserId", objMasterSector.vchInveUserId);  /// add by anil

                //cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                //   cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                IssueRegistration objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new IssueRegistration();
                        objInner.int_UserId = Convert.ToInt32(sqlReader["Int_AuthorityId"]);
                        objInner.vch_UserName = Convert.ToString(sqlReader["USERNAME"]);
                        objInner.Email = Convert.ToString(sqlReader["VCH_EMAILID"]);
                        objInner.VchMobile = Convert.ToString(sqlReader["VCH_MOBILENO"]);
                        objInner.vchMobileContent = Convert.ToString(sqlReader["VCH_PHONE_CONTENT"]);
                        objInner.vchEmailContent = Convert.ToString(sqlReader["VCH_EMAIL_CONTENT"]);
                        objInner.vchUserEmail = Convert.ToString(sqlReader["USEREMAIL"]);
                        objInner.vchUserMobileNo = Convert.ToString(sqlReader["USERMOBILE"]);
                        objInner.VCHATAMOBILENO = Convert.ToString(sqlReader["ATAMOBILENO"]);
                        objInner.VCHATAEMAIL = Convert.ToString(sqlReader["ATAEMAIL"]);
                        objInner.vchIssueNo = Convert.ToString(sqlReader["ISSUENO"]);
                        objInner.vch_IssueDetails = Convert.ToString(sqlReader["VCHDESCRIPTION"]);
                        list.Add(objInner);
                    }

                }
                sqlReader.Close();
                //str_Retvalue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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
            return list;
        }
        public List<IssueRegistration> ViewIssueRegistration(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                //cmd.Parameters.AddWithValue("@PintDepartmentId", obj.int_DeptId);
                cmd.Parameters.AddWithValue("@P_intstatus", obj.status);
                cmd.Parameters.AddWithValue("@PvchType", obj.vch_Type);
                cmd.Parameters.AddWithValue("@Pvch_IssueNo", obj.vchIssueNo);
                cmd.Parameters.AddWithValue("@P_VCH_FROMDATE", obj.vchFromDate);
                cmd.Parameters.AddWithValue("@P_VCH_TODATE", obj.vchToDate);
                cmd.Parameters.AddWithValue("@PintCategoryId", obj.int_CategoryId);
                cmd.Parameters.AddWithValue("@PintSubCategoryId", obj.int_SubcategoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.vchIssueNo = sqlReader["issueNo"].ToString();
                        objService.vch_UserName = sqlReader["vchUserName"].ToString();
                        objService.vch_Type = sqlReader["Type"].ToString();
                        objService.dtmCreatedOn = sqlReader["dtmCreatedOn"].ToString();
                        objService.status = Convert.ToInt16(sqlReader["vchStatusName"].ToString());
                        objService.int_IssueId = Convert.ToInt16(sqlReader["issueid"].ToString());
                        objService.vch_IssueDetails = sqlReader["vch_IssueDetails"].ToString();
                        objService.vch_FIleUpload = sqlReader["vch_FIleUpload"].ToString();
                        objService.vch_Priority = sqlReader["vch_Priority"].ToString();
                        objService.VchMobile = sqlReader["vch_MobileNo"].ToString();
                        objService.Email = sqlReader["VCH_EMAIL"].ToString();
                        objService.Address = sqlReader["vch_Address"].ToString();
                        objService.int_CategoryId = Convert.ToInt32(sqlReader["int_CategoryId"].ToString());
                        objService.int_SubcategoryId = Convert.ToInt32(sqlReader["int_SubcategoryId"].ToString());
                        objService.CategoryName = sqlReader["vch_CategoryName"].ToString();
                        objService.SubCategory = sqlReader["vch_SubcategoryName"].ToString();
                        objService.OtherName = sqlReader["dtmUpdatedOn"].ToString();
                        // objService.intServiceId = Convert.ToInt32(sqlReader["intServiceId"].ToString());
                        //objService.SMType = sqlReader["VCH_TYPE"].ToString();
                        //objService.SMMAILContent = sqlReader["VCH_EMAIL_CONT1"].ToString();
                        //objService.SMSMSContent = sqlReader["VCH_SMS_CONT1"].ToString();
                        //objService.SMAppStatus = sqlReader["Appstatus"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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
        public List<IssueRegistration> ViewFile(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.UserManual = sqlReader["vchUserManual"].ToString();
                        objService.ServiceId = Convert.ToInt32(sqlReader["intServiceId"].ToString());
                        objService.ServiceName = sqlReader["VCH_SERVICENAME"].ToString();

                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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
        public List<IssueRegistration> HelpdeskCategory_Report(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_HELPDESK_Report";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_FROMDATE",obj.vchFromDate);
                cmd.Parameters.AddWithValue("@P_VCH_TODATE", obj.vchToDate);
                cmd.Parameters.AddWithValue("@P_Action", obj.Action);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.int_CategoryId = Convert.ToInt32(sqlReader["int_CategoryId"].ToString());
                        objService.CategoryName = sqlReader["vch_CategoryName"].ToString();
                        objService.Total_Recieved = sqlReader["TotalReceived"].ToString();
                        objService.Applied = sqlReader["Applied"].ToString();
                        objService.Approved = sqlReader["Approved"].ToString();
                        objService.Pending = sqlReader["Pending"].ToString();
                        objService.Discard = sqlReader["Discard"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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
        public List<IssueRegistration> HelpdeskSubCategory_Report(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_HELPDESK_Report";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_Action", obj.Action);
                cmd.Parameters.AddWithValue("@P_intCategoryId", obj.int_CategoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.int_CategoryId = Convert.ToInt32(sqlReader["int_CategoryId"].ToString());
                        objService.CategoryName = sqlReader["vch_CategoryName"].ToString();
                        objService.int_SubcategoryId = Convert.ToInt32(sqlReader["int_SubcategoryId"].ToString());
                        objService.SubCategory = sqlReader["vch_SubcategoryName"].ToString();
                        objService.Total_Recieved = sqlReader["TotalReceived"].ToString();
                        objService.Applied = sqlReader["Applied"].ToString();
                        objService.Approved = sqlReader["Approved"].ToString();
                        objService.Pending = sqlReader["Pending"].ToString();
                        objService.Discard = sqlReader["Discard"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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
        public List<IssueRegistration> Helpdesk_Report(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_HELPDESK_Report";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_Action", obj.Action);
                cmd.Parameters.AddWithValue("@P_intCategoryId", obj.int_CategoryId);
                cmd.Parameters.AddWithValue("@P_IntSubCategoryId", obj.int_SubcategoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.int_IssueId = Convert.ToInt32(sqlReader["int_IssueId"].ToString());
                        objService.int_CategoryId = Convert.ToInt32(sqlReader["int_CategoryId"].ToString());
                        objService.CategoryName = sqlReader["vch_CategoryName"].ToString();
                        objService.int_SubcategoryId = Convert.ToInt32(sqlReader["int_SubcategoryId"].ToString());
                        objService.SubCategory = sqlReader["vch_SubcategoryName"].ToString();
                        objService.vchIssueNo = sqlReader["vch_IssueNo"].ToString();
                        objService.Email = sqlReader["VCH_EMAIL"].ToString();
                        objService.vch_UserName = sqlReader["vchusername"].ToString();
                        objService.dtmCreatedOn = sqlReader["date"].ToString();
                        objService.VCH_STATUS = sqlReader["STATUS"].ToString();
                        objService.vch_Type = sqlReader["vch_Type"].ToString();
                        objService.VchMobile = sqlReader["vch_MobileNo"].ToString();
                        objService.vch_FIleUpload = sqlReader["vch_FIleUpload"].ToString();
                        objService.Address = sqlReader["vch_Address"].ToString();
                        objService.vch_IssueDetails = sqlReader["vch_IssueDetails"].ToString();
                        objService.vch_Priority = sqlReader["vch_Priority"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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
        public List<IssueRegistration> Helpdesk_Status_Report(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_HELPDESK_Report";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_Action", obj.Action);
                cmd.Parameters.AddWithValue("@P_intCategoryId", obj.int_CategoryId);
                //cmd.Parameters.AddWithValue("@P_IntSubCategoryId", obj.int_SubcategoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.int_CategoryId = Convert.ToInt32(sqlReader["int_CategoryId"].ToString());
                        objService.CategoryName = sqlReader["vch_CategoryName"].ToString();
                        objService.int_SubcategoryId = Convert.ToInt32(sqlReader["int_SubcategoryId"].ToString());
                        objService.SubCategory = sqlReader["vch_SubcategoryName"].ToString();
                        objService.vchIssueNo = sqlReader["vch_IssueNo"].ToString();
                        objService.Email = sqlReader["VCH_EMAIL"].ToString();
                        objService.vch_UserName = sqlReader["vchusername"].ToString();
                        objService.dtmCreatedOn = sqlReader["date"].ToString();
                        objService.VCH_STATUS = sqlReader["STATUS"].ToString();
                        objService.vch_Type = sqlReader["vch_Type"].ToString();
                        objService.VchMobile = sqlReader["vch_MobileNo"].ToString();
                        objService.vch_FIleUpload = sqlReader["vch_FIleUpload"].ToString();
                        objService.Address = sqlReader["vch_Address"].ToString();
                        objService.vch_IssueDetails = sqlReader["vch_IssueDetails"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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
        public string AddIssueCategory(IssueRegistration objMaster)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueCategory";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objMaster.Action);
                cmd.Parameters.AddWithValue("@P_vch_type", objMaster.vch_Type);
                cmd.Parameters.AddWithValue("@P_int_Category_Id", objMaster.int_CategoryId);
                cmd.Parameters.AddWithValue("@P_vch_Category", objMaster.vch_CategoryName);

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
        public List<IssueRegistration> ViewIssueCategory(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueCategory";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                cmd.Parameters.AddWithValue("@P_int_Category_Id", obj.int_CategoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.int_CategoryId = Convert.ToInt32(sqlReader["int_CategoryId"].ToString());
                        objService.intTypeId = Convert.ToInt32(sqlReader["int_Type"].ToString());
                        objService.vch_CategoryName = sqlReader["vch_CategoryName"].ToString();
                        objService.strTypeName = sqlReader["Int_IssueName"].ToString();
                        objService.dtmCreatedOn = sqlReader["Date"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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
        public string AddIssueSubCategory(IssueRegistration objMaster)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueSubCategory";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objMaster.Action);

                cmd.Parameters.AddWithValue("@P_int_Category_Id", objMaster.int_CategoryId);
                cmd.Parameters.AddWithValue("@P_vch_SubCategory", objMaster.vch_SubCategoryName);
                cmd.Parameters.AddWithValue("@P_int_SubCategory_Id", objMaster.int_SubcategoryId);
                cmd.Parameters.AddWithValue("@int_EscLevel", objMaster.int_EscLevel);
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


        public List<IssueRegistration> ViewIssueSubCategory(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueSubCategory";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                cmd.Parameters.AddWithValue("@P_int_SubCategory_Id", obj.int_SubcategoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.int_CategoryId = Convert.ToInt32(sqlReader["int_CategoryId"].ToString());
                        objService.int_SubcategoryId = Convert.ToInt32(sqlReader["int_SubcategoryId"].ToString());
                        objService.intTypeId = Convert.ToInt32(sqlReader["Int_IssueType"].ToString());
                        objService.vch_CategoryName = sqlReader["vch_CategoryName"].ToString();
                        objService.vch_SubCategoryName = sqlReader["vch_SubcategoryName"].ToString();
                        objService.int_EscLevel = Convert.ToInt32(sqlReader["int_Escalation_Level"].ToString());
                        objService.dtmCreatedOn = sqlReader["Date"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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

        public List<IssueRegistration> ViewIssueintimation(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                cmd.Parameters.AddWithValue("@PintIssueId", obj.int_IssueId);

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.strRemark = sqlReader["vch_Remark"].ToString();
                        objService.vch_FIleUpload = sqlReader["vch_FIleUpload"].ToString();
                        objService.intCreatedBy = Convert.ToInt16(sqlReader["intCreatedBy"].ToString());
                        objService.dtmCreatedOn = sqlReader["dtmCreatedOn"].ToString();
                        objService.VCH_STATUS = sqlReader["Status"].ToString();
                        objService.int_IssueId = Convert.ToInt16(sqlReader["issueid"].ToString());
                        objService.vch_UserName = sqlReader["vchFullName"].ToString();

                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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

        public List<IssueRegistration> FillAuthority(IssueRegistration obj)
        {
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            List<IssueRegistration> lstAuth = new List<IssueRegistration>();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueConfiguration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.int_UserId = Convert.ToInt16(sqlReader["intUserId"].ToString());
                        objService.vch_UserName = sqlReader["vchFullName"].ToString();
                        lstAuth.Add(objService);
                    }
                }
                sqlReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }
            return lstAuth;
        }
        public string AddHDEscalationConfiguration(IssueRegistration obj)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueConfiguration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                cmd.Parameters.AddWithValue("@INT_CATEGORY_ID", obj.int_CategoryId);
                cmd.Parameters.AddWithValue("@INT_ESCALATION_LEVELID", obj.intLevelid);
                cmd.Parameters.AddWithValue("@INT_SUB_CATEGORY_ID ", obj.int_SubcategoryId);
                cmd.Parameters.AddWithValue("@VCH_STANDARD_DAYS", obj.VCH_STANDARD_DAYS);
                cmd.Parameters.AddWithValue("@Int_AuthorityId", obj.int_UserId);
                cmd.Parameters.AddWithValue("@INT_UPDATED_BY", obj.intUpdatedBy);
                cmd.Parameters.AddWithValue("@VCH_PHONENO", obj.VchMobile);
                cmd.Parameters.AddWithValue("@VCH_EMILID", obj.Email);
                cmd.Parameters.AddWithValue("@VCH_PHONECONTENT", obj.vchMobileContent);
                cmd.Parameters.AddWithValue("@VCH_EMAILCONTENT", obj.vchEmailContent);
                cmd.Parameters.AddWithValue("@P_STRXML", obj.strXmlHelpDesk);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                object objOutput = new object();
                objOutput = cmd.Parameters["@P_OUT_MSG"].Value;
                if (objOutput != null && objOutput != DBNull.Value)
                {
                    str_Retvalue = objOutput.ToString();
                }
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
        public int CountEscalationLevel(IssueRegistration objMaster)
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
                cmd.CommandText = "USP_IssueConfiguration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objMaster.Action);
                cmd.Parameters.AddWithValue("@INT_SUB_CATEGORY_ID", objMaster.int_SubcategoryId);
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
            return Convert.ToInt32(str_Retvalue);
        }
        public List<IssueRegistration> ViewConfigEscalation(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueConfiguration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                cmd.Parameters.AddWithValue("@int_type_id", obj.intTypeId);
                cmd.Parameters.AddWithValue("@INT_CATEGORY_ID", obj.int_CategoryId);
                cmd.Parameters.AddWithValue("@INT_SUB_CATEGORY_ID", obj.int_SubcategoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();

                        objService.vch_CategoryName = sqlReader["vch_CategoryName"].ToString();
                        objService.vch_SubCategoryName = sqlReader["vch_SubcategoryName"].ToString();
                        objService.int_EscLevel = Convert.ToInt32(sqlReader["int_Escalation_Level"].ToString());
                        objService.int_SubcategoryId = Convert.ToInt32(sqlReader["int_SubcategoryId"]);
                        objService.intEscConfigId = Convert.ToInt32(sqlReader["INT_CONFIG_ID"]);
                        //objService.I = sqlReader["vch_Type"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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
        public List<IssueRegistration> EditViewConfigEscalation(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueConfiguration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                cmd.Parameters.AddWithValue("@int_type_id", obj.intTypeId);
                cmd.Parameters.AddWithValue("@INT_CATEGORY_ID", obj.int_CategoryId);
                cmd.Parameters.AddWithValue("@INT_SUB_CATEGORY_ID", obj.int_SubcategoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.vch_CategoryName = sqlReader["vch_CategoryName"].ToString();
                        objService.vch_SubCategoryName = sqlReader["vch_SubcategoryName"].ToString();
                        objService.int_EscLevel = Convert.ToInt32(sqlReader["int_Escalation_Level"].ToString());
                        objService.int_SubcategoryId = Convert.ToInt32(sqlReader["int_SubcategoryId"]);
                        objService.intEscConfigId = Convert.ToInt32(sqlReader["INT_CONFIG_ID"]);
                        objService.intTypeId = Convert.ToInt32(sqlReader["Int_IssueType"]);
                        if (string.Equals("ee", obj.Action, StringComparison.OrdinalIgnoreCase))
                        {
                            objService.int_CategoryId = Convert.ToInt32(sqlReader["int_CategoryId"]);
                            objService.VCH_STANDARD_DAYS = sqlReader["VCH_STANDARD_DAYS"].ToString();
                            objService.VchMobile = sqlReader["VCH_MOBILENO"].ToString();
                            objService.Email = sqlReader["VCH_EMAILID"].ToString();
                            objService.vchMobileContent = sqlReader["VCH_PHONE_CONTENT"].ToString();
                            objService.vchEmailContent = sqlReader["VCH_EMAIL_CONTENT"].ToString();
                            objService.intLevelid = Convert.ToInt32(sqlReader["INT_ESCALATION_LEVELID"]);
                            objService.int_UserId = Convert.ToInt32(sqlReader["Int_AuthorityId"]);
                        }
                        //objService.I = sqlReader["vch_Type"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd = null;
            }
            return list;
        }
        public List<IssueRegistration> AutoEscalationProcess(IssueRegistration objMaster)
        {
            List<IssueRegistration> rtnList = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueConfiguration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objMaster.Action);
                //cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                //cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                // cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objIssue = new IssueRegistration();
                        objIssue.int_IssueId = Convert.ToInt32(sqlReader["ISSUEID"].ToString());
                        objIssue.Email = sqlReader["EMAIL"].ToString();
                        objIssue.VchMobile = sqlReader["MOBILENO"].ToString();
                        objIssue.vchEmailContent = sqlReader["EMAILCONTNT"].ToString();
                        objIssue.vchMobileContent = sqlReader["MOBILECONTNT"].ToString();
                        objIssue.vchUserEmail = sqlReader["USERMAIL"].ToString();
                        objIssue.vchUserMobileNo = sqlReader["USERMOBILE"].ToString();
                        objIssue.vchIssueNo = sqlReader["vchIssueNo"].ToString();
                        objIssue.CategoryName = sqlReader["CATEGORYNAME"].ToString();
                        objIssue.vch_IssueDetails = sqlReader["VCHDESCRIPTION"].ToString();
                        objIssue.vch_investName = sqlReader["IVESTORNAME"].ToString();
                        //objService.I = sqlReader["vch_Type"].ToString();
                        rtnList.Add(objIssue);
                    }
                }
                sqlReader.Close();
                //str_Retvalue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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
            return rtnList;
        }

        public List<IssueRegistration> ViewpopConfigEscalation(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueConfiguration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                cmd.Parameters.AddWithValue("@INT_SUB_CATEGORY_ID", obj.int_SubcategoryId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.intEscConfigId = Convert.ToInt32(sqlReader["INT_CONFIG_ID"].ToString());
                        objService.vch_CategoryName = sqlReader["vch_CategoryName"].ToString();
                        objService.vch_SubCategoryName = sqlReader["vch_SubcategoryName"].ToString();
                        objService.VCH_STANDARD_DAYS = sqlReader["VCH_STANDARD_DAYS"].ToString();
                        objService.int_SubcategoryId = Convert.ToInt32(sqlReader["int_SubcategoryId"]);
                        objService.vch_UserName = sqlReader["vchFullName"].ToString();

                        /*added by Ritika lath on 8th May 2018*/
                        if (string.Equals("po", obj.Action, StringComparison.OrdinalIgnoreCase))
                        {
                            objService.VchMobile = sqlReader["VCH_MOBILENO"].ToString();
                            objService.Email = sqlReader["VCH_EMAILID"].ToString();
                            objService.vchMobileContent = sqlReader["VCH_PHONE_CONTENT"].ToString();
                            objService.vchEmailContent = sqlReader["VCH_EMAIL_CONTENT"].ToString();
                        }

                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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
        public List<IssueRegistration> ViewEscalationEmailRegistration(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                cmd.Parameters.AddWithValue("@PintSubCategoryId", obj.int_SubcategoryId);
                cmd.Parameters.AddWithValue("@PintIssueId", obj.int_IssueId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        IssueRegistration objService = new IssueRegistration();
                        objService.VchMobile = sqlReader["VCH_MOBILENO"].ToString();
                        objService.Email = sqlReader["VCH_EMAILID"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
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
        public List<IssueRegistration> ViewIssueRegistrationMIS(IssueRegistration obj)
        {

            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueCategory_MIS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", obj.Action);
                cmd.Parameters.AddWithValue("@PCategoryId", obj.int_CategoryId);
                cmd.Parameters.AddWithValue("@P_VCH_FROMDATE", obj.vchFromDate);
                cmd.Parameters.AddWithValue("@P_VCH_TODATE", obj.vchToDate);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (obj.Action == "V")
                        {
                            IssueRegistration objService = new IssueRegistration();
                            objService.int_CategoryId = Convert.ToInt32(sqlReader["int_categoryid"].ToString());
                            objService.CategoryName = Convert.ToString(sqlReader["vch_categoryName"].ToString());
                            objService.Total_Count = Convert.ToString(sqlReader["TotalCount"].ToString());
                            objService.Total_Resolved = Convert.ToString(sqlReader["TotalResolved"].ToString());
                            objService.Total_Pending = Convert.ToString(sqlReader["TotalPending"].ToString());
                            objService.Total_IssueResolvedSLA = Convert.ToString(sqlReader["IssueResolvedSLA"].ToString());
                            objService.Total_IssueOpenPastSLA = Convert.ToString(sqlReader["IssueOpenPastSLA"].ToString());
                            objService.int_SubcategoryId = Convert.ToInt32(sqlReader["int_SubcategoryId"].ToString());
                            objService.vch_SubCategoryName = Convert.ToString(sqlReader["vch_SubcategoryName"].ToString());
                            objService.strTypeName = Convert.ToString(sqlReader["Int_IssueName"].ToString());
                            objService.Total_IssuePastSLA = Convert.ToString(sqlReader["IssuePastSLA"].ToString());
                            objService.Total_Resoved_Hour = Convert.ToString(sqlReader["TotalResolvehourCNT"].ToString());
                            list.Add(objService);
                        }
                        if (obj.Action == "K")
                        {
                            IssueRegistration objService = new IssueRegistration();
                            objService.int_CategoryId = Convert.ToInt32(sqlReader["int_categoryid"].ToString());
                            objService.CategoryName = Convert.ToString(sqlReader["vch_categoryName"].ToString());
                            objService.dtmCreatedOn = Convert.ToString(sqlReader["dtmCreatedOn"].ToString());
                            objService.vch_IssueDetails = Convert.ToString(sqlReader["vch_IssueDetails"].ToString());
                            objService.vch_UserName = Convert.ToString(sqlReader["vchFullName"].ToString());
                            objService.strRemark = Convert.ToString(sqlReader["vch_Remark"].ToString());
                            objService.vch_SubCategoryName = Convert.ToString(sqlReader["vch_SubcategoryName"].ToString());
                            list.Add(objService);
                        }
                        if (obj.Action == "M")
                        {
                            IssueRegistration objService = new IssueRegistration();
                            objService.int_CategoryId = Convert.ToInt32(sqlReader["int_categoryid"].ToString());
                            objService.CategoryName = Convert.ToString(sqlReader["vch_categoryName"].ToString());
                            objService.dtmCreatedOn = Convert.ToString(sqlReader["dtmCreatedOn"].ToString());
                            objService.vch_IssueDetails = Convert.ToString(sqlReader["vch_IssueDetails"].ToString());
                            objService.vch_UserName = Convert.ToString(sqlReader["vchFullName"].ToString());
                            objService.strRemark = Convert.ToString(sqlReader["vch_Remark"].ToString());
                            objService.vch_SubCategoryName = Convert.ToString(sqlReader["vch_SubcategoryName"].ToString());
                            list.Add(objService);
                        }
                        if (obj.Action == "N")
                        {
                            IssueRegistration objService = new IssueRegistration();
                            objService.int_CategoryId = Convert.ToInt32(sqlReader["int_categoryid"].ToString());
                            objService.CategoryName = Convert.ToString(sqlReader["vch_categoryName"].ToString());
                            objService.dtmCreatedOn = Convert.ToString(sqlReader["dtmCreatedOn"].ToString());
                            objService.vch_IssueDetails = Convert.ToString(sqlReader["vch_IssueDetails"].ToString());
                            objService.vch_UserName = Convert.ToString(sqlReader["vchFullName"].ToString());
                            objService.strRemark = Convert.ToString(sqlReader["vch_Remark"].ToString());
                            objService.vch_SubCategoryName = Convert.ToString(sqlReader["vch_SubcategoryName"].ToString());
                            list.Add(objService);
                        }
                        if (obj.Action == "O")
                        {
                            IssueRegistration objService = new IssueRegistration();
                            objService.int_CategoryId = Convert.ToInt32(sqlReader["int_categoryid"].ToString());
                            objService.CategoryName = Convert.ToString(sqlReader["vch_categoryName"].ToString());
                            objService.dtmCreatedOn = Convert.ToString(sqlReader["dtmCreatedOn"].ToString());
                            objService.dtmUpdatedOn = Convert.ToString(sqlReader["dtmUpdatedOn"].ToString());
                            objService.vch_IssueDetails = Convert.ToString(sqlReader["vch_IssueDetails"].ToString());
                            objService.vch_UserName = Convert.ToString(sqlReader["vchFullName"].ToString());
                            objService.strRemark = Convert.ToString(sqlReader["vch_Remark"].ToString());
                            objService.dtDiffrence = Convert.ToString(sqlReader["DatDiff"].ToString());
                            objService.vch_SubCategoryName = Convert.ToString(sqlReader["vch_SubcategoryName"].ToString());
                            list.Add(objService);
                        }
                    }
                }
                sqlReader.Close();
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
        public List<IssueRegistration> GetEmailID(IssueRegistration objDdl)
        {
            List<IssueRegistration> list = new List<IssueRegistration>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = con;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_IssueRegistration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PAction", objDdl.Action);
                cmd.Parameters.AddWithValue("@PvchInveUserId", objDdl.vchInveUserId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                sqlReader = cmd.ExecuteReader();
                IssueRegistration objInner;
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objInner = new IssueRegistration();
                        objInner.Email = Convert.ToString(sqlReader["VCH_EMAIL"]);
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
    }



}
