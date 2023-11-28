#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   AddContentNews.aspx.cs
// Description           :   Add News details
// Created by            :   AMit Sahoo
// Created On            :  7 Sep 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion

#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Data;
using System.IO;
using System.Collections.Specialized;
#endregion

public partial class Portal_CMS_AddContentNews : SessionCheck
{
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    string str_Retvalue = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            fldAttachment.Attributes["onchange"] = "UploadFile(this)";
            btnSave.Text = "Submit";
            if (Request.QueryString["Id"] != null)
            {
                editdata();
            }
           
        }
       
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        EntityLayer.CMS.CMSDetails objServiceEntity = new EntityLayer.CMS.CMSDetails();
        try
        {
            IsFileValid(fldAttachment);
          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
            }
    private void Clear()
    {
        txtheading.Text = "";
        txtRemark.Text = "";
        radbtnlst.SelectedValue = "News";
    }
    private void editdata()
    {
        DataTable dt = new DataTable();
        dt = objService.BindCondData("CA",Convert.ToInt32( Request.QueryString["Id"].ToString()));
        if (dt.Rows.Count > 0)
        {
            txtheading.Text = dt.Rows[0]["VCH_HEADING"].ToString();
            if (dt.Rows[0]["VCH_TYPE"].ToString()=="News")
            {
                radbtnlst.SelectedValue = "News";
                divimg.Visible = true;
                divarea.Visible = false;
                divchk.Visible = true;
                txtRemark.Text = dt.Rows[0]["VCH_CONTENT"].ToString();
            }
            else if (dt.Rows[0]["VCH_TYPE"].ToString() == "Annoncement")
            {
                radbtnlst.SelectedValue = "Annoncement";
                divimg.Visible = false;
                divarea.Visible = true;
                divchk.Visible = false;
                txtarea.Text = dt.Rows[0]["VCH_CONTENT"].ToString();
            }
            else if (dt.Rows[0]["VCH_TYPE"].ToString() == "Notification")
            {
                radbtnlst.SelectedValue = "Notification";
                divimg.Visible = false;
                divarea.Visible = true;
                divchk.Visible = false;
                txtarea.Text = dt.Rows[0]["VCH_CONTENT"].ToString();
            }
            //txtRemark.Text = dt.Rows[0]["VCH_CONTENT"].ToString();
            btnSave.Text = "Update";
            //ImgPhoto.ImageUrl = "~/CMSImageGallery/" + dt.Rows[0]["VCH_IMAGE"].ToString();
            ViewState["ImagePath"] = dt.Rows[0]["VCH_IMAGE"].ToString();
            lblUFName.Text = ViewState["ImagePath"].ToString() ;

        }
    }
    private bool IsFileValid(FileUpload FileUpload1)
    {
        bool imgType = true; string strFiletype = ""; string fileExt = ""; int count = 0; 
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "image/gif", "image/x-png", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".gif" };         
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
             strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
             fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
             count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
         
            imgType = imageTypes.Contains(strFiletype);
        }

       // cmmf.IsValidStringWithoutSpace(ref filename,1)

        if (btnSave.Text == "Update")
        {
            if (FileUpload1.HasFile)
            {
                if (imgType == true && imageExtension.Contains(fileExt) && count == 1)
                {
                    objServiceEntity.StrAction = "UN";
                    objServiceEntity.Strtype = radbtnlst.SelectedValue.ToString();
                    objServiceEntity.Strheading = txtheading.Text.ToString();
                    //objServiceEntity.StrContent = txtRemark.Text.ToString();
                    objServiceEntity.IntCreatedBy = Convert.ToInt32(Session["InvestorId"]);
                    objServiceEntity.IntCmsId = Convert.ToInt32(Request.QueryString["Id"].ToString());
                    if (radbtnlst.SelectedValue.ToString() == "News")
                    {
                        string strCon = MimeType.GetHTMLtext(txtRemark.Text.ToString());
                        objServiceEntity.StrContent = MimeType.GetHTMLtext(txtRemark.Text.ToString());
                        if (!fldAttachment.HasFile)
                        {
                            objServiceEntity.strpath = ViewState["ImagePath"].ToString();
                        }
                        else
                        {
                            if (fldAttachment.HasFile)
                            {
                                if (fldAttachment.PostedFile.ContentLength > 2096000)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File size  should be less than or equal to 2 MB !','" + Messages.TitleOfProject + "'); </script>", false);
                                }
                                else if (Path.GetExtension(fldAttachment.FileName) != ".gif" && Path.GetExtension(fldAttachment.FileName) != ".jpeg" && Path.GetExtension(fldAttachment.FileName) != ".jpg" && Path.GetExtension(fldAttachment.FileName) != ".png")
                                {

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please upload file in .gif, .jpeg, .jpg and .png format !','" + Messages.TitleOfProject + "'); </script>", false);
                                }
                                else
                                {
                                    string retmsg = string.Empty;
                                    string path = Server.MapPath("~/CMSImageGallery/");
                                    if (!Directory.Exists(path))
                                    {
                                        Directory.CreateDirectory(path);
                                    }
                                    fldAttachment.SaveAs(path + "/" + fldAttachment.FileName);
                                    objServiceEntity.strAttachment = path;
                                    objServiceEntity.Strimg = fldAttachment.FileName;
                                    objServiceEntity.strpath = path + fldAttachment.FileName;
                                }
                            }
                            else
                            {
                                objServiceEntity.Strimg = "";

                            }
                        }
                    }
                    else
                    {
                        objServiceEntity.StrContent = txtarea.Text.ToString();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                    return false;
                }
            }
            else
            {
                objServiceEntity.StrAction = "UN";
                objServiceEntity.Strtype = radbtnlst.SelectedValue.ToString();
                objServiceEntity.Strheading = txtheading.Text.ToString();
                //objServiceEntity.StrContent = txtRemark.Text.ToString();
                objServiceEntity.IntCreatedBy = Convert.ToInt32(Session["InvestorId"]);
                objServiceEntity.IntCmsId = Convert.ToInt32(Request.QueryString["Id"].ToString());
                if (radbtnlst.SelectedValue.ToString() == "News")
                {
                    string strCon = MimeType.GetHTMLtext(txtRemark.Text.ToString());
                    objServiceEntity.StrContent = MimeType.GetHTMLtext(txtRemark.Text.ToString());
                    if (!fldAttachment.HasFile)
                    {
                        objServiceEntity.strpath = ViewState["ImagePath"].ToString();
                    }
                    else
                    {
                        if (fldAttachment.HasFile)
                        {
                            if (fldAttachment.PostedFile.ContentLength > 2096000)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File size  should be less than or equal to 2 MB !','" + Messages.TitleOfProject + "'); </script>", false);
                            }
                            else if (Path.GetExtension(fldAttachment.FileName) != ".gif" && Path.GetExtension(fldAttachment.FileName) != ".jpeg" && Path.GetExtension(fldAttachment.FileName) != ".jpg" && Path.GetExtension(fldAttachment.FileName) != ".png")
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please upload file in .gif, .jpeg, .jpg and .png format !','" + Messages.TitleOfProject + "'); </script>", false);
                            }
                            else
                            {
                                string retmsg = string.Empty;
                                string path = Server.MapPath("~/CMSImageGallery/");
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                fldAttachment.SaveAs(path + "/" + fldAttachment.FileName);
                                objServiceEntity.strAttachment = path;
                                objServiceEntity.Strimg = fldAttachment.FileName;
                                objServiceEntity.strpath = path + fldAttachment.FileName;
                            }
                        }
                        else
                        {
                            objServiceEntity.Strimg = "";

                        }
                    }
                }
                else
                {
                    objServiceEntity.StrContent = txtarea.Text.ToString();
                }
            }
            str_Retvalue = objService.AddNews(objServiceEntity);
            if (str_Retvalue == "2")
            {
                Clear();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Updated Sucessfully !','" + Messages.TitleOfProject + "'); </script>", false);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Updated Sucessfully !', '" + Messages.TitleOfProject + "', function () {location.href = 'ViewContentNews.aspx?linkn=" + Request.QueryString["linkn"].ToString() + "&linkm=" + Request.QueryString["linkm"].ToString() + "&btn=" + Request.QueryString["btn"].ToString() + "&tab=" + Request.QueryString["tab"].ToString() + "';}); </script>", false);
            }

        }


        else
        {
            if (imgType == true && imageExtension.Contains(fileExt) && count == 1)
            {
                objServiceEntity.StrAction = "AN";
                objServiceEntity.Strtype = radbtnlst.SelectedValue.ToString();
                objServiceEntity.Strheading = txtheading.Text.ToString();
                //objServiceEntity.StrContent = txtRemark.Text.ToString();
                objServiceEntity.IntCreatedBy = Convert.ToInt32(Session["InvestorId"]);
                if (radbtnlst.SelectedValue.ToString() == "News")
                {
                    objServiceEntity.StrContent = MimeType.GetHTMLtext(txtRemark.Text.ToString());
                    if (fldAttachment.HasFile)
                    {
                        if (fldAttachment.PostedFile.ContentLength > 2096000)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('File size  should be less than or equal to 2 MB !', '" + Messages.TitleOfProject + "'); </script>", false);
                        }
                        else if (Path.GetExtension(fldAttachment.FileName) != ".gif" && Path.GetExtension(fldAttachment.FileName) != ".jpeg" && Path.GetExtension(fldAttachment.FileName) != ".jpg" && Path.GetExtension(fldAttachment.FileName) != ".png")
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Please upload file in .gif, .jpeg, .jpg and .png format !', '" + Messages.TitleOfProject + "'); </script>", false);
                        }
                        else
                        {
                            string retmsg = string.Empty;
                            string path = Server.MapPath("~/CMSImageGallery/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            fldAttachment.SaveAs(path + "/" + fldAttachment.FileName);
                            objServiceEntity.strAttachment = path;
                            objServiceEntity.Strimg = fldAttachment.FileName;
                            objServiceEntity.strpath = path + fldAttachment.FileName;
                        }
                    }
                    else
                    {
                        objServiceEntity.Strimg = "";
                    }
                }
                else
                {
                    objServiceEntity.StrContent = txtarea.Text.ToString();
                }
                str_Retvalue = objService.AddNews(objServiceEntity);
                if (str_Retvalue == "1")
                {
                    Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Added Sucessfully !', '" + Messages.TitleOfProject + "'); </script>", false);
                }
            }


            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !', '" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
           
        }
        return true;
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
        //    return false;
        //}
    } 
}