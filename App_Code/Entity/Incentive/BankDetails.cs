using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BankDetails
/// </summary>
/// 
namespace EntityLayer.Incentive
{
    public class BankDetails
    {
        public int? SectionNo { get; set; }
        public string AccountNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string IFSCNo { get; set; }
        public string MICRNo { get; set; }
        public int? BankId { get; set; }
        public int? INCUNQUEID { get; set; }
        public string BankDoc { get; set; }
    }
}