using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Web.UI;
using System.Linq;
using System.Text;
using System.Globalization;

public partial class Portal_MISReport_MISAllstatusRptDtls : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
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
                FillGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ProposalMIS");
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
            hdnPgindex.Value = (string)((sender as LinkButton).CommandArgument);
            FillGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
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
            FillGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion

    protected void grdPealDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hypProposalNo = (HyperLink)e.Row.FindControl("hypProposalNo");
            hypProposalNo.NavigateUrl = string.Format("../Proposal/MISproposaldetails.aspx?Pno={0}", hypProposalNo.Text);

            HiddenField HdnFieldRemarks =(HiddenField)e.Row.FindControl("HdnFieldRemarks");

            e.Row.ToolTip = HdnFieldRemarks.Value;
        }

    }

    private void FillGrid(int intPageIndex, int intPageSize)
    {
        grdPealDetails.DataSource = null;
        grdPealDetails.DataBind();
        divExport.Visible = false;
        lblSearchDetails.Text = string.Empty;
        string strFromDate = string.Empty;
        string strToDate = string.Empty;
        int intMonth =DateTime.Today.Month;
        //Changes By Manoj
        int sectorid = 0;
        if (Request.QueryString["Secid"].ToString()!="0")
        {
            sectorid = Convert.ToInt32(Request.QueryString["Secid"].ToString());
        }
        else
        {
            if (Request.QueryString["Secid"].ToString()=="0")
            {
                if (Request.QueryString["distname"].ToString().Contains("IT") == true)
                {
                    sectorid = 10;
                }
                else if (Request.QueryString["distname"].ToString().Contains("Tourism") == true)
                {
                    sectorid = 38;
                }
                else
                {
                    sectorid = 0;
                }
            }
            else
            {
                sectorid = 0;
            }
        }
        //END OF CHANGES
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

        PealSearch objSearch = new PealSearch()
        {
            strActionCode = Request.QueryString["Act"],
            intDistrictId = !string.IsNullOrEmpty(Request.QueryString["dstid"]) ? Convert.ToInt32(Request.QueryString["dstid"]) : 0,
            intProjectType = !string.IsNullOrEmpty(Request.QueryString["projctType"]) ? Convert.ToInt32(Request.QueryString["projctType"]) : 0,
            //intSectorId = !string.IsNullOrEmpty(Request.QueryString["Secid"]) ? Convert.ToInt32(Request.QueryString["Secid"]) : 0,
            //Changes by manoj
            intSectorId=sectorid,
            strFromDate = string.IsNullOrEmpty(Request.QueryString["fDate"]) ? strFromDate : Request.QueryString["fDate"],
            strToDate = string.IsNullOrEmpty(Request.QueryString["tDate"]) ? strToDate : Request.QueryString["tDate"],
            intInvestmentAmt = !string.IsNullOrEmpty(Request.QueryString["InvAmt"]) ? Convert.ToInt32(Request.QueryString["InvAmt"]) : 0,
            intPageSize = intPageSize,
            intIntPageIndex = intPageIndex,
            intUserId = Convert.ToInt32(Session["UserId"]),
            intStatus = !string.IsNullOrEmpty(Request.QueryString["status"]) ? Convert.ToInt32(Request.QueryString["status"]) : 0
        };

        if (Convert.ToInt32(Session["UserId"]) == 557)// for idco user only
        {
            objSearch.intProjectType = 0;
        }

        List<Mis_ChildServiceDtls> lstDetails = new List<Mis_ChildServiceDtls>();

        if (!string.IsNullOrEmpty(Request.QueryString["Logic"]))
        {
            lstDetails = MisReportServices.PEAL_MisReportLogic2_Details(objSearch);
        }
        else
        {
            lstDetails = MisReportServices.PealMisRpt_Details(objSearch);
        }
        grdPealDetails.DataSource = lstDetails;
        grdPealDetails.DataBind();

        if (grdPealDetails.Rows.Count > 0)
        {
            ddlNoOfRec.Visible = true;
            rptPager.Visible = true;
            divExport.Visible = true;
            CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(lstDetails[0].intRowCount), Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));

            GridViewRow gRowFooter = grdPealDetails.FooterRow;
            gRowFooter.Cells[1].Text = "Total";
            gRowFooter.Cells[9].Text = IncentiveCommonFunctions.FormatDecimalString(lstDetails.Sum(x => x.decInvestment).ToString());
            gRowFooter.Cells[10].Text = IncentiveCommonFunctions.FormatString(lstDetails.Sum(x => x.intPropEmployment).ToString());
            gRowFooter.Cells[11].Text = IncentiveCommonFunctions.FormatDecimalString(lstDetails.Sum(x => x.decTotalLandRequired).ToString());
            gRowFooter.Cells[12].Text = IncentiveCommonFunctions.FormatDecimalString(lstDetails.Sum(x => x.decLandRecommendedToIdco).ToString());

            /****************code to show paging details in the label************/
            int intPIndex = Convert.ToInt32(hdnPgindex.Value);
            int intStartIndex = 1, intEndIndex = 0;
            int intPSize = Convert.ToInt32(ddlNoOfRec.SelectedValue);
            intStartIndex = ((intPIndex - 1) * intPSize) + 1;
            if (intPSize == grdPealDetails.Rows.Count)
            {
                intEndIndex = intPSize * intPIndex;
            }
            else
            {
                intEndIndex = grdPealDetails.Rows.Count + (intPSize * (intPIndex - 1));
            }
            lblDetails.Text = intStartIndex.ToString() + "-" + intEndIndex.ToString() + " of " + Convert.ToInt32(lstDetails[0].intRowCount).ToString();

            StringBuilder strSearch = new StringBuilder();
            if (!string.IsNullOrEmpty(Request.QueryString["dstid"]) && Request.QueryString["dstid"] != "0")
            {
                strSearch.Append("<strong>District - </strong>");
                strSearch.Append(lstDetails[0].ServiceName);
                // strSearch.Append("<br/>");
                strSearch.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Secid"]) && Request.QueryString["Secid"] != "0")
            {
                strSearch.Append("<strong>Sector - </strong>");
                strSearch.Append(lstDetails[0].strSector);
                strSearch.Append("<br/>");               
            }
            if (!string.IsNullOrEmpty(Request.QueryString["fDate"]))
            {
                strSearch.Append("<strong>From Date - </strong>");
                strSearch.Append(Convert.ToDateTime(Request.QueryString["fDate"]).ToString("dd-MMM-yyyy"));
                //strSearch.Append("<br/>");
                strSearch.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["tDate"]))
            {
                strSearch.Append("<strong>To Date - </strong>");
                strSearch.Append(Convert.ToDateTime(Request.QueryString["tDate"]).ToString("dd-MMM-yyyy"));
                strSearch.Append("<br/>");
                 //strSearch.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            }
            if (!string.IsNullOrEmpty(Request.QueryString["InvAmt"]) && Request.QueryString["InvAmt"] != "0")
            {
                strSearch.Append("<strong>Investment Amount - </strong>");
                string strIndType = Request.QueryString["InvAmt"];
                if (strIndType == "1")
                {
                    strSearch.Append("less than 50 crore");
                    strSearch.Append("<br/>");
                }
                else if (strIndType == "2")
                {
                    strSearch.Append("greater than 50 crore");
                    strSearch.Append("<br/>");
                }
                else if (strIndType == "3")
                {
                    strSearch.Append("greater than 1000 crore");
                    strSearch.Append("<br/>");
                }

            }
            if (!string.IsNullOrEmpty(Request.QueryString["Status"]) && Request.QueryString["Status"] != "0")
            {
                string Status = Request.QueryString["Status"];
                strSearch.Append("<strong>Status - </strong>");
                if (Status == "1")
                {
                    strSearch.Append("<span style='color: blue;font-weight:bold;'>Current period pending</span>");
                }
                else if (Status == "2")
                {
                    strSearch.Append("<span style='color: green;font-weight:bold;'>Approved</span>");
                }
                else if (Status == "3")
                {
                    strSearch.Append("<span style='color: red;font-weight:bold;'>Rejected</span>");
                }
                else if (Status == "7")
                {
                    strSearch.Append("<span style='color: violet;font-weight:bold;'>Deferred</span>");
                }
                else if (Status == "-2")
                {
                    strSearch.Append("Application pending beyond 30 days");
                }
                else if (Status == "8")
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
            ddlNoOfRec.Visible = false;
            rptPager.Visible = false;
            hdnPgindex.Value = "1";
        }

    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("PealMisReportDtls", grdPealDetails);
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.ExportToExcel("PealMisReportDtls", grdPealDetails, "Mis Report on PEAL", lblSearchDetails.Text + "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }


}