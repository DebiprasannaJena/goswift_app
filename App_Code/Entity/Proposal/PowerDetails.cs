using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
    public class PowerDetails
    {
        public string strAction { get; set; }
        public int intPowerId { get; set; }
        public string vchProposalNo { get; set; }
        public decimal intDGCapacity { get; set; }
        public int intPowerRequired { get; set; }
        public decimal intLoadDemand { get; set; }
        public DateTime dtmDemandStartDate { get; set; }
        public int intGridConnection { get; set; }
        public int intIPPConnection { get; set; }
        public int intCPPConnection { get; set; }
        public int intProcessType { get; set; }
        public decimal decLoadGrid { get; set; }
        public decimal decLoadCPP { get; set; }
        public decimal decLoadIPP { get; set; }
        public decimal decCPPCapacity { get; set; }
        public string vchIPPSourceName { get; set; }
        public string vchRenewableEnergy { get; set; }
        public string vchLoadDemand { get; set; }
        public int intCreatedBy { get; set; }
        public int intStatus { get; set; }	
    }
}
