using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.Data;
using System.IO;

public partial class incentives_Subsidy_Plant_MCPreview : System.Web.UI.Page
{
    DataTable dtSalutation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            crdtincentive1();
            FillData();

            GetMasterdetails();
            TRVisibility();
        }

    }
    public void GetMasterdetails()
    {
        try
        {
            Incentive objIncentive = new Incentive();
            objIncentive.strcActioncode = "M";
            objIncentive.IncentiveNum = hdnTitle.Value;
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveMaster(objIncentive);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtbPostSubFlag = ds.Tables[0];
                lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
                //lblTitle1.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #region FillData
    protected void FillData()
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
            DataSet dsliveView = IncentiveManager.PostpopulateDataSPM(Convert.ToInt32(Request.QueryString["InctUniqueNo"].ToString()));
            DataTable dtindustryView = dsliveView.Tables[0];////////////industry panel
            DataTable dtProductionPre = dsliveView.Tables[1];///////////production & employment

            DataTable dtProductionDetBefPre = dsliveView.Tables[2];///////////production & employment Before
            DataTable dtProductionDetAftPre = dsliveView.Tables[3];///////////production & employment After
            DataTable dtInvestmentPre = dsliveView.Tables[4];///////////investment details
            DataTable dtMeansFinancePre = dsliveView.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanPre = dsliveView.Tables[6];///////////Means of Finance Term Loan
            DataTable dtMoFWorkingLoanPre = dsliveView.Tables[7];///////////Means of Finance Working Loan


            DataTable dtAvailDetView = dsliveView.Tables[8];///////////Avail Details Master
            DataTable dtAssistanceView = dsliveView.Tables[9];///////////Avail Details Assistance Tran Table
            DataTable dtBankView = dsliveView.Tables[10];///////////Avail Details Assistance Tran Table

            DataTable dtMainTable = dsliveView.Tables[11];///////////M_INCT_APPLICATION Table
            dtSalutation = dsliveView.Tables[12];///////////For Mail Id and Mobile number

            hdnEmail.Value = dtSalutation.Rows[0]["VCH_EMAIL"].ToString();
            hdnMobile.Value = dtSalutation.Rows[0]["VCH_OFF_MOBILE"].ToString();

            //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();
            #region IndustrailUnit
            if (dtindustryView.Rows.Count > 0)
            {
                lbl_EnterPrise_Name.Text = dtindustryView.Rows[0]["vchEnterpriseName"].ToString();
                lblName.Text = lbl_EnterPrise_Name.Text;
                //dtindustryView.Rows[0]["intOrganisationType"].ToString();	
                DataSet ds1 = new DataSet();
                ds1 = IncentiveManager.dynamic_name_doc_bind();
                ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryView.Rows[0]["intUnitType"].ToString() + "'";
                ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryView.Rows[0]["intOrganisationType"].ToString() + "'";
                LblApplicantName.Text = dtindustryView.Rows[0]["VCHPREAPPLICANTNAME"].ToString();
                lblApplyBy.Text = (dtindustryView.Rows[0]["INTAPPLYBY"].ToString() == "1") ? "Self" : "Authorized Person";
                DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
                if (dt.Rows.Count > 0)
                {
                    Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                    Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                    Lbl_Org_Doc_Type.Text = dt.Rows[0]["vch_orgdocumentname"].ToString();
                    //lblAuthorizing.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();               

                }
                else
                {
                    Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                    //Hid_Org_Doc_Type.Value = "";
                }

                if (dtindustryView.Rows[0]["INTAPPLYBY"].ToString() == "1")
                {
                    if (dtindustryView.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                    {
                        LblAdhaar1.Text = dtindustryView.Rows[0]["VCHAADHAARNO"].ToString();
                    }
                    divadhhardetails.Visible = true;
                    divAuthorizing.Visible = false;
                }
                else
                {
                    divadhhardetails.Visible = false;
                    divAuthorizing.Visible = true;
                    if (dtindustryView.Rows[0]["VCHAUTHORIZEDFILECODE"].ToString() != "")
                    {
                        Lbl_Org_Doc_Type.Text = dtindustryView.Rows[0]["VCHAUTHORIZEDFILECODEName"].ToString();
                        hypAUTHORIZEDFILE.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryView.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    }
                }

                lbl_Org_Type.Text = dtindustryView.Rows[0]["OrgTypename"].ToString();
                lbl_Industry_Address.Text = dtindustryView.Rows[0]["vchIndustryAddress"].ToString();
                lblAt.Text = lbl_Industry_Address.Text;
                lblAt1.Text = lbl_Industry_Address.Text;
                //dtindustryPre.Rows[0]["intUnitCat"].ToString();		
                lbl_Unit_Cat.Text = dtindustryView.Rows[0]["Unitcategoryname"].ToString();
                Lbl_Pioneer_Doc_Name.Text = dtindustryView.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();

                dt = (ds1.Tables[1].DefaultView).ToTable();
                if (dt.Rows.Count > 0)
                {
                    string strDocType = dt.Rows[0]["vchDocumentType"].ToString();
                    if (strDocType != "")
                    {
                        Div_Unit_Type_Doc.Visible = true;
                        Lbl_Unit_Type_Doc_Name.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();

                        Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryView.Rows[0]["vchUnitTypeDoc"].ToString();
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


                //dtindustryView.Rows[0]["intUnitType"].ToString();			
                lbl_Unit_Type.Text = dtindustryView.Rows[0]["UnitTypename"].ToString();
                //dtindustryView.Rows[0]["vchDocCode"].ToString();	

                //dtindustryView.Rows[0]["vchUnitTypeDoc"].ToString();

                if (dtindustryView.Rows[0]["intPriority"].ToString() == "1")
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
                if (dtindustryView.Rows[0]["intPioneer"].ToString() == "1")
                {
                    lblIs_Is_Pioneer.Text = "Yes";
                }
                else
                {
                    lblIs_Is_Pioneer.Text = "No";
                }


                Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryView.Rows[0]["vchPioneerCertificate"].ToString();

                lbl_Regd_Office_Address.Text = dtindustryView.Rows[0]["vchRegisteredOfcAddress"].ToString();

                lbl_Gender_Partner.Text = dtindustryView.Rows[0]["GenderType"].ToString() + " " + dtindustryView.Rows[0]["vchManagingPartnerName"].ToString();

                //dtindustryView.Rows[0]["vchManagingPartnerName"].ToString();
                Lbl_Org_Doc_Type.Text = dtindustryView.Rows[0]["CertOfRegdDocName"].ToString();
                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryView.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                //dtindustryView.Rows[0]["vchCertOfRegdDocCode"].ToString();	

                lbl_EIN_IL_NO.Text = dtindustryView.Rows[0]["vchEINNO"].ToString();
                lbl_EIN_IL_Date.Text = dtindustryView.Rows[0]["dtmEIN"].ToString();
                lbl_PC_No.Text = dtindustryView.Rows[0]["vchPcNo"].ToString();
                lbl_Prod_Comm_Date_Before.Text = dtindustryView.Rows[0]["dtmProdCommBefore"].ToString();
                lbl_PC_Issue_Date_Before.Text = dtindustryView.Rows[0]["dtmPCIssueDateBefore"].ToString();

                if (dtindustryView.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryView.Rows[0]["vchappnobef"].ToString();
                }
                else if (dtindustryView.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryView.Rows[0]["vchappnobef"].ToString();
                }
                if (dtindustryView.Rows[0]["dtmProdCommBefore"].ToString() != "")
                {

                    divbefor.Visible = true;
                    divbefor1.Visible = true;
                    divbefor2.Visible = true;
                    tr_Prod_Comm_Before.Visible = true;
                    divEmp_Before_Doc_Name.Visible = true;
                    FFCI_Before_Doc_Name.Visible = true;
                    Approved_DPR_Before_Doc_Name.Visible = true;

                }
                else
                {
                    divbefor.Visible = false;
                    divbefor1.Visible = false;
                    divbefor2.Visible = false;
                    tr_Prod_Comm_Before.Visible = false;
                    divEmp_Before_Doc_Name.Visible = false;
                    FFCI_Before_Doc_Name.Visible = false;
                    Approved_DPR_Before_Doc_Name.Visible = false;
                    lblAfterEMD11.Text = "Date of Production Commencement";
                    lblAfterEMD189.Text = "PC Issuance Date";
                    lbl_PC_No_After.Text = "PC No";
                    lblemd.Text = "";
                    Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                    lblEMDInvestment.Text = "";
                }
                lbl_pcno_befor.Text = dtindustryView.Rows[0]["vchpcnobefore"].ToString();
                lblGstin.Text = dtindustryView.Rows[0]["VCHGSTIN"].ToString();
                lbl_Prod_Comm_Date_After.Text = dtindustryView.Rows[0]["dtmProdCommAfter"].ToString();
                lbl_PC_Issue_Date_After.Text = dtindustryView.Rows[0]["dtmPCIssueDateAfter"].ToString();
                if (dtindustryView.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryView.Rows[0]["vchappnoAft"].ToString();
                }
                else if (dtindustryView.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryView.Rows[0]["vchappnoAft"].ToString();
                }
                //dtindustryView.Rows[0]["intDistrictCode"].ToString();			
                lbl_District.Text = dtindustryView.Rows[0]["distname"].ToString();
                lblDist.Text = dtindustryView.Rows[0]["distname"].ToString();
                lblDist1.Text = dtindustryView.Rows[0]["distname"].ToString();
                //dtindustryView.Rows[0]["intSectorId"].ToString();			
                lbl_Sector.Text = dtindustryView.Rows[0]["sectorName"].ToString();
                //dtindustryView.Rows[0]["intSubSectorId"].ToString();			
                lbl_Sub_Sector.Text = dtindustryView.Rows[0]["SubsectorName"].ToString();
                Lbl_Derived_Sector.Text = dtindustryView.Rows[0]["vchDerivedSector"].ToString();
                //dtindustryView.Rows[0]["bitSectoralPolicy"].ToString();

                if (dtindustryView.Rows[0]["bitPriorityIPR"].ToString() == "1")
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

            #endregion


            #region Investment


            //dtInvestmentPre.Rows[0]["slno"].ToString();
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

            /*--------------------------------------------------------------------*/

            if (dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString() != "")
            {
                divInvest1.Visible = true;
                hypPlantMachinery.NavigateUrl = "../incentives/Files/investment/" + dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString();
            }
            else
            {
                divInvest1.Visible = false;
            }

            /*--------------------------------------------------------------------*/

            if (dtInvestmentPre.Rows[0]["vchNewBillDoc"].ToString() != "")
            {
                divInvest2.Visible = true;
                hypPlantMachinery2.NavigateUrl = "../incentives/Files/investment/" + dtInvestmentPre.Rows[0]["vchNewBillDoc"].ToString();
            }
            else
            {
                divInvest2.Visible = false;
            }

            /*--------------------------------------------------------------------*/

            if (dtInvestmentPre.Rows[0]["vchSecHandDoc"].ToString() != "")
            {
                divInvest3.Visible = true;
                hypPlantMachinerySecond.NavigateUrl = "../incentives/Files/investment/" + dtInvestmentPre.Rows[0]["vchSecHandDoc"].ToString();
            }
            else
            {
                divInvest3.Visible = false;
            }

            /*--------------------------------------------------------------------*/

            if (dtInvestmentPre.Rows[0]["vchSecHandBill"].ToString() != "")
            {
                divInvest4.Visible = true;
                hypPlantMachinerySecond2.NavigateUrl = "../incentives/Files/investment/" + dtInvestmentPre.Rows[0]["vchSecHandBill"].ToString();
            }
            else
            {
                divInvest4.Visible = false;
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


            #region Avail Details
            if (dtAvailDetView.Rows.Count > 0)
            {
                if (dtAvailDetView.Rows[0]["intNeverAvailedPrior"].ToString() == "1")
                {
                    lblAvailYes.Text = "Yes";
                    lblexemp.Text = dtAvailDetView.Rows[0]["decClaimExempted"].ToString();
                    lblreim.Text = dtAvailDetView.Rows[0]["decClaimReimbursement"].ToString();
                    lblAvailDoc.Text = "Document details of assistance sanctioned";

                    if (dtAvailDetView.Rows[0]["VchSanctionDoc"].ToString() != "")
                    {
                        hypAvailDoc.NavigateUrl = "../incentives/Files/AvailDetails/" + dtAvailDetView.Rows[0]["VchSanctionDoc"].ToString();
                        //hypAvailDoc.NavigateUrl = "../incentives/Files/AvailDetails/" + Convert.ToString(Session["investorid"]) + "/" + dtAvailDetView.Rows[0]["VchSanctionDoc"].ToString();
                    }
                    if (dtAssistanceView.Rows.Count > 0)
                    {
                        DataTable dtincentive1 = new DataTable();
                        dtincentive1 = (DataTable)ViewState["dtincentive1"];

                        foreach (DataRow dravil2 in dtAssistanceView.Rows)
                        {
                            DataRow dravil1gr = dtincentive1.NewRow();

                            dravil1gr["vchagency"] = dravil2["vchInstitutionName"].ToString();
                            dravil1gr["vchsacamt"] = dravil2["decSanctionedAmount"].ToString();
                            dravil1gr["vchsacord"] = dravil2["vchSanctionOrderNo"].ToString();
                            dravil1gr["vchsacdat"] = dravil2["dtmAvailedDate"].ToString();
                            dravil1gr["vchavilamt"] = dravil2["decAmountAvailed"].ToString();
                            dtincentive1.Rows.Add(dravil1gr);
                        }

                        ViewState["dtincentive1"] = dtincentive1;
                        grdIncentiveAvailed.DataSource = dtincentive1;
                        grdIncentiveAvailed.DataBind();
                    }
                }
                else
                {
                    lblAvailYes.Text = "No";
                    lblreim.Text = dtAvailDetView.Rows[0]["decClaimReimbursement"].ToString();
                    lblAvailDoc.Text = "Undertaking on non-availment of subsidy earlier on this project";
                    if (dtAvailDetView.Rows[0]["vchUndertakingSubsidyDoc"].ToString() != "")
                    {
                        hypAvailDoc.NavigateUrl = "../incentives/Files/AvailDetails/" + Convert.ToString(Session["investorid"]) + "/" + dtAvailDetView.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                    }
                    divAvail1.Visible = false;
                    divAvail2.Visible = false;
                    //divAvail3.Visible = false;
                }

            }
            //LblDiffClaimAmt.Text = dtAvailDetView.Rows[0]["DecDiffAmtClaim"].ToString();
            //HypSupportingDocs.NavigateUrl = "~/incentives/Files/AvailDetails/" + dtAvailDetView.Rows[0]["VchSanctionDoc"].ToString();

            //grdAssistanceDetailsAD.DataSource = dtAssistanceView;
            //grdAssistanceDetailsAD.DataBind();
            #endregion

            #region Bank Details

            if (dtBankView.Rows.Count > 0)
            {
                LblAccNo.Text = dtBankView.Rows[0]["VCHACCOUNTNO"].ToString();
                LblBnkNm.Text = dtBankView.Rows[0]["VCHBANKNAME"].ToString();
                LblBranch.Text = dtBankView.Rows[0]["VCHBRANCHNAME"].ToString();
                LblIFSC.Text = dtBankView.Rows[0]["VCHIFSCNO"].ToString();
                LblMICRNo.Text = dtBankView.Rows[0]["VCHMICR"].ToString();
                if (dtBankView.Rows[0]["vchBankDoc"].ToString() != "")
                {
                    hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBankView.Rows[0]["vchBankDoc"].ToString();
                }
            }
            #endregion

            LblNameFooter.Text = LblApplicantName.Text;// dtindustryView.Rows[0]["VCHAPPLICANTNAME"].ToString();
            LblIndustryUnit.Text = lbl_EnterPrise_Name.Text;
            Label101.Text = LblIndustryUnit.Text;

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
            hdnTitle.Value = dtMainTable.Rows[0]["VCHINCENTIVENO"].ToString();
            lblDate.Text = dtMainTable.Rows[0]["DTMCREATEDBY"].ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
    public string RetFileNamePath(string Folder, string filename)
    {
        string strret = "javascript:void(0)";
        try
        {
            if (filename != "")
            {
                strret = "../incentives/Files/" + Folder + "/" + filename;
            }
            else
            {

            }
        }
        catch (Exception)
        {
        }
        return strret;
    }
    private void SMSEMailContent()
    {
        try
        {
            CommonHelperCls objcomm = new CommonHelperCls();
            string strSubject = "GOSwift: Application submitted successfully";
            string PreviewURL = System.Configuration.ConfigurationManager.AppSettings["PreviewURL"];

            //StringBuilder strBldrApplyLeave = new StringBuilder();
            //strBldrApplyLeave.Append("<table>");
            //strBldrApplyLeave.Append("<tr><td>" + lblTitle.Text + " of M/s " + lblMr.Text  +  " has been submitted successfully." + "</td></tr>");
            //strBldrApplyLeave.Append("<tr><td> Log into www.swp.investodisha.gov.in for further details. </td></tr>");
            //strBldrApplyLeave.Append("</table>");
            //string strBody = strBldrApplyLeave.ToString();

            string strBody = lblTitle.Text + " of M/s " + lblName.Text + " has been submitted successfully. " + PreviewURL;
            string SMSContent = strSubject;

            var myList = new List<string>();
            myList.Add(hdnEmail.Value);
            string[] tomail = myList.ToArray();

            bool mailStatus = objcomm.sendMail(strSubject, strBody, tomail, true);
            bool smsStatus = objcomm.SendSmsNew(hdnMobile.Value, SMSContent);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Incentive objEntity = new Incentive();
        try
        {
            string[] fileext = { ".png", ".jpg", ".jpeg" };
            if (flSignature.HasFile)
            {
                if (fileext.Contains(System.IO.Path.GetExtension(flSignature.FileName).ToLower()))
                {
                    bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/Signature/"));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath("../incentives/Files/Signature/"));

                    string extension = Path.GetExtension(flSignature.PostedFile.FileName);
                    string filename = "Signature" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
                    flSignature.SaveAs(Server.MapPath("../incentives/Files/Signature/") + filename);
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

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Saved Successfully !', 'SWP');window.location.href='appliedlistwithdetails.aspx'; </script>", false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + Messages.ShowMessage("1") + "');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Upload Signature. !', 'SWP'); </script>", false);
            }


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
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
    void crdtincentive1()
    {
        DataTable dtincentive1 = new DataTable();



        DataColumn dcRowId = new DataColumn("dcRowId");
        dcRowId.DataType = Type.GetType("System.Int32");
        dcRowId.AutoIncrement = true;
        dcRowId.AutoIncrementSeed = 1;
        dcRowId.AutoIncrementStep = 1;
        dtincentive1.Columns.Add(dcRowId);

        DataColumn vchagency = new DataColumn("vchagency");
        vchagency.DataType = Type.GetType("System.String");
        dtincentive1.Columns.Add(vchagency);

        DataColumn vchsacamt = new DataColumn("vchsacamt");
        vchsacamt.DataType = Type.GetType("System.String");
        dtincentive1.Columns.Add(vchsacamt);

        DataColumn vchsacord = new DataColumn("vchsacord");
        vchsacord.DataType = Type.GetType("System.String");
        dtincentive1.Columns.Add(vchsacord);

        DataColumn vchsacdat = new DataColumn("vchsacdat");
        vchsacdat.DataType = Type.GetType("System.String");
        dtincentive1.Columns.Add(vchsacdat);

        DataColumn vchavilamt = new DataColumn("vchavilamt");
        vchavilamt.DataType = Type.GetType("System.String");
        dtincentive1.Columns.Add(vchavilamt);



        ViewState["dtincentive1"] = dtincentive1;
        grdIncentiveAvailed.DataSource = dtincentive1;
        grdIncentiveAvailed.DataBind();
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
            TrDisplayNone(Lbl_Org_Doc_Type.Text, tr_Org_Doc_Type);
            TrDisplayNone(Lbl_Prod_Comm_Before_Doc_Name.Text, tr_Prod_Comm_Before);
            TrDisplayNone(Lbl_Prod_Comm_After_Doc_Name.Text, tr_Prod_Comm_After_Doc_Name);
            TrDisplayNone(Lbl_Pioneer_Doc_Name.Text, DivPioneer);
            TrDisplayNone(Lbl_Direct_Emp_Before_Doc_Name.Text, divEmp_Before_Doc_Name);
            TrDisplayNone(Lbl_Unit_Type_Doc_Name.Text, Div_Unit_Type_Doc);
            TrDisplayNone(Lbl_Direct_Emp_After_Doc_Name.Text, tr_Direct_Emp_After_Doc_Name);
            TrDisplayNone(Lbl_FFCI_Before_Doc_Name.Text, FFCI_Before_Doc_Name);
            TrDisplayNone(Lbl_Approved_DPR_Before_Doc_Name.Text, Approved_DPR_Before_Doc_Name);
            TrDisplayNone(Lbl_FFCI_After_Doc_Name.Text, tr_FFCI_After_Doc_Name);
            TrDisplayNone(Lbl_Term_Loan_Doc_Name.Text, tr_Term_Loan_Doc_Name);
            TrDisplayNone(Lbl_Approved_DPR_After_Doc_Name.Text, tr_Approved_DPR_After_Doc_Name);
            TrDisplayNone(lblAvailDoc.Text, trAvailDoc);
            TrDisplayNone(lblBank.Text, trBank);


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