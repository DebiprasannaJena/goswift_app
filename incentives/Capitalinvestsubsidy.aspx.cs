using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAcessLayer.Common;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;

public partial class incentives_Capitalinvestsubsidy : SessionCheck
{
    decimal dPageTotal = 0;
    string strMsg = "Incentive";
    protected void Page_Load(object sender, EventArgs e)
    {
        txtOperationalization.Attributes.Add("readonly", "readonly");
        if (!IsPostBack)
        {
            fillSalutation();
            txtOtherEquipmentType.Visible = false;
            BindData();
            GetMasterdetails();
            btnApply.Enabled = true;
            btnDraft.Enabled = true;
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
    public void PrepopulateDataPlus(int id)
    {
        DataSet ds = IncentiveManager.PostpopulateDataPLUS(id);
        DataTable dtBank = ds.Tables[0];
        if (dtBank.Rows.Count > 0)
        {
            PreBankPlus(dtBank);
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
    protected void btnApply_Click(object sender, EventArgs e)
    {

        string retval = InsertUpdateData();
        int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
        ///------------------------------------------------------------------------------------------------
        Response.Redirect("FormPreview_Capitalinvestsubsidy.aspx?InctUniqueNo=" + Convert.ToString(mstyp));

    }
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        string retval = InsertUpdateData();
        if (retval.Split('~')[0].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !!</strong>', 'Incentive'); </script>", false);

        }
    }
    #region Methods

    protected void lnkDocumentUpload_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, lnkOperationalizationDoc.ID))
        {
            if (fuOperationalization.HasFile)
            {
                string strFileName = "Operationalization" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Operationalization";
                UploadDocument(fuOperationalization, hdnOperationalization, strFileName, hypOperationalization, lblOperationalization, lnkOperationalizationDocDelete, strFolderName, "pdf/zip");
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
        else if (string.Equals(lnk.ID, lnkBankUpload.ID))
        {
            if (fuBank.HasFile)
            {
                string strFileName = "Bank" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Bank";
                UploadDocument(fuBank, hdnBank, strFileName, hypBank, lblBank, lnkBankDelete, strFolderName, "pdf/jpg/jpeg");
            }
        }
    }
    private void UploadDocument(FileUpload fuDocument, HiddenField hdnDocument, string strFileName, HyperLink hypDocument, Label lblDocument, LinkButton lnkDocumentDelete, string strFolderName, string Extention)
    {
        string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFolderName));
        if (!Directory.Exists(strMainFolderPath))
        {
            Directory.CreateDirectory(strMainFolderPath);
        }
        if (fuDocument.HasFile)
        {
            string filename = string.Empty;

            if (IsFileValid(fuDocument) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }


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
    protected void lnkDocumentDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, lnkOperationalizationDocDelete.ID))
        {
            string strFolderName = "Operationalization";
            UpdFileRemove(hdnOperationalization, lnkOperationalizationDoc, lnkOperationalizationDocDelete, hypOperationalization, lblOperationalization, fuOperationalization, strFolderName);
        }
        else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILEDdelete.ID))
        {
            string strFolderName = "InctBasicDoc";
            UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, strFolderName);
        }
        else if (string.Equals(lnk.ID, lnkBankDelete.ID))
        {
            string strFolderName = "Bank";
            UpdFileRemove(hdnBank, lnkBankUpload, lnkBankDelete, hypBank, lblBank, fuBank, strFolderName);
        }
    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string strFolderName)
    {
        string filename = hdnFile.Value;
        string path = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
        string completePath = Server.MapPath(path);
        if (File.Exists(completePath))
        {
            File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
        else
        {

            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
    }
    public void BindData()
    {
        DataTable table = new DataTable();
        try
        {
            CommonDataLayer objDataUnit = new CommonDataLayer();
            ddlEquipmentType.DataTextField = "vchName";
            ddlEquipmentType.DataValueField = "slno";
            ddlEquipmentType.DataSource = objDataUnit.FillUnitType("M");
            ddlEquipmentType.DataBind();
            ddlEquipmentType.Items.Insert(0, new ListItem("-Select-", "0"));

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
        if (hdnOperationalization.Value != "")
        {
            DataRow dorgRowT = dtFiles.NewRow();
            dorgRowT["vchDocId"] = hdnOperationalizationDocId.Value;
            dorgRowT["vchFileName"] = hdnOperationalization.Value;
            dorgRowT["vchFilePath"] = "Files/Operationalization/";
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
    protected string InsertUpdateData()
    {
        Incentive objIncentive = new Incentive();
        InvestmentPollution objPollution = new InvestmentPollution();
        List<InvestmentPollutionDetails> listInvestDtls = new List<InvestmentPollutionDetails>();
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
        //if (!String.IsNullOrEmpty(Session["FyYear"].ToString()) || Session["FyYear"].ToString() != null)
        //    objIncentive.FYear = Convert.ToInt16(Session["FyYear"]);
        objIncentive.incentivetype = 4;
        objIncentive.FormType = FormNumber.CapitalInvestmentZld_18;

        try
        {

            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            if (DdlGender.SelectedIndex > 0)
            {
                objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            }
            if (radApplyBy.SelectedIndex >= 0)
            {
                objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }
            objINDUSTRIAL_UNIT_MASTER.AADHAARNO_IND = TxtAdhaar1.Text;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objIncentive.IndsutUnitMstDet = objINDUSTRIAL_UNIT_MASTER;

            /*10-Bank Details*/
            BankDetails obj = new BankDetails();
            obj.BankName = txtBnkNm.Text;
            obj.BranchName = txtBranch.Text;
            obj.IFSCNo = txtIFSC.Text;
            obj.AccountNo = txtAccNo.Text;
            obj.MICRNo = txtMICRNo.Text;
            if (hdnBank.Value != "")
            {
                obj.BankDoc = hdnBank.Value;
            }
            objIncentive.BankDet = obj;

            /*26-Investment in PollutionControl Equipment*/

            objPollution.operationalizationDate = txtOperationalization.Text;

            if (fuOperationalization.HasFile)
            {
                objPollution.operationalizationDOC = hdnOperationalization.Value;
            }
            else
            {
                if (hdnOperationalization.Value != "")
                    objPollution.operationalizationDOC = hdnOperationalization.Value;
            }

            for (int i = 0; i < gvEquipment.Rows.Count; i++)
            {
                InvestmentPollutionDetails objPollutioninvest = new InvestmentPollutionDetails();
                Label lblEquipmentName = (Label)gvEquipment.Rows[i].FindControl("lblEquipmentName");
                Label lblInvestmentAmt = (Label)gvEquipment.Rows[i].FindControl("lblInvestmentAmt");
                Label lblEuipmentType = (Label)gvEquipment.Rows[i].FindControl("lblEuipmentType");
                Label lblOtherEquiType = (Label)gvEquipment.Rows[i].FindControl("lblOthetEuipmentType");
                HiddenField hdnEquipmentType = (HiddenField)gvEquipment.Rows[i].FindControl("hdnEquipmentType");
                if (hdnEquipmentType.Value != "")
                {
                    objPollutioninvest.EquipmentTYPE = Convert.ToInt32(hdnEquipmentType.Value);
                }
                objPollutioninvest.strEquipmentName = lblEquipmentName.Text;
                objPollutioninvest.dcmInvestedAmt = Convert.ToDecimal(lblInvestmentAmt.Text);
                objPollutioninvest.OtherEquiType = lblOtherEquiType.Text;
                listInvestDtls.Add(objPollutioninvest);
            }
            objPollution.lstInvestPollution = listInvestDtls;
            objIncentive.InvestPolutionDet = objPollution;
            objIncentive.FileUploadDetails = getFileUploadDatatable();
            string retVal = IncentiveManager.CreateIncentiveCapitalInvst(objIncentive);

            return retVal;
        }

        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
            listInvestDtls = null;
        }
        return null;
    }
    public string RetDateFrmDB(string srcDate)
    {
        string retdt = "";
        try
        {
            if (srcDate != "")
            {
                DateTime dbdt = Convert.ToDateTime(srcDate);
                retdt = dbdt.ToString("dd/MM/yyyy");

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return retdt;
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
    public void PostpopulateData(int id)
    {
        Grd_TL.DataSource = null;
        Grd_TL.DataBind();
        Grd_WC.DataSource = null;
        Grd_WC.DataBind();
        Incentive objIncentive = new Incentive();
        GetAndViewParam objPar = new GetAndViewParam();

        objPar.InctType = 4;
        objIncentive.UnqIncentiveId = id;
        objIncentive.GetVwPrmtrs = objPar;
        objIncentive.FormType = FormNumber.CapitalInvestmentZld_18;

        DataSet ds = IncentiveManager.PostpopulateCapSubsidy(objIncentive);

        DataTable dtindustryPre = ds.Tables[0];////////////industry panel
        DataTable dtProductionPre = ds.Tables[1];///////////production & employment
        DataTable dtProductionDetBefPre = ds.Tables[2];///////////production & employment Before
        DataTable dtProductionDetAftPre = ds.Tables[3];///////////production & employment After
        DataTable dtInvestmentPre = ds.Tables[4];///////////investment details
        DataTable dtMeansFinancePre = ds.Tables[5];///////////Means of Finance
        DataTable dtMoFTermLoanPre = ds.Tables[6];///////////Means of Finance Term Loan
        DataTable dtMoFWorkingLoanPre = ds.Tables[7];///////////Means of Finance Working Loan

        DataTable dtBankDetail = ds.Tables[10];
        DataTable dtInvestmentPollution = ds.Tables[8];
        DataTable dtInvestmentPollutionDtl = ds.Tables[9];

        if (dtindustryPre.Rows.Count > 0)
        {
            lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
            DataSet ds1 = new DataSet();
            ds1 = IncentiveManager.dynamic_name_doc_bind();
            ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intUnitType"].ToString() + "'";
            ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intOrganisationType"].ToString() + "'";
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

            lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
            lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
            lbl_Unit_Cat.Text = dtindustryPre.Rows[0]["Unitcategoryname"].ToString();
            if (dtindustryPre.Rows[0]["Unitcategoryname"].ToString().ToLower() == "large")
            {
                btnApply.Enabled = false;
                btnDraft.Enabled = false;
            }
            Lbl_Pioneer_Doc_Name.Text = dtindustryPre.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();
            hdnRadibutton.Value = dtindustryPre.Rows[0]["INTAPPLYBY"].ToString();

            dt = (ds1.Tables[1].DefaultView).ToTable();
            if (dt.Rows.Count > 0)
            {
                string strDocType = dt.Rows[0]["vchDocumentType"].ToString();
                if (strDocType != "")
                {
                    Div_Unit_Type_Doc.Visible = true;
                    Lbl_Unit_Type_Doc_Name.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                    Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();
                    Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();
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
            lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();
            if (dtindustryPre.Rows[0]["intPriority"].ToString() == "1")
            {
                lblIs_Priority.Text = "Yes";
                Pioneersec.Visible = true;
            }
            else
            {
                lblIs_Priority.Text = "No";
                Pioneersec.Visible = false;
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

            lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();

            Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
            lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
            lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
            lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
            lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
            lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();

            lbl_pcno_befor.Text = dtindustryPre.Rows[0]["vchpcnobefore"].ToString();
            lblGstin.Text = dtindustryPre.Rows[0]["VCHGSTIN"].ToString();

            if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
            {
                Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
            }
            else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            {
                Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
            }
            if (dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString() != "")
            {
                divbefor.Visible = true;
                divbefor1.Visible = true;
                divbefor2.Visible = true;
            }
            else
            {
                divbefor.Visible = false;
                divbefor1.Visible = false;
                divbefor2.Visible = false;
                lblAfterEMD11.Text = "Date of Production Commencement";
                lblAfterEMD189.Text = "PC Issuance Date";
                lbl_PC_No_After.Text = "PC No";
                lblemd.Text = "";
                Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                lblEMDInvestment.Text = "";
            }

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
            TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHAPPLICANTNAME"].ToString();

            DdlGender.SelectedValue = dtindustryPre.Rows[0]["INTGENDER"].ToString();
            if (dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
            {
                TxtAdhaar1.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString();
            }
            if (dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString() != "")
            {
                hdnAUTHORIZEDFILE.Value = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(); /////AUTHORIZEDFILE file upload
                hypAUTHORIZEDFILE.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                hypAUTHORIZEDFILE.Visible = true;
                lnkAUTHORIZEDFILEDdelete.Visible = true;
                FlupAUTHORIZEDFILE.Enabled = false;
            }

            if (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() != "0")
            {
                radApplyBy.SelectedValue = dtindustryPre.Rows[0]["INTAPPLYBY"].ToString();
            }
            else
            {
                radApplyBy.SelectedIndex = -1;
            }
        }

        if (dtProductionPre.Rows.Count > 0)
        {
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
        }

        if (dtInvestmentPre.Rows.Count > 0)
        {
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
        }

        if (dtMeansFinancePre.Rows.Count > 0)
        {
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
        }


        #region Investment Pollution
        if (dtInvestmentPollution.Rows.Count > 0)
        {
            if (dtInvestmentPollution.Rows[0]["DTMOPERATIONALIZATION"].ToString() != "01-Jan-1900")
            {
                txtOperationalization.Text = dtInvestmentPollution.Rows[0]["DTMOPERATIONALIZATION"].ToString(); /////----------- datetime
            }
            else
            {
                txtOperationalization.Text = "";
            }
            if (dtInvestmentPollution.Rows[0]["VCHOPERATIONALIZATIONDOC"].ToString() != "")
            {
                hdnOperationalization.Value = dtInvestmentPollution.Rows[0]["VCHOPERATIONALIZATIONDOC"].ToString();
                fuOperationalization.Enabled = false;
                hypOperationalization.NavigateUrl = "~/incentives/Files/Operationalization/" + dtInvestmentPollution.Rows[0]["VCHOPERATIONALIZATIONDOC"].ToString();
                lnkOperationalizationDocDelete.Visible = true;
                hypOperationalization.Visible = true;
            }

            if (dtInvestmentPollutionDtl.Rows.Count > 0)
            {
                gvEquipment.DataSource = dtInvestmentPollutionDtl;
                gvEquipment.DataBind();
            }
        }
        #endregion

        if (dtBankDetail.Rows.Count > 0)
        {
            txtAccNo.Text = dtBankDetail.Rows[0]["VCHACCOUNTNO"].ToString();
            txtBnkNm.Text = dtBankDetail.Rows[0]["VCHBANKNAME"].ToString();
            txtBranch.Text = dtBankDetail.Rows[0]["VCHBRANCHNAME"].ToString();
            txtIFSC.Text = dtBankDetail.Rows[0]["VCHIFSCNO"].ToString();
            txtMICRNo.Text = dtBankDetail.Rows[0]["VCHMICR"].ToString();
            if (dtBankDetail.Rows[0]["vchBankDoc"].ToString() != "")
            {
                hdnBank.Value = dtBankDetail.Rows[0]["vchBankDoc"].ToString();
                hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBankDetail.Rows[0]["vchBankDoc"].ToString();
                hypBank.Visible = true;
                lnkBankDelete.Visible = true;
                fuBank.Enabled = false;
            }
        }
    }
    public void PrepopulateData(int id)
    {
        Grd_TL.DataSource = null;
        Grd_TL.DataBind();
        Grd_WC.DataSource = null;
        Grd_WC.DataBind();
        DataSet ds = IncentiveManager.PrepopulateData(id);
        DataTable dtindustryPre = ds.Tables[0];//industry panel
        DataTable dtProductionPre = ds.Tables[1];//production & employment

        DataTable dtProductionDetBefPre = ds.Tables[2];//production & employment Before
        DataTable dtProductionDetAftPre = ds.Tables[3];//production & employment After
        DataTable dtInvestmentPre = ds.Tables[4];//investment details
        DataTable dtMeansFinancePre = ds.Tables[5];//Means of Finance
        DataTable dtMoFTermLoanPre = ds.Tables[6];//Means of Finance Term Loan
        DataTable dtMoFWorkingLoanPre = ds.Tables[7];//Means of Finance Working Loan

        if (dtindustryPre.Rows.Count > 0)
        {
            lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
            DataSet ds1 = new DataSet();
            ds1 = IncentiveManager.dynamic_name_doc_bind();
            ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intUnitType"].ToString() + "'";
            ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPre.Rows[0]["intOrganisationType"].ToString() + "'";
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

            lbl_Org_Type.Text = dtindustryPre.Rows[0]["OrgTypename"].ToString();
            lbl_Industry_Address.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
            lbl_Unit_Cat.Text = dtindustryPre.Rows[0]["Unitcategoryname"].ToString();

            if (dtindustryPre.Rows[0]["Unitcategoryname"].ToString().ToLower() == "large")
            {
                btnApply.Enabled = false;
                btnDraft.Enabled = false;
            }

            Lbl_Pioneer_Doc_Name.Text = dtindustryPre.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();

            dt = (ds1.Tables[1].DefaultView).ToTable();
            if (dt.Rows.Count > 0)
            {
                string strDocType = dt.Rows[0]["vchDocumentType"].ToString();
                if (strDocType != "")
                {
                    Div_Unit_Type_Doc.Visible = true;
                    Lbl_Unit_Type_Doc_Name.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                    Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();

                    Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();
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
            lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();
            if (dtindustryPre.Rows[0]["intPriority"].ToString() == "1")
            {
                lblIs_Priority.Text = "Yes";
                Pioneersec.Visible = true;
            }
            else
            {
                lblIs_Priority.Text = "No";
                Pioneersec.Visible = false;
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

            lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();

            DdlGender.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
            TxtApplicantName.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();

            lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
            lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
            lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
            lbl_pcno_befor.Text = dtindustryPre.Rows[0]["vchpcnobefore"].ToString();
            lblGstin.Text = dtindustryPre.Rows[0]["VCHGSTIN"].ToString();
            lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
            lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();
            if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
            {
                Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
            }
            else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            {
                Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
            }

            if (dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString() != "")
            {
                divbefor.Visible = true;
                divbefor1.Visible = true;
                divbefor2.Visible = true;
            }
            else
            {
                divbefor.Visible = false;
                divbefor1.Visible = false;
                divbefor2.Visible = false;
                lblAfterEMD11.Text = "Date of Production Commencement";
                lblAfterEMD189.Text = "PC Issuance Date";
                lbl_PC_No_After.Text = "PC No";
                lblemd.Text = "";
                Lbl_Prod_Comm_After_Doc_Name.Text = "Certificate on Date of Commencement of production";
                lblEMDInvestment.Text = "";
            }

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
        if (dtProductionPre.Rows.Count > 0)
        {
            Grd_Production_Before.DataSource = dtProductionDetBefPre;
            Grd_Production_Before.DataBind();
            Grd_Production_After.DataSource = dtProductionDetAftPre;
            Grd_Production_After.DataBind();
            lbl_Direct_Emp_Before.Text = dtProductionPre.Rows[0]["intDirectEmpBefore"].ToString();
            lbl_Contract_Emp_Before.Text = dtProductionPre.Rows[0]["intContractualEmpBefore"].ToString();
            Hyp_View_Direct_Emp_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocBefore"].ToString();
            lbl_Managarial_Before.Text = dtProductionPre.Rows[0]["intManagerialBefore"].ToString();
            lbl_Supervisor_Before.Text = dtProductionPre.Rows[0]["intSupervisorBefore"].ToString();
            lbl_Skilled_Before.Text = dtProductionPre.Rows[0]["intSkilledBefore"].ToString();
            lbl_Semi_Skilled_Before.Text = dtProductionPre.Rows[0]["intSemiSkilledBefore"].ToString();
            lbl_Unskilled_Before.Text = dtProductionPre.Rows[0]["intUnskilledBefore"].ToString();
            lbl_Total_Emp_Before.Text = dtProductionPre.Rows[0]["intTotalEmpBefore"].ToString();
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

            lbl_Managarial_After.Text = dtProductionPre.Rows[0]["intManagerialAfter"].ToString();
            lbl_Supervisor_After.Text = dtProductionPre.Rows[0]["intSupervisorAfter"].ToString();
            lbl_Skilled_After.Text = dtProductionPre.Rows[0]["intSkilledAfter"].ToString();
            lbl_Semi_Skilled_After.Text = dtProductionPre.Rows[0]["intSemiSkilledAfter"].ToString();
            lbl_Unskilled_After.Text = dtProductionPre.Rows[0]["intUnskilledAfter"].ToString();
            lbl_Total_Emp_After.Text = dtProductionPre.Rows[0]["intTotalEmpAfter"].ToString();
            lbl_General_After.Text = dtProductionPre.Rows[0]["intGeneralAfter"].ToString();
            lbl_SC_After.Text = dtProductionPre.Rows[0]["intSCAfter"].ToString();
            lbl_ST_After.Text = dtProductionPre.Rows[0]["intSTAfter"].ToString();
            lbl_Total_Cast_Emp_After.Text = dtProductionPre.Rows[0]["intTotalEmpCastAfter"].ToString();
            lbl_Women_After.Text = dtProductionPre.Rows[0]["intWomenAfter"].ToString();
            lbl_PHD_After.Text = dtProductionPre.Rows[0]["intDisabledAfter"].ToString();
        }

        if (dtInvestmentPre.Rows.Count > 0)
        {
            Txt_FFCI_Date_Before.Text = dtInvestmentPre.Rows[0]["dtmFFCIDateBefore"].ToString();
            Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();
            Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocBefore"].ToString();
            lbl_Land_Before.Text = dtInvestmentPre.Rows[0]["decLandAmtBefore"].ToString();
            lbl_Building_Before.Text = dtInvestmentPre.Rows[0]["decBuildingAmtBefore"].ToString();
            lbl_Plant_Mach_Before.Text = dtInvestmentPre.Rows[0]["decPlantMachAmtBefore"].ToString();
            lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPre.Rows[0]["decOtheFixedAssetAmtBefore"].ToString();
            lbl_Total_Capital_Before.Text = dtInvestmentPre.Rows[0]["decTotalAmtBefore"].ToString();
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
        }

        if (dtMeansFinancePre.Rows.Count > 0)
        {
            lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
            lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
            Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString();
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
        }

    }
    public DataTable GetTableWithInitialData() // this might be your sp for select
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("VCHEQUIPMENTNAME", typeof(string));
            table.Columns.Add("DCMINVESTEDAMT", typeof(string));
            table.Columns.Add("VCHEQIPMENTTYPE", typeof(string));
            table.Columns.Add("INTEQUIPMENTTYPE", typeof(string));
            table.Columns.Add("VCHOTHEREQIPTYPE", typeof(string));
            for (int i = 0; i < gvEquipment.Rows.Count; i++)
            {
                Label lblEquipmentName = (Label)gvEquipment.Rows[i].FindControl("lblEquipmentName");
                Label lblInvestmentAmt = (Label)gvEquipment.Rows[i].FindControl("lblInvestmentAmt");
                Label lblEuipmentType = (Label)gvEquipment.Rows[i].FindControl("lblEuipmentType");
                HiddenField hdnEquipmentType = (HiddenField)gvEquipment.Rows[i].FindControl("hdnEquipmentType");
                Label lblOtherEquiType = (Label)gvEquipment.Rows[i].FindControl("lblOthetEuipmentType");
                table.Rows.Add(lblEquipmentName.Text, lblInvestmentAmt.Text, lblEuipmentType.Text, hdnEquipmentType.Value, lblOtherEquiType.Text);
            }
            table.Rows.Add(txtEquipmentName.Text, txtInvestedAmount.Text, ddlEquipmentType.SelectedItem.Text, ddlEquipmentType.SelectedValue, txtOtherEquipmentType.Text);

            return table;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            //table = null;
        }
        return null;
    }
    private int IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf", "application/x-zip-compressed" };
        string[] allowedExtension = { ".pdf", ".zip" };
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
            return 0;
        }
        else
        {
            return 1;
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
    #region Events
    protected void lnkEquipmentAdd_Click(object sender, EventArgs e)
    {
        gvEquipment.DataSource = GetTableWithInitialData();
        gvEquipment.DataBind();
        txtEquipmentName.Text = string.Empty;
        txtInvestedAmount.Text = string.Empty;
        ddlEquipmentType.SelectedValue = "0";
        txtOtherEquipmentType.Text = string.Empty;
        txtOtherEquipmentType.Visible = false;
    }
    protected void ImageButtonDelete_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            LinkButton imgbtn = (LinkButton)sender;
            int RowID = Convert.ToInt32(imgbtn.CommandArgument);

            table.Columns.Add("VCHEQUIPMENTNAME", typeof(string));
            table.Columns.Add("DCMINVESTEDAMT", typeof(string));
            table.Columns.Add("VCHEQIPMENTTYPE", typeof(string));
            table.Columns.Add("INTEQUIPMENTTYPE", typeof(string));
            table.Columns.Add("VCHOTHEREQIPTYPE", typeof(string));
            for (int i = 0; i < gvEquipment.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label lblProductionName = (Label)gvEquipment.Rows[i].FindControl("lblEquipmentName");
                    Label lblQuantity = (Label)gvEquipment.Rows[i].FindControl("lblInvestmentAmt");
                    Label lblEuipmentType = (Label)gvEquipment.Rows[i].FindControl("lblEuipmentType");
                    HiddenField hdnEquipmentType = (HiddenField)gvEquipment.Rows[i].FindControl("hdnEquipmentType");
                    Label lblOtherEquiType = (Label)gvEquipment.Rows[i].FindControl("lblOthetEuipmentType");
                    table.Rows.Add(lblProductionName.Text, lblQuantity.Text, lblEuipmentType.Text, hdnEquipmentType.Value, lblOtherEquiType.Text);
                }
            }
            gvEquipment.DataSource = table;
            gvEquipment.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            table = null;
        }
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
    protected void ddlEquipmentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtOtherEquipmentType.Visible = false;
        if (ddlEquipmentType.SelectedIndex > 0)
        {
            if (ddlEquipmentType.SelectedItem.Text == "Others")
            {
                txtOtherEquipmentType.Visible = true;
            }
            else
            {
                txtOtherEquipmentType.Text = "";
                txtOtherEquipmentType.Visible = false;
            }
        }
    }
    #endregion
}