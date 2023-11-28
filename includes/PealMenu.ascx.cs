using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class includes_PealMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (Session["UserId"] == null || Session["IndustryType"] == null)
        {
            Response.Redirect("~/LogOut.aspx", false);
        }

        if (Convert.ToString(Session["IndustryType"]) == "2") ///If It's a Non-Indsutry type then logout from the system.
        {
            Response.Redirect("~/LogOut.aspx", false);
        }

        try
        {
            /*-----------------------------------------------------------*/
            ///// Display Manage Menu If the Unit is Main Unit (Parent Unit)
            /*-----------------------------------------------------------*/
            if (Session["ParentId"] != null)
            {
                if (Convert.ToInt32(Session["ParentId"]) == 0 && Convert.ToInt32(Session["UserLevel"]) == 1)
                {
                    managemenu.Visible = true;
                }
                else
                {
                    managemenu.Visible = false;
                }
            }
            else
            {
                managemenu.Visible = false;
            }
            /*-----------------------------------------------------------*/

            othermenulist.Visible = false;

            /*-----------------------------------------------------------*/

            //SSOService.ValidateService validate = new SSOService.ValidateService();
            //SSOService.Registration[] App;
            //App = validate.GetOtherApps(validate.URLEncryption(ConfigurationManager.AppSettings["SSOKey"]));

            DWHServiceReference.DWHServiceHostClient validate = new DWHServiceReference.DWHServiceHostClient();
            DWHServiceReference.Registration[] App;
            App = validate.GetOtherAppsForUser(validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]), Session["UID"].ToString(), "");

            if (App != null)
            {
                if (App.Length != 0)
                {
                    for (int i = 0; i < App.Length; i++)
                    {
                        othermenulist.Visible = true;
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        OtherApps.Controls.Add(li);
                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        anchor.Attributes.Add("href", "../AppRedirect.aspx?Key=" + App[i].AppKey + "&AppName=" + App[i].AppAlias + "");
                        anchor.Attributes.Add("target", "_blank");
                        anchor.InnerText = App[i].AppAlias;
                        li.Controls.Add(anchor);
                    }
                }
            }
        }
        catch
        {
        }
    }
}