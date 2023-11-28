using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using DataAcessLayer.Common;
using System.Data;
using System.IO;
using DataAcessLayer.Incentive;

public partial class incentives_FormPreview_EDD : SessionCheck
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    DataTable dtSalutation;
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
        catch (Exception)
        {
        }
        return retdt;
    }
    private void PostPopulateData()
    {

        try
        {

            Incentive objEntity = new Incentive();
            GetAndViewParam objPar = new GetAndViewParam();
            objEntity.strcActioncode = "";
            objPar.Param1ID = "";
            objPar.Param2ID = "";
            objPar.Param3ID = "";
            objPar.InctType = 4;
            objEntity.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);

            hdnIncentiveId.Value = objEntity.UnqIncentiveId.ToString();

            objEntity.GetVwPrmtrs = objPar;
            objEntity.FormType = FormNumber.ElectricityDuty_12;
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveEDD(objEntity);



            #region "Electricity"
            DataTable dtelectric = ds.Tables[0];
            if (dtelectric.Rows.Count > 0)
            {
                lblelecdat.Text = dtelectric.Rows[0]["DTMSUPPLYDATE"].ToString();
                lblcons.Text = dtelectric.Rows[0]["VCHCONSUMENUMBER"].ToString();
                lblconnected.Text = dtelectric.Rows[0]["VCHCONNECTEDLOAD"].ToString();

                hypelectricbill.NavigateUrl = "~/incentives/Files/EDD/" + dtelectric.Rows[0]["vchpmtvoucherdoc"].ToString();
                hypDOPS.NavigateUrl = "~/incentives/Files/EDD/" + dtelectric.Rows[0]["vchdopsdoc"].ToString();
                hypconnected.NavigateUrl = "~/incentives/Files/EDD/" + dtelectric.Rows[0]["vchcondoc"].ToString();


            }

            DataTable dtelectric1 = ds.Tables[1];
            if (dtelectric1.Rows.Count > 0)
            {
                DataTable dtdutylectric = new DataTable();
                dtdutylectric = (DataTable)ViewState["dtdutylectric"];

                foreach (DataRow drelecdb in dtelectric1.Rows)
                {
                    DataRow drelect = dtdutylectric.NewRow();
                    drelect["dcstategovt"] = drelecdb["VCHSTATEGOVT"].ToString();
                    drelect["dcfrmdate"] = drelecdb["DTMFRMDATE"].ToString();
                    drelect["dctodate"] = drelecdb["DTMTODATE"].ToString();
                    drelect["dcamtclaim"] = drelecdb["NUMCLAIMAMOUNT"].ToString();
                    drelect["dcmfininst"] = drelecdb["VCHFININST"].ToString();
                    dtdutylectric.Rows.Add(drelect);
                }


                ViewState["dtdutylectric"] = dtdutylectric;
                grdelectric.DataSource = dtdutylectric;
                grdelectric.DataBind();
            }

            #endregion

            #region "Availed Details"

            DataTable dtAvail = ds.Tables[3];
            DataTable dtAvail2 = ds.Tables[4];



            if (dtAvail.Rows.Count > 0)
            {
                if (dtAvail.Rows[0]["intNeverAvailedPrior"].ToString() == "1")
                {
                    lblsubsidy.Text = "Yes";

                    Div_Avail_Exempted.Visible = true;
                    Div_Avail_Grid.Visible = true;

                }
                else
                {
                    lblsubsidy.Text = "No";

                    Div_Avail_Exempted.Visible = false;
                    Div_Avail_Grid.Visible = false;
                }



                if (dtAvail.Rows[0]["vchUndertakingSubsidyDoc"].ToString() != "")
                    hypundertaking.NavigateUrl = "~/incentives/Files/EDD/" + dtAvail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                if (dtAvail.Rows[0]["VchSanctionDoc"].ToString() != "")
                    hypsanctioned.NavigateUrl = "~/incentives/Files/EDD/" + dtAvail.Rows[0]["VchSanctionDoc"].ToString();



                lblexemp.Text = dtAvail.Rows[0]["decClaimExempted"].ToString();
                lblreim.Text = dtAvail.Rows[0]["decClaimReimbursement"].ToString();
            }

            if (dtAvail2.Rows.Count > 0)
            {

                DataTable dtincentive1 = new DataTable();
                dtincentive1 = (DataTable)ViewState["dtincentive1"];

                foreach (DataRow dravil2 in dtAvail2.Rows)
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
                grdinctive.DataSource = dtincentive1;
                grdinctive.DataBind();
            }



            #endregion
            #region "Bank Details"
            DataTable dtbank = ds.Tables[2];
            if (dtbank.Rows.Count > 0)
            {
                lblacc.Text = dtbank.Rows[0]["VCHACCOUNTNO"].ToString();
                lblbank.Text = dtbank.Rows[0]["VCHBANKNAME"].ToString();
                lblbranch.Text = dtbank.Rows[0]["VCHBRANCHNAME"].ToString();
                lblIFSC.Text = dtbank.Rows[0]["VCHIFSCNO"].ToString();
                Label1.Text = dtbank.Rows[0]["VCHMICR"].ToString();


                hypbankdetails.NavigateUrl = "~/incentives/Files/EDD/" + dtbank.Rows[0]["vchBankDoc"].ToString();

            }
            #endregion
            #region "Dynamic Form Name"

            DataTable dtdynamic = ds.Tables[6];
            if (dtdynamic.Rows.Count > 0)
            {
                lblTitle.Text = "Application For " + dtdynamic.Rows[0]["vchInctName"].ToString();

            }


            #endregion

            #region "For Header visiblity"

            DataTable dtMainTable = ds.Tables[5];
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
            lblcurdt.Text = dtMainTable.Rows[0]["DTMCREATEDBY"].ToString();


            #endregion
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }



    }
    void crdtdutylectric()
    {
        DataTable dtdutylectric = new DataTable();

        DataColumn dcRowId = new DataColumn("dcRowId");
        dcRowId.DataType = Type.GetType("System.Int32");
        dcRowId.AutoIncrement = true;
        dcRowId.AutoIncrementSeed = 1;
        dcRowId.AutoIncrementStep = 1;
        dtdutylectric.Columns.Add(dcRowId);

        DataColumn dcstategovt = new DataColumn("dcstategovt");
        dcstategovt.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(dcstategovt);

        DataColumn dcfrmdate = new DataColumn("dcfrmdate");
        dcfrmdate.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(dcfrmdate);

        DataColumn dctodate = new DataColumn("dctodate");
        dctodate.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(dctodate);

        DataColumn dcamtclaim = new DataColumn("dcamtclaim");
        dcamtclaim.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(dcamtclaim);

        DataColumn dcmfininst = new DataColumn("dcmfininst");
        dcmfininst.DataType = Type.GetType("System.String");
        dtdutylectric.Columns.Add(dcmfininst);


        ViewState["dtdutylectric"] = dtdutylectric;
        //grdelectric.DataSource = dtdutylectric;
        //grdelectric.DataBind();


    }
    public string RetFileNamePath(string filename, string foldername)
    {
        string strret = "";
        try
        {
            if (filename != "")
            {
                strret = "../incentives/" + foldername + "/" + filename;
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
        grdinctive.DataSource = dtincentive1;
        grdinctive.DataBind();
    }
    public void PostpopulateDataComm(int id)
    {
        try
        {
            DataSet dslivePre = IncentiveManager.PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment

            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
            DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
            DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan
            dtSalutation = dslivePre.Tables[8];///////////For Mail Id and Mobile number
            //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();
            #region IndustrailUnit
            if (dtindustryPre.Rows.Count > 0)
            {
                TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHPREAPPLICANTNAME"].ToString();
                lblMr.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString().Trim();
                lblName.Text = dtindustryPre.Rows[0]["VCHPREAPPLICANTNAME"].ToString().Trim();
                lblAddress.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                lblUnitAddress.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                Label101.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
                lblApplyBy.Text = (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1") ? "Self" : "Authorized Person";


                if (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1")
                {
                    if (dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                    {
                        TxtAdhaar1.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString();
                    }
                    divadhhardetails.Visible = true;
                    divAuthorizing.Visible = false;
                }
                else
                {
                    divadhhardetails.Visible = false;
                    divAuthorizing.Visible = true;

                    if (dtindustryPre.Rows[0]["VCHAUTHORIZEDFILECODE"].ToString() != "")
                    {
                        Lbl_Org_Doc_Type.Text = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILECODEName"].ToString();
                        hypAUTHORIZEDFILE.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
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
                    //lblAuthorizing.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();               

                }
                else
                {
                    Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                    //Hid_Org_Doc_Type.Value = "";
                }



                lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
                lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                lblAddress.Text = lbl_Industry_Address.Text;
                lblPresent.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                //Label9.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
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
                //dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();	
                if (dtindustryPre.Rows[0]["GenderType"].ToString() == "1")
                {
                    lbl_Gender_Partner.Text = "Mr." + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                }
                else
                {
                    lbl_Gender_Partner.Text = "Ms." + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                }
                lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();

                //dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                Lbl_Org_Doc_Type.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
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

            //,vchFFCIDocBeforeCode,,,,,
            //    ,,INT_INCUNQUEID,vchProjectDocBeforeCode,vchProjectDocBefore,dtmFFCIDateAfter,
            //    vchFFCIDocAfterCode,vchFFCIDocAfter,decLandAmtAfter,decBuildingAmtAfter,decPlantMachAmtAfter,decOtheFixedAssetAmtAfter,
            //    decTotalAmtAfter,vchProjectDocAfterCode,vchProjectDocAfter,INT_CREATED_BY,DTM_CREATEDON

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

            #endregion

            #region MEANS OF FINANCE

            //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
            lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
            lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();

            lbl_FDI_Componet.Text = dtMeansFinancePre.Rows[0]["decFDIComponet"].ToString();
            //dtMeansFinancePre.Rows[0]["intCreatedBy"].ToString();
            //dtMeansFinancePre.Rows[0]["vchTermLoanDocCode"].ToString();
            Lbl_Term_Loan_Doc_Name.Text = dtMeansFinancePre.Rows[0]["vchTermLoanDocCodeNAme"].ToString();
            Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();



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
            string strSubject = "GOSwift: Application submitted successfully";
            string PreviewURL = System.Configuration.ConfigurationManager.AppSettings["PreviewURL"];

            //StringBuilder strBldrApplyLeave = new StringBuilder();
            //strBldrApplyLeave.Append("<table>");
            //strBldrApplyLeave.Append("<tr><td>" + lblTitle.Text + " of M/s " + lblMr.Text  +  " has been submitted successfully." + "</td></tr>");
            //strBldrApplyLeave.Append("<tr><td> Log into www.swp.investodisha.gov.in for further details. </td></tr>");
            //strBldrApplyLeave.Append("</table>");
            //string strBody = strBldrApplyLeave.ToString();

            string strBody = lblTitle.Text + " of M/s " + lblMr.Text + " has been submitted successfully. " + PreviewURL;
            string SMSContent = strSubject;

            var myList = new List<string>();
            myList.Add(dtSalutation.Rows[0]["VCH_EMAIL"].ToString());
            string[] tomail = myList.ToArray();

            bool mailStatus = objcomm.sendMail(strSubject, strBody, tomail, true);
            bool smsStatus = objcomm.SendSmsNew(dtSalutation.Rows[0]["VCH_OFF_MOBILE"].ToString(), SMSContent);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["FyYear"] != null && Convert.ToString(Session["FyYear"]) != "")
        {
            lblfinyear.Text = Convert.ToString(Session["FyYear"]);
        }
        else
        {
            lblfinyear.Text = Convert.ToString(int.Parse(DateTime.Now.Month.ToString()) >= 4 ? int.Parse(DateTime.Now.Year.ToString()) : int.Parse(DateTime.Now.Year.ToString()) - 1);
            //"2017";
        }


        if (!Page.IsPostBack)
        {

            crdtdutylectric();
            crdtincentive1();

            if (Convert.ToString(Request.QueryString["InctUniqueNo"]) != "")
            {
                PostpopulateDataComm(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
                PostPopulateData();

            }
            TRVisibility();
        }

        lbldist.Text = lbl_District.Text;
        lblGM.Text = lbl_District.Text;
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



            //TrDisplayNone(lblAvailDoc.Text, trAvailDoc);
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

    protected void btnapply_Click(object sender, EventArgs e)
    {
        Incentive objEntity = new Incentive();
        try
        {
            string[] fileext = { ".png", ".jpg", ".jpeg" };
            if (FileUpload1.HasFile)
            {
                if (fileext.Contains(System.IO.Path.GetExtension(FileUpload1.FileName).ToLower()))
                {
                    bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/Signature/"));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath("../incentives/Files/Signature/"));

                    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string filename = "Signature" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
                    FileUpload1.SaveAs(Server.MapPath("../incentives/Files/Signature/") + filename);
                    objEntity.Signature = filename;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please upload .png/.jpg/.jpeg format image only !', '" + strProjName + "'); </script>", false);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Upload Signature. !', '" + strProjName + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
}