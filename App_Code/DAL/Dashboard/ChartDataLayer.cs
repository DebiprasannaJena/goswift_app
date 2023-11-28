using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using EntityLayer.Chart;

/// <summary>
/// Summary description for ChartDataLayer
/// </summary>
public class ChartDataLayer
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    public ChartDataLayer()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet Feedback_Chart_View(Feedback_Chart_Entity objPlcEntity)
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();
        try
        {
            objCommand.CommandText = "USP_FEEDBACK_CHART_COUNT";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", objPlcEntity.strAction);
            objCommand.Parameters.AddWithValue("@P_INT_QUESTION_ID", objPlcEntity.intQuestionId);

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