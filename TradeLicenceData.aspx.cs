
//******************************************************************************************************************
// File Name             :   TradeLicenceData.aspx.cs
// Description           :   Shows the District and Ulb according 
// Created by            :   AMit Sahoo
// Created on            :   30th June 2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAcessLayer.Service;
using BusinessLogicLayer.Service;
using EntityLayer.Service;
using System.Configuration;
public partial class TradeLicenceData : SessionCheck
{
    #region Global Variable and Object
    /// <summary>
    /// Prasun Kali
    /// All global variable and object are declared here
    /// </summary>
    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    ServiceDetails objServiceEntity = new ServiceDetails();
    
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserId"] != null)
            {
                FillDistrict();
                
            }
            else
            {
                Response.Redirect("~/inestorlogin.aspx");
            }
        }
    }

    #region BindDistrict
    /// <summary>
    /// Prasun Kali
    /// Here all the district are binded
    /// </summary>
    /// <param name="FormId"></param>
    private void FillDistrict()
    {
        IList<ServiceDetails> objIndList = new List<ServiceDetails>();
        ddlDistrict.DataSource = objService.PopulateDistrict();
        ddlDistrict.DataTextField = "StrDistname";
        ddlDistrict.DataValueField = "DistId";
        ddlDistrict.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlDistrict.Items.Insert(0, list);

    }
    #endregion
    # region BindUlb
    /// <summary>
    /// Prasun Kali
    /// Here Ulb Dropdown Binded as per the selected District
    /// </summary>
    /// <param name="DistrictId"></param>
    protected void ddlDistrict_SelectedIndexChanged1(object sender, EventArgs e)
    {
        IList<ServiceDetails> objIndList = new List<ServiceDetails>();
        ddlULB.DataSource = objService.PopulateULB(Convert.ToInt32(ddlDistrict.SelectedValue.Trim()));
        ddlULB.DataTextField = "StrDistname";
        ddlULB.DataValueField = "StrDireName";
        ddlULB.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlULB.Items.Insert(0, list);
    }

    #endregion
  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string appLandUrl = ConfigurationManager.AppSettings["TradeLicense"].ToString();

        //Response.Redirect("http://117.240.239.40/or/ulb/citizen-services?p_p_id=eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__spage=%2Fportlet_action%2FEmunicipality_portlet%2Ftradelicense-status%2Faction&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__sorig=%2Fportlet_action%2FEmunicipality_portlet%2Ftl%2Frender&ulbCode=" + ddlULB.SelectedValue.ToString() + "&swpId=" + Session["InvestorId"].ToString() + "&proposalId=" + Request.QueryString["ProposalNo"].ToString() + "");

        Response.Redirect("" + appLandUrl + "?p_p_id=eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__spage=%2Fportlet_action%2FEmunicipality_portlet%2Ftradelicense-status%2Faction&_eMunicipality_WAR_Emunicipalityportlet_INSTANCE_4cJF__sorig=%2Fportlet_action%2FEmunicipality_portlet%2Ftl%2Frender&ulbCode=" + ddlULB.SelectedValue.ToString() + "&swpId=" + Session["InvestorId"].ToString() + "&proposalId=" + Request.QueryString["ProposalNo"].ToString() + "");


    }
}
