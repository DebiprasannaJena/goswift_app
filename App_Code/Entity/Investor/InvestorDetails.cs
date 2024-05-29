using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Investor
{
    public class InvestorDetails
    {
        public string strAction { get; set; }
        public int IntIndustryID { get; set; }
        public string strIndName { get; set; }
        public string strEmail { get; set; }
        public int Country { get; set; }
        public int Salutation { get; set; }
        public string strContactFirstName { get; set; }
        public string strContactMiddleName { get; set; }
        public string strContactLastName { get; set; }
        public string MobNo { get; set; }
        public string strAddress { get; set; }
        public string strUserID { get; set; }
        public string strPassword { get; set; }
        public string strRPassword { get; set; }
        public string IntSecQues { get; set; }
        public string strSecAnswer { get; set; }
        public int EmailStatus { get; set; }
        public int SmsStatus { get; set; }
        public int TermStatus { get; set; }
        public string Photo { get; set; }
        public int IntCreatedBy { get; set; }
        public int IntUpdatedBy { get; set; }
        public int IntDeletedStatus { get; set; }
        public string OTPTIME { get; set; }
        public string strIndGroupName { get; set; }
        public int IntIndGroupID { get; set; }
        public int IntIndStatus { get; set; }
        public int IntInvestorId { get; set; }
        public string StrRemarks { get; set; }
        public int intOTPStatus { get; set; }
        public int intDistrictId { get; set; }
        public int intBlockId { get; set; }
        public int intSectorId { get; set; }
        public int intSubSectorId { get; set; }
        public int intCategoryId { get; set; }
        public decimal decInvstAmount { get; set; }
        public string strSiteAddress { get; set; }
        public string strPanno { get; set; }
        public int intInvId { get; set; }
        public string MobileNo { get; set; }
        public string SMSContent { get; set; }
        public string strfullname { get; set; }

        #region Added by Sushant Jena
        //// Added on Dt.09.08.2018

        public int IntParentId { get; set; }
        public string strReadUserPermission { get; set; }
        public string strWriteUserPermission { get; set; }

        public string strRejectionCause { get; set; }

        public string strUniqueId { get; set; }

        #endregion

        public string strLicenceDoc { get; set; }

        public string strCINnumber { get; set; }

        public int intEntitytype { get; set; }
        public string strApprovalRemarks { get; set; }
        public string StrRegAddress_2 { get; set; }
        public int IntRegCountry { get; set; }
        public string StrRegState { get; set; }
        public string StrRegCity { get; set; }
        public string StrRegPincode { get; set; }
        public string StrSlAddress_2 { get; set; }
        public int IntSlCountry { get; set; }
        public string StrSlState { get; set; }
        public string StrSlCity { get; set; }
        public string StrSlPincode { get; set; }
        public string StrLLPINumber { get; set; }

        public string StrCinLlpinData  { get; set; }// add by anil
    }


    #region for CIN and LLPN validation add by anil


    public class CinRootData
    {
        public string message { get; set; }
        public List<CompanyData> data { get; set; }
    }

    public class CompanyData
    {
      

        public string CIN { get; set; }
        public string companyName { get; set; }
        public string incorporationDate { get; set; }
        public string companyStatus { get; set; }
        public string ROCName { get; set; }
        public string addressType { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string area { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public int pincode { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string emailAddress { get; set; }
        public string contactNumber { get; set; }
        public string financialYear { get; set; }
        public string profitLoss { get; set; }
        public string turnover { get; set; }
        public string financialRange { get; set; }
        public string isAuditStatusApplicable { get; set; }
        public string auditStatus { get; set; }
        public string type { get; set; }

    }


    public class DirectorData
    {
        public string CIN { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DIN { get; set; }
        public string DOB { get; set; }
        public string FatherFirstName { get; set; }
        public string FatherMidName { get; set; }
        public string FatherLastName { get; set; }
        public string DINStatus { get; set; }
        public string AssociationStatus { get; set; }
    }

    public class DirectorDataList
    {
        public string message { get; set; }
        public List<DirectorData> Data { get; set; }
    }


    #endregion
}
