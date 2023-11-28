using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer.Incentive
{
    /// <summary>
    /// Summary description for QualityCertification
    /// </summary>
    public class QualityCertification
    {
        public List<QualityCertificationActivities> QualityCertificationActivitiesDetails { get; set; }
        public decimal decCertificationTotal { get; set; }
        public int intCertificationID { get; set; }
    }
    [Serializable()]
    public class QualityCertificationActivities
    {
        public int intSectionNumber { get; set; }
        public int intCertificationSlno { get; set; }
        public string strProductName { get; set; }
        public string strNameaddressofRA { get; set; }
        public string strCertificateNo { get; set; }
        public string dtmCertificateDate { get; set; }
        public string strCertificateDetailsDOC { get; set; }
        public string intRenewalSlno { get; set; }      
        public string dtmRenewalDate { get; set; }
        public string strRenewalDateDOC { get; set; }
        public decimal strAmountofExpenditure { get; set; }
        public string strExpenditureDetails { get; set; }
    }
}
