using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLayer.Incentive
{
    /// <summary>
    /// Summary description for IndustrialUnit
    /// </summary>
    public class IndustrialUnit
    {
        public string Name { get; set; }
        public int OrganizationTypeId { get; set; }
        public string OrganizationType { get; set; }
        public string NameOfApplicant { get; set; }
        public int ApplicationApplyingBy { get; set; }
        public string ApplicantAdhaar { get; set; }
        public string IndustrialAddress { get; set; }
        public int UnitCategory { get; set; }
        public int UnitType { get; set; }
        public string RehabilitatedDocument { get; set; }
        public string lndustrialDocument { get; set; }
        public int IsPriority { get; set; }
        public string RegisteredOfficeAddress { get; set; }
        public string ManagingPartnerName { get; set; }
        public string CertificateDocument { get; set; }
        public string EIN { get; set; }
        public DateTime EINDate { get; set; }
        public string PCNo { get; set; }
        public string PCIssuanceDate { get; set; }
        public DateTime CommencementDate { get; set; }
        public string CommencementDocument { get; set; }

    }
}