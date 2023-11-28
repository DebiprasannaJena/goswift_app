/*
 * Created By : Manoj Kumar Behera
 * Created On : 04 Jun 2019
 * File Name  :  EventLog_Exception_Report.aspx.cs
 * Class Name : EventLog_Exception_Report
 * Description: Event Log and Exception Reports
 */


using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using BusinessLogicLayer.Service;
using EntityLayer.Proposal;
using EntityLayer.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class EventLog_Exception_Report : System.Web.UI.Page
{    

    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();
    DataTable dt = new DataTable();
    StringBuilder sb = new StringBuilder();

    int intUserId = 0;
    int intDeptId = 0;
    int intDistrictId = 0;
    int intDesignationId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("../SessionTimeout.aspx");
        }
        if (!IsPostBack)
        {
            BindUser();
            FillFinYrPortletS(ddlspmgyear);
            FillFinYrPortletS(ddlCSRYear);
            BindDistrict();
            DashboradCommon.MONTHBIND(ddlCICGMonth);
            DashboradCommon.YEARBIND(ddlYearCICG);
            BindDeptCICG();
            FillFinYrPortlet(ddlPealYear);
            FillFinYrPortlet(ddlpealdicyear);
            BindDistrictPEAL();
            BindService();
            BindDept();
            FillFinYrPortlet(ddlserviceyear);
            DashboradCommon.MONTHBIND(ddlServcMonth);
            FillFinYrPortlet(ddlIncentiveYear);
            FillFinYrPortlet(ddldicincentiveyear);
            BindDistrictINCENTIVE();
            FillFinYrPortlet(ddlLandFinYear);
            FillFinYrPortlet(ddldicLandFinYear);
            FillFinYrPortlet(ddlAppaYear);
            FillFinYrPortlet(ddldicAppaYear);
            BindDistrictAPAA();
            BindDicDistrictAPAA();
            DashboradCommon.MONTHBIND(ddlAppaMonth);
            DashboradCommon.MONTHBIND(ddldicAppaMonth);
        }
    }
    private void BindDicDistrictAPAA()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objService = new ProposalBAL();
        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();
        ddldicAPAADistrict.DataSource = objProjList;
        ddldicAPAADistrict.DataTextField = "vchDistName";
        ddldicAPAADistrict.DataValueField = "intDistId";
        ddldicAPAADistrict.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddldicAPAADistrict.Items.Insert(0, list);
    }
    private void BindDistrictAPAA()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objService = new ProposalBAL();
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
    private void BindDistrictINCENTIVE()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objService = new ProposalBAL();
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
    private void BindDistrictPEAL()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objService = new ProposalBAL();
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
    private void BindDistrict()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objService = new ProposalBAL();
        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();
        ddlcsrdistrict.DataSource = objProjList;
        ddlcsrdistrict.DataTextField = "vchDistName";
        ddlcsrdistrict.DataValueField = "intDistId";
        ddlcsrdistrict.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlcsrdistrict.Items.Insert(0, list);
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
    private void BindUser()
    {
        try
        {
            MisReportServiceProvider objService = new MisReportServiceProvider();            
            IndustrySearch objProp = new IndustrySearch();
            dt.Reset();
            objProp.strActionCode= "USER";
            dt = objService.FillDeptUserName(objProp);
            drpuser.DataSource = dt;
            drpuser.DataTextField = "vchUserName";
            drpuser.DataValueField = "intUserId";
            drpuser.DataBind();
            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            drpuser.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpuser.SelectedIndex <= 0)
            {
                string str = "jAlert('<strong>Please Select User</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
                return;
            }
            else 
            {
                MisReportServiceProvider objService = new MisReportServiceProvider();
                IndustrySearch objProp = new IndustrySearch();
                dt.Reset();
                objProp.strActionCode = "USERDETAIL";
                objProp.INT_USER_ID = Convert.ToInt32(drpuser.SelectedValue);
                dt = objService.FillUserDetails(objProp);
                if (dt.Rows.Count > 0)
                {
                    intUserId = Convert.ToInt32(dt.Rows[0]["intUserId"]);
                    intDesignationId = Convert.ToInt32(dt.Rows[0]["intDesignationId"]);
                    intDeptId = Convert.ToInt32(dt.Rows[0]["intLevelDetailId"]);
                    intDistrictId = Convert.ToInt32(dt.Rows[0]["intDistrict"]);

                    Session["EventUserid"] = intUserId.ToString();
                    Session["EventDesignationid"] = intDesignationId.ToString();
                    Session["EventDeptid"] = intDeptId.ToString();
                    Session["EventDistrictid"] = intDistrictId.ToString();

                    if (intDesignationId == 94) //// CM Odisha
                    {
                        #region CM Odisha

                        SWASPANEL.Visible = true;
                        SWASDICPANEL.Visible = false;
                        LADPANEL.Visible = true;                        
                        DICLADPANEL.Visible = false;
                        CSRPANEL.Visible = true;
                        APPPANEL.Visible = true;
                        DICAPPPANEL.Visible = false;
                        CIFPANEL.Visible = true;
                        SPMGPANEL.Visible = true;
                        INCENTIVEPANEL.Visible = true;
                        DICINCENTIVEPANEL.Visible = false;
                        DWAPANEL.Visible = true;
                        DWADICPANEL.Visible = false;
                        

                        FillProposalDetails();
                        FillProposalDistwiseDetails();
                        FillProposalDetailsTourism();
                        FillProposalDetailsIT();
                        LandServiceBind();

                        CSRPortletSatus();
                        ViewApaaStatus();
                        ViewCICGData();
                        BindSPMGData();
                        IncentiveBind();

                       // /////// Department Wise Approval
                       ViewServiceStatus();                       

                        #endregion
                    }
                    else if (intDesignationId == 95 || intDesignationId == 124) //// Chief Secretary Odisha and Development Commissioner
                    {

                        #region CS Odisha

                        SWASPANEL.Visible = true;
                        SWASDICPANEL.Visible = false;
                        LADPANEL.Visible = true;
                        DICLADPANEL.Visible = false;
                        CSRPANEL.Visible = true;
                        APPPANEL.Visible = true;
                        DICAPPPANEL.Visible = false;
                        CIFPANEL.Visible = true;
                        SPMGPANEL.Visible = true;
                        INCENTIVEPANEL.Visible = true;
                        DICINCENTIVEPANEL.Visible = false;
                        DWAPANEL.Visible = true;
                        DWADICPANEL.Visible = false;
                        

                        FillProposalDetails();
                        FillProposalDistwiseDetails();
                        FillProposalDetailsTourism();
                        FillProposalDetailsIT();
                        LandServiceBind();

                        CSRPortletSatus();
                        ViewApaaStatus();
                        ViewCICGData();
                        BindSPMGData();
                        IncentiveBind();

                        ///////// Department Wise Approval
                        ViewServiceStatus();                       

                        #endregion
                    }
                    else if (intDesignationId == 125 || intDesignationId == 97) //// ACS MSME and PS MSME
                    {

                        #region PS-MSME

                        SWASPANEL.Visible = true;
                        SWASDICPANEL.Visible = false;
                        LADPANEL.Visible = true;
                        DICLADPANEL.Visible = false;
                        CSRPANEL.Visible = true;
                        APPPANEL.Visible = true;
                        DICAPPPANEL.Visible = false;
                        CIFPANEL.Visible = true;
                        SPMGPANEL.Visible = true;
                        INCENTIVEPANEL.Visible = true;
                        DICINCENTIVEPANEL.Visible = false;
                        DWAPANEL.Visible = true;
                        DWADICPANEL.Visible = false;
                        

                        FillProposalDetails();
                        FillProposalDistwiseDetails();
                        FillProposalDetailsTourism();
                        FillProposalDetailsIT();
                        LandServiceBind();

                        CSRPortletSatus();
                        ViewApaaStatus();
                        ViewCICGData();
                        BindSPMGData();
                        IncentiveBind();

                        ///////// Department Wise Approval
                        ViewServiceStatus();

                       

                        #endregion
                    }
                    else if (intDesignationId == 96) //// PS Odisha
                    {

                        #region PS Odisha

                        SWASPANEL.Visible = true;
                        SWASDICPANEL.Visible = false;
                        LADPANEL.Visible = true;
                        DICLADPANEL.Visible = false;
                        CSRPANEL.Visible = true;
                        APPPANEL.Visible = true;
                        DICAPPPANEL.Visible = false;
                        CIFPANEL.Visible = true;
                        SPMGPANEL.Visible = true;
                        INCENTIVEPANEL.Visible = true;
                        DICINCENTIVEPANEL.Visible = false;
                        DWAPANEL.Visible = true;
                        DWADICPANEL.Visible = false;
                        

                        FillProposalDetails();
                        FillProposalDistwiseDetails();
                        FillProposalDetailsTourism();
                        FillProposalDetailsIT();
                        // /////// Land Allotment Details
                         LandServiceBind();

                        CSRPortletSatus();
                        ViewApaaStatus();
                        ViewCICGData();
                        BindSPMGData();
                        IncentiveBind();

                       // /////// Department Wise Approval
                        ViewServiceStatus();

                       

                        #endregion
                    }
                    else if (intDesignationId == 98) //// PS Finance
                    {


                        #region PS Finance

                        SWASPANEL.Visible = true;
                        SWASDICPANEL.Visible = false;
                        LADPANEL.Visible = true;
                        DICLADPANEL.Visible = false;
                        CSRPANEL.Visible = true;
                        APPPANEL.Visible = true;
                        DICAPPPANEL.Visible = false;
                        CIFPANEL.Visible = true;
                        SPMGPANEL.Visible = true;
                        INCENTIVEPANEL.Visible = true;
                        DICINCENTIVEPANEL.Visible = false;
                        DWAPANEL.Visible = true;
                        DWADICPANEL.Visible = false;
                        

                        FillProposalDetails();
                        FillProposalDistwiseDetails();
                        FillProposalDetailsTourism();
                        FillProposalDetailsIT();
                        LandServiceBind();

                        CSRPortletSatus();
                        ViewApaaStatus();
                        ViewCICGData();
                        BindSPMGData();
                        IncentiveBind();

                        ///////// Department Wise Approval
                        ViewServiceStatus();

                      

                        #endregion
                    }
                    else if (intDesignationId == 99) //// CMD IPICOL
                    {

                        #region CMD IPICOL

                        SWASPANEL.Visible = true;
                        SWASDICPANEL.Visible = false;
                        LADPANEL.Visible = true;
                        DICLADPANEL.Visible = false;
                        CSRPANEL.Visible = true;
                        APPPANEL.Visible = true;
                        DICAPPPANEL.Visible = false;
                        CIFPANEL.Visible = true;
                        SPMGPANEL.Visible = true;
                        INCENTIVEPANEL.Visible = true;
                        DICINCENTIVEPANEL.Visible = false;
                        DWAPANEL.Visible = true;
                        DWADICPANEL.Visible = false;
                        

                        FillProposalDetails();
                        FillProposalDistwiseDetails();
                        FillProposalDetailsTourism();
                        FillProposalDetailsIT();
                        LandServiceBind();

                        CSRPortletSatus();
                        ViewApaaStatus();
                        ViewCICGData();
                        BindSPMGData();
                        IncentiveBind();

                        ///////// Department Wise Approval
                        ViewServiceStatus();



                        #endregion
                    }
                    else if (intDesignationId == 128) //// CMD IDCO
                    {

                        #region CMD IDCO

                        SWASPANEL.Visible = true;
                        SWASDICPANEL.Visible = false;
                        LADPANEL.Visible = true;
                        DICLADPANEL.Visible = false;
                        CSRPANEL.Visible = true;
                        APPPANEL.Visible = true;
                        DICAPPPANEL.Visible = false;
                        CIFPANEL.Visible = true;
                        SPMGPANEL.Visible = true;
                        INCENTIVEPANEL.Visible = true;
                        DICINCENTIVEPANEL.Visible = false;
                        DWAPANEL.Visible = true;
                        DWADICPANEL.Visible = false;
                        

                        FillProposalDetails();
                        FillProposalDistwiseDetails();
                        FillProposalDetailsTourism();
                        FillProposalDetailsIT();
                        LandServiceBind();

                        CSRPortletSatus();
                        ViewApaaStatus();
                        ViewCICGData();
                        BindSPMGData();
                        IncentiveBind();

                        ///////// Department Wise Approval
                        ViewServiceStatus();



                        #endregion
                    }
                    else if (intDesignationId == 100) //// SLNA IPICOL
                    {

                        #region SLNA-IPICOL

                        SWASPANEL.Visible = true;
                        SWASDICPANEL.Visible = false;
                        LADPANEL.Visible = true;
                        DICLADPANEL.Visible = false;
                        CSRPANEL.Visible = true;
                        APPPANEL.Visible = true;
                        DICAPPPANEL.Visible = false;
                        CIFPANEL.Visible = true;
                        SPMGPANEL.Visible = true;
                        INCENTIVEPANEL.Visible = true;
                        DICINCENTIVEPANEL.Visible = false;
                        DWAPANEL.Visible = true;
                        DWADICPANEL.Visible = false;
                        


                        FillProposalDetails();
                        FillProposalDistwiseDetails();
                        FillProposalDetailsTourism();
                        FillProposalDetailsIT();
                        LandServiceBind();

                        CSRPortletSatus();
                        ViewApaaStatus();
                        ViewCICGData();
                        BindSPMGData();
                        IncentiveBind();

                        ///////// Department Wise Approval
                        ViewServiceStatus();


                        #endregion
                    }
                    else if (intDesignationId == 10 || intDesignationId == 9)
                    {

                        //#region DIC-RIC
                        SWASPANEL.Visible = false;
                        SWASDICPANEL.Visible = true;
                        LADPANEL.Visible = false;
                        DICLADPANEL.Visible = true;
                        CSRPANEL.Visible = false;
                        APPPANEL.Visible = false;
                        DICAPPPANEL.Visible = true;
                        CIFPANEL.Visible = false;
                        SPMGPANEL.Visible = false;
                        INCENTIVEPANEL.Visible = false;
                        DICINCENTIVEPANEL.Visible = true;
                        DWAPANEL.Visible = false;
                        DWADICPANEL.Visible = true;
                        


                        FillDICProposalDetails();



                        LandServiceBindDic();
                        DicViewApaaStatus();
                        DicIncentiveBind();

                        ///////// Department Wise Approval
                        DicViewServiceStatus();

                       

                        //#endregion
                    }
                    else if (intDesignationId == 126)
                    {

                        #region Collector

                        SWASPANEL.Visible = false;
                        SWASDICPANEL.Visible = true;
                        LADPANEL.Visible = false;
                        DICLADPANEL.Visible = true;
                        CSRPANEL.Visible = false;
                        APPPANEL.Visible = false;
                        DICAPPPANEL.Visible = true;
                        CIFPANEL.Visible = false;
                        SPMGPANEL.Visible = false;
                        INCENTIVEPANEL.Visible = false;
                        DICINCENTIVEPANEL.Visible = true;
                        DWAPANEL.Visible = false;
                        DWADICPANEL.Visible = true;
                        


                        FillDICProposalDetails();

                        LandServiceBindDic();
                        DicViewApaaStatus();
                        ////CSRCountDistrict();
                        DicIncentiveBind();

                        ///////// Department Wise Approval
                        DicViewServiceStatus();

                       

                        #endregion
                    }
                    else if (intDesignationId == 101)
                    {

                        #region DI

                        SWASPANEL.Visible = true;
                        SWASDICPANEL.Visible = false;
                        LADPANEL.Visible = true;
                        CSRPANEL.Visible = true;
                        APPPANEL.Visible = true;
                        DICAPPPANEL.Visible = false;
                        CIFPANEL.Visible = true;
                        SPMGPANEL.Visible = true;
                        INCENTIVEPANEL.Visible = true;
                        DICINCENTIVEPANEL.Visible = false;
                        DWAPANEL.Visible = true;
                        DWADICPANEL.Visible = false;
                        


                        FillProposalDetails();
                        FillProposalDistwiseDetails();
                        FillProposalDetailsTourism();
                        FillProposalDetailsIT();
                        LandServiceBind();

                        CSRPortletSatus();
                        ViewApaaStatus();
                        ViewCICGData();
                        BindSPMGData();
                        IncentiveBind();

                        ///////// Department Wise Approval
                        ViewServiceStatus();                       

                        #endregion
                    }
                    else ///// DepartmentDashboard
                    {

                        SWASPANEL.Visible = false;
                        SWASDICPANEL.Visible = false;
                        LADPANEL.Visible = false;
                        DICLADPANEL.Visible = false;
                        CSRPANEL.Visible = false;
                        APPPANEL.Visible = false;
                        DICAPPPANEL.Visible = false;
                        CIFPANEL.Visible = true;
                        SPMGPANEL.Visible = false;
                        INCENTIVEPANEL.Visible = false;
                        DICINCENTIVEPANEL.Visible = false;
                        DWAPANEL.Visible = true;
                        DWADICPANEL.Visible = false;
                        

                        #region Department

                        ////intDeptId = Convert.ToInt32(objds.Tables[1].Rows[i]["intLevelDetailId"]);
                        ////intDeptId = objserviceDashboard.GetDepartment(intDeptId.ToString());

                        ///////// Department Wise Approval
                        ViewServiceStatus();

                        ///*-----------------------------------------------------------------------*/
                        ////// In case of pslabour (116)
                        ////// Display ORTPSA timeline count for Directorate of Labour and Directorate of F & B separately.
                        ///*-----------------------------------------------------------------------*/
                        //if (intDesignationId == 116)
                        //{
                        //    ///// Department Wise Approval
                        //   // ViewORTPSAStatus();
                        //   // CreateServiceHtmlTablePsLabour();
                        //}
                        //else
                        //{
                        //    ///// Create Mail Body                    
                        //    //CreateServiceHtmlTable();
                        //}

                        ///*-----------------------------------------------------------------------*/
                        ////// In Case of psfb(115),pslabour(116),psfscw(117),psospcb(122) 
                        ////// Display SPMG and CICG Count
                        ///*-----------------------------------------------------------------------*/
                        if (intDesignationId == 115 || intDesignationId == 116 || intDesignationId == 117 || intDesignationId == 122)
                        {
                            //ViewSPMGDEPTwiseCount();
                            ViewCICGData();

                            //CreatePealHtmlTable2();
                        }
                        #endregion
                    }
                }
                else
                {
                    intUserId = 0;
                    intDesignationId = 0;
                    intDeptId = 0;
                    intDistrictId = 0;
                    Session["EventUserid"] = null;
                    Session["EventDesignationid"] = null;
                    Session["EventDeptid"] = null;
                    Session["EventDistrictid"] = null;
                }
            }         
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
        }
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
    private void InsertSPMGStatus()
    {
        try
        {
            string finalquery = "";
            SqlCommand cmd;
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
            string serviceUrl = "https://esuvidha.gov.in/odisha/restservices/RestServer.php?view=issuestatus";
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
                                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                                    if (con.State == ConnectionState.Closed)
                                    {
                                        con.Open();
                                    }

                                    string query = "SELECT intYearId FROM T_SPMG_DASHBOARD WHERE intYearId='" + FinYear + "'";
                                    SqlDataAdapter adp = new SqlDataAdapter(query, con);
                                    DataSet ds = new DataSet();
                                    adp.Fill(ds);

                                    if (ds.Tables[0].Rows.Count > 0)
                                    {

                                        finalquery = "UPDATE T_SPMG_DASHBOARD SET [Issues Received]=" + DynTable.Rows[0]["Issues Received"].ToString() + ",[Issues Resolved]=" + DynTable.Rows[0]["Issues Resolved"].ToString() + "," +
                                             "[Issues Pending]=" + DynTable.Rows[0]["Issues Pending"].ToString() + ",[Issues Pending (more than 30 days)]=" + DynTable.Rows[0]["Issues Pending (more than 30 days)"].ToString() + ",DTM_CREATED_ON='" + DateTime.Now.ToString("dd-MMM-yy")
                                            + "'" + " WHERE intYearId='" + FinYear + "'";
                                    }
                                    else
                                    {

                                        finalquery = "INSERT INTO T_SPMG_DASHBOARD(INT_ID,[Issues Received],[Issues Resolved],[Issues Pending],[Issues Pending (more than 30 days)],DTM_CREATED_ON,intYearId)" +
                                        "VALUES(" + Convert.ToInt32(Session["Userid"]) + "," + DynTable.Rows[0]["Issues Received"].ToString() + "," + DynTable.Rows[0]["Issues Resolved"].ToString() + "," + DynTable.Rows[0]["Issues Pending"].ToString() + "," + DynTable.Rows[0]["Issues Pending (more than 30 days)"].ToString() + "," +
                                        "'" + DateTime.Now.ToString("dd-MMM-yy") + "'," + FinYear + ")";

                                    }
                                    cmd = new SqlCommand(finalquery, con);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    BindSPMGData();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Util.LogError(ex, "EventLogPage");
                            spmgreceivedclr.Style.Add("color", "#e5343d");
                            spmgresolvedclr.Style.Add("color", "#e5343d");
                            spmgpendingclr.Style.Add("color", "#e5343d");
                            spmgpending30clr.Style.Add("color", "#e5343d");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
            spmgreceivedclr.Style.Add("color", "#e5343d");
            spmgresolvedclr.Style.Add("color", "#e5343d");
            spmgpendingclr.Style.Add("color", "#e5343d");
            spmgpending30clr.Style.Add("color", "#e5343d");
        }
    }
    private void BindSPMGData()
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
                spmgreceivedclr.Style.Add("color", "#7a7a7a");
                spmgresolvedclr.Style.Add("color", "#7a7a7a");
                spmgpendingclr.Style.Add("color", "#7a7a7a");
                spmgpending30clr.Style.Add("color", "#7a7a7a");
            }
            else
            {
                spmgraised.InnerHtml = "0";
                spmgresolved.InnerHtml = "0";
                spmgpending.InnerHtml = "0";
                spmg30pending.InnerHtml = "0";
                spmgreceivedclr.Style.Add("color", "#7a7a7a");
                spmgresolvedclr.Style.Add("color", "#7a7a7a");
                spmgpendingclr.Style.Add("color", "#7a7a7a");
                spmgpending30clr.Style.Add("color", "#7a7a7a");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
            spmgreceivedclr.Style.Add("color", "#e5343d");
            spmgresolvedclr.Style.Add("color", "#e5343d");
            spmgpendingclr.Style.Add("color", "#e5343d");
            spmgpending30clr.Style.Add("color", "#e5343d");
            spmgraised.InnerHtml = "0";
            spmgresolved.InnerHtml = "0";
            spmgpending.InnerHtml = "0";
            spmg30pending.InnerHtml = "0";
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }
    private void CSRPortletSatus()
    {
        try
        {            
            string strType = "1";
            string strYear = "0";
            string strUniqueKey = "0";
            string strDistrict = "0";
            if (ddlcsrdistrict.SelectedIndex > 0)
            {
                strDistrict = ddlcsrdistrict.SelectedValue;
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
                        decimal TotAmount = Convert.ToDecimal(DynTable.Rows[0]["TotalAmount"]) / 100;

                        Spcor.InnerHtml = Convert.ToString(DynTable.Rows[0]["RecCSRProjectsUnderTakenByCorp"]);
                        SPProject.InnerHtml = Convert.ToString(DynTable.Rows[0]["TotalProject"]);
                        SPRecommendedCouncil.InnerHtml = Convert.ToString(DynTable.Rows[0]["TotalRecommendProject"]);
                        SPSpent.InnerHtml = Convert.ToString(Math.Round(TotAmount));
                        Spcorclr.Style.Add("color", "#7a7a7a");
                        SPProjectclr.Style.Add("color", "#7a7a7a");
                        SPRecommendedCouncilclr.Style.Add("color", "#7a7a7a");
                        SPSpentclr.Style.Add("color", "#7a7a7a");
                    }
                    else
                    {
                        Spcor.InnerHtml = "0";
                        SPProject.InnerHtml = "0";
                        SPRecommendedCouncil.InnerHtml = "0";
                        SPSpent.InnerHtml = "0";
                        Spcorclr.Style.Add("color", "#7a7a7a");
                        SPProjectclr.Style.Add("color", "#7a7a7a");
                        SPRecommendedCouncilclr.Style.Add("color", "#7a7a7a");
                        SPSpentclr.Style.Add("color", "#7a7a7a");
                    }
                }
            }
        }
        catch (Exception ex)
        {            
            Util.LogError(ex, "EventLogPage");
            Spcorclr.Style.Add("color", "#e5343d");
            SPProjectclr.Style.Add("color", "#e5343d");
            SPRecommendedCouncilclr.Style.Add("color", "#e5343d");
            SPSpentclr.Style.Add("color", "#e5343d");
            Spcor.InnerHtml = "0";
            SPProject.InnerHtml = "0";
            SPRecommendedCouncil.InnerHtml = "0";
            SPSpent.InnerHtml = "0";
        }
    }
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
        objSWP.intUserid = Convert.ToInt32(Session["EventUserid"]);

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
                        finalquery = "UPDATE T_CICG_DASHBOARD SET INT_ID=" + Convert.ToInt32(Session["EventUserid"]) + ",INT_INS_COMPLETED=" + finalOut[2].ToString() + ",INT_INS_SCHEDULED=" + DynTable.Rows[0]["InspectionScheduled"].ToString() + "," +
                             "INT_REPORT_PENDING=" + DynTable.Rows[0]["PendingInspection"].ToString() + ",INT_ReportNot_Uploaded=" + DynTable.Rows[0]["ReportNotUploaded"].ToString() + ",INT_UNATTENDED_INS=" + DynTable.Rows[0]["UnAttendedInspection"].ToString() + ",DTM_CREATED_ON='" + DateTime.Now.ToString("dd-MMM-yy")
                             + "',intDeptId=" + Department + ",intMonthId=" + Month + ",intYearId=" + Year + " WHERE INT_ID='" + ds.Tables[0].Rows[0]["INT_ID"].ToString() + "'";

                    }
                    else
                    {

                        finalquery = "INSERT INTO T_CICG_DASHBOARD(INT_ID,INT_INS_COMPLETED,INT_INS_SCHEDULED,INT_REPORT_PENDING,INT_ReportNot_Uploaded,INT_UNATTENDED_INS,DTM_CREATED_ON,intDeptId,intMonthId,intYearId)" +
                        "VALUES(" + Convert.ToInt32(Session["EventUserid"]) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["InspectionScheduled"].ToString() + "," + DynTable.Rows[0]["PendingInspection"].ToString() + ","
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
            Util.LogError(ex, "EventLogPage");
            SPcicgappliedclr.Style.Add("color", "#e5343d");
            SPcicgcompletedclr.Style.Add("color", "#e5343d");
            SPunattInsdashclr.Style.Add("color", "#e5343d");
            SPReprtNotUploadedclr.Style.Add("color", "#e5343d");
        }
    }
    private void ViewCICGData()
    {

        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "VCI";
            objSWP.intUserid = Convert.ToInt32(Session["EventUserid"]);
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
                SPcicgappliedclr.Style.Add("color", "#7a7a7a");
                SPcicgcompletedclr.Style.Add("color", "#7a7a7a");
                SPunattInsdashclr.Style.Add("color", "#7a7a7a");
                SPReprtNotUploadedclr.Style.Add("color", "#7a7a7a");
            }
            else
            {
                SPcicgapplied.InnerHtml = "0";
                SPcicgcompleted.InnerHtml = "0";
                SPunattInsdash.InnerHtml = "0";
                SPReprtNotUploaded.InnerHtml = "0";
                SPcicgappliedclr.Style.Add("color", "#7a7a7a");
                SPcicgcompletedclr.Style.Add("color", "#7a7a7a");
                SPunattInsdashclr.Style.Add("color", "#7a7a7a");
                SPReprtNotUploadedclr.Style.Add("color", "#7a7a7a");

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
            SPcicgappliedclr.Style.Add("color", "#e5343d");
            SPcicgcompletedclr.Style.Add("color", "#e5343d");
            SPunattInsdashclr.Style.Add("color", "#e5343d");
            SPReprtNotUploadedclr.Style.Add("color", "#e5343d");
            SPcicgapplied.InnerHtml = "0";
            SPcicgcompleted.InnerHtml = "0";
            SPunattInsdash.InnerHtml = "0";
            SPReprtNotUploaded.InnerHtml = "0";
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }
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
            objSWP.strFinacialYear = ddlPealYear.SelectedValue.ToString();
            objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
            objSWP.intUserid = Convert.ToInt32(Session["EventUserid"]);
            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
            if (objswpDashboardList.Count > 0)
            {
                lblPealApplied.Text = objswpDashboardList[0].strApplied;
                lblPealRejected.Text = objswpDashboardList[0].strRejected;
                lblPealApproved.Text = objswpDashboardList[0].strApproved;
                lblPealDeferred.Text = objswpDashboardList[0].strDeferred;
                lblPealUnderEvalution.Text = objswpDashboardList[0].strUnderEvaltion;
                lblPealQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                Lbl_Peal_ORTPSA_State.Text = objswpDashboardList[0].strPealOrtpsaCrossedState; ///// Added by Sushant Jena on Dt. 26-May-2018
            }
            else
            {
                lblPealApplied.Text = "0";
                lblPealRejected.Text = "0";
                lblPealApproved.Text = "0";
                lblPealUnderEvalution.Text = "0";
                lblPealQueryRaise.Text = "0";
                lblPealDeferred.Text = "0";
                Lbl_Peal_ORTPSA_State.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
        }
        finally
        {
            objSWP = null;
        }
    }
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
            objSWP.strFinacialYear = ddlPealYear.SelectedValue.ToString();
            objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
            objSWP.intUserid = Convert.ToInt32(Session["EventUserid"]);
            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
            if (objswpDashboardList.Count > 0)
            {
                lblPealdistApplied.Text = objswpDashboardList[0].strDistApplied;
                lblPealdistRejected.Text = objswpDashboardList[0].strDistRejected;
                lblPealdistApproved.Text = objswpDashboardList[0].strDistApproved;
                lblPealdistUnderEvalution.Text = objswpDashboardList[0].strDistUnderEvaltion;
                lblPealdistQueryRaise.Text = objswpDashboardList[0].QraiseTotal;
                lblPealdistDeferred.Text = objswpDashboardList[0].strDistDeferred;
                Lbl_Peal_ORTPSA_Dist.Text = objswpDashboardList[0].strPealOrtpsaCrossedDist;
            }
            else
            {
                Lbl_Peal_ORTPSA_Dist.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
        }
        finally
        {
            objSWP = null;
        }
    }
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
                Lbl_Peal_ORTPSA_Tourism.Text = objswpDashboardList[0].strPealOrtpsaCrossedITandTourism;
            }
            else
            {
                lblPealTourismApplied.Text = "0";
                lblPealTourismRejected.Text = "0";
                lblPealTourismApproved.Text = "0";
                lblPealTourismUnderEvalution.Text = "0";
                lblPealTourismDeferred.Text = "0";
                lblPealTourismQueryRaise.Text = "0";
                Lbl_Peal_ORTPSA_Tourism.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
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
                Lbl_Peal_ORTPSA_IT.Text = objswpDashboardList[0].strPealOrtpsaCrossedITandTourism;
            }
            else
            {
                lblPealITApplied.Text = "0";
                lblPealITRejected.Text = "0";
                lblPealITApproved.Text = "0";
                lblPealITUnderEvalution.Text = "0";
                lblPealITDeferred.Text = "0";
                lblPealITQueryRaise.Text = "0";
                Lbl_Peal_ORTPSA_IT.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
        }
        finally
        {
            objSWP = null;
        }
    }
    private void FillDICProposalDetails()
    {
        try
        {
            SWPDashboard objSWP = new SWPDashboard();
            DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "PB";
            string PealQuareter = string.IsNullOrEmpty(ddldicPealQuarter.SelectedValue) ? default(string) : ddldicPealQuarter.SelectedValue.ToString();
            string PealDistrict = string.IsNullOrEmpty(ddlpealdicdistrict.SelectedValue) ? default(string) : ddlpealdicdistrict.SelectedValue.ToString();
            string PealYear = string.IsNullOrEmpty(ddlpealdicyear.SelectedValue) ? default(string) : ddlpealdicyear.SelectedValue.ToString();
            objSWP.intQuarter = 0;           
            objSWP.strFinacialYear = PealYear.ToString();
            objSWP.intDistrictid = Convert.ToInt32(Session["EventDistrictid"].ToString());//Convert.ToInt16(PealDistrict);
            objSWP.intUserid = Convert.ToInt32(Session["EventUserid"]);

            objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
            lbldicPealApplied.Text = objswpDashboardList[0].strApplied;
            lbldicPealRejected.Text = objswpDashboardList[0].strRejected;
            lbldicPealApproved.Text = objswpDashboardList[0].strApproved;
            lbldicQueryInprogress.Text = objswpDashboardList[0].QraiseTotal;            
            lbldicPealUnderEvalution.Text = objswpDashboardList[0].strUnderEvaltion;
            lbldicPealDeferred.Text = objswpDashboardList[0].strDeferred;
            Lbl_dic_Peal_ORTPSA.Text = objswpDashboardList[0].strPealOrtpsaCrossedState;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
        }
        finally
        {
            objSWP = null;
        }
    }
    private void ViewServiceStatus()
    {
        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "D";           
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
            Util.LogError(ex, "EventLogPage");
        }
        finally
        {
            objDashboard = null;
            objDashboardBal = null;
        }
    }
    private void DicViewServiceStatus()
    {
        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "DGM";           
            objDashboard.intDistrictid = Convert.ToInt32(Session["EventDistrictid"]);           
            objDashboard.strFinacialYear = ddlserviceyear.SelectedValue;
            objDashboard.intMonthId = Convert.ToInt32(ddlServcMonth.SelectedValue);
            objDashboard.intUserid = Convert.ToInt32(Session["EventUserid"]);
            List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
            if (objServiceStatus.Count > 0)
            {
                dichdApplied.InnerHtml = objServiceStatus[0].strApplied.ToString();
                dichdApprove.InnerHtml = objServiceStatus[0].strApproved.ToString();
                dichdPending.InnerHtml = objServiceStatus[0].strPending.ToString();
                dichdReject.InnerHtml = objServiceStatus[0].strRejected.ToString();               
                dichdnqueryRaised.InnerHtml = objServiceStatus[0].QraiseTotal.ToString();
            }
            else
            {
                dichdApplied.InnerHtml = "0";
                dichdApprove.InnerHtml = "0";
                dichdPending.InnerHtml = "0";              
                dichdnqueryRaised.InnerHtml = "0";
                dichdReject.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
        }
        finally
        {
            objDashboard = null;
            objDashboardBal = null;
        }
    }
    private void IncentiveBind()
    {
        try
        {
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "B";           
            string IncentiveDistrict = string.IsNullOrEmpty(ddlIncentiveDistrict.SelectedValue) ? default(string) : ddlIncentiveDistrict.SelectedValue.ToString();
            string IncentiveYear = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
            objSWP.intQuarter = 0;           
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
            Util.LogError(ex, "EventLogPage");
        }
        finally
        {
            objSWP = null;
        }
    }
    private void DicIncentiveBind()
    {
        try
        {
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "B";
            string IncentiveQtr = string.IsNullOrEmpty(ddldicIncentive.SelectedValue) ? default(string) : ddldicIncentive.SelectedValue.ToString();
            string IncentiveDist = string.IsNullOrEmpty(ddldicincentivedistrict.SelectedValue) ? default(string) : ddldicincentivedistrict.SelectedValue.ToString();
            string IncentiveYr = string.IsNullOrEmpty(ddldicincentiveyear.SelectedValue) ? default(string) : ddldicincentiveyear.SelectedValue.ToString();
            objSWP.intQuarter = Convert.ToInt16(IncentiveQtr);
            objSWP.strFinacialYear = IncentiveYr.ToString();
            objSWP.intDistrictid = Convert.ToInt32(Session["EventDistrictid"].ToString());//Convert.ToInt16(IncentiveDist);
            objSWP.intUserid = 0;// Convert.ToInt32(Session["Userid"]);
            objswpDashboardList = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();
            lbldicIncApplied.Text = objswpDashboardList[0].INCAPLLIED;
            lbldicIncsanctioed.Text = objswpDashboardList[0].INCSANCTIONED;
            lbldicIncpending.Text = objswpDashboardList[0].INCPENDING;
            lbldicIncrejected.Text = objswpDashboardList[0].INCREJECTED;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
        }
        finally
        {
            objSWP = null;
        }
    }
    private void LandServiceBind()
    {
        try
        {
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "LANDV";
            objSWP.intUserid = 0;
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
            Util.LogError(ex, "EventLogPage");
        }
        finally
        {
            objSWP = null;
        }
    }
    private void LandServiceBindDic()
    {
        try
        {
            objSWP = new SWPDashboard();
            objserviceDashboard = new DashboardBusinessLayer();
            List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
            objSWP.strAction = "LANDV";
            objSWP.intUserid = 0;//Convert.ToInt32(Session["Userid"]);
            objSWP.intDeptId = 0;
            objSWP.intDistrictid = Convert.ToInt32(Session["EventDistrictid"].ToString());
            objSWP.Year = ddldicLandFinYear.SelectedValue;
            objswpDashboardList = objserviceDashboard.GETLandDetails(objSWP).ToList();
            dicspLandAssesment.InnerText = objswpDashboardList[0].LandAssessment;
            dicspLandAllotByIDCO.InnerText = objswpDashboardList[0].ApplnLandAllotedByIDCO;
            dicspPropIDCO.InnerText = objswpDashboardList[0].PropNoForLand;
            dicspLandAllot.InnerText = objswpDashboardList[0].AreaAllotLand;
            dicspORTPSALAnd.InnerText = objswpDashboardList[0].ApplnLandORTPS;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
        }
        finally
        {
            objSWP = null;
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

        string Year = string.IsNullOrEmpty(ddlAppaYear.SelectedValue) ? default(string) : ddlAppaYear.SelectedValue.ToString();      

        string Deptid = "0";       
        string UniqueKey = "0";
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(Session["EventUserid"]);
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
            else
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
                        finalquery = "UPDATE T_APAA_Service_Admin SET adminid=" + Convert.ToInt32(Session["EventUserid"]) + ",TotalApplied=" + finalOut[2].ToString() + ",TotalDisposed=" + DynTable.Rows[0]["TotalDisposed"].ToString() + "," +
                             "TotalMajorPendingIdco=" + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + ",TotalPendingIdco=" + DynTable.Rows[0]["TotalPendingIdco"].ToString() + ",TotalPendingUnit=" + DynTable.Rows[0]["TotalPendingUnit"].ToString() + ",dtmCreatedon='" + DateTime.Now.ToString("dd-MMM-yy") + "',intYearId='" + Year + "',intDistrictId=" + District + ",intMonthId=" + Month + " WHERE adminid='" + ds.Tables[0].Rows[0]["ADMINID"].ToString() + "'";
                    }
                    else
                    {
                        finalquery = "INSERT INTO T_APAA_Service_Admin(adminid,TotalApplied,TotalDisposed,TotalMajorPendingIdco,TotalPendingIdco,TotalPendingUnit,dtmCreatedon,intYearId,intDistrictId,intMonthId)" +
                    "VALUES(" + Convert.ToInt32(Session["EventUserid"]) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["TotalDisposed"].ToString() + "," + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingUnit"].ToString() + "," +
                    "'" + DateTime.Now.ToString("dd-MMM-yy") + "','" + Year + "'," + District + "," + Month + ")";
                    }
                    cmd = new SqlCommand(finalquery, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
            spchngrqstAppliedclr.Style.Add("color", "#e5343d");
            spchngreqdisposeclr.Style.Add("color", "#e5343d");
            spchngreqPendAtIDCOclr.Style.Add("color", "#e5343d");
            spchngReqCrossThirtyclr.Style.Add("color", "#e5343d");
            spnPendingatUnitclr.Style.Add("color", "#e5343d");
        }
    }
    private void ViewApaaStatus()
    {

        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "AP";
            objSWP.intUserid = Convert.ToInt32(Session["EventUserid"]);
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
                spchngrqstAppliedclr.Style.Add("color", "#7a7a7a");
                spchngreqdisposeclr.Style.Add("color", "#7a7a7a");
                spchngreqPendAtIDCOclr.Style.Add("color", "#7a7a7a");
                spchngReqCrossThirtyclr.Style.Add("color", "#7a7a7a");
                spnPendingatUnitclr.Style.Add("color", "#7a7a7a");
            }
            else
            {
                spchngrqstApplied.InnerHtml = "0";
                spchngreqdispose.InnerHtml = "0";
                spchngreqPendAtIDCO.InnerHtml = "0";
                spchngReqCrossThirty.InnerHtml = "0";
                spnPendingatUnit.InnerHtml = "0";
                spchngrqstAppliedclr.Style.Add("color", "#7a7a7a");
                spchngreqdisposeclr.Style.Add("color", "#7a7a7a");
                spchngreqPendAtIDCOclr.Style.Add("color", "#7a7a7a");
                spchngReqCrossThirtyclr.Style.Add("color", "#7a7a7a");
                spnPendingatUnitclr.Style.Add("color", "#7a7a7a");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
            spchngrqstAppliedclr.Style.Add("color", "#e5343d");
            spchngreqdisposeclr.Style.Add("color", "#e5343d");
            spchngreqPendAtIDCOclr.Style.Add("color", "#e5343d");
            spchngReqCrossThirtyclr.Style.Add("color", "#e5343d");
            spnPendingatUnitclr.Style.Add("color", "#e5343d");
            spchngrqstApplied.InnerHtml = "0";
            spchngreqdispose.InnerHtml = "0";
            spchngreqPendAtIDCO.InnerHtml = "0";
            spchngReqCrossThirty.InnerHtml = "0";
            spnPendingatUnit.InnerHtml = "0";
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }
    private void DicInsertAppaStatus(DropDownList ddlDistrict, DropDownList ddlMonth, DropDownList ddlYear)
    {
        string finalquery = string.Empty;
        string deptId = "";
        SqlCommand cmd = null;
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        string Type = string.Empty;
        SWPDashboard objSWP = new SWPDashboard();
        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        inputJson = inputJson.TrimStart('[').TrimEnd(']');
        string District = string.IsNullOrEmpty(ddldicAPAADistrict.SelectedValue) ? default(string) : ddldicAPAADistrict.SelectedValue.ToString();
        string Month = string.IsNullOrEmpty(ddldicAppaMonth.SelectedValue) ? default(string) : ddldicAppaMonth.SelectedValue.ToString();
        string Year = string.IsNullOrEmpty(ddldicAppaYear.SelectedValue) ? default(string) : ddldicAppaYear.SelectedValue.ToString();       
        string UniqueKey = "0";
        deptId = Session["EventDeptid"].ToString();
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.intUserid = Convert.ToInt32(Session["EventUserid"]);       
        deptId = Session["EventDeptid"].ToString();        
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
            District = Session["EventDistrictid"].ToString();
            string serviceUrl = "";
            if (Session["EventDesignationid"].ToString() == "126")
            {
                Type = "0";
                serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + District + "/" + 0 + "/" + Type + "/" + Year + "/" + Month;
            }
            else
            {
                serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + District + "/" + deptId + "/" + Type + "/" + Year + "/" + Month;
            }
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
                    string query = "SELECT ADMINID FROM T_APAA_Service_DIC WHERE CONVERT(DATE,dtmCreatedon)='" + DateTime.Now.ToString("dd-MMM-yy") + "'";
                    SqlDataAdapter adp = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        finalquery = "UPDATE T_APAA_Service_DIC SET adminid=" + Convert.ToInt32(Session["EventUserid"]) + ",TotalApplied=" + finalOut[2].ToString() + ",TotalDisposed=" + DynTable.Rows[0]["TotalDisposed"].ToString() + "," +
                             "TotalMajorPendingIdco=" + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + ",TotalPendingIdco=" + DynTable.Rows[0]["TotalPendingIdco"].ToString() + ",TotalPendingUnit=" + DynTable.Rows[0]["TotalPendingUnit"].ToString() + ",dtmCreatedon='" + DateTime.Now.ToString("dd-MMM-yy") + "',intYearId=" + Year + ",intDistrictId=" + District + ",intMonthId=" + Month + " WHERE adminid='" + ds.Tables[0].Rows[0]["ADMINID"].ToString() + "'";
                    }
                    else
                    {
                        finalquery = "INSERT INTO T_APAA_Service_DIC(adminid,TotalApplied,TotalDisposed,TotalMajorPendingIdco,TotalPendingIdco,TotalPendingUnit,dtmCreatedon,intYearId,intDistrictId,intMonthId)" +
                    "VALUES(" + Convert.ToInt32(Session["EventUserid"]) + "," + finalOut[2].ToString() + "," + DynTable.Rows[0]["TotalDisposed"].ToString() + "," + DynTable.Rows[0]["TotalMajorPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingIdco"].ToString() + "," + DynTable.Rows[0]["TotalPendingUnit"].ToString() + "," +
                    "'" + DateTime.Now.ToString("dd-MMM-yy") + "'," + Year + "," + District + "," + Month + ")";
                    }
                    cmd = new SqlCommand(finalquery, con);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
            dicspchngrqstAppliedclr.Style.Add("color", "#e5343d");
            dicspchngreqdisposeclr.Style.Add("color", "#e5343d");
            dicspchngreqPendAtIDCOclr.Style.Add("color", "#e5343d");
            dicspchngReqCrossThirtyclr.Style.Add("color", "#e5343d");
            dicspnPendingatUnitclr.Style.Add("color", "#e5343d");
        }
    }
    private void DicViewApaaStatus()
    {

        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "DIC";
            objSWP.intUserid = Convert.ToInt32(Session["EventUserid"]);           
            objSWP.Year = ddldicAppaYear.SelectedItem.Text;
            objSWP.intMonthId = Convert.ToInt32(ddldicAppaMonth.SelectedValue);
            objSWP.intDistrictid = Convert.ToInt32(ddldicAPAADistrict.SelectedValue);
            List<SWPDashboard> objPEALStatusList = objserviceDashboard.GetDashboardAPAAtatus(objSWP).ToList();
            if (objPEALStatusList.Count > 0)
            {
                dicspchngrqstApplied.InnerHtml = objPEALStatusList[0].strChngReqApplied.ToString();
                dicspchngreqdispose.InnerHtml = objPEALStatusList[0].strChngReqDispose.ToString();
                dicspchngreqPendAtIDCO.InnerHtml = objPEALStatusList[0].strChngReqPendingAtIDCO.ToString();
                dicspchngReqCrossThirty.InnerHtml = objPEALStatusList[0].strChngReqCrossthirty.ToString();               
                dicspnPendingatUnit.InnerHtml = objPEALStatusList[0].strChngReqPendAtUnit.ToString();
                dicspchngrqstAppliedclr.Style.Add("color", "#7a7a7a");
                dicspchngreqdisposeclr.Style.Add("color", "#7a7a7a");
                dicspchngreqPendAtIDCOclr.Style.Add("color", "#7a7a7a");
                dicspchngReqCrossThirtyclr.Style.Add("color", "#7a7a7a");
                dicspnPendingatUnitclr.Style.Add("color", "#7a7a7a");
            }
            else
            {
                dicspchngrqstApplied.InnerHtml = "0";
                dicspchngreqdispose.InnerHtml = "0";
                dicspchngreqPendAtIDCO.InnerHtml = "0";
                dicspchngReqCrossThirty.InnerHtml = "0";               
                dicspnPendingatUnit.InnerHtml = "0";
                dicspchngrqstAppliedclr.Style.Add("color", "#7a7a7a");
                dicspchngreqdisposeclr.Style.Add("color", "#7a7a7a");
                dicspchngreqPendAtIDCOclr.Style.Add("color", "#7a7a7a");
                dicspchngReqCrossThirtyclr.Style.Add("color", "#7a7a7a");
                dicspnPendingatUnitclr.Style.Add("color", "#7a7a7a");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
            dicspchngrqstAppliedclr.Style.Add("color", "#e5343d");
            dicspchngreqdisposeclr.Style.Add("color", "#e5343d");
            dicspchngreqPendAtIDCOclr.Style.Add("color", "#e5343d");
            dicspchngReqCrossThirtyclr.Style.Add("color", "#e5343d");
            dicspnPendingatUnitclr.Style.Add("color", "#e5343d");
            dicspchngrqstApplied.InnerHtml = "0";
            dicspchngreqdispose.InnerHtml = "0";
            dicspchngreqPendAtIDCO.InnerHtml = "0";
            dicspchngReqCrossThirty.InnerHtml = "0";
            dicspnPendingatUnit.InnerHtml = "0";
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }
    }
    private void CSRCountDistrict()
    {
        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetDataMonthYearWise/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + Convert.ToInt32(Session["EventDistrictid"].ToString());
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
                }
            }
        }
        catch (Exception ex)
        {           
            Util.LogError(ex, "EventLogPage");
        }
    }
    private void ViewSPMGDEPTwiseCount()
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

            string strCurrentYear = DateTime.Now.Year.ToString();
            string strCurrentMonth = DateTime.Now.Month.ToString();

            //string serviceUrl = "https://164.100.58.41//odisha/restservices/RestServer.php?view=issuestatus";
            string serviceUrl = "https://esuvidha.gov.in/odisha/restservices/RestServer.php?view=monthlyissuestatusbydepartmentid";
            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
                DepartmentID = intDeptId,
                CurrentYear = strCurrentYear,
                CurrentMonth = strCurrentMonth
            };

            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            var webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            webRequest.Method = WebRequestMethods.Http.Post;
            webRequest.ContentType = "application/json";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

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
                        var responseData = reader.ReadToEnd();
                        webResponse.Close();

                        string strResult = responseData.ToString();
                        if (strResult != "")
                        {
                            DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
                            if (DynTable.Rows.Count > 0)
                            {
                                //intSPMGPendingCM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Current"]);
                                //intSPMGMore30DaysCM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Current (more than 30 days))"]);

                                //intSPMGPendingLM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Last"]);
                                //intSPMGMore30DaysLM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Last (more than 30 days))"]);
                            }
                            else
                            {
                                //intSPMGPendingCM = 0;
                                //intSPMGMore30DaysCM = 0;
                                //intSPMGPendingLM = 0;
                                //intSPMGMore30DaysLM = 0;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "EventLogPage");
        }
    }
    protected void btnspmg_Click(object sender, EventArgs e)
    {
        InsertSPMGStatus();
        BindSPMGData();
    }
    protected void btnCSRStatus_Click(object sender, EventArgs e)
    {
        CSRPortletSatus();
    }
    protected void btnCICGStatus_Click(object sender, EventArgs e)
    {
        InsertCICGStatus(ddlCICGMonth, ddldeptCIF, ddlYearCICG);
        ViewCICGData();
    }
    protected void btnPealsubmit_Click(object sender, EventArgs e)
    {
        FillProposalDetails();
        FillProposalDistwiseDetails();
        FillProposalDetailsTourism();
        FillProposalDetailsIT();
    }
    protected void btnpealdicsubmit_Click(object sender, EventArgs e)
    {
        FillDICProposalDetails();
    }
    protected void btnStatusOfApproval_Click(object sender, EventArgs e)
    {
        ViewServiceStatus();
    }
    protected void btnIncentiveSubmit_Click(object sender, EventArgs e)
    {
        IncentiveBind();
    }
    protected void btnDicIncentiveSubmit_Click(object sender, EventArgs e)
    {
        DicIncentiveBind();
    }
    protected void btnLandSubmit_Click(object sender, EventArgs e)
    {
        LandServiceBind();
    }
    protected void btnAPAASubmit_Click(object sender, EventArgs e)
    {
        InsertAppaStatus(ddlAPAADistrict, ddlAppaMonth, ddlAppaYear);
        ViewApaaStatus();
    }
    protected void btnDicStatusOfApproval_Click(object sender, EventArgs e)
    {
        DicViewServiceStatus();
    }
    protected void btndicLandSubmit_Click(object sender, EventArgs e)
    {
        LandServiceBindDic();
    }
    protected void btndicAPAASubmit_Click(object sender, EventArgs e)
    {
        DicInsertAppaStatus(ddldicAPAADistrict, ddldicAppaMonth, ddldicAppaYear);
        DicViewApaaStatus();
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindService();
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "ShowSearchpanel()", true);
    }
}