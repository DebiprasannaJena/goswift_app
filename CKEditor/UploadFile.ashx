<%@ WebHandler Language="C#" Class="UploadFile" %>

using System;
using System.Web;

public class UploadFile : IHttpHandler
{
    //public void ProcessRequest (HttpContext context) {
    //    HttpPostedFile uploads = context.Request.Files["upload"];
    //    string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
    //    string file = System.IO.Path.GetFileName(uploads.FileName);
    //    uploads.SaveAs(context.Server.MapPath(".") + "\\CKFiles\\" +  DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss-f")+file);
    //    string url = "../ckeditor/CKFiles/" +  DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss-f")+file;
    //    context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
    //    context.Response.End();   
    
     public void ProcessRequest (HttpContext context) {
        HttpPostedFile uploads = context.Request.Files["upload"];
        string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
        string file = System.IO.Path.GetFileName(uploads.FileName);
        uploads.SaveAs(context.Server.MapPath("CMSImage\\") + DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss-f") + file);
        string url = "../CKImages/" + DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss-f") + file;
        context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        context.Response.End();             
          
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}