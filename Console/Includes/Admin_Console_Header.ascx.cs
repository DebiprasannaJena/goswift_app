using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//using CSMPDK_3_0;
using System.Collections.Generic;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;

public partial class Admin_Console_Header : System.Web.UI.UserControl
{
    AdminAppService objBAL = new AdminAppService();
    //CommonDLL objCmnDll = new CommonDLL();
    AdminApp.Model.IPTrack objiptrack = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
        if (Session["userName"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }
        if (!IsPostBack)
        {
            lblDynamicHeader.Text = objBAL.GetLoginHeader();
        }
    }
    protected void TrackIP()
    {
        objiptrack = new AdminApp.Model.IPTrack();
        objiptrack.ActionCode = "E";
        objiptrack.UserId = Convert.ToInt32(HttpContext.Current.Session["userid"]);
        int intTrackid = objBAL.IpTracking(objiptrack);
        objiptrack.ActionCode = "U";
        objiptrack.Id = intTrackid;
        objiptrack.UserName = Convert.ToString(HttpContext.Current.Session["userName"]);
        string strClientIp = null;
        strClientIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FOWARDED_FOR"];
        if (strClientIp == string.Empty || strClientIp == null)
        {
            strClientIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        objiptrack.IpAddress = strClientIp;
        objiptrack.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
        objBAL.IpTracking(objiptrack);

    }
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        //if (Session["userid"] != null)
        //{
        //    TrackIP();
        //}
        Session.Abandon();
        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Logout"].ToString()))
        {
            Response.Redirect(ConfigurationManager.AppSettings["Logout"].ToString());
        }
       
        
    }
    protected void lnkHome_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Home"].ToString()))
        {
            Response.Redirect(ConfigurationManager.AppSettings["Home"].ToString());
        }
       
    }
}
