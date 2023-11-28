using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using EntityLayer.Incentive;


public partial class incentives_FormPreview_AnchorTenant : SessionCheck
{
    #region Common Member
    Incentive objIncentive = new Incentive();
    string MsgTitle = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            FillFormDetails(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            ////lblDate.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            TRVisibility();
        }

    }
    #region Post Populate
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


        #region Industrial Unit

        if (dtindustryPre.Rows.Count > 0)
        {
            lblUnitAddress.Text = dtindustryPre.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
            Label101.Text = dtindustryPre.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
            lblMr.Text = dtindustryPre.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
            lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();

            DataSet ds1 = new DataSet();
            ds1 = IncentiveManager.dynamic_name_doc_bind();
            ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intUnitType"].ToString() + "'";
            ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intOrganisationType"].ToString() + "'";
            DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
            if (dt.Rows.Count > 0)
            {
                Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
            }
            else
            {
                Lbl_Org_Name_Type.Text = "Name of Managing Partner";
            }

            lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
            lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
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
                Lbl_Pioneer_Doc_Name.Text = "";
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
            TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHPREAPPLICANTNAME"].ToString().Trim();


            lblName.Text = dtindustryPre.Rows[0]["VCHPREAPPLICANTNAME"].ToString().Trim();
            lbl_Regd_Office_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();
            //if (dtindustryPre.Rows[0]["GenderType"].ToString() == "1")
            //{
            //    lbl_Gender_Partner.Text = "Mr. " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            //}
            //else
            //{
            //    lbl_Gender_Partner.Text = "Ms. " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            //}

            lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            lbl_Regd_Office_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();

            lblApplyBy.Text = (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1") ? "Self" : "Authorized Person";

            divadhhardetails.Visible = false;
            divAuthorizing.Visible = false;
            DivAppDocType.Visible = false;
            LblAadhaar.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim();
            lblApplyBy.Text = dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1" ? "Self" : "Authorized Person";
            if (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1")
            {
                divadhhardetails.Visible = true;
                //lblauthority.Text = "Applicant";
            }
            else
            {
                divAuthorizing.Visible = true;
                DivAppDocType.Visible = false;
                lblinstMultiselect.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
                LnkViewMultiselectDoc.Attributes.Add("href", RetFileNamePath(dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc"));
                lblDocumentType.Text = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
            }
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
                FFCI_Before_Doc_Name.Visible = true;
                Approved_DPR_Before_Doc_Name.Visible = true;

            }
            else
            {
                divbefor.Visible = false;
                divbefor1.Visible = false;
                divbefor2.Visible = false;
                //tr_Prod_Comm_Before.Visible = false; Lbl_Prod_Comm_Before_Doc_Name.Text = "";
                divEmp_Before_Doc_Name.Visible = false; Lbl_Direct_Emp_Before_Doc_Name.Text = "";
                FFCI_Before_Doc_Name.Visible = false; Lbl_FFCI_Before_Doc_Name.Text = "";
                Approved_DPR_Before_Doc_Name.Visible = false; Lbl_Approved_DPR_Before_Doc_Name.Text = "";
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
                Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
            }
            else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            {
                Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnoAft"].ToString();
            }

            lbl_District.Text = dtindustryPre.Rows[0]["distname"].ToString();
            lblDist.Text = dtindustryPre.Rows[0]["distname"].ToString();

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
        #endregion
        #region Investment
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
        #endregion
        #region MEANS OF FINANCE
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
        #endregion
    }
    #endregion
    #region Fill Form Details
    public void FillFormDetails(int id)
    {
        try
        {
            objIncentive = new Incentive();
            objIncentive.GetVwPrmtrs = new GetAndViewParam();
            objIncentive.GetVwPrmtrs.Param1ID = "";
            objIncentive.GetVwPrmtrs.Param2ID = "";
            objIncentive.GetVwPrmtrs.Param3ID = "";
            objIncentive.GetVwPrmtrs.Param4ID = "";
            objIncentive.GetVwPrmtrs.InctType = 4;
            objIncentive.UnqIncentiveId = id;
            objIncentive.FormType = FormNumber.AnchorTenant_17;
            DataSet dslive = IncentiveManager.GetIncentiveAnchorTenant(objIncentive);

            DataTable dtDLSWCA = dslive.Tables[2];
            FillDLSWCADetails(dtDLSWCA);
            DataTable dtBankDetails = dslive.Tables[3];
            FillBankDetails(dtBankDetails, "0");
            DataTable dtBriefDetails = dslive.Tables[0];
            DataTable dtBriefDetailsDtl = dslive.Tables[1];
            FillBriefDeatils(dtBriefDetails, dtBriefDetailsDtl);
            DataTable dtMainTable = dslive.Tables[5];
            DataTable dtInvstInfo = dslive.Tables[6];///////////M_INVESTOR_DETAILS Table

            #region Menu/Panel
            //string creatby = dtMainTable.Rows[0]["INTCREATEDBY"].ToString();
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
            string incentiveNo = dtMainTable.Rows[0]["VCHINCENTIVENO"].ToString();
            lblDate.Text = RetDateFrmDB(dtMainTable.Rows[0]["DTMCREATEDBY"].ToString());
            #endregion

            #region Title
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
            #endregion


            HdnMobNo.Value = dtInvstInfo.Rows[0]["VCH_OFF_MOBILE"].ToString();
            HdnEmail.Value = dtInvstInfo.Rows[0]["VCH_EMAIL"].ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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
                lknViewDtlOfSecTnt.NavigateUrl = "../incentives/Files/BriefDetailsProposed/" + dtBriefDetails.Rows[0]["vchDtlOfSecondTnt"].ToString();

            }
            if (dtBriefDetails.Rows[0]["vchdtlAttractSecndTnt"].ToString() != "")
            {
                lnkDtlBusnessPlanView.NavigateUrl = "../incentives/Files/BriefDetailsProposed/" + dtBriefDetails.Rows[0]["vchdtlAttractSecndTnt"].ToString();

            }

            if (dtBriefDetails.Rows[0]["vchConsetSecndTnt"].ToString() != "")
            {
                lnkConsetSecView.NavigateUrl = "../incentives/Files/BriefDetailsProposed/" + dtBriefDetails.Rows[0]["vchConsetSecndTnt"].ToString();
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
                lnkDLSWCAApprovalDocView.NavigateUrl = "../incentives/Files/DLSWCA/" + dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString();
                //lknApprovalDocUpload.Text = dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString();
            }
            if (dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString() != "")
            {
                lnkDLSWCASubstanDocView.NavigateUrl = "../incentives/Files/DLSWCA/" + dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString();
                // lknSubstanDocUpload.Text = dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString();
            }
        }

    }
    #endregion
    #region Fill Bank Details
    public void FillBankDetails(DataTable dtBankDetails, string strStatus)
    {
        try
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
                    HypSampBankDoc.NavigateUrl = RetFileNamePath(dtBankDetails.Rows[0]["vchBankDoc"].ToString(), "Bank");
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #endregion
    protected void btnApply_Click(object sender, EventArgs e)
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
    public string RetFileNamePath(string filename, string foldername)
    {
        string strret = "";
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
                retdt = dbdt.ToString("dd-MMM-yyyy");

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return retdt;
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
            //TrDisplayNone(Lbl_Prod_Comm_Before_Doc_Name.Text, tr_Prod_Comm_Before);
            TrDisplayNone(Lbl_Prod_Comm_After_Doc_Name.Text, tr_Prod_Comm_After_Doc_Name);
            TrDisplayNone(Lbl_Pioneer_Doc_Name.Text, DivPioneer);
            TrDisplayNone(Lbl_Direct_Emp_Before_Doc_Name.Text, divEmp_Before_Doc_Name);
            TrDisplayNone(Lbl_Unit_Type_Doc_Name.Text, Div_Unit_Type_Doc);
            TrDisplayNone(Lbl_Direct_Emp_After_Doc_Name.Text, tr_Direct_Emp_After_Doc_Name);
            TrDisplayNone(Lbl_FFCI_Before_Doc_Name.Text, FFCI_Before_Doc_Name);
            TrDisplayNone(Lbl_Approved_DPR_After_Doc_Name.Text, tr_Approved_DPR_After_Doc_Name);
            TrDisplayNone(Lbl_Approved_DPR_Before_Doc_Name.Text, Approved_DPR_Before_Doc_Name);
            TrDisplayNone(Lbl_FFCI_After_Doc_Name.Text, tr_FFCI_After_Doc_Name);
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