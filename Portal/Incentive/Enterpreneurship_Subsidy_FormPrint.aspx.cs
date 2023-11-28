using System;
using System.Data;
using System.IO;
using System.Linq;
using EntityLayer.Incentive;
public partial class incentives_Enterpreneurship_Subsidy_FormPreview : System.Web.UI.Page
{
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
            Incentive objincUnit = new Incentive();
            GetAndViewParam objPar = new GetAndViewParam();
            objincUnit.strcActioncode = "10";

            objPar.Param1ID = "456642";  //IncentiveNum
            objPar.Param2ID = "546463";   //UnitCode
            objPar.Param3ID = "524240";   //ProposalNum
            objPar.InctType = 4;
            objincUnit.UnqIncentiveId = 154;
            objincUnit.GetVwPrmtrs = objPar;
            objincUnit.FormType = FormNumber.EntreprenuershipDevelopmentSubsidy_10;

            DataSet dslive = IncentiveManager.GetIncentive(objincUnit);
            DataTable dtindustry = dslive.Tables[0];////////////industry panel
            DataTable dtCourseDetails = dslive.Tables[1];///////////Course Details Tran table
            DataTable dtAvail = dslive.Tables[2];///////////Avail Details Tran table
            DataTable dtAvailIncentive = dslive.Tables[4];
            DataTable dtBankDetails = dslive.Tables[5];///////////Bank Details Tran table
            DataTable dtdocumentDetails = dslive.Tables[6];///////////Document Details Tran table

            FillIndustryFields(dtindustry);
            FillCourseDetails(dtCourseDetails);
            FillAvailDetails(dtAvail, dtAvailIncentive);
            FillBankDetails(dtBankDetails);
            FillDocumentToSubmit(dtdocumentDetails);
        }
        catch (Exception)
        {
        }
    }
    #region Industry Details Bind
    public void FillIndustryFields(DataTable dtindustry)
    {
        try
        {
            if (dtindustry.Rows.Count > 0)
            {
                lbldate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
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
    #region Course Detail
    public void FillCourseDetails(DataTable dtCourse)
    {
        if (dtCourse.Rows.Count > 0)
        {

            lblInstName.Text = dtCourse.Rows[0]["VCH_CD_Institution_Name"].ToString();
            lblInstAddress.Text = dtCourse.Rows[0]["VCH_CD_Institution_Address"].ToString();
            lblCourseDur.Text = dtCourse.Rows[0]["VCH_CD_Course_Duratio"].ToString();
            lblCourseFee.Text = dtCourse.Rows[0]["DEC_CD_Course_Amount"].ToString();
            lblDtSelect.Text = dtCourse.Rows[0]["DTM_CD_Date_of_selection"].ToString();
            lblExpectDt.Text = dtCourse.Rows[0]["DTM_CD_Excepteddate_of_course"].ToString();
            hypAttachment.Attributes.Add("href", RetFileNamePath(dtCourse.Rows[0]["VCH_CD_Course_Attachment"].ToString(), "CourseDetails")); /////RehabilDoc file upload View
            hypLinkLetterselection.Attributes.Add("href", RetFileNamePath(dtCourse.Rows[0]["VCH_CD_Copy_of_letterofselection"].ToString(), "CourseDetails")); /////IndustryUnitDoc file upload View
        }

    }
    #endregion
    #region Document To be Submitted
    public void FillDocumentToSubmit(DataTable dtDoc)
    {
        if (dtDoc.Rows.Count > 0)
        {
            lnkviewProvisional.Attributes.Add("href", RetFileNamePath(dtDoc.Rows[0]["VCH_PROV_SAC_LETTER"].ToString(), "DocumentToBeSubmited")); /////RehabilDoc file upload View
            lnkviewcompletion.Attributes.Add("href", RetFileNamePath(dtDoc.Rows[0]["VCH_MANG_DEV_LETTER"].ToString(), "DocumentToBeSubmited")); /////IndustryUnitDoc file upload View
        }
    }
    #endregion
    #region Avail Details
    public void FillAvailDetails(DataTable dtAvail, DataTable dtAvailIncentive)
    {
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
                lnkviewSubsidy.Attributes.Add("href", RetFileNamePath("AvailDetails", dtAvail.Rows[0]["vchUndertakingSubsidyDoc"].ToString()));
            }
            if (dtAvail.Rows[0]["vchSupportingDocs"].ToString() != "")
            {
                lnkviewSubsidyAvailed.Attributes.Add("href", RetFileNamePath("AvailDetails", dtAvail.Rows[0]["vchSupportingDocs"].ToString()));
            }
        }
        if (dtAvailIncentive.Rows.Count > 0)
        {

            grdIncentiveAvailed.DataSource = dtAvailIncentive;
            grdIncentiveAvailed.DataBind();
        }

    }
            #endregion
    #region bank Detail
    public void FillBankDetails(DataTable dtBankDetails)
    {
        if (dtBankDetails.Rows.Count > 0)
        {
            lblAccNo.Text = dtBankDetails.Rows[0]["VCHACCOUNTNO"].ToString();
            lblBnkNm.Text = dtBankDetails.Rows[0]["VCHBANKNAME"].ToString();
            lblBranch.Text = dtBankDetails.Rows[0]["VCHBRANCHNAME"].ToString();
            lblIFSC.Text = dtBankDetails.Rows[0]["VCHIFSCNO"].ToString();
            lblMICRNo.Text = dtBankDetails.Rows[0]["VCHMICR"].ToString();
        }
    }
    #endregion

    protected void btnApply_Click(object sender, EventArgs e)
    {

        Incentive objEntity = new Incentive();
        try
        {
            string[] fileext = { ".png", ".jpg", ".jpeg" };
            if (FluSign.HasFile)
            {
                if (fileext.Contains(System.IO.Path.GetExtension(FluSign.FileName).ToLower()))
                {
                    bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/Signature/"));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath("../incentives/Files/Signature/"));

                    string extension = Path.GetExtension(FluSign.PostedFile.FileName);
                    string filename = "Signature" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
                    FluSign.SaveAs(Server.MapPath("../incentives/Files/Signature/") + filename);
                    objEntity.Signature = filename;
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('Please upload .png/.jpg/.jpeg format image only.')", true);
                    return;
                }
                objEntity.ApprovalAction = "A";
                objEntity.UnqIncentiveId = Convert.ToInt32(hdnId.Value);
                int i = IncentiveManager.UpdateSignature(objEntity);
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('Data Saved Successfully.')", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('Please Upload Signature.')", true);
            }
        }
        catch (Exception)
        {
            throw;
        }

    }
}