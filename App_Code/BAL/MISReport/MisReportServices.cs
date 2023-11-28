using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for MisReportServices
/// </summary>
public class MisReportServices
{
    private static  IMisReportServiceProvider eProvider;
    public static IMisReportServiceProvider Provider
    {
        get
        {
            eProvider = new MisReportServiceProvider();
            return eProvider;
        }
    }

    public MisReportServices()
    {

    }

    #region Created by Ritika Lath for Child Service report on 31st Jan 2018
    public static List<MIS_ChildServiceRpt> View_ChildServices_MISReport(RptSearch objSearch)
    {
        return Provider.View_ChildServices_MISReport(objSearch);
    }

    public static List<MIS_ChildServiceRpt> View_ChildServices_MISReport_New(RptSearch objSearch)
    {
        return Provider.View_ChildServices_MISReport_New(objSearch);
    }

  
    public static List<Mis_ChildServiceDtls> View_DetailsChildServices_MISReport(RptSearch objSearch)
    {
        return Provider.View_DetailsChildServices_MISReport(objSearch);
    }

    public static List<Mis_ChildServiceDtls> View_ChildServices_ApplicationDtls_MISReport(RptSearch objSearch)
    {
        return Provider.View_ChildServices_ApplicationDtls_MISReport(objSearch);
    }

    public static List<Mis_ChildServiceDtls> View_ChildServices_ApplicationDtls_MISReport_New(RptSearch objSearch)
    {
        return Provider.View_ChildServices_ApplicationDtls_MISReport_New(objSearch);
    }

    public static Dictionary<int, string> ViewDepartmentListByUser(RptSearch objSearch)
    {
        return Provider.ViewDepartmentListByUser(objSearch);
    }

    public static List<PealMisReport> PealUserwiseMisRpt(PealSearch objprop)
    {
        return Provider.PealUserwiseMisRpt(objprop);
    }

    public static List<Mis_ChildServiceDtls> PealMisRpt_Details(PealSearch objSearch)
    {
        return Provider.PealMisRpt_Details(objSearch);
    }

    public static List<Mis_ChildServiceDtls> PealMisQueryRpt_Details(PealSearch objSearch)
    {
        return Provider.PealMisQueryRpt_Details(objSearch);
    }

    public static List<PealMisReport> PEAL_MisReportLogic2(PealSearch objSearch)
    {
        return Provider.PEAL_MisReportLogic2(objSearch);
    }

    public static List<Mis_ChildServiceDtls> PEAL_MisReportLogic2_Details(PealSearch objSearch)
    {
        return Provider.PEAL_MisReportLogic2_Details(objSearch);
    }


    public static List<MIS_ChildServiceRpt> View_ChildServices_UserWise_MISReport(RptSearch objSearch)
    {
        return Provider.View_ChildServices_UserWise_MISReport(objSearch);
    }

    public static List<Mis_ChildServiceDtls> View_ChildServices_UserWiseDetails_MISReport(RptSearch objSearch)
    {
        return Provider.View_ChildServices_UserWiseDetails_MISReport(objSearch);
    }
    public static List<MIS_ChildServiceRpt> View_ChildServices_District_Rpt(RptSearch objSearch)
    {
        return Provider.View_ChildServices_District_Rpt(objSearch);
    }

    public static List<Mis_ChildServiceDtls> View_ChildServices_District_Details_Rpt(RptSearch objSearch)
    {
        return Provider.View_ChildServices_District_Details_Rpt(objSearch);
    }
    #endregion

    #region Created by Monalisa Nayak  on 31st Jan 2018 to bind Investor Details

    public static List<MIS_InvestorRpt> View_ChildServices_Investor_MISReport(InvestorRptSearch objSearch)
    {
        return Provider.View_ChildServices_Investor_MISReport(objSearch);
    }
    public static List<rtnIndustry> View_Industry(IndustrySearch objSearch)
    {
        return Provider.View_Industry(objSearch);
    }
    #endregion
    public static DataTable FillDeptUserName(IndustrySearch objSearch)
    {
        return Provider.FillDeptUserName(objSearch);
    }
    public static DataTable FillUserDetails(IndustrySearch objSearch)
    {
        return Provider.FillUserDetails(objSearch);
    }

    #region "Added By BHAGYASHREE DAS FOR INCENTIVE MIS REPORT"
    public List<IncentiveMisReport> GetIncentiveMISReportDtls(RptSearch objSearch)
    {
        return Provider.GetIncentiveMISReportDtls(objSearch);
    }
    #endregion

    public static DataTable GetGrievanceMISReportDtls(GrievanceMisSearch objSearch)
    {
        return Provider.GetGrievanceMISReportDtls(objSearch);
    }

    public static DataTable GetStatusWiseGrievanceMISReportDtls(GrievanceMisSearch objSearch)
    {
        return Provider.GetStatusWiseGrievanceMISReportDtls(objSearch);
    }

    //Created by Debiprasanna 05-09-22
    public static List<MIS_InvestorRpt> View_DistWise_Investor_Report(InvestorRptSearch objSearch)
    {
        return Provider.View_DistWise_Investor_Report(objSearch);
    }
//Created by Debiprasanna on 12-09-22
    public static List<MIS_InvestorRpt> View_Pending_Otp_Report(InvestorRptSearch objSearch)
    {
        return Provider.View_Pending_Otp_Report(objSearch);
    }
    //Created by Debiprasanna on 21-09-22
    //public static List<IndustrywiseApplicationReport> View_Industrywise_Application_Report(InvestorRptSearch objSearch)
    //{
    //    return Provider.View_Industrywise_Application_Report(objSearch);
    //}


    public static List<Financialreport> View_FinancilaReport(Financialreport objSearch)
    {
        return Provider.View_FinancilaReport(objSearch);
    }
    public static List<SMSCountReport> View_SMSCounrReport(SMSCountReport objSearch)
    {
        return Provider.View_SMSCounrReport(objSearch);
    }
    public static List<SMSCountReport> View_SMSType(SMSCountReport objSearch)
    {
        return Provider.View_SMSType(objSearch);
    }
    public static DataTable View_ChildSMSCounrReport(string SMSType)
    {
        return Provider.View_ChildSMSCounrReport(SMSType);
    }
}