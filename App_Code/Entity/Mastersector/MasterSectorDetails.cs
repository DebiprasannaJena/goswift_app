using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Mastersector
{
    public class MasterSectorDetails
    {
        public string strAction { get; set; }
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
    public class MasterDdl
    {
        public string strAction { get; set; }
        public int intPolicyId { get; set; }
        public string strpolicyname { get; set; }
    }
    public class MasterGrid
    {
        public string strAction { get; set; }
        public int SectorId { get; set; }
        public int SectorCode { get; set; }
    }
    public class Gridviewgrd
    {
        public string strAction { get; set; }
        public int SectorId { get; set; }
        public int SectorCode { get; set; }
        public string strSectorName { get; set; }
        public string strSectorDescription { get; set; }
        public int SectorPriority { get; set; }
        public int intPolicyReference { get; set; }
    }
}
