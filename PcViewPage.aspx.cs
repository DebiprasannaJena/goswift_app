/*
 * File name : PcViewPage
 * Class name : PcViewPage.cs
 * Description : Landing page for production certificate
 * [Modification History]
 * [CR No]      [Modified By]       [Modified On]       [Description]
 *  1           25th Oct 2017       Pranay kumar        Added code for the query IMPLEMENTATION
 *  2           21st Nov 2017       Ritika lath         Added validation and exception handling
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;


public partial class PcViewPage : SessionCheck
{
    string unitCategory;
    IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (!IsPostBack)
        {
            try
            {
                GetApplicationDetails();
                NavigateQueryURL();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Incentive");
            }
        }
    }

    /// <summary>
    /// Function to get the all the details of the user. if the user is from peal then get details accordingly, else get values from pc 
    /// </summary>
    private void GetPealDetails()
    {
        gvPCReport.DataSource = null;
        gvPCReport.DataBind();

        gvViewIR.DataSource = null;
        gvViewIR.DataBind();

        try
        {
            PcSearch objSearch = new PcSearch()
            {
                intAppFor = Convert.ToInt32(Session["InvestorId"]),
                strActionCode = "peal",
                intPageIndex = 0,
                intPageSize = 0,
                strFromDate = string.Empty,
                strToDate = string.Empty
            };

            DataSet objDs = new DataSet();
            IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
            objDs = objBuisnessLayer.Incentive_PcForm_View(objSearch);
            if (objDs != null && objDs.Tables.Count > 0)
            {
                DataTable dt = objDs.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dRow = dt.Rows[0];
                    lblIndustryCode.Text = dRow["vchIndustryCode"].ToString();
                    int intEin = Convert.ToInt32(dRow["intEinNo"].ToString());
                    if (intEin >= 1 && intEin < 4)
                    {
                        switch (intEin)
                        {
                            case 1:
                                lblDocName.Text = "EIN";
                                divEin.Visible = true;
                                break;
                            case 2:
                                lblDocName.Text = "IEM";
                                divEin.Visible = true;
                                break;
                            case 3:
                                lblDocName.Text = "IL";
                                divEin.Visible = true;
                                break;
                            default:
                                divEin.Visible = false;
                                lblDocName.Text = "EIN";
                                break;
                        }
                        txtEin.Text = dRow["vchEINnIEMnIL"].ToString();
                    }

                    if (intEin == 4)
                    {
                        divUan.Visible = true;
                        txtUan.Text = dRow["vchEINnIEMnIL"].ToString() == "" ? "NA" : dRow["vchEINnIEMnIL"].ToString();
                    }

                    ddlSector.Text = dRow["sectornm"].ToString();
                    ddlSubSector.Text = dRow["SubSectornm"].ToString();
                    txtEnterpriseName.Text = dRow["vchNameOfUnit"].ToString();
                    drpOrganizationType.Text = dRow["organizationTypeNm"].ToString();
                    string projectType = dRow["projecttypename"].ToString();
                    txtInvestment.Text = dRow["decPlantndMachinery"].ToString();
                    if (dRow["inv"].ToString() == "1")
                    {
                        dvOrg.Visible = false;
                        dvInd.Visible = false;
                    }
                    else
                    {
                        dvOrg.Visible = true;
                        dvInd.Visible = true;
                    }
                    int intUserType = Convert.ToInt32(dRow["inv"].ToString());
                    hdnUserType.Value = intUserType.ToString();
                    if (intUserType == 1) //user has no records in peal or pc in that case show link for offline pc
                    {
                        btnOfflinePc.Visible = true;
                    }
                    else
                    {
                        btnOfflinePc.Visible = false;
                    }
                }
                DataTable dtNew;                
                if (objDs.Tables.Count > 1)
                {

                    dtNew = objDs.Tables[2];
                    gvViewIR.DataSource = dtNew;
                    gvViewIR.DataBind();

                }
                if (objDs.Tables.Count > 2)
                {
                    dtNew = objDs.Tables[3];
                    gvPCReport.DataSource = dtNew;
                    gvPCReport.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// Function to get all the details based on the application details called when editing
    /// </summary>
    private void GetApplicationDetails()
    {
        try
        {
            PcSearch objSearch = new PcSearch()
            {
                intAppFor = Convert.ToInt32(Session["investorid"]),
                strActionCode = "pc",
                intPageIndex = 0,
                intPageSize = 0,
                strFromDate = string.Empty,
                strToDate = string.Empty,
            };
            DataSet objDs = new DataSet();
            IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();

            objDs = objBuisnessLayer.Incentive_PcForm_Large_View(objSearch);
            if (objDs != null && objDs.Tables.Count > 0)
            {
                DataTable dtPcDetails = new DataTable();
                dtPcDetails = objDs.Tables[0];
                if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                {
                    Panel1.Visible = false;
                    pnlmain.Visible = true;
                    tPeal.Visible = false;

                    DataRow objRow = dtPcDetails.Rows[0];
                    lblIndustryCode.Text = objRow["vchIndustryCode"].ToString() == "" ? "NA" : objRow["vchIndustryCode"].ToString();
                    txtEin.Text = objRow["vchEINEMIIPMTNo"].ToString();
                    lblDocName.Text = "EIN/IEM";
                    txtUan.Text = objRow["vchUAN"].ToString() == "" ? "NA" : objRow["vchUAN"].ToString();
                    divEin.Visible = true;
                    divUan.Visible = true;
                    txtEnterpriseName.Text = objRow["vchCompName"].ToString();
                    drpOrganizationType.Text = objRow["organizationTypeNm"].ToString();
                    hdnUnitCat.Value = objRow["intUnitCat"].ToString();
                    ddlSector.Text = objRow["sectornm"].ToString();
                    ddlSubSector.Text = objRow["SubSectornm"].ToString();
                    lblUnit.Text = objRow["unittype"].ToString();
                    int intUnitCategory = Convert.ToInt32(hdnUnitCat.Value);

                    //if the user already has one record that is an offline pc and it is in not approved position, it means user was applying for existing emd and he has only filled the existing details, user now has to fill the existing with emd details for offline pc
                    if (Convert.ToInt32(objRow["intApproved"].ToString()) < 2 && objRow["intApplyFlag"].ToString() == "0" && objRow["intOfflinePc"].ToString() == "1")
                    {
                        btnOfflinePc.Visible = true;
                        hypApply.Visible = false;
                        if (intUnitCategory == 39)
                        {
                            hypOfflinePc.NavigateUrl = "incentives/PC_Large_Apply.aspx?offline=3";
                        }
                        else
                        {
                            hypOfflinePc.NavigateUrl = "incentives/IncentivePcForm.aspx?offline=3";
                        }
                    }

                    //if the user already has a record for a pc that is not in offline mode and is not approved 
                    else if (Convert.ToInt32(objRow["intApproved"].ToString()) < 2 && objRow["intApplyFlag"].ToString() == "1")
                    {
                        hypApply.Attributes.Add("onclick", "jAlert('Your PC has been submitted successfully. You cannot apply for another PC until your current application is approved.','SWP')");
                        hypApply.NavigateUrl = "#";
                        hypApply.Enabled = false;
                    }

                    // else if the user has an approved pc  or a pc in draft position that are not offline pc 
                    else
                    {
                        if (intUnitCategory == 39)
                        {
                            hypApply.NavigateUrl = "incentives/PC_Large_Apply.aspx";
                        }
                        else
                        {
                            hypApply.NavigateUrl = "incentives/IncentivePcForm.aspx";
                        }
                    }
                }

                // if the user has no records in the database 
                else
                {
                    hypApply.Enabled = false;
                    Panel1.Visible = true;
                    pnlmain.Visible = false;
                    GetPealDetails();
                }

                DataTable dtNew;
                if (objDs.Tables.Count > 0)
                {
                    dtNew = objDs.Tables[1];
                    gvViewIR.DataSource = dtNew;
                    gvViewIR.DataBind();

                }
                if (objDs.Tables.Count > 1)
                {
                    dtNew = objDs.Tables[2];
                    gvPCReport.DataSource = dtNew;
                    gvPCReport.DataBind();

                }

            }
            else
            {
                // GetPealDetails();
                hypApply.Enabled = false;
                Panel1.Visible = true;
                pnlmain.Visible = false;
                GetPealDetails();

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// rowdatabound event for the gvViewIR to show the IR report for the user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvViewIR_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hypIRForm = (HyperLink)e.Row.FindControl("hypIRForm");
            HiddenField hdnAppNo = (HiddenField)e.Row.FindControl("hdnAppNo");
            HiddenField hdnAppSts = (HiddenField)e.Row.FindControl("hdnAppSts");
            if (hdnAppSts.Value == "2" || hdnAppSts.Value == "3")
                hypIRForm.Visible = true;
            else
                hypIRForm.Visible = false;
            if (string.Equals(lblUnit.Text, "large", StringComparison.OrdinalIgnoreCase))
                hypIRForm.NavigateUrl = string.Format("Portal/Incentive/IRPrint_large.aspx?id={0}", hdnAppNo.Value, 0, 0);
            else
                hypIRForm.NavigateUrl = string.Format("Portal/Incentive/IRFormPrint.aspx?id={0}", hdnAppNo.Value, 0, 0);
        }

    }

    /// <summary>
    /// rowdatabound event for the gvPCReport to show the PC Certificate of the user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPCReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hypPCForm = (HyperLink)e.Row.FindControl("hypPCForm");
            HiddenField hdnAppStatus = (HiddenField)e.Row.FindControl("hdnAppStatus");
            HiddenField hdnAppNo = (HiddenField)e.Row.FindControl("hdnAppNo");
            if (hdnAppStatus.Value == "3")
            {
                hypPCForm.NavigateUrl = null;
                hypPCForm.Visible = false;
                e.Row.Cells[3].Text = "Rejected";
            }
            else
            {
                HiddenField hdnFilePath = (HiddenField)e.Row.FindControl("hdnFilePath");
                if (!string.IsNullOrEmpty(hdnFilePath.Value) && File.Exists(Server.MapPath("~/" + hdnFilePath.Value)))
                {
                    hypPCForm.NavigateUrl = "~/" + hdnFilePath.Value;
                }

            }

        }
    }

    /// <summary>
    /// selectedindexchanged for the rbtnCompanyType to show label accordingly
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbtnCompanyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnCompanyType.SelectedValue == "40")
                lblInvestment.InnerHtml = "Investment for Plant & Machinery (in Lakh)";
            else
                lblInvestment.InnerHtml = "Investment for Equipment (in Lakh)";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// click for btnsubmit to determine if the user is large or msme
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            pnlmain.Visible = true;
            Panel1.Visible = false;
            tPeal.Visible = true;
            lblComType.Text = rbtnCompanyType.SelectedItem.Text;
            lblInvAmt.Text = txtInvestment.Text;
            hypApply.Enabled = true;
            decimal investmentAmt = txtInvestment.Text == "" ? 0 : Convert.ToDecimal(txtInvestment.Text);
            string unittype = IncentiveCommonFunctions.GetUnitCategory(rbtnCompanyType.SelectedValue, investmentAmt);
            lblUnit.Text = unittype;
            Session["comptype"] = rbtnCompanyType.SelectedValue;
            Session["unitCat"] = IncentiveCommonFunctions.GetUnitCategoryId(rbtnCompanyType.SelectedValue, investmentAmt);
            Session["InvAmt"] = lblInvAmt.Text;
            if (string.Equals(lblUnit.Text, "large", StringComparison.OrdinalIgnoreCase))
            {
                if (hdnUserType.Value == "1")
                {
                    hypApply.NavigateUrl = "incentives/PC_Large_Apply.aspx?uType=1";
                }
                else
                {
                    hypApply.NavigateUrl = "incentives/PC_Large_Apply.aspx";
                }
                hypOfflinePc.NavigateUrl = "incentives/PC_Large_Apply.aspx?offline=1";
            }
            else
            {
                if (hdnUserType.Value == "1")
                {
                    hypApply.NavigateUrl = "incentives/IncentivePcForm.aspx?uType=1";
                }
                else
                {
                    hypApply.NavigateUrl = "incentives/IncentivePcForm.aspx";
                }
                hypOfflinePc.NavigateUrl = "incentives/IncentivePcForm.aspx?offline=1";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void btnResubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (hdnUnitCat.Value == "39")
            {
                hypApply.NavigateUrl = "incentives/PC_Large_Apply.aspx";
            }
            else
            {
                hypApply.NavigateUrl = "incentives/IncentivePcForm.aspx";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #region "Added By Pranay Kumar"
    #region "Navigate Query URL According to Large/MSME"
    /// <summary>
    /// 
    /// </summary>
    private void NavigateQueryURL()
    {
        try
        {
            List<QueryMgntDtls> objProposalList = new List<QueryMgntDtls>();
            QueryMgntDtls objQueryMgtDtls = new QueryMgntDtls();
            objQueryMgtDtls.strAction = "QS";
            objQueryMgtDtls.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            if (hdnUnitCat.Value == "39")
            {
                objProposalList = ObjIMB.getPCLargeRaisedQueryDetails(objQueryMgtDtls).ToList();
            }
            else
            {
                objProposalList = ObjIMB.getPCMSMERaisedQueryDetails(objQueryMgtDtls).ToList();
            }

            if (objProposalList.Count > 0)
            {
                grdQuery.DataSource = objProposalList;
                grdQuery.DataBind();

                //if (objProposalList[0].strQuerytype != "")
                //{
                //    string strCurrQueryStatus = Convert.ToString(objProposalList[0].strQuerytype.Split('~')[1]);
                //    string vchAppNO = Convert.ToString(objProposalList[0].strQuerytype.Split('~')[0]);
                //    if (strCurrQueryStatus == "--")
                //    {
                //        hypQueryDtls.Text = "--";
                //    }
                //    else if (strCurrQueryStatus == "Completed")
                //    {
                //        hypQueryDtls.NavigateUrl = "incentives/PC_Query_Revert.aspx?UnitCat=" + hdnUnitCat.Value + "&AppNo=" + vchAppNO;
                //        hypQueryDtls.CssClass = "btn btn-success btn-sm";
                //        hypQueryDtls.Text = "<i class='fa fa-eye' aria-hidden='true'></i>";
                //    }
                //    else if (strCurrQueryStatus == "QUERY RAISED")
                //    {
                //        hypQueryDtls.NavigateUrl = "incentives/PC_Query_Revert.aspx?UnitCat=" + hdnUnitCat.Value + "&AppNo=" + vchAppNO;
                //        hypQueryDtls.Text = strCurrQueryStatus;
                //        hypQueryDtls.CssClass = "btn btn-success btn-sm";
                //    }
                //    else if (strCurrQueryStatus == "QUERY RESPONDED")
                //    {
                //        hypQueryDtls.NavigateUrl = "incentives/PC_Query_Revert.aspx?UnitCat=" + hdnUnitCat.Value + "&AppNo=" + vchAppNO;
                //        hypQueryDtls.Text = strCurrQueryStatus;
                //        hypQueryDtls.CssClass = "label-warning label label-default";
                //    }
                //}
            }

            // hypQueryDtls.NavigateUrl = "incentives/PC_Query_Revert.aspx?UnitCat=" + hdnUnitCat.Value;            

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion

    #region "GridView RowDataBound"
    protected void grdQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hypQueryDtls = (e.Row.FindControl("hypQueryDtls") as HyperLink);
            // hypQueryDtls.NavigateUrl="~/Portal/Proposal/QueryProposalRevert.aspx?ProposalNo=" + strProposalNo + "&linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "";
            string strCurrQueryStatus = Convert.ToString(grdQuery.DataKeys[e.Row.RowIndex].Values[1]);
            string vchAppNO = Convert.ToString(grdQuery.DataKeys[e.Row.RowIndex].Values[0]);

            if (strCurrQueryStatus == "--")
            {
                hypQueryDtls.Text = "--";
            }
            else if (strCurrQueryStatus == "Completed")
            {
                hypQueryDtls.NavigateUrl = "incentives/PC_Query_Revert.aspx?UnitCat=" + hdnUnitCat.Value + "&AppNo=" + vchAppNO;
                hypQueryDtls.CssClass = "btn btn-success btn-sm";
                hypQueryDtls.Text = "<i class='fa fa-eye' aria-hidden='true'></i>";
            }
            else if (strCurrQueryStatus == "QUERY RAISED")
            {
                hypQueryDtls.NavigateUrl = "incentives/PC_Query_Revert.aspx?UnitCat=" + hdnUnitCat.Value + "&AppNo=" + vchAppNO;
                hypQueryDtls.Text = strCurrQueryStatus;
                hypQueryDtls.CssClass = "btn btn-success btn-sm";
            }
            else if (strCurrQueryStatus == "QUERY RESPONDED")
            {
                hypQueryDtls.NavigateUrl = "incentives/PC_Query_Revert.aspx?UnitCat=" + hdnUnitCat.Value + "&AppNo=" + vchAppNO;
                hypQueryDtls.Text = strCurrQueryStatus;
                hypQueryDtls.CssClass = "label-warning label label-default";
            }

        }
    }
    #endregion
    #endregion
}