using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class includes_header : System.Web.UI.UserControl
{
    public bool isboolAdmin = true;
    #region "Page Load event"
    protected void Page_Load(object sender, EventArgs e)
    {
        //Code to redirect to the customised session out page when the session expires
        //Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.Cache.SetNoStore();
        string SessionValue = Session["UserId"] as string;       
        if (String.IsNullOrEmpty(SessionValue))
        {
            Response.Redirect("../Logout.aspx");
        }
        else
        {
          
            if (!Page.IsPostBack)
            {
                Session["UsrName"] = Session["userName"];
                lblUserName.Text = Session["UsrName"].ToString();
                if (lblUserName.Text == "")
                {
                    lblUserName.Visible = false;
                }
                else
                {
                    lblUserName.Visible = true;
                    lblUserName.Text = Session["fullName"].ToString();
                }
            }
        }
    }
    #endregion
}