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

public partial class Portal_MISReport_ChildService_UserDtls : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonFunctions.PopulatePageSize(ddlNoOfRec);
            hdnPgindex.Value = "1";
            if (!string.IsNullOrEmpty(Request.QueryString["hdn"]))
            {
                hdnPgindex.Value = Request.QueryString["hdn"];
            }
            else
            {
                hdnPgindex.Value = "1";
            }
            if (Request.QueryString["pSize"] != null)
            {
                ddlNoOfRec.SelectedValue = Request.QueryString["pSize"];
            }
            else
            {
                ddlNoOfRec.SelectedValue = "10";
            }

            BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
    }

    #region Data Paging
    /// <summary>
    /// Click event for all the link button created for the paging control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Changed(object sender, EventArgs e)
    {
        try
        {
            hdnPgindex.Value = (string)((sender as LinkButton).CommandArgument);
            BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// selected index change event for the dropdown that contains different size for the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlNoOfRec_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdnPgindex.Value = "1";
            BindGridView(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion

    protected void grdService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Rowid = 0;
            if (Convert.ToInt32(hdnPgindex.Value) > 1)
            {
                Rowid = (Convert.ToInt32(hdnPgindex.Value) - 1) * Convert.ToInt32(ddlNoOfRec.SelectedValue) + e.Row.DataItemIndex + 1;
            }
            else
            {
                Rowid = e.Row.DataItemIndex + 1;
            }
            e.Row.Cells[0].Text = Rowid.ToString();
        }
    }

    private void BindGridView(int intPageIndex, int intPageSize)
    {
        lblSearchDetails.Text = string.Empty;
        divExport.Visible = false;
        grdService.DataSource = null;
        grdService.DataBind();
        string strFromDate= string.Empty,  strTodate= string.Empty;
        GetDefaultFromAndToDate(out strFromDate, out strTodate);
        RptSearch objSearch = new RptSearch()
        {
            strActionCode = "st",
            intPageSize = intPageSize,
            intIntPageIndex = intPageIndex,
            strToDate = string.IsNullOrEmpty(Request.QueryString["tDate"]) ? strTodate : Request.QueryString["tDate"],
            strFromDate = string.IsNullOrEmpty(Request.QueryString["fdate"]) ? strFromDate : Request.QueryString["fdate"],
            intDepartmentId = string.IsNullOrEmpty(Request.QueryString["dId"]) ? 0 : Convert.ToInt32(Request.QueryString["dId"]),
            intServiceId = string.IsNullOrEmpty(Request.QueryString["SId"]) ? 0 : Convert.ToInt32(Request.QueryString["SId"]),
            intUserId = string.IsNullOrEmpty(Request.QueryString["intId"]) ? 0 : Convert.ToInt32(Request.QueryString["intId"]),
            intStatus = string.IsNullOrEmpty(Request.QueryString["status"]) ? 0 : Convert.ToInt32(Request.QueryString["status"])
        };

        List<Mis_ChildServiceDtls> lstChildServices = new List<Mis_ChildServiceDtls>();
        lstChildServices = MisReportServices.View_ChildServices_UserWiseDetails_MISReport(objSearch);
        grdService.DataSource = lstChildServices;
        grdService.DataBind();
        if (grdService.Rows.Count > 0)
        {
            divExport.Visible = true;
            ddlNoOfRec.Visible = true;
            rptPager.Visible = true;
            CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(lstChildServices[0].intRowCount), Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));

            /****************code to show paging details in the label************/
            int intPIndex = Convert.ToInt32(hdnPgindex.Value);
            int intStartIndex = 1, intEndIndex = 0;
            int intPSize = Convert.ToInt32(ddlNoOfRec.SelectedValue);
            intStartIndex = ((intPIndex - 1) * intPSize) + 1;
            if (intPSize == grdService.Rows.Count)
            {
                intEndIndex = intPSize * intPIndex;
            }
            else
            {
                intEndIndex = grdService.Rows.Count + (intPSize * (intPIndex - 1));

            }
            lblDetails.Text = intStartIndex.ToString() + "-" + intEndIndex.ToString() + " of " + Convert.ToInt32(lstChildServices[0].intRowCount).ToString();
            StringBuilder strSearchDetails = new StringBuilder();
            if (!string.IsNullOrEmpty(Request.QueryString["intId"]) && Request.QueryString["intId"] != "0")
            {
                strSearchDetails.Append("<strong>User - </strong>");
                strSearchDetails.Append(lstChildServices[0].strUsername);
                strSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["dId"]) && Request.QueryString["dId"] != "0")
            {
                strSearchDetails.Append("<strong>Department - </strong>");
                strSearchDetails.Append(lstChildServices[0].strDepartment);
                strSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["SId"]) && Request.QueryString["SId"] != "0")
            {
                strSearchDetails.Append("<strong>Service - </strong>");
                strSearchDetails.Append(lstChildServices[0].ServiceName);
                strSearchDetails.Append("<br/>");
            }

            if (!string.IsNullOrEmpty(Request.QueryString["fDate"]))
            {
                strSearchDetails.Append("<strong>From Date - </strong>");
                strSearchDetails.Append(Convert.ToDateTime(Request.QueryString["fDate"]).ToString("dd-MMM-yyyy"));
                strSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["tDate"]))
            {
                strSearchDetails.Append("<strong>To Date - </strong>");
                strSearchDetails.Append(Convert.ToDateTime(Request.QueryString["tDate"]).ToString("dd-MMM-yyyy"));
                strSearchDetails.Append("<br/>");
            }

            lblSearchDetails.Text = strSearchDetails.ToString();
        }
        else
        {
            ddlNoOfRec.Visible = false;
            rptPager.Visible = false;
        }
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.ExportToExcel("ChildServicesDetailsRpt", grdService, "User wise report for Child Services", lblSearchDetails.Text + "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("ChildServicesDetailsRpt", grdService);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    private void GetDefaultFromAndToDate(out string strFromDate, out string strToDate)
    {
        strFromDate = string.Empty;
        strToDate = string.Empty;
        int intMonth =DateTime.Today.Month;
        if (intMonth == 1)
        {
            strFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
            strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
        else
        {
            strFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString().Substring(0, 3) + "-" + (DateTime.Today.Year).ToString();
            strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
    }
}