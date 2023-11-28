using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Text;
using System.Globalization;

public partial class Portal_MISReport_InvestorDetails_Rpt : System.Web.UI.Page
{
    int intRetVal = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TxtFromDate.Attributes.Add("readonly", "readonly");
            TxtToDate.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);

            try
            {
                FillIndustry();
                BindDistrict();
                BindSector();
                FillGrid();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "InvestorDetailReport");
            }
            
        }
    }

    private void FillGrid()
    {
        try
        {


            /*---------------------------------------------------------------*/
            string strFromDate = string.Empty;
            string strToDate = string.Empty;
            int intMonth = DateTime.Today.Month;
            if (intMonth == 1)
            {
                strFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
                strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }
            else
            {
                strFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName((DateTime.Today.Month - 1)).ToString() + "-" + (DateTime.Today.Year).ToString();
                strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }

            /*---------------------------------------------------------------*/

            InvestorRptSearch objSearch = new InvestorRptSearch()
            {
                strActionCode = "ID",
                intDistrictId = Convert.ToInt32(DdlDistrict.SelectedValue),
                intBlockId = Convert.ToInt32(DdlBlock.SelectedValue),
                intSectorId = Convert.ToInt32(DdlSector.SelectedValue),
                intSubsectorId = Convert.ToInt32(DdlSubsector.SelectedValue),
                intCompanyId = Convert.ToInt32(string.IsNullOrEmpty(Convert.ToString(DdlCompany.SelectedValue)) ? "0" : Convert.ToString(DdlCompany.SelectedValue)),
                strPanNo = TxtPanNo.Text,
                IntIndustyType = Convert.ToInt32(DdlIndustryType.SelectedItem.Value),
                StrRegdSource = Convert.ToString(DdlRegdSource.SelectedValue),
                StrFromDate = string.IsNullOrEmpty(TxtFromDate.Text.Trim()) ? strFromDate : TxtFromDate.Text.Trim(),
                StrToDate   = string.IsNullOrEmpty(TxtToDate.Text.Trim()) ? strToDate : TxtToDate.Text.Trim(),
                StrLicenceNoType =Convert.ToString(DrpDwn_License_Type.SelectedItem.Text),
                IntCategory=Convert.ToInt32(DrpDwn_Invest_Level.SelectedItem.Value)

            };
           
            List<MIS_InvestorRpt> lstChildServices = MisReportServices.View_ChildServices_Investor_MISReport(objSearch);
            GrdExcel.DataSource = lstChildServices;
            GrdExcel.DataBind();
            GrdInvestor.DataSource = lstChildServices;
            GrdInvestor.DataBind();
            string SearchDetails = string.Empty;
            SearchDetails = "Search Detail For : ";
            if (DdlDistrict.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + "District :- " + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + "District :- " + DdlDistrict.SelectedItem.Text + " | ";
            }
            if (DdlBlock.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + " Block :- " + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " Block :- " + DdlBlock.SelectedItem.Text + " | ";
            }
            if (DdlSector.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + " Sector :- " + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " Sector :- " + DdlSector.SelectedItem.Text + " | ";
            }
            if (DdlSubsector.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + " Subsector :- " + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " Subsector :- " + DdlSubsector.SelectedItem.Text + " | ";
            }
            if (DdlCompany.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + " Company Name :- " + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " Company Name :- " + DdlCompany.SelectedItem.Text + " | ";
            }
            if (TxtPanNo.Text == "")
            {
                SearchDetails = SearchDetails + " Pan No :- " + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " Pan No :- " + TxtPanNo.Text.Trim() + " | ";
            }
            if (DdlIndustryType.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + " Industry Type :- " + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " Industry Type :- " + DdlIndustryType.SelectedItem.Text + " | ";
            }
            if (DdlRegdSource.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + " Registration Source :- " + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " Registration Source :- " + DdlRegdSource.SelectedItem.Text + " | ";
            }
            if (TxtFromDate.Text.Trim() == "")
            {
                SearchDetails = SearchDetails + " From Date :- " + strFromDate + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " From Date :- " + TxtFromDate.Text.Trim() + " | ";
            }
            if (TxtFromDate.Text.Trim() == "")
            {
                SearchDetails = SearchDetails + " To Date :- " + strToDate +" | "  ;
            }
            else
            {
                SearchDetails = SearchDetails + " To Date :- " + TxtToDate.Text.Trim() + " | ";
            }
            if ( DrpDwn_Invest_Level.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + " Investment Level :-" + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " Investment Level :-" + DrpDwn_Invest_Level.SelectedItem.Text+  " | ";
            }
            if (DrpDwn_License_Type.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + " EIN / IEM Number:-" + " All ";
            }
            else
            {
                SearchDetails = SearchDetails + " EIN / IEM Number:- " + DrpDwn_License_Type.SelectedItem.Text ;
            }

            LblSearchDetails.Text = SearchDetails;
            intRetVal = lstChildServices.Count;
            if (lstChildServices.Count > 0)
            {
                DisplayPaging();
            }
            else
            {
                lblPaging.Visible = false;
                LbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void FillIndustry()
    {
        try
        {

        
        IndustrySearch objSearch = new IndustrySearch()
        {
            strActionCode = "IL",
        };

        
        List<rtnIndustry> listIndustry = MisReportServices.View_Industry(objSearch);
        DdlCompany.DataSource = listIndustry;
        DdlCompany.DataTextField = "VCH_INV_NAME";
        DdlCompany.DataValueField = "INT_INVESTOR_ID";
        DdlCompany.DataBind();
        DdlCompany.Items.Insert(0, new ListItem("Select Industry", "0"));
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    private void BindDistrict()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();
           
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            DdlDistrict.DataSource = objProjList;
            DdlDistrict.DataTextField = "vchDistName";
            DdlDistrict.DataValueField = "intDistId";
            DdlDistrict.DataBind();

            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DdlDistrict.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void BindSector()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();            
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "SE";
            objProp.vchProposalNo = " ";

            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            DdlSector.DataSource = objProjList;
            DdlSector.DataTextField = "vchSectorName";
            DdlSector.DataValueField = "intSectorId";
            DdlSector.DataBind();

            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DdlSector.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
           throw  ex.InnerException;
        }
    }
    private void BindBlock(string district)
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();            
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "BL";
            objProp.vchProposalNo = district;

            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            DdlBlock.DataSource = objProjList;
            DdlBlock.DataTextField = "vchBlockName";
            DdlBlock.DataValueField = "intBlockId";
            DdlBlock.DataBind();

            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DdlBlock.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void BindSubSector(string sector)
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();
           
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "SU";
            objProp.vchProposalNo = sector;

            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            DdlSubsector.DataSource = objProjList;
            DdlSubsector.DataTextField = "vchSectorName";
            DdlSubsector.DataValueField = "intSectorId";
            DdlSubsector.DataBind();

            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DdlSubsector.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    protected void LbtnAll_Click(object sender, EventArgs e)
    {
        if (LbtnAll.Text == "All")
        {
            LbtnAll.Text = "Paging";
            GrdInvestor.PageIndex = 0;
            GrdInvestor.AllowPaging = false;
            FillGrid();
        }
        else
        {
            LbtnAll.Text = "All";
            GrdInvestor.AllowPaging = true;
            FillGrid();
        }
    }
    protected void GrdInvestor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdInvestor.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    private void DisplayPaging()
    {
        try
        {
            if (this.GrdInvestor.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                LbtnAll.Visible = true;
            }
            if (this.GrdInvestor.PageIndex + 1 == this.GrdInvestor.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GrdInvestor.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GrdInvestor.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)GrdInvestor.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((GrdInvestor.PageSize - 1))) + "</b> of <b>" + intRetVal + "</b>";
            }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }
        
    }

    protected void LnkExport_Click(object sender, EventArgs e)
    {
        try 
        { 
        IncentiveCommonFunctions.ExportToExcel("InvestorDetailRpt", GrdExcel, "Investor details report ", LblSearchDetails.Text + "<br/>Report generation date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        { 
        
        FillGrid();
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }
    }

    protected void DdlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {        
        BindBlock(DdlDistrict.SelectedValue);
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }
    }
    protected void DdlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSubSector(DdlSector.SelectedValue);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }
        
    }

    
    protected void GrdInvestor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIndustryType = (HiddenField)e.Row.FindControl("HdnIndustryType");
                Label Lbl_Industry_Type = (Label)e.Row.FindControl("Lbl_Industry_Type");
                if (HdnIndustryType.Value == "1")
                {
                    Lbl_Industry_Type.ForeColor = System.Drawing.Color.Orange;
                }
                else if (HdnIndustryType.Value == "2")
                {
                    Lbl_Industry_Type.ForeColor = System.Drawing.Color.YellowGreen;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }
       
    }
}