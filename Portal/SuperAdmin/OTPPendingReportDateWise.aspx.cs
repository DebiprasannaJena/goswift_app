using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;

public partial class Portal_SuperAdmin_OTPPendingReportDateWise_ : System.Web.UI.Page
{
    int intRetVal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
        if (!IsPostBack)
        {
            TxtFromdate.Attributes.Add("readonly", "readonly");
            TxtTodate.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);
        }
        

        }
        catch(Exception ex)
        {
            Util.LogError(ex, "OtpPendingReport");
        }

    }
    // Function for Fill GridView to view Otp Pending Status Report
    private void FillGrd()
    {
        try
        {
            string strFromDate = string.Empty;
            string strToDate = string.Empty;
            InvestorRptSearch objSearch = new InvestorRptSearch()
            {
                strActionCode = "OTPR",
                StrFromDate = string.IsNullOrEmpty(TxtFromdate.Text.Trim()) ? strFromDate : TxtFromdate.Text.Trim(),
                StrToDate = string.IsNullOrEmpty(TxtTodate.Text.Trim()) ? strToDate : TxtTodate.Text.Trim(),
            
            };
            
            List<MIS_InvestorRpt> lstChildServices = MisReportServices.View_Pending_Otp_Report(objSearch);
            GrdOtp.DataSource = lstChildServices;
            GrdOtp.DataBind();
            GridViewExcel.DataSource = lstChildServices;
            GridViewExcel.DataBind();
            string SearchDetails = string.Empty;
            SearchDetails = "Search Detail For : ";
            if (TxtFromdate.Text.Trim() == "")
            {
                SearchDetails = SearchDetails + " From Date :- " + strFromDate + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " From Date :- " + TxtFromdate.Text.Trim() + " | ";
            }
            if (TxtTodate.Text.Trim() == "")
            {
                SearchDetails = SearchDetails + " To Date :- " + strToDate + " | ";
            }
            else
            {
                SearchDetails = SearchDetails + " To Date :- " + TxtTodate.Text.Trim() + " | ";
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
    //// Function for Display Paging
    private void DisplayPaging()
    {
        try
        {
            if (this.GrdOtp.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                LbtnAll.Visible = true;
            }
            if (this.GrdOtp.PageIndex + 1 == this.GrdOtp.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GrdOtp.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)GrdOtp.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)GrdOtp.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((GrdOtp.PageSize - 1))) + "</b> of <b>" + intRetVal + "</b>";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OtpPendingReport");
        }

    }
    // Search by From Date and To Date
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {

            FillGrd();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OtpPendingReport");
        }

    }

    protected void LbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (LbtnAll.Text == "All")
            {
                LbtnAll.Text = "Paging";
                GrdOtp.PageIndex = 0;
                GrdOtp.AllowPaging = false;
                FillGrd();
            }
            else
            {
                LbtnAll.Text = "All";
                GrdOtp.AllowPaging = true;
                FillGrd();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OtpPendingReport");
        }
    }
    // Function for Excel Export
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        try
        {
            IncentiveCommonFunctions.ExportToExcel("OtpPendingStatusReport", GridViewExcel, " Otp Pending Status Report", "<br/>Report generation date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OtpPendingReport");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
       

    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("OtpPendingStatusReport", GridViewExcel);
    }

    protected void GrdOtp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdOtp.PageIndex = e.NewPageIndex;
            FillGrd();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OtpPendingReport");
        }
    }
}