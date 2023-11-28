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

public partial class incentives_PatentRegistration : System.Web.UI.Page
{
    Incentive objincUnit = new Incentive();
    string gFilePath = "../incentives/Files";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FetchAllFieldCotents();
        }
    }

    public void FetchAllFieldCotents()
    {
        try
        {
            objincUnit = new Incentive();
            objincUnit.GetVwPrmtrs = new GetAndViewParam();
            objincUnit.GetVwPrmtrs.Param1ID = "2555";//////Incentive Number
            objincUnit.GetVwPrmtrs.Param2ID = "2555";//////--UnitCode
            objincUnit.GetVwPrmtrs.Param3ID = "2555";/////--Proposal/Peal/PC Number
            objincUnit.GetVwPrmtrs.InctType = 4;/////--Form type 4(for Pre Insentive) 5(for Post Incentive)
            objincUnit.PCNum = "2555";
            objincUnit.UnitCode = "2555";
            objincUnit.Userid = 2555;//// to be passed from session
            objincUnit.strcActioncode = "A";
            objincUnit.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            objincUnit.Createdby = 2555;
            objincUnit.FormType = FormNumber.PremiumLeviableForConversionOfLandForIndustrialUse_11;

            DataSet dslive = IncentiveManager.GetIncentive(objincUnit);
            DataTable dtindustry = dslive.Tables[0];////////////industry panel
            DataTable dtProduction = dslive.Tables[1];///////////production & employment 
            DataTable dtProductionDet = dslive.Tables[2];///////////production & employment 
            DataTable dtInvestment = dslive.Tables[3];///////////investment details
            DataTable dtMeansFinanceLoanDet = dslive.Tables[5];///////////Means of Finance
            DataTable dtLandInfo = dslive.Tables[6];///////////Land Details Master
            DataTable dtLandDetails = dslive.Tables[7];///////////Land Details Tran table
            DataTable dtAdditionalDoc = dslive.Tables[8];////// additional document master ////////// Tables[9] ---- additional document Tran Table --Add More concept
            DataTable dtstatus = dslive.Tables[10];
            string draftStatus = dtstatus.Rows[0]["status"].ToString();

            FillIndustryFields(dtindustry, draftStatus);
            FillProdEmployee(dtProduction, dtProductionDet);
            FillInvestmentDetails(dtInvestment);
            FillMeansofFinance(dtMeansFinanceLoanDet);
            FillLandDetails(dtLandInfo, dtLandDetails);
            FillAdditionalDoc(dtAdditionalDoc);

        }
        catch (Exception)
        {
        }
    }
    #region Industry Details Bind
    public void FillIndustryFields(DataTable dtindustry, string DraftStatus)
    {
        try
        {
            if (dtindustry.Rows.Count > 0)
            {

                LblEnterPrise.Text = dtindustry.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
                LblOrgType.Text = dtindustry.Rows[0]["INDUSTRIAL_UNIT_Name"].ToString();
                LblApplicantName.Text = (dtindustry.Rows[0]["INTGENDER"].ToString() == "1" ? "Mr" : "Mrs") + " " + dtindustry.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();

                if (dtindustry.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                {
                    LblAadhaar.Text = dtindustry.Rows[0]["VCHAADHAARNO"].ToString();
                }

                LnkViewRehabilDoc.Attributes.Add("href", RetFileNamePath(dtindustry.Rows[0]["VCHREHABILITATEDDOCUMENT"].ToString(), "IndustryUnit")); /////RehabilDoc file upload View
                LnkViewIndustryUnitDoc.Attributes.Add("href", RetFileNamePath(dtindustry.Rows[0]["VCHINDUSTRIALDOCUMENT"].ToString(), "IndustryUnit")); /////IndustryUnitDoc file upload View
                LnkViewCertificateRegistration.Attributes.Add("href", RetFileNamePath(dtindustry.Rows[0]["VCHCERTIFICATEOFREGISTRATION"].ToString(), "IndustryUnit")); /////CertificateRegistration file upload View
                LnkViewCertificateCommence.Attributes.Add("href", RetFileNamePath(dtindustry.Rows[0]["VCHCOMMENCEMENTCERTIFICATE"].ToString(), "IndustryUnit")); /////CertificateCommence file upload View



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
                    LnkViewAUTHORIZEDFILE.Attributes.Add("href", RetFileNamePath(dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "IndustryUnit")); /////AUTHORIZEDFILE file upload View
                    tr_authorizing.Visible = true;
                }
                if (dtindustry.Rows[0]["INTPRIORITY"].ToString() == "1")
                {
                    LblPriority.Text = "Yes";
                    LnkViewPinoneerDoc.Attributes.Add("href", RetFileNamePath(dtindustry.Rows[0]["VCHPIONEERCERTIFICATE"].ToString(), "IndustryUnit")); /////PinoneerDoc file upload View
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
    public string RetFileNamePath(string filename, string foldername)
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
        catch (Exception)
        {
        }
        return strret;
    }
    #endregion
    #region Production & Employee
    public void FillProdEmployee(DataTable dtProduction, DataTable dtProductionDet)
    {

        #region Production Details
        if (dtProductionDet.Rows.Count > 0)
        {
            grdProduction.DataSource = dtProductionDet;
            grdProduction.DataBind();
        }
        //bind master data
        if (dtProduction.Rows.Count > 0)
        {

            LblDateProduction.Text = dtProduction.Rows[0]["DTMPRODUCTION"].ToString();

            LblLocationProd.Text = dtProduction.Rows[0]["VCHLOCATION"].ToString();
            LblStatusProd.Text = dtProduction.Rows[0]["VCHSTATUS"].ToString();
        }
        #endregion
    }
    #endregion

    #region InvestmentDetails
    public void FillInvestmentDetails(DataTable dtInvestment)
    {
        if (dtInvestment.Rows.Count > 0)
        {
            if (dtInvestment.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString() != "")
            {
                LblTimescheduleforyearofcomm.Text = Convert.ToDateTime(dtInvestment.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString()).ToString("MM/dd/yyyy");
            }
            if (dtInvestment.Rows[0]["VCH_Document_in_support"].ToString() != "")
            {
                hypDocfirstinvestment.Attributes.Add("href", RetFileNamePath(dtInvestment.Rows[0]["VCH_Document_in_support"].ToString(), "investment"));
            }
            if (dtInvestment.Rows[0]["VCH_PROJECTDOC"].ToString() != "")
            {
                hypLinkApprovedDetailDoc.Attributes.Add("href", RetFileNamePath(dtInvestment.Rows[0]["VCH_PROJECTDOC"].ToString(), "investment"));
            }
            LblLandtype.Text = dtInvestment.Rows[0]["VCH_LAND_TYPE"].ToString();
            txtLandtype.Text = dtInvestment.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
            txtBuilding.Text = dtInvestment.Rows[0]["DEC_Building"].ToString();
            txtPlantMachinery.Text = dtInvestment.Rows[0]["DEC_Plant_Machinery"].ToString();
            txtOtherFixedAssests.Text = dtInvestment.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
            lblTotalAmount.Text = dtInvestment.Rows[0]["DEC_Total"].ToString();

        }
    }
    #endregion
    #region MeansofFinance
    public void FillMeansofFinance(DataTable dtMeansFinanceLoanDet)
    {
        if (dtMeansFinanceLoanDet.Rows.Count > 0)
        {
            grdMeansOfFinance.DataSource = dtMeansFinanceLoanDet;
            grdMeansOfFinance.DataBind();
        }
    }
    #endregion
    #region Land Details
    public void FillLandDetails(DataTable dtLandInfo, DataTable dtLandDetails)
    {
        try
        {
            if (dtLandInfo.Rows.Count > 0)
            {
                LblCostofProject.Text = dtLandInfo.Rows[0]["VCHCOSTOFPROJECT"].ToString();
                LblLandRequiredAsperRpt.Text = dtLandInfo.Rows[0]["VCHLANDAREAPERPROJECT"].ToString();
                LblLandRequired.Text = dtLandInfo.Rows[0]["VCHLANDAREA"].ToString();
                if (dtLandInfo.Rows[0]["VCHLANDDOCUMENT"].ToString() != "")
                {
                    lnkLandfileview.Attributes.Add("href", RetFileNamePath(dtLandInfo.Rows[0]["VCHLANDDOCUMENT"].ToString(), "LandDocument"));
                }

            }
            if (dtLandDetails.Rows.Count > 0)
            {
                grvLandInfo.DataSource = dtLandDetails;
                grvLandInfo.DataBind();
            }
        }
        catch (Exception)
        {

        }
    }
    #endregion
    #region AdditionalDoc
    public void FillAdditionalDoc(DataTable dtAdditionalDoc)
    {
        try
        {
            lknSatutoryClean.Attributes.Add("href", RetFileNamePath(dtAdditionalDoc.Rows[0]["VCHCLEARANCECETIFTOSPCB"].ToString(), "AdditionalDocument"));
        }
        catch (Exception)
        {
        }
    } 
    #endregion

}