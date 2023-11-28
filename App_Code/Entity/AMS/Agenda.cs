/// <summary>
/// Summary description for Agenda
/// </summary>
public class Agenda
{
	public Agenda()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public string Action { get; set; }
    public int Id { get; set; }
    public int OfficerType { get; set; }   
    public string OfficerId { get; set; }
    public int CreatedBy { get; set; }
    public int UserId { get; set; }

    public int ProjectId { get; set; }
    public decimal CivilCost { get; set; }
    public decimal PlantCost { get; set; }
    public decimal OtherCost { get; set; }
    public string FinaceDetails { get; set; }
    public string FinanceDescription { get; set; }
    public string Land { get; set; }
    public string Water { get; set; }
    public string Power { get; set; }
    public int Source { get; set; }
    public string ImplementPeriod { get; set; }
    public string Material { get; set; }
    public int Employement { get; set; }
    public int Contractual { get; set; }
    public string VCH_XMLTBL { get; set; }
    public string VCH_XMLFINTBL { get; set; }
    public string VCH_XMLSOURCE { get; set; }
    
    public string Cost { get; set; }
    public string PercentageCost { get; set; }
    public string Remark { get; set; }
    public int intRemarkID { get; set; }
}