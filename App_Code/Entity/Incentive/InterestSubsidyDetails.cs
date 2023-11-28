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
    public class InterestSubsidyDetails
    {
        public int INT_IS_ID { get; set; }
        public decimal decInterestSubsidy { get; set; }
        public int intPeriodClaim { get; set; }
        public decimal decDifferAmtClaim { get; set; }
        public string DocDifferAmtClaim { get; set; }
        public decimal decReImbursement { get; set; }
        public string SuppotImplemenetDoc { get; set; }
        public List<IntrestOnTermLoan> lstInterestLoan { get; set; }
        public List<SanctionSubsidy> lstSanctionSubsidy { get; set; }
        public List<StatutoryClearences> lstStatutoryClearences { get; set; }
    }
     [Serializable]
    public class IntrestOnTermLoan
    {
        public string strFYrTLoan { get; set; }
        public decimal decInterestAmt { get; set; }
        public string strPaymentDate { get; set; }
    }
     [Serializable]
    public class SanctionSubsidy
    {
        public string strFYrSanction { get; set; }
        public string strFinInstitute { get; set; }
        public string strSanOrderNo { get; set; }
        public string strSanDate { get; set; }
        public decimal decSancAmt { get; set; }
    }
     [Serializable]
    public class StatutoryClearences
    {
        public string strClearanceName { get; set; }
        public string ClearanceNameDoc { get; set; }
    }
}

