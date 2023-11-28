using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmploymentInfo
/// </summary>
/// 
namespace EntityLayer.Incentive
{
    public class EmploymentInfo
    {
        public int EmploymentTypeId { get; set; }
        public string EmploymentType { get; set; }
        public int Current { get; set; }
        public int Proposed { get; set; }
    }
}