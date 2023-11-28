using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
    public class ProjectInfo
    {
        public string strAction { get; set; }
        public string strProjLocation { get; set; }
        public string strOtherUnits { get; set; }
        public string vchIndustryInterprenur { get; set; }
        public string vchManufacturingProcessFlow { get; set; }
        public string vchFeasibilityReport { get; set; }
        public string vchBoardResolution { get; set; }
        public string vchSourceOfFinance { get; set; }
        public int intProjectId { get; set; }
        public int intUnitId { get; set; }
        public string vchSectorName { get; set; }
        public string vchSubSectorName { get; set; }
        public string vchStateName { get; set; }
        public string vchDistName { get; set; }
        public string vchCountryName { get; set; }

        public int intBlockId { get; set; }
        public string vchBlockName { get; set; }

        public int intTeheshilId { get; set; }
        public string vchTeheshilName { get; set; }

        public int intProposedId { get; set; }
        public string vchProposalNo { get; set; }
        public string vchNameOfUnit { get; set; }
        public string vchEINnIEMnIL { get; set; }
        public int intSectorId { get; set; }
        public int intSubSectorId { get; set; }
        public decimal decProposedAnnualCapacity { get; set; }
        public string vchUnit { get; set; }
        public decimal decLandIncLandDev { get; set; }
        public decimal decBuildingndConstruction { get; set; }
        public decimal decPlantndMachinery { get; set; }
        public decimal decOthers { get; set; }
        public decimal decTotCapitalInvestment { get; set; }
        public int intPeriodToCommenceProduction { get; set; }
        public string vchProjectComingUnder { get; set; }
        public string vchPollutionCategory { get; set; }
        public int intGroundBreaking { get; set; }
        public int intCivilndStructuralCompln { get; set; }
        public int intMajorEquipmentErect { get; set; }
        public int intStartOfCommercialProd { get; set; }
        public decimal decEquityContribution { get; set; }
        public decimal decBankndInstitutionalFin { get; set; }
        public decimal decTotFinance { get; set; }
        public decimal decForeignInvestment { get; set; }
        public string vchIRR { get; set; }
        public string vchDSCR { get; set; }
        public int intMangerExist { get; set; }
        public int intManagerProp { get; set; }
        public int intSupervisorExist { get; set; }
        public int intSupervisorProp { get; set; }
        public int intSkilledExist { get; set; }
        public int intSkilledProp { get; set; }
        public int intSemiSkilledExist { get; set; }
        public int intSemiSkilledProp { get; set; }
        public int intUnSkilledExist { get; set; }
        public int intUnSkilledProp { get; set; }
        public int intTotalExist { get; set; }
        public int intTotalProp { get; set; }
        public int intPropDirectEmployment { get; set; }
        public int intPropContractualEmployment { get; set; }
        public int intCreatedBy { get; set; }
        public string vchUnitName { get; set; }
        public string vchProduct { get; set; }
        public string vchTotCapacity { get; set; }
        public int intStateId { get; set; }
        public int intDistId { get; set; }
        public int intCountryId { get; set; }
        public string vchCityName { get; set; }
        public int intQid { get; set; }
        public string vchQf { get; set; }
        public int intTid { get; set; }
        public string vchTQ { get; set; }
        public string vchOtherUnit { get; set; }
        public string CapacityUnit { get; set; }
        public int intEinNo { get; set; }
        public int intEinNoderr { get; set; }
        public int intCnt { get; set; }
        public string vchCNTISDNo { get; set; }
        public string vchCompanyName { get; set; }

        public int discomeid { get; set; }
        public string discomename { get; set; }

        public int intserviceid { get; set; }
        public string vchserviceName { get; set; }
        public string vchAccountHead { get; set; }
        public decimal decPaymentAmt { get; set; }
        public string intSAid { get; set; }
        public string vchDescription { get; set; }
        public string vchApplicantNo { get; set; }
        public string vchOrderNo { get; set; }
        public string vchServiceType { get; set; }
        public int intTypeid { get; set; }

        public string Ramount { get; set; }
        public string RDesc { get; set; }
        public string RApplicationNo { get; set; }
        public string RAccountHead { get; set; }
        public string RUserName { get; set; }

        public string vchChallanAmt { get; set; }
        public string vchbankTransactionId { get; set; }
        public string vchbankchallanRefId { get; set; }
        public string vchbankTransactionStatus { get; set; }
        public string vchbankTransTimeStamp { get; set; }
        public string vchReqID { get; set; }
        public string ReqID { get; set; }
        public string OrderNo { get; set; }
        public int intStatusId { get; set; }
        public string vchStatusName { get; set; }
        public int intPriority { get; set; }
        public string strProductDetails { get; set; }

        //Added by suroj kumar pradhan
        public string Reqid { get; set; }
        public int intUnitid { get; set; }
        public string vchProposedUnit { get; set; }
        public string vchProductName { get; set; }
        public string vchProposedAnnualCapacity { get; set; }
        public int intProductid { get; set; }

        public int intConstitution { get; set; } //// Added by Sushant Jena On Dt:-27-Aug-2019
        public string strXmlGcNetWorth { get; set; } //// Added by Sushant Jena on Dt:05-Mar-2021 (Previously on PromoterDet.cs)   
    }
}
