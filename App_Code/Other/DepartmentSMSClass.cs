using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityLayer.Service;
using System.Data;
using System.Data.SqlClient;
using Common;
/// <summary>
/// Summary description for DepartmentSMSClass
/// </summary>
public class DepartmentSMSClass
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    string result;
    CommonHelperCls objCmnHlpr = new CommonHelperCls();
	public DepartmentSMSClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string DepartmentSendSms(ServiceDetails obj)
    {

        SqlCommand cmd = new SqlCommand();
        SqlDataReader sqlReader = null;

        try
        {
           
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "USP_DEPARTMENTHEAD_SMS";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@P_ServiceID", obj.INT_SERVICEID);  
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                sqlReader = cmd.ExecuteReader();
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        obj.strUsername = sqlReader["HOD_NAME"].ToString();
                        obj.strMobileno = sqlReader["VCH_MOBILE"].ToString();
                        obj.Email = sqlReader["VCH_EMAIL"].ToString();
                        string[] email = new string[1];
                        email[0] = obj.Email.ToString();
                        objCmnHlpr.SendSms(obj.strMobileno, obj.smsContent);
                        objCmnHlpr.sendMail(obj.strSubject, obj.strBody, email, true);
                    }
                    sqlReader.Close();
                }
                conn.Close();
           
        }
        catch (NullReferenceException ex) { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally { cmd = null; }
        return result;
    
    }
}