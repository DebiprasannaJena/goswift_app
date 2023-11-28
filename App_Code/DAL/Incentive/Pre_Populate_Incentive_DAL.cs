using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Common.Persistence.Data;

/// <summary>
/// Summary description for Pre_Populate_Incentive_DAL
/// </summary>
public class Pre_Populate_Incentive_DAL
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    string Str_RetValue = "";

    string StrSql = string.Empty;
    int int_Return_Val = 0;
    string str_Return_Val = string.Empty;
    object param = null;

    public Pre_Populate_Incentive_DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataSet Pre_Populate_Inct_Data(Pre_Populate_Incentive_Entity objOGEntity)
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();
        try
        {
            objCommand.CommandText = "USP_INCT_PRE_POPULATED_DATA";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;
            objCommand.Parameters.AddWithValue("@P_USER_ID", objOGEntity.strUserID);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objCommand = null;
        }
        return objds;
    }
}