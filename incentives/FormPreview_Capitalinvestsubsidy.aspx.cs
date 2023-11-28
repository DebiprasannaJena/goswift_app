using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.Collections.Generic;

public partial class incentives_Capitalinvestsubsidy : SessionCheck
{
    decimal dPageTotal = 0;
    DataTable dtSalutation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillData();
            TRVisibility();
        }
    }

    protected void FillData()
    {
        Grd_TL.DataSource = null;
        Grd_TL.DataBind();
        Grd_WC.DataSource = null;
        Grd_WC.DataBind();
        Incentive objIncentive = new Incentive();
        GetAndViewParam objPar = new GetAndViewParam();
        objIncentive.strcActioncode = "action";
        objPar.Param1ID = Convert.ToString(Request.QueryString["IncentiveNo"]); //IncentiveNum
        objPar.Param2ID = Convert.ToString(Session["UnitCode"]);   //UnitCode
        objPar.Param3ID = Convert.ToString(Session["ProposalNo"]);   //ProposalNum


        objPar.InctType = 4;
        objIncentive.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
        objIncentive.GetVwPrmtrs = objPar;
        objIncentive.FormType = FormNumber.CapitalInvestmentZld_18;


        DataSet ds = IncentiveManager.PostpopulateCapSubsidy(objIncentive);

        DataTable dtindustryPre = ds.Tables[0];          //industry panel
        DataTable dtProductionPre = ds.Tables[1];        //production & employment
        DataTable dtProductionDetBefPre = ds.Tables[2];  //production & employment Before
        DataTable dtProductionDetAftPre = ds.Tables[3];  //production & employment After
        DataTable dtInvestmentPre = ds.Tables[4];        //investment details
        DataTable dtMeansFinancePre = ds.Tables[5];      //Means of Finance
        DataTable dtMoFTermLoanPre = ds.Tables[6];       //Means of Finance Term Loan
        DataTable dtMoFWorkingLoanPre = ds.Tables[7];    //Means of Finance Working Loan
        DataTable dtInvestmentPollution = ds.Tables[8];
        DataTable dtInvestmentPollutionDtl = ds.Tables[9];
        DataTable dtBankDetail = ds.Tables[10];
        DataTable dtMainTable = ds.Tables[12];
        dtSalutation = ds.Tables[13];
        hdnEmail.Value = dtSalutation.Rows[0]["VCH_EMAIL"].ToString();
        hdnMobile.Value = dtSalutation.Rows[0]["VCH_OFF_MOBILE"].ToString();

        try
        {
            #region Industrial Unit

            if (dtindustryPre.Rows.Count > 0)
            {
                lblUnitAddress.Text = dtindustryPre.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
                lblUnitAddr.Text = dtindustryPre.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
                lblMr.Text = dtindustryPre.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
                lblName.Text = dtindustryPre.Rows[0]["VCHPREAPPLICANTNAME"].ToString().Trim();
                lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                lblDistrict.Text = dtindustryPre.Rows[0]["distname"].ToString();
                DataSet ds1 = new DataSet();
                ds1 = IncentiveManager.dynamic_name_doc_bind();
                ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intUnitType"].ToString() + "'";
                ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intOrganisationType"].ToString() + "'";
                DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
                if (dt.Rows.Count > 0)
                {
                    Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                    Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                    lblinstMultiselect.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
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
                lblIs_Is_Pioneer.Text = (dtindustryPre.Rows[0]["intPioneer"].ToString() == "1") ? "Yes" : "No";

                if (dtindustryPre.Rows[0]["vchPioneerCertificate"].ToString() != "")
                {
                    DivPioneer.Visible = true;
                    Lbl_Pioneer_Doc_Name.Text = dtindustryPre.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();
                    Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchPioneerCertificate"].ToString();
                }
                else
                {
                    DivPioneer.Visible = false;
                }

                lbl_Regd_Office_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();


                lbl_Regd_Office_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();

                lblApplyBy.Text = (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1") ? "Self" : "Authorized Person";
                TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHPREAPPLICANTNAME"].ToString().Trim();
                lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                divadhhardetails.Visible = false;
                divAuthorizing.Visible = false;
                DivAppDocType.Visible = false;
                LblAadhaar.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim();
                lblApplyBy.Text = dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1" ? "Self" : "Authorized Person";
                if (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1")
                {
                    divadhhardetails.Visible = true;
                }
                else
                {
                    divAuthorizing.Visible = true;
                    DivAppDocType.Visible = false;
                    LnkViewMultiselectDoc.Attributes.Add("href", RetFileNamePath(dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc"));
                    lblDocumentType.Text = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    lblauthority.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
                }
                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();

                lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
                lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
                lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
                lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
                lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();

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
                    //tr_Prod_Comm_Before.Visible = false;
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
                lbl_Sectoral.Text = (dtindustryPre.Rows[0]["bitPriorityIPR"].ToString() == "1") ? "Yes" : "No";
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
            if (dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString() == "")
            {
                FFCI_Before_Doc_Name.Visible = false;
            }
            else
            {
                FFCI_Before_Doc_Name.Visible = true;
                Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString() + " - Before E/M/D";
                Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString();
            }
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
                Lbl_Approved_DPR_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocBeforeCodeName"].ToString() + " - Before E/M/D";
                Hyp_View_Approved_DPR_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocBefore"].ToString();
            }
            lbl_FFCI_Date_After.Text = dtInvestmentPre.Rows[0]["dtmFFCIDateAfter"].ToString();
            Lbl_FFCI_After_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocAfterCodeName"].ToString();
            Hyp_View_FFCI_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocAfter"].ToString();

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
            #region Investment Pollution
            if (dtInvestmentPollution.Rows.Count > 0)
            {
                txtOperationalization.Text = dtInvestmentPollution.Rows[0]["DTMOPERATIONALIZATION"].ToString();
                if (dtInvestmentPollutionDtl.Rows.Count > 0)
                {
                    gvEquipment.DataSource = dtInvestmentPollutionDtl;
                    gvEquipment.DataBind();
                }

                if (dtInvestmentPollution.Rows[0]["VCHOPERATIONALIZATIONDOC"].ToString() != "")
                {
                    hypOperationalization.Attributes.Add("href", RetFileNamePath(dtInvestmentPollution.Rows[0]["VCHOPERATIONALIZATIONDOC"].ToString(), "Operationalization"));
                }
            }

            #endregion
            #region Bank Details
            if (dtBankDetail.Rows.Count > 0)
            {
                lblAccNo.Text = dtBankDetail.Rows[0]["VCHACCOUNTNO"].ToString();
                lblBankName.Text = dtBankDetail.Rows[0]["VCHBANKNAME"].ToString();
                lblBranchName.Text = dtBankDetail.Rows[0]["VCHBRANCHNAME"].ToString();
                lblIFSC.Text = dtBankDetail.Rows[0]["VCHIFSCNO"].ToString();
                lblMICRNo.Text = dtBankDetail.Rows[0]["VCHMICR"].ToString();
                if (dtBankDetail.Rows[0]["vchBankDoc"].ToString() != "")
                {
                    hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBankDetail.Rows[0]["vchBankDoc"].ToString();
                }
            }
            #endregion
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
            objIncentive.strcActioncode = "M";
            objIncentive.IncentiveNum = dtMainTable.Rows[0]["VCHINCENTIVENO"].ToString();
            DataSet dsMaster = new DataSet();
            dsMaster = IncentiveManager.GetIncentiveMaster(objIncentive);
            if (dsMaster.Tables[0].Rows.Count > 0)
            {
                DataTable dtbPostSubFlag = dsMaster.Tables[0];
                lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
                lblTitle2.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
            }

            lblDate.Text = dtMainTable.Rows[0]["DTMCREATEDBY"].ToString();
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
    protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblInvestmentAmt = (Label)e.Row.FindControl("lblInvestmentAmt");
            dPageTotal += Decimal.Parse(lblInvestmentAmt.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotal = (Label)e.Row.FindControl("lblTotal");
            lblTotal.Text = dPageTotal.ToString("N2");
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

    protected void btnApply_Click(object sender, EventArgs e)
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please upload .png/.jpg/.jpeg format image only !', 'GOSwift'); </script>", false);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Upload Signature. !', 'GOSwift'); </script>", false);
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
            //TrDisplayNone(Lbl_Prod_Comm_Before_Doc_Name.Text, tr_Prod_Comm_Before);
            TrDisplayNone(Lbl_Prod_Comm_After_Doc_Name.Text, tr_Prod_Comm_After_Doc_Name);
            TrDisplayNone(Lbl_Pioneer_Doc_Name.Text, DivPioneer);
            TrDisplayNone(Lbl_Direct_Emp_Before_Doc_Name.Text, divEmp_Before_Doc_Name);
            TrDisplayNone(Lbl_Unit_Type_Doc_Name.Text, Div_Unit_Type_Doc);
            TrDisplayNone(Lbl_Direct_Emp_After_Doc_Name.Text, divEmp_Emp_After_Doc_Name);
            TrDisplayNone(Lbl_FFCI_Before_Doc_Name.Text, FFCI_Before_Doc_Name);
            TrDisplayNone(Lbl_Approved_DPR_After_Doc_Name.Text, Div_Approved_DPR_After_Doc);
            TrDisplayNone(Lbl_Approved_DPR_Before_Doc_Name.Text, Approved_DPR_Before_Doc_Name);
            TrDisplayNone(Lbl_FFCI_After_Doc_Name.Text, tr_FFCI_After_Doc_Name);
            TrDisplayNone(Lbl_Term_Loan_Doc_Name.Text, tr_Term_Loan_Doc_Name);
            TrDisplayNone(lblBank.Text, tr_Bank);


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