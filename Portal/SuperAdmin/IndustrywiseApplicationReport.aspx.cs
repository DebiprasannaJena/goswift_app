using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Globalization;

public partial class Portal_SuperAdmin_IndustrywiseApplicationReport : System.Web.UI.Page
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
                FillGrid();
                
               
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "IndustrywiseApplicationReport");
            }

            
        }

    }
    ///Function for  Industry Bind
    private void FillIndustry()
    {
        try
        {


            IndustrySearch objSearch = new IndustrySearch()
            {
                strActionCode = "IL",
            };

            List<rtnIndustry> listIndustry = new List<rtnIndustry>();
            listIndustry = MisReportServices.View_Industry(objSearch);

            DdlCompany.DataSource = listIndustry;
            DdlCompany.DataTextField = "VCH_INV_NAME";
            DdlCompany.DataValueField = "VCH_INV_NAME";
            DdlCompany.DataBind();
            DdlCompany.Items.Insert(0, new ListItem("--Select--", ""));
         
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    // Function for Fill GridView to view Industry wise Application Report
    private void FillGrid()
    {
        try
        {


            /*---------------------------------------------------------------*/
            string strFromDate = string.Empty;
            string strToDate = string.Empty;
           
            /*---------------------------------------------------------------*/

            InvestorRptSearch objSearch = new InvestorRptSearch()
            {
                strActionCode = "IAR",
                strInvestorName = Convert.ToString(string.IsNullOrEmpty(Convert.ToString(DdlCompany.SelectedValue)) ? "" : Convert.ToString(DdlCompany.SelectedValue)),
                StrFromDate = string.IsNullOrEmpty(TxtFromDate.Text.Trim()) ? strFromDate : TxtFromDate.Text.Trim(),
                StrToDate = string.IsNullOrEmpty(TxtToDate.Text.Trim()) ? strToDate : TxtToDate.Text.Trim(),
                IntCategory = Convert.ToInt32(DrpDwn_Invest_Level.SelectedItem.Value)

            };
            List<IndustrywiseApplicationReport> lstChildServices = new List<IndustrywiseApplicationReport>();

          // lstChildServices = MisReportServices.View_Industrywise_Application_Report(objSearch);
            GridView1.DataSource = lstChildServices;
            GridView1.DataBind();
            GrdExcel.DataSource = lstChildServices;
            GrdExcel.DataBind();
            string SearchDetails = string.Empty;
            SearchDetails = "Search Detail For : ";
           
           
            if (DdlCompany.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + " Company Name :- " + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " Company Name :- " + DdlCompany.SelectedItem.Text + " | ";
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
                SearchDetails = SearchDetails + " To Date :- " + strToDate + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " To Date :- " + TxtToDate.Text.Trim() + " | ";
            }
            if (DrpDwn_Invest_Level.SelectedIndex == 0)
            {
                SearchDetails = SearchDetails + " Investment Level :-" + " All" + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " Investment Level :-" + DrpDwn_Invest_Level.SelectedItem.Text + " | ";
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
                lbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //// Function for Display Paging
    private void DisplayPaging()
    {
        try
        {
            if (this.GridView1.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                lbtnAll.Visible = true;
            }
            if (this.GridView1.PageIndex + 1 == this.GridView1.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GridView1.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GridView1.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)GridView1.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((GridView1.PageSize - 1))) + "</b> of <b>" + intRetVal + "</b>";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IndustrywiseApplicationReport");
        }

    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
        
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IndustrywiseApplicationReport");
        }

    }
    // Function for Excel Export
    protected void LnkExport_Click(object sender, EventArgs e)
    {
        try
        {
            IncentiveCommonFunctions.ExportToExcel("Industrywiseapplicationreport", GrdExcel, "Industry wise application details report ", LblSearchDetails.Text + "<br/>Report generation date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IndustrywiseApplicationReport");
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IndustrywiseApplicationReport");
        }
    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                GridView1.PageIndex = 0;
                GridView1.AllowPaging = false;
                FillGrid();
            }
            else
            {
                lbtnAll.Text = "All";
                GridView1.AllowPaging = true;
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "IndustrywiseApplicationReport");
        }

    }
}
