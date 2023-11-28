using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;

public partial class incentives_EmploymentRating : SessionCheck
{
    Incentive objincUnit = new Incentive();
    int intCalTotNewRec = 0, intCalTotSkillUp = 0;
    string MsgTitle = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];

    protected void Page_Load(object sender, EventArgs e)
    {
        txtTotal_NewRec.Attributes.Add("readonly", "readonly");///// Training Details
        txtTotal_SkillUpgrade.Attributes.Add("readonly", "readonly");///// Training Details

        if (!IsPostBack)
        {
            fillSalutation();
            if (Session["FyYear"] != null && Session["FyYear"] != "")
            {
                lblFyear.Text = Session["FyYear"].ToString();
                lblTYear.Text = (Convert.ToInt32(Session["FyYear"].ToString()) + 1).ToString();
            }
            else
            {
                lblFyear.Text = "2017";
                lblTYear.Text = (Convert.ToInt32(lblFyear.Text) + 1).ToString();
            }

            FillSubsidyClaim();
            FillPageHeaderDtl();
            if (Session["ApplySource"].ToString() == "0")
            {
                PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
                DataRetrive(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            }
            else
            {
                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
                PrepopulateDataPlus(Convert.ToInt16(Session["InvestorId"]));
                FillPrepopulateFiles();
            }
        }
    }

    #region viewdetail
    public void PrepopulateData(int id)
    {
        try
        {
            Grd_TL.DataSource = null;
            Grd_TL.DataBind();
            Grd_WC.DataSource = null;
            Grd_WC.DataBind();
            DataSet dslivePre = IncentiveManager.PrepopulateData(id);
            DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment

            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
            DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
            DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan   

            if (dtindustryPre.Rows.Count > 0)
            {
                lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                //dtindustryPre.Rows[0]["intOrganisationType"].ToString();	
                DataSet ds1 = new DataSet();
                ds1 = IncentiveManager.dynamic_name_doc_bind();
                ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intUnitType"].ToString() + "'";
                ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intOrganisationType"].ToString() + "'";
                DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
                if (dt.Rows.Count > 0)
                {
                    Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                    Lbl_Org_Doc_Type.Text = dt.Rows[0]["vch_orgdocumentname"].ToString();
                    Hid_Org_Doc_Type.Value = dt.Rows[0]["vchDocumentType"].ToString();
                    lblAuthorizing.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                    hidAuthorizing.Value = dt.Rows[0]["vchDocumentType"].ToString();
                    hdnAUTHORIZEDFILEDocId.Value = dt.Rows[0]["vchDocumentType"].ToString();
                }
                else
                {
                    Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                    Hid_Org_Doc_Type.Value = "";
                }

                lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
                lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                //dtindustryPre.Rows[0]["intUnitCat"].ToString();		
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
                        Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();

                        Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();
                    }
                    else
                    {
                        Div_Unit_Type_Doc.Visible = false;
                        Lbl_Unit_Type_Doc_Name.Text = "";
                        Hid_Unit_Type_Doc_Code.Value = "";
                    }
                }
                else
                {
                    Div_Unit_Type_Doc.Visible = false;
                    Lbl_Unit_Type_Doc_Name.Text = "";
                    Hid_Unit_Type_Doc_Code.Value = "";
                }

                //dtindustryPre.Rows[0]["intUnitType"].ToString();			
                lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();
                //dtindustryPre.Rows[0]["vchDocCode"].ToString();	

                //dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();

                if (dtindustryPre.Rows[0]["intPriority"].ToString() == "1")
                {
                    lblIs_Priority.Text = "Yes";
                    Pioneersec.Visible = true;
                }
                else
                {
                    lblIs_Priority.Text = "No";
                    Pioneersec.Visible = false;
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
                //dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();	
                lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                DdlGender.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
                TxtApplicantName.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                //dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                // Lbl_Org_Doc_Type.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                //dtindustryPre.Rows[0]["vchCertOfRegdDocCode"].ToString();		

                //dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
                lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
                lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
                lbl_pcno_befor.Text = dtindustryPre.Rows[0]["vchpcnobefore"].ToString();
                lblGstin.Text = dtindustryPre.Rows[0]["VCHGSTIN"].ToString();
                lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
                lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();
                //dtindustryPre.Rows[0]["vchProdCommCertBeforeCode"].ToString();	
                //Lbl_Prod_Comm_Before_Doc_Name.Text = dtindustryPre.Rows[0]["ProdCommCertBeforeDocName"].ToString();
                if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
                }
                else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
                }

                //dtindustryPre.Rows[0]["vchProdCommCertBefore"].ToString();
                if (dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString() != "")
                {
                    divbefor.Visible = true;
                    divbefor1.Visible = true;
                    divbefor2.Visible = true;
                }
                else
                {
                    divbefor.Visible = false;
                    divbefor1.Visible = false;
                    divbefor2.Visible = false;
                    lblAfterEMD11.Text = "Date of Production Commencement";
                    lblAfterEMD189.Text = "PC Issuance Date";
                    lbl_PC_No_After.Text = "PC No";
                    lblemd.Text = "";
                    Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                    lblEMDInvestment.Text = "";
                }

                lbl_Prod_Comm_Date_After.Text = dtindustryPre.Rows[0]["dtmProdCommAfter"].ToString();
                lbl_PC_Issue_Date_After.Text = dtindustryPre.Rows[0]["dtmPCIssueDateAfter"].ToString();
                //dtindustryPre.Rows[0]["vchProdCommCertAfterCode"].ToString();		
                //Lbl_Prod_Comm_After_Doc_Name.Text = dtindustryPre.Rows[0]["ProdCommCertAfterDocName"].ToString();

                if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
                }
                else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
                }

                //dtindustryPre.Rows[0]["intDistrictCode"].ToString();			
                lbl_District.Text = dtindustryPre.Rows[0]["distname"].ToString();
                //dtindustryPre.Rows[0]["intSectorId"].ToString();			
                lbl_Sector.Text = dtindustryPre.Rows[0]["sectorName"].ToString();
                //dtindustryPre.Rows[0]["intSubSectorId"].ToString();			
                lbl_Sub_Sector.Text = dtindustryPre.Rows[0]["SubsectorName"].ToString();
                Lbl_Derived_Sector.Text = dtindustryPre.Rows[0]["vchDerivedSector"].ToString();
                //dtindustryPre.Rows[0]["bitSectoralPolicy"].ToString();

                if (dtindustryPre.Rows[0]["bitPriorityIPR"].ToString() == "1")
                {
                    lbl_Sectoral.Text = "Yes";
                }
                else
                {
                    lbl_Sectoral.Text = "No";
                }
                //dtindustryPre.Rows[0]["intCreatedBy"].ToString();
            }

            if (dtProductionPre.Rows.Count > 0)
            {
                Grd_Production_Before.DataSource = dtProductionDetBefPre;
                Grd_Production_Before.DataBind();
                Grd_Production_After.DataSource = dtProductionDetAftPre;
                Grd_Production_After.DataBind();

                //dtProductionPre.Rows[0]["intProductionId"].ToString();
                lbl_Direct_Emp_Before.Text = dtProductionPre.Rows[0]["intDirectEmpBefore"].ToString();
                lbl_Contract_Emp_Before.Text = dtProductionPre.Rows[0]["intContractualEmpBefore"].ToString();
                Hyp_View_Direct_Emp_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocBefore"].ToString();
                lbl_Managarial_Before.Text = dtProductionPre.Rows[0]["intManagerialBefore"].ToString();
                lbl_Supervisor_Before.Text = dtProductionPre.Rows[0]["intSupervisorBefore"].ToString();
                lbl_Skilled_Before.Text = dtProductionPre.Rows[0]["intSkilledBefore"].ToString();
                lbl_Semi_Skilled_Before.Text = dtProductionPre.Rows[0]["intSemiSkilledBefore"].ToString();
                lbl_Unskilled_Before.Text = dtProductionPre.Rows[0]["intUnskilledBefore"].ToString();
                lbl_Total_Emp_Before.Text = dtProductionPre.Rows[0]["intTotalEmpBefore"].ToString();
                lbl_General_Before.Text = dtProductionPre.Rows[0]["intGeneralBefore"].ToString();
                lbl_SC_Before.Text = dtProductionPre.Rows[0]["intSCBefore"].ToString();
                lbl_ST_Before.Text = dtProductionPre.Rows[0]["intSTBefore"].ToString();
                lbl_Total_Cast_Emp_Before.Text = dtProductionPre.Rows[0]["intTotalEmpCastBefore"].ToString();
                lbl_Women_Before.Text = dtProductionPre.Rows[0]["intWomenBefore"].ToString();
                lbl_PHD_Before.Text = dtProductionPre.Rows[0]["intDisabledBefore"].ToString();
                lbl_Direct_Emp_After.Text = dtProductionPre.Rows[0]["intDirectEmpAfter"].ToString();
                lbl_Contract_Emp_After.Text = dtProductionPre.Rows[0]["intContractualEmpAfter"].ToString();
                //dtProductionPre.Rows[0]["vchEmpDocBeforeCode"].ToString();			
                Lbl_Direct_Emp_Before_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocBeforeCodeName"].ToString();
                Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString();
                //dtProductionPre.Rows[0]["vchEmpDocAfterCode"].ToString();			
                Lbl_Direct_Emp_After_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocAfterCodeName"].ToString();

                lbl_Managarial_After.Text = dtProductionPre.Rows[0]["intManagerialAfter"].ToString();
                lbl_Supervisor_After.Text = dtProductionPre.Rows[0]["intSupervisorAfter"].ToString();
                lbl_Skilled_After.Text = dtProductionPre.Rows[0]["intSkilledAfter"].ToString();
                lbl_Semi_Skilled_After.Text = dtProductionPre.Rows[0]["intSemiSkilledAfter"].ToString();
                lbl_Unskilled_After.Text = dtProductionPre.Rows[0]["intUnskilledAfter"].ToString();
                lbl_Total_Emp_After.Text = dtProductionPre.Rows[0]["intTotalEmpAfter"].ToString();
                lbl_General_After.Text = dtProductionPre.Rows[0]["intGeneralAfter"].ToString();
                lbl_SC_After.Text = dtProductionPre.Rows[0]["intSCAfter"].ToString();
                lbl_ST_After.Text = dtProductionPre.Rows[0]["intSTAfter"].ToString();
                lbl_Total_Cast_Emp_After.Text = dtProductionPre.Rows[0]["intTotalEmpCastAfter"].ToString();
                lbl_Women_After.Text = dtProductionPre.Rows[0]["intWomenAfter"].ToString();
                lbl_PHD_After.Text = dtProductionPre.Rows[0]["intDisabledAfter"].ToString();

                //dtProductionPre.Rows[0]["intCreatedBy"].ToString();
            }

            if (dtInvestmentPre.Rows.Count > 0)
            {
                //dtInvestmentPre.Rows[0]["slno"].ToString();
                Txt_FFCI_Date_Before.Text = dtInvestmentPre.Rows[0]["dtmFFCIDateBefore"].ToString();
                //dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCode"].ToString();
                Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();
                Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocBefore"].ToString();
                lbl_Land_Before.Text = dtInvestmentPre.Rows[0]["decLandAmtBefore"].ToString();
                lbl_Building_Before.Text = dtInvestmentPre.Rows[0]["decBuildingAmtBefore"].ToString();
                lbl_Plant_Mach_Before.Text = dtInvestmentPre.Rows[0]["decPlantMachAmtBefore"].ToString();
                lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPre.Rows[0]["decOtheFixedAssetAmtBefore"].ToString();
                lbl_Total_Capital_Before.Text = dtInvestmentPre.Rows[0]["decTotalAmtBefore"].ToString();
                //dtInvestmentPre.Rows[0]["vchProjectDocBeforeCode"].ToString();			
                Lbl_Approved_DPR_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocBeforeCodeName"].ToString();
                Hyp_View_Approved_DPR_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocBefore"].ToString();
                lbl_FFCI_Date_After.Text = dtInvestmentPre.Rows[0]["dtmFFCIDateAfter"].ToString();
                //dtInvestmentPre.Rows[0]["vchFFCIDocAfterCode"].ToString();			
                Lbl_FFCI_After_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocAfterCodeName"].ToString();
                Hyp_View_FFCI_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocAfter"].ToString();

                lbl_Land_After.Text = dtInvestmentPre.Rows[0]["decLandAmtAfter"].ToString();
                lbl_Building_After.Text = dtInvestmentPre.Rows[0]["decBuildingAmtAfter"].ToString();
                lbl_Plant_Mach_After.Text = dtInvestmentPre.Rows[0]["decPlantMachAmtAfter"].ToString();
                lbl_Other_Fixed_Asset_After.Text = dtInvestmentPre.Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
                lbl_Total_Capital_After.Text = dtInvestmentPre.Rows[0]["decTotalAmtAfter"].ToString();

                //dtInvestmentPre.Rows[0]["vchProjectDocAfterCode"].ToString();			
                Lbl_Approved_DPR_After_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocAfterCodeName"].ToString();
                Hyp_View_Approved_DPR_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocAfter"].ToString();
                //dtInvestmentPre.Rows[0]["intCreatedBy"].ToString();
            }

            if (dtMeansFinancePre.Rows.Count > 0)
            {
                //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
                lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
                lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();

                Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString();
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
            }

            BindDocFromService(enServiceDocType.OSPCB, D275, flValidStatutary, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, hdnIsOsPCBDownloaded);
            BindDocFromService(enServiceDocType.Boiler, D280, flCleanApproveAuthority, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, hdnBoilerDownloaded);

        }
        catch (Exception x)
        {
            Util.LogError(x, "Incetnive");
        }

    }
    public void PostpopulateData(int id)
    {
        Grd_TL.DataSource = null;
        Grd_TL.DataBind();
        Grd_WC.DataSource = null;
        Grd_WC.DataBind();
        DataSet dslivePre = IncentiveManager.PostpopulateData(id);
        DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
        DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment
        DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
        DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
        DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
        DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
        DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
        DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan

        if (dtindustryPre.Rows.Count > 0)
        {
            lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
            DataSet ds1 = new DataSet();
            ds1 = IncentiveManager.dynamic_name_doc_bind();
            ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intUnitType"].ToString() + "'";
            ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intOrganisationType"].ToString() + "'";
            DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
            if (dt.Rows.Count > 0)
            {
                Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                Lbl_Org_Doc_Type.Text = dt.Rows[0]["vch_orgdocumentname"].ToString();
                Hid_Org_Doc_Type.Value = dt.Rows[0]["vchDocumentType"].ToString();
                lblAuthorizing.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                hidAuthorizing.Value = dt.Rows[0]["vchDocumentType"].ToString();
                hdnAUTHORIZEDFILEDocId.Value = dt.Rows[0]["vchDocumentType"].ToString();
            }
            else
            {
                Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                Hid_Org_Doc_Type.Value = "";
            }

            lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
            lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
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
                    Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();

                    Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();
                }
                else
                {
                    Div_Unit_Type_Doc.Visible = false;
                    Lbl_Unit_Type_Doc_Name.Text = "";
                    Hid_Unit_Type_Doc_Code.Value = "";
                }
            }
            else
            {
                Div_Unit_Type_Doc.Visible = false;
                Lbl_Unit_Type_Doc_Name.Text = "";
                Hid_Unit_Type_Doc_Code.Value = "";
            }
            lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();
            if (dtindustryPre.Rows[0]["intPriority"].ToString() == "1")
            {
                lblIs_Priority.Text = "Yes";
                Pioneersec.Visible = true;
            }
            else
            {
                lblIs_Priority.Text = "No";
                Pioneersec.Visible = false;
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

            //DdlGender.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
            //TxtApplicantName.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();

            Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();

            lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
            lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
            lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
            lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
            lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();

            lbl_pcno_befor.Text = dtindustryPre.Rows[0]["vchpcnobefore"].ToString();
            lblGstin.Text = dtindustryPre.Rows[0]["VCHGSTIN"].ToString();

            if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
            {
                Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
            }
            else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            {
                Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
            }

            if (dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString() != "")
            {
                divbefor.Visible = true;
                divbefor1.Visible = true;
                divbefor2.Visible = true;
            }
            else
            {
                divbefor.Visible = false;
                divbefor1.Visible = false;
                divbefor2.Visible = false;
                lblAfterEMD11.Text = "Date of Production Commencement";
                lblAfterEMD189.Text = "PC Issuance Date";
                lbl_PC_No_After.Text = "PC No";
                lblemd.Text = "";
                Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                lblEMDInvestment.Text = "";
            }

            lbl_Prod_Comm_Date_After.Text = dtindustryPre.Rows[0]["dtmProdCommAfter"].ToString();
            lbl_PC_Issue_Date_After.Text = dtindustryPre.Rows[0]["dtmPCIssueDateAfter"].ToString();

            if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
            {
                Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
            }
            else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            {
                Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
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

        //------------------extra added --------------
        TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHAPPLICANTNAME"].ToString();
        DdlGender.SelectedValue = dtindustryPre.Rows[0]["INTGENDER"].ToString();
        int intApplyBy = Convert.ToInt32(dtindustryPre.Rows[0]["INTAPPLYBY"].ToString());
        radApplyBy.SelectedValue = dtindustryPre.Rows[0]["INTAPPLYBY"].ToString();
        if (intApplyBy == 1)
        {
            divadhhardetails.Attributes.Add("style", "display::block");
            divAuthorizing.Attributes.Add("style", "display:none");
            if (dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
            {
                if (dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Length == 12)
                {
                    TxtAdhaar.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString();
                }
            }
        }
        else
        {
            divadhhardetails.Attributes.Add("style", "display::none");
            divAuthorizing.Attributes.Add("style", "display:block");
            if (dtindustryPre.Rows[0]["VCHAUTHORIZEDFILECODE"].ToString().Trim() != "")
            {
                if (dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"] != null && dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"] != DBNull.Value && dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"] != "")
                {
                    hdnAUTHORIZEDFILE.Value = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    FlupAUTHORIZEDFILE.Enabled = false;
                    hypAUTHORIZEDFILE.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    hypAUTHORIZEDFILE.Visible = true;
                    lnkAUTHORIZEDFILEDdelete.Visible = true;
                }
            }
        }

        if (dtProductionPre.Rows.Count > 0)
        {
            Grd_Production_Before.DataSource = dtProductionDetBefPre;
            Grd_Production_Before.DataBind();
            Grd_Production_After.DataSource = dtProductionDetAftPre;
            Grd_Production_After.DataBind();

            lbl_Direct_Emp_Before.Text = dtProductionPre.Rows[0]["intDirectEmpBefore"].ToString();
            lbl_Contract_Emp_Before.Text = dtProductionPre.Rows[0]["intContractualEmpBefore"].ToString();
            Hyp_View_Direct_Emp_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["VCHEMPDOC"].ToString();
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
            Lbl_Direct_Emp_Before_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocBeforeCodeName"].ToString();
            Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString();
            Lbl_Direct_Emp_After_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocAfterCodeName"].ToString();

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
        }

        if (dtInvestmentPre.Rows.Count > 0)
        {
            Txt_FFCI_Date_Before.Text = dtInvestmentPre.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString();
            Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();

            Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString();

            lbl_Land_Before.Text = dtInvestmentPre.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
            lbl_Building_Before.Text = dtInvestmentPre.Rows[0]["DEC_Building"].ToString();
            lbl_Plant_Mach_Before.Text = dtInvestmentPre.Rows[0]["DEC_Plant_Machinery"].ToString();
            lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPre.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
            lbl_Total_Capital_Before.Text = dtInvestmentPre.Rows[0]["DEC_Total"].ToString();
            Lbl_Approved_DPR_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocBeforeCodeName"].ToString();
            Hyp_View_Approved_DPR_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocBefore"].ToString();
            lbl_FFCI_Date_After.Text = dtInvestmentPre.Rows[0]["dtmFFCIDateAfter"].ToString();
            Lbl_FFCI_After_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocAfterCodeName"].ToString();
            Hyp_View_FFCI_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocAfter"].ToString();

            lbl_Land_After.Text = dtInvestmentPre.Rows[0]["decLandAmtAfter"].ToString();
            lbl_Building_After.Text = dtInvestmentPre.Rows[0]["decBuildingAmtAfter"].ToString();
            lbl_Plant_Mach_After.Text = dtInvestmentPre.Rows[0]["decPlantMachAmtAfter"].ToString();
            lbl_Other_Fixed_Asset_After.Text = dtInvestmentPre.Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
            lbl_Total_Capital_After.Text = dtInvestmentPre.Rows[0]["decTotalAmtAfter"].ToString();

            Lbl_Approved_DPR_After_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocAfterCodeName"].ToString();
            Hyp_View_Approved_DPR_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocAfter"].ToString();
        }

        if (dtMeansFinancePre.Rows.Count > 0)
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
    }

    public void FillPrepopulateFiles()
    {
        objincUnit = new Incentive();
        objincUnit.UnitCode = Convert.ToString(Session["UnitCode"]);
        DataSet dslivePreFile = IncentiveManager.PrepopulateFile(objincUnit);
        if (dslivePreFile.Tables[0].Rows.Count > 0)
        {
            FillFileUpladControls(dslivePreFile.Tables[0]);
        }
    }
    #endregion

    public void PrepopulateDataPlus(int id)
    {
        DataSet dslivePre = IncentiveManager.PostpopulateDataPLUS(id);
        DataTable dtBank = dslivePre.Tables[0];////////////industry panel
        if (dtBank.Rows.Count > 0)
        {
            PreBankPlus(dtBank);
        }
    }
    public void PreBankPlus(DataTable dtBank)
    {
        txtAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
        txtBnkNm.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
        txtBranch.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
        txtIFSC.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
        txtMICRNo.Text = dtBank.Rows[0]["VCHMICR"].ToString();
        if (dtBank.Rows[0]["vchBankDoc"].ToString() != "")
        {
            hdnBank.Value = dtBank.Rows[0]["vchBankDoc"].ToString();
            hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBank.Rows[0]["vchBankDoc"].ToString();
            hypBank.Visible = true;
            lnkBankDelete.Visible = true;
            fuBank.Enabled = false;
        }
    }

    #region Fill Page Header Details
    private void FillPageHeaderDtl()
    {
        try
        {
            objincUnit = new Incentive();
            objincUnit.strcActioncode = "M";
            objincUnit.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveMaster(objincUnit);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblTitle.Text = ds.Tables[0].Rows[0]["vchInctName"].ToString();

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
    private void FillSubsidyClaim()
    {
        try
        {
            objincUnit = new Incentive();
            objincUnit.strcActioncode = "T";
            objincUnit.Userid = Convert.ToInt16(Session["InvestorId"]);
            DataSet dsSubsidy = new DataSet();
            dsSubsidy = IncentiveManager.GetSubsidyClaim(objincUnit);

            if (dsSubsidy.Tables[0].Rows.Count > 0)
            {
                grdSubsidy.DataSource = dsSubsidy.Tables[0];
                grdSubsidy.DataBind();
                h4Txt.Visible = true;
            }
            else
            {
                grdSubsidy.DataSource = null;
                grdSubsidy.DataBind();
                h4Txt.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            objincUnit = new Incentive();
            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objincUnit.UnqIncentiveId = 0;
            }
            else
            {
                objincUnit.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }
            objincUnit.strcActioncode = "A";
            objincUnit.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objincUnit.PealNum = Convert.ToString(Session["ProposalNo"]);
            objincUnit.PCNum = Convert.ToString(Session["PCNo"]);
            objincUnit.UnitCode = Convert.ToString(Session["UnitCode"]);
            objincUnit.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objincUnit.Userid = Convert.ToInt16(Session["InvestorId"]);
            objincUnit.Createdby = Convert.ToInt16(Session["InvestorId"]);
            objincUnit.FYear = Convert.ToInt16(Convert.ToString(Session["FyYear"]).Trim() == "" ? DateTime.Now.Year.ToString() : Convert.ToString(Session["FyYear"]).Trim());
            objincUnit.ApprovalAction = "A";
            objincUnit.incentivetype = 4;
            objincUnit.FormType = FormNumber.TrainingSubsidy_16;
            objincUnit.FileUploadDetails = getFileUploadDatatable();

            INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();
            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;

            objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);

            if (radApplyBy.SelectedIndex > -1)
            {
                objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }
            objINDUSTRIAL_UNIT_MASTER.AADHAARNO_IND = TxtAdhaar.Text;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILECODE_IND = hdnAUTHORIZEDFILEDocId.Value;
            objincUnit.IndsutUnitMstDet = objINDUSTRIAL_UNIT_MASTER;

            AdditionalDocumentsdata();
            AddBankDetails();
            SetTrainingSection();


            string retval = IncentiveManager.CreateIncentiveTrainingSubsidy(objincUnit);
            int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
            Response.Redirect("Training_Subsidy_FormPreview.aspx?InctUniqueNo=" + Convert.ToString(mstyp));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void btndraft_Click(object sender, EventArgs e)
    {
        try
        {
            objincUnit = new Incentive();
            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objincUnit.UnqIncentiveId = 0;
            }
            else
            {
                objincUnit.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }

            objincUnit.strcActioncode = "A";
            objincUnit.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objincUnit.PealNum = Convert.ToString(Session["ProposalNo"]);
            objincUnit.PCNum = Convert.ToString(Session["PCNo"]);
            objincUnit.UnitCode = Convert.ToString(Session["UnitCode"]);
            objincUnit.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objincUnit.Userid = Convert.ToInt16(Session["InvestorId"]);
            objincUnit.Createdby = Convert.ToInt16(Session["InvestorId"]);
            objincUnit.FYear = Convert.ToInt16(Convert.ToString(Session["FyYear"]).Trim() == "" ? DateTime.Now.Year.ToString() : Convert.ToString(Session["FyYear"]).Trim());
            objincUnit.ApprovalAction = "A";
            objincUnit.incentivetype = 4;
            objincUnit.FormType = FormNumber.TrainingSubsidy_16;
            objincUnit.FileUploadDetails = getFileUploadDatatable();

            INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();
            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);

            if (radApplyBy.SelectedIndex > -1)
            {
                objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }

            objINDUSTRIAL_UNIT_MASTER.AADHAARNO_IND = TxtAdhaar.Text;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILECODE_IND = hdnAUTHORIZEDFILEDocId.Value;
            objincUnit.IndsutUnitMstDet = objINDUSTRIAL_UNIT_MASTER;

            AdditionalDocumentsdata();
            AddBankDetails();
            SetTrainingSection();

            string retval = IncentiveManager.CreateIncentiveTrainingSubsidy(objincUnit);
            string msg = "<strong>Application Drafted Successfully !!</strong>";

            if (retval.Split('~')[0].ToString() == "1")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + msg + "', '" + Messages.TitleOfProject + "');   </script>", false);

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    private List<lstFileUpload> getFileUploadDatatable()
    {
        List<lstFileUpload> listItmProp = new List<lstFileUpload>();
        DataTable dtFiles = new DataTable() { TableName = "dtFiles" };
        dtFiles.Columns.Add(new DataColumn("id")
        {
            AutoIncrement = true,
            AutoIncrementSeed = 1,
            AutoIncrementStep = 1
        });
        dtFiles.Columns.Add(new DataColumn("vchDocId"));
        dtFiles.Columns.Add(new DataColumn("vchFileName"));
        dtFiles.Columns.Add(new DataColumn("vchFilePath"));

        if (hdnUploadSatutory4Tdet.Value != "")
        {
            DataRow dorgRowT = dtFiles.NewRow();
            dorgRowT["vchDocId"] = hdnSatutory4TdetDoc.Value;
            dorgRowT["vchFileName"] = hdnUploadSatutory4Tdet.Value;
            dorgRowT["vchFilePath"] = "Files/TrainingDetail/";
            dtFiles.Rows.Add(dorgRowT);
        }

        if (hdnUploadSatutory4Rdet.Value != "")
        {
            DataRow dorgRowTR = dtFiles.NewRow();
            dorgRowTR["vchDocId"] = hdnMnyReceipt.Value;
            dorgRowTR["vchFileName"] = hdnUploadSatutory4Rdet.Value;
            dorgRowTR["vchFilePath"] = "Files/TrainingDetail/Receipt/";
            dtFiles.Rows.Add(dorgRowTR);
        }

        if (D274.Value != "")
        {
            DataRow dorgRowSC = dtFiles.NewRow();
            dorgRowSC["vchDocId"] = D274;
            dorgRowSC["vchFileName"] = D274.Value;
            dorgRowSC["vchFilePath"] = "Files/AdditionalDocument/";
            dtFiles.Rows.Add(dorgRowSC);
        }

        if (D275.Value != "")
        {
            DataRow dorgRow = dtFiles.NewRow();
            dorgRow["vchDocId"] = D275;
            dorgRow["vchFileName"] = D275.Value;
            dorgRow["vchFilePath"] = "Files/AdditionalDocument/";
            dtFiles.Rows.Add(dorgRow);
        }
        if (D280.Value != "")
        {
            DataRow dorgRow = dtFiles.NewRow();
            dorgRow["vchDocId"] = D280;
            dorgRow["vchFileName"] = D280.Value;
            dorgRow["vchFilePath"] = "Files/AdditionalDocument/";
            dtFiles.Rows.Add(dorgRow);
        }
        if (hdnAUTHORIZEDFILE.Value != "")
        {
            DataRow dorgRow = dtFiles.NewRow();
            dorgRow["vchDocId"] = hdnAUTHORIZEDFILEDocId.Value;
            dorgRow["vchFileName"] = hdnAUTHORIZEDFILE.Value;
            dorgRow["vchFilePath"] = "Files/InctBasicDoc/";
            dtFiles.Rows.Add(dorgRow);
        }

        if (hdnBank.Value != "")
        {
            DataRow dorgRow = dtFiles.NewRow();
            dorgRow["vchDocId"] = hdnBankID.Value;
            dorgRow["vchFileName"] = hdnBank.Value;
            dorgRow["vchFilePath"] = "../incentives/Files/Bank/";
            dtFiles.Rows.Add(dorgRow);

        }

        if (dtFiles.Rows.Count > 0)
        {
            listItmProp = dtFiles.AsEnumerable().Select(m => new lstFileUpload()
            {
                id = m.Field<int>("id"),
                vchDocId = m.Field<string>("vchDocId"),
                vchFileName = m.Field<string>("vchFileName"),
                vchFilePath = m.Field<string>("vchFilePath")

            }).ToList();
        }

        return listItmProp;
    }

    #region Training Subsidy
    public void SetTrainingSection()
    {
        try
        {
            objincUnit.TrainingDetail = new TrainingDetails();
            List<lstTrainingDtlNewRec> lstNewRec = new List<lstTrainingDtlNewRec>();
            List<lstTrainingDtlSkillUpgrade> lstSkillUpgrade = new List<lstTrainingDtlSkillUpgrade>();
            for (int i = 0; i < grdNewRecruited.Rows.Count; i++)
            {
                lstTrainingDtlNewRec objItem = new lstTrainingDtlNewRec();
                Label lblTraineeType_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblTraineeType_NewRec");
                Label lblTrainingLoc_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblTrainingLoc_NewRec");
                HiddenField hdnTrainingLoc_NewRec = (HiddenField)grdNewRecruited.Rows[i].FindControl("hdnTrainingLoc_NewRec");
                Label lblNoOfTrainee_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblNoOfTrainee_NewRec");
                Label lblNoOfDays_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblNoOfDays_NewRec");
                Label lblOrg_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblOrg_NewRec");

                objItem.vchTraineeType = lblTraineeType_NewRec.Text;
                objItem.vchTraingLoc = lblTrainingLoc_NewRec.Text;
                if (hdnTrainingLoc_NewRec.Value != "")
                {
                    objItem.intTraingLoc = Convert.ToInt32(hdnTrainingLoc_NewRec.Value.ToString());
                }
                if (lblNoOfTrainee_NewRec.Text != "")
                {
                    objItem.intNoOftrainee = Convert.ToInt32(lblNoOfTrainee_NewRec.Text);
                }

                if (lblNoOfDays_NewRec.Text != "")
                {
                    objItem.intNoOfDays = Convert.ToInt32(lblNoOfDays_NewRec.Text);
                }
                objItem.vchOrgName = lblOrg_NewRec.Text;

                lstNewRec.Add(objItem);
            }

            for (int i = 0; i < grdSkillUpgrade.Rows.Count; i++)
            {
                lstTrainingDtlSkillUpgrade objItem = new lstTrainingDtlSkillUpgrade();
                Label lblTraineeType_SkillUpgrade = (Label)grdSkillUpgrade.Rows[i].FindControl("lblTraineeType_SkillUpgrade");
                Label lblTrainingLoc_SkillUpgrade = (Label)grdSkillUpgrade.Rows[i].FindControl("lblTrainingLoc_SkillUpgrade");
                HiddenField hdnTrainingLoc_SkillUpgrade = (HiddenField)grdSkillUpgrade.Rows[i].FindControl("hdnTrainingLoc_SkillUpgrade");
                Label lblNoOfTrainee_SkillUpgrade = (Label)grdSkillUpgrade.Rows[i].FindControl("lblNoOfTrainee_SkillUpgrade");
                Label lblNoOfDays_SkillUpgrade = (Label)grdSkillUpgrade.Rows[i].FindControl("lblNoOfDays_SkillUpgrade");
                Label lblOrg_SkillUpgrade = (Label)grdSkillUpgrade.Rows[i].FindControl("lblOrg_SkillUpgrade");

                objItem.vchTraineeType = lblTraineeType_SkillUpgrade.Text;
                objItem.vchTraingLoc = lblTrainingLoc_SkillUpgrade.Text;
                if (hdnTrainingLoc_SkillUpgrade.Value != "")
                {
                    objItem.intTraingLoc = Convert.ToInt32(hdnTrainingLoc_SkillUpgrade.Value);
                }
                if (lblNoOfTrainee_SkillUpgrade.Text != "")
                {
                    objItem.intNoOftrainee = Convert.ToInt32(lblNoOfTrainee_SkillUpgrade.Text);
                }

                if (lblNoOfDays_SkillUpgrade.Text != "")
                {
                    objItem.intNoOfDays = Convert.ToInt32(lblNoOfDays_SkillUpgrade.Text);
                }
                objItem.vchOrgName = lblOrg_SkillUpgrade.Text;

                lstSkillUpgrade.Add(objItem);
            }


            objincUnit.TrainingDetail.NewlyRecruited = lstNewRec;
            objincUnit.TrainingDetail.SkillUpgradation = lstSkillUpgrade;
            objincUnit.TrainingDetail.AmountPaid = TextBox120.Text.Trim() == "" ? 0 : Decimal.Parse(TextBox120.Text.Trim());
            objincUnit.TrainingDetail.vchFyear = lblFyear.Text + "-" + lblTYear.Text;
            objincUnit.TrainingDetail.TraineeDetails = hdnUploadSatutory4Tdet.Value;
            objincUnit.TrainingDetail.MoneyReceipt = hdnUploadSatutory4Rdet.Value;

        }
        catch (Exception xx)
        {
            Util.LogError(xx, "Incentive");
        }

    }
    #endregion

    #region Additional Doc
    protected void AdditionalDocumentsdata()
    {
        try
        {
            if (hdnIsOsPCBDownloaded.Value == "1")
            {
                SaveServiceDoc(enServiceDocType.OSPCB, D275, "AdditionalDocument");
            }
            if (hdnBoilerDownloaded.Value == "1")
            {
                SaveServiceDoc(enServiceDocType.Boiler, D280, "AdditionalDocument");
            }

            objincUnit.AdditionalDocument = new EntityLayer.Incentive.AdditionalDocuments();
            objincUnit.AdditionalDocument.strValidSatutoryGreenCategory = D275.Value;
            objincUnit.AdditionalDocument.strCondoDocumentationDelay = D274.Value;
            objincUnit.AdditionalDocument.strCleanApproveAuthorityOSPCB = D280.Value;
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }
    #endregion

    #region Bank Details
    public void AddBankDetails()
    {
        //bank info
        objincUnit.BankDet = new EntityLayer.Incentive.BankDetails();
        objincUnit.BankDet.BankName = txtBnkNm.Text;
        objincUnit.BankDet.BranchName = txtBranch.Text;
        objincUnit.BankDet.IFSCNo = txtIFSC.Text;
        objincUnit.BankDet.AccountNo = txtAccNo.Text;
        objincUnit.BankDet.MICRNo = txtMICRNo.Text;
        if (hdnBank.Value != "")
        {
            objincUnit.BankDet.BankDoc = hdnBank.Value;
        }
    }
    #endregion

    #region Common Methods

    public void DataRetrive(int id)
    {
        DataSet ds = new DataSet();
        try
        {
            objincUnit = new Incentive();
            objincUnit.GetVwPrmtrs = new GetAndViewParam();

            objincUnit.strcActioncode = "A";//action
            objincUnit.GetVwPrmtrs.Param1ID = "";//--Incentive Number
            objincUnit.GetVwPrmtrs.Param2ID = "";//--UnitCode
            objincUnit.GetVwPrmtrs.Param3ID = "";//--Proposal/Peal/PC Number
            objincUnit.GetVwPrmtrs.Param4ID = "";
            objincUnit.GetVwPrmtrs.Param5 = "";
            objincUnit.GetVwPrmtrs.Param6 = "";
            objincUnit.GetVwPrmtrs.Param7 = "";
            objincUnit.GetVwPrmtrs.FrmDate = Convert.ToDateTime("1/1/1900");
            objincUnit.GetVwPrmtrs.Todate = Convert.ToDateTime("1/1/1900");
            objincUnit.GetVwPrmtrs.InctType = 4;
            objincUnit.UnqIncentiveId = id;
            objincUnit.FormType = FormNumber.TrainingSubsidy_16;
            ds = IncentiveManager.GetIncentiveAnchorTenant(objincUnit);

            //Training Details
            #region Training Details
            if (ds.Tables[0].Rows.Count > 0)
            {

                TextBox120.Text = ds.Tables[0].Rows[0]["decAmountPaid"].ToString();
                if (ds.Tables[0].Rows[0]["vch_TraineeDetails"].ToString() != "")
                {

                    hdnUploadSatutory4Tdet.Value = ds.Tables[0].Rows[0]["vch_TraineeDetails"].ToString();
                    FileTraineeDetails.Enabled = false;
                    hlnkViewTraineeDetails.NavigateUrl = "../incentives/Files/TrainingDetail/" + ds.Tables[0].Rows[0]["vch_TraineeDetails"].ToString();
                    hlnkViewTraineeDetails.Visible = true;
                    lknDelTraineeDetails.Visible = true;
                }
                if (ds.Tables[0].Rows[0]["vch_MoneyReceiptFile"].ToString() != "")
                {
                    hdnUploadSatutory4Rdet.Value = ds.Tables[0].Rows[0]["vch_MoneyReceiptFile"].ToString();
                    FileReceipt.Enabled = false;
                    lknViewMnyReceipt.NavigateUrl = "../incentives/Files/TrainingDetail/Receipt/" + ds.Tables[0].Rows[0]["vch_MoneyReceiptFile"].ToString();
                    lknViewMnyReceipt.Visible = true;
                    lknDelMnyReceipt.Visible = true;
                }

            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                grdNewRecruited.DataSource = ds.Tables[1];
                grdNewRecruited.DataBind();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                grdSkillUpgrade.DataSource = ds.Tables[2];
                grdSkillUpgrade.DataBind();
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                txtTotal_NewRec.Text = ds.Tables[3].Rows[0]["intTotalNewRec"].ToString();
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                txtTotal_SkillUpgrade.Text = ds.Tables[4].Rows[0]["intTotalSkill"].ToString();
            }
            #endregion
            //Bank Details
            #region "Bank Details"
            DataTable dtBank = new DataTable();
            dtBank = ds.Tables[5];
            if (dtBank.Rows.Count > 0)
            {
                txtAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
                txtBnkNm.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
                txtBranch.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
                txtIFSC.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
                txtMICRNo.Text = dtBank.Rows[0]["VCHMICR"].ToString();
                if (dtBank.Rows[0]["vchBankDoc"].ToString() != "")
                {
                    hdnBank.Value = dtBank.Rows[0]["vchBankDoc"].ToString(); /////AUTHORIZEDFILE file upload
                    hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBank.Rows[0]["vchBankDoc"].ToString();
                    hypBank.Visible = true;
                    lnkBankDelete.Visible = true;
                    fuBank.Enabled = false;
                }
            }
            #endregion

            //Additional  Details Doc
            if (ds.Tables[6].Rows.Count > 0)
            {
                D275.Value = ds.Tables[6].Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
                D274.Value = ds.Tables[6].Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
                D280.Value = ds.Tables[6].Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();

                if (D275.Value != "")
                {
                    string Path = "~/incentives/Files/AdditionalDocument";
                    string filename = D275.Value;
                    FileViewProcess(flValidStatutary, D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, Path, filename);
                }
                if (D274.Value != "")
                {
                    string Path = "~/incentives/Files/AdditionalDocument";
                    string filename = D274.Value;
                    FileViewProcess(flDelay, D274, lnkUDelay, lnkDDelay, hypDelay, lblDelay, Path, filename);
                }
                if (D280.Value != "")
                {
                    string Path = "~/incentives/Files/AdditionalDocument";
                    string filename = D280.Value;
                    FileViewProcess(flCleanApproveAuthority, D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, Path, filename);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ds = null;
        }
    }

    public void FileViewProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string FileName)
    {
        string fileName = FileName;
        if (fileName != "")
        {
            if (File.Exists(Server.MapPath(xPath + "/" + fileName)))
            {
                hdn.Value = fileName;//also in view time
                hyp.NavigateUrl = xPath + "/" + fileName;
                F.Enabled = false;
                LU.Visible = false;

                LD.Visible = true;
                hyp.Visible = true;
                lblMsg.Visible = false;
            }
        }
    }
    public void FillFileUpladControls(DataTable dtFilesDtl)
    {
        if (dtFilesDtl.Rows.Count > 0 && dtFilesDtl != null)
        {
            for (int i = 0; i < dtFilesDtl.Rows.Count; i++)
            {
                DataRow objRow = dtFilesDtl.Rows[i];
                if (objRow["vchDocId"].ToString() == hdnSatutory4TdetDoc.Value)
                {
                    if (objRow["vchFileName"] != null && objRow["vchFileName"] != DBNull.Value && objRow["vchFileName"] != "")
                    {
                        hdnUploadSatutory4Tdet.Value = objRow["vchFileName"].ToString();
                        FileTraineeDetails.Enabled = false;
                        hlnkViewTraineeDetails.NavigateUrl = string.Format("~/incentives/" + objRow["vchFolderPath"].ToString() + "{0}", objRow["vchFileName"]);
                        hlnkViewTraineeDetails.Visible = true;
                        lknDelTraineeDetails.Visible = true;
                    }
                }

                if (objRow["vchDocId"].ToString() == hdnMnyReceipt.Value)
                {
                    if (objRow["vchFileName"] != null && objRow["vchFileName"] != DBNull.Value && objRow["vchFileName"] != "")
                    {
                        hdnUploadSatutory4Rdet.Value = objRow["vchFileName"].ToString();
                        FileReceipt.Enabled = false;
                        lknViewMnyReceipt.NavigateUrl = string.Format("~/incentives/" + objRow["vchFolderPath"].ToString() + "{0}", objRow["vchFileName"]);
                        lknViewMnyReceipt.Visible = true;
                        lknDelMnyReceipt.Visible = true;
                    }
                }

                if (objRow["vchDocId"].ToString() == D275.Value && objRow["vchDocId"].ToString() != "")
                {
                    if (objRow["vchFileName"] != null && objRow["vchFileName"] != DBNull.Value && objRow["vchFileName"] != "")
                    {
                        D275.Value = objRow["vchFileName"].ToString();
                        flValidStatutary.Enabled = false;
                        hypValidStatutary.NavigateUrl = string.Format("~/incentives/" + objRow["vchFolderPath"].ToString() + "{0}", objRow["vchFileName"]);
                        hypValidStatutary.Visible = true;
                        lnkUValidStatutary.Visible = true;
                    }
                }
                if (objRow["vchDocId"].ToString() == D274.Value && objRow["vchDocId"].ToString() != "")
                {
                    if (objRow["vchFileName"] != null && objRow["vchFileName"] != DBNull.Value && objRow["vchFileName"] != "")
                    {
                        hypDelay.Visible = true;
                        hypDelay.NavigateUrl = "../incentives/Files/AdditionalDocument/" + objRow["vchFileName"].ToString();
                        D274.Value = objRow["vchFileName"].ToString();
                        flDelay.Enabled = false;
                        lnkUDelay.Visible = true;
                    }
                }
                if (objRow["vchDocId"].ToString() == D280.Value && objRow["vchDocId"].ToString() != "")
                {
                    if (objRow["vchFileName"] != null && objRow["vchFileName"] != DBNull.Value && objRow["vchFileName"] != "")
                    {
                        hypCleanApproveAuthority.Visible = true;
                        hypCleanApproveAuthority.NavigateUrl = "../incentives/Files/AdditionalDocument/" + objRow["vchFileName"].ToString();
                        D280.Value = objRow["vchFileName"].ToString();
                        flCleanApproveAuthority.Enabled = false;
                        lnkDCleanApproveAuthority.Visible = true;
                    }
                }
            }
        }
    }
    private bool IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf", "application/x-zip-compressed", "image/jpeg", "image/jpg", "application/msxls" };
        string[] allowedExtension = { ".pdf", ".zip", ".jpg", "jpeg", ".xls", ".xlsx" };
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        imageTypes.AddRange(allowedImageTyps);
        imageExtension.AddRange(allowedExtension);
        string strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
        string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);// 
        int count = FileUpload1.FileName.Count(f => f == '.');
        string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
        CommonFunctions cmmf = new CommonFunctions();

        if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //////////////////////////////----------------New File Upload Logic-----------------
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string strFolername)
    {
        try
        {
            string filename = hdnFile.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolername, filename);
            string completePath = Server.MapPath(path);

            // File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
        catch (Exception)
        { }

    }
    private void UploadDocument(FileUpload fuOrgDocument, HiddenField hdnOrgDocument, string strFileName, HyperLink hypOrdDocument, Label lblOrgDocument, LinkButton lnkOrgDocumentDelete, string strFoldername)
    {
        string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFoldername));
        if (!Directory.Exists(strMainFolderPath))
        {
            Directory.CreateDirectory(strMainFolderPath);
        }
        if (fuOrgDocument.HasFile)
        {
            if (!(IsFileValid(fuOrgDocument)))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots. ', '" + MsgTitle + "'); </script>", false);
                return;
            }
            string filename = string.Empty;
            if ((Path.GetExtension(fuOrgDocument.FileName) != ".pdf") && (Path.GetExtension(fuOrgDocument.FileName) != ".zip"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload  PDF,ZIP file Only!','" + Messages.TitleOfProject + "')", true);
                return;
            }
            int fileSize = fuOrgDocument.PostedFile.ContentLength;
            if (fileSize > (4 * 1024 * 1024))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + Messages.TitleOfProject + "')", true);
                return;
            }
            else
            {
                filename = strFileName + Path.GetExtension(fuOrgDocument.FileName);
            }

            fuOrgDocument.SaveAs(strMainFolderPath + filename);
            hdnOrgDocument.Value = filename;
            hypOrdDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFoldername, filename);
            hypOrdDocument.Visible = true;
            lnkOrgDocumentDelete.Visible = true;
            lblOrgDocument.Visible = true;
            fuOrgDocument.Enabled = false;
        }
    }

    /// <summary>
    /// Function to get certificate details for OSPCB/Factory and boiler from service
    /// </summary>
    /// <param name="aEnServiceDocType">Service Doc Type i.e whether boiler or factory</param>
    /// <param name="hdnDocValue">name of hidden field in which the document filename will be stored</param>
    /// <param name="fuUpload">fileupload control for the document to disable them in case service has document</param>
    /// <param name="lnkAdd">add linkbutton for the document to disable them in case service has document</param>
    /// <param name="lnkDel">delete linkbutton for the document to disable them in case service has document</param>
    /// <param name="hyp">Hyperlink to view/download the document</param>
    /// <param name="hdnServiceDocStatus">hidden field to store the status as to whether doc is present or not</param>
    private void BindDocFromService(enServiceDocType aEnServiceDocType, HiddenField hdnDocValue, FileUpload fuUpload, LinkButton lnkAdd, LinkButton lnkDel, HyperLink hyp, HiddenField hdnServiceDocStatus)
    {
        try
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
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }

    /// <summary>
    /// Function to delete all the service document saved in temp folder and create the new ones in pc folder
    /// </summary>
    /// <param name="aEnserviceDocType">service doc type</param>
    /// <param name="hdnFileName">hiddenfield that has the file name</param>
    private void SaveServiceDoc(enServiceDocType aEnserviceDocType, HiddenField hdnFileName, string destfoldername)
    {
        try
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
                string strDestinationFile = Server.MapPath(string.Format("~/incentives/Files/{1}/{0}", hdnFileName.Value, destfoldername));
                File.Copy(strSourceFile, strDestinationFile, true);
                hdnFileName.Value = strPreFix + DateTime.Now.ToString("_ddMMyyhhmmss") + ".zip";
                string strReNameFile = Server.MapPath(string.Format("~/incentives/Files/{1}/{0}", hdnFileName.Value, destfoldername));

                System.IO.File.Move(strDestinationFile, strReNameFile);
                //then delete the old folder and old zip folder
                File.Delete(Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}.zip", strPreFix, Session["investorId"].ToString())));
                Directory.Delete(Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}", strPreFix, Session["investorId"].ToString())), true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void fillSalutation()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "R";
            objBAL.BindDropdown(DdlGender, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    /////////////////////////////////////////////------------------------------------end 
    public void DeleteProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string ModuleName)
    {
        string fileName = hdn.Value;


        if (fileName != "")
            if (File.Exists(Server.MapPath(xPath + "/" + fileName)))
                //File.Delete(Server.MapPath(xPath + "/" + fileName));


                hdn.Value = "";
        hyp.NavigateUrl = "";


        F.Enabled = true;
        LU.Visible = true;

        LD.Visible = false;
        hyp.Visible = false;
        lblMsg.Visible = false;
    }
    public void UploadProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string ModuleName)
    {
        string fileName = "";
        fileName = UploadX(F, xPath, ModuleName);

        if (!(IsFileValid(F)))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots. ', '" + MsgTitle + "'); </script>", false);
            return;
        }
        if (fileName != "")
        {

            if (File.Exists(Server.MapPath(xPath + "/" + fileName)))
            {
                hdn.Value = fileName;//also in view time
                hyp.NavigateUrl = xPath + "/" + fileName;

                F.Enabled = false;
                LU.Visible = false;

                LD.Visible = true;
                hyp.Visible = true;
                lblMsg.Visible = true;//not in view time  (false)
            }
        }
    }
    public string UploadX(FileUpload fileControl, string xPath, string ModuleName)
    {
        string FileName = "";
        try
        {
            if (fileControl.HasFile)
            {
                string yPath = Server.MapPath(xPath);
                if (!Directory.Exists(yPath))
                {
                    Directory.CreateDirectory(yPath);
                }
                string FileExtension = Path.GetExtension(fileControl.FileName);
                //extension check
                if (FileExtension != ".pdf" && FileExtension != ".zip" && FileExtension != ".doc")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "jAlert('File format should be pdf/zip/doc.','" + MsgTitle + "')", true);
                }
                //file size check
                else if (fileControl.PostedFile.ContentLength > (4 * 1028 * 1028))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "jAlert('File size can not be more than 4 MB.','" + MsgTitle + "')", true);
                }
                else
                {
                    FileName = ModuleName + DateTime.Now.ToString("_ddMMyyyyHHmmss_") + DateTime.Now.Millisecond.ToString() + FileExtension;
                    string FileNamewithPath = Server.MapPath(xPath + "/" + FileName);
                    fileControl.SaveAs(FileNamewithPath);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return FileName;
    }
    #endregion

    #region LinkBtn Events
    protected void lnkOrgDocumentPdf_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        if (string.Equals(lnk.ID, lknAddMnyReceipt.ID))
        {
            if (FileReceipt.HasFile)
            {
                string strFileName = "TraineeDtlReceipt" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(FileReceipt, hdnUploadSatutory4Rdet, strFileName, lknViewMnyReceipt, lblMoneyReceipt, lknDelMnyReceipt, "TrainingDetail/Receipt");
            }
        }
        else if (string.Equals(lnk.ID, lknAddTraineeDetails.ID))
        {

            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", "TrainingDetail"));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (FileTraineeDetails.HasFile)
            {
                if (!(IsFileValid(FileTraineeDetails)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots. ', '" + MsgTitle + "'); </script>", false);
                    return;
                }
                string strFileName = "TrainingDetail" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string filename = string.Empty;
                if ((Path.GetExtension(FileTraineeDetails.FileName) != ".xls") && (Path.GetExtension(FileTraineeDetails.FileName) != ".xlsx"))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload  XLS,XLSX file Only!','" + Messages.TitleOfProject + "')", true);
                    return;
                }
                int fileSize = FileTraineeDetails.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + Messages.TitleOfProject + "')", true);

                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(FileTraineeDetails.FileName);
                }
                FileTraineeDetails.SaveAs(strMainFolderPath + filename);
                hdnUploadSatutory4Tdet.Value = filename;
                hlnkViewTraineeDetails.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "TrainingDetail", filename);
                hlnkViewTraineeDetails.Visible = true;
                lknDelTraineeDetails.Visible = true;
                lblTraineeDlt.Visible = true;
                FileTraineeDetails.Enabled = false;
            }
        }
        else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILE.ID))
        {
            if (FlupAUTHORIZEDFILE.HasFile)
            {
                string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, hypAUTHORIZEDFILE, lblAUTHORIZEDFILED, lnkAUTHORIZEDFILEDdelete, "InctBasicDoc");
            }
        }

        else if (string.Equals(lnk.ID, lnkBankUpload.ID))
        {

            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", "Bank"));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (fuBank.HasFile)
            {
                string strFileName = "Bank" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string filename = string.Empty;
                if ((Path.GetExtension(fuBank.FileName).ToLower() != ".pdf") && (Path.GetExtension(fuBank.FileName).ToLower() != ".jpg") && (Path.GetExtension(fuBank.FileName).ToLower() != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .pdf/.jpg/.jpeg file Only!','" + Messages.TitleOfProject + "')", true);
                    return;
                }
                int fileSize = fuBank.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + Messages.TitleOfProject + "')", true);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(fuBank.FileName);
                }
                fuBank.SaveAs(strMainFolderPath + filename);
                hdnBank.Value = filename;
                hypBank.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "Bank", filename);
                hypBank.Visible = true;
                lnkBankDelete.Visible = true;
                lblBank.Visible = true;
                fuBank.Enabled = false;
            }
        }
    }

    protected void lnkOrgDocumentDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, lknDelMnyReceipt.ID))
        {
            UpdFileRemove(hdnUploadSatutory4Rdet, lknAddMnyReceipt, lknDelMnyReceipt, lknViewMnyReceipt, lblMoneyReceipt, FileReceipt, "TrainingDetail/Receipt");
        }

        else if (string.Equals(lnk.ID, lknDelTraineeDetails.ID))
        {
            UpdFileRemove(hdnUploadSatutory4Tdet, lknAddTraineeDetails, lknDelTraineeDetails, hlnkViewTraineeDetails, lblTraineeDlt, FileTraineeDetails, "TrainingDetail");
        }

        else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILEDdelete.ID))
        {
            UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILED, FlupAUTHORIZEDFILE, "InctBasicDoc");
        }

        else if (string.Equals(lnk.ID, lnkBankDelete.ID))
        {
            UpdFileRemove(hdnBank, lnkBankUpload, lnkBankDelete, hypBank, lblBank, fuBank, "Bank");
        }
    }

    protected void lnkUValidStatutary_click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        UploadProcess(flValidStatutary, D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, Path, "ValidStatutary");
    }
    protected void lnkDValidStatutary_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        DeleteProcess(flValidStatutary, D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, Path, "ValidStatutary");
    }
    protected void lnkUDelay_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        UploadProcess(flDelay, D274, lnkUDelay, lnkDDelay, hypDelay, lblDelay, Path, "Delay");
    }
    protected void lnkDDelay_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        DeleteProcess(flDelay, D274, lnkUDelay, lnkDDelay, hypDelay, lblDelay, Path, "Delay");
    }
    protected void lnkUCleanApproveAuthority_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        UploadProcess(flCleanApproveAuthority, D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, Path, "STATUTORYCLEARANCE");
    }
    protected void lnkDCleanApproveAuthority_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        DeleteProcess(flCleanApproveAuthority, D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, Path, "STATUTORYCLEARANCE");
    }
    #endregion

    /////----------------------newly recruited add more
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        grdNewRecruited.DataSource = GetInitialDataNewRec();
        grdNewRecruited.DataBind();
        txtTraineeType_NewRec.Text = string.Empty;
        rbtnInOutHouse_NewRecruit.SelectedValue = "2";
        txtOrganisation_NewRec.Attributes.Add("style", "display:block");
        txtNoOfTrainee_NewRec.Text = string.Empty;
        txtNoOfDays_NewRec.Text = string.Empty;
        txtOrganisation_NewRec.Text = string.Empty;
    }

    public DataTable GetInitialDataNewRec() // this might be your sp for select
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchTraineeType", typeof(string));
            table.Columns.Add("vchTraingLoc", typeof(string));
            table.Columns.Add("intTraingLoc", typeof(string));
            table.Columns.Add("intNoOftrainee", typeof(string));
            table.Columns.Add("intNoOfDays", typeof(string));
            table.Columns.Add("vchOrgName", typeof(string));

            for (int i = 0; i < grdNewRecruited.Rows.Count; i++)
            {
                Label lblTraineeType_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblTraineeType_NewRec");
                Label lblTrainingLoc_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblTrainingLoc_NewRec");
                HiddenField hdnTrainingLoc_NewRec = (HiddenField)grdNewRecruited.Rows[i].FindControl("hdnTrainingLoc_NewRec");
                Label lblNoOfTrainee_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblNoOfTrainee_NewRec");
                Label lblNoOfDays_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblNoOfDays_NewRec");
                Label lblOrg_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblOrg_NewRec");

                table.Rows.Add(lblTraineeType_NewRec.Text, lblTrainingLoc_NewRec.Text, hdnTrainingLoc_NewRec.Value, lblNoOfTrainee_NewRec.Text, lblNoOfDays_NewRec.Text, lblOrg_NewRec.Text);
            }
            table.Rows.Add(txtTraineeType_NewRec.Text, rbtnInOutHouse_NewRecruit.SelectedItem.Text, rbtnInOutHouse_NewRecruit.SelectedValue, txtNoOfTrainee_NewRec.Text, txtNoOfDays_NewRec.Text, txtOrganisation_NewRec.Text);

            return table;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
        return null;
    }

    protected void ImageButtonDelete_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            LinkButton imgbtn = (LinkButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchTraineeType", typeof(string));
            table.Columns.Add("vchTraingLoc", typeof(string));
            table.Columns.Add("intTraingLoc", typeof(string));
            table.Columns.Add("intNoOftrainee", typeof(string));
            table.Columns.Add("intNoOfDays", typeof(string));
            table.Columns.Add("vchOrgName", typeof(string));

            for (int i = 0; i < grdNewRecruited.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label lblTraineeType_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblTraineeType_NewRec");
                    Label lblTrainingLoc_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblTrainingLoc_NewRec");
                    HiddenField hdnTrainingLoc_NewRec = (HiddenField)grdNewRecruited.Rows[i].FindControl("hdnTrainingLoc_NewRec");
                    Label lblNoOfTrainee_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblNoOfTrainee_NewRec");
                    Label lblNoOfDays_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblNoOfDays_NewRec");
                    Label lblOrg_NewRec = (Label)grdNewRecruited.Rows[i].FindControl("lblOrg_NewRec");

                    table.Rows.Add(lblTraineeType_NewRec.Text, lblTrainingLoc_NewRec.Text, hdnTrainingLoc_NewRec.Value, lblNoOfTrainee_NewRec.Text, lblNoOfDays_NewRec.Text, lblOrg_NewRec.Text);
                }
            }

            grdNewRecruited.DataSource = table;
            grdNewRecruited.DataBind();
            if (grdNewRecruited.Rows.Count == 0)
            {
                txtTotal_NewRec.Text = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }

    protected void lnkSkillUpgrade_Click(object sender, EventArgs e)
    {
        grdSkillUpgrade.DataSource = GetInitialDataSkillUpgrade();
        grdSkillUpgrade.DataBind();
        txtTraineeType_SkillUpgrade.Text = string.Empty;
        rbtnInOutHouse_SkillUpgrade.SelectedValue = "2";
        txtOrganisation_SkillUpgrade.Attributes.Add("style", "display:block");
        txtNoOfTrainee_SkillUpgrade.Text = string.Empty;
        txtNoOfDays_SkillUpgrade.Text = string.Empty;
        txtOrganisation_SkillUpgrade.Text = string.Empty;
    }

    public DataTable GetInitialDataSkillUpgrade() // this might be your sp for select
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchTraineeType", typeof(string));
            table.Columns.Add("vchTraingLoc", typeof(string));
            table.Columns.Add("intTraingLoc", typeof(string));
            table.Columns.Add("intNoOftrainee", typeof(string));
            table.Columns.Add("intNoOfDays", typeof(string));
            table.Columns.Add("vchOrgName", typeof(string));

            for (int i = 0; i < grdSkillUpgrade.Rows.Count; i++)
            {
                Label lblTraineeType_SkillUpgrade = (Label)grdSkillUpgrade.Rows[i].FindControl("lblTraineeType_SkillUpgrade");
                Label lblTrainingLoc_SkillUpgrade = (Label)grdSkillUpgrade.Rows[i].FindControl("lblTrainingLoc_SkillUpgrade");
                HiddenField hdnTrainingLoc_SkillUpgrade = (HiddenField)grdSkillUpgrade.Rows[i].FindControl("hdnTrainingLoc_SkillUpgrade");
                Label lblNoOfTrainee_SkillUpgrade = (Label)grdSkillUpgrade.Rows[i].FindControl("lblNoOfTrainee_SkillUpgrade");
                Label lblNoOfDays_SkillUpgrade = (Label)grdSkillUpgrade.Rows[i].FindControl("lblNoOfDays_SkillUpgrade");
                Label lblOrg_SkillUpgrade = (Label)grdSkillUpgrade.Rows[i].FindControl("lblOrg_SkillUpgrade");

                table.Rows.Add(lblTraineeType_SkillUpgrade.Text, lblTrainingLoc_SkillUpgrade.Text, hdnTrainingLoc_SkillUpgrade.Value, lblNoOfTrainee_SkillUpgrade.Text, lblNoOfDays_SkillUpgrade.Text, lblOrg_SkillUpgrade.Text);
            }
            table.Rows.Add(txtTraineeType_SkillUpgrade.Text, rbtnInOutHouse_SkillUpgrade.SelectedItem.Text, rbtnInOutHouse_SkillUpgrade.SelectedValue, txtNoOfTrainee_SkillUpgrade.Text, txtNoOfDays_SkillUpgrade.Text, txtOrganisation_SkillUpgrade.Text);

            return table;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
        return null;
    }

    protected void imgBtnSkillUpgrade_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {

            LinkButton imgbtn = (LinkButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchTraineeType", typeof(string));
            table.Columns.Add("vchTraingLoc", typeof(string));
            table.Columns.Add("intTraingLoc", typeof(string));
            table.Columns.Add("intNoOftrainee", typeof(string));
            table.Columns.Add("intNoOfDays", typeof(string));
            table.Columns.Add("vchOrgName", typeof(string));

            for (int i = 0; i < grdSkillUpgrade.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label lblTraineeType_SkillUpgrade = (Label)grdNewRecruited.Rows[i].FindControl("lblTraineeType_SkillUpgrade");
                    Label lblTrainingLoc_SkillUpgrade = (Label)grdNewRecruited.Rows[i].FindControl("lblTrainingLoc_SkillUpgrade");
                    HiddenField hdnTrainingLoc_SkillUpgrade = (HiddenField)grdNewRecruited.Rows[i].FindControl("hdnTrainingLoc_SkillUpgrade");
                    Label lblNoOfTrainee_SkillUpgrade = (Label)grdNewRecruited.Rows[i].FindControl("lblNoOfTrainee_SkillUpgrade");
                    Label lblNoOfDays_SkillUpgrade = (Label)grdNewRecruited.Rows[i].FindControl("lblNoOfDays_SkillUpgrade");
                    Label lblOrg_SkillUpgrade = (Label)grdNewRecruited.Rows[i].FindControl("lblOrg_SkillUpgrade");

                    table.Rows.Add(lblTraineeType_SkillUpgrade.Text, lblTrainingLoc_SkillUpgrade.Text, hdnTrainingLoc_SkillUpgrade.Value, lblNoOfTrainee_SkillUpgrade.Text, lblNoOfDays_SkillUpgrade.Text, lblOrg_SkillUpgrade.Text);
                }
            }

            grdSkillUpgrade.DataSource = table;
            grdSkillUpgrade.DataBind();
            if (grdSkillUpgrade.Rows.Count == 0)
            {
                txtTotal_SkillUpgrade.Text = "0";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            table = null;
        }
    }

    //-------------------------------------End of Skill Upgradation

    protected void grdNewRecruited_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoOfTrainee_NewRec = (Label)e.Row.FindControl("lblNoOfTrainee_NewRec");
                intCalTotNewRec += Convert.ToInt32(lblNoOfTrainee_NewRec.Text);

            }
            txtTotal_NewRec.Text = intCalTotNewRec.ToString();
        }
        catch (Exception)
        {
        }
    }

    protected void grdSkillUpgrade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblNoOfTrainee_SkillUpgrade = (Label)e.Row.FindControl("lblNoOfTrainee_SkillUpgrade");
                intCalTotSkillUp += Convert.ToInt32(lblNoOfTrainee_SkillUpgrade.Text);

            }
            txtTotal_SkillUpgrade.Text = intCalTotSkillUp.ToString();
        }
        catch (Exception)
        {
        }
    }
}

