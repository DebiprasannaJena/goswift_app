//******************************************************************************************************************
// File Name             :   OG_Master.aspx.cs
// Description           :   Add,Update OG Master
// Created by            :   Sushant Kumar Jena
// Created on            :   7th Sept 2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.IO;
using System.Collections.Specialized;

public partial class Portal_Incentive_OG_Master : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    ///// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Txt_Effect_Date.Attributes.Add("readonly", "readonly");

            fillPolicy();
            Hid_OG_Id.Value = "0";

            if (Request.QueryString["og_id"] != null)
            {
                string val = Request.QueryString["og_id"].ToString();
                fillEditData(val);
            }
        }
    }

    ///// DropDownList SelectedIndexChanged
    protected void DrpDwn_Policy_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intPlcId = Convert.ToInt32(DrpDwn_Policy_Name.SelectedValue);
        fillSection(intPlcId);
    }

    ///// Function Used
    #region FunctionUsed

    ///// Bind Policy Name
    private void fillPolicy()
    {
        IncentiveMaster objIncentive = new IncentiveMaster();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        try
        {
            objIncentive.Action = "C";
            objLayer.BindDropdown(DrpDwn_Policy_Name, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
            objLayer = null;
        }
    }
    ///// Bind Filtered Section Against Policy
    private void fillSection(int intPlcId)
    {
        IncentiveMaster objIncentive = new IncentiveMaster();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        try
        {
            objIncentive.Action = "K";
            objIncentive.Param = intPlcId;
            objLayer.BindDropdown(DrpDwn_Section_Name, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
            objLayer = null;
        }
    }
    ///// Fill Data for Edit
    private void fillEditData(string strOGId)
    {
        DataSet ds = new DataSet();
        IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
        OG_Master_Entity objBU_Entity = new OG_Master_Entity();
        try
        {
            int? i_plc_id = null;

            objBU_Entity.intPlcId = i_plc_id;
            objBU_Entity.intOGId = Convert.ToInt32(strOGId);
            objBU_Entity.strAction = "E";

            ////// Call the Function to Fill Data
            ds = ObjIMB.OG_Master_View(objBU_Entity);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Txt_OG_Name.Text = ds.Tables[0].Rows[0]["vchOGName"].ToString();
                Txt_OG_Desc.Text = ds.Tables[0].Rows[0]["vchDesc"].ToString();

                Txt_Effect_Date.Text = String.Format("{0:dd-MMM-yyyy}", ds.Tables[0].Rows[0]["dtmOGEffcDate"]);

                DrpDwn_Policy_Name.SelectedValue = ds.Tables[0].Rows[0]["intPlcId"].ToString();
                Hid_OG_Id.Value = ds.Tables[0].Rows[0]["intOGId"].ToString();

                /////// Fill Section Against Policy Id
                fillSection(Convert.ToInt32(ds.Tables[0].Rows[0]["intPlcId"]));

                ////// Assign Section No. to DropDownList
                DrpDwn_Section_Name.SelectedValue = ds.Tables[0].Rows[0]["vchSectionNo"].ToString();

                if (ds.Tables[0].Rows[0]["vchOGDoc"].ToString() != "")
                {
                    Hid_OG_Doc_File_Name.Value = ds.Tables[0].Rows[0]["vchOGDoc"].ToString();
                    Hyp_View_OG_Doc.NavigateUrl = "../Incentive/OGDoc/" + Hid_OG_Doc_File_Name.Value;
                    Hyp_View_OG_Doc.Visible = true;
                    LnkBtn_Delete_OG_Doc.Visible = true;
                    FU_OG_Doc.Enabled = false;
                }
                else
                {
                    Hid_OG_Doc_File_Name.Value = "";
                    Hyp_View_OG_Doc.Visible = false;
                    LnkBtn_Delete_OG_Doc.Visible = false;
                    FU_OG_Doc.Enabled = true;
                }

                Btn_Submit.Text = "Update";
                Btn_Reset.Text = "Cancel";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ObjIMB = null;
            objBU_Entity = null;
            ds = null;
        }
    }
    ///// Clear Input Fields
    private void clearFields()
    {
        Txt_Effect_Date.Text = "";
        Txt_OG_Desc.Text = "";
        Txt_OG_Name.Text = "";
        DrpDwn_Policy_Name.SelectedIndex = 0;
        DrpDwn_Section_Name.SelectedIndex = 0;

        Btn_Submit.Text = "Submit";
        Btn_Reset.Text = "Reset";
        Hid_OG_Id.Value = "0";
    }

    #endregion

    ///// Button Submit and Update
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
        OG_Master_Entity objEntity = new OG_Master_Entity();

        try
        {
            objEntity.intOGId = Convert.ToInt32(Hid_OG_Id.Value);
            objEntity.intPlcId = Convert.ToInt32(DrpDwn_Policy_Name.SelectedValue);
            objEntity.strOGName = Txt_OG_Name.Text;
            objEntity.strOGDoc = Hid_OG_Doc_File_Name.Value;
            objEntity.strOGEffcDate = Txt_Effect_Date.Text;
            objEntity.strSectionNo = DrpDwn_Section_Name.SelectedValue;
            objEntity.strDesc = Txt_OG_Desc.Text.Trim();

            if (Btn_Submit.Text == "Submit")
            {
                objEntity.strAction = "A";
            }
            else if (Btn_Submit.Text == "Update")
            {
                objEntity.strAction = "U";
            }

            objEntity.intCreatedBy = Convert.ToInt32(Session["UserId"]);

            ////// Data Insert and Update
            string strReturnStatus = ObjIMB.OG_Master_AED(objEntity);
            if (Btn_Submit.Text == "Update")
            {
                clearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>" + Messages.ShowMessage(strReturnStatus) + "</strong>');</script>", false);
                //ScriptManager.RegisterStartupScript(Btn_Submit, this.GetType(), "Myalert", "alert('" + Messages.ShowMessage(strReturnStatus) + "');window.location.href='View_OG_Master.aspx?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "';", true);
            }
            else
            {
                clearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>" + Messages.ShowMessage(strReturnStatus) + "</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ObjIMB = null;
            objEntity = null;
        }
    }

    ///// Button Reset and Cancel
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        if (Btn_Reset.Text == "Reset")
        {
            clearFields();
        }
        else if (Btn_Reset.Text == "Cancel")
        {
            Response.Redirect("View_OG_Master.aspx");
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
            if (string.Equals(lnk.ID, LnkBtn_Upload_OG_Doc.ID))
            {
                if (FU_OG_Doc.HasFile)
                {
                    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_OGDOC";
                    UploadDocument(FU_OG_Doc, Hid_OG_Doc_File_Name, strFileName, Hyp_View_OG_Doc, Lbl_Msg_OG_Doc, LnkBtn_Delete_OG_Doc, "OGDoc");
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
            if (string.Equals(lnk.ID, LnkBtn_Delete_OG_Doc.ID))
            {
                UpdFileRemove(Hid_OG_Doc_File_Name, LnkBtn_Upload_OG_Doc, LnkBtn_Delete_OG_Doc, Hyp_View_OG_Doc, Lbl_Msg_OG_Doc, FU_OG_Doc, "OGDoc");
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
}