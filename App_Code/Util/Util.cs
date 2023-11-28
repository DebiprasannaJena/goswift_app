using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
	public Util()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void SessionCheck()
    {
        if (HttpContext.Current.Session["UserId"] == null)
        {
            HttpContext.Current.Response.Redirect("~/LogOut.aspx");
        }
    }

    public static void LogError(Exception ex,string strModule)
    {
        try
        {
            string strFileName = strModule+"ErrorLog" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Logs/ErrorLog/" + strFileName);
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
        catch
        {
        }
    }

    public static void LogRequestResponse(string strModule, string strMethod, string strReqResData)
    {
        try
        {
            string strFileName = strModule + "ReqResLog" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += " For ";
            message += string.Format("RequestMethod: {0}", strMethod);
            message += Environment.NewLine;
            message += string.Format("InputOutput: {0}", strReqResData);
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";


            if (!Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/ReqResLog/")))
            {
                Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/ReqResLog/"));
            }

            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Logs/ReqResLog/" + strFileName);
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
        catch
        {
        }
    }
}