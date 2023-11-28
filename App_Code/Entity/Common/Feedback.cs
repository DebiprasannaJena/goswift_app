using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Common
{
    public class Feedback
    {
        public string strAction     { get; set; }
        public int intFeedbackId    {get;set;}
        public string vchFirstName  {get;set;}
        public string vchLastName   { get; set; }
        public string vchEmail      { get; set; }
        public string vchMobileNo   { get; set; }
        public string vchSubject    { get; set; }
        public string vchFeedback   { get; set; }
        public int intCreatedBy     {get;set;}
        public int intUpdatedBy     {get;set;}
        public int bitDeletedFlag   {get;set;}
        public string StrFullName { get; set; }     // Added by Dharmasis sahoo
        public string StrFromDate { get; set; }     // Added by Dharmasis sahoo
        public string StrToDate { get; set; }       // Added by Dharmasis sahoo
        public string StrDate { get; set; }         // Added by Dharmasis sahoo
    }
}
