//******************************************************************************************************************
// File Name             :   Incentive\QueryMgntDetails.cs
// Description           :   To declare variables abd properties for Query Management Details
// Created by            :   Pranay Kumar
// Created on            :   10-OCT-2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QueryMgntDetails
/// </summary>

//public class QueryMgntDetails
//{
//    public QueryMgntDetails()
//    {
//        //
//        // TODO: Add constructor logic here
//        //
//    }  
//}

namespace EntityLayer.Incentive
{
    public class QueryMgntDtls
    {
        public string strAction { get; set; }
        public int intCreatedBy { get; set; }
        public string strActionToBeTakenBY { get; set; }
        public string strRemarks { get; set; }
        public int intQueryId { get; set; }
        public string strApplicationNum { get; set; }      
        public string strIncentiveUnqNo { get; set; }
        public string strFileName { get; set; }
        public string dtmCreatedOn { get; set; }
        public string strQueryDesc { get; set; }
        public string strQuerytype { get; set; }
        public int intStatus { get; set; }
        public string strMobileNo { get; set; }
        public string strEmailBody { get; set; }
        public string strEmailID { get; set; }
        public string strEmailSubject { get; set; }
        public int intNoOfTimes { get; set; }
        public string strXML { get; set; }
        public int intQueryStatus { get; set; }
        public int intExtendedStatus { get; set; }
        public string strStatus { get; set; }
        public string strQueryUnqNo { get; set; }
        public string strSMSContent { get; set; }
    }
}