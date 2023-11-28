using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer.Incentive
{
    /// <summary>
    /// Summary description for Techncalknow
    /// </summary>
    /// 

    public class Technicalknow
    {
        public int INT_Section_No { get; set; }
        public int INT_TECHNICAL_CLAIM { get; set; }
        public string STR_BRIEF_ON_TECHNICAL { get; set; }
        public int INTINCUNQUEID { get; set; }
        public int INTINDUSTRAILUNIT { get; set; }
        public int BITDELETEDFLAG { get; set; }
        public List<Technicalknowdetail> Technicalknowdetails { get; set; }

    }
    [Serializable]
    public class Technicalknowdetail
    {
        public int INT_TECHNICAL_CLAIM_DETAIL { get; set; }
        public string STR_IMPORTED { get; set; }
        public string STR_NAME_OF_THE_AGENCY { get; set; }
        public string STR_ADDRESS_OF_THE_AGENCY { get; set; }
        public string STR_PROFILE_UPLOAD_DOCUMENT { get; set; }
        public Decimal DEC_AMOUNT_0F_EXPENDITURE { get; set; }
        public string STR_BILL_NO { get; set; }
        public string STR_BILL_DOCUMENT { get; set; }
        public string DTM_BILL_DATE { get; set; }
        public Decimal DEC_TOTAL_BILL_AMOUNT { get; set; }
        public string STR_GOI_PERMISSION { get; set; }


    }
}