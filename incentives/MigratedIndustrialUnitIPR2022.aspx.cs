
// *******************************************************************************************************************
// File Name         : MigratedIndustrialUnitIPR2022.aspx
// Description       : Migrated Industrial Unit IPR-2022 Add and Draft Page
// Created by        : Debiprasanna Jena
// Created On        : 17th Nov 2023
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

public partial class incentives_MigratedIndustrialUnitIPR2022 : System.Web.UI.Page
{
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            FillUnitCategory();
            FillOrgType();
            FillSalutation();
            FillUnitMeasurment();
            Txt_Other_Unit_Before.Visible = false;
            Div_Incentive_Availed.Visible = false;
            Div_Clearance_pcb.Visible = false;
            if (Request.QueryString["InctUniqueNo"] != null)
            {
                 PostpopulateDataComm();
            }
            else
            {

                Txt_EIN_IL_Date.Attributes.Add("readonly", "readonly");
                Txt_Proposed_Date.Attributes.Add("readonly", "readonly");
                Txt_Commence_production.Attributes.Add("readonly", "readonly");
                Txt_TL_Sanction_Date.Attributes.Add("readonly", "readonly");
                Txt_TL_Availed_Date.Attributes.Add("readonly", "readonly");
                Txt_WC_Sanction_Date.Attributes.Add("readonly", "readonly");
                Txt_WC_Availed_Date.Attributes.Add("readonly", "readonly");

                FillUnitMeasurment();
                FillUnitCategory();
                Txt_Other_Unit_Before.Visible = false;
                Txt_EnterPrise_Name.Enabled = false;
                FillData();
                FillOrgType();
                FillSalutation();
            }
        }
    }

    public void PostpopulateDataComm()
    {
        try
        {
            DataSet dslivePre = IncentiveManager.MigratedIndustrialUnit_populateDatainDraft(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));
            DataTable dtindustryPre = dslivePre.Tables[0];////////////Industrial Unit's Details
            DataTable dtProductionPre = dslivePre.Tables[1];///////////production & employment
            DataTable dtProductionDetBefPre = dslivePre.Tables[2];///////////Term Loan Details
            DataTable dtProductionDetAftPre = dslivePre.Tables[3];///////////Working Capital Loan Details
            DataTable dtInvestmentPre = dslivePre.Tables[4];///////////Product Details
            DataTable dtMeansFinancePre = dslivePre.Tables[5];///////////Means of Finance
            DataTable dtMoFTermLoanPre = dslivePre.Tables[6];///////////investment details
            DataTable dtMoFWorkingLoanPre = dslivePre.Tables[7];///////////Other Document List


            #region IndustrailUnit
            if (dtindustryPre.Rows.Count > 0)
            {
                Txt_EnterPrise_Name.Text = dtindustryPre.Rows[0]["vchEnterpriseName"].ToString().Trim();
                DrpDwn_Unit_Cat.SelectedValue = (dtindustryPre.Rows[0]["intUnitCat"].ToString());
                Txt_Address_Unit.Text = dtindustryPre.Rows[0]["vchIndustryAddress"].ToString();
                Txt_Regd_Office_Address.Text = dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString();

                if ((dtindustryPre.Rows[0]["vchIndustryAddress"].ToString()) == (dtindustryPre.Rows[0]["vchRegisteredOfcAddress"].ToString()))
                {
                    ChkSameData.Checked = true;
                }
                else
                {
                    ChkSameData.Checked = false;
                }
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
            //*-------------------------------------------------------------------------------------------*/

                #region Production

                Grd_Production_Before.DataSource = dtInvestmentPre;
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

                #endregion
                Txt_Proposed_Date.Text = dtindustryPre.Rows[0]["dtmProdCommBefore"].ToString();
                Txt_Commence_production.Text = dtProductionPre.Rows[0]["dtmFFCIDateBefore"].ToString();
              
       /*--------------------------------------------------------------------------------------------------------------*/

                #region Investment
               
                Txt_Land_Details_before.Text = dtProductionPre.Rows[0]["decLandAmtBefore"].ToString();
                Txt_Building_Before.Text = dtProductionPre.Rows[0]["decBuildingAmtBefore"].ToString();
                Txt_Electrical_inst_Before.Text = dtProductionPre.Rows[0]["decPlantMachAmtBefore"].ToString();
                Txt_Plant_Mach_Before.Text = dtProductionPre.Rows[0]["decOtheFixedAssetAmtBefore"].ToString();
                Txt_Other_Fixed_Asset_Before.Text = dtProductionPre.Rows[0]["decLoadUnloadAmtBefore"].ToString();
                Txt_Loadig_Before.Text = dtProductionPre.Rows[0]["decMarginMoneyForworkingAmtBefore"].ToString();
                Txt_Margine_money.Text = dtProductionPre.Rows[0]["decElectricalInstAmtBefore"].ToString();
                Txt_Total_Capital_invst.Text = dtProductionPre.Rows[0]["decTotalAmtBefore"].ToString();

                Txt_Land_After.Text = dtProductionPre.Rows[0]["decLandAmtAfter"].ToString();
                Txt_Building_After.Text = dtProductionPre.Rows[0]["decBuildingAmtAfter"].ToString();
                Txt_Electrical_inst_After.Text = dtProductionPre.Rows[0]["decPlantMachAmtAfter"].ToString();
                Txt_Plant_Mach_After.Text = dtProductionPre.Rows[0]["decOtheFixedAssetAmtAfter"].ToString();
                Txt_Other_Fixed_Asset_After.Text = dtProductionPre.Rows[0]["decElectricalInstAmtAfter"].ToString();
                Txt_Loadig_After.Text = dtProductionPre.Rows[0]["decMarginMoneyForworkingAmtAfter"].ToString();
                Txt_Margine_money_After.Text = dtProductionPre.Rows[0]["decLoadUnloadAmtAfter"].ToString();
                Txt_Total_Capital_After.Text = dtProductionPre.Rows[0]["decTotalAmtAfter"].ToString();

                #endregion
           
                Txt_Equity_Amt.Text = dtMeansFinancePre.Rows[0]["decEquity"].ToString();

                if (dtProductionDetBefPre.Rows.Count > 0)
                {
                    Grd_TL.DataSource = dtProductionDetBefPre;
                    Grd_TL.DataBind();
                }

                if (dtProductionDetAftPre.Rows.Count > 0)
                {
                    Grd_WC.DataSource = dtProductionDetAftPre;
                    Grd_WC.DataBind();
                }
                Rad_Incentive_availed.SelectedValue = dtindustryPre.Rows[0]["intIPRinctiveAvel"].ToString();
                if (Rad_Incentive_availed.SelectedValue == "1")
                {
                    Grd_Incentive.DataSource = dtMoFTermLoanPre;
                    Grd_Incentive.DataBind();
                    Div_Incentive_Availed.Visible = true;
                }
                else
                {
                    Div_Incentive_Availed.Visible = false;
                }

                Txt_Present_status.Text= dtindustryPre.Rows[0]["vchPrsentStatus"].ToString();
                Rad_Project_needs.SelectedValue= dtindustryPre.Rows[0]["intProjectClearance"].ToString();
                if (Rad_Project_needs.SelectedValue == "1")
                {
                    Txt_Clearance_pcb.Text= dtindustryPre.Rows[0]["vchClearnceswm"].ToString();
                    Div_Clearance_pcb.Visible = true;
                }
                else
                {
                    Div_Clearance_pcb.Visible = false;
                }

             ///----------------------------------Other File Upload-----------------------------------------------///

                for (int i = 0; i < dtMoFWorkingLoanPre.Rows.Count; i++)
                {
                    string vchDocId = dtMoFWorkingLoanPre.Rows[i]["vchDocId"].ToString();
                    string vchFileName = dtMoFWorkingLoanPre.Rows[i]["vchFileName"].ToString();

                    if (vchDocId == "D334")
                    {
                        HdnPoweratt_Name.Value = vchFileName;
                        HypPoweratt.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypPoweratt.Visible = true;
                        LnkDPoweratt.Visible = true;
                        LblPoweratt.Visible = true;
                        FluPoweratt.Enabled = false;
                    }
                    else if (vchDocId == "D335")
                    {
                        HdnCertofreg_Name.Value = vchFileName;
                        HypVwCertofreg.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwCertofreg.Visible = true;
                        LnkDcertofreg.Visible = true;
                        LblCertofreg.Visible = true;
                        FluCertofreg.Enabled = false;
                    }
                    else if (vchDocId == "D336")
                    {
                        HdnEIN_Name.Value = vchFileName;
                        HypVwEIN.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwEIN.Visible = true;
                        LnkDEIN.Visible = true;
                        LblEIN.Visible = true;
                        FluEIN.Enabled = false;
                    }
                    else if (vchDocId == "D337")
                    {
                        HdnPlantmachinery_Name.Value = vchFileName;
                        HyVwPlantmachinery.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HyVwPlantmachinery.Visible = true;
                        LnkDPlantmachinery.Visible = true;
                        LblPlantmachinery.Visible = true;
                        FluPlantmachinery.Enabled = false;
                    }
                    else if (vchDocId == "D338")
                    {
                        HdnLoansancorFIapplied_Name.Value = vchFileName;
                        HypvwLoansancorFIapplied.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypvwLoansancorFIapplied.Visible = true;
                        LnkDloansancorFIapplied.Visible = true;
                        LblLoansancorFIapplied.Visible = true;
                        FluLoansancorFIapplied.Enabled = false;
                    }
                    else if (vchDocId == "D339")
                    {
                        HdnIncentiveAvail_Name.Value = vchFileName;
                        HypVwIncentiveAvail.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwIncentiveAvail.Visible = true;
                        LnkDIncentiveAvail.Visible = true;
                        LblIncentiveAvail.Visible = true;
                        FluIncentiveAvail.Enabled = false;
                    }
                    else if (vchDocId == "D340")
                    {
                        HdnUndertakingeffect_Name.Value = vchFileName;
                        HypVwUndertakingeffect.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwUndertakingeffect.Visible = true;
                        LnkDUndertakingeffect.Visible = true;
                        LblUndertakingeffect.Visible = true;
                        FluUndertakingeffect.Enabled = false;
                    }
                    else if (vchDocId == "D341")
                    {
                        HdnClearancefromPCB_Name.Value = vchFileName;
                        HypVwClearancefromPCB.NavigateUrl = "../incentives/Files/InctBasicDoc/" + vchFileName;
                        HypVwClearancefromPCB.Visible = true;
                        LnkDClearancefromPCB.Visible = true;
                        LblClearancefromPCB.Visible = true;
                        FluClearancefromPCB.Enabled = false;
                    }
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }        
    }

    protected void LnkBtn_TL_Add_More_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            table.Rows.Add(Txt_TL_Financial_Institution.Text, Txt_TL_State.Text, Txt_TL_City.Text, Txt_TL_Amount.Text, Txt_TL_Sanction_Date.Text, Txt_TL_Avail_Amount.Text, Txt_TL_Availed_Date.Text);
            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");
                table.Rows.Add(Lbl_TL_Financial_Inst.Text, Lbl_TL_State.Text, Lbl_TL_City.Text, Lbl_TL_Amount.Text, Lbl_TL_Sanction_Date.Text, Lbl_TL_Avail_Amt.Text, Lbl_TL_Avail_Date.Text);
            }

            Grd_TL.DataSource = table;
            Grd_TL.DataBind();

            Txt_TL_Financial_Institution.Text = "";
            Txt_TL_State.Text = "";
            Txt_TL_City.Text = "";
            Txt_TL_Amount.Text = "";
            Txt_TL_Sanction_Date.Text = "";
            Txt_TL_Avail_Amount.Text = "";
            Txt_TL_Availed_Date.Text = "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }       
    }

    protected void ImgBtn_Delete_TL_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                    Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                    Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                    Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                    Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                    Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                    Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");
                    table.Rows.Add(Lbl_TL_Financial_Inst.Text, Lbl_TL_State.Text, Lbl_TL_City.Text, Lbl_TL_Amount.Text, Lbl_TL_Sanction_Date.Text, Lbl_TL_Avail_Amt.Text, Lbl_TL_Avail_Date.Text);
                }
            }

            Grd_TL.DataSource = table;
            Grd_TL.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }      
    }

    protected void LnkBtn_WC_Add_More_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            table.Rows.Add(Txt_WC_Financial_Institution.Text, Txt_WC_State.Text, Txt_WC_City.Text, Txt_WC_Amount.Text, Txt_WC_Sanction_Date.Text, Txt_WC_Avail_Amount.Text, Txt_WC_Availed_Date.Text);
            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");
                table.Rows.Add(Lbl_WC_Financial_Inst.Text, Lbl_WC_State.Text, Lbl_WC_City.Text, Lbl_WC_Amount.Text, Lbl_WC_Sanction_Date.Text, Lbl_WC_Avail_Amt.Text, Lbl_WC_Avail_Date.Text);
            }

            Grd_WC.DataSource = table;
            Grd_WC.DataBind();

            Txt_WC_Financial_Institution.Text = "";
            Txt_WC_State.Text = "";
            Txt_WC_City.Text = "";
            Txt_WC_Amount.Text = "";
            Txt_WC_Sanction_Date.Text = "";
            Txt_WC_Avail_Amount.Text = "";
            Txt_WC_Availed_Date.Text = "";

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }      
    }

    protected void ImgBtn_Delete_WC_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchNameOfFinancialInst", typeof(string));
            table.Columns.Add("vchState", typeof(string));
            table.Columns.Add("vchCity", typeof(string));
            table.Columns.Add("decLoanAmt", typeof(string));
            table.Columns.Add("dtmSanctionDate", typeof(string));
            table.Columns.Add("decAvailedAmt", typeof(string));
            table.Columns.Add("dtmAvailedDate", typeof(string));

            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                    Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                    Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                    Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                    Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                    Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                    Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");
                    table.Rows.Add(Lbl_WC_Financial_Inst.Text, Lbl_WC_State.Text, Lbl_WC_City.Text, Lbl_WC_Amount.Text, Lbl_WC_Sanction_Date.Text, Lbl_WC_Avail_Amt.Text, Lbl_WC_Avail_Date.Text);
                }
            }

            Grd_WC.DataSource = table;
            Grd_WC.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }       
    }

    protected void LnkBtn_Add_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkUPoweratt.ID))
            {
                if (FluPoweratt.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "__PoweofAttorneyMigrateUndustrialunitIPR-2022";
                    UploadDocument(FluPoweratt, HdnPoweratt_Name, strFileName, HypPoweratt, LblPoweratt, LnkDPoweratt, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUcertofreg.ID))
            {
                if (FluCertofreg.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_MemorandumMigrateUndustrialunitIPR-2022";
                    UploadDocument(FluCertofreg, HdnCertofreg_Name, strFileName, HypVwCertofreg, LblCertofreg, LnkDcertofreg, "InctBasicDoc");
                }
            }

            else if (string.Equals(lnk.ID, LnkUEIN.ID))
            {
                if (FluEIN.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_EINMigrateUndustrialunitIPR-2022";
                    UploadDocument(FluEIN, HdnEIN_Name, strFileName, HypVwEIN, LblEIN, LnkDEIN, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUPlantmachinery.ID))
            {
                if (FluPlantmachinery.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_Plant&MachineryMigrateUndustrialunitIPR-2022";
                    UploadDocument(FluPlantmachinery, HdnPlantmachinery_Name, strFileName, HyVwPlantmachinery, LblPlantmachinery, LnkDPlantmachinery, "InctBasicDoc");
                }
            }

            else if (string.Equals(lnk.ID, LnkUloansancorFIapplied.ID))
            {
                if (FluLoansancorFIapplied.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_LoansancorMigrateUndustrialunitIPR-2022";
                    UploadDocument(FluLoansancorFIapplied, HdnLoansancorFIapplied_Name, strFileName, HypvwLoansancorFIapplied, LblLoansancorFIapplied, LnkDloansancorFIapplied, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUIncentiveAvail.ID))
            {
                if (FluIncentiveAvail.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_IncentiveAvailMigrateUndustrialunitIPR-2022";
                    UploadDocument(FluIncentiveAvail, HdnIncentiveAvail_Name, strFileName, HypVwIncentiveAvail, LblIncentiveAvail, LnkDIncentiveAvail, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUUndertakingeffect.ID))
            {
                if (FluUndertakingeffect.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_UndertakingeffectMigrateUndustrialunitIPR-2022";
                    UploadDocument(FluUndertakingeffect, HdnUndertakingeffect_Name, strFileName, HypVwUndertakingeffect, LblUndertakingeffect, LnkDUndertakingeffect, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkUClearancefromPCB.ID) && (FluClearancefromPCB.HasFile))
            {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ClearancefromPCBMigrateUndustrialunitIPR-2022";
                    UploadDocument(FluClearancefromPCB, HdnClearancefromPCB_Name, strFileName, HypVwClearancefromPCB, LblClearancefromPCB, LnkDClearancefromPCB, "InctBasicDoc");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }
    }

    protected void LnkBtn_Delete_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkDPoweratt.ID))
            {
                UpdFileRemove(HdnPoweratt_Name, LnkUPoweratt, LnkDPoweratt, HypPoweratt, LblPoweratt, FluPoweratt, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDcertofreg.ID))
            {
                UpdFileRemove(HdnCertofreg_Name, LnkUcertofreg, LnkDcertofreg, HypVwCertofreg, LblCertofreg, FluCertofreg, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDEIN.ID))
            {
                UpdFileRemove(HdnEIN_Name, LnkUEIN, LnkDEIN, HypVwEIN, LblEIN, FluEIN, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDPlantmachinery.ID))
            {
                UpdFileRemove(HdnPlantmachinery_Name, LnkUPlantmachinery, LnkDPlantmachinery, HyVwPlantmachinery, LblPlantmachinery, FluPlantmachinery, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDIncentiveAvail.ID))
            {
                UpdFileRemove(HdnIncentiveAvail_Name, LnkUIncentiveAvail, LnkDIncentiveAvail, HypVwIncentiveAvail, LblIncentiveAvail, FluIncentiveAvail, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDUndertakingeffect.ID))
            {
                UpdFileRemove(HdnUndertakingeffect_Name, LnkUUndertakingeffect, LnkDUndertakingeffect, HypVwUndertakingeffect, LblUndertakingeffect, FluUndertakingeffect, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDloansancorFIapplied.ID))
            {
                UpdFileRemove(HdnLoansancorFIapplied_Name, LnkUloansancorFIapplied, LnkDloansancorFIapplied, HypvwLoansancorFIapplied, LblLoansancorFIapplied, FluLoansancorFIapplied, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, LnkDClearancefromPCB.ID))
            {
                UpdFileRemove(HdnClearancefromPCB_Name, LnkUClearancefromPCB, LnkDClearancefromPCB, HypVwClearancefromPCB, LblClearancefromPCB, FluClearancefromPCB, "InctBasicDoc");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("incentiveoffered.aspx");
    }

    protected void LnkBtn_Add_Item_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchIncentive", typeof(string));
            table.Columns.Add("decValue", typeof(string));
            table.Columns.Add("decPeriod", typeof(string));
            table.Columns.Add("vchIPRApplica", typeof(string));

            table.Rows.Add(Txt_Incentive.Text, Txt_Quantum.Text, Txt_Perod.Text, Txt_Ipr_Applicability.Text);
            for (int i = 0; i < Grd_Incentive.Rows.Count; i++)
            {
                Label Lbl_Incentive = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Incentive");
                Label Lbl_Quantum = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Quantum");
                Label Lbl_Period = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Period");
                Label Lbl_IPR_Applicability = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_IPR_Applicability");
                table.Rows.Add(Lbl_Incentive.Text, Lbl_Quantum.Text, Lbl_Period.Text, Lbl_IPR_Applicability.Text);
            }

            Grd_Incentive.DataSource = table;
            Grd_Incentive.DataBind();

            Txt_Incentive.Text = "";
            Txt_Quantum.Text = "";
            Txt_Perod.Text = "";
            Txt_Ipr_Applicability.Text = "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }       
    }

    protected void ImgBtn_Delete_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchIncentive", typeof(string));
            table.Columns.Add("decValue", typeof(string));
            table.Columns.Add("decPeriod", typeof(string));
            table.Columns.Add("vchIPRApplica", typeof(string));


            for (int i = 0; i < Grd_Incentive.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_Incentive = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Incentive");
                    Label Lbl_Quantum = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Quantum");
                    Label Lbl_Period = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Period");
                    Label Lbl_IPR_Applicability = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_IPR_Applicability");
                    table.Rows.Add(Lbl_Incentive.Text, Lbl_Quantum.Text, Lbl_Period.Text, Lbl_IPR_Applicability.Text);
                }
            }

            Grd_Incentive.DataSource = table;
            Grd_Incentive.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }      
    }

    protected void Rad_Project_needs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Rad_Project_needs.SelectedValue == "1")
            {
                Div_Clearance_pcb.Visible = true;
                Txt_Clearance_pcb.Text = "";
            }
            else
            {
                Div_Clearance_pcb.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }
    }

    protected void Rad_Incentive_availed_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        if (Rad_Incentive_availed.SelectedValue == "1")
        {
            Div_Incentive_Availed.Visible = true;
            Txt_Incentive.Text = "";
            Txt_Quantum.Text = "";
            Txt_Perod.Text = "";
            Txt_Ipr_Applicability.Text = "";
            Grd_Incentive.DataSource = table;
            Grd_Incentive.DataBind();
            table.Clear();
        }
        else
        {
            Div_Incentive_Availed.Visible = false;
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


            #region Term Loan
            /*---------------------------------------------------------------------*/
            /// Add Term Loan
            /*---------------------------------------------------------------------*/

            List<BasicTermLoan> listTL = new List<BasicTermLoan>();

            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                BasicTermLoan objItem = new BasicTermLoan();

                Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");

                objItem.vchNameOfFinancialInst = Lbl_TL_Financial_Inst.Text;
                objItem.vchState = Lbl_TL_State.Text;
                objItem.vchCity = Lbl_TL_City.Text;
                objItem.decLoanAmt = Convert.ToDecimal(Lbl_TL_Amount.Text == "" ? "0" : Lbl_TL_Amount.Text);
                objItem.dtmSanctionDate = Lbl_TL_Sanction_Date.Text == "" ? null : Lbl_TL_Sanction_Date.Text;
                objItem.decAvailedAmt = Convert.ToDecimal(Lbl_TL_Avail_Amt.Text == "" ? "0" : Lbl_TL_Avail_Amt.Text);
                objItem.dtmAvailedDate = Lbl_TL_Avail_Date.Text == "" ? null : Lbl_TL_Avail_Date.Text;

                listTL.Add(objItem);
            }

            objEntity1.TermLoan = listTL;
            #endregion

            #region Working Capital Loan

            /*---------------------------------------------------------------------*/
            /// Add Working Capital Loan
            /*---------------------------------------------------------------------*/

            List<BasicWorkingCapitalLoan> listWC = new List<BasicWorkingCapitalLoan>();

            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                BasicWorkingCapitalLoan objItem = new BasicWorkingCapitalLoan();

                Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");

                objItem.vchNameOfFinancialInst = Lbl_WC_Financial_Inst.Text;
                objItem.vchState = Lbl_WC_State.Text;
                objItem.vchCity = Lbl_WC_City.Text;
                objItem.decLoanAmt = Convert.ToDecimal(Lbl_WC_Amount.Text == "" ? "0" : Lbl_WC_Amount.Text);
                objItem.dtmSanctionDate = Lbl_WC_Sanction_Date.Text == "" ? null : Lbl_WC_Sanction_Date.Text;
                objItem.decAvailedAmt = Convert.ToDecimal(Lbl_WC_Avail_Amt.Text == "" ? "0" : Lbl_WC_Avail_Amt.Text);
                objItem.dtmAvailedDate = Lbl_WC_Avail_Date.Text == "" ? null : Lbl_WC_Avail_Date.Text;

                listWC.Add(objItem);
            }

            objEntity1.WorkingCapitalLoan = listWC;
            #endregion


            #region Incentive Availed Details

            /*---------------------------------------------------------------------*/
            /// Add Incentive Availed Details
            /*---------------------------------------------------------------------*/

            List<IncentiveAvailed> listInctAvail = new List<IncentiveAvailed>();

            for (int i = 0; i < Grd_Incentive.Rows.Count; i++)
            {
                IncentiveAvailed objInctAvailDetl = new IncentiveAvailed();

                Label LblIncentive = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Incentive");
                Label LblQuantum = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Quantum");
                Label LblPeriod = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Period");
                Label LblIPR_Applicability = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_IPR_Applicability");

                objInctAvailDetl.vchIncentive = LblIncentive.Text;
                objInctAvailDetl.decValue = Convert.ToDecimal(LblQuantum.Text == "" ? "0" : LblQuantum.Text);
                objInctAvailDetl.decPeriod = Convert.ToDecimal(LblPeriod.Text == "" ? "0" : LblPeriod.Text);
                objInctAvailDetl.vchIPRApplica = LblIPR_Applicability.Text;

                listInctAvail.Add(objInctAvailDetl);
            }
            objEntity1.Incentive_Availeds = listInctAvail;

            #endregion

            #region IndustrialUnitDetailsSection

            /*---------------------------------------------------------------------*/
            /// Industrial Unit Details Section
            /*---------------------------------------------------------------------*/

            objEntity1.strEnterpriseName = Txt_EnterPrise_Name.Text;
            objEntity1.intOrganisationType = Convert.ToInt32(DrpDwn_Org_Type.SelectedValue);
            objEntity1.strIndustryAddress = Txt_Address_Unit.Text;
            objEntity1.intUnitCat = Convert.ToInt32(DrpDwn_Unit_Cat.SelectedValue);

            objEntity1.strRegisteredOfcAddress = Txt_Regd_Office_Address.Text;
            objEntity1.strManagingPartnerGender = DrpDwn_Gender_Partner.SelectedValue;
            objEntity1.strManagingPartnerName = Txt_Partner_Name.Text;

            objEntity1.strEINNO = Txt_EIN_IL_NO.Text == "" ? null : Txt_EIN_IL_NO.Text;
            objEntity1.dtmEIN = Txt_EIN_IL_Date.Text == "" ? null : Txt_EIN_IL_Date.Text;


            /*-------------------------Industrial Unit Details End---------------------------------*/
            #endregion

            #region ProductionandEmploymentDetails Section

            /*----------------------------------------------------------*/
            /// Production and Employment Details Section
            /*----------------------------------------------------------*/
            objEntity1.dtmFFCIDateBefore = Txt_Proposed_Date.Text;
            objEntity1.dtmProdCommBefore = Txt_Commence_production.Text;

            #endregion

            #region InvestmentDetailsSection

            /*----------------------------------------------------------*/
            /// Investment Details Section
            /*----------------------------------------------------------*/

            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text);
            decimal decLandBefore = 0;
            decimal decBuildingBefore = 0;
            decimal decPlantMachBefore = 0;
            decimal decOtherFixedAssetBefore = 0;
            decimal decElectricalInstBefore = 0;
            decimal decLoadUnloadBefore = 0;
            decimal decMarginMoneyForworkingBefore = 0;

            if (Txt_Land_Details_before.Text != "")
            {
                decLandBefore = Convert.ToDecimal(Txt_Land_Details_before.Text);
            }
            if (Txt_Building_Before.Text != "")
            {
                decBuildingBefore = Convert.ToDecimal(Txt_Building_Before.Text);
            }
            if (Txt_Plant_Mach_Before.Text != "")
            {
                decPlantMachBefore = Convert.ToDecimal(Txt_Plant_Mach_Before.Text);
            }
            if (Txt_Other_Fixed_Asset_Before.Text != "")
            {
                decOtherFixedAssetBefore = Convert.ToDecimal(Txt_Other_Fixed_Asset_Before.Text);
            }
            if (Txt_Electrical_inst_Before.Text != "")
            {
                decElectricalInstBefore = Convert.ToDecimal(Txt_Electrical_inst_Before.Text);
            }
            if (Txt_Loadig_Before.Text != "")
            {
                decLoadUnloadBefore = Convert.ToDecimal(Txt_Loadig_Before.Text);
            }
            if (Txt_Margine_money.Text != "")
            {
                decMarginMoneyForworkingBefore = Convert.ToDecimal(Txt_Margine_money.Text);
            }
            decimal decTotalCapitalBefore = decLandBefore + decBuildingBefore + decPlantMachBefore + decOtherFixedAssetBefore + decElectricalInstBefore + decLoadUnloadBefore + decMarginMoneyForworkingBefore;

            objEntity1.decLandAmtBefore = decLandBefore;
            objEntity1.decBuildingAmtBefore = decBuildingBefore;
            objEntity1.decPlantMachAmtBefore = decPlantMachBefore;
            objEntity1.decOtheFixedAssetAmtBefore = decOtherFixedAssetBefore;
            objEntity1.decTotalAmtBefore = decTotalCapitalBefore;
            objEntity1.decElectricalInstAmtBefore = decElectricalInstBefore;
            objEntity1.decLoadUnloadAmtBefore = decLoadUnloadBefore;
            objEntity1.decMarginMoneyForworkingAmtBefore = decMarginMoneyForworkingBefore;
            /*---------------------------------------------------------------------*/
            /// Investment Details (After)
            /*---------------------------------------------------------------------*/

            decimal decLandAfter = 0;
            decimal decBuildingAfter = 0;
            decimal decPlantMachAfter = 0;
            decimal decOtherFixedAssetAfter = 0;
            decimal decElectricalInstAfter = 0;
            decimal decLoadUnloadAfter = 0;
            decimal decMarginMoneyForworkingAfter = 0;

            if (Txt_Land_After.Text != "")
            {
                decLandAfter = Convert.ToDecimal(Txt_Land_After.Text);
            }
            if (Txt_Building_After.Text != "")
            {
                decBuildingAfter = Convert.ToDecimal(Txt_Building_After.Text);
            }
            if (Txt_Plant_Mach_After.Text != "")
            {
                decPlantMachAfter = Convert.ToDecimal(Txt_Plant_Mach_After.Text);
            }
            if (Txt_Other_Fixed_Asset_After.Text != "")
            {
                decOtherFixedAssetAfter = Convert.ToDecimal(Txt_Other_Fixed_Asset_After.Text);
            }
            if (Txt_Electrical_inst_Before.Text != "")
            {
                decElectricalInstAfter = Convert.ToDecimal(Txt_Electrical_inst_Before.Text);
            }
            if (Txt_Loadig_After.Text != "")
            {
                decLoadUnloadAfter = Convert.ToDecimal(Txt_Loadig_After.Text);
            }
            if (Txt_Margine_money_After.Text != "")
            {
                decMarginMoneyForworkingAfter = Convert.ToDecimal(Txt_Margine_money_After.Text);
            }
            decimal decTotalCapitalAfter = decLandAfter + decBuildingAfter + decPlantMachAfter + decOtherFixedAssetAfter + decElectricalInstAfter + decLoadUnloadAfter + decMarginMoneyForworkingAfter;

            objEntity1.decLandAmtAfter = decLandAfter;
            objEntity1.decBuildingAmtAfter = decBuildingAfter;
            objEntity1.decPlantMachAmtAfter = decPlantMachAfter;
            objEntity1.decOtheFixedAssetAmtAfter = decOtherFixedAssetAfter;
            objEntity1.decTotalAmtAfter = decTotalCapitalAfter;
            objEntity1.decElectricalInstAmtAfter = decElectricalInstAfter;
            objEntity1.decLoadUnloadAmtAfter = decLoadUnloadAfter;
            objEntity1.decMarginMoneyForworkingAmtAfter = decMarginMoneyForworkingAfter;

            /*----------------------------Investment Details Section End------------------------------*/
            #endregion

            #region MeansofFinanceSection

            /*----------------------------------------------------------*/
            /// Means of Finance Section
            /*----------------------------------------------------------*/
            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text == "" ? "0" : Txt_Equity_Amt.Text);
            objEntity1.strPrsentStatus = Txt_Present_status.Text;
            objEntity1.intIPRinctiveAvel = Convert.ToInt32(Rad_Incentive_availed.SelectedValue);

            if (Rad_Project_needs.SelectedValue == "1")
            {
                objEntity1.intProjectClearance = Convert.ToInt32(Rad_Project_needs.SelectedValue);
                objEntity1.strClearnceswm = Txt_Clearance_pcb.Text;
            }
            else
            {
                objEntity1.intProjectClearance = Convert.ToInt32(Rad_Project_needs.SelectedValue);
            }


            #endregion

            #region OtherDocumentSection

            /*---------------------------------------------------------------------*/
            ///Other Document Section
            /*---------------------------------------------------------------------*/
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

            if (HdnEIN_Name.Value != "")
            {
                objEntity1.strEINapprovalDocCode = HdnEIN_Code.Value;
                objEntity1.strEINapproval = HdnEIN_Name.Value;
            }
            else
            {
                objEntity1.strEINapprovalDocCode = null;
                objEntity1.strEINapproval = null;
            }

            if (HdnPlantmachinery_Name.Value != "")
            {
                objEntity1.strCapitalInvstDocCode = HdnPlantmachinery_Code.Value;
                objEntity1.strCapitalInvst = HdnPlantmachinery_Name.Value;
            }
            else
            {
                objEntity1.strCapitalInvstDocCode = null;
                objEntity1.strCapitalInvst = null;
            }

            if (HdnLoansancorFIapplied_Name.Value != "")
            {
                objEntity1.strloansancorFIappliedDocCode = HdnLoansancorFIapplied_Code.Value;
                objEntity1.strloansancorFIapplied = HdnLoansancorFIapplied_Name.Value;
            }
            else
            {
                objEntity1.strloansancorFIappliedDocCode = null;
                objEntity1.strloansancorFIapplied = null;
            }

            if (HdnIncentiveAvail_Name.Value != "")
            {
                objEntity1.strIncentiveAvailDocCode = HdnIncentiveAvail_Code.Value;
                objEntity1.strIncentiveAvail = HdnIncentiveAvail_Name.Value;
            }
            else
            {
                objEntity1.strIncentiveAvailDocCode = null;
                objEntity1.strIncentiveAvail = null;
            }

            if (HdnUndertakingeffect_Name.Value != "")
            {
                objEntity1.strUndertakingeffectDocCode = HdnUndertakingeffect_Code.Value;
                objEntity1.strUndertakingeffect = HdnUndertakingeffect_Name.Value;
            }
            else
            {
                objEntity1.strUndertakingeffectDocCode = null;
                objEntity1.strUndertakingeffect = null;
            }

            if (HdnClearancefromPCB_Name.Value != "")
            {
                objEntity1.strclearancefromPCBDocCode = HdnClearancefromPCB_Code.Value;
                objEntity1.strclearancefromPCB = HdnClearancefromPCB_Name.Value;
            }
            else
            {
                objEntity1.strclearancefromPCBDocCode = null;
                objEntity1.strclearancefromPCB = null;
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
            string strReturnStatus = objBAL.Migrated_Industrial_Unit_AED(objEntity1);
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
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
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


            #region Term Loan
            /*---------------------------------------------------------------------*/
            /// Add Term Loan
            /*---------------------------------------------------------------------*/

            List<BasicTermLoan> listTL = new List<BasicTermLoan>();

            for (int i = 0; i < Grd_TL.Rows.Count; i++)
            {
                BasicTermLoan objItem = new BasicTermLoan();

                Label Lbl_TL_Financial_Inst = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Financial_Inst");
                Label Lbl_TL_State = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_State");
                Label Lbl_TL_City = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_City");
                Label Lbl_TL_Amount = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Amount");
                Label Lbl_TL_Sanction_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Sanction_Date");
                Label Lbl_TL_Avail_Amt = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Amt");
                Label Lbl_TL_Avail_Date = (Label)Grd_TL.Rows[i].FindControl("Lbl_TL_Avail_Date");

                objItem.vchNameOfFinancialInst = Lbl_TL_Financial_Inst.Text;
                objItem.vchState = Lbl_TL_State.Text;
                objItem.vchCity = Lbl_TL_City.Text;
                objItem.decLoanAmt = Convert.ToDecimal(Lbl_TL_Amount.Text == "" ? "0" : Lbl_TL_Amount.Text);
                objItem.dtmSanctionDate = Lbl_TL_Sanction_Date.Text == "" ? null : Lbl_TL_Sanction_Date.Text;
                objItem.decAvailedAmt = Convert.ToDecimal(Lbl_TL_Avail_Amt.Text == "" ? "0" : Lbl_TL_Avail_Amt.Text);
                objItem.dtmAvailedDate = Lbl_TL_Avail_Date.Text == "" ? null : Lbl_TL_Avail_Date.Text;

                listTL.Add(objItem);
            }

            objEntity1.TermLoan = listTL;
            #endregion

            #region Working Capital Loan

            /*---------------------------------------------------------------------*/
            /// Add Working Capital Loan
           /*---------------------------------------------------------------------*/

           
            List<BasicWorkingCapitalLoan> listWC = new List<BasicWorkingCapitalLoan>();

            for (int i = 0; i < Grd_WC.Rows.Count; i++)
            {
                BasicWorkingCapitalLoan objItem = new BasicWorkingCapitalLoan();

                Label Lbl_WC_Financial_Inst = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Financial_Inst");
                Label Lbl_WC_State = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_State");
                Label Lbl_WC_City = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_City");
                Label Lbl_WC_Amount = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Amount");
                Label Lbl_WC_Sanction_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Sanction_Date");
                Label Lbl_WC_Avail_Amt = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Amt");
                Label Lbl_WC_Avail_Date = (Label)Grd_WC.Rows[i].FindControl("Lbl_WC_Avail_Date");

                objItem.vchNameOfFinancialInst = Lbl_WC_Financial_Inst.Text;
                objItem.vchState = Lbl_WC_State.Text;
                objItem.vchCity = Lbl_WC_City.Text;
                objItem.decLoanAmt = Convert.ToDecimal(Lbl_WC_Amount.Text == "" ? "0" : Lbl_WC_Amount.Text);
                objItem.dtmSanctionDate = Lbl_WC_Sanction_Date.Text == "" ? null : Lbl_WC_Sanction_Date.Text;
                objItem.decAvailedAmt = Convert.ToDecimal(Lbl_WC_Avail_Amt.Text == "" ? "0" : Lbl_WC_Avail_Amt.Text);
                objItem.dtmAvailedDate = Lbl_WC_Avail_Date.Text == "" ? null : Lbl_WC_Avail_Date.Text;

                listWC.Add(objItem);
            }

            objEntity1.WorkingCapitalLoan = listWC;
            #endregion


            #region Incentive Availed Details

            /*---------------------------------------------------------------------*/
            /// Add Incentive Availed Details
            /*---------------------------------------------------------------------*/

            List<IncentiveAvailed> listInctAvail = new List<IncentiveAvailed>();

            for (int i = 0; i < Grd_Incentive.Rows.Count; i++)
            {
                IncentiveAvailed objInctAvailDetl = new IncentiveAvailed();

                Label LblIncentive = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Incentive");
                Label LblQuantum = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Quantum");
                Label LblPeriod = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_Period");
                Label LblIPR_Applicability = (Label)Grd_Incentive.Rows[i].FindControl("Lbl_IPR_Applicability");

                objInctAvailDetl.vchIncentive = LblIncentive.Text;
                objInctAvailDetl.decValue = Convert.ToDecimal(LblQuantum.Text == "" ? "0" : LblQuantum.Text);
                objInctAvailDetl.decPeriod = Convert.ToDecimal(LblPeriod.Text == "" ? "0" : LblPeriod.Text);
                objInctAvailDetl.vchIPRApplica = LblIPR_Applicability.Text;

                listInctAvail.Add(objInctAvailDetl);
            }
            objEntity1.Incentive_Availeds = listInctAvail;

            #endregion

            #region IndustrialUnitDetailsSection

            /*---------------------------------------------------------------------*/
            /// Industrial Unit Details Section
            /*---------------------------------------------------------------------*/

            objEntity1.strEnterpriseName = Txt_EnterPrise_Name.Text;
            objEntity1.intOrganisationType = Convert.ToInt32(DrpDwn_Org_Type.SelectedValue);
            objEntity1.strIndustryAddress = Txt_Address_Unit.Text;
            objEntity1.intUnitCat = Convert.ToInt32(DrpDwn_Unit_Cat.SelectedValue);
            objEntity1.strRegisteredOfcAddress = Txt_Regd_Office_Address.Text;
            objEntity1.strManagingPartnerGender = DrpDwn_Gender_Partner.SelectedValue;
            objEntity1.strManagingPartnerName = Txt_Partner_Name.Text;
            objEntity1.strEINNO = Txt_EIN_IL_NO.Text == "" ? null : Txt_EIN_IL_NO.Text;
            objEntity1.dtmEIN = Txt_EIN_IL_Date.Text == "" ? null : Txt_EIN_IL_Date.Text;

            /*-------------------------Industrial Unit Details End---------------------------------*/
            #endregion

            #region ProductionandEmploymentDetails Section

            /*----------------------------------------------------------*/
            /// Production and Employment Details Section
            /*----------------------------------------------------------*/
            objEntity1.dtmFFCIDateBefore = Txt_Proposed_Date.Text;
            objEntity1.dtmProdCommBefore = Txt_Commence_production.Text;


            #endregion

            #region InvestmentDetailsSection

            /*----------------------------------------------------------*/
            /// Investment Details Section
            /*----------------------------------------------------------*/

            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text);
            decimal decLandBefore = 0;
            decimal decBuildingBefore = 0;
            decimal decPlantMachBefore = 0;
            decimal decOtherFixedAssetBefore = 0;
            decimal decElectricalInstBefore = 0;
            decimal decLoadUnloadBefore = 0;
            decimal decMarginMoneyForworkingBefore = 0;

            if (Txt_Land_Details_before.Text != "")
            {
                decLandBefore = Convert.ToDecimal(Txt_Land_Details_before.Text);
            }
            if (Txt_Building_Before.Text != "")
            {
                decBuildingBefore = Convert.ToDecimal(Txt_Building_Before.Text);
            }
            if (Txt_Plant_Mach_Before.Text != "")
            {
                decPlantMachBefore = Convert.ToDecimal(Txt_Plant_Mach_Before.Text);
            }
            if (Txt_Other_Fixed_Asset_Before.Text != "")
            {
                decOtherFixedAssetBefore = Convert.ToDecimal(Txt_Other_Fixed_Asset_Before.Text);
            }
            if (Txt_Electrical_inst_Before.Text != "")
            {
                decElectricalInstBefore = Convert.ToDecimal(Txt_Electrical_inst_Before.Text);
            }
            if (Txt_Loadig_Before.Text != "")
            {
                decLoadUnloadBefore = Convert.ToDecimal(Txt_Loadig_Before.Text);
            }
            if (Txt_Margine_money.Text != "")
            {
                decMarginMoneyForworkingBefore = Convert.ToDecimal(Txt_Margine_money.Text);
            }
            decimal decTotalCapitalBefore = decLandBefore + decBuildingBefore + decPlantMachBefore + decOtherFixedAssetBefore + decElectricalInstBefore + decLoadUnloadBefore + decMarginMoneyForworkingBefore;

            objEntity1.decLandAmtBefore = decLandBefore;
            objEntity1.decBuildingAmtBefore = decBuildingBefore;
            objEntity1.decPlantMachAmtBefore = decPlantMachBefore;
            objEntity1.decOtheFixedAssetAmtBefore = decOtherFixedAssetBefore;
            objEntity1.decTotalAmtBefore = decTotalCapitalBefore;
            objEntity1.decElectricalInstAmtBefore = decElectricalInstBefore;
            objEntity1.decLoadUnloadAmtBefore = decLoadUnloadBefore;
            objEntity1.decMarginMoneyForworkingAmtBefore = decMarginMoneyForworkingBefore;
            /*---------------------------------------------------------------------*/
            /// Investment Details (After)
            /*---------------------------------------------------------------------*/

            decimal decLandAfter = 0;
            decimal decBuildingAfter = 0;
            decimal decPlantMachAfter = 0;
            decimal decOtherFixedAssetAfter = 0;
            decimal decElectricalInstAfter = 0;
            decimal decLoadUnloadAfter = 0;
            decimal decMarginMoneyForworkingAfter = 0;

            if (Txt_Land_After.Text != "")
            {
                decLandAfter = Convert.ToDecimal(Txt_Land_After.Text);
            }
            if (Txt_Building_After.Text != "")
            {
                decBuildingAfter = Convert.ToDecimal(Txt_Building_After.Text);
            }
            if (Txt_Plant_Mach_After.Text != "")
            {
                decPlantMachAfter = Convert.ToDecimal(Txt_Plant_Mach_After.Text);
            }
            if (Txt_Other_Fixed_Asset_After.Text != "")
            {
                decOtherFixedAssetAfter = Convert.ToDecimal(Txt_Other_Fixed_Asset_After.Text);
            }
            if (Txt_Electrical_inst_Before.Text != "")
            {
                decElectricalInstAfter = Convert.ToDecimal(Txt_Electrical_inst_After.Text);
            }
            if (Txt_Loadig_After.Text != "")
            {
                decLoadUnloadAfter = Convert.ToDecimal(Txt_Loadig_After.Text);
            }
            if (Txt_Margine_money_After.Text != "")
            {
                decMarginMoneyForworkingAfter = Convert.ToDecimal(Txt_Margine_money_After.Text);
            }
            decimal decTotalCapitalAfter = decLandAfter + decBuildingAfter + decPlantMachAfter + decOtherFixedAssetAfter + decElectricalInstAfter + decLoadUnloadAfter + decMarginMoneyForworkingAfter;

            objEntity1.decLandAmtAfter = decLandAfter;
            objEntity1.decBuildingAmtAfter = decBuildingAfter;
            objEntity1.decPlantMachAmtAfter = decPlantMachAfter;
            objEntity1.decOtheFixedAssetAmtAfter = decOtherFixedAssetAfter;
            objEntity1.decTotalAmtAfter = decTotalCapitalAfter;
            objEntity1.decElectricalInstAmtAfter = decElectricalInstAfter;
            objEntity1.decLoadUnloadAmtAfter = decLoadUnloadAfter;
            objEntity1.decMarginMoneyForworkingAmtAfter = decMarginMoneyForworkingAfter;

            /*----------------------------Investment Details Section End------------------------------*/
            #endregion

            #region MeansofFinanceSection

            /*----------------------------------------------------------*/
            /// Means of Finance Section
           /*----------------------------------------------------------*/

            objEntity1.decEquity = Convert.ToDecimal(Txt_Equity_Amt.Text == "" ? "0" : Txt_Equity_Amt.Text);
            objEntity1.strPrsentStatus = Txt_Present_status.Text;
            objEntity1.intIPRinctiveAvel = Convert.ToInt32(Rad_Incentive_availed.SelectedValue);          
            
            if ( Rad_Project_needs.SelectedValue == "1")
            {
                objEntity1.intProjectClearance = Convert.ToInt32(Rad_Project_needs.SelectedValue);
                objEntity1.strClearnceswm = Txt_Clearance_pcb.Text;
            }
            else
            {
                objEntity1.intProjectClearance = Convert.ToInt32(Rad_Project_needs.SelectedValue);
            }

            #endregion

            #region OtherDocumentSection

            /*---------------------------------------------------------------------*/
            /// Other Document Section
            /*----------------------------------------------------------*/

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

            if (HdnEIN_Name.Value != "")
            {
                objEntity1.strEINapprovalDocCode = HdnEIN_Code.Value;
                objEntity1.strEINapproval = HdnEIN_Name.Value;
            }
            else
            {
                objEntity1.strEINapprovalDocCode = null;
                objEntity1.strEINapproval = null;
            }

            if (HdnPlantmachinery_Name.Value != "")
            {
                objEntity1.strCapitalInvstDocCode = HdnPlantmachinery_Code.Value;
                objEntity1.strCapitalInvst = HdnPlantmachinery_Name.Value;
            }
            else
            {
                objEntity1.strCapitalInvstDocCode = null;
                objEntity1.strCapitalInvst = null;
            }

            if (HdnLoansancorFIapplied_Name.Value != "")
            {
                objEntity1.strloansancorFIappliedDocCode = HdnLoansancorFIapplied_Code.Value;
                objEntity1.strloansancorFIapplied = HdnLoansancorFIapplied_Name.Value;
            }
            else
            {
                objEntity1.strloansancorFIappliedDocCode = null;
                objEntity1.strloansancorFIapplied = null;
            }

            if (HdnIncentiveAvail_Name.Value != "")
            {
                objEntity1.strIncentiveAvailDocCode = HdnIncentiveAvail_Code.Value;
                objEntity1.strIncentiveAvail = HdnIncentiveAvail_Name.Value;
            }
            else
            {
                objEntity1.strIncentiveAvailDocCode = null;
                objEntity1.strIncentiveAvail = null;
            }

            if (HdnUndertakingeffect_Name.Value != "")
            {
                objEntity1.strUndertakingeffectDocCode = HdnUndertakingeffect_Code.Value;
                objEntity1.strUndertakingeffect = HdnUndertakingeffect_Name.Value;
            }
            else
            {
                objEntity1.strUndertakingeffectDocCode = null;
                objEntity1.strUndertakingeffect = null;
            }

            if (HdnClearancefromPCB_Name.Value != "")
            {
                objEntity1.strclearancefromPCBDocCode = HdnClearancefromPCB_Code.Value;
                objEntity1.strclearancefromPCB = HdnClearancefromPCB_Name.Value;
            }
            else
            {
                objEntity1.strclearancefromPCBDocCode = null;
                objEntity1.strclearancefromPCB = null;
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

            string strReturnStatus = objBAL.Migrated_Industrial_Unit_Draft(objEntity1);
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
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MigratedIndustrialUntitIPR2022");
        }       
    }
}
