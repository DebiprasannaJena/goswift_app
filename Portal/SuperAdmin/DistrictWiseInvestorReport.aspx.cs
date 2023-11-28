using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_SuperAdmin_District_Wise_Investor_Report : System.Web.UI.Page
{
    int intRetVal = 0; 


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindDistrict();
                FillGrid();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "InvestorDetailReport");
            }

        }
    }

    ///Function for  District Bind
    private void BindDistrict() 
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();          
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            ddlDistrict.DataSource = objProjList;
            ddlDistrict.DataTextField = "vchDistName";
            ddlDistrict.DataValueField = "intDistId";
            ddlDistrict.DataBind();

            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlDistrict.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
// Function for Fill GridView to view District wise Investor Report
    private void FillGrid()
    {
        try
        {

            InvestorRptSearch objSearch = new InvestorRptSearch()
            {
                strActionCode = "DIR",
                intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue)
            };
           
            List<MIS_InvestorRpt> lstChildServices = MisReportServices.View_DistWise_Investor_Report(objSearch);
            GrdInvestor.DataSource = lstChildServices;
            GrdInvestor.DataBind();
            Grdforexcel.DataSource = lstChildServices;
            Grdforexcel.DataBind();
            string SearchDetails = string.Empty;
            SearchDetails = "Search Detail For : ";

            if (ddlDistrict.SelectedIndex == 0)
            {
              SearchDetails= SearchDetails + "District :- " + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + "District :- " + ddlDistrict.SelectedItem.Text;
                LblSearchDetails.Text = SearchDetails;
                intRetVal = lstChildServices.Count;
            }

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
    // Search by District id
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("DistrictWiseInvestorReport", Grdforexcel);
    }
    // Function for Excel Export
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        try
        {
            IncentiveCommonFunctions.ExportToExcel("DistrictWiseInvestorReport", Grdforexcel, " District Wise Investor Report",  "<br/>Report generation date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);


        }
        catch(Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    
    protected void GrdInvestor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdInvestor.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }
       
    }
    //// Function for Display Paging
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
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }

    }
    protected void LbtnAll_Click(object sender, EventArgs e)
    {
        try
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
        catch(Exception ex)
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
                Label Lbl_IndustryType = (Label)e.Row.FindControl("IndustryType");
                Label Lbl_InvLevel = (Label)e.Row.FindControl("InvLevel");
                if (HdnIndustryType.Value == "Industry")
                {
                    Lbl_IndustryType.ForeColor = System.Drawing.Color.Orange;
                    Lbl_InvLevel.ForeColor = System.Drawing.Color.Red;
                }
                else if (HdnIndustryType.Value == "Non Industry")
                {
                    Lbl_IndustryType.ForeColor = System.Drawing.Color.YellowGreen;
                    Lbl_InvLevel.ForeColor = System.Drawing.Color.Silver;
                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDetailReport");
        }

    }
   
}



