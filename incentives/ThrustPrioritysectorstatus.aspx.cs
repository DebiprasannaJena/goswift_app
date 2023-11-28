using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    
    
    DataTable dtSalutation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["InctUniqueNo"]) || !String.IsNullOrEmpty(Request.QueryString["key"]))
        {
            if (Request.QueryString["key"] != null)
            {
              string strInctId = Request.QueryString["key"].ToString();
            }

            if (Request.QueryString["InctUniqueNo"] != null)
            {
               string UniqueNo = Request.QueryString["InctUniqueNo"].ToString();
               string InctNo= Request.QueryString["IncentiveNo"].ToString();
            }
        }

        if (!IsPostBack)
            {            
            fillUnitCategory();
            fillOrgType();
            fillSalutation();
            fillUnitMeasurment();
            Txt_Other_Unit_Before.Visible = false;
           
            if (Request.QueryString["InctUniqueNo"] != null)
            {             
                PostpopulateDataComm(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
               
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
                fillUnitMeasurment();
                fillUnitCategory();
                Txt_Other_Unit_Before.Visible = false;
                Txt_EnterPrise_Name.Enabled = false;
                fillData();
                fillOrgType();
                fillSalutation();
                BtnDraft.Visible = true;
                BtnApply.Visible = true;
            }
        }
    }


    public void PostpopulateDataComm(int id)
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
           

            ViewState["salutation"] = dtSalutation;

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
                Txt_Partner_Name.Text =dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                DrpDwn_Gender_Partner.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
                Txt_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
                Txt_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
      //   *---------------------------------------------------------------------------------------------------------------------------------*/

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
                      Txt_Proposed_Date.Text= dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
                      Txt_Commence_production.Text= dtProductionPre.Rows[0]["dtmFFCIDateBefore"].ToString();
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
                    Txt_Uam_No.Text= dtMoFTermLoanPre.Rows[0]["vchEIMorUAMnumber"].ToString();
                    Txt_Uam_Date.Text= dtMoFTermLoanPre.Rows[0]["dtmEIMorUAMdate"].ToString();
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

                //#region MEANS OF FINANCE

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

///----------------------------------------------Other File Upload---------------------------------------------------------------------/////

                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D282")
                {
                    hdnPowerattpre_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypPowerattpre.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypPowerattpre.Visible = true;
                    lnkDPowerattpre.Visible = true;
                    lblPowerattpre.Visible = true;
                    flPowerattpre.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D283")
                {
                    certofreg_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwcertofreg.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwcertofreg.Visible = true;
                    lnkDcertofreg.Visible = true;
                    lblcertofreg.Visible = true;
                    flcertofreg.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D284")
                {
                    hdnAppDPR_Name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwAppDPR.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwAppDPR.Visible = true;
                    lnkDAppDPR.Visible = true;
                    lblAppDPR.Visible = true;
                    flAppDPR.Enabled = false;
                }               
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D285")
                {
                    hdnEIN_Name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwEIN.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwEIN.Visible = true;
                    lnkDEIN.Visible = true;
                    lblEIN.Visible = true;
                    flEIN.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D286")
                {
                    hdnPlantmachinery_Name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hyVwPlantmachinery.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hyVwPlantmachinery.Visible = true;
                    lnkDPlantmachinery.Visible = true;
                    lblPlantmachinery.Visible = true;
                    flPlantmachinery.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D287")
                {
                    hdnCapitalInvst_Name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwCapitalInvst.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwCapitalInvst.Visible = true;
                    lnkDCapitalInvst.Visible = true;
                    lblCapitalInvst.Visible = true;
                    flCapitalInvst.Enabled = false;
                }

                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D288")
                {
                    hdnInvplantmachinary_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwInvplantmachinary.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwInvplantmachinary.Visible = true;
                    lnkDInvplantmachinary.Visible = true;
                    lblInvplantmachinary.Visible = true;
                    flInvplantmachinary.Enabled = false;
                }

                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D289")
                {
                    hdnproposedprod_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwproposedprod.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwproposedprod.Visible = true;
                    lnkDproposedprod.Visible = true;
                    lblproposedprod.Visible = true;
                    flproposedprod.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D290")
                {
                    hdnpresentstageimplemnt_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwpresentstageimplemnt.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypVwpresentstageimplemnt.Visible = true;
                    lnkDpresentstageimplemnt.Visible = true;
                    lblpresentstageimplemnt.Visible = true;
                    flpresentstageimplemnt.Enabled = false;
                }

                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D291")
                {
                    hdnmigrantindustrial_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwmigrantindustrial.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwmigrantindustrial.Visible = true;
                    lnkDmigrantindustrial.Visible = true;
                    lblmigrantindustrial.Visible = true;
                    flmigrantindustrial.Enabled = false;
                }

                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D292")
                {
                    hdnfixedcapitalinvst_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwfixedcapitalinvst.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwfixedcapitalinvst.Visible = true;
                    lnkDfixedcapitalinvst.Visible = true;
                    lblfixedcapitalinvst.Visible = true;
                    flfixedcapitalinvst.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D293")
                {
                    hdncatagoryfalpriority_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwcatagoryfalpriority.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwcatagoryfalpriority.Visible = true;
                    lnkDcatagoryfalpriority.Visible = true;
                    lblcatagoryfalpriority.Visible = true;
                    flcatagoryfalpriority.Enabled = false;
                }

            ///---------------------------------------------End--------------------------------------------------------------------------------////

            ///----------------------------------------------Priority or Thrust Sector Status(Post Production)-----------------------------------//



                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D294")
                {
                    Powerattpost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwPowerattpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwPowerattpost.Visible = true;
                    lnkDPowerattpost.Visible = true;
                    lblPowerattpost.Visible = true;
                    flPowerattpost.Enabled = false;
                }

                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D295")
                {
                    hdnpporthrustcertificate_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwpporthrustcertificate.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwpporthrustcertificate.Visible = true;
                    lnkDpporthrustcertificate.Visible = true;
                    lblpporthrustcertificate.Visible = true;
                    flpporthrustcertificate.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D296")
                {
                    hdncertofregpost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwcertofregpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwcertofregpost.Visible = true;
                    lnkDcertofregpost.Visible = true;
                    lblcertofregpost.Visible = true;
                    flcertofregpost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D297")
                {
                    hdnAppDPRpost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwAppDPRpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwAppDPRpost.Visible = true;
                    lnkDAppDPRpost.Visible = true;
                    lblAppDPRpost.Visible = true;
                    flAppDPRpost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D298")
                {
                    hdnPCorEINPost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwPCorEINPost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwPCorEINPost.Visible = true;
                    lnkDPCorEINPost.Visible = true;
                    lblPCorEINPost.Visible = true;
                    flPCorEINPost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D299")
                {
                    hdnloansancorFIappliedpost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwloansancorFIappliedpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwloansancorFIappliedpost.Visible = true;
                    lnkDloansancorFIappliedpost.Visible = true;
                    lblloansancorFIappliedpost.Visible = true;
                    flloansancorFIappliedpost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D300")
                {
                    hdnCapitalInvstPost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwCapitalInvstPost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwCapitalInvstPost.Visible = true;
                    lnkDCapitalInvstPost.Visible = true;
                    lblCapitalInvstPost.Visible = true;
                    flCapitalInvstPost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D301")
                {
                    hdnflInvplantmachinaryPost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwInvplantmachinaryPost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwInvplantmachinaryPost.Visible = true;
                    lnkDInvplantmachinaryPost.Visible = true;
                    lblInvplantmachinaryPost.Visible = true;
                    flInvplantmachinaryPost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D302")
                {
                    hdnPlantmachinerypost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwPlantmachinerypost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwPlantmachinerypost.Visible = true;
                    lnkDPlantmachinerypost.Visible = true;
                    lblPlantmachinerypost.Visible = true;
                    flPlantmachinerypost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D303")
                {
                    hdnproductionormanufactpost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwproductionormanufactpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwproductionormanufactpost.Visible = true;
                    lnkDproductionormanufactpost.Visible = true;
                    lblproductionormanufactpost.Visible = true;
                    flproductionormanufactpost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D304")
                {
                    hdncatagoryfalprioritypost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwcatagoryfalprioritypost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwcatagoryfalprioritypost.Visible = true;
                    lnkDcatagoryfalprioritypost.Visible = true;
                    lblcatagoryfalprioritypost.Visible = true;
                    flcatagoryfalprioritypost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D305")
                {
                    hdnclearancefromPCB_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwclearancefromPCB.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwclearancefromPCB.Visible = true;
                    lnkDclearancefromPCB.Visible = true;
                    lblclearancefromPCB.Visible = true;
                    flclearancefromPCB.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D306")
                {
                    hdnmigratedindustunitpost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwmigratedindustunitpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwmigratedindustunitpost.Visible = true;
                    lnkDmigratedindustunitpost.Visible = true;
                    lblmigratedindustunitpost.Visible = true;
                    flmigratedindustunitpost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D307")
                {
                    hdnproductionforMSMEPost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwproductionforMSMEPost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwproductionforMSMEPost.Visible = true;
                    lnkDproductionforMSMEPost.Visible = true;
                    lblproductionforMSMEPost.Visible = true;
                    flproductionforMSMEPost.Enabled = false;
                }
                if (dtMoFWorkingLoanPre.Rows[0]["vchDocId"].ToString() == "D308")
                {
                    hdnproductionforMSMEPost_name.Value = dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwEmpoweredcommitpost.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMoFWorkingLoanPre.Rows[0]["vchFileName"].ToString();
                    hypvwEmpoweredcommitpost.Visible = true;
                    lnkDEmpoweredcommitpost.Visible = true;
                    lblEmpoweredcommitpost.Visible = true;
                    flEmpoweredcommitpost.Enabled = false;
                }

                #endregion

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }


    }



    private void fillUnitMeasurment()
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

    private void fillOrgType()
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
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    private void fillSalutation()
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
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    private void fillUnitCategory()
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
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }


    private void fillData()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity = new Basic_Unit_Details_Entity();
        DataSet ds = new DataSet();
        try
        {
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            ds = objBAL.Basic_Unit_Details_V(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                /*----------------------------------------------------------------------------*/
                ///// Common Information 
                
                string strDataSource = ds.Tables[0].Rows[0]["vch_Data_Source"].ToString();
                string strPcStatus = ds.Tables[0].Rows[0]["vch_PC_Status"].ToString();
                string strIsExistBefore = ds.Tables[0].Rows[0]["vch_Is_Before_Exist"].ToString();
                string strIsExistAfter = ds.Tables[0].Rows[0]["vch_Is_After_Exist"].ToString();
                string strIndustryCode = ds.Tables[0].Rows[0]["vch_Industry_Code"].ToString();
                string strProposalNo = ds.Tables[0].Rows[0]["vch_Proposal_No"].ToString();
                string strProjectType = ds.Tables[0].Rows[0]["int_Project_Type"].ToString();
                string strNewPcFound = ds.Tables[0].Rows[0]["vch_New_PC_Found"].ToString();

                /*----------------------------------------------------------------------------*/
                ////// If new PC found then assign strDataSource=PC
                ////// Only when data present in basic table and a new PC found 
                /*----------------------------------------------------------------------------*/
                if (strDataSource == "BASIC")
                {
                    if (strNewPcFound == "Y")
                    {
                        strDataSource = "PC";
                    }
                }
                /*----------------------------------------------------------------------------*/
                ////// Value Assigned to HiddenField for use in Validation
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
                ///// Session Assigned Here
                
                Session["UnitCode"] = strIndustryCode;
                Session["ProposalNo"] = strProposalNo;
            }

            
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
            ds = null;
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

        string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
        CommonFunctions cmmf = new CommonFunctions();

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
            string completePath = Server.MapPath(path);
            //if (File.Exists(completePath))
            //{
            //    File.Delete(completePath);
            //}

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
        catch(Exception ex)
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
        finally
        {
            table = null;
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
        finally
        {
            table = null;
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
        finally
        {
            table = null;
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
        finally
        {
            table = null;
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
                table.Rows.Add(Lbl_WC_Financial_Inst.Text, Lbl_WC_State.Text, Lbl_WC_City.Text, Lbl_WC_Amount.Text, Lbl_WC_Sanction_Date.Text, Lbl_WC_Avail_Amt.Text, Txt_WC_Availed_Date.Text);
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
        finally
        {
            table = null;
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
                    table.Rows.Add(Lbl_WC_Financial_Inst.Text, Lbl_WC_State.Text, Lbl_WC_City.Text, Lbl_WC_Amount.Text, Lbl_WC_Sanction_Date.Text, Lbl_WC_Avail_Amt.Text, Txt_WC_Availed_Date.Text);
                }
            }

            Grd_WC.DataSource = table;
            Grd_WC.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            table = null;
        }
    }

    protected void BtnApply_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
        DataSet ds = new DataSet();
       
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity1 = new Basic_Unit_Details_Entity();      

        try
        {
            #region Production Item Before

            /*---------------------------------------------------------------------*/
            ////// Add Production Item Before

            BasicProductionItemBefore objProdBefore = new BasicProductionItemBefore();
            List<BasicProductionItemBefore> listProdBefore = new List<BasicProductionItemBefore>();

            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                BasicProductionItemBefore objItem = new BasicProductionItemBefore();

                Label lblProductionName = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                Label lblQuantity = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                Label lblUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
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
            ////// Add Term Loan

            BasicTermLoan objTL = new BasicTermLoan();
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
            ////// Add Working Capital Loan

            BasicWorkingCapitalLoan objWC = new BasicWorkingCapitalLoan();
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
            /////// Industrial Unit Details Section

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
            ////// Production and Employment Details Section
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
            ////// Investment Details Section
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
            decimal decTotalCapitalBefore = decLandBefore + decBuildingBefore + decPlantMachBefore + decOtherFixedAssetBefore+ decElectricalInstBefore+ decLoadUnloadBefore + decMarginMoneyForworkingBefore;

            objEntity1.decLandAmtBefore = decLandBefore;
            objEntity1.decBuildingAmtBefore = decBuildingBefore;
            objEntity1.decPlantMachAmtBefore = decPlantMachBefore;
            objEntity1.decOtheFixedAssetAmtBefore = decOtherFixedAssetBefore;
            objEntity1.decTotalAmtBefore = decTotalCapitalBefore;
            objEntity1.decElectricalInstAmtBefore = decElectricalInstBefore;
            objEntity1.decLoadUnloadAmtBefore = decLoadUnloadBefore;
            objEntity1.decMarginMoneyForworkingAmtBefore = decMarginMoneyForworkingBefore;
            /*---------------------------------------------------------------------*/
            ///// Investment Details (After)
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
            decimal decTotalCapitalAfter = decLandAfter + decBuildingAfter + decPlantMachAfter + decOtherFixedAssetAfter+ decElectricalInstAfter+ decLoadUnloadAfter+ decMarginMoneyForworkingAfter;

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

            /*----------------------------------------------------------*/
            ////// Means of Finance Section

            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text == "" ? "0" : Txt_Equity_Amt.Text);
            objEntity1.intProjectClearance = Convert.ToInt32(Rad_Clearnce_PCB.SelectedValue);
            objEntity1.intProvisnalPriorityThrustStatus= Convert.ToInt32(Rad_PP_thrust_Status.SelectedValue);
            objEntity1.intIPRinctiveAvel= Convert.ToInt32(Rad_IPR_Incentive_avail.SelectedValue);
            objEntity1.strClearnceswm = Txt_Swm_approve.Text;

            #endregion

            #region OtherDocumentSection

            /*---------------------------------------------------------------------*/
            /////// Other Document Section

            /*----------------------------------------------------------*/
            /*--------------------------Pre-Production Document Section Start-----------------------------------------------*/
            if (hdnPowerattpre_name.Value != "")
            {
                objEntity1.strPworofAttorneyPreDocCode = hdnPowerattpre_code.Value;
                objEntity1.strPworofAttorneyPre = hdnPowerattpre_name.Value;
            }
            else
            {
                objEntity1.strPworofAttorneyPreDocCode = null;
                objEntity1.strPworofAttorneyPre = null;
            }
            if (certofreg_name.Value != "")
            {
                objEntity1.strCertificateofregistrationDocCodepre = certofreg_code.Value;
                objEntity1.strCertificateofregistrationpre = certofreg_name.Value;
            }
            else
            {
                objEntity1.strCertificateofregistrationDocCodepre = null;
                objEntity1.strCertificateofregistrationpre = null;
            }
            if (hdnAppDPR_Name.Value != "")
            {
                objEntity1.strApproveDPRDocCodePre = hdnAppDPR_Code.Value;
                objEntity1.strApproveDPRPre = hdnAppDPR_Name.Value;
            }
            else
            {
                objEntity1.strApproveDPRDocCodePre = null;
                objEntity1.strApproveDPRPre = null;
            }
            if (hdnEIN_Name.Value != "")
            {
                objEntity1.strEINapprovalDocCodePre = hdnEIN_Code.Value;
                objEntity1.strEINapprovalPre = hdnEIN_Name.Value;
            }
            else
            {
                objEntity1.strEINapprovalDocCodePre = null;
                objEntity1.strEINapprovalPre = null;
            }
            if (hdnPlantmachinery_Name.Value != "")
            {
                objEntity1.strBalacingEquipmentDocCodePre = hdnPlantmachinery_Code.Value;
                objEntity1.strBalacingEquipmentPre = hdnPlantmachinery_Name.Value;
            }
            else
            {
                objEntity1.strBalacingEquipmentDocCodePre = null;
                objEntity1.strBalacingEquipmentPre = null;
            }
            if (hdnCapitalInvst_Name.Value != "")
            {
                objEntity1.strCapitalInvstDocCodePre = hdnCapitalInvst_Code.Value;
                objEntity1.strCapitalInvstPre = hdnCapitalInvst_Name.Value;
            }
            else
            {
                objEntity1.strCapitalInvstDocCodePre = null;
                objEntity1.strCapitalInvstPre = null;
            }
            if (hdnInvplantmachinary_name.Value != "")
            {
                objEntity1.strInvestmentplantmachinaryDocCodePre = hdnInvplantmachinary_code.Value;
                objEntity1.strInvestmentplantmachinaryPre = hdnInvplantmachinary_name.Value;
            }
            else
            {
                objEntity1.strInvestmentplantmachinaryDocCodePre = null;
                objEntity1.strInvestmentplantmachinaryPre = null;
            }
            if (hdnproposedprod_name.Value != "")
            {
                objEntity1.strProposedprodDocCodePre = hdnproposedprod_code.Value;
                objEntity1.strProposedprodPre = hdnproposedprod_name.Value;
            }
            else
            {
                objEntity1.strProposedprodDocCodePre = null;
                objEntity1.strProposedprodPre = null;
            }
            if (hdnpresentstageimplemnt_name.Value != "")
            {
                objEntity1.strPresentStageImplentDocCodePre = hdnpresentstageimplemnt_code.Value;
                objEntity1.strPresentStageImplentPre = hdnpresentstageimplemnt_name.Value;
            }
            else
            {
                objEntity1.strPresentStageImplentDocCodePre = null;
                objEntity1.strPresentStageImplentPre = null;
            }
            if (hdnmigrantindustrial_name.Value != "")
            {
                objEntity1.strMigrantIndustryunitDocCodePre = hdnmigrantindustrial_code.Value;
                objEntity1.strMigrantIndustryunitPre = hdnmigrantindustrial_name.Value;
            }
            else
            {
                objEntity1.strMigrantIndustryunitDocCodePre = null;
                objEntity1.strMigrantIndustryunitPre = null;
            }
            if (hdnfixedcapitalinvst_name.Value != "")
            {
                objEntity1.strfixedcapitalinvstDocCodePre = hdnfixedcapitalinvst_code.Value;
                objEntity1.strfixedcapitalinvstPre = hdnfixedcapitalinvst_name.Value;
            }
            else
            {
                objEntity1.strfixedcapitalinvstDocCodePre = null;
                objEntity1.strfixedcapitalinvstPre = null;
            }
            if (hdncatagoryfalpriority_name.Value != "")
            {
                objEntity1.strPriorityorThrustsectorDocCodePre = hdncatagoryfalpriority_code.Value;
                objEntity1.strPriorityorThrustsectorPre = hdncatagoryfalpriority_name.Value;
            }
            else
            {
                objEntity1.strPriorityorThrustsectorDocCodePre = null;
                objEntity1.strPriorityorThrustsectorPre = null;
            }
            /*--------------------------Pre-Production Document Section End-----------------------------------------------*/

            /*--------------------------Post-Production Document Section Start-----------------------------------------------*/
            if (Powerattpost_name.Value != "")
            {
                objEntity1.strPworofAttorneyPostDocCode = hdnPowerattpost_code.Value;
                objEntity1.strPworofAttorneyPost = Powerattpost_name.Value;
            }
            else
            {
                objEntity1.strPworofAttorneyPostDocCode = null;
                objEntity1.strPworofAttorneyPost = null;
            }
            if (hdnpporthrustcertificate_name.Value != "")
            {
                objEntity1.strPPorThrustStatusCertPostDocCode = hdnpporthrustcertificate_code.Value;
                objEntity1.strPPorThrustStatusCertPost = hdnpporthrustcertificate_name.Value;
            }
            else
            {
                objEntity1.strPPorThrustStatusCertPostDocCode = null;
                objEntity1.strPPorThrustStatusCertPost = null;
            }
            if (hdncertofregpost_name.Value != "")
            {
                objEntity1.strCertificateofregistrationDocCodepost = hdncertofregpost_code.Value;
                objEntity1.strCertificateofregistrationpost = hdncertofregpost_name.Value;
            }
            else
            {
                objEntity1.strCertificateofregistrationDocCodepost = null;
                objEntity1.strCertificateofregistrationpost = null;
            }
            if (hdnAppDPRpost_name.Value != "")
            {
                objEntity1.strApproveDPRDocCodePost = hdnAppDPRpost_code.Value;
                objEntity1.strApproveDPRPost = hdnAppDPRpost_name.Value;
            }
            else
            {
                objEntity1.strApproveDPRDocCodePost = null;
                objEntity1.strApproveDPRPost = null;
            }
            if (hdnPCorEINPost_name.Value != "")
            {
                objEntity1.strPcDocCodePost = hdnPCorEINPost_code.Value;
                objEntity1.strPcPost = hdnPCorEINPost_name.Value;
            }
            else
            {
                objEntity1.strPcDocCodePost = null;
                objEntity1.strPcPost = null;
            }
            if (hdnloansancorFIappliedpost_name.Value != "")
            {
                objEntity1.strSanctionbankorFIDocCodePost = hdnloansancorFIappliedpost_code.Value;
                objEntity1.strSanctionbankorFIPost = hdnloansancorFIappliedpost_name.Value;
            }
            else
            {
                objEntity1.strSanctionbankorFIDocCodePost = null;
                objEntity1.strSanctionbankorFIPost = null;
            }
            if (hdnCapitalInvstPost_name.Value != "")
            {
                objEntity1.strCapitalInvstDocCodePost = hdnCapitalInvstPost_code.Value;
                objEntity1.strCapitalInvstPost = hdnCapitalInvstPost_name.Value;
            }
            else
            {
                objEntity1.strCapitalInvstDocCodePost = null;
                objEntity1.strCapitalInvstPost = null;
            }
            if (hdnflInvplantmachinaryPost_name.Value != "")
            {
                objEntity1.strInvestmentplantmachinaryDocCodePost = hdnflInvplantmachinaryPost_code.Value;
                objEntity1.strInvestmentplantmachinaryPost = hdnflInvplantmachinaryPost_name.Value;
            }
            else
            {
                objEntity1.strInvestmentplantmachinaryDocCodePost = null;
                objEntity1.strInvestmentplantmachinaryPost = null;
            }
            if (hdnPlantmachinerypost_name.Value != "")
            {
                objEntity1.strBalacingEquipmentDocCodePost = hdnPlantmachinerypost_code.Value;
                objEntity1.strBalacingEquipmentPost = hdnPlantmachinerypost_name.Value;
            }
            else
            {
                objEntity1.strBalacingEquipmentDocCodePost = null;
                objEntity1.strBalacingEquipmentPost = null;
            }
            if (hdnproductionormanufactpost_name.Value != "")
            {
                objEntity1.strServiceProvideDocCodePost = hdnproductionormanufactpost_code.Value;
                objEntity1.strServiceProvidePost = hdnproductionormanufactpost_name.Value;
            }
            else
            {
                objEntity1.strServiceProvideDocCodePost = null;
                objEntity1.strServiceProvidePost = null;
            }
            if (hdncatagoryfalprioritypost_name.Value != "")
            {
                objEntity1.strPriorityorThrustsectorDocCodePost = hdncatagoryfalprioritypost_code.Value;
                objEntity1.strPriorityorThrustsectorPost = hdncatagoryfalprioritypost_name.Value;
            }
            else
            {
                objEntity1.strPriorityorThrustsectorDocCodePost = null;
                objEntity1.strPriorityorThrustsectorPost = null;
            }
            if (hdnclearancefromPCB_name.Value != "")
            {
                objEntity1.strClearancefromPCBDocCodePost = hdnclearancefromPCB_code.Value;
                objEntity1.strClearancefromPCBPost = hdnclearancefromPCB_name.Value;
            }
            else
            {
                objEntity1.strClearancefromPCBDocCodePost = null;
                objEntity1.strClearancefromPCBPost = null;
            }

            if (hdnmigratedindustunitpost_name.Value != "")
            {
                objEntity1.strMigrantIndustryunitDocCodePost = hdnmigratedindustunitpost_code.Value;
                objEntity1.strMigrantIndustryunitPost = hdnmigratedindustunitpost_name.Value;
            }
            else
            {
                objEntity1.strMigrantIndustryunitDocCodePost = null;
                objEntity1.strMigrantIndustryunitPost = null;
            }
            if (hdnproductionforMSMEPost_name.Value != "")
            {
                objEntity1.strfixedcapitalinvstDocCodePost = hdnproductionforMSMEPost_code.Value;
                objEntity1.strfixedcapitalinvstPost = hdnproductionforMSMEPost_name.Value;
            }
            else
            {
                objEntity1.strfixedcapitalinvstDocCodePost = null;
                objEntity1.strfixedcapitalinvstPost = null;
            }
            if (hdnEmpoweredcommitpost_name.Value != "")
            {
                objEntity1.strEmpoweredCommitteeDocCodePost = hdnEmpoweredcommitpost_code.Value;
                objEntity1.strEmpoweredCommitteePost = hdnEmpoweredcommitpost_name.Value;
            }
            else
            {
                objEntity1.strEmpoweredCommitteeDocCodePost = null;
                objEntity1.strEmpoweredCommitteePost = null;
            }
            /*--------------------------Post-Production Document Section End-----------------------------------------------*/


            /*-------------------------Other Document End---------------------------------*/


            #endregion

            objEntity1.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
           
            fillData();
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

            /*---------------------------------------------------------------*/
            /////// Data Insert and Update

            string strReturnStatus = objBAL.Thrust_Priority_AED(objEntity1);
            if (strReturnStatus == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = './incentiveoffered.aspx';}); </script>", false);
                return;
            }       
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something Went wrong.</strong>');", true);
                return;
            }
            ModalPopupExtender1.Show();

            ///*---------------------------------------------------------------*/

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            objBAL = null;
            objEntity1 = null;
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
        catch(Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void LnkBtn_Add_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkUPowerattpre.ID))
            {
                if (flPowerattpre.HasFile)
                {

                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PoweofAttorneyPre";
                    UploadDocument(flPowerattpre, hdnPowerattpre_name, strFileName, hypPowerattpre, lblPowerattpre, lnkDPowerattpre, "InctBasicDoc");
                }
                
            }
            else if (string.Equals(lnk.ID, lnkUcertofreg.ID))
            {
                if (flcertofreg.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_MEMORANDUMPRE";
                    UploadDocument(flcertofreg, certofreg_name, strFileName, hypVwcertofreg, lblcertofreg, lnkDcertofreg, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUAppDPR.ID))
            {
                if (flAppDPR.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_DPRPRE";
                    UploadDocument(flAppDPR, hdnAppDPR_Name, strFileName, hypVwAppDPR, lblAppDPR, lnkDAppDPR, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUEIN.ID))
            {
                if (flEIN.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_EINPRE";
                    UploadDocument(flEIN, hdnEIN_Name, strFileName, hypVwEIN, lblEIN, lnkDEIN, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUPlantmachinery.ID))
            {
                if (flPlantmachinery.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_Plant&MachineryPre";
                    UploadDocument(flPlantmachinery, hdnPlantmachinery_Name, strFileName, hyVwPlantmachinery, lblPlantmachinery, lnkDPlantmachinery, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUCapitalInvst.ID))
            {
                if (flCapitalInvst.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_CapitalInvestmentPre";
                    UploadDocument(flCapitalInvst, hdnCapitalInvst_Name, strFileName, hypVwCapitalInvst, lblCapitalInvst, lnkDCapitalInvst, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUInvplantmachinary.ID))
            {
                if (flInvplantmachinary.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_IvtPlant&machineryPre";
                    UploadDocument(flInvplantmachinary, hdnInvplantmachinary_name, strFileName, hypVwInvplantmachinary, lblInvplantmachinary, lnkDInvplantmachinary, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUproposedprod.ID))
            {
                if (flproposedprod.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ProposedprodPre";
                    UploadDocument(flproposedprod, hdnproposedprod_name, strFileName, hypVwproposedprod, lblproposedprod, lnkDproposedprod, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUpresentstageimplemnt.ID))
            {
                if (flpresentstageimplemnt.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PresentStageImplementationPre";
                    UploadDocument(flpresentstageimplemnt, hdnpresentstageimplemnt_name, strFileName, hypVwpresentstageimplemnt, lblpresentstageimplemnt, lnkDpresentstageimplemnt, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUmigrantindustrial.ID))
            {
                if (flmigrantindustrial.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_MigrantindustrialPre";
                    UploadDocument(flmigrantindustrial, hdnmigrantindustrial_name, strFileName, hypvwmigrantindustrial, lblmigrantindustrial, lnkDmigrantindustrial, "InctBasicDoc");
                }
            }

            else if (string.Equals(lnk.ID, lnkUfixedcapitalinvst.ID))
            {
                if (flfixedcapitalinvst.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_FixedcapitalinvstPre";
                    UploadDocument(flfixedcapitalinvst, hdnfixedcapitalinvst_name, strFileName, hypvwfixedcapitalinvst, lblfixedcapitalinvst, lnkDfixedcapitalinvst, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUcatagoryfalpriority.ID))
            {
                if (flcatagoryfalpriority.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_CatagoryfalpriorityPre";
                    UploadDocument(flcatagoryfalpriority, hdncatagoryfalpriority_name, strFileName, hypvwcatagoryfalpriority, lblcatagoryfalpriority, lnkDcatagoryfalpriority, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUPowerattpost.ID))
            {
                if (flPowerattpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PoweofAttorneypost";
                    UploadDocument(flPowerattpost, Powerattpost_name, strFileName, hypvwPowerattpost, lblPowerattpost, lnkDPowerattpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUpporthrustcertificate.ID))
            {
                if (flpporthrustcertificate.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "PPorthrustcertificatePost";
                    UploadDocument(flpporthrustcertificate, hdnpporthrustcertificate_name, strFileName, hypvwpporthrustcertificate, lblpporthrustcertificate, lnkDpporthrustcertificate, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUcertofregpost.ID))
            {
                if (flcertofregpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Certofregpost";
                    UploadDocument(flcertofregpost, hdncertofregpost_name, strFileName, hypvwcertofregpost, lblcertofregpost, lnkDcertofregpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUAppDPRpost.ID))
            {
                if (flAppDPRpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "AppDPRpost";
                    UploadDocument(flAppDPRpost, hdnAppDPRpost_name, strFileName, hypvwAppDPRpost, lblAppDPRpost, lnkDAppDPRpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUPCorEINPost.ID))
            {
                if (flPCorEINPost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "PCorEINPost";
                    UploadDocument(flPCorEINPost, hdnPCorEINPost_name, strFileName, hypvwPCorEINPost, lblPCorEINPost, lnkDPCorEINPost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUloansancorFIappliedpost.ID))
            {
                if (flloansancorFIappliedpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "LoansancorFIappliedpost";
                    UploadDocument(flloansancorFIappliedpost, hdnloansancorFIappliedpost_name, strFileName, hypvwloansancorFIappliedpost, lblloansancorFIappliedpost, lnkDloansancorFIappliedpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUCapitalInvstPost.ID))
            {
                if (flCapitalInvstPost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "CapitalInvstPost";
                    UploadDocument(flCapitalInvstPost, hdnCapitalInvstPost_name, strFileName, hypvwCapitalInvstPost, lblCapitalInvstPost, lnkDCapitalInvstPost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUInvplantmachinaryPost.ID))
            {
                if (flInvplantmachinaryPost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "InvplantmachinaryPost";
                    UploadDocument(flInvplantmachinaryPost, hdnflInvplantmachinaryPost_name, strFileName, hypvwInvplantmachinaryPost, lblInvplantmachinaryPost, lnkDInvplantmachinaryPost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUPlantmachinerypost.ID))
            {
                if (flPlantmachinerypost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Plantmachinerypost";
                    UploadDocument(flPlantmachinerypost, hdnPlantmachinerypost_name, strFileName, hypvwPlantmachinerypost, lblPlantmachinerypost, lnkDPlantmachinerypost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUproductionormanufactpost.ID))
            {
                if (flproductionormanufactpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Productionormanufactpost";
                    UploadDocument(flproductionormanufactpost, hdnproductionormanufactpost_name, strFileName, hypvwproductionormanufactpost, lblproductionormanufactpost, lnkDproductionormanufactpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUcatagoryfalprioritypost.ID))
            {
                if (flcatagoryfalprioritypost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Catagoryfalprioritypost";
                    UploadDocument(flcatagoryfalprioritypost, hdncatagoryfalprioritypost_name, strFileName, hypvwcatagoryfalprioritypost, lblcatagoryfalprioritypost, lnkDcatagoryfalprioritypost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUclearancefromPCB.ID))
            {
                if (flclearancefromPCB.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "ClearancefromPCBpost";
                    UploadDocument(flclearancefromPCB, hdnclearancefromPCB_name, strFileName, hypvwclearancefromPCB, lblclearancefromPCB, lnkDclearancefromPCB, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUmigratedindustunitpost.ID))
            {
                if (flmigratedindustunitpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Migratedindustunitpost";
                    UploadDocument(flmigratedindustunitpost, hdnmigratedindustunitpost_name, strFileName, hypvwmigratedindustunitpost, lblmigratedindustunitpost, lnkDmigratedindustunitpost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUproductionforMSMEPost.ID))
            {
                if (flproductionforMSMEPost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "ProductionforMSMEPost";
                    UploadDocument(flproductionforMSMEPost, hdnproductionforMSMEPost_name, strFileName, hypvwproductionforMSMEPost, lblproductionforMSMEPost, lnkDproductionforMSMEPost, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUEmpoweredcommitpost.ID))
            {
                if (flEmpoweredcommitpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "Empoweredcommitpost";
                    UploadDocument(flEmpoweredcommitpost, hdnEmpoweredcommitpost_name, strFileName, hypvwEmpoweredcommitpost, lblEmpoweredcommitpost, lnkDEmpoweredcommitpost, "InctBasicDoc");
                }
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
            if (string.Equals(lnk.ID, lnkDPowerattpre.ID))
            {
                UpdFileRemove(hdnPowerattpre_name, lnkUPowerattpre, lnkDPowerattpre, hypPowerattpre, lblPowerattpre, flPowerattpre, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDcertofreg.ID))
            {
                UpdFileRemove(certofreg_name, lnkUcertofreg, lnkDcertofreg, hypVwcertofreg, lblcertofreg, flcertofreg, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDAppDPR.ID))
            {
                UpdFileRemove(hdnAppDPR_Name, lnkUAppDPR, lnkDAppDPR, hypVwAppDPR, lblAppDPR, flAppDPR, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDEIN.ID))
            {
                UpdFileRemove(hdnEIN_Name, lnkUEIN, lnkDEIN, hypVwEIN, lblEIN, flEIN, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDPlantmachinery.ID))
            {
                UpdFileRemove(hdnPlantmachinery_Name, lnkUPlantmachinery, lnkDPlantmachinery, hyVwPlantmachinery, lblPlantmachinery, flPlantmachinery, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDCapitalInvst.ID))
            {
                UpdFileRemove(hdnCapitalInvst_Name, lnkUCapitalInvst, lnkDCapitalInvst, hypVwCapitalInvst, lblCapitalInvst, flCapitalInvst, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDInvplantmachinary.ID))
            {
                UpdFileRemove(hdnInvplantmachinary_name, lnkUInvplantmachinary, lnkDInvplantmachinary, hypVwInvplantmachinary, lblInvplantmachinary, flInvplantmachinary, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDproposedprod.ID))
            {
                UpdFileRemove(hdnproposedprod_name, lnkUproposedprod, lnkDproposedprod, hypVwproposedprod, lblproposedprod, flproposedprod, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDpresentstageimplemnt.ID))
            {
                UpdFileRemove(hdnpresentstageimplemnt_name, lnkUpresentstageimplemnt, lnkDpresentstageimplemnt, hypVwpresentstageimplemnt, lblpresentstageimplemnt, flpresentstageimplemnt, "InctBasicDoc");
            }

            else if (string.Equals(lnk.ID, lnkDmigrantindustrial.ID))
            {
                UpdFileRemove(hdnmigrantindustrial_name, lnkUmigrantindustrial, lnkDmigrantindustrial, hypvwmigrantindustrial, lblmigrantindustrial, flpresentstageimplemnt, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDfixedcapitalinvst.ID))
            {
                UpdFileRemove(hdnfixedcapitalinvst_name, lnkUfixedcapitalinvst, lnkDfixedcapitalinvst, hypvwfixedcapitalinvst, lblfixedcapitalinvst, flfixedcapitalinvst, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDcatagoryfalpriority.ID))
            {
                UpdFileRemove(hdncatagoryfalpriority_name, lnkUcatagoryfalpriority, lnkDcatagoryfalpriority, hypvwcatagoryfalpriority, lblcatagoryfalpriority, flcatagoryfalpriority, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDPowerattpost.ID))
            {
                UpdFileRemove(Powerattpost_name, lnkUPowerattpost, lnkDPowerattpost, hypvwPowerattpost, lblPowerattpost, flPowerattpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDpporthrustcertificate.ID))
            {
                UpdFileRemove(hdnpporthrustcertificate_name, lnkUpporthrustcertificate, lnkDpporthrustcertificate, hypvwpporthrustcertificate, lblpporthrustcertificate, flpporthrustcertificate, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDcertofregpost.ID))
            {
                UpdFileRemove(hdncertofregpost_name, lnkUcertofregpost, lnkDcertofregpost, hypvwcertofregpost, lblcertofregpost, flcertofregpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDAppDPRpost.ID))
            {
                UpdFileRemove(hdnAppDPRpost_name, lnkUAppDPRpost, lnkDAppDPRpost, hypvwAppDPRpost, lblAppDPRpost, flAppDPRpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDPCorEINPost.ID))
            {
                UpdFileRemove(hdnPCorEINPost_name, lnkUPCorEINPost, lnkDPCorEINPost, hypvwPCorEINPost, lblPCorEINPost, flPCorEINPost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDloansancorFIappliedpost.ID))
            {
                UpdFileRemove(hdnloansancorFIappliedpost_name, lnkUloansancorFIappliedpost, lnkDloansancorFIappliedpost, hypvwloansancorFIappliedpost, lblloansancorFIappliedpost, flloansancorFIappliedpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDCapitalInvstPost.ID))
            {
                UpdFileRemove(hdnCapitalInvstPost_name, lnkUCapitalInvstPost, lnkDCapitalInvstPost, hypvwCapitalInvstPost, lblCapitalInvstPost, flCapitalInvstPost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDInvplantmachinaryPost.ID))
            {
                UpdFileRemove(hdnflInvplantmachinaryPost_name, lnkUInvplantmachinaryPost, lnkDInvplantmachinaryPost, hypvwInvplantmachinaryPost, lblInvplantmachinaryPost, flInvplantmachinaryPost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDPlantmachinerypost.ID))
            {
                UpdFileRemove(hdnPlantmachinerypost_name, lnkUPlantmachinerypost, lnkDPlantmachinerypost, hypvwPlantmachinerypost, lblPlantmachinerypost, flPlantmachinerypost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDproductionormanufactpost.ID))
            {
                UpdFileRemove(hdnproductionormanufactpost_name, lnkUproductionormanufactpost, lnkDproductionormanufactpost, hypvwproductionormanufactpost, lblproductionormanufactpost, flproductionormanufactpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDcatagoryfalprioritypost.ID))
            {
                UpdFileRemove(hdncatagoryfalprioritypost_name, lnkUcatagoryfalprioritypost, lnkDcatagoryfalprioritypost, hypvwcatagoryfalprioritypost, lblcatagoryfalprioritypost, flcatagoryfalprioritypost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDclearancefromPCB.ID))
            {
                UpdFileRemove(hdnclearancefromPCB_name, lnkUclearancefromPCB, lnkDclearancefromPCB, hypvwclearancefromPCB, lblclearancefromPCB, flclearancefromPCB, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDmigratedindustunitpost.ID))
            {
                UpdFileRemove(hdnmigratedindustunitpost_name, lnkUmigratedindustunitpost, lnkDmigratedindustunitpost, hypvwmigratedindustunitpost, lblmigratedindustunitpost, flmigratedindustunitpost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDproductionforMSMEPost.ID))
            {
                UpdFileRemove(hdnproductionforMSMEPost_name, lnkUproductionforMSMEPost, lnkDproductionforMSMEPost, hypvwproductionforMSMEPost, lblproductionforMSMEPost, flproductionforMSMEPost, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDEmpoweredcommitpost.ID))
            {
                UpdFileRemove(hdnEmpoweredcommitpost_name, lnkUEmpoweredcommitpost, lnkDEmpoweredcommitpost, hypvwEmpoweredcommitpost, lblEmpoweredcommitpost, flEmpoweredcommitpost, "InctBasicDoc");
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
        DataSet ds = new DataSet();

        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity1 = new Basic_Unit_Details_Entity();

        try
        {
            #region Production Item Before

            /*---------------------------------------------------------------------*/
            ////// Add Production Item Before

            BasicProductionItemBefore objProdBefore = new BasicProductionItemBefore();
            List<BasicProductionItemBefore> listProdBefore = new List<BasicProductionItemBefore>();

            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                BasicProductionItemBefore objItem = new BasicProductionItemBefore();

                Label lblProductionName = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                Label lblQuantity = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                Label lblUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
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
            ////// Add Term Loan

            BasicTermLoan objTL = new BasicTermLoan();
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
            ////// Add Working Capital Loan

            BasicWorkingCapitalLoan objWC = new BasicWorkingCapitalLoan();
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
            /////// Industrial Unit Details Section

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
            ////// Production and Employment Details Section
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
            ////// Investment Details Section
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
            ///// Investment Details (After)
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

            /*----------------------------------------------------------*/
            ////// Means of Finance Section

            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text == "" ? "0" : Txt_Equity_Amt.Text);
            objEntity1.intProjectClearance = Convert.ToInt32(Rad_Clearnce_PCB.SelectedValue);
            objEntity1.intProvisnalPriorityThrustStatus = Convert.ToInt32(Rad_PP_thrust_Status.SelectedValue);
            objEntity1.intIPRinctiveAvel = Convert.ToInt32(Rad_IPR_Incentive_avail.SelectedValue);
            objEntity1.strClearnceswm = Txt_Swm_approve.Text;

            #endregion

            #region OtherDocumentSection

            /*---------------------------------------------------------------------*/
            /////// Other Document Section

            /*----------------------------------------------------------*/
            /*--------------------------Pre-Production Document Section Start-----------------------------------------------*/
            if (hdnPowerattpre_name.Value != "")
            {
                objEntity1.strPworofAttorneyPreDocCode = hdnPowerattpre_code.Value;
                objEntity1.strPworofAttorneyPre = hdnPowerattpre_name.Value;
            }
            else
            {
                objEntity1.strPworofAttorneyPreDocCode = null;
                objEntity1.strPworofAttorneyPre = null;
            }
            if (certofreg_name.Value != "")
            {
                objEntity1.strCertificateofregistrationDocCodepre = certofreg_code.Value;
                objEntity1.strCertificateofregistrationpre = certofreg_name.Value;
            }
            else
            {
                objEntity1.strCertificateofregistrationDocCodepre = null;
                objEntity1.strCertificateofregistrationpre = null;
            }
            if (hdnAppDPR_Name.Value != "")
            {
                objEntity1.strApproveDPRDocCodePre = hdnAppDPR_Code.Value;
                objEntity1.strApproveDPRPre = hdnAppDPR_Name.Value;
            }
            else
            {
                objEntity1.strApproveDPRDocCodePre = null;
                objEntity1.strApproveDPRPre = null;
            }
            if (hdnEIN_Name.Value != "")
            {
                objEntity1.strEINapprovalDocCodePre = hdnEIN_Code.Value;
                objEntity1.strEINapprovalPre = hdnEIN_Name.Value;
            }
            else
            {
                objEntity1.strEINapprovalDocCodePre = null;
                objEntity1.strEINapprovalPre = null;
            }
            if (hdnPlantmachinery_Name.Value != "")
            {
                objEntity1.strBalacingEquipmentDocCodePre = hdnPlantmachinery_Code.Value;
                objEntity1.strBalacingEquipmentPre = hdnPlantmachinery_Name.Value;
            }
            else
            {
                objEntity1.strBalacingEquipmentDocCodePre = null;
                objEntity1.strBalacingEquipmentPre = null;
            }
            if (hdnCapitalInvst_Name.Value != "")
            {
                objEntity1.strCapitalInvstDocCodePre = hdnCapitalInvst_Code.Value;
                objEntity1.strCapitalInvstPre = hdnCapitalInvst_Name.Value;
            }
            else
            {
                objEntity1.strCapitalInvstDocCodePre = null;
                objEntity1.strCapitalInvstPre = null;
            }
            if (hdnInvplantmachinary_name.Value != "")
            {
                objEntity1.strInvestmentplantmachinaryDocCodePre = hdnInvplantmachinary_code.Value;
                objEntity1.strInvestmentplantmachinaryPre = hdnInvplantmachinary_name.Value;
            }
            else
            {
                objEntity1.strInvestmentplantmachinaryDocCodePre = null;
                objEntity1.strInvestmentplantmachinaryPre = null;
            }
            if (hdnproposedprod_name.Value != "")
            {
                objEntity1.strProposedprodDocCodePre = hdnproposedprod_code.Value;
                objEntity1.strProposedprodPre = hdnproposedprod_name.Value;
            }
            else
            {
                objEntity1.strProposedprodDocCodePre = null;
                objEntity1.strProposedprodPre = null;
            }
            if (hdnpresentstageimplemnt_name.Value != "")
            {
                objEntity1.strPresentStageImplentDocCodePre = hdnpresentstageimplemnt_code.Value;
                objEntity1.strPresentStageImplentPre = hdnpresentstageimplemnt_name.Value;
            }
            else
            {
                objEntity1.strPresentStageImplentDocCodePre = null;
                objEntity1.strPresentStageImplentPre = null;
            }
            if (hdnmigrantindustrial_name.Value != "")
            {
                objEntity1.strMigrantIndustryunitDocCodePre = hdnmigrantindustrial_code.Value;
                objEntity1.strMigrantIndustryunitPre = hdnmigrantindustrial_name.Value;
            }
            else
            {
                objEntity1.strMigrantIndustryunitDocCodePre = null;
                objEntity1.strMigrantIndustryunitPre = null;
            }
            if (hdnfixedcapitalinvst_name.Value != "")
            {
                objEntity1.strfixedcapitalinvstDocCodePre = hdnfixedcapitalinvst_code.Value;
                objEntity1.strfixedcapitalinvstPre = hdnfixedcapitalinvst_name.Value;
            }
            else
            {
                objEntity1.strfixedcapitalinvstDocCodePre = null;
                objEntity1.strfixedcapitalinvstPre = null;
            }
            if (hdncatagoryfalpriority_name.Value != "")
            {
                objEntity1.strPriorityorThrustsectorDocCodePre = hdncatagoryfalpriority_code.Value;
                objEntity1.strPriorityorThrustsectorPre = hdncatagoryfalpriority_name.Value;
            }
            else
            {
                objEntity1.strPriorityorThrustsectorDocCodePre = null;
                objEntity1.strPriorityorThrustsectorPre = null;
            }
            /*--------------------------Pre-Production Document Section End-----------------------------------------------*/

            /*--------------------------Post-Production Document Section Start-----------------------------------------------*/
            if (Powerattpost_name.Value != "")
            {
                objEntity1.strPworofAttorneyPostDocCode = hdnPowerattpost_code.Value;
                objEntity1.strPworofAttorneyPost = Powerattpost_name.Value;
            }
            else
            {
                objEntity1.strPworofAttorneyPostDocCode = null;
                objEntity1.strPworofAttorneyPost = null;
            }
            if (hdnpporthrustcertificate_name.Value != "")
            {
                objEntity1.strPPorThrustStatusCertPostDocCode = hdnpporthrustcertificate_code.Value;
                objEntity1.strPPorThrustStatusCertPost = hdnpporthrustcertificate_name.Value;
            }
            else
            {
                objEntity1.strPPorThrustStatusCertPostDocCode = null;
                objEntity1.strPPorThrustStatusCertPost = null;
            }
            if (hdncertofregpost_name.Value != "")
            {
                objEntity1.strCertificateofregistrationDocCodepost = hdncertofregpost_code.Value;
                objEntity1.strCertificateofregistrationpost = hdncertofregpost_name.Value;
            }
            else
            {
                objEntity1.strCertificateofregistrationDocCodepost = null;
                objEntity1.strCertificateofregistrationpost = null;
            }
            if (hdnAppDPRpost_name.Value != "")
            {
                objEntity1.strApproveDPRDocCodePost = hdnAppDPRpost_code.Value;
                objEntity1.strApproveDPRPost = hdnAppDPRpost_name.Value;
            }
            else
            {
                objEntity1.strApproveDPRDocCodePost = null;
                objEntity1.strApproveDPRPost = null;
            }
            if (hdnPCorEINPost_name.Value != "")
            {
                objEntity1.strPcDocCodePost = hdnPCorEINPost_code.Value;
                objEntity1.strPcPost = hdnPCorEINPost_name.Value;
            }
            else
            {
                objEntity1.strPcDocCodePost = null;
                objEntity1.strPcPost = null;
            }
            if (hdnloansancorFIappliedpost_name.Value != "")
            {
                objEntity1.strSanctionbankorFIDocCodePost = hdnloansancorFIappliedpost_code.Value;
                objEntity1.strSanctionbankorFIPost = hdnloansancorFIappliedpost_name.Value;
            }
            else
            {
                objEntity1.strSanctionbankorFIDocCodePost = null;
                objEntity1.strSanctionbankorFIPost = null;
            }
            if (hdnCapitalInvstPost_name.Value != "")
            {
                objEntity1.strCapitalInvstDocCodePost = hdnCapitalInvstPost_code.Value;
                objEntity1.strCapitalInvstPost = hdnCapitalInvstPost_name.Value;
            }
            else
            {
                objEntity1.strCapitalInvstDocCodePost = null;
                objEntity1.strCapitalInvstPost = null;
            }
            if (hdnflInvplantmachinaryPost_name.Value != "")
            {
                objEntity1.strInvestmentplantmachinaryDocCodePost = hdnflInvplantmachinaryPost_code.Value;
                objEntity1.strInvestmentplantmachinaryPost = hdnflInvplantmachinaryPost_name.Value;
            }
            else
            {
                objEntity1.strInvestmentplantmachinaryDocCodePost = null;
                objEntity1.strInvestmentplantmachinaryPost = null;
            }
            if (hdnPlantmachinerypost_name.Value != "")
            {
                objEntity1.strBalacingEquipmentDocCodePost = hdnPlantmachinerypost_code.Value;
                objEntity1.strBalacingEquipmentPost = hdnPlantmachinerypost_name.Value;
            }
            else
            {
                objEntity1.strBalacingEquipmentDocCodePost = null;
                objEntity1.strBalacingEquipmentPost = null;
            }
            if (hdnproductionormanufactpost_name.Value != "")
            {
                objEntity1.strServiceProvideDocCodePost = hdnproductionormanufactpost_code.Value;
                objEntity1.strServiceProvidePost = hdnproductionormanufactpost_name.Value;
            }
            else
            {
                objEntity1.strServiceProvideDocCodePost = null;
                objEntity1.strServiceProvidePost = null;
            }
            if (hdncatagoryfalprioritypost_name.Value != "")
            {
                objEntity1.strPriorityorThrustsectorDocCodePost = hdncatagoryfalprioritypost_code.Value;
                objEntity1.strPriorityorThrustsectorPost = hdncatagoryfalprioritypost_name.Value;
            }
            else
            {
                objEntity1.strPriorityorThrustsectorDocCodePost = null;
                objEntity1.strPriorityorThrustsectorPost = null;
            }
            if (hdnclearancefromPCB_name.Value != "")
            {
                objEntity1.strClearancefromPCBDocCodePost = hdnclearancefromPCB_code.Value;
                objEntity1.strClearancefromPCBPost = hdnclearancefromPCB_name.Value;
            }
            else
            {
                objEntity1.strClearancefromPCBDocCodePost = null;
                objEntity1.strClearancefromPCBPost = null;
            }

            if (hdnmigratedindustunitpost_name.Value != "")
            {
                objEntity1.strMigrantIndustryunitDocCodePost = hdnmigratedindustunitpost_code.Value;
                objEntity1.strMigrantIndustryunitPost = hdnmigratedindustunitpost_name.Value;
            }
            else
            {
                objEntity1.strMigrantIndustryunitDocCodePost = null;
                objEntity1.strMigrantIndustryunitPost = null;
            }
            if (hdnproductionforMSMEPost_name.Value != "")
            {
                objEntity1.strfixedcapitalinvstDocCodePost = hdnproductionforMSMEPost_code.Value;
                objEntity1.strfixedcapitalinvstPost = hdnproductionforMSMEPost_name.Value;
            }
            else
            {
                objEntity1.strfixedcapitalinvstDocCodePost = null;
                objEntity1.strfixedcapitalinvstPost = null;
            }
            if (hdnEmpoweredcommitpost_name.Value != "")
            {
                objEntity1.strEmpoweredCommitteeDocCodePost = hdnEmpoweredcommitpost_code.Value;
                objEntity1.strEmpoweredCommitteePost = hdnEmpoweredcommitpost_name.Value;
            }
            else
            {
                objEntity1.strEmpoweredCommitteeDocCodePost = null;
                objEntity1.strEmpoweredCommitteePost = null;
            }
            /*--------------------------Post-Production Document Section End-----------------------------------------------*/


            /*-------------------------Other Document End---------------------------------*/


            #endregion
            fillData();
            objEntity1.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
           
            objEntity1.strPcNoAfter = Session["ProposalNo"].ToString();
            objEntity1.strIndustryCode = Session["UnitCode"].ToString();

            if (Request.QueryString["key"] != null)
            {
                objEntity1.strIncentiveNumber = Request.QueryString["key"].ToString();
              
            }
            else
            {
                objEntity1.strIncentiveNumber= Request.QueryString["IncentiveNo"].ToString();
            }

            /*---------------------------------------------------------------*/
            /////// Data Insert and Update

            string strReturnStatus = objBAL.Thrust_Priority_Draft(objEntity1);
            if (strReturnStatus == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Drafted Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = './incentiveoffered.aspx';}); </script>", false);
                return;
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something Went wrong.</strong>');", true);
                return;
            }          
            ModalPopupExtender1.Show();

            ///*---------------------------------------------------------------*/

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            objBAL = null;
            objEntity1 = null;
        }

    }

    
}


