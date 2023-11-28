using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Collections.Specialized;

public partial class Portal_CMS_AddUseFulink : System.Web.UI.Page
{
    #region Variable Declaration
    CmsBusinesslayer objserv = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    CMSDetails obj = new CMSDetails();
    List<CMSDetails> newlist = new List<CMSDetails>();
    string str_Retvalue = "";
    string SaveFilePath = "";
    string SaveFileName = "";
    string FilePath = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                btnSave.Text = "Update";
                EditlinkData(Convert.ToInt32(Request.QueryString["ID"]));
            }
        }
    }

    private void EditlinkData(int linkid)
    {
        try
        {
            CMSDetails objedit = new CMSDetails();
            List<CMSDetails> objComp = new List<CMSDetails>();

            objedit.actioncode = "S";
            objedit.intlinkId = linkid;
            objComp = objserv.UseFulLinkDetails(objedit).ToList();
            if (objComp.Count > 0)
            {
                txtUsefulLink.Text = objComp[0].vchUseFulinkName.ToString();
                txtURL.Text = objComp[0].vchURL.ToString();
                hdnFile.Value = objComp[0].vchUseImageURL.ToString();
                hlnkfile1.Text = objComp[0].vchUseImageURL.ToString();
                hlnkfile1.NavigateUrl = "~/UseFulLink/" + objComp[0].vchUseImageURL.ToString();

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CMSDetails objdata = new CMSDetails();
        if (btnSave.Text == "Update")
        {
            objdata.actioncode = "U";
            objdata.intlinkId = Convert.ToInt32(Request.QueryString["ID"]);
        }
        else
        {
            objdata.actioncode = "A";
        }
        IsFileValidImageURL(fileUploadImage);
        objdata.vchUseImageURL = hdnFile.Value;
        objdata.vchUseFulinkName = txtUsefulLink.Text.Trim();
        objdata.vchURL = txtURL.Text.Trim();
        obj.IntCreatedBy = Convert.ToInt32(Session["UserId"]);
        string strRes = objserv.AddUseFulinkDetails(objdata);
        if (strRes == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = 'AddUseFulink.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + Request.QueryString["index"] + "';});   </script>", false);

           // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data saved sucessfully');window.location ='AddUseFulink.aspx';", true);
           
        }
        if (strRes == "2")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Data Update Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = 'ViewUseFulink.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + Request.QueryString["index"] + "';});   </script>", false);

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data Update sucessfully');window.location ='ViewUseFulink.aspx';", true);
            
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddUseFulink.aspx");
    }
    private bool IsFileValidImageURL(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/UseFulLink/"));

        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(fileUploadImage.FileName) != ".pdf") && (Path.GetExtension(fileUploadImage.FileName) != ".png") && (Path.GetExtension(fileUploadImage.FileName) != ".jpg") && (Path.GetExtension(fileUploadImage.FileName) != ".jpeg"))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file Only!')", true);
                    return false;
                }
                int fileSize = fileUploadImage.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }

                else
                {
                    FilePath = string.Format("{0:yyyyMMddhhmmss}" + "UseFulImg" + Path.GetExtension(fileUploadImage.FileName), DateTime.Now);
                }

                if (!string.IsNullOrEmpty(fileUploadImage.FileName))
                {
                    if (dir.Exists)
                    {
                        fileUploadImage.SaveAs(Server.MapPath("~/UseFulLink/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/UseFulLink"));
                        fileUploadImage.SaveAs(Server.MapPath("~/UseFulLink/" + FilePath));
                    }
                    hdnFile.Value = FilePath;
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
}