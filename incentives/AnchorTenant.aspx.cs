using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAcessLayer.Common;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;


public partial class Subsidy_Plant_MC : SessionCheck
{
    private int IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf", "application/x-zip-compressed" };
        string[] allowedExtension = { ".pdf", ".zip" };
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
            return 0;
        }
        else
        {
            return 1;
        }
    }
    #region "Variable Declaration"
    Incentive objInc = null;
    MajorOperationOfCompany objMajor = null;
    BankDetails obj = null;
    List<MajorOperationItmDtl> listItm = new List<MajorOperationItmDtl>();
    MajorOperationItmDtl objItmDtl = null;
    BriefDtlPropActivity objBrfDtl = null;
    ProposedCommonFacility onjPropCmmnFacl = null;
    List<ProposedCommonFacility> listPropCmnFacl = new List<ProposedCommonFacility>();
    string projname = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        txtDLSWCADateOfApproval.Attributes.Add("readonly", "readonly");///// DLSWCA section

        if (!IsPostBack)
        {
            fillSalutation();
            SetInitialRowItineraryProp();
            FillPageHeaderDtl();
            if (Convert.ToString(Session["ApplySource"]) == "0")
            {
                PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
                FillFormDetails(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            }
            else
            {

                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
                FillPrepopulateFiles();
                PrepopulateDataPlus(Convert.ToInt16(Session["InvestorId"]));
            }
        }
    }
    public void PrepopulateDataPlus(int id)
    {
        DataSet ds = IncentiveManager.PostpopulateDataPLUS(id);
        DataTable dtBank = ds.Tables[0];
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
    public void FillPrepopulateFiles()
    {

        objInc = new Incentive();
        objInc.UnitCode = Convert.ToString(Session["UnitCode"]);
        DataSet dslivePreFile = IncentiveManager.PrepopulateFile(objInc);
        if (dslivePreFile.Tables[0].Rows.Count > 0)
        {
            FillFileUpladControls(dslivePreFile.Tables[0]);
        }

    }
    #region Fill Page Header Details
    private void FillPageHeaderDtl()
    {
        try
        {
            objInc = new Incentive();
            objInc.strcActioncode = "M";
            objInc.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveMaster(objInc);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblTitle.Text = ds.Tables[0].Rows[0]["vchInctName"].ToString();
                hdnTimeFrame.Value = ds.Tables[0].Rows[0]["intTimeFrame"].ToString();
                hdnPostSubFlag.Value = ds.Tables[0].Rows[0]["intPostSubmissionFlag"].ToString();

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
    public void FillUnit(DropDownList ddl, string action)
    {
        DataTable table = new DataTable();
        try
        {
            CommonDataLayer objDataUnit = new CommonDataLayer();
            ddl.DataTextField = "vchName";
            ddl.DataValueField = "slno";
            ddl.DataSource = objDataUnit.FillUnitType(action);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-Select-", "0"));
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
    public void PrepopulateData(int id)
    {
        DataSet dslivePre = IncentiveManager.PrepopulateData(id);
        DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
        DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment

        DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
        DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
        DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
        DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
        DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
        DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan

        //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();


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



        //if (dtProductionDetBefPre.Rows.Count > 0)
        //{
        //    Grd_Production_Before.DataSource = dtProductionDetBef;
        //    Grd_Production_Before.DataBind();
        //}

        //if (dtProductionDetAftPre.Rows.Count > 0)
        //{
        //    Grd_Production_After.DataSource = dtProductionDetAft;
        //    Grd_Production_After.DataBind();
        //}		

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



    }
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

        //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();

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

            //dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            //Lbl_Org_Doc_Type.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
            Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
            //dtindustryPre.Rows[0]["vchCertOfRegdDocCode"].ToString();		


            //dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
            lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
            lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
            lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
            lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
            lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();
            //dtindustryPre.Rows[0]["vchProdCommCertBeforeCode"].ToString();	
            //Lbl_Prod_Comm_Before_Doc_Name.Text = dtindustryPre.Rows[0]["ProdCommCertBeforeDocName"].ToString();
            //Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchProdCommCertBefore"].ToString();
            //dtindustryPre.Rows[0]["vchProdCommCertBefore"].ToString();


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
            //dtindustryPre.Rows[0]["vchProdCommCertAfterCode"].ToString();		
            //Lbl_Prod_Comm_After_Doc_Name.Text = dtindustryPre.Rows[0]["ProdCommCertAfterDocName"].ToString();
            //Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchProdCommCertAfter"].ToString();



            if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
            {
                Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
            }
            else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            {
                Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
            }

            ////dtindustryPre.Rows[0]["intDistrictCode"].ToString();			
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

                TxtAdhaar1.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString();
                //TxtAdhaar2.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Substring(4, 4);
                //TxtAdhaar3.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Substring(8, 4);
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


            //dtProductionPre.Rows[0]["intProductionId"].ToString();
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
            //dtProductionPre.Rows[0]["vchEmpDocBeforeCode"].ToString();			
            Lbl_Direct_Emp_Before_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocBeforeCodeName"].ToString();
            Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString();
            //dtProductionPre.Rows[0]["vchEmpDocAfterCode"].ToString();			
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

            //dtProductionPre.Rows[0]["intCreatedBy"].ToString();
        }






        //,vchFFCIDocBeforeCode,,,,,
        //    ,,INT_INCUNQUEID,vchProjectDocBeforeCode,vchProjectDocBefore,dtmFFCIDateAfter,
        //    vchFFCIDocAfterCode,vchFFCIDocAfter,decLandAmtAfter,decBuildingAmtAfter,decPlantMachAmtAfter,decOtheFixedAssetAmtAfter,
        //    decTotalAmtAfter,vchProjectDocAfterCode,vchProjectDocAfter,INT_CREATED_BY,DTM_CREATEDON

        //dtInvestmentPre.Rows[0]["slno"].ToString();
        if (dtInvestmentPre.Rows.Count > 0)
        {
            Txt_FFCI_Date_Before.Text = dtInvestmentPre.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString();
            //dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCode"].ToString();
            Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();

            Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString();


            lbl_Land_Before.Text = dtInvestmentPre.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
            lbl_Building_Before.Text = dtInvestmentPre.Rows[0]["DEC_Building"].ToString();
            lbl_Plant_Mach_Before.Text = dtInvestmentPre.Rows[0]["DEC_Plant_Machinery"].ToString();
            lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPre.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
            lbl_Total_Capital_Before.Text = dtInvestmentPre.Rows[0]["DEC_Total"].ToString();
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
        }

    }
    public void FillFormDetails(int id)
    {
        try
        {


            objInc = new Incentive();
            objInc.GetVwPrmtrs = new GetAndViewParam();
            objInc.GetVwPrmtrs.Param1ID = "";
            objInc.GetVwPrmtrs.Param2ID = "";
            objInc.GetVwPrmtrs.Param3ID = "";
            objInc.GetVwPrmtrs.Param4ID = "";
            objInc.GetVwPrmtrs.InctType = 4;
            objInc.UnqIncentiveId = id;
            objInc.FormType = FormNumber.AnchorTenant_17;
            DataSet dslive = IncentiveManager.GetIncentiveAnchorTenant(objInc);

            #region Fill DLSWCA / SLSWCA / HLCA Apporval Details
            DataTable dtDLSWCA = dslive.Tables[2];
            FillDLSWCADetails(dtDLSWCA);
            #endregion


            #region Fill Bank Details
            DataTable dtBankDetails = dslive.Tables[3];
            FillBankDetails(dtBankDetails, "0");
            #endregion


            #region Fill Brief details of Proposed Activity
            DataTable dtBriefDetails = dslive.Tables[0];
            DataTable dtBriefDetailsDtl = dslive.Tables[1];
            FillBriefDeatils(dtBriefDetails, dtBriefDetailsDtl);
            #endregion




        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #region Fill File Upload
    public void FillFileUpladControls(DataTable dtFilesDtl)
    {
        if (dtFilesDtl.Rows.Count > 0 && dtFilesDtl != null)
        {
            for (int i = 0; i < dtFilesDtl.Rows.Count; i++)
            {
                DataRow objgRow = dtFilesDtl.Rows[i];

                if (objgRow["vchDocId"].ToString() == hdnDtlOfSecTntnDocId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        hdnDtlOfSecTnt.Value = objgRow["vchFileName"].ToString();
                        fupDtlOfSecTnt.Enabled = false;
                        lknViewDtlOfSecTnt.NavigateUrl = string.Format("~/incentives/" + objgRow["vchFolderPath"].ToString() + "{0}", objgRow["vchFileName"]);
                        lknViewDtlOfSecTnt.Visible = true;
                        lnkDelDtlOfSecTnt.Visible = true;
                    }
                }
                if (objgRow["vchDocId"].ToString() == hdnDtlBusnessPlanDocId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        hdnDtlBusnessPlan.Value = objgRow["vchFileName"].ToString();
                        fupldDtlBusnessPlanUpload.Enabled = false;
                        lknViewDtlBusnessPlan.NavigateUrl = string.Format("~/incentives/" + objgRow["vchFolderPath"].ToString() + "{0}", objgRow["vchFileName"]);
                        lknViewDtlBusnessPlan.Visible = true;
                        lnkDelDtlBusnessPlan.Visible = true;
                    }
                }

                if (objgRow["vchDocId"].ToString() == hdnConsetSecUploadDocId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        hdnConsetSecUpload.Value = objgRow["vchFileName"].ToString();
                        fupldConsetSecUpload.Enabled = false;
                        lknViewConsetSecUpload.NavigateUrl = string.Format("~/incentives/" + objgRow["vchFolderPath"].ToString() + "{0}", objgRow["vchFileName"]);
                        lknViewConsetSecUpload.Visible = true;
                        lnkDelConsetSecUpload.Visible = true;
                    }
                }
                if (objgRow["vchDocId"].ToString() == hdnDLSWCAApprovalDocId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        hdnDLSWCAApprovalDoc.Value = objgRow["vchFileName"].ToString();
                        fupDLSWCAApprovalDocUpload.Enabled = false;
                        lnkDLSWCAApprovalDocView.NavigateUrl = string.Format("~/incentives/" + objgRow["vchFolderPath"].ToString() + "{0}", objgRow["vchFileName"]);
                        lnkDLSWCAApprovalDocView.Visible = true;
                        lnkDelDLSWCAApprovalDoc.Visible = true;
                    }
                }
                if (objgRow["vchDocId"].ToString() == hdnDLSWCASubstanDocId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        hdnDLSWCASubstanDoc.Value = objgRow["vchFileName"].ToString();
                        fupDLSWCASubstanDocUpload.Enabled = false;
                        lnkDLSWCASubstanDocView.NavigateUrl = string.Format("~/incentives/" + objgRow["vchFolderPath"].ToString() + "{0}", objgRow["vchFileName"]);
                        lnkDLSWCASubstanDocView.Visible = true;
                        lnkDelDLSWCASubstanDoc.Visible = true;
                    }
                }



            }

        }
    }
    #endregion
    #region Fill Brief details of Proposed Activity

    public void FillBriefDeatils(DataTable dtBriefDetails, DataTable dtBriefDetailsDtl)
    {
        if (dtBriefDetailsDtl.Rows.Count > 0)
        {
            grdPropCmmnFacl.DataSource = dtBriefDetailsDtl;
            grdPropCmmnFacl.DataBind();
            ViewState["CurrentListItemProp"] = dtBriefDetailsDtl;
            strUpdateStatusBrief.Value = "2";
        }

        if (dtBriefDetails.Rows.Count > 0)
        {
            if (dtBriefDetails.Rows[0]["vchBriefDtlProposed"].ToString() != "")
            {
                txtBriefDtlProposed.Text = dtBriefDetails.Rows[0]["vchBriefDtlProposed"].ToString();
            }
            if (dtBriefDetails.Rows[0]["vchProsDwnStrm"].ToString() != "")
            {
                txtPropDwnstrm.Text = dtBriefDetails.Rows[0]["vchProsDwnStrm"].ToString();
            }
            if (dtBriefDetails.Rows[0]["vchProsAncillary"].ToString() != "")
            {
                txtPropAnci.Text = dtBriefDetails.Rows[0]["vchProsAncillary"].ToString();
            }
            if (dtBriefDetails.Rows[0]["vchDevelopUtility"].ToString() != "")
            {
                txtDevelopUtil.Text = dtBriefDetails.Rows[0]["vchDevelopUtility"].ToString();
            }
            if (dtBriefDetails.Rows[0]["vchExternalities"].ToString() != "")
            {
                txtExternalities.Text = dtBriefDetails.Rows[0]["vchExternalities"].ToString();
            }
            if (dtBriefDetails.Rows[0]["vchProposedCFC"].ToString() != "")
            {
                txtPropCfc.Text = dtBriefDetails.Rows[0]["vchProposedCFC"].ToString();
            }
            if (dtBriefDetails.Rows[0]["vchAnyOthers"].ToString() != "")
            {
                txtOthers.Text = dtBriefDetails.Rows[0]["vchAnyOthers"].ToString();
            }

            if (dtBriefDetails.Rows[0]["vchDtlOfSecondTnt"].ToString() != "")
            {

                hdnDtlOfSecTnt.Value = dtBriefDetails.Rows[0]["vchDtlOfSecondTnt"].ToString();
                fupDtlOfSecTnt.Enabled = false;
                lknViewDtlOfSecTnt.NavigateUrl = "../incentives/Files/BriefDetailsProposed/" + dtBriefDetails.Rows[0]["vchDtlOfSecondTnt"].ToString();
                lknViewDtlOfSecTnt.Visible = true;
                lnkDelDtlOfSecTnt.Visible = true;
            }
            if (dtBriefDetails.Rows[0]["vchdtlAttractSecndTnt"].ToString() != "")
            {
                hdnDtlBusnessPlan.Value = dtBriefDetails.Rows[0]["vchDtlOfSecondTnt"].ToString();
                fupldDtlBusnessPlanUpload.Enabled = false;
                lknViewDtlBusnessPlan.NavigateUrl = "../incentives/Files/BriefDetailsProposed/" + dtBriefDetails.Rows[0]["vchdtlAttractSecndTnt"].ToString();
                lknViewDtlBusnessPlan.Visible = true;
                lnkDelDtlBusnessPlan.Visible = true;
            }

            if (dtBriefDetails.Rows[0]["vchConsetSecndTnt"].ToString() != "")
            {
                hdnConsetSecUpload.Value = dtBriefDetails.Rows[0]["vchConsetSecndTnt"].ToString();
                fupldConsetSecUpload.Enabled = false;
                lknViewConsetSecUpload.NavigateUrl = "../incentives/Files/BriefDetailsProposed/" + dtBriefDetails.Rows[0]["vchConsetSecndTnt"].ToString();
                lknViewConsetSecUpload.Visible = true;
                lnkDelConsetSecUpload.Visible = true;
            }


        }

    }

    #endregion
    #region DLSWCA / SLSWCA / HLCA Apporval Details
    public void FillDLSWCADetails(DataTable dtDLSWCA)
    {
        if (dtDLSWCA.Rows.Count > 0)
        {
            if (dtDLSWCA.Rows[0]["DTMAPPROVALDATE"].ToString() != "")
            {
                txtDLSWCADateOfApproval.Text = dtDLSWCA.Rows[0]["DTMAPPROVALDATE"].ToString();
            }
            if (dtDLSWCA.Rows[0]["DCMLANDAPPROVED"].ToString() != "")
            {
                txtDLSWCALandApproved.Text = dtDLSWCA.Rows[0]["DCMLANDAPPROVED"].ToString();
            }
            if (dtDLSWCA.Rows[0]["DCMCOSTOFLAND"].ToString() != "")
            {
                txtDLSWCALandCost.Text = dtDLSWCA.Rows[0]["DCMCOSTOFLAND"].ToString();
            }
            if (dtDLSWCA.Rows[0]["DCMAMOUNTELIGIBLE"].ToString() != "")
            {
                txtDLSWCASubsidyAmt.Text = dtDLSWCA.Rows[0]["DCMAMOUNTELIGIBLE"].ToString();
            }

            if (dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString() != "")
            {

                hdnDLSWCAApprovalDoc.Value = dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString();
                fupDLSWCAApprovalDocUpload.Enabled = false;
                lnkDLSWCAApprovalDocView.NavigateUrl = "../incentives/Files/DLSWCA/" + dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString();
                lnkDLSWCAApprovalDocView.Visible = true;
                lnkDelDLSWCAApprovalDoc.Visible = true;

            }
            if (dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString() != "")
            {
                hdnDLSWCASubstanDoc.Value = dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString();
                fupDLSWCASubstanDocUpload.Enabled = false;
                lnkDLSWCASubstanDocView.NavigateUrl = "../incentives/Files/DLSWCA/" + dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString();
                lnkDLSWCASubstanDocView.Visible = true;
                lnkDelDLSWCASubstanDoc.Visible = true;
            }

        }

    }
    #endregion
    #region Fill Bank Details
    public void FillBankDetails(DataTable dtBankDetails, string strStatus)
    {
        if (dtBankDetails.Rows.Count > 0)
        {

            txtBnkNm.Text = dtBankDetails.Rows[0]["VCHBANKNAME"].ToString();
            txtBranch.Text = dtBankDetails.Rows[0]["VCHBRANCHNAME"].ToString();
            txtIFSC.Text = dtBankDetails.Rows[0]["VCHIFSCNO"].ToString();
            txtAccNo.Text = dtBankDetails.Rows[0]["VCHACCOUNTNO"].ToString();
            txtMICRNo.Text = dtBankDetails.Rows[0]["VCHMICR"].ToString();
            if (dtBankDetails.Rows[0]["vchBankDoc"].ToString() != "")
            {
                hdnBank.Value = dtBankDetails.Rows[0]["vchBankDoc"].ToString();
                hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBankDetails.Rows[0]["vchBankDoc"].ToString();
                hypBank.Visible = true;
                lnkBankDelete.Visible = true;
                fuBank.Enabled = false;
            }
        }
    }
    #endregion
    protected void btnApply_Click(object sender, EventArgs e)
    {

        try
        {

            objInc = new Incentive();

            #region Bank Details
            AddBankDetails();
            #endregion

            #region Requirment of land approved by DLSWCA / SLSWCA / HLCA
            AddDLSWCA_SLSWCA();
            #endregion


            #region Brief details of Proposed Activity
            AddProposedActivity();
            #endregion

            #region Industrial Unit
            INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();
            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            if (DdlGender.SelectedIndex > 0)
            {
                objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            }

            if (radApplyBy.SelectedIndex > -1)
            {
                objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);

            }
            objINDUSTRIAL_UNIT_MASTER.AADHAARNO_IND = TxtAdhaar1.Text;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILECODE_IND = hdnAUTHORIZEDFILEDocId.Value;
            objInc.IndsutUnitMstDet = objINDUSTRIAL_UNIT_MASTER;

            #endregion
            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objInc.UnqIncentiveId = 0;
            }
            else
            {
                objInc.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }
            objInc.strcActioncode = "A";
            objInc.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objInc.PealNum = Convert.ToString(Session["ProposalNo"]);
            objInc.PCNum = Convert.ToString(Session["PCNo"]);
            objInc.UnitCode = Convert.ToString(Session["UnitCode"]);
            objInc.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objInc.Userid = Convert.ToInt16(Session["InvestorId"]);
            objInc.Createdby = Convert.ToInt16(Session["InvestorId"]);
            objInc.incentivetype = 4;
            objInc.FormType = FormNumber.AnchorTenant_17;

            objInc.FileUploadDetails = getFileUploadDatatable();


            string retval = IncentiveManager.CreateIncentiveAnchorTenant(objInc).ToString();
            int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
            /////------------------------------------------------------------------------------------------------
            Response.Redirect("FormPreview_AnchorTenant.aspx?InctUniqueNo=" + Convert.ToString(mstyp));

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }


    }
    /// <summary>
    /// Created by Anjali Panigrahi
    /// To save all data in draft
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        try
        {
            objInc = new Incentive();

            #region Bank Details
            AddBankDetails();
            #endregion

            #region Requirment of land approved by DLSWCA / SLSWCA / HLCA
            AddDLSWCA_SLSWCA();
            #endregion


            #region Brief details of Proposed Activity
            AddProposedActivity();
            #endregion

            #region Industrial Unit
            INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();
            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            if (DdlGender.SelectedIndex > 0)
            {
                objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            }

            if (radApplyBy.SelectedIndex > -1)
            {
                objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);

            }
            objINDUSTRIAL_UNIT_MASTER.AADHAARNO_IND = TxtAdhaar1.Text;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILECODE_IND = hdnAUTHORIZEDFILEDocId.Value;
            objInc.IndsutUnitMstDet = objINDUSTRIAL_UNIT_MASTER;

            #endregion
            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objInc.UnqIncentiveId = 0;
            }
            else
            {
                objInc.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }
            objInc.strcActioncode = "A";
            objInc.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objInc.PealNum = Convert.ToString(Session["ProposalNo"]);
            objInc.PCNum = Convert.ToString(Session["PCNo"]);
            objInc.UnitCode = Convert.ToString(Session["UnitCode"]);
            objInc.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objInc.Userid = Convert.ToInt16(Session["InvestorId"]);
            objInc.Createdby = Convert.ToInt16(Session["InvestorId"]);
            objInc.incentivetype = 4;
            objInc.FormType = FormNumber.AnchorTenant_17;

            objInc.FileUploadDetails = getFileUploadDatatable();

            string retval = IncentiveManager.CreateIncentiveAnchorTenant(objInc).ToString();

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

        if (hdnDtlOfSecTnt.Value != "")
        {
            DataRow dorgRowTR = dtFiles.NewRow();
            dorgRowTR["vchDocId"] = hdnDtlOfSecTntnDocId.Value;
            dorgRowTR["vchFileName"] = hdnDtlOfSecTnt.Value;
            dorgRowTR["vchFilePath"] = "Files/BriefDetailsProposed/";
            dtFiles.Rows.Add(dorgRowTR);
        }


        if (hdnDtlBusnessPlan.Value != "")
        {
            DataRow dorgRowSC = dtFiles.NewRow();
            dorgRowSC["vchDocId"] = hdnDtlBusnessPlanDocId.Value;
            dorgRowSC["vchFileName"] = hdnDtlBusnessPlan.Value;
            dorgRowSC["vchFilePath"] = "Files/BriefDetailsProposed/";
            dtFiles.Rows.Add(dorgRowSC);
        }

        if (hdnConsetSecUpload.Value != "")
        {
            DataRow dorgRow = dtFiles.NewRow();
            dorgRow["vchDocId"] = hdnConsetSecUploadDocId.Value;
            dorgRow["vchFileName"] = hdnConsetSecUpload.Value;
            dorgRow["vchFilePath"] = "Files/BriefDetailsProposed/";
            dtFiles.Rows.Add(dorgRow);
        }


        if (hdnDLSWCAApprovalDoc.Value != "")
        {
            DataRow dorgRowDLSWCAAppr = dtFiles.NewRow();
            dorgRowDLSWCAAppr["vchDocId"] = hdnDLSWCAApprovalDocId.Value;
            dorgRowDLSWCAAppr["vchFileName"] = hdnDLSWCAApprovalDoc.Value;
            dorgRowDLSWCAAppr["vchFilePath"] = "Files/DLSWCA/";
            dtFiles.Rows.Add(dorgRowDLSWCAAppr);
        }


        if (hdnDLSWCASubstanDoc.Value != "")
        {
            DataRow dorgRowDLSWCASubstan = dtFiles.NewRow();
            dorgRowDLSWCASubstan["vchDocId"] = hdnDLSWCASubstanDocId.Value;
            dorgRowDLSWCASubstan["vchFileName"] = hdnDLSWCASubstanDoc.Value;
            dorgRowDLSWCASubstan["vchFilePath"] = "Files/DLSWCA/";
            dtFiles.Rows.Add(dorgRowDLSWCASubstan);
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
    public void AddBankDetails()
    {
        obj = new BankDetails();
        obj.BankName = txtBnkNm.Text;
        obj.BranchName = txtBranch.Text;
        obj.IFSCNo = txtIFSC.Text;
        obj.AccountNo = txtAccNo.Text;
        obj.MICRNo = txtMICRNo.Text;
        if (hdnBank.Value != "")
        {
            obj.BankDoc = hdnBank.Value;
        }
        objInc.BankDet = obj;
    }

    public void AddDLSWCA_SLSWCA()
    {
        DLSWCAApprovalDtls objDLS = new DLSWCAApprovalDtls();
        objDLS.dtmApprovalDate = txtDLSWCADateOfApproval.Text == "" ? "1/1/1900" : txtDLSWCADateOfApproval.Text;
        objDLS.dcmLandRequired = txtDLSWCALandApproved.Text == "" ? Convert.ToDecimal(0.00) : Convert.ToDecimal(txtDLSWCALandApproved.Text);
        objDLS.dcmCostOfLand = txtDLSWCALandCost.Text == "" ? Convert.ToDecimal(0.00) : Convert.ToDecimal(txtDLSWCALandCost.Text);
        objDLS.dcmSubsidyAmount = txtDLSWCASubsidyAmt.Text == "" ? Convert.ToDecimal(0.00) : Convert.ToDecimal(txtDLSWCASubsidyAmt.Text);
        objDLS.strDLSWCAApprovalDoc = hdnDLSWCAApprovalDoc.Value;
        objDLS.strsubstantitateDoc = hdnDLSWCASubstanDoc.Value;
        objInc.DLSWCAApprovalDet = objDLS;
    }
    /// <summary>
    /// Function to add Major operational activities of Company
    /// </summary>


    /// <summary>
    /// Function to add Brief details of Proposed Activity
    /// </summary>
    public void AddProposedActivity()
    {

        objBrfDtl = new BriefDtlPropActivity();
        List<ProposedCommonFacility> listItmProp = new List<ProposedCommonFacility>();
        onjPropCmmnFacl = new ProposedCommonFacility();

        objBrfDtl.vchBriefDtlProposed = txtBriefDtlProposed.Text;
        objBrfDtl.vchProsDwnStrm = txtPropDwnstrm.Text;
        objBrfDtl.vchProsAncillary = txtPropAnci.Text;
        objBrfDtl.vchDevelopUtility = txtDevelopUtil.Text;
        objBrfDtl.vchExternalities = txtExternalities.Text;
        objBrfDtl.vchProposedCFC = txtPropCfc.Text;
        objBrfDtl.vchAnyOthers = txtOthers.Text;
        if (strUpdateStatusBrief.Value == "2")
        {
            DataTable dt = new DataTable();
            if (ViewState["CurrentListItemProp"] != null)
            {
                dt = (DataTable)ViewState["CurrentListItemProp"];
            }
            if (dt.Rows.Count > 0)
            {
                listItmProp = dt.AsEnumerable().Select(m => new ProposedCommonFacility()
                {
                    intPropComSecTnt = m.Field<int>("intPropComSecTnt"),
                    intSlNo = m.Field<int>("intSlNo"),
                    vchPropCommonFacility = m.Field<string>("vchPropCommonFacility")

                }).ToList();
            }
        }
        else
        {

            listItmProp = (List<ProposedCommonFacility>)ViewState["CurrentListItemProp"];
        }
        objBrfDtl.lstProposedCommonFacility = listItmProp;
        objBrfDtl.vchDtlOfSecondTnt = hdnDtlOfSecTnt.Value;
        objBrfDtl.vchdtlAttractSecndTnt = hdnDtlBusnessPlan.Value;
        objBrfDtl.vchConsetSecndTnt = hdnConsetSecUpload.Value;


        objInc.BriefDtlPropActvy = objBrfDtl;
    }
    //////////////////////////////----------------New File Upload Logic-----------------
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string strFolername)
    {
        try
        {
            string filename = hdnFile.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolername, filename);
            string completePath = Server.MapPath(path);

            //File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

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
            string filename = string.Empty;

            if (IsFileValid(fuOrgDocument) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }

            if ((Path.GetExtension(fuOrgDocument.FileName) != ".pdf") && (Path.GetExtension(fuOrgDocument.FileName) != ".zip"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,ZIP file Only!')", true);
                return;
            }
            int fileSize = fuOrgDocument.PostedFile.ContentLength;
            if (fileSize > (4 * 1024 * 1024))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
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
    protected void lnkOrgDocumentPdf_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, lnkAUTHORIZEDFILE.ID))
        {
            if (FlupAUTHORIZEDFILE.HasFile)
            {
                string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, hypAUTHORIZEDFILE, lblAUTHORIZEDFILED, lnkAUTHORIZEDFILEDdelete, "InctBasicDoc");
            }
        }
        else if (string.Equals(lnk.ID, lnkAddDtlOfSecTnt.ID))
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", "BriefDetailsProposed"));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (fupDtlOfSecTnt.HasFile)
            {
                string strFileName = "BriefDtlsProposed" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string filename = string.Empty;
                if ((Path.GetExtension(fupDtlOfSecTnt.FileName) != ".xls") && (Path.GetExtension(fupDtlOfSecTnt.FileName) != ".xlsx"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  XLS,XLSX file Only!')", true);
                    return;
                }
                int fileSize = fupDtlOfSecTnt.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(fupDtlOfSecTnt.FileName);
                }
                fupDtlOfSecTnt.SaveAs(strMainFolderPath + filename);
                hdnDtlOfSecTnt.Value = filename;
                lknViewDtlOfSecTnt.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", "BriefDetailsProposed", filename);
                lknViewDtlOfSecTnt.Visible = true;
                lnkDelDtlOfSecTnt.Visible = true;
                lblDtlOfSecTnt.Visible = true;
                fupDtlOfSecTnt.Enabled = false;
            }
        }

        else if (string.Equals(lnk.ID, lnkAddDtlBusnessPlan.ID))
        {
            if (fupldDtlBusnessPlanUpload.HasFile)
            {

                string strFileName = "BriefDtlsProposed" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(fupldDtlBusnessPlanUpload, hdnDtlBusnessPlan, strFileName, lknViewDtlBusnessPlan, lbltlBusnessPlan, lnkDelDtlBusnessPlan, "BriefDetailsProposed");
            }
        }

        else if (string.Equals(lnk.ID, lnkAddConsetSecUpload.ID))
        {
            if (fupldConsetSecUpload.HasFile)
            {

                string strFileName = "BriefDtlsProposed" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(fupldConsetSecUpload, hdnConsetSecUpload, strFileName, lknViewConsetSecUpload, lblConsetSecUpload, lnkDelConsetSecUpload, "BriefDetailsProposed");
            }
        }
        else if (string.Equals(lnk.ID, lnkAddDLSWCAApprovalDoc.ID))
        {
            if (fupDLSWCAApprovalDocUpload.HasFile)
            {

                string strFileName = "DLSWCA" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(fupDLSWCAApprovalDocUpload, hdnDLSWCAApprovalDoc, strFileName, lnkDLSWCAApprovalDocView, lblDLSWCAApprovalDoc, lnkDelDLSWCAApprovalDoc, "DLSWCA");
            }
        }
        else if (string.Equals(lnk.ID, lnkAddDLSWCASubstanDoc.ID))
        {
            if (fupDLSWCASubstanDocUpload.HasFile)
            {
                string strFileName = "AddDocument" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(fupDLSWCASubstanDocUpload, hdnDLSWCASubstanDoc, strFileName, lnkDLSWCASubstanDocView, lblDLSWCASubstanDoc, lnkDelDLSWCASubstanDoc, "DLSWCA");
            }
        }
        else if (string.Equals(lnk.ID, lnkBankUpload.ID))
        {
            if (fuBank.HasFile)
            {
                string strFileName = "Bank" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Bank";
                UploadDocument(fuBank, hdnBank, strFileName, hypBank, lblBank, lnkBankDelete, strFolderName);
            }
        }
    }
    protected void lnkOrgDocumentDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, lnkAUTHORIZEDFILEDdelete.ID))
        {
            UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILED, FlupAUTHORIZEDFILE, "InctBasicDoc");
        }
        else if (string.Equals(lnk.ID, lnkDelDtlOfSecTnt.ID))
        {
            UpdFileRemove(hdnDtlOfSecTnt, lnkAddDtlOfSecTnt, lnkDelDtlOfSecTnt, lknViewDtlOfSecTnt, lblDtlOfSecTnt, fupDtlOfSecTnt, "BriefDetailsProposed");
        }
        else if (string.Equals(lnk.ID, lnkDelDtlBusnessPlan.ID))
        {
            UpdFileRemove(hdnDtlBusnessPlan, lnkAddDtlBusnessPlan, lnkDelDtlBusnessPlan, lknViewDtlBusnessPlan, lbltlBusnessPlan, fupldDtlBusnessPlanUpload, "BriefDetailsProposed");
        }
        else if (string.Equals(lnk.ID, lnkDelDLSWCAApprovalDoc.ID))
        {
            UpdFileRemove(hdnDLSWCAApprovalDoc, lnkAddDLSWCAApprovalDoc, lnkDelDLSWCAApprovalDoc, lnkDLSWCAApprovalDocView, lblDLSWCAApprovalDoc, fupDLSWCAApprovalDocUpload, "BriefDetailsProposed");
        }
        else if (string.Equals(lnk.ID, lnkDelConsetSecUpload.ID))
        {
            UpdFileRemove(hdnConsetSecUpload, lnkAddConsetSecUpload, lnkDelConsetSecUpload, lknViewConsetSecUpload, lblConsetSecUpload, fupldConsetSecUpload, "BriefDetailsProposed");
        }
        else if (string.Equals(lnk.ID, lnkDelDLSWCAApprovalDoc.ID))
        {
            UpdFileRemove(hdnDLSWCAApprovalDoc, lnkAddDLSWCAApprovalDoc, lnkDelDLSWCAApprovalDoc, lnkDLSWCAApprovalDocView, lblDLSWCAApprovalDoc, fupDLSWCAApprovalDocUpload, "DLSWCA");
        }
        else if (string.Equals(lnk.ID, lnkDelDLSWCASubstanDoc.ID))
        {
            UpdFileRemove(hdnDLSWCASubstanDoc, lnkAddDLSWCASubstanDoc, lnkDelDLSWCASubstanDoc, lnkDLSWCASubstanDocView, lblDLSWCASubstanDoc, fupDLSWCASubstanDocUpload, "DLSWCA");
        }
        else if (string.Equals(lnk.ID, lnkBankDelete.ID))
        {
            string strFolderName = "Bank";
            UpdFileRemove(hdnBank, lnkBankUpload, lnkBankDelete, hypBank, lblBank, fuBank, strFolderName);
        }
    }
    protected void lnkAddMorePropCommon_Click(object sender, EventArgs e)
    {
        try
        {
            AddPropCommon();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally { }
    }
    private void AddPropCommon()
    {
        List<ProposedCommonFacility> listPropCmnFacl = new List<ProposedCommonFacility>();
        onjPropCmmnFacl = new ProposedCommonFacility();
        try
        {

            if (ViewState["CurrentListItemProp"] != null)
            {

                if (strUpdateStatusBrief.Value == "2")
                {
                    DataTable dt = new DataTable();
                    if (ViewState["CurrentListItemProp"] != null)
                    {
                        dt = (DataTable)ViewState["CurrentListItemProp"];
                    }
                    if (dt.Rows.Count > 0)
                    {

                        listPropCmnFacl = dt.AsEnumerable().Select(m => new ProposedCommonFacility()
                        {
                            intPropComSecTnt = m.Field<int>("intPropComSecTnt"),
                            vchPropCommonFacility = m.Field<string>("vchPropCommonFacility"),
                            intSlNo = m.Field<int>("intSlNo")


                        }).ToList();
                    }
                }
                else
                {

                    listPropCmnFacl = (List<ProposedCommonFacility>)ViewState["CurrentListItemProp"];
                }


                onjPropCmmnFacl.intPropComSecTnt = 0;
                onjPropCmmnFacl.vchPropCommonFacility = txtProposedCommonFacl.Text;

                onjPropCmmnFacl.intSlNo = listPropCmnFacl.Count + 1;
                if (hdnPropCmnFacl.Value == "Add More" || hdnPropCmnFacl.Value == "Add")
                {

                    if (listPropCmnFacl.Any(c => c.vchPropCommonFacility == txtProposedCommonFacl.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Click", "$('#" + txtProposedCommonFacl.ClientID + "').focus(); jAlert('<strong>Duplicate Proposed Facility are not allowed !</strong>', '" + projname + "');$('#" + txtProposedCommonFacl.ClientID + "').scrollView();", true);
                        return;
                    }
                    else
                    {

                        listPropCmnFacl.Add(onjPropCmmnFacl);
                        txtProposedCommonFacl.Text = "";

                    }

                }

                ViewState["CurrentListItemProp"] = listPropCmnFacl;
                grdPropCmmnFacl.DataSource = listPropCmnFacl;
                grdPropCmmnFacl.DataBind();
                strUpdateStatusBrief.Value = "1";


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Click", "Sorry ! You can not add  details.", true);
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
            listPropCmnFacl = null;
            onjPropCmmnFacl = null;
        }

        finally { listPropCmnFacl = null; onjPropCmmnFacl = null; }
    }
    protected void grdPropCmmnFacl_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["CurrentListItemProp"] != null)
        {
            txtProposedCommonFacl.Text = "";
            List<ProposedCommonFacility> listPropCmnFacl_del = new List<ProposedCommonFacility>();
            try
            {

                if (strUpdateStatusBrief.Value == "2")
                {
                    DataTable dt = new DataTable();
                    if (ViewState["CurrentListItemProp"] != null)
                    {
                        dt = (DataTable)ViewState["CurrentListItemProp"];
                    }

                    if (dt.Rows.Count > 0)
                    {
                        listPropCmnFacl_del = dt.AsEnumerable().Select(m => new ProposedCommonFacility()
                        {
                            intPropComSecTnt = m.Field<int>("intPropComSecTnt"),
                            intSlNo = m.Field<int>("intSlNo"),
                            vchPropCommonFacility = m.Field<string>("vchPropCommonFacility")

                        }).ToList();
                    }
                }
                else
                {

                    listPropCmnFacl_del = (List<ProposedCommonFacility>)ViewState["CurrentListItemProp"];
                }


                listPropCmnFacl_del.RemoveAt(e.RowIndex);
                ViewState["CurrentListItemProp"] = listPropCmnFacl_del;
                grdPropCmmnFacl.DataSource = listPropCmnFacl_del;
                grdPropCmmnFacl.DataBind();
                strUpdateStatusBrief.Value = "1";
                //ancAddMore.InnerHtml = "<i class='glyphicon glyphicon-plus'></i>Add More";
                if (grdPropCmmnFacl.Rows.Count > 0)
                {
                    //btnSubmit.Enabled = true;
                    hdnPropCmnFacl.Value = "Add More";
                    grdPropCmmnFacl.Focus();
                }
                else
                {
                    // btnSubmit.Enabled = false;
                    hdnPropCmnFacl.Value = "Add";
                    grdPropCmmnFacl.Focus();
                }

            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Incentive");
                listPropCmnFacl_del = null;
            }

            finally { listPropCmnFacl_del = null; }
        }
    }
    private void SetInitialRowItineraryProp()
    {
        listPropCmnFacl = new List<ProposedCommonFacility>();
        try
        {
            ViewState["CurrentListItemProp"] = listPropCmnFacl;
            grdPropCmmnFacl.DataSource = listPropCmnFacl;
            grdPropCmmnFacl.DataBind();
            if (grdPropCmmnFacl.Rows.Count > 0)
            {
                hdnPropCmnFacl.Value = "Add More";
            }
            else
            {

                hdnPropCmnFacl.Value = "Add";
            }



        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally { listItm = null; }
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
}