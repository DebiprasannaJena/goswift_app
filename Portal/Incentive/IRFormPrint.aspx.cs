﻿using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;
using EntityLayer.Proposal;
using System.Collections.Generic;
using BusinessLogicLayer.Proposal;
using System.IO;

public partial class Portal_Incentive_IRFormPrint : System.Web.UI.Page
{
    #region Constant Values
    const int conUnitothers = 52;
    const int conOrgOthers = 24;
    const string gStrOriginal = "for Original Enterprise";
    const string gStrEmd = "for E / M / D";
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
            pnlmain.Enabled = false;
            #region textbox readonly
            //txtAvailedDate.Attributes.Add("readonly", "readonly");
            //txtSanctionDate.Attributes.Add("readonly", "readonly");
            //txtWcAvailedDate.Attributes.Add("readonly", "readonly");
            //txtWcSanctionDate.Attributes.Add("readonly", "readonly");
            //txtToClDate.Attributes.Add("readonly", "readonly");
            //txtFromClDate.Attributes.Add("readonly", "readonly");
            //txtToDate.Attributes.Add("readonly", "readonly");
            //txtFromDate.Attributes.Add("readonly", "readonly");
            //txtDateOfProd.Attributes.Add("readonly", "readonly");
            txtPowerCommisioning.Attributes.Add("readonly", "readonly");
            txtPowerConnection.Attributes.Add("readonly", "readonly");
            txtDateOfInspection.Attributes.Add("readonly", "readonly");
            txtDateFFI.Attributes.Add("readonly", "readonly");
            txtDateOfPlant.Attributes.Add("readonly", "readonly");
            txtProdCommencement.Attributes.Add("readonly", "readonly");
           // txtDateofPurchase.Attributes.Add("readonly", "readonly");
            #endregion


            if (Request.QueryString["type"] != null)
            {
                #region hide all addmore if view of ir form
                int type = Convert.ToInt32(Request.QueryString["type"]);
                pnlmain.Enabled = false;
                btnCancel.Visible = false;
                btnConfirm.Visible = false;
                //tblApplied.Visible = false;
                //tblApproval.Visible = false;
                //tblClearence.Visible = false;
                //tblOfficer.Visible = false;
                //tblOther.Visible = false;
                //tblProducts.Visible = false;
                //tblTermLoan.Visible = false;
                //Table1.Visible = false;
              //  btnDraft.Visible = false;
                divSignatureUpload.Visible = true;              
                imgSignature.Visible = true;
                if (type == 1)
                {
                    //btnApprove.Visible = true;
                    //btnReject.Visible = true;
                    //btnDraft.Visible = false;

                }
                #endregion
            }

            GetIrFormDetails();
           // BindState(ddlState);

            #region bind all add more grids
            BindAddMoreGrids((int)enGridType.Approval, enGridName.Approval.ToString(), grdApproval);
            BindAddMoreGrids((int)enGridType.officer, enGridName.officer.ToString(), grdOffice);
            BindAddMoreGrids((int)enGridType.Products, enGridName.Products.ToString(), grdProducts);
            BindAddMoreGrids((int)enGridType.statutoryclearence, enGridName.statutoryclearence.ToString(), grdStatutoryClearence);
            BindAddMoreGrids((int)enGridType.termLoan, enGridName.termLoan.ToString(), grdTermLoan);
            BindAddMoreGrids((int)enGridType.workingCapital, enGridName.workingCapital.ToString(), grdWorkingCapital);
            BindAddMoreGrids((int)enGridType.otherEnterprise, enGridName.otherEnterprise.ToString(), gvLOCDetails);
            BindAddMoreGrids((int)enGridType.IPR, enGridName.IPR.ToString(), grdIncentiveApplied);
            #endregion

            ShowProductDetailsByUnitCond();
            ShowOwnerTypeByCode();
            //set all labels as original enterprise in case of new else set them as EXISting EMD
            //constants created for them same
            string strApplicationType = drpApplicationType.SelectedValue;
            if (strApplicationType == "58" || strApplicationType == "59" || strApplicationType == "60")
            {
                SetAllLabels(gStrEmd);
            }
            else
            {
                SetAllLabels(gStrOriginal);
            }

            //btnApprove.OnClientClick = "return confirm('Are you sure you want to approve the PC No. -" + lblApplicationNo.Text + "')";
            //btnReject.OnClientClick = "return confirm('Are you sure you want to reject the PC No. -" + lblApplicationNo.Text + "')";
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
        objds = objBuisness.Incentive_PcForm_View(objSearch);

        if (objds != null && objds.Tables.Count > 0)
        {
            GetApplicationDetails(objds);
            DataTable dtDetails = new DataTable();
            dtDetails = objds.Tables[0];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                DataRow dRow = dtDetails.Rows[0];
                hdnScheduleDt.Value = dRow["dtmIRScheduleOn"].ToString();
                txtDateOfInspection.Text = hdnScheduleDt.Value;
                lblDist.Text = dRow["distname"].ToString();
            }
            dtDetails = objds.Tables[14];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                BindMachineryGridview(dtDetails);
                ViewState["Machinery"] = dtDetails;

            }
            dtDetails = objds.Tables[3];

            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                DataRow dRow = dtDetails.Rows[0];
                //   txtPollutionControl.Text = dRow["vchControlMeasures"].ToString();
                // txtSafety.Text = dRow["vchIndSafety"].ToString();
                txtPowerLoad.Text = dRow["vchPowerLoad"].ToString();
                txtCpp.Text = dRow["vchCppDetails"].ToString();
                txtRemarks.Text = dRow["vchRemarks"].ToString();
                txtSuggestions.Text = dRow["vchSuggestions"].ToString();
                if (dRow["intCheck"].ToString() == "1")
                    chkverification.Checked = true;
                rdBtnUntiCond.SelectedValue = dRow["intunit"].ToString();
                if (dRow["dtmInspectionReport"].ToString() == "")
                    txtDateOfInspection.Text = hdnScheduleDt.Value;
                else
                    txtDateOfInspection.Text = dRow["dtmInspectionReport"].ToString();

                txtPowerCommisioning.Text = dRow["dtmComissioning"].ToString();
                if (txtPowerCommisioning.Text == "01-Jan-1900")
                    txtPowerCommisioning.Text = string.Empty;
                txtDateOfPlant.Text = dRow["dtmPlantInvest"].ToString();
                if (txtDateOfPlant.Text == "01-Jan-1900")
                    txtDateOfPlant.Text = string.Empty;
                
                imgSignature.ImageUrl = string.Format("~/incentives/Files/PC/{0}/{1}", hdnInvestorId.Value, dRow["vchsignature"].ToString());
            }
            dtDetails = new DataTable();
            dtDetails = objds.Tables[4];

            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                grdOffice.DataSource = dtDetails;
                grdOffice.DataBind();
                //  grdOffice.Columns[grdOffice.Columns.Count - 1].Visible = false;
                ViewState[enGridName.officer.ToString()] = dtDetails;
            }
            dtDetails = new DataTable();
            dtDetails = objds.Tables[5];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                grdCapitalInvestment.DataSource = dtDetails;
                grdCapitalInvestment.DataBind();
                ViewState[enGridName.cInvest.ToString()] = dtDetails;

            }
            dtDetails = new DataTable();
            dtDetails = objds.Tables[6];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                ViewState[enGridName.termLoan.ToString()] = dtDetails;
                grdTermLoan.DataSource = dtDetails;
                grdTermLoan.DataBind();
               // grdTermLoan.Columns[grdTermLoan.Columns.Count - 1].Visible = false;
            }
            dtDetails = new DataTable();
            dtDetails = objds.Tables[7];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                ViewState[enGridName.workingCapital.ToString()] = dtDetails;
                grdWorkingCapital.DataSource = dtDetails;
                grdWorkingCapital.DataBind();
               // grdWorkingCapital.Columns[grdWorkingCapital.Columns.Count - 1].Visible = false;
            }
            dtDetails = new DataTable();
            dtDetails = objds.Tables[8];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                ViewState[enGridName.Approval.ToString()] = dtDetails;
                grdApproval.DataSource = dtDetails;
                grdApproval.DataBind();
              //  grdApproval.Columns[grdApproval.Columns.Count - 1].Visible = false;
            }
            dtDetails = new DataTable();
            dtDetails = objds.Tables[9];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                ViewState[enGridName.statutoryclearence.ToString()] = dtDetails;
                grdStatutoryClearence.DataSource = dtDetails;
                grdStatutoryClearence.DataBind();
                //grdStatutoryClearence.Columns[grdStatutoryClearence.Columns.Count - 1].Visible = false;
            }
            dtDetails = new DataTable();
            dtDetails = objds.Tables[10];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                ViewState[enGridName.problems.ToString()] = dtDetails;
                grdProblems.DataSource = dtDetails;
                grdProblems.DataBind();
            }
            dtDetails = new DataTable();
            dtDetails = objds.Tables[11];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                ViewState[enGridName.otherEnterprise.ToString()] = dtDetails;
                gvLOCDetails.DataSource = dtDetails;
                gvLOCDetails.DataBind();
               // gvLOCDetails.Columns[gvLOCDetails.Columns.Count - 1].Visible = false;
            }
            dtDetails = new DataTable();
            dtDetails = objds.Tables[12];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                ViewState[enGridName.IPR.ToString()] = dtDetails;
                grdIncentiveApplied.DataSource = dtDetails;
                grdIncentiveApplied.DataBind();
               // grdIncentiveApplied.Columns[grdIncentiveApplied.Columns.Count - 1].Visible = false;
            }
            dtDetails = new DataTable();
            dtDetails = objds.Tables[13];
            if (dtDetails != null && dtDetails.Rows.Count > 0)
            {
                grdFiles.DataSource = dtDetails;
                grdFiles.DataBind();
            }


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
        //  CommonTextAreaSetUp(txtPollutionControl, lblOriginal);
        //   CommonTextAreaSetUp(txtSafety, lblSafety);
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
        txt.Attributes.Add("onkeyup", string.Format("return CheckLengthKeyUp('{0}','{1}',200);", txt.ClientID, Lbl.ClientID));
        txt.Attributes.Add("onchange", string.Format("return checkLength('{0}','{1}',200);", txt.ClientID, Lbl.ClientID));
    }
    #endregion

    #region gridview events

    protected void grdFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnFileName = (HiddenField)e.Row.FindControl("hdnFileName");
            HiddenField hdnFolderName = (HiddenField)e.Row.FindControl("hdnFolderName");
            string path = string.Format("~/incentives{0}{1}", hdnFolderName.Value, hdnFileName.Value);
            HyperLink hypViewProductFile = (HyperLink)e.Row.FindControl("hypViewProductFile");
            hypViewProductFile.NavigateUrl = path;
        }
    }

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

  

    //protected void lnkAdd_Click(object sender, EventArgs e)
    //{
    //    LinkButton lnk = (LinkButton)sender;
    //    if (string.Equals(lnk.ID, lnkOfficerAdd.ID))
    //    {
    //        AddNewItemInGrid(null, (int)enGridType.officer, enGridName.officer.ToString(), grdOffice);
    //    }
    //    else if (string.Equals(lnk.ID, lnkAdd.ID))
    //    {
    //        AddNewItemInGrid(null, (int)enGridType.Products, enGridName.Products.ToString(), grdProducts);
    //    }
    //    else if (string.Equals(lnk.ID, lnkTermAdd.ID))
    //    {
    //        AddNewItemInGrid(null, (int)enGridType.termLoan, enGridName.termLoan.ToString(), grdTermLoan);
    //    }
    //    else if (string.Equals(lnk.ID, lnkWcAdd.ID))
    //    {
    //        AddNewItemInGrid(null, (int)enGridType.workingCapital, enGridName.workingCapital.ToString(), grdWorkingCapital);
    //    }
    //    else if (string.Equals(lnk.ID, lnkSupDocAdd.ID))
    //    {
    //        AddNewItemInGrid(null, (int)enGridType.Approval, enGridName.Approval.ToString(), grdApproval);
    //    }
    //    else if (string.Equals(lnk.ID, lnkClearenceAdd.ID))
    //    {
    //        AddNewItemInGrid(null, (int)enGridType.statutoryclearence, enGridName.statutoryclearence.ToString(), grdStatutoryClearence);
    //    }
    //    else if (string.Equals(lnk.ID, lnkOthersAdd.ID))
    //    {
    //        AddNewItemInGrid(null, (int)enGridType.otherEnterprise, enGridName.otherEnterprise.ToString(), gvLOCDetails);
    //    }
    //    else if (string.Equals(lnk.ID, lnkAppliedAdd.ID))
    //    {
    //        AddNewItemInGrid(null, (int)enGridType.IPR, enGridName.IPR.ToString(), grdIncentiveApplied);
    //    }
    //    if (string.Equals(lnk.ID, lnkOfficerAdd.ID))
    //    {
    //        AddNewItemInGrid(null, (int)enGridType.officer, enGridName.officer.ToString(), grdOffice);
    //    }

    //}

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
        LinkButton lnk = (LinkButton)e.CommandSource;
        GridViewRow GRow = (GridViewRow)lnk.NamingContainer;
        GridView grd = (GridView)GRow.NamingContainer;
        int id = Convert.ToInt32(grd.DataKeys[intRowIndex].Value);
        string grdName = grd.ID;

        if (string.Equals("d", strCommandName, StringComparison.OrdinalIgnoreCase))
        {
            if (grdName == grdOffice.ID)
            {
                DeleteFromGrid(id, (int)enGridType.officer, enGridName.officer.ToString(), grd);
            }
            else if (grdName == grdProducts.ID)
            {
                DeleteFromGrid(id, (int)enGridType.Products, enGridName.Products.ToString(), grd);
            }
            else if (grdName == grdTermLoan.ID)
            {
                DeleteFromGrid(id, (int)enGridType.termLoan, enGridName.termLoan.ToString(), grd);
            }
            else if (grdName == grdWorkingCapital.ID)
            {
                DeleteFromGrid(id, (int)enGridType.workingCapital, enGridName.workingCapital.ToString(), grd);
            }
            else if (grdName == grdApproval.ID)
            {
                DeleteFromGrid(id, (int)enGridType.Approval, enGridName.Approval.ToString(), grd);
                HiddenField hdnProductFile = (HiddenField)grdApproval.Rows[intRowIndex].FindControl("hdnProductFile");
                string filename = hdnProductFile.Value;
                string path = string.Format("../incentives/Files/Approval/{0}", Session["incentiveid"], filename);
                string completePath = Server.MapPath(path);
                if (System.IO.File.Exists(completePath))
                {
                    File.Delete(completePath);
                    hdnProductFile.Value = string.Empty;
                }
            }
            else if (grdName == grdStatutoryClearence.ID)
            {
                DeleteFromGrid(id, (int)enGridType.statutoryclearence, enGridName.statutoryclearence.ToString(), grd);
            }
            else if (grdName == gvLOCDetails.ID)
            {
                DeleteFromGrid(id, (int)enGridType.otherEnterprise, enGridName.otherEnterprise.ToString(), grd);
            }
            else if (grdName == grdIncentiveApplied.ID)
            {
                DeleteFromGrid(id, (int)enGridType.IPR, enGridName.IPR.ToString(), grd);
            }
        }

        if (e.CommandName == "Upd") //FOR UPDATE
        {
           // lnkAdd.Text = "Update";
            ViewState["EditIndex"] = intRowIndex;
            if (grdName == grdProducts.ID)
            {
                //txtItemProduct.Text = grdProducts.Rows[intRowIndex].Cells[1].Text;
                //txtItemCode.Text = grdProducts.Rows[intRowIndex].Cells[2].Text == "&nbsp;" ? "" : grdProducts.Rows[intRowIndex].Cells[2].Text;
                //txtQuantity.Text = grdProducts.Rows[intRowIndex].Cells[3].Text;
                //drpUnitType.SelectedValue = ((HiddenField)grdProducts.Rows[intRowIndex].FindControl("hdnUnit")).Value;
                //txtCost.Text = grdProducts.Rows[intRowIndex].Cells[5].Text;
                //txtDateOfProd.Text = grdProducts.Rows[intRowIndex].Cells[6].Text == "&nbsp;" ? "" : grdProducts.Rows[intRowIndex].Cells[6].Text;
                //hdnSlNo.Value = ((HiddenField)grdProducts.Rows[intRowIndex].Cells[0].FindControl("hdnSlnos")).Value;
                HiddenField hdnIsMainProduct = (HiddenField)grdProducts.Rows[intRowIndex].Cells[0].FindControl("hdnIsMainProduct");
                //if (string.IsNullOrEmpty(hdnIsMainProduct.Value) || (!string.IsNullOrEmpty(hdnIsMainProduct.Value) && Convert.ToBoolean(hdnIsMainProduct.Value) == false))
                //{
                //    chkMainCategory.Checked = false;
                //}
                //else
                //{
                //    chkMainCategory.Checked = true;
                //}
            }

        }

    }

    private void DeleteFromGrid(int id, int intGridType, string strTableName, GridView grd)
    {
        DataTable dtView = (DataTable)ViewState[strTableName];
        for (int cnt = 0; cnt < dtView.Rows.Count; cnt++)
        {
            if (dtView.Rows[cnt]["id"].ToString() == id.ToString())
            {
                dtView.Rows[cnt].Delete();
                dtView.AcceptChanges();
            }
        }
        ViewState[strTableName] = dtView;
        BindAddMoreGrids(intGridType, strTableName, grd);
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
        if (drpAuthority.SelectedValue == "any regulatory body")
        {
            drpAuthority.Visible = false;
            //hdnAuthority.Value = drpAuthority.SelectedValue;
            //txtOthers.Visible = true;
        }
    }

    /// <summary>
    /// based on the organization type selected, change the owner name type and the file type
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void drpOrganizationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowOwnerTypeByCode();
    }

    //protected void drpUnitType_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    txtUnitType.Visible = false;
    //    if (drpUnitType.SelectedIndex > 0)
    //    {
    //        int intUnitType = Convert.ToInt32(drpUnitType.SelectedValue);
    //        if (intUnitType == conUnitothers)
    //        {
    //            txtUnitType.Visible = true;
    //        }
    //    }
    //}
    protected void rdBtnApplicationFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intAppType = Convert.ToInt32(rdBtnApplicationFor.SelectedValue);
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        if (intAppType == 1)
        {
            objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
            {
                Action = "new",
                Name = string.Empty,
                ID = string.Empty,
                Param = 0,
                Param_1 = 0,
                Param_2 = string.Empty,
                Param_3 = string.Empty
            });
        }
        else
        {
            objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
            {
                Action = "amd",
                Name = string.Empty,
                ID = string.Empty,
                Param = 0,
                Param_1 = 0,
                Param_2 = string.Empty,
                Param_3 = string.Empty
            });
        }


    }

    private void ShowOwnerTypeByCode()
    {
        lblOwnerLabel.Text = string.Empty;
        int intOrgType = Convert.ToInt32(drpOrganizationType.SelectedValue);
        string strType = drpOrganizationType.SelectedItem.Text.ToLower();
        string strOwnerType = "Name of Managing member";
        txtOtherOrg.Visible = false;
        if (intOrgType > 0)
        {
            PcSearch objSearch = new PcSearch()
            {
                intAppFor = intOrgType,
                intPageIndex = 0,
                intPageSize = 0,
                strFromDate = string.Empty,
                strToDate = string.Empty,
                strActionCode = "org"
            };
            IncentiveMasterBusinessLayer objBuisness = new IncentiveMasterBusinessLayer();
            DataSet ds = objBuisness.Incentive_PcForm_View(objSearch);
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];
                if (!string.IsNullOrEmpty(dt.Rows[0]["vchOwnerType"].ToString()))
                {
                    strOwnerType = dt.Rows[0]["vchOwnerType"].ToString();
                }
            }

            if (intOrgType == conOrgOthers)
            {
                txtOtherOrg.Visible = true;
            }
        }
        lblOwnerLabel.Text = strOwnerType;
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
            HiddenField hdnApprovalName = (HiddenField)e.Row.FindControl("hdnApprovalName");
            HiddenField hdnOthers = (HiddenField)e.Row.FindControl("hdnOthers");
            Label lblValue = (Label)e.Row.FindControl("lblValue");
            if (string.IsNullOrEmpty(hdnApprovalName.Value))
            {
                lblValue.Text = hdnOthers.Value;
            }
            else
            {
                lblValue.Text = hdnApprovalName.Value;
            }

            HiddenField hdnProductFile = (HiddenField)e.Row.FindControl("hdnProductFile");
            if (!string.IsNullOrEmpty(hdnProductFile.Value))
            {
                HyperLink hypViewProductFile = (HyperLink)e.Row.FindControl("hypViewProductFile");
                string strMainFolderPath = string.Format("~/incentives/Files/Approval/{0}", hdnAppNo.Value);
                strMainFolderPath = strMainFolderPath + hdnProductFile.Value;
                hypViewProductFile.NavigateUrl = strMainFolderPath;
                hypViewProductFile.Visible = true;
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            //e.Row.Cells[4].Visible = false;
        }
    }

    protected void grdStatutoryClearence_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridViewRow objHeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableHeaderCell objSlNoCell = new TableHeaderCell() { RowSpan = 2, Text = "Sl#" };
            objHeaderRow.Cells.Add(objSlNoCell);
            TableHeaderCell objHcInstitute = new TableHeaderCell() { RowSpan = 2, Text = "Statutory Clearence" };
            objHeaderRow.Cells.Add(objHcInstitute);
            TableHeaderCell objHcLocation = new TableHeaderCell() { ColumnSpan = 2, Text = "Period" };
            objHeaderRow.Cells.Add(objHcLocation);
            //if (Request.QueryString["type"] == null)
            //{
            //    TableHeaderCell objHcAmt = new TableHeaderCell() { RowSpan = 2, Text = "Action" };
            //    objHeaderRow.Cells.Add(objHcAmt);
            //}
            objGridView.Controls[0].Controls.AddAt(0, objHeaderRow);
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
            TableHeaderCell objSlNoCell = new TableHeaderCell() { RowSpan = 2, Text = "Sl#" };
            objHeaderRow.Cells.Add(objSlNoCell);
            TableHeaderCell objHcInstitute = new TableHeaderCell() { RowSpan = 2, Text = "Name of Financial Institution" };
            objHeaderRow.Cells.Add(objHcInstitute);
            TableHeaderCell objHcLocation = new TableHeaderCell() { ColumnSpan = 2, Text = "Location" };
            objHeaderRow.Cells.Add(objHcLocation);
            TableHeaderCell objHcAmt = new TableHeaderCell() { RowSpan = 2, Text = "Amount" };
            objHeaderRow.Cells.Add(objHcAmt);
            TableHeaderCell objhcSanctionDate = new TableHeaderCell() { RowSpan = 2, Text = "Sanction Date" };
            objHeaderRow.Cells.Add(objhcSanctionDate);
            TableHeaderCell objHcAvailedAmt = new TableHeaderCell() { RowSpan = 2, Text = "Availed Amount" };
            objHeaderRow.Cells.Add(objHcAvailedAmt);
            TableHeaderCell objHcAvailedDate = new TableHeaderCell() { RowSpan = 2, Text = "Availed Date" };
            objHeaderRow.Cells.Add(objHcAvailedDate);
            //if (Request.QueryString["type"] == null)
            //{
            //    TableHeaderCell objHcTakeAction = new TableHeaderCell() { RowSpan = 2, Text = "Action" };
            //    objHeaderRow.Cells.Add(objHcTakeAction);
            //}
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
            //e.Row.Cells[8].Visible = false;
        }

    }
    #endregion

    #region page events
    /// <summary>
    /// if the verification checkbox is checked then user needs to enter the aadhaar number else hide the columns
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void chkVerification_CheckChanged(object sender, EventArgs e)
    //{
    //    //if (chkverification.Checked)
    //    //{
    //    //    for (int i = 0; i < tblOfficer.Rows.Count; i++)
    //    //    {
    //    //        tblOfficer.Rows[i].Cells[3].Visible = true;
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    for (int i = 0; i < tblOfficer.Rows.Count; i++)
    //    //    {
    //    //        tblOfficer.Rows[i].Cells[3].Visible = false;
    //    //    }
    //    //}
    //    BindAddMoreGrids((int)enGridType.officer, enGridName.officer.ToString(), grdOffice);
    //}

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
        IRDetails objIrDetails = new IRDetails()
        {
            intAppNo = Convert.ToInt32(Request.QueryString["id"]),
            strInspectionReport = txtDateOfInspection.Text,
            // ControlMeasures = txtPollutionControl.Text,
            //IndSafety = txtSafety.Text,
            PowerLoad = txtPowerLoad.Text,
            CppDetails = txtCpp.Text,
            strRemarks = txtRemarks.Text,
            strSuggestions = txtSuggestions.Text,
            inCreatedBy = Convert.ToInt32(Session["userId"]),
            //strSignature = hdnSignature.Value,
            strCommisioningDate = txtPowerCommisioning.Text,
            strPlantInvestDate = txtDateOfPlant.Text,
            strProductCode = txtProductCode.Text.Trim(),
            strProductName = txtName.Text.Trim()
        };
        CommonFunctions objFunction = new CommonFunctions();

        /*Officer*/
        DataTable dtOfficer = CreateChildTable((int)enGridType.officer);
        dtOfficer.TableName = "Officer";
        if (grdOffice.Rows.Count > 0)
        {
            for (int i = 0; i < grdOffice.Rows.Count; i++)
            {
                DataRow dRow = dtOfficer.NewRow();
                GridViewRow Grow = grdOffice.Rows[i];
                dRow["strOfficer"] = Grow.Cells[1].Text;
                dRow["strDesignation"] = Grow.Cells[2].Text;
                dRow["strAuthority"] = Grow.Cells[3].Text;
                dtOfficer.Rows.Add(dRow);
            }

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
            HiddenField hdnUnit = (HiddenField)GRow.FindControl("hdnUnit");
            HiddenField hdnUnitOthers = (HiddenField)GRow.FindControl("hdnUnitOthers");
            HiddenField hdnIsMainProduct = (HiddenField)GRow.FindControl("hdnIsMainProduct");
            dRow["item"] = GRow.Cells[1].Text;
            if (string.IsNullOrEmpty(GRow.Cells[2].Text.Trim()) || GRow.Cells[2].Text.Trim() == "&nbsp;")
            {
                dRow["Code"] = string.Empty;
            }
            else
            {
                dRow["Code"] = GRow.Cells[2].Text;
            }
            dRow["Qty"] = GRow.Cells[3].Text;
            dRow["Unit"] = hdnUnit.Value;
            dRow["Cost"] = GRow.Cells[5].Text;
            dRow["dtmProd"] = GRow.Cells[6].Text;
            dRow["unitOthers"] = hdnUnitOthers.Value;
            dRow["bitMainProduct"] = hdnIsMainProduct.Value;
            if (string.IsNullOrEmpty(GRow.Cells[5].Text) || GRow.Cells[5].Text == "&nbsp;")
            {
                dRow["Cost"] = "0.00";
            }
            else
            {
                dRow["Cost"] = GRow.Cells[5].Text;
            }
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
            TextBox txtOriginalExpIncuured = (TextBox)GRow.FindControl("txtOriginalExpIncuured");
            dRow["slno"] = grdCapitalInvestment.DataKeys[GRow.RowIndex].Value;
            dRow["PerDpr"] = string.IsNullOrEmpty(txtAsPerDpr.Text) ? "0.00" : txtAsPerDpr.Text;
            dRow["ActualExpenditure"] = string.IsNullOrEmpty(txtActualExpIncuured.Text) ? "0.00" : txtActualExpIncuured.Text;
            dRow["OriginalExpenditure"] = string.IsNullOrEmpty(txtOriginalExpIncuured.Text) ? "0.00" : txtOriginalExpIncuured.Text;
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
            dRow["institute"] = GRow.Cells[1].Text;
            dRow["state"] = GRow.Cells[2].Text;
            dRow["City"] = GRow.Cells[3].Text;
            dRow["Amt"] = string.IsNullOrEmpty(GRow.Cells[4].Text) ? "0.00" : GRow.Cells[4].Text;
            dRow["SanctionDate"] = GRow.Cells[5].Text;
            dRow["AvailedAmt"] = string.IsNullOrEmpty(GRow.Cells[6].Text) ? "0.00" : GRow.Cells[6].Text;
            dRow["AvailedDate"] = GRow.Cells[7].Text; ;
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
            dRow["institute"] = GRow.Cells[1].Text;
            dRow["state"] = GRow.Cells[2].Text;
            dRow["City"] = GRow.Cells[3].Text;
            dRow["Amt"] = string.IsNullOrEmpty(GRow.Cells[4].Text) ? "0.00" : GRow.Cells[4].Text; ;
            dRow["SanctionDate"] = GRow.Cells[5].Text;
            dRow["AvailedAmt"] = string.IsNullOrEmpty(GRow.Cells[6].Text) ? "0.00" : GRow.Cells[6].Text; ;
            dRow["AvailedDate"] = GRow.Cells[7].Text; ;
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
            HiddenField hdnApprovalId = (HiddenField)GRow.FindControl("hdnApprovalId");
            HiddenField hdnProductFile = (HiddenField)GRow.FindControl("hdnProductFile");
            HiddenField hdnOthers = (HiddenField)GRow.FindControl("hdnOthers");
            dRow["name"] = GRow.Cells[1].Text;
            dRow["authority"] = hdnApprovalId.Value;
            dRow["others"] = hdnOthers.Value;
            dRow["filename"] = hdnProductFile.Value;
            dtApproval.Rows.Add(dRow);
        }
        if (dtApproval.Rows.Count > 0)
        {
            objIrDetails.strXmlApproval = objFunction.GetSTRXMLResult(dtApproval);
        }

        ///FOR PLANT AND MACHINERY
        DataTable dtMachinery = CreateMachineryTable();

        if (gvPlant.Rows.Count > 0)
        {
            dtMachinery.TableName = "Machinery";
            for (int cnt = 0; cnt < gvPlant.Rows.Count; cnt++)
            {
                GridViewRow GRow = gvPlant.Rows[cnt];
                DataRow dRow = dtMachinery.NewRow();
                dRow["MachineryName"] = GRow.Cells[1].Text;
                if (string.IsNullOrEmpty(GRow.Cells[2].Text.Trim()) || GRow.Cells[2].Text.Trim() == "&nbsp;")
                {
                    dRow["DateofPurchase"] = string.Empty;
                }
                else
                {
                    dRow["DateofPurchase"] = GRow.Cells[2].Text;
                }

                if (string.IsNullOrEmpty(GRow.Cells[3].Text) || GRow.Cells[3].Text == "&nbsp;")
                {
                    dRow["Cost"] = "0.00";
                }
                else
                {
                    dRow["Cost"] = GRow.Cells[3].Text;
                }
                dtMachinery.Rows.Add(dRow);
            }
        }
        if (dtMachinery.Rows.Count > 0)
        {
            CommonFunctions obj = new CommonFunctions();
            objIrDetails.strXmlMachinery = obj.GetSTRXMLResult(dtMachinery);
        }
        DataTable dtClearence = CreateChildTable((int)enGridType.statutoryclearence);
        dtClearence.TableName = "dtClearence";
        for (int cnt = 0; cnt < grdStatutoryClearence.Rows.Count; cnt++)
        {
            DataRow dRow = dtClearence.NewRow();
            GridViewRow gRow = grdStatutoryClearence.Rows[cnt];
            HiddenField hdnClearence = (HiddenField)gRow.FindControl("hdnClearence");
            dRow["clearence"] = hdnClearence.Value;
            dRow["fromDate"] = gRow.Cells[2].Text;
            dRow["toDate"] = gRow.Cells[3].Text;
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
            GridViewRow gRow = gvLOCDetails.Rows[cnt];
            HiddenField hdnState = (HiddenField)gRow.FindControl("hdnState");
            HiddenField hdnDistrict = (HiddenField)gRow.FindControl("hdnDistrict");
            dRow["unitname"] = gRow.Cells[1].Text;
            dRow["product"] = gRow.Cells[2].Text;
            dRow["totalCapacity"] = gRow.Cells[3].Text;
            dRow["state"] = hdnState.Value;
            dRow["district"] = hdnDistrict.Value;
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
            GridViewRow grow = grdIncentiveApplied.Rows[cnt];
            DataRow dRow = dtApplied.NewRow();
            HiddenField hdnType = (HiddenField)grow.FindControl("hdnType");
            HiddenField hdnIpr = (HiddenField)grow.FindControl("hdnIpr");
            dRow["type"] = hdnType.Value;
            dRow["quantam"] = string.IsNullOrEmpty(grow.Cells[2].Text) ? "0.00" : grow.Cells[2].Text;
            dRow["fromdate"] = grow.Cells[3].Text;
            dRow["todate"] = grow.Cells[4].Text;
            dRow["ipr"] = hdnIpr.Value;
            dtApplied.Rows.Add(dRow);
        }
        if (dtApplied.Rows.Count > 0)
        {
            objIrDetails.strXmlApplied = objFunction.GetSTRXMLResult(dtApplied);
        }

        Incentive_PCMaster objMaster = new Incentive_PCMaster();
        objMaster.intApplyFlag = 2;
        objMaster.intAppNo = Convert.ToInt32(Request.QueryString["id"]);
        #region set properties
        objMaster.intAppFor = Convert.ToInt32(drpApplicationType.SelectedValue);
        objMaster.strChngIn = GetCheckBoxListValues(chkLstChange);
        objMaster.strEINEMIIPMTNo = txtEin.Text.Trim();
        objMaster.strCompName = txtEnterpriseName.Text.Trim();
        objMaster.intUnitCat = Convert.ToInt32(drpUnitCategory.SelectedValue);
        objMaster.intUnitType = Convert.ToInt32(drpCompanyType.SelectedValue);
        objMaster.intOrgType = Convert.ToInt32(drpOrganizationType.SelectedValue);
        objMaster.strOwnerName = txtOwnerName.Text.Trim();
        objMaster.intSalutation = Convert.ToInt16(drpSalutation.SelectedValue);
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
        objMaster.dtmProdComm = txtProdCommencement.Text;
        objMaster.dtmFFCI = txtDateFFI.Text;
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
        if (!string.IsNullOrEmpty(txtUnskilled.Text.Trim()))
        {
            objMaster.intUnskilled = Convert.ToInt32(txtUnskilled.Text.Trim());
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
        if (!string.IsNullOrEmpty(txtGeneral.Text.Trim()))
        {
            objMaster.intGeneral = Convert.ToInt32(txtGeneral.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtDirectEmployement.Text.Trim()))
        {
            objMaster.intTotalEmployee = Convert.ToInt32(txtDirectEmployement.Text.Trim());
        }
        if (!string.IsNullOrEmpty(txtContractualEmp.Text.Trim()))
        {
            objMaster.intContractual = Convert.ToInt32(txtContractualEmp.Text.Trim());
        }
        objMaster.intCreatedBy = Convert.ToInt32(Session["userId"]);
        objMaster.strOfficeMobCode = ddlCode.SelectedValue;
        objMaster.strOfficeFaxCode = drpFx.SelectedValue;
        objMaster.strEntFaxCode = drpEnterpriseFax.SelectedValue;
        objMaster.strEntMobCode = drpEntCode.SelectedValue;
        objMaster.strDateConnection = txtPowerConnection.Text;
        objMaster.strUnitOthersk = txtOtherOrg.Text.Trim();
        Button objButton = (Button)sender;
        string strCommandArg = objButton.CommandArgument;
        if (string.Equals(strCommandArg, "d", StringComparison.OrdinalIgnoreCase))
        {
            objMaster.strActionCode = "Draf";
        }
        else
        {
            objMaster.strActionCode = "add";
        }

        #endregion

        IncentiveMasterBusinessLayer objBuisness = new IncentiveMasterBusinessLayer();
        int intRetValue = objBuisness.IRForm_AED(objIrDetails, objMaster);
        if (intRetValue == 2 || intRetValue == 1)
        {
            if (string.Equals(strCommandArg, "d", StringComparison.OrdinalIgnoreCase))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirectno('" + Messages.ShowMessage(Convert.ToString(intRetValue)) + "');</script>", false);
                //ScriptManager.RegisterStartupScript(btnConfirm, this.GetType(), "Myalert", string.Format("alert('{0}');window.location.href='IRForm.aspx?ID={1}&linkm={2}&linkn={3}&btn={4}&tab={5}&ranNum={6}';", Messages.ShowMessage("2"), Request.QueryString["ID"], Request.QueryString["linkm"], Request.QueryString["linkn"], Request.QueryString["btn"], Request.QueryString["tab"], Session["RandomNo"]), true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + Messages.ShowMessage(Convert.ToString(intRetValue)) + "');</script>", false);
                //ScriptManager.RegisterStartupScript(btnConfirm, this.GetType(), "Myalert", string.Format("alert('{0}');window.location.href='ViewIncentiveApplication.aspx?ID={1}&linkm={2}&linkn={3}&btn={4}&tab={5}&ranNum={6}';", Messages.ShowMessage("2"), Request.QueryString["ID"], Request.QueryString["linkm"], Request.QueryString["linkn"], Request.QueryString["btn"], Request.QueryString["tab"], Session["RandomNo"]), true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(btnConfirm, this.GetType(), "Myalert", string.Format("alert('{0}');", Messages.ShowMessage("4")), true);
        }
    }

    //protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindDistrict(drpDistrict, ddlState.SelectedValue);
    //}

    protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnUnitOthers = (HiddenField)e.Row.FindControl("hdnUnitOthers");
            HiddenField hdnUnit = (HiddenField)e.Row.FindControl("hdnUnit");

            if (!string.IsNullOrEmpty(hdnUnit.Value))
            {
                int intUnitType = Convert.ToInt32(hdnUnit.Value);
                if (intUnitType == conUnitothers)
                {
                    e.Row.Cells[4].Text = hdnUnitOthers.Value;
                }
            }
        }
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
        ListItem list = new ListItem() { Text = "--Select--", Value = "0" };
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
    private void GetApplicationDetails(DataSet objDs)
    {
        if (objDs != null && objDs.Tables.Count > 0)
        {
            string strMainFolderPath = string.Empty;
            DataTable dtPcDetails = new DataTable();
            dtPcDetails = objDs.Tables[0];
            if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
            {
                DataRow objRow = dtPcDetails.Rows[0];

                drpApplicationType.SelectedValue = objRow["vchAppFor"].ToString();
                int intApplicationType = Convert.ToInt32(drpApplicationType.SelectedValue);

                if (intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod)
                {
                    IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
                    rdBtnApplicationFor.Items.Clear();
                    rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
                    rdBtnApplicationFor.Items[0].Selected = true;
                    objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
                    {
                        Action = "amd",
                        Name = string.Empty,
                        ID = string.Empty,
                        Param = 0,
                        Param_1 = 0,
                        Param_2 = string.Empty,
                        Param_3 = string.Empty
                    });
                    divChangeIn.Visible = true;
                }
                else if (intApplicationType == (int)enAppFor.New || intApplicationType == (int)enAppFor.Rehabilitated_New || intApplicationType == (int)enAppFor.Transferred_new || intApplicationType == (int)enAppFor.Migrated_new)
                {
                    IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
                    rdBtnApplicationFor.Items.Clear();
                    rdBtnApplicationFor.Items.Add(new ListItem("New", "1"));
                    rdBtnApplicationFor.Items[0].Selected = true;
                    objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
                    {
                        Action = "new",
                        Name = string.Empty,
                        ID = string.Empty,
                        Param = 0,
                        Param_1 = 0,
                        Param_2 = string.Empty,
                        Param_3 = string.Empty
                    });

                }
                else if (intApplicationType == (int)enAppFor.New_EMD)
                {
                    IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
                    rdBtnApplicationFor.Items.Clear();
                    rdBtnApplicationFor.Items.Add(new ListItem("New", "1"));
                    rdBtnApplicationFor.Items[0].Selected = true;
                    drpApplicationType.Items.Clear();
                    drpApplicationType.Items.Add(new ListItem("New Unit E/M/D", ((int)enAppFor.New_EMD).ToString()));
                    drpApplicationType.SelectedIndex = 0;
                    divChangeIn.Visible = true;
                }
                else if (intApplicationType == (int)enAppFor.exist)
                {
                    IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
                    rdBtnApplicationFor.Items.Clear();
                    rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
                    rdBtnApplicationFor.Items[0].Selected = true;
                    drpApplicationType.Items.Clear();
                    drpApplicationType.Items.Add(new ListItem("Existing", ((int)enAppFor.exist).ToString()));
                    drpApplicationType.SelectedIndex = 0;
                    divChangeIn.Visible = false;
                }
                hdnInvestorId.Value = objRow["intCreatedBy"].ToString();
                string strChangeIn = objRow["vchChngIn"].ToString();
                UpdateCheckBoxList(strChangeIn, chkLstChange);
                lblApplicationNo.Text = objRow["vchAppFormattedNo"].ToString();
                hdnAppNo.Value = objRow["vchAppNo"].ToString();
                strMainFolderPath = string.Format("~/incentives/Files/PC/PC{0}/", lblApplicationNo.Text);
                txtEin.Text = objRow["vchEINEMIIPMTNo"].ToString();
                lblIndustryCode.Text = objRow["vchIndustryCode"].ToString();
                txtEin.Text = objRow["vchEINEMIIPMTNo"].ToString();
                txtEin.Enabled = false;
                txtUan.Enabled = false;
                txtUan.Text = objRow["vchUAN"].ToString();
                txtEnterpriseName.Text = objRow["vchCompName"].ToString();
                drpCompanyType.SelectedValue = objRow["intUnitType"].ToString();
                drpOrganizationType.SelectedValue = objRow["intOrgType"].ToString();
                txtOtherOrg.Text = objRow["vchOrgOther"].ToString();
                drpOwnerType.SelectedValue = objRow["intOwnerCode"].ToString();
                drpUnitCategory.SelectedValue = objRow["intUnitCat"].ToString();
                txtOwnerName.Text = objRow["vchOwnerName"].ToString();
                ddlSector.SelectedValue = objRow["intSectorId"].ToString();
                BindSubSector(ddlSector.SelectedValue);
                ddlSubSector.SelectedValue = objRow["intSubSectorId"].ToString();
                txtProdCommencement.Text = objRow["dtmProdComm"].ToString();
                txtOfficeAddress.Text = objRow["vchOffcAddr"].ToString();
                txtOfficeEmail.Text = objRow["vchOffcEmail"].ToString();
                txtOfficeFax.Text = objRow["vchOffcFaxNo"].ToString();
                txtOfficePhone.Text = objRow["vchOffcPhNo"].ToString();
                txtOfficeWebsite.Text = objRow["vchOffcWebsite"].ToString();
                ddlCode.SelectedValue = objRow["vchOfficeMobCode"].ToString();
                drpEntCode.SelectedValue = objRow["vchEntMobCode"].ToString();
                drpFx.SelectedValue = objRow["vchOfficeFaxCode"].ToString();
                drpEnterpriseFax.SelectedValue = objRow["vchEntFaxCode"].ToString();
                txtEnterpriseAddress.Text = objRow["vchAddr"].ToString();
                txtPhoneNo.Text = objRow["vchPhNo"].ToString();
                txtFax.Text = objRow["vchFaxNo"].ToString();
                txtEmail.Text = objRow["vchEmail"].ToString();
                txtWebsite.Text = objRow["vchWebsite"].ToString();
                ddlDistrict.SelectedValue = objRow["intDistrict"].ToString();
                BindBlock(ddlDistrict.SelectedValue);
                ddlBlock.SelectedValue = objRow["intBlock"].ToString();
                drpSalutation.SelectedValue = objRow["intsalutation"].ToString();
                txtDateFFI.Text = objRow["dtmFFCI"].ToString();
                lblModeOfInvestment.Text = objRow["VchChange"].ToString();
                //added by Suman in 10-oct-2017

                txtProductCode.Text = objRow["vchProductCode"].ToString();
                txtName.Text = objRow["vchProductName"].ToString();

                txtPowerConnection.Text = objRow["dtmPowerConn"].ToString();
                if (txtPowerConnection.Text == "01-Jan-1900")
                    txtPowerConnection.Text = string.Empty;
            }

            dtPcDetails = objDs.Tables[1];
            if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
            {
                DataRow objRow = dtPcDetails.Rows[0];
                txtSupervisor.Text = objRow["intSupervisor"].ToString();
                txtManagarial.Text = objRow["intManaregailSkill"].ToString();
                txtSkilled.Text = objRow["intSkilled"].ToString();
                txtSemiSkilled.Text = objRow["intSemiSkilled"].ToString();
                txtUnskilled.Text = objRow["intUnSkilled"].ToString();
                txtTotalSc.Text = objRow["intScTotal"].ToString();
                txtTotalSt.Text = objRow["intStTotal"].ToString();
                txtWomen.Text = objRow["intWomen"].ToString();
                txtPhd.Text = objRow["intDisabled"].ToString();
                txtGeneral.Text = objRow["intGeneral"].ToString();
                txtDirectEmployement.Text = objRow["intDirectEmp"].ToString();
                txtContractualEmp.Text = objRow["intContractual"].ToString();
                txtGrandTotal.Text = objRow["TotalSum"].ToString();
                txtTotal.Text = objRow["TotalSumST"].ToString();
            }

            dtPcDetails = objDs.Tables[2];
            if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
            {
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
    private void FillCheckBoxList(DataTable objDt, CheckBoxList objChkList)
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
            FillCheckBoxList(objDa.Tables[1], chkLstChange);
            FillDropDown(objDa.Tables[2], "Organization Type", drpOrganizationType);
            FillDropDown(objDa.Tables[3], "Owner Type", drpOwnerType);
            FillDropDown(objDa.Tables[4], "Unit Category", drpUnitCategory);
            FillDropDown(objDa.Tables[5], "Company Type", drpCompanyType);
            //grdCapitalInvestment.DataSource = objDa.Tables[7];
            //grdCapitalInvestment.DataBind();
            grdProblems.DataSource = objDa.Tables[8];
            grdProblems.DataBind();
          //  FillDropDown(objDa.Tables[10], "Unit Type", drpUnitType);
            FillDropDown(objDa.Tables[11], "District", ddlDistrict);
            FillDropDown(objDa.Tables[12], "Sector", ddlSector);
            FillDropDown(objDa.Tables[13], "Select", ddlCode);
            FillDropDown(objDa.Tables[13], "Select", drpFx);
            FillDropDown(objDa.Tables[13], "Select", drpEntCode);
            FillDropDown(objDa.Tables[13], "Select", drpEnterpriseFax);
            FillDropDown(objDa.Tables[14], "", drpSalutation);
        }

        objDa = objBuisnessLayer.BindDropdown("ir");
        if (objDa != null)
        {
            //FillDropDown(objDa.Tables[1], "Incentive Type", drpIncentiveType);
            //FillDropDown(objDa.Tables[0], "IPR", drpIpr);
            //FillDropDown(objDa.Tables[2], "Authority", drpApprovalAuthority);
            //FillDropDown(objDa.Tables[3], "Unit Type", drpUnitType);
            //FillDropDown(objDa.Tables[4], "Clearence", drpClearence);
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
        DataColumn dcId = new DataColumn()
        {
            ColumnName = "id",
            AutoIncrement = true,
            AutoIncrementSeed = 1,
            AutoIncrementStep = 1
        };
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
                dt.Columns.Add(new DataColumn("DtmProd"));
                dt.Columns.Add(new DataColumn("UnitId"));
                dt.Columns.Add(new DataColumn("unitOthers"));
                dt.Columns.Add(new DataColumn("bitMainProduct"));
                dt.Columns.Add(new DataColumn("VchIsMainProduct"));
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
                dt.Columns.Add(new DataColumn("authorityName"));
                dt.Columns.Add(new DataColumn("others"));
                dt.Columns.Add(new DataColumn("filename"));
                break;
            case (int)enGridType.statutoryclearence:
                dt.Columns.Add(new DataColumn("clearence"));
                dt.Columns.Add(new DataColumn("clearencename"));
                dt.Columns.Add(new DataColumn("fromdate"));
                dt.Columns.Add(new DataColumn("todate"));
                break;
            case (int)enGridType.cInvest:
                dt.Columns.Add(new DataColumn("slno"));
                dt.Columns.Add(new DataColumn("PerDpr"));
                dt.Columns.Add(new DataColumn("ActualExpenditure"));
                dt.Columns.Add(new DataColumn("OriginalExpenditure"));
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
                dt.Columns.Add(new DataColumn("statename"));
                dt.Columns.Add(new DataColumn("district"));
                dt.Columns.Add(new DataColumn("distname"));
                break;
            case (int)enGridType.IPR:
                dt.Columns.Add(new DataColumn("type"));
                dt.Columns.Add(new DataColumn("incentivetext"));
                dt.Columns.Add(new DataColumn("quantam"));
                dt.Columns.Add(new DataColumn("fromdate"));
                dt.Columns.Add(new DataColumn("todate"));
                dt.Columns.Add(new DataColumn("ipr"));
                dt.Columns.Add(new DataColumn("iprtext"));
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
    //private void AddNewItemInGrid(GridViewRow GRow, int intGridType, string strTypeName, GridView objGrid)
    //{
    //    DataTable dtGrid = null;

    //    if (ViewState[strTypeName] != null)
    //    {
    //        dtGrid = (DataTable)ViewState[strTypeName];
    //        if (!dtGrid.Columns["id"].AutoIncrement)
    //        {
    //            dtGrid.Columns["id"].AutoIncrement = true;
    //        }
    //    }
    //    else
    //    {
    //        dtGrid = CreateChildTable(intGridType);
    //    }

    //    DataRow dRow = null;
    //    if (intGridType == (int)enGridType.officer)
    //    {
    //        dRow = dtGrid.NewRow();
    //        //dRow["strOfficer"] = txtOfficerName.Text;
    //        //dRow["strDesignation"] = txtDesignation.Text;
    //        //dRow["strAuthority"] = txtOrganization.Text;
    //        ////dRow["AadhaarNo"] = txtAadhaarNo.Text;
    //        //dtGrid.Rows.Add(dRow);
    //        //ViewState[strTypeName] = dtGrid;
    //        //txtOfficerName.Text = string.Empty;
    //        //txtDesignation.Text = string.Empty;
    //        //txtOrganization.Text = string.Empty;
    //    }
    //    else if (intGridType == (int)enGridType.Products)
    //    {
    //        if (!string.IsNullOrEmpty(Convert.ToString(ViewState["EditIndex"])) && (Convert.ToInt32(ViewState["EditIndex"]) != -1))
    //        {
    //            int rowIndex = Convert.ToInt32(ViewState["EditIndex"]);
    //            //removed duplicacy and code has been removed as a mandatory field
    //            dtGrid.Rows[rowIndex]["item"] = txtItemProduct.Text;
    //            dtGrid.Rows[rowIndex]["Code"] = txtItemCode.Text;
    //            dtGrid.Rows[rowIndex]["Qty"] = txtQuantity.Text;
    //            dtGrid.Rows[rowIndex]["Unit"] = drpUnitType.SelectedItem.Text;
    //            dtGrid.Rows[rowIndex]["unitId"] = drpUnitType.SelectedValue;
    //            dtGrid.Rows[rowIndex]["dtmProd"] = txtDateOfProd.Text;
    //            dtGrid.Rows[rowIndex]["Cost"] = txtCost.Text;
    //            dtGrid.Rows[rowIndex]["unitothers"] = txtUnitType.Text;
    //            dtGrid.Rows[rowIndex]["bitMainProduct"] = chkMainCategory.Checked;
    //            dtGrid.Rows[rowIndex]["VchIsMainProduct"] = chkMainCategory.Checked ? "Yes" : "No";
    //            ViewState[strTypeName] = dtGrid;

    //            ViewState["EditIndex"] = -1;
    //            lnkAdd.Text = "<i class='fa fa-plus-square' ></i>";
    //        }
    //        else
    //        {
    //            dRow = dtGrid.NewRow();
    //            dRow["item"] = txtItemProduct.Text;
    //            dRow["Code"] = txtItemCode.Text;
    //            dRow["Qty"] = txtQuantity.Text;
    //            dRow["Unit"] = drpUnitType.SelectedItem.Text;
    //            dRow["unitId"] = drpUnitType.SelectedValue;
    //            dRow["dtmProd"] = txtDateOfProd.Text;
    //            dRow["Cost"] = txtCost.Text;
    //            dRow["unitothers"] = txtUnitType.Text;
    //            dRow["bitMainProduct"] = chkMainCategory.Checked;
    //            dRow["VchIsMainProduct"] = chkMainCategory.Checked ? "Yes" : "No";
    //            dtGrid.Rows.Add(dRow);
    //            ViewState[strTypeName] = dtGrid;
    //        }

    //        txtItemProduct.Text = string.Empty;
    //        txtItemCode.Text = string.Empty;
    //        txtQuantity.Text = string.Empty;
    //        drpUnitType.SelectedValue = "0";
    //        txtDateOfProd.Text = string.Empty;
    //        txtCost.Text = string.Empty;
    //        txtUnitType.Text = string.Empty;
    //        chkMainCategory.Checked = false;

    //    }
    //    if (intGridType == (int)enGridType.termLoan || intGridType == (int)enGridType.workingCapital)
    //    {
    //        if (intGridType == (int)enGridType.termLoan)
    //        {
    //            dRow = dtGrid.NewRow();
    //            dRow["institute"] = txtTlInstitue.Text;
    //            dRow["state"] = txtState.Text;
    //            dRow["City"] = txtCity.Text;
    //            dRow["Amt"] = txtAmount.Text;
    //            dRow["SanctionDate"] = txtSanctionDate.Text;
    //            dRow["AvailedAmt"] = txtAvailedAmount.Text;
    //            dRow["AvailedDate"] = txtAvailedDate.Text;
    //            txtTlInstitue.Text = string.Empty;
    //            txtState.Text = string.Empty;
    //            txtCity.Text = string.Empty;
    //            txtAmount.Text = string.Empty;
    //            txtSanctionDate.Text = string.Empty;
    //            txtAvailedAmount.Text = string.Empty;
    //            txtAvailedDate.Text = string.Empty;

    //        }
    //        else if (intGridType == (int)enGridType.workingCapital)
    //        {
    //            dRow = dtGrid.NewRow();
    //            dRow["institute"] = txtWcInstitue.Text;
    //            dRow["state"] = txtWcState.Text;
    //            dRow["City"] = txtWcCity.Text;
    //            dRow["Amt"] = txtWcAmt.Text;
    //            dRow["SanctionDate"] = txtWcSanctionDate.Text;
    //            dRow["AvailedAmt"] = txtWcAvailedAmt.Text;
    //            dRow["AvailedDate"] = txtWcAvailedDate.Text;


    //        }

    //        dtGrid.Rows.Add(dRow);
    //        ViewState[strTypeName] = dtGrid;
    //        txtWcInstitue.Text = string.Empty;
    //        txtWcState.Text = string.Empty;
    //        txtWcCity.Text = string.Empty;
    //        txtWcSanctionDate.Text = string.Empty;
    //        txtWcAvailedAmt.Text = string.Empty;
    //        txtWcAvailedDate.Text = string.Empty;
    //    }
    //    else if (intGridType == (int)enGridType.Approval)
    //    {
    //        dRow = dtGrid.NewRow();
    //        dRow["name"] = txtNameOfSite.Text;
    //        dRow["authority"] = drpApprovalAuthority.SelectedValue;
    //        dRow["authorityname"] = drpApprovalAuthority.SelectedItem.Text;
    //        dRow["others"] = txtOthers.Text;
    //        dRow["filename"] = hdnSupDocument.Value;
    //        dtGrid.Rows.Add(dRow);
    //        ViewState[strTypeName] = dtGrid;
    //        txtNameOfSite.Text = string.Empty;
    //        drpApprovalAuthority.SelectedValue = "0";
    //        txtOthers.Text = string.Empty;
    //        hdnSupDocument.Value = string.Empty;
    //    }
    //    else if (intGridType == (int)enGridType.statutoryclearence)
    //    {
    //        dRow = dtGrid.NewRow();
    //        dRow["clearence"] = drpClearence.SelectedValue;
    //        dRow["clearencename"] = drpClearence.SelectedItem.Text;
    //        dRow["fromdate"] = txtFromClDate.Text;
    //        dRow["toDate"] = txtToClDate.Text;
    //        dtGrid.Rows.Add(dRow);
    //        ViewState[strTypeName] = dtGrid;
    //        drpClearence.SelectedValue = "0";
    //        txtFromClDate.Text = string.Empty;
    //        txtToClDate.Text = string.Empty;
    //    }
    //    else if (intGridType == (int)enGridType.otherEnterprise)
    //    {
    //        dRow = dtGrid.NewRow();
    //        dRow["unitname"] = txtUnit.Text;
    //        dRow["product"] = txtProduct.Text;
    //        dRow["totalCapacity"] = txtCapacity.Text;
    //        dRow["state"] = ddlState.SelectedValue;
    //        dRow["district"] = ddlDistrict.SelectedValue;
    //        dRow["statename"] = ddlState.SelectedItem.Text;
    //        dRow["distname"] = ddlDistrict.SelectedItem.Text;
    //        dtGrid.Rows.Add(dRow);
    //        ViewState[strTypeName] = dtGrid;


    //    }
    //    else if (intGridType == (int)enGridType.IPR)
    //    {
    //        dRow = dtGrid.NewRow();
    //        dRow["type"] = drpIncentiveType.SelectedValue;
    //        dRow["incentivetext"] = drpIncentiveType.SelectedItem.Text;
    //        dRow["quantam"] = txtQuantam.Text;
    //        dRow["fromdate"] = txtFromDate.Text;
    //        dRow["todate"] = txtToDate.Text;
    //        dRow["ipr"] = drpIpr.SelectedValue;
    //        dRow["iprtext"] = drpIpr.SelectedItem.Text;
    //        dtGrid.Rows.Add(dRow);
    //        ViewState[strTypeName] = dtGrid;
    //    }
    //    BindAddMoreGrids(intGridType, strTypeName, objGrid);
    //}

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
            divUnitCond.Visible = true;
            lblUnitCOnd.Text = "Original Enterprise";
        }
        else if (rdBtnUntiCond.SelectedValue == "2")
        {
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
        if (dtExisting != null)
        {
            dtGrid = dtExisting;
        }
        else if (dtExisting == null && ViewState[tableName] != null)
        {
            dtGrid = (DataTable)ViewState[tableName];
        }
        ObjGrid.DataSource = dtGrid;
        ObjGrid.DataBind();
        //if ((int)enGridType.officer == intGridType)
        //{
        //    if (chkverification.Checked)
        //    {
        //        ObjGrid.Columns[4].Visible = true;
        //    }
        //    else
        //    {
        //        ObjGrid.Columns[4].Visible = false;
        //    }
        //}
    }

    /// <summary>
    /// Function to set all the labels as per new and EMD selected
    /// </summary>
    /// <param name="strValue"></param>
    private void SetAllLabels(string strValue)
    {
        //  lblProductionControl.Text = strValue;
        lblPowerLoad.Text = strValue;
        lblEmployement.Text = strValue;
        lblCapitalInvest.Text = strValue;
        lblApproval.Text = strValue;
        lblStatutaryCLearence.Text = strValue;
    }


    private void BindSubSector(string strstate)
    {
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        objBuisnessLayer.BindDropdown(ddlSubSector, new IncentiveMaster()
        {
            Action = "sub",
            Param = 0,
            Param_1 = 0,
            Param_2 = strstate,
            Param_3 = string.Empty
        });
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
        ListItem list = new ListItem() { Text = "-Select District-", Value = "0" };
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
        ListItem list = new ListItem() { Text = "--Select Block--", Value = "0" };
        ddlBlock.Items.Insert(0, list);
    }
    #endregion
    protected void grdProducts_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        //DataTable dtRemove = new DataTable();
        //ViewState["EditIndex"] = -1;
        //int rowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
        //if (e.CommandName == "Upd") //FOR UPDATE
        //{
        //    lnkAdd.Text = "Update";
        //    ViewState["EditIndex"] = rowIndex;

        //    //  txtDrugNm.Text = ((Label)grdDrug.Rows[rowIndex].FindControl("lblDrugNm")).Text;
        //    txtItemProduct.Text = grdProducts.Rows[rowIndex].Cells[1].Text;
        //    txtItemCode.Text = grdProducts.Rows[rowIndex].Cells[2].Text;
        //    txtQuantity.Text = grdProducts.Rows[rowIndex].Cells[3].Text;
        //    drpUnitType.SelectedValue = ((HiddenField)grdProducts.Rows[rowIndex].FindControl("hdnUnit")).Value;
        //    txtDateOfProd.Text = grdProducts.Rows[rowIndex].Cells[4].Text;

        //}

    }
    //protected void lnkMachinery_Click(object sender, EventArgs e)
    //{
    //    DataTable dtMachinery = null;
    //    DataRow dRow = null;
    //    if (ViewState["Machinery"] != null)
    //    {
    //        dtMachinery = (DataTable)ViewState["Machinery"];
    //        if (!dtMachinery.Columns["id"].AutoIncrement)
    //        {
    //            dtMachinery.Columns["id"].AutoIncrement = true;
    //        }
    //    }
    //    else
    //    {
    //        dtMachinery = CreateMachineryTable();
    //    }
    //    dRow = dtMachinery.NewRow();
    //    if (txtMachinery.Text != "" && txtAmt.Text != "")
    //    {
    //        dRow["MachineryName"] = txtMachinery.Text.Trim();
    //        dRow["DateofPurchase"] = txtDateofPurchase.Text;
    //        if (txtAmt.Text == "")
    //            dRow["Cost"] = "0";
    //        else
    //            dRow["Cost"] = txtAmt.Text.Trim();
    //        dtMachinery.Rows.Add(dRow);
    //        ViewState["Machinery"] = dtMachinery;
    //        BindMachineryGridview(dtMachinery);
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('Plant & Machinery Details cannot be blank');</script>", false);
    //    }

    //    txtMachinery.Text = string.Empty;
    //    txtDateofPurchase.Text = string.Empty;
    //    txtAmt.Text = string.Empty;
    //}
    private void BindMachineryGridview(DataTable dtView)
    {
        gvPlant.DataSource = dtView;
        gvPlant.DataBind();
    }
    protected void gvPlant_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int intRowIndex = Convert.ToInt32(e.CommandArgument);

        if (string.Equals(commandName, "d", StringComparison.OrdinalIgnoreCase))
        {
            int intId = Convert.ToInt32(gvPlant.DataKeys[intRowIndex].Value);
            DataTable dtView = (DataTable)ViewState["Machinery"];
            for (int cnt = 0; cnt < dtView.Rows.Count; cnt++)
            {
                if (dtView.Rows[cnt][0].ToString() == intId.ToString())
                {
                    dtView.Rows[cnt].Delete();
                    dtView.AcceptChanges();
                }
            }
            BindMachineryGridview(dtView);
            ViewState["Machinery"] = dtView;

        }
    }
    private DataTable CreateMachineryTable()
    {
        DataTable dtMachinery = new DataTable();
        DataColumn dcId = new DataColumn() { ColumnName = "id", AutoIncrement = true, AutoIncrementSeed = 1, AutoIncrementStep = 1 };
        dtMachinery.Columns.Add(dcId);
        dtMachinery.Columns.Add(new DataColumn("MachineryName"));
        dtMachinery.Columns.Add(new DataColumn("DateofPurchase"));
        dtMachinery.Columns.Add(new DataColumn("Cost"));
        return dtMachinery;
    }

    private void BindMachinery()
    {
        DataTable dtMachinery = CreateMachineryTable();
        dtMachinery.TableName = "Machinery";
        if ((ViewState["Machinery"] != null))
        {
            dtMachinery = (DataTable)ViewState["Machinery"];
        }
        BindMachineryGridview(dtMachinery);

    }
}