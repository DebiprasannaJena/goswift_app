using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityLayer.Incentive;
using System.IO;
using DataAcessLayer.Common;
using DataAcessLayer.Incentive;
using System.Collections.Specialized;
using BusinessLogicLayer.Incentive;
public partial class Subsidy_Plant_MC : SessionCheck
{
    #region Avail Details
    DataTable gObjDtIncentiveAvailed = new DataTable();
    Incentive objEntity = new Incentive();
    string strMsg = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];//"Incentive";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fillSalutation();
            txtsacdat.Attributes.Add("readonly", "readonly");
            GetMasterdetails();
            crdtincentive();
            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
                PrepopulateDataPlus(Convert.ToInt16(Session["InvestorId"]));
            }
            else
            {
                PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            }

        }
    }
    public void GetMasterdetails()
    {
        try
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    //private void FillApplyBy()
    //{
    //    try
    //    {
    //        IncentiveMasterDataLayer objinsentive = new IncentiveMasterDataLayer();
    //        DataSet dsdoc = new DataSet();
    //        dsdoc = objinsentive.BindDropdown("grp");
    //        radAuthorizing.DataSource = dsdoc;
    //        radAuthorizing.DataTextField = "vchDocName";
    //        radAuthorizing.DataValueField = "vchDocId";
    //        radAuthorizing.DataBind();
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}



    protected void btnDraft_Click(object sender, EventArgs e)
    {

        string msgdt = Add(0);
        if (msgdt.Split('~')[0].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !!</strong>', '" + strMsg + "'); </script>", false);

        }


    }
    public string Add(int save)
    {
        string ret_Val = string.Empty;
        Production objProd = new Production();
        List<ProductionItem> lists = new List<ProductionItem>();
        BankDetails objBank = new BankDetails();
        //avail details
        AvailDetails objAvailDetails = new AvailDetails();
        List<Assistance> listIncentiveAvailed = new List<Assistance>();
        Assistance objIncentiveAvailed = new Assistance();

        //objEntity.ProdEmpDet = objProd;
        try
        {
            objBank.BankName = txtBnkNm.Text;
            objBank.BranchName = txtBranch.Text;
            objBank.IFSCNo = txtIFSC.Text;
            objBank.AccountNo = txtAccNo.Text;
            objBank.MICRNo = txtMICRNo.Text;
            if (hdnBank.Value != "")
            {
                objBank.BankDoc = hdnBank.Value;
            }
            objEntity.BankDet = objBank;

            //Avail Details
            objAvailDetails.NeverAvailedPrior = Convert.ToInt16(RadBtn_Availed_Earlier.SelectedValue);
            objAvailDetails.SubsidyAvailed = 0;

            if (txtreimamt.Text.Trim() != "")
            {
                objAvailDetails.ClaimReimbursement = Convert.ToDouble(txtreimamt.Text.Trim());
            }
            if (RadBtn_Availed_Earlier.SelectedValue == "1")
            {
                objAvailDetails.ClaimtExempted = Convert.ToDouble(txtdiffclaimamt.Text.Trim() == "" ? "0" : txtdiffclaimamt.Text.Trim());
                objAvailDetails.SanctionOrderDoc = Hid_Asst_Sanc_File_Name.Value;

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

            //investment detail
            #region Investment Details
            objEntity.InvestmentDet = new EntityLayer.Incentive.InvestmentDetails();
            if (hdnDocfirstinvestment.Value != "")
            {
                objEntity.InvestmentDet.Document_in_support = hdnDocfirstinvestment.Value;
            }
            if (hdnDocfirstinvestment2.Value != "")
            {
                objEntity.InvestmentDet.vchNewBillDoc = hdnDocfirstinvestment2.Value;
            }


            if (hdnDocSecondinvestment.Value != "")
            {
                objEntity.InvestmentDet.vchSecHandDoc = hdnDocSecondinvestment.Value;
            }
            if (hdnDocSecondinvestment2.Value != "")
            {
                objEntity.InvestmentDet.vchSecHandBill = hdnDocSecondinvestment2.Value;
            }

            #endregion
            //industry
            IndustryDataSave();



            FileUploadControls();



            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                objEntity.UnqIncentiveId = 0;
            }
            else
            {
                objEntity.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            }
            objEntity.strcActioncode = "A";
            objEntity.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
            objEntity.PealNum = Convert.ToString(Session["ProposalNo"]);
            objEntity.PCNum = Convert.ToString(Session["PCNo"]);
            objEntity.UnitCode = Convert.ToString(Session["UnitCode"]);
            objEntity.ProposalNum = Convert.ToString(Session["ProposalNo"]);
            objEntity.Userid = Convert.ToInt16(Session["InvestorId"]);
            objEntity.Createdby = Convert.ToInt16(Session["InvestorId"]);
            //objEntity.FYear = Convert.ToInt16(Session["FyYear"]);
            objEntity.incentivetype = 4;
            objEntity.FormType = FormNumber.SubsidyOnPlantAndMachinery_05;

            ret_Val = IncentiveManager.CreateIncentiveSubsidyPlant_MAchinery(objEntity);




        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objProd = null;
            lists = null;
            objBank = null;
        }
        return ret_Val;
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        string retval = Add(0);
        int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
        /////------------------------------------------------------------------------------------------------
        Response.Redirect("Subsidy_Plant_MCPreview.aspx?InctUniqueNo=" + Convert.ToString(mstyp));
        //ScriptManager.RegisterStartupScript(btnApply, this.GetType(), "OnClick", "<script>jAlert('Data Saved Successfully.','SWP');location.href='Subsidy_Plant_MCPreview.aspx'</script>", false);

    }

    #region Industry

    public string ReturnDateFormat(string srcDate)
    {
        return srcDate;
    }
    public string IndustryDataSave()
    {

        string resdt = "1";
        try
        {
            objEntity.IndsutUnitMstDet = new INDUSTRIAL_UNIT_MASTER();

            objEntity.IndsutUnitMstDet.APPLICANTNAME_IND = TxtApplicantName.Text;
            objEntity.IndsutUnitMstDet.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue == "0" ? "1" : DdlGender.SelectedValue);
            if (radApplyBy.SelectedValue == "")
            {
                objEntity.IndsutUnitMstDet.APPLYBY_IND = 0;
            }
            else
            {
                objEntity.IndsutUnitMstDet.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }
            objEntity.IndsutUnitMstDet.AADHAARNO_IND = TxtAdhaar1.Text;
            objEntity.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;
            objEntity.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND = hidAuthorizing.Value;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }


        return resdt;
    }
    #endregion



    #region FileUPloadControls
    public void FileUploadControls()
    {
        try
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
            ////investment
            //if (hdnDocfirstinvestment.Value != "")
            //{
            //    fileList.Add(new lstFileUpload()
            //    {
            //        id = 1,
            //        vchDocId = "D255",
            //        vchFileName = hdnDocfirstinvestment.Value,
            //        vchFilePath = "../incentives/Files/investment/"

            //    });
            //}
            //Avail Details
            if (RadBtn_Availed_Earlier.SelectedValue == "1")
            {
                if (Hid_Asst_Sanc_File_Name.Value != "")
                {
                    fileList.Add(new lstFileUpload()
                    {
                        id = 1,
                        vchDocId = "D253",
                        vchFileName = Hid_Asst_Sanc_File_Name.Value,
                        vchFilePath = "../incentives/Files/AvailDetails/"
                        //vchFilePath = "../incentives/Files/AvailDetails/" + Session["investorid"].ToString() + "/"
                    });
                }
            }
            else
            {
                if (Hid_Undertaking_File_Name.Value != "")
                {
                    fileList.Add(new lstFileUpload()
                    {
                        id = 1,
                        vchDocId = "D230",
                        vchFileName = Hid_Undertaking_File_Name.Value,
                        vchFilePath = "../incentives/Files/AvailDetails/"
                        //vchFilePath = "../incentives/Files/AvailDetails/" + Session["investorid"].ToString() + "/"
                    });
                }
            }
            //Bank Details
            if (hdnBank.Value != "")
            {
                fileList.Add(new lstFileUpload()
                {
                    id = 1,
                    vchDocId = "D266",
                    vchFileName = hdnBank.Value,
                    vchFilePath = "../incentives/Files/Bank/"

                });
            }
            objEntity.FileUploadDetails = fileList;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion






    protected void lnkDocfirstinvestmentUpload_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkDocfirstinvestmentUpload.ID))
            {
                if (fileDocfirstinvestment.HasFile)
                {
                    string strFileName = "FirstInvestment" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fileDocfirstinvestment, hdnDocfirstinvestment, strFileName, hypDocfirstinvestment, lblDocfirstinvestment, lnkDocfirstinvestmentDelete, "../incentives/Files/investment/", "excel", lnkDocfirstinvestmentUpload);
                }
            }
            else if (string.Equals(lnk.ID, lnkDocfirstinvestmentUpload2.ID))
            {
                if (fileDocfirstinvestment2.HasFile)
                {
                    string strFileName = "FirstBills" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fileDocfirstinvestment2, hdnDocfirstinvestment2, strFileName, hypDocfirstinvestment2, lblDocfirstinvestment2, lnkDocfirstinvestmentDelete2, "../incentives/Files/investment/", "zip", lnkDocfirstinvestmentUpload2);
                }
            }
            else if (string.Equals(lnk.ID, lnkDocSecondinvestmentUpload.ID))
            {
                if (fileDocSecondinvestment.HasFile)
                {
                    string strFileName = "SecondInvestment" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fileDocSecondinvestment, hdnDocSecondinvestment, strFileName, hypDocSecondinvestment, lblDocSecondinvestment, lnkDocSecondmentDelete, "../incentives/Files/investment/", "excel", lnkDocSecondinvestmentUpload);
                }
            }
            else if (string.Equals(lnk.ID, lnkDocSecondinvestmentUpload2.ID))
            {
                if (fileDocSecondinvestment2.HasFile)
                {
                    string strFileName = "SecondBills" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fileDocSecondinvestment2, hdnDocSecondinvestment2, strFileName, hypDocSecondinvestment2, lblDocSecondinvestment2, lnkDocSecondmentDelete2, "../incentives/Files/investment/", "zip", lnkDocSecondinvestmentUpload2);
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Asst_Sanc_Doc.ID))
            {
                if (FU_Asst_Sanc_Doc.HasFile)
                {
                    string strFileName = "ASSTSANC" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(FU_Asst_Sanc_Doc, Hid_Asst_Sanc_File_Name, strFileName, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, "../incentives/Files/AvailDetails/", "pdf/zip", LnkBtn_Upload_Asst_Sanc_Doc);
                    //UploadDocument(FU_Asst_Sanc_Doc, Hid_Asst_Sanc_File_Name, strFileName, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, "../incentives/Files/AvailDetails/" + Session["investorid"].ToString() + "/", "pdf/zip", LnkBtn_Upload_Asst_Sanc_Doc);
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Undertaking_Doc.ID))
            {
                if (FU_Undertaking_Doc.HasFile)
                {
                    string strFileName = "UND" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(FU_Undertaking_Doc, Hid_Undertaking_File_Name, strFileName, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, "../incentives/Files/AvailDetails/", "pdf/zip", LnkBtn_Upload_Undertaking_Doc);
                    //UploadDocument(FU_Undertaking_Doc, Hid_Undertaking_File_Name, strFileName, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, "../incentives/Files/AvailDetails/" + Session["investorid"].ToString() + "/", "pdf/zip", LnkBtn_Upload_Undertaking_Doc);
                }
            }
            else if (string.Equals(lnk.ID, lnkBankUpload.ID))
            {
                if (fuBank.HasFile)
                {
                    string strFileName = "Bank" + DateTime.Now.ToString("_ddMMyyhhmmss");
                    UploadDocument(fuBank, hdnBank, strFileName, hypBank, lblBank, lnkBankDelete, "../incentives/Files/Bank/", "pdf/jpg/jpeg", lnkBankUpload);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void lnkDocfirstinvestmentDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkDocfirstinvestmentDelete.ID))
            {
                UpdFileRemove(hdnDocfirstinvestment, lnkDocfirstinvestmentUpload, lnkDocfirstinvestmentDelete, hypDocfirstinvestment, lblDocfirstinvestment, fileDocfirstinvestment, "../incentives/Files/investment/");
            }
            else if (string.Equals(lnk.ID, lnkDocfirstinvestmentDelete2.ID))
            {
                UpdFileRemove(hdnDocfirstinvestment2, lnkDocfirstinvestmentUpload2, lnkDocfirstinvestmentDelete2, hypDocfirstinvestment2, lblDocfirstinvestment2, fileDocfirstinvestment2, "../incentives/Files/investment/");
            }
            else if (string.Equals(lnk.ID, lnkDocSecondmentDelete.ID))
            {
                UpdFileRemove(hdnDocSecondinvestment, lnkDocSecondinvestmentUpload, lnkDocSecondmentDelete, hypDocSecondinvestment, lblDocSecondinvestment, fileDocSecondinvestment, "../incentives/Files/investment/");
            }
            else if (string.Equals(lnk.ID, lnkDocSecondmentDelete2.ID))
            {
                UpdFileRemove(hdnDocSecondinvestment2, lnkDocSecondinvestmentUpload2, lnkDocSecondmentDelete2, hypDocSecondinvestment2, lblDocSecondinvestment2, fileDocSecondinvestment2, "../incentives/Files/investment/");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Asst_Sanc_Doc.ID))
            {
                UpdFileRemove(Hid_Asst_Sanc_File_Name, LnkBtn_Upload_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, "../incentives/Files/AvailDetails/");
                //UpdFileRemove(Hid_Asst_Sanc_File_Name, LnkBtn_Upload_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, "../incentives/Files/AvailDetails/" + Session["investorid"].ToString() + "/");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Undertaking_Doc.ID))
            {
                UpdFileRemove(Hid_Undertaking_File_Name, LnkBtn_Upload_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, FU_Undertaking_Doc, "../incentives/Files/AvailDetails/");
                //UpdFileRemove(Hid_Undertaking_File_Name, LnkBtn_Upload_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, FU_Undertaking_Doc, "../incentives/Files/AvailDetails/" + Session["investorid"].ToString() + "/");
            }
            else if (string.Equals(lnk.ID, lnkBankDelete.ID))
            {
                UpdFileRemove(hdnBank, lnkBankUpload, lnkBankDelete, hypBank, lblBank, fuBank, "../incentives/Files/Bank/");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #region Methods
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
    private void UploadDocument(FileUpload fuOrgDocument, HiddenField hdnOrgDocument, string strFileName, HyperLink hypOrdDocument, Label lblOrgDocument, LinkButton lnkOrgDocumentDelete, string FolderPath, string Extention, LinkButton lnkBtnup = null)
    {
        try
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots. ', '" + strMsg + "'); </script>", false);
                    return;
                }
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
                else if (Extention == "pdf/jpg/jpeg")
                {
                    if ((Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".pdf") && (Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".jpg") && (Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".jpeg"))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .pdf/.jpg/.jpeg file Only!','" + strMsg + "')", true);
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
                else if (Extention == "zip")
                {
                    if (Path.GetExtension(fuOrgDocument.FileName).ToLower() != ".zip")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "jAlert('Please Upload .zip file Only!','" + strMsg + "')", true);
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
                lnkBtnup.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string FolderPath)
    {
        try
        {
            string filename = hdnFile.Value;
            string completePath = Server.MapPath(FolderPath + filename);
            //File.Delete(completePath);
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
    void crdtincentive()
    {
        try
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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
    #endregion

    protected void LnkUpAUTHORIZEDFILE_Click(object sender, EventArgs e)
    {
        try
        {
            if (FlupAUTHORIZEDFILE.HasFile)
            {
                string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                string strFolderName = "../incentives/Files/InctBasicDoc/";
                UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, strFolderName, "pdf", lnkAUTHORIZEDFILE);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkDelAUTHORIZEDFILE_Click(object sender, EventArgs e)
    {
        try
        {
            string strFolderName = "../incentives/Files/InctBasicDoc";
            UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
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
    #region viewdetail
    public void PrepopulateData(int id)
    {
        try
        {
            Grd_TL.DataSource = null;
            Grd_TL.DataBind();
            Grd_WC.DataSource = null;
            Grd_WC.DataBind();
            Grd_Production_After.DataSource = null;
            Grd_Production_After.DataBind();
            Grd_Production_Before.DataSource = null;
            Grd_Production_Before.DataBind();
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

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    public void PrepopulateDataPlus(int id)
    {
        DataSet dslivePre = IncentiveManager.PostpopulateDataPLUS(id);
        DataTable dtBank = dslivePre.Tables[0];////////////industry panel
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
            hdnBank.Value = dtBank.Rows[0]["vchBankDoc"].ToString(); /////AUTHORIZEDFILE file upload
            hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBank.Rows[0]["vchBankDoc"].ToString();
            hypBank.Visible = true;
            lnkBankDelete.Visible = true;
            //lblOrgDocument.Visible = true;
            fuBank.Enabled = false;
        }
    }
    public void PostpopulateData(int id)
    {
        try
        {
            Grd_TL.DataSource = null;
            Grd_TL.DataBind();
            Grd_WC.DataSource = null;
            Grd_WC.DataBind();
            Grd_Production_After.DataSource = null;
            Grd_Production_After.DataBind();
            Grd_Production_Before.DataSource = null;
            Grd_Production_Before.DataBind();
            DataSet dslivePre = IncentiveManager.PostpopulateDataSPM(id);
            DataTable dtindustryPre = dslivePre.Tables[0];////////////industry panel
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment

            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////production & employment Before
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////production & employment After
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////investment details
            DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////Means of Finance Term Loan
            DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Means of Finance Working Loan
            DataTable dtavail = dslivePre.Tables[8];///////////Avail Details
            DataTable dtavailgrd1 = dslivePre.Tables[9];///////////Avail Details
            DataTable dtBank = dslivePre.Tables[10];///////////Avail Details

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

                //DdlGender.SelectedValue = dtindustryPost.Rows[0]["vchManagingPartnerGender"].ToString();
                //TxtApplicantName.Text = dtindustryPost.Rows[0]["vchManagingPartnerName"].ToString(); 

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
                /////---------------------------------individual part------------
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
                if (dtindustryPre.Rows[0]["INTAPPLYBY"].ToString() != "0")
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
                //extra
                if (dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString() != "")
                {
                    //hdnDocfirstinvestment.Value = dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString(); /////AUTHORIZEDFILE file upload
                    //hypDocfirstinvestment.NavigateUrl = "../incentives/Files/investment/" + dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString();
                    //hypDocfirstinvestment.Visible = true;
                    //lnkDocfirstinvestmentDelete.Visible = true;
                    ////lblOrgDocument.Visible = true;
                    //fileDocfirstinvestment.Enabled = false;


                    FileVisibilty(hdnDocfirstinvestment, hypDocfirstinvestment, lnkDocfirstinvestmentDelete, fileDocfirstinvestment, dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString(), "investment", lnkDocfirstinvestmentUpload);

                }
                if (dtInvestmentPre.Rows[0]["vchNewBillDoc"].ToString() != "")
                {

                    //hdnDocfirstinvestment2.Value = dtInvestmentPre.Rows[0]["vchNewBillDoc"].ToString(); /////AUTHORIZEDFILE file upload
                    //hypDocfirstinvestment2.NavigateUrl = "../incentives/Files/investment/" + dtInvestmentPre.Rows[0]["vchNewBillDoc"].ToString();
                    //hypDocfirstinvestment2.Visible = true;
                    //lnkDocfirstinvestmentDelete2.Visible = true;
                    ////lblOrgDocument.Visible = true;
                    //fileDocfirstinvestment2.Enabled = false;


                    FileVisibilty(hdnDocfirstinvestment2, hypDocfirstinvestment2, lnkDocfirstinvestmentDelete2, fileDocfirstinvestment2, dtInvestmentPre.Rows[0]["vchNewBillDoc"].ToString(), "investment", lnkDocfirstinvestmentUpload2);
                }
                if (dtInvestmentPre.Rows[0]["vchSecHandDoc"].ToString() != "")
                {
                    //hdnDocSecondinvestment.Value = dtInvestmentPre.Rows[0]["vchSecHandDoc"].ToString(); /////AUTHORIZEDFILE file upload
                    //hypDocSecondinvestment.NavigateUrl = "../incentives/Files/investment/" + dtInvestmentPre.Rows[0]["vchSecHandDoc"].ToString();
                    //hypDocSecondinvestment.Visible = true;
                    //lnkDocSecondmentDelete.Visible = true;
                    ////lblOrgDocument.Visible = true;
                    //fileDocSecondinvestment.Enabled = false;

                    FileVisibilty(hdnDocSecondinvestment, hypDocSecondinvestment, lnkDocSecondmentDelete, fileDocSecondinvestment, dtInvestmentPre.Rows[0]["vchSecHandDoc"].ToString(), "investment", lnkDocSecondinvestmentUpload);

                }
                if (dtInvestmentPre.Rows[0]["vchSecHandBill"].ToString() != "")
                {

                    //hdnDocSecondinvestment2.Value = dtInvestmentPre.Rows[0]["vchSecHandBill"].ToString(); /////AUTHORIZEDFILE file upload
                    //hypDocSecondinvestment2.NavigateUrl = "../incentives/Files/investment/" + dtInvestmentPre.Rows[0]["vchSecHandBill"].ToString();
                    //hypDocSecondinvestment2.Visible = true;
                    //lnkDocSecondmentDelete2.Visible = true;
                    ////lblOrgDocument.Visible = true;
                    //fileDocSecondinvestment2.Enabled = false;

                    FileVisibilty(hdnDocSecondinvestment2, hypDocSecondinvestment2, lnkDocSecondmentDelete2, fileDocSecondinvestment2, dtInvestmentPre.Rows[0]["vchSecHandBill"].ToString(), "investment", lnkDocSecondinvestmentUpload2);
                }
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

            #region Avail Detail
            if (dtavail.Rows.Count > 0)
            {
                RadBtn_Availed_Earlier.SelectedValue = dtavail.Rows[0]["intNeverAvailedPrior"].ToString();
                txtreimamt.Text = dtavail.Rows[0]["decClaimReimbursement"].ToString();
                if (dtavail.Rows[0]["intNeverAvailedPrior"].ToString() == "1")
                {
                    txtdiffclaimamt.Text = dtavail.Rows[0]["decClaimExempted"].ToString();

                    if (dtavail.Rows[0]["VchSanctionDoc"].ToString() != "")
                    {
                        FileVisibilty(Hid_Asst_Sanc_File_Name, Hyp_View_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, dtavail.Rows[0]["VchSanctionDoc"].ToString(), "AvailDetails", LnkBtn_Upload_Asst_Sanc_Doc);
                        //FileVisibilty(Hid_Asst_Sanc_File_Name, Hyp_View_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, dtavail.Rows[0]["VchSanctionDoc"].ToString(), "AvailDetails/" + Convert.ToString(Session["investorid"]) + "/", LnkBtn_Upload_Asst_Sanc_Doc);
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
                            //drassistant["vchsacdat"] = RetDateFrmDB(dravgr1["dtmAvailedDate"].ToString());
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
                    if (dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString() != "")
                    {
                        FileVisibilty(Hid_Undertaking_File_Name, Hyp_View_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, FU_Undertaking_Doc, dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString(), "AvailDetails", LnkBtn_Upload_Undertaking_Doc);
                        //FileVisibilty(Hid_Undertaking_File_Name, Hyp_View_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, FU_Undertaking_Doc, dtavail.Rows[0]["vchUndertakingSubsidyDoc"].ToString(), "AvailDetails/" + Convert.ToString(Session["investorid"]) + "/", LnkBtn_Upload_Undertaking_Doc);
                    }
                }
            }

            #endregion
            #region "Bank Details"
            if (dtBank.Rows.Count > 0)
            {
                txtAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
                txtBnkNm.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
                txtBranch.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
                txtIFSC.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
                txtMICRNo.Text = dtBank.Rows[0]["VCHMICR"].ToString();
                if (dtBank.Rows[0]["vchBankDoc"].ToString() != "")
                {
                    hdnBank.Value = dtBank.Rows[0]["vchBankDoc"].ToString(); /////AUTHORIZEDFILE file upload
                    hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBank.Rows[0]["vchBankDoc"].ToString();
                    hypBank.Visible = true;
                    lnkBankDelete.Visible = true;
                    //lblOrgDocument.Visible = true;
                    fuBank.Enabled = false;
                    lnkBankUpload.Visible = false;
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
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
                //lblOrgDocument.Visible = true;
                FluCtrl.Enabled = false;
                lnkBtnUpload.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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
}
