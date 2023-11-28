/*
 * File Name : IncentivePCForm.aspx
 * Class name : incentives_IncentivePCForm
 * Created By : Ritika Lath
 * Created On : 5th September 2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class incentives_IncentivePCForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown();
        }
    }

    protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    {
        Button objButton = (Button)sender;
        string strCommandArg = objButton.CommandArgument;
        Incentive_PCMaster objMaster = new Incentive_PCMaster();
        if (string.Equals(strCommandArg, "d", StringComparison.OrdinalIgnoreCase))
        {
            objMaster.intApplyFlag = 0;
        }
        else
        {
            objMaster.intApplyFlag = 1;
        }
        objMaster.intAppFor = Convert.ToInt32(drpApplicationType.SelectedValue);

        objMaster.strChngIn = GetCheckBoxListValues(chkLstChange);
        objMaster.intAppNo = Convert.ToInt32(txtApplicationNo.Text.Trim());
        objMaster.strEINEMIIPMTNo = txtEin.Text.Trim();
        objMaster.strUAN = txtUan.Text.Trim();
        objMaster.strCompName = txtEnterpriseName.Text.Trim();
        objMaster.intUnitCat = Convert.ToInt32(drpUnitCategory.SelectedValue);
        objMaster.intUnitType = Convert.ToInt32(drpCompanyType.SelectedValue);
        objMaster.intOrgType = Convert.ToInt32(drpOrganizationType.SelectedValue);
        objMaster.strOwnerName = txtOwnerName.Text.Trim();
        objMaster.intOwnerCode = Convert.ToInt32(txtOwnerCode.Text.Trim());

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
        objMaster.strUnitLoc = txtLocationOfUnit.Text.Trim();

        objMaster.dtmFFCI = txtDateFFI.Value;
        objMaster.strInvestIn = GetCheckBoxListValues(chkInvestIn);
        objMaster.strInvestMode = txtModeOfInvestment.Text.Trim();
        objMaster.decFixedCapital = Convert.ToDecimal(txtFixedCapital.Text.Trim());
        objMaster.decWorkingCapital = Convert.ToDecimal(txtWorkingCapital.Text.Trim());
        objMaster.decSelfFinance = Convert.ToDecimal(txtSelfFinance.Text.Trim());
        objMaster.decBorrowFinance = Convert.ToDecimal(txtBorrowed.Text.Trim());

        objMaster.intTechSupSkillGen = Convert.ToInt32(txt_Gen_Technical.Text.Trim());
        objMaster.intTechSupSkillSC = Convert.ToInt32(txt_SC_Technical.Text.Trim());
        objMaster.intTechSupSkillST = Convert.ToInt32(txt_ST_Technical.Text.Trim());
        objMaster.intTechSupSkillTotal = Convert.ToInt32(txt_LocalGen_Technical.Text.Trim());
        objMaster.intTechSupSkillLocal = Convert.ToInt32(txt_Local_Technical.Text.Trim());
        objMaster.intNTechAdmUnSkillGen = Convert.ToInt32(txt_Gen_NonNonTechnical.Text.Trim());
        objMaster.intNTechAdmUnSkillSC = Convert.ToInt32(txt_SC_NonTechnical.Text.Trim());
        objMaster.intNTechAdmUnSkillST = Convert.ToInt32(txt_ST_NonTechnical.Text.Trim());
        objMaster.intNTechAdmUnSkillTotal = Convert.ToInt32(txt_Local_NonTechnical.Text.Trim());
        objMaster.intNTechAdmUnSkillLocal = Convert.ToInt32(txt_LocalGen_NonTechnical.Text.Trim());
        objMaster.intWomen = Convert.ToInt32(txtWomen.Text.Trim());
        objMaster.intDisabled = Convert.ToInt32(txtPhysicallyChallenged.Text.Trim());

        objMaster.strProductCode = txtProductCode.Text.Trim();
        objMaster.strProductName = txtName.Text.Trim();
        objMaster.intIsPwrReq = Convert.ToInt32(rdBtnLstPower.SelectedValue);
        objMaster.decContractDemand = Convert.ToDecimal(txtContractDemand.Text.Trim());
        objMaster.dtmProdComm = txtProdComm.Value;
        int intRetValue = 0;
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        intRetValue = objBuisnessLayer.Incentive_PcDetails_AED(objMaster);
    }

    private DataTable CreateProductTable()
    {
        DataTable dtProduct = new DataTable();
        DataColumn dcId = new DataColumn();
        dcId.ColumnName = "id";
        dcId.AutoIncrement = true;
        dcId.AutoIncrementSeed = 1;
        dcId.AutoIncrementStep = 1;
        dtProduct.Columns.Add(dcId);
        dtProduct.Columns.Add(new DataColumn("item"));
        dtProduct.Columns.Add(new DataColumn("Code"));
        dtProduct.Columns.Add(new DataColumn("Qty"));
        dtProduct.Columns.Add(new DataColumn("Unit"));
        dtProduct.Columns.Add(new DataColumn("Cost"));
        return dtProduct;
    }

    private void BindDropDown()
    {
        IncentiveMasterBusinessLayer objBuisnessLayer = new IncentiveMasterBusinessLayer();
        DataSet objDa = objBuisnessLayer.BindDropdown("ddl");
        if (objDa != null)
        {
            FillDropDown(objDa.Tables[0], "Application for", drpApplicationType);
            FillDropDown(objDa.Tables[1], "Change in", chkLstChange);
            FillDropDown(objDa.Tables[2], "Organization Type", drpOrganizationType);
            FillDropDown(objDa.Tables[3], "Owner Type", drpOrganizationType);
            FillDropDown(objDa.Tables[4], "Unit Category", drpUnitCategory);
            FillDropDown(objDa.Tables[5], "Company Type", drpCompanyType);
            FillDropDown(objDa.Tables[6], "Unit", drpUnitType);
            FillDropDown(objDa.Tables[7], "Invest In", chkInvestIn);
        }

    }

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

    private void FillDropDown(DataTable objDt, string strHeaderType, CheckBoxList objChkList)
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
            strRetValue = strRetValue.Substring(0, strRetValue.Length - 2);
        }
        return strRetValue;
    }
}