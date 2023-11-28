using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityLayer.Proposal
{
    public class CNET
    {
        public string vchProposalNo { get; set; }
        public string gui_start_value_from_swp { get; set; }
        public string gui_end_value_from_swp { get; set; }
        public string unique_application_id_from_swp { get; set; }
        public string industry_code { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public string company_country { get; set; }
        public string company_state { get; set; }
        public string company_city { get; set; }
        public string company_phnumber { get; set; }
        public string company_faxnumber { get; set; }
        public string company_email { get; set; }
        public string company_pincode { get; set; }
        public string company_gst_no { get; set; }
        public string company_project_type { get; set; }
        public string company_application_for { get; set; }
        public string company_year_of_incorporation { get; set; }
        public string company_place_of_incorporation { get; set; }
        public string educational_qualification { get; set; }
        public string technical_qualification { get; set; }
        public string company_contact_person_name { get; set; }
        public string company_contact_person_address { get; set; }
        public string company_contact_person_country { get; set; }
        public string company_contact_person_state { get; set; }
        public string company_contact_person_city { get; set; }
        public string company_contact_person_mobno { get; set; }
        public string company_contact_person_email { get; set; }
        public string company_contact_person_pincode { get; set; }
        public string company_constitution_type { get; set; }
        public string company_no_partner { get; set; }
        public string company_managing_partner_name { get; set; }
       
        public string company_curr_annual_turnover { get; set; }
        public string company_prev_annual_turnover { get; set; }
        public string company_prev_to_last_annual_turnover { get; set; }
        public string company_curr_profit_tax { get; set; }
        public string company_prev_profit_tax { get; set; }
        public string company_prev_to_last_profit_tax { get; set; }
        public string company_curr_net_worth { get; set; }
        public string company_prev_net_worth { get; set; }
        public string company_prev_to_last__net_worth { get; set; }
        public string company_curr_surplus { get; set; }
        public string company_prev_surplus { get; set; }
        public string company_prev_to_last_surplus { get; set; }
        
        public string exprience_in_year { get; set; }

        public string company_curr_share_capital { get; set; }
        public string company_prev_share_capital { get; set; }
        public string company_prev_to_last_share_capital { get; set; }
        public string eim_iem_il { get; set; }
        public string sector { get; set; }
        public string sub_sector { get; set; }
        public string proposed_annual_capacity { get; set; }
        public string unit_proposed_annual_capacity { get; set; }
        public string project_coming_under { get; set; }
        public string comercial_production_period { get; set; }
        public string polution_category { get; set; }
        public string ground_breaking { get; set; }
        public string civil_structural { get; set; }
        public string major_equipment_erection { get; set; }
        public string start_of_comercial_production { get; set; }
        public string land_development_cost { get; set; }
        public string building_cost { get; set; }
        public string plant_machinary_cost { get; set; }
        public string others_cost { get; set; }
        public string total_cost { get; set; }
        public string irr { get; set; }
        public string dscr { get; set; }
        public string equity_contribution { get; set; }
        public string bank_finance { get; set; }
        public string fdi_investment { get; set; }
        public string ext_manager { get; set; }
        public string proposed_manager { get; set; }
        public string ext_superviser { get; set; }
        public string proposed_superviser { get; set; }
        public string ext_skilled { get; set; }
        public string proposed_skilled { get; set; }
        public string ext_semiskilled { get; set; }
        public string proposed_semiskilled { get; set; }
        public string ext_unskilled { get; set; }
        public string proposed_unskilled { get; set; }
        public string ext_total { get; set; }
        public string proposed_total { get; set; }
        public string proposed_direct_employeement { get; set; }
        public string proposed_contractual_employeement { get; set; }
        public string other_location { get; set; }
        public string outside_location { get; set; }
        public string industry_district { get; set; }
        public string industry_tahasil { get; set; }
        public string land_industry_estate_area { get; set; }
        
        public string total_area { get; set; }
        public string power_demand { get; set; }
        public string power_drawl { get; set; }
        public string power_capacity { get; set; }
       
        public string existing_water_requirement { get; set; }
        public string proposed_water_requirement { get; set; }
        public string water_requirement_for_production { get; set; }
        public string quantum_waste_water { get; set; }
        public string recommended_land_ipicol { get; set; }

        public string specification { get; set; }
        public string nodal_agency_code { get; set; }
        public string project_code { get; set; }
        public string pan { get; set; }
        public string district_code { get; set; }

      
        public List<BoardDirector> company_other_constitution { get; set; }
        public List<CNETPromo> company_promoter { get; set; }
        public List<CNETRawMet> raw_material { get; set; }
        public List<CNETProjectLocation> project_location { get; set; }
        public List<CNETProjLocDet> project_locationDet { get; set; }
        public List<CNETArea> industry_estate_area { get; set; }
        public List<CNETBank> industry_land_bank { get; set; }
        public List<CNETSourcePower> sources_of_power { get; set; }
        public List<CNETSourceWater> sources_of_water { get; set; }
        public List<CNETDocument> document_list { get; set; }
        public List<CNETProductName> product_details { get; set; }

    }
    public class CNETProductName
    {
        public string product_name { get; set; }
        public string proposed_annual_capacity { get; set; }
        public string unit_proposed_annual_capacity { get; set; }
    }
    public class CNETArea
    {
        public string area_name { get; set; }
    }
    public class CNETBank
    {
        public string bank_name { get; set; }
    }
    public class CNETDocument
    {
        public string document_name { get; set; }
        public string document_status { get; set; }
        public string document_Link { get; set; }
    }
    public class CNETProjectLocation
    {
        public string unitName { get; set; }
        public string product_name { get; set; }
        public string total_capacity { get; set; }
        public string state { get; set; }
        public string district { get; set; }
    }
    public class CNETProjLocDet
    {
        public string unitName { get; set; }
        public string product_name { get; set; }
        public string total_capacity { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
    public class CNETPromo
    {
        public string promoter_name { get; set; }
    }
    public class CNETRawMet
    {
        public string raw_material_name { get; set; }
        public string material_source { get; set; }
    }
    public class CNETSourcePower
    {
        public string source_of_power { get; set; }
    }
    public class CNETSourceWater
    {
        public string source_of_water { get; set; }

    }
    public class BoardDirector
    {
        public string director_designation { get; set; }
        public string director_name { get; set; }
    }
}
