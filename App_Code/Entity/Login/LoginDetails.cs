using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Login
{
    public class LoginDetails
    {
        public string strAction { get; set; }
        public string strUserID { get; set; }
        public string strPassword { get; set; }
        public string strOldPassword { get; set; }
        public string strNewPassword { get; set; }
        public string strEmail { get; set; }
        public int intInvestorId { get; set; }
        public string strUserName { get; set; }
        public string strRegDate { get; set; }
        public string strGSTIN { get; set; }
        public string strMobile { get; set; }
        public string strUID { get; set; }
        public string strInvName { get; set; }
        public string strTokenno { get; set; }
        public string strFullname { get; set; }
        public string strlogtime { get; set; }
        public string strPAN { get; set; } ///// Added by Sushant Jena On Dt.01-Jun-2018
        public string strEINIEM { get; set; }
        public string strUserIdAlias { get; set; } ///// Added by Sushant Jena On Dt.14-Sep-2018
        public string strUniqueId { get; set; } ///// Added by Sushant Jena On Dt.18-Sep-2018
        public string strLicenseNoType { get; set; } ///// Added by Sushant Jena On Dt.24-Sep-2018
        public string strLicenseDoc { get; set; } ///// Added by Sushant Jena On Dt.24-Sep-2018
        public int intAliasNameCount { get; set; } ///// Added by Sushant Jena On Dt.20-Nov-2018
        public int IntIndustryType { get; set; } ///// Added by Sushant Jena On Dt.01-Sep-2021
        public string strPanHolderName { get; set; } // add by anil
        public string strDOB { get; set; }// add by anil 
    }
}
