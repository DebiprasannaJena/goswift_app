using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SSOlogout : SessionCheck
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Util.SessionCheck();
        if (!string.IsNullOrEmpty(Session["IndustryName"] as string))
        {
            lblIndustry.Text = Session["IndustryName"].ToString();
        }

    }
}