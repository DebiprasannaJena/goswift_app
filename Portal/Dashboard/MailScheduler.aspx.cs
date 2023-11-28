using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data;
using System.Text;
using BusinessLogicLayer.Dashboard;
using System.Net;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class Portal_Dashboard_MailScheduler : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Lbl_Msg.Text = "";
    }

    protected void Btn_Send_Mail_Click(object sender, EventArgs e)
    {
        string userid = Session["userid"].ToString();

        MailMasterTracker c1 = new MailMasterTracker();
        string retData = c1.mailSchedule_Test(userid);

        PlaceHolder1.Controls.Add(new Literal { Text = retData.ToString() });
    }

    private string CreateDynamicTable(DataTable dt)
    {
        //Building an HTML string.
        StringBuilder html = new StringBuilder();

        html.Append("Dear Mr. XXX " + "<br />" + "This is for your information " + "<br />");


        //Table start.
        html.Append("<table border = '1' cellpadding='0' cellspacing='0' width='60%'>");

        //Building the Header row.
        html.Append("<tr bgcolor='#42f4f1'>");
        foreach (DataColumn column in dt.Columns)
        {
            html.Append("<th>");
            html.Append(column.ColumnName);
            html.Append("</th>");
        }
        html.Append("</tr>");

        //Building the Data rows.
        foreach (DataRow row in dt.Rows)
        {
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<td>");
                html.Append(row[column.ColumnName]);
                html.Append("</td>");
            }
            html.Append("</tr>");
        }

        //Table end.
        html.Append("</table>");

        //Append the HTML string to Placeholder.
        PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });

        return html.ToString();
    }
}