// *******************************************************************************************************************
// File Name         : ExemptionLandforIndustrialUseIPR2022.aspx
// Description       : Exemption Land for Industrial Use IPR-2022 Add and Draft Page
// Created by        : Debiprasanna Jena
// Created On        : 11th Oct 2023
// Modification History:

// <CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

// *********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAcessLayer.Common;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Collections.Specialized;
using System.IO;

public partial class incentives_ExemptionLandforIndustrialUseIPR2022 : System.Web.UI.Page
{
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            FillUnitCategory();
            FillSalutation();
            FillOrgType();
            FillUnitMeasurment();
            Txt_EnterPrise_Name.Enabled = false;
            Txt_Other_Unit_Before.Visible = false;
            Div_Land_Converter.Visible = false;

            if (Request.QueryString["InctUniqueNo"] != null)
            {
                PostpopulateDataComm();
            }
            else
            {
                FillUnitCategory();
                FillSalutation();
                FillOrgType();
                FillUnitMeasurment();
                FillData();
                Txt_EnterPrise_Name.Enabled = false;
                Txt_Other_Unit_Before.Visible = false;
                Div_Land_Converter.Visible = false;
            }
        }
    }

    public void PostpopulateDataComm()
    {
        try
        {
            DataSet dslivePre = IncentiveManager.ExemptionLandIndustrialUse_populateDatainDraft(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            DataTable dtindustryPre = dslivePre.Tables[0];////////////Industrial Unit's Details
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment

            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////Term Loan Details
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////Working Capital Loan Details
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////Other Document Details
          
            #region IndustrailUnit
            if (dtindustryPre.Rows.Count > 0)
            {
                Txt_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString().Trim();
                Txt_ThrustorPriority.Text= dtindustryPre.Rows[0]["vchThrustPriorityStatus"].ToString().Trim();
                DrpDwn_Unit_Cat.SelectedValue = (dtindustryPre.Rows[0]["intUnitCat"].ToString());              
                Txt_Regd_Office_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();
             
                DrpDwn_Org_Type.SelectedValue = (dtindustryPre.Rows[0]["intOrganisationType"].ToString());

                if (DrpDwn_Org_Type.SelectedValue == "15")
                {
                    Lbl_Org_Name_Type.Text = "Name of Proprietor";
                }
                else if (DrpDwn_Org_Type.SelectedValue == "17")
                {
                    Lbl_Org_Name_Type.Text = "Name of Managing Partner";
                }
                else if (DrpDwn_Org_Type.SelectedValue == "20")
                {
                    Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
                }
                else if (DrpDwn_Org_Type.SelectedValue == "19")
                {
                    Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
                }
                else if (DrpDwn_Org_Type.SelectedValue == "18")
                {
                    Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
                }
                Txt_Partner_Name.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
                DrpDwn_Gender_Partner.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
                Txt_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
                Txt_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
                Txt_Propsed_Location.Text= dtindustryPre.Rows[0]["vchProposedLocation"].ToString();
                Txt_Status.Text = dtindustryPre.Rows[0]["vchPrsentStatus"].ToString();
                Txt_Proposed_Date.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();

                //*------------------------------------------------------------------------------------------*/
                #region Production

                Txt_Commence_Production.Text = dtProductionPre.Rows[0]["dtmFFCIDateBefore"].ToString();
                Txt_Financer.Text= dtProductionPre.Rows[0]["vchNameoffinancer"].ToString();
                Txt_Cost_Project.Text= dtProductionPre.Rows[0]["decCostofproject"].ToString();
                Txt_Land_Required.Text= dtProductionPre.Rows[0]["decAreaofLandRequired"].ToString();
                Txt_Land_Acquired.Text= dtProductionPre.Rows[0]["decAreaofLandAcquired"].ToString();
                Rad_Land_converted.SelectedValue= dtProductionPre.Rows[0]["intParticularsLandtobeconverted"].ToString();

                Grd_Production_Before.DataSource = dtProductionDetBefPre;
                Grd_Production_Before.DataBind();
                if (DrpDwn_Unit_Before.SelectedIndex > 0)
                {
                    if (DrpDwn_Unit_Before.SelectedValue == "52")
                    {
                        Txt_Other_Unit_Before.Visible = true;
                        Txt_Other_Unit_Before.Focus();
                    }
                    else
                    {
                        Txt_Other_Unit_Before.Text = "";
                        Txt_Other_Unit_Before.Visible = false;
                        Txt_Value_Before.Focus();
                    }
                }
                else
                {
                    Txt_Other_Unit_Before.Text = "";
                    Txt_Other_Unit_Before.Visible = false;
                }

                if (Rad_Land_converted.SelectedValue == "1")
                {
                    Grd_Land.DataSource = dtProductionDetAftPre;
                    Grd_Land.DataBind();
                    Div_Land_Converter.Visible = true;
                }
                else
                {
                    Div_Land_Converter.Visible = false;
                }

                #endregion

                /*--------------------------------------------------------------------------------------------------------*/

                ///-------------------------------Other File Upload-----------------------------------/////

               for (int i = 0; i < dtInvestmentPre.Rows.Count; i++)
                {
                    string vchDocId = dtInvestmentPre.Rows[i]["vchDocId"].ToString();
                    string vchFileName = dtInvestmentPre.Rows[i]["vchFileName"].ToString();

                    if (vchDocId == "D324")
                    {
                        HdnEinno_Name.Value = vchFileName;
                        HypEinno.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypEinno.Visible = true;
                        LnkDEinno.Visible = true;
                        LblEinno.Visible = true;
                        FluEinno.Enabled = false;
                    }
                    else if (vchDocId == "D325")
                    {
                        HdnPoweratt_Name.Value = vchFileName;
                        HypPoweratt.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypPoweratt.Visible = true;
                        LnkDPoweratt.Visible = true;
                        LblPoweratt.Visible = true;
                        FluPoweratt.Enabled = false;
                    }
                    else if (vchDocId == "D326")
                    {
                        HdnCertofreg_Name.Value = vchFileName;
                        HypVwCertofreg.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwCertofreg.Visible = true;
                        LnkDCertofreg.Visible = true;
                        LblCertofreg.Visible = true;
                        FluCertofreg.Enabled = false;
                    }
                    else if (vchDocId == "D327")
                    {
                        HdnFixcapital_Name.Value = vchFileName;
                        HypVwFixcapital.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwFixcapital.Visible = true;
                        LnkDFixcapital.Visible = true;
                        LblFixcapital.Visible = true;
                        FluFixcapital.Enabled = false;
                    }
                    else if (vchDocId == "D328")
                    {
                        HdnThrustcerti_Name.Value = vchFileName;
                        HypvwThrustcerti.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwThrustcerti.Visible = true;
                        LnkDThrustcerti.Visible = true;
                        LblThrustcerti.Visible = true;
                        FluThrustcerti.Enabled = false;
                    }
                    else if (vchDocId == "D329")
                    {
                        HdnAppprovedproj_Name.Value = vchFileName;
                        HypvwAppprovedproj.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwAppprovedproj.Visible = true;
                        LnkDAppprovedproj.Visible = true;
                        LblAppprovedproj.Visible = true;
                        FluAppprovedproj.Enabled = false;
                    }
                    else if (vchDocId == "D330")
                    {
                        HdnAppraisal_Name.Value = vchFileName;
                        HypVwAppraisal.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwAppraisal.Visible = true;
                        LnkDAppraisal.Visible = true;
                        LblAppraisal.Visible = true;
                        FluAppraisal.Enabled = false;
                    }
                    else if (vchDocId == "D331")
                    {
                        HdnLanddocument_Name.Value = vchFileName;
                        HypvwLanddocument.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwLanddocument.Visible = true;
                        LnkDLanddocument.Visible = true;
                        LblLanddocument.Visible = true;
                        FluLanddocument.Enabled = false;
                    }
                    else if (vchDocId == "D332")
                    {
                        HdnStatutory_Name.Value = vchFileName;
                        HypvwStatutory.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwStatutory.Visible = true;
                        LnkDstatutory.Visible = true;
                        LblStatutory.Visible = true;
                        FluStatutory.Enabled = false;
                    }
                    else if (vchDocId == "D333")
                    {
                        HdnStampPaper_Name.Value = vchFileName;
                        HypvwStampPaper.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwStampPaper.Visible = true;
                        LnkDStampPaper.Visible = true;
                        LblStampPaper.Visible = true;
                        FluStampPaper.Enabled = false;
                    }
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }
    }

    private void FillUnitCategory()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "B";
            objBAL.BindDropdown(DrpDwn_Unit_Cat, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }      
    }

    private void FillSalutation()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "R";
            objBAL.BindDropdown(DrpDwn_Gender_Partner, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }       
    }

    private void FillOrgType()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();

        try
        {
            objEntity.Action = "OT";
            objBAL.BindDropdown(DrpDwn_Org_Type, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }      
    }

    private void FillUnitMeasurment()
    {
        try
        {
            string action = "A";
            CommonDataLayer objDataUnit = new CommonDataLayer();

            DrpDwn_Unit_Before.DataTextField = "vchName";
            DrpDwn_Unit_Before.DataValueField = "slno";
            DrpDwn_Unit_Before.DataSource = objDataUnit.FillUnitType(action);
            DrpDwn_Unit_Before.DataBind();
            DrpDwn_Unit_Before.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }
    }

    private void FillData()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity = new Basic_Unit_Details_Entity();
       
        try
        {
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            DataSet ds = objBAL.Basic_Unit_Details_V(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                /*----------------------------------------------------------------------------*/
                /// Common Information 
                /*----------------------------------------------------------------------------*/
                string strDataSource = ds.Tables[0].Rows[0]["vch_Data_Source"].ToString();
                string strPcStatus = ds.Tables[0].Rows[0]["vch_PC_Status"].ToString();
                string strIsExistBefore = ds.Tables[0].Rows[0]["vch_Is_Before_Exist"].ToString();
                string strIsExistAfter = ds.Tables[0].Rows[0]["vch_Is_After_Exist"].ToString();
                string strIndustryCode = ds.Tables[0].Rows[0]["vch_Industry_Code"].ToString();
                string strProposalNo = ds.Tables[0].Rows[0]["vch_Proposal_No"].ToString();
                string strProjectType = ds.Tables[0].Rows[0]["int_Project_Type"].ToString();
                string strNewPcFound = ds.Tables[0].Rows[0]["vch_New_PC_Found"].ToString();

                /*----------------------------------------------------------------------------*/
                /// If new PC found then assign strDataSource=PC
                /// Only when data present in basic table and a new PC found 
                /*----------------------------------------------------------------------------*/
                if (strDataSource == "BASIC" && strNewPcFound == "Y")
                {
                        strDataSource = "PC";
                }
                /*----------------------------------------------------------------------------*/
                /// Value Assigned to HiddenField for use in Validation
                /*----------------------------------------------------------------------------*/

                Hid_Is_Exist_Before.Value = strIsExistBefore;
                Hid_Is_Exist_After.Value = strIsExistAfter;
                Hid_Data_Source.Value = strDataSource;
                Hid_PC_Status.Value = strPcStatus;
                Hid_Project_Type.Value = strProjectType;
                /*----------------------------------------------------------------------------*/

                if (strDataSource == "BASIC")
                {
                    Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchEnterpriseName"].ToString();
                }

                else if (strDataSource == "PC")
                {
                    if (strIsExistBefore == "Y")
                    {
                        Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchCompName"].ToString();
                    }
                    if (strIsExistAfter == "Y")
                    {
                        Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchCompName"].ToString();
                    }
                }
                else if (strDataSource == "PEAL" || strDataSource == "REGD")
                {
                    Txt_EnterPrise_Name.Text = ds.Tables[1].Rows[0]["vchNameOfUnit"].ToString();
                }
                /*---------------------------------------------------------------*/
                /// Session Assigned Here

                Session["UnitCode"] = strIndustryCode;
                Session["ProposalNo"] = strProposalNo;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }      
    }

    private bool IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf", "application/x-zip-compressed" };
        string[] allowedExtension = { ".pdf", ".zip" };
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        imageTypes.AddRange(allowedImageTyps);
        imageExtension.AddRange(allowedExtension);
        string strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
        string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
        int count = FileUpload1.FileName.Count(f => f == '.');
        System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);

        if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UploadDocument(FileUpload FileUpload_Document, HiddenField Hid_File_Name, string strFileName, HyperLink Hyp_Document, Label Lbl_Upload_Msg, LinkButton LnkBtn_Delete_Doc, string strFoldername)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/incentives/Files/{0}/", strFoldername));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }
            if (FileUpload_Document.HasFile)
            {
                string filename = string.Empty;
                if (Path.GetExtension(FileUpload_Document.FileName).ToLower() != ".pdf" && Path.GetExtension(FileUpload_Document.FileName).ToLower() != ".zip")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload either .pdf or .zip file !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }

                if (!IsFileValid(FileUpload_Document))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid file type (or) File name may contain dots !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }

                int fileSize = FileUpload_Document.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>File size is too large. Maximum file size permitted is 4 MB !!</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }
                else
                {
                    filename = strFileName + Path.GetExtension(FileUpload_Document.FileName);
                }

                FileUpload_Document.SaveAs(strMainFolderPath + filename);
                Hid_File_Name.Value = filename;
                Hyp_Document.NavigateUrl = string.Format("~/incentives/Files/{0}/{1}", strFoldername, filename);
                Hyp_Document.Visible = true;
                LnkBtn_Delete_Doc.Visible = true;
                Lbl_Upload_Msg.Visible = true;
                FileUpload_Document.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }
    }

    private void UpdFileRemove(HiddenField Hid_File_Name, LinkButton LnkBtn_Upload_Doc, LinkButton LnkBtn_Delete_Doc, HyperLink Hyp_Document, Label Lbl_Upload_Msg, FileUpload FileUpload_Document, string strFolername)
    {
        try
        {
            string filename = Hid_File_Name.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolername, filename);
            Server.MapPath(path);

            Hid_File_Name.Value = "";
            LnkBtn_Delete_Doc.Visible = false;
            LnkBtn_Upload_Doc.Visible = true;
            Hyp_Document.Visible = false;
            Lbl_Upload_Msg.Visible = false;
            FileUpload_Document.Enabled = true;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }
    }
    protected void LnkBtn_Add_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkUEinno.ID))
            {
                if (FluEinno.HasFile)
                {

                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_EinnoLandforIndustrialUseIPR-2022";
                    UploadDocument(FluEinno, HdnEinno_Name, strFileName, HypEinno, LblEinno, LnkDEinno, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUPoweratt.ID))
            {
                if (FluPoweratt.HasFile)
                {

                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PoweofAttorneyLandforIndustrialUseIPR-2022";
                    UploadDocument(FluPoweratt, HdnPoweratt_Name, strFileName, HypPoweratt, LblPoweratt, LnkDPoweratt, "InctBasicDoc");
                }

            }
            else if (string.Equals(lnk.ID, LnkUCertofreg.ID))
            {
                if (FluCertofreg.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_MEMORANDUMLandforIndustrialUseIPR-2022";
                    UploadDocument(FluCertofreg, HdnCertofreg_Name, strFileName, HypVwCertofreg, LblCertofreg, LnkDCertofreg, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUFixcapital.ID))
            {
                if (FluFixcapital.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_FixCapitalInvstLandforIndustrialUseIPR-2022";
                    UploadDocument(FluFixcapital, HdnFixcapital_Name, strFileName, HypVwFixcapital, LblFixcapital, LnkDFixcapital, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUThrustcerti.ID))
            {
                if (FluThrustcerti.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ThrustCertificateLandforIndustrialUseIPR-2022";
                    UploadDocument(FluThrustcerti, HdnThrustcerti_Name, strFileName, HypvwThrustcerti, LblThrustcerti, LnkDThrustcerti, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUAppprovedproj.ID))
            {
                if (FluAppprovedproj.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ApproveddetailedLandforIndustrialUseIPR-2022";
                    UploadDocument(FluAppprovedproj, HdnAppprovedproj_Name, strFileName, HypvwAppprovedproj, LblAppprovedproj, LnkDAppprovedproj, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUAppraisal.ID))
            {
                if (FluAppraisal.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_AppraisalLandforIndustrialUseIPR-2022";
                    UploadDocument(FluAppraisal, HdnAppraisal_Name, strFileName, HypVwAppraisal, LblAppraisal, LnkDAppraisal, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkULanddocument.ID))
            {
                if (FluLanddocument.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_LandDocumentLandforIndustrialUseIPR-2022";
                    UploadDocument(FluLanddocument, HdnLanddocument_Name, strFileName, HypvwLanddocument, LblLanddocument, LnkDLanddocument, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUstatutory.ID))
            {
                if (FluStatutory.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_StatutoryLandforIndustrialUseIPR-2022";
                    UploadDocument(FluStatutory, HdnStatutory_Name, strFileName, HypvwStatutory, LblStatutory, LnkDstatutory, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUStampPaper.ID) && (FluStampPaper.HasFile))
            {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_StampPaperLandforIndustrialUseIPR-2022";
                    UploadDocument(FluStampPaper, HdnStampPaper_Name, strFileName, HypvwStampPaper, LblStampPaper, LnkDStampPaper, "InctBasicDoc");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }
    }

    protected void LnkBtn_Delete_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkDEinno.ID))
            {
                UpdFileRemove(HdnEinno_Name, LnkUEinno, LnkDEinno, HypEinno, LblEinno, FluEinno, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDPoweratt.ID))
            {
                UpdFileRemove(HdnPoweratt_Name, LnkUPoweratt, LnkDPoweratt, HypPoweratt, LblPoweratt, FluPoweratt, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDCertofreg.ID))
            {
                UpdFileRemove(HdnCertofreg_Name, LnkUCertofreg, LnkDCertofreg, HypVwCertofreg, LblCertofreg, FluCertofreg, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDFixcapital.ID))
            {
                UpdFileRemove(HdnFixcapital_Name, LnkUFixcapital, LnkDFixcapital, HypVwFixcapital, LblFixcapital, FluFixcapital, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDThrustcerti.ID))
            {
                UpdFileRemove(HdnThrustcerti_Name, LnkUThrustcerti, LnkDThrustcerti, HypvwThrustcerti, LblThrustcerti, FluThrustcerti, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDAppprovedproj.ID))
            {
                UpdFileRemove(HdnAppprovedproj_Name, LnkUAppprovedproj, LnkDAppprovedproj, HypvwAppprovedproj, LblAppprovedproj, FluAppprovedproj, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDAppraisal.ID))
            {
                UpdFileRemove(HdnAppraisal_Name, LnkUAppraisal, LnkDAppraisal, HypVwAppraisal, LblAppraisal, FluAppraisal, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDLanddocument.ID))
            {
                UpdFileRemove(HdnLanddocument_Name, LnkULanddocument, LnkDLanddocument, HypvwLanddocument, LblLanddocument, FluLanddocument, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDstatutory.ID))
            {
                UpdFileRemove(HdnStatutory_Name, LnkUstatutory, LnkDstatutory, HypvwStatutory, LblStatutory, FluStatutory, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDStampPaper.ID))
            {
                UpdFileRemove(HdnStampPaper_Name, LnkUStampPaper, LnkDStampPaper, HypvwStampPaper, LblStampPaper, FluStampPaper, "InctBasicDoc");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }
    }

    protected void DrpDwn_Org_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DrpDwn_Org_Type.SelectedValue == "15")
            {
                Lbl_Org_Name_Type.Text = "Name of Proprietor";
            }
            else if (DrpDwn_Org_Type.SelectedValue == "17")

            {
                Lbl_Org_Name_Type.Text = "Name of Managing Partner";
            }
            else if (DrpDwn_Org_Type.SelectedValue == "18")

            {
                Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
            }
            else if (DrpDwn_Org_Type.SelectedValue == "19")

            {
                Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
            }
            else if (DrpDwn_Org_Type.SelectedValue == "20")

            {
                Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }
    }

    protected void DrpDwn_Unit_Before_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DrpDwn_Unit_Before.SelectedIndex > 0)
            {
                if (DrpDwn_Unit_Before.SelectedValue == "52")
                {
                    Txt_Other_Unit_Before.Visible = true;
                    Txt_Other_Unit_Before.Focus();
                }
                else
                {
                    Txt_Other_Unit_Before.Text = "";
                    Txt_Other_Unit_Before.Visible = false;
                    Txt_Value_Before.Focus();
                }
            }
            else
            {
                Txt_Other_Unit_Before.Text = "";
                Txt_Other_Unit_Before.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }
    }

    protected void LnkBtn_Add_Item_Before_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("intUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));
            table.Columns.Add("decValue", typeof(string));
            table.Rows.Add(Txt_Product_Name_Before.Text, Txt_Quantity_Before.Text, DrpDwn_Unit_Before.SelectedItem.Text, DrpDwn_Unit_Before.SelectedValue, Txt_Other_Unit_Before.Text == "" ? null : Txt_Other_Unit_Before.Text, Txt_Value_Before.Text);
            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                Label Lbl_Product_Name_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                Label Lbl_Quantity_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                Label Lbl_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
                Label Lbl_Other_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                Label Lbl_Value_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                HiddenField Hid_Unit_Before = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");
                table.Rows.Add(Lbl_Product_Name_Before.Text, Lbl_Quantity_Before.Text, Lbl_Unit_Before.Text, Hid_Unit_Before.Value, Lbl_Other_Unit_Before.Text, Lbl_Value_Before.Text);
            }

            Grd_Production_Before.DataSource = table;
            Grd_Production_Before.DataBind();

            Txt_Product_Name_Before.Text = "";
            Txt_Quantity_Before.Text = "";
            DrpDwn_Unit_Before.SelectedIndex = 0;
            Txt_Value_Before.Text = "";
            Txt_Other_Unit_Before.Text = "";
            Txt_Other_Unit_Before.Visible = false;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }      
    }

    protected void ImgBtn_Delete_Before_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("intUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));
            table.Columns.Add("decValue", typeof(string));

            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_Product_Name_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                    Label Lbl_Quantity_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                    Label Lbl_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
                    Label Lbl_Other_Unit_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                    Label Lbl_Value_Before = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                    HiddenField Hid_Unit_Before = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");
                    table.Rows.Add(Lbl_Product_Name_Before.Text, Lbl_Quantity_Before.Text, Lbl_Unit_Before.Text, Hid_Unit_Before.Value, Lbl_Other_Unit_Before.Text, Lbl_Value_Before.Text);
                }
            }

            Grd_Production_Before.DataSource = table;
            Grd_Production_Before.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }      
    }

    protected void LnkBtn_Add_Item_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchMouza", typeof(string));
            table.Columns.Add("vchKhataNo", typeof(string));
            table.Columns.Add("vchArea", typeof(string));
            table.Columns.Add("vchPlotNo", typeof(string));
            table.Columns.Add("vchPresentKisam", typeof(string));
            table.Rows.Add(Txt_Mouza.Text, Txt_Khata_No.Text, Txt_Plot_No.Text, Txt_Area.Text, Txt_Present_Kisam.Text);
            for (int i = 0; i < Grd_Land.Rows.Count; i++)
            {
                Label Lbl_Mouza = (Label)Grd_Land.Rows[i].FindControl("Lbl_Mouza");
                Label Lbl_Khata_No = (Label)Grd_Land.Rows[i].FindControl("Lbl_Khata_No");
                Label Lbl_Plot_No = (Label)Grd_Land.Rows[i].FindControl("Lbl_Plot_No");
                Label Lbl_Area = (Label)Grd_Land.Rows[i].FindControl("Lbl_Area");
                Label Lbl_Prsent_Kisam = (Label)Grd_Land.Rows[i].FindControl("Lbl_Prsent_Kisam");

                table.Rows.Add(Lbl_Mouza.Text, Lbl_Khata_No.Text, Lbl_Plot_No.Text, Lbl_Area.Text, Lbl_Prsent_Kisam.Text);
            }

            Grd_Land.DataSource = table;
            Grd_Land.DataBind();


            Txt_Mouza.Text = "";
            Txt_Khata_No.Text = "";
            Txt_Plot_No.Text = "";
            Txt_Area.Text = "";
            Txt_Present_Kisam.Text = "";

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }      
    }

    protected void ImgBtn_Delete_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchMouza", typeof(string));
            table.Columns.Add("vchKhataNo", typeof(string));
            table.Columns.Add("vchArea", typeof(string));
            table.Columns.Add("vchPlotNo", typeof(string));
            table.Columns.Add("vchPresentKisam", typeof(string));

            for (int i = 0; i < Grd_Land.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_Mouza = (Label)Grd_Land.Rows[i].FindControl("Lbl_Mouza");
                    Label Lbl_Khata_No = (Label)Grd_Land.Rows[i].FindControl("Lbl_Khata_No");
                    Label Lbl_Plot_No = (Label)Grd_Land.Rows[i].FindControl("Lbl_Plot_No");
                    Label Lbl_Area = (Label)Grd_Land.Rows[i].FindControl("Lbl_Area");
                    Label Lbl_Prsent_Kisam = (Label)Grd_Land.Rows[i].FindControl("Lbl_Prsent_Kisam");

                    table.Rows.Add(Lbl_Mouza.Text, Lbl_Khata_No.Text, Lbl_Plot_No.Text, Lbl_Area.Text, Lbl_Prsent_Kisam.Text);
                }
            }

            Grd_Land.DataSource = table;
            Grd_Land.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }     
    }

    protected void Rad_Land_converted_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        if (Rad_Land_converted.SelectedValue == "1")
        {
            Div_Land_Converter.Visible = true;
            Txt_Mouza.Text = "";
            Txt_Khata_No.Text = "";
            Txt_Plot_No.Text = "";
            Txt_Area.Text = "";
            Txt_Present_Kisam.Text = "";
            Grd_Land.DataSource = table;
            Grd_Land.DataBind();
            table.Clear();
        }
        else
        {
            Div_Land_Converter.Visible = false;

        }
    }

    protected void BtnApply_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
        
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity1 = new Basic_Unit_Details_Entity();

        try
        {
            #region Production Item Before

            /*---------------------------------------------------------------------*/
            /// Add Production Item Before
            /*---------------------------------------------------------------------*/

            List<BasicProductionItemBefore> listProdBefore = new List<BasicProductionItemBefore>();

            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                BasicProductionItemBefore objItem = new BasicProductionItemBefore();

                Label lblProductionName = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                Label lblQuantity = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                Label lblOtherUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                Label lblValue = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                HiddenField hdnUnit = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");

                objItem.vchProductName = lblProductionName.Text;
                objItem.decQuantity = Convert.ToDecimal(lblQuantity.Text == "" ? "0" : lblQuantity.Text);
                if (hdnUnit.Value != "")
                {
                    objItem.intUnitType = Convert.ToInt32(hdnUnit.Value);
                }
                objItem.vchOtherUnit = lblOtherUnit.Text == "" ? null : lblOtherUnit.Text;
                objItem.decValue = Convert.ToDecimal(lblValue.Text == "" ? "0" : lblValue.Text);
                objItem.chItemFor = "B"; //// Before

                listProdBefore.Add(objItem);
            }

            objEntity1.ProductionItem_BE = listProdBefore;


            #endregion

            #region Land Converted

            /*---------------------------------------------------------------------*/
            ///  Add Land Converted
            /*---------------------------------------------------------------------*/
           
            List<LandConverted> listland = new List<LandConverted>();

            for (int i = 0; i < Grd_Land.Rows.Count; i++)
            {
                LandConverted objlandconvert = new LandConverted();

                Label LblMouza = (Label)Grd_Land.Rows[i].FindControl("Lbl_Mouza");
                Label LblKhataNo = (Label)Grd_Land.Rows[i].FindControl("Lbl_Khata_No");
                Label LblPlotNo = (Label)Grd_Land.Rows[i].FindControl("Lbl_Plot_No");
                Label LblArea = (Label)Grd_Land.Rows[i].FindControl("Lbl_Area");
                Label LblPrsentKisam = (Label)Grd_Land.Rows[i].FindControl("Lbl_Prsent_Kisam");


                objlandconvert.vchMouza = LblMouza.Text;
                objlandconvert.vchKhataNo = LblKhataNo.Text;
                objlandconvert.vchPlotNo = LblPlotNo.Text;
                objlandconvert.vchArea = LblArea.Text;
                objlandconvert.vchPresentKisam = LblPrsentKisam.Text;

                listland.Add(objlandconvert);
            }
            objEntity1.Land_tobe_converted = listland;
            #endregion


            #region IndustrialUnitDetailsSection

            /*---------------------------------------------------------------------*/
            /// Industrial Unit Details Section
            /*---------------------------------------------------------------------*/

            objEntity1.strEnterpriseName = Txt_EnterPrise_Name.Text;
            objEntity1.intUnitCat = Convert.ToInt32(DrpDwn_Unit_Cat.SelectedValue);
            objEntity1.strThrustorPrioritySector = Txt_ThrustorPriority.Text;
            objEntity1.strRegisteredOfcAddress = Txt_Regd_Office_Address.Text;
            objEntity1.intOrganisationType = Convert.ToInt32(DrpDwn_Org_Type.SelectedValue);
            objEntity1.strManagingPartnerGender = DrpDwn_Gender_Partner.SelectedValue;
            objEntity1.strManagingPartnerName = Txt_Partner_Name.Text;
            objEntity1.dtmProdCommBefore = Txt_Commence_Production.Text;
            objEntity1.strEINNO = Txt_EIN_IL_NO.Text == "" ? null : Txt_EIN_IL_NO.Text;
            objEntity1.dtmEIN = Txt_EIN_IL_Date.Text == "" ? null : Txt_EIN_IL_Date.Text;
            objEntity1.dtmFFCIDateBefore = Txt_Proposed_Date.Text;
            objEntity1.strProposedLocation = Txt_Propsed_Location.Text;
            objEntity1.strPrsentStatus = Txt_Status.Text;
            objEntity1.strNameoffinancer = Txt_Financer.Text;
            objEntity1.decCostofproject = Convert.ToDecimal(Txt_Cost_Project.Text);
            objEntity1.decAreaofLandRequired = Convert.ToDecimal(Txt_Land_Required.Text);
            objEntity1.decAreaofLandAcquired = Convert.ToDecimal(Txt_Land_Acquired.Text);
            objEntity1.intParticularsLandtobeconverted = Convert.ToInt32(Rad_Land_converted.SelectedValue);
                
            /*-------------------------Industrial Unit Details End---------------------------------*/
            #endregion

            #region OtherDocumentSection

            /*---------------------------------------------------------------------*/
            /// Other Document Section
            /*---------------------------------------------------------------------*/

            if (HdnEinno_Name.Value != "")
            {
                objEntity1.strEINorPEALapprovalDocCode = HdnEinno_Code.Value;
                objEntity1.strEINorPEALapproval = HdnEinno_Name.Value;
            }
            else
            {
                objEntity1.strEINorPEALapprovalDocCode = null;
                objEntity1.strEINorPEALapproval = null;
            }

            if (HdnPoweratt_Name.Value != "")
            {
                objEntity1.strPworofAttorneyDocCode = HdnPoweratt_Code.Value;
                objEntity1.strPworofAttorney = HdnPoweratt_Name.Value;
            }
            else
            {
                objEntity1.strPworofAttorneyDocCode = null;
                objEntity1.strPworofAttorney = null;
            }

            if (HdnCertofreg_Name.Value != "")
            {
                objEntity1.strCertificateofregistrationDocCode = HdnCertofreg_Code.Value;
                objEntity1.strCertificateofregistration = HdnCertofreg_Name.Value;
            }
            else
            {
                objEntity1.strCertificateofregistrationDocCode = null;
                objEntity1.strCertificateofregistration = null;
            }

            if (HdnFixcapital_Name.Value != "")
            {
                objEntity1.strfixedcapitalinvstDocCode = HdnFixcapital_Code.Value;
                objEntity1.strfixedcapitalinvst = HdnFixcapital_Name.Value;
            }
            else
            {
                objEntity1.strfixedcapitalinvstDocCode = null;
                objEntity1.strfixedcapitalinvst = null;
            }

            if (HdnThrustcerti_Name.Value != "")
            {
                objEntity1.strProvisionalPriorityorThruststatusDocCode = HdnThrustcerti_Code.Value;
                objEntity1.strProvisionalPriorityorThruststatus = HdnThrustcerti_Name.Value;
            }
            else
            {
                objEntity1.strProvisionalPriorityorThruststatusDocCode = null;
                objEntity1.strProvisionalPriorityorThruststatus = null;
            }

            if (HdnAppprovedproj_Name.Value != "")
            {
                objEntity1.strApprovalDetailsprojrctDocCode = HdnAppprovedproj_Code.Value;
                objEntity1.strApprovalDetailsprojrct = HdnAppprovedproj_Name.Value;
            }
            else
            {
                objEntity1.strApprovalDetailsprojrctDocCode = null;
                objEntity1.strApprovalDetailsprojrct = null;
            }

            if (HdnAppraisal_Name.Value != "")
            {
                objEntity1.strApprisalsupportexpansionDocCode = HdnAppraisal_Code.Value;
                objEntity1.strApprisalsupportexpansion = HdnAppraisal_Name.Value;
            }
            else
            {
                objEntity1.strApprisalsupportexpansionDocCode = null;
                objEntity1.strApprisalsupportexpansion = null;
            }

            if (HdnLanddocument_Name.Value != "")
            {
                objEntity1.strLanddocumentDocCode = HdnLanddocument_Code.Value;
                objEntity1.strLanddocument = HdnLanddocument_Name.Value;
            }
            else
            {
                objEntity1.strLanddocumentDocCode = null;
                objEntity1.strLanddocument = null;
            }

            if (HdnStatutory_Name.Value != "")
            {
                objEntity1.strStatutoryClearancesDocCode = HdnStatutory_Code.Value;
                objEntity1.strStatutoryClearances = HdnStatutory_Name.Value;
            }
            else
            {
                objEntity1.strStatutoryClearancesDocCode = null;
                objEntity1.strStatutoryClearances = null;
            }

            if (HdnStampPaper_Name.Value != "")
            {
                objEntity1.strNonjudicialStampDocCode = HdnStampPaper_Code.Value;
                objEntity1.strNonjudicialStamp = HdnStampPaper_Name.Value;
            }
            else
            {
                objEntity1.strNonjudicialStampDocCode = null;
                objEntity1.strNonjudicialStamp = null;
            }

            /*-------------------------Other Document End---------------------------------*/

            #endregion

            ///This method is called to assign  Session value
            FillData();
            objEntity1.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objEntity1.strPcNoAfter = Session["ProposalNo"].ToString();
            objEntity1.strIndustryCode = Session["UnitCode"].ToString();

            if (Request.QueryString["key"] != null)
            {
                objEntity1.strInctFlow = Request.QueryString["key"].ToString();
            }
            else
            {
                objEntity1.strInctFlow = Request.QueryString["IncentiveNo"].ToString();
            }

            /*---------------------------------------------------------------*/
            /// Data Insert and Update
            /// 1-Insert
            /// 2-Update
            /*---------------------------------------------------------------*/

            string strReturnStatus = objBAL.Exemption_Land_IndustrialUse(objEntity1);
            if (strReturnStatus == "1" || strReturnStatus == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = './incentiveoffered.aspx';}); </script>", false);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something Went wrong.</strong>');", true);
                return;
            }
           
            ///*---------------------------------------------------------------*/
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }       
    }

    protected void BtnDraft_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
       

        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity1 = new Basic_Unit_Details_Entity();

        try
        {
            #region Production Item Before

            /*---------------------------------------------------------------------*/
            /// Add Production Item Before
            /*---------------------------------------------------------------------*/

            List<BasicProductionItemBefore> listProdBefore = new List<BasicProductionItemBefore>();

            for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
            {
                BasicProductionItemBefore objItem = new BasicProductionItemBefore();

                Label lblProductionName = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
                Label lblQuantity = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
                Label lblOtherUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
                Label lblValue = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
                HiddenField hdnUnit = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");

                objItem.vchProductName = lblProductionName.Text;
                objItem.decQuantity = Convert.ToDecimal(lblQuantity.Text == "" ? "0" : lblQuantity.Text);
                if (hdnUnit.Value != "")
                {
                    objItem.intUnitType = Convert.ToInt32(hdnUnit.Value);
                }
                objItem.vchOtherUnit = lblOtherUnit.Text == "" ? null : lblOtherUnit.Text;
                objItem.decValue = Convert.ToDecimal(lblValue.Text == "" ? "0" : lblValue.Text);
                objItem.chItemFor = "B"; //// Before

                listProdBefore.Add(objItem);
            }

            objEntity1.ProductionItem_BE = listProdBefore;


            #endregion

            #region Land Converted

            /*---------------------------------------------------------------------*/
            /// Add Land Converted
           /*---------------------------------------------------------------------*/

            List<LandConverted> listland = new List<LandConverted>();

            for (int i = 0; i < Grd_Land.Rows.Count; i++)
            {
                LandConverted objlandconvert = new LandConverted();

                Label LblMouza = (Label)Grd_Land.Rows[i].FindControl("Lbl_Mouza");
                Label LblKhataNo = (Label)Grd_Land.Rows[i].FindControl("Lbl_Khata_No");
                Label LblPlotNo = (Label)Grd_Land.Rows[i].FindControl("Lbl_Plot_No");
                Label LblArea = (Label)Grd_Land.Rows[i].FindControl("Lbl_Area");
                Label LblPrsentKisam = (Label)Grd_Land.Rows[i].FindControl("Lbl_Prsent_Kisam");


                objlandconvert.vchMouza = LblMouza.Text;
                objlandconvert.vchKhataNo = LblKhataNo.Text;
                objlandconvert.vchPlotNo = LblPlotNo.Text;
                objlandconvert.vchArea = LblArea.Text;
                objlandconvert.vchPresentKisam = LblPrsentKisam.Text;

                listland.Add(objlandconvert);
            }
            objEntity1.Land_tobe_converted = listland;

            #endregion


            #region IndustrialUnitDetailsSection

            /*---------------------------------------------------------------------*/
            /// Industrial Unit Details Section
            /*---------------------------------------------------------------------*/

            objEntity1.strEnterpriseName = Txt_EnterPrise_Name.Text;
            objEntity1.intUnitCat = Convert.ToInt32(DrpDwn_Unit_Cat.SelectedValue);
            objEntity1.strThrustorPrioritySector = Txt_ThrustorPriority.Text;
            objEntity1.strRegisteredOfcAddress = Txt_Regd_Office_Address.Text;
            objEntity1.intOrganisationType = Convert.ToInt32(DrpDwn_Org_Type.SelectedValue);
            objEntity1.strManagingPartnerGender = DrpDwn_Gender_Partner.SelectedValue;
            objEntity1.strManagingPartnerName = Txt_Partner_Name.Text;
            objEntity1.dtmProdCommBefore = Txt_Commence_Production.Text;
            objEntity1.strEINNO = Txt_EIN_IL_NO.Text == "" ? null : Txt_EIN_IL_NO.Text;
            objEntity1.dtmEIN = Txt_EIN_IL_Date.Text == "" ? null : Txt_EIN_IL_Date.Text;
            objEntity1.dtmFFCIDateBefore = Txt_Proposed_Date.Text;
            objEntity1.strProposedLocation = Txt_Propsed_Location.Text;
            objEntity1.strPrsentStatus = Txt_Status.Text;
            objEntity1.strNameoffinancer = Txt_Financer.Text;
            objEntity1.decCostofproject = Convert.ToDecimal(Txt_Cost_Project.Text);
            objEntity1.decAreaofLandRequired = Convert.ToDecimal(Txt_Land_Required.Text);
            objEntity1.decAreaofLandAcquired = Convert.ToDecimal(Txt_Land_Acquired.Text);
            objEntity1.intParticularsLandtobeconverted = Convert.ToInt32(Rad_Land_converted.SelectedValue);
              
            /*-------------------------Industrial Unit Details End---------------------------------*/
            #endregion



            #region OtherDocumentSection

            /*---------------------------------------------------------------------*/
            /// Other Document Section
            /*---------------------------------------------------------------------*/

            if (HdnEinno_Name.Value != "")
            {
                objEntity1.strEINorPEALapprovalDocCode = HdnEinno_Code.Value;
                objEntity1.strEINorPEALapproval = HdnEinno_Name.Value;
            }
            else
            {
                objEntity1.strEINorPEALapprovalDocCode = null;
                objEntity1.strEINorPEALapproval = null;
            }

            if (HdnPoweratt_Name.Value != "")
            {
                objEntity1.strPworofAttorneyDocCode = HdnPoweratt_Code.Value;
                objEntity1.strPworofAttorney = HdnPoweratt_Name.Value;
            }
            else
            {
                objEntity1.strPworofAttorneyDocCode = null;
                objEntity1.strPworofAttorney = null;
            }

            if (HdnCertofreg_Name.Value != "")
            {
                objEntity1.strCertificateofregistrationDocCode = HdnCertofreg_Code.Value;
                objEntity1.strCertificateofregistration = HdnCertofreg_Name.Value;
            }
            else
            {
                objEntity1.strCertificateofregistrationDocCode = null;
                objEntity1.strCertificateofregistration = null;
            }

            if (HdnFixcapital_Name.Value != "")
            {
                objEntity1.strfixedcapitalinvstDocCode = HdnFixcapital_Code.Value;
                objEntity1.strfixedcapitalinvst = HdnFixcapital_Name.Value;
            }
            else
            {
                objEntity1.strfixedcapitalinvstDocCode = null;
                objEntity1.strfixedcapitalinvst = null;
            }

            if (HdnThrustcerti_Name.Value != "")
            {
                objEntity1.strProvisionalPriorityorThruststatusDocCode = HdnThrustcerti_Code.Value;
                objEntity1.strProvisionalPriorityorThruststatus = HdnThrustcerti_Name.Value;
            }
            else
            {
                objEntity1.strProvisionalPriorityorThruststatusDocCode = null;
                objEntity1.strProvisionalPriorityorThruststatus = null;
            }

            if (HdnAppprovedproj_Name.Value != "")
            {
                objEntity1.strApprovalDetailsprojrctDocCode = HdnAppprovedproj_Code.Value;
                objEntity1.strApprovalDetailsprojrct = HdnAppprovedproj_Name.Value;
            }
            else
            {
                objEntity1.strApprovalDetailsprojrctDocCode = null;
                objEntity1.strApprovalDetailsprojrct = null;
            }

            if (HdnAppraisal_Name.Value != "")
            {
                objEntity1.strApprisalsupportexpansionDocCode = HdnAppraisal_Code.Value;
                objEntity1.strApprisalsupportexpansion = HdnAppraisal_Name.Value;
            }
            else
            {
                objEntity1.strApprisalsupportexpansionDocCode = null;
                objEntity1.strApprisalsupportexpansion = null;
            }

            if (HdnLanddocument_Name.Value != "")
            {
                objEntity1.strLanddocumentDocCode = HdnLanddocument_Code.Value;
                objEntity1.strLanddocument = HdnLanddocument_Name.Value;
            }
            else
            {
                objEntity1.strLanddocumentDocCode = null;
                objEntity1.strLanddocument = null;
            }

            if (HdnStatutory_Name.Value != "")
            {
                objEntity1.strStatutoryClearancesDocCode = HdnStatutory_Code.Value;
                objEntity1.strStatutoryClearances = HdnStatutory_Name.Value;
            }
            else
            {
                objEntity1.strStatutoryClearancesDocCode = null;
                objEntity1.strStatutoryClearances = null;
            }

            if (HdnStampPaper_Name.Value != "")
            {
                objEntity1.strNonjudicialStampDocCode = HdnStampPaper_Code.Value;
                objEntity1.strNonjudicialStamp = HdnStampPaper_Name.Value;
            }
            else
            {
                objEntity1.strNonjudicialStampDocCode = null;
                objEntity1.strNonjudicialStamp = null;
            }

            /*-------------------------Other Document End---------------------------------*/

            #endregion

            ///This method is called to assign  Session value
            FillData();
            objEntity1.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objEntity1.strPcNoAfter = Session["ProposalNo"].ToString();
            objEntity1.strIndustryCode = Session["UnitCode"].ToString();

            if (Request.QueryString["key"] != null)
            {
                objEntity1.strInctFlow = Request.QueryString["key"].ToString();
            }
            else
            {
                objEntity1.strInctFlow = Request.QueryString["IncentiveNo"].ToString();
            }

            /*---------------------------------------------------------------*/
            /// Data Insert and Update
            /// 1-Insert
            /// 2-Update
            /*---------------------------------------------------------------*/
            string strReturnStatus = objBAL.Exemption_Land_IndustrialUse_Draft(objEntity1);
            if (strReturnStatus == "1" || strReturnStatus == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Drafted Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = './incentiveoffered.aspx';}); </script>", false);
                return;
            }         
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something Went wrong.</strong>');", true);
                return;
            }
           
            ///*---------------------------------------------------------------*/
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }        
    }
}