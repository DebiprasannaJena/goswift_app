/*
 * Created By : Ritika Lath
 * Created On : 10th September 2017
 * Description : to show the incentive details filled by the user to the admin
 * Class name :Portal_Incentive_IncentiveDetails
 * file name : IncentiveDetails.aspx.cs
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using BusinessLogicLayer.Proposal;
using EntityLayer.Incentive;
using EntityLayer.Proposal;

public partial class Portal_Incentive_IncentiveDetails : System.Web.UI.Page
{
    const int conUnitothers = 52;
    const int conOrgOthers = 24;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("../default.aspx");
        }
        if (!IsPostBack)
        {
            txtEnterpriseAddress.Attributes.Add("onkeyup", string.Format("return CheckLengthKeyUp('{0}','{1}',200);", txtEnterpriseAddress.ClientID, lblRemark.ClientID));
            txtEnterpriseAddress.Attributes.Add("onchange", string.Format("return checkLength('{0}','{1}',200);", txtEnterpriseAddress.ClientID, lblRemark.ClientID));
            txtOfficeAddress.Attributes.Add("onkeyup", string.Format("return CheckLengthKeyUp('{0}','{1}',200);", txtOfficeAddress.ClientID, lblOfficeAddress.ClientID));
            txtOfficeAddress.Attributes.Add("onchange", string.Format("return checkLength('{0}','{1}',200);", txtOfficeAddress.ClientID, lblOfficeAddress.ClientID));
            txtDateFFI.Attributes.Add("readonly", "readonly");
            txtProdComm.Attributes.Add("readonly", "readonly");
            txtDateFFI.Attributes.Add("readonly", "readonly");
            txtPowerConnection.Attributes.Add("readonly", "readonly");
            BindDropDown();
            GetApplicationDetails();
            ShowOwnerTypeByCode();
            BindProduct();
            pnlMain.Enabled = false;
            string strValuue = rdBtnLstPower.SelectedValue;
            if (strValuue == "1")
            {
                divPower.Visible = true;
            }
        }
    }

    #region Click events
    /// <summary>
    /// based on the organization type selected, change the owner name type and the file type
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void drpOrganizationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowOwnerTypeByCode();
    }

    protected void drpUnitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtUnitType.Visible = false;
        if (drpUnitType.SelectedIndex > 0)
        {
            int intUnitType = Convert.ToInt32(drpUnitType.SelectedValue);
            if (intUnitType == conUnitothers)
            {
                txtUnitType.Visible = true;
            }
        }
    }
    //protected void rdBtnApplicationFor_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int intAppType = Convert.ToInt32(rdBtnApplicationFor.SelectedValue);
    //    IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
    //    if (intAppType == 1)
    //    {
    //        objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
    //        {
    //            Action = "new",
    //            Name = string.Empty,
    //            ID = string.Empty,
    //            Param = 0,
    //            Param_1 = 0,
    //            Param_2 = string.Empty,
    //            Param_3 = string.Empty
    //        });
    //    }
    //    else
    //    {
    //        objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
    //        {
    //            Action = "amd",
    //            Name = string.Empty,
    //            ID = string.Empty,
    //            Param = 0,
    //            Param_1 = 0,
    //            Param_2 = string.Empty,
    //            Param_3 = string.Empty
    //        });
    //    }
    //}

    private void ShowOwnerTypeByCode()
    {
        lblOwnerLabel.Text = string.Empty;
        int intOrgType = Convert.ToInt32(drpOrganizationType.SelectedValue);
        string strType = drpOrganizationType.SelectedItem.Text.ToLower();
        string strOwnerType = "Name of Managing member";
        txtOtherOrg.Visible = false;
        try
        {
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void rdBtnLstPower_SelectedIndexChanged(object sender, EventArgs e)
    {
        divPower.Visible = false;
        txtContractDemand.Text = string.Empty;
        string strValuue = rdBtnLstPower.SelectedValue;
        if (strValuue == "1")
        {
            divPower.Visible = true;
        }
    }

    /// <summary>
    /// Click event for btnSaveAsDraft
    /// Common function for apply and save as draft button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //    Button objButton = (Button)sender;
    //    string strCommandArg = objButton.CommandArgument;
    //    Incentive_PCMaster objMaster = new Incentive_PCMaster();
    //    objMaster.intApplyFlag = 1;
    //    objMaster.strActionCode = "ud";
    //    #region set properties
    //    objMaster.intAppFor = Convert.ToInt32(drpApplicationType.SelectedValue);
    //    objMaster.strChngIn = GetCheckBoxListValues(chkLstChange);
    //    objMaster.strEINEMIIPMTNo = txtEin.Text.Trim();
    //    objMaster.strUAN = txtUan.Text.Trim();
    //    objMaster.strCompName = txtEnterpriseName.Text.Trim();
    //    objMaster.intUnitCat = Convert.ToInt32(drpUnitCategory.SelectedValue);
    //    objMaster.intUnitType = Convert.ToInt32(drpCompanyType.SelectedValue);
    //    objMaster.intOrgType = Convert.ToInt32(drpOrganizationType.SelectedValue);
    //    objMaster.strOwnerName = txtOwnerName.Text.Trim();
    //    objMaster.intSalutation = Convert.ToInt16(drpSalutation.SelectedValue);
    //    objMaster.intOwnerCode = Convert.ToInt32(drpOwnerType.SelectedValue);
    //    objMaster.strAddr = txtEnterpriseAddress.Text.Trim();
    //    objMaster.strPhNo = txtPhoneNo.Text.Trim();
    //    objMaster.strFaxNo = txtFax.Text.Trim();
    //    objMaster.strEmail = txtEmail.Text.Trim();
    //    objMaster.strWebsite = txtWebsite.Text.Trim();
    //    objMaster.strOffcAddr = txtOfficeAddress.Text.Trim();
    //    objMaster.strOffcEmail = txtOfficeEmail.Text.Trim();
    //    objMaster.strOffcFaxNo = txtOfficeFax.Text.Trim();
    //    objMaster.strOffcPhNo = txtOfficePhone.Text.Trim();
    //    objMaster.strOffcWebsite = txtOfficeWebsite.Text.Trim();
    //    objMaster.strOfficeMobCode = ddlCode.SelectedValue;
    //    objMaster.strOfficeFaxCode = drpFx.SelectedValue;
    //    objMaster.strEntFaxCode = drpEnterpriseFax.SelectedValue;
    //    objMaster.strEntMobCode = drpEntCode.SelectedValue;

    //    objMaster.dtmFFCI = txtDateFFI.Value;
    //    objMaster.strInvestIn = GetCheckBoxListValues(chkInvestIn);
    //    objMaster.strInvestMode = txtModeOfInvestment.Text.Trim();

    //    if (!string.IsNullOrEmpty(txtWorkingCapital.Text.Trim()))
    //    {
    //        objMaster.decWorkingCapital = Convert.ToDecimal(txtWorkingCapital.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtEquity.Text.Trim()))
    //    {
    //        objMaster.decEquity = Convert.ToDecimal(txtEquity.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtLoan.Text.Trim()))
    //    {
    //        objMaster.decLoan = Convert.ToDecimal(txtLoan.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtFdiComponent.Text.Trim()))
    //    {
    //        objMaster.decFdiComp = Convert.ToDecimal(txtFdiComponent.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtGeneral.Text.Trim()))
    //    {
    //        objMaster.intGeneral = Convert.ToInt32(txtGeneral.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtWomen.Text.Trim()))
    //    {
    //        objMaster.intWomen = Convert.ToInt32(txtWomen.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtManagarial.Text.Trim()))
    //    {
    //        objMaster.intManaregailSkill = Convert.ToInt32(txtManagarial.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtSupervisor.Text.Trim()))
    //    {
    //        objMaster.intSupervisor = Convert.ToInt32(txtSupervisor.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtSkilled.Text.Trim()))
    //    {
    //        objMaster.intSkilled = Convert.ToInt32(txtSkilled.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtUnSKilled.Text.Trim()))
    //    {
    //        objMaster.intUnskilled = Convert.ToInt32(txtUnSKilled.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtSemiSkilled.Text.Trim()))
    //    {
    //        objMaster.intSemiSkilled = Convert.ToInt32(txtSemiSkilled.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtTotalSc.Text.Trim()))
    //    {
    //        objMaster.intScTotal = Convert.ToInt32(txtTotalSc.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtTotalSt.Text.Trim()))
    //    {
    //        objMaster.intStTotal = Convert.ToInt32(txtTotalSt.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtPhd.Text.Trim()))
    //    {
    //        objMaster.intDisabled = Convert.ToInt32(txtPhd.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtDirectEmployement.Text.Trim()))
    //    {
    //        objMaster.intTotalEmployee = Convert.ToInt32(txtDirectEmployement.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtContractualEmp.Text.Trim()))
    //    {
    //        objMaster.intContractual = Convert.ToInt32(txtContractualEmp.Text.Trim());
    //    }
    //    objMaster.strProductCode = txtProductCode.Text.Trim();
    //    objMaster.strProductName = txtName.Text.Trim();
    //    objMaster.intIsPwrReq = Convert.ToInt32(rdBtnLstPower.SelectedValue);
    //    objMaster.intSectorId = Convert.ToInt32(ddlSector.SelectedValue);
    //    objMaster.intSubSectorId = Convert.ToInt32(ddlSubSector.SelectedValue);
    //    objMaster.intBlock = Convert.ToInt32(ddlBlock.SelectedValue);
    //    objMaster.intDistrict = Convert.ToInt32(ddlDistrict.SelectedValue);
    //    if (!string.IsNullOrEmpty(txtland.Text.Trim()))
    //    {
    //        objMaster.decLandInvestment = Convert.ToDecimal(txtland.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtBuilding.Text.Trim()))
    //    {
    //        objMaster.decBuilding = Convert.ToDecimal(txtBuilding.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtPlantMachinery.Text.Trim()))
    //    {
    //        objMaster.decPlant = Convert.ToDecimal(txtPlantMachinery.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtOthers.Text.Trim()))
    //    {
    //        objMaster.decOthers = Convert.ToDecimal(txtOthers.Text.Trim());
    //    }
    //    if (!string.IsNullOrEmpty(txtContractDemand.Text.Trim()))
    //    {
    //        objMaster.decContractDemand = Convert.ToDecimal(txtContractDemand.Text.Trim());
    //    }
    //    objMaster.dtmProdComm = txtProdComm.Value;
    //    objMaster.strDateConnection = txtPowerConnection.Value;
    //    objMaster.strUnitOthersk = txtOtherOrg.Text.Trim();
    //    DataTable dtProducts = CreateProductTable();
    //    if (grdProducts.Rows.Count > 0)
    //    {
    //        dtProducts.TableName = "Products";
    //        for (int cnt = 0; cnt < grdProducts.Rows.Count; cnt++)
    //        {
    //            GridViewRow GRow = grdProducts.Rows[cnt];
    //            HiddenField hdnUnit = (HiddenField)GRow.FindControl("hdnUnit");
    //            DataRow dRow = dtProducts.NewRow();
    //            dRow["item"] = GRow.Cells[1].Text;
    //            dRow["Code"] = GRow.Cells[2].Text;
    //            dRow["Qty"] = GRow.Cells[3].Text;
    //            dRow["UnitOthers"] = txtUnitType.Text;
    //            dRow["Unit"] = hdnUnit.Value;
    //            if (string.IsNullOrEmpty(GRow.Cells[5].Text) || GRow.Cells[5].Text == "&nbsp;")
    //            {
    //                dRow["Cost"] = "0.00";
    //            }
    //            else
    //            {
    //                dRow["Cost"] = GRow.Cells[5].Text;
    //            }
    //            dtProducts.Rows.Add(dRow);
    //        }
    //    }
    //    if (dtProducts.Rows.Count > 0)
    //    {
    //        CommonFunctions obj = new CommonFunctions();
    //        objMaster.strXml = obj.GetSTRXMLResult(dtProducts);
    //    }
    //    objMaster.intAppNo = Convert.ToInt32(Request.QueryString["id"]);
    //    objMaster.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
    //    objMaster.strIndustryCode = lblIndustryCode.Text;
    //    objMaster.strOthersOrg = txtOtherOrg.Text.Trim();

    //    #endregion
    //    int intRetValue = 0;
    //    IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
    //    intRetValue = objBuisnessLayer.Incentive_PcDetails_AED(objMaster);

    //    if (intRetValue == 2)
    //    {
    //        StringBuilder sbUrl = new StringBuilder();
    //        sbUrl.AppendFormat("<script>alert('{0}');", Messages.ShowMessage(intRetValue.ToString()));
    //        sbUrl.AppendFormat("window.location.href='../Incentive/ViewIncentiveApplication.aspx'");
    //        sbUrl.Append("</script>");
    //        Response.Write(sbUrl.ToString());
    //    }
    //    else
    //    {
    //        ClientScript.RegisterStartupScript(this.GetType(), "alert", string.Format("<script>alert('{0}')</script>", Messages.ShowMessage(intRetValue.ToString())));
    //    }
    //}

    protected void lnkAdd_ClicK(object sender, EventArgs e)
    {
        DataTable dtProducts = null;
        DataRow dRow = null;
        if (ViewState["Products"] != null)
        {
            dtProducts = (DataTable)ViewState["Products"];
        }
        else
        {
            dtProducts = CreateProductTable();
        }
        dRow = dtProducts.NewRow();
        dRow["item"] = txtItemProduct.Text;
        dRow["Code"] = txtItemCode.Text;
        dRow["Qty"] = txtQuantity.Text;
        dRow["Unit"] = drpUnitType.SelectedItem.Text;
        dRow["UnitId"] = drpUnitType.SelectedValue;
        dRow["Cost"] = txtCost.Text;
        dtProducts.Rows.Add(dRow);
        ViewState["Products"] = dtProducts;
        BindProductGridview(dtProducts);
        txtItemProduct.Text = string.Empty;
        txtItemCode.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        drpUnitType.SelectedIndex = 0;
        txtCost.Text = string.Empty;
    }

    protected void btnCancel_click(object sender, EventArgs e)
    {
        StringBuilder sbUrl = new StringBuilder();
        sbUrl.Append("<script>");
        sbUrl.AppendFormat("window.location.href='ViewIncentiveApplication.aspx?id={0}&pIndex={1}&pSize={2}&linkn={3}&linkm={4}&btn={5}&tab={6}'", Request.QueryString["id"], Request.QueryString["pIndex"], Request.QueryString["pSize"], Request.QueryString["linkn"], Request.QueryString["linkm"], Request.QueryString["btn"], Request.QueryString["tab"]);
        sbUrl.Append("</script>");
        Response.Write(sbUrl.ToString());
    }
    #endregion

    #region gridview events
    /// <summary>
    /// Rowcommand event for product gridview
    /// It will add new products and delete old products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdProducts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int intRowIndex = Convert.ToInt32(e.CommandArgument);

        if (string.Equals(commandName, "d", StringComparison.OrdinalIgnoreCase))
        {
            int intId = Convert.ToInt32(grdProducts.DataKeys[intRowIndex].Value);
            DataTable dtView = (DataTable)ViewState["Products"];
            for (int cnt = 0; cnt < dtView.Rows.Count; cnt++)
            {
                if (dtView.Rows[cnt][0].ToString() == intId.ToString())
                {
                    dtView.Rows[cnt].Delete();
                    dtView.AcceptChanges();
                }
            }
            BindProductGridview(dtView);
            ViewState["Products"] = dtView;

        }
    }

    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubSector(ddlSector.SelectedValue);
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlock(ddlDistrict.SelectedValue);
    }


    #endregion

    #region function to get value while edit
    /// <summary>
    /// Function to get all the details based on the application details called when editing
    /// </summary>
    //private void GetApplicationDetails()
    //{

    //    PcSearch objSearch = new PcSearch()
    //    {
    //        intAppFor = Convert.ToInt32(Session["investorid"]),
    //        strActionCode = "inv",
    //        intPageIndex = 0,
    //        intPageSize = 0,
    //        strFromDate = string.Empty,
    //        strToDate = string.Empty,
    //    };
    //    if (Request.QueryString["id"] != null)
    //    {
    //        objSearch.intAppFor = Convert.ToInt32(Request.QueryString["id"]);
    //        objSearch.strActionCode = "e";
    //    }
    //    DataSet objDs = new DataSet();
    //    IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
    //    objDs = objBuisnessLayer.Incentive_PcForm_View(objSearch);
    //    if (objDs != null && objDs.Tables.Count > 0)
    //    {
    //        DataTable dtPcDetails = new DataTable();
    //        dtPcDetails = objDs.Tables[0];
    //        if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
    //        {
    //            DataRow objRow = dtPcDetails.Rows[0];
    //            drpApplicationType.SelectedValue = objRow["vchAppFor"].ToString();
    //            int intApplicationType = Convert.ToInt32(drpApplicationType.SelectedValue);
    //            if (intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod)
    //            {
    //                rdBtnApplicationFor.Items.Clear();
    //                rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
    //                rdBtnApplicationFor.Items[0].Selected = true;
    //                objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
    //                {
    //                    Action = "amd",
    //                    Name = string.Empty,
    //                    ID = string.Empty,
    //                    Param = 0,
    //                    Param_1 = 0,
    //                    Param_2 = string.Empty,
    //                    Param_3 = string.Empty
    //                });
    //                divChangeIn.Visible = true;
    //            }
    //            else if (intApplicationType == (int)enAppFor.New || intApplicationType == (int)enAppFor.New_EMD || intApplicationType == (int)enAppFor.Rehabilitated_New || intApplicationType == (int)enAppFor.Transferred_new || intApplicationType == (int)enAppFor.Migrated_new)
    //            {
    //                rdBtnApplicationFor.Items.Clear();
    //                rdBtnApplicationFor.Items.Add(new ListItem("New", "1"));
    //                rdBtnApplicationFor.Items[0].Selected = true;
    //                //objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
    //                //{
    //                //    Action = "new",
    //                //    Name = string.Empty,
    //                //    ID = string.Empty,
    //                //    Param = 0,
    //                //    Param_1 = 0,
    //                //    Param_2 = string.Empty,
    //                //    Param_3 = string.Empty
    //                //});
    //                divChangeIn.Visible = true;
    //            }
    //            divChangeIn.Visible = true;
    //            string strChangeIn = objRow["vchChngIn"].ToString();
    //            UpdateCheckBoxList(strChangeIn, chkLstChange);

    //            if (Request.QueryString["id"] != null)
    //            {
    //                lblApplicationNo.Text = objRow["vchAppNo"].ToString();
    //            }

    //            lblIndustryCode.Text = objRow["vchIndustryCode"].ToString();
    //            txtEin.Text = objRow["vchEINEMIIPMTNo"].ToString();
    //            txtEin.Enabled = false;
    //            txtUan.Enabled = false;
    //            txtUan.Text = objRow["vchUAN"].ToString();
    //            txtEnterpriseName.Text = objRow["vchCompName"].ToString();
    //            drpCompanyType.SelectedValue = objRow["intUnitType"].ToString();
    //            drpOrganizationType.SelectedValue = objRow["intOrgType"].ToString();
    //            txtOtherOrg.Text = objRow["vchOrgOther"].ToString();
    //            drpOwnerType.SelectedValue = objRow["intOwnerCode"].ToString();
    //            drpUnitCategory.SelectedValue = objRow["intUnitCat"].ToString();
    //            txtOwnerName.Text = objRow["vchOwnerName"].ToString();
    //            ddlSector.SelectedValue = objRow["intSectorId"].ToString();
    //            BindSubSector(ddlSector.SelectedValue);
    //            ddlSubSector.SelectedValue = objRow["intSubSectorId"].ToString();

    //            txtOfficeAddress.Text = objRow["vchOffcAddr"].ToString();
    //            txtOfficeEmail.Text = objRow["vchOffcEmail"].ToString();
    //            txtOfficeFax.Text = objRow["vchOffcFaxNo"].ToString();
    //            txtOfficePhone.Text = objRow["vchOffcPhNo"].ToString();
    //            txtOfficeWebsite.Text = objRow["vchOffcWebsite"].ToString();
    //            ddlCode.SelectedValue = objRow["vchOfficeMobCode"].ToString();
    //            drpEntCode.SelectedValue = objRow["vchEntMobCode"].ToString();
    //            drpFx.SelectedValue = objRow["vchOfficeFaxCode"].ToString();
    //            drpEnterpriseFax.SelectedValue = objRow["vchEntFaxCode"].ToString();
    //            txtEnterpriseAddress.Text = objRow["vchAddr"].ToString();
    //            txtPhoneNo.Text = objRow["vchPhNo"].ToString();
    //            txtFax.Text = objRow["vchFaxNo"].ToString();
    //            txtEmail.Text = objRow["vchEmail"].ToString();
    //            txtWebsite.Text = objRow["vchWebsite"].ToString();
    //            ddlDistrict.SelectedValue = objRow["intDistrict"].ToString();
    //            BindBlock(ddlDistrict.SelectedValue);
    //            ddlBlock.SelectedValue = objRow["intBlock"].ToString();
    //            drpSalutation.SelectedValue = objRow["intsalutation"].ToString();
    //            string strInvestIn = objRow["vchInvestIn"].ToString();
    //            UpdateCheckBoxList(strInvestIn, chkInvestIn);

    //            txtDateFFI.Value = objRow["dtmFFCI"].ToString();
    //            txtModeOfInvestment.Text = objRow["vchInvestMode"].ToString();
    //            txtWorkingCapital.Text = objRow["decWorkingCapital"].ToString();
    //            txtEquity.Text = objRow["decEquity"].ToString();
    //            txtLoan.Text = objRow["decLoan"].ToString();
    //            txtFdiComponent.Text = objRow["decFdi"].ToString();
    //            txtland.Text = objRow["decLandInvestment"].ToString();
    //            txtBuilding.Text = objRow["decBuilding"].ToString();
    //            txtPlantMachinery.Text = objRow["decPlant"].ToString();
    //            txtOthers.Text = objRow["decOthers"].ToString();

    //            txtProductCode.Text = objRow["vchProductCode"].ToString();
    //            txtName.Text = objRow["vchProductName"].ToString();
    //            txtContractDemand.Text = objRow["decContractDemand"].ToString();
    //            txtPowerConnection.Value = objRow["dtmPowerConn"].ToString();
    //            txtProdComm.Value = objRow["dtmProdComm"].ToString();
    //            rdBtnLstPower.SelectedValue = objRow["intPwrReq"].ToString();
    //            string strValuue = rdBtnLstPower.SelectedValue;
    //            if (strValuue == "1")
    //            {
    //                divPower.Visible = true;
    //            }
    //            if (rdBtnLstPower.SelectedValue == "1")
    //            {
    //                divPower.Visible = true;
    //            }
    //        }
    //        dtPcDetails = objDs.Tables[1];
    //        if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
    //        {
    //            DataRow objRow = dtPcDetails.Rows[0];
    //            txtSupervisor.Text = objRow["intSupervisor"].ToString();
    //            txtManagarial.Text = objRow["intManaregailSkill"].ToString();
    //            txtSkilled.Text = objRow["intSkilled"].ToString();
    //            txtSemiSkilled.Text = objRow["intSemiSkilled"].ToString();
    //            txtUnSKilled.Text = objRow["intUnSkilled"].ToString();
    //            txtTotalSc.Text = objRow["intScTotal"].ToString();
    //            txtTotalSt.Text = objRow["intStTotal"].ToString();
    //            txtWomen.Text = objRow["intWomen"].ToString();
    //            txtPhd.Text = objRow["intDisabled"].ToString();
    //            txtGeneral.Text = objRow["intGeneral"].ToString();
    //            txtDirectEmployement.Text = objRow["intDirectEmp"].ToString();
    //            txtContractualEmp.Text = objRow["intContractual"].ToString();

    //        }

    //        dtPcDetails = objDs.Tables[2];
    //        if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
    //        {
    //            BindProductGridview(dtPcDetails);
    //            ViewState["Products"] = dtPcDetails;
    //        }
    //    }
    //}


    private void GetApplicationDetails()
    {
        try
        {
            PcSearch objSearch = new PcSearch()
            {
                intAppFor = Convert.ToInt32(Session["investorid"]),
                strActionCode = "inv",
                intPageIndex = 0,
                intPageSize = 0,
                strFromDate = string.Empty,
                strToDate = string.Empty,
            };
            if (Request.QueryString["id"] != null)
            {
                objSearch.intAppFor = Convert.ToInt32(Request.QueryString["id"]);
                objSearch.strActionCode = "e";
            }
            DataSet objDs = new DataSet();
            IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
            objDs = objBuisnessLayer.Incentive_PcForm_View(objSearch);
            if (objDs != null && objDs.Tables.Count > 0)
            {
                DataTable dtPcDetails = new DataTable();
                if (objDs.Tables[3].Rows.Count > 0)
                {
                    dtPcDetails = objDs.Tables[3];
                    if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                    {
                        gvPlant.DataSource = dtPcDetails;
                        gvPlant.DataBind();

                    }
                }
                dtPcDetails = objDs.Tables[0];
                if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                {

                    DataRow objRow = dtPcDetails.Rows[0];
                    GetIrFormDetails(Convert.ToInt32(objRow["vchAppNo"].ToString()));
                    int intApplicationType = Convert.ToInt32(objRow["vchAppFor"].ToString());
                    if (intApplicationType == (int)enAppFor.New || intApplicationType == (int)enAppFor.Rehabilitated_New || intApplicationType == (int)enAppFor.Transferred_new || intApplicationType == (int)enAppFor.Migrated_new)
                    {
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
                        drpApplicationType.SelectedValue = intApplicationType.ToString();
                    }
                    else if (intApplicationType == (int)enAppFor.New_EMD)
                    {
                        rdBtnApplicationFor.Items.Clear();
                        rdBtnApplicationFor.Items.Add(new ListItem("New", "1"));
                        rdBtnApplicationFor.Items[0].Selected = true;
                        drpApplicationType.Items.Clear();
                        drpApplicationType.Items.Add(new ListItem("New Unit E/M/D", ((int)enAppFor.New_EMD).ToString()));
                        divChangeIn.Visible = true;
                        UpdateCheckBoxList(objRow["vchChngIn"].ToString(), chkLstChange);
                    }
                    else if (intApplicationType == (int)enAppFor.exist)
                    {

                        rdBtnApplicationFor.Items.Clear();
                        rdBtnApplicationFor.Items.Add(new ListItem("existing", "2"));
                        rdBtnApplicationFor.Items[0].Selected = true;
                        drpApplicationType.Items.Clear();
                        drpApplicationType.Items.Add(new ListItem("Existing", ((int)enAppFor.exist).ToString()));
                        drpApplicationType.SelectedIndex = 0;

                    }
                    else if (intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod)
                    {
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
                        drpApplicationType.SelectedValue = intApplicationType.ToString();
                        divChangeIn.Visible = true;
                        UpdateCheckBoxList(objRow["vchChngIn"].ToString(), chkLstChange);
                    }

                    lblApplicationNo.Text = objRow["vchAppFormattedNo"].ToString();

                    lblIndustryCode.Text = objRow["vchIndustryCode"].ToString() == "" ? "NA" : objRow["vchIndustryCode"].ToString();
                    txtEin.Text = objRow["vchEINEMIIPMTNo"].ToString();
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
                    drpChangeIn.SelectedValue = objRow["intChangeIn"].ToString();
                    txtModeOfInvestment.Text = objRow["vchInvestMode"].ToString();
                    txtModeOfInvestment.Enabled = false;
                    txtDateFFI.Value = objRow["dtmFFCI"].ToString();
                    if (txtDateFFI.Value == "01-Jan-1900")
                        txtDateFFI.Value = "";
                    txtWorkingCapital.Text = objRow["decWorkingCapital"].ToString();
                    txtEquity.Text = objRow["decEquity"].ToString();
                    txtLoan.Text = objRow["decLoan"].ToString();
                    txtFdiComponent.Text = objRow["decFdi"].ToString();
                    txtland.Text = objRow["decLandInvestment"].ToString();
                    txtBuilding.Text = objRow["decBuilding"].ToString();
                    txtPlantMachinery.Text = objRow["decPlant"].ToString();
                    txtOthers.Text = objRow["decOthers"].ToString();
                    lblTotal.Text = Convert.ToString(Convert.ToInt32(objRow["decLandInvestment"] == "" || objRow["decLandInvestment"] == null ? 0 : objRow["decLandInvestment"]) + Convert.ToInt32(objRow["decBuilding"] == "" || objRow["decBuilding"] == null ? 0 : objRow["decBuilding"]) + Convert.ToInt32(objRow["decPlant"] == "" || objRow["decPlant"] == null ? 0 : objRow["decPlant"]) + Convert.ToInt32(objRow["decOthers"] == "" || objRow["decOthers"] == null ? 0 : objRow["decOthers"]));
                    txtProductCode.Text = objRow["vchProductCode"].ToString();
                    txtName.Text = objRow["vchProductName"].ToString();
                    txtContractDemand.Text = objRow["decContractDemand"].ToString();
                    txtPowerConnection.Value = objRow["dtmPowerConn"].ToString();
                    if (txtPowerConnection.Value == "01-Jan-1900")
                        txtPowerConnection.Value = "";
                    txtProdComm.Value = objRow["dtmProdComm"].ToString();
                    if (txtProdComm.Value == "01-Jan-1900")
                        txtProdComm.Value = "";

                    rdBtnLstPower.SelectedValue = objRow["intPwrReq"].ToString();
                    string strValuue = rdBtnLstPower.SelectedValue;

                    if (strValuue == "1")
                    {
                        divPower.Visible = true;
                    }
                    if (rdBtnLstPower.SelectedValue == "1")
                    {
                        divPower.Visible = true;
                    }
                }


                dtPcDetails = objDs.Tables[1];
                if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                {
                    DataRow objRow = dtPcDetails.Rows[0];
                    txtSupervisor.Text = objRow["intSupervisor"].ToString();
                    txtManagarial.Text = objRow["intManaregailSkill"].ToString();
                    txtSkilled.Text = objRow["intSkilled"].ToString();
                    txtSemiSkilled.Text = objRow["intSemiSkilled"].ToString();
                    txtUnSKilled.Text = objRow["intUnSkilled"].ToString();
                    txtTotalSc.Text = objRow["intScTotal"].ToString();
                    txtTotalSt.Text = objRow["intStTotal"].ToString();
                    txtWomen.Text = objRow["intWomen"].ToString();
                    txtPhd.Text = objRow["intDisabled"].ToString();
                    txtGeneral.Text = objRow["intGeneral"].ToString();
                    txtDirectEmployement.Text = objRow["intDirectEmp"].ToString();
                    txtContractualEmp.Text = objRow["intContractual"].ToString();
                }

                dtPcDetails = objDs.Tables[2];
                if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
                {
                    BindProductGridview(dtPcDetails);
                    ViewState["Products"] = dtPcDetails;

                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #region Common functions for page
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
    /// <summary>
    /// Common function to get all the datatable values from database to bind to gridview
    /// </summary>
    private void BindDropDown()
    {
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        DataSet objDa = objBuisnessLayer.BindDropdown("ddl");
        if (objDa != null)
        {
            FillDropDown(objDa.Tables[0], "Application for", drpApplicationType);
            FillCheckBoxList(objDa.Tables[1], chkLstChange);
            FillDropDown(objDa.Tables[2], "Organization Type", drpOrganizationType);
            FillDropDown(objDa.Tables[3], "Owner Type", drpOwnerType);
            FillDropDown(objDa.Tables[4], "Unit Category", drpUnitCategory);
            FillDropDown(objDa.Tables[5], "Company Type", drpCompanyType);
            FillDropDown(objDa.Tables[6], "Change In", drpChangeIn);
            FillDropDown(objDa.Tables[10], "Unit Type", drpUnitType);
            FillDropDown(objDa.Tables[11], "District", ddlDistrict);
            FillDropDown(objDa.Tables[12], "Sector", ddlSector);
            FillDropDown(objDa.Tables[13], "", ddlCode);
            FillDropDown(objDa.Tables[13], "", drpFx);
            FillDropDown(objDa.Tables[13], "", drpEntCode);
            FillDropDown(objDa.Tables[13], "", drpEnterpriseFax);
            FillDropDown(objDa.Tables[14], "", drpSalutation);

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
        int cnter = 0;
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
                        cnter = cnter + 1;
                    }
                }
            }
        }
        if (cnter > 0)
        {
            divChangeIn.Visible = true;
        }
        else
        {
            divChangeIn.Visible = false;
        }
    }
    #endregion

    #region product table code
    private DataTable CreateProductTable()
    {
        DataTable dtProduct = new DataTable();
        DataColumn dcId = new DataColumn() { ColumnName = "id", AutoIncrement = true, AutoIncrementSeed = 1, AutoIncrementStep = 1 };
        dtProduct.Columns.Add(dcId);
        dtProduct.Columns.Add(new DataColumn("item"));
        dtProduct.Columns.Add(new DataColumn("Code"));
        dtProduct.Columns.Add(new DataColumn("Qty"));
        dtProduct.Columns.Add(new DataColumn("Unit"));
        dtProduct.Columns.Add(new DataColumn("UnitOthers"));
        dtProduct.Columns.Add(new DataColumn("Cost"));
        dtProduct.Columns.Add(new DataColumn("unitId"));
        return dtProduct;
    }

    /// <summary>
    /// Function to bind the product gridview from the datatable in view state
    /// if not existing then create a dummy row in datatable bind it to gridview
    /// </summary>
    private void BindProduct()
    {
        DataTable dtProducts = CreateProductTable();
        dtProducts.TableName = "Products";
        if ((ViewState["Products"] != null))
        {
            dtProducts = (DataTable)ViewState["Products"];
        }
        BindProductGridview(dtProducts);
    }
    /// <summary>
    /// Common function to just bind the data in datatable to gridview and hide and show the add and delete button
    /// </summary>
    /// <param name="dtView"></param>
    private void BindProductGridview(DataTable dtView)
    {
        grdProducts.DataSource = dtView;
        grdProducts.DataBind();
    }

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
    #endregion

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
            //  divChangeIn.Visible = true;
        }
        //if (intAppType == 2)
        //{
        //    //added by suman on 5th Oct,2017 to enable entry for original fields for data coming from investor not  in peal
        //    if (hdnPeal.Value == "1")
        //    {
        //        divOldContractual.Visible = true;
        //        divOldDirect.Visible = true;
        //        divOldFfci.Visible = true;
        //        //  divOldPC.Visible = true;
        //        divOldProd.Visible = true;
        //        divOldProducts.Visible = true;
        //        for (int cnt = 0; cnt < tblLandPlant.Rows.Count; cnt++)
        //        {
        //            tblLandPlant.Rows[cnt].Cells[2].Visible = true;
        //        }
        //        for (int cnt = 0; cnt < tblEmployement.Rows.Count; cnt++)
        //        {
        //            tblEmployement.Rows[cnt].Cells[1].Visible = true;
        //            tblEmployement.Rows[cnt].Cells[4].Visible = true;
        //        }
        //    }
        //    else
        //    {
        //        divOldContractual.Visible = false;
        //        divOldDirect.Visible = false;
        //        divOldFfci.Visible = false;
        //        divOldPC.Visible = false;
        //        divOldProd.Visible = false;
        //        divOldProducts.Visible = false;
        //    }
        //}

    }

    //private void EnableDisablePanelByCheckBox()
    //{
    //    int intApplicationType = Convert.ToInt32(drpApplicationType.SelectedValue);
    //    if (intApplicationType == (int)enAppFor.New_EMD || intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod)
    //    {
    //        pnlAdditional.Enabled = false;
    //        pnlIndOthers.Enabled = false;
    //        pnlIndustry.Enabled = false;
    //        pnlInvestment.Enabled = false;
    //        pnlLocation.Enabled = false;
    //        pnlname.Enabled = false;
    //        pnlProduction.Enabled = false;
    //        pnlOrgType.Enabled = false;
    //        pnlRedOff.Enabled = false;
    //        divChangeIn.Enabled = true;
    //        pnlApp.Enabled = false;
    //        for (int cnt = 0; cnt < chkLstChange.Items.Count; cnt++)
    //        {
    //            ListItem curr = chkLstChange.Items[cnt];
    //            int id = Convert.ToInt32(curr.Value);
    //            if (curr.Selected)
    //            {
    //                switch (id)
    //                {
    //                    case (int)enAmdType.Name:
    //                        pnlname.Enabled = true;
    //                        break;
    //                    case (int)enAmdType.Org:
    //                        pnlOrgType.Enabled = true;
    //                        break;
    //                    case (int)enAmdType.RegdOff:
    //                        pnlRedOff.Enabled = true;
    //                        break;
    //                    case (int)enAmdType.Location:
    //                        pnlLocation.Enabled = true;
    //                        break;
    //                    case (int)enAmdType.EMD:
    //                        pnlIndOthers.Enabled = true;
    //                        pnlOrgType.Enabled = true;
    //                        pnlRedOff.Enabled = true;
    //                        pnlLocation.Enabled = true;
    //                        pnlProduction.Enabled = true;
    //                        pnlInvestment.Enabled = true;
    //                        pnlAdditional.Enabled = true;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                string strChangeIn = hdnChangeIn.Value;
    //                if (string.IsNullOrEmpty(hdnChangeIn.Value))
    //                {
    //                    hdnChangeIn.Value = string.Format("{0}", id.ToString());
    //                }
    //                else
    //                {
    //                    hdnChangeIn.Value = string.Format("{0},{1}", hdnChangeIn.Value, id.ToString());
    //                }
    //            }
    //        }
    //    }
    //}
    private void GetNewAppCode()
    {
        PcSearch objSearch = new PcSearch()
        {
            intAppFor = 0,
            strActionCode = "new",
            intPageIndex = 0,
            intPageSize = 0,
            strFromDate = string.Empty,
            strToDate = string.Empty
        };
        DataSet objDs = new DataSet();
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        objDs = objBuisnessLayer.Incentive_PcForm_View(objSearch);
        if (objDs != null)
        {
            DataTable dt = objDs.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                //lblAppNo.Text = dt.Rows[0]["appno"].ToString();
            }
        }
    }

    private void GetPealDetails()
    {
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
            if (objDs != null)
            {
                DataTable dt = objDs.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dRow = dt.Rows[0];
                    GetNewAppCode();
                    lblIndustryCode.Text = dRow["vchIndustryCode"].ToString() == "" ? "NA" : dRow["vchIndustryCode"].ToString();
                    int intApplicationType = dRow["intapplicationfor"] == null || dRow["intapplicationfor"] == "" ? 0 : Convert.ToInt32(dRow["intapplicationfor"].ToString());
                    if (intApplicationType == 0)
                    {
                        rdBtnApplicationFor.Items.Clear();
                        rdBtnApplicationFor.Items.Add(new ListItem("New", "1"));
                        rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
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
                    else if (intApplicationType == (int)enAppFor.exist)
                    {
                        rdBtnApplicationFor.Items.Clear();
                        rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
                        rdBtnApplicationFor.SelectedValue = "2";
                        drpApplicationType.Items.Clear();
                        drpApplicationType.Items.Add(new ListItem("Existing", ((int)enAppFor.exist).ToString()));
                        drpApplicationType.SelectedIndex = 0;
                        divChangeIn.Visible = false;
                    }
                    else if (intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod)
                    {
                        rdBtnApplicationFor.Items.Clear();
                        rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
                        rdBtnApplicationFor.SelectedValue = "2";
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
                    else if (intApplicationType == (int)enAppFor.New)
                    {
                        rdBtnApplicationFor.Items.Clear();
                        rdBtnApplicationFor.Items.Add(new ListItem("New", "1"));
                        rdBtnApplicationFor.SelectedValue = "1";
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
                    int intEin = Convert.ToInt32(dRow["intEinNo"].ToString());
                    if (intEin >= 1 && intEin < 4)
                    {
                        txtEin.Text = dRow["vchEINnIEMnIL"].ToString();
                    }
                    else if (intEin == 4)
                    {
                        txtUan.Text = dRow["vchEINnIEMnIL"].ToString();
                    }
                    ddlSector.SelectedValue = dRow["intSectorId"].ToString();
                    BindSubSector(ddlSector.SelectedValue);
                    ddlSubSector.SelectedValue = dRow["intSubSectorId"].ToString();
                    txtOfficeAddress.Text = dRow["vchCorAdd"].ToString();
                    txtOfficeEmail.Text = dRow["vchCorEmail"].ToString();
                    txtOfficeFax.Text = dRow["vchCorFaxNo"].ToString();
                    txtOfficePhone.Text = dRow["vchCorMobileNo"].ToString();
                    txtEnterpriseName.Text = dRow["vchNameOfUnit"].ToString();
                    drpOrganizationType.SelectedValue = dRow["organizationType"].ToString();
                    txtland.Text = dRow["decLandIncLandDev"].ToString();
                    txtBuilding.Text = dRow["decBuildingndConstruction"].ToString();
                    txtPlantMachinery.Text = dRow["decPlantndMachinery"].ToString();
                    txtOthers.Text = dRow["decOthers"].ToString();
                    lblTotal.Text = Convert.ToString(Convert.ToInt32(dRow["decLandInvestment"] == "" || dRow["decLandInvestment"] == null ? 0 : dRow["decLandInvestment"]) + Convert.ToInt32(dRow["decBuilding"] == "" || dRow["decBuilding"] == null ? 0 : dRow["decBuilding"]) + Convert.ToInt32(dRow["decPlant"] == "" || dRow["decPlant"] == null ? 0 : dRow["decPlant"]) + Convert.ToInt32(dRow["decOthers"] == "" || dRow["decOthers"] == null ? 0 : dRow["decOthers"]));
                    txtSupervisor.Text = dRow["intSupervisorProp"].ToString();
                    txtManagarial.Text = dRow["intManagerProp"].ToString();
                    txtSkilled.Text = dRow["intSkilledProp"].ToString();
                    txtSemiSkilled.Text = dRow["intSemiSkilledProp"].ToString();
                    txtUnSKilled.Text = dRow["intUnSkilledProp"].ToString();
                    ddlDistrict.SelectedValue = dRow["intExisDistrict"].ToString();
                    BindBlock(ddlDistrict.SelectedValue);
                    ddlBlock.SelectedValue = dRow["intExisBlock"].ToString();
                    string projectType = dRow["projecttypename"].ToString();
                    txtEquity.Text = dRow["decEquityContribution"].ToString();
                    txtLoan.Text = dRow["decBankndInstitutionalFin"].ToString();
                    txtFdiComponent.Text = dRow["decForeignInvestment"].ToString();
                    txtDirectEmployement.Text = dRow["intPropDirectEmployment"].ToString();
                    txtContractualEmp.Text = dRow["intPropContractualEmployment"].ToString();
                    // txtDateFFI.Value = dRow["dtmFFCI"].ToString();
                    ddlCode.SelectedValue = dRow["vchMOBN"].ToString();
                    drpFx.SelectedValue = dRow["vchFaxCorDet"].ToString();
                    txtOwnerName.Text = dRow["ownername"].ToString();
                    if (Session["comptype"] != null)
                        drpCompanyType.SelectedValue = Session["comptype"].ToString();
                    else
                        drpCompanyType.SelectedValue = "0";

                    if (Session["unitCat"] != null)
                    {
                        string strUnitType = Session["unitCat"].ToString();
                        drpUnitCategory.SelectedValue = drpUnitCategory.Items.FindByText(strUnitType).Value;
                    }
                    else
                        drpUnitCategory.SelectedValue = "0";

                    // hdnPeal.Value = dRow["inv"].ToString();

                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    protected void chkLstChange_SelectedIndexChanged(object sender, EventArgs e)
    {
        //EnableDisablePanelByCheckBox();
    }

    //private void EnableDisablePanelByCheckBox()
    //{
    //    int intApplicationType = Convert.ToInt32(drpApplicationType.SelectedValue);
    //    if (intApplicationType == (int)enAppFor.New_EMD || intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod)
    //    {
    //        pnlAdditional.Enabled = false;
    //        pnlIndOthers.Enabled = false;
    //        pnlIndustry.Enabled = false;
    //        pnlInvestment.Enabled = false;
    //        pnlLocation.Enabled = false;
    //        pnlname.Enabled = false;
    //        pnlProduction.Enabled = false;
    //        pnlOrgType.Enabled = false;
    //        pnlRedOff.Enabled = false;
    //        divChangeIn.Enabled = true;
    //        pnlApp.Enabled = false;
    //        for (int cnt = 0; cnt < chkLstChange.Items.Count; cnt++)
    //        {
    //            ListItem curr = chkLstChange.Items[cnt];
    //            int id = Convert.ToInt32(curr.Value);
    //            if (curr.Selected)
    //            {
    //                switch (id)
    //                {
    //                    case (int)enAmdType.Name:
    //                        pnlname.Enabled = true;
    //                        break;
    //                    case (int)enAmdType.Org:
    //                        pnlOrgType.Enabled = true;
    //                        break;
    //                    case (int)enAmdType.RegdOff:
    //                        pnlRedOff.Enabled = true;
    //                        break;
    //                    case (int)enAmdType.Location:
    //                        pnlLocation.Enabled = true;
    //                        break;
    //                    case (int)enAmdType.EMD:
    //                        pnlIndOthers.Enabled = true;
    //                        pnlOrgType.Enabled = true;
    //                        pnlRedOff.Enabled = true;
    //                        pnlLocation.Enabled = true;
    //                        pnlProduction.Enabled = true;
    //                        pnlInvestment.Enabled = true;
    //                        pnlAdditional.Enabled = true;
    //                        break;
    //                    default:
    //                        break;
    //                }
    //                string strChangeIn = hdnChangeIn.Value;
    //                if (string.IsNullOrEmpty(hdnChangeIn.Value))
    //                {
    //                    hdnChangeIn.Value = string.Format("{0}", id.ToString());
    //                }
    //                else
    //                {
    //                    hdnChangeIn.Value = string.Format("{0},{1}", hdnChangeIn.Value, id.ToString());
    //                }
    //            }
    //        }
    //    }
    //}

    private void GetIrFormDetails(int appno)
    {
        DataSet objds = new DataSet();
        try
        {
            PcSearch objSearch = new PcSearch()
            {
                strActionCode = "ir",
                intPageIndex = 0,
                intPageSize = 0,
                strFromDate = string.Empty,
                strToDate = string.Empty,
                intAppFor = appno,
            };
            IncentiveMasterBusinessLayer objBuisness = new IncentiveMasterBusinessLayer();
            objds = objBuisness.Incentive_PcForm_View(objSearch);
            DataTable dtDetails = new DataTable();
            if (objds != null && objds.Tables.Count > 0)
            {
                dtDetails = new DataTable();
                dtDetails = objds.Tables[13];
                if (dtDetails != null && dtDetails.Rows.Count > 0)
                {
                    grdFiles.DataSource = dtDetails;
                    grdFiles.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

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
    #endregion
}