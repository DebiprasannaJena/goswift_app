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

public partial class incentives_ExemptionLandforIndustrialUse : System.Web.UI.Page
{
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    DataTable dtSalutation;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["key"] != null)
        {
            string strInctId = Request.QueryString["key"].ToString();
        }
        if (!IsPostBack)
        {
            fillUnitCategory();
            fillSalutation();
            fillOrgType();
            fillUnitMeasurment();
            fillData();
            Txt_EnterPrise_Name.Enabled = false;
            Txt_Other_Unit_Before.Visible = false;
            Div_Land_Converter.Visible = false;
        }
    }

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
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
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
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
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
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
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
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
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
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
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
            Util.LogError(ex, "ExemptionLandforIndustrialUseIPR2022");
        }
    }

    private void UpdFileRemove(HiddenField Hid_File_Name, LinkButton LnkBtn_Upload_Doc, LinkButton LnkBtn_Delete_Doc, HyperLink Hyp_Document, Label Lbl_Upload_Msg, FileUpload FileUpload_Document, string strFolername)
    {
        try
        {
            string filename = Hid_File_Name.Value;
            string path = string.Format("~/incentives/Files/{0}/{1}", strFolername, filename);
            string completePath = Server.MapPath(path);
            
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
            if (string.Equals(lnk.ID, lnkUEinno.ID))
            {
                if (flEinno.HasFile)
                {

                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_EinnoLandforIndustrialUseIPR-2022";
                    UploadDocument(flEinno, hdnEinno_name, strFileName, hypEinno, lblEinno, lnkDEinno, "InctBasicDoc");
                }

            }
            else if (string.Equals(lnk.ID, lnkUPoweratt.ID))
            {
                if (flPoweratt.HasFile)
                {

                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PoweofAttorneyLandforIndustrialUseIPR-2022";
                    UploadDocument(flPoweratt, hdnPoweratt_name, strFileName, hypPoweratt, lblPoweratt, lnkDPoweratt, "InctBasicDoc");
                }

            }
            else if (string.Equals(lnk.ID, lnkUcertofreg.ID))
            {
                if (flcertofreg.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_MEMORANDUMLandforIndustrialUseIPR-2022";
                    UploadDocument(flcertofreg, certofreg_name, strFileName, hypVwcertofreg, lblcertofreg, lnkDcertofreg, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUfixcapital.ID))
            {
                if (flfixcapital.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_FixCapitalInvstLandforIndustrialUseIPR-2022";
                    UploadDocument(flfixcapital, hdnfixcapital_Name, strFileName, hypVwfixcapital, lblfixcapital, lnkDfixcapital, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUThrustcerti.ID))
            {
                if (flThrustcerti.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ThrustCertificateLandforIndustrialUseIPR-2022";
                    UploadDocument(flThrustcerti, hdnThrustcerti_name, strFileName, hypvwThrustcerti, lblThrustcerti, lnkDThrustcerti, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUAppprovedproj.ID))
            {
                if (flAppprovedproj.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ApproveddetailedLandforIndustrialUseIPR-2022";
                    UploadDocument(flAppprovedproj, hdnAppprovedproj_name, strFileName, hypvwAppprovedproj, lblAppprovedproj, lnkDAppprovedproj, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUAppraisal.ID))
            {
                if (flAppraisal.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_AppraisalLandforIndustrialUseIPR-2022";
                    UploadDocument(flAppraisal, hdnAppraisal_Name, strFileName, hypVwAppraisal, lblAppraisal, lnkDAppraisal, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkULanddocument.ID))
            {
                if (flLanddocument.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_LandDocumentLandforIndustrialUseIPR-2022";
                    UploadDocument(flLanddocument, hdnLanddocument_name, strFileName, hypvwLanddocument, lblLanddocument, lnkDLanddocument, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUstatutory.ID))
            {
                if (flstatutory.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_StatutoryLandforIndustrialUseIPR-2022";
                    UploadDocument(flstatutory, hdnstatutory_name, strFileName, hypvwstatutory, lblstatutory, lnkDstatutory, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUStampPaper.ID))
            {
                if (flStampPaper.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_StampPaperLandforIndustrialUseIPR-2022";
                    UploadDocument(flStampPaper, hdnStampPaper_name, strFileName, hypvwStampPaper, lblStampPaper, lnkDStampPaper, "InctBasicDoc");
                }
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
            else if (string.Equals(lnk.ID, lnkDThrustcerti.ID))
            {
                UpdFileRemove(hdnThrustcerti_name, lnkUThrustcerti, lnkDThrustcerti, hypvwThrustcerti, lblThrustcerti, flThrustcerti, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDAppprovedproj.ID))
            {
                UpdFileRemove(hdnAppprovedproj_name, lnkUAppprovedproj, lnkDAppprovedproj, hypvwAppprovedproj, lblAppprovedproj, flAppprovedproj, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDAppraisal.ID))
            {
                UpdFileRemove(hdnAppraisal_Name, lnkUAppraisal, lnkDAppraisal, hypVwAppraisal, lblAppraisal, flAppraisal, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDLanddocument.ID))
            {
                UpdFileRemove(hdnLanddocument_name, lnkULanddocument, lnkDLanddocument, hypvwLanddocument, lblLanddocument, flLanddocument, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDstatutory.ID))
            {
                UpdFileRemove(hdnstatutory_name, lnkUstatutory, lnkDstatutory, hypvwstatutory, lblstatutory, flstatutory, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDStampPaper.ID))
            {
                UpdFileRemove(hdnStampPaper_name, lnkUStampPaper, lnkDStampPaper, hypvwStampPaper, lblStampPaper, flStampPaper, "InctBasicDoc");
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
        finally
        {
            table = null;
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
        finally
        {
            table = null;
        }
    }

    protected void LnkBtn_Add_Item_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));
            table.Columns.Add("decValue", typeof(string));
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
        finally
        {
            table = null;
        }
    }

    protected void ImgBtn_Delete_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));
            table.Columns.Add("decValue", typeof(string));

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
        finally
        {
            table = null;
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
}