using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.IO;
using System.Data;
using System.Linq;
using System.Collections.Specialized;
using BusinessLogicLayer.Incentive;

public partial class incentives_Exemption_Premium_Land_Conversion : SessionCheck
{
    Incentive objincUnit = new Incentive();
    string gFilePath = "../incentives/Files";
    List<LandInfo> listItm = new List<LandInfo>();
    string MsgTitle = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillSalutation();
            GetMasterdetails();
            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
            }
            else
            {
                PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            }
        }
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
            lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            objincUnit = new Incentive();

            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objincUnit.UnqIncentiveId = 0;
            }
            else
            {
                objincUnit.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }

            objincUnit.strcActioncode = "A";
            objincUnit.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objincUnit.PealNum = Convert.ToString(Session["ProposalNo"]);
            objincUnit.PCNum = Convert.ToString(Session["PCNo"]);
            objincUnit.UnitCode = Convert.ToString(Session["UnitCode"]);
            objincUnit.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objincUnit.Userid = Convert.ToInt16(Session["InvestorId"]);
            objincUnit.Createdby = Convert.ToInt16(Session["InvestorId"]);
            //objincUnit.FYear = Convert.ToInt16(Session["FyYear"]);
            objincUnit.incentivetype = 4;
            objincUnit.FormType = FormNumber.PremiumLeviableForConversionOfLandForIndustrialUse_11;

            IndustryDataSave();
            AdditionalDocument();
            SaveLandDetails();
            FileUploadControls();

            string retval = IncentiveManager.CreateIncentiveLandSubsidy(objincUnit);
            if (retval.Split('~')[0].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !!</strong>', '" + MsgTitle + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objincUnit = null;
        }
    }
    protected void BtnApply_Click(object sender, EventArgs e)
    {
        try
        {
            objincUnit = new Incentive();

            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objincUnit.UnqIncentiveId = 0;
            }
            else
            {
                objincUnit.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }

            objincUnit.strcActioncode = "A";
            objincUnit.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objincUnit.PealNum = Convert.ToString(Session["ProposalNo"]);
            objincUnit.PCNum = Convert.ToString(Session["PCNo"]);
            objincUnit.UnitCode = Convert.ToString(Session["UnitCode"]);
            objincUnit.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objincUnit.Userid = Convert.ToInt16(Session["InvestorId"]);
            objincUnit.Createdby = Convert.ToInt16(Session["InvestorId"]);
            // objincUnit.FYear = Convert.ToInt16(Session["FyYear"]);
            objincUnit.incentivetype = 4;
            objincUnit.FormType = FormNumber.PremiumLeviableForConversionOfLandForIndustrialUse_11;

            IndustryDataSave();
            AdditionalDocument();
            SaveLandDetails();
            FileUploadControls();

            string retval = IncentiveManager.CreateIncentiveLandSubsidy(objincUnit);

            int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
            /////------------------------------------------------------------------------------------------------
            Response.Redirect("Exemption_Premium_FormPreview.aspx?InctUniqueNo=" + Convert.ToString(mstyp));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objincUnit = null;
        }
    }

    #region Save Module

    #region Industry Unit Details
    /// <summary>
    /// Method to save the file upload content on in the name of dattime second format
    /// </summary>
    /// <param name="AUTHORIZEDFILE"></param>
    /// <param name="RehabilDoc"></param>
    /// <param name="IndustryUnitDoc"></param>
    /// <param name="PinoneerDoc"></param>
    /// <param name="CertificateRegistration"></param>
    /// <param name="CertificateCommence"></param>
    public void SaveInd_Document(ref string AUTHORIZEDFILE, ref string RehabilDoc, ref string IndustryUnitDoc, ref string PinoneerDoc, ref string CertificateRegistration, ref string CertificateCommence)
    {
        try
        {
            string dtms = System.DateTime.Now.ToString("_ddMMyyhhmmss");
            string extension = "";
            bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/IndustryUnit/"));
            if (!folderExists)
                Directory.CreateDirectory(Server.MapPath("../incentives/Files/IndustryUnit/"));
            if (FlupAUTHORIZEDFILE.HasFile)
            {
                extension = Path.GetExtension(FlupAUTHORIZEDFILE.PostedFile.FileName);
                AUTHORIZEDFILE = "AUTHORIZEDFILE_" + dtms + extension;
                FlupAUTHORIZEDFILE.SaveAs(Server.MapPath("../incentives/Files/IndustryUnit/") + AUTHORIZEDFILE);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    /// <summary>
    /// change date from dd/MM/yyyy format to yyyy/MM/dd format inorderr to operatre in DB
    /// </summary>
    /// <param name="srcDate"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Attribute assignement to Industry master class object
    /// </summary>
    /// <returns></returns>
    public string IndustryDataSave()
    {
        string resdt = "1";
        try
        {
            objincUnit.IndsutUnitMstDet = new INDUSTRIAL_UNIT_MASTER();

            objincUnit.IndsutUnitMstDet.APPLICANTNAME_IND = TxtApplicantName.Text;
            objincUnit.IndsutUnitMstDet.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            if (radApplyBy.SelectedValue == "")
            {
                objincUnit.IndsutUnitMstDet.APPLYBY_IND = 0;
            }
            else
            {
                objincUnit.IndsutUnitMstDet.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }
            objincUnit.IndsutUnitMstDet.AADHAARNO_IND = TxtAdhaar1.Text;
            objincUnit.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objincUnit.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND = hidAuthorizing.Value;
        }
        catch (Exception ex)
        {
            resdt = ex.Message;
            Util.LogError(ex, "Incentive");
        }

        return resdt;
    }
    #endregion

    #region Additional Document
    public void AdditionalDocument()
    {
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

            objincUnit.AdditionalDocument = new EntityLayer.Incentive.AdditionalDocuments();
            objincUnit.AdditionalDocument.strValidSatutoryGreenCategory = D275.Value;
            objincUnit.AdditionalDocument.strCondoDocumentationDelay = D274.Value;
            objincUnit.AdditionalDocument.strCleanApproveAuthorityOSPCB = D280.Value;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    private void Upload_File(FileUpload UploadFile, HiddenField hdnField, string FolderName)
    {
        gFilePath = "../incentives/Files";
        string strtime = "4_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        // string strFile = Guid.NewGuid().ToString() ;
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
        else
        {
            //if (LnkClearanceCertificate.Text.Trim() != "")
            //    hdnField.Value = LnkClearanceCertificate.Text;
        }
    }

    protected void lnkUValidStatutary_click(object sender, EventArgs e)
    {
        try
        {
            if (flValidStatutary.HasFile)
            {
                string strFileName = "OSPCB" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(flValidStatutary, D275, strFileName, hypValidStatutary, lblValidStatutary, lnkDValidStatutary, "../incentives/Files/AdditionalDocument/", "pdf/zip");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void lnkDValidStatutary_Click(object sender, EventArgs e)
    {
        try
        {
            UpdFileRemove(D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, flValidStatutary, "../incentives/Files/AdditionalDocument/");
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void lnkUDelay_Click(object sender, EventArgs e)
    {
        try
        {
            if (flDelay.HasFile)
            {
                string strFileName = "Delay" + DateTime.Now.ToString("_ddMMyyhhmmss");

                UploadDocument(flDelay, D274, strFileName, hypDelay, lblDelay, lnkDDelay, "../incentives/Files/AdditionalDocument/", "pdf/zip");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void lnkDDelay_Click(object sender, EventArgs e)
    {
        try
        {
            UpdFileRemove(D274, lnkUDelay, lnkDDelay, hypDelay, lblDelay, flDelay, "../incentives/Files/AdditionalDocument/");
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void lnkUCleanApproveAuthority_Click(object sender, EventArgs e)
    {
        try
        {
            if (flCleanApproveAuthority.HasFile)
            {

                string strFileName = "Factory_Boiler" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(flCleanApproveAuthority, D280, strFileName, hypCleanApproveAuthority, lblCleanApproveAuthority, lnkDCleanApproveAuthority, "../incentives/Files/AdditionalDocument/", "pdf/zip");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void lnkDCleanApproveAuthority_Click(object sender, EventArgs e)
    {
        try
        {
            UpdFileRemove(D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, flCleanApproveAuthority, "../incentives/Files/AdditionalDocument/");
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #endregion

    #region LandDetails
    #region "control event"
    protected void lnkAddMore_Click(object sender, EventArgs e)
    {
        try
        {
            grvLandInfo.DataSource = GetTbWithLandInfoData();
            grvLandInfo.DataBind();
            txtMouza.Text = "";
            txtKhataNo.Text = "";
            txtPlotNo.Text = "";
            txtArea.Text = "";
            txtKisam.Text = "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #endregion
    #region "Grid event"
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("Mouza", typeof(string));
            table.Columns.Add("KhataNo", typeof(string));
            table.Columns.Add("PlotNo", typeof(string));
            table.Columns.Add("Area", typeof(string));
            table.Columns.Add("PresentKisam", typeof(string));

            for (int i = 0; i < grvLandInfo.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label lblMouza = (Label)grvLandInfo.Rows[i].FindControl("lblMouza");
                    Label lblKhataNo = (Label)grvLandInfo.Rows[i].FindControl("lblKhataNo");
                    Label lblPlotNo = (Label)grvLandInfo.Rows[i].FindControl("lblPlotNo");
                    Label lblArea = (Label)grvLandInfo.Rows[i].FindControl("lblArea");
                    Label lblPresentKisam = (Label)grvLandInfo.Rows[i].FindControl("lblPresentKisam");
                    table.Rows.Add(lblMouza.Text, lblKhataNo.Text, lblPlotNo.Text, lblArea.Text, lblPresentKisam.Text);
                }
            }
            grvLandInfo.DataSource = table;
            grvLandInfo.DataBind();
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
    #endregion
    #region Functions

    public DataTable GetTbWithLandInfoData() // this might be your sp for select
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("Mouza", typeof(string));
            table.Columns.Add("KhataNo", typeof(string));
            table.Columns.Add("PlotNo", typeof(string));
            table.Columns.Add("Area", typeof(string));
            table.Columns.Add("PresentKisam", typeof(string));
            table.Rows.Add(txtMouza.Text, txtKhataNo.Text, txtPlotNo.Text, txtArea.Text, txtKisam.Text);
            for (int i = 0; i < grvLandInfo.Rows.Count; i++)
            {
                Label lblMouza = (Label)grvLandInfo.Rows[i].FindControl("lblMouza");
                Label lblKhataNo = (Label)grvLandInfo.Rows[i].FindControl("lblKhataNo");
                Label lblPlotNo = (Label)grvLandInfo.Rows[i].FindControl("lblPlotNo");
                Label lblArea = (Label)grvLandInfo.Rows[i].FindControl("lblArea");
                Label lblPresentKisam = (Label)grvLandInfo.Rows[i].FindControl("lblPresentKisam");
                table.Rows.Add(lblMouza.Text, lblKhataNo.Text, lblPlotNo.Text, lblArea.Text, lblPresentKisam.Text);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
        }
        return table;
    }
    public void UploadLandDetailsFile()
    {
        try
        {
            string filename = "";
            if (fldLandDocument.HasFile)
            {
                string strFilePath = "../incentives/Files";
                string strextension = Path.GetExtension(fldLandDocument.PostedFile.FileName);
                if (strextension == ".pdf" || strextension == ".doc" || strextension == ".docx" || strextension == ".xls" || strextension == ".xlsx")
                {
                    filename = "LandDoc" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + strextension;
                }

                if (!Directory.Exists(Server.MapPath(strFilePath + "/LandDocument/")))
                {
                    Directory.CreateDirectory(Server.MapPath(strFilePath + "/LandDocument/"));
                }
                strFilePath = Server.MapPath(strFilePath + "/LandDocument/" + filename);
                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                }
                fldLandDocument.PostedFile.SaveAs(strFilePath);
                hdnFileUpload.Value = filename;
            }
            else
            {
                //if (LnkLandDocument.Text.Trim() != "")
                //{
                //    hdnFileUpload.Value = LnkLandDocument.Text.Trim();
                //}
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void SaveLandDetails()
    {
        List<LandInfo> listLandinfo = new List<LandInfo>();
        try
        {
            for (int intRow = 0; intRow < grvLandInfo.Rows.Count; intRow++)
            {
                LandInfo objLandinfo = new LandInfo();
                Label lblMouza = (Label)grvLandInfo.Rows[intRow].FindControl("lblMouza");
                Label lblKhataNo = (Label)grvLandInfo.Rows[intRow].FindControl("lblKhataNo");
                Label lblPlotNo = (Label)grvLandInfo.Rows[intRow].FindControl("lblPlotNo");
                Label lblArea = (Label)grvLandInfo.Rows[intRow].FindControl("lblArea");
                Label lblPresentKisam = (Label)grvLandInfo.Rows[intRow].FindControl("lblPresentKisam");
                objLandinfo.Mouza = lblMouza.Text;
                objLandinfo.KhataNo = lblKhataNo.Text;
                objLandinfo.PlotNo = lblPlotNo.Text;
                objLandinfo.Area = lblArea.Text;
                objLandinfo.PresentKisam = lblPresentKisam.Text;
                listLandinfo.Add(objLandinfo);
            }

            LandDetails objLandDetails = new LandDetails();
            objLandDetails.CostofProject = txtCostofProject.Text.Trim() == "" ? "0" : txtCostofProject.Text.Trim();
            objLandDetails.LandRequiredAsperReport = txtLandRequiredAsperRpt.Text.Trim() == "" ? "0" : txtLandRequiredAsperRpt.Text.Trim();
            objLandDetails.LandRequired = txtLandRequired.Text.Trim() == "" ? "0" : txtLandRequired.Text.Trim();
            //UploadLandDetailsFile();
            objLandDetails.LandDocument = hdnFileUpload.Value;
            objLandDetails.LANDUNDERTAKINGDOC = hdnLandUndertaking.Value;
            objLandDetails.Landconverted = listLandinfo;
            objincUnit.LandDet = objLandDetails;
            objincUnit.FormType = FormNumber.PremiumLeviableForConversionOfLandForIndustrialUse_11;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
    #endregion

    #endregion

    #region View & Fetch

    public void PrepopulateData(int id)
    {
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
            DivVisibilty(Lbl_Pioneer_Doc_Name, Div_Pioneer_Doc);

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
        DataSet dslivePre = IncentiveManager.PostpopulateDataLand(id);
        DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
        DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment

        DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
        DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
        DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
        DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
        DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
        DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan
        DataTable dtLandInfo = dslivePre.Tables[8];///////////Avail Details
        DataTable dtLandDet = dslivePre.Tables[9];///////////Avail Details

        DataTable dtAdditionalDoc = dslivePre.Tables[10];///////////Avail Details

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
            DivVisibilty(Lbl_Pioneer_Doc_Name, Div_Pioneer_Doc);

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

            //dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
            //Lbl_Org_Doc_Type.Text = dtindustryPre.Rows[0]["CertOfRegdDocName"].ToString();
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

            ////dtindustryPre.Rows[0]["intDistrictCode"].ToString();			
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
            //dtindustryPre.Rows[0]["intCreatedBy"].ToString();
            DdlGender.SelectedValue = dtindustryPre.Rows[0]["INTGENDER"].ToString();
            TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHAPPLICANTNAME"].ToString();
            if (dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
            {
                TxtAdhaar1.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString();
            }
            hdnAUTHORIZEDFILE.Value = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(); /////AUTHORIZEDFILE file upload
            ///----------------------
            FileVisibilty(hdnAUTHORIZEDFILE, hypAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, FlupAUTHORIZEDFILE, dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc", lnkAUTHORIZEDFILE);
            ///----------------------
            if (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "1")
            {
                radApplyBy.SelectedValue = dtindustryPre.Rows[0]["INTAPPLYBY"].ToString();
            }
            else if (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() == "2")
            {
                radApplyBy.SelectedValue = dtindustryPre.Rows[0]["INTAPPLYBY"].ToString();
            }
            else
            {
                radApplyBy.SelectedIndex = -1;
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
            Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePre.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();
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

        #region Land
        try
        {
            if (dtLandInfo.Rows.Count > 0)
            {
                txtCostofProject.Text = dtLandInfo.Rows[0]["VCHCOSTOFPROJECT"].ToString();
                txtLandRequiredAsperRpt.Text = dtLandInfo.Rows[0]["VCHLANDAREAPERPROJECT"].ToString();
                txtLandRequired.Text = dtLandInfo.Rows[0]["VCHLANDAREA"].ToString();
                if (dtLandInfo.Rows[0]["VCHLANDDOCUMENT"].ToString() != "")
                {
                    hdnFileUpload.Value = dtLandInfo.Rows[0]["VCHLANDDOCUMENT"].ToString();
                    lnkLandfileview.NavigateUrl = "../incentives/Files/LandDocument/" + dtLandInfo.Rows[0]["VCHLANDDOCUMENT"].ToString();
                    lnkLandfileview.Visible = true;
                    lnkLandDocumentDelete.Visible = true;
                    //lblOrgDocument.Visible = true;
                    fldLandDocument.Enabled = false;
                    lnkLandfileupload.Visible = false;
                }
                if (dtLandInfo.Rows[0]["VCHLANDUNDERTAKINGDOC"].ToString() != "")
                {
                    hdnLandUndertaking.Value = dtLandInfo.Rows[0]["VCHLANDUNDERTAKINGDOC"].ToString();
                    hypLandUndertaking.NavigateUrl = "../incentives/Files/LandDocument/" + dtLandInfo.Rows[0]["VCHLANDUNDERTAKINGDOC"].ToString();
                    hypLandUndertaking.Visible = true;
                    lnkDelLandUndertaking.Visible = true;
                    //lblOrgDocument.Visible = true;
                    fldLandUndertaking.Enabled = false;
                    lnkUpLandUndertaking.Visible = false;
                }

            }
            if (dtLandDet.Rows.Count > 0)
            {
                grvLandInfo.DataSource = dtLandDet;
                grvLandInfo.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        #endregion
        #region Additional Document
        if (dtAdditionalDoc.Rows.Count > 0)
        {
            D275.Value = dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
            D274.Value = dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
            D280.Value = dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();
            if (dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString() != "")
            {
                FileVisibilty(D275, hypValidStatutary, lnkDValidStatutary, flValidStatutary, dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString(), "AdditionalDocument", lnkUValidStatutary);
            }
            if (dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString() != "")
            {
                FileVisibilty(D274, hypDelay, lnkDDelay, flDelay, dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString(), "AdditionalDocument", lnkUDelay);
            }
            if (dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString() != "")
            {
                FileVisibilty(D280, hypCleanApproveAuthority, lnkDCleanApproveAuthority, flCleanApproveAuthority, dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString(), "AdditionalDocument", lnkUCleanApproveAuthority);
            }
        }
        #endregion

    }
    public void DivVisibilty(Label Lbl, System.Web.UI.HtmlControls.HtmlGenericControl divvsb)
    {
        if (Lbl.Text.Trim() == "")
            divvsb.Visible = false;
        else
            divvsb.Visible = true;
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

    #region Industry Details Bind

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
        string strret = "";
        try
        {
            if (filename != "")
            {
                strret = "../incentives/Files/IndustryUnit/" + filename;
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


    #region Fill Additional Document
    public void FillAdditionalDoc(DataTable dtAdditionalDoc)
    {
        try
        {
            if (dtAdditionalDoc.Rows.Count > 0)
            {

                D275.Value = dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
                D274.Value = dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
                D280.Value = dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();
                if (dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString() != "")
                {
                    FileVisibilty(D275, hypValidStatutary, lnkDValidStatutary, flValidStatutary, dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString(), "AdditionalDocument", lnkUValidStatutary);
                }
                if (dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString() != "")
                {
                    FileVisibilty(D274, hypDelay, lnkDDelay, flDelay, dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString(), "AdditionalDocument", lnkUDelay);
                }
                if (dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString() != "")
                {
                    FileVisibilty(D280, hypCleanApproveAuthority, lnkDCleanApproveAuthority, flCleanApproveAuthority, dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString(), "AdditionalDocument", lnkUCleanApproveAuthority);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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
                txtCostofProject.Text = dtLandInfo.Rows[0]["VCHCOSTOFPROJECT"].ToString();
                txtLandRequiredAsperRpt.Text = dtLandInfo.Rows[0]["VCHLANDAREAPERPROJECT"].ToString();
                txtLandRequired.Text = dtLandInfo.Rows[0]["VCHLANDAREA"].ToString();
                if (dtLandInfo.Rows[0]["VCHLANDDOCUMENT"].ToString() != "")
                {
                    lnkLandfileview.NavigateUrl = "../incentives/Files/LandDocument/" + dtLandInfo.Rows[0]["VCHLANDDOCUMENT"].ToString();
                    //LnkLandDocument.Text = dtLandInfo.Rows[0]["VCHLANDDOCUMENT"].ToString();
                }
                if (dtLandInfo.Rows[0]["VCHLANDUNDERTAKINGDOC"].ToString() != "")
                {
                    hypLandUndertaking.NavigateUrl = "../incentives/Files/LandDocument/" + dtLandInfo.Rows[0]["VCHLANDUNDERTAKINGDOC"].ToString();
                    //LnkLandDocument.Text = dtLandInfo.Rows[0]["VCHLANDDOCUMENT"].ToString();
                }
            }
            if (dtLandDetails.Rows.Count > 0)
            {
                grvLandInfo.DataSource = dtLandDetails;
                grvLandInfo.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
    #endregion

    #region File Upload Control

    protected void lnkUpload_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkLandfileupload.ID))
            {
                if (fldLandDocument.HasFile)
                {
                    string strFileName = "LandDoc" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fldLandDocument, hdnFileUpload, strFileName, lnkLandfileview, lblLandDocument, lnkLandDocumentDelete, "../incentives/Files/LandDocument/", "pdf/zip", lnkLandfileupload);
                }
            }
            else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILE.ID))
            {
                if (FlupAUTHORIZEDFILE.HasFile)
                {
                    string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                    UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, "../incentives/Files/InctBasicDoc/", "pdf/zip", lnkAUTHORIZEDFILE);
                }
            }
            else if (string.Equals(lnk.ID, lnkUpLandUndertaking.ID))
            {
                if (fldLandUndertaking.HasFile)
                {
                    string strFileName = "LandUndertaking" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fldLandUndertaking, hdnLandUndertaking, strFileName, hypLandUndertaking, lblLandUndertaking, lnkDelLandUndertaking, "../incentives/Files/LandDocument/", "pdf/zip", lnkUpLandUndertaking);
                }
            }
        }
        catch (Exception)
        {
        }
    }
    private void UploadDocument(FileUpload fuOrgDocument, HiddenField hdnOrgDocument, string strFileName, HyperLink hypOrdDocument, Label lblOrgDocument, LinkButton lnkOrgDocumentDelete, string FolderPath, string Extention, LinkButton lnkUPDocument = null)
    {
        string strMainFolderPath = Server.MapPath(FolderPath);
        if (!Directory.Exists(strMainFolderPath))
        {
            Directory.CreateDirectory(strMainFolderPath);
        }
        if (fuOrgDocument.HasFile)
        {
            if (!(IsFileValid(fuOrgDocument)))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots.', '" + MsgTitle + "'); </script>", false);
                return;
            }
            string filename = string.Empty;
            if (Extention == "excel")
            {
                if ((Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".xls") && (Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".xlsx"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .xls/.xlsx file Only!','" + MsgTitle + "')", true);
                    return;
                }
                int fileSize = fuOrgDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + MsgTitle + "')", true);
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .pdf file Only!','" + MsgTitle + "')", true);
                    return;
                }
                int fileSize = fuOrgDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + MsgTitle + "')", true);
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .pdf/zip file Only!','" + MsgTitle + "')", true);
                    return;
                }
                int fileSize = fuOrgDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('File size is too large. Maximum file size permitted is 4 MB','" + MsgTitle + "')", true);
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
            lnkUPDocument.Visible = false;
        }
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkLandDocumentDelete.ID))
            {
                UpdFileRemove(hdnFileUpload, lnkLandfileupload, lnkLandDocumentDelete, lnkLandfileview, lblLandDocument, fldLandDocument, "../incentives/Files/LandDocument/");
            }
            else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILEDdelete.ID))
            {
                UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, "../incentives/Files/InctBasicDoc/");
            }
            else if (string.Equals(lnk.ID, lnkDelLandUndertaking.ID))
            {
                UpdFileRemove(hdnLandUndertaking, lnkUpLandUndertaking, lnkDelLandUndertaking, hypLandUndertaking, lblLandUndertaking, fldLandUndertaking, "../incentives/Files/LandDocument/");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkUPDocument, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string FolderPath)
    {
        try
        {
            string filename = hdnFile.Value;
            string completePath = Server.MapPath(FolderPath + filename);

            //File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkUPDocument.Visible = true;
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
            string[] allowedImageTyps = { "application/pdf", "application/x-zip-compressed", "application/msxls" };
            string[] allowedExtension = { ".pdf", ".zip", ".xls", ".xlsx", ".ods" };
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
    public void FileUploadControls()
    {
        List<lstFileUpload> fileList = new List<lstFileUpload>();
        //industry
        if (radApplyBy.SelectedValue == "2")
        {
            if (hdnAUTHORIZEDFILE.Value != "")
            {
                fileList.Add(new lstFileUpload()
                {
                    id = 1,
                    vchDocId = hidAuthorizing.Value,
                    vchFileName = hdnAUTHORIZEDFILE.Value,
                    vchFilePath = "../incentives/Files/InctBasicDoc/"

                });
            }
        }
        //Land
        if (hdnFileUpload.Value != "")
        {
            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = "D124",
                vchFileName = hdnFileUpload.Value,
                vchFilePath = "../incentives/Files/LandDocument/"

            });
        }
        if (hdnLandUndertaking.Value != "")
        {
            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = "D281",
                vchFileName = hdnLandUndertakingID.Value,
                vchFilePath = "../incentives/Files/LandDocument/"

            });
        }
        //Additional doc
        if (D275.Value != "")
        {
            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = D275.ID,
                vchFileName = D275.Value,
                vchFilePath = "../incentives/Files/AdditionalDocument/"

            });
        }
        if (D274.Value != "")
        {
            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = D274.ID,
                vchFileName = D274.Value,
                vchFilePath = "../incentives/Files/AdditionalDocument/"

            });
        }
        if (D280.Value != "")
        {
            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = D280.ID,
                vchFileName = D280.Value,
                vchFilePath = "../incentives/Files/AdditionalDocument/"

            });
        }

        objincUnit.FileUploadDetails = fileList;

    }
    public void FileVisibilty(HiddenField HdnFileName, HyperLink HypView, LinkButton LnkDeleteBtn, FileUpload FluCtrl, string FileName, string FolderName, LinkButton lnkBtnUpload = null)
    {
        try
        {
            if (FileName != "")
            {
                HdnFileName.Value = FileName;
                HypView.NavigateUrl = "../incentives/Files/" + FolderName + "/" + FileName;
                HypView.Visible = true;
                LnkDeleteBtn.Visible = true;
                FluCtrl.Enabled = false;
                lnkBtnUpload.Visible = false;
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #endregion

    #region Bind Document From Service

    ///// Added by Sushant Jena on Dated 03-Jan-2018
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
}