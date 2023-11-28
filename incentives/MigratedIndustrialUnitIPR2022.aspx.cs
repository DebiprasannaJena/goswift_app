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
            fillOrgType();
            fillSalutation();
            fillUnitMeasurment();
            Txt_Other_Unit_Before.Visible = false;
            Div_Incentive_Availed.Visible = false;
            if (Request.QueryString["InctUniqueNo"] != null)
            {
                // PostpopulateDataComm(Convert.ToInt16(Request.QueryString["InctUniqueNo"]));

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
                Div_Clearance_pcb.Visible = false;
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


    private void fillUnitMeasurment()
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            table = null;
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            table = null;
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            table = null;
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
                table.Rows.Add(Lbl_WC_Financial_Inst.Text, Lbl_WC_State.Text, Lbl_WC_City.Text, Lbl_WC_Amount.Text, Lbl_WC_Sanction_Date.Text, Lbl_WC_Avail_Amt.Text, Txt_WC_Availed_Date.Text);
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
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            table = null;
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
                    table.Rows.Add(Lbl_WC_Financial_Inst.Text, Lbl_WC_State.Text, Lbl_WC_City.Text, Lbl_WC_Amount.Text, Lbl_WC_Sanction_Date.Text, Lbl_WC_Avail_Amt.Text, Txt_WC_Availed_Date.Text);
                }
            }

            Grd_WC.DataSource = table;
            Grd_WC.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }
        finally
        {
            table = null;
        }
    }


    protected void LnkBtn_Add_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkUPowerattpre.ID))
            {
                if (flPowerattpre.HasFile)
                {

                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PoweofAttorneyPre";
                    UploadDocument(flPowerattpre, hdnPowerattpre_name, strFileName, hypPowerattpre, lblPowerattpre, lnkDPowerattpre, "InctBasicDoc");
                }

            }
            else if (string.Equals(lnk.ID, lnkUcertofreg.ID))
            {
                if (flcertofreg.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_MEMORANDUMPRE";
                    UploadDocument(flcertofreg, certofreg_name, strFileName, hypVwcertofreg, lblcertofreg, lnkDcertofreg, "InctBasicDoc");
                }
            }

            else if (string.Equals(lnk.ID, lnkUEIN.ID))
            {
                if (flEIN.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_EINPRE";
                    UploadDocument(flEIN, hdnEIN_Name, strFileName, hypVwEIN, lblEIN, lnkDEIN, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUPlantmachinery.ID))
            {
                if (flPlantmachinery.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_Plant&MachineryPre";
                    UploadDocument(flPlantmachinery, hdnPlantmachinery_Name, strFileName, hyVwPlantmachinery, lblPlantmachinery, lnkDPlantmachinery, "InctBasicDoc");
                }
            }

            else if (string.Equals(lnk.ID, lnkUInvplantmachinary.ID))
            {
                if (flInvplantmachinary.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_IvtPlant&machineryPre";
                    UploadDocument(flInvplantmachinary, hdnInvplantmachinary_name, strFileName, hypVwInvplantmachinary, lblInvplantmachinary, lnkDInvplantmachinary, "InctBasicDoc");
                }
            }
            else if (string.Equals(lnk.ID, lnkUproposedprod.ID))
            {
                if (flproposedprod.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_ProposedprodPre";
                    UploadDocument(flproposedprod, hdnproposedprod_name, strFileName, hypVwproposedprod, lblproposedprod, lnkDproposedprod, "InctBasicDoc");
                }
            }

            else if (string.Equals(lnk.ID, lnkUloansancorFIappliedpost.ID))
            {
                if (flloansancorFIappliedpost.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "LoansancorFIappliedpost";
                    UploadDocument(flloansancorFIappliedpost, hdnloansancorFIappliedpost_name, strFileName, hypvwloansancorFIappliedpost, lblloansancorFIappliedpost, lnkDloansancorFIappliedpost, "InctBasicDoc");
                }
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
        }

    }

    protected void LnkBtn_Delete_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, lnkDPowerattpre.ID))
            {
                UpdFileRemove(hdnPowerattpre_name, lnkUPowerattpre, lnkDPowerattpre, hypPowerattpre, lblPowerattpre, flPowerattpre, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDcertofreg.ID))
            {
                UpdFileRemove(certofreg_name, lnkUcertofreg, lnkDcertofreg, hypVwcertofreg, lblcertofreg, flcertofreg, "InctBasicDoc");
            }

            else if (string.Equals(lnk.ID, lnkDEIN.ID))
            {
                UpdFileRemove(hdnEIN_Name, lnkUEIN, lnkDEIN, hypVwEIN, lblEIN, flEIN, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDPlantmachinery.ID))
            {
                UpdFileRemove(hdnPlantmachinery_Name, lnkUPlantmachinery, lnkDPlantmachinery, hyVwPlantmachinery, lblPlantmachinery, flPlantmachinery, "InctBasicDoc");
            }

            else if (string.Equals(lnk.ID, lnkDInvplantmachinary.ID))
            {
                UpdFileRemove(hdnInvplantmachinary_name, lnkUInvplantmachinary, lnkDInvplantmachinary, hypVwInvplantmachinary, lblInvplantmachinary, flInvplantmachinary, "InctBasicDoc");
            }
            else if (string.Equals(lnk.ID, lnkDproposedprod.ID))
            {
                UpdFileRemove(hdnproposedprod_name, lnkUproposedprod, lnkDproposedprod, hypVwproposedprod, lblproposedprod, flproposedprod, "InctBasicDoc");
            }

            else if (string.Equals(lnk.ID, lnkDloansancorFIappliedpost.ID))
            {
                UpdFileRemove(hdnloansancorFIappliedpost_name, lnkUloansancorFIappliedpost, lnkDloansancorFIappliedpost, hypvwloansancorFIappliedpost, lblloansancorFIappliedpost, flloansancorFIappliedpost, "InctBasicDoc");
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThrustorPriorityIPR2022");
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
            table.Columns.Add("vchProductName", typeof(string));
            table.Columns.Add("intQuantity", typeof(string));
            table.Columns.Add("vchUnit", typeof(string));
            table.Columns.Add("vchOtherUnit", typeof(string));

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
        finally
        {
            table = null;
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
}
