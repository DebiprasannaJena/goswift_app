using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer.Master
{
    public class MasterDetails
    {
        public string strAction { get; set;}
        public int SectorId { get; set; }
        public int SectorCode { get; set; }
        public string strSectorName { get; set; }
        public string strSectorDescription { get; set; }
        public int SectorPriority { get; set; }
        public int intPolicyReference { get; set; }
        public DateTime Policyreference { get; set; }
        public int IntCreatedBy { get; set; }
        public int IntUpdatedBy { get; set; }
        public int IntDeletedStatus { get; set; }
    }
}