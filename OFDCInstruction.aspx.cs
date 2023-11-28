using BusinessLogicLayer.Proposal;
using DWHServiceReference;
using EntityLayer.Proposal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OFDCInstruction: System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }

    protected void btnnavigate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OFDCDetails.aspx");
    }
}