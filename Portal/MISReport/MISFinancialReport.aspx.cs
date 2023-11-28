using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_MISReport_MISFinancialReport : System.Web.UI.Page
{
    int intRecordCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);

            if (!IsPostBack)
           {
               if (Session["UserId"] != null)
               {
                   BindGridView();
               }
               else
               {
                   Response.Redirect("../SessionTimeout.aspx");
               }
           }

        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Financialreport");
        }
    }

    public void BindGridView()
    {
        try
        {
            grdFinancialReport.DataSource = null;
            grdFinancialReport.DataBind();


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
                strFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString() + "-" + (DateTime.Today.Year).ToString();
                strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
            }



            Financialreport objSearch = new Financialreport()
            {
                 strActionCode= "FN",
                 decFee = Convert.ToInt32(drpInvestmentAmount.SelectedItem.Value),
                 decInvestamount= Convert.ToDecimal(drpFees.SelectedItem.Value),
                 intStatus= Convert.ToInt32(ddlStatus.SelectedItem.Value),

                StrFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
                StrToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strToDate : txtToDate.Text.Trim()
            };
           


            List<Financialreport> listfinancial = MisReportServices.View_FinancilaReport(objSearch);

            grdFinancialReport.DataSource = listfinancial;
            grdFinancialReport.DataBind();

            if (grdFinancialReport.Rows.Count > 0)
            {

                GridViewRow gRowFooter = grdFinancialReport.FooterRow;
                Label lblTotal = (Label)gRowFooter.FindControl("lblTotal");
                lblTotal.Text = "Total";

                Label lbltotalproposal = (Label)gRowFooter.FindControl("lbltotalproposal");
                lbltotalproposal.Text = listfinancial.Count.ToString();


                Label lblfee = (Label)gRowFooter.FindControl("lblfee");
                lblfee.Text= listfinancial.Sum(x => x.decFee).ToString();

            }


            grdExcel.DataSource = listfinancial;
            grdExcel.DataBind();

            StringBuilder strSearchDetails = new StringBuilder();
            lblSearchDetails.Text = strSearchDetails.ToString();

            intRecordCount = listfinancial.Count;
            if (intRecordCount > 0)
            {
                DisplayPaging();
            }
            else
            {
                lblPaging.Visible = false;
            }
           
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string str = string.Empty;
            if (string.IsNullOrEmpty(txtFromDate.Text.Trim()))
            {
                str = "jAlert('<strong>Please select from date.</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else if (string.IsNullOrEmpty(txtToDate.Text.Trim()))
            {
                str = "jAlert('<strong>Please select to date.</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else if (Convert.ToDateTime(txtFromDate.Text.Trim()) > Convert.ToDateTime(txtToDate.Text.Trim()))
            {
                str = "jAlert('<strong>From date cannot be greater than to date.</strong>', 'GO-SWIFT');";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else
            {
                BindGridView();
            }

            
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Financialreport");
        }
    }

    protected void grdFinancialReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
      
        grdFinancialReport.PageIndex = e.NewPageIndex;
        BindGridView();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Financialreport");
        }
    }

    private void DisplayPaging()
    {
        if (grdFinancialReport.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (grdFinancialReport.PageIndex + 1 == grdFinancialReport.PageCount)
            {
                lblPaging.Text = "Results <b>" + ((Label)grdFinancialReport.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + intRecordCount + "</b> of <b>" + intRecordCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)grdFinancialReport.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)grdFinancialReport.Rows[0].FindControl("lblsl")).Text) + grdFinancialReport.PageSize - 1) + "</b> of <b>" + intRecordCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }


    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                grdFinancialReport.PageIndex = 0;
                grdFinancialReport.AllowPaging = false;
                BindGridView();
            }
            else
            {
                lbtnAll.Text = "All";
                grdFinancialReport.AllowPaging = true;
                BindGridView();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Financialreport");
        }
    }


    protected void lnkExport_Click(object sender, EventArgs e)
    {
        try
        {      
        IncentiveCommonFunctions.ExportToExcel("ProposalFinancialRpt", grdExcel, "Report on Proposal Financial", lblSearchDetails.Text + "<br/> As on date - " + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Financialreport");
        }
    }

}