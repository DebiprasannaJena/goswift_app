using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Globalization;

public partial class Portal_MISReport_ChildServiceQueryDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
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
        else
        {
            Response.Redirect("../SessionTimeout.aspx");
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
            Util.LogError(ex, "MIS Report");
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
            Util.LogError(ex, "MIS Report");
        }
    }
    #endregion

    protected void grdIncentive_RowDataBound(object sender, GridViewRowEventArgs e)
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
        grdDepartment.DataSource = null;
        grdDepartment.DataBind();
        divExport.Visible = false;
        string strFromDate= string.Empty,  strTodate= string.Empty;
        GetDefaultFromAndToDate(out strFromDate, out strTodate);
        RptSearch objSearch = new RptSearch()
        {
            strActionCode = Request.QueryString["Action"],
            intPageSize = intPageSize,
            intIntPageIndex = intPageIndex,
            strToDate = string.IsNullOrEmpty(Request.QueryString["tDate"]) ? strTodate : Request.QueryString["tDate"],
            strFromDate = string.IsNullOrEmpty(Request.QueryString["fdate"]) ? strFromDate : Request.QueryString["fdate"],
            intDepartmentId = string.IsNullOrEmpty(Request.QueryString["intId"]) ? 0 : Convert.ToInt32(Request.QueryString["intId"]),
            intDistrictId = string.IsNullOrEmpty(Request.QueryString["dist"]) ? 0 : Convert.ToInt32(Request.QueryString["dist"]),
            intServiceId = string.IsNullOrEmpty(Request.QueryString["sId"]) ? 0 : Convert.ToInt32(Request.QueryString["sId"])
        };

        List<Mis_ChildServiceDtls> lstChildServices = new List<Mis_ChildServiceDtls>();
        if (string.Equals(objSearch.strActionCode, "dq", StringComparison.OrdinalIgnoreCase))
        {
            lstChildServices = MisReportServices.View_ChildServices_District_Details_Rpt(objSearch);
        }
        else
        {
            lstChildServices = MisReportServices.View_DetailsChildServices_MISReport(objSearch);
        }
        grdDepartment.DataSource = lstChildServices;
        grdDepartment.DataBind();

        if (grdDepartment.Rows.Count > 0)
        {
            divExport.Visible = true;
            ddlNoOfRec.Visible = true;
            rptPager.Visible = true;
            CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(lstChildServices[0].intRowCount), Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));

            GridViewRow gRowFooter = grdDepartment.FooterRow;
            gRowFooter.Cells[1].Text = "Total";
            gRowFooter.Cells[6].Text = IncentiveCommonFunctions.FormatDecimalString(lstChildServices.Sum(x => x.decInvestment).ToString());
            gRowFooter.Cells[7].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intPropEmployment).ToString());

            /****************code to show paging details in the label************/
            int intPIndex = Convert.ToInt32(hdnPgindex.Value);
            int intStartIndex = 1, intEndIndex = 0;
            int intPSize = Convert.ToInt32(ddlNoOfRec.SelectedValue);
            intStartIndex = ((intPIndex - 1) * intPSize) + 1;
            if (intPSize == grdDepartment.Rows.Count)
            {
                intEndIndex = intPSize * intPIndex;
            }
            else
            {
                intEndIndex = grdDepartment.Rows.Count + (intPSize * (intPIndex - 1));

            }
            lblDetails.Text = intStartIndex.ToString() + "-" + intEndIndex.ToString() + " of " + Convert.ToInt32(lstChildServices[0].intRowCount).ToString();

            StringBuilder strSearch = new StringBuilder();
            if ((!string.IsNullOrEmpty(Request.QueryString["intId"]) && Request.QueryString["intId"] != "0") && (string.Equals(objSearch.strActionCode, "dd", StringComparison.OrdinalIgnoreCase)))
            {
                strSearch.Append("<strong>Department - </strong>");
                strSearch.Append(lstChildServices[0].strDepartment);
                strSearch.Append("<br/>");
            }
            else if (string.Equals(objSearch.strActionCode, "sd", StringComparison.OrdinalIgnoreCase))
            {
                strSearch.Append("<strong>Department - </strong>");
                strSearch.Append(lstChildServices[0].strDepartment);
                strSearch.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["sId"]) && Request.QueryString["sId"] != "0")
            {
                strSearch.Append("<strong>Service - </strong>");
                strSearch.Append(lstChildServices[0].ServiceName);
                strSearch.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fDate"]))
            {
                strSearch.Append("<strong>From Date - </strong>");
                strSearch.Append(Convert.ToDateTime(Request.QueryString["fDate"]).ToString("dd-MMM-yyyy"));
                strSearch.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["tDate"]))
            {
                strSearch.Append("<strong>To Date - </strong>");
                strSearch.Append(Convert.ToDateTime(Request.QueryString["tDate"]).ToString("dd-MMM-yyyy"));
                strSearch.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["dist"]) && Request.QueryString["dist"] != "0")
            {
                strSearch.Append("<strong>District - </strong>");
                strSearch.Append(lstChildServices[0].strDistName);
                strSearch.Append("<br/>");
            }
            lblSearchDetails.Text = strSearch.ToString();
        }
        else
        {
            ddlNoOfRec.Visible = false;
            rptPager.Visible = false;
        }
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.ExportToExcel("ServiceQueryRpt", grdDepartment, "Report on Child Services", lblSearchDetails.Text + "<br/> As on date - " + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("ServiceQueryRpt", grdDepartment);
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