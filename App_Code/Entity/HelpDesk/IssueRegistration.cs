using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IssueRegistration
/// </summary>

    public class IssueRegistration
    {
        public string Action { get; set; }
        public int int_IssueId { get; set; }
        public int int_CategoryId { get; set; }
        public string vch_CategoryName { get; set; }
        public int int_SubcategoryId { get; set; }
        public string vch_SubCategoryName { get; set; }
        public string vch_Type { get; set; }
        public int int_DeptId { get; set; }
        public string vch_deptName { get; set; }
        public int int_investId { get; set; }
        public string vch_investName { get; set; }
        public int int_UserId { get; set; }
        public string vch_UserName { get; set; }
        public string vch_IssueDetails { get; set; }
        public string vch_IssueSource { get; set; }
        public string vch_FIleUpload { get; set; }
        public int intCreatedBy { get; set; }
        public int intUpdatedBy { get; set; }
        public string dtmCreatedOn { get; set; }
        public string vchIssueNo { get; set; }
        public string vchFromDate { get; set; }
        public string vchToDate { get; set; }
        public int status { get; set; }
        public string strRemark { get; set; }
        public string vch_fileUploadpopup { get; set; }
        public string OtherName { get; set; }
        public string Address { get; set; }
       
        public string vch_Priority { get; set; }
        public string VchMobile { get; set; }
        public string Email { get; set; }
        public string vchMobileContent { get; set; }
        public string vchEmailContent { get; set; }
        public string vchUserEmail { get; set; }
        public string vchUserMobileNo{ get; set; }
        public string VCHATAMOBILENO { get; set; }
        public string VCHATAEMAIL { get; set; }

        public string VCH_STATUS { get; set; }

        public string CategoryName { get; set; }

        public string SubCategory { get; set; }

       

        public string UserManual { get; set; }

        public string ServiceName { get; set; }
        public int ServiceId { get; set; }

        public string Applied { get; set; }

        public string Total_Recieved { get; set; }

        public string Discard { get; set; }

        public string Pending { get; set; }

        public string Approved { get; set; }
        public int intLevelid { get; set; }
        public string VCH_STANDARD_DAYS { get; set; }
        public int intEscConfigId { get; set; }
        public int int_EscLevel { get; set; }

        public int intTypeId { get; set; }
        public string strTypeName { get; set; }

        public string strXmlHelpDesk { get; set; }
        public string Total_Count { get; set; }
        public string Total_Resolved { get; set; }
        public string Total_Pending { get; set; }
        public string Total_IssueResolvedSLA { get; set; }
        public string Total_IssueOpenPastSLA { get; set; }
        public string Total_IssuePastSLA { get; set; }
        public string Total_Resoved_Hour { get; set; }
        public string dtmUpdatedOn { get; set; }
        public string dtDiffrence { get; set; }
        public string vchInveUserId { get; set; } // Add by Anil

    }