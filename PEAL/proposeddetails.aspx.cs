#region  Page Info
//******************************************************************************************************************
// File Name             :   proposeddetails.aspx.cs
// Description           :   Project Information Details of Promoter
// Created by            :   Subhasmita Behera
// Created On            :   18-Aug-2017
//  "VERION= v2"
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   1                      27-Aug-2019               Sushant Jena                      Equity and Networth validation implementaion.  
//******************************************************************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ServiceModel;
using BusinessLogicLayer.Proposal;
using System.IO;
using EntityLayer.Proposal;
using System.Collections.Specialized;
using System.Net;
using System.Configuration;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public partial class proposeddetails : SessionCheck
{
    #region "Variable Declaration"
    ProposalBAL objService = new ProposalBAL();
    ProjectInfo objProp = new ProjectInfo();
    PromoterDet objProposal = new PromoterDet();
    CommonValidation objcmv = new CommonValidation();
    DataTable objdt;
    string strRetval = "";
    string FilePath = "";
    int Maxsize = (4 * 1024 * 1024);
    string allFileVal = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        SetValueOfAllControls();
        if (!Page.IsPostBack)
        {
            try
            {
                ModalPopupExtender1.Hide();

                DelBtnHideshow();
                BindCommercial();
                CreateDTOtherLocation();
                CreateDTOtherUnits();
                BindState();
                BindCountry();
                BindUnits();
                BindSector();
                CreateDTOProduct();
                BindUnits1();
                if ((!string.IsNullOrEmpty(Session["proposalno"] as string)) || (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"])))
                {
                    GetData();
                }

                ddlProjectcomingunder.Enabled = false;
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "PEAL");
            }
        }
    }

    #region FunctionUsed

    protected void SetValueOfAllControls()
    {
        txtTotalCapitalInv.Text = Request["txtTotalCapitalInv"];
        txtTotalExist.Text = Request["txtTotalExist"];
        txtTotalProposed.Text = Request["txtTotalProposed"];
        txtTotal.Text = Request["txtTotal"];
    }
    public static string GetSTRXMLResult(DataTable dtTable)
    {
        string strXMLResult = "";
        if ((dtTable != null))
        {
            if (dtTable.Rows.Count > 0)
            {
                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw);
                strXMLResult = sw.ToString();
                sw.Close();
                sw.Dispose();
            }
        }

        return strXMLResult;
    }
    public void AddProjectInfo()
    {
        try
        {
            if (Convert.ToInt32(Session["id"]) != 0)
            {
                objProp.strAction = "U";
            }
            else
            {
                objProp.strAction = "A";

            }

            objProp.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }

            objProp.vchNameOfUnit = Convert.ToString(Server.HtmlEncode(txtUnitName.Text));
            objProp.vchEINnIEMnIL = Convert.ToString(txtregApp.Text);
            objProp.intSectorId = Convert.ToInt32(ddlSector.SelectedValue);
            objProp.intSubSectorId = Convert.ToInt32(ddlSubSector.SelectedValue);
            if (txtProposedAnnCapacity.Text != "")
            {
                objProp.decProposedAnnualCapacity = Convert.ToDecimal(txtProposedAnnCapacity.Text);
            }
            else
            {
                objProp.decProposedAnnualCapacity = 0;
            }

            objProp.vchUnit = ddlUnit.SelectedValue;
            if (ddlUnit.SelectedItem.Text == "Other")
            {
                DvSpecify.Visible = true;
                objProp.vchOtherUnit = Convert.ToString(txtOtherUnit.Text);
            }

            objProp.decLandIncLandDev = Convert.ToDecimal(txtlanddev.Text);
            objProp.decBuildingndConstruction = Convert.ToDecimal(txtBuilding.Text);
            objProp.decPlantndMachinery = Convert.ToDecimal(txtPlantMachinery.Text);
            if (txtOthers.Text != "")
            {
                objProp.decOthers = Convert.ToDecimal(txtOthers.Text);
            }
            else
            {
                objProp.decOthers = 0;
            }

            if (txtTotalCapitalInv.Text != "")
            {
                objProp.decTotCapitalInvestment = Convert.ToDecimal(Request["txtTotalCapitalInv"]);
            }
            else
            {
                objProp.decTotCapitalInvestment = 0;
            }
            // objProp.decTotCapitalInvestment = Convert.ToDecimal(Request["txtTotalCapitalInv"]);

            objProp.intPeriodToCommenceProduction = Convert.ToInt32(ddlCommProdInMonth.SelectedValue);
            objProp.vchProjectComingUnder = Convert.ToString(ddlProjectcomingunder.SelectedValue);
            objProp.vchPollutionCategory = Convert.ToString(ddlPolutionCategory.SelectedValue);
            if (txtGroundBreaking.Text != "")
            {
                objProp.intGroundBreaking = Convert.ToInt32(txtGroundBreaking.Text);
            }
            else
            {
                objProp.intGroundBreaking = 0;
            }

            if (txtCivil.Text != "")
            {
                objProp.intCivilndStructuralCompln = Convert.ToInt32(txtCivil.Text);
            }
            else
            {
                objProp.intCivilndStructuralCompln = 0;
            }

            if (txtEquipment.Text != "")
            {
                objProp.intMajorEquipmentErect = Convert.ToInt32(txtEquipment.Text);
            }
            else
            {
                objProp.intMajorEquipmentErect = 0;
            }

            if (txtCommissioning.Text != "")
            {
                objProp.intStartOfCommercialProd = Convert.ToInt32(txtCommissioning.Text);
            }
            else
            {
                objProp.intStartOfCommercialProd = 0;
            }

            if (txtEquity.Text != "")
            {
                objProp.decEquityContribution = Convert.ToDecimal(txtEquity.Text);
            }
            else
            {
                objProp.decEquityContribution = 0;
            }

            if (txtBankFinance.Text != "")
            {
                objProp.decBankndInstitutionalFin = Convert.ToDecimal(txtBankFinance.Text);
            }
            else
            {
                objProp.decBankndInstitutionalFin = 0;
            }

            if (Request["txtTotal"] != "")
            {
                objProp.decTotFinance = Convert.ToDecimal(Request["txtTotal"]);
            }
            else
            {
                objProp.decTotFinance = 0;
            }

            if (txtFDI.Text != "")
            {
                objProp.decForeignInvestment = Convert.ToDecimal(txtFDI.Text);
            }
            else
            {
                objProp.decForeignInvestment = 0;
            }

            objProp.vchIRR = Convert.ToString(txtIRR.Text);
            objProp.vchDSCR = Convert.ToString(txtDSCR.Text);

            if (Request["txtManagerExist"] != "")
            {
                objProp.intMangerExist = Convert.ToInt32(Request["txtManagerExist"]);
            }
            else
            {
                objProp.intMangerExist = 0;
            }

            objProp.intManagerProp = Convert.ToInt32(txtManagerProposed.Text);

            if (Request["txtSupervisorExist"] != "")
            {
                objProp.intSupervisorExist = Convert.ToInt32(Request["txtSupervisorExist"]);
            }
            else
            {
                objProp.intSupervisorExist = 0;
            }

            objProp.intSupervisorProp = Convert.ToInt32(txtSupervisorProposed.Text);

            if (Request["txtSkilledExist"] != "")
            {
                objProp.intSkilledExist = Convert.ToInt32(Request["txtSkilledExist"]);
            }
            else
            {
                objProp.intSkilledExist = 0;
            }

            objProp.intSkilledProp = Convert.ToInt32(txtSkilledProposed.Text);

            if (Request["txtSemiskilledExist"] != "")
            {
                objProp.intSemiSkilledExist = Convert.ToInt32(Request["txtSemiskilledExist"]);
            }
            else
            {
                objProp.intSemiSkilledExist = 0;
            }

            objProp.intSemiSkilledProp = Convert.ToInt32(txtSemiskilledProposed.Text);

            if (txtUnskilledExist.Text != "")
            {
                objProp.intUnSkilledExist = Convert.ToInt32(txtUnskilledExist.Text);
            }
            else
            {
                objProp.intUnSkilledExist = 0;
            }

            objProp.intUnSkilledProp = Convert.ToInt32(txtUnskilledProposed.Text);

            if (txtTotalExist.Text != "")
            {
                objProp.intTotalExist = Convert.ToInt32(Request["txtTotalExist"]);
            }
            else
            {
                objProp.intTotalExist = 0;
            }

            objProp.intTotalProp = Convert.ToInt32(Request["txtTotalProposed"]);
            objProp.intPropDirectEmployment = Convert.ToInt32(txtDirectEmp.Text.Trim() == "" ? "0" : txtDirectEmp.Text.Trim());
            objProp.intPropContractualEmployment = Convert.ToInt32(txtContractualEmp.Text.Trim() == "" ? "0" : txtContractualEmp.Text.Trim());

            objProp.vchIndustryInterprenur = Convert.ToString(hdnIndustryEntMemorandum.Value);
            objProp.vchFeasibilityReport = Convert.ToString(hdnFeasibilityReport.Value);
            objProp.vchBoardResolution = Convert.ToString(hdnBoardResolution.Value);
            objProp.vchSourceOfFinance = Convert.ToString(hdnOtherFin.Value);

            objdt = (DataTable)ViewState["OTHERUNITS"];
            objdt.TableName = "OTHERUNITS";
            objProp.strOtherUnits = GetSTRXMLResult(objdt);

            objdt = (DataTable)ViewState["OTHERLOC"];
            objdt.TableName = "OTHERLOC";
            objProp.strProjLocation = GetSTRXMLResult(objdt);

            objdt = (DataTable)ViewState["PRODUCTS"];
            objdt.TableName = "PRODUCTS";
            objProp.strProductDetails = GetSTRXMLResult(objdt);

            objProp.intEinNoderr = Convert.ToInt32(drpEin.SelectedValue);

            if (ViewState["GCNetWorth"] != null)
            {
                objdt = (DataTable)ViewState["GCNetWorth"];
                objdt.TableName = "GCNetWorth";
                objProp.strXmlGcNetWorth = GetSTRXMLResult(objdt);
            }

            ///// DML Operation
            string strRetVal = objService.ProjectInfoAED(objProp);
            if (strRetVal == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Record(s) saved successfully.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objProp = null;
        }
    }
    public void AddProjectInfoAsDraft()
    {
        try
        {
            if (Convert.ToInt32(Session["id"]) != 0)
            {
                objProp.strAction = "U";
            }
            else
            {
                objProp.strAction = "A";

            }

            objProp.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }

            objProp.vchNameOfUnit = Convert.ToString(Server.HtmlEncode(txtUnitName.Text));
            objProp.vchEINnIEMnIL = Convert.ToString(txtregApp.Text);
            objProp.intSectorId = Convert.ToInt32(ddlSector.SelectedValue);
            objProp.intSubSectorId = Convert.ToInt32(ddlSubSector.SelectedValue);

            if (txtProposedAnnCapacity.Text != "")
            {
                objProp.decProposedAnnualCapacity = Convert.ToDecimal(txtProposedAnnCapacity.Text);
            }
            else
            {
                objProp.decProposedAnnualCapacity = 0;
            }

            objProp.vchUnit = ddlUnit.SelectedValue;

            if (ddlUnit.SelectedItem.Text == "Other")
            {
                objProp.vchOtherUnit = Convert.ToString(txtOtherUnit.Text);
            }

            if (txtlanddev.Text != "")
            {
                objProp.decLandIncLandDev = Convert.ToDecimal(txtlanddev.Text);
            }
            else
            {
                objProp.decLandIncLandDev = 0;
            }

            if (txtBuilding.Text != "")
            {
                objProp.decBuildingndConstruction = Convert.ToDecimal(txtBuilding.Text);
            }
            else
            {
                objProp.decBuildingndConstruction = 0;
            }

            if (txtPlantMachinery.Text != "")
            {
                objProp.decPlantndMachinery = Convert.ToDecimal(txtPlantMachinery.Text);
            }
            else
            {
                objProp.decPlantndMachinery = 0;
            }

            if (txtOthers.Text != "")
            {
                objProp.decOthers = Convert.ToDecimal(txtOthers.Text);
            }
            else
            {
                objProp.decOthers = 0;
            }

            if (Request["txtTotalCapitalInv"] != "")
            {
                objProp.decTotCapitalInvestment = Convert.ToDecimal(Request["txtTotalCapitalInv"]);
            }
            else
            {
                objProp.decTotCapitalInvestment = 0;
            }

            objProp.intPeriodToCommenceProduction = Convert.ToInt32(ddlCommProdInMonth.SelectedValue);
            objProp.vchProjectComingUnder = Convert.ToString(ddlProjectcomingunder.SelectedValue);
            objProp.vchPollutionCategory = Convert.ToString(ddlPolutionCategory.SelectedValue);

            if (txtGroundBreaking.Text != "")
            {
                objProp.intGroundBreaking = Convert.ToInt32(txtGroundBreaking.Text);
            }
            else
            {
                objProp.intGroundBreaking = 0;
            }

            if (txtCivil.Text != "")
            {
                objProp.intCivilndStructuralCompln = Convert.ToInt32(txtCivil.Text);
            }
            else
            {
                objProp.intCivilndStructuralCompln = 0;
            }

            if (txtEquipment.Text != "")
            {
                objProp.intMajorEquipmentErect = Convert.ToInt32(txtEquipment.Text);
            }
            else
            {
                objProp.intMajorEquipmentErect = 0;
            }

            if (txtCommissioning.Text != "")
            {
                objProp.intStartOfCommercialProd = Convert.ToInt32(txtCommissioning.Text);
            }
            else
            {
                objProp.intStartOfCommercialProd = 0;
            }

            if (txtEquity.Text != "")
            {
                objProp.decEquityContribution = Convert.ToDecimal(txtEquity.Text);
            }
            else
            {
                objProp.decEquityContribution = 0;
            }

            if (txtBankFinance.Text != "")
            {
                objProp.decBankndInstitutionalFin = Convert.ToDecimal(txtBankFinance.Text);
            }
            else
            {
                objProp.decBankndInstitutionalFin = 0;
            }

            if (Request["txtTotal"] != "")
            {
                objProp.decTotFinance = Convert.ToDecimal(Request["txtTotal"]);
            }
            else
            {
                objProp.decTotFinance = 0;
            }

            if (txtFDI.Text != "")
            {
                objProp.decForeignInvestment = Convert.ToDecimal(txtFDI.Text);
            }
            else
            {
                objProp.decForeignInvestment = 0;
            }

            objProp.vchIRR = Convert.ToString(txtIRR.Text);
            objProp.vchDSCR = Convert.ToString(txtDSCR.Text);
            if (txtManagerExist.Text != "")
            {
                objProp.intMangerExist = Convert.ToInt32(txtManagerExist.Text);
            }
            else
            {
                objProp.intMangerExist = 0;
            }

            if (txtManagerProposed.Text != "")
            {
                objProp.intManagerProp = Convert.ToInt32(txtManagerProposed.Text);
            }
            else
            {
                objProp.intManagerProp = 0;
            }
            if (txtSupervisorExist.Text != "")
            {
                objProp.intSupervisorExist = Convert.ToInt32(txtSupervisorExist.Text);
            }
            else
            {
                objProp.intSupervisorExist = 0;
            }

            if (txtSupervisorProposed.Text != "")
            {
                objProp.intSupervisorProp = Convert.ToInt32(txtSupervisorProposed.Text);
            }
            else
            {
                objProp.intSupervisorProp = 0;
            }

            if (txtSkilledExist.Text != "")
            {
                objProp.intSkilledExist = Convert.ToInt32(txtSkilledExist.Text);
            }
            else
            {
                objProp.intSkilledExist = 0;
            }

            if (txtSkilledProposed.Text != "")
            {
                objProp.intSkilledProp = Convert.ToInt32(txtSkilledProposed.Text);
            }
            else
            {
                objProp.intSkilledProp = 0;
            }

            if (txtSemiskilledExist.Text != "")
            {
                objProp.intSemiSkilledExist = Convert.ToInt32(txtSemiskilledExist.Text);
            }
            else
            {
                objProp.intSemiSkilledExist = 0;
            }

            if (txtSemiskilledProposed.Text != "")
            {
                objProp.intSemiSkilledProp = Convert.ToInt32(txtSemiskilledProposed.Text);
            }
            else
            {
                objProp.intSemiSkilledProp = 0;
            }

            if (txtUnskilledExist.Text != "")
            {
                objProp.intUnSkilledExist = Convert.ToInt32(txtUnskilledExist.Text);
            }
            else
            {
                objProp.intUnSkilledExist = 0;
            }

            if (txtUnskilledProposed.Text != "")
            {
                objProp.intUnSkilledProp = Convert.ToInt32(txtUnskilledProposed.Text);
            }
            else
            {
                objProp.intUnSkilledProp = 0;
            }

            if (Request["txtTotalExist"] != "")
            {
                objProp.intTotalExist = Convert.ToInt32(Request["txtTotalExist"]);
            }
            else
            {
                objProp.intTotalExist = 0;
            }

            if (Request["txtTotalProposed"] != "")
            {
                objProp.intTotalProp = Convert.ToInt32(Request["txtTotalProposed"]);
            }
            else
            {
                objProp.intTotalProp = 0;
            }

            if (txtDirectEmp.Text != "")
            {
                objProp.intPropDirectEmployment = Convert.ToInt32(txtDirectEmp.Text);
            }
            else
            {
                objProp.intPropDirectEmployment = 0;
            }

            if (txtContractualEmp.Text != "")
            {
                objProp.intPropContractualEmployment = Convert.ToInt32(txtContractualEmp.Text);
            }
            else
            {
                objProp.intPropContractualEmployment = 0;
            }

            objProp.vchIndustryInterprenur = Convert.ToString(hdnIndustryEntMemorandum.Value);
            objProp.vchFeasibilityReport = Convert.ToString(hdnFeasibilityReport.Value);
            objProp.vchBoardResolution = Convert.ToString(hdnBoardResolution.Value);
            objProp.vchSourceOfFinance = Convert.ToString(hdnOtherFin.Value);

            objdt = (DataTable)ViewState["OTHERUNITS"];
            objdt.TableName = "OTHERUNITS";
            objProp.strOtherUnits = GetSTRXMLResult(objdt);

            objdt = (DataTable)ViewState["OTHERLOC"];
            objdt.TableName = "OTHERLOC";
            objProp.strProjLocation = GetSTRXMLResult(objdt);

            objdt = (DataTable)ViewState["PRODUCTS"];
            objdt.TableName = "PRODUCTS";
            objProp.strProductDetails = GetSTRXMLResult(objdt);

            objProp.intEinNoderr = Convert.ToInt32(drpEin.SelectedValue);

            if (ViewState["GCNetWorth"] != null)
            {
                objdt = (DataTable)ViewState["GCNetWorth"];
                objdt.TableName = "GCNetWorth";
                objProp.strXmlGcNetWorth = GetSTRXMLResult(objdt);
            }

            ///DML Operation
            string strRetVal = objService.ProjectInfoAED(objProp);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objProp = null;
        }
    }
    private DataTable CreateDTOtherLocation()
    {
        try
        {
            DataTable dtTrans = new DataTable();
            DataColumn intProjectId = new DataColumn("intProjectId");
            intProjectId.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(intProjectId);

            DataColumn vchUnitName = new DataColumn("vchUnitName");
            vchUnitName.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchUnitName);

            DataColumn vchProduct = new DataColumn("vchProduct");
            vchProduct.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchProduct);

            DataColumn vchTotCapacity = new DataColumn("vchTotCapacity");
            vchTotCapacity.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchTotCapacity);

            DataColumn intStateId = new DataColumn("intStateId");
            intStateId.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(intStateId);

            DataColumn vchStateName = new DataColumn("vchStateName");
            vchStateName.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchStateName);

            DataColumn vchDistName = new DataColumn("vchDistName");
            vchDistName.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchDistName);

            DataColumn intDistId = new DataColumn("intDistId");
            intDistId.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(intDistId);

            ViewState["OTHERLOC"] = dtTrans;
            return dtTrans;
        }
        catch (Exception)
        {
            throw;
        }
    }
    private DataTable CreateDTOtherUnits()
    {
        try
        {
            DataTable dtTrans = new DataTable();
            DataColumn intUnitId = new DataColumn("intUnitId");
            intUnitId.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(intUnitId);

            DataColumn vchUnitName = new DataColumn("vchUnitName");
            vchUnitName.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchUnitName);

            DataColumn vchProduct = new DataColumn("vchProduct");
            vchProduct.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchProduct);

            DataColumn vchTotCapacity = new DataColumn("vchTotCapacity");
            vchTotCapacity.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchTotCapacity);

            DataColumn intCountryId = new DataColumn("intCountryId");
            intCountryId.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(intCountryId);

            DataColumn vchCountryName = new DataColumn("vchCountryName");
            vchCountryName.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchCountryName);

            DataColumn vchCityName = new DataColumn("vchCityName");
            vchCityName.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchCityName);

            ViewState["OTHERUNITS"] = dtTrans;
            return dtTrans;
        }
        catch (Exception)
        {
            throw;
        }
    }
    private DataTable CreateDTOProduct()
    {
        try
        {
            DataTable dtTrans = new DataTable();
            DataColumn intProductid = new DataColumn("intProductid");
            intProductid.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(intProductid);

            DataColumn vchProductName = new DataColumn("vchProductName");
            vchProductName.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchProductName);

            DataColumn vchProposedAnnualCapacity = new DataColumn("vchProposedAnnualCapacity");
            vchProposedAnnualCapacity.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchProposedAnnualCapacity);

            DataColumn vchProposedUnit = new DataColumn("vchProposedUnit");
            vchProposedUnit.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(vchProposedUnit);

            DataColumn intUnitId = new DataColumn("intUnitId");
            intUnitId.DataType = Type.GetType("System.String");
            dtTrans.Columns.Add(intUnitId);

            ViewState["PRODUCTS"] = dtTrans;
            return dtTrans;
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void GetData()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();
            objProp.strAction = "V";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProp.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objProp.vchProposalNo = Session["proposalno"].ToString();
            }

            /////Get Project Information
            objProjList = objService.getProjectInfoDetails(objProp).ToList();
            if (objProjList.Count > 0)
            {
                Session["id"] = "1";

                txtUnitName.Text = Convert.ToString(Server.HtmlDecode(objProjList[0].vchNameOfUnit));
                txtregApp.Text = Convert.ToString(objProjList[0].vchEINnIEMnIL);
                ddlSector.SelectedValue = Convert.ToString(objProjList[0].intSectorId);

                BindSubSector(objProjList[0].intSectorId.ToString());
                ddlSubSector.SelectedValue = Convert.ToString(objProjList[0].intSubSectorId.ToString());

                txtProposedAnnCapacity.Text = Convert.ToString(objProjList[0].decProposedAnnualCapacity);

                ddlUnit.SelectedValue = Convert.ToString(objProjList[0].vchUnit);
                if (ddlUnit.SelectedItem.Text == "Other")
                {
                    txtOtherUnit.Text = Convert.ToString(objProjList[0].vchOtherUnit);
                    DvSpecify.Visible = true;
                }

                txtlanddev.Text = Convert.ToString(objProjList[0].decLandIncLandDev);
                txtBuilding.Text = Convert.ToString(objProjList[0].decBuildingndConstruction);
                txtPlantMachinery.Text = Convert.ToString(objProjList[0].decPlantndMachinery);
                txtOthers.Text = Convert.ToString(objProjList[0].decOthers);
                txtTotalCapitalInv.Text = Convert.ToString(objProjList[0].decTotCapitalInvestment);
                ddlCommProdInMonth.SelectedValue = Convert.ToString(objProjList[0].intPeriodToCommenceProduction.ToString());
                ddlProjectcomingunder.SelectedValue = Convert.ToString(objProjList[0].vchProjectComingUnder);

                ddlPolutionCategory.SelectedValue = Convert.ToString(objProjList[0].vchPollutionCategory);
                txtGroundBreaking.Text = Convert.ToString(objProjList[0].intGroundBreaking);
                txtCivil.Text = Convert.ToString(objProjList[0].intCivilndStructuralCompln);
                txtEquipment.Text = Convert.ToString(objProjList[0].intMajorEquipmentErect);
                txtCommissioning.Text = Convert.ToString(objProjList[0].intStartOfCommercialProd);
                txtEquity.Text = Convert.ToString(objProjList[0].decEquityContribution);
                txtBankFinance.Text = Convert.ToString(objProjList[0].decBankndInstitutionalFin);
                txtTotal.Text = Convert.ToString(objProjList[0].decTotFinance);
                txtFDI.Text = Convert.ToString(objProjList[0].decForeignInvestment);
                txtIRR.Text = Convert.ToString(objProjList[0].vchIRR);
                txtDSCR.Text = Convert.ToString(objProjList[0].vchDSCR);
                txtManagerExist.Text = Convert.ToString(objProjList[0].intMangerExist);
                txtManagerProposed.Text = Convert.ToString(objProjList[0].intManagerProp);
                txtSupervisorExist.Text = Convert.ToString(objProjList[0].intSupervisorExist);
                txtSupervisorProposed.Text = Convert.ToString(objProjList[0].intSupervisorProp);
                txtSkilledExist.Text = Convert.ToString(objProjList[0].intSkilledExist);
                txtSkilledProposed.Text = Convert.ToString(objProjList[0].intSkilledProp);
                txtSemiskilledExist.Text = Convert.ToString(objProjList[0].intSemiSkilledExist);
                txtSemiskilledProposed.Text = Convert.ToString(objProjList[0].intSemiSkilledProp);
                txtUnskilledExist.Text = Convert.ToString(objProjList[0].intUnSkilledExist);
                txtUnskilledProposed.Text = Convert.ToString(objProjList[0].intUnSkilledProp);
                txtTotalExist.Text = Convert.ToString(objProjList[0].intTotalExist);
                txtTotalProposed.Text = Convert.ToString(objProjList[0].intTotalProp);
                txtDirectEmp.Text = Convert.ToString(objProjList[0].intPropDirectEmployment);
                txtContractualEmp.Text = Convert.ToString(objProjList[0].intPropContractualEmployment);
                drpEin.SelectedValue = Convert.ToString(objProjList[0].intEinNoderr);

                ViewState["ConstitutionId"] = Convert.ToString(objProjList[0].intConstitution); //// Added by Sushant Jena On Dt:-27-Aug-2019

                if (Convert.ToString(objProjList[0].vchIndustryInterprenur) != "")
                {
                    hdnIndustryEntMemorandum.Value = Convert.ToString(objProjList[0].vchIndustryInterprenur);
                    hlDoc1.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchIndustryInterprenur);
                    hlDoc1.Visible = true;
                    lnkDelIndustryEntMemorandum.Visible = true;
                    lblIndMemo.Visible = true;
                    FileIndustryEntMemorandum.Enabled = false;
                }

                if (Convert.ToString(objProjList[0].vchFeasibilityReport) != "")
                {
                    hdnFeasibilityReport.Value = Convert.ToString(objProjList[0].vchFeasibilityReport);
                    hlDoc3.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchFeasibilityReport);
                    hlDoc3.Visible = true;
                    lnkDelFeasibilityReport.Visible = true;
                    lblFeasibility.Visible = true;
                    FileFeasibilityReport.Enabled = false;
                }

                if (Convert.ToString(objProjList[0].vchBoardResolution) != "")
                {
                    hdnBoardResolution.Value = Convert.ToString(objProjList[0].vchBoardResolution);
                    hlDoc4.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchBoardResolution);
                    hlDoc4.Visible = true;
                    lnkDelBoardResolution.Visible = true;
                    lblBoardResolution.Visible = true;
                    FileBoardResolution.Enabled = false;
                }

                if (Convert.ToString(objProjList[0].vchSourceOfFinance) != "")
                {
                    hdnOtherFin.Value = Convert.ToString(objProjList[0].vchSourceOfFinance);
                    hlDoc5.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProjList[0].vchSourceOfFinance);
                    hlDoc5.Visible = true;
                    lnkDelOtherFin.Visible = true;
                    FileOtherFin.Enabled = false;
                    lblOtherFin.Visible = true;
                }

                /*---------------------------------------------------------------------*/

                objProp.strAction = "E1";
                objProjList = objService.getProjectLOCDetails(objProp).ToList();
                gvLOCDetails.DataSource = objProjList;
                gvLOCDetails.DataBind();

                if (objProjList.Count > 0)
                {
                    ddlprojloc.SelectedValue = "1";
                }
                else
                {
                    ddlprojloc.SelectedValue = "2";
                }

                DataTable dt = CreateDTOtherLocation();
                for (int i = 0; i <= gvLOCDetails.Rows.Count - 1; i++)
                {
                    HiddenField hdnid = (HiddenField)gvLOCDetails.Rows[i].FindControl("hdnid");

                    HiddenField hdnstateid = (HiddenField)gvLOCDetails.Rows[i].FindControl("hdnstateid");
                    HiddenField hdnDistid = (HiddenField)gvLOCDetails.Rows[i].FindControl("hdnDistid");
                    DataRow dr = dt.NewRow();
                    dr["intProjectId"] = hdnid.Value;
                    dr["vchUnitName"] = gvLOCDetails.Rows[i].Cells[1].Text;
                    dr["vchProduct"] = gvLOCDetails.Rows[i].Cells[2].Text;
                    dr["vchTotCapacity"] = gvLOCDetails.Rows[i].Cells[3].Text;
                    dr["vchStateName"] = gvLOCDetails.Rows[i].Cells[4].Text;
                    dr["vchDistName"] = gvLOCDetails.Rows[i].Cells[5].Text;
                    dr["intStateId"] = hdnstateid.Value;
                    dr["intDistId"] = hdnDistid.Value;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
                ViewState["OTHERLOC"] = dt;

                /*---------------------------------------------------------------------*/

                objProp.strAction = "E2";
                objProjList = objService.getOtherUnitlDetails(objProp).ToList();
                gvOtherUnits.DataSource = objProjList;
                gvOtherUnits.DataBind();
                if (objProjList.Count > 0)
                {
                    ddlprojectUnits.SelectedValue = "1";
                }
                else
                {
                    ddlprojectUnits.SelectedValue = "2";
                }

                DataTable dtraw = CreateDTOtherUnits();
                for (int i = 0; i <= gvOtherUnits.Rows.Count - 1; i++)
                {
                    HiddenField hdnid = (HiddenField)gvOtherUnits.Rows[i].FindControl("hdnid");
                    HiddenField hdncountryid = (HiddenField)gvOtherUnits.Rows[i].FindControl("hdncountryid");
                    DataRow dr = dtraw.NewRow();
                    dr["intUnitId"] = hdnid.Value;
                    dr["vchUnitName"] = gvOtherUnits.Rows[i].Cells[1].Text;
                    dr["vchProduct"] = gvOtherUnits.Rows[i].Cells[2].Text;
                    dr["vchTotCapacity"] = gvOtherUnits.Rows[i].Cells[3].Text;
                    dr["vchCountryName"] = gvOtherUnits.Rows[i].Cells[4].Text;
                    dr["intCountryId"] = hdncountryid.Value;
                    dr["vchCityName"] = gvOtherUnits.Rows[i].Cells[5].Text;
                    dtraw.Rows.Add(dr);
                    dtraw.AcceptChanges();
                }
                ViewState["OTHERUNITS"] = dtraw;

                /*---------------------------------------------------------------------*/

                objProp.strAction = "M2";
                objProjList = objService.getProductNameDetails(objProp).ToList();
                grdProduct.DataSource = objProjList;
                grdProduct.DataBind();

                DataTable dtraw1 = CreateDTOProduct();
                for (int i = 0; i <= grdProduct.Rows.Count - 1; i++)
                {
                    HiddenField hdnidproduct = (HiddenField)grdProduct.Rows[i].FindControl("hdnidproduct");
                    HiddenField hdnunitid = (HiddenField)grdProduct.Rows[i].FindControl("hdnunitid");

                    DataRow dr = dtraw1.NewRow();
                    dr["intUnitId"] = hdnunitid.Value;

                    dr["vchProductName"] = grdProduct.Rows[i].Cells[1].Text;
                    dr["vchProposedAnnualCapacity"] = grdProduct.Rows[i].Cells[2].Text;
                    dr["vchProposedUnit"] = grdProduct.Rows[i].Cells[3].Text;

                    dr["intProductid"] = hdnidproduct.Value;

                    dtraw1.Rows.Add(dr);
                    dtraw1.AcceptChanges();
                }
                ViewState["PRODUCTS"] = dtraw1;

                /*--------------------------------------------------------------------*/
                ///// For Group of Comapny Net Worth Details

                objProp.strAction = "GCNW";
                DataTable dtNW = new DataTable();
                dtNW = objService.GetGcNewWorthDetails(objProp);
                Grd_GC_Net_Worth.DataSource = dtNW;
                Grd_GC_Net_Worth.DataBind();
                ViewState["GCNetWorth"] = dtNW;

                /*--------------------------------------------------------------------*/
            }
            else
            {
                Session["id"] = "0";

                ///// Added by Sushant Jena on Dt:- 05-Apr-2021
                if (Session["NswsInvSwsId"] != null && Convert.ToString(Session["NswsInvSwsId"]) != "")
                {
                    ///// Pull State CAF from NSWS portal and Populate at respective places.
                    string strInvSwsId = Convert.ToString(Session["NswsInvSwsId"]);
                    PullStateCafNsws(strInvSwsId);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objProp = null;
        }
    }

    private void ClearValue3()
    {
        txtProductNamedet.Text = string.Empty;
        txtAnlCap.Text = string.Empty;
        drpProductUnit.SelectedValue = "0";
    }
    private void ClearValue()
    {
        txtUnit.Text = string.Empty;
        txtProduct.Text = string.Empty;
        txtCapacity.Text = string.Empty;
        ddlState.SelectedValue = "0";
        ddldist.SelectedValue = "0";
    }
    private void ClearValue1()
    {
        txtOtherUnitname.Text = string.Empty;
        txtOtherProd.Text = string.Empty;
        txtOthercapacity.Text = string.Empty;
        txtCityname.Text = string.Empty;
        ddlCountry.SelectedValue = "0";
    }

    private void BindState()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

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
        catch (Exception)
        {
            throw;
        }
    }
    private void BindDistrict(string strstate)
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "SD";
            objProp.vchProposalNo = strstate;
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddldist.DataSource = objProjList;
            ddldist.DataTextField = "vchDistName";
            ddldist.DataValueField = "intDistId";
            ddldist.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddldist.Items.Insert(0, list);
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void BindCountry()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "CT";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlCountry.DataSource = objProjList;
            ddlCountry.DataTextField = "vchCountryName";
            ddlCountry.DataValueField = "intCountryId";
            ddlCountry.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlCountry.Items.Insert(0, list);
            ddlCountry.Items.Remove(new ListItem("India", "1"));
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void BindUnits()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "UT";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlUnit.DataSource = objProjList;
            ddlUnit.DataTextField = "vchUnitName";
            ddlUnit.DataValueField = "intUnitId";
            ddlUnit.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select Unit--";
            list.Value = "0";
            ddlUnit.Items.Insert(0, list);
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void BindUnits1()
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "UT";
            objProp.vchProposalNo = "";
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            drpProductUnit.DataSource = objProjList;
            drpProductUnit.DataTextField = "vchUnitName";
            drpProductUnit.DataValueField = "intUnitId";
            drpProductUnit.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select Unit--";
            list.Value = "0";
            drpProductUnit.Items.Insert(0, list);
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void BindSector()
    {
        try
        {
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
            list.Text = "--Select--";
            list.Value = "0";
            ddlSector.Items.Insert(0, list);
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void BindSubSector(string strstate)
    {
        try
        {
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "SU";
            objProp.vchProposalNo = strstate;
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();
            ddlSubSector.DataSource = objProjList;
            ddlSubSector.DataTextField = "vchSectorName";
            ddlSubSector.DataValueField = "intSectorId";
            ddlSubSector.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlSubSector.Items.Insert(0, list);
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void BindCommercial()
    {
        try
        {
            for (int i = 6; i <= 60; i++)
            {
                ListItem list = new ListItem();
                ddlCommProdInMonth.Items.Add(new ListItem((i).ToString(), (i).ToString()));
            }
            ddlCommProdInMonth.Items.Insert(0, new ListItem() { Text = "-Select-", Value = "0" });
        }
        catch (Exception)
        {
            throw;
        }
    }
    private void DelBtnHideshow()
    {
        lnkDelIndustryEntMemorandum.Visible = false;
        lnkDelOtherFin.Visible = false;
        lnkDelMnfprocess.Visible = false;
        lnkDelFeasibilityReport.Visible = false;
        lnkDelBoardResolution.Visible = false;
    }

    /// <summary>
    /// To get total net worth provide.
    /// Total networth=Net worth last financial year (self)+net worth for group of company.
    /// Added by Sushant Jena On Dt: 27-Aug-2019
    /// </summary>
    /// <param name="strProposalNo"></param>
    /// <returns></returns>
    private decimal GetTotalNetWorth(string strProposalNo)
    {
        ProposalBAL objService = new ProposalBAL();
        ProjectInfo objProp = new ProjectInfo();
        decimal decTotalNetWorth = 0;

        try
        {
            DataTable dt = new DataTable();

            objProp.strAction = "TNW";
            objProp.vchProposalNo = strProposalNo;

            dt = objService.GetGcNewWorthDetails(objProp);
            if (dt.Rows.Count > 0)
            {
                decTotalNetWorth = Convert.ToDecimal(dt.Rows[0]["decTotalNetWorth"]);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objService = null;
            objProp = null;
        }

        return decTotalNetWorth;
    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string Position)
    {
        if (Position == "1")
        {
            string strPanFiletoRemove = hdn1.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }

        if (Position == "2")
        {
            string strPanFiletoRemove = hdn2.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }

        if (Position == "3")
        {
            string strPanFiletoRemove = hdn3.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }

        if (Position == "4")
        {
            string strPanFiletoRemove = hdn4.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }

        string filename = hdnFile.Value;
        string path = "~/Enclosure/" + filename;
        string completePath = Server.MapPath(path);
        if (System.IO.File.Exists(completePath))
        {
            System.IO.File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
    }
    private bool IsFileValidOtherFin(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));

        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileOtherFin.FileName) != ".pdf") && (Path.GetExtension(FileOtherFin.FileName) != ".png") && (Path.GetExtension(FileOtherFin.FileName) != ".jpg") && (Path.GetExtension(FileOtherFin.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload PDF,PNG,JPG,JPEG file Only!')", true);
                    return false;
                }

                int fileSize = FileOtherFin.PostedFile.ContentLength;
                if (fileSize > Maxsize)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }
                else
                {
                    FilePath = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "OtherSourceofFin" + Path.GetExtension(FileOtherFin.FileName), DateTime.Now);
                }

                if (!string.IsNullOrEmpty(FileOtherFin.FileName))
                {
                    allFileVal = allFileVal + FileOtherFin.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    hdn1.Value = FileOtherFin.FileName;
                    if (dir.Exists)
                    {
                        FileOtherFin.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileOtherFin.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }

                    hdnOtherFin.Value = FilePath;
                    hlDoc5.NavigateUrl = "~/Enclosure/" + FilePath;
                    hlDoc5.Visible = true;
                    lnkDelOtherFin.Visible = true;
                    FileOtherFin.Enabled = false;
                    lblOtherFin.Visible = true;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidBoardResolution(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileBoardResolution.FileName) != ".pdf") && (Path.GetExtension(FileBoardResolution.FileName) != ".png") && (Path.GetExtension(FileBoardResolution.FileName) != ".jpg") && (Path.GetExtension(FileBoardResolution.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload PDF,PNG,JPG,JPEG file Only!')", true);
                    return false;
                }

                int fileSize = FileBoardResolution.PostedFile.ContentLength;
                if (fileSize > Maxsize)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }
                else
                {
                    FilePath = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "BoardResolution" + Path.GetExtension(FileBoardResolution.FileName), DateTime.Now);
                }

                if (!string.IsNullOrEmpty(FileBoardResolution.FileName))
                {
                    allFileVal = allFileVal + FileBoardResolution.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    hdn4.Value = FileBoardResolution.FileName;
                    if (dir.Exists)
                    {
                        FileBoardResolution.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileBoardResolution.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }

                    hdnBoardResolution.Value = FilePath;
                    hlDoc4.NavigateUrl = "~/Enclosure/" + FilePath;
                    hlDoc4.Visible = true;
                    lnkDelBoardResolution.Visible = true;
                    lblBoardResolution.Visible = true;
                    FileBoardResolution.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidFeasibilityReport(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileFeasibilityReport.FileName) != ".pdf") && (Path.GetExtension(FileFeasibilityReport.FileName) != ".png") && (Path.GetExtension(FileFeasibilityReport.FileName) != ".jpg") && (Path.GetExtension(FileFeasibilityReport.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload PDF,PNG,JPG,JPEG file Only!')", true);
                    return false;
                }

                int fileSize = FileFeasibilityReport.PostedFile.ContentLength;
                if (fileSize > Maxsize)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }
                else
                {
                    FilePath = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "FeasibilityReport" + Path.GetExtension(FileFeasibilityReport.FileName), DateTime.Now);
                }

                if (!string.IsNullOrEmpty(FileFeasibilityReport.FileName))
                {
                    allFileVal = allFileVal + FileFeasibilityReport.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    hdn3.Value = FileFeasibilityReport.FileName;
                    if (dir.Exists)
                    {
                        FileFeasibilityReport.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileFeasibilityReport.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }

                    hdnFeasibilityReport.Value = FilePath;
                    hlDoc3.NavigateUrl = "~/Enclosure/" + FilePath;
                    hlDoc3.Visible = true;
                    lnkDelFeasibilityReport.Visible = true;
                    lblFeasibility.Visible = true;
                    FileFeasibilityReport.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidEntMemorandum(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileIndustryEntMemorandum.FileName) != ".pdf") && (Path.GetExtension(FileIndustryEntMemorandum.FileName) != ".png") && (Path.GetExtension(FileIndustryEntMemorandum.FileName) != ".jpg") && (Path.GetExtension(FileIndustryEntMemorandum.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload PDF,PNG,JPG,JPEG file Only!')", true);
                    return false;
                }
                int fileSize = FileIndustryEntMemorandum.PostedFile.ContentLength;
                if (fileSize > Maxsize)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }
                else
                {
                    FilePath = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "IndustryEntMemorandum" + Path.GetExtension(FileIndustryEntMemorandum.FileName), DateTime.Now);
                }

                if (!string.IsNullOrEmpty(FileIndustryEntMemorandum.FileName))
                {
                    allFileVal = allFileVal + FileIndustryEntMemorandum.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    hdn2.Value = FileIndustryEntMemorandum.FileName;
                    if (dir.Exists)
                    {
                        FileIndustryEntMemorandum.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileIndustryEntMemorandum.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    hdnIndustryEntMemorandum.Value = FilePath;
                    hlDoc1.NavigateUrl = "~/Enclosure/" + FilePath;
                    hlDoc1.Visible = true;
                    lnkDelIndustryEntMemorandum.Visible = true;
                    lblIndMemo.Visible = true;
                    FileIndustryEntMemorandum.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidGCNetWorth(FileUpload FileUpload1)
    {
        string strFiletype = "";
        string fileExt = "";
        int count = 0;

        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpload1.FileName) != ".pdf") && (Path.GetExtension(FileUpload1.FileName) != ".png") && (Path.GetExtension(FileUpload1.FileName) != ".jpg") && (Path.GetExtension(FileUpload1.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload PDF,PNG,JPG,JPEG file file Only!')", true);
                    return false;
                }
                int fileSize = FileUpload1.PostedFile.ContentLength;
                if (fileSize > (12 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 12 MB')", true);
                    return false;
                }

                FilePath = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "GCNetWorth" + Path.GetExtension(FileUpload1.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpload1.FileName))
                {
                    allFileVal = hdnAllFileValue.Value + FileUpload1.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpload1.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }

                    Hid_GC_New_Worth_File_Name.Value = FilePath;
                    Hyp_View_GC_New_Worth_Doc.NavigateUrl = "~/Enclosure/" + FilePath;
                    Hyp_View_GC_New_Worth_Doc.Visible = true;
                    LnkBtn_Delete_GC_New_Worth_Doc.Visible = true;
                    Lbl_Msg_GC_Net_Worth_Doc.Visible = true;
                    FileUpload1.Enabled = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }

    #endregion

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindDistrict(ddlState.SelectedValue);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void ddlSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSubSector(ddlSector.SelectedValue);
            List<ProjectInfo> objProjList = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "SR";
            objProp.vchProposalNo = ddlSector.SelectedValue;
            objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            ddlProjectcomingunder.SelectedValue = Convert.ToString(objProjList[0].intPriority);
            ddlProjectcomingunder.Enabled = false;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    protected void btnUnitAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt1 = new DataTable();
            dt1 = ViewState["OTHERUNITS"] as DataTable;
            DataTable dts = new DataTable();
            DataRow dr = null;

            dr = dt1.NewRow();

            dr["intUnitId"] = (dt1.Rows.Count + 1).ToString();
            dr["vchUnitName"] = txtOtherUnitname.Text;
            dr["vchProduct"] = txtOtherProd.Text;
            dr["vchTotCapacity"] = txtOthercapacity.Text;
            dr["vchCountryName"] = ddlCountry.SelectedItem.Text;
            dr["intCountryId"] = ddlCountry.SelectedValue;
            dr["vchCityName"] = txtCityname.Text;

            int flag1 = 0;

            Regex regex = new Regex(@"^[ A-Za-z0-9]*$");

            if (!regex.IsMatch(txtOtherUnitname.Text.ToString()))
            {

                flag1 = 1;
            }
            else if (!regex.IsMatch(txtOtherProd.Text.ToString()))
            {
                flag1 = 1;
            }
            else if (!regex.IsMatch(txtOthercapacity.Text.ToString()))
            {
                flag1 = 1;
            }
            else if (!regex.IsMatch(txtCityname.Text.ToString()))
            {
                flag1 = 1;
            }
            else
            {
                dt1.Rows.Add(dr);
            }


           
            dr = null;

            gvOtherUnits.DataSource = dt1;
            gvOtherUnits.DataBind();
            dt1.TableName = "OTHERUNITS";
            ViewState["OTHERUNITS"] = dt1;

            ClearValue1();

            if (flag1 == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Special characters not allowed.', '" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void AddMore_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt1 = new DataTable();
            dt1 = ViewState["OTHERLOC"] as DataTable;
            DataTable dts = new DataTable();
            DataRow dr = null;

            dr = dt1.NewRow();

            dr["intProjectId"] = (dt1.Rows.Count + 1).ToString();
            dr["vchUnitName"] = txtUnit.Text;
            dr["vchProduct"] = txtProduct.Text;
            dr["vchTotCapacity"] = txtCapacity.Text;
            dr["vchStateName"] = ddlState.SelectedItem.Text;
            dr["vchDistName"] = ddldist.SelectedItem.Text;
            dr["intStateId"] = ddlState.SelectedValue;
            dr["intDistId"] = ddldist.SelectedValue;

            int flag1 = 0;

            Regex regex = new Regex(@"^[ A-Za-z0-9]*$");

            if (!regex.IsMatch(txtUnit.Text.ToString()))
            {

                flag1 = 1;
            }
            else if (!regex.IsMatch(txtProduct.Text.ToString()))
            {
                flag1 = 1;
            }
            else if (!regex.IsMatch(txtCapacity.Text.ToString()))
            {
                flag1 = 1;
            }
            else
            {
                dt1.Rows.Add(dr);
            }


           
            dr = null;

            gvLOCDetails.DataSource = dt1;
            gvLOCDetails.DataBind();
            dt1.TableName = "OTHERLOC";
            ViewState["OTHERLOC"] = dt1;

            ClearValue();

            if (flag1 == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Special characters not allowed.', '" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void btnProduct_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt1 = new DataTable();
            dt1 = ViewState["PRODUCTS"] as DataTable;
            DataTable dts = new DataTable();
            DataRow dr = null;

            dr = dt1.NewRow();

            dr["intProductid"] = (dt1.Rows.Count + 1).ToString();

           

            
            if (txtAnlCap.Text != "")
            {
                dr["vchProposedAnnualCapacity"] = txtAnlCap.Text.Trim();
            }
            else
            {
                dr["vchProposedAnnualCapacity"] = "0";
            }

            if (drpProductUnit.SelectedValue == "4")
            {
                dr["vchProposedUnit"] = txtOtherProductUnit.Text.Trim();
            }
            else
            {
                dr["vchProposedUnit"] = drpProductUnit.SelectedItem.Text;
            }

            dr["intUnitid"] = drpProductUnit.SelectedValue;

            int flag1 = 0;

            Regex regex = new Regex(@"^[ A-Za-z0-9]*$");

            if (!regex.IsMatch(txtProductNamedet.Text.ToString()))
            {

                flag1 = 1;
            }
            else
            {
                dr["vchProductName"] = txtProductNamedet.Text.Trim();
                dt1.Rows.Add(dr);
            }

           
            dr = null;

            grdProduct.DataSource = dt1;
            grdProduct.DataBind();
            dt1.TableName = "PRODUCTS";
            ViewState["PRODUCTS"] = dt1;

            ClearValue3();

            if (flag1 == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Special characters not allowed.', '" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    protected void imgbtnDeleteproduct_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt2 = new DataTable();
            dt2 = ViewState["PRODUCTS"] as DataTable;
            ImageButton imgbtn = (ImageButton)sender;
            HiddenField hdnidproduct = (HiddenField)imgbtn.FindControl("hdnidproduct");

            DataRow[] dr1 = null;

            dr1 = dt2.Select("intProductid='" + hdnidproduct.Value + "'");

            for (int i = 0; i < dr1.Length; i++)
            {
                dr1[i].Delete();
            }

            dt2.AcceptChanges();
            grdProduct.DataSource = dt2;
            grdProduct.DataBind();
            ViewState["PRODUCTS"] = dt2;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt2 = new DataTable();
            dt2 = ViewState["OTHERLOC"] as DataTable;
            ImageButton imgbtn = (ImageButton)sender;
            HiddenField hdnid = (HiddenField)imgbtn.FindControl("hdnid");

            DataRow[] dr1 = null;

            dr1 = dt2.Select("intProjectId='" + hdnid.Value + "'");

            for (int i = 0; i < dr1.Length; i++)
            {
                dr1[i].Delete();
            }

            dt2.AcceptChanges();
            gvLOCDetails.DataSource = dt2;
            gvLOCDetails.DataBind();
            ViewState["OTHERLOC"] = dt2;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void imgbtnUnitDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt2 = new DataTable();
            dt2 = ViewState["OTHERUNITS"] as DataTable;
            ImageButton imgbtn = (ImageButton)sender;
            HiddenField hdnid = (HiddenField)imgbtn.FindControl("hdnid");

            DataRow[] dr1 = null;

            dr1 = dt2.Select("intUnitId='" + hdnid.Value + "'");

            for (int i = 0; i < dr1.Length; i++)
            {
                dr1[i].Delete();
            }

            dt2.AcceptChanges();
            gvOtherUnits.DataSource = dt2;
            gvOtherUnits.DataBind();
            ViewState["OTHERUNITS"] = dt2;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            #region ValidationCommented
            //string str = "";
            //if (txtregApp.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Alphabets_Num(txtregApp.Text.Trim(), "EIN/IEM/IL");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtProposedAnnCapacity.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtProposedAnnCapacity.Text.Trim(), "Proposed annual capacity");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtOtherUnit.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Alphabets(txtOtherUnit.Text.Trim(), "Others(Please specify)");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtlanddev.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtlanddev.Text.Trim(), "Land including land development");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtBuilding.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtBuilding.Text.Trim(), "Building & civil construction");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtPlantMachinery.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtPlantMachinery.Text.Trim(), "Plant & machinery");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtOthers.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtOthers.Text.Trim(), "Others");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtTotalCapitalInv.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtTotalCapitalInv.Text.Trim(), "Total capital investment");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtEquity.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtEquity.Text.Trim(), "Equity contribution");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtBankFinance.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtBankFinance.Text.Trim(), "Bank/institutional finance");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtTotal.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtTotal.Text.Trim(), "Total");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtFDI.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtFDI.Text.Trim(), "Foreign direct investment (if any)");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtIRR.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtIRR.Text.Trim(), "IRR");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtDSCR.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtDSCR.Text.Trim(), "DSCR");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtGroundBreaking.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtGroundBreaking.Text.Trim(), "Ground breaking");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtGroundBreaking.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtGroundBreaking.Text.Trim(), "Ground breaking");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}

            //if (txtCivil.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtCivil.Text.Trim(), "Civil and structural completion");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtEquipment.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtEquipment.Text.Trim(), "Major equipment erection");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtCommissioning.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtCommissioning.Text.Trim(), "Start of commercial production");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtManagerExist.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtManagerExist.Text.Trim(), "Managerial");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtManagerProposed.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtManagerProposed.Text.Trim(), "Proposed");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}

            //if (txtSupervisorExist.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtSupervisorExist.Text.Trim(), "Supervisory");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtSupervisorProposed.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtSupervisorProposed.Text.Trim(), "Proposed");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtSkilledExist.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtSkilledExist.Text.Trim(), "Skilled");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtSkilledProposed.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtSkilledProposed.Text.Trim(), "Skilled");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtSemiskilledExist.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtSemiskilledExist.Text.Trim(), "Semi skilled");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtSemiskilledProposed.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtSemiskilledProposed.Text.Trim(), "Semi skilled");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtUnskilledExist.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtUnskilledExist.Text.Trim(), "Un skilled");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtUnskilledProposed.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtUnskilledProposed.Text.Trim(), "Un skilled");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtTotalExist.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtTotalExist.Text.Trim(), "Total Exist");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtTotalProposed.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtTotalProposed.Text.Trim(), "Total Proposed");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtDirectEmp.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtDirectEmp.Text.Trim(), "Proposed direct employment (On company payroll)");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtContractualEmp.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtContractualEmp.Text.Trim(), "Proposed contractual employment");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}

            #endregion

            /*---------------------------------------------------------------------*/
            //// Equity Validation Section.(If the constitution is not Proprietorship)        
            //// Equity must be greater than or equal to (>=) 30% of total Capital Investment.
            //// If equity is less than 30% of Total capital investment,then display a alert message with Yes/No button.
            //// If user click on YES button then allow it proceed and in next phase check equity vs networth validation(Code inside Btn_Yes_Click).
            //// If user click on NO button then simply close the alert message and allow user to change the values.       
            /*---------------------------------------------------------------------*/

            if (Convert.ToInt32(ViewState["ConstitutionId"]) != 1)
            {
                decimal decTotalCapInvest = Convert.ToDecimal(txtTotalCapitalInv.Text);
                decimal decEquity = Convert.ToDecimal(txtEquity.Text);

                //// If equity less 30% of total capital investment
                if (decEquity < (decTotalCapInvest * Convert.ToDecimal(0.3)))
                {
                    txtEquity.BorderColor = System.Drawing.Color.Red;
                    txtEquity.Focus();
                    ModalPopupExtender1.Show();
                    return;
                }

                /*---------------------------------------------------------------------*/
                //// Net Worth validation section.    

                string strProposalNo = "";
                if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
                {
                    strProposalNo = Request.QueryString["StrPropNo"];
                }
                else
                {
                    strProposalNo = Session["proposalno"].ToString();
                }

                //// Get Total Net Worth against Proposal Number (Provided in 1st Page)
                decimal decTotalNetWorth = GetTotalNetWorth(strProposalNo);

                decimal decAdditionalNetWorth = 0;
                if (ViewState["GCNetWorth"] != null)
                {
                    DataTable dt = (DataTable)ViewState["GCNetWorth"];
                    if (dt.Rows.Count > 0)
                    {
                        decAdditionalNetWorth = Convert.ToDecimal(dt.Compute("SUM(decNetWorth)", string.Empty));
                    }
                }
                decTotalNetWorth = decTotalNetWorth + decAdditionalNetWorth;

                if (decTotalNetWorth < decEquity)
                {
                    txtEquity.BorderColor = System.Drawing.ColorTranslator.FromHtml("#F1F1F1");
                    Txt_GC_Company_Name.BorderColor = System.Drawing.Color.Red;
                    Txt_GC_Company_Name.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Net worth is less than the equity provided. Please submit additional source of net worth to match the equity.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                    return;
                }

                int intTotalProposed = Convert.ToInt32(txtTotalProposed.Text);
                int intDirectEmp=Convert.ToInt32(txtDirectEmp.Text);
                if (intTotalProposed<=0)
                {
                    txtManagerProposed.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Proposed Employment Potential is blankl.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                    return;
                }
                if (intDirectEmp <= 0)
                {
                    txtManagerProposed.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Proposed direct employment (On company payroll) is blankl.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                    return;
                }

            }

            /*---------------------------------------------------------------------*/

            AddProjectInfo();
            Session["AllFileValue"] = hdnAllFileValue.Value;
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                Response.Redirect("landdetails.aspx?StrPropNo=" + Request.QueryString["StrPropNo"].ToString());
            }
            else
            {
                Response.Redirect("landdetails.aspx");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["AllFileValue"] = hdnAllFileValue.Value;
        if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            Response.Redirect("PromoterDetails.aspx?StrPropNo=" + Request.QueryString["StrPropNo"].ToString());
        }
        else
        {
            Response.Redirect("PromoterDetails.aspx");
        }

    }
    protected void btnSaveAsdraft_Click(object sender, EventArgs e)
    {
        try
        {
            AddProjectInfoAsDraft();
            Session["AllFileValue"] = hdnAllFileValue.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Record(s) saved successfully.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("proposeddetails.aspx");
    }

    /// <summary>
    /// This button is used confirm equity vs net worth validation.
    /// Added by Sushant Jena On Dt:-27-Aug-2019
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Yes_Click(object sender, EventArgs e)
    {
        try
        {
            decimal decEquity = Convert.ToDecimal(txtEquity.Text);
            string strProposalNo = "";
            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                strProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                strProposalNo = Session["proposalno"].ToString();
            }

            //// Get Total Net Worth against Proposal Number
            decimal decTotalNetWorth = GetTotalNetWorth(strProposalNo);
            decimal decAdditionalNetWorth = 0;
            if (ViewState["GCNetWorth"] != null)
            {
                DataTable dt = (DataTable)ViewState["GCNetWorth"];
                if (dt.Rows.Count > 0)
                {
                    decAdditionalNetWorth = Convert.ToDecimal(dt.Compute("SUM(decNetWorth)", string.Empty));
                }
            }
            decTotalNetWorth = decTotalNetWorth + decAdditionalNetWorth;

            if (decTotalNetWorth < decEquity)
            {
                Txt_GC_Company_Name.BorderColor = System.Drawing.Color.Red;
                Txt_GC_Company_Name.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Net worth is less than the equity provided. Please submit additional source of net worth to match the equity.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }
            else
            {
                AddProjectInfo();
                Session["AllFileValue"] = hdnAllFileValue.Value;
                if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
                {
                    Response.Redirect("landdetails.aspx?StrPropNo=" + Request.QueryString["StrPropNo"].ToString());
                }
                else
                {
                    Response.Redirect("landdetails.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    protected void lnkIndustryEntMemorandum_Click(object sender, EventArgs e)
    {
        SetValueOfAllControls();
        try
        {
            IsFileValidEntMemorandum(FileIndustryEntMemorandum);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
            Util.LogError(ex, "PEAL");
        }
        finally
        {

        }
    }
    protected void lnkFeasibilityReport_Click(object sender, EventArgs e)
    {
        SetValueOfAllControls();
        try
        {
            IsFileValidFeasibilityReport(FileFeasibilityReport);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
            Util.LogError(ex, "PEAL");
        }
        finally
        {

        }
    }

    protected void lnkBoardResolution_Click(object sender, EventArgs e)
    {
        SetValueOfAllControls();
        try
        {
            IsFileValidBoardResolution(FileBoardResolution);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
            Util.LogError(ex, "PEAL");
        }
        finally
        {

        }
    }
    protected void lnkOtherFin_Click(object sender, EventArgs e)
    {
        SetValueOfAllControls();
        try
        {
            IsFileValidOtherFin(FileOtherFin);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
            Util.LogError(ex, "PEAL");
        }
        finally
        {

        }
    }
    protected void lnkDelIndustryEntMemorandum_Click(object sender, EventArgs e)
    {
        try
        {
            UpdFileRemove(hdnIndustryEntMemorandum, lnkIndustryEntMemorandum, lnkDelIndustryEntMemorandum, hlDoc1, lblIndMemo, FileIndustryEntMemorandum, "2");
            if (!string.IsNullOrEmpty(Session["proposalno"] as string))
            {
                objProposal.vchProposalNo = Session["proposalno"].ToString();
                objProposal.strAction = "J";
                string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
                objProposal.strAction = "J";
                string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    protected void lnkDelFeasibilityReport_Click(object sender, EventArgs e)
    {
        try
        {
            UpdFileRemove(hdnFeasibilityReport, lnkFeasibilityReport, lnkDelFeasibilityReport, hlDoc3, lblFeasibility, FileFeasibilityReport, "3");
            if (!string.IsNullOrEmpty(Session["proposalno"] as string))
            {
                objProposal.vchProposalNo = Session["proposalno"].ToString();
                objProposal.strAction = "L";
                string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
                objProposal.strAction = "L";
                string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void lnkDelBoardResolution_Click(object sender, EventArgs e)
    {
        try
        {
            UpdFileRemove(hdnBoardResolution, lnkBoardResolution, lnkDelBoardResolution, hlDoc4, lblBoardResolution, FileBoardResolution, "4");
            if (!string.IsNullOrEmpty(Session["proposalno"] as string))
            {
                objProposal.vchProposalNo = Session["proposalno"].ToString();
                objProposal.strAction = "M";
                string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
                objProposal.strAction = "M";
                string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void lnkDelOtherFin_Click(object sender, EventArgs e)
    {
        try
        {
            UpdFileRemove(hdnOtherFin, lnkOtherFin, lnkDelOtherFin, hlDoc5, lblOtherFin, FileOtherFin, "1");
            if (!string.IsNullOrEmpty(Session["proposalno"] as string))
            {
                objProposal.vchProposalNo = Session["proposalno"].ToString();
                objProposal.strAction = "N";
                string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
                objProposal.strAction = "N";
                string strRetVal = objService.ProposalEnclosurUpdate(objProposal);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }
    protected void LnkBtn_Delete_GC_New_Worth_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            UpdFileRemove(Hid_GC_New_Worth_File_Name, LnkBtn_Upload_GC_New_Worth_Doc, LnkBtn_Delete_GC_New_Worth_Doc, Hyp_View_GC_New_Worth_Doc, Lbl_Msg_GC_Net_Worth_Doc, FU_GC_New_Worth, "13");
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    protected void LnkBtn_Upload_GC_New_Worth_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidGCNetWorth(FU_GC_New_Worth);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
            Util.LogError(ex, "PEAL");
        }
        finally
        {

        }
    }

    /// <summary>
    /// Added by Sushant Jena On Dt:27-Aug-2019
    /// Addmore Button to Add Group of Company Net Worth details for different group of companies.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Add_GC_Net_Worth_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            /*------------------------------------------------------*/

            #region ValidationSection

            if (Txt_GC_Company_Name.Text.Trim() == "")
            {
                Txt_GC_Company_Name.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please enter group of company name.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }

            


            if (Txt_GC_Net_Worth.Text.Trim() == "")
            {
                Txt_GC_Net_Worth.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please enter net worth of last financial year.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }
            if (Hid_GC_New_Worth_File_Name.Value == "")
            {
                FU_GC_New_Worth.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload document related to networth.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }

            #endregion

            /*------------------------------------------------------*/

            dt.Columns.Add("vchCompanyName", typeof(string));
            dt.Columns.Add("decNetWorth", typeof(decimal));
            dt.Columns.Add("vchNetWorthDoc", typeof(string));
            int flag1 = 0;

            Regex regex = new Regex(@"^[ A-Za-z0-9]*$");

            if (!regex.IsMatch(Txt_GC_Company_Name.Text.ToString()))
            {

                flag1 = 1;
            }
            else
            {
                dt.Rows.Add(Txt_GC_Company_Name.Text, Txt_GC_Net_Worth.Text, Hid_GC_New_Worth_File_Name.Value);
            }

            
            for (int i = 0; i < Grd_GC_Net_Worth.Rows.Count; i++)
            {
                Label Lbl_GC_Company_Name_G = (Label)Grd_GC_Net_Worth.Rows[i].FindControl("Lbl_GC_Company_Name_G");
                Label Lbl_GC_Net_Worth_G = (Label)Grd_GC_Net_Worth.Rows[i].FindControl("Lbl_GC_Net_Worth_G");
                HiddenField Hid_GC_Net_Worth_File_Name_G = (HiddenField)Grd_GC_Net_Worth.Rows[i].FindControl("Hid_GC_Net_Worth_File_Name_G");
                dt.Rows.Add(Lbl_GC_Company_Name_G.Text, Lbl_GC_Net_Worth_G.Text, Hid_GC_Net_Worth_File_Name_G.Value);
            }

            ViewState["GCNetWorth"] = dt;
            Grd_GC_Net_Worth.DataSource = dt;
            Grd_GC_Net_Worth.DataBind();

            Txt_GC_Company_Name.Text = "";
            Txt_GC_Net_Worth.Text = "";
            Hid_GC_New_Worth_File_Name.Value = "";

            FU_GC_New_Worth.Enabled = true;
            LnkBtn_Delete_GC_New_Worth_Doc.Visible = false;
            Hyp_View_GC_New_Worth_Doc.Visible = false;
            Lbl_Msg_GC_Net_Worth_Doc.Visible = false;


            if (flag1 == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Special characters not allowed.', '" + Messages.TitleOfProject + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
        finally
        {
            dt = null;
        }
    }

    /// <summary>
    /// Added by Sushant Jena On Dt:27-Aug-2019
    /// Remove Group of Company Networth details after Add More.  
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImgBtn_Delete_GC_Net_Worth_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchCompanyName", typeof(string));
            table.Columns.Add("decNetWorth", typeof(decimal));
            table.Columns.Add("vchNetWorthDoc", typeof(string));

            for (int i = 0; i < Grd_GC_Net_Worth.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_GC_Company_Name_G = (Label)Grd_GC_Net_Worth.Rows[i].FindControl("Lbl_GC_Company_Name_G");
                    Label Lbl_GC_Net_Worth_G = (Label)Grd_GC_Net_Worth.Rows[i].FindControl("Lbl_GC_Net_Worth_G");
                    HiddenField Hid_GC_Net_Worth_File_Name_G = (HiddenField)Grd_GC_Net_Worth.Rows[i].FindControl("Hid_GC_Net_Worth_File_Name_G");
                    table.Rows.Add(Lbl_GC_Company_Name_G.Text, Lbl_GC_Net_Worth_G.Text, Hid_GC_Net_Worth_File_Name_G.Value);
                }
            }

            ViewState["GCNetWorth"] = table;
            Grd_GC_Net_Worth.DataSource = table;
            Grd_GC_Net_Worth.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
        finally
        {
            table = null;
        }
    }

    /// <summary>
    /// Added by Sushant Jena On Dt:-27-Aug-2019
    /// Gridview rowdatabound to assign file path for group of company net worth document
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Grd_GC_Net_Worth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_GC_Net_Worth_File_Name_G = (HiddenField)e.Row.FindControl("Hid_GC_Net_Worth_File_Name_G");
            HyperLink Hyp_View_GC_Doc = (HyperLink)e.Row.FindControl("Hyp_View_GC_Doc");

            Hyp_View_GC_Doc.NavigateUrl = "~/Enclosure/" + Hid_GC_Net_Worth_File_Name_G.Value;
        }
    }

    /// <summary>
    /// Added by Sushant Jena On Dt:- 05-04-2021.
    /// This method is used to auto-populate NSWS State CAF details in this page.
    /// This method will be executed only when the user is coming from NSWS portal.
    /// </summary>
    /// <param name="strInvestorSwsId"></param>
    private void PullStateCafNsws(string strInvestorSwsId)
    {
        try
        {
            objProposal.strAction = "B";
            objProposal.strInvestorSWSId = strInvestorSwsId;
            DataTable dt = new DataTable();
            dt = objService.GetCAFDetailsNSWS(objProposal);
            if (dt.Rows.Count > 0)
            {
                /*--------------------------------------------------------------*/
                ///Project Information
                /*--------------------------------------------------------------*/
                ddlSector.SelectedIndex = -1;
                ddlSector.SelectedValue = Convert.ToString(dt.Rows[0]["vchSector"]);
                BindSubSector(ddlSector.SelectedValue);
                ddlSubSector.SelectedValue = Convert.ToString(dt.Rows[0]["vchSubSector"]);

                if (Convert.ToString(dt.Rows[0]["vchIsPriority"]) == "Yes")
                {
                    ddlProjectcomingunder.SelectedValue = "1";
                }
                else
                {
                    ddlProjectcomingunder.SelectedValue = "0";
                }

                string strEINIEMType = Convert.ToString(dt.Rows[0]["vchEINIEMType"]);
                string strEINIEMDoc = "";
                if (strEINIEMType.ToUpper() == "EIN")
                {
                    drpEin.SelectedValue = "1";
                    txtregApp.Text = Convert.ToString(dt.Rows[0]["vchEINNo"]);
                    strEINIEMDoc = Convert.ToString(dt.Rows[0]["vchEINDoc"]);
                }
                else if (strEINIEMType.ToUpper() == "IEM")
                {
                    drpEin.SelectedValue = "2";
                    txtregApp.Text = Convert.ToString(dt.Rows[0]["vchIEMNo"]);
                    strEINIEMDoc = Convert.ToString(dt.Rows[0]["vchIEMDoc"]);
                }
                //else  if (strEINIEMType.ToUpper() == "IL")
                //{
                //    drpEin.SelectedValue = "3";
                //    txtregApp.Text = Convert.ToString(dt.Rows[0]["vchEINNo"]);
                //}
                else if (strEINIEMType.ToUpper() == "UDYOG AADHAAR")
                {
                    drpEin.SelectedValue = "4";
                    txtregApp.Text = Convert.ToString(dt.Rows[0]["vchUAadhaarNo"]);
                }

                /*============================================================================================*/
                /// Download EIN/IEM/IL/Udyog Aadhar document from NSWS portal and display in PEAL form. 
                /*============================================================================================*/
                if (strEINIEMDoc != "")
                {
                    DownloadDocFromNSWS(strEINIEMDoc, "IndustryEntMemorandum", hdnIndustryEntMemorandum, lnkDelIndustryEntMemorandum, hlDoc1, FileIndustryEntMemorandum, lblIndMemo);
                }

                //hdnIndustryEntMemorandum.Value = FilePath;
                //hlDoc1.NavigateUrl = "~/Enclosure/" + FilePath;
                //hlDoc1.Visible = true;
                //lnkDelIndustryEntMemorandum.Visible = true;
                //lblIndMemo.Visible = true;
                //FileIndustryEntMemorandum.Enabled = false;

                txtAnlCap.Text = Convert.ToString(dt.Rows[0]["decPropAnnualCapacity"]);
                drpProductUnit.SelectedValue = Convert.ToString(dt.Rows[0]["vchUnitCapacity"]);
                txtProductNamedet.Text = Convert.ToString(dt.Rows[0]["vchProductName"]);
                txtUnitName.Text = Convert.ToString(dt.Rows[0]["vchUnitName"]);
                //else if (fieldName == "Is the Project coming under Priority Sector")     //vchIsPriority VARCHAR(16), 

                /*--------------------------------------------------------------*/
                ///Proposed Capital Investment
                /*--------------------------------------------------------------*/
                string strLandIncLandDev = Convert.ToString(dt.Rows[0]["decLandIncLandDev"]);
                string strBuildingndConstruction = Convert.ToString(dt.Rows[0]["decBuildingndConstruction"]);
                string strPlantAndMachinery = Convert.ToString(dt.Rows[0]["decPlantAndMachinery"]);
                string strOthers = Convert.ToString(dt.Rows[0]["decOthers"]);

                decimal decLandIncLandDev = 0;
                decimal decBuildingndConstruction = 0;
                decimal decPlantAndMachinery = 0;
                decimal decOthers = 0;

                if (strLandIncLandDev != "")
                {
                    txtlanddev.Text = strLandIncLandDev;
                    decLandIncLandDev = Convert.ToDecimal(strLandIncLandDev);
                }

                if (strBuildingndConstruction != "")
                {
                    txtBuilding.Text = strBuildingndConstruction;
                    decBuildingndConstruction = Convert.ToDecimal(strBuildingndConstruction);
                }

                if (strPlantAndMachinery != "")
                {
                    txtPlantMachinery.Text = strPlantAndMachinery;
                    decPlantAndMachinery = Convert.ToDecimal(strPlantAndMachinery);
                }

                if (strOthers != "")
                {
                    txtOthers.Text = strOthers;
                    decOthers = Convert.ToDecimal(strOthers);
                }

                txtTotalCapitalInv.Text = Convert.ToString(decLandIncLandDev + decBuildingndConstruction + decPlantAndMachinery + decOthers);

                string strPollutionCategory = Convert.ToString(dt.Rows[0]["vchPollutionCategory"]);
                if (strPollutionCategory != "")
                {
                    ddlPolutionCategory.SelectedValue = strPollutionCategory;
                }               

                ddlCommProdInMonth.SelectedValue = Convert.ToString(dt.Rows[0]["intPeriodToCommenceProduction"]);

                /*--------------------------------------------------------------*/
                ///Means of Finance for Capital Investment
                /*--------------------------------------------------------------*/
                string strEquityContribution = Convert.ToString(dt.Rows[0]["decEquityContribution"]);
                string strBankAndInstitutionalFin = Convert.ToString(dt.Rows[0]["decBankAndInstitutionalFin"]);

                decimal decEquityContribution = 0;
                decimal decBankAndInstitutionalFin = 0;

                if (strEquityContribution != "")
                {
                    txtEquity.Text = strEquityContribution;
                    decEquityContribution = Convert.ToDecimal(strEquityContribution);
                }

                if (strBankAndInstitutionalFin != "")
                {
                    txtBankFinance.Text = strBankAndInstitutionalFin;
                    decBankAndInstitutionalFin = Convert.ToDecimal(strBankAndInstitutionalFin);
                }

                txtTotal.Text = Convert.ToString(decEquityContribution + decBankAndInstitutionalFin);
                txtFDI.Text = Convert.ToString(dt.Rows[0]["decFDI"]);

                //else if (fieldName == "In case of FDI please upload relevant document")
                //{
                //    vchFDIDoc
                //}
                //else if (fieldName == "IRR")
                //{
                //    vchIRR
                //}
                //else if (fieldName == "DSCR")
                //{
                //    vchDSCR
                //}

                /*--------------------------------------------------------------*/
                ///Project Implementation Schedule
                /*--------------------------------------------------------------*/
                txtEquipment.Text = Convert.ToString(dt.Rows[0]["intMajorEquipmentErection"]);
                txtGroundBreaking.Text = Convert.ToString(dt.Rows[0]["intGroundBreaking"]);
                txtCommissioning.Text = Convert.ToString(dt.Rows[0]["intStartOfCommercialProd"]);
                txtCivil.Text = Convert.ToString(dt.Rows[0]["intCivilCompletion"]);

                /*============================================================================================*/
                ///Download Feasibility Report document from NSWS portal and display in PEAL form. 
                /*============================================================================================*/
                string strFeasibilityReportDoc = Convert.ToString(dt.Rows[0]["vchFeasibilityReportDoc"]);
                if (strFeasibilityReportDoc != "")
                {
                    DownloadDocFromNSWS(strFeasibilityReportDoc, "FeasibilityReport", hdnFeasibilityReport, lnkDelFeasibilityReport, hlDoc3, FileFeasibilityReport, lblFeasibility);
                }

                //hdnFeasibilityReport.Value = FilePath;
                //hlDoc3.NavigateUrl = "~/Enclosure/" + FilePath;
                //hlDoc3.Visible = true;
                //lnkDelFeasibilityReport.Visible = true;
                //lblFeasibility.Visible = true;
                //FileFeasibilityReport.Enabled = false;

                /*============================================================================================*/
                ///Download Board Resolution document from NSWS portal and display in PEAL form. 
                /*============================================================================================*/
                string strBoardResolutionDoc = Convert.ToString(dt.Rows[0]["vchBoardResolutionDoc"]);
                if (strBoardResolutionDoc != "")
                {
                    DownloadDocFromNSWS(strBoardResolutionDoc, "BoardResolution", hdnBoardResolution, lnkDelBoardResolution, hlDoc4, FileBoardResolution, lblBoardResolution);
                }

                //hdnBoardResolution.Value = FilePath;
                //hlDoc4.NavigateUrl = "~/Enclosure/" + FilePath;
                //hlDoc4.Visible = true;
                //lnkDelBoardResolution.Visible = true;
                //lblBoardResolution.Visible = true;
                //FileBoardResolution.Enabled = false;

                /*--------------------------------------------------------------*/
                ///Employment Potential
                /*--------------------------------------------------------------*/
                txtManagerProposed.Text = Convert.ToString(dt.Rows[0]["intManagerialProp"]);
                txtSupervisorProposed.Text = Convert.ToString(dt.Rows[0]["intSupervisoryProp"]);
                txtSkilledProposed.Text = Convert.ToString(dt.Rows[0]["intSkilledProp"]);
                txtSemiskilledProposed.Text = Convert.ToString(dt.Rows[0]["intSemiSkilledProp"]);
                txtUnskilledProposed.Text = Convert.ToString(dt.Rows[0]["intUnSkilledProp"]);

                int intTotalEmp = Convert.ToInt32(txtManagerProposed.Text) + Convert.ToInt32(txtSupervisorProposed.Text) + Convert.ToInt32(txtSkilledProposed.Text) + Convert.ToInt32(txtSemiskilledProposed.Text) + Convert.ToInt32(txtUnskilledProposed.Text);
                txtTotalProposed.Text = Convert.ToString(intTotalEmp);

                txtDirectEmp.Text = Convert.ToString(dt.Rows[0]["intPropDirectEmployment"]);
                if (txtDirectEmp.Text.Trim() != "" && txtTotalProposed.Text.Trim() != "")
                {
                    if (Convert.ToInt32(txtDirectEmp.Text.Trim()) > Convert.ToInt32(txtTotalProposed.Text.Trim()))
                    {
                        txtDirectEmp.Text = "";
                        txtContractualEmp.Text = "";
                    }
                }

                /*--------------------------------------------------------------*/
                ///Projects at other Locations
                /*--------------------------------------------------------------*/
                string strOutsideIndia = Convert.ToString(dt.Rows[0]["vchProjOutsideIndia"]);
                if (strOutsideIndia == "Yes")
                {
                    ddlprojectUnits.SelectedValue = "1";
                }
                else if (strOutsideIndia == "No")
                {
                    ddlprojectUnits.SelectedValue = "2";
                }

                string strOtherLocIndia = Convert.ToString(dt.Rows[0]["vchProjOtherLocIndia"]);
                if (strOtherLocIndia == "Yes")
                {
                    ddlprojloc.SelectedValue = "1";
                }
                else if (strOtherLocIndia == "No")
                {
                    ddlprojloc.SelectedValue = "2";
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void DownloadDocFromNSWS(string strNswsFileName, string strFileType, HiddenField HdnFileName, LinkButton LnkBtnDelete, HyperLink HypLnkView, FileUpload FileUpd, Label LblMsg)
    {
        try
        {
            string strFileContentId = JsonConvert.DeserializeObject<Dictionary<string, object>>(strNswsFileName.Replace("[", "").Replace("]", ""))["value"].ToString();

            /*---------------------------------------------------------------------------------------*/
            ///Get the API address,access-id,access-secret and api key from web configuration file.
            /*---------------------------------------------------------------------------------------*/
            string strPullDocApiUrl = ConfigurationManager.AppSettings["NswsPullDocApiUrl"].ToString();
            string strAccessId = ConfigurationManager.AppSettings["NswsApiAccessId"].ToString();
            string strAccessSecret = ConfigurationManager.AppSettings["NswsApiAccessSecret"].ToString();
            string strApiKeyPullDoc = ConfigurationManager.AppSettings["NswsApiKeyPullDoc"].ToString();
            /*---------------------------------------------------------------------------------------*/

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var client = new RestClient(strPullDocApiUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("access-id", strAccessId);
            request.AddHeader("access-secret", strAccessSecret);
            request.AddHeader("api-key", strApiKeyPullDoc);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"contentId\":[\"" + strFileContentId + "\"]}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            string strCmnLogMsg = "File Type:- " + strFileType + " <----> File Content Id:- " + strFileContentId;

            string strFileResposnse = response.Content.Length > 200 ? response.Content.ToString().Substring(0, 200) : response.Content.ToString();
            Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> Response Content:- " + strFileResposnse);

            if (response.Content.ToString() != "")
            {
                PullApiDoc objApp = JsonConvert.DeserializeObject<PullApiDoc>(response.Content);

                string strStatus = objApp.status;
                if (strStatus == "200")
                {
                    List<DocResponseFile> objDocRes = new List<DocResponseFile>();
                    objDocRes = objApp.response.ToList();

                    string strFileName = objDocRes[0].fileName;
                    string strFileResponse = objDocRes[0].fileResponse; ////Byte stream of the file to be downloaded.

                    byte[] data = Convert.FromBase64String(strFileResponse);
                    if (data.Length > 0)
                    {
                        if (IsFileValidNSWS(data, strFileName))
                        {
                            /*--------------------------------------------------------------*/
                            ///Rename the file as per the GOSWIFT naming format.
                            /*--------------------------------------------------------------*/
                            string strFileExtention = Path.GetExtension(strFileName);
                            string strGoswiftFileName = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + strFileType + strFileExtention, DateTime.Now);

                            /*--------------------------------------------------------------*/
                            ///Physical path of GOSWIFT document folder.
                            /*--------------------------------------------------------------*/
                            string strPath = Server.MapPath("~/Enclosure/");

                            /*--------------------------------------------------------------*/
                            ///Save the file to destination folder
                            /*--------------------------------------------------------------*/
                            FileStream fileStream = null;
                            fileStream = new FileStream(strPath + strGoswiftFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            using (System.IO.FileStream fs = fileStream)
                            {
                                fs.Write(data, 0, data.Length);

                                HdnFileName.Value = strGoswiftFileName;
                                HypLnkView.NavigateUrl = "~/Enclosure/" + strGoswiftFileName;
                                HypLnkView.Visible = true;
                                LnkBtnDelete.Visible = true;
                                LblMsg.Visible = true;
                                FileUpd.Enabled = false;
                            }
                        }
                        else
                        {
                            Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> Invalid or corrupted file found");
                        }
                    }
                    else
                    {
                        Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> No file found for download.");
                    }
                }
                else
                {
                    Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> No file found (404 Error).");
                }
            }
            else
            {
                Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> No response found for file.");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public class PullApiDoc
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<DocResponseFile> response { get; set; }
    }
    public class DocResponseFile
    {
        public string fileName { get; set; }
        public string fileResponse { get; set; }
    }

    private bool IsFileValidNSWS(byte[] filebyte, string strFileName)
    {
        try
        {
            StringCollection imageTypes = new StringCollection();
            StringCollection imageExtension = new StringCollection();

            string[] allowedMimeTypes = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedMimeTypes);
            imageExtension.AddRange(allowedExtension);
            string strFileMimeType = MimeType.GetMimeType(filebyte, strFileName);
            string strFileExt = System.IO.Path.GetExtension(strFileName);
            int intDotCount = strFileName.Count(f => f == '.');

            if (imageTypes.Contains(strFileMimeType) == true && imageExtension.Contains(strFileExt.ToLower()) && intDotCount == 1)
            {
                return true;

                //int fileSize = FileUpldPan.PostedFile.ContentLength;
                //if (fileSize > (4 * 1024 * 1024))
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                //    return false;
                //} 
            }
            else
            {
                Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strFileName + " ---- " + "File Mime type is not Correct.");
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}