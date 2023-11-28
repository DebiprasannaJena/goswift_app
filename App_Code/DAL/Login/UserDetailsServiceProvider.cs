/*
 * Created By : Ritika lath
 * Created On : 10th May 2018
 * Class name : UserDetailsServiceProvider
 * File name : UserDetailsServiceProvider.cs
 */

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

/// <summary>
/// Summary description for UserDetailsServiceProvider
/// </summary>
public class UserDetailsServiceProvider : IUserDetailsServiceProvider
{
    #region "Member Variable"
    string ConnectionString = "AdminAppConnectionProd";
    SqlConnection gSqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    #endregion

    public override Dictionary<int, string> GetUserList(ManageUserDetails objUserDetails)
    {
        Dictionary<int,string> dcUser= new Dictionary<int, string>();
        SqlCommand objCommand = new SqlCommand();
        objCommand.CommandText = "USP_ManageUserDetailsByAdmin";
        objCommand.CommandType = CommandType.StoredProcedure;
        objCommand.Connection = gSqlConn;
        objCommand.Parameters.AddWithValue("@pChrAction", objUserDetails.strAction);
        SqlDataAdapter objDa = new SqlDataAdapter(objCommand);
        DataTable objDt = new DataTable();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        objDa.Fill(objDt);
        if (objDt != null && objDt.Rows.Count > 0)
        {
            for (int cnt=0; cnt < objDt.Rows.Count; cnt++)
            {
                dcUser.Add(Convert.ToInt32(objDt.Rows[cnt]["intUserId"]), objDt.Rows[cnt]["vchUsername"].ToString());
            }
        }
        gSqlConn.Close();
        return dcUser;
    }

    public override int ManageUser_AED(ManageUserDetails objUserDetails)
    {
        int intRetValue = 0;
        SqlCommand objCommand = new SqlCommand();
        objCommand.CommandText = "USP_ManageUserDetailsByAdmin";
        objCommand.CommandType = CommandType.StoredProcedure;
        objCommand.Connection = gSqlConn;
        objCommand.Parameters.AddWithValue("@pChrAction", objUserDetails.strAction);
        objCommand.Parameters.AddWithValue("@pIntUserId", objUserDetails.intUserId);
        objCommand.Parameters.AddWithValue("@pIntNoOfLockHours", objUserDetails.intNoOfLockHours);
        SqlParameter objParam = new SqlParameter()
        {
            ParameterName = "@pIntOut",
            Direction = ParameterDirection.Output,
            SqlDbType = SqlDbType.Int
        };
        objCommand.Parameters.Add(objParam);
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }

        objCommand.ExecuteNonQuery();
        object obj = new object();
        obj = objParam.Value;
        if (obj != null && obj != DBNull.Value)
        {
            intRetValue = Convert.ToInt32(obj);
        }
        gSqlConn.Close();
        return intRetValue;
    }
}