using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class ckeditor_ImageBrowser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strServerName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
        string strVpath = HttpRuntime.AppDomainAppVirtualPath;
        string exDir = "..\\CKImages";
        exLabel.Text = "";
        foreach (string exFile in Directory.GetFiles(Server.MapPath(exDir)))
        {
            object[] ArrFile = exFile.Split('\\');
            string file = "CKImages/" + ArrFile[ArrFile.Length - 1].ToString();
         // string file = "\\CKImages\\" + ArrFile[ArrFile.Length - 1].ToString();
            string file1 = "../ckeditor/CKImages/" + ArrFile[ArrFile.Length - 1].ToString();
            string fileName = ArrFile[ArrFile.Length - 1].ToString();
         // exLabel.Text += @"<a href='" + file + "'> " + fileName + " </a><br/>";
            exLabel.Text += @"<a href='" + file1 + "'   height='100' width='105' > <img alt='' src='../" + file + "'  height='100' width='105'  /> <br/>" + fileName + "</a>";
        }
        CKEditorFuncNum.Value = Convert.ToString(Request.QueryString["CKEditorFuncNum"]);
    }
}
