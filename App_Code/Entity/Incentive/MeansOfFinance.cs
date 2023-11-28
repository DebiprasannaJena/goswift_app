using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MeansOfFinance
/// </summary>
/// /// CREATED BY PRAKASH SWAIN ON 9-Sept-2017
namespace EntityLayer.Incentive
{
    public class MeansOfFinance
    {
        public int? MOFID { get; set; }
        public string Employment { get; set; }
        public int? CreatedBy { get; set; }
        public string TermLoanSaction { get; set; }
        public string InternalSource { get; set; }
        public string SactionOrderOfLoanDoc { get; set; }
        public string ApprovedDocdocument { get; set; }
        public double? AmtClaimPerWorker { get; set; }
        public string AssesesmentReport { get; set; }
        public List<MeansOfFinanceLoanDetails> LoanList { get; set; }
        public List<MeansOfFinanceWorkigLoanDetails> WorkingLoanList { get; set; }
        public List<RepaymentDetails> RepaymentList { get; set; }
    }
    public class MeansOfFinanceLoanDetails
    {
        public string NameOfFinacialInst { get; set; }
        public string LocState { get; set; }
        public string LocCity { get; set; }
        public decimal? LoanAmt { get; set; }
        public string SactionDate { get; set; }
        public decimal? AvailedAmt { get; set; }
        public string AvailedDate { get; set; }
    }
    public class MeansOfFinanceWorkigLoanDetails
    {
        public string NameOfFinacialInst { get; set; }
        public string LocState { get; set; }
        public string LocCity { get; set; }
        public decimal? LoanAmt { get; set; }
        public string SactionDate { get; set; }
        public decimal? AvailedAmt { get; set; }
        public string AvailedDate { get; set; }
    }
    public class RepaymentDetails
    {
        public string FinancialYear { get; set; }
        public decimal? PrincipalAmt { get; set; }
        public decimal? InvestmentAmt { get; set; }
        public string PaymentDate { get; set; }
    }
}