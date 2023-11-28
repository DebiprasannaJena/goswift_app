using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;

public partial class incentives_EmployeementCostSubsidy : SessionCheck
{
    System.Timers.Timer myTimer = new System.Timers.Timer();
    Incentive objincUnit = new Incentive();
    string SessionName = "Temp_UniqueNo_9_";///// value from UniqueNo Querystring

    string MsgTitle = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    protected void Page_Load(object sender, EventArgs e)
    {
        TxtESIEPFDate.Attributes.Add("readonly", "readonly");///// Production Analysis 
        TxtEPFregDate.Attributes.Add("readonly", "readonly");///// Production Analysis 
        txtsacdat.Attributes.Add("readonly", "readonly");///// Avail Claim Document
        //TxtESIEPFDate.Attributes.Remove("class");
        ////ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>calCheck(Div2);</script>", false);//// for making date field disable
        if (!IsPostBack)
        {
            fillSalutation();
            GetHeaderName();
            BindFYearLabel();
            crdtincentive();//// shud be at top of all method
            //// DeleteTempAdditionalDoc();//-- to delete temp saved additional doc file
            ////FillApplyBy();

            #region Fill for update
            if (Convert.ToString(Session["ApplySource"]) == "1")
            {
                PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
                PrepopulateDataPlus(Convert.ToInt16(Session["InvestorId"]));
            }
            else
            {
                PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            }
            #endregion
        }
    }

    #region Save Option
    #region Industry Unit Details

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

            objincUnit.IndsutUnitMstDet.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue == "0" ? "1" : DdlGender.SelectedValue);
            objincUnit.IndsutUnitMstDet.APPLICANTNAME_IND = TxtApplicantName.Text.Trim();
            objincUnit.IndsutUnitMstDet.APPLYBY_IND = radApplyBy.SelectedIndex == -1 ? "0" : radApplyBy.SelectedValue;
            objincUnit.IndsutUnitMstDet.AADHAARNO_IND = TxtAdhaar1.Text.Trim();// +TxtAdhaar2.Text.Trim() + TxtAdhaar3.Text.Trim();// radApplyBy.SelectedValue == "1" ? TxtAdhaar1.Text.Trim() + TxtAdhaar2.Text.Trim() + TxtAdhaar3.Text.Trim() : "";
            objincUnit.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;// radApplyBy.SelectedValue == "2" ? hdnAUTHORIZEDFILE.Value : "";
            //objincUnit.IndsutUnitMstDet.AUTHORIZEDFILECODE_IND = radAuthorizing.SelectedIndex == -1 ? "0" : radAuthorizing.SelectedValue;
        }
        catch (Exception ex)
        {
            resdt = ex.Message;
            Util.LogError(ex, "Incentive");
        }


        return resdt;
    }

    protected void LnkUpAUTHORIZEDFILE_Click(object sender, EventArgs e)
    {
        try
        {
            if (FlupAUTHORIZEDFILE.HasFile)
            {
                string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                string strFolderName = "InctBasicDoc";
                UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, HypViewAUTHORIZEDFILE, LblAUTHORIZEDFILE, LnkDelAUTHORIZEDFILE, strFolderName, lnkUpAUTHORIZEDFILE);
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
            string strFolderName = "InctBasicDoc";// "IndustryUnit";
            UpdFileRemove(hdnAUTHORIZEDFILE, lnkUpAUTHORIZEDFILE, LnkDelAUTHORIZEDFILE, HypViewAUTHORIZEDFILE, LblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #endregion
    #region Production & Employment Cost Subsidy
    public void SaveProductAnalysis(int save)
    {
        Production objProd = new Production();
        List<ProductionItem> lists = new List<ProductionItem>();

        objincUnit.ProdEmpDet = objProd;
        try
        {


            objProd.ESI = RadSI.Checked == true ? 1 : 0;
            objProd.EPF = RadEPF.Checked == true ? 1 : 0;
            objProd.ESIOREPFREGNO = TxtESIRegNo.Text.Trim();
            objProd.ESIAUTHNAME = TxtESIAuthName.Text.Trim();
            objProd.ESIOREPF = TxtESIEPFDate.Text.Trim();
            objProd.DELAYREASON = TxtReasonDelay.Text.Trim();

            objProd.EPFREGNO = TxtEPFRegNo.Text.Trim();
            objProd.EPFREGDATE = TxtEPFregDate.Text.Trim();
            objProd.EPFAUTHNAME = TxtEPFAuthName.Text.Trim();

            #region FileUpload & Save

            objProd.DPRDOC = HdnDPR.Value.ToString();

            objProd.REGATTACHDOC = HdnRegAttachment.Value.ToString();
            objProd.EPFREGATTACHDOC = HdnEPFRegAttachment.Value.ToString();


            ///////----------------------Excel File Upload----------------
            objProd.PAYROLLDOC = hdnPayrollDoc.Value.ToString();
            objProd.ESIOREPFDOC = HdnContESI.Value.ToString();

            objProd.ESIEPFCOMPDOC = hdnESIComp.Value.ToString();

            ///////----------------------Excel File Upload----------------
            objProd.DOCUMENTINSUPPORT = HdnDelayDoc.Value.ToString();
            #endregion


            if (save == 1)
            {
                objProd.STATUSFORDFTAPPLY = 1;
            }

            objincUnit.ProdEmpDet = objProd;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objProd = null;
            lists = null;
        }
    }
    int hdnflag = 0;
    #region Fileupload/Delete Events
    protected void LnkUpDPR_Click(object sender, EventArgs e)
    {
        try
        {
            if (FluDPR.HasFile)
            {
                string strFileName = "DPR" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Production";
                UploadDocument(FluDPR, HdnDPR, strFileName, HypViewDPR, LblDPR, LnkDelDPR, strFolderName, LnkUpDPR);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkDelDPR_Click(object sender, EventArgs e)
    {
        try
        {
            string strFolderName = "Production";
            UpdFileRemove(HdnDPR, LnkUpPayrollDoc, LnkDelDPR, HypViewDPR, LblDPR, FluDPR, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    protected void LnkUpPayrollDoc_Click(object sender, EventArgs e)
    {

        try
        {
            if (FluPayrollDoc.HasFile)
            {
                string strFileName = "PayrollDoc" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Production";
                UploadDocument(FluPayrollDoc, hdnPayrollDoc, strFileName, HypViewPayrollDoc, LblPayrollDoc, LnkDelPayrollDoc, strFolderName, LnkUpPayrollDoc);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkDelPayrollDoc_Click(object sender, EventArgs e)
    {
        try
        {
            string strFolderName = "Production";
            UpdFileRemove(hdnPayrollDoc, LnkUpPayrollDoc, LnkDelPayrollDoc, HypViewPayrollDoc, LblPayrollDoc, FluPayrollDoc, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkUpContESI_Click(object sender, EventArgs e)///////// employee contribution
    {

        try
        {
            if (FluContESI.HasFile)
            {
                string strFileName = "CompESI" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Production";
                UploadDocument(FluContESI, HdnContESI, strFileName, HypViewContESI, LblContESI, LnkDelContESI, strFolderName, LnkUpContESI);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkDelContESI_Click(object sender, EventArgs e)
    {
        try
        {
            string strFolderName = "Production";
            UpdFileRemove(HdnContESI, LnkUpContESI, LnkDelContESI, HypViewContESI, LblContESI, FluContESI, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkUpESIComp_Click(object sender, EventArgs e)///// company contribution
    {

        try
        {
            if (FluESIComp.HasFile)
            {
                string strFileName = "ContESI" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Production";
                UploadDocument(FluESIComp, hdnESIComp, strFileName, HypViewESIComp, LblESIComp, LnkDelESIComp, strFolderName, LnkUpESIComp);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    protected void LnkDelESIComp_Click(object sender, EventArgs e)
    {

        try
        {
            string strFolderName = "Production";
            UpdFileRemove(hdnESIComp, LnkUpESIComp, LnkDelESIComp, HypViewESIComp, LblESIComp, FluESIComp, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkUpDelayDoc_Click(object sender, EventArgs e)
    {

        try
        {
            if (FluDelayDoc.HasFile)
            {
                string strFileName = "DelayReasonDoc" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Production";
                UploadDocument(FluDelayDoc, HdnDelayDoc, strFileName, HypViewDelayDoc, LblDelayDoc, LnkDelDelayDoc, strFolderName, LnkUpDelayDoc);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkDelDelayDoc_Click(object sender, EventArgs e)
    {
        try
        {
            string strFolderName = "Production";
            UpdFileRemove(HdnDelayDoc, LnkUpDelayDoc, LnkDelDelayDoc, HypViewDelayDoc, LblDelayDoc, FluDelayDoc, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkUpRegAttachment_Click(object sender, EventArgs e)
    {
        try
        {
            if (FluRegAttachment.HasFile)
            {
                string strFileName = "ESIReg_Attachment" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Production";
                UploadDocument(FluRegAttachment, HdnRegAttachment, strFileName, HypViewRegAttachment, LblRegAttachment, LnkDelRegAttachment, strFolderName, LnkUpRegAttachment);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkDelRegAttachment_Click(object sender, EventArgs e)
    {
        try
        {
            string strFolderName = "Production";

            UpdFileRemove(HdnRegAttachment, LnkUpRegAttachment, LnkDelRegAttachment, HypViewRegAttachment, LblRegAttachment, FluRegAttachment, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
            throw;
        }

    }
    protected void LnkUpEPFRegAttachment_Click(object sender, EventArgs e)
    {
        try
        {
            if (FluEPFRegAttachment.HasFile)
            {
                string strFileName = "EPFReg_Attachment" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Production";
                UploadDocument(FluEPFRegAttachment, HdnEPFRegAttachment, strFileName, HypViewEPFRegAttachment, LblEPFRegAttachment, LnkDelEPFRegAttachment, strFolderName, LnkUpEPFRegAttachment);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    protected void LnkDelEPFRegAttachment_Click(object sender, EventArgs e)
    {
        try
        {
            string strFolderName = "Production";

            UpdFileRemove(HdnEPFRegAttachment, LnkUpEPFRegAttachment, LnkDelEPFRegAttachment, HypViewEPFRegAttachment, LblEPFRegAttachment, FluEPFRegAttachment, strFolderName);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
            throw;
        }
    }
    #endregion
    #endregion
    #region Bank Details
    public void SaveBankDetails()
    {
        try
        {
            BankDetails objBank = new BankDetails();
            objBank.BankName = txtBnkNm.Text;
            objBank.BranchName = txtBranch.Text;
            objBank.IFSCNo = txtIFSC.Text;
            objBank.AccountNo = txtAccNo.Text;
            objBank.MICRNo = txtMICRNo.Text;
            if (hdnBank.Value != "")
            {
                objBank.BankDoc = hdnBank.Value;
            }
            objincUnit.BankDet = objBank;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }

    protected void lnkBankUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (fuBank.HasFile)
            {
                string strFileName = "Bank" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "Bank";
                UploadDocument(fuBank, hdnBank, strFileName, hypBank, lblBank, lnkBankDelete, strFolderName, lnkBankUpload);
                HdnCssClass.Value = "BankDetails";
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void lnkBankDelete_Click(object sender, EventArgs e)
    {
        try
        {
            string strFolderName = "Bank";
            UpdFileRemove(hdnBank, lnkBankUpload, lnkBankDelete, hypBank, lblBank, fuBank, strFolderName);
            HdnCssClass.Value = "BankDetails";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #endregion
    #region Additional Document
    /// <summary>
    /// Function to add Additional documents
    /// </summary>
    protected void AdditionalDocument()
    {
        try
        {
            if (hdnIsOsPCBDownloaded.Value == "1")
            {
                SaveServiceDoc(enServiceDocType.OSPCB, D275, "AdditionalDocument");
            }
            if (hdnBoilerDownloaded.Value == "1")
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
    protected void myTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        myTimer.Stop();
        DeleteTempAdditionalDoc();
    }
    protected void DeleteTempAdditionalDoc()
    {
        try
        {
            string gFilePath2 = "../incentives/Files/AdditionalDocument/";

            string DeleteThis = SessionName;
            string[] Files = Directory.GetFiles(Server.MapPath(gFilePath2));

            foreach (string file in Files)
            {
                if (file.ToUpper().Contains(DeleteThis.ToUpper()))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        Util.LogError(ex, "Incentive");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void lnkUValidStatutary_click(object sender, EventArgs e)
    {
        try
        {
            if (flValidStatutary.HasFile)
            {

                string strFileName = "OSPCB" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "AdditionalDocument";
                UploadDocument(flValidStatutary, D275, strFileName, hypValidStatutary, lblValidStatutary, lnkDValidStatutary, strFolderName, lnkUValidStatutary);
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
            string strFolderName = "AdditionalDocument";
            UpdFileRemove(D275, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, lblValidStatutary, flValidStatutary, strFolderName);
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

    protected void lnkUDelay_Click(object sender, EventArgs e)
    {
        try
        {
            if (flDelay.HasFile)
            {
                string strFileName = "Sector_Relevant" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "AdditionalDocument";
                UploadDocument(flDelay, D274, strFileName, hypDelay, lblDelay, lnkDDelay, strFolderName, lnkUDelay);
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
            string strFolderName = "AdditionalDocument";
            UpdFileRemove(D274, lnkUDelay, lnkDDelay, hypDelay, lblDelay, flDelay, strFolderName);

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
    #region Avail Details

    protected void AvailClaimSave()
    {
        try
        {
            #region Availed detail

            AvailDetails objAvailDetails = new AvailDetails();
            List<Assistance> listIncentiveAvailed = new List<Assistance>();
            Assistance objIncentiveAvailed = new Assistance();

            objAvailDetails.NeverAvailedPrior = Convert.ToInt16(RadBtn_Availed_Earlier.SelectedValue);
            objAvailDetails.SubsidyAvailed = 0;

            if (RadBtn_Availed_Earlier.SelectedValue == "1")
            {
                objAvailDetails.ClaimtExempted = Convert.ToDouble(txtdiffclaimamt.Text.Trim() == "" ? "0" : txtdiffclaimamt.Text.Trim());
                objAvailDetails.SanctionOrderDoc = Hid_Asst_Sanc_File_Name.Value;
            }
            else
            {
                objAvailDetails.UndertakingSubsidyDoc = Hid_Undertaking_File_Name.Value;
            }

            objAvailDetails.ClaimReimbursement = Convert.ToDouble(txtreimamt.Text.Trim() == "" ? "0" : txtreimamt.Text.Trim());
            objAvailDetails.decClaimReimbursementEPF = Convert.ToDecimal(txtreimamtEPF.Text.Trim() == "" ? "0" : txtreimamtEPF.Text.Trim());

            DataTable dtincentive = new DataTable();
            dtincentive = (DataTable)ViewState["dtincentive"];

            if (dtincentive.Rows.Count > 0)
            {
                foreach (DataRow dr in dtincentive.Rows)
                {
                    objIncentiveAvailed = new Assistance();

                    objIncentiveAvailed.InstitutionName = dr["vchagency"].ToString();
                    if (dr["vchavilamt"].ToString().Trim() != "")
                    {
                        objIncentiveAvailed.AmountAvailed = Convert.ToDouble(dr["vchavilamt"].ToString());
                    }
                    else
                    {
                        objIncentiveAvailed.AmountAvailed = 0;
                    }

                    if (dr["vchsacamt"].ToString().Trim() != "")
                    {
                        objIncentiveAvailed.SanctionedAmount = Convert.ToDouble(dr["vchsacamt"].ToString());
                    }
                    else
                    {
                        objIncentiveAvailed.SanctionedAmount = 0;
                    }

                    if (dr["vchsacdat"].ToString().Trim() != "")
                    {
                        objIncentiveAvailed.AvailedDate = Convert.ToDateTime(dr["vchsacdat"].ToString().Trim());
                    }

                    objIncentiveAvailed.SanctionOrderNo = dr["vchsacord"].ToString();

                    listIncentiveAvailed.Add(objIncentiveAvailed);
                }
            }

            objAvailDetails.IncentiveAvailed = listIncentiveAvailed;
            objincUnit.AvailDet = objAvailDetails;

            #endregion
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void crdtincentive()
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
    protected void LinkButton41_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtincentive = new DataTable();
            dtincentive = (DataTable)ViewState["dtincentive"];
            DataRow dr = dtincentive.NewRow();

            dr["vchagency"] = txtagency.Text.Trim();
            dr["vchsacamt"] = txtsacamt.Text.Trim() == "" ? "0" : txtsacamt.Text.Trim();
            dr["vchsacord"] = txtsacord.Text.Trim();
            dr["vchsacdat"] = txtsacdat.Text.Trim();
            dr["vchavilamt"] = txtavilamt.Text.Trim() == "" ? "0" : txtavilamt.Text.Trim();

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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void grdAssistanceDetailsAD_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void LnkBtn_Upload_Asst_Sanc_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            if (FU_Asst_Sanc_Doc.HasFile)
            {
                string strFileName = "ASSTSANC" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "AvailDetails";
                UploadDocument(FU_Asst_Sanc_Doc, Hid_Asst_Sanc_File_Name, strFileName, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, strFolderName, LnkBtn_Upload_Asst_Sanc_Doc);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkBtn_Delete_Asst_Sanc_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkBtn_Delete_Asst_Sanc_Doc.ID))
            {
                string strFolderName = "AvailDetails";
                UpdFileRemove(Hid_Asst_Sanc_File_Name, LnkBtn_Upload_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, Hyp_View_Asst_Sanc_Doc, Lbl_Msg_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, strFolderName);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void LnkBtn_Upload_Undertaking_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            if (FU_Undertaking_Doc.HasFile)
            {
                string strFileName = "UND" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFolderName = "AvailDetails";
                UploadDocument(FU_Undertaking_Doc, Hid_Undertaking_File_Name, strFileName, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, strFolderName, LnkBtn_Upload_Undertaking_Doc);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    protected void LnkBtn_Delete_Undertaking_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkBtn_Delete_Undertaking_Doc.ID))
            {
                string strFolderName = "AvailDetails";
                UpdFileRemove(Hid_Undertaking_File_Name, LnkBtn_Upload_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, Hyp_View_Undertaking_Doc, Lbl_Msg_Undertaking_Doc, FU_Undertaking_Doc, strFolderName);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    #endregion
    #endregion

    #region FileUPloadControls
    /// <summary>
    /// INSERT INTO DOC MASTER
    /// </summary>
    public void FileUploadControls()
    {
        try
        {
            List<lstFileUpload> fileList = new List<lstFileUpload>();

            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = "D132",
                vchFileName = HdnDPR.Value,
                vchFilePath = "../incentives/Files/Production/"
            });
            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = "D268",
                vchFileName = hdnPayrollDoc.Value,
                vchFilePath = "../incentives/Files/Production/"
            });

            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = "D269",
                vchFileName = Hid_Asst_Sanc_File_Name.Value,
                vchFilePath = "../incentives/Files/AvailDetails/"
            });
            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = "D269",
                vchFileName = Hid_Asst_Sanc_File_Name.Value,
                vchFilePath = "../incentives/Files/AvailDetails/"
            });//Bank Details
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
            ///////---- additional document
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
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }

    }
    #endregion

    #region viewdetail
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

            //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();
            #region IndustrailUnit

            if (dtindustryPre.Rows.Count > 0)
            {

                try
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
                    if (Lbl_Pioneer_Doc_Name.Text.Trim() == "")
                        div_Pioneer_Doc_Name.Visible = false;


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
                catch (Exception ex)
                {
                    Util.LogError(ex, "Incentive");
                }
                //dtindustryPre.Rows[0]["intCreatedBy"].ToString();
            }
            #endregion

            #region Production
            if (dtProductionPre.Rows.Count > 0)
            {
                try
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

                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "Incentive");
                }
                //dtProductionPre.Rows[0]["intCreatedBy"].ToString();
            }
            #endregion

            #region Investment

            if (dtInvestmentPre.Rows.Count > 0)
            {
                try
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
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "Incentive");
                }
                //dtInvestmentPre.Rows[0]["intCreatedBy"].ToString();
            }
            #endregion

            #region MEANS OF FINANCE
            if (dtMeansFinancePre.Rows.Count > 0)
            {
                try
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
                catch (Exception ex)
                {
                    Util.LogError(ex, "Incentive");
                }
            }
            #endregion

            BindDocFromService(enServiceDocType.OSPCB, D275, flValidStatutary, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, hdnIsOsPCBDownloaded);
            BindDocFromService(enServiceDocType.Boiler, D280, flCleanApproveAuthority, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, hdnBoilerDownloaded);
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
            DivCheckList.Visible = true;
            DataSet ds1 = new DataSet();
            DataTable dt = new DataTable();
            DataSet dslivePre = IncentiveManager.PostpopulateDataCostSubsidy(id);
            if (dslivePre.Tables.Count > 0)
            {
                DataTable dtindustryPost = dslivePre.Tables[0];////////////industry panel
                DataTable dtProductionpost = dslivePre.Tables[1];///////////production & employment

                DataTable dtProductionDetBefPost = dslivePre.Tables[2];///////////production & employment Before
                DataTable dtProductionDetAftPost = dslivePre.Tables[3];///////////production & employment After
                DataTable dtInvestmentPost = dslivePre.Tables[4];///////////investment details
                DataTable dtMeansFinancePost = dslivePre.Tables[5];///////////Means of Finance
                DataTable dtMoFTermLoanPost = dslivePre.Tables[6];///////////Means of Finance Term Loan
                DataTable dtMoFWorkingLoanPost = dslivePre.Tables[7];///////////Means of Finance Working Loan

                DataTable dtAvailDetPost = dslivePre.Tables[8];///////////Avail Details Master
                DataTable dtAssistancePost = dslivePre.Tables[9];///////////Avail Details Assistance Tran Table
                DataTable dtBankPost = dslivePre.Tables[10];///////////Avail Details Assistance Tran Table
                DataTable dtAddDocMastPost = dslivePre.Tables[11];///////////Addititional Document Master Table
                DataTable dtAddDocTranPost = dslivePre.Tables[12];///////////Addititional Document Tran Table

                //dtindustryPre.Rows[0]["intIndustrailUnit"].ToString();

                #region IndustrialUnit
                try
                {
                    if (dtindustryPost.Rows.Count > 0)
                    {
                        lbl_EnterPrise_Name.Text = dtindustryPost.Rows[0]["vchEnterpriseName"].ToString();
                        //dtindustryPost.Rows[0]["intOrganisationType"].ToString();	
                        ds1 = IncentiveManager.dynamic_name_doc_bind();
                        ds1.Tables[1].DefaultView.RowFilter = "slno = '" + dtindustryPost.Rows[0]["intUnitType"].ToString() + "'";
                        ds1.Tables[0].DefaultView.RowFilter = "slno = '" + dtindustryPost.Rows[0]["intOrganisationType"].ToString() + "'";
                        dt = (ds1.Tables[0].DefaultView).ToTable();
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



                        lbl_Org_Type.Text = dtindustryPost.Rows[0]["OrgTypename"].ToString();
                        lbl_Industry_Address.Text = dtindustryPost.Rows[0]["vchIndustryAddress"].ToString();
                        //dtindustryPost.Rows[0]["intUnitCat"].ToString();		
                        lbl_Unit_Cat.Text = dtindustryPost.Rows[0]["Unitcategoryname"].ToString();
                        Lbl_Pioneer_Doc_Name.Text = dtindustryPost.Rows[0]["VCHPIONEERCERTIFICATEDOCCODEName"].ToString();
                        if (Lbl_Pioneer_Doc_Name.Text.Trim() == "")
                            div_Pioneer_Doc_Name.Visible = false;


                        dt = (ds1.Tables[1].DefaultView).ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            string strDocType = dt.Rows[0]["vchDocumentType"].ToString();
                            if (strDocType != "")
                            {
                                Div_Unit_Type_Doc.Visible = true;
                                Lbl_Unit_Type_Doc_Name.Text = dt.Rows[0]["vchDocumentTypeName"].ToString();
                                Hid_Unit_Type_Doc_Code.Value = dt.Rows[0]["vchDocumentType"].ToString();

                                Hyp_View_Unit_Type_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPost.Rows[0]["vchUnitTypeDoc"].ToString();
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




                        //dtindustryPost.Rows[0]["intUnitType"].ToString();			
                        lbl_Unit_Type.Text = dtindustryPost.Rows[0]["UnitTypename"].ToString();
                        //dtindustryPost.Rows[0]["vchDocCode"].ToString();	

                        //dtindustryPost.Rows[0]["vchUnitTypeDoc"].ToString();

                        if (dtindustryPost.Rows[0]["intPriority"].ToString() == "1")
                        {
                            lblIs_Priority.Text = "Yes";
                            Pioneersec.Visible = true;

                        }
                        else
                        {
                            lblIs_Priority.Text = "No";
                            Pioneersec.Visible = false;

                        }
                        if (dtindustryPost.Rows[0]["intPioneer"].ToString() == "1")
                        {
                            lblIs_Is_Pioneer.Text = "Yes";

                        }
                        else
                        {
                            lblIs_Is_Pioneer.Text = "No";

                        }


                        Hyp_View_Pioneer_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPost.Rows[0]["vchPioneerCertificate"].ToString();



                        lbl_Regd_Office_Address.Text = dtindustryPost.Rows[0]["vchRegisteredOfcAddress"].ToString();
                        //dtindustryPost.Rows[0]["vchManagingPartnerGender"].ToString();	

                        lbl_Gender_Partner.Text = dtindustryPost.Rows[0]["GenderType"].ToString() + " " + dtindustryPost.Rows[0]["vchManagingPartnerName"].ToString();

                        ////----- assigned in below region-individual part---------------
                        //DdlGender.SelectedValue = dtindustryPost.Rows[0]["vchManagingPartnerGender"].ToString();
                        //TxtApplicantName.Text = dtindustryPost.Rows[0]["vchManagingPartnerName"].ToString(); 
                        Hyp_View_Org_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtindustryPost.Rows[0]["vchCertOfRegdDocFileName"].ToString();


                        lbl_EIN_IL_NO.Text = dtindustryPost.Rows[0]["vchEINNO"].ToString();
                        lbl_EIN_IL_Date.Text = dtindustryPost.Rows[0]["dtmEIN"].ToString();
                        lbl_PC_No.Text = dtindustryPost.Rows[0]["vchPcNo"].ToString();
                        lbl_Prod_Comm_Date_Before.Text = dtindustryPost.Rows[0]["dtmProdCommBefore"].ToString();
                        lbl_PC_Issue_Date_Before.Text = dtindustryPost.Rows[0]["dtmPCIssueDateBefore"].ToString();


                        lbl_pcno_befor.Text = dtindustryPost.Rows[0]["vchpcnobefore"].ToString();
                        lblGstin.Text = dtindustryPost.Rows[0]["VCHGSTIN"].ToString();

                        if (dtindustryPost.Rows[0]["projectType"].ToString() == "1")
                        {
                            Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPost.Rows[0]["vchappnobef"].ToString();
                        }
                        else if (dtindustryPost.Rows[0]["projectType"].ToString() == "2")
                        {
                            Hyp_View_Prod_Comm_Before_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPost.Rows[0]["vchappnobef"].ToString();
                        }

                        if (dtindustryPost.Rows[0]["dtmProdCommBefore"].ToString() != "")
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

                        lbl_Prod_Comm_Date_After.Text = dtindustryPost.Rows[0]["dtmProdCommAfter"].ToString();
                        lbl_PC_Issue_Date_After.Text = dtindustryPost.Rows[0]["dtmPCIssueDateAfter"].ToString();


                        if (dtindustryPost.Rows[0]["projectType"].ToString() == "1")
                        {
                            Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PCPrintLarge.aspx?id=" + dtindustryPost.Rows[0]["vchappnoAft"].ToString();
                        }
                        else if (dtindustryPost.Rows[0]["projectType"].ToString() == "2")
                        {
                            Hyp_View_Prod_Comm_After_Doc.NavigateUrl = "~/Portal/Incentive/PcPrint.aspx?id=" + dtindustryPost.Rows[0]["vchappnoAft"].ToString();
                        }

                        ////dtindustryPost.Rows[0]["intDistrictCode"].ToString();			
                        lbl_District.Text = dtindustryPost.Rows[0]["distname"].ToString();
                        //dtindustryPost.Rows[0]["intSectorId"].ToString();			
                        lbl_Sector.Text = dtindustryPost.Rows[0]["sectorName"].ToString();
                        //dtindustryPost.Rows[0]["intSubSectorId"].ToString();			
                        lbl_Sub_Sector.Text = dtindustryPost.Rows[0]["SubsectorName"].ToString();
                        Lbl_Derived_Sector.Text = dtindustryPost.Rows[0]["vchDerivedSector"].ToString();
                        //dtindustryPost.Rows[0]["bitSectoralPolicy"].ToString();

                        if (dtindustryPost.Rows[0]["bitPriorityIPR"].ToString() == "1")
                        {

                            lbl_Sectoral.Text = "Yes";
                        }
                        else
                        {
                            lbl_Sectoral.Text = "No";
                        }


                        /////---------------------------------individual part------------


                        DdlGender.SelectedValue = dtindustryPost.Rows[0]["INTGENDER"].ToString();
                        TxtApplicantName.Text = dtindustryPost.Rows[0]["VCHAPPLICANTNAME"].ToString();
                        if (dtindustryPost.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                        {
                            if (dtindustryPost.Rows[0]["VCHAADHAARNO"].ToString().Trim().Length == 12)
                            {
                                TxtAdhaar1.Text = dtindustryPost.Rows[0]["VCHAADHAARNO"].ToString();
                            }
                        }
                        hdnAUTHORIZEDFILE.Value = dtindustryPost.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(); /////AUTHORIZEDFILE file upload

                        ///----------------------
                        if (dtindustryPost.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString() != "")
                        {
                            FileVisibilty(hdnAUTHORIZEDFILE, HypViewAUTHORIZEDFILE, LnkDelAUTHORIZEDFILE, FlupAUTHORIZEDFILE, dtindustryPost.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString(), "InctBasicDoc", lnkUpAUTHORIZEDFILE);

                        }

                        ///----------------------
                        if (dtindustryPost.Rows[0]["INTAPPLYBY"].ToString() != "0")
                        {
                            radApplyBy.SelectedValue = dtindustryPost.Rows[0]["INTAPPLYBY"].ToString();
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
                #endregion

                #region Production
                try
                {
                    if (dtProductionpost.Rows.Count > 0)
                    {
                        Grd_Production_Before.DataSource = dtProductionDetBefPost;
                        Grd_Production_Before.DataBind();
                        Grd_Production_After.DataSource = dtProductionDetAftPost;
                        Grd_Production_After.DataBind();


                        //dtProductionpost.Rows[0]["intProductionId"].ToString();
                        lbl_Direct_Emp_Before.Text = dtProductionpost.Rows[0]["intDirectEmpBefore"].ToString();
                        lbl_Contract_Emp_Before.Text = dtProductionpost.Rows[0]["intContractualEmpBefore"].ToString();
                        Hyp_View_Direct_Emp_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionpost.Rows[0]["VCHEMPDOC"].ToString();
                        lbl_Managarial_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDMANAGERIAL"].ToString();
                        lbl_Supervisor_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDSUPERVISORY"].ToString();
                        lbl_Skilled_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDSKILLED"].ToString();
                        lbl_Semi_Skilled_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDSEMISKILLED"].ToString();
                        lbl_Unskilled_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDUNSKILLED"].ToString();
                        lbl_Total_Emp_Before.Text = dtProductionpost.Rows[0]["INTPROPOSEDTOTAL"].ToString();
                        lbl_General_Before.Text = dtProductionpost.Rows[0]["intGeneralBefore"].ToString();
                        lbl_SC_Before.Text = dtProductionpost.Rows[0]["intSCBefore"].ToString();
                        lbl_ST_Before.Text = dtProductionpost.Rows[0]["intSTBefore"].ToString();
                        lbl_Total_Cast_Emp_Before.Text = dtProductionpost.Rows[0]["intTotalEmpCastBefore"].ToString();
                        lbl_Women_Before.Text = dtProductionpost.Rows[0]["intWomenBefore"].ToString();
                        lbl_PHD_Before.Text = dtProductionpost.Rows[0]["intDisabledBefore"].ToString();
                        lbl_Direct_Emp_After.Text = dtProductionpost.Rows[0]["intDirectEmpAfter"].ToString();
                        lbl_Contract_Emp_After.Text = dtProductionpost.Rows[0]["intContractualEmpAfter"].ToString();
                        //dtProductionpost.Rows[0]["vchEmpDocBeforeCode"].ToString();			
                        Lbl_Direct_Emp_Before_Doc_Name.Text = dtProductionpost.Rows[0]["vchEmpDocBeforeCodeName"].ToString();
                        Hyp_View_Direct_Emp_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtProductionpost.Rows[0]["vchEmpDocAfter"].ToString();
                        //dtProductionpost.Rows[0]["vchEmpDocAfterCode"].ToString();			
                        Lbl_Direct_Emp_After_Doc_Name.Text = dtProductionpost.Rows[0]["vchEmpDocAfterCodeName"].ToString();


                        lbl_Managarial_After.Text = dtProductionpost.Rows[0]["INTCURRENTMANAGERIAL"].ToString();
                        lbl_Supervisor_After.Text = dtProductionpost.Rows[0]["INTCURRENTSUPERVISORY"].ToString();
                        lbl_Skilled_After.Text = dtProductionpost.Rows[0]["INTCURRENTSKILLED"].ToString();
                        lbl_Semi_Skilled_After.Text = dtProductionpost.Rows[0]["INTCURRENTSEMISKILLED"].ToString();
                        lbl_Unskilled_After.Text = dtProductionpost.Rows[0]["INTCURRENTUNSKILLED"].ToString();
                        lbl_Total_Emp_After.Text = dtProductionpost.Rows[0]["INTCURRENTTOTAL"].ToString();
                        lbl_General_After.Text = dtProductionpost.Rows[0]["intGeneralAfter"].ToString();
                        lbl_SC_After.Text = dtProductionpost.Rows[0]["intSCAfter"].ToString();
                        lbl_ST_After.Text = dtProductionpost.Rows[0]["intSTAfter"].ToString();
                        lbl_Total_Cast_Emp_After.Text = dtProductionpost.Rows[0]["intTotalEmpCastAfter"].ToString();
                        lbl_Women_After.Text = dtProductionpost.Rows[0]["intWomenAfter"].ToString();
                        lbl_PHD_After.Text = dtProductionpost.Rows[0]["intDisabledAfter"].ToString();

                        //dtProductionpost.Rows[0]["intCreatedBy"].ToString();
                    }

                    ///////--------individual part---------------
                    RadSI.Checked = dtProductionpost.Rows[0]["BITESI"].ToString() == "1" ? true : false;
                    RadEPF.Checked = dtProductionpost.Rows[0]["BITEPF"].ToString() == "1" ? true : false;
                    TxtESIRegNo.Text = dtProductionpost.Rows[0]["VCHESIOREPFREGNO"].ToString();
                    TxtESIEPFDate.Text = dtProductionpost.Rows[0]["ESIOREPFDATE"].ToString();
                    TxtESIAuthName.Text = dtProductionpost.Rows[0]["VCHESIAUTHNAME"].ToString();
                    TxtReasonDelay.Text = dtProductionpost.Rows[0]["VCHDELAYREASON"].ToString();

                    TxtEPFRegNo.Text = dtProductionpost.Rows[0]["VCHEPFREGNO"].ToString();
                    TxtEPFregDate.Text = dtProductionpost.Rows[0]["DTMEPFREGDATE"].ToString();
                    TxtEPFAuthName.Text = dtProductionpost.Rows[0]["VCHEPFAUTHNAME"].ToString();


                    HdnDPR.Value = dtProductionpost.Rows[0]["VCHDPRDOC"].ToString();
                    FileVisibilty(HdnDPR, HypViewDPR, LnkDelDPR, FluDPR, dtProductionpost.Rows[0]["VCHDPRDOC"].ToString(), "Production", LnkUpDPR);

                    HdnRegAttachment.Value = dtProductionpost.Rows[0]["VCHREGATTACHDOC"].ToString();
                    if (dtProductionpost.Rows[0]["VCHREGATTACHDOC"].ToString() != "")
                        FileVisibilty(HdnRegAttachment, HypViewRegAttachment, LnkDelRegAttachment, FluRegAttachment, dtProductionpost.Rows[0]["VCHREGATTACHDOC"].ToString(), "Production", LnkUpRegAttachment);

                    HdnEPFRegAttachment.Value = dtProductionpost.Rows[0]["VCHEPFREGATTACHDOC"].ToString();
                    if (dtProductionpost.Rows[0]["VCHEPFREGATTACHDOC"].ToString() != "")
                        FileVisibilty(HdnEPFRegAttachment, HypViewEPFRegAttachment, LnkDelEPFRegAttachment, FluEPFRegAttachment, dtProductionpost.Rows[0]["VCHEPFREGATTACHDOC"].ToString(), "Production", LnkUpEPFRegAttachment);

                    hdnPayrollDoc.Value = dtProductionpost.Rows[0]["VCHPAYROLLDOC"].ToString();
                    if (dtProductionpost.Rows[0]["VCHPAYROLLDOC"].ToString() != "")
                        FileVisibilty(hdnPayrollDoc, HypViewPayrollDoc, LnkDelPayrollDoc, FluPayrollDoc, dtProductionpost.Rows[0]["VCHPAYROLLDOC"].ToString(), "Production", LnkUpPayrollDoc);
                    HdnContESI.Value = dtProductionpost.Rows[0]["VCHESIOREPFDOC"].ToString();
                    if (dtProductionpost.Rows[0]["VCHESIOREPFDOC"].ToString() != "")
                        FileVisibilty(HdnContESI, HypViewContESI, LnkDelContESI, FluContESI, dtProductionpost.Rows[0]["VCHESIOREPFDOC"].ToString(), "Production", LnkUpContESI);
                    hdnESIComp.Value = dtProductionpost.Rows[0]["VCHESIEPFCOMPDOC"].ToString();
                    if (dtProductionpost.Rows[0]["VCHESIEPFCOMPDOC"].ToString() != "")
                        FileVisibilty(hdnESIComp, HypViewESIComp, LnkDelESIComp, FluESIComp, dtProductionpost.Rows[0]["VCHESIEPFCOMPDOC"].ToString(), "Production", LnkUpESIComp);
                    HdnDelayDoc.Value = dtProductionpost.Rows[0]["VCHDOCUMENTINSUPPORT"].ToString();
                    if (dtProductionpost.Rows[0]["VCHDOCUMENTINSUPPORT"].ToString() != "")
                        FileVisibilty(HdnDelayDoc, HypViewDelayDoc, LnkDelDelayDoc, FluDelayDoc, dtProductionpost.Rows[0]["VCHDOCUMENTINSUPPORT"].ToString(), "Production", LnkUpDelayDoc);


                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "Incentive");
                }
                #endregion

                #region Investment
                try
                {


                    //dtInvestmentPost.Rows[0]["slno"].ToString();
                    if (dtInvestmentPost.Rows.Count > 0)
                    {
                        Txt_FFCI_Date_Before.Text = dtInvestmentPost.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString();
                        //dtInvestmentPost.Rows[0]["vchFFCIDocBeforeCode"].ToString();
                        Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPost.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();

                        Hyp_View_FFCI_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPost.Rows[0]["VCH_Document_in_support"].ToString();


                        lbl_Land_Before.Text = dtInvestmentPost.Rows[0]["DEC_LAND_TYPE_AMOUNT"].ToString();
                        lbl_Building_Before.Text = dtInvestmentPost.Rows[0]["DEC_Building"].ToString();
                        lbl_Plant_Mach_Before.Text = dtInvestmentPost.Rows[0]["DEC_Plant_Machinery"].ToString();
                        lbl_Other_Fixed_Asset_Before.Text = dtInvestmentPost.Rows[0]["DEC_Other_Fixed_Assests"].ToString();
                        lbl_Total_Capital_Before.Text = dtInvestmentPost.Rows[0]["DEC_Total"].ToString();
                        //dtInvestmentPost.Rows[0]["vchProjectDocBeforeCode"].ToString();			
                        Lbl_Approved_DPR_Before_Doc_Name.Text = dtInvestmentPost.Rows[0]["vchProjectDocBeforeCodeName"].ToString();
                        Hyp_View_Approved_DPR_Before_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPost.Rows[0]["vchProjectDocBefore"].ToString();
                        lbl_FFCI_Date_After.Text = dtInvestmentPost.Rows[0]["dtmFFCIDateAfter"].ToString();
                        //dtInvestmentPost.Rows[0]["vchFFCIDocAfterCode"].ToString();			
                        Lbl_FFCI_After_Doc_Name.Text = dtInvestmentPost.Rows[0]["vchFFCIDocAfterCodeName"].ToString();
                        Hyp_View_FFCI_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPost.Rows[0]["vchFFCIDocAfter"].ToString();

                        lbl_Land_After.Text = dtInvestmentPost.Rows[0]["decLandAmtAfter"].ToString();
                        lbl_Building_After.Text = dtInvestmentPost.Rows[0]["decBuildingAmtAfter"].ToString();
                        lbl_Plant_Mach_After.Text = dtInvestmentPost.Rows[0]["decPlantMachAmtAfter"].ToString();
                        lbl_Other_Fixed_Asset_After.Text = dtInvestmentPost.Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
                        lbl_Total_Capital_After.Text = dtInvestmentPost.Rows[0]["decTotalAmtAfter"].ToString();


                        //dtInvestmentPost.Rows[0]["vchProjectDocAfterCode"].ToString();			
                        Lbl_Approved_DPR_After_Doc_Name.Text = dtInvestmentPost.Rows[0]["vchProjectDocAfterCodeName"].ToString();
                        Hyp_View_Approved_DPR_After_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtInvestmentPost.Rows[0]["vchProjectDocAfter"].ToString();
                        //dtInvestmentPost.Rows[0]["intCreatedBy"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "Incentive");
                }
                #endregion

                #region MEANS OF FINANCE
                try
                {
                    if (dtMeansFinancePost.Rows.Count > 0)
                    {
                        //dtMeansFinancePre.Rows[0]["intMeansFinanceId"].ToString();
                        lbl_Equity_Amt.Text = dtMeansFinancePost.Rows[0]["decEquity"].ToString();
                        lbl_Loan_Bank_FI.Text = dtMeansFinancePost.Rows[0]["decLoanBankFI"].ToString();
                        Hyp_View_Term_Loan_Doc.NavigateUrl = "~/incentives/Files/InctBasicDoc/" + dtMeansFinancePost.Rows[0]["VCH_TERM_LOAN_SAC"].ToString();
                        lbl_FDI_Componet.Text = dtMeansFinancePost.Rows[0]["decFDIComponet"].ToString();
                        //dtMeansFinancePre.Rows[0]["intCreatedBy"].ToString();
                        //dtMeansFinancePre.Rows[0]["vchTermLoanDocCode"].ToString();
                        Lbl_Term_Loan_Doc_Name.Text = dtMeansFinancePost.Rows[0]["vchTermLoanDocCodeNAme"].ToString();



                        if (dtMoFTermLoanPost.Rows.Count > 0)
                        {
                            Grd_TL.DataSource = dtMoFTermLoanPost;
                            Grd_TL.DataBind();
                        }

                        if (dtMoFWorkingLoanPost.Rows.Count > 0)
                        {
                            Grd_WC.DataSource = dtMoFWorkingLoanPost;
                            Grd_WC.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "Incentive");
                }
                #endregion

                #region Avail Details

                try
                {
                    if (dtAvailDetPost.Rows.Count > 0)
                    {
                        if (dtAvailDetPost.Rows[0]["intNeverAvailedPrior"].ToString() == "1")
                        {
                            RadBtn_Availed_Earlier.SelectedIndex = 0;
                        }
                        else
                        {
                            RadBtn_Availed_Earlier.SelectedIndex = 1;
                        }

                        txtdiffclaimamt.Text = dtAvailDetPost.Rows[0]["decClaimExempted"].ToString();
                        txtreimamt.Text = dtAvailDetPost.Rows[0]["decClaimReimbursement"].ToString();
                        txtreimamtEPF.Text = dtAvailDetPost.Rows[0]["decClaimReimbursementEPF"].ToString();

                        if (dtAvailDetPost.Rows[0]["VchSanctionDoc"].ToString() != "")//fuSupportingDocs
                        {
                            Hid_Asst_Sanc_File_Name.Value = dtAvailDetPost.Rows[0]["VchSanctionDoc"].ToString();
                            FileVisibilty(Hid_Asst_Sanc_File_Name, Hyp_View_Asst_Sanc_Doc, LnkBtn_Delete_Asst_Sanc_Doc, FU_Asst_Sanc_Doc, dtAvailDetPost.Rows[0]["VchSanctionDoc"].ToString(), "AvailDetails", LnkBtn_Upload_Asst_Sanc_Doc);
                        }
                        if (dtAvailDetPost.Rows[0]["vchUndertakingSubsidyDoc"].ToString() != "")
                        {
                            Hid_Undertaking_File_Name.Value = dtAvailDetPost.Rows[0]["vchUndertakingSubsidyDoc"].ToString();
                            FileVisibilty(Hid_Undertaking_File_Name, Hyp_View_Undertaking_Doc, LnkBtn_Delete_Undertaking_Doc, FU_Undertaking_Doc, dtAvailDetPost.Rows[0]["vchUndertakingSubsidyDoc"].ToString(), "AvailDetails", LnkBtn_Upload_Undertaking_Doc);
                        }
                        if (dtAssistancePost.Rows.Count > 0)
                        {
                            grdAssistanceDetailsAD.DataSource = dtAssistancePost;
                            grdAssistanceDetailsAD.DataBind();
                            ViewState["dtincentive"] = dtAssistancePost;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "Incentive");
                }

                #endregion

                #region Bank Details
                try
                {
                    if (dtBankPost.Rows.Count > 0)
                    {
                        txtAccNo.Text = dtBankPost.Rows[0]["VCHACCOUNTNO"].ToString();
                        txtBnkNm.Text = dtBankPost.Rows[0]["VCHBANKNAME"].ToString();
                        txtBranch.Text = dtBankPost.Rows[0]["VCHBRANCHNAME"].ToString();
                        txtIFSC.Text = dtBankPost.Rows[0]["VCHIFSCNO"].ToString();
                        txtMICRNo.Text = dtBankPost.Rows[0]["VCHMICR"].ToString();

                        if (dtBankPost.Rows[0]["vchBankDoc"].ToString() != "")
                        {
                            FileVisibilty(hdnBank, hypBank, lnkBankDelete, fuBank, dtBankPost.Rows[0]["vchBankDoc"].ToString(), "Bank", lnkBankUpload);
                        }

                    }

                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "Incentive");
                }


                #endregion

                #region Additional Doc
                if (dtAddDocMastPost.Rows.Count > 0)
                {
                    try
                    {
                        D275.Value = dtAddDocMastPost.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
                        D274.Value = dtAddDocMastPost.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
                        D280.Value = dtAddDocMastPost.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();
                        if (dtAddDocMastPost.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString() != "")
                        {
                            FileVisibilty(D275, hypValidStatutary, lnkDValidStatutary, flValidStatutary, dtAddDocMastPost.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString(), "AdditionalDocument", lnkUValidStatutary);
                        }
                        if (dtAddDocMastPost.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString() != "")
                        {
                            FileVisibilty(D274, hypDelay, lnkDDelay, flDelay, dtAddDocMastPost.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString(), "AdditionalDocument", lnkUDelay);
                        }
                        if (dtAddDocMastPost.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString() != "")
                        {
                            FileVisibilty(D280, hypCleanApproveAuthority, lnkDCleanApproveAuthority, flCleanApproveAuthority, dtAddDocMastPost.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString(), "AdditionalDocument", lnkUCleanApproveAuthority);
                        }
                    }
                    catch (Exception ex)
                    {
                        Util.LogError(ex, "Incentive");
                    }
                }

                #endregion
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
        try
        {
            txtAccNo.Text = dtBank.Rows[0]["VCHACCOUNTNO"].ToString();
            txtBnkNm.Text = dtBank.Rows[0]["VCHBANKNAME"].ToString();
            txtBranch.Text = dtBank.Rows[0]["VCHBRANCHNAME"].ToString();
            txtIFSC.Text = dtBank.Rows[0]["VCHIFSCNO"].ToString();
            txtMICRNo.Text = dtBank.Rows[0]["VCHMICR"].ToString();
            if (dtBank.Rows[0]["vchBankDoc"].ToString() != "")
            {
                //hdnBank.Value = dtBank.Rows[0]["vchBankDoc"].ToString(); /////AUTHORIZEDFILE file upload
                //hypBank.NavigateUrl = "../incentives/Files/Bank/" + dtBank.Rows[0]["vchBankDoc"].ToString();
                //hypBank.Visible = true;
                //lnkBankDelete.Visible = true;
                ////lblOrgDocument.Visible = true;
                //fuBank.Enabled = false;

                FileVisibilty(hdnBank, hypBank, lnkBankDelete, fuBank, dtBank.Rows[0]["vchBankDoc"].ToString(), "Bank", lnkBankUpload);

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion

    #region Common Method
    private void UploadDocument(FileUpload fuDocument, HiddenField hdnDocument, string strFileName, HyperLink hypViewDocument, Label lblDocument, LinkButton lnkDocumentDelete, string strFolderName, LinkButton lnkBtnUpload = null)
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

                //// validation checked in client sciprt side
                if (!(IsFileValid(fuDocument)))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid file type (or) File name may contain dots. ', '" + MsgTitle + "'); </script>", false);
                    return;
                }
                int fileSize = fuDocument.PostedFile.ContentLength;
                if (fuDocument != FluDPR)
                {
                    if (fileSize > (4 * 1024 * 1024))
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 2 MB')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File size is too large. Maximum file size permitted is 4 MB !', '" + MsgTitle + "'); </script>", false);
                        return;
                    }
                    else
                    {
                        filename = strFileName + Path.GetExtension(fuDocument.FileName);
                    }
                }
                else
                {
                    if (fileSize > (12 * 1024 * 1024))
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 2 MB')", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File size is too large. Maximum file size permitted is 4 MB !', '" + MsgTitle + "'); </script>", false);
                        return;
                    }
                    else
                    {
                        filename = strFileName + Path.GetExtension(fuDocument.FileName);
                    }

                }
                fuDocument.SaveAs(strMainFolderPath + filename);
                hdnDocument.Value = filename;
                hypViewDocument.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFolderName, filename);
                hypViewDocument.Visible = true;
                lnkDocumentDelete.Visible = true;
                lblDocument.Visible = true;
                fuDocument.Enabled = false;
                lnkBtnUpload.Visible = false;
                //lnkBtnUpload.Attributes.Add("style", "visibility:hidden");
                //imgshow.Attributes.Add("src", "../images/incapproved.png");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtnUpload, LinkButton lnkDel, HyperLink hplnkView, Label lblFile, FileUpload updFile, string strFolderName)
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
            //imgshow.Attributes.Add("src", "../images/cancel-square.png");
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void FileVisibilty(HiddenField HdnFileName, HyperLink HypView, LinkButton LnkDeleteBtn, FileUpload FluCtrl, string FileName, string FolderName, LinkButton lnkBtnUpload = null)
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
            //if (FileName != "")
            //    imgshow.Attributes.Add("src", "../images/incapproved.png");
            //else
            //    imgshow.Attributes.Add("src", "../images/cancel-square.png");
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    private void GetHeaderName()
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
    private void BindFYearLabel()
    {
        try
        {
            string fyear = Convert.ToString(Session["FyYear"]).Trim() == "" ? RetFyear().ToString() : Convert.ToString(Session["FyYear"]).Trim();
            if (Session["FyYear"] == null)
            {
                if (DateTime.Now.Month < 4)
                {
                    fyear = (int.Parse(fyear) - 1).ToString();
                }
            }
            int year = int.Parse(fyear);
            LblYear.Text = year.ToString() + "-" + (year + 1).ToString();
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
    private int RetFyear()
    {
        int year = 0;
        int month = DateTime.Now.Month;
        if (month < 4)
        {
            year = (DateTime.Now.Year) - 1;
        }
        else
        {
            year = DateTime.Now.Year;
        }
        return year;
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

    /// <summary>
    /// Function to get certificate details for OSPCB/Factory and boiler from service
    /// </summary>
    /// <param name="aEnServiceDocType">Service Doc Type i.e whether boiler or factory</param>
    /// <param name="hdnDocValue">name of hidden field in which the document filename will be stored</param>
    /// <param name="fuUpload">fileupload control for the document to disable them in case service has document</param>
    /// <param name="lnkAdd">add linkbutton for the document to disable them in case service has document</param>
    /// <param name="lnkDel">delete linkbutton for the document to disable them in case service has document</param>
    /// <param name="hyp">Hyperlink to view/download the document</param>
    /// <param name="hdnServiceDocStatus">hidden field to store the status as to whether doc is present or not</param>
    private void BindDocFromService(enServiceDocType aEnServiceDocType, HiddenField hdnDocValue, FileUpload fuUpload, LinkButton lnkAdd, LinkButton lnkDel, HyperLink hyp, HiddenField hdnServiceDocStatus)
    {
        try
        {
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

            string strTempFilePath = IncentiveCommonFunctions.GetCertificateDetailsFromService(aEnServiceDocType, lstFiles, Convert.ToInt32(Session["investorid"]));

            if (!string.IsNullOrEmpty(strTempFilePath))
            {
                //set hidden field value
                hdnDocValue.Value = string.Format("{0}.zip", Session["investorId"].ToString());

                //disable the file upload control
                fuUpload.Enabled = false;
                lnkAdd.Visible = false;

                //remove the delete button
                lnkDel.Visible = false;
                hyp.Visible = true;
                hyp.NavigateUrl = strTempFilePath;
                hdnServiceDocStatus.Value = "1";
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }

    /// <summary>
    /// Function to delete all the service document saved in temp folder and create the new ones in pc folder
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
    #endregion

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
            objincUnit.FYear = Convert.ToInt16(Convert.ToString(Session["FyYear"]).Trim() == "" ? RetFyear().ToString() : Convert.ToString(Session["FyYear"]).Trim());/// Convert.ToInt16(Session["FyYear"]);
            objincUnit.incentivetype = 4;
            objincUnit.FormType = FormNumber.EmploymentCostSubsidy_09;





            IndustryDataSave();
            SaveProductAnalysis(0);
            AdditionalDocument();

            AvailClaimSave();
            SaveBankDetails();
            FileUploadControls();////---- doc master
            string retval = IncentiveManager.CreateIncentiveEmpCostSubsidy(objincUnit);
            if (retval.Contains('~'))
            {
                if (retval.Split('~')[0].ToString() == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application Drafted Successfully !</strong>', '" + MsgTitle + "'); </script>", false);

                }
                PostpopulateData(Convert.ToInt16(retval.Split('~')[1].ToString()));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Network Error found !', '" + MsgTitle + "'); </script>", false);
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
            ///////InitiateAllClass(objincUnit);
            ///////no used Session["InvestorId"] 
            //objincUnit.IncentiveNum = "6986";
            //objincUnit.PealNum = "6986";
            //objincUnit.PCNum = "6986";////Session["PCNo"]
            //objincUnit.UnitCode = "6986";///Session["UnitCode"] 
            //objincUnit.ProposalNum = "6986";////Session["ProposalNo"]
            //objincUnit.Userid = 1022;//// to be passed from session
            //objincUnit.strcActioncode = "A";
            //objincUnit.UnqIncentiveId = 6986;
            //objincUnit.Createdby = 6986;
            //objincUnit.FYear = 2016;////Session["FyYear"]
            //objincUnit.FormType = FormNumber.EmploymentCostSubsidy_09;


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
            objincUnit.FYear = Convert.ToInt16(Convert.ToString(Session["FyYear"]).Trim() == "" ? RetFyear().ToString() : Convert.ToString(Session["FyYear"]).Trim());
            objincUnit.incentivetype = 4;
            objincUnit.FormType = FormNumber.EmploymentCostSubsidy_09;


            IndustryDataSave();
            SaveProductAnalysis(0);
            AdditionalDocument();
            //SaveInvestmentDetails();
            //MeansOfFinance();
            AvailClaimSave();
            SaveBankDetails();
            FileUploadControls();////---- doc master


            string retval = IncentiveManager.CreateIncentiveEmpCostSubsidy(objincUnit);
            if (retval.Contains('~'))
            {
                int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
                /////------------------------------------------------------------------------------------------------

                Response.Redirect("EmployeementCostSubsidyPreview.aspx?InctUniqueNo=" + Convert.ToString(mstyp));
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Network Error found !', '" + MsgTitle + "'); </script>", false);
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


}
