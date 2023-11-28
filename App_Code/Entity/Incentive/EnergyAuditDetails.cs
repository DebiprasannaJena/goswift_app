using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EnergyAuditDetails
/// </summary>
public class EnergyAuditDetails
{
    public int intSectionNumber { get; set; }
    public string strEnergyAuditorName { get; set; }
    public string strEnergyAuditorDocName { get; set; }
    public string strEnergyAuditorAddress { get; set; }
    public string strEnergyAuditorAccreditation { get; set; }
    public string strEnergyAuditorAccreditationDoc { get; set; }
    public decimal strExpenditureincurred { get; set; }
    public string strExpenditureincurredDoc { get; set; }
    public DateTime? dtmSuccessfulcompletionAuditDate { get; set; }
    public string strSupportofimplementationofEnergyDOC { get; set; }
    public string strSuccessfulcompletionAuditDOC { get; set; }
    public decimal dtmEnergyConsumptionAfter { get; set; }
    public decimal strEnergyConsumptionBefore { get; set; }
    public string strReductionOfEnergyDoc { get; set; }
    public string strEnergyEfficiencyCertificate { get; set; }
    public int intEnergyAuditID { get; set; }
    public string strCarbonFootprintDoc { get; set; }
}