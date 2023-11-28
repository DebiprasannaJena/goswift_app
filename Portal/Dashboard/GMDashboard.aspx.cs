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
using System.Globalization;
using System.Data.SqlClient;
using System.Web.UI;
#endregion

public partial class Portal_Dashboard_GMDashboard : SessionCheck
{
    #region Global variable
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();
    int intRecCount = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
        if (Session["desId"].ToString() == "100")
        {
            InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
           
            if (!IsPostBack)
            {
                string strDep = Session["DeptId"].ToString();
                spLastUpdate.InnerText = DateTime.Now.ToString();
                FillMasterFinYr();
                InsertCSRMaster(ddlFinacialYear);
                BindDistrict();
                FillFinYrPortlet(ddlCSRYear);
                InsertCSRStatus(hdnCSRDistrct, ddlCSRYear);
                ViewServiceMaster();
                YEARBIND(ddlserviceyear);
                MONTHBIND(ddlServcMonth);
                BindDistrictSrvc();
                ViewServiceStatus();
                BindDistrictPEAL();
                ViewCICGData();
                ViewCICGMasterData();
                FillFinYrPortlet(ddlIncentiveYear);
                //YEARBIND(ddlIncentiveYear);            
                MONTHBIND(ddlCICGMonth);
                YEARBIND(ddlYearCICG);
                //YEARBIND(ddlPealYear);
                FillFinYrPortlet(ddlPealYear);
                IncentiveMasterBind();
                CSRPortletSatus();
                BindDeptCICG();
                CurrentMonthSelect(ddlIncentive);
                //CurrentMonthSelect(ddlPealQuarter);
                FillProposalDetails();
                FillProposalDistwiseDetails();
                IncentiveBind();
                BindUnderEvalution();
                AMSBIND();
                BindPealRecieved();
                BindPealApproved();
                MONTHBIND(ddlMonthQuery);
                FillFinYrPortlet(ddlyearquery);
                ViewQueryService(); ///added by nibedita behera on 21-12-2017
                ViewQueryServicepeal();
                 ViewQueryServiceIncentive();
                 FillFinYrPortlet(ddlLandFinYear);
                 LandServiceBind();
                 //ADDED BY NIBEDITA BEHERA FOR SPECIAL SINGLW WINDOW ON 01-01-2018
                 FillProposalDetailsTourism();
                 FillProposalDetailsIT();
            }
        }
        else
        {
            Response.Redirect("~/Portal/Default.aspx");
        }
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

        //ListItem list = new ListItem();
        //list.Text = "--Select--";
        //list.Value = "0";
        //ddl.Items.Insert(0, list);

    }
    private void BindDistrictSrvc()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlServcDistrict.DataSource = objProjList;
        ddlServcDistrict.DataTextField = "vchDistName";
        ddlServcDistrict.DataValueField = "intDistId";
        ddlServcDistrict.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlServcDistrict.Items.Insert(0, list);

    }
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
    private void CurrentMonthSelect(DropDownList ddlQuarter)
    {
        decimal Month = Math.Ceiling(DateTime.Today.Month / 3m);
        ddlQuarter.SelectedValue = Month.ToString();
        //ddlYear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
    }
    protected void btnCSRStatus_Click(object sender, EventArgs e)
    {
        InsertCSRStatus(hdnCSRDistrct, ddlCSRYear);
        CSRPortletSatus();
    }
    private void CSRPortletSatus()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "VA";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            if (hdnCSRDistrct.Value == "")
            {
                objSWP.Distirict = "0";
            }
            else
            {
                objSWP.Distirict = hdnCSRDistrct.Value;
            }

            if (ddlCSRYear.SelectedItem.Text == "--Select--")
            {
                objSWP.Year = "0";
            }
            else
            {
                objSWP.Year = ddlCSRYear.SelectedItem.Text;
            }
            List<SWPDashboard> objCSRCATStatus = objserviceDashboard.GetDashCSRDtls(objSWP).ToList();
            if (objCSRCATStatus.Count > 0)
            {
                SPProject.InnerHtml = objCSRCATStatus[0].TotalProject.ToString();
                //decimal TotAmount = Convert.ToDecimal((Convert.ToDouble(objCSRCATStatus[0].TotalAmount.ToString()) / 100));
                //SPSpent.InnerHtml = TotAmount.ToString("0.##");
                //SPSpent.InnerHtml = objCSRCATStatus[0].TotalAmount.ToString();

                decimal TotAmount = Convert.ToDecimal((Convert.ToDouble(objCSRCATStatus[0].TotalAmount.ToString()) / 100));
                SPSpent.InnerHtml = Math.Round(TotAmount).ToString();
            }
            else
            {
                SPSpent.InnerHtml = "0";
                SPProject.InnerHtml = "0";
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

    //private void FillFinYr()
    //{
    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();

    //    objSWP.strAction = "FY";
    //    List<SWPDashboard> objCICGFINYear = objserviceDashboard.FillFinacialYear(objSWP).ToList();

    //    ddlCSRYear.DataSource = objCICGFINYear;
    //    ddlCSRYear.DataTextField = "Year";
    //    ddlCSRYear.DataValueField = "Year";
    //    ddlCSRYear.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    ddlCSRYear.Items.Insert(0, list);

    //}
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
        string Year = string.IsNullOrEmpty(ddlyear.SelectedItem.Text) ? default(string) : ddlyear.SelectedItem.Text;
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
                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    //decimal TotAmount = Convert.ToDecimal((Convert.ToDouble(finalOut[2].ToString()) / 100));
                    //SPNetSpent.InnerHtml = TotAmount.ToString("0.##");
                    //SPNetSpent.InnerHtml = Math.Ceiling(Convert.ToDouble(finalOut[2])).ToString();
                    //decimal TotAmount = Convert.ToDecimal((finalOut[2].ToString()));
                    //SPNetSpent.InnerHtml = Math.Round(TotAmount).ToString();
                    decimal TotAmount = Convert.ToDecimal((Convert.ToDouble(finalOut[2].ToString()) / 100));
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
                    DataTable DynTable = JsonStringToDataTable(strResult);
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
        InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
        ViewCICGData();
    }
    #endregion

    #region Dashboard SPMG Data
    //private void ViewSPMGData()
    //{

    //    objSWP = new SWPDashboard();
    //    objserviceDashboard = new DashboardBusinessLayer();
    //    try
    //    {

    //        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
    //        objSWP.strAction = "SP";
    //        objSWP.Year = ddlspmgyear.SelectedValue;
    //        List<SWPDashboard> objServiceStatus = objserviceDashboard.GetSPMGDashboardService(objSWP).ToList();
    //        if (objServiceStatus.Count > 0)
    //        {
    //            spSpmgpnd.InnerHtml = objServiceStatus[0].intSPMGPending.ToString();
    //            spmgraised.InnerHtml = objServiceStatus[0].intSPMGRaised.ToString();
    //            spmgresolved.InnerHtml = objServiceStatus[0].intSPMGResolved.ToString();
    //            spmgpending.InnerHtml = objServiceStatus[0].intSPMGPending.ToString();
    //            spmg30pending.InnerHtml = objServiceStatus[0].intSPMGIssuePending.ToString();
    //        }
    //        else
    //        {
    //            spSpmgpnd.InnerHtml = "0";
    //            spmgraised.InnerHtml = "0";
    //            spmgresolved.InnerHtml = "0";
    //            spmgpending.InnerHtml = "0";
    //            spmg30pending.InnerHtml = "0";

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        
    //    }
    //    finally
    //    {
    //        objSWP = null;
    //        objserviceDashboard = null;
    //    }
    //}
    protected void btnspmg_Click(object sender, EventArgs e)
    {
        //ViewSPMGData();
    }
    //private void BindInnerSPMG()
    //{

    //    SWPDashboard objDashboard = new SWPDashboard();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
    //        objDashboard.strAction = "SPD";
    //        objDashboard.Year = ddlspmgyear.SelectedValue;
    //        List<SWPDashboard> objServiceStatus = objDashboardBal.GetSPMGDetailService(objDashboard).ToList();
    //        grdSPMGDtl.DataSource = objServiceStatus;
    //        grdSPMGDtl.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        
    //    }
    //    finally
    //    {
    //        objDashboard = null;
    //        objDashboardBal = null;
    //    }
    //}
    #endregion

    #region Service Portlet
    private void BindService()
    {
        //ServiceBusinessLayer objService = new ServiceBusinessLayer();
        //List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        //objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
        //ddlService.DataSource = objServicelist;
        //ddlService.DataTextField = "strServiceName";
        //ddlService.DataValueField = "intServiceId";
        //ddlService.DataBind();
        //ListItem list = new ListItem();
        //list.Text = "--Select--";
        //list.Value = "0";
        //ddlService.Items.Insert(0, list);

    }
    private void BindDept()
    {
        //ServiceBusinessLayer objService = new ServiceBusinessLayer();
        //List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        //objServicelist = objService.BindDepartment("DP").ToList();
        //ddldept.DataSource = objServicelist;
        //ddldept.DataTextField = "strdeptname";
        //ddldept.DataValueField = "Deptid";
        //ddldept.DataBind();
        //ListItem list = new ListItem();
        //list.Text = "--Select--";
        //list.Value = "0";
        //ddldept.Items.Insert(0, list);
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
            objDashboard.strAction = "DGM";
            //objDashboard.intInvestorId = 0;
            objDashboard.intDistrictid = Convert.ToInt32(ddlServcDistrict.SelectedValue);
            objDashboard.intYearId = Convert.ToInt32(ddlserviceyear.SelectedValue);
            objDashboard.intMonthId = Convert.ToInt32(ddlServcMonth.SelectedValue);
            List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
            if (objServiceStatus.Count > 0)
            {

                hdApplied.InnerHtml = objServiceStatus[0].strApplied.ToString();
                hdApprove.InnerHtml = objServiceStatus[0].strApproved.ToString();
                hdPending.InnerHtml = objServiceStatus[0].strPending.ToString();
                //hdReject.InnerHtml = objServiceStatus[0].strRejected.ToString();
                //hdExceed.InnerHtml = objServiceStatus[0].intDaysPass.ToString();
                hdnqueryRaised.InnerHtml = objServiceStatus[0].QraiseTotal.ToString();
            }
            else
            {
                hdApplied.InnerHtml = "0";
                hdApprove.InnerHtml = "0";
                hdPending.InnerHtml = "0";
               // hdExceed.InnerHtml = "0";
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
    public static List<SWPDashboard> EmployeementPealDetailsBind(string Pealyear, string Pealdistrict)
    {

        SWPDashboard objDashboard = new SWPDashboard();
        List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "PED";
            objDashboard.intYearId = Convert.ToInt16(Pealyear);
            objDashboard.intDistrictid = Convert.ToInt16(Pealdistrict);
            objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        return objCSRStatus;

    }
    [WebMethod]
    public static List<SWPDashboard> EmployeementCapitalPealDetailsBind(string Pealyear, string Pealdistrict)
    {

        SWPDashboard objDashboard = new SWPDashboard();
        List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "PID";
            objDashboard.intYearId = Convert.ToInt16(Pealyear);
            objDashboard.intDistrictid = Convert.ToInt16(Pealdistrict);
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
    //private void FillProposalCapital()
    //{
    //    try
    //    {
    //        SWPDashboard objSWP = new SWPDashboard();
    //        List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
    //        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    //        objSWP.strAction = "PCI";
    //        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
    //        string PealYear = string.IsNullOrEmpty(ddlYearInvest.SelectedValue) ? default(string) : ddlYearInvest.SelectedValue.ToString();
    //        string PealDistrict = string.IsNullOrEmpty(ddlYDistrictInvest.SelectedValue) ? default(string) : ddlYDistrictInvest.SelectedValue.ToString();
    //        objSWP.intYearId = Convert.ToInt16(PealYear);
    //        objSWP.intDistrictid = Convert.ToInt16(PealDistrict);
    //        objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
    //        if (objswpDashboardList.Count > 0)
    //        {
    //            //decimal EmpInvestment = Convert.ToDecimal((Convert.ToDouble(objswpDashboardList[0].strTotCapitalPropApproved) / 100));
    //            lblEmpInvestment.Text = objswpDashboardList[0].strTotCapitalPropApproved;
    //            //lblEmpInvestment.Text = EmpInvestment.ToString("0.##");
    //        }
    //        else
    //        {
    //            lblEmpInvestment.Text = "0";
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        
    //    }
    //    finally
    //    {
    //        objSWP = null;
    //    }
    //}

    //#region"Added by suroj to view Proposal dtls"
    //private void FillProposalEmployement()
    //{
    //    try
    //    {

    //        SWPDashboard objSWP = new SWPDashboard();
    //        List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
    //        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    //        objSWP.strAction = "PD";
    //        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
    //        string PealYear = string.IsNullOrEmpty(ddlYearEmployement.SelectedValue) ? default(string) : ddlYearEmployement.SelectedValue.ToString();
    //        string PealDistrict = string.IsNullOrEmpty(ddlDistrictEmployeement.SelectedValue) ? default(string) : ddlDistrictEmployeement.SelectedValue.ToString();
    //        objSWP.intYearId = Convert.ToInt16(PealYear);
    //        objSWP.intDistrictid = Convert.ToInt16(PealDistrict);
    //        objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
    //        if (objswpDashboardList.Count > 0)
    //        {
    //            lblPealEmployeemnet.Text = objswpDashboardList[0].strTotNoCapitalPropApproved;
    //        }
    //        else
    //        {
    //            lblPealEmployeemnet.Text = "0";
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        
    //    }
    //    finally
    //    {
    //        objSWP = null;
    //    }
    //}
    //#endregion

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
                lblPealTourismDeferred.Text = objswpDashboardList[0].strDistDeferred;
                lblPealTourismQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
            }
            else
            {
                lblPealTourismApplied.Text = "0";
                lblPealTourismRejected.Text = "0";
                lblPealTourismApproved.Text = "0";
                lblPealTourismUnderEvalution.Text = "0";
                lblPealTourismDeferred.Text = "0";
                lblPealTourismQueryRaise.Text = "0";
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
                lblPealITDeferred.Text = objswpDashboardList[0].strDistDeferred;
                lblPealITQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
            }
            else
            {
                lblPealITApplied.Text = "0";
                lblPealITRejected.Text = "0";
                lblPealITApproved.Text = "0";
                lblPealITUnderEvalution.Text = "0";
                lblPealITDeferred.Text = "0";
                lblPealITQueryRaise.Text = "0";
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
   

    #region"Added by suroj to view Proposal dtls"
    private void FillProposalDetails()
    {
        try
        {
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "PB";
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            string PealQuareter = string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
            string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();
            objSWP.intType = Convert.ToInt16(PealQuareter);
            objSWP.intDistrictid = Convert.ToInt16(PealDistrict);
            objSWP.intQuarter = 0;
            objSWP.strFinacialYear = PealYear.ToString();
            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
             if (objswpDashboardList.Count > 0)
            {
            lblPealApplied.Text = objswpDashboardList[0].strApplied;
            lblPealRejected.Text = objswpDashboardList[0].strRejected;
            lblPealApproved.Text = objswpDashboardList[0].strApproved;
            //lblPealEmployeemnet.Text = objswpDashboardList[0].strTotNoCapitalPropApproved;
            decimal EmpInvestment = Convert.ToDecimal((Convert.ToDouble(objswpDashboardList[0].strTotCapitalPropApproved) / 100));
            //added by nibedita
            lblPealUnderEvalution.Text = objswpDashboardList[0].strUnderEvaltion;
            lblPealQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
            //lblEmpInvestment.Text = EmpInvestment.ToString("0.##");
            //lblmastinv.Text = EmpInvestment.ToString("0.##");
            //SPEmpGen.InnerHtml = objswpDashboardList[0].strTotNoCapitalPropApproved;
            ////lblPealCommence.Text = "0";
            lblPealDeferred.Text = objswpDashboardList[0].strDeferred;
            }
             else
             {
                 lblPealApplied.Text = "0";
                 lblPealRejected.Text = "0";
                 lblPealApproved.Text = "0";
                 lblPealUnderEvalution.Text = "0";
                 lblPealDeferred.Text = "0";
                 lblPealQueryRaise.Text = "0";
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
    //protected void btnPealEmployement_Click(object sender, EventArgs e)
    //{
    //    FillProposalEmployement();
    //}
    //protected void btnPealcapitalsubmit_Click(object sender, EventArgs e)
    //{
    //    FillProposalCapital();
    //}

    protected void btnPealsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            FillProposalDetails();
            FillProposalDistwiseDetails();
            //ADDED BY NIBEDITA BEHERA FOR SPECIAL SINGLW WINDOW ON 01-01-2018
            FillProposalDetailsTourism();
            FillProposalDetailsIT();
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

    #region MEMBER FUNCTION
    public DataTable JsonStringToDataTable(string jsonString)
    {
        DataTable dt = new DataTable();
        string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
        List<string> ColumnsName = new List<string>();
        foreach (string jSA in jsonStringArray)
        {
            string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            foreach (string ColumnsNameData in jsonStringData)
            {
                try
                {
                    int idx = ColumnsNameData.IndexOf(":");
                    string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                    if (!ColumnsName.Contains(ColumnsNameString))
                    {
                        ColumnsName.Add(ColumnsNameString);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                }
            }
            break;
        }
        foreach (string AddColumnName in ColumnsName)
        {
            dt.Columns.Add(AddColumnName);
        }
        foreach (string jSA in jsonStringArray)
        {
            string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in RowData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[RowColumns] = RowDataString;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dt.Rows.Add(nr);
        }
        return dt;
    }

    private void MONTHBIND(DropDownList ddlMonth)
    {
        for (int month = 1; month <= 12; month++)
        {
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            ddlMonth.Items.Add(new ListItem(monthName, month.ToString()));
        }
    }
    
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
    private void YEARBIND(DropDownList ddlYear)
    {
        ddlYear.Items.Clear();
        ListItem lt = new ListItem();
        for (int i = DateTime.Now.Year; i >= 2007; i--)
        {
            lt = new ListItem();
            lt.Text = i.ToString();
            lt.Value = i.ToString();
            ddlYear.Items.Add(lt);
        }
        ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    //private void InsertAppa(DropDownList ddlYear)
    //{
    //    string finalquery = string.Empty;
    //    SqlCommand cmd = null;
    //    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    //    string Type = string.Empty;
    //    SWPDashboard objSWP = new SWPDashboard();
    //    string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
    //    inputJson = inputJson.TrimStart('[').TrimEnd(']');
    //    //string District = string.IsNullOrEmpty(ddlAPAADistrict.SelectedValue) ? default(string) : ddlAPAADistrict.SelectedValue.ToString();
    //    //string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
    //    string Year = string.IsNullOrEmpty(ddlYear.SelectedValue) ? default(string) : ddlYear.SelectedValue.ToString();
    //    string Deptid = "0";
    //    //string UniqueKey = Session["UID"].ToString();
    //    string UniqueKey = "0";
    //    List<SWPDashboard> objlist = new List<SWPDashboard>();
    //    objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
    //    objlist = objserviceDashboard.CheckAppastatus(objSWP);
    //    if (objlist.Count > 0)
    //    {
    //        if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
    //        {
    //            Type = "0";
    //        }
    //        else if (objlist[0].intStatus != 1 && objlist[0].intDistrictid == 0)//IPICOL Largescale
    //        {
    //            Type = "1";
    //        }
    //        else//MSME
    //        {
    //            Type = "2";
    //        }
    //    }
    //    try
    //    {
    //        string serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + "0" + "/" + Deptid + "/" + Type + "/" + Year + "/" + "0";
    //        HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
    //        httpRequest.Accept = "application/json";
    //        httpRequest.ContentType = "application/json";
    //        httpRequest.Method = "GET";
    //        using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
    //        {
    //            using (Stream stream = httpResponse.GetResponseStream())
    //            {
    //                string strResult = (new StreamReader(stream)).ReadToEnd();
    //                DataTable DynTable = JsonStringToDataTable(strResult);
    //                string output = DynTable.Rows[0]["getSWPConsolidationDataResult"].ToString();
    //                string[] finalOut = output.Split(':');
    //                spAPAAPending.te = DynTable.Rows[0]["TotalPendingUnit"].ToString();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        spAPAAPending.InnerText = "0";
    //        Util.LogError(ex, "Dashboard");
    //    }
    //}
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
   
    #endregion

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
    #region"ADDED BY SUROJ FOR INCENTIVE PORTLET BIND"
    private void IncentiveBind()
    {
        try
        {
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "B";
            string PealQuareter = string.IsNullOrEmpty(ddlIncentive.SelectedValue) ? default(string) : ddlIncentive.SelectedValue.ToString();
            //string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
            string IncentiveYear = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
            objSWP.intQuarter = Convert.ToInt32(PealQuareter);
            objSWP.strFinacialYear = IncentiveYear.ToString();

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
            lblIncpendingdtls.Text = objswpDashboardList[0].INCPENDING;

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

    protected void btnIncentiveSubmit_Click(object sender, EventArgs e)
    {
        IncentiveBind();
    }

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
    protected void ddlFinacialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        IncentiveMasterBind();
        BindUnderEvalution();
        ViewServiceMaster();
        InsertCSRMaster(ddlFinacialYear);
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
        BindPealRecieved();
        BindPealApproved();
    }
    private void AMSBIND()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        objSWP.strAction = "AMS";
        objSWP.strOption = string.IsNullOrEmpty(ddlAMS.SelectedValue) ? default(string) : ddlAMS.SelectedValue.ToString();
        objswpDashboardList = objserviceDashboard.GETAMS(objSWP).ToList();
        gvAMS.DataSource = objswpDashboardList;
        intRecCount = objswpDashboardList.Count;
        gvAMS.DataBind();
    }

    protected void gvAMS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvAMS.PageIndex * this.gvAMS.PageSize) + e.Row.RowIndex + 1);
        }

    }

    //private void DisplayPaging()
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
    protected void gvAMS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAMS.PageIndex = e.NewPageIndex;
        AMSBIND();
    }

    protected void btnAmsSubmit_Click(object sender, EventArgs e)
    {
        AMSBIND();
    }
    protected void gvAMS_PreRender(object sender, EventArgs e)
    {
        GridDecorator.MergeRows(gvAMS);
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


                    row.Cells[1].Style.Add("vertical-align", "middle");
                    previousRow.Cells[1].Visible = false;

                }
            }
        }
    }

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
    protected void LinkButton1_Click(object sender, EventArgs e)
    {

        string strDep = Session["DeptId"].ToString();
        spLastUpdate.InnerText = DateTime.Now.ToString();
        FillMasterFinYr();
        InsertCSRMaster(ddlFinacialYear);
        BindDistrict();
        FillFinYrPortlet(ddlCSRYear);
        InsertCSRStatus(hdnCSRDistrct, ddlCSRYear);
        ViewServiceMaster();
        YEARBIND(ddlserviceyear);
        MONTHBIND(ddlServcMonth);
        BindDistrictSrvc();
        ViewServiceStatus();
        BindDistrictPEAL();
        ViewCICGData();
        ViewCICGMasterData();
        FillFinYrPortlet(ddlIncentiveYear);
        //YEARBIND(ddlIncentiveYear);            
        MONTHBIND(ddlCICGMonth);
        YEARBIND(ddlYearCICG);
        //YEARBIND(ddlPealYear);
        FillFinYrPortlet(ddlPealYear);
        IncentiveMasterBind();
        CSRPortletSatus();
        BindDeptCICG();
        CurrentMonthSelect(ddlIncentive);
        CurrentMonthSelect(ddlPealQuarter);
        FillProposalDetails();
        FillProposalDistwiseDetails();
        IncentiveBind();
        BindUnderEvalution();
        AMSBIND();
        BindPealRecieved();
        BindPealApproved();
        MONTHBIND(ddlMonthQuery);
        ViewQueryService(); ///added by nibedita behera on 21-12-2017
        ViewQueryServicepeal();
        ViewQueryServiceIncentive();
        LandServiceBind();

        //ADDED BY NIBEDITA BEHERA FOR SPECIAL SINGLW WINDOW ON 01-01-2018
        FillProposalDetailsTourism();
        FillProposalDetailsIT();
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
            objSWP.intQuarter = 0;
            //objSWP.intYearId = Convert.ToInt16(PealYear);
            objSWP.strFinacialYear = PealYear.ToString();
            objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
            if (objswpDashboardList.Count > 0)
            {
                lblPealdistApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealdistRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealdistApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealdistUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealdistDeferred.Text = objswpDashboardList[0].strDistDeferred;
                lblPealdistQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                //lblPealDeferred.Text = "0";
            }
            else
            {

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
            objSWP.intDeptId = 1;
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
}

