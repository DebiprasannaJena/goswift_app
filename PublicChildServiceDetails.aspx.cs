﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;

public partial class PublicChildServiceDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["UserId"] != null)
        //{
        //    if (!IsPostBack)
        //    {
        //        string strDepartment = Session["DeptId"].ToString();
        //        BindGridView();
        //    }
        //}
        //else
        //{
        //    Response.Redirect("../SessionTimeout.aspx");
        //}

        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    private void BindGridView()
    {
        lblSearchDetails.Text = string.Empty;
        grdDepartment.DataSource = null;
        grdDepartment.DataBind();

        string strFromDate = string.Empty, strTodate = string.Empty;
        GetDefaultFromAndToDate(out strFromDate, out strTodate);

        RptSearch objSearch = new RptSearch()
        {
            strActionCode = Request.QueryString["Action"],
            intPageSize = 0,
            intIntPageIndex = 0,
            strToDate = string.IsNullOrEmpty(Request.QueryString["tDate"]) ? strTodate : Request.QueryString["tDate"],
            strFromDate = string.IsNullOrEmpty(Request.QueryString["fdate"]) ? strFromDate : Request.QueryString["fdate"],
            intDepartmentId = string.IsNullOrEmpty(Request.QueryString["intId"]) ? 0 : Convert.ToInt32(Request.QueryString["intId"]),
            intDistrictId = string.IsNullOrEmpty(Request.QueryString["dist"]) ? 0 : Convert.ToInt32(Request.QueryString["dist"]),
            intServiceId = string.IsNullOrEmpty(Request.QueryString["sId"]) ? 0 : Convert.ToInt32(Request.QueryString["sId"])
        };

        List<MIS_ChildServiceRpt> lstChildServices = new List<MIS_ChildServiceRpt>();
        if (string.Equals("sn", objSearch.strActionCode, StringComparison.OrdinalIgnoreCase))
        {
            objSearch.strActionCode = "s";
            lstChildServices = MisReportServices.View_ChildServices_MISReport_New(objSearch);
        }
        else
        {
            lstChildServices = MisReportServices.View_ChildServices_MISReport(objSearch);
        }

        /*---------------------------------------------------------------------------------*/
        ///// Changes made by Sushant Jena on Dt:-13-Jul-2021
        ///// A new row(child service) to be added with static data for department of energy (Dept Id = 3),H & UD (Dept Id = 659) and IDCO water connection (Dept Id = 5).
        ///// For Health and Family Welfare (H&FW) Department (dept id=9), Static record to be added for 2 services having service id 30 and 31.
        /*---------------------------------------------------------------------------------*/
        if (Request.QueryString["intId"] != null && Request.QueryString["intId"] != "")
        {
            int intDeptId = Convert.ToInt32(Request.QueryString["intId"]);
            if (intDeptId == 3 || intDeptId == 659 || intDeptId == 5)
            {
                MIS_ChildServiceRpt objChildService = new MIS_ChildServiceRpt();

                objChildService.strDeptName = "Mutation (Change of Name)";
                objChildService.intTotalApplication = 293023;
                objChildService.intTotalApproved = 281549;
                objChildService.intTotalRejected = 78415;
                objChildService.intORTPSAtimeline = 90;
                objChildService.intTotalORTPSAtimelinePassed = 17998;
                objChildService.intAvgDaysApproval = 125;
                objChildService.intMinApprovalDays = 1;
                objChildService.intMaxApprovalDays = 188;

                lstChildServices.Add(objChildService);

                if (intDeptId == 3)
                {
                    objChildService = new MIS_ChildServiceRpt();

                    objChildService.strDeptName = "Approval for DG Set Installation";
                    objChildService.intTotalApplication = 28;
                    objChildService.intTotalApproved = 28;
                    objChildService.intTotalRejected = 0;
                    objChildService.intORTPSAtimeline = 25;
                    objChildService.intTotalORTPSAtimelinePassed = 24;
                    objChildService.intAvgDaysApproval = 16;
                    objChildService.decMedian = 35;
                    objChildService.intMinApprovalDays = 12;
                    objChildService.intMaxApprovalDays = 58;

                    lstChildServices.Add(objChildService);
                }
            }
            else if (intDeptId == 9) //// Health and Family Welfare (H&FW) Department
            {
                for (int i = 0; i < lstChildServices.Count; i++)
                {
                    int intServiceId = lstChildServices[i].intKey;
                    if (intServiceId == 30)////Retail Drug License (Pharmacy) and renewal thereof
                    {
                        lstChildServices[i].intTotalApplication = 156;
                        lstChildServices[i].intTotalApproved = 156;
                        lstChildServices[i].intTotalRejected = 0;
                        lstChildServices[i].intAllTotalPending = 0;
                        lstChildServices[i].intORTPSAtimeline = 40;
                        lstChildServices[i].intTotalORTPSAtimelinePassed = 0;
                        lstChildServices[i].intAvgDaysApproval = 23;
                        lstChildServices[i].decMedian = 21;
                        lstChildServices[i].intMinApprovalDays = 15;
                        lstChildServices[i].intMaxApprovalDays = 40;
                    }
                    else if (intServiceId == 31)////Wholesale drug license and renewal thereof
                    {
                        lstChildServices[i].intTotalApplication = 47;
                        lstChildServices[i].intTotalApproved = 47;
                        lstChildServices[i].intTotalRejected = 0;
                        lstChildServices[i].intAllTotalPending = 0;
                        lstChildServices[i].intORTPSAtimeline = 40;
                        lstChildServices[i].intTotalORTPSAtimelinePassed = 0;
                        lstChildServices[i].intAvgDaysApproval = 24;
                        lstChildServices[i].decMedian = 20;
                        lstChildServices[i].intMinApprovalDays = 15;
                        lstChildServices[i].intMaxApprovalDays = 40;
                    }
                }
            }
        }
        /*---------------------------------------------------------------------------------*/

        grdDepartment.DataSource = lstChildServices;
        grdDepartment.DataBind();

        if (grdDepartment.Rows.Count > 0)
        {
            GridViewRow gRowFooter = grdDepartment.FooterRow;
            gRowFooter.Cells[1].Text = "Total";
            gRowFooter.Cells[2].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intCarryFwdPending).ToString());
            gRowFooter.Cells[3].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalApplication).ToString());
            gRowFooter.Cells[4].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalApproved).ToString());
            gRowFooter.Cells[5].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalRejected).ToString());
            gRowFooter.Cells[6].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalQueryRaised).ToString());
            gRowFooter.Cells[7].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalPending).ToString());
            gRowFooter.Cells[8].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intAllTotalPending).ToString());
            gRowFooter.Cells[9].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalORTPSAtimelinePassed).ToString());
            gRowFooter.Cells[10].Text = IncentiveCommonFunctions.FormatString(lstChildServices[0].intAvgDaysApprovalTotal.ToString());
            gRowFooter.Cells[11].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalDeferred).ToString()); //// Added by Sushant Jena On Dated: 08-May-2020
            gRowFooter.Cells[12].Text = IncentiveCommonFunctions.FormatString(lstChildServices.Sum(x => x.intTotalForwarded).ToString());//// Added by Sushant Jena On Dated: 08-May-2020

            StringBuilder strSearch = new StringBuilder();
            strSearch.Append("<strong>Department - </strong>");
            strSearch.Append(lstChildServices[0].strParentName);
            strSearch.Append("<br/>");

            if (!string.IsNullOrEmpty(Request.QueryString["sId"]) && Request.QueryString["sId"] != "0")
            {
                strSearch.Append("<strong>Service - </strong>");
                strSearch.Append(lstChildServices[0].strDeptName);
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
    }

    protected void grdDegrdDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        LinkButton lnkBtn = (LinkButton)e.CommandSource;    // the button
        GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row
        GridView myGrid = (GridView)sender; // the gridview

        string ID = myGrid.DataKeys[myRow.RowIndex].Value.ToString();

        StringBuilder strNavigateUrl = new StringBuilder();

        if (string.Equals(e.CommandName, "D", StringComparison.OrdinalIgnoreCase)) //for details with application status
        {
            try
            {
                strNavigateUrl.Append(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("MISReport")));
                strNavigateUrl.Append("MisReport/ChildServiceAppDetails.aspx");
                strNavigateUrl.Append("?intId=");
                strNavigateUrl.Append(Request.QueryString["intId"]);//department id from query string
                strNavigateUrl.Append("&SId=");
                strNavigateUrl.Append(ID); //service id from gridview
                strNavigateUrl.Append("&fDate=");
                strNavigateUrl.Append(Request.QueryString["fDate"]);
                strNavigateUrl.Append("&tDate=");
                strNavigateUrl.Append(Request.QueryString["tDate"]);
                strNavigateUrl.Append("&dist=");
                strNavigateUrl.Append(Request.QueryString["dist"]);
                strNavigateUrl.Append("&Action=");
                strNavigateUrl.Append("sdn");
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
                strNavigateUrl.Append(Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("MISReport")));
                strNavigateUrl.Append("MisReport/ChildServiceQueryDetails.aspx");
                strNavigateUrl.Append("?intId=");
                strNavigateUrl.Append(Request.QueryString["intId"]);//department id from query string
                strNavigateUrl.Append("&SId=");
                strNavigateUrl.Append(ID); //service id from gridview
                strNavigateUrl.Append("&fDate=");
                strNavigateUrl.Append(Request.QueryString["fDate"]);
                strNavigateUrl.Append("&tDate=");
                strNavigateUrl.Append(Request.QueryString["tDate"]);
                strNavigateUrl.Append("&dist=");
                strNavigateUrl.Append(Request.QueryString["dist"]);
                strNavigateUrl.Append("&Action=");
                strNavigateUrl.Append("sq");
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
    protected void grdDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //LinkButton lnkTotalApplication = (LinkButton)e.Row.FindControl("lnkTotalApplication");
            //LinkButton lnkApproved = (LinkButton)e.Row.FindControl("lnkApproved");
            //LinkButton lnkPending = (LinkButton)e.Row.FindControl("lnkPending");
            //LinkButton lnkQuery = (LinkButton)e.Row.FindControl("lnkQuery");
            //LinkButton lnkORTPS = (LinkButton)e.Row.FindControl("lnkORTPS");
            //LinkButton lnkRejected = (LinkButton)e.Row.FindControl("lnkRejected");
            //LinkButton lnkCarryFwdPending = (LinkButton)e.Row.FindControl("lnkCarryFwdPending");
            //LinkButton lnkAllPending = (LinkButton)e.Row.FindControl("lnkAllPending");
            //LinkButton lnkDeferred = (LinkButton)e.Row.FindControl("lnkDeferred");
            //LinkButton lnkForwarded = (LinkButton)e.Row.FindControl("lnkForwarded");

            //Label lblTotalApplication = (Label)e.Row.FindControl("lblTotalApplication");
            //Label lblQuery = (Label)e.Row.FindControl("lblQuery");
            //Label lblApproved = (Label)e.Row.FindControl("lblApproved");
            //Label lblPending = (Label)e.Row.FindControl("lblPending");
            //Label lblRejected = (Label)e.Row.FindControl("lblRejected");
            //Label lblORTPS = (Label)e.Row.FindControl("lblORTPS");
            //Label lblCarryFwdPending = (Label)e.Row.FindControl("lblCarryFwdPending");
            //Label lblAllPending = (Label)e.Row.FindControl("lblAllPending");
            //Label lblDeferred = (Label)e.Row.FindControl("lblDeferred");
            //Label lblForwarded = (Label)e.Row.FindControl("lblForwarded");

            //ShowHideHyperlink(lnkTotalApplication, lblTotalApplication);
            //ShowHideHyperlink(lnkApproved, lblApproved);
            //ShowHideHyperlink(lnkPending, lblPending);
            //ShowHideHyperlink(lnkQuery, lblQuery);
            //ShowHideHyperlink(lnkORTPS, lblORTPS);
            //ShowHideHyperlink(lnkRejected, lblRejected);
            //ShowHideHyperlink(lnkCarryFwdPending, lblCarryFwdPending);
            //ShowHideHyperlink(lnkAllPending, lblAllPending);
            //ShowHideHyperlink(lnkDeferred, lblDeferred);
            //ShowHideHyperlink(lnkForwarded, lblForwarded);
        }
    }

    /// <summary>
    /// Function to show and hide the hyperlink button and label in gridview
    /// </summary>
    /// <param name="lnkBtn">hyperlink object in row</param>
    /// <param name="lbl">Label object in row</param>
    /// <param name="intStatus">the type of cell - 1 pending, 2 approved, 3 rejected, 4 disbursed</param>
    /// <param name="gRow">current gridview row</param>
    /// <param name="intYear">the current financial year</param>
    private void ShowHideHyperlink(LinkButton lnkBtn, Label lbl)
    {
        if (!string.IsNullOrEmpty(lnkBtn.Text) && lnkBtn.Text != "-" && lnkBtn.Text != "0")
        {
            lnkBtn.Visible = true;
        }
        else
        {
            lbl.Visible = true;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    private void GetDefaultFromAndToDate(out string strFromDate, out string strToDate)
    {
        strFromDate = string.Empty;
        strToDate = string.Empty;
        int intMonth = DateTime.Today.Month;
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

    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PublicDashBoardService.aspx");
    }
}