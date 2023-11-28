using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

/// <summary>
/// Summary description for SessionCheck
/// </summary>
public class SessionCheck : System.Web.UI.Page
{
    /// <summary>
    /// property vcariable for the URL Property
    /// </summary>
    private static string _redirectUrl;

    /// <summary>
    /// property to hold the redirect url we will
    /// use if the users session is expired or has
    /// timed out.
    /// </summary>
    public static string RedirectUrl
    {
        get { return _redirectUrl; }
        set { _redirectUrl = value; }
    }
    public SessionCheck()
    {
        //_redirectUrl = string.Empty;
    }

    override protected void OnInit(EventArgs e)
    {
        //initialize our base class (System.Web,UI.Page)
        base.OnInit(e);
        //check to see if the Session is null (doesnt exist)
        
        //if (Request.UrlReferrer == null)
        //{
        //    Response.Redirect("~/Login.aspx");
        //}
        if (Context.Session != null)
        {
            //check the IsNewSession value, this will tell us if the session has been reset.
            //IsNewSession will also let us know if the users session has timed out
            if (Session.IsNewSession)
            {
                //now we know it's a new session, so we check to see if a cookie is present
                string cookie = Request.Headers["Cookie"];
                //now we determine if there is a cookie does it contains what we're looking for
                if ((null != cookie) && (cookie.IndexOf("ASP.NET_SessionId") >= 0))
                {
                    //since it's a new session but a ASP.Net cookie exist we know
                    //the session has expired so we need to redirect them
                    if (RedirectUrl == null)
                    {
                        string strURL = Request.Url.ToString();
                       // string previousPageUrl = Request.UrlReferrer.ToString();
                        //string previousPageName = System.IO.Path.GetFileName(Request.UrlReferrer.AbsolutePath);

                        if (File.Exists(Server.MapPath("SessionTimeout.aspx")))
                        {
                            Response.Redirect("SessionTimeout.aspx");
                        }
                        else
                        {
                            Response.Redirect("../SessionRedirect.aspx");
                        }
                       
                    }
                    else
                    {
                        Response.Redirect(RedirectUrl);
                    }
                }
            }
        }
    }
}
