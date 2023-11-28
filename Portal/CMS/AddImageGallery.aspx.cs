#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   AddImageGallery.aspx.cs
// Description           :   Add ImageGallery
// Created by            :   Sanghamitra Samal
// Created On            :   05 Sep 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Common;
using EntityLayer.Common;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;


public partial class Miscellaneous_AddImageGallery : SessionCheck
{
    #region Variable Declaration
    CommonBusinessLayer objService = new CommonBusinessLayer();
    Gallery objServiceEntity = new Gallery();
    string str_Retvalue = "";
    string strFileName = "";
    int intFileError = 0;
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fileUploadImage.Attributes["onchange"] = "UploadFile(this)";
            if (Request.QueryString["id"] != null)
            {
                editData(Convert.ToInt32(Request.QueryString["id"]));
            }
        }
    }
    #endregion

    #region Common_Functions

    public void editData(int id)
    {
        try
        {
            EntityLayer.Common.Gallery objdata = new EntityLayer.Common.Gallery();
            objdata = objService.EditGallery(id);
            if (objdata != null)
            {
                txtImgDesc.Text = objdata.vchImgDescription.ToString();
                lblUpload.Text = objdata.vchImage.ToString();
                ViewState["id"] = id;
                hdnval.Value = Convert.ToString(id);
                btnSave.Text = "Update";
                ViewState["ImagePath"] = objdata.vchImage.ToString();

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }

    //private void ImageUpload()
    //{
    //    string pathname = string.Empty;
    //    try
    //    {

    //        if (hdnval.Value == null)
    //        {
    //            string Extn = System.IO.Path.GetExtension(fileUploadImage.FileName);
    //            if (fileUploadImage.HasFile)
    //            {
    //                if (fileUploadImage.PostedFile.ContentLength > 1024 * 1024)//1 MB
    //                {
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File size must be within 1 MB !');", true);
    //                }
    //                else
    //                {
    //                    int intLength = 0;
    //                    string strExtension = fileUploadImage.FileName.ToString().Substring(intLength = fileUploadImage.FileName.ToString().LastIndexOf('.') + 1, fileUploadImage.FileName.ToString().Length - intLength);
    //                    strFileName = txtImgDesc.Text.Trim() + "." + strExtension;
    //                    fileUploadImage.SaveAs(Server.MapPath("~/Portal/ImageGallery/") + strFileName);
    //                }
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Please upload valid file !');", true);
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            if (fileUploadImage.HasFile)
    //            {
    //                if (fileUploadImage.PostedFile.ContentLength > 1024 * 1024)//1 MB
    //                {
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File size must be within 1 MB !');", true);
    //                    return;
    //                }
    //                else
    //                {
    //                    int intLength = 0;
    //                    string strExtension = fileUploadImage.FileName.ToString().Substring(intLength = fileUploadImage.FileName.ToString().LastIndexOf('.') + 1, fileUploadImage.FileName.ToString().Length - intLength);
    //                    //strFileName = txtImgDesc.Text.Trim() + "." + strExtension;
    //                    strFileName = fileUploadImage.FileName;
    //                    fileUploadImage.SaveAs(Server.MapPath("~/Portal/ImageGallery/") + strFileName);
    //                }
    //            }
    //            else
    //            {
    //                strFileName = ViewState["ImagePath"].ToString();
    //            }

    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert( " + ex.Message.Replace('"', ' ').ToString() + ");", true);
    //    }
    //}
    #endregion

    #region Button_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValid(fileUploadImage);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
        finally
        {
            //txtImgDesc.Text = "";
        }
    }
    #endregion

    private bool IsFileValid(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
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
        }
        //if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
        //{
        if (btnSave.Text == "Update")
        {
            if (FileUpload1.HasFile)
            {
                if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
                {
                    objServiceEntity.strAction = "U";
                    objServiceEntity.intImageId = Convert.ToInt32(hdnval.Value);
                    if (System.IO.File.Exists(Server.MapPath("~/Portal/ImageGallery/") + "B_" + fileUploadImage.FileName))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File with this name already exists !');", true);
                        return false;
                    }
                    objServiceEntity.vchImgDescription = txtImgDesc.Text;

                    if (hdnval.Value == null)
                    {
                        string Extn = System.IO.Path.GetExtension(fileUploadImage.FileName);
                        if (fileUploadImage.HasFile)
                        {
                            if (fileUploadImage.PostedFile.ContentLength > 1024 * 1024)//1 MB
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File size must be within 1 MB !');", true);
                            }
                            else
                            {
                                int intLength = 0;
                                string strExtension = fileUploadImage.FileName.ToString().Substring(intLength = fileUploadImage.FileName.ToString().LastIndexOf('.') + 1, fileUploadImage.FileName.ToString().Length - intLength);
                                strFileName = txtImgDesc.Text.Trim() + "." + strExtension;
                                fileUploadImage.SaveAs(Server.MapPath("~/Portal/ImageGallery/") + strFileName);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Please upload valid file !');", true);
                            return false;
                        }
                    }
                    else
                    {
                        if (fileUploadImage.HasFile)
                        {
                            if (fileUploadImage.PostedFile.ContentLength > 1024 * 1024)//1 MB
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File size must be within 1 MB !');", true);
                                return false;
                            }
                            else
                            {
                                int intLength = 0;
                                string strExtension = fileUploadImage.FileName.ToString().Substring(intLength = fileUploadImage.FileName.ToString().LastIndexOf('.') + 1, fileUploadImage.FileName.ToString().Length - intLength);
                                strFileName = fileUploadImage.FileName;
                                //fileUploadImage.SaveAs(Server.MapPath("~/Portal/ImageGallery/") + strFileName);

                                ////// Added by Sushant Jena Dt.08-Mar-2018
                                resizeAndUploadImage(fileUploadImage, strFileName);
                            }
                        }
                        else
                        {
                            strFileName = ViewState["ImagePath"].ToString();
                        }
                    }

                    objServiceEntity.vchImage = strFileName;
                    objServiceEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                    return false;
                }
            }
            else
            {
                objServiceEntity.strAction = "U";
                objServiceEntity.intImageId = Convert.ToInt32(hdnval.Value);
                if (System.IO.File.Exists(Server.MapPath("~/Portal/ImageGallery/") + "B_"+fileUploadImage.FileName))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File with this name already exists !');", true);
                    return false;
                }
                objServiceEntity.vchImgDescription = txtImgDesc.Text;

                if (hdnval.Value == null)
                {
                    string Extn = System.IO.Path.GetExtension(fileUploadImage.FileName);
                    if (fileUploadImage.HasFile)
                    {
                        if (fileUploadImage.PostedFile.ContentLength > 1024 * 1024)//1 MB
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File size must be within 1 MB !');", true);
                        }
                        else
                        {
                            int intLength = 0;
                            string strExtension = fileUploadImage.FileName.ToString().Substring(intLength = fileUploadImage.FileName.ToString().LastIndexOf('.') + 1, fileUploadImage.FileName.ToString().Length - intLength);
                            strFileName = txtImgDesc.Text.Trim() + "." + strExtension;
                            fileUploadImage.SaveAs(Server.MapPath("~/Portal/ImageGallery/") + strFileName);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Please upload valid file !');", true);
                        return false;
                    }
                }
                else
                {
                    if (fileUploadImage.HasFile)
                    {
                        if (fileUploadImage.PostedFile.ContentLength > 1024 * 1024)//1 MB
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File size must be within 1 MB !');", true);
                            return false;
                        }
                        else
                        {
                            int intLength = 0;
                            string strExtension = fileUploadImage.FileName.ToString().Substring(intLength = fileUploadImage.FileName.ToString().LastIndexOf('.') + 1, fileUploadImage.FileName.ToString().Length - intLength);
                            strFileName = fileUploadImage.FileName;
                            //fileUploadImage.SaveAs(Server.MapPath("~/Portal/ImageGallery/") + strFileName);

                            ////// Added by Sushant Jena Dt.08-Mar-2018
                            resizeAndUploadImage(fileUploadImage, strFileName);
                        }
                    }
                    else
                    {
                        strFileName = ViewState["ImagePath"].ToString();
                    }
                }

                objServiceEntity.vchImage = strFileName;
                objServiceEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            }
        }
        else
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                objServiceEntity.strAction = "A";

                /*-------------------------------------------------------------------------*/
                ////// Added by Sushant Jena on Dt.09-Mar-2018

                int intMinHeight = 496;
                int intMinWidth = 750;

                System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                int height = img.Height;
                int width = img.Width;

                if (height < intMinHeight || width < intMinWidth)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Minimum allowed file resolution is " + intMinWidth.ToString() + " X " + intMinHeight.ToString() + " !');", true);
                    return false;
                }

                /*-------------------------------------------------------------------------*/

                if (System.IO.File.Exists(Server.MapPath("~/Portal/ImageGallery/") + "B_" + fileUploadImage.FileName))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File with this name already exists !');", true);
                    return false;
                }
                objServiceEntity.vchImgDescription = txtImgDesc.Text;

                if (hdnval.Value == null)
                {
                    string Extn = System.IO.Path.GetExtension(fileUploadImage.FileName);
                    if (fileUploadImage.HasFile)
                    {
                        if (fileUploadImage.PostedFile.ContentLength > 1024 * 1024)//1 MB
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File size must be within 1 MB !');", true);
                        }
                        else
                        {
                            int intLength = 0;
                            string strExtension = fileUploadImage.FileName.ToString().Substring(intLength = fileUploadImage.FileName.ToString().LastIndexOf('.') + 1, fileUploadImage.FileName.ToString().Length - intLength);
                            strFileName = txtImgDesc.Text.Trim() + "." + strExtension;
                            //fileUploadImage.SaveAs(Server.MapPath("~/Portal/ImageGallery/") + strFileName);

                            ////// Added by Sushant Jena Dt.08-Mar-2018
                            resizeAndUploadImage(fileUploadImage, strFileName);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Please upload valid file !');", true);
                        return false;
                    }
                }
                else
                {
                    if (fileUploadImage.HasFile)
                    {
                        if (fileUploadImage.PostedFile.ContentLength > 1024 * 1024)//1 MB
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('File size must be within 1 MB !');", true);
                            return false;
                        }
                        else
                        {
                            int intLength = 0;
                            string strExtension = fileUploadImage.FileName.ToString().Substring(intLength = fileUploadImage.FileName.ToString().LastIndexOf('.') + 1, fileUploadImage.FileName.ToString().Length - intLength);
                            //strFileName = txtImgDesc.Text.Trim() + "." + strExtension;
                            strFileName = fileUploadImage.FileName;
                            //fileUploadImage.SaveAs(Server.MapPath("~/Portal/ImageGallery/") + strFileName);

                            ////// Added by Sushant Jena Dt.08-Mar-2018
                            resizeAndUploadImage(fileUploadImage, strFileName);
                        }
                    }
                    else
                    {
                        strFileName = ViewState["ImagePath"].ToString();
                    }
                }

                objServiceEntity.vchImage = strFileName;
                objServiceEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }

        str_Retvalue = objService.ManageGallery(objServiceEntity);
        if (str_Retvalue == "1")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>jAlert('Data Added Successfully !');</script>'");
            txtImgDesc.Text = "";
        }
        else if (str_Retvalue == "4")
        {
            txtImgDesc.Text = "";
            txtImgDesc.Focus();
            ClientScript.RegisterClientScriptBlock(this.GetType(), "validate", "<script>jAlert('Image Name already Exists !" + "');</script>");
        }
        else if (str_Retvalue == "2")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Updated Sucessfully !', '" + Messages.TitleOfProject + "', function () {location.href = 'ViewImageGallery.aspx';}); </script>", false);
            txtImgDesc.Text = "";
        }

        return true;
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('FileMime type is not Correct !');", true);
        //    return false;
        //}
    }

    ///// Resize Image to Medium and Small Size and then Store
    private void resizeAndUploadImage(FileUpload FileUpload1, string strFileName)
    {
        ///// Store Original Image with Prefix 'B_'
        FileUpload1.SaveAs(Server.MapPath("~/Portal/ImageGallery/") + "B_" + strFileName);

        Stream strm = FileUpload1.PostedFile.InputStream;
        using (var image = System.Drawing.Image.FromStream(strm))
        {
            /*---------------------------------------------------------------*/
            // Store Medium Size Image with Prefix 'M_' After Resizing
            /*---------------------------------------------------------------*/
            int newWidth = 750;  // New Width of Image in Pixel  
            int newHeight = 496;   // New Height of Image in Pixel  
            var thumbImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imgRectangle);
            // Save the file   
            string targetPath = Server.MapPath("~/Portal/ImageGallery/") + "M_" + strFileName;
            thumbImg.Save(targetPath, image.RawFormat);

            /*---------------------------------------------------------------*/
            // Store Small Size Image with Prefix 'S_' After Resizing
            /*---------------------------------------------------------------*/
            int newWidthSmall = 300;  // New Width of Image in Pixel  
            int newHeightSmall = 196;   // New Height of Image in Pixel  
            var thumbImgSmall = new Bitmap(newWidthSmall, newHeightSmall);
            var thumbGraphSmall = Graphics.FromImage(thumbImgSmall);
            thumbGraphSmall.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraphSmall.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraphSmall.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imgRectangleSmall = new Rectangle(0, 0, newWidthSmall, newHeightSmall);
            thumbGraphSmall.DrawImage(image, imgRectangleSmall);
            // Save the file   
            string targetPathSmall = Server.MapPath("~/Portal/ImageGallery/") + "S_" + strFileName;
            thumbImgSmall.Save(targetPathSmall, image.RawFormat);

            /*---------------------------------------------------------------*/
        }
    }
}