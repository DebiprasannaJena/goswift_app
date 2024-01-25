
// *******************************************************************************************************************
// File Name         : ThrustPrioritysectorstatus.aspx
// Description       : Thrust Priority Sector Status IPR-2022 Add and Draft Page
// Created by        : Debiprasanna Jena
// Created On        : 07th June 2023
// Modification History:

// <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

// *********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAcessLayer.Common;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Collections.Specialized;
using System.IO;

public partial class incentives_ThrustPrioritysectorstatus : System.Web.UI.Page
{
   readonly string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            FillUnitCategory();
            FillOrgType();
            FillSalutation();
            FillUnitMeasurment();
            Txt_Other_Unit_Before.Visible = false;

            if (Request.QueryString["InctUniqueNo"] != null)
            {
                PostpopulateDataComm();
            }
            else
            {
                Txt_EIN_IL_Date.Attributes.Add("readonly", "readonly");
                Txt_Proposed_Date.Attributes.Add("readonly", "readonly");
                Txt_Commence_production.Attributes.Add("readonly", "readonly");
                Txt_PC_EMI_Date.Attributes.Add("readonly", "readonly");
                Txt_Uam_Date.Attributes.Add("readonly", "readonly");
                Txt_TL_Sanction_Date.Attributes.Add("readonly", "readonly");
                Txt_TL_Availed_Date.Attributes.Add("readonly", "readonly");
                Txt_WC_Sanction_Date.Attributes.Add("readonly", "readonly");
                Txt_WC_Availed_Date.Attributes.Add("readonly", "readonly");
                Div_UAM_No.Visible = false;
                Div_UAM_date.Visible = false;
                Div_EIM_No.Visible = false;
                Div_Eim_date.Visible = false;
                FillUnitMeasurment();
                FillUnitCategory();
                Txt_Other_Unit_Before.Visible = false;
                Txt_EnterPrise_Name.Enabled = false;
                FillData();
                FillOrgType();
                FillSalutation();
                BtnDraft.Visible = true;
                BtnApply.Visible = true;
            }
        }
    }

    public void PostpopulateDataComm()
    {
        try
        {
            DataSet dslivePre = IncentiveManager.ProvisionalThrustsectorpopulateDatainDraft(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            DataTable dtindustryPre = dslivePre.Tables[0];////////////Industrial Unit's Details
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment
            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////Term Loan Details
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////Working Capital Loan Details
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////Product Details
            DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////investment details
            DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Other Document List

            #region IndustrailUnit

            if (dtindustryPre.Rows.Count > 0)
            {
                Txt_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString().Trim();
                DrpDwn_Unit_Cat.SelectedValue = (dtindustryPre.Rows[0]["intUnitCat"].ToString());
                Txt_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                Txt_Regd_Office_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();
                if ((dtindustryPre.Rows[0]["vchIndustryAddress"].ToString()) == (dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString()))
                {
                    ChkSameData.Checked = true;
                }
                else
                {
                    ChkSameData.Checked = false;
                }
                Txt_Phone_no.Text = dtindustryPre.Rows[0]["vchPhoneNumber"].ToString();
                Txt_Email.Text = dtindustryPre.Rows[0]["vchEmail"].ToString();
                DrpDwn_Org_Type.SelectedValue = (dtindustryPre.Rows[0]["intOrganisationType"].ToString());

                if (DrpDwn_Org_Type.SelectedValue == "15")
                {
                    Lbl_Org_Name_Type.Text = "Name of Proprietor";
                }
                else if (DrpDwn_Org_Type.SelectedValue == "17")
                {
                    Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                }
                else if (DrpDwn_Org_Type.SelectedValue == "20")
                {
                    Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
                }
                else if (DrpDwn_Org_Type.SelectedValue == "19")
                {
                    Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
                }
                else if (DrpDwn_Org_Type.SelectedValue == "18")
                {
                    Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
                }
                Txt_Partner_Name.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                DrpDwn_Gender_Partner.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
                Txt_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
                Txt_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
            ///*--------------------------------------------------------------------------------------------------------------------*/

                #region Production
                Grd_Production_Before.DataSource = dtInvestmentPre;
                Grd_Production_Before.DataBind();
                if (DrpDwn_Unit_Before.SelectedIndex > 0)
                {
                    if (DrpDwn_Unit_Before.SelectedValue == "52")
                    {
                        Txt_Other_Unit_Before.Visible = true;
                        Txt_Other_Unit_Before.Focus();
                    }
                    else
                    {
                        Txt_Other_Unit_Before.Text = "";
                        Txt_Other_Unit_Before.Visible = false;
                        Txt_Value_Before.Focus();
                    }
                }
                else
                {
                    Txt_Other_Unit_Before.Text = "";
                    Txt_Other_Unit_Before.Visible = false;
                }

                #endregion
                Txt_Proposed_Date.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
                Txt_Commence_production.Text = dtProductionPre.Rows[0]["dtmFFCIDateBefore"].ToString();
                Rad_production.SelectedValue = dtMoFTermLoanPre.Rows[0]["intEIMorUAMtype"].ToString();

                if (Rad_production.SelectedValue == "1")
                {
                    Txt_PC_EMI_No.Text = dtMoFTermLoanPre.Rows[0]["vchEIMorUAMnumber"].ToString();
                    Txt_PC_EMI_Date.Text = dtMoFTermLoanPre.Rows[0]["dtmEIMorUAMdate"].ToString();
                    Div_EIM_No.Visible = true;
                    Div_Eim_date.Visible = true;
                    Div_UAM_No.Visible = false;
                    Div_UAM_date.Visible = false;

                }
                else if (Rad_production.SelectedValue == "2")
                {
                    Txt_Uam_No.Text = dtMoFTermLoanPre.Rows[0]["vchEIMorUAMnumber"].ToString();
                    Txt_Uam_Date.Text = dtMoFTermLoanPre.Rows[0]["dtmEIMorUAMdate"].ToString();
                    Div_UAM_No.Visible = true;
                    Div_UAM_date.Visible = true;
                    Div_EIM_No.Visible = false;
                    Div_Eim_date.Visible = false;

                }
                else if (Rad_production.SelectedValue == "3")
                {
                    Div_UAM_No.Visible = false;
                    Div_UAM_date.Visible = false;
                    Div_EIM_No.Visible = false;
                    Div_Eim_date.Visible = false;
                    Txt_PC_EMI_No.Text = "";
                    Txt_PC_EMI_Date.Text = "";
                    Txt_Uam_No.Text = "";
                    Txt_Uam_Date.Text = "";
                }
              /*--------------------------------------------------------------------------------------------------------------------------*/

                #region Investment
                Txt_total_emp_Number.Text = dtMoFTermLoanPre.Rows[0]["intDirectEmpAfter"].ToString();
                Txt_Land_Details_before.Text = dtProductionPre.Rows[0]["decLandAmtBefore"].ToString();
                Txt_Building_Before.Text = dtProductionPre.Rows[0]["decBuildingAmtBefore"].ToString();
                Txt_Electrical_inst_Before.Text = dtProductionPre.Rows[0]["decPlantMachAmtBefore"].ToString();
                Txt_Plant_Mach_Before.Text = dtProductionPre.Rows[0]["decOtheFixedAssetAmtBefore"].ToString();
                Txt_Other_Fixed_Asset_Before.Text = dtProductionPre.Rows[0]["decLoadUnloadAmtBefore"].ToString();
                Txt_Loadig_Before.Text = dtProductionPre.Rows[0]["decMarginMoneyForworkingAmtBefore"].ToString();
                Txt_Margine_money.Text = dtProductionPre.Rows[0]["decElectricalInstAmtBefore"].ToString();
                Txt_Total_Capital_invst.Text = dtProductionPre.Rows[0]["decTotalAmtBefore"].ToString();

                Txt_Land_After.Text = dtProductionPre.Rows[0]["decLandAmtAfter"].ToString();
                Txt_Building_After.Text = dtProductionPre.Rows[0]["decBuildingAmtAfter"].ToString();
                Txt_Electrical_inst_After.Text = dtProductionPre.Rows[0]["decPlantMachAmtAfter"].ToString();
                Txt_Plant_Mach_After.Text = dtProductionPre.Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
                Txt_Other_Fixed_Asset_After.Text = dtProductionPre.Rows[0]["decElectricalInstAmtAfter"].ToString();
                Txt_Loadig_After.Text = dtProductionPre.Rows[0]["decMarginMoneyForworkingAmtAfter"].ToString();
                Txt_Margine_money_After.Text = dtProductionPre.Rows[0]["decLoadUnloadAmtAfter"].ToString();
                Txt_Total_Capital_After.Text = dtProductionPre.Rows[0]["decTotalAmtAfter"].ToString();

                #endregion

                Txt_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();

                if (dtProductionDetBefPre.Rows.Count > 0)
                {
                    Grd_TL.DataSource = dtProductionDetBefPre;
                    Grd_TL.DataBind();
                }

                if (dtProductionDetAftPre.Rows.Count > 0)
                {
                    Grd_WC.DataSource = dtProductionDetAftPre;
                    Grd_WC.DataBind();
                }
                Rad_Clearnce_PCB.SelectedValue = dtindustryPre.Rows[0]["intProjectClearance"].ToString();
                Rad_PP_thrust_Status.SelectedValue = dtindustryPre.Rows[0]["intProvisnalPriorityThrustStatus"].ToString();
                Rad_IPR_Incentive_avail.SelectedValue = dtindustryPre.Rows[0]["intIPRinctiveAvel"].ToString();
                Txt_Swm_approve.Text = dtindustryPre.Rows[0]["vchClearnceswm"].ToString();

                ///----------------------------------------Other File Upload----------------------------------------------------------///

                for (int i = 0; i < dtMoFWorkingLoanPre.Rows.Count; i++)
                {
                    string vchDocId = dtMoFWorkingLoanPre.Rows[i]["vchDocId"].ToString();
                    string vchFileName = dtMoFWorkingLoanPre.Rows[i]["vchFileName"].ToString();

                    if (vchDocId == "D282")
                    {
                        HdnPowerattpre_Name.Value = vchFileName;
                        HypPowerattpre.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypPowerattpre.Visible = true;
                        LnkDPowerattpre.Visible = true;
                        LblPowerattpre.Visible = true;
                        FluPowerattpre.Enabled = false;
                    }
                    else if (vchDocId == "D283")
                    {
                        HdnCertofreg_Name.Value = vchFileName;
                        HypvwCertofreg.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwCertofreg.Visible = true;
                        LnkDCertofreg.Visible = true;
                        LblCertofreg.Visible = true;
                        FluCertofreg.Enabled = false;
                    }
                    else if (vchDocId == "D284")
                    {
                        HdnAppDPR_Name.Value = vchFileName;
                        HypVwAppDPR.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwAppDPR.Visible = true;
                        LnkDAppDPR.Visible = true;
                        LblAppDPR.Visible = true;
                        FlAppDPR.Enabled = false;
                    }
                    else if (vchDocId == "D285")
                    {
                        HdnEIN_Name.Value = vchFileName;
                        HypVwEIN.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwEIN.Visible = true;
                        LnkDEIN.Visible = true;
                        LblEIN.Visible = true;
                        FluEIN.Enabled = false;
                    }
                    else if (vchDocId == "D286")
                    {
                        HdnPlantmachinery_Name.Value = vchFileName;
                        HyVwPlantmachinery.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HyVwPlantmachinery.Visible = true;
                        LnkDPlantmachinery.Visible = true;
                        LblPlantmachinery.Visible = true;
                        FluPlantmachinery.Enabled = false;
                    }
                    else if (vchDocId == "D287")
                    {
                        HdnCapitalInvst_Name.Value = vchFileName;
                        HypVwCapitalInvst.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwCapitalInvst.Visible = true;
                        LnkDCapitalInvst.Visible = true;
                        LblCapitalInvst.Visible = true;
                        FluCapitalInvst.Enabled = false;
                    }
                    else if (vchDocId == "D288")
                    {
                        HdnInvplantmachinary_Name.Value = vchFileName;
                        HypVwInvplantmachinary.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwInvplantmachinary.Visible = true;
                        LnkDInvplantmachinary.Visible = true;
                        LblInvplantmachinary.Visible = true;
                        FluInvplantmachinary.Enabled = false;
                    }
                    else if (vchDocId == "D289")
                    {
                        HdnProposedprod_Name.Value = vchFileName;
                        HypVwProposedprod.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwProposedprod.Visible = true;
                        LnkDproposedprod.Visible = true;
                        LblProposedprod.Visible = true;
                        FluProposedprod.Enabled = false;
                    }
                    else if (vchDocId == "D290")
                    {
                        HdnPresentstageimplemnt_Name.Value = vchFileName;
                        HypVwpresentstageimplemnt.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwpresentstageimplemnt.Visible = true;
                        LnkDpresentstageimplemnt.Visible = true;
                        LblPresentstageimplemnt.Visible = true;
                        FluPresentstageimplemnt.Enabled = false;
                    }
                    else if (vchDocId == "D291")
                    {
                        HdnMigrantindustrial_Name.Value = vchFileName;
                        HypvwMigrantindustrial.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwMigrantindustrial.Visible = true;
                        LnkDmigrantindustrial.Visible = true;
                        LblMigrantindustrial.Visible = true;
                        FluMigrantindustrial.Enabled = false;
                    }
                    else if (vchDocId == "D292")
                    {
                        HdnFixedcapitalinvst_Name.Value = vchFileName;
                        HypvwFixedcapitalinvst.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwFixedcapitalinvst.Visible = true;
                        LnkDfixedcapitalinvst.Visible = true;
                        LblFixedcapitalinvst.Visible = true;
                        FluFixedcapitalinvst.Enabled = false;
                    }
                    else if (vchDocId == "D293")
                    {
                        HdnCatagoryfalpriority_Name.Value = vchFileName;
                        HypvwCatagoryfalpriority.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwCatagoryfalpriority.Visible = true;
                        LnkDcatagoryfalpriority.Visible = true;
                        LblCatagoryfalpriority.Visible = true;
                        FluCatagoryfalpriority.Enabled = false;
                    }

                    else if (vchDocId == "D294")
                    {
                        HdnPowerattpost_Name.Value = vchFileName;
                        HypvwPowerattpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwPowerattpost.Visible = true;
                        LnkDPowerattpost.Visible = true;
                        LblPowerattpost.Visible = true;
                        FluPowerattPost.Enabled = false;
                    }
                    else if (vchDocId == "D295")
                    {
                        HdnPporthrustcertificate_Name.Value = vchFileName;
                        HypvwPporthrustcertificate.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwPporthrustcertificate.Visible = true;
                        LnkDpporthrustcertificate.Visible = true;
                        LblPporthrustcertificate.Visible = true;
                        FluPporthrustcertificate.Enabled = false;
                    }
                    else if (vchDocId == "D296")
                    {
                        HdnCertofregpost_Name.Value = vchFileName;
                        HypvwCertofregpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwCertofregpost.Visible = true;
                        LnkDcertofregpost.Visible = true;
                        LblCertofregpost.Visible = true;
                        FluCertofregpost.Enabled = false;
                    }
                    else if (vchDocId == "D297")
                    {
                        HdnAppDPRpost_Name.Value = vchFileName;
                        HypvwAppDPRpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwAppDPRpost.Visible = true;
                        LnkDAppDPRpost.Visible = true;
                        LblAppDPRpost.Visible = true;
                        FluAppDPRpost.Enabled = false;
                    }
                    else if (vchDocId == "D298")
                    {
                        HdnPCorEINPost_Name.Value = vchFileName;
                        HypvwPCorEINPost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwPCorEINPost.Visible = true;
                        LnkDPCorEINPost.Visible = true;
                        LblPCorEINPost.Visible = true;
                        FluPCorEINPost.Enabled = false;
                    }
                    else if (vchDocId == "D299")
                    {
                        HdnLoansancorFIappliedpost_Name.Value = vchFileName;
                        HypvwLoansancorFIappliedpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwLoansancorFIappliedpost.Visible = true;
                        LnkDloansancorFIappliedpost.Visible = true;
                        LblLoansancorFIappliedpost.Visible = true;
                        FluLoansancorFIappliedpost.Enabled = false;
                    }
                    else if (vchDocId == "D300")
                    {
                        HdnCapitalInvstPost_Name.Value = vchFileName;
                        HypvwCapitalInvstPost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwCapitalInvstPost.Visible = true;
                        LnkDCapitalInvstPost.Visible = true;
                        LblCapitalInvstPost.Visible = true;
                        FluCapitalInvstPost.Enabled = false;
                    }
                    else if (vchDocId == "D301")
                    {
                        HdnInvplantmachinaryPost_Name.Value = vchFileName;
                        HypvwInvplantmachinaryPost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwInvplantmachinaryPost.Visible = true;
                        LnkDInvplantmachinaryPost.Visible = true;
                        LblInvplantmachinaryPost.Visible = true;
                        FluInvplantmachinaryPost.Enabled = false;

                    }
                    else if (vchDocId == "D302")
                    {
                        HdnPlantmachinerypost_Name.Value = vchFileName;
                        HypvwPlantmachinerypost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwPlantmachinerypost.Visible = true;
                        LnkDPlantmachinerypost.Visible = true;
                        LblPlantmachinerypost.Visible = true;
                        FluPlantmachinerypost.Enabled = false;

                    }
                    else if (vchDocId == "D303")
                    {
                        HdnProductionormanufactpost_Name.Value = vchFileName;
                        HypvwProductionormanufactpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwProductionormanufactpost.Visible = true;
                        LnkDproductionormanufactpost.Visible = true;
                        LblProductionormanufactpost.Visible = true;
                        FluProductionormanufactpost.Enabled = false;

                    }
                    else if (vchDocId == "D304")
                    {
                        HdnCatagoryfalprioritypost_Name.Value = vchFileName;
                        HypvwCatagoryfalprioritypost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwCatagoryfalprioritypost.Visible = true;
                        LnkDcatagoryfalprioritypost.Visible = true;
                        LblCatagoryfalprioritypost.Visible = true;
                        FluCatagoryfalprioritypost.Enabled = false;

                    }
                    else if (vchDocId == "D305")
                    {
                        HdnClearancefromPCB_Name.Value = vchFileName;
                        HypvwClearancefromPCB.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwClearancefromPCB.Visible = true;
                        LnkDclearancefromPCB.Visible = true;
                        LblClearancefromPCB.Visible = true;
                        FluClearancefromPCB.Enabled = false;

                    }
                    else if (vchDocId == "D306")
                    {
                        HdnMigratedindustunitpost_Name.Value = vchFileName;
                        HypvwMigratedindustunitpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwMigratedindustunitpost.Visible = true;
                        LnkDmigratedindustunitpost.Visible = true;
                        LblMigratedindustunitpost.Visible = true;
                        FluMigratedindustunitpost.Enabled = false;

                    }
                    else if (vchDocId == "D307")
                    {
                        HdnProductionforMSMEPost_Name.Value = vchFileName;
                        HypvwProductionforMSMEPost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwProductionforMSMEPost.Visible = true;
                        LnkDproductionforMSMEPost.Visible = true;
                        LblProductionforMSMEPost.Visible = true;
                        FluProductionforMSMEPost.Enabled = false;
                    }
                    else if (vchDocId == "D308")
                    {
                        HdnEmpoweredcommitpost_Name.Value = vchFileName;
                        HypvwEmpoweredcommitpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwEmpoweredcommitpost.Visible = true;
                        LnkDEmpoweredcommitpost.Visible = true;
                        LblEmpoweredcommitpost.Visible = true;
                        FluEmpoweredcommitpost.Enabled = false;
                    }
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    private void FillUnitMeasurment()
    {

        try
        {
            string action = "A";
            CommonDataLayer objDataUnit = new CommonDataLayer();

            DrpDwn_Unit_Before.DataTextField = "vchName";
            DrpDwn_Unit_Before.DataValueField = "slno";
            DrpDwn_Unit_Before.DataSource = objDataUnit.FillUnitType(action);
            DrpDwn_Unit_Before.DataBind();
            DrpDwn_Unit_Before.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }

    }

    private void FillOrgType()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();

        try
        {
            objEntity.Action = "OT";
            objBAL.BindDropdown(DrpDwn_Org_Type, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }       
    }

    private void FillSalutation()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "R";
            objBAL.BindDropdown(DrpDwn_Gender_Partner, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }        
    }

    private void FillUnitCategory()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "B";
            objBAL.BindDropdown(DrpDwn_Unit_Cat, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }


    private void FillData()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity = new Basic_Unit_Details_Entity();       
        try
        {
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            DataSet ds = objBAL.Basic_Unit_Details_V(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                /*----------------------------------------------------------------------------*/
                /// Common Information 

                string strDataSource = ds.Tables[0].Rows[0]["vch_Data_Source"].ToString();
                string strPcStatus = ds.Tables[0].Rows[0]["vch_PC_Status"].ToString();
                string strIsExistBefore = ds.Tables[0].Rows[0]["vch_Is_Before_Exist"].ToString();
                string strIsExistAfter = ds.Tables[0].Rows[0]["vch_Is_After_Exist"].ToString();
                string strIndustryCode = ds.Tables[0].Rows[0]["vch_Industry_Code"].ToString();
                string strProposalNo = ds.Tables[0].Rows[0]["vch_Proposal_No"].ToString();
                string strProjectType = ds.Tables[0].Rows[0]["int_Project_Type"].ToString();
                string strNewPcFound = ds.Tables[0].Rows[0]["vch_New_PC_Found"].ToString();

                /*----------------------------------------------------------------------------*/
                /// If new PC found then assign strDataSource=PC
                /// Only when data present in basic table and a new PC found 
                /*----------------------------------------------------------------------------*/
                if (strDataSource == "BASIC" && strNewPcFound == "Y")
                {
                        strDataSource = "PC";
                }
                /*----------------------------------------------------------------------------*/
                /// Value Assigned to HiddenField for use in Validation
                Hid_Is_Exist_Before.Value = strIsExistBefore;
                Hid_Is_Exist_After.Value = strIsExistAfter;
                Hid_Data_Source.Value = strDataSource;
                Hid_PC_Status.Value = strPcStatus;
                Hid_Project_Type.Value = strProjectType;
                /*----------------------------------------------------------------------------*/

                if (strDataSource == "BASIC")
                {
                    Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchEnterpriseName"].ToString();
                }

                else if (strDataSource == "PC")
                {
                    if (strIsExistBefore == "Y")
                    {
                        Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchCompName"].ToString();
                    }

                    if (strIsExistAfter == "Y")
                    {
                        Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchCompName"].ToString();
                    }
                }
                else if (strDataSource == "PEAL" || strDataSource == "REGD")
                {
                    Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchNameOfUnit"].ToString();
                }
                /*---------------------------------------------------------------*/
                /// Session Assigned Here

                Session["UnitCode"] = strIndustryCode;
                Session["ProposalNo"] = strProposalNo;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }        
    }


    private bool IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf", "application/x-zip-compressed" };
        string[] allowedExtension = { ".pdf", ".zip" };
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        imageTypes.AddRange(allowedImageTyps);
        imageExtension.AddRange(allowedExtension);
        string strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
        string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
        int count = FileUpload1.FileName.Count(f => f == '.');

        System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
        
        if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void UploadDocument(FileUpload FileUpload_Document, HiddenField Hid_File_Name, string strFileName, HyperLink Hyp_Document, Label Lbl_Upload_Msg, LinkButton LnkBtn_Delete_Doc, string strFoldername)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFoldername));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (FileUpload_Document.HasFile)
            {
                string filename = string.Empty;
                if (Path.GetExtension(FileUpload_Document.FileName).ToLower() != ".pdf" && Path.GetExtension(FileUpload_Document.FileName).ToLower() != ".zip")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload either .pdf or .zip file !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }

                if (!IsFileValid(FileUpload_Document))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid file type (or) File name may contain dots !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }

                int fileSize = FileUpload_Document.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>File size is too large. Maximum file size permitted is 4 MB !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(FileUpload_Document.FileName);
                }

                FileUpload_Document.SaveAs(strMainFolderPath + filename);
                Hid_File_Name.Value = filename;
                Hyp_Document.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFoldername, filename);
                Hyp_Document.Visible = true;
                LnkBtn_Delete_Doc.Visible = true;
                Lbl_Upload_Msg.Visible = true;
                FileUpload_Document.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }


    private void UpdFileRemove(HiddenField Hid_File_Name, LinkButton LnkBtn_Upload_Doc, LinkButton LnkBtn_Delete_Doc, HyperLink Hyp_Document, Label Lbl_Upload_Msg, FileUpload FileUpload_Document, string strFolername)
    {
        try
        {
            string filename = Hid_File_Name.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolername, filename);          
            Server.MapPath(path);
            Hid_File_Name.Value = "";
            LnkBtn_Delete_Doc.Visible = false;
            LnkBtn_Upload_Doc.Visible = true;
            Hyp_Document.Visible = false;
            Lbl_Upload_Msg.Visible = false;
            FileUpload_Document.Enabled = true;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void DrpDwn_Unit_Before_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DrpDwn_Unit_Before.SelectedIndex > 0)
            {
                if (DrpDwn_Unit_Before.SelectedValue == "52")
                {
                    Txt_Other_Unit_Before.Visible = true;
                    Txt_Other_Unit_Before.Focus();
                }
                else
                {
                    Txt_Other_Unit_Before.Text = "";
                    Txt_Other_Unit_Before.Visible = false;
                    Txt_Value_Before.Focus();
                }
            }
            else
            {
                Txt_Other_Unit_Before.Text = "";
                Txt_Other_Unit_Before.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void DrpDwn_Org_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DrpDwn_Org_Type.SelectedValue == "15")
            {
                Lbl_Org_Name_Type.Text = "Name of Proprietor";
            }
            else if (DrpDwn_Org_Type.SelectedValue == "17")

            {
                Lbl_Org_Name_Type.Text = "Name of Managing Partner";
            }
            else if (DrpDwn_Org_Type.SelectedValue == "18")

            {
                Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
            }
            else if (DrpDwn_Org_Type.SelectedValue == "19")
            {
                Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
            }
            else if (DrpDwn_Org_Type.SelectedValue == "20")
            {
                Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void LnkBtn_Add_Item_Before_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("intUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));
            table.Columns.Add("decValue", typeof(string));
            table.Rows.Add(Txt_Product_Name_Before.Text, Txt_Quantity_Before.Text, DrpDwn_Unit_Before.SelectedItem.Text, DrpDwn_Unit_Before.SelectedValue, Txt_Other_Unit_Before.Text == "" ? null : Txt_Other_Unit_Before.Text, Txt_Value_Before.Text);
            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                Label Lbl_Product_Name_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                Label Lbl_Quantity_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                Label Lbl_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
                Label Lbl_Other_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                Label Lbl_Value_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                HiddenField Hid_Unit_Before = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");
                table.Rows.Add(Lbl_Product_Name_Before.Text, Lbl_Quantity_Before.Text, Lbl_Unit_Before.Text, Hid_Unit_Before.Value, Lbl_Other_Unit_Before.Text, Lbl_Value_Before.Text);
            }

            Grd_Production_Before.DataSource = table;
            Grd_Production_Before.DataBind();

            Txt_Product_Name_Before.Text = "";
            Txt_Quantity_Before.Text = "";
            DrpDwn_Unit_Before.SelectedIndex = 0;
            Txt_Value_Before.Text = "";
            Txt_Other_Unit_Before.Text = "";
            Txt_Other_Unit_Before.Visible = false;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void ImgBtn_Delete_Before_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("intUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));
            table.Columns.Add("decValue", typeof(string));

            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_Product_Name_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                    Label Lbl_Quantity_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                    Label Lbl_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
                    Label Lbl_Other_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                    Label Lbl_Value_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                    HiddenField Hid_Unit_Before = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");
                    table.Rows.Add(Lbl_Product_Name_Before.Text, Lbl_Quantity_Before.Text, Lbl_Unit_Before.Text, Hid_Unit_Before.Value, Lbl_Other_Unit_Before.Text, Lbl_Value_Before.Text);
                }
            }

            Grd_Production_Before.DataSource = table;
            Grd_Production_Before.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }       
    }

    protected void LnkBtn_TL_Add_More_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            table.Rows.Add(Txt_TL_Financial_Institution.Text, Txt_TL_State.Text, Txt_TL_City.Text, Txt_TL_Amount.Text, Txt_TL_Sanction_Date.Text, Txt_TL_Avail_Amount.Text, Txt_TL_Availed_Date.Text);
            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");
                table.Rows.Add(Lbl_TL_Financial_Inst.Text, Lbl_TL_State.Text, Lbl_TL_City.Text, Lbl_TL_Amount.Text, Lbl_TL_Sanction_Date.Text, Lbl_TL_Avail_Amt.Text, Lbl_TL_Avail_Date.Text);
            }

            Grd_TL.DataSource = table;
            Grd_TL.DataBind();

            Txt_TL_Financial_Institution.Text = "";
            Txt_TL_State.Text = "";
            Txt_TL_City.Text = "";
            Txt_TL_Amount.Text = "";
            Txt_TL_Sanction_Date.Text = "";
            Txt_TL_Avail_Amount.Text = "";
            Txt_TL_Availed_Date.Text = "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }      
    }

    protected void ImgBtn_Delete_TL_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                    Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                    Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                    Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                    Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                    Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                    Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");
                    table.Rows.Add(Lbl_TL_Financial_Inst.Text, Lbl_TL_State.Text, Lbl_TL_City.Text, Lbl_TL_Amount.Text, Lbl_TL_Sanction_Date.Text, Lbl_TL_Avail_Amt.Text, Lbl_TL_Avail_Date.Text);
                }
            }

            Grd_TL.DataSource = table;
            Grd_TL.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }       
    }

    protected void LnkBtn_WC_Add_More_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            table.Rows.Add(Txt_WC_Financial_Institution.Text, Txt_WC_State.Text, Txt_WC_City.Text, Txt_WC_Amount.Text, Txt_WC_Sanction_Date.Text, Txt_WC_Avail_Amount.Text, Txt_WC_Availed_Date.Text);
            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");
                table.Rows.Add(Lbl_WC_Financial_Inst.Text, Lbl_WC_State.Text, Lbl_WC_City.Text, Lbl_WC_Amount.Text, Lbl_WC_Sanction_Date.Text, Lbl_WC_Avail_Amt.Text, Lbl_WC_Avail_Date.Text);
            }

            Grd_WC.DataSource = table;
            Grd_WC.DataBind();

            Txt_WC_Financial_Institution.Text = "";
            Txt_WC_State.Text = "";
            Txt_WC_City.Text = "";
            Txt_WC_Amount.Text = "";
            Txt_WC_Sanction_Date.Text = "";
            Txt_WC_Avail_Amount.Text = "";
            Txt_WC_Availed_Date.Text = "";

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }       
    }

    protected void ImgBtn_Delete_WC_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                    Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                    Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                    Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                    Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                    Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                    Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");
                    table.Rows.Add(Lbl_WC_Financial_Inst.Text, Lbl_WC_State.Text, Lbl_WC_City.Text, Lbl_WC_Amount.Text, Lbl_WC_Sanction_Date.Text, Lbl_WC_Avail_Amt.Text, Lbl_WC_Avail_Date.Text);
                }
            }

            Grd_WC.DataSource = table;
            Grd_WC.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }        
    }

    protected void BtnApply_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();

        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity1 = new Basic_Unit_Details_Entity();

        try
        {
            #region Production Item Before

            /*---------------------------------------------------------------------*/
            /// Add Production Item Before
            /*---------------------------------------------------------------------*/

            List<BasicProductionItemBefore> listProdBefore = new List<BasicProductionItemBefore>();

            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                BasicProductionItemBefore objItem = new BasicProductionItemBefore();

                Label lblProductionName = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                Label lblQuantity = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                Label lblOtherUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                Label lblValue = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                HiddenField hdnUnit = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");

                objItem.vchProductName = lblProductionName.Text;
                objItem.decQuantity = Convert.ToDecimal(lblQuantity.Text == "" ? "0" : lblQuantity.Text);
                if (hdnUnit.Value != "")
                {
                    objItem.intUnitType = Convert.ToInt32(hdnUnit.Value);
                }
                objItem.vchOtherUnit = lblOtherUnit.Text == "" ? null : lblOtherUnit.Text;
                objItem.decValue = Convert.ToDecimal(lblValue.Text == "" ? "0" : lblValue.Text);
                objItem.chItemFor = "B"; //// Before

                listProdBefore.Add(objItem);
            }

            objEntity1.ProductionItem_BE = listProdBefore;

            #endregion

            #region Term Loan
            /*---------------------------------------------------------------------*/
            /// Add Term Loan
            /*---------------------------------------------------------------------*/
            List<BasicTermLoan> listTL = new List<BasicTermLoan>();

            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                BasicTermLoan objItem = new BasicTermLoan();

                Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");

                objItem.vchNameOfFinancialInst = Lbl_TL_Financial_Inst.Text;
                objItem.vchState = Lbl_TL_State.Text;
                objItem.vchCity = Lbl_TL_City.Text;
                objItem.decLoanAmt = Convert.ToDecimal(Lbl_TL_Amount.Text == "" ? "0" : Lbl_TL_Amount.Text);
                objItem.dtmSanctionDate = Lbl_TL_Sanction_Date.Text == "" ? null : Lbl_TL_Sanction_Date.Text;
                objItem.decAvailedAmt = Convert.ToDecimal(Lbl_TL_Avail_Amt.Text == "" ? "0" : Lbl_TL_Avail_Amt.Text);
                objItem.dtmAvailedDate = Lbl_TL_Avail_Date.Text == "" ? null : Lbl_TL_Avail_Date.Text;

                listTL.Add(objItem);
            }

            objEntity1.TermLoan = listTL;
            #endregion

            #region Working Capital Loan

            /*---------------------------------------------------------------------*/
            /// Add Working Capital Loan
            /*---------------------------------------------------------------------*/

            List<BasicWorkingCapitalLoan> listWC = new List<BasicWorkingCapitalLoan>();

            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                BasicWorkingCapitalLoan objItem = new BasicWorkingCapitalLoan();

                Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");

                objItem.vchNameOfFinancialInst = Lbl_WC_Financial_Inst.Text;
                objItem.vchState = Lbl_WC_State.Text;
                objItem.vchCity = Lbl_WC_City.Text;
                objItem.decLoanAmt = Convert.ToDecimal(Lbl_WC_Amount.Text == "" ? "0" : Lbl_WC_Amount.Text);
                objItem.dtmSanctionDate = Lbl_WC_Sanction_Date.Text == "" ? null : Lbl_WC_Sanction_Date.Text;
                objItem.decAvailedAmt = Convert.ToDecimal(Lbl_WC_Avail_Amt.Text == "" ? "0" : Lbl_WC_Avail_Amt.Text);
                objItem.dtmAvailedDate = Lbl_WC_Avail_Date.Text == "" ? null : Lbl_WC_Avail_Date.Text;

                listWC.Add(objItem);
            }

            objEntity1.WorkingCapitalLoan = listWC;
            #endregion

            #region IndustrialUnitDetailsSection

            /*---------------------------------------------------------------------*/
            ///Industrial Unit Details Section
            /*---------------------------------------------------------------------*/
            objEntity1.strEnterpriseName = Txt_EnterPrise_Name.Text;
            objEntity1.intOrganisationType = Convert.ToInt32(DrpDwn_Org_Type.SelectedValue);
            objEntity1.strIndustryAddress = Txt_Industry_Address.Text;
            objEntity1.intUnitCat = Convert.ToInt32(DrpDwn_Unit_Cat.SelectedValue);

            objEntity1.strRegisteredOfcAddress = Txt_Regd_Office_Address.Text;
            objEntity1.strManagingPartnerGender = DrpDwn_Gender_Partner.SelectedValue;
            objEntity1.strManagingPartnerName = Txt_Partner_Name.Text;

            objEntity1.strEINNO = Txt_EIN_IL_NO.Text == "" ? null : Txt_EIN_IL_NO.Text;
            objEntity1.dtmEIN = Txt_EIN_IL_Date.Text == "" ? null : Txt_EIN_IL_Date.Text;
            objEntity1.strPhoneNo = Txt_Phone_no.Text;
            objEntity1.strEmail = Txt_Email.Text;

            /*-------------------------Industrial Unit Details End---------------------------------*/
            #endregion

            #region ProductionandEmploymentDetails Section

            /*----------------------------------------------------------*/
            ///Production and Employment Details Section
            /*----------------------------------------------------------*/
            objEntity1.dtmFFCIDateBefore = Txt_Proposed_Date.Text;
            objEntity1.dtmProdCommBefore = Txt_Commence_production.Text;
            if (Rad_production.SelectedValue == "1")
            {
                objEntity1.strUAMNo = Txt_PC_EMI_No.Text;
                objEntity1.dtmUAMdate = Txt_PC_EMI_Date.Text;
            }
            else if (Rad_production.SelectedValue == "2")
            {
                objEntity1.strUAMNo = Txt_Uam_No.Text;
                objEntity1.dtmUAMdate = Txt_Uam_Date.Text;
            }

            objEntity1.intEIMorUAMtype = Convert.ToInt32(Rad_production.SelectedValue);

            #endregion

            #region InvestmentDetailsSection

            /*----------------------------------------------------------*/
            /// Investment Details Section
            /*----------------------------------------------------------*/
            objEntity1.intDirectEmpAfter = Convert.ToInt32(Txt_total_emp_Number.Text == "" ? "0" : Txt_total_emp_Number.Text);
            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text);
            decimal decLandBefore = 0;
            decimal decBuildingBefore = 0;
            decimal decPlantMachBefore = 0;
            decimal decOtherFixedAssetBefore = 0;
            decimal decElectricalInstBefore = 0;
            decimal decLoadUnloadBefore = 0;
            decimal decMarginMoneyForworkingBefore = 0;

            if (Txt_Land_Details_before.Text != "")
            {
                decLandBefore = Convert.ToDecimal(Txt_Land_Details_before.Text);
            }
            if (Txt_Building_Before.Text != "")
            {
                decBuildingBefore = Convert.ToDecimal(Txt_Building_Before.Text);
            }
            if (Txt_Plant_Mach_Before.Text != "")
            {
                decPlantMachBefore = Convert.ToDecimal(Txt_Plant_Mach_Before.Text);
            }
            if (Txt_Other_Fixed_Asset_Before.Text != "")
            {
                decOtherFixedAssetBefore = Convert.ToDecimal(Txt_Other_Fixed_Asset_Before.Text);
            }
            if (Txt_Electrical_inst_Before.Text != "")
            {
                decElectricalInstBefore = Convert.ToDecimal(Txt_Electrical_inst_Before.Text);
            }
            if (Txt_Loadig_Before.Text != "")
            {
                decLoadUnloadBefore = Convert.ToDecimal(Txt_Loadig_Before.Text);
            }
            if (Txt_Margine_money.Text != "")
            {
                decMarginMoneyForworkingBefore = Convert.ToDecimal(Txt_Margine_money.Text);
            }

            decimal decTotalCapitalBefore = decLandBefore + decBuildingBefore + decPlantMachBefore + decOtherFixedAssetBefore + decElectricalInstBefore + decLoadUnloadBefore + decMarginMoneyForworkingBefore;

            objEntity1.decLandAmtBefore = decLandBefore;
            objEntity1.decBuildingAmtBefore = decBuildingBefore;
            objEntity1.decPlantMachAmtBefore = decPlantMachBefore;
            objEntity1.decOtheFixedAssetAmtBefore = decOtherFixedAssetBefore;
            objEntity1.decTotalAmtBefore = decTotalCapitalBefore;
            objEntity1.decElectricalInstAmtBefore = decElectricalInstBefore;
            objEntity1.decLoadUnloadAmtBefore = decLoadUnloadBefore;
            objEntity1.decMarginMoneyForworkingAmtBefore = decMarginMoneyForworkingBefore;

            /*---------------------------------------------------------------------*/
            /// Investment Details (After)
            /*---------------------------------------------------------------------*/
            decimal decLandAfter = 0;
            decimal decBuildingAfter = 0;
            decimal decPlantMachAfter = 0;
            decimal decOtherFixedAssetAfter = 0;
            decimal decElectricalInstAfter = 0;
            decimal decLoadUnloadAfter = 0;
            decimal decMarginMoneyForworkingAfter = 0;

            if (Txt_Land_After.Text != "")
            {
                decLandAfter = Convert.ToDecimal(Txt_Land_After.Text);
            }
            if (Txt_Building_After.Text != "")
            {
                decBuildingAfter = Convert.ToDecimal(Txt_Building_After.Text);
            }
            if (Txt_Plant_Mach_After.Text != "")
            {
                decPlantMachAfter = Convert.ToDecimal(Txt_Plant_Mach_After.Text);
            }
            if (Txt_Other_Fixed_Asset_After.Text != "")
            {
                decOtherFixedAssetAfter = Convert.ToDecimal(Txt_Other_Fixed_Asset_After.Text);
            }
            if (Txt_Electrical_inst_Before.Text != "")
            {
                decElectricalInstAfter = Convert.ToDecimal(Txt_Electrical_inst_Before.Text);
            }
            if (Txt_Loadig_After.Text != "")
            {
                decLoadUnloadAfter = Convert.ToDecimal(Txt_Loadig_After.Text);
            }
            if (Txt_Margine_money_After.Text != "")
            {
                decMarginMoneyForworkingAfter = Convert.ToDecimal(Txt_Margine_money_After.Text);
            }
            decimal decTotalCapitalAfter = decLandAfter + decBuildingAfter + decPlantMachAfter + decOtherFixedAssetAfter + decElectricalInstAfter + decLoadUnloadAfter + decMarginMoneyForworkingAfter;

            objEntity1.decLandAmtAfter = decLandAfter;
            objEntity1.decBuildingAmtAfter = decBuildingAfter;
            objEntity1.decPlantMachAmtAfter = decPlantMachAfter;
            objEntity1.decOtheFixedAssetAmtAfter = decOtherFixedAssetAfter;
            objEntity1.decTotalAmtAfter = decTotalCapitalAfter;
            objEntity1.decElectricalInstAmtAfter = decElectricalInstAfter;
            objEntity1.decLoadUnloadAmtAfter = decLoadUnloadAfter;
            objEntity1.decMarginMoneyForworkingAmtAfter = decMarginMoneyForworkingAfter;

            /*----------------------------Investment Details Section End------------------------------*/
            #endregion

            #region MeansofFinanceSection

            /*---------------------------------------------------------------------*/
            /// Means of Finance Section
            /*---------------------------------------------------------------------*/

            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text == "" ? "0" : Txt_Equity_Amt.Text);
            objEntity1.intProjectClearance = Convert.ToInt32(Rad_Clearnce_PCB.SelectedValue);
            objEntity1.intProvisnalPriorityThrustStatus = Convert.ToInt32(Rad_PP_thrust_Status.SelectedValue);
            objEntity1.intIPRinctiveAvel = Convert.ToInt32(Rad_IPR_Incentive_avail.SelectedValue);
            objEntity1.strClearnceswm = Txt_Swm_approve.Text;

            #endregion

            #region OtherDocumentSection

            /*---------------------------------------------------------------------*/
            /// Other Document Section
            /*---------------------------------------------------------------------*/         
            if (HdnPowerattpre_Name.Value != "")
            {
                objEntity1.strPworofAttorneyPreDocCode = HdnPowerattpre_Code.Value;
                objEntity1.strPworofAttorneyPre = HdnPowerattpre_Name.Value;
            }
            else
            {
                objEntity1.strPworofAttorneyPreDocCode = null;
                objEntity1.strPworofAttorneyPre = null;
            }

            if (HdnCertofreg_Name.Value != "")
            {
                objEntity1.strCertificateofregistrationDocCodepre = HdnCertofreg_Code.Value;
                objEntity1.strCertificateofregistrationpre = HdnCertofreg_Name.Value;
            }
            else
            {
                objEntity1.strCertificateofregistrationDocCodepre = null;
                objEntity1.strCertificateofregistrationpre = null;
            }

            if (HdnAppDPR_Name.Value != "")
            {
                objEntity1.strApproveDPRDocCodePre = HdnAppDPR_Code.Value;
                objEntity1.strApproveDPRPre = HdnAppDPR_Name.Value;
            }
            else
            {
                objEntity1.strApproveDPRDocCodePre = null;
                objEntity1.strApproveDPRPre = null;
            }

            if (HdnEIN_Name.Value != "")
            {
                objEntity1.strEINapprovalDocCodePre = HdnEIN_Code.Value;
                objEntity1.strEINapprovalPre = HdnEIN_Name.Value;
            }
            else
            {
                objEntity1.strEINapprovalDocCodePre = null;
                objEntity1.strEINapprovalPre = null;
            }

            if (HdnPlantmachinery_Name.Value != "")
            {
                objEntity1.strBalacingEquipmentDocCodePre = HdnPlantmachinery_Code.Value;
                objEntity1.strBalacingEquipmentPre = HdnPlantmachinery_Name.Value;
            }
            else
            {
                objEntity1.strBalacingEquipmentDocCodePre = null;
                objEntity1.strBalacingEquipmentPre = null;
            }

            if (HdnCapitalInvst_Name.Value != "")
            {
                objEntity1.strCapitalInvstDocCodePre = HdnCapitalInvst_Code.Value;
                objEntity1.strCapitalInvstPre = HdnCapitalInvst_Name.Value;
            }
            else
            {
                objEntity1.strCapitalInvstDocCodePre = null;
                objEntity1.strCapitalInvstPre = null;
            }

            if (HdnInvplantmachinary_Name.Value != "")
            {
                objEntity1.strInvestmentplantmachinaryDocCodePre = HdnInvplantmachinary_Code.Value;
                objEntity1.strInvestmentplantmachinaryPre = HdnInvplantmachinary_Name.Value;
            }
            else
            {
                objEntity1.strInvestmentplantmachinaryDocCodePre = null;
                objEntity1.strInvestmentplantmachinaryPre = null;
            }

            if (HdnProposedprod_Name.Value != "")
            {
                objEntity1.strProposedprodDocCodePre = HdnProposedprod_Code.Value;
                objEntity1.strProposedprodPre = HdnProposedprod_Name.Value;
            }
            else
            {
                objEntity1.strProposedprodDocCodePre = null;
                objEntity1.strProposedprodPre = null;
            }

            if (HdnPresentstageimplemnt_Name.Value != "")
            {
                objEntity1.strPresentStageImplentDocCodePre = HdnPresentstageimplemnt_Code.Value;
                objEntity1.strPresentStageImplentPre = HdnPresentstageimplemnt_Name.Value;
            }
            else
            {
                objEntity1.strPresentStageImplentDocCodePre = null;
                objEntity1.strPresentStageImplentPre = null;
            }

            if (HdnMigrantindustrial_Name.Value != "")
            {
                objEntity1.strMigrantIndustryunitDocCodePre = HdnMigrantindustrial_Code.Value;
                objEntity1.strMigrantIndustryunitPre = HdnMigrantindustrial_Name.Value;
            }
            else
            {
                objEntity1.strMigrantIndustryunitDocCodePre = null;
                objEntity1.strMigrantIndustryunitPre = null;
            }

            if (HdnFixedcapitalinvst_Name.Value != "")
            {
                objEntity1.strfixedcapitalinvstDocCodePre = HdnFixedcapitalinvst_Code.Value;
                objEntity1.strfixedcapitalinvstPre = HdnFixedcapitalinvst_Name.Value;
            }
            else
            {
                objEntity1.strfixedcapitalinvstDocCodePre = null;
                objEntity1.strfixedcapitalinvstPre = null;
            }

            if (HdnCatagoryfalpriority_Name.Value != "")
            {
                objEntity1.strPriorityorThrustsectorDocCodePre = HdnCatagoryfalpriority_Code.Value;
                objEntity1.strPriorityorThrustsectorPre = HdnCatagoryfalpriority_Name.Value;
            }
            else
            {
                objEntity1.strPriorityorThrustsectorDocCodePre = null;
                objEntity1.strPriorityorThrustsectorPre = null;
            }

            if (HdnPowerattpost_Name.Value != "")
            {
                objEntity1.strPworofAttorneyPostDocCode = HdnPowerattpost_Code.Value;
                objEntity1.strPworofAttorneyPost = HdnPowerattpost_Name.Value;
            }
            else
            {
                objEntity1.strPworofAttorneyPostDocCode = null;
                objEntity1.strPworofAttorneyPost = null;
            }

            if (HdnPporthrustcertificate_Name.Value != "")
            {
                objEntity1.strPPorThrustStatusCertPostDocCode = HdnPporthrustcertificate_Code.Value;
                objEntity1.strPPorThrustStatusCertPost = HdnPporthrustcertificate_Name.Value;
            }
            else
            {
                objEntity1.strPPorThrustStatusCertPostDocCode = null;
                objEntity1.strPPorThrustStatusCertPost = null;
            }

            if (HdnCertofregpost_Name.Value != "")
            {
                objEntity1.strCertificateofregistrationDocCodepost = HdnCertofregpost_Code.Value;
                objEntity1.strCertificateofregistrationpost = HdnCertofregpost_Name.Value;
            }
            else
            {
                objEntity1.strCertificateofregistrationDocCodepost = null;
                objEntity1.strCertificateofregistrationpost = null;
            }

            if (HdnAppDPRpost_Name.Value != "")
            {
                objEntity1.strApproveDPRDocCodePost = HdnAppDPRpost_Code.Value;
                objEntity1.strApproveDPRPost = HdnAppDPRpost_Name.Value;
            }
            else
            {
                objEntity1.strApproveDPRDocCodePost = null;
                objEntity1.strApproveDPRPost = null;
            }

            if (HdnPCorEINPost_Name.Value != "")
            {
                objEntity1.strPcDocCodePost = HdnPCorEINPost_Code.Value;
                objEntity1.strPcPost = HdnPCorEINPost_Name.Value;
            }
            else
            {
                objEntity1.strPcDocCodePost = null;
                objEntity1.strPcPost = null;
            }

            if (HdnLoansancorFIappliedpost_Name.Value != "")
            {
                objEntity1.strSanctionbankorFIDocCodePost = HdnLoansancorFIappliedpost_Code.Value;
                objEntity1.strSanctionbankorFIPost = HdnLoansancorFIappliedpost_Name.Value;
            }
            else
            {
                objEntity1.strSanctionbankorFIDocCodePost = null;
                objEntity1.strSanctionbankorFIPost = null;
            }

            if (HdnCapitalInvstPost_Name.Value != "")
            {
                objEntity1.strCapitalInvstDocCodePost = HdnCapitalInvstPost_Code.Value;
                objEntity1.strCapitalInvstPost = HdnCapitalInvstPost_Name.Value;
            }
            else
            {
                objEntity1.strCapitalInvstDocCodePost = null;
                objEntity1.strCapitalInvstPost = null;
            }

            if (HdnInvplantmachinaryPost_Name.Value != "")
            {
                objEntity1.strInvestmentplantmachinaryDocCodePost = HdnInvplantmachinaryPost_Code.Value;
                objEntity1.strInvestmentplantmachinaryPost = HdnInvplantmachinaryPost_Name.Value;
            }
            else
            {
                objEntity1.strInvestmentplantmachinaryDocCodePost = null;
                objEntity1.strInvestmentplantmachinaryPost = null;
            }

            if (HdnPlantmachinerypost_Name.Value != "")
            {
                objEntity1.strBalacingEquipmentDocCodePost = HdnPlantmachinerypost_Code.Value;
                objEntity1.strBalacingEquipmentPost = HdnPlantmachinerypost_Name.Value;
            }
            else
            {
                objEntity1.strBalacingEquipmentDocCodePost = null;
                objEntity1.strBalacingEquipmentPost = null;
            }

            if (HdnProductionormanufactpost_Name.Value != "")
            {
                objEntity1.strServiceProvideDocCodePost = HdnProductionormanufactpost_Code.Value;
                objEntity1.strServiceProvidePost = HdnProductionormanufactpost_Name.Value;
            }
            else
            {
                objEntity1.strServiceProvideDocCodePost = null;
                objEntity1.strServiceProvidePost = null;
            }

            if (HdnCatagoryfalprioritypost_Name.Value != "")
            {
                objEntity1.strPriorityorThrustsectorDocCodePost = HdnCatagoryfalprioritypost_Code.Value;
                objEntity1.strPriorityorThrustsectorPost = HdnCatagoryfalprioritypost_Name.Value;
            }
            else
            {
                objEntity1.strPriorityorThrustsectorDocCodePost = null;
                objEntity1.strPriorityorThrustsectorPost = null;
            }

            if (HdnClearancefromPCB_Name.Value != "")
            {
                objEntity1.strClearancefromPCBDocCodePost = HdnClearancefromPCB_Code.Value;
                objEntity1.strClearancefromPCBPost = HdnClearancefromPCB_Name.Value;
            }
            else
            {
                objEntity1.strClearancefromPCBDocCodePost = null;
                objEntity1.strClearancefromPCBPost = null;
            }

            if (HdnMigratedindustunitpost_Name.Value != "")
            {
                objEntity1.strMigrantIndustryunitDocCodePost = HdnMigratedindustunitpost_Code.Value;
                objEntity1.strMigrantIndustryunitPost = HdnMigratedindustunitpost_Name.Value;
            }
            else
            {
                objEntity1.strMigrantIndustryunitDocCodePost = null;
                objEntity1.strMigrantIndustryunitPost = null;
            }

            if (HdnProductionforMSMEPost_Name.Value != "")
            {
                objEntity1.strfixedcapitalinvstDocCodePost = HdnProductionforMSMEPost_Code.Value;
                objEntity1.strfixedcapitalinvstPost = HdnProductionforMSMEPost_Name.Value;
            }
            else
            {
                objEntity1.strfixedcapitalinvstDocCodePost = null;
                objEntity1.strfixedcapitalinvstPost = null;
            }

            if (HdnEmpoweredcommitpost_Name.Value != "")
            {
                objEntity1.strEmpoweredCommitteeDocCodePost = HdnEmpoweredcommitpost_Code.Value;
                objEntity1.strEmpoweredCommitteePost = HdnEmpoweredcommitpost_Name.Value;
            }
            else
            {
                objEntity1.strEmpoweredCommitteeDocCodePost = null;
                objEntity1.strEmpoweredCommitteePost = null;
            }
            /*-------------------------Other Document End---------------------------------*/


            #endregion

            ///This method is called to assign  Session value
            FillData();

            objEntity1.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objEntity1.strPcNoAfter = Session["ProposalNo"].ToString();
            objEntity1.strIndustryCode = Session["UnitCode"].ToString();

            if (Request.QueryString["key"] != null)
            {
                objEntity1.strInctFlow = Request.QueryString["key"].ToString();
            }
            else
            {
                objEntity1.strInctFlow = Request.QueryString["IncentiveNo"].ToString();
            }

            /*---------------------------------------------------------------------*/
            /// Data Insert and Update
            /// 1-Insert
            /// 2-Update
            /*---------------------------------------------------------------------*/

            string strReturnStatus = objBAL.Thrust_Priority_AED(objEntity1);
            if (strReturnStatus == "1" || strReturnStatus == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = './incentiveoffered.aspx';}); </script>", false);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something Went wrong.</strong>');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void Rad_production_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rad_production.SelectedValue == "1")
            {
                Div_EIM_No.Visible = true;
                Div_Eim_date.Visible = true;
                Div_UAM_No.Visible = false;
                Div_UAM_date.Visible = false;
                Txt_PC_EMI_No.Text = "";
                Txt_PC_EMI_Date.Text = "";
            }
            else if (Rad_production.SelectedValue == "2")
            {
                Div_UAM_No.Visible = true;
                Div_UAM_date.Visible = true;
                Div_EIM_No.Visible = false;
                Div_Eim_date.Visible = false;
                Txt_Uam_No.Text = "";
                Txt_Uam_Date.Text = "";
            }
            else if (Rad_production.SelectedValue == "3")
            {
                Div_UAM_No.Visible = false;
                Div_UAM_date.Visible = false;
                Div_EIM_No.Visible = false;
                Div_Eim_date.Visible = false;
                Txt_PC_EMI_No.Text = "";
                Txt_PC_EMI_Date.Text = "";
                Txt_Uam_No.Text = "";
                Txt_Uam_Date.Text = "";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void LnkBtn_Add_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkUPowerattpre.ID))
            {
                if (FluPowerattpre.HasFile)
                {

                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PoweofAttorneyPre";
                    UploadDocument(FluPowerattpre, HdnPowerattpre_Name, strFileName, HypPowerattpre, LblPowerattpre, LnkDPowerattpre, "InctBasicDoc");
                }

            }
            else if (string.Equals(lnk.ID, LnkUCertofreg.ID))
            {
                if (FluCertofreg.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_MEMORANDUMPRE";
                    UploadDocument(FluCertofreg, HdnCertofreg_Name, strFileName, HypvwCertofreg, LblCertofreg, LnkDCertofreg, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUAppDPR.ID))
            {
                if (FlAppDPR.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_DPRPRE";
                    UploadDocument(FlAppDPR, HdnAppDPR_Name, strFileName, HypVwAppDPR, LblAppDPR, LnkDAppDPR, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUEIN.ID))
            {
                if (FluEIN.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_EINPRE";
                    UploadDocument(FluEIN, HdnEIN_Name, strFileName, HypVwEIN, LblEIN, LnkDEIN, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUPlantmachinery.ID))
            {
                if (FluPlantmachinery.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_Plant&MachineryPre";
                    UploadDocument(FluPlantmachinery, HdnPlantmachinery_Name, strFileName, HyVwPlantmachinery, LblPlantmachinery, LnkDPlantmachinery, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUCapitalInvst.ID))
            {
                if (FluCapitalInvst.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_CapitalInvestmentPre";
                    UploadDocument(FluCapitalInvst, HdnCapitalInvst_Name, strFileName, HypVwCapitalInvst, LblCapitalInvst, LnkDCapitalInvst, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUInvplantmachinary.ID))
            {
                if (FluInvplantmachinary.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_IvtPlant&machineryPre";
                    UploadDocument(FluInvplantmachinary, HdnInvplantmachinary_Name, strFileName, HypVwInvplantmachinary, LblInvplantmachinary, LnkDInvplantmachinary, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUproposedprod.ID))
            {
                if (FluProposedprod.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ProposedprodPre";
                    UploadDocument(FluProposedprod, HdnProposedprod_Name, strFileName, HypVwProposedprod, LblProposedprod, LnkDproposedprod, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUpresentstageimplemnt.ID))
            {
                if (FluPresentstageimplemnt.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PresentStageImplementationPre";
                    UploadDocument(FluPresentstageimplemnt, HdnPresentstageimplemnt_Name, strFileName, HypVwpresentstageimplemnt, LblPresentstageimplemnt, LnkDpresentstageimplemnt, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUmigrantindustrial.ID))
            {
                if (FluMigrantindustrial.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_MigrantindustrialPre";
                    UploadDocument(FluMigrantindustrial, HdnMigrantindustrial_Name, strFileName, HypvwMigrantindustrial, LblMigrantindustrial, LnkDmigrantindustrial, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUfixedcapitalinvst.ID))
            {
                if (FluFixedcapitalinvst.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_FixedcapitalinvstPre";
                    UploadDocument(FluFixedcapitalinvst, HdnFixedcapitalinvst_Name, strFileName, HypvwFixedcapitalinvst, LblFixedcapitalinvst, LnkDfixedcapitalinvst, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUcatagoryfalpriority.ID))
            {
                if (FluCatagoryfalpriority.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_CatagoryfalpriorityPre";
                    UploadDocument(FluCatagoryfalpriority, HdnCatagoryfalpriority_Name, strFileName, HypvwCatagoryfalpriority, LblCatagoryfalpriority, LnkDcatagoryfalpriority, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUPowerattpost.ID))
            {
                if (FluPowerattPost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PoweofAttorneypost";
                    UploadDocument(FluPowerattPost, HdnPowerattpost_Name, strFileName, HypvwPowerattpost, LblPowerattpost, LnkDPowerattpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUpporthrustcertificate.ID))
            {
                if (FluPporthrustcertificate.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "PPorthrustcertificatePost";
                    UploadDocument(FluPporthrustcertificate, HdnPporthrustcertificate_Name, strFileName, HypvwPporthrustcertificate, LblPporthrustcertificate, LnkDpporthrustcertificate, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUcertofregpost.ID))
            {
                if (FluCertofregpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Certofregpost";
                    UploadDocument(FluCertofregpost, HdnCertofregpost_Name, strFileName, HypvwCertofregpost, LblCertofregpost, LnkDcertofregpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUAppDPRpost.ID))
            {
                if (FluAppDPRpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "AppDPRpost";
                    UploadDocument(FluAppDPRpost, HdnAppDPRpost_Name, strFileName, HypvwAppDPRpost, LblAppDPRpost, LnkDAppDPRpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUPCorEINPost.ID))
            {
                if (FluPCorEINPost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "PCorEINPost";
                    UploadDocument(FluPCorEINPost, HdnPCorEINPost_Name, strFileName, HypvwPCorEINPost, LblPCorEINPost, LnkDPCorEINPost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUloansancorFIappliedpost.ID))
            {
                if (FluLoansancorFIappliedpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "LoansancorFIappliedpost";
                    UploadDocument(FluLoansancorFIappliedpost, HdnLoansancorFIappliedpost_Name, strFileName, HypvwLoansancorFIappliedpost, LblLoansancorFIappliedpost, LnkDloansancorFIappliedpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUCapitalInvstPost.ID))
            {
                if (FluCapitalInvstPost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "CapitalInvstPost";
                    UploadDocument(FluCapitalInvstPost, HdnCapitalInvstPost_Name, strFileName, HypvwCapitalInvstPost, LblCapitalInvstPost, LnkDCapitalInvstPost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUInvplantmachinaryPost.ID))
            {
                if (FluInvplantmachinaryPost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "InvplantmachinaryPost";
                    UploadDocument(FluInvplantmachinaryPost, HdnInvplantmachinaryPost_Name, strFileName, HypvwInvplantmachinaryPost, LblInvplantmachinaryPost, LnkDInvplantmachinaryPost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUPlantmachinerypost.ID))
            {
                if (FluPlantmachinerypost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Plantmachinerypost";
                    UploadDocument(FluPlantmachinerypost, HdnPlantmachinerypost_Name, strFileName, HypvwPlantmachinerypost, LblPlantmachinerypost, LnkDPlantmachinerypost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUproductionormanufactpost.ID))
            {
                if (FluProductionormanufactpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Productionormanufactpost";
                    UploadDocument(FluProductionormanufactpost, HdnProductionormanufactpost_Name, strFileName, HypvwProductionormanufactpost, LblProductionormanufactpost, LnkDproductionormanufactpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUcatagoryfalprioritypost.ID))
            {
                if (FluCatagoryfalprioritypost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Catagoryfalprioritypost";
                    UploadDocument(FluCatagoryfalprioritypost, HdnCatagoryfalprioritypost_Name, strFileName, HypvwCatagoryfalprioritypost, LblCatagoryfalprioritypost, LnkDcatagoryfalprioritypost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUclearancefromPCB.ID))
            {
                if (FluClearancefromPCB.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "ClearancefromPCBpost";
                    UploadDocument(FluClearancefromPCB, HdnClearancefromPCB_Name, strFileName, HypvwClearancefromPCB, LblClearancefromPCB, LnkDclearancefromPCB, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUmigratedindustunitpost.ID))
            {
                if (FluMigratedindustunitpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Migratedindustunitpost";
                    UploadDocument(FluMigratedindustunitpost, HdnMigratedindustunitpost_Name, strFileName, HypvwMigratedindustunitpost, LblMigratedindustunitpost, LnkDmigratedindustunitpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUproductionforMSMEPost.ID))
            {
                if (FluProductionforMSMEPost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "ProductionforMSMEPost";
                    UploadDocument(FluProductionforMSMEPost, HdnProductionforMSMEPost_Name, strFileName, HypvwProductionforMSMEPost, LblProductionforMSMEPost, LnkDproductionforMSMEPost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUEmpoweredcommitpost.ID) && (FluEmpoweredcommitpost.HasFile))
            {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Empoweredcommitpost";
                    UploadDocument(FluEmpoweredcommitpost, HdnEmpoweredcommitpost_Name, strFileName, HypvwEmpoweredcommitpost, LblEmpoweredcommitpost, LnkDEmpoweredcommitpost, "InctBasicDoc");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void LnkBtn_Delete_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkDPowerattpre.ID))
            {
                UpdFileRemove(HdnPowerattpre_Name, LnkUPowerattpre, LnkDPowerattpre, HypPowerattpre, LblPowerattpre, FluPowerattpre, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDCertofreg.ID))
            {
                UpdFileRemove(HdnCertofreg_Name, LnkUCertofreg, LnkDCertofreg, HypvwCertofreg, LblCertofreg, FluCertofreg, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDAppDPR.ID))
            {
                UpdFileRemove(HdnAppDPR_Name, LnkUAppDPR, LnkDAppDPR, HypVwAppDPR, LblAppDPR, FlAppDPR, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDEIN.ID))
            {
                UpdFileRemove(HdnEIN_Name, LnkUEIN, LnkDEIN, HypVwEIN, LblEIN, FluEIN, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDPlantmachinery.ID))
            {
                UpdFileRemove(HdnPlantmachinery_Name, LnkUPlantmachinery, LnkDPlantmachinery, HyVwPlantmachinery, LblPlantmachinery, FluPlantmachinery, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDCapitalInvst.ID))
            {
                UpdFileRemove(HdnCapitalInvst_Name, LnkUCapitalInvst, LnkDCapitalInvst, HypVwCapitalInvst, LblCapitalInvst, FluCapitalInvst, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDInvplantmachinary.ID))
            {
                UpdFileRemove(HdnInvplantmachinary_Name, LnkUInvplantmachinary, LnkDInvplantmachinary, HypVwInvplantmachinary, LblInvplantmachinary, FluInvplantmachinary, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDproposedprod.ID))
            {
                UpdFileRemove(HdnProposedprod_Name, LnkUproposedprod, LnkDproposedprod, HypVwProposedprod, LblProposedprod, FluProposedprod, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDpresentstageimplemnt.ID))
            {
                UpdFileRemove(HdnPresentstageimplemnt_Name, LnkUpresentstageimplemnt, LnkDpresentstageimplemnt, HypVwpresentstageimplemnt, LblPresentstageimplemnt, FluPresentstageimplemnt, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDmigrantindustrial.ID))
            {
                UpdFileRemove(HdnMigrantindustrial_Name, LnkUmigrantindustrial, LnkDmigrantindustrial, HypvwMigrantindustrial, LblMigrantindustrial, FluMigrantindustrial, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDfixedcapitalinvst.ID))
            {
                UpdFileRemove(HdnFixedcapitalinvst_Name, LnkUfixedcapitalinvst, LnkDfixedcapitalinvst, HypvwFixedcapitalinvst, LblFixedcapitalinvst, FluFixedcapitalinvst, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDcatagoryfalpriority.ID))
            {
                UpdFileRemove(HdnCatagoryfalpriority_Name, LnkUcatagoryfalpriority, LnkDcatagoryfalpriority, HypvwCatagoryfalpriority, LblCatagoryfalpriority, FluCatagoryfalpriority, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDPowerattpost.ID))
            {
                UpdFileRemove(HdnPowerattpost_Name, LnkUPowerattpost, LnkDPowerattpost, HypvwPowerattpost, LblPowerattpost, FluPowerattPost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDpporthrustcertificate.ID))
            {
                UpdFileRemove(HdnPporthrustcertificate_Name, LnkUpporthrustcertificate, LnkDpporthrustcertificate, HypvwPporthrustcertificate, LblPporthrustcertificate, FluPporthrustcertificate, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDcertofregpost.ID))
            {
                UpdFileRemove(HdnCertofregpost_Name, LnkUcertofregpost, LnkDcertofregpost, HypvwCertofregpost, LblCertofregpost, FluCertofregpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDAppDPRpost.ID))
            {
                UpdFileRemove(HdnAppDPRpost_Name, LnkUAppDPRpost, LnkDAppDPRpost, HypvwAppDPRpost, LblAppDPRpost, FluAppDPRpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDPCorEINPost.ID))
            {
                UpdFileRemove(HdnPCorEINPost_Name, LnkUPCorEINPost, LnkDPCorEINPost, HypvwPCorEINPost, LblPCorEINPost, FluPCorEINPost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDloansancorFIappliedpost.ID))
            {
                UpdFileRemove(HdnLoansancorFIappliedpost_Name, LnkUloansancorFIappliedpost, LnkDloansancorFIappliedpost, HypvwLoansancorFIappliedpost, LblLoansancorFIappliedpost, FluLoansancorFIappliedpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDCapitalInvstPost.ID))
            {
                UpdFileRemove(HdnCapitalInvstPost_Name, LnkUCapitalInvstPost, LnkDCapitalInvstPost, HypvwCapitalInvstPost, LblCapitalInvstPost, FluCapitalInvstPost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDInvplantmachinaryPost.ID))
            {
                UpdFileRemove(HdnInvplantmachinaryPost_Name, LnkUInvplantmachinaryPost, LnkDInvplantmachinaryPost, HypvwInvplantmachinaryPost, LblInvplantmachinaryPost, FluInvplantmachinaryPost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDPlantmachinerypost.ID))
            {
                UpdFileRemove(HdnPlantmachinerypost_Name, LnkUPlantmachinerypost, LnkDPlantmachinerypost, HypvwPlantmachinerypost, LblPlantmachinerypost, FluPlantmachinerypost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDproductionormanufactpost.ID))
            {
                UpdFileRemove(HdnProductionormanufactpost_Name, LnkUproductionormanufactpost, LnkDproductionormanufactpost, HypvwProductionormanufactpost, LblProductionormanufactpost, FluProductionormanufactpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDcatagoryfalprioritypost.ID))
            {
                UpdFileRemove(HdnCatagoryfalprioritypost_Name, LnkUcatagoryfalprioritypost, LnkDcatagoryfalprioritypost, HypvwCatagoryfalprioritypost, LblCatagoryfalprioritypost, FluCatagoryfalprioritypost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDclearancefromPCB.ID))
            {
                UpdFileRemove(HdnClearancefromPCB_Name, LnkUclearancefromPCB, LnkDclearancefromPCB, HypvwClearancefromPCB, LblClearancefromPCB, FluClearancefromPCB, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDmigratedindustunitpost.ID))
            {
                UpdFileRemove(HdnMigratedindustunitpost_Name, LnkUmigratedindustunitpost, LnkDmigratedindustunitpost, HypvwMigratedindustunitpost, LblMigratedindustunitpost, FluMigratedindustunitpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDproductionforMSMEPost.ID))
            {
                UpdFileRemove(HdnProductionforMSMEPost_Name, LnkUproductionforMSMEPost, LnkDproductionforMSMEPost, HypvwProductionforMSMEPost, LblProductionforMSMEPost, FluProductionforMSMEPost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDEmpoweredcommitpost.ID))
            {
                UpdFileRemove(HdnEmpoweredcommitpost_Name, LnkUEmpoweredcommitpost, LnkDEmpoweredcommitpost, HypvwEmpoweredcommitpost, LblEmpoweredcommitpost, FluEmpoweredcommitpost, "InctBasicDoc");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("incentiveoffered.aspx");
    }

    protected void BtnDraft_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();

        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity1 = new Basic_Unit_Details_Entity();

        try
        {
            #region Production Item Before

            /*---------------------------------------------------------------------*/
            /// Add Production Item Before
            /*---------------------------------------------------------------------*/

            List<BasicProductionItemBefore> listProdBefore = new List<BasicProductionItemBefore>();

            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                BasicProductionItemBefore objItem = new BasicProductionItemBefore();

                Label lblProductionName = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                Label lblQuantity = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                Label lblOtherUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                Label lblValue = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                HiddenField hdnUnit = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");

                objItem.vchProductName = lblProductionName.Text;
                objItem.decQuantity = Convert.ToDecimal(lblQuantity.Text == "" ? "0" : lblQuantity.Text);
                if (hdnUnit.Value != "")
                {
                    objItem.intUnitType = Convert.ToInt32(hdnUnit.Value);
                }
                objItem.vchOtherUnit = lblOtherUnit.Text == "" ? null : lblOtherUnit.Text;
                objItem.decValue = Convert.ToDecimal(lblValue.Text == "" ? "0" : lblValue.Text);
                objItem.chItemFor = "B"; /// Before

                listProdBefore.Add(objItem);
            }

            objEntity1.ProductionItem_BE = listProdBefore;

            #endregion


            #region Term Loan
            /*---------------------------------------------------------------------*/
            /// Add Term Loan
            /*---------------------------------------------------------------------*/


            List<BasicTermLoan> listTL = new List<BasicTermLoan>();

            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                BasicTermLoan objItem = new BasicTermLoan();

                Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");

                objItem.vchNameOfFinancialInst = Lbl_TL_Financial_Inst.Text;
                objItem.vchState = Lbl_TL_State.Text;
                objItem.vchCity = Lbl_TL_City.Text;
                objItem.decLoanAmt = Convert.ToDecimal(Lbl_TL_Amount.Text == "" ? "0" : Lbl_TL_Amount.Text);
                objItem.dtmSanctionDate = Lbl_TL_Sanction_Date.Text == "" ? null : Lbl_TL_Sanction_Date.Text;
                objItem.decAvailedAmt = Convert.ToDecimal(Lbl_TL_Avail_Amt.Text == "" ? "0" : Lbl_TL_Avail_Amt.Text);
                objItem.dtmAvailedDate = Lbl_TL_Avail_Date.Text == "" ? null : Lbl_TL_Avail_Date.Text;

                listTL.Add(objItem);
            }

            objEntity1.TermLoan = listTL;
            #endregion

            #region Working Capital Loan

            /*---------------------------------------------------------------------*/
            ///Add Working Capital Loan
            /*---------------------------------------------------------------------*/

            List<BasicWorkingCapitalLoan> listWC = new List<BasicWorkingCapitalLoan>();

            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                BasicWorkingCapitalLoan objItem = new BasicWorkingCapitalLoan();

                Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");

                objItem.vchNameOfFinancialInst = Lbl_WC_Financial_Inst.Text;
                objItem.vchState = Lbl_WC_State.Text;
                objItem.vchCity = Lbl_WC_City.Text;
                objItem.decLoanAmt = Convert.ToDecimal(Lbl_WC_Amount.Text == "" ? "0" : Lbl_WC_Amount.Text);
                objItem.dtmSanctionDate = Lbl_WC_Sanction_Date.Text == "" ? null : Lbl_WC_Sanction_Date.Text;
                objItem.decAvailedAmt = Convert.ToDecimal(Lbl_WC_Avail_Amt.Text == "" ? "0" : Lbl_WC_Avail_Amt.Text);
                objItem.dtmAvailedDate = Lbl_WC_Avail_Date.Text == "" ? null : Lbl_WC_Avail_Date.Text;

                listWC.Add(objItem);
            }

            objEntity1.WorkingCapitalLoan = listWC;
            #endregion

            #region IndustrialUnitDetailsSection

            /*---------------------------------------------------------------------*/
            ///Industrial Unit Details Section
            /*---------------------------------------------------------------------*/
            objEntity1.strEnterpriseName = Txt_EnterPrise_Name.Text;
            objEntity1.intOrganisationType = Convert.ToInt32(DrpDwn_Org_Type.SelectedValue);
            objEntity1.strIndustryAddress = Txt_Industry_Address.Text;
            objEntity1.intUnitCat = Convert.ToInt32(DrpDwn_Unit_Cat.SelectedValue);

            objEntity1.strRegisteredOfcAddress = Txt_Regd_Office_Address.Text;
            objEntity1.strManagingPartnerGender = DrpDwn_Gender_Partner.SelectedValue;
            objEntity1.strManagingPartnerName = Txt_Partner_Name.Text;

            objEntity1.strEINNO = Txt_EIN_IL_NO.Text == "" ? null : Txt_EIN_IL_NO.Text;
            objEntity1.dtmEIN = Txt_EIN_IL_Date.Text == "" ? null : Txt_EIN_IL_Date.Text;
            objEntity1.strPhoneNo = Txt_Phone_no.Text;
            objEntity1.strEmail = Txt_Email.Text;

            /*-------------------------Industrial Unit Details End---------------------------------*/
            #endregion

            #region ProductionandEmploymentDetails Section

            /*----------------------------------------------------------*/
            /// Production and Employment Details Section
            /*----------------------------------------------------------*/
            objEntity1.dtmFFCIDateBefore = Txt_Proposed_Date.Text;
            objEntity1.dtmProdCommBefore = Txt_Commence_production.Text;
            if (Rad_production.SelectedValue == "1")
            {
                objEntity1.strUAMNo = Txt_PC_EMI_No.Text;
                objEntity1.dtmUAMdate = Txt_PC_EMI_Date.Text;
            }
            else if (Rad_production.SelectedValue == "2")
            {
                objEntity1.strUAMNo = Txt_Uam_No.Text;
                objEntity1.dtmUAMdate = Txt_Uam_Date.Text;
            }

            objEntity1.intEIMorUAMtype = Convert.ToInt32(Rad_production.SelectedValue);

            #endregion

            #region InvestmentDetailsSection

            /*----------------------------------------------------------*/
            /// Investment Details Section
            /*----------------------------------------------------------*/
            objEntity1.intDirectEmpAfter = Convert.ToInt32(Txt_total_emp_Number.Text == "" ? "0" : Txt_total_emp_Number.Text);
            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text);
            decimal decLandBefore = 0;
            decimal decBuildingBefore = 0;
            decimal decPlantMachBefore = 0;
            decimal decOtherFixedAssetBefore = 0;
            decimal decElectricalInstBefore = 0;
            decimal decLoadUnloadBefore = 0;
            decimal decMarginMoneyForworkingBefore = 0;

            if (Txt_Land_Details_before.Text != "")
            {
                decLandBefore = Convert.ToDecimal(Txt_Land_Details_before.Text);
            }
            if (Txt_Building_Before.Text != "")
            {
                decBuildingBefore = Convert.ToDecimal(Txt_Building_Before.Text);
            }
            if (Txt_Plant_Mach_Before.Text != "")
            {
                decPlantMachBefore = Convert.ToDecimal(Txt_Plant_Mach_Before.Text);
            }
            if (Txt_Other_Fixed_Asset_Before.Text != "")
            {
                decOtherFixedAssetBefore = Convert.ToDecimal(Txt_Other_Fixed_Asset_Before.Text);
            }
            if (Txt_Electrical_inst_Before.Text != "")
            {
                decElectricalInstBefore = Convert.ToDecimal(Txt_Electrical_inst_Before.Text);
            }
            if (Txt_Loadig_Before.Text != "")
            {
                decLoadUnloadBefore = Convert.ToDecimal(Txt_Loadig_Before.Text);
            }
            if (Txt_Margine_money.Text != "")
            {
                decMarginMoneyForworkingBefore = Convert.ToDecimal(Txt_Margine_money.Text);
            }
            decimal decTotalCapitalBefore = decLandBefore + decBuildingBefore + decPlantMachBefore + decOtherFixedAssetBefore + decElectricalInstBefore + decLoadUnloadBefore + decMarginMoneyForworkingBefore;

            objEntity1.decLandAmtBefore = decLandBefore;
            objEntity1.decBuildingAmtBefore = decBuildingBefore;
            objEntity1.decPlantMachAmtBefore = decPlantMachBefore;
            objEntity1.decOtheFixedAssetAmtBefore = decOtherFixedAssetBefore;
            objEntity1.decTotalAmtBefore = decTotalCapitalBefore;
            objEntity1.decElectricalInstAmtBefore = decElectricalInstBefore;
            objEntity1.decLoadUnloadAmtBefore = decLoadUnloadBefore;
            objEntity1.decMarginMoneyForworkingAmtBefore = decMarginMoneyForworkingBefore;

            /*---------------------------------------------------------------------*/
            /// Investment Details (After)
            /*---------------------------------------------------------------------*/
            decimal decLandAfter = 0;
            decimal decBuildingAfter = 0;
            decimal decPlantMachAfter = 0;
            decimal decOtherFixedAssetAfter = 0;
            decimal decElectricalInstAfter = 0;
            decimal decLoadUnloadAfter = 0;
            decimal decMarginMoneyForworkingAfter = 0;

            if (Txt_Land_After.Text != "")
            {
                decLandAfter = Convert.ToDecimal(Txt_Land_After.Text);
            }
            if (Txt_Building_After.Text != "")
            {
                decBuildingAfter = Convert.ToDecimal(Txt_Building_After.Text);
            }
            if (Txt_Plant_Mach_After.Text != "")
            {
                decPlantMachAfter = Convert.ToDecimal(Txt_Plant_Mach_After.Text);
            }
            if (Txt_Other_Fixed_Asset_After.Text != "")
            {
                decOtherFixedAssetAfter = Convert.ToDecimal(Txt_Other_Fixed_Asset_After.Text);
            }
            if (Txt_Electrical_inst_Before.Text != "")
            {
                decElectricalInstAfter = Convert.ToDecimal(Txt_Electrical_inst_Before.Text);
            }
            if (Txt_Loadig_After.Text != "")
            {
                decLoadUnloadAfter = Convert.ToDecimal(Txt_Loadig_After.Text);
            }
            if (Txt_Margine_money_After.Text != "")
            {
                decMarginMoneyForworkingAfter = Convert.ToDecimal(Txt_Margine_money_After.Text);
            }
            decimal decTotalCapitalAfter = decLandAfter + decBuildingAfter + decPlantMachAfter + decOtherFixedAssetAfter + decElectricalInstAfter + decLoadUnloadAfter + decMarginMoneyForworkingAfter;

            objEntity1.decLandAmtAfter = decLandAfter;
            objEntity1.decBuildingAmtAfter = decBuildingAfter;
            objEntity1.decPlantMachAmtAfter = decPlantMachAfter;
            objEntity1.decOtheFixedAssetAmtAfter = decOtherFixedAssetAfter;
            objEntity1.decTotalAmtAfter = decTotalCapitalAfter;
            objEntity1.decElectricalInstAmtAfter = decElectricalInstAfter;
            objEntity1.decLoadUnloadAmtAfter = decLoadUnloadAfter;
            objEntity1.decMarginMoneyForworkingAmtAfter = decMarginMoneyForworkingAfter;

            /*----------------------------Investment Details Section End------------------------------*/
            #endregion

            #region MeansofFinanceSection

            /*---------------------------------------------------------------------*/
            /// Means of Finance Section
            /*---------------------------------------------------------------------*/
            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text == "" ? "0" : Txt_Equity_Amt.Text);
            objEntity1.intProjectClearance = Convert.ToInt32(Rad_Clearnce_PCB.SelectedValue);
            objEntity1.intProvisnalPriorityThrustStatus = Convert.ToInt32(Rad_PP_thrust_Status.SelectedValue);
            objEntity1.intIPRinctiveAvel = Convert.ToInt32(Rad_IPR_Incentive_avail.SelectedValue);
            objEntity1.strClearnceswm = Txt_Swm_approve.Text;

            #endregion

            #region OtherDocumentSection

            /*---------------------------------------------------------------------*/
            /// Other Document Section
            /*---------------------------------------------------------------------*/           
            if (HdnPowerattpre_Name.Value != "")
            {
                objEntity1.strPworofAttorneyPreDocCode = HdnPowerattpre_Code.Value;
                objEntity1.strPworofAttorneyPre = HdnPowerattpre_Name.Value;
            }
            else
            {
                objEntity1.strPworofAttorneyPreDocCode = null;
                objEntity1.strPworofAttorneyPre = null;
            }

            if (HdnCertofreg_Name.Value != "")
            {
                objEntity1.strCertificateofregistrationDocCodepre = HdnCertofreg_Code.Value;
                objEntity1.strCertificateofregistrationpre = HdnCertofreg_Name.Value;
            }
            else
            {   
                objEntity1.strCertificateofregistrationDocCodepre = null;
                objEntity1.strCertificateofregistrationpre = null;
            }

            if (HdnAppDPR_Name.Value != "")
            {
                objEntity1.strApproveDPRDocCodePre = HdnAppDPR_Code.Value;
                objEntity1.strApproveDPRPre = HdnAppDPR_Name.Value;
            }
            else
            {
                objEntity1.strApproveDPRDocCodePre = null;
                objEntity1.strApproveDPRPre = null;
            }

            if (HdnEIN_Name.Value != "")
            {
                objEntity1.strEINapprovalDocCodePre = HdnEIN_Code.Value;
                objEntity1.strEINapprovalPre = HdnEIN_Name.Value;
            }
            else
            {
                objEntity1.strEINapprovalDocCodePre = null;
                objEntity1.strEINapprovalPre = null;
            }

            if (HdnPlantmachinery_Name.Value != "")
            {
                objEntity1.strBalacingEquipmentDocCodePre = HdnPlantmachinery_Code.Value;
                objEntity1.strBalacingEquipmentPre = HdnPlantmachinery_Name.Value;
            }
            else
            {
                objEntity1.strBalacingEquipmentDocCodePre = null;
                objEntity1.strBalacingEquipmentPre = null;
            }

            if (HdnCapitalInvst_Name.Value != "")
            {
                objEntity1.strCapitalInvstDocCodePre = HdnCapitalInvst_Code.Value;
                objEntity1.strCapitalInvstPre = HdnCapitalInvst_Name.Value;
            }
            else
            {
                objEntity1.strCapitalInvstDocCodePre = null;
                objEntity1.strCapitalInvstPre = null;
            }

            if (HdnInvplantmachinary_Name.Value != "")
            {
                objEntity1.strInvestmentplantmachinaryDocCodePre = HdnInvplantmachinary_Code.Value;
                objEntity1.strInvestmentplantmachinaryPre = HdnInvplantmachinary_Name.Value;
            }
            else
            {
                objEntity1.strInvestmentplantmachinaryDocCodePre = null;
                objEntity1.strInvestmentplantmachinaryPre = null;
            }

            if (HdnProposedprod_Name.Value != "")
            {
                objEntity1.strProposedprodDocCodePre = HdnProposedprod_Code.Value;
                objEntity1.strProposedprodPre = HdnProposedprod_Name.Value;
            }
            else
            {
                objEntity1.strProposedprodDocCodePre = null;
                objEntity1.strProposedprodPre = null;
            }

            if (HdnPresentstageimplemnt_Name.Value != "")
            {
                objEntity1.strPresentStageImplentDocCodePre = HdnPresentstageimplemnt_Code.Value;
                objEntity1.strPresentStageImplentPre = HdnPresentstageimplemnt_Name.Value;
            }
            else
            {
                objEntity1.strPresentStageImplentDocCodePre = null;
                objEntity1.strPresentStageImplentPre = null;
            }

            if (HdnMigrantindustrial_Name.Value != "")
            {
                objEntity1.strMigrantIndustryunitDocCodePre = HdnMigrantindustrial_Code.Value;
                objEntity1.strMigrantIndustryunitPre = HdnMigrantindustrial_Name.Value;
            }
            else
            {
                objEntity1.strMigrantIndustryunitDocCodePre = null;
                objEntity1.strMigrantIndustryunitPre = null;
            }

            if (HdnFixedcapitalinvst_Name.Value != "")
            {
                objEntity1.strfixedcapitalinvstDocCodePre = HdnFixedcapitalinvst_Code.Value;
                objEntity1.strfixedcapitalinvstPre = HdnFixedcapitalinvst_Name.Value;
            }
            else
            {
                objEntity1.strfixedcapitalinvstDocCodePre = null;
                objEntity1.strfixedcapitalinvstPre = null;
            }

            if (HdnCatagoryfalpriority_Name.Value != "")
            {
                objEntity1.strPriorityorThrustsectorDocCodePre = HdnCatagoryfalpriority_Code.Value;
                objEntity1.strPriorityorThrustsectorPre = HdnCatagoryfalpriority_Name.Value;
            }
            else
            {
                objEntity1.strPriorityorThrustsectorDocCodePre = null;
                objEntity1.strPriorityorThrustsectorPre = null;
            }
            
            if (HdnPowerattpost_Name.Value != "")
            {
                objEntity1.strPworofAttorneyPostDocCode = HdnPowerattpost_Code.Value;
                objEntity1.strPworofAttorneyPost = HdnPowerattpost_Name.Value;
            }
            else
            {
                objEntity1.strPworofAttorneyPostDocCode = null;
                objEntity1.strPworofAttorneyPost = null;
            }

            if (HdnPporthrustcertificate_Name.Value != "")
            {
                objEntity1.strPPorThrustStatusCertPostDocCode = HdnPporthrustcertificate_Code.Value;
                objEntity1.strPPorThrustStatusCertPost = HdnPporthrustcertificate_Name.Value;
            }
            else
            {
                objEntity1.strPPorThrustStatusCertPostDocCode = null;
                objEntity1.strPPorThrustStatusCertPost = null;
            }

            if (HdnCertofregpost_Name.Value != "")
            {
                objEntity1.strCertificateofregistrationDocCodepost = HdnCertofregpost_Code.Value;
                objEntity1.strCertificateofregistrationpost = HdnCertofregpost_Name.Value;
            }
            else
            {
                objEntity1.strCertificateofregistrationDocCodepost = null;
                objEntity1.strCertificateofregistrationpost = null;
            }

            if (HdnAppDPRpost_Name.Value != "")
            {
                objEntity1.strApproveDPRDocCodePost = HdnAppDPRpost_Code.Value;
                objEntity1.strApproveDPRPost = HdnAppDPRpost_Name.Value;
            }
            else
            {
                objEntity1.strApproveDPRDocCodePost = null;
                objEntity1.strApproveDPRPost = null;
            }

            if (HdnPCorEINPost_Name.Value != "")
            {
                objEntity1.strPcDocCodePost = HdnPCorEINPost_Code.Value;
                objEntity1.strPcPost = HdnPCorEINPost_Name.Value;
            }
            else
            {
                objEntity1.strPcDocCodePost = null;
                objEntity1.strPcPost = null;
            }

            if (HdnLoansancorFIappliedpost_Name.Value != "")
            {
                objEntity1.strSanctionbankorFIDocCodePost = HdnLoansancorFIappliedpost_Code.Value;
                objEntity1.strSanctionbankorFIPost = HdnLoansancorFIappliedpost_Name.Value;
            }
            else
            {
                objEntity1.strSanctionbankorFIDocCodePost = null;
                objEntity1.strSanctionbankorFIPost = null;
            }

            if (HdnCapitalInvstPost_Name.Value != "")
            {
                objEntity1.strCapitalInvstDocCodePost = HdnCapitalInvstPost_Code.Value;
                objEntity1.strCapitalInvstPost = HdnCapitalInvstPost_Name.Value;
            }
            else
            {
                objEntity1.strCapitalInvstDocCodePost = null;
                objEntity1.strCapitalInvstPost = null;
            }

            if (HdnInvplantmachinaryPost_Name.Value != "")
            {
                objEntity1.strInvestmentplantmachinaryDocCodePost = HdnInvplantmachinaryPost_Code.Value;
                objEntity1.strInvestmentplantmachinaryPost = HdnInvplantmachinaryPost_Name.Value;
            }
            else
            {
                objEntity1.strInvestmentplantmachinaryDocCodePost = null;
                objEntity1.strInvestmentplantmachinaryPost = null;
            }

            if (HdnPlantmachinerypost_Name.Value != "")
            {
                objEntity1.strBalacingEquipmentDocCodePost = HdnPlantmachinerypost_Code.Value;
                objEntity1.strBalacingEquipmentPost = HdnPlantmachinerypost_Name.Value;
            }
            else
            {
                objEntity1.strBalacingEquipmentDocCodePost = null;
                objEntity1.strBalacingEquipmentPost = null;
            }

            if (HdnProductionormanufactpost_Name.Value != "")
            {
                objEntity1.strServiceProvideDocCodePost = HdnProductionormanufactpost_Code.Value;
                objEntity1.strServiceProvidePost = HdnProductionormanufactpost_Name.Value;
            }
            else
            {
                objEntity1.strServiceProvideDocCodePost = null;
                objEntity1.strServiceProvidePost = null;
            }

            if (HdnCatagoryfalprioritypost_Name.Value != "")
            {
                objEntity1.strPriorityorThrustsectorDocCodePost = HdnCatagoryfalprioritypost_Code.Value;
                objEntity1.strPriorityorThrustsectorPost = HdnCatagoryfalprioritypost_Name.Value;
            }
            else
            {
                objEntity1.strPriorityorThrustsectorDocCodePost = null;
                objEntity1.strPriorityorThrustsectorPost = null;
            }

            if (HdnClearancefromPCB_Name.Value != "")
            {
                objEntity1.strClearancefromPCBDocCodePost = HdnClearancefromPCB_Code.Value;
                objEntity1.strClearancefromPCBPost = HdnClearancefromPCB_Name.Value;
            }
            else
            {
                objEntity1.strClearancefromPCBDocCodePost = null;
                objEntity1.strClearancefromPCBPost = null;
            }

            if (HdnMigratedindustunitpost_Name.Value != "")
            {
                objEntity1.strMigrantIndustryunitDocCodePost = HdnMigratedindustunitpost_Code.Value;
                objEntity1.strMigrantIndustryunitPost = HdnMigratedindustunitpost_Name.Value;
            }
            else
            {
                objEntity1.strMigrantIndustryunitDocCodePost = null;
                objEntity1.strMigrantIndustryunitPost = null;
            }

            if (HdnProductionforMSMEPost_Name.Value != "")
            {
                objEntity1.strfixedcapitalinvstDocCodePost = HdnProductionforMSMEPost_Code.Value;
                objEntity1.strfixedcapitalinvstPost = HdnProductionforMSMEPost_Name.Value;
            }
            else
            {
                objEntity1.strfixedcapitalinvstDocCodePost = null;
                objEntity1.strfixedcapitalinvstPost = null;
            }

            if (HdnEmpoweredcommitpost_Name.Value != "")
            {
                objEntity1.strEmpoweredCommitteeDocCodePost = HdnEmpoweredcommitpost_Code.Value;
                objEntity1.strEmpoweredCommitteePost = HdnEmpoweredcommitpost_Name.Value;
            }
            else
            {
                objEntity1.strEmpoweredCommitteeDocCodePost = null;
                objEntity1.strEmpoweredCommitteePost = null;
            }

            /*-------------------------Other Document End---------------------------------*/
            #endregion

            ///This method is called to assign  Session value
            FillData();
            objEntity1.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);

            objEntity1.strPcNoAfter = Session["ProposalNo"].ToString();
            objEntity1.strIndustryCode = Session["UnitCode"].ToString();

            if (Request.QueryString["key"] != null)
            {
                objEntity1.strIncentiveNumber = Request.QueryString["key"].ToString();
            }
            else
            {
                objEntity1.strIncentiveNumber = Request.QueryString["IncentiveNo"].ToString();
            }

            /*---------------------------------------------------------------------*/
            /// Data Insert and Update
            /// 1-Insert
            /// 2-Update
            /*---------------------------------------------------------------------*/ 

            string strReturnStatus = objBAL.Thrust_Priority_Draft(objEntity1);
            if (strReturnStatus == "1" || strReturnStatus == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Drafted Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = './incentiveoffered.aspx';}); </script>", false);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something Went wrong.</strong>');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }
}