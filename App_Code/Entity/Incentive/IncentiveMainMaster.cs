using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IncentiveMaster
/// </summary>
namespace EntityLayer.Incentive
{
    #region myRegion Suman

    public class IncentiveMaster
    {
        public string Action { get; set; }
        public int Param { get; set; }
        public int Param_1 { get; set; }
        public string Param_2 { get; set; }
        public string Param_3 { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }


    #endregion

    #region MyRegion Sushant

    public class Policy_Master_Entity
    {
        public string strAction { get; set; }
        public int intSectorId { get; set; }
        public int intSubSectorId { get; set; }
        public int intPolicyCat { get; set; }
        public int intPolicyId { get; set; }
        public string strPolicyCode { get; set; }
        public string strPolicyName { get; set; }
        public string strEffectiveDate { get; set; }
        public string strDecription { get; set; }
        public string strPolicyDocs { get; set; }
        public string strAmendmentDoc { get; set; }
        public int intCreatedBy { get; set; }
        public int intPageNo { get; set; }
        public int intPageSize { get; set; }
        public string strPlcIds { get; set; }
        public List<SectionMasterItem> listSectionItem { get; set; }

        //public string strSectorName { get; set; }
        //public int IndustryGrpId { get; set; }
        //public string IndustryName { get; set; }
        //public string plcyAction { get; set; }
        //public int Sector { get; set; }
        //public string SpecifySector { get; set; }
        //public int Sectoral { get; set; }
        //public int IndustryType { get; set; }
        //public int SpecificIndustryType { get; set; }
        //public int SpecificSector { get; set; }
        //public string Section { get; set; }
        //public string SectionName { get; set; }
        //public int TotalCount { get; set; }
        //public int Serial { get; set; }
        //public int IsProvisional { get; set; }
        //public string ProvisonalCertificate { get; set; }
    }

    [Serializable]
    public class SectionMasterItem
    {
        public string vchSectionNo { get; set; }
        public string vchSectionName { get; set; }
    }

    public class Sector_Master_Entity
    {
        public string strAction { get; set; }
        public int intPolicyId { get; set; }
        public int intSecTagId { get; set; }
        public int intSubSecTagId { get; set; }
        public int intSectorId { get; set; }
        public string strDescription { get; set; }
        public int intCreatedBy { get; set; }
        public int bitSectoralPolicy { get; set; }
        public int bitPriorityIPR { get; set; }
        public int intPageNo { get; set; }
        public int intPageSize { get; set; }
        public string strSectorTagIds { get; set; }

        //public int SubSectorID { get; set; }
        //public string SectoralPolicy { get; set; }
        //public string PriorityIPR { get; set; }      
        //public int UpdatedBy { get; set; }
        //public int DeletedFlag { get; set; }
        //public string SECTOR_NAME { get; set; }       
        //public int TotalCount { get; set; }
        //public int Serial { get; set; }

    }
    public class OG_Master_Entity
    {
        public int? intOGId { get; set; }
        public int? intPlcId { get; set; }
        public string strPlcName { get; set; }
        public string strOGName { get; set; }
        public string strOGDoc { get; set; }
        public string strOGEffcDate { get; set; }
        public string strSectionNo { get; set; }
        public string strDesc { get; set; }
        public string strAction { get; set; }
        public int intCreatedBy { get; set; }

        public DateTime dtmOGEffcDate { get; set; }

        public int intPageSize { get; set; }
        public int intPageNo { get; set; }

        public int intTotalCount { get; set; }
        public int intSerialNo { get; set; }

        public string strOGIds { get; set; }
    }
    public class Incentive_Master_Entity
    {
        public string strAction { get; set; }
        public int? intInctId { get; set; }
        public string strInctName { get; set; }
        public int? intOGId { get; set; }
        public int intDisburseType { get; set; }
        public int intAvailType { get; set; }
        public int intInctNature { get; set; }
        public int intMaxLimit { get; set; }
        public int? intMaxLimitPriority { get; set; }
        public int? intMaxLimitPioneer { get; set; }
        public int intTimeFrame { get; set; }
        public string strPeriodicity { get; set; }
        public int intIsProvisional { get; set; }
        public string strShortCode { get; set; }
        public string strProvFileName { get; set; }
        public int intCreatedBy { get; set; }

        public string strDisburseType { get; set; }
        public string strAvailType { get; set; }
        public string strInctNature { get; set; }
        public string strOGName { get; set; }

        public int intPageSize { get; set; }
        public int intPageNo { get; set; }
        public int intTotalCount { get; set; }
        public int intSerialNo { get; set; }

        public string strInctIds { get; set; }
    }

    public class Inct_Applied_With_PC_Entity
    {
        public int intInvestorId { get; set; }
        public string strAction { get; set; }

        public int intInctId { get; set; }
        public string strInctName { get; set; }
        public string strFormId { get; set; }
        public string strOGName { get; set; }
        public string strOGDoc { get; set; }
        public string strPlcName { get; set; }
        public string strDisburseType { get; set; }
        public string strAvailType { get; set; }
        public string strInctNature { get; set; }

        public string strCompName { get; set; }
        public string dtmFFCI { get; set; }
        public string dtmProdComm { get; set; }
        public string strUnitCat { get; set; }
        public string strIndustryCode { get; set; }

        public string intSectorId { get; set; }
        public string strSectorName { get; set; }
        public string strTotCapInvest { get; set; }
        public string strPlantMachInvest { get; set; }
        public string strDistCat { get; set; }
        public string strRating { get; set; }

        public string strProvFileName { get; set; }
        public string strPlcCat { get; set; }

        public int intFinancialYear { get; set; }
        public string strInctFlow { get; set; }
    }
    public class Inct_Basic_Unit_Details_WPC_Entity
    {
        public string strUserID { get; set; }
        public string strFilterMode { get; set; }

        //public string strFFCIDate { get; set; }
        //public string strProdCommDate { get; set; }
        //public string strSectorName { get; set; }
        //public string strDistrictName { get; set; }
        //public string strUnitCategory { get; set; }
        //public string strUnitType { get; set; }
        //public string strTotalEmp { get; set; }
        //public string strTotalLocalEmp { get; set; }
        //public string strFFCIAmt { get; set; }
        //public string strTotalInvest { get; set; }
        //public string strCapitalInvest { get; set; }
        //public string strProposalNo { get; set; }
        //public string strPCNo { get; set; }

        //public string strIndustryCode { get; set; }
        //public string strCompName { get; set; }
        //public DateTime dtmFFCIDate { get; set; }
        //public DateTime dtmProdCommDate { get; set; }
        //public int intSectorId { get; set; }
        //public int intSubSectorId { get; set; }
        //public string strDerivedSector { get; set; }

        //public int intSectoralPolicy { get; set; }
        //public int intPriorityIPR { get; set; }

        //public string strPriority { get; set; }
        //public int intDistrictId { get; set; }
        //public int intUnitType { get; set; }
        //public int intUnitCat { get; set; }
        //public int intManager { get; set; }
        //public int intSupervisor { get; set; }
        //public int intSkilled { get; set; }
        //public int intSemiSkilled { get; set; }
        //public int intUnSkilled { get; set; }
        //public int intTotal1 { get; set; }
        //public int intSC { get; set; }
        //public int intST { get; set; }
        //public int intWomen { get; set; }
        //public int intPHD { get; set; }
        //public int intTotal2 { get; set; }
        //public decimal decFFCIAmt { get; set; }
        //public decimal decLandAmt { get; set; }
        //public decimal decBuildingAmt { get; set; }
        //public decimal decPlantMachAmt { get; set; }
        //public decimal decOtherAmt { get; set; }
        //public decimal decTotalAmt { get; set; }
        //public int intCreatedBy { get; set; }

        //public string strUnitTypeDoc { get; set; }

        public int intDraftCount { get; set; }
        public int intApprovedCount { get; set; }
        public int intScrutinyCount { get; set; }
        public int intRejectedCount { get; set; }
        public int intTotalAppCount { get; set; }
        public int intDisburseCount { get; set; }
    }
    public class Inct_Drafted_Application_Entity
    {
        public int intInctUniqueId { get; set; }
        public string strUserID { get; set; }
        public string strInctName { get; set; }
        public int intInctId { get; set; }
        public DateTime dtmCreatedOn { get; set; }
        public string strFormId { get; set; }
        public string strIndustryCode { get; set; }
    }
    public class Inct_Application_Details_Entity
    {
        //Added By Pranay Kumar on 11-OCT-2017
        public string strQueryStatus { get; set; }
        public int intQueryStatus { get; set; }
        //Ended By Pranay Kumar

        public string strUserID { get; set; }
        public string strAction { get; set; }

        public string strAppNo { get; set; }
        public string strUnitName { get; set; }
        public string strInctName { get; set; }
        public DateTime dtmCreatedOn { get; set; }
        public string strStatus { get; set; }

        public int? intInctId { get; set; }
        public int? intStatus { get; set; }

        public int intPageSize { get; set; }
        public int intPageNo { get; set; }

        public int intTotalCount { get; set; }
        public int intSerialNo { get; set; }

        public int IsProvisional { get; set; }
        public string ProvisionalCertificate { get; set; }

        public string strApplicationNum { get; set; }
        public string strFormPreviewId { get; set; }

        public string strSanFileName { get; set; }

        public int INTINCUNQUEID { get; set; }
        public string VchInctNum { get; set; }
        public int intAvailType { get; set; }
        public string DocName { get; set; }
        public string Remark { get; set; }
        public string VCHPROVDOCCODE { get; set; }
        public string VCHSANCDOCCODE { get; set; }

        public long DisburseNo { get; set; }
        public decimal DisburseAmount { get; set; }
        public string DisburseDate { get; set; }
        public int? intDisburseStatus { get; set; }
        public int intDisburseType { get; set; }
        public string DisbursementDocument { get; set; }
        public string DisburseTime { get; set; }
        public string BankName { get; set; }

        public int CreatedBy { get; set; }
        public string EMAILId { get; set; }
        public string MOBILENo { get; set; }
        public int intEmpowerment { get; set; }
        public decimal SanctionedAmount { get; set; }

        public string strEsignStatus { get; set; }
        public string strFilterMode { get; set; }
        public int intActionTakenBy
        {
            get;set;
        }
        public int intSectorStatus
        {
            get; set;
        }
        public int vchSanFileName
        {
            get; set;
        }
        
    }
    public class Validate_Inct_Apply_Entity
    {
        public int intUserID { get; set; }
    }
    public class Basic_Unit_Details_Entity
    {
        public string strIndustryCode { get; set; }
        public string intIndustrailUnit { get; set; }
        public string strEnterpriseName { get; set; }
        public int intOrganisationType { get; set; }
        public string strIndustryAddress { get; set; }
        public int intUnitCat { get; set; }
        public int intUnitType { get; set; }
        public string strDocCode { get; set; }
        public string strUnitTypeDoc { get; set; }
        public int intPriority { get; set; }
        public int intPioneer { get; set; }
        public string strPioneerCertificate { get; set; }
        public string strPioneerCertificateCode { get; set; }
        public string strRegisteredOfcAddress { get; set; }
        public string strManagingPartnerGender { get; set; }
        public string strManagingPartnerName { get; set; }
        public string strCertOfRegdDocCode { get; set; }
        public string strCertOfRegdDocFileName { get; set; }
        public string strEINNO { get; set; }
        public string dtmEIN { get; set; }
        public string strPcNoBefore { get; set; }
        public string dtmProdCommBefore { get; set; }
        public string dtmPCIssueDateBefore { get; set; }
        public string strProdCommCertBeforeCode { get; set; }
        public string strPcNoAfter { get; set; }
        public string strProdCommCertBefore { get; set; }
        public string dtmProdCommAfter { get; set; }
        public string dtmPCIssueDateAfter { get; set; }
        public string strProdCommCertAfterCode { get; set; }
        public string strProdCommCertAfter { get; set; }
        public int intDistrictCode { get; set; }
        public int intSectorId { get; set; }
        public int intSubSectorId { get; set; }
        public string strDerivedSector { get; set; }
        public int bitSectoralPolicy { get; set; }
        public int bitPriorityIPR { get; set; }
        public string strGSTIN { get; set; }

        public int intIsAncillary { get; set; }
        public string strAncillaryFileName { get; set; }
        public string strAncillaryDocCode { get; set; }

        public int intCompNature { get; set; }
        public int intPriorityUser { get; set; }

        /*--------------------------------------------------------*/

        public string dtmFFCIDateBefore { get; set; }
        public string strFFCIDocBeforeCode { get; set; }
        public string strFFCIDocBefore { get; set; }
        public decimal decLandAmtBefore { get; set; }
        public decimal decBuildingAmtBefore { get; set; }
        public decimal decPlantMachAmtBefore { get; set; }
        public decimal decOtheFixedAssetAmtBefore { get; set; }
        public decimal decTotalAmtBefore { get; set; }
        public string strProjectDocBeforeCode { get; set; }
        public string strProjectDocBefore { get; set; }
        public string dtmFFCIDateAfter { get; set; }
        public string strFFCIDocAfterCode { get; set; }
        public string strFFCIDocAfter { get; set; }
        public decimal decLandAmtAfter { get; set; }
        public decimal decBuildingAmtAfter { get; set; }
        public decimal decPlantMachAmtAfter { get; set; }
        public decimal decOtheFixedAssetAmtAfter { get; set; }
        public decimal decTotalAmtAfter { get; set; }
        public string strProjectDocAfter { get; set; }
        public string strProjectDocAfterCode { get; set; }

        public int intProductionId { get; set; }
        public int intDirectEmpBefore { get; set; }
        public int intContractualEmpBefore { get; set; }
        public string strEmpDocBeforeCode { get; set; }
        public string strEmpDocBefore { get; set; }
        public int intManagerialBefore { get; set; }
        public int intSupervisorBefore { get; set; }
        public int intSkilledBefore { get; set; }
        public int intSemiSkilledBefore { get; set; }
        public int intUnskilledBefore { get; set; }
        public int intTotalEmpBefore { get; set; }
        public int intGeneralBefore { get; set; }
        public int intSCBefore { get; set; }
        public int intSTBefore { get; set; }
        public int intTotalEmpCastBefore { get; set; }
        public int intWomenBefore { get; set; }
        public int intDisabledBefore { get; set; }
        public int intDirectEmpAfter { get; set; }
        public int intContractualEmpAfter { get; set; }
        public string strEmpDocAfterCode { get; set; }
        public string strEmpDocAfter { get; set; }
        public int intManagerialAfter { get; set; }
        public int intSupervisorAfter { get; set; }
        public int intSkilledAfter { get; set; }
        public int intSemiSkilledAfter { get; set; }
        public int intUnskilledAfter { get; set; }
        public int intTotalEmpAfter { get; set; }
        public int intGeneralAfter { get; set; }
        public int intSCAfter { get; set; }
        public int intSTAfter { get; set; }
        public int intTotalEmpCastAfter { get; set; }
        public int intWomenAfter { get; set; }
        public int intDisabledAfter { get; set; }

        public string intMeansFinanceId { get; set; }
        public decimal decEquity { get; set; }
        public decimal decLoanBankFI { get; set; }
        public string strTermLoanDocCode { get; set; }
        public string strTermLoanDoc { get; set; }
        public decimal decFDIComponet { get; set; }

        public int intCreatedBy { get; set; }

        public List<BasicProductionItemBefore> ProductionItem_BE { get; set; }
        public List<BasicProductionItemAfter> ProductionItem_AF { get; set; }
        public List<BasicTermLoan> TermLoan { get; set; }
        public List<BasicWorkingCapitalLoan> WorkingCapitalLoan { get; set; }
        public List<LandConverted> Land_tobe_converted { get; set; }
        public List<IncentiveAvailed> Incentive_Availeds { get; set; }

        public string strInctFlow { get; set; }
        /*----------------------------------------------------------------*/
        public string strUAMNo { get; set; } //Add By Debi on 19-06-2023
        public string dtmUAMdate { get; set; } //Add By Debi on 19-06-2023

        public string strIncentiveNumber { get; set; }//Add By Debi on 19-06-2023

        public int intEIMorUAMtype { get; set; }//Add By Debi on 19-06-2023
        public int intProjectClearance { get; set; }//Add By Debi on 19-06-2023
        public int intProvisnalPriorityThrustStatus { get; set; }//Add By Debi on 19-06-2023
        public int intIPRinctiveAvel { get; set; }//Add By Debi on 19-06-2023
        public string strClearnceswm { get; set; }//Add By Debi on 19-06-2023
        public decimal decElectricalInstAmtBefore { get; set; }//Add By Debi on 19-06-2023

        public decimal decElectricalInstAmtAfter { get; set; }
        public decimal decLoadUnloadAmtBefore { get; set; }

        public decimal decLoadUnloadAmtAfter { get; set; }
        public decimal decMarginMoneyForworkingAmtBefore { get; set; }

        public decimal decMarginMoneyForworkingAmtAfter { get; set; }

        public string strPhoneNo { get; set; }
        public string strEmail { get; set; }

        public string strPworofAttorneyPreDocCode { get; set; }
        public string strPworofAttorneyPre { get; set; }
        public string strCertificateofregistrationDocCodepre { get; set; }
        public string strCertificateofregistrationpre { get; set; }
        public string strApproveDPRDocCodePre { get; set; }
        public string strApproveDPRPre { get; set; }
        public string strEINapprovalDocCodePre { get; set; }
        public string strEINapprovalPre { get; set; }
        public string strBalacingEquipmentDocCodePre { get; set; }
        public string strBalacingEquipmentPre { get; set; }
        public string strCapitalInvstDocCodePre { get; set; }
        public string strCapitalInvstPre { get; set; }
        public string strInvestmentplantmachinaryDocCodePre { get; set; }
        public string strInvestmentplantmachinaryPre { get; set; }
        public string strProposedprodDocCodePre { get; set; }
        public string strProposedprodPre { get; set; }
        public string strPresentStageImplentDocCodePre { get; set; }
        public string strPresentStageImplentPre { get; set; }
        public string strMigrantIndustryunitDocCodePre { get; set; }
        public string strMigrantIndustryunitPre { get; set; }
        public string strfixedcapitalinvstDocCodePre { get; set; }
        public string strfixedcapitalinvstPre { get; set; }
        public string strPriorityorThrustsectorDocCodePre { get; set; }
        public string strPriorityorThrustsectorPre { get; set; }
        public string strPworofAttorneyPostDocCode { get; set;}
        public string strPworofAttorneyPost { get; set;}
        public string strPPorThrustStatusCertPostDocCode { get; set;}
        public string strPPorThrustStatusCertPost {get; set;}
        public string strCertificateofregistrationDocCodepost {get; set;}
        public string strCertificateofregistrationpost {get; set; }
        public string strApproveDPRDocCodePost { get;set;}
        public string strApproveDPRPost {get; set;}
        public string strPcDocCodePost {get; set;}
        public string strPcPost {get; set;}
        public string strSanctionbankorFIDocCodePost { get; set; }
        public string strSanctionbankorFIPost {get; set;}
        public string strCapitalInvstDocCodePost { get; set;}
        public string strCapitalInvstPost {get; set;}
        public string strInvestmentplantmachinaryDocCodePost { get; set;}
        public string strInvestmentplantmachinaryPost {get; set;}
        public string strBalacingEquipmentDocCodePost {get; set;}
        public string strBalacingEquipmentPost {get; set; }
        public string strServiceProvideDocCodePost{ get; set; }
        public string strServiceProvidePost {get; set; }
        public string strPriorityorThrustsectorDocCodePost {get; set;}
        public string strPriorityorThrustsectorPost { get; set;}
        public string strClearancefromPCBDocCodePost { get; set;}
        public string strClearancefromPCBPost {get; set;}
        public string strMigrantIndustryunitDocCodePost {get; set; }
        public string strMigrantIndustryunitPost { get; set;}
        public string strfixedcapitalinvstDocCodePost { get; set; }
        public string strfixedcapitalinvstPost { get; set;}
        public string strEmpoweredCommitteeDocCodePost { get; set;}
        public string strEmpoweredCommitteePost { get; set;}
        public string strEINapprovalDocCode { get; set; }
        public string strEINapproval { get; set; }
        public string strCapitalInvstDocCode { get; set; }
        public string strCapitalInvst { get; set; }
        public string strloansancorFIappliedDocCode { get; set; }
        public string strloansancorFIapplied { get; set; }
        public string strIncentiveAvailDocCode { get; set; }
        public string strIncentiveAvail { get; set; }
        public string strUndertakingeffectDocCode { get; set; }
        public string strUndertakingeffect { get; set; }
        public string strclearancefromPCBDocCode { get; set; }
        public string strclearancefromPCB { get; set; }
        //--------------------------------------------------------------------------//
        //Add by Debiprasanna on Dt:16-08-2023
        public string strProposedLocation { get; set; }
      public string strPrsentStatus { get; set; }
      public string strDeed { get; set; }
      public decimal decSdeClaimed { get; set; }
      public decimal decAmountAvailed { get; set; }
      public decimal decDeferentialClaim { get; set; }
      public string strEINorPEALapprovalDocCode { get; set; }
      public string strEINorPEALapproval { get; set; }
      public string strPworofAttorneyDocCode { get; set; }
      public string strPworofAttorney { get; set; }
      public string strCertificateofregistrationDocCode { get; set; }
      public string strCertificateofregistration { get; set; }
      public string strfixedcapitalinvstDocCode { get; set; }
      public string strfixedcapitalinvst { get; set; }
      public string strAppraisalThrustorPriorityDocCode { get; set; }
      public string strAppraisalThrustorPriority { get; set; }
      public string strCertficateofcommproductionDocCode { get; set; }
      public string strCertficateofcommproduction { get; set; }
      public string strCertficateofmigrationunitDocCode { get; set; }
      public string strCertficateofmigrationunit { get; set; }
      public string strPrivateindustDocCode { get; set; }
      public string strPrivateindust { get; set; }
      public string strDeedorAgreementDocCode { get; set; }
      public string strDeedorAgreement { get; set; }
      public string strSupportoftransferunitDocCode { get; set; }
      public string strSupportoftransferunit { get; set; }
      public string strProvisionsenunciatedDocCode { get; set; }
      public string strProvisionsenunciated { get; set; }
      public string strValidstatutoryclearancesDocCode { get; set; }
      public string strValidstatutoryclearances { get; set; }
      public string strStamppaperdulyDocCode { get; set; }
      public string strStamppaperduly { get; set; }
      public string strProvisionalPrioritycetificateDocCode { get; set; }
      public string strProvisionalPrioritycetificate { get; set; }
      public string strEmpoweredCommitteeDocCode { get; set; }
      public string strEmpoweredCommittee { get; set; }
 //----------------------------------------------------------------------------//
     //Add by Debiprasanna Jena on Dt:29-11-2023
     public string strNameoffinancer { get; set; }
     public decimal decCostofproject { get; set; }
     public decimal decAreaofLandRequired { get; set; }
     public decimal decAreaofLandAcquired { get; set; }
     public int intParticularsLandtobeconverted { get; set; }
     public string strThrustorPrioritySector { get; set; }
     public string strProvisionalPriorityorThruststatusDocCode { get; set; }
     public string strProvisionalPriorityorThruststatus { get; set; }
     public string strApprovalDetailsprojrctDocCode { get; set; }
     public string strApprovalDetailsprojrct { get; set; }
     public string strApprisalsupportexpansionDocCode { get; set; }
     public string strApprisalsupportexpansion { get; set; }
     public string strLanddocumentDocCode { get; set; }
     public string strLanddocument{ get; set; }
     public string strStatutoryClearancesDocCode { get; set; }
     public string strStatutoryClearances { get; set; }
     public string strNonjudicialStampDocCode { get; set; }
     public string strNonjudicialStamp { get; set; }
    }

    [Serializable]
    public class BasicProductionItemBefore
    {
        public string vchProductName { get; set; }
        public decimal? decQuantity { get; set; }
        public int intUnitType { get; set; }
        public string vchOtherUnit { get; set; }
        public decimal? decValue { get; set; }
        public string chItemFor { get; set; }
    }

    [Serializable]
    public class BasicProductionItemAfter
    {
        public string vchProductName { get; set; }
        public decimal? decQuantity { get; set; }
        public int intUnitType { get; set; }
        public string vchOtherUnit { get; set; }
        public decimal? decValue { get; set; }
        public string chItemFor { get; set; }
    }

    [Serializable]
    public class BasicTermLoan
    {
        public string vchNameOfFinancialInst { get; set; }
        public string vchState { get; set; }
        public string vchCity { get; set; }
        public decimal decLoanAmt { get; set; }
        public string dtmSanctionDate { get; set; }
        public decimal decAvailedAmt { get; set; }
        public string dtmAvailedDate { get; set; }
    }

    [Serializable]
    public class BasicWorkingCapitalLoan
    {
        public string vchNameOfFinancialInst { get; set; }
        public string vchState { get; set; }
        public string vchCity { get; set; }
        public decimal decLoanAmt { get; set; }
        public string dtmSanctionDate { get; set; }
        public decimal decAvailedAmt { get; set; }
        public string dtmAvailedDate { get; set; }
    }

    public class Inct_EC_Delay_Reason_Entity
    {
        public string strIndustryCode { get; set; }
        public string strEnterpriseName { get; set; }
        public int intUnitCat { get; set; }
        public int intUnitType { get; set; }
        public string strFFCIDate { get; set; }
        public string strProdCommDate { get; set; }
        public string strDelayReason { get; set; }
        public int intCreatedBy { get; set; }
        public string strAction { get; set; }
        public int intStatus { get; set; }
        public int INT_DELAY_ID { get; set; }
        public int intTimeAllowed { get; set; }
        public string vchRemark { get; set; }
        public string vchECLetter { get; set; }

        public List<InctECDelayDoc> ECDelayDoc { get; set; }
    }

    [Serializable]
    public class InctECDelayDoc
    {
        public string vchDocDesc { get; set; }
        public string vchFileName { get; set; }
    }

    #endregion

    #region MyRegion Ritika
    public enum enAppFor
    {
        New = 57,
        exist_Exp = 58,
        exist_div = 59,
        exist_mod = 60,
        Migrated_new = 61,
        Rehabilitated_New = 62,
        Transferred_new = 63,
        New_EMD = 1081,
        exist = 1119
    }

    public enum enAmdType
    {
        Name = 10,
        Location = 11,
        RegdOff = 12,
        Org = 13,
        EMD = 14
    }

    public enum enOwnerType
    {
        SC = 28,
        ST = 29,
        OBC = 30,
        GEN = 31,
        SC_WOMEN = 32,
        ST_WOMEN = 33,
        OBC_WOMEN = 34,
        GEN_WOMEN = 35
    }
    //added by ritika lath on 1st Jan 2018, for the feedback control
    public enum enFeedBackFormType
    {
        PCertificate = 501,
        Incentive = 500,
        Priority = 502
    }

    //added by ritika lath on 1st Jan 2018, for the type of document to get from Service
    public enum enServiceDocType
    {
        OSPCB = 1,
        Boiler = 2
    }

    public enum enModeOfInvestment
    {
        ein = 1120
    }

    public static class clsOwnerTypeDoc
    {
        private static string _strOwnerDocSc;
        public static string StrOwnerDocSc
        {
            get { return "D244"; }
        }

        private static string _strOwnerDocST;
        public static string StrOwnerDocST
        {
            get { return "D245"; }
        }
        private static string _strOwnerDocOBC;
        public static string StrOwnerDocOBC
        {
            get { return "D246"; }
        }
    }

    public class Incentive_PCMaster
    {
        public int intAppFor { get; set; }
        public string strChngIn { get; set; }
        public int intAppNo { get; set; }
        public string strEINEMIIPMTNo { get; set; }
        public string strUAN { get; set; }
        public string strCompName { get; set; }
        public int intUnitCat { get; set; }
        public int intUnitType { get; set; }
        public int intOrgType { get; set; }
        public string strOwnerName { get; set; }
        public int intOwnerCode { get; set; }

        public string strAddr { get; set; }
        public string strPhNo { get; set; }
        public string strFaxNo { get; set; }
        public string strEmail { get; set; }
        public string strWebsite { get; set; }
        public string strUnitLoc { get; set; }
        public string strOffcAddr { get; set; }
        public string strOffcPhNo { get; set; }
        public string strOffcFaxNo { get; set; }
        public string strOffcEmail { get; set; }
        public string strOffcWebsite { get; set; }

        public string dtmFFCI { get; set; }
        public string strInvestIn { get; set; }
        public string strInvestMode { get; set; }
        public decimal decWorkingCapital { get; set; }
        public decimal decEquity { get; set; }
        public decimal decLoan { get; set; }
        public decimal decFdiComp { get; set; }
        public string strProductCode { get; set; }
        public string strProductName { get; set; }
        public int intIsPwrReq { get; set; }
        public decimal decContractDemand { get; set; }
        public string dtmProdComm { get; set; }
        public string dtmEinIssuance { get; set; }

        public int intCreatedBy { get; set; }
        public string strPCNo { get; set; }
        public string dtmIssueDate { get; set; }
        public string dtmAmendedOn { get; set; }

        public int intApplyFlag { get; set; }

        public int intManaregailSkill { get; set; }
        public int intSupervisor { get; set; }
        public int intSkilled { get; set; }
        public int intSemiSkilled { get; set; }
        public int intUnskilled { get; set; }
        public int intScTotal { get; set; }
        public int intStTotal { get; set; }
        public int intWomen { get; set; }
        public int intDisabled { get; set; }
        public int intTotalEmployee { get; set; }
        public int intContractual { get; set; }
        public string strXml { get; set; }

        public string strActionCode { get; set; }

        public int intSectorId { get; set; }
        public int intSubSectorId { get; set; }
        public int intDistrict { get; set; }
        public int intBlock { get; set; }
        public decimal decLandInvestment { get; set; }
        public decimal decBuilding { get; set; }
        public decimal decPlant { get; set; }
        public decimal decOthers { get; set; }
        public string strFileorgTypeDocument { get; set; }
        public string strFileOwnerTypeDocument { get; set; }
        public string strFileFirstSaleBill { get; set; }
        public string strFileLand { get; set; }
        public string strFilePlant { get; set; }
        public string strFileSanction { get; set; }
        public string strFileEmployement { get; set; }
        public string strFileClearence { get; set; }
        public string strFileProject { get; set; }
        public string strFilePower { get; set; }
        public string strFileProducts { get; set; }
        public string strFileOwnerCategory { get; set; }
        public string strUnitOthersk { get; set; }
        public string strOfficeMobCode { get; set; }
        public string strOfficeFaxCode { get; set; }
        public string strEntMobCode { get; set; }
        public string strEntFaxCode { get; set; }
        public int intGeneral { get; set; }
        public string strXmlDocuments { get; set; }
        public string strIndustryCode { get; set; }
        public string strFileXML { get; set; }
        public string strDateConnection { get; set; }
        public string strOthersOrg { get; set; }
        public string strIEMFile { get; set; }
        public int intSalutation { get; set; }
        public int intChangeIn { get; set; }
        public string VATFile { get; set; }
        public string strBuildFile { get; set; }
        public string strAgreementFile { get; set; }
        public string strSalePost3Months { get; set; }
        public string strProductionFile { get; set; }
        public string strRMPostFile { get; set; }
        public string strRMPreFile { get; set; }
        public string strRMInoviceFile { get; set; }
        public string strFactoryLicFile { get; set; }
        public string strSaleInvoiceFile { get; set; }
        public string strInvestCommercialFile { get; set; }
        public string GSTIN { get; set; }
        public string dtmIRScheduleOn { get; set; }
        public string IRRemark { get; set; }
        public string strXmlMachinery { get; set; }
        public int intTechnical { get; set; }

        //parameter added by Ritika Lath to store whether the user is from PEAL(0) or PC(1)
        public int intInvType { get; set; }

        public Boolean BitProdModified { get; set; }
        public Boolean BitPlantModified { get; set; }

        //parameter added by Ritika Lath ON 12/06/2017
        public string strBoilerFile { get; set; }

        public int intApproved { get; set; }
        public int intGeneratePc { get; set; }
        public string strPdfName { get; set; }
        public int intOfflinePc { get; set; }

    }


    public class PcSearch
    {
        public string strActionCode { get; set; }
        public int intPageIndex { get; set; }
        public int intPageSize { get; set; }
        public int intAppFor { get; set; }
        public string strFromDate { get; set; }
        public string strToDate { get; set; }
        public int intAppNo { get; set; }
        public string vchAppNos { get; set; }
        public string intDistId { get; set; }
        public string strUnitName { get; set; }
        public int UserId { get; set; }
    }



    public class PcApplied
    {
        public int rowcnt { get; set; }
        public string requestType { get; set; }
        public int intAppNo { get; set; }
        public string strCompName { get; set; }
        public string strPhNo { get; set; }
        public string strUnitCategory { get; set; }
        public string strOrganizationType { get; set; }
        public int intApproved { get; set; }
        public string strApplied { get; set; }

        public string vchAppNo { get; set; }
    }


    public class IRDetails
    {
        public int intAppNo { get; set; }
        public string strInspectionReport { get; set; }
        public string ControlMeasures { get; set; }
        public string IndSafety { get; set; }
        public string PowerLoad { get; set; }
        public string CppDetails { get; set; }
        public string strRemarks { get; set; }
        public string strSuggestions { get; set; }
        public int inCreatedBy { get; set; }
        public string strXmlOfficer { get; set; }
        public string strXmlProducts { get; set; }
        public string strXmlCapitalInvestment { get; set; }
        public string strXmlTermPlan { get; set; }
        public string strXmlWorkingCapital { get; set; }
        public string strXmlApproval { get; set; }
        public string strXmlClearence { get; set; }
        public string strXmlProblems { get; set; }
        public string strXmlOther { get; set; }
        public string strXmlApplied { get; set; }
        public string strSignature { get; set; }
        public string strCommisioningDate { get; set; }
        public string strPlantInvestDate { get; set; }

        public string strXmlMachinery { get; set; }

        public string strProductCode { get; set; }

        public string strProductName { get; set; }

        public int intCheck { get; set; }

        public int intUnit { get; set; }
    }
    public class CertificateDetails
    {
        public int intAppNo { get; set; }
        public string strPlaceNew { get; set; }
        public string strDateNew { get; set; }
        public string strFileNew { get; set; }
        public string strActionCode { get; set; }
        public int intCreatedBy { get; set; }
        public string strPlaceAmd { get; set; }
        public string strDateAmd { get; set; }
        public string strDateChangeCat { get; set; }
        public string strFileAmd { get; set; }
        public string strXmlAmdRemarks { get; set; }
        public string strIRSignature { get; set; }
        public string strPdfName { get; set; }
        public string strProdEmd { get; set; }
        public string strPlantEmd { get; set; }
    }
    #endregion

    #region Main Incentive
    public class Incentive
    {
        public string strcActioncode { get; set; }
        public int UnqIncentiveId { get; set; }
        public string IncentiveNum { get; set; }
        public string PealNum { get; set; }
        public string PCNum { get; set; }
        public string UnitCode { get; set; }
        public string ProposalNum { get; set; }
        public int Userid { get; set; }
        public int Createdby { get; set; }
        public string ApprovalAction { get; set; }
        public bool IsDraft { get; set; }
        public FormNumber FormType { get; set; }
        public int incentivetype { get; set; }
        public string Signature { get; set; }
        public int FYear { get; set; }

        public string strEsignFileName { get; set; } //// Added by Sushant Jena On Dt.25-Oct-2018

        //#region Pioneer Unit
        //public string strAction { get; set; }
        //public int intUniqueId { get; set; }
        //public int intCertAvail { get; set; }
        //public int intSectorCertificate { get; set; }
        //public string strAcknowledgement { get; set; }
        //public int intOspcbNoc { get; set; }
        //public int intOspcbConsent { get; set; }
        //public int intOspcbExcise { get; set; }
        //public int intOspcbFSHGSCD { get; set; }
        //public int intOspcbExpolsive { get; set; }
        //public string strStatutoryClearances { get; set; }
        //#endregion

        public INDUSTRIAL_UNIT_MASTER IndsutUnitMstDet { get; set; }
        public IndustrialUnit MyProperty { get; set; }
        public Production ProdEmpDet { get; set; }
        public InvestmentDetails InvestmentDet { get; set; }
        public InterestSubsidyDetails IntrstSubsidyDet { get; set; }
        public PrioritySectorDetails PrioritySector { get; set; }
        public AdditionalDocuments AdditionalDocument { get; set; }
        public PatentDetails PatentDet { get; set; }
        public AvailDetails AvailDet { get; set; }
        public AvailedClaimDetails AvailClaimDet { get; set; }
        public BankDetails BankDet { get; set; }
        public StampDutyExemption StampDutyDet { get; set; }
        public Technicalknow TechnicalKnowDet { get; set; }
        public CourseDetails CourseDet { get; set; }
        public DocumentSubmittedAftComp DocSubAftCompDet { get; set; }
        public MeansOfFinance MeanOfFinanceDet { get; set; }
        public LandDetails LandDet { get; set; }
        public ConsumeLoadDet ConsumLoadDet { get; set; }
        public ContractLoadDet ContractLoadDet { get; set; }
        public EnergyAuditDetails EnergyAuditDet { get; set; }
        public QualityCertification QualityCertDet { get; set; }
        public PowerTariff PowerTariffDet { get; set; }
        public TrainingDetails TrainingDetail { get; set; }
        public MajorOperationOfCompany MajorOperationOfComp { get; set; }
        public BriefDtlPropActivity BriefDtlPropActvy { get; set; }
        public DLSWCAApprovalDtls DLSWCAApprovalDet { get; set; }
        public InvestmentPollution InvestPolutionDet { get; set; }
        public GetAndViewParam GetVwPrmtrs { get; set; }
        public List<lstFileUpload> FileUploadDetails { get; set; }//XML File Unit
        public AvailedIncentiveEarlier AvailedEarlier { get; set; }
        public TermLoanDetails TermLoanDetails { get; set; }
    }
    public class lstFileUpload
    {
        public int id { get; set; }
        public string vchDocId { get; set; }
        public string vchFileName { get; set; }
        public string vchFilePath { get; set; }
    }
    public enum FormNumber
    {
        InterestSubsidy_01,
        PrioritySector_02,
        PioneerUnits_03,
        PatentRegistration_04,
        SubsidyOnPlantAndMachinery_05,
        StampDutyExemption_06,
        TechnicalKnowHow_07,
        CapitalGrantToSupportQualityInfrastructure_08,
        EmploymentCostSubsidy_09,
        EntreprenuershipDevelopmentSubsidy_10,
        PremiumLeviableForConversionOfLandForIndustrialUse_11,
        ElectricityDuty_12,
        OneTimeReimbursementOfEnergyAuditCost_13,
        QualityCertification_14,
        PowerTarrif_15,
        TrainingSubsidy_16,
        AnchorTenant_17,
        CapitalInvestmentZld_18,
        GrantprioritySector_19

    };
    #endregion

    #region Ritika's Dashboard
    public class InctSearch
    {
        public int intPageIndex { get; set; }
        public int intPageSize { get; set; }
        public int intStatus { get; set; }
        public string strApplicationNo { get; set; }
        public string strUnitName { get; set; }
        public int intPriority { get; set; }
        public string strActionCode { get; set; }
        public int intYear { get; set; }
        public int intDistrict { get; set; }
        public int intUnitType { get; set; }
        public int intUserUnitType { get; set; }
        public int intPolicyType { get; set; }
    }

    public class InctApplicationDetails_Entity
    {
        public int intRowCount { get; set; }
        public string strApplicationNum { get; set; }
        public string strUnitName { get; set; }
        public string strInctName { get; set; }
        public string strAppliedOn { get; set; }
        public string strStatus { get; set; }
        public string strSectorName { get; set; }
        public string strPriority { get; set; }
        public string strUnitType { get; set; }
    }

    public class InctDashBoard_Entity
    {
        public int intMainId { get; set; }
        public int intParentId { get; set; }
        public string strName { get; set; }
        public int intCount { get; set; }
        public decimal decAmount { get; set; }
    }

    public enum enDashboardAppType
    {
        Under_Processing = 2,
        Rejected = 3,
        Sanctioned = 4,
        IPR = 5,
        Exemption = 6,
        Reiumbursement = 7,
        ORTPSA = 8,
        Disburse = 9,
        Pending = 10,
        Total = 1
    }
    #endregion

    #region MisReports
    public class InctWiseClaimReport
    {
        public string strPolicyName { get; set; }
        public string strIncentiveName { get; set; }
        public string strIncentiveType { get; set; }
        public string strUnitName { get; set; }
        public string strUnitType { get; set; }
        public int intDraftedCount { get; set; }
        public int intAppliedCount { get; set; }
        public int intPendingCount { get; set; }
        public int intRejectedCount { get; set; }
        public int intApprovedCount { get; set; }
        public int intDisbursedCount { get; set; }
        public decimal decClaimedAmt { get; set; }
        public decimal decPendingAmt { get; set; }
        public decimal decRejectedAmt { get; set; }
        public decimal decApprovedAmt { get; set; }
        public decimal decDisbursedAmt { get; set; }
        public int intIncentiveId { get; set; }
        public int intRowCount { get; set; }
    }

    public class InctApplicationStatusRpt_Entity
    {
        public string strUnitName { get; set; }
        public string strAppliedOn { get; set; }
        public decimal decClaimAmount { get; set; }
        public string strPendingAt { get; set; }
        public string strDisbursementDate { get; set; }
        public decimal decDisbursementAmount { get; set; }
        public string strStatus { get; set; }
        public int intRowCount { get; set; }
        public string strIncentiveName { get; set; }

    }
    #endregion

    [Serializable]
    public class LandConverted
    {
        public string vchMouza { get; set; }  
        public string vchKhataNo { get; set; }   
        public string vchPlotNo { get; set; }
        public string vchArea { get; set; }
        public string vchPresentKisam { get; set; }
    }

    [Serializable]
    public class IncentiveAvailed
    {
        public string vchIncentive { get; set; }
        public Decimal decValue { get; set; }
        public Decimal decPeriod { get; set; }
        public string vchIPRApplica { get; set; }

    }
}
