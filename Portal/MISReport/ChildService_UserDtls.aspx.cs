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
        try
        {

           if (!IsPostBack)
            {
             CommonFunctions.PopulatePageSize(DrpDwn_NoOfRec);
            Hdn_Pgindex.Value = "1";
            if (!string.IsNullOrEmpty(Request.QueryString["hdn"]))
            {
                Hdn_Pgindex.Value = Request.QueryString["hdn"];
            }
            else
            {
                Hdn_Pgindex.Value = "1";
            }
            if (Request.QueryString["pSize"] != null)
            {
                DrpDwn_NoOfRec.SelectedValue = Request.QueryString["pSize"];
            }
            else
            {
                DrpDwn_NoOfRec.SelectedValue = "10";
            }

              BindGridView(Convert.ToInt32(Hdn_Pgindex.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceDetail");
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
            Hdn_Pgindex.Value = (sender as LinkButton).CommandArgument;
            BindGridView(Convert.ToInt32(Hdn_Pgindex.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceDetail");
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
            Hdn_Pgindex.Value = "1";
            BindGridView(Convert.ToInt32(Hdn_Pgindex.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceDetail");
        }
    }
    #endregion

    protected void grdService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Rowid = 0;
                if (Convert.ToInt32(Hdn_Pgindex.Value) > 1)
                {
                    Rowid = (Convert.ToInt32(Hdn_Pgindex.Value) - 1) * Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue) + e.Row.DataItemIndex + 1;
                }
                else
                {
                    Rowid = e.Row.DataItemIndex + 1;
                }
                e.Row.Cells[0].Text = Rowid.ToString();
            }
        }
        catch(Exception ex)
        {
            throw ex.InnerException;
        }
        
    }

    private void BindGridView(int IntPageIndex, int IntPageSize)
    {
        try 
        { 
        Lbl_SearchDetails.Text = string.Empty;
        DivExport.Visible = false;
        GrdService.DataSource = null;
        GrdService.DataBind();
        string StrFromDate= string.Empty,  StrTodate= string.Empty;
        GetDefaultFromAndToDate(out StrFromDate, out StrTodate);
        RptSearch ObjSearch = new RptSearch()
        {
            strActionCode = "st",
            intPageSize = IntPageSize,
            intIntPageIndex = IntPageIndex,
            strToDate = string.IsNullOrEmpty(Request.QueryString["tDate"]) ? StrTodate : Request.QueryString["tDate"],
            strFromDate = string.IsNullOrEmpty(Request.QueryString["fdate"]) ? StrFromDate : Request.QueryString["fdate"],
            intDepartmentId = string.IsNullOrEmpty(Request.QueryString["dId"]) ? 0 : Convert.ToInt32(Request.QueryString["dId"]),
            intServiceId = string.IsNullOrEmpty(Request.QueryString["SId"]) ? 0 : Convert.ToInt32(Request.QueryString["SId"]),
            intUserId = string.IsNullOrEmpty(Request.QueryString["intId"]) ? 0 : Convert.ToInt32(Request.QueryString["intId"]),
            intStatus = string.IsNullOrEmpty(Request.QueryString["status"]) ? 0 : Convert.ToInt32(Request.QueryString["status"])
        };

       
        List<Mis_ChildServiceDtls> LstChildServices = MisReportServices.View_ChildServices_UserWiseDetails_MISReport(ObjSearch);
        GrdService.DataSource = LstChildServices;
        GrdService.DataBind();
        if (GrdService.Rows.Count > 0)
        {
            DivExport.Visible = true;
            DrpDwn_NoOfRec.Visible = true;
            rptPager.Visible = true;
            CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(LstChildServices[0].intRowCount), Convert.ToInt32(Hdn_Pgindex.Value), Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue));

            /****************code to show paging details in the label************/
            int IntPIndex = Convert.ToInt32(Hdn_Pgindex.Value);
            int IntStartIndex = 1, IntEndIndex = 0;
            int IntPSize = Convert.ToInt32(DrpDwn_NoOfRec.SelectedValue);
            IntStartIndex = ((IntPIndex - 1) * IntPSize) + 1;
            if (IntPSize == GrdService.Rows.Count)
            {
                IntEndIndex = IntPSize * IntPIndex;
            }
            else
            {
                IntEndIndex = GrdService.Rows.Count + (IntPSize * (IntPIndex - 1));

            }
            Lbl_Details.Text = IntStartIndex.ToString() + "-" + IntEndIndex.ToString() + " of " + Convert.ToInt32(LstChildServices[0].intRowCount).ToString();
            StringBuilder StrSearchDetails = new StringBuilder();
            if (!string.IsNullOrEmpty(Request.QueryString["intId"]) && Request.QueryString["intId"] != "0")
            {
                StrSearchDetails.Append("<strong>User - </strong>");
                StrSearchDetails.Append(LstChildServices[0].strUsername);
                StrSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["dId"]) && Request.QueryString["dId"] != "0")
            {
                StrSearchDetails.Append("<strong>Department - </strong>");
                StrSearchDetails.Append(LstChildServices[0].strDepartment);
                StrSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["SId"]) && Request.QueryString["SId"] != "0")
            {
                StrSearchDetails.Append("<strong>Service - </strong>");
                StrSearchDetails.Append(LstChildServices[0].ServiceName);
                StrSearchDetails.Append("<br/>");
            }

            if (!string.IsNullOrEmpty(Request.QueryString["fDate"]))
            {
                StrSearchDetails.Append("<strong>From Date - </strong>");
                StrSearchDetails.Append(Convert.ToDateTime(Request.QueryString["fDate"]).ToString("dd-MMM-yyyy"));
                StrSearchDetails.Append("<br/>");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["tDate"]))
            {
                StrSearchDetails.Append("<strong>To Date - </strong>");
                StrSearchDetails.Append(Convert.ToDateTime(Request.QueryString["tDate"]).ToString("dd-MMM-yyyy"));
                StrSearchDetails.Append("<br/>");
            }

            Lbl_SearchDetails.Text = StrSearchDetails.ToString();
        }
        else
        {
            DrpDwn_NoOfRec.Visible = false;
            rptPager.Visible = false;
        }

        }
        catch(Exception ex)
        {
            Util.LogError(ex, "ChildServiceDetail");
        }
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        try
        {

       
        IncentiveCommonFunctions.ExportToExcel("ChildServicesDetailsRpt", GrdService, "User wise report for Child Services", Lbl_SearchDetails.Text + "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChildServiceDetail");
        }

    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        try
        {
            IncentiveCommonFunctions.CreatePdf("ChildServicesDetailsRpt", GrdService);
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "ChildServiceDetail");
        }
        
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    private void GetDefaultFromAndToDate(out string StrFromDate, out string StrToDate)
    {
        try
        {
        StrFromDate = string.Empty;
        StrToDate = string.Empty;
        int IntMonth =DateTime.Today.Month;
        if (IntMonth == 1)
        {
            StrFromDate = "01-Dec-" + (DateTime.Today.Year - 1).ToString();
            StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
        else
        {
            StrFromDate = "01-" + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName((DateTime.Today.Month - 1)).ToString().Substring(0, 3) + "-" + (DateTime.Today.Year).ToString();
            StrToDate = DateTime.Today.ToString("dd-MMM-yyyy");
        }
        }
        catch(Exception ex)
        {
            throw ex.InnerException;
        }
    }
}