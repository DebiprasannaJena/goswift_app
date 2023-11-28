using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InvestmentPollution
/// </summary>
public class InvestmentPollution
{
    public int intInvestmentID { get; set; }
    public int intInvstmntPllutionID { get; set; }
    public int intUniqueID { get; set; }
    public int intSectionNo { get; set; }
    public int intCreatedBy { get; set; }
    public string operationalizationDate { get; set; }
    public string dtmCreatedOn { get; set; }
    public string operationalizationDOC { get; set; }
    public List<InvestmentPollutionDetails> lstInvestPollution { get; set; }
}

public class InvestmentPollutionDetails //For ADD More
{
    public int intSectionNo { get; set; }
    public string strEquipmentName { get; set; }
    public int? EquipmentTYPE { get; set; }
    public string OtherEquiType { get; set; }
    public decimal dcmInvestedAmt { get; set; }
    public int intCreatedBy { get; set; }
}