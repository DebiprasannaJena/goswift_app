using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer.Incentive
{
    public class ContractLoadDet
    {
        public int intconctdemamdid { get; set; }
        public string strcdemandfile {get; set;}
        public List<ContractLoanDet> ContractLoanDet { get; set; }
    }
    public class ContractLoanDet
    {
        public string strcntdemand { get; set; }
        public string strconsumerno { get; set; }
    }
}