using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Common.Persistence.Data;

/// <summary>
/// Summary description for ManageMailDataLayer
/// </summary>
public class ManageMailDataLayer
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    object param = null;

    string Str_RetValue = "";
    public ManageMailDataLayer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet Manage_Mail_View(ManageMailEntityLayer objEntity)
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();
        try
        {
            objCommand.CommandText = "USP_MANAGE_MAIL_SCHEDULER_VIEW";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", objEntity.strAction);
            objCommand.Parameters.AddWithValue("@P_INT_DESIGNATION_ID", objEntity.intDesgId);
            objCommand.Parameters.AddWithValue("@P_CH_MAIL_STATUS", objEntity.strMailStatus);

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
    public string Manage_Mail_AED(ManageMailEntityLayer objEntity)
    {
        try
        {
            object[] objArray = new object[]
                {
                 "@P_VCH_ACTION", objEntity.strAction
                ,"@P_CH_MAIL_STATUS", objEntity.strMailStatus
                ,"@P_VCH_IDS", objEntity.strIds
                ,"@P_INT_DESIGNATION_ID", objEntity.intDesgId
                ,"@P_VCH_CC_MAIL_ID", objEntity.strCcMailId
                ,"@P_CH_CC_ENABLE_STATUS", objEntity.strCcEnableStatus
                ,"@P_VCH_BCC_MAIL_ID", objEntity.strBccMailId
                ,"@P_CH_BCC_ENABLE_STATUS", objEntity.strBccEnableStatus
                ,"@P_INT_CREATED_BY", objEntity.intCreatedBy
                ,"@P_INT_SL_NO", objEntity.intSerialNo
                ,"@P_VCH_MAIL_ID", objEntity.strMailId
                ,"@P_VCH_SPAM_MODE", objEntity.strSpamMode
                ,"@P_VCH_SPAM_TEXT", objEntity.strSpamText
                ,"@P_VCH_SUBACTION",objEntity.strSubAction // ADD ANIL SAHOO
                ,"@P_INT_USERID",objEntity.intUserId  // Add anil sahoo
                ,"@P_OUT_MSG", "OUT"
                };

            int intOutput = SqlHelper.ExecuteNonQuery(conn.ConnectionString, "USP_MANAGE_MAIL_SCHEDULER_AED", out param, objArray);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }
        return param.ToString();
    }

    #region add anil sahoo

    public string Edit_MailData(ManageMailEntityLayer objPolicy)
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
            cmd.CommandText = "USP_MANAGE_MAIL_SCHEDULER_AED";

            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@P_VCH_ACTION", objPolicy.strAction);
            cmd.Parameters.AddWithValue("@P_INT_USERID", objPolicy.intUserId);
            cmd.Parameters.AddWithValue("@P_INT_CREATED_BY", objPolicy.intCreatedBy);
            cmd.Parameters.AddWithValue("@P_CH_CC_ENABLE_STATUS", objPolicy.strCcEnableStatus);
            cmd.Parameters.AddWithValue("@P_CH_BCC_ENABLE_STATUS", objPolicy.strBccEnableStatus);
            cmd.Parameters.AddWithValue("@P_VCH_CC_MAIL_ID", objPolicy.strCcMailId);
            cmd.Parameters.AddWithValue("@P_VCH_BCC_MAIL_ID", objPolicy.strBccMailId);

            cmd.Parameters.AddWithValue("@P_OUT_MSG", SqlDbType.VarChar);
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
            cmd.Dispose();
        }
        return Str_RetValue;
    }
    #endregion
}