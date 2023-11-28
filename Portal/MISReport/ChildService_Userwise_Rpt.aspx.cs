/*
 * File Name : Peal_Userwise_Rpt.aspx
 * Class Name : Portal_MISReport_Peal_Userwise_Rpt
 * Created On : 3rd Feb 2018
 * Created By : Ritika Lath
 * Desription : Peal wise Report to get details by the user
 */

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

public partial class Portal_MISReport_ChildService_Userwise_Rpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            string strFromDate= string.Empty,  strTodate= string.Empty;
            GetDefaultFromAndToDate(out strFromDate, out strTodate);
            txtFromDate.Text = strFromDate;
            txtToDate.Text = strTodate;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues('" + strFromDate + "','" + strTodate + "');</script>", false);
            if (Session["UserId"] != null)
            {
                int intLevelId = Convert.ToInt32(Session["LevelID"].ToString());
                int intDepartMentId = Convert.ToInt32(Session["DeptId"]);
                if (intLevelId == 1 || intLevelId == 3)
                {
                    CommonBindDropDown(drpDepartment, "DD", string.Empty);
                }
                else if (intLevelId == 2)
                {
                    BindDeptDropdown(intDepartMentId);
                }

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
            hdnPgindex.Value = (sender as LinkButton).CommandArgument;
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

    protected void grdDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LinkButton lnkBtn = (LinkButton)e.CommandSource;    // the button
        GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row
        GridView myGrid = (GridView)sender; // the gridview

        string ID = myGrid.DataKeys[myRow.RowIndex].Value.ToString();

        StringBuilder strNavigateUrl = new StringBuilder();
        int intService = Convert.ToInt32(drpService.SelectedValue);
        int intDepartment = Convert.ToInt32(drpDepartment.SelectedValue);

        if (string.Equals(e.CommandName, "D", StringComparison.OrdinalIgnoreCase)) //for details with application status
        {
            try
            {
                strNavigateUrl.Append(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("MISReport")));
                strNavigateUrl.Append("MISReport/ChildService_UserDtls.aspx");
                strNavigateUrl.Append("?intId=");
                strNavigateUrl.Append(ID);
                strNavigateUrl.Append("&status=");
                strNavigateUrl.Append(lnkBtn.CommandArgument);
                strNavigateUrl.Append("&SId=");
                strNavigateUrl.Append(intService);
                strNavigateUrl.Append("&dId=");
                strNavigateUrl.Append(intDepartment);
                strNavigateUrl.Append("&fDate=");
                strNavigateUrl.Append(txtFromDate.Text.Trim());
                strNavigateUrl.Append("&tDate=");
                strNavigateUrl.Append(txtToDate.Text.Trim());
                strNavigateUrl.Append("&Action=");
                strNavigateUrl.Append("st");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ViewModal('" + strNavigateUrl.ToString() + "') ;", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage("4") + "');", true);
            }
        }
        else if (string.Equals(e.CommandName, "Q", StringComparison.OrdinalIgnoreCase)) //for details with Query
        {
            try
            {
                strNavigateUrl.Append(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("MISReport")));
                strNavigateUrl.Append("MisReport/ChildService_User_QueryDtls.aspx");
                strNavigateUrl.Append("?intId=");
                strNavigateUrl.Append(ID);
                strNavigateUrl.Append("&status=");
                strNavigateUrl.Append(lnkBtn.CommandArgument);
                strNavigateUrl.Append("&SId=");
                strNavigateUrl.Append(intService);
                strNavigateUrl.Append("&dId=");
                strNavigateUrl.Append(intDepartment);
                strNavigateUrl.Append("&fDate=");
                strNavigateUrl.Append(txtFromDate.Text.Trim());
                strNavigateUrl.Append("&tDate=");
                strNavigateUrl.Append(txtToDate.Text.Trim());
                strNavigateUrl.Append("&Action=");
                strNavigateUrl.Append("sq");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ViewModal('" + strNavigateUrl.ToString() + "') ;", true);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage("4") + "');", true);
            }
        }
    }

    protected void grdDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
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
            LinkButton lnkTotalApplication = (LinkButton)e.Row.FindControl("lnkTotalApplication");
            LinkButton lnkApproved = (LinkButton)e.Row.FindControl("lnkApproved");
            LinkButton lnkQueryRaised = (LinkButton)e.Row.FindControl("lnkQueryRaised");
            LinkButton lnkPending = (LinkButton)e.Row.FindControl("lnkPending");
            LinkButton lnkRejected = (LinkButton)e.Row.FindControl("lnkRejected");

            Label lblRejected = (Label)e.Row.FindControl("lblRejected");
            Label lblPending = (Label)e.Row.FindControl("lblPending");
            Label lblQueryRaised = (Label)e.Row.FindControl("lblQueryRaised");
            Label lblApproved = (Label)e.Row.FindControl("lblApproved");
            Label lblTotalApplication = (Label)e.Row.FindControl("lblTotalApplication");

            ShowHideHyperlink(lnkTotalApplication, lblTotalApplication);
            ShowHideHyperlink(lnkApproved, lblApproved);
            ShowHideHyperlink(lnkRejected, lblRejected);
            ShowHideHyperlink(lnkPending, lblPending);
            ShowHideHyperlink(lnkQueryRaised, lblQueryRaised);
        }
    }

    /// <summary>
    /// Function to show and hide the hyperlink button and label in gridview
    /// </summary>
    /// <param name="lnk">hyperlink object in row</param>
    /// <param name="lbl">Label object in row</param>
    private void ShowHideHyperlink(LinkButton lnk, Label lbl)
    {
        if (!string.IsNullOrEmpty(lnk.Text) && lnk.Text != "-" && lnk.Text != "0")
        {
            lnk.Visible = true;
        }
        else
        {
            lbl.Visible = true;
        }
    }

    private void FillGrid(int intPageIndex, int intPageSize)
    {
        grdDepartment.DataSource = null;
        grdDepartment.DataBind();
        lblSearchDetails.Text = string.Empty;
        string strFromDate= string.Empty,  strTodate= string.Empty;
        GetDefaultFromAndToDate(out strFromDate, out strTodate);
        RptSearch objSearch = new RptSearch()
        {
            strActionCode = "view",
            intPageSize = intPageSize,
            intIntPageIndex = intPageIndex,
            strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
            strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strTodate : txtToDate.Text.Trim(),
            intDepartmentId = drpDepartment.SelectedIndex > 0 ? Convert.ToInt32(drpDepartment.SelectedValue) : 0,
            intServiceId = drpService.SelectedIndex > 0 ? Convert.ToInt32(drpService.SelectedValue) : 0
        };

        int intLevelId = Convert.ToInt32(Session["LevelID"].ToString());
        int intDepartMentId = Convert.ToInt32(Session["DeptId"]);

        if (intLevelId == 2)//department login
        {
            if (drpDepartment.SelectedIndex > 0)
            {
                objSearch.intDepartmentId = Convert.ToInt32(drpDepartment.SelectedValue);
            }
            else
            {
                objSearch.intDepartmentId = intDepartMentId;
                drpDepartment.SelectedValue = intDepartMentId.ToString();
            }
        }
       
       
        List<MIS_ChildServiceRpt> lstChildServices = MisReportServices.View_ChildServices_UserWise_MISReport(objSearch);
        grdDepartment.DataSource = lstChildServices;
        grdDepartment.DataBind();

        if (grdDepartment.Rows.Count > 0)
        {
            GridViewRow gRowFooter = grdDepartment.FooterRow;
            gRowFooter.Cells[1].Text = "Total";
            gRowFooter.Cells[2].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalApplication).ToString());
            gRowFooter.Cells[3].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalApproved).ToString());
            gRowFooter.Cells[4].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalQueryRaised).ToString());
            gRowFooter.Cells[5].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalPending).ToString());
            gRowFooter.Cells[6].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalRejected).ToString());
            gRowFooter.Cells[7].Text = IncentiveCommonFunctions.FormatString(lstChildServices[0].intAvgDaysApprovalTotal.ToString());

            ddlNoOfRec.Visible = true;
            rptPager.Visible = true;
            CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(lstChildServices[0].intRowCount), Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));

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

            StringBuilder strSearchDetails = new StringBuilder();
            if (drpDepartment.SelectedIndex > 0)
            {
                strSearchDetails.Append("<strong>Department - </strong>");
                strSearchDetails.Append(drpDepartment.SelectedItem.Text.Trim());
                strSearchDetails.Append("<br/>");
            }
            if (drpService.SelectedIndex > 0)
            {
                strSearchDetails.Append("<strong>Service - </strong>");
                strSearchDetails.Append(drpService.SelectedItem.Text.Trim());
                strSearchDetails.Append("<br/>");
            }
            strSearchDetails.Append("<strong>From Date - </strong>");
            strSearchDetails.Append(txtFromDate.Text.Trim());
            strSearchDetails.Append("<br/>");

            strSearchDetails.Append("<strong>To Date - </strong>");
            strSearchDetails.Append(txtToDate.Text.Trim());
            lblSearchDetails.Text = strSearchDetails.ToString();
        }
        else
        {
            ddlNoOfRec.Visible = false;
            rptPager.Visible = false;
            hdnPgindex.Value = "1";
        }
    }

    private void BindDeptDropdown(int intDepartmentId)
    {
        int desId = Convert.ToInt32(Session["desId"].ToString());
        RptSearch objSearch = new RptSearch()
        {
            strActionCode = "drp",
            intDepartmentId = intDepartmentId,
        };

        if (desId == 3)
        {
            objSearch.strActionCode = "ddl";
        }
       
        Dictionary<int, string> dcDept = MisReportServices.ViewDepartmentListByUser(objSearch);

        drpDepartment.DataSource = dcDept;
        drpDepartment.DataTextField = "value";
        drpDepartment.DataValueField = "key";
        drpDepartment.DataBind();

        if (drpDepartment.Items.Count > 1)
        {
            drpDepartment.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        else
        {
            drpDepartment.SelectedIndex = 0;
            CommonBindDropDown(drpService, "SS", drpDepartment.SelectedValue);
        }
    }

    private void CommonBindDropDown(DropDownList drp, string strActionCode, string strProposalNo)
    {
        
        ProposalBAL objProposalBal = new ProposalBAL();
        ProjectInfo objProp = new ProjectInfo()
        {
            strAction = strActionCode,
            vchProposalNo = strProposalNo
        };

        List<ProjectInfo> objProjList = objProposalBal.PopulateProjDropdowns(objProp).ToList();
        drp.DataSource = objProjList;
        drp.DataTextField = "vchserviceName";
        drp.DataValueField = "intserviceid";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDepartment.SelectedIndex > 0)
        {
            CommonBindDropDown(drpService, "SS", drpDepartment.SelectedValue);
        }
        else
        {
            drpService.Items.Clear();
            drpService.Items.Insert(0, new ListItem("-Select-", "0"));
        }
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.ExportToExcel("ChildServicesRpt", grdDepartment, "User wise report for Child Services", lblSearchDetails.Text + "<br/>As on date -" + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("ChildServicesRpt", grdDepartment);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
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