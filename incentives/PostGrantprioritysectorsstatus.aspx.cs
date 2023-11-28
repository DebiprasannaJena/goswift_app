using System.IO;
using System;
using System.Data;
using System.Web.UI;
using EntityLayer.Incentive;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

public partial class incentives_PostGrantprioritysectorsstatus : SessionCheck
{
    #region Member Functions
    //string gFilePath = "../incentives/Files";

    Incentive objIncentive = new Incentive();
    int intUniqueId;//---------session value stored
    int intRetMsg;
    string eligbilemsg = "";
    List<AvailedEarlierDetails> ListAvailedEarlierDetails = new List<AvailedEarlierDetails>();
    string strmode = "";

    void FillActivities()
    {
        try
        {
            PrioritySectorDetails PrioritySector = new PrioritySectorDetails();
            if (hdnsectorid.Value.Trim() == "")
            {
                PrioritySector.intSectorId = 0;
            }
            else
            {
                PrioritySector.intSectorId = Convert.ToInt16(hdnsectorid.Value);
            }
            if (hdnsubsectorid.Value.Trim() == "")
            {
                PrioritySector.intSubSectorId = 0;
            }
            else
            {
                PrioritySector.intSubSectorId = Convert.ToInt16(hdnsubsectorid.Value);
            }

            DataSet dtActs = IncentiveManager.FillAllActivities(PrioritySector);
            if (dtActs.Tables[0].Rows[0]["bitsegment"].ToString().Trim() == "1")
            {
                divddl.Visible = true;

                ddlActivities.DataTextField = "vchActivityName";
                ddlActivities.DataValueField = "intActivityId";
                ddlActivities.DataSource = dtActs.Tables[1].DefaultView;
                ddlActivities.DataBind();
                ddlActivities.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            else
            {
                divddl.Visible = false;
                ddlSpecificActivity_old.DataTextField = "vchActivityName";
                ddlSpecificActivity_old.DataValueField = "intActivityId";
                ddlSpecificActivity_old.DataSource = dtActs.Tables[1];
                ddlSpecificActivity_old.DataBind();

            }


            foreach (ListItem _listItem in this.ddlSpecificActivity_old.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);

            }
            foreach (ListItem _listItem in this.ddlSpecificActivity.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    string ChkPriorityApp(int intInvestorId)
    {


        string retval = "";
        try
        {
            retval = IncentiveManager.IsPriorityApp(intInvestorId, 2);
            eligbilemsg = retval;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return retval;


    }
    private void documentsindt(string strdocid, string filname, string foldername)
    {
        DataTable dtdocument = new DataTable();
        dtdocument = (DataTable)ViewState["dtdocument"];
        DataRow drdoc = dtdocument.NewRow();
        drdoc["vchDocId"] = strdocid;
        drdoc["vchFileName"] = filname;
        drdoc["vchFilePath"] = "../incentives/" + foldername + "/";
        dtdocument.Rows.Add(drdoc);
        ViewState["dtdocument"] = dtdocument;

    }
    string ChkProvisionalCertAvail(int intInvestorId, string strIncentiveNum)
    {
        string retval = "";
        try
        {
            retval = IncentiveManager.IsProvisionalCertificate(intInvestorId, strIncentiveNum);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return retval;

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


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        #region "Priority Document upload facility"

        if (Request.QueryString["IncentiveNo"].IndexOf("~") > 0)
        {
            strmode = Request.QueryString["IncentiveNo"].Split('~')[1].ToString();
            hdfmode.Value = strmode;
        }
        else
        {
            strmode = "";
            hdfmode.Value = "0";
        }

        #endregion

        txtTimeschedulefrom.Attributes.Add("readonly", "readonly");
        txtTimescheduleTo.Attributes.Add("readonly", "readonly");
        txtDLSWCADateOfApproval.Attributes.Add("readonly", "readonly");


        if (ChkPriorityApp(Convert.ToInt16(Session["InvestorId"])) != "1")
        {
            lblelgmsg.Text = eligbilemsg;
            pnldet.Visible = true;
        }
        if (ChkProvisionalCertAvail(Convert.ToInt16(Session["InvestorId"]), Convert.ToString(Request.QueryString["IncentiveNo"].Split('~')[0].ToString())) != "0")
        {
            lblprop.Visible = false;
            hypProposalPriority.Visible = true;
            hypProposalPriority.NavigateUrl = "../Portal/Incentive/Sanctionorder/" + ChkProvisionalCertAvail(Convert.ToInt16(Session["InvestorId"]), Convert.ToString(Request.QueryString["IncentiveNo"].Split('~')[0].ToString()));
        }
        else
        {
            lblprop.Visible = true;
            hypProposalPriority.Visible = false;
        }


        foreach (ListItem _listItem in this.ddlSpecificActivity_old.Items)
        {

            _listItem.Attributes.Add("title", _listItem.Text);

        }
        foreach (ListItem _listItem in this.ddlSpecificActivity.Items)
        {

            _listItem.Attributes.Add("title", _listItem.Text);

        }

        GetMasterdetails();

        if (!IsPostBack)
        {

            foreach (ListItem _listItem in this.ddlSpecificActivity_old.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);

            }
            foreach (ListItem _listItem in this.ddlSpecificActivity.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);

            }
            fillSalutation();
            fillSector();
            crdtincentive();
            documenttable();

            if (Convert.ToString(Session["ApplySource"]) == "0")
            {
                PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
                FillActivities();
                FillFormDetails(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            }
            else
            {

                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
                FillActivities();


            }

        }
    }
    public void FillPrepopulate()
    {
        DataSet ds = new DataSet();
        Pre_Populate_Incentive_Entity objent = new Pre_Populate_Incentive_Entity();
        objent.strUserID = Convert.ToString(Session["InvestorId"]);
        Pre_Populate_Incentive_DAL objprepoulate = new Pre_Populate_Incentive_DAL();
        ds = objprepoulate.Pre_Populate_Inct_Data(objent);
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

    #region Fill Form Details
    public void FillFormDetails(int id)
    {

        try
        {

            Incentive objEntity = new Incentive();
            GetAndViewParam objPar = new GetAndViewParam();
            objEntity.strcActioncode = "19";
            objPar.Param1ID = "";
            objPar.Param2ID = "";
            objPar.Param3ID = "";
            objPar.InctType = 4;
            objEntity.UnqIncentiveId = id;
            objEntity.GetVwPrmtrs = objPar;
            objEntity.FormType = FormNumber.GrantprioritySector_19;
            DataSet dslive = new DataSet();
            dslive = IncentiveManager.GetGrantPriority(objEntity);


            DataTable dtstatus = dslive.Tables[5];
            string draftStatus = dslive.Tables[8].Rows[0]["status"].ToString();//////---store draft satus


            #region Fill Priority Sector Details
            DataTable dtPrioritySector = dslive.Tables[0];
            FillPrioritySector(dtPrioritySector);
            #endregion

            #region Fill Additional Document
            DataTable dtAdditionalDoc = dslive.Tables[1];
            DataTable dtAdditionalDocDtl = dslive.Tables[2];
            FillAdditionalDoc(dtAdditionalDoc, dtAdditionalDocDtl);
            #endregion

            #region Fill DLSWCA / SLSWCA / HLCA Apporval Details
            DataTable dtDLSWCA = dslive.Tables[3];
            FillDLSWCADetails(dtDLSWCA);
            #endregion

            #region "Availed Earlier"

            DataTable dtaviledfromdb = dslive.Tables[4];
            DataTable grdavileddt = (DataTable)ViewState["dtincentive"];
            if (dtaviledfromdb.Rows.Count > 0)
            {
                foreach (DataRow dr in dtaviledfromdb.Rows)
                {
                    DataRow dr1 = grdavileddt.NewRow();

                    dr1["intIncentiveType"] = dr["intIncentiveTypeID"].ToString().Trim();
                    dr1["vchIncentiveType"] = dr["vchIncentiveTypeID"].ToString().Trim();
                    dr1["decQuantum"] = dr["dcmQuantumValue"].ToString().Trim();
                    dr1["dtmFrom"] = dr["dtmPeriodFrom"].ToString().Trim();
                    dr1["dtmto"] = dr["dtmPeriodTo"].ToString().Trim();
                    dr1["intIPRApplicability"] = dr["intIPRApplicabilityID"].ToString().Trim();
                    dr1["vchIPRApplicability"] = dr["vchIPRApplicabilityID"].ToString().Trim();

                    grdavileddt.Rows.Add(dr1);
                }

                ViewState["dtincentive"] = grdavileddt;
                grdAlreadyAvailed.DataSource = grdavileddt;
                grdAlreadyAvailed.DataBind();
            }



            #endregion
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #endregion
    #region Fill Additional Document

    public void FillAdditionalDoc(DataTable dtAdditionalDoc, DataTable dtAdditionalDocDtl)
    {
        try
        {
            if (dtAdditionalDoc.Rows.Count > 0)
            {

                D275.Value = dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();
                D274.Value = dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
                D280.Value = dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();

                if (dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString() != "")
                {


                    flValidStatutary.Enabled = false;
                    hypValidStatutary.NavigateUrl = "../incentives/Files/AdditionalDocument/" + dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();
                    hypValidStatutary.Visible = true;
                    lnkDValidStatutary.Visible = true;

                }
                if (dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString() != "")
                {


                    flDelay.Enabled = false;
                    hypDelay.NavigateUrl = "../incentives/Files/AdditionalDocument/" + dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
                    hypDelay.Visible = true;
                    lnkDDelay.Visible = true;

                }
                if (dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString() != "")
                {


                    flCleanApproveAuthority.Enabled = false;
                    hypCleanApproveAuthority.NavigateUrl = "../incentives/Files/AdditionalDocument/" + dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
                    hypCleanApproveAuthority.Visible = true;
                    lnkDCleanApproveAuthority.Visible = true;

                }


            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }


    }
    #endregion
    #region Fill Priority Sector Details
    public void FillPrioritySector(DataTable dtPrioritySector)
    {
        try
        {
            if (dtPrioritySector.Rows.Count > 0)
            {
                ddl_Sector.SelectedValue = dtPrioritySector.Rows[0]["INTSECTORID"].ToString();
                fillSubSector(dtPrioritySector.Rows[0]["INTSECTORID"].ToString());
                ddl_Sub_Sector.SelectedValue = dtPrioritySector.Rows[0]["INTSUBSECTORID"].ToString();
                lbl_DerivedSector.Text = dtPrioritySector.Rows[0]["VCHDERIVESECTOR"].ToString();

                if (dtPrioritySector.Rows[0]["VCHDERIVESECTOR"].ToString() == "1")
                {
                    ChkBx_Priority.Checked = true;
                }




                if ((dtPrioritySector.Rows[0]["VCHACTIVITY"].ToString().Trim() != ""))
                {
                    string[] srvalues = dtPrioritySector.Rows[0]["VCHACTIVITY"].ToString().Trim().Split(',');
                    string[] srnames = dtPrioritySector.Rows[0]["actname"].ToString().Trim().Split('~');

                    for (int j = 0; j < srvalues.Length; j++)
                    {
                        if (srvalues[j].ToString().Trim() != "")
                        {
                            ListItem li = new ListItem();
                            li.Value = srvalues[j].ToString();
                            li.Text = srnames[j].ToString();
                            ddlSpecificActivity.Items.Add(li);
                        }

                    }
                }



                if (dtPrioritySector.Rows[0]["vchpresentnotestage"].ToString() != "")
                {
                    HiddenField3.Value = dtPrioritySector.Rows[0]["vchpresentnotestage"].ToString();
                    HyperLink2.NavigateUrl = "../incentives/Files/PrioritySectorDetails/" + dtPrioritySector.Rows[0]["vchpresentnotestage"].ToString();
                    documentsindt(HiddenField4.Value, dtPrioritySector.Rows[0]["vchpresentnotestage"].ToString(), "/Files/PrioritySectorDetails");

                    FileUpload2.Enabled = false;
                    HyperLink2.Visible = true;
                    LinkButton4.Visible = true;
                }


                if (dtPrioritySector.Rows[0]["VCHSECTORCERT"].ToString() != "")
                {

                    hdnUploadCertificate.Value = dtPrioritySector.Rows[0]["VCHSECTORCERT"].ToString();
                    hlkViewCertificate.NavigateUrl = "../incentives/Files/PrioritySectorDetails/" + dtPrioritySector.Rows[0]["VCHSECTORCERT"].ToString();
                    documentsindt(hdnCertificateDocId.Value, dtPrioritySector.Rows[0]["VCHSECTORCERT"].ToString(), "/Files/PrioritySectorDetails");

                    fldUploadCertificate.Enabled = false;
                    hlkViewCertificate.Visible = true;
                    lknDelCertificate.Visible = true;

                }
                if (dtPrioritySector.Rows[0]["VCHACKNOW"].ToString() != "")
                {
                    hdnUploadApplAcknow.Value = dtPrioritySector.Rows[0]["VCHACKNOW"].ToString();
                    hlkViewApplAcknow.NavigateUrl = "../incentives/Files/PrioritySectorDetails/" + dtPrioritySector.Rows[0]["VCHACKNOW"].ToString();
                    documentsindt(hdnApplAcknowDocId.Value, dtPrioritySector.Rows[0]["VCHACKNOW"].ToString(), "/Files/PrioritySectorDetails");

                    fldUploadApplAcknow.Enabled = false;
                    lknDelApplAcknow.Visible = true;
                    hlkViewApplAcknow.Visible = true;
                }

                if (dtPrioritySector.Rows[0]["vchsupportcertificatedoc"].ToString() != "")
                {
                    HiddenField1.Value = dtPrioritySector.Rows[0]["vchsupportcertificatedoc"].ToString();
                    HyperLink1.NavigateUrl = "../incentives/Files/PrioritySectorDetails/" + dtPrioritySector.Rows[0]["VCHACKNOW"].ToString();
                    documentsindt(HiddenField2.Value, dtPrioritySector.Rows[0]["vchsupportcertificatedoc"].ToString(), "/Files/PrioritySectorDetails");

                    FileUpload1.Enabled = false;
                    LinkButton2.Visible = true;
                    HyperLink1.Visible = true;
                }


            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #endregion
    #region  FIll DLSWCA / SLSWCA / HLCA Apporval Details
    public void FillDLSWCADetails(DataTable dtDLSWCA)
    {
        try
        {

            if (dtDLSWCA.Rows.Count > 0)
            {
                if (dtDLSWCA.Rows[0]["DTMAPPROVALDATE"].ToString() != "")
                {
                    txtDLSWCADateOfApproval.Text = dtDLSWCA.Rows[0]["DTMAPPROVALDATE"].ToString();
                }
                if (dtDLSWCA.Rows[0]["DCMLANDAPPROVED"].ToString() != "")
                {
                    txtDLSWCALandApproved.Text = dtDLSWCA.Rows[0]["DCMLANDAPPROVED"].ToString();
                }
                if (dtDLSWCA.Rows[0]["DCMCOSTOFLAND"].ToString() != "")
                {
                    txtDLSWCALandCost.Text = dtDLSWCA.Rows[0]["DCMCOSTOFLAND"].ToString();
                }
                if (dtDLSWCA.Rows[0]["DCMAMOUNTELIGIBLE"].ToString() != "")
                {
                    txtDLSWCASubsidyAmt.Text = dtDLSWCA.Rows[0]["DCMAMOUNTELIGIBLE"].ToString();
                }

                if (dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString() != "")
                {

                    hdnDLSWCAApprovalDoc.Value = dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString();
                    fupDLSWCAApprovalDocUpload.Enabled = false;
                    lnkDLSWCAApprovalDocView.NavigateUrl = "../incentives/Files/DLSWCA/" + dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString();
                    documentsindt(hdnDLSWCAApprovalDocId.Value, dtDLSWCA.Rows[0]["VCHAPPROVALDOC"].ToString(), "/Files/DLSWCA");
                    lnkDLSWCAApprovalDocView.Visible = true;
                    lnkDelDLSWCAApprovalDoc.Visible = true;

                }
                if (dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString() != "")
                {
                    hdnDLSWCASubstanDoc.Value = dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString();
                    fupDLSWCASubstanDocUpload.Enabled = false;
                    lnkDLSWCASubstanDocView.NavigateUrl = "../incentives/Files/DLSWCA/" + dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString();
                    documentsindt(hdnDLSWCASubstanDocId.Value, dtDLSWCA.Rows[0]["VCHSUBSTANTIATEDOC"].ToString(), "/Files/DLSWCA");
                    lnkDLSWCASubstanDocView.Visible = true;
                    lnkDelDLSWCASubstanDoc.Visible = true;
                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #endregion

    protected void lnkDocumentUpload_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;

            if (string.Equals(lnk.ID, lnkAUTHORIZEDFILE.ID))
            {
                if (FlupAUTHORIZEDFILE.HasFile)
                {
                    string extension = Path.GetExtension(FlupAUTHORIZEDFILE.PostedFile.FileName);
                    string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                    string strFolderName = "InctBasicDoc";
                    UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, strFolderName);
                    //documentsindt(hdnexemption.Value, strFileName,"IndustryUnit");
                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    protected void lnkDocumentDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkAUTHORIZEDFILEDdelete.ID))
            {
                string strFolderName = "InctBasicDoc";
                UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, strFolderName);

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void LNBUploadPrior_Click(object sender, EventArgs e)
    {

        try
        {

            string strMainFolderPath = Server.MapPath("~/Portal/Incentive/Sanctionorder/");
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (FUPPriorCert.HasFile)
            {
                string filename = string.Empty;


                if (IsFileValid(FUPPriorCert) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                    return;
                }

                if ((Path.GetExtension(FUPPriorCert.FileName) != ".pdf") && (Path.GetExtension(FUPPriorCert.FileName) != ".zip"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,ZIP file Only!')", true);
                    return;
                }
                int fileSize = FUPPriorCert.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return;
                }
                else
                {
                    filename = "SanctionOrder_" + DateTime.Now.ToString("_ddMMyyhhmmss") + Path.GetExtension(FUPPriorCert.FileName);
                }
                FUPPriorCert.SaveAs(strMainFolderPath + filename);
                HDNPriorCertdoc.Value = filename;
                HypviewPrior.NavigateUrl = string.Format("~/Portal/Incentive/Sanctionorder/{0}", filename);
                HypviewPrior.Visible = true;
                LNBDeletePrior.Visible = true;
                LBLPriorCert.Visible = true;
                FUPPriorCert.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    protected void LNBDeletePrior_Click(object sender, EventArgs e)
    {
        try
        {
            string filename = HDNPriorCertdoc.Value;
            string path = string.Format("~/Portal/Incentive/Sanctionorder/{0}", filename);
            string completePath = Server.MapPath(path);
            if (File.Exists(completePath))
            {
                File.Delete(completePath);
                HDNPriorCertdoc.Value = "";
                LNBDeletePrior.Visible = false;
                LNBUploadPrior.Visible = true;
                HypviewPrior.Visible = false;
                LBLPriorCert.Visible = false;
                FUPPriorCert.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }



    protected void btnEdit_Click(object sender, EventArgs e)
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
            GrantData();
            string vchSignFileName = "";
            //--- Offline checking for signature Upload
            if (hdfmode.Value.Trim() == "2")
            {
                string[] fileext = { ".png", ".jpg", ".jpeg" };
                if (SignFiles.HasFile)
                {
                    if (fileext.Contains(System.IO.Path.GetExtension(SignFiles.FileName).ToLower()))
                    {
                        bool folderExists = Directory.Exists(Server.MapPath("../incentives/Files/Signature/"));
                        if (!folderExists)
                            Directory.CreateDirectory(Server.MapPath("../incentives/Files/Signature/"));

                        string extension = Path.GetExtension(SignFiles.PostedFile.FileName);
                        vchSignFileName = "Signature" + System.DateTime.Now.ToString("_ddMMyyhhmmss") + extension;
                        SignFiles.SaveAs(Server.MapPath("../incentives/Files/Signature/") + vchSignFileName);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please upload .png/.jpg/.jpeg format image only !', 'SWP'); </script>", false);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please Upload Signature. !', 'SWP'); </script>", false);
                    return;
                }
            }


            if (hdnIsOsPCBDownloaded.Value == "1")
            {
                SaveServiceDoc(enServiceDocType.OSPCB, D275, "AdditionalDocument");
            }
            if (hdnBoilderDownloaded.Value == "1")
            {
                SaveServiceDoc(enServiceDocType.Boiler, D280, "AdditionalDocument");
            }
            INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();

            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            objINDUSTRIAL_UNIT_MASTER.AADHAARNO_IND = TxtAdhaar1.Text;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILECODE_IND = hidAuthorizing.Value;
            objINDUSTRIAL_UNIT_MASTER.Int_Mode = hdfmode.Value;
            objINDUSTRIAL_UNIT_MASTER.Vch_Priorityfile = HDNPriorCertdoc.Value;
            objINDUSTRIAL_UNIT_MASTER.vch_signfile = vchSignFileName;
            objIncentive.IndsutUnitMstDet = objINDUSTRIAL_UNIT_MASTER;




            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objIncentive.UnqIncentiveId = 0;
            }
            else
            {
                objIncentive.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }
            objIncentive.strcActioncode = "A";
            objIncentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"].Split('~')[0].ToString());
            objIncentive.PealNum = Convert.ToString(Session["ProposalNo"]);
            objIncentive.PCNum = Convert.ToString(Session["PCNo"]);
            objIncentive.UnitCode = Convert.ToString(Session["UnitCode"]);
            objIncentive.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objIncentive.Userid = Convert.ToInt16(Session["InvestorId"]);
            objIncentive.Createdby = Convert.ToInt16(Session["InvestorId"]);
            objIncentive.ApprovalAction = "A";
            objIncentive.incentivetype = 4;



            #region "All Uploaded documents"

            DataTable dtdocument = new DataTable();
            dtdocument = (DataTable)ViewState["dtdocument"];
            if (dtdocument.Rows.Count > 0)
            {
                List<lstFileUpload> listItmProp = new List<lstFileUpload>();
                listItmProp = dtdocument.AsEnumerable().Select(m => new lstFileUpload()
                {
                    id = m.Field<int>("id"),
                    vchDocId = m.Field<string>("vchDocId"),
                    vchFileName = m.Field<string>("vchFileName"),
                    vchFilePath = m.Field<string>("vchFilePath")

                }).ToList();


                objIncentive.FileUploadDetails = listItmProp;
            }


            #endregion



            objIncentive.FormType = FormNumber.GrantprioritySector_19;
            //objIncentive.FileUploadDetails = getFileUploadDatatable();

            string retval = IncentiveManager.CreateGrantPriority(objIncentive);
            int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
            ///------------------------------------------------------------------------------------------------
            if (hdfmode.Value.Trim() != "2")
            {
                Response.Redirect("FormPreview_PostGPSS.aspx?InctUniqueNo=" + Convert.ToString(mstyp));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + Messages.ShowMessage("1") + "');</script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally { }

    }

    protected void Button1_Click(object sender, EventArgs e)
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
            GrantData();

            INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();

            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            objINDUSTRIAL_UNIT_MASTER.AADHAARNO_IND = TxtAdhaar1.Text;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objINDUSTRIAL_UNIT_MASTER.AUTHORIZEDFILECODE_IND = hidAuthorizing.Value;
            objIncentive.IndsutUnitMstDet = objINDUSTRIAL_UNIT_MASTER;

            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objIncentive.UnqIncentiveId = 0;
            }
            else
            {
                objIncentive.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }
            objIncentive.strcActioncode = "A";
            objIncentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"].Split('~')[0].ToString());
            objIncentive.PealNum = Convert.ToString(Session["ProposalNo"]);
            objIncentive.PCNum = Convert.ToString(Session["PCNo"]);
            objIncentive.UnitCode = Convert.ToString(Session["UnitCode"]);
            objIncentive.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objIncentive.Userid = Convert.ToInt16(Session["InvestorId"]);
            objIncentive.Createdby = Convert.ToInt16(Session["InvestorId"]);
            objIncentive.ApprovalAction = "A";
            objIncentive.incentivetype = 4;

            #region "All Uploaded documents"

            DataTable dtdocument = new DataTable();
            dtdocument = (DataTable)ViewState["dtdocument"];
            if (dtdocument.Rows.Count > 0)
            {
                List<lstFileUpload> listItmProp = new List<lstFileUpload>();
                listItmProp = dtdocument.AsEnumerable().Select(m => new lstFileUpload()
                {
                    id = m.Field<int>("id"),
                    vchDocId = m.Field<string>("vchDocId"),
                    vchFileName = m.Field<string>("vchFileName"),
                    vchFilePath = m.Field<string>("vchFilePath")

                }).ToList();


                objIncentive.FileUploadDetails = listItmProp;
            }


            #endregion

            objIncentive.FormType = FormNumber.GrantprioritySector_19;
            //objIncentive.FileUploadDetails = getFileUploadDatatable();

            string retval = IncentiveManager.CreateGrantPriority(objIncentive);
            if (retval.Split('~')[0].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !!</strong>', 'SWP'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally { }
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

        DataColumn intIncentiveType = new DataColumn("intIncentiveType");
        intIncentiveType.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(intIncentiveType);

        DataColumn vchIncentiveType = new DataColumn("vchIncentiveType");
        vchIncentiveType.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchIncentiveType);

        DataColumn decQuantum = new DataColumn("decQuantum");
        decQuantum.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(decQuantum);

        DataColumn dtmFrom = new DataColumn("dtmFrom");
        dtmFrom.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(dtmFrom);

        DataColumn dtmto = new DataColumn("dtmto");
        dtmto.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(dtmto);

        DataColumn intIPRApplicability = new DataColumn("intIPRApplicability");
        intIPRApplicability.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(intIPRApplicability);

        DataColumn vchIPRApplicability = new DataColumn("vchIPRApplicability");
        vchIPRApplicability.DataType = Type.GetType("System.String");
        dtincentive.Columns.Add(vchIPRApplicability);



        ViewState["dtincentive"] = dtincentive;
        grdAlreadyAvailed.DataSource = dtincentive;
        grdAlreadyAvailed.DataBind();
    }
    public void IncentiveavailedDetails()
    {
        try
        {
            DataTable dtincentive = new DataTable();
            dtincentive = (DataTable)ViewState["dtincentive"];
            DataRow dr = dtincentive.NewRow();
            dr["intIncentiveType"] = ddlIncentiveType.SelectedValue.ToString();
            dr["vchIncentiveType"] = ddlIncentiveType.SelectedItem.ToString();
            dr["decQuantum"] = txtQuantum.Text.Trim();
            dr["dtmFrom"] = txtTimeschedulefrom.Text.Trim();
            dr["dtmto"] = txtTimescheduleTo.Text.Trim();
            dr["intIPRApplicability"] = ddlApplicability.SelectedValue.ToString();
            dr["vchIPRApplicability"] = ddlApplicability.SelectedItem.ToString();
            dtincentive.Rows.Add(dr);
            ViewState["dtincentive"] = dtincentive;

            grdAlreadyAvailed.DataSource = dtincentive;
            grdAlreadyAvailed.DataBind();

            ddlIncentiveType.SelectedIndex = 0;
            txtQuantum.Text = "";
            txtTimeschedulefrom.Text = "";
            txtTimescheduleTo.Text = "";
            ddlApplicability.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void GrantData()
    {
        #region Priority sector Details
        PrioritySectorDetails();
        #endregion

        #region Additional Document
        AdditionalDocument();
        #endregion

        #region "DLSWCA / SLSWCA / HLCA Apporval Details"
        AddDLSWCA_SLSWCA();
        #endregion

        #region Already Availed Earlier
        AddAvailed_Earlier();
        #endregion
    }

    protected void AddAvailed_Earlier()
    {
        try
        {

            AvailedIncentiveEarlier objAvailed = new AvailedIncentiveEarlier();
            AvailedEarlierDetails objAvailDetails = new AvailedEarlierDetails();

            List<AvailedEarlierDetails> listItmProp = new List<AvailedEarlierDetails>();



            DataTable dt = new DataTable();
            if (ViewState["dtincentive"] != null)
            {
                dt = (DataTable)ViewState["dtincentive"];
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; dt.Rows.Count > i; i++)
                {
                    objAvailDetails = new AvailedEarlierDetails();
                    objAvailDetails.IncentiveTypeID = Convert.ToInt32(dt.Rows[i]["intIncentiveType"].ToString());
                    objAvailDetails.QuantumValue = Convert.ToDecimal(dt.Rows[i]["decQuantum"]);
                    objAvailDetails.AvailedEarlierFrom = Convert.ToDateTime(dt.Rows[i]["dtmFrom"]);
                    objAvailDetails.AvailedEarlierTo = Convert.ToDateTime(dt.Rows[i]["dtmto"]);
                    objAvailDetails.ApplicabilityId = Convert.ToInt32(dt.Rows[i]["intIPRApplicability"].ToString());

                    listItmProp.Add(objAvailDetails);
                }
            }
            objIncentive.AvailedEarlier = new AvailedIncentiveEarlier();
            objIncentive.AvailedEarlier.ListAvailedEarlierDetails = listItmProp;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {

        }
    }
    protected void AdditionalDocument()
    {
        objIncentive.AdditionalDocument = new EntityLayer.Incentive.AdditionalDocuments();
        objIncentive.AdditionalDocument.intStatutoryCleanOSPCB_NOC = 0;
        objIncentive.AdditionalDocument.intStatutoryCleanOSPCB_Consent = 0;
        objIncentive.AdditionalDocument.intStatutoryCleanCentralExec = 0;
        objIncentive.AdditionalDocument.intStatutoryCleanFSHGSCD = 0;
        objIncentive.AdditionalDocument.intStatutoryCleanExplosive_NOC = 0;
        objIncentive.AdditionalDocument.strCleanApproveAuthorityOSPCB = D275.Value; //OSPCB
        objIncentive.AdditionalDocument.strValidSatutoryGreenCategory = D280.Value; //Factory n Boiler
        objIncentive.AdditionalDocument.strCondoDocumentationDelay = D274.Value;    //Sector Relevant

    }
    public void AddDLSWCA_SLSWCA()
    {
        DLSWCAApprovalDtls objDLS = new DLSWCAApprovalDtls();
        objDLS.dtmApprovalDate = txtDLSWCADateOfApproval.Text == "" ? "1/1/1900" : txtDLSWCADateOfApproval.Text;

        if (txtDLSWCALandApproved.Text.Trim() == "")
        {
            objDLS.dcmLandRequired = 0;
        }
        else
        {
            objDLS.dcmLandRequired = Convert.ToDecimal(txtDLSWCALandApproved.Text);
        }
        objDLS.dcmCostOfLand = 0;
        objDLS.dcmSubsidyAmount = 0;
        objDLS.strDLSWCAApprovalDoc = hdnDLSWCAApprovalDoc.Value;
        objDLS.strsubstantitateDoc = hdnDLSWCASubstanDoc.Value;
        objIncentive.DLSWCAApprovalDet = objDLS;
    }
    protected void PrioritySectorDetails()
    {

        objIncentive.PrioritySector = new EntityLayer.Incentive.PrioritySectorDetails();

        objIncentive.PrioritySector.strDerivedSector = Lbl_Derived_Sector.Text.ToString();
        if (ChkBx_Priority.Checked)
        {
            objIncentive.PrioritySector.intLiesSector = 1;
        }
        else
        {
            objIncentive.PrioritySector.intLiesSector = 0;
        }

        objIncentive.PrioritySector.intAvailPriorityCertf = 1;
        objIncentive.PrioritySector.strPrioritycCertf2015 = hdnUploadCertificate.Value;
        objIncentive.PrioritySector.strAppcnAcknow = hdnUploadApplAcknow.Value;

        string strActs = "";
        foreach (ListItem li in ddlSpecificActivity.Items)
        {

            strActs = strActs + "," + li.Value.ToString();

        }
        objIncentive.PrioritySector.strSpecificActivity = strActs;
        objIncentive.PrioritySector.strNote = HiddenField3.Value.Trim();
        objIncentive.PrioritySector.intSpecificActivity = 0;
        objIncentive.PrioritySector.strSupportingDoc = HiddenField1.Value;

    }
    public string RetDateFrmDB(string srcDate)
    {
        return srcDate;
    }
    private void documenttable()
    {
        DataTable dtdocument = new DataTable();

        DataColumn id = new DataColumn("id");
        id.DataType = Type.GetType("System.Int32");
        id.AutoIncrement = true;
        id.AutoIncrementSeed = 1;
        id.AutoIncrementStep = 1;
        dtdocument.Columns.Add(id);

        DataColumn vchDocId = new DataColumn("vchDocId");
        vchDocId.DataType = Type.GetType("System.String");
        dtdocument.Columns.Add(vchDocId);

        DataColumn vchFileName = new DataColumn("vchFileName");
        vchFileName.DataType = Type.GetType("System.String");
        dtdocument.Columns.Add(vchFileName);

        DataColumn vchFilePath = new DataColumn("vchFilePath");
        vchFilePath.DataType = Type.GetType("System.String");
        dtdocument.Columns.Add(vchFilePath);

        ViewState["dtdocument"] = dtdocument;
    }

    protected void lnkOrgDocumentPdf_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lknAddCertificate.ID))
            {
                if (fldUploadCertificate.HasFile)
                {
                    string strFileName = "PrioritySec" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fldUploadCertificate, hdnUploadCertificate, strFileName, hlkViewCertificate, lblCertificate, lknDelCertificate, "PrioritySectorDetails");
                    documentsindt(hdnCertificateDocId.Value, strFileName, "/Files/PrioritySectorDetails");
                }
            }
            else if (string.Equals(lnk.ID, lknAddApplAcknow.ID))
            {
                if (fldUploadApplAcknow.HasFile)
                {
                    string strFileName = "PrioritySec" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fldUploadApplAcknow, hdnUploadApplAcknow, strFileName, hlkViewApplAcknow, lblApplAcknow, lknDelApplAcknow, "PrioritySectorDetails");
                    documentsindt(hdnApplAcknowDocId.Value, strFileName, "/Files/PrioritySectorDetails");
                }
            }
            else if (string.Equals(lnk.ID, LinkButton1.ID))
            {
                if (FileUpload1.HasFile)
                {
                    string strFileName = "PrioritySec" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(FileUpload1, HiddenField1, strFileName, HyperLink1, Label4, LinkButton2, "PrioritySectorDetails");
                    documentsindt(HiddenField2.Value, strFileName, "/Files/PrioritySectorDetails");
                }
            }
            else if (string.Equals(lnk.ID, LinkButton3.ID))
            {
                if (FileUpload2.HasFile)
                {
                    string strFileName = "PrioritySec" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(FileUpload2, HiddenField3, strFileName, HyperLink2, Label5, LinkButton4, "PrioritySectorDetails");
                    documentsindt(HiddenField4.Value, strFileName, "/Files/PrioritySectorDetails");
                }
            }
            else if (string.Equals(lnk.ID, lnkUValidStatutary.ID))
            {
                if (flValidStatutary.HasFile)
                {
                    string strFileName = "AddDocument" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(flValidStatutary, D275, strFileName, hypValidStatutary, lblValidStatutary, lnkDValidStatutary, "AdditionalDocument");
                    documentsindt("D275", strFileName, "/Files/AdditionalDocument");
                }
            }
            else if (string.Equals(lnk.ID, lnkUDelay.ID))
            {
                if (flDelay.HasFile)
                {
                    string strFileName = "AddDocument" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(flDelay, D274, strFileName, hypDelay, lblDelay, lnkDDelay, "AdditionalDocument");
                    documentsindt("D274", strFileName, "/Files/AdditionalDocument");
                }
            }
            else if (string.Equals(lnk.ID, lnkUCleanApproveAuthority.ID))
            {
                if (flCleanApproveAuthority.HasFile)
                {
                    string strFileName = "AddDocument" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(flCleanApproveAuthority, D280, strFileName, hypCleanApproveAuthority, lblCleanApproveAuthority, lnkDCleanApproveAuthority, "AdditionalDocument");
                    documentsindt("280", strFileName, "/Files/AdditionalDocument");
                }
            }
            else if (string.Equals(lnk.ID, lnkAddDLSWCAApprovalDoc.ID))
            {
                if (fupDLSWCAApprovalDocUpload.HasFile)
                {
                    string strFileName = "DLSWCA" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupDLSWCAApprovalDocUpload, hdnDLSWCAApprovalDoc, strFileName, lnkDLSWCAApprovalDocView, lblDLSWCAApprovalDoc, lnkDelDLSWCAApprovalDoc, "DLSWCA");
                    documentsindt(hdnDLSWCAApprovalDocId.Value, strFileName, "/Files/DLSWCA");
                }
            }
            else if (string.Equals(lnk.ID, lnkAddDLSWCASubstanDoc.ID))
            {
                if (fupDLSWCASubstanDocUpload.HasFile)
                {
                    string strFileName = "AddDocument" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fupDLSWCASubstanDocUpload, hdnDLSWCASubstanDoc, strFileName, lnkDLSWCASubstanDocView, lblDLSWCASubstanDoc, lnkDelDLSWCASubstanDoc, "DLSWCA");
                    documentsindt(hdnDLSWCASubstanDocId.Value, strFileName, "/Files/DLSWCA");
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void lnkOrgDocumentDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lknDelCertificate.ID))
            {
                UpdFileRemove(hdnUploadCertificate, lknAddCertificate, lknDelCertificate, hlkViewCertificate, lblCertificate, fldUploadCertificate, "PrioritySectorDetails");
            }
            else if (string.Equals(lnk.ID, lknDelApplAcknow.ID))
            {
                UpdFileRemove(hdnUploadApplAcknow, lknAddApplAcknow, lknDelApplAcknow, hlkViewApplAcknow, lblApplAcknow, fldUploadApplAcknow, "PrioritySectorDetails");
            }
            else if (string.Equals(lnk.ID, LinkButton2.ID))
            {
                UpdFileRemove(HiddenField1, LinkButton1, LinkButton2, HyperLink1, Label4, FileUpload1, "PrioritySectorDetails");
            }
            else if (string.Equals(lnk.ID, LinkButton4.ID))
            {
                UpdFileRemove(HiddenField3, LinkButton3, LinkButton4, HyperLink2, Label5, FileUpload2, "PrioritySectorDetails");
            }
            else if (string.Equals(lnk.ID, lnkDValidStatutary.ID))
            {
                UpdFileRemove(D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, flValidStatutary, "AdditionalDocument");
            }
            else if (string.Equals(lnk.ID, lnkDDelay.ID))
            {
                UpdFileRemove(D274, lnkUDelay, lnkDDelay, hypDelay, lblDelay, flDelay, "AdditionalDocument");
            }
            else if (string.Equals(lnk.ID, lnkDCleanApproveAuthority.ID))
            {
                UpdFileRemove(D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, flCleanApproveAuthority, "AdditionalDocument");
            }
            else if (string.Equals(lnk.ID, lnkDelDLSWCAApprovalDoc.ID))
            {
                UpdFileRemove(hdnDLSWCAApprovalDoc, lnkAddDLSWCAApprovalDoc, lnkDelDLSWCAApprovalDoc, lnkDLSWCAApprovalDocView, lblDLSWCAApprovalDoc, fupDLSWCAApprovalDocUpload, "DLSWCA");
            }
            else if (string.Equals(lnk.ID, lnkDelDLSWCASubstanDoc.ID))
            {
                UpdFileRemove(hdnDLSWCASubstanDoc, lnkAddDLSWCASubstanDoc, lnkDelDLSWCASubstanDoc, lnkDLSWCASubstanDocView, lblDLSWCASubstanDoc, fupDLSWCASubstanDocUpload, "DLSWCA");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string strFolername)
    {
        try
        {
            string filename = hdnFile.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolername, filename);
            string completePath = Server.MapPath(path);
            //if (File.Exists(completePath))
            //{
            //    File.Delete(completePath);
            //}
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void UploadDocument(FileUpload fuOrgDocument, HiddenField hdnOrgDocument, string strFileName, HyperLink hypOrdDocument, Label lblOrgDocument, LinkButton lnkOrgDocumentDelete, string strFoldername)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFoldername));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (fuOrgDocument.HasFile)
            {
                string filename = string.Empty;

                if (IsFileValid(fuOrgDocument) == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                    return;
                }

                if ((Path.GetExtension(fuOrgDocument.FileName) != ".pdf") && (Path.GetExtension(fuOrgDocument.FileName) != ".zip"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,ZIP file Only!')", true);
                    return;
                }

                int fileSize = fuOrgDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(fuOrgDocument.FileName);
                }

                fuOrgDocument.SaveAs(strMainFolderPath + filename);
                hdnOrgDocument.Value = filename;
                hypOrdDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFoldername, filename);
                hypOrdDocument.Visible = true;
                lnkOrgDocumentDelete.Visible = true;
                lblOrgDocument.Visible = true;
                fuOrgDocument.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void fillSector()
    {
        try
        {
            IncentiveMaster objIncentive = new IncentiveMaster();
            IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
            objIncentive.Action = "L";
            objLayer.BindDropdown(ddl_Sector, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
        }
    }
    private void fillSubSector(string StrSectorId)
    {
        try
        {
            IncentiveMaster objIncentive = new IncentiveMaster();
            IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
            objIncentive.Action = "sub";
            objIncentive.Param_2 = StrSectorId;
            objLayer.BindDropdown(ddl_Sub_Sector, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
        }
    }

    public void PrepopulateData(int id)
    {
        try
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
                else if (dtindustryPre.Rows[0]["intPriority"].ToString() == "3")
                {
                    lblIs_Priority.Text = "Provisional";
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
                    lblAfterEMD189.Text = "PC Issurance Date";
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
                hdnsectorid.Value = dtindustryPre.Rows[0]["intSectorId"].ToString();
                hdnsubsectorid.Value = dtindustryPre.Rows[0]["intSubSectorId"].ToString();
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
                Hyp_View_Term_Loan_Doc.NavigateUrl = dtMeansFinancePre.Rows[0]["vchTermLoanDoc"].ToString();
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

            #region Get Documents (OSPCB,Factory & Boiler) from Service

            /*-----------------------------------------------------------*/
            ///// Get Documents (OSPCB,Factory & Boiler) from Service

            BindDocFromService(enServiceDocType.OSPCB, D275, flValidStatutary, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, hdnIsOsPCBDownloaded);
            BindDocFromService(enServiceDocType.Boiler, D280, flCleanApproveAuthority, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, hdnBoilderDownloaded);

            #endregion
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }


    }
    public void PostpopulateData(int id)
    {
        try
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
                else if (dtindustryPre.Rows[0]["intPriority"].ToString() == "3")
                {
                    lblIs_Priority.Text = "Provisional";
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

                TxtApplicantName.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["vchCertOfRegdDocFileName"].ToString();

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
                    lblAfterEMD189.Text = "PC Issurance Date";
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
                hdnsectorid.Value = dtindustryPre.Rows[0]["intSectorId"].ToString();
                hdnsubsectorid.Value = dtindustryPre.Rows[0]["intSubSectorId"].ToString();

            }
            #endregion

            #region "Post Industrial Unit Populateddata"

            if (dtindustryPre.Rows.Count > 0)
            {
                DdlGender.SelectedValue = dtindustryPre.Rows[0]["INTGENDER"].ToString();
                TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHAPPLICANTNAME"].ToString();
                radApplyBy.SelectedValue = dtindustryPre.Rows[0]["INTAPPLYBY"].ToString();

                if (dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                {
                    TxtAdhaar1.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString();

                }

                hidAuthorizing.Value = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILECODE"].ToString();
                lblAuthorizing.Text = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILECODEName"].ToString();

                hdnAUTHORIZEDFILE.Value = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();

                if (hdnAUTHORIZEDFILE.Value.Trim() != "")
                {
                    hypAUTHORIZEDFILE.Visible = true;
                    lnkAUTHORIZEDFILEDdelete.Visible = true;
                    FlupAUTHORIZEDFILE.Enabled = false;
                    hypAUTHORIZEDFILE.NavigateUrl = string.Format("~/incentives/{0}/{1}", "/Files/InctBasicDoc", hdnAUTHORIZEDFILE.Value);
                }
                else
                {
                    hypAUTHORIZEDFILE.Visible = false;
                    lnkAUTHORIZEDFILEDdelete.Visible = false;
                    FlupAUTHORIZEDFILE.Enabled = true;
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


                //dtInvestmentPre.Rows[0]["vchProjectDocAfterCode"].ToString();			
                Lbl_Approved_DPR_After_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchProjectDocAfterCodeName"].ToString();
                Hyp_View_Approved_DPR_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPre.Rows[0]["vchProjectDocAfter"].ToString();
                //dtInvestmentPre.Rows[0]["intCreatedBy"].ToString();
            }
            #endregion

            #region MEANS OF FINANCE
            if (dtMeansFinancePre.Rows.Count > 0)
            {
                lbl_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();
                lbl_Loan_Bank_FI.Text = dtMeansFinancePre.Rows[0]["decLoanBankFI"].ToString();
                Hyp_View_Term_Loan_Doc.NavigateUrl = dtMeansFinancePre.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();
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
            #endregion
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }


    }

    protected void ddl_Sector_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillSubSector(ddl_Sector.SelectedValue.ToString());
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

        if (hdnUploadCertificate.Value != "")
        {
            DataRow dorgRowT = dtFiles.NewRow();
            dorgRowT["vchDocId"] = hdnCertificateDocId.Value;
            dorgRowT["vchFileName"] = hdnUploadCertificate.Value;
            dorgRowT["vchFilePath"] = "Files/PrioritySectorDetails/";
            dtFiles.Rows.Add(dorgRowT);
        }

        if (hdnUploadApplAcknow.Value != "")
        {
            DataRow dorgRowTR = dtFiles.NewRow();
            dorgRowTR["vchDocId"] = hdnApplAcknowDocId.Value;
            dorgRowTR["vchFileName"] = hdnUploadApplAcknow.Value;
            dorgRowTR["vchFilePath"] = "Files/PrioritySectorDetails/";
            dtFiles.Rows.Add(dorgRowTR);
        }


        if (D274.Value != "")
        {
            DataRow dorgRowSC = dtFiles.NewRow();
            dorgRowSC["vchDocId"] = "D274";
            dorgRowSC["vchFileName"] = D274.Value;
            dorgRowSC["vchFilePath"] = "Files/AdditionalDocument/";
            dtFiles.Rows.Add(dorgRowSC);
        }
        if (D275.Value != "")
        {
            DataRow dorgRowSC = dtFiles.NewRow();
            dorgRowSC["vchDocId"] = "D275";
            dorgRowSC["vchFileName"] = D275.Value;
            dorgRowSC["vchFilePath"] = "Files/AdditionalDocument/";
            dtFiles.Rows.Add(dorgRowSC);
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
    protected void lnkAddmore_Click(object sender, EventArgs e)
    {
        IncentiveavailedDetails();
    }
    private void GetMasterdetails()
    {
        try
        {
            Incentive objIncentive = new Incentive();
            objIncentive.strcActioncode = "M";
            objIncentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"].Split('~')[0].ToString());
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveMaster(objIncentive);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtbPostSubFlag = ds.Tables[0];
                lblTitle.Text = "Application For " + dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }


    private void UploadDocument(FileUpload fuDocument, HiddenField hdnDocument, string strFileName, HyperLink hypViewDocument, Label lblDocument, LinkButton lnkDocumentDelete, string strFolderName, LinkButton lnkBtnUpload = null, Image imgshow = null)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFolderName));//, Session["investorid"]
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

                int fileSize = fuDocument.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File size is too large. Maximum file size permitted is 4 MB !', ''); </script>", false);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(fuDocument.FileName);
                }

                fuDocument.SaveAs(strMainFolderPath + filename);
                hdnDocument.Value = filename;
                hypViewDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
                hypViewDocument.Visible = true;
                lnkDocumentDelete.Visible = true;
                lblDocument.Visible = true;
                fuDocument.Enabled = false;
                lnkBtnUpload.Visible = false;

                imgshow.Attributes.Add("src", "../images/incapproved.png");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtnUpload, LinkButton lnkDel, HyperLink hplnkView, Label lblFile, FileUpload updFile, string strFolderName, Image imgshow = null)
    {
        try
        {
            string filename = hdnFile.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
            string completePath = Server.MapPath(path);
            if (File.Exists(completePath))
            {
                ////File.Delete(completePath);
            }
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtnUpload.Visible = true;
            hplnkView.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
            imgshow.Attributes.Add("src", "../images/cancel-square.png");
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

                string strFileName = "STATUTORYCLEARANCE" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "AdditionalDocument";
                UploadDocument(flCleanApproveAuthority, D280, strFileName, hypCleanApproveAuthority, lblCleanApproveAuthority, lnkDCleanApproveAuthority, strFolderName, lnkUCleanApproveAuthority);
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
            string strFolderName = "AdditionalDocument";
            UpdFileRemove(D280, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, lblCleanApproveAuthority, flCleanApproveAuthority, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }

    protected void grdAlreadyAvailed_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            HiddenField hdfanew = (HiddenField)grdAlreadyAvailed.Rows[e.RowIndex].Cells[6].FindControl("hdnItr");
            DataTable dtnew0 = new DataTable();
            dtnew0 = (DataTable)ViewState["dtincentive"];
            DataRow[] dr1 = null;
            dr1 = dtnew0.Select("dcRowId='" + hdfanew.Value + "'");
            for (int i = 0; i < dr1.Length; i++)
            {

                dr1[i].Delete();
            }
            dtnew0.AcceptChanges();
            grdAlreadyAvailed.DataSource = dtnew0;
            grdAlreadyAvailed.DataBind();
            ViewState["dtincentive"] = dtnew0;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void ddlActivities_SelectedIndexChanged(object sender, EventArgs e)
    {

        PrioritySectorDetails PrioritySector = new PrioritySectorDetails();
        if (hdnsectorid.Value.Trim() == "")
        {
            PrioritySector.intSectorId = 0;
        }
        else
        {
            PrioritySector.intSectorId = Convert.ToInt16(hdnsectorid.Value);
        }
        if (hdnsubsectorid.Value.Trim() == "")
        {
            PrioritySector.intSubSectorId = 0;
        }
        else
        {
            PrioritySector.intSubSectorId = Convert.ToInt16(hdnsubsectorid.Value);
        }
        PrioritySector.intLiesSector = 1;
        PrioritySector.intActivityid = Convert.ToInt16(ddlActivities.SelectedValue.ToString());
        DataSet dtActs = IncentiveManager.FillAllActivities(PrioritySector);
        if (dtActs.Tables[0].Rows.Count > 0)
        {
            ddlSpecificActivity_old.DataSource = null;
            ddlSpecificActivity_old.DataTextField = "vchSegmentName";
            ddlSpecificActivity_old.DataValueField = "intsegmentid";
            ddlSpecificActivity_old.DataSource = dtActs.Tables[0];
            ddlSpecificActivity_old.DataBind();
        }

        foreach (ListItem _listItem in this.ddlSpecificActivity_old.Items)
        {

            _listItem.Attributes.Add("title", _listItem.Text);

        }
        foreach (ListItem _listItem in this.ddlSpecificActivity.Items)
        {

            _listItem.Attributes.Add("title", _listItem.Text);

        }
    }
    protected void btnselect_Click(object sender, EventArgs e)
    {
        foreach (ListItem li in ddlSpecificActivity_old.Items)
        {
            if (li.Selected == true)
            {
                ListItem tempItem = ddlSpecificActivity.Items.FindByValue(li.Value);
                if (tempItem == null)
                {
                    ListItem li1 = new ListItem();
                    li1.Text = li.Text;
                    li1.Value = li.Value;
                    ddlSpecificActivity.Items.Add(li1);
                }
            }
        }

        foreach (ListItem _listItem in this.ddlSpecificActivity_old.Items)
        {

            _listItem.Attributes.Add("title", _listItem.Text);

        }
        foreach (ListItem _listItem in this.ddlSpecificActivity.Items)
        {

            _listItem.Attributes.Add("title", _listItem.Text);

        }
    }

    protected void btnremove_Click(object sender, EventArgs e)
    {
        ListItem li = ddlSpecificActivity.SelectedItem;
        ddlSpecificActivity.Items.Remove(li);

        foreach (ListItem _listItem in this.ddlSpecificActivity_old.Items)
        {

            _listItem.Attributes.Add("title", _listItem.Text);

        }
        foreach (ListItem _listItem in this.ddlSpecificActivity.Items)
        {

            _listItem.Attributes.Add("title", _listItem.Text);

        }
    }

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
}


