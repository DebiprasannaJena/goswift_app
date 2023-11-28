using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

/// <summary>
/// Summary description for WaterAllotmentDetails
/// </summary>
public class WaterAllotmentDetails
{
	public WaterAllotmentDetails()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    public string Action { get; set; }
    public string strApplicationId { get; set; }
    public string strInvestorName { get; set; }
    public string strProposalId { get; set; }
    public int intServiceId { get; set; }
    public string strIndustryCode { get; set; }
    public string strUnitName { get; set; }
    public int intIEId { get; set; }
    public string strPlotShedNo { get; set; }
    public int intPupose { get; set; }
    public string strQuantity { get; set; }
    public string strFlowMeterSize { get; set; }
    public string strMakeModel { get; set; }
    public string strManfSerialNo { get; set; }
    public string strOHTankSize { get; set; }
    public string strOHTankNo { get; set; }
    public string strSumpVatSize { get; set; }
    public string strSumpVatNo { get; set; }
    public string strContactName { get; set; }
    public string strContactEmail { get; set; }
    public string strContactMobile { get; set; }
    public string strContactAddress { get; set; }
    public string strPlumberName { get; set; }
    public string strPlumberEmail { get; set; }
    public string strPlumberMobile { get; set; }
    public string strPlumberAddress { get; set; }
    public int intCreatedBy { get; set; }
    public string strRefNo { get; set; }
    public int intStatus { get; set; }
    public string PaymentUrl { get; set; }
}
public class clsWaterAllotment
{
    public string swp_application_id { get; set; }
    public string party_details { get; set; }
    public string industrial_estate_area { get; set; }
    public string plot_shed_no { get; set; }
    public string proposeOf_water_connection { get; set; }
    public string water_required_per_day { get; set; }
    public string flow_meter_size { get; set; }
    public string make_model { get; set; }
    public string manufacture_serial_no { get; set; }
    public string oh_tank_size { get; set; }
    public string oh_tank_no { get; set; }
    public string sump_size { get; set; }
    public string sump_no { get; set; }
    public string contact_person_name { get; set; }
    public string contact_address { get; set; }
    public string contact_email { get; set; }
    public string contact_mobile { get; set; }
    public string plumber_name { get; set; }
    public string plumber_address { get; set; }
    public string plumber_email { get; set; }
    public string plumber_mobile { get; set; }
    public string industry_code { get; set; }
}