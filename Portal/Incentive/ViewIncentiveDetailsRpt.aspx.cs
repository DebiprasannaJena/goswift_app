/*
 File Name         : ViewIncentiveDetailsRpt.aspx.cs
 Description       : To show the details of the incentive application as per the link clicked in the dashboard
 Created by        : Ritika lath
 Created On        : 11th December 2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Data;

public partial class Portal_Incentive_ViewIncentiveDetailsRpt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                FillDropDown();
                if (Request.QueryString["uType"] != null)
                {
                    drpUnitType.SelectedValue = Request.QueryString["uType"].ToString();
                }
                if (Request.QueryString["Did"] != null)
                {
                    drpDistrict.SelectedValue = Request.QueryString["Did"].ToString();
                }
                if (Request.QueryString["year"] != null)
                {
                    drpYear.SelectedValue = Request.QueryString["year"].ToString();
                }
                if (Request.QueryString["policy"] != null)
                {
                    drpPolicy.SelectedValue = Request.QueryString["policy"].ToString();
                }
                if (Request.QueryString["type"] != null)
                {
                    int intType = Convert.ToInt32(Request.QueryString["type"]);
                    switch (intType)
                    {
                        case (int)enDashboardAppType.Total: //total application without any filter
                            drpStatus.SelectedValue = "0";
                            break;
                        case (int)enDashboardAppType.Under_Processing: //under processing
                        case (int)enDashboardAppType.Disburse: //sanctioned disbursed
                        case (int)enDashboardAppType.Pending: //sanctioned pending
                        case (int)enDashboardAppType.Rejected: //rejected
                        case (int)enDashboardAppType.Sanctioned: //sanctioned
                            drpStatus.SelectedValue = intType.ToString();
                            break;
                        case (int)enDashboardAppType.Exemption: //application under IPR exemption
                        case (int)enDashboardAppType.Reiumbursement: //application under IPR reimbursement
                            drpIncentiveType.SelectedValue = intType.ToString();
                            break;
                        default:
                            drpStatus.SelectedValue = "0";
                            break;
                    }
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
            catch (Exception ex)
            {
                Util.LogError(ex, "Incentive");
            }
        }
    }

    private void FillDropDown()
    {
        drpStatus.Items.Add(new ListItem("-Select Status-", "0"));
        drpStatus.Items.Add(new ListItem("Under Processing", ((int)enDashboardAppType.Under_Processing).ToString()));
        drpStatus.Items.Add(new ListItem("Rejected", ((int)enDashboardAppType.Rejected).ToString()));
        drpStatus.Items.Add(new ListItem("Sanctioned", ((int)enDashboardAppType.Sanctioned).ToString()));
        drpStatus.Items.Add(new ListItem("Sanctioned Pending", ((int)enDashboardAppType.Pending).ToString()));
        drpStatus.Items.Add(new ListItem("Sanctioned Disbursed", ((int)enDashboardAppType.Disburse).ToString()));

        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        if (Session["desId"].ToString() == "127" || Session["desId"].ToString() == "97")
        {
            DataSet objDa = objBuisnessLayer.BindDropdown("prpt");
            if (objDa != null && objDa.Tables.Count > 0)
            {
                FillDropDown(objDa.Tables[0], "Unit Type", drpUnitType);
                FillDropDown(objDa.Tables[1], "District", drpDistrict);
                FillDropDown(objDa.Tables[2], "Policy", drpPolicy);
            }
        }
        else
        {
            DataSet objDa = objBuisnessLayer.BindDropdown("rpt");
            if (objDa != null && objDa.Tables.Count > 0)
            {
                FillDropDown(objDa.Tables[0], "Unit Type", drpUnitType);
                FillDropDown(objDa.Tables[1], "District", drpDistrict);
                FillDropDown(objDa.Tables[2], "Policy", drpPolicy);
            }
        }
        if (Session["desId"].ToString() == "10" || Session["desId"].ToString() == "126")
        {
            drpDistrict.SelectedValue = Session["Pealuserid"].ToString();
            drpDistrict.Enabled = false;
        }

        drpIncentiveType.Items.Add(new ListItem("-Select Incentive Type-", "0"));
        drpIncentiveType.Items.Add(new ListItem("Exemption", ((int)enDashboardAppType.Exemption).ToString()));
        drpIncentiveType.Items.Add(new ListItem("Reiumbursement", ((int)enDashboardAppType.Reiumbursement).ToString()));

        for (int i = 2010; i <= DateTime.Today.Year; i++)
        {
            drpYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        drpYear.Items.Insert(0, new ListItem("-Select Year-", "0"));
        drpYear.SelectedValue = DateTime.Today.Year.ToString();


    }

    /// <summary>
    /// Function to bind the drodown, main code to bind the dropdown
    /// </summary>
    /// <param name="objDt">Datatable with all values</param>
    /// <param name="strHeaderType">type of data in dropdown</param>
    /// <param name="objDropdown">dropdown to bind</param>
    private void FillDropDown(DataTable objDt, string strHeaderType, DropDownList objDropdown)
    {
        objDropdown.Items.Clear();
        if (objDt != null && objDt.Rows.Count > 0)
        {
            objDropdown.DataSource = objDt;
            objDropdown.DataTextField = "NAME";
            objDropdown.DataValueField = "ID";
            objDropdown.DataBind();
        }
        objDropdown.Items.Insert(0, new ListItem(string.Format("-Select {0}-", strHeaderType), "0"));
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

    /// <summary>
    /// rowdatabound event for grdSupervisorShow. It will set serial no in first column based on paging 
    /// and set navigateurl for edit hyperlink
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// click event for btnSearch
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        hdnPgindex.Value = "1";
        ddlNoOfRec.SelectedValue = "10";
        FillGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
    }

    protected void lnkExport_Click(object sender, EventArgs e)
    {
        if (grdIncentive.Rows.Count > 0)
        {
            IncentiveCommonFunctions.ExportToExcel("IncentiveAppDetails", grdIncentive, lblSearchDetails.Text, string.Empty, string.Empty, true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(lnkExport, this.GetType(), "OnClick", "jAlert('No records to transfer to excel','GO-SWIFT');", true);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        if (grdIncentive.Rows.Count > 0)
        {
            IncentiveCommonFunctions.CreatePdf("IncentiveAppDetails", divIncentiveDetails);
        }
        else
        {
            ScriptManager.RegisterStartupScript(lnkExport, this.GetType(), "OnClick", "jAlert('No records to transfer to excel','GO-SWIFT');", true);
        }
    }


    private void FillGrid(int intPageIndex, int intPageSize)
    {
        grdIncentive.DataSource = null;
        grdIncentive.DataBind();
        lblDetails.Text = string.Empty;
        lnkExport.Visible = false;
        lnkPdf.Visible = false;
        ancPrint.Visible = false;
        lblSearchDetails.Text = string.Empty;
        InctSearch objSearch = new InctSearch()
        {
            intPageIndex = intPageIndex,
            intPageSize = intPageSize,
            strActionCode = "view",
            strApplicationNo = txtAppNo.Text.Trim(),
            strUnitName = txtUnitName.Text.Trim(),
            intPriority = 0,
            intStatus = 0,
            intUnitType = 0,
            intDistrict = 0,
            intYear = 0,
            intUserUnitType = 0,
            intPolicyType = 0
        };

        if (rdBtnLstPriority.SelectedIndex >= 0)
        {
            objSearch.intPriority = Convert.ToInt32(rdBtnLstPriority.SelectedValue);
        }
        if (drpUnitType.SelectedIndex > 0)
        {
            objSearch.intUnitType = Convert.ToInt32(drpUnitType.SelectedValue);
        }
        if (objSearch.intUnitType == 0)
        {
            objSearch.intUserUnitType = 0;
        }
        else if ((objSearch.intUnitType == (int)IncentiveCommonFunctions.enUnitCategory.MEDIUM || objSearch.intUnitType == (int)IncentiveCommonFunctions.enUnitCategory.MICRO || objSearch.intUnitType == (int)IncentiveCommonFunctions.enUnitCategory.SMALL))
        {
            objSearch.intUserUnitType = 1;
        }
        else if (objSearch.intUnitType == (int)IncentiveCommonFunctions.enUnitCategory.LARGE)
        {
            objSearch.intUserUnitType = 2;
        }

        if (drpDistrict.SelectedIndex > 0)
        {
            objSearch.intDistrict = Convert.ToInt32(drpDistrict.SelectedValue);
        }
        if (drpYear.SelectedIndex > 0)
        {
            objSearch.intYear = Convert.ToInt32(drpYear.SelectedValue);
        }


        if (drpStatus.SelectedIndex > 0) // if one of the values from status then set value accordingly
        {
            objSearch.intStatus = Convert.ToInt32(drpStatus.SelectedValue);
        }
        else
        {
            if (Request.QueryString["type"] != null) // if not status then id ortpsa or ipr then set values
            {
                int intType = Convert.ToInt32(Request.QueryString["type"]);
                if (intType == (int)enDashboardAppType.IPR || intType == (int)enDashboardAppType.ORTPSA)
                {
                    objSearch.intStatus = intType;
                }
                else
                {
                    if (drpIncentiveType.SelectedIndex > 0)// if not status, ortpsa or ipr then check if the user has selected incentive type
                    {
                        objSearch.intStatus = Convert.ToInt32(drpIncentiveType.SelectedValue);
                    }
                }
            }

        }

        List<InctApplicationDetails_Entity> lstEntity = new List<InctApplicationDetails_Entity>();
        IncentiveMasterBusinessLayer objInctBuisness = new IncentiveMasterBusinessLayer();
        lstEntity = objInctBuisness.ViewInctApplicationDetailsRpt(objSearch);
        grdIncentive.DataSource = lstEntity;
        grdIncentive.DataBind();

        if (grdIncentive.Rows.Count == 0)
        {
            ddlNoOfRec.Visible = false;
            rptPager.Visible = false;
            hdnPgindex.Value = "1";
        }
        else
        {
            lnkExport.Visible = true;
            lnkPdf.Visible = true;
            ancPrint.Visible = true;
            string strSearchType = string.Empty;
            int intType = Convert.ToInt32(Request.QueryString["type"]);
            switch (intType)
            {
                case (int)enDashboardAppType.Total: //total application without any filter
                    strSearchType = string.Empty;
                    break;
                case (int)enDashboardAppType.Under_Processing: //under processing
                    strSearchType = " which are - Under Processing";
                    break;
                case (int)enDashboardAppType.Disburse: //sanctioned disbursed
                    strSearchType = " which are - Sanctioned Disbursed";
                    break;
                case (int)enDashboardAppType.Pending: //sanctioned pending
                    strSearchType = " which are - Sanctioned Pending";
                    break;
                case (int)enDashboardAppType.Rejected: //rejected
                    strSearchType = " which are - Rejected";
                    break;
                case (int)enDashboardAppType.Sanctioned: //sanctioned
                    strSearchType = " which are - Sanctioned";
                    break;
                case (int)enDashboardAppType.Exemption: //application under IPR exemption
                    strSearchType = " which are - under IPR exemption";
                    break;
                case (int)enDashboardAppType.Reiumbursement: //application under IPR reimbursement
                    strSearchType = " which are - under IPR reimbursement";
                    break;
                case (int)enDashboardAppType.IPR: //application under IPR reimbursement
                    strSearchType = " which are - under IPR 2015";
                    break;
                case (int)enDashboardAppType.ORTPSA: //application under IPR reimbursement
                    strSearchType = " which undergoes ORTPSA";
                    break;
            }
            lblSearchDetails.Text = "List of all incentive application" + strSearchType;
            ddlNoOfRec.Visible = true;
            rptPager.Visible = true;
            CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(lstEntity[0].intRowCount), Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));

            /****************code to show paging details in the label************/
            int intPIndex = Convert.ToInt32(hdnPgindex.Value);
            int intStartIndex = 1, intEndIndex = 0;
            int intPSize = Convert.ToInt32(ddlNoOfRec.SelectedValue);
            intStartIndex = ((intPIndex - 1) * intPSize) + 1;
            if (intPSize == grdIncentive.Rows.Count)
            {
                intEndIndex = intPSize * intPIndex;
            }
            else
            {
                intEndIndex = grdIncentive.Rows.Count + (intPSize * (intPIndex - 1));

            }
            lblDetails.Text = intStartIndex.ToString() + "-" + intEndIndex.ToString() + " of " + Convert.ToInt32(lstEntity[0].intRowCount).ToString();
        }
    }
}