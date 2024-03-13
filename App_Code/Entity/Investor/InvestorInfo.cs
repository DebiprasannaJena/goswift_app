using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Investor
{
    public class InvestorInfo
    {
        public int INT_INVESTOR_ID { get; set; }
        public string VCH_INV_NAME { get; set; }
        public int INT_COUNTRY { get; set; }
        public string VCH_EMAIL { get; set; }
        public int INT_SALUTATION { get; set; }
        public string VCH_CONTACT_FIRSTNAME { get; set; }
        public string VCH_CONTACT_MIDDLENAME { get; set; }
        public string VCH_CONTACT_LASTNAME { get; set; }
        public string VCH_OFF_MOBILE { get; set; }
        public string VCH_ADDRESS { get; set; }
        public string VCH_INV_USERID { get; set; }
        public string VCH_INV_PASSWORD { get; set; }
        public int INT_SEC_QUES { get; set; }
        public string VCH_SEC_ANSWER { get; set; }
        public int INT_EMAIL_STATUS { get; set; }
        public int INT_SMS_STATUS { get; set; }
        public int INT_TERM_STATUS { get; set; }
        public string VCH_INV_PHOTO { get; set; }
        public int INT_CREATED_BY { get; set; }
        public object DTM_CREATED_ON { get; set; }
        public int INT_UPDATED_BY { get; set; }
        public object DTM_UPDATED_ON { get; set; }
        public int INT_INDUSTRY_GROUP_ID { get; set; }
        public string VCH_INDUSTRY_NAME { get; set; }
        public int INT_STATUS { get; set; }
        public string VCH_GSTIN { get; set; }
        public int INT_DISTRICT { get; set; }
        public int INT_BLOCK { get; set; }
        public int INT_SECTOR { get; set; }
        public int INT_SUBSECTOR { get; set; }
        public string VCH_SITELOCATION { get; set; }
        public decimal DEC_INVESTAMOUNT { get; set; }
        public int INT_CATEGORY { get; set; }
        public string VCH_UID { get; set; }
        public string VCH_INDUSTRY_CODE { get; set; }
        public string VCH_PANNO { get; set; }

        public string VCH_TOKENNO { get; set; }
        public string VCH_TOKENTIME { get; set; }
        public string INT_TOKENSTATUS { get; set; }
        public string INT_LOGINFAILED_COUNT { get; set; }
        public string VCH_LOGINEXPIRETIME { get; set; }
        public int INT_PARENTID { get; set; }
        public string VCH_EIN_IEM { get; set; }
        public int INT_APPROVALSTATUS { get; set; }
        public string VCH_LICENCE_NO_TYPE { get; set; }
        public string VCH_LICENCE_DOC { get; set; }

        public int intUserLevel { get; set; }
        public int INT_OTP_STATUS { get; set; }
        public string strAction { get; set; }

        public string strUniqueId { get; set; }
        public string strDesignation { get; set; }
        public string strAppIds { get; set; }
        public string StrVCH_PROP_NAME { get; set; }

        #region Add by Anil Sahoo
        public int INT_INDUSTRY_TYPE { get; set; }

        public string strCINnumber { get; set; }

        public int intEntitytype { get; set; }

        public string strPanHolderName { get; set; }

        public string strDOB { get; set; }

        #endregion




    }
}
