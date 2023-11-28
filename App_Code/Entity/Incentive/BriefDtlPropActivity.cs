using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Created By - Bikash Sahoo
/// Created On - 9th-SEP-2017
/// Description - This Entity Class is used for Brief Details of Proposed Activity
/// Tables Inserted - T_INCT_BRIEF_PROPOSED_ACTIVITY, T_INCT_BRIEF_PROPOSED_ACTIVITY_DTL
/// Procedure used - USP_INCT_BriefDtlProposedActivity
/// </summary>
public class BriefDtlPropActivity
{
    public int intBrfDetailPropActivity { get; set; }
    public int INTINCUNQUEID { get; set; }
    public string vchBriefDtlProposed { get; set; }
    public string vchProsDwnStrm { get; set; }
    public string vchProsAncillary { get; set; }
    public string vchDevelopUtility { get; set; }
    public string vchExternalities { get; set; }
    public string vchProposedCFC { get; set; }
    public string vchAnyOthers { get; set; }
    public string vchDtlOfSecondTnt { get; set; }
    public string vchdtlAttractSecndTnt { get; set; }
    public string vchConsetSecndTnt { get; set; }



    public int BITDELETEDFLAG { get; set; }
    public string chrActionCode { get; set; }
    public int intSectionNo { get; set; }
    public string XmlData { get; set; }
    public List<ProposedCommonFacility> lstProposedCommonFacility { get; set; }
}


// Properties for detail table
[Serializable]
public class ProposedCommonFacility
{
    public int intPropComSecTnt { get; set; }
    public int intSlNo { get; set; }
    public string vchPropCommonFacility { get; set; }
    public int BITDELETEDFLAG { get; set; }
}


