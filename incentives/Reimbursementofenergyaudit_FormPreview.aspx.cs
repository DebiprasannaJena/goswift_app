using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using EntityLayer.Incentive;
using System.Collections.Generic;
public partial class incentives_Reimbursementofenergyaudit_FormPreview : SessionCheck
{
    Incentive objincUnit = new Incentive();
    string gFilePath = "../incentives/Files";
    DataTable dtSalutation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FetchAllFieldCotents();
            TRVisibility();
        }
    }
    public void FetchAllFieldCotents()
    {
        try
        {
            objincUnit = new Incentive();
            objincUnit.GetVwPrmtrs = new GetAndViewParam();
            objincUnit.GetVwPrmtrs.Param1ID = "";//////Incentive Number
            objincUnit.GetVwPrmtrs.Param2ID = "";//////--UnitCode
            objincUnit.GetVwPrmtrs.Param3ID = "";/////--Proposal/Peal/PC Number
            objincUnit.GetVwPrmtrs.InctType = 4;/////--Form type 4(for Pre Insentive) 5(for Post Incentive)
            objincUnit.PCNum = "";
            objincUnit.UnitCode = "";
            //objincUnit.Userid = 33;//// to be passed from session
            objincUnit.strcActioncode = "A";
            objincUnit.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            hdnId.Value = Request.QueryString["InctUniqueNo"].ToString();
            objincUnit.FormType = FormNumber.OneTimeReimbursementOfEnergyAuditCost_13;

            DataSet dslive = IncentiveManager.GetIncentiveOneTmReim(objincUnit);
            DataTable dtindustryPre = dslive.Tables[0];          //industry panel
            DataTable dtInvestmentPre = dslive.Tables[1];        //investment details

            DataTable dtMeansFinancePre = dslive.Tables[11];      //Means of Finance
            DataTable dtMoFTermLoanPre = dslive.Tables[12];       //Means of Finance Term Loan
            DataTable dtMoFWorkingLoanPre = dslive.Tables[13];    //Means of Finance Working Loan
            dtSalutation = dslive.Tables[15];
            hdnEmail.Value = dtSalutation.Rows[0]["VCH_EMAIL"].ToString();
            hdnMobile.Value = dtSalutation.Rows[0]["VCH_OFF_MOBILE"].ToString();

            DataTable dtContractDemand = dslive.Tables[2];
            DataTable dtEnergyAudit = dslive.Tables[4];///////////investment details
            DataTable dtAvaildt1 = dslive.Tables[5];
            DataTable dtAvaildt2 = dslive.Tables[6];
            DataTable dtAvaildt3 = dslive.Tables[7];
            DataTable dtAdditional = dslive.Tables[8];
            DataTable dtContract = dslive.Tables[10];
            DataTable dtMainTable = dslive.Tables[14];///////////M_INCT_APPLICATION Table
            DataTable dtstatus = dslive.Tables[16];///////////production & employment 
            string draftStatus = dtstatus.Rows[0]["status"].ToString();

            FillIndustry(dtindustryPre);
            FillInvestment(dtInvestmentPre);
            FillMeansOfFinance(dtMeansFinancePre, dtMoFTermLoanPre, dtMoFWorkingLoanPre);
            FillContractDemand(dtContract);
            FillEnergyDetails(dtEnergyAudit, draftStatus);
            FillAdditionalDoc(dtAdditional);
            FillAvailed(dtAvaildt1, dtAvaildt3, dtAvaildt2);
            if (dtContractDemand.Rows[0]["VCHDEMANDFILE"].ToString() != "")
            {
                lbkViewContractDemand.HRef = "../incentives/Files/ContractDemand/" + dtContractDemand.Rows[0]["VCHDEMANDFILE"].ToString();
            }
            string creatby = dtMainTable.Rows[0]["INTCREATEDBY"].ToString();
            //if (creatby != Convert.ToString(Session["InvestorId"]))
            //{
            //}
            string bitflag = dtMainTable.Rows[0]["BITFLAG"].ToString();
            if (bitflag == "1")
            {
                HdnValueFlag.Value = "1";
                if (dtMainTable.Rows[0]["VCHSIGNATURE"].ToString() != "")
                {
                    PreviewImage.Attributes.Add("src", "../incentives/Files/Signature/" + dtMainTable.Rows[0]["VCHSIGNATURE"].ToString());
                    PreviewImage.Attributes.Add("style", "display:block");
                }
            }

            Incentive objIncentive = new Incentive();
            objIncentive.strcActioncode = "M";
            objIncentive.IncentiveNum = dtMainTable.Rows[0]["VCHINCENTIVENO"].ToString();

            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveMaster(objIncentive);
            lblDate.Text = RetDateFrmDB(dtMainTable.Rows[0]["DTMCREATEDBY"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtbPostSubFlag = ds.Tables[0];

                lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
                lblTitle2.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #region Industrial Unit

    public void FillIndustry(DataTable dtindustryPre)
    {
        #region IndustrailUnit
        if (dtindustryPre.Rows.Count > 0)
        {
            lblMr.Text = dtindustryPre.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
            TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHPREAPPLICANTNAME"].ToString().Trim();
            lblApplyBy.Text = (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1") ? "Self" : "Authorized Person";
            if (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1")
            {
                if (dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                {
                    divadhhardetails.Visible = true;
                    lblAadharNo.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString();
                }
                else
                {
                    divadhhardetails.Visible = false;
                }
                //authorise.Visible = false;
            }
            else
            {
                divadhhardetails.Visible = false;
                if (dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString() != "")
                {
                    //authorise.Visible = true;
                    //LnkView_Org_Doc_TypeDoc.Attributes.Add("href", RetFileNamePath(dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc"));

                    divAuthorizing.Visible = true;
                    hypAUTHORIZEDFILE.Attributes.Add("href", RetFileNamePath(dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc"));
                }
                else
                {
                    divAuthorizing.Visible = false;
                    //authorise.Visible = false;
                }
            }
            lblName.Text = dtindustryPre.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();
            lblUnitAddress.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
            lblDistrict.Text = dtindustryPre.Rows[0]["distname"].ToString();
            lblDist.Text = dtindustryPre.Rows[0]["distname"].ToString();
            //lblauthority.Text = (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1") ? "Self" : dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();

            lblUnitAddr.Text = dtindustryPre.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
            lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
            DataSet ds1 = new DataSet();
            ds1 = IncentiveManager.dynamic_name_doc_bind();
            ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intUnitType"].ToString() + "'";
            ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intOrganisationType"].ToString() + "'";
            DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
            if (dt.Rows.Count > 0)
            {
                Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                Lbl_Org_Doc_Type.Text = dt.Rows[0]["vch_orgdocumentname"].ToString();
            }
            else
            {
                Lbl_Org_Name_Type.Text = "Name of Managing Partner";
            }
            lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
            lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
            lblAddress.Text = lbl_Industry_Address.Text;
            lblPresent.Text = lbl_Industry_Address.Text;
            lbl_Unit_Cat.Text = dtindustryPre.Rows[0]["Unitcategoryname"].ToString();
            Lbl_Pioneer_Doc_Name.Text = dtindustryPre.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();
            dt = (ds1.Tables[1].DefaultView).ToTable();
            if (dt.Rows.Count > 0)
            {
                string strDocType = dt.Rows[0]["vchDocumentType"].ToString();
                if (strDocType != "")
                {
                    Div_Unit_Type_Doc.Visible = true;
                    Lbl_Unit_Type_Doc_Name.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();

                    Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();
                }
                else
                {
                    Div_Unit_Type_Doc.Visible = false;
                    Lbl_Unit_Type_Doc_Name.Text = "";
                }
            }
            else
            {
                Div_Unit_Type_Doc.Visible = false;
                Lbl_Unit_Type_Doc_Name.Text = "";
            }
            lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();

            if (dtindustryPre.Rows[0]["intPriority"].ToString() == "1")
            {
                lblIs_Priority.Text = "Yes";
                Pioneersec.Visible = true;
                DivPioneer.Visible = true;

            }
            else
            {
                lblIs_Priority.Text = "No";
                Pioneersec.Visible = false;
                DivPioneer.Visible = false;
            }
            if (dtindustryPre.Rows[0]["intPioneer"].ToString() == "1")
            {
                lblIs_Is_Pioneer.Text = "Yes";

            }
            else
            {
                lblIs_Is_Pioneer.Text = "No";

            }
            Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchPioneerCertificate"].ToString();

            lbl_Regd_Office_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();
            //if (dtindustryPre.Rows[0]["GenderType"].ToString() == "1")
            //{
            //    lbl_Gender_Partner.Text = "Mr." + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            //}
            //else
            //{
            //    lbl_Gender_Partner.Text = "Ms." + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            //}
            lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            Lbl_Org_Doc_Type.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
            Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
            lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
            lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
            lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
            lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
            lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();

            //if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
            //{
            //    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
            //}
            //else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            //{
            //    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
            //}
            if (dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString() != "")
            {
                divbefor.Visible = true;
                divbefor2.Visible = true;
                //tr_Prod_Comm_Before.Visible = true;
                Approved_DPR_Before_Doc_Name.Visible = true;

            }
            else
            {
                divbefor.Visible = false;
                divbefor2.Visible = false;
                //tr_Prod_Comm_Before.Visible = false;
                Approved_DPR_Before_Doc_Name.Visible = false;
                lblAfterEMD11.Text = "Date of Production Commencement";
                lblAfterEMD189.Text = "PC Issuance Date";
                lbl_PC_No_After.Text = "PC No";
                Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                lblEMDInvestment.Text = "";
            }
            lbl_pcno_befor.Text = dtindustryPre.Rows[0]["vchpcnobefore"].ToString();
            lblGstin.Text = dtindustryPre.Rows[0]["VCHGSTIN"].ToString();
            lbl_Prod_Comm_Date_After.Text = dtindustryPre.Rows[0]["dtmProdCommAfter"].ToString();
            lbl_PC_Issue_Date_After.Text = dtindustryPre.Rows[0]["dtmPCIssueDateAfter"].ToString();
            if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
            {
                if (dtindustryPre.Rows[0]["vchappnoAft"].ToString() != "")
                {
                    CertiDtCommen.Visible = true;
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
                }
                else
                {
                    CertiDtCommen.Visible = false;
                }
            }
            else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            {
                if (dtindustryPre.Rows[0]["vchappnoAft"].ToString() != "")
                {
                    CertiDtCommen.Visible = true;
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
                }
                else
                {
                    CertiDtCommen.Visible = false;
                }
            }

            lbl_District.Text = dtindustryPre.Rows[0]["distname"].ToString();
            lbl_Sector.Text = dtindustryPre.Rows[0]["sectorName"].ToString();
            lbl_Sub_Sector.Text = dtindustryPre.Rows[0]["SubsectorName"].ToString();
            Lbl_Derived_Sector.Text = dtindustryPre.Rows[0]["vchDerivedSector"].ToString();

            if (dtindustryPre.Rows[0]["bitPriorityIPR"].ToString() == "1")
            {
                lbl_Sectoral.Text = "Yes";
            }
            else
            {
                lbl_Sectoral.Text = "No";
            }
        }
        #endregion
    }

    #endregion
    #region Investment

    public void FillInvestment(DataTable dtInvestmentPre)
    {

        Txt_FFCI_Date_Before.Text = dtInvestmentPre.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString();
        Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();
        Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString();
        lbl_Land_Before.Text = dtInvestmentPre.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
        lbl_Building_Before.Text = dtInvestmentPre.Rows[0]["DEC_Building"].ToString();
        lbl_Plant_Mach_Before.Text = dtInvestmentPre.Rows[0]["DEC_Plant_Machinery"].ToString();
        lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPre.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
        lbl_Total_Capital_Before.Text = dtInvestmentPre.Rows[0]["DEC_Total"].ToString();
        if (dtInvestmentPre.Rows[0]["vchProjectDocBeforeCodeName"].ToString() == "")
        {
            Approved_DPR_Before_Doc_Name.Visible = false;
        }
        else
        {
            Approved_DPR_Before_Doc_Name.Visible = true;
            Lbl_Approved_DPR_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocBeforeCodeName"].ToString();
            Hyp_View_Approved_DPR_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocBefore"].ToString();
        }
        lbl_FFCI_Date_After.Text = dtInvestmentPre.Rows[0]["dtmFFCIDateAfter"].ToString();

        if (dtInvestmentPre.Rows[0]["vchFFCIDocAfterCodeName"].ToString() == "")
        {
            View_FFCI_After_Doc.Visible = false;
        }
        else
        {
            View_FFCI_After_Doc.Visible = true;
            Lbl_FFCI_After_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocAfterCodeName"].ToString();
            Hyp_View_FFCI_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocAfter"].ToString();
        }
        lbl_Land_After.Text = dtInvestmentPre.Rows[0]["decLandAmtAfter"].ToString();
        lbl_Building_After.Text = dtInvestmentPre.Rows[0]["decBuildingAmtAfter"].ToString();
        lbl_Plant_Mach_After.Text = dtInvestmentPre.Rows[0]["decPlantMachAmtAfter"].ToString();
        lbl_Other_Fixed_Asset_After.Text = dtInvestmentPre.Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
        lbl_Total_Capital_After.Text = dtInvestmentPre.Rows[0]["decTotalAmtAfter"].ToString();
        if (dtInvestmentPre.Rows[0]["vchProjectDocAfterCodeName"].ToString() == "")
        {
            Div_Approved_DPR_After_Doc.Visible = false;
        }
        else
        {
            Div_Approved_DPR_After_Doc.Visible = true;
            Lbl_Approved_DPR_After_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocAfterCodeName"].ToString();
            Hyp_View_Approved_DPR_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocAfter"].ToString();
        }
    }
    #endregion
    #region MEANS OF FINANCE
    public void FillMeansOfFinance(DataTable dtMeansFinancePre, DataTable dtMoFTermLoanPre, DataTable dtMoFWorkingLoanPre)
    {
        lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
        lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
        Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();
        lbl_FDI_Componet.Text = dtMeansFinancePre.Rows[0]["decFDIComponet"].ToString();
        Lbl_Term_Loan_Doc_Name.Text = dtMeansFinancePre.Rows[0]["vchTermLoanDocCodeNAme"].ToString();
        if (dtMoFTermLoanPre.Rows.Count > 0)
        {
            Grd_TL.DataSource = dtMoFTermLoanPre;
            Grd_TL.DataBind();
        }

        if (dtMoFWorkingLoanPre.Rows.Count > 0)
        {
            Grd_WC.DataSource = dtMoFWorkingLoanPre;
            Grd_WC.DataBind();
        }
    }
    #endregion
    public string RetDateFrmDB(string srcDate)
    {
        string retdt = "";
        try
        {
            if (srcDate != "")
            {
                DateTime dbdt = Convert.ToDateTime(srcDate);
                retdt = dbdt.ToString("dd-MMM-yyyy");

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return retdt;
    }
    #region "FillAvailed Details"
    public void FillAvailed(DataTable dtavail, DataTable dtavailgrd1, DataTable dtavailgrd2)
    {
        if (dtavail.Rows.Count > 0)
        {
            lbldiffclaimamt.Text = dtavail.Rows[0]["decClaimExempted"].ToString();
            lblreimamt.Text = dtavail.Rows[0]["decClaimReimbursement"].ToString();

            if (Convert.ToInt32(dtavail.Rows[0]["intNeverAvailedPrior"]) == 1)
            {
                lblSubsidyEarlier.Text = "Yes";
                av1.Visible = true;
                av2.Visible = true;
                Sanc.Visible = true;
                UnderTkg.Visible = false;
                if (dtavail.Rows[0]["VchSanctionDoc"].ToString() != "")
                {
                    lnkviewSanction.HRef = "../incentives/Files/AvailDetails/" + dtavail.Rows[0]["VchSanctionDoc"].ToString();
                }
            }
            else
            {
                lblSubsidyEarlier.Text = "No";
                av1.Visible = false;
                av2.Visible = false;
                Sanc.Visible = false;
                UnderTkg.Visible = true;
                if (dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString() != "")
                {
                    lnkviewUnderTkg.HRef = "../incentives/Files/AvailDetails/" + dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                }
            }
        }
        if (dtavailgrd1.Rows.Count > 0)
        {
            DataTable dtincentive = new DataTable();
            dtincentive = (DataTable)ViewState["dtincentive"];

            grdAssistanceDetailsAD.DataSource = dtavailgrd1;
            grdAssistanceDetailsAD.DataBind();
        }
    }
    #endregion
    #region Fill Contract Deman
    public void FillContractDemand(DataTable dtcontract)
    {
        if (dtcontract.Rows.Count > 0)
        {
            lblConnected.Text = dtcontract.Rows[0]["VCHCONNECTEDLOAD"].ToString();
            lblConsumerNo.Text = dtcontract.Rows[0]["VCHCONSUMENUMBER"].ToString();
        }
    }
    #endregion
    #region Industry Details Bind
    public string ReturnUnitTypeName(string typeid)
    {
        string UnitTypeName = "--";
        try
        {
            switch (typeid)
            {
                case "1":
                    UnitTypeName = "Existing E/M/D";
                    break;
                case "2":
                    UnitTypeName = "New Unit";
                    break;
                case "3":
                    UnitTypeName = "Migrated Unit Treated As New";
                    break;
                case "4":
                    UnitTypeName = "Rehabilitated Unit Treated As New";
                    break;

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return UnitTypeName;
    }
    public string ReturnUnitCategory(string categoryid)
    {
        string CategoryName = "--";
        try
        {

            switch (categoryid)
            {
                case "1":
                    CategoryName = "Micro";
                    break;
                case "2":
                    CategoryName = "Small";
                    break;
                case "3":
                    CategoryName = "Medium";
                    break;
                case "4":
                    CategoryName = "Large";
                    break;

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return CategoryName;
    }
    public string RetFileNamePath(string filename, string foldername)
    {
        string strret = "javascript:void(0)";
        try
        {
            if (filename != "")
            {
                strret = "../incentives/Files/" + foldername + "/" + filename;
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return strret;
    }
    #endregion
    #region Energy Detail

    public void FillEnergyDetails(DataTable dtEnergy, string strStatus)
    {
        if (dtEnergy.Rows.Count > 0)
        {

            lblEnergyNm.Text = dtEnergy.Rows[0]["strEnergyAuditorName"].ToString();
            lblEnergyAddress.Text = dtEnergy.Rows[0]["strEnergyAuditorAddress"].ToString();
            lblAccredi.Text = dtEnergy.Rows[0]["strEnergyAuditorAccreditation"].ToString();
            lblExpendi.Text = dtEnergy.Rows[0]["strExpenditureincurred"].ToString();
            lblcompletDt.Text = dtEnergy.Rows[0]["dtmSuccessfulcompletionAuditDate"].ToString();
            lblBefAudit.Text = dtEnergy.Rows[0]["strEnergyConsumptionBefore"].ToString();
            lblAftAudit.Text = dtEnergy.Rows[0]["dtmEnergyConsumptionAfter"].ToString();

            if (dtEnergy.Rows[0]["strSupportofimplementationofEnergyDoc"].ToString() != "")
            {
                LnkViewEnergyAuditReport.Attributes.Add("href", RetFileNamePath(dtEnergy.Rows[0]["strSupportofimplementationofEnergyDoc"].ToString(), "EneryAuditDetail")); ///// Document in support of implementation of Energy Audit Report
            }
            if (dtEnergy.Rows[0]["strSuccessfulcompletionAuditDoc"].ToString() != "")
            {
                LnkViewcompletionEnergy.Attributes.Add("href", RetFileNamePath(dtEnergy.Rows[0]["strSuccessfulcompletionAuditDoc"].ToString(), "EneryAuditDetail")); /////  Date of completion of successful implementation Energy Audit
            }


            if (dtEnergy.Rows[0]["strEnergyAuditorDocName"].ToString() != "")
            {
                hypAuditor.Attributes.Add("href", RetFileNamePath(dtEnergy.Rows[0]["strEnergyAuditorDocName"].ToString(), "EneryAuditDetail")); /////Name of Energy Auditor / Organization Doc
            }

            if (dtEnergy.Rows[0]["strEnergyAuditorAccreditationDoc"].ToString() != "")
            {
                hypLinkAccreditation.Attributes.Add("href", RetFileNamePath(dtEnergy.Rows[0]["strEnergyAuditorAccreditationDoc"].ToString(), "EneryAuditDetail")); /////Accreditation of the Auditor Doc
            }


            if (dtEnergy.Rows[0]["strExpenditureincurredDoc"] != "")
            {
                lnkviewExpenditureIncurred.Attributes.Add("href", RetFileNamePath(dtEnergy.Rows[0]["strExpenditureincurredDoc"].ToString(), "EneryAuditDetail")); /////  Expenditure incurred Doc
            }

            if (dtEnergy.Rows[0]["strReductionOfEnergyDoc"].ToString() != "")
            {
                lnkviewReduction.Attributes.Add("href", RetFileNamePath(dtEnergy.Rows[0]["strReductionOfEnergyDoc"].ToString(), "EneryAuditDetail")); /////Document(s) / proof on reduction of Energy expenses.
            }

            if (dtEnergy.Rows[0]["strEnergyEfficiencyCertificate"].ToString() != "")
            {
                lnkviewCertificateindep.Attributes.Add("href", RetFileNamePath(dtEnergy.Rows[0]["strEnergyEfficiencyCertificate"].ToString(), "EneryAuditDetail")); ///// Certificate on energy efficiency and reduction of carbon footprint by independent and credible third part agency
            }

            if (dtEnergy.Rows[0]["strCarbonFootprintDoc"].ToString() != "")
            {
                TrCarbonfootprint.Visible = true;
                lnkviewCarbonfootprint.Attributes.Add("href", RetFileNamePath(dtEnergy.Rows[0]["strCarbonFootprintDoc"].ToString(), "EneryAuditDetail")); ///// Certificate on energy efficiency and reduction of carbon footprint by independent and credible third part agency
            }
            else
            {
                TrCarbonfootprint.Visible = false;

            }
        }
    }
    #endregion
    #region Fill Additional Document

    public void FillAdditionalDoc(DataTable dtAdditionalDoc)
    {
        if (dtAdditionalDoc.Rows.Count > 0)
        {

            if (dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString() != "")
            {
                HypValidStatutary.NavigateUrl = RetFilePath(dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString(), "AdditionalDocument");
            }

            if (dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString() != "")
            {
                HypDelay.NavigateUrl = RetFilePath(dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString(), "AdditionalDocument");
            }
            if (dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString() != "")
            {
                HypCleanApproveAuthority.NavigateUrl = RetFilePath(dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString(), "AdditionalDocument");
            }
        }
    }
    #endregion
    public string RetFilePath(string filename, string foldername)
    {
        string strret = "javascript:void(0)";
        try
        {
            if (filename != "")
            {
                strret = "../incentives/Files/" + foldername + "/" + filename;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return strret;
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {

        Incentive objEntity = new Incentive();
        try
        {
            string[] fileext = { ".png", ".jpg", ".jpeg" };
            if (FluSign.HasFile)
            {
                if (fileext.Contains(System.IO.Path.GetExtension(FluSign.FileName).ToLower()))
                {
                    bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/Signature/"));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath("../incentives/Files/Signature/"));

                    string extension = Path.GetExtension(FluSign.PostedFile.FileName);
                    string filename = "Signature" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
                    FluSign.SaveAs(Server.MapPath("../incentives/Files/Signature/") + filename);
                    objEntity.Signature = filename;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please upload .png/.jpg/.jpeg format image only !', 'SWP'); </script>", false);
                    return;
                }
                objEntity.ApprovalAction = "A";
                objEntity.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
                int i = IncentiveManager.UpdateSignature(objEntity);
                SMSEmailContent();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + Messages.ShowMessage("1") + "');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Upload Signature !', 'SWP'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    private void SMSEmailContent()
    {
        try
        {
            CommonHelperCls objcomm = new CommonHelperCls();
            string strSubject = "GO-SWIFT: Application submitted successfully";
            string PreviewURL = System.Configuration.ConfigurationManager.AppSettings["PreviewURL"];
            string strBody = lblTitle.Text + " of M/s " + lblMr.Text + " has been submitted successfully. " + PreviewURL;
            string SMSContent = strSubject;

            var myList = new List<string>();
            myList.Add(hdnEmail.Value.ToString());
            string[] tomail = myList.ToArray();

            bool mailStatus = objcomm.sendMail(strSubject, strBody, tomail, true);
            bool smsStatus = objcomm.SendSmsNew(hdnMobile.Value, SMSContent);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #region TRDisplay
    /// <summary>
    /// To make Tr visible false if respective Label Text is blank
    /// by GS Chhotray 17/11/2017
    /// </summary>
    /// <param name="lbtext"></param>
    /// <param name="trid"></param>
    protected void TRVisibility()
    {
        try
        {


            TrDisplayNone(Lbl_Unit_Type_Doc_Name.Text, Div_Unit_Type_Doc);
            TrDisplayNone(Lbl_Pioneer_Doc_Name.Text, DivPioneer);
            //TrDisplayNone(Lbl_Prod_Comm_Before_Doc_Name.Text, tr_Prod_Comm_Before);
            TrDisplayNone(Lbl_FFCI_Before_Doc_Name.Text, FFCI_Before_Doc_Name);
            TrDisplayNone(Lbl_Approved_DPR_Before_Doc_Name.Text, Approved_DPR_Before_Doc_Name);
            TrDisplayNone(Lbl_Prod_Comm_After_Doc_Name.Text, CertiDtCommen);
            TrDisplayNone(Lbl_FFCI_After_Doc_Name.Text, View_FFCI_After_Doc);
            TrDisplayNone(Lbl_Approved_DPR_After_Doc_Name.Text, Div_Approved_DPR_After_Doc);
            //TrDisplayNone(Lbl_Org_Doc_Type.Text, authorise);
            TrDisplayNone(Lbl_Term_Loan_Doc_Name.Text, tr_Term_Loan_Doc_Name);
        }
        catch (Exception)
        {
        }
    }
    /// <summary>
    /// To make Tr visible false if respective Label Text is blank
    /// by GS Chhotray 17/11/2017
    /// </summary>
    /// <param name="lbtext"></param>
    /// <param name="trid"></param>
    public void TrDisplayNone(string lbtext, System.Web.UI.HtmlControls.HtmlTableRow trid)
    {
        try
        {
            if (lbtext.Trim() == "")
            {
                trid.Visible = false;
            }
            else
            {
                trid.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
}