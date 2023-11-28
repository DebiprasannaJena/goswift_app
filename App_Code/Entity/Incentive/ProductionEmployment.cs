using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer.Incentive
{
    /// <summary>
    /// Summary description for ProductionEmployment
    /// </summary>
    public class ProductionEmployment
    {
        public ProductionEmployment()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int DirectEmpolyment { get; set; }
        public int ContractualEmployment { get; set; }
        public string SupportDocument { get; set; }
        public List<EmploymentInfo> Employment { get; set; }
    }
}