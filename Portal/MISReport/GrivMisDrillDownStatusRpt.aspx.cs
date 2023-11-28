using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Web.UI;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Data;

public partial class Portal_MISReport_GrivMisDrillDownStatusRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                CommonFunctions.PopulatePageSize(DrpDwn_NoOfRec);
                Hid_Page_Index.Value = "1";

                if (!string.IsNullOrEmpty(Request.QueryString["hdn"]))
                {
                    Hid_Page_Index.Value = Request.QueryString["hdn"];
                }
                else
                {
                    Hid_Page_Index.Value = "1";
                }

                if (Request.QueryString["pSize"] != null)
                {
                    DrpDwn_NoOfRec.SelectedValue = Request.QueryString["pSize"];
                }
                else
                {
                    DrpDwn_NoOfRec.SelectedValue = "10";
                }

                FillGrid(Convert.ToInt32(Hid_Page_Index.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "GrievanceMIS");
            }
            finally
            {
            }
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
            Hid_Page_Index.Value = (string)((sender as LinkButton).CommandArgument);
            FillGrid(Convert.ToInt32(Hid_Page_Index.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GrievanceMIS");
        }
    }

    /// <summary>
    /// selected index change event for the dropdown that contains different size for the page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DrpDwn_NoOfRec_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Hid_Page_Index.Value = "1";
            FillGrid(Convert.ToInt32(Hid_Page_Index.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GrievanceMIS");
        }
    }

    #endregion

    protected void GrdGrivDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Rowid = 0;
            if (Convert.ToInt32(Hid_Page_Index.Value) > 1)
            {
                Rowid = (Convert.ToInt32(Hid_Page_Index.Value) - 1) * Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue) + e.Row.DataItemIndex + 1;
            }
            else
            {
                Rowid = e.Row.DataItemIndex + 1;
            }
            e.Row.Cells[0].Text = Rowid.ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hypGrievanceNo = (HyperLink)e.Row.FindControl("hypGrievanceNo");
            hypGrievanceNo.NavigateUrl = string.Format("../Grievance/MisViewGrievanceDetails.aspx?GrivId={0}", hypGrievanceNo.Text);
        }
    }

    private void FillGrid(int intPageIndex, int intPageSize)
    {
        GrdGrivDetails.DataSource = null;
        GrdGrivDetails.DataBind();

        divExport.Visible = false;
        lblSearchDetails.Text = string.Empty;

        /*-----------------------------------------------------------------------*/
        string strFromDate;
        string strToDate;
        int intMonth = DateTime.Today.Month;
        if (intMonth == 1)
        {
            strFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
            strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
        else
        {
            strFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString() + (DateTime.Today.Year).ToString();
            strToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
        /*-----------------------------------------------------------------------*/

        GrievanceMisSearch objSearch = new GrievanceMisSearch
        {
            StrActionCode = Request.QueryString["Act"],
            IntDistrictId = !string.IsNullOrEmpty(Request.QueryString["dstid"]) ? Convert.ToInt32(Request.QueryString["dstid"]) : 0,
            IntGrivTypeId = !string.IsNullOrEmpty(Request.QueryString["Gtypeid"]) ? Convert.ToInt32(Request.QueryString["Gtypeid"]) : 0,
            StrFromDate = string.IsNullOrEmpty(Request.QueryString["fDate"]) ? strFromDate : Request.QueryString["fDate"],
            StrToDate = string.IsNullOrEmpty(Request.QueryString["tDate"]) ? strToDate : Request.QueryString["tDate"],
            IntPageSize = intPageSize,
            IntIntPageIndex = intPageIndex,
            IntStatus = !string.IsNullOrEmpty(Request.QueryString["status"]) ? Convert.ToInt32(Request.QueryString["status"]) : 0
        };

        DataTable dt = MisReportServices.GetStatusWiseGrievanceMISReportDtls(objSearch);

        GrdGrivDetails.DataSource = dt;
        GrdGrivDetails.DataBind();

        if (GrdGrivDetails.Rows.Count > 0)
        {
            DrpDwn_NoOfRec.Visible = true;
            rptPager.Visible = true;
            divExport.Visible = true;

            CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(dt.Rows.Count), Convert.ToInt32(Hid_Page_Index.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));

            /****************code to show paging details in the label************/
            int intPIndex = Convert.ToInt32(Hid_Page_Index.Value);
            int intPSize = Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue);

            int intStartIndex = ((intPIndex - 1) * intPSize) + 1;

            int intEndIndex;
            if (intPSize == GrdGrivDetails.Rows.Count)
            {
                intEndIndex = intPSize * intPIndex;
            }
            else
            {
                intEndIndex = GrdGrivDetails.Rows.Count + (intPSize * (intPIndex - 1));
            }

            lblDetails.Text = intStartIndex.ToString() + "-" + intEndIndex.ToString() + " of " + Convert.ToInt32(dt.Rows.Count).ToString();

            StringBuilder strSearch = new StringBuilder();
            if (!string.IsNullOrEmpty(Request.QueryString["dstid"]) && Request.QueryString["dstid"] != "0")
            {
                strSearch.Append("<strong>District - </strong>");
                strSearch.Append(Request.QueryString["distname"].ToString());
                strSearch.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Gtypeid"]) && Request.QueryString["Gtypeid"] != "0")
            {
                strSearch.Append("<strong>Sector - </strong>");
                strSearch.Append(dt.Rows[0]["vchGrivType"].ToString());
                strSearch.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fDate"]))
            {
                strSearch.Append("<strong>From Date - </strong>");
                strSearch.Append(Convert.ToDateTime(Request.QueryString["fDate"]).ToString("dd-MMM-yyyy"));
                strSearch.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["tDate"]))
            {
                strSearch.Append("<strong>To Date - </strong>");
                strSearch.Append(Convert.ToDateTime(Request.QueryString["tDate"]).ToString("dd-MMM-yyyy"));
                strSearch.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Status"]) && Request.QueryString["Status"] != "0")
            {
                string Status = Request.QueryString["Status"];
                strSearch.Append("<strong>Status - </strong>");
                if (Status == "1")
                {
                    strSearch.Append("Current period pending");
                }
                else if (Status == "13")
                {
                    strSearch.Append("Resolved");
                }
                else if (Status == "3")
                {
                    strSearch.Append("Rejected");
                }
                else if (Status == "7")
                {
                    strSearch.Append("Deferred");
                }
                else if (Status == "8")
                {
                    strSearch.Append("Forwarded");
                }
                else if (Status == "-2")
                {
                    strSearch.Append("Application pending beyond 30 days");
                }
                else if (Status == "10")
                {
                    strSearch.Append("Opening Balance");
                }
                else if (Status == "9")
                {
                    strSearch.Append("Total Pending applications(Opening Balance + Current period pending)");
                }
            }

            lblSearchDetails.Text = strSearch.ToString();
        }
        else
        {
            DrpDwn_NoOfRec.Visible = false;
            rptPager.Visible = false;
            Hid_Page_Index.Value = "1";
        }
    }

    protected void LnkBtnPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("GrievanceMisReportDtls", GrdGrivDetails);
    }

    protected void LnkBtnExcel_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.ExportToExcel("GrievanceMisReportDtls", GrdGrivDetails, "Mis Report on Grievance", lblSearchDetails.Text + "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}