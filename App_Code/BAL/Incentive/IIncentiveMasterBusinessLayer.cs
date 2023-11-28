using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using DataAcessLayer.Incentive;
using EntityLayer.Incentive;

/// <summary>
/// Summary description for IIncentiveMasterBusinessLayer
/// </summary>
/// Name
namespace BusinessLogicLayer.Incentive
{
    public interface IIncentiveMasterBusinessLayer
    {
        #region MyRegion Sushant

        string Policy_Master_AED(Policy_Master_Entity objPlcEntity);
        DataSet Policy_Master_View(Policy_Master_Entity objPlcEntity);

        string Sector_Master_AED(Sector_Master_Entity objSecEntity);
        DataSet Sector_Master_View(Sector_Master_Entity objSecEntity);

        string OG_Master_AED(OG_Master_Entity objOGEntity);
        DataSet OG_Master_View(OG_Master_Entity objOGEntity);

        string Inct_Name_Master_AED(Incentive_Master_Entity objIMEntity);
        DataSet Inct_Name_Master_View(Incentive_Master_Entity objIMEntity);

        IList<Inct_Applied_With_PC_Entity> View_Inct_List_With_PC(Inct_Applied_With_PC_Entity objInctEntity);
        IList<Inct_Applied_With_PC_Entity> View_Policy_List_With_PC(Inct_Applied_With_PC_Entity objInctEntity);
        IList<Inct_Applied_With_PC_Entity> View_Summary_With_PC(Inct_Applied_With_PC_Entity objInctEntity);
        IList<Inct_Basic_Unit_Details_WPC_Entity> Inct_Application_Count(Inct_Basic_Unit_Details_WPC_Entity objInctEntity);

        IList<Inct_Drafted_Application_Entity> View_Drafted_Application(Inct_Drafted_Application_Entity objInctEntity);
        string Drafted_Application_AED(Inct_Drafted_Application_Entity objInctEntity); ///// Added on Dt 02-05-2018 by Sushant Jena

        IList<Inct_Application_Details_Entity> View_Application_Details(Inct_Application_Details_Entity objInctEntity);
        string Check_Time_Frame(Inct_Applied_With_PC_Entity objIMEntity);
        string Validate_Inct_Apply(Validate_Inct_Apply_Entity objInctEntity);
        string Basic_Unit_Details_AED(Basic_Unit_Details_Entity objInctEntity);
        DataSet Basic_Unit_Details_V(Basic_Unit_Details_Entity objEntity);
        DataSet dynamic_name_doc_bind();
        DataSet Bind_Inct_With_Eligible(Basic_Unit_Details_Entity objEntity);
        RadioButtonList BindRadioButton(RadioButtonList Rad_Btn, IncentiveMaster objIncentive);
        DataSet BindDerivedSector(IncentiveMaster objEntity);
        DataTable ValidateCertification(Inct_Applied_With_PC_Entity objEntity);

        string Inct_EC_Delay_Reason_AED(Inct_EC_Delay_Reason_Entity objInctEntity);
        DataSet Inct_EC_Delay_Reason_VIEW(Inct_EC_Delay_Reason_Entity objEntity);

        #endregion

        #region MyRegion Suman

        DropDownList BindDropdown(DropDownList ddlDrop, IncentiveMaster objIncentive);
        int PC_Large_AED(Incentive_PCMaster objProperties);
        DataSet Incentive_PcForm_Large_View(PcSearch objSearch);
        int Incentive_PcDetails_Approve(Incentive_PCMaster objProperties);
        int IRFormLarge_AED(IRDetails objIrDetails, Incentive_PCMaster objIncentive);
        int Incentive_PcDetailsLarge_Approve(Incentive_PCMaster objProperties);
        string Incentive_Approval(Inct_Application_Details_Entity objIncentive);
        int PcPrintDetailsLarge_AED(CertificateDetails objCertificateDetails);
        string AddReason(Incentive_PCMaster objProperties);
        DataSet Incentive_LateReason_View(Incentive_PCMaster objIncentive);

        #endregion

        #region MyRegion Ritika

        DataSet BindDropdown(string strActionCode);
        int Incentive_PcDetails_AED(Incentive_PCMaster objProperties);
        DataSet Incentive_PcForm_View(PcSearch objSearch);
        List<PcApplied> ViewPcAppliedDetails(PcSearch objSearch);
        int IRForm_AED(IRDetails objIrDetails, Incentive_PCMaster objIncentive);
        int PcPrintDetails_AED(CertificateDetails objCertificateDetails);
        /// <summary>
        /// Function to get the OSPCB certificate details for the investor when applying for PC
        /// </summary>
        /// <param name="objSearch">InctSearch object</param>
        /// <returns>List of string type</returns>
        List<string> ViewInctOSPCBDetails(InctSearch objSearch);
        #endregion

        IList<Inct_Application_Details_Entity> View_Application_ApprveFetch(Inct_Application_Details_Entity objInctEntity);

        #region "Added By Pranay Kumar"
        List<QueryMgntDtls> getInctRaisedQueryDetails(QueryMgntDtls objQueryMgtDtls);
        string IncentivesRaiseQuery(QueryMgntDtls objQueryMgtDtls);
        int ExtendDate(string strAction, int intIncentivesUnqNo);

        #region "Query Management for PC-Large"
        List<QueryMgntDtls> getPCLargeRaisedQueryDetails(QueryMgntDtls objQueryMgtDtls);
        string PCLargeRaiseQuery(QueryMgntDtls objQueryMgtDtls);
        int PCLargeExtendDate(string strAction, int intAppNo);
        #endregion

        #region "Query Management for PC-MSME"
        List<QueryMgntDtls> getPCMSMERaisedQueryDetails(QueryMgntDtls objQueryMgtDtls);
        string PCMSMERaiseQuery(QueryMgntDtls objQueryMgtDtls);
        int PCMSMEExtendDate(string strAction, int intAppNo);
        #endregion

        #endregion

        #region "Ritika's Dashboard"
        List<InctApplicationDetails_Entity> ViewInctApplicationDetailsRpt(InctSearch objSearch);
        List<InctDashBoard_Entity> ViewInctDashBoardDetails(InctSearch objSearch);
        #endregion

        #region MIS Reports
        /// <summary>
        /// function to get the details for the Incentive Claimwise report
        /// </summary>
        /// <param name="objSearch">InctSearch entity - it will get policytype,financial year and action code</param>
        /// <returns>List of InctWiseClaimReport entity object</returns>
        List<InctWiseClaimReport> View_MIS_IncentiveWiseClaimDetails(InctSearch objSearch);

        /// <summary>
        /// function to get the details for the Applicant wise claim report
        /// </summary>
        /// <param name="objSearch">InctSearch entity - it will get policytype,financial year and action code</param>
        /// <returns>List of InctWiseClaimReport entity object</returns>
        List<InctWiseClaimReport> View_MIS_ApplicantUnitWiseClaimDetails(InctSearch objSearch);

        /// <summary>
        /// Function to get the unitwise details for a particular incentive and the status
        /// </summary>
        /// <param name="objSearch">InctSearch object</param>
        /// <returns>List of InctApplicationStatusRpt_Entity</returns>
        List<InctApplicationStatusRpt_Entity> View_Mis_InctWiseStatusDetails(InctSearch objSearch);

        /// <summary>
        /// Function to show unit wise incentive wise claim details report
        /// </summary>
        /// <param name="objSearch">InctSearch object</param>
        /// <returns>List of InctWiseClaimReport object</returns>
        List<InctWiseClaimReport> View_MIS_UnitWiseIncentiveClaimDetails(InctSearch objSearch);
        #endregion

        string Thrust_Priority_AED(Basic_Unit_Details_Entity objInctEntity);

        string Thrust_Priority_Draft(Basic_Unit_Details_Entity objInctEntity);
        string Stamp_Duty_Exemption_AED(Basic_Unit_Details_Entity objInctEntity);
        string Stamp_Duty_Exemption_Draft(Basic_Unit_Details_Entity objInctEntity);
    }
}
