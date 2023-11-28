using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DLSWCAApprovalDtls
/// </summary>
public class DLSWCAApprovalDtls
{
    public int intDLSWCAID { get; set; }
    public int intUniqueID { get; set; }
    public int intSectionNo {get;set;}
    public string dtmApprovalDate { get; set; }
    public decimal dcmLandRequired { get; set; }
    public decimal dcmCostOfLand { get; set; }
    public decimal dcmSubsidyAmount { get; set; }
    public string strDLSWCAApprovalDoc { get; set; }
    public string strsubstantitateDoc { get; set; }
    public int intCreatedBy { get; set; }
}