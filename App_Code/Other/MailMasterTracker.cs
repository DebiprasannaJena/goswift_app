//******************************************************************************************************************
// File Name             :   MailMasterTracker.cs
// Description           :   Class file used for Master Tracker Scheduled Mail
// Created by            :   Sushant Kumar Jena
// Created on            :   26-Feb-2018
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer.Dashboard;
using System.Data;
using System.Text;
using System.Net.Mail;
using BusinessLogicLayer.Dashboard;
using System.Net;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Security.Cryptography;

/// <summary>
/// Summary description for MailMasterTracker
/// </summary>
public class MailMasterTracker
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    CommonDashboardFunction DashboradCommon = new CommonDashboardFunction();

    DataTable dtMail = new DataTable();

    int itSectorId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdIT"]);
    int tourismSectorId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdTOURISM"]);

    string strBody = "";
    int intUserId = 0;
    int intDeptId = 0;
    string strFY = "";

    string strDeptStatus = "DEPT";
    string strDomainName = "";
    string strUserName = "";

    string strCurrentMonthLastDate = "";
    string strLastMonthLastDate = "";

    string strCurrentYear = DateTime.Now.Year.ToString();
    string strCurrentMonth = DateTime.Now.Month.ToString();

    public MailMasterTracker()
    {
        //
        // TODO: Add constructor logic here
        //       

        /*--------------------------------------------------------------------------------*/
        //// Get previous month last date as [Till current month] 
        //// and previous of previous month last date as [Till last month]
        //// Note:- If the user sending request to get data for month-July and year-2018,
        //// then it will display data till 30th-Jun-2018 as [Till Current Month] and till 31st-May-2018 as [Till Last Month].
        /*--------------------------------------------------------------------------------*/
        DateTime dt = new DateTime();
        dt = DateTime.Now.AddMonths(-1);
        int intYear = dt.Year;
        int intMonth = dt.Month;
        int daysInMonth = DateTime.DaysInMonth(intYear, intMonth);
        DateTime dtCurrentMonthLastDate = new DateTime(intYear, intMonth, daysInMonth);
        //strCurrentMonthLastDate = string.Format("{0:dd-MMM-yyyy}", dtCurrentMonthLastDate);
        strCurrentMonthLastDate = String.Format(dtCurrentMonthLastDate.ToString("dd{0} MMMM, yyyy"), GetSuffix(dtCurrentMonthLastDate.Day.ToString()));

        dt = dt.AddMonths(-1);
        intYear = dt.Year;
        intMonth = dt.Month;
        daysInMonth = DateTime.DaysInMonth(intYear, intMonth);
        DateTime dtLastMonthLastDate = new DateTime(intYear, intMonth, daysInMonth);
        //strLastMonthLastDate = string.Format("{0:dd-MMM-yyyy}", dtLastMonthLastDate);
        strLastMonthLastDate = String.Format(dtLastMonthLastDate.ToString("dd{0} MMMM, yyyy"), GetSuffix(dtLastMonthLastDate.Day.ToString()));
    }

    ///// Schedule the Mail (Automatics)
    public void mailSchedule()
    {
        configureMail("AUTO");
    }

    ///// Schedule the Mail (Manual)
    public void mailScheduleManual()
    {
        configureMail("MANUAL");
    }

    private void configureMail(string strMailType)
    {
        string strMailId = null;

        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();

        objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
        objCommand.CommandType = CommandType.StoredProcedure;
        objCommand.Connection = conn;

        objCommand.Parameters.AddWithValue("@P_VCH_ACTION", 'V');

        objDa.SelectCommand = objCommand;
        objDa.Fill(objds);

        if (objds.Tables[0].Rows.Count > 0)
        {
            string strStatus = objds.Tables[0].Rows[0]["STATUS"].ToString(); //// If Status=2 then Send mail

            string strSpamMode = objds.Tables[3].Rows[0]["vchSpamMode"].ToString(); //// Spam Mode (ON/OFF)
            string strSpamText = objds.Tables[3].Rows[0]["vchSpamText"].ToString(); //// Span Text

            /*-----------------------------------------------------------------*/
            /////// Check internal mail id(s) present or not. 
            /////// If present then all the mail will be sent to internal mail id(s) only.
            /////// If not present then all the mail will be sent to respective users.
            /*-----------------------------------------------------------------*/
            if (objds.Tables[2].Rows.Count > 0)
            {
                dtMail = objds.Tables[2];
            }
            else
            {
                dtMail = new DataTable();
            }

            /*-----------------------------------------------------------------*/
            ///// If the user gave request to fire mail in MANUAL process then set strStatus=2 
            /*-----------------------------------------------------------------*/
            if (strMailType == "MANUAL")
            {
                strStatus = "2";
            }

            /*-----------------------------------------------------------------*/

            if (strStatus == "2")  ///// Allow to send mail
            {
                ////// Collect SPMG data for all and use it in mail sending process of PS,CS,CM etc level. 
                
                getSPMGDataForAll();

                ////// Collect District SPMG data for all and use it in mail sending process of DIC,RIC,COLLECTOR etc level. 

                getSPMGDistrictDataForAll();

                /*-----------------------------------------------------------------*/
                ////// Insert Record (To Keep Track Records For Mail Scheduled)
                /*-----------------------------------------------------------------*/
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                objCommand = new SqlCommand();
                objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", 'A');
                objCommand.Parameters.AddWithValue("@P_VCH_MAIL_TYPE", strMailType);
                objCommand.ExecuteNonQuery();

                for (int i = 0; i < objds.Tables[1].Rows.Count; i++)
                {
                    System.Threading.Thread.Sleep(2000);

                    strUserName = objds.Tables[1].Rows[i]["vchUserName"].ToString();

                    intUserId = Convert.ToInt32(objds.Tables[1].Rows[i]["intUserId"]);
                    intDesignationId = Convert.ToInt32(objds.Tables[1].Rows[i]["intDesignationId"]);
                    strMailId = Convert.ToString(objds.Tables[1].Rows[i]["vchEmail"]);

                    string strCcMailId = Convert.ToString(objds.Tables[1].Rows[i]["vchCcMailId"]);
                    string strCcEnableStatus = Convert.ToString(objds.Tables[1].Rows[i]["chCcEnableStatus"]);

                    string strBccMailId = Convert.ToString(objds.Tables[1].Rows[i]["vchBccMailId"]);
                    string strBccEnableStatus = Convert.ToString(objds.Tables[1].Rows[i]["chBccEnableStatus"]);

                    #region CheckDesignation

                    if (intDesignationId == 94) //// CM Odisha
                    {
                        //Response.Redirect("CmDashboard.aspx");
                        #region CM Odisha

                        FillProposalDetailsState();
                        FillProposalDistrictLevel();
                        FillProposalITLevel();
                        FillProposalTourismLevel();

                        LandServiceBind();

                        CSRSpendAll();
                        ApaaStatus();
                        ViewCICGMasterData();
                        ViewSPMGMasterData();
                        ViewSPMGDistrictMasterData();//Added By Manoj Kumar Behera on 12.09.2019
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatus();

                        ///// Create Mail Body
                        CreatePealHtmlTableFragment();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateServiceHtmlTable();
                        CreateSPMGStateandDistrictLevelHtmlTable();//Added By Manoj Kumar Behera on 12.09.2019
                        #endregion
                    }
                    else if (intDesignationId == 95 || intDesignationId == 124) //// Chief Secretary Odisha and Development Commissioner
                    {
                        //Response.Redirect("ChiefSecretaryDashboard.aspx");  

                        #region CS Odisha

                        FillProposalDetailsState();
                        FillProposalDistrictLevel();
                        FillProposalITLevel();
                        FillProposalTourismLevel();

                        LandServiceBind();

                        CSRSpendAll();
                        ApaaStatus();
                        ViewCICGMasterData();
                        ViewSPMGMasterData();
                        ViewSPMGDistrictMasterData();//Added By Manoj Kumar Behera on 12.09.2019
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatus();

                        ///// Create Mail Body
                        CreatePealHtmlTableFragment();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateServiceHtmlTable();
                        CreateSPMGStateandDistrictLevelHtmlTable();//Added By Manoj Kumar Behera on 12.09.2019
                        #endregion
                    }
                    else if (intDesignationId == 125 || intDesignationId == 97) //// ACS MSME and PS MSME
                    {
                        //Response.Redirect("PS(MSME)Dashboard.aspx");

                        #region PS-MSME

                        FillProposalDetailsState();
                        FillProposalDistrictLevel();
                        FillProposalITLevel();
                        FillProposalTourismLevel();

                        LandServiceBind();

                        CSRSpendAll();
                        ApaaStatus();
                        ViewCICGMasterData();
                        ViewSPMGMasterData();
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatus();

                        ///// Create Mail Body
                        CreatePealHtmlTableFragment();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateServiceHtmlTable();

                        #endregion
                    }
                    else if (intDesignationId == 96) //// PS Odisha
                    {
                        //Response.Redirect("PSIndustriesDashboard.aspx");   

                        #region PS Odisha

                        FillProposalDetailsState();
                        FillProposalDistrictLevel();
                        FillProposalITLevel();
                        FillProposalTourismLevel();

                        //fillTotalInvestmentLevelWise();
                        //fillTotalEmploymentLevelWise();

                        ///// Land Allotment Details
                        LandServiceBind();

                        CSRSpendAll();
                        ApaaStatus();
                        ViewCICGMasterData();
                        ViewSPMGMasterData();
                        ViewSPMGDistrictMasterData();//Added By Manoj Kumar Behera on 12.09.2019
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatus();

                        ///// Create Mail Body
                        CreatePealHtmlTableFragment();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateServiceHtmlTable();
                        CreateSPMGStateandDistrictLevelHtmlTable();//Added By Manoj Kumar Behera on 12.09.2019
                        #endregion
                    }
                    else if (intDesignationId == 98) //// PS Finance
                    {
                        //Response.Redirect("PS(Finance)Dashboard.aspx");   

                        #region PS Finance

                        FillProposalDetailsState();
                        FillProposalDistrictLevel();
                        FillProposalITLevel();
                        FillProposalTourismLevel();

                        LandServiceBind();

                        CSRSpendAll();
                        ApaaStatus();
                        ViewCICGMasterData();
                        ViewSPMGMasterData();
                        ViewSPMGDistrictMasterData();//Added By Manoj Kumar Behera on 12.09.2019
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatus();

                        ///// Create Mail Body
                        CreatePealHtmlTableFragment();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateServiceHtmlTable();
                        CreateSPMGStateandDistrictLevelHtmlTable();//Added By Manoj Kumar Behera on 12.09.2019
                        #endregion
                    }
                    else if (intDesignationId == 99) //// CMD IPICOL
                    {
                        //Response.Redirect("CMDIPICOLDashboard.aspx");

                        #region CMD IPICOL

                        FillProposalDetailsState();
                        FillProposalDistrictLevel();
                        FillProposalITLevel();
                        FillProposalTourismLevel();

                        LandServiceBind();

                        CSRSpendAll();
                        ApaaStatus();
                        ViewCICGMasterData();
                        ViewSPMGMasterData();
                        ViewSPMGDistrictMasterData();//Added By Manoj Kumar Behera on 12.09.2019
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatus();

                        ///// Create Mail Body
                        CreatePealHtmlTableFragment();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateServiceHtmlTable();
                        CreateSPMGStateandDistrictLevelHtmlTable();//Added By Manoj Kumar Behera on 12.09.2019
                        #endregion
                    }
                    else if (intDesignationId == 128) //// CMD IDCO
                    {
                        //Response.Redirect("CMDIDCODashboard.aspx");

                        #region CMD IDCO

                        FillProposalDetailsState();
                        FillProposalDistrictLevel();
                        FillProposalITLevel();
                        FillProposalTourismLevel();

                        LandServiceBind();

                        CSRSpendAll();
                        ApaaStatus();
                        ViewCICGMasterData();
                        ViewSPMGMasterData();
                        ViewSPMGDistrictMasterData();//Added By Manoj Kumar Behera on 12.09.2019
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatus();

                        ///// Create Mail Body
                        CreatePealHtmlTableFragment();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateServiceHtmlTable();
                        CreateSPMGStateandDistrictLevelHtmlTable();//Added By Manoj Kumar Behera on 12.09.2019
                        #endregion
                    }
                    else if (intDesignationId == 100) //// SLNA IPICOL
                    {
                        //Response.Redirect("GMDashboard.aspx");

                        #region SLNA-IPICOL

                        FillProposalDetailsState();
                        FillProposalDistrictLevel();
                        FillProposalITLevel();
                        FillProposalTourismLevel();

                        LandServiceBind();

                        CSRSpendAll();
                        ApaaStatus();
                        ViewCICGMasterData();
                        ViewSPMGMasterData();
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatus();

                        ///// Create Mail Body
                        CreatePealHtmlTableFragment();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateServiceHtmlTable();

                        #endregion
                    }
                    else if (intDesignationId == 10 || intDesignationId == 9)
                    {
                        //Response.Redirect("DICDashboard.aspx");

                        #region DIC-RIC

                        strDeptStatus = "DOMAIN";

                        intDeptId = Convert.ToInt32(objds.Tables[1].Rows[i]["intLevelDetailId"]);
                        intDistrictId = Convert.ToInt32(objds.Tables[1].Rows[i]["intDistrict"]);
                        strDomainName = objds.Tables[1].Rows[i]["vchDomainUName"].ToString();

                        FillProposalDetailsDIC();

                        //fillTotalInvestmentDIC();
                        //fillTotalEmploymentDIC();

                        LandServiceBindDIC();
                        ApaaStatus();
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatusDIC();
                        ViewSPMGDistrictMasterData();//Added By Manoj Kumar Behera on 12.09.2019

                        ///// Create Mail Body
                        CreatePealHtmlTable1();
                        CreateServiceHtmlTable();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateSPMGDistrictLevelHtmlTable();//Added By Manoj Kumar Behera on 12.09.2019
                        #endregion
                    }
                    else if (intDesignationId == 126)
                    {
                        //Response.Redirect("DICDashboard.aspx");

                        #region Collector

                        strDeptStatus = "DOMAIN";

                        intDeptId = Convert.ToInt32(objds.Tables[1].Rows[i]["intLevelDetailId"]);
                        intDistrictId = Convert.ToInt32(objds.Tables[1].Rows[i]["intDistrict"]);
                        strDomainName = objds.Tables[1].Rows[i]["vchDomainUName"].ToString();

                        FillProposalDetailsDIC();

                        LandServiceBindDIC();
                        ApaaStatus();
                        CSRCountDistrict();
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatusDIC();
                        ViewSPMGDistrictMasterData();//Added By Manoj Kumar Behera on 12.09.2019

                        ///// Create Mail Body
                        CreatePealHtmlTable1();
                        CreateServiceHtmlTable();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateSPMGDistrictLevelHtmlTable();//Added By Manoj Kumar Behera on 12.09.2019
                        #endregion
                    }
                    else if (intDesignationId == 101)
                    {
                        //Response.Redirect("DIDashboard.aspx");

                        #region DI

                        strDeptStatus = "DOMAIN";

                        //intDeptId = Convert.ToInt32(objds.Tables[1].Rows[i]["intLevelDetailId"]);
                        //intDistrictId = Convert.ToInt32(objds.Tables[1].Rows[i]["intDistrict"]);
                        strDomainName = objds.Tables[1].Rows[i]["vchDomainUName"].ToString();

                        FillProposalDetailsState();
                        FillProposalDistrictLevel();
                        FillProposalITLevel();
                        FillProposalTourismLevel();

                        LandServiceBind();

                        CSRSpendAll();
                        ApaaStatus();
                        ViewCICGMasterData();
                        //ViewSPMGMasterData();
                        ViewSPMGDistrictMasterData();//Added By Manoj Kumar Behera on 12.09.2019
                        IncentiveMasterBind();

                        ///// Department Wise Approval
                        ViewServiceStatus();

                        ///// Create Mail Body

                        CreatePealHtmlTableFragment();
                        CreateLandHtmlTable();
                        CreatePealHtmlTable2();
                        CreateServiceHtmlTable();
                        CreateSPMGDistrictLevelHtmlTable();//Added By Manoj Kumar Behera on 12.09.2019
                        #endregion
                    }
                    else ///// DepartmentDashboard
                    {
                        //Response.Redirect("DepartmentDashboard.aspx");  

                        #region Department

                        intDeptId = Convert.ToInt32(objds.Tables[1].Rows[i]["intLevelDetailId"]);
                        intDeptId = objserviceDashboard.GetDepartment(intDeptId.ToString());

                        ///// Department Wise Approval
                        ViewServiceStatus();

                        /*-----------------------------------------------------------------------*/
                        //// In case of pslabour (116)
                        //// Display ORTPSA timeline count for Directorate of Labour and Directorate of F & B separately.
                        /*-----------------------------------------------------------------------*/
                        if (intDesignationId == 116)
                        {
                            ///// Department Wise Approval
                            ViewORTPSAStatus();
                            CreateServiceHtmlTablePsLabour();
                        }
                        else
                        {
                            ///// Create Mail Body                    
                            CreateServiceHtmlTable();
                        }

                        /*-----------------------------------------------------------------------*/
                        //// In Case of psfb(115),pslabour(116),psfscw(117),psospcb(122) 
                        //// Display SPMG and CICG Count
                        /*-----------------------------------------------------------------------*/
                        if (intDesignationId == 115 || intDesignationId == 116 || intDesignationId == 117 || intDesignationId == 122)
                        {
                            ViewSPMGDEPTwiseCount();
                            ViewCICGMasterData();

                            CreatePealHtmlTable2();
                        }

                        #endregion
                    }

                    #endregion

                    /*------------------------------------------------*/
                    ////// Add Header,Footer for Mail and Send
                    /*------------------------------------------------*/
                    mailHeader();
                    mailFooter();
                    SendMail(strBody, strMailId, strCcMailId, strCcEnableStatus, strBccMailId, strBccEnableStatus, strSpamMode, strSpamText);

                    /*------------------------------------------------*/

                    intUserId = 0;
                    intDesignationId = 0;
                    strMailId = null;
                    intDeptId = 0;
                    intDistrictId = 0;
                    strDomainName = "";
                    strDeptStatus = "DEPT";

                    clearVaribales();
                }
            }
        }
    }

    ///// Reset variables after each mail
    private void clearVaribales()
    {
        strBody = "";

        /*------------------------------------------------------*/
        ////// Clear SWP state level variables

        intPealAppliedStateCM = 0;
        intPealApprovedStateCM = 0;
        intPealRejectedStateCM = 0;
        intPealDeferredStateCM = 0;
        intPealQueryRaiseStateCM = 0;
        intPealPendingStateCM = 0;
        intPealOrtpsaStateCM = 0;
        decTotalInvestStateCM = 0;

        intPealAppliedStateLM = 0;
        intPealApprovedStateLM = 0;
        intPealRejectedStateLM = 0;
        intPealDeferredStateLM = 0;
        intPealQueryRaiseStateLM = 0;
        intPealPendingStateLM = 0;
        intPealOrtpsaStateLM = 0;
        decTotalInvestStateLM = 0;

        /*------------------------------------------------------*/
        ////// Clear SWP district level variables

        intPealAppliedDistCM = 0;
        intPealApprovedDistCM = 0;
        intPealRejectedDistCM = 0;
        intPealDeferredDistCM = 0;
        intPealQueryRaiseDistCM = 0;
        intPealPendingDistCM = 0;
        intPealOrtpsaDistCM = 0;
        decTotalInvestDistrictCM = 0;

        intPealAppliedDistLM = 0;
        intPealApprovedDistLM = 0;
        intPealRejectedDistLM = 0;
        intPealDeferredDistLM = 0;
        intPealQueryRaiseDistLM = 0;
        intPealPendingDistLM = 0;
        intPealOrtpsaDistLM = 0;
        decTotalInvestDistrictLM = 0;

        /*------------------------------------------------------*/
        ////// Clear SWP IT level variables

        intPealAppliedITCM = 0;
        intPealApprovedITCM = 0;
        intPealRejectedITCM = 0;
        intPealDeferredITCM = 0;
        intPealQueryRaiseITCM = 0;
        intPealPendingITCM = 0;
        intPealOrtpsaITCM = 0;
        decTotalInvestITCM = 0;

        intPealAppliedITLM = 0;
        intPealApprovedITLM = 0;
        intPealRejectedITLM = 0;
        intPealDeferredITLM = 0;
        intPealQueryRaiseITLM = 0;
        intPealPendingITLM = 0;
        intPealOrtpsaITLM = 0;
        decTotalInvestITLM = 0;

        /*------------------------------------------------------*/
        ////// Clear SWP tourism level variables

        intPealAppliedTourismCM = 0;
        intPealApprovedTourismCM = 0;
        intPealRejectedTourismCM = 0;
        intPealDeferredTourismCM = 0;
        intPealQueryRaiseTourismCM = 0;
        intPealPendingTourismCM = 0;
        intPealOrtpsaTourismCM = 0;
        decTotalInvestTourismCM = 0;

        intPealAppliedTourismLM = 0;
        intPealApprovedTourismLM = 0;
        intPealRejectedTourismLM = 0;
        intPealDeferredTourismLM = 0;
        intPealQueryRaiseTourismLM = 0;
        intPealPendingTourismLM = 0;
        intPealOrtpsaTourismLM = 0;
        decTotalInvestTourismLM = 0;

        /*------------------------------------------------------*/

        intTotalPealApplied = 0;
        intTotalPealApproved = 0;
        intTotalPealRejected = 0;
        intTotalPealQueryRaise = 0;
        intTotalPealPending = 0;
        intTotalPealOrtpsa = 0;

        /*------------------------------------------------------*/
        ////// Clear department service variables

        intDeptReceivedCM = null;
        intDeptApprovedCM = null;
        intDeptPendingCM = null;
        intDeptRejectedCM = null;
        intDeptQueryRaisedCM = null;
        intDeptOrtpsaCM = null;

        intDeptReceivedLM = null;
        intDeptApprovedLM = null;
        intDeptPendingLM = null;
        intDeptRejectedLM = null;
        intDeptQueryRaisedLM = null;
        intDeptOrtpsaLM = null;

        /*------------------------------------------------------*/
        ////// Clear land details variables

        intLandAssesmentCM = 0;
        intLandProposalIdcoCM = 0;
        intLandAllotByIdcoCM = 0;
        decLandAreaAllotedCM = 0;
        intLandOrtpsaExceedCM = 0;

        intLandAssesmentLM = 0;
        intLandProposalIdcoLM = 0;
        intLandAllotByIdcoLM = 0;
        decLandAreaAllotedLM = 0;
        intLandOrtpsaExceedLM = 0;

        /*------------------------------------------------------*/

        decInvestment = 0;
        intEmployment = 0;

        /*------------------------------------------------------*/
        ////// Clear Incentive Variables

        intInctPendingCM = null;
        intInctOrtpsaCM = null;

        intInctPendingLM = null;
        intInctOrtpsaLM = null;

        /*------------------------------------------------------*/
        ////// Clear SPMG Variables

        intSPMGPendingCM = null;
        intSPMGMore30DaysCM = null;

        intSPMGPendingLM = null;
        intSPMGMore30DaysLM = null;

        /*------------------------------------------------------*/
        ////// Clear APAA Variables

        intAppaPendingCM = null;
        intAppa30DaysCM = null;

        intAppaPendingLM = null;
        intAppa30DaysLM = null;

        /*------------------------------------------------------*/
        ////// Clear Common Variables

        intDesignationId = 0;
        intDistrictId = 0;

        /*------------------------------------------------------*/
        ////// Clear CSR Variables

        decCSRTotalAmountCM = null;
        intCSRcouncilCM = null;
        intCSRCorporateCM = null;

        decCSRTotalAmountLM = null;
        intCSRcouncilLM = null;
        intCSRCorporateLM = null;

        intCSRRecProjDistrictCM = null;
        intCSRTotalProjDistrictCM = null;
        intCSRUnderTakenProjDistrictCM = null;

        intCSRRecProjDistrictLM = null;
        intCSRTotalProjDistrictLM = null;
        intCSRUnderTakenProjDistrictLM = null;

        /*------------------------------------------------------*/
        ////// Clear CICG Variables

        intCIFPendingInspCM = null;
        intCIFInspNotUpdCM = null;

        intCIFPendingInspLM = null;
        intCIFInspNotUpdLM = null;

        /*------------------------------------------------------*/

        //intPassedTimeLine = null;
        //intORTPSA = null;

        //intApaa30DaysDIC = null;
        //intApaaPendingDIC = null;
    }

    ///// Send Mail
    private void SendMail(string textBody, string strToMailId, string strCcMailId, string strCcEnableStatus, string strBccMailId, string strBccEnableStatus, string strSpamMode, string strSpamText)
    {
        try
        {
            string strSubject = "GO-SWIFT || Monthly status as on " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

            if (strSpamMode == "ON" && strSpamText != "")
            {
                //strSubject += " - [Please consider this mail as safe and do not map it to your spam folder]";
                strSubject += " - [" + strSpamText + "]";
            }

            if (dtMail.Rows.Count > 0)
            {
                ////// Send Mail to Internal User               

                string[] arrToMailId = new string[dtMail.Rows.Count];

                for (int i = 0; i < dtMail.Rows.Count; i++)
                {
                    arrToMailId[i] = Convert.ToString(dtMail.Rows[i]["vchMailId"]);
                }

                string[] arrCcMail = new string[0];
                string[] arrBccMail = new string[0];

                CommonHelperCls objComm = new CommonHelperCls();
                bool bMailStatus = objComm.sendMailScheduler(strSubject, textBody, arrToMailId, arrCcMail, arrBccMail, true);
            }
            else
            {
                ////// Send Mail to Live User               

                string[] arrToMailId = new string[1];
                arrToMailId[0] = strToMailId;

                string[] arrCcMail = new string[0];
                if (strCcMailId != "" && strCcEnableStatus == "Y")
                {
                    strCcMailId = strCcMailId.Replace("\n", "");
                    arrCcMail = strCcMailId.Split(',');
                }

                string[] arrBccMail = new string[0];
                if (strBccMailId != "" && strBccEnableStatus == "Y")
                {
                    strBccMailId = strBccMailId.Replace("\n", "");
                    arrBccMail = strBccMailId.Split(',');
                }

                CommonHelperCls objComm = new CommonHelperCls();
                bool bMailStatus = objComm.sendMailScheduler(strSubject, textBody, arrToMailId, arrCcMail, arrBccMail, true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }
    }

    ///// HTML table start,end and Mail Header Footer

    #region HTMLCreation

    ///// Mail Header
    private void mailHeader()
    {
        string strDeptName = "";
        string strHeaderMsg = "";

        if (strDeptStatus == "DEPT")
        {
            if (intDeptId == 1 || intDeptId == 0) ///// For All PS Level User
            {
                strDeptName = "";
                //strHeaderMsg = "Dear Sir,"
                //             + "<br /><br />"
                //             + "Please find the following activity status under GOSWIFT for the past week.";

                //strHeaderMsg = "Dear Sir,"
                //                + "<br /><br />"
                //                + "The current position of industrial approvals and related matters since 1st April, 2016 to " + strLastMonthLastDate + " (Till last month) and since 1st April,2016 to " + strCurrentMonthLastDate + " (Till current month) is given below for your kind perusal and necessary action.";

                strHeaderMsg = "Dear Sir,"
                                + "<br /><br />"
                                + "The current position of industrial approvals and related matters as on " + strCurrentMonthLastDate + " is given below for your reference and suitable action.";
            }
            else ///// For Departmental User,DIC,RIC
            {
                strHeaderMsg = "Dear Sir/Madam,"
                                 + "<br /><br />"
                                 + "The current position of industrial approvals and related matters for ";

                if (intDesignationId == 116)
                {
                    strDeptName = "<strong>\"Directorate of Labour\"</strong> and <strong>\"Directorate of Factories & Boilers\"</strong>";

                    strHeaderMsg += strDeptName;
                }
                else
                {
                    int intParentDeptId = objserviceDashboard.GetDepartment(intDeptId.ToString());
                    strDeptName = objserviceDashboard.GetDeptName(intParentDeptId);

                    strHeaderMsg += "<strong>" + "\"" + strDeptName + "\"" + "</strong>";
                }

                //strHeaderMsg += " since 1st April, 2016 to " + strLastMonthLastDate + " (Till last month) and since 1st April,2016 to " + strCurrentMonthLastDate + " (Till current month) is given below for your kind perusal and necessary action.";
                strHeaderMsg += " as on " + strCurrentMonthLastDate + " is given below for your reference and suitable action.";

            }
        }
        else if (strDeptStatus == "DOMAIN")
        {
            strHeaderMsg = "Dear Sir/Madam,"
                         + "<br /><br />"
                         + "The current position of industrial approvals and related matters for ";

            strDeptName = strDomainName;

            strHeaderMsg += "<strong>" + "\"" + strDeptName + "\"" + "</strong>";
            //strHeaderMsg += " since 1st April, 2016 to " + strLastMonthLastDate + " (Till last month) and since 1st April,2016 to " + strCurrentMonthLastDate + " (Till current month) is given below for your kind perusal and necessary action.";
            strHeaderMsg += " as on " + strCurrentMonthLastDate + " is given below for your reference and suitable action.";
        }

        /*------------------------------------------------*/

        strHeaderMsg += "<br /><br />";
        strHeaderMsg = "<span style='font-family:Verdana;font-size:13px;'>" + strHeaderMsg + "</span>";

        strBody = strHeaderMsg + strBody;
    }
    ///// Mail Footer
    private void mailFooter()
    {
        strBody += "<span style='font-family:Verdana;font-size:11px;color:red'>* &nbsp;</span><span style='font-family:Verdana;font-size:11px;'>Since launch of GO SWIFT,i.e. 17-Nov-2017</span><br>";
        strBody += "<span style='font-family:Verdana;font-size:13px;'><br><br>From,<br>TEAM GO-SWIFT<br>-------------------------------------------------------------<br></span>";
        strBody += "<span style='font-family:Verdana;font-size:11px;'>For more details,you can view the dashboard by logging on to <a href='https://investodisha.gov.in/goswift/' target='_blank' title='Click here to redirect GO-SWIFT application.'>GO-SWIFT</a> application through login credentials already provided.</span>";
        strBody += "<br><span style='font-family:Verdana;font-size:11px;'>For further information you may write to support.investodisha@nic.in or call <strong>1800 345 7157</strong>(Toll Free Helpline).</span><br>";
    }

    string strStyle = "font-family:Verdana;font-size:12px; border-top:1px solid #f89d30;";
    string strMainHeadingStyle = "height:25px;background-color:#1a5f7a;color:#FFFFFF;font-weight:400;";
    string strHeaderStyle = "font-weight:700;color:#51a2b5;text-align:center;";
    string strSubHeaderStyle = "font-weight:500;color:#4da8cc;text-align:center;";
    string strPlainHeaderStyle = "font-weight:600;color:#1a5f7a;height:25px;";
    string strTrHeight = "height:20px;";

    ///// HTML Table for SWP Application Status (PEAL Detailed Information(Fragmented Data))
    private void CreatePealHtmlTableFragment()
    {
        strBody += "<table rules='all' border='1' cellpadding='0' cellspacing='0' width ='95%' style=" + strStyle + ">";

        //strBody += "<tr><td colspan='5' style=" + strMainHeadingStyle + ">Single Window Application Status <span style='font-size:10px;'>(Since 01-Apr-2016)</span></td></tr>";

        strBody += "<tr><td colspan='9' style=" + strMainHeadingStyle + ">Single Window Application Status - Cumulative Status</td></tr>";

        strBody += "<tr>"
                + "<th width='20%' rowspan='3' style=" + strHeaderStyle + ">Status</th>"
                + "<th width='20%' rowspan='2' colspan=2 style=" + strHeaderStyle + ">State Level</th>"
                + "<th width='20%' rowspan='2' colspan=2 style=" + strHeaderStyle + ">District Level</th>"
                + "<th colspan='4' style=" + strHeaderStyle + "><span style='text-align:center;'>Special Single Window</span></th>"
                + "</tr>";

        strBody += "<tr>"
                + "<th width='20%' colspan=2 style=" + strHeaderStyle + ">IT</th>"
                + "<th width='20%' colspan=2 style=" + strHeaderStyle + ">Tourism</th>"
                + "</tr>";

        strBody += "<tr>"
                + "<th width='9%' style=" + strSubHeaderStyle + ">Till last month</th>"
                + "<th width='11%' style=" + strSubHeaderStyle + ">Till current month</th>"
                + "<th width='9%' style=" + strSubHeaderStyle + ">Till last month</th>"
                + "<th width='11%' style=" + strSubHeaderStyle + ">Till current month</th>"
                + "<th width='9%' style=" + strSubHeaderStyle + ">Till last month</th>"
                + "<th width='11%' style=" + strSubHeaderStyle + ">Till current month</th>"
                + "<th width='9%' style=" + strSubHeaderStyle + ">Till last month</th>"
                + "<th width='11%' style=" + strSubHeaderStyle + ">Till current month</th>"
                + "</tr>";


        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Applied</td>"
                + "<td align='right'>" + FormatString(intPealAppliedStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealAppliedStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealAppliedDistLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealAppliedDistCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealAppliedITLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealAppliedITCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealAppliedTourismLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealAppliedTourismCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Approved</td>"
                + "<td align='right'>" + FormatString(intPealApprovedStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealApprovedStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealApprovedDistLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealApprovedDistCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealApprovedITLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealApprovedITCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealApprovedTourismLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealApprovedTourismCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Rejected</td>"
                + "<td align='right'>" + FormatString(intPealRejectedStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealRejectedStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealRejectedDistLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealRejectedDistCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealRejectedITLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealRejectedITCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealRejectedTourismLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealRejectedTourismCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Deferred</td>"
                + "<td align='right'>" + FormatString(intPealDeferredStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealDeferredStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealDeferredDistLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealDeferredDistCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealDeferredITLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealDeferredITCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealDeferredTourismLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealDeferredTourismCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Query In Progress</td>"
                + "<td align='right'>" + FormatString(intPealQueryRaiseStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealQueryRaiseStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealQueryRaiseDistLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealQueryRaiseDistCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealQueryRaiseITLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealQueryRaiseITCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealQueryRaiseTourismLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealQueryRaiseTourismCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Under Evaluation</td>"
                + "<td align='right'>" + FormatString(intPealPendingStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealPendingStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealPendingDistLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealPendingDistCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealPendingITLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealPendingITCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealPendingTourismLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealPendingTourismCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Applications pending more than 30 days</td>"
                + "<td align='right'>" + FormatString(intPealOrtpsaStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealOrtpsaStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealOrtpsaDistLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealOrtpsaDistCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealOrtpsaITLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealOrtpsaITCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealOrtpsaTourismLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealOrtpsaTourismCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Proposed Investment (Rs. in Cr.)</td>"
                + "<td align='right'>" + FormatString(decTotalInvestStateLM) + "</td>"
                + "<td align='right'>" + FormatString(decTotalInvestStateCM) + "</td>"
                + "<td align='right'>" + FormatString(decTotalInvestDistrictLM) + "</td>"
                + "<td align='right'>" + FormatString(decTotalInvestDistrictCM) + "</td>"
                + "<td align='right'>" + FormatString(decTotalInvestITLM) + "</td>"
                + "<td align='right'>" + FormatString(decTotalInvestITCM) + "</td>"
                + "<td align='right'>" + FormatString(decTotalInvestTourismLM) + "</td>"
                + "<td align='right'>" + FormatString(decTotalInvestTourismCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Proposed Employment</td>"
                + "<td align='right'>" + FormatString(intTotalEmpStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intTotalEmpStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intTotalEmpDistrictLM) + "</td>"
                + "<td align='right'>" + FormatString(intTotalEmpDistrictCM) + "</td>"
                + "<td align='right'>" + FormatString(intTotalEmpITLM) + "</td>"
                + "<td align='right'>" + FormatString(intTotalEmpITCM) + "</td>"
                + "<td align='right'>" + FormatString(intTotalEmpTourismLM) + "</td>"
                + "<td align='right'>" + FormatString(intTotalEmpTourismCM) + "</td>"
                + "</tr>";

        strBody += "</table><br/>";
    }

    ///// HTML Table for Land Allotment Details
    private void CreateLandHtmlTable()
    {
        strBody += "<table rules='all' border='1' cellpadding='0' cellspacing='0' width ='80%' bordercolor='Gray' style=" + strStyle + ">";

        strBody += "<tr><td colspan='3' style=" + strMainHeadingStyle + ">Land Allotment Detail - Cumulative Status</td></tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td width='56%' style=" + strSubHeaderStyle + ">Status</td>"
                + "<td width='22%' style=" + strSubHeaderStyle + ">Till last month</td>"
                + "<td width='22%' style=" + strSubHeaderStyle + ">Till current month</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>No. of Projects Requiring Land</td>"
                + "<td align='right'>" + FormatString(intLandAssesmentLM) + "</td>"
                + "<td align='right'>" + FormatString(intLandAssesmentCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>No. of Proposals sent to IDCO for Land Allotment</td>"
                + "<td align='right'>" + FormatString(intLandProposalIdcoLM) + "</td>"
                + "<td align='right'>" + FormatString(intLandProposalIdcoCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>No. of Proposals for which Land Allotted by IDCO</td>"
                + "<td align='right'>" + FormatString(intLandAllotByIdcoLM) + "</td>"
                + "<td align='right'>" + FormatString(intLandAllotByIdcoCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Area Allotted by IDCO (in Acres)</td>"
                + "<td align='right'>" + FormatString(decLandAreaAllotedLM) + "</td>"
                + "<td align='right'>" + FormatString(decLandAreaAllotedCM) + "</td>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td>Proposals past ORTPSA timeline</td>"
                + "<td align='right'>" + FormatString(intLandOrtpsaExceedLM) + "</td>"
                + "<td align='right'>" + FormatString(intLandOrtpsaExceedCM) + "</td>"
                + "</tr>";

        strBody += "</table><br/>";
    }

    ///// HTML Table for SWP Application Status (PEAL Information)
    private void CreatePealHtmlTable1()
    {
        strBody += "<table rules='all' border='1' cellpadding='0' cellspacing='0' width ='100%' bordercolor='Gray' style=" + strStyle + ">";

        strBody += "<tr>"
                + " <td colspan='14' style=" + strMainHeadingStyle + ">Single Window Application Status - Cumulative Status</td>"
                + " </tr>";

        strBody += "<tr>"
                + "<th colspan='10' style='font-family:Verdana;font-size:13px;text-align:center!important;font-weight:500;'>APPLICATIONS</th> "
                + "<th colspan='2' rowspan='2' style=" + strHeaderStyle + ">Proposed Investment <br> (Rs. in Cr.)</th> "
                + "<th colspan='2' rowspan='2' style=" + strHeaderStyle + ">Proposed Employment</th> "
                + "</tr>";

        strBody += "<tr>"
                + "<th width='15%' colspan='2' style=" + strHeaderStyle + ">Received</th>"
                + "<th width='15%' colspan='2' style=" + strHeaderStyle + ">Approved</th>"
                + "<th width='15%' colspan='2' style=" + strHeaderStyle + ">Pending</th>"
                + "<th width='15%' colspan='2' style=" + strHeaderStyle + ">Query in Progress</th>"
                + "<th width='15%' colspan='2' style=" + strHeaderStyle + ">Past ORTPSA timelines</th>"
                + "</tr>";

        strBody += "<tr>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='7%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "</tr>";


        strBody += "<tr style=" + strTrHeight + ">"
                + "<td align='right'>" + FormatString(intPealAppliedStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealAppliedStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealApprovedStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealApprovedStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealPendingStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealPendingStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealQueryRaiseStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealQueryRaiseStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intPealOrtpsaStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intPealOrtpsaStateCM) + "</td>"
                + "<td align='right'>" + FormatString(decTotalInvestStateLM) + "</td>"
                + "<td align='right'>" + FormatString(decTotalInvestStateCM) + "</td>"
                + "<td align='right'>" + FormatString(intTotalEmpStateLM) + "</td>"
                + "<td align='right'>" + FormatString(intTotalEmpStateCM) + "</td>"
                + "</tr>";

        strBody += "</table><br/>";
    }

    ///// HTML Table for Master Tracker Content
    private void CreatePealHtmlTable2()
    {
        strBody += "<table rules='all' border='1' cellpadding='0' cellspacing='0' width ='80%' bordercolor='Gray' style=" + strStyle + ">";

        ///// APAA Status (All and District)
        if (intAppaPendingCM != null)
        {
            strBody += "<tr>"
                    + " <td width='100%' colspan='3' style=" + strMainHeadingStyle + ">IDCO Post Allotment Applications - Cumulative Status</td>"
                    + " </tr>";
            strBody += "<tr>"
                    + " <td width='56%' style=" + strSubHeaderStyle + ">Status</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till last month</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till current month</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Pending Applications</td>"
                    + " <td align='right'>" + FormatString(intAppaPendingLM) + "</td>"
                    + " <td align='right'>" + FormatString(intAppaPendingCM) + "</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Applications pending more than 30 days</td>"
                    + " <td align='right'>" + FormatString(intAppa30DaysLM) + "</td>"
                    + " <td align='right'>" + FormatString(intAppa30DaysCM) + "</td>"
                    + " </tr>";
        }

        //if (intApaa30DaysDIC != null)
        //{
        //    strBody += "<tr>"
        //            + " <td width='100%' colspan='3' style=" + strMainHeadingStyle + ">IDCO Post Allotment Applications</td>"
        //            + " </tr>";

        //    strBody += "<tr>"
        //            + " <td width='56%' style=" + strHeaderStyle + ">Status</td>"
        //            + " <td width='22%' style=" + strHeaderStyle + ">Till last month</td>"
        //            + " <td width='22%' style=" + strHeaderStyle + ">Till current month</td>"
        //            + " </tr>";

        //    strBody += "<tr style=" + strTrHeight + ">"
        //            + " <td>Applications pending</td>"
        //            + " <td align='right'>XXXXXX</td>"
        //            + " <td align='right'>" + FormatString(intApaaPendingDIC) + "</td>"
        //            + " </tr>";
        //    strBody += "<tr style=" + strTrHeight + ">"
        //            + " <td>Applications pending for more than 30 days</td>"
        //            + " <td align='right'>XXXXXX</td>"
        //            + " <td align='right'>" + FormatString(intApaa30DaysDIC) + "</td>"
        //            + " </tr>";
        //}


        ///// Central Inspection Framework
        if (intCIFPendingInspCM != null)
        {
            strBody += "<tr>"
                    + " <td width='100%' colspan='3' style=" + strMainHeadingStyle + ">Central Inspection Framework - Cumulative Status</td>"
                    + " </tr>";

            strBody += "<tr>"
                    + " <td width='56%' style=" + strSubHeaderStyle + ">Status</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till last month</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till current month</td>"
                    + " </tr>";

            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Pending Inspections</td>"
                    + " <td align='right'>" + FormatString(intCIFPendingInspLM) + "</td>"
                    + " <td align='right'>" + FormatString(intCIFPendingInspCM) + "</td>"
                    + " </tr>";

            if (intCIFInspNotUpdCM != null)
            {
                strBody += "<tr style=" + strTrHeight + ">"
                        + " <td>Inspection reports not uploaded within 48 hours</td>"
                        + " <td align='right'>" + FormatString(intCIFInspNotUpdLM) + "</td>"
                        + " <td align='right'>" + FormatString(intCIFInspNotUpdCM) + "</td>"
                        + " </tr>";
            }
        }


      ////// Incentive (All and District)
      if (intInctPendingCM != null)
        {
            strBody += "<tr>"
                    + " <td width='100%' colspan='3' style=" + strMainHeadingStyle + ">Incentive Details - Cumulative Status<span style='color:Red'>*</span></td>"
                    + " </tr>";
            strBody += "<tr>"
                    + " <td width='56%' style=" + strSubHeaderStyle + ">Status</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till last month</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till current month</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Pending Incentive Applications</td>"
                    + " <td align='right'>" + FormatString(intInctPendingLM) + "</td>"
                    + " <td align='right'>" + FormatString(intInctPendingCM) + "</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Incentives applications pending more than 30 Days</td>"
                    + " <td align='right'>" + FormatString(intInctOrtpsaLM) + "</td>"
                    + " <td align='right'>" + FormatString(intInctOrtpsaCM) + "</td>"
                    + " </tr>";
        }

      ////// CSR All
      if (decCSRTotalAmountCM != null)
        {
            strBody += "<tr>"
                    + " <td width='100%' colspan='3' style=" + strMainHeadingStyle + ">CSR Spend - Cumulative Status</td>"
                    + " </tr>";
            strBody += "<tr>"
                    + " <td width='56%' style=" + strSubHeaderStyle + ">Status</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till last month</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till current month</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Total Spending (In Rs Crores)</td>"
                    + " <td align='right'>" + FormatString(decCSRTotalAmountLM) + "</td>"
                    + " <td align='right'>" + FormatString(decCSRTotalAmountCM) + "</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Projects recommended by CSR council</td>"
                    + " <td align='right'>" + FormatString(intCSRcouncilLM) + "</td>"
                    + " <td align='right'>" + FormatString(intCSRcouncilCM) + "</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>No. of recommended CSR projects undertaken by corporates</td>"
                    + " <td align='right'>" + FormatString(intCSRCorporateLM) + "</td>"
                    + " <td align='right'>" + FormatString(intCSRCorporateCM) + "</td>"
                    + " </tr>";
        }

      ////// CSR District Wise
      if (intCSRRecProjDistrictCM != null)
        {
            strBody += "<tr>"
                    + " <td width='100%' colspan='3' style=" + strMainHeadingStyle + ">CSR Spend- Cumulative Status</td>"
                    + " </tr>";
            strBody += "<tr>"
                    + " <td width='56%' style=" + strSubHeaderStyle + ">Status</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till last month</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till current month</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Total no. of CSR projects undertaken by corporates in the district</td>"
                    + " <td align='right'>" + FormatString(intCSRTotalProjDistrictLM) + "</td>"
                    + " <td align='right'>" + FormatString(intCSRTotalProjDistrictCM) + "</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Total no. of recommended CSR projects in the district</td>"
                    + " <td align='right'>" + FormatString(intCSRRecProjDistrictLM) + "</td>"
                    + " <td align='right'>" + FormatString(intCSRRecProjDistrictCM) + "</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>No. of recommended CSR projects undertaken by corporates in the district</td>"
                    + " <td align='right'>" + FormatString(intCSRUnderTakenProjDistrictLM) + "</td>"
                    + " <td align='right'>" + FormatString(intCSRUnderTakenProjDistrictCM) + "</td>"
                    + " </tr>";
        }

        strBody += "</table><br/>";
    }

    ///// HTML Table for Department Wise Approvals (Service Information)
    private void CreateServiceHtmlTable()
    {
        strBody += "<table rules='all' border='1' cellpadding='0' cellspacing='0' width ='95%' bordercolor='Gray' style=" + strStyle + ">";

        strBody += "<tr><td colspan='12' style=" + strMainHeadingStyle + ">Applications for Departmental Approvals - Cumulative Status<span style='color:red'>*</span></td></tr>";

        strBody += "<tr>"
                + "<th width='16%' colspan='2' style=" + strHeaderStyle + ">Received</th>"
                + "<th width='16%' colspan='2' style=" + strHeaderStyle + ">Approved</th>"
                + "<th width='16%' colspan='2' style=" + strHeaderStyle + ">Pending</th> "
                + "<th width='16%' colspan='2' style=" + strHeaderStyle + ">Rejected</th>"
                + "<th width='16%' colspan='2' style=" + strHeaderStyle + ">Query In Progress</th>"
                + "<th colspan='2' style=" + strHeaderStyle + ">Past ORTPSA timelines</th>"
                + "</tr>";

        strBody += "<tr>"
                + "<th width='8%' style=" + strSubHeaderStyle + ">Till last month</th>"
                + "<th width='8%' style=" + strSubHeaderStyle + ">Till current month</th>"
                + "<th width='8%' style=" + strSubHeaderStyle + ">Till last month</th>"
                + "<th width='8%' style=" + strSubHeaderStyle + ">Till current month</th>"
                + "<th width='8%' style=" + strSubHeaderStyle + ">Till last month</th>"
                + "<th width='8%' style=" + strSubHeaderStyle + ">Till current month</th>"
                + "<th width='8%' style=" + strSubHeaderStyle + ">Till last month</th>"
                + "<th width='8%' style=" + strSubHeaderStyle + ">Till current month</th>"
                + "<th width='8%' style=" + strSubHeaderStyle + ">Till last month</th>"
                + "<th width='8%' style=" + strSubHeaderStyle + ">Till current month</th>"
                + "<th style=" + strSubHeaderStyle + ">Till last month</th>"
                + "<th style=" + strSubHeaderStyle + ">Till current month</th>"
                + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td align='right'>" + FormatString(intDeptReceivedLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptReceivedCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptApprovedLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptApprovedCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptPendingLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptPendingCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptRejectedLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptRejectedCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptQueryRaisedLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptQueryRaisedCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptOrtpsaLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptOrtpsaCM) + "</td>"
                + "</tr>";

        strBody += "</table><br/>";
    }

    ///// HTML Table for Department Wise Approvals (Only in Case of PS Labour)
    private void CreateServiceHtmlTablePsLabour()
    {
        strBody += "<table rules='all' border='1' cellpadding='0' cellspacing='0' width ='85%' bordercolor='Gray' style=" + strStyle + ">";

        strBody += "<tr>"
                + "<td colspan='14' style=" + strMainHeadingStyle + ">Applications for Departmental Approvals - Cumulative Status <span style='font-size:10px;color:red'>*</span> </td>"
                + "</tr>";

        strBody += "<tr>"
                + "<th width='15%' colspan='2' rowspan='2' style=" + strHeaderStyle + ">Received</th>"
                + "<th width='15%' colspan='2' rowspan='2' style=" + strHeaderStyle + ">Approved</th>"
                + "<th width='15%' colspan='2' rowspan='2' style=" + strHeaderStyle + ">Pending</th>"
                + "<th width='15%' colspan='2' rowspan='2' style=" + strHeaderStyle + ">Rejected</th>"
                + "<th width='15%' colspan='2' rowspan='2' style=" + strHeaderStyle + ">Query In Progress</th>"
                + "<th width='25%' colspan='4' style=" + strHeaderStyle + ">Past ORTPSA Timelines</th> "
                + "</tr>";

        strBody += "<tr>"
               + "<td colspan='2' style=" + strHeaderStyle + ">Directorate of Labour</td>"
               + "<td colspan='2' style=" + strHeaderStyle + ">Directorate of F & B</td>"
               + "</tr>";

        strBody += "<tr>"
               + "<th width='8%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='8%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th width='8%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='8%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th width='8%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='8%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th width='8%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='8%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th width='8%' style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th width='8%' style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th style=" + strSubHeaderStyle + ">Till current month</th>"
               + "<th style=" + strSubHeaderStyle + ">Till last month</th>"
               + "<th style=" + strSubHeaderStyle + ">Till current month</th>"
               + "</tr>";

        strBody += "<tr style=" + strTrHeight + ">"
                + "<td align='right'>" + FormatString(intDeptReceivedLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptReceivedCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptApprovedLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptApprovedCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptPendingLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptPendingCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptRejectedLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptRejectedCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptQueryRaisedLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptQueryRaisedCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptOrtpsaLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptOrtpsaCM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptOrtpsaFactoryLM) + "</td>"
                + "<td align='right'>" + FormatString(intDeptOrtpsaFactoryCM) + "</td>"
                + "</tr>";

        strBody += "</table><br/>";
    }


    ////// BEGIN State Project Monitoring Group (ALL and Department) ADDED BY MANOJ KUMAR BEHERA 12.09.2019

    private void CreateSPMGStateLevelHtmlTable()
    {
        strBody += "<table rules='all' border='1' cellpadding='0' cellspacing='0' width ='80%' bordercolor='Gray' style=" + strStyle + ">";
        if (intSPMGPendingCM != null)
        {
            strBody += "<tr>"
                    + " <td width='100%' colspan='3' style=" + strMainHeadingStyle + ">State Project Monitoring Group - Cumulative Status</td>"
                    + " </tr>";
            strBody += "<tr>"
                    + " <td width='56%' style=" + strSubHeaderStyle + ">Status</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till last month</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till current month</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Issues Pending</td>"
                    + " <td align='right'>" + FormatString(intSPMGPendingLM) + "</td>"
                    + " <td align='right'>" + FormatString(intSPMGPendingCM) + "</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Issues Pending more than 30 Days</td>"
                    + " <td align='right'>" + FormatString(intSPMGMore30DaysLM) + "</td>"
                    + " <td align='right'>" + FormatString(intSPMGMore30DaysCM) + "</td>"
                    + " </tr>";
        }
        strBody += "</table><br/>";
    }

    ////// END State Project Monitoring Group (ALL and Department) ADDED BY MANOJ KUMAR BEHERA 12.09.2019



    ////// BEGIN State Project Monitoring Group (ALL and Department) FOR DISTRICT ADDED BY MANOJ KUMAR BEHERA 12.09.2019

    private void CreateSPMGDistrictLevelHtmlTable()
    {
        strBody += "<table rules='all' border='1' cellpadding='0' cellspacing='0' width ='80%' bordercolor='Gray' style=" + strStyle + ">";
        if (intdistSPMGPendingCM != null)
        {
            strBody += "<tr>"
                    + " <td width='100%' colspan='3' style=" + strMainHeadingStyle + ">State Project Monitoring Group - Cumulative Status</td>"
                    + " </tr>";
            strBody += "<tr>"
                    + " <td width='56%' style=" + strSubHeaderStyle + ">Status</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till last month</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till current month</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Issues Pending</td>"
                    + " <td align='right'>" + FormatString(intdistSPMGPendingLM) + "</td>"
                    + " <td align='right'>" + FormatString(intdistSPMGPendingCM) + "</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Issues Pending more than 30 Days</td>"
                    + " <td align='right'>" + FormatString(intdistSPMGMore30DaysLM) + "</td>"
                    + " <td align='right'>" + FormatString(intdistSPMGMore30DaysCM) + "</td>"
                    + " </tr>";

        }
        strBody += "</table><br/>";
    }

    ////// END State Project Monitoring Group (ALL and Department) FOR DISTRICT ADDED BY MANOJ KUMAR BEHERA 12.09.2019




    ////// BEGIN State Project Monitoring Group (ALL and Department) FOR BOTH STATE AND DISTRICT ADDED BY MANOJ KUMAR BEHERA 12.09.2019

    private void CreateSPMGStateandDistrictLevelHtmlTable()
    {
        strBody += "<table rules='all' border='1' cellpadding='0' cellspacing='0' width ='100%' bordercolor='Gray' style=" + strStyle + ">";
        if (intSPMGPendingCM != null && intdistSPMGPendingCM != null)
        {
            strBody += "<tr>"
                    + " <td width='100%' colspan='3' style=" + strMainHeadingStyle + ">State Project Monitoring Group - Cumulative Status</td>"
                    + " </tr>";
            strBody += "<tr>"
                    + " <td width='30%' style=" + strSubHeaderStyle + ">Status</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till last month</td>"
                    + " <td width='22%' style=" + strSubHeaderStyle + ">Till current month</td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Issues Pending</td>"
                    + " <td align='right'><table style='font-family:Verdana;font-size:12px;'border='1' width='100%'><tr><td>State Level</td><td>District Level</td></tr><tr><td>" + FormatString(intSPMGPendingLM) + "</td><td>" + FormatString(intdistSPMGPendingLM) + "</td></tr></table></td>"
                    + " <td align='right'><table style='font-family:Verdana;font-size:12px;'border='1' width='100%'><tr><td>State Level</td><td>District Level</td></tr><tr><td>" + FormatString(intSPMGPendingCM) + "</td><td>" + FormatString(intdistSPMGPendingCM) + "</td></tr></table></td>"
                    + " </tr>";
            strBody += "<tr style=" + strTrHeight + ">"
                    + " <td>Issues Pending more than 30 Days</td>"
                    + " <td align='right'><table style='font-family:Verdana;font-size:12px;'border='1' width='100%'><tr><td>State Level</td><td>District Level</td></tr><tr><td>" + FormatString(intSPMGMore30DaysLM) + "</td><td>" + FormatString(intdistSPMGMore30DaysLM) + "</td></tr></table></td>"
                    + " <td align='right'><table style='font-family:Verdana;font-size:12px;'border='1' width='100%'><tr><td>State Level</td><td>District Level</td></tr><tr><td>" + FormatString(intSPMGMore30DaysCM) + "</td><td>" + FormatString(intdistSPMGMore30DaysCM) + "</td></tr></table></td>"
                    + " </tr>";

        }
        strBody += "</table><br/>";
    }


    ////// END State Project Monitoring Group (ALL and Department) FOR BOTH STATE AND DISTRICT ADDED BY MANOJ KUMAR BEHERA 12.09.2019

    #endregion

    ///// Convert Decimal value to Comma Separted Format (Indian Style)
    private string FormatString(object strValue)
    {
        if (strValue != null)
        {
            if (strValue.ToString().Contains('.'))
            {
                if (Convert.ToDecimal(strValue) > 0)
                {
                    strValue = string.Format(CultureInfo.GetCultureInfo("hi-IN"), "{0:#,#.00}", Convert.ToDecimal(strValue));
                }
            }
            else
            {
                if (Convert.ToInt64(strValue) > 0)
                {
                    strValue = string.Format(CultureInfo.GetCultureInfo("hi-IN"), "{0:#,#}", Convert.ToInt64(strValue));
                }
            }
        }
        return Convert.ToString(strValue);
    }
    ///// Get Date Suffix
    private string GetSuffix(string day)
    {
        string suffix = "th";

        if (int.Parse(day) < 11 || int.Parse(day) > 20)
        {
            day = day.ToCharArray()[day.ToCharArray().Length - 1].ToString();
            switch (day)
            {
                case "1":
                    suffix = "st";
                    break;
                case "2":
                    suffix = "nd";
                    break;
                case "3":
                    suffix = "rd";
                    break;
            }
        }

        return suffix;
    }

    ///// Function Used to get Counts
    #region FunctionUsedForGetCounts

    #region GlobalVariables

    /*----------------------------------*/
    /////// PEAL Variables (State)

    int intPealAppliedStateCM = 0;
    int intPealApprovedStateCM = 0;
    int intPealRejectedStateCM = 0;
    int intPealDeferredStateCM = 0;
    int intPealQueryRaiseStateCM = 0;
    int intPealPendingStateCM = 0;
    int intPealOrtpsaStateCM = 0;

    int intPealAppliedStateLM = 0;
    int intPealApprovedStateLM = 0;
    int intPealRejectedStateLM = 0;
    int intPealDeferredStateLM = 0;
    int intPealQueryRaiseStateLM = 0;
    int intPealPendingStateLM = 0;
    int intPealOrtpsaStateLM = 0;

    /*----------------------------------*/
    /////// PEAL Variables (District)

    int intPealAppliedDistCM = 0;
    int intPealApprovedDistCM = 0;
    int intPealRejectedDistCM = 0;
    int intPealDeferredDistCM = 0;
    int intPealQueryRaiseDistCM = 0;
    int intPealPendingDistCM = 0;
    int intPealOrtpsaDistCM = 0;

    int intPealAppliedDistLM = 0;
    int intPealApprovedDistLM = 0;
    int intPealRejectedDistLM = 0;
    int intPealDeferredDistLM = 0;
    int intPealQueryRaiseDistLM = 0;
    int intPealPendingDistLM = 0;
    int intPealOrtpsaDistLM = 0;

    /*----------------------------------*/
    /////// PEAL Variables (IT)

    int intPealAppliedITCM = 0;
    int intPealApprovedITCM = 0;
    int intPealRejectedITCM = 0;
    int intPealDeferredITCM = 0;
    int intPealQueryRaiseITCM = 0;
    int intPealPendingITCM = 0;
    int intPealOrtpsaITCM = 0;

    int intPealAppliedITLM = 0;
    int intPealApprovedITLM = 0;
    int intPealRejectedITLM = 0;
    int intPealDeferredITLM = 0;
    int intPealQueryRaiseITLM = 0;
    int intPealPendingITLM = 0;
    int intPealOrtpsaITLM = 0;

    /*----------------------------------*/
    /////// PEAL Variables (Tourism)

    int intPealAppliedTourismCM = 0;
    int intPealApprovedTourismCM = 0;
    int intPealRejectedTourismCM = 0;
    int intPealDeferredTourismCM = 0;
    int intPealQueryRaiseTourismCM = 0;
    int intPealPendingTourismCM = 0;
    int intPealOrtpsaTourismCM = 0;

    int intPealAppliedTourismLM = 0;
    int intPealApprovedTourismLM = 0;
    int intPealRejectedTourismLM = 0;
    int intPealDeferredTourismLM = 0;
    int intPealQueryRaiseTourismLM = 0;
    int intPealPendingTourismLM = 0;
    int intPealOrtpsaTourismLM = 0;

    /*----------------------------------*/
    /////// Investment Fragmented 

    decimal decTotalInvestStateCM = 0;
    decimal decTotalInvestDistrictCM = 0;
    decimal decTotalInvestITCM = 0;
    decimal decTotalInvestTourismCM = 0;

    decimal decTotalInvestStateLM = 0;
    decimal decTotalInvestDistrictLM = 0;
    decimal decTotalInvestITLM = 0;
    decimal decTotalInvestTourismLM = 0;

    /*----------------------------------*/
    /////// Employment Fragmented  

    int intTotalEmpStateCM = 0;
    int intTotalEmpDistrictCM = 0;
    int intTotalEmpITCM = 0;
    int intTotalEmpTourismCM = 0;

    int intTotalEmpStateLM = 0;
    int intTotalEmpDistrictLM = 0;
    int intTotalEmpITLM = 0;
    int intTotalEmpTourismLM = 0;

    /*----------------------------------*/

    int intTotalPealApplied = 0;
    int intTotalPealApproved = 0;
    int intTotalPealRejected = 0;
    int intTotalPealQueryRaise = 0;
    int intTotalPealPending = 0;
    int intTotalPealOrtpsa = 0;

    /*----------------------------------*/
    /////// Land Details Variables

    int intLandAssesmentCM = 0;
    int intLandProposalIdcoCM = 0;
    int intLandAllotByIdcoCM = 0;
    decimal decLandAreaAllotedCM = 0;
    int intLandOrtpsaExceedCM = 0;

    int intLandAssesmentLM = 0;
    int intLandProposalIdcoLM = 0;
    int intLandAllotByIdcoLM = 0;
    decimal decLandAreaAllotedLM = 0;
    int intLandOrtpsaExceedLM = 0;

    /*----------------------------------*/
    /////// Departments Details Variables (Internal Service)

    int? intDeptReceivedCM = null;
    int? intDeptApprovedCM = null;
    int? intDeptPendingCM = null;
    int? intDeptRejectedCM = null;
    int? intDeptQueryRaisedCM = null;
    int? intDeptOrtpsaCM = null;
    int? intDeptOrtpsaFactoryCM = null;

    int? intDeptReceivedLM = null;
    int? intDeptApprovedLM = null;
    int? intDeptPendingLM = null;
    int? intDeptRejectedLM = null;
    int? intDeptQueryRaisedLM = null;
    int? intDeptOrtpsaLM = null;
    int? intDeptOrtpsaFactoryLM = null;

    /*----------------------------------*/
    /////// Incentive Variable

    int? intInctPendingCM = null;
    int? intInctOrtpsaCM = null;
    int? intInctPendingLM = null;
    int? intInctOrtpsaLM = null;

    /*----------------------------------*/

    decimal decInvestment = 0;
    int intEmployment = 0;
    //int? intORTPSA = null;

    /*----------------------------------*/
    /////// SPMG Variables

    int? intSPMGPendingCM = null;
    int? intSPMGMore30DaysCM = null;
    int? intSPMGPendingLM = null;
    int? intSPMGMore30DaysLM = null;

    /////// The below variables are used to store SPMG details for all 

    int? intTempSPMGPendingCM = null;
    int? intTempSPMGMore30DaysCM = null;
    int? intTempSPMGPendingLM = null;
    int? intTempSPMGMore30DaysLM = null;

    /////// The below variables are used to store SPMG District Level variable ADDDED BY MANOJ KUMAR BEHERA 11.09.2019

    int? intdistSPMGPendingCM = null;
    int? intdistSPMGMore30DaysCM = null;
    int? intdistSPMGPendingLM = null;
    int? intdistSPMGMore30DaysLM = null;


    /////// The below variables are used to store SPMG District Level details for all  ADDDED BY MANOJ KUMAR BEHERA 11.09.2019

    int? intdistTempSPMGPendingCM = null;
    int? intdistTempSPMGMore30DaysCM = null;
    int? intdistTempSPMGPendingLM = null;
    int? intdistTempSPMGMore30DaysLM = null;





    /*----------------------------------*/
    /////// Central Inspection Framework Variables

    int? intCIFPendingInspCM = null;
    int? intCIFInspNotUpdCM = null;

    int? intCIFPendingInspLM = null;
    int? intCIFInspNotUpdLM = null;

    /*----------------------------------*/
    /////// CSR Variables

    decimal? decCSRTotalAmountCM = null;
    int? intCSRcouncilCM = null;
    int? intCSRCorporateCM = null;

    decimal? decCSRTotalAmountLM = null;
    int? intCSRcouncilLM = null;
    int? intCSRCorporateLM = null;

    int? intCSRRecProjDistrictCM = null;
    int? intCSRTotalProjDistrictCM = null;
    int? intCSRUnderTakenProjDistrictCM = null;

    int? intCSRRecProjDistrictLM = null;
    int? intCSRTotalProjDistrictLM = null;
    int? intCSRUnderTakenProjDistrictLM = null;

    /*----------------------------------*/
    /////// APAA Variables

    int? intAppaPendingCM = null;
    int? intAppa30DaysCM = null;

    int? intAppaPendingLM = null;
    int? intAppa30DaysLM = null;

    /*----------------------------------*/

    //int? intPassedTimeLine = null;
    int intDesignationId = 0;
    int intDistrictId = 0;


    //int? intApaaPendingDIC = null;
    //int? intApaa30DaysDIC = null;

    #endregion

    ///// Application Status (State Level)
    private void FillProposalDetailsState()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = getPEALCounts(1, 0, 0, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                intPealAppliedStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intAppliedCurrent"]);
                intPealApprovedStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intApprovedCurrent"]);
                intPealRejectedStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intRejectedCurrent"]);
                intPealDeferredStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intDeferredCurrent"]);
                intPealQueryRaiseStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intQRaiseTotalCurrent"]);
                intPealPendingStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intUnderEvaluationCurrent"]);
                intPealOrtpsaStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intORTPSACrossedCurrent"]);
                decTotalInvestStateCM = Convert.ToDecimal(ds.Tables[0].Rows[0]["decTotalInvestCurrent"]);
                intTotalEmpStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intTotalEmpCurrent"]);

                intPealAppliedStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intAppliedLast"]);
                intPealApprovedStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intApprovedLast"]);
                intPealRejectedStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intRejectedLast"]);
                intPealDeferredStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intDeferredLast"]);
                intPealQueryRaiseStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intQRaiseTotalLast"]);
                intPealPendingStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intUnderEvaluationLast"]);
                intPealOrtpsaStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intORTPSACrossedLast"]);
                decTotalInvestStateLM = Convert.ToDecimal(ds.Tables[0].Rows[0]["decTotalInvestLast"]);
                intTotalEmpStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intTotalEmpLast"]);
            }
            else
            {
                intPealAppliedStateCM = 0;
                intPealApprovedStateCM = 0;
                intPealRejectedStateCM = 0;
                intPealQueryRaiseStateCM = 0;
                intPealPendingStateCM = 0;
                intPealDeferredStateCM = 0;
                intPealOrtpsaStateCM = 0;

                intPealAppliedStateLM = 0;
                intPealApprovedStateLM = 0;
                intPealRejectedStateLM = 0;
                intPealQueryRaiseStateLM = 0;
                intPealPendingStateLM = 0;
                intPealDeferredStateLM = 0;
                intPealOrtpsaStateLM = 0;
            }

            intTotalPealApplied += intPealAppliedStateCM;
            intTotalPealApproved += intPealApprovedStateCM;
            intTotalPealRejected += intPealRejectedStateCM;
            intTotalPealQueryRaise += intPealQueryRaiseStateCM;
            intTotalPealPending += intPealPendingStateCM;
            intTotalPealOrtpsa += intPealOrtpsaStateCM;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }
        finally
        {
            ds = null;
        }

        //try
        //{
        //    objSWP = new SWPDashboard();
        //    objserviceDashboard = new DashboardBusinessLayer();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "PB";
        //    objSWP.intUserid = intUserId; //Convert.ToInt32(Session["Userid"]);
        //    string PealQuareter = "0";// string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
        //    string PealDistrict = intDistrictId.ToString();// string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
        //    objSWP.strFinacialYear = "";// strFY;// ddlPealYear.SelectedValue.ToString();
        //    objSWP.intType = Convert.ToInt16(PealQuareter);
        //    objSWP.intDistrictid = Convert.ToInt16(PealDistrict);
        //    objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //    if (objswpDashboardList.Count > 0)
        //    {
        //        intPealAppliedStateCM = Convert.ToInt32(objswpDashboardList[0].strApplied);
        //        intPealApprovedStateCM = Convert.ToInt32(objswpDashboardList[0].strApproved);
        //        intPealRejectedStateCM = Convert.ToInt32(objswpDashboardList[0].strRejected);
        //        intPealDeferredStateCM = Convert.ToInt32(objswpDashboardList[0].strDeferred);
        //        intPealQueryRaiseStateCM = Convert.ToInt32(objswpDashboardList[0].QraiseTotal);
        //        intPealPendingStateCM = Convert.ToInt32(objswpDashboardList[0].strUnderEvaltion);
        //        intPealOrtpsaStateCM = Convert.ToInt32(objswpDashboardList[0].strPealOrtpsaCrossedState);
        //    }
        //    else
        //    {
        //        intPealAppliedStateCM = 0;
        //        intPealApprovedStateCM = 0;
        //        intPealRejectedStateCM = 0;
        //        intPealQueryRaiseStateCM = 0;
        //        intPealPendingStateCM = 0;
        //        intPealDeferredStateCM = 0;
        //        intPealOrtpsaStateCM = 0;
        //    }

        //    intTotalPealApplied += intPealAppliedStateCM;
        //    intTotalPealApproved += intPealApprovedStateCM;
        //    intTotalPealRejected += intPealRejectedStateCM;
        //    intTotalPealQueryRaise += intPealQueryRaiseStateCM;
        //    intTotalPealPending += intPealPendingStateCM;
        //    intTotalPealOrtpsa += intPealOrtpsaStateCM;
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    ///// Application Status (District Level)
    private void FillProposalDistrictLevel()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = getPEALCounts(2, 0, 0, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                intPealAppliedDistCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intAppliedCurrent"]);
                intPealApprovedDistCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intApprovedCurrent"]);
                intPealRejectedDistCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intRejectedCurrent"]);
                intPealDeferredDistCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intDeferredCurrent"]);
                intPealQueryRaiseDistCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intQRaiseTotalCurrent"]);
                intPealPendingDistCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intUnderEvaluationCurrent"]);
                intPealOrtpsaDistCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intORTPSACrossedCurrent"]);
                decTotalInvestDistrictCM = Convert.ToDecimal(ds.Tables[0].Rows[0]["decTotalInvestCurrent"]);
                intTotalEmpDistrictCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intTotalEmpCurrent"]);

                intPealAppliedDistLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intAppliedLast"]);
                intPealApprovedDistLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intApprovedLast"]);
                intPealRejectedDistLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intRejectedLast"]);
                intPealDeferredDistLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intDeferredLast"]);
                intPealQueryRaiseDistLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intQRaiseTotalLast"]);
                intPealPendingDistLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intUnderEvaluationLast"]);
                intPealOrtpsaDistLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intORTPSACrossedLast"]);
                decTotalInvestDistrictLM = Convert.ToDecimal(ds.Tables[0].Rows[0]["decTotalInvestLast"]);
                intTotalEmpDistrictLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intTotalEmpLast"]);
            }
            else
            {
                intPealAppliedDistCM = 0;
                intPealApprovedDistCM = 0;
                intPealRejectedDistCM = 0;
                intPealDeferredDistCM = 0;
                intPealQueryRaiseDistCM = 0;
                intPealPendingDistCM = 0;
                intPealOrtpsaDistCM = 0;

                intPealAppliedDistLM = 0;
                intPealApprovedDistLM = 0;
                intPealRejectedDistLM = 0;
                intPealDeferredDistLM = 0;
                intPealQueryRaiseDistLM = 0;
                intPealPendingDistLM = 0;
                intPealOrtpsaDistLM = 0;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }
        finally
        {
            ds = null;
        }



        //try
        //{
        //    objSWP = new SWPDashboard();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "PDD";
        //    string PealDistrict = "0";//  string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();

        //    objSWP.intQuarter = 0;
        //    objSWP.strFinacialYear = strFY;// ddlPealYear.SelectedValue.ToString();
        //    objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
        //    objSWP.intUserid = intUserId;// Convert.ToInt32(Session["Userid"]);
        //    objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //    if (objswpDashboardList.Count > 0)
        //    {
        //        intPealAppliedDistCM = Convert.ToInt32(objswpDashboardList[0].strDistApplied);
        //        intPealApprovedDistCM = Convert.ToInt32(objswpDashboardList[0].strDistApproved);
        //        intPealRejectedDistCM = Convert.ToInt32(objswpDashboardList[0].strDistRejected);
        //        intPealDeferredDistCM = Convert.ToInt32(objswpDashboardList[0].strDistDeferred);
        //        intPealQueryRaiseDistCM = Convert.ToInt32(objswpDashboardList[0].QraiseTotal);
        //        intPealPendingDistCM = Convert.ToInt32(objswpDashboardList[0].strDistUnderEvaltion);
        //        intPealOrtpsaDistCM = Convert.ToInt32(objswpDashboardList[0].strPealOrtpsaCrossedDist);
        //    }
        //    else
        //    {
        //        intPealAppliedDistCM = 0;
        //        intPealApprovedDistCM = 0;
        //        intPealRejectedDistCM = 0;
        //        intPealDeferredDistCM = 0;
        //        intPealQueryRaiseDistCM = 0;
        //        intPealPendingDistCM = 0;
        //        intPealOrtpsaDistCM = 0;
        //    }

        //    intTotalPealApplied += intPealAppliedDistCM;
        //    intTotalPealApproved += intPealApprovedDistCM;
        //    intTotalPealRejected += intPealRejectedDistCM;
        //    intTotalPealQueryRaise += intPealQueryRaiseDistCM;
        //    intTotalPealPending += intPealPendingDistCM;
        //    intTotalPealOrtpsa += intPealOrtpsaDistCM;
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    ///// Application Status (Special IT Level)
    private void FillProposalITLevel()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = getPEALCounts(2, 10, 0, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                intPealAppliedITCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intAppliedCurrent"]);
                intPealApprovedITCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intApprovedCurrent"]);
                intPealRejectedITCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intRejectedCurrent"]);
                intPealDeferredITCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intDeferredCurrent"]);
                intPealQueryRaiseITCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intQRaiseTotalCurrent"]);
                intPealPendingITCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intUnderEvaluationCurrent"]);
                intPealOrtpsaITCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intORTPSACrossedCurrent"]);
                decTotalInvestITCM = Convert.ToDecimal(ds.Tables[0].Rows[0]["decTotalInvestCurrent"]);
                intTotalEmpITCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intTotalEmpCurrent"]);

                intPealAppliedITLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intAppliedLast"]);
                intPealApprovedITLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intApprovedLast"]);
                intPealRejectedITLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intRejectedLast"]);
                intPealDeferredITLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intDeferredLast"]);
                intPealQueryRaiseITLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intQRaiseTotalLast"]);
                intPealPendingITLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intUnderEvaluationLast"]);
                intPealOrtpsaITLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intORTPSACrossedLast"]);
                decTotalInvestITLM = Convert.ToDecimal(ds.Tables[0].Rows[0]["decTotalInvestLast"]);
                intTotalEmpITLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intTotalEmpLast"]);
            }
            else
            {
                intPealAppliedITCM = 0;
                intPealApprovedITCM = 0;
                intPealRejectedITCM = 0;
                intPealDeferredITCM = 0;
                intPealQueryRaiseITCM = 0;
                intPealPendingITCM = 0;
                intPealOrtpsaITCM = 0;
                decTotalInvestITCM = 0;
                intTotalEmpITCM = 0;

                intPealAppliedITLM = 0;
                intPealApprovedITLM = 0;
                intPealRejectedITLM = 0;
                intPealDeferredITLM = 0;
                intPealQueryRaiseITLM = 0;
                intPealPendingITLM = 0;
                intPealOrtpsaITLM = 0;
                decTotalInvestITLM = 0;
                intTotalEmpITLM = 0;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }
        finally
        {
            ds = null;
        }


        //try
        //{
        //    SWPDashboard objSWP = new SWPDashboard();
        //    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "PSE";

        //    string PealDistrict = "0";//string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
        //    string PealYear = strFY;//string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();

        //    objSWP.intQuarter = 0;
        //    objSWP.strFinacialYear = PealYear.ToString();
        //    objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
        //    objSWP.intUserid = 0;//Convert.ToInt32(Session["Userid"]);
        //    objSWP.intSecId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdIT"]);

        //    objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //    if (objswpDashboardList.Count > 0)
        //    {
        //        intPealAppliedITCM = Convert.ToInt32(objswpDashboardList[0].strDistApplied);
        //        intPealRejectedITCM = Convert.ToInt32(objswpDashboardList[0].strDistRejected);
        //        intPealApprovedITCM = Convert.ToInt32(objswpDashboardList[0].strDistApproved);
        //        intPealDeferredITCM = Convert.ToInt32(objswpDashboardList[0].strDistDeferred);
        //        intPealQueryRaiseITCM = Convert.ToInt32(objswpDashboardList[0].QraiseTotal);
        //        intPealPendingITCM = Convert.ToInt32(objswpDashboardList[0].strDistUnderEvaltion);
        //        intPealOrtpsaITCM = Convert.ToInt32(objswpDashboardList[0].strPealOrtpsaCrossedITandTourism);
        //    }
        //    else
        //    {
        //        intPealAppliedITCM = 0;
        //        intPealApprovedITCM = 0;
        //        intPealRejectedITCM = 0;
        //        intPealDeferredITCM = 0;
        //        intPealQueryRaiseITCM = 0;
        //        intPealPendingITCM = 0;
        //        intPealOrtpsaITCM = 0;
        //    }

        //    intTotalPealApplied += intPealAppliedITCM;
        //    intTotalPealApproved += intPealApprovedITCM;
        //    intTotalPealRejected += intPealRejectedITCM;
        //    intTotalPealQueryRaise += intPealQueryRaiseITCM;
        //    intTotalPealPending += intPealPendingITCM;
        //    intTotalPealOrtpsa += intPealOrtpsaITCM;
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    ///// Application Status (Special Tourism Level)
    private void FillProposalTourismLevel()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = getPEALCounts(2, 38, 0, 0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                intPealAppliedTourismCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intAppliedCurrent"]);
                intPealApprovedTourismCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intApprovedCurrent"]);
                intPealRejectedTourismCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intRejectedCurrent"]);
                intPealDeferredTourismCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intDeferredCurrent"]);
                intPealQueryRaiseTourismCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intQRaiseTotalCurrent"]);
                intPealPendingTourismCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intUnderEvaluationCurrent"]);
                intPealOrtpsaTourismCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intORTPSACrossedCurrent"]);
                decTotalInvestTourismCM = Convert.ToDecimal(ds.Tables[0].Rows[0]["decTotalInvestCurrent"]);
                intTotalEmpTourismCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intTotalEmpCurrent"]);

                intPealAppliedTourismLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intAppliedLast"]);
                intPealApprovedTourismLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intApprovedLast"]);
                intPealRejectedTourismLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intRejectedLast"]);
                intPealDeferredTourismLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intDeferredLast"]);
                intPealQueryRaiseTourismLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intQRaiseTotalLast"]);
                intPealPendingTourismLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intUnderEvaluationLast"]);
                intPealOrtpsaTourismLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intORTPSACrossedLast"]);
                decTotalInvestTourismLM = Convert.ToDecimal(ds.Tables[0].Rows[0]["decTotalInvestCurrent"]);
                intTotalEmpTourismLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intTotalEmpCurrent"]);
            }
            else
            {
                intPealAppliedTourismCM = 0;
                intPealApprovedTourismCM = 0;
                intPealRejectedTourismCM = 0;
                intPealDeferredTourismCM = 0;
                intPealQueryRaiseTourismCM = 0;
                intPealPendingTourismCM = 0;
                intPealOrtpsaTourismCM = 0;
                decTotalInvestTourismCM = 0;
                intTotalEmpTourismCM = 0;

                intPealAppliedTourismLM = 0;
                intPealApprovedTourismLM = 0;
                intPealRejectedTourismLM = 0;
                intPealDeferredTourismLM = 0;
                intPealQueryRaiseTourismLM = 0;
                intPealPendingTourismLM = 0;
                intPealOrtpsaTourismLM = 0;
                decTotalInvestTourismLM = 0;
                intTotalEmpTourismLM = 0;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }
        finally
        {
            ds = null;
        }

        //try
        //{
        //    SWPDashboard objSWP = new SWPDashboard();
        //    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "PSE";

        //    string PealDistrict = "0";//string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
        //    string PealYear = strFY;// string.IsNullOrEmpty(ddlPealYear.SelectedValue) ? default(string) : ddlPealYear.SelectedValue.ToString();

        //    objSWP.intQuarter = 0;
        //    objSWP.strFinacialYear = PealYear.ToString();
        //    objSWP.intDistrictid = Convert.ToInt32(PealDistrict);
        //    objSWP.intUserid = 0;// Convert.ToInt32(Session["Userid"]);
        //    objSWP.intSecId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdTOURISM"]);

        //    objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //    if (objswpDashboardList.Count > 0)
        //    {
        //        intPealAppliedTourismCM = Convert.ToInt32(objswpDashboardList[0].strDistApplied);
        //        intPealApprovedTourismCM = Convert.ToInt32(objswpDashboardList[0].strDistApproved);
        //        intPealRejectedTourismCM = Convert.ToInt32(objswpDashboardList[0].strDistRejected);
        //        intPealDeferredTourismCM = Convert.ToInt32(objswpDashboardList[0].strDistDeferred);
        //        intPealQueryRaiseTourismCM = Convert.ToInt32(objswpDashboardList[0].QraiseTotal);
        //        intPealPendingTourismCM = Convert.ToInt32(objswpDashboardList[0].strDistUnderEvaltion);
        //        intPealOrtpsaTourismCM = Convert.ToInt32(objswpDashboardList[0].strPealOrtpsaCrossedITandTourism);
        //    }
        //    else
        //    {
        //        intPealAppliedTourismCM = 0;
        //        intPealApprovedTourismCM = 0;
        //        intPealRejectedTourismCM = 0;
        //        intPealDeferredTourismCM = 0;
        //        intPealQueryRaiseTourismCM = 0;
        //        intPealPendingTourismCM = 0;
        //        intPealOrtpsaTourismCM = 0;
        //    }

        //    intTotalPealApplied += intPealAppliedTourismCM;
        //    intTotalPealApproved += intPealApprovedTourismCM;
        //    intTotalPealRejected += intPealRejectedTourismCM;
        //    intTotalPealQueryRaise += intPealQueryRaiseTourismCM;
        //    intTotalPealPending += intPealPendingTourismCM;
        //    intTotalPealOrtpsa += intPealOrtpsaTourismCM;
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    ///// Application Status (DIC)
    private void FillProposalDetailsDIC()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = getPEALCounts(2, 0, intDistrictId, intUserId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                intPealAppliedStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intAppliedCurrent"]);
                intPealApprovedStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intApprovedCurrent"]);
                intPealRejectedStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intRejectedCurrent"]);
                intPealDeferredStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intDeferredCurrent"]);
                intPealQueryRaiseStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intQRaiseTotalCurrent"]);
                intPealPendingStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intUnderEvaluationCurrent"]);
                intPealOrtpsaStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intORTPSACrossedCurrent"]);
                decTotalInvestStateCM = Convert.ToDecimal(ds.Tables[0].Rows[0]["decTotalInvestCurrent"]);
                intTotalEmpStateCM = Convert.ToInt32(ds.Tables[0].Rows[0]["intTotalEmpCurrent"]);

                intPealAppliedStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intAppliedLast"]);
                intPealApprovedStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intApprovedLast"]);
                intPealRejectedStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intRejectedLast"]);
                intPealDeferredStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intDeferredLast"]);
                intPealQueryRaiseStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intQRaiseTotalLast"]);
                intPealPendingStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intUnderEvaluationLast"]);
                intPealOrtpsaStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intORTPSACrossedLast"]);
                decTotalInvestStateLM = Convert.ToDecimal(ds.Tables[0].Rows[0]["decTotalInvestLast"]);
                intTotalEmpStateLM = Convert.ToInt32(ds.Tables[0].Rows[0]["intTotalEmpLast"]);
            }
            else
            {
                intPealAppliedStateCM = 0;
                intPealApprovedStateCM = 0;
                intPealRejectedStateCM = 0;
                intPealQueryRaiseStateCM = 0;
                intPealPendingStateCM = 0;
                intPealDeferredStateCM = 0;
                intPealOrtpsaStateCM = 0;

                intPealAppliedStateLM = 0;
                intPealApprovedStateLM = 0;
                intPealRejectedStateLM = 0;
                intPealQueryRaiseStateLM = 0;
                intPealPendingStateLM = 0;
                intPealDeferredStateLM = 0;
                intPealOrtpsaStateLM = 0;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }
        finally
        {
            ds = null;
        }

        //try
        //{
        //    objSWP = new SWPDashboard();
        //    objserviceDashboard = new DashboardBusinessLayer();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "PB";
        //    objSWP.intUserid = intUserId; //Convert.ToInt32(Session["Userid"]);
        //    string PealQuareter = "0";// string.IsNullOrEmpty(ddlPealQuarter.SelectedValue) ? default(string) : ddlPealQuarter.SelectedValue.ToString();
        //    string PealDistrict = intDistrictId.ToString();// string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
        //    objSWP.strFinacialYear = "";// strFY;// ddlPealYear.SelectedValue.ToString();
        //    objSWP.intType = Convert.ToInt16(PealQuareter);
        //    objSWP.intDistrictid = Convert.ToInt16(PealDistrict);
        //    objswpDashboardList = objserviceDashboard.GetDashboardProposalDtls(objSWP).ToList();
        //    if (objswpDashboardList.Count > 0)
        //    {
        //        intPealAppliedStateCM = Convert.ToInt32(objswpDashboardList[0].strApplied);
        //        intPealApprovedStateCM = Convert.ToInt32(objswpDashboardList[0].strApproved);
        //        intPealRejectedStateCM = Convert.ToInt32(objswpDashboardList[0].strRejected);
        //        intPealDeferredStateCM = Convert.ToInt32(objswpDashboardList[0].strDeferred);
        //        intPealQueryRaiseStateCM = Convert.ToInt32(objswpDashboardList[0].QraiseTotal);
        //        intPealPendingStateCM = Convert.ToInt32(objswpDashboardList[0].strUnderEvaltion);
        //        intPealOrtpsaStateCM = Convert.ToInt32(objswpDashboardList[0].strPealOrtpsaCrossedState);
        //    }
        //    else
        //    {
        //        intPealAppliedStateCM = 0;
        //        intPealApprovedStateCM = 0;
        //        intPealRejectedStateCM = 0;
        //        intPealQueryRaiseStateCM = 0;
        //        intPealPendingStateCM = 0;
        //        intPealDeferredStateCM = 0;
        //        intPealOrtpsaStateCM = 0;
        //    }

        //    intTotalPealApplied += intPealAppliedStateCM;
        //    intTotalPealApproved += intPealApprovedStateCM;
        //    intTotalPealRejected += intPealRejectedStateCM;
        //    intTotalPealQueryRaise += intPealQueryRaiseStateCM;
        //    intTotalPealPending += intPealPendingStateCM;
        //    intTotalPealOrtpsa += intPealOrtpsaStateCM;
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    ///// Get PEAL Application Counts
    private DataSet getPEALCounts(int intProjectType, int intSectorId, int intDistrictId, int intUserId)
    {
        DataSet objds = new DataSet();
        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();

            objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "PEAL");
            objCommand.Parameters.AddWithValue("@P_INT_PROJECT_TYPE", intProjectType);
            objCommand.Parameters.AddWithValue("@P_INT_SECTOR_ID", intSectorId);
            objCommand.Parameters.AddWithValue("@P_INT_DISTRICT_ID", intDistrictId);
            objCommand.Parameters.AddWithValue("@P_INT_USER_ID", intUserId);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }

        return objds;
    }

    ///// Department Wise Approvals
    private void ViewServiceStatus()
    {
        DataSet objds = new DataSet();
        try
        {
            objds = getServiceCounts(0, intDeptId);
            if (objds.Tables[0].Rows.Count > 0)
            {
                intDeptReceivedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvAppliedCurrent"]);
                intDeptApprovedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvApprovedCurrent"]);
                intDeptPendingCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvPendingCurrent"]);
                intDeptRejectedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvRejectedCurrent"]);
                intDeptOrtpsaCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvOrtpsaCurrent"]);
                intDeptQueryRaisedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvQueryRaiseCurrent"]);

                intDeptReceivedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvAppliedLast"]);
                intDeptApprovedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvApprovedLast"]);
                intDeptPendingLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvPendingLast"]);
                intDeptRejectedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvRejectedLast"]);
                intDeptOrtpsaLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvOrtpsaLast"]);
                intDeptQueryRaisedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvQueryRaiseLast"]);
            }
            else
            {
                intDeptReceivedCM = 0;
                intDeptApprovedCM = 0;
                intDeptPendingCM = 0;
                intDeptRejectedCM = 0;
                intDeptOrtpsaCM = 0;
                intDeptQueryRaisedCM = 0;

                intDeptReceivedLM = 0;
                intDeptApprovedLM = 0;
                intDeptPendingLM = 0;
                intDeptRejectedLM = 0;
                intDeptOrtpsaLM = 0;
                intDeptQueryRaisedLM = 0;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }

        //DataSet objds = new DataSet();
        //try
        //{
        //    SqlCommand objCommand = new SqlCommand();
        //    SqlDataAdapter objDa = new SqlDataAdapter();

        //    objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
        //    objCommand.CommandType = CommandType.StoredProcedure;
        //    objCommand.Connection = conn;

        //    objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "SERVICE");
        //    objCommand.Parameters.AddWithValue("@P_INT_DISTRICT_ID", 0);

        //    objDa.SelectCommand = objCommand;
        //    objDa.Fill(objds);

        //    if (objds.Tables[0].Rows.Count > 0)
        //    {
        //        intDeptReceivedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvAppliedCurrent"]);
        //        intDeptApprovedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvApprovedCurrent"]);
        //        intDeptPendingCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvPendingCurrent"]);
        //        intDeptRejectedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvRejectedCurrent"]);
        //        intDeptOrtpsaCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvOrtpsaCurrent"]);
        //        intDeptQueryRaisedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvQueryRaiseCurrent"]);

        //        intDeptReceivedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvAppliedLast"]);
        //        intDeptApprovedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvApprovedLast"]);
        //        intDeptPendingLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvPendingLast"]);
        //        intDeptRejectedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvRejectedLast"]);
        //        intDeptOrtpsaLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvOrtpsaLast"]);
        //        intDeptQueryRaisedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvQueryRaiseLast"]);
        //    }
        //    else
        //    {
        //        intDeptReceivedCM = 0;
        //        intDeptApprovedCM = 0;
        //        intDeptPendingCM = 0;
        //        intDeptRejectedCM = 0;
        //        intDeptOrtpsaCM = 0;
        //        intDeptQueryRaisedCM = 0;

        //        intDeptReceivedLM = 0;
        //        intDeptApprovedLM = 0;
        //        intDeptPendingLM = 0;
        //        intDeptRejectedLM = 0;
        //        intDeptOrtpsaLM = 0;
        //        intDeptQueryRaisedLM = 0;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}


        //SWPDashboard objDashboard = new SWPDashboard();
        //DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        //try
        //{
        //    objDashboard.strAction = "D";
        //    //objDashboard.intInvestorId = 0;
        //    objDashboard.intDeptId = intDeptId;// Convert.ToInt32(ddldept.SelectedValue);
        //    objDashboard.intServiceId = 0;// Convert.ToInt32(ddlService.SelectedValue);
        //    List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
        //    if (objServiceStatus.Count > 0)
        //    {
        //        intDeptReceivedCM = Convert.ToInt32(objServiceStatus[0].strApplied);
        //        intDeptApprovedCM = Convert.ToInt32(objServiceStatus[0].strApproved);
        //        intDeptPendingCM = Convert.ToInt32(objServiceStatus[0].strPending);
        //        intDeptRejectedCM = Convert.ToInt32(objServiceStatus[0].strRejected);
        //        intDeptORTPSACM = Convert.ToInt32(objServiceStatus[0].intDaysPass);
        //        intDeptQueryRaisedCM = Convert.ToInt32(objServiceStatus[0].QraiseTotal);
        //    }
        //    else
        //    {
        //        intDeptReceivedCM = 0;
        //        intDeptApprovedCM = 0;
        //        intDeptPendingCM = 0;
        //        intDeptRejectedCM = 0;
        //        intDeptQueryRaisedCM = 0;
        //        intDeptORTPSACM = 0;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    ///// Department Wise Approvals (DIC)
    private void ViewServiceStatusDIC()
    {
        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();

            objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "SERVICEDEPT");
            objCommand.Parameters.AddWithValue("@P_INT_DISTRICT_ID", intDistrictId);
            objCommand.Parameters.AddWithValue("@P_INT_USER_ID", intUserId);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Tables[0].Rows.Count > 0)
            {
                intDeptReceivedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_APPLY_CM"]);
                intDeptApprovedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_APPROVED_CM"]);
                intDeptPendingCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_PENDING_CM"]);
                intDeptRejectedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_REJECTED_CM"]);
                intDeptOrtpsaCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_ORTPSA_TIMELINE_CM"]);
                intDeptQueryRaisedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_QUERY_RAISE_CM"]);

                intDeptReceivedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_APPLY_LM"]);
                intDeptApprovedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_APPROVED_LM"]);
                intDeptPendingLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_PENDING_LM"]);
                intDeptRejectedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_REJECTED_LM"]);
                intDeptOrtpsaLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_ORTPSA_TIMELINE_LM"]);
                intDeptQueryRaisedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_QUERY_RAISE_LM"]);
            }
            else
            {
                intDeptReceivedCM = 0;
                intDeptApprovedCM = 0;
                intDeptPendingCM = 0;
                intDeptRejectedCM = 0;
                intDeptQueryRaisedCM = 0;
                intDeptOrtpsaCM = 0;

                intDeptReceivedLM = 0;
                intDeptApprovedLM = 0;
                intDeptPendingLM = 0;
                intDeptRejectedLM = 0;
                intDeptOrtpsaLM = 0;
                intDeptQueryRaisedLM = 0;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }


        //DataSet objds = new DataSet();
        //try
        //{
        //    objds = getServiceCounts(intDistrictId, 0);
        //    if (objds.Tables[0].Rows.Count > 0)
        //    {
        //        intDeptReceivedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvAppliedCurrent"]);
        //        intDeptApprovedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvApprovedCurrent"]);
        //        intDeptPendingCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvPendingCurrent"]);
        //        intDeptRejectedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvRejectedCurrent"]);
        //        intDeptOrtpsaCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvOrtpsaCurrent"]);
        //        intDeptQueryRaisedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvQueryRaiseCurrent"]);

        //        intDeptReceivedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvAppliedLast"]);
        //        intDeptApprovedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvApprovedLast"]);
        //        intDeptPendingLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvPendingLast"]);
        //        intDeptRejectedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvRejectedLast"]);
        //        intDeptOrtpsaLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvOrtpsaLast"]);
        //        intDeptQueryRaisedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intSrvQueryRaiseLast"]);
        //    }
        //    else
        //    {
        //        intDeptReceivedCM = 0;
        //        intDeptApprovedCM = 0;
        //        intDeptPendingCM = 0;
        //        intDeptRejectedCM = 0;
        //        intDeptOrtpsaCM = 0;
        //        intDeptQueryRaisedCM = 0;

        //        intDeptReceivedLM = 0;
        //        intDeptApprovedLM = 0;
        //        intDeptPendingLM = 0;
        //        intDeptRejectedLM = 0;
        //        intDeptOrtpsaLM = 0;
        //        intDeptQueryRaisedLM = 0;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}


        /*------------------------------------------------------------------------------------------*/


        //SWPDashboard objDashboard = new SWPDashboard();
        //DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        //try
        //{
        //    objDashboard.strAction = "DGM";
        //    objDashboard.intDistrictid = intDistrictId; //Convert.ToInt32(Session["Pealuserid"]);             
        //    objDashboard.strFinacialYear = "2017-18";// ddlserviceyear.SelectedValue;
        //    objDashboard.intMonthId = 0;// Convert.ToInt32(ddlServcMonth.SelectedValue);
        //    List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
        //    if (objServiceStatus.Count > 0)
        //    {
        //        intDeptReceived = Convert.ToInt32(objServiceStatus[0].strApplied);
        //        intDeptApproved = Convert.ToInt32(objServiceStatus[0].strApproved);
        //        intDeptPending = Convert.ToInt32(objServiceStatus[0].strPending);
        //        intDeptRejected = Convert.ToInt32(objServiceStatus[0].strRejected);
        //        intDeptORTPSA = Convert.ToInt32(objServiceStatus[0].intDaysPass);
        //        intDeptQueryRaised = Convert.ToInt32(objServiceStatus[0].QraiseTotal);

        //        //hdApplied.InnerHtml = objServiceStatus[0].strApplied.ToString();
        //        //hdApprove.InnerHtml = objServiceStatus[0].strApproved.ToString();
        //        //hdPending.InnerHtml = objServiceStatus[0].strPending.ToString();
        //        //hdReject.InnerHtml = objServiceStatus[0].strRejected.ToString();
        //        ////hdExceed.InnerHtml = objServiceStatus[0].intDaysPass.ToString();
        //        //hdnqueryRaised.InnerHtml = objServiceStatus[0].QraiseTotal.ToString();
        //    }
        //    else
        //    {
        //        intDeptReceived = 0;
        //        intDeptApproved = 0;
        //        intDeptPending = 0;
        //        intDeptRejected = 0;
        //        intDeptQueryRaised = 0;
        //        intDeptORTPSA = 0;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    ///// Get Service Counts
    private DataSet getServiceCounts(int intDistrictId, int intDeptId)
    {
        DataSet objds = new DataSet();
        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();

            objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "SERVICE");
            objCommand.Parameters.AddWithValue("@P_INT_DISTRICT_ID", intDistrictId);
            objCommand.Parameters.AddWithValue("@P_INT_DEPT_ID", intDeptId);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }

        return objds;
    }

    ///// Department Wise Approvals (ORTPSA Staus of PSLABOUR (pslabour and psfb)
    private void ViewORTPSAStatus()
    {
        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();

            objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "PSLABOUR");
            objCommand.Parameters.AddWithValue("@P_INT_DEPT_ID", intDeptId);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Tables[0].Rows.Count > 0)
            {
                intDeptOrtpsaCM = Convert.ToInt32(objds.Tables[0].Rows[0]["Labour_Count_CM"]);
                intDeptOrtpsaFactoryCM = Convert.ToInt32(objds.Tables[0].Rows[0]["Fb_Count_CM"]);
                intDeptOrtpsaLM = Convert.ToInt32(objds.Tables[0].Rows[0]["Labour_Count_LM"]);
                intDeptOrtpsaFactoryLM = Convert.ToInt32(objds.Tables[0].Rows[0]["Fb_Count_LM"]);
            }
            else
            {
                intDeptOrtpsaCM = 0;
                intDeptOrtpsaFactoryCM = 0;
                intDeptOrtpsaLM = 0;
                intDeptOrtpsaFactoryLM = 0;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }
    }

    ///// Incentive Details
    private void IncentiveMasterBind()
    {
        DataSet objds = new DataSet();
        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();

            objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "INCT");
            objCommand.Parameters.AddWithValue("@P_INT_DISTRICT_ID", intDistrictId);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Tables[0].Rows.Count > 0)
            {
                intInctPendingCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intInctPendingCurrent"]);
                intInctOrtpsaCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intOrtpsaCurrent"]);

                intInctPendingLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intInctPendingLast"]);
                intInctOrtpsaLM = Convert.ToInt32(objds.Tables[0].Rows[0]["intOrtpsaLast"]);
            }
            else
            {
                intInctPendingCM = 0;
                intInctOrtpsaCM = 0;

                intInctPendingLM = 0;
                intInctOrtpsaLM = 0;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }


        //try
        //{
        //    objSWP = new SWPDashboard();
        //    objserviceDashboard = new DashboardBusinessLayer();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "B";
        //    // string PealQuareter = string.IsNullOrEmpty(ddlIncentive.SelectedValue) ? default(string) : ddlIncentive.SelectedValue.ToString();
        //    //string PealDistrict = string.IsNullOrEmpty(ddlPEALDistrict.SelectedValue) ? default(string) : ddlPEALDistrict.SelectedValue.ToString();
        //    //string PealYear = string.IsNullOrEmpty(ddlIncentiveYear.SelectedValue) ? default(string) : ddlIncentiveYear.SelectedValue.ToString();
        //    objSWP.intDistrictid = Convert.ToInt16(intDistrictId);
        //    objSWP.intQuarter = 0;
        //    objSWP.intYearId = 0;
        //    objSWP.strFinacialYear = strFY;  //ddlFinacialYear.SelectedValue;//Added by suroj on 26-10-17 to check finacial yr
        //    objSWP.intUserid = 0;// Convert.ToInt32(Session["Userid"]);
        //    objswpDashboardList = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP).ToList();

        //    //spIncPending.InnerText = objswpDashboardList[0].INCPENDING;

        //    intInctPendingCM = Convert.ToInt32(objswpDashboardList[0].INCPENDING);
        //    intInctOrtpsaCM = Convert.ToInt32(objswpDashboardList[0].strInctPending30Days);

        //    //strBody += "<td colspan='2' bgcolor='#e3efef'><b>Incentive Details</b></td></tr>";
        //    //strBody += "<tr><td>Pending incentives</td><td> " + objswpDashboardList[0].INCPENDING + "</td> </tr>";
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    ///// Land Allotment Details
    private void LandServiceBind()
    {
        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();

            objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "LANDALL");

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Tables[0].Rows.Count > 0)
            {
                intLandAssesmentCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_LAND_ASSESMENT_CM"]);
                intLandProposalIdcoCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_PROPOSAL_SENT_CM"]);
                intLandAllotByIdcoCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_LAND_ALLOTED_BY_IDCO_CM"]);
                decLandAreaAllotedCM = Convert.ToDecimal(objds.Tables[0].Rows[0]["DEC_AREA_ALLOTED_BY_IDCO_CM"]);
                intLandOrtpsaExceedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_ORTPSA_TIME_CM"]);

                intLandAssesmentLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_LAND_ASSESMENT_LM"]);
                intLandProposalIdcoLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_PROPOSAL_SENT_LM"]);
                intLandAllotByIdcoLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_LAND_ALLOTED_BY_IDCO_LM"]);
                decLandAreaAllotedLM = Convert.ToDecimal(objds.Tables[0].Rows[0]["DEC_AREA_ALLOTED_BY_IDCO_LM"]);
                intLandOrtpsaExceedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_ORTPSA_TIME_LM"]);
            }
            else
            {
                intLandAssesmentCM = 0;
                intLandAllotByIdcoCM = 0;
                intLandProposalIdcoCM = 0;
                decLandAreaAllotedCM = 0;
                intLandOrtpsaExceedCM = 0;

                intLandAssesmentLM = 0;
                intLandProposalIdcoLM = 0;
                intLandAllotByIdcoLM = 0;
                decLandAreaAllotedLM = 0;
                intLandOrtpsaExceedLM = 0;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }

        //try
        //{
        //    objSWP = new SWPDashboard();
        //    objserviceDashboard = new DashboardBusinessLayer();
        //    List<SWPDashboard> objswpDashboardList = new List<SWPDashboard>();
        //    objSWP.strAction = "LANDV";
        //    objSWP.intUserid = 0;//Convert.ToInt32(Session["Userid"]);
        //    objSWP.Year = "";// "2017-18";// ddlLandFinYear.SelectedValue;
        //    objswpDashboardList = objserviceDashboard.GETLandDetails(objSWP).ToList();

        //    if (objswpDashboardList.Count > 0)
        //    {
        //        intLandAssesment = Convert.ToInt32(objswpDashboardList[0].LandAssessment);
        //        intLandProposalIdco = Convert.ToInt32(objswpDashboardList[0].PropNoForLand);
        //        intLandAllotByIdco = Convert.ToInt32(objswpDashboardList[0].ApplnLandAllotedByIDCO);
        //        decLandAreaAlloted = Convert.ToDecimal(objswpDashboardList[0].AreaAllotLand);
        //        intLandOrtpsaExceed = Convert.ToInt32(objswpDashboardList[0].ApplnLandORTPS);

        //        //spLandAssesment.InnerText = objswpDashboardList[0].LandAssessment;
        //        //spLandAllotByIDCO.InnerText = objswpDashboardList[0].ApplnLandAllotedByIDCO;
        //        //spPropIDCO.InnerText = objswpDashboardList[0].PropNoForLand;
        //        //spLandAllot.InnerText = objswpDashboardList[0].AreaAllotLand;
        //        //spORTPSALAnd.InnerText = objswpDashboardList[0].ApplnLandORTPS;
        //    }
        //    else
        //    {
        //        intLandAssesment = 0;
        //        intLandAllotByIdco = 0;
        //        intLandProposalIdco = 0;
        //        decLandAreaAlloted = 0;
        //        intLandOrtpsaExceed = 0;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    ///// Land Allotment Details (For DIC)
    private void LandServiceBindDIC()
    {
        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataSet objds = new DataSet();

            objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "LANDDEPT");
            objCommand.Parameters.AddWithValue("@P_INT_DISTRICT_ID", intDistrictId);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Tables[0].Rows.Count > 0)
            {
                intLandAssesmentCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_LAND_ASSESMENT_CM"]);
                intLandProposalIdcoCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_PROPOSAL_SENT_CM"]);
                intLandAllotByIdcoCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_LAND_ALLOTED_BY_IDCO_CM"]);
                decLandAreaAllotedCM = Convert.ToDecimal(objds.Tables[0].Rows[0]["DEC_AREA_ALLOTED_BY_IDCO_CM"]);
                intLandOrtpsaExceedCM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_ORTPSA_TIME_CM"]);

                intLandAssesmentLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_LAND_ASSESMENT_LM"]);
                intLandProposalIdcoLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_PROPOSAL_SENT_LM"]);
                intLandAllotByIdcoLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_LAND_ALLOTED_BY_IDCO_LM"]);
                decLandAreaAllotedLM = Convert.ToDecimal(objds.Tables[0].Rows[0]["DEC_AREA_ALLOTED_BY_IDCO_LM"]);
                intLandOrtpsaExceedLM = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_ORTPSA_TIME_LM"]);
            }
            else
            {
                intLandAssesmentCM = 0;
                intLandAllotByIdcoCM = 0;
                intLandProposalIdcoCM = 0;
                decLandAreaAllotedCM = 0;
                intLandOrtpsaExceedCM = 0;

                intLandAssesmentLM = 0;
                intLandProposalIdcoLM = 0;
                intLandAllotByIdcoLM = 0;
                decLandAreaAllotedLM = 0;
                intLandOrtpsaExceedLM = 0;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }
    }

    /*-----------------------------External Service Section (Start)-------------------------------------------------*/

    ///// IDCO Post Allotment Applications (APAA)
    private void ApaaStatus()
    {
        try
        {
            string strUnitId = "0";
            string strIndType = "0";
            string strDeptId = "0";
            string strDistrictId = "0";

            if (intDesignationId == 126) //// If Collector then fetch both Large and MSME
            {
                strDistrictId = intDistrictId.ToString();
            }
            else
            {
                SWPDashboard objSWP = new SWPDashboard();
                List<SWPDashboard> objlist = new List<SWPDashboard>();
                objSWP.intUserid = intUserId;
                objlist = objserviceDashboard.CheckAppastatus(objSWP);
                if (objlist.Count > 0)
                {
                    if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
                    {
                        strIndType = "0";
                    }
                    else if (objlist[0].intStatus != 1 && objlist[0].intDistrictid == 0)//IPICOL Largescale
                    {
                        strIndType = "1";
                    }
                    else //MSME
                    {
                        strIndType = "2";
                        strDistrictId = intDistrictId.ToString();
                        strDeptId = intDeptId.ToString();
                    }
                }
            }

            string serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getAPAAConsolidationDataMail/" + strUnitId + "/" + strDistrictId + "/" + strDeptId + "/" + strIndType + "/" + strCurrentYear + "/" + strCurrentMonth;

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
                    string output = DynTable.Rows[0]["getAPAAConsolidationDataMailResult"].ToString();
                    string[] finalOut = output.Split(':');

                    if (DynTable.Rows.Count > 0)
                    {
                        intAppaPendingCM = Convert.ToInt32(DynTable.Rows[0]["TotalPendingIdcoCurrent"]);
                        intAppa30DaysCM = Convert.ToInt32(DynTable.Rows[0]["TotalMajorPendingIdcoCurrent"]);

                        intAppaPendingLM = Convert.ToInt32(DynTable.Rows[0]["TotalPendingIdcoLast"]);
                        intAppa30DaysLM = Convert.ToInt32(DynTable.Rows[0]["TotalMajorPendingIdcoLast"]);
                    }
                    else
                    {
                        intAppaPendingCM = 0;
                        intAppa30DaysCM = 0;

                        intAppaPendingLM = 0;
                        intAppa30DaysLM = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }


        //string finalquery = string.Empty;
        //SqlCommand cmd = null;
        //DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        //string Type = string.Empty;
        //SWPDashboard objSWP = new SWPDashboard();
        //string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
        //inputJson = inputJson.TrimStart('[').TrimEnd(']');
        ////string District = string.IsNullOrEmpty(ddlAPAADistrict.SelectedValue) ? default(string) : ddlAPAADistrict.SelectedValue.ToString();
        ////string Month = string.IsNullOrEmpty(ddlAppaMonth.SelectedValue) ? default(string) : ddlAppaMonth.SelectedValue.ToString();
        //string Year = "0";// strFY.Substring(0, 4);// "2017"; //string.IsNullOrEmpty(ddlYear.SelectedValue) ? default(string) : ddlYear.SelectedValue.ToString();
        //string Deptid = "0";
        ////string UniqueKey = Session["UID"].ToString();
        //string UniqueKey = "0";
        //List<SWPDashboard> objlist = new List<SWPDashboard>();
        //objSWP.intUserid = intUserId;// Convert.ToInt32(Session["Userid"]);
        //objlist = objserviceDashboard.CheckAppastatus(objSWP);
        //if (objlist.Count > 0)
        //{
        //    if (objlist[0].intStatus == 1 && objlist[0].intDistrictid == 0)//Admin
        //    {
        //        Type = "0";
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
        //try
        //{
        //    //string serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + "0" + "/" + Deptid + "/" + Type + "/" + Year + "/" + "0";
        //    string serviceUrl = "";
        //    if (intDesignationId == 126)
        //    {
        //        serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + intDistrictId + "/" + 0 + "/" + Type + "/" + Year + "/" + 0;
        //    }
        //    else
        //    {
        //        serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + intDistrictId + "/" + intDeptId + "/" + Type + "/" + Year + "/" + 0;
        //    }
        //    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
        //    httpRequest.Accept = "application/json";
        //    httpRequest.ContentType = "application/json";
        //    httpRequest.Method = "GET";
        //    using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
        //    {
        //        using (Stream stream = httpResponse.GetResponseStream())
        //        {
        //            string strResult = (new StreamReader(stream)).ReadToEnd();
        //            DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
        //            string output = DynTable.Rows[0]["getSWPConsolidationDataResult"].ToString();
        //            string[] finalOut = output.Split(':');

        //            //intAppa = Convert.ToInt32(DynTable.Rows[0]["TotalPendingUnit"]);
        //            intAppa = Convert.ToInt32(DynTable.Rows[0]["TotalPendingIdco"]);
        //            intAppa30Days = Convert.ToInt32(DynTable.Rows[0]["TotalMajorPendingIdco"]);
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    private void getSPMGDataForAll()
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

            //string serviceUrl = "https://164.100.58.41//odisha/restservices/RestServer.php?view=issuestatus";
            string serviceUrl = "https://esuvidha.gov.in/odisha/restservices/RestServer.php?view=monthlyissuestatus";
            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
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
                                intTempSPMGPendingCM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Current"]);
                                intTempSPMGMore30DaysCM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Current (more than 30 days))"]);
                                intTempSPMGPendingLM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Last"]);
                                intTempSPMGMore30DaysLM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Last (more than 30 days))"]);
                            }
                            else
                            {
                                intTempSPMGPendingCM = 0;
                                intTempSPMGMore30DaysCM = 0;
                                intTempSPMGPendingLM = 0;
                                intTempSPMGMore30DaysLM = 0;
                            }
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Util.LogError(ex, "MailScheduler");
        }

        //try
        //{
        //    SqlCommand objCommand = new SqlCommand();
        //    SqlDataAdapter objDa = new SqlDataAdapter();
        //    DataSet objds = new DataSet();

        //    objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
        //    objCommand.CommandType = CommandType.StoredProcedure;
        //    objCommand.Connection = conn;

        //    objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "SPMGALL");

        //    objDa.SelectCommand = objCommand;
        //    objDa.Fill(objds);

        //    if (objds.Tables[0].Rows.Count > 0)
        //    {
        //        intSPMGPendingCM = Convert.ToInt32(objds.Tables[0].Rows[0]["Pending"]);
        //        intSPMGMore30DaysCM = Convert.ToInt32(objds.Tables[0].Rows[0]["issuePendingMore30Days"]);
        //    }
        //    else
        //    {
        //        intSPMGPendingCM = 0;
        //        intSPMGMore30DaysCM = 0;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    ///// State Project Monitoring Group (ALL)
    private void ViewSPMGMasterData()
    {
        intSPMGPendingCM = intTempSPMGPendingCM;
        intSPMGMore30DaysCM = intTempSPMGMore30DaysCM;
        intSPMGPendingLM = intTempSPMGPendingLM;
        intSPMGMore30DaysLM = intTempSPMGMore30DaysLM;
    }

    ///// State Project Monitoring Group (Department)
    private void ViewSPMGDEPTwiseCount()
    {
        System.Threading.Thread.Sleep(5000);

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
                                intSPMGPendingCM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Current"]);
                                intSPMGMore30DaysCM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Current (more than 30 days))"]);

                                intSPMGPendingLM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Last"]);
                                intSPMGMore30DaysLM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Last (more than 30 days))"]);
                            }
                            else
                            {
                                intSPMGPendingCM = 0;
                                intSPMGMore30DaysCM = 0;
                                intSPMGPendingLM = 0;
                                intSPMGMore30DaysLM = 0;
                            }
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Util.LogError(ex, "MailScheduler");
        }


        //try
        //{
        //    SqlCommand objCommand = new SqlCommand();
        //    SqlDataAdapter objDa = new SqlDataAdapter();
        //    DataSet objds = new DataSet();

        //    objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
        //    objCommand.CommandType = CommandType.StoredProcedure;
        //    objCommand.Connection = conn;

        //    objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "SPMGDEPT");
        //    objCommand.Parameters.AddWithValue("@P_INT_DEPT_ID", intDeptId);

        //    objDa.SelectCommand = objCommand;
        //    objDa.Fill(objds);

        //    if (objds.Tables[0].Rows.Count > 0)
        //    {
        //        intSPMGPendingCM = Convert.ToInt32(objds.Tables[0].Rows[0]["Pending"]);
        //        intSPMGMore30DaysCM = Convert.ToInt32(objds.Tables[0].Rows[0]["Pendingmore"]);
        //    }
        //    else
        //    {
        //        intSPMGPendingCM = 0;
        //        intSPMGMore30DaysCM = 0;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    //BEGIN State Project Monitoring Group(District Level) ADDED BY MANOJ KUMAR BEHERA 11.09.2019

    private void getSPMGDistrictDataForAll()
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

            //string serviceUrl = "https://164.100.58.41//odisha/restservices/RestServer.php?view=issuestatus";
            string serviceUrl = "https://esuvidha.gov.in/odishadi/restservices/RestServer.php?view=monthlyissuestatus";            
            object input = new
            {
                RandomNonce = randno,
                TimeStamp = plunixtime,
                PasswordDigest = ranpss1,
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
                                intdistTempSPMGPendingCM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Current"]);
                                intdistTempSPMGMore30DaysCM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Current (more than 30 days))"]);
                                intdistTempSPMGPendingLM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Last"]);
                                intdistTempSPMGMore30DaysLM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Last (more than 30 days))"]);
                            }
                            else
                            {
                                intdistTempSPMGPendingCM = 0;
                                intdistTempSPMGMore30DaysCM = 0;
                                intdistTempSPMGPendingLM = 0;
                                intdistTempSPMGMore30DaysLM = 0;
                            }
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Util.LogError(ex, "MailScheduler");
        }
    }

    private void ViewSPMGDistrictMasterData()
    {
        intdistSPMGPendingCM = intdistTempSPMGPendingCM;
        intdistSPMGMore30DaysCM = intdistTempSPMGMore30DaysCM;
        intdistSPMGPendingLM = intdistTempSPMGPendingLM;
        intdistSPMGMore30DaysLM = intdistTempSPMGMore30DaysLM;

    }

    private void ViewSPMGDistrictDEPTwiseCount()
    {
       // System.Threading.Thread.Sleep(5000);

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

            string serviceUrl = "https://esuvidha.gov.in/odishadi/restservices/RestServer.php?view=monthlyissuestatusbydepartmentid";
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
                                intdistSPMGPendingCM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Current"]);
                                intdistSPMGMore30DaysCM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Current (more than 30 days))"]);

                                intdistSPMGPendingLM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Last"]);
                                intdistSPMGMore30DaysLM = Convert.ToInt32(DynTable.Rows[0]["Issues Pending Last (more than 30 days))"]);
                            }
                            else
                            {
                                intdistSPMGPendingCM = 0;
                                intdistSPMGMore30DaysCM = 0;
                                intdistSPMGPendingLM = 0;
                                intdistSPMGMore30DaysLM = 0;
                            }
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            Util.LogError(ex, "MailScheduler");
        }
    }

    //END OF State Project Monitoring Group(District Level) ADDED BY MANOJ KUMAR BEHERA 11.09.2019


    ///// Random Number Generation for SPMG Service
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

    ///// Encryption Code Generation for SPMG Service
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

    ///// Central Inspection Framework
    private void ViewCICGMasterData()
    {
        /////// Get CICG Dept. Id
        string Department = objserviceDashboard.GetCIFDepartmentid(intDeptId.ToString());
        if (Department == "")
        {
            Department = "0";
        }

        //string Month = "0";
        //string Year = "0";

        try
        {
            //string serviceUrl = ConfigurationManager.AppSettings["CICGServiceURL"] + "GetInspectionDtls/" + Department + "/" + Month + "/" + Year;
            string serviceUrl = ConfigurationManager.AppSettings["CICGServiceURL"] + "GetInspDtlsMonthYearWise/" + Department + "/" + strCurrentMonth + "/" + strCurrentYear;
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
                    string output = DynTable.Rows[0]["GetInspDtlsMonthYearWiseResult"].ToString();
                    string[] finalOut = output.Split(':');

                    if (DynTable.Rows.Count > 0)
                    {
                        //string InspectionCompleted = finalOut[2].ToString();
                        //string InspectionScheduled = DynTable.Rows[0]["InspectionScheduledCurrent"].ToString();
                        //string PendingInspection = DynTable.Rows[0]["PendingInspectionCurrent"].ToString();
                        //string ReportNotUploaded = DynTable.Rows[0]["ReportNotUploadedCurrent"].ToString();
                        //string UnAttendedInspection = DynTable.Rows[0]["UnAttendedInspectionCurrent"].ToString();

                        intCIFPendingInspCM = Convert.ToInt32(DynTable.Rows[0]["PendingInspectionCurrent"]);
                        intCIFInspNotUpdCM = Convert.ToInt32(DynTable.Rows[0]["ReportNotUploadedCurrent"]);

                        intCIFPendingInspLM = Convert.ToInt32(DynTable.Rows[0]["PendingInspectionLast"]);
                        intCIFInspNotUpdLM = Convert.ToInt32(DynTable.Rows[0]["ReportNotUploadedLast"]);
                    }
                    else
                    {
                        intCIFPendingInspCM = 0;
                        intCIFInspNotUpdCM = 0;

                        intCIFPendingInspLM = 0;
                        intCIFInspNotUpdLM = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailScheduler");
        }


        /*  try
          {
              SqlCommand objCommand = new SqlCommand();
              SqlDataAdapter objDa = new SqlDataAdapter();
              DataSet objds = new DataSet();

              objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
              objCommand.CommandType = CommandType.StoredProcedure;
              objCommand.Connection = conn;

              objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "CICGALL");
              objCommand.Parameters.AddWithValue("@P_INT_USER_ID", intUserId);

              objDa.SelectCommand = objCommand;
              objDa.Fill(objds);

              if (objds.Tables[0].Rows.Count > 0)
              {
                  intCIF = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_REPORT_PENDING"]);
              }
              else
              {
                  intCIF = 0;
              }
          }
          catch (Exception ex)
          {
              Util.LogError(ex, "MailScheduler");
          }
          */

        /* objSWP = new SWPDashboard();
         objserviceDashboard = new DashboardBusinessLayer();
         try
         {
             objSWP.strAction = "VCI";
             objSWP.intUserid = intUserId;//  Convert.ToInt32(Session["Userid"]);

             List<SWPDashboard> objServiceStatus = objserviceDashboard.GetCICGDashboardService(objSWP).ToList();
             string strCount = "";
             if (objServiceStatus.Count > 0)
             {
                 strCount = objServiceStatus[0].INT_REPORT_PENDING.ToString();
                 //SPcicgpending.Text = objServiceStatus[0].INT_REPORT_PENDING.ToString();
             }
             else
             {
                 strCount = "0";
                 //SPcicgpending.Text = "0";
             }

             intCIF = Convert.ToInt32(strCount);
         }
         catch (Exception ex)
         {
             Util.LogError(ex, "MailScheduler");
         }
         * */
    }

    ///// CSR Spend
    private void CSRSpendAll()
    {
        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetDataMonthYearWise/" + strCurrentMonth + "/" + strCurrentYear + "/" + 0;
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
                        string output = DynTable.Rows[0]["GetDataMonthYearWiseResult"].ToString();
                        string[] finalOut = output.Split(':');

                        decimal TotAmountCurrent = Convert.ToDecimal((Convert.ToDouble(finalOut[2].ToString()) / 100));

                        //decimal TotAmountCurrent = Convert.ToDecimal(DynTable.Rows[0]["CSRTotalAmountCurrent"]) / 100;
                        decCSRTotalAmountCM = Math.Round(TotAmountCurrent);
                        intCSRcouncilCM = Convert.ToInt32(DynTable.Rows[0]["TotalRecommendProjectCurrent"]);
                        intCSRCorporateCM = Convert.ToInt32(DynTable.Rows[0]["RecCSRProjectsUnderTakenByCorpCurrent"]);

                        decimal TotAmountLast = Convert.ToDecimal(DynTable.Rows[0]["CSRTotalAmountLast"]) / 100;
                        decCSRTotalAmountLM = Math.Round(TotAmountLast);
                        intCSRcouncilLM = Convert.ToInt32(DynTable.Rows[0]["TotalRecommendProjectLast"]);
                        intCSRCorporateLM = Convert.ToInt32(DynTable.Rows[0]["RecCSRProjectsUnderTakenByCorpLast"]);
                    }
                    else
                    {
                        decCSRTotalAmountCM = 0;
                        intCSRcouncilCM = 0;
                        intCSRCorporateCM = 0;

                        decCSRTotalAmountLM = 0;
                        intCSRcouncilLM = 0;
                        intCSRCorporateLM = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            decCSRTotalAmountCM = 0;
            intCSRcouncilCM = 0;
            intCSRCorporateCM = 0;

            decCSRTotalAmountLM = 0;
            intCSRcouncilLM = 0;
            intCSRCorporateLM = 0;

            Util.LogError(ex, "MailScheduler");
        }

        //try
        //{
        //    SqlCommand objCommand = new SqlCommand();
        //    SqlDataAdapter objDa = new SqlDataAdapter();
        //    DataSet objds = new DataSet();

        //    objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
        //    objCommand.CommandType = CommandType.StoredProcedure;
        //    objCommand.Connection = conn;

        //    objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "CSRALL");

        //    objDa.SelectCommand = objCommand;
        //    objDa.Fill(objds);

        //    if (objds.Tables[0].Rows.Count > 0)
        //    {
        //        decCSR = Convert.ToDecimal(objds.Tables[0].Rows[0]["Total_CSR_Amt"]);
        //        intCSRcouncil = 0;
        //    }
        //    else
        //    {
        //        decCSR = 0;
        //        intCSRcouncil = 0;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "MailScheduler");
        //}

    }

    ///// CSR Count (District Wise)
    private void CSRCountDistrict()
    {
        try
        {
            string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "GetDataMonthYearWise/" + strCurrentMonth + "/" + strCurrentYear + "/" + intDistrictId;
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
                        string output = DynTable.Rows[0]["GetDataMonthYearWiseResult"].ToString();
                        string[] finalOut = output.Split(':');

                        intCSRTotalProjDistrictCM = Convert.ToInt32(DynTable.Rows[0]["TotalProjectCurrent"]);
                        intCSRRecProjDistrictCM = Convert.ToInt32(DynTable.Rows[0]["TotalRecommendProjectCurrent"]);
                        intCSRUnderTakenProjDistrictCM = Convert.ToInt32(DynTable.Rows[0]["RecCSRProjectsUnderTakenByCorpCurrent"]);

                        intCSRTotalProjDistrictLM = Convert.ToInt32(DynTable.Rows[0]["TotalProjectLast"]);
                        intCSRRecProjDistrictLM = Convert.ToInt32(DynTable.Rows[0]["TotalRecommendProjectLast"]);
                        intCSRUnderTakenProjDistrictLM = Convert.ToInt32(DynTable.Rows[0]["RecCSRProjectsUnderTakenByCorpLast"]);
                    }
                    else
                    {
                        intCSRTotalProjDistrictCM = 0;
                        intCSRRecProjDistrictCM = 0;
                        intCSRUnderTakenProjDistrictCM = 0;

                        intCSRTotalProjDistrictLM = 0;
                        intCSRRecProjDistrictLM = 0;
                        intCSRUnderTakenProjDistrictLM = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            intCSRRecProjDistrictCM = 0;
            intCSRTotalProjDistrictCM = 0;
            intCSRUnderTakenProjDistrictCM = 0;

            intCSRRecProjDistrictLM = 0;
            intCSRTotalProjDistrictLM = 0;
            intCSRUnderTakenProjDistrictLM = 0;

            Util.LogError(ex, "MailScheduler");
        }

        //try
        //{
        //    string serviceUrl = ConfigurationManager.AppSettings["CSRServiceURL"].ToString() + "getDataDistrictWise/" + intDistrictId;
        //    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(serviceUrl));
        //    httpRequest.Accept = "application/json";
        //    httpRequest.ContentType = "application/json";
        //    httpRequest.Method = "GET";
        //    using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
        //    {
        //        using (Stream stream = httpResponse.GetResponseStream())
        //        {
        //            string strResult = (new StreamReader(stream)).ReadToEnd();
        //            DataTable DynTable = DashboradCommon.JsonStringToDataTable(strResult);
        //            string output = DynTable.Rows[0]["getDataDistrictWiseResult"].ToString();
        //            string[] finalOut = output.Split(':');

        //            intCSRRecProjDistrictCM = Convert.ToInt32(finalOut[2].ToString());
        //            intCSRTotalProjDistrictCM = Convert.ToInt32(DynTable.Rows[0]["CountTotalProj"]);
        //            intCSRUnderTakenProjDistrictCM = Convert.ToInt32(DynTable.Rows[0]["CountUnderTakenProj"]);
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    intCSRRecProjDistrictCM = 0;
        //    intCSRTotalProjDistrictCM = 0;
        //    intCSRUnderTakenProjDistrictCM = 0;

        //    Util.LogError(ex, "MailScheduler");
        //}
    }

    /*-----------------------------External Service Section (End)-------------------------------------------------*/

    ///// Total Investment Amount (State,Distric,IT and Tourism Level)
    //private void fillTotalInvestmentLevelWise()
    //{
    //    try
    //    {
    //        SqlCommand objCommand = new SqlCommand();
    //        SqlDataAdapter objDa = new SqlDataAdapter();
    //        DataSet objds = new DataSet();

    //        objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
    //        objCommand.CommandType = CommandType.StoredProcedure;
    //        objCommand.Connection = conn;

    //        objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "INVLEVEL");

    //        objDa.SelectCommand = objCommand;
    //        objDa.Fill(objds);

    //        if (objds.Tables[0].Rows.Count > 0)
    //        {
    //            decTotalInvestStateCM = Convert.ToDecimal(objds.Tables[0].Rows[0]["decTotalInvestState"]);
    //            decTotalInvestDistrictCM = Convert.ToDecimal(objds.Tables[0].Rows[0]["decTotalInvestDistrict"]);
    //            decTotalInvestITCM = Convert.ToDecimal(objds.Tables[0].Rows[0]["decTotalInvestIT"]);
    //            decTotalInvestTourismCM = Convert.ToDecimal(objds.Tables[0].Rows[0]["decTotalInvestTourism"]);
    //        }
    //        else
    //        {
    //            decTotalInvestStateCM = 0;
    //            decTotalInvestDistrictCM = 0;
    //            decTotalInvestITCM = 0;
    //            decTotalInvestTourismCM = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "MailScheduler");
    //    }
    //}

    ///// Total Employment (State,Distric,IT and Tourism Level)
    //private void fillTotalEmploymentLevelWise()
    //{
    //    try
    //    {
    //        SqlCommand objCommand = new SqlCommand();
    //        SqlDataAdapter objDa = new SqlDataAdapter();
    //        DataSet objds = new DataSet();

    //        objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
    //        objCommand.CommandType = CommandType.StoredProcedure;
    //        objCommand.Connection = conn;

    //        objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "EMPLEVEL");

    //        objDa.SelectCommand = objCommand;
    //        objDa.Fill(objds);

    //        if (objds.Tables[0].Rows.Count > 0)
    //        {
    //            intTotalEmpStateCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intTotalEmpState"]);
    //            intTotalEmpDistrictCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intTotalEmpDistrict"]);
    //            intTotalEmpITCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intTotalEmpIT"]);
    //            intTotalEmpTourismCM = Convert.ToInt32(objds.Tables[0].Rows[0]["intTotalEmpTourism"]);
    //        }
    //        else
    //        {
    //            intTotalEmpStateCM = 0;
    //            intTotalEmpDistrictCM = 0;
    //            intTotalEmpITCM = 0;
    //            intTotalEmpTourismCM = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "MailScheduler");
    //    }
    //}

    ///// Total Investment Amount for DIC (Only MSME)
    //private void fillTotalInvestmentDIC()
    //{
    //    try
    //    {
    //        SqlCommand objCommand = new SqlCommand();
    //        SqlDataAdapter objDa = new SqlDataAdapter();
    //        DataSet objds = new DataSet();

    //        objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
    //        objCommand.CommandType = CommandType.StoredProcedure;
    //        objCommand.Connection = conn;

    //        objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "INV");
    //        objCommand.Parameters.AddWithValue("@P_INT_DISTRICT_ID", intDistrictId);
    //        objCommand.Parameters.AddWithValue("@P_INT_USER_ID", intUserId);

    //        objDa.SelectCommand = objCommand;
    //        objDa.Fill(objds);

    //        if (objds.Tables[0].Rows.Count > 0)
    //        {
    //            decInvestment = Convert.ToDecimal(objds.Tables[0].Rows[0]["Total_Inv"]);
    //        }
    //        else
    //        {
    //            decInvestment = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "MailScheduler");
    //    }
    //}

    ///// Total Employment for DIC (Only MSME)
    //private void fillTotalEmploymentDIC()
    //{
    //    try
    //    {
    //        SqlCommand objCommand = new SqlCommand();
    //        SqlDataAdapter objDa = new SqlDataAdapter();
    //        DataSet objds = new DataSet();

    //        objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
    //        objCommand.CommandType = CommandType.StoredProcedure;
    //        objCommand.Connection = conn;

    //        objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "EMP");
    //        objCommand.Parameters.AddWithValue("@P_INT_DISTRICT_ID", intDistrictId);
    //        objCommand.Parameters.AddWithValue("@P_INT_USER_ID", intUserId);

    //        objDa.SelectCommand = objCommand;
    //        objDa.Fill(objds);

    //        if (objds.Tables[0].Rows.Count > 0)
    //        {
    //            intEmployment = Convert.ToInt32(objds.Tables[0].Rows[0]["Total_Emp"]);
    //        }
    //        else
    //        {
    //            intEmployment = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "MailScheduler");
    //    }
    //}

    /////// Central Inspection Framework (Drilled)
    //private void ViewCICGData()
    //{
    //    try
    //    {
    //        SqlCommand objCommand = new SqlCommand();
    //        SqlDataAdapter objDa = new SqlDataAdapter();
    //        DataSet objds = new DataSet();

    //        objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
    //        objCommand.CommandType = CommandType.StoredProcedure;
    //        objCommand.Connection = conn;

    //        objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "CICGDEPT");
    //        objCommand.Parameters.AddWithValue("@P_INT_USER_ID", intUserId);
    //        objCommand.Parameters.AddWithValue("@P_INT_DEPT_ID", intDeptId);

    //        objDa.SelectCommand = objCommand;
    //        objDa.Fill(objds);

    //        if (objds.Tables[0].Rows.Count > 0)
    //        {
    //            intInspNotUpd = Convert.ToInt32(objds.Tables[0].Rows[0]["INT_ReportNot_Uploaded"]);
    //        }
    //        else
    //        {
    //            intInspNotUpd = 0;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "MailScheduler");
    //    }

    //    //objSWP = new SWPDashboard();
    //    //objserviceDashboard = new DashboardBusinessLayer();
    //    //try
    //    //{
    //    //    objSWP.strAction = "VCI";
    //    //    objSWP.intUserid = intUserId;// Convert.ToInt32(Session["Userid"]);
    //    //    objSWP.intDeptId = intDeptId;// Convert.ToInt32(ddldeptCIF.SelectedValue);
    //    //    objSWP.intMonthId = 0; //Convert.ToInt32(ddlCICGMonth.SelectedValue);
    //    //    objSWP.intYearId = 0; //Convert.ToInt32(ddlYearCICG.SelectedValue);
    //    //    List<SWPDashboard> objServiceStatus = objserviceDashboard.GetCICGDashboardService(objSWP).ToList();
    //    //    string strCount = "";
    //    //    if (objServiceStatus.Count > 0)
    //    //    {
    //    //        //SPcicgapplied.InnerHtml = objServiceStatus[0].INT_INS_SCHEDULED.ToString();
    //    //        //SPcicgcompleted.InnerHtml = objServiceStatus[0].INT_INS_COMPLETED.ToString();
    //    //        //SPunattInsdash.InnerHtml = objServiceStatus[0].INT_UNATTENDED_INS.ToString();
    //    //        //SPReprtNotUploaded.InnerHtml = objServiceStatus[0].INT_REPORTNOT_UPLOADED.ToString();

    //    //        strCount = objServiceStatus[0].INT_REPORTNOT_UPLOADED.ToString();
    //    //    }
    //    //    else
    //    //    {
    //    //        //SPcicgapplied.InnerHtml = "0";
    //    //        //SPcicgcompleted.InnerHtml = "0";
    //    //        //SPunattInsdash.InnerHtml = "0";
    //    //        //SPReprtNotUploaded.InnerHtml = "0";
    //    //        strCount = "0";
    //    //    }

    //    //    intInspNotUpd = Convert.ToInt32(strCount);
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    Util.LogError(ex, "MailScheduler");
    //    //}
    //}

    ///// IDCO POST ALLOTMENT APPLICATIONS (For DIC)
    //private void viewApaaStatusDIC()
    //{
    //    try
    //    {
    //        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    //        string strType = string.Empty;
    //        SWPDashboard objSWP = new SWPDashboard();
    //        string inputJson = (new JavaScriptSerializer()).Serialize(objSWP);
    //        inputJson = inputJson.TrimStart('[').TrimEnd(']');

    //        string Year = "0";
    //        string UniqueKey = "0";
    //        List<SWPDashboard> objlist = new List<SWPDashboard>();
    //        objSWP.intUserid = intUserId;// Convert.ToInt32(Session["Userid"]);

    //        string serviceUrl = "";
    //        if (intDesignationId == 126) //// If Collector then fetch both Large and MSME
    //        {
    //            strType = "0";
    //            serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + Convert.ToInt16(intDistrictId) + "/" + 0 + "/" + strType + "/" + Year + "/" + 0;
    //        }
    //        else
    //        {
    //            strType = "2";
    //            serviceUrl = ConfigurationManager.AppSettings["AppaServiceURL"] + "getSWPConsolidationData/" + UniqueKey + "/" + Convert.ToInt16(intDistrictId) + "/" + intDeptId + "/" + strType + "/" + Year + "/" + 0;
    //        }

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
    //                string output = DynTable.Rows[0]["getSWPConsolidationDataResult"].ToString();
    //                string[] finalOut = output.Split(':');

    //                //intApaaPendingDIC = Convert.ToInt32(DynTable.Rows[0]["TotalPendingUnit"]);
    //                intApaaPendingDIC = Convert.ToInt32(DynTable.Rows[0]["TotalPendingIdco"]);
    //                intApaa30DaysDIC = Convert.ToInt32(DynTable.Rows[0]["TotalMajorPendingIdco"]);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        intApaaPendingDIC = 0;
    //        intApaa30DaysDIC = 0;
    //        Util.LogError(ex, "MailScheduler");
    //    }

    //    //try
    //    //{
    //    //    SqlCommand objCommand = new SqlCommand();
    //    //    SqlDataAdapter objDa = new SqlDataAdapter();
    //    //    DataSet objds = new DataSet();

    //    //    objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
    //    //    objCommand.CommandType = CommandType.StoredProcedure;
    //    //    objCommand.Connection = conn;

    //    //    objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "APAADEPT");
    //    //    objCommand.Parameters.AddWithValue("@P_INT_DISTRICT_ID", intDistrictId);

    //    //    objDa.SelectCommand = objCommand;
    //    //    objDa.Fill(objds);

    //    //    if (objds.Tables[0].Rows.Count > 0)
    //    //    {
    //    //        intApaa30DaysDIC = Convert.ToInt32(objds.Tables[0].Rows[0]["TotalMajorPendingIdco"]);
    //    //        intApaaPendingDIC = Convert.ToInt32(objds.Tables[0].Rows[0]["TotalPendingUnit"]);
    //    //    }
    //    //    else
    //    //    {
    //    //        intApaa30DaysDIC = 0;
    //    //        intApaaPendingDIC = 0;
    //    //    }
    //    //}
    //    //catch (Exception ex)
    //    //{
    //    //    Util.LogError(ex, "MailScheduler");
    //    //}
    //}

    #endregion

    ///// Schedule the Mail ( For Testing Purpose)
    public string mailSchedule_Test(string strSessUserId)
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();

        objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
        objCommand.CommandType = CommandType.StoredProcedure;
        objCommand.Connection = conn;

        objCommand.Parameters.AddWithValue("@P_VCH_ACTION", 'V');

        objDa.SelectCommand = objCommand;
        objDa.Fill(objds);

        if (objds.Tables[0].Rows.Count > 0)
        {
            string strStatus = objds.Tables[0].Rows[0]["STATUS"].ToString();

            /*-----------------------------------------------------------------*/
            /////// Check Internal Mail Id Present or Not (Temporarily Used,To be deleted Later)
            if (objds.Tables[2].Rows.Count > 0)
            {
                dtMail = objds.Tables[2];
            }
            else
            {
                dtMail = new DataTable();
            }
            /*-----------------------------------------------------------------*/

            strStatus = "2";

            if (strStatus == "2")
            {
                getSPMGDataForAll();

                //Changes By Manoj Kumar Behera on 12.09.2019

                getSPMGDistrictDataForAll();

                //End of Changes Manoj Kumar Behera on 12.09.2019

                /*-----------------------------------------------------------------*/
                //////// Insert Record 
                //if (conn.State == ConnectionState.Closed)
                //{
                //    conn.Open();
                //}

                //objCommand = new SqlCommand();
                //objCommand.CommandText = "USP_GET_USER_INFO_MAIL_SCHEDULER";
                //objCommand.CommandType = CommandType.StoredProcedure;
                //objCommand.Connection = conn;

                //objCommand.Parameters.AddWithValue("@P_VCH_ACTION", 'A');
                //objCommand.ExecuteNonQuery();

                #region GetCurrentFinancialYear

                ///*-----------------------------------------------------------------*/
                /////// Get Current Financial Year              
                //if (DateTime.Now.Month > 3)
                //{
                //    strFY = DateTime.Now.Year.ToString() + "-" + (DateTime.Now.Year + 1).ToString().Substring(2, 2);
                //}
                //else
                //{
                //    strFY = Convert.ToString(DateTime.Today.Year - 1) + "-" + DateTime.Now.Year.ToString().Substring(2, 2);
                //}

                ///*-----------------------------------------------------------------*/

                #endregion

                //for (int i = 0; i < objds.Tables[1].Rows.Count; i++)
                //{
                //    System.Threading.Thread.Sleep(2000);

                //    intUserId = Convert.ToInt32(objds.Tables[1].Rows[i]["intUserId"]);
                //    intDesignationId = Convert.ToInt32(objds.Tables[1].Rows[i]["intDesignationId"]);
                //    //strMailId = Convert.ToString(objds.Tables[1].Rows[i]["vchEmail"]);


                DataView dataView = objds.Tables[1].DefaultView;

                dataView.RowFilter = "intUserId = '" + strSessUserId + "'";
                intDesignationId = Convert.ToInt32(dataView[0]["intDesignationId"]);
                strUserName = dataView[0]["vchUserName"].ToString();
                // intDeptId = Convert.ToInt32(dataView[0]["intLevelDetailId"]);
                intUserId = Convert.ToInt32(strSessUserId);

                #region CheckDesignation

                if (intDesignationId == 94) //// CM Odisha
                {
                    //Response.Redirect("CmDashboard.aspx");

                    #region CM Odisha

                    FillProposalDetailsState();
                    FillProposalDistrictLevel();
                    FillProposalITLevel();
                    FillProposalTourismLevel();

                    LandServiceBind();

                    CSRSpendAll();
                    ApaaStatus();
                    ViewCICGMasterData();
                    ViewSPMGMasterData();

                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatus();

                    ///// Create Mail Body
                    CreatePealHtmlTableFragment();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();
                    CreateServiceHtmlTable();

                    #endregion
                }
                else if (intDesignationId == 95 || intDesignationId == 124) //// Chief Secretary Odisha and Development Commissioner
                {
                    //Response.Redirect("ChiefSecretaryDashboard.aspx");  

                    #region CS Odisha

                    FillProposalDetailsState();
                    FillProposalDistrictLevel();
                    FillProposalITLevel();
                    FillProposalTourismLevel();

                    //fillTotalInvestmentLevelWise();
                    //fillTotalEmploymentLevelWise();

                    LandServiceBind();

                    CSRSpendAll();
                    ApaaStatus();
                    ViewCICGMasterData();
                    ViewSPMGMasterData();
                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatus();

                    ///// Create Mail Body
                    CreatePealHtmlTableFragment();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();
                    CreateServiceHtmlTable();

                    #endregion
                }
                else if (intDesignationId == 125 || intDesignationId == 97) //// ACS MSME and PS MSME
                {
                    //Response.Redirect("PS(MSME)Dashboard.aspx");

                    #region PS-MSME

                    FillProposalDetailsState();
                    FillProposalDistrictLevel();
                    FillProposalITLevel();
                    FillProposalTourismLevel();

                    LandServiceBind();

                    CSRSpendAll();
                    ApaaStatus();
                    ViewCICGMasterData();
                    ViewSPMGMasterData();
                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatus();

                    ///// Create Mail Body
                    CreatePealHtmlTableFragment();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();
                    CreateServiceHtmlTable();

                    #endregion
                }
                else if (intDesignationId == 96) //// PS Odisha
                {
                    //Response.Redirect("PSIndustriesDashboard.aspx");   

                    #region PS Odisha

                    FillProposalDetailsState();
                    FillProposalDistrictLevel();
                    FillProposalITLevel();
                    FillProposalTourismLevel();

                    //fillTotalInvestmentLevelWise();
                    //fillTotalEmploymentLevelWise();

                    LandServiceBind();

                    CSRSpendAll();
                    ApaaStatus();
                    ViewCICGMasterData();
                    ViewSPMGMasterData();
                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatus();

                    ///// Create Mail Body
                    CreatePealHtmlTableFragment();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();
                    CreateServiceHtmlTable();

                    #endregion
                }
                else if (intDesignationId == 98) //// PS Finance
                {
                    //Response.Redirect("PS(Finance)Dashboard.aspx");   

                    #region PS Finance

                    FillProposalDetailsState();
                    FillProposalDistrictLevel();
                    FillProposalITLevel();
                    FillProposalTourismLevel();

                    LandServiceBind();

                    CSRSpendAll();
                    ApaaStatus();
                    ViewCICGMasterData();
                    ViewSPMGMasterData();
                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatus();

                    ///// Create Mail Body
                    CreatePealHtmlTableFragment();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();
                    CreateServiceHtmlTable();

                    #endregion
                }
                else if (intDesignationId == 99) //// CMD IPICOL
                {
                    //Response.Redirect("CMDIPICOLDashboard.aspx");

                    #region CMD IPICOL

                    FillProposalDetailsState();
                    FillProposalDistrictLevel();
                    FillProposalITLevel();
                    FillProposalTourismLevel();

                    LandServiceBind();

                    CSRSpendAll();
                    ApaaStatus();
                    ViewCICGMasterData();
                    ViewSPMGMasterData();
                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatus();

                    ///// Create Mail Body
                    CreatePealHtmlTableFragment();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();
                    CreateServiceHtmlTable();

                    #endregion
                }
                else if (intDesignationId == 128) //// CMD IDCO
                {
                    //Response.Redirect("CMDIDCODashboard.aspx");

                    #region CMD IDCO

                    FillProposalDetailsState();
                    FillProposalDistrictLevel();
                    FillProposalITLevel();
                    FillProposalTourismLevel();

                    LandServiceBind();

                    CSRSpendAll();
                    ApaaStatus();
                    ViewCICGMasterData();
                    ViewSPMGMasterData();
                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatus();

                    ///// Create Mail Body
                    CreatePealHtmlTableFragment();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();
                    CreateServiceHtmlTable();

                    #endregion
                }
                else if (intDesignationId == 100) //// SLNA IPICOL
                {
                    //Response.Redirect("GMDashboard.aspx");

                    #region SLNA-IPICOL

                    FillProposalDetailsState();
                    FillProposalDistrictLevel();
                    FillProposalITLevel();
                    FillProposalTourismLevel();

                    LandServiceBind();

                    CSRSpendAll();
                    ApaaStatus();
                    ViewCICGMasterData();
                    ViewSPMGMasterData();
                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatus();

                    ///// Create Mail Body
                    CreatePealHtmlTableFragment();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();
                    CreateServiceHtmlTable();

                    #endregion
                }
                else if (intDesignationId == 10 || intDesignationId == 9)
                {
                    //Response.Redirect("DICDashboard.aspx");

                    #region DIC-RIC

                    strDeptStatus = "DOMAIN";

                    //intDeptId = Convert.ToInt32(objds.Tables[1].Rows[i]["intLevelDetailId"]);
                    //intDistrictId = Convert.ToInt32(objds.Tables[1].Rows[i]["intDistrict"]); 

                    intDeptId = Convert.ToInt32(dataView[0]["intLevelDetailId"]);
                    intDistrictId = Convert.ToInt32(dataView[0]["intDistrict"]);
                    strDomainName = dataView[0]["vchDomainUName"].ToString();

                    FillProposalDetailsDIC();

                    //fillTotalInvestmentDIC();
                    //fillTotalEmploymentDIC();

                    LandServiceBindDIC();
                    //viewApaaStatusDIC();
                    ApaaStatus();
                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatusDIC();

                    ///// Create Mail Body
                    CreatePealHtmlTable1();
                    CreateServiceHtmlTable();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();

                    #endregion
                }
                else if (intDesignationId == 126)
                {
                    //Response.Redirect("DICDashboard.aspx");

                    #region Collector

                    strDeptStatus = "DOMAIN";

                    //intDeptId = Convert.ToInt32(objds.Tables[1].Rows[i]["intLevelDetailId"]);
                    //intDistrictId = Convert.ToInt32(objds.Tables[1].Rows[i]["intDistrict"]); 

                    intDeptId = Convert.ToInt32(dataView[0]["intLevelDetailId"]);
                    intDistrictId = Convert.ToInt32(dataView[0]["intDistrict"]);
                    strDomainName = dataView[0]["vchDomainUName"].ToString();

                    //FillProposalDetailsState();

                    FillProposalDetailsDIC();

                    //fillTotalInvestmentDIC();
                    //fillTotalEmploymentDIC();

                    LandServiceBindDIC();

                    //viewApaaStatusDIC();
                    ApaaStatus();
                    CSRCountDistrict();

                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatusDIC();

                    ///// Create Mail Body
                    CreatePealHtmlTable1();
                    CreateServiceHtmlTable();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();

                    #endregion
                }

                else if (intDesignationId == 101)
                {
                    //Response.Redirect("DIDashboard.aspx");

                    #region DI

                    strDeptStatus = "DOMAIN";

                    //intDeptId = Convert.ToInt32(objds.Tables[1].Rows[i]["intLevelDetailId"]);
                    //intDistrictId = Convert.ToInt32(objds.Tables[1].Rows[i]["intDistrict"]); 

                    //intDeptId = Convert.ToInt32(dataView[0]["intLevelDetailId"]);
                    //intDistrictId = Convert.ToInt32(dataView[0]["intDistrict"]);
                    strDomainName = dataView[0]["vchDomainUName"].ToString();

                    FillProposalDetailsState();
                    FillProposalDistrictLevel();
                    FillProposalITLevel();
                    FillProposalTourismLevel();

                    LandServiceBind();

                    CSRSpendAll();
                    ApaaStatus();
                    ViewCICGMasterData();
                    ViewSPMGMasterData();
                    IncentiveMasterBind();

                    ///// Department Wise Approval
                    ViewServiceStatus();

                    ///// Create Mail Body
                    CreatePealHtmlTableFragment();
                    CreateLandHtmlTable();
                    CreatePealHtmlTable2();
                    CreateServiceHtmlTable();

                    #endregion
                }
                else ///// DepartmentDashboard
                {
                    //Response.Redirect("DepartmentDashboard.aspx");                      

                    #region Department

                    //intDeptId = Convert.ToInt32(objds.Tables[1].Rows[i]["intLevelDetailId"]);
                    //intDeptId = objserviceDashboard.GetDepartment(intDeptId.ToString());

                    intDeptId = Convert.ToInt32(dataView[0]["intLevelDetailId"]);
                    intDeptId = objserviceDashboard.GetDepartment(intDeptId.ToString());
                    intDistrictId = Convert.ToInt32(dataView[0]["intDistrict"]);

                    ///// Department Wise Approval
                    ViewServiceStatus();

                    /*-----------------------------------------------------------------------*/
                    //// In case of pslabour (116)
                    //// Display ORTPSA timeline count for Directorate of Labour and Directorate of F & B separately.
                    /*-----------------------------------------------------------------------*/
                    if (intDesignationId == 116)
                    {
                        ///// Department Wise Approval
                        ViewORTPSAStatus();
                        CreateServiceHtmlTablePsLabour();
                    }
                    else
                    {
                        ///// Create Mail Body                    
                        CreateServiceHtmlTable();
                    }

                    /*-----------------------------------------------------------------------*/
                    //// In Case of psfb(115),pslabour(116),psfscw(117),psospcb(122) 
                    //// Display SPMG and CICG Count
                    /*-----------------------------------------------------------------------*/
                    if (intDesignationId == 115 || intDesignationId == 116 || intDesignationId == 117 || intDesignationId == 122)
                    {
                        ViewSPMGDEPTwiseCount();
                        ViewCICGMasterData();

                        CreatePealHtmlTable2();
                    }

                    #endregion
                }

                #endregion

                /*------------------------------------------------*/
                ////// Add Header,Footer for Mail and Send
                /*------------------------------------------------*/
                mailHeader();
                mailFooter();
                //SendMail(strBody, "hiiiii");

                /*------------------------------------------------*/
                intUserId = 0;
                intDesignationId = 0;
                intDeptId = 0;
                strDomainName = "";
                strDeptStatus = "DEPT";

                //clearVaribales();
                //    }
            }
        }

        return strBody;
    }
}