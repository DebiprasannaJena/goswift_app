using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_Application : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["fullname"] != null)
        {
            lblUserName.Text = Session["fullname"].ToString();
        }
        else
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }
    }
}
