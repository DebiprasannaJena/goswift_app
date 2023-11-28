using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityLayer.Service;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using Common.Persistence.Data;
namespace DataAcessLayer.Service
{
    public class ServiceDataLayer
    {
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        string Str_RetValue = "";
        object param = null;

        public string UpdatePaymentService(string strApplicationID, double amount, decimal decAppFee)
        {
            string res = "0";

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Payment";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_APPLICATIONID", strApplicationID);
                cmd.Parameters.AddWithValue("@PNUM_PAYMENT_AMOUNT", amount);
                cmd.Parameters.AddWithValue("@P_DEC_APPLICATION_FEE", decAppFee); ////Added by Sushant Jena On Dt:-15-Apr-2020

                cmd.ExecuteNonQuery();
                Str_RetValue = "1";
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
            return res;
        }
        public string UpdatePaymentStatus(string strApplicationID, string VCH_PAYMENT_ACKNOWLEDGEMENT_NO, string VCH_CHALLAN_NO, int INT_PAYMENT_STATUS, string strOrderNo)
        {

            string res = "0";

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PaymentStatus";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_UNQ_KEY", strApplicationID);
                cmd.Parameters.AddWithValue("@P_VCH_PAYMENT_ACKNOWLEDGEMENT_NO", VCH_PAYMENT_ACKNOWLEDGEMENT_NO);
                cmd.Parameters.AddWithValue("@P_VCH_CHALLAN_NO", VCH_CHALLAN_NO);
                cmd.Parameters.AddWithValue("@P_INT_PAYMENT_STATUS", INT_PAYMENT_STATUS);
                cmd.Parameters.AddWithValue("@P_VCH_ORDERID", strOrderNo);

                cmd.ExecuteNonQuery();
                Str_RetValue = "1";
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
            return res;
        }
        public List<EntityLayer.Service.ServiceDetails> DepartmentWise_Reporty(ServiceDetails obj)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_DepartmentWise_Report";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_Action", obj.strAction);
                cmd.Parameters.AddWithValue("@P_INT_USERID", obj.Deptid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.nvchLevelName = sqlReader["nvchLevelName"].ToString();
                        objService.intLevelDetailId = Convert.ToInt32(sqlReader["intLevelDetailId"]);
                        // objService.INT_SERVICEID = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        //objService.INT_STATUS = sqlReader["INT_STATUS"].ToString();
                        objService.Total_Recieved = sqlReader["TotalRecieved"].ToString();
                        objService.Applied = sqlReader["Applied"].ToString();
                        objService.Rejected = sqlReader["Rejected"].ToString();
                        objService.Approved = sqlReader["Approved"].ToString();
                        objService.QueryRasied = sqlReader["QueryRasied"].ToString();
                        objService.QueryReverted = sqlReader["QueryReverted"].ToString();
                        objService.Differed = sqlReader["Differed"].ToString();
                        objService.InProgress = sqlReader["InProgress"].ToString();
                        objService.Pending = sqlReader["Pending"].ToString();
                        objService.Forwarded = sqlReader["Forwarded"].ToString();
                        objService.GenerateDemand = sqlReader["GenerateDemand"].ToString();
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
        public List<EntityLayer.Service.ServiceDetails> ServiceWise_Report(ServiceDetails obj)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_DepartmentWise_Report";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_Action", obj.strAction);
                cmd.Parameters.AddWithValue("@P_intLevelDetailId", obj.intLevelDetailId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.nvchLevelName = sqlReader["nvchLevelName"].ToString();
                        //objService.intLevelDetailId = Convert.ToInt32(sqlReader["intLevelDetailId"]);
                        objService.INT_SERVICEID = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.VCH_SERVICENAME = sqlReader["VCH_SERVICENAME"].ToString();
                        objService.Total_Recieved = sqlReader["TotalRecieved"].ToString();
                        objService.Applied = sqlReader["Applied"].ToString();
                        objService.Rejected = sqlReader["Rejected"].ToString();
                        objService.Approved = sqlReader["Approved"].ToString();
                        objService.QueryRasied = sqlReader["QueryRasied"].ToString();
                        objService.QueryReverted = sqlReader["QueryReverted"].ToString();
                        objService.Differed = sqlReader["Differed"].ToString();
                        objService.InProgress = sqlReader["InProgress"].ToString();
                        objService.Pending = sqlReader["Pending"].ToString();
                        objService.Forwarded = sqlReader["Forwarded"].ToString();
                        objService.GenerateDemand = sqlReader["GenerateDemand"].ToString();
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
        public List<EntityLayer.Service.ServiceDetails> ApplicationWise_Report(ServiceDetails obj)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_DepartmentWise_Report";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_Action", obj.strAction);
                //cmd.Parameters.AddWithValue("@P_intLevelDetailId", obj.intLevelDetailId);
                cmd.Parameters.AddWithValue("@P_INT_SERVICEID", obj.INT_SERVICEID);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.VCH_APPLICATION_UNQ_KEY = sqlReader["VCH_APPLICATION_UNQ_KEY"].ToString();
                        objService.INT_SERVICEID = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.strInvesterName = sqlReader["VCH_INVESTOR_NAME"].ToString();
                        //objService.nvchLevelName = sqlReader["nvchLevelName"].ToString();
                        //objService.VCH_SERVICENAME = sqlReader["VCH_SERVICENAME"].ToString();
                        objService.Payment = sqlReader["Payment"].ToString();
                        objService.INT_STATUS = sqlReader["STATUS"].ToString();
                        // objService.INT_PAYMENT_STATUS = Convert.ToInt32(sqlReader["INT_PAYMENT_STATUS"].ToString());

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
        public List<EntityLayer.Service.ServiceDetails> ApplicationStatus_Report(ServiceDetails obj)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_DepartmentWise_Report";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_Action", obj.strAction);
                //cmd.Parameters.AddWithValue("@P_INT_SERVICEID", obj.INT_SERVICEID);
                cmd.Parameters.AddWithValue("@P_intLevelDetailId", obj.intLevelDetailId);
                cmd.Parameters.AddWithValue("@p_type", obj.vchType);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.VCH_APPLICATION_UNQ_KEY = sqlReader["VCH_APPLICATION_UNQ_KEY"].ToString();
                        objService.Payment = sqlReader["Payment"].ToString();
                        objService.INT_SERVICEID = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        //objService.INT_STATUS = sqlReader["INT_STATUS"].ToString();
                        //objService.nvchLevelName = sqlReader["nvchLevelName"].ToString();
                        //objService.VCH_SERVICENAME = sqlReader["VCH_SERVICENAME"].ToString();
                        objService.strInvesterName = sqlReader["VCH_INVESTOR_NAME"].ToString();
                        objService.INT_STATUS = sqlReader["STATUS"].ToString();
                        //objService.INT_PAYMENT_STATUS =Convert.ToInt32( sqlReader["INT_PAYMENT_STATUS"].ToString());

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
        public List<EntityLayer.Service.ServiceDetails> BindDepartmentWise(ServiceDetails obj)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;

            try
            {
                string querry = "select nvchLevelName,VCH_SERVICENAME,Total_Recieved,Applied,Rejected,Approved,Pending from VW_MIS_SERVICE";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = querry;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }


                cmd.CommandType = CommandType.Text;

                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.nvchLevelName = sqlReader["nvchLevelName"].ToString();
                        objService.VCH_SERVICENAME = sqlReader["VCH_SERVICENAME"].ToString();
                        objService.Total_Recieved = sqlReader["Total_Recieved"].ToString();
                        objService.Applied = sqlReader["Applied"].ToString();
                        objService.Rejected = sqlReader["Rejected"].ToString();
                        objService.Approved = sqlReader["Approved"].ToString();
                        objService.Pending = sqlReader["Pending"].ToString();
                        list.Add(objService);
                    }

                }
                sqlReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    cmd = null;
            //}
            return list;
        }
        //public static string GetWeightCalculation()
        //{
        //    string strOut = "";
        //    string querry = "select nvchLevelName ,VCH_SERVICENAME ,[Total Recieved] , Applied ,Rejected,Approved  from VW_MIS_SERVICE ";
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand(querry))
        //        {

        //            cmd.CommandType = CommandType.Text;
        //            cmd.Connection = con;
        //            con.Open();
        //            strOut = cmd.ExecuteScalar().ToString();
        //            con.Close();
        //            return strOut;
        //        }
        //    }
        //}
        public List<EntityLayer.Service.ServiceDetails> BindServiceOnlyForward(string strAction, int Deptid, int? userid, int intserviceid)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                //cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@Deptid", Deptid);
                cmd.Parameters.AddWithValue("@P_USERID", userid);
                cmd.Parameters.AddWithValue("@SERVICEID", intserviceid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.strServiceName = sqlReader["VCH_SERVICENAME"].ToString();
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
        public List<EntityLayer.Service.ServiceDetails> BindService(string strAction, int Deptid)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                //cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@Deptid", Deptid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.strServiceName = sqlReader["VCH_SERVICENAME"].ToString();
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
        public List<EntityLayer.Service.ServiceDetails> BindPanel(string strAction, int FormId)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_PanelDetails";
                //cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "DP");
                cmd.Parameters.AddWithValue("@FORMID", FormId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.PanelId = Convert.ToInt32(sqlReader["INT_PANELID"]);
                        objService.PanelName = sqlReader["VCH_PANELNAME"].ToString();
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
        public List<EntityLayer.Service.ServiceDetails> BindEmployeeName(string strAction, string strFullname)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@fullname", strFullname);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.userid = Convert.ToInt32(sqlReader["intUserId"]);
                        objService.strfullname = sqlReader["fullname"].ToString();
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

        public List<EntityLayer.Service.ServiceDetails> BindDepartment(string strAction)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.Deptid = Convert.ToInt32(sqlReader["intLevelDetailId"]);
                        objService.strdeptname = sqlReader["nvchLevelName"].ToString();
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
        public List<EntityLayer.Service.ServiceDetails> FindUserDepartment(string strAction, string strUserid)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@P_USERID", strUserid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.Deptid = Convert.ToInt32(sqlReader["department"]);
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
        public List<EntityLayer.Service.ServiceDetails> BindLineDepartment(string strAction, int Deptid)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@Deptid", Deptid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.LinedeptId = Convert.ToInt32(sqlReader["intLevelDetailId"]);
                        objService.strlinedeptname = sqlReader["nvchLevelName"].ToString();
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
        public List<EntityLayer.Service.ServiceDetails> GetEmailAndMobile(string strAction, string vchApplicationNo)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_GETMAILDETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCHACTION", strAction);
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", vchApplicationNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.strMobileno = sqlReader["MOBILENO"].ToString();
                        objService.InvestorName = sqlReader["VCH_INV_NAME"].ToString();
                        objService.Email = sqlReader["EMAIL"].ToString();
                        objService.strApplicationUnqKey = sqlReader["VCH_UNIQUEID"].ToString();
                        objService.vchChallanAmount = sqlReader["vchChallanAmount"].ToString();
                        objService.vchChallanNo = sqlReader["vchChallanRefid"].ToString();
                        objService.str_PaymentReceived = sqlReader["dtm_Payment_date"].ToString();
                        objService.VCH_PAN = sqlReader["VCH_PAN"].ToString();
                        objService.ParentUniqueKey = sqlReader["ParentUniqueKey"].ToString();
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

        public List<EntityLayer.Service.ServiceDetails> BindOffice(string strAction, int LinedeptIdDeptid)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@LineDeptid", LinedeptIdDeptid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.OfficeId = Convert.ToInt32(sqlReader["intLevelDetailId"]);
                        objService.OfficeName = sqlReader["nvchLevelName"].ToString();
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

        public List<EntityLayer.Service.ServiceDetails> BindLocation(string strAction)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.LocationId = Convert.ToInt32(sqlReader["intLocationId"]);
                        objService.StrLocationName = sqlReader["nvchLocation"].ToString();
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

        public List<EntityLayer.Service.ServiceDetails> BindAllDepartment(string strAction, int ParentId)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@INTPARENTID", ParentId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.Deptid = Convert.ToInt32(sqlReader["intLevelDetailId"]);
                        objService.strdeptname = sqlReader["nvchLevelName"].ToString();
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

        public List<EntityLayer.Service.ServiceDetails> BindDirectorate(string strAction, int ParentId)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@INTPARENTID", ParentId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.DirectId = Convert.ToInt32(sqlReader["intLevelDetailId"]);
                        objService.StrDireName = sqlReader["nvchLevelName"].ToString();
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

        public List<EntityLayer.Service.ServiceDetails> BindDivision(string strAction, int ParentId)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@INTPARENTID", ParentId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.DivisionId = Convert.ToInt32(sqlReader["intLevelDetailId"]);
                        objService.StrDivisionName = sqlReader["nvchLevelName"].ToString();
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

        public List<EntityLayer.Service.ServiceDetails> BindDistrict(string strAction, int ParentId)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@INTPARENTID", ParentId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.DistId = Convert.ToInt32(sqlReader["intLevelDetailId"]);
                        objService.StrDistname = sqlReader["nvchLevelName"].ToString();
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

        public List<EntityLayer.Service.ServiceDetails> BindUser(string strAction, int LevelDetailsId)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                //cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@intLevelDetailId", LevelDetailsId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.userid = Convert.ToInt32(sqlReader["intUserId"]);
                        objService.strUsername = sqlReader["vchFullName"].ToString();
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

        public List<EntityLayer.Service.ServiceDetails> BindAllUser(string strAction)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                //cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.userid = Convert.ToInt32(sqlReader["intUserId"]);
                        objService.strUsername = sqlReader["vchFullName"].ToString();
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

        public string ServiceConfigurationData(ServiceDetails objService)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@SERVICEID", objService.intServiceId);
                cmd.Parameters.AddWithValue("@DEPTID", objService.Deptid);
                cmd.Parameters.AddWithValue("@LINEDEPTID", objService.Typeid);
                cmd.Parameters.Add("@P_XML", SqlDbType.VarChar).Value = objService.XMLDATA;
                cmd.Parameters.AddWithValue("@P_MSGOUT", SqlDbType.VarChar);
                cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_MSGOUT"].Value.ToString();
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

        public List<EntityLayer.Service.ServiceDetails> ViewSErviceTakeActionDetails(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            string strAction = "";
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_ServiceViewAndTakeAction";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_USERID", objService.intActionTobeTakenBy);
                cmd.Parameters.AddWithValue("@P_SERVICEID", objService.intServiceId);
                cmd.Parameters.AddWithValue("@P_DEPTD", objService.Deptid);
                cmd.Parameters.AddWithValue("@P_VCHAPPLICATIONNO", objService.str_ApplicationNo);
                cmd.Parameters.AddWithValue("@P_VCHPROPOSALNO", objService.strProposalId);
                cmd.Parameters.AddWithValue("@P_VCHFROMDATE", objService.strFromdate);
                cmd.Parameters.AddWithValue("@P_VCHTODATE", objService.strTodate);
                strAction = objService.strAction;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.strApplicationUnqKey = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                        objService.strProposalId = sqlReader["VCH_PROPOSALID"].ToString();
                        objService.strServiceName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                        objService.strInvesterName = Convert.ToString(sqlReader["VCH_INVESTOR_NAME"]);
                        objService.strIndustryName = Convert.ToString(sqlReader["INDUSTRYNAME"]);
                        objService.strIndType = Convert.ToString(sqlReader["INDTYPE"]);
                        objService.Requestdate = Convert.ToString(sqlReader["DTM_CREATEDON"]);
                        objService.strStatus = Convert.ToString(sqlReader["INT_STATUS"]);
                        objService.strActionTakenBy = Convert.ToString(sqlReader["ACTIONTAKENBY"]);
                        objService.strActionTobeTakenBy = Convert.ToString(sqlReader["ACTIONTOBETAKENBY"]);
                        objService.intActionTobeTakenBy = Convert.ToInt32(sqlReader["ACTIONTOBETAKENBYID"]);
                        objService.strIndustryName = Convert.ToString(sqlReader["INDUSTRYNAME"]);
                        objService.intPaymentStatus = Convert.ToInt32(sqlReader["INT_PAYMENT_STATUS"]);
                        objService.strPaymentAmount = Convert.ToString(sqlReader["NUM_PAYMENT_AMOUNT"]);
                        objService.intStatus = Convert.ToInt32(sqlReader["STS"]);
                        objService.VCHINDUSTRIESNAME = Convert.ToString(sqlReader["VCH_INV_NAME"]);
                        objService.VCHCURRENTQUERYSTATUSDATE = Convert.ToString(sqlReader["CURRENT_QUERY_STATUS_DATE"]);
                        if (strAction == "V")
                        {
                            objService.strRemark = Convert.ToString(sqlReader["querydtl"]);
                            objService.strExcalationDays = Convert.ToString(sqlReader["dtm_EndOfORTPS_Timeline"]);
                            //Added By Pranay Kumar on 11-Sept-2017 for checking query status 
                            objService.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            //Ended By Pranay Kumar on 11-Sept-2017 for checking query status
                            //Added By Pranay Kumar on 21-Sept-2017 for Checking Current Query Status
                            objService.strQueryStatus = Convert.ToString(sqlReader["CURRENT_QUERY_STATUS"]);
                            //Ended By Pranay Kumar on 21-Sept-2017 for Checking Current Query Status
                        }
                        if (strAction == "E")
                        {
                            objService.strReferenceFilename = Convert.ToString(sqlReader["vchFileName"]);
                            objService.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            objService.str_ApplicationNo = Convert.ToString(sqlReader["VCH_APPLICATION_NO"]);
                        }
                        if (strAction == "M")
                        {
                            objService.strReferenceFilename = Convert.ToString(sqlReader["vchFileName"]);
                            objService.intQueryStatus = Convert.ToInt32(sqlReader["intQueryStatus"]);
                            objService.str_ApplicationNo = Convert.ToString(sqlReader["VCH_APPLICATION_NO"]);
                        }
                        list.Add(objService);
                    }

                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return list;
        }

        public string MappingConfigurationData(ServiceDetails objService)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@INTLEVELDETAILID", objService.intLevelDetailId);
                cmd.Parameters.AddWithValue("@P_DesignId", objService.intdesignationid);
                cmd.Parameters.AddWithValue("@PintDistrictId", objService.DistId);
                cmd.Parameters.AddWithValue("@P_Teheshilid", objService.intTahashilId);
                cmd.Parameters.AddWithValue("@P_DiscomeId", objService.intdiscomeid);
                cmd.Parameters.AddWithValue("@P_USERID", objService.userid);
                cmd.Parameters.AddWithValue("@P_MSGOUT", SqlDbType.VarChar);
                cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_MSGOUT"].Value.ToString();
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

        public List<EntityLayer.Service.ServiceDetails> ViewDynSrviceEditView(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            string strAction = "";
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_PanelDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "VW");
                cmd.Parameters.AddWithValue("@FORMID", objService.FormId);
                cmd.Parameters.AddWithValue("@PANELID", objService.PanelId);
                strAction = objService.strAction;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.ControlName = Convert.ToString(sqlReader["CNTNAME"]);
                        objService.FormId = Convert.ToInt32(sqlReader["INT_FORM_ID"]);
                        objService.ControlType = Convert.ToString(sqlReader["CNTTYPE"]);
                        objService.ControlLabel = sqlReader["CNTLABEL"].ToString();
                        objService.ControlSize = Convert.ToString(sqlReader["CNTSIZE"]);
                        objService.ControlReq = Convert.ToString(sqlReader["CNTREQUID"]);
                        objService.ControlId = Convert.ToInt32(sqlReader["INT_ID"]);
                        list.Add(objService);
                    }

                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return list;
        }
        public int ViewDynSrviceDelete(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            int intOut = 0;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_PanelDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "SD");
                cmd.Parameters.AddWithValue("@INT_ID", objService.INT_ID);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                intOut = cmd.ExecuteNonQuery();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return intOut;
        }
        public List<EntityLayer.Service.ServiceDetails> DynSrviceEditUpdate(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            string strAction = "";
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_PanelDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "SV");
                cmd.Parameters.AddWithValue("@INT_ID", objService.ControlId);
                strAction = objService.strAction;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.ControlName = Convert.ToString(sqlReader["PVCH_CONTROL_NAME"]);
                        objService.FormId = Convert.ToInt32(sqlReader["INT_FORM_ID"]);
                        objService.ControlType = Convert.ToString(sqlReader["PVCH_CONTROL_TYPE"]);
                        objService.ControlLabel = sqlReader["PVCH_LABEL_NAME"].ToString();
                        objService.ControlSize = Convert.ToString(sqlReader["PINT_LENGTH"]);
                        objService.ControlReq = Convert.ToString(sqlReader["PINT_REQVALIDATION"]);
                        objService.ControlId = Convert.ToInt32(sqlReader["INT_ID"]);
                        objService.INT_SEQUENCEID = Convert.ToString(sqlReader["INT_SEQUENCEID"]);
                        objService.INT_PANEL_ID = Convert.ToString(sqlReader["INT_PANEL_ID"]);
                        objService.PVCH_VALIDATIONTYPE = Convert.ToString(sqlReader["PVCH_VALIDATIONTYPE"]);
                        objService.PVCH_TOOLTIP = Convert.ToString(sqlReader["PVCH_TOOLTIP"]);
                        objService.PINT_AUTOMAPPING = Convert.ToString(sqlReader["PINT_AUTOMAPPING"]);
                        objService.PVCH_TEXTMODE = Convert.ToString(sqlReader["PVCH_TEXTMODE"]);
                        objService.PVCH_CSSCLASS = Convert.ToString(sqlReader["PVCH_CSSCLASS"]);
                        objService.PVCH_DATASOURCE = Convert.ToString(sqlReader["PVCH_DATASOURCE"]);
                        objService.PVCH_DATAVALUEFIELD = Convert.ToString(sqlReader["PVCH_DATAVALUEFIELD"]);
                        objService.PVCH_DATATEXTFIELD = Convert.ToString(sqlReader["PVCH_DATATEXTFIELD"]);
                        objService.PVCH_FILEALLOWED = Convert.ToString(sqlReader["PVCH_FILEALLOWED"]);
                        objService.PINT_MAXSIZE = Convert.ToString(sqlReader["PINT_MAXSIZE"]);
                        objService.PVCH_OPTION = Convert.ToString(sqlReader["PVCH_OPTION"]);
                        objService.PVCH_DEFAULTVALUE = Convert.ToString(sqlReader["PVCH_DEFAULTVALUE"]);
                        objService.PVCH_HEADINGTEXT = Convert.ToString(sqlReader["PVCH_HEADINGTEXT"]);

                        list.Add(objService);


                    }

                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return list;
        }
        public int DynamicSrviceUpdate(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            int intOutPut = 0;
            string strAction = "";
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_UPDATE_DYNAMICFORM";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_UPDATETYPE", Convert.ToString(objService.PVCH_CONTROL_TYPE));
                cmd.Parameters.AddWithValue("@P_INT_ID", Convert.ToInt32(objService.INT_ID));
                cmd.Parameters.AddWithValue("@P_INT_FORM_ID", Convert.ToInt32(objService.INT_FORM_ID));
                cmd.Parameters.AddWithValue("@P_INT_SEQUENCEID ", Convert.ToInt32(objService.INT_SEQUENCEID));
                cmd.Parameters.AddWithValue("@P_INT_PANEL_ID ", Convert.ToInt32(objService.INT_PANEL_ID));
                cmd.Parameters.AddWithValue("@P_PVCH_CONTROL_NAME ", Convert.ToString(objService.PVCH_CONTROL_NAME));
                cmd.Parameters.AddWithValue("@P_PVCH_CONTROL_TYPE ", Convert.ToString(objService.PVCH_CONTROL_TYPE));
                cmd.Parameters.AddWithValue("@P_PVCH_CONTROL_ID ", Convert.ToInt32(objService.PVCH_CONTROL_ID));
                cmd.Parameters.AddWithValue("@P_PVCH_LABEL_NAME ", Convert.ToString(objService.PVCH_LABEL_NAME));
                cmd.Parameters.AddWithValue("@P_PINT_LENGTH ", Convert.ToInt32(objService.PINT_LENGTH));
                cmd.Parameters.AddWithValue("@P_PVCH_VALIDATIONTYPE ", Convert.ToString(objService.PVCH_VALIDATIONTYPE));
                cmd.Parameters.AddWithValue("@P_PINT_REQVALIDATION ", Convert.ToInt32(objService.PINT_REQVALIDATION));
                cmd.Parameters.AddWithValue("@P_PVCH_TOOLTIP ", Convert.ToString(objService.PVCH_TOOLTIP));
                cmd.Parameters.AddWithValue("@P_PINT_AUTOMAPPING ", Convert.ToInt32(objService.PINT_AUTOMAPPING));
                cmd.Parameters.AddWithValue("@P_PVCH_TEXTMODE ", Convert.ToString(objService.PVCH_TEXTMODE));
                cmd.Parameters.AddWithValue("@P_PVCH_CSSCLASS ", Convert.ToString(objService.PVCH_CSSCLASS));
                cmd.Parameters.AddWithValue("@P_PVCH_DATASOURCE ", Convert.ToString(objService.PVCH_DATASOURCE));
                cmd.Parameters.AddWithValue("@P_PVCH_DATAVALUEFIELD ", Convert.ToString(objService.PVCH_DATAVALUEFIELD));
                cmd.Parameters.AddWithValue("@P_PVCH_DATATEXTFIELD ", Convert.ToString(objService.PVCH_DATATEXTFIELD));
                cmd.Parameters.AddWithValue("@P_PVCH_FILEALLOWED", Convert.ToString(objService.PVCH_FILEALLOWED));
                cmd.Parameters.AddWithValue("@P_PINT_MAXSIZE ", Convert.ToInt32(objService.PINT_MAXSIZE));
                cmd.Parameters.AddWithValue("@P_PVCH_OPTION ", Convert.ToString(objService.PVCH_OPTION));
                cmd.Parameters.AddWithValue("@P_PVCH_DEFAULTVALUE ", Convert.ToString(objService.PVCH_DEFAULTVALUE));
                cmd.Parameters.AddWithValue("@P_PVCH_HEADINGTEXT ", Convert.ToString(objService.PVCH_HEADINGTEXT));
                cmd.Parameters.AddWithValue("@P_PVCH_PLUGINID ", Convert.ToInt32(objService.PVCH_PLUGINID));
                cmd.Parameters.AddWithValue("@P_INT_DELETED_FLAG ", 0);
                strAction = objService.strAction;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                intOutPut = cmd.ExecuteNonQuery();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return intOutPut;
        }
        public int DynamicSrviceInsert(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            int intOutPut = 0;
            string strAction = "";
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_INSERT_DYNAMICFORM";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_UPDATETYPE", Convert.ToString(objService.PVCH_CONTROL_TYPE));
                //cmd.Parameters.AddWithValue("@P_INT_ID", Convert.ToInt32(objService.INT_ID));
                cmd.Parameters.AddWithValue("@P_INT_FORM_ID", Convert.ToInt32(objService.INT_FORM_ID));
                //cmd.Parameters.AddWithValue("@P_INT_SEQUENCEID ", Convert.ToInt32(objService.INT_SEQUENCEID));
                cmd.Parameters.AddWithValue("@P_INT_PANEL_ID", Convert.ToInt32(objService.INT_PANEL_ID));
                cmd.Parameters.AddWithValue("@P_PVCH_CONTROL_NAME", Convert.ToString(objService.PVCH_CONTROL_NAME));
                cmd.Parameters.AddWithValue("@P_PVCH_CONTROL_TYPE", Convert.ToString(objService.PVCH_CONTROL_TYPE));
                cmd.Parameters.AddWithValue("@P_PVCH_CONTROL_ID", Convert.ToString(objService.PVCH_CONTROL_ID));
                cmd.Parameters.AddWithValue("@P_PVCH_LABEL_NAME", Convert.ToString(objService.PVCH_LABEL_NAME));
                cmd.Parameters.AddWithValue("@P_PINT_LENGTH", Convert.ToInt32(objService.PINT_LENGTH));
                cmd.Parameters.AddWithValue("@P_PVCH_VALIDATIONTYPE", Convert.ToString(objService.PVCH_VALIDATIONTYPE));
                cmd.Parameters.AddWithValue("@P_PINT_REQVALIDATION", Convert.ToInt32(objService.PINT_REQVALIDATION));
                cmd.Parameters.AddWithValue("@P_PVCH_TOOLTIP", Convert.ToString(objService.PVCH_TOOLTIP));
                cmd.Parameters.AddWithValue("@P_PINT_AUTOMAPPING", Convert.ToInt32(objService.PINT_AUTOMAPPING));
                cmd.Parameters.AddWithValue("@P_PVCH_TEXTMODE", Convert.ToString(objService.PVCH_TEXTMODE));
                cmd.Parameters.AddWithValue("@P_PVCH_CSSCLASS", Convert.ToString(objService.PVCH_CSSCLASS));
                cmd.Parameters.AddWithValue("@P_PVCH_DATASOURCE", Convert.ToString(objService.PVCH_DATASOURCE));
                cmd.Parameters.AddWithValue("@P_PVCH_DATAVALUEFIELD", Convert.ToString(objService.PVCH_DATAVALUEFIELD));
                cmd.Parameters.AddWithValue("@P_PVCH_DATATEXTFIELD", Convert.ToString(objService.PVCH_DATATEXTFIELD));
                cmd.Parameters.AddWithValue("@P_PVCH_FILEALLOWED", Convert.ToString(objService.PVCH_FILEALLOWED));
                cmd.Parameters.AddWithValue("@P_PINT_MAXSIZE", Convert.ToInt32(objService.PINT_MAXSIZE));
                cmd.Parameters.AddWithValue("@P_PVCH_OPTION", Convert.ToString(objService.PVCH_OPTION));
                cmd.Parameters.AddWithValue("@P_PVCH_DEFAULTVALUE", Convert.ToString(objService.PVCH_DEFAULTVALUE));
                cmd.Parameters.AddWithValue("@P_PVCH_HEADINGTEXT", Convert.ToString(objService.PVCH_HEADINGTEXT));
                cmd.Parameters.AddWithValue("@P_PVCH_PLUGINID", Convert.ToString(objService.PVCH_PLUGINID));
                cmd.Parameters.AddWithValue("@P_INT_DELETED_FLAG", 0);
                strAction = objService.strAction;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                intOutPut = cmd.ExecuteNonQuery();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return intOutPut;
        }

        #region Added By Girija Sankar Sahoo on 04-Aug-2017
        #region Add promoter details
        public string UpdateServiceDet(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // cmd.CommandText = "USP_ApplicationMasterInsertUpdate_Static";
                cmd.CommandText = "USP_ApplicationMasterInsertUpdate";
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_PROPOSALID", objService.strProposalId);
                cmd.Parameters.AddWithValue("@P_VCH_INVESTOR_NAME", objService.strInvesterName);
                cmd.Parameters.AddWithValue("@P_SERVICEID", objService.intServiceId);
                cmd.Parameters.AddWithValue("@P_INT_STATUS", objService.intStatus);
                cmd.Parameters.AddWithValue("@P_INT_ACTION_TAKEN_BY", objService.intActionTakenBy);
                cmd.Parameters.AddWithValue("@P_INT_ACTION_TOBE_TAKEN_BY", objService.intActionTobeTakenBy);
                cmd.Parameters.AddWithValue("@P_INT_PAYMENT_STATUS", objService.intPaymentStatus);
                cmd.Parameters.AddWithValue("@P_NUM_PAYMENT_AMOUNT", objService.strPaymentAmount);
                cmd.Parameters.AddWithValue("@P_VCH_CERTIFICATE_FILENAME", objService.strCertificateFilename);
                cmd.Parameters.AddWithValue("@P_VCH_REFERENCE_DOC_NAME", objService.strReferenceFilename);
                cmd.Parameters.AddWithValue("@P_VCH_INSPECTION_DOC_NAME", objService.VCHINSPECTIONFILENAME);//Added By Manoj Kumar Behera
                cmd.Parameters.AddWithValue("@P_VCH_RESTORATION_DOC_NAME", objService.VCHRESTRATIONFILENAME);//Added By Manoj Kumar Behera
                cmd.Parameters.AddWithValue("@P_VCH_REMARK", objService.strRemark);
                cmd.Parameters.AddWithValue("@P_INT_ESCALATION_ID", objService.intEscalationId);
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_UNQ_KEY", objService.strApplicationUnqKey);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                return Str_RetValue;
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
        }
        #endregion
        #endregion

        public List<EntityLayer.Service.ServiceDetails> ViewServiceConfigurationData(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_Service_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@SERVICEID", objService.intServiceId);
                cmd.Parameters.AddWithValue("@DEPTID", objService.Deptid);
                cmd.Parameters.AddWithValue("@LINEDEPTID", objService.Typeid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.intConfigId = Convert.ToInt32(sqlReader["INTCONFIGID"]);
                        objService.intServiceId = Convert.ToInt32(sqlReader["INTSERVICEID"]);
                        objService.intEscalationId = Convert.ToInt32(sqlReader["INTESCALATIONLEVELID"]);
                        objService.Deptid = Convert.ToInt32(sqlReader["INTDEPTID"]);
                        objService.LocationId = Convert.ToInt32(sqlReader["INTLOCATIONID"]);
                        objService.DirectId = Convert.ToInt32(sqlReader["INTDIRECTORATEID"]);
                        objService.DivisionId = Convert.ToInt32(sqlReader["INTDIVISIONID"]);
                        objService.DistId = Convert.ToInt32(sqlReader["INTDISTID"]);
                        objService.userid = Convert.ToInt32(sqlReader["INTUSERDEPTID"]);
                        objService.strUsername = Convert.ToString(sqlReader["VCHFULLNAME"]);
                        objService.strExcalationDays = Convert.ToString(sqlReader["VCHESCALATIONDAYS"]);
                        objService.Typeid = Convert.ToString(sqlReader["INTTYPEID"]);
                        objService.desigid = Convert.ToString(sqlReader["INTDESIGID"]);
                        list.Add(objService);
                    }

                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return list;
        }
        public List<EntityLayer.Service.ServiceDetails> ViewDepartmentWiseServiceDetails(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_DepartmentWiseServiceDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_PROPOSALID", objService.intProposalId);
                cmd.Parameters.AddWithValue("@P_INTSECTORID", objService.intSectorId);
                cmd.Parameters.AddWithValue("@P_CUBSECTORID", objService.intSubSectorId);
                cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", objService.intCreatedBy);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.Deptid = Convert.ToInt32(sqlReader["INT_DEPARTMENT_ID"]);
                        // objService.strStatus = sqlReader["Status"].ToString();
                        //objService.intStatus = Convert.ToInt32(sqlReader["StatusId"]);
                        objService.strServiceName = sqlReader["Services"].ToString();
                        objService.strdeptname = sqlReader["Department"].ToString();
                        // objService.strProposalId = sqlReader["ProposalNo"].ToString();
                        objService.Str_Amount = Convert.ToString(sqlReader["NUM_PAYMENT_AMOUNT"]);
                        objService.Str_ExtrnalServiceUrl = sqlReader["VCH_EXTERNAL_SERVICE_URL"].ToString();
                        objService.Int_ServiceType = Convert.ToInt32(sqlReader["INT_SERVICE_TYPE"]);
                        objService.str_ApplicationStatus = sqlReader["ApplyStatus"].ToString();
                        objService.intExternalType = Convert.ToInt32(sqlReader["INT_EXTERNAL_TYPE"]); ///// Added by Sushant Jena On Dt.19-Feb-2021

                        list.Add(objService);
                    }

                }
                sqlReader.Close();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                sqlReader.Close();
            }

            return list;
        }

        public List<EntityLayer.Service.ServiceDetails> DeleteServiceConfigurationData(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_Service_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@ConfigId", objService.intConfigId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.intConfigId = Convert.ToInt32(sqlReader["INTCONFIGID"]);
                        objService.intServiceId = Convert.ToInt32(sqlReader["INTSERVICEID"]);
                        objService.intEscalationId = Convert.ToInt32(sqlReader["INTESCALATIONLEVELID"]);
                        objService.Deptid = Convert.ToInt32(sqlReader["INTDEPTID"]);
                        objService.LocationId = Convert.ToInt32(sqlReader["INTLOCATIONID"]);
                        objService.DirectId = Convert.ToInt32(sqlReader["INTDIRECTORATEID"]);
                        objService.DivisionId = Convert.ToInt32(sqlReader["INTDIVISIONID"]);
                        objService.DistId = Convert.ToInt32(sqlReader["INTDISTID"]);
                        objService.userid = Convert.ToInt32(sqlReader["INTUSERID"]);
                        objService.strUsername = Convert.ToString(sqlReader["VCHFULLNAME"]);
                        objService.strExcalationDays = Convert.ToString(sqlReader["VCHESCALATIONDAYS"]);

                        list.Add(objService);
                    }

                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return list;
        }

        #region Populate Dropdown
        /// <summary>
        /// Created By  :   AMit Sahoo
        /// Created On  :   9th Aug 2017
        /// To Populate District in Dropdownlist
        /// </summary>
        /// <param name="objInvestor"></param>
        /// <returns></returns>
        public IList<EntityLayer.Service.ServiceDetails> PopulateDistrict()
        {
            IList<EntityLayer.Service.ServiceDetails> objList = new List<EntityLayer.Service.ServiceDetails>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_Service_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "DIS");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.Service.ServiceDetails objInvDet = new EntityLayer.Service.ServiceDetails();
                        objInvDet.StrDistname = Convert.ToString(sqlReader["nvchDistrictName"]);
                        objInvDet.DistId = Convert.ToInt32(sqlReader["intDistrictCode"]);
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

        public IList<EntityLayer.Service.ServiceDetails> PopulateULB(int DistrictId)
        {
            IList<EntityLayer.Service.ServiceDetails> objList = new List<EntityLayer.Service.ServiceDetails>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_Service_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "ULB");
                cmd.Parameters.AddWithValue("@PintDistrictId", DistrictId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.Service.ServiceDetails objInvDet = new EntityLayer.Service.ServiceDetails();
                        objInvDet.StrDistname = Convert.ToString(sqlReader["vchULBName"]);
                        objInvDet.DistId = Convert.ToInt32(sqlReader["intULBId"]);
                        objInvDet.StrDireName = Convert.ToString(sqlReader["vchULBCode"]);
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

        public List<EntityLayer.Service.ServiceDetails> ViewApplicationDetails(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_DepartmentWiseServiceDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_PROPOSALID", "123333789");
                cmd.Parameters.AddWithValue("@P_INTSECTORID", objService.intSectorId);
                cmd.Parameters.AddWithValue("@P_CUBSECTORID", objService.intSubSectorId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.Deptid = Convert.ToInt32(sqlReader["INT_DEPARTMENT_ID"]);
                        // objService.strStatus = sqlReader["Status"].ToString();
                        //objService.intStatus = Convert.ToInt32(sqlReader["StatusId"]);
                        objService.strServiceName = sqlReader["Services"].ToString();
                        objService.strdeptname = sqlReader["Department"].ToString();
                        objService.strProposalId = sqlReader["ProposalNo"].ToString();
                        objService.Dec_Amount = Convert.ToDecimal(sqlReader["NUM_PAYMENT_AMOUNT"]);
                        objService.Str_ExtrnalServiceUrl = sqlReader["VCH_EXTERNAL_SERVICE_URL"].ToString();
                        objService.Int_ServiceType = Convert.ToInt32(sqlReader["INT_SERVICE_TYPE"]);
                        list.Add(objService);
                    }

                }
                sqlReader.Close();

            }
            catch (NullReferenceException ex) { throw ex; }
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

        #region Added By Prasun Kali on 22 August 2017
        #region Bind All Application Detail
        public List<ServiceDetails> GetAllApplicationDetails(ServiceDetails objServiceEntity)
        {
            List<EntityLayer.Service.ServiceDetails> objList = new List<EntityLayer.Service.ServiceDetails>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_GetApplicationDetails";

                cmd.Parameters.Clear();

                //cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", Int_GetUserID(objServiceEntity.strUsername));
                cmd.Parameters.AddWithValue("@P_VCH_MODE", "AD"); ///// This is Action Mode
                cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", objServiceEntity.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_VCH_FILTER_MODE", objServiceEntity.strFilterMode);
                cmd.Parameters.AddWithValue("@P_INT_DEPT_ID", objServiceEntity.Deptid);
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", objServiceEntity.str_ApplicationNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.Service.ServiceDetails objApllcantDetail = new EntityLayer.Service.ServiceDetails();
                        objApllcantDetail.str_Department = Convert.ToString(sqlReader["DepartmentName"]);
                        objApllcantDetail.str_ServicesName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                        objApllcantDetail.str_ApplicationNo = Convert.ToString(sqlReader["ApplicationNo"]);
                        objApllcantDetail.strProposalId = Convert.ToString(sqlReader["VCH_PROPOSALID"]);
                        objApllcantDetail.str_ApplicantName = Convert.ToString(sqlReader["ApplicantName"]);
                        objApllcantDetail.str_checkStatus = Convert.ToString(sqlReader["ChkExistng"]);
                        objApllcantDetail.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objApllcantDetail.str_UlbCode = Convert.ToString(sqlReader["intQueryStatus"]);
                        objApllcantDetail.str_QueryStatus = Convert.ToString(sqlReader["STRQueryStatus"]);
                        objApllcantDetail.Requestdate = Convert.ToString(sqlReader["DTM_CREATEDON"]);
                        objApllcantDetail.UpdatedOn = Convert.ToString(sqlReader["DTM_UpdatedOn"]);
                        objApllcantDetail.Str_NocFileName = Convert.ToString(sqlReader["VCH_Noc_FileName"]);
                        objApllcantDetail.strCertificateFilename = Convert.ToString(sqlReader["VCH_CERTIFICATE_FILENAME"]);
                        objApllcantDetail.str_ApplicationStatus = Convert.ToString(sqlReader["querydtl"]);
                        objApllcantDetail.Dec_Amount = Convert.ToDecimal(sqlReader["NUM_PAYMENT_AMOUNT"]);
                        objApllcantDetail.intPaymentStatus = Convert.ToInt32(sqlReader["INT_PAYMENT_STATUS"]);
                        objApllcantDetail.strStatus = Convert.ToString(sqlReader["vchStatusName"]);
                        objApllcantDetail.vchType = Convert.ToString(sqlReader["transCheck"]);
                        //Added BY Pritiprangya Pattanaik on 28-Nov-2017 to display remarks
                        objApllcantDetail.str_CorrectionRemark = Convert.ToString(sqlReader["Remarks"]);
                        objApllcantDetail.intStatus = Convert.ToInt32(sqlReader["int_status"]);
                        objApllcantDetail.ESIGNSTATUS = Convert.ToInt32(sqlReader["ESIGNSTATUS"].ToString());
                        //Added BY Pritiprangya Pattanaik on 28-Nov-2017 to display remarks
                        objApllcantDetail.decAppFee = Convert.ToDecimal(sqlReader["decApplicationFee"].ToString()); //// Added by Sushant Jena on Dt:-15-Apr-2020

                        objList.Add(objApllcantDetail);
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

        public List<ServiceDetails> GetAllDraftedApplicationDetails(string UserName)
        {

            List<EntityLayer.Service.ServiceDetails> objList = new List<EntityLayer.Service.ServiceDetails>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_GetApplicationDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", Int_GetUserID(UserName));
                cmd.Parameters.AddWithValue("@P_VCH_MODE", "DD");

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.Service.ServiceDetails objApllcantDetail = new EntityLayer.Service.ServiceDetails();
                        objApllcantDetail.str_Department = Convert.ToString(sqlReader["DepartmentName"]);
                        objApllcantDetail.str_ServicesName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                        objApllcantDetail.str_ApplicationNo = Convert.ToString(sqlReader["ApplicationNo"]);
                        objApllcantDetail.strProposalId = Convert.ToString(sqlReader["VCH_PROPOSALID"]);
                        objApllcantDetail.str_ApplicantName = Convert.ToString(sqlReader["ApplicantName"]);
                        objApllcantDetail.str_checkStatus = Convert.ToString(sqlReader["ChkExistng"]);
                        objApllcantDetail.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objApllcantDetail.str_UlbCode = Convert.ToString(sqlReader["intQueryStatus"]);
                        objApllcantDetail.str_QueryStatus = Convert.ToString(sqlReader["STRQueryStatus"]);
                        objApllcantDetail.Requestdate = Convert.ToString(sqlReader["DTM_CREATEDON"]);
                        objApllcantDetail.UpdatedOn = Convert.ToString(sqlReader["DTM_UpdatedOn"]);
                        objApllcantDetail.Str_NocFileName = Convert.ToString(sqlReader["VCH_Noc_FileName"]);
                        objApllcantDetail.strCertificateFilename = Convert.ToString(sqlReader["VCH_CERTIFICATE_FILENAME"]);
                        objApllcantDetail.str_ApplicationStatus = Convert.ToString(sqlReader["querydtl"]);
                        objApllcantDetail.Dec_Amount = Convert.ToDecimal(sqlReader["NUM_PAYMENT_AMOUNT"]);
                        objApllcantDetail.intPaymentStatus = Convert.ToInt32(sqlReader["INT_PAYMENT_STATUS"]);
                        objApllcantDetail.strStatus = Convert.ToString(sqlReader["vchStatusName"]);
                        objApllcantDetail.vchType = Convert.ToString(sqlReader["transCheck"]);
                        //Added BY Pritiprangya Pattanaik on 28-Nov-2017 to display remarks
                        objApllcantDetail.str_CorrectionRemark = Convert.ToString(sqlReader["Remarks"]);
                        objApllcantDetail.intStatus = Convert.ToInt32(sqlReader["int_status"]);
                        //Added BY Pritiprangya Pattanaik on 28-Nov-2017 to display remarks
                        objApllcantDetail.Str_ExtrnalServiceUrl = Convert.ToString(sqlReader["VCH_EXTERNAL_SERVICE_URL"]); //// Added by Sourav Kumar on date 15 mar 2021 to fetch external url
                        objApllcantDetail.intExternalType = Convert.ToInt32(sqlReader["INT_EXTERNAL_TYPE"]); //// Added by Sourav Kumar on date 15 mar 2021 to fetch type
                        objApllcantDetail.intHOACount = Convert.ToInt32(sqlReader["intHOACount"]);//// Added by Manoj Kumar on date 23 mar 2021 to fetch type
                        objApllcantDetail.vchTranscationNo = Convert.ToString(sqlReader["VCH_TRACKING_ID"]);//// Added by Sourav Kumar on date 23 mar 2021 to get group id

                        objList.Add(objApllcantDetail);
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
        #endregion


        #region Added By Girija Sankar Sahoo on 22-Aug-2017
        #region Add JsFile Upload
        public string UploadJs(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_UploadJS_DETAILS";
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_INT_DEPT_ID", objService.Deptid);
                cmd.Parameters.AddWithValue("@P_INT_SERVICE_ID", objService.intServiceId);
                cmd.Parameters.AddWithValue("@P_VCH_JS_FILENAME", objService.UploadJs);
                cmd.Parameters.AddWithValue("@P_INT_CREATED_BY", objService.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                return Str_RetValue;
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
        }
        #endregion
        #endregion

        public List<EntityLayer.Service.ServiceDetails> ViewUploadJS(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_UploadJS_DETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_INT_SERVICE_ID", objService.intServiceId);
                cmd.Parameters.AddWithValue("@P_INT_DEPT_ID", objService.Deptid);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.UploadJs = sqlReader["VCH_JS_FILENAME"].ToString();
                        list.Add(objService);
                    }

                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return list;
        }

        #region Added By PrasunKali on 26 aug 2017

        public string GetULBCode(string applicationNo)
        {

            EntityLayer.Service.ServiceDetails objUlbCode = new EntityLayer.Service.ServiceDetails();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_GetUlbCode";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@VCH_ApplicationNo", applicationNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {

                        objUlbCode.str_UlbCode = Convert.ToString(sqlReader["VCH_ULB_CODE"]);

                    }
                    sqlReader.Close();
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }
            return objUlbCode.str_UlbCode;
        }
        #endregion
        #region "FillProposalId"
        /// <summary>
        /// Radhika Rani Patri on 8/27/2017
        /// </summary>
        /// <param name="InvestorId"></param>
        /// <returns></returns>
        public List<EntityLayer.Service.ServiceDetails> FillProposalId(int InvestorId)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_ProposalView";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", 'P');
                cmd.Parameters.AddWithValue("@P_INVESTORID", InvestorId);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.strProposalId = sqlReader["vchProposalNo"].ToString();
                        list.Add(objService);
                    }

                }

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; sqlReader.Close(); }

            return list;
        }
        #endregion

        #region GetUserID
        /// <summary>
        /// Prasun Kali on 29/8/2017
        /// </summary>
        /// <param name="Str_USrName"></param>
        /// <returns></returns>
        public int Int_GetUserID(string Str_USrName)
        {
            int Int_UserID = 0;

            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_GetUserID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_UserName", Str_USrName);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {

                        Int_UserID = Convert.ToInt32(sqlReader["INT_INVESTOR_ID"]);

                    }

                }

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; sqlReader.Close(); }
            return Int_UserID;
        }
        #endregion

        public List<ServiceDetails> GetParticularApplicationDetails(ServiceDetails objService)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[USP_GetApplicationDetails]";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", objService.str_ApplicationNo);
                cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", Int_GetUserID(objService.strInvesterName));
                if (objService.Typeid == "S")
                {
                    cmd.Parameters.AddWithValue("@P_VCH_MODE", "SS");//IF redirect from (Portal/service)
                }
                else
                {
                    cmd.Parameters.AddWithValue("@P_VCH_MODE", "GD");//IF redirect from (applicationdetail.aspx)
                }


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {


                        objService.str_Department = Convert.ToString(sqlReader["DepartmentName"]);
                        objService.str_ServicesName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                        objService.str_ApplicationNo = Convert.ToString(sqlReader["ApplicationNo"]);
                        //objService.str_ApplicationStatus = Convert.ToString(sqlReader["ApplicationStatus"]);

                        objService.str_ApplicantName = Convert.ToString(sqlReader["ApplicantName"]);
                        objService.str_checkStatus = Convert.ToString(sqlReader["ChkExistng"]);
                        objService.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.str_UlbCode = Convert.ToString(sqlReader["intQueryStatus"]);

                        list.Add(objService);
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
        public List<EntityLayer.Service.ServiceDetails> TrakingDetailsOfTakeAction(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_TRAKINGDETAILS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_UNQ_KEY", objService.strApplicationUnqKey);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();

                        objService.strfullname = sqlReader["FULLNAME"].ToString();
                        objService.strRemark = sqlReader["REMARK"].ToString();
                        objService.strStatus = sqlReader["STATUSNM"].ToString();
                        objService.strTodate = Convert.ToString(sqlReader["CREATEDON"]);
                        objService.strFileUpload = sqlReader["FILENM"].ToString();
                        objService.strCertificateFilename = sqlReader["Approvaldoc"].ToString();
                        list.Add(objService);
                    }
                    sqlReader.Close();

                }
            }
            catch (NullReferenceException ex) { throw ex; }
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

        public string[] UpdateExistingServiceStatus(ServiceDetails objService)
        {
            string[] Str_RetValue = new string[2];
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_APPLICATION_CHECKSTATUS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_vchApplicationNo", objService.str_ApplicationNo);
                cmd.Parameters.AddWithValue("@P_intServiceId", objService.intServiceId);
                cmd.Parameters.AddWithValue("@P_vchProposalNo", objService.strProposalId);
                cmd.Parameters.AddWithValue("@P_intStatus", objService.intStatus);
                cmd.Parameters.AddWithValue("@P_vchStatusMsg", objService.strStatus);
                cmd.Parameters.AddWithValue("@P_vchCertificateUrl", objService.strCertificateFilename);
                cmd.Parameters.AddWithValue("@P_ACTION", objService.strAction);
                cmd.Parameters.Add("@P_RETURNID", SqlDbType.Int);
                cmd.Parameters.Add("@P_UpdatedOn", SqlDbType.VarChar, 100);
                cmd.Parameters["@P_RETURNID"].Direction = ParameterDirection.Output;
                cmd.Parameters["@P_UpdatedOn"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                Str_RetValue[0] = cmd.Parameters["@P_RETURNID"].Value.ToString();
                Str_RetValue[1] = cmd.Parameters["@P_UpdatedOn"].Value.ToString();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; conn.Close(); }
            return Str_RetValue;
        }

        #region "WHEN USER WANTS TO EXTEND THE QUERY DATE(Added By Pranay Kumar on 12-Sept-2017)"
        public int ExtendServiceQDate(string strAction, string strApplicationNo)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            int intStatus = 0;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SERVICE_QUERY_MANAGEMENTDTLS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", strAction);
                cmd.Parameters.AddWithValue("@PvchProposalNo", strApplicationNo);
                cmd.Parameters.AddWithValue("@P_INT_OUT", SqlDbType.VarChar);
                cmd.Parameters["@P_INT_OUT"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                intStatus = Convert.ToInt32(cmd.Parameters["@P_INT_OUT"].Value);

                return intStatus;
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
        }
        #endregion
        public List<ServiceDetails> GetDepartmentofParticularUser(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[USP_GET_APPLIED_DEPARTMENT]";
                cmd.Parameters.Clear();

                //cmd.Parameters.AddWithValue("@P_Int_CreatedBy", Int_GetUserID(objService.strInvesterName));
                cmd.Parameters.AddWithValue("@P_Int_CreatedBy", objService.intCreatedBy);//// Added by Sushant Jena On Dt.17-Aug-2018 (Added this line by commenting above line)
                cmd.Parameters.AddWithValue("@P_ACTION", "GD");

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();

                        objService.str_Department = Convert.ToString(sqlReader["DepartmentName"]);
                        objService.Deptid = Convert.ToInt32(sqlReader["INT_DEPARTMENT_ID"].ToString());

                        list.Add(objService);
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

        public List<ServiceDetails> GetAppliedApplicationNoofParticularUser(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[USP_GET_APPLIED_DEPARTMENT]";
                cmd.Parameters.Clear();

                //cmd.Parameters.AddWithValue("@P_Int_CreatedBy", Int_GetUserID(objService.strInvesterName));
                cmd.Parameters.AddWithValue("@P_Int_CreatedBy", objService.intCreatedBy);//// Added by Sushant Jena On Dt.06-Dec-2018 (Added this line by commenting above line)
                cmd.Parameters.AddWithValue("@P_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_DepartmentID", objService.Deptid);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.strApplicationUnqKey = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                        list.Add(objService);
                    }
                    sqlReader.Close();
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
                cmd = null;
            }
            return list;
        }

        public List<ServiceDetails> GetApplicationNoofParticularUserDepartmentWise(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[USP_GET_APPLIED_DEPARTMENT]";
                cmd.Parameters.Clear();

                //cmd.Parameters.AddWithValue("@P_Int_CreatedBy", Int_GetUserID(objService.strInvesterName));
                cmd.Parameters.AddWithValue("@P_Int_CreatedBy", objService.intCreatedBy);//// Added by Sushant Jena On Dt.06-Dec-2018 (Added this line by commenting above line)
                cmd.Parameters.AddWithValue("@P_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_DepartmentID", objService.Deptid);


                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();

                        objService.strApplicationUnqKey = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);


                        list.Add(objService);
                    }
                    sqlReader.Close();
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
                cmd = null;
            }
            return list;
        }

        public string UpdateApplicationStatus(ServiceDetails objService)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;
            try
            {
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_APPLICATION_CHECKSTATUS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_vchApplicationNo", objService.str_ApplicationNo);
                cmd.Parameters.AddWithValue("@P_ACTION", objService.strAction);

                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();

                        objService.strStatus = Convert.ToString(sqlReader["vchStatusName"]);



                    }
                    sqlReader.Close();
                }

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; conn.Close(); }
            return objService.strStatus;
        }

        public List<ServiceDetails> GetFilterApplicationDetails(ServiceDetails objService)
        {

            List<EntityLayer.Service.ServiceDetails> objList = new List<EntityLayer.Service.ServiceDetails>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_GetApplicationDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", Int_GetUserID(objService.str_ApplicantName));
                cmd.Parameters.AddWithValue("@P_VCH_MODE", objService.strAction);
                if (objService.strAction == "GAP")
                {
                    cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", objService.str_ApplicationNo);
                }
                else if (objService.strAction == "GAD")
                {
                    cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_NO", objService.Deptid);
                }
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.Service.ServiceDetails objApllcantDetail = new EntityLayer.Service.ServiceDetails();
                        objApllcantDetail.str_Department = Convert.ToString(sqlReader["DepartmentName"]);
                        objApllcantDetail.str_ServicesName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                        objApllcantDetail.str_ApplicationNo = Convert.ToString(sqlReader["ApplicationNo"]);
                        objApllcantDetail.strProposalId = Convert.ToString(sqlReader["VCH_PROPOSALID"]);
                        objApllcantDetail.str_ApplicantName = Convert.ToString(sqlReader["ApplicantName"]);
                        objApllcantDetail.str_checkStatus = Convert.ToString(sqlReader["ChkExistng"]);
                        objApllcantDetail.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objApllcantDetail.str_UlbCode = Convert.ToString(sqlReader["intQueryStatus"]);
                        objApllcantDetail.Requestdate = Convert.ToString(sqlReader["DTM_CREATEDON"]);
                        objApllcantDetail.UpdatedOn = Convert.ToString(sqlReader["DTM_UpdatedOn"]);
                        objApllcantDetail.Str_NocFileName = Convert.ToString(sqlReader["VCH_Noc_FileName"]);
                        objApllcantDetail.str_QueryStatus = Convert.ToString(sqlReader["STRQueryStatus"]);
                        objApllcantDetail.strCertificateFilename = Convert.ToString(sqlReader["VCH_CERTIFICATE_FILENAME"]);
                        objApllcantDetail.str_ApplicationStatus = Convert.ToString(sqlReader["querydtl"]);
                        objApllcantDetail.Dec_Amount = Convert.ToDecimal(sqlReader["NUM_PAYMENT_AMOUNT"]);
                        objApllcantDetail.intPaymentStatus = Convert.ToInt32(sqlReader["INT_PAYMENT_STATUS"]);
                        objApllcantDetail.strStatus = Convert.ToString(sqlReader["vchStatusName"]);
                        objApllcantDetail.vchType = Convert.ToString(sqlReader["transCheck"]);
                        objApllcantDetail.str_CorrectionRemark = Convert.ToString(sqlReader["Remarks"]);
                        objApllcantDetail.intStatus = Convert.ToInt32(sqlReader["int_status"]);
                        objList.Add(objApllcantDetail);
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

        public List<ServiceDetails> ORTPS_SMSConfiguration(ServiceDetails objService)
        {

            List<EntityLayer.Service.ServiceDetails> objList = new List<EntityLayer.Service.ServiceDetails>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Ortps_SMSConfiguration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PVCH_ACTION", objService.STRACTION);
                cmd.Parameters.AddWithValue("@PEmailid", objService.STRMAILCONTENT);
                cmd.Parameters.AddWithValue("@PSmsid", objService.STRSMSCONTENT);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        EntityLayer.Service.ServiceDetails objApllcantDetail = new EntityLayer.Service.ServiceDetails();
                        objApllcantDetail.STRSMSCONTENT = Convert.ToString(sqlReader["VCH_SMS_CONT1"]);
                        objApllcantDetail.STRMAILCONTENT = Convert.ToString(sqlReader["VCH_EMAIL_CONT1"]);
                        objApllcantDetail.strMobileno = Convert.ToString(sqlReader["VCH_MOBILE_NO"]);
                        objApllcantDetail.STRMOBILENO = Convert.ToString(sqlReader["VCH_MOBILE_NO"]);
                        objApllcantDetail.STREMAILID = Convert.ToString(sqlReader["VCH_EMAIL_ID"]);
                        objApllcantDetail.intConfigId = Convert.ToInt32(sqlReader["INT_ID"]);
                        objApllcantDetail.INT_SERVICEID = Convert.ToInt32(sqlReader["INT_SERVICE_ID"]);
                        objApllcantDetail.VCH_APPLICATION_UNQ_KEY = Convert.ToString(sqlReader["VCH_APPLICATION_NO"]);
                        objList.Add(objApllcantDetail);
                    }
                    sqlReader.Close();
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                cmd = null;
                sqlReader.Close();
            }
            return objList;
        }

        public List<EntityLayer.Service.ServiceDetails> StausReport(ServiceDetails obj)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_Service_Status_details";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_vchApplicationNo", obj.VCH_APPLICATION_NO);
                cmd.Parameters.AddWithValue("@P_vchBankTransStaus", obj.status);
                cmd.Parameters.AddWithValue("@PDTM_FROMDATE", obj.SMFrmDat);
                cmd.Parameters.AddWithValue("@PDTM_TODATE", obj.SMToDt);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.vchOrderNo = sqlReader["vchOrderNo"].ToString();
                        objService.VCH_APPLICATION_NO = sqlReader["vchApplicationNo"].ToString();
                        objService.vchChallanAmount = sqlReader["vchChallanAmount"].ToString();
                        objService.dtmCreatedOn = sqlReader["date"].ToString();
                        objService.status = sqlReader["status"].ToString();
                        objService.intServiceId = Convert.ToInt32(sqlReader["intServiceId"].ToString());
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
        public List<EntityLayer.Service.SMSAndMAILCls> SMSAndMailStatusReport(SMSAndMAILCls objSMSClsObj)
        {

            List<EntityLayer.Service.SMSAndMAILCls> list = new List<EntityLayer.Service.SMSAndMAILCls>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SmsAndMailReport";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PINT_DEPARTMENTID", objSMSClsObj.SMDEPTID);
                cmd.Parameters.AddWithValue("@PINT_SERVICEID", objSMSClsObj.SMServiceID);
                cmd.Parameters.AddWithValue("@PVCH_TYPE", objSMSClsObj.SMType);
                cmd.Parameters.AddWithValue("@PDTM_FROMDATE", objSMSClsObj.SMFrmDat);
                cmd.Parameters.AddWithValue("@PDTM_TODATE", objSMSClsObj.SMToDt);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        SMSAndMAILCls objService = new SMSAndMAILCls();
                        objService.SMApplicationNo = sqlReader["VCH_APPLICATION_NO"].ToString();
                        objService.SMMailStatus = sqlReader["MailStatus"].ToString();
                        objService.SMMobileNo = sqlReader["VCH_MOBILE_NO"].ToString();
                        objService.SMSmsStatus = sqlReader["Smsstatus"].ToString();
                        objService.SMType = sqlReader["VCH_TYPE"].ToString();
                        objService.SMMAILContent = sqlReader["VCH_EMAIL_CONT1"].ToString();
                        objService.SMSMSContent = sqlReader["VCH_SMS_CONT1"].ToString();
                        objService.SMAppStatus = sqlReader["Appstatus"].ToString();
                        objService.SMFrmDat = sqlReader["DTM_CREATED_ON"].ToString();
                        objService.SMEmail = sqlReader["VCH_EMAIL_ID"].ToString();
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

        public List<EntityLayer.Service.ServiceDetails> BindAllTable()
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "ATBL");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.ControlName = sqlReader["TBLNAME"].ToString();
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
        public List<EntityLayer.Service.ServiceDetails> BindAllColumn(string tableName)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PRO_SERVICE_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "ACOL");
                cmd.Parameters.AddWithValue("@FULLNAME", tableName);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        //objService.ControlId = Convert.ToInt32(sqlReader["CLMNAME"]);
                        objService.ControlName = sqlReader["CLMNAME"].ToString();
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

        public int newPanelInsert(ServiceDetails objService)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            int intout = 0;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_Panel_Operation";
                //cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "PA");
                cmd.Parameters.AddWithValue("@FORMID", objService.FormId);
                cmd.Parameters.AddWithValue("@Panel_Text", objService.PanelText);
                cmd.Parameters.AddWithValue("@Panel_name", objService.PanelName);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                intout = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
            }
            return intout;
        }
        public List<EntityLayer.Service.ServiceDetails> PanelView(ServiceDetails obj)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_Panel_Operation";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "PV");
                cmd.Parameters.AddWithValue("@FORMID", obj.FormId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.PanelName = sqlReader["VCH_PANELNAME"].ToString();
                        objService.PanelText = sqlReader["VCH_PANETEXT"].ToString();
                        objService.VCH_SERVICENAME = sqlReader["VCH_SERVICENAME"].ToString();
                        objService.PanelId = Convert.ToInt32(sqlReader["INT_PANELID"].ToString());
                        objService.FormId = Convert.ToInt32(sqlReader["INT_FORM_ID"].ToString());
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
        public List<EntityLayer.Service.ServiceDetails> PanelEdit(ServiceDetails obj)
        {

            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_Panel_Operation";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "PE");
                cmd.Parameters.AddWithValue("@Panel_id", obj.PanelId);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.PanelName = sqlReader["VCH_PANELNAME"].ToString();
                        objService.PanelText = sqlReader["VCH_PANETEXT"].ToString();
                        objService.VCH_SERVICENAME = sqlReader["VCH_SERVICENAME"].ToString();
                        objService.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"].ToString());
                        objService.intdeptid = Convert.ToInt32(sqlReader["INT_DEPARTMENT_ID"].ToString());
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

        #region Added by Sushant Jena for Panel Operation in Dynamic Control on Dt 16-May-2018

        public string Panel_Operation_AED(ServiceDetails objSrvcEntity)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "USP_PANEL_OPERATION_AED";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSrvcEntity.strAction);
                cmd.Parameters.AddWithValue("@P_INT_PANEL_ID", objSrvcEntity.PanelId);
                cmd.Parameters.AddWithValue("@P_INT_FORM_ID", objSrvcEntity.FormId);
                cmd.Parameters.AddWithValue("@P_VCH_PANEL_TEXT", objSrvcEntity.PanelText);
                cmd.Parameters.AddWithValue("@P_VCH_PANEL_NAME", objSrvcEntity.PanelName);

                SqlParameter par1 = new SqlParameter("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par1.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par1);

                cmd.ExecuteNonQuery();

                string strResult = cmd.Parameters["@P_OUT_MSG"].Value.ToString();

                return strResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        #endregion

        #region Add VIEW Service Master details
        /// <summary>
        /// Added BY Pritiprangya Pattanaik on 31-oct-2017 to add service details
        /// </summary>
        /// <param name="objService"></param>
        /// <returns></returns>
        public string AddServiceMasterDet(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SERVICEMASTER_AED";
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_INT_SERVICEID", objService.intServiceId);
                cmd.Parameters.AddWithValue("@P_INT_DEPARTMENT_ID", objService.intdeptid);
                cmd.Parameters.AddWithValue("@P_VCH_SERVICENAME", objService.strServiceName);
                cmd.Parameters.AddWithValue("@P_VCH_SERVICEDESCRIPTION", objService.strRemark);
                cmd.Parameters.AddWithValue("@P_INT_PAYMENT_REQUIRED", objService.intPaymentStatus);
                cmd.Parameters.AddWithValue("@P_NUM_PAYMENT_AMOUNT", objService.strPaymentAmount);
                cmd.Parameters.AddWithValue("@P_INT_SERVICE_TYPE", objService.Int_ServiceType);
                cmd.Parameters.AddWithValue("@P_INT_DELETED_FLAG", objService.strStatus);
                cmd.Parameters.AddWithValue("@P_int_OLTPSTIMELINE", objService.strExcalationDays);
                cmd.Parameters.AddWithValue("@P_VCH_ALIAS_NAME", objService.strServiceAliasName);
                cmd.Parameters.AddWithValue("@P_INT_CategoryType", objService.intServiceCategory);
                cmd.Parameters.AddWithValue("@P_VCH_EXTERNAL_SERVICEURL", objService.Str_ExtrnalServiceUrl);
                cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", objService.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_INT_EXTERNAL_TYPE", objService.intExternalType);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                return Str_RetValue;
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
        }

        /// <summary>
        /// Added BY Pritiprangya Pattanaik on 31-oct-2017 to view service details
        /// </summary>
        /// <param name="objService"></param>
        /// <returns></returns>
        public List<ServiceDetails> ViewServiceMasterDet(ServiceDetails objService)
        {

            List<EntityLayer.Service.ServiceDetails> objList = new List<EntityLayer.Service.ServiceDetails>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SERVICEMASTER_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_INT_SERVICEID", objService.intServiceId);
                cmd.Parameters.AddWithValue("@P_INT_DEPARTMENT_ID", objService.intdeptid);
                cmd.Parameters.AddWithValue("@P_VCH_SERVICENAME", objService.strServiceName);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new EntityLayer.Service.ServiceDetails();
                        objService.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.intdeptid = Convert.ToInt32(sqlReader["INT_DEPARTMENT_ID"]);
                        objService.str_Department = Convert.ToString(sqlReader["nvchLevelName"]);
                        objService.strServiceName = Convert.ToString(sqlReader["VCH_SERVICENAME"]);
                        objService.strRemark = Convert.ToString(sqlReader["VCH_SERVICEDESCRIPTION"]);
                        objService.intPaymentStatus = Convert.ToInt32(sqlReader["INT_PAYMENT_REQUIRED"]);
                        objService.strPaymentAmount = Convert.ToString(sqlReader["NUM_PAYMENT_AMOUNT"]);
                        objService.Int_ServiceType = Convert.ToInt32(sqlReader["INT_SERVICE_TYPE"]);
                        objService.intStatus = Convert.ToInt32(sqlReader["INT_DELETED_FLAG"]);
                        objService.strExcalationDays = Convert.ToString(sqlReader["int_OLTPSTIMELINE"]);
                        objService.strServiceAliasName = Convert.ToString(sqlReader["VCH_ALIAS_NAME"]);
                        objService.Str_ExtrnalServiceUrl = Convert.ToString(sqlReader["VCH_EXTERNAL_SERVICE_URL"]);
                        objService.intServiceCategory = Convert.ToInt32(sqlReader["INT_CategoryType"]);
                        objService.intExternalType = Convert.ToInt32(sqlReader["INT_EXTERNAL_TYPE"]);
                        objList.Add(objService);
                    }
                    sqlReader.Close();
                }
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                cmd = null;
                sqlReader.Close();
            }
            return objList;
        }
        #endregion

       

        public int DynPanelDelete(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            int intOut = 0;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_PRO_PanelDetails";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "SD");
                cmd.Parameters.AddWithValue("@INT_ID", objService.INT_ID);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                intOut = cmd.ExecuteNonQuery();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return intOut;
        }

        public List<ServiceDetails> StatusSchedule(ServiceDetails obj)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_UPDATE_STATUS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", "G");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService = new ServiceDetails();
                        objService.VCH_APPLICATION_UNQ_KEY = sqlReader["VCH_APPLICATION_UNQ_KEY"].ToString();
                        objService.INT_SERVICEID = Convert.ToInt32(sqlReader["INT_SERVICEID"].ToString());
                        objService.DTM_CREATEDON = sqlReader["DTM_CREATEDON"].ToString();
                        objService.Int_ServiceType = Convert.ToInt32(sqlReader["INT_SERVICE_TYPE"].ToString());
                        objService.intCreatedBy = Convert.ToInt32(sqlReader["INT_CREATEDBY"].ToString());
                        objService.strProposalId = sqlReader["VCH_PROPOSALID"].ToString();
                        objService.str_UlbCode = sqlReader["VCH_ULB_CODE"].ToString();
                        list.Add(objService);
                    }
                }
                sqlReader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cmd = null;
            }
            return list;
        }

        public List<ServiceDetails> PaymentOrderDetails(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            int intOut = 0;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PaymentOrder_Details";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", objService.STRACTION);
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_UNQ_KEY", objService.strApplicationUnqKey);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService1 = new ServiceDetails();
                        objService1.strApplicationUnqKey = sqlReader["vchApplicationNo"].ToString();
                        objService1.dtmCreatedOn = sqlReader["dtmOrderDate"].ToString();
                        objService1.strStatus = sqlReader["PaymentStatus"].ToString();
                        objService1.vchOrderNo = sqlReader["vchOrderNo"].ToString();
                        objService1.vchAmount = sqlReader["vchChallanAmount"].ToString();
                        list.Add(objService1);
                    }
                }
                sqlReader.Close();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return list;
        }
        public List<ServiceDetails> FillApplicationNo()
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            int intOut = 0;
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_PaymentOrder_Details";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", "A");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        ServiceDetails objService1 = new ServiceDetails();
                        objService1.strApplicationUnqKey = sqlReader["VCH_APPLICATION_UNQ_KEY"].ToString();

                        list.Add(objService1);
                    }
                }
                sqlReader.Close();

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return list;
        }

        public string AddServiceChallan(string Action, string strChallanxml, string ApplicationNo, int CretedBy, string vchOrderNo, string AmtPaid, string Overdue)
        {

            string res = "0";

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SERVICE_CHALLAN_ORDER_ADD";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", Action);
                cmd.Parameters.AddWithValue("@P_XML_Data", strChallanxml);
                cmd.Parameters.AddWithValue("@P_Application", ApplicationNo);
                cmd.Parameters.AddWithValue("@P_CreatedBy", CretedBy);
                cmd.Parameters.AddWithValue("@P_OrderNo", vchOrderNo);
                cmd.Parameters.AddWithValue("@P_TotalAmt_Tobepaid", AmtPaid);
                cmd.Parameters.AddWithValue("@P_Payment_Overdue", Overdue);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                return Str_RetValue;
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
            return res;
        }
        public List<EntityLayer.Service.ServiceDetails> GetAllChalanDetails(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SERVICE_CHALLAN_ORDER_ADD";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PvchAction", "V");
                cmd.Parameters.AddWithValue("@P_Application", objService.str_ApplicationNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.intProId2 = Convert.ToString(sqlReader["intProId2"]);
                        objService.vchChallanNo = Convert.ToString(sqlReader["vchChallanNo"]);
                        objService.vchTranscationNo = Convert.ToString(sqlReader["vchTranscationNo"]);
                        objService.vchAmount = Convert.ToString(sqlReader["vchAmount"]);
                        objService.vchChallanFile = Convert.ToString(sqlReader["vchChallanFile"]);
                        objService.vchChallanDate = Convert.ToString(sqlReader["vchChallanDate"]);
                        objService.TransMode = Convert.ToString(sqlReader["TransMode"]);
                        list.Add(objService);
                    }

                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return list;
        }

        public string getServiceId(string applicationNo)
        {
            string serviceId = "";
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_FeedBackBind";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Action", "G");
                cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNIQUEID", applicationNo);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        serviceId = Convert.ToString(sqlReader["INT_SERVICEID"]);
                    }

                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; conn.Close(); }

            return serviceId;
        }

        public List<fileCheckCls> AllFileView(fileCheckCls obj)
        {
            List<EntityLayer.Service.fileCheckCls> list = new List<EntityLayer.Service.fileCheckCls>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_FileUpload_Check";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ACTION", obj.PVCH_ACTIONCODE);
                cmd.Parameters.AddWithValue("@P_FORMID", obj.PVCH_FORMID);
                cmd.Parameters.AddWithValue("@P_CLMNAME", obj.PVCH_COLUMNNAME);
                cmd.Parameters.AddWithValue("@P_VCHAPPLICATIONKEY", obj.PVCH_APPLICATIONKEY);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    if (obj.PVCH_ACTIONCODE == "V")
                    {
                        while (sqlReader.Read())
                        {
                            fileCheckCls objService = new fileCheckCls();
                            objService.PVCH_CONTROL_NAME = sqlReader["PVCH_CONTROL_NAME"].ToString();
                            objService.PVCH_LABEL_NAME = sqlReader["PVCH_LABEL_NAME"].ToString();
                            list.Add(objService);
                        }
                    }
                    else if (obj.PVCH_ACTIONCODE == "C")
                    {
                        while (sqlReader.Read())
                        {
                            fileCheckCls objService = new fileCheckCls();
                            objService.VCH_FILENAME = sqlReader["fileName"].ToString();
                            list.Add(objService);
                        }

                    }
                }
                sqlReader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                cmd = null;
            }
            return list;
        }



        public List<ServiceDetails> eSineDetails(ServiceDetails objService)
        {


            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlDataReader sqlReader = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_eSignIntegration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.action);
                cmd.Parameters.AddWithValue("@P_APPLICATIONKEY", objService.VCH_APPLICATION_UNQ_KEY);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();
                        objService.VCH_APPLICATION_UNQ_KEY = Convert.ToString(sqlReader["VCH_APPLICATIONKEY"]);
                        objService.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.Dec_Amount = Convert.ToDecimal(sqlReader["NUM_PAYMENT_AMOUNT"]);
                        objService.vchAccountHead = Convert.ToString(sqlReader["VCH_ACCOUNTHEAD"]);
                        objService.intCreatedBy = Convert.ToInt32(sqlReader["INT_CREATEDBY"]);

                        list.Add(objService);
                    }

                }
                sqlReader.Close();
            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally { cmd = null; }

            return list;
        }
        public string eSignUpdate(fileCheckCls objService)
        {
            List<EntityLayer.Service.fileCheckCls> list = new List<EntityLayer.Service.fileCheckCls>();
            SqlDataReader sqlReader = null;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand();
            int intOut = 0;
            string res = "0";
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_eSignIntegration";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.PVCH_ACTIONCODE);
                cmd.Parameters.AddWithValue("@P_APPLICATIONKEY", objService.PVCH_APPLICATIONKEY);
                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                return Str_RetValue;
                res = Str_RetValue;
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
            return res;
        }

        public string AddTransactionDetails(ServiceDetails objSer)
        {

            string res = "0";

            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_TRANSACTIONDETAILS";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objSer.action);
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_UNQ_KEY", objSer.str_ApplicationNo);
                cmd.Parameters.AddWithValue("@P_NUM_PAYMENT_AMOUNT", Convert.ToDouble(objSer.Dec_Amount));
                cmd.Parameters.AddWithValue("@P_VCH_ACCOUNTHEAD", objSer.vchAccountHead);
                cmd.Parameters.AddWithValue("@INT_CREATEDBY", objSer.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_SERVICEID", objSer.intServiceId);
                cmd.Parameters.AddWithValue("@P_DEC_APPLICATION_FEE", objSer.decAppFee);

                SqlParameter par;
                par = cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                res = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                // return Str_RetValue;
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
            return res;
        }
        public List<ServiceDetails> PCBAutoUpdate(ServiceDetails objService)
        {
            List<EntityLayer.Service.ServiceDetails> list = new List<EntityLayer.Service.ServiceDetails>();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader sqlReader = null;

            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "[USP_AUTO_SERVICE_STATUSUPDATE]";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_ACTION", objService.strAction);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        objService = new ServiceDetails();

                        objService.strApplicationUnqKey = Convert.ToString(sqlReader["VCH_APPLICATION_UNQ_KEY"]);
                        objService.intServiceId = Convert.ToInt32(sqlReader["INT_SERVICEID"]);
                        objService.strProposalId = Convert.ToString(sqlReader["VCH_PROPOSALID"]);
                        list.Add(objService);
                    }
                    sqlReader.Close();
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
                cmd = null;
            }
            return list;
        }

        #region add by anil sahoo

        public DataSet TrackServiceAppliactionDetail(TrackService objService)   //  For Application Track Service 
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_TrackServiceAppStatus";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.StrAction);
                cmd.Parameters.AddWithValue("@P_VCH_APP_ID", objService.Str_Application_Id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                cmd = null;
            }
            return ds;
        }

        #endregion 

        /// <summary>
        /// Added by Sushant Jena On Dt:-11-Jan-2022
        /// To get all service applications against Group/Tracking Id
        /// </summary>
        /// <param name="strTrackingId"></param>
        /// <returns></returns>
        public DataTable GetApplicationByTrackingId(string strTrackingId)
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
                cmd.CommandText = "USP_GetApplicationDetails";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_VCH_MODE", "GAT");
                cmd.Parameters.AddWithValue("@P_VCH_TRACKING_ID", strTrackingId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataSet ViewServiceMasterByDepartMentID(ServiceDetails objService) //Added New Service 14-04-22
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SERVICEMASTER_AED";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_INT_DEPARTMENT_ID", objService.intdeptid);
                objDa.SelectCommand = cmd;
                objDa.Fill(objds);

            }
            catch (NullReferenceException ex) { throw ex; }
            catch (Exception ex)
            { throw ex; }
            finally
            {
                cmd = null;

            }
            return objds;
        }

        public int UpdateServiceByServiceId(ServiceDetails objService) ////Added New Service 14-04-22
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_SERVICEMASTER_AED";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_SERVICENAME", objService.strServiceName);
                cmd.Parameters.AddWithValue("@P_INT_CategoryType", objService.intServiceCategory);
                cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", objService.intCreatedBy);
                cmd.Parameters.AddWithValue("@P_INT_SERVICEID", objService.intServiceId);               
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.Int);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Str_RetValue = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return Convert.ToInt32(Str_RetValue.ToString());
        }

        // Function to get the view of service query date
        //Add By Debiprasanna
        public DataTable ViewQueryDateUpdate(ServiceDetails objService) 
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
                cmd.CommandText = "USP_ApplicationMasterInsertUpdate";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", "SQV");
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_UNQ_KEY", objService.strApplicationUnqKey);
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
        // Function to get the Update of Service Query Date
        //Add By Debiprasanna
        public string ServiceUpdateQueryDate(ServiceDetails objService) 
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_ApplicationMasterInsertUpdate";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objService.strAction);
                cmd.Parameters.AddWithValue("@P_VCH_APPLICATION_UNQ_KEY", objService.strApplicationUnqKey);
                cmd.Parameters.AddWithValue("@P_DTM_QUERY_RAISE", objService.QueryRasied);
                cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
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

    }
}

    