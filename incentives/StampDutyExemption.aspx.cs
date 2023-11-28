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

public partial class incentives_StampDutyExemption : System.Web.UI.Page
{
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    DataTable dtSalutation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["InctUniqueNo"]) || !String.IsNullOrEmpty(Request.QueryString["key"]))
        {
            if (Request.QueryString["key"] != null)
            {
                string strInctId = Request.QueryString["key"].ToString();
            }

            if (Request.QueryString["InctUniqueNo"] != null)
            {
                string UniqueNo = Request.QueryString["InctUniqueNo"].ToString();
                string InctNo = Request.QueryString["IncentiveNo"].ToString();
            }
        }
        if (!IsPostBack)
        {
            fillUnitCategory();
            fillSalutation();
            fillOrgType();
            fillData();
            Txt_EnterPrise_Name.Enabled = false;
            Txt_Other_Unit_Before.Visible = false;
            fillUnitMeasurment();
            if (Request.QueryString["InctUniqueNo"] != null)
            {
               // PostpopulateDataComm(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));

            }
            else
            {
                Txt_EIN_IL_Date.Attributes.Add("readonly", "readonly");
                Txt_Proposed_Date.Attributes.Add("readonly", "readonly");
                Txt_Commence_production.Attributes.Add("readonly", "readonly");


                fillUnitMeasurment();
                fillUnitCategory();
                Txt_Other_Unit_Before.Visible = false;
                Txt_EnterPrise_Name.Enabled = false;
                fillData();
                fillOrgType();
                fillSalutation();
                
            }
        }
       

    }


    //public void PostpopulateDataComm(int id)
    //{
    //    try
    //    {
    //        DataSet dslivePre = IncentiveManager.StampDutyExemptionpopulateDatainDraft(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
    //        DataTable dtindustryPre = dslivePre.Tables[0];////////////Industrial Unit's Details
    //        DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment
    //        DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////Term Loan Details
    //        DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////Working Capital Loan Details
    //        DataTable dtDistrict = dslivePre.Tables[4];/////District

    //        ViewState["salutation"] = dtSalutation;

    //        #region IndustrailUnit
    //        if (dtindustryPre.Rows.Count > 0)
    //        {


    //            Txt_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString().Trim();

    //            DrpDwn_Unit_Cat.SelectedValue = (dtindustryPre.Rows[0]["intUnitCat"].ToString());
    //            Txt_Industry_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();
                
               
    //            DrpDwn_Org_Type.SelectedValue = (dtindustryPre.Rows[0]["intOrganisationType"].ToString());

    //            if (DrpDwn_Org_Type.SelectedValue == "15")
    //            {
    //                Lbl_Org_Name_Type.Text = "Name of Proprietor";
    //            }
    //            else if (DrpDwn_Org_Type.SelectedValue == "17")
    //            {
    //                Lbl_Org_Name_Type.Text = "Name of Managing Partner";
    //            }
    //            else if (DrpDwn_Org_Type.SelectedValue == "20")
    //            {
    //                Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
    //            }
    //            else if (DrpDwn_Org_Type.SelectedValue == "19")
    //            {
    //                Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
    //            }
    //            else if (DrpDwn_Org_Type.SelectedValue == "18")
    //            {
    //                Lbl_Org_Name_Type.Text = "Name of Authorized Signatory";
    //            }
    //            Txt_Partner_Name.Text = dtindustryPre.Rows[0]["vchManagingPartnerName"].ToString();
    //            DrpDwn_Gender_Partner.SelectedValue = dtindustryPre.Rows[0]["vchManagingPartnerGender"].ToString();
    //            Txt_EIN_IL_NO.Text = dtindustryPre.Rows[0]["vchEINNO"].ToString();
    //            Txt_EIN_IL_Date.Text = dtindustryPre.Rows[0]["dtmEIN"].ToString();
    //            Txt_Propsed_location.Text= dtindustryPre.Rows[0]["vchProposedLocation"].ToString();
    //            Txt_Status.Text = dtindustryPre.Rows[0]["vchPrsentStatus"].ToString();
    //            Txt_deed.Text= dtindustryPre.Rows[0]["vchDeedoragreement"].ToString();
    //            Txt_Stampduty_claimed.Text= dtProductionPre.Rows[0]["decSdeClaimed"].ToString();
    //            Txt_Availed.Text= dtProductionPre.Rows[0]["decAmountAvailed"].ToString();
    //            txt_Deferential.Text= dtProductionPre.Rows[0]["decDeferentialClaim"].ToString();

    //            Grd_Production_Before.DataSource = dtProductionDetBefPre;
    //            Grd_Production_Before.DataBind();
    //       // *--------------------------------------------------------------------------------------------------------------------------*//

    //            #region Production



    //            if (DrpDwn_Unit_Before.SelectedIndex > 0)
    //            {
    //                if (DrpDwn_Unit_Before.SelectedValue == "52")
    //                {
    //                    Txt_Other_Unit_Before.Visible = true;
    //                    Txt_Other_Unit_Before.Focus();
    //                }
    //                else
    //                {
    //                    Txt_Other_Unit_Before.Text = "";
    //                    Txt_Other_Unit_Before.Visible = false;
    //                    Txt_Value_Before.Focus();
    //                }
    //            }
    //            else
    //            {
    //                Txt_Other_Unit_Before.Text = "";
    //                Txt_Other_Unit_Before.Visible = false;
    //            }

    //            #endregion
    //            Txt_Proposed_Date.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
    //            Txt_Commence_production.Text = dtProductionPre.Rows[0]["dtmFFCIDateBefore"].ToString();
              

    //            /*--------------------------------------------------------------------------------------------------------------------------*/

              

    //            //#region MEANS OF FINANCE

            
    //            Txt_clearances.Text = dtindustryPre.Rows[0]["vchClearnceswm"].ToString();

    //            ///----------------------------------Other File Upload---------------------------------------------------------------------/////

    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D309")
    //            {
    //                hdnEinno_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypEinno.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypEinno.Visible = true;
    //                lnkDEinno.Visible = true;
    //                lblEinno.Visible = true;
    //                flEinno.Enabled = false;
    //            }
    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D310")
    //            {
    //                hdnPoweratt_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypPoweratt.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypPoweratt.Visible = true;
    //                lnkDPoweratt.Visible = true;
    //                lblPoweratt.Visible = true;
    //                flPoweratt.Enabled = false;
    //            }
    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D311")
    //            {
    //                certofreg_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwcertofreg.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwcertofreg.Visible = true;
    //                lnkDcertofreg.Visible = true;
    //                lblcertofreg.Visible = true;
    //                flcertofreg.Enabled = false;
    //            }
    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D312")
    //            {
    //                hdnfixcapital_Name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwfixcapital.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwfixcapital.Visible = true;
    //                lnkDfixcapital.Visible = true;
    //                lblfixcapital.Visible = true;
    //                flfixcapital.Enabled = false;
    //            }
    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D313")
    //            {
    //                hdnAppraisal_Name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwAppraisal.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwAppraisal.Visible = true;
    //                lnkDAppraisal.Visible = true;
    //                lblAppraisal.Visible = true;
    //                flAppraisal.Enabled = false;
    //            }
    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D314")
    //            {
    //                hdnCommproduction_Name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hyVwCommproduction.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hyVwCommproduction.Visible = true;
    //                lnkDCommproduction.Visible = true;
    //                lblCommproduction.Visible = true;
    //                flCommproduction.Enabled = false;
    //            }
    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D315")
    //            {
    //                hdnMigrationindust_Name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwMigrationindust.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwMigrationindust.Visible = true;
    //                lnkDMigrationindust.Visible = true;
    //                lblMigrationindust.Visible = true;
    //                flMigrationindust.Enabled = false;
    //            }

    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D316")
    //            {
    //                hdnPrivateIndust_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwPrivateIndust.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwPrivateIndust.Visible = true;
    //                lnkDPrivateIndust.Visible = true;
    //                lblPrivateIndust.Visible = true;
    //                flPrivateIndust.Enabled = false;
    //            }

    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D317")
    //            {
    //                hdnDeed_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwDeed.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwDeed.Visible = true;
    //                lnkDDeed.Visible = true;
    //                lblDeed.Visible = true;
    //                flDeed.Enabled = false;
    //            }
    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D318")
    //            {
    //                hdnFinancialAssets_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwFinancialAssets.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypVwFinancialAssets.Visible = true;
    //                lnkDFinancialAssets.Visible = true;
    //                lblFinancialAssets.Visible = true;
    //                flFinancialAssets.Enabled = false;
    //            }

    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D319")
    //            {
    //                hdnexemption_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypvwexemption.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypvwexemption.Visible = true;
    //                lnkDexemption.Visible = true;
    //                lblexemption.Visible = true;
    //                flexemption.Enabled = false;
    //            }

    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D320")
    //            {
    //                hdnstatutory_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypvwstatutory.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypvwstatutory.Visible = true;
    //                lnkDstatutory.Visible = true;
    //                lblstatutory.Visible = true;
    //                flstatutory.Enabled = false;
    //            }
    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D321")
    //            {
    //                hdnStampPaper_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypvwStampPaper.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypvwStampPaper.Visible = true;
    //                lnkDStampPaper.Visible = true;
    //                lblStampPaper.Visible = true;
    //                flStampPaper.Enabled = false;
    //            }
    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D322")
    //            {
    //                hdnThrustcerti_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypvwThrustcerti.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypvwThrustcerti.Visible = true;
    //                lnkDThrustcerti.Visible = true;
    //                lblThrustcerti.Visible = true;
    //                flThrustcerti.Enabled = false;
    //            }
    //            if (dtProductionDetAftPre.Rows[0]["vchDocId"].ToString() == "D323")
    //            {
    //                hdnEmpCommittee_name.Value = dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypvwEmpCommittee.NavigateUrl = "../incentives/Files/InctBasicDoc/" + dtProductionDetAftPre.Rows[0]["vchFileName"].ToString();
    //                hypvwEmpCommittee.Visible = true;
    //                lnkDEmpCommittee.Visible = true;
    //                lblEmpCommittee.Visible = true;
    //                flEmpCommittee.Enabled = false;
    //            }

    //            ///-------------------------------------End--------------------------------------------------------------------------------////




    //            #endregion

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "StampDutyExemptionIPR2022");
    //    }


    //}

    private void fillUnitCategory()
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
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    private void fillSalutation()
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
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    private void fillOrgType()
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
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    private void fillUnitMeasurment()
    {
        DataTable table = new DataTable();
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
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }
        finally
        {
            table = null;
        }
    }

    private void fillData()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity = new Basic_Unit_Details_Entity();
        DataSet ds = new DataSet();
        try
        {
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            ds = objBAL.Basic_Unit_Details_V(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                /*----------------------------------------------------------------------------*/
                ///// Common Information 

                string strDataSource = ds.Tables[0].Rows[0]["vch_Data_Source"].ToString();
                string strPcStatus = ds.Tables[0].Rows[0]["vch_PC_Status"].ToString();
                string strIsExistBefore = ds.Tables[0].Rows[0]["vch_Is_Before_Exist"].ToString();
                string strIsExistAfter = ds.Tables[0].Rows[0]["vch_Is_After_Exist"].ToString();
                string strIndustryCode = ds.Tables[0].Rows[0]["vch_Industry_Code"].ToString();
                string strProposalNo = ds.Tables[0].Rows[0]["vch_Proposal_No"].ToString();
                string strProjectType = ds.Tables[0].Rows[0]["int_Project_Type"].ToString();
                string strNewPcFound = ds.Tables[0].Rows[0]["vch_New_PC_Found"].ToString();

                /*----------------------------------------------------------------------------*/
                ////// If new PC found then assign strDataSource=PC
                ////// Only when data present in basic table and a new PC found 
                /*----------------------------------------------------------------------------*/
                if (strDataSource == "BASIC")
                {
                    if (strNewPcFound == "Y")
                    {
                        strDataSource = "PC";
                    }
                }
                /*----------------------------------------------------------------------------*/
                ////// Value Assigned to HiddenField for use in Validation
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
                ///// Session Assigned Here

                Session["UnitCode"] = strIndustryCode;
                Session["ProposalNo"] = strProposalNo;
            }


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
            ds = null;
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
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }
    }

    private void UpdFileRemove(HiddenField Hid_File_Name, LinkButton LnkBtn_Upload_Doc, LinkButton LnkBtn_Delete_Doc, HyperLink Hyp_Document, Label Lbl_Upload_Msg, FileUpload FileUpload_Document, string strFolername)
    {
        try
        {
            string filename = Hid_File_Name.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolername, filename);
            string completePath = Server.MapPath(path);
            //if (File.Exists(completePath))
            //{
            //    File.Delete(completePath);
            //}

            Hid_File_Name.Value = "";
            LnkBtn_Delete_Doc.Visible = false;
            LnkBtn_Upload_Doc.Visible = true;
            Hyp_Document.Visible = false;
            Lbl_Upload_Msg.Visible = false;
            FileUpload_Document.Enabled = true;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "StampDutyExemptionIPR2022");
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
            Util.LogError(ex, "StampDutyExemptionIPR2022");
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
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }
        finally
        {
            table = null;
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
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }
    }


    protected void LnkBtn_Add_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkUEinno.ID))
            {
                if (flEinno.HasFile)
                {

                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_EinnoStampdutyIPR-2022";
                    UploadDocument(flEinno, hdnEinno_name, strFileName, hypEinno, lblEinno, lnkDEinno, "InctBasicDoc");
                }

            }
            else if (string.Equals(lnk.ID, lnkUPoweratt.ID))
            {
                if (flPoweratt.HasFile)
                {

                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PoweofAttorneyStampdutyIPR-2022";
                    UploadDocument(flPoweratt, hdnPoweratt_name, strFileName, hypPoweratt, lblPoweratt, lnkDPoweratt, "InctBasicDoc");
                }

            }
            else if (string.Equals(lnk.ID, lnkUcertofreg.ID))
            {
                if (flcertofreg.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_MEMORANDUMStampdutyIPR-2022";
                    UploadDocument(flcertofreg, certofreg_name, strFileName, hypVwcertofreg, lblcertofreg, lnkDcertofreg, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUfixcapital.ID))
            {
                if (flfixcapital.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_FixCapitalInvstStampdutyIPR-2022";
                    UploadDocument(flfixcapital, hdnfixcapital_Name, strFileName, hypVwfixcapital, lblfixcapital, lnkDfixcapital, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUAppraisal.ID))
            {
                if (flAppraisal.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_AppraisalStampdutyIPR-2022";
                    UploadDocument(flAppraisal, hdnAppraisal_Name, strFileName, hypVwAppraisal, lblAppraisal, lnkDAppraisal, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUCommproduction.ID))
            {
                if (flCommproduction.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_CommercialproductionStampdutyIPR-2022";
                    UploadDocument(flCommproduction, hdnCommproduction_Name, strFileName, hyVwCommproduction, lblCommproduction, lnkDCommproduction, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUMigrationindust.ID))
            {
                if (flMigrationindust.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_MigrationindustrialStampdutyIPR-2022";
                    UploadDocument(flMigrationindust, hdnMigrationindust_Name, strFileName, hypVwMigrationindust, lblMigrationindust, lnkDMigrationindust, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUPrivateIndust.ID))
            {
                if (flPrivateIndust.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PrivateIndustrialStampdutyIPR-2022";
                    UploadDocument(flPrivateIndust, hdnPrivateIndust_name, strFileName, hypVwPrivateIndust, lblPrivateIndust, lnkDPrivateIndust, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUDeed.ID))
            {
                if (flDeed.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_DeedStampdutyIPR-2022";
                    UploadDocument(flDeed, hdnDeed_name, strFileName, hypVwDeed, lblDeed, lnkDDeed, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUFinancialAssets.ID))
            {
                if (flFinancialAssets.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_FinancialAssetsStampdutyIPR-2022";
                    UploadDocument(flFinancialAssets, hdnFinancialAssets_name, strFileName, hypVwFinancialAssets, lblFinancialAssets, lnkDFinancialAssets, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUexemption.ID))
            {
                if (flexemption.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ExemptionStampdutyIPR-2022";
                    UploadDocument(flexemption, hdnexemption_name, strFileName, hypvwexemption, lblexemption, lnkDexemption, "InctBasicDoc");
                }
            }

            else if (string.Equals(lnk.ID, lnkUstatutory.ID))
            {
                if (flstatutory.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_StatutoryStampdutyIPR-2022";
                    UploadDocument(flstatutory, hdnstatutory_name, strFileName, hypvwstatutory, lblstatutory, lnkDstatutory, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUStampPaper.ID))
            {
                if (flStampPaper.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_StampPaperStampdutyIPR-2022";
                    UploadDocument(flStampPaper, hdnStampPaper_name, strFileName, hypvwStampPaper, lblStampPaper, lnkDStampPaper, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUThrustcerti.ID))
            {
                if (flThrustcerti.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ThrustCertificateStampdutyIPR-2022";
                    UploadDocument(flThrustcerti, hdnThrustcerti_name, strFileName, hypvwThrustcerti, lblThrustcerti, lnkDThrustcerti, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUEmpCommittee.ID))
            {
                if (flEmpCommittee.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_EmpoweredCommitteeStampdutyIPR-2022";
                    UploadDocument(flEmpCommittee, hdnEmpCommittee_name, strFileName, hypvwEmpCommittee, lblEmpCommittee, lnkDEmpCommittee, "InctBasicDoc");
                }
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }

    }


    protected void LnkBtn_Delete_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkDEinno.ID))
            {
                UpdFileRemove(hdnEinno_name, lnkUEinno, lnkDEinno, hypEinno, lblEinno, flEinno, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDPoweratt.ID))
            {
                UpdFileRemove(hdnPoweratt_name, lnkUPoweratt, lnkDPoweratt, hypPoweratt, lblPoweratt, flPoweratt, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDcertofreg.ID))
            {
                UpdFileRemove(certofreg_name, lnkUcertofreg, lnkDcertofreg, hypVwcertofreg, lblcertofreg, flcertofreg, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDfixcapital.ID))
            {
                UpdFileRemove(hdnfixcapital_Name, lnkUfixcapital, lnkDfixcapital, hypVwfixcapital, lblfixcapital, flfixcapital, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDAppraisal.ID))
            {
                UpdFileRemove(hdnAppraisal_Name, lnkUAppraisal, lnkDAppraisal, hypVwAppraisal, lblAppraisal, flAppraisal, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDCommproduction.ID))
            {
                UpdFileRemove(hdnCommproduction_Name, lnkUCommproduction, lnkDCommproduction, hyVwCommproduction, lblCommproduction, flCommproduction, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDMigrationindust.ID))
            {
                UpdFileRemove(hdnMigrationindust_Name, lnkUMigrationindust, lnkDMigrationindust, hypVwMigrationindust, lblMigrationindust, flMigrationindust, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDPrivateIndust.ID))
            {
                UpdFileRemove(hdnPrivateIndust_name, lnkUPrivateIndust, lnkDPrivateIndust, hypVwPrivateIndust, lblPrivateIndust, flPrivateIndust, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDDeed.ID))
            {
                UpdFileRemove(hdnDeed_name, lnkUDeed, lnkDDeed, hypVwDeed, lblDeed, flDeed, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDFinancialAssets.ID))
            {
                UpdFileRemove(hdnFinancialAssets_name, lnkUFinancialAssets, lnkDFinancialAssets, hypVwFinancialAssets, lblFinancialAssets, flFinancialAssets, "InctBasicDoc");
            }

            else if (string.Equals(lnk.ID, lnkDexemption.ID))
            {
                UpdFileRemove(hdnexemption_name, lnkUexemption, lnkDexemption, hypvwexemption, lblexemption, flexemption, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDstatutory.ID))
            {
                UpdFileRemove(hdnstatutory_name, lnkUstatutory, lnkDstatutory, hypvwstatutory, lblstatutory, flstatutory, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDStampPaper.ID))
            {
                UpdFileRemove(hdnStampPaper_name, lnkUStampPaper, lnkDStampPaper, hypvwStampPaper, lblStampPaper, flStampPaper, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDThrustcerti.ID))
            {
                UpdFileRemove(hdnThrustcerti_name, lnkUThrustcerti, lnkDThrustcerti, hypvwThrustcerti, lblThrustcerti, flThrustcerti, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDEmpCommittee.ID))
            {
                UpdFileRemove(hdnEmpCommittee_name, lnkUEmpCommittee, lnkDEmpCommittee, hypvwEmpCommittee, lblEmpCommittee, flEmpCommittee, "InctBasicDoc");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }

    }

    protected void BtnApply_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender2.Hide();
        //DataSet ds = new DataSet();

        //IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        //Basic_Unit_Details_Entity objEntity1 = new Basic_Unit_Details_Entity();

        //try
        //{
        //    #region Production Item Before

        //    /*---------------------------------------------------------------------*/
        //    ////// Add Production Item Before

        //    BasicProductionItemBefore objProdBefore = new BasicProductionItemBefore();
        //    List<BasicProductionItemBefore> listProdBefore = new List<BasicProductionItemBefore>();

        //    for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
        //    {
        //        BasicProductionItemBefore objItem = new BasicProductionItemBefore();

        //        Label lblProductionName = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
        //        Label lblQuantity = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
        //        Label lblUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
        //        Label lblOtherUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
        //        Label lblValue = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
        //        HiddenField hdnUnit = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");

        //        objItem.vchProductName = lblProductionName.Text;
        //        objItem.decQuantity = Convert.ToDecimal(lblQuantity.Text == "" ? "0" : lblQuantity.Text);
        //        if (hdnUnit.Value != "")
        //        {
        //            objItem.intUnitType = Convert.ToInt32(hdnUnit.Value);
        //        }
        //        objItem.vchOtherUnit = lblOtherUnit.Text == "" ? null : lblOtherUnit.Text;
        //        objItem.decValue = Convert.ToDecimal(lblValue.Text == "" ? "0" : lblValue.Text);
        //        objItem.chItemFor = "B"; //// Before

        //        listProdBefore.Add(objItem);
        //    }

        //    objEntity1.ProductionItem_BE = listProdBefore;

        //    #endregion

        //    /*---------------------------------------------------------------------*/
        //    /////// Industrial Unit Details Section

        //    objEntity1.strEnterpriseName = Txt_EnterPrise_Name.Text;
        //    objEntity1.intOrganisationType = Convert.ToInt32(DrpDwn_Org_Type.SelectedValue);            
        //    objEntity1.intUnitCat = Convert.ToInt32(DrpDwn_Unit_Cat.SelectedValue);
        //    objEntity1.strRegisteredOfcAddress = Txt_Industry_Address.Text;
        //    objEntity1.strManagingPartnerGender = DrpDwn_Gender_Partner.SelectedValue;
        //    objEntity1.strManagingPartnerName = Txt_Partner_Name.Text;
        //    objEntity1.dtmFFCIDateBefore = Txt_Commence_production.Text;
        //    objEntity1.dtmProdCommBefore = Txt_Proposed_Date.Text;
        //    objEntity1.strEINNO = Txt_EIN_IL_NO.Text == "" ? null : Txt_EIN_IL_NO.Text;
        //    objEntity1.dtmEIN = Txt_EIN_IL_Date.Text == "" ? null : Txt_EIN_IL_Date.Text;
        //    objEntity1.strProposedLocation = Txt_Propsed_location.Text;
        //    objEntity1.strPrsentStatus = Txt_Status.Text;

        //    /*-------------------------Industrial Unit Details End---------------------------------*/

        //    #region MeansofFinanceSection

        //    /*----------------------------------------------------------*/
        //    //// Means of Finance Section

        //    objEntity1.strClearnceswm = Txt_clearances.Text;
        //    objEntity1.strDeed = Txt_deed.Text;
        //    objEntity1.decSdeClaimed =Convert.ToDecimal (Txt_Stampduty_claimed.Text);
        //    objEntity1.decAmountAvailed = Convert.ToDecimal(Txt_Availed.Text);
        //    objEntity1.decDeferentialClaim = Convert.ToDecimal(txt_Deferential.Text);

        //    #endregion

        //    #region OtherDocumentSection

        //    ///*---------------------------------------------------------------------*/
        //    ///////// Other Document Section

        //    ///*----------------------------------------------------------*/
        //    ///*--------------------------Pre-Production Document Section Start-----------------------------------------------*/
        //    if (hdnEinno_name.Value != "")
        //    {
        //        objEntity1.strEINorPEALapprovalDocCode = hdnEinno_code.Value;
        //        objEntity1.strEINorPEALapproval = hdnEinno_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strEINorPEALapprovalDocCode = null;
        //        objEntity1.strEINorPEALapproval = null;
        //    }
        //    if (hdnPoweratt_name.Value != "")
        //    {
        //        objEntity1.strPworofAttorneyDocCode = hdnPoweratt_code.Value;
        //        objEntity1.strPworofAttorney = hdnPoweratt_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strPworofAttorneyDocCode = null;
        //        objEntity1.strPworofAttorney = null;
        //    }
        //    if (certofreg_name.Value != "")
        //    {
        //        objEntity1.strCertificateofregistrationDocCode = certofreg_code.Value;
        //        objEntity1.strCertificateofregistration = certofreg_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strCertificateofregistrationDocCode = null;
        //        objEntity1.strCertificateofregistration = null;
        //    }
        //    if (hdnfixcapital_Name.Value != "")
        //    {
        //        objEntity1.strfixedcapitalinvstDocCode = hdnfixcapital_Code.Value;
        //        objEntity1.strfixedcapitalinvst = hdnfixcapital_Name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strfixedcapitalinvstDocCode = null;
        //        objEntity1.strfixedcapitalinvst = null;
        //    }
        //    if (hdnAppraisal_Name.Value != "")
        //    {
        //        objEntity1.strAppraisalThrustorPriorityDocCode = hdnAppraisal_Code.Value;
        //        objEntity1.strAppraisalThrustorPriority = hdnAppraisal_Name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strAppraisalThrustorPriorityDocCode = null;
        //        objEntity1.strAppraisalThrustorPriority = null;
        //    }
        //    if (hdnCommproduction_Name.Value != "")
        //    {
        //        objEntity1.strCertficateofcommproductionDocCode = hdnCommproduction_Code.Value;
        //        objEntity1.strCertficateofcommproduction = hdnCommproduction_Name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strCertficateofcommproductionDocCode = null;
        //        objEntity1.strCertficateofcommproduction = null;
        //    }
        //    if (hdnMigrationindust_Name.Value != "")
        //    {
        //        objEntity1.strCertficateofmigrationunitDocCode = hdnMigrationindust_Code.Value;
        //        objEntity1.strCertficateofmigrationunit = hdnMigrationindust_Name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strCertficateofmigrationunitDocCode = null;
        //        objEntity1.strCertficateofmigrationunit = null;
        //    }
        //    if (hdnPrivateIndust_name.Value != "")
        //    {
        //        objEntity1.strPrivateindustDocCode = hdnPrivateIndust_code.Value;
        //        objEntity1.strPrivateindust = hdnPrivateIndust_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strPrivateindustDocCode = null;
        //        objEntity1.strPrivateindust = null;
        //    }
        //    if (hdnDeed_name.Value != "")
        //    {
        //        objEntity1.strDeedorAgreementDocCode = hdnDeed_code.Value;
        //        objEntity1.strDeedorAgreement = hdnDeed_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strDeedorAgreementDocCode = null;
        //        objEntity1.strDeedorAgreement = null;
        //    }
        //    if (hdnFinancialAssets_name.Value != "")
        //    {
        //        objEntity1.strSupportoftransferunitDocCode = hdnFinancialAssets_code.Value;
        //        objEntity1.strSupportoftransferunit = hdnFinancialAssets_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strSupportoftransferunitDocCode = null;
        //        objEntity1.strSupportoftransferunit = null;
        //    }
        //    if (hdnexemption_name.Value != "")
        //    {
        //        objEntity1.strProvisionsenunciatedDocCode = hdnexemption_code.Value;
        //        objEntity1.strProvisionsenunciated = hdnexemption_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strProvisionsenunciatedDocCode = null;
        //        objEntity1.strProvisionsenunciated = null;
        //    }
        //    if (hdnstatutory_name.Value != "")
        //    {
        //        objEntity1.strValidstatutoryclearancesDocCode = hdnstatutory_code.Value;
        //        objEntity1.strValidstatutoryclearances = hdnstatutory_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strValidstatutoryclearancesDocCode = null;
        //        objEntity1.strValidstatutoryclearances = null;
        //    }
        //    if (hdnStampPaper_name.Value != "")
        //    {
        //        objEntity1.strStamppaperdulyDocCode = hdnStampPaper_code.Value;
        //        objEntity1.strStamppaperduly = hdnStampPaper_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strStamppaperdulyDocCode = null;
        //        objEntity1.strStamppaperduly = null;
        //    }
        //    if (hdnThrustcerti_name.Value != "")
        //    {
        //        objEntity1.strProvisionalPrioritycetificateDocCode = hdnThrustcerti_code.Value;
        //        objEntity1.strProvisionalPrioritycetificate = hdnThrustcerti_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strProvisionalPrioritycetificateDocCode = null;
        //        objEntity1.strProvisionalPrioritycetificate = null;
        //    }
        //    if (hdnEmpCommittee_name.Value != "")
        //    {
        //        objEntity1.strEmpoweredCommitteeDocCode = hdnEmpCommittee_code.Value;
        //        objEntity1.strEmpoweredCommittee = hdnEmpCommittee_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strEmpoweredCommitteeDocCode = null;
        //        objEntity1.strEmpoweredCommittee = null;
        //    }
        //    /*--------------------------Pre-Production Document Section End-----------------------------------------------*/

        //    /*-------------------------Other Document End---------------------------------*/


        //    #endregion

        //    //objEntity1.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);     

        //    fillData();
        //    objEntity1.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);

        //    objEntity1.strPcNoAfter = Session["ProposalNo"].ToString();
        //    objEntity1.strIndustryCode = Session["UnitCode"].ToString();

        //    if (Request.QueryString["key"] != null)
        //    {
        //        objEntity1.strInctFlow = Request.QueryString["key"].ToString();

        //    }
        //    else
        //    {
        //        objEntity1.strInctFlow = Request.QueryString["IncentiveNo"].ToString();
        //    }

        //    /*---------------------------------------------------------------*/
        //    /////// Data Insert and Update

        //    string strReturnStatus = objBAL.Stamp_Duty_Exemption_AED(objEntity1);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = './incentiveoffered.aspx';}); </script>", false);
        //    return;
        //    ModalPopupExtender1.Show();

        //    ///*---------------------------------------------------------------*/

        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "StampDutyExemptionIPR2022");
        //}
        //finally
        //{
        //    objBAL = null;
        //    objEntity1 = null;
        //}
    }

    protected void BtnDraft_Click(object sender, EventArgs e)
    {
        //ModalPopupExtender2.Hide();
        //DataSet ds = new DataSet();

        //IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        //Basic_Unit_Details_Entity objEntity1 = new Basic_Unit_Details_Entity();

        //try
        //{
        //    #region Production Item Before

        //    /*---------------------------------------------------------------------*/
        //    ////// Add Production Item Before

        //    BasicProductionItemBefore objProdBefore = new BasicProductionItemBefore();
        //    List<BasicProductionItemBefore> listProdBefore = new List<BasicProductionItemBefore>();

        //    for (int i = 0; i < Grd_Production_Before.Rows.Count; i++)
        //    {
        //        BasicProductionItemBefore objItem = new BasicProductionItemBefore();

        //        Label lblProductionName = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Product_Name_Before");
        //        Label lblQuantity = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Quantity_Before");
        //        Label lblUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Unit_Before");
        //        Label lblOtherUnit = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Other_Unit_Before");
        //        Label lblValue = (Label)Grd_Production_Before.Rows[i].FindControl("Lbl_Value_Before");
        //        HiddenField hdnUnit = (HiddenField)Grd_Production_Before.Rows[i].FindControl("Hid_Unit_Before");

        //        objItem.vchProductName = lblProductionName.Text;
        //        objItem.decQuantity = Convert.ToDecimal(lblQuantity.Text == "" ? "0" : lblQuantity.Text);
        //        if (hdnUnit.Value != "")
        //        {
        //            objItem.intUnitType = Convert.ToInt32(hdnUnit.Value);
        //        }
        //        objItem.vchOtherUnit = lblOtherUnit.Text == "" ? null : lblOtherUnit.Text;
        //        objItem.decValue = Convert.ToDecimal(lblValue.Text == "" ? "0" : lblValue.Text);
        //        objItem.chItemFor = "B"; //// Before

        //        listProdBefore.Add(objItem);
        //    }

        //    objEntity1.ProductionItem_BE = listProdBefore;

        //    #endregion

        //    /*---------------------------------------------------------------------*/
        //    /////// Industrial Unit Details Section

        //    objEntity1.strEnterpriseName = Txt_EnterPrise_Name.Text;
        //    objEntity1.intOrganisationType = Convert.ToInt32(DrpDwn_Org_Type.SelectedValue);
        //    objEntity1.intUnitCat = Convert.ToInt32(DrpDwn_Unit_Cat.SelectedValue);
        //    objEntity1.strRegisteredOfcAddress = Txt_Industry_Address.Text;
        //    objEntity1.strManagingPartnerGender = DrpDwn_Gender_Partner.SelectedValue;
        //    objEntity1.strManagingPartnerName = Txt_Partner_Name.Text;
        //    objEntity1.dtmFFCIDateBefore = Txt_Commence_production.Text;
        //    objEntity1.dtmProdCommBefore = Txt_Proposed_Date.Text;
        //    objEntity1.strEINNO = Txt_EIN_IL_NO.Text == "" ? null : Txt_EIN_IL_NO.Text;
        //    objEntity1.dtmEIN = Txt_EIN_IL_Date.Text == "" ? null : Txt_EIN_IL_Date.Text;
        //    objEntity1.strProposedLocation = Txt_Propsed_location.Text;
        //    objEntity1.strPrsentStatus = Txt_Status.Text;

        //    /*-------------------------Industrial Unit Details End---------------------------------*/

        //    #region MeansofFinanceSection

        //    /*----------------------------------------------------------*/
        //    //// Means of Finance Section

        //    objEntity1.strClearnceswm = Txt_clearances.Text;
        //    objEntity1.strDeed = Txt_deed.Text ; 
        //    objEntity1.decSdeClaimed = Convert.ToDecimal(Txt_Stampduty_claimed.Text);
        //    objEntity1.decAmountAvailed = Convert.ToDecimal(Txt_Availed.Text);
        //    objEntity1.decDeferentialClaim = Convert.ToDecimal(txt_Deferential.Text);

        //    #endregion

        //    #region OtherDocumentSection

        // ///*---------------------------------------------------------------------*/
        //    ///////// Other Document Section

        // ///*---------------------------------------------------------------------*/
          
        //    if (hdnEinno_name.Value != "")
        //    {
        //        objEntity1.strEINorPEALapprovalDocCode = hdnEinno_code.Value;
        //        objEntity1.strEINorPEALapproval = hdnEinno_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strEINorPEALapprovalDocCode = null;
        //        objEntity1.strEINorPEALapproval = null;
        //    }
        //    if (hdnPoweratt_name.Value != "")
        //    {
        //        objEntity1.strPworofAttorneyDocCode = hdnPoweratt_code.Value;
        //        objEntity1.strPworofAttorney = hdnPoweratt_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strPworofAttorneyDocCode = null;
        //        objEntity1.strPworofAttorney = null;
        //    }
        //    if (certofreg_name.Value != "")
        //    {
        //        objEntity1.strCertificateofregistrationDocCode = certofreg_code.Value;
        //        objEntity1.strCertificateofregistration = certofreg_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strCertificateofregistrationDocCode = null;
        //        objEntity1.strCertificateofregistration = null;
        //    }
        //    if (hdnfixcapital_Name.Value != "")
        //    {
        //        objEntity1.strfixedcapitalinvstDocCode = hdnfixcapital_Code.Value;
        //        objEntity1.strfixedcapitalinvst = hdnfixcapital_Name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strfixedcapitalinvstDocCode = null;
        //        objEntity1.strfixedcapitalinvst = null;
        //    }
        //    if (hdnAppraisal_Name.Value != "")
        //    {
        //        objEntity1.strAppraisalThrustorPriorityDocCode = hdnAppraisal_Code.Value;
        //        objEntity1.strAppraisalThrustorPriority = hdnAppraisal_Name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strAppraisalThrustorPriorityDocCode = null;
        //        objEntity1.strAppraisalThrustorPriority = null;
        //    }
        //    if (hdnCommproduction_Name.Value != "")
        //    {
        //        objEntity1.strCertficateofcommproductionDocCode = hdnCommproduction_Code.Value;
        //        objEntity1.strCertficateofcommproduction = hdnCommproduction_Name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strCertficateofcommproductionDocCode = null;
        //        objEntity1.strCertficateofcommproduction = null;
        //    }
        //    if (hdnMigrationindust_Name.Value != "")
        //    {
        //        objEntity1.strCertficateofmigrationunitDocCode = hdnMigrationindust_Code.Value;
        //        objEntity1.strCertficateofmigrationunit = hdnMigrationindust_Name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strCertficateofmigrationunitDocCode = null;
        //        objEntity1.strCertficateofmigrationunit = null;
        //    }
        //    if (hdnPrivateIndust_name.Value != "")
        //    {
        //        objEntity1.strPrivateindustDocCode = hdnPrivateIndust_code.Value;
        //        objEntity1.strPrivateindust = hdnPrivateIndust_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strPrivateindustDocCode = null;
        //        objEntity1.strPrivateindust = null;
        //    }
        //    if (hdnDeed_name.Value != "")
        //    {
        //        objEntity1.strDeedorAgreementDocCode = hdnDeed_code.Value;
        //        objEntity1.strDeedorAgreement = hdnDeed_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strDeedorAgreementDocCode = null;
        //        objEntity1.strDeedorAgreement = null;
        //    }
        //    if (hdnFinancialAssets_name.Value != "")
        //    {
        //        objEntity1.strSupportoftransferunitDocCode = hdnFinancialAssets_code.Value;
        //        objEntity1.strSupportoftransferunit = hdnFinancialAssets_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strSupportoftransferunitDocCode = null;
        //        objEntity1.strSupportoftransferunit = null;
        //    }
        //    if (hdnexemption_name.Value != "")
        //    {
        //        objEntity1.strProvisionsenunciatedDocCode = hdnexemption_code.Value;
        //        objEntity1.strProvisionsenunciated = hdnexemption_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strProvisionsenunciatedDocCode = null;
        //        objEntity1.strProvisionsenunciated = null;
        //    }
        //    if (hdnstatutory_name.Value != "")
        //    {
        //        objEntity1.strValidstatutoryclearancesDocCode = hdnstatutory_code.Value;
        //        objEntity1.strValidstatutoryclearances = hdnstatutory_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strValidstatutoryclearancesDocCode = null;
        //        objEntity1.strValidstatutoryclearances = null;
        //    }
        //    if (hdnStampPaper_name.Value != "")
        //    {
        //        objEntity1.strStamppaperdulyDocCode = hdnStampPaper_code.Value;
        //        objEntity1.strStamppaperduly = hdnStampPaper_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strStamppaperdulyDocCode = null;
        //        objEntity1.strStamppaperduly = null;
        //    }
        //    if (hdnThrustcerti_name.Value != "")
        //    {
        //        objEntity1.strProvisionalPrioritycetificateDocCode = hdnThrustcerti_code.Value;
        //        objEntity1.strProvisionalPrioritycetificate = hdnThrustcerti_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strProvisionalPrioritycetificateDocCode = null;
        //        objEntity1.strProvisionalPrioritycetificate = null;
        //    }
        //    if (hdnEmpCommittee_name.Value != "")
        //    {
        //        objEntity1.strEmpoweredCommitteeDocCode = hdnEmpCommittee_code.Value;
        //        objEntity1.strEmpoweredCommittee = hdnEmpCommittee_name.Value;
        //    }
        //    else
        //    {
        //        objEntity1.strEmpoweredCommitteeDocCode = null;
        //        objEntity1.strEmpoweredCommittee = null;
        //    }
        //    /*--------------------------Pre-Production Document Section End-----------------------------------------------*/

        //    /*-------------------------Other Document End---------------------------------*/


        //    #endregion

           

        //    fillData();
        //    objEntity1.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);

        //    objEntity1.strPcNoAfter = Session["ProposalNo"].ToString();
        //    objEntity1.strIndustryCode = Session["UnitCode"].ToString();

        //    if (Request.QueryString["key"] != null)
        //    {
        //        objEntity1.strInctFlow = Request.QueryString["key"].ToString();

        //    }
        //    else
        //    {
        //        objEntity1.strInctFlow = Request.QueryString["IncentiveNo"].ToString();
        //    }

        //    /*---------------------------------------------------------------*/
        //    /////// Data Insert and Update

        //    string strReturnStatus = objBAL.Stamp_Duty_Exemption_Draft(objEntity1);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = './incentiveoffered.aspx';}); </script>", false);
        //    return;
        //    ModalPopupExtender1.Show();

        // ///*-----------------------------------------------------------------*///

        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "StampDutyExemptionIPR2022");
        //}
        //finally
        //{
        //    objBAL = null;
        //    objEntity1 = null;
        //}

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
            Util.LogError(ex, "StampDutyExemptionIPR2022");
        }
        finally
        {
            table = null;
        }
    }
}