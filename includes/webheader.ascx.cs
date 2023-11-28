using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Application_includes_header : System.Web.UI.UserControl
{
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            generateDynamicMenu();
            //if (ConfigurationManager.AppSettings["Notice"].ToString() == "On")
            //{
            //    DynamicScrolingText();
            //}
            if (Session["InvestorId"] != null)
            {
                
                //if (Session["UserId"] == null)
                //{


                //    liDashBoardId.Visible = true;
                //    // lblUserName.Text = "Admin";
                //    lidept.Visible = true;
                //    userDetails.Visible = false;
                //}
                //else
                //{
                //    invlogin.Visible = false;
                //    liDashBoardId.Visible = true;
                //    liDashBoardId.Style.Add("display", "block");


                //    //lblUserName.Text = Session["UserId"].ToString();
                //    lblUserName.Text = Session["IndustryName"].ToString();// Session["UserName"].ToString();

                //    lidept.Visible = false;
                //}
            }
            else
            {
                //liDashBoardId.Visible = true;
                //// lblUserName.Text = "Admin";
                //lidept.Visible = true;
                //userDetails.Visible = false;
            }
            string straction = "D";
            DataTable dt = objService.BindDepartment(straction);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    Label lsi = new Label();
                    li.Attributes.Add("class", "plSWClearance");
                  //  uldeparmentid.Controls.Add(li);
                    HtmlGenericControl anchor = new HtmlGenericControl("a");
                    anchor.Attributes.Add("href", "Department.aspx?deptid=" + dt.Rows[i]["intLevelDetailId"] + "");
                   
                    anchor.Attributes.Add("title", "" + dt.Rows[i]["nvchLevelName"] + "");
                    lsi.Text = "" + dt.Rows[i]["nvchLevelName"] + "";
                    anchor.Controls.Add(lsi);
                    li.Controls.Add(anchor);
                }
            }
            else
            {
               // uldeparmentid.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
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
        CmsBusinesslayer objService2 = new CmsBusinesslayer();
        string stractionsub = "FT";
        DataTable tblSubMenu2 = objService2.BindGlinkSubMenuDetails(stractionsub, parentId);
        if (tblSubMenu2.Rows.Count > 0)
        {
            for (int j = 0; j < tblSubMenu2.Rows.Count; j++)
            {
                if (tblSubMenu2.Rows[j]["TotalCount"].ToString() != "0")
                {
                    if (type == 1)
                    {
                        if (tblSubMenu2.Rows[j]["vchURL"].ToString() == "")
                        {
                            if (tblSubMenu2.Rows[j]["intWindowType"].ToString() == "2")
                            {
                                sbMenu.Append("<li class=\" dropdown-submenu\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\" target=\"_blank\"  href=\"" + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
                            }
                            else
                            {
                                sbMenu.Append("<li class=\" dropdown-submenu\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\"  href=\"" + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
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
                                sbMenu.Append("<li class=\" dropdown\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\" target=\"_blank\"  href=\"" + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
                            }
                            else
                            {
                                sbMenu.Append("<li class=\" dropdown\"><a class=\"dropdown-toggle\" data-toggle=\"dropdown\"   href=\"" + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "<span class=\"caret\"></span></a><ul class=\"dropdown-menu\">");
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

                            sbMenu.Append("<li><a target=\"_blank\"  href=\"" + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li>");

                        }
                        else
                        {

                            sbMenu.Append("<li><a  href=\"" + tblSubMenu2.Rows[j]["nvcPageName"] + "?id=" + tblSubMenu2.Rows[j]["intPageId"] + "&lnkid=" + tblSubMenu2.Rows[j]["intLinkId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li>");

                        }
                    }
                    else
                    {
                        if (tblSubMenu2.Rows[j]["intWindowType"].ToString() == "3")///For Modal
                        {
                            sbMenu.Append("<li><a href =\"" + tblSubMenu2.Rows[j]["vchURL"] + "\" data-target=\"#" + tblSubMenu2.Rows[j]["vchModalId"] + "\" data-toggle = 'modal'>" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li> ");
                            divModal.InnerHtml += "<div class='modal fade' id=\"" + tblSubMenu2.Rows[j]["vchModalId"] + "\"><div class='modal-dialog'> <div class='modal-content'></div></div></div>";
                        }
                        else if (tblSubMenu2.Rows[j]["intWindowType"].ToString() == "2")
                        {
                            sbMenu.Append("<li><a target=\"_blank\" href=\"" + tblSubMenu2.Rows[j]["vchURL"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li>");
                        }
                        else
                        {
                            sbMenu.Append("<li><a  href=\"" + tblSubMenu2.Rows[j]["vchURL"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li>");
                        }
                    }
                }
            }
        }

        sbMenu.Append("</ul>");
        return sbMenu.ToString();
    }
    void DynamicScrolingText()
    {
        string strOut = "";
        string dtmFrm = "";
        string dtmTo = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader sqlReader = null;
        try
        {
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_AddNotice";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Action", "S");

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            sqlReader = cmd.ExecuteReader();

            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    strOut = sqlReader["VCH_NOTIFICATION"].ToString();
                    dtmFrm = Convert.ToDateTime(sqlReader["DTM_FROM"].ToString()).ToString("dd-MMM-yyyy");
                    dtmTo = Convert.ToDateTime(sqlReader["DTM_END"].ToString()).ToString("dd-MMM-yyyy");

                }


                string strHtmlText = "<marquee> " + strOut + "</marquee>";

                divScrollingText.Visible = true;
                divScrollingText.InnerHtml = strHtmlText.ToString();

            }

        }
        catch
        {
        }



    }
}