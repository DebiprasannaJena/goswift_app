using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.Linq;
using BusinessLogicLayer.Incentive;


public partial class incentives_Quality_Certification100 : SessionCheck
{

    #region Global Veriables

    string gFilePath = "../incentives/Files";
    string fillename;
    DataTable gObjDtAssistanceDetails = new DataTable();
    DataTable gObjDtIncentiveAvailed = new DataTable();
    DataTable gObjDtTermLoanDetails = new DataTable();
    DataTable dtFiles;
    string strMsg = "Incentive";

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillSalutation();
            #region AvailDetail
            crdtincentive();
            #endregion
            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
                Fill_PrePopulated_Files_Document_Master(Session["UnitCode"].ToString());
            }
            else
            {
                PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
                fillPage(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            }
        }
        txtsacdat.Attributes.Add("readonly", "readonly");
        txtCertDate.Attributes.Add("readonly", "readonly");
        txtRenewDate.Attributes.Add("readonly", "readonly");
        txtTotal.Attributes.Add("readonly", "readonly");
    }

    #endregion

    #region Avail Details


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
    public void AvailDetailAdd(Incentive objEntity)
    {
        AvailDetails objAvailDetails = new AvailDetails();

        // objAvailDetails.SubsidyAvailed = Convert.ToInt16(radNeverAvailedPrior.SelectedValue.ToString());
        objAvailDetails.NeverAvailedPrior = Convert.ToInt16(RadBtn_Availed_Earlier.SelectedValue.ToString());

        if (RadBtn_Availed_Earlier.SelectedValue == "1")
        {
            objAvailDetails.SanctionOrderDoc = Hid_Asst_Sanc_File_Name.Value;

            if (txtdiffclaimamt.Text.Trim() != "")
            {
                objAvailDetails.ClaimtExempted = Convert.ToDouble(txtdiffclaimamt.Text);
            }
            else
            {
                objAvailDetails.ClaimtExempted = 0;
            }

            List<Assistance> listIncentiveAvailed = new List<Assistance>();
            Assistance objIncentiveAvailed = new Assistance();
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
                    if (dr["vchsacdat"].ToString() != "")
                    {
                        objIncentiveAvailed.AvailedDate = Convert.ToDateTime(dr["vchsacdat"].ToString());
                    }

                    listIncentiveAvailed.Add(objIncentiveAvailed);
                }
            }

            objAvailDetails.IncentiveAvailed = listIncentiveAvailed;
        }
        else
        {
            objAvailDetails.UndertakingSubsidyDoc = Hid_Undertaking_File_Name.Value;
        }

        if (txtreimamt.Text.Trim() != "")
        {
            objAvailDetails.ClaimReimbursement = Convert.ToDouble(txtreimamt.Text);
        }
        else
        {
            objAvailDetails.ClaimReimbursement = 0;
        }

        objEntity.AvailDet = objAvailDetails;
    }

    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string FolderPath)
    {
        string filename = hdnFile.Value;
        string completePath = Server.MapPath(FolderPath + filename);
        //if (File.Exists(completePath))
        //{
        File.Delete(completePath);
        hdnFile.Value = "";
        lnkDel.Visible = false;
        lnkBtn.Visible = true;
        hplnk.Visible = false;
        lblFile.Visible = false;
        updFile.Enabled = true;
        //}
    }
    private void UploadDocument(FileUpload fuOrgDocument, HiddenField hdnOrgDocument, string strFileName, HyperLink hypOrdDocument, Label lblOrgDocument, LinkButton lnkOrgDocumentDelete, string FolderPath, string Extention)
    {
        string strMainFolderPath = Server.MapPath(FolderPath);
        if (!Directory.Exists(strMainFolderPath))
        {
            Directory.CreateDirectory(strMainFolderPath);
        }
        if (fuOrgDocument.HasFile)
        {
            string filename = string.Empty;
            if (Extention == "excel")
            {
                if ((Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".xls") && (Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".xlsx"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .xls/.xlsx file Only!','" + strMsg + "')", true);
                    return;
                }
                int fileSize = fuOrgDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + strMsg + "')", true);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(fuOrgDocument.FileName);
                }
            }
            else if (Extention == "pdf")
            {
                if ((Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".pdf"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .pdf file Only!','" + strMsg + "')", true);
                    return;
                }
                int fileSize = fuOrgDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + strMsg + "')", true);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(fuOrgDocument.FileName);
                }
            }
            else if (Extention == "pdf/zip")
            {
                if ((Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".pdf") && (Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".zip"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .pdf/zip file Only!','" + strMsg + "')", true);
                    return;
                }
                int fileSize = fuOrgDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + strMsg + "')", true);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(fuOrgDocument.FileName);
                }
            }
            fuOrgDocument.SaveAs(strMainFolderPath + filename);
            hdnOrgDocument.Value = filename;
            hypOrdDocument.NavigateUrl = FolderPath + filename;
            hypOrdDocument.Visible = true;
            lnkOrgDocumentDelete.Visible = true;
            lblOrgDocument.Visible = true;
            fuOrgDocument.Enabled = false;
        }
    }

    protected void lnkDocfirstinvestmentUpload_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        //---------------------------Avail Detail----------------------------------------------------
        if (string.Equals(lnk.ID, LnkBtn_Upload_Asst_Sanc_Doc.ID))
        {
            if (FU_Asst_Sanc_Doc.HasFile)
            {
                string strFileName = "ASSTSANC" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(FU_Asst_Sanc_Doc, Hid_Asst_Sanc_File_Name, strFileName, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, "../incentives/Files/AvailDetails/", "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, LnkBtn_Upload_Undertaking_Doc.ID))
        {
            if (FU_Undertaking_Doc.HasFile)
            {
                string strFileName = "UND" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(FU_Undertaking_Doc, Hid_Undertaking_File_Name, strFileName, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, "../incentives/Files/AvailDetails/", "pdf/zip");
            }
        }
    }

    protected void lnkDocfirstinvestmentDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
        if (string.Equals(lnk.ID, LnkBtn_Delete_Asst_Sanc_Doc.ID))
        {
            UpdFileRemove(Hid_Asst_Sanc_File_Name, LnkBtn_Upload_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, "../incentives/Files/AvailDetails/");
        }
        else if (string.Equals(lnk.ID, LnkBtn_Delete_Undertaking_Doc.ID))
        {
            UpdFileRemove(Hid_Undertaking_File_Name, LnkBtn_Upload_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, FU_Undertaking_Doc, "../incentives/Files/AvailDetails/");
        }
    }
    public void FillAvailed(DataTable dtavail, DataTable dtavailgrd1, DataTable dtavailgrd2)
    {
        if (dtavail.Rows.Count > 0)
        {
            txtdiffclaimamt.Text = dtavail.Rows[0]["decClaimExempted"].ToString();
            txtreimamt.Text = dtavail.Rows[0]["decClaimReimbursement"].ToString();

            RadBtn_Availed_Earlier.SelectedValue = dtavail.Rows[0]["intNeverAvailedPrior"].ToString();

            if (dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString() != "")
            {
                Hyp_View_Undertaking_Doc.Visible = true;
                Hyp_View_Undertaking_Doc.NavigateUrl = "../incentives/Files/AvailDetails/" + dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                Hid_Undertaking_File_Name.Value = dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString();

                FU_Undertaking_Doc.Enabled = false;
                LnkBtn_Upload_Undertaking_Doc.Enabled = false;
                LnkBtn_Delete_Undertaking_Doc.Visible = true;
            }
            else
            {
                Hyp_View_Undertaking_Doc.Visible = false;
                FU_Undertaking_Doc.Enabled = true;
                LnkBtn_Upload_Undertaking_Doc.Enabled = true;
                LnkBtn_Delete_Undertaking_Doc.Visible = false;
            }

            /*------------------------------------------------------------*/

            if (dtavail.Rows[0]["VchSanctionDoc"].ToString() != "")
            {
                Hyp_View_Asst_Sanc_Doc.Visible = true;
                Hyp_View_Asst_Sanc_Doc.NavigateUrl = "../incentives/Files/AvailDetails/" + dtavail.Rows[0]["VchSanctionDoc"].ToString();
                Hid_Asst_Sanc_File_Name.Value = dtavail.Rows[0]["VchSanctionDoc"].ToString();

                FU_Asst_Sanc_Doc.Enabled = false;
                LnkBtn_Upload_Asst_Sanc_Doc.Enabled = false;
                LnkBtn_Delete_Asst_Sanc_Doc.Visible = true;
            }
            else
            {
                Hyp_View_Asst_Sanc_Doc.Visible = false;
                FU_Asst_Sanc_Doc.Enabled = true;
                LnkBtn_Upload_Asst_Sanc_Doc.Enabled = true;
                LnkBtn_Delete_Asst_Sanc_Doc.Visible = false;
            }
        }

        if (dtavailgrd1.Rows.Count > 0)
        {
            DataTable dtincentive = new DataTable();
            dtincentive = (DataTable)ViewState["dtincentive"];

            foreach (DataRow dravgr1 in dtavailgrd1.Rows)
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

    #endregion

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


    public void Fill_PrePopulated_Files_Document_Master(string UnitCode)
    {
        DataTable dt = IncentiveManager.GetFile_FromDocMaster_UnitCodeWise(UnitCode);

        DataRow[] drCollection1 = dt.Select("vchDocId = 'D253'");
        if (drCollection1.Length == 1)
        {
            if (drCollection1[0]["vchFileName"].ToString() != "")
            {
                if (File.Exists("../incentives/Files/AvailDetails/" + drCollection1[0]["vchFileName"].ToString()))
                {
                    Hyp_View_Asst_Sanc_Doc.Visible = true;
                    LnkBtn_Delete_Asst_Sanc_Doc.Visible = true;
                    LnkBtn_Delete_Asst_Sanc_Doc.Enabled = true;
                    FU_Asst_Sanc_Doc.Enabled = false;
                    Hyp_View_Asst_Sanc_Doc.NavigateUrl = "../incentives/Files/AvailDetails/" + drCollection1[0]["vchFileName"].ToString();
                    Hid_Asst_Sanc_File_Name.Value = drCollection1[0]["vchFileName"].ToString();
                }
            }

        }
        DataRow[] drCollection2 = dt.Select("vchDocId = 'D230'");
        if (drCollection2.Length == 1)
        {
            if (drCollection2[0]["vchFileName"].ToString() != "")
            {
                if (File.Exists("../incentives/Files/AvailDetails/" + drCollection2[0]["vchFileName"].ToString()))
                {
                    Hyp_View_Undertaking_Doc.Visible = true;
                    LnkBtn_Delete_Undertaking_Doc.Visible = true;
                    LnkBtn_Delete_Undertaking_Doc.Enabled = true;
                    FU_Undertaking_Doc.Enabled = false;
                    Hyp_View_Undertaking_Doc.NavigateUrl = "../incentives/Files/AvailDetails/" + drCollection2[0]["vchFileName"].ToString();
                    Hid_Undertaking_File_Name.Value = drCollection2[0]["vchFileName"].ToString();
                }
            }
        }
    }
    #region Populate Common-Field Pre/Post
    #region viewdetail
    public void PrepopulateData(int id)
    {
        int tryint;
        //string UnitCode = Session["UnitCode"].ToString();
        DataSet dslivePre = IncentiveManager.PrepopulateData(id);
        DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
        DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment

        DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
        DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
        DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
        DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
        DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
        DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan

        //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();
        #region IndustrailUnit

        if (dtindustryPre.Rows.Count > 0)
        {
            lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
            //dtindustryPre.Rows[0]["intOrganisationType"].ToString();	
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

            //dtindustryPre.Rows[0]["intUnitType"].ToString();			
            lbl_Unit_Type.Text = dtindustryPre.Rows[0]["UnitTypename"].ToString();
            //dtindustryPre.Rows[0]["vchDocCode"].ToString();	

            //dtindustryPre.Rows[0]["vchUnitTypeDoc"].ToString();

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
            //dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();	

            lbl_Gender_Partner.Text = dtindustryPre.Rows[0]["GenderType"].ToString() + " " + dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();

            DdlGender.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
            TxtApplicantName.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            //dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            // Lbl_Org_Doc_Type.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
            Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
            //dtindustryPre.Rows[0]["vchCertOfRegdDocCode"].ToString();		


            //dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();
            lbl_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
            lbl_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
            lbl_PC_No.Text = dtindustryPre.Rows[0]["vchPcNo"].ToString();
            lbl_pcno_befor.Text = dtindustryPre.Rows[0]["vchpcnobefore"].ToString();
            lblGstin.Text = dtindustryPre.Rows[0]["VCHGSTIN"].ToString();
            lbl_Prod_Comm_Date_Before.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
            lbl_PC_Issue_Date_Before.Text = dtindustryPre.Rows[0]["dtmPCIssueDateBefore"].ToString();
            //dtindustryPre.Rows[0]["vchProdCommCertBeforeCode"].ToString();	
            //Lbl_Prod_Comm_Before_Doc_Name.Text = dtindustryPre.Rows[0]["ProdCommCertBeforeDocName"].ToString();
            if (dtindustryPre.Rows[0]["projectType"].ToString() == "1")
            {
                Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
            }
            else if (dtindustryPre.Rows[0]["projectType"].ToString() == "2")
            {
                Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPre.Rows[0]["vchappnobef"].ToString();
            }


            //dtindustryPre.Rows[0]["vchProdCommCertBefore"].ToString();
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
            //dtindustryPre.Rows[0]["vchProdCommCertAfterCode"].ToString();		
            //Lbl_Prod_Comm_After_Doc_Name.Text = dtindustryPre.Rows[0]["ProdCommCertAfterDocName"].ToString();

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
        if (dtProductionPre.Rows.Count > 0)
        {
            Grd_Production_Before.DataSource = dtProductionDetBefPre;
            Grd_Production_Before.DataBind();
            Grd_Production_After.DataSource = dtProductionDetAftPre;
            Grd_Production_After.DataBind();

            //dtProductionPre.Rows[0]["intProductionId"].ToString();
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
            //dtProductionPre.Rows[0]["vchEmpDocBeforeCode"].ToString();			
            Lbl_Direct_Emp_Before_Doc_Name.Text = dtProductionPre.Rows[0]["vchEmpDocBeforeCodeName"].ToString();
            Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionPre.Rows[0]["vchEmpDocAfter"].ToString();
            //dtProductionPre.Rows[0]["vchEmpDocAfterCode"].ToString();			
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

            //dtProductionPre.Rows[0]["intCreatedBy"].ToString();
        }
        #endregion


        //if (dtProductionDetBefPre.Rows.Count > 0)
        //{
        //    Grd_Production_Before.DataSource = dtProductionDetBef;
        //    Grd_Production_Before.DataBind();
        //}

        //if (dtProductionDetAftPre.Rows.Count > 0)
        //{
        //    Grd_Production_After.DataSource = dtProductionDetAft;
        //    Grd_Production_After.DataBind();
        //}		
        #region Investment
        if (dtInvestmentPre.Rows.Count > 0)
        {
            //dtInvestmentPre.Rows[0]["slno"].ToString();
            Txt_FFCI_Date_Before.Text = dtInvestmentPre.Rows[0]["dtmFFCIDateBefore"].ToString();
            //dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCode"].ToString();
            Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();
            Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchFFCIDocBefore"].ToString();
            lbl_Land_Before.Text = dtInvestmentPre.Rows[0]["decLandAmtBefore"].ToString();
            lbl_Building_Before.Text = dtInvestmentPre.Rows[0]["decBuildingAmtBefore"].ToString();
            lbl_Plant_Mach_Before.Text = dtInvestmentPre.Rows[0]["decPlantMachAmtBefore"].ToString();
            lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPre.Rows[0]["decOtheFixedAssetAmtBefore"].ToString();
            lbl_Total_Capital_Before.Text = dtInvestmentPre.Rows[0]["decTotalAmtBefore"].ToString();
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
        }
        #endregion

        #region MEANS OF FINANCE
        if (dtMeansFinancePre.Rows.Count > 0)
        {
            //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
            lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
            lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
            Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString();
            lbl_FDI_Componet.Text = dtMeansFinancePre.Rows[0]["decFDIComponet"].ToString();
            //dtMeansFinancePre.Rows[0]["intCreatedBy"].ToString();
            //dtMeansFinancePre.Rows[0]["vchTermLoanDocCode"].ToString();
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
        #endregion


        /*-----------------------------------------------------------*/
        ///// Get Documents (OSPCB,Factory & Boiler) from Service

        BindDocFromService(enServiceDocType.OSPCB, D275, flValidStatutary, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, hdnIsOsPCBDownloaded);
        BindDocFromService(enServiceDocType.Boiler, D280, flCleanApproveAuthority, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, hdnBoilderDownloaded);

    }
    public void PostpopulateData(int id)
    {
        DataSet dslivePre = IncentiveManager.PostpopulateData(id);
        DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
        DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment

        DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
        DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
        DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
        DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
        DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
        DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan

        //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();
        #region IndustrailUnit
        if (dtindustryPre.Rows.Count > 0)
        {
            lbl_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString();
            //dtindustryPre.Rows[0]["intOrganisationType"].ToString();	
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
        }
        #endregion

        #region Production
        if (dtProductionPre.Rows.Count > 0)
        {
            Grd_Production_Before.DataSource = dtProductionDetBefPre;
            Grd_Production_Before.DataBind();
            Grd_Production_After.DataSource = dtProductionDetAftPre;
            Grd_Production_After.DataBind();

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
        #endregion

        #region Investment

        //,vchFFCIDocBeforeCode,,,,,
        //    ,,INT_INCUNQUEID,vchProjectDocBeforeCode,vchProjectDocBefore,dtmFFCIDateAfter,
        //    vchFFCIDocAfterCode,vchFFCIDocAfter,decLandAmtAfter,decBuildingAmtAfter,decPlantMachAmtAfter,decOtheFixedAssetAmtAfter,
        //    decTotalAmtAfter,vchProjectDocAfterCode,vchProjectDocAfter,INT_CREATED_BY,DTM_CREATEDON

        //dtInvestmentPre.Rows[0]["slno"].ToString();
        if (dtInvestmentPre.Rows.Count > 0)
        {
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
        }
        #endregion

        #region MEANS OF FINANCE
        if (dtMeansFinancePre.Rows.Count > 0)
        {
            //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
            lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
            lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
            Hyp_View_Term_Loan_Doc.NavigateUrl = dtMeansFinancePre.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();
            lbl_FDI_Componet.Text = dtMeansFinancePre.Rows[0]["decFDIComponet"].ToString();
            //dtMeansFinancePre.Rows[0]["intCreatedBy"].ToString();
            //dtMeansFinancePre.Rows[0]["vchTermLoanDocCode"].ToString();
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
        #endregion
    }
    #endregion
    #endregion
    #region Button Click
    protected void btnApply_Click(object sender, EventArgs e)
    {
        try
        {
            Incentive objIncentive = new Incentive();
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

            if (Session["FyYear"] != null && !string.IsNullOrEmpty(Session["FyYear"].ToString()))
            {
                objIncentive.FYear = Convert.ToInt16(Convert.ToString(Session["FyYear"]));
            }
            else
            {
                objIncentive.FYear = 0;
            }

            objIncentive.incentivetype = 4;
            objIncentive.FormType = FormNumber.QualityCertification_14;

            dtFiles = createFileListTable();
            setIndustrialUnitDetails(objIncentive);
            objIncentive.QualityCertDet = SaveQualityCertification(objIncentive);
            AvailDetailAdd(objIncentive);
            objIncentive.AdditionalDocument = SaveAdditionalDocuments(objIncentive);
            SetDocMasterDetails(objIncentive);

            string retval = IncentiveManager.CreateIncentiveQualityCertificate(objIncentive);

            int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());

            Response.Redirect("PQC.aspx?InctUniqueNo=" + Convert.ToString(mstyp), false);
        }
        catch (Exception x)
        {
            Util.LogError(x, "incentive");
        }
    }
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        try
        {
            Incentive objIncentive = new Incentive();
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

            if ((Session["FyYear"] != null) && (Convert.ToString(Session["FyYear"]) != ""))
            {
                objIncentive.FYear = Convert.ToInt16(Convert.ToString(Session["FyYear"]));
            }
            else
            {
                objIncentive.FYear = 0;
            }
            objIncentive.incentivetype = 4;
            objIncentive.FormType = FormNumber.QualityCertification_14;

            dtFiles = createFileListTable();
            setIndustrialUnitDetails(objIncentive);
            objIncentive.QualityCertDet = SaveQualityCertification(objIncentive);
            AvailDetailAdd(objIncentive);

            objIncentive.AdditionalDocument = SaveAdditionalDocuments(objIncentive);
            SetDocMasterDetails(objIncentive);

            string retval = IncentiveManager.CreateIncentiveQualityCertificate(objIncentive);
            string msg = "<strong>Application Drafted Successfully !</strong>";
            string msgTtl = "SWP";

            if (retval.Split('~')[0].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + msg + "', '" + msgTtl + "');   </script>", false);
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }
    #endregion
    #region common section set
    public void setIndustrialUnitDetails(Incentive incentive)
    {
        incentive.IndsutUnitMstDet = new INDUSTRIAL_UNIT_MASTER();

        incentive.IndsutUnitMstDet.APPLICANTNAME_IND = TxtApplicantName.Text;
        if (DdlGender.SelectedIndex > 0)
        {
            incentive.IndsutUnitMstDet.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
        }
        //if (radApplyBy.SelectedIndex > 0)
        //{
        incentive.IndsutUnitMstDet.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
        //}
        incentive.IndsutUnitMstDet.AADHAARNO_IND = TxtAdhaar.Text;
        incentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
        //incentive.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND = radAuthorizing.SelectedValue;

    }
    #endregion
    #region common section fill
    public void FillIndustryFields(DataTable dtindustry)
    {
        try
        {
            if (dtindustry.Rows.Count > 0)
            {
                TxtApplicantName.Text = dtindustry.Rows[0]["VCHAPPLICANTNAME"].ToString().Trim();
                if (dtindustry.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                {
                    TxtAdhaar.Text = dtindustry.Rows[0]["VCHAADHAARNO"].ToString();

                    //TxtAdhaar1.Text = dtindustry.Rows[0]["VCHAADHAARNO"].ToString().Substring(0, 4);
                    //TxtAdhaar2.Text = dtindustry.Rows[0]["VCHAADHAARNO"].ToString().Substring(4, 4);
                    //TxtAdhaar3.Text = dtindustry.Rows[0]["VCHAADHAARNO"].ToString().Substring(8, 4);
                }
                DdlGender.SelectedValue = dtindustry.Rows[0]["INTGENDER"].ToString();
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
                hdnAUTHORIZEDFILE.Value = dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                if (dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString() != "")
                {
                    hdnAUTHORIZEDFILE.Value = dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(); /////AUTHORIZEDFILE file upload
                    hypAUTHORIZEDFILE.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtindustry.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    hypAUTHORIZEDFILE.Visible = true;
                    lnkAUTHORIZEDFILEDdelete.Visible = true;
                    FlupAUTHORIZEDFILE.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
    #region Quality Certification Details
    public void FillQualityCertification(DataTable dtQuality, DataTable dtQualityDt)
    {
        if (dtQuality.Rows.Count > 0)
        {
            txtTotal.Text = dtQuality.Rows[0]["intTotal"].ToString();
        }
        gvdQuality.DataSource = dtQualityDt;
        gvdQuality.DataBind();
    }
    public QualityCertification SaveQualityCertification(Incentive incentive)
    {
        decimal trydecimal;
        QualityCertification objQualityCertification = new QualityCertification();
        objQualityCertification.QualityCertificationActivitiesDetails = new List<EntityLayer.Incentive.QualityCertificationActivities>();
        objQualityCertification.decCertificationTotal = decimal.TryParse(txtTotal.Text.Trim(), out trydecimal) ? trydecimal : trydecimal;
        DataTable dt = BulkEntryQuality();
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                QualityCertificationActivities objlist = new QualityCertificationActivities();

                objlist.strProductName = dr["strProductName"].ToString();
                objlist.strNameaddressofRA = dr["strNameaddressofRA"].ToString();

                /*-------------------------------------------------------*/

                if (dr["strCertificateNo"].ToString() != "")
                {
                    objlist.strCertificateNo = dr["strCertificateNo"].ToString();
                    objlist.dtmCertificateDate = dr["dtmCertificateDate"].ToString();
                    objlist.strCertificateDetailsDOC = dr["strCertificateDetailsDOC"].ToString();
                }
                else
                {
                    objlist.strCertificateNo = null;
                    objlist.dtmCertificateDate = null;
                    objlist.strCertificateDetailsDOC = null;
                }

                /*-------------------------------------------------------*/

                if (dr["intRenewalSlno"].ToString() != "")
                {
                    objlist.intRenewalSlno = dr["intRenewalSlno"].ToString();
                    objlist.dtmRenewalDate = dr["dtmRenewalDate"].ToString();
                    objlist.strRenewalDateDOC = dr["strRenewalDateDOC"].ToString();
                }
                else
                {
                    objlist.intRenewalSlno = null;
                    objlist.dtmRenewalDate = null;
                    objlist.strRenewalDateDOC = null;
                }

                /*-------------------------------------------------------*/

                objlist.strAmountofExpenditure = Convert.ToDecimal(dr["strAmountofExpenditure"].ToString());
                objlist.strExpenditureDetails = dr["strExpenditureDetails"].ToString();

                objQualityCertification.QualityCertificationActivitiesDetails.Add(objlist);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return objQualityCertification;
    }
    public DataTable CreateDataTableQuality()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add(new DataColumn("strProductName"));
        dt.Columns.Add(new DataColumn("strNameaddressofRA"));

        dt.Columns.Add(new DataColumn("strCertificateNo"));
        dt.Columns.Add(new DataColumn("dtmCertificateDate"));
        dt.Columns.Add(new DataColumn("strCertificateDetailsDOC"));

        dt.Columns.Add(new DataColumn("intRenewalSlno"));
        dt.Columns.Add(new DataColumn("dtmRenewalDate"));
        dt.Columns.Add(new DataColumn("strRenewalDateDOC"));

        dt.Columns.Add(new DataColumn("strAmountofExpenditure"));
        dt.Columns.Add(new DataColumn("strExpenditureDetails"));

        return dt;

    }
    protected DataTable BulkEntryQuality()
    {
        DataTable dt = CreateDataTableQuality();

        foreach (GridViewRow grr in gvdQuality.Rows)
        {
            Label ProductName = (Label)grr.FindControl("lblstrProductName");
            Label NameaddressofRA = (Label)grr.FindControl("lblstrNameaddressofRA");

            Label CertificateNo = (Label)grr.FindControl("lblstrCertificateNo");
            Label CertificateDate = (Label)grr.FindControl("lbldtmCertificateDate");
            HyperLink CertificateDetailsDOC = (HyperLink)grr.FindControl("lbllblstrCertificateDetailsDOC");
            HiddenField Hid_Cert_Doc_File_Name = (HiddenField)grr.FindControl("Hid_Cert_Doc_File_Name");

            Label RenewalSlno = (Label)grr.FindControl("lblintRenewalSlno");
            Label RenewalDate = (Label)grr.FindControl("lbldtmRenewalDate");
            HyperLink RenewalDateDOC = (HyperLink)grr.FindControl("lblstrRenewalDateDOC");
            HiddenField Hid_Renewal_Doc_File_Name = (HiddenField)grr.FindControl("Hid_Renewal_Doc_File_Name");

            Label AmountofExpenditure = (Label)grr.FindControl("lblstrAmountofExpenditure");
            HyperLink ExpenditureDetails = (HyperLink)grr.FindControl("lblstrExpenditureDetails");
            HiddenField Hid_Expen_Doc_File_Name = (HiddenField)grr.FindControl("Hid_Expen_Doc_File_Name");

            dt.Rows.Add(
            ProductName.Text.Trim(),
            NameaddressofRA.Text.Trim(),

            CertificateNo.Text.Trim(),
            CertificateDate.Text.Trim(),
                //CertificateDetailsDOC.Text.Trim(),
            Hid_Cert_Doc_File_Name.Value.Trim(),

            RenewalSlno.Text.Trim(),
            RenewalDate.Text.Trim(),
                // RenewalDateDOC.Text.Trim(),
            Hid_Renewal_Doc_File_Name.Value.Trim(),

            AmountofExpenditure.Text.Trim(),
                //ExpenditureDetails.Text.Trim()
            Hid_Expen_Doc_File_Name.Value.Trim()

            );

        }
        return dt;
    }
    protected void BulkPlusEntryQuality(object sender, EventArgs e)
    {
        decimal trydecimal;
        decimal total = 0;
        string CertDoc = "";
        string renewDoc = "";
        string expDoc = "";

        DataTable dt = CreateDataTableQuality();

        foreach (GridViewRow grr in gvdQuality.Rows)
        {
            Label ProductName = (Label)grr.FindControl("lblstrProductName");
            Label NameaddressofRA = (Label)grr.FindControl("lblstrNameaddressofRA");

            Label CertificateNo = (Label)grr.FindControl("lblstrCertificateNo");
            Label CertificateDate = (Label)grr.FindControl("lbldtmCertificateDate");
            HyperLink CertificateDetailsDOC = (HyperLink)grr.FindControl("lbllblstrCertificateDetailsDOC");
            HiddenField Hid_Cert_Doc_File_Name = (HiddenField)grr.FindControl("Hid_Cert_Doc_File_Name");

            Label RenewalSlno = (Label)grr.FindControl("lblintRenewalSlno");
            Label RenewalDate = (Label)grr.FindControl("lbldtmRenewalDate");
            HyperLink RenewalDateDOC = (HyperLink)grr.FindControl("lblstrRenewalDateDOC");
            HiddenField Hid_Renewal_Doc_File_Name = (HiddenField)grr.FindControl("Hid_Renewal_Doc_File_Name");

            Label AmountofExpenditure = (Label)grr.FindControl("lblstrAmountofExpenditure");
            HyperLink ExpenditureDetails = (HyperLink)grr.FindControl("lblstrExpenditureDetails");
            HiddenField Hid_Expen_Doc_File_Name = (HiddenField)grr.FindControl("Hid_Expen_Doc_File_Name");

            dt.Rows.Add(
                ProductName.Text.Trim(),
                NameaddressofRA.Text.Trim(),

                CertificateNo.Text.Trim(),
                CertificateDate.Text.Trim(),
                //CertificateDetailsDOC.Text.Trim(),
                Hid_Cert_Doc_File_Name.Value.Trim(),

                RenewalSlno.Text.Trim(),
                RenewalDate.Text.Trim(),
                //RenewalDateDOC.Text.Trim(),
                 Hid_Renewal_Doc_File_Name.Value.Trim(),

                AmountofExpenditure.Text.Trim(),
                //ExpenditureDetails.Text.Trim()
                 Hid_Expen_Doc_File_Name.Value.Trim()
            );

            total += decimal.TryParse(AmountofExpenditure.Text.Trim(), out trydecimal) ? trydecimal : trydecimal;
        }

        if (txtProductnameQuality.Text.Trim() != "" && txtAddress.Text.Trim() != "" && txtAmountExpenditure.Text.Trim() != "")
        {
            if (fuCertificateDetailsDoc.HasFile)
            {
                string fillename = fuCertificateDetailsDoc.FileName;
                if (Path.GetExtension(fillename).ToUpper() == ".PDF")
                {
                    bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/QualityCertificate/"));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath("../incentives/Files/QualityCertificate/"));
                    string extension = Path.GetExtension(fuCertificateDetailsDoc.PostedFile.FileName);
                    fillename = "QualityCertificateC" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
                    fuCertificateDetailsDoc.SaveAs(Server.MapPath("../incentives/Files/QualityCertificate/") + fillename);
                    hplCertificateDetailsDoc.NavigateUrl = fillename;
                    CertDoc = fillename;
                }
                else
                {
                    Response.Write("<script>alart('Upload only pdf file')</script>");
                }
            }
            else if (hplCertificateDetailsDoc.NavigateUrl != "")
            {
                fillename = hplCertificateDetailsDoc.NavigateUrl;
            }


            string fillename1 = "";
            if (fuRenewalDetails.HasFile)
            {
                fillename1 = fuRenewalDetails.FileName;
                if (Path.GetExtension(fillename1).ToUpper() == ".PDF")
                {
                    bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/QualityCertificate/"));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath("../incentives/Files/QualityCertificate/"));
                    string extension = Path.GetExtension(fuRenewalDetails.PostedFile.FileName);
                    fillename1 = "QualityCertificateR" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
                    fuRenewalDetails.SaveAs(Server.MapPath("../incentives/Files/QualityCertificate/") + fillename1);
                    hplRenewalDetails.NavigateUrl = fillename1;
                }
                else
                {
                    Response.Write("<script>alart('Upload only pdf file')</script>");
                }
                renewDoc = fillename1;
            }

            else if (hplRenewalDetails.NavigateUrl != "")
            {
                fillename1 = hplRenewalDetails.NavigateUrl;
            }


            string fillename2 = "";
            if (fuExpenditure.HasFile)
            {
                fillename2 = fuExpenditure.FileName;
                if (Path.GetExtension(fillename2).ToUpper() == ".PDF")
                {
                    bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/QualityCertificate/"));
                    if (!folderExists)
                        Directory.CreateDirectory(Server.MapPath("../incentives/Files/QualityCertificate/"));
                    string extension = Path.GetExtension(fuExpenditure.PostedFile.FileName);
                    fillename2 = "QualityCertificateE" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
                    fuExpenditure.SaveAs(Server.MapPath("../incentives/Files/QualityCertificate/") + fillename2);
                    hplExpenditure.NavigateUrl = fillename2;
                    expDoc = fillename2;
                }
                else
                {
                    Response.Write("<script>alart('Upload only pdf file')</script>");
                }
            }
            else if (hplExpenditure.NavigateUrl != "")
            {
                fillename2 = hplExpenditure.NavigateUrl;
            }



            dt.Rows.Add(
              txtProductnameQuality.Text.Trim(),
            txtAddress.Text.Trim(),

            txtCertificateNo.Text.Trim(),
            txtCertDate.Value.ToString(),
            CertDoc,

            txtRenewal.Text.Trim(),
             txtRenewDate.Value.ToString(),
             renewDoc,

             txtAmountExpenditure.Text.Trim(),
             expDoc

               );

            total += decimal.TryParse(txtAmountExpenditure.Text.Trim(), out trydecimal) ? trydecimal : trydecimal;

        }
        gvdQuality.DataSource = dt;
        gvdQuality.DataBind();

        txtTotal.Text = total.ToString();

        /*-------------------------------------------------*/
        ///// Clear fields after add
        txtProductnameQuality.Text = "";
        txtAddress.Text = "";
        txtCertificateNo.Text = "";
        txtCertDate.Value = "";
        txtRenewal.Text = "";
        txtRenewDate.Value = "";
        txtAmountExpenditure.Text = "";
    }
    protected void BulkMinusEntryQuality(object sender, ImageClickEventArgs e)
    {
        decimal trydecimal;
        decimal total = 0;
        GridViewRow GR = (GridViewRow)((ImageButton)sender).NamingContainer;

        int deletedRowIndex = GR.RowIndex;

        DataTable dt = CreateDataTableQuality();

        foreach (GridViewRow grr in gvdQuality.Rows)
        {
            if (grr.RowIndex != deletedRowIndex)
            {
                Label ProductName = (Label)grr.FindControl("lblstrProductName");
                Label NameaddressofRA = (Label)grr.FindControl("lblstrNameaddressofRA");

                Label CertificateNo = (Label)grr.FindControl("lblstrCertificateNo");
                Label CertificateDate = (Label)grr.FindControl("lbldtmCertificateDate");
                HyperLink CertificateDetailsDOC = (HyperLink)grr.FindControl("lbllblstrCertificateDetailsDOC");
                HiddenField Hid_Cert_Doc_File_Name = (HiddenField)grr.FindControl("Hid_Cert_Doc_File_Name");

                Label RenewalSlno = (Label)grr.FindControl("lblintRenewalSlno");
                Label RenewalDate = (Label)grr.FindControl("lbldtmRenewalDate");
                HyperLink RenewalDateDOC = (HyperLink)grr.FindControl("lblstrRenewalDateDOC");
                HiddenField Hid_Renewal_Doc_File_Name = (HiddenField)grr.FindControl("Hid_Renewal_Doc_File_Name");

                Label AmountofExpenditure = (Label)grr.FindControl("lblstrAmountofExpenditure");
                HyperLink ExpenditureDetails = (HyperLink)grr.FindControl("lblstrExpenditureDetails");
                HiddenField Hid_Expen_Doc_File_Name = (HiddenField)grr.FindControl("Hid_Expen_Doc_File_Name");


                dt.Rows.Add(

                ProductName.Text.Trim(),
                NameaddressofRA.Text.Trim(),

                CertificateNo.Text.Trim(),
                CertificateDate.Text.Trim(),
                    //CertificateDetailsDOC.Text.Trim(),
                 Hid_Cert_Doc_File_Name.Value.Trim(),

                RenewalSlno.Text.Trim(),
                RenewalDate.Text.Trim(),
                    //RenewalDateDOC.Text.Trim(),
                 Hid_Renewal_Doc_File_Name.Value.Trim(),

                AmountofExpenditure.Text.Trim(),
                    // ExpenditureDetails.Text.Trim() 
                Hid_Expen_Doc_File_Name.Value.Trim()

                );
                total += decimal.TryParse(AmountofExpenditure.Text.Trim(), out trydecimal) ? trydecimal : trydecimal;
            }
        }
        gvdQuality.DataSource = dt;
        gvdQuality.DataBind();
        txtTotal.Text = total.ToString();
    }
    public void Clear()
    {
        txtProductnameQuality.Text = "";
        txtAddress.Text = "";
        txtCertificateNo.Text = "";
        txtCertDate.Value = "";
        txtRenewDate.Value = "";
        txtRenewal.Text = "";
        txtAmountExpenditure.Text = "";


    }
    #endregion
    #region Additional Document

    protected AdditionalDocuments SaveAdditionalDocuments(Incentive incentive)
    {
        AdditionalDocuments objAdditionalDoc = new EntityLayer.Incentive.AdditionalDocuments();
        try
        {
            if (hdnIsOsPCBDownloaded.Value == "1")
            {
                SaveServiceDoc(enServiceDocType.OSPCB, D275, "AdditionalDocument");
            }
            if (hdnBoilderDownloaded.Value == "1")
            {
                SaveServiceDoc(enServiceDocType.Boiler, D280, "AdditionalDocument");
            }

            objAdditionalDoc.strValidSatutoryGreenCategory = D275.Value;
            objAdditionalDoc.strCondoDocumentationDelay = D274.Value;
            objAdditionalDoc.strCleanApproveAuthorityOSPCB = D280.Value;
            addFileDataTableRow(ref dtFiles, D275.ID, D275.Value, "~/incentives/Files/AdditionalDocument", incentive.UnitCode);
            addFileDataTableRow(ref dtFiles, D274.ID, D274.Value, "~/incentives/Files/AdditionalDocument", incentive.UnitCode);
            addFileDataTableRow(ref dtFiles, D280.ID, D280.Value, "~/incentives/Files/AdditionalDocument", incentive.UnitCode);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return objAdditionalDoc;
    }
    private void Upload_File(FileUpload UploadFile, HiddenField hdnField, string FolderName)
    {
        gFilePath = "../incentives/Files";
        string strtime = "4_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        if (UploadFile.PostedFile.FileName != null && UploadFile.PostedFile.FileName != "")
        {

            string lPageName = string.Empty;
            string lFileExt = string.Empty;
            try
            {
                if (UploadFile.PostedFile.FileName != null && UploadFile.PostedFile.FileName != "")
                {
                    lFileExt = System.IO.Path.GetExtension(UploadFile.FileName);
                    if (lFileExt == ".pdf" || lFileExt == ".doc" || lFileExt == ".docx" || lFileExt == ".xls" || lFileExt == ".xlsx")
                    {
                        lPageName = FolderName + strtime + lFileExt;
                    }

                    if (!Directory.Exists(Server.MapPath(gFilePath + "/" + FolderName)))
                    {
                        // Create the directory.
                        Directory.CreateDirectory(Server.MapPath(gFilePath + "/" + FolderName));
                    }
                    gFilePath = Server.MapPath(gFilePath + "/" + FolderName + "/" + lPageName);
                    if (File.Exists(gFilePath))
                    {
                        File.Delete(gFilePath);
                    }
                    UploadFile.PostedFile.SaveAs(gFilePath);
                }
                hdnField.Value = lPageName;
                UploadFile.Dispose();

            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Incentive");
            }
        }
    }
    #endregion
    #region Fill
    public void fillPage(int id)
    {
        Incentive objIncentive = new Incentive();
        GetAndViewParam gv = new GetAndViewParam();
        objIncentive.strcActioncode = "14";
        gv.Param1ID = "";
        gv.Param2ID = "";
        gv.Param3ID = "";
        gv.InctType = 4;
        objIncentive.UnqIncentiveId = id;
        objIncentive.GetVwPrmtrs = gv;
        objIncentive.FormType = FormNumber.QualityCertification_14;
        DataSet dslive = IncentiveManager.GetIncentiveQuality(objIncentive);

        DataTable dtApplyStatusOrFinalSaveStatus = dslive.Tables[8];
        DataTable dtIndustry = dslive.Tables[0];
        FillIndustryFields(dtIndustry);

        #region Fill Quality Certification
        DataTable dtQuality = dslive.Tables[1];
        DataTable dtQualityDt = dslive.Tables[2];
        if (dtQuality.Rows.Count > 0)
            FillQualityCertification(dtQuality, dtQualityDt);
        #endregion

        #region Fill Additional Document
        DataTable dtAddDoc = dslive.Tables[6];

        if (dtAddDoc.Rows.Count > 0)
        {
            D275.Value = dtAddDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
            D274.Value = dtAddDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
            D280.Value = dtAddDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();

            if (D275.Value != "")
            {
                string Path = "~/incentives/Files/AdditionalDocument";
                string filename = D275.Value;
                FileViewProcess(flValidStatutary, D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, Path, filename);
            }
            if (D274.Value != "")
            {
                string Path = "~/incentives/Files/AdditionalDocument";
                string filename = D274.Value;
                FileViewProcess(flDelay, D274, lnkUDelay, lnkDDelay, hypDelay, lblDelay, Path, filename);
            }
            if (D280.Value != "")
            {
                string Path = "~/incentives/Files/AdditionalDocument";
                string filename = D280.Value;
                FileViewProcess(flCleanApproveAuthority, D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, Path, filename);
            }
        }
        #endregion

        DataTable dtAvaildt1 = dslive.Tables[3];
        DataTable dtAvaildt2 = dslive.Tables[4];
        DataTable dtAvaildt3 = dslive.Tables[5];
        FillAvailed(dtAvaildt1, dtAvaildt3, dtAvaildt2);
    }
    #endregion

    protected void lnkUValidStatutary_click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        UploadProcess(flValidStatutary, D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, Path, "ValidStatutary");
    }
    protected void lnkDValidStatutary_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        DeleteProcess(flValidStatutary, D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, Path, "ValidStatutary");
    }
    protected void lnkUCleanApproveAuthority_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        UploadProcess(flCleanApproveAuthority, D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, Path, "STATUTORYCLEARANCE");
    }
    protected void lnkDCleanApproveAuthority_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        DeleteProcess(flCleanApproveAuthority, D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, Path, "STATUTORYCLEARANCE");
    }
    protected void lnkUDelay_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        UploadProcess(flDelay, D274, lnkUDelay, lnkDDelay, hypDelay, lblDelay, Path, "Delay");
    }
    protected void lnkDDelay_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        DeleteProcess(flDelay, D274, lnkUDelay, lnkDDelay, hypDelay, lblDelay, Path, "Delay");
    }

    #region Extra
    public string UploadFile(FileUpload fileControl, string Path, HiddenField hdn)
    {
        string FileName = "";
        try
        {

            if (fileControl.HasFile)
            {
                string path = Server.MapPath(Path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                FileName = Guid.NewGuid().ToString() + fileControl.FileName.ToString();
                string FileNamewithPath = Server.MapPath(Path + "/" + FileName);
                fileControl.SaveAs(FileNamewithPath);


                if (hdn.Value != "")
                {
                    string deleteFilePath = Server.MapPath(Path + "/" + hdn.Value);
                    if (File.Exists(deleteFilePath))
                        File.Delete(deleteFilePath);
                }


            }
            else if (hdn.Value != "")
            {
                string PreviousFilePath = Server.MapPath(Path + "/" + hdn.Value);
                if (File.Exists(PreviousFilePath))
                    FileName = hdn.Value;
                else
                    FileName = "";
            }
            else
            {
                FileName = "";
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
            FileName = "";
        }
        return FileName;
    }
    #endregion

    #region File Upload/Delete Event Sections

    protected void lnkUSubsidyAvailed_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AvailDetails";
        //UploadProcess(flSubsidyAvailed, D230, lnkUSubsidyAvailed, lnkDSubsidyAvailed, hypSubsidyAvailed, lblSubsidyAvailed, Path, "SubsidyAvailed");
    }
    protected void lnkDSubsidyAvailed_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AvailDetails";
        // DeleteProcess(flSubsidyAvailed, D230, lnkUSubsidyAvailed, lnkDSubsidyAvailed, hypSubsidyAvailed, lblSubsidyAvailed, Path, "SubsidyAvailed");
    }

    protected void lnkUSupportingDocs_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AvailDetails";
        // UploadProcess(flSupportingDocs, D117, lnkUSupportingDocs, lnkDSupportingDocs, hypSupportingDocs, lblSupportingDocs, Path, "SupportingDocs");
    }
    protected void lnkDSupportingDocs_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AvailDetails";
        // DeleteProcess(flSupportingDocs, D117, lnkUSupportingDocs, lnkDSupportingDocs, hypSupportingDocs, lblSupportingDocs, Path, "SupportingDocs");
    }

    protected void lnkDocumentUpload_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/InctBasicDoc";
        UploadProcess(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, Path, "AUTHORIZEDFILE");
    }
    protected void lnkDocumentDelete_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/InctBasicDoc";
        DeleteProcess(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, Path, "AUTHORIZEDFILE");
    }

    #region Common Functions

    public void DeleteProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string ModuleName)
    {
        string fileName = hdn.Value;

        if (fileName != "")
            if (File.Exists(Server.MapPath(xPath + "/" + fileName)))
                File.Delete(Server.MapPath(xPath + "/" + fileName));

        hdn.Value = "";
        hyp.NavigateUrl = "";

        F.Enabled = true;
        LU.Visible = true;

        LD.Visible = false;
        hyp.Visible = false;
        lblMsg.Visible = false;
    }
    public void UploadProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string ModuleName)
    {
        string fileName = "";
        fileName = UploadX(F, xPath, ModuleName);

        if (fileName != "")
        {
            if (IsFileValid(F) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }

            if (File.Exists(Server.MapPath(xPath + "/" + fileName)))
            {
                hdn.Value = fileName;//also in view time
                hyp.NavigateUrl = xPath + "/" + fileName;

                F.Enabled = false;
                LU.Visible = false;

                LD.Visible = true;
                hyp.Visible = true;
                lblMsg.Visible = true;//not in view time  (false)
            }
        }
    }
    public string UploadX(FileUpload fileControl, string xPath, string ModuleName)
    {
        string FileName = "";
        try
        {

            if (fileControl.HasFile)
            {
                string yPath = Server.MapPath(xPath);
                if (!Directory.Exists(yPath))
                {
                    Directory.CreateDirectory(yPath);
                }
                string FileExtension = Path.GetExtension(fileControl.FileName);
                //extension check
                if (FileExtension != ".pdf" && FileExtension != ".zip" && FileExtension != ".doc")
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "jAlert('File format should be pdf/zip/doc.','Incentive')", true);
                }
                //file size check
                else if (fileControl.PostedFile.ContentLength > (4 * 1028 * 1028))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "jAlert('File size can not be more than 4 MB.','Incentive')", true);
                }
                else
                {
                    FileName = ModuleName + DateTime.Now.ToString("_ddMMyyyyHHmmss_") + DateTime.Now.Millisecond.ToString() + FileExtension;
                    string FileNamewithPath = Server.MapPath(xPath + "/" + FileName);
                    fileControl.SaveAs(FileNamewithPath);
                }
            }


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
            FileName = "";
        }
        return FileName;
    }
    public void FileViewProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string FileName)
    {
        string fileName = FileName;

        if (fileName != "")
        {
            if (File.Exists(Server.MapPath(xPath + "/" + fileName)))
            {
                hdn.Value = fileName;//also in view time
                hyp.NavigateUrl = xPath + "/" + fileName;
                F.Enabled = false;
                LU.Visible = false;

                LD.Visible = true;
                hyp.Visible = true;
                lblMsg.Visible = false;
            }
        }
    }
    public DataTable createFileListTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("DocID", typeof(string));
        dt.Columns.Add("FileName", typeof(string));
        dt.Columns.Add("Path", typeof(string));
        dt.Columns.Add("UnitCode", typeof(string));
        return dt;
    }
    public void addFileDataTableRow(ref DataTable dt, string docid, string fileName, string xPath, string unitcode)
    {
        if (fileName != "")
        {
            dt.Rows.Add(docid, fileName, xPath, unitcode);
        }
    }

    #endregion

    #endregion

    #region Bind Document From Service

    ///// Added by Sushant Jena on Dated 04-Jan-2018

    /// <summary>
    /// Function to delete all the service document saved in temp folder and create the new ones in respective folder
    /// </summary>
    /// <param name="aEnserviceDocType">service doc type</param>
    /// <param name="hdnFileName">hiddenfield that has the file name</param>
    private void SaveServiceDoc(enServiceDocType aEnserviceDocType, HiddenField hdnFileName, string destfoldername)
    {
        try
        {
            string strPreFix = string.Empty;
            if (aEnserviceDocType == enServiceDocType.Boiler)
            {
                strPreFix = "FactoryBoiler";
            }
            else if (aEnserviceDocType == enServiceDocType.OSPCB)
            {
                strPreFix = "OSPCB";
            }
            string strSourceFile = Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}.zip", strPreFix, Session["investorId"].ToString()));
            if (File.Exists(strSourceFile))
            {
                string strDestinationFile = Server.MapPath(string.Format("~/incentives/Files/{1}/{0}", hdnFileName.Value, destfoldername));
                File.Copy(strSourceFile, strDestinationFile, true);
                hdnFileName.Value = strPreFix + DateTime.Now.ToString("_ddMMyyhhmmss") + ".zip";
                string strReNameFile = Server.MapPath(string.Format("~/incentives/Files/{1}/{0}", hdnFileName.Value, destfoldername));

                System.IO.File.Move(strDestinationFile, strReNameFile);
                //then delete the old folder and old zip folder
                File.Delete(Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}.zip", strPreFix, Session["investorId"].ToString())));
                Directory.Delete(Server.MapPath(string.Format("~/incentives/Files/{0}_Temp/{1}", strPreFix, Session["investorId"].ToString())), true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    /// <summary>
    /// Functiion to get certificate details for OSPCB/Factory and boiler from service
    /// </summary>
    /// <param name="aEnServiceDocType">Service Doc Type i.e whether boiler or factory</param>
    /// <param name="Hid_File_Name">name of hidden field in which the document filename will be stored</param>
    /// <param name="FU_Upload">fileupload control for the document to disable them in case service has document</param>
    /// <param name="LnkBtn_Add">add linkbutton for the document to disable them in case service has document</param>
    /// <param name="LnkBtn_Delete">delete linkbutton for the document to disable them in case service has document</param>
    /// <param name="Hyp_View_Doc">Hyperlink to view/download the document</param>
    /// <param name="Hid_Service_Doc_Status">hidden field to store the status as to whether doc is present or not</param>    
    private void BindDocFromService(enServiceDocType aEnServiceDocType, HiddenField Hid_File_Name, FileUpload FU_Upload, LinkButton LnkBtn_Add, LinkButton LnkBtn_Delete, HyperLink Hyp_View_Doc, HiddenField Hid_Service_Doc_Status)
    {
        try
        {
            /*------------------------------------------------------------------*/
            //first send the investorid to database and get all the records for documents
            InctSearch objSearch = new InctSearch()
            {
                intUserUnitType = Convert.ToInt32(Session["investorid"]),
                strActionCode = "view",
                intUnitType = (int)aEnServiceDocType
            };

            List<string> lstFiles = new List<string>();
            IncentiveMasterBusinessLayer objInctBuisnessLayer = new IncentiveMasterBusinessLayer();
            lstFiles = objInctBuisnessLayer.ViewInctOSPCBDetails(objSearch);

            /*------------------------------------------------------------------*/
            ////// File Zipping Process
            string strTempFilePath = IncentiveCommonFunctions.GetCertificateDetailsFromService(aEnServiceDocType, lstFiles, Convert.ToInt32(Session["investorid"]));

            if (!string.IsNullOrEmpty(strTempFilePath))
            {
                //set hidden field value
                Hid_File_Name.Value = string.Format("{0}.zip", Session["investorId"].ToString());

                //disable the file upload control
                FU_Upload.Enabled = false;
                LnkBtn_Add.Visible = false;

                //remove the delete button
                LnkBtn_Delete.Visible = false;

                //// Assign File Path to Hyperlink for Display
                Hyp_View_Doc.Visible = true;
                Hyp_View_Doc.NavigateUrl = strTempFilePath;
                Hid_Service_Doc_Status.Value = "1";
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }

    #endregion


    public void SetDocMasterDetails(Incentive incentive)
    {
        List<lstFileUpload> DocMasterDetails = new List<lstFileUpload>();

        foreach (DataRow dr in dtFiles.Rows)
        {
            lstFileUpload OneFileDetails = new lstFileUpload();
            OneFileDetails.vchDocId = dr["DocID"].ToString();
            OneFileDetails.vchFileName = dr["FileName"].ToString();
            OneFileDetails.vchFilePath = dr["Path"].ToString();
            DocMasterDetails.Add(OneFileDetails);
        }
        incentive.FileUploadDetails = DocMasterDetails;
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
    protected void gvdQuality_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_Cert_Doc_File_Name = (HiddenField)e.Row.FindControl("Hid_Cert_Doc_File_Name");
            HiddenField Hid_Renewal_Doc_File_Name = (HiddenField)e.Row.FindControl("Hid_Renewal_Doc_File_Name");
            HiddenField Hid_Expen_Doc_File_Name = (HiddenField)e.Row.FindControl("Hid_Expen_Doc_File_Name");

            HyperLink CertificateDetailsDOC = (HyperLink)e.Row.FindControl("lbllblstrCertificateDetailsDOC");
            HyperLink RenewalDateDOC = (HyperLink)e.Row.FindControl("lblstrRenewalDateDOC");
            HyperLink ExpenditureDetails = (HyperLink)e.Row.FindControl("lblstrExpenditureDetails");

            /*-----------------------------------------------------------*/

            if (Hid_Cert_Doc_File_Name.Value == "")
            {
                CertificateDetailsDOC.Visible = false;
            }
            else
            {
                CertificateDetailsDOC.Visible = true;
            }

            /*-----------------------------------------------------------*/

            if (Hid_Renewal_Doc_File_Name.Value == "")
            {
                RenewalDateDOC.Visible = false;
            }
            else
            {
                RenewalDateDOC.Visible = true;
            }

            /*-----------------------------------------------------------*/

            if (Hid_Expen_Doc_File_Name.Value == "")
            {
                ExpenditureDetails.Visible = false;
            }
            else
            {
                ExpenditureDetails.Visible = true;
            }

            /*-----------------------------------------------------------*/

        }
    }
}