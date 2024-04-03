#region "Comment"
////<%--'******************************************************************************
////' File Name             :   AddIncentivePolicy.aspx
////' Description           :   Add Incentive Policy
////' Created by            :   Suman Lata Gupta
////' Created On            :   6-Sept-2017 
////' Modification History  :
////'                           <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
////'                               1                       16-Feb-2018           Sushant Kumar Jena            Recoded as per Master table
////' Register File Name    :   
////' Style sheet           :   
////' JavaScript            :    
////'**************************************************************************************/--%>
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BusinessLogicLayer.Incentive;
using DataAcessLayer.Incentive;
using EntityLayer.Incentive;
using System.Collections.Specialized;
using System.Configuration;

public partial class Master_AddIncentive : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
   readonly string StrProjName = ConfigurationManager.AppSettings["ProjectName"].ToString();

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try 
        { 
        
           if (!IsPostBack)
           {
               Txt_Policy_Effect_Date.Attributes.Add("readonly", "readonly");
               FillSector();
           
               if (Request.QueryString["ID"] != null)
               {
                   FillEditData();
                   Btn_Submit.Text = "Update";
                   Btn_Reset.Text = "Cancel";
               }
           }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    //// Document Upload Delete and View
    #region Document Upload Delete and View

    //// Upload Document Button Click
    protected void LnkBtn_Add_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkBtn_Upload_Policy_Doc.ID))
            {
                if (FU_Policy_Doc.HasFile)
                {
                    string StrFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PLCDOC";
                    UploadDocument(FU_Policy_Doc, Hid_Policy_Doc_File_Name, StrFileName, Hyp_View_Policy_Doc, Lbl_Msg_Policy_Doc, LnkBtn_Delete_Policy_Doc, "PolicyDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Amend_Doc.ID) && FU_Amendment_Doc.HasFile)
            {
                
                    string StrFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_AMENDDOC";
                    UploadDocument(FU_Amendment_Doc, Hid_Amend_Doc_File_Name, StrFileName, Hyp_View_Amend_Doc, Lbl_Msg_Amend_Doc, LnkBtn_Delete_Amend_Doc, "PolicyDoc");
                
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    //// Delete Document Button Click
    protected void LnkBtn_Delete_Doc_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = (LinkButton)sender;
            if (string.Equals(lnk.ID, LnkBtn_Delete_Policy_Doc.ID))
            {
                UpdFileRemove(Hid_Policy_Doc_File_Name, LnkBtn_Upload_Policy_Doc, LnkBtn_Delete_Policy_Doc, Hyp_View_Policy_Doc, Lbl_Msg_Policy_Doc, FU_Policy_Doc, "PolicyDoc");
            }
            else if (string.Equals(lnk.ID, LnkBtn_Delete_Amend_Doc.ID))
            {
                UpdFileRemove(Hid_Amend_Doc_File_Name, LnkBtn_Upload_Amend_Doc, LnkBtn_Delete_Amend_Doc, Hyp_View_Amend_Doc, Lbl_Msg_Amend_Doc, FU_Amendment_Doc, "PolicyDoc");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    //// Method to Upload Document
    private void UploadDocument(FileUpload FileUpload_Document, HiddenField Hid_File_Name, string StrFileName, HyperLink Hyp_Document, Label Lbl_Upload_Msg, LinkButton LnkBtn_Delete_Doc, string StrFolderName)
    {
        try
        {
            string StrMainFolderPath = Server.MapPath(string.Format("../Incentive/{0}/", StrFolderName));
            if (!Directory.Exists(StrMainFolderPath))
            {
                Directory.CreateDirectory(StrMainFolderPath);
            }

            if (FileUpload_Document.HasFile)
            {
                string FileName = string.Empty;
                if (Path.GetExtension(FileUpload_Document.FileName).ToLower() != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload .pdf file Only .</strong>', '" + StrProjName + "'); </script>", false);
                    return;
                }

                if (!IsFileValid(FileUpload_Document))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid file type (or) File name may contain dots.</strong>', '" + StrProjName + "'); </script>", false);
                    return;
                }

                int fileSize = FileUpload_Document.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>File size is too large. Maximum file size permitted is 4 MB .</strong>', '" + StrProjName + "'); </script>", false);
                    return;
                }
                else
                {
                    FileName = StrFileName + Path.GetExtension(FileUpload_Document.FileName);
                }

                FileUpload_Document.SaveAs(StrMainFolderPath + FileName);
                Hid_File_Name.Value = FileName;
                Hyp_Document.NavigateUrl = string.Format("../Incentive/{0}/{1}", StrFolderName, FileName);
                Hyp_Document.Visible = true;
                LnkBtn_Delete_Doc.Visible = true;
                Lbl_Upload_Msg.Visible = true;
                FileUpload_Document.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    //// Method to Delete Document
    private void UpdFileRemove(HiddenField Hid_File_Name, LinkButton LnkBtn_Upload_Doc, LinkButton LnkBtn_Delete_Doc, HyperLink Hyp_Document, Label Lbl_Upload_Msg, FileUpload FileUpload_Document, string StrFolderName)
    {
        try
        {
            string FileName = Hid_File_Name.Value;
            string Path = string.Format("../Incentive/{0}/{1}", StrFolderName, FileName);
            string CompletePath = Server.MapPath(Path);
            if (File.Exists(CompletePath))
            {
                File.Delete(CompletePath);
            }

            Hid_File_Name.Value = "";
            LnkBtn_Delete_Doc.Visible = false;
            LnkBtn_Upload_Doc.Visible = true;
            Hyp_Document.Visible = false;
            Lbl_Upload_Msg.Visible = false;
            FileUpload_Document.Enabled = true;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    //// Method to Check File MimeType
    private bool IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf" };
        string[] allowedExtension = { ".pdf" };
        StringCollection ImageTypes = new StringCollection();
        StringCollection ImageExtension = new StringCollection();
        ImageTypes.AddRange(allowedImageTyps);
        ImageExtension.AddRange(allowedExtension);
        string StrFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
        string FileExt = Path.GetExtension(FileUpload1.FileName);
        int Count = FileUpload1.FileName.Count(f => f == '.');

        if (ImageTypes.Contains(StrFiletype) && ImageExtension.Contains(FileExt) && Count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    //// Function Used
    #region FunctionUsed

    //// Bind Sector Name
    private void FillSector()
    {
        IncentiveMaster ObjEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer ObjBAL = new IncentiveMasterBusinessLayer();
        try
        {
            ObjEntity.Action = "L";
            ObjBAL.BindDropdown(DrpDwn_Sector, ObjEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ObjEntity = null;
            ObjBAL = null;
        }
    }
    //// Bind Filtered SubSector
    private void FillSubSectorFiltered()
    {
        IncentiveMaster ObjEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer ObjBAL = new IncentiveMasterBusinessLayer();
        try
        {
            ObjEntity.Action = "sub";
            ObjEntity.Param_2 = DrpDwn_Sector.SelectedValue;
            ObjBAL.BindDropdown(DrpDwn_Sub_Sector, ObjEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ObjEntity = null;
            ObjBAL = null;
        }
    }
    ///// Fill Data for Edit
    private void FillEditData()
    {
        DataSet ds = new DataSet();
        IncentiveMasterBusinessLayer ObjLayer = new IncentiveMasterBusinessLayer();
        Policy_Master_Entity ObjEntity = new Policy_Master_Entity();
        try
        {
            ObjEntity.strAction = "E";
            ObjEntity.intPageNo = 0;
            ObjEntity.intPageSize = 0;
            ObjEntity.intPolicyId = Convert.ToInt32(Request.QueryString["ID"].ToString());

            ds = ObjLayer.Policy_Master_View(ObjEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grd_Section.DataSource = ds.Tables[1];
                Grd_Section.DataBind();

                Txt_Policy_Code.Text = ds.Tables[0].Rows[0]["vchPolicyCode"].ToString();
                Txt_Policy_Name.Text = ds.Tables[0].Rows[0]["vchPlcName"].ToString();
                Txt_Policy_Effect_Date.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["dtmEffectiveDate"]).ToString("dd-MMM-yyyy");
                Txt_Description.Text = ds.Tables[0].Rows[0]["vchDesc"].ToString();
                DrpDwn_Sector.SelectedValue = ds.Tables[0].Rows[0]["intSecTagId"].ToString();

                ////// Fill Filtered Sub-Sector
                FillSubSectorFiltered();

                DrpDwn_Sub_Sector.SelectedValue = ds.Tables[0].Rows[0]["intSubSecTagId"].ToString();
                DrpDwn_Policy_Category.SelectedValue = ds.Tables[0].Rows[0]["intPlcCat"].ToString();

                if (ds.Tables[0].Rows[0]["vchPlcDoc"].ToString() != "")
                {
                    Hid_Policy_Doc_File_Name.Value = ds.Tables[0].Rows[0]["vchPlcDoc"].ToString();
                    Hyp_View_Policy_Doc.NavigateUrl = "../Incentive/PolicyDoc/" + Hid_Policy_Doc_File_Name.Value;
                    Hyp_View_Policy_Doc.Visible = true;
                    LnkBtn_Delete_Policy_Doc.Visible = true;
                    FU_Policy_Doc.Enabled = false;
                }
                else
                {
                    Hid_Policy_Doc_File_Name.Value = "";
                    Hyp_View_Policy_Doc.Visible = false;
                    LnkBtn_Delete_Policy_Doc.Visible = false;
                    FU_Policy_Doc.Enabled = true;
                }

                if (ds.Tables[0].Rows[0]["vchAmendmentDoc"].ToString() != "")
                {
                    Hid_Amend_Doc_File_Name.Value = ds.Tables[0].Rows[0]["vchAmendmentDoc"].ToString();
                    Hyp_View_Amend_Doc.NavigateUrl = "../Incentive/PolicyDoc/" + Hid_Amend_Doc_File_Name.Value;
                    Hyp_View_Amend_Doc.Visible = true;
                    LnkBtn_Delete_Amend_Doc.Visible = true;
                    FU_Amendment_Doc.Enabled = false;
                }
                else
                {
                    Hid_Amend_Doc_File_Name.Value = "";
                    Hyp_View_Amend_Doc.Visible = false;
                    LnkBtn_Delete_Amend_Doc.Visible = false;
                    FU_Amendment_Doc.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        
    }
    ///// Clear Input Fields
    private void ClearFields()
    {
        Txt_Description.Text = "";
        Txt_Policy_Code.Text = "";
        Txt_Policy_Effect_Date.Text = "";
        Txt_Policy_Name.Text = "";
        Txt_Policy_Sec_Name.Text = "";
        Txt_Policy_Section.Text = "";

        if (DrpDwn_Policy_Category.SelectedIndex > 0)
        {
            DrpDwn_Policy_Category.SelectedIndex = 0;
        }

        if (DrpDwn_Sector.SelectedIndex > 0)
        {
            DrpDwn_Sector.SelectedIndex = 0;
        }

        if (DrpDwn_Sub_Sector.SelectedIndex > 0)
        {
            DrpDwn_Sub_Sector.SelectedIndex = 0;
        }
    }

    #endregion

    //// Button Submit and Update
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer ObjBAL = new IncentiveMasterBusinessLayer();
        Policy_Master_Entity ObjEntity = new Policy_Master_Entity();
        try
        {
            if (Request.QueryString["ID"] == null)
            {
                ObjEntity.strAction = "A";
                ObjEntity.intPolicyId = 0;
            }
            else
            {
                ObjEntity.strAction = "U";
                ObjEntity.intPolicyId = Convert.ToInt32(Request.QueryString["ID"].ToString());
            }

            /*---------------------------------------------------------------------*/
            ////// Add Section Item (Add More Part)

            #region Policy Section Details

            List<SectionMasterItem> ListSection = new List<SectionMasterItem>();

            for (int i = 0; i < Grd_Section.Rows.Count; i++)
            {
                SectionMasterItem objItem = new SectionMasterItem();

                Label Lbl_Section_No = (Label)Grd_Section.Rows[i].FindControl("Lbl_Section_No");
                Label Lbl_Section_Name = (Label)Grd_Section.Rows[i].FindControl("Lbl_Section_Name");

                objItem.vchSectionNo = Lbl_Section_No.Text;
                objItem.vchSectionName = Lbl_Section_Name.Text;

                ListSection.Add(objItem);
            }

            ObjEntity.listSectionItem = ListSection;

            #endregion

            /*---------------------------------------------------------------------*/

            ObjEntity.strPolicyCode = Convert.ToString(Txt_Policy_Code.Text.Trim());
            ObjEntity.strPolicyName = Convert.ToString(Txt_Policy_Name.Text.Trim());
            ObjEntity.strEffectiveDate = Convert.ToString(Txt_Policy_Effect_Date.Text);

            /*---------------------------------------------------------------------*/

            if (Hid_Policy_Doc_File_Name.Value != "")
            {
                ObjEntity.strPolicyDocs = Hid_Policy_Doc_File_Name.Value;
            }
            else
            {
                ObjEntity.strPolicyDocs = null;
            }

            /*---------------------------------------------------------------------*/

            if (Hid_Amend_Doc_File_Name.Value != "")
            {
                ObjEntity.strAmendmentDoc = Hid_Amend_Doc_File_Name.Value;
            }
            else
            {
                ObjEntity.strAmendmentDoc = null;
            }

            /*---------------------------------------------------------------------*/

            ObjEntity.strDecription = Convert.ToString(Txt_Description.Text);

            /*---------------------------------------------------------------------*/

            if (DrpDwn_Sector.SelectedIndex > 0)
            {
                ObjEntity.intSectorId = Convert.ToInt32(DrpDwn_Sector.SelectedValue);
            }
            else
            {
                ObjEntity.intSectorId = 0;
            }

            /*---------------------------------------------------------------------*/

            if (DrpDwn_Sub_Sector.SelectedIndex > 0)
            {
                ObjEntity.intSubSectorId = Convert.ToInt32(DrpDwn_Sub_Sector.SelectedValue);
            }
            else
            {
                ObjEntity.intSubSectorId = 0;
            }

            /*---------------------------------------------------------------------*/

            ObjEntity.intPolicyCat = Convert.ToInt32(DrpDwn_Policy_Category.SelectedValue);
            ObjEntity.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());

            /*---------------------------------------------------------------------*/
            ///// Add and Update
            string StrRetvalue = ObjBAL.Policy_Master_AED(ObjEntity);
            if (StrRetvalue == "1")
            {
                ClearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>" + Messages.ShowMessage(StrRetvalue) + ".</strong>','" + StrProjName + "')", true);
            }
            else if (StrRetvalue == "2")
            {
                ClearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>" + Messages.ShowMessage(StrRetvalue) + ".</strong>');</script>", false);
            }
            else if (StrRetvalue == "5")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>" + Messages.ShowMessage(StrRetvalue) + ".</strong>','" + StrProjName + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>" + Messages.ShowMessage("4") + ".</strong>','" + StrProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        
    }
    //// Button Reset and Cancel
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        if (Btn_Reset.Text == "Reset")
        {
            ClearFields();
        }
        else if (Btn_Reset.Text == "Cancel")
        {
            Response.Redirect("ViewIncentivePolicy.aspx?PIndex=" + Request.QueryString["PIndex"] + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "");
        }
    }

    //// LinkButton Add More
    protected void LnkBtn_Add_Section_Click(object sender, EventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            table.Columns.Add("vchSectionNo", typeof(string));
            table.Columns.Add("vchSectionName", typeof(string));

            table.Rows.Add(Txt_Policy_Section.Text, Txt_Policy_Sec_Name.Text);
            for (int i = 0; i < Grd_Section.Rows.Count; i++)
            {
                Label Lbl_Section_No = (Label)Grd_Section.Rows[i].FindControl("Lbl_Section_No");
                Label Lbl_Section_Name = (Label)Grd_Section.Rows[i].FindControl("Lbl_Section_Name");

                table.Rows.Add(Lbl_Section_No.Text, Lbl_Section_Name.Text);
            }

            Grd_Section.DataSource = table;
            Grd_Section.DataBind();

            Txt_Policy_Section.Text = "";
            Txt_Policy_Sec_Name.Text = "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        
    }
    //// ImageButton Delete
    protected void ImgBtn_Delete_Section_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton ImgBtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(ImgBtn.CommandArgument);

            table.Columns.Add("vchSectionNo", typeof(string));
            table.Columns.Add("vchSectionName", typeof(string));

            for (int i = 0; i < Grd_Section.Rows.Count; i++)
            {
                if (i != RowID)
                {
                    Label Lbl_Section_No = (Label)Grd_Section.Rows[i].FindControl("Lbl_Section_No");
                    Label Lbl_Section_Name = (Label)Grd_Section.Rows[i].FindControl("Lbl_Section_Name");

                    table.Rows.Add(Lbl_Section_No.Text, Lbl_Section_Name.Text);
                }
            }

            Grd_Section.DataSource = table;
            Grd_Section.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        
    }
    //// DropDownList SelectedIndexChanged
    protected void DrpDwn_Sector_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillSubSectorFiltered();
    }
}