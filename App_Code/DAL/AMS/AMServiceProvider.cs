using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for AMServiceProvider
/// </summary>
/// public class MasterServiceProvider:IMasterServiceProvider
public class AMServiceProvider : IAMServiceProvider
{

   #region "Member Variable"
    CSMPDK_3_0.CommonDLL ObjCmnDll = new CSMPDK_3_0.CommonDLL();
    string ConnectionString = "AdminAppConnectionProd";
   
    public SqlDataReader gSqlDataReader;
    
    public string str_query;
    public string str_return;
    public int gCount;
    SqlCommand cmd;
    SqlConnection con;
    #endregion

    #region "Connection"
    public AMServiceProvider()
	{
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
    }
    #endregion

    #region "To Fill Sector"

    public override DataTable FillSector()
    {
        DataTable dt = new DataTable();
        try
        {
            dt = ObjCmnDll.GetDataTable(ConnectionString, "SELECT intSectorId,VchSectorName FROM M_AMS_SECTOR WHERE bitDeletedFlag<>1 Order by VchSectorName");
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    #endregion

    #region "To Fill Sector"

    public override DataTable FillSLFCChecklist(int ProjectId)
    {
        DataTable dt = new DataTable();
        try
        {
            dt = ObjCmnDll.GetDataTable(ConnectionString, "SELECT L.intChecklistId AS ChecklistId,L.vchChecklistPoint AS ChecklistPoint FROM M_AMS_SLFC_CHECKLIST L WHERE L.bitDeletedFlag=0 and L.intType IN(0,(SELECT INTSECTORID FROM M_AMS_PROJECT WHERE INTPROJCTID=" + ProjectId + "))");
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    #endregion

    #region "View Financial performance"

    /// <summary>
    /// For View Financial performance
    /// </summary>
    /// <param name="objMaster"></param>
    /// <returns>dt</returns>
    public override DataTable ViewFinancperfm(AMS objAMS)
    {
        try
        {
            DataTable dt = new DataTable();
            Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId };
            dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_FINANCE_MASTER", objParam);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ObjCmnDll = null;

        }
    }
    #endregion


    #region "Project Master"
    /// <summary>
    /// For Add project master
    /// </summary>
    /// <param name="objAMS"></param>
    /// <returns>strOutput</returns>
    public override string AddProjectMaster(AMS objAMS)
    {
        string strOutput = "0";
        try
        {
            cmd = new SqlCommand();
            cmd = new SqlCommand("USP_AMS_PROJECT_MASTER", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
            cmd.Parameters.AddWithValue("@PVCH_PROJCT_TITLE", objAMS.ProjectTitle);
            cmd.Parameters.AddWithValue("@PVCH_PROJCT_NAME", objAMS.CompanyName);
            cmd.Parameters.AddWithValue("@PINT_SECTORID", objAMS.SectorId);
            cmd.Parameters.AddWithValue("@PVCH_SECTOR", objAMS.SectorName);
            cmd.Parameters.AddWithValue("@PDTM_APPLICATION_EBIZ", objAMS.ApplicationDate);
            cmd.Parameters.AddWithValue("@PVCH_PROJCT_LOCATION", objAMS.ProjectLocation);
            cmd.Parameters.AddWithValue("@PINT_CATEGORY", objAMS.CategoryId);
            cmd.Parameters.AddWithValue("@PINT_TYPE", objAMS.TypeId);   
            cmd.Parameters.AddWithValue("@PINT_OFFICERID", objAMS.NodalOfficerId);
            cmd.Parameters.AddWithValue("@PVCH_BOARD", objAMS.BoardOfDirectors);
            cmd.Parameters.AddWithValue("@PVCH_BUSINESS", objAMS.Business);
            cmd.Parameters.AddWithValue("@PINT_CREATED_BY", objAMS.CreatedBy);
            cmd.Parameters.AddWithValue("@PINT_PROJCTID", objAMS.ProjectId);
            cmd.Parameters.AddWithValue("@PINT_DISTRICTID", objAMS.DistrictId);
            cmd.Parameters.AddWithValue("@PVCH_UID", objAMS.strUID);
            cmd.Parameters.AddWithValue("@PVCH_REMARK", objAMS.Remark);
            cmd.Parameters.AddWithValue("@PINT_REMARKID", objAMS.intRemarkID);
            cmd.Parameters.AddWithValue("@P_XML_TABLEPRODUCT", objAMS.VCH_XMLTBL);
            cmd.Parameters.AddWithValue("@P_XML_TABLELOC", objAMS.XmlData);
            cmd.Parameters.AddWithValue("@PINT_TOURISM", objAMS.TourismId);                              
            cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
            cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
            cmd.Parameters["@P_MSGOUT"].DbType = System.Data.DbType.String;
            cmd.Parameters["@P_MSGOUT"].Size = 100;
            cmd.ExecuteNonQuery();
            strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
            return strOutput;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            con.Dispose();
        }
    }
   
    /// <summary>
    /// For View Project Master
    /// </summary>
    /// <param name="objAMS"></param>
    /// <returns>dt</returns>
    public override DataSet ViewProjectMasterEdit(AMS objAMS)
    {
        try
        {
            DataSet dt = new DataSet();

            Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId, "@PINT_OFFICERID",objAMS.OfficerId };

            dt = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_PROJECT_MASTER", objParam);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ObjCmnDll = null;

        }
    }
    
    /// <summary>
    /// For View Project Master
    /// </summary>
    /// <param name="objAMS"></param>
    /// <returns>dt</returns>
    public override DataTable ViewProjectMaster(AMS objAMS)
    {
        try
        {
            DataTable dt = new DataTable();

            Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId, "@PINT_TYPE", objAMS.TypeId, "@PINT_OFFICERID", objAMS.OfficerId };

            dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_PROJECT_MASTER", objParam);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ObjCmnDll = null;

        }
    }

    /// <summary>
    /// For View Project Status
    /// </summary>
    /// <param name="objAMS"></param>
    /// <returns>ds</returns>
    public override DataSet ViewProjectSts(AMS objAMS)
    {
        try
        {
            DataSet ds = new DataSet();

            Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_TYPE", objAMS.TypeId, "@PINT_OFFICERID", objAMS.OfficerId };

            ds = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_PROJECT_MASTER", objParam);
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ObjCmnDll = null;

        }
    }


    /// <summary>
    /// For View Project from Single Window Portal
    /// </summary>
    /// <param name="objAMS"></param>
    /// <returns>dt</returns>
    public override DataSet ViewNewRequest(AMS objAMS)
    {
        try
        {
            DataSet dt = new DataSet();

            Object[] objParam = { "@ACTIONCODE", objAMS.Action, "@INTPROJECTID", objAMS.ProjectId };

            dt = ObjCmnDll.GetDataSet(ConnectionString, "USP_M_SWP_PROJECT", objParam);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ObjCmnDll = null;

        }
    }
    #endregion


    #region "Add Proposal"
    /// <summary>
    /// For Add project master
    /// </summary>
    /// <param name="objAMS"></param>
    /// <returns>strOutput</returns>
    public override string AddProposalMaster(AMS objAMS)
    {
        string strOutput = "0";
        try
        {
            cmd = new SqlCommand();
            cmd = new SqlCommand("USP_AMS_PROPOSAL", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
            cmd.Parameters.AddWithValue("@PINT_PROJCTID", objAMS.ProjectId);
            //cmd.Parameters.AddWithValue("@XMLDATA", objAMS.XmlData);
            cmd.Parameters.AddWithValue("@P_BRIEF", objAMS.ProposalDetails);
            cmd.Parameters.AddWithValue("@PINT_CREATED_BY", objAMS.CreatedBy);
            cmd.Parameters.AddWithValue("@PVCH_REMARK", objAMS.Remark);
            cmd.Parameters.AddWithValue("@PINT_REMARKID", objAMS.intRemarkID);
            cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
            cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
            cmd.Parameters["@P_MSGOUT"].DbType = System.Data.DbType.String;
            cmd.Parameters["@P_MSGOUT"].Size = 100;
            cmd.ExecuteNonQuery();
            strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
            return strOutput;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            con.Dispose();
        }
    }
    #endregion

    #region "View Proposal"

    /// <summary>
    /// For View Proposal Master
    /// </summary>
    /// <param name="objAMS"></param>
    /// <returns>dt</returns>
    public override DataTable ViewProposalMaster(AMS objAMS)
    {
        try
        {
            DataTable dt = new DataTable();

            Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId };

            dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_PROPOSAL", objParam);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ObjCmnDll = null;

        }
    }

    public override DataTable ViewSWPProposalMaster(AMS objAMS)
    {
        try
        {
            DataTable ds = new DataTable();

            Object[] objParam = { "@ACTIONCODE", objAMS.Action, "@INTPROJECTID", objAMS.ProjectId };

            ds = ObjCmnDll.GetDataTable(ConnectionString, "USP_M_SWP_PROJECT", objParam);
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ObjCmnDll = null;

        }
    }
    #endregion

    #region "Add Finance"
    /// <summary>
    /// For Add Finance master
    /// </summary>
    /// <param name="objAMS"></param>
    /// <returns>strOutput</returns>
    public override string AddFinanceMaster(AMS objAMS)
    {
        string strOutput = "0";
        try
        {
            cmd = new SqlCommand();
            cmd = new SqlCommand("USP_AMS_FINANCE_MASTER", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
            cmd.Parameters.AddWithValue("@PINT_PROJCTID", objAMS.ProjectId);
            cmd.Parameters.AddWithValue("@PVCH_REMARK", objAMS.Remark);
            cmd.Parameters.AddWithValue("@PINT_REMARKID", objAMS.intRemarkID);

            cmd.Parameters.AddWithValue("@XMLDATA", objAMS.XmlData);
            cmd.Parameters.AddWithValue("@PINT_CREATED_BY", objAMS.CreatedBy);

            cmd.Parameters.AddWithValue("@PVCH_FY1", objAMS.FinancialYear);
            cmd.Parameters.AddWithValue("@PVCH_FY2", objAMS.FinancialYear1);
            cmd.Parameters.AddWithValue("@PVCH_FY3", objAMS.FinancialYear2);
           
            cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
            cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
            cmd.Parameters["@P_MSGOUT"].DbType = System.Data.DbType.String;
            cmd.Parameters["@P_MSGOUT"].Size = 100;
            cmd.ExecuteNonQuery();
            strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
            return strOutput;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            con.Dispose();
        }
    }
    #endregion

    #region "Add Financial Document"
    /// <summary>
    /// For Add Financial Document
    /// </summary>
    /// <param name="objAMS"></param>
    /// <returns>strOutput</returns>
    public override string AddFinanceDocument(AMS objAMS)
    {
        string strOutput = "0";
        try
        {
            cmd = new SqlCommand();
            cmd = new SqlCommand("USP_AMS_FINANCE_MASTER", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
            cmd.Parameters.AddWithValue("@PINT_PROJCTID", objAMS.ProjectId);
            cmd.Parameters.AddWithValue("@PVCH_REMARK", objAMS.Remark);
            cmd.Parameters.AddWithValue("@PINT_REMARKID ", objAMS.intRemarkID);
            cmd.Parameters.AddWithValue("@PINT_CREATED_BY", objAMS.CreatedBy);
            cmd.Parameters.AddWithValue("@XMLDATA", objAMS.XmlData);
            
            cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
            cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
            cmd.Parameters["@P_MSGOUT"].DbType = System.Data.DbType.String;
            cmd.Parameters["@P_MSGOUT"].Size = 100;
            cmd.ExecuteNonQuery();
            strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
            return strOutput;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            cmd.Dispose();
            con.Dispose();
        }
    }
    #endregion

    #region "Manage Officers"
    
    public override string AddOfficers(Agenda objA)
    {
        string strOutput = "0";
        try
        {
            Object[] objParam = { "@PCH_ACTION", objA.Action, "@PINT_TAGID", objA.Id,"@PINT_TYPE", objA.OfficerType,
                                  "@PVCH_OFFICERID", objA.OfficerId, "@PINT_CREATEDBY", objA.CreatedBy,"@P_MSGOUT", "out" };

            strOutput = Convert.ToString(ObjCmnDll.ExeNonQry(ConnectionString, "USP_AMS_TAGOFFICERS", objParam));
            return strOutput;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ObjCmnDll = null;
        }
    }
   
    public override DataTable ViewOfficers(Agenda objA)
    {
        DataTable dt = new DataTable();
        try
        {
            Object[] objParam = { "@PCH_ACTION", objA.Action, "@PINT_TAGID", objA.Id,"@PINT_TYPE", objA.OfficerType };

            dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_TAGOFFICERS", objParam);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    public override int GetOfficersType(int UserId)
    {
        int intUserId;
        try
        {
            Object[] objParam = { "@PCH_ACTION", "T", "@PINT_CREATEDBY", UserId };

            intUserId = (int)ObjCmnDll.ExeScalar(ConnectionString, "USP_AMS_TAGOFFICERS", objParam);
            return intUserId;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ObjCmnDll = null;
        }
    }

    #endregion

    #region "Project Details"

    public override DataTable FillActiveProject(Agenda objA)
    {
        DataTable dt = new DataTable();
        try
        {
            Object[] objParam = { "@PCH_ACTION", objA.Action, "@PINT_OFFICERID", objA.UserId, "@PINT_TYPE",objA.OfficerType };

            dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_PROJECT_MASTER", objParam);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    public override DataTable FillActiveSWPProject(Agenda objA)
    {
        DataTable dt = new DataTable();
        try
        {
            Object[] objParam = { "@ACTIONCODE", objA.Action, "@INTOFFICERID", objA.UserId, "@INT_TYPE", objA.OfficerType };

            dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_M_SWP_PROJECT", objParam);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    public override string UpdateProjectDetails(Agenda objA)
    {
        string strOutput = "0";
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_AMS_PROJECTDETAILS";
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@PCH_ACTION", SqlDbType.VarChar).Value = "U";
            cmd.Parameters.Add("@PintDetailsId", SqlDbType.Int).Value = objA.Id;
            cmd.Parameters.Add("@PintProjectId", SqlDbType.VarChar).Value = objA.ProjectId;
            cmd.Parameters.Add("@PvchFinaceDetails", SqlDbType.VarChar).Value = objA.FinaceDetails;
            //cmd.Parameters.Add("@PvchFinanceDescription", SqlDbType.VarChar).Value = objA.FinanceDescription;
            cmd.Parameters.Add("@PvchLand", SqlDbType.VarChar).Value = objA.Land;
            cmd.Parameters.Add("@PvchWater", SqlDbType.VarChar).Value = objA.Water;
            cmd.Parameters.Add("@PvchPower", SqlDbType.VarChar).Value = objA.Power;
            cmd.Parameters.Add("@PintPowerSource", SqlDbType.Int).Value = objA.Source;
            cmd.Parameters.Add("@PvchImplementPeriod", SqlDbType.VarChar).Value = objA.ImplementPeriod;
            cmd.Parameters.Add("@PvchRawMaterial", SqlDbType.VarChar).Value = objA.Material;
            cmd.Parameters.Add("@PintEmployement", SqlDbType.Int).Value = objA.Employement;
            cmd.Parameters.Add("@PintContractual", SqlDbType.Int).Value = objA.Contractual;
            cmd.Parameters.Add("@PintCreatedBy", SqlDbType.Int).Value = objA.CreatedBy;
            cmd.Parameters.Add("@PVCH_REMARK", SqlDbType.VarChar).Value = objA.Remark;
            cmd.Parameters.Add("@PINT_REMARKID", SqlDbType.Int).Value = objA.intRemarkID;
            cmd.Parameters.Add("@P_XML_TABLE", SqlDbType.Xml).Value = objA.VCH_XMLTBL;
            //cmd.Parameters.Add("@P_XML_FINTABLE", SqlDbType.Xml).Value = objA.VCH_XMLFINTBL;
            cmd.Parameters.Add("@P_XML_SOURCE", SqlDbType.Xml).Value = objA.VCH_XMLSOURCE;
            cmd.Parameters.Add("@P_MSGOUT", SqlDbType.VarChar, 100).Value = "Out";
            cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            strOutput = (string)cmd.Parameters["@P_MSGOUT"].Value;
            cmd.Connection.Close();
            return strOutput;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ObjCmnDll = null;
        }
    }
    public override string AddProjectDetails(Agenda objA)
    {
        string strOutput = "0";
        try
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_AMS_PROJECTDETAILS";
            cmd.CommandTimeout = 0;
            cmd.Parameters.Add("@PCH_ACTION", SqlDbType.VarChar).Value = "I";
            cmd.Parameters.Add("@PintDetailsId", SqlDbType.Int).Value = objA.Id;
            cmd.Parameters.Add("@PintProjectId", SqlDbType.VarChar).Value = objA.ProjectId;
            cmd.Parameters.Add("@PvchFinaceDetails", SqlDbType.VarChar).Value = objA.FinaceDetails;
            cmd.Parameters.Add("@PvchFinanceDescription", SqlDbType.VarChar).Value = objA.FinanceDescription;
            cmd.Parameters.Add("@PvchLand", SqlDbType.VarChar).Value = objA.Land;
            cmd.Parameters.Add("@PvchWater", SqlDbType.VarChar).Value = objA.Water;
            cmd.Parameters.Add("@PvchPower", SqlDbType.VarChar).Value = objA.Power;
            cmd.Parameters.Add("@PintPowerSource", SqlDbType.Int).Value = objA.Source;
            cmd.Parameters.Add("@PvchImplementPeriod", SqlDbType.VarChar).Value = objA.ImplementPeriod;
            cmd.Parameters.Add("@PvchRawMaterial", SqlDbType.VarChar).Value = objA.Material;
            cmd.Parameters.Add("@PintEmployement", SqlDbType.Int).Value = objA.Employement;
            cmd.Parameters.Add("@PintContractual", SqlDbType.Int).Value = objA.Contractual;
            cmd.Parameters.Add("@PVCH_REMARK", SqlDbType.VarChar).Value = objA.Remark;
            cmd.Parameters.Add("@PINT_REMARKID", SqlDbType.Int).Value = objA.intRemarkID;
            cmd.Parameters.Add("@PintCreatedBy", SqlDbType.Int).Value = objA.CreatedBy;
            cmd.Parameters.Add("@P_XML_TABLE", SqlDbType.Xml).Value = objA.VCH_XMLTBL;
            cmd.Parameters.Add("@P_XML_SOURCE", SqlDbType.Xml).Value = objA.VCH_XMLSOURCE;
            cmd.Parameters.Add("@P_MSGOUT", SqlDbType.VarChar, 100).Value = "Out";
            cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            strOutput = (string)cmd.Parameters["@P_MSGOUT"].Value;
            cmd.Connection.Close();

            return strOutput;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ObjCmnDll = null;
        }
    }
    public override DataTable ViewProjectDetails(Agenda objA)
    {
        DataTable dt = new DataTable();
        try
        {
            Object[] objParam = { "@PCH_ACTION", objA.Action, "@PintDetailsId", objA.Id, "@PintProjectId", objA.ProjectId };

            dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_PROJECTDETAILS", objParam);
            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    public override DataSet ViewProjectDetailsMaster(Agenda objA)
    {
        DataSet ds = new DataSet();
        try
        {
            Object[] objParam = { "@PCH_ACTION", objA.Action, "@PintDetailsId", objA.Id, "@PintProjectId", objA.ProjectId };

            ds = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_PROJECTDETAILS", objParam);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    public override DataSet ViewSWPProjectDetailsMaster(Agenda objA)
    {
        DataSet ds = new DataSet();
        try
        {
            Object[] objParam = { "@ACTIONCODE", objA.Action, "@INTPROJECTID", objA.ProjectId };

            ds = ObjCmnDll.GetDataSet(ConnectionString, "USP_M_SWP_PROJECT", objParam);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }

    #endregion

    public override DataSet ViewFinaceYr(AMS objAMS)
    {
        try
        {
            DataSet dt = new DataSet();

            Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId, "@PVCH_FY1", objAMS.FinancialYear, "@PVCH_FY2", objAMS.FinancialYear1, "@PVCH_FY3", objAMS.FinancialYear2 };

            dt = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_FINANCE_MASTER", objParam);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ObjCmnDll = null;

        }
    }
    
    #region "Manage Decision"
    
    public override string AddDecision(AMS objAMS)
    {
        string strOutput = "0";
        try
        {
            Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId, "@PVCHDECISION", objAMS.DECISION, "@PVCHPOINT", objAMS.DecisionPoint, "@PINT_CREATEDBY", objAMS.CreatedBy, "@P_MSGOUT", "out" };

            strOutput = Convert.ToString(ObjCmnDll.ExeNonQry(ConnectionString, "USP_AMS_SLFC_DECISION", objParam));
            return strOutput;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            ObjCmnDll = null;
        }
    }

    public override DataTable ViewDecision(AMS objAMS)
    {
        try
        {
            DataTable ds = new DataTable();

            Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId };

            ds = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_SLFC_DECISION", objParam);
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            ObjCmnDll = null;

        }
    }


    #endregion
  
   public override DataTable ViewFinace(AMS objAMS)
    {
        DataTable dt = new DataTable();
        try
        {          
            Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId };

            dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_FINANCE_MASTER", objParam);           
        }
        catch (Exception ex)
        {
            dt = null;
        }
        finally
        {
            ObjCmnDll = null;
        }
        return dt;
    }

   public override DataTable ViewFinaceDoc(AMS objAMS)
   {
       DataTable dt = new DataTable();
       try
       {
           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId };

           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_FINANCE_MASTER", objParam);
       }
       catch (Exception ex)
       {
           dt = null;
       }
       finally
       {
           ObjCmnDll = null;
       }
       return dt;
   }

   public override DataTable ViewSWPFinace(AMS objAMS)
   {
       DataTable dt = new DataTable();
       try
       {
           Object[] objParam = { "@ACTIONCODE", objAMS.Action, "@INTPROJECTID", objAMS.ProjectId };

           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_M_SWP_PROJECT", objParam);
       }
       catch (Exception ex)
       {
           dt = null;
       }
       finally
       {
           ObjCmnDll = null;
       }
       return dt;
   }


   public override DataTable ViewSWPFinaceDoc(AMS objAMS)
   {
       DataTable dt = new DataTable();
       try
       {
           Object[] objParam = { "@ACTIONCODE", objAMS.Action, "@INTPROJECTID", objAMS.ProjectId };

           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_M_SWP_PROJECT", objParam);
       }
       catch (Exception ex)
       {
           dt = null;
       }
       finally
       {
           ObjCmnDll = null;
       }
       return dt;
   }

   //MOM DETAILS

   #region "View Mom"

   /// <summary>
   /// For View Mom
   /// </summary>
   /// <param name="objAMS"></param>
   /// <returns>dt</returns>
   public override DataSet ViewMom(AMS objAMS)
   {
       try
       {
           DataSet dt = new DataSet();

           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId, "@PINTOFFICERID", objAMS.OfficerId };

           dt = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_AGENDA", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;

       }
   }

   #endregion

   public override DataTable ViewNodalOfficer(AMS objAMS)
   {
       try
       {
           DataTable dt = new DataTable();

           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId };

           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_PROJECT_MASTER", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;

       }
   }

   #region "Add for Publish"
   /// <summary>
   /// For Add project master
   /// </summary>
   /// <param name="objAMS"></param>
   /// <returns>strOutput</returns>
   public override string AddProjectPublish(AMS objAMS)
   {
      string strOutput = "0";
       try
       {
           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId,"@PINTSTATUS",objAMS.ProjectStatus, "@PINT_CREATED_BY", objAMS.CreatedBy,
                                    "@P_MSGOUT", "out" };

           strOutput = Convert.ToString(ObjCmnDll.ExeNonQry(ConnectionString, "USP_AMS_AGENDA", objParam));
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }
   #endregion

   public override DataSet ViewComments(AMS objAMS)
   {
       DataSet ds = new DataSet();
       try
       {
           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId, "@PINTOFFICERID", objAMS.OfficerId };
           ds = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_FEEDBACK", objParam);
       }
       catch (Exception ex)
       {
           ds = null;
       }
       finally
       {
           ObjCmnDll = null;
       }
       return ds;
   }


   #region "Add Comments"
   public override string AddComments(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId, "@PINTOFFICERID", objAMS.OfficerId, "@PVCHCOMMENT", objAMS.COMMENT, "@P_MSGOUT", "out" };
           strOutput = Convert.ToString(ObjCmnDll.ExeNonQry(ConnectionString, "USP_AMS_FEEDBACK", objParam));
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   #endregion
   public override DataSet ViewSLFC(AMS objAMS)
   {
       try
       {
           DataSet dt = new DataSet();

           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINTOFFICERID", objAMS.OfficerId, "@PINT_PROJCTID", objAMS.ProjectId, "@PINTOFFICERTYPE",objAMS.TypeId };

           dt = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_AGENDA", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;

       }
   }

   public override DataSet ViewMailInfo(AMS objAMS)
   {
       try
       {
           DataSet dt = new DataSet();

           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINTOFFICERID", objAMS.OfficerId, "@PINT_PROJCTID", objAMS.ProjectId };

           dt = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_PUBLISH", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;

       }
   }

   #region "TakeAction"

   public override string TakeAction(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINT_PROJCTID", objAMS.ProjectId, "@PINTOFFICERID", objAMS.OfficerId, "@PVCHCOMMENT", objAMS.COMMENT,"@PINT_REMARKID", objAMS.intRemarkID,
                                   "@PINTTYPE", objAMS.TypeId, "@PINTSTATUS", objAMS.ProjectStatus, "@PVCHMEMBER", objAMS.SLFCMember,"@PINT_CREATED_BY",objAMS.CreatedBy, "@P_MSGOUT", "out" };
           strOutput = Convert.ToString(ObjCmnDll.ExeNonQry(ConnectionString, "USP_AMS_FORWARD", objParam));
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }

   #endregion
   #region for View Nodal Officer Against a Project
   public override DataSet ViewNodalOfficerMailinfo(AMS objAMS)
   {
       try
       {
           DataSet dt = new DataSet();

           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PINTOFFICERID", objAMS.OfficerId, "@PINT_PROJCTID", objAMS.ProjectId, "@PINTOFFICERTYPE", objAMS.TypeId, "@PVCHMEMBER",objAMS.SLFCMember };

           dt = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_FORWARD", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;

       }
   }
   #endregion

   public override DataTable GetDefaultMember(AMS objAMS)
   {
       try
       {
           DataTable dt = new DataTable();

           Object[] objParam = { "@PCH_ACTION", objAMS.Action };

           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_SCHEUDLE_MAIL", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;

       }
   }

   public override DataTable ViewEmployeeDtls(string StrAction,string strText)
   {
       try
       {
           DataTable dt = new DataTable();
           Object[] objParam = { "@CHRACTION", StrAction, "@SEARCHTEXT", strText };
           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_DASHBOARD", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   public override DataTable ViewFinanceDetails(Agenda objA)
   {
       DataTable dt = new DataTable();
       try
       {
           Object[] objParam = { "@PCH_ACTION", objA.Action, "@PintProjectId", objA.ProjectId };

           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_PROJECTDETAILS", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }
   #region "Add Proposal"
 
   public override string AddSLFCComments(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_AMS_SLFC_DECISION", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
           cmd.Parameters.AddWithValue("@PVCHDECISION", objAMS.DECISION);
           cmd.Parameters.AddWithValue("@PCHECKLISTID", objAMS.TypeId);
           cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
           cmd.Parameters["@P_MSGOUT"].Size = 100;
           cmd.ExecuteNonQuery();
           strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   #endregion
   public override DataTable FillTermCondition()
   {
       DataTable dt = new DataTable();
       try
       {
           dt = ObjCmnDll.GetDataTable(ConnectionString, "SELECT intChecklistId AS ChecklistId,vchChecklistPoint AS ChecklistPoint,bitDeletedFlag FROM M_AMS_SLFC_CHECKLIST");
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }
   public override string ActiveSLFCComments(Agenda objA)
   {
       string strOutput = "0";
       try
       {
           SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.CommandText = "USP_AMS_SLFC_DECISION";
           cmd.CommandTimeout = 0;
           cmd.Parameters.Add("@PCH_ACTION", SqlDbType.VarChar).Value = objA.Action;
           cmd.Parameters.Add("@PChecklistId", SqlDbType.Int).Value = objA.Id;
           cmd.Parameters.Add("@P_MSGOUT", SqlDbType.VarChar, 100).Value = "Out";
           cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           cmd.ExecuteNonQuery();
           strOutput = (string)cmd.Parameters["@P_MSGOUT"].Value;
           cmd.Connection.Close();
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   #region "To Fill District"

   public override DataTable FillDistrict()
   {
       DataTable dt = new DataTable();
       try
       {
           dt = ObjCmnDll.GetDataTable(ConnectionString, "SELECT intDistrictId,vchDistrictName FROM M_District WHERE bitDeletedFlag<>1 and intStateId=20 Order by vchDistrictName");
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }

   #endregion
   public override string AddCostDesc(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_AMS_COSTDTLS_MASTER", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
           cmd.Parameters.AddWithValue("@PVCHCOST", objAMS.CostDtls);
           cmd.Parameters.AddWithValue("@PChecklistId", objAMS.CostID);
           cmd.Parameters.AddWithValue("@PINT_CREATED_BY", objAMS.CreatedBy);
           cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
           cmd.Parameters["@P_MSGOUT"].Size = 100;
           cmd.ExecuteNonQuery();
           strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   public override DataTable FillCostDtls()
   {
       DataTable dt = new DataTable();
       try
       {
           dt = ObjCmnDll.GetDataTable(ConnectionString, "SELECT INT_COST_ID,VCH_COST_DTLS_DESC,INT_DELETED_FLAG FROM M_AMS_PROJECTCOST_DTLS");
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }
   public override string ActiveCostDescription(Agenda objA)
   {
       string strOutput = "0";
       try
       {
           SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.CommandText = "USP_AMS_COSTDTLS_MASTER";
           cmd.CommandTimeout = 0;
           cmd.Parameters.Add("@PCH_ACTION", SqlDbType.VarChar).Value = objA.Action;
           cmd.Parameters.Add("@PChecklistId", SqlDbType.Int).Value = objA.Id;
           cmd.Parameters.Add("@P_MSGOUT", SqlDbType.VarChar, 100).Value = "Out";
           cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           cmd.ExecuteNonQuery();
           strOutput = (string)cmd.Parameters["@P_MSGOUT"].Value;
           cmd.Connection.Close();
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   public override DataTable FillCostDetails()
   {
       DataTable dt = new DataTable();
       try
       {
           dt = ObjCmnDll.GetDataTable(ConnectionString, "SELECT INT_COST_ID,VCH_COST_DTLS_DESC FROM M_AMS_PROJECTCOST_DTLS WHERE INT_DELETED_FLAG<>1");
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }
   public override string AddFinDtls(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_AMS_COSTDTLS_MASTER", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
           cmd.Parameters.AddWithValue("@PVCHFINDTLS", objAMS.FinDtls);
           cmd.Parameters.AddWithValue("@PChecklistId", objAMS.FinID);
           cmd.Parameters.AddWithValue("@PINT_CREATED_BY", objAMS.CreatedBy);
           cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
           cmd.Parameters["@P_MSGOUT"].Size = 100;
           cmd.ExecuteNonQuery();
           strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   public override DataTable FillFinDtls()
   {
       DataTable dt = new DataTable();
       try
       {
           dt = ObjCmnDll.GetDataTable(ConnectionString, "SELECT INT_FIN_ID,VCH_FIN_DTLS_DESC,INT_DELETED_FLAG FROM M_AMS_FINANCE_DTLS");
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }
   public override string ActiveFinDescription(Agenda objA)
   {
       string strOutput = "0";
       try
       {
           SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.CommandText = "USP_AMS_COSTDTLS_MASTER";
           cmd.CommandTimeout = 0;
           cmd.Parameters.Add("@PCH_ACTION", SqlDbType.VarChar).Value = objA.Action;
           cmd.Parameters.Add("@PChecklistId", SqlDbType.Int).Value = objA.Id;
           cmd.Parameters.Add("@P_MSGOUT", SqlDbType.VarChar, 100).Value = "Out";
           cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           cmd.ExecuteNonQuery();
           strOutput = (string)cmd.Parameters["@P_MSGOUT"].Value;
           cmd.Connection.Close();
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   public override DataTable FillFinDetails()
   {
       DataTable dt = new DataTable();
       try
       {
           dt = ObjCmnDll.GetDataTable(ConnectionString, "SELECT INT_FIN_ID,VCH_FIN_DTLS_DESC FROM M_AMS_FINANCE_DTLS WHERE INT_DELETED_FLAG<>1");
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }
   public override string UpdateClarification(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_AMS_FEEDBACK", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
           cmd.Parameters.AddWithValue("@PVCHCOMMENT", objAMS.COMMENT);
           cmd.Parameters.AddWithValue("@PINT_PROJCTID", objAMS.ProjectId);        
           cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
           cmd.Parameters["@P_MSGOUT"].Size = 100;
           cmd.ExecuteNonQuery();
           strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   public override DataTable ViewSlfcFeedback(AMS objAMS)
   {
       try
       {
           DataTable dt = new DataTable();

           Object[] objParam = { "@PCH_ACTION", objAMS.Action,"@USERID", objAMS.ID };

           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_SLFC_FEEDBACK", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;

       }
   }
   public override string UpdateFeedback(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_AMS_FEEDBACK", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
           cmd.Parameters.AddWithValue("@PVCHFEEDBACK", objAMS.Feedback);
           cmd.Parameters.AddWithValue("@PINT_FEEDBACKID", objAMS.FeedbackId);
           cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
           cmd.Parameters["@P_MSGOUT"].Size = 100;
           cmd.ExecuteNonQuery();
           strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   public override string UpdateClarificationGM(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_AMS_FEEDBACK", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
           cmd.Parameters.AddWithValue("@PVCHCLARIFICATION", objAMS.Clarification);
           cmd.Parameters.AddWithValue("@PINT_CLARIFICATIONID", objAMS.ClarificationId);
           cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
           cmd.Parameters["@P_MSGOUT"].Size = 100;
           cmd.ExecuteNonQuery();
           strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   public override string AddFinDetails(Agenda objA)
   {
       string strOutput = "0";
       try
       {
           SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
           SqlCommand cmd = new SqlCommand();
           cmd.Connection = conn;
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.CommandText = "USP_AMS_PROJECTDETAILS";
           cmd.CommandTimeout = 0;
           cmd.Parameters.Add("@PCH_ACTION", SqlDbType.VarChar).Value = objA.Action;
           cmd.Parameters.Add("@PintProjectId", SqlDbType.VarChar).Value = objA.ProjectId;
           cmd.Parameters.Add("@PvchFinanceDescription", SqlDbType.VarChar).Value = objA.FinanceDescription;
           cmd.Parameters.Add("@PVCH_REMARK", SqlDbType.VarChar).Value = objA.Remark;
           cmd.Parameters.Add("@PINT_REMARKID", SqlDbType.Int).Value = objA.intRemarkID;
           cmd.Parameters.Add("@PintCreatedBy", SqlDbType.Int).Value = objA.CreatedBy;
           cmd.Parameters.Add("@P_XML_FINTABLE", SqlDbType.Xml).Value = objA.VCH_XMLFINTBL;
           cmd.Parameters.Add("@P_MSGOUT", SqlDbType.VarChar, 100).Value = "Out";
           cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
           cmd.Connection.Open();
           cmd.ExecuteNonQuery();
           strOutput = (string)cmd.Parameters["@P_MSGOUT"].Value;
           cmd.Connection.Close();
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           ObjCmnDll = null;
       }
   }
   public override DataSet ViewForwordDetails(Agenda objA)
   {
       DataSet ds = new DataSet();
       try
       {
           Object[] objParam = { "@PCH_ACTION", objA.Action, "@PintDetailsId", objA.Id, "@PintProjectId", objA.ProjectId };

           ds = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_PROJECTDETAILS", objParam);
           return ds;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }
   public override DataTable FillInactiveTermsConditions()
   {
       DataTable dt = new DataTable();
       try
       {
           dt = ObjCmnDll.GetDataTable(ConnectionString, "SELECT intChecklistId AS ChecklistId,vchChecklistPoint AS ChecklistPoint,bitDeletedFlag FROM M_AMS_SLFC_CHECKLIST where bitDeletedFlag=1");
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }
   public override DataTable FillActiveTermCondition()
   {
       DataTable dt = new DataTable();
       try
       {
           dt = ObjCmnDll.GetDataTable(ConnectionString, "SELECT intChecklistId AS ChecklistId,vchChecklistPoint AS ChecklistPoint,bitDeletedFlag FROM M_AMS_SLFC_CHECKLIST where bitDeletedFlag=0");
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {

       }
   }
   public override DataTable EditSLFCDiscussion(AMS objAMS)
   {
       try
       {
           DataTable dt = new DataTable();

           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PChecklistId", objAMS.ID };

           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_SLFC_DECISION", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;

       }
   }
   public override string UpdateComments(AMS objAMS)
   {
       try
       {
           string strOutput = "";
           Object[] objParam = { "@PCH_ACTION", objAMS.Action, "@PChecklistId", objAMS.ID, "@PVCHDECISION", objAMS.DECISION
                                  ,"@P_MSGOUT", "out" };
           strOutput = Convert.ToString(ObjCmnDll.ExeNonQry(ConnectionString, "USP_AMS_SLFC_DECISION", objParam));
           return strOutput;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;
       }
   }

   public override DataTable FillForwardProject(AMS objA)
   {
       DataTable dt = new DataTable();
       try
       {
           Object[] objParam = { "@ACTIONCODE", objA.Action, "@INTOFFICERID",objA.OfficerId };

           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_M_SWP_PROJECT", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           objA = null;
       }
   }

   public override string UpdateForwardProject(AMS objA)
   {
       string strRes = "";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_M_SWP_PROJECT", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@ACTIONCODE", objA.Action);
           cmd.Parameters.AddWithValue("@INTPROJECTID", objA.ProjectId);
           cmd.Parameters.AddWithValue("@INTOFFICERID", objA.OfficerId);
           cmd.Parameters.AddWithValue("@vchGMRemark", objA.Remark);
           cmd.Parameters.AddWithValue("@vchRes", "out");
           cmd.Parameters["@vchRes"].Direction = ParameterDirection.Output;
           cmd.Parameters["@vchRes"].DbType = System.Data.DbType.String;
           cmd.ExecuteNonQuery();
           strRes = Convert.ToString(cmd.Parameters["@vchRes"].Value);
           return strRes;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           objA = null;
       }
   }

   #region "Ropen Published Project"
   /// <summary>
   /// For Reopening Published Project
   /// </summary>
   /// <param name="objAMS"></param>
   /// <returns>strOutput</returns>
   public override string Reopen_Published_Project(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_AMS_PROJECT_REOPEN", con);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.AddWithValue("@CHARACTION", objAMS.Action);
           cmd.Parameters.AddWithValue("@INTPROJCTID", objAMS.ProjectId);
           cmd.Parameters.AddWithValue("@VCHMSGOUT", "out");
           cmd.Parameters["@VCHMSGOUT"].Direction = ParameterDirection.Output;
           cmd.Parameters["@VCHMSGOUT"].DbType = System.Data.DbType.String;
           cmd.Parameters["@VCHMSGOUT"].Size = 200;
           cmd.ExecuteNonQuery();
           strOutput = Convert.ToString(cmd.Parameters["@VCHMSGOUT"].Value);
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           cmd.Dispose();
           con.Dispose();
       }
   }
   #endregion

   public override string InsertStatus(AMS objAMS)
   {

       string strOutput = "0";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_SWP_STATUS_LOG", con);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.AddWithValue("@PvchMessage", objAMS.Remark);
           cmd.Parameters.AddWithValue("@VCHMSGOUT", "out");
           cmd.Parameters["@VCHMSGOUT"].Direction = ParameterDirection.Output;
           cmd.Parameters["@VCHMSGOUT"].DbType = System.Data.DbType.String;
           cmd.Parameters["@VCHMSGOUT"].Size = 200;
           cmd.ExecuteNonQuery();
           strOutput = Convert.ToString(cmd.Parameters["@VCHMSGOUT"].Value);
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           cmd.Dispose();
           con.Dispose();
       }
   }

   public override string InsertAccountantComment(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_AMS_PROJECTDETAILS", con);
           cmd.CommandType = CommandType.StoredProcedure;

           cmd.Parameters.AddWithValue("@PCH_ACTION", objAMS.Action);
           cmd.Parameters.AddWithValue("@PintProjectId", objAMS.ProjectId);
           cmd.Parameters.AddWithValue("@PintCreatedBy", objAMS.OfficerId);
           cmd.Parameters.AddWithValue("@PVCH_ACCOUNTANT_REMARK", objAMS.Remark);
           cmd.Parameters.AddWithValue("@P_MSGOUT", "out");
           cmd.Parameters["@P_MSGOUT"].Direction = ParameterDirection.Output;
           cmd.Parameters["@P_MSGOUT"].DbType = System.Data.DbType.String;
           cmd.Parameters["@P_MSGOUT"].Size = 200;
           cmd.ExecuteNonQuery();
           strOutput = Convert.ToString(cmd.Parameters["@P_MSGOUT"].Value);
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           cmd.Dispose();
           con.Dispose();
       }
   }

   public override DataTable FillAccountantComment(AMS objA)
   {
       DataTable dt = new DataTable();
       try
       {
           Object[] objParam = { "@PCH_ACTION", objA.Action, "@PintProjectId", objA.ProjectId };

           dt = ObjCmnDll.GetDataTable(ConnectionString, "USP_AMS_PROJECTDETAILS", objParam);
           return dt;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           objA = null;
       }
   }
   public override DataSet GetProjectCnt(AMS objAMS)
   {
       try
       {
           DataSet ds = new DataSet();
           Object[] objParam = { "@CHRACTION", objAMS.Action};
           ds = ObjCmnDll.GetDataSet(ConnectionString, "USP_AMS_DASHBOARD", objParam);
           return ds;
       }
       catch (Exception ex)
       {
           throw new Exception(ex.Message);
       }
       finally
       {
           ObjCmnDll = null;

       }
    }

   public override string UpdateStatus(AMS objAMS)
   {
       string strOutput = "0";
       try
       {
           cmd = new SqlCommand();
           cmd = new SqlCommand("USP_Add_Agenda", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.AddWithValue("@PAction", "A");
           cmd.Parameters.AddWithValue("@PvchProposalNo", objAMS.strUID);
           cmd.Parameters.AddWithValue("@PstrStatus", objAMS.TypeId);
           cmd.Parameters.AddWithValue("@PvchRemark", objAMS.Remark);
           cmd.Parameters.AddWithValue("@PvchUrl", objAMS.strUrl);
           cmd.Parameters.AddWithValue("@PvchRecomendLand", objAMS.strlandVal);
           cmd.Parameters.AddWithValue("@P_OUT_MSG", "out");
           cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
           cmd.Parameters["@P_OUT_MSG"].DbType = System.Data.DbType.String;
           cmd.Parameters["@P_OUT_MSG"].Size = 200;
           cmd.ExecuteNonQuery();
           strOutput = Convert.ToString(cmd.Parameters["@P_OUT_MSG"].Value);
           return strOutput;
       }
       catch (Exception ex)
       {
           throw ex;
       }
       finally
       {
           cmd.Dispose();
           con.Dispose();
       }
   }
}
