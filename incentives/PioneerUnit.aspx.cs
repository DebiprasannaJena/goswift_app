using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.Data;
using System.ComponentModel;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using BusinessLogicLayer.Incentive;
using DataAcessLayer.Common;

public partial class incentives_EmploymentRating : SessionCheck
{
    #region Data Member

    //Incentive objIncentiveEntity = null;

    Additional doc;
    #endregion
    #region Member Functions
    string gFilePath = "../incentives/Files";

    Incentive objIncentive = new Incentive();
    int intUniqueId;//---------session value stored
    int intRetMsg;
    string eligbilemsg = "";
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
    string ChkPriorityApp(int intInvestorId)
    {


        string retval = "";
        try
        {
            retval = IncentiveManager.IsPriorityApp(intInvestorId, 3);
            eligbilemsg = retval;
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
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ChkPriorityApp(Convert.ToInt16(Session["InvestorId"])).Trim() != "1")
        {

            lblelgmsg.Text = eligbilemsg;

            pnldet.Visible = true;
        }

        if (ChkProvisionalCertAvail(Convert.ToInt16(Session["InvestorId"]), Convert.ToString(Request.QueryString["IncentiveNo"])) != "0")
        {
            lblprop.Visible = false;
            hypProposalPriority.Visible = true;
            hypProposalPriority.NavigateUrl = "../Portal/Incentive/Sanctionorder/" + ChkProvisionalCertAvail(Convert.ToInt16(Session["InvestorId"]), Convert.ToString(Request.QueryString["IncentiveNo"]));
        }
        else
        {
            lblprop.Visible = true;
            hypProposalPriority.Visible = false;
        }

        if (!IsPostBack)
        {
            //FillUnit(ddlUnit, "A");
            //FillUnit(ddlOrgType, "O");
            fillSalutation();
            FillPageHeaderDtl();
            documenttable();

            if (Convert.ToString(Session["ApplySource"]) == "0")
            {
                PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
                FillFormDetails(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            }
            else
            {
                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
                //FillPrepopulate(Convert.ToInt16(Session["InvestorId"]));
            }
        }
    }
    #endregion
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

    #region Fill Page Header Details
    private void FillPageHeaderDtl()
    {
        try
        {
            objIncentive = new Incentive();
            objIncentive.strcActioncode = "M";
            objIncentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            DataSet ds = new DataSet();
            ds = IncentiveManager.GetIncentiveMaster(objIncentive);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblTitle.Text = ds.Tables[0].Rows[0]["vchInctName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
    #region Fill File Upload
    public void FillFileUpladControls(DataTable dtFilesDtl)
    {
        try
        {
            if (dtFilesDtl.Rows.Count > 0 && dtFilesDtl != null)
            {
                for (int i = 0; i < dtFilesDtl.Rows.Count; i++)
                {
                    DataRow objgRow = dtFilesDtl.Rows[i];
                    //if (objgRow["vchDocId"].ToString() == hdnCertificateDocId.Value)
                    //{
                    //    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    //    {
                    //        hdnUploadCertificate.Value = objgRow["vchFileName"].ToString();
                    //        fldUploadCertificate.Enabled = false;
                    //        hlkViewCertificate.NavigateUrl = string.Format("~/incentives/" + objgRow["vchFolderPath"].ToString() + "{0}", objgRow["vchFileName"]);
                    //        hlkViewCertificate.Visible = true;
                    //        lknDelCertificate.Visible = true;
                    //    }
                    //}
                    if (objgRow["vchDocId"].ToString() == hdnApplAcknowDocId.Value)
                    {
                        if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                        {
                            hdnUploadApplAcknow.Value = objgRow["vchFileName"].ToString();
                            fldUploadApplAcknow.Enabled = false;
                            hlkViewApplAcknow.NavigateUrl = string.Format("~/incentives/" + objgRow["vchFolderPath"].ToString() + "{0}", objgRow["vchFileName"]);
                            hlkViewApplAcknow.Visible = true;
                            lknDelApplAcknow.Visible = true;
                        }
                    }
                    //if (objgRow["vchDocId"].ToString() == hdnUploadSatutoryDocId.Value)
                    //{
                    //    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    //    {
                    //        hdnUploadSatutory.Value = objgRow["vchFileName"].ToString();
                    //        fupSatutoryClean.Enabled = false;
                    //        lknViewSatutoryClean.NavigateUrl = string.Format("~/incentives/" + objgRow["vchFolderPath"].ToString() + "{0}", objgRow["vchFileName"]);
                    //        lknViewSatutoryClean.Visible = true;
                    //        lnkDelSatutoryClean.Visible = true;
                    //    }
                    //}


                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion

    #region Fill Form Details
    public void FillFormDetails(int id)
    {
        try
        {

            objIncentive = new Incentive();
            objIncentive.GetVwPrmtrs = new GetAndViewParam();
            objIncentive.GetVwPrmtrs.Param1ID = "";
            objIncentive.GetVwPrmtrs.Param2ID = "";
            objIncentive.GetVwPrmtrs.Param3ID = "";
            objIncentive.GetVwPrmtrs.Param4ID = "";
            objIncentive.GetVwPrmtrs.InctType = 4;
            objIncentive.UnqIncentiveId = id;

            objIncentive.FormType = FormNumber.PioneerUnits_03;
            DataSet dslive = IncentiveManager.GetIncentivePioneer(objIncentive);

            DataTable dtstatus = dslive.Tables[3];
            string draftStatus = dslive.Tables[5].Rows[0]["status"].ToString();//////---store draft satus


            #region Fill Priority Sector Details
            DataTable dtPrioritySector = dslive.Tables[0];
            FillPrioritySector(dtPrioritySector);
            #endregion

            #region Fill Additional Document
            DataTable dtAdditionalDoc = dslive.Tables[1];
            DataTable dtAdditionalDocDtl = dslive.Tables[2];
            FillAdditionalDoc(dtAdditionalDoc, dtAdditionalDocDtl);
            #endregion
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
                rdnLstPriority.SelectedValue = dtPrioritySector.Rows[0]["INTCERTAVAIL"].ToString();
                //if (dtPrioritySector.Rows[0]["VCHSECTORCERT"].ToString() != "")
                //{
                //    hdnUploadCertificate.Value = dtPrioritySector.Rows[0]["VCHSECTORCERT"].ToString();
                //    fldUploadCertificate.Enabled = false;
                //    hlkViewCertificate.NavigateUrl = "../incentives/Files/PrioritySectorDetails/" + dtPrioritySector.Rows[0]["VCHSECTORCERT"].ToString();
                //    hlkViewCertificate.Visible = true;
                //    lknDelCertificate.Visible = true;
                //}
                if (dtPrioritySector.Rows[0]["VCHACKNOW"].ToString() != "")
                {
                    hdnUploadApplAcknow.Value = dtPrioritySector.Rows[0]["VCHACKNOW"].ToString();
                    fldUploadApplAcknow.Enabled = false;
                    hlkViewApplAcknow.NavigateUrl = "../incentives/Files/PrioritySectorDetails/" + dtPrioritySector.Rows[0]["VCHACKNOW"].ToString();
                    hlkViewApplAcknow.Visible = true;
                    lknDelApplAcknow.Visible = true;

                }

            }
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

    public void FillUnit(DropDownList ddl, string action)
    {
        DataTable table = new DataTable();
        try
        {
            CommonDataLayer objDataUnit = new CommonDataLayer();
            ddl.DataTextField = "vchName";
            ddl.DataValueField = "slno";
            ddl.DataSource = objDataUnit.FillUnitType(action);
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-Select-", "0"));
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
    #region Button Clicks
    /// <summary>
    /// Created by Anjali Panigrahi
    /// To save all data in draft
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDraft_Click(object sender, EventArgs e)
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
            SavePioneerForm();

            //Incentive objEntity = new Incentive();


            objIncentive.IndsutUnitMstDet = new INDUSTRIAL_UNIT_MASTER();


            INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();


            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            if (DdlGender.SelectedIndex >= 0)
            {
                objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            }
            if (radApplyBy.SelectedIndex >= 0)
            {
                objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }
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
            objIncentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objIncentive.PealNum = Convert.ToString(Session["ProposalNo"]);
            objIncentive.PCNum = Convert.ToString(Session["PCNo"]);
            objIncentive.UnitCode = Convert.ToString(Session["UnitCode"]);
            objIncentive.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objIncentive.Userid = Convert.ToInt16(Session["InvestorId"]);
            objIncentive.Createdby = Convert.ToInt16(Session["InvestorId"]);
            objIncentive.incentivetype = 4;
            objIncentive.FormType = FormNumber.PioneerUnits_03;
            objIncentive.FileUploadDetails = getFileUploadDatatable();

            string retval = IncentiveManager.CreateIncentivePioneer(objIncentive);
            string msg = "<strong>Application Drafted Successfully !!</strong>";
            string msgTtl = "SWP";
            if (retval.Split('~')[0].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + msg + "', '" + msgTtl + "');   </script>", false);

            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    /// <summary>
    /// Created by Anjali Panigrahi
    /// To save all data in draft
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnApply_Click(object sender, EventArgs e)
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

            SavePioneerForm();



            ///Incentive objEntity = new Incentive();
            ///
            objIncentive.IndsutUnitMstDet = new INDUSTRIAL_UNIT_MASTER();

            INDUSTRIAL_UNIT_MASTER objINDUSTRIAL_UNIT_MASTER = new INDUSTRIAL_UNIT_MASTER();

            objINDUSTRIAL_UNIT_MASTER.APPLICANTNAME_IND = TxtApplicantName.Text;
            if (DdlGender.SelectedIndex >= 0)
            {
                objINDUSTRIAL_UNIT_MASTER.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            }
            if (radApplyBy.SelectedIndex >= 0)
            {
                objINDUSTRIAL_UNIT_MASTER.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }
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
            objIncentive.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objIncentive.PealNum = Convert.ToString(Session["ProposalNo"]);
            objIncentive.PCNum = Convert.ToString(Session["PCNo"]);
            objIncentive.UnitCode = Convert.ToString(Session["UnitCode"]);
            objIncentive.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objIncentive.Userid = Convert.ToInt16(Session["InvestorId"]);
            objIncentive.Createdby = Convert.ToInt16(Session["InvestorId"]);
            objIncentive.ApprovalAction = "A";
            objIncentive.incentivetype = 4;
            objIncentive.FormType = FormNumber.PioneerUnits_03;
            objIncentive.FileUploadDetails = getFileUploadDatatable();

            string retval = IncentiveManager.CreateIncentivePioneer(objIncentive);
            int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
            ///------------------------------------------------------------------------------------------------
            Response.Redirect("FormPreview_PioneerUnit.aspx?InctUniqueNo=" + Convert.ToString(mstyp));


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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

        if (D274.Value != "")
        {
            DataRow dorgRowT = dtFiles.NewRow();
            dorgRowT["vchDocId"] = "D274";
            dorgRowT["vchFileName"] = D274.Value;
            dorgRowT["vchFilePath"] = "Files/AdditionalDocument/";
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

    public void SavePioneerForm()
    {
        #region Additional Document

        AdditionalDocument();

        #endregion

        #region Priority sector Details
        PrioritySectorDetails();

        #endregion
    }
    /// <summary>
    /// Function to add Additional documents
    /// </summary>
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
    /// <summary>
    /// Function to add Priority Details
    /// </summary>
    protected void PrioritySectorDetails()
    {
        objIncentive.PrioritySector = new EntityLayer.Incentive.PrioritySectorDetails();
        objIncentive.PrioritySector.intAvailPriorityCertf = Convert.ToInt32(rdnLstPriority.SelectedValue);
        objIncentive.PrioritySector.strPrioritycCertf2015 = "";
        objIncentive.PrioritySector.strAppcnAcknow = hdnUploadApplAcknow.Value;
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
                    if (lFileExt == ".pdf")
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
                throw ex;
            }

        }

    }

    //protected void grdUpload_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    //Checking the RowType of the Row  
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        HyperLink hlnkFileName = e.Row.FindControl("hlnkFileName") as HyperLink;
    //        hlnkFileName.NavigateUrl = "../incentives/Files/AdditionalDocument/" + hlnkFileName.Text;

    //    }
    //}
    /// <summary>
    /// Function to add Investment Details
    /// </summary>


    #region Industrial
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
            else
            {
                //  AUTHORIZEDFILE = LnkAUTHORIZEDFILE.Text;
            }

            //if (FlupRehabilDoc.HasFile)
            //{
            //    extension = Path.GetExtension(FlupRehabilDoc.PostedFile.FileName);
            //    RehabilDoc = "RehabilDoc_" + dtms + extension;
            //    FlupRehabilDoc.SaveAs(Server.MapPath("../incentives/Files/IndustryUnit/") + RehabilDoc);
            //}
            //else
            //{
            //    RehabilDoc = LnkRehabilDoc.Text;
            //}

            //if (FluIndustryUnitDoc.HasFile)
            //{
            //    extension = Path.GetExtension(FluIndustryUnitDoc.PostedFile.FileName);
            //    IndustryUnitDoc = "PinoneerDoc_" + dtms + extension;
            //    FluIndustryUnitDoc.SaveAs(Server.MapPath("../incentives/Files/IndustryUnit/") + IndustryUnitDoc);
            //}
            //else
            //{
            //    IndustryUnitDoc = LnkIndustryUnitDoc.Text;
            //}
            //if (FluPinoneerDoc.HasFile)
            //{
            //    extension = Path.GetExtension(FluPinoneerDoc.PostedFile.FileName);
            //    PinoneerDoc = "PinoneerDoc_" + dtms + extension;
            //    FluPinoneerDoc.SaveAs(Server.MapPath("../incentives/Files/IndustryUnit/") + PinoneerDoc);
            //}
            //else
            //{
            //    PinoneerDoc = LnkPinoneerDoc.Text;
            //}
            //if (FluCertificateRegistration.HasFile)
            //{
            //    extension = Path.GetExtension(FluCertificateRegistration.PostedFile.FileName);
            //    CertificateRegistration = "CertificateRegistration_" + dtms + extension;
            //    FluCertificateRegistration.SaveAs(Server.MapPath("../incentives/Files/IndustryUnit/") + CertificateRegistration);
            //}
            //else
            //{
            //    CertificateRegistration = LnkCertificateRegistration.Text;
            //}
            //if (FluCertificateCommence.HasFile)
            //{
            //    extension = Path.GetExtension(FluCertificateCommence.PostedFile.FileName);
            //    CertificateCommence = "CertificateCommence_" + dtms + extension;
            //    FluCertificateCommence.SaveAs(Server.MapPath("../incentives/Files/IndustryUnit/") + CertificateCommence);
            //}
            //else
            //{
            //    CertificateCommence = LnkCertificateCommence.Text;
            //}

        }
        catch (Exception x)
        {
            throw x;
        }
    }
    /// <summary>
    /// change date from dd/MM/yyyy format to yyyy/MM/dd format inorderr to operatre in DB
    /// </summary>
    /// <param name="srcDate"></param>
    /// <returns></returns>
    public string ReturnDateFormat(string srcDate)
    {
        //string resdt = "1900/01/01";
        //try
        //{
        //    if (srcDate != "")
        //    {
        //        string[] strarr = srcDate.Split('/');
        //        resdt = strarr[2] + "/" + strarr[0] + "/" + strarr[1];
        //    }
        //}
        //catch (Exception)
        //{
        //}

        return srcDate;
    }
    /// <summary>
    /// Attribute assignement to Industry master class object
    /// </summary>
    /// <returns></returns>
    public void IndustryDataSave(Incentive objIncentive)
    {


        try
        {

            string AUTHORIZEDFILE = "";
            string RehabilDoc = "";
            string IndustryUnitDoc = "";
            string PinoneerDoc = "";
            string CertificateRegistration = "";
            string CertificateCommence = "";

            SaveInd_Document(ref AUTHORIZEDFILE, ref RehabilDoc, ref IndustryUnitDoc, ref PinoneerDoc, ref CertificateRegistration, ref CertificateCommence);
            objIncentive.IndsutUnitMstDet = new INDUSTRIAL_UNIT_MASTER();


            //objIncentive.IndsutUnitMstDet.ENTERPRISENAME_IND = TxtEnterPrise.Text.Trim();
            //objIncentive.IndsutUnitMstDet.ORGANISATIONTYPE_IND = ddlOrgType.SelectedValue;
            objIncentive.IndsutUnitMstDet.GENDER_IND = DdlGender.SelectedValue;
            objIncentive.IndsutUnitMstDet.APPLICANTNAME_IND = TxtApplicantName.Text.Trim();
            objIncentive.IndsutUnitMstDet.APPLYBY_IND = radApplyBy.SelectedIndex == -1 ? "0" : radApplyBy.SelectedValue;
            objIncentive.IndsutUnitMstDet.AADHAARNO_IND = TxtAdhaar1.Text.Trim();
            objIncentive.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND = AUTHORIZEDFILE;///// file upload
            //objIncentive.IndsutUnitMstDet.INDUSTRYADDRESS_IND = TxtAddressInd.Text.Trim();
            //objIncentive.IndsutUnitMstDet.CATAGORYUNIT_IND = ddlUnitCategory.SelectedValue;
            //objIncentive.IndsutUnitMstDet.UNITTYPE_IND = ddlUnitType.SelectedValue;
            objIncentive.IndsutUnitMstDet.REHABILITATEDDOCUMENT_IND = RehabilDoc;///// file upload
            objIncentive.IndsutUnitMstDet.INDUSTRIALDOCUMENT_IND = IndustryUnitDoc;///// file upload
            //objIncentive.IndsutUnitMstDet.PRIORITY_IND = RadIsPriority.SelectedIndex == -1 ? "0" : RadIsPriority.SelectedValue;
            //objIncentive.IndsutUnitMstDet.PIONEER_IND = RadIspioneer.SelectedIndex == -1 ? "0" : RadIspioneer.SelectedValue;
            objIncentive.IndsutUnitMstDet.PIONEERCERTIFICATE_IND = PinoneerDoc;///// file upload
            //objIncentive.IndsutUnitMstDet.REGISTEREDOFCADDRESS_IND = TxtRegAddress.Text.Trim();
            //objIncentive.IndsutUnitMstDet.MANAGINGPARTNERGENDER_IND = ddlGenderPartner.SelectedValue;
            //objIncentive.IndsutUnitMstDet.MANAGINGPARTNERNAME_IND = TxtNamePartner.Text.Trim();
            objIncentive.IndsutUnitMstDet.CERTIFICATEOFREGISTRATION_IND = CertificateRegistration;///// file upload
            //objIncentive.IndsutUnitMstDet.EINNO_IND = TxtEIN_IL_NO.Text.Trim();
            //objIncentive.IndsutUnitMstDet.EINDATE_IND = ReturnDateFormat(TxtEIN_IL_Date.Text.Trim());/////----------- datetime
            //objIncentive.IndsutUnitMstDet.PCNO_INDUSTRIAL_IND = TxtPCNo.Text.Trim();
            //objIncentive.IndsutUnitMstDet.PCISSUANCE_IND = ReturnDateFormat(TxtPCInsDate.Text.Trim());/////------- dateime
            //objIncentive.IndsutUnitMstDet.COMMENCEMENT_IND = TxtCommenceDate.Text.Trim();
            objIncentive.IndsutUnitMstDet.COMMENCEMENTCERTIFICATE_IND = CertificateCommence;///// file upload 
            /////////------------not used property and assigning default values--------------        
            objIncentive.IndsutUnitMstDet.COMPANYDATE_IND = "1900/01/01";
            objIncentive.IndsutUnitMstDet.COMPANYPLACE_IND = "";
            objIncentive.IndsutUnitMstDet.REGISTRATIONNO_IND = "";
            objIncentive.IndsutUnitMstDet.COMPANYCIN_IND = "";
            objIncentive.IndsutUnitMstDet.COMPANYPAN_IND = "";
            objIncentive.IndsutUnitMstDet.TINVAT_IND = "";
            objIncentive.IndsutUnitMstDet.DISTID_IND = "0";



        }
        catch (Exception x)
        {

        }



    }
    #endregion




    protected void ImageButtonDelete_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {

            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);


            table.Columns.Add("VCHPRODUCTNAME", typeof(string));
            table.Columns.Add("INTQUANTITY", typeof(string));
            table.Columns.Add("VCHUNIT", typeof(string));
            table.Columns.Add("INTUNIT", typeof(string));
            table.Columns.Add("DECVALUE", typeof(string));

            //for (int i = 0; i < grdProduction.Rows.Count; i++)
            //{
            //    if (i != RowID)
            //    {
            //        Label lblProductionName = (Label)grdProduction.Rows[i].FindControl("lblProductionName");
            //        Label lblQuantity = (Label)grdProduction.Rows[i].FindControl("lblQuantity");
            //        Label lblUnit = (Label)grdProduction.Rows[i].FindControl("lblUnit");
            //        Label lblValue = (Label)grdProduction.Rows[i].FindControl("lblValue");
            //        HiddenField hdnUnit = (HiddenField)grdProduction.Rows[i].FindControl("hdnUnit");
            //        table.Rows.Add(lblProductionName.Text, lblQuantity.Text, lblUnit.Text, hdnUnit.Value, lblValue.Text);
            //    }
            //}

            //grdProduction.DataSource = table;
            //grdProduction.DataBind();

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

    public DataTable GetTableWithInitialData() // this might be your sp for select
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("VCHPRODUCTNAME", typeof(string));
            table.Columns.Add("INTQUANTITY", typeof(string));
            table.Columns.Add("VCHUNIT", typeof(string));
            table.Columns.Add("INTUNIT", typeof(string));
            table.Columns.Add("DECVALUE", typeof(string));
            //table.Rows.Add(txtProductName.Text, txtQuantity.Text, ddlUnit.SelectedItem.Text, ddlUnit.SelectedValue, txtValue.Text);
            //for (int i = 0; i < grdProduction.Rows.Count; i++)
            //{
            //    Label lblProductionName = (Label)grdProduction.Rows[i].FindControl("lblProductionName");
            //    Label lblQuantity = (Label)grdProduction.Rows[i].FindControl("lblQuantity");
            //    Label lblUnit = (Label)grdProduction.Rows[i].FindControl("lblUnit");
            //    Label lblValue = (Label)grdProduction.Rows[i].FindControl("lblValue");
            //    HiddenField hdnUnit = (HiddenField)grdProduction.Rows[i].FindControl("hdnUnit");
            //    table.Rows.Add(lblProductionName.Text, lblQuantity.Text, lblUnit.Text, hdnUnit.Value, lblValue.Text);
            //}

            return table;
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            table = null;
        }


    }
    /// <summary>
    /// Function to add Means Of Finance
    /// </summary>


    public DataTable GetTableLoanData() // this might be your sp for select
    {
        DataTable table = new DataTable();
        try
        {
            //table.Columns.Add("INT_LOANDTL_ID", typeof(string));
            table.Columns.Add("VCH_NAME_OF_FINANCIAL_INST", typeof(string));
            table.Columns.Add("VCH_STATE", typeof(string));
            table.Columns.Add("VCH_CITY", typeof(string));
            table.Columns.Add("DEC_LOAN_AMT", typeof(string));
            table.Columns.Add("DTM_SACTION_DATE", typeof(string));
            table.Columns.Add("DEC_AVAILED_AMT", typeof(string));
            table.Columns.Add("DTM_AVAILED_DATE", typeof(string));
            //for (int i = 0; i < grdMeansOfFinance.Rows.Count; i++)
            //{

            //    Label lblFinancialName = (Label)grdMeansOfFinance.Rows[i].FindControl("lblFinancialName");
            //    Label lblState = (Label)grdMeansOfFinance.Rows[i].FindControl("lblState");
            //    Label lblCity = (Label)grdMeansOfFinance.Rows[i].FindControl("lblCity");
            //    Label lblTermLoan = (Label)grdMeansOfFinance.Rows[i].FindControl("lblTermLoan");
            //    Label lblSacDate = (Label)grdMeansOfFinance.Rows[i].FindControl("lblSacDate");
            //    Label lblAvailedAmt = (Label)grdMeansOfFinance.Rows[i].FindControl("lblAvailedAmt");
            //    Label lblAvailedDate = (Label)grdMeansOfFinance.Rows[i].FindControl("lblAvailedDate");
            //    table.Rows.Add(lblFinancialName.Text, lblState.Text, lblCity.Text, lblTermLoan.Text, lblSacDate.Text, lblAvailedAmt.Text, lblAvailedDate.Text);
            //}
            //table.Rows.Add(txtNameOfFinancialInst.Text, txtLocationState.Text, txtLocationCity.Text, txtLoanAmt.Text, txtSactionDate.Text, txtAvailedAmt.Text, txtAvailedDate.Text);
            return table;
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            table = null;
        }


    }
    public DataTable GetTableLoanData_Working() // this might be your sp for select
    {
        DataTable table = new DataTable();
        try
        {
            //table.Columns.Add("INT_LOANDTL_ID", typeof(string));
            table.Columns.Add("VCH_NAME_OF_FINANCIAL_INST_WORKING", typeof(string));
            table.Columns.Add("VCH_STATE_WORKING", typeof(string));
            table.Columns.Add("VCH_CITY_WORKING", typeof(string));
            table.Columns.Add("DEC_LOAN_AMT_WORKING", typeof(string));
            table.Columns.Add("DTM_SACTION_DATE_WORKING", typeof(string));
            table.Columns.Add("DEC_AVAILED_AMT_WORKING", typeof(string));
            table.Columns.Add("DTM_AVAILED_DATE_WORKING", typeof(string));
            //for (int i = 0; i < grdWorkingLoan.Rows.Count; i++)
            //{
            //    //HiddenField hdnLoanDetailid = (HiddenField)grdMeansOfFinance.Rows[i].FindControl("hdnLoanDetailid");
            //    Label lblFinancialName_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblFinancialName_working");
            //    Label lblState_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblState_working");
            //    Label lblCity_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblCity_working");
            //    Label lblTermLoan_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblTermLoan_working");
            //    Label lblSacDate_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblSacDate_working");
            //    Label lblAvailedAmt_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblAvailedAmt_working");
            //    Label lblAvailedDate_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblAvailedDate_working");
            //    table.Rows.Add(lblFinancialName_working.Text, lblState_working.Text, lblCity_working.Text, lblTermLoan_working.Text, lblSacDate_working.Text, lblAvailedAmt_working.Text, lblAvailedDate_working.Text);
            //}
            //table.Rows.Add(txtNameOfFinancialInst_working.Text, txtLocationState_working.Text, txtLocationCity_working.Text, txtLoanAmt_working.Text, txtSactionDate_working.Text, txtAvailedAmt_working.Text, txtAvailedDate_working.Text);
            return table;
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            table = null;
        }


    }

    protected void lnkTermLoan_Click(object sender, EventArgs e)
    {
        //grdMeansOfFinance.DataSource = GetTableLoanData();
        //grdMeansOfFinance.DataBind();
        //txtNameOfFinancialInst.Text = "";
        //txtLocationState.Text = "";
        //txtLocationCity.Text = "";
        //txtLoanAmt.Text = "";
        //txtSactionDate.Text = "";
        //txtAvailedAmt.Text = "";
        //txtAvailedDate.Text = "";


    }
    protected void lnk_working_Click(object sender, EventArgs e)
    {
        //grdWorkingLoan.DataSource = GetTableLoanData_Working();
        //grdWorkingLoan.DataBind();
        //txtNameOfFinancialInst_working.Text = "";
        //txtLocationState_working.Text = "";
        //txtLocationCity_working.Text = "";
        //txtLoanAmt_working.Text = "";
        //txtSactionDate_working.Text = "";
        //txtAvailedAmt_working.Text = "";
        //txtAvailedDate_working.Text = "";
    }

    protected void ImageButtonDelete_working_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {

            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            //table.Columns.Add("INT_LOANDTL_ID", typeof(string));
            table.Columns.Add("VCH_NAME_OF_FINANCIAL_INST_WORKING", typeof(string));
            table.Columns.Add("VCH_STATE_WORKING", typeof(string));
            table.Columns.Add("VCH_CITY_WORKING", typeof(string));
            table.Columns.Add("DEC_LOAN_AMT_WORKING", typeof(string));
            table.Columns.Add("DTM_SACTION_DATE_WORKING", typeof(string));
            table.Columns.Add("DEC_AVAILED_AMT_WORKING", typeof(string));
            table.Columns.Add("DTM_AVAILED_DATE_WORKING", typeof(string));

            //for (int i = 0; i < grdWorkingLoan.Rows.Count; i++)
            //{
            //    if (i != RowID)
            //    {

            //        Label lblFinancialName_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblFinancialName_working");
            //        Label lblState_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblState_working");
            //        Label lblCity_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblCity_working");
            //        Label lblTermLoan_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblTermLoan_working");
            //        Label lblSacDate_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblSacDate_working");
            //        Label lblAvailedAmt_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblAvailedAmt_working");
            //        Label lblAvailedDate_working = (Label)grdWorkingLoan.Rows[i].FindControl("lblAvailedDate_working");
            //        table.Rows.Add(lblFinancialName_working.Text, lblState_working.Text, lblCity_working.Text, lblTermLoan_working.Text, lblSacDate_working.Text, lblAvailedAmt_working.Text, lblAvailedDate_working.Text);
            //    }
            //}

            //grdWorkingLoan.DataSource = table;
            //grdWorkingLoan.DataBind();

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
    protected void imgbtnTermLoan_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {

            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            //table.Columns.Add("INT_LOANDTL_ID", typeof(string));
            table.Columns.Add("VCH_NAME_OF_FINANCIAL_INST", typeof(string));
            table.Columns.Add("VCH_STATE", typeof(string));
            table.Columns.Add("VCH_CITY", typeof(string));
            table.Columns.Add("DEC_LOAN_AMT", typeof(string));
            table.Columns.Add("DTM_SACTION_DATE", typeof(string));
            table.Columns.Add("DEC_AVAILED_AMT", typeof(string));
            table.Columns.Add("DTM_AVAILED_DATE", typeof(string));

            //for (int i = 0; i < grdMeansOfFinance.Rows.Count; i++)
            //{
            //    if (i != RowID)
            //    {
            //        HiddenField hdnLoanDetailid = (HiddenField)grdMeansOfFinance.Rows[i].FindControl("hdnLoanDetailid");
            //        Label lblFinancialName = (Label)grdMeansOfFinance.Rows[i].FindControl("lblFinancialName");
            //        Label lblState = (Label)grdMeansOfFinance.Rows[i].FindControl("lblState");
            //        Label lblCity = (Label)grdMeansOfFinance.Rows[i].FindControl("lblCity");
            //        Label lblTermLoan = (Label)grdMeansOfFinance.Rows[i].FindControl("lblTermLoan");
            //        Label lblSacDate = (Label)grdMeansOfFinance.Rows[i].FindControl("lblSacDate");
            //        Label lblAvailedAmt = (Label)grdMeansOfFinance.Rows[i].FindControl("lblAvailedAmt");
            //        Label lblAvailedDate = (Label)grdMeansOfFinance.Rows[i].FindControl("lblAvailedDate");
            //        table.Rows.Add(lblFinancialName.Text, lblState.Text, lblCity.Text, lblTermLoan.Text, lblSacDate.Text, lblAvailedAmt.Text, lblAvailedDate.Text);
            //    }
            //}

            //grdMeansOfFinance.DataSource = table;
            //grdMeansOfFinance.DataBind();

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

    //////////////////////////////----------------New File Upload Logic-----------------
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string strFolername)
    {
        string filename = hdnFile.Value;
        string path = string.Format("~/incentives/Files/{0}/{1}", strFolername, filename);
        string completePath = Server.MapPath(path);
        //if (File.Exists(completePath))
        //{
        //File.Delete(completePath);
        hdnFile.Value = "";
        lnkDel.Visible = false;
        lnkBtn.Visible = true;
        hplnk.Visible = false;
        lblFile.Visible = false;
        updFile.Enabled = true;
        //}
    }
    private void UploadDocument(FileUpload fuOrgDocument, HiddenField hdnOrgDocument, string strFileName, HyperLink hypOrdDocument, Label lblOrgDocument, LinkButton lnkOrgDocumentDelete, string strFoldername)
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

    protected void lnkOrgDocumentPdf_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        if (string.Equals(lnk.ID, lknAddApplAcknow.ID))
        {
            if (fldUploadApplAcknow.HasFile)
            {
                string strFileName = "PrioritySec" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(fldUploadApplAcknow, hdnUploadApplAcknow, strFileName, hlkViewApplAcknow, lblApplAcknow, lknDelApplAcknow, "PrioritySectorDetails");
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
    }
    protected void lnkOrgDocumentDelete_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;

        if (string.Equals(lnk.ID, lknDelApplAcknow.ID))
        {
            UpdFileRemove(hdnUploadApplAcknow, lknAddApplAcknow, lknDelApplAcknow, hlkViewApplAcknow, lblApplAcknow, fldUploadApplAcknow, "PrioritySectorDetails");
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
    }
    /////////////////////////////////////////////------------------------------------end 


    #endregion

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


    protected void lnkUValidStatutary_click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        UploadProcess(flValidStatutary, D274, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, Path, "ValidStatutary");
    }
    protected void lnkDValidStatutary_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        DeleteProcess(flValidStatutary, D274, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, Path, "ValidStatutary");
    }
    protected void lnkUDelay_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        UploadProcess(flDelay, D275, lnkUDelay, lnkDDelay, hypDelay, lblDelay, Path, "Delay");
    }
    protected void lnkDDelay_Click(object sender, EventArgs e)
    {
        string Path = "~/incentives/Files/AdditionalDocument";
        DeleteProcess(flDelay, D275, lnkUDelay, lnkDDelay, hypDelay, lblDelay, Path, "Delay");
    }


    public void DeleteProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string ModuleName)
    {
        try
        {
            string fileName = hdn.Value;


            if (fileName != "")
                if (File.Exists(Server.MapPath(xPath)))
                    File.Delete(Server.MapPath(xPath));


            hdn.Value = "";
            hyp.NavigateUrl = "";


            F.Enabled = true;
            LU.Visible = true;

            LD.Visible = false;
            hyp.Visible = false;
            lblMsg.Visible = false;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    public void UploadProcess(FileUpload F, HiddenField hdn, LinkButton LU, LinkButton LD, HyperLink hyp, Label lblMsg, string xPath, string ModuleName)
    {
        try
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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

                TxtApplicantName.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
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
                    string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss") + extension;
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