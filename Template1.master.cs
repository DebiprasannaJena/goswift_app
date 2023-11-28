using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Text;

public partial class Template1 : System.Web.UI.MasterPage
{
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CmsBusinesslayer objService2 = new CmsBusinesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            generateDynamicMenu();
            dynamicHeaderMenu();

            dynamicFooterMenu();
    
        }
    }
    public void generateDynamicMenu()
    {
        StringBuilder sbMenu = new StringBuilder();
        sbMenu.Append(" <ul class=\"nav navbar-nav nav-links\" >");
        string childItems = getMenuItems(0, 0);
        sbMenu.Append(childItems);
        sbMenu.Append(" </ul>");
        myNavbar.InnerHtml = sbMenu.ToString();
    }

    StringBuilder sbMenu = new StringBuilder();
    int childCount = 0;
    public string getMenuItems(int parentId, int type)
    {
        string stractionsub = "FT";
        DataTable tblSubMenu2 = objService.BindGlinkSubMenuDetails(stractionsub, parentId);
        if (tblSubMenu2.Rows.Count > 0)
        {
            for (int j = 0; j < tblSubMenu2.Rows.Count; j++)
            {

                if (tblSubMenu2.Rows[j]["nvcPageName"].ToString() == "Default.aspx")
                {


                }
                if (tblSubMenu2.Rows[j]["TotalCount"].ToString() != "0")
                {
                    if (type == 1)
                    {
                        if (tblSubMenu2.Rows[j]["vchURL"].ToString() == "")
                        {
                            if (tblSubMenu2.Rows[j]["intWindowType"].ToString() == "2")
                            {
                                sbMenu.Append("<li class=\" dropdown-submenu\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\" target=\"_blank\"  href=\"" + System.Configuration.ConfigurationManager.AppSettings["AppUrl"].ToString()+tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
                            }
                            else
                            {
                                sbMenu.Append("<li class=\" dropdown-submenu\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\"  href=\"" + System.Configuration.ConfigurationManager.AppSettings["AppUrl"].ToString() + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
                            }
                        }
                        else
                        {
                            if (tblSubMenu2.Rows[j]["intWindowType"].ToString() == "2")
                            {
                                sbMenu.Append("<li class=\" dropdown-submenu\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\" target=\"_blank\" href=\"" + tblSubMenu2.Rows[j]["vchURL"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
                            }
                            else
                            {
                                sbMenu.Append("<li class=\" dropdown-submenu\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\"  href=\"" + tblSubMenu2.Rows[j]["vchURL"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
                            }
                        }

                    }
                    else
                    {
                        if (tblSubMenu2.Rows[j]["vchURL"].ToString() == "")
                        {
                            if (tblSubMenu2.Rows[j]["intWindowType"].ToString() == "2")
                            {
                                sbMenu.Append("<li class=\" dropdown\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\" target=\"_blank\"  href=\"" + System.Configuration.ConfigurationManager.AppSettings["AppUrl"].ToString() + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
                            }
                            else
                            {
                                sbMenu.Append("<li class=\" dropdown\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\"   href=\"" + System.Configuration.ConfigurationManager.AppSettings["AppUrl"].ToString() + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
                            }
                        }
                        else
                        {
                            if (tblSubMenu2.Rows[j]["intWindowType"].ToString() == "2")
                            {
                                sbMenu.Append("<li class=\" dropdown\"><a class=\"dropdown-toggle\" target=\"_blank\" data-toggle=\"dropdown\"  href=\"" + tblSubMenu2.Rows[j]["vchURL"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
                            }
                            else
                            {
                                sbMenu.Append("<li class=\" dropdown\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\"  href=\"" + tblSubMenu2.Rows[j]["vchURL"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
                            }
                        }
                    }
                    getMenuItems(Convert.ToInt32(tblSubMenu2.Rows[j]["intLinkId"].ToString()), 1);
                }
                else
                {
                    if (tblSubMenu2.Rows[j]["vchURL"].ToString() == "")
                    {
                        if (tblSubMenu2.Rows[j]["intWindowType"].ToString() == "2")
                        {

                            sbMenu.Append("<li><a target=\"_blank\"  href=\"" + System.Configuration.ConfigurationManager.AppSettings["AppUrl"].ToString()  + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li>");
                           
                        }
                        else
                        {

                            sbMenu.Append("<li><a  href=\"" + System.Configuration.ConfigurationManager.AppSettings["AppUrl"].ToString() + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li>");
                            
                        }
                    }
                    else
                    {
                        if (tblSubMenu2.Rows[j]["intWindowType"].ToString() == "2")
                        {
                            sbMenu.Append("<li><a target=\"_blank\" href=\"" + tblSubMenu2.Rows[j]["vchURL"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li>");
                        }
                        else
                        {
                            sbMenu.Append("<li><a  href=\"" + System.Configuration.ConfigurationManager.AppSettings["AppUrl"].ToString() + tblSubMenu2.Rows[j]["vchURL"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li>");
                        }
                    }
                }
            }
        }

        sbMenu.Append("</ul>");
        return sbMenu.ToString();
    }
    public void dynamicHeaderMenu()
    {
        string strHtml = "<div class='row'> <div class='helpline'> <i class='fa fa-phone'></i><h3>Toll Free Helpline - <span>1800 345 7157</span><small>(Timing 10.00 AM to 6.00 PM on working days)</small></h3> <h4><i class='fa fa-envelope'></i>support[dot]investodisha[at]nic[dot]in</h4> </div><ul><li><a class='scrdr' href='http://www.nvda-project.org/' target='_blank'><i class='fa fa-fax'></i></a></li><li><a href='#' class='font-plus'>T+</a></li><li><a href='#' class='font-normal active'>T</a></li> <li><a href='#' class='font-minus'>T-</a></li>";
        string stractionsub = "H";
        CMSDetails obj = new CMSDetails();
        obj.actioncode = "H";
        List<CMSDetails> tblHeader = objService2.Dynamicheaderfooterview(obj);
        if (tblHeader.Count > 0)
        {
            for (int j = 0; j < tblHeader.Count; j++)
            {
                strHtml = strHtml + "<li><a  target=\"_blank\" href=" + tblHeader[j].vchURL.ToString() + ">" + tblHeader[j].StrMenuName + "</a></li>";
            }

        }
        strHtml = strHtml + "</ul>  </div>";
        HdrMenus.InnerHtml = strHtml.ToString();
    }
    public void dynamicFooterMenu()
    {
        string strHtml = " <ul>";

        CMSDetails obj = new CMSDetails();
        obj.actioncode = "F";
        List<CMSDetails> tblHeader = objService2.Dynamicheaderfooterview(obj);
        if (tblHeader.Count > 0)
        {
            for (int j = 0; j < tblHeader.Count; j++)
            {
                strHtml = strHtml + "<li><a target=\"_blank\" href=" + tblHeader[j].vchURL.ToString() + ">" + tblHeader[j].StrMenuName + "</a></li>";
            }

        }
        strHtml = strHtml + "</ul>  </div>";
        divFooterId.InnerHtml = strHtml.ToString();
    }
}
