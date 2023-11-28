/*
 * Class Name : AMSNodalDetails, AMS_Search
 * File Name : AMSQuery.cs
 * Created By : Ritika Lath
 * Created On : 22nd Feb 2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class AMSNodalDetails
{
    public string strProposalNo { get; set; }
    public string strForwardedDate { get; set; }
    public string strReplyDate { get; set; }
    public string strInvReplyDate { get; set; }
    public int intConfigId { get; set; }
    public int isUpdated { get; set; }
    public int isDatePassed { get; set; }
    public int intRowCount { get; set; }
    public string strRemarks { get; set; }
    public string strFileName { get; set; }
    public string strInvResponse { get; set; }
    public string strUsername { get; set; }
    public int intUserId { get; set; }
    public int intInvReplyStatus { get; set; }
    public int intQueryConfigId { get; set; }
    public int intCreatedBy { get; set; }
    public string strActionCode { get; set; }
    public string strInvName { get; set; }
    public string strInvAppliedDate { get; set; }
    public string strQueryTime { get; set; }
    public string intNoOfTime { get; set; }
    public Boolean BitNoQuery { get; set; }
    public string strNoQuery { get; set; }
    public int intUserType { get; set; }
    public string strXml { get; set; }
    public string strGmComments { get; set; }
    public string strGmFileName { get; set; }
}


public class AMS_Search
{
    public int intIntPageIndex { get; set; }
    public int intPageSize { get; set; }
    public int intServiceId { get; set; }
    public int intYear { get; set; }
    public string strActionCode { get; set; }
    public int intUserId { get; set; }
    public int intStatus { get; set; }
    public string strProposalNo { get; set; }
}