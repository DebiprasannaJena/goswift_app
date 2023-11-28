using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration.Provider;
using System.Data;

/// <summary>
/// Summary description for IMisReportServiceProvider
/// </summary>
public abstract class IMisReportServiceProvider : ProviderBase
{
    public IMisReportServiceProvider()
    {

    }

    #region Created by Ritika Lath for Child Service report on 31st Jan 2018

    public abstract List<MIS_ChildServiceRpt> View_ChildServices_MISReport(RptSearch objSearch);

    public abstract List<MIS_ChildServiceRpt> View_ChildServices_MISReport_New(RptSearch objSearch);

    public abstract List<Mis_ChildServiceDtls> View_DetailsChildServices_MISReport(RptSearch objSearch);

    public abstract List<Mis_ChildServiceDtls> View_ChildServices_ApplicationDtls_MISReport(RptSearch objSearch);
    public abstract List<Mis_ChildServiceDtls> View_ChildServices_ApplicationDtls_MISReport_New(RptSearch objSearch);

    public abstract Dictionary<int, string> ViewDepartmentListByUser(RptSearch objSearch);

    public abstract List<PealMisReport> PealUserwiseMisRpt(PealSearch objprop);

    public abstract List<Mis_ChildServiceDtls> PealMisRpt_Details(PealSearch objSearch);

    public abstract List<Mis_ChildServiceDtls> PealMisQueryRpt_Details(PealSearch objSearch);

    public abstract List<MIS_ChildServiceRpt> View_ChildServices_UserWise_MISReport(RptSearch objSearch);

    public abstract List<Mis_ChildServiceDtls> View_ChildServices_UserWiseDetails_MISReport(RptSearch objSearch);

    public abstract List<MIS_ChildServiceRpt> View_ChildServices_District_Rpt(RptSearch objSearch);

    public abstract List<Mis_ChildServiceDtls> View_ChildServices_District_Details_Rpt(RptSearch objSearch);

    public abstract List<PealMisReport> PEAL_MisReportLogic2(PealSearch objSearch);

    public abstract List<Mis_ChildServiceDtls> PEAL_MisReportLogic2_Details(PealSearch objSearch);
    #endregion
    #region Created by Monalisa Nayak  on 31st Jan 2018 to bind Investor Details

    public abstract List<MIS_InvestorRpt> View_ChildServices_Investor_MISReport(InvestorRptSearch objSearch);
    public abstract List<rtnIndustry> View_Industry(IndustrySearch objSearch);

    #endregion

    public abstract DataTable FillDeptUserName(IndustrySearch objSearch);
    public abstract DataTable FillUserDetails(IndustrySearch objSearch);

    #region Created by Bhagyashree Das  on 28th Dec 2020 to bind Incentive Details

    public abstract List<IncentiveMisReport> GetIncentiveMISReportDtls(RptSearch objSearch);

    #endregion

    public abstract DataTable GetGrievanceMISReportDtls(GrievanceMisSearch objSearch);
    public abstract DataTable GetStatusWiseGrievanceMISReportDtls(GrievanceMisSearch objSearch);

    //Created by Debiprasanna 05-09-22 to view the district wise invetor report
    public abstract List<MIS_InvestorRpt> View_DistWise_Investor_Report(InvestorRptSearch objSearch);

    //Created by Debiprasanna on 12-09-22 to view Otp pending Status report
    public abstract List<MIS_InvestorRpt> View_Pending_Otp_Report(InvestorRptSearch objSearch);
    //public abstract List<IndustrywiseApplicationReport> View_Industrywise_Application_Report(InvestorRptSearch objSearch);

    public abstract List<Financialreport> View_FinancilaReport(Financialreport objSearch);

    public abstract List<SMSCountReport> View_SMSCounrReport(SMSCountReport objSearch);

    public abstract List<SMSCountReport> View_SMSType(SMSCountReport objSearch);
    public abstract DataTable View_ChildSMSCounrReport(string SMSType);



    
}