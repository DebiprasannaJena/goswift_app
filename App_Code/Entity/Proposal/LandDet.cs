using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
    public class LandDet
    {
        public string strAction { get; set; }
        public int intLandId { get; set; }
        public string vchProposalNo { get; set; }
        public Boolean bitLandRequired { get; set; }
        public int intDistrictId { get; set; }
        public int intRecomendDistrict { get; set; }
        public string nvchDistrictName { get; set; }
        public int intBlockId { get; set; }
        public int intRecomendBlock { get; set; }
        public string vchBlockName { get; set; }
        public decimal decExtendLand { get; set; }
        public int sintLandRequiredIDCO { get; set; }
        public string vchIDCOInustrialName { get; set; }
        public int sintLandAcquiredIDCO { get; set; }
        public Boolean bitGridSource { get; set; }
        public Boolean bitCppSource { get; set; }
        public decimal decPowerDemandGrid { get; set; }
        public decimal decPowerDrawalCPP { get; set; }
        public decimal decCapacityofCPPPlant { get; set; }
        public decimal decWaterRequireExist { get; set; }
        public decimal decWaterReqireProposed { get; set; }
        public decimal decWaterRequirProduct { get; set; }
        public string vchSurfaceWater { get; set; }
        public string vchIdcoSupply { get; set; }
        public string vchRainWtrHarvesting { get; set; }
        public string vchother { get; set; }
        public string vchOtherSpecify { get; set; }
        public Boolean bitAdoptionWater { get; set; }
        public string vchQuntRecyllingWaste { get; set; }
        public int intCreatedBy { get; set; }
        public int intUpdatedBy { get; set; }
        public DateTime dtmCreatedOn { get; set; }
        public DateTime dtmUpdatedOn { get; set; }
        public Boolean bitDeletedFlag { get; set; }
        public string vchWasteConserFile { get; set; }
        public int intIdcoIndustrial { get; set; }
        public string vchWaterHazardousFile { get; set; }
        public int intIndustrialEstateId { get; set; }
        public string vchIndustrialName { get; set; }
        public string vchIDCOInustrial { get; set; }
        public string MobileNo { get; set; }
        public string SMSContent { get; set; }
        public string OrderNo { get; set; }
        public string ApplicationNo { get; set; }
        public string LandAprvByIPICOL { get; set; }
        public string Email { get; set; }
        public string strProjectLayOut { get; set; }
        public string strProjectLandStmt { get; set; }

        //  public string strLandUnit { get; set; } //// Added by Sushant Jena On Dt:-18-Feb-2020

        public Boolean BitIppSource { get; set; } //// Added by Sushant Jena On Dt:-23-Aug-2021
        public decimal DecPowerProducerIpp { get; set; } //// Added by Sushant Jena On Dt:-23-Aug-2021

        public string StrLandRecomFile { get; set; } // Add by Anil Sahoo

        public decimal DecRecomendLand { get; set; }  // Add by Anil sahoo

    }
}
