using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Linq;

public partial class includes_IncludeScript : System.Web.UI.UserControl
{
    #region "Member Variables"
    public static string linkm, linkn, strPL, strGL;
    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                linkm = string.IsNullOrEmpty(Request.QueryString["linkm"]) ? "1" : Request.QueryString["linkm"];
                linkn = string.IsNullOrEmpty(Request.QueryString["linkn"]) ? "0" : Request.QueryString["linkn"];;

                string strPath = "~/UserXML/" + Session["UserId"].ToString() + ".xml";
                if (!System.IO.File.Exists(Server.MapPath(strPath)))
                {
                    return;
                }
                XElement menuXml = XElement.Load(Server.MapPath(strPath));
                var glinks = (from glink in menuXml.Elements("GLinkMaster")
                              where glink.Element("INT_GLINK_ID").Value == linkm
                              select glink.Element("VCH_GLINK_NAME").Value
                             ).FirstOrDefault();

                var plinks = (from plink in menuXml.Elements("PLinkMaster")
                              where plink.Element("INT_PLINK_ID").Value == linkn.TrimStart('P')
                              select plink.Element("VCH_PLINK_NAME").Value
                              ).FirstOrDefault();

                strPL = Convert.ToString(plinks);
                strGL = Convert.ToString(glinks);

            }
            catch (Exception)
            {

                Response.Redirect("~/CustomError.aspx");
            }
        }
    }

    #endregion
}