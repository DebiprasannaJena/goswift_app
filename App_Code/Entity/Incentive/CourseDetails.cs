using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseDetails
/// </summary>
/// 
namespace EntityLayer.Incentive
{
    public class CourseDetails
    {
        public int INT_Section_No { get; set; }
        public int IntCourseDetails { get; set; }
        public int InstitutionName { get; set; }
        public string OtherInstitutionName { get; set; }
        public int InstitutionLocation { get; set; }
        public string InstitutionAddress { get; set; }
        public string CourseDuratio { get; set; }
        public decimal CourseAmount { get; set; }
        public string CourseAttachment { get; set; }
        public string Dateofselection { get; set; }
        public string Copyofletterofselection { get; set; }
        public string ProvSacLetter { get; set; }
        public string DateofSanction { get; set; }
        public string SanctionNo { get; set; }
        public int INTINCUNQUEID { get; set; }
        public int INTINDUSTRAILUNIT { get; set; }
    }
}