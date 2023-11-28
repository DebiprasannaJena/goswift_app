using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class External : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                if (Request.QueryString["id"] == "1")
                {
                    Response.Redirect("http://investodisha.org/eodb/investors-guide");
                    //divCont.InnerHtml = GetServiceDetailsContent("http://investodisha.org/eodb/investors-guide").ToString();
                }
            }
        }
    }

    StringWriter GetServiceDetailsContent(string strURL)
    {
        StringWriter writer = new StringWriter();
        Server.Execute(strURL, writer);
        return writer;
    }
}