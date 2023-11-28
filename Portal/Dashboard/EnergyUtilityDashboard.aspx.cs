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

public partial class Portal_Dashboard_EnergyUtilityDashboard : System.Web.UI.Page
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
            UserId = Convert.ToString(Session["UserId"]);
        }
        if (!IsPostBack)
        {

            try
            {

                spLastUpdate.InnerText = DateTime.Now.ToString();

                /*-----------------------------------------------------------------*/
                ///Fill Dropdownlist for Financial Year
                /*-----------------------------------------------------------------*/
                commonMethodobj.FillFinancialYear(ddlFinacialYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on master tracker
                commonMethodobj.FillFinancialYear(ddlsyear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Deparment Wise Approval
                commonMethodobj.FillFinancialYear(ddlyearquery); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Query 
                /*-----------------------------------------------------------------*/
                //// Master Tracker
                /*-----------------------------------------------------------------*/

                commonMethodobj.FillDepartment(ddldept); // bind Department record in dropdown list
                int intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
                ddldept.SelectedValue = Convert.ToString(intDeptId);
                ddldept.Enabled = false;
                commonMethodobj.FillServiceEnergy(ddldept, ddlService);// bind service record in  dropdown list

                ViewServiceMaster();  //Pending Approvals

                /*-----------------------------------------------------------------*/
                ////Portlet Section
                /*-----------------------------------------------------------------*/

                //commonMethodobj.FillDepartment(ddldept); // bind Department record in dropdown list

                ViewServiceStatus();//DEPARTMENT WISE APPROVALS



                ViewQueryService(); //Query on Normal Portlet 



            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Dashboard");
            }




           // BindDept();

                //FillFinYrPortletS(ddlspmgyear);

                // BindService();
                //if (intDeptId == idco || intDeptId == idcoW)
                //{
                //dvapaa.Style.Add("display", "block");
                //dvAPAAMast.Style.Add("display", "block");

                //FillFinYrPortlet(ddlAppaYear);
                // }
            //else if (intDeptId == LabDirectorate || intDeptId == OSPCB || intDeptId == FactBoil || intDeptId == LegalMetrology)
            //{
                //dvCICG.Style.Add("display", "block");
                //dvCICGMast.Style.Add("display", "block");

                //ddldeptCIF.SelectedValue = Convert.ToString(objserviceDashboard.GetCIFDepartmentid(Session["DeptId"].ToString()));
                //ddldeptCIF.Enabled = false;
            //}
          //  spLastUpdate.InnerText = DateTime.Now.ToString();
           // FillMasterFinYr();

           // string strDep = Session["DeptId"].ToString();
           // FillFinYrPortlet(ddlsyear);
           // ViewServiceStatus();
           // ViewServiceMaster();
           // FillFinYrPortlet(ddlyearquery);            
           // ViewQueryService(); //Query on Normal Portlet 
           // FillFinYrPortletS(ddlspmgyear);
            //ViewSPMGServiceDistrictLevelDataDirect();
           // ViewSPMGMasterData();
        }
    }
    #endregion

    #region Query Portlet

    private void ViewQueryService()  //Query on Normal Portlet 
    {
        //objSWP = new SWPDashboard();
        //objserviceDashboard = new DashboardBusinessLayer();
        //try
        //{
        //    objSWP.strAction = "EQV";
        //    objSWP.Year = ddlyearquery.SelectedValue;
        //    objSWP.intDeptId = Convert.ToInt32(Session["DeptId"]);// objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
        //    List<SWPDashboard> objServiceStatus = objserviceDashboard.GetServicesQuery(objSWP).ToList();
        //    //hdnsrvc.Value = Convert.ToString(objserviceDashboard.GetDepartment(Session["DeptId"].ToString()));
        //    hdnsrvc.Value = Convert.ToString(objSWP.intDeptId);
        //    if (objServiceStatus.Count > 0)
        //    {
        //        spRaised.InnerText = objServiceStatus[0].strTotalQueryRaised.ToString();
        //        spRevert.InnerText = objServiceStatus[0].strTotalQueryResponse.ToString();
        //        spPending.InnerText = objServiceStatus[0].strTotalQueryPending.ToString();
        //        spResponseTimeline.InnerText = objServiceStatus[0].strTotQuerynotRecTimeline.ToString();
        //        spAvgTime.InnerText = objServiceStatus[0].strAvgRaiseQuery.ToString();
        //    }
        //    else
        //    {
        //        spRaised.InnerText = "0";
        //        spRevert.InnerText = "0";
        //        spPending.InnerText = "0";
        //        spResponseTimeline.InnerText = "0";
        //        spAvgTime.InnerText = "0";
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


        try
        {
            hdnsrvc.Value = Convert.ToString(Session["DeptId"]);
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.ViewQueryService(ddlyearquery.SelectedValue, Convert.ToString(Session["DeptId"]));
            if (objswpDashboardList.Count > 0)
            {
                spRaised.InnerText = objswpDashboardList[0].strTotalQueryRaised.ToString();
                spRevert.InnerText = objswpDashboardList[0].strTotalQueryResponse.ToString();
                spPending.InnerText = objswpDashboardList[0].strTotalQueryPending.ToString();
                spResponseTimeline.InnerText = objswpDashboardList[0].strTotQuerynotRecTimeline.ToString();
                spAvgTime.InnerText = objswpDashboardList[0].strAvgRaiseQuery.ToString();
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
        catch(Exception ex)
        {
            throw ex;
        }
       
    }

    #endregion

    #region Service Portlet

    //private void BindService()
    //{
    //    //ServiceBusinessLayer objService = new ServiceBusinessLayer();
    //    //List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    //    //objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
    //    //ddlService.DataSource = objServicelist;
    //    //ddlService.DataTextField = "strServiceName";
    //    //ddlService.DataValueField = "intServiceId";
    //    //ddlService.DataBind();
    //    //ListItem list = new ListItem();
    //    //list.Text = "--Select--";
    //    //list.Value = "0";
    //    //ddlService.Items.Insert(0, list);

    //    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    //    List<ServiceDetails> objServicelist = new List<ServiceDetails>();

    //    objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
    //    List<ServiceDetails> tlistFiltered = objServicelist.Where(item => item.intServiceId == 16).ToList();

    //    ddlService.DataSource = tlistFiltered;
    //    ddlService.DataTextField = "strServiceName";
    //    ddlService.DataValueField = "intServiceId";
    //    ddlService.DataBind();
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
        try
        {
            ViewServiceStatus();
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        
    }
    private void ViewServiceStatus() //view Department Wise Approvals on normal platfrom
    {
        //SWPDashboard objDashboard = new SWPDashboard();
        //DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        //try
        //{
        //    objDashboard.strAction = "EUC";

        //    //if (Session["DeptId"].ToString() != null)
        //    //{
        //    //    if (ddldept.SelectedIndex > 0)
        //    //    {
        //    //        objDashboard.intDeptId = Convert.ToInt32(ddldept.SelectedValue);
        //    //    }
        //    //    else
        //    //    {
        //    //        objDashboard.intDeptId = objDashboardBal.GetDepartment(Session["DeptId"].ToString());
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    objDashboard.intDeptId = Convert.ToInt32(ddldept.SelectedValue);
        //    //}


        //    objDashboard.intDeptId = Convert.ToInt32(Session["DeptId"]);

        //    hdnsrvc.Value = objDashboard.intDeptId.ToString();
        //    objDashboard.intServiceId = Convert.ToInt32(ddlService.SelectedValue);

        //    objDashboard.strFinacialYear = ddlsyear.SelectedValue; //Added By Manoj Kumar Behera

        //    List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
        //    if (objServiceStatus.Count > 0)
        //    {
        //        hdApplied.InnerHtml = objServiceStatus[0].strApplied.ToString();
        //        hdApprove.InnerHtml = objServiceStatus[0].strApproved.ToString();
        //        hdPending.InnerHtml = objServiceStatus[0].strPending.ToString();
        //        hdReject.InnerHtml = objServiceStatus[0].strRejected.ToString();
        //        hdExceed.InnerHtml = objServiceStatus[0].intDaysPass.ToString();
        //        hdnqueryRaised.InnerHtml = objServiceStatus[0].QraiseTotal.ToString();
        //    }
        //    else
        //    {
        //        hdApplied.InnerHtml = "0";
        //        hdApprove.InnerHtml = "0";
        //        hdPending.InnerHtml = "0";
        //        hdExceed.InnerHtml = "0";
        //        hdnqueryRaised.InnerHtml = "0";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "Dashboard");
        //}
        //finally
        //{
        //    objDashboard = null;
        //    objDashboardBal = null;
        //}




        try
        {

            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.ViewServiceStatus(Convert.ToInt32(Session["DeptId"]), Convert.ToInt32(ddlService.SelectedValue), ddlsyear.SelectedValue);
            if (objswpDashboardList.Count > 0)
            {
                hdApplied.InnerHtml = objswpDashboardList[0].strApplied.ToString();
                hdApprove.InnerHtml = objswpDashboardList[0].strApproved.ToString();
                hdPending.InnerHtml = objswpDashboardList[0].strPending.ToString();
                hdReject.InnerHtml = objswpDashboardList[0].strRejected.ToString();
                hdExceed.InnerHtml = objswpDashboardList[0].intDaysPass.ToString();
                hdnqueryRaised.InnerHtml = objswpDashboardList[0].QraiseTotal.ToString();
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
        catch(Exception ex)
        {
            throw ex;
        }

    }
    private void ViewServiceMaster()  // Method for  bind Pending approval  on master tracker
    {
        //SWPDashboard objDashboard = new SWPDashboard();
        //DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();       
        //objDashboard.strAction = "MEUC";      
        //objDashboard.intDeptId = Convert.ToInt32(Session["DeptId"]);
        //objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
        //objDashboard.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
        //objDashboard.strFinacialYear = ddlFinacialYear.SelectedValue;
        //List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();      
        //if (objServiceStatus.Count > 0)
        //{
        //    spanapprove.InnerText = objServiceStatus[0].strPending.ToString();          
        //}



        try
        {
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objswpDashboardList = commonMethodobj.ViewServiceMasterDept(Convert.ToInt32(Session["DeptId"]), Convert.ToInt32(UserId) , Convert.ToInt32(ddlService.SelectedValue), ddlFinacialYear.SelectedValue);
            if (objswpDashboardList.Count > 0)
            {
                spanapprove.InnerText = objswpDashboardList[0].strPending.ToString();
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

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

        ViewServiceMaster();
        //string FinYear = ddlFinacialYear.SelectedItem.Text;
        //string FinTearFinal = FinYear.Remove(4, 3);
        //string strFY = DateTime.Now.Month > 3 ? DateTime.Now.Year.ToString() : Convert.ToString(DateTime.Today.Year - 1);

        //if (FinTearFinal == strFY)
        //{
        //    ViewCICGMasterData();
        //}
        //else
        //{
        //    SPcicgpending.InnerText = "0";
        //}
       // ViewSPMGMasterData();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        //// BindDept();
        // int intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
        // ddldept.SelectedValue = Convert.ToString(objserviceDashboard.GetDepartment(Session["DeptId"].ToString()));
        // ddldept.Enabled = false;
        // if (intDeptId == idco || intDeptId == idcoW)
        // {
        //     //dvapaa.Style.Add("display", "block");
        //     //dvAPAAMast.Style.Add("display", "block");
        //     //FillFinYrPortlet(ddlAppaYear);
        //     ////InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);

        // }
        // else if (intDeptId == LabDirectorate || intDeptId == OSPCB || intDeptId == FactBoil || intDeptId == LegalMetrology)
        // {
        //     //dvCICG.Style.Add("display", "block");
        //     //dvCICGMast.Style.Add("display", "block");
        //     //InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
        //     //ddldeptCIF.SelectedValue = objserviceDashboard.GetCIFDepartmentid(Session["DeptId"].ToString());
        //     //ddldeptCIF.Enabled = false;
        // }

        // spLastUpdate.InnerText = DateTime.Now.ToString();
        //// FillMasterFinYr();

        // string strDep = Session["DeptId"].ToString();
        // ViewServiceStatus();
        // ViewServiceMaster();
        // YEARBIND(ddlyearquery);
        // ViewQueryService();
        // //ViewSPMGServiceDistrictLevelDataDirect();
        //// ViewSPMGMasterData();
        ///

        try
        {
            spLastUpdate.InnerText = DateTime.Now.ToString();

            /*-----------------------------------------------------------------*/
            ///Fill Dropdownlist for Financial Year
            /*-----------------------------------------------------------------*/
            commonMethodobj.FillFinancialYear(ddlFinacialYear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on master tracker
            commonMethodobj.FillFinancialYear(ddlsyear); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Deparment Wise Approval
            commonMethodobj.FillFinancialYear(ddlyearquery); //bind both value 2016-17 and  text 2016-17  FinalcealYear on Query 
            /*-----------------------------------------------------------------*/
            //// Master Tracker
            /*-----------------------------------------------------------------*/

            commonMethodobj.FillDepartment(ddldept); // bind Department record in dropdown list
            int intDeptId = objserviceDashboard.GetDepartment(Session["DeptId"].ToString());
            ddldept.SelectedValue = Convert.ToString(intDeptId);
            ddldept.Enabled = false;
            commonMethodobj.FillServiceEnergy(ddldept, ddlService);// bind service record in  dropdown list

            ViewServiceMaster();  //Pending Approvals

            /*-----------------------------------------------------------------*/
            ////Portlet Section
            /*-----------------------------------------------------------------*/

            //commonMethodobj.FillDepartment(ddldept); // bind Department record in dropdown list

            ViewServiceStatus();//DEPARTMENT WISE APPROVALS



            ViewQueryService(); //Query on Normal Portlet 

        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }

    }
    #endregion

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

    protected void Button10_Click(object sender, EventArgs e)
    {
        try
        {
            ViewQueryService();
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        
    }

    #region SPMG Portlet Added By Manoj Kumar Behera On 30/08/2019

    //public void ViewSPMGServiceDistrictLevelDataDirect()
    //{
    //    try
    //    {
    //        objserviceDashboard = new DashboardBusinessLayer();
    //        string strrandomgen = MakeRandom(10);
    //        var plainran = Encoding.UTF8.GetBytes(strrandomgen);
    //        string randno = Convert.ToBase64String(plainran);
    //        //Timestamp
    //        TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
    //        double unixTime = span.TotalSeconds;
    //        var plainut = Encoding.UTF8.GetBytes(unixTime.ToString());
    //        string plunixtime = Convert.ToBase64String(plainut);

    //        //PasswordDigest
    //        string ranpss = strrandomgen + "$CSDa2017@shbo@rD$Ipic)L" + unixTime.ToString();
    //        SHA256 mySHA256 = SHA256Managed.Create();
    //        string finalstr = GetSha256FromString(ranpss);
    //        byte[] bytes = Encoding.UTF8.GetBytes(finalstr);
    //        string ranpss1 = Convert.ToBase64String(bytes);

    //        //Financial year
    //        string FinYear = ddlspmgyear.SelectedValue;

    //        string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
    //        string strdeptid = objserviceDashboard.GetSPMGDepartment(dept).ToString();

    //        string serviceUrl = "https://esuvidha.gov.in/odishadi/restservices/RestServer.php?view=issuestatusbydepartmentid";

    //        object input = new
    //        {
    //            RandomNonce = randno,
    //            TimeStamp = plunixtime,
    //            PasswordDigest = ranpss1,
    //            FinancialYear = FinYear,
    //            DepartmentID = strdeptid
    //        };
    //        string inputJson = (new JavaScriptSerializer()).Serialize(input);
    //        var webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
    //        webRequest.Method = WebRequestMethods.Http.Post;
    //        webRequest.ContentType = "application/json";
    //        try
    //        {
    //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
    //            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
    //            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);
    //        }
    //        catch
    //        {
    //        }
    //        var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);
    //        using (var requestStream = webRequest.GetRequestStream())
    //        {
    //            using (var writer = new StreamWriter(requestStream))
    //            {
    //                writer.Write(json);
    //            }
    //        }

    //        using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
    //        {
    //            using (var responseStream = webResponse.GetResponseStream())
    //            {
    //                using (var reader = new StreamReader(responseStream))
    //                {
    //                    try
    //                    {
    //                        var responseData = reader.ReadToEnd();
    //                        webResponse.Close();

    //                        string strResult = responseData.ToString();
    //                        if (strResult != "")
    //                        {
    //                            DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
    //                            if (DynTable.Rows.Count > 0)
    //                            {
    //                                InsertViewDistrictlevelSPMGData(DynTable.Rows[0]["Issues Received"].ToString(), DynTable.Rows[0]["Issues Resolved"].ToString(),
    //                                DynTable.Rows[0]["Issues Pending"].ToString(), DynTable.Rows[0]["Issues Pending (more than 30 days)"].ToString(), strdeptid);
    //                            }
    //                            else
    //                            {
    //                                spmgraised.InnerHtml = "0";
    //                                spmgresolved.InnerHtml = "0";
    //                                spmgpending.InnerHtml = "0";
    //                                spmg30pending.InnerHtml = "0";
    //                            }
    //                        }
    //                        else
    //                        {
    //                            ViewDistrictlevelSPMGData();
    //                        }
    //                    }
    //                    catch (WebException ex)
    //                    {
    //                        Util.LogError(ex, "Dashboard");
    //                        ViewDistrictlevelSPMGData();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (WebException ex)
    //    {
    //        Util.LogError(ex, "Dashboard");
    //        ViewDistrictlevelSPMGData();
    //    }
    //}
    //private void InsertViewDistrictlevelSPMGData(string recived, string resolved, string pending, string pendinglast, string strdeptid)
    //{
    //    DataTable PnlDt = new DataTable();
    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        try
    //        {
    //            using (SqlCommand cmd = new SqlCommand("USP_SPMG_DASHBOARD"))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;
    //                cmd.Parameters.Add("@INTID", SqlDbType.Int).Value = Convert.ToInt32(Session["Userid"].ToString());
    //                cmd.Parameters.Add("@INTYEAR", SqlDbType.Int).Value = Convert.ToInt32(ddlspmgyear.SelectedValue);
    //                cmd.Parameters.Add("@INTRECIVED", SqlDbType.Int).Value = Convert.ToInt32(recived);
    //                cmd.Parameters.Add("@INTRESOLVED", SqlDbType.Int).Value = Convert.ToInt32(resolved);
    //                cmd.Parameters.Add("@INTPENDING", SqlDbType.Int).Value = Convert.ToInt32(pending);
    //                cmd.Parameters.Add("@INTPENDING30", SqlDbType.Int).Value = Convert.ToInt32(pendinglast);
    //                cmd.Parameters.Add("@INTDEPT", SqlDbType.Int).Value = Convert.ToInt32(strdeptid);
    //                cmd.Parameters.Add("@VCHTYPE", SqlDbType.VarChar).Value = "2D";//For ALL DIC LEVEL IS 2
    //                cmd.Connection = con;
    //                con.Open();
    //                PnlDt.Load(cmd.ExecuteReader());
    //                con.Close();
    //            }
    //            if (PnlDt.Rows.Count > 0)
    //            {
    //                spmgraised.InnerHtml = PnlDt.Rows[0]["RAISED"].ToString();
    //                spmgresolved.InnerHtml = PnlDt.Rows[0]["RESOLVED"].ToString();
    //                spmgpending.InnerHtml = PnlDt.Rows[0]["PENDING"].ToString();
    //                spmg30pending.InnerHtml = PnlDt.Rows[0]["ISSUEPENDING"].ToString();
    //            }
    //            else
    //            {
    //                spmgraised.InnerHtml = "0";
    //                spmgresolved.InnerHtml = "0";
    //                spmgpending.InnerHtml = "0";
    //                spmg30pending.InnerHtml = "0";
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            con.Close();
    //        }
    //    }
    //}
    //private void ViewDistrictlevelSPMGData()
    //{
    //    SWPDashboard objDashboard = new SWPDashboard();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    objserviceDashboard = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "DD";
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
    //            spmg30pending.InnerHtml = objServiceStatus[0].intDaysPass.ToString();
    //        }
    //        else
    //        {
    //            spmgraised.InnerHtml = "0";
    //            spmgresolved.InnerHtml = "0";
    //            spmgpending.InnerHtml = "0";
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
    //        objserviceDashboard = null;
    //    }
    //}

    //public void ViewSPMGMasterData()
    //{
    //    SWPDashboard objDashboard = new SWPDashboard();
    //    DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
    //    objserviceDashboard = new DashboardBusinessLayer();
    //    try
    //    {
    //        objDashboard.strAction = "DALL";
    //        string dept = objserviceDashboard.GetDepartment(Session["DeptId"].ToString()).ToString();
    //        string strdeptid = objserviceDashboard.GetSPMGDepartment(dept).ToString();
    //        string Finyear = ddlFinacialYear.SelectedValue;
    //        objDashboard.intYearId = Convert.ToInt32(Finyear.ToString().Split('-')[0]);
    //        objDashboard.intDeptId = Convert.ToInt32(strdeptid);
    //        List<SWPDashboard> objServiceStatus = objDashboardBal.GetSPMGdepartmentwiseStatus(objDashboard).ToList();
    //        if (objServiceStatus.Count > 0)
    //        {
    //            spSpmgpnd.InnerText = objServiceStatus[0].strPending.ToString();
    //        }
    //        else
    //        {
    //            spSpmgpnd.InnerText = "0";
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
    //        objserviceDashboard = null;
    //    }
    //}
    protected void btnspmg_Click(object sender, EventArgs e)
    {
       // ViewSPMGServiceDistrictLevelDataDirect();
    }
    #endregion SPMG Portlet Added By Manoj Kumar Behera On 30/08/2019
}

