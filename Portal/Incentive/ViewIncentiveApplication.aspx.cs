/*
 * File name : PcViewPage
 * Class name : PcViewPage.cs
 * Description : Landing page for production certificate
 * [Modification History]
 * [CR No]      [Modified By]       [Modified On]       [Description]
 *  1           25th Oct 2017       Pranay kumar        Added code for the query IMPLEMENTATION
 */


using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;
using EntityLayer.Service;
using Ionic.Zip;


public partial class Incentive_ViewIncentiveApplication : SessionCheck
{
    int Retval = 0;
    //Added By Pranay Kumar on 10-OCT-2017 FOR QUERY IMPLEMENTATION
    bool IsPageRefresh = false;
    IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
    ServiceDetails objServiceEntity = new ServiceDetails();
    DepartmentSMSClass objDepartmntSms = new DepartmentSMSClass();
    string strRetVal = "";
    //ENDED By Pranay Kumar on 10-OCT-2017    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        try
        {
            if (!IsPostBack)
            {

                BindDistrict();
                getDist();
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

                //(START By Pranay Kumar on 24-OCT-2017 ) this section of code checks if the page postback is due to genuine submit by user or by pressing "refresh"
                ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
                Session["SessionId"] = ViewState["ViewStateId"].ToString();
                //(END By Pranay Kumar on 24-OCT-2017 ) this section of code checks if the page postback is due to genuine submit by user or by pressing "refresh"
            }
            else
            {
                //(START By Pranay Kumar on 24-OCT-2017 ) this section of code checks if the page postback is due to genuine submit by user or by pressing "refresh"
                if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
                {
                    IsPageRefresh = true;
                }

                Session["SessionId"] = System.Guid.NewGuid().ToString();
                ViewState["ViewStateId"] = Session["SessionId"].ToString();
                //(END By Pranay Kumar on 24-OCT-2017 ) this section of code checks if the page postback is due to genuine submit by user or by pressing "refresh"
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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

    protected void grdPcApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Approve")
            {
                GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer) as GridViewRow;
                LinkButton LnkApprove = (LinkButton)row.FindControl("LnkApprove");
                string formtype = row.Cells[5].Text;
                if (ActionCode(Convert.ToInt32(LnkApprove.CommandArgument), 2, formtype) == true)
                {
                    ScriptManager.RegisterStartupScript(LnkApprove, this.GetType(), "Myalert", "alert('Approved Sucessfully');window.location.href='ViewIncentiveApplication.aspx?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "';", true);
                }

            }
            if (e.CommandName == "Reject")
            {
                GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer) as GridViewRow;
                LinkButton LnkReject = (LinkButton)row.FindControl("LnkReject");
                string formtype = row.Cells[4].Text;
                if (ActionCode(Convert.ToInt32(LnkReject.CommandArgument), 3, formtype) == true)
                {
                    ScriptManager.RegisterStartupScript(LnkReject, this.GetType(), "Myalert", "alert('Rejected Sucessfully');window.location.href='ViewIncentiveApplication.aspx?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "';", true);
                    //  Response.Redirect("ViewIncentiveApplication.aspx?&linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&URL=1");
                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "incentive");
        }
    }

    /// <summary>
    /// rowdatabound event for grdSupervisorShow. It will set serial no in first column based on paging 
    /// and set navigateurl for edit hyperlink
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdPcApplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int Rowid = 0;
                int type = 0;
                if (e.Row.Cells[5].Text == "Large")
                { type = 2; }
                else { type = 1; }
                if (Convert.ToInt32(hdnPgindex.Value) > 1)
                    Rowid = (Convert.ToInt32(hdnPgindex.Value) - 1) * Convert.ToInt32(ddlNoOfRec.SelectedValue) + e.Row.DataItemIndex + 1;
                else
                    Rowid = e.Row.DataItemIndex + 1;
                e.Row.Cells[0].Text = Rowid.ToString();

                HiddenField hdnStatus = (HiddenField)e.Row.FindControl("hdnStatus");
                HyperLink hypIpcForm = (HyperLink)e.Row.FindControl("hypIpcForm");
                HyperLink hypUploadPC = (HyperLink)e.Row.FindControl("hypUploadPC");
                HyperLink hypDetails = (HyperLink)e.Row.FindControl("hypDetails");
                HyperLink hypViewPC = (HyperLink)e.Row.FindControl("hypViewPC");
                HiddenField hdnDetails = (HiddenField)e.Row.FindControl("hdnDetails");
                HiddenField hdnGenerate = (HiddenField)e.Row.FindControl("hdnGenerate");
                HiddenField hdnScheduleSt = (HiddenField)e.Row.FindControl("hdnScheduleSt");
                HiddenField hdnScheduleDate = (HiddenField)e.Row.FindControl("hdnScheduleDate");
                HyperLink hypScheduleIR = (HyperLink)e.Row.FindControl("hypScheduleIR");
                HiddenField hdnOfflinePc = (HiddenField)e.Row.FindControl("hdnOfflinePc");

                if (!string.IsNullOrEmpty(hdnStatus.Value))
                {
                    int intStatus = Convert.ToInt32(hdnStatus.Value);
                    if (intStatus == 0)
                    {

                        hypUploadPC.Visible = false;
                        if (hdnScheduleSt.Value == "0")
                        {
                            hypIpcForm.Visible = false;
                            hypScheduleIR.Visible = true;
                            hypScheduleIR.NavigateUrl = string.Format("../Incentive/ScheduleInspection.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, type);
                        }
                        else
                        {
                            hypIpcForm.Visible = true;
                            hypScheduleIR.Visible = false;
                        }

                        if (e.Row.Cells[5].Text == "Large")
                        {

                            hypIpcForm.NavigateUrl = string.Format("../Incentive/IRFormLarge.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value);
                            hypDetails.NavigateUrl = string.Format("../Incentive/IncentiveDetailsLarge.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, 1);
                        }
                        else
                        {
                            hypIpcForm.NavigateUrl = string.Format("../Incentive/IRForm.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value);
                            hypDetails.NavigateUrl = string.Format("../Incentive/IncentiveDetails.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, 1);
                        }
                    }
                    else if (intStatus == 1)
                    {
                        hypScheduleIR.Visible = false;
                        hypIpcForm.Visible = true;
                        hypIpcForm.Text = "VERIFY IR";
                        hypUploadPC.Visible = false;
                        if (e.Row.Cells[5].Text == "Large")
                        {
                            hypIpcForm.NavigateUrl = string.Format("../Incentive/IRFormLarge.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, 1);
                            hypDetails.NavigateUrl = string.Format("../Incentive/IncentiveDetailsLarge.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, 1);
                        }
                        else
                        {
                            hypIpcForm.NavigateUrl = string.Format("../Incentive/IRForm.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, 1);

                            hypDetails.NavigateUrl = string.Format("../Incentive/IncentiveDetails.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, 1);
                        }
                    }
                    else if (intStatus == 2)
                    {
                        hypScheduleIR.Visible = false;
                        if (hdnOfflinePc.Value == "1")
                        {
                            hypIpcForm.Visible = false;
                            //hypIpcForm.Text = "VIEW - IR";
                        }
                        else
                        {
                            hypIpcForm.Visible = true;
                            hypIpcForm.Text = "VIEW - IR";
                        }

                        if (e.Row.Cells[5].Text == "Large")
                        {
                            hypIpcForm.NavigateUrl = string.Format("../Incentive/IRFormLarge.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, 2);
                            hypDetails.NavigateUrl = string.Format("../Incentive/IncentiveDetailsLarge.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, 1);
                        }
                        else
                        {
                            hypDetails.NavigateUrl = string.Format("../Incentive/IncentiveDetails.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, 1);
                            hypIpcForm.NavigateUrl = string.Format("../Incentive/IRForm.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}&type={7}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value, 2);
                        }

                        HiddenField hdnPcPdfPath = (HiddenField)e.Row.FindControl("hdnPcPdfPath");
                        if (e.Row.Cells[5].Text == "Large")
                        {

                            if (hdnGenerate.Value != "0")
                            {
                                hypViewPC.Visible = true;
                                hypUploadPC.Visible = false;
                                if (!string.IsNullOrEmpty(hdnPcPdfPath.Value))
                                {
                                    hypViewPC.NavigateUrl = "~" + hdnPcPdfPath.Value;
                                }
                                //hypViewPC.NavigateUrl = string.Format("../Incentive/PCPrintLarge.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value);
                            }
                            else
                            {
                                hypUploadPC.Visible = true;
                                hypViewPC.Visible = false;

                                hypUploadPC.NavigateUrl = string.Format("../Incentive/ProductionCertificate_large.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value);
                            }
                        }

                        else
                        {

                            if (hdnGenerate.Value != "0")
                            {
                                hypViewPC.Visible = true;
                                hypUploadPC.Visible = false;

                                if (!string.IsNullOrEmpty(hdnPcPdfPath.Value))
                                {
                                    hypViewPC.NavigateUrl = "~" + hdnPcPdfPath.Value;
                                }
                            }
                            else
                            {
                                //hypUploadPC.Text = "View PC";
                                hypViewPC.Visible = false;
                                hypUploadPC.Visible = true;
                                hypUploadPC.NavigateUrl = string.Format("../Incentive/ProductionCertificate.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value);
                            }
                        }
                    }


                    else if (intStatus == 3)
                    {
                        hypScheduleIR.Visible = false;
                        hypIpcForm.Visible = true;
                        hypUploadPC.Visible = false;
                        hypViewPC.Visible = false;
                        if (e.Row.Cells[5].Text == "Large")
                        {
                            hypIpcForm.NavigateUrl = string.Format("../Incentive/IRFormLarge.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value);
                            hypDetails.NavigateUrl = string.Format("../Incentive/IncentiveDetailsLarge.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value);
                        }
                        else
                        {
                            hypIpcForm.NavigateUrl = string.Format("../Incentive/IRForm.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value);

                            hypDetails.NavigateUrl = string.Format("../Incentive/IncentiveDetails.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}&pSize={5}&pIndex={6}", hdnDetails.Value, Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"], ddlNoOfRec.SelectedValue, hdnPgindex.Value);
                        }
                    }
                }

                //Added By Pranay Kumar on 24-OCT-2017 for Implementation of Query Management
                int intQueryStatus = Convert.ToInt32(grdPcApplication.DataKeys[e.Row.RowIndex].Values[1]);
                LinkButton lbtnRaise = (e.Row.FindControl("lbtnRaise") as LinkButton);

                if (intQueryStatus == 1)//Query Raised
                {
                    lbtnRaise.Visible = true;
                }
                else if (intQueryStatus == 0) //IF Query Date is Blank
                {
                    lbtnRaise.Visible = false;
                }
                else if (intQueryStatus == 2) //If Query Date is Expired
                {
                    lbtnRaise.Visible = false;

                }

                HiddenField hdnTextVal1 = (HiddenField)e.Row.FindControl("hdnTextVal1");
                List<QueryMgntDtls> objProposalList = new List<QueryMgntDtls>();
                QueryMgntDtls objQueryMgtDtls = new QueryMgntDtls();
                objQueryMgtDtls.strAction = "QD";
                objQueryMgtDtls.strApplicationNum = hdnTextVal1.Value;
                if (e.Row.Cells[5].Text == "Large")
                {
                    objProposalList = ObjIMB.getPCLargeRaisedQueryDetails(objQueryMgtDtls).ToList();
                }
                else
                {
                    objProposalList = ObjIMB.getPCMSMERaisedQueryDetails(objQueryMgtDtls).ToList();
                }

                HtmlGenericControl QueryHist = (HtmlGenericControl)e.Row.FindControl("QueryHist");
                HtmlGenericControl QueryHist1 = (HtmlGenericControl)e.Row.FindControl("QueryHist1");

                if (objProposalList.Count > 0)
                {
                    string strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th>Query Reference No.</th><th>User Name</th><th> Query Details</th><th>Date</th><th>Files</th></tr>";
                    for (int i = 0; i < objProposalList.Count; i++)
                    {
                        if (objProposalList[i].strFileName == null || objProposalList[i].strFileName == "")
                        {
                            strHTMlQuery = strHTMlQuery + "<tr><td>" + objProposalList[i].strQueryUnqNo + "</td><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a target='' href='#'>--</a>" + "</td></tr>";
                        }
                        else
                        {
                            strHTMlQuery = strHTMlQuery + "<tr><td>" + objProposalList[i].strQueryUnqNo + "</td><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a class='ancDownload' href='../../incentives/Files/QueryDocs/" + objProposalList[i].strFileName + "'>Download</a>" + "</td></tr>";
                        }
                    }
                    strHTMlQuery = strHTMlQuery + "</table>";

                    QueryHist.InnerHtml = strHTMlQuery;
                    QueryHist1.InnerHtml = strHTMlQuery;
                }
                string strCurrQueryStatus = Convert.ToString(grdPcApplication.DataKeys[e.Row.RowIndex].Values[2]);
                LinkButton lbtnQueryDtls = (e.Row.FindControl("lbtnQueryDtls") as LinkButton);
                if (strCurrQueryStatus == "--")
                {
                    lbtnQueryDtls.Visible = false;
                    lbtnQueryDtls.Text = "--";
                }
                else if (strCurrQueryStatus == "Completed")
                {
                    lbtnQueryDtls.CssClass = "btn btn-success btn-sm";
                    lbtnQueryDtls.Text = "<i class='fa fa-eye' aria-hidden='true'></i>";
                }
                else if (strCurrQueryStatus == "QUERY RAISED")
                {
                    lbtnQueryDtls.Text = strCurrQueryStatus;
                    lbtnQueryDtls.CssClass = "btn btn-success btn-sm";
                }
                else if (strCurrQueryStatus == "QUERY RESPONDED")
                {
                    lbtnQueryDtls.Text = "QRY RESPONDED";
                    lbtnQueryDtls.CssClass = "btn btn-warning btn-sm";
                    lbtnQueryDtls.Visible = false;
                    lbtnRaise.Text = "QRY RESPONDED";
                    lbtnRaise.CssClass = "btn btn-warning btn-sm";
                }
                //Ended By Pranay Kumar on 24-oct-2017

            }
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    for (int i = 0; i < e.Row.Cells.Count; i++)
            //    {
            //        e.Row.Cells[i].Style["text-align"] = "center";
            //    }
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage("4") + "');", true);
        }
    }

    //to get district id
    private void getDist()
    {
        DataSet dt = new DataSet();
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        PcSearch objSearch = new PcSearch()
        {

            strActionCode = "Dist",
            intPageSize = 0,
            intPageIndex = 0,
            intAppFor = 0,
            vchAppNos = "",
            intDistId = "0",
            strUnitName = string.Empty,
            UserId = Convert.ToInt32(Session["userId"])
        };
        dt = objBuisnessLayer.Incentive_PcForm_View(objSearch);
        if (dt.Tables[0].Rows.Count > 0)
        {
            ddlDist.SelectedValue = dt.Tables[0].Rows[0]["distid"].ToString();
            if (ddlDist.SelectedValue != "0")
                ddlDist.Enabled = false;
        }

    }

    private void FillGrid(int intpageIndex, int intPageSize)
    {
        DataSet dt = new DataSet();
        grdPcApplication.DataSource = null;
        grdPcApplication.DataBind();
        PcSearch objSearch = new PcSearch()
        {
            strActionCode = "appr",
            intPageSize = intPageSize,
            intPageIndex = intpageIndex,
            intAppFor = 0,
            vchAppNos = "",
            intDistId = "0",
            strUnitName = string.Empty,
            UserId = Convert.ToInt32(Session["userId"])
        };
        if (!string.IsNullOrEmpty(txtAppNo.Text.Trim()))
        {
            objSearch.vchAppNos = txtAppNo.Text.Trim();
        }
        if (ddlStatus.SelectedIndex >= 0)
        {
            objSearch.intAppFor = Convert.ToInt32(ddlStatus.SelectedValue);
        }
        if (ddlDist.SelectedIndex >= 0)
        {
            objSearch.intDistId = ddlDist.SelectedValue;
        }
        if (!string.IsNullOrEmpty(txtUnitName.Text.Trim()))
        {
            objSearch.strUnitName = txtUnitName.Text.Trim();
        }
        //List<PcApplied> lstPcApplied = new List<PcApplied>();
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        dt = objBuisnessLayer.Incentive_PcForm_View(objSearch);
        if (dt != null && dt.Tables[0].Rows.Count > 0)
        {

            grdPcApplication.DataSource = dt.Tables[0];
            grdPcApplication.DataBind();

            if (grdPcApplication.Rows.Count == 0)
            {
                ddlNoOfRec.Visible = false;
                rptPager.Visible = false;
                hdnPgindex.Value = "1";
            }
            else
            {
                //lnkExportToExcel.Visible = true;
                ddlNoOfRec.Visible = true;
                rptPager.Visible = true;
                CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(dt.Tables[0].Rows[0]["rowcnt"]), Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
                //hdnTtlRowCount.Value = ObjDt.Rows[0]["rowcnt"].ToString();
                /****************code to show paging details in the label************/
                int intPIndex = Convert.ToInt32(hdnPgindex.Value);
                int intStartIndex = 1, intEndIndex = 0;
                int intPSize = Convert.ToInt32(ddlNoOfRec.SelectedValue);
                intStartIndex = ((intPIndex - 1) * intPSize) + 1;
                if (intPSize == grdPcApplication.Rows.Count)
                {
                    intEndIndex = intPSize * intPIndex;
                }
                else
                {
                    intEndIndex = grdPcApplication.Rows.Count + (intPSize * (intPIndex - 1));

                }
                lblDetails.Text = intStartIndex.ToString() + "-" + intEndIndex.ToString() + " of " + Convert.ToInt32(dt.Tables[0].Rows[0]["rowcnt"]).ToString();
            }
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid(1, 10);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }

    private bool ActionCode(int intAppNo, int AppStatus, string formtype)
    {
        bool retval = true;
        Incentive_PCMaster objMaster = new Incentive_PCMaster();
        try
        {
            IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();

            objMaster.intApplyFlag = AppStatus;
            objMaster.strActionCode = "E";
            objMaster.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            objMaster.intAppNo = intAppNo;
            if (formtype == "Large")
                Retval = objBuisnessLayer.Incentive_PcDetailsLarge_Approve(objMaster);
            else
                Retval = objBuisnessLayer.Incentive_PcDetails_Approve(objMaster);
            if (Retval == 1)
            {
                retval = true;
            }

            else
            {
                retval = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", ex.Message.ToString(), true);
        }
        finally
        {
            objMaster = null;

        }
        return retval;
    }

    private void BindDistrict()
    {
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        objBuisnessLayer.BindDropdown(ddlDist, new IncentiveMaster()
        {
            Action = "I",
            Param = 0,
            Param_1 = 0,
            Param_2 = string.Empty,
            Param_3 = string.Empty
        });

    }

    #region "Added By Pranay Kumar on 10-OCT-2017"
    protected void btnQuerySubmit_Click(object sender, EventArgs e)
    {

        try
        {
            QueryMgntDtls objQueryMgtDtls = new QueryMgntDtls();
            if (IsPageRefresh == false)
            {
                Button btnQuerySubmit = (Button)sender;
                TextBox txtq1 = (TextBox)btnQuerySubmit.FindControl("txtq1");
                FileUpload FileUpload1 = (FileUpload)btnQuerySubmit.FindControl("FileUpload1");
                HiddenField hdnUnitCategory = (HiddenField)btnQuerySubmit.FindControl("hdnUnitCategory");
                objQueryMgtDtls.strAction = "Q";
                objQueryMgtDtls.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                objQueryMgtDtls.strApplicationNum = btnQuerySubmit.CommandArgument.ToString();
                objQueryMgtDtls.intStatus = 5;
                objQueryMgtDtls.strRemarks = txtq1.Text;
                string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + btnQuerySubmit.CommandArgument.ToString() + "_Query1" + ".pdf", DateTime.Now);

                if (FileUpload1.HasFile)
                {

                    if (Path.GetExtension(FileUpload1.FileName) != ".pdf")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
                    }
                    else if (!IncentiveCommonFunctions.IsFileValid(FileUpload1, new string[] { ".pdf" }))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid file type (or) file name contains dots');", true);
                    }
                    else
                    {
                        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/incentives/Files/QueryDocs/"));
                        if (!string.IsNullOrEmpty(FileUpload1.FileName))
                        {
                            if (dir.Exists)
                            {
                                FileUpload1.SaveAs(Server.MapPath("~/incentives/Files/QueryDocs/" + filepath));
                            }
                            else
                            {
                                System.IO.Directory.CreateDirectory(Server.MapPath("~/incentives/Files/QueryDocs"));
                                FileUpload1.SaveAs(Server.MapPath("~/incentives/Files/QueryDocs/" + filepath));

                            }
                        }
                    }
                }
                else { filepath = ""; }
                objQueryMgtDtls.strFileName = filepath;
                if (hdnUnitCategory.Value == "Large")
                {
                    strRetVal = ObjIMB.PCLargeRaiseQuery(objQueryMgtDtls);
                }
                else
                {
                    strRetVal = ObjIMB.PCMSMERaiseQuery(objQueryMgtDtls);
                }
                FillGrid(1, 10);
                if (strRetVal == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Query Raised Successfully.')</script>;", false);
                    //FOR SENDING MAIL & SMS
                    CommonHelperCls comm = new CommonHelperCls();
                    List<QueryMgntDtls> objProposalList = new List<QueryMgntDtls>();
                    QueryMgntDtls objProp = new QueryMgntDtls();

                    objProp.strAction = "S";
                    objProp.strApplicationNum = btnQuerySubmit.CommandArgument.ToString();
                    objProp.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                    if (hdnUnitCategory.Value == "Large")
                    {
                        objProposalList = ObjIMB.getPCLargeRaisedQueryDetails(objProp).ToList();

                    }
                    else
                    {
                        objProposalList = ObjIMB.getPCMSMERaisedQueryDetails(objProp).ToList();
                    }

                    string mobile = "";
                    string smsContent = "";
                    string strSubject = "";
                    string[] toEmail = new string[1];

                    if (objProposalList.Count > 0)
                    {
                        mobile = Convert.ToString(objProposalList[0].strMobileNo);
                        smsContent = Convert.ToString(objProposalList[0].strEmailBody);
                        toEmail[0] = Convert.ToString(objProposalList[0].strEmailID);
                        strSubject = Convert.ToString(objProposalList[0].strEmailSubject);
                        string strBody = smsContent;
                       // comm.sendMail(strSubject, strBody, toEmail, true);
                        comm.SendSms(mobile, smsContent);
                    }
                    //For Sending SMS TO HOD
                    objProp.strAction = "T";
                    objProp.strApplicationNum = btnQuerySubmit.CommandArgument.ToString();
                    if (hdnUnitCategory.Value == "Large")
                    {
                        objProposalList = ObjIMB.getPCLargeRaisedQueryDetails(objProp).ToList();

                    }
                    else
                    {
                        objProposalList = ObjIMB.getPCMSMERaisedQueryDetails(objProp).ToList();
                    }


                    if (objProposalList.Count > 0)
                    {
                        if (objProposalList[0].intNoOfTimes >= 2) // for fetching how many times query raised by dept
                        {
                            if (hdnUnitCategory.Value == "Large")
                            {
                                objServiceEntity.INT_SERVICEID = 502; //service id for PC Large is 502

                            }
                            else
                            {
                                objServiceEntity.INT_SERVICEID = 503; //service id for PC MSME is 502
                            }
                            objServiceEntity.strSubject = strSubject;
                            objServiceEntity.strBody = smsContent;
                            objServiceEntity.smsContent = smsContent;

                            objDepartmntSms.DepartmentSendSms(objServiceEntity);
                        }
                    }

                }
                else if (strRetVal == "4")
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Action can not be taken Successfully.')</script>;", false); }
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            // objProposal = null;
        }
    }
    #region "ZIP DOWNLOAD"
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("QueryDocs");
                if (hdnFileNames.Value != "")
                {
                    string[] arrFileName = hdnFileNames.Value.Split(',');
                    for (int i = 0; i <= arrFileName.Count() - 1; i++)
                    {
                        string FileName = "../../incentives/Files/QueryDocs/" + Convert.ToString(arrFileName[i]);
                        string filePath = Server.MapPath(FileName);
                        zip.AddFile(filePath, "QueryDocs");
                    }
                }
                Response.Clear();
                Response.BufferOutput = false;
                string zipName = String.Format("QueryFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                Response.ContentType = "application/zip";
                Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                zip.Save(Response.OutputStream);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #endregion
    #endregion
}
