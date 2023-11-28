using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SWPDashboard
{
    #region Investor dashboard added by nibedita behera
    //ADDED BY NIBEDITA ON 18-09-2017 FOR APAA PORTLET OF INVESTOR DASHBOARD 
    public string strChngReqApplied { get; set; }
    public string strChngReqDispose { get; set; }
    public string strChngReqPendingAtIDCO { get; set; }
    public string strChngReqPendAtUnit { get; set; }
    public string strChngReqCrossthirty { get; set; }


    public string Distirict { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }
    public string strFromDate { get; set; } //Added by Bhagyashree Das on Dt. 03-Feb-2021
    public string strToDate { get; set; } //Added by Bhagyashree Das on Dt. 03-Feb-2021
    public string UniqueKey { get; set; }

    public string StrStatus { get; set; }
    public int PEALIndays { get; set; }

    //added by nibedita behera on 20-09-2017 for grid apaa objection
    public string ApplicationName { get; set; }
    public string IEName { get; set; }
    public string PartyName { get; set; }
    public string PendingDays { get; set; }
    public string RequestDate { get; set; }
    #endregion

    //Added by suroj kumar pradhan on 19-09-2017 to show Proposal in dashboard
    public string strAction { get; set; }
    public int intUserid { get; set; }
    public string strApplied { get; set; }
    public string strApproved { get; set; }
    public string strRejected { get; set; }
    public int intDeptId { get; set; }
    public int intServiceId { get; set; }
    //added by nibedita 
    public string strPending { get; set; }

    public string strUnderEvaltion { get; set; }
    public string QraiseTotal { get; set; }

    public string strPealOrtpsaCrossedState { get; set; } //// Added by Sushant Jena on Dt.23-May-2018
    public string strPealOrtpsaCrossedDist { get; set; } //// Added by Sushant Jena on Dt.23-May-2018
    public string strPealOrtpsaCrossedITandTourism { get; set; } //// Added by Sushant Jena on Dt.23-May-2018 (For both IT and Tourism one varibale in declared)

    public string strTotCapitalPropApplied { get; set; }
    public string strTotNoCapitalPropApplied { get; set; }
    public string strTotCapitalPropApproved { get; set; }
    public string strTotNoCapitalPropApproved { get; set; }
    public int intCreatedBy { get; set; }
    public int intTotalApplied { get; set; }
    public int intTotalDisposed { get; set; }
    public int intTotalMajorPending { get; set; }
    public int intTotalPendingIdco { get; set; }
    public int intTotalPendingUnit { get; set; }
    public int intStatus { get; set; }
    public int intDistrictid { get; set; }
    public int intServiceApplied { get; set; }
    public string intDaysPass { get; set; }

    public string strServiceName { get; set; }

    //Added by Romalin Panda for SPMG data
    public int intInvestorId { get; set; }
    public int intSPMGRaised { get; set; }
    public int intSPMGResolved { get; set; }
    public int intSPMGPending { get; set; }
    public int intSPMGIssuePending { get; set; }
    public string VCH_DEPT_NAME { get; set; }
    public string VCH_ISSUE_TYPE { get; set; }
    public string VCH_DAYS { get; set; }
    public string Project_Department { get; set; }
    public string Type_Of_Issue { get; set; }
    public string Issue_Description { get; set; }
    public string Issue_Category { get; set; }
    public string Pending_Department { get; set; }
    public string Pending_Department_Type { get; set; }
    public string Pending_Days { get; set; }
    public string Name_Of_Investor { get; set; }
    public string Issue_Date { get; set; }

    //added by nibedita behera on 22-09-2017
    public string TotalProject { get; set; }
    public string TotalAmount { get; set; }
    public string AmountSpent { get; set; }
    public string CategoryName { get; set; }

    //added by suroj kumar pradhan for incentive dashboard
    public int intincApplied { get; set; }
    public int intincSanctioned { get; set; }
    public int intincPending { get; set; }
    public int intincRejected { get; set; }
    public int intincdistrubed { get; set; }

    //public string StrType { get; set; }
    public string StrincSector { get; set; }
    public string StrincType { get; set; }
    public int intincNoDays { get; set; }
    public string strincStatus { get; set; }
    public string strincRESStatus { get; set; }
    //Added by Romalin Panda for CICG data
    public int INT_INS_SCHEDULED { get; set; }
    public int INT_INS_COMPLETED { get; set; }
    public int INT_UNATTENDED_INS { get; set; }
    public int INT_REPORT_PENDING { get; set; }
    public int INT_REPORTNOT_UPLOADED { get; set; }
    //Added by Romalin Panda for CICG data

    public int intDistrictId { get; set; }
    public int intYearId { get; set; }
    public int intMonthId { get; set; }

    public int intSlNo { get; set; }
    public decimal decFee { get; set; }
    public string strComapnyName { get; set; }
    public int intEmployeement { get; set; }
    public string intEmployeement1 { get; set; }

    //added by suroj kumar pradhan on 27-09-17
    public string strDistrictName { get; set; }
    public int intPealstatus { get; set; }
    public int intType { get; set; }

    //added by nibedita behera on 06-oct-2017
    public string IndustryName { get; set; }
    public string InspectorName { get; set; }
    public string TOTALHOUR { get; set; }
    public int intDirectEmployee { get; set; }
    public int intContractualEmployee { get; set; }
    public int intQuarter { get; set; }

    //ADDED BY SUROJ KUMAR PRADHAN FOR INCENTIVE BIND
    public string INCAPLLIED { get; set; }
    public string INCSANCTIONED { get; set; }
    public string INCPENDING { get; set; }
    public string INCREJECTED { get; set; }
    public string strInctPending30Days { get; set; } //// Added by Sushant Jena on Dt.03-Apr-2018
    public string strIncMean { get; set; } //Added by Bhagyashree Das on Dt. 07-Dec-2020
    public string strIncMedian { get; set; } //Added by Bhagyashree Das on Dt. 07-Dec-2020

    public string strINCCompanyname { get; set; }
    public string strIncentiveSector { get; set; }
    public string strIncentiveStatus { get; set; }

    public int strPealDays { get; set; }
    public string strPealStatus { get; set; }
    public string strPealRemark { get; set; }
    public string strPealProposalno { get; set; }
    public string strPealQuerystatus { get; set; }
    //added for multiple district select on 25-10-2017
    public string strDistrictDtl { get; set; }
    public string strFinacialYear { get; set; }

    public string strUnitname { get; set; }
    public string strNodalPersonName { get; set; }
    public int intPendingdays { get; set; }
    public string strOption { get; set; }
    public int IntProposeid { get; set; }

    //ADDED BY SUROJ KUMAR PRADHAN TO SHOW PEAL DETAILS USERID WISE
    public int intPealuserstatus { get; set; }
    public int intPealuseroption { get; set; }
    public string strPealRecived { get; set; }
    public string strPealApproved { get; set; }
    public string strDeferred { get; set; }
    //ENDED BY SUROJ

    //ADDED FOR sERVICE QUERY PORTLET
    public string strTotalQuery { get; set; }
    public string strTotalQueryRaised { get; set; }
    public string strTotalQueryPending { get; set; }
    public string strTotalQueryResponse { get; set; }
    public string strTotQuerynotRecTimeline { get; set; }
    public string strAvgRaiseQuery { get; set; }

    public int intTotalQuery { get; set; }
    public int intTotalQueryRaised { get; set; }
    public int intTotalQueryPending { get; set; }
    public int intTotalQueryResponse { get; set; }
    public int intTotQuerynotRecTimeline { get; set; }
    public int intAvgRaiseQuery { get; set; }

    public string strPEALQueryRaised { get; set; }
    public string strPEALQueryRevert { get; set; }
    public string strPEALQueryResolved { get; set; }
    public string strPEALQueryAvg { get; set; }
    public string strPEALQueryPending { get; set; }
    public string strPEALQueryPendingPast { get; set; }

    public int intExhistingemployee { get; set; }

    //ADDED FOR peal QUERY PORTLET

    public int intPEALQueryRaised { get; set; }
    public int intPEALQueryRevert { get; set; }
    public int intPEALQueryResolved { get; set; }
    public int intPEALQueryAvg { get; set; }
    public int intPEALQueryPending { get; set; }
    public int intPEALQueryPendingPast { get; set; }

    //Added by nibedita behera for cicg Status on 08-Nov-2017
    public string Block { get; set; }
    public string CICGDate { get; set; }
    public string Distict { get; set; }
    public string EndDate { get; set; }
    public string InspectingDept { get; set; }
    public string InspectionDate { get; set; }
    public string InspectorRemark { get; set; }
    public string RescheduledDate { get; set; }
    public string StartDate { get; set; }

    //added by nibedita behera on 20-11-2017
    public string ProjectName { get; set; }
    public string TypeofIssue { get; set; }
    public string IssueCategory { get; set; }
    //public string Pendingdays { get; set; }
    public string InvestorName { get; set; }

    public string strDistApplied { get; set; }
    public string strDistApproved { get; set; }
    public string strDistRejected { get; set; }
    public string strDistUnderEvaltion { get; set; }

    public string strDistDeferred { get; set; }
    public int intPealdistwise { get; set; }
    public string vchCapitalInvestment { get; set; }
    public string vchEmployement { get; set; }
    public string vchYear { get; set; }

    //added by nibedita behera on 13-12-2017 for Land Services
    public string LandAssessment { get; set; }
    public string PropNoForLand { get; set; }
    public string AreaAllotLand { get; set; }
    public string ApplnLandORTPS { get; set; }
    public string ApplnLandAllotedByIDCO { get; set; }

    //added on 18-12-2017
    public int intSecId { get; set; }

    //added by nibedita behera on 23-12-2017 for query details
    public string QueryapplicationNo { get; set; }
    public string QueryRemarks { get; set; }
    public string QueryDate { get; set; }
    public string QueryInvestorName { get; set; }
    public string QueryAvgTime { get; set; }
    public string QueryApplicationDate { get; set; }

    //public string QueryapplicationNo { get; set; }
    //public string QueryRemarks { get; set; }
    //public string QueryDate { get; set; }
    //public string QueryInvestorName { get; set; }
    //public string QueryAvgTime { get; set; }

    #region Added By Sushant Jena

    public string strFilterMode { get; set; } //// Added by Sushant Jena On Dt.14-Aug-2018 

    #endregion

    #region Added By Manoj Kumar Behera

    public string strMinRaiseQuery { get; set; } //// Added by manoj Kumar Behera On Dt.04-Dec-2020
    public string strMaxRaiseQuery { get; set; } //// Added by manoj Kumar Behera On Dt.04-Dec-2020
    public string strMedianRaiseQuery { get; set; } //// Added by manoj Kumar Behera On Dt.04-Dec-2020

    #endregion
}