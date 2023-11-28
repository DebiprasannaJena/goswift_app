using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class incentives_IncentiveWizard : System.Web.UI.Page
{
    static int v = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        v = v + 1;
        if (v == 0)
        {
            dvSection.Visible = false;
            dvHeader.Visible = false;
            Button1.Visible = false;
            dvSection4.Visible = false;
            dvHeader4.Visible = false;
            Button2.Visible = true;
        }
        if (v == 1)
        {
            dvSection.Visible = false;
            dvHeader.Visible = false;
            dvSection1.Visible = true;
            dvHeader1.Visible = true;
            dvSection2.Visible = false;
            dvHeader2.Visible = false;
            dvSection3.Visible = false;
            dvHeader3.Visible = false;
            Button1.Visible = true;
            dvSection4.Visible = false;
            dvHeader4.Visible = false;
            Button2.Visible = true;
            
        }
        if (v == 2)
        {
            dvSection.Visible = false;
            dvHeader.Visible = false;
            dvSection1.Visible = false;
            dvHeader1.Visible = false;
            dvSection2.Visible = true;
            dvHeader2.Visible = true;
            dvSection3.Visible = false;
            dvHeader3.Visible = false;
            Button1.Visible = true;
            dvSection4.Visible = false;
            dvHeader4.Visible = false;
            Button2.Visible = true;
        }
        if (v == 3)
        {
            dvSection3.Visible = true;
            dvHeader3.Visible = true;
            dvSection.Visible = false;
            dvHeader.Visible = false;
            dvSection1.Visible = false;
            dvHeader1.Visible = false;
            dvSection2.Visible = false;
            dvHeader2.Visible = false;
            dvSection4.Visible = false;
            dvHeader4.Visible = false;
            Button2.Visible = true;
        }
        if (v == 4)
        {
            dvSection4.Visible = true;
            dvHeader4.Visible = true;
            dvSection3.Visible = false;
            dvHeader3.Visible = false;
            dvSection.Visible = false;
            dvHeader.Visible = false;
            dvSection1.Visible = false;
            dvHeader1.Visible = false;
            dvSection2.Visible = false;
            dvHeader2.Visible = false;
            Button2.Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        v = v - 1;
        if (v == 0)
        {
            dvSection.Visible = true;
            dvHeader.Visible = true;
            Button1.Visible = false;
            Button2.Visible = true;
            dvSection1.Visible = false;
            dvHeader1.Visible = false;
            dvSection2.Visible = false;
            dvHeader2.Visible = false;
            dvSection3.Visible = false;
            dvHeader3.Visible = false;
            dvSection4.Visible = false;
            dvHeader4.Visible = false;
        }
        if (v == 1)
        {
            dvSection.Visible = false;
            dvHeader.Visible = false;
            dvSection1.Visible = true;
            dvHeader1.Visible = true;
            dvSection2.Visible = false;
            dvHeader2.Visible = false;
            dvSection3.Visible = false;
            dvHeader3.Visible = false;
            Button1.Visible = true;
            dvSection4.Visible = false;
            dvHeader4.Visible = false;
            Button2.Visible = true;
           
        }
        if (v == 2)
        {
            dvSection.Visible = false;
            dvHeader.Visible = false;
            dvSection1.Visible = false;
            dvHeader1.Visible = false;
            dvSection2.Visible = true;
            dvHeader2.Visible = true;
            dvSection3.Visible = false;
            dvHeader3.Visible = false;
            Button1.Visible = true;
            dvSection4.Visible = false;
            dvHeader4.Visible = false;
            Button2.Visible = true;
        }
        if (v == 3)
        {
            dvSection3.Visible = true;
            dvHeader3.Visible = true;
            dvSection.Visible = false;
            dvHeader.Visible = false;
            dvSection1.Visible = false;
            dvHeader1.Visible = false;
            dvSection2.Visible = false;
            dvHeader2.Visible = false;
            dvSection4.Visible = false;
            dvHeader4.Visible = false;
            Button2.Visible = true;
        }
        if (v == 4)
        {
            dvSection4.Visible = true;
            dvHeader4.Visible = true;
            dvSection3.Visible = false;
            dvHeader3.Visible = false;
            dvSection.Visible = false;
            dvHeader.Visible = false;
            dvSection1.Visible = false;
            dvHeader1.Visible = false;
            dvSection2.Visible = false;
            dvHeader2.Visible = false;
            Button2.Visible = false;
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("IncentiveDet.aspx");
    }
}