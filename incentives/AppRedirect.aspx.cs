using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class AppRedirect : SessionCheck
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util.SessionCheck();
        if (!IsPostBack)
        {         
                if (Request.QueryString["Key"] != null)
                {
                    try
                    {
                        string Key = Request.QueryString["Key"].ToString();
                        SSOService.ValidateService validate = new SSOService.ValidateService();
                        string url = validate.AppRedirectURL(Key, Session["UID"].ToString(), validate.URLEncryption(ConfigurationManager.AppSettings["SSOKey"]), Session["LogId"].ToString(), Session["SSOUserId"].ToString());
                        Response.Redirect(url);
                    }
                    catch (Exception ex)
                    {
                    }
               
            }
        }
    }
}