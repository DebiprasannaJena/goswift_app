using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Data;
using System.Globalization;
using BusinessLogicLayer.Common;
using EntityLayer.Common;
using System.Web.Services;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Text;
using System.Data.SqlClient;
public partial class website_Default : System.Web.UI.Page
{
    CmsBusinesslayer objService2 = new CmsBusinesslayer();
    CommonBusinessLayer objService = new CommonBusinessLayer();
    CmsBusinesslayer objService1 = new CmsBusinesslayer();
    CMSDetails objCms = new CMSDetails();
    Feedback objServiceEntity = new Feedback();
    Gallery objServiceGallery = new Gallery();
    CmsBusinesslayer objServif = new CmsBusinesslayer();
    string str_Retvalue = "";
    string strprojname = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillYear();
            generateDynamicMenu();
            FillContent();
            //BindAnnoncement();
            //BindNews();
            //BindNotification();
            ViewProposalStatus();
            BindRepeater();
            BindUsefulLink();
            if (ConfigurationManager.AppSettings["Notice"].ToString() == "On")
            {
                DynamicScrolingText();
            }
        }

        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string s = "Browser Capabilities\n"
            + "Type = " + browser.Type + "\n"
            + "Name = " + browser.Browser + "\n"
            + "Version = " + browser.Version + "\n"
            + "Major Version = " + browser.MajorVersion + "\n"
            + "Minor Version = " + browser.MinorVersion + "\n"
            + "Platform = " + browser.Platform + "\n"
            + "Is Beta = " + browser.Beta + "\n"
            + "Is Crawler = " + browser.Crawler + "\n"
            + "Is AOL = " + browser.AOL + "\n"
            + "Is Win16 = " + browser.Win16 + "\n"
            + "Is Win32 = " + browser.Win32 + "\n"
            + "Supports Frames = " + browser.Frames + "\n"
            + "Supports Tables = " + browser.Tables + "\n"
            + "Supports Cookies = " + browser.Cookies + "\n"
            + "Supports VBScript = " + browser.VBScript + "\n"
            + "Supports JavaScript = " + browser.EcmaScriptVersion.ToString() + "\n"
            + "Supports Java Applets = " + browser.JavaApplets + "\n"
            + "Supports ActiveX Controls = " + browser.ActiveXControls + "\n"
            + "Supports JavaScript Version = " + browser["JavaScriptVersion"] + "\n";

        Session["brs"] = s;
    }

    void DynamicScrolingText()
    {
        int? DEFAULT_PAGE =null ;
        string strOut = "";
      
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
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

            SqlDataReader sqlReader = cmd.ExecuteReader();

            if (sqlReader.HasRows)
            {
                while (sqlReader.Read())
                {
                    strOut = sqlReader["VCH_NOTIFICATION"].ToString();
                    DEFAULT_PAGE =Convert.ToInt32(sqlReader["INT_DEFAULT_PAGE"].ToString());
                }

                if(DEFAULT_PAGE == 0)
                {
                    string strHtmlText = "<marquee onmouseover='this.stop()' onmouseout='this.start()'> " + strOut + "</marquee>";

                    divScrollingText.Visible = true;
                    divScrollingText.InnerHtml = strHtmlText.ToString();
                }
                
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "CMS");
        }
    }

    StringBuilder usefulLink = new StringBuilder();
    private void BindUsefulLink()
    {
        try
        {
            string stractionsub = "E";
            DataTable tblUseful = objServif.BindUsefulLinkDetails(stractionsub);
            if (tblUseful.Rows.Count > 0)
            {
                usefulLink.Append("<h2>USEFUL LINKS</h2>");
                for (int j = 0; j < tblUseful.Rows.Count; j++)
                {
                    usefulLink.Append(" <a   class=\"imglinksdet\" href=\"" + tblUseful.Rows[j]["vchURL"] + "\" target=\"_blank\"><img  src=https://investodisha.gov.in/goswift/UseFulLink/" + tblUseful.Rows[j]["vchImgeURL"] + "></a>");
                }
            }
            dvUlink.InnerHtml = usefulLink.ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "CMS");
        }
    }
    private void FillContent()
    {
        try
        {
            int intmenuid = 1;
            string straction = "HC";
            DataTable dt = objService1.GetHeadContent(straction, intmenuid);
            if (dt.Rows.Count > 0)
            {
                string sValueTobeShowninDiv = dt.Rows[0]["vchContent"].ToString();
                divabout.InnerHtml = sValueTobeShowninDiv;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "CMS");
        }
    }
    //private void BindAnnoncement()
    //{
    //    try
    //    {
    //        string strtype = "Annoncement";
    //        string straction = "BNA";
    //        DataTable dt = objService1.BindNewsEventData(straction, strtype);
    //        if (dt.Rows.Count > 0)
    //        {
    //            //string sValueTobeShowninDiv = dt.Rows[0]["vchContent"].ToString();
    //            //divabout.InnerHtml = sValueTobeShowninDiv;
    //            RepAnnouncement.DataSource = dt;
    //            RepAnnouncement.DataBind();
    //        }
    //        else
    //        {
    //            RepAnnouncement.DataSource = null;
    //            RepAnnouncement.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "CMS");
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
    //    }
    //}
    //private void BindNews()
    //{
    //    try
    //    {
    //        string strtype = "News";
    //        string straction = "BN";
    //        DataTable dtnews = objService1.BindNewsEventData(straction, strtype);
    //        if (dtnews.Rows.Count > 0)
    //        {
    //            //string sValueTobeShowninDiv = dt.Rows[0]["vchContent"].ToString();
    //            //divabout.InnerHtml = sValueTobeShowninDiv;
    //            RepNews.DataSource = dtnews;
    //            RepNews.DataBind();
    //        }
    //        else
    //        {
    //            RepNews.DataSource = null;
    //            RepNews.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "CMS");
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
    //    }
    //}
    //private void BindNotification()
    //{
    //    try
    //    {
    //        string strtype = "Notification";
    //        string straction = "BNO";
    //        DataTable dtnotification = objService1.BindNewsEventData(straction, strtype);
    //        if (dtnotification.Rows.Count > 0)
    //        {
    //            //string sValueTobeShowninDiv = dt.Rows[0]["vchContent"].ToString();
    //            //divabout.InnerHtml = sValueTobeShowninDiv;
    //           RepNotification.DataSource = dtnotification;
    //            RepNotification.DataBind();
    //        }
    //        else
    //        {
    //            RepNotification.DataSource = null;
    //            RepNotification.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "CMS");
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
    //    }
    //}

    private void ViewProposalStatus()
    {
        objService1 = new CmsBusinesslayer();
        try
        {
            objCms.StrAction = "PC";
            objCms.Received = drpYear.SelectedValue;
            List<CMSDetails> objPEALStatusList = objService1.GetWebsiteProposalDetails(objCms).ToList();
            if (objPEALStatusList.Count > 0)
            {
                hdApplRec.InnerHtml = Formatstring(Math.Round(Convert.ToDecimal(objPEALStatusList[0].Received.ToString()), 0).ToString());
                if (hdApplRec.InnerHtml == "")
                {
                    hdApplRec.InnerHtml = "0";
                }
                hdApproveProp.InnerHtml = Formatstring(Math.Round(Convert.ToDecimal(objPEALStatusList[0].Approved.ToString()), 0).ToString());
                if (hdApproveProp.InnerHtml == "")
                {
                    hdApproveProp.InnerHtml = "0";
                }


                //decimal TotInvest = Convert.ToDecimal(objPEALStatusList[0].TotCapital);
                //if (TotInvest > Convert.ToDecimal(10000000))
                //{
                //    double x = Convert.ToDouble(objPEALStatusList[0].TotCapital.ToString());
                //    decimal TotInvestment = Convert.ToDecimal(x/100);
                hdPropInv.InnerHtml = Formatstring(Math.Round(Convert.ToDecimal(objPEALStatusList[0].TotCapital.ToString()), 0).ToString());
                lblAmount.Text = "Cr.";
                if (hdPropInv.InnerHtml == "")
                {
                    hdPropInv.InnerHtml = "0";
                }
                //}
                //else
                //{
                //    hdPropInv.InnerHtml = TotInvest.ToString();
                //    lblAmount.Text = "Lakh";
                //}
                hdPropEmp.InnerHtml = Formatstring(objPEALStatusList[0].TotEmpProp.ToString());
                if (hdPropEmp.InnerHtml == "")
                {
                    hdPropEmp.InnerHtml = "0";
                }
            }
            else
            {
                hdApplRec.InnerHtml = "0";
                hdPropInv.InnerHtml = "0";
                hdApproveProp.InnerHtml = "0";
                hdPropEmp.InnerHtml = "0";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "CMS");
        }
        finally
        {
            objCms = null;
        }
    }

    private string Formatstring(string strvalue)
    {
        CultureInfo CInfo = new CultureInfo("hi-IN");
        strvalue = Convert.ToInt64(strvalue).ToString("#,#", CInfo);
        return strvalue;
    }
    private void BindRepeater()
    {
        List<Gallery> objList = new List<Gallery>();
        try
        {
            objServiceGallery.strAction = "VH";
            objList = objService.ViewGallery(objServiceGallery).ToList();
            Repeater1.DataSource = objList;
            Repeater1.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "CMS");
        }
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Image img = (Image)e.Item.FindControl("Image1");
        HiddenField hid1 = (HiddenField)e.Item.FindControl("hid1");
        img.ImageUrl = "~/Portal/ImageGallery/" + hid1.Value;
    }
    public void Clear()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
        txtMobileNumber.Text = "";
        txtSubject.Text = "";
        txtFeedback.Text = "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
            Boolean bt = Captcha1.UserValidated;
            if (bt == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Invalid Captcha !', '" + strprojname + "'); </script>", false);
                txtCaptcha.Text = "";
                txtCaptcha.Focus();
                return;
            }
            else
            {
                objServiceEntity.strAction = "A";
                objServiceEntity.vchFirstName = txtFirstName.Text;
                objServiceEntity.vchLastName = txtLastName.Text;
                objServiceEntity.vchEmail = txtEmail.Text;
                objServiceEntity.vchMobileNo = txtMobileNumber.Text;
                objServiceEntity.vchSubject = txtSubject.Text;
                objServiceEntity.vchFeedback = txtFeedback.Text;
                objServiceEntity.intCreatedBy = 1;
                str_Retvalue = objService.ManageFeedback(objServiceEntity);
                if (str_Retvalue == "1")
                {

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Feedback Submitted Successfully !');window.location='Default.aspx'", true);
                    Clear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Feedback Submitted Successfully !');", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "CMS");
        }

    }
    protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewProposalStatus();
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
   //int childCount = 0;
    public string getMenuItems(int parentId, int type)
    {
        try
        {
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
                            if (tblSubMenu2.Rows[j]["intWindowType"].ToString() == "3")////For Modal Pouup
                            {
                                //sbMenu.Append("<li><a href=\"" + tblSubMenu2.Rows[j]["vchURL"] + "\" data-toggle=\"modal\" data-target=\"#" + tblSubMenu2.Rows[j]["vchModalId"] + "\"> <img src='images/nsws-logo.png'> </a></li>");
                                
                                //sbMenu.Append("<li><a href=\"" + tblSubMenu2.Rows[j]["vchURL"] + "\" data-toggle=\"modal\" data-target=\"#" + tblSubMenu2.Rows[j]["vchModalId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li>");
                                //divModal.InnerHtml += "<div class='modal fade' id=\"" + tblSubMenu2.Rows[j]["vchModalId"] + "\" ><div class='modal-dialog lg'> <div class='modal-content'></div></div></div>";

                                sbMenu.Append("<li><a href=\"" + tblSubMenu2.Rows[j]["vchURL"] + "\" data-toggle=\"modal\" data-target=\"#" + tblSubMenu2.Rows[j]["vchModalId"] + "\">" + tblSubMenu2.Rows[j]["vchPlink"] + "</a></li>");
                                divModal.InnerHtml += "<div class='modal fade' id=\"" + tblSubMenu2.Rows[j]["vchModalId"] + "\" ><div class='modal-dialog lg'> <div class='modal-content'></div></div></div>";

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
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "CMS");
        }

        return sbMenu.ToString();
    }

    /// <summary>
    /// Added by Sushant Jena On Dt:- 06-Jan-2022
    /// To fill financial year dropdown.
    /// </summary>
    private void FillYear()
    {
        try
        {
            drpYear.Items.Clear();
            int datediff = DateTime.Now.Year - 2015;
            for (int i = datediff; i >= 0; i--)
            {
                string TextFY = Convert.ToString(DateTime.Now.Year - i) + "-" + Convert.ToString((DateTime.Now.Year - i) + 1);
                string ValueYr = Convert.ToString(DateTime.Now.Year - i);

                ListItem list1 = new ListItem
                {
                    Text = TextFY,
                    Value = ValueYr
                };

                drpYear.Items.Add(list1);
            }

            ListItem list = new ListItem
            {
                Text = "Since 2015",
                Value = "0"
            };
            drpYear.Items.Insert(0, list);

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "CMS");
        }
    }
}