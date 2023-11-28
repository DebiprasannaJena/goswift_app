using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AvailDetails
/// </summary>
public class AvailDetails
{
    public AvailDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<Assistance> AssistanceDetails { get; set; }//XML
    public string UndertakingSubsidyDoc { get; set; }
    public int SubsidyAvailed { get; set; }
    public List<Assistance> IncentiveAvailed { get; set; }//XML
    public string SupportingDocs { get; set; }
    public double ClaimtExempted { get; set; }
    public double ClaimReimbursement { get; set; }
    public int NeverAvailedPrior { get; set; }


    public double DecDiffAmtClaim { get; set; }
    public string SanctionOrderDoc { get; set; }
    public decimal decClaimReimbursementEPF { get; set; }
}
public class Assistance
{
    public string Body { get; set; }
    public string InstitutionName { get; set; }
    public double AmountAvailed { get; set; }
    public DateTime AvailedDate { get; set; }
    public double SanctionedAmount { get; set; }
    public string SanctionOrderNo { get; set; }
}
