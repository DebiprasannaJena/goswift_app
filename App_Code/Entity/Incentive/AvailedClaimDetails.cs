using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AvailedClaimDetails
/// </summary>
/// 
namespace EntityLayer.Incentive
{
    public class AvailedClaimDetails
    {
        public int AVAIL_CLAIM_ID { get; set; }
        public int INCUNQUEID { get; set; }
        public decimal DIFF_AMOUNT_CLAIM { get; set; }
        public string SANCTION_FILE { get; set; }
        public List<AvailedClaimDetailsInfo> ListAvailedClaimDetailsInfo { get; set; }


    }
    public class AvailedClaimDetailsInfo
    {
        public int TRAN_ID { get; set; }
        public string INSTITUTION_NAME { get; set; }
        public string SANCTION_ORDER_NO { get; set; }
        public DateTime? SANCTION_DATE { get; set; }
        public decimal SANCTION_AMT { get; set; }
        public decimal AVAILED_AMOUNT { get; set; }
        public DateTime? AVAILED_DATE { get; set; }
    }

}

