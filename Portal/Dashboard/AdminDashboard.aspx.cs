#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
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
using BusinessLogicLayer.Incentive;
using System.Drawing;
#endregion

public partial class Portal_Dashboard_AdminDashboard : SessionCheck
{
    #region Global variable
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();
    int intRecCount = 0;
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Redirect("../Proposal/ViewProposal.aspx");

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }

        if (Session["desId"].ToString() == "1" || Session["desId"].ToString() == "8")
        {
            if (!IsPostBack)
            {
                spLastUpdate.InnerText = DateTime.Now.ToString();
                FillMasterFinYr();
                FillFinYrPortlet(ddlAppaYear);
                FillFinYrPortlet(ddlCSRYear);
                FillFinYrPortletS(ddlspmgyear);
                //FillFinYrPortlet(ddlYearCICG);
                if (ConfigurationManager.AppSettings["IDCO"] == "ON")
                {
                    InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
                    InsertAppa(ddlFinacialYear);
                }
                if (ConfigurationManager.AppSettings["CICG"] == "ON")
                {
                    InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
                }
                if (ConfigurationManager.AppSettings["CSR"] == "ON")
                {
                    InsertCSRStatus(hdnCSRDistrct, ddlCSRYear);
                    InsertCSRMaster(ddlFinacialYear);
                    CSRPortletSatus();
                }
                   
                //ddlspmgyear.SelectedValue = "2017";
                //InsertSPMGStatus();
               
                
                
                   
                FillTrackerInvestment();
                FillTrackerEmployement();
                BindDistrict();
                FillFinYrPortlet(ddlPealYear);
                FillProposalDetails();
                ViewServiceStatus();
                ViewServiceMaster();
                BindDept();
                //ViewSPMGData();
                //ViewSPMGMasterData();
                if (ConfigurationManager.AppSettings["SPMG"] == "ON")
                {
                    ViewSPMGServiceStateLevelDataDirect();
                    ViewSPMGServiceDistrictLevelDataDirect();
                }
                   
               
                ViewSPMGMasterData();
                BindDistrictPEAL();
                BindDeptCICG();
                BindDistrictInvest();
                FillFinYrPortlet(ddlYearInvest);
                FillFinYrPortlet(ddlYearEmployement);
                BindDistrictEmployeeMentYearwise();
                DashboradCommon.MONTHBIND(ddlAppaMonth);
                DashboradCommon.MONTHBIND(ddlCICGMonth);
                DashboradCommon.YEARBIND(ddlYearCICG);

                FillFinYrPortlet(ddlIncentiveYear);
                BindDistrictAPAA();
                ViewCICGMasterData();
                ViewCICGData();
               
                ViewApaaStatus();
                FillProposalEmployement();
                FillProposalCapital();
                BindDistrictINCENTIVE();
                IncentiveMasterBind();
                IncentiveBind();
                BindUnderEvalution();
                //AMSBIND();
                BindPealRecieved();
                BindPealApproved();
                FillProposalDistwiseDetails();
                FillFinYrPortlet(ddlyearquery);
                FillFinYrPortlet(ddlLandFinYear);
                LandServiceBind();
                ViewQueryService(); ///added by nibedita behera on 21-12-2017
                ViewQueryServicepeal();
                ViewQueryServiceIncentive();

                //added by Ritika Lath
                FillIncentiveApplicationDropdown();
                FillProposalDetailsTourism();
                FillProposalDetailsIT();

                //added by monalisa for AMS
                FillForwardProj();

                ////For Grievance
                FillFinYrPortletS(ddlgyear);
                BindDistrictGrievance();
                BindMasterGrievanceportlet();
                BindGrievanceportlet();
            }
        }
        else
        {
            Response.Redirect("~/Portal/Default.aspx");
        }
    }
    #endregion

    #region added by ritika lath 13th dec 2017
    private void FillIncentiveApplicationDropdown()
    {
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        DataSet objDa = objBuisnessLayer.BindDropdown("rpt");
        if (objDa != null)
        {
            FillDropDown(objDa.Tables[0], "Unit Type", drpInctUnitType);
            FillDropDown(objDa.Tables[1], "District", drpInctAppDistrict);
            FillDropDown(objDa.Tables[2], "Policy", drpPolicy);
        }
        for (int i = 2010; i <= DateTime.Today.Year; i++)
        {
            drpInctYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        drpInctYear.Items.Insert(0, new ListItem("-Select Year-", "0"));
        //drpInctYear.SelectedValue = DateTime.Today.Year.ToString();
    }

    /// <summary>
    /// Function to bind the drodown, main code to bind the dropdown
    /// </summary>
    /// <param name="objDt">Datatable with all values</param>
    /// <param name="strHeaderType">type of data in dropdown</param>
    /// <param name="objDropdown">dropdown to bind</param>
    private void FillDropDown(DataTable objDt, string strHeaderType, DropDownList objDropdown)
    {
        objDropdown.Items.Clear();
        if (objDt != null && objDt.Rows.Count > 0)
        {
            objDropdown.DataSource = objDt;
            objDropdown.DataTextField = "NAME";
            objDropdown.DataValueField = "ID";
            objDropdown.DataBind();
        }
        objDropdown.Items.Insert(0, new ListItem(string.Format("-Select {0}-", strHeaderType), "0"));
    }
    #endregion

    #region CSR Portlet
    private void InsertCSRStatus(HiddenField hdnCSRDistrct, DropDownList ddlCSRYear)
    {
        string finalquery = string.Empty;
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = "1";
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string District = "";
        if (hdnCSRDistrct.Value != "")
        {
            District = hdnCSRDistrct.Value.ToString();
        }
        else
        {
            District = "0";
        }
        //string District = string.IsNullOrEmpty(ddlDistrict.SelectedValue) ? default(string) : ddlDistrict.SelectedValue.ToString();
        //string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
        string Year = string.IsNullOrEmpty(ddlCSRYear.SelectedItem.Text) ? default(string) : ddlCSRYear.SelectedItem.Text;
        if (Year == "--Select--")
        {
            Year = "0";
        }
        else
        {
            Year = ddlCSRYear.SelectedItem.Text;
        }
        //string UniqueKey = Session["UID"].ToString();
        string UniqueKey = "0";
        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetTotalAmount/" + UniqueKey + "/" + Type + "/" + District + "/" + Year;
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
                    string output = DynTable.Rows[0]["GetTotalAmountResult"].ToString();
                    string[] finalOut = output.Split(':');
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "SELECT ADMINID FROM T_CSR_SpentDtls_Admin WHERE CONVERT(DATE,dtmCreatedon)='" + DateTime.Now.ToString("dd-MMM-yy") + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        finalquery = "UPDATE T_CSR_SpentDtls_Admin SET adminid=" + Convert.ToInt32(Session["Userid"]) + ",TotalAmount=" + finalOut[2].ToString() + ",TotalProject=" + DynTable.Rows[0]["TotalProject"].ToString() + "," +
                              "dtmCreatedon='" + DateTime.Now.ToString("dd-MMM-yy") + "',vchYear='" + Year + "',intDistrictId='" + District + "' WHERE adminid='" + ds.Tables[0].Rows[0]["ADMINID"].ToString() + "'";
                    }
                    else
                    {
                        if (finalOut[2].ToString() != "")
                        {
                            ViewState["finalvalue"] = finalOut[2].ToString(); ;
                        }
                        else
                        { ViewState["finalvalue"] = "0"; }
                        finalquery = "INSERT INTO T_CSR_SpentDtls_Admin(adminid,TotalAmount,TotalProject,dtmCreatedon,vchYear,intDistrictId)" +
                    "VALUES(" + Convert.ToInt32(Session["Userid"]) + "," + ViewState["finalvalue"] + "," + DynTable.Rows[0]["TotalProject"].ToString() + "," +
                    "'" + DateTime.Now.ToString("dd-MMM-yy") + "','" + Year + "'," + District + ")";
                    }
                    cmd = new SqlCommand(finalquery, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch
        {
        }
    }
    private void CurrentMonthSelect(DropDownList ddlQuarter, DropDownList ddlYear)
    {
        decimal Month = Math.Ceiling(DateTime.Today.Month / 3m);
        ddlQuarter.SelectedValue = Month.ToString();
        ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
    }
    protected void btnCSRStatus_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            InsertCSRStatus(hdnCSRDistrct, ddlCSRYear);
            CSRPortletSatus();
        }
           
        
    }
    private void CSRPortletSatus()
    {
        //////// Dispaly data directly from service implemented by Sushant Jena on Dt. 29-May-2018
        try
        {
            string strType = "1";
            string strYear = "0";
            string strUniqueKey = "0";
            string strDistrict = "0";

            if (hdnCSRDistrct.Value != "")
            {
                strDistrict = hdnCSRDistrct.Value;
            }

            if (ddlCSRYear.SelectedItem.Text != "--Select--")
            {
                strYear = ddlCSRYear.SelectedItem.Text;
            }

            string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetTotalAmount/" + strUniqueKey + "/" + strType + "/" + strDistrict + "/" + strYear;
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
                    if (DynTable.Rows.Count > 0)
                    {
                        string output = DynTable.Rows[0]["GetTotalAmountResult"].ToString();
                        string[] finalOut = output.Split(':');

                        SPProject.InnerHtml = Convert.ToString(DynTable.Rows[0]["TotalProject"]);
                        SPRecommendedCouncil.InnerHtml = Convert.ToString(DynTable.Rows[0]["TotalRecommendProject"]);
                        decimal TotAmount = Convert.ToDecimal(DynTable.Rows[0]["TotalAmount"]) / 100;
                        SPSpent.InnerHtml = Convert.ToString(Math.Round(TotAmount));
                        SPUudTknProj.InnerHtml = DynTable.Rows[0]["RecCSRProjectsUnderTakenByCorp"].ToString();
                    }
                    else
                    {
                        SPProject.InnerHtml = "0";
                        SPRecommendedCouncil.InnerHtml = "0";
                        SPSpent.InnerHtml = "0";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }


        //objSWP = new SWPDashboard();
        //objserviceDashboard = new DashboardBusinessLayer();
        //try
        //{
        //    objSWP.strAction = "VA";
        //    objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //    if (hdnCSRDistrct.Value == "")
        //    {
        //        objSWP.Distirict = "0";
        //    }
        //    else
        //    {
        //        objSWP.Distirict = hdnCSRDistrct.Value;
        //    }

        //    if (ddlCSRYear.SelectedItem.Text == "--Select--")
        //    {
        //        objSWP.Year = "0";
        //    }
        //    else
        //    {
        //        objSWP.Year = ddlCSRYear.SelectedItem.Text;
        //    }
        //    List<SWPDashboard> objCSRCATStatus = objserviceDashboard.GetDashCSRDtls(objSWP).ToList();
        //    if (objCSRCATStatus.Count > 0)
        //    {
        //        SPProject.InnerHtml = objCSRCATStatus[0].TotalProject.ToString();
        //        //decimal TotAmount = Convert.ToDecimal((Convert.ToDouble(objCSRCATStatus[0].TotalAmount.ToString()) / 100));
        //        //SPSpent.InnerHtml = TotAmount.ToString("0.##");
        //        //SPSpent.InnerHtml = objCSRCATStatus[0].TotalAmount.ToString();

        //        decimal TotAmount = Convert.ToDecimal((Convert.ToDouble(objCSRCATStatus[0].TotalAmount.ToString()) / 100));
        //        SPSpent.InnerHtml = Math.Round(TotAmount).ToString();
        //    }
        //    else
        //    {
        //        SPSpent.InnerHtml = "0";
        //        SPProject.InnerHtml = "0";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objSWP = null;
        //    objserviceDashboard = null;
        //}
    }

    private void BindDistrict()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();
        for (int i = 0; i < objProjList.Count; i++)
        {
            ListItem item = new ListItem();
            item.Text = objProjList[i].vchDistName;
            item.Value = objProjList[i].intDistId.ToString();
            chkCSRDistrct.Items.Add(item);
        }
        foreach (ListItem lstitem in chkCSRDistrct.Items)
        {
            lstitem.Selected = true;
        }
    }
    private void InsertCSRMaster(DropDownList ddlyear)
    {
        string finalquery = string.Empty;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = "1";
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string Year = string.IsNullOrEmpty(ddlFinacialYear.SelectedItem.Text) ? default(string) : ddlFinacialYear.SelectedItem.Text;
        if (Year == "--Select--")
        {
            Year = "0";
        }
        else
        {
            Year = ddlyear.SelectedItem.Text;
        }
        //string UniqueKey = Session["UID"].ToString();
        string UniqueKey = "0";

        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetTotalAmount/" + UniqueKey + "/" + Type + "/" + 0 + "/" + Year;
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
                    string output = DynTable.Rows[0]["GetTotalAmountResult"].ToString();
                    string[] finalOut = output.Split(':');
                    //SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    //if (con.State == ConnectionState.Closed)
                    //{
                    //    con.Open();
                    //}
                    //decimal TotAmount = Convert.ToDecimal((Convert.ToDouble(finalOut[2].ToString()) / 100));
                    //SPNetSpent.InnerHtml = TotAmount.ToString("0.##");
                    //SPNetSpent.InnerHtml = Math.Ceiling(Convert.ToDouble(finalOut[2])).ToString();
                    //decimal TotAmount = Convert.ToDecimal((finalOut[2].ToString()));
                    //SPNetSpent.InnerHtml = Math.Round(TotAmount).ToString();
                    // decimal TotAmount = Convert.ToDecimal((Convert.ToDouble(finalOut[2].ToString()) / 100));
                    decimal TotAmount = Convert.ToDecimal(DynTable.Rows[0]["TotalAmount"]) / 100;
                    //SPNetSpent.InnerHtml = Math.Round(Convert.ToDouble(finalOut[2].ToString())).ToString();
                    SPNetSpent.InnerText = Math.Round(TotAmount).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            SPNetSpent.InnerHtml = "0";
            Util.LogError(ex, "Dashboard");
        }
    }
    #endregion

    #region APAA portlet
    private void BindDistrictAPAA()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlAPAADistrict.DataSource = objProjList;
        ddlAPAADistrict.DataTextField = "vchDistName";
        ddlAPAADistrict.DataValueField = "intDistId";
        ddlAPAADistrict.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlAPAADistrict.Items.Insert(0, list);

    }
    private void InsertAppa(DropDownList ddlYear)
    {
        string finalquery = string.Empty;
        SqlCommand cmd = null;
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
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
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
    private void ViewCICGData()
    {

        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "VCI";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objSWP.intDeptId = Convert.ToInt32(ddldeptCIF.SelectedValue);
            objSWP.intMonthId = Convert.ToInt32(ddlCICGMonth.SelectedValue);
            objSWP.intYearId = Convert.ToInt32(ddlYearCICG.SelectedValue);
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetCICGDashboardService(objSWP).ToList();
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

    private void ViewCICGMasterData()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "VCI";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);


            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetCICGDashboardService(objSWP).ToList();
            if (objServiceStatus.Count > 0)
            {
                SPcicgpending.InnerHtml = objServiceStatus[0].INT_REPORT_PENDING.ToString();
            }
            else
            {
                SPcicgpending.InnerHtml = "0";
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

    private void BindDeptCICG()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        objSWP.strAction = "CDP";
        List<SWPDashboard> objServiceStatus = objserviceDashboard.BindCICGDepartment(objSWP).ToList();
        ddldeptCIF.DataSource = objServiceStatus;
        ddldeptCIF.DataTextField = "VCH_DEPT_NAME";
        ddldeptCIF.DataValueField = "intDeptId";
        ddldeptCIF.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddldeptCIF.Items.Insert(0, list);
    }

    protected void btnCICGStatus_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["CICG"] == "ON")
        {
            InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
        }
          
        ViewCICGData();
    }
    #endregion

    #region SPMG Portlet

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
                            var ss = webResponse.StatusCode;

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
    private void ViewSPMGMasterData()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objSWP.strAction = "SPMGALL";
            string Finyear = ddlFinacialYear.SelectedValue;
            objSWP.intYearId = Convert.ToInt32(Finyear.ToString().Split('-')[0]);
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
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
        finally
        {
            objSWP = null;
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
    private void ViewServiceMaster()
    {
        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        objDashboard.strAction = "DM";
        objDashboard.intDeptId = 0;
        objDashboard.intServiceId = 0;
        objDashboard.strFinacialYear = ddlFinacialYear.SelectedValue;
        List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
        if (objServiceStatus.Count > 0)
        {
            spanapprove.InnerText = objServiceStatus[0].strPending.ToString();
        }
    }
    private void BindService()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
        ddlService.DataSource = objServicelist;
        ddlService.DataTextField = "strServiceName";
        ddlService.DataValueField = "intServiceId";
        ddlService.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlService.Items.Insert(0, list);

    }
    private void BindDept()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindDepartment("DP").ToList();
        ddldept.DataSource = objServicelist;
        ddldept.DataTextField = "strdeptname";
        ddldept.DataValueField = "Deptid";
        ddldept.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddldept.Items.Insert(0, list);
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindService();
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowSearchpanel()", true);

    }
    protected void btnStatusOfApproval_Click(object sender, EventArgs e)
    {
        ViewServiceStatus();
    }
    private void ViewServiceStatus()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "D";
            //objDashboard.intInvestorId = 0;
            objDashboard.intDeptId = Convert.ToInt32(ddldept.SelectedValue);
            objDashboard.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
            List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
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
            objDashboard = null;
            objDashboardBal = null;
        }
    }
    #endregion

    #region PEAL Portlet
    [WebMethod]
    public static List<SWPDashboard> EmployeementPealDetailsBind(string Pealyear, string Pealdistrictdtls)
    {

        SWPDashboard objDashboard = new SWPDashboard();
        List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "PED";
            //objDashboard.intYearId = Convert.ToInt16(Pealyear);
            objDashboard.strFinacialYear = Pealyear.ToString();
            if (Pealdistrictdtls != "")
            {
                objDashboard.strDistrictDtl = Pealdistrictdtls.ToString();
            }
            else
            {
                objDashboard.strDistrictDtl = "";
            }
            objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        return objCSRStatus;

    }
    [WebMethod]
    public static List<SWPDashboard> EmployeementCapitalPealDetailsBind(string Pealyear, string Pealdistrictdtls)
    {

        SWPDashboard objDashboard = new SWPDashboard();
        List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "PID";
            //objDashboard.intYearId = Convert.ToInt16(Pealyear);
            objDashboard.strFinacialYear = Pealyear.ToString();
            //objDashboard.intDistrictid = Convert.ToInt16(Pealdistrict);
            if (Pealdistrictdtls != "")
            {
                objDashboard.strDistrictDtl = Pealdistrictdtls.ToString();
            }
            else
            {
                objDashboard.strDistrictDtl = "";
            }
            objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        return objCSRStatus;

    }
    [WebMethod]
    public static List<SWPDashboard> PealDetailsBind()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "PPD";
            objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        return objCSRStatus;

    }
    private void FillProposalCapital()
    {
        try
        {
            SWPDashboard objSWP = new SWPDashboard();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
            objSWP.strAction = "PCI";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            string PealYear = string.IsNullOrEmpty(ddlYearInvest.SelectedValue) ? default(string) : ddlYearInvest.SelectedValue.ToString();
            //if (ddlYearInvest.SelectedItem.Text == "--Select--")
            //{
            //   lblYearInvestment.Text = DateTime.Now.Year.ToString();
            //}
            //else
            //{
            lblYearInvestment.Text = ddlYearInvest.SelectedItem.Text;
            // }
            if (hddnValue1.Value != "")
            {
                objSWP.strDistrictDtl = hddnValue1.Value.ToString();
            }
            else
            {
                objSWP.strDistrictDtl = "";
            }
            objSWP.strFinacialYear = PealYear.ToString();
            //objSWP.intDistrictid = Convert.ToInt16(PealDistrict);
            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
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
        finally
        {
            objSWP = null;
        }
    }

    #region"Added by suroj to view Proposal dtls"
    private void FillProposalEmployement()
    {
        try
        {

            SWPDashboard objSWP = new SWPDashboard();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
            objSWP.strAction = "PD";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            string PealYear = string.IsNullOrEmpty(ddlYearEmployement.SelectedValue) ? default(string) : ddlYearEmployement.SelectedValue.ToString();
            //string PealDistrict = string.IsNullOrEmpty(ddlDistrictEmployeement.SelectedValue) ? default(string) : ddlDistrictEmployeement.SelectedValue.ToString();
            //string PealDistrict = hddnValue.Value.ToString();
            //if (ddlYearEmployement.SelectedItem.Text == "--Select--")
            //{
            //    lblYearEmployement.Text = DateTime.Now.Year.ToString();
            //}
            //else
            //{
            lblYearEmployement.Text = ddlYearEmployement.SelectedItem.Text;
            // }
            if (hddnValue.Value != "")
            {
                objSWP.strDistrictDtl = hddnValue.Value.ToString();
            }
            else
            {
                objSWP.strDistrictDtl = "";
            }
            //objSWP.intYearId = Convert.ToInt16(PealYear);
            objSWP.strFinacialYear = PealYear.ToString();
            //objSWP.intDistrictid = Convert.ToInt16(PealDistrict);
            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
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
        finally
        {
            objSWP = null;
        }
    }
    #endregion

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
    private void FillProposalDetails()
    {
        try
        {
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "PB";
            string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
            objSWP.intQuarter = Convert.ToInt16(PealQuareter);
            //objSWP.intYearId = Convert.ToInt16(PealYear);
            objSWP.strFinacialYear = ddlPealYear.SelectedValue.ToString();
            objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
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
                //decimal EmpInvestment = Convert.ToDecimal((Convert.ToDouble(objswpDashboardList[0].strTotCapitalPropApproved) / 100));
                //lblEmpInvestment.Text = objswpDashboardList[0].strTotCapitalPropApproved;
                //lblmastinv.Text = objswpDashboardList[0].strTotCapitalPropApproved;
                //SPEmpGen.InnerHtml = objswpDashboardList[0].strTotNoCapitalPropApproved;

                //lblPealDeferred.Text = "0";

                Lbl_Peal_ORTPSA_State.Text = objswpDashboardList[0].strPealOrtpsaCrossedState; ///// Added by Sushant Jena on Dt. 26-May-2018
            }
            else
            {
                lblPealApplied.Text = "0";
                lblPealRejected.Text = "0";
                lblPealApproved.Text = "0";
                //lblPealEmployeemnet.Text = "0";
                //added by nibedita
                lblPealUnderEvalution.Text = "0";
                lblPealQueryRaise.Text = "0";
                //decimal EmpInvestment = Convert.ToDecimal((Convert.ToDouble(objswpDashboardList[0].strTotCapitalPropApproved) / 100));
                //lblEmpInvestment.Text = "0";
                //lblmastinv.Text = "0";
                //SPEmpGen.InnerHtml = "0";

                lblPealDeferred.Text = "0";
                Lbl_Peal_ORTPSA_State.Text = "0"; ///// Added by Sushant Jena on Dt. 26-May-2018
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
        }
    }
    #endregion

    private void BindUnderEvalution()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        objSWP.strAction = "UE";
        objSWP.strFinacialYear = ddlFinacialYear.SelectedValue;
        objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        lblTrackerEvalution.InnerText = objswpDashboardList[0].strUnderEvaltion;
    }

    #region"Added by suroj to view Proposal dtls"
    private void FillProposalDistwiseDetails()
    {
        try
        {
            objSWP = new SWPDashboard();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "PDD";
            string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
            objSWP.intQuarter = Convert.ToInt16(PealQuareter);
            //objSWP.intYearId = Convert.ToInt16(PealYear);
            objSWP.strFinacialYear = ddlPealYear.SelectedValue.ToString();
            objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
            if (objswpDashboardList.Count > 0)
            {
                lblPealdistApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealdistRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealdistApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealdistUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealdistQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                lblPealdistDeferred.Text = objswpDashboardList[0].strDistDeferred;
                Lbl_Peal_ORTPSA_Dist.Text = objswpDashboardList[0].strPealOrtpsaCrossedDist; ///// Added by Sushant Jena on Dt. 26-May-2018
            }
            else
            {
                Lbl_Peal_ORTPSA_Dist.Text = "0"; ///// Added by Sushant Jena on Dt. 26-May-2018
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
        }
    }
    #endregion

    private void FillProposalDetailsTourism()
    {
        try
        {
            SWPDashboard objSWP = new SWPDashboard();
            DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "PSE";
            string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
            objSWP.intQuarter = 0;
            objSWP.strFinacialYear = PealYear.ToString();
            objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
            objSWP.intUserid = 0;// Convert.ToInt32(Session["Userid"]);

            objSWP.intSecId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdTOURISM"]);
            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
            if (objswpDashboardList.Count > 0)
            {
                lblPealTourismApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealTourismRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealTourismApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealTourismUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealTourismQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                lblPealTourismDeferred.Text = objswpDashboardList[0].strDistDeferred;
                Lbl_Peal_ORTPSA_Tourism.Text = objswpDashboardList[0].strPealOrtpsaCrossedITandTourism; ///// Added by Sushant Jena on Dt. 26-May-2018
            }
            else
            {
                lblPealTourismApplied.Text = "0";
                lblPealTourismRejected.Text = "0";
                lblPealTourismApproved.Text = "0";
                lblPealTourismUnderEvalution.Text = "0";
                lblPealTourismDeferred.Text = "0";
                lblPealTourismQueryRaise.Text = "0";
                Lbl_Peal_ORTPSA_Tourism.Text = "0"; ///// Added by Sushant Jena on Dt. 26-May-2018
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
        }
    }
    private void FillProposalDetailsIT()
    {
        try
        {
            SWPDashboard objSWP = new SWPDashboard();
            DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "PSE";
            string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
            objSWP.intQuarter = 0;
            objSWP.strFinacialYear = PealYear.ToString();
            objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
            objSWP.intUserid = 0;//Convert.ToInt32(Session["Userid"]);
            objSWP.intSecId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdIT"]);

            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
            if (objswpDashboardList.Count > 0)
            {
                lblPealITApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealITRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealITApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealITUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealITQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                lblPealITDeferred.Text = objswpDashboardList[0].strDistDeferred;
                Lbl_Peal_ORTPSA_IT.Text = objswpDashboardList[0].strPealOrtpsaCrossedITandTourism; ///// Added by Sushant Jena on Dt. 26-May-2018
            }
            else
            {
                lblPealITApplied.Text = "0";
                lblPealITRejected.Text = "0";
                lblPealITApproved.Text = "0";
                lblPealITUnderEvalution.Text = "0";
                lblPealITDeferred.Text = "0";
                lblPealITQueryRaise.Text = "0";
                Lbl_Peal_ORTPSA_IT.Text = "0"; ///// Added by Sushant Jena on Dt. 26-May-2018
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
        }
    }

    #endregion

    #region Incentive Portlet
    #region"ADDED BY SUROJ FOR INCENTIVE PORTLET BIND"
    private void IncentiveBind()
    {
        try
        {
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "B";
            //string PealQuareter = string.IsNullOrEmpty(ddlIncentive.SelectedValue) ? default(string) : ddlIncentive.SelectedValue.ToString();
            string IncentiveDistrict = string.IsNullOrEmpty(ddlIncentiveDistrict.SelectedValue) ? default(string) : ddlIncentiveDistrict.SelectedValue.ToString();
            string IncentiveYear = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
            objSWP.intQuarter = 0;
            //objSWP.intYearId = Convert.ToInt16(IncentiveYear);
            objSWP.strFinacialYear = IncentiveYear.ToString();
            objSWP.intDistrictid = Convert.ToInt16(IncentiveDistrict);
            objSWP.intUserid = 0;// Convert.ToInt32(Session["Userid"]);
            objswpDashboardList = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
            lblIncApplied.Text = objswpDashboardList[0].INCAPLLIED;
            lblIncsanctioed.Text = objswpDashboardList[0].INCSANCTIONED;
            lblIncpending.Text = objswpDashboardList[0].INCPENDING;
            lblIncrejected.Text = objswpDashboardList[0].INCREJECTED;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
        }
    }
    #endregion
    private void IncentiveMasterBind()
    {
        try
        {
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "B";
            // string PealQuareter = string.IsNullOrEmpty(ddlIncentive.SelectedValue) ? default(string) : ddlIncentive.SelectedValue.ToString();
            //string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            //string PealYear = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
            objSWP.intQuarter = 0;
            objSWP.intYearId = 0;
            objSWP.strFinacialYear = ddlFinacialYear.SelectedValue;//Added by suroj on 26-10-17 to check finacial yr
            objSWP.intUserid = 0;// Convert.ToInt32(Session["Userid"]);
            objswpDashboardList = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
            spIncPending.InnerText = objswpDashboardList[0].INCPENDING;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
        }
    }
    protected void btnIncentiveSubmit_Click(object sender, EventArgs e)
    {
        IncentiveBind();
    }
    private void BindDistrictINCENTIVE()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlIncentiveDistrict.DataSource = objProjList;
        ddlIncentiveDistrict.DataTextField = "vchDistName";
        ddlIncentiveDistrict.DataValueField = "intDistId";
        ddlIncentiveDistrict.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlIncentiveDistrict.Items.Insert(0, list);

    }
    #endregion

    #region MEMBER FUNCTION

    private void MONTHBIND(DropDownList ddlMonth)
    {
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlMonth.Items.Add(new ListItem(monthName, month.ToString()));
        }
    }


    private void BindDistrictEmployeeMentYearwise()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();
        for (int i = 0; i < objProjList.Count; i++)
        {
            ListItem item = new ListItem();
            item.Text = objProjList[i].vchDistName;
            item.Value = objProjList[i].intDistId.ToString();
            CheckBoxList1.Items.Add(item);
        }
        foreach (ListItem lstitem in CheckBoxList1.Items)
        {
            lstitem.Selected = true;
        }
    }
    private void BindDistrictPEAL()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlPEALDistrict.DataSource = objProjList;
        ddlPEALDistrict.DataTextField = "vchDistName";
        ddlPEALDistrict.DataValueField = "intDistId";
        ddlPEALDistrict.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlPEALDistrict.Items.Insert(0, list);

    }
    private void BindDistrictInvest()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();
        for (int i = 0; i < objProjList.Count; i++)
        {
            ListItem item = new ListItem();
            item.Text = objProjList[i].vchDistName;
            item.Value = objProjList[i].intDistId.ToString();
            chkYearwise.Items.Add(item);
        }
        foreach (ListItem lstitem in chkYearwise.Items)
        {
            lstitem.Selected = true;
        }

    }
    private void YEARBIND(DropDownList ddlYear)
    {
        ddlYear.Items.Clear();
        ListItem lt = new ListItem();
        for (int i = DateTime.Now.Year; i >= 2016; i--)
        {
            lt = new ListItem();
            lt.Text = i.ToString();
            lt.Value = i.ToString();
            ddlYear.Items.Add(lt);
        }
        ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));

    }

    private void FillMasterFinYr()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();

        objSWP.strAction = "FY";
        List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

        ddlFinacialYear.DataSource = objCICGFINYear;
        ddlFinacialYear.DataTextField = "Year";
        ddlFinacialYear.DataValueField = "Year";
        ddlFinacialYear.DataBind();
        ListItem list = new ListItem();
        string year = DateTime.Now.Year.ToString();
        //ddlFinacialYear.SelectedIndex = 1;
    }
    private void FillTrackerEmployement()
    {
        List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        objSWP.strAction = "PB";
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        objSWP.strFinacialYear = ddlFinacialYear.SelectedValue;
        objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        SPEmpGen.InnerHtml = objswpDashboardList[0].strTotNoCapitalPropApproved;
    }
    private void FillTrackerInvestment()
    {
        List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        objSWP.strAction = "PB";
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        objSWP.strFinacialYear = ddlFinacialYear.SelectedValue;
        objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //decimal EmpInvestment = Convert.ToDecimal((Convert.ToDouble(objswpDashboardList[0].strTotCapitalPropApproved) / 100));
        lblmastinv.Text = objswpDashboardList[0].strTotCapitalPropApproved.ToString();
    }
    private void BindPealRecieved()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        objSWP.strAction = "RP";
        objSWP.strFinacialYear = ddlFinacialYear.SelectedValue;
        objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        lblProposalRecieved.InnerText = objswpDashboardList[0].strPealRecived;
    }
    private void BindPealApproved()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        objSWP.strAction = "RA";
        objSWP.strFinacialYear = ddlFinacialYear.SelectedValue;
        objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        lblProposalapproved.InnerText = objswpDashboardList[0].strPealApproved;
    }
    private void FillFinYrPortlet(DropDownList ddl)
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();

        objSWP.strAction = "FY";
        List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

        ddl.DataSource = objCICGFINYear;
        ddl.DataTextField = "Year";
        ddl.DataValueField = "Year";
        ddl.DataBind();
        if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 3)
        {
            ddl.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString()) - 1) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")));
        }
        else
        {
            ddl.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString())) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")) + 1);
        }
    }
    private void FillFinYrPortletS(DropDownList ddl)
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();

        objSWP.strAction = "FS";
        List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

        ddl.DataSource = objCICGFINYear;
        ddl.DataTextField = "Year";
        ddl.DataValueField = "intYearId";
        ddl.DataBind();
        //if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 3)
        //{
        //    ddl.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString()) - 1) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")));
        //}
        //else
        //{
        //    ddl.SelectedValue = (Convert.ToInt32(DateTime.Now.Year.ToString())) + "-" + (Convert.ToInt32(DateTime.Now.ToString("yy")) + 1);
        //}
    }
    string year = DateTime.Now.Year.ToString();
    #endregion

    #region AMS Portlet
    //private void AMSBIND()
    //{
    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();
    //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
    //    objSWP.strAction = "AMS";
    //    objSWP.strOption = string.IsNullOrEmpty(ddlAMS.SelectedValue) ? default(string) : ddlAMS.SelectedValue.ToString();
    //    objswpDashboardList = objserviceDashboard.GETAMS(objSWP).ToList();
    //    gvAMS.DataSource = objswpDashboardList;
    //    intRecCount = objswpDashboardList.Count;
    //    gvAMS.DataBind();
    //}

    //protected void gvAMS_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
        
    //    if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lblPendingdays = (e.Row.FindControl("Label5") as Label);
    //        Label lblNodalPersonName = (e.Row.FindControl("Label4") as Label);
    //        Label lblUnitname = (e.Row.FindControl("Label3") as Label);
    //        e.Row.Cells[0].Text = Convert.ToString((this.gvAMS.PageIndex * this.gvAMS.PageSize) + e.Row.RowIndex + 1);
           
    //        if (Convert.ToInt32(lblPendingdays.Text) <= 7)
    //        {
    //            lblPendingdays.Font.Bold = true;
    //            lblPendingdays.ForeColor= System.Drawing.Color.Green;
    //            lblNodalPersonName.Font.Bold = true;             
    //            lblUnitname.Font.Bold = true;               

    //        }
    //        else if(Convert.ToInt32(lblPendingdays.Text) <= 99)
    //        {
    //            lblPendingdays.Font.Bold = true;
    //            lblPendingdays.ForeColor = System.Drawing.Color.Orange;
    //            lblNodalPersonName.Font.Bold = true;               
    //            lblUnitname.Font.Bold = true;            
    //        }
    //        else
    //        {
    //            lblPendingdays.Font.Bold = true;
    //            lblPendingdays.ForeColor = System.Drawing.Color.Red;
    //            lblNodalPersonName.Font.Bold = true;               
    //            lblUnitname.Font.Bold = true;                
    //        }
    //    }

    //}
   
    ////private void DisplayPaging()
    //{
    //    if (gvAMS.Rows.Count > 0)
    //    {
    //        this.lblPaging.Visible = true;
    //        lbtnAll.Visible = true;
    //        if (gvAMS.PageIndex + 1 == gvAMS.PageCount)
    //        {
    //            this.lblPaging.Text = "Results <b>" + gvAMS.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
    //        }
    //        else
    //        {
    //            this.lblPaging.Text = "Results <b>" + gvAMS.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvAMS.Rows[0].Cells[0].Text) + (gvAMS.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
    //        }
    //    }
    //    else
    //    {
    //        this.lblPaging.Visible = false;
    //        lbtnAll.Visible = false;
    //    }
    //}

    //protected void lbtnAll_Click(object sender, EventArgs e)
    //{
    //    if (lbtnAll.Text == "All")
    //    {
    //        lbtnAll.Text = "Paging";
    //        gvAMS.PageIndex = 0;
    //        gvAMS.AllowPaging = false;
    //        AMSBIND();
    //    }
    //    else
    //    {
    //        lbtnAll.Text = "All";
    //        gvAMS.AllowPaging = true;
    //        AMSBIND();
    //    }
    //}
    //protected void gvAMS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvAMS.PageIndex = e.NewPageIndex;
    //    AMSBIND();
    //}

    protected void btnAmsSubmit_Click(object sender, EventArgs e)
    {
        //AMSBIND();
    }
    protected void gvAMS_PreRender(object sender, EventArgs e)
    {
       // GridDecorator.MergeRows(gvAMS);
    }
    public class GridDecorator
    {
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                if ((((HiddenField)row.FindControl("HiddenField1")).Value.Trim() == ((HiddenField)previousRow.FindControl("HiddenField1")).Value.Trim()))
                {
                    row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 :
                                          previousRow.Cells[1].RowSpan + 1;

                    previousRow.Cells[1].Visible = false;

                }
            }
        }

    }
    #endregion

    #region Event Click
    protected void ddlFinacialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["IDCO"] == "ON")
        {
            InsertAppa(ddlFinacialYear);
        }
           
        BindUnderEvalution();
        FillTrackerEmployement();
        FillTrackerInvestment();
        ViewServiceMaster();
        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            InsertCSRMaster(ddlFinacialYear);
        }
           
        string FinYear = ddlFinacialYear.SelectedItem.Text;
        string FinTearFinal = FinYear.Remove(4, 3);
        ViewSPMGMasterData();

        string strFY = DateTime.Now.Month > 3 ? DateTime.Now.Year.ToString() : Convert.ToString(DateTime.Today.Year - 1);

        if (FinTearFinal == strFY)
        {
            ViewCICGMasterData();
        }
        else
        {
            SPcicgpending.InnerText = "0";
        }

        BindPealRecieved();
        BindPealApproved();
        IncentiveMasterBind();
        BindMasterGrievanceportlet();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        FillFinYrPortlet(ddlAppaYear);
        if (ConfigurationManager.AppSettings["IDCO"] == "ON")
        {
            InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
            InsertAppa(ddlFinacialYear);
        }


        //FillFinYrPortlet(ddlYearCICG);

        if (ConfigurationManager.AppSettings["CICG"] == "ON")
        {
            InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
        }

        if (ConfigurationManager.AppSettings["CSR"] == "ON")
        {
            InsertCSRStatus(hdnCSRDistrct, ddlCSRYear);
            InsertCSRMaster(ddlFinacialYear);
            CSRPortletSatus();
        }
            
        ddlspmgyear.SelectedValue = "2017";
        //InsertSPMGStatus();
        spLastUpdate.InnerText = DateTime.Now.ToString();
        FillMasterFinYr();
        
        
           
        FillTrackerInvestment();
        FillTrackerEmployement();
        BindDistrict();
        FillFinYrPortlet(ddlPealYear);
        FillProposalDetails();
        ViewServiceStatus();
        ViewServiceMaster();
        BindDept();
        //ViewSPMGData();

        if (ConfigurationManager.AppSettings["SPMG"] == "ON")
        {
            ViewSPMGServiceStateLevelDataDirect();
            ViewSPMGServiceDistrictLevelDataDirect();
        }
            
        ViewSPMGMasterData();
        BindDistrictPEAL();
        BindDeptCICG();
        BindDistrictInvest();
        FillFinYrPortlet(ddlYearInvest);
        FillFinYrPortlet(ddlYearEmployement);
        BindDistrictEmployeeMentYearwise();
        DashboradCommon.MONTHBIND(ddlAppaMonth);
        DashboradCommon.MONTHBIND(ddlCICGMonth);
        DashboradCommon.YEARBIND(ddlYearCICG);
        FillFinYrPortlet(ddlIncentiveYear);
        BindDistrictAPAA();
        ViewCICGMasterData();
        ViewCICGData();
        
        ViewApaaStatus();
        FillProposalEmployement();
        FillProposalCapital();
        BindDistrictINCENTIVE();
        IncentiveMasterBind();
        IncentiveBind();
        BindUnderEvalution();
        //AMSBIND();
        BindPealRecieved();
        BindPealApproved();
        FillProposalDistwiseDetails();
        FillFinYrPortlet(ddlyearquery);
        FillFinYrPortlet(ddlLandFinYear);
        LandServiceBind();
        ViewQueryService(); ///added by nibedita behera on 21-12-2017
        ViewQueryServicepeal();
        ViewQueryServiceIncentive();

        FillProposalDetailsTourism();
        FillProposalDetailsIT();

        ////Grievance
        FillFinYrPortletS(ddlgyear);
        BindDistrictGrievance();
        BindMasterGrievanceportlet();
        BindGrievanceportlet();
    }
    #endregion

    #region Land Service Portlet

    private void LandServiceBind()
    {
        try
        {
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "LANDV";
            objSWP.intUserid = 0;//Convert.ToInt32(Session["Userid"]);
            objSWP.Year = ddlLandFinYear.SelectedValue;
            objswpDashboardList = objserviceDashboard.GETLandDetails(objSWP).ToList();
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
        finally
        {
            objSWP = null;
        }
    }
    protected void btnLandSubmit_Click(object sender, EventArgs e)
    {
        LandServiceBind();
    }

    #endregion

    #region Query
    private void ViewQueryService()
    {

        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            //if (Session["DeptId"].ToString() != null)
            //{
            //    if (ddldept.SelectedIndex > 0)
            //    {
            //        objDashboard.intDeptId = Convert.ToInt32(ddldept.SelectedValue);
            //    }
            //    else
            //    {
            //        objDashboard.intDeptId = objDashboardBal.GetDepartment(Session["DeptId"].ToString());

            //    }
            //}
            //else
            //{
            //    objDashboard.intDeptId = Convert.ToInt32(ddldept.SelectedValue);
            //}
            objSWP.strAction = "QV";
            // objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            //objSWP.intDeptId = 0;//suggested by girija
            objSWP.Year = ddlyearquery.SelectedValue;
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetServicesQuery(objSWP).ToList();

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
    private void ViewQueryServicepeal()
    {

        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            //objSWP.intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
            //objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objSWP.strAction = "QPV";
            objSWP.Year = ddlyearquery.SelectedValue;
            //string FinYear = ddlMonthQuery.SelectedValue;
            //string FinTearFinal = FinYear.Remove(4, 3);
            //objSWP.Year = FinYear;
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetPEALQuery(objSWP).ToList();
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
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }

    private void ViewQueryServiceIncentive()
    {

        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            //objSWP.intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
            //objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objSWP.strAction = "IQV";
            objSWP.Year = ddlyearquery.SelectedValue;

            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetiNCENTIVEQuery(objSWP).ToList();
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
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }


    protected void btnQuery_Click(object sender, EventArgs e)
    {
        ViewQueryService();
        ViewQueryServicepeal();
        ViewQueryServiceIncentive();
    }
    #endregion

    #region Added by monalisa nayak for AMS on 23-01-2018

    private void FillForwardProj()
    {
        AMS objams = new AMS();

        objams.Action = "VFPN";
        objams.OfficerId = Convert.ToInt32(Session["UserId"]);
        DataTable dt = new DataTable();
        dt = AMServices.FillForwardProject(objams);
        grdForwardProj.DataSource = dt;
        grdForwardProj.DataBind();
        if (dt.Rows.Count > 0)
        {
            lblMessage1.Visible = false;
        }
        else
        {
            lblMessage1.Visible = true;
        }
    }
    protected void grdForwardProj_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdForwardProj.PageIndex = e.NewPageIndex;
        FillForwardProj();
    }
    protected void grdForwardProj_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        AMS objams = new AMS();
        try
        {
            if (e.CommandName == "E")
            {
                objams.ProjectId = Convert.ToInt32(e.CommandArgument);
                Session["PType"] = "2";
                Response.Redirect("../AMS/ProjectMasterAdd.aspx?ID=" + objams.ProjectId + "&PIndex=" + grdForwardProj.PageIndex.ToString() + "", false);
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null; }

    }
    #endregion

    #region SPMG Portlet Database Interaction Added By Manoj Kumar Behera on 29-08-2019
    private void ViewStatelevelSPMGData()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "SP";
            objSWP.intYearId = Convert.ToInt32(ddlspmgyear.SelectedValue);
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
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
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }
    private void ViewDistrictlevelSPMGData()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "SPMGDIC";
            objSWP.intYearId = Convert.ToInt32(ddlspmgyear.SelectedValue);
            List<SWPDashboard> objServiceStatus = objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
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
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }
    private void InsertViewStatelevelSPMGData(string recived, string resolved, string pending, string pendinglast)
    {
        DataTable PnlDt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("USP_SPMG_DASHBOARD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@INTID", SqlDbType.Int).Value = Convert.ToInt32(Session["Userid"].ToString());
                    cmd.Parameters.Add("@INTYEAR", SqlDbType.Int).Value = Convert.ToInt32(ddlspmgyear.SelectedValue);
                    cmd.Parameters.Add("@INTRECIVED", SqlDbType.Int).Value = Convert.ToInt32(recived);
                    cmd.Parameters.Add("@INTRESOLVED", SqlDbType.Int).Value = Convert.ToInt32(resolved);
                    cmd.Parameters.Add("@INTPENDING", SqlDbType.Int).Value = Convert.ToInt32(pending);
                    cmd.Parameters.Add("@INTPENDING30", SqlDbType.Int).Value = Convert.ToInt32(pendinglast);
                    cmd.Parameters.Add("@VCHTYPE", SqlDbType.VarChar).Value = "1";//FOR ALL STATE LEVEL IS 1.
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }
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
                con.Close();
            }
        }
    }
    private void InsertViewDistrictlevelSPMGData(string recived, string resolved, string pending, string pendinglast)
    {
        DataTable PnlDt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("USP_SPMG_DASHBOARD"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@INTID", SqlDbType.Int).Value = Convert.ToInt32(Session["Userid"].ToString());
                    cmd.Parameters.Add("@INTYEAR", SqlDbType.Int).Value = Convert.ToInt32(ddlspmgyear.SelectedValue);
                    cmd.Parameters.Add("@INTRECIVED", SqlDbType.Int).Value = Convert.ToInt32(recived);
                    cmd.Parameters.Add("@INTRESOLVED", SqlDbType.Int).Value = Convert.ToInt32(resolved);
                    cmd.Parameters.Add("@INTPENDING", SqlDbType.Int).Value = Convert.ToInt32(pending);
                    cmd.Parameters.Add("@INTPENDING30", SqlDbType.Int).Value = Convert.ToInt32(pendinglast);
                    cmd.Parameters.Add("@VCHTYPE", SqlDbType.VarChar).Value = "2";//For ALL DIC LEVEL IS 2
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }
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
                con.Close();
            }
        }

    }

    #endregion

    private void BindDistrictGrievance()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlgdist.DataSource = objProjList;
        ddlgdist.DataTextField = "vchDistName";
        ddlgdist.DataValueField = "intDistId";
        ddlgdist.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlgdist.Items.Insert(0, list);
    }
    private void BindMasterGrievanceportlet()
    {
        try
        {
            DataTable Gdt = new DataTable();
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            objSWP.strAction = "DGD";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objSWP.strFinacialYear = ddlFinacialYear.SelectedItem.Text;
            objSWP.intDistrictId = 0;
            Gdt = objserviceDashboard.getDepartmentGrievance(objSWP);
            if (Gdt.Rows.Count > 0)
            {
                //Spangresolved.InnerText = Gdt.Rows[0]["RESOLVED"].ToString();
                Spangpending.InnerText = Gdt.Rows[0]["PENDING"].ToString();
            }
            else
            {
                //Spangresolved.InnerText = "";
                Spangpending.InnerText = "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
        }
    }
    private void BindGrievanceportlet()
    {
        try
        {
            DataTable Gdt = new DataTable();
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            objSWP.strAction = "DGD";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objSWP.strFinacialYear = ddlgyear.SelectedItem.Text;
            if (ddlgdist.SelectedIndex > 0)
            {
                objSWP.intDistrictId = Convert.ToInt32(ddlgdist.SelectedValue);
            }
            else
            {
                objSWP.intDistrictId = 0;
            }
            Gdt = objserviceDashboard.getDepartmentGrievance(objSWP);
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
        finally
        {
            objSWP = null;
        }
    }
    protected void btnGSearch_Click(object sender, EventArgs e)
    {
        BindGrievanceportlet();
    }
}
