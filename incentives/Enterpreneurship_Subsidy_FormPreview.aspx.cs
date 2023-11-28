using System;
using System.Linq;
using System.Web.UI;
using EntityLayer.Incentive;
using System.Data;
using System.IO;
using System.Collections.Generic;

public partial class incentives_Enterpreneurship_Subsidy_FormPreview : SessionCheck 
{
    DataTable dtSalutation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            crdtincentive1();
            FetchAllFieldCotents();
            TRVisibility();
        }
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
        grdAssistanceDetailsAD.DataSource = dtincentive1;
        grdAssistanceDetailsAD.DataBind();
    }
    public void FetchAllFieldCotents()
    {
        try
        {
            Incentive objIncentive = new Incentive();
            GetAndViewParam objPar = new GetAndViewParam();
            objIncentive.strcActioncode = "action";
            objPar.Param1ID = Convert.ToString(Request.QueryString["IncentiveNo"]); //IncentiveNum
            objPar.Param2ID = Convert.ToString(Session["UnitCode"]);   //UnitCode
            objPar.Param3ID = Convert.ToString(Session["ProposalNo"]);   //ProposalNum
            objPar.InctType = 4;
            objIncentive.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            objIncentive.GetVwPrmtrs = objPar;
            objIncentive.FormType = FormNumber.EntreprenuershipDevelopmentSubsidy_10;

            DataSet dslive = IncentiveManager.PostpopulateEnterpreneurshipSubsidy(objIncentive);
            DataTable dtindustry = dslive.Tables[0];
            DataTable dtCourseDetails = dslive.Tables[1];
            DataTable dtAvail = dslive.Tables[2];
            DataTable dtAvailIncentive = dslive.Tables[3];
            DataTable dtBankDetails = dslive.Tables[4];
            DataTable dtdocumentDetails = dslive.Tables[5];
            DataTable dtMainTable = dslive.Tables[7];
            dtSalutation = dslive.Tables[8];
            hdnEmail.Value = dtSalutation.Rows[0]["VCH_EMAIL"].ToString();
            hdnMobile.Value = dtSalutation.Rows[0]["VCH_OFF_MOBILE"].ToString();

            FillIndustryFields(dtindustry);
            FillCourseDetails(dtCourseDetails);
            FillAvailDetails(dtAvail, dtAvailIncentive);
            FillBankDetails(dtBankDetails);
            FillDocumentToSubmit(dtdocumentDetails);

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
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveMaster(objIncentive);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtbPostSubFlag = ds.Tables[0];
                lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
                lblTitle2.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
            }

            if (dtMainTable.Rows[0]["vchProvisionalCertificate"].ToString() != "")
            {
                lnkviewProvisional.NavigateUrl = "~/Portal/Incentive/Sanctionorder/" + dtMainTable.Rows[0]["vchProvisionalCertificate"].ToString();
            }

            lblDate.Text = dtMainTable.Rows[0]["DTMCREATEDBY"].ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #region Industry Details Bind
    public void FillIndustryFields(DataTable dtindustry)
    {
        try
        {
            if (dtindustry.Rows.Count > 0)
            {
                lblUnitAddress.Text = dtindustry.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
                lblUnitAddr.Text = dtindustry.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
                lblMr.Text = dtindustry.Rows[0]["VCHENTERPRISENAME"].ToString().Trim();
                //lblName.Text = dtindustry.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();
                lblName.Text = dtindustry.Rows[0]["VCHPREAPPLICANTNAME"].ToString().Trim();
                lbl_EnterPrise_Name.Text = dtindustry.Rows[0]["vchEnterpriseName"].ToString();
                lblDistrict.Text = dtindustry.Rows[0]["distname"].ToString();
                lblDist.Text = dtindustry.Rows[0]["distname"].ToString();
                DataSet ds1 = new DataSet();
                ds1 = IncentiveManager.dynamic_name_doc_bind();
                ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustry.Rows[0]["intUnitType"].ToString() + "'";
                ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustry.Rows[0]["intOrganisationType"].ToString() + "'";
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

                lbl_Org_Type.Text = dtindustry.Rows[0]["OrgTypename"].ToString();
                lbl_Industry_Address.Text = dtindustry.Rows[0]["vchIndustryAddress"].ToString();
                lblAddress.Text = lbl_Industry_Address.Text;
                lblPresent.Text = lbl_Industry_Address.Text;
                lbl_Unit_Cat.Text = dtindustry.Rows[0]["Unitcategoryname"].ToString();
        
                lbl_Unit_Type.Text = dtindustry.Rows[0]["UnitTypename"].ToString();
                if (dtindustry.Rows[0]["intPriority"].ToString() == "1")
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
                lblIs_Is_Pioneer.Text = (dtindustry.Rows[0]["intPioneer"].ToString() == "1") ? "Yes" : "No";
                TxtApplicantName.Text = dtindustry.Rows[0]["VCHPREAPPLICANTNAME"].ToString().Trim();
                //lblGenderype.Text = dtindustry.Rows[0]["GenderType"].ToString();
                lbl_Regd_Office_Address.Text = dtindustry.Rows[0]["vchRegisteredOfcAddress"].ToString();   
                lbl_Gender_Partner.Text = dtindustry.Rows[0]["GenderType"].ToString() + " " + dtindustry.Rows[0]["vchManagingPartnerName"].ToString();
                lblApplyBy.Text = (dtindustry.Rows[0]["INTAPPLYBY"].ToString() == "1") ? "Self" : "Authorized Person";

                divadhhardetails.Visible = false;
                divAuthorizing.Visible = false;
                DivAppDocType.Visible = false;
                LblAadhaar.Text = dtindustry.Rows[0]["VCHAADHAARNO"].ToString().Trim();
                lblApplyBy.Text = dtindustry.Rows[0]["INTAPPLYBY"].ToString() == "1" ? "Self" : "Authorized Person";
                if (dtindustry.Rows[0]["INTAPPLYBY"].ToString() == "1")
                {
                    divadhhardetails.Visible = true;
                }
                else
                {
                    divAuthorizing.Visible = true;
                    DivAppDocType.Visible = false;
                    LnkViewMultiselectDoc.Attributes.Add("href", RetFileNamePath(dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc"));
                    lblDocumentType.Text = dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                   // lblauthority.Text = dtindustry.Rows[0]["CertOfRegdDocName"].ToString();
                }
                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustry.Rows[0]["vchCertOfRegdDocFileName"].ToString();

                lbl_EIN_IL_NO.Text = dtindustry.Rows[0]["vchEINNO"].ToString();
                lbl_EIN_IL_Date.Text = dtindustry.Rows[0]["dtmEIN"].ToString();
                lbl_PC_No.Text = dtindustry.Rows[0]["vchPcNo"].ToString();
                lbl_Prod_Comm_Date_Before.Text = dtindustry.Rows[0]["dtmProdCommBefore"].ToString();
                lbl_PC_Issue_Date_Before.Text = dtindustry.Rows[0]["dtmPCIssueDateBefore"].ToString();

                //if (dtindustry.Rows[0]["projectType"].ToString() == "1")
                //{
                //    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustry.Rows[0]["vchappnobef"].ToString();
                //}
                //else if (dtindustry.Rows[0]["projectType"].ToString() == "2")
                //{
                //    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustry.Rows[0]["vchappnobef"].ToString();
                //}
                if (dtindustry.Rows[0]["dtmProdCommBefore"].ToString() != "")
                {
                    divbefor.Visible = true;
                    //tr_Prod_Comm_Before.Visible = true;
                }
                else
                {
                    divbefor.Visible = false;
                    //tr_Prod_Comm_Before.Visible = false;
                    lblAfterEMD11.Text = "Date of Production Commencement";
                    lblAfterEMD189.Text = "PC Issurance Date";
                    lbl_PC_No_After.Text = "PC No";
                    Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                }

                if (dtindustry.Rows[0]["vchPioneerCertificate"].ToString() != "")
                {
                    DivPioneer.Visible = true;
                    Lbl_Pioneer_Doc_Name.Text = dtindustry.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();
                    Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustry.Rows[0]["vchPioneerCertificate"].ToString();
                }
                else
                {
                    DivPioneer.Visible = false;
                }
                lbl_pcno_befor.Text = dtindustry.Rows[0]["vchpcnobefore"].ToString();
                lblGstin.Text = dtindustry.Rows[0]["VCHGSTIN"].ToString();
                lbl_Prod_Comm_Date_After.Text = dtindustry.Rows[0]["dtmProdCommAfter"].ToString();
                lbl_PC_Issue_Date_After.Text = dtindustry.Rows[0]["dtmPCIssueDateAfter"].ToString();

                if (dtindustry.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustry.Rows[0]["vchappnoAft"].ToString();
                }
                else if (dtindustry.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustry.Rows[0]["vchappnoAft"].ToString();
                }

                lbl_District.Text = dtindustry.Rows[0]["distname"].ToString();
                lblDist.Text = dtindustry.Rows[0]["distname"].ToString();

                lbl_Sector.Text = dtindustry.Rows[0]["sectorName"].ToString();

                lbl_Sub_Sector.Text = dtindustry.Rows[0]["SubsectorName"].ToString();
                Lbl_Derived_Sector.Text = dtindustry.Rows[0]["vchDerivedSector"].ToString();
                lbl_Sectoral.Text = (dtindustry.Rows[0]["bitPriorityIPR"].ToString() == "1") ? "Yes" : "No";
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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
            lblInstLocation.Text = (dtCourse.Rows[0]["VCH_CD_Location_Institute"].ToString() != "") ? dtCourse.Rows[0]["VCH_CD_Location_Institute"].ToString() : "NA";
            lblDtSelect.Text = RetDateFrmDB(dtCourse.Rows[0]["DTM_CD_Date_of_selection"].ToString());

            if (dtCourse.Rows[0]["VCH_CD_Institution_Name"].ToString() == "Others")
            {
                lblOtherInsitute.Visible = true;
                lblOtherInstitution.Visible = true;
                lblOtherInstitution.Text = dtCourse.Rows[0]["VCH_CD_Other_Institution_Name"].ToString();
            }
            else
            {
                lblOtherInsitute.Visible = false;
                lblOtherInstitution.Visible = false;
            }
            hypAttachment.Attributes.Add("href", RetFileNamePath(dtCourse.Rows[0]["VCH_CD_Course_Attachment"].ToString(), "CourseDetails")); /////RehabilDoc file upload View
            hypLinkLetterselection.Attributes.Add("href", RetFileNamePath(dtCourse.Rows[0]["VCH_CD_Copy_of_letterofselection"].ToString(), "CourseDetails")); /////IndustryUnitDoc file upload View
            lblSanctiondt.Text = dtCourse.Rows[0]["DTM_CD_Date_of_Saction_Letter"].ToString();
            lblSanctionNo.Text = dtCourse.Rows[0]["VCH_CD_Saction_Letter_No"].ToString();
        }

    }
    #endregion
    #region Document To be Submitted
    public void FillDocumentToSubmit(DataTable dtDoc)
    {
        if (dtDoc.Rows.Count > 0)
        {
            txtExcepteddateofcourse.Text = dtDoc.Rows[0]["DTM_CD_Excepteddate_of_course"].ToString();
            lnkviewcompletion.Attributes.Add("href", RetFileNamePath(dtDoc.Rows[0]["VCH_MANG_DEV_LETTER"].ToString(), "CourseDetails")); /////IndustryUnitDoc file upload View
        }
    }
    #endregion
    #region Avail Details
    public void FillAvailDetails(DataTable dtAvail, DataTable dtAvailIncentive)
    {
        if (dtAvail.Rows.Count > 0)
        {
            lbldiffclaimamt.Text = dtAvail.Rows[0]["decClaimExempted"].ToString();
            lblreimamt.Text = dtAvail.Rows[0]["decClaimReimbursement"].ToString();
            if (Convert.ToInt32(dtAvail.Rows[0]["intNeverAvailedPrior"]) == 1)
            {
                lblSubsidyEarlier.Text = "Yes";
                av1.Visible = true;
                av2.Visible = true;
                Sanc.Visible = true;
                UnderTkg.Visible = false;
                if (dtAvail.Rows[0]["VchSanctionDoc"].ToString() != "")
                {
                    lnkviewSanction.HRef = "../incentives/Files/AvailDetails/" + dtAvail.Rows[0]["VchSanctionDoc"].ToString();
                }
            }
            else
            {
                lblSubsidyEarlier.Text = "No";
                av1.Visible = false;
                av2.Visible = false;
                Sanc.Visible = false;
                UnderTkg.Visible = true;
                if (dtAvail.Rows[0]["vchUndertakingSubsidyDoc"].ToString() != "")
                {
                    lnkviewUnderTkg.HRef = "../incentives/Files/AvailDetails/" + dtAvail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                }
            }
            av3.Visible = true;
        }
        if (dtAvailIncentive.Rows.Count > 0)
        {
            DataTable dtincentive = new DataTable();
            dtincentive = (DataTable)ViewState["dtincentive"];

            grdAssistanceDetailsAD.DataSource = dtAvailIncentive;
            grdAssistanceDetailsAD.DataBind();
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
            if (dtBankDetails.Rows[0]["vchBankDoc"].ToString() != "")
            {
                hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBankDetails.Rows[0]["vchBankDoc"].ToString();
            } 
        }
    }
    #endregion
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please upload .png/.jpg/.jpeg format image only !', 'SWP'); </script>", false);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Upload Signature. !', 'SWP'); </script>", false);
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
            string strBody = lblTitle.Text + " of M/s " + lblMr.Text + " has been submitted successfully." + Environment.NewLine + PreviewURL;
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
}