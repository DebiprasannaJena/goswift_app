using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.Data;
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using DataAcessLayer.Common;
using System.Text;

public partial class incentives_FormPreview_GrantThrustorprioritysectorstatus : System.Web.UI.Page
{
    Incentive objIncentive = new Incentive();
    string intCreatedBy;
    DataTable dtSalutation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PostpopulateDataComm(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
          
            
        }

    }


    public void PostpopulateDataComm(int id)
    {
        try
        {
            DataSet dslivePre = IncentiveManager.ProvisionalThrustsectorpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            DataTable dtindustryPre = dslivePre.Tables[0];////////////Industrial Unit's Details
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment

            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////Term Loan Details
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////Working Capital Loan Details
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////Product Details
            DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////investment details
            DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Other Document List
            DataTable dtDistrict = dslivePre.Tables[8];
           
            ViewState["salutation"] = dtSalutation;

            lblGM.Text= dtDistrict.Rows[0]["vchDistrictName"].ToString().Trim();
            Label5.Text = dtDistrict.Rows[0]["vchDistrictName"].ToString().Trim();
            #region IndustrailUnit
            if (dtindustryPre.Rows.Count > 0)
            {

                lblMr.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString().Trim();
                
                lblAddress.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();               
                lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                lbl_Unit_Cat.Text = dtindustryPre.Rows[0]["Unitcategoryname"].ToString();
                lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                lbl_Regd_Office_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();
                lbl_Phone_No.Text= dtindustryPre.Rows[0]["vchPhoneNumber"].ToString();
                lbl_Email.Text= dtindustryPre.Rows[0]["vchEmail"].ToString();
                lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
                if (lbl_Org_Type.Text == "Proprietorship")
                {
                    Lbl_Org_Name_Type.Text = "Name of Proprietor";
                }
                else if (lbl_Org_Type.Text == "PARTNERSHIP")
                {
                    Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                }
                else if (lbl_Org_Type.Text == "PUBLIC LIMITED COMPANY")
                {
                    Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
                }
                else if (lbl_Org_Type.Text == "PVT. LTD. COMPANY")
                {
                    Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
                }
                else if (lbl_Org_Type.Text == "CO-OPERATIVE")
                {
                    Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
                }
                lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
                lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
/*---------------------------------------------------------------------------------------------------------------------------------*/
               
               
                #region Production

                Grd_Production_Before.DataSource = dtInvestmentPre;
                Grd_Production_Before.DataBind();

                #endregion

                lbl_First_Capital_Invst.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
                lbl_proposed_Date.Text = dtProductionPre.Rows[0]["dtmFFCIDateBefore"].ToString();
                lbl_Type_EIMorUAM.Text = dtMoFTermLoanPre.Rows[0]["intEIMorUAMtype"].ToString();
                if (lbl_Type_EIMorUAM.Text == "1")
                {
                    EIMorUAM.Visible = true;
                    lbl_Type_EIMorUAM.Text = "EIM";
                    lbl_PCorEm_No.Text = "Production certificate / EM-II No.";
                   lbl_PcoeEM_Date.Text = "Production certificate/EM-II Date";
                }
                else if (lbl_Type_EIMorUAM.Text == "2")
                {
                    EIMorUAM.Visible = true;
                    lbl_Type_EIMorUAM.Text = "UAM";
                    lbl_PCorEm_No.Text = "UAM no. for MSME";
                    lbl_PcoeEM_Date.Text = "UAM no. and Date for MSME";
                }
                else if (lbl_Type_EIMorUAM.Text == "3")
                {
                    lbl_Type_EIMorUAM.Text = "Other";
                    EIMorUAM.Visible = false;
                }

                lbl_PCorEm_Number.Text= dtMoFTermLoanPre.Rows[0]["vchEIMorUAMnumber"].ToString();
                lbl_PcoeEM_Dt.Text= dtMoFTermLoanPre.Rows[0]["dtmEIMorUAMdate"].ToString();
/*--------------------------------------------------------------------------------------------------------------------------*/

            }
            #endregion

            #region Investment
            lbl_toal_Emp_No.Text= dtMoFTermLoanPre.Rows[0]["intDirectEmpAfter"].ToString();
            lbl_Land_Before.Text = dtProductionPre.Rows[0]["decLandAmtBefore"].ToString();
            lbl_Building_Before.Text = dtProductionPre.Rows[0]["decBuildingAmtBefore"].ToString();
            lbl_Plant_Mach_Before.Text = dtProductionPre.Rows[0]["decPlantMachAmtBefore"].ToString();
            lbl_Other_Fixed_Asset_Before.Text = dtProductionPre.Rows[0]["decOtheFixedAssetAmtBefore"].ToString();
            lbl_Loading_bf.Text = dtProductionPre.Rows[0]["decLoadUnloadAmtBefore"].ToString();
            lbl_Margin_bf.Text = dtProductionPre.Rows[0]["decMarginMoneyForworkingAmtBefore"].ToString();
            lbl_Electrification_inst_Bf.Text = dtProductionPre.Rows[0]["decElectricalInstAmtBefore"].ToString();
            lbl_Total_Capital_Before.Text = dtProductionPre.Rows[0]["decTotalAmtBefore"].ToString();

            lbl_Land_After.Text = dtProductionPre.Rows[0]["decLandAmtAfter"].ToString();
            lbl_Building_After.Text = dtProductionPre.Rows[0]["decBuildingAmtAfter"].ToString();
            lbl_Plant_Mach_After.Text = dtProductionPre.Rows[0]["decPlantMachAmtAfter"].ToString();
            lbl_Other_Fixed_Asset_After.Text = dtProductionPre.Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
            lbl_Electrification_inst_After.Text= dtProductionPre.Rows[0]["decElectricalInstAmtAfter"].ToString();
            lbl_Margin_After.Text = dtProductionPre.Rows[0]["decMarginMoneyForworkingAmtAfter"].ToString();
            lbl_Loading_After.Text = dtProductionPre.Rows[0]["decLoadUnloadAmtAfter"].ToString();
            lbl_Total_Capital_After.Text = dtProductionPre.Rows[0]["decTotalAmtAfter"].ToString();

            #endregion

            #region MEANS OF FINANCE
            
           lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
           lbl_Project_Clearabce_PCB.Text= dtindustryPre.Rows[0]["intProjectClearance"].ToString();
            if (lbl_Project_Clearabce_PCB.Text == "1")
            {
                lbl_Project_Clearabce_PCB.Text = "Yes";
            }
            else
            {
                lbl_Project_Clearabce_PCB.Text = "No";
            }
           lbl_PP_Thruststatus.Text = dtindustryPre.Rows[0]["intProvisnalPriorityThrustStatus"].ToString();
            if (lbl_PP_Thruststatus.Text == "1")
            {
                lbl_PP_Thruststatus.Text = "Yes";
            }
            else
            {
                lbl_PP_Thruststatus.Text = "No";
            }
           lbl_Incetive_Avail.Text = dtindustryPre.Rows[0]["intIPRinctiveAvel"].ToString();
            if (lbl_Incetive_Avail.Text == "1")
            {
                lbl_Incetive_Avail.Text = "Yes";
            }
            else
            {
                lbl_Incetive_Avail.Text = "No";
            }
            lbl_Approval_SWM.Text= dtindustryPre.Rows[0]["vchClearnceswm"].ToString();
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
            if (dtMoFWorkingLoanPre.Rows.Count > 0)
            {
                GridView_CheckList.DataSource = dtMoFWorkingLoanPre;
                GridView_CheckList.DataBind();
            }
            #endregion

            
            int intActionToBeTakenBy =Convert.ToInt32( dtindustryPre.Rows[0]["intActionToBeTakenBy"]);
            int intActionTakenBy = Convert.ToInt32(dtindustryPre.Rows[0]["intActionTakenBy"]);

            if (intActionToBeTakenBy == 124 || intActionTakenBy== 124)
            {
                
                MGD.Visible = true;
            }
            else
            {
                GMD.Visible = true;
               
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }


    }

    private void SMSEMailContent()
    {
        try
        {
            CommonHelperCls objcomm = new CommonHelperCls();
            string strSubject = "GOSwift: Application submitted successfully";
            string PreviewURL = System.Configuration.ConfigurationManager.AppSettings["PreviewURL"];
            DataTable dtsal1 = (DataTable)ViewState["salutation"];

            string strBody = lblTitle.Text + " of M/s " + lblMr.Text + " has been submitted successfully. " + PreviewURL;
            string SMSContent = strSubject;

            var myList = new List<string>();
            myList.Add(dtsal1.Rows[0]["VCH_EMAIL"].ToString());
            string[] tomail = myList.ToArray();

            bool mailStatus = objcomm.sendMail(strSubject, strBody, tomail, true);
            bool smsStatus = objcomm.SendSmsNew(dtsal1.Rows[0]["VCH_OFF_MOBILE"].ToString(), SMSContent);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void GridView_CheckList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink Hy_Document_Doc = (e.Row.FindControl("Hy_Document_Doc") as HyperLink);

                HiddenField Hid_Document_File_Name = (e.Row.FindControl("Hid_Document_File_Name") as HiddenField);

                if (Hid_Document_File_Name.Value == "")
                {
                    Hy_Document_Doc.Visible = false;
                }
                else
                {
                    Hy_Document_Doc.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
    }

    protected void Grd_Production_Before_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField Hid_Unit_Type = (e.Row.FindControl("hdnUnitType") as HiddenField);
                Label Lbl_Status = (Label)e.Row.FindControl("Lbl_Unit_Before");
                if (Hid_Unit_Type.Value == "49")
                {
                    Lbl_Status.Text = "MT";
                }
                else if (Hid_Unit_Type.Value == "50")
                {
                    Lbl_Status.Text = "KG";
                }
                else if (Hid_Unit_Type.Value == "51")
                {
                    Lbl_Status.Text = "Litre";
                }
                else if (Hid_Unit_Type.Value == "52")
                {
                    Lbl_Status.Text = "Other";
                }
                else if (Hid_Unit_Type.Value == "64")
                {
                    Lbl_Status.Text = "MW";
                }
                else if (Hid_Unit_Type.Value == "65")
                {
                    Lbl_Status.Text = "No. of People";
                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }

    }
}