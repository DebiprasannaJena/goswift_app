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
using System.Collections.Generic;
using System.IO;

public partial class LinkBrowser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strFileType = string.Empty;
        string strFileTypeImage = string.Empty;
        string strServerName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];        
        string strVpath = HttpRuntime.AppDomainAppVirtualPath;
        string exDir = "..\\CKFiles";
        exLabel.Text = "";

        foreach (string exFile in Directory.GetFiles(Server.MapPath( exDir)))
        {
            object[] ArrFile =exFile.Split('\\');
           // string file = strServerName + strVpath + "\\CKFiles\\" + ArrFile[ArrFile.Length - 1].ToString();
            string file = "../ckeditor/CKFiles/" + ArrFile[ArrFile.Length - 1].ToString();
            string fileName=ArrFile[ArrFile.Length - 1].ToString();
            object[] ArrFileType =fileName.Split('.');
            strFileType = ArrFileType[ArrFileType.Length - 1].ToString();

            if (strFileType.Trim()=="doc"  )
            {
                strFileTypeImage = " <img alt='' src='images/doc.png'   height='16' width='16'  />";
            }
            if (strFileType.Trim()=="xls" || strFileType.Trim()==".xlsx" )
            {
                strFileTypeImage = " <img alt='' src='images/xls.png'  height='16' width='16'  />";
            }
            if (strFileType.Trim()=="pdf"  )
            {
                strFileTypeImage = " <img alt='' src='images/pdf.png'  height='16' width='16'  /> ";
            }
            if (strFileType.Trim()=="txt"  )
            {
                strFileTypeImage = " <img alt='' src='images/txt.png'  height='16' width='16'/> ";
            }
           if (strFileType.Trim()=="ppt"  )
            {
                strFileTypeImage = " <img alt='' src='images/ppt.png'  height='16' width='16'/> ";
            }
            exLabel.Text += @" "+ strFileTypeImage + "<a href='" + file + "'> " + fileName + " </a><br/>";
        }
        CKEditorFuncNum.Value = Convert.ToString(Request.QueryString["CKEditorFuncNum"]);

      


    }
}
