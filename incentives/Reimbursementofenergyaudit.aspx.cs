using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using System.Linq;
using System.Collections.Specialized;
using BusinessLogicLayer.Incentive;

public partial class Reimbursementofenergyaudit : SessionCheck
{
    #region Member Variable
    Incentive objincUnit = new Incentive();
    DataTable gObjDtAssistanceDetails = new DataTable();
    DataTable gObjDtIncentiveAvailed = new DataTable();
    DataTable gObjDtTermLoanDetails = new DataTable();
    Additional doc;
    string resdt = "1";
    string gFilePath;
    string strMsg = "Incentive";
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                fillSalutation();
                #region AvailDetail
                crdtincentive();
                #endregion
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
                    lblHeader.Text = dtbPostSubFlag.Rows[0]["vchInctName"].ToString();
                }

                if (Convert.ToString(Session["ApplySource"]) == "0")
                {
                    PostpopulateData(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
                    FetchFieldCotents();//self                   
                }
                else
                {
                    PrepopulateData(Convert.ToInt16(Session["InvestorId"]));
                    FillPrepopulateFiles(); //master 
                }
            }
            #region Avail Details
            gObjDtIncentiveAvailed = (DataTable)ViewState["IncentiveAvailed"];
            #endregion
            #region Contract Demand / Connected load Details
            gObjDtTermLoanDetails = (DataTable)ViewState["TermLoanDetails"];
            #endregion

            precheckpreEDDdata();
            txtComptionDate.Attributes.Add("readonly", "readonly");
            txtsacdat.Attributes.Add("readonly", "readonly");
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
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

    public void FillPrepopulateFiles()
    {

        objincUnit = new Incentive();
        objincUnit.UnitCode = Convert.ToString(Session["UnitCode"]);
        DataSet dslivePreFile = IncentiveManager.PrepopulateFile(objincUnit);
        if (dslivePreFile.Tables[0].Rows.Count > 0)
        {
            FillFileUpladControls(dslivePreFile.Tables[0]);
        }

    }
    #region Fill File Upload
    public void FillFileUpladControls(DataTable dtFilesDtl)
    {
        if (dtFilesDtl.Rows.Count > 0 && dtFilesDtl != null)
        {
            for (int i = 0; i < dtFilesDtl.Rows.Count; i++)
            {
                DataRow objgRow = dtFilesDtl.Rows[i];

                if (objgRow["vchDocId"].ToString() == hdnDocumentinsupportID.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString()))
                        {
                            hydDocumentinsupport.Visible = true;
                            hydDocumentinsupport.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString();
                            hdnDocumentinsupport.Value = objgRow["vchFileName"].ToString();
                            FileDocumentinsupport.Enabled = false;
                            lnkDocumentinsupportDelet.Visible = true;
                        }
                    }
                }
                if (objgRow["vchDocId"].ToString() == hdnDateofcompletionID.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString()))
                        {
                            HydDateofcompletion.Visible = true;
                            HydDateofcompletion.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString();
                            hdnDateofcompletion.Value = objgRow["vchFileName"].ToString();
                            fileDateofcompletion.Enabled = false;
                            lnkDateofcompletionDelet.Visible = true;
                        }
                    }
                }

                if (objgRow["vchDocId"].ToString() == hdnAuditorDocId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString()))
                        {
                            hplAuditorDoc.Visible = true;
                            hplAuditorDoc.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString();
                            hdnAuditorDoc.Value = objgRow["vchFileName"].ToString();
                            fuAuditorDoc.Enabled = false;
                            lnkAuditorDocDelet.Visible = true;
                        }
                    }
                }
                if (objgRow["vchDocId"].ToString() == hdnAccreditationDocId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString()))
                        {
                            hplAuditorAccreditationDoc.Visible = true;
                            hplAuditorAccreditationDoc.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString();
                            hdnAccreditationDoc.Value = objgRow["vchFileName"].ToString();
                            fuAuditorAccreditationDoc.Enabled = false;
                            lnkAccreditationDocDelet.Visible = true;
                        }
                    }
                }
                if (objgRow["vchDocId"].ToString() == hdnExpenditureDocId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString()))
                        {
                            hplExpenditureDoc.Visible = true;
                            hplExpenditureDoc.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString();
                            hdnExpenditureDoc.Value = objgRow["vchFileName"].ToString();
                            fuExpenditureDoc.Enabled = false;
                            lnkExpenditureDocDelet.Visible = true;
                        }
                    }
                }


                if (objgRow["vchDocId"].ToString() == hdnDocumentId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString()))
                        {
                            HydDocument.Visible = true;
                            HydDocument.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString();
                            hdnDocument.Value = objgRow["vchFileName"].ToString();
                            FileDocument.Enabled = false;
                            lnkDocumentDelet.Visible = true;
                        }
                    }
                }

                if (objgRow["vchDocId"].ToString() == hdnCertificateId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString()))
                        {
                            hydCertificate.Visible = true;
                            hydCertificate.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString();
                            hdnCertificate.Value = objgRow["vchFileName"].ToString();
                            FileCertificate.Enabled = false;
                            lnkCertificateDelet.Visible = true;
                        }
                    }
                }
                if (objgRow["vchDocId"].ToString() == hdnCarbonFootPrtID.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString()))
                        {
                            hypCarbonFootPrt.Visible = true;
                            hypCarbonFootPrt.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + objgRow["vchFileName"].ToString();
                            hdnCarbonFootPrt.Value = objgRow["vchFileName"].ToString();
                            FileCarbonFootPrt.Enabled = false;
                            lnkCarbonFootPrtDelet.Visible = true;
                        }
                    }
                }

                //change made by Ritika Lath as availed details document will not flow from the document master table 
                //if (objgRow["vchDocId"].ToString() == hdnSupportingDocsID.Value)
                //{
                //    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                //    {
                //        hypSupportingDocs.Visible = true;
                //        hypSupportingDocs.NavigateUrl = "../incentives/Files/AvailDetails/" + objgRow["vchFileName"].ToString();
                //        hdnSupportingDocs.Value = objgRow["vchFileName"].ToString();
                //        fuSupportingDocs.Enabled = false;
                //        lnkSupportingDocsDelete.Visible = true;
                //    }
                //}
                //if (objgRow["vchDocId"].ToString() == hdnSubsidyAvailedID.Value)
                //{
                //    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                //    {
                //        hypSubsidyAvailed.Visible = true;
                //        hypSubsidyAvailed.NavigateUrl = "../incentives/Files/AvailDetails/" + objgRow["vchFileName"].ToString();
                //        hdnSubsidyAvailed.Value = objgRow["vchFileName"].ToString();
                //        fuSubsidyAvailed.Enabled = false;
                //        lnkSubsidyAvailedDocDelete.Visible = true;
                //    }
                //}
                if (objgRow["vchDocId"].ToString() == D274.Value && objgRow["vchDocId"].ToString() != "")
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/AdditionalDocument/" + objgRow["vchFileName"].ToString()))
                        {
                            hypDelay.Visible = true;
                            hypDelay.NavigateUrl = "../incentives/Files/AdditionalDocument/" + objgRow["vchFileName"].ToString();
                            D274.Value = objgRow["vchFileName"].ToString();
                            flDelay.Enabled = false;
                            lnkUDelay.Visible = true;
                        }
                    }
                }
                if (objgRow["vchDocId"].ToString() == D275.Value && objgRow["vchDocId"].ToString() != "")
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/AdditionalDocument/" + objgRow["vchFileName"].ToString()))
                        {
                            hypValidStatutary.Visible = true;
                            hypValidStatutary.NavigateUrl = "../incentives/Files/AdditionalDocument/" + objgRow["vchFileName"].ToString();
                            D275.Value = objgRow["vchFileName"].ToString();
                            flValidStatutary.Enabled = false;
                            lnkUValidStatutary.Visible = true;
                        }
                    }
                }

                if (objgRow["vchDocId"].ToString() == hdnDemandDocId.Value)
                {
                    if (objgRow["vchFileName"] != null && objgRow["vchFileName"] != DBNull.Value && objgRow["vchFileName"] != "")
                    {
                        if (File.Exists("../incentives/Files/ContractDemand/" + objgRow["vchFileName"].ToString()))
                        {
                            hypDemandDoc.Visible = true;
                            hypDemandDoc.NavigateUrl = "../incentives/Files/ContractDemand/" + objgRow["vchFileName"].ToString();
                            hdnDemandDoc.Value = objgRow["vchFileName"].ToString();
                            flDemandDoc.Enabled = false;
                            lnkDemandDocDelet.Visible = true;
                        }
                    }
                }
            }
        }
    }
    #endregion

    private void precheckpreEDDdata()
    {
        //modified by Ritika Lath
        txtconsumenumber.Enabled = true;
        txtconnectedload.Enabled = true;
        Incentive objEntityExe = new Incentive();
        objEntityExe.Userid = Convert.ToInt16(Session["InvestorId"]);
        objEntityExe.IncentiveNum = Convert.ToString(Request.QueryString["IncentiveNo"]);
        DataSet dsEDDearly = IncentiveManager.getPreEDDdataExistance(objEntityExe);

        if (dsEDDearly.Tables[0].Rows.Count > 0)
        {

            string strConsumerNumber = string.Empty;
            if (dsEDDearly.Tables[0].Rows[0]["VCHCONSUMENUMBER"] != null && dsEDDearly.Tables[0].Rows[0]["VCHCONSUMENUMBER"] != DBNull.Value)
            {
                strConsumerNumber = dsEDDearly.Tables[0].Rows[0]["VCHCONSUMENUMBER"].ToString();
            }
            string strConnectedLoad = string.Empty;
            if (dsEDDearly.Tables[0].Rows[0]["VCHCONNECTEDLOAD"] != null && dsEDDearly.Tables[0].Rows[0]["VCHCONNECTEDLOAD"] != DBNull.Value)
            {
                strConnectedLoad = dsEDDearly.Tables[0].Rows[0]["VCHCONNECTEDLOAD"].ToString();
            }

            if (!string.IsNullOrEmpty(strConsumerNumber))
            {
                txtconsumenumber.Text = dsEDDearly.Tables[0].Rows[0]["VCHCONSUMENUMBER"].ToString();
                txtconsumenumber.Enabled = false;
            }

            if (!string.IsNullOrEmpty(strConnectedLoad))
            {
                txtconnectedload.Text = dsEDDearly.Tables[0].Rows[0]["VCHCONNECTEDLOAD"].ToString();
                txtconnectedload.Enabled = false;
            }
        }


    }
    public void FetchFieldCotents()
    {
        try
        {
            objincUnit = new Incentive();
            objincUnit.GetVwPrmtrs = new GetAndViewParam();
            objincUnit.GetVwPrmtrs.Param1ID = "";//////Incentive Number
            objincUnit.GetVwPrmtrs.Param2ID = "";//////--UnitCode
            objincUnit.GetVwPrmtrs.Param3ID = "";/////--Proposal/Peal/PC Number
            objincUnit.GetVwPrmtrs.InctType = 4;/////--Form type 4(for Pre Insentive) 5(for Post Incentive)
            objincUnit.PCNum = "";
            objincUnit.UnitCode = "";
            objincUnit.Userid = Convert.ToInt16(Session["InvestorId"]);
            objincUnit.strcActioncode = "A";
            objincUnit.UnqIncentiveId = Convert.ToInt16(Request.QueryString["InctUniqueNo"]);
            objincUnit.Createdby = Convert.ToInt16(Session["InvestorId"]);
            objincUnit.FormType = FormNumber.OneTimeReimbursementOfEnergyAuditCost_13;

            DataSet dslive = IncentiveManager.GetIncentiveOneTmReim(objincUnit);
            DataTable dtEnergyAudit = dslive.Tables[4];///////////investment details
            DataTable dtContractDemand = dslive.Tables[2];
            DataTable dtAvaildt1 = dslive.Tables[5];
            DataTable dtAvaildt2 = dslive.Tables[6];
            DataTable dtAvaildt3 = dslive.Tables[7];
            DataTable dtAdditional = dslive.Tables[8];
            DataTable dtContract = dslive.Tables[10];
            DataTable dtstatus = dslive.Tables[16];///////////Status
            string draftStatus = dtstatus.Rows[0]["status"].ToString();

            FillEnergyDetails(dtEnergyAudit, draftStatus);
            FillAvailed(dtAvaildt1, dtAvaildt3, dtAvaildt2);
            FillAdditionalDoc(dtAdditional);

            if (dtContractDemand.Rows[0]["VCHDEMANDFILE"].ToString() != "")
            {
                hypDemandDoc.Visible = true;
                hypDemandDoc.NavigateUrl = "../incentives/Files/ContractDemand/" + dtContractDemand.Rows[0]["VCHDEMANDFILE"].ToString();
                hdnDemandDoc.Value = dtContractDemand.Rows[0]["VCHDEMANDFILE"].ToString();
                flDemandDoc.Enabled = false;
                lnkDemandDocDelet.Visible = true;
            }
            else
            {
                hypDemandDoc.Visible = false;
                flDemandDoc.Enabled = true;
                lnkDemandDocDelet.Visible = false;
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
    #region "FillAvailed Details"
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
                LnkBtn_Delete_Undertaking_Doc.Visible = true;
            }
            else
            {
                Hyp_View_Undertaking_Doc.Visible = false;
                FU_Undertaking_Doc.Enabled = true;
                LnkBtn_Delete_Undertaking_Doc.Visible = false;
            }

            if (dtavail.Rows[0]["VchSanctionDoc"].ToString() != "")
            {
                Hyp_View_Asst_Sanc_Doc.Visible = true;
                Hyp_View_Asst_Sanc_Doc.NavigateUrl = "../incentives/Files/AvailDetails/" + dtavail.Rows[0]["VchSanctionDoc"].ToString();
                Hid_Asst_Sanc_File_Name.Value = dtavail.Rows[0]["VchSanctionDoc"].ToString();
                FU_Asst_Sanc_Doc.Enabled = false;
                LnkBtn_Delete_Asst_Sanc_Doc.Visible = true;
            }
            else
            {
                Hyp_View_Asst_Sanc_Doc.Visible = false;
                FU_Asst_Sanc_Doc.Enabled = true;
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
    #region Energy Detail

    public void FillEnergyDetails(DataTable dtEnergy, string strStatus)
    {
        if (dtEnergy.Rows.Count > 0)
        {
            txtAuditorName.Text = dtEnergy.Rows[0]["strEnergyAuditorName"].ToString();
            txtEnergyAuditorAddress.Text = dtEnergy.Rows[0]["strEnergyAuditorAddress"].ToString();
            txtAuditorAccreditation.Text = dtEnergy.Rows[0]["strEnergyAuditorAccreditation"].ToString();
            txtExpenditure.Text = dtEnergy.Rows[0]["strExpenditureincurred"].ToString();

            if (dtEnergy.Rows[0]["dtmSuccessfulcompletionAuditDate"].ToString() == "01-Jan-1900")
            {
                txtComptionDate.Text = "";
            }
            else
            {
                txtComptionDate.Text = dtEnergy.Rows[0]["dtmSuccessfulcompletionAuditDate"].ToString();
            }

            txtBeforeAudit.Text = dtEnergy.Rows[0]["strEnergyConsumptionBefore"].ToString();
            txtAfterAudit.Text = dtEnergy.Rows[0]["dtmEnergyConsumptionAfter"].ToString();

            if (dtEnergy.Rows[0]["strSupportofimplementationofEnergyDoc"].ToString() != "")
            {
                hydDocumentinsupport.Visible = true;
                hydDocumentinsupport.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + dtEnergy.Rows[0]["strSupportofimplementationofEnergyDoc"].ToString();
                hdnDocumentinsupport.Value = dtEnergy.Rows[0]["strSupportofimplementationofEnergyDoc"].ToString();
                FileDocumentinsupport.Enabled = false;
                lnkDocumentinsupportDelet.Visible = true;
            }
            else
            {
                hydDocumentinsupport.Visible = false;
                FileDocumentinsupport.Enabled = true;
                lnkDocumentinsupportDelet.Visible = false;
            }
            if (dtEnergy.Rows[0]["strSuccessfulcompletionAuditDoc"].ToString() != "")
            {
                HydDateofcompletion.Visible = true;
                HydDateofcompletion.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + dtEnergy.Rows[0]["strSuccessfulcompletionAuditDoc"].ToString();
                hdnDateofcompletion.Value = dtEnergy.Rows[0]["strSuccessfulcompletionAuditDoc"].ToString();
                fileDateofcompletion.Enabled = false;
                lnkDateofcompletionDelet.Visible = true;
            }
            else
            {
                HydDateofcompletion.Visible = false;
                fileDateofcompletion.Enabled = true;
                lnkDateofcompletionDelet.Visible = false;
            }
            if (dtEnergy.Rows[0]["strEnergyAuditorDocName"].ToString() != "")
            {
                hplAuditorDoc.Visible = true;
                hplAuditorDoc.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + dtEnergy.Rows[0]["strEnergyAuditorDocName"].ToString();
                hdnAuditorDoc.Value = dtEnergy.Rows[0]["strEnergyAuditorDocName"].ToString();
                fuAuditorDoc.Enabled = false;
                lnkAuditorDocDelet.Visible = true;
            }
            else
            {
                hplAuditorDoc.Visible = false;
                fuAuditorDoc.Enabled = true;
                lnkAuditorDocDelet.Visible = false;
            }
            if (dtEnergy.Rows[0]["strEnergyAuditorAccreditationDoc"].ToString() != "")
            {
                hplAuditorAccreditationDoc.Visible = true;
                hplAuditorAccreditationDoc.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + dtEnergy.Rows[0]["strEnergyAuditorAccreditationDoc"].ToString();
                hdnAccreditationDoc.Value = dtEnergy.Rows[0]["strEnergyAuditorAccreditationDoc"].ToString();
                fuAuditorAccreditationDoc.Enabled = false;
                lnkAccreditationDocDelet.Visible = true;
            }
            else
            {
                hplAuditorAccreditationDoc.Visible = false;
                fuAuditorAccreditationDoc.Enabled = true;
                lnkAccreditationDocDelet.Visible = false;
            }
            if (dtEnergy.Rows[0]["strExpenditureincurredDoc"].ToString() != "")
            {
                hplExpenditureDoc.Visible = true;
                hplExpenditureDoc.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + dtEnergy.Rows[0]["strExpenditureincurredDoc"].ToString();
                hdnExpenditureDoc.Value = dtEnergy.Rows[0]["strExpenditureincurredDoc"].ToString();
                fuExpenditureDoc.Enabled = false;
                lnkExpenditureDocDelet.Visible = true;
            }
            else
            {
                hplExpenditureDoc.Visible = false;
                fuExpenditureDoc.Enabled = true;
                lnkExpenditureDocDelet.Visible = false;
            }
            if (dtEnergy.Rows[0]["strReductionOfEnergyDoc"].ToString() != "")
            {
                HydDocument.Visible = true;
                HydDocument.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + dtEnergy.Rows[0]["strReductionOfEnergyDoc"].ToString();
                hdnDocument.Value = dtEnergy.Rows[0]["strReductionOfEnergyDoc"].ToString();
                FileDocument.Enabled = false;
                lnkDocumentDelet.Visible = true;
            }
            else
            {
                HydDocument.Visible = false;
                FileDocument.Enabled = true;
                lnkDocumentDelet.Visible = false;
            }

            if (dtEnergy.Rows[0]["strEnergyEfficiencyCertificate"].ToString() != "")
            {
                hydCertificate.Visible = true;
                hydCertificate.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + dtEnergy.Rows[0]["strEnergyEfficiencyCertificate"].ToString();
                hdnCertificate.Value = dtEnergy.Rows[0]["strEnergyEfficiencyCertificate"].ToString();
                FileCertificate.Enabled = false;
                lnkCertificateDelet.Visible = true;
            }
            else
            {
                hydCertificate.Visible = false;
                FileCertificate.Enabled = true;
                lnkCertificateDelet.Visible = false;
            }

            if (dtEnergy.Rows[0]["strCarbonFootprintDoc"].ToString() != "")
            {
                hypCarbonFootPrt.Visible = true;
                hypCarbonFootPrt.NavigateUrl = "../incentives/Files/EneryAuditDetail/" + dtEnergy.Rows[0]["strCarbonFootprintDoc"].ToString();
                hdnCarbonFootPrt.Value = dtEnergy.Rows[0]["strCarbonFootprintDoc"].ToString();
                FileCarbonFootPrt.Enabled = false;
                lnkCarbonFootPrtDelet.Visible = true;
            }
            else
            {
                hypCarbonFootPrt.Visible = false;
                FileCarbonFootPrt.Enabled = true;
                lnkCarbonFootPrtDelet.Visible = false;
            }
        }
    }
    #endregion
    #region Fill Additional Document

    public void FillAdditionalDoc(DataTable dtAdditionalDoc)
    {
        if (dtAdditionalDoc.Rows.Count > 0)
        {
            D275.Value = dtAdditionalDoc.Rows[0]["VCHVALIDSATUTORYGREENCATEGORY"].ToString();
            D274.Value = dtAdditionalDoc.Rows[0]["VCHCONDODOCUMENTATIONDELAY"].ToString();
            D280.Value = dtAdditionalDoc.Rows[0]["VCHSTATUTORYCLEARANCE"].ToString();

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
    }
    #endregion
    #region viewdetail
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

        #region IndustrailUnit

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
                divbefor2.Visible = true;
            }
            else
            {
                divbefor.Visible = false;
                divbefor2.Visible = false;
                lblAfterEMD11.Text = "Date of Production Commencement";
                lblAfterEMD189.Text = "PC Issuance Date";
                lbl_PC_No_After.Text = "PC No";
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

        #region Investment
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
        #endregion

        #region MEANS OF FINANCE
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
        #endregion

        BindDocFromService(enServiceDocType.OSPCB, D275, flValidStatutary, lnkUValidStatutary, lnkDValidStatutary, hypValidStatutary, hdnIsOsPCBDownloaded);
        BindDocFromService(enServiceDocType.Boiler, D280, flCleanApproveAuthority, lnkUCleanApproveAuthority, lnkDCleanApproveAuthority, hypCleanApproveAuthority, hdnBoilerDownloaded);

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

        #region IndustrailUnit
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
                divbefor2.Visible = true;

            }
            else
            {
                divbefor.Visible = false;
                divbefor2.Visible = false;
                lblAfterEMD11.Text = "Date of Production Commencement";
                lblAfterEMD189.Text = "PC Issuance Date";
                lbl_PC_No_After.Text = "PC No";
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

            //------------------extra added --------------
            TxtApplicantName.Text = dtindustryPre.Rows[0]["VCHAPPLICANTNAME"].ToString();
            DdlGender.SelectedValue = dtindustryPre.Rows[0]["INTGENDER"].ToString();
            int intApplyBy = Convert.ToInt32(dtindustryPre.Rows[0]["INTAPPLYBY"].ToString());
            radApplyBy.SelectedValue = dtindustryPre.Rows[0]["INTAPPLYBY"].ToString();
            if (intApplyBy == 1)
            {

                if (dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Trim() != "")
                {
                    TxtAdhaar1.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString();
                    //TxtAdhaar2.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Substring(4, 4);
                    //TxtAdhaar3.Text = dtindustryPre.Rows[0]["VCHAADHAARNO"].ToString().Substring(8, 4);
                }
            }
            else
            {

                if (dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"] != null && dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"] != DBNull.Value && dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"] != "")
                {
                    hdnAUTHORIZEDFILE.Value = dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    FlupAUTHORIZEDFILE.Enabled = false;
                    hypAUTHORIZEDFILE.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtindustryPre.Rows[0]["VCHAUTHORIZEDFILENAME"].ToString();
                    hypAUTHORIZEDFILE.Visible = true;
                    lnkAUTHORIZEDFILEDdelete.Visible = true;
                }
            }
        }
        #endregion
        #region Investment
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
        #region Investment
        Txt_FFCI_Date_Before.Text = dtInvestmentPre.Rows[0]["DTM_IND_Date_of_First_Fixed"].ToString();
        Lbl_FFCI_Before_Doc_Name.Text = dtInvestmentPre.Rows[0]["vchFFCIDocBeforeCodeName"].ToString();
        Hyp_View_FFCI_Before_Doc.Text = dtInvestmentPre.Rows[0]["VCH_Document_in_support"].ToString();
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
        #endregion

        #region MEANS OF FINANCE
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
        #endregion


    }
    #endregion
    #region Industrial Unit's Details
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

    protected EntityLayer.Incentive.InvestmentDetails SaveInvestmentDetails(Incentive objIncen)
    {
        EntityLayer.Incentive.InvestmentDetails InvestmentDet = new EntityLayer.Incentive.InvestmentDetails();
        try
        {
            objIncen.InvestmentDet = InvestmentDet;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {

        }
        return InvestmentDet;

    }



    /// <summary>
    /// Attribute assignement to Industry master class object
    /// </summary>
    /// <returns></returns>
    public string IndustryDataSave()
    {
        try
        {
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
            objincUnit.FYear = Session["FyYear"] == "" ? 0 : Convert.ToInt16(Session["FyYear"]);
            objincUnit.incentivetype = 4;
            objincUnit.FormType = FormNumber.OneTimeReimbursementOfEnergyAuditCost_13;

            objincUnit.IndsutUnitMstDet = new INDUSTRIAL_UNIT_MASTER();

            objincUnit.IndsutUnitMstDet.APPLICANTNAME_IND = TxtApplicantName.Text;
            if (DdlGender.SelectedIndex >= 0)
            {
                objincUnit.IndsutUnitMstDet.GENDER_IND = Convert.ToInt32(DdlGender.SelectedValue);
            }
            if (radApplyBy.SelectedIndex >= 0)
            {
                objincUnit.IndsutUnitMstDet.APPLYBY_IND = Convert.ToInt32(radApplyBy.SelectedValue);
            }
            objincUnit.IndsutUnitMstDet.AADHAARNO_IND = TxtAdhaar1.Text;
            objincUnit.IndsutUnitMstDet.AUTHORIZEDFILENAME_IND = hdnAUTHORIZEDFILE.Value;

            EnergyAuditDetails(objincUnit);
            AvailDetailAdd(objincUnit);
            AdditionalDocument(objincUnit);
            ContractDemand(objincUnit);
            SaveInvestmentDetails(objincUnit);
            FileUploadControls(objincUnit);
            objincUnit.FormType = FormNumber.OneTimeReimbursementOfEnergyAuditCost_13;
            string retval = IncentiveManager.CreateIncentive_OneTimeReimbursement(objincUnit);
            return retval;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        return resdt;
    }
    #endregion
    protected void lnkDocfirstinvestmentUpload_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (LinkButton)sender;
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
        else if (string.Equals(lnk.ID, lnkDemandDocUpload.ID))
        {
            if (flDemandDoc.HasFile)
            {
                string strFileName = "ContractDemand" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(flDemandDoc, hdnDemandDoc, strFileName, hypDemandDoc, lblDemandDoc, lnkDemandDocDelet, "../incentives/Files/ContractDemand/", "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkDocumentinsupportUpload.ID))
        {
            if (FileDocumentinsupport.HasFile)
            {
                string strFileName = "DocuSupport" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(FileDocumentinsupport, hdnDocumentinsupport, strFileName, hydDocumentinsupport, lblDocumentinsupport, lnkDocumentinsupportDelet, "../incentives/Files/EneryAuditDetail/", "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkDateofcompletionUpload.ID))
        {
            if (fileDateofcompletion.HasFile)
            {
                //string strFileName = "SuccImple" + DateTime.Now.ToString("_ddMMyyhhmmss");
                string strFileName = "RptEnerAudit" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(fileDateofcompletion, hdnDateofcompletion, strFileName, HydDateofcompletion, lblDateofcompletion, lnkDateofcompletionDelet, "../incentives/Files/EneryAuditDetail/", "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkAuditorDocUpload.ID))
        {
            if (fuAuditorDoc.HasFile)
            {
                string strFileName = "ProflEnergyAudi" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(fuAuditorDoc, hdnAuditorDoc, strFileName, hplAuditorDoc, lblAuditorDoc, lnkAuditorDocDelet, "../incentives/Files/EneryAuditDetail/", "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkAccreditationDocUpload.ID))
        {
            if (fuAuditorAccreditationDoc.HasFile)
            {
                string strFileName = "Accredi" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(fuAuditorAccreditationDoc, hdnAccreditationDoc, strFileName, hplAuditorAccreditationDoc, lblAccreditationDoc, lnkAccreditationDocDelet, "../incentives/Files/EneryAuditDetail/", "pdf/zip");
            }
        }

        else if (string.Equals(lnk.ID, lnkExpenditureDocUpload.ID))
        {
            if (fuExpenditureDoc.HasFile)
            {
                string strFileName = "Expendi" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(fuExpenditureDoc, hdnExpenditureDoc, strFileName, hplExpenditureDoc, lblExpenditureDoc, lnkExpenditureDocDelet, "../incentives/Files/EneryAuditDetail/", "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkDocumentUpload.ID))
        {
            if (FileDocument.HasFile)
            {
                string strFileName = "DocuRedu" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(FileDocument, hdnDocument, strFileName, HydDocument, lblDocument, lnkDocumentDelet, "../incentives/Files/EneryAuditDetail/", "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkCertificateUpload.ID))
        {
            if (FileCertificate.HasFile)
            {
                string strFileName = "CertiReduc" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(FileCertificate, hdnCertificate, strFileName, hydCertificate, lblCertificate, lnkCertificateDelet, "../incentives/Files/EneryAuditDetail/", "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkCarbonFootPrtUpload.ID))
        {
            if (FileCarbonFootPrt.HasFile)
            {
                string strFileName = "CarbnFootPrt" + DateTime.Now.ToString("_ddMMyyhhmmss");
                UploadDocument(FileCarbonFootPrt, hdnCarbonFootPrt, strFileName, hypCarbonFootPrt, lblCarbonFootPrt, lnkCarbonFootPrtDelet, "../incentives/Files/EneryAuditDetail/", "pdf/zip");
            }
        }
        else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILE.ID))
        {
            if (FlupAUTHORIZEDFILE.HasFile)
            {
                string strFileName = "AUTHORIZEDFILE" + DateTime.Now.ToString("_ddMMyyyyhhmmss");
                UploadDocument(FlupAUTHORIZEDFILE, hdnAUTHORIZEDFILE, strFileName, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, "../incentives/Files/InctBasicDoc/", "pdf/zip");
            }
        }
    }
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
        else if (string.Equals(lnk.ID, lnkDemandDocDelet.ID))
        {
            UpdFileRemove(hdnDemandDoc, lnkDemandDocUpload, lnkDemandDocDelet, hypDemandDoc, lblDemandDoc, flDemandDoc, "../incentives/Files/ContractDemand/");
        }
        else if (string.Equals(lnk.ID, lnkDocumentinsupportDelet.ID))
        {
            UpdFileRemove(hdnDocumentinsupport, lnkDocumentinsupportUpload, lnkDocumentinsupportDelet, hydDocumentinsupport, lblDocumentinsupport, FileDocumentinsupport, "../incentives/Files/EneryAuditDetail/");
        }
        else if (string.Equals(lnk.ID, lnkDateofcompletionDelet.ID))
        {
            UpdFileRemove(hdnDateofcompletion, lnkDateofcompletionUpload, lnkDateofcompletionDelet, HydDateofcompletion, lblDateofcompletion, fileDateofcompletion, "../incentives/Files/EneryAuditDetail/");
        }
        else if (string.Equals(lnk.ID, lnkAuditorDocDelet.ID))
        {
            UpdFileRemove(hdnAuditorDoc, lnkAuditorDocUpload, lnkAuditorDocDelet, hplAuditorDoc, lblAuditorDoc, fuAuditorDoc, "../incentives/Files/EneryAuditDetail/");
        }
        else if (string.Equals(lnk.ID, lnkAccreditationDocDelet.ID))
        {
            UpdFileRemove(hdnAccreditationDoc, lnkAccreditationDocUpload, lnkAccreditationDocDelet, hplAuditorAccreditationDoc, lblAccreditationDoc, fuAuditorAccreditationDoc, "../incentives/Files/EneryAuditDetail/");
        }
        else if (string.Equals(lnk.ID, lnkExpenditureDocDelet.ID))
        {
            UpdFileRemove(hdnExpenditureDoc, lnkExpenditureDocUpload, lnkExpenditureDocDelet, hplExpenditureDoc, lblExpenditureDoc, fuExpenditureDoc, "../incentives/Files/EneryAuditDetail/");
        }
        else if (string.Equals(lnk.ID, lnkDocumentDelet.ID))
        {
            UpdFileRemove(hdnDocument, lnkDocumentUpload, lnkDocumentDelet, HydDocument, lblDocument, FileDocument, "../incentives/Files/EneryAuditDetail/");
        }
        else if (string.Equals(lnk.ID, lnkCertificateDelet.ID))
        {
            UpdFileRemove(hdnCertificate, lnkCertificateUpload, lnkCertificateDelet, hydCertificate, lblCertificate, FileCertificate, "../incentives/Files/EneryAuditDetail/");
        }
        else if (string.Equals(lnk.ID, lnkCarbonFootPrtDelet.ID))
        {
            UpdFileRemove(hdnCarbonFootPrt, lnkCarbonFootPrtUpload, lnkCarbonFootPrtDelet, hypCarbonFootPrt, lblCarbonFootPrt, FileCarbonFootPrt, "../incentives/Files/EneryAuditDetail/");
        }
        else if (string.Equals(lnk.ID, lnkAUTHORIZEDFILEDdelete.ID))
        {
            UpdFileRemove(hdnAUTHORIZEDFILE, lnkAUTHORIZEDFILE, lnkAUTHORIZEDFILEDdelete, hypAUTHORIZEDFILE, lblAUTHORIZEDFILE, FlupAUTHORIZEDFILE, "../incentives/Files/InctBasicDoc/");
        }
    }
    #region FileUPloadControls

    public void FileUploadControls(Incentive objEntity)
    {
        List<lstFileUpload> fileList = new List<lstFileUpload>();
        if (RadBtn_Availed_Earlier.SelectedValue == "1")
        {
            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = "D253",
                vchFileName = Hid_Asst_Sanc_File_Name.Value,
                // vchFilePath = "../incentives/Files/AvailDetails/" + Session["investorid"].ToString() + "/"

                vchFilePath = "../incentives/Files/AvailDetails/"

            });
        }
        else
        {
            fileList.Add(new lstFileUpload()
            {
                id = 1,
                vchDocId = "D230",
                vchFileName = Hid_Undertaking_File_Name.Value,
                vchFilePath = "../incentives/Files/AvailDetails/"

            });
        }
        fileList.Add(new lstFileUpload()
        {
            id = 2,
            vchDocId = "D271",
            vchFileName = hdnDemandDoc.Value,
            vchFilePath = "../incentives/Files/ContractDemand/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 3,
            vchDocId = "D155",
            vchFileName = hdnDocumentinsupport.Value,
            vchFilePath = "../incentives/Files/EneryAuditDetail/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 4,
            vchDocId = "D152",
            vchFileName = hdnDateofcompletion.Value,
            vchFilePath = "../incentives/Files/EneryAuditDetail/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 5,
            vchDocId = "D156",
            vchFileName = hdnAuditorDoc.Value,
            vchFilePath = "../incentives/Files/EneryAuditDetail/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 6,
            vchDocId = "D180",
            vchFileName = hdnAccreditationDoc.Value,
            vchFilePath = "../incentives/Files/EneryAuditDetail/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 7,
            vchDocId = "D165",
            vchFileName = hdnExpenditureDoc.Value,
            vchFilePath = "../incentives/Files/EneryAuditDetail/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 8,
            vchDocId = "D154",
            vchFileName = hdnDocument.Value,
            vchFilePath = "../incentives/Files/EneryAuditDetail/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 9,
            vchDocId = "D127",
            vchFileName = hdnCertificate.Value,
            vchFilePath = "../incentives/Files/EneryAuditDetail/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 10,
            vchDocId = "D272",
            vchFileName = hdnCarbonFootPrt.Value,
            vchFilePath = "../incentives/Files/EneryAuditDetail/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 11,
            vchDocId = "D274",
            vchFileName = D274.Value,
            vchFilePath = "../incentives/Files/AdditionalDocument/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 12,
            vchDocId = "D275",
            vchFileName = D275.Value,
            vchFilePath = "../incentives/Files/AdditionalDocument/"

        });
        fileList.Add(new lstFileUpload()
        {
            id = 13,
            vchDocId = "D280",
            vchFileName = D280.Value,
            vchFilePath = "../incentives/Files/AdditionalDocument/"

        });
        objEntity.FileUploadDetails = fileList;

    }
    #endregion
    #region "Availed Details"

    public void AvailDetailAdd(Incentive objEntity)
    {
        AvailDetails objAvailDetails = new AvailDetails();
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

    #endregion
    #region Additional Document
    public void AdditionalDocument(Incentive objIncentive)
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

            objIncentive.AdditionalDocument = new EntityLayer.Incentive.AdditionalDocuments();
            objIncentive.AdditionalDocument.strValidSatutoryGreenCategory = D275.Value;
            objIncentive.AdditionalDocument.strCondoDocumentationDelay = D274.Value;
            objIncentive.AdditionalDocument.strCleanApproveAuthorityOSPCB = D280.Value;
        }
        catch (Exception)
        {
        }
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

    #region Energy Audit Details
    public void EnergyAuditDetails(Incentive inv)
    {
        try
        {
            inv.EnergyAuditDet = new EnergyAuditDetails();
            inv.EnergyAuditDet.strEnergyAuditorName = txtAuditorName.Text.Trim();
            inv.EnergyAuditDet.strEnergyAuditorDocName = hdnAuditorDoc.Value;
            inv.EnergyAuditDet.strEnergyAuditorAddress = txtEnergyAuditorAddress.Text.Trim();
            inv.EnergyAuditDet.strEnergyAuditorAccreditation = txtAuditorAccreditation.Text.Trim();
            inv.EnergyAuditDet.strEnergyAuditorAccreditationDoc = hdnAccreditationDoc.Value;
            if (txtComptionDate.Text != "")
            {
                inv.EnergyAuditDet.dtmSuccessfulcompletionAuditDate = Convert.ToDateTime(txtComptionDate.Text);
            }

            inv.EnergyAuditDet.strExpenditureincurredDoc = hdnExpenditureDoc.Value;
            inv.EnergyAuditDet.strSupportofimplementationofEnergyDOC = hdnDocumentinsupport.Value;
            inv.EnergyAuditDet.strSuccessfulcompletionAuditDOC = hdnDateofcompletion.Value;
            if (txtBeforeAudit.Text.Trim() != "")
            {
                inv.EnergyAuditDet.strEnergyConsumptionBefore = Convert.ToDecimal(txtBeforeAudit.Text.Trim());
            }
            if (txtAfterAudit.Text.Trim() != "")
            {
                inv.EnergyAuditDet.dtmEnergyConsumptionAfter = Convert.ToDecimal(txtAfterAudit.Text.Trim());
            }
            inv.EnergyAuditDet.strReductionOfEnergyDoc = hdnDocument.Value;
            inv.EnergyAuditDet.strEnergyEfficiencyCertificate = hdnCertificate.Value;
            inv.EnergyAuditDet.strCarbonFootprintDoc = hdnCarbonFootPrt.Value;
            if (txtExpenditure.Text != "")
            {
                inv.EnergyAuditDet.strExpenditureincurred = Convert.ToDecimal(txtExpenditure.Text);  /// need to decimal field
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    #endregion
    #region Common Methods
    public void ContractDemand(Incentive objMasterIncentiveAvailDetails)
    {

        ConsumeLoadDet objContractLoadDet = new ConsumeLoadDet();
        ContractLoadDet objContractLoan = new ContractLoadDet();
        objContractLoadDet.stringCONSUMENUMBER = txtconsumenumber.Text;
        objContractLoadDet.stringCONNECTEDLOAD = txtconnectedload.Text;
        objContractLoan.strcdemandfile = hdnDemandDoc.Value;

        objMasterIncentiveAvailDetails.ContractLoadDet = objContractLoan;
        objMasterIncentiveAvailDetails.ConsumLoadDet = objContractLoadDet;

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

    void documenttable()
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
    void documentsindt(string strdocid, string filname)
    {
        DataTable dtdocument = new DataTable();
        dtdocument = (DataTable)ViewState["dtdocument"];
        DataRow drdoc = dtdocument.NewRow();
        drdoc["vchDocId"] = strdocid;
        drdoc["vchFileName"] = filname;
        drdoc["vchFilePath"] = "../incentives/Files/AvailDetails/";
        dtdocument.Rows.Add(drdoc);
        ViewState["dtdocument"] = dtdocument;

    }
    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string FolderPath)
    {
        string filename = hdnFile.Value;
        string completePath = Server.MapPath(FolderPath + filename);
        //if (File.Exists(completePath))
        //{
        // File.Delete(completePath);
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


            if (IsFileValid(fuOrgDocument) == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "jAlert('Invalid File Contain','');", true);
                return;
            }
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
    #region "Availed2ndGrid"

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

    #endregion
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string retval = IndustryDataSave();
        string msg = "<strong>Application Drafted Successfully !!</strong>";
        string msgTtl = "SWP";
        if (retval.Split('~')[0].ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + msg + "', '" + msgTtl + "');   </script>", false);
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        string retval = IndustryDataSave();
        int mstyp = Convert.ToInt32(retval.Split('~')[1].ToString());
        Response.Redirect("Reimbursementofenergyaudit_FormPreview.aspx?InctUniqueNo=" + Convert.ToString(mstyp));
    }
}
