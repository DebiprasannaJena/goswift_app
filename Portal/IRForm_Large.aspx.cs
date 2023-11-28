using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;
using EntityLayer.Proposal;
using System.Collections.Generic;
using BusinessLogicLayer.Proposal;

public partial class Portal_IRForm_Large : System.Web.UI.Page
{
    #region Constant Values
    string gStrOriginal = "For Original Enterprise";
    string gStrEmd = "For E / M / D";
    public enum enGridType
    {
        officer = 1,
        termLoan = 2,
        workingCapital = 3,
        Approval = 4,
        statutoryclearence = 5,
        Products = 6,
        cInvest = 7,
        problems = 8,
        otherEnterprise = 9,
        IPR = 10
    };
    public enum enGridName
    {
        officer,
        termLoan,
        workingCapital,
        Approval,
        statutoryclearence,
        Products,
        cInvest,
        problems,
        otherEnterprise,
        IPR
    };
    #endregion

    /// <summary>
    /// page load event 
    /// get all the application details from pc form
    /// set up textarea 
    /// bind all dropdown
    /// bind all add more grids
    /// set all label as per new and amendement
    /// bind all other gridview necessary
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetUpTextArea();
            BindDropDown();
            BindSector();
            BindDistrict(ddlDistrict);
            if (Request.QueryString["id"] != null)
            {
                GetApplicationDetails();
            }
            if (Request.QueryString["type"] != null)
            {
                pnlmain.Enabled = false;
                btnCancel.Visible = false;
                btnConfirm.Visible = false;
                GetIrFormDetails();
            }
            BindAddMoreGrids((int)enGridType.Approval, enGridName.Approval.ToString(), grdApproval);
            BindAddMoreGrids((int)enGridType.officer, enGridName.officer.ToString(), grdOffice);
            BindAddMoreGrids((int)enGridType.Products, enGridName.Products.ToString(), grdProducts);
            BindAddMoreGrids((int)enGridType.statutoryclearence, enGridName.statutoryclearence.ToString(), grdStatutoryClearence);
            BindAddMoreGrids((int)enGridType.termLoan, enGridName.termLoan.ToString(), grdTermLoan);
            BindAddMoreGrids((int)enGridType.workingCapital, enGridName.workingCapital.ToString(), grdWorkingCapital);
            BindAddMoreGrids((int)enGridType.otherEnterprise, enGridName.otherEnterprise.ToString(), gvLOCDetails);
            BindAddMoreGrids((int)enGridType.IPR, enGridName.IPR.ToString(), grdIncentiveApplied);
            ShowProductDetailsByUnitCond();
            if (drpApplicationType.SelectedIndex != 1)
            {
                SetAllLabels(gStrOriginal);
            }
            else
            {
                SetAllLabels(gStrEmd);
            }



        }
    }

    private void GetIrFormDetails()
    {
        DataSet objds = new DataSet();
        PcSearch objSearch = new PcSearch()
        {
            strActionCode = "ir",
            intPageIndex = 0,
            intPageSize = 0,
            strFromDate = string.Empty,
            strToDate = string.Empty,
            intAppFor = Convert.ToInt32(Request.QueryString["id"]),
        };
        IncentiveMasterBusinessLayer objBuisness = new IncentiveMasterBusinessLayer();
        objds = objBuisness.Incentive_PcForm_Large_View(objSearch);

        if (objds != null)
        {
            DataTable dtDetails = new DataTable();
            dtDetails = objds.Tables[0];

            DataRow dRow = dtDetails.Rows[0];
            txtPollutionControl.Text = dRow["vchControlMeasures"].ToString();
            txtSafety.Text = dRow["vchIndSafety"].ToString();
            txtPowerLoad.Text = dRow["vchPowerLoad"].ToString();
            txtCpp.Text = dRow["vchCppDetails"].ToString();
            txtRemarks.Text = dRow["vchRemarks"].ToString();
            txtSuggestions.Text = dRow["vchSuggestions"].ToString();
            txtDateOfInspection.Value = dRow["vchSuggestions"].ToString();

            dtDetails = new DataTable();
            dtDetails = objds.Tables[1];
            grdOffice.DataSource = dtDetails;
            grdOffice.DataBind();
            grdOffice.Columns[grdOffice.Columns.Count - 1].Visible = false;

            dtDetails = new DataTable();
            dtDetails = objds.Tables[2];
            grdProducts.DataSource = dtDetails;
            grdProducts.DataBind();
            grdProducts.Columns[grdProducts.Columns.Count - 1].Visible = false;

            dtDetails = new DataTable();
            dtDetails = objds.Tables[3];
            grdCapitalInvestment.DataSource = dtDetails;
            grdCapitalInvestment.DataBind();

            dtDetails = new DataTable();
            dtDetails = objds.Tables[4];
            grdTermLoan.DataSource = dtDetails;
            grdTermLoan.DataBind();
            grdTermLoan.Columns[grdTermLoan.Columns.Count - 1].Visible = false;

            dtDetails = new DataTable();
            dtDetails = objds.Tables[5];
            grdWorkingCapital.DataSource = dtDetails;
            grdWorkingCapital.DataBind();
            grdWorkingCapital.Columns[grdWorkingCapital.Columns.Count - 1].Visible = false;

            dtDetails = new DataTable();
            dtDetails = objds.Tables[6];
            grdApproval.DataSource = dtDetails;
            grdApproval.DataBind();
            grdApproval.Columns[grdApproval.Columns.Count - 1].Visible = false;

            dtDetails = new DataTable();
            dtDetails = objds.Tables[7];
            grdStatutoryClearence.DataSource = dtDetails;
            grdStatutoryClearence.DataBind();
            grdStatutoryClearence.Columns[grdStatutoryClearence.Columns.Count - 1].Visible = false;

            dtDetails = new DataTable();
            dtDetails = objds.Tables[8];
            grdProblems.DataSource = dtDetails;
            grdProblems.DataBind();

            dtDetails = new DataTable();
            dtDetails = objds.Tables[9];
            gvLOCDetails.DataSource = dtDetails;
            gvLOCDetails.DataBind();
            gvLOCDetails.Columns[gvLOCDetails.Columns.Count - 1].Visible = false;

            dtDetails = new DataTable();
            dtDetails = objds.Tables[10];
            grdIncentiveApplied.DataSource = dtDetails;
            grdIncentiveApplied.DataBind();
            grdIncentiveApplied.Columns[grdIncentiveApplied.Columns.Count - 1].Visible = false;
        }
    }
    #region set up textarea
    /// <summary>
    /// Function to set up all the textarea
    /// </summary>
    private void SetUpTextArea()
    {
        CommonTextAreaSetUp(txtEnterpriseAddress, lblRemark);
        CommonTextAreaSetUp(txtOfficeAddress, lblOfficeAddress);
        CommonTextAreaSetUp(txtPollutionControl, lblOriginal);
        CommonTextAreaSetUp(txtSafety, lblSafety);
        CommonTextAreaSetUp(txtPowerLoad, lblPowerRemarks);
        CommonTextAreaSetUp(txtCpp, lblCpp);
        CommonTextAreaSetUp(txtRemarks, lblEndRemarks);
        CommonTextAreaSetUp(txtSuggestions, lblSuggestion);
    }

    /// <summary>
    /// main function to bind the keyup and onchange event for all textarea
    /// </summary>
    /// <param name="txt"></param>
    /// <param name="Lbl"></param>
    private void CommonTextAreaSetUp(TextBox txt, Label Lbl)
    {
        txt.Attributes.Add("onkeyup", "return CheckLengthKeyUp('" + txt.ClientID + "','" + Lbl.ClientID + "',200);");
        txt.Attributes.Add("onchange", "return checkLength('" + txt.ClientID + "','" + Lbl.ClientID + "',200);");
    }
    #endregion

    #region gridview events
    /// <summary>
    /// rowdatabound event for grdProblems to set up all the textarea
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdProblems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtProblems = (TextBox)e.Row.FindControl("txtProblems");
            Label lblProblems = (Label)e.Row.FindControl("lblProblems");
            CommonTextAreaSetUp(txtProblems, lblProblems);
        }
    }

    /// <summary>
    /// Common row datacommand event for all the grid which have add more button
    /// calls the AddNewItemInGrid function 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdCommon_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string strCommandName = e.CommandName;
        int intRowIndex = Convert.ToInt32(e.CommandArgument);
        LinkButton lnkAdd = (LinkButton)e.CommandSource;
        GridViewRow GRow = (GridViewRow)lnkAdd.NamingContainer;
        GridView grd = (GridView)GRow.NamingContainer;
        string grdName = grd.ID;
        if (string.Equals("add", strCommandName, StringComparison.OrdinalIgnoreCase))
        {
            if (grdName == grdOffice.ID)
            {
                AddNewItemInGrid(GRow, (int)enGridType.officer, enGridName.officer.ToString(), grd);
            }
            else if (grdName == grdProducts.ID)
            {
                AddNewItemInGrid(GRow, (int)enGridType.Products, enGridName.Products.ToString(), grd);
            }
            else if (grdName == grdTermLoan.ID)
            {
                AddNewItemInGrid(GRow, (int)enGridType.termLoan, enGridName.termLoan.ToString(), grd);
            }
            else if (grdName == grdWorkingCapital.ID)
            {
                AddNewItemInGrid(GRow, (int)enGridType.workingCapital, enGridName.workingCapital.ToString(), grd);
            }
            else if (grdName == grdApproval.ID)
            {
                AddNewItemInGrid(GRow, (int)enGridType.Approval, enGridName.Approval.ToString(), grd);
            }
            else if (grdName == grdStatutoryClearence.ID)
            {
                AddNewItemInGrid(GRow, (int)enGridType.statutoryclearence, enGridName.statutoryclearence.ToString(), grd);
            }
            else if (grdName == gvLOCDetails.ID)
            {
                AddNewItemInGrid(GRow, (int)enGridType.otherEnterprise, enGridName.otherEnterprise.ToString(), grd);
            }
            else if (grdName == grdIncentiveApplied.ID)
            {
                AddNewItemInGrid(GRow, (int)enGridType.IPR, enGridName.IPR.ToString(), grd);
            }
        }
    }

    /// <summary>
    /// Selected index change event for the dropdown in Approval Authority in approval gridview
    /// If other is selected then show textbox and hide gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void drpApprovalAuthority_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drpAuthority = (DropDownList)sender;
        if (drpAuthority.SelectedValue == "3")
        {
            GridViewRow objRow = (GridViewRow)drpAuthority.NamingContainer;
            TextBox txtOthers = (TextBox)objRow.FindControl("txtOthers");
            drpAuthority.Visible = false;
            txtOthers.Visible = true;
        }
    }

    /// <summary>
    /// row databound event for grdApproval 
    /// will set the value for dropdown and if the value in 3 then show textbox and hide dropdownlist
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList drpApprovalAuthority = (DropDownList)e.Row.FindControl("drpApprovalAuthority");
            HiddenField hdnAuthority = (HiddenField)e.Row.FindControl("hdnAuthority");
            TextBox txtOthers = (TextBox)e.Row.FindControl("txtOthers");
            drpApprovalAuthority.SelectedValue = hdnAuthority.Value;
            if (hdnAuthority.Value == "3")
            {
                drpApprovalAuthority.Visible = false;
                txtOthers.Visible = true;
            }
        }
    }

    /// <summary>
    /// rowdatabound event for grdStatutoryClearence
    /// set the value for the clearence dropdown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdStatutoryClearence_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList drpClearence = (DropDownList)e.Row.FindControl("drpClearence");
            HiddenField hdnClearence = (HiddenField)e.Row.FindControl("hdnClearence");
            drpClearence.SelectedValue = hdnClearence.Value;
        }
    }

    /// <summary>
    /// RowCreatedEvent for the grdTermloan. Same function is used for working capital grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTermLoan_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow objHeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableHeaderCell objSlNoCell = new TableHeaderCell();
            objSlNoCell.RowSpan = 2;
            objSlNoCell.Text = "Sl#";
            objHeaderRow.Cells.Add(objSlNoCell);
            TableHeaderCell objHcInstitute = new TableHeaderCell() { RowSpan = 2, Text = "Name of Financial Institution" };
            objHeaderRow.Cells.Add(objHcInstitute);
            TableHeaderCell objHcLocation = new TableHeaderCell() { ColumnSpan = 2, Text = "Location" };
            objHeaderRow.Cells.Add(objHcLocation);
            TableHeaderCell objHcAmt = new TableHeaderCell() { RowSpan = 2, Text = "Term Loan Amount" };
            objHeaderRow.Cells.Add(objHcAmt);
            TableHeaderCell objhcSanctionDate = new TableHeaderCell() { RowSpan = 2, Text = "Sanction Date" };
            objHeaderRow.Cells.Add(objhcSanctionDate);
            TableHeaderCell objHcAvailedAmt = new TableHeaderCell() { RowSpan = 2, Text = "Availed Amount" };
            objHeaderRow.Cells.Add(objHcAvailedAmt);
            TableHeaderCell objHcAvailedDate = new TableHeaderCell() { RowSpan = 2, Text = "Availed Date" };
            objHeaderRow.Cells.Add(objHcAvailedDate);
            TableHeaderCell objHcTakeAction = new TableHeaderCell() { RowSpan = 2, Text = "Action" };
            objHeaderRow.Cells.Add(objHcTakeAction);
            objGridView.Controls[0].Controls.AddAt(0, objHeaderRow);
        }
    }

    /// <summary>
    /// rowdatabound event for grdTermLoan
    /// hide columns that are not required
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTermLoan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtSanctionDate = (TextBox)e.Row.FindControl("txtSanctionDate");
            TextBox txtAvailedDate = (TextBox)e.Row.FindControl("txtAvailedDate");
            txtAvailedDate.Attributes.Add("readonly", "readonly");
            txtSanctionDate.Attributes.Add("readonly", "readonly");
        }
    }
    #endregion

    #region page events
    /// <summary>
    /// if the verification checkbox is checked then user needs to enter the aadhaar number else hide the columns
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkVerification_CheckChanged(object sender, EventArgs e)
    {
        if (chkverification.Checked)
        {
            grdOffice.Columns[4].Visible = true;
        }
        else
        {
            grdOffice.Columns[4].Visible = false;
        }
    }

    /// <summary>
    /// if the unitcondition radio button was selected
    /// it will call the function ShowProductDetailsByUnitCond
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rdBtnUntiCond_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowProductDetailsByUnitCond();
    }

    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubSector(ddlSector.SelectedValue);
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlock(ddlDistrict.SelectedValue);
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        IRDetails objIrDetails = new IRDetails();
        objIrDetails.intAppNo = Convert.ToInt32(Request.QueryString["id"]);
        objIrDetails.strInspectionReport = txtDateOfInspection.Value;
        objIrDetails.ControlMeasures = txtPollutionControl.Text;
        objIrDetails.IndSafety = txtSafety.Text;
        objIrDetails.PowerLoad = txtPowerLoad.Text;
        objIrDetails.CppDetails = txtCpp.Text;
        objIrDetails.strRemarks = txtRemarks.Text;
        objIrDetails.strSuggestions = txtSuggestions.Text;
        objIrDetails.inCreatedBy = Convert.ToInt32(Session["userId"]);

        CommonFunctions objFunction = new CommonFunctions();

        /*Officer*/
        DataTable dtOfficer = CreateChildTable((int)enGridType.officer);
        dtOfficer.TableName = "Officer";
        for (int cnt = 0; cnt < grdOffice.Rows.Count; cnt++)
        {
            DataRow dRow = dtOfficer.NewRow();
            GridViewRow GRow = grdOffice.Rows[cnt];
            TextBox txtOfficerName = (TextBox)GRow.FindControl("txtOfficerName");
            TextBox txtDesignation = (TextBox)GRow.FindControl("txtDesignation");
            TextBox txtOrganization = (TextBox)GRow.FindControl("txtOrganization");
            dRow["strOfficer"] = txtOfficerName.Text;
            dRow["strDesignation"] = txtDesignation.Text;
            dRow["strAuthority"] = txtOrganization.Text;
            if (GRow.FindControl("txtAadhaarNo") != null)
            {
                TextBox txtAadhaarNo = (TextBox)GRow.FindControl("txtAadhaarNo");
                dRow["AadhaarNo"] = txtAadhaarNo.Text;
            }
            else
            {
                dRow["AadhaarNo"] = string.Empty;
            }
            dtOfficer.Rows.Add(dRow);
        }

        if (dtOfficer.Rows.Count > 0)
        {
            objIrDetails.strXmlOfficer = objFunction.GetSTRXMLResult(dtOfficer);
        }

        DataTable dtProducts = CreateChildTable((int)enGridType.Products);
        dtProducts.TableName = "Products";
        for (int cnt = 0; cnt < grdProducts.Rows.Count; cnt++)
        {
            DataRow dRow = dtProducts.NewRow();
            GridViewRow GRow = grdProducts.Rows[cnt];
            TextBox txtItemProduct = (TextBox)GRow.FindControl("txtItemProduct");
            TextBox txtItemCode = (TextBox)GRow.FindControl("txtItemCode");
            TextBox txtQuantity = (TextBox)GRow.FindControl("txtQuantity");
            TextBox txtCost = (TextBox)GRow.FindControl("txtCost");
            TextBox txtUnit = (TextBox)GRow.FindControl("txtUnit");
            dRow["item"] = txtItemProduct.Text;
            dRow["Code"] = txtItemCode.Text;
            dRow["Qty"] = string.IsNullOrEmpty(txtQuantity.Text) ? "0.00" : txtQuantity.Text;
            dRow["Unit"] = txtUnit.Text;
            dRow["Cost"] = string.IsNullOrEmpty(txtCost.Text) ? "0.00" : txtCost.Text;
            dtProducts.Rows.Add(dRow);
        }
        if (dtProducts.Rows.Count > 0)
        {
            objIrDetails.strXmlProducts = objFunction.GetSTRXMLResult(dtProducts);
        }

        DataTable dtcapitalInvest = CreateChildTable((int)enGridType.cInvest);
        dtcapitalInvest.TableName = "cInvest";
        for (int cnt = 0; cnt < grdCapitalInvestment.Rows.Count; cnt++)
        {
            DataRow dRow = dtcapitalInvest.NewRow();
            GridViewRow GRow = grdCapitalInvestment.Rows[cnt];
            TextBox txtAsPerDpr = (TextBox)GRow.FindControl("txtAsPerDpr");
            TextBox txtActualExpIncuured = (TextBox)GRow.FindControl("txtActualExpIncuured");
            dRow["slno"] = grdCapitalInvestment.DataKeys[GRow.RowIndex].Value;
            dRow["PerDpr"] = string.IsNullOrEmpty(txtAsPerDpr.Text) ? "0.00" : txtAsPerDpr.Text;
            dRow["ActualExpenditure"] = string.IsNullOrEmpty(txtActualExpIncuured.Text) ? "0.00" : txtActualExpIncuured.Text;
            dtcapitalInvest.Rows.Add(dRow);
        }
        if (dtcapitalInvest.Rows.Count > 0)
        {
            objIrDetails.strXmlCapitalInvestment = objFunction.GetSTRXMLResult(dtcapitalInvest);
        }

        DataTable dtTermLoan = CreateChildTable((int)enGridType.termLoan);
        dtTermLoan.TableName = "TermLoan";
        for (int cnt = 0; cnt < grdTermLoan.Rows.Count; cnt++)
        {
            DataRow dRow = dtTermLoan.NewRow();
            GridViewRow GRow = grdTermLoan.Rows[cnt];
            TextBox txtTlInstitue = (TextBox)GRow.FindControl("txtTlInstitue");
            TextBox txtState = (TextBox)GRow.FindControl("txtState");
            TextBox txtCity = (TextBox)GRow.FindControl("txtCity");
            TextBox txtAmount = (TextBox)GRow.FindControl("txtAmount");
            TextBox txtSanctionDate = (TextBox)GRow.FindControl("txtSanctionDate");
            TextBox txtAvailedAmount = (TextBox)GRow.FindControl("txtSanctionDate");
            TextBox txtAvailedDate = (TextBox)GRow.FindControl("txtSanctionDate");
            dRow["institute"] = txtTlInstitue.Text;
            dRow["state"] = txtState.Text;
            dRow["City"] = txtCity.Text;
            dRow["Amt"] = string.IsNullOrEmpty(txtAmount.Text) ? "0.00" : txtAmount.Text;
            dRow["SanctionDate"] = txtSanctionDate.Text;
            dRow["AvailedAmt"] = string.IsNullOrEmpty(txtAvailedAmount.Text) ? "0.00" : txtAvailedAmount.Text;
            dRow["AvailedDate"] = txtAvailedDate.Text;
            dtTermLoan.Rows.Add(dRow);
        }
        if (dtTermLoan.Rows.Count > 0)
        {
            objIrDetails.strXmlTermPlan = objFunction.GetSTRXMLResult(dtTermLoan);
        }


        DataTable dtWorkingCapital = CreateChildTable((int)enGridType.workingCapital);
        dtWorkingCapital.TableName = "WCapital";
        for (int cnt = 0; cnt < grdWorkingCapital.Rows.Count; cnt++)
        {
            DataRow dRow = dtWorkingCapital.NewRow();
            GridViewRow GRow = grdWorkingCapital.Rows[cnt];
            TextBox txtTlInstitue = (TextBox)GRow.FindControl("txtTlInstitue");
            TextBox txtState = (TextBox)GRow.FindControl("txtState");
            TextBox txtCity = (TextBox)GRow.FindControl("txtCity");
            TextBox txtAmount = (TextBox)GRow.FindControl("txtAmount");
            TextBox txtSanctionDate = (TextBox)GRow.FindControl("txtSanctionDate");
            TextBox txtAvailedAmount = (TextBox)GRow.FindControl("txtSanctionDate");
            TextBox txtAvailedDate = (TextBox)GRow.FindControl("txtSanctionDate");
            dRow["institute"] = txtTlInstitue.Text;
            dRow["state"] = txtState.Text;
            dRow["City"] = txtCity.Text;
            dRow["Amt"] = string.IsNullOrEmpty(txtAmount.Text) ? "0.00" : txtAmount.Text;
            dRow["SanctionDate"] = txtSanctionDate.Text;
            dRow["AvailedAmt"] = string.IsNullOrEmpty(txtAvailedAmount.Text) ? "0.00" : txtAvailedAmount.Text;
            dRow["AvailedDate"] = txtAvailedDate.Text;
            dtWorkingCapital.Rows.Add(dRow);
        }
        if (dtWorkingCapital.Rows.Count > 0)
        {
            objIrDetails.strXmlWorkingCapital = objFunction.GetSTRXMLResult(dtWorkingCapital);
        }


        DataTable dtApproval = CreateChildTable((int)enGridType.Approval);
        dtApproval.TableName = "dtApproval";
        for (int cnt = 0; cnt < grdApproval.Rows.Count; cnt++)
        {
            DataRow dRow = dtApproval.NewRow();
            GridViewRow GRow = grdApproval.Rows[cnt];
            TextBox txtNameOfSite = (TextBox)GRow.FindControl("txtNameOfSite");
            DropDownList drpAuthority = (DropDownList)GRow.FindControl("drpApprovalAuthority");
            TextBox txtOthers = (TextBox)GRow.FindControl("txtOthers");
            dRow["name"] = txtNameOfSite.Text;
            dRow["authority"] = drpAuthority.SelectedValue;
            dRow["others"] = txtOthers.Text;
            dRow["filename"] = string.Empty;
            dtApproval.Rows.Add(dRow);
        }
        if (dtApproval.Rows.Count > 0)
        {
            objIrDetails.strXmlApproval = objFunction.GetSTRXMLResult(dtApproval);
        }


        DataTable dtClearence = CreateChildTable((int)enGridType.statutoryclearence);
        dtClearence.TableName = "dtClearence";
        for (int cnt = 0; cnt < grdStatutoryClearence.Rows.Count; cnt++)
        {
            DataRow dRow = dtClearence.NewRow();
            GridViewRow GRow = grdStatutoryClearence.Rows[cnt];
            TextBox txtPeriod = (TextBox)GRow.FindControl("txtPeriod");
            DropDownList drpClearence = (DropDownList)GRow.FindControl("drpClearence");
            dRow["clearence"] = drpClearence.SelectedValue;
            dRow["period"] = txtPeriod.Text;
            dtClearence.Rows.Add(dRow);
        }
        if (dtClearence.Rows.Count > 0)
        {
            objIrDetails.strXmlClearence = objFunction.GetSTRXMLResult(dtClearence);
        }

        DataTable dtProblems = CreateChildTable((int)enGridType.problems);
        dtProblems.TableName = "dtProblems";
        for (int cnt = 0; cnt < grdProblems.Rows.Count; cnt++)
        {
            DataRow dRow = dtProblems.NewRow();
            GridViewRow GRow = grdProblems.Rows[cnt];
            TextBox txtProblems = (TextBox)GRow.FindControl("txtProblems");
            dRow["slno"] = grdProblems.DataKeys[GRow.RowIndex].Value;
            dRow["problems"] = txtProblems.Text;
            dtProblems.Rows.Add(dRow);
        }
        if (dtProblems.Rows.Count > 0)
        {
            objIrDetails.strXmlProblems = objFunction.GetSTRXMLResult(dtProblems);
        }

        DataTable dtOtherEnterPrise = CreateChildTable((int)enGridType.otherEnterprise);
        dtOtherEnterPrise.TableName = "dtOtherEnterPrise";
        for (int cnt = 0; cnt < gvLOCDetails.Rows.Count; cnt++)
        {
            DataRow dRow = dtOtherEnterPrise.NewRow();
            GridViewRow GRow = gvLOCDetails.Rows[cnt];
            TextBox txtUnit = (TextBox)GRow.FindControl("txtUnit");
            TextBox txtProduct = (TextBox)GRow.FindControl("txtProduct");
            TextBox txtCapacity = (TextBox)GRow.FindControl("txtCapacity");
            DropDownList ddlState = (DropDownList)GRow.FindControl("ddlState");
            DropDownList drpDistrict = (DropDownList)GRow.FindControl("ddlDistrict");
            dRow["unitname"] = txtUnit.Text;
            dRow["product"] = txtProduct.Text;
            dRow["totalCapacity"] = txtCapacity.Text;
            dRow["state"] = ddlState.SelectedValue;
            dRow["district"] = drpDistrict.SelectedValue;
            dtOtherEnterPrise.Rows.Add(dRow);
        }
        if (dtOtherEnterPrise.Rows.Count > 0)
        {
            objIrDetails.strXmlOther = objFunction.GetSTRXMLResult(dtOtherEnterPrise);
        }

        DataTable dtApplied = CreateChildTable((int)enGridType.IPR);
        dtApplied.TableName = "dtApplied";
        for (int cnt = 0; cnt < grdIncentiveApplied.Rows.Count; cnt++)
        {
            DataRow dRow = dtApplied.NewRow();
            GridViewRow GRow = grdIncentiveApplied.Rows[cnt];
            TextBox txtQuantam = (TextBox)GRow.FindControl("txtQuantam");
            TextBox txtFromDate = (TextBox)GRow.FindControl("txtFromDate");
            TextBox txtToDate = (TextBox)GRow.FindControl("txtToDate");
            DropDownList drpIpr = (DropDownList)GRow.FindControl("drpIpr");
            DropDownList drpIncentiveType = (DropDownList)GRow.FindControl("drpIncentiveType");
            dRow["type"] = drpIncentiveType.SelectedValue;
            dRow["quantam"] = txtQuantam.Text;
            dRow["fromdate"] = txtFromDate.Text;
            dRow["todate"] = txtToDate.Text;
            dRow["ipr"] = drpIpr.SelectedValue;
            dtApplied.Rows.Add(dRow);
        }
        if (dtApplied.Rows.Count > 0)
        {
            objIrDetails.strXmlApplied = objFunction.GetSTRXMLResult(dtApplied);
        }

        Incentive_PCMaster objMaster = new Incentive_PCMaster();
        objMaster.intApplyFlag = 2;
        //objMaster.strActionCode = "u";
        objMaster.intAppNo = Convert.ToInt32(Request.QueryString["id"]);
        #region set properties
        objMaster.intAppFor = Convert.ToInt32(drpApplicationType.SelectedValue);
        objMaster.strChngIn = GetCheckBoxListValues(chkLstChange);
        objMaster.intAppNo = Convert.ToInt32(lblApplicationNo.Text.Trim());
        objMaster.strEINEMIIPMTNo = txtEin.Text.Trim();
        objMaster.strCompName = txtEnterpriseName.Text.Trim();
        objMaster.intUnitCat = Convert.ToInt32(drpUnitCategory.SelectedValue);
        objMaster.intUnitType = Convert.ToInt32(drpCompanyType.SelectedValue);
        objMaster.intOrgType = Convert.ToInt32(drpOrganizationType.SelectedValue);
        objMaster.strOwnerName = txtOwnerName.Text.Trim();
        objMaster.intOwnerCode = Convert.ToInt32(drpOwnerType.SelectedValue);
        objMaster.strAddr = txtEnterpriseAddress.Text.Trim();
        objMaster.strPhNo = txtPhoneNo.Text.Trim();
        objMaster.strFaxNo = txtFax.Text.Trim();
        objMaster.strEmail = txtEmail.Text.Trim();
        objMaster.strWebsite = txtWebsite.Text.Trim();
        objMaster.strOffcAddr = txtOfficeAddress.Text.Trim();
        objMaster.strOffcEmail = txtOfficeEmail.Text.Trim();
        objMaster.strOffcFaxNo = txtOfficeFax.Text.Trim();
        objMaster.strOffcPhNo = txtOfficePhone.Text.Trim();
        objMaster.strOffcWebsite = txtOfficeWebsite.Text.Trim();
        objMaster.intSectorId = Convert.ToInt32(ddlSector.SelectedValue);
        objMaster.intSubSectorId = Convert.ToInt32(ddlSubSector.SelectedValue);
        objMaster.intBlock = Convert.ToInt32(ddlBlock.SelectedValue);
        objMaster.intDistrict = Convert.ToInt32(ddlDistrict.SelectedValue);

        objMaster.dtmFFCI = txtDateFFI.Value;
        if (!string.IsNullOrEmpty(txtManagarial.Text.Trim()))
        {
            objMaster.intManaregailSkill = Convert.ToInt32(txtManagarial.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtSupervisor.Text.Trim()))
        {
            objMaster.intSupervisor = Convert.ToInt32(txtSupervisor.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtSkilled.Text.Trim()))
        {
            objMaster.intSkilled = Convert.ToInt32(txtSkilled.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtUnSKilled.Text.Trim()))
        {
            objMaster.intUnskilled = Convert.ToInt32(txtUnSKilled.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtSemiSkilled.Text.Trim()))
        {
            objMaster.intSemiSkilled = Convert.ToInt32(txtSemiSkilled.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtTotalSc.Text.Trim()))
        {
            objMaster.intScTotal = Convert.ToInt32(txtTotalSc.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtTotalSt.Text.Trim()))
        {
            objMaster.intStTotal = Convert.ToInt32(txtTotalSt.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtPhd.Text.Trim()))
        {
            objMaster.intDisabled = Convert.ToInt32(txtPhd.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtWomen.Text.Trim()))
        {
            objMaster.intWomen = Convert.ToInt32(txtWomen.Text.Trim());
        }
        objMaster.intCreatedBy = Convert.ToInt32(Session["userId"]);
        objMaster.strActionCode = "add";
        #endregion

        IncentiveMasterBusinessLayer objBuisness = new IncentiveMasterBusinessLayer();
        int intRetValue = objBuisness.IRForm_AED(objIrDetails, objMaster);
        ScriptManager.RegisterStartupScript(btnConfirm, this.GetType(), "Myalert", "alert('" + Messages.ShowMessage("2") + "');window.location.href='ViewIncentiveApplication.aspx?ID=" + Request.QueryString["ID"].ToString() + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "';", true);

    }

    protected void gvLocDetails_rowdatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlState = (DropDownList)e.Row.FindControl("ddlState");
            BindState(ddlState);
            DropDownList ddlDistrict = (DropDownList)e.Row.FindControl("ddlDistrict");
            HiddenField hdnDistrict = (HiddenField)e.Row.FindControl("hdnDistrict");
            HiddenField hdnState = (HiddenField)e.Row.FindControl("hdnState");
            if (!string.IsNullOrEmpty(hdnState.Value))
            {
                ddlState.SelectedValue = hdnState.Value;
                BindDistrict(ddlDistrict, ddlState.SelectedValue);
                if (!string.IsNullOrEmpty(hdnDistrict.Value))
                {
                    ddlDistrict.SelectedValue = hdnDistrict.Value;
                }
            }
        }
    }

    protected void grdIncentiveApplied_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList drpIncentiveType = (DropDownList)e.Row.FindControl("drpIncentiveType");
            HiddenField hdnType = (HiddenField)e.Row.FindControl("hdnType");
            DropDownList drpIpr = (DropDownList)e.Row.FindControl("drpIpr");
            HiddenField hdnIpr = (HiddenField)e.Row.FindControl("hdnIpr");
            if (!string.IsNullOrEmpty(hdnType.Value))
            {
                drpIncentiveType.SelectedValue = hdnType.Value;
            }
            if (!string.IsNullOrEmpty(hdnIpr.Value))
            {
                drpIpr.SelectedValue = hdnIpr.Value;
            }
            TextBox txtFromDate = (TextBox)e.Row.FindControl("txtFromDate");
            TextBox txtToDate = (TextBox)e.Row.FindControl("txtToDate");
            txtToDate.Attributes.Add("readonly", "readonly");
            txtFromDate.Attributes.Add("readonly", "readonly");
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlState = (DropDownList)sender;
        GridViewRow gRow = (GridViewRow)ddlState.NamingContainer;
        DropDownList ddlDistrict = (DropDownList)gRow.FindControl("ddlDistrict");
        BindDistrict(ddlDistrict, ddlState.SelectedValue);
    }

    private void BindState(DropDownList ddlState)
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objService = new ProposalBAL();
        objProp.strAction = "ST";
        objProp.vchProposalNo = "1";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();
        ddlState.DataSource = objProjList;
        ddlState.DataTextField = "vchStateName";
        ddlState.DataValueField = "intStateId";
        ddlState.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlState.Items.Insert(0, list);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("../Incentive/ViewIncentiveApplication.aspx?id={0}&linkn={1}&linkm={2}&btn={3}&tab={4}", Request.QueryString["id"], Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"]));
    }
    #endregion

    #region function to get value while edit
    /// <summary>
    /// Function to get all the details based on the application details called when editing
    /// </summary>
    private void GetApplicationDetails()
    {
        PcSearch objSearch = new PcSearch()
        {
            intAppFor = Convert.ToInt32(Request.QueryString["id"]),
            strActionCode = "e",
            intPageIndex = 0,
            intPageSize = 0,
            strFromDate = string.Empty,
            strToDate = string.Empty
        };
        DataSet objDs = new DataSet();
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        objDs = objBuisnessLayer.Incentive_PcForm_Large_View(objSearch);
        if (objDs != null)
        {
            DataTable dtPcDetails = new DataTable();
            dtPcDetails = objDs.Tables[0];
            if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
            {
                DataRow objRow = dtPcDetails.Rows[0];
                string strChangeIn = objRow["vchChngIn"].ToString();
                UpdateCheckBoxList(strChangeIn, chkLstChange);
                string strInvestIn = objRow["vchInvestIn"].ToString();
                //UpdateCheckBoxList(strInvestIn, chkInvestIn);
                lblApplicationNo.Text = objRow["vchAppNo"].ToString();
                txtEin.Text = objRow["vchEINEMIIPMTNo"].ToString();
                //txtUan.Text = objRow["vchUAN"].ToString();
                txtEnterpriseName.Text = objRow["vchCompName"].ToString();
                drpApplicationType.SelectedValue = objRow["vchAppFor"].ToString();
                drpCompanyType.SelectedValue = objRow["intUnitType"].ToString();
                drpOrganizationType.SelectedValue = objRow["intOrgType"].ToString();
                drpOwnerType.SelectedValue = objRow["intOwnerCode"].ToString();
                drpUnitCategory.SelectedValue = objRow["intUnitCat"].ToString();
                txtOfficeAddress.Text = objRow["vchOffcAddr"].ToString();
                txtOfficeEmail.Text = objRow["vchOffcEmail"].ToString();
                txtOfficeFax.Text = objRow["vchOffcFaxNo"].ToString();
                txtOfficePhone.Text = objRow["vchOffcPhNo"].ToString();
                txtOfficeWebsite.Text = objRow["vchOffcWebsite"].ToString();
                txtEnterpriseAddress.Text = objRow["vchAddr"].ToString();
                txtPhoneNo.Text = objRow["vchPhNo"].ToString();
                txtFax.Text = objRow["vchFaxNo"].ToString();
                txtEmail.Text = objRow["vchEmail"].ToString();
                txtWebsite.Text = objRow["vchWebsite"].ToString();
                txtOwnerName.Text = objRow["vchOwnerName"].ToString();
                txtDateFFI.Value = objRow["dtmFFCI"].ToString();
                ddlSector.SelectedValue = objRow["intSectorId"].ToString();
                BindSubSector(ddlSector.SelectedValue);
                ddlSubSector.SelectedValue = objRow["intSubSectorId"].ToString();
                ddlDistrict.SelectedValue = objRow["intDistrict"].ToString();
                BindBlock(ddlDistrict.SelectedValue);
                ddlBlock.SelectedValue = objRow["intBlock"].ToString();
            }

            dtPcDetails = objDs.Tables[2];
            if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
            {
                //BindProductGridview(dtPcDetails);
                ViewState["Products"] = dtPcDetails;
            }
        }
    }
    #endregion

    #region Checkboxlist common functions
    /// <summary>
    /// Function to get all the selected values from checkboxlist and form a comma separarted string
    /// </summary>
    /// <param name="objChkLst"></param>
    /// <returns></returns>
    private string GetCheckBoxListValues(CheckBoxList objChkLst)
    {
        string strRetValue = string.Empty;
        for (int cnt = 0; cnt < objChkLst.Items.Count; cnt++)
        {
            if (objChkLst.Items[cnt].Selected)
            {
                strRetValue = string.Format("{0}{1},", strRetValue, objChkLst.Items[cnt].Value);
            }
        }
        if (!string.IsNullOrEmpty(strRetValue))
        {
            strRetValue = strRetValue.Substring(0, strRetValue.Length - 1);
        }
        return strRetValue;
    }

    /// <summary>
    /// Function to clear checkvalues of checkboxlist
    /// </summary>
    /// <param name="objCheckBoxList"></param>
    private void ClearCheckBoxList(CheckBoxList objCheckBoxList)
    {
        for (int cnt = 0; cnt < objCheckBoxList.Items.Count; cnt++)
        {
            if (objCheckBoxList.Items[cnt].Selected)
            {
                objCheckBoxList.Items[cnt].Selected = false;
            }
        }

    }

    /// <summary>
    /// function to set the checkbox as selected based on the comma separated values in string
    /// </summary>
    /// <param name="strValues">comma separated values</param>
    /// <param name="objCheckBoxList">checkboxlist</param>
    private void UpdateCheckBoxList(string strValues, CheckBoxList objCheckBoxList)
    {
        if (!string.IsNullOrEmpty(strValues))
        {
            string[] arrChange = strValues.Split(',');
            for (int aCnt = 0; aCnt < arrChange.Length; aCnt++)
            {
                int intId = Convert.ToInt32(arrChange[aCnt]);
                for (int cnt = 0; cnt < objCheckBoxList.Items.Count; cnt++)
                {
                    if (Convert.ToInt32(objCheckBoxList.Items[cnt].Value) == intId)
                    {
                        objCheckBoxList.Items[cnt].Selected = true;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Function to bind the checkboxlist, main code to bind the dropdown
    /// </summary>
    /// <param name="objDt">Datatable with all values</param>
    /// <param name="strHeaderType">type of data in dropdown</param>
    /// <param name="objChkList">checkboxlist to bind</param>
    private void FillCheckBoxList(DataTable objDt, string strHeaderType, CheckBoxList objChkList)
    {
        objChkList.Items.Clear();
        if (objDt != null && objDt.Rows.Count > 0)
        {
            objChkList.DataSource = objDt;
            objChkList.DataTextField = "vchName";
            objChkList.DataValueField = "slno";
            objChkList.DataBind();
        }
    }
    #endregion

    #region Common functions for page
    /// <summary>
    /// Common function to get all the datatable values from database to bind to gridview
    /// </summary>
    private void BindDropDown()
    {
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        DataSet objDa = objBuisnessLayer.BindDropdown("ddl");
        if (objDa != null)
        {
            FillDropDown(objDa.Tables[0], "Application For", drpApplicationType);
            FillCheckBoxList(objDa.Tables[1], "Change in", chkLstChange);
            FillDropDown(objDa.Tables[2], "Organization Type", drpOrganizationType);
            FillDropDown(objDa.Tables[3], "Owner Type", drpOwnerType);
            FillDropDown(objDa.Tables[4], "Unit Category", drpUnitCategory);
            FillDropDown(objDa.Tables[5], "Company Type", drpCompanyType);
            grdCapitalInvestment.DataSource = objDa.Tables[7];
            grdCapitalInvestment.DataBind();
            grdProblems.DataSource = objDa.Tables[8];
            grdProblems.DataBind();
        }

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
            objDropdown.DataTextField = "vchName";
            objDropdown.DataValueField = "slno";
            objDropdown.DataBind();
        }
        objDropdown.Items.Insert(0, new ListItem(string.Format("-Select {0}-", strHeaderType), "0"));
    }

    /// <summary>
    /// Common function to create the datatable to bind the gridview
    /// </summary>
    /// <param name="intType"></param>
    /// <returns></returns>
    private DataTable CreateChildTable(int intType)
    {
        DataTable dt = new DataTable();
        DataColumn dcId = new DataColumn();
        dcId.ColumnName = "id";
        dcId.AutoIncrement = true;
        dcId.AutoIncrementSeed = 1;
        dcId.AutoIncrementStep = 1;
        dt.Columns.Add(dcId);
        switch (intType)
        {
            case (int)enGridType.officer:
                dt.Columns.Add(new DataColumn("strOfficer"));
                dt.Columns.Add(new DataColumn("strDesignation"));
                dt.Columns.Add(new DataColumn("strAuthority"));
                dt.Columns.Add(new DataColumn("AadhaarNo"));
                break;
            case (int)enGridType.Products:
                dt.Columns.Add(new DataColumn("item"));
                dt.Columns.Add(new DataColumn("Code"));
                dt.Columns.Add(new DataColumn("Qty"));
                dt.Columns.Add(new DataColumn("Unit"));
                dt.Columns.Add(new DataColumn("Cost"));
                break;
            case (int)enGridType.termLoan:
            case (int)enGridType.workingCapital:
                dt.Columns.Add(new DataColumn("institute"));
                dt.Columns.Add(new DataColumn("state"));
                dt.Columns.Add(new DataColumn("City"));
                dt.Columns.Add(new DataColumn("Amt"));
                dt.Columns.Add(new DataColumn("SanctionDate"));
                dt.Columns.Add(new DataColumn("AvailedAmt"));
                dt.Columns.Add(new DataColumn("AvailedDate"));
                break;
            case (int)enGridType.Approval:
                dt.Columns.Add(new DataColumn("name"));
                dt.Columns.Add(new DataColumn("authority"));
                dt.Columns.Add(new DataColumn("others"));
                dt.Columns.Add(new DataColumn("filename"));
                break;
            case (int)enGridType.statutoryclearence:
                dt.Columns.Add(new DataColumn("clearence"));
                dt.Columns.Add(new DataColumn("period"));
                break;
            case (int)enGridType.cInvest:
                dt.Columns.Add(new DataColumn("slno"));
                dt.Columns.Add(new DataColumn("PerDpr"));
                dt.Columns.Add(new DataColumn("ActualExpenditure"));
                break;
            case (int)enGridType.problems:
                dt.Columns.Add(new DataColumn("slno"));
                dt.Columns.Add(new DataColumn("Problems"));
                break;
            case (int)enGridType.otherEnterprise:
                dt.Columns.Add(new DataColumn("unitname"));
                dt.Columns.Add(new DataColumn("product"));
                dt.Columns.Add(new DataColumn("totalCapacity"));
                dt.Columns.Add(new DataColumn("state"));
                dt.Columns.Add(new DataColumn("district"));
                break;
            case (int)enGridType.IPR:
                dt.Columns.Add(new DataColumn("type"));
                dt.Columns.Add(new DataColumn("quantam"));
                dt.Columns.Add(new DataColumn("fromdate"));
                dt.Columns.Add(new DataColumn("todate"));
                dt.Columns.Add(new DataColumn("ipr"));
                break;
            default:
                break;
        }
        return dt;
    }

    /// <summary>
    /// function is valled when add more button is clicked in any of the gridview
    /// </summary>
    /// <param name="GRow">The row in which the add more button is present</param>
    /// <param name="intGridType">The type of grid as per enGridType</param>
    /// <param name="strTypeName">name of the gridview as per engridname for viewstate table name mapping</param>
    /// <param name="objGrid">gridivew whose rowcommand event is fired</param>
    private void AddNewItemInGrid(GridViewRow GRow, int intGridType, string strTypeName, GridView objGrid)
    {
        DataTable dtGrid = null;
        DataRow dRow = null;
        if (ViewState[strTypeName] != null)
        {
            dtGrid = (DataTable)ViewState[strTypeName];
            dRow = dtGrid.Rows[dtGrid.Rows.Count - 1];
        }
        else
        {
            dtGrid = CreateChildTable(intGridType);
            dRow = dtGrid.NewRow();
        }

        if (intGridType == (int)enGridType.officer)
        {
            TextBox txtOfficerName = (TextBox)GRow.FindControl("txtOfficerName");
            TextBox txtDesignation = (TextBox)GRow.FindControl("txtDesignation");
            TextBox txtOrganization = (TextBox)GRow.FindControl("txtOrganization");
            dRow["strOfficer"] = txtOfficerName.Text;
            dRow["strDesignation"] = txtDesignation.Text;
            dRow["strAuthority"] = txtOrganization.Text;
            if (GRow.FindControl("txtAadhaarNo") != null)
            {
                TextBox txtAadhaarNo = (TextBox)GRow.FindControl("txtAadhaarNo");
                dRow["AadhaarNo"] = txtAadhaarNo.Text;
            }
            else
            {
                dRow["AadhaarNo"] = string.Empty;
            }

            if (ViewState[strTypeName] != null)
            {
                ViewState[strTypeName] = dtGrid;
            }
            else
            {
                dtGrid.Rows.Add(dRow);
                ViewState[strTypeName] = dtGrid;
            }
            dRow = dtGrid.NewRow();
            dRow["strOfficer"] = string.Empty;
            dRow["strDesignation"] = string.Empty;
            dRow["strAuthority"] = string.Empty;
            dRow["AadhaarNo"] = string.Empty;

        }
        else if (intGridType == (int)enGridType.Products)
        {
            TextBox txtItemProduct = (TextBox)GRow.FindControl("txtItemProduct");
            TextBox txtItemCode = (TextBox)GRow.FindControl("txtItemCode");
            TextBox txtQuantity = (TextBox)GRow.FindControl("txtQuantity");
            TextBox txtCost = (TextBox)GRow.FindControl("txtCost");
            TextBox txtUnit = (TextBox)GRow.FindControl("txtUnit");
            dRow["item"] = txtItemProduct.Text;
            dRow["Code"] = txtItemCode.Text;
            dRow["Qty"] = txtQuantity.Text;
            dRow["Unit"] = txtUnit.Text;
            dRow["Cost"] = txtCost.Text;

            if (ViewState[strTypeName] != null)
            {
                ViewState[strTypeName] = dtGrid;
            }
            else
            {
                ViewState[strTypeName] = dtGrid;
                dtGrid.Rows.Add(dRow);
            }
            dRow = dtGrid.NewRow();
            dRow["item"] = string.Empty;
            dRow["Code"] = string.Empty;
            dRow["Qty"] = 0;
            dRow["Unit"] = 0;
            dRow["Cost"] = 0.00;
        }
        if (intGridType == (int)enGridType.termLoan || intGridType == (int)enGridType.workingCapital)
        {
            TextBox txtTlInstitue = (TextBox)GRow.FindControl("txtTlInstitue");
            TextBox txtState = (TextBox)GRow.FindControl("txtState");
            TextBox txtCity = (TextBox)GRow.FindControl("txtCity");
            TextBox txtAmount = (TextBox)GRow.FindControl("txtAmount");
            TextBox txtSanctionDate = (TextBox)GRow.FindControl("txtSanctionDate");
            TextBox txtAvailedAmount = (TextBox)GRow.FindControl("txtSanctionDate");
            TextBox txtAvailedDate = (TextBox)GRow.FindControl("txtSanctionDate");
            dRow["institute"] = txtTlInstitue.Text;
            dRow["state"] = txtState.Text;
            dRow["City"] = txtCity.Text;
            dRow["Amt"] = txtAmount.Text;
            dRow["SanctionDate"] = txtSanctionDate;
            dRow["AvailedAmt"] = txtAvailedAmount.Text;
            dRow["AvailedDate"] = txtAvailedDate.Text;

            if (ViewState[strTypeName] != null)
            {
                ViewState[strTypeName] = dtGrid;
            }
            else
            {
                ViewState[strTypeName] = dtGrid;
                dtGrid.Rows.Add(dRow);
            }
            dRow = dtGrid.NewRow();
            dRow["institute"] = string.Empty;
            dRow["state"] = string.Empty;
            dRow["City"] = string.Empty;
            dRow["Amt"] = "0.00";
            dRow["SanctionDate"] = string.Empty;
            dRow["AvailedAmt"] = "0.00";
            dRow["AvailedDate"] = string.Empty;
        }
        else if (intGridType == (int)enGridType.Approval)
        {
            TextBox txtNameOfSite = (TextBox)GRow.FindControl("txtNameOfSite");
            DropDownList drpAuthority = (DropDownList)GRow.FindControl("drpApprovalAuthority");
            TextBox txtOthers = (TextBox)GRow.FindControl("txtOthers");
            dRow["name"] = txtNameOfSite.Text;
            dRow["authority"] = drpAuthority.SelectedValue;
            dRow["others"] = txtOthers.Text;
            dRow["filename"] = string.Empty;

            if (ViewState[strTypeName] != null)
            {
                ViewState[strTypeName] = dtGrid;
            }
            else
            {
                ViewState[strTypeName] = dtGrid;
                dtGrid.Rows.Add(dRow);
            }
            dRow = dtGrid.NewRow();
            dRow["name"] = string.Empty;
            dRow["authority"] = string.Empty;
            dRow["others"] = string.Empty;
            dRow["filename"] = string.Empty;
        }
        else if (intGridType == (int)enGridType.statutoryclearence)
        {
            TextBox txtPeriod = (TextBox)GRow.FindControl("txtPeriod");
            DropDownList drpClearence = (DropDownList)GRow.FindControl("drpClearence");
            dRow["clearence"] = drpClearence.SelectedValue;
            dRow["period"] = txtPeriod.Text;

            if (ViewState[strTypeName] != null)
            {
                ViewState[strTypeName] = dtGrid;
            }
            else
            {
                ViewState[strTypeName] = dtGrid;
                dtGrid.Rows.Add(dRow);
            }
            dRow = dtGrid.NewRow();
            dRow["clearence"] = string.Empty;
            dRow["period"] = string.Empty;
        }
        else if (intGridType == (int)enGridType.otherEnterprise)
        {
            TextBox txtUnit = (TextBox)GRow.FindControl("txtUnit");
            TextBox txtProduct = (TextBox)GRow.FindControl("txtProduct");
            TextBox txtCapacity = (TextBox)GRow.FindControl("txtCapacity");
            DropDownList ddlState = (DropDownList)GRow.FindControl("ddlState");
            DropDownList ddlDistrict = (DropDownList)GRow.FindControl("ddlDistrict");
            dRow["unitname"] = txtUnit.Text;
            dRow["product"] = txtProduct.Text;
            dRow["totalCapacity"] = txtCapacity.Text;
            dRow["state"] = ddlState.SelectedValue;
            dRow["district"] = ddlDistrict.SelectedValue;

            if (ViewState[strTypeName] != null)
            {
                ViewState[strTypeName] = dtGrid;
            }
            else
            {
                ViewState[strTypeName] = dtGrid;
                dtGrid.Rows.Add(dRow);
            }
            dRow = dtGrid.NewRow();
            dRow["unitname"] = string.Empty;
            dRow["product"] = string.Empty;
            dRow["totalCapacity"] = 0;
            dRow["state"] = 0;
            dRow["district"] = 0;
        }
        else if (intGridType == (int)enGridType.IPR)
        {
            TextBox txtQuantam = (TextBox)GRow.FindControl("txtQuantam");
            TextBox txtFromDate = (TextBox)GRow.FindControl("txtFromDate");
            TextBox txtToDate = (TextBox)GRow.FindControl("txtToDate");
            DropDownList drpIpr = (DropDownList)GRow.FindControl("drpIpr");
            DropDownList drpIncentiveType = (DropDownList)GRow.FindControl("drpIncentiveType");
            dRow["type"] = drpIncentiveType.SelectedValue;
            dRow["quantam"] = txtQuantam.Text;
            dRow["fromdate"] = txtFromDate.Text;
            dRow["todate"] = txtToDate.Text;
            dRow["ipr"] = drpIpr.SelectedValue;

            if (ViewState[strTypeName] != null)
            {
                ViewState[strTypeName] = dtGrid;
            }
            else
            {
                ViewState[strTypeName] = dtGrid;
                dtGrid.Rows.Add(dRow);
            }
            dRow = dtGrid.NewRow();
            dRow["type"] = 0;
            dRow["quantam"] = 0;
            dRow["fromdate"] = string.Empty;
            dRow["todate"] = string.Empty;
            dRow["ipr"] = 0;
        }
        dtGrid.Rows.Add(dRow);
        BindAddMoreGrids(intGridType, strTypeName, objGrid, dtGrid);
    }

    /// <summary>
    /// if the unit has commenced production or under implementation show and hide column
    /// </summary>
    private void ShowProductDetailsByUnitCond()
    {
        divProdStartDate.Visible = false;
        divUnitCond.Visible = false;
        lblUnitCOnd.Text = string.Empty;
        if (rdBtnUntiCond.SelectedValue == "1")
        {
            ViewState["Products"] = null;
            BindAddMoreGrids((int)enGridType.Products, enGridName.Products.ToString(), grdProducts);
            divUnitCond.Visible = true;
            lblUnitCOnd.Text = "Original Enterprise";
        }
        else if (rdBtnUntiCond.SelectedValue == "2")
        {
            BindAddMoreGrids((int)enGridType.Products, enGridName.Products.ToString(), grdProducts);
            divProdStartDate.Visible = true;
            divUnitCond.Visible = true;
            lblUnitCOnd.Text = "Expansion / Modernization / Diversification";
        }
    }

    /// <summary>
    /// Function to bind all the addmore grids
    /// </summary>
    /// <param name="intGridType">gridtype as engridtype</param>
    /// <param name="tableName">name of table as per enTypename</param>
    /// <param name="ObjGrid">gridview to bind</param>
    /// <param name="dtExisting">datatable if provided(optional parameter)</param>
    private void BindAddMoreGrids(int intGridType, string tableName, GridView ObjGrid, DataTable dtExisting = null)
    {
        DataTable dtGrid = CreateChildTable(intGridType);
        dtGrid.TableName = tableName;
        if (ViewState[tableName] != null)
        {
            dtGrid = dtExisting;
        }
        else
        {
            DataRow dRow = dtGrid.NewRow();
            switch (intGridType)
            {
                case (int)enGridType.statutoryclearence:
                    dRow["clearence"] = string.Empty;
                    dRow["period"] = string.Empty;
                    break;
                case (int)enGridType.Approval:
                    dRow["name"] = string.Empty;
                    dRow["authority"] = string.Empty;
                    dRow["others"] = string.Empty;
                    dRow["filename"] = string.Empty;
                    break;
                case (int)enGridType.termLoan:
                case (int)enGridType.workingCapital:
                    dRow["institute"] = string.Empty;
                    dRow["state"] = string.Empty;
                    dRow["City"] = string.Empty;
                    dRow["Amt"] = "0.00";
                    dRow["SanctionDate"] = string.Empty;
                    dRow["AvailedAmt"] = "0.00";
                    dRow["AvailedDate"] = string.Empty;
                    break;
                case (int)enGridType.officer:
                    dRow["strOfficer"] = string.Empty;
                    dRow["strDesignation"] = string.Empty;
                    dRow["strAuthority"] = string.Empty;
                    dRow["AadhaarNo"] = string.Empty;
                    break;
                case (int)enGridType.Products:
                    dRow["item"] = string.Empty;
                    dRow["Code"] = string.Empty;
                    dRow["Qty"] = 0;
                    dRow["Unit"] = 0;
                    dRow["Cost"] = 0.00;
                    break;
                case (int)enGridType.cInvest:
                    dRow["slno"] = 0;
                    dRow["PerDpr"] = string.Empty;
                    dRow["ActualExpenditure"] = 0;
                    break;
                case (int)enGridType.otherEnterprise:
                    dRow["unitname"] = string.Empty;
                    dRow["product"] = string.Empty;
                    dRow["totalCapacity"] = 0;
                    dRow["state"] = 0;
                    dRow["district"] = 0;
                    break;
                case (int)enGridType.IPR:
                    dRow["type"] = 0;
                    dRow["quantam"] = 0;
                    dRow["fromdate"] = string.Empty;
                    dRow["todate"] = string.Empty;
                    dRow["ipr"] = 0;
                    break;
                default:
                    break;
            }
            dtGrid.Rows.Add(dRow);
        }
        ObjGrid.DataSource = dtGrid;
        ObjGrid.DataBind();
        if ((int)enGridType.officer == intGridType)
        {
            if (chkverification.Checked)
            {
                ObjGrid.Columns[4].Visible = true;
            }
            else
            {
                ObjGrid.Columns[4].Visible = false;
            }
        }
        int totalCnt = ObjGrid.Rows.Count;
        for (int cnt = 0; cnt < totalCnt; cnt++)
        {
            if (cnt == totalCnt - 1)
            {
                LinkButton lnkAdd = (LinkButton)ObjGrid.Rows[cnt].FindControl("lnkAdd");
                lnkAdd.Visible = true;
            }
            else
            {
                LinkButton lnkAdd = (LinkButton)ObjGrid.Rows[cnt].FindControl("lnkAdd");
                lnkAdd.Visible = false;
                LinkButton lnkDelete = (LinkButton)ObjGrid.Rows[cnt].FindControl("lnkDelete");
                lnkDelete.Visible = true;
            }
        }

    }

    /// <summary>
    /// Function to set all the labels as per new and EMD selected
    /// </summary>
    /// <param name="strValue"></param>
    private void SetAllLabels(string strValue)
    {
        lblProductionControl.Text = strValue;
        lblPowerLoad.Text = strValue;
        lblEmployement.Text = strValue;
        lblCapitalInvest.Text = strValue;
        lblApproval.Text = strValue;
        lblStatutaryCLearence.Text = strValue;
    }

    private void BindSector()
    {
        ProposalBAL objService = new ProposalBAL();
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "SE";
        objProp.vchProposalNo = "";
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlSector.DataSource = objProjList;
        ddlSector.DataTextField = "vchSectorName";
        ddlSector.DataValueField = "intSectorId";
        ddlSector.DataBind();
        ListItem list = new ListItem();
        list.Text = "-Select Sector-";
        list.Value = "0";
        ddlSector.Items.Insert(0, list);
    }

    private void BindSubSector(string strstate)
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objService = new ProposalBAL();
        objProp.strAction = "SU";
        objProp.vchProposalNo = strstate;
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();
        ddlSubSector.DataSource = objProjList;
        ddlSubSector.DataTextField = "vchSectorName";
        ddlSubSector.DataValueField = "intSectorId";
        ddlSubSector.DataBind();
        ListItem list = new ListItem();
        list.Text = "-Select Sub Sector-";
        list.Value = "0";
        ddlSubSector.Items.Insert(0, list);
    }

    private void BindDistrict(DropDownList ddlDistrict, string strState = "")
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objService = new ProposalBAL();
        if (string.IsNullOrEmpty(strState))
        {
            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
        }
        else
        {
            objProp.strAction = "SD";
            objProp.vchProposalNo = strState;
        }
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlDistrict.DataSource = objProjList;
        ddlDistrict.DataTextField = "vchDistName";
        ddlDistrict.DataValueField = "intDistId";
        ddlDistrict.DataBind();
        ListItem list = new ListItem();
        list.Text = "-Select District-";
        list.Value = "0";
        ddlDistrict.Items.Insert(0, list);

    }
    private void BindBlock(string strdist)
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objService = new ProposalBAL();
        objProp.strAction = "BL";
        objProp.vchProposalNo = strdist;
        objProjList = objService.PopulateProjDropdowns(objProp).ToList();

        ddlBlock.DataSource = objProjList;
        ddlBlock.DataTextField = "vchBlockName";
        ddlBlock.DataValueField = "intBlockId";
        ddlBlock.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select Block--";
        list.Value = "0";
        ddlBlock.Items.Insert(0, list);
    }
    #endregion
}