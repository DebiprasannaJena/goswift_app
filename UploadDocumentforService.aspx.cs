using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class UploadDocumentforService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try{
        if (docUpload.HasFile)
        {
            int fileSize = docUpload.PostedFile.ContentLength;
            //if (Path.GetExtension(docUpload.FileName) != ".pdf")
            //{
            //    string strmsg11 = "alert('Only .pdf file accepted!');";

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Click", strmsg11, true);
            //    return;
            //}

            if (fileSize > (1 * 1024 * 1024))
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OnClick", "<script> jAlert('File size is too large. Maximum file size permitted is 4 MB !','" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath(txtPath.Text));
        if (!string.IsNullOrEmpty(docUpload.FileName))
        {
            if (dir.Exists)
            {
                docUpload.SaveAs(Server.MapPath(txtPath.Text +"/"+ txtFileName.Text + System.IO.Path.GetExtension(docUpload.FileName.ToString())));
            }
            else
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(txtPath.Text));
                docUpload.SaveAs(Server.MapPath(txtPath.Text +"/"+ txtFileName.Text + System.IO.Path.GetExtension(docUpload.FileName.ToString())));

            }
           
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Document saved Successfully !','" + Messages.TitleOfProject + "'); </script>", false);
        txtFileName.Text = "";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service File Upload");
        }
    }
}