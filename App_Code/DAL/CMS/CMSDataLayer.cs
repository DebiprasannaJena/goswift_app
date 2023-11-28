using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLayer.CMS;
using System.Data.SqlClient;
using System.Data;

namespace DataAcessLayer.CMS
{
    public class CMSDataLayer
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        string Str_RetValue = "";
        string strretval = "";
        #region Populate Dropdown
        /// <summary>
        /// Created By  :   AMit Sahoo
        /// Created On  :   19th Aug 2017
        /// To Populate Menus in Dropdownlist
        /// </summary>       
        /// <returns></returns>
        public IList<EntityLayer.CMS.CMSDetails> PopulateMenu()
        {
            IList<EntityLayer.CMS.CMSDetails> objList = new List<EntityLayer.CMS.CMSDetails>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "FM");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.CMS.CMSDetails objInvDet = new EntityLayer.CMS.CMSDetails();
                        objInvDet.StrMenuName = Convert.ToString(sqlReader["vchMenuName"]);
                        objInvDet.IntMenuId = Convert.ToInt32(sqlReader["intMenuId"]);
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

        public string ManageCMS(CMSDetails objCMS)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objCMS.StrAction);
                cmd.Parameters.AddWithValue("@PintMenuId", objCMS.IntMenuId);
                cmd.Parameters.AddWithValue("@PintCmsId", objCMS.IntCmsId);
                cmd.Parameters.AddWithValue("@PvchContent", objCMS.StrContent);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objCMS.IntCreatedBy);
                // cmd.Parameters.AddWithValue("@PbitDeletedFlag", objCMS.bitDeletedFlag);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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

        public List<CMSDetails> ViewCMS(CMSDetails objectCMS)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objectCMS.StrAction);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        CMSDetails objCMS = new CMSDetails();
                        objCMS.IntCmsId = Convert.ToInt32(sqlReader["intCmsId"]);
                        objCMS.StrMenuName = Convert.ToString(sqlReader["vchMenuName"]);
                        objCMS.StrDescription = Convert.ToString(sqlReader["vchDescription"]);
                        objCMS.StrContent = Convert.ToString(sqlReader["vchContent"]);
                        list.Add(objCMS);
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


        public CMSDetails EditCMS(int CmsId)
        {
            CMSDetails objCMS = new CMSDetails();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "E");
                cmd.Parameters.AddWithValue("@PintCmsId", CmsId);


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objCMS.IntCmsId = Convert.ToInt32(sqlReader["intCmsId"]);
                        objCMS.IntMenuId = Convert.ToInt32(sqlReader["intMenuId"]);
                        objCMS.StrContent = Convert.ToString(sqlReader["vchContent"]);
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
            return objCMS;
        }
        public DataTable ChkCMSData(string action, int IntMenuId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
                cmd.Parameters.AddWithValue("@PintMenuId", IntMenuId);
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
        public DataTable GetHeadContent(string action, int IntMenuId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
                cmd.Parameters.AddWithValue("@PintMenuId", IntMenuId);
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
        public string AddContact(CMSDetails objCMS)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objCMS.StrAction);
                cmd.Parameters.AddWithValue("@P_VCHNAME", objCMS.Strusername);
                cmd.Parameters.AddWithValue("@P_VCHEMAIL", objCMS.Strmail);
                cmd.Parameters.AddWithValue("@P_VCHPHONENO ", objCMS.Strmobileno);
                cmd.Parameters.AddWithValue("@P_VCHCOMPANYNAME", objCMS.Strcompanyname);
                cmd.Parameters.AddWithValue("@P_VCHMESSAGE", objCMS.StrDescription);
                cmd.Parameters.AddWithValue("@P_INTCREATEDBY ", objCMS.IntCreatedBy);
                // cmd.Parameters.AddWithValue("@PbitDeletedFlag", objCMS.bitDeletedFlag);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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
        public string AddNews(CMSDetails objCMS)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objCMS.StrAction);
                cmd.Parameters.AddWithValue("@P_VCH_TYPE", objCMS.Strtype);
                cmd.Parameters.AddWithValue("@P_VCH_HEADING", objCMS.Strheading);
                cmd.Parameters.AddWithValue("@P_VCHIMAGE", objCMS.Strimg);
                cmd.Parameters.AddWithValue("@PvchContent", objCMS.StrContent);
                cmd.Parameters.AddWithValue("@P_INTCREATEDBY ", objCMS.IntCreatedBy);
                cmd.Parameters.AddWithValue("@P_VCHIMGPATH ", objCMS.strpath);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters.AddWithValue("@PintCmsId ", objCMS.IntCmsId);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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
        public DataTable BindNewsEventData(string action, string strtype)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
                cmd.Parameters.AddWithValue("@P_VCH_TYPE", strtype);
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
        public DataTable BindCondData(string action, int id)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
                cmd.Parameters.AddWithValue("@PintCmsId", id);
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
        #region ServiceCMS Management
        //  /// <summary>
        /// Created By  :  Prasun Kali
        /// Created On  :   20th Sep 2017
        /// To Manage Service Instruction CMS.
        /// </summary>       
        /// <returns></returns>
        public IList<CMSDetails> GetServices()
        {
            IList<EntityLayer.CMS.CMSDetails> objList = new List<EntityLayer.CMS.CMSDetails>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "FS");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.CMS.CMSDetails objInvDet = new EntityLayer.CMS.CMSDetails();
                        objInvDet.StrServicename = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                        objInvDet.IntServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
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

        public string ManageServiceCMS(CMSDetails objCMS)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objCMS.StrAction);
                cmd.Parameters.AddWithValue("@P_SERVICEId", objCMS.IntServiceId);
                cmd.Parameters.AddWithValue("@PintCmsId", objCMS.IntCmsId);
                cmd.Parameters.AddWithValue("@PvchContent", objCMS.StrContent);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objCMS.IntCreatedBy);
                cmd.Parameters.AddWithValue("@P_vchUserManual", objCMS.strAttachment);
                // cmd.Parameters.AddWithValue("@PbitDeletedFlag", objCMS.bitDeletedFlag);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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

        public IList<CMSDetails> GetCMSData(CMSDetails objCMS)
        {


            IList<EntityLayer.CMS.CMSDetails> objList = new List<EntityLayer.CMS.CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objCMS.StrAction);
                cmd.Parameters.AddWithValue("@P_SERVICEId", objCMS.IntServiceId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.CMS.CMSDetails objInvDet = new EntityLayer.CMS.CMSDetails();
                        objInvDet.StrContent = Convert.ToString(sqlReader["vchContent"]);
                        objInvDet.IntServiceId = Convert.ToInt32(sqlReader["intServiceId"]);
                        objInvDet.strAttachment = sqlReader["vchUserManual"].ToString();
                        objList.Add(objInvDet);
                    }
                    sqlReader.Close();
                }

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
            return objList;
        }


        #endregion
        public DataTable BindDepartment(string action)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                //conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DEPARTMENT";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
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
        public DataTable BindServiceData(string action, int deptid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                //conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DEPARTMENT";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
                cmd.Parameters.AddWithValue("@P_INT_DEPTID", deptid);
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


        public IList<CMSDetails> GetWebsiteProposalDetails(CMSDetails objCMS)
        {
            IList<EntityLayer.CMS.CMSDetails> objList = new List<EntityLayer.CMS.CMSDetails>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "VP");
                cmd.Parameters.AddWithValue("@YEARVal", objCMS.Received);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.CMS.CMSDetails objInvDet = new EntityLayer.CMS.CMSDetails();
                        objInvDet.Received = Convert.ToString(sqlReader["TotApplRec"]);
                        objInvDet.Approved = Convert.ToString(sqlReader["Approved"]);
                        if (Convert.ToString(sqlReader["TotCapitalApprove"]) == "")
                        {
                            objInvDet.TotCapital = "0";
                        }
                        else
                        {
                            objInvDet.TotCapital = Convert.ToString(sqlReader["TotCapitalApprove"]);
                        }
                        objInvDet.TotEmpProp = Convert.ToString(sqlReader["TotPropEmpApproved"]);
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


        public IList<CMSDetails> GetServiceManual()
        {
            IList<EntityLayer.CMS.CMSDetails> objList = new List<EntityLayer.CMS.CMSDetails>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "GUM");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.CMS.CMSDetails objInvDet = new EntityLayer.CMS.CMSDetails();
                        objInvDet.StrServicename = sqlReader["VCH_SERVICENAME"].ToString();
                        objInvDet.strAttachment = sqlReader["vchUserManual"].ToString();
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
        public DataTable ChkProposal(string action, int investorid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                //conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DEPARTMENT";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
                cmd.Parameters.AddWithValue("@P_INVEST_ID", investorid);
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
        public DataTable GetServiceDetails(string action, int serviceid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();


            try
            {
                //conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DEPARTMENT";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", action);
                cmd.Parameters.AddWithValue("@P_SERVICE_ID", serviceid);
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
        public string DeleteContentData(CMSDetails objCMS)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_Gallery";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objCMS.StrAction);
                cmd.Parameters.AddWithValue("@P_INT_CMSID", objCMS.IntCmsId);
                cmd.Parameters.AddWithValue("@PintCreatedBy", objCMS.IntCreatedBy);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
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
            return Str_RetValue;
        }


        public string AddTemplateDetails(CMSDetails obj)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PnvcTemplate", obj.Template);
                cmd.Parameters.AddWithValue("@PnvcPageName", obj.pagename);
                cmd.Parameters.AddWithValue("@PnvcTitle", obj.title);
                cmd.Parameters.AddWithValue("@PnvcDescription", obj.description);
                cmd.Parameters.AddWithValue("@nvcKeyWord", obj.keyword);
                cmd.Parameters.AddWithValue("@PintTemplateId", obj.Templateid);
                cmd.Parameters.AddWithValue("@Ptempid", obj.Temptid);
                cmd.Parameters.AddWithValue("@PvchSinppet", obj.Sinppet);
                cmd.Parameters.AddWithValue("@PageContent", obj.PageContent);
                cmd.Parameters.AddWithValue("@PnvcAuthor", obj.Authorname);
                cmd.Parameters.AddWithValue("@vch_Content1", obj.strContent1);
                cmd.Parameters.AddWithValue("@vch_Content2", obj.strContent2);
                cmd.Parameters.AddWithValue("@vch_Content3", obj.strContent3);
                cmd.Parameters.AddWithValue("@vch_Content4", obj.strContent4);
                cmd.Parameters.AddWithValue("@P_MSGOUT", SqlDbType.NVarChar);
                cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
                cmd.Parameters["@P_MSGOUT"].Size = 500;
                cmd.ExecuteNonQuery();
                strretval = cmd.Parameters["@P_MSGOUT"].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            return strretval;
        }
        public List<CMSDetails> ViewPageDetails(CMSDetails obj)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PintTemplateId", obj.Templateid);
                cmd.Parameters.AddWithValue("@PnvcPageName", obj.pagename);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CMSDetails objdata = new CMSDetails();
                        objdata.Templateid = Convert.ToInt32(dr["intTemplateId"].ToString());
                        objdata.Template = dr["nvcTemplate"].ToString();
                        objdata.pagename = dr["nvcPageName"].ToString();
                        objdata.title = dr["nvcTitle"].ToString();
                        objdata.description = dr["nvcDescription"].ToString();
                        objdata.keyword = dr["nvcKeyWord"].ToString();
                        objdata.Sinppet = dr["vchSinppet"].ToString();
                        objdata.PageContent = dr["nvcPageContent"].ToString();
                        objdata.Authorname = dr["nvcAuthor"].ToString();
                        objdata.Temptid = Convert.ToInt32(dr["intTmplateId"].ToString());
                        objdata.dtmCreatedOn = dr["dtmCreatedOn"].ToString();
                        objdata.strContent1 = dr["vch_Content1"].ToString();
                        objdata.strContent2 = dr["vch_Content2"].ToString();
                        objdata.strContent3 = dr["vch_Content3"].ToString();
                        objdata.strContent4 = dr["vch_Content4"].ToString();
                        list.Add(objdata);
                    }

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return list;

        }
        public List<CMSDetails> GetContentDetails(CMSDetails obj)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PintPageid", obj.pageid);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CMSDetails objdata = new CMSDetails();
                        objdata.PageContent = dr["nvcPageContent"].ToString();
                        objdata.pagename = dr["nvcPageName"].ToString();
                        list.Add(objdata);
                    }

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return list;

        }
        public string AddGlinkDetails(CMSDetails obj)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PvchGlink", obj.Glink);
                cmd.Parameters.AddWithValue("@PGlinkid", obj.Glinkid);
                cmd.Parameters.AddWithValue("@PintWindowType", obj.intWindowType);
                cmd.Parameters.AddWithValue("@PintPageid", obj.pageid);
                cmd.Parameters.AddWithValue("@PintPageType", obj.intPageType);
                cmd.Parameters.AddWithValue("@PVchURL", obj.vchURL);
                cmd.Parameters.AddWithValue("@vchModalId", obj.vchModalId);
                cmd.Parameters.AddWithValue("@P_MSGOUT", SqlDbType.NVarChar);
                cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
                cmd.Parameters["@P_MSGOUT"].Size = 500;
                cmd.ExecuteNonQuery();
                strretval = cmd.Parameters["@P_MSGOUT"].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            return strretval;
        }

        public List<CMSDetails> ViewGlinkDetails(CMSDetails obj)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PGlinkid", obj.Glinkid);
                cmd.Parameters.AddWithValue("@PvchGlink", obj.Glink);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CMSDetails objdata = new CMSDetails();
                        objdata.Glinkid = Convert.ToInt32(dr["intGlinkId"].ToString());
                        objdata.Glink = dr["vchGlink"].ToString();
                        objdata.pageid = Convert.ToInt32(dr["intPageId"].ToString());
                        objdata.intWindowType = Convert.ToInt32(dr["intWindowType"].ToString());
                        objdata.intPageType = Convert.ToInt32(dr["intPageType"].ToString());
                        objdata.pagename = dr["vchURL"].ToString();
                        objdata.viewWindowType = dr["viewWindowType"].ToString();
                        objdata.viewPageType = dr["viewPageType"].ToString();
                        objdata.vchModalId = dr["vchModalId"].ToString();


                        list.Add(objdata);
                    }

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return list;

        }
        public List<CMSDetails> ViewPlinkDetails(CMSDetails obj)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PPlinkid", obj.Plinkid);
                cmd.Parameters.AddWithValue("@PGlinkid", obj.Glinkid);
                cmd.Parameters.AddWithValue("@PvchPlink", obj.Plink);
                cmd.Parameters.AddWithValue("@PnvcPageName", obj.pagename);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CMSDetails objdata = new CMSDetails();
                        objdata.Plinkid = Convert.ToInt32(dr["intPlinkId"].ToString());
                        objdata.Glinkid = Convert.ToInt32(dr["intGlinkId"].ToString());
                        objdata.pageid = Convert.ToInt32(dr["intPageId"].ToString());
                        objdata.Glink = dr["vchGlink"].ToString();
                        objdata.Plink = dr["vchPlink"].ToString();
                        //objdata.pagename = dr["nvcPageName"].ToString();
                        objdata.intWindowType = Convert.ToInt32(dr["intWindowType"].ToString());
                        objdata.intLinkType = Convert.ToInt32(dr["intLinkType"].ToString());
                        objdata.intPageType = Convert.ToInt32(dr["intPageType"].ToString());
                        objdata.vchURL = dr["vchURL"].ToString();
                        list.Add(objdata);
                    }

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return list;

        }
        public List<CMSDetails> GlinkList(CMSDetails obj)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CMSDetails objdata = new CMSDetails();
                        objdata.Glinkid = Convert.ToInt32(dr["intGlinkId"]);
                        objdata.Glink = dr["vchGlink"].ToString();
                        list.Add(objdata);
                    }

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return list;

        }
        public List<CMSDetails> BindPageList(CMSDetails obj)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CMSDetails objdata = new CMSDetails();
                        objdata.pageid = Convert.ToInt32(dr["intTemplateId"]);
                        objdata.pagename = dr["nvcPageName"].ToString();
                        list.Add(objdata);
                    }

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return list;

        }
        public string AddPlinkDetails(CMSDetails obj)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PPlinkid", obj.Plinkid);
                cmd.Parameters.AddWithValue("@PGlinkid", obj.Glinkid);
                cmd.Parameters.AddWithValue("@PintPageid", obj.pageid);
                cmd.Parameters.AddWithValue("@PvchPlink", obj.Plink);
                cmd.Parameters.AddWithValue("@PintWindowType", obj.intWindowType);
                cmd.Parameters.AddWithValue("@PintLinkType", obj.intLinkType);
                cmd.Parameters.AddWithValue("@PVchURL", obj.vchURL);
                cmd.Parameters.AddWithValue("@PintPageType", obj.intPageType);
                cmd.Parameters.AddWithValue("@P_MSGOUT", SqlDbType.NVarChar);
                cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
                cmd.Parameters["@P_MSGOUT"].Size = 500;
                cmd.ExecuteNonQuery();
                strretval = cmd.Parameters["@P_MSGOUT"].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            return strretval;
        }
        public DataTable BindPlinkDetails(string action)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", action);
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
        public DataTable BindGlinkMenuDetails(string action)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", action);
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
        public DataTable BindGlinkSubMenuDetails(string action, int GlinkId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", action);
                cmd.Parameters.AddWithValue("@PGlinkid", GlinkId);
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

        public List<CMSDetails> GetMenuLinkDetails(CMSDetails obj)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PPlinkid", obj.Plinkid);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CMSDetails objdata = new CMSDetails();
                        objdata.pagename = dr["treepath"].ToString();
                        objdata.Plink = dr["vchLinkName"].ToString();
                        list.Add(objdata);
                    }

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return list;

        }

        public string AddUseFulinkDetails(CMSDetails obj)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_USEFULLINK_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PvchUsefulLinkName", obj.vchUseFulinkName);
                cmd.Parameters.AddWithValue("@PvchURL", obj.vchURL);
                cmd.Parameters.AddWithValue("@PvchImgeURL", obj.vchUseImageURL);
                cmd.Parameters.AddWithValue("@PintlinkId", obj.intlinkId);
                cmd.Parameters.AddWithValue("@intCreatedBy", obj.IntCreatedBy);
                cmd.Parameters.AddWithValue("@P_MSGOUT", SqlDbType.NVarChar);
                cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
                cmd.Parameters["@P_MSGOUT"].Size = 500;
                cmd.ExecuteNonQuery();
                strretval = cmd.Parameters["@P_MSGOUT"].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            return strretval;
        }
        public List<CMSDetails> UseFulLinkDetails(CMSDetails obj)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_USEFULLINK_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PintlinkId", obj.intlinkId);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CMSDetails objdata = new CMSDetails();
                        objdata.intlinkId = Convert.ToInt32(dr["intlinkId"].ToString());
                        objdata.vchUseFulinkName = dr["vchUsefulLinkName"].ToString();
                        objdata.vchURL = dr["vchURL"].ToString();
                        objdata.vchUseImageURL = dr["vchImgeURL"].ToString();
                        list.Add(objdata);
                    }

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return list;

        }
        public DataTable BindUsefulLinkDetails(string action)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_USEFULLINK_AUD";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", action);

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

        public List<TemplateDetails> TemplateContentDetails(TemplateDetails obj)
        {
            List<TemplateDetails> list = new List<TemplateDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PintTemplateId", obj.TemplateId);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TemplateDetails objdata = new TemplateDetails();
                        //objdata.ContentType = Convert.ToInt32(dr["INT_CONTENT_TYPE"].ToString());
                        //objdata.DivName = dr["VCH_DIVNAME"].ToString();
                        objdata.TemplateId = Convert.ToInt32(dr["intTemplateId"].ToString());
                        objdata.strContent1 = dr["vch_Content1"].ToString();
                        objdata.strContent2 = dr["vch_Content2"].ToString();
                        objdata.strContent3 = dr["vch_Content3"].ToString();
                        objdata.strContent4 = dr["vch_Content4"].ToString();
                        objdata.strPageName = dr["nvcPageName"].ToString();
                        //objdata.strContent5 = dr["vch_Content5"].ToString();
                        //objdata.strContent6 = dr["vch_Content6"].ToString();
                        //objdata.strContent7 = dr["vch_Content7"].ToString();
                        //objdata.strContent8 = dr["vch_Content8"].ToString();
                        //objdata.strContent9 = dr["vch_Content9"].ToString();
                        //objdata.strContent10 = dr["vch_Content10"].ToString();
                        list.Add(objdata);
                    }

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return list;

        }
        public string UpdateTemplateDetails(TemplateDetails obj)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PintTemplateId", obj.TemplateId);
                cmd.Parameters.AddWithValue("@vch_Content1", obj.strContent1);
                cmd.Parameters.AddWithValue("@vch_Content2", obj.strContent2);
                cmd.Parameters.AddWithValue("@vch_Content3", obj.strContent3);
                cmd.Parameters.AddWithValue("@vch_Content4", obj.strContent4);
                //cmd.Parameters.AddWithValue("@vch_Content5", obj.strContent5);
                //cmd.Parameters.AddWithValue("@vch_Content6", obj.strContent6);
                //cmd.Parameters.AddWithValue("@vch_Content7", obj.strContent7);
                //cmd.Parameters.AddWithValue("@vch_Content8", obj.strContent8);
                //cmd.Parameters.AddWithValue("@vch_Content9", obj.strContent9);
                //cmd.Parameters.AddWithValue("@vch_Content10", obj.strContent10);
                cmd.Parameters.AddWithValue("@P_MSGOUT", SqlDbType.NVarChar);
                cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
                cmd.Parameters["@P_MSGOUT"].Size = 500;
                cmd.ExecuteNonQuery();
                strretval = cmd.Parameters["@P_MSGOUT"].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            return strretval;
        }

        public string TemplateContentCount(TemplateDetails obj)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@P_MSGOUT", SqlDbType.NVarChar);
                cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
                cmd.Parameters["@P_MSGOUT"].Size = 500;
                cmd.ExecuteNonQuery();
                strretval = cmd.Parameters["@P_MSGOUT"].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            return strretval;
        }
        public string PrimaryLinkName(TemplateDetails obj)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TEMPLATE_AUD";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@PPlinkid", obj.IntPLinkId);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        strretval = dr["vchLinkName"].ToString();
                    }

                }
                dr.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            return strretval;
        }

        public List<CMSDetails> Dynamicheaderfooterview(CMSDetails obj)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_DYNAMICHEADER_FOOTER";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CMSDetails objdata = new CMSDetails();
                        objdata.intlinkId = Convert.ToInt32(dr["INT_ID"].ToString());
                        objdata.vchURL = dr["VCH_LINK"].ToString();
                        objdata.StrMenuName = dr["VCH_NAME"].ToString();
                        list.Add(objdata);
                    }

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return list;
        }
        public string AddHeaderDetails(CMSDetails objHeader)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_DYNAMICHEADER_FOOTER";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objHeader.actioncode);
                cmd.Parameters.AddWithValue("@P_INT_HEADERID", objHeader.intHeaderId);
                cmd.Parameters.AddWithValue("@P_VCHHEADERNAME", objHeader.strHdrMenues);
                cmd.Parameters.AddWithValue("@P_VCHHEADERURL", objHeader.strHdrUrl);
                cmd.Parameters.AddWithValue("@P_MSGOUT", SqlDbType.NVarChar);
                cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
                cmd.Parameters["@P_MSGOUT"].Size = 500;
                cmd.ExecuteNonQuery();
                strretval = cmd.Parameters["@P_MSGOUT"].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            return strretval;
        }
        public string AddFooterDetails(CMSDetails objFooter)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_DYNAMICHEADER_FOOTER";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", objFooter.actioncode);
                cmd.Parameters.AddWithValue("@P_INT_FOOTERID", objFooter.intFooterId);
                cmd.Parameters.AddWithValue("@P_VCHFOOTERNAME", objFooter.strFtrMenues);
                cmd.Parameters.AddWithValue("@P_VCHFOOTERURL", objFooter.strFtrUrl);
                cmd.Parameters.AddWithValue("@P_MSGOUT", SqlDbType.NVarChar);
                cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
                cmd.Parameters["@P_MSGOUT"].Size = 500;
                cmd.ExecuteNonQuery();
                strretval = cmd.Parameters["@P_MSGOUT"].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
                cmd.Dispose();
            }
            return strretval;
        }
        public DataTable DynamicHeaderview(CMSDetails obj)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_DYNAMICHEADER_FOOTER";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@P_INT_HEADERID", obj.intHeaderId);
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
        public DataTable DynamicFooterview(CMSDetails obj)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_DYNAMICHEADER_FOOTER";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@P_INT_FOOTERID", obj.intFooterId);
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
        public List<CMSDetails> ContactusDetails(CMSDetails obj)
        {
            List<CMSDetails> list = new List<CMSDetails>();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_CMS_DETAILS";
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", obj.actioncode);
                cmd.Parameters.AddWithValue("@P_VCHCOMPANYNAME", obj.Strcompanyname);
                cmd.Parameters.AddWithValue("@P_VCHPHONENO", obj.Strmobileno);
                cmd.Parameters.AddWithValue("@P_VCHEMAIL", obj.Strmail);
                cmd.Parameters.AddWithValue("@P_VCHNAME", obj.Strusername);
                cmd.Parameters.AddWithValue("@P_VchFromDate", obj.StrFromDate);
                cmd.Parameters.AddWithValue("@p_VchToDate", obj.StrToDate);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CMSDetails objdata = new CMSDetails();
                        objdata.IntCmsId = Convert.ToInt32(dr["INT_ID"].ToString());
                        objdata.Strusername = dr["VCH_NAME"].ToString();
                        objdata.Strmail = dr["VCH_EMAIL"].ToString();
                        objdata.Strmobileno = dr["VCH_PHONE_NO"].ToString();
                        objdata.Strcompanyname = dr["VCH_COMPANY_NAME"].ToString();
                        objdata.StrDescription = dr["VCH_MESSAGE"].ToString();
                        objdata.StrDate = Convert.ToString(dr["DTM_CONTACT_DATE"]);
                        objdata.IntCreatedBy = Convert.ToInt32(dr["INT_CREATED_BY"].ToString());
                        list.Add(objdata);
                    }
                }
                dr.Close();

            }
            catch (Exception ex)
            {

                throw;
            }
            return list;

        }
    }



}
