using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllRequirement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("EncloserList.aspx");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("ProposedEmployment.aspx");
        Response.Redirect("ManPowerDetails.aspx");
    }
}