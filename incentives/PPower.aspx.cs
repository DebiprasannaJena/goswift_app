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

public partial class incentives_EmploymentRatingPP : SessionCheck
{
    string MsgTitle = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            PostpopulateData();
            fillPage();
            TRVisibility();

            ////lbl_EnterPrise_Name1.Text = LblDist.Text;
            ////LblGetdate.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
        }
    }

    #region Populate Common-Field Pre/Post

    #region viewdetail
    public void PostpopulateData()
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

            DataSet dslivePre = IncentiveManager.PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            DataTable dtindustryView = dslivePre.Tables[0];////////////industry panel
            DataTable dtProductionView = dslivePre.Tables[1];///////////production & employment

            DataTable dtProductionDetBefView = dslivePre.Tables[2];///////////production & employment Before
            DataTable dtProductionDetAftView = dslivePre.Tables[3];///////////production & employment After
            DataTable dtInvestmentView = dslivePre.Tables[4];///////////investment details
            DataTable dtMeansFinanceView = dslivePre.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanView = dslivePre.Tables[6];///////////Means of Finance Term Loan
            DataTable dtMoFWorkingLoanView = dslivePre.Tables[7];///////////Means of Finance Working Loan

            //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();

            #region IndustrialUnit
            try
            {
                if (dtindustryView.Rows.Count > 0)
                {
                    lbl_EnterPrise_Name.Text = dtindustryView.Rows[0]["vchEnterpriseName"].ToString();
                    lbl_EnterPrise_Name1.Text = lbl_EnterPrise_Name.Text;
                    //dtindustryView.Rows[0]["intOrganisationType"].ToString();	
                    DataSet ds1 = new DataSet();
                    ds1 = IncentiveManager.dynamic_name_doc_bind();
                    ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryView.Rows[0]["intUnitType"].ToString() + "'";
                    ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryView.Rows[0]["intOrganisationType"].ToString() + "'";
                    DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
                    if (dt.Rows.Count > 0)
                    {
                        Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                        Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                        // Lbl_Org_Doc_Type.Text = dt.Rows[0]["vch_orgdocumentname"].ToString();
                        //lblAuthorizing.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();               

                    }
                    else
                    {
                        Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                        //Hid_Org_Doc_Type.Value = "";
                    }



                    lbl_Org_Type.Text = dtindustryView.Rows[0]["OrgTypename"].ToString();
                    lbl_Industry_Address.Text = dtindustryView.Rows[0]["vchIndustryAddress"].ToString();
                    lblAddress.Text = lbl_Industry_Address.Text;
                    lblAddress1.Text = lbl_Industry_Address.Text;
                    //dtindustryView.Rows[0]["intUnitCat"].ToString();		
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
                    //dtindustryView.Rows[0]["vchManagingPartnerGender"].ToString();	
                    //if (dtindustryView.Rows[0]["GenderType"].ToString() == "1")
                    //{
                    //    lbl_Gender_Partner.Text = "Mr." + dtindustryView.Rows[0]["vchManagingPartnerName"].ToString();
                    //}
                    //else
                    //{
                    //    lbl_Gender_Partner.Text = "Ms." + dtindustryView.Rows[0]["vchManagingPartnerName"].ToString();
                    //}
                    lbl_Gender_Partner.Text = dtindustryView.Rows[0]["GenderType"].ToString() + " " + dtindustryView.Rows[0]["vchManagingPartnerName"].ToString();

                    //dtindustryView.Rows[0]["vchManagingPartnerName"].ToString();
                    Lbl_Org_Doc_Type.Text = dtindustryView.Rows[0]["CertOfRegdDocName"].ToString();
                    Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryView.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                    //dtindustryView.Rows[0]["vchCertOfRegdDocCode"].ToString();		


                    //dtindustryView.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                    lbl_EIN_IL_NO.Text = dtindustryView.Rows[0]["vchEINNO"].ToString();
                    lbl_EIN_IL_Date.Text = dtindustryView.Rows[0]["dtmEIN"].ToString();
                    lbl_PC_No.Text = dtindustryView.Rows[0]["vchPcNo"].ToString();
                    lbl_Prod_Comm_Date_Before.Text = dtindustryView.Rows[0]["dtmProdCommBefore"].ToString();
                    lbl_PC_Issue_Date_Before.Text = dtindustryView.Rows[0]["dtmPCIssueDateBefore"].ToString();
                    //dtindustryView.Rows[0]["vchProdCommCertBeforeCode"].ToString();	
                    //Lbl_Prod_Comm_Before_Doc_Name.Text = dtindustryView.Rows[0]["ProdCommCertBeforeDocName"].ToString();
                    //Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryView.Rows[0]["vchProdCommCertBefore"].ToString();
                    //dtindustryView.Rows[0]["vchProdCommCertBefore"].ToString();

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
                    //dtindustryView.Rows[0]["vchProdCommCertAfterCode"].ToString();		
                    //Lbl_Prod_Comm_After_Doc_Name.Text = dtindustryView.Rows[0]["ProdCommCertAfterDocName"].ToString();
                    //Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryView.Rows[0]["vchProdCommCertAfter"].ToString();
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
                    //dtindustryView.Rows[0]["intCreatedBy"].ToString();


                    divadhhardetails.Visible = false;
                    divAuthorizing.Visible = false;
                    LblAadhaar.Text = dtindustryView.Rows[0]["VCHAADHAARNO"].ToString().Trim();
                    lblApplyBy.Text = dtindustryView.Rows[0]["INTAPPLYBY"].ToString() == "1" ? "Self" : "Authorized Person";
                    if (dtindustryView.Rows[0]["INTAPPLYBY"].ToString() == "1")
                    {
                        divadhhardetails.Visible = true;
                       // lblauthority.Text = "Self";
                    }
                    else
                    {
                        if (dtindustryView.Rows[0]["VCHAUTHORIZEDFILECODE"].ToString() != "")
                        {
                            //lblauthority.Text = dtindustryView.Rows[0]["VCHAUTHORIZEDFILECODEName"].ToString();
                            divAuthorizing.Visible = true;
                            hypAUTHORIZEDFILE.NavigateUrl = RetFilePath(dtindustryView.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc");
                        }
                    }
                    //if (dtindustryView.Rows[0]["INTGENDER"].ToString() == "1")
                    //{
                    //    LblApplicantName.Text = "Mr." + dtindustryView.Rows[0]["VCHAPPLICANTNAME"].ToString();
                    //}
                    //else
                    //{
                    //    LblApplicantName.Text = "Ms." + dtindustryView.Rows[0]["VCHAPPLICANTNAME"].ToString();
                    //}
                    LblApplicantName.Text = dtindustryView.Rows[0]["VCHPREAPPLICANTNAME"].ToString();


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

                Grd_Production_Before.DataSource = dtProductionDetBefView;
                Grd_Production_Before.DataBind();
                Grd_Production_After.DataSource = dtProductionDetAftView;
                Grd_Production_After.DataBind();


                //dtProductionView.Rows[0]["intProductionId"].ToString();
                lbl_Direct_Emp_Before.Text = dtProductionView.Rows[0]["intDirectEmpBefore"].ToString();
                lbl_Contract_Emp_Before.Text = dtProductionView.Rows[0]["intContractualEmpBefore"].ToString();
                Hyp_View_Direct_Emp_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionView.Rows[0]["VCHEMPDOC"].ToString();
                lbl_Managarial_Before.Text = dtProductionView.Rows[0]["INTPROPOSEDMANAGERIAL"].ToString();
                lbl_Supervisor_Before.Text = dtProductionView.Rows[0]["INTPROPOSEDSUPERVISORY"].ToString();
                lbl_Skilled_Before.Text = dtProductionView.Rows[0]["INTPROPOSEDSKILLED"].ToString();
                lbl_Semi_Skilled_Before.Text = dtProductionView.Rows[0]["INTPROPOSEDSEMISKILLED"].ToString();
                lbl_Unskilled_Before.Text = dtProductionView.Rows[0]["INTPROPOSEDUNSKILLED"].ToString();
                lbl_Total_Emp_Before.Text = dtProductionView.Rows[0]["INTPROPOSEDTOTAL"].ToString();
                lbl_General_Before.Text = dtProductionView.Rows[0]["intGeneralBefore"].ToString();
                lbl_SC_Before.Text = dtProductionView.Rows[0]["intSCBefore"].ToString();
                lbl_ST_Before.Text = dtProductionView.Rows[0]["intSTBefore"].ToString();
                lbl_Total_Cast_Emp_Before.Text = dtProductionView.Rows[0]["intTotalEmpCastBefore"].ToString();
                lbl_Women_Before.Text = dtProductionView.Rows[0]["intWomenBefore"].ToString();
                lbl_PHD_Before.Text = dtProductionView.Rows[0]["intDisabledBefore"].ToString();
                lbl_Direct_Emp_After.Text = dtProductionView.Rows[0]["intDirectEmpAfter"].ToString();
                lbl_Contract_Emp_After.Text = dtProductionView.Rows[0]["intContractualEmpAfter"].ToString();
                //dtProductionView.Rows[0]["vchEmpDocBeforeCode"].ToString();			
                Lbl_Direct_Emp_Before_Doc_Name.Text = dtProductionView.Rows[0]["vchEmpDocBeforeCodeName"].ToString();
                Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionView.Rows[0]["vchEmpDocAfter"].ToString();
                //dtProductionView.Rows[0]["vchEmpDocAfterCode"].ToString();			
                Lbl_Direct_Emp_After_Doc_Name.Text = dtProductionView.Rows[0]["vchEmpDocAfterCodeName"].ToString();


                lbl_Managarial_After.Text = dtProductionView.Rows[0]["INTCURRENTMANAGERIAL"].ToString();
                lbl_Supervisor_After.Text = dtProductionView.Rows[0]["INTCURRENTSUPERVISORY"].ToString();
                lbl_Skilled_After.Text = dtProductionView.Rows[0]["INTCURRENTSKILLED"].ToString();
                lbl_Semi_Skilled_After.Text = dtProductionView.Rows[0]["INTCURRENTSEMISKILLED"].ToString();
                lbl_Unskilled_After.Text = dtProductionView.Rows[0]["INTCURRENTUNSKILLED"].ToString();
                lbl_Total_Emp_After.Text = dtProductionView.Rows[0]["INTCURRENTTOTAL"].ToString();
                lbl_General_After.Text = dtProductionView.Rows[0]["intGeneralAfter"].ToString();
                lbl_SC_After.Text = dtProductionView.Rows[0]["intSCAfter"].ToString();
                lbl_ST_After.Text = dtProductionView.Rows[0]["intSTAfter"].ToString();
                lbl_Total_Cast_Emp_After.Text = dtProductionView.Rows[0]["intTotalEmpCastAfter"].ToString();
                lbl_Women_After.Text = dtProductionView.Rows[0]["intWomenAfter"].ToString();
                lbl_PHD_After.Text = dtProductionView.Rows[0]["intDisabledAfter"].ToString();

            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Incentive");
            }
            //dtProductionView.Rows[0]["intCreatedBy"].ToString();

            #endregion

            #region Investment

            //,vchFFCIDocBeforeCode,,,,,
            //    ,,INT_INCUNQUEID,vchProjectDocBeforeCode,vchProjectDocBefore,dtmFFCIDateAfter,
            //    vchFFCIDocAfterCode,vchFFCIDocAfter,decLandAmtAfter,decBuildingAmtAfter,decPlantMachAmtAfter,decOtheFixedAssetAmtAfter,
            //    decTotalAmtAfter,vchProjectDocAfterCode,vchProjectDocAfter,INT_CREATED_BY,DTM_CREATEDON

            //dtInvestmentView.Rows[0]["slno"].ToString();
            Txt_FFCI_Date_Before.Text = dtInvestmentView.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString();
            //dtInvestmentView.Rows[0]["vchFFCIDocBeforeCode"].ToString();
            Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentView.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();
            Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentView.Rows[0]["VCH_Document_in_support"].ToString();
            lbl_Land_Before.Text = dtInvestmentView.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
            lbl_Building_Before.Text = dtInvestmentView.Rows[0]["DEC_Building"].ToString();
            lbl_Plant_Mach_Before.Text = dtInvestmentView.Rows[0]["DEC_Plant_Machinery"].ToString();
            lbl_Other_Fixed_Asset_Before.Text = dtInvestmentView.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
            lbl_Total_Capital_Before.Text = dtInvestmentView.Rows[0]["DEC_Total"].ToString();
            //dtInvestmentView.Rows[0]["vchProjectDocBeforeCode"].ToString();			
            Lbl_Approved_DPR_Before_Doc_Name.Text = dtInvestmentView.Rows[0]["vchProjectDocBeforeCodeName"].ToString();
            Hyp_View_Approved_DPR_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentView.Rows[0]["vchProjectDocBefore"].ToString();
            lbl_FFCI_Date_After.Text = dtInvestmentView.Rows[0]["dtmFFCIDateAfter"].ToString();
            //dtInvestmentView.Rows[0]["vchFFCIDocAfterCode"].ToString();			
            Lbl_FFCI_After_Doc_Name.Text = dtInvestmentView.Rows[0]["vchFFCIDocAfterCodeName"].ToString();
            Hyp_View_FFCI_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentView.Rows[0]["vchFFCIDocAfter"].ToString();

            lbl_Land_After.Text = dtInvestmentView.Rows[0]["decLandAmtAfter"].ToString();
            lbl_Building_After.Text = dtInvestmentView.Rows[0]["decBuildingAmtAfter"].ToString();
            lbl_Plant_Mach_After.Text = dtInvestmentView.Rows[0]["decPlantMachAmtAfter"].ToString();
            lbl_Other_Fixed_Asset_After.Text = dtInvestmentView.Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
            lbl_Total_Capital_After.Text = dtInvestmentView.Rows[0]["decTotalAmtAfter"].ToString();


            //dtInvestmentView.Rows[0]["vchProjectDocAfterCode"].ToString();			
            Lbl_Approved_DPR_After_Doc_Name.Text = dtInvestmentView.Rows[0]["vchProjectDocAfterCodeName"].ToString();
            Hyp_View_Approved_DPR_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentView.Rows[0]["vchProjectDocAfter"].ToString();
            //dtInvestmentView.Rows[0]["intCreatedBy"].ToString();

            #endregion

            #region MEANS OF FINANCE

            //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
            lbl_Equity_Amt.Text = dtMeansFinanceView.Rows[0]["decEquity"].ToString();
            lbl_Loan_Bank_FI.Text = dtMeansFinanceView.Rows[0]["decLoanBankFI"].ToString();
            Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinanceView.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();
            lbl_FDI_Componet.Text = dtMeansFinanceView.Rows[0]["decFDIComponet"].ToString();
            //dtMeansFinancePre.Rows[0]["intCreatedBy"].ToString();
            //dtMeansFinancePre.Rows[0]["vchTermLoanDocCode"].ToString();
            Lbl_Term_Loan_Doc_Name.Text = dtMeansFinanceView.Rows[0]["vchTermLoanDocCodeNAme"].ToString();



            if (dtMoFTermLoanView.Rows.Count > 0)
            {
                Grd_TL.DataSource = dtMoFTermLoanView;
                Grd_TL.DataBind();
            }

            if (dtMoFWorkingLoanView.Rows.Count > 0)
            {
                Grd_WC.DataSource = dtMoFWorkingLoanView;
                Grd_WC.DataBind();
            }
            #endregion
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #endregion

    #endregion
    public void fillPage()
    {
        try
        {
            int tryint;
            Incentive incentive = new Incentive();
            incentive.UnqIncentiveId = int.TryParse(Request.QueryString["InctUniqueNo"], out tryint) ? tryint : tryint;


            DataSet ds = IncentiveManager.GetIncentivePower(incentive);

            DataTable dtindustryView = ds.Tables[0];
            DataTable dtPower = ds.Tables[1];
            DataTable dtBank = ds.Tables[2];
            DataTable dtADDDocument = ds.Tables[3];

            DataTable dtMainTable = ds.Tables[5];///////////M_INCT_APPLICATION Table
            DataTable dtInvstInfo = ds.Tables[6];///////////M_INVESTOR_DETAILS Table
            DataTable dtApplyStatusOrFinalSaveStatus = ds.Tables[7];



            if (dtindustryView.Rows.Count > 0)
            {
                fillIndustrialModuleControls(dtindustryView);
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

            #region Header-Footer Text
            ////LblName.Text = dtindustryView.Rows[0]["VCHAPPLICANTNAME"].ToString();
            //LblAddress.Text = lbl_Industry_Address.Text;
            LblDist.Text = lbl_District.Text;
            LblDist2.Text = lbl_District.Text;

            LblNameFooter.Text = LblApplicantName.Text;// dtindustryView.Rows[0]["VCHAPPLICANTNAME"].ToString();
            LblIndustryUnit.Text = lbl_EnterPrise_Name.Text;
            LblMSName.Text = LblIndustryUnit.Text;

            ////string creatby = dtMainTable.Rows[0]["INTCREATEDBY"].ToString();
            ////if (creatby != Convert.ToString(Session["InvestorId"]))
            ////{
            ////}
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
            string incentiveNo = dtMainTable.Rows[0]["VCHINCENTIVENO"].ToString();
            LblGetdate.Text = dtMainTable.Rows[0]["DTMCREATEDBY"].ToString();
            #endregion
            #region Title Text
            try
            {
                Incentive objIncentive = new Incentive();
                objIncentive.strcActioncode = "M";
                objIncentive.IncentiveNum = Convert.ToString(incentiveNo);
                DataSet ds2 = new DataSet();
                ds2 = IncentiveManager.GetIncentiveMaster(objIncentive);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    DataTable dtbPostSubFlag = ds2.Tables[0];
                    lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Incentive");
            }
            #endregion

            HdnMobNo.Value = dtInvstInfo.Rows[0]["VCH_OFF_MOBILE"].ToString();
            HdnEmail.Value = dtInvstInfo.Rows[0]["VCH_EMAIL"].ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    } 

    #region fill Module_Controls
    private void fillIndustrialModuleControls(DataTable dtindustry)
    {

        try
        {
            if (dtindustry.Rows.Count > 0)
            {

                divadhhardetails.Visible = false;
                divAuthorizing.Visible = false; 

                LblApplicantName.Text = dtindustry.Rows[0]["VCHPREAPPLICANTNAME"].ToString();
                if (dtindustry.Rows[0]["INTAPPLYBY"].ToString() == "1")
                {
                    divadhhardetails.Visible = true;
                    lblauthority.Text = "Self";
                }
                else
                {
                    if (dtindustry.Rows[0]["VCHAUTHORIZEDFILECODE"].ToString() != "")
                    {
                        divAuthorizing.Visible = true;
                        hypAUTHORIZEDFILE.NavigateUrl = RetFilePath(dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc");
                    }
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
            LblNewSP.Text = dtPower.Rows[0]["decNewInvestment_SchematicProvisions"].ToString();//NewInvestment_SchematicProvisions
            LblNewCP.Text = dtPower.Rows[0]["decNewInvestment_TillDateOfCommencementOfProduction"].ToString(); //NewInvestment_TillDateOfCommencementOfProduction
            LblNewJR.Text = dtPower.Rows[0]["vchNewInvestment_reasons"].ToString();//NewInvestment_reasons

            LblLandSP.Text = dtPower.Rows[0]["decLand_SchematicProvisions"].ToString();//Land_SchematicProvisions
            LblLandCP.Text = dtPower.Rows[0]["decLand_TillDateOfCommencementOfProduction"].ToString(); //Land_TillDateOfCommencementOfProduction
            LblLandJR.Text = dtPower.Rows[0]["vchLand_reasons"].ToString();//Land_reasons

            LblBuildSP.Text = dtPower.Rows[0]["decBuilding_SchematicProvisions"].ToString();//Building_SchematicProvisions
            LblBuildCP.Text = dtPower.Rows[0]["decBuilding_TillDateOfCommencementOfProduction"].ToString();//Building_TillDateOfCommencementOfProduction
            LblBuildJR.Text = dtPower.Rows[0]["vchBuilding_reasons"].ToString();//Building_reasons

            LblPMSP.Text = dtPower.Rows[0]["decPlantMachinery_SchematicProvisions"].ToString();//PlantMachinery_SchematicProvisions
            LblPMCP.Text = dtPower.Rows[0]["decPlantMachinery_TillDateOfCommencementOfProduction"].ToString();//PlantMachinery_TillDateOfCommencementOfProduction
            LblPMJR.Text = dtPower.Rows[0]["vchPlantMachinery_reasons"].ToString();//PlantMachinery_reasons

            LblOFASP.Text = dtPower.Rows[0]["decOtherFixedAssets_SchematicProvisions"].ToString();//OtherFixedAssets_SchematicProvisions
            LblOFACP.Text = dtPower.Rows[0]["decOtherFixedAssets_TillDateOfCommencementOfProduction"].ToString();//OtherFixedAssets_TillDateOfCommencementOfProduction
            LblOFAJR.Text = dtPower.Rows[0]["vchOtherFixedAssets_reasons"].ToString();//OtherFixedAssets_reasons

            LblElectSP.Text = dtPower.Rows[0]["decElectricalInstallations_SchematicProvisions"].ToString();//ElectricalInstallations_SchematicProvisions
            LblElectCP.Text = dtPower.Rows[0]["decElectricalInstallations_TillDateOfCommencementOfProduction"].ToString();//ElectricalInstallations_TillDateOfCommencementOfProduction
            LblElectJR.Text = dtPower.Rows[0]["vchElectricalInstallations_reasons"].ToString();//ElectricalInstallations_reasons

            LblJustExc.Text = dtPower.Rows[0]["vchJustificationForExcessInvestment"].ToString();//JustificationForExcessInvestment

            LblTotUnit.Text = dtPower.Rows[0]["vchTotalUnitConsumed"].ToString();//TotalUnitConsumed

            LblTotAmtPaid.Text = dtPower.Rows[0]["decAmountPaid"].ToString();//AmountPaid


            if (dtPower.Rows[0]["vch_MoneyReceiptFile"].ToString() != "")
            {
                HypPowerTariff.NavigateUrl = RetFilePath(dtPower.Rows[0]["vch_MoneyReceiptFile"].ToString(), "PowerTariff");
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
            LblBnkNm.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
            LblBranch.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
            LblIFSC.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
            LblAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
            LblMICRNo.Text = dtBank.Rows[0]["VCHMICR"].ToString();
            if (dtBank.Rows[0]["vchBankDoc"].ToString() != "")
            {
                HypSampBankDoc.NavigateUrl = RetFilePath(dtBank.Rows[0]["vchBankDoc"].ToString(), "Bank");
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
            if (dtADDDocument.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString() != "")
            {
                HypValidStatutary.NavigateUrl = RetFilePath(dtADDDocument.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString(), "AdditionalDocument");
            }

            if (dtADDDocument.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString() != "")
            {
                HypDelay.NavigateUrl = RetFilePath(dtADDDocument.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString(), "AdditionalDocument");
            }
            if (dtADDDocument.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString() != "")
            {
                HypCleanApproveAuthority.NavigateUrl = RetFilePath(dtADDDocument.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString(), "AdditionalDocument");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
     

    #region Button Click
    protected void btnFinalSave_Click(object sender, EventArgs e)
    {
        CommonHelperCls comm = new CommonHelperCls();
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please upload .png/.jpg/.jpeg format image only !', '" + MsgTitle + "'); </script>", false);
                    return;
                }
                objEntity.ApprovalAction = "A";
                objEntity.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
                int i = IncentiveManager.UpdateSignature(objEntity);
                if (HdnMobNo.Value.ToString() != "")
                {
                    comm.SendSmsNew(HdnMobNo.Value.ToString(), "GO-Swift! Application submitted successfully.");
                }
                if (HdnEmail.Value.ToString() != "")
                {
                    string strSubject = "Go-Swift! Application Submission.";
                    string PreviewURL = System.Configuration.ConfigurationManager.AppSettings["PreviewURL"];
                    string strBody = "The application for " + lblTitle.Text + " of M/s " + lbl_EnterPrise_Name.Text + " has been submitted successfully." + Environment.NewLine + PreviewURL;
                    string[] arramail = new string[] { HdnEmail.Value };
                    comm.sendMail(strSubject, strBody, arramail, true);
                }
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Saved Successfully !', 'SWP');window.location.href='appliedlistwithdetails.aspx'; </script>", false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + Messages.ShowMessage("1") + "');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Upload Signature. !', '" + MsgTitle + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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