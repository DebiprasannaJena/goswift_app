using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
    public class PromoterDet
    {
        public string strAction { get; set; }
        public string vchProposalNo { get; set; }
        public string vchCompName { get; set; }
        public string vchAddress { get; set; }
        public int intCountry { get; set; }
        public int intState { get; set; }
        public string vchCity { get; set; }
        public int intPin { get; set; }
        public string vchPhoneNo { get; set; }
        public string vchFaxNo { get; set; }
        public string vchEmail { get; set; }
        public int bitAddresSameAsCorp { get; set; }
        public string vchContactPerson { get; set; }
        public string vchCorAdd { get; set; }
        public int intCorCountry { get; set; }
        public int intCorState { get; set; }
        public string vchCorCity { get; set; }
        public int intCorPin { get; set; }
        public string vchCorMobileNo { get; set; }
        public string vchCorFaxNo { get; set; }
        public string vchCorEmail { get; set; }
        public int intConstitution { get; set; }
        public string vchOtheConstituition { get; set; }
        public string intYearOfIncorporation { get; set; }
        public string vchPlaceIncor { get; set; }
        public string vchGSTIN { get; set; }
        public int intProjectType { get; set; }
        public int intApplicationFor { get; set; }
        public int intNumberOfPartner { get; set; }
        public string vchManagPartner { get; set; }
        public string decAnnulTurnOvr1 { get; set; }
        public string decAnnulTurnOvr2 { get; set; }
        public string decAnnulTurnOvr3 { get; set; }
        public string decProfitAftrTx1 { get; set; }
        public string decProfitAftrTx2 { get; set; }
        public string decProfitAftrTx3 { get; set; }
        public string decNetWorth1 { get; set; }
        public string decNetWorth2 { get; set; }
        public string decNetWorth3 { get; set; }
        public string decResvSurp1 { get; set; }
        public string decResvSurp2 { get; set; }
        public string decResvSurp3 { get; set; }
        public string decShareCap1 { get; set; }
        public string decShareCap2 { get; set; }
        public string decShareCap3 { get; set; }
        public int intEduQualif { get; set; }
        public int intTecQualif { get; set; }
        public int intExpInYr { get; set; }
        public string vchExisIndName { get; set; }
        public int intExisDistrict { get; set; }
        public int intExisBlock { get; set; }
        public int intAllotedBy { get; set; }
        public string vchlandInAcres { get; set; }
        public string vchNatureAct { get; set; }
        public int intSectorId { get; set; }
        public int intSubSectorId { get; set; }
        public string vchCapacity { get; set; }
        public int intCapacityUnit { get; set; }
        public string vchOther { get; set; }
        public int intCreatedBy { get; set; }
        public int intUpdatedBy { get; set; }
        public int intPromoterId { get; set; }
        public string strXML_Data { get; set; }
        public string strXML_BD_Data { get; set; }
        public string strXML_RWM_Data { get; set; }
        public int intFyn1 { get; set; }
        public int intFyn2 { get; set; }
        public int intFyn3 { get; set; }
        public string vchPanfile { get; set; }
        public string vchGSTNfile { get; set; }
        public string vchMemorandumfile { get; set; }
        public string vchCertificateincorpfile { get; set; }
        public string vchEduQualifile { get; set; }
        public string vchTechniQualifile { get; set; }
        public string vchExpFile { get; set; }
        public string vchAuditFile { get; set; }
        public string vchNetWorthfile { get; set; }
        public string vchNameOfPromoter { get; set; }
        public int intProId { get; set; }
        public string vchName { get; set; }
        public string vchDesignation { get; set; }
        public int intProId1 { get; set; }
        public string vchRawMaterial { get; set; }
        public string vchRawMeterialSrc { get; set; }
        public int intProId2 { get; set; }
        public int intCordist { get; set; }
        //added for PEAL Report
        public string vchCountryName { get; set; }
        public string vchStateName { get; set; }
        public string vchCorCountryName { get; set; }
        public string vchCorStateName { get; set; }
        public string vchSector { get; set; }
        public string vchSubSector { get; set; }
        public string vchDistrictName { get; set; }
        public string vchConstitution { get; set; }
        public string vchCapacityUnit { get; set; }
        public string vchBlockName { get; set; }
        public string vchOtherState { get; set; }
        public string vchOtherStateCor { get; set; }
        public string vchAuditFileSecondYrs { get; set; }
        public string vchAuditFileThrdYrs { get; set; }
        public int intISDPHNo { get; set; }
        public int intISDFXNo { get; set; }
        public int intISDMOBo { get; set; }
        public int intFaxCordet { get; set; }
        public string VCHISDPHNo { get; set; }
        public string VCHISDFXNo { get; set; }
        public string VCHISDMOBo { get; set; }
        public string VCHISDFAXCor { get; set; }
        public string vch_oas_cafno { get; set; }
        public string vch_unique_application_id_from_swp { get; set; }
        public string vch_industry_code { get; set; }
        public string vch_success_message { get; set; }
        public string vch_Error_Msg { get; set; }
        public string vch_validation_Msg { get; set; }
        public int PhoneStateCode { get; set; }
        public string vch_Input_String { get; set; }
        public int Tagtodet { get; set; }
        public int intFowardAMS { get; set; }
        public int intForwardIDCO { get; set; }
        public string strNodalOfcrName { get; set; }
        public int intNodalOfcrID { get; set; }
        public string strFwrDAMSDate { get; set; }
        public string bankname { get; set; }
        public string ifsccode { get; set; }
        public string dealername { get; set; }
        public string ordernumber { get; set; }
        public string bankcode { get; set; }
        public string treasuryrefno { get; set; }
        public string banktranstimestamp { get; set; }
        public string banktransstatus { get; set; }
        public string Amount { get; set; }
        public string dtmFromDate { get; set; }
        public string dtmToDate { get; set; }
        public string vchApplication { get; set; }
        public string dtmForwardDate { get; set; }
        public string strIncomeTaxReturn { get; set; }

        public string strUpdatedOn { get; set; }
        public int strCreatedBy { get; set; }

        public string strLandRecommendedtoidco { get; set; } //// Added by Manoj Behera on Dt:16-Apr-2019
        public string strLandRecommendedbyidco { get; set; } //// Added by Manoj Behera on Dt:16-Apr-2019
        public string strApprovalorderlink { get; set; } //// Added by Manoj Behera on Dt:16-Apr-2019

        public DateTime dtmApplicationDate { get; set; }//// Added by Sushant Jena on Dt:19-Aug-2019
        public string strXmlGcNetWorth { get; set; } //// Added by Sushant Jena on Dt:27-Aug-2019
        public int intIdcoReturnStatus { get; set; } //// Added by Sushant Jena on Dt:27-Jul-2020   
        public string strInvestorSWSId { get; set; } //// Added by Sushant Jena On Dt.19-Apr-2021 

        public string strPayStatus { get; set; }
    }
}
