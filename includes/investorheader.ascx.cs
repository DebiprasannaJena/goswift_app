using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class includes_investorheader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["UserId"]) != null && Convert.ToString(Session["UserId"]) != "")//UserName
        {
            LblUserName.Text = Session["IndustryName"].ToString();
            if (ConfigurationManager.AppSettings["DirectLogin"].ToString() == "Off")
            {
                if (Session["LogId"] != null)
                {
                    if (Session["LogId"].ToString() != "NA")
                    {
                        DWHServiceReference.DWHServiceHostClient validate = new DWHServiceReference.DWHServiceHostClient();
                        string ExistLog = validate.CheckLog("C", validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]), Convert.ToInt32(Session["LogId"]));
                        if (ExistLog == "0")
                        {
                            Response.Redirect("~/LogOut.aspx");
                        }
                    }
                }
            }
            
                
        }
        else
        {
            LblUserName.Text = "Admin";
        }
    }
}