using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityLayer.Incentive;
using CommonDataExtensionHelper;
using System.IO;
public partial class incentives_InterestSubsidyPreview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillData();
    }
    #region FillData
    protected void FillData()
    {
        Incentive objEntity = new Incentive();
        GetAndViewParam objPar = new GetAndViewParam();
        objEntity.strcActioncode = "5";
        objPar.Param1ID = "jeevan";
        objPar.Param2ID = "jeevan";
        objPar.Param3ID = "jeevan";
        objEntity.UnqIncentiveId = 37;
        objPar.InctType = 4;
        objEntity.GetVwPrmtrs = objPar;
        objEntity.FormType = FormNumber.InterestSubsidy_01;
       
        DataSet ds = new DataSet();
        ds = IncentiveManager.GetIncentive(objEntity);
        DataTable dtIndustry = ds.Tables[0];
        DataTable dtProduction = ds.Tables[1];
        DataTable dtProductionDet = ds.Tables[2];
        DataTable dtInvestment = ds.Tables[3];
        DataTable dtMeansFinance = ds.Tables[4];
        DataTable dtMeansFinanceLoanDet = ds.Tables[5];
        DataTable dtLoanRePayment = ds.Tables[7];
        DataTable dtInterestSubsidy = ds.Tables[8];
        DataTable dtInterestPaid = ds.Tables[9];
        DataTable dtSTATUTARYCLEARANCE = ds.Tables[10];
        DataTable dtTERMLOAN = ds.Tables[11];

        try
        {
            #region Industrial Unit
            FillIndustryFields(dtIndustry, "0");

            #endregion
            #region Production Details
            if (dtProductionDet.Rows.Count > 0)
            {
                grdProduction.DataSource = dtProductionDet;
                grdProduction.DataBind();
            }
            //bind master data
            if (dtProduction.Rows.Count > 0)
            {
                lblDirEmp.Text = dtProduction.Rows[0]["INTDIRECTEMP"].ToString();
                lblContEmp.Text = dtProduction.Rows[0]["INTCONTRACTUALEMP"].ToString();
                if (dtProduction.Rows[0]["VCHEMPDOC"].ToString() != "")
                {
                    hypEmpDoc.NavigateUrl =RetFileNamePath("Production", dtProduction.Rows[0]["VCHEMPDOC"].ToString()); /////PinoneerDoc file upload View
                }
                lblCurrentManagerial.Text = dtProduction.Rows[0]["INTCURRENTMANAGERIAL"].ToString();
                lblProposedManagerial.Text = dtProduction.Rows[0]["INTPROPOSEDMANAGERIAL"].ToString();
                lblCurrentSupervisory.Text = dtProduction.Rows[0]["INTCURRENTSUPERVISORY"].ToString();
                lblProposedSupervisory.Text = dtProduction.Rows[0]["INTPROPOSEDSUPERVISORY"].ToString();

                lblCurrentSkilled.Text = dtProduction.Rows[0]["INTCURRENTSKILLED"].ToString();
                lblProposedSkilled.Text = dtProduction.Rows[0]["INTPROPOSEDSKILLED"].ToString();

                lblCurrentSemiSkilled.Text = dtProduction.Rows[0]["INTCURRENTSEMISKILLED"].ToString();
                lblProposedSemiSkilled.Text = dtProduction.Rows[0]["INTPROPOSEDSEMISKILLED"].ToString();

                lblCurrentUnskilled.Text = dtProduction.Rows[0]["INTCURRENTUNSKILLED"].ToString();
                lblProposedUnskilled.Text = dtProduction.Rows[0]["INTPROPOSEDUNSKILLED"].ToString();

                lblCurrentTotal.Text = dtProduction.Rows[0]["INTCURRENTTOTAL"].ToString();
                lblProposedTotal.Text = dtProduction.Rows[0]["INTPROPOSEDTOTAL"].ToString();
            }
            #endregion
            #region Investment Details
            if (dtInvestment.Rows.Count > 0)
            {
                if (dtInvestment.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString() != "")
                {
                    lblTimescheduleforyearofcomm.Text = Convert.ToDateTime(dtInvestment.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString()).ToString("dd-MMM-yyyy");
                }
                if (dtInvestment.Rows[0]["VCH_Document_in_support"].ToString() != "")
                {

                    hypFirstInvestment.NavigateUrl =RetFileNamePath("investment", dtInvestment.Rows[0]["VCH_Document_in_support"].ToString());
                }
                lblLandtype.Text = dtInvestment.Rows[0]["VCH_LAND_TYPE"].ToString();
                lblLandtypeAmount.Text = dtInvestment.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
                lblBuilding.Text = dtInvestment.Rows[0]["DEC_Building"].ToString();
                lblPlantMachinery.Text = dtInvestment.Rows[0]["DEC_Plant_Machinery"].ToString();
                lblBalancingEquipment.Text = dtInvestment.Rows[0]["DEC_Balancing_Equipment"].ToString();
                lblOtherFixedAssests.Text = dtInvestment.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
                lblTotalAmount.Text = dtInvestment.Rows[0]["DEC_Total"].ToString();

            }
            #endregion
            #region Means Of Finance
            if (dtMeansFinance.Rows.Count > 0)
            {
                if (dtMeansFinance.Rows[0]["VCH_TERM_LOAN_SAC"].ToString() != "")
                {

                    hypFinancial.NavigateUrl = RetFileNamePath("MeansOfFinance", dtMeansFinance.Rows[0]["VCH_TERM_LOAN_SAC"].ToString());
                }
            }
                if (dtMeansFinanceLoanDet.Rows.Count > 0)
                {
                    grdMeansOfFinance.DataSource = dtMeansFinanceLoanDet;
                    grdMeansOfFinance.DataBind();
                }
                if (dtLoanRePayment.Rows.Count > 0)
                {
                    grdWorkingLoan.DataSource = dtLoanRePayment;
                    grdWorkingLoan.DataBind();
                }
            
            #endregion
            #region Interest Subsidy Details
            if (dtInterestPaid.Rows.Count > 0)
            {
                foreach (DataRow dr in dtInterestPaid.Rows)
                {
                    if (ViewState["gvdsanctioned"] == null)
                    {
                        List<SanctionSubsidy> objSanctionSubsidyList = new List<SanctionSubsidy>();
                        ViewState["gvdsanctioned"] = objSanctionSubsidyList;
                    }
                    SanctionSubsidy objSanctionSubsidy = new SanctionSubsidy();

                    objSanctionSubsidy.decSancAmt = Convert.ToDecimal(dr["DEC_SANCTION_AMT"].ToString());

                    objSanctionSubsidy.strFinInstitute = dr["VCH_INSTITUTE_NAME"].ToString();
                    objSanctionSubsidy.strFYrSanction = dr["VCH_FYEAR"].ToString();
                    objSanctionSubsidy.strSanDate = dr["VCH_SANCTION_DATE"].ToString();
                    objSanctionSubsidy.strSanOrderNo = dr["VCH_SORDER_NO"].ToString();
                    List<SanctionSubsidy> objSanctionSubsidyList1 = (List<SanctionSubsidy>)ViewState["gvdsanctioned"];
                    objSanctionSubsidyList1.Add(objSanctionSubsidy);
                    ViewState["gvdsanctioned"] = objSanctionSubsidyList1;
                    gvdsanctioned.DataSource = objSanctionSubsidyList1;
                    gvdsanctioned.DataBind();
                }
            }

            if (dtSTATUTARYCLEARANCE.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSTATUTARYCLEARANCE.Rows)
                {
                    if (ViewState["gvdStatutoryClearences"] == null)
                    {
                        List<StatutoryClearences> objStatutoryClearencesList = new List<StatutoryClearences>();
                        ViewState["gvdStatutoryClearences"] = objStatutoryClearencesList;
                    }
                    StatutoryClearences objStatutoryClearences = new StatutoryClearences();

                    objStatutoryClearences.strClearanceName = dr["VCH_CLEARANCE_NAME"].ToString();
                    //string fillename = "";
                    //if (dr["VCH_CLEARANCE_NAME"].ToString()!="")
                    //{
                    //    fillename = fileUploadClearences.FileName;
                    //    if (Path.GetExtension(fillename).ToUpper() == ".PDF")
                    //    {
                    //        fillename = DateTime.Now.ToString("ddMMMyyyyHHmmssfff") + ".pdf";
                    //        fileUploadClearences.SaveAs(Server.MapPath("~/incentives/Files/Interest/" + fillename));

                    //    }
                    //    else
                    //    {
                    //        Response.Write("<script>alart('Upload only pdf file')</script>");
                    //    }
                    //}
                    objStatutoryClearences.ClearanceNameDoc = dr["VCH_CLEARANCE_DOC"].ToString();
                    List<StatutoryClearences> objStatutoryClearencesList1 = (List<StatutoryClearences>)ViewState["gvdStatutoryClearences"];
                    objStatutoryClearencesList1.Add(objStatutoryClearences);
                    ViewState["gvdStatutoryClearences"] = objStatutoryClearencesList1;
                    //gvdStatutoryClearences.DataSource = objStatutoryClearencesList1;
                    //gvdStatutoryClearences.DataBind();
                }
            }
            if (dtTERMLOAN.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTERMLOAN.Rows)
                {
                    if (ViewState["gvdProduction"] == null)
                    {
                        List<IntrestOnTermLoan> objIntrestOnTermLoanList = new List<IntrestOnTermLoan>();
                        ViewState["gvdProduction"] = objIntrestOnTermLoanList;
                    }
                    IntrestOnTermLoan objIntrestOnTermLoan = new IntrestOnTermLoan();
                    objIntrestOnTermLoan.strFYrTLoan = dr["VCH_FYEAR"].ToString(); ;

                    objIntrestOnTermLoan.decInterestAmt = Convert.ToDecimal(dr["DEC_INTEREST_AMT"].ToString());

                    objIntrestOnTermLoan.strPaymentDate = dr["VCH_PAYMENT_DATE"].ToString();

                    List<IntrestOnTermLoan> objIntrestOnTermLoanList1 = (List<IntrestOnTermLoan>)ViewState["gvdProduction"];
                    objIntrestOnTermLoanList1.Add(objIntrestOnTermLoan);
                    ViewState["gvdProduction"] = objIntrestOnTermLoanList1;
                    gvdProduction.DataSource = objIntrestOnTermLoanList1;
                    gvdProduction.DataBind();
                }
            }

            if (dtInterestSubsidy.Rows.Count > 0)
            {
                lblAmountClaimed.Text = dtInterestSubsidy.Rows[0]["DEC_DIFFER_AMT"].ToString();
                lblDifferentialAmont.Text = dtInterestSubsidy.Rows[0]["DEC_DIFFER_AMT"].ToString();
                lblreimbursement.Text = dtInterestSubsidy.Rows[0]["DEC_REIMBURSEMENT"].ToString();
                lblPeriodofClaim.Text = dtInterestSubsidy.Rows[0]["INT_PERIOD_CLAIM"].ToString();
                if (dtInterestSubsidy.Rows[0]["VCH_DIFFER_AMT_DOC"].ToString() != "")
                {
                    hypDifferAmtClaim.NavigateUrl = "~/incentives/Files/Interest/" + dtInterestSubsidy.Rows[0]["VCH_DIFFER_AMT_DOC"].ToString();
                    
                }
            }
            #endregion

        }
        catch (Exception)
        {

            throw;
        }
    }
    #endregion
    #region Industry Details Bind
    public void FillIndustryFields(DataTable dtindustry, string DraftStatus)
    {
        try
        {
            if (dtindustry.Rows.Count > 0)
            {
                hdnId.Value = dtindustry.Rows[0]["INTINCUNQUEID"].ToString().Trim();
                LblEnterPrise.Text = dtindustry.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
                LblOrgType.Text = dtindustry.Rows[0]["INDUSTRIAL_UNIT_Name"].ToString();
                LblApplicantName.Text = (dtindustry.Rows[0]["INTGENDER"].ToString() == "1" ? "Mr" : "Mrs") + " " + dtindustry.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();

                if (dtindustry.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                {
                    LblAadhaar.Text = dtindustry.Rows[0]["VCHAADHAARNO"].ToString();
                }

                LnkViewRehabilDoc.NavigateUrl = RetFileNamePath("IndustryUnit", dtindustry.Rows[0]["VCHREHABILITATEDDOCUMENT"].ToString()); /////RehabilDoc file upload View
                LnkViewIndustryUnitDoc.NavigateUrl = RetFileNamePath("IndustryUnit", dtindustry.Rows[0]["VCHINDUSTRIALDOCUMENT"].ToString()); /////IndustryUnitDoc file upload View
                LnkViewCertificateRegistration.NavigateUrl = RetFileNamePath("IndustryUnit", dtindustry.Rows[0]["VCHCERTIFICATEOFREGISTRATION"].ToString()); /////CertificateRegistration file upload View
                LnkViewCertificateCommence.NavigateUrl = RetFileNamePath("IndustryUnit", dtindustry.Rows[0]["VCHCOMMENCEMENTCERTIFICATE"].ToString()); /////CertificateCommence file upload View



                LblManagingPartner.Text = (dtindustry.Rows[0]["VCHMANAGINGPARTNERGENDER"].ToString() == "1" ? "Mr" : "Mrs") + " " + dtindustry.Rows[0]["VCHMANAGINGPARTNERNAME"].ToString().Trim();
                LblEINNo.Text = dtindustry.Rows[0]["VCHEINNO"].ToString().Trim();
                LblPCNo.Text = dtindustry.Rows[0]["VCHPCNO"].ToString().Trim();
                LblAddressInd.Text = dtindustry.Rows[0]["VCHINDUSTRYADDRESS"].ToString();
                LblRegAddress.Text = dtindustry.Rows[0]["VCHREGISTEREDOFCADDRESS"].ToString().Trim();



                LblUnitCategory.Text = ReturnUnitCategory(dtindustry.Rows[0]["INTCATAGORYUNIT"].ToString());
                LblUnitType.Text = ReturnUnitTypeName(dtindustry.Rows[0]["INTUNITTYPE"].ToString());

                ////Lblpio.SelectedValue = dtindustry.Rows[0]["INTPIONEER"].ToString(); 

                LblEINDate.Text = dtindustry.Rows[0]["DTMEIN"].ToString(); /////----------- datetime
                LblPCInsuranceDate.Text = dtindustry.Rows[0]["DTMPCISSUANCE"].ToString(); /////------- dateime
                LblCommenceDate.Text = dtindustry.Rows[0]["DTMCOMMENCEMENT"].ToString();  ////------- dateime

                if (dtindustry.Rows[0]["INTAPPLYBY"].ToString() == "1")
                {
                    LblApplyBy.Text = "Self"; ////radApplyBy.SelectedValue = dtindustry.Rows[0]["INTAPPLYBY"].ToString();
                    divaadhar.Visible = true;
                    tr_authorizing.Visible = false;
                }
                else if (dtindustry.Rows[0]["INTAPPLYBY"].ToString() == "2")
                {
                    LblApplyBy.Text = "Authorized Person"; ////radApplyBy.SelectedValue = dtindustry.Rows[0]["INTAPPLYBY"].ToString();
                    divaadhar.Visible = false;
                    LnkViewAUTHORIZEDFILE.Attributes.Add("href", RetFileNamePath("IndustryUnit", dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString())); /////AUTHORIZEDFILE file upload View
                    tr_authorizing.Visible = true;
                }
                if (dtindustry.Rows[0]["INTPRIORITY"].ToString() == "1")
                {
                    LblPriority.Text = "Yes";
                    LnkViewPinoneerDoc.Attributes.Add("href", RetFileNamePath("IndustryUnit", dtindustry.Rows[0]["VCHPIONEERCERTIFICATE"].ToString())); /////PinoneerDoc file upload View
                    tr_Pioneer.Visible = true;
                }
                else if (dtindustry.Rows[0]["INTPRIORITY"].ToString() == "2")
                {
                    LblPriority.Text = "No";
                    tr_Pioneer.Visible = false;
                }

            }
        }
        catch (Exception)
        {
        }
    }
    public string ReturnUnitTypeName(string typeid)
    {
        string UnitTypeName = "--";
        try
        {
            switch (typeid)
            {
                case "1":
                    UnitTypeName = "Existing E/M/D";
                    break;
                case "2":
                    UnitTypeName = "New Unit";
                    break;
                case "3":
                    UnitTypeName = "Migrated Unit Treated As New";
                    break;
                case "4":
                    UnitTypeName = "Rehabilitated Unit Treated As New";
                    break;

            }
        }
        catch (Exception)
        {
        }
        return UnitTypeName;
    }
    public string ReturnUnitCategory(string categoryid)
    {
        string CategoryName = "--";
        try
        {

            switch (categoryid)
            {
                case "1":
                    CategoryName = "Micro";
                    break;
                case "2":
                    CategoryName = "Small";
                    break;
                case "3":
                    CategoryName = "Medium";
                    break;
                case "4":
                    CategoryName = "Large";
                    break;

            }
        }
        catch (Exception)
        {
        }
        return CategoryName;
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
    
}