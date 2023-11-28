/*
 * File Name : PCFormView.aspx.cs
 * Class Name : incentives_PCFormView
 * Created On : 6th September 2017
 * Created By : Ritika Lath
 * Description : Complete details of the view page
 * 
 */

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;

public partial class incentives_PCFormView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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
            FillDropDown();
            txtFromDate.Value = DateTime.Today.AddDays(-7).ToString("dd-MMM-yyyy");
            txtToDate.Value = DateTime.Today.ToString("dd-MMM-yyyy");
            FillGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
        }
    }

    private void FillDropDown()
    {
        IncentiveMasterBusinessLayer objBuisness = new IncentiveMasterBusinessLayer();
        DataSet objds = objBuisness.BindDropdown("appt");
        if (objds != null)
        {
            drpApplicationType.Items.Clear();
            drpApplicationType.DataSource = objds.Tables[0];
            drpApplicationType.DataTextField = "vchName";
            drpApplicationType.DataValueField = "slno";
            drpApplicationType.DataBind();
            drpApplicationType.Items.Insert(0, new ListItem("-Select Application Type-", "0"));
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        hdnPgindex.Value = "1";
        ddlNoOfRec.SelectedValue = "10";
        FillGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
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
            //ErrorHandler.WriteError(ex);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage("4") + "');", true);
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
            // ErrorHandler.WriteError(ex);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage("4") + "');", true);
        }
    }
    #endregion

    protected void grdPcApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strCommandName = e.CommandName;
        int intRowIndex = Convert.ToInt32(e.CommandArgument);
        if (string.Equals(strCommandName, "d", StringComparison.OrdinalIgnoreCase))
        {
            Incentive_PCMaster objMaster = new Incentive_PCMaster();
            objMaster.strActionCode = "d";
            #region set properties
            objMaster.intAppFor = 0;
            objMaster.strChngIn = string.Empty;
            objMaster.intAppNo = Convert.ToInt32(grdPcApplication.DataKeys[intRowIndex].Value);
            objMaster.strEINEMIIPMTNo = string.Empty;
            objMaster.strUAN = string.Empty;
            objMaster.strCompName = string.Empty;
            objMaster.intUnitCat = 0;
            objMaster.intUnitType = 0;
            objMaster.intOrgType = 0;
            objMaster.strOwnerName = string.Empty;
            objMaster.intOwnerCode = 0;
            objMaster.strAddr = string.Empty;
            objMaster.strPhNo = string.Empty;
            objMaster.strFaxNo = string.Empty;
            objMaster.strEmail = string.Empty;
            objMaster.strWebsite = string.Empty;
            objMaster.strOffcAddr = string.Empty;
            objMaster.strOffcEmail = string.Empty;
            objMaster.strOffcFaxNo = string.Empty;
            objMaster.strOffcPhNo = string.Empty;
            objMaster.strOffcWebsite = string.Empty;
            objMaster.strUnitLoc = string.Empty;
            objMaster.dtmFFCI = string.Empty;
            objMaster.strInvestIn = string.Empty;
            objMaster.strInvestMode = string.Empty;
            //objMaster.decFixedCapital = 0.00M;
            objMaster.decWorkingCapital = 0.00M;
            //objMaster.decSelfFinance = 0.00M;
            //objMaster.decBorrowFinance = 0.00M;
            objMaster.intManaregailSkill = 0;
            objMaster.intSupervisor = 0;
            objMaster.intSkilled = 0;
            objMaster.intSemiSkilled = 0;
            objMaster.intUnskilled = 0;
            objMaster.intStTotal = 0;
            objMaster.intStTotal = 0;
            objMaster.intWomen = 0;
            objMaster.intDisabled = 0;
            objMaster.strProductCode = string.Empty;
            objMaster.strProductName = string.Empty;
            objMaster.intIsPwrReq = 0;
            objMaster.strXml = string.Empty;
            objMaster.intCreatedBy = 1;
            #endregion
            int intRetValue = 0;
            IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
            intRetValue = objBuisnessLayer.Incentive_PcDetails_AED(objMaster);
            if (intRetValue == 3)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", string.Format("alert('{0}');", Messages.ShowMessage(intRetValue.ToString())), true);
                FillGrid(Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
            }
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
                if (Convert.ToInt32(hdnPgindex.Value) > 1)
                    Rowid = (Convert.ToInt32(hdnPgindex.Value) - 1) * Convert.ToInt32(ddlNoOfRec.SelectedValue) + e.Row.DataItemIndex + 1;
                else
                    Rowid = e.Row.DataItemIndex + 1;
                e.Row.Cells[0].Text = Rowid.ToString();

                HiddenField hdnApplyFlag = (HiddenField)e.Row.FindControl("hdnApplyFlag");
                HyperLink HypEdit = (HyperLink)e.Row.FindControl("HypEdit");
                LinkButton lnkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                Label lblApplyFlag = (Label)e.Row.FindControl("lblApplyFlag");
                if (hdnApplyFlag.Value == "0")
                {
                    HypEdit.Visible = true;
                    lnkDelete.Visible = true;
                }
                else
                {
                    lblApplyFlag.Visible = true;
                    lblApplyFlag.Text = "Applied";
                }

                HypEdit.NavigateUrl = "IncentivePCForm.aspx?Id=" + grdPcApplication.DataKeys[e.Row.RowIndex].Values[0].ToString() + "&hdn=" + hdnPgindex.Value + "&pSize=" + ddlNoOfRec.SelectedValue;
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Style["text-align"] = "center";
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('" + Messages.ShowMessage("4") + "');", true);
        }
    }

    private void FillGrid(int intpageIndex, int intPageSize)
    {
        grdPcApplication.DataSource = null;
        grdPcApplication.DataBind();
        PcSearch objSearch = new PcSearch()
        {
            strActionCode = "view",
            intPageSize = intPageSize,
            intPageIndex = intpageIndex,
            intAppFor = 0,
            strFromDate = DateTime.Today.AddDays(-7).ToShortDateString(),
            strToDate = DateTime.Today.ToShortDateString()
        };

        if (drpApplicationType.SelectedIndex > 0)
        {
            objSearch.intAppFor = Convert.ToInt32(drpApplicationType.SelectedValue);
        }
        if (!string.IsNullOrEmpty(txtFromDate.Value))
        {
            objSearch.strFromDate = txtFromDate.Value;
        }
        if (!string.IsNullOrEmpty(txtToDate.Value))
        {
            objSearch.strToDate = txtToDate.Value;
        }
        DataSet objds = new DataSet();
        DataTable dtPcForm = new DataTable();
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        objds = objBuisnessLayer.Incentive_PcForm_View(objSearch);
        if (objds != null)
        {
            dtPcForm = objds.Tables[0];
            grdPcApplication.DataSource = dtPcForm;
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
                CommonFunctions.PopulatePager(rptPager, Convert.ToInt32(dtPcForm.Rows[0]["rowcnt"]), Convert.ToInt32(hdnPgindex.Value), Convert.ToInt32(ddlNoOfRec.SelectedValue));
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
                lblDetails.Text = intStartIndex.ToString() + "-" + intEndIndex.ToString() + " of " + dtPcForm.Rows[0]["rowcnt"].ToString();
            }
        }
    }
}