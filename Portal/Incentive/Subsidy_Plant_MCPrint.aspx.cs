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
    protected void Page_Load(object sender, EventArgs e)
    {
        FillData();
    }
    #region FillData
    protected void FillData()
    {
        Incentive objEntity = new Incentive();
        GetAndViewParam objPar = new GetAndViewParam();
        objEntity.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
        objEntity.strcActioncode = "5";
        objPar.Param1ID = "B001234";
        objPar.Param2ID = "B132222";
        objPar.Param3ID = "pro222";
        objPar.InctType = 4;
        objEntity.GetVwPrmtrs = objPar;
        objEntity.FormType = FormNumber.SubsidyOnPlantAndMachinery_05;
        DataSet ds = new DataSet();
        ds = IncentiveManager.GetIncentive(objEntity);
        DataTable dtIndustry = ds.Tables[0];
        DataTable dtProduction = ds.Tables[1];
        DataTable dtProductionDet = ds.Tables[2];
        DataTable dtInvestment = ds.Tables[3];
        DataTable dtMeansFinance = ds.Tables[4];
        DataTable dtMeansFinanceLoanDet = ds.Tables[5];
        DataTable dtAvail = ds.Tables[8];
        DataTable dtAvailIncentive = ds.Tables[10];
        DataTable dtBank = ds.Tables[11];
        
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

                    hypFirstInvestment.Attributes.Add("href", RetFileNamePath("investment", dtInvestment.Rows[0]["VCH_Document_in_support"].ToString()));
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
            if (dtMeansFinance.Rows[0]["VCH_TERM_LOAN_SAC"].ToString() != "")
            {

                hypFinancial.Attributes.Add("href", RetFileNamePath("MeansOfFinance", dtMeansFinance.Rows[0]["VCH_TERM_LOAN_SAC"].ToString()));
            }
            if (dtMeansFinanceLoanDet.Rows.Count > 0)
            {
                grdMeansOfFinance.DataSource = dtMeansFinanceLoanDet;
                grdMeansOfFinance.DataBind();
            }
            #endregion
            #region Avail Details
            if (dtAvail.Rows.Count > 0)
            {
                if (dtAvail.Rows[0]["intNeverAvailedPrior"].ToString() == "1")
                {
                    radNeverAvailedPrior.Checked = true;
                    radNeverAvailedPrior.Enabled = false;
                }
                if (dtAvail.Rows[0]["intSubsidyAvailed"].ToString() == "1")
                {
                    radSubsidyAvailed.Checked = true;
                    radSubsidyAvailed.Enabled = false;
                }
                if (dtAvail.Rows[0]["vchUndertakingSubsidyDoc"].ToString() != "")
                {
                    hypAvail1.Attributes.Add("href", RetFileNamePath("AvailDetails", dtAvail.Rows[0]["vchUndertakingSubsidyDoc"].ToString()));
                }
                if (dtAvail.Rows[0]["vchSupportingDocs"].ToString() != "")
                {
                    hypAvail2.Attributes.Add("href", RetFileNamePath("AvailDetails", dtAvail.Rows[0]["vchSupportingDocs"].ToString()));
                }
            }
            if (dtAvailIncentive.Rows.Count > 0)
            {

                grdIncentiveAvailed.DataSource = dtAvailIncentive;
                grdIncentiveAvailed.DataBind();
            }
            #endregion
            #region Bank Details
            lblAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
            lblBnkNm.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
            lblBranch.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
            lblIFSC.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
            lblMICRNo.Text = dtBank.Rows[0]["VCHMICR"].ToString();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Incentive objEntity = new Incentive();
        try
        {
            string[] fileext = { ".png",".jpg",".jpeg" };
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "alert('Please upload .png/.jpg/.jpeg format image only.')", false);
                    return;
                }
              objEntity.ApprovalAction = "A";
              objEntity.UnqIncentiveId =Convert.ToInt32(hdnId.Value);
              int i = IncentiveManager.UpdateSignature(objEntity);
              ScriptManager.RegisterStartupScript(btnSave, this.GetType(), "OnClick", "<script>alert('Data Saved Successfully.');location.href='incentivelist.aspx'</script>", false); 
            }
            else
            {
                  ScriptManager.RegisterStartupScript(btnSave, this.GetType(), "OnClick", "<script>alert('Please Upload Signature.');location.href=''</script>", false); 
            }
            
            
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}