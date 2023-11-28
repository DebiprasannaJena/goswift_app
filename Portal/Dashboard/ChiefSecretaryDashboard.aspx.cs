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
using System.Data.SqlClient;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;
using System.Web;
#endregion

public partial class Portal_Dashboard_ChiefSecretaryDashboard : SessionCheck
{
    #region Global variable
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    CommonMethodForDashboard commonMethodobj = new CommonMethodForDashboard();
    string UserId = string.Empty;
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
           
        }
        else
        {
            UserId = Session["UserId"].ToString();
        }

        string strDesignationId = Convert.ToString(Session["desId"].ToString());

        if (strDesignationId == "95" || strDesignationId == "124" || strDesignationId == "171")
        {
            if (strDesignationId == "95")
            {
                h1Dashuser.InnerText = "Chief Secretary DashBoard";
                adashuser.InnerText = "Chief Secretary DashBoard";
            }
            else if (strDesignationId == "124")
            {
                h1Dashuser.InnerText = "Development Commissioner DashBoard";
                adashuser.InnerText = "Development Commissioner DashBoard";
            }
            else if (strDesignationId == "171")
            {
                h1Dashuser.InnerText = "Principal Advisor DashBoard";
                adashuser.InnerText = "Principal Advisor DashBoard";
            }
            if (ConfigurationManager.AppSettings["CICG"] == "ON")
            {
                InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
            }
                

            string x = hdnFinacial.Value;
            if (!IsPostBack)
            {
                spLastUpdate.InnerText = DateTime.Now.ToString();

                /*-----------------------------------------------------------------*/
                ///Fill Dropdownlist for Financial Year
                /*-----------------------------------------------------------------*/

                commonMethodobj.FillFinancialYear(ddlAppaYear); //bind both value field  2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYear(ddlCSRYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYearWithYear(ddlspmgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year value  on Normal platform
                commonMethodobj.FillFinancialYear(ddlFinacialYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on master tracker
                commonMethodobj.FillFinancialYear(ddlPealYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYear(ddlYearInvest); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYear(ddlYearEmployement); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                DashboradCommon.MONTHBIND(ddlAppaMonth); //bind both value 1 and  text January    Month value  on Normal platform
                DashboradCommon.MONTHBIND(ddlCICGMonth); //bind both value 1 and  text January    Month value  on Normal platform
                DashboradCommon.YEARBIND(ddlYearCICG);  //bind both value 2016 and  text 2016    Year value  on Normal platform
                commonMethodobj.FillFinancialYear(ddlIncentiveYear); //bind bothe value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYear(ddlyearquery); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYear(ddlLandFinYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Normal platform
                commonMethodobj.FillFinancialYearWithYear(ddlgyear); //bind both value 2016 and  text 2016-17  FinalcealYear and year value  on Normal platform



                /*-----------------------------------------------------------------*/
                //// Master Tracker
                /*-----------------------------------------------------------------*/

                BindUnderEvalution();// Single Window Application Status
                BindPealRecieved(); //Single Window Application Status
                BindPealApproved();//Single Window Application Status 
                IncentiveMasterBind(); // Incentive Details
                ViewSPMGMasterData();//State Project Monitoring Group 
                ViewServiceMaster();  //Pending Approvals
                FillTrackerInvestment(); //Investment
                ViewCICGMasterData();//Central Inspection Framework

                BindMasterGrievanceportlet();//Grievance Status
                if (ConfigurationManager.AppSettings["CSR"] == "ON")
                {
                    InsertCSRMaster(ddlFinacialYear);//CSR Spend
                }
                    
                FillTrackerEmployement(); //Employment 
                if (ConfigurationManager.AppSettings["IDCO"] == "ON")
                {
                    InsertAppa(ddlFinacialYear);// IDCO Post Allotment Applications
                }
                   

                /*-----------------------------------------------------------------*/
                ////Portlet Section
                /*-----------------------------------------------------------------*/

                commonMethodobj.FillDist(ddlPEALDistrict); // bind dist in PEAL dropdown list for SINGLE WINDOW APPLICATION STATUS 
                FillProposalDetails();//SINGLE WINDOW APPLICATION STATUS 
                FillProposalDistwiseDetails();//SINGLE WINDOW APPLICATION STATUS 
                FillProposalDetailsIT();//SINGLE WINDOW APPLICATION STATUS 
                FillProposalDetailsTourism();//SINGLE WINDOW APPLICATION STATUS 


                commonMethodobj.FillDist(chkYearwise); //Bind dist in checkbox list for YEAR WISE INVESTMENT
                FillProposalCapital();//YEAR WISE INVESTMENT


                // BindDistrictEmployeeMentYearwise();
                commonMethodobj.FillDist(CheckBoxList1); //Bind dist in checkbox list for YEAR WISE EMPLOYMENT
                FillProposalEmployement();//YEAR WISE EMPLOYMENT

                if (ConfigurationManager.AppSettings["SPMG"] == "ON")
                {
                    ViewSPMGServiceStateLevelDataDirect();// STATE PROJECT MONITORING GROUP
                    ViewSPMGServiceDistrictLevelDataDirect(); //STATE PROJECT MONITORING GROUP
                }
                   
               

                commonMethodobj.FillDepartment(ddldept); // bind Department record in dropdown list
                ViewServiceStatus();//DEPARTMENT WISE APPROVALS

                commonMethodobj.FillDepartmentForCIF(ddldeptCIF);  // bind Department record in CIF dropdown list
                if (ConfigurationManager.AppSettings["CICG"] == "ON")
                {
                    InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
                }
                   
                ViewCICGData(); //CENTRAL INSPECTION FRAMEWORK


                commonMethodobj.FillDist(ddlAPAADistrict);// bind dist in dropdown list  for IDCO POST ALLOTMENT APPLICATIONS 
                if (ConfigurationManager.AppSettings["IDCO"] == "ON")
                {
                    InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
                }
                    
                ViewApaaStatus();//IDCO POST ALLOTMENT APPLICATIONS

                commonMethodobj.FillDist(chkCSRDistrct);// bind dist in csr checkbox list

                if (ConfigurationManager.AppSettings["CSR"] == "ON")
                {
                    CSRPortletSatus(); //CSR ACTIVITIES
                }
                   

                commonMethodobj.FillDist(ddlIncentiveDistrict);// bind dist in dropdown list  for INCENTIVE DETAILS
                IncentiveBind();//INCENTIVE DETAILS


                LandServiceBind();//LAND ALLOTMENT DETAILS


                ViewQueryService(); //QUERY MONITORING
                ViewQueryServicepeal();//QUERY MONITORING
                ViewQueryServiceIncentive();//QUERY MONITORING

                commonMethodobj.FillDist(ddlgdist);// bind dist in dropdown list  for GRIEVANCE STATUS
                BindGrievanceportlet();//GRIEVANCE STATUS

               
            }
        }
        else
        {
            Response.Redirect("~/Portal/Default.aspx");
        }
    }
    #endregion

    #region CSR Portlet
    //private void InsertCSRStatus(HiddenField hdnCSRDistrct, DropDownList ddlCSRYear)
    //{
    //    string finalquery = string.Empty;
    //    SqlCommand cmd = null;
    //    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    //    string Type = "1";
    //    SWPDashboard objSWP = new SWPDashboard();
    //    string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
    //    inputJson = inputJson.TrimStart('[').TrimEnd(']');
    //    string District = "";
    //    if (hdnCSRDistrct.Value != "")
    //    {
    //        District = hdnCSRDistrct.Value.ToString();
    //    }
    //    else
    //    {
    //        District = "0";
    //    }
    //    //string District = string.IsNullOrEmpty(ddlDistrict.SelectedValue) ? default(string) : ddlDistrict.SelectedValue.ToString();
    //    //string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
    //    string Year = string.IsNullOrEmpty(ddlCSRYear.SelectedItem.Text) ? default(string) : ddlCSRYear.SelectedItem.Text;
    //    if (Year == "--Select--")
    //    {
    //        Year = "0";
    //    }
    //    else
    //    {
    //        Year = ddlCSRYear.SelectedItem.Text;
    //    }
    //    //string UniqueKey = Session["UID"].ToString();
    //    string UniqueKey = "0";
    //    try
    //    {
    //        string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetTotalAmount/" + UniqueKey + "/" + Type + "/" + District + "/" + Year;
    //        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
    //        httpRequest.Accept = "application/json";
    //        httpRequest.ContentType = "application/json";
    //        httpRequest.Method = "GET";
    //        using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
    //        {
    //            using (Stream stream = httpResponse.GetResponseStream())
    //            {
    //                string strResult = (new StreamReader(stream)).ReadToEnd();
    //                DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
    //                string output = DynTable.Rows[0]["GetTotalAmountResult"].ToString();
    //                string[] finalOut = output.Split(':');
    //                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    //                if (con.State == ConnectionState.Closed)
    //                {
    //                    con.Open();
    //                }
    //                string query = "SELECT ADMINID FROM T_CSR_SpentDtls_Admin WHERE CONVERT(DATE,dtmCreatedon)='" + DateTime.Now.ToString("dd-MMM-yy") + "'";
    //                SqlDataAdapter adp = new SqlDataAdapter(query, con);
    //                DataSet ds = new DataSet();
    //                adp.Fill(ds);
    //                if (ds.Tables[0].Rows.Count > 0)
    //                {
    //                    finalquery = "UPDATE T_CSR_SpentDtls_Admin SET adminid=" + Convert.ToInt32(UserId) + ",TotalAmount=" + finalOut[2].ToString() + ",TotalProject=" + DynTable.Rows[0]["TotalProject"].ToString() + "," +
    //                          "dtmCreatedon='" + DateTime.Now.ToString("dd-MMM-yy") + "',vchYear='" + Year + "',intDistrictId='" + District + "' WHERE adminid='" + ds.Tables[0].Rows[0]["ADMINID"].ToString() + "'";
    //                }
    //                else
    //                {
    //                    if (finalOut[2].ToString() != "")
    //                    {
    //                        ViewState["finalvalue"] = finalOut[2].ToString(); ;
    //                    }
    //                    else
    //                    { ViewState["finalvalue"] = "0"; }
    //                    finalquery = "INSERT INTO T_CSR_SpentDtls_Admin(adminid,TotalAmount,TotalProject,dtmCreatedon,vchYear,intDistrictId)" +
    //                "VALUES(" + Convert.ToInt32(UserId) + "," + ViewState["finalvalue"] + "," + DynTable.Rows[0]["TotalProject"].ToString() + "," +
    //                "'" + DateTime.Now.ToString("dd-MMM-yy") + "','" + Year + "'," + District + ")";
    //                }
    //                cmd = new SqlCommand(finalquery, con);
    //                cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //}
    //private void CurrentMonthSelect(DropDownList ddlQuarter, DropDownList ddlYear)
    //{
    //    decimal Month = Math.Ceiling(DateTime.Today.Month / 3m);
    //    ddlQuarter.SelectedValue = Month.ToString();
    //    ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
    //}
    protected void btnCSRStatus_Click(object sender, EventArgs e)
    {
        ///InsertCSRStatus(hdnCSRDistrct, ddlCSRYear); Commented for not in use
        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            CSRPortletSatus();
        }
           
    }
    private void CSRPortletSatus()//view  CSR ACTIVITIES on normal platfrom
    {
        //////// Dispaly CSR data directly from service implemented by Sushant Jena on Dt. 29-May-2018
        try
        {
            string strYear = "0";
            string strDistrict = "0";

            if (hdnCSRDistrct.Value != "")
            {
                strDistrict = hdnCSRDistrct.Value;
            }

            if (ddlCSRYear.SelectedItem.Text != "--Select--")
            {
                strYear = ddlCSRYear.SelectedItem.Text;
            }

            DataTable DynTable = commonMethodobj.CSRPortletSatus(strYear, strDistrict);
            if (DynTable.Rows.Count > 0)
            {
                string output = DynTable.Rows[0]["GetTotalAmountResult"].ToString();
                string[] finalOut = output.Split(':');

                SPProject.InnerHtml = Convert.ToString(DynTable.Rows[0]["TotalProject"]);
                SPRecommendedCouncil.InnerHtml = Convert.ToString(DynTable.Rows[0]["TotalRecommendProject"]);
                decimal TotAmount = Convert.ToDecimal(DynTable.Rows[0]["TotalAmount"]) / 100;
                SPSpent.InnerHtml = Convert.ToString(Math.Round(TotAmount));
            }
            else
            {
                SPProject.InnerHtml = "0";
                SPRecommendedCouncil.InnerHtml = "0";
                SPSpent.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }

    //private void BindDistrict()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();
    //    for (int i = 0; i < objProjList.Count; i++)
    //    {
    //        ListItem item = new ListItem();
    //        item.Text = objProjList[i].vchDistName;
    //        item.Value = objProjList[i].intDistId.ToString();
    //        chkCSRDistrct.Items.Add(item);
    //    }
    //    foreach (ListItem lstitem in chkCSRDistrct.Items)
    //    {
    //        lstitem.Selected = true;
    //    }
    //}
    private void InsertCSRMaster(DropDownList ddlyear) /// Method for binding CSR Spend on master tracker section
    {
        try
        {
            string Year = string.IsNullOrEmpty(ddlyear.SelectedItem.Text) ? default(string) : ddlyear.SelectedItem.Text;
            if (Year == "--Select--")
            {
                Year = "0";
            }
            else
            {
                Year = ddlyear.SelectedItem.Text;
            }

            DataTable DynTable = new DataTable();
            DynTable = commonMethodobj.InsertCSRMaster(Year);
            decimal TotAmount = Convert.ToDecimal(DynTable.Rows[0]["TotalAmount"]) / 100; //// Added by Sushant Jena on Dt. 28-May-2018 for converting lakhs to Crore
            SPNetSpent.InnerText = Math.Round(TotAmount).ToString();
        }
        catch (Exception ex)
        {
            SPNetSpent.InnerHtml = "0";
            Util.LogError(ex, "Dashboard");
        }
    }

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
    private void InsertAppa(DropDownList ddlYear) /// Mothod for bind IDCO Post Allotment Applications on master tracker
    {
        string Year = string.IsNullOrEmpty(ddlYear.SelectedValue) ? default(string) : ddlYear.SelectedValue.ToString();

        try
        {
            DataTable DynTable = commonMethodobj.InsertAppa(Year, Convert.ToInt32(UserId));
            spAPAAPending.Text = DynTable.Rows[0]["TotalPendingIdco"].ToString();
        }
        catch (Exception ex)
        {
            spAPAAPending.Text = "0";
            Util.LogError(ex, "Dashboard");
        }
    }
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
        //string Year = string.IsNullOrEmpty(ddlAppaYear.SelectedValue) ? default(string) : ddlAppaYear.SelectedValue.ToString();

        string Year = string.IsNullOrEmpty(ddlAppaYear.SelectedValue) ? default(string) : ddlAppaYear.SelectedValue.ToString();
        //if (Year == "--Select--")
        //{
        //    Year = "0";
        //}
        //else
        //{
        //    Year = ddlYear.SelectedItem.Text;
        //}

        string Deptid = "0";
        //string UniqueKey = Session["UID"].ToString();
        string UniqueKey = "0";
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(UserId);
        objlist = objserviceDashboard.CheckAppastatus(objSWP);
        if (objlist.Count > 0)
        {
            if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
            {
                Type = "0";
            }
            else if (objlist[0].intStatus != 1 && objlist[0].intDistrictid == 0)//IPICOL Largescale
            {
                Type = "1";
            }
            else//MSME
            {
                Type = "2";
            }
        }
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
                        finalquery = "UPDATE T_APAA_Service_Admin SET adminid=" + Convert.ToInt32(UserId) + ",TotalApplied=" + finalOut[2].ToString() + ",TotalDisposed=" + DynTable.Rows[0]["TotalDisposed"].ToString() + "," +
                             "TotalMajorPendingIdco=" + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + ",TotalPendingIdco=" + DynTable.Rows[0]["TotalPendingIdco"].ToString() + ",TotalPendingUnit=" + DynTable.Rows[0]["TotalPendingUnit"].ToString() + ",dtmCreatedon='" + DateTime.Now.ToString("dd-MMM-yy") + "',intYearId='" + Year + "',intDistrictId=" + District + ",intMonthId=" + Month + " WHERE adminid='" + ds.Tables[0].Rows[0]["ADMINID"].ToString() + "'";
                    }
                    else
                    {
                        finalquery = "INSERT INTO T_APAA_Service_Admin(adminid,TotalApplied,TotalDisposed,TotalMajorPendingIdco,TotalPendingIdco,TotalPendingUnit,dtmCreatedon,intYearId,intDistrictId,intMonthId)" +
                    "VALUES(" + Convert.ToInt32(UserId) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["TotalDisposed"].ToString() + "," + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingUnit"].ToString() + "," +
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
    private void ViewApaaStatus()//view IDCO POST ALLOTMENT APPLICATIONS on normal platfrom
    {
        try
        {
            List<SWPDashboard> objPEALStatusList = commonMethodobj.ViewApaaStatus(Convert.ToInt32(UserId), ddlAppaYear.SelectedItem.Text, Convert.ToInt32(ddlAppaMonth.SelectedValue), Convert.ToInt32(ddlAPAADistrict.SelectedValue));
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

    }
    protected void btnAPAASubmit_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["IDCO"] == "ON")
        {
            InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
        }
           
        ViewApaaStatus();
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

        string Month = string.IsNullOrEmpty(ddlCICGMonth.SelectedValue) ? default(string) : ddlCICGMonth.SelectedValue.ToString();
        string Year = string.IsNullOrEmpty(ddlYearCICG.SelectedValue) ? default(string) : ddlYearCICG.SelectedValue.ToString();
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(UserId);

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

                    string query = "SELECT INT_ID FROM T_CICG_DASHBOARD WHERE CONVERT(DATE,DTM_CREATED_ON)='" + DateTime.Now.ToString("dd-MMM-yy") + "' and INT_ID = " + Convert.ToInt32(UserId);
                    SqlDataAdapter adp = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        finalquery = "UPDATE T_CICG_DASHBOARD SET INT_ID=" + Convert.ToInt32(UserId) + ",INT_INS_COMPLETED=" + finalOut[2].ToString() + ",INT_INS_SCHEDULED=" + DynTable.Rows[0]["InspectionScheduled"].ToString() + "," +
                             "INT_REPORT_PENDING=" + DynTable.Rows[0]["PendingInspection"].ToString() + ",INT_ReportNot_Uploaded=" + DynTable.Rows[0]["ReportNotUploaded"].ToString() + ",INT_UNATTENDED_INS=" + DynTable.Rows[0]["UnAttendedInspection"].ToString() + ",DTM_CREATED_ON='" + DateTime.Now.ToString("dd-MMM-yy")
                             + "',intDeptId=" + Department + ",intMonthId=" + Month + ",intYearId=" + Year + " WHERE INT_ID='" + ds.Tables[0].Rows[0]["INT_ID"].ToString() + "'";

                    }
                    else
                    {

                        finalquery = "INSERT INTO T_CICG_DASHBOARD(INT_ID,INT_INS_COMPLETED,INT_INS_SCHEDULED,INT_REPORT_PENDING,INT_ReportNot_Uploaded,INT_UNATTENDED_INS,DTM_CREATED_ON,intDeptId,intMonthId,intYearId)" +
                        "VALUES(" + Convert.ToInt32(UserId) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["InspectionScheduled"].ToString() + "," + DynTable.Rows[0]["PendingInspection"].ToString() + ","
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
    private void ViewCICGData()//view CENTRAL INSPECTION FRAMEWORK on normal platfrom
    {


        try
        {

            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewCICGData(Convert.ToInt32(UserId), Convert.ToInt32(ddldeptCIF.SelectedValue), Convert.ToInt32(ddlCICGMonth.SelectedValue), Convert.ToInt32(ddlYearCICG.SelectedValue));
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

    }

    private void ViewCICGMasterData() /// Mothod for bind Central Inspection Framework on master tracker
    {

        try
        {

            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewCICGMasterData(Convert.ToInt32(UserId));
            if (objServiceStatus.Count > 0)
            {
                SPcicgpending.Text = objServiceStatus[0].INT_REPORT_PENDING.ToString();
            }
            else
            {
                SPcicgpending.Text = "0";
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

    #region SPMG Portlet Added By Manoj Kumar Behera on 29-08-2019
    private void ViewSPMGServiceStateLevelDataDirect()
    {
        try
        {
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

            string serviceUrl = "https://164.100.58.41//odisha/restservices/RestServer.php?view=issuestatus";

            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
                FinancialYear = FinYear
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
                                    DynTable.Rows[0]["Issues Pending"].ToString(), DynTable.Rows[0]["Issues Pending (more than 30 days)"].ToString());
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
    private void ViewSPMGServiceDistrictLevelDataDirect()
    {
        try
        {
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

            string serviceUrl = "https://esuvidha.gov.in/odishadi/restservices/RestServer.php?view=issuestatus";

            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
                FinancialYear = FinYear
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
                                    DynTable.Rows[0]["Issues Pending"].ToString(), DynTable.Rows[0]["Issues Pending (more than 30 days)"].ToString());
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
    private void ViewSPMGMasterData()/// Method for  bind SPMG  on master tracker
    {

        try
        {

            string Finyear = ddlFinacialYear.SelectedValue;
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewSPMGMasterData(Convert.ToInt32(UserId), Convert.ToInt32(Finyear.ToString().Split('-')[0]));
            if (objServiceStatus.Count > 0)
            {
                spSpmgpnd.InnerText = objServiceStatus[0].intSPMGPending.ToString();
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
    private void ViewServiceMaster() /// Method for  bind Pending approval  on master tracker
    {

        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewServiceMaster(ddlFinacialYear.SelectedValue);
            if (objServiceStatus.Count > 0)
            {
                spanapprove.InnerText = objServiceStatus[0].strPending.ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
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
        commonMethodobj.FillService(ddldept, ddlService);// bind service record in  dropdown list
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowSearchpanel()", true);

    }
    protected void btnStatusOfApproval_Click(object sender, EventArgs e)
    {
        ViewServiceStatus();
    }
    private void ViewServiceStatus() //view Department Wise Approvals on normal platfrom
    {


        try
        {

            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewServiceStatus(Convert.ToInt32(ddldept.SelectedValue), Convert.ToInt32(ddlService.SelectedValue));
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

    }
    #endregion

    #region PEAL Portlet
    private void BindUnderEvalution() /// Method for binding data from  Peal pending data on master Tracker dashbord  .
    {
        try
        {
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();

            objswpDashboardList = commonMethodobj.BindUnderEvalution(ddlFinacialYear.SelectedValue);
            lblTrackerEvalution.InnerText = objswpDashboardList[0].strUnderEvaltion;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }

    //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
    //[WebMethod]
    //public static List<SWPDashboard> EmployeementPealDetailsBind(string Pealyear, string Pealdistrictdtls)
    //{

    //    SWPDashboard objDashboard = new SWPDashboard();
    //    List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "PED";
    //        // objDashboard.intYearId = Convert.ToInt16(Pealyear);
    //        objDashboard.strFinacialYear = Pealyear.ToString();

    //        if (Pealdistrictdtls != "")
    //        {
    //            objDashboard.strDistrictDtl = Pealdistrictdtls.ToString();
    //        }
    //        else
    //        {
    //            objDashboard.strDistrictDtl = "";
    //        }
    //        objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    return objCSRStatus;

    //}
    //ENDED BY SUROJ
    //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
    //[WebMethod]
    //public static List<SWPDashboard> EmployeementCapitalPealDetailsBind(string Pealyear, string Pealdistrictdtls)
    //{

    //    SWPDashboard objDashboard = new SWPDashboard();
    //    List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "PID";
    //        objDashboard.strFinacialYear = Pealyear.ToString();
    //        // objDashboard.intYearId = Convert.ToInt16(Pealyear);
    //        //objDashboard.intDistrictid = Convert.ToInt16(Pealdistrict);
    //        if (Pealdistrictdtls != "")
    //        {
    //            objDashboard.strDistrictDtl = Pealdistrictdtls.ToString();
    //        }
    //        else
    //        {
    //            objDashboard.strDistrictDtl = "";
    //        }
    //        objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //    }
    //    return objCSRStatus;

    //}
    //ENDED BY SUROJ
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
    protected void btnPealEmployement_Click(object sender, EventArgs e)
    {
        FillProposalEmployement();
    }
    protected void btnPealcapitalsubmit_Click(object sender, EventArgs e)
    {
        FillProposalCapital();
    }

    protected void btnPealsubmit_Click(object sender, EventArgs e)
    {
        FillProposalDetails();
        FillProposalDistwiseDetails();
        FillProposalDetailsTourism();
        FillProposalDetailsIT();
    }
    #region"Added by suroj to view Proposal dtls"
    private void FillProposalDetails() // Mothod for view data for Proposal details   on noramal platform
    {
        try
        {

            string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();


            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalDetails(Convert.ToInt16(PealQuareter), ddlPealYear.SelectedValue.ToString(), Convert.ToInt32(PealDistrict), Convert.ToInt32(UserId));
            if (objswpDashboardList.Count > 0)
            {
                lblPealApplied.Text = objswpDashboardList[0].strApplied;
                lblPealRejected.Text = objswpDashboardList[0].strRejected;
                lblPealApproved.Text = objswpDashboardList[0].strApproved;
                //lblPealEmployeemnet.Text = objswpDashboardList[0].strTotNoCapitalPropApproved;
                lblPealDeferred.Text = objswpDashboardList[0].strDeferred;
                //added by nibedita
                lblPealUnderEvalution.Text = objswpDashboardList[0].strUnderEvaltion;
                lblPealQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                Lbl_Peal_ORTPSA_State.Text = objswpDashboardList[0].strPealOrtpsaCrossedState; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
            else
            {
                lblPealApplied.Text = "0";
                lblPealRejected.Text = "0";
                lblPealApproved.Text = "0";
                lblPealQueryRaise.Text = "0";
                lblPealUnderEvalution.Text = "0";
                lblPealDeferred.Text = "0";
                Lbl_Peal_ORTPSA_State.Text = "0"; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    #endregion
    #region"Added by suroj to view Proposal dtls"
    //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
    private void FillProposalCapital()// view year wise investment on normal platfrom 
    {
        try
        {
            string PealYear = string.IsNullOrEmpty(ddlYearInvest.SelectedValue) ? default(string) : ddlYearInvest.SelectedValue.ToString();

            lblYearInvestment.Text = ddlYearInvest.SelectedItem.Text;

            string strDistrictDtl = string.Empty;
            if (hddnValue1.Value != "")
            {
                strDistrictDtl = hddnValue1.Value.ToString();
            }
            else
            {
                strDistrictDtl = "";
            }


            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalCapital(Convert.ToInt32(UserId), strDistrictDtl, PealYear);
            if (objswpDashboardList.Count > 0)
            {
                //decimal EmpInvestment = Convert.ToDecimal((Convert.ToDouble(objswpDashboardList[0].strTotCapitalPropApproved) / 100));
                lblEmpInvestment.Text = objswpDashboardList[0].strTotCapitalPropApproved;
                //lblEmpInvestment.Text = EmpInvestment.ToString("0.##");
            }
            else
            {
                lblEmpInvestment.Text = "0";
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }

    //ENDED BY SUROJ
    #endregion

    #region"Added by suroj to view Proposal dtls"
    //MODIFIED BY SUROJ KUMAR PRADHAN ON 24-10-2017
    private void FillProposalEmployement() // view year wise employeement on normal platfrom
    {
        try
        {


            string PealYear = string.IsNullOrEmpty(ddlYearEmployement.SelectedValue) ? default(string) : ddlYearEmployement.SelectedValue.ToString();
            lblYearEmployement.Text = ddlYearEmployement.SelectedItem.Text;

            string strDistrictDtl = string.Empty;
            if (hddnValue.Value != "")
            {
                strDistrictDtl = hddnValue.Value.ToString();
            }
            else
            {
                strDistrictDtl = "";
            }

            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalEmployement(Convert.ToInt32(UserId), strDistrictDtl, PealYear);
            if (objswpDashboardList.Count > 0)
            {
                lblPealEmployeemnet.Text = objswpDashboardList[0].strTotNoCapitalPropApproved;
            }
            else
            {
                lblPealEmployeemnet.Text = "0";
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    //ENDED BY SUROJ
    #endregion

    private void FillTrackerEmployement()/// Mothod for bind Employment on master tracker
    {
        try
        {
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();

            objswpDashboardList = commonMethodobj.FillTrackerEmployement(Convert.ToInt32(UserId), ddlFinacialYear.SelectedValue);
            SPEmpGen.InnerHtml = objswpDashboardList[0].strTotNoCapitalPropApproved;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    private void FillTrackerInvestment() /// Mothod for bind Investment on master tracker
    {
        try
        {
            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillTrackerInvestment(Convert.ToInt32(UserId), ddlFinacialYear.SelectedValue);
            lblmastinv.Text = objswpDashboardList[0].strTotCapitalPropApproved.ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }


    private void BindPealRecieved() /// Method for binding data from  Peal recived data on master Tracker dashbord  .
    {

        try
        {

            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.BindPealRecieved(ddlFinacialYear.SelectedValue);
            lblProposalRecieved.InnerText = objswpDashboardList[0].strPealRecived;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }
    private void BindPealApproved() /// Method for binding data from  Peal Approved data on master Tracker dashbord  .
    {

        try
        {
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.BindPealApproved(ddlFinacialYear.SelectedValue);
            lblProposalapproved.InnerText = objswpDashboardList[0].strPealApproved;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }


    //modified by suroj on 24-10-2017
    //private void BindDistrictEmployeeMentYearwise()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();
    //    for (int i = 0; i < objProjList.Count; i++)
    //    {
    //        ListItem item = new ListItem();
    //        item.Text = objProjList[i].vchDistName;
    //        item.Value = objProjList[i].intDistId.ToString();
    //        CheckBoxList1.Items.Add(item);
    //    }
    //    foreach (ListItem lstitem in CheckBoxList1.Items)
    //    {
    //        lstitem.Selected = true;
    //    }
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
    //    for (int i = 0; i < objProjList.Count; i++)
    //    {
    //        ListItem item = new ListItem();
    //        item.Text = objProjList[i].vchDistName;
    //        item.Value = objProjList[i].intDistId.ToString();
    //        chkYearwise.Items.Add(item);
    //    }
    //    foreach (ListItem lstitem in chkYearwise.Items)
    //    {
    //        lstitem.Selected = true;
    //    }

    //}
    private void FillProposalDetailsTourism() // view proposal detail of Tourism department on normal platform
    {
        try
        {

            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
            int intSecId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdTOURISM"]);
            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalDetailsTourism(PealYear, Convert.ToInt32(PealDistrict), intSecId);
            if (objswpDashboardList.Count > 0)
            {
                lblPealTourismApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealTourismRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealTourismApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealTourismUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealTourismQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                lblPealTourismDeferred.Text = objswpDashboardList[0].strDistDeferred;

                Lbl_Peal_ORTPSA_Tourism.Text = objswpDashboardList[0].strPealOrtpsaCrossedITandTourism; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
            else
            {
                lblPealTourismApplied.Text = "0";
                lblPealTourismRejected.Text = "0";
                lblPealTourismApproved.Text = "0";
                lblPealTourismUnderEvalution.Text = "0";
                lblPealTourismDeferred.Text = "0";
                lblPealTourismQueryRaise.Text = "0";

                Lbl_Peal_ORTPSA_Tourism.Text = "0"; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    private void FillProposalDetailsIT() // view proposal detail of IT department on normal platform
    {
        try
        {

            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();

            int intSecId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdIT"]);

            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalDetailsIT(PealYear, Convert.ToInt32(PealDistrict), intSecId);
            if (objswpDashboardList.Count > 0)
            {
                lblPealITApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealITRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealITApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealITUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealITQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                lblPealITDeferred.Text = objswpDashboardList[0].strDistDeferred;
                Lbl_Peal_ORTPSA_IT.Text = objswpDashboardList[0].strPealOrtpsaCrossedITandTourism; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
            else
            {
                lblPealITApplied.Text = "0";
                lblPealITRejected.Text = "0";
                lblPealITApproved.Text = "0";
                lblPealITUnderEvalution.Text = "0";
                lblPealITDeferred.Text = "0";
                lblPealITQueryRaise.Text = "0";
                Lbl_Peal_ORTPSA_IT.Text = "0"; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    #region"Added by suroj to view Proposal dtls"
    private void FillProposalDistwiseDetails()  // for view proposal dist  wise on normal platform
    {
        try
        {

            string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();

            List<SWPDashboard> objswpDashboardList = commonMethodobj.FillProposalDistwiseDetails(Convert.ToInt16(PealQuareter), ddlPealYear.SelectedValue.ToString(), Convert.ToInt32(PealDistrict), Convert.ToInt32(UserId));
            if (objswpDashboardList.Count > 0)
            {
                lblPealdistApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealdistRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealdistApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealdistUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealdistQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                lblPealdistDeferred.Text = objswpDashboardList[0].strDistDeferred;
                //lblPealDeferred.Text = "0";

                Lbl_Peal_ORTPSA_Dist.Text = objswpDashboardList[0].strPealOrtpsaCrossedDist; ///// Added by Sushant Jena on Dt. 25-May-2018
            }
            else
            {
                Lbl_Peal_ORTPSA_Dist.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    #endregion
    #endregion



    #region"ADDED BY SUROJ FOR INCENTIVE PORTLET BIND"
    private void IncentiveBind()  //view INCENTIVE DETAILS on normal platfrom
    {
        try
        {

            string IncentiveDistrict = string.IsNullOrEmpty(ddlIncentiveDistrict.SelectedValue) ? default(string) : ddlIncentiveDistrict.SelectedValue.ToString();
            string IncentiveYear = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
            List<SWPDashboard> objswpDashboardList = commonMethodobj.IncentiveBind(IncentiveYear, Convert.ToInt16(IncentiveDistrict));
            lblIncApplied.Text = objswpDashboardList[0].INCAPLLIED;
            lblIncsanctioed.Text = objswpDashboardList[0].INCSANCTIONED;
            lblIncpending.Text = objswpDashboardList[0].INCPENDING;
            lblIncrejected.Text = objswpDashboardList[0].INCREJECTED;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    private void IncentiveMasterBind() /// Method for  bind Incetive data 
    {
        try
        {

            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.IncentiveMasterBind(ddlFinacialYear.SelectedValue);
            spIncPending.InnerText = objswpDashboardList[0].INCPENDING;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }

    protected void btnIncentiveSubmit_Click(object sender, EventArgs e)
    {
        IncentiveBind();
    }
    //private void BindDistrictINCENTIVE()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "DT";
    //    objProp.vchProposalNo = " ";
    //    objProjList = objService.PopulateProjDropdowns(objProp).ToList();

    //    ddlIncentiveDistrict.DataSource = objProjList;
    //    ddlIncentiveDistrict.DataTextField = "vchDistName";
    //    ddlIncentiveDistrict.DataValueField = "intDistId";
    //    ddlIncentiveDistrict.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlIncentiveDistrict.Items.Insert(0, list);

    //}
    #endregion

    #region Land Service Portlet

    private void LandServiceBind() // view LAND ALLOTMENT DETAILS on normal platfrom
    {
        try
        {

            List<SWPDashboard> objswpDashboardList = commonMethodobj.LandServiceBind(ddlLandFinYear.SelectedValue);
            spLandAssesment.InnerText = objswpDashboardList[0].LandAssessment;
            spLandAllotByIDCO.InnerText = objswpDashboardList[0].ApplnLandAllotedByIDCO;
            spPropIDCO.InnerText = objswpDashboardList[0].PropNoForLand;
            spLandAllot.InnerText = objswpDashboardList[0].AreaAllotLand;
            spORTPSALAnd.InnerText = objswpDashboardList[0].ApplnLandORTPS;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    protected void btnLandSubmit_Click(object sender, EventArgs e)
    {
        LandServiceBind();
    }

    #endregion

    #region Query
    private void ViewQueryService() //view QUERY MONITORING on normal platfrom
    {
        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewQueryService(ddlyearquery.SelectedValue);

            if (objServiceStatus.Count > 0)
            {
                spTotalQueryRaised.Text = objServiceStatus[0].strTotalQueryRaised.ToString();
                spTotalQueryResponse.Text = objServiceStatus[0].strTotalQueryResponse.ToString();
                spNotResponse.Text = objServiceStatus[0].strTotQuerynotRecTimeline.ToString();
                spAvgTimeTaken.Text = objServiceStatus[0].strAvgRaiseQuery.ToString();
                spTotalQueryPendings.Text = objServiceStatus[0].strTotalQueryPending.ToString();
            }
            else
            {
                spTotalQueryRaised.Text = "0";
                spTotalQueryResponse.Text = "0";
                spNotResponse.Text = "0";
                spAvgTimeTaken.Text = "0";
                spTotalQueryPendings.Text = "0";
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
    private void ViewQueryServicepeal()//view QUERY MONITORING on normal platfrom
    {
        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewQueryServicepeal(ddlyearquery.SelectedValue);
            if (objServiceStatus.Count > 0)
            {
                spRaisedpeal.Text = objServiceStatus[0].strPEALQueryRaised.ToString();
                spResolvedpeal.Text = objServiceStatus[0].strPEALQueryResolved.ToString();
                spPendingpeal.Text = objServiceStatus[0].strPEALQueryPending.ToString();
                spResponcenotRecPeal.Text = objServiceStatus[0].strPEALQueryPendingPast.ToString();
                spAvgTimeQuerypeal.Text = objServiceStatus[0].strPEALQueryAvg.ToString();
            }
            else
            {
                spRaisedpeal.Text = "0";
                spResolvedpeal.Text = "0";
                spPendingpeal.Text = "0";
                spResponcenotRecPeal.Text = "0";
                spAvgTimeQuerypeal.Text = "0";

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }

    private void ViewQueryServiceIncentive()//view QUERY MONITORING on normal platfrom
    {
        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewQueryServiceIncentive(ddlyearquery.SelectedValue);
            if (objServiceStatus.Count > 0)
            {
                spRaisedIncentive.Text = objServiceStatus[0].strTotalQueryRaised.ToString();
                spResolvedIncentive.Text = objServiceStatus[0].strTotalQueryResponse.ToString();
                spPendingIncentive.Text = objServiceStatus[0].strTotalQueryPending.ToString();
                spResponcenotResIncentive.Text = objServiceStatus[0].strTotQuerynotRecTimeline.ToString();
                spAvgTimeQueryIncentive.Text = objServiceStatus[0].strAvgRaiseQuery.ToString();
            }
            else
            {
                spRaisedIncentive.Text = "0";
                spResolvedIncentive.Text = "0";
                spPendingIncentive.Text = "0";
                spResponcenotResIncentive.Text = "0";
                spAvgTimeQueryIncentive.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
    }


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ViewQueryService();
        ViewQueryServicepeal();
        ViewQueryServiceIncentive();
    }
    #endregion

    #region Event Click
    protected void ddlFinacialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        IncentiveMasterBind();
        if (ConfigurationManager.AppSettings["IDCO"] == "ON")
        {
            InsertAppa(ddlFinacialYear);
        }
            
        BindUnderEvalution();
        FillTrackerEmployement();
        FillTrackerInvestment();
        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            InsertCSRMaster(ddlFinacialYear);
        }
          
        ViewSPMGMasterData();
        ViewServiceMaster();
        string FinYear = ddlFinacialYear.SelectedItem.Text;
        string FinTearFinal = FinYear.Remove(4, 3);


        string strFY = DateTime.Now.Month > 3 ? DateTime.Now.Year.ToString() : Convert.ToString(DateTime.Today.Year - 1);

        if (FinTearFinal == strFY)
        {
            ViewCICGMasterData();
        }
        else
        {
            SPcicgpending.Text = "0";
        }
        BindPealRecieved();
        BindPealApproved();

        BindMasterGrievanceportlet();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        /*-----------------------------------------------------------------*/
        ///Fill Dropdownlist for Financial Year
        /*-----------------------------------------------------------------*/

        commonMethodobj.FillFinancialYear(ddlAppaYear); //bind both value field  2016-17 and  text field 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYear(ddlFinacialYear); //bind both value field 2016-17 and  text field 2016-17  FinalcealYear on master tracker
        commonMethodobj.FillFinancialYear(ddlPealYear); //bind both value field 2016-17 and  text field 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYear(ddlYearInvest); //bind both value field 2016-17 and  text field 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYear(ddlYearEmployement); //bind both value field 2016-17 and  text field 2016-17  FinalcealYear on Normal platform
        DashboradCommon.MONTHBIND(ddlAppaMonth); //bind both value  field 1 and  text field January    Month value  on Normal platform
        DashboradCommon.MONTHBIND(ddlCICGMonth); //bind both value field 1 and  text field  January    Month value  on Normal platform
        DashboradCommon.YEARBIND(ddlYearCICG); //bind both value field 2016 and  text field 2016    Year value  on Normal platform
        commonMethodobj.FillFinancialYear(ddlIncentiveYear); //bind both value field 2016-17 and  text field 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYear(ddlyearquery); //bind both value field 2016-17 and  text field 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYear(ddlLandFinYear); //bind both value field 2016-17 and  text field 2016-17  FinalcealYear on Normal platform
        commonMethodobj.FillFinancialYearWithYear(ddlgyear); //bind both value field 2016 and  text field 2016-17  FinalcealYear and year value  on Normal platform

        /*-----------------------------------------------------------------*/
        //// Master Tracker
        /*-----------------------------------------------------------------*/
        BindUnderEvalution();// Single Window Application Status
        BindPealRecieved();// Single Window Application Status
        BindPealApproved();// Single Window Application Status
        IncentiveMasterBind(); // Incentive Details
        ViewSPMGMasterData();//State Project Monitoring Group
        ViewServiceMaster();//Pending Approvals
        FillTrackerInvestment();//Investment
        ViewCICGMasterData();//Central Inspection Framework
        BindMasterGrievanceportlet();//Grievance Status
        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            InsertCSRMaster(ddlFinacialYear);//CSR Spend
        }

           
        FillTrackerEmployement();//Employment 
        if (ConfigurationManager.AppSettings["IDCO"] == "ON")
        {
            InsertAppa(ddlFinacialYear); // IDCO Post Allotment Applications
        }
           

        /*-----------------------------------------------------------------*/
        ////Portlet Section
        /*-----------------------------------------------------------------*/
        commonMethodobj.FillDist(ddlPEALDistrict); // bind dist in PEAL dropdown list for SINGLE WINDOW APPLICATION STATUS
        FillProposalDetails();//SINGLE WINDOW APPLICATION STATUS 
        FillProposalDistwiseDetails(); //SINGLE WINDOW APPLICATION STATUS 
        FillProposalDetailsIT();//SINGLE WINDOW APPLICATION STATUS 
        FillProposalDetailsTourism(); //SINGLE WINDOW APPLICATION STATUS 

        commonMethodobj.FillDist(chkYearwise); //Bind dist in checkbox list for YEAR WISE INVESTMENT
        FillProposalCapital(); //YEAR WISE INVESTMENT

        commonMethodobj.FillDist(CheckBoxList1); //Bind dist in checkbox list for YEAR WISE EMPLOYMENT
        FillProposalEmployement(); //YEAR WISE EMPLOYMENT

        if (ConfigurationManager.AppSettings["SPMG"] == "ON")
        {
            ViewSPMGServiceStateLevelDataDirect();// STATE PROJECT MONITORING GROUP
            ViewSPMGServiceDistrictLevelDataDirect();// STATE PROJECT MONITORING GROUP
        }
           
        

        commonMethodobj.FillDepartment(ddldept); // bind Department record in dropdown list
        ViewServiceStatus();//DEPARTMENT WISE APPROVALS

        commonMethodobj.FillDepartmentForCIF(ddldeptCIF); // bind Department record in CIF dropdown list
        if (ConfigurationManager.AppSettings["CICG"] == "ON")
        {
            InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
        }
           
        ViewCICGData();//CENTRAL INSPECTION FRAMEWORK

        commonMethodobj.FillDist(ddlAPAADistrict);// bind dist in dropdown list  for IDCO POST ALLOTMENT APPLICATIONS 
        if (ConfigurationManager.AppSettings["IDCO"] == "ON")
        {
            InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
        }
            
        ViewApaaStatus();//IDCO POST ALLOTMENT APPLICATIONS

        commonMethodobj.FillDist(chkCSRDistrct);// bind dist in csr checkbox list
        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            CSRPortletSatus();//CSR ACTIVITIES
        }
            

        commonMethodobj.FillDist(ddlIncentiveDistrict); // bind dist in dropdown list  for INCENTIVE DETAILS
        IncentiveBind();//INCENTIVE DETAILS

        LandServiceBind();//LAND ALLOTMENT DETAILS

        ViewQueryService(); //QUERY MONITORING
        ViewQueryServicepeal();//QUERY MONITORING
        ViewQueryServiceIncentive();//QUERY MONITORING

        commonMethodobj.FillDist(ddlgdist);// bind dist in dropdown list  for GRIEVANCE STATUS
        BindGrievanceportlet();//GRIEVANCE STATUS



        /// InsertCSRStatus(hdnCSRDistrct, ddlCSRYear); Commented for not in use
       // ddlspmgyear.SelectedValue = "2017";
        //InsertSPMGStatus();
        spLastUpdate.InnerText = DateTime.Now.ToString();





    }

    #endregion
    //protected void lnkExport_Click(object sender, EventArgs e)
    //{
    //    //Fillgrid1();
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Details.xls"));
    //    HttpContext.Current.Response.ContentType = "application/ms-excel";
    //    System.IO.StringWriter sw = new System.IO.StringWriter();
    //    HtmlTextWriter htw = new HtmlTextWriter(sw);
    //    xyz.RenderControl(htw);
    //    HttpContext.Current.Response.Write(sw.ToString());
    //    HttpContext.Current.Response.End();
    //}
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //}

    #region SPMG Portlet Database Interaction Added By Manoj Kumar Behera on 29-08-2019
    private void ViewStatelevelSPMGData() //STATE PROJECT MONITORING GROUP
    {
        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewStatelevelSPMGData(Convert.ToInt32(ddlspmgyear.SelectedValue)).ToList();
            if (objServiceStatus.Count > 0)
            {
                spmgraised.InnerHtml = objServiceStatus[0].intSPMGRaised.ToString();
                spmgresolved.InnerHtml = objServiceStatus[0].intSPMGResolved.ToString();
                spmgpending.InnerHtml = objServiceStatus[0].intSPMGPending.ToString();
                spmg30pending.InnerHtml = objServiceStatus[0].intSPMGIssuePending.ToString();
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

    }
    private void ViewDistrictlevelSPMGData()//STATE PROJECT MONITORING GROUP
    {
        try
        {
            List<SWPDashboard> objServiceStatus = commonMethodobj.ViewDistrictlevelSPMGData(Convert.ToInt32(ddlspmgyear.SelectedValue));
            if (objServiceStatus.Count > 0)
            {
                spmgdistraised.InnerHtml = objServiceStatus[0].intSPMGRaised.ToString();
                spmgdistresolved.InnerHtml = objServiceStatus[0].intSPMGResolved.ToString();
                spmgdistpending.InnerHtml = objServiceStatus[0].intSPMGPending.ToString();
                spmg30distpending.InnerHtml = objServiceStatus[0].intSPMGIssuePending.ToString();
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

    }
    private void InsertViewStatelevelSPMGData(string recived, string resolved, string pending, string pendinglast)//STATE PROJECT MONITORING GROUP
    {
        try
        {
            DataTable PnlDt = commonMethodobj.InsertViewStatelevelSPMGData(Convert.ToInt32(UserId), Convert.ToInt32(ddlspmgyear.SelectedValue), recived, resolved, pending, pendinglast);
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
            Util.LogError(ex, "Dashboard");
        }
    }
    private void InsertViewDistrictlevelSPMGData(string recived, string resolved, string pending, string pendinglast) // STATE PROJECT MONITORING GROUP
    {

        try
        {

            DataTable PnlDt = commonMethodobj.InsertViewDistrictlevelSPMGData(Convert.ToInt32(UserId), Convert.ToInt32(ddlspmgyear.SelectedValue), recived, resolved, pending, pendinglast);
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
            Util.LogError(ex, "Dashboard");
        }


    }

    #endregion

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
    private void BindMasterGrievanceportlet()  /// Mothod for bind Grievance Status on master tracker
    {
        try
        {
            DataTable Gdt = new DataTable();
            Gdt = commonMethodobj.BindMasterGrievanceportlet(Convert.ToInt32(Session["Userid"]), ddlFinacialYear.SelectedItem.Text);
            if (Gdt.Rows.Count > 0)
            {
                Spangpending.InnerText = Gdt.Rows[0]["PENDING"].ToString();
            }
            else
            {
                Spangpending.InnerText = "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    private void BindGrievanceportlet()//Method for  GRIEVANCE STATUS on normal platfrom
    {
        try
        {
            int intDistrictId = 0;
            if (ddlgdist.SelectedIndex > 0)
            {
                intDistrictId = Convert.ToInt32(ddlgdist.SelectedValue);
            }
            else
            {
                intDistrictId = 0;
            }
            DataTable Gdt = commonMethodobj.BindGrievanceportlet(Convert.ToInt32(UserId), ddlgyear.SelectedItem.Text, intDistrictId);
            if (Gdt.Rows.Count > 0)
            {
                lblGapplied.Text = Gdt.Rows[0]["APPLIED"].ToString();
                lblGrsolved.Text = Gdt.Rows[0]["RESOLVED"].ToString();
                lblGpending.Text = Gdt.Rows[0]["PENDING"].ToString();
                lblGrejected.Text = Gdt.Rows[0]["REJECTED"].ToString();
            }
            else
            {
                lblGapplied.Text = "0";
                lblGrsolved.Text = "0";
                lblGpending.Text = "0";
                lblGrejected.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    protected void btnGSearch_Click(object sender, EventArgs e)
    {
        BindGrievanceportlet();
    }
}