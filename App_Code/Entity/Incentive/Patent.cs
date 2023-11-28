using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Patent
/// </summary>
/// 
public class PatentDetails
{   //Patented Items Details
    public int intSectionNo { get; set; }
    public int IntIncUnqueId { get; set; }
    public int intPatentId { get; set; }
    public List<ItemDetails> lstitemsDetails { get; set; }
    public List<PatentLoanDetails> lstPatLoanDetails { get; set; }

    public string AgencyName { get; set; }

    public string AgencyAddress { get; set; }
}
[Serializable]
public class ItemDetails
{
    public int slno { get; set; }
    public string IntCatgoryid { get; set; }
    public string VchCatgoryName { get; set; }
    public string IntSubCatgoryid { get; set; }
    public string VchSubCatgoryName { get; set; }
    public string vchItemName { get; set; }
    public string vchAuthorityNm { get; set; }
    public string dtCommercialDt { get; set; }
    public string vchIPRRegistrationNo { get; set; }
    public string dtRegistrationDate { get; set; }
    public decimal decExpenditureincurred { get; set; }
    public string vchIPRRegistrationFile { get; set; }
    public string vchExpenditureFile { get; set; }
    public string vchAgencyName { get; set; }
    public string vchAgencyAddress { get; set; }
}
[Serializable]
public class PatentLoanDetails
{
    //Loan Details
    public int slno { get; set; }
    public string FinancialInstitutionNm { get; set; }
    public decimal AmountAvailed { get; set; }
    public string AmountAvailedDate { get; set; }
    public string LoanNumber { get; set; }
}