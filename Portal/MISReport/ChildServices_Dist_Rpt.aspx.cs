using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using System.Globalization;
using System.Text;

public partial class Portal_MISReport_ChildServices_Dist_Rpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
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


                int intLevelId = Convert.ToInt32(Session["LevelID"].ToString());
                int intDepartMentId = Convert.ToInt32(Session["DeptId"]);
                if (intLevelId == 1)
                {
                    CommonBindDropDown(drpDepartment, "DD", string.Empty);
                }
                else if (intLevelId == 2)
                {
                    BindDeptDropdown(intDepartMentId);
                }
                BindGridView();
            }
        }
        else
        {
            Response.Redirect("../SessionTimeout.aspx");
        }
    }

    #region User defined functions
    private void BindGridView()
    {
        lblSearchDetails.Text = string.Empty;
        grdDistrict.DataSource = null;
        grdDistrict.DataBind();

        string strFromDate= string.Empty,  strTodate= string.Empty;
        GetDefaultFromAndToDate(out strFromDate, out strTodate);
        RptSearch objSearch = new RptSearch()
        {
            strActionCode = "P",
            intPageSize = 0,
            intIntPageIndex = 0,
            intDistrictId = 0,
            strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
            strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strTodate : txtToDate.Text.Trim(),
            intDepartmentId = drpDepartment.SelectedValue != "0" ? Convert.ToInt32(drpDepartment.SelectedValue) : 0,
            intServiceId = drpService.SelectedIndex > 0 ? Convert.ToInt32(drpService.SelectedValue) : 0
        };

        int intLevelId = Convert.ToInt32(Session["LevelID"].ToString());
        int intDepartMentId = Convert.ToInt32(Session["DeptId"]);

        if (intLevelId == 4)
        {
            objSearch.intDistrictId = Convert.ToInt32(Session["Pealuserid"].ToString());
        }

        
        List<MIS_ChildServiceRpt> lstChildServices = new List<MIS_ChildServiceRpt>();
        lstChildServices = MisReportServices.View_ChildServices_District_Rpt(objSearch);
        grdDistrict.DataSource = lstChildServices;
        grdDistrict.DataBind();

        if (grdDistrict.Rows.Count > 0)
        {
            GridViewRow gRowFooter = grdDistrict.FooterRow;
            gRowFooter.Cells[1].Text = "Total";
            gRowFooter.Cells[2].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalApplication).ToString());
            gRowFooter.Cells[3].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalApproved).ToString());
            gRowFooter.Cells[4].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalQueryRaised).ToString());
            gRowFooter.Cells[5].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalPending).ToString());
            gRowFooter.Cells[6].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intCarryFwdPending).ToString());
            gRowFooter.Cells[7].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalORTPSAtimelinePassed).ToString());
            gRowFooter.Cells[8].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalRejected).ToString());
            gRowFooter.Cells[9].Text = IncentiveCommonFunctions.FormatString(lstChildServices[0].intAvgDaysApprovalTotal.ToString());

            StringBuilder strSearchDetails = new StringBuilder();
            if (drpDepartment.SelectedValue != "0")
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
    }

    private void CommonBindDropDown(DropDownList drp, string strActionCode, string strProposalNo)
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProposalBAL objProposalBal = new ProposalBAL();
        ProjectInfo objProp = new ProjectInfo()
        {
            strAction = strActionCode,
            vchProposalNo = strProposalNo
        };

        objProjList = objProposalBal.PopulateProjDropdowns(objProp).ToList();
        drp.DataSource = objProjList;
        drp.DataTextField = "vchserviceName";
        drp.DataValueField = "intserviceid";
        drp.DataBind();
        drp.Items.Insert(0, new ListItem("-Select-", "0"));


    }

    private void BindDeptDropdown(int intDepartmentId)
    {
        RptSearch objSearch = new RptSearch()
        {
            strActionCode = "drp",
            intDepartmentId = intDepartmentId,
        };

        Dictionary<int,string> dcDept = new Dictionary<int, string>();
       
        dcDept = MisReportServices.ViewDepartmentListByUser(objSearch);

        drpDepartment.DataSource = dcDept;
        drpDepartment.DataTextField = "value";
        drpDepartment.DataValueField = "key";
        drpDepartment.DataBind();

        drpDepartment.SelectedValue = intDepartmentId.ToString();
        CommonBindDropDown(drpService, "SS", drpDepartment.SelectedValue);
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

    protected void drpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpService.Items.Clear();
        if (drpDepartment.SelectedValue != "0")
        {
            CommonBindDropDown(drpService, "SS", drpDepartment.SelectedValue);
        }
    }

    /// <summary>
    /// Function to show and hide the hyperlink button and label in gridview
    /// </summary>
    /// <param name="hyp">hyperlink object in row</param>
    /// <param name="lbl">Label object in row</param>
    /// <param name="intStatus">the type of cell - 1 pending, 2 approved, 3 rejected, 4 disbursed</param>
    /// <param name="gRow">current gridview row</param>
    /// <param name="intYear">the current financial year</param>
    private void ShowHideHyperlink(LinkButton hyp, Label lbl)
    {
        if (!string.IsNullOrEmpty(hyp.Text) && hyp.Text != "-" && hyp.Text != "0")
        {
            hyp.Visible = true;
        }
        else
        {
            if (lbl != null)
            {
                lbl.Visible = true;
            }
        }
    }
    #endregion

    #region Grid events
    protected void grdDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LinkButton lnkBtn = (LinkButton)e.CommandSource;    // the button
        GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row
        GridView myGrid = (GridView)sender; // the gridview

        string ID = myGrid.DataKeys[myRow.RowIndex].Value.ToString();

        StringBuilder strNavigateUrl = new StringBuilder();
        int intDeptId = drpDepartment.SelectedIndex > 0 ? Convert.ToInt32(drpDepartment.SelectedValue) : 0;
        int intServiceId = drpService.SelectedIndex > 0 ? Convert.ToInt32(drpService.SelectedValue) : 0;

        if (string.Equals(e.CommandName, "D", StringComparison.OrdinalIgnoreCase)) //for details with application status
        {
            try
            {
                strNavigateUrl.Append(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("MISReport", StringComparison.OrdinalIgnoreCase)));
                strNavigateUrl.Append("MisReport/ChildServiceAppDetails.aspx");
                strNavigateUrl.Append("?intId=");
                strNavigateUrl.Append(intDeptId);//department id from query string
                strNavigateUrl.Append("&SId=");
                strNavigateUrl.Append(intServiceId); //service id from gridview
                strNavigateUrl.Append("&fDate=");
                strNavigateUrl.Append(txtFromDate.Text.Trim());
                strNavigateUrl.Append("&tDate=");
                strNavigateUrl.Append(txtToDate.Text.Trim());
                strNavigateUrl.Append("&dist=");
                strNavigateUrl.Append(ID);
                strNavigateUrl.Append("&Action=");
                strNavigateUrl.Append("dd");
                strNavigateUrl.Append("&Status=");
                strNavigateUrl.Append(lnkBtn.CommandArgument);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ViewModal('" + strNavigateUrl.ToString() + "') ;", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage("4") + "');", true);
            }
        }
        else if (string.Equals(e.CommandName, "Q", StringComparison.OrdinalIgnoreCase)) //for details with Query
        {
            try
            {
                strNavigateUrl.Append(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("MISReport", StringComparison.OrdinalIgnoreCase)));
                strNavigateUrl.Append("MisReport/ChildServiceQueryDetails.aspx");
                strNavigateUrl.Append("?intId=");
                strNavigateUrl.Append(intDeptId);//department id from query string
                strNavigateUrl.Append("&SId=");
                strNavigateUrl.Append(intServiceId); //service id from gridview
                strNavigateUrl.Append("&fDate=");
                strNavigateUrl.Append(txtFromDate.Text.Trim());
                strNavigateUrl.Append("&tDate=");
                strNavigateUrl.Append(txtToDate.Text.Trim());
                strNavigateUrl.Append("&dist=");
                strNavigateUrl.Append(ID);
                strNavigateUrl.Append("&Action=");
                strNavigateUrl.Append("dq");
                strNavigateUrl.Append("&Status=");
                strNavigateUrl.Append(lnkBtn.CommandArgument);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "ViewModal('" + strNavigateUrl.ToString() + "') ;", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage("4") + "');", true);
            }
        }
    }

    protected void grdDistrict_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkTotalApplication =(LinkButton)e.Row.FindControl("lnkTotalApplication");
            LinkButton lnkApproved =(LinkButton)e.Row.FindControl("lnkApproved");
            LinkButton lnkPending =(LinkButton)e.Row.FindControl("lnkPending");
            LinkButton lnkQuery =(LinkButton)e.Row.FindControl("lnkQuery");
            LinkButton lnkORTPS =(LinkButton)e.Row.FindControl("lnkORTPS");
            LinkButton lnkRejected =(LinkButton)e.Row.FindControl("lnkRejected");
            LinkButton lnkCarryFwdPending =(LinkButton)e.Row.FindControl("lnkCarryFwdPending");

            Label lblTotalApplication = (Label)e.Row.FindControl("lblTotalApplication");
            Label lblQuery = (Label)e.Row.FindControl("lblQuery");
            Label lblApproved = (Label)e.Row.FindControl("lblApproved");
            Label lblPending = (Label)e.Row.FindControl("lblPending");
            Label lblRejected = (Label)e.Row.FindControl("lblRejected");
            Label lblORTPS = (Label)e.Row.FindControl("lblORTPS");
            Label lblCarryFwdPending = (Label)e.Row.FindControl("lblCarryFwdPending");

            ShowHideHyperlink(lnkTotalApplication, lblTotalApplication);
            ShowHideHyperlink(lnkApproved, lblApproved);
            ShowHideHyperlink(lnkPending, lblPending);
            ShowHideHyperlink(lnkQuery, lblQuery);
            ShowHideHyperlink(lnkORTPS, lblORTPS);
            ShowHideHyperlink(lnkRejected, lblRejected);
            ShowHideHyperlink(lnkCarryFwdPending, lblCarryFwdPending);
        }
    }
    #endregion

    #region Click events
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.ExportToExcel("ChildServicesRpt", grdDistrict, "Report on Child Services", lblSearchDetails.Text + "<br/> As on date - " + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("ChildServicesRpt", grdDistrict);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}