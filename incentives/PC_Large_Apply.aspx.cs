/*
 * File name : PC_Large_Apply 
 * class name : incentives_PC_Large_Apply
 * Created By : Sushant Jena
 * Created On : 9/9/2017
 * [Modification History]
 * [CR No]      [Modified By]       [Modified On]       [Description]
 *  1           Suman Lata Gupta                        Code for insertion 
 *  2           Ritika Lath                              Modification as required
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using BusinessLogicLayer.Proposal;
using EntityLayer.Incentive;
using EntityLayer.Proposal;
using Ionic.Zip;
using System.Text;

public partial class incentives_PC_Large_Apply : SessionCheck
{
    const int conUnitothers = 52;
    const int conOrgOthers = 24;

    /// <summary>
    /// Check Session value - to be done
    /// set function for Textarea controls
    /// if edit then fill details of application id
    /// bind all dropdown
    /// also bind all the products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["investorid"] == null)
        {
            Response.Redirect("../default.aspx");
        }
        if (!IsPostBack)
        {
            try
            {
                txtEnterpriseAddress.Attributes.Add("onkeyup", string.Format("return CheckLengthKeyUp('{0}','{1}',200);", txtEnterpriseAddress.ClientID, lblRemark.ClientID));
                txtEnterpriseAddress.Attributes.Add("onchange", string.Format("return checkLength('{0}','{1}',200);", txtEnterpriseAddress.ClientID, lblRemark.ClientID));
                txtOfficeAddress.Attributes.Add("onkeyup", string.Format("return CheckLengthKeyUp('{0}','{1}',200);", txtOfficeAddress.ClientID, lblOfficeAddress.ClientID));
                txtOfficeAddress.Attributes.Add("onchange", string.Format("return checkLength('{0}','{1}',200);", txtOfficeAddress.ClientID, lblOfficeAddress.ClientID));
                txtDateFFI.Attributes.Add("readonly", "readonly");
                lblOldFcci.Attributes.Add("readonly", "readonly");
                lblOldProdValue.Attributes.Add("readonly", "readonly");
                txtProdComm.Attributes.Add("readonly", "readonly");
                txtDateFFI.Attributes.Add("readonly", "readonly");
                txtPowerConnection.Attributes.Add("readonly", "readonly");
                txtDateOfIssuance.Attributes.Add("readonly", "readonly");
                txtDateofPurchase.Attributes.Add("readonly", "readonly");
                txtDateOfProd.Attributes.Add("readonly", "readonly");
                txtPcIssueDate.Attributes.Add("readonly", "readonly");
                BindDropDown();

                BindRadioButton();
                GetApplicationDetails();
                ShowDetailsByConstitutionOfOrg();
                BindProduct();
                SetOwnerCategoryDocByType();

                string strValuue = rdBtnLstPower.SelectedValue;
                if (strValuue == "1")
                {
                    divPower.Visible = true;
                }
                if (Request.QueryString["offline"] != null)
                {
                    hdnOfflineStatus.Value = Request.QueryString["offline"];
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Incentive");
            }
        }
    }

    #region Selected Index Change events
    /// <summary>
    /// selectedindex change event for ddlsector dropdown
    /// bind sub sector with all the details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSubSector(ddlSector.SelectedValue);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// Selected index change event for the rdBtnApplicationFor radio button
    /// It will check if the user is coming from peal/ no peal or pc/ from pc
    /// then based on new and existing fill the dropdown for drpApplicationType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rdBtnApplicationFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divOldContractual.Visible = false;
            divOldDirect.Visible = false;
            divOldFfci.Visible = false;
            divOldPC.Visible = false;
            divOldProd.Visible = false;
            divOldProducts.Visible = false;
            divChangeIn.Visible = false;
            ClearCheckBoxList(chkLstChange);
            for (int cnt = 0; cnt < tblLandPlant.Rows.Count; cnt++)
            {
                tblLandPlant.Rows[cnt].Cells[2].Visible = false;
            }
            for (int cnt = 0; cnt < tblEmployement.Rows.Count; cnt++)
            {
                tblEmployement.Rows[cnt].Cells[1].Visible = false;
                tblEmployement.Rows[cnt].Cells[4].Visible = false;
            }
            int intAppType = Convert.ToInt32(rdBtnApplicationFor.SelectedValue);
            IncentiveMaster objInctMaster = new IncentiveMaster()
            {
                Name = string.Empty,
                ID = string.Empty,
                Param = 0,
                Param_1 = 0,
                Param_2 = string.Empty,
                Param_3 = string.Empty
            };
            IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
            if (intAppType == 1)
            {
                //if user is applying for offline pc or user with no peal or pc is applying for pc, user will be able to see all the options
                if ((Request.QueryString["offline"] != null && Request.QueryString["offline"] == "1") || (Request.QueryString["uType"] != null && Request.QueryString["uType"] == "1"))
                {
                    objInctMaster.Action = "anew";
                }
                else
                {
                    objInctMaster.Action = "new";
                }
                objBuisnessLayer.BindDropdown(drpApplicationType, objInctMaster);
            }
            else
            {
                //if user is applying for offline pc or user with no peal or pc is applying for existing pc, user will be able to see all the options
                if ((Request.QueryString["offline"] != null && Request.QueryString["offline"] == "1") || (Request.QueryString["uType"] != null && Request.QueryString["uType"] == "1"))
                {
                    objInctMaster.Action = "aemd";
                    objBuisnessLayer.BindDropdown(drpApplicationType, objInctMaster);
                }
                else
                {
                    drpApplicationType.Items.Clear();
                    drpApplicationType.Items.Add(new ListItem("Existing", ((int)enAppFor.exist).ToString()));
                    drpApplicationType.SelectedIndex = 0;
                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }

    /// <summary>
    /// selectedindex change event for ddlDistrict dropdown
    /// bind block with all the details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindBlock(ddlDistrict.SelectedValue);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// dropdown to selected whether the user is SC, ST and General and then accordingly make changes
    /// Changes are invluded in function SelectOwnerDocByType
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void drpOwnerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SetOwnerCategoryDocByType();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// based on the organization type selected, change the owner name type and the file type
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void drpOrganizationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpOrganizationType.SelectedIndex > 0)
            {
                fuOrgDocument.Enabled = true;
                lblOrgDocument.Visible = false;
                lnkOrgDocumentDelete.Visible = false;
                hypOrdDocument.Visible = false;
                hypOrdDocument.NavigateUrl = string.Empty;
                hypDocType.Visible = false;
                lnkDocTypeDelete.Visible = false;
                hypDocType.NavigateUrl = string.Empty;
                fuDocumentType.Enabled = true;
                hdnOrgDocument.Value = string.Empty;
                hdnDocumentType.Value = string.Empty;
                lblDocType.Visible = false;
                ShowDetailsByConstitutionOfOrg();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// selectindex change event for the drpUnitType dropdown for filling the product details
    /// It will check if the user has selected others than show textbox to fill the records 
    /// else hide the textbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void drpUnitType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtUnitType.Visible = false;
            if (drpUnitType.SelectedIndex > 0)
            {
                int intUnitType = Convert.ToInt32(drpUnitType.SelectedValue);
                if (intUnitType == 52)
                {
                    txtUnitType.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// selected index change event for drpApplicationType. It will show and hide the option for Amendement i.e divChangeIn
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void drpApplicationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClearCheckBoxList(chkLstChange);
            int intApplicationType = Convert.ToInt32(drpApplicationType.SelectedValue);

            //if user is applying for offline pc, user will be see pop up to fill all the details for existing pc as offline pc
            if ((Request.QueryString["offline"] != null && Request.QueryString["offline"] == "1") && (intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod))
            {

                //Show popup 
                ModalPopupExtender1.Show();

                //then reload the page with setting to specify that it an emd page
                lblPopUpHeader.Text = "Steps for uploading an offline PC under existing E/M/D";
                lblPcMandatory.Text = "Fill all the details for the Original Unit ( i.e. before the IPR 2015 policy effective date). Production Certificate/EM-II details are not mandatory, but provide all details for original unit.";
            }

             //if user is no peal and pc user, 
            else if (Request.QueryString["uType"] != null && Request.QueryString["uType"] == "1")
            {
                //if user is applying for offline pc for exisitng emd, user will be see pop up to fill all the details for existing pc as offline pc
                if (intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod)
                {
                    ModalPopupExtender1.Show();
                    lblPopUpHeader.Text = "Steps for applying for an existing E/M/D";
                    lblPcMandatory.Text = "Fill all the details for the Original Unit ( i.e. before the IPR 2015 policy effective date). Production Certificate/EM-II details are mandatory alongwith the corresponding unit details.";
                }

                //if user is applying for offline pc for New emd, user will be see pop up to fill all the details for new pc as offline pc
                else if (intApplicationType == (int)enAppFor.New_EMD)
                {
                    ModalPopupExtender1.Show();

                    lblPopUpHeader.Text = "Steps for applying for an New unit undergoing E/M/D";
                    lblPcMandatory.Text = "Fill all the details for the Original Unit ( i.e. one with FFCI date after IPR 2015 policy effective date). Production Certificate/EM-II details are mandatory alongwith the corresponding unit details.";
                }
            }

            //normal condition
            else
            {
                if (intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod || intApplicationType == (int)enAppFor.New_EMD)
                {
                    divChangeIn.Visible = true;
                }
                else
                {
                    divChangeIn.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// Selected index change event for the chkplant. It will enable and disable the plant details area
    /// for the user to enter the details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkplant_checkChanged(object sender, EventArgs e)
    {
        if (chkPlant.Checked)
        {
            pnlPlantMachinery.Enabled = true;
        }
        else
        {
            pnlPlantMachinery.Enabled = false;
        }
    }

    /// <summary>
    /// Selected index change event for the chkProductsAmd. It will enable and disable the product details area
    /// for the user to enter the details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkProductsAmd_CheckChanged(object sender, EventArgs e)
    {
        if (chkProductsAmd.Checked)
        {
            pnlProduction.Enabled = true;
        }
        else
        {
            pnlProduction.Enabled = false;
        }
    }

    /// <summary>
    /// Selected index change for chkLstChange. It will call the function EnableDisablePanelByCheckBox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkLstChange_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if the user is uploading offline pc for new_emd or user with no peal or pc is applying for new_emd
            if (!(((Request.QueryString["offline"] != null && Request.QueryString["offline"] == "1") || (Request.QueryString["uType"] != null && Request.QueryString["uType"] == "1")) && (drpApplicationType.SelectedIndex > 0 && Convert.ToInt32(drpApplicationType.SelectedValue) == (int)enAppFor.New_EMD)))
            {
                EnableDisablePanelByCheckBox();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// selected index change event for rdBtnLstPower. It will check if the user has selected power connection is required the 
    /// show the div to enter power related details, else it will hide the div. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rdBtnLstPower_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divPower.Visible = false;
            txtContractDemand.Text = string.Empty;
            lnkPowerAdd.Visible = true;
            lnkPowerDel.Visible = false;
            hypPower.Visible = false;
            hdnPower.Value = string.Empty;
            hypPower.NavigateUrl = string.Empty;
            fuPower.Enabled = true;
            string strValuue = rdBtnLstPower.SelectedValue;
            if (strValuue == "1")
            {
                divPower.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }

    /// <summary>
    /// selected index change event for rbtnTechnical. If the user has selected has yes then show div
    /// to upload the file else hide the div to upload the employement document
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbtnTechnical_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbtnTechnical.SelectedValue == "1")
                dvTechnical.Visible = true;
            else
                dvTechnical.Visible = false;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// Text change event for txtPlantMachinery. It is used to check if the user has entered correct plant and machinery as per the 
    /// unit type selected by the user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtPlantMachinery_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string unittype = string.Empty;
            decimal decInvestment = 0.00M;
            decInvestment = string.IsNullOrEmpty(txtPlantMachinery.Text.Trim()) ? 0.00M : Convert.ToDecimal(txtPlantMachinery.Text.Trim());
            unittype = IncentiveCommonFunctions.GetUnitCategory(drpCompanyType.SelectedValue, decInvestment);
            lblPlantMsg.Text = "";
            if (!string.Equals(drpUnitCategory.SelectedItem.Text, unittype, StringComparison.OrdinalIgnoreCase))
            {
                lblPlantMsg.Text = "As per your investment  <br/> in Plant and Machinery <br/> and nature  of activity provided <br/>your unit category is " + unittype + " <br/> and your current category is " + drpUnitCategory.SelectedItem.Text;
                lblPlantMsg.Visible = true;
            }
            else
            {
                lblPlantMsg.Text = "";
                lblPlantMsg.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }


    #endregion

    #region Click events
    protected void btnokPopup_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["offline"] != null && Request.QueryString["offline"] == "1")
        {
            Response.Redirect("PC_Large_Apply.aspx?offline=2", false);
        }
        else if (Request.QueryString["uType"] != null && Request.QueryString["uType"] == "1")
        {
            //if user is applying for new unit with emd, then user has to fill all the details for new unit as an offline pc
            if (Convert.ToInt32(drpApplicationType.SelectedValue) == (int)enAppFor.New_EMD)
            {
                Response.Redirect("PC_Large_Apply.aspx?uType=3", false);
            }

            //if user is applying for existing emd then user has to fill all details for existing unit
            else
            {
                Response.Redirect("PC_Large_Apply.aspx?uType=2", false);
            }
        }
    }

    private string isValidDate()
    {
        string strPass = "Pass";

        string strOfflineStatus = string.Empty;
        if (!string.IsNullOrEmpty(Request.QueryString["offline"]))
        {
            strOfflineStatus = Request.QueryString["offline"];
        }

        int intApplicationType = Convert.ToInt32(drpApplicationType.SelectedValue);

        //if user is applying for new, transferred, and rehabilitated unit the date of ffci should be after policy effective date
        if ((hdnPeal.Value == "1" || hdnPeal.Value == "2") && (!string.IsNullOrEmpty(txtDateFFI.Value) && !string.IsNullOrEmpty(hdnPolicyEffectiveDate.Value) && (Convert.ToDateTime(txtDateFFI.Value) < Convert.ToDateTime(hdnPolicyEffectiveDate.Value)) && (intApplicationType == (int)enAppFor.New || intApplicationType == (int)enAppFor.Rehabilitated_New || intApplicationType == (int)enAppFor.Transferred_new)))
        {
            strPass = "As your Date of FFCI is less than IPR 2015 policy effective date - " + Convert.ToDateTime(hdnPolicyEffectiveDate.Value).ToString("dd-MMM-yyyy") + " you cannot apply for a PC for " + drpApplicationType.SelectedItem.Text;
            txtDateFFI.Focus();
        }

            //if user is applying for existing unit the date of production commencement should be before policy effective date
        else if ((hdnPeal.Value == "1" || hdnPeal.Value == "2") && (!string.IsNullOrEmpty(txtProdComm.Value) && !string.IsNullOrEmpty(hdnPolicyEffectiveDate.Value) && Convert.ToDateTime(hdnPolicyEffectiveDate.Value) < Convert.ToDateTime(txtProdComm.Value) && (intApplicationType == (int)enAppFor.exist)))
        {
            strPass = "As your Date of production commencement is greater than IPR 2015 policy effective date - " + Convert.ToDateTime(hdnPolicyEffectiveDate.Value).ToString("dd-MMM-yyyy") + " you cannot apply for a PC for a Existing Unit";
            txtProdComm.Focus();
        }

            //if user is applying for migrated unit the date of production commencement should be after policy effective date and date of ffci should be before policy effective date
        else if ((hdnPeal.Value == "1" || hdnPeal.Value == "2") && (intApplicationType == (int)enAppFor.Migrated_new) && !string.IsNullOrEmpty(txtDateFFI.Value) && !string.IsNullOrEmpty(hdnPolicyEffectiveDate.Value) && !string.IsNullOrEmpty(txtProdComm.Value) && !((Convert.ToDateTime(txtDateFFI.Value) < Convert.ToDateTime(hdnPolicyEffectiveDate.Value)) && (Convert.ToDateTime(txtProdComm.Value) > Convert.ToDateTime(hdnPolicyEffectiveDate.Value))))
        {
            strPass = "To apply for " + drpApplicationType.SelectedItem.Text + " your Date of FFCI should be less and date of Production Commencement should be greater than IPR 2015 Policy effective date - " + Convert.ToDateTime(hdnPolicyEffectiveDate.Value).ToString("dd-MMM-yyyy") + ". Please Check.";
            txtProdComm.Focus();
        }

        //if the user is applying for another pc when he has an already existing pc online, or the user is updating an offline pc fo a already provided old pc with ecxisting details
        else if ((hdnPeal.Value == "3" || (!string.IsNullOrEmpty(strOfflineStatus) && strOfflineStatus == "3")) && divOldPC.Visible == true && !string.IsNullOrEmpty(txtProdComm.Value) && Convert.ToDateTime(hdnPrevProdCommDate.Value) > Convert.ToDateTime(txtProdComm.Value))
        {
            strPass = "Date of production commencement cannot be less than Date of commencement of Production of previous production certificate - " + Convert.ToDateTime(hdnPrevProdCommDate.Value).ToString("dd-MMM-yyyy");
            txtProdComm.Focus();
        }
        else if (drpUnitCategory.SelectedIndex > 0 && drpCompanyType.SelectedIndex > 0 && !string.IsNullOrEmpty(txtPlantMachinery.Text.Trim()) && Convert.ToDecimal(txtPlantMachinery.Text) != 0.00M)
        {
            string  strSelectedUnitType = drpUnitCategory.SelectedItem.Text;
            decimal decPlantMachinery = Convert.ToDecimal(txtPlantMachinery.Text);
            string strCompanyType= drpCompanyType.SelectedValue;
            string strCalculatedUnitType = IncentiveCommonFunctions.GetUnitCategory(strCompanyType, decPlantMachinery);
            if (!string.Equals(strSelectedUnitType, strCalculatedUnitType, StringComparison.OrdinalIgnoreCase))
            {
                strPass = "As per your investment in Plant and Machinery and nature  of activity provided your unit category is " + strCalculatedUnitType + ", but your selected category is " + strSelectedUnitType + ". Please check.";
                txtPlantMachinery.Focus();
            }
        }
        return strPass;
    }

    private string isValidValues()
    {
        string strMessage = "Pass";
        string strMsg = "Pass";
        string strFocusControlId = string.Empty;
        string functionCall = string.Empty;
        string strOfflineStatus = string.Empty;
        if (!string.IsNullOrEmpty(Request.QueryString["offline"]))
        {
            strOfflineStatus = Request.QueryString["offline"];
        }
        int appType = Convert.ToInt32(drpApplicationType.SelectedValue);
        int cnt = 0;
        for (int i = 0; i < chkLstChange.Items.Count; i++)
        {
            if (chkLstChange.Items[i].Selected == true)
                cnt++;
        }
        if ((appType == (int)enAppFor.exist_mod || appType == (int)enAppFor.exist_Exp || appType == (int)enAppFor.exist_div || appType == (int)enAppFor.New_EMD) && cnt == 0)
        {
            strMsg = "Please select atleast one checkbox for change in";
            strFocusControlId = chkLstChange.ID;
            functionCall = "CollapseFirst();";
        }
        else if (drpOwnerType.SelectedIndex > 0 && (Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.OBC || Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.OBC_WOMEN || Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.SC || Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.SC_WOMEN || Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.ST || Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.ST_WOMEN) && rdBtnOwnerCategory.SelectedIndex < 0)
        {
            strMsg = "Please select document type for Certificate in support of SC / ST / OBC ";
            strFocusControlId = fuOwnerCategory.ID;
            functionCall = "CollapseFirst();";
        }
        else if (drpOwnerType.SelectedIndex > 0 && (Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.OBC || Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.OBC_WOMEN || Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.SC || Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.SC_WOMEN || Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.ST || Convert.ToInt32(drpOwnerType.SelectedValue) == (int)enOwnerType.ST_WOMEN) && string.IsNullOrEmpty(hdnOwnerCategory.Value))
        {
            strMsg = "Please provide Certificate in support of SC / ST / OBC  ";
            strFocusControlId = fuOwnerCategory.ID;
            functionCall = "CollapseFirst();";
        }
        else if (divOldFfci.Visible && (Convert.ToDateTime(lblOldFcci.Value) > Convert.ToDateTime(txtDateFFI.Value)))
        {
            strMsg = "Date of FFCI cannot be less than/equal to Date of FFCI - Original E/M/D";
            strFocusControlId = txtDateFFI.ID;
            functionCall = "CollapseIndustry();";
        }
        else if (divOldProd.Visible && (Convert.ToDateTime(lblOldProdValue.Value) > Convert.ToDateTime(txtProdComm.Value)))
        {
            strMsg = "Date of production commencement cannot be less than/equal to Date of commencement of Production - Original E/M/D";
            strFocusControlId = txtProdComm.ID;
            functionCall = "CollapseEmployee();";
        }
        else if ((!string.IsNullOrEmpty(strOfflineStatus) && (strOfflineStatus == "1" || strOfflineStatus == "3")) || (Request.QueryString["uType"] != null && (Request.QueryString["uType"] == "2" || Request.QueryString["uType"] == "3")))
        {
            if (string.IsNullOrEmpty(txtOfflinePcNo.Text.Trim()))
            {
                strMsg = "Production Certificate/IEM-II number cannot be blank!";
                strFocusControlId = txtOfflinePcNo.ID;
                functionCall = "CollaspseAdditional();";
            }
            else if (string.IsNullOrEmpty(txtPcIssueDate.Value.Trim()))
            {
                strMsg = "Date of Issue of Production Certificate/IEM-II cannot be blank!";
                strFocusControlId = txtPcIssueDate.ID;
                functionCall = "CollaspseAdditional();";
            }
            else if (!string.IsNullOrEmpty(txtPcIssueDate.Value.Trim()) && Convert.ToDateTime(txtPcIssueDate.Value) > DateTime.Today)
            {
                strMsg = "Date of Issue of Production Certificate/IEM-II cannot be greater than current date!";
                strFocusControlId = txtPcIssueDate.ID;
                functionCall = "CollaspseAdditional();";
            }

            //else if user is updating existing or existing emd details then verify that date of production commencement will be lesss than the pc issue date
            //if the user is applying for another pc when he has an already existing pc online, or the user is updating an offline pc fo a already provided old pc with ecxisting details
            else if (!string.IsNullOrEmpty(txtProdComm.Value) && !string.IsNullOrEmpty(txtPcIssueDate.Value) && (Convert.ToDateTime(txtProdComm.Value) > Convert.ToDateTime(txtPcIssueDate.Value)))
            {
                strMsg = "Date of Issue of Production Certificate/IEM-II cannot be less than date of production commencement!";
                strFocusControlId = txtPcIssueDate.ID;
                functionCall = "CollaspseAdditional();";
            }

               //else if user is updating existing with emd then check if the last production certificate issue date if one provided must be less than the production certificate
            else if (strOfflineStatus == "3" && !string.IsNullOrEmpty(lblPcIssueDate.Text) && !string.IsNullOrEmpty(txtPcIssueDate.Value) && (Convert.ToDateTime(lblPcIssueDate.Text) > Convert.ToDateTime(txtPcIssueDate.Value)))
            {
                strMsg = "Date of Issue of Production Certificate/IEM-II cannot be less than date of old production certificate/IEM-II!";
                strFocusControlId = txtPcIssueDate.ID;
                functionCall = "CollaspseAdditional();";
                txtPcIssueDate.Focus();
            }

            else if (string.IsNullOrEmpty(hdnProductionCertificate.Value.Trim()))
            {
                strMsg = "Please upload Production Certificate/IEM-II!";
                strFocusControlId = fuProductionCertificate.ID;
                functionCall = "CollaspseAdditional();";
                fuProductionCertificate.Focus();
            }
        }

        if (!string.Equals(strMsg, "Pass", StringComparison.OrdinalIgnoreCase))
        {
            strMessage = "jAlert('<strong>" + strMsg + "</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + strFocusControlId + "').focus(); });" + functionCall + "";
        }
        return strMessage;
    }


    /// <summary>
    /// Click event for btnSaveAsDraft
    /// Common function for apply and save as draft button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        try
        {
            string strValidDate = "Pass";
            strValidDate = isValidDate();
            Button objButton = (Button)sender;
            string strCommandArg = objButton.CommandArgument;
            Incentive_PCMaster objMaster = new Incentive_PCMaster();

            //validate logic, check date of prod comm for new , existing and compare old new prod comm dates
            if (!string.Equals(strValidDate, "Pass", StringComparison.OrdinalIgnoreCase))
            {
                ScriptManager.RegisterStartupScript(btnSaveAsDraft, this.GetType(), "OnClick", "jAlert('" + strValidDate + "','GO-SWIFT');", true);
            }

            //verify entry by the user 
            else
            {
                string strPass = "Pass";
                strPass = isValidValues();
                if (!string.Equals(strCommandArg, "d", StringComparison.OrdinalIgnoreCase) && !string.Equals(strPass, "Pass", StringComparison.OrdinalIgnoreCase))
                {
                    ScriptManager.RegisterStartupScript(btnSaveAsDraft, this.GetType(), "OnClick", strPass, true);
                }
                else
                {
                    if (string.Equals(strCommandArg, "d", StringComparison.OrdinalIgnoreCase))
                    {
                        objMaster.intApplyFlag = 0;
                    }
                    else
                    {
                        objMaster.intApplyFlag = 1;
                    }

                    if (Request.QueryString["id"] != null)
                    {
                        objMaster.strActionCode = "u";
                        objMaster.intAppNo = Convert.ToInt32(Request.QueryString["id"]);

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(hdnApplyFlag.Value) || hdnApplyFlag.Value == "0")
                        {
                            objMaster.strActionCode = "add";
                        }
                        else
                        {
                            objMaster.strActionCode = "u";
                            objMaster.intAppNo = Convert.ToInt32(lblAppNo.Text.Substring(7, 4));
                        }
                    }

                    #region set properties
                    objMaster.intTechnical = Convert.ToInt32(rbtnTechnical.SelectedValue);
                    objMaster.intAppFor = Convert.ToInt32(drpApplicationType.SelectedValue);
                    objMaster.strChngIn = GetCheckBoxListValues(chkLstChange);
                    objMaster.strEINEMIIPMTNo = txtEin.Text.Trim();
                    objMaster.strUAN = txtUan.Text.Trim();
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
                    objMaster.strOfficeMobCode = ddlCode.SelectedValue;
                    objMaster.strOfficeFaxCode = drpFx.SelectedValue;
                    objMaster.strEntFaxCode = drpEnterpriseFax.SelectedValue;
                    objMaster.strEntMobCode = drpEntCode.SelectedValue;
                    objMaster.intInvType = Convert.ToInt32(hdnPeal.Value);
                    objMaster.dtmFFCI = txtDateFFI.Value;

                    if (!string.IsNullOrEmpty(txtWorkingCapital.Text.Trim()))
                    {
                        objMaster.decWorkingCapital = Convert.ToDecimal(txtWorkingCapital.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtEquity.Text.Trim()))
                    {
                        objMaster.decEquity = Convert.ToDecimal(txtEquity.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtLoan.Text.Trim()))
                    {
                        objMaster.decLoan = Convert.ToDecimal(txtLoan.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtFdiComponent.Text.Trim()))
                    {
                        objMaster.decFdiComp = Convert.ToDecimal(txtFdiComponent.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtGeneral.Text.Trim()))
                    {
                        objMaster.intGeneral = Convert.ToInt32(txtGeneral.Text.Trim());
                    }

                    if (!string.IsNullOrEmpty(txtWomen.Text.Trim()))
                    {
                        objMaster.intWomen = Convert.ToInt32(txtWomen.Text.Trim());
                    }
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
                    if (!string.IsNullOrEmpty(txtDirectEmployement.Text.Trim()))
                    {
                        objMaster.intTotalEmployee = Convert.ToInt32(txtDirectEmployement.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtContractualEmp.Text.Trim()))
                    {
                        objMaster.intContractual = Convert.ToInt32(txtContractualEmp.Text.Trim());
                    }
                    objMaster.intChangeIn = Convert.ToInt32(drpChangeIn.SelectedValue);
                    objMaster.dtmEinIssuance = txtDateOfIssuance.Value;
                    objMaster.strProductCode = txtProductCode.Text.Trim();
                    objMaster.strProductName = txtName.Text.Trim();
                    objMaster.intIsPwrReq = Convert.ToInt32(rdBtnLstPower.SelectedValue);
                    objMaster.intSectorId = Convert.ToInt32(ddlSector.SelectedValue);
                    objMaster.intSubSectorId = Convert.ToInt32(ddlSubSector.SelectedValue);
                    objMaster.intBlock = Convert.ToInt32(ddlBlock.SelectedValue);

                    objMaster.intDistrict = Convert.ToInt32(ddlDistrict.SelectedValue);


                    if (!string.IsNullOrEmpty(txtland.Text.Trim()))
                    {
                        objMaster.decLandInvestment = Convert.ToDecimal(txtland.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtBuilding.Text.Trim()))
                    {
                        objMaster.decBuilding = Convert.ToDecimal(txtBuilding.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtPlantMachinery.Text.Trim()))
                    {
                        objMaster.decPlant = Convert.ToDecimal(txtPlantMachinery.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtOthers.Text.Trim()))
                    {
                        objMaster.decOthers = Convert.ToDecimal(txtOthers.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtContractDemand.Text.Trim()))
                    {
                        objMaster.decContractDemand = Convert.ToDecimal(txtContractDemand.Text.Trim());
                    }
                    objMaster.dtmProdComm = txtProdComm.Value;

                    objMaster.strDateConnection = txtPowerConnection.Value;

                    if (!string.IsNullOrEmpty(hdnIsOsPCBDownloaded.Value) && hdnIsOsPCBDownloaded.Value == "1")
                    {
                        hdnClearence.Value = "Clearence" + DateTime.Now.ToString("_ddMMyyhhmmss") + ".zip";
                    }

                    //if there is folder for the user in ospcb_temp then first create a new file name as per our syntax 
                    if (!string.IsNullOrEmpty(hdnBoilderDownloaded.Value) && hdnBoilderDownloaded.Value == "1")
                    {
                        hdnBoiler.Value = "FactoryBoiler" + DateTime.Now.ToString("_ddMMyyhhmmss") + ".zip";
                    }
                    objMaster.strFileClearence = hdnClearence.Value;
                    objMaster.strFileEmployement = hdnEmployement.Value;
                    objMaster.strFileFirstSaleBill = hdnFistSaleBill.Value;
                    objMaster.strFileLand = hdnLand.Value;
                    objMaster.strFileorgTypeDocument = hdnOrgDocument.Value;
                    objMaster.strFileOwnerTypeDocument = hdnDocumentType.Value;
                    objMaster.strFilePlant = hdnPlant.Value;
                    objMaster.strFilePower = hdnPower.Value;
                    objMaster.strFileProject = hdnProject.Value;
                    objMaster.strFileSanction = hdnBank.Value;
                    objMaster.strFileProducts = hdnProductfilename.Value;
                    objMaster.strFileOwnerCategory = hdnOwnerCategory.Value;
                    objMaster.strRMPreFile = hdnRawMaterialPre.Value;
                    objMaster.strRMPostFile = hdnRawMaterialPost.Value;
                    objMaster.strIEMFile = hdnIEM.Value;
                    objMaster.VATFile = hdnVAT.Value;
                    objMaster.strBuildFile = hdnBuildingValuation.Value;
                    objMaster.strAgreementFile = hdnAgreement.Value;
                    objMaster.strSaleInvoiceFile = hdnSaleInvoice.Value;
                    objMaster.strFactoryLicFile = hdnFactoryLic.Value;
                    objMaster.strRMInoviceFile = hdnInovice.Value;
                    objMaster.strInvestCommercialFile = hdnCompAuthority.Value;
                    objMaster.strProductionFile = hdnProductionPost.Value; //production for last three months
                    objMaster.strBoilerFile = hdnBoiler.Value;
                    //objMaster.strPlantFile = hdn

                    objMaster.strUnitOthersk = txtOtherOrg.Text.Trim();
                    objMaster.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
                    objMaster.strIndustryCode = lblIndustryCode.Text;
                    objMaster.strOthersOrg = txtOtherOrg.Text.Trim();
                    objMaster.GSTIN = txtGST.Text.Trim();
                    objMaster.BitPlantModified = chkPlant.Checked;
                    objMaster.BitProdModified = chkProductsAmd.Checked;
                    #endregion

                    #region Products
                    DataTable dtProducts = CreateProductTable();
                    if (grdProducts.Rows.Count > 0)
                    {
                        dtProducts.TableName = "Products";
                        for (int cnt = 0; cnt < grdProducts.Rows.Count; cnt++)
                        {
                            GridViewRow GRow = grdProducts.Rows[cnt];
                            HiddenField hdnUnit = (HiddenField)GRow.FindControl("hdnUnit");
                            HiddenField hdnIsMainProduct = (HiddenField)GRow.FindControl("hdnIsMainProduct");
                            HiddenField hdnUnitOthers = (HiddenField)GRow.FindControl("hdnUnitOthers");
                            DataRow dRow = dtProducts.NewRow();
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
                            dRow["UnitOthers"] = hdnUnitOthers.Value;
                            dRow["Unit"] = hdnUnit.Value;
                            dRow["bitMainProduct"] = hdnIsMainProduct.Value;
                            if (string.IsNullOrEmpty(GRow.Cells[5].Text) || GRow.Cells[5].Text == "&nbsp;")
                            {
                                dRow["Cost"] = "0.00";
                            }
                            else
                            {
                                dRow["Cost"] = GRow.Cells[5].Text;
                            }
                            dRow["dtmProd"] = GRow.Cells[6].Text;
                            dtProducts.Rows.Add(dRow);
                        }
                    }
                    if (dtProducts.Rows.Count > 0)
                    {
                        CommonFunctions obj = new CommonFunctions();
                        objMaster.strXml = obj.GetSTRXMLResult(dtProducts);
                    }
                    #endregion

                    #region Documents
                    DataTable dtFiles = new DataTable() { TableName = "dtFiles" };
                    dtFiles.Columns.Add(new DataColumn("id")
                    {
                        AutoIncrement = true,
                        AutoIncrementSeed = 1,
                        AutoIncrementStep = 1
                    });
                    dtFiles.Columns.Add(new DataColumn("vchDocId"));
                    dtFiles.Columns.Add(new DataColumn("vchFileName"));
                    dtFiles.Columns.Add(new DataColumn("vchFolderName"));


                    DataRow dorgRow = dtFiles.NewRow();
                    dorgRow["vchDocId"] = rdBtnOrg.SelectedValue;
                    dorgRow["vchFileName"] = hdnOrgDocument.Value;
                    dorgRow["vchFolderName"] = "/Files/Industry/";
                    dtFiles.Rows.Add(dorgRow);

                    DataRow downertype = dtFiles.NewRow();
                    downertype["vchDocId"] = rdBtnOwnerTYpe.SelectedValue;
                    downertype["vchFileName"] = hdnDocumentType.Value;
                    downertype["vchFolderName"] = "/Files/Industry/";
                    dtFiles.Rows.Add(downertype);

                    DataRow dLand = dtFiles.NewRow();
                    dLand["vchDocId"] = rdBtnLand.SelectedValue;
                    dLand["vchFileName"] = hdnLand.Value;
                    dLand["vchFolderName"] = "/Files/Investment/";
                    dtFiles.Rows.Add(dLand);

                    DataRow dPlant = dtFiles.NewRow();
                    dPlant["vchDocId"] = rdBtnPlant.SelectedValue;
                    dPlant["vchFileName"] = hdnPlant.Value;
                    dPlant["vchFolderName"] = "/Files/Investment/";
                    dtFiles.Rows.Add(dPlant);

                    DataRow dBank = dtFiles.NewRow();
                    dBank["vchDocId"] = hdnBankAppDocId.Value;
                    dBank["vchFileName"] = hdnBank.Value;
                    dBank["vchFolderName"] = "/Files/Investment/";
                    dtFiles.Rows.Add(dBank);

                    DataRow dEmployement = dtFiles.NewRow();
                    dEmployement["vchDocId"] = rdBtnEmployement.Value;
                    dEmployement["vchFileName"] = hdnEmployement.Value;
                    dEmployement["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dEmployement);

                    DataRow dOwnerCat = dtFiles.NewRow();
                    dOwnerCat["vchDocId"] = rdBtnOwnerCategory.SelectedValue;
                    dOwnerCat["vchFileName"] = hdnOwnerCategory.Value;
                    dOwnerCat["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dOwnerCat);

                    DataRow dRawMaterial = dtFiles.NewRow();
                    dRawMaterial["vchDocId"] = rdBtnRawMaterial.SelectedValue;
                    dRawMaterial["vchFileName"] = hdnProductfilename.Value;
                    dRawMaterial["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dRawMaterial);

                    DataRow dPower = dtFiles.NewRow();
                    dPower["vchDocId"] = rdBtnPower.SelectedValue;
                    dPower["vchFileName"] = hdnPower.Value;
                    dPower["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dPower);

                    if (!string.IsNullOrEmpty(hdnProject.Value))
                    {
                        DataRow dProject = dtFiles.NewRow();
                        dProject["vchDocId"] = rdBtnProject.Value;
                        dProject["vchFileName"] = hdnProject.Value;
                        dProject["vchFolderName"] = "/Files/Production/";
                        dtFiles.Rows.Add(dProject);
                    }


                    DataRow dClearence = dtFiles.NewRow();
                    dClearence["vchDocId"] = rdBtnClearence.Value;
                    dClearence["vchFileName"] = hdnClearence.Value;
                    dClearence["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dClearence);

                    if (!string.IsNullOrEmpty(hdnBoiler.Value))
                    {
                        DataRow dBoiler = dtFiles.NewRow();
                        dBoiler["vchDocId"] = hdnBoilerDocId.Value;
                        dBoiler["vchFileName"] = hdnBoiler.Value;
                        dBoiler["vchFolderName"] = "/Files/Production/";
                        dtFiles.Rows.Add(dBoiler);
                    }

                    if (!string.IsNullOrEmpty(hdnIEM.Value))
                    {
                        DataRow dIEM = dtFiles.NewRow();
                        dIEM["vchDocId"] = hdnIEMDoc.Value;
                        dIEM["vchFileName"] = hdnIEM.Value;
                        dIEM["vchFolderName"] = "/Files/Industry/";
                        dtFiles.Rows.Add(dIEM);
                    }
                    if (!string.IsNullOrEmpty(hdnVAT.Value))
                    {
                        DataRow dGST = dtFiles.NewRow();
                        dGST["vchDocId"] = hdnGSTDocId.Value;
                        dGST["vchFileName"] = hdnVAT.Value;
                        dGST["vchFolderName"] = "/Files/Industry/";
                        dtFiles.Rows.Add(dGST);
                    }


                    DataRow dFactory = dtFiles.NewRow();
                    dFactory["vchDocId"] = hdnFactoryDoc.Value;
                    dFactory["vchFileName"] = hdnFactoryLic.Value;
                    dFactory["vchFolderName"] = "/Files/Industry/";
                    dtFiles.Rows.Add(dFactory);

                    DataRow dBuilding = dtFiles.NewRow();
                    dBuilding["vchDocId"] = hdnBuildDocId.Value;
                    dBuilding["vchFileName"] = hdnBuildingValuation.Value;
                    dBuilding["vchFolderName"] = "/Files/Investment/";
                    dtFiles.Rows.Add(dBuilding);

                    DataRow dComAuthority = dtFiles.NewRow();
                    dComAuthority["vchDocId"] = hdnCompAuthorityDocId.Value;
                    dComAuthority["vchFileName"] = hdnCompAuthority.Value;
                    dComAuthority["vchFolderName"] = "/Files/Investment/";
                    dtFiles.Rows.Add(dComAuthority);


                    DataRow dRawMaterialInvoice = dtFiles.NewRow();
                    dRawMaterialInvoice["vchDocId"] = hdnInvoiceDocId.Value;
                    dRawMaterialInvoice["vchFileName"] = hdnInovice.Value;
                    dRawMaterialInvoice["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dRawMaterialInvoice);


                    DataRow dRawMaterialSTPre = dtFiles.NewRow();
                    dRawMaterialSTPre["vchDocId"] = hdnRawMaterialPreDocId.Value;
                    dRawMaterialSTPre["vchFileName"] = hdnRawMaterialPre.Value;
                    dRawMaterialSTPre["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dRawMaterialSTPre);


                    DataRow dRawMaterialSTPost = dtFiles.NewRow();
                    dRawMaterialSTPost["vchDocId"] = hdnRawMaterialPostDocId.Value;
                    dRawMaterialSTPost["vchFileName"] = hdnRawMaterialPost.Value;
                    dRawMaterialSTPost["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dRawMaterialSTPost);

                    DataRow dProduction = dtFiles.NewRow();
                    dProduction["vchDocId"] = hdnProductiondocId.Value;
                    dProduction["vchFileName"] = hdnProductionPost.Value;
                    dProduction["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dProduction);

                    DataRow dSaleBill3yrs = dtFiles.NewRow();
                    dSaleBill3yrs["vchDocId"] = hdnFistSaleBillDocId.Value;
                    dSaleBill3yrs["vchFileName"] = hdnFistSaleBill.Value;
                    dSaleBill3yrs["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dSaleBill3yrs);

                    DataRow dSaleInvoice = dtFiles.NewRow();
                    dSaleInvoice["vchDocId"] = hdnSaleInvoiceDocId.Value;
                    dSaleInvoice["vchFileName"] = hdnSaleInvoice.Value;
                    dSaleInvoice["vchFolderName"] = "/Files/Production/";
                    dtFiles.Rows.Add(dSaleInvoice);

                    DataRow dAgreementId = dtFiles.NewRow();
                    dAgreementId["vchDocId"] = hdnAgreementDocId.Value;
                    dAgreementId["vchFileName"] = hdnAgreement.Value;
                    dAgreementId["vchFolderName"] = "/Files/Power/";
                    dtFiles.Rows.Add(dAgreementId);



                    CommonFunctions objCommon = new CommonFunctions();
                    objMaster.strFileXML = objCommon.GetSTRXMLResult(dtFiles);
                    #endregion

                    #region Plant and machinery
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
                        objMaster.strXmlMachinery = obj.GetSTRXMLResult(dtMachinery);
                    }
                    #endregion

                    //settings to be added to make the pc pre approved pc in offline and in case of no peal and pc applying for existing emd
                    if ((Request.QueryString["offline"] != null) || (Request.QueryString["uType"] != null))
                    {
                        if (objMaster.intAppFor == (int)enAppFor.exist || objMaster.intAppFor == (int)enAppFor.New || objMaster.intAppFor == (int)enAppFor.Rehabilitated_New || objMaster.intAppFor == (int)enAppFor.Transferred_new || objMaster.intAppFor == (int)enAppFor.Migrated_new)
                        {
                            objMaster.dtmIssueDate = txtPcIssueDate.Value;
                        }
                        else if (objMaster.intAppFor == (int)enAppFor.exist_div || objMaster.intAppFor == (int)enAppFor.exist_Exp || objMaster.intAppFor == (int)enAppFor.exist_mod || objMaster.intAppFor == (int)enAppFor.New_EMD)
                        {
                            objMaster.dtmAmendedOn = txtPcIssueDate.Value;
                        }
                        objMaster.strPCNo = txtOfflinePcNo.Text;
                        objMaster.strPdfName = hdnProductionCertificate.Value;

                        //if user is applying for normal new or existing pc then make it pre approved and set the values  and in case of no peal and pc applying for existing emd
                        if (Request.QueryString["offline"] == "1" || Request.QueryString["offline"] == "3" || Request.QueryString["uType"] == "2" || Request.QueryString["uType"] == "3")
                        {
                            objMaster.intApproved = 2;
                            objMaster.intGeneratePc = 1;
                            objMaster.intInvType = 3;
                            objMaster.intOfflinePc = 1;
                        }

                        //if user is applying for existing details before applying for existing with amendement pc
                        else if (Request.QueryString["offline"] == "2")
                        {
                            objMaster.intApplyFlag = 0;
                            objMaster.intApproved = 0;
                            objMaster.intGeneratePc = 0;
                            objMaster.intInvType = 1;
                            objMaster.intOfflinePc = 1;
                        }
                    }

                    int intRetValue = 0;
                    IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
                    intRetValue = objBuisnessLayer.PC_Large_AED(objMaster);

                    //if record was saved successfully it will get a new application id or the updated application
                    if (intRetValue >= 1000)
                    {

                        //if hdnIsOsPCBDownloaded value is 1 that means, there is folder for the user in ospcb_temp then first copy folder to a new one with the new file name created by us 
                        if (!string.IsNullOrEmpty(hdnIsOsPCBDownloaded.Value) && hdnIsOsPCBDownloaded.Value == "1")
                        {
                            DeleteServiceOldDoc(enServiceDocType.OSPCB, hdnClearence);
                        }


                        //if hdnIsOsPCBDownloaded value is 1 that means, there is folder for the user in ospcb_temp then first copy folder to a new one with the new file name created by us 
                        if (!string.IsNullOrEmpty(hdnBoilderDownloaded.Value) && hdnBoilderDownloaded.Value == "1")
                        {
                            DeleteServiceOldDoc(enServiceDocType.Boiler, hdnBoiler);
                        }

                        //if it is in draft position and new record is being added
                        if (objMaster.intApplyFlag == 0)
                        {
                            if (Request.QueryString["offline"] != null && Request.QueryString["offline"] == "2")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirectOffline('" + Messages.ShowMessage("1") + "');</script>", false);
                            }
                            else
                            {
                                if (objMaster.strActionCode == "add")
                                {
                                    //show message for add
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirectno('" + Messages.ShowMessage("1") + "');</script>", false);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Save Successfully !', '" + Messages.TitleOfProject + "', function () {location.href ='../pcViewPage.aspx';}); </script>", false);
                                    return;
                                }
                                else if (objMaster.strActionCode == "u")
                                {
                                    //show message for update
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirectno('" + Messages.ShowMessage("2") + "');</script>", false);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Drafted Successfully !', '" + Messages.TitleOfProject + "', function () {location.href ='../pcViewPage.aspx';}); </script>", false);
                                    return;
                                   

                                }
                            }
                        }
                        else if (objMaster.intApplyFlag == 1)
                        {
                            if (Request.QueryString["offline"] != null)
                            {
                                if (Request.QueryString["offline"] == "1" || Request.QueryString["offline"] == "3")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + Messages.ShowMessage("1") + "');</script>", false);
                                }
                            }
                            else if (Request.QueryString["uType"] != null && (Request.QueryString["uType"] == "2" || Request.QueryString["uType"] == "3"))
                            {
                                //show message for add
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirectno('" + Messages.ShowMessage("1") + "');</script>", false);
                            }
                            else if ((Request.QueryString["uType"] != null && Request.QueryString["uType"] == "1") || (Request.QueryString["uType"] == null))
                            {
                                if (objMaster.strActionCode == "add")
                                {
                                    //show message for add
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirectFeedBack('" + Messages.ShowMessage("1") + "'," + intRetValue.ToString() + "," + (int)enFeedBackFormType.PCertificate + ");</script>", false);
                                }
                                else if (objMaster.strActionCode == "u")
                                {
                                    //show message for update
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirectFeedBack('" + Messages.ShowMessage("2") + "'," + intRetValue.ToString() + "," + (int)enFeedBackFormType.PCertificate + ");</script>", false);
                                }
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(btnSaveAsDraft, this.GetType(), "OnClick", "jAlert('" + Messages.ShowMessage(intRetValue.ToString()) + "','GO-SWIFT');", true);
                    }
                }
            }//if all valid
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// Common Click function to upload document in the upload controls
    /// It will take the ID of the link button and accordingly call the common function UploadDocument alongwith the controls
    /// that are necessary
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkOrgDocumentPdf_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkOrgDocumentPdf.ID))
            {
                if (fuOrgDocument.HasFile)
                {
                    string strFileName = "OrdDocument" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuOrgDocument, hdnOrgDocument, strFileName, hypOrdDocument, lblOrgDocument, lnkOrgDocumentDelete, "Industry");
                }
            }
            else if (string.Equals(lnk.ID, lnkDocTypeUpload.ID))
            {
                if (fuDocumentType.HasFile)
                {
                    string strFileName = "OwnerType" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuDocumentType, hdnDocumentType, strFileName, hypDocType, lblDocType, lnkDocTypeDelete, "Industry");
                }
            }
            else if (string.Equals(lnk.ID, lnkPlantadd.ID))
            {
                if (fuPlant.HasFile)
                {
                    string strFileName = "Plant" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuPlant, hdnPlant, strFileName, hypPlant, lblPlant, lnkPlandDelete, "Investment");
                }
            }
            else if (string.Equals(lnk.ID, lnkLandAdd.ID))
            {
                if (fuLand.HasFile)
                {
                    string strFileName = "Land" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuLand, hdnLand, strFileName, hypLandDelete, lblLand, lnkLandDelete, "Investment");
                }
            }
            else if (string.Equals(lnk.ID, lnkbankAdd.ID))
            {
                if (fuBank.HasFile)
                {
                    string strFileName = "Bank" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuBank, hdnBank, strFileName, hypBank, lblBank, lnkbankDelete, "Investment");
                }
            }
            else if (string.Equals(lnk.ID, lnkEmployementAdd.ID))
            {
                if (fuEmployement.HasFile)
                {
                    string strFileName = "Employee" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuEmployement, hdnEmployement, strFileName, hypEmployement, lblEmployement, lnkEmployementDelete, "Production");
                }
            }
            else if (string.Equals(lnk.ID, lnkFirstSalBillAdd.ID))
            {
                if (fuFirstSaleBill.HasFile)
                {
                    string strFileName = "FirstSaleBill" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuFirstSaleBill, hdnFistSaleBill, strFileName, hypFirstSaleBill, lblFirstSaleBill, lnkFirstSaleBillDel, "Production");
                }
            }
            else if (string.Equals(lnk.ID, lnkPowerAdd.ID))
            {
                if (fuPower.HasFile)
                {
                    string strFileName = "Power" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuPower, hdnPower, strFileName, hypPower, lblPower, lnkPowerDel, "Production");
                }
            }
            else if (string.Equals(lnk.ID, lnkProjectAdd.ID))
            {
                if (fuProject.HasFile)
                {
                    string strFileName = "Project" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuProject, hdnProject, strFileName, hypProject, lblProject, lnkProjectDel, "Production");
                }
            }

            //clearence
            else if (string.Equals(lnk.ID, lnkCLearenceAdd.ID))
            {
                if (fuClearence.HasFile)
                {
                    string[] strExtension = { ".zip" };
                    string strFileName = "Clearence" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuClearence, hdnClearence, strFileName, hypCLearence, lblClearence, lnkClearenceDel, "Production", strExtension, ".zip");
                }
            }

            else if (string.Equals(lnk.ID, lnkOnwerCatAdd.ID))
            {
                if (fuOwnerCategory.HasFile)
                {
                    string strFileName = "OwnerCategory" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuOwnerCategory, hdnOwnerCategory, strFileName, hypOwnerCategory, lblOwnerCategoryFile, lnkOwnerCatDel, "Production");
                    for (int i = 0; i < rdBtnOwnerCategory.Items.Count; i++)
                    {
                        if (rdBtnOwnerCategory.Items[i].Selected == false)
                        {
                            rdBtnOwnerCategory.Items[i].Attributes.Add("style", "display:none");
                        }
                    }
                }
            }
            ///FOR VAT/GST
            else if (string.Equals(lnk.ID, lnkVATPDF.ID))
            {
                if (fupVAT.HasFile)
                {
                    string strFileName = "VAT" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupVAT, hdnVAT, strFileName, hypVAT, lblVAT, lnkVATDel, "Industry");
                }
            }
            ///FOR IEM
            else if (string.Equals(lnk.ID, lnkIEM.ID))
            {
                if (fupIEM.HasFile)
                {
                    string strFileName = "IEM" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupIEM, hdnIEM, strFileName, hypIEMDelete, lblIEM, lnkIEMDelete, "Industry");
                }
            }
            else if (string.Equals(lnk.ID, lnkBuildVal.ID))
            {
                if (fupBuildingValReport.HasFile)
                {
                    string strFileName = "BuildReport" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupBuildingValReport, hdnBuildingValuation, strFileName, hypBuildVal, lblBuildVal, lnkBuildValdel, "Investment");
                }
            }
            //for agreement with CESU
            else if (string.Equals(lnk.ID, lnkAgreement.ID))
            {
                if (fupAgreement.HasFile)
                {
                    string strFileName = "Agreement" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupAgreement, hdnAgreement, strFileName, hypAgreement, lblAgreement, lnkAgreementDel, "Power");
                }
            }
            //FOR INVOICE OF RAW MATERIAL
            else if (string.Equals(lnk.ID, lnkInovice.ID))
            {
                if (fupInvoice.HasFile)
                {
                    string strFileName = "Invoice" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupInvoice, hdnInovice, strFileName, hypInovice, lblProductFile, lnkInvoiceDel, "Production");
                }
            }

               //FOR INVOICE OF RAW MATERIAL PREPRODUCTION
            else if (string.Equals(lnk.ID, lnkRawMaterialPre.ID))
            {
                if (fupRawMaterialStatementPre.HasFile)
                {
                    string strFileName = "RawMaterialPre" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupRawMaterialStatementPre, hdnRawMaterialPre, strFileName, hypRawMaterialPre, lblRawMaterialPre, lnkRawMaterialPreDel, "Production");
                }
            }

               //FOR INVOICE OF RAW MATERIAL POST PRODUCTION
            else if (string.Equals(lnk.ID, lnkRawMaterialPost.ID))
            {
                if (fupRawMaterialStatementPost.HasFile)
                {
                    string strFileName = "RawMaterialPost" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupRawMaterialStatementPost, hdnRawMaterialPost, strFileName, hypRawMaterialPost, lblRawMaterialPost, lnkRawMaterialPostDel, "Production");
                }
            }

            else if (string.Equals(lnk.ID, lnkProductionPost.ID))
            {
                if (fupProductStatementPost.HasFile)
                {
                    string strFileName = "Production" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupProductStatementPost, hdnProductionPost, strFileName, hypProduction, lblProduction, lnkProdcutionDel, "Production");
                }
            }

            else if (string.Equals(lnk.ID, lnkSaleInovice.ID))
            {
                if (fupSaleInvoice.HasFile)
                {
                    string strFileName = "SaleInvoice" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupSaleInvoice, hdnSaleInvoice, strFileName, hypSaleInvoice, lblSaleInvoice, lnkSaleInoviceDel, "Production");
                }
            }
            else if (string.Equals(lnk.ID, lnkFactoryLic.ID))
            {
                if (fupFactoryLic.HasFile)
                {
                    string strFileName = "FactoryLic" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupFactoryLic, hdnFactoryLic, strFileName, hypFactoryLic, lblFactoryLic, lnkFactoryLicDel, "Industry");
                }
            }

            else if (string.Equals(lnk.ID, lnkCompAuthority.ID))
            {
                if (fupComAuthority.HasFile)
                {
                    string strFileName = "CommercialInvest" + System.DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupComAuthority, hdnCompAuthority, strFileName, hypCompAuthority, lblComInvestment, lnkCompAuthorityDel, "Investment");
                }
            }

            //Boiler
            else if (string.Equals(lnk.ID, lnkBoilerAdd.ID))
            {
                if (fupBoiler.HasFile)
                {
                    string[] strExtension = { ".zip" };
                    string strFileName = "FactoryBoiler" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupBoiler, hdnBoiler, strFileName, hypBoilerView, lblBoiler, lnkBoilerDelete, "Production", strExtension, ".zip");
                }
            }

            else if (string.Equals(lnk.ID, lnkProductAdd.ID))
            {
                if (fuProduct.HasFile)
                {
                    string strFileName = "Products" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuProduct, hdnProductfilename, strFileName, hypViewProductFile, lblProductDetails, lnkProductDel, "Production");
                }
            }
            else if (string.Equals(lnk.ID, lnkPCUpload.ID))
            {
                if (fuProductionCertificate.HasFile)
                {
                    string strFileName = "PC" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    string[] strExtension = { ".pdf" };
                    UploadDocument(fuProductionCertificate, hdnProductionCertificate, strFileName, hypProductionCert, lblProdCertificate, lnkPCDelete, "PC", strExtension, "PDF");
                    hdnProductionCertificate.Value = "/incentives/Files/PC/" + strFileName + ".PDF";
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// Common Click function to delete document in the upload controls
    /// It will take the ID of the link button and accordingly call the common function UploadDocument alongwith the controls
    /// that are necessary
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkOrgDocumentDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkOrgDocumentDelete.ID))
            {
                UpdFileRemove(hdnOrgDocument, lnkOrgDocumentPdf, lnkOrgDocumentDelete, hypOrdDocument, lblOrgDocument, fuOrgDocument, "Industry");
            }
            else if (string.Equals(lnk.ID, lnkDocTypeDelete.ID))
            {
                UpdFileRemove(hdnDocumentType, lnkDocTypeUpload, lnkDocTypeDelete, hypDocType, lblDocType, fuDocumentType, "Industry");
            }
            else if (string.Equals(lnk.ID, lnkLandDelete.ID))
            {
                UpdFileRemove(hdnLand, lnkLandAdd, lnkLandDelete, hypLandDelete, lblLand, fuLand, "Investment");
            }
            else if (string.Equals(lnk.ID, lnkPlandDelete.ID))
            {
                UpdFileRemove(hdnPlant, lnkPlantadd, lnkPlandDelete, hypPlant, lblPlant, fuPlant, "Investment");
            }
            else if (string.Equals(lnk.ID, lnkbankDelete.ID))
            {
                UpdFileRemove(hdnBank, lnkbankAdd, lnkbankDelete, hypBank, lblBank, fuBank, "Investment");
            }
            else if (string.Equals(lnk.ID, lnkEmployementDelete.ID))
            {
                UpdFileRemove(hdnEmployement, lnkEmployementAdd, lnkEmployementDelete, hypEmployement, lblEmployement, fuEmployement, "Production");
            }
            else if (string.Equals(lnk.ID, lnkFirstSaleBillDel.ID))
            {
                UpdFileRemove(hdnFistSaleBill, lnkFirstSalBillAdd, lnkFirstSaleBillDel, hypFirstSaleBill, lblFirstSaleBill, fuFirstSaleBill, "Investment");
            }
            else if (string.Equals(lnk.ID, lnkPowerDel.ID))
            {
                UpdFileRemove(hdnPower, lnkPowerAdd, lnkPowerDel, hypPower, lblPower, fuPower, "Production");
            }
            else if (string.Equals(lnk.ID, lnkProjectDel.ID))
            {
                UpdFileRemove(hdnProject, lnkProjectAdd, lnkProjectDel, hypProject, lblProject, fuProject, "Production");
            }
            else if (string.Equals(lnk.ID, lnkClearenceDel.ID))
            {
                UpdFileRemove(hdnClearence, lnkCLearenceAdd, lnkClearenceDel, hypCLearence, lblClearence, fuClearence, "Production");
            }
            else if (string.Equals(lnk.ID, lnkOwnerCatDel.ID))
            {
                UpdFileRemove(hdnOwnerCategory, lnkOnwerCatAdd, lnkOwnerCatDel, hypOwnerCategory, lblOwnerCategoryFile, fuOwnerCategory, "Production");
            }
            else if (string.Equals(lnk.ID, lnkVATDel.ID))
            {
                UpdFileRemove(hdnVAT, lnkVATPDF, lnkVATDel, hypVAT, lblVAT, fupVAT, "Industry");
            }
            else if (string.Equals(lnk.ID, lnkIEMDelete.ID))
            {
                UpdFileRemove(hdnIEM, lnkIEM, lnkIEMDelete, hypIEMDelete, lblIEM, fupIEM, "Industry");
            }
            else if (string.Equals(lnk.ID, lnkBuildValdel.ID))
            {
                UpdFileRemove(hdnBuildingValuation, lnkBuildVal, lnkBuildValdel, hypBuildVal, lblBuildVal, fupBuildingValReport, "Investment");
            }
            else if (string.Equals(lnk.ID, lnkAgreementDel.ID))
            {
                UpdFileRemove(hdnAgreement, lnkAgreement, lnkAgreementDel, hypAgreement, lblAgreement, fupAgreement, "Power");
            }

            else if (string.Equals(lnk.ID, lnkInvoiceDel.ID))
            {
                UpdFileRemove(hdnInovice, lnkInovice, lnkInvoiceDel, hypInovice, lblProductFile, fupInvoice, "Production");
            }
            else if (string.Equals(lnk.ID, lnkRawMaterialPreDel.ID))
            {
                UpdFileRemove(hdnRawMaterialPre, lnkRawMaterialPre, lnkRawMaterialPreDel, hypRawMaterialPre, lblRawMaterialPre, fupRawMaterialStatementPre, "Production");
            }
            //FOR INVOICE OF RAW MATERIAL POST PRODUCTION
            else if (string.Equals(lnk.ID, lnkRawMaterialPostDel.ID))
            {
                UpdFileRemove(hdnRawMaterialPost, lnkRawMaterialPost, lnkRawMaterialPostDel, hypRawMaterialPost, lblRawMaterialPost, fupRawMaterialStatementPost, "Production");
            }

            else if (string.Equals(lnk.ID, lnkProdcutionDel.ID))
            {
                UpdFileRemove(hdnProductionPost, lnkProductionPost, lnkProdcutionDel, hypProduction, lblProduction, fupProductStatementPost, "Production");
            }

            else if (string.Equals(lnk.ID, lnkSaleInoviceDel.ID))
            {
                UpdFileRemove(hdnSaleInvoice, lnkSaleInovice, lnkSaleInoviceDel, hypSaleInvoice, lblSaleInvoice, fupSaleInvoice, "Production");
            }
            else if (string.Equals(lnk.ID, lnkFactoryLicDel.ID))
            {
                UpdFileRemove(hdnFactoryLic, lnkFactoryLic, lnkFactoryLicDel, hypFactoryLic, lblFactoryLic, fupFactoryLic, "Industry");
            }

            else if (string.Equals(lnk.ID, lnkCompAuthorityDel.ID))
            {
                UpdFileRemove(hdnCompAuthority, lnkCompAuthority, lnkCompAuthorityDel, hypCompAuthority, lblComInvestment, fupComAuthority, "Investment");
            }
            else if (string.Equals(lnk.ID, lnkBoilerDelete.ID))
            {
                UpdFileRemove(hdnBoiler, lnkBoilerAdd, lnkBoilerDelete, hypBoilerView, lblBoiler, fupBoiler, "Production");
            }
            else if (string.Equals(lnk.ID, lnkProductDel.ID))
            {
                UpdFileRemove(hdnProductfilename, lnkProductAdd, lnkProductDel, hypViewProductFile, lblProductDetails, fuProduct, "Production");
            }
            else if (string.Equals(lnk.ID, lnkPCDelete.ID))
            {
                UpdFileRemove(hdnProductionCertificate, lnkPCUpload, lnkPCDelete, hypProductionCert, lblProdCertificate, fuProductionCertificate, "PC");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }

    /// <summary>
    /// Click event the add button of the products table. It will get the value from the controls and then add
    /// the product in the product datatable present in viewState. Then it will rebind the gridview for products 
    /// with the same
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkAdd_ClicK(object sender, EventArgs e)
    {
        try
        {
            DataTable dtProducts = null;
            DataRow dRow = null;
            if (ViewState["Products"] != null)
            {
                dtProducts = (DataTable)ViewState["Products"];
                if (!dtProducts.Columns["id"].AutoIncrement)
                {
                    dtProducts.Columns["id"].AutoIncrement = true;
                }
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
            dRow["UnitOthers"] = txtUnitType.Text;
            dRow["Cost"] = txtCost.Text;
            dRow["bitMainProduct"] = chkMainCategory.Checked ? 1 : 0;
            dRow["VchIsMainProduct"] = chkMainCategory.Checked ? "Yes" : "No";
            dRow["dtmProd"] = txtDateOfProd.Text;
            dtProducts.Rows.Add(dRow);
            ViewState["Products"] = dtProducts;
            BindProductGridview(dtProducts);
            txtItemProduct.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            drpUnitType.SelectedIndex = 0;
            txtUnitType.Text = string.Empty;
            txtUnitType.Visible = false;
            txtCost.Text = string.Empty;
            txtDateOfProd.Text = string.Empty;
            chkMainCategory.Checked = false;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// Click event to add new plant and machinery records
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lnkMachinery_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtMachinery = null;
            DataRow dRow = null;
            if (ViewState["Machinery"] != null)
            {
                dtMachinery = (DataTable)ViewState["Machinery"];
                if (!dtMachinery.Columns["id"].AutoIncrement)
                {
                    dtMachinery.Columns["id"].AutoIncrement = true;
                }
            }
            else
            {
                dtMachinery = CreateMachineryTable();
            }
            dRow = dtMachinery.NewRow();
            dRow["MachineryName"] = txtMachinery.Text.Trim();
            dRow["DateofPurchase"] = txtDateofPurchase.Text;
            dRow["Cost"] = txtAmt.Text.Trim();
            dtMachinery.Rows.Add(dRow);
            ViewState["Machinery"] = dtMachinery;
            BindMachineryGridview(dtMachinery);
            txtMachinery.Text = string.Empty;
            txtDateofPurchase.Text = string.Empty;
            txtAmt.Text = string.Empty;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
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
        try
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// rowdatabound event for the grdProducts gridview. It will check if the user has selected others option 
    /// then set the value for unit column to the hdnUnit hiddenfield value
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Rowcommand event for Plant gridview
    /// It will add new products and delete old Plant records
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPlant_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// rowdatabound event for the grdProductsold gridview. It will check if the user has selected others option 
    /// then set the value for unit column to the hdnUnit hiddenfield value
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdProductsold_RowDataBound(object sender, GridViewRowEventArgs e)
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

    #region Common functions for page
    /// <summary>
    /// function to get the investort details from peal
    /// </summary>
    private void GetPealDetails()
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
                txtGST.Text = dRow["vchGSTIN"].ToString();
                if (txtGST.Text != "")
                {
                    txtGST.Enabled = false;
                }
                lblIndustryCode.Text = dRow["vchIndustryCode"].ToString() == "" ? "NA" : dRow["vchIndustryCode"].ToString();
                int intApplicationType = dRow["intapplicationfor"] == null || dRow["intapplicationfor"] == "" ? 0 : Convert.ToInt32(dRow["intapplicationfor"].ToString());
                //no peal and pc then show all options
                if (intApplicationType == 0)
                {
                    lblPcTypeDetails.Text = string.Empty;
                    rdBtnApplicationFor.Items.Clear();
                    rdBtnApplicationFor.Items.Add(new ListItem("New", "1"));
                    rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
                    if (Request.QueryString["offline"] != null)
                    {
                        btnApply.Text = "Submit";
                        divUploadPc.Visible = true;
                        if (Request.QueryString["offline"] == "1")  // if the user is applying for offline pc, new or existing
                        {
                            lblPcMessage.Visible = true;
                            rdBtnApplicationFor.Items[0].Selected = true;
                            objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
                            {
                                Action = "anew",
                                Name = string.Empty,
                                ID = string.Empty,
                                Param = 0,
                                Param_1 = 0,
                                Param_2 = string.Empty,
                                Param_3 = string.Empty
                            });
                            btnSaveAsDraft.Visible = false;
                        }

                        //if the user is entering details for the details of the pc for exising unit before policy effective date
                        //i.e user was applying for existing with amendement and he is first filling up all details for the existing pc
                        //both in case of offline pc
                        else if ((Request.QueryString["offline"] == "2"))
                        {
                            lblPcTypeDetails.Text = "&nbsp;(Please enter details of original unit i.e. details of unit before policy effective date)";
                            rdBtnApplicationFor.Items.Clear();
                            rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
                            rdBtnApplicationFor.Items[0].Selected = true;
                            drpApplicationType.Items.Clear();
                            drpApplicationType.Items.Add(new ListItem("Existing", ((int)enAppFor.exist).ToString()));
                            drpApplicationType.SelectedIndex = 0;
                            divChangeIn.Visible = false;
                            btnSaveAsDraft.Visible = false;
                            lblPcMessage.Visible = false;
                        }

                        else //normal setting or default settings
                        {
                            rdBtnApplicationFor.Items[0].Selected = true;
                            objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
                            {
                                Action = "anew",
                                Name = string.Empty,
                                ID = string.Empty,
                                Param = 0,
                                Param_1 = 0,
                                Param_2 = string.Empty,
                                Param_3 = string.Empty
                            });
                        }
                    }

                         //if the user is entering details for the details of the pc for exising unit before policy effective date
                    //i.e user was applying for existing with amendement and he is first filling up all details for the existing pc
                    // in case  user with no peal or pc
                    else if ((Request.QueryString["uType"] == "2"))
                    {
                        hdnOfflineStatus.Value = "1";
                        lblPcMessage.Visible = true;
                        divUploadPc.Visible = true;
                        rdBtnApplicationFor.Items.Clear();
                        rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
                        rdBtnApplicationFor.Items[0].Selected = true;
                        drpApplicationType.Items.Clear();
                        drpApplicationType.Items.Add(new ListItem("Existing", ((int)enAppFor.exist).ToString()));
                        drpApplicationType.SelectedIndex = 0;
                        divChangeIn.Visible = false;
                        btnSaveAsDraft.Visible = false;
                        btnApply.Text = "Submit";
                    }

                    //if the user is entering details for the details of the pc for new unit 
                    //i.e user was applying for new unit with amendement and he is first filling up all details for the new pc
                    // in case  user with no peal or pc
                    else if ((Request.QueryString["uType"] == "3"))
                    {
                        hdnOfflineStatus.Value = "1";
                        lblPcMessage.Visible = true;
                        divUploadPc.Visible = true;
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
                        divChangeIn.Visible = false;
                        btnSaveAsDraft.Visible = false;
                        btnApply.Text = "Submit";
                    }
                    else //normal setting or default settings for no peal and pc and apply online
                    {
                        rdBtnApplicationFor.Items[0].Selected = true;
                        objBuisnessLayer.BindDropdown(drpApplicationType, new IncentiveMaster()
                        {
                            Action = "anew",
                            Name = string.Empty,
                            ID = string.Empty,
                            Param = 0,
                            Param_1 = 0,
                            Param_2 = string.Empty,
                            Param_3 = string.Empty
                        });
                    }
                }
                else if (intApplicationType == (int)enAppFor.exist) // if existing was selected in peal only existing will be shown
                {
                    rdBtnApplicationFor.Items.Clear();
                    rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
                    rdBtnApplicationFor.SelectedValue = "2";
                    drpApplicationType.Items.Clear();
                    drpApplicationType.Items.Add(new ListItem("Existing", ((int)enAppFor.exist).ToString()));
                    drpApplicationType.SelectedIndex = 0;
                    divChangeIn.Visible = false;
                }
                else if (intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod) //if existing amd
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
                else if (intApplicationType == (int)enAppFor.New) //if new
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
                if (txtEin.Text != "")
                {
                    txtEin.Enabled = false;
                }
                else if (intEin == 4)
                {
                    txtUan.Text = dRow["vchEINnIEMnIL"].ToString();
                }
                if (txtUan.Text != "")
                {
                    txtUan.Enabled = false;
                }
                ddlSector.SelectedValue = dRow["intSectorId"].ToString();
                BindSubSector(ddlSector.SelectedValue);
                ddlSubSector.SelectedValue = dRow["intSubSectorId"].ToString();
                drpSalutation.SelectedValue = dRow["INT_SALUTATION"].ToString();
                txtOfficeAddress.Text = dRow["vchCorAdd"].ToString();
                txtOfficeEmail.Text = dRow["vchCorEmail"].ToString();
                txtOfficeFax.Text = dRow["vchCorFaxNo"].ToString();
                txtOfficePhone.Text = dRow["vchCorMobileNo"].ToString();
                txtEnterpriseName.Text = dRow["vchNameOfUnit"].ToString();
                drpOrganizationType.SelectedValue = dRow["organizationType"].ToString();
                txtland.Text = dRow["decLandIncLandDev"].ToString();
                txtBuilding.Text = dRow["decBuildingndConstruction"].ToString();
                if (Session["InvAmt"] != null)
                    txtPlantMachinery.Text = Session["InvAmt"].ToString();
                else
                    txtPlantMachinery.Text = dRow["decPlantndMachinery"].ToString();
                txtOthers.Text = dRow["decOthers"].ToString();
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
                    drpUnitCategory.SelectedValue = strUnitType;
                }
                else
                {
                    drpUnitCategory.SelectedValue = "0";
                }
                hdnPeal.Value = dRow["inv"].ToString();
            }
        }
    }

    /// <summary>
    /// Function to delete all the service document saved in temp folder and create the new ones in pc folder
    /// </summary>
    /// <param name="aEnserviceDocType">service doc type</param>
    /// <param name="hdnFileName">hiddenfield that has the file name</param>
    private void DeleteServiceOldDoc(enServiceDocType aEnserviceDocType, HiddenField hdnFileName)
    {
        string strPreFix = string.Empty;
        if (aEnserviceDocType == enServiceDocType.Boiler)
        {
            strPreFix = "FactoryBoiler";
        }
        else if (aEnserviceDocType == enServiceDocType.OSPCB)
        {
            strPreFix = "OSPCB";
        }
        string strSourceFile = Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}.zip", strPreFix, Session["investorId"].ToString()));
        if (File.Exists(strSourceFile))
        {
            string strDestinationFile = Server.MapPath(string.Format("~/incentives/Files/Production/{0}", hdnFileName.Value));
            File.Copy(strSourceFile, strDestinationFile, true);

            //then delete the old folder and old zip folder
            File.Delete(Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}.zip", strPreFix, Session["investorId"].ToString())));
            Directory.Delete(Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}", strPreFix, Session["investorId"].ToString())), true);
        }
    }

    /// <summary>
    /// Functiion to get certificate details for OSPCB/Factory and boiler from service
    /// </summary>
    /// <param name="aEnServiceDocType">Service Doc Type i.e whether boiler or factory</param>
    /// <param name="hdnDocValue">name of hidden field in which the document filename will be stored</param>
    /// <param name="fuUpload">fileupload control for the document to disable them in case service has document</param>
    /// <param name="lnkAdd">add linkbutton for the document to disable them in case service has document</param>
    /// <param name="lnkDel">delete linkbutton for the document to disable them in case service has document</param>
    /// <param name="hyp">Hyperlink to view/download the document</param>
    /// <param name="hdnServiceDocStatus">hidden field to store the status as to whether doc is present or not</param>
    private void GetCertificateDetailsFromService(enServiceDocType aEnServiceDocType, HiddenField hdnDocValue, FileUpload fuUpload, LinkButton lnkAdd, LinkButton lnkDel, HyperLink hyp, HiddenField hdnServiceDocStatus)
    {
        //first send the investorid to database and get all the records for documents
        InctSearch objSearch = new InctSearch()
        {
            intUserUnitType = Convert.ToInt32(Session["investorid"]),
            strActionCode = "view",
            intUnitType = (int)aEnServiceDocType
        };

        List<string> lstFiles = new List<string>();
        IncentiveMasterBusinessLayer objInctBuisnessLayer = new IncentiveMasterBusinessLayer();
        lstFiles = objInctBuisnessLayer.ViewInctOSPCBDetails(objSearch);

        string strTempFilePath = IncentiveCommonFunctions.GetCertificateDetailsFromService(aEnServiceDocType, lstFiles, Convert.ToInt32(Session["investorid"]));

        if (!string.IsNullOrEmpty(strTempFilePath))
        {
            //set hidden field value
            hdnDocValue.Value = string.Format("{0}.zip", Session["investorId"].ToString());

            //disable the file upload control
            fuUpload.Enabled = false;
            lnkAdd.Visible = false;

            //remove the delete button
            lnkDel.Visible = false;
            hyp.Visible = true;
            hyp.NavigateUrl = strTempFilePath;
            hdnServiceDocStatus.Value = "1";
        }
    }

    /// <summary>
    /// function to get a new application code
    /// </summary>
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
        objDs = objBuisnessLayer.Incentive_PcForm_Large_View(objSearch);
        if (objDs != null)
        {
            DataTable dt = objDs.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                lblAppNo.Text = dt.Rows[0]["vchFormattedNo"].ToString();
            }
        }
        //check the details for the OSPCB Certificate

        //if it is normal apply it will not have offline 
        if (string.IsNullOrEmpty(Request.QueryString["offline"]))
        {
            //if it is no peal and pc check if it is 1, if it is from peal it will not have utype 
            if (string.IsNullOrEmpty(Request.QueryString["uType"]) || (!string.IsNullOrEmpty(Request.QueryString["uType"]) && Request.QueryString["uType"] == "1"))
            {
                GetCertificateDetailsFromService(enServiceDocType.OSPCB, hdnClearence, fuClearence, lnkCLearenceAdd, lnkClearenceDel, hypCLearence, hdnIsOsPCBDownloaded);
                GetCertificateDetailsFromService(enServiceDocType.Boiler, hdnBoiler, fupBoiler, lnkBoilerAdd, lnkBoilerDelete, hypBoilerView, hdnBoilderDownloaded);
            }
            else
            {
                SetDefaultForServiceDocument();
            }
        }
        else
        {
            SetDefaultForServiceDocument();
        }
    }

    private void SetDefaultForServiceDocument()
    {
        lnkCLearenceAdd.Visible = true;
        lnkCLearenceAdd.Enabled = true;
        lnkClearenceDel.Visible = false;
        hypCLearence.Visible = false;
        hypCLearence.NavigateUrl = string.Empty;
        hdnClearence.Value = string.Empty;
        fuClearence.Enabled = true;
        hdnIsOsPCBDownloaded.Value = "0";

        lnkBoilerAdd.Visible = true;
        lnkBoilerAdd.Enabled = true;
        lnkBoilerDelete.Visible = false;
        hypBoilerView.Visible = false;
        hypBoilerView.NavigateUrl = string.Empty;
        hdnBoiler.Value = string.Empty;
        fupBoiler.Enabled = true;
        hdnBoilderDownloaded.Value = "0";
    }


    /// <summary>
    /// Function to show the onwer type, onwer type document, organization type document based on the constiution of organization selected bu the user
    /// </summary>
    private void ShowDetailsByConstitutionOfOrg()
    {
        lblOwnerLabel.Text = string.Empty;
        int intOrgType = Convert.ToInt32(drpOrganizationType.SelectedValue);
        string strType = drpOrganizationType.SelectedItem.Text.ToLower();
        string strOwnerType = "Name of Managing member";
        string strOrgdocumentType = "Relevant Document";
        lblOrgTypeDoc.Text = string.Empty;
        string strDocumentType = "";
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
                if (!string.IsNullOrEmpty(dt.Rows[0]["vchDocumentType"].ToString()))
                {
                    rdBtnOwnerTYpe.Items.Clear();
                    rdBtnOwnerTYpe.Items.Add(new ListItem(dt.Rows[0]["vchDocumentTypename"].ToString(), dt.Rows[0]["vchDocumentType"].ToString()));
                    rdBtnOwnerTYpe.SelectedValue = dt.Rows[0]["vchDocumentType"].ToString();
                    strDocumentType = dt.Rows[0]["vchDocumentTypename"].ToString();
                }
                if (!string.IsNullOrEmpty(dt.Rows[0]["vch_OrgDocument"].ToString()))
                {
                    rdBtnOrg.Items.Clear();
                    rdBtnOrg.Items.Add(new ListItem(dt.Rows[0]["vch_OrgDocumentname"].ToString(), dt.Rows[0]["vch_OrgDocument"].ToString()));
                    rdBtnOrg.SelectedValue = dt.Rows[0]["vch_OrgDocument"].ToString();
                    strOrgdocumentType = dt.Rows[0]["vch_OrgDocumentname"].ToString();
                }
            }

            if (intOrgType == 24)
            {
                txtOtherOrg.Visible = true;
            }



        }
        lblOwnerLabel.Text = strOwnerType;
        lblDocumentType.Text = strDocumentType;
        lblOrgTypeDoc.Text = strOrgdocumentType;
    }

    /// <summary>
    /// bind sub sector details by Sector 
    /// </summary>
    /// <param name="strSectorId">sub sector Id</param>
    private void BindSubSector(string strSectorId)
    {
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        objBuisnessLayer.BindDropdown(ddlSubSector, new IncentiveMaster()
        {
            Action = "sub",
            Param = 0,
            Param_1 = 0,
            Param_2 = strSectorId,
            Param_3 = string.Empty
        });
    }

    /// <summary>
    /// Bind block details by the district
    /// </summary>
    /// <param name="strdist"></param>
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
    /// Function to delete the uploaded file
    /// </summary>
    /// <param name="hdnFile">Hidden field that contains the file name</param>
    /// <param name="lnkBtn">Add LinkButton</param>
    /// <param name="lnkDel">delete link button</param>
    /// <param name="hplnk">Hyperlink to view uploaded file</param>
    /// <param name="lblFile">label to show if document uploaded successfully</param>
    /// <param name="updFile">File Upload control</param>
    /// <param name="strFolername">Folder name where file is present</param>
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string strFolername)
    {
        string filename = hdnFile.Value;
        string path = string.Format("~/incentives/Files/{0}/{1}", strFolername, filename);
        string completePath = Server.MapPath(path);
        if (File.Exists(completePath))
        {
            //  File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
        else
        {

        }
    }

    /// <summary>
    /// Function to upload the document
    /// </summary>
    /// <param name="fuOrgDocument">fileupload control</param>
    /// <param name="hdnOrgDocument">hidden field with the file name</param>
    /// <param name="strFileName">file name to be set</param>
    /// <param name="hypOrdDocument">hyperlink to view the file</param>
    /// <param name="lblOrgDocument">label to show if document uploaded successfully</param>
    /// <param name="lnkOrgDocumentDelete">delete LinkButton</param>
    /// <param name="strFoldername">Folder name where file is to be stored</param>
    private void UploadDocument(FileUpload fuOrgDocument, HiddenField hdnOrgDocument, string strFileName, HyperLink hypOrdDocument, Label lblOrgDocument, LinkButton lnkOrgDocumentDelete, string strFoldername, string[] strExtension = null, string strFileTypeAllowed = null)
    {
        string[] arrExtension = { ".pdf", ".jpg", ".jpeg", ".png" };
        string strFileAllowed = "PDF, PNG, JPG, JPEG";
        if (strExtension != null)
        {
            arrExtension = strExtension;
        }
        if (!string.IsNullOrEmpty(strFileTypeAllowed))
        {
            strFileAllowed = strFileTypeAllowed;
        }
        if (fuOrgDocument.HasFile)
        {
            string filename = string.Empty;
            string fileExtension = Path.GetExtension(fuOrgDocument.FileName);
            int fileSize = fuOrgDocument.PostedFile.ContentLength;
            string str = string.Empty;
            if (!arrExtension.Contains(fileExtension))
            {
                str = "jAlert('<strong>Please Upload  " + strFileAllowed + " Only!</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuOrgDocument.ID + "').focus(); });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else if (fileSize > (4 * 1024 * 1024))
            {
                str = "jAlert('<strong>File size is too large. Maximum file size permitted is 4 MB</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuOrgDocument.ID + "').focus(); });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else if (!IncentiveCommonFunctions.IsFileValid(fuOrgDocument, arrExtension))
            {
                str = "jAlert('<strong>Invalid file type (or) File name might contain dots</strong>', 'GO-SWIFT'); $('#popup_ok').click(function () { $('#" + fuOrgDocument.ID + "').focus(); });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", str, true);
            }
            else
            {
                string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFoldername));
                if (!Directory.Exists(strMainFolderPath))
                {
                    Directory.CreateDirectory(strMainFolderPath);
                }
                filename = strFileName + Path.GetExtension(fuOrgDocument.FileName);
                fuOrgDocument.SaveAs(strMainFolderPath + filename);
                hdnOrgDocument.Value = filename;
                hypOrdDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFoldername, filename);
                hypOrdDocument.Visible = true;
                lnkOrgDocumentDelete.Visible = true;
                lblOrgDocument.Visible = true;
                fuOrgDocument.Enabled = false;
            }
        }

    }

    /// <summary>
    /// Common function to get all the datatable values from database to bind to gridview
    /// </summary>
    private void BindDropDown()
    {
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        DataSet objDa = objBuisnessLayer.BindDropdown("ddlL");
        if (objDa != null)
        {
            FillDropDown(objDa.Tables[0], "Application for", drpApplicationType);
            FillCheckBoxList(objDa.Tables[1], chkLstChange);
            FillDropDown(objDa.Tables[2], "Organization Type", drpOrganizationType);
            FillDropDown(objDa.Tables[3], "Ownership Pattern", drpOwnerType);
            FillDropDown(objDa.Tables[4], "Unit Category", drpUnitCategory);
            FillDropDown(objDa.Tables[5], "Nature of Activity", drpCompanyType);
            FillDropDown(objDa.Tables[6], "Change In", drpChangeIn);
            FillDropDown(objDa.Tables[10], "Unit Type", drpUnitType);
            FillDropDown(objDa.Tables[11], "District", ddlDistrict);
            FillDropDown(objDa.Tables[12], "Sector", ddlSector);
            FillDropDown(objDa.Tables[13], "", ddlCode);
            FillDropDown(objDa.Tables[13], "", drpFx);
            FillDropDown(objDa.Tables[13], "", drpEntCode);
            FillDropDown(objDa.Tables[13], "", drpEnterpriseFax);
            FillDropDown(objDa.Tables[14], "", drpSalutation);
            drpFx.SelectedValue = "1";
            ddlCode.SelectedValue = "1";
            drpEntCode.SelectedValue = "1";
            drpEnterpriseFax.SelectedValue = "1";
            DataTable dtPolicyDate = new DataTable();
            dtPolicyDate = objDa.Tables[16];
            if (dtPolicyDate != null && dtPolicyDate.Rows.Count > 0)
            {
                if (dtPolicyDate.Rows[0]["dtmEffectiveDate"] != DBNull.Value && dtPolicyDate.Rows[0]["dtmEffectiveDate"] != null)
                {
                    hdnPolicyEffectiveDate.Value = dtPolicyDate.Rows[0]["dtmEffectiveDate"].ToString();
                }
            }
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

    /// <summary>
    /// function to bind all the radio button the document upload
    /// Also set the value of the document is in different hidden field
    /// </summary>
    private void BindRadioButton()
    {
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        DataSet objDa = objBuisnessLayer.BindDropdown("grpl");
        if (objDa != null && objDa.Tables.Count > 0)
        {
            FillRadioButton(rdBtnOwnerTYpe, objDa.Tables[0]);
            FillRadioButton(rdBtnOrg, objDa.Tables[1]);
            rdBtnProject.Value = objDa.Tables[2].Rows[0]["vchDocId"].ToString();
            rdBtnClearence.Value = objDa.Tables[3].Rows[0]["vchDocId"].ToString();
            FillRadioButton(rdBtnLand, objDa.Tables[4]);
            FillRadioButton(rdBtnPlant, objDa.Tables[5]);
            FillRadioButton(rdBtnPower, objDa.Tables[6]);
            FillRadioButton(rdBtnRawMaterial, objDa.Tables[7]);
            rdBtnEmployement.Value = objDa.Tables[9].Rows[0]["vchDocId"].ToString();
            FillRadioButton(rdBtnOwnerCategory, objDa.Tables[11]);

            DataView dv = new DataView(objDa.Tables[12]);

            //for agreement doc
            dv.RowFilter = "INT_GROUP_ID=21";
            hdnAgreementDocId.Value = dv[0]["vchDocId"].ToString();
            //for IEM document
            dv.RowFilter = "INT_GROUP_ID=20";
            hdnIEMDoc.Value = dv[0]["vchDocId"].ToString();

            //for GST
            dv.RowFilter = "INT_GROUP_ID=25";
            hdnGSTDocId.Value = dv[0]["vchDocId"].ToString();

            //for Sale Invoice document
            dv.RowFilter = " INT_GROUP_ID=18";
            hdnSaleInvoiceDocId.Value = dv[0]["vchDocId"].ToString();

            //for Sale Invoice document last 3 month
            dv.RowFilter = "vchDocId='D261'";
            hdnFistSaleBillDocId.Value = dv[0]["vchDocId"].ToString();

            //for production document last 3 month
            dv.RowFilter = "vchDocId='D260'";
            hdnProductiondocId.Value = dv[0]["vchDocId"].ToString();

            //for production document last 3 month
            dv.RowFilter = "vchDocId='D259'";
            hdnRawMaterialPostDocId.Value = dv[0]["vchDocId"].ToString();

            //for production document last 3 month
            dv.RowFilter = "vchDocId='D258'";
            hdnRawMaterialPreDocId.Value = dv[0]["vchDocId"].ToString();

            //for production document last 3 month
            dv.RowFilter = "vchDocId='D257'";
            hdnInvoiceDocId.Value = dv[0]["vchDocId"].ToString();

            //for factory license
            dv.RowFilter = "vchDocId='D214'";
            hdnFactoryDoc.Value = dv[0]["vchDocId"].ToString();

            //for COMPANY INVESTMENT
            dv.RowFilter = "vchDocId='D213'";
            hdnCompAuthorityDocId.Value = dv[0]["vchDocId"].ToString();

            //for BANK PROPOSAL
            dv.RowFilter = "vchDocId='D204'";
            hdnBankAppDocId.Value = dv[0]["vchDocId"].ToString();

            dv.RowFilter = "vchDocId='D207'";
            hdnBuildDocId.Value = dv[0]["vchDocId"].ToString();

            //Boiler document
            dv.RowFilter = "vchDocId='D280'";
            hdnBoilerDocId.Value = dv[0]["vchDocId"].ToString();
        }
    }

    /// <summary>
    /// Common function with the code to actually bind the radiobuttonlist with the values for document
    /// </summary>
    /// <param name="rdBtnLst">radiobuttonlist to bind</param>
    /// <param name="dtView">datatable to bind to radiobuttonlist</param>
    private void FillRadioButton(RadioButtonList rdBtnLst, DataTable dtView)
    {
        rdBtnLst.DataSource = dtView;
        rdBtnLst.DataTextField = "vchDocName";
        rdBtnLst.DataValueField = "vchDocId";
        rdBtnLst.DataBind();
        if (rdBtnLst.Items.Count == 1)
        {
            rdBtnLst.Items[0].Selected = true;
            rdBtnLst.Visible = false;
        }
    }

    /// <summary>
    /// Function is called when drpOwnerType is changed. It will check if the user has selected 
    /// SC, ST, OBC, SC-women, st-women, obc-women then show controls to upload document for the same. 
    /// Also set the correct document type in the radiobuttonlist. 
    /// Else it will hide the complete div with controls altogether
    /// </summary>
    private void SetOwnerCategoryDocByType()
    {
        divOwnerCategoryDoc.Visible = false;
        if (drpOwnerType.SelectedIndex > 0)
        {
            int intOwnerType = Convert.ToInt32(drpOwnerType.SelectedValue);
            if (intOwnerType == (int)enOwnerType.GEN || intOwnerType == (int)enOwnerType.GEN_WOMEN)
            {
                rdBtnOwnerCategory.Enabled = false;
                for (int i = 0; i < rdBtnOwnerCategory.Items.Count; i++)
                {
                    if (rdBtnOwnerCategory.Items[i].Selected)
                    {
                        rdBtnOwnerCategory.Items[i].Selected = false;

                    }
                    hdnOwnerCategory.Value = string.Empty;
                }
            }
            else
            {
                divOwnerCategoryDoc.Visible = true;
                if (intOwnerType == (int)enOwnerType.SC || intOwnerType == (int)enOwnerType.SC_WOMEN)
                {
                    rdBtnOwnerCategory.Enabled = false;
                    rdBtnOwnerCategory.SelectedValue = clsOwnerTypeDoc.StrOwnerDocSc;
                }
                else if (intOwnerType == (int)enOwnerType.ST || intOwnerType == (int)enOwnerType.ST_WOMEN)
                {
                    rdBtnOwnerCategory.Enabled = false;
                    rdBtnOwnerCategory.SelectedValue = clsOwnerTypeDoc.StrOwnerDocST;
                }
                else if (intOwnerType == (int)enOwnerType.OBC || intOwnerType == (int)enOwnerType.OBC_WOMEN)
                {
                    rdBtnOwnerCategory.Enabled = false;
                    rdBtnOwnerCategory.SelectedValue = clsOwnerTypeDoc.StrOwnerDocOBC;
                }
            }
            for (int i = 0; i < rdBtnOwnerCategory.Items.Count; i++)
            {
                if (rdBtnOwnerCategory.Items[i].Selected == false)
                {
                    rdBtnOwnerCategory.Items[i].Attributes.Add("style", "display:none");

                }
            }
        }
    }

    /// <summary>
    /// Function to enable and disable different panel of page based on the checkbox checked in chkLstChange. 
    /// </summary>
    private void EnableDisablePanelByCheckBox()
    {
        pnlAdditional.Enabled = false;
        pnlIndOthers.Enabled = false;
        pnlSector.Enabled = false;
        pnlPlantMachinery.Enabled = false;
        pnlInvestment.Enabled = false;
        pnlLocation.Enabled = false;
        pnlname.Enabled = false;
        pnlProduction.Enabled = false;
        pnlOrgType.Enabled = false;
        pnlRedOff.Enabled = false;
        divChangeIn.Enabled = true;
        pnlApp.Enabled = false;
        pnlEmployement.Enabled = false;
        pnlPower.Enabled = false;

        //check if emd has been selected by default then donot change the state of the checkboc
        Boolean isEmd = false;
        for (int cnt = 0; cnt < chkLstChange.Items.Count; cnt++)
        {
            ListItem curr = chkLstChange.Items[cnt];
            int id = Convert.ToInt32(curr.Value);
            if (id == (int)enAmdType.EMD && curr.Selected)
            {
                isEmd = true;
                break;
            }
        }
        if (!isEmd)
        {
            chkPlant.Checked = false;
            chkPlant.Visible = false;
            chkProductsAmd.Checked = false;
            chkProductsAmd.Visible = false;
        }
        int intApplicationType = Convert.ToInt32(drpApplicationType.SelectedValue);
        if (intApplicationType == (int)enAppFor.New_EMD || intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod)
        {
            for (int cnt = 0; cnt < chkLstChange.Items.Count; cnt++)
            {
                ListItem curr = chkLstChange.Items[cnt];
                int id = Convert.ToInt32(curr.Value);
                if (curr.Selected)
                {
                    switch (id)
                    {
                        case (int)enAmdType.Name:
                            pnlname.Enabled = true;
                            break;
                        case (int)enAmdType.Org:
                            pnlOrgType.Enabled = true;
                            break;
                        case (int)enAmdType.RegdOff:
                            pnlRedOff.Enabled = true;
                            break;
                        case (int)enAmdType.Location:
                            pnlLocation.Enabled = true;
                            break;
                        case (int)enAmdType.EMD:
                            pnlSector.Enabled = true;
                            pnlPower.Enabled = true;
                            pnlInvestment.Enabled = true;
                            pnlAdditional.Enabled = true;
                            chkPlant.Visible = true;
                            chkProductsAmd.Visible = true;
                            pnlEmployement.Enabled = true;
                            break;
                        default:
                            break;
                    }
                    string strChangeIn = hdnChangeIn.Value;
                    if (string.IsNullOrEmpty(hdnChangeIn.Value))
                    {
                        hdnChangeIn.Value = string.Format("{0}", id.ToString());
                    }
                    else
                    {
                        hdnChangeIn.Value = string.Format("{0},{1}", hdnChangeIn.Value, id.ToString());
                    }
                }
            }
        }
    }

    /// <summary>
    /// Function to set the default value for the panel in case of amendement
    /// </summary>
    private void DisableInAmd()
    {
        pnlAdditional.Enabled = false;
        pnlIndOthers.Enabled = false;
        pnlSector.Enabled = false;
        pnlPlantMachinery.Enabled = false;
        pnlInvestment.Enabled = false;
        pnlEmployement.Enabled = false;
        pnlLocation.Enabled = false;
        pnlname.Enabled = false;
        pnlProduction.Enabled = false;
        pnlOrgType.Enabled = false;
        pnlRedOff.Enabled = false;
        divChangeIn.Enabled = true;
        pnlApp.Enabled = false;
        pnlPower.Enabled = false;
        chkPlant.Checked = false;
        chkPlant.Visible = false;
        chkProductsAmd.Checked = false;
        chkProductsAmd.Visible = false;
    }

    /// <summary>
    /// function to set the value in the file upload controls
    /// </summary>
    /// <param name="objDt">Datatable with the value</param>
    /// <param name="objHyp">hyperlink to view file</param>
    /// <param name="lnkAdd">add linkbutton</param>
    /// <param name="lnkDelete">delete linkbuton</param>
    /// <param name="fileUpload">file upload</param>
    /// <param name="objhiddenField">hidden field to set filename</param>
    /// <param name="objRadioButton">radiobuttonlist for the document id</param>
    private void SetFileUploadFromDB(DataTable objDt, HyperLink objHyp, LinkButton lnkAdd, LinkButton lnkDelete, FileUpload fileUpload, HiddenField objhiddenField, RadioButtonList objRadioButton)
    {
        string strFilename = string.Empty;
        string strFolderPath = string.Empty;
        if (objDt != null && objDt.Rows.Count > 0)
        {
            {
                DataRow objRow = objDt.Rows[0];
                if (objRow["vchFileName"] != null && objRow["vchFileName"] != DBNull.Value)
                {
                    strFilename = objRow["vchFileName"].ToString();
                }
                if (objRow["vchFolderPath"] != null && objRow["vchFolderPath"] != DBNull.Value)
                {
                    strFolderPath = objRow["vchFolderPath"].ToString();
                }

                if (!string.IsNullOrEmpty(strFilename) && File.Exists(Server.MapPath(string.Format("~/incentives{0}{1}", strFolderPath, strFilename))))
                {
                    objhiddenField.Value = objRow["vchFileName"].ToString();
                    fileUpload.Enabled = false;
                    objHyp.NavigateUrl = string.Format("~/incentives{0}{1}", strFolderPath, objRow["vchFileName"]);
                    objHyp.Visible = true;
                    lnkDelete.Visible = true;
                    lnkDelete.Enabled = true;
                    //lnkAdd.Enabled = false;
                }
                else
                {
                    fileUpload.Enabled = true;
                    objHyp.Visible = false;
                    lnkDelete.Visible = false;
                }
                if (objRadioButton != null)
                {
                    if (objRow["vchDocId"] != null && objRow["vchDocId"] != DBNull.Value)
                    {
                        objRadioButton.SelectedValue = objRow["vchDocId"].ToString();
                    }
                }

            }
        }
    }

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
    #endregion

    #region function to get value while edit
    /// <summary>
    /// Function to get all the details based on the application details called when editing
    /// </summary>
    private void GetApplicationDetails()
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
        Boolean oldRecords = false;
        DataSet objDs = new DataSet();
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        objDs = objBuisnessLayer.Incentive_PcForm_Large_View(objSearch);
        int intApplyFlag = 0;
        if (objDs != null && objDs.Tables.Count > 0)
        {
            DataTable dtPcDetails = new DataTable();
            dtPcDetails = objDs.Tables[0];
            if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
            {
                DataRow objRow = dtPcDetails.Rows[0];
                txtDateOfIssuance.Value = objRow["dtmEinIssuance"].ToString();
                txtGST.Text = objRow["vchgstin"].ToString();
                rbtnTechnical.SelectedValue = objRow["intTechnical"].ToString();
                if (rbtnTechnical.SelectedValue == "2")
                    dvTechnical.Visible = false;
                if (objRow["intApplyFlag"] != null && objRow["intApplyFlag"] != DBNull.Value)
                {
                    intApplyFlag = Convert.ToInt32(objRow["intApplyFlag"].ToString());
                }
                hdnPeal.Value = objRow["inv"].ToString();
                if (intApplyFlag == 1) //only if these are approved records then those are old records
                {
                    oldRecords = true;
                }
                int intApplicationType = Convert.ToInt32(objRow["vchAppFor"].ToString());

                //if user is coming from no peal and no pc for first time and it is in draft position
                if (hdnPeal.Value == "1" && intApplyFlag == 0)
                {
                    //these are the existing details of the offline pc that user was trying to upload for the existing with amendement pc
                    if (Request.QueryString["offline"] == "3")
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
                        divUploadPc.Visible = true;
                        lblPcMessage.Visible = true;
                        divChangeIn.Visible = true;
                        DisableInAmd();
                        btnSaveAsDraft.Visible = false;
                        btnApply.Text = "Submit";
                        lblPcTypeDetails.Text = "Please enter details of unit for E/M/D";
                    }
                    else
                    {
                        //show both the options 
                        rdBtnApplicationFor.Items.Clear();
                        rdBtnApplicationFor.Items.Add(new ListItem("New", "1"));
                        rdBtnApplicationFor.Items.Add(new ListItem("Existing", "2"));
                        if (intApplicationType == (int)enAppFor.New || intApplicationType == (int)enAppFor.Rehabilitated_New || intApplicationType == (int)enAppFor.Transferred_new || intApplicationType == (int)enAppFor.Migrated_new || intApplicationType == (int)enAppFor.New_EMD)
                        {
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
                            drpApplicationType.SelectedValue = intApplicationType.ToString();
                        }
                        else if (intApplicationType == (int)enAppFor.exist_div || intApplicationType == (int)enAppFor.exist_Exp || intApplicationType == (int)enAppFor.exist_mod || intApplicationType == (int)enAppFor.exist)
                        {
                            rdBtnApplicationFor.SelectedValue = "2";
                            drpApplicationType.Items.Clear();
                            drpApplicationType.Items.Add(new ListItem("Existing", ((int)enAppFor.exist).ToString()));
                            drpApplicationType.SelectedIndex = 0;
                            drpApplicationType.SelectedValue = intApplicationType.ToString();
                        }
                    }
                } //end of no peal and pc in draft position

                //if the user has an approved peal or approved pc
                else if (hdnPeal.Value == "2" || hdnPeal.Value == "3")
                {
                    if (intApplicationType == (int)enAppFor.New || intApplicationType == (int)enAppFor.Rehabilitated_New || intApplicationType == (int)enAppFor.Transferred_new || intApplicationType == (int)enAppFor.Migrated_new)
                    {
                        if (intApplyFlag == 1) //if user is appying for another pc and the old record was a PC for a new unit
                        {
                            rdBtnApplicationFor.Items.Clear();
                            rdBtnApplicationFor.Items.Add(new ListItem("New", "1"));
                            rdBtnApplicationFor.Items[0].Selected = true;
                            drpApplicationType.Items.Clear();
                            drpApplicationType.Items.Add(new ListItem("New Unit E/M/D", ((int)enAppFor.New_EMD).ToString()));
                            drpApplicationType.SelectedIndex = 0;
                            divChangeIn.Visible = true;
                            DisableInAmd();
                        }
                        else // if it is draft and the user had selected one of them then bind 
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
                    }
                    else if (intApplicationType == (int)enAppFor.New_EMD)
                    {
                        rdBtnApplicationFor.Items.Clear();
                        rdBtnApplicationFor.Items.Add(new ListItem("New", "1"));
                        rdBtnApplicationFor.Items[0].Selected = true;
                        drpApplicationType.Items.Clear();
                        drpApplicationType.Items.Add(new ListItem("New Unit E/M/D", ((int)enAppFor.New_EMD).ToString()));
                        divChangeIn.Visible = true;
                        DisableInAmd();
                    }
                    else if (intApplicationType == (int)enAppFor.exist)
                    {
                        if (intApplyFlag == 1)
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
                            divChangeIn.Visible = true;
                            DisableInAmd();
                        }
                        else
                        {
                            rdBtnApplicationFor.Items.Clear();
                            rdBtnApplicationFor.Items.Add(new ListItem("existing", "2"));
                            rdBtnApplicationFor.Items[0].Selected = true;
                            drpApplicationType.Items.Clear();
                            drpApplicationType.Items.Add(new ListItem("Existing", ((int)enAppFor.exist).ToString()));
                            drpApplicationType.SelectedIndex = 0;
                        }
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
                        DisableInAmd();
                    }
                } //end of user with approved pc or approved peal

                if (Request.QueryString["id"] != null)
                {
                    lblAppNo.Text = objRow["vchFormattedNo"].ToString();
                }
                else
                {
                    if (intApplyFlag == 0)
                    {
                        if (Request.QueryString["offline"] != null && Request.QueryString["offline"] == "3")
                        {
                            GetNewAppCode();
                            hdnApplyFlag.Value = "0";
                            lblOldPcNo.Text = objRow["vchPCNo"].ToString();
                            lblPcIssueDate.Text = objRow["dtmIssueDate"].ToString();
                            divOldPC.Visible = true;
                            hdnPrevProdCommDate.Value = objRow["dtmPrevProdComm"].ToString();
                        }
                        else
                        {
                            lblAppNo.Text = objRow["vchFormattedNo"].ToString();// objRow["vchAppNo"].ToString();
                            hdnApplyFlag.Value = "1";

                            string vchChngIn = objRow["vchChngIn"].ToString();
                            hdnChangeIn.Value = vchChngIn;
                            UpdateCheckBoxList(vchChngIn, chkLstChange);
                            if (chkLstChange.SelectedIndex > 0)
                            {
                                EnableDisablePanelByCheckBox();
                                if (objRow["bitPlantModified"] != null && objRow["bitPlantModified"] != DBNull.Value)
                                {
                                    chkPlant.Checked = Convert.ToBoolean(objRow["bitPlantModified"].ToString());
                                }
                                if (objRow["bitprodModified"] != null && objRow["bitprodModified"] != DBNull.Value)
                                {
                                    chkProductsAmd.Checked = Convert.ToBoolean(objRow["bitprodModified"].ToString());
                                }
                            }
                        }

                    }
                    else
                    {
                        int intApproved = 0;
                        if (objRow["intApproved"] != null && objRow["intApproved"] != DBNull.Value)
                        {
                            intApproved = Convert.ToInt32(objRow["intApproved"].ToString());
                        }
                        if (intApproved == 2)
                        {
                            lblOldPcNo.Text = objRow["vchPCNo"].ToString();
                            lblPcIssueDate.Text = objRow["dtmIssueDate"].ToString();
                            divOldPC.Visible = true;
                            hdnPrevProdCommDate.Value = objRow["dtmPrevProdComm"].ToString();
                        }
                        GetNewAppCode();
                    }
                }

                lblIndustryCode.Text = objRow["vchIndustryCode"].ToString() == "" ? "NA" : objRow["vchIndustryCode"].ToString();
                txtEin.Text = objRow["vchEINEMIIPMTNo"].ToString();
                if (txtEin.Text != "" && intApplyFlag == 1)
                    txtEin.Enabled = false;
                txtUan.Text = objRow["vchUAN"].ToString();
                if (txtUan.Text != "" && intApplyFlag == 1)
                    txtUan.Enabled = false;
                txtEnterpriseName.Text = objRow["vchCompName"].ToString();
                drpCompanyType.SelectedValue = objRow["intUnitType"].ToString();
                drpOrganizationType.SelectedValue = objRow["intOrgType"].ToString();
                txtOtherOrg.Text = objRow["vchOrgOther"].ToString();
                drpOwnerType.SelectedValue = objRow["intOwnerCode"].ToString();
                SetOwnerCategoryDocByType();
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
                if (objRow["vchOfficeMobCode"].ToString() != "0")
                    ddlCode.SelectedValue = objRow["vchOfficeMobCode"].ToString();
                else
                    ddlCode.SelectedValue = "1";
                if (objRow["vchEntMobCode"].ToString() != "0")
                    drpEntCode.SelectedValue = objRow["vchEntMobCode"].ToString();
                else
                    drpEntCode.SelectedIndex = 1;

                if (objRow["vchOfficeFaxCode"].ToString() != "0")
                    drpFx.SelectedValue = objRow["vchOfficeFaxCode"].ToString();
                else
                    drpFx.SelectedValue = "1";

                if (objRow["vchEntFaxCode"].ToString() != "0")
                    drpEnterpriseFax.SelectedValue = objRow["vchEntFaxCode"].ToString();
                else
                    drpEnterpriseFax.SelectedValue = "1";

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

                txtDateFFI.Value = objRow["dtmFFCI"].ToString();
                if (txtDateFFI.Value == "01-Jan-1900")
                    txtDateFFI.Value = "";
                txtWorkingCapital.Text = objRow["decWorkingCapital"].ToString();
                txtEquity.Text = objRow["decEquity"].ToString();
                txtLoan.Text = objRow["decLoan"].ToString();
                txtFdiComponent.Text = objRow["decFdi"].ToString();
                txtland.Text = objRow["decLandInvestment"].ToString();
                txtBuilding.Text = objRow["decBuilding"].ToString();
                if (Session["InvAmt"] != null)
                    txtPlantMachinery.Text = Session["InvAmt"].ToString();
                else
                    txtPlantMachinery.Text = objRow["decPlant"].ToString();
                txtOthers.Text = objRow["decOthers"].ToString();

                txtProductCode.Text = objRow["vchProductCode"].ToString();
                txtName.Text = objRow["vchProductName"].ToString();
                txtContractDemand.Text = objRow["decContractDemand"].ToString();
                txtPowerConnection.Value = objRow["dtmPowerConn"].ToString();
                if (txtPowerConnection.Value == "01-Jan-1900")
                    txtPowerConnection.Value = "";
                txtProdComm.Value = objRow["dtmProdComm"].ToString();
                if (txtProdComm.Value == "01-Jan-1900")
                    txtProdComm.Value = "";
                txtDateOfIssuance.Value = objRow["dtmEinIssuance"].ToString();
                if (txtDateOfIssuance.Value == "01-Jan-1900")
                    txtDateOfIssuance.Value = "";

                rdBtnLstPower.SelectedValue = objRow["intPwrReq"].ToString();
                string strValuue = rdBtnLstPower.SelectedValue;
                if (strValuue == "1")
                {
                    divPower.Visible = true;
                }
                if (Request.QueryString["id"] != null)
                {
                    if (intApplyFlag == 0)
                    {
                        btnApply.Visible = true;
                        btnSaveAsDraft.Visible = true;
                    }
                    else
                    {
                        btnApply.Visible = false;
                        btnSaveAsDraft.Visible = false;
                    }
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
            if (Request.QueryString["id"] != null)
            {
                for (int cnt = 0; cnt < tblEmployement.Rows.Count; cnt++)
                {
                    tblEmployement.Rows[cnt].Cells[1].Visible = false;
                    tblEmployement.Rows[cnt].Cells[4].Visible = false;
                }
            }
            dtPcDetails = objDs.Tables[2];
            if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
            {
                BindProductGridview(dtPcDetails);
                ViewState["Products"] = dtPcDetails;

            }
            dtPcDetails = objDs.Tables[28];
            if (dtPcDetails != null && dtPcDetails.Rows.Count > 0)
            {
                BindMachineryGridview(dtPcDetails);
                ViewState["Machinery"] = dtPcDetails;

            }
            #region file upload view
            //organization type doc
            SetFileUploadFromDB(objDs.Tables[4], hypOrdDocument, lnkOrgDocumentPdf, lnkOrgDocumentDelete, fuOrgDocument, hdnOrgDocument, rdBtnOrg);

            //owner type based on consitution of organization
            SetFileUploadFromDB(objDs.Tables[3], hypDocType, lnkDocTypeUpload, lnkDocTypeDelete, fuDocumentType, hdnDocumentType, rdBtnOwnerTYpe);

            //sector relevant document
            SetFileUploadFromDB(objDs.Tables[5], hypProject, lnkProjectAdd, lnkProjectDel, fuProject, hdnProject, null);

            //check if clearence document is being downloaded from service, if yes then get the document and set the parameter else set from database
            if (!(!string.IsNullOrEmpty(hdnIsOsPCBDownloaded.Value) && hdnIsOsPCBDownloaded.Value == "1"))
            {
                SetFileUploadFromDB(objDs.Tables[6], hypCLearence, lnkCLearenceAdd, lnkClearenceDel, fuClearence, hdnClearence, null);
            }

            //land document
            SetFileUploadFromDB(objDs.Tables[7], hypLandDelete, lnkLandAdd, lnkLandDelete, fuLand, hdnLand, rdBtnLand);

            //plant document
            SetFileUploadFromDB(objDs.Tables[8], hypPlant, lnkPlantadd, lnkPlandDelete, fuPlant, hdnPlant, rdBtnPlant);

            //power document
            SetFileUploadFromDB(objDs.Tables[9], hypPower, lnkPowerAdd, lnkPowerDel, fuPower, hdnPower, rdBtnPower);

            // 1st Raw material purchase Bill
            SetFileUploadFromDB(objDs.Tables[10], hypViewProductFile, lnkPowerAdd, lnkProductDel, fuProduct, hdnProductfilename, null);

            // Please provide Certificate in support of SC / ST / OBC etc
            SetFileUploadFromDB(objDs.Tables[12], hypOwnerCategory, lnkOwnerCatDel, lnkOwnerCatDel, fuOwnerCategory, hdnOwnerCategory, rdBtnOwnerCategory);

            //Certificate in Support of Technical Qualification
            SetFileUploadFromDB(objDs.Tables[14], hypEmployement, lnkEmployementAdd, lnkEmployementDelete, fuEmployement, hdnEmployement, null);

            //Upload Agreement With CESU/NESCO
            SetFileUploadFromDB(objDs.Tables[15], hypAgreement, lnkAgreement, lnkAgreementDel, fupAgreement, hdnAgreement, null);

            //IEM Certificate
            SetFileUploadFromDB(objDs.Tables[16], hypIEMDelete, lnkIEM, lnkIEMDelete, fupIEM, hdnIEM, null);

            //GSTIN Document
            SetFileUploadFromDB(objDs.Tables[17], hypVAT, lnkVATPDF, lnkVATDel, fupVAT, hdnVAT, null);

            //Sale Invoice
            SetFileUploadFromDB(objDs.Tables[18], hypSaleInvoice, lnkSaleInovice, lnkSaleInoviceDel, fupSaleInvoice, hdnSaleInvoice, null);

            //Statement Showing Sale of last 3 months
            SetFileUploadFromDB(objDs.Tables[19], hypFirstSaleBill, lnkFirstSalBillAdd, lnkFirstSaleBillDel, fuFirstSaleBill, hdnFistSaleBill, null);

            //Statement Showing Production for last 3 months
            SetFileUploadFromDB(objDs.Tables[20], hypProduction, lnkProductionPost, lnkProdcutionDel, fupProductStatementPost, hdnProductionPost, null);

            //Statement of Raw Material Purchased for Processing After Production for 3 months
            SetFileUploadFromDB(objDs.Tables[21], hypRawMaterialPost, lnkRawMaterialPost, lnkRawMaterialPostDel, fupRawMaterialStatementPost, hdnRawMaterialPost, null);

            //Statement of Raw Material Purchased for Processing Before Production for 3 months
            SetFileUploadFromDB(objDs.Tables[22], hypRawMaterialPre, lnkRawMaterialPre, lnkRawMaterialPreDel, fupRawMaterialStatementPre, hdnRawMaterialPre, null);

            //  Upload Invoice of Raw Material
            SetFileUploadFromDB(objDs.Tables[23], hypInovice, lnkInovice, lnkInvoiceDel, fupInvoice, hdnInovice, null);

            //Upload Factory License No.
            SetFileUploadFromDB(objDs.Tables[24], hypFactoryLic, lnkFactoryLic, lnkFactoryLicDel, fupFactoryLic, hdnFactoryLic, null);

            //Investment as on the date of commercial production from the competent authority
            SetFileUploadFromDB(objDs.Tables[25], hypCompAuthority, lnkCompAuthority, lnkCompAuthorityDel, fupComAuthority, hdnCompAuthority, null);

            // Upload Bank Appraisal Report
            SetFileUploadFromDB(objDs.Tables[26], hypBank, lnkbankAdd, lnkbankDelete, fuBank, hdnBank, null);

            //(Building Valuation Report)
            SetFileUploadFromDB(objDs.Tables[27], hypBuildVal, lnkBuildVal, lnkBuildValdel, fupBuildingValReport, hdnBuildingValuation, null);

            //check if boiler document is being downloaded from service, if yes then get the document and set the parameter else set from database
            if (!(!string.IsNullOrEmpty(hdnBoilderDownloaded.Value) && hdnBoilderDownloaded.Value == "1"))
            {
                SetFileUploadFromDB(objDs.Tables[29], hypBoilerView, lnkBoilerAdd, lnkBoilerDelete, fupBoiler, hdnBoiler, null);
            }
            #endregion

            #region Old records for existing amd
            int intAppType = Convert.ToInt32(drpApplicationType.SelectedValue);
            if (rdBtnApplicationFor.SelectedValue == "2") // if the user has existing unit with emd
            {
                //if data is not coming from peal and the user has old records, only then two rows will be shown one for old records and the other one for 
                //before policy effective date
                if (hdnPeal.Value == "3" || (Request.QueryString["offline"] != null && Request.QueryString["offline"] == "3"))
                {

                    objSearch = new PcSearch()
                    {
                        intAppFor = Convert.ToInt32(Session["investorid"]),
                        strActionCode = "old",
                        intPageIndex = 0,
                        intPageSize = 0,
                        strFromDate = string.Empty,
                        strToDate = string.Empty,
                    };

                    objDs = new DataSet();
                    objDs = objBuisnessLayer.Incentive_PcForm_Large_View(objSearch);
                    if (objDs != null && objDs.Tables.Count > 0)
                    {
                        dtPcDetails = objDs.Tables[0];
                        if (dtPcDetails != null)
                        {
                            DataRow objRow = dtPcDetails.Rows[0];
                            lblOldFcci.Value = objRow["dtmFFCI"].ToString();
                            lblOldFcci.Disabled = true;
                            divOldFfci.Visible = true;
                            lblOldProdValue.Value = objRow["dtmProdComm"].ToString();
                            if (lblOldProdValue.Value == "01-Jan-1900")
                            {
                                lblOldProdValue.Value = "";
                            }
                            lblOldProdValue.Disabled = true;
                            divOldProd.Visible = true;

                            for (int cnt = 0; cnt < tblLandPlant.Rows.Count; cnt++)
                            {
                                tblLandPlant.Rows[cnt].Cells[2].Visible = true;
                            }
                            lblLandOld.Text = objRow["decLandInvestment"].ToString();
                            lblLandOld.Enabled = false;
                            lblPlantOld.Text = objRow["decPlant"].ToString();
                            lblPlantOld.Enabled = false;
                            lblBuildingOld.Text = objRow["decBuilding"].ToString();
                            lblBuildingOld.Enabled = false;
                            lblOtherOld.Text = objRow["decOthers"].ToString();
                            lblOtherOld.Enabled = false;
                            lblTotalOld.Text = objRow["decTotal"].ToString();
                            lblTotalOld.Enabled = false;
                            lblFfciName.Text = "Date of First Fixed Capital Investment - E/M/D";
                            lblProdComm.Text = "   Date of commencement of Production - E/M/D";
                        }

                        dtPcDetails = objDs.Tables[1];
                        if (dtPcDetails != null)
                        {
                            DataRow objRow = dtPcDetails.Rows[0];
                            for (int cnt = 0; cnt < tblEmployement.Rows.Count; cnt++)
                            {
                                tblEmployement.Rows[cnt].Cells[1].Visible = true;
                                tblEmployement.Rows[cnt].Cells[4].Visible = true;
                            }
                            lblSupOld.Text = objRow["intSupervisor"].ToString();
                            lblManagerialOld.Text = objRow["intManaregailSkill"].ToString();
                            lblSkilledOld.Text = objRow["intSkilled"].ToString();
                            lblSemiSkilledOld.Text = objRow["intSemiSkilled"].ToString();
                            lblUnSkilledOld.Text = objRow["intUnSkilled"].ToString();
                            lblTotalScOld.Text = objRow["intScTotal"].ToString();
                            lblTotalStOld.Text = objRow["intStTotal"].ToString();
                            lblWomenOld.Text = objRow["intWomen"].ToString();
                            lblPhdOld.Text = objRow["intDisabled"].ToString();
                            lblGeneralOld.Text = objRow["intGeneral"].ToString();
                            lblGrandTotalOld.Text = objRow["TotalSum"].ToString();
                            lblEmpTotalOld.Text = objRow["TotalSumST"].ToString();
                            lblOldDirectEmp.Text = objRow["intDirectEmp"].ToString();
                            divOldDirect.Visible = true;
                            lblOldContractual.Text = objRow["intContractual"].ToString();
                            divOldContractual.Visible = true;
                            lblEmployementName.Text = "Direct Employment (in Numbers) As on Company Payroll -  E/M/D";
                            lblContractual.Text = "Contractual Employment -  E/M/D";


                            lblSupOld.Enabled = false;
                            lblManagerialOld.Enabled = false;
                            lblSkilledOld.Enabled = false;
                            lblSemiSkilledOld.Enabled = false;
                            lblUnSkilledOld.Enabled = false;
                            lblTotalScOld.Enabled = false;
                            lblTotalStOld.Enabled = false;
                            lblWomenOld.Enabled = false;
                            lblPhdOld.Enabled = false;
                            lblGeneralOld.Enabled = false;
                            lblGrandTotalOld.Enabled = false;
                            lblEmpTotalOld.Enabled = false;
                            lblOldDirectEmp.Enabled = false;
                            lblOldContractual.Enabled = false;
                        }
                        dtPcDetails = objDs.Tables[2];
                        if (dtPcDetails != null)
                        {
                            divOldProducts.Visible = true;
                            grdOldProducts.Visible = true;
                            grdOldProducts.DataSource = dtPcDetails;
                            grdOldProducts.DataBind();
                        }

                    }
                }
            #endregion

            }
        }
        else
        {
            hdnApplyFlag.Value = "0";
            // GetNewAppCode();
            GetPealDetails();
        }
    }
    #endregion

    #region product table code
    /// <summary>
    /// Create the datatable for the product gridview
    /// </summary>
    /// <returns></returns>
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
        dtProduct.Columns.Add(new DataColumn("bitMainProduct"));
        dtProduct.Columns.Add(new DataColumn("VchIsMainProduct"));
        dtProduct.Columns.Add(new DataColumn("dtmProd"));
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
    #endregion

    #region old product table code
    /// <summary>
    /// Create the datatable for the product gridview
    /// </summary>
    /// <returns></returns>
    private DataTable CreateProductTableOld()
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
        dtProduct.Columns.Add(new DataColumn("bitMainProduct"));
        dtProduct.Columns.Add(new DataColumn("VchIsMainProduct"));
        return dtProduct;
    }

    /// <summary>
    /// Function to bind the product gridview from the datatable in view state
    /// if not existing then create a dummy row in datatable bind it to gridview
    /// </summary>
    private void BindProductOld()
    {
        DataTable dtProducts = CreateProductTableOld();
        dtProducts.TableName = "Products";
        if ((ViewState["ProductsOld"] != null))
        {
            dtProducts = (DataTable)ViewState["ProductsOld"];
        }
        BindProductGridviewOld(dtProducts);
    }

    /// <summary>
    /// Common function to just bind the data in datatable to gridview and hide and show the add and delete button
    /// </summary>
    /// <param name="dtView"></param>
    private void BindProductGridviewOld(DataTable dtView)
    {
        grdOldProducts.DataSource = dtView;
        grdOldProducts.DataBind();
    }
    #endregion

    #region plant table code
    /// <summary>
    /// Create the datatable for the old product gridview
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Function to bind the machinery gridview from the datatable in view state
    /// if not existing then create a dummy row in datatable bind it to gridview
    /// </summary>
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

    /// <summary>
    /// bind machinery gridview details
    /// </summary>
    /// <param name="dtView"></param>
    private void BindMachineryGridview(DataTable dtView)
    {
        gvPlant.DataSource = dtView;
        gvPlant.DataBind();
    }
    #endregion
    #endregion


}