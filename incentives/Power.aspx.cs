using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.IO;
using System.Data;
using DataAcessLayer.Common;
using DataAcessLayer.Incentive;
using System.Linq;
using System.Collections.Specialized;
using BusinessLogicLayer.Incentive;

public partial class incentives_EmploymentRating : SessionCheck
{
    DataTable dtFiles;
    string MsgTitle = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillSalutation();
            GetHeaderName();
            BindFYearLabel();
            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
                PrepopulateDataPlus(Convert.ToInt16(Session["InvestorId"]));
            }
            else
            {
                int tryint;
                PostpopulateData(int.TryParse(Request.QueryString["InctUniqueNo"], out tryint) ? tryint : tryint);
                fillPage(int.TryParse(Request.QueryString["InctUniqueNo"], out tryint) ? tryint : tryint);
            }




        }
    }

    #region Populate Common-Field Pre/Post

    #region viewdetail
    public void PrepopulateData(int id)
    {
        try
        {
            Grd_TL.DataSource = null;
            Grd_TL.DataBind();
            Grd_WC.DataSource = null;
            Grd_WC.DataBind();
            Grd_Production_After.DataSource = null;
            Grd_Production_After.DataBind();
            Grd_Production_Before.DataSource = null;
            Grd_Production_Before.DataBind();
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
                DivVisibilty(Lbl_Pioneer_Doc_Name, div_Pioneer_Doc_Name);



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
            #endregion


            BindDocFromService(enServiceDocType.OSPCB, D275, flValidStatutary, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, hdnIsOsPCBDownloaded);
            BindDocFromService(enServiceDocType.Boiler, D280, flCleanApproveAuthority, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, hdnBoilerDownloaded);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }


    }
    public void PostpopulateData(int id)
    {
        Grd_TL.DataSource = null;
        Grd_TL.DataBind();
        Grd_WC.DataSource = null;
        Grd_WC.DataBind();
        Grd_Production_After.DataSource = null;
        Grd_Production_After.DataBind();
        Grd_Production_Before.DataSource = null;
        Grd_Production_Before.DataBind();

        DataSet ds1 = new DataSet();
        DataTable dt = new DataTable();
        DataSet dslivePre = IncentiveManager.PostpopulateData(id);
        DataTable dtindustryPost = dslivePre.Tables[0];////////////industry panel
        DataTable dtProductionpost = dslivePre.Tables[1];///////////production & employment

        DataTable dtProductionDetBefPost = dslivePre.Tables[2];///////////production & employment Before
        DataTable dtProductionDetAftPost = dslivePre.Tables[3];///////////production & employment After
        DataTable dtInvestmentPost = dslivePre.Tables[4];///////////investment details
        DataTable dtMeansFinancePost = dslivePre.Tables[5];///////////Means of Finance
        DataTable dtMoFTermLoanPost = dslivePre.Tables[6];///////////Means of Finance Term Loan
        DataTable dtMoFWorkingLoanPost = dslivePre.Tables[7];///////////Means of Finance Working Loan

        //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();

        #region IndustrialUnit
        try
        {
            if (dtindustryPost.Rows.Count > 0)
            {
                lbl_EnterPrise_Name.Text = dtindustryPost.Rows[0]["vchEnterpriseName"].ToString();
                //dtindustryPost.Rows[0]["intOrganisationType"].ToString();	
                ds1 = IncentiveManager.dynamic_name_doc_bind();
                ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPost.Rows[0]["intUnitType"].ToString() + "'";
                ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPost.Rows[0]["intOrganisationType"].ToString() + "'";
                dt = (ds1.Tables[0].DefaultView).ToTable();
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



                lbl_Org_Type.Text = dtindustryPost.Rows[0]["OrgTypename"].ToString();
                lbl_Industry_Address.Text = dtindustryPost.Rows[0]["vchIndustryAddress"].ToString();
                //dtindustryPost.Rows[0]["intUnitCat"].ToString();		
                lbl_Unit_Cat.Text = dtindustryPost.Rows[0]["Unitcategoryname"].ToString();
                Lbl_Pioneer_Doc_Name.Text = dtindustryPost.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();
                DivVisibilty(Lbl_Pioneer_Doc_Name, div_Pioneer_Doc_Name);



                dt = (ds1.Tables[1].DefaultView).ToTable();
                if (dt.Rows.Count > 0)
                {
                    string strDocType = dt.Rows[0]["vchDocumentType"].ToString();
                    if (strDocType != "")
                    {
                        Div_Unit_Type_Doc.Visible = true;
                        Lbl_Unit_Type_Doc_Name.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                        Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();

                        Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPost.Rows[0]["vchUnitTypeDoc"].ToString();
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




                //dtindustryPost.Rows[0]["intUnitType"].ToString();			
                lbl_Unit_Type.Text = dtindustryPost.Rows[0]["UnitTypename"].ToString();
                //dtindustryPost.Rows[0]["vchDocCode"].ToString();	

                //dtindustryPost.Rows[0]["vchUnitTypeDoc"].ToString();

                if (dtindustryPost.Rows[0]["intPriority"].ToString() == "1")
                {
                    lblIs_Priority.Text = "Yes";
                    Pioneersec.Visible = true;

                }
                else
                {
                    lblIs_Priority.Text = "No";
                    Pioneersec.Visible = false;

                }
                if (dtindustryPost.Rows[0]["intPioneer"].ToString() == "1")
                {
                    lblIs_Is_Pioneer.Text = "Yes";

                }
                else
                {
                    lblIs_Is_Pioneer.Text = "No";

                }


                Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPost.Rows[0]["vchPioneerCertificate"].ToString();



                lbl_Regd_Office_Address.Text = dtindustryPost.Rows[0]["vchRegisteredOfcAddress"].ToString();
                //dtindustryPost.Rows[0]["vchManagingPartnerGender"].ToString();	
                lbl_Gender_Partner.Text = dtindustryPost.Rows[0]["GenderType"].ToString() + " " + dtindustryPost.Rows[0]["vchManagingPartnerName"].ToString();

                DdlGender.SelectedValue = dtindustryPost.Rows[0]["vchManagingPartnerGender"].ToString();
                TxtApplicantName.Text = dtindustryPost.Rows[0]["vchManagingPartnerName"].ToString();
                //dtindustryPost.Rows[0]["vchManagingPartnerName"].ToString();
                //Lbl_Org_Doc_Type.Text = dtindustryPost.Rows[0]["CertOfRegdDocName"].ToString();
                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPost.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                //dtindustryPost.Rows[0]["vchCertOfRegdDocCode"].ToString();		


                //dtindustryPost.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                lbl_EIN_IL_NO.Text = dtindustryPost.Rows[0]["vchEINNO"].ToString();
                lbl_EIN_IL_Date.Text = dtindustryPost.Rows[0]["dtmEIN"].ToString();
                lbl_PC_No.Text = dtindustryPost.Rows[0]["vchPcNo"].ToString();
                lbl_Prod_Comm_Date_Before.Text = dtindustryPost.Rows[0]["dtmProdCommBefore"].ToString();
                lbl_PC_Issue_Date_Before.Text = dtindustryPost.Rows[0]["dtmPCIssueDateBefore"].ToString();
                //dtindustryPost.Rows[0]["vchProdCommCertBeforeCode"].ToString();	
                //Lbl_Prod_Comm_Before_Doc_Name.Text = dtindustryPost.Rows[0]["ProdCommCertBeforeDocName"].ToString();
                //Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPost.Rows[0]["vchProdCommCertBefore"].ToString();
                //dtindustryPost.Rows[0]["vchProdCommCertBefore"].ToString();


                lbl_pcno_befor.Text = dtindustryPost.Rows[0]["vchpcnobefore"].ToString();
                lblGstin.Text = dtindustryPost.Rows[0]["VCHGSTIN"].ToString();

                if (dtindustryPost.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPost.Rows[0]["vchappnobef"].ToString();
                }
                else if (dtindustryPost.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPost.Rows[0]["vchappnobef"].ToString();
                }



                if (dtindustryPost.Rows[0]["dtmProdCommBefore"].ToString() != "")
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

                lbl_Prod_Comm_Date_After.Text = dtindustryPost.Rows[0]["dtmProdCommAfter"].ToString();
                lbl_PC_Issue_Date_After.Text = dtindustryPost.Rows[0]["dtmPCIssueDateAfter"].ToString();

                //dtindustryPost.Rows[0]["vchProdCommCertAfterCode"].ToString();		
                //Lbl_Prod_Comm_After_Doc_Name.Text = dtindustryPost.Rows[0]["ProdCommCertAfterDocName"].ToString();
                //Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPost.Rows[0]["vchProdCommCertAfter"].ToString();



                if (dtindustryPost.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPost.Rows[0]["vchappnoAft"].ToString();
                }
                else if (dtindustryPost.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPost.Rows[0]["vchappnoAft"].ToString();
                }

                ////dtindustryPost.Rows[0]["intDistrictCode"].ToString();			
                lbl_District.Text = dtindustryPost.Rows[0]["distname"].ToString();
                //dtindustryPost.Rows[0]["intSectorId"].ToString();			
                lbl_Sector.Text = dtindustryPost.Rows[0]["sectorName"].ToString();
                //dtindustryPost.Rows[0]["intSubSectorId"].ToString();			
                lbl_Sub_Sector.Text = dtindustryPost.Rows[0]["SubsectorName"].ToString();
                Lbl_Derived_Sector.Text = dtindustryPost.Rows[0]["vchDerivedSector"].ToString();
                //dtindustryPost.Rows[0]["bitSectoralPolicy"].ToString();

                if (dtindustryPost.Rows[0]["bitPriorityIPR"].ToString() == "1")
                {

                    lbl_Sectoral.Text = "Yes";
                }
                else
                {
                    lbl_Sectoral.Text = "No";
                }
                //dtindustryPost.Rows[0]["intCreatedBy"].ToString();


                /////////---------------------------------individual part------------


                ////DdlGender.SelectedValue = dtindustryPost.Rows[0]["INTGENDER"].ToString();
                ////TxtApplicantName.Text = dtindustryPost.Rows[0]["VCHAPPLICANTNAME"].ToString();
                ////if (dtindustryPost.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                ////{
                ////    TxtAdhaar1.Text = dtindustryPost.Rows[0]["VCHAADHAARNO"].ToString().Substring(0, 4);
                ////    TxtAdhaar2.Text = dtindustryPost.Rows[0]["VCHAADHAARNO"].ToString().Substring(4, 4);
                ////    TxtAdhaar3.Text = dtindustryPost.Rows[0]["VCHAADHAARNO"].ToString().Substring(8, 4);
                ////}
                ////hdnAUTHORIZEDFILE.Value = dtindustryPost.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(); /////AUTHORIZEDFILE file upload

                ///////----------------------

                ////FileVisibilty(hdnAUTHORIZEDFILE, HypViewAUTHORIZEDFILE, LnkDelAUTHORIZEDFILE, FlupAUTHORIZEDFILE, dtindustryPost.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc", lnkUpAUTHORIZEDFILE);


                ///////----------------------
                ////if (dtindustryPost.Rows[0]["INTAPPLYBY"].ToString() == "1")
                ////{
                ////    radApplyBy.SelectedValue = dtindustryPost.Rows[0]["INTAPPLYBY"].ToString();
                ////}
                ////else if (dtindustryPost.Rows[0]["INTAPPLYBY"].ToString() == "2")
                ////{
                ////    radApplyBy.SelectedValue = dtindustryPost.Rows[0]["INTAPPLYBY"].ToString();
                ////}
                ////else
                ////{
                ////    radApplyBy.SelectedIndex = -1;
                ////}

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        #endregion

        #region Production
        try
        {
            if (dtProductionpost.Rows.Count > 0)
            {
                Grd_Production_Before.DataSource = dtProductionDetBefPost;
                Grd_Production_Before.DataBind();
                Grd_Production_After.DataSource = dtProductionDetAftPost;
                Grd_Production_After.DataBind();


                //dtProductionpost.Rows[0]["intProductionId"].ToString();
                lbl_Direct_Emp_Before.Text = dtProductionpost.Rows[0]["intDirectEmpBefore"].ToString();
                lbl_Contract_Emp_Before.Text = dtProductionpost.Rows[0]["intContractualEmpBefore"].ToString();
                Hyp_View_Direct_Emp_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionpost.Rows[0]["VCHEMPDOC"].ToString();
                lbl_Managarial_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDMANAGERIAL"].ToString();
                lbl_Supervisor_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDSUPERVISORY"].ToString();
                lbl_Skilled_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDSKILLED"].ToString();
                lbl_Semi_Skilled_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDSEMISKILLED"].ToString();
                lbl_Unskilled_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDUNSKILLED"].ToString();
                lbl_Total_Emp_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDTOTAL"].ToString();
                lbl_General_Before.Text = dtProductionpost.Rows[0]["intGeneralBefore"].ToString();
                lbl_SC_Before.Text = dtProductionpost.Rows[0]["intSCBefore"].ToString();
                lbl_ST_Before.Text = dtProductionpost.Rows[0]["intSTBefore"].ToString();
                lbl_Total_Cast_Emp_Before.Text = dtProductionpost.Rows[0]["intTotalEmpCastBefore"].ToString();
                lbl_Women_Before.Text = dtProductionpost.Rows[0]["intWomenBefore"].ToString();
                lbl_PHD_Before.Text = dtProductionpost.Rows[0]["intDisabledBefore"].ToString();
                lbl_Direct_Emp_After.Text = dtProductionpost.Rows[0]["intDirectEmpAfter"].ToString();
                lbl_Contract_Emp_After.Text = dtProductionpost.Rows[0]["intContractualEmpAfter"].ToString();
                //dtProductionpost.Rows[0]["vchEmpDocBeforeCode"].ToString();			
                Lbl_Direct_Emp_Before_Doc_Name.Text = dtProductionpost.Rows[0]["vchEmpDocBeforeCodeName"].ToString();
                Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionpost.Rows[0]["vchEmpDocAfter"].ToString();
                //dtProductionpost.Rows[0]["vchEmpDocAfterCode"].ToString();			
                Lbl_Direct_Emp_After_Doc_Name.Text = dtProductionpost.Rows[0]["vchEmpDocAfterCodeName"].ToString();


                lbl_Managarial_After.Text = dtProductionpost.Rows[0]["INTCURRENTMANAGERIAL"].ToString();
                lbl_Supervisor_After.Text = dtProductionpost.Rows[0]["INTCURRENTSUPERVISORY"].ToString();
                lbl_Skilled_After.Text = dtProductionpost.Rows[0]["INTCURRENTSKILLED"].ToString();
                lbl_Semi_Skilled_After.Text = dtProductionpost.Rows[0]["INTCURRENTSEMISKILLED"].ToString();
                lbl_Unskilled_After.Text = dtProductionpost.Rows[0]["INTCURRENTUNSKILLED"].ToString();
                lbl_Total_Emp_After.Text = dtProductionpost.Rows[0]["INTCURRENTTOTAL"].ToString();
                lbl_General_After.Text = dtProductionpost.Rows[0]["intGeneralAfter"].ToString();
                lbl_SC_After.Text = dtProductionpost.Rows[0]["intSCAfter"].ToString();
                lbl_ST_After.Text = dtProductionpost.Rows[0]["intSTAfter"].ToString();
                lbl_Total_Cast_Emp_After.Text = dtProductionpost.Rows[0]["intTotalEmpCastAfter"].ToString();
                lbl_Women_After.Text = dtProductionpost.Rows[0]["intWomenAfter"].ToString();
                lbl_PHD_After.Text = dtProductionpost.Rows[0]["intDisabledAfter"].ToString();

                //dtProductionpost.Rows[0]["intCreatedBy"].ToString();
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        #endregion

        #region Investment
        try
        {


            //dtInvestmentPost.Rows[0]["slno"].ToString();
            if (dtInvestmentPost.Rows.Count > 0)
            {
                Txt_FFCI_Date_Before.Text = dtInvestmentPost.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString();
                //dtInvestmentPost.Rows[0]["vchFFCIDocBeforeCode"].ToString();
                Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPost.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();

                Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPost.Rows[0]["VCH_Document_in_support"].ToString();


                lbl_Land_Before.Text = dtInvestmentPost.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
                lbl_Building_Before.Text = dtInvestmentPost.Rows[0]["DEC_Building"].ToString();
                lbl_Plant_Mach_Before.Text = dtInvestmentPost.Rows[0]["DEC_Plant_Machinery"].ToString();
                lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPost.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
                lbl_Total_Capital_Before.Text = dtInvestmentPost.Rows[0]["DEC_Total"].ToString();
                //dtInvestmentPost.Rows[0]["vchProjectDocBeforeCode"].ToString();			
                Lbl_Approved_DPR_Before_Doc_Name.Text = dtInvestmentPost.Rows[0]["vchProjectDocBeforeCodeName"].ToString();
                Hyp_View_Approved_DPR_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPost.Rows[0]["vchProjectDocBefore"].ToString();
                lbl_FFCI_Date_After.Text = dtInvestmentPost.Rows[0]["dtmFFCIDateAfter"].ToString();
                //dtInvestmentPost.Rows[0]["vchFFCIDocAfterCode"].ToString();			
                Lbl_FFCI_After_Doc_Name.Text = dtInvestmentPost.Rows[0]["vchFFCIDocAfterCodeName"].ToString();
                Hyp_View_FFCI_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPost.Rows[0]["vchFFCIDocAfter"].ToString();

                lbl_Land_After.Text = dtInvestmentPost.Rows[0]["decLandAmtAfter"].ToString();
                lbl_Building_After.Text = dtInvestmentPost.Rows[0]["decBuildingAmtAfter"].ToString();
                lbl_Plant_Mach_After.Text = dtInvestmentPost.Rows[0]["decPlantMachAmtAfter"].ToString();
                lbl_Other_Fixed_Asset_After.Text = dtInvestmentPost.Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
                lbl_Total_Capital_After.Text = dtInvestmentPost.Rows[0]["decTotalAmtAfter"].ToString();


                //dtInvestmentPost.Rows[0]["vchProjectDocAfterCode"].ToString();			
                Lbl_Approved_DPR_After_Doc_Name.Text = dtInvestmentPost.Rows[0]["vchProjectDocAfterCodeName"].ToString();
                Hyp_View_Approved_DPR_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPost.Rows[0]["vchProjectDocAfter"].ToString();
                //dtInvestmentPost.Rows[0]["intCreatedBy"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        #endregion

        #region MEANS OF FINANCE
        try
        {
            if (dtMeansFinancePost.Rows.Count > 0)
            {
                //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
                lbl_Equity_Amt.Text = dtMeansFinancePost.Rows[0]["decEquity"].ToString();
                lbl_Loan_Bank_FI.Text = dtMeansFinancePost.Rows[0]["decLoanBankFI"].ToString();
                Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePost.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();
                lbl_FDI_Componet.Text = dtMeansFinancePost.Rows[0]["decFDIComponet"].ToString();
                //dtMeansFinancePre.Rows[0]["intCreatedBy"].ToString();
                //dtMeansFinancePre.Rows[0]["vchTermLoanDocCode"].ToString();
                Lbl_Term_Loan_Doc_Name.Text = dtMeansFinancePost.Rows[0]["vchTermLoanDocCodeNAme"].ToString();



                if (dtMoFTermLoanPost.Rows.Count > 0)
                {
                    Grd_TL.DataSource = dtMoFTermLoanPost;
                    Grd_TL.DataBind();
                }

                if (dtMoFWorkingLoanPost.Rows.Count > 0)
                {
                    Grd_WC.DataSource = dtMoFWorkingLoanPost;
                    Grd_WC.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        #endregion


    }
    public void fillPage(int id)
    {
        try
        {
            Incentive incentive = new Incentive();
            incentive.UnqIncentiveId = id;// int.TryParse(Request.QueryString["InctUniqueNo"], out tryint) ? tryint : tryint;


            DataSet ds = IncentiveManager.GetIncentivePower(incentive);

            DataTable dtIndustrial = ds.Tables[0];
            DataTable dtPower = ds.Tables[1];
            DataTable dtBank = ds.Tables[2];
            DataTable dtADDDocument = ds.Tables[3];
            DataTable dtApplyStatusOrFinalSaveStatus = ds.Tables[5];



            if (Convert.ToString(Session["ApplySource"]) == "1")
            {

            }
            else
            {
                if (dtIndustrial.Rows.Count > 0)
                {
                    FillIndustryFields(dtIndustrial, "0");
                }

                if (dtPower.Rows.Count > 0)
                {
                    fillPowerModuleControls(dtPower);
                }
                if (dtBank.Rows.Count > 0)
                {
                    fillBankModuleControls(dtBank);
                }
                if (dtADDDocument.Rows.Count > 0)
                {
                    fillADDDocModuleControls(dtADDDocument);
                }
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }

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
        try
        {
            txtAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
            txtBnkNm.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
            txtBranch.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
            txtIFSC.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
            txtMICRNo.Text = dtBank.Rows[0]["VCHMICR"].ToString();
            if (dtBank.Rows[0]["vchBankDoc"].ToString() != "")
            {
                //hdnBank.Value = dtBank.Rows[0]["vchBankDoc"].ToString(); /////AUTHORIZEDFILE file upload
                //hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBank.Rows[0]["vchBankDoc"].ToString();
                //hypBank.Visible = true;
                //lnkBankDelete.Visible = true;
                ////lblOrgDocument.Visible = true;
                //fuBank.Enabled = false;


                FileVisibilty(hdnBank, hypBank, lnkBankDelete, fuBank, dtBank.Rows[0]["vchBankDoc"].ToString(), "Bank", lnkBankUpload);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void DivVisibilty(Label Lbl, System.Web.UI.HtmlControls.HtmlGenericControl divvsb)
    {
        if (Lbl.Text.Trim() == "")
            divvsb.Visible = false;
        else
            divvsb.Visible = true;

    }
    #endregion

    #endregion


    #region Button Click
    protected void btnApply_Click(object sender, EventArgs e)
    {
        int tryint;
        try
        {
            Incentive incentive = new Incentive();


            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                incentive.UnqIncentiveId = 0;
            }
            else
            {
                incentive.UnqIncentiveId = int.TryParse(Request.QueryString["InctUniqueNo"], out tryint) ? tryint : tryint;
            }
            incentive.strcActioncode = "A";
            incentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            incentive.PealNum = Convert.ToString(Session["ProposalNo"]);
            incentive.PCNum = Convert.ToString(Session["PCNo"]);
            incentive.UnitCode = Convert.ToString(Session["UnitCode"]);
            incentive.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            incentive.Userid = Convert.ToInt16(Session["InvestorId"]);
            incentive.Createdby = Convert.ToInt16(Session["InvestorId"]);
            incentive.FYear = Convert.ToInt16(Convert.ToString(Session["FyYear"]).Trim() == "" ? RetFyear().ToString() : Convert.ToString(Session["FyYear"]).Trim());// Convert.ToInt16(Convert.ToString(Session["FyYear"]));
            incentive.incentivetype = 4;
            incentive.FormType = FormNumber.PowerTarrif_15;

            dtFiles = createFileListTable();
            setIndustrialUnitDetails(incentive);
            SetPowerTariffSection(incentive);
            SetBankDetails(incentive);
            SetAdditionalDocument(incentive);
            SetDocMasterDetails(incentive);

            string retval = IncentiveManager.CreateIncentivePowerTariff(incentive).ToString();
            int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
            /////------------------------------------------------------------------------------------------------
            Response.Redirect("PPower.aspx?InctUniqueNo=" + Convert.ToString(mstyp));

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        int tryint;
        try
        {
            Incentive incentive = new Incentive();

            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                incentive.UnqIncentiveId = 0;
            }
            else
            {
                incentive.UnqIncentiveId = int.TryParse(Request.QueryString["InctUniqueNo"], out tryint) ? tryint : tryint;
            }

            incentive.strcActioncode = "A";
            incentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            incentive.PealNum = Convert.ToString(Session["ProposalNo"]);
            incentive.PCNum = Convert.ToString(Session["PCNo"]);
            incentive.UnitCode = Convert.ToString(Session["UnitCode"]);
            incentive.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            incentive.Userid = Convert.ToInt16(Session["InvestorId"]);
            incentive.Createdby = Convert.ToInt16(Session["InvestorId"]);
            incentive.FYear = Convert.ToInt16(Convert.ToString(Session["FyYear"]).Trim() == "" ? RetFyear().ToString() : Convert.ToString(Session["FyYear"]).Trim());// Convert.ToInt16(Convert.ToString(Session["FyYear"]));
            incentive.incentivetype = 4;
            incentive.FormType = FormNumber.PowerTarrif_15;

            dtFiles = createFileListTable();
            setIndustrialUnitDetails(incentive);
            SetPowerTariffSection(incentive);
            SetBankDetails(incentive);
            SetAdditionalDocument(incentive);
            SetDocMasterDetails(incentive);

            string retval = IncentiveManager.CreateIncentivePowerTariff(incentive).ToString();


            if (retval.Split('~')[0].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !!</strong>', '" + MsgTitle + "'); </script>", false);
                PostpopulateData(Convert.ToInt16(retval.Split('~')[1].ToString()));
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion

    #region fill-Edit Module_Controls
    public void FillIndustryFields(DataTable dtindustry, string DraftStatus)
    {
        try
        {
            if (dtindustry.Rows.Count > 0)
            {
                TxtApplicantName.Text = dtindustry.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();
                if (dtindustry.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                {
                    TxtAdhaar1.Text = dtindustry.Rows[0]["VCHAADHAARNO"].ToString();
                }
                DdlGender.SelectedValue = dtindustry.Rows[0]["INTGENDER"].ToString();
                if (dtindustry.Rows[0]["INTAPPLYBY"].ToString() != "0")
                {
                    radApplyBy.SelectedValue = dtindustry.Rows[0]["INTAPPLYBY"].ToString();
                }
                else
                {
                    radApplyBy.SelectedIndex = -1;
                }
                hdnAUTHORIZEDFILE.Value = dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                if (dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString() != "")
                {
                    string Path = "~/incentives/Files/InctBasicDoc";
                    string filename = dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    FileViewProcess(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, lnkUpAUTHORIZEDFILE, LnkDelAUTHORIZEDFILE, HypViewAUTHORIZEDFILE, LblAUTHORIZEDFILE, Path, filename);
                    // radAuthorizing.SelectedValue = dtindustry.Rows[0]["VCHAUTHORIZEDFILECODE"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void fillPowerModuleControls(DataTable dtPower)
    {
        try
        {
            TxtNewSP.Text = dtPower.Rows[0]["decNewInvestment_SchematicProvisions"].ToString();//NewInvestment_SchematicProvisions
            TxtNewCP.Text = dtPower.Rows[0]["decNewInvestment_TillDateOfCommencementOfProduction"].ToString(); //NewInvestment_TillDateOfCommencementOfProduction
            TxtNewJR.Text = dtPower.Rows[0]["vchNewInvestment_reasons"].ToString();//NewInvestment_reasons


            TxtLandSP.Text = dtPower.Rows[0]["decLand_SchematicProvisions"].ToString();//Land_SchematicProvisions
            TxtLandCP.Text = dtPower.Rows[0]["decLand_TillDateOfCommencementOfProduction"].ToString(); //Land_TillDateOfCommencementOfProduction
            TxtLandJR.Text = dtPower.Rows[0]["vchLand_reasons"].ToString();//Land_reasons

            TxtBuildSP.Text = dtPower.Rows[0]["decBuilding_SchematicProvisions"].ToString();//Building_SchematicProvisions
            TxtBuildCP.Text = dtPower.Rows[0]["decBuilding_TillDateOfCommencementOfProduction"].ToString();//Building_TillDateOfCommencementOfProduction
            TxtBuildJR.Text = dtPower.Rows[0]["vchBuilding_reasons"].ToString();//Building_reasons


            TxtPMSP.Text = dtPower.Rows[0]["decPlantMachinery_SchematicProvisions"].ToString();//PlantMachinery_SchematicProvisions
            TxtPMCP.Text = dtPower.Rows[0]["decPlantMachinery_TillDateOfCommencementOfProduction"].ToString();//PlantMachinery_TillDateOfCommencementOfProduction
            TxtPMJR.Text = dtPower.Rows[0]["vchPlantMachinery_reasons"].ToString();//PlantMachinery_reasons


            TxtOFASP.Text = dtPower.Rows[0]["decOtherFixedAssets_SchematicProvisions"].ToString();//OtherFixedAssets_SchematicProvisions
            TxtOFACP.Text = dtPower.Rows[0]["decOtherFixedAssets_TillDateOfCommencementOfProduction"].ToString();//OtherFixedAssets_TillDateOfCommencementOfProduction
            TxtOFAJR.Text = dtPower.Rows[0]["vchOtherFixedAssets_reasons"].ToString();//OtherFixedAssets_reasons

            TxtElectSP.Text = dtPower.Rows[0]["decElectricalInstallations_SchematicProvisions"].ToString();//ElectricalInstallations_SchematicProvisions
            TxtElectCP.Text = dtPower.Rows[0]["decElectricalInstallations_TillDateOfCommencementOfProduction"].ToString();//ElectricalInstallations_TillDateOfCommencementOfProduction
            TxtElectJR.Text = dtPower.Rows[0]["vchElectricalInstallations_reasons"].ToString();//ElectricalInstallations_reasons


            TxtJustExc.Text = dtPower.Rows[0]["vchJustificationForExcessInvestment"].ToString();//JustificationForExcessInvestment

            TxtTotUnit.Text = dtPower.Rows[0]["vchTotalUnitConsumed"].ToString();//TotalUnitConsumed

            TxtTotAmtPaid.Text = dtPower.Rows[0]["decAmountPaid"].ToString();//AmountPaid


            if (dtPower.Rows[0]["vch_MoneyReceiptFile"].ToString() != "")
            {
                string Path = "~/incentives/Files/PowerTariff";
                string filename = dtPower.Rows[0]["vch_MoneyReceiptFile"].ToString();
                FileViewProcess(flMoneyreceipt, D131, lnkUMoneyreceipt, lnkDMoneyreceipt, hypMoneyreceipt, lblMoneyreceipt, Path, filename);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    private void fillBankModuleControls(DataTable dtBank)
    {
        try
        {
            txtBnkNm.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
            txtBranch.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
            txtIFSC.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
            txtAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
            txtMICRNo.Text = dtBank.Rows[0]["VCHMICR"].ToString();
            if (dtBank.Rows[0]["vchBankDoc"].ToString() != "")
            {
                FileVisibilty(hdnBank, hypBank, lnkBankDelete, fuBank, dtBank.Rows[0]["vchBankDoc"].ToString(), "Bank", lnkBankUpload);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void fillADDDocModuleControls(DataTable dtADDDocument)
    {
        try
        {
            D275.Value = dtADDDocument.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
            D274.Value = dtADDDocument.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
            D280.Value = dtADDDocument.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();

            if (dtADDDocument.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString() != "")
            {
                string Path = "~/incentives/Files/AdditionalDocument";
                string filename = dtADDDocument.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
                FileViewProcess(flValidStatutary, D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, Path, filename);
            }

            if (dtADDDocument.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString() != "")
            {
                string Path = "~/incentives/Files/AdditionalDocument";
                string filename = dtADDDocument.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
                FileViewProcess(flDelay, D274, lnkUDelay, lnkDDelay, hypDelay, lblDelay, Path, filename);
            }
            if (dtADDDocument.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString() != "")
            {
                string Path = "~/incentives/Files/AdditionalDocument";
                string filename = dtADDDocument.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();
                FileViewProcess(flCleanApproveAuthority, D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, Path, filename);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #endregion

    #region    SAVE Module_Parameters
    public void setIndustrialUnitDetails(Incentive incentive)
    {
        incentive.IndsutUnitMstDet = new INDUSTRIAL_UNIT_MASTER();

        incentive.IndsutUnitMstDet.APPLICANTNAME_IND = TxtApplicantName.Text;

        incentive.IndsutUnitMstDet.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);

        if (radApplyBy.SelectedIndex > -1)
        {
            incentive.IndsutUnitMstDet.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
        }
        incentive.IndsutUnitMstDet.AADHAARNO_IND = TxtAdhaar1.Text;
        incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;


        incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND = 0;// radAuthorizing.SelectedValue;

    }

    public void SetPowerTariffSection(Incentive incentive)
    {

        try
        {
            decimal tryDecimal;
            incentive.PowerTariffDet = new PowerTariff();
            incentive.PowerTariffDet.NewInvestment_SchematicProvisions = (decimal.TryParse(TxtNewSP.Text.Trim() == "" ? "0" : TxtNewSP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.NewInvestment_TillDateOfCommencementOfProduction = (decimal.TryParse(TxtNewCP.Text.Trim() == "" ? "0" : TxtNewCP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.NewInvestment_reasons = TxtNewJR.Text.Trim();


            incentive.PowerTariffDet.Land_SchematicProvisions = (decimal.TryParse(TxtLandSP.Text.Trim() == "" ? "0" : TxtLandSP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.Land_TillDateOfCommencementOfProduction = (decimal.TryParse(TxtLandCP.Text.Trim() == "" ? "0" : TxtLandCP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.Land_reasons = TxtLandJR.Text.Trim();

            incentive.PowerTariffDet.Building_SchematicProvisions = (decimal.TryParse(TxtBuildSP.Text.Trim() == "" ? "0" : TxtBuildSP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.Building_TillDateOfCommencementOfProduction = (decimal.TryParse(TxtBuildCP.Text.Trim() == "" ? "0" : TxtBuildCP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.Building_reasons = TxtBuildJR.Text.Trim();


            incentive.PowerTariffDet.PlantMachinery_SchematicProvisions = (decimal.TryParse(TxtPMSP.Text.Trim() == "" ? "0" : TxtPMSP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.PlantMachinery_TillDateOfCommencementOfProduction = (decimal.TryParse(TxtPMCP.Text.Trim() == "" ? "0" : TxtPMCP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.PlantMachinery_reasons = TxtPMJR.Text.Trim();


            incentive.PowerTariffDet.OtherFixedAssets_SchematicProvisions = (decimal.TryParse(TxtOFASP.Text.Trim() == "" ? "0" : TxtOFASP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.OtherFixedAssets_TillDateOfCommencementOfProduction = (decimal.TryParse(TxtOFACP.Text.Trim() == "" ? "0" : TxtOFACP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.OtherFixedAssets_reasons = TxtOFAJR.Text.Trim();

            incentive.PowerTariffDet.ElectricalInstallations_SchematicProvisions = (decimal.TryParse(TxtElectSP.Text.Trim() == "" ? "0" : TxtElectSP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.ElectricalInstallations_TillDateOfCommencementOfProduction = (decimal.TryParse(TxtElectCP.Text.Trim() == "" ? "0" : TxtElectCP.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);
            incentive.PowerTariffDet.ElectricalInstallations_reasons = TxtElectJR.Text.Trim();


            incentive.PowerTariffDet.JustificationForExcessInvestment = TxtJustExc.Text.Trim();

            incentive.PowerTariffDet.TotalUnitConsumed = TxtTotUnit.Text.Trim();

            incentive.PowerTariffDet.AmountPaid = (decimal.TryParse(TxtTotAmtPaid.Text.Trim() == "" ? "0" : TxtTotAmtPaid.Text.Trim(), out tryDecimal) ? tryDecimal : tryDecimal);

            //File Upload   function() return filename string

            incentive.PowerTariffDet.MoneyReceipt = D131.Value;
            addFileDataTableRow(ref dtFiles, D131.ID, D131.Value, "~/incentives/Files/PowerTariff", incentive.UnitCode);

        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }
    public void SetBankDetails(Incentive incentive)
    {
        try
        {
            incentive.BankDet = new EntityLayer.Incentive.BankDetails();
            incentive.BankDet.BankName = txtBnkNm.Text.Trim();
            incentive.BankDet.BranchName = txtBranch.Text.Trim();
            incentive.BankDet.IFSCNo = txtIFSC.Text.Trim();
            incentive.BankDet.AccountNo = txtAccNo.Text.Trim();
            incentive.BankDet.MICRNo = txtMICRNo.Text.Trim();
            if (hdnBank.Value != "")
            {
                incentive.BankDet.BankDoc = hdnBank.Value;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    public void SetAdditionalDocument(Incentive incentive)
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
            incentive.AdditionalDocument = new EntityLayer.Incentive.AdditionalDocuments();
            incentive.AdditionalDocument.strValidSatutoryGreenCategory = D275.Value;
            incentive.AdditionalDocument.strCondoDocumentationDelay = D274.Value;
            incentive.AdditionalDocument.strCleanApproveAuthorityOSPCB = D280.Value;


            addFileDataTableRow(ref dtFiles, D275.ID, D275.Value, "~/incentives/Files/AdditionalDocument", incentive.UnitCode);
            addFileDataTableRow(ref dtFiles, D274.ID, D274.Value, "~/incentives/Files/AdditionalDocument", incentive.UnitCode);
            addFileDataTableRow(ref dtFiles, D280.ID, D280.Value, "~/incentives/Files/AdditionalDocument", incentive.UnitCode);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    public void SetDocMasterDetails(Incentive incentive)
    {
        List<lstFileUpload> DocMasterDetails = new List<lstFileUpload>();

        try
        {
            foreach (DataRow dr in dtFiles.Rows)
            {
                lstFileUpload OneFileDetails = new lstFileUpload();
                OneFileDetails.vchDocId = dr["DocID"].ToString();
                OneFileDetails.vchFileName = dr["FileName"].ToString();
                OneFileDetails.vchFilePath = dr["Path"].ToString();
                //dr["UnitCode"].ToString();
                DocMasterDetails.Add(OneFileDetails);

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

        incentive.FileUploadDetails = DocMasterDetails;
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
    #region Common Method
    private void FillApplyBy()
    {

        try
        {
            /*
            IncentiveMasterDataLayer objinsentive = new IncentiveMasterDataLayer();
            DataSet dsdoc = new DataSet();
            dsdoc = objinsentive.BindDropdown("grp");
            radAuthorizing.DataSource = dsdoc;
            radAuthorizing.DataTextField = "vchDocName";
            radAuthorizing.DataValueField = "vchDocId";
            radAuthorizing.DataBind();
            */
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void UploadDocument(FileUpload fuDocument, HiddenField hdnDocument, string strFileName, HyperLink hypViewDocument, Label lblDocument, LinkButton lnkDocumentDelete, string strFolderName, LinkButton lnkBtnUpload = null)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFolderName));//, Session["investorid"]
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (fuDocument.HasFile)
            {
                string filename = string.Empty;


                if (!(IsFileValid(fuDocument)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid File Type. (The file name should not contain any dots.) ', '" + MsgTitle + "'); </script>", false);
                    return;
                }
                //// validation checked in client sciprt side
                ////if ((Path.GetExtension(fuDocument.FileName) != ".pdf") && (Path.GetExtension(fuDocument.FileName) != ".zip") && (Path.GetExtension(fuDocument.FileName) != ".xls") && (Path.GetExtension(fuDocument.FileName) != ".xls"))
                ////{
                ////    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,ZIP file Only!')", true);
                ////    return;
                ////}
                int fileSize = fuDocument.PostedFile.ContentLength;

                if (fileSize > (4 * 1024 * 1024))
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 2 MB')", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File size is too large. Maximum file size permitted is 4 MB !', '" + MsgTitle + "'); </script>", false);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(fuDocument.FileName);
                }

                fuDocument.SaveAs(strMainFolderPath + filename);
                hdnDocument.Value = filename;
                hypViewDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
                hypViewDocument.Visible = true;
                lnkDocumentDelete.Visible = true;
                lblDocument.Visible = true;
                fuDocument.Enabled = false;
                lnkBtnUpload.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtnUpload, LinkButton lnkDel, HyperLink hplnkView, Label lblFile, FileUpload updFile, string strFolderName)
    {
        try
        {
            string filename = hdnFile.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
            string completePath = Server.MapPath(path);
            if (File.Exists(completePath))
            {
                ////File.Delete(completePath);
            }
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtnUpload.Visible = true;
            hplnkView.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void FileVisibilty(HiddenField HdnFileName, HyperLink HypView, LinkButton LnkDeleteBtn, FileUpload FluCtrl, string FileName, string FolderName, LinkButton lnkBtnUpload = null)
    {
        try
        {
            if (FileName != "")
            {
                HdnFileName.Value = FileName;
                HypView.NavigateUrl = "../incentives/Files/" + FolderName + "/" + FileName;
                HypView.Visible = true;
                LnkDeleteBtn.Visible = true;
                //lblOrgDocument.Visible = true;
                FluCtrl.Enabled = false;
                lnkBtnUpload.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void BindFYearLabel()
    {
        try
        {
            string fyear = Convert.ToString(Session["FyYear"]).Trim() == "" ? DateTime.Now.Year.ToString() : Convert.ToString(Session["FyYear"]).Trim();
            if (Session["FyYear"] == null)
            {
                if (DateTime.Now.Month < 4)
                {
                    fyear = (int.Parse(fyear) - 1).ToString();
                }
            }
            int year = int.Parse(fyear);
            LblYear.Text = year.ToString() + "-" + (year + 1).ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private bool IsFileValid(FileUpload FileUpload1)
    {
        try
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
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
    #endregion
    public string RetFileNamePath(string filename)
    {
        string strret = "";
        try
        {
            if (filename != "")
            {
                strret = "../incentives/Files/IndustryUnit/" + filename;
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
    private int RetFyear()
    {
        int year = 0;
        int month = DateTime.Now.Month;
        if (month < 4)
        {
            year = (DateTime.Now.Year) - 1;
        }
        else
        {
            year = DateTime.Now.Year;
        }
        return year;
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
    #endregion


    #region File Upload/Delete Event Sections
    protected void lnkUMoneyreceipt_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/PowerTariff";
        UploadProcess(flMoneyreceipt, D131, lnkUMoneyreceipt, lnkDMoneyreceipt, hypMoneyreceipt, lblMoneyreceipt, Path, "Moneyreceipt");
    }
    protected void lnkDMoneyreceipt_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/PowerTariff";
        DeleteProcess(flMoneyreceipt, D131, lnkUMoneyreceipt, lnkDMoneyreceipt, hypMoneyreceipt, lblMoneyreceipt, Path, "Moneyreceipt");
    }

    protected void lnkUValidStatutary_click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        UploadProcess(flValidStatutary, D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, Path, "OSPCB");
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
        try
        {
            if (flCleanApproveAuthority.HasFile)
            {

                string Path = "~/incentives/Files/AdditionalDocument";
                UploadProcess(flCleanApproveAuthority, D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, Path, "Factory_Boiler");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    protected void lnkDCleanApproveAuthority_Click(object sender, EventArgs e)
    {
        try
        {
            string Path = "~/incentives/Files/AdditionalDocument";
            DeleteProcess(flCleanApproveAuthority, D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, Path, "STATUTORYCLEARANCE");
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }


    protected void LnkUpAUTHORIZEDFILE_Click(object sender, EventArgs e)
    {
        try
        {
            if (FlupAUTHORIZEDFILE.HasFile)
            {
                string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                string strFolderName = "InctBasicDoc";
                UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, HypViewAUTHORIZEDFILE, LblAUTHORIZEDFILE, LnkDelAUTHORIZEDFILE, strFolderName, lnkUpAUTHORIZEDFILE);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkDelAUTHORIZEDFILE_Click(object sender, EventArgs e)
    {
        try
        {
            string strFolderName = "InctBasicDoc";
            UpdFileRemove(hdnAUTHORIZEDFILE, lnkUpAUTHORIZEDFILE, LnkDelAUTHORIZEDFILE, HypViewAUTHORIZEDFILE, LblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }


    protected void lnkBankUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (fuBank.HasFile)
            {
                string strFileName = "Bank" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Bank";
                UploadDocument(fuBank, hdnBank, strFileName, hypBank, lblBank, lnkBankDelete, strFolderName, lnkBankUpload);
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void lnkBankDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string strFolderName = "Bank";
            UpdFileRemove(hdnBank, lnkBankUpload, lnkBankDelete, hypBank, lblBank, fuBank, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #region Common Functions
    public void DeleteProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string ModuleName)
    {
        try
        {
            string fileName = hdn.Value;


            //if (fileName != "")
            //    if (File.Exists(Server.MapPath(xPath + "/" + fileName)))
            // File.Delete(Server.MapPath(xPath + "/" + fileName));


            hdn.Value = "";
            hyp.NavigateUrl = "";


            F.Enabled = true;
            LU.Visible = true;

            LD.Visible = false;
            hyp.Visible = false;
            lblMsg.Visible = false;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void UploadProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string ModuleName)
    {
        string fileName = "";

        if (!(IsFileValid(F)))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots.', '" + MsgTitle + "'); </script>", false);
            return;
        }
        fileName = UploadX(F, xPath, ModuleName);

        if (fileName != "")
        {

            //if (File.Exists(Server.MapPath(xPath + "/" + fileName)))
            //{

            hdn.Value = fileName;//also in view time
            hyp.NavigateUrl = xPath + "/" + fileName;


            F.Enabled = false;
            LU.Visible = false;

            LD.Visible = true;
            hyp.Visible = true;
            lblMsg.Visible = true;//not in view time  (false)
            //}

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

                if (!(IsFileValid(fileControl)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots. ', '" + MsgTitle + "'); </script>", false);
                    return "";
                }
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

            FileName = "";
        }
        return FileName;
    }
    public void FileViewProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string FileName)
    {
        string fileName = FileName;


        if (fileName != "")
        {

            //if (File.Exists(Server.MapPath(xPath + "/" + fileName)))
            //{

            hdn.Value = fileName;//also in view time
            hyp.NavigateUrl = xPath + "/" + fileName;


            F.Enabled = false;
            LU.Visible = false;

            LD.Visible = true;
            hyp.Visible = true;
            lblMsg.Visible = false;
            //}

        }

    }
    public DataTable createFileListTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("DocID", typeof(string));
        dt.Columns.Add("FileName", typeof(string));
        dt.Columns.Add("Path", typeof(string));
        dt.Columns.Add("UnitCode", typeof(string));
        return dt;
    }
    public void addFileDataTableRow(ref DataTable dt, string docid, string fileName, string xPath, string unitcode)
    {
        if (fileName != "")
        {

            dt.Rows.Add(docid, fileName, xPath, unitcode);

        }
    }
    #endregion
    #endregion



}