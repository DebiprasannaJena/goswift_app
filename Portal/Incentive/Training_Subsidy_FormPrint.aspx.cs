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

public partial class incentives_Training_Subsidy_FormPreview : System.Web.UI.Page
{
   protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FillData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #region FillData
    protected void FillData()
    {
        Incentive objincUnit = new Incentive();
        objincUnit.GetVwPrmtrs = new GetAndViewParam();


        objincUnit.GetVwPrmtrs.Param1ID = "D18thSep";//--Incentive Number
        objincUnit.GetVwPrmtrs.Param2ID = "D18thSep";//--UnitCode
        objincUnit.GetVwPrmtrs.Param3ID = "D18thSep";//--Proposal/Peal/PC Number
        objincUnit.GetVwPrmtrs.Param4ID = "";
        objincUnit.GetVwPrmtrs.Param5 = "";
        objincUnit.GetVwPrmtrs.Param6 = "";
        objincUnit.GetVwPrmtrs.Param7 = "";
        objincUnit.GetVwPrmtrs.FrmDate = Convert.ToDateTime("1/1/1900");
        objincUnit.GetVwPrmtrs.Todate = Convert.ToDateTime("1/1/1900");
        objincUnit.GetVwPrmtrs.InctType = 4;
        objincUnit.UnqIncentiveId=Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
        objincUnit.FormType = FormNumber.TrainingSubsidy_16;
       

        DataSet ds = new DataSet();
        ds = IncentiveManager.GetIncentive(objincUnit);
        DataTable dtIndustry = ds.Tables[0];
        DataTable dtProduction = ds.Tables[1];
        DataTable dtProductionDet = ds.Tables[2];
        DataTable dtInvestment = ds.Tables[3];
        DataTable dtMeansFinance = ds.Tables[4];
        DataTable dtMeansFinanceLoanDet = ds.Tables[5]; 
        DataTable dtBank = ds.Tables[9];
        DataTable dtTraiingDtl = ds.Tables[8];
        DataTable dtAdditioalDoc = ds.Tables[10];
       
        
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
                    hypEmpDoc.Attributes.Add("href", RetFileNamePath("Production", dtProduction.Rows[0]["VCHEMPDOC"].ToString())); /////PinoneerDoc file upload View
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

                    lknViewFieldCapital.Attributes.Add("href", RetFileNamePath("investment", dtInvestment.Rows[0]["VCH_Document_in_support"].ToString()));
                }
                lblLandtype.Text = dtInvestment.Rows[0]["VCH_LAND_TYPE"].ToString();
                lblLandtypeAmount.Text = dtInvestment.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
                lblBuilding.Text = dtInvestment.Rows[0]["DEC_Building"].ToString();
                lblPlantMachinery.Text = dtInvestment.Rows[0]["DEC_Plant_Machinery"].ToString();
                lblBalancingEquipment.Text = dtInvestment.Rows[0]["DEC_Balancing_Equipment"].ToString();
                lblOtherFixedAssests.Text = dtInvestment.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
                lblelecInstall.Text = dtInvestment.Rows[0]["DEC_ELECTRIC_INSTALL"].ToString();
                lblTotalAmount.Text = Convert.ToString(Convert.ToDecimal(lblLandtypeAmount.Text) + Convert.ToDecimal(lblBuilding.Text) + Convert.ToDecimal(lblPlantMachinery.Text) + Convert.ToDecimal(lblBalancingEquipment.Text) + Convert.ToDecimal(lblOtherFixedAssests.Text) +Convert.ToDecimal(lblelecInstall.Text));
               
            }
            #endregion
            #region Means Of Finance
            if (dtMeansFinance.Rows[0]["VCH_TERM_LOAN_SAC"].ToString() != "")
            {

                lknSatutoryClean3.Attributes.Add("href", RetFileNamePath("MeansOfFinance", dtMeansFinance.Rows[0]["VCH_TERM_LOAN_SAC"].ToString()));
            }

            if (dtMeansFinance.Rows[0]["VCH_APPROVED_DOC"].ToString() != "")
            {

                lknSatutoryClean4.Attributes.Add("href", RetFileNamePath("MeansOfFinance", dtMeansFinance.Rows[0]["VCH_APPROVED_DOC"].ToString()));
            }
            if (dtMeansFinanceLoanDet.Rows.Count > 0)
            {
                grdMeansOfFinance.DataSource = dtMeansFinanceLoanDet;
                grdMeansOfFinance.DataBind();
            }
            #endregion

            #region Training Details
           
           //Training Details
            if (dtTraiingDtl.Rows.Count > 0)
            {
                TextBox1.Text = ds.Tables[8].Rows[0]["intNewlyrecruited_NoOfTrainees"].ToString();
                TextBox8.Text = ds.Tables[8].Rows[0]["intNewlyrecruited_NoOfDays"].ToString();
                TextBox102.Text = ds.Tables[8].Rows[0]["vchNewlyrecruited_InHouseOrOutSide"].ToString();
                TextBox9.Text = ds.Tables[8].Rows[0]["vchNewlyrecruited_NameOfInstitute"].ToString();
                TextBox103.Text = ds.Tables[8].Rows[0]["intSkillupgradation_NoOfTrainees"].ToString();
                TextBox104.Text = ds.Tables[8].Rows[0]["vchSkillupgradation_NoOfDays"].ToString();
                TextBox105.Text = ds.Tables[8].Rows[0]["vchSkillupgradation_InHouseOrOutSide"].ToString();
                TextBox10.Text = ds.Tables[8].Rows[0]["vchSkillupgradation_NameOfInstitute"].ToString();
                TextBox119.Text = ds.Tables[8].Rows[0]["vchTotalUnitConsumed"].ToString();
                TextBox120.Text = ds.Tables[8].Rows[0]["decAmountPaid"].ToString();

                if (ds.Tables[8].Rows[0]["vch_TraineeDetails"].ToString() != "")
                {
                    lknSatutoryClean4Tdet.NavigateUrl = "../incentives/Files/TrainingDetail/" + ds.Tables[8].Rows[0]["vch_TraineeDetails"].ToString();
                   
                }
                if (ds.Tables[8].Rows[0]["vch_MoneyReceiptFile"].ToString() != "")
                {
                    lknSatutoryClean4Rdet.NavigateUrl = "../incentives/Files/TrainingDetail/Receipt/" + ds.Tables[8].Rows[0]["vch_MoneyReceiptFile"].ToString();
                    
                }
            }

            #endregion

            #region Bank Details
            lblAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
            lblBnkNm.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
            lblBranch.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
            lblIFSC.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
            lblMICRNo.Text = dtBank.Rows[0]["VCHMICR"].ToString();
            #endregion

            #region Additional Document
            if (dtAdditioalDoc.Rows.Count > 0)
            {
                if (dtAdditioalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString() != "")
                {

                    lknViewSatutoryClean.NavigateUrl = "../incentives/Files/AdditionalDocument/" + dtAdditioalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
                }
                if (dtAdditioalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString() != "")
                {

                    lknViewSatutoryClean1.NavigateUrl = "../incentives/Files/AdditionalDocument/" + dtAdditioalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
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
                lblMr.Text = dtindustry.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();
                lblName.Text = dtindustry.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();
                lblUnitAddress.Text = dtindustry.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
                lblAddress.Text = dtindustry.Rows[0]["VCHINDUSTRYADDRESS"].ToString().Trim();

                hdnId.Value = dtindustry.Rows[0]["INTINCUNQUEID"].ToString().Trim();
                LblEnterPrise.Text = dtindustry.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
                LblOrgType.Text = dtindustry.Rows[0]["INDUSTRIAL_UNIT_Name"].ToString();
                LblApplicantName.Text = (dtindustry.Rows[0]["INTGENDER"].ToString() == "1" ? "Mr" : "Mrs") + " " + dtindustry.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();

                if (dtindustry.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                {
                    LblAadhaar.Text = dtindustry.Rows[0]["VCHAADHAARNO"].ToString();
                }

                LnkViewRehabilDoc.Attributes.Add("href", RetFileNamePath("IndustryUnit",dtindustry.Rows[0]["VCHREHABILITATEDDOCUMENT"].ToString())); /////RehabilDoc file upload View
                LnkViewIndustryUnitDoc.Attributes.Add("href", RetFileNamePath("IndustryUnit", dtindustry.Rows[0]["VCHINDUSTRIALDOCUMENT"].ToString())); /////IndustryUnitDoc file upload View
                LnkViewCertificateRegistration.Attributes.Add("href", RetFileNamePath("IndustryUnit", dtindustry.Rows[0]["VCHCERTIFICATEOFREGISTRATION"].ToString())); /////CertificateRegistration file upload View
                LnkViewCertificateCommence.Attributes.Add("href", RetFileNamePath("IndustryUnit", dtindustry.Rows[0]["VCHCOMMENCEMENTCERTIFICATE"].ToString())); /////CertificateCommence file upload View



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
                lblTINRegNo.Text = dtindustry.Rows[0]["VCHREGISTRATIONNO"].ToString();
                lblTINRegDate.Text = dtindustry.Rows[0]["VCHTINDATE"].ToString();
                lblLicenseNo.Text = dtindustry.Rows[0]["VCHLICENSENO"].ToString();
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
    public string RetFileNamePath(string Folder,string filename)
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