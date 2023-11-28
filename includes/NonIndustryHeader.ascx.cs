using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class includes_NonIndustryHeader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["UserId"]) != null && Convert.ToString(Session["UserId"]) != "")//UserName
            {
                LblUserName.Text = Session["IndustryName"].ToString();
            }
            else
            {
                LblUserName.Text = "Admin";
            }
        }
    }
}