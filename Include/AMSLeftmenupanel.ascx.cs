/*'*******************************************************************************************************************
'' File Name             :   Leftmenupanel.ascx.cs
'' Description           :   To Create Global link and Primary Link Dynamically
'' Created by            :   
'' Created On            :   
'' Modification History  :
''                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
''                              1                       15/10/2013          Mahesh Kumar Nayak          For URL encryption   
''                         
'' Function Name         :   
'' User Defined Namespace:  
*/
using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml.Linq;
public partial class includes_AMSLeftmenupanel : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/LogOut.aspx", false);
            }
            else
            {
                CreateNodesinULLI();
            }

        }

    }


    /// <summary>
    /// Function Created By Biswaranjan on 7-Sept-2010 to Get type of access of a concern plink(ex:Add,veiw,manage)
    /// </summary>
    /// <param name="Action"></param>
    /// <param name="Buttonid"></param>
    /// <param name="TabAccess"></param>

    private void CreateNodesinULLI()
    {


        litMenuStr.Text = string.Empty;
        StringBuilder sb = new StringBuilder();
        
        string strval;
        int indexCnt = 0;
        strval = Session["UserId"].ToString();
        if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
        {
            string strPath = "~/UserXML/" + Session["UserId"].ToString() + ".xml";
            if (!System.IO.File.Exists(Server.MapPath(strPath)))
            {
                return;
            }
            var glinks = from glink in XElement.Load(Server.MapPath(strPath)).Elements("GLinkMaster")
                         select new
                         {
                             glinkName = glink.Element("VCH_GLINK_NAME").Value,
                             glinkId = glink.Element("INT_GLINK_ID").Value
                         };

            foreach (var item in glinks)
            {
                string glink = item.glinkId;
                sb.Append("<div class=\"glossymenu\">");
                //For display GLINK
                string iconName = item.glinkId + ".gif"; //"Business.png";            
                string glinkName = item.glinkName;
                string altText = item.glinkName;

                //if (glinkName == "Dashboard")
                //{
                //    ul = "../Dashboard/dashboard.aspx?linkm=G" + glink + "&linkn=1&ranNum=" + Session["RandomNo"];
                //    sb.Append(string.Format("<li id=\"{1}\"> <a href=\"{2}\"> <i class=\"menu-icon fa fa-tachometer\"></i> <span class=\"menu-text\"> {0}</span> </a><b class=\"arrow\"></b>", glinkName, "G" + glink, ul));
                //    sb.Append(string.Format("<a href=\"#\" class=\"menuitem submenuheader th_menuitembg\"><Img alt=\"{0}\" src=\"../img/{1}\" border=\"0\" align=\"absmiddle\"> {2} </a>", altText, iconName, glinkName));
                //}
                //else
                //{
                //    //string str = strIcon[int.Parse(glink) - 1];
                //    sb.Append(string.Format("<li class=\"hsub\" id=\"{1}\"> <a href=\"#\" class=\"dropdown-toggle\"> <i class=\"{2}\"></i> <span class=\"menu-text\"> {0}</span> <b class=\"arrow fa fa-angle-down\"></b> </a> <b class=\"arrow\"></b>", glinkName, "G" + glink, strIcon[int.Parse(glink) - 1]));
                //}

                sb.Append(string.Format("<a href=\"#\" class=\"menuitem submenuheader th_menuitembg\"><Img alt=\"{0}\" src=\"../img/{1}\" border=\"0\" align=\"absmiddle\"> {2} </a>", altText, iconName, glinkName));

                #region PLINK
                var plinks = (from plink in XElement.Load(Server.MapPath(strPath)).Elements("PLinkMaster")
                              where plink.Element("INT_GLINK_ID").Value == glink
                              select new
                              {
                                  plinkName = plink.Element("VCH_PLINK_NAME").Value,
                                  plinkId = plink.Element("INT_PLINK_ID").Value,
                                  pFileName = plink.Element("VCH_FILE_NAME").Value,
                                  btnId = plink.Element("INT_LNKBTN_ID").Value,
                                  tabId = plink.Element("INT_LNKTAB_ID").Value,
                                  functionid = plink.Element("INT_FUNCTION_ID").Value,
                                  slno = plink.Element("INT_SLNO").Value
                              } into pl
                              orderby int.Parse(pl.slno) ascending
                              select pl).ToList();
                #region Check IF PLINK is present the add a DIV tag of call submenu

                sb.Append(string.Format("<div class=\"submenu\"><ul>"));

                #endregion
                #region Add List Items
                string url;
                string strchek = string.Empty;

                foreach (var pitem in plinks)
                {
                    strchek = string.Empty;

                    if (pitem.functionid != "0")
                    {
                        url = "../" + pitem.pFileName + "?linkm=" + item.glinkId + "&linkn=" + pitem.plinkId + "&btn=" + pitem.btnId + "&tab=" + pitem.tabId ;
                    }
                    else
                    {
                        strchek = "target=\"_balnk\"";
                        url = pitem.pFileName;
                    }
                    string name = pitem.plinkName;
                    sb.Append(string.Format("<li><a id=\"{3}\" href=\"{0}\" {2} >{1} </a></li>", url, name, strchek, pitem.plinkId));
                    
                }
                #endregion
                #region If PLINK is present  then close the DIV tag
                sb.Append(string.Format("</ul></div>"));

                #endregion
                #endregion

                sb.Append("</div>");

                indexCnt += 1;

            }
        }

        litMenuStr.Text = sb.ToString();

    }





}





