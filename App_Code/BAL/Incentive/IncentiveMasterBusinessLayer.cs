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
    public class IncentiveMasterBusinessLayer : IIncentiveMasterBusinessLayer
    {
        IncentiveMasterDataLayer objINCTDal = new IncentiveMasterDataLayer();

        #region MyRegion Sushant

        public string Policy_Master_AED(Policy_Master_Entity objPolicy)
        {
            return objINCTDal.Policy_Master_AED(objPolicy);
        }
        public DataSet Policy_Master_View(Policy_Master_Entity objPolicy)
        {
            return objINCTDal.Policy_Master_View(objPolicy);
        }

        public string Sector_Master_AED(Sector_Master_Entity objSecEntity)
        {
            return objINCTDal.Sector_Master_AED(objSecEntity);
        }
        public DataSet Sector_Master_View(Sector_Master_Entity objSecEntity)
        {
            return objINCTDal.Sector_Master_View(objSecEntity);
        }

        public string OG_Master_AED(OG_Master_Entity objOGEntity)
        {
            return objINCTDal.OG_Master_AED(objOGEntity);
        }
        public DataSet OG_Master_View(OG_Master_Entity objSecEntity)
        {
            return objINCTDal.OG_Master_View(objSecEntity);
        }

        public string Inct_Name_Master_AED(Incentive_Master_Entity objIMEntity)
        {
            return objINCTDal.Inct_Name_Master_AED(objIMEntity);
        }
        public DataSet Inct_Name_Master_View(Incentive_Master_Entity objIMEntity)
        {
            return objINCTDal.Inct_Name_Master_View(objIMEntity);
        }

        public IList<Inct_Applied_With_PC_Entity> View_Inct_List_With_PC(Inct_Applied_With_PC_Entity objInctEntity)
        {
            return objINCTDal.View_Inct_List_With_PC(objInctEntity);
        }
        public IList<Inct_Applied_With_PC_Entity> View_Policy_List_With_PC(Inct_Applied_With_PC_Entity objInctEntity)
        {
            return objINCTDal.View_Policy_List_With_PC(objInctEntity);
        }
        public IList<Inct_Applied_With_PC_Entity> View_Summary_With_PC(Inct_Applied_With_PC_Entity objInctEntity)
        {
            return objINCTDal.View_Summary_With_PC(objInctEntity);
        }
        public IList<Inct_Basic_Unit_Details_WPC_Entity> Inct_Application_Count(Inct_Basic_Unit_Details_WPC_Entity objInctEntity)
        {
            return objINCTDal.Inct_Application_Count(objInctEntity);
        }

        public IList<Inct_Drafted_Application_Entity> View_Drafted_Application(Inct_Drafted_Application_Entity objInctEntity)
        {
            return objINCTDal.View_Drafted_Application(objInctEntity);
        }
        public string Drafted_Application_AED(Inct_Drafted_Application_Entity objInctEntity) ///// Added on Dt 02-05-2018 by Sushant Jena
        {
            return objINCTDal.Drafted_Application_AED(objInctEntity);
        }

        public IList<Inct_Application_Details_Entity> View_Application_Details(Inct_Application_Details_Entity objInctEntity)
        {
            return objINCTDal.View_Application_Details(objInctEntity);
        }
        public string Check_Time_Frame(Inct_Applied_With_PC_Entity objIMEntity)
        {
            return objINCTDal.Check_Time_Frame(objIMEntity);
        }
        public string Validate_Inct_Apply(Validate_Inct_Apply_Entity objInctEntity)
        {
            return objINCTDal.Validate_Inct_Apply(objInctEntity);
        }
        public string Basic_Unit_Details_AED(Basic_Unit_Details_Entity objIMEntity)
        {
            return objINCTDal.Basic_Unit_Details_AED(objIMEntity);
        }
        public DataSet Basic_Unit_Details_V(Basic_Unit_Details_Entity objEntity)
        {
            return objINCTDal.Basic_Unit_Details_V(objEntity);
        }
        public DataSet dynamic_name_doc_bind()
        {
            return objINCTDal.dynamic_name_doc_bind();
        }
        public DataSet Bind_Inct_With_Eligible(Basic_Unit_Details_Entity objEntity)
        {
            return objINCTDal.Bind_Inct_With_Eligible(objEntity);
        }
        public RadioButtonList BindRadioButton(RadioButtonList Rad_Btn, IncentiveMaster objIncentive)
        {
            return objINCTDal.BindRadioButton(Rad_Btn, objIncentive);
        }
        public DataSet BindDerivedSector(IncentiveMaster objEntity)
        {
            return objINCTDal.BindDerivedSector(objEntity);
        }
        public DataTable ValidateCertification(Inct_Applied_With_PC_Entity objEntity)
        {
            return objINCTDal.ValidateCertification(objEntity);
        }
        public string Inct_EC_Delay_Reason_AED(Inct_EC_Delay_Reason_Entity objIMEntity)
        {
            return objINCTDal.Inct_EC_Delay_Reason_AED(objIMEntity);
        }
        public DataSet Inct_EC_Delay_Reason_VIEW(Inct_EC_Delay_Reason_Entity objEntity)
        {
            return objINCTDal.Inct_EC_Delay_Reason_VIEW(objEntity);
        }
        public string DelayReason_Approval(Inct_EC_Delay_Reason_Entity objIncentive)
        {
            return objINCTDal.DelayReason_Approval(objIncentive);
        }

        #endregion

        #region Suman
        public int PcPrintDetailsLarge_AED(CertificateDetails objCertificateDetails)
        {
            return objINCTDal.PcPrintDetailsLarge_AED(objCertificateDetails);
        }
        public DropDownList BindDropdown(DropDownList ddlDrop, IncentiveMaster objIncentive)
        {
            return objINCTDal.BindDropdown(ddlDrop, objIncentive);
        }
        public int PC_Large_AED(Incentive_PCMaster objProperties)
        {
            return objINCTDal.PC_Large_AED(objProperties);
        }
        public DataSet Incentive_PcForm_Large_View(PcSearch objSearch)
        {
            return objINCTDal.Incentive_PcForm_Large_View(objSearch);
        }
        public int Incentive_PcDetails_Approve(Incentive_PCMaster objProperties)
        {
            return objINCTDal.Incentive_PcDetails_Approve(objProperties);
        }
        public int IRFormLarge_AED(IRDetails objIrDetails, Incentive_PCMaster objIncentive)
        {
            return objINCTDal.IRFormLarge_AED(objIrDetails, objIncentive);
        }
        public int Incentive_PcDetailsLarge_Approve(Incentive_PCMaster objProperties)
        {
            return objINCTDal.Incentive_PcDetailsLarge_Approve(objProperties);
        }
        public string Incentive_Approval(Inct_Application_Details_Entity objIncentive)
        {
            return objINCTDal.Incentive_Approval(objIncentive);
        }

        public string AddReason(Incentive_PCMaster objProperties)
        {
            return objINCTDal.AddReason(objProperties);
        }
        public DataSet Incentive_LateReason_View(Incentive_PCMaster objIncentive)
        {
            return objINCTDal.Incentive_LateReason_View(objIncentive);
        }
        #endregion

        #region Ritika

        public int PcPrintDetails_AED(CertificateDetails objCertificateDetails)
        {
            return objINCTDal.PcPrintDetails_AED(objCertificateDetails);
        }

        public DataSet BindDropdown(string strActionCode)
        {
            return objINCTDal.BindDropdown(strActionCode);
        }
        public int Incentive_PcDetails_AED(Incentive_PCMaster objProperties)
        {
            return objINCTDal.Incentive_PcDetails_AED(objProperties);
        }
        public DataSet Incentive_PcForm_View(PcSearch objSearch)
        {
            return objINCTDal.Incentive_PcForm_View(objSearch);
        }
        public List<PcApplied> ViewPcAppliedDetails(PcSearch objSearch)
        {
            return objINCTDal.ViewPcAppliedDetails(objSearch);
        }
        public int IRForm_AED(IRDetails objIrDetails, Incentive_PCMaster objIncentive)
        {
            return objINCTDal.IRForm_AED(objIrDetails, objIncentive);
        }

        /// <summary>
        /// Function to get the OSPCB certificate details for the investor when applying for PC
        /// </summary>
        /// <param name="objSearch">InctSearch object</param>
        /// <returns>List of string type</returns>
        public List<string> ViewInctOSPCBDetails(InctSearch objSearch)
        {
            return objINCTDal.ViewInctOSPCBDetails(objSearch);
        }
        #endregion

        public IList<Inct_Application_Details_Entity> View_Application_ApprveFetch(Inct_Application_Details_Entity objInctEntity)
        {
            return objINCTDal.View_Application_ApprveFetch(objInctEntity);
        }

        #region "Added By Pranay Kumar"
        public List<QueryMgntDtls> getInctRaisedQueryDetails(QueryMgntDtls objQueryMgtDtls)
        {
            return objINCTDal.getInctRaisedQueryDetails(objQueryMgtDtls);
        }
        public string IncentivesRaiseQuery(QueryMgntDtls objQueryMgtDtls)
        {
            return objINCTDal.IncentivesRaiseQuery(objQueryMgtDtls);
        }
        public int ExtendDate(string strAction, int intIncentivesUnqNo)
        {
            return objINCTDal.ExtendDate(strAction, intIncentivesUnqNo);
        }

        #region "Query Management for PC-Large"
        public List<QueryMgntDtls> getPCLargeRaisedQueryDetails(QueryMgntDtls objQueryMgtDtls)
        {
            return objINCTDal.getPCLargeRaisedQueryDetails(objQueryMgtDtls);
        }
        public string PCLargeRaiseQuery(QueryMgntDtls objQueryMgtDtls)
        {
            return objINCTDal.PCLargeRaiseQuery(objQueryMgtDtls);
        }
        public int PCLargeExtendDate(string strAction, int intAppNo)
        {
            return objINCTDal.PCLargeExtendDate(strAction, intAppNo);
        }
        #endregion

        #region "Query Management for PC-MSME"
        public List<QueryMgntDtls> getPCMSMERaisedQueryDetails(QueryMgntDtls objQueryMgtDtls)
        {
            return objINCTDal.getPCMSMERaisedQueryDetails(objQueryMgtDtls);
        }
        public string PCMSMERaiseQuery(QueryMgntDtls objQueryMgtDtls)
        {
            return objINCTDal.PCMSMERaiseQuery(objQueryMgtDtls);
        }
        public int PCMSMEExtendDate(string strAction, int intAppNo)
        {
            return objINCTDal.PCMSMEExtendDate(strAction, intAppNo);
        }
        #endregion



        #endregion

        #region "Ritika's Dashboard"


        public List<InctApplicationDetails_Entity> ViewInctApplicationDetailsRpt(InctSearch objSearch)
        {
            return objINCTDal.ViewInctApplicationDetailsRpt(objSearch);
        }

        /// <summary>
        /// Function to get the details of incentive application in dasboard
        /// </summary>
        /// <param name="objSearch">InctSearch</param>
        /// <returns>List of object InctDashBoard_Entity</returns>
        public List<InctDashBoard_Entity> ViewInctDashBoardDetails(InctSearch objSearch)
        {
            return objINCTDal.ViewInctDashBoardDetails(objSearch);
        }
        #endregion

        #region MIS Reports
        /// <summary>
        /// function to get the details for the Incentive Claimwise report
        /// </summary>
        /// <param name="objSearch">InctSearch entity - it will get policytype,financial year and action code</param>
        /// <returns>List of InctWiseClaimReport entity object</returns>
        public List<InctWiseClaimReport> View_MIS_IncentiveWiseClaimDetails(InctSearch objSearch)
        {
            return objINCTDal.View_MIS_IncentiveWiseClaimDetails(objSearch);
        }

        /// <summary>
        /// function to get the details for the Applicant wise claim report
        /// </summary>
        /// <param name="objSearch">InctSearch entity - it will get policytype,financial year and action code</param>
        /// <returns>List of InctWiseClaimReport entity object</returns>
        public List<InctWiseClaimReport> View_MIS_ApplicantUnitWiseClaimDetails(InctSearch objSearch)
        {
            return objINCTDal.View_MIS_ApplicantUnitWiseClaimDetails(objSearch);
        }

        /// <summary>
        /// Function to get the unitwise details for a particular incentive and the status
        /// </summary>
        /// <param name="objSearch">InctSearch object</param>
        /// <returns>List of InctApplicationStatusRpt_Entity</returns>
        public List<InctApplicationStatusRpt_Entity> View_Mis_InctWiseStatusDetails(InctSearch objSearch)
        {
            return objINCTDal.View_Mis_InctWiseStatusDetails(objSearch);
        }

        /// <summary>
        /// Function to show unit wise incentive wise claim details report
        /// </summary>
        /// <param name="objSearch">InctSearch object</param>
        /// <returns>List of InctWiseClaimReport object</returns>
        public List<InctWiseClaimReport> View_MIS_UnitWiseIncentiveClaimDetails(InctSearch objSearch)
        {
            return objINCTDal.View_MIS_UnitWiseIncentiveClaimDetails(objSearch);
        }
        #endregion


        public string Thrust_Priority_AED(Basic_Unit_Details_Entity objIMEntity)
        {
            return objINCTDal.Thrust_Priority_AED(objIMEntity);
        }
        public IList<Inct_Application_Details_Entity> IPR2022View_Application_Details(Inct_Application_Details_Entity objInctEntity)
        {
            return objINCTDal.IPR2022View_Application_Details(objInctEntity);
        }
        public string IPR2022Incentive_Approval(Inct_Application_Details_Entity objIncentive)
        {
            return objINCTDal.IPR2022Incentive_Approval(objIncentive);
        }
        public IList<Inct_Application_Details_Entity> DI_ViewIPR2022_Application_Details(Inct_Application_Details_Entity objInctEntity)
        {
            return objINCTDal.DI_ViewIPR2022_Application_Details(objInctEntity);
        }
        public string Thrust_Priority_Draft(Basic_Unit_Details_Entity objIMEntity)
        {
            return objINCTDal.Thrust_Priority_Draft(objIMEntity);
        }
        public string Stamp_Duty_Exemption_AED(Basic_Unit_Details_Entity objIMEntity)
        {
            return objINCTDal.Stamp_Duty_Exemption_AED(objIMEntity);
        }
        public string Stamp_Duty_Exemption_Draft(Basic_Unit_Details_Entity objIMEntity)
        {
            return objINCTDal.Stamp_Duty_Exemption_Draft(objIMEntity);
        }
        public string Exemption_Land_IndustrialUse(Basic_Unit_Details_Entity objIMEntity)
        {
            return objINCTDal.Exemption_Land_IndustrialUse(objIMEntity);
        }
        public string Exemption_Land_IndustrialUse_Draft(Basic_Unit_Details_Entity objIMEntity)
        {
            return objINCTDal.Exemption_Land_IndustrialUse_Draft(objIMEntity);
        }
        public string Migrated_Industrial_Unit_AED(Basic_Unit_Details_Entity objIMEntity)
        {
            return objINCTDal.Migrated_Industrial_Unit_AED(objIMEntity);
        }
        
        public string Migrated_Industrial_Unit_Draft(Basic_Unit_Details_Entity objIMEntity)
        {
            return objINCTDal.Migrated_Industrial_Unit_Draft(objIMEntity);
        }
    }
}
