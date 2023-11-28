/*
 * Created By : Ritika Lath
 * Created On : 31st Jan 2018
 * File Name : ChilServiceRpt.aspx.cs
 * Class Name : Portal_MISReport_ChilServiceRpt
 * Description : MIS Report for the Child Services
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;

public partial class Portal_MISReport_ChilServiceRpt : System.Web.UI.Page
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
                if (intLevelId == 1 || intLevelId == 4) //admin or dic login
                {
                    CommonBindDropDown(drpDepartment, "DD", string.Empty);
                }
                else if (intLevelId == 2)
                {
                    BindDeptDropdown(intDepartMentId);
                }

                BindDistrict();
                if (intLevelId == 4) //for DIC Users
                {
                    int intDistrictid = Convert.ToInt32(Session["Pealuserid"].ToString());
                    System.Web.UI.WebControls.ListItem lstDistrict= ddlDistrict.Items.FindByValue(intDistrictid.ToString());
                    ddlDistrict.Items.Clear();
                    ddlDistrict.Items.Add(lstDistrict);
                    ddlDistrict.SelectedIndex = 0;

                }
                BindGridView();
            }
            else
            {
                Response.Redirect("../SessionTimeout.aspx");
            }
        }
    }

    private void BindDistrict()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            ddlDistrict.DataSource = objProjList;
            ddlDistrict.DataTextField = "vchDistName";
            ddlDistrict.DataValueField = "intDistId";
            ddlDistrict.DataBind();
            System.Web.UI.WebControls.ListItem list = new System.Web.UI.WebControls.ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlDistrict.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalMIS");
        }
    }

    private void BindGridView()
    {
        lblSearchDetails.Text = string.Empty;
        grdDepartment.DataSource = null;
        grdDepartment.DataBind();

        string strFromDate= string.Empty,  strTodate= string.Empty;
        GetDefaultFromAndToDate(out strFromDate, out strTodate);
        RptSearch objSearch = new RptSearch()
        {
            strActionCode = "P",
            intPageSize = 0,
            intIntPageIndex = 0,
            strFromDate = string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? strFromDate : txtFromDate.Text.Trim(),
            strToDate = string.IsNullOrEmpty(txtToDate.Text.Trim()) ? strTodate : txtToDate.Text.Trim(),
            intDistrictId = ddlDistrict.SelectedIndex > 0 ? Convert.ToInt32(ddlDistrict.SelectedValue) : 0,
            intDepartmentId = drpDepartment.SelectedValue != "0" ? Convert.ToInt32(drpDepartment.SelectedValue) : 0,
            intServiceId = drpService.SelectedIndex > 0 ? Convert.ToInt32(drpService.SelectedValue) : 0
        };

        int intLevelId = Convert.ToInt32(Session["LevelID"].ToString());
        int intDepartMentId = Convert.ToInt32(Session["DeptId"]);

        if (intLevelId == 4)
        {
            objSearch.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
        }

   
        List<MIS_ChildServiceRpt> lstChildServices = new List<MIS_ChildServiceRpt>();
        lstChildServices = MisReportServices.View_ChildServices_MISReport(objSearch);
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
            gRowFooter.Cells[6].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intCarryFwdPending).ToString());
            gRowFooter.Cells[7].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intAllTotalPending).ToString());
            gRowFooter.Cells[8].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalORTPSAtimelinePassed).ToString());
            gRowFooter.Cells[9].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalRejected).ToString());
            gRowFooter.Cells[10].Text = IncentiveCommonFunctions.FormatString(lstChildServices[0].intAvgDaysApprovalTotal.ToString());

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
            if (ddlDistrict.SelectedIndex > 0 || intLevelId == 4)
            {
                strSearchDetails.Append("<strong>District - </strong>");
                strSearchDetails.Append(ddlDistrict.SelectedItem.Text.Trim());
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

    protected void grdDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hypDepartment = (HyperLink)e.Row.FindControl("hypDepartment");
            ShowHideHyperlink(hypDepartment, null, e.Row, "S", "ChildServiceRptDtls.aspx");
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
    private void ShowHideHyperlink(HyperLink hyp, Label lbl, GridViewRow gRow, string strActionCode, string strPageName)
    {
        if (!string.IsNullOrEmpty(hyp.Text) && hyp.Text != "-" && hyp.Text != "0")
        {
            int intService = drpService.SelectedIndex > 0 ? Convert.ToInt32(drpService.SelectedValue) : 0;
            hyp.Visible = true;
            StringBuilder strNavigateUrl = new StringBuilder();
            strNavigateUrl.Append("~/Portal/MISReport/");
            strNavigateUrl.Append(strPageName);
            strNavigateUrl.Append("?intId=");
            strNavigateUrl.Append(grdDepartment.DataKeys[gRow.RowIndex].Value);
            strNavigateUrl.Append("&dist=");
            strNavigateUrl.Append(ddlDistrict.SelectedValue);
            strNavigateUrl.Append("&fDate=");
            strNavigateUrl.Append(txtFromDate.Text.Trim());
            strNavigateUrl.Append("&tDate=");
            strNavigateUrl.Append(txtToDate.Text.Trim());
            strNavigateUrl.Append("&SId=");
            strNavigateUrl.Append(intService);
            strNavigateUrl.Append("&Action=");
            strNavigateUrl.Append(strActionCode);
            hyp.NavigateUrl = strNavigateUrl.ToString();

        }
        else
        {
            if (lbl != null)
            {
                lbl.Visible = true;
            }
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
        Dictionary<int,string> dcDept = new Dictionary<int, string>();
     
        dcDept = MisReportServices.ViewDepartmentListByUser(objSearch);

        drpDepartment.DataSource = dcDept;
        drpDepartment.DataTextField = "value";
        drpDepartment.DataValueField = "key";
        drpDepartment.DataBind();

        drpDepartment.SelectedValue = intDepartmentId.ToString();
        CommonBindDropDown(drpService, "SS", drpDepartment.SelectedValue);
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.ExportToExcel("ChildServicesRpt", grdDepartment, "Report on Child Services", lblSearchDetails.Text + "<br/> As on date - " + DateTime.Today.ToString("d-MMM-yyyy"), string.Empty, true);
    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        IncentiveCommonFunctions.CreatePdf("ChildServicesRpt", grdDepartment);
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
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