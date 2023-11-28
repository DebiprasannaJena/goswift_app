<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;

public class Upload : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        HttpPostedFile uploads = context.Request.Files["upload"];
        string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
        string file = System.IO.Path.GetFileName(uploads.FileName);
        string strPath = context.Server.MapPath(".").Replace("CKEditor", "");
        uploads.SaveAs(context.Server.MapPath(".").Replace("CKEditor","") + "Portal\\CMSImage\\" + DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss-f") + file);
        string url =System.Configuration.ConfigurationManager.AppSettings["AppUrl"].ToString()+ "Portal/CMSImage/" + DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss-f") + file;
        context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        context.Response.End();             
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}