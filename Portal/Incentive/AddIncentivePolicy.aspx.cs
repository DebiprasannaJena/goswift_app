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

public partial class Master_AddIncentive : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Txt_Policy_Effect_Date.Attributes.Add("readonly", "readonly");
            fillSector();

            if (Request.QueryString["ID"] != null)
            {
                fillEditData();
                Btn_Submit.Text = "Update";
                Btn_Reset.Text = "Cancel";
            }
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
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_PLCDOC";
                    UploadDocument(FU_Policy_Doc, Hid_Policy_Doc_File_Name, strFileName, Hyp_View_Policy_Doc, Lbl_Msg_Policy_Doc, LnkBtn_Delete_Policy_Doc, "PolicyDoc");
                }
            }
            else if (string.Equals(lnk.ID, LnkBtn_Upload_Amend_Doc.ID))
            {
                if (FU_Amendment_Doc.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_AMENDDOC";
                    UploadDocument(FU_Amendment_Doc, Hid_Amend_Doc_File_Name, strFileName, Hyp_View_Amend_Doc, Lbl_Msg_Amend_Doc, LnkBtn_Delete_Amend_Doc, "PolicyDoc");
                }
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
    private void UploadDocument(FileUpload FileUpload_Document, HiddenField Hid_File_Name, string strFileName, HyperLink Hyp_Document, Label Lbl_Upload_Msg, LinkButton LnkBtn_Delete_Doc, string strFoldername)
    {
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("../Incentive/{0}/", strFoldername));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }

            if (FileUpload_Document.HasFile)
            {
                string filename = string.Empty;
                if (Path.GetExtension(FileUpload_Document.FileName).ToLower() != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload .pdf file Only !!</strong>', '" + strProjName + "'); </script>", false);
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
                Hyp_Document.NavigateUrl = string.Format("../Incentive/{0}/{1}", strFoldername, filename);
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
    private void UpdFileRemove(HiddenField Hid_File_Name, LinkButton LnkBtn_Upload_Doc, LinkButton LnkBtn_Delete_Doc, HyperLink Hyp_Document, Label Lbl_Upload_Msg, FileUpload FileUpload_Document, string strFolderName)
    {
        try
        {
            string fileName = Hid_File_Name.Value;
            string path = string.Format("../Incentive/{0}/{1}", strFolderName, fileName);
            string completePath = Server.MapPath(path);
            if (File.Exists(completePath))
            {
                File.Delete(completePath);
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

    #endregion

    //// Function Used
    #region FunctionUsed

    //// Bind Sector Name
    private void fillSector()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "L";
            objBAL.BindDropdown(DrpDwn_Sector, objEntity);
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
    //// Bind Filtered SubSector
    private void fillSubSectorFiltered()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "sub";
            objEntity.Param_2 = DrpDwn_Sector.SelectedValue;
            objBAL.BindDropdown(DrpDwn_Sub_Sector, objEntity);
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
    ///// Fill Data for Edit
    private void fillEditData()
    {
        DataSet ds = new DataSet();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Policy_Master_Entity objEntity = new Policy_Master_Entity();
        try
        {
            objEntity.strAction = "E";
            objEntity.intPageNo = 0;
            objEntity.intPageSize = 0;
            objEntity.intPolicyId = Convert.ToInt32(Request.QueryString["ID"].ToString());

            ds = objLayer.Policy_Master_View(objEntity);
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
                fillSubSectorFiltered();

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
        finally
        {
            ds = null;
            objLayer = null;
            objEntity = null;
        }
    }
    ///// Clear Input Fields
    private void clearFields()
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
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Policy_Master_Entity objEntity = new Policy_Master_Entity();
        try
        {
            if (Request.QueryString["ID"] == null)
            {
                objEntity.strAction = "A";
                objEntity.intPolicyId = 0;
            }
            else
            {
                objEntity.strAction = "U";
                objEntity.intPolicyId = Convert.ToInt32(Request.QueryString["ID"].ToString());
            }

            /*---------------------------------------------------------------------*/
            ////// Add Section Item (Add More Part)

            #region Policy Section Details

            List<SectionMasterItem> listSection = new List<SectionMasterItem>();

            for (int i = 0; i < Grd_Section.Rows.Count; i++)
            {
                SectionMasterItem objItem = new SectionMasterItem();

                Label Lbl_Section_No = (Label)Grd_Section.Rows[i].FindControl("Lbl_Section_No");
                Label Lbl_Section_Name = (Label)Grd_Section.Rows[i].FindControl("Lbl_Section_Name");

                objItem.vchSectionNo = Lbl_Section_No.Text;
                objItem.vchSectionName = Lbl_Section_Name.Text;

                listSection.Add(objItem);
            }

            objEntity.listSectionItem = listSection;

            #endregion

            /*---------------------------------------------------------------------*/

            objEntity.strPolicyCode = Convert.ToString(Txt_Policy_Code.Text.Trim());
            objEntity.strPolicyName = Convert.ToString(Txt_Policy_Name.Text.Trim());
            objEntity.strEffectiveDate = Convert.ToString(Txt_Policy_Effect_Date.Text);

            /*---------------------------------------------------------------------*/

            if (Hid_Policy_Doc_File_Name.Value != "")
            {
                objEntity.strPolicyDocs = Hid_Policy_Doc_File_Name.Value;
            }
            else
            {
                objEntity.strPolicyDocs = null;
            }

            /*---------------------------------------------------------------------*/

            if (Hid_Amend_Doc_File_Name.Value != "")
            {
                objEntity.strAmendmentDoc = Hid_Amend_Doc_File_Name.Value;
            }
            else
            {
                objEntity.strAmendmentDoc = null;
            }

            /*---------------------------------------------------------------------*/

            objEntity.strDecription = Convert.ToString(Txt_Description.Text);

            /*---------------------------------------------------------------------*/

            if (DrpDwn_Sector.SelectedIndex > 0)
            {
                objEntity.intSectorId = Convert.ToInt32(DrpDwn_Sector.SelectedValue);
            }
            else
            {
                objEntity.intSectorId = 0;
            }

            /*---------------------------------------------------------------------*/

            if (DrpDwn_Sub_Sector.SelectedIndex > 0)
            {
                objEntity.intSubSectorId = Convert.ToInt32(DrpDwn_Sub_Sector.SelectedValue);
            }
            else
            {
                objEntity.intSubSectorId = 0;
            }

            /*---------------------------------------------------------------------*/

            objEntity.intPolicyCat = Convert.ToInt32(DrpDwn_Policy_Category.SelectedValue);
            objEntity.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());

            /*---------------------------------------------------------------------*/
            ///// Add and Update
            string strRetvalue = objBAL.Policy_Master_AED(objEntity);
            if (strRetvalue == "1")
            {
                clearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>" + Messages.ShowMessage(strRetvalue) + " !</strong>','" + strProjName + "')", true);
            }
            else if (strRetvalue == "2")
            {
                clearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>" + Messages.ShowMessage(strRetvalue) + "!</strong>');</script>", false);
            }
            else if (strRetvalue == "5")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>" + Messages.ShowMessage(strRetvalue) + " !</strong>','" + strProjName + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>" + Messages.ShowMessage("4") + " !</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }
    //// Button Reset and Cancel
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        if (Btn_Reset.Text == "Reset")
        {
            clearFields();
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
        finally
        {
            table = null;
        }
    }
    //// ImageButton Delete
    protected void ImgBtn_Delete_Section_Click(object sender, ImageClickEventArgs e)
    {
        DataTable table = new DataTable();
        try
        {
            ImageButton imgbtn = (ImageButton)sender;
            int RowID = Convert.ToInt16(imgbtn.CommandArgument);

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
        finally
        {
            table = null;
        }
    }
    //// DropDownList SelectedIndexChanged
    protected void DrpDwn_Sector_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillSubSectorFiltered();
    }
}