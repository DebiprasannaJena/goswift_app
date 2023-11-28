using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


//Added By Ritika Lath for the ChildServices MIS Report on 31st Jan 2018
public class MIS_ChildServiceRpt
{
    public string strParentName { get; set; }
    public string strDeptName { get; set; }
    public int intTotalApplication { get; set; }
    public int intTotalApproved { get; set; }
    public int intTotalRejected { get; set; }
    public int intTotalPending { get; set; }
    public int intTotalQueryRaised { get; set; }
    public int intAvgDaysApproval { get; set; }
    public int intKey { get; set; }
    public int intRowCount { get; set; }
    public int intAvgDaysApprovalTotal { get; set; }
    public int intTotalORTPSAtimelinePassed { get; set; }
    public string strDistName { get; set; }
    public int intCarryFwdPending { get; set; }
    public int intAllTotalPending { get; set; }
    public int intTotalDeferred { get; set; } //// Added by Sushant Jena On Dated:-08-May-2020
    public int intTotalForwarded { get; set; }//// Added by Sushant Jena On Dated:-08-May-2020
    public decimal decMedian { get; set; }//// Added by Sushant Jena On Dated:-25-Nov-2020
    public int intORTPSAtimeline { get; set; }//Added by Bhagyashree Das on Dated: 21-Dec-2020
    public int intMinApprovalDays { get; set; }//Added by Bhagyashree Das on Dated: 21-Dec-2020
    public int intMaxApprovalDays { get; set; }//Added by Bhagyashree Das on Dated: 21-Dec-2020
}

public class RptSearch
{
    public int intIntPageIndex { get; set; }
    public int intPageSize { get; set; }
    public int intDepartmentId { get; set; }
    public int intServiceId { get; set; }
    public int intYear { get; set; }
    public string strActionCode { get; set; }
    public int intUserId { get; set; }
    public int intStatus { get; set; }
    public string strFromDate { get; set; }
    public string strToDate { get; set; }
    public int intDistrictId { get; set; }

}

public class Mis_ChildServiceDtls
{
    public string ProposalNo { get; set; }
    public string ServiceName { get; set; }
    public string strCompany { get; set; }
    public string strBlock { get; set; }
    public string strSector { get; set; }
    public string strSubSector { get; set; }
    public int intPropEmployment { get; set; }
    public Decimal decInvestment { get; set; }
    public string FirstTimeQuery { get; set; }
    public string FirstResponse { get; set; }
    public string SecondQuery { get; set; }
    public string SecondResponse { get; set; }
    public int intRowCount { get; set; }
    public string strDepartment { get; set; }
    public string strUsername { get; set; }
    public string strApplicationNo { get; set; }
    public string strApplicationDate { get; set; }
    public string strApprovalDate { get; set; }
    public decimal decPaymentAmt { get; set; }
    public string strDistName { get; set; }
    public string strORTPSATimelineDate { get; set; }
    // Added By Manoj Kumar Behera
    public decimal decTotalLandRequired { get; set; }
    public decimal decLandRecommendedToIdco { get; set; }

    public string strRemarks { get; set; } //add by anil
}

public class PealSearch
{
    public int intIntPageIndex { get; set; }
    public int intPageSize { get; set; }
    public int intDistrictId { get; set; }
    public int intProjectType { get; set; }
    public int intSectorId { get; set; }
    public int intInvestmentAmt { get; set; }
    public int intYearOfApplication { get; set; }
    public int intYearOfApproval { get; set; }
    public string strActionCode { get; set; }
    public int intUserId { get; set; }
    public int intStatus { get; set; }
    public string strFromDate { get; set; }
    public string strToDate { get; set; }
}

public class PealMisReport
{
    public int intDistrictId { get; set; }
    public string strDistrictName { get; set; }
    public int cnt_Total { get; set; }
    public int cnt_Pending { get; set; }
    public int cnt_Approved { get; set; }
    public int cnt_rejected { get; set; }
    public int cnt_Query { get; set; }
    public int cnt_Proposed_Emp { get; set; }
    public decimal total_Capital_Investment { get; set; }
    public int cnt_landAssessment { get; set; }
    public int cnt_landAllotment { get; set; }
    public int cnt_AvgDaysApproval { get; set; }
    public int cnt_AvgDaysAllotment { get; set; }
    public int cnt_Total_AvgDaysApproval { get; set; }
    public int cnt_Total_AvgDaysAllotment { get; set; }
    public int cnt_Total_ORTPSAtimeline { get; set; }
    public int cnt_deferred { get; set; }
    public int cnt_Land_Allotment_ORTPSA { get; set; }
    public int cnt_CarryFwd_pending { get; set; }
    public int intORTPSAtimeline { get; set; }//Added by Bhagyashree Das on Dated: 22-Dec-2020
    public int intMinApprovalDays { get; set; }//Added by Bhagyashree Das on Dated: 22-Dec-2020
    public int intMaxApprovalDays { get; set; }//Added by Bhagyashree Das on Dated: 22-Dec-2020

    //added by Ritika Lath on 4th April 2018
    public int int_Total_Pending { get; set; }
    public int cnt_median { get; set; }
}

//Added by BHAGYASHREE DAS 
public class IncentiveMisReport
{
    public string INCAPLLIED { get; set; }
    public string INCSANCTIONED { get; set; }
    public string INCPENDING { get; set; }
    public string INCREJECTED { get; set; }
    public string strIncMean { get; set; }
    public string strIncMedian { get; set; }
    public string Department { get; set; }
    public string Incentive { get; set; }
    public int Timeline { get; set; }
    public int minSactionDays { get; set; }
    public int maxSactionDays { get; set; }
}

public class PEAL_Update_Payment_Entity
{
    public string strProposalNo { get; set; }
    public decimal decChallanAmt { get; set; }
    public string strAppliedDate { get; set; }
    public string strInvestorName { get; set; }
    public string strPaymentDate { get; set; }
    public string strAction { get; set; }
    public int intCreatedBy { get; set; }
    public string strChallanRefId { get; set; }
    public string strBankTransactionId { get; set; }
    public string strOrderNo { get; set; }
    public int intReqId { get; set; }
}
//Added by Monalisa Nayak To bind Investor Details
public class InvestorRptSearch
{
    public string strActionCode { get; set; }
    public int intDistrictId { get; set; }
    public int intBlockId { get; set; }
    public int intSectorId { get; set; }
    public int intSubsectorId { get; set; }
    public int intCompanyId { get; set; }
    public string strPanNo { get; set; }

    public int IntIndustyType { get; set; }        // Added by Anil sahoo
    public string StrRegdSource  { get; set; }     // Added by Anil sahoo
    public string StrFromDate { get; set; }        // Added by Anil sahoo
    public string StrToDate { get; set; }          // Added by Anil sahoo

    public string StrLicenceNoType { get; set; }   // Added by Dharmasis sahoo
    public int IntCategory { get; set; }           // Added by Dharmasis sahoo 
    public string strInvLevel { get; set; }        // Added by Dharmasis sahoo 
    public string strInvestorName { get; set; }  //Add by Debiprasanna
   
}
public class MIS_InvestorRpt
{
    public string strInvestorName { get; set; }
    public string strUserId { get; set; }
    public string strEmailId { get; set; }
    public string strContactPersn { get; set; }
    public string strAddress { get; set; }
    public string strMobile { get; set; }
    public string VCH_EIN_IEM { get; set; }
    public string DTM_CREATED_ON { get; set; }
    public string vchDistrictName { get; set; }
    public string vchBlockName { get; set; }

    public string StrIndustyType { get; set; }      // Add anil sahoo
    public string StrRegdSource  { get; set; }      // Add anil sahoo
    public int IntIndustryType { get; set; }        // Add anil sahoo
    public string StrLicenceNoType  { get; set; }   // Add anil sahoo

    public int IntCategory { get; set; }            //Added by Dharmasis sahoo 
    public string strInvLevel { get; set; }         //Added by Dharmasis sahoo 
    public string vchSectorName { get; set; }
    public string vchSubSectorName { get; set; }
    public string strPanNo { get; set; }  //Add by Debiprasanna
    public int IntOtpStatus { get; set; }  //Add by Debiprasanna

}
public class IndustrySearch
{
    public string strActionCode { get; set; } 
    public int INT_USER_ID { get; set; }
}
public class rtnIndustry
{
    public int INT_INVESTOR_ID { get; set; }
    public string VCH_INV_NAME { get; set; }
}

public class GrievanceMisSearch
{
    public int IntIntPageIndex { get; set; }
    public int IntPageSize { get; set; }
    public int IntDistrictId { get; set; }
    public string StrActionCode { get; set; }
    public int IntStatus { get; set; }
    public string StrFromDate { get; set; }
    public string StrToDate { get; set; }
    public int IntGrivTypeId { get; set; }
}

//Add by Debiprasanna on 21-09-22
public class IndustrywiseApplicationReport
{
    public string strInvestorName { get; set; }
    public string Str_USER_ID { get; set; }
    public string strInvLevel { get; set; }
    public int Int_PROPOSAL_Count { get; set; }
    public int Int_SERVICE_Count { get; set; }
    public int Int_INCENTIVE_Count { get; set; }
    public int Int_GRIEVANCE_Count { get; set; }
    public int Int_LARGE_PC_Count { get; set; }
    public int Int_SMALL_PC_Count { get; set; }
    public int Int_PPC_Count { get; set; }



}

public class Financialreport
{
    public string strActionCode { get; set; }
    public string strProposalnumber { get; set; }
    public string strInvestment_Amount { get; set; }
    public string strFees { get; set; }
    public string strStatus { get; set; }
    public decimal decFee { get; set; }
    public decimal decInvestamount { get; set; }
    public int intStatus { get; set; }

    public string StrFromDate { get; set; }
    public string StrToDate { get; set; }

}

public class SMSCountReport
{
    public string strActionCode { get; set; }
    public int intNumberofSMS { get; set; }
    public string StrSMSType { get; set; }
    public string StrFromDate { get; set; }
    public string StrToDate { get; set; }
    public int intSMSTypeId { get; set; }
    public string strApplicationno { get; set; }
    public string strMobileno { get; set; }
    public string strSMS_Conent { get; set; }
    public string StrCreatedon{ get; set; }
}



