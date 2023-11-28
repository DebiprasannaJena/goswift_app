#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Net;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Configuration;
using System.Web.Services;
using BusinessLogicLayer.Service;
using EntityLayer.Service;
using System.Globalization;
using System.Web.UI;
using System.Data.SqlClient;
using BusinessLogicLayer.Incentive;
using System.Security.Cryptography;
using System.Globalization;
using System.Configuration;
using System.Text;
#endregion


public partial class Portal_Dashboard_DepartmentDashboard : SessionCheck
{
    #region Global variable
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();
    int idco = Convert.ToInt32(ConfigurationManager.AppSettings["DeptIdIDCO"]);
    int idcoW = Convert.ToInt32(ConfigurationManager.AppSettings["DeptIdIDCOW"]);
    int LabDirectorate = Convert.ToInt32(ConfigurationManager.AppSettings["DeptIdLabDirectorate"]);
    int OSPCB = Convert.ToInt32(ConfigurationManager.AppSettings["DeptIdOSPCB"]);
    int FactBoil = Convert.ToInt32(ConfigurationManager.AppSettings["DeptIdFactBoil"]);
    int LegalMetrology = Convert.ToInt32(ConfigurationManager.AppSettings["DeptIdLegalMetrology"]);
    // int ChiefElecInsp =Convert.ToInt32( ConfigurationManager.AppSettings["DeptIdChiefElecInsp"]);
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    CommonMethodForDashboard commonMethodobj = new CommonMethodForDashboard();
    string UserId = string.Empty;
    int intDeptId = 0;
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
        else
        {
            UserId = Session["UserId"].ToString();
            string da = Session["DeptId"].ToString();
            intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
        }
        if (!IsPostBack)
        {

            spLastUpdate.InnerText = DateTime.Now.ToString();
            /*-----------------------------------------------------------------*/
            ///Fill Dropdownlist for Financial Year
            /*-----------------------------------------------------------------*/
            commonMethodobj.FillFinancialYear(ddlFinacialYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on master tracker
            commonMethodobj.FillFinancialYearWithYear(ddlspmgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year of spmg 
            commonMethodobj.FillFinancialYear(ddlyearquery); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
            DashboradCommon.MONTHBIND(ddlCICGMonth); //bind both value 1 and  text January    Month value  on Normal platform
            DashboradCommon.YEARBIND(ddlYearCICG);  //bind both value 2016 and  text 2016    Year value  on Normal platform
            DashboradCommon.MONTHBIND(ddlAppaMonth); //bind both value 1 and  text January    Month value  on Normal platform
            commonMethodobj.FillFinancialYear(ddlAppaYear); //bind both value field  2016-17 and  text 2016-17  FinalcealYear on Normal platform

            /*-----------------------------------------------------------------*/
            //// Master Tracker
            /*-----------------------------------------------------------------*/
            ViewServiceMaster();  //Pending Approvals
            ViewSPMGMasterData();//State Project Monitoring Group
            ViewCICGMasterData();//Central Inspection Framework



            /*-----------------------------------------------------------------*/
            ////Portlet Section
            /*-----------------------------------------------------------------*/
            commonMethodobj.FillDepartment(ddldept); // bind Department record in dropdown list
            ddldept.SelectedValue = Convert.ToString(intDeptId);
            ddldept.Enabled = false;
            commonMethodobj.FillService(ddldept, ddlService);// bind service record in  dropdown list
            ViewServiceStatus();//DEPARTMENT WISE APPROVALS

            if (ConfigurationManager.AppSettings["SPMG"] == "ON")
            {
                ViewSPMGServiceStateLevelDataDirect();// STATE PROJECT MONITORING GROUP
                ViewSPMGServiceDistrictLevelDataDirect(); //STATE PROJECT MONITORING GROUP
            }

               


            ViewQueryService(); //QUERY MONITORING


            commonMethodobj.FillDepartmentForCIF(ddldeptCIF);  // bind Department record in CIF dropdown list
            //InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
           // ViewCICGData(); //CENTRAL INSPECTION FRAMEWORK


            commonMethodobj.FillDist(ddlAPAADistrict);// bind dist in dropdown list  for IDCO POST ALLOTMENT APPLICATIONS 


            // BindDept();
           // BindDeptCICG();
           // FillFinYrPortletS(ddlspmgyear);
           // int intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
            //ddldept.SelectedValue = Convert.ToString(intDeptId);
            //ddldept.Enabled = false;
           // BindService();
            if (intDeptId == idco || intDeptId == idcoW)
            {
                dvapaa.Style.Add("display", "block");
                dvAPAAMast.Style.Add("display", "block");
                if (ConfigurationManager.AppSettings["IDCO"] == "ON")
                {
                    InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
                }
                   
               // FillFinYrPortlet(ddlAppaYear);
            }
            else if (intDeptId == LabDirectorate || intDeptId == OSPCB || intDeptId == FactBoil || intDeptId == LegalMetrology)
            {
                dvCICG.Style.Add("display", "block");
                dvCICGMast.Style.Add("display", "block");
                if (ConfigurationManager.AppSettings["CICG"] == "ON")
                {
                    InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
                }
                    
                ddldeptCIF.SelectedValue = Convert.ToString(objserviceDashboard.GetCIFDepartmentid(Session["DeptId"].ToString()));
                ddldeptCIF.Enabled = false;
            }
            // spLastUpdate.InnerText = DateTime.Now.ToString();
            // FillMasterFinYr();
            if (ConfigurationManager.AppSettings["IDCO"] == "ON")
            {
                InsertAppa(ddlFinacialYear);
            }
               
            //Added BY Pritiprangya pattanaik on 15-jan-2017 to add and view spmg department wise status count

            //ViewSPMGDEPTwiseCount();
            //InsertSPMGStatus();

            //Added BY Pritiprangya pattanaik on 15-jan-2017 to add and view spmg department wise status count
          //  string strDep = Session["DeptId"].ToString();
            //ViewServiceStatus();
           // ViewServiceMaster();
            ViewCICGData();//CENTRAL INSPECTION FRAMEWORK
           // MONTHBIND(ddlAppaMonth);
           // MONTHBIND(ddlCICGMonth);
          //  YEARBIND(ddlAppaYear);
           // FillFinYrPortlet(ddlyearquery);
          //  YEARBIND(ddlYearCICG);
           // BindDistrictAPAA();
            ViewApaaStatus();

            //YEARBIND(ddlyearquery);
           // ViewQueryService();
           // ViewCICGMasterData();
            // ViewSPMGData();
            // BindInnerSPMG();            
           // ViewSPMGServiceStateLevelDataDirect();
          //  ViewSPMGServiceDistrictLevelDataDirect();
           // ViewSPMGMasterData();

            //added by Ritika Lath For incentive portlet 13th dec 2017
          //  FillIncentiveApplicationDropdown();

            ////For Grievance
            //FillFinYrPortletS(ddlgyear);
            //BindDistrictGrievance();
            //BindMasterGrievanceportlet();
            //BindGrievanceportlet();
        }
    }
    #endregion

    private void ViewQueryService() //view QUERY MONITORING on normal platfrom
    {

       // objSWP = new SWPDashboard();
       // objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            //objSWP.strAction = "QV";
            //// objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            ////objSWP.intDeptId = 0;//suggested by girija
            ////objSWP.intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());
            //objSWP.Year = ddlyearquery.SelectedValue;
            //objSWP.intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
            //List<SWPDashboard> objServiceStatus = objserviceDashboard.GetServicesQuery(objSWP).ToList();

            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewQueryServiceDept (ddlyearquery.SelectedValue ,intDeptId);
            hdnsrvc.Value = Convert.ToString(intDeptId);
            if (objServiceStatus.Count > 0)
            {
                spRaised.InnerText = objServiceStatus[0].strTotalQueryRaised.ToString();
                spRevert.InnerText = objServiceStatus[0].strTotalQueryResponse.ToString();
                spPending.InnerText = objServiceStatus[0].strTotalQueryPending.ToString();
                spResponseTimeline.InnerText = objServiceStatus[0].strTotQuerynotRecTimeline.ToString();
                spAvgTime.InnerText = objServiceStatus[0].strAvgRaiseQuery.ToString();
            }
            else
            {
                spRaised.InnerText = "0";
                spRevert.InnerText = "0";
                spPending.InnerText = "0";
                spResponseTimeline.InnerText = "0";
                spAvgTime.InnerText = "0";

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
           // objSWP = null;
           // objserviceDashboard = null;
        }
    }

    #region added by ritika lath 13th dec 2017
    //private void FillIncentiveApplicationDropdown()
    //{
    //    IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
    //    DataSet objDa = objBuisnessLayer.BindDropdown("rpt");
    //    if (objDa != null)
    //    {
    //        FillDropDown(objDa.Tables[0], "Unit Type", drpInctUnitType);
    //        FillDropDown(objDa.Tables[1], "District", drpInctAppDistrict);
    //        FillDropDown(objDa.Tables[2], "Policy", drpPolicy);
    //    }
    //    for (int i = 2010; i <= DateTime.Today.Year; i++)
    //    {
    //        drpInctYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //    }
    //    drpInctYear.Items.Insert(0, new ListItem("-Select Year-", "0"));
    //    //drpInctYear.SelectedValue = DateTime.Today.Year.ToString();
    //}

    /// <summary>
    /// Function to bind the drodown, main code to bind the dropdown
    /// </summary>
    /// <param name="objDt">Datatable with all values</param>
    /// <param name="strHeaderType">type of data in dropdown</param>
    /// <param name="objDropdown">dropdown to bind</param>
    //private void FillDropDown(DataTable objDt, string strHeaderType, DropDownList objDropdown)
    //{
    //    objDropdown.Items.Clear();
    //    if (objDt != null && objDt.Rows.Count > 0)
    //    {
    //        objDropdown.DataSource = objDt;
    //        objDropdown.DataTextField = "NAME";
    //        objDropdown.DataValueField = "ID";
    //        objDropdown.DataBind();
    //    }
    //    objDropdown.Items.Insert(0, new ListItem(string.Format("-Select {0}-", strHeaderType), "0"));
    //}
    #endregion

    #region APAA portlet
    //private void BindDistrictAPAA()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlAPAADistrict.DataSource = objProjList;
    //    ddlAPAADistrict.DataTextField = "vchDistName";
    //    ddlAPAADistrict.DataValueField = "intDistId";
    //    ddlAPAADistrict.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlAPAADistrict.Items.Insert(0, list);

    //}

    private void InsertAppaStatus(DropDownList ddlDistrict, DropDownList ddlMonth, DropDownList ddlYear)
    {
        string finalquery = string.Empty;
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = string.Empty;
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string District = string.IsNullOrEmpty(ddlAPAADistrict.SelectedValue) ? default(string) : ddlAPAADistrict.SelectedValue.ToString();
        string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
        string Year = string.IsNullOrEmpty(ddlAppaYear.SelectedValue) ? default(string) : ddlAppaYear.SelectedValue.ToString();
        string Deptid = "0";
        //string UniqueKey = Session["UID"].ToString();
        string UniqueKey = "0";
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //objlist = objserviceDashboard.CheckAppastatus(objSWP);
        //if (objlist.Count > 0)
        //{
        //    if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
        //    {
        //        Type = "0";
        //    }
        //    else if (objlist[0].intStatus != 1 && objlist[0].intDistrictid == 0)//IPICOL Largescale
        //    {
        Type = "0";
        //    }
        //    else//MSME
        //    {
        //        Type = "2";
        //    }
        //}
        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + District + "/" + Deptid + "/" + Type + "/" + Year + "/" + Month;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                    string output = DynTable.Rows[0]["getSWPConsolidationDataResult"].ToString();
                    string[] finalOut = output.Split(':');
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "SELECT ADMINID FROM T_APAA_Service_Admin WHERE CONVERT(DATE,dtmCreatedon)='" + DateTime.Now.ToString("dd-MMM-yy") + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        finalquery = "UPDATE T_APAA_Service_Admin SET adminid=" + Convert.ToInt32(Session["Userid"]) + ",TotalApplied=" + finalOut[2].ToString() + ",TotalDisposed=" + DynTable.Rows[0]["TotalDisposed"].ToString() + "," +
                             "TotalMajorPendingIdco=" + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + ",TotalPendingIdco=" + DynTable.Rows[0]["TotalPendingIdco"].ToString() + ",TotalPendingUnit=" + DynTable.Rows[0]["TotalPendingUnit"].ToString() + ",dtmCreatedon='" + DateTime.Now.ToString("dd-MMM-yy") + "',intYearId='" + Year + "',intDistrictId=" + District + ",intMonthId=" + Month + " WHERE adminid='" + ds.Tables[0].Rows[0]["ADMINID"].ToString() + "'";
                    }
                    else
                    {
                        finalquery = "INSERT INTO T_APAA_Service_Admin(adminid,TotalApplied,TotalDisposed,TotalMajorPendingIdco,TotalPendingIdco,TotalPendingUnit,dtmCreatedon,intYearId,intDistrictId,intMonthId)" +
                    "VALUES(" + Convert.ToInt32(Session["Userid"]) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["TotalDisposed"].ToString() + "," + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingUnit"].ToString() + "," +
                    "'" + DateTime.Now.ToString("dd-MMM-yy") + "','" + Year + "'," + District + "," + Month + ")";
                    }
                    cmd = new SqlCommand(finalquery, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    private void ViewApaaStatus()
    {

        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "AP";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objSWP.Year = ddlAppaYear.SelectedItem.Text;
            objSWP.intMonthId = Convert.ToInt32(ddlAppaMonth.SelectedValue);
            objSWP.intDistrictid = Convert.ToInt32(ddlAPAADistrict.SelectedValue);
            List<SWPDashboard> objPEALStatusList = objserviceDashboard.GetDashboardAPAAtatus(objSWP).ToList();
            if (objPEALStatusList.Count > 0)
            {
                spchngrqstApplied.InnerHtml = objPEALStatusList[0].strChngReqApplied.ToString();
                spchngreqdispose.InnerHtml = objPEALStatusList[0].strChngReqDispose.ToString();
                spchngreqPendAtIDCO.InnerHtml = objPEALStatusList[0].strChngReqPendingAtIDCO.ToString();
                spchngReqCrossThirty.InnerHtml = objPEALStatusList[0].strChngReqCrossthirty.ToString();
                spnPendingatUnit.InnerHtml = objPEALStatusList[0].strChngReqPendAtUnit.ToString();
            }
            else
            {
                spchngrqstApplied.InnerHtml = "0";
                spchngreqdispose.InnerHtml = "0";
                spchngreqPendAtIDCO.InnerHtml = "0";
                spchngReqCrossThirty.InnerHtml = "0";
                spnPendingatUnit.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    protected void btnAPAASubmit_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["IDCO"] == "ON")
        {
            InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
        }
           
        ViewApaaStatus();
    }

    private void InsertAppa(DropDownList ddlYear)
    {
        string finalquery = string.Empty;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = string.Empty;
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        //string District = string.IsNullOrEmpty(ddlAPAADistrict.SelectedValue) ? default(string) : ddlAPAADistrict.SelectedValue.ToString();
        //string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
        string Year = string.IsNullOrEmpty(ddlYear.SelectedValue) ? default(string) : ddlYear.SelectedValue.ToString();
        string Deptid = "0";
        //string UniqueKey = Session["UID"].ToString();
        string UniqueKey = "0";
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //objlist = objserviceDashboard.CheckAppastatus(objSWP);
        //if (objlist.Count > 0)
        //{
        //if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
        //{
        Type = "0";
        //    }
        //    else if (objlist[0].intStatus != 1 && objlist[0].intDistrictid == 0)//IPICOL Largescale
        //    {
        //        Type = "1";
        //    }
        //    else//MSME
        //    {
        //        Type = "2";
        //    }
        //}
        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + "0" + "/" + Deptid + "/" + Type + "/" + Year + "/" + "0";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                    string output = DynTable.Rows[0]["getSWPConsolidationDataResult"].ToString();
                    string[] finalOut = output.Split(':');
                    spAPAAPending.InnerText = DynTable.Rows[0]["TotalPendingIdco"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            spAPAAPending.InnerText = "0";
            Util.LogError(ex, "Dashboard");
        }
    }
    #endregion

    #region CICG Portlet
    private void InsertCICGStatus(DropDownList ddlCICGMonth, DropDownList ddldeptCIF, DropDownList ddlYearCICG)
    {
        string finalquery = string.Empty;
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = string.Empty;
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string Department = string.IsNullOrEmpty(ddldeptCIF.SelectedValue) ? default(string) : ddldeptCIF.SelectedValue.ToString();

        if (Session["DeptId"].ToString() != null)
        {
            if (ddldeptCIF.SelectedIndex > 0)
            {
                Department = string.IsNullOrEmpty(ddldeptCIF.SelectedValue) ? default(string) : ddldeptCIF.SelectedValue.ToString();
            }
            else
            {
                Department = objserviceDashboard.GetCIFDepartmentid(Session["DeptId"].ToString());
            }
        }
        else
        {
            Department = string.IsNullOrEmpty(ddldeptCIF.SelectedValue) ? default(string) : ddldeptCIF.SelectedValue.ToString();
        }
        string Month = string.IsNullOrEmpty(ddlCICGMonth.SelectedValue) ? default(string) : ddlCICGMonth.SelectedValue.ToString();
        string Year = string.IsNullOrEmpty(ddlYearCICG.SelectedValue) ? default(string) : ddlYearCICG.SelectedValue.ToString();
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);

        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["CICGServiceURL"] + "GetInspectionDtls/" + Department + "/" + Month + "/" + Year;
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "GET";
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string strResult = (new StreamReader(stream)).ReadToEnd();
                    DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                    string output = DynTable.Rows[0]["GetInspectionDtlsResult"].ToString();
                    string[] finalOut = output.Split(':');
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "SELECT INT_ID FROM T_CICG_DASHBOARD WHERE CONVERT(DATE,DTM_CREATED_ON)='" + DateTime.Now.ToString("dd-MMM-yy") + "' and INT_ID = " + Convert.ToInt32(Session["Userid"]);
                    SqlDataAdapter adp = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        finalquery = "UPDATE T_CICG_DASHBOARD SET INT_ID=" + Convert.ToInt32(Session["Userid"]) + ",INT_INS_COMPLETED=" + finalOut[2].ToString() + ",INT_INS_SCHEDULED=" + DynTable.Rows[0]["InspectionScheduled"].ToString() + "," +
                             "INT_REPORT_PENDING=" + DynTable.Rows[0]["PendingInspection"].ToString() + ",INT_ReportNot_Uploaded=" + DynTable.Rows[0]["ReportNotUploaded"].ToString() + ",INT_UNATTENDED_INS=" + DynTable.Rows[0]["UnAttendedInspection"].ToString() + ",DTM_CREATED_ON='" + DateTime.Now.ToString("dd-MMM-yy")
                             + "',intDeptId=" + Department + ",intMonthId=" + Month + ",intYearId=" + Year + " WHERE INT_ID='" + ds.Tables[0].Rows[0]["INT_ID"].ToString() + "'";

                    }
                    else
                    {

                        finalquery = "INSERT INTO T_CICG_DASHBOARD(INT_ID,INT_INS_COMPLETED,INT_INS_SCHEDULED,INT_REPORT_PENDING,INT_ReportNot_Uploaded,INT_UNATTENDED_INS,DTM_CREATED_ON,intDeptId,intMonthId,intYearId)" +
                        "VALUES(" + Convert.ToInt32(Session["Userid"]) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["InspectionScheduled"].ToString() + "," + DynTable.Rows[0]["PendingInspection"].ToString() + ","
                        + DynTable.Rows[0]["ReportNotUploaded"].ToString() + "," + DynTable.Rows[0]["UnAttendedInspection"].ToString() + "," +
                        "'" + DateTime.Now.ToString("dd-MMM-yy") + "'," + Department + "," + Month + "," + Year + ")";
                    }
                    cmd = new SqlCommand(finalquery, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    private void ViewCICGData() //view CENTRAL INSPECTION FRAMEWORK on normal platfrom
    {

       // objSWP = new SWPDashboard();
       // objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            //objSWP.strAction = "VCI";
            //objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            //objSWP.intDeptId = Convert.ToInt32(ddldeptCIF.SelectedValue);
            //objSWP.intMonthId = Convert.ToInt32(ddlCICGMonth.SelectedValue);
            //objSWP.intYearId = Convert.ToInt32(ddlYearCICG.SelectedValue);
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewCICGData(Convert.ToInt32(Session["Userid"]), Convert.ToInt32(ddldeptCIF.SelectedValue), Convert.ToInt32(ddlCICGMonth.SelectedValue), Convert.ToInt32(ddlYearCICG.SelectedValue));
            // List<SWPDashboard> objServiceStatus = objserviceDashboard.GetCICGDashboardService(objSWP).ToList();
            if (objServiceStatus.Count > 0)
            {
                SPcicgapplied.InnerHtml = objServiceStatus[0].INT_INS_SCHEDULED.ToString();
                SPcicgcompleted.InnerHtml = objServiceStatus[0].INT_INS_COMPLETED.ToString();
                SPunattInsdash.InnerHtml = objServiceStatus[0].INT_UNATTENDED_INS.ToString();
                SPReprtNotUploaded.InnerHtml = objServiceStatus[0].INT_REPORTNOT_UPLOADED.ToString();

            }
            else
            {
                SPcicgapplied.InnerHtml = "0";
                SPcicgcompleted.InnerHtml = "0";
                SPunattInsdash.InnerHtml = "0";
                SPReprtNotUploaded.InnerHtml = "0";

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    private void ViewCICGMasterData() // Mothod for bind Central Inspection Framework on master tracker
    {
        //objSWP = new SWPDashboard();
       // objserviceDashboard = new DashboardBusinessLayer();
        try
        {

            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewCICGMasterData(Convert.ToInt32(UserId));
            if (objServiceStatus.Count > 0)
            {
                SPcicgpending.InnerText = objServiceStatus[0].INT_REPORT_PENDING.ToString();
            }
            else
            {
                SPcicgpending.InnerText = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }

    //private void BindDeptCICG()
    //{
    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();
    //    objSWP.strAction = "CDP";
    //    List<SWPDashboard> objServiceStatus = objserviceDashboard.BindCICGDepartment(objSWP).ToList();
    //    ddldeptCIF.DataSource = objServiceStatus;
    //    ddldeptCIF.DataTextField = "VCH_DEPT_NAME";
    //    ddldeptCIF.DataValueField = "intDeptId";
    //    ddldeptCIF.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddldeptCIF.Items.Insert(0, list);
    //}

    protected void btnCICGStatus_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["CICG"] == "ON")
        {
            InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
        }
           
        ViewCICGData();
    }
    #endregion

    #region Dashboard SPMG Data

    public void ViewSPMGServiceStateLevelDataDirect()
    {
        try
        {
            objserviceDashboard = new DashboardBusinessLayer();
            //Random number generate
            string strrandomgen = MakeRandom(10);
            var plainran = Encoding.UTF8.GetBytes(strrandomgen);
            string randno = Convert.ToBase64String(plainran);

            //Timestamp
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            double unixTime = span.TotalSeconds;
            var plainut = Encoding.UTF8.GetBytes(unixTime.ToString());
            string plunixtime = Convert.ToBase64String(plainut);

            //PasswordDigest
            string ranpss = strrandomgen + "$CSDa2017@shbo@rD$Ipic)L" + unixTime.ToString();
            SHA256 mySHA256 = SHA256Managed.Create();
            string finalstr = GetSha256FromString(ranpss);
            byte[] bytes = Encoding.UTF8.GetBytes(finalstr);
            string ranpss1 = Convert.ToBase64String(bytes);

            //Financial year
            string FinYear = ddlspmgyear.SelectedValue;

           // string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
            string strdeptid = objserviceDashboard.GetSPMGDepartment(Convert.ToString(intDeptId)).ToString();

            string serviceUrl = "https://164.100.58.41//odisha/restservices/RestServer.php?view=issuestatusbydepartmentid";

            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
                FinancialYear = FinYear,
                DepartmentID = strdeptid
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            var webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            webRequest.Method = WebRequestMethods.Http.Post;
            webRequest.ContentType = "application/json";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            }
            catch
            {
            }
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            using (var requestStream = webRequest.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(json);
                }
            }

            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var responseStream = webResponse.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        try
                        {
                            var responseData = reader.ReadToEnd();
                            webResponse.Close();

                            string strResult = responseData.ToString();
                            if (strResult != "")
                            {
                                DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                                if (DynTable.Rows.Count > 0)
                                {
                                    InsertViewStatelevelSPMGData(DynTable.Rows[0]["Issues Received"].ToString(), DynTable.Rows[0]["Issues Resolved"].ToString(),
                                    DynTable.Rows[0]["Issues Pending"].ToString(), DynTable.Rows[0]["Issues Pending (more than 30 days)"].ToString(), strdeptid);
                                }
                                else
                                {
                                    spmgraised.InnerHtml = "0";
                                    spmgresolved.InnerHtml = "0";
                                    spmgpending.InnerHtml = "0";
                                    spmg30pending.InnerHtml = "0";
                                }
                            }
                            else
                            {
                                ViewStatelevelSPMGData();
                            }
                        }
                        catch (WebException ex)
                        {
                            Util.LogError(ex, "Dashboard");
                            ViewStatelevelSPMGData();
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Util.LogError(ex, "Dashboard");
            ViewStatelevelSPMGData();
        }
    }
    public void ViewSPMGServiceDistrictLevelDataDirect()
    {
        try
        {
            objserviceDashboard = new DashboardBusinessLayer();
            string strrandomgen = MakeRandom(10);
            var plainran = Encoding.UTF8.GetBytes(strrandomgen);
            string randno = Convert.ToBase64String(plainran);
            //Timestamp
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            double unixTime = span.TotalSeconds;
            var plainut = Encoding.UTF8.GetBytes(unixTime.ToString());
            string plunixtime = Convert.ToBase64String(plainut);

            //PasswordDigest
            string ranpss = strrandomgen + "$CSDa2017@shbo@rD$Ipic)L" + unixTime.ToString();
            SHA256 mySHA256 = SHA256Managed.Create();
            string finalstr = GetSha256FromString(ranpss);
            byte[] bytes = Encoding.UTF8.GetBytes(finalstr);
            string ranpss1 = Convert.ToBase64String(bytes);

            //Financial year
            string FinYear = ddlspmgyear.SelectedValue;

           // string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
            string strdeptid = objserviceDashboard.GetSPMGDepartment(Convert.ToString( intDeptId)).ToString();

            string serviceUrl = "https://esuvidha.gov.in/odishadi/restservices/RestServer.php?view=issuestatusbydepartmentid";

            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
                FinancialYear = FinYear,
                DepartmentID = strdeptid
            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            var webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            webRequest.Method = WebRequestMethods.Http.Post;
            webRequest.ContentType = "application/json";
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
            }
            catch
            {
            }
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);
            using (var requestStream = webRequest.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(json);
                }
            }

            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var responseStream = webResponse.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        try
                        {
                            var responseData = reader.ReadToEnd();
                            webResponse.Close();

                            string strResult = responseData.ToString();
                            if (strResult != "")
                            {
                                DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                                if (DynTable.Rows.Count > 0)
                                {
                                    InsertViewDistrictlevelSPMGData(DynTable.Rows[0]["Issues Received"].ToString(), DynTable.Rows[0]["Issues Resolved"].ToString(),
                                    DynTable.Rows[0]["Issues Pending"].ToString(), DynTable.Rows[0]["Issues Pending (more than 30 days)"].ToString(), strdeptid);
                                }
                                else
                                {
                                    spmgdistraised.InnerHtml = "0";
                                    spmgdistresolved.InnerHtml = "0";
                                    spmgdistpending.InnerHtml = "0";
                                    spmg30distpending.InnerHtml = "0";
                                }
                            }
                            else
                            {
                                ViewDistrictlevelSPMGData();
                            }
                        }
                        catch (WebException ex)
                        {
                            Util.LogError(ex, "Dashboard");
                            ViewDistrictlevelSPMGData();
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Util.LogError(ex, "Dashboard");
            ViewDistrictlevelSPMGData();
        }
    }
    public void ViewSPMGMasterData()  // Method for  bind SPMG  on master tracker
    {
        //SWPDashboard objDashboard = new SWPDashboard();
        //DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
           // objDashboard.strAction = "ALL";
           // string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
            string strdeptid = objserviceDashboard.GetSPMGDepartment( Convert.ToString( intDeptId)).ToString();
            string Finyear = ddlFinacialYear.SelectedValue;
            //objDashboard.intYearId = Convert.ToInt32(Finyear.ToString().Split('-')[0]);
           // objDashboard.intDeptId = Convert.ToInt32(strdeptid);





            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewSPMGMasterDataDept(Convert.ToInt32(strdeptid), Convert.ToInt32(Finyear.ToString().Split('-')[0]));
           // List<SWPDashboard> objServiceStatus = objDashboardBal.GetSPMGdepartmentwiseStatus(objDashboard).ToList();
            if (objServiceStatus.Count > 0)
            {
                spSpmgpnd.InnerText = objServiceStatus[0].strPending.ToString();
            }
            else
            {
                spSpmgpnd.InnerText = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            //objDashboard = null;
          //  objDashboardBal = null;
            objserviceDashboard = null;
        }
    }
    protected void btnspmg_Click(object sender, EventArgs e)
    {
        //InsertSPMGStatus();
        // ViewSPMGData();
        if (ConfigurationManager.AppSettings["SPMG"] == "ON")
        {
            ViewSPMGServiceStateLevelDataDirect();
            ViewSPMGServiceDistrictLevelDataDirect();
        }
           
    }
    public static string GetSha256FromString(string strData)
    {
        var message = Encoding.ASCII.GetBytes(strData);
        SHA256Managed hashString = new SHA256Managed();
        string hex = "";

        var hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }
    public string MakeRandom(int pl)
    {
        string possibles = "0123456789";
        char[] passwords = new char[pl];
        Random rd = new Random();

        for (int i = 0; i < pl; i++)
        {
            passwords[i] = possibles[rd.Next(0, possibles.Length)];
        }
        return new string(passwords);
    }

    #endregion

    #region Service Portlet
    //private void BindService()
    //{
    //    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    //    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    //    objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
    //    ddlService.DataSource = objServicelist;
    //    ddlService.DataTextField = "strServiceName";
    //    ddlService.DataValueField = "intServiceId";
    //    ddlService.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlService.Items.Insert(0, list);

    //}
    //private void BindDept()
    //{
    //    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    //    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    //    objServicelist = objService.BindDepartment("DP").ToList();
    //    ddldept.DataSource = objServicelist;
    //    ddldept.DataTextField = "strdeptname";
    //    ddldept.DataValueField = "Deptid";
    //    ddldept.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddldept.Items.Insert(0, list);

    //}
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
       // BindService();
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowSearchpanel()", true);
    }
    protected void btnStatusOfApproval_Click(object sender, EventArgs e)
    {
        ViewServiceStatus();
    }
    private void ViewServiceStatus()
    {

        //SWPDashboard objDashboard = new SWPDashboard();
       // DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            //int intDeptId_ = 0;
            //objDashboard.strAction = "D";
            ////objDashboard.intInvestorId = 0;
            //if (Session["DeptId"].ToString() != null)
            //{
            //    if (ddldept.SelectedIndex > 0)
            //    {
            //        intDeptId_ = Convert.ToInt32(ddldept.SelectedValue);
            //    }
            //    else
            //    {
            //        intDeptId_ = objDashboardBal.GetDepartment(Session["DeptId"].ToString());

            //    }
            //}
            //else
            //{
            //    intDeptId_ = Convert.ToInt32(ddldept.SelectedValue);
            //}
            // hdnsrvc.Value = objDashboard.intDeptId.ToString();
            // objDashboard.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewServiceStatus(Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlService.SelectedValue));

           // List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
            if (objServiceStatus.Count > 0)
            {

                hdApplied.InnerHtml = objServiceStatus[0].strApplied.ToString();
                hdApprove.InnerHtml = objServiceStatus[0].strApproved.ToString();
                hdPending.InnerHtml = objServiceStatus[0].strPending.ToString();
                hdReject.InnerHtml = objServiceStatus[0].strRejected.ToString();
                hdExceed.InnerHtml = objServiceStatus[0].intDaysPass.ToString();
                hdnqueryRaised.InnerHtml = objServiceStatus[0].QraiseTotal.ToString();
            }
            else
            {
                hdApplied.InnerHtml = "0";
                hdApprove.InnerHtml = "0";
                hdPending.InnerHtml = "0";
                hdExceed.InnerHtml = "0";
                hdnqueryRaised.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
           // objDashboard = null;
           // objDashboardBal = null;
        }
    }
    //private void ViewSPMGDEPTwiseCount()
    //{

    //    SWPDashboard objDashboard = new SWPDashboard();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "D";
    //        string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
    //        string strdeptid = objserviceDashboard.GetSPMGDepartment(dept).ToString();
    //        objDashboard.intYearId = Convert.ToInt32(ddlspmgyear.SelectedValue);
    //        objDashboard.intDeptId = Convert.ToInt32(strdeptid);
    //        List<SWPDashboard> objServiceStatus = objDashboardBal.GetSPMGdepartmentwiseStatus(objDashboard).ToList();
    //        if (objServiceStatus.Count > 0)
    //        {

    //            spmgraised.InnerHtml = objServiceStatus[0].strApplied.ToString();
    //            spmgresolved.InnerHtml = objServiceStatus[0].strApproved.ToString();
    //            spmgpending.InnerHtml = objServiceStatus[0].strPending.ToString();
    //            spSpmgpnd.InnerHtml = objServiceStatus[0].strPending.ToString();
    //            spmg30pending.InnerHtml = objServiceStatus[0].intDaysPass.ToString();
    //        }
    //        else
    //        {
    //            spmgraised.InnerHtml = "0";
    //            spmgresolved.InnerHtml = "0";
    //            spmgpending.InnerHtml = "0";
    //            spSpmgpnd.InnerHtml = "0";
    //            spmg30pending.InnerHtml = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    finally
    //    {
    //        objDashboard = null;
    //        objDashboardBal = null;
    //    }
    //}
    private void ViewServiceMaster()  // Method for  bind Pending approval  on master tracker
    {
        try
        {

        
       // SWPDashboard objDashboard = new SWPDashboard();
       // DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
      //  objDashboard.strAction = "DM";
       // objDashboard.intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
      //  objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
       // objDashboard.intServiceId = 0;
       // objDashboard.strFinacialYear = ddlFinacialYear.SelectedValue;
        List<SWPDashboard> objServiceStatus = commonMethodobj.ViewServiceMasterDept (intDeptId , Convert.ToInt32(Session["Userid"]), ddlFinacialYear.SelectedValue);
       // List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
        if (objServiceStatus.Count > 0)
        {
            spanapprove.InnerText = objServiceStatus[0].strPending.ToString();
        }

        }
        catch(Exception ex)
        {
            throw ex;
        }



    }

    #endregion

    #region PEAL Portlet
    //[WebMethod]
    //public static List<SWPDashboard> EmployeementPealDetailsBind()
    //{

    //    SWPDashboard objDashboard = new SWPDashboard();
    //    List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "PED";
    //        objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    return objCSRStatus;

    //}
    //[WebMethod]
    //public static List<SWPDashboard> EmployeementCapitalPealDetailsBind()
    //{

    //    SWPDashboard objDashboard = new SWPDashboard();
    //    List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "PID";
    //        objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    return objCSRStatus;

    //}
    //[WebMethod]
    //public static List<SWPDashboard> PealDetailsBind()
    //{

    //    SWPDashboard objDashboard = new SWPDashboard();
    //    List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "PPD";
    //        objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    return objCSRStatus;

    //}
    #endregion

    #region MEMBER FUNCTION

    //private void MONTHBIND(DropDownList ddlMonth)
    //{
    //    for (int month = 1; month <= 12; month++)
    //    {
    //        string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
    //        ddlMonth.Items.Add(new ListItem(monthName, month.ToString().PadLeft(2, '0')));
    //    }
    //}
    //private void BindDistrict()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlDistrict.DataSource = objProjList;
    //    ddlDistrict.DataTextField = "vchDistName";
    //    ddlDistrict.DataValueField = "intDistId";
    //    ddlDistrict.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlDistrict.Items.Insert(0, list);

    //}

    //private void BindDistrictEmployeeMentYearwise()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlDistrictEmployeement.DataSource = objProjList;
    //    ddlDistrictEmployeement.DataTextField = "vchDistName";
    //    ddlDistrictEmployeement.DataValueField = "intDistId";
    //    ddlDistrictEmployeement.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlDistrictEmployeement.Items.Insert(0, list);

    //}
    //private void BindDistrictPEAL()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlPEALDistrict.DataSource = objProjList;
    //    ddlPEALDistrict.DataTextField = "vchDistName";
    //    ddlPEALDistrict.DataValueField = "intDistId";
    //    ddlPEALDistrict.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlPEALDistrict.Items.Insert(0, list);

    //}
    //private void BindDistrictInvest()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlYDistrictInvest.DataSource = objProjList;
    //    ddlYDistrictInvest.DataTextField = "vchDistName";
    //    ddlYDistrictInvest.DataValueField = "intDistId";
    //    ddlYDistrictInvest.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlYDistrictInvest.Items.Insert(0, list);

    //}
    //private void YEARBIND(DropDownList ddlYear)
    //{
    //    ddlYear.Items.Clear();
    //    ListItem lt = new ListItem();
    //    for (int i = DateTime.Now.Year; i >= 2007; i--)
    //    {
    //        lt = new ListItem();
    //        lt.Text = i.ToString();
    //        lt.Value = i.ToString();
    //        ddlYear.Items.Add(lt);
    //    }
    //    ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));
    //}
    //private void FillFinYrPortletS(DropDownList ddl)
    //{
    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();

    //    objSWP.strAction = "FS";
    //    List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

    //    ddl.DataSource = objCICGFINYear;
    //    ddl.DataTextField = "Year";
    //    ddl.DataValueField = "intYearId";
    //    ddl.DataBind();
    //    //if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 3)
    //    //{
    //    //    ddl.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString()) - 1) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")));
    //    //}
    //    //else
    //    //{
    //    //    ddl.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString())) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")) + 1);
    //    //}
    //}
    //private void FillMasterFinYr()
    //{
    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();
    //    objSWP.strAction = "FY";
    //    List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

    //    ddlFinacialYear.DataSource = objCICGFINYear;
    //    ddlFinacialYear.DataTextField = "Year";
    //    ddlFinacialYear.DataValueField = "Year";
    //    ddlFinacialYear.DataBind();
    //    ListItem list = new ListItem();
    //    string year = DateTime.Now.Year.ToString();
    //    //ddlFinacialYear.SelectedIndex = 1;
    //}
    //private void FillFinYrPortlet(DropDownList ddl)
    //{
    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();

    //    objSWP.strAction = "FY";
    //    List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

    //    ddl.DataSource = objCICGFINYear;
    //    ddl.DataTextField = "Year";
    //    ddl.DataValueField = "Year";
    //    ddl.DataBind();
    //    if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 3)
    //    {
    //        ddl.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString()) - 1) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")));
    //    }
    //    else
    //    {
    //        ddl.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString())) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")) + 1);
    //    }
    //}
    #endregion

    #region Event click
    protected void ddlFinacialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
       // InsertAppa(ddlFinacialYear);
        ViewServiceMaster();
        ViewSPMGMasterData();
        string FinYear = ddlFinacialYear.SelectedItem.Text;
        string FinTearFinal = FinYear.Remove(4, 3);
        string strFY = DateTime.Now.Month > 3 ? DateTime.Now.Year.ToString() : Convert.ToString(DateTime.Today.Year - 1);

        if (FinTearFinal == strFY)
        {
            ViewCICGMasterData();
        }
        else
        {
            SPcicgpending.InnerText = "0";
        }

        //BindMasterGrievanceportlet();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
            spLastUpdate.InnerText = DateTime.Now.ToString();
            /*-----------------------------------------------------------------*/
            ///Fill Dropdownlist for Financial Year
            /*-----------------------------------------------------------------*/
            commonMethodobj.FillFinancialYear(ddlFinacialYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on master tracker
            commonMethodobj.FillFinancialYearWithYear(ddlspmgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year of spmg 
            commonMethodobj.FillFinancialYear(ddlyearquery); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
            DashboradCommon.MONTHBIND(ddlCICGMonth); //bind both value 1 and  text January    Month value  on Normal platform
            DashboradCommon.YEARBIND(ddlYearCICG);  //bind both value 2016 and  text 2016    Year value  on Normal platform
            DashboradCommon.MONTHBIND(ddlAppaMonth); //bind both value 1 and  text January    Month value  on Normal platform
            commonMethodobj.FillFinancialYear(ddlAppaYear); //bind both value field  2016-17 and  text 2016-17  FinalcealYear on Normal platform

            /*-----------------------------------------------------------------*/
            //// Master Tracker
            /*-----------------------------------------------------------------*/
            ViewServiceMaster();  //Pending Approvals
            ViewSPMGMasterData();//State Project Monitoring Group
            ViewCICGMasterData();//Central Inspection Framework



            /*-----------------------------------------------------------------*/
            ////Portlet Section
            /*-----------------------------------------------------------------*/
            commonMethodobj.FillDepartment(ddldept); // bind Department record in dropdown list
            ddldept.SelectedValue = Convert.ToString(intDeptId);
            ddldept.Enabled = false;
            commonMethodobj.FillService(ddldept, ddlService);// bind service record in  dropdown list
            ViewServiceStatus();//DEPARTMENT WISE APPROVALS

            if (ConfigurationManager.AppSettings["SPMG"] == "ON")
            {
            ViewSPMGServiceStateLevelDataDirect();// STATE PROJECT MONITORING GROUP
            ViewSPMGServiceDistrictLevelDataDirect(); //STATE PROJECT MONITORING GROUP
            }
           


            ViewQueryService(); //QUERY MONITORING


            commonMethodobj.FillDepartmentForCIF(ddldeptCIF);  // bind Department record in CIF dropdown list
            //InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
           // ViewCICGData(); //CENTRAL INSPECTION FRAMEWORK


            commonMethodobj.FillDist(ddlAPAADistrict);// bind dist in dropdown list  for IDCO POST ALLOTMENT APPLICATIONS 


            // BindDept();
           // BindDeptCICG();
           // FillFinYrPortletS(ddlspmgyear);
           // int intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
            //ddldept.SelectedValue = Convert.ToString(intDeptId);
            //ddldept.Enabled = false;
           // BindService();
            if (intDeptId == idco || intDeptId == idcoW)
            {
                dvapaa.Style.Add("display", "block");
                dvAPAAMast.Style.Add("display", "block");
               if (ConfigurationManager.AppSettings["IDCO"] == "ON")
               {
                InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
               }
                
               // FillFinYrPortlet(ddlAppaYear);
            }
            else if (intDeptId == LabDirectorate || intDeptId == OSPCB || intDeptId == FactBoil || intDeptId == LegalMetrology)
            {
                dvCICG.Style.Add("display", "block");
                dvCICGMast.Style.Add("display", "block");
                 if (ConfigurationManager.AppSettings["CICG"] == "ON")
                 {
                   InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
                 }
                
                
                ddldeptCIF.SelectedValue = Convert.ToString(objserviceDashboard.GetCIFDepartmentid(Session["DeptId"].ToString()));
                ddldeptCIF.Enabled = false;
            }
        // spLastUpdate.InnerText = DateTime.Now.ToString();
        // FillMasterFinYr();

               if (ConfigurationManager.AppSettings["IDCO"] == "ON")
               {
                 InsertAppa(ddlFinacialYear);
               }
              
           
            //Added BY Pritiprangya pattanaik on 15-jan-2017 to add and view spmg department wise status count

            //ViewSPMGDEPTwiseCount();
            //InsertSPMGStatus();

            //Added BY Pritiprangya pattanaik on 15-jan-2017 to add and view spmg department wise status count
          //  string strDep = Session["DeptId"].ToString();
            //ViewServiceStatus();
           // ViewServiceMaster();
            ViewCICGData();//CENTRAL INSPECTION FRAMEWORK
           // MONTHBIND(ddlAppaMonth);
           // MONTHBIND(ddlCICGMonth);
          //  YEARBIND(ddlAppaYear);
           // FillFinYrPortlet(ddlyearquery);
          //  YEARBIND(ddlYearCICG);
           // BindDistrictAPAA();
            ViewApaaStatus();

            //YEARBIND(ddlyearquery);
           // ViewQueryService();
           // ViewCICGMasterData();
            // ViewSPMGData();
            // BindInnerSPMG();            
           // ViewSPMGServiceStateLevelDataDirect();
          //  ViewSPMGServiceDistrictLevelDataDirect();
           // ViewSPMGMasterData();

            //added by Ritika Lath For incentive portlet 13th dec 2017
          //  FillIncentiveApplicationDropdown();

            ////For Grievance
            //FillFinYrPortletS(ddlgyear);
            //BindDistrictGrievance();
            //BindMasterGrievanceportlet();
            //BindGrievanceportlet();


























      // // BindDept();
      //  int intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
      //  ddldept.SelectedValue = Convert.ToString(objserviceDashboard.GetDepartment(Session["DeptId"].ToString()));
      //  ddldept.Enabled = false;
      //  if (intDeptId == idco || intDeptId == idcoW)
      //  {
      //      dvapaa.Style.Add("display", "block");
      //      dvAPAAMast.Style.Add("display", "block");
      //     // FillFinYrPortlet(ddlAppaYear);
      //    //  InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);

      //  }
      //  else if (intDeptId == LabDirectorate || intDeptId == OSPCB || intDeptId == FactBoil || intDeptId == LegalMetrology)
      //  {
      //      dvCICG.Style.Add("display", "block");
      //      dvCICGMast.Style.Add("display", "block");
      //      InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
      //      ddldeptCIF.SelectedValue = objserviceDashboard.GetCIFDepartmentid(Session["DeptId"].ToString());
      //      ddldeptCIF.Enabled = false;
      //  }

      //  spLastUpdate.InnerText = DateTime.Now.ToString();
      // // FillMasterFinYr();
      //  InsertAppa(ddlFinacialYear);
      //  string strDep = Session["DeptId"].ToString();
      //  ViewServiceStatus();
      //  ViewServiceMaster();

      //  ViewCICGData();
      // // MONTHBIND(ddlAppaMonth);
      ////  MONTHBIND(ddlCICGMonth);
      //  // YEARBIND(ddlAppaYear);
      ////  YEARBIND(ddlYearCICG);
      // // BindDistrictAPAA();
      //  ViewApaaStatus();
      // // BindDeptCICG();
      ////  YEARBIND(ddlyearquery);
      //  ViewQueryService();
      //  ViewCICGMasterData();
      //  ViewSPMGServiceStateLevelDataDirect();
      //  ViewSPMGServiceDistrictLevelDataDirect();
      //  ViewSPMGMasterData();
      //  //ViewSPMGData();
      //  //BindInnerSPMG();

      //  //////Grievance
      //  //FillFinYrPortletS(ddlgyear);
      //  //BindDistrictGrievance();
      //  //BindMasterGrievanceportlet();
      //  //BindGrievanceportlet();

    }
    #endregion

    #region SPMG Portlet Database Interaction Added By Manoj Kumar Behera on 29-08-2019
    private void ViewStatelevelSPMGData() //STATE PROJECT MONITORING GROUP
    {
        //SWPDashboard objDashboard = new SWPDashboard();
       // DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
           // objDashboard.strAction = "DS";
           // string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
            string strdeptid = objserviceDashboard.GetSPMGDepartment(Convert.ToString(intDeptId)).ToString();
           // objDashboard.intYearId = Convert.ToInt32(ddlspmgyear.SelectedValue);
           // objDashboard.intDeptId = Convert.ToInt32(strdeptid);
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewStatelevelSPMGDataDept(Convert.ToInt32(ddlspmgyear.SelectedValue) , Convert.ToInt32(strdeptid));
            if (objServiceStatus.Count > 0)
            {
                spmgraised.InnerHtml = objServiceStatus[0].strApplied.ToString();
                spmgresolved.InnerHtml = objServiceStatus[0].strApproved.ToString();
                spmgpending.InnerHtml = objServiceStatus[0].strPending.ToString();
                spmg30pending.InnerHtml = objServiceStatus[0].intDaysPass.ToString();
            }
            else
            {
                spmgraised.InnerHtml = "0";
                spmgresolved.InnerHtml = "0";
                spmgpending.InnerHtml = "0";
                spmg30pending.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
           // objDashboard = null;
           // objDashboardBal = null;
            objserviceDashboard = null;
        }
    }
    private void ViewDistrictlevelSPMGData() //STATE PROJECT MONITORING GROUP
    {
       // SWPDashboard objDashboard = new SWPDashboard();
       // DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
           // objDashboard.strAction = "DD";
           // string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
            string strdeptid = objserviceDashboard.GetSPMGDepartment(Convert.ToString(intDeptId)).ToString();
           // objDashboard.intYearId = Convert.ToInt32(ddlspmgyear.SelectedValue);
           // objDashboard.intDeptId = Convert.ToInt32(strdeptid);
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewDistrictlevelSPMGDataDept(Convert.ToInt32(ddlspmgyear.SelectedValue), Convert.ToInt32(strdeptid));
            if (objServiceStatus.Count > 0)
            {
                spmgdistraised.InnerHtml = objServiceStatus[0].strApplied.ToString();
                spmgdistresolved.InnerHtml = objServiceStatus[0].strApproved.ToString();
                spmgdistpending.InnerHtml = objServiceStatus[0].strPending.ToString();
                spmg30distpending.InnerHtml = objServiceStatus[0].intDaysPass.ToString();
            }
            else
            {
                spmgdistraised.InnerHtml = "0";
                spmgdistresolved.InnerHtml = "0";
                spmgdistpending.InnerHtml = "0";
                spmg30distpending.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
           // objDashboard = null;
           // objDashboardBal = null;
            objserviceDashboard = null;
        }
    }
    private void InsertViewStatelevelSPMGData(string recived, string resolved, string pending, string pendinglast, string strdeptid) //STATE PROJECT MONITORING GROUP
    {
       // DataTable PnlDt = new DataTable();
       // using (SqlConnection con = new SqlConnection(connectionString))
       // {
            try
            {

                DataTable PnlDt = commonMethodobj.InsertViewStatelevelSPMGDataDept(Convert.ToInt32(UserId), Convert.ToInt32(ddlspmgyear.SelectedValue), recived, resolved, pending, pendinglast, Convert.ToInt32(strdeptid));

                //using (SqlCommand cmd = new SqlCommand("USP_SPMG_DASHBOARD"))
                //{
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.Add("@INTID", SqlDbType.Int).Value = Convert.ToInt32(Session["Userid"].ToString());
                //    cmd.Parameters.Add("@INTYEAR", SqlDbType.Int).Value = Convert.ToInt32(ddlspmgyear.SelectedValue);
                //    cmd.Parameters.Add("@INTRECIVED", SqlDbType.Int).Value = Convert.ToInt32(recived);
                //    cmd.Parameters.Add("@INTRESOLVED", SqlDbType.Int).Value = Convert.ToInt32(resolved);
                //    cmd.Parameters.Add("@INTPENDING", SqlDbType.Int).Value = Convert.ToInt32(pending);
                //    cmd.Parameters.Add("@INTPENDING30", SqlDbType.Int).Value = Convert.ToInt32(pendinglast);
                //    cmd.Parameters.Add("@INTDEPT", SqlDbType.Int).Value = Convert.ToInt32(strdeptid);
                //    cmd.Parameters.Add("@VCHTYPE", SqlDbType.VarChar).Value = "1D";//FOR ALL STATE LEVEL IS 1.
                //    cmd.Connection = con;
                //    con.Open();
                //    PnlDt.Load(cmd.ExecuteReader());
                //    con.Close();
                //}
                if (PnlDt.Rows.Count > 0)
                {
                    spmgraised.InnerHtml = PnlDt.Rows[0]["RAISED"].ToString();
                    spmgresolved.InnerHtml = PnlDt.Rows[0]["RESOLVED"].ToString();
                    spmgpending.InnerHtml = PnlDt.Rows[0]["PENDING"].ToString();
                    spmg30pending.InnerHtml = PnlDt.Rows[0]["ISSUEPENDING"].ToString();
                }
                else
                {
                    spmgraised.InnerHtml = "0";
                    spmgresolved.InnerHtml = "0";
                    spmgpending.InnerHtml = "0";
                    spmg30pending.InnerHtml = "0";
                }
            }
            catch (Exception ex)
            {
               throw ex;
               // con.Close();
            }
        //}
    }
    private void InsertViewDistrictlevelSPMGData(string recived, string resolved, string pending, string pendinglast, string strdeptid) //STATE PROJECT MONITORING GROUP
    {
        //DataTable PnlDt = new DataTable();
        //using (SqlConnection con = new SqlConnection(connectionString))
        //{
            try
            {
            //using (SqlCommand cmd = new SqlCommand("USP_SPMG_DASHBOARD"))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@INTID", SqlDbType.Int).Value = Convert.ToInt32(Session["Userid"].ToString());
            //    cmd.Parameters.Add("@INTYEAR", SqlDbType.Int).Value = Convert.ToInt32(ddlspmgyear.SelectedValue);
            //    cmd.Parameters.Add("@INTRECIVED", SqlDbType.Int).Value = Convert.ToInt32(recived);
            //    cmd.Parameters.Add("@INTRESOLVED", SqlDbType.Int).Value = Convert.ToInt32(resolved);
            //    cmd.Parameters.Add("@INTPENDING", SqlDbType.Int).Value = Convert.ToInt32(pending);
            //    cmd.Parameters.Add("@INTPENDING30", SqlDbType.Int).Value = Convert.ToInt32(pendinglast);
            //    cmd.Parameters.Add("@INTDEPT", SqlDbType.Int).Value = Convert.ToInt32(strdeptid);
            //    cmd.Parameters.Add("@VCHTYPE", SqlDbType.VarChar).Value = "2D";//For ALL DIC LEVEL IS 2
            //    cmd.Connection = con;
            //    con.Open();
            //    PnlDt.Load(cmd.ExecuteReader());
            //    con.Close();
            //}
            DataTable PnlDt = commonMethodobj.InsertViewDistrictlevelSPMGDataDept(Convert.ToInt32(UserId), Convert.ToInt32(ddlspmgyear.SelectedValue), recived, resolved, pending, pendinglast, strdeptid);

            if (PnlDt.Rows.Count > 0)
                {
                    spmgdistraised.InnerHtml = PnlDt.Rows[0]["RAISED"].ToString();
                    spmgdistresolved.InnerHtml = PnlDt.Rows[0]["RESOLVED"].ToString();
                    spmgdistpending.InnerHtml = PnlDt.Rows[0]["PENDING"].ToString();
                    spmg30distpending.InnerHtml = PnlDt.Rows[0]["ISSUEPENDING"].ToString();
                }
                else
                {
                    spmgdistraised.InnerHtml = "0";
                    spmgdistresolved.InnerHtml = "0";
                    spmgdistpending.InnerHtml = "0";
                    spmg30distpending.InnerHtml = "0";
                }
            }
            catch (Exception ex)
            {
            // con.Close();
               throw ex;
            }
        //}

    }

    #endregion

    protected void Button10_Click(object sender, EventArgs e)
    {
        ViewQueryService();
    }

    //private void BindDistrictGrievance()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlgdist.DataSource = objProjList;
    //    ddlgdist.DataTextField = "vchDistName";
    //    ddlgdist.DataValueField = "intDistId";
    //    ddlgdist.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlgdist.Items.Insert(0, list);
    //}
    //private void BindMasterGrievanceportlet()
    //{
    //    try
    //    {
    //        DataTable Gdt = new DataTable();
    //        objSWP = new SWPDashboard();
    //        objserviceDashboard = new DashboardBusinessLayer();
    //        objSWP.strAction = "DGD";
    //        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
    //        objSWP.strFinacialYear = ddlFinacialYear.SelectedItem.Text;
    //        objSWP.intDistrictId = 0;
    //        Gdt = objserviceDashboard.getDepartmentGrievance(objSWP);
    //        if (Gdt.Rows.Count > 0)
    //        {
    //            //Spangresolved.InnerText = Gdt.Rows[0]["RESOLVED"].ToString();
    //            Spangpending.InnerText = Gdt.Rows[0]["PENDING"].ToString();
    //        }
    //        else
    //        {
    //            //Spangresolved.InnerText = "";
    //            Spangpending.InnerText = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    finally
    //    {
    //        objSWP = null;
    //    }
    //}
    //private void BindGrievanceportlet()
    //{
    //    try
    //    {
    //        DataTable Gdt = new DataTable();
    //        objSWP = new SWPDashboard();
    //        objserviceDashboard = new DashboardBusinessLayer();
    //        objSWP.strAction = "DGD";
    //        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
    //        objSWP.strFinacialYear = ddlgyear.SelectedItem.Text;
    //        if (ddlgdist.SelectedIndex > 0)
    //        {
    //            objSWP.intDistrictId = Convert.ToInt32(ddlgdist.SelectedValue);
    //        }
    //        else
    //        {
    //            objSWP.intDistrictId = 0;
    //        }
    //        Gdt = objserviceDashboard.getDepartmentGrievance(objSWP);
    //        if (Gdt.Rows.Count > 0)
    //        {
    //            lblGapplied.Text = Gdt.Rows[0]["APPLIED"].ToString();
    //            lblGrsolved.Text = Gdt.Rows[0]["RESOLVED"].ToString();
    //            lblGpending.Text = Gdt.Rows[0]["PENDING"].ToString();
    //            lblGrejected.Text = Gdt.Rows[0]["REJECTED"].ToString();
    //        }
    //        else
    //        {
    //            lblGapplied.Text = "0";
    //            lblGrsolved.Text = "0";
    //            lblGpending.Text = "0";
    //            lblGrejected.Text = "0";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    finally
    //    {
    //        objSWP = null;
    //    }
    //}
    //protected void btnGSearch_Click(object sender, EventArgs e)
    //{
    //    BindGrievanceportlet();
    //}
}

