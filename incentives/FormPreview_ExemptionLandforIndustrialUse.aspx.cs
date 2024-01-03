using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.Data;
public partial class incentives_FormPreview_ExemptionLandforIndustrialUse : System.Web.UI.Page
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
            DataSet dslivePre = IncentiveManager.ExemptionLandIndustrialUse_ViewData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            DataTable dtindustryPre = dslivePre.Tables[0];////////////Industrial Unit's Details
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment
            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////Term Loan Details
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////Working Capital Loan Details
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////Product Details
            DataTable dtDistrict = dslivePre.Tables[5];/////District
            ViewState["salutation"] = dtSalutation;

            lblGM.Text = dtDistrict.Rows[0]["vchDistrictName"].ToString().Trim();
            Label5.Text = dtDistrict.Rows[0]["vchDistrictName"].ToString().Trim();
            #region IndustrailUnit
            if (dtindustryPre.Rows.Count > 0)
            {

                lblMr.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString().Trim();
                lblAddress.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();
                lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();             
                lbl_Unit_Cat.Text = dtindustryPre.Rows[0]["Unitcategoryname"].ToString();
                lbl_ThrustorPriority_Sector.Text = dtindustryPre.Rows[0]["vchThrustPriorityStatus"].ToString();
                lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();
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
                lbl_propose_location.Text = dtindustryPre.Rows[0]["vchProposedLocation"].ToString();
                lbl_present_status.Text = dtindustryPre.Rows[0]["vchPrsentStatus"].ToString();
                lbl_Proposed_Date.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();

                /*--------------------------------------------------------------------------------------------------------------*/
                lbl_Date_fixed_capitalinv.Text = dtProductionPre.Rows[0]["dtmFFCIDateBefore"].ToString();
                lbl_Financer_Name.Text= dtProductionPre.Rows[0]["vchNameoffinancer"].ToString();
                lbl_cost_project.Text= dtProductionPre.Rows[0]["decCostofproject"].ToString();
                lbl_land_required.Text = dtProductionPre.Rows[0]["decAreaofLandAcquired"].ToString();
                lbl_land_acquired.Text= dtProductionPre.Rows[0]["decAreaofLandRequired"].ToString();
                lbl_land_converted.Text= dtProductionPre.Rows[0]["intParticularsLandtobeconverted"].ToString();
                if(lbl_land_converted.Text == "1")
                {
                    lbl_land_converted.Text = "Yes";
                    Div_Land_Converter.Visible = true;
                }
                else
                {
                    lbl_land_converted.Text = "No";
                    Div_Land_Converter.Visible = false;
                }
                

                #region Production

                Grd_Production_Before.DataSource = dtProductionDetBefPre;
                Grd_Production_Before.DataBind();

                #endregion
                Grd_Land_Converted.DataSource= dtProductionDetAftPre;
                Grd_Land_Converted.DataBind();

                if (dtInvestmentPre.Rows.Count > 0)
                {
                    GridView_CheckList.DataSource = dtInvestmentPre;
                    GridView_CheckList.DataBind();
                }
                /*----------------------------------------------------------------------------------------------------*/

            }
            #endregion

            int intActionToBeTakenBy = Convert.ToInt32(dtindustryPre.Rows[0]["intActionToBeTakenBy"]);
            int intActionTakenBy = Convert.ToInt32(dtindustryPre.Rows[0]["intActionTakenBy"]);

            if (intActionToBeTakenBy == 124 || intActionTakenBy == 124)
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
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
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
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
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
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }
    }
}