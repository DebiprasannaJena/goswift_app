<%@ WebHandler Language="C#" Class="FileHandler" %>

using System;
using System.Web;
using System.IO;

public class FileHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        DateTime dt = DateTime.Now;

        string strDate = dt.ToString("MMddyyyyHHmmss");
        try
        {
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    string newFilName = context.Request.QueryString["FnlDateFrmt"];
                    string pathrefer = context.Request.UrlReferrer.ToString();
                    string Serverpath = HttpContext.Current.Server.MapPath("Portal/Document/Upload");
                    HttpPostedFile postedFile = files[i];
                    string file;
                    if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = postedFile.FileName.Split(new char[] { '\\' });
                        file = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        file = postedFile.FileName;
                    }
                    if (!Directory.Exists(Serverpath))
                        Directory.CreateDirectory(Serverpath);
                    string fileDirectory = Serverpath;
                    if (context.Request.QueryString["fileName"] != null)
                    {
                        file = context.Request.QueryString["fileName"];
                        if (File.Exists(fileDirectory + "\\" + file))
                        {
                            File.Delete(fileDirectory + "\\" + file);
                        }
                    }
                    file = postedFile.FileName;
                    fileDirectory = Serverpath + "\\" + newFilName;
                    postedFile.SaveAs(fileDirectory);
                    context.Response.AddHeader("Vary", "Accept");
                    try
                    {
                        if (context.Request["HTTP_ACCEPT"].Contains("application/json"))
                            context.Response.ContentType = "application/json";
                        else
                            context.Response.ContentType = "text/plain";
                    }
                    catch (Exception exp)
                    {
                        context.Response.ContentType = "text/plain";
                        Util.LogError(exp, "ServiceUploadDoc");
                    }
                    context.Response.Write(newFilName);                    
                }
            }
        }
        catch (Exception exp)
        {
            context.Response.Write(exp.Message);
            Util.LogError(exp, "ServiceUploadDoc");
        }
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}