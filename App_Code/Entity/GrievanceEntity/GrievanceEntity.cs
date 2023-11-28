using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GrievanceEntity
/// </summary>
/// 
namespace EntityLayer.GrievanceEntity
{
    public class GrievanceEntity
    {
        public string StrAction { get; set; }
        public int intInvestorId { get; set; }
        public int intInvestmentLevel { get; set; }
        public int intGrivId { get; set; }
        public string vchApplicantName { get; set; }
        public string vchDesignation { get; set; }
        public string vchMobileNo { get; set; }
        public string vchEmail { get; set; }
        public int intDistrictId { get; set; }
        public string vchDistrictName { get; set; }
        public int intGrivTypeId { get; set; }
        public int intGrivSubTypeId { get; set; }
        public string vchGrivTitle { get; set; }
        public string vchGrivSubTypeId { get; set; }
        public string vchGrivDetail { get; set; }
        public string vchAttachment1 { get; set; }
        public string vchAttachment2 { get; set; }
        public int intStatus { get; set; }
        public string vchRemarks { get; set; }
        public int intCreatedBy { get; set; }

        #region Added By Sushant Jena

        public string strGrivId { get; set; }
        public string strFromDate { get; set; }
        public string strToDate { get; set; }
        public int intUserId { get; set; }
        public int intActionTobeTakenBy { get; set; }
        public string strReferenceFilename { get; set; }
        public string strRemark { get; set; }
        public int intActionTakenBy { get; set; }

        #endregion

        #region Added by Anil Sahoo

        public string strGrivType { get; set; }
        public int intGrivActiveStatus { get; set; }
        public string strGrivSubType { get; set; }

        public int IntIndustryCategory { get; set; }

        public int IntSmsId { get; set; }

        #endregion

        #region Added by Arabinda Tripathy
        public int intCreatedByDept { get; set; }
        #endregion
    }
}

