using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AMS
/// </summary>
public class AMS
{

    public string           Action { get; set; }
    public string           ProjectTitle { get; set; }
    public string           CompanyName { get; set; }
    public string           SectorName { get; set; }
    public DateTime         ApplicationDate { get; set; }
    public string           ProjectLocation { get; set; }
	public string           ProductName { get; set; }
	public string		    Capacity{ get; set; }
    public string           Product { get; set; }
	public int		        CategoryId{ get; set; }
    public int              SectorId { get; set; }
	public int		        TypeId{ get; set; }
    public string           strUID { get; set; }

	public int		        CreatedBy{ get; set; }
    public int              ID             { get; set; }
    public int              ProjectId { get; set; }
  
    public int              OfficerId { get; set; }  
    public string           BoardOfDirectors { get; set; }
    public string           Business { get; set; }
    public int              NodalOfficerId { get; set; } 
    public string           XmlData { get; set; }

    public string           FinancialYear { get; set; }
    public string           FinancialYear1 { get; set; }
    public string           FinancialYear2 { get; set; }
    public string           DECISION { get; set; }
    public string           DecisionPoint { get; set; }
    public string           PROJECTIDLIST { get; set; }
    public string           COMMENT { get; set; }
    public int              ProjectStatus { get; set; }
    public int              AccptRejectType{ get; set; }
    public string           SLFCMember { get; set; }

    public string           CostDtls { get; set; }
    public int              TourismId { get; set; }
    public int              DistrictId { get; set; }
    public string           VCH_XMLTBL { get; set; }
    public string           FinDtls { get; set; }
    public int              CostID { get; set; }
    public int              FinID { get; set; }

    public int              FeedbackId { get; set; }
    public string           Feedback { get; set; }

    public int              ClarificationId { get; set; }
    public string           Clarification { get; set; }
    public int              DescId { get; set; }
    public string           FinAmount { get; set; }
    public string           ProposalDetails { get; set; }
    public string           Percentage { get; set; }

    public string           Materials { get; set; }
    public string           Source { get; set; }
    public string           FinDoc { get; set; }

    public string           Remark { get; set; }
    public string           UsrRemark { get; set; }
    public int              intRemarkID { get; set; }

    public string           XMLDOC { get; set; }
    public string           strUrl { get; set; }
    public string           strlandVal { get; set; }

}