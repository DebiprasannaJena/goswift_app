using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Dashboard_CustomError : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //initialize our base class (System.Web,UI.Page)
        string redirectUrl = FormsAuthentication.LoginUrl;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();

        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Session.Abandon();
        FormsAuthentication.SignOut();
      
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}