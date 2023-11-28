/*============================================================================
 *File Name :PatentRegistration.aspx
 *Class Name : incentives_PatentRegistration
 *Created On : 
 *Created By:
 *[Modification History]
 *[Cr no.]  [Modified On]       [Modified By]       [Description]
 *  1       16th January 2017   Ritika Lath         As per the defect log submitted after testing on 12th January 2017
 ============================================================================*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using DataAcessLayer.Common;
using EntityLayer.Incentive;

public partial class incentives_PatentRegistration : SessionCheck
{
    #region Variables

    List<ItemDetails> listItm = new List<ItemDetails>();
    ItemDetails objItem = null;
    string fillenameRegdNo = string.Empty;
    string fillenammeExpend = string.Empty;
    Incentive objEntity = new Incentive();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        txtCommercialDt.Attributes.Add("readonly", "readonly");
        txtRegistrationdt.Attributes.Add("readonly", "readonly");
        txtPatLoanAvl.Attributes.Add("readonly", "readonly");
        txtsacdat.Attributes.Add("readonly", "readonly");
        if (!IsPostBack)
        {
            fillSalutation();
            finanncedt();
            GetHeaderName();

            #region Avail Details
            crdtincentive();
            #endregion
            #region Patent Registration
            SetInitialRowItinerary();
            FillPatentCategory();
            #endregion
            #region Fill for update
            if (Convert.ToString(Session["ApplySource"]) == "0")
            {
                PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            }
            else
            {
                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
            }
            #endregion

        }
    }

    #region viewdetail

    public void PrepopulateData(int id)
    {
        try
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
            #region IndustrailUnit

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
            #endregion


            #region Production
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
            #endregion


            #region Investment
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
            #endregion

            #region MEANS OF FINANCE
            if (dtMeansFinancePre.Rows.Count > 0)
            {
                //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
                lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
                lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
                Hyp_View_Term_Loan_Doc.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString();
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
            #endregion
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }


    }
    public void FileUploadControls()
    {
        try
        {
            List<lstFileUpload> fileList = new List<lstFileUpload>();
            //industry
            if (radApplyBy.SelectedValue == "2")
            {
                if (hdnAUTHORIZEDFILE.Value != "")
                {
                    fileList.Add(new lstFileUpload()
                    {
                        id = 1,
                        vchDocId = hidAuthorizing.Value,
                        vchFileName = hdnAUTHORIZEDFILE.Value,
                        vchFilePath = "../incentives/Files/InctBasicDoc/"
                    });
                }
            }

            //Avail Details
            if (RadBtn_Availed_Earlier.SelectedValue == "1")
            {
                if (Hid_Asst_Sanc_File_Name.Value != "")
                {
                    fileList.Add(new lstFileUpload()
                    {
                        id = 1,
                        vchDocId = "D253",
                        vchFileName = Hid_Asst_Sanc_File_Name.Value,
                        vchFilePath = "../incentives/Files/AvailDetails/" + Session["investorid"].ToString() + "/"
                    });
                }
            }
            else
            {
                if (Hid_Undertaking_File_Name.Value != "")
                {
                    fileList.Add(new lstFileUpload()
                    {
                        id = 1,
                        vchDocId = "D230",
                        vchFileName = Hid_Undertaking_File_Name.Value,
                        vchFilePath = "../incentives/Files/AvailDetails/" + Session["investorid"].ToString() + "/"
                    });
                }
            }
            objEntity.FileUploadDetails = fileList;
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }

    public void PostpopulateData(int id)
    {
        try
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
            
            #region IndustrailUnit
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
                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                //dtindustryPre.Rows[0]["vchCertOfRegdDocCode"].ToString();		


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
            #endregion

            #region Production
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
            #endregion

            #region Investment

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
            #endregion

            #region MEANS OF FINANCE
            if (dtMeansFinancePre.Rows.Count > 0)
            {
                //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
                lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
                lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
                Hyp_View_Term_Loan_Doc.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();
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
            #endregion

            #region mypanel
            DataSet dsPanel = new DataSet();
            dsPanel = IncentiveManager.patentView(id);
            DataTable dtproductunit = dsPanel.Tables[1];
            DataTable patentdetail = dsPanel.Tables[2];
            DataTable dtpatentitem = dsPanel.Tables[3];
            DataTable dtpatentmin = dsPanel.Tables[4];
            DataTable dtaveldetail = dsPanel.Tables[5];
            DataTable dtavelitem = dsPanel.Tables[6];
            #region unit
            if (dtproductunit.Rows.Count > 0)
            {
                DdlGender.SelectedValue = dtproductunit.Rows[0]["INTGENDER"].ToString();
                TxtApplicantName.Text = dtproductunit.Rows[0]["VCHAPPLICANTNAME"].ToString();

                if (dtproductunit.Rows[0]["intApplyby"].ToString() != "0")
                {
                    radApplyBy.SelectedValue = dtproductunit.Rows[0]["intApplyby"].ToString();
                }
                else
                {
                    radApplyBy.SelectedIndex = -1;
                }
                if (dtproductunit.Rows[0]["vchAAdhaarno"].ToString().Count() == 12)
                {
                    TxtAdhaar1.Text = dtproductunit.Rows[0]["vchAAdhaarno"].ToString();
                }
                if (dtproductunit.Rows[0]["vchauthorizedfilename"].ToString() != "")
                {
                    FlupAUTHORIZEDFILE.Enabled = false;
                    lnkAUTHORIZEDFILE.Enabled = false;
                    lnkAUTHORIZEDFILEDdelete.Visible = true;
                    hypAUTHORIZEDFILE.Visible = true;
                    hdnAUTHORIZEDFILE.Value = dtproductunit.Rows[0]["vchauthorizedfilename"].ToString();
                    hypAUTHORIZEDFILE.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtproductunit.Rows[0]["vchauthorizedfilename"].ToString();
                }
            }
            #endregion
            #region patent
            if (patentdetail.Rows.Count > 0)
            {
                //txtagencyName.Text = patentdetail.Rows[0]["vchAgencyName"].ToString();
                //txtagencyAddress.Text = patentdetail.Rows[0]["vchAgencyAddress"].ToString();

                if (dtpatentitem.Rows.Count > 0)
                {
                    List<ItemDetails> listItem = new List<ItemDetails>();
                    listItem = (List<ItemDetails>)ViewState["CurrentListItem"];
                    for (int i = 0; dtpatentitem.Rows.Count > i; i++)
                    {
                        objItem = new ItemDetails();
                        try
                        {

                            objItem.vchAgencyName = dtpatentitem.Rows[i]["vchAgencyName"].ToString();
                            objItem.vchAgencyAddress = dtpatentitem.Rows[i]["vchAgencyAddress"].ToString();

                            objItem.IntCatgoryid = dtpatentitem.Rows[i]["IntCatgoryid"].ToString();
                            objItem.VchCatgoryName = dtpatentitem.Rows[i]["VchCatgoryName"].ToString();
                            objItem.IntSubCatgoryid = dtpatentitem.Rows[i]["IntSubCatgoryid"].ToString();
                            objItem.VchSubCatgoryName = dtpatentitem.Rows[i]["VchSubCatgoryName"].ToString();
                            //objItem.vchAuthorityNm = txtAuthorityName.Text;
                            objItem.dtCommercialDt = dtpatentitem.Rows[i]["dtCommercialDt"].ToString();
                            objItem.vchIPRRegistrationNo = dtpatentitem.Rows[i]["vchIPRRegistrationNo"].ToString();
                            objItem.dtRegistrationDate = dtpatentitem.Rows[i]["dtRegistrationDate"].ToString();
                            objItem.decExpenditureincurred = Convert.ToDecimal(dtpatentitem.Rows[i]["decExpenditureincurred"].ToString());
                            objItem.vchIPRRegistrationFile = dtpatentitem.Rows[i]["vchIPRRegistrationFile"].ToString();
                            objItem.vchExpenditureFile = dtpatentitem.Rows[i]["vchExpenditureFile"].ToString();
                            objItem.slno = listItem.Count + 1;
                            listItem.Add(objItem);

                        }
                        catch { }
                        finally { objItem = null; }
                    }
                    ViewState["CurrentListItem"] = listItem;
                    grvItmDetail.DataSource = listItem;
                    grvItmDetail.DataBind();


                }
                if (dtpatentmin.Rows.Count > 0)
                {
                    ViewState["finanncedt"] = dtpatentmin;
                    grdMeansOfFinancePatent.DataSource = dtpatentmin;
                    grdMeansOfFinancePatent.DataBind();
                }
            }
            #endregion
            #region Availed
            if (dtaveldetail.Rows.Count > 0)
            {
                if (dtaveldetail.Rows[0]["intNeverAvailedPrior"].ToString() == "1")
                {
                    RadBtn_Availed_Earlier.SelectedIndex = 0;
                }
                else
                {
                    RadBtn_Availed_Earlier.SelectedIndex = 1;
                }

                txtdiffclaimamt.Text = dtaveldetail.Rows[0]["decClaimExempted"].ToString();
                txtreimamt.Text = dtaveldetail.Rows[0]["decClaimReimbursement"].ToString();  //fuSupportingDocs
                if (dtaveldetail.Rows[0]["vchSupportingDocs"].ToString() != "")
                {
                    FU_Asst_Sanc_Doc.Enabled = false;
                    LnkBtn_Upload_Asst_Sanc_Doc.Enabled = false;
                    LnkBtn_Delete_Asst_Sanc_Doc.Visible = true;
                    Hyp_View_Asst_Sanc_Doc.NavigateUrl = "~/incentives/Files/AvailDetails/" + dtaveldetail.Rows[0]["vchSupportingDocs"].ToString();
                    Hyp_View_Asst_Sanc_Doc.Visible = true;
                    Hid_Asst_Sanc_File_Name.Value = dtaveldetail.Rows[0]["vchSupportingDocs"].ToString();
                }
                if (dtaveldetail.Rows[0]["vchUndertakingSubsidyDoc"].ToString() != "")
                {
                    FU_Undertaking_Doc.Enabled = false;
                    LnkBtn_Upload_Undertaking_Doc.Enabled = false;
                    LnkBtn_Delete_Undertaking_Doc.Visible = true;
                    Hyp_View_Undertaking_Doc.NavigateUrl = "~/incentives/Files/AvailDetails/" + dtaveldetail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                    Hyp_View_Undertaking_Doc.Visible = true;
                    Hid_Undertaking_File_Name.Value = dtaveldetail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                }
                if (dtavelitem.Rows.Count > 0)
                {
                    grdAssistanceDetailsAD.DataSource = dtavelitem;
                    grdAssistanceDetailsAD.DataBind();
                    ViewState["dtincentive"] = dtavelitem;
                }
            }
            #endregion
            #endregion
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }
    #endregion

    protected void btnDraft_Click(object sender, EventArgs e)
    {
        string retval = Add();
        if (retval.Split('~')[0].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !</strong>', 'SWP'); </script>", false);
        }
    }
    public string Add()
    {
        string ret_Val = string.Empty;
        try
        {
            AvailDetails objAvailDetails = new AvailDetails();
            List<Assistance> listIncentiveAvailed = new List<Assistance>();
            Assistance objIncentiveAvailed = new Assistance();
            INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();
            //objEntity.ProdEmpDet = objProd;


            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;

            objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);

            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;

            objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);

            objINDUSTRIAL_UNIT_MASTER.AADHAARNO_IND = TxtAdhaar1.Text;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILECODE_IND = hidAuthorizing.Value;
            objEntity.IndsutUnitMstDet = objINDUSTRIAL_UNIT_MASTER;

            #region Availed detail

            objAvailDetails.NeverAvailedPrior = Convert.ToInt16(RadBtn_Availed_Earlier.SelectedValue.ToString());
            objAvailDetails.SubsidyAvailed = 0;

            if (RadBtn_Availed_Earlier.SelectedValue == "1")
            {
                if (txtdiffclaimamt.Text.Trim() != "")
                {
                    objAvailDetails.ClaimtExempted = Convert.ToDouble(txtdiffclaimamt.Text.Trim());
                }
                else
                {
                    objAvailDetails.ClaimtExempted = 0;
                }

                objAvailDetails.SanctionOrderDoc = Hid_Asst_Sanc_File_Name.Value;
            }
            else
            {
                objAvailDetails.UndertakingSubsidyDoc = Hid_Undertaking_File_Name.Value;
            }

            if (txtreimamt.Text.Trim() != "")
            {
                objAvailDetails.ClaimReimbursement = Convert.ToDouble(txtreimamt.Text.Trim());
            }
            else
            {
                objAvailDetails.ClaimReimbursement = 0;
            }

            DataTable dtincentive = new DataTable();
            dtincentive = (DataTable)ViewState["dtincentive"];

            if (dtincentive.Rows.Count > 0)
            {
                foreach (DataRow dr in dtincentive.Rows)
                {
                    objIncentiveAvailed = new Assistance();
                    objIncentiveAvailed.InstitutionName = dr["vchagency"].ToString();
                    if (dr["vchavilamt"].ToString().Trim() != "")
                    {
                        objIncentiveAvailed.AmountAvailed = Convert.ToDouble(dr["vchavilamt"].ToString());
                    }
                    else
                    {
                        objIncentiveAvailed.AmountAvailed = 0;
                    }
                    if (dr["vchsacamt"].ToString().Trim() != "")
                    {
                        objIncentiveAvailed.SanctionedAmount = Convert.ToDouble(dr["vchsacamt"].ToString());
                    }
                    else
                    {
                        objIncentiveAvailed.SanctionedAmount = 0;
                    }
                    objIncentiveAvailed.AvailedDate = Convert.ToDateTime(dr["vchsacdat"].ToString());
                    objIncentiveAvailed.SanctionOrderNo = dr["vchsacord"].ToString();
                    listIncentiveAvailed.Add(objIncentiveAvailed);
                }
            }
            objAvailDetails.IncentiveAvailed = listIncentiveAvailed;
            objEntity.AvailDet = objAvailDetails;


            #endregion

            //Patent Details
            PatentsDetails();

            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objEntity.UnqIncentiveId = 0;
            }
            else
            {
                objEntity.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }
            objEntity.strcActioncode = "A";
            objEntity.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objEntity.PealNum = Convert.ToString(Session["ProposalNo"]);
            objEntity.PCNum = Convert.ToString(Session["PCNo"]);
            objEntity.UnitCode = Convert.ToString(Session["UnitCode"]);
            objEntity.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objEntity.Userid = Convert.ToInt16(Session["InvestorId"]);
            objEntity.Createdby = Convert.ToInt16(Session["InvestorId"]);
            objEntity.incentivetype = 4;
            objEntity.FormType = FormNumber.PatentRegistration_04;

            FileUploadControls();
            //call BAL
            ret_Val = IncentiveManager.CreateIncentivePatent(objEntity);


        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }

        finally
        {
            objEntity = null;
        }
        return ret_Val;
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {

        string retval = Add();
        int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
        /////------------------------------------------------------------------------------------------------
        Response.Redirect("PatentRegistrationPreview.aspx?InctUniqueNo=" + Convert.ToString(mstyp), false);
    }

    #region Industry

    public string ReturnDateFormat(string srcDate)
    {
        string resdt = "1900/01/01";
        try
        {
            if (srcDate != "")
            {
                string[] strarr = srcDate.Split('/');
                resdt = strarr[2] + "/" + strarr[0] + "/" + strarr[1];
            }
        }
        catch (Exception)
        {
        }

        return resdt;
    }

    #endregion

    #region Member Function

    protected void ClearItemDetail()
    {
        try
        {
            txtagencyName.Text = string.Empty;
            txtagencyAddress.Text = string.Empty;
            ddlPatCategory.SelectedValue = "0";
            ddlPatSubCategory.Items.Clear();
            ddlPatSubCategory.Items.Add(new ListItem("-Select-", "0"));
            ddlPatSubCategory.SelectedValue = "0";
            txtCommercialDt.Text = string.Empty;
            txtregistrNo.Text = string.Empty;
            txtRegistrationdt.Text = string.Empty;
            txtExpendincurr.Text = string.Empty;

        }
        catch { }
        finally { }

    }
    public void FillPatentCategory()
    {
        DataTable table = new DataTable();
        try
        {
            ddlPatCategory.Items.Clear();
            CommonDataLayer objDataUnit = new CommonDataLayer();
            ddlPatCategory.DataTextField = "VCH_CATEGORY_NAME";
            ddlPatCategory.DataValueField = "INT_CATEGORY_ID";
            ddlPatCategory.DataSource = objDataUnit.FillPatentCategory();
            ddlPatCategory.DataBind();
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally
        {
            table = null;
            ddlPatCategory.Items.Insert(0, new ListItem("-Select-", "0"));
        }
    }
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

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally
        {
            table = null;
        }
    }
    protected void ddlPatCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable table = new DataTable();
        try
        {
            ddlPatSubCategory.Items.Clear();
            CommonDataLayer objDataUnit = new CommonDataLayer();
            ddlPatSubCategory.DataTextField = "VCH_SUB_CAT_NAME";
            ddlPatSubCategory.DataValueField = "INT_SUB_CATEGORY_ID";
            ddlPatSubCategory.DataSource = objDataUnit.FillPatentSubCategory(Convert.ToInt32(ddlPatCategory.SelectedValue));
            ddlPatSubCategory.DataBind();
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally
        {
            ddlPatSubCategory.Items.Insert(0, new ListItem("-Select-", "0"));
            table = null;
        }
    }
    //Initial Grid Bind
    private void SetInitialRowItinerary()
    {
        listItm = new List<ItemDetails>();
        try
        {
            ViewState["CurrentListItem"] = listItm;
            grvItmDetail.DataSource = listItm;
            grvItmDetail.DataBind();
            if (grvItmDetail.Rows.Count > 0)
            {

                //lnkAddMore.Text = "Add More";
                //grvItmDetail.Focus();
                hdnAddMore.Value = "Add More";
            }
            else
            {

                //lnkAddMore.Text = "Add";
                //grvItmDetail.Focus();
                hdnAddMore.Value = "Add";
            }



        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { listItm = null; }
    }


    // Add Item Details In Grid View
    /// <summary>
    /// Function to add Patented Items in gridview and viewstate
    /// </summary>
    private void AddItemDetails()
    {
        List<ItemDetails> listItem = new List<ItemDetails>();
        objItem = new ItemDetails();
        try
        {
            string path = Server.MapPath("../incentives/Files/PatentDetail/");
            if (ViewState["CurrentListItem"] != null)
            {
                listItem = (List<ItemDetails>)ViewState["CurrentListItem"];
                objItem.vchAgencyName = txtagencyName.Text.Trim();
                objItem.vchAgencyAddress = txtagencyAddress.Text.Trim();
                objItem.IntCatgoryid = ddlPatCategory.SelectedValue;
                objItem.VchCatgoryName = ddlPatCategory.SelectedItem.Text;
                objItem.IntSubCatgoryid = ddlPatSubCategory.SelectedValue;
                objItem.VchSubCatgoryName = ddlPatSubCategory.SelectedItem.Text;
                objItem.dtCommercialDt = txtCommercialDt.Text;
                objItem.vchIPRRegistrationNo = txtregistrNo.Text;
                objItem.dtRegistrationDate = txtRegistrationdt.Text;
                objItem.decExpenditureincurred = Convert.ToDecimal(txtExpendincurr.Text);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (FLPRegistertnUpload.HasFile)
                {
                    string fillename = FLPRegistertnUpload.FileName;
                    if (Path.GetExtension(fillename).ToUpper() == ".PDF")
                    {
                        fillenameRegdNo = "Regd_" + DateTime.Now.ToString("ddMMMyyyyHHmmssfff.") + Path.GetExtension(FLPRegistertnUpload.PostedFile.FileName).Substring(1);
                        FLPRegistertnUpload.SaveAs(Server.MapPath("~/incentives/Files/PatentDetail/") + fillenameRegdNo);

                    }
                    else
                    {
                        Response.Write("<script>alart('Upload only pdf file')</script>");
                    }
                }
                objItem.vchIPRRegistrationFile = fillenameRegdNo;
                if (FLPExpenditureUpload.HasFile)
                {
                    fillenammeExpend = "Ex_" + DateTime.Now.ToString("ddMMMyyyyHHmmssfff.") + Path.GetExtension(FLPExpenditureUpload.PostedFile.FileName).Substring(1);
                    FLPExpenditureUpload.SaveAs(Server.MapPath("~/incentives/Files/PatentDetail/") + fillenammeExpend);

                }
                objItem.vchExpenditureFile = fillenammeExpend;

                //objItem.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                objItem.slno = listItem.Count + 1;
                if (hdnAddMore.Value == "Add More" || hdnAddMore.Value == "Add")
                {
                    listItem.Add(objItem);
                    ClearItemDetail();
                }

                ViewState["CurrentListItem"] = listItem;
                grvItmDetail.DataSource = listItem;
                grvItmDetail.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Click", "Sorry ! You can not add  details.", true);
            }

        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { listItem = null; objItem = null; }
    }

    public DataTable GetTablePatentLoanData() // this might be your sp for select
    {
        DataTable table = new DataTable();
        try
        {
            //table.Columns.Add("INT_LOANDTL_ID", typeof(string));
            table.Columns.Add("VCH_NAME_OF_FINANCIAL_INST", typeof(string));
            table.Columns.Add("DEC_AVAILED_AMT", typeof(string));
            table.Columns.Add("DTM_AVAILED_DATE", typeof(string));
            table.Columns.Add("VCH_LOAN_NO", typeof(string));




            //for (int i = 0; i < grdMeansOfFinancePatent.Rows.Count; i++)
            //{
            //    //HiddenField hdnLoanDetailid = (HiddenField)grdMeansOfFinance.Rows[i].FindControl("hdnLoanDetailid");
            //    Label lblPatFinancialName = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatFinancialName");

            //    Label lblPatAvailedAmt = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatAvailedAmt");
            //    Label lblPatAvailedDate = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatAvailedDate");
            //    Label lblPatLoanNo = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatLoanNo");
            //    table.Rows.Add(lblPatFinancialName.Text, lblPatAvailedAmt.Text, lblPatAvailedDate.Text, lblPatLoanNo.Text);
            //}
            table.Rows.Add(txtPatFinancialinst.Text, txtPatavailedLoan.Text, txtPatLoanAvl.Text, txtPatLoanNo.Text);

        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally
        {
            table = null;
        }
        return table;

    }
    public void PatentsDetails()
    {
        List<ItemDetails> listitemF = new List<ItemDetails>();
        List<PatentLoanDetails> objPatentLoanlist = new List<PatentLoanDetails>();

        try
        {
            PatentDetails objPatent = new PatentDetails();
            objPatent.AgencyName = txtagencyName.Text;
            objPatent.AgencyAddress = txtagencyAddress.Text;
            for (int i = 0; i < grvItmDetail.Rows.Count; i++)
            {
                ItemDetails objItem = new ItemDetails();
                try
                {


                    HiddenField hdnCategory = (HiddenField)grvItmDetail.Rows[i].FindControl("hdnCategory");
                    HiddenField hdnSubCategory = (HiddenField)grvItmDetail.Rows[i].FindControl("hdnSubCategory");

                    HiddenField hdnvchIPRRegistrationFile = (HiddenField)grvItmDetail.Rows[i].FindControl("hdnvchIPRRegistrationFile");
                    HiddenField hdnvchExpenditureFile = (HiddenField)grvItmDetail.Rows[i].FindControl("hdnvchExpenditureFile");
                    //objItem.vchItemName = grvItmDetail.Rows[i].Cells[1].Text.ToString();
                    objItem.IntCatgoryid = hdnCategory.Value;
                    objItem.IntSubCatgoryid = hdnSubCategory.Value;
                    objItem.vchAgencyName = grvItmDetail.Rows[i].Cells[1].Text.ToString();
                    objItem.vchAgencyAddress = grvItmDetail.Rows[i].Cells[2].Text.ToString();


                    objItem.dtCommercialDt = grvItmDetail.Rows[i].Cells[5].Text.ToString();
                    objItem.vchIPRRegistrationNo = grvItmDetail.Rows[i].Cells[6].Text.ToString();
                    objItem.vchIPRRegistrationFile = hdnvchIPRRegistrationFile.Value.ToString();
                    objItem.dtRegistrationDate = grvItmDetail.Rows[i].Cells[8].Text.ToString();
                    objItem.decExpenditureincurred = Convert.ToDecimal(grvItmDetail.Rows[i].Cells[9].Text);
                    objItem.vchExpenditureFile = hdnvchExpenditureFile.Value.ToString();

                }
                catch (Exception)
                {
                }
                listitemF.Add(objItem);
            }

            objPatent.lstitemsDetails = listitemF;


            //List for Means of Finance Loan Details for Patent
            for (int i = 0; i < grdMeansOfFinancePatent.Rows.Count; i++)
            {
                PatentLoanDetails objPatLoanItem = new PatentLoanDetails();
                try
                {
                    Label lblPatFinancialName = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatFinancialName");
                    Label lblPatAvailedAmt = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatAvailedAmt");
                    Label lblPatAvailedDate = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatAvailedDate");
                    Label lblPatLoanNo = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatLoanNo");

                    objPatLoanItem.FinancialInstitutionNm = lblPatFinancialName.Text;
                    objPatLoanItem.AmountAvailed = Convert.ToDecimal(lblPatAvailedAmt.Text);
                    objPatLoanItem.AmountAvailedDate = lblPatAvailedDate.Text;
                    objPatLoanItem.LoanNumber = lblPatLoanNo.Text;
                }
                catch (Exception)
                {
                }
                objPatentLoanlist.Add(objPatLoanItem);
            }
            objPatent.lstPatLoanDetails = objPatentLoanlist;
            objEntity.PatentDet = objPatent;
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }
    #endregion

    #region Patent Grid Events
    protected void lnkAddMorePatent_Click(object sender, EventArgs e)
    {
        try
        {
            AddItemDetails();
        }
        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }
    protected void grvItmDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["CurrentListItem"] != null)
        {
            ClearItemDetail();
            List<ItemDetails> listItem_del = new List<ItemDetails>();
            try
            {
                listItem_del = (List<ItemDetails>)ViewState["CurrentListItem"];

                string completePath = Server.MapPath("~/incentives/Files/PatentDetail/") + listItem_del[e.RowIndex].vchIPRRegistrationFile;
                string completeExpPath = Server.MapPath("~/incentives/Files/PatentDetail/") + listItem_del[e.RowIndex].vchExpenditureFile;
                if (System.IO.File.Exists(completePath))
                {

                    // System.IO.File.Delete(completePath);

                }


                if (System.IO.File.Exists(completeExpPath))
                {

                    //System.IO.File.Delete(completeExpPath);

                }


                listItem_del.RemoveAt(e.RowIndex);




                ViewState["CurrentListItem"] = listItem_del;
                grvItmDetail.DataSource = listItem_del;
                grvItmDetail.DataBind();
                //ancAddMore.InnerHtml = "<i class='glyphicon glyphicon-plus'></i>Add More";
                if (grvItmDetail.Rows.Count > 0)
                {
                    //btnSubmit.Enabled = true;
                    hdnAddMore.Value = "Add More";
                    grvItmDetail.Focus();
                }
                else
                {
                    // btnSubmit.Enabled = false;
                    hdnAddMore.Value = "Add";
                    grvItmDetail.Focus();
                }

            }

            catch (Exception ex) { Util.LogError(ex, "Incentive"); }
            finally { listItem_del = null; }
        }
    }
    protected void lnk_PatentLoan_click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtfinn = (DataTable)ViewState["finanncedt"];
            DataRow dr = dtfinn.NewRow();
            dr["VCH_NAME_OF_FINANCIAL_INST"] = txtPatFinancialinst.Text;
            dr["DEC_AVAILED_AMT"] = txtPatavailedLoan.Text;
            dr["DTM_AVAILED_DATE"] = txtPatLoanAvl.Text;
            dr["VCH_LOAN_NO"] = txtPatLoanNo.Text;
            dtfinn.Rows.Add(dr);
            ViewState["finanncedt"] = dtfinn;

            grdMeansOfFinancePatent.DataSource = dtfinn;
            grdMeansOfFinancePatent.DataBind();

            txtPatFinancialinst.Text = string.Empty;
            txtPatavailedLoan.Text = string.Empty;
            txtPatLoanAvl.Text = string.Empty;
            txtPatLoanNo.Text = string.Empty;
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }
    protected void ImageButtonDeletePatentmeans_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {

            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            //table.Columns.Add("INT_LOANDTL_ID", typeof(string));
            table.Columns.Add("VCH_NAME_OF_FINANCIAL_INST", typeof(string));
            table.Columns.Add("DEC_AVAILED_AMT", typeof(string));
            table.Columns.Add("DTM_AVAILED_DATE", typeof(string));
            table.Columns.Add("VCH_LOAN_NO", typeof(string));

            for (int i = 0; i < grdMeansOfFinancePatent.Rows.Count; i++)
            {
                if (i != RowID)
                {

                    Label lblPatFinancialName = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatFinancialName");
                    Label lblPatAvailedAmt = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatAvailedAmt");
                    Label lblPatAvailedDate = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatAvailedDate");
                    Label lblPatLoanNo = (Label)grdMeansOfFinancePatent.Rows[i].FindControl("lblPatLoanNo");
                    table.Rows.Add(lblPatFinancialName.Text, lblPatAvailedAmt.Text, lblPatAvailedDate.Text, lblPatLoanNo.Text);
                }
            }

            ViewState["finanncedt"] = table;
            grdMeansOfFinancePatent.DataSource = table;
            grdMeansOfFinancePatent.DataBind();

        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally
        {
            table = null;
        }
    }
    #endregion

    #region Avail Details
    protected void LinkButton41_Click(object sender, EventArgs e)
    {
        DataTable dtincentive = new DataTable();
        dtincentive = (DataTable)ViewState["dtincentive"];
        DataRow dr = dtincentive.NewRow();
        dr["vchagency"] = txtagency.Text.Trim();
        dr["vchsacamt"] = txtsacamt.Text.Trim();
        dr["vchsacord"] = txtsacord.Text.Trim();
        dr["vchsacdat"] = txtsacdat.Text.Trim();
        dr["vchavilamt"] = txtavilamt.Text.Trim();
        dtincentive.Rows.Add(dr);
        ViewState["dtincentive"] = dtincentive;

        grdAssistanceDetailsAD.DataSource = dtincentive;
        grdAssistanceDetailsAD.DataBind();

        txtagency.Text = "";
        txtsacamt.Text = "";
        txtsacord.Text = "";
        txtsacdat.Text = "";
        txtavilamt.Text = "";
    }
    protected void grdAssistanceDetailsAD_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdfanew = (HiddenField)grdAssistanceDetailsAD.Rows[e.RowIndex].Cells[5].FindControl("hdnRowId");
            DataTable dtnew0 = new DataTable();
            dtnew0 = (DataTable)ViewState["dtincentive"];
            DataRow[] dr1 = null;
            dr1 = dtnew0.Select("dcRowId='" + hdfanew.Value + "'");
            for (int i = 0; i < dr1.Length; i++)
            {

                dr1[i].Delete();
            }
            dtnew0.AcceptChanges();
            grdAssistanceDetailsAD.DataSource = dtnew0;
            grdAssistanceDetailsAD.DataBind();
            ViewState["dtincentive"] = dtnew0;
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }
    void crdtincentive()
    {
        try
        {
            DataTable dtincentive = new DataTable();


            DataColumn dcRowId = new DataColumn("dcRowId");
            dcRowId.DataType = Type.GetType("System.Int32");
            dcRowId.AutoIncrement = true;
            dcRowId.AutoIncrementSeed = 1;
            dcRowId.AutoIncrementStep = 1;
            dtincentive.Columns.Add(dcRowId);

            DataColumn vchagency = new DataColumn("vchagency");
            vchagency.DataType = Type.GetType("System.String");
            dtincentive.Columns.Add(vchagency);

            DataColumn vchsacamt = new DataColumn("vchsacamt");
            vchsacamt.DataType = Type.GetType("System.String");
            dtincentive.Columns.Add(vchsacamt);

            DataColumn vchsacord = new DataColumn("vchsacord");
            vchsacord.DataType = Type.GetType("System.String");
            dtincentive.Columns.Add(vchsacord);

            DataColumn vchsacdat = new DataColumn("vchsacdat");
            vchsacdat.DataType = Type.GetType("System.String");
            dtincentive.Columns.Add(vchsacdat);

            DataColumn vchavilamt = new DataColumn("vchavilamt");
            vchavilamt.DataType = Type.GetType("System.String");
            dtincentive.Columns.Add(vchavilamt);

            ViewState["dtincentive"] = dtincentive;
            grdAssistanceDetailsAD.DataSource = dtincentive;
            grdAssistanceDetailsAD.DataBind();
        }
        catch (Exception)
        {
        }
    }
    #endregion
    #region Common Evetns & Method
    protected void lnkDocumentDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_Asst_Sanc_Doc.ID))
        {
            string strFolderName = "AvailDetails";
            UpdFileRemove(Hid_Asst_Sanc_File_Name, LnkBtn_Upload_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, strFolderName);
        }
        else if (string.Equals(lnk.ID, LnkBtn_Delete_Undertaking_Doc.ID))
        {
            string strFolderName = "AvailDetails";
            UpdFileRemove(Hid_Undertaking_File_Name, LnkBtn_Upload_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, FU_Undertaking_Doc, strFolderName);
        }
        else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILEDdelete.ID))
        {
            string strFolderName = "InctBasicDoc";
            UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, strFolderName);

        }
    }
    protected void lnkDocumentUpload_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        if (string.Equals(lnk.ID, LnkBtn_Upload_Asst_Sanc_Doc.ID))
        {
            if (FU_Asst_Sanc_Doc.HasFile)
            {
                string strFileName = "ASSTSANC" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "AvailDetails";
                UploadDocument(FU_Asst_Sanc_Doc, Hid_Asst_Sanc_File_Name, strFileName, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, strFolderName);
            }
            else
            {
                Response.Redirect("<script></script>");
            }
        }
        else if (string.Equals(lnk.ID, LnkBtn_Upload_Undertaking_Doc.ID))
        {
            if (FU_Undertaking_Doc.HasFile)
            {
                string strFileName = "UND" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "AvailDetails";
                UploadDocument(FU_Undertaking_Doc, Hid_Undertaking_File_Name, strFileName, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, strFolderName);
            }
            else
            {
                Response.Redirect("<script></script>");
            }
        }
        else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILE.ID))
        {
            if (FlupAUTHORIZEDFILE.HasFile)
            {
                string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                string strFolderName = "InctBasicDoc";
                UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, strFolderName);
            }
        }
    }
    private void UploadDocument(FileUpload fuDocument, HiddenField hdnDocument, string strFileName, HyperLink hypDocument, Label lblDocument, LinkButton lnkDocumentDelete, string strFolderName)
    {
        string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFolderName));
        if (!Directory.Exists(strMainFolderPath))
        {
            Directory.CreateDirectory(strMainFolderPath);
        }
        if (fuDocument.HasFile)
        {


            if (IsFileValid(fuDocument) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','SWP');", true);
                return;
            }
            string filename = string.Empty;
            if ((Path.GetExtension(fuDocument.FileName) != ".pdf") && (Path.GetExtension(fuDocument.FileName) != ".zip"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload  PDF,ZIP file Only!','SWP')", true);
                return;
            }
            int fileSize = fuDocument.PostedFile.ContentLength;
            if (fileSize > (5 * 1024 * 1024))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 5 MB','SWP')", true);
                return;
            }
            else
            {
                filename = strFileName + Path.GetExtension(fuDocument.FileName);
            }
            fuDocument.SaveAs(strMainFolderPath + filename);
            hdnDocument.Value = filename;
            hypDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
            hypDocument.Visible = true;
            lnkDocumentDelete.Visible = true;
            lblDocument.Visible = true;
            fuDocument.Enabled = false;
        }
    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string strFolderName)
    {
        try
        {
            string filename = hdnFile.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
            string completePath = Server.MapPath(path);

            //File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            lnkBtn.Enabled = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
        catch (Exception)
        {
        }

    }
    public void GetHeaderName()
    {
        try
        {
            Incentive objIncentive = new Incentive();
            objIncentive.strcActioncode = "M";
            objIncentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveMaster(objIncentive);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtbPostSubFlag = ds.Tables[0];
                lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
            }
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }
    private void finanncedt()
    {
        ViewState["finanncedt"] = null;

        DataTable finanncedt = new DataTable();

        DataColumn dcRowId = new DataColumn("dcRowId");
        dcRowId.DataType = Type.GetType("System.Int32");
        dcRowId.AutoIncrement = true;
        dcRowId.AutoIncrementSeed = 1;
        dcRowId.AutoIncrementStep = 1;
        finanncedt.Columns.Add(dcRowId);

        DataColumn VCH_NAME_OF_FINANCIAL_INST = new DataColumn("VCH_NAME_OF_FINANCIAL_INST");
        VCH_NAME_OF_FINANCIAL_INST.DataType = Type.GetType("System.String");
        finanncedt.Columns.Add(VCH_NAME_OF_FINANCIAL_INST);

        DataColumn DEC_AVAILED_AMT = new DataColumn("DEC_AVAILED_AMT");
        DEC_AVAILED_AMT.DataType = Type.GetType("System.String");
        finanncedt.Columns.Add(DEC_AVAILED_AMT);

        DataColumn DTM_AVAILED_DATE = new DataColumn("DTM_AVAILED_DATE");
        DTM_AVAILED_DATE.DataType = Type.GetType("System.String");
        finanncedt.Columns.Add(DTM_AVAILED_DATE);

        DataColumn VCH_LOAN_NO = new DataColumn("VCH_LOAN_NO");
        VCH_LOAN_NO.DataType = Type.GetType("System.String");
        finanncedt.Columns.Add(VCH_LOAN_NO);




        ViewState["finanncedt"] = finanncedt;


    }
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
    #endregion

}