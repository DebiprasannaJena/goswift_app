using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace EntityLayer.Incentive
{
    /// <summary>
    /// Summary description for InterestSubsidyDetails
    /// Created By :Dinanath Chand on 09-Sept-2017
    /// </summary>
    public class TermLoanDetails
    {
        public int? INT_IS_ID { get; set; }
        public int? IntIncUnqueId { get; set; }
        public string vchFinancialInstitution { get; set; }
        public int? intYear { get; set; }
        public DateTime? dtmLoanStartDate { get; set; }
        public DateTime? dtmLoanMaturitydate { get; set; }
        public decimal decSanctionAmount { get; set; }
        public string vchSanctionOrderDoc { get; set; }
        public decimal decReinursementAmount1 { get; set; }
        public decimal decReinursementAmount2 { get; set; }
        public decimal decReinursementAmount3 { get; set; }
        public decimal decReinursementAmount4 { get; set; }

        public List<PlannedDisbursal> lstPlannedDisbursal { get; set; }
        public List<RepaymentSchedule> lstRepaymentSchedule { get; set; }


        public string VCHTermLoanDOC { get; set; }
        public List<PreviousSanction> listPreviousSanction { get; set; }
        public string VCHBankDetailDOC { get; set; }
    }
    [Serializable]
    public class PlannedDisbursal
    {
        public int? INTPlannedDisbursal { get; set; }
        public DateTime? DTMDisbursalDate { get; set; }
        public decimal Amount { get; set; }
        public string Year { get; set; }
    }
    [Serializable]
    public class RepaymentSchedule
    {
        public decimal int_Repayment_id { get; set; }
        public decimal decPlannedPrincipalAmount { get; set; }
        public decimal decPlannedInterestAmount { get; set; }
        public DateTime? dtmPlannedRepaymentDate { get; set; }
        public decimal decActualPrincipalAmount { get; set; }
        public decimal decActualInterestAmount { get; set; }
        public DateTime? dtmActualRepaymentDate { get; set; }
    }
    [Serializable]
    public class PreviousSanction
    {
        public decimal DECSactionAmount { get; set; }
        public DateTime? DTMSactionData { get; set; }
        public string VCHSactionOrder { get; set; }
        public string VCHSanctionOrderdOC { get; set; }
    }

}

