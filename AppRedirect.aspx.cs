using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using DWHServiceReference;

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
                    //SSOService.ValidateService validate = new SSOService.ValidateService();
                    DWHServiceHostClient validate = new DWHServiceHostClient();
                    string url = validate.AppRedirectURL(Key, Session["UID"].ToString(), validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]), Session["LogId"].ToString(), Session["SSOUserId"].ToString());
                    Util.LogRequestResponse("SSORedirect", "SSORedirectKey", "[Key]:- " + Key);
                    Util.LogRequestResponse("SSORedirect", "SSORedirectUID", "[UID]:- " + Convert.ToString(Session["UID"]));
                    Util.LogRequestResponse("SSORedirect", "SSORedirectULREncription", "[ULREncription]:- " + ConfigurationManager.AppSettings["DWHEncryptionKey"]);
                    Util.LogRequestResponse("SSORedirect", "SSORedirectKey", "[LogId]:- " + Convert.ToString(Session["LogId"]));
                    Util.LogRequestResponse("SSORedirect", "SSORedirectKey", "[SSOUserId]:- " + Convert.ToString(Session["SSOUserId"]));
                    Util.LogRequestResponse("SSORedirect", "SSORedirectURL", "[URL]:- " + url);

                    Response.Redirect(url);
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "AppRedirect");
                }
            }
        }
    }
}