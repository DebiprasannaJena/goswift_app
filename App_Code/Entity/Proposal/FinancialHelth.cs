using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
   public class FinancialHelth
    {
       public string Action                 {get;set;}
       public int intFinalcialId            { get; set; }
       public string vchProposalNo          { get; set; }
       public decimal decAnnlCrntYr         { get; set; }
       public decimal decAnnlLastYr         { get; set; }
       public decimal decAnnlPrevToLastYr   { get; set; }
       public decimal decPftBTCrntYr        { get; set; }
       public decimal decPftBTLastYr { get; set; }
       public decimal decPftBTPrevToLastYr { get; set; }
       public decimal decNWCrntYr { get; set; }
       public decimal decNWLastYr { get; set; }
       public decimal decNWPrevTolastyr { get; set; }
       public decimal decRSCrntyr { get; set; }
       public decimal decRSLastYr { get; set; }
       public decimal decRSPrevTolastYr { get; set; }
       public decimal decSCCrntYr { get; set; }
       public decimal decSCLastYr { get; set; }
       public decimal decSCPrevToLastYr { get; set; }
       public string vchBankName { get; set; }
       public string vchBranch { get; set; }
       public string vchbankAcNo { get; set; }
       public string vchIfscCode { get; set; }
       public int intCreatedBy { get; set; }
      
    }
}
