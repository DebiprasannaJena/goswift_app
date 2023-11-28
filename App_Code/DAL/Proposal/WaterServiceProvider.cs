using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using EntityLayer.Mastersector;
using System.Configuration;

/// <summary>
/// Summary description for WaterServiceProvider
/// </summary>
public class WaterServiceProvider
{
    public WaterServiceProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    CSMPDK_3_0.CommonDLL ObjCmnDll = new CSMPDK_3_0.CommonDLL();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
    public string AddWaterAllotmentDetails(WaterAllotmentDetails objWater)
    {
        string str_Retvalue;
        con.Open();
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_Water_Allotment";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objWater.Action);
            cmd.Parameters.AddWithValue("@P_VCH_INDUSTRYCODE", objWater.strIndustryCode);
            cmd.Parameters.AddWithValue("@P_VCH_UNITNAME", objWater.strUnitName);
            cmd.Parameters.AddWithValue("@P_INT_IEID", objWater.intIEId);
            cmd.Parameters.AddWithValue("@P_VCH_PLOTNO", objWater.strPlotShedNo);
            cmd.Parameters.AddWithValue("@P_INT_PURPOSE", objWater.intPupose);
            cmd.Parameters.AddWithValue("@P_VCH_QUANTITY", objWater.strQuantity);
            cmd.Parameters.AddWithValue("@P_VCH_FLOWMETERSIZE", objWater.strFlowMeterSize);
            cmd.Parameters.AddWithValue("@P_VCH_MAKEMODEL", objWater.strMakeModel);
            cmd.Parameters.AddWithValue("@P_VCH_MANFSERIALNO", objWater.strManfSerialNo);
            cmd.Parameters.AddWithValue("@P_VCH_OHTANKSIZE", objWater.strOHTankSize);
            cmd.Parameters.AddWithValue("@P_VCH_OHTANKNO", objWater.strOHTankNo);
            cmd.Parameters.AddWithValue("@P_VCH_SUMPVATSIZE", objWater.strSumpVatSize);
            cmd.Parameters.AddWithValue("@P_VCH_SUMPVATNO", objWater.strSumpVatNo);
            cmd.Parameters.AddWithValue("@P_VCH_CONTACTNAME", objWater.strContactName);
            cmd.Parameters.AddWithValue("@P_VCH_CONTACTEMAIL", objWater.strContactEmail);
            cmd.Parameters.AddWithValue("@P_VCH_CONTACTMOBILE", objWater.strContactMobile);
            cmd.Parameters.AddWithValue("@P_VCH_CONTACTADDRESS", objWater.strContactAddress);
            cmd.Parameters.AddWithValue("@P_VCH_PLUMBERNAME", objWater.strPlumberName);
            cmd.Parameters.AddWithValue("@P_VCH_PLUMBEREMAIL", objWater.strPlumberEmail);
            cmd.Parameters.AddWithValue("@P_VCH_PLUMBERMOBILE", objWater.strPlumberMobile);
            cmd.Parameters.AddWithValue("@P_VCH_PLUMBERADDRESS", objWater.strPlumberAddress);
            cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", objWater.intCreatedBy);
            cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 50);
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
    public string UpdateStatus(WaterAllotmentDetails objWater)
    {
        string str_Retvalue;
        con.Open();
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_Water_Allotment";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_CHAR_ACTION", objWater.Action);
            cmd.Parameters.AddWithValue("@P_VCH_APPLICATIONID", objWater.strApplicationId);
            cmd.Parameters.AddWithValue("@P_VCH_REFNO", objWater.strRefNo);
            cmd.Parameters.AddWithValue("@P_VCH_INVESTORNAME", objWater.strInvestorName);
            cmd.Parameters.AddWithValue("@P_VCH_PAYMENTURL", objWater.PaymentUrl);
            cmd.Parameters.AddWithValue("@P_INT_STATUS", objWater.intStatus);
            cmd.Parameters.AddWithValue("@P_INT_PROPOSAL_ID", objWater.strProposalId);
            cmd.Parameters.AddWithValue("@P_INT_SERVICE_ID", objWater.intServiceId);
            cmd.Parameters.AddWithValue("@P_INT_CREATEDBY", objWater.intCreatedBy);
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
    public DataTable IEName(WaterAllotmentDetails objWater)
    {
        DataTable dt = new DataTable();
        try
        {
            object[] objParam = { "@P_CHAR_ACTION", objWater.Action, "@P_INT_PROPOSAL_ID", objWater.strProposalId };
            dt = (DataTable)ObjCmnDll.GetDataTable("AdminAppConnectionProd", "USP_Water_Allotment", objParam);
            return dt;
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
            dt = null;
        }
    }
}