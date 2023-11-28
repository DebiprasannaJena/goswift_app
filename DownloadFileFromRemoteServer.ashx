<%@ WebHandler Language="C#" Class="DownloadFileFromRemoteServer" %>

using System;
using System.Web;

public class DownloadFileFromRemoteServer : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        var strFileName = context.Request.QueryString["fileName"];
        var strFilePath = context.Request.QueryString["fileFullPath"];

        //context.Response.Clear();
        //context.Response.ContentType = "application/octet-stream";   
        //context.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName);
        //context.Response.WriteFile(strFilePath);
        //context.Response.End();


        string[] strArr = strFileName.Split('.');
        string fileExt = strArr[1];
        if (fileExt.ToUpper() == "PDF")
        {
            context.Response.Clear();
            context.Response.ContentType = "application/pdf";
            context.Response.BinaryWrite(System.IO.File.ReadAllBytes(strFilePath));
            context.Response.AddHeader("Content-Length", new System.IO.FileInfo(strFilePath).Length.ToString());
            context.Response.End();
        }
        else if (fileExt.ToUpper() == "TXT")
        {
            context.Response.Clear();
            context.Response.ContentType = "application/octet-stream";
            context.Response.BinaryWrite(System.IO.File.ReadAllBytes(strFilePath));
            //context.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName);
            context.Response.AddHeader("Content-Length", new System.IO.FileInfo(strFilePath).Length.ToString());
            //context.Response.BinaryWrite(strFilePath);
            System.IO.File.ReadAllLines(strFilePath);
            context.Response.End();
        }
        else if (fileExt.ToUpper() == "JPEG" || fileExt.ToUpper() == "JPG")
        {

            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            context.Response.AppendHeader("content-length", new System.IO.FileInfo(strFilePath).Length.ToString());
            //context.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName);
            context.Response.WriteFile(strFilePath);
            context.Response.End();

            //context.Response.Clear();
            //context.Response.ContentType = "image/jpeg";
            ////context.Response.Write(System.IO.File.ReadAllBytes(strFilePath));
            //context.Response.TransmitFile(strFilePath);
            //context.Response.AddHeader("Content-Length", new System.IO.FileInfo(strFilePath).Length.ToString());

            //context.Response.End();
        }
        else if (fileExt.ToUpper() == "PNG")
        {
            context.Response.Clear();
            context.Response.ClearContent();
            context.Response.ContentType = "image/png";
            context.Response.AppendHeader("Content-Disposition", new System.IO.FileInfo(strFilePath).Length.ToString());
            context.Response.WriteFile(strFilePath);
            context.Response.End();

        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}