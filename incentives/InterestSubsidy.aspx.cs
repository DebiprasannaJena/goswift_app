using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EntityLayer.Incentive;
using System.Data;
using System.Collections.Specialized;
using BusinessLogicLayer.Incentive;

public partial class incentives_InterestSubsidy : SessionCheck
{
    Incentive objEntity = new Incentive();
    string MsgTitle = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    protected void Page_Load(object sender, EventArgs e)
    {
        txtSactionData.Attributes.Add("readonly", "true");
        txtDisbursalDate.Attributes.Add("readonly", "true");
        txtLoanMaturityDate.Attributes.Add("readonly", "true");
        if (!IsPostBack)
        {
            fillSalutation();
            GetHeaderName();
            crdtincentive();
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
                Hyp_View_Term_Loan_Doc.NavigateUrl = "~/Incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString();
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
    public void PostpopulateData(int id)
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
            #endregion

            #region mypanel
            DataSet dsPanel = new DataSet();
            dsPanel = IncentiveManager.InetersSubsidyView(id);
            DataTable dtproductunit = dsPanel.Tables[1];
            DataTable dtTERMLOANDETAILS = dsPanel.Tables[2];
            DataTable dtPlannedDisbursal = dsPanel.Tables[3];
            DataTable dtRepaymentSchedule = dsPanel.Tables[4];
            DataTable dtPreviousSanction = dsPanel.Tables[5];
            #region unit
            if (dtproductunit.Rows.Count > 0)
            {
                TxtApplicantName.Text = dtproductunit.Rows[0]["VCHAPPLICANTNAME"].ToString();

                DdlGender.SelectedValue = dtproductunit.Rows[0]["INTGENDER"].ToString();
                if (dtproductunit.Rows[0]["intApplyby"].ToString() != "0")
                {
                    radApplyBy.SelectedValue = dtproductunit.Rows[0]["intApplyby"].ToString();
                }

                if (dtproductunit.Rows[0]["vchAAdhaarno"].ToString().Count() == 12)
                {
                    TxtAdhaar1.Text = dtproductunit.Rows[0]["vchAAdhaarno"].ToString();
                }
                if (dtproductunit.Rows[0]["intApplyby"].ToString() != "")
                {
                    //radAuthorizing.SelectedValue = dtproductunit.Rows[0]["intApplyby"].ToString();
                }
                if (dtproductunit.Rows[0]["vchauthorizedfilename"].ToString() != "")
                {
                    FlupAUTHORIZEDFILE.Enabled = false;
                    lnkAUTHORIZEDFILEDdelete.Visible = true;
                    hdnAUTHORIZEDFILE.Value = dtproductunit.Rows[0]["vchauthorizedfilename"].ToString();
                    hypAUTHORIZEDFILE.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtproductunit.Rows[0]["vchauthorizedfilename"].ToString();
                    hypAUTHORIZEDFILE.Visible = true;
                    lnkAUTHORIZEDFILE.Visible = false;
                }
            }
            #endregion

            #region TERMLOANDETAILS
            if (dtTERMLOANDETAILS.Rows.Count > 0)
            {
                txtReinursementAmount1.Text = dtTERMLOANDETAILS.Rows[0]["decReinursementAmount1"].ToString();
                txtReinursementAmount2.Text = dtTERMLOANDETAILS.Rows[0]["decReinursementAmount2"].ToString();
                txtReinursementAmount3.Text = dtTERMLOANDETAILS.Rows[0]["decReinursementAmount3"].ToString();
                txtReinursementAmount4.Text = dtTERMLOANDETAILS.Rows[0]["decReinursementAmount4"].ToString();
                hdnTermLoan.Value = dtTERMLOANDETAILS.Rows[0]["VCHTermLoanDOC"].ToString();

                if (hdnTermLoan.Value != "")
                {
                    hypTermLoan.NavigateUrl = "~/Incentives/Files/TermLoan/" + dtTERMLOANDETAILS.Rows[0]["VCHTermLoanDOC"].ToString();
                    hypTermLoan.Visible = true;
                    lnkTermLoanDelete.Visible = true;
                    fileTermLoan.Enabled = false;
                    lnkTermLoanuplod.Visible = false;

                }

                hdnBankStatement.Value = dtTERMLOANDETAILS.Rows[0]["VCHBankDetailDOC"].ToString();
                if (hdnBankStatement.Value != "")
                {
                    hypBankStatement.NavigateUrl = "~/Incentives/Files/TermLoan/" + dtTERMLOANDETAILS.Rows[0]["VCHBankDetailDOC"].ToString();
                    hypBankStatement.Visible = true;
                    lnkBankStatementDelete.Visible = true;
                    fu_BankStatement.Enabled = false;
                    lnkBankStatement.Visible = false;
                }

                txtLoanSanctionAmount.Text = dtTERMLOANDETAILS.Rows[0]["decSanctionAmount"].ToString();
                txtFinancialInstitution.Text = dtTERMLOANDETAILS.Rows[0]["vchFinancialInstitution"].ToString();
                // txtYear.Text=dtTERMLOANDETAILS.Rows[0]["intYear"].ToString();

                txtLoanMaturityDate.Text = Convert.ToDateTime(dtTERMLOANDETAILS.Rows[0]["dtmLoanMaturitydate"].ToString()).ToString("dd-MMM-yyyy").Replace("01-Jan-1900", "");
                //txtLoanStartDate.Text = Convert.ToDateTime(dtTERMLOANDETAILS.Rows[0]["dtmLoanStartDate"].ToString()).ToString("dd-MMM-yyyy");

            }
            if (dtPlannedDisbursal.Rows.Count > 0)
            {
                DataTable dtPlanned = new DataTable();
                dtPlanned = (DataTable)ViewState["dtPlannedDisbursal"];///  dcRowId column added
                foreach (DataRow dr1 in dtPlannedDisbursal.Rows)
                {
                    DataRow dr = dtPlanned.NewRow();
                    dr["DTMDisbursalDate"] = dr1["DTMDisbursalDate"].ToString();
                    dr["Amount"] = dr1["Amount"].ToString();
                    dr["Year"] = dr1["Year"].ToString();

                    dtPlanned.Rows.Add(dr);

                }

                ViewState["dtPlannedDisbursal"] = dtPlanned;
                grdPlannedDisbursal.DataSource = dtPlanned;
                grdPlannedDisbursal.DataBind();

                decimal totalamount = 0;
                for (int i = 0; dtPlannedDisbursal.Rows.Count > i; i++)
                {
                    totalamount += Convert.ToDecimal(dtPlannedDisbursal.Rows[i]["Amount"].ToString());
                }
                txtTotal.Text = totalamount.ToString("0.00");
            }
            if (dtRepaymentSchedule.Rows.Count > 0)
            {
                //---1
                //foreach (DataRow dr1 in dtRepaymentSchedule.Rows)
                //{
                //    DataTable dtRepayment = new DataTable();
                //    dtRepayment = (DataTable)ViewState["dtRepaymentSchedule"];
                //    DataRow dr = dtRepayment.NewRow();
                //    dr["decPlannedPrincipalAmount"] = dr1["decPlannedPrincipalAmount"].ToString();
                //    dr["decPlannedInterestAmount"] = dr1["decPlannedInterestAmount"].ToString();
                //    dr["dtmPlannedRepaymentDate"] = dr1["dtmPlannedRepaymentDate"].ToString();
                //    dr["decActualPrincipalAmount"] = dr1["decActualPrincipalAmount"].ToString();
                //    dr["decActualInterestAmount"] = dr1["decActualInterestAmount"].ToString();
                //    dr["dtmActualRepaymentDate"] = dr1["dtmActualRepaymentDate"].ToString();
                //    dtRepayment.Rows.Add(dr);
                //    ViewState["dtRepaymentSchedule"] = dtRepayment;

                //    grdRepayment.DataSource = dtRepayment;
                //    grdRepayment.DataBind();
                //}
            }
            if (dtPreviousSanction.Rows.Count > 0)
            {
                DataTable dtSanction = new DataTable();
                dtSanction = (DataTable)ViewState["dtPreviousSanction"];
                foreach (DataRow dr1 in dtPreviousSanction.Rows)
                {
                    DataRow dr = dtSanction.NewRow();
                    dr["DECSactionAmount"] = dr1["DECSactionAmount"].ToString();
                    dr["DTMSactionData"] = dr1["DTMSactionData"].ToString();
                    dr["VCHSactionOrder"] = dr1["VCHSactionOrder"].ToString();
                    dr["VCHSanctionOrderdOC"] = dr1["VCHSanctionOrderdOC"].ToString();
                    dtSanction.Rows.Add(dr);
                }
                ViewState["dtPreviousSanction"] = dtSanction;
                GVDPRESAN.DataSource = dtSanction;
                GVDPRESAN.DataBind();
            }
            //else
            //{
            //    GVDPRESAN.DataSource = null;
            //    GVDPRESAN.DataBind();
            //}


            #endregion

            #endregion
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }
    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string retval = Add();
            if (retval.Split('~')[0].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !</strong>', '" + MsgTitle + "'); </script>", false);

            }
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }

    #region Upload File
    protected void lnkDocumentDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;

            if (string.Equals(lnk.ID, lnkAUTHORIZEDFILEDdelete.ID))
            {
                string strFolderName = "InctBasicDoc";
                UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, strFolderName);
            }
            else if (string.Equals(lnk.ID, lnkTermLoanDelete.ID))
            {
                string strFolderName = "TermLoan";
                UpdFileRemove(hdnTermLoan, lnkTermLoanuplod, lnkTermLoanDelete, hypTermLoan, lblTermLoan, fileTermLoan, strFolderName);
            }
            else if (string.Equals(lnk.ID, lnkBankStatementDelete.ID))
            {
                string strFolderName = "TermLoan";
                UpdFileRemove(hdnBankStatement, lnkBankStatement, lnkBankStatementDelete, hypBankStatement, lblBankStatement, fu_BankStatement, strFolderName);
            }
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }
    protected void lnkDocumentUpload_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;

            //if (string.Equals(lnk.ID, lnkSupportingDocs.ID))
            //{
            //    if (fuSupportingDocs.HasFile)
            //    {
            //        string strFileName = "SupportingDoc" + DateTime.Now.ToString("_ddMMyyhhmmss");
            //        string strFolderName = "AvailDetails";
            //        UploadDocument(fuSupportingDocs, hdnSupportingDocs, strFileName, hypSupportingDocs, lblSupportingDocs, lnkSupportingDocsDelete, strFolderName);
            //    }
            //    else
            //    {
            //        Response.Redirect("<script></script>");
            //    }
            //}

            // else 
            if (string.Equals(lnk.ID, lnkAUTHORIZEDFILE.ID))
            {
                if (FlupAUTHORIZEDFILE.HasFile)
                {
                    string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                    string strFolderName = "InctBasicDoc";
                    UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, strFolderName, lnkAUTHORIZEDFILE);
                }
                else
                {
                }
            }
            else if (string.Equals(lnk.ID, lnkTermLoanuplod.ID))
            {
                if (fileTermLoan.HasFile)
                {
                    string strFileName = "TermLoan" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    string strFolderName = "TermLoan";
                    UploadDocument(fileTermLoan, hdnTermLoan, strFileName, hypTermLoan, lblTermLoan, lnkTermLoanDelete, strFolderName, lnkTermLoanuplod);
                }
                else
                {
                    Response.Redirect("<script></script>");
                }
            }
            else if (string.Equals(lnk.ID, lnkBankStatement.ID))
            {
                if (fu_BankStatement.HasFile)
                {
                    string strFileName = "bankstatement" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    string strFolderName = "TermLoan";
                    UploadDocument(fu_BankStatement, hdnBankStatement, strFileName, hypBankStatement, lblBankStatement, lnkBankStatementDelete, strFolderName, lnkBankStatement);
                }
                else
                {
                    Response.Redirect("<script></script>");
                }
            }
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }
    private void UploadDocument(FileUpload fuDocument, HiddenField hdnDocument, string strFileName, HyperLink hypDocument, Label lblDocument, LinkButton lnkDocumentDelete, string strFolderName, LinkButton lnkDocumentupload = null)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFolderName));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (fuDocument.HasFile)
            {

                //// validation checked in client sciprt side
                if (!(IsFileValid(fuDocument)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots.', '" + MsgTitle + "'); </script>", false);
                    return;
                }
                string filename = string.Empty;
                if ((Path.GetExtension(fuDocument.FileName) != ".pdf") && (Path.GetExtension(fuDocument.FileName) != ".zip"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Upload  pdf or zip file Only!', '" + MsgTitle + "'); </script>", false);
                    return;
                }
                int fileSize = fuDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File size is too large. Maximum file size permitted is 4 MB', '" + MsgTitle + "'); </script>", false);
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
                lnkDocumentupload.Visible = false;
                fuDocument.Enabled = false;
            }
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
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
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;

        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
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

    #region Addmore for interest
    void crdtincentive()
    {
        try
        {
            DataTable dtPlannedDisbursal = new DataTable();
            DataColumn dcRowId = new DataColumn("dcRowId");
            dcRowId.DataType = Type.GetType("System.Int32");
            dcRowId.AutoIncrement = true;
            dcRowId.AutoIncrementSeed = 1;
            dcRowId.AutoIncrementStep = 1;
            dtPlannedDisbursal.Columns.Add(dcRowId);

            DataColumn DTMDisbursalDate = new DataColumn("DTMDisbursalDate");
            DTMDisbursalDate.DataType = Type.GetType("System.String");
            dtPlannedDisbursal.Columns.Add(DTMDisbursalDate);

            DataColumn Amount = new DataColumn("Amount");
            Amount.DataType = Type.GetType("System.String");
            dtPlannedDisbursal.Columns.Add(Amount);

            DataColumn Year = new DataColumn("Year");
            Year.DataType = Type.GetType("System.String");
            dtPlannedDisbursal.Columns.Add(Year);

            ViewState["dtPlannedDisbursal"] = dtPlannedDisbursal;
            grdPlannedDisbursal.DataSource = dtPlannedDisbursal;
            grdPlannedDisbursal.DataBind();


            DataTable dtRepaymentSchedule = new DataTable();
            dcRowId = new DataColumn("dcRowId");
            dcRowId.DataType = Type.GetType("System.Int32");
            dcRowId.AutoIncrement = true;
            dcRowId.AutoIncrementSeed = 1;
            dcRowId.AutoIncrementStep = 1;
            dtRepaymentSchedule.Columns.Add(dcRowId);

            DataColumn decPlannedPrincipalAmount = new DataColumn("decPlannedPrincipalAmount");
            decPlannedPrincipalAmount.DataType = Type.GetType("System.String");
            dtRepaymentSchedule.Columns.Add(decPlannedPrincipalAmount);

            DataColumn decPlannedInterestAmount = new DataColumn("decPlannedInterestAmount");
            decPlannedInterestAmount.DataType = Type.GetType("System.String");
            dtRepaymentSchedule.Columns.Add(decPlannedInterestAmount);

            DataColumn dtmPlannedRepaymentDate = new DataColumn("dtmPlannedRepaymentDate");
            dtmPlannedRepaymentDate.DataType = Type.GetType("System.String");
            dtRepaymentSchedule.Columns.Add(dtmPlannedRepaymentDate);

            DataColumn decActualPrincipalAmount = new DataColumn("decActualPrincipalAmount");
            decActualPrincipalAmount.DataType = Type.GetType("System.String");
            dtRepaymentSchedule.Columns.Add(decActualPrincipalAmount);

            DataColumn decActualInterestAmount = new DataColumn("decActualInterestAmount");
            decActualInterestAmount.DataType = Type.GetType("System.String");
            dtRepaymentSchedule.Columns.Add(decActualInterestAmount);

            DataColumn dtmActualRepaymentDate = new DataColumn("dtmActualRepaymentDate");
            dtmActualRepaymentDate.DataType = Type.GetType("System.String");
            dtRepaymentSchedule.Columns.Add(dtmActualRepaymentDate);

            //--2
            // ViewState["dtRepaymentSchedule"] = dtRepaymentSchedule;
            grdPlannedDisbursal.DataSource = dtRepaymentSchedule;
            grdPlannedDisbursal.DataBind();


            DataTable dtPreviousSanction = new DataTable();
            dcRowId = new DataColumn("dcRowId");
            dcRowId.DataType = Type.GetType("System.Int32");
            dcRowId.AutoIncrement = true;
            dcRowId.AutoIncrementSeed = 1;
            dcRowId.AutoIncrementStep = 1;
            dtPreviousSanction.Columns.Add(dcRowId);

            DataColumn DECSactionAmount = new DataColumn("DECSactionAmount");
            DECSactionAmount.DataType = Type.GetType("System.String");
            dtPreviousSanction.Columns.Add(DECSactionAmount);

            DataColumn DTMSactionData = new DataColumn("DTMSactionData");
            DTMSactionData.DataType = Type.GetType("System.String");
            dtPreviousSanction.Columns.Add(DTMSactionData);

            DataColumn VCHSactionOrder = new DataColumn("VCHSactionOrder");
            VCHSactionOrder.DataType = Type.GetType("System.String");
            dtPreviousSanction.Columns.Add(VCHSactionOrder);

            DataColumn VCHSanctionOrderdOC = new DataColumn("VCHSanctionOrderdOC");
            VCHSanctionOrderdOC.DataType = Type.GetType("System.String");
            dtPreviousSanction.Columns.Add(VCHSanctionOrderdOC);

            ViewState["dtPreviousSanction"] = dtPreviousSanction;
            GVDPRESAN.DataSource = dtPreviousSanction;
            GVDPRESAN.DataBind();
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }


    }
    protected void grdPlannedDisbursal_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdfanew = (HiddenField)grdPlannedDisbursal.Rows[e.RowIndex].Cells[2].FindControl("hdnRowId");
            DataTable dtnew0 = new DataTable();
            dtnew0 = (DataTable)ViewState["dtPlannedDisbursal"];
            DataRow[] dr1 = null;
            dr1 = dtnew0.Select("dcRowId='" + hdfanew.Value + "'");
            for (int i = 0; i < dr1.Length; i++)
            {

                dr1[i].Delete();
            }
            dtnew0.AcceptChanges();
            grdPlannedDisbursal.DataSource = dtnew0;
            grdPlannedDisbursal.DataBind();
            ViewState["dtPlannedDisbursal"] = dtnew0;

            double dbltotdisburse = 0;
            if (dtnew0.Rows.Count > 0)
            {
                foreach (DataRow dr12 in dtnew0.Rows)
                {
                    if (Convert.ToString(dr12["Amount"]).Trim() != "")
                    {
                        dbltotdisburse = dbltotdisburse + Convert.ToDouble(Convert.ToString(dr12["Amount"]));
                    }
                    else
                    {
                        dbltotdisburse = dbltotdisburse + 0;
                    }

                    txtTotal.Text = Convert.ToString(dbltotdisburse);

                }
            }
            else
            {
                txtTotal.Text = "0.00";
            }
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }

    }
    protected void grdRepayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //--5
        //try
        //{
        //    HiddenField hdfanew = (HiddenField)grdRepayment.Rows[e.RowIndex].Cells[2].FindControl("hdnRowId");
        //    DataTable dtnew0 = new DataTable();
        //    dtnew0 = (DataTable)ViewState["dtRepaymentSchedule"];
        //    DataRow[] dr1 = null;
        //    dr1 = dtnew0.Select("dcRowId='" + hdfanew.Value + "'");
        //    for (int i = 0; i < dr1.Length; i++)
        //    {

        //        dr1[i].Delete();
        //    }
        //    dtnew0.AcceptChanges();
        //    grdRepayment.DataSource = dtnew0;
        //    grdRepayment.DataBind();
        //    ViewState["dtRepaymentSchedule"] = dtnew0;
        //}

        //catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        //finally { }
    }

    protected void GVDPRESAN_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdfanew = (HiddenField)GVDPRESAN.Rows[e.RowIndex].Cells[4].FindControl("hdnRowId");
            DataTable dtnew0 = new DataTable();
            dtnew0 = (DataTable)ViewState["dtPreviousSanction"];
            DataRow[] dr1 = null;
            dr1 = dtnew0.Select("dcRowId='" + hdfanew.Value + "'");
            for (int i = 0; i < dr1.Length; i++)
            {

                dr1[i].Delete();
            }
            dtnew0.AcceptChanges();
            GVDPRESAN.DataSource = dtnew0;
            GVDPRESAN.DataBind();
            ViewState["dtPreviousSanction"] = dtnew0;
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }

    protected void LinkButton26_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtPlannedDisbursal = new DataTable();
            dtPlannedDisbursal = (DataTable)ViewState["dtPlannedDisbursal"];
            bool status = true;
            foreach (DataRow drc in dtPlannedDisbursal.Rows)
            {
                if (drc["DTMDisbursalDate"].ToString() == txtDisbursalDate.Text)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Sanction date should not be same!', '" + MsgTitle + "'); </script>", false);
                    //return;
                    status = false;
                }
            }
            if (status == true)
            {
                DataRow dr = dtPlannedDisbursal.NewRow();
                dr["DTMDisbursalDate"] = txtDisbursalDate.Text.Trim();
                dr["Amount"] = txtAmount.Text.Trim();
                // dr["Year"] = ddlYear.SelectedItem;

                dtPlannedDisbursal.Rows.Add(dr);
                ViewState["dtPlannedDisbursal"] = dtPlannedDisbursal;

                grdPlannedDisbursal.DataSource = dtPlannedDisbursal;
                grdPlannedDisbursal.DataBind();

                txtDisbursalDate.Text = "";
                txtAmount.Text = "";
                //txtYear.Text = "";
                decimal totalamount = 0;
                for (int i = 0; dtPlannedDisbursal.Rows.Count > i; i++)
                {
                    totalamount += Convert.ToDecimal(dtPlannedDisbursal.Rows[i]["Amount"].ToString());
                }
                txtTotal.Text = totalamount.ToString(".00");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Disbursal Date should not be same!', '" + MsgTitle + "'); </script>", false);
            }
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }

    }
    protected void LinkButton28_Click(object sender, EventArgs e)
    {
        //--3
        //try
        //{
        //    DataTable dtRepaymentSchedule = new DataTable();
        //    dtRepaymentSchedule = (DataTable)ViewState["dtRepaymentSchedule"];
        //    DataRow dr = dtRepaymentSchedule.NewRow();
        //    //dr["decPlannedPrincipalAmount"] = txtPlannedPrincipalAmount .Text.Trim();
        //    //dr["decPlannedInterestAmount"] =  txtPlannedInterestAmount .Text.Trim();
        //    //dr["dtmPlannedRepaymentDate"] =   txtPlannedRepaymentDate .Text.Trim();
        //    dr["decActualPrincipalAmount"] = txtActualPrincipalAmount.Text.Trim();
        //    dr["decActualInterestAmount"] = txtActualInterestAmount.Text.Trim();
        //    // dr["dtmActualRepaymentDate"] =    txtActualRepaymentDate .Text.Trim();
        //    dtRepaymentSchedule.Rows.Add(dr);
        //    ViewState["dtRepaymentSchedule"] = dtRepaymentSchedule;

        //    grdRepayment.DataSource = dtRepaymentSchedule;
        //    grdRepayment.DataBind();
        //    //txtPlannedPrincipalAmount.Text="";
        //    //txtPlannedInterestAmount.Text ="";
        //    //txtPlannedRepaymentDate.Text  ="";
        //    txtActualPrincipalAmount.Text = "";
        //    txtActualInterestAmount.Text = "";
        //    //txtActualRepaymentDate.Text = "";
        //}

        //catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        //finally { }

    }
    #endregion

    protected string Add()
    {
        string retVal = string.Empty;
        INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();

        TermLoanDetails objTermLoanDetails = new TermLoanDetails();
        List<PlannedDisbursal> objlistPlannedDisbursal = new List<PlannedDisbursal>();
        List<RepaymentSchedule> objlistRepaymentSchedule = new List<RepaymentSchedule>();
        List<PreviousSanction> objlistPreviousSanction = new List<PreviousSanction>();
        try
        {
            #region INDUSTRIAL UNIT
            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            if (DdlGender.SelectedIndex > 0)
            {
                objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            }
            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            if (radApplyBy.SelectedIndex > 0)
            {
                objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }
            objINDUSTRIAL_UNIT_MASTER.AADHAARNO_IND = TxtAdhaar1.Text;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILECODE_IND = hidAuthorizing.Value;
            objEntity.IndsutUnitMstDet = objINDUSTRIAL_UNIT_MASTER;
            #endregion

            #region interest
            objTermLoanDetails.decReinursementAmount1 = Convert.ToDecimal(txtReinursementAmount1.Text.Trim() == "" ? "0" : txtReinursementAmount1.Text.Trim());
            objTermLoanDetails.decReinursementAmount2 = Convert.ToDecimal(txtReinursementAmount2.Text.Trim() == "" ? "0" : txtReinursementAmount2.Text.Trim());
            objTermLoanDetails.decReinursementAmount3 = Convert.ToDecimal(txtReinursementAmount3.Text.Trim() == "" ? "0" : txtReinursementAmount3.Text.Trim());
            objTermLoanDetails.decReinursementAmount4 = Convert.ToDecimal(txtReinursementAmount4.Text.Trim() == "" ? "0" : txtReinursementAmount1.Text.Trim());
            objTermLoanDetails.decSanctionAmount = Convert.ToDecimal(txtLoanSanctionAmount.Text.Trim() == "" ? "0" : txtLoanSanctionAmount.Text.Trim());
            //if (txtLoanMaturityDate.Text.Trim() != "")
            objTermLoanDetails.dtmLoanMaturitydate = Convert.ToDateTime(txtLoanMaturityDate.Text.Trim() == "" ? "1900/01/01" : txtLoanMaturityDate.Text.Trim());
            //if (txtLoanMaturityDate.Text.Trim() != "")
            objTermLoanDetails.dtmLoanStartDate = Convert.ToDateTime(txtLoanMaturityDate.Text.Trim() == "" ? "1900/01/01" : txtLoanMaturityDate.Text.Trim());

            //objTermLoanDetails.intYear = Convert.ToInt32(txtYear.Text);
            objTermLoanDetails.vchFinancialInstitution = txtFinancialInstitution.Text;
            objTermLoanDetails.VCHTermLoanDOC = hdnTermLoan.Value;
            objTermLoanDetails.VCHBankDetailDOC = hdnBankStatement.Value;

            DataTable dtPlannedDisbursal = new DataTable();
            dtPlannedDisbursal = (DataTable)ViewState["dtPlannedDisbursal"];
            if (dtPlannedDisbursal.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPlannedDisbursal.Rows)
                {
                    PlannedDisbursal objPlannedDisbursal = new PlannedDisbursal();
                    objPlannedDisbursal.Amount = Convert.ToDecimal(dr["Amount"].ToString().Trim() == "" ? "0" : dr["Amount"].ToString().Trim());
                    //if (dr["DTMDisbursalDate"].ToString().Trim() != "")
                    objPlannedDisbursal.DTMDisbursalDate = Convert.ToDateTime(dr["DTMDisbursalDate"].ToString().Trim() == "" ? "1900/01/01" : dr["DTMDisbursalDate"].ToString().Trim());
                    //objPlannedDisbursal.Year=dr["Year"].ToString ();
                    objlistPlannedDisbursal.Add(objPlannedDisbursal);
                }
            }
            objTermLoanDetails.lstPlannedDisbursal = objlistPlannedDisbursal;
            //--4
            //DataTable dtRepaymentSchedule = new DataTable();
            //dtRepaymentSchedule = (DataTable)ViewState["dtRepaymentSchedule"];
            //if (dtRepaymentSchedule.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dtRepaymentSchedule.Rows)
            //    {
            //        RepaymentSchedule objRepaymentSchedule = new RepaymentSchedule();
            //        objRepaymentSchedule.decActualInterestAmount = Convert.ToDecimal(dr["decActualInterestAmount"].ToString());
            //        objRepaymentSchedule.decActualPrincipalAmount = Convert.ToDecimal(dr["decActualPrincipalAmount"].ToString());
            //        //objRepaymentSchedule.decPlannedInterestAmount = Convert.ToDecimal(dr["decPlannedInterestAmount"].ToString());
            //        //objRepaymentSchedule.decPlannedPrincipalAmount = Convert.ToDecimal(dr["decPlannedPrincipalAmount"].ToString());
            //        //objRepaymentSchedule.dtmActualRepaymentDate = Convert.ToDateTime(dr["dtmActualRepaymentDate"].ToString());
            //        //objRepaymentSchedule.dtmPlannedRepaymentDate = Convert.ToDateTime(dr["dtmPlannedRepaymentDate"].ToString());
            //        objlistRepaymentSchedule.Add(objRepaymentSchedule);
            //    }
            //}
            objTermLoanDetails.lstRepaymentSchedule = objlistRepaymentSchedule;


            DataTable dtPreviousSanction = new DataTable();
            dtPreviousSanction = (DataTable)ViewState["dtPreviousSanction"];
            if (dtPreviousSanction.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPreviousSanction.Rows)
                {
                    PreviousSanction objPreviousSanction = new PreviousSanction();
                    objPreviousSanction.DECSactionAmount = Convert.ToDecimal(dr["DECSactionAmount"].ToString().Trim() == "" ? "0" : dr["DECSactionAmount"].ToString().Trim());
                    //if (dr["DTMSactionData"].ToString().Trim() != "")
                    objPreviousSanction.DTMSactionData = Convert.ToDateTime(dr["DTMSactionData"].ToString().Trim() == "" ? "1900/01/01" : dr["DTMSactionData"].ToString().Trim());
                    objPreviousSanction.VCHSactionOrder = dr["VCHSactionOrder"].ToString();
                    objPreviousSanction.VCHSanctionOrderdOC = dr["VCHSanctionOrderdOC"].ToString();
                    objlistPreviousSanction.Add(objPreviousSanction);
                }
            }
            objTermLoanDetails.listPreviousSanction = objlistPreviousSanction;
            objEntity.TermLoanDetails = objTermLoanDetails;
            #endregion

            #region COMMON
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
            if (Session["FyYear"] != null && Session["FyYear"].ToString().Trim() != "")
            {
                objEntity.FYear = Convert.ToInt16(Convert.ToString(Session["FyYear"]));
            }
            else
            {
                objEntity.FYear = 0;
            }
            objEntity.incentivetype = 4;
            objEntity.FormType = FormNumber.InterestSubsidy_01;
            #endregion

            retVal = IncentiveManager.CreateIncentiveInetersSubsidy(objEntity);

        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
        return retVal;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string retval = Add();
        int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
        /////------------------------------------------------------------------------------------------------
        Response.Redirect("InterestSubsidyPreview.aspx?InctUniqueNo=" + Convert.ToString(mstyp));
    }

    protected void lnkPrSanctionadd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtPreviousSanction = new DataTable();
            dtPreviousSanction = (DataTable)ViewState["dtPreviousSanction"];
            bool status = true;
            foreach (DataRow drc in dtPreviousSanction.Rows)
            {
                if (drc["DTMSactionData"].ToString() == txtSactionData.Text)
                {
                    status = false;

                }
            }
            if (status == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Sanction date should not be same!', '" + MsgTitle + "'); </script>", false);

            }
            else
            {

                DataRow dr = dtPreviousSanction.NewRow();
                dr["DECSactionAmount"] = txtSactionAmount.Text;
                dr["DTMSactionData"] = txtSactionData.Text;
                dr["VCHSactionOrder"] = txtSactionOrder.Text;
                string VCHSanctionOrderdOC = "";
                if (FLPSanctionOrder.HasFile)
                {

                    //// validation checked in client sciprt side
                    if (!(IsFileValid(FLPSanctionOrder)))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots. ', '" + MsgTitle + "'); </script>", false);
                        return;
                    }
                    string fillename = FLPSanctionOrder.FileName;
                    if (Path.GetExtension(fillename).ToUpper() == ".PDF")
                    {
                        VCHSanctionOrderdOC = "Regd_" + DateTime.Now.ToString("ddMMMyyyyHHmmssfff.") + Path.GetExtension(FLPSanctionOrder.PostedFile.FileName).Substring(1);
                        FLPSanctionOrder.SaveAs(Server.MapPath("~/incentives/Files/TermLoan/") + VCHSanctionOrderdOC);

                    }
                    else
                    {
                        Response.Write("<script>jAlert('Upload only pdf file','" + MsgTitle + "')</script>");
                    }
                }
                dr["VCHSanctionOrderdOC"] = VCHSanctionOrderdOC;
                dtPreviousSanction.Rows.Add(dr);
                ViewState["dtPreviousSanction"] = dtPreviousSanction;


                txtSactionAmount.Text = txtSactionData.Text = txtSactionOrder.Text = "";

                GVDPRESAN.DataSource = dtPreviousSanction;
                GVDPRESAN.DataBind();
            }
        }

        catch (Exception ex) { Util.LogError(ex, "Incentive"); }
        finally { }
    }
    protected void GVDPRESAN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdnVCHSanctionOrderdOC = (HiddenField)e.Row.FindControl("hdnVCHSanctionOrderdOC");
            HyperLink hypvchIPRRegistrationFile = (HyperLink)e.Row.FindControl("hypvchIPRRegistrationFile");

            /*-----------------------------------------------------------*/

            if (hdnVCHSanctionOrderdOC.Value == "")
            {
                hypvchIPRRegistrationFile.Visible = false;
            }
            else
            {
                hypvchIPRRegistrationFile.Visible = true;
            }
        }
    }
}
