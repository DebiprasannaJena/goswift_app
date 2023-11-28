<%@ WebHandler Language="C#" Class="TestDownloadFileHandler" %>

using System;
using System.Web;

public class TestDownloadFileHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");


    
        string id = context.Request.QueryString["id"];
        //save into the database 
       // string fileName = "Anchor-Tenant.png";
        string fileName = "20190830042141_LICNDOC.pdf";
        context.Response.Clear();
        context.Response.ContentType = "application/pdf";
        context.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

        if (System.IO.File.Exists(context.Server.MapPath("~/Document/RegdDoc/") + fileName))
        {
            context.Response.TransmitFile(context.Server.MapPath("~/Document/RegdDoc/") + fileName);
        }
       
        //context.Response.WriteFile(context.Server.MapPath("~/Document/RegdDoc/") + fileName);
        context.Response.End();
        //download the file

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}