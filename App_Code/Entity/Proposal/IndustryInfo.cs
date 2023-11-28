using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
    public class IndustryInfo
    {
        #region "Industry Info"
        public string strAction { get; set; }
        public int intCreatedBy { get; set; }
        public int intIndustryId { get; set; }
        public string vchProposalNo { get; set; }
        public DateTime dtmStartofProjectConst { get; set; }
        public DateTime dtmTimescheduleCommisnIndustry { get; set; }
        public string vchStartofProduction { get; set; }
        public decimal decLandincludinglanddevelopment { get; set; }
        public decimal decPlantMachinery { get; set; }
        public decimal decBuildingCivilConstruction { get; set; }
        public decimal decOthers { get; set; }
        public int intTypeofIndustry { get; set; }
        public decimal decCapitalInvestment { get; set; }
        public decimal decBankInstitutionalFinance { get; set; }
        public decimal decEquityContribution { get; set; }
        public decimal decFDI { get; set; }
        public decimal decSubsidyorGrant { get; set; }
        public string vchOthersMeansofFinance { get; set; }
        public decimal decTotal { get; set; }
        public string vchRegdNoLicense { get; set; }
        public int intType { get; set; }
        public decimal decCapacityPresent { get; set; }
        public decimal decExistingBusinessInterest { get; set; }
        public decimal decFutureExpansionPlan { get; set; }
        public string vchProductName { get; set; }
        public string vchSectorofIndustries { get; set; }
        public string vchSubSectorofIndustries { get; set; }
        public string strIRR { get; set; }


        public int intIRRandDSCRId { get; set; }
        public string vchYear { get; set; }
        public decimal decProfitAfterTax { get; set; }
        public decimal decDepreciation { get; set; }
        public decimal decInterestOnTermLoan { get; set; }
        public decimal decTermloanEMI { get; set; }
        #endregion
    }
}
