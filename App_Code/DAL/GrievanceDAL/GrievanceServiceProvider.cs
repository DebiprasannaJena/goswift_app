using EntityLayer.GrievanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GrievanceServiceProvider
/// </summary>
public class GrievanceServiceProvider : IGrievanceServiceProvider
{
    #region "Member Variable"
    string ConnectionString = "AdminAppConnectionProd";
    SqlConnection gSqlConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    string Str_RetValue = "";
    #endregion
    public GrievanceServiceProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override DataTable FillUnitDetail(GrievanceEntity grievanceEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
            cmd.Parameters.AddWithValue("@intInvestorId", grievanceEntity.intInvestorId);
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
            gSqlConn.Close();
        }
        return dt;
    }
    public override DataTable FillDistrict(GrievanceEntity grievanceEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
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
            gSqlConn.Close();
        }
        return dt;
    }
    public override DataTable FillIndustry(GrievanceEntity grievanceEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
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
            gSqlConn.Close();
        }
        return dt;
    }
    public override DataTable FillStatus(GrievanceEntity grievanceEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
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
            gSqlConn.Close();
        }
        return dt;
    }
    public override DataTable FillGrievanceType(GrievanceEntity grievanceEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
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
            gSqlConn.Close();
        }
        return dt;
    }
    public override DataTable FillGrievanceSubType(GrievanceEntity grievanceEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@intInvestorId", grievanceEntity.intInvestorId);
            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
            cmd.Parameters.AddWithValue("@intGrivTypeId", grievanceEntity.intGrivTypeId);
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
            gSqlConn.Close();
        }
        return dt;
    }
    public override string SaveGrievanceDetail(GrievanceEntity grievanceEntity)
    {
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_ADD";

            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@intDistrictId", grievanceEntity.intDistrictId);
            cmd.Parameters.AddWithValue("@vchDistrictName", grievanceEntity.vchDistrictName);
            cmd.Parameters.AddWithValue("@intGrivTypeId", grievanceEntity.intGrivTypeId);
            cmd.Parameters.AddWithValue("@vchGrivSubTypeId", grievanceEntity.vchGrivSubTypeId);
            cmd.Parameters.AddWithValue("@intInvestLevel", grievanceEntity.intInvestmentLevel);
            cmd.Parameters.AddWithValue("@vchApplicantName", grievanceEntity.vchApplicantName);
            cmd.Parameters.AddWithValue("@vchDesignation", grievanceEntity.vchDesignation);
            cmd.Parameters.AddWithValue("@vchMobileNo", grievanceEntity.vchMobileNo);
            cmd.Parameters.AddWithValue("@vchEmail", grievanceEntity.vchEmail);
            cmd.Parameters.AddWithValue("@vchGrivTitle", grievanceEntity.vchGrivTitle);
            cmd.Parameters.AddWithValue("@vchGrivDetail", grievanceEntity.vchGrivDetail);
            cmd.Parameters.AddWithValue("@vchAttachment1", grievanceEntity.vchAttachment1);
            cmd.Parameters.AddWithValue("@vchAttachment2", grievanceEntity.vchAttachment2);
            cmd.Parameters.AddWithValue("@intCreatedBy", grievanceEntity.intInvestorId);
            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
            cmd.Parameters.AddWithValue("@intIndustryCategory", grievanceEntity.IntIndustryCategory);  // Add by anil sahoo

            //cmd.Parameters.AddWithValue("@vchOutput", SqlDbType.VarChar);
            //cmd.Parameters["@vchOutput"].Direction = ParameterDirection.Output;
            //cmd.ExecuteNonQuery();

            SqlParameter par1 = new SqlParameter("@vchOutput", SqlDbType.VarChar, 2000);
            par1.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par1);

            cmd.ExecuteNonQuery();


            //Str_RetValue = cmd.Parameters["@vchOutput"].Value.ToString();

            Str_RetValue = Convert.ToString(cmd.Parameters["@vchOutput"].Value);
        }        
        catch (Exception ex)
        { 
            throw ex; 
        }
        finally
        {
            gSqlConn.Close();
            cmd.Dispose();
        }
        return Str_RetValue;
    }
    public override DataTable DisplayInvestorGrievanceDetail(GrievanceEntity grievanceEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
            cmd.Parameters.AddWithValue("@intInvestorId", grievanceEntity.intInvestorId);
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
            gSqlConn.Close();
        }
        return dt;
    }

    #region Added By Sushant Jena

    public override DataTable ViewGrivTakeActionDetails(GrievanceEntity objGrivEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }

        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@vchAction", objGrivEntity.StrAction);
            cmd.Parameters.AddWithValue("@P_VCH_GRIV_ID", objGrivEntity.strGrivId);
            cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objGrivEntity.intDistrictId);
            cmd.Parameters.AddWithValue("@P_INT_INVEST_LEVEL", objGrivEntity.intInvestmentLevel);
            cmd.Parameters.AddWithValue("@P_VCH_FROM_DATE", objGrivEntity.strFromDate);
            cmd.Parameters.AddWithValue("@P_VCH_TO_DATE", objGrivEntity.strToDate);
            cmd.Parameters.AddWithValue("@P_INT_USER_ID", objGrivEntity.intUserId);

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
            gSqlConn.Close();
        }
        return dt;
    }
    public override DataSet ViewGrivApplicationDetails(GrievanceEntity objGrivEntity)
    {
        DataSet ds = new DataSet();
        SqlCommand cmd = new SqlCommand();

        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }

        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@vchAction", objGrivEntity.StrAction);
            cmd.Parameters.AddWithValue("@P_VCH_GRIV_ID", objGrivEntity.strGrivId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd = null;
            gSqlConn.Close();
        }
        return ds;
    }
    public override DataTable ViewGrivDetails(GrievanceEntity objGrivEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }

        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@vchAction", objGrivEntity.StrAction);
            cmd.Parameters.AddWithValue("@P_VCH_GRIV_ID", objGrivEntity.strGrivId);
            cmd.Parameters.AddWithValue("@P_INT_DISTRICT_ID", objGrivEntity.intDistrictId);
            cmd.Parameters.AddWithValue("@P_INT_INVEST_LEVEL", objGrivEntity.intInvestmentLevel);
            cmd.Parameters.AddWithValue("@P_VCH_FROM_DATE", objGrivEntity.strFromDate);
            cmd.Parameters.AddWithValue("@P_VCH_TO_DATE", objGrivEntity.strToDate);
            cmd.Parameters.AddWithValue("@P_INT_USER_ID", objGrivEntity.intUserId);
            cmd.Parameters.AddWithValue("@P_INT_STATUS", objGrivEntity.intStatus);

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
            gSqlConn.Close();
        }
        return dt;
    }

    public override string TakeActionDetail(GrievanceEntity grievanceEntity)
    {
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_ADD";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@intDistrictId", grievanceEntity.intDistrictId);
            cmd.Parameters.AddWithValue("@strReferenceFilename", grievanceEntity.strReferenceFilename);
            cmd.Parameters.AddWithValue("@strRemark", grievanceEntity.strRemark);
            cmd.Parameters.AddWithValue("@intActionTakenBy", grievanceEntity.intActionTakenBy);
            cmd.Parameters.AddWithValue("@intCreatedBy", grievanceEntity.intCreatedBy);
            cmd.Parameters.AddWithValue("@intStatus", grievanceEntity.intStatus);
            cmd.Parameters.AddWithValue("@vchGrivId", grievanceEntity.strGrivId);
            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
            cmd.Parameters.AddWithValue("@vchOutput", SqlDbType.VarChar);
            cmd.Parameters["@vchOutput"].Direction = ParameterDirection.Output;

            //SqlParameter par1 = new SqlParameter("@vchOutput", SqlDbType.VarChar, 2000);
            //par1.Direction = ParameterDirection.Output;
            //cmd.Parameters.Add(par1);

            cmd.ExecuteNonQuery();

            Str_RetValue = cmd.Parameters["@vchOutput"].Value.ToString();

        }
        catch (NullReferenceException ex)
        { throw ex; }
        catch (Exception ex)
        { throw ex; }
        finally
        {
            gSqlConn.Close();
            cmd.Dispose();
        }
        return Str_RetValue;
    }

    public override DataTable GetDistrictIdByUser(GrievanceEntity objGrivEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }

        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@vchAction", "GD");
            cmd.Parameters.AddWithValue("@P_INT_USER_ID", objGrivEntity.intUserId);

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
            gSqlConn.Close();
        }
        return dt;
    }

    #endregion


    #region add by anil sahoo

    //public override string SaveGrievanceType(GrievanceEntity grievanceEntity)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    if (gSqlConn.State == ConnectionState.Closed)
    //    {
    //        gSqlConn.Open();
    //    }
    //    try
    //    {
    //        cmd.Connection = gSqlConn;
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.CommandText = "USP_GRIEVANCE_ADD";
    //        cmd.Parameters.Clear();
    //        cmd.Parameters.AddWithValue("@vchGrivName", grievanceEntity.strGrivType);
    //        cmd.Parameters.AddWithValue("@intGrivStatus", grievanceEntity.intGrivActiveStatus);
    //        cmd.Parameters.AddWithValue("@intCreatedBy", grievanceEntity.intCreatedBy);
    //        cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
    //        cmd.Parameters.AddWithValue("@vchOutput", SqlDbType.VarChar);
    //        cmd.Parameters["@vchOutput"].Direction = ParameterDirection.Output;
    //        cmd.ExecuteNonQuery();
    //        Str_RetValue = cmd.Parameters["@vchOutput"].Value.ToString();
    //    }
    //    catch (NullReferenceException ex)
    //    { throw ex; }
    //    catch (Exception ex)
    //    { throw ex; }
    //    finally
    //    {
    //        gSqlConn.Close();
    //        cmd.Dispose();
    //    }
    //    return Str_RetValue;
    //}
    //public override string SaveGrievanceTypeEdit(GrievanceEntity grievanceEntity)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    if (gSqlConn.State == ConnectionState.Closed)
    //    {
    //        gSqlConn.Open();
    //    }
    //    try
    //    {
    //        cmd.Connection = gSqlConn;
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.CommandText = "USP_GRIEVANCE_ADD";
    //        cmd.Parameters.Clear();
    //        cmd.Parameters.AddWithValue("@vchGrivName", grievanceEntity.strGrivType);
    //        cmd.Parameters.AddWithValue("@intGrivStatus", grievanceEntity.intGrivActiveStatus);
    //        cmd.Parameters.AddWithValue("@intCreatedBy", grievanceEntity.intCreatedBy);
    //        cmd.Parameters.AddWithValue("@intGrivTypeId", grievanceEntity.intGrivTypeId);
    //        cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
    //        cmd.Parameters.AddWithValue("@vchOutput", SqlDbType.VarChar);
    //        cmd.Parameters["@vchOutput"].Direction = ParameterDirection.Output;
    //        cmd.ExecuteNonQuery();
    //        Str_RetValue = cmd.Parameters["@vchOutput"].Value.ToString();
    //    }
    //    catch (NullReferenceException ex)
    //    { throw ex; }
    //    catch (Exception ex)
    //    { throw ex; }
    //    finally
    //    {
    //        gSqlConn.Close();
    //        cmd.Dispose();
    //    }
    //    return Str_RetValue;
    //}
    //public override DataTable ViewGrivTypeSerch(GrievanceEntity grievanceEntity)
    //{
    //    DataTable dt = new DataTable();
    //    SqlCommand cmd = new SqlCommand();
    //    if (gSqlConn.State == ConnectionState.Closed)
    //    {
    //        gSqlConn.Open();
    //    }
    //    try
    //    {
    //        cmd.Connection = gSqlConn;
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.CommandText = "USP_GRIEVANCE_VIEW";
    //        cmd.Parameters.Clear();
    //        cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
    //        cmd.Parameters.AddWithValue("@intGrivTypeId", grievanceEntity.intGrivTypeId);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(dt);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        cmd = null;
    //        gSqlConn.Close();
    //    }
    //    return dt;
    //}
    //public override DataTable FillGrievanceTypeFilter(GrievanceEntity grievanceEntity)
    //{
    //    DataTable dt = new DataTable();
    //    SqlCommand cmd = new SqlCommand();
    //    if (gSqlConn.State == ConnectionState.Closed)
    //    {
    //        gSqlConn.Open();
    //    }
    //    try
    //    {
    //        cmd.Connection = gSqlConn;
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.CommandText = "USP_GRIEVANCE_VIEW";
    //        cmd.Parameters.Clear();
    //        cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(dt);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        cmd = null;
    //        gSqlConn.Close();
    //    }
    //    return dt;
    //}
    //public override string SaveGrievanceSubtype(GrievanceEntity grievanceEntity)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    if (gSqlConn.State == ConnectionState.Closed)
    //    {
    //        gSqlConn.Open();
    //    }
    //    try
    //    {
    //        cmd.Connection = gSqlConn;
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.CommandText = "USP_GRIEVANCE_ADD";
    //        cmd.Parameters.Clear();
    //        cmd.Parameters.AddWithValue("@intGrivTypeId", grievanceEntity.intGrivTypeId);
    //        cmd.Parameters.AddWithValue("@vchGrivSubTypeId", grievanceEntity.strGrivSubtype);
    //        cmd.Parameters.AddWithValue("@intGrivStatus", grievanceEntity.intGrivActiveStatus);
    //        cmd.Parameters.AddWithValue("@intCreatedBy", grievanceEntity.intCreatedBy);
    //        cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
    //        cmd.Parameters.AddWithValue("@vchOutput", SqlDbType.VarChar);
    //        cmd.Parameters["@vchOutput"].Direction = ParameterDirection.Output;
    //        cmd.ExecuteNonQuery();
    //        Str_RetValue = cmd.Parameters["@vchOutput"].Value.ToString();
    //    }
    //    catch (NullReferenceException ex)
    //    { throw ex; }
    //    catch (Exception ex)
    //    { throw ex; }
    //    finally
    //    {
    //        gSqlConn.Close();
    //        cmd.Dispose();
    //    }
    //    return Str_RetValue;
    //}   
    //public override DataTable ViewGrivSubTypeDetailsFilter(GrievanceEntity grievanceEntity)
    //{
    //    DataTable dt = new DataTable();
    //    SqlCommand cmd = new SqlCommand();
    //    if (gSqlConn.State == ConnectionState.Closed)
    //    {
    //        gSqlConn.Open();
    //    }
    //    try
    //    {
    //        cmd.Connection = gSqlConn;
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.CommandText = "USP_GRIEVANCE_VIEW";
    //        cmd.Parameters.Clear();
    //        cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
    //        cmd.Parameters.AddWithValue("@intGrivSubTypeId", grievanceEntity.intGrivSubTypeId);
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        da.Fill(dt);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        cmd = null;
    //        gSqlConn.Close();
    //    }
    //    return dt;
    //}
    //public override string SaveGrievanceSubTypeEdit(GrievanceEntity grievanceEntity)
    //{
    //    SqlCommand cmd = new SqlCommand();
    //    if (gSqlConn.State == ConnectionState.Closed)
    //    {
    //        gSqlConn.Open();
    //    }
    //    try
    //    {
    //        cmd.Connection = gSqlConn;
    //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
    //        cmd.CommandText = "USP_GRIEVANCE_ADD";
    //        cmd.Parameters.Clear();
    //        cmd.Parameters.AddWithValue("@intGrivTypeId", grievanceEntity.intGrivTypeId);
    //        cmd.Parameters.AddWithValue("@vchGrivSubTypeId", grievanceEntity.strGrivSubtype);
    //        cmd.Parameters.AddWithValue("@intGrivStatus", grievanceEntity.intGrivActiveStatus);
    //        cmd.Parameters.AddWithValue("@intCreatedBy", grievanceEntity.intCreatedBy);
    //        cmd.Parameters.AddWithValue("@intGrivSubTypeId", grievanceEntity.intGrivSubTypeId);
    //        cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
    //        cmd.Parameters.AddWithValue("@vchOutput", SqlDbType.VarChar);
    //        cmd.Parameters["@vchOutput"].Direction = ParameterDirection.Output;
    //        cmd.ExecuteNonQuery();
    //        Str_RetValue = cmd.Parameters["@vchOutput"].Value.ToString();
    //    }
    //    catch (NullReferenceException ex)
    //    { throw ex; }
    //    catch (Exception ex)
    //    { throw ex; }
    //    finally
    //    {
    //        gSqlConn.Close();
    //        cmd.Dispose();
    //    }
    //    return Str_RetValue;
    //}



    public override string AddUpdateGrievanceType(GrievanceEntity grievanceEntity)
    {
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_ADD";

            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
            cmd.Parameters.AddWithValue("@vchGrivType", grievanceEntity.strGrivType);
            cmd.Parameters.AddWithValue("@intGrivStatus", grievanceEntity.intGrivActiveStatus);
            cmd.Parameters.AddWithValue("@intCreatedBy", grievanceEntity.intCreatedBy);
            cmd.Parameters.AddWithValue("@intGrivTypeId", grievanceEntity.intGrivTypeId);
            cmd.Parameters.AddWithValue("@vchGrivSubTypeId", grievanceEntity.strGrivSubType);
            cmd.Parameters.AddWithValue("@intGrivSubTypeId", grievanceEntity.intGrivSubTypeId);  

            cmd.Parameters.AddWithValue("@vchOutput", SqlDbType.VarChar);
            cmd.Parameters["@vchOutput"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            Str_RetValue = cmd.Parameters["@vchOutput"].Value.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            gSqlConn.Close();
            cmd.Dispose();
        }
        return Str_RetValue;
    }
    public override DataTable ViewGrivTypeDetails(GrievanceEntity grievanceEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();

        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
            cmd.Parameters.AddWithValue("@intGrivTypeId", grievanceEntity.intGrivTypeId);

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
            gSqlConn.Close();
        }
        return dt;
    }

    public override string AddUpdateGrievanceSubType(GrievanceEntity grievanceEntity)
    {
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_ADD";
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
            cmd.Parameters.AddWithValue("@intGrivTypeId", grievanceEntity.intGrivTypeId);
            cmd.Parameters.AddWithValue("@vchGrivSubTypeId", grievanceEntity.strGrivSubType);
            cmd.Parameters.AddWithValue("@intGrivStatus", grievanceEntity.intGrivActiveStatus);
            cmd.Parameters.AddWithValue("@intCreatedBy", grievanceEntity.intCreatedBy);
            cmd.Parameters.AddWithValue("@intGrivSubTypeId", grievanceEntity.intGrivSubTypeId);

            cmd.Parameters.AddWithValue("@vchOutput", SqlDbType.VarChar);
            cmd.Parameters["@vchOutput"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            Str_RetValue = cmd.Parameters["@vchOutput"].Value.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            gSqlConn.Close();
            cmd.Dispose();
        }
        return Str_RetValue;
    }
    public override DataTable ViewGrivSubTypeDetails(GrievanceEntity grievanceEntity)
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";

            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@vchAction", grievanceEntity.StrAction);
            cmd.Parameters.AddWithValue("@intGrivTypeId", grievanceEntity.intGrivTypeId);
            cmd.Parameters.AddWithValue("@intGrivSubTypeId", grievanceEntity.intGrivSubTypeId);

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
            gSqlConn.Close();
        }
        return dt;
    }

    public override DataTable GetGrievanceSmsContent(GrievanceEntity objGrivEntity)    // get sms content
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();
            
        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchAction", objGrivEntity.StrAction);
            cmd.Parameters.AddWithValue("@P_INT_SMS_ID", objGrivEntity.IntSmsId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            throw ex;

        }
       finally
        {
            gSqlConn.Close();
        }
        return dt;
    }



    public override DataTable GetUserInformationSmsEmailSend(GrievanceEntity objGrivEntity)  /// get user infrometion for send sms and email
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();

        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchAction", objGrivEntity.StrAction);
            cmd.Parameters.AddWithValue("@P_VCH_GRIV_ID", objGrivEntity.strGrivId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            gSqlConn.Close();
        }
        return dt;
    }


    public override DataTable GrievanceTrackDetails(GrievanceEntity objGrivEntity)  /// get user Tracking  infrometion 
    {
        DataTable dt = new DataTable();
        SqlCommand cmd = new SqlCommand();
        if (gSqlConn.State == ConnectionState.Closed)
        {
            gSqlConn.Open();

        }
        try
        {
            cmd.Connection = gSqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_GRIEVANCE_VIEW";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@vchAction", objGrivEntity.StrAction);
            cmd.Parameters.AddWithValue("@P_VCH_GRIV_ID", objGrivEntity.strGrivId);
            cmd.Parameters.AddWithValue("@P_INT_USER_ID", objGrivEntity.intUserId);
            cmd.Parameters.AddWithValue("@P_VCH_Mobile_No", objGrivEntity.vchMobileNo);
            cmd.Parameters.AddWithValue("@P_VCH_EMAIL", objGrivEntity.vchEmail);
            cmd.Parameters.AddWithValue("@P_VCH_COMPANY_NAME", objGrivEntity.vchApplicantName);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            gSqlConn.Close();
        }
        return dt;
    }

    #endregion

}