using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for ExternalServiceIntegration
/// </summary>
public class ExternalServiceIntegration
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    string Str_RetValue = "";
    public string ExternalServiceData(string ACTION, int SERVICEID, string PROPOSALNO,int CREATEDBY,string PanNo)
    {
         
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_GENERATE_APPLICATIONNUMBER";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_ACTION", ACTION);
            cmd.Parameters.AddWithValue("@P_SERVICEID", SERVICEID);
            cmd.Parameters.AddWithValue("@P_PROPOSALNO", PROPOSALNO);
            cmd.Parameters.AddWithValue("@P_CreatedBy", CREATEDBY);
            cmd.Parameters.AddWithValue("@P_PanNo", PanNo);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    Str_RetValue = Convert.ToString(sqlReader["ApplicationKey"]);
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
        return Str_RetValue;
    }

    //Added by suroj kumar pradhan on 13-09-17 To Fetch UserDetail
    public List<Serviceinfo> GetUserDetail(Serviceinfo objService)
    {
        List<Serviceinfo> objServicelist=new List<Serviceinfo>();
        SqlDataReader sqlReader = null;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_GENERATE_APPLICATIONNUMBER";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_ACTION", objService.vchAction);
            cmd.Parameters.AddWithValue("@P_CreatedBy", objService.intCreatedBy);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    objService.vchPassword = Convert.ToString(sqlReader["VCHMOBILENO"]);
                    objService.vchAddress = Convert.ToString(sqlReader["VCHADDRESS"]);
                    objService.vchCompanyName = Convert.ToString(sqlReader["VCHCOMAPNYNAME"]);
                    objService.vchDateOfBirth = Convert.ToString(sqlReader["DOB"]);
                    objService.vchEmailId = Convert.ToString(sqlReader["VCHEMAIL"]);
                    objService.vchFirstName = Convert.ToString(sqlReader["VCHFIRSTNAME"]);
                    objService.vchMiddleName = Convert.ToString(sqlReader["VCHMIDDLENAME"]);
                    objService.vchLastName = Convert.ToString(sqlReader["VCHLASTNAME"]);
                    objService.vchPhoneNumber = Convert.ToString(sqlReader["VCHPHONENO"]);
                    objService.vchSalutation = Convert.ToString(sqlReader["VCHSALUTATION"]);
                    objServicelist.Add(objService);
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
        return objServicelist;
    }


    //Added by suroj kumar pradhan to register new user

    public class Serviceinfo
    {
        public string vchPassword { get; set; }
        public string vchAction { get; set; }
        public int intCreatedBy { get; set; }
        public int intUserid { get; set; }
        public string vchAddress { get; set;}
        public string vchCompanyName { get; set; }
        public string vchDateOfBirth { get; set; }
        public string vchEmailId { get; set; }
        public string vchFirstName { get; set; }
        public string vchMiddleName { get; set; }
        public string vchLastName { get; set; }
        public string vchMobileNo { get; set; }
        public string vchPhoneNumber { get; set; }
        public string vchSalutation { get; set; }
        public string vchUserSWPCode { get; set; }
        public string vchUserName { get; set; }
    }
}