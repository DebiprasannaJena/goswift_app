using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class includes_investorheader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)//UserName
        {

            userDetails.Visible = false;
           
            lblUserName.Text = "Admin";
            Response.Redirect("../Default.aspx");
        }
        else
        {          
            userDetails.Visible = true;
            userDetails.Style.Add("display", "block");
            lblUserName.Text = Convert.ToString(Session["IndustryName"]);

        }
    }
}