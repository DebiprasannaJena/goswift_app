using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAcessLayer.Common;
using EntityLayer.Incentive;
using System.Collections.Specialized;
using BusinessLogicLayer.Incentive;

public partial class Enterpreneurship_Subsidy : SessionCheck
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    #region Member Variable
    Incentive objIncentive = new Incentive();
    static DataTable dtbInsttuteLo = null;
    string strMsg = "Incentive";
    string resdt = "1";
    #endregion
    #region "PageEvent"
    protected void Page_Load(object sender, EventArgs e)
    {
        txtDateofselection.Attributes.Add("readonly", "readonly");
        txtSanctiondt.Attributes.Add("readonly", "readonly");
        txtsacdat.Attributes.Add("readonly", "readonly");
        txtExcepteddateofcourse.Attributes.Add("readonly", "readonly");
        if (!IsPostBack)
        {
            fillSalutation();
            txtOtherInstitution.Visible = false;
            crdtincentive();
            GetMasterdetails();
            BindData();
            ChkProvisionalSanctionStatus();
            if (Convert.ToString(Session["ApplySource"]) == "0")
            {
                PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));   //draft
            }
            else
            {
                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
                PrepopulateDataPlus(Convert.ToInt16(Session["InvestorId"]));
            }
        }
    }
    #endregion
    #region "Methods"
    public void PrepopulateDataPlus(int id)
    {
        try
        {
            DataSet dslivePre = IncentiveManager.PostpopulateDataPLUS(id);
            DataTable dtBank = dslivePre.Tables[0];
            if (dtBank.Rows.Count > 0)
            {
                PreBankPlus(dtBank);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void PreBankPlus(DataTable dtBank)
    {
        txtAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
        txtBnkNm.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
        txtBranch.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
        txtIFSC.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
        txtMICRNo.Text = dtBank.Rows[0]["VCHMICR"].ToString();
        if (dtBank.Rows[0]["vchBankDoc"].ToString() != "")
        {
            hdnBank.Value = dtBank.Rows[0]["vchBankDoc"].ToString();
            hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBank.Rows[0]["vchBankDoc"].ToString();
            hypBank.Visible = true;
            lnkBankDelete.Visible = true;
            fuBank.Enabled = false;
        }
    }
    public void PrepopulateData(int id)
    {
        DataSet ds = IncentiveManager.PrepopulateData(id);
        DataTable dtindustry = ds.Tables[0];


        if (dtindustry.Rows.Count > 0)
        {
            lbl_EnterPrise_Name.Text = dtindustry.Rows[0]["vchEnterpriseName"].ToString();
            DataSet ds1 = new DataSet();
            ds1 = IncentiveManager.dynamic_name_doc_bind();
            ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustry.Rows[0]["intUnitType"].ToString() + "'";
            ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustry.Rows[0]["intOrganisationType"].ToString() + "'";
            DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
            if (dt.Rows.Count > 0)
            {
                Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                Lbl_Org_Doc_Type.Text = dt.Rows[0]["vch_orgdocumentname"].ToString();
                Hid_Org_Doc_Type.Value = dt.Rows[0]["vchDocumentType"].ToString();
                lblAuthorizing.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                hidAuthorizing.Value = dt.Rows[0]["vchDocumentType"].ToString();
            }
            else
            {
                Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                Hid_Org_Doc_Type.Value = "";
            }

            hdnRadibutton.Value = "0";

            lbl_Org_Type.Text = dtindustry.Rows[0]["OrgTypename"].ToString();
            lbl_Industry_Address.Text = dtindustry.Rows[0]["vchIndustryAddress"].ToString();
            lbl_Unit_Cat.Text = dtindustry.Rows[0]["Unitcategoryname"].ToString();
            Lbl_Pioneer_Doc_Name.Text = dtindustry.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();

            dt = (ds1.Tables[1].DefaultView).ToTable();
            if (dt.Rows.Count > 0)
            {
                string strDocType = dt.Rows[0]["vchDocumentType"].ToString();
                if (strDocType != "")
                {
                    Div_Unit_Type_Doc.Visible = true;
                    Lbl_Unit_Type_Doc_Name.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                    Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();

                    Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustry.Rows[0]["vchUnitTypeDoc"].ToString();
                }
                else
                {
                    Div_Unit_Type_Doc.Visible = false;
                    Lbl_Unit_Type_Doc_Name.Text = "";
                    Hid_Unit_Type_Doc_Code.Value = "";
                }
            }
            else
            {
                Div_Unit_Type_Doc.Visible = false;
                Lbl_Unit_Type_Doc_Name.Text = "";
                Hid_Unit_Type_Doc_Code.Value = "";
            }
            lbl_Unit_Type.Text = dtindustry.Rows[0]["UnitTypename"].ToString();
            if (dtindustry.Rows[0]["intPriority"].ToString() == "1")
            {
                lblIs_Priority.Text = "Yes";
                Pioneersec.Visible = true;
            }
            else
            {
                lblIs_Priority.Text = "No";
                Pioneersec.Visible = false;
            }
            if (dtindustry.Rows[0]["intPioneer"].ToString() == "1")
            {
                lblIs_Is_Pioneer.Text = "Yes";
            }
            else
            {
                lblIs_Is_Pioneer.Text = "No";
            }
            Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustry.Rows[0]["vchPioneerCertificate"].ToString();
            lbl_Regd_Office_Address.Text = dtindustry.Rows[0]["vchRegisteredOfcAddress"].ToString();

            lbl_Gender_Partner.Text = dtindustry.Rows[0]["GenderType"].ToString() + " " + dtindustry.Rows[0]["vchManagingPartnerName"].ToString();

            DdlGender.SelectedValue = dtindustry.Rows[0]["vchManagingPartnerGender"].ToString();
            TxtApplicantName.Text = dtindustry.Rows[0]["vchManagingPartnerName"].ToString();


            Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustry.Rows[0]["vchCertOfRegdDocFileName"].ToString();

            lbl_EIN_IL_NO.Text = dtindustry.Rows[0]["vchEINNO"].ToString();
            lbl_EIN_IL_Date.Text = dtindustry.Rows[0]["dtmEIN"].ToString();
            lbl_PC_No.Text = dtindustry.Rows[0]["vchPcNo"].ToString();
            lbl_pcno_befor.Text = dtindustry.Rows[0]["vchpcnobefore"].ToString();
            lblGstin.Text = dtindustry.Rows[0]["VCHGSTIN"].ToString();
            lbl_Prod_Comm_Date_Before.Text = dtindustry.Rows[0]["dtmProdCommBefore"].ToString();
            lbl_PC_Issue_Date_Before.Text = dtindustry.Rows[0]["dtmPCIssueDateBefore"].ToString();
            if (dtindustry.Rows[0]["projectType"].ToString() == "1")
            {
                Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustry.Rows[0]["vchappnobef"].ToString();
            }
            else if (dtindustry.Rows[0]["projectType"].ToString() == "2")
            {
                Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustry.Rows[0]["vchappnobef"].ToString();
            }

            if (dtindustry.Rows[0]["dtmProdCommBefore"].ToString() != "")
            {
                divbefor.Visible = true;
            }
            else
            {
                divbefor.Visible = false;
                lblAfterEMD11.Text = "Date of Production Commencement";
                lblAfterEMD189.Text = "PC Issuance Date";
                lbl_PC_No_After.Text = "PC No";
                Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
            }

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
            lbl_Sector.Text = dtindustry.Rows[0]["sectorName"].ToString();
            lbl_Sub_Sector.Text = dtindustry.Rows[0]["SubsectorName"].ToString();
            Lbl_Derived_Sector.Text = dtindustry.Rows[0]["vchDerivedSector"].ToString();

            if (dtindustry.Rows[0]["bitPriorityIPR"].ToString() == "1")
            {
                lbl_Sectoral.Text = "Yes";
            }
            else
            {
                lbl_Sectoral.Text = "No";
            }
        }
    }
    public string InsertUpdateData()
    {
        try
        {
            INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();

            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objIncentive.UnqIncentiveId = 0;
            }
            else
            {
                objIncentive.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }
            objIncentive.strcActioncode = "A";
            objIncentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objIncentive.PealNum = Convert.ToString(Session["ProposalNo"]);
            objIncentive.PCNum = Convert.ToString(Session["PCNo"]);
            objIncentive.UnitCode = Convert.ToString(Session["UnitCode"]);
            objIncentive.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objIncentive.Userid = Convert.ToInt16(Session["InvestorId"]);
            objIncentive.Createdby = Convert.ToInt16(Session["InvestorId"]);
            //if (!String.IsNullOrEmpty(Session["FyYear"].ToString()))
            //    objIncentive.FYear = Convert.ToInt16(Session["FyYear"]);
            objIncentive.incentivetype = 4;
            objIncentive.FormType = FormNumber.EntreprenuershipDevelopmentSubsidy_10;

            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            if (DdlGender.SelectedIndex > 0)
            {
                objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            }
            if (radApplyBy.SelectedIndex >= 0)
            {
                objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }
            objINDUSTRIAL_UNIT_MASTER.AADHAARNO_IND = TxtAdhaar1.Text.Trim();
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objIncentive.IndsutUnitMstDet = objINDUSTRIAL_UNIT_MASTER;

            ////////------------------Course Detail ---------------------------------------
            Asign(objIncentive);
            ///////------------------Avail Detail----------------------------------------
            AvailDetail(objIncentive);
            ////////------------------Bank Detail ---------------------------------------
            BankDetail(objIncentive);
            ////////------------------Documents to be submitted after completion of course ---------------------------------------
            SetDocument(objIncentive);
            objIncentive.FileUploadDetails = getFileUploadDatatable();
            string retval = IncentiveManager.CreateIncentiveEntSubsidy(objIncentive);
            return retval;
        }

        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
        }
        return null;
    }
    public void GetMasterdetails()
    {
        Incentive objIncentive = new Incentive();
        objIncentive.strcActioncode = "M";
        objIncentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
        DataSet ds = new DataSet();
        ds = IncentiveManager.GetIncentiveMaster(objIncentive);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dtbPostSubFlag = ds.Tables[0];
            hdnPostSubFlag.Value = dtbPostSubFlag.Rows[0]["intPostSubmissionFlag"].ToString();
            hdnTimeFrame.Value = dtbPostSubFlag.Rows[0]["intTimeFrame"].ToString();
            lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
        }
    }
    public void ChkProvisionalSanctionStatus()
    {
        Incentive objIncentive = new Incentive();
        objIncentive.strcActioncode = "P";
        objIncentive.IncentiveNum = Request.QueryString["InctUniqueNo"];
        DataSet ds = new DataSet();
        ds = IncentiveManager.GetIncentiveMaster(objIncentive);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataTable dtbPostSubFlag = ds.Tables[0];
            if (dtbPostSubFlag.Rows[0]["ISPROVISIONAL"].ToString() == "1" && dtbPostSubFlag.Rows[0]["INTSTATUS"].ToString() == "5")
            {
                hdnIsProvisional.Value = dtbPostSubFlag.Rows[0]["ISPROVISIONAL"].ToString();
                hdnProvisionalDoc.Value = dtbPostSubFlag.Rows[0]["vchProvisionalCertificate"].ToString();
                GetCourseCotents();
                btnProvisional.Visible = false;
                btnApply.Enabled = false;
                btnDraft.Enabled = false;
                if (dtbPostSubFlag.Rows[0]["vchProvisionalCertificate"].ToString() != "")
                {
                    hdnSanctionLetter.Value = dtbPostSubFlag.Rows[0]["vchProvisionalCertificate"].ToString();
                    hypSanctionLetter.NavigateUrl = "~/Portal/Incentive/Sanctionorder/" + dtbPostSubFlag.Rows[0]["vchProvisionalCertificate"].ToString();
                    fuSanctionLetter.Enabled = false;
                    lnkSanctionLetterDelete.Visible = true;
                    hypSanctionLetter.Visible = true;
                    btnApply.Enabled = true;
                    btnDraft.Enabled = true;
                }
            }
            else
            {
                btnApply.Enabled = false;
                btnDraft.Enabled = false;
            }
        }
        else
        {
            btnApply.Enabled = false;
            btnDraft.Enabled = false;
        }
    }
    public void GetCourseCotents()
    {
        GetAndViewParam objPar = new GetAndViewParam();
        objIncentive.strcActioncode = "10";

        objPar.Param1ID = Convert.ToString(Request.QueryString["IncentiveNo"]); //IncentiveNum
        objPar.Param2ID = Convert.ToString(Session["UnitCode"]);   //UnitCode
        objPar.Param3ID = Convert.ToString(Session["ProposalNo"]);   //ProposalNum
        objPar.InctType = 4;
        objIncentive.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
        objIncentive.GetVwPrmtrs = objPar;
        objIncentive.FormType = FormNumber.EntreprenuershipDevelopmentSubsidy_10;

        DataSet dslive = IncentiveManager.PostpopulateEnterpreneurshipSubsidy(objIncentive);
        DataTable dtCourseDetails = dslive.Tables[1];///////////Course Details Tran table
        FillCourseDetail(dtCourseDetails);
    }
    public void BindData()
    {
        DataTable table = new DataTable();
        try
        {
            CommonDataLayer objDataUnit = new CommonDataLayer();
            ddlInstitute.DataTextField = "vchName";
            ddlInstitute.DataValueField = "slno";
            ddlInstitute.DataSource = objDataUnit.FillUnitType("E");
            ddlInstitute.DataBind();
            ddlInstitute.Items.Insert(0, new ListItem("-Select-", "0"));

            ddlInsttuteLoc.DataTextField = "InstitutionLocation";
            ddlInsttuteLoc.DataValueField = "intLocationID";
            dtbInsttuteLo = objDataUnit.FillUnitType("L");
            ddlInsttuteLoc.DataSource = dtbInsttuteLo;
            ddlInsttuteLoc.DataBind();
            ddlInsttuteLoc.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            table = null;
        }
    }
    void crdtincentive()
    {
        DataTable dtincentive = new DataTable();

        DataColumn dcRowId = new DataColumn("dcRowId");
        dcRowId.DataType = Type.GetType("System.Int32");
        dcRowId.AutoIncrement = true;
        dcRowId.AutoIncrementSeed = 1;
        dcRowId.AutoIncrementStep = 1;
        dtincentive.Columns.Add(dcRowId);

        DataColumn vchagency = new DataColumn("vchagency");
        vchagency.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchagency);

        DataColumn vchsacamt = new DataColumn("vchsacamt");
        vchsacamt.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchsacamt);

        DataColumn vchsacord = new DataColumn("vchsacord");
        vchsacord.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchsacord);

        DataColumn vchsacdat = new DataColumn("vchsacdat");
        vchsacdat.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchsacdat);

        DataColumn vchavilamt = new DataColumn("vchavilamt");
        vchavilamt.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchavilamt);

        ViewState["dtincentive"] = dtincentive;
        grdAssistanceDetailsAD.DataSource = dtincentive;
        grdAssistanceDetailsAD.DataBind();
    }
    public string ReturnDateFormat(string srcDate)
    {
        string resdt = "1900/01/01";
        try
        {
            if (srcDate != "")
            {
                string[] strarr = srcDate.Split('/');
                resdt = strarr[2] + "/" + strarr[0] + "/" + strarr[1];
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

        return resdt;
    }
    public void Asign(Incentive objIncentive)
    {
        try
        {
            objIncentive.CourseDet = new EntityLayer.Incentive.CourseDetails();

            if (fuSanctionLetter.HasFile)  //done
            {
                objIncentive.CourseDet.ProvSacLetter = hdnSanctionLetter.Value;
            }
            else
            {
                if (hdnSanctionLetter.Value != "")
                    objIncentive.CourseDet.ProvSacLetter = lnkSanctionLetterDoc.Text;
            }
            if (fuCopyletterselection.HasFile)  //done
            {
                objIncentive.CourseDet.Copyofletterofselection = hdnCopyletterselection.Value;
            }
            else
            {
                if (hdnCopyletterselection.Value != "")
                    objIncentive.CourseDet.Copyofletterofselection = hdnCopyletterselection.Value;
            }

            if (fuAttachment.HasFile)  //done
            {
                objIncentive.CourseDet.CourseAttachment = hdnAttachmentDoc.Value;
            }
            else
            {
                if (hdnAttachmentDoc.Value != "")
                    objIncentive.CourseDet.CourseAttachment = hdnAttachmentDoc.Value;
            }

            if (txtCourseFee.Text != "")
            {
                objIncentive.CourseDet.CourseAmount = Convert.ToDecimal(txtCourseFee.Text);
            }

            objIncentive.CourseDet.CourseDuratio = txtCourseDuration.Text;
            if (txtDateofselection.Text != "")
            {
                objIncentive.CourseDet.Dateofselection = txtDateofselection.Text;
            }

            objIncentive.CourseDet.InstitutionAddress = txtAddress.Text;

            if (ddlInstitute.SelectedItem.ToString() == "Others")
            {
                objIncentive.CourseDet.OtherInstitutionName = txtOtherInstitution.Text;
                objIncentive.CourseDet.InstitutionName = Convert.ToInt32(ddlInstitute.SelectedValue);
            }
            else
            {
                objIncentive.CourseDet.OtherInstitutionName = "";
                objIncentive.CourseDet.InstitutionName = Convert.ToInt32(ddlInstitute.SelectedValue);
            }
            if (ddlInsttuteLoc.SelectedIndex != -1)
            {
                objIncentive.CourseDet.InstitutionLocation = Convert.ToInt32(ddlInsttuteLoc.SelectedValue);
            }
            if (txtSanctionLetterNo.Text != "")
            {
                objIncentive.CourseDet.SanctionNo = txtSanctionLetterNo.Text;
            }
            if (txtSanctiondt.Text != "")
            {
                objIncentive.CourseDet.DateofSanction = txtSanctiondt.Text;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void AvailDetail(Incentive objEntity)
    {
        AvailDetails objAvailDetails = new AvailDetails();
        List<Assistance> listIncentiveAvailed = new List<Assistance>();
        Assistance objIncentiveAvailed = new Assistance();

        objAvailDetails.NeverAvailedPrior = Convert.ToInt16(RadBtn_Availed_Earlier.SelectedValue);
        objAvailDetails.SubsidyAvailed = 0;
        if (txtreimamt.Text != "")
            objAvailDetails.ClaimReimbursement = Convert.ToDouble(txtreimamt.Text.Trim());
        if (RadBtn_Availed_Earlier.SelectedValue == "1")
        {
            if (txtdiffclaimamt.Text != "")
                objAvailDetails.ClaimtExempted = Convert.ToDouble(txtdiffclaimamt.Text.Trim());

            objAvailDetails.SupportingDocs = Hid_Asst_Sanc_File_Name.Value;
            DataTable dtincentive = new DataTable();
            dtincentive = (DataTable)ViewState["dtincentive"];
            if (dtincentive.Rows.Count > 0)
            {
                foreach (DataRow dr in dtincentive.Rows)
                {
                    objIncentiveAvailed = new Assistance();

                    objIncentiveAvailed.InstitutionName = dr["vchagency"].ToString();
                    if (dr["vchsacamt"].ToString().Trim() != "")
                    {
                        objIncentiveAvailed.SanctionedAmount = Convert.ToDouble(dr["vchsacamt"].ToString());
                    }
                    else
                    {
                        objIncentiveAvailed.SanctionedAmount = 0;
                    }
                    if (dr["vchavilamt"].ToString().Trim() != "")
                    {
                        objIncentiveAvailed.AmountAvailed = Convert.ToDouble(dr["vchavilamt"].ToString());
                    }
                    else
                    {
                        objIncentiveAvailed.AmountAvailed = 0;
                    }
                    objIncentiveAvailed.SanctionOrderNo = dr["vchsacord"].ToString();
                    objIncentiveAvailed.AvailedDate = Convert.ToDateTime(dr["vchsacdat"].ToString());

                    listIncentiveAvailed.Add(objIncentiveAvailed);
                }
            }
            objAvailDetails.IncentiveAvailed = listIncentiveAvailed;
        }
        else
        {
            objAvailDetails.UndertakingSubsidyDoc = Hid_Undertaking_File_Name.Value;
        }
        objEntity.AvailDet = objAvailDetails;
    }
    public void BankDetail(Incentive incentive)
    {
        try
        {
            incentive.BankDet = new EntityLayer.Incentive.BankDetails();
            incentive.BankDet.BankName = txtBnkNm.Text;
            incentive.BankDet.BranchName = txtBranch.Text;
            incentive.BankDet.IFSCNo = txtIFSC.Text;
            incentive.BankDet.AccountNo = txtAccNo.Text;
            incentive.BankDet.MICRNo = txtMICRNo.Text;
            if (hdnBank.Value != "")
            {
                incentive.BankDet.BankDoc = hdnBank.Value;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void SetDocument(Incentive incentive)
    {
        incentive.DocSubAftCompDet = new EntityLayer.Incentive.DocumentSubmittedAftComp();
        if (txtExcepteddateofcourse.Text != "")
        {
            incentive.DocSubAftCompDet.Excepteddateofcourse = txtExcepteddateofcourse.Text;
        }

        if (fuCoursecomplitation.HasFile)  //done
        {
            incentive.DocSubAftCompDet.ManagementDevSuceLetter = hdnCoursecomplitation.Value;
        }
        else
        {
            if (hdnCoursecomplitation.Value != "")
                incentive.DocSubAftCompDet.ManagementDevSuceLetter = hdnCoursecomplitation.Value;
        }
    }
    private List<lstFileUpload> getFileUploadDatatable()
    {
        List<lstFileUpload> listItmProp = new List<lstFileUpload>();
        DataTable dtFiles = new DataTable() { TableName = "dtFiles" };
        dtFiles.Columns.Add(new DataColumn("id")
        {
            AutoIncrement = true,
            AutoIncrementSeed = 1,
            AutoIncrementStep = 1
        });
        dtFiles.Columns.Add(new DataColumn("vchDocId"));
        dtFiles.Columns.Add(new DataColumn("vchFileName"));
        dtFiles.Columns.Add(new DataColumn("vchFilePath"));
        if (Hid_Asst_Sanc_File_Name.Value != "")
        {
            DataRow dorgRowT = dtFiles.NewRow();
            dorgRowT["vchDocId"] = hdnSupportingDocsID.Value;
            dorgRowT["vchFileName"] = Hid_Asst_Sanc_File_Name.Value;
            dorgRowT["vchFilePath"] = "Files/AvailDetails/";
            dtFiles.Rows.Add(dorgRowT);
        }
        if (hdnAttachmentDoc.Value != "")
        {
            DataRow dorgRowT = dtFiles.NewRow();
            dorgRowT["vchDocId"] = hdnAttachmentDocDocId.Value;
            dorgRowT["vchFileName"] = hdnAttachmentDoc.Value;
            dorgRowT["vchFilePath"] = "Files/CourseDetails/";
            dtFiles.Rows.Add(dorgRowT);
        }
        if (hdnCopyletterselection.Value != "")
        {
            DataRow dorgRowT = dtFiles.NewRow();
            dorgRowT["vchDocId"] = hdnCopyletterselectionDocId.Value;
            dorgRowT["vchFileName"] = hdnCopyletterselection.Value;
            dorgRowT["vchFilePath"] = "Files/CourseDetails/";
            dtFiles.Rows.Add(dorgRowT);
        }
        if (hdnSanctionLetter.Value != "")
        {
            DataRow dorgRowT = dtFiles.NewRow();
            dorgRowT["vchDocId"] = hdnSanctionLetterDocId.Value;
            dorgRowT["vchFileName"] = hdnSanctionLetter.Value;
            dorgRowT["vchFilePath"] = "Files/CourseDetails/";
            dtFiles.Rows.Add(dorgRowT);
        }
        if (hdnAUTHORIZEDFILE.Value != "")
        {
            DataRow dorgRowT = dtFiles.NewRow();
            dorgRowT["vchDocId"] = hidAuthorizing.Value;
            dorgRowT["vchFileName"] = hdnAUTHORIZEDFILE.Value;
            dorgRowT["vchFilePath"] = "Files/InctBasicDoc/";
            dtFiles.Rows.Add(dorgRowT);
        }
        if (hdnCoursecomplitation.Value != "")
        {
            DataRow dorgRowT = dtFiles.NewRow();
            dorgRowT["vchDocId"] = hdnCoursecomplitationId.Value;
            dorgRowT["vchFileName"] = hdnCoursecomplitation.Value;
            dorgRowT["vchFilePath"] = "Files/CourseDetails/";
            dtFiles.Rows.Add(dorgRowT);
        }

        if (dtFiles.Rows.Count > 0)
        {
            listItmProp = dtFiles.AsEnumerable().Select(m => new lstFileUpload()
            {
                id = m.Field<int>("id"),
                vchDocId = m.Field<string>("vchDocId"),
                vchFileName = m.Field<string>("vchFileName"),
                vchFilePath = m.Field<string>("vchFilePath")

            }).ToList();
        }
        return listItmProp;
    }
    public void PostpopulateData(int id)
    {
        try
        {
            Incentive objIncentive = new Incentive();
            GetAndViewParam objPar = new GetAndViewParam();

            objPar.Param1ID = "";   //IncentiveNum
            objPar.Param2ID = "";   //UnitCode
            objPar.Param3ID = "";   //ProposalNum
            objPar.InctType = 4;
            objIncentive.UnqIncentiveId = id;
            objIncentive.GetVwPrmtrs = objPar;
            objIncentive.FormType = FormNumber.EntreprenuershipDevelopmentSubsidy_10;

            DataSet dslive = IncentiveManager.PostpopulateEnterpreneurshipSubsidy(objIncentive);
            DataTable dtindustry = dslive.Tables[0];////////////industry panel
            DataTable dtCourseDetails = dslive.Tables[1];///////////Course Details Tran table
            DataTable dtAvail = dslive.Tables[2];///////////Avail Details Tran table
            DataTable dtAvailIncentive = dslive.Tables[3];
            DataTable dtBankDetails = dslive.Tables[4];///////////Bank Details Tran table
            DataTable dtdocumentDetails = dslive.Tables[5];///////////Document Details Tran table

            FillIndustryFields(dtindustry);
            FillCourseDetail(dtCourseDetails);
            FillAvailDetails(dtAvail, dtAvailIncentive);
            FillBankDetails(dtBankDetails);
            FillDocumentToSubmit(dtdocumentDetails);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void FillCourseDetail(DataTable dtCourseDetails)
    {
        if (dtCourseDetails.Rows.Count > 0)
        {
            txtAddress.Text = dtCourseDetails.Rows[0]["VCH_CD_Institution_Address"].ToString();
            txtCourseDuration.Text = dtCourseDetails.Rows[0]["VCH_CD_Course_Duratio"].ToString();
            txtCourseFee.Text = dtCourseDetails.Rows[0]["DEC_CD_Course_Amount"].ToString();
            txtDateofselection.Text = dtCourseDetails.Rows[0]["DTM_CD_Date_of_selection"].ToString();

            if (dtCourseDetails.Rows[0]["VCH_CD_Institution_Name"].ToString() == "Others")
            {
                ddlInstitute.SelectedValue = dtCourseDetails.Rows[0]["Int_CD_Institution_Name"].ToString();
                txtOtherInstitution.Visible = true;
                txtOtherInstitution.Text = dtCourseDetails.Rows[0]["VCH_CD_Other_Institution_Name"].ToString();
                ddlInsttuteLoc.Enabled = false;
            }
            else
            {
                ddlInstitute.SelectedValue = dtCourseDetails.Rows[0]["Int_CD_Institution_Name"].ToString();
                txtOtherInstitution.Visible = false;
                txtOtherInstitution.Text = "";
                ddlInsttuteLoc.Enabled = true;
            }

            ddlInsttuteLoc.SelectedValue = dtCourseDetails.Rows[0]["Int_CD_Location_Institute"].ToString();
            if (dtCourseDetails.Rows[0]["VCH_CD_Course_Attachment"].ToString() != "")
            {
                hypAttachmentDoc.NavigateUrl = "../incentives/Files/CourseDetails/" + dtCourseDetails.Rows[0]["VCH_CD_Course_Attachment"].ToString();
                hdnAttachmentDoc.Value = dtCourseDetails.Rows[0]["VCH_CD_Course_Attachment"].ToString();
                fuAttachment.Enabled = false;
                lnkAttachmentDocDelete.Visible = true;
                hypAttachmentDoc.Visible = true;
            }
            if (dtCourseDetails.Rows[0]["VCH_CD_Copy_of_letterofselection"].ToString() != "")
            {
                hdnCopyletterselection.Value = dtCourseDetails.Rows[0]["VCH_CD_Copy_of_letterofselection"].ToString();
                hypCopyletterselection.NavigateUrl = "../incentives/Files/CourseDetails/" + dtCourseDetails.Rows[0]["VCH_CD_Copy_of_letterofselection"].ToString();
                fuCopyletterselection.Enabled = false;
                lnkCopyletterselectionDelete.Visible = true;
                hypCopyletterselection.Visible = true;
            }
            txtSanctiondt.Text = dtCourseDetails.Rows[0]["DTM_CD_Date_of_Saction_Letter"].ToString();
            txtSanctionLetterNo.Text = dtCourseDetails.Rows[0]["VCH_CD_Saction_Letter_No"].ToString();
        }
    }
    public void FillDocumentToSubmit(DataTable dtdocumentDetails)
    {
        if (dtdocumentDetails.Rows.Count > 0)
        {
            txtExcepteddateofcourse.Text = dtdocumentDetails.Rows[0]["DTM_CD_Excepteddate_of_course"].ToString();

            if (dtdocumentDetails.Rows[0]["VCH_MANG_DEV_LETTER"].ToString() != "")
            {
                hypCoursecomplitation.NavigateUrl = "../incentives/Files/CourseDetails/" + dtdocumentDetails.Rows[0]["VCH_MANG_DEV_LETTER"].ToString();
                hdnCoursecomplitation.Value = dtdocumentDetails.Rows[0]["VCH_MANG_DEV_LETTER"].ToString();
                fuCoursecomplitation.Enabled = false;
                lnkCoursecomplitationDocDelete.Visible = true;
                hypCoursecomplitation.Visible = true;
            }
        }

    }
    public void FillAvailDetails(DataTable dtAvail, DataTable dtAvailIncentive)
    {
        if (dtAvail.Rows.Count > 0)
        {
            RadBtn_Availed_Earlier.SelectedValue = dtAvail.Rows[0]["intNeverAvailedPrior"].ToString();
            txtreimamt.Text = dtAvail.Rows[0]["decClaimReimbursement"].ToString();
            if (dtAvail.Rows[0]["intNeverAvailedPrior"].ToString() == "1")
            {
                txtdiffclaimamt.Text = dtAvail.Rows[0]["decClaimExempted"].ToString();

                if (dtAvail.Rows[0]["VchSanctionDoc"].ToString() != "")
                {
                    Hid_Asst_Sanc_File_Name.Value = dtAvail.Rows[0]["VchSanctionDoc"].ToString();
                    Hyp_View_Asst_Sanc_Doc.NavigateUrl = "../incentives/Files/AvailDetails/" + dtAvail.Rows[0]["VchSanctionDoc"].ToString();
                    Hyp_View_Asst_Sanc_Doc.Visible = true;
                    LnkBtn_Delete_Asst_Sanc_Doc.Visible = true;
                    FU_Asst_Sanc_Doc.Enabled = false;
                }

                if (dtAvailIncentive.Rows.Count > 0)
                {
                    DataTable dtincentive = new DataTable();
                    dtincentive = (DataTable)ViewState["dtincentive"];

                    foreach (DataRow dravgr1 in dtAvailIncentive.Rows)
                    {
                        DataRow drassistant = dtincentive.NewRow();
                        drassistant["vchagency"] = dravgr1["vchInstitutionName"].ToString();
                        drassistant["vchsacamt"] = dravgr1["decSanctionedAmount"].ToString();
                        drassistant["vchsacord"] = dravgr1["vchSanctionOrderNo"].ToString();
                        drassistant["vchsacdat"] = dravgr1["dtmAvailedDate"].ToString();
                        drassistant["vchavilamt"] = dravgr1["decAmountAvailed"].ToString();
                        dtincentive.Rows.Add(drassistant);
                    }

                    ViewState["dtincentive"] = dtincentive;
                    grdAssistanceDetailsAD.DataSource = dtincentive;
                    grdAssistanceDetailsAD.DataBind();
                }
            }
            else
            {
                if (dtAvail.Rows[0]["vchUndertakingSubsidyDoc"].ToString() != "")
                {
                    Hid_Undertaking_File_Name.Value = dtAvail.Rows[0]["vchUndertakingSubsidyDoc"].ToString(); /////AUTHORIZEDFILE file upload
                    Hyp_View_Undertaking_Doc.NavigateUrl = "../incentives/Files/AvailDetails/" + dtAvail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                    Hyp_View_Undertaking_Doc.Visible = true;
                    LnkBtn_Delete_Undertaking_Doc.Visible = true;
                    FU_Undertaking_Doc.Enabled = false;
                }
            }
        }
    }
    public void FillBankDetails(DataTable dtBankDetails)
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
                hdnBank.Value = dtBankDetails.Rows[0]["vchBankDoc"].ToString();
                hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBankDetails.Rows[0]["vchBankDoc"].ToString();
                hypBank.Visible = true;
                lnkBankDelete.Visible = true;
                fuBank.Enabled = false;
            }
        }
    }
    public void FillIndustryFields(DataTable dtindustry)
    {
        try
        {
            if (dtindustry.Rows.Count > 0)
            {
                lbl_EnterPrise_Name.Text = dtindustry.Rows[0]["vchEnterpriseName"].ToString();
                DataSet ds1 = new DataSet();
                ds1 = IncentiveManager.dynamic_name_doc_bind();
                ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustry.Rows[0]["intUnitType"].ToString() + "'";
                ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustry.Rows[0]["intOrganisationType"].ToString() + "'";
                DataTable dt = (ds1.Tables[0].DefaultView).ToTable();
                if (dt.Rows.Count > 0)
                {
                    Lbl_Org_Name_Type.Text = dt.Rows[0]["vchOwnerType"].ToString();
                    Lbl_Org_Doc_Type.Text = dt.Rows[0]["vch_orgdocumentname"].ToString();
                    Hid_Org_Doc_Type.Value = dt.Rows[0]["vchDocumentType"].ToString();
                    lblAuthorizing.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                    hidAuthorizing.Value = dt.Rows[0]["vchDocumentType"].ToString();
                }
                else
                {
                    Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                    Hid_Org_Doc_Type.Value = "";
                }
                lbl_Org_Type.Text = dtindustry.Rows[0]["OrgTypename"].ToString();
                lbl_Industry_Address.Text = dtindustry.Rows[0]["vchIndustryAddress"].ToString();
                lbl_Unit_Cat.Text = dtindustry.Rows[0]["Unitcategoryname"].ToString();
                Lbl_Pioneer_Doc_Name.Text = dtindustry.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();

                hdnRadibutton.Value = dtindustry.Rows[0]["INTAPPLYBY"].ToString();

                dt = (ds1.Tables[1].DefaultView).ToTable();
                if (dt.Rows.Count > 0)
                {
                    string strDocType = dt.Rows[0]["vchDocumentType"].ToString();
                    if (strDocType != "")
                    {
                        Div_Unit_Type_Doc.Visible = true;
                        Lbl_Unit_Type_Doc_Name.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                        Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();
                        Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustry.Rows[0]["vchUnitTypeDoc"].ToString();
                    }
                    else
                    {
                        Div_Unit_Type_Doc.Visible = false;
                        Lbl_Unit_Type_Doc_Name.Text = "";
                        Hid_Unit_Type_Doc_Code.Value = "";
                    }
                }
                else
                {
                    Div_Unit_Type_Doc.Visible = false;
                    Lbl_Unit_Type_Doc_Name.Text = "";
                    Hid_Unit_Type_Doc_Code.Value = "";
                }
                lbl_Unit_Type.Text = dtindustry.Rows[0]["UnitTypename"].ToString();
                if (dtindustry.Rows[0]["intPriority"].ToString() == "1")
                {
                    lblIs_Priority.Text = "Yes";
                    Pioneersec.Visible = true;
                }
                else
                {
                    lblIs_Priority.Text = "No";
                    Pioneersec.Visible = false;
                }
                if (dtindustry.Rows[0]["intPioneer"].ToString() == "1")
                {
                    lblIs_Is_Pioneer.Text = "Yes";

                }
                else
                {
                    lblIs_Is_Pioneer.Text = "No";

                }
                Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustry.Rows[0]["vchPioneerCertificate"].ToString();
                lbl_Regd_Office_Address.Text = dtindustry.Rows[0]["vchRegisteredOfcAddress"].ToString();

                lbl_Gender_Partner.Text = dtindustry.Rows[0]["GenderType"].ToString() + " " + dtindustry.Rows[0]["vchManagingPartnerName"].ToString();

                DdlGender.SelectedValue = dtindustry.Rows[0]["vchManagingPartnerGender"].ToString();
                TxtApplicantName.Text = dtindustry.Rows[0]["vchManagingPartnerName"].ToString();
                TxtApplicantName.Text = dtindustry.Rows[0]["vchManagingPartnerName"].ToString();
                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustry.Rows[0]["vchCertOfRegdDocFileName"].ToString();
                lbl_EIN_IL_NO.Text = dtindustry.Rows[0]["vchEINNO"].ToString();
                lbl_EIN_IL_Date.Text = dtindustry.Rows[0]["dtmEIN"].ToString();
                lbl_PC_No.Text = dtindustry.Rows[0]["vchPcNo"].ToString();
                lbl_Prod_Comm_Date_Before.Text = dtindustry.Rows[0]["dtmProdCommBefore"].ToString();
                lbl_PC_Issue_Date_Before.Text = dtindustry.Rows[0]["dtmPCIssueDateBefore"].ToString();

                lbl_pcno_befor.Text = dtindustry.Rows[0]["vchpcnobefore"].ToString();
                lblGstin.Text = dtindustry.Rows[0]["VCHGSTIN"].ToString();

                if (dtindustry.Rows[0]["projectType"].ToString() == "1")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustry.Rows[0]["vchappnobef"].ToString();
                }
                else if (dtindustry.Rows[0]["projectType"].ToString() == "2")
                {
                    Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustry.Rows[0]["vchappnobef"].ToString();
                }
                if (dtindustry.Rows[0]["dtmProdCommBefore"].ToString() != "")
                {
                    divbefor.Visible = true;
                }
                else
                {
                    divbefor.Visible = false;
                    lblAfterEMD11.Text = "Date of Production Commencement";
                    lblAfterEMD189.Text = "PC Issuance Date";
                    lbl_PC_No_After.Text = "PC No";
                    Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                }

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
                lbl_Sector.Text = dtindustry.Rows[0]["sectorName"].ToString();
                lbl_Sub_Sector.Text = dtindustry.Rows[0]["SubsectorName"].ToString();
                Lbl_Derived_Sector.Text = dtindustry.Rows[0]["vchDerivedSector"].ToString();

                if (dtindustry.Rows[0]["bitPriorityIPR"].ToString() == "1")
                {

                    lbl_Sectoral.Text = "Yes";
                }
                else
                {
                    lbl_Sectoral.Text = "No";
                }
                TxtApplicantName.Text = dtindustry.Rows[0]["VCHAPPLICANTNAME"].ToString();

                DdlGender.SelectedValue = dtindustry.Rows[0]["INTGENDER"].ToString();
                if (dtindustry.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                {
                    TxtAdhaar1.Text = dtindustry.Rows[0]["VCHAADHAARNO"].ToString();
                }
                if (dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString() != "")
                {
                    hdnAUTHORIZEDFILE.Value = dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(); /////AUTHORIZEDFILE file upload
                    hypAUTHORIZEDFILE.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    hypAUTHORIZEDFILE.Visible = true;
                    lnkAUTHORIZEDFILEDdelete.Visible = true;
                    FlupAUTHORIZEDFILE.Enabled = false;
                }

                if (dtindustry.Rows[0]["INTAPPLYBY"].ToString() == "1")
                {
                    radApplyBy.SelectedValue = dtindustry.Rows[0]["INTAPPLYBY"].ToString();
                }
                else if (dtindustry.Rows[0]["INTAPPLYBY"].ToString() == "2")
                {
                    radApplyBy.SelectedValue = dtindustry.Rows[0]["INTAPPLYBY"].ToString();
                }
                else
                {
                    radApplyBy.SelectedIndex = -1;
                }
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
    public string RetFileNamePath(string filename)
    {
        string strret = "#";
        try
        {
            if (filename != "")
            {
                strret = "../incentives/Files/IndustryUnit/" + filename;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return strret;
    }
    private void UploadDocument(FileUpload fuDocument, HiddenField hdnDocument, string strFileName, HyperLink hypDocument, Label lblDocument, LinkButton lnkDocumentDelete, string strFolderName, string Extention)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFolderName));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (fuDocument.HasFile)
            {
                if (!(IsFileValid(fuDocument)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots. ', '" + strMsg + "'); </script>", false);
                    return;
                }
                string filename = string.Empty;

                if (Extention == "pdf/zip")
                {
                    if ((Path.GetExtension(fuDocument.FileName).ToLower() != ".pdf") && (Path.GetExtension(fuDocument.FileName).ToLower() != ".zip"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .pdf/zip file Only!','" + strMsg + "')", true);
                        return;
                    }
                    int fileSize = fuDocument.PostedFile.ContentLength;
                    if (fileSize > (4 * 1024 * 1024))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + strMsg + "')", true);
                        return;
                    }
                    else
                    {
                        filename = strFileName + Path.GetExtension(fuDocument.FileName);
                    }
                }
                else if (Extention == "pdf/jpg/jpeg")
                {
                    if ((Path.GetExtension(fuDocument.FileName).ToLower() != ".pdf") && (Path.GetExtension(fuDocument.FileName).ToLower() != ".jpg") && (Path.GetExtension(fuDocument.FileName).ToLower() != ".jpeg"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .pdf/.jpg/.jpeg file Only!','" + strMsg + "')", true);
                        return;
                    }
                    int fileSize = fuDocument.PostedFile.ContentLength;
                    if (fileSize > (4 * 1024 * 1024))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + strMsg + "')", true);
                        return;
                    }
                    else
                    {
                        filename = strFileName + Path.GetExtension(fuDocument.FileName);
                    }
                }
                else if (Extention == "pdf")
                {
                    if ((Path.GetExtension(fuDocument.FileName).ToLower() != ".pdf"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .pdf file Only!','" + strMsg + "')", true);
                        return;
                    }
                    int fileSize = fuDocument.PostedFile.ContentLength;
                    if (fileSize > (4 * 1024 * 1024))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + strMsg + "')", true);
                        return;
                    }
                    else
                    {
                        filename = strFileName + Path.GetExtension(fuDocument.FileName);
                    }
                }
                fuDocument.SaveAs(strMainFolderPath + filename);
                hdnDocument.Value = filename;
                hypDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
                hypDocument.Visible = true;
                lnkDocumentDelete.Visible = true;
                lblDocument.Visible = true;
                fuDocument.Enabled = false;
            }
        }
        catch (Exception)
        {
        }
    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string strFolderName)
    {
        try
        {
            string filename = hdnFile.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
            string completePath = Server.MapPath(path);

            //File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;

        }
        catch (Exception)
        {
        }
    }
    private bool IsFileValid(FileUpload FileUpload1)
    {
        try
        {
            string[] allowedImageTyps = { "application/pdf", "application/x-zip-compressed", "image/jpeg", "image/jpg", "application/msxls" };
            string[] allowedExtension = { ".pdf", ".zip", ".jpg", "jpeg", ".xls", ".xlsx" };
            StringCollection imageTypes = new StringCollection();
            StringCollection imageExtension = new StringCollection();
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            string strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);// 
            int count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonFunctions cmmf = new CommonFunctions();

            if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
    private void fillSalutation()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "R";
            objBAL.BindDropdown(DdlGender, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    #endregion
    #region "Event Controls"
    protected void LinkButton41_Click(object sender, EventArgs e)
    {
        DataTable dtincentive = new DataTable();
        dtincentive = (DataTable)ViewState["dtincentive"];
        DataRow dr = dtincentive.NewRow();
        dr["vchagency"] = txtagency.Text.Trim();
        dr["vchsacamt"] = txtsacamt.Text.Trim();
        dr["vchsacord"] = txtsacord.Text.Trim();
        dr["vchsacdat"] = txtsacdat.Text.Trim();
        dr["vchavilamt"] = txtavilamt.Text.Trim();
        dtincentive.Rows.Add(dr);
        ViewState["dtincentive"] = dtincentive;

        grdAssistanceDetailsAD.DataSource = dtincentive;
        grdAssistanceDetailsAD.DataBind();

        txtagency.Text = "";
        txtsacamt.Text = "";
        txtsacord.Text = "";
        txtsacdat.Text = "";
        txtavilamt.Text = "";
    }
    protected void grdAssistanceDetailsAD_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hdfanew = (HiddenField)grdAssistanceDetailsAD.Rows[e.RowIndex].Cells[5].FindControl("hdnRowId");
        DataTable dtnew0 = new DataTable();
        dtnew0 = (DataTable)ViewState["dtincentive"];
        DataRow[] dr1 = null;
        dr1 = dtnew0.Select("dcRowId='" + hdfanew.Value + "'");
        for (int i = 0; i < dr1.Length; i++)
        {

            dr1[i].Delete();
        }
        dtnew0.AcceptChanges();
        grdAssistanceDetailsAD.DataSource = dtnew0;
        grdAssistanceDetailsAD.DataBind();
        ViewState["dtincentive"] = dtnew0;
    }
    protected void ddlInstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtOtherInstitution.Visible = false;
            if (ddlInstitute.SelectedIndex > 0)
            {

                int intlnstType = Convert.ToInt32(ddlInstitute.SelectedValue);
                if (ddlInstitute.SelectedItem.Text == "Others")
                {
                    txtOtherInstitution.Visible = true;
                    ddlInsttuteLoc.Enabled = false;
                    ddlInsttuteLoc.Items.Clear();
                }
                else
                {
                    ddlInsttuteLoc.Enabled = true;
                    txtOtherInstitution.Visible = false;
                }
                if (ddlInstitute.SelectedItem.Text != "Others")
                {

                    DataTable selectedTable = dtbInsttuteLo.AsEnumerable()
                                .Where(r => r.Field<int>("intInstitutionID") == intlnstType)
                                .CopyToDataTable();

                    ddlInsttuteLoc.DataSource = selectedTable;
                    ddlInsttuteLoc.DataValueField = "intLocationID";
                    ddlInsttuteLoc.DataTextField = "InstitutionLocation";
                    ddlInsttuteLoc.DataBind();

                    ddlInstitute.SelectedItem.Value = intlnstType.ToString();
                }
            }
        }
        catch (Exception)
        {
        }
    }
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        string msgdt = InsertUpdateData();
        if (msgdt.Split('~')[0].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !</strong>', '" + strProjName + "'); </script>", false);
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        string retval = InsertUpdateData();
        int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
        /////------------------------------------------------------------------------------------------------
        Response.Redirect("Enterpreneurship_Subsidy_FormPreview.aspx?InctUniqueNo=" + Convert.ToString(mstyp));
    }
    protected void btnProvisional_Click(object sender, EventArgs e)
    {
        string retval = InsertUpdateData();
        int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
        if (mstyp > 0)
        {
            Incentive objEntity = new Incentive();
            objEntity.ApprovalAction = "P";
            objEntity.UnqIncentiveId = mstyp;
            int i = IncentiveManager.UpdateSignature(objEntity);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Provisional Sanction applied Successfully.','" + strMsg + "')", true);
            btnProvisional.Visible = false;
        }
    }
    protected void lnkDocumentDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, lnkSanctionLetterDelete.ID))
        {
            string strFolderName = "CourseDetails";  //done
            UpdFileRemove(hdnSanctionLetter, lnkSanctionLetterDoc, lnkSanctionLetterDelete, hypSanctionLetter, lblSanctionLetterDocument, fuSanctionLetter, strFolderName);
        }
        else if (string.Equals(lnk.ID, lnkCoursecomplitationDocDelete.ID))  //done
        {
            string strFolderName = "CourseDetails";
            UpdFileRemove(hdnCoursecomplitation, lnkCoursecomplitationDoc, lnkCoursecomplitationDocDelete, hypCoursecomplitation, lblCoursecomplitationDoc, fuCoursecomplitation, strFolderName);
        }
        else if (string.Equals(lnk.ID, LnkBtn_Delete_Asst_Sanc_Doc.ID))
        {
            string strFolderName = "AvailDetails";
            UpdFileRemove(Hid_Asst_Sanc_File_Name, LnkBtn_Upload_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, strFolderName);
        }
        else if (string.Equals(lnk.ID, LnkBtn_Delete_Undertaking_Doc.ID))
        {
            string strFolderName = "AvailDetails";
            UpdFileRemove(Hid_Undertaking_File_Name, LnkBtn_Upload_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, FU_Undertaking_Doc, strFolderName);
        }
        else if (string.Equals(lnk.ID, lnkAttachmentDocDelete.ID))  //done
        {
            string strFolderName = "CourseDetails";
            UpdFileRemove(hdnAttachmentDoc, lnkAttachmentDoc, lnkAttachmentDocDelete, hypAttachmentDoc, lblAttachmentDoc, fuAttachment, strFolderName);
        }
        else if (string.Equals(lnk.ID, lnkCopyletterselectionDelete.ID))   //done
        {
            string strFolderName = "CourseDetails";
            UpdFileRemove(hdnCopyletterselection, lnkCopyletterselectionDoc, lnkCopyletterselectionDelete, hypCopyletterselection, lblCopyletterselection, fuCopyletterselection, strFolderName);
        }
        else if (string.Equals(lnk.ID, lnkBankDelete.ID))
        {
            string strFolderName = "Bank";
            UpdFileRemove(hdnBank, lnkBankUpload, lnkBankDelete, hypBank, lblBank, fuBank, strFolderName);
        }
        else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILEDdelete.ID))
        {
            string strFolderName = "InctBasicDoc";
            UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, strFolderName);
        }
    }
    protected void lnkDocumentUpload_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, lnkSanctionLetterDoc.ID))
        {
            if (fuSanctionLetter.HasFile) //done
            {
                string strFileName = "ProvisionalSanction" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "CourseDetails";
                UploadDocument(fuSanctionLetter, hdnSanctionLetter, strFileName, hypSanctionLetter, lblSanctionLetterDocument, lnkSanctionLetterDelete, strFolderName, "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkCoursecomplitationDoc.ID))
        {
            if (fuCoursecomplitation.HasFile)  //done
            {
                string strFileName = "CertificateManagementProg" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "CourseDetails";
                UploadDocument(fuCoursecomplitation, hdnCoursecomplitation, strFileName, hypCoursecomplitation, lblCoursecomplitationDoc, lnkCoursecomplitationDocDelete, strFolderName, "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, LnkBtn_Upload_Asst_Sanc_Doc.ID))
        {
            if (FU_Asst_Sanc_Doc.HasFile)
            {
                string strFileName = "ASSTSANC" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "AvailDetails";
                UploadDocument(FU_Asst_Sanc_Doc, Hid_Asst_Sanc_File_Name, strFileName, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, strFolderName, "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, LnkBtn_Upload_Undertaking_Doc.ID))
        {
            if (FU_Undertaking_Doc.HasFile)
            {
                string strFileName = "UND" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "AvailDetails";
                UploadDocument(FU_Undertaking_Doc, Hid_Undertaking_File_Name, strFileName, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, strFolderName, "pdf/zip");
            }
        }

        else if (string.Equals(lnk.ID, lnkAttachmentDoc.ID))  //done
        {
            if (fuAttachment.HasFile)
            {
                string strFileName = "CourseAttachment" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "CourseDetails";
                UploadDocument(fuAttachment, hdnAttachmentDoc, strFileName, hypAttachmentDoc, lblAttachmentDoc, lnkAttachmentDocDelete, strFolderName, "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkCopyletterselectionDoc.ID))  //done
        {
            if (fuCopyletterselection.HasFile)
            {
                string strFileName = "SelectionLetter" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "CourseDetails";
                UploadDocument(fuCopyletterselection, hdnCopyletterselection, strFileName, hypCopyletterselection, lblCopyletterselection, lnkCopyletterselectionDelete, strFolderName, "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkBankUpload.ID))
        {
            if (fuBank.HasFile)
            {
                string strFileName = "Bank" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Bank";
                UploadDocument(fuBank, hdnBank, strFileName, hypBank, lblBank, lnkBankDelete, strFolderName, "pdf/jpg/jpeg");
            }
        }
        else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILE.ID))
        {
            if (FlupAUTHORIZEDFILE.HasFile)
            {
                string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                string strFolderName = "InctBasicDoc";
                UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, strFolderName, "pdf");
            }
        }

    }
    #endregion
}