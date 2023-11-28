using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.Collections.Generic;

public partial class incentives_PQC100 : SessionCheck
{

    DataTable dtSalutation;
    #region Populate Common-Field Pre/Post
    #region viewdetail
    public void PostpopulateData(int id)
    {
        DataSet dslivePre = IncentiveManager.PostpopulateData(id);
        DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
        DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment
        DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
        DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
        DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
        DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
        DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
        DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan
        dtSalutation = dslivePre.Tables[8];///////////For Mail Id and Mobile number
        hdnEmail.Value = dtSalutation.Rows[0]["VCH_EMAIL"].ToString();
        hdnMobile.Value = dtSalutation.Rows[0]["VCH_OFF_MOBILE"].ToString();

        #region IndustrailUnit
        if (dtindustryPre.Rows.Count > 0)
        {
            lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
            lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
            lblMr.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString().Trim();
            lblAddress.Text = lbl_Industry_Address.Text;
            lblPresent.Text = lbl_Industry_Address.Text;
            lblDistrict.Text = dtindustryPre.Rows[0]["distname"].ToString();
            lblDist.Text = dtindustryPre.Rows[0]["distname"].ToString();
            lblName.Text = dtindustryPre.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();
            lblUnitAddress.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
            lblauthority.Text = (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1") ? "Self" : dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
            lblUnitAddr.Text = dtindustryPre.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
            TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHPREAPPLICANTNAME"].ToString().Trim();
            lblApplyBy.Text = (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1") ? "Self" : "Authorized Person";

            divadhhardetails.Visible = false;
            authorise.Visible = false;
            DivAppDocType.Visible = false;

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
                authorise.Visible = false;
            }
            else
            {

                divadhhardetails.Visible = false;
                if (dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString() != "")
                {
                    authorise.Visible = true;
                    LnkView_Org_Doc_TypeDoc.Attributes.Add("href", RetFileNamePath(dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc"));
                    lblDocumentType.Text = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    lblauthority.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
                }
                else
                {
                    authorise.Visible = false;
                }
            }

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

            lblAddress.Text = lbl_Industry_Address.Text;
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
                divbefor1.Visible = true;
                divbefor2.Visible = true;
                //tr_Prod_Comm_Before.Visible = true;
                divEmp_Before_Doc_Name.Visible = true;
                Approved_DPR_Before_Doc_Name.Visible = true;
            }
            else
            {
                divbefor.Visible = false;
                divbefor1.Visible = false;
                divbefor2.Visible = false;
                //tr_Prod_Comm_Before.Visible = false;
                divEmp_Before_Doc_Name.Visible = false;
                Approved_DPR_Before_Doc_Name.Visible = false;
                lblAfterEMD11.Text = "Date of Production Commencement";
                lblAfterEMD189.Text = "PC Issuance Date";
                lbl_PC_No_After.Text = "PC No";
                lblemd.Text = "";
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
        #region Production

        Grd_Production_Before.DataSource = dtProductionDetBefPre;
        Grd_Production_Before.DataBind();
        Grd_Production_After.DataSource = dtProductionDetAftPre;
        Grd_Production_After.DataBind();

        lbl_Direct_Emp_Before.Text = dtProductionPre.Rows[0]["intDirectEmpBefore"].ToString();
        lbl_Contract_Emp_Before.Text = dtProductionPre.Rows[0]["intContractualEmpBefore"].ToString();
        lbl_Managarial_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDMANAGERIAL"].ToString();
        lbl_Supervisor_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDSUPERVISORY"].ToString();
        lbl_Skilled_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDSKILLED"].ToString();
        lbl_Semi_Skilled_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDSEMISKILLED"].ToString();
        lbl_Unskilled_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDUNSKILLED"].ToString();
        lbl_Total_Emp_Before.Text = dtProductionPre.Rows[0]["INTPROPOSEDTOTAL"].ToString();
        lbl_General_Before.Text = dtProductionPre.Rows[0]["intGeneralBefore"].ToString();
        lbl_SC_Before.Text = dtProductionPre.Rows[0]["intSCBefore"].ToString();
        lbl_ST_Before.Text = dtProductionPre.Rows[0]["intSTBefore"].ToString();
        lbl_Total_Cast_Emp_Before.Text = dtProductionPre.Rows[0]["intTotalEmpCastBefore"].ToString();
        lbl_Women_Before.Text = dtProductionPre.Rows[0]["intWomenBefore"].ToString();
        lbl_PHD_Before.Text = dtProductionPre.Rows[0]["intDisabledBefore"].ToString();
        lbl_Direct_Emp_After.Text = dtProductionPre.Rows[0]["intDirectEmpAfter"].ToString();
        lbl_Contract_Emp_After.Text = dtProductionPre.Rows[0]["intContractualEmpAfter"].ToString();
        if (dtProductionPre.Rows[0]["vchEmpDocBeforeCodeName"].ToString() == "")
        {
            divEmp_Before_Doc_Name.Visible = false;
        }
        else
        {
            divEmp_Before_Doc_Name.Visible = true;
            Lbl_Direct_Emp_Before_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocBeforeCodeName"].ToString();
            Hyp_View_Direct_Emp_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["VCHEMPDOC"].ToString();
        }

        if (dtProductionPre.Rows[0]["vchEmpDocAfterCodeName"].ToString() == "")
        {
            divEmp_Emp_After_Doc_Name.Visible = false;
        }
        else
        {
            divEmp_Emp_After_Doc_Name.Visible = true;
            Lbl_Direct_Emp_After_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocAfterCodeName"].ToString();
            Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString();
        }

        lbl_Managarial_After.Text = dtProductionPre.Rows[0]["INTCURRENTMANAGERIAL"].ToString();
        lbl_Supervisor_After.Text = dtProductionPre.Rows[0]["INTCURRENTSUPERVISORY"].ToString();
        lbl_Skilled_After.Text = dtProductionPre.Rows[0]["INTCURRENTSKILLED"].ToString();
        lbl_Semi_Skilled_After.Text = dtProductionPre.Rows[0]["INTCURRENTSEMISKILLED"].ToString();
        lbl_Unskilled_After.Text = dtProductionPre.Rows[0]["INTCURRENTUNSKILLED"].ToString();
        lbl_Total_Emp_After.Text = dtProductionPre.Rows[0]["INTCURRENTTOTAL"].ToString();
        lbl_General_After.Text = dtProductionPre.Rows[0]["intGeneralAfter"].ToString();
        lbl_SC_After.Text = dtProductionPre.Rows[0]["intSCAfter"].ToString();
        lbl_ST_After.Text = dtProductionPre.Rows[0]["intSTAfter"].ToString();
        lbl_Total_Cast_Emp_After.Text = dtProductionPre.Rows[0]["intTotalEmpCastAfter"].ToString();
        lbl_Women_After.Text = dtProductionPre.Rows[0]["intWomenAfter"].ToString();
        lbl_PHD_After.Text = dtProductionPre.Rows[0]["intDisabledAfter"].ToString();
        #endregion
        #region Investment
        Txt_FFCI_Date_Before.Text = dtInvestmentPre.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString();
        lbl_Land_Before.Text = dtInvestmentPre.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
        lbl_Building_Before.Text = dtInvestmentPre.Rows[0]["DEC_Building"].ToString();
        lbl_Plant_Mach_Before.Text = dtInvestmentPre.Rows[0]["DEC_Plant_Machinery"].ToString();
        lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPre.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
        lbl_Total_Capital_Before.Text = dtInvestmentPre.Rows[0]["DEC_Total"].ToString();
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
        if (dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString() == "")
        {
            FFCI_Before_Doc_Name.Visible = false;
        }
        else
        {
            FFCI_Before_Doc_Name.Visible = true;
            Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();
            Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString();
        }
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
        #endregion
        #region MEANS OF FINANCE

        //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
        lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
        lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
        Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();
        lbl_FDI_Componet.Text = dtMeansFinancePre.Rows[0]["decFDIComponet"].ToString();
        //dtMeansFinancePre.Rows[0]["intCreatedBy"].ToString();
        //dtMeansFinancePre.Rows[0]["vchTermLoanDocCode"].ToString();
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
        #endregion
    }
    #endregion
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        int tryint;
        if (!IsPostBack)
        {
            int InctUniqueNo = int.TryParse(Request.QueryString["InctUniqueNo"], out tryint) ? tryint : tryint;

            PostpopulateData(InctUniqueNo);
            fillPage(InctUniqueNo);
            TRVisibility();
        }
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
    public string RetDateFrmDB(string srcDate)
    {
        string retdt = "";
        try
        {
            if (srcDate != "")
            {
                DateTime dbdt = Convert.ToDateTime(srcDate);
                retdt = dbdt.ToString("MM/dd/yyyy");

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return retdt;
    }
    public void fillPage(int id)
    {
        Incentive objIncentive = new Incentive();
        GetAndViewParam gv = new GetAndViewParam();
        objIncentive.strcActioncode = "14";
        gv.Param1ID = "";
        gv.Param2ID = "";
        gv.Param3ID = "";
        gv.InctType = 4;
        objIncentive.UnqIncentiveId = id;
        objIncentive.GetVwPrmtrs = gv;
        objIncentive.FormType = FormNumber.QualityCertification_14;
        DataSet dslive = IncentiveManager.GetIncentiveQuality(objIncentive);


        DataTable dtApplyStatusOrFinalSaveStatus = dslive.Tables[8];

        DataTable dtIndustrial = dslive.Tables[0];
        if (dtIndustrial.Rows.Count > 0)
        {
            fillIndustrialModuleControls(dtIndustrial);
        }
        #region Fill Quality Certification
        DataTable dtQuality = dslive.Tables[1];
        DataTable dtQualityDt = dslive.Tables[2];
        if (dtQuality.Rows.Count > 0)
            FillQualityCertification(dtQuality, dtQualityDt);
        #endregion
        #region Fill Alailed Details
        DataTable dtAvail = dslive.Tables[3];
        DataTable dtAvailIncentive = dslive.Tables[5];
        if (dtAvail.Rows.Count > 0)
            FillAvailDetails(dtAvail, dtAvailIncentive);
        #endregion
        #region Fill Additional Document
        DataTable dtAddDoc = dslive.Tables[6];
        fillAddDoc(dtAddDoc);
        #endregion
        DataTable dtMainTable = dslive.Tables[8];///////////M_INCT_APPLICATION Table
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

        objIncentive = new Incentive();
        objIncentive.strcActioncode = "M";
        objIncentive.IncentiveNum = dtMainTable.Rows[0]["VCHINCENTIVENO"].ToString();

        DataSet ds = new DataSet();
        ds = IncentiveManager.GetIncentiveMaster(objIncentive);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dtbPostSubFlag = ds.Tables[0];

            lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
            lblTitle2.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
        }
        lblDate.Text = dtMainTable.Rows[0]["DTMCREATEDBY"].ToString();

    }
    #region fill
    private void fillIndustrialModuleControls(DataTable dtindustry)
    {

        try
        {
            if (dtindustry.Rows.Count > 0)
            {
                TxtApplicantName.Text = dtindustry.Rows[0]["VCHPREAPPLICANTNAME"].ToString().Trim();
            }
        }
        catch (Exception)
        {
        }
    }
    public void FillQualityCertification(DataTable dtQuality, DataTable dtQualityDt)
    {
        if (dtQuality.Rows.Count > 0)
        {
            txtTotal.Text = dtQuality.Rows[0]["intTotal"].ToString();
        }
        gvdQuality.DataSource = dtQualityDt;
        gvdQuality.DataBind();
    }
    public void FillAvailDetails(DataTable dtavail, DataTable dtavailgrd1)
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
            av3.Visible = true;
        }
        if (dtavailgrd1.Rows.Count > 0)
        {
            DataTable dtincentive = new DataTable();
            dtincentive = (DataTable)ViewState["dtincentive"];

            grdAssistanceDetailsAD.DataSource = dtavailgrd1;
            grdAssistanceDetailsAD.DataBind();
        }
    }
    public void fillAddDoc(DataTable dtAddDoc)
    {
        if (dtAddDoc.Rows.Count > 0)
        {
            if (dtAddDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString() != "")
            {
                HypValidStatutary.NavigateUrl = RetFilePath(dtAddDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString(), "AdditionalDocument");
            }

            if (dtAddDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString() != "")
            {
                HypDelay.NavigateUrl = RetFilePath(dtAddDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString(), "AdditionalDocument");
            }
            if (dtAddDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString() != "")
            {
                HypCleanApproveAuthority.NavigateUrl = RetFilePath(dtAddDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString(), "AdditionalDocument");
            }
        }
    }
    public string RetFilePath(string filename, string foldername)
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
    #region Value To Name
    private string oganizationType(string OT)
    {
        switch (OT)
        {
            case "1":
                OT = "Proprietorship";
                break;
            case "2":
                OT = "Partnership";
                break;
            case "3":
                OT = "Private Limited";
                break;
            case "4":
                OT = "Public Limited<";
                break;
            case "5":
                OT = "One Person Company";
                break;
            case "6":
                OT = "Co-operative";
                break;
            case "7":
                OT = "Trust";
                break;
            case "8":
                OT = "Society";
                break;
            default:
                OT = "-";
                break;
        }
        return OT;

    }
    private string Gender(string G)
    {
        switch (G)
        {
            case "1":
                G = "Mr.";
                break;
            case "2":
                G = "Ms.";
                break;
            default:
                G = "";
                break;
        }
        return G;

    }
    private string ApplyBy(string G)
    {
        switch (G)
        {
            case "1":
                G = "Self";
                break;
            case "2":
                G = "Authorized Person";
                break;
            default:
                G = "";
                break;
        }
        return G;

    }
    private string UnitCatagory(string G)
    {
        switch (G)
        {
            case "1":
                G = "Micro";
                break;
            case "2":
                G = "Small";
                break;
            case "3":
                G = "Medium";
                break;
            case "4":
                G = "Large";
                break;
            default:
                G = "";
                break;
        }
        return G;

    }
    private string UnitType(string G)
    {
        switch (G)
        {
            case "1":
                G = "Existing E/M/D";
                break;
            case "2":
                G = "New Unit";
                break;
            case "3":
                G = "Migrated Unit Treated As New";
                break;
            case "4":
                G = "Rehabilitated Unit Treated As New";
                break;
            default:
                G = "";
                break;
        }
        return G;

    }
    private string IsPriority(string G)
    {
        switch (G)
        {
            case "1":
                G = "Yes";
                break;
            case "2":
                G = "No";
                break;
            default:
                G = "";
                break;
        }
        return G;
    }
    private string rblAuthorizing(string OT)
    {
        switch (OT)
        {
            case "D102":
                OT = "Power of Attorney";
                break;
            case "D219":
                OT = "Society Resolution";
                break;
            case "D218":
                OT = "Board Resolution";
                break;
            default:
                OT = "-";
                break;
        }
        return OT;

    }


    #endregion
    #region fill Module_Controls

    #endregion
    #region Calculations

    #endregion
    #region Button Click
    protected void btnFinalSave_Click(object sender, EventArgs e)
    {

        Incentive objEntity = new Incentive();
        try
        {
            string[] fileext = { ".png", ".jpg", ".jpeg" };
            if (fileSignature.HasFile)
            {
                if (fileext.Contains(System.IO.Path.GetExtension(fileSignature.FileName).ToLower()))
                {
                    bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/Signature/"));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath("../incentives/Files/Signature/"));

                    string extension = Path.GetExtension(fileSignature.PostedFile.FileName);
                    string filename = "Signature" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
                    fileSignature.SaveAs(Server.MapPath("../incentives/Files/Signature/") + filename);
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
                SMSEMailContent();

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
    private void SMSEMailContent()
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
    #endregion
    #region Common Functions
    public string UploadFile(FileUpload fileControl, string Path, HiddenField hdn)
    {
        string FileName = "";
        try
        {

            if (fileControl.HasFile)
            {
                string path = Server.MapPath(Path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                FileName = Guid.NewGuid().ToString() + fileControl.FileName.ToString();
                string FileNamewithPath = Server.MapPath(Path + "/" + FileName);
                fileControl.SaveAs(FileNamewithPath);


                if (hdn.Value != "")
                {
                    string deleteFilePath = Server.MapPath(Path + "/" + hdn.Value);
                    if (File.Exists(deleteFilePath))
                        File.Delete(deleteFilePath);
                }


            }
            else if (hdn.Value != "")
            {
                string PreviousFilePath = Server.MapPath(Path + "/" + hdn.Value);
                if (File.Exists(PreviousFilePath))
                    FileName = hdn.Value;
                else
                    FileName = "";
            }
            else
            {
                FileName = "";
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
            FileName = "";
        }
        return FileName;
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
            TrDisplayNone(Lbl_Direct_Emp_Before_Doc_Name.Text, divEmp_Before_Doc_Name);
            TrDisplayNone(Lbl_FFCI_Before_Doc_Name.Text, FFCI_Before_Doc_Name);
            TrDisplayNone(Lbl_Approved_DPR_Before_Doc_Name.Text, Approved_DPR_Before_Doc_Name);
            TrDisplayNone(Lbl_Prod_Comm_After_Doc_Name.Text, CertiDtCommen);
            TrDisplayNone(Lbl_FFCI_After_Doc_Name.Text, View_FFCI_After_Doc);
            TrDisplayNone(Lbl_Approved_DPR_After_Doc_Name.Text, Div_Approved_DPR_After_Doc);
            TrDisplayNone(Lbl_Term_Loan_Doc_Name.Text, tr_Term_Loan_Doc_Name);
            TrDisplayNone(Lbl_Direct_Emp_After_Doc_Name.Text, divEmp_Emp_After_Doc_Name);

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
    #endregion

    protected void gvdQuality_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_Cert_Doc_File_Name = (HiddenField)e.Row.FindControl("Hid_Cert_Doc_File_Name");
            HiddenField Hid_Renewal_Doc_File_Name = (HiddenField)e.Row.FindControl("Hid_Renewal_Doc_File_Name");
            HiddenField Hid_Expen_Doc_File_Name = (HiddenField)e.Row.FindControl("Hid_Expen_Doc_File_Name");

            HyperLink CertificateDetailsDOC = (HyperLink)e.Row.FindControl("lbllblstrCertificateDetailsDOC");
            HyperLink RenewalDateDOC = (HyperLink)e.Row.FindControl("lblstrRenewalDateDOC");
            HyperLink ExpenditureDetails = (HyperLink)e.Row.FindControl("lblstrExpenditureDetails");

            /*-----------------------------------------------------------*/

            if (Hid_Cert_Doc_File_Name.Value == "")
            {
                CertificateDetailsDOC.Visible = false;
            }
            else
            {
                CertificateDetailsDOC.Visible = true;
            }

            /*-----------------------------------------------------------*/

            if (Hid_Renewal_Doc_File_Name.Value == "")
            {
                RenewalDateDOC.Visible = false;
            }
            else
            {
                RenewalDateDOC.Visible = true;
            }

            /*-----------------------------------------------------------*/

            if (Hid_Expen_Doc_File_Name.Value == "")
            {
                ExpenditureDetails.Visible = false;
            }
            else
            {
                ExpenditureDetails.Visible = true;
            }

            /*-----------------------------------------------------------*/

        }
    }
}