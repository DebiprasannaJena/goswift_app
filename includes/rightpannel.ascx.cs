using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Data;
using System.Web.UI.HtmlControls;
public partial class Application_includes_header : System.Web.UI.UserControl
{
    CmsBusinesslayer objService = new CmsBusinesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindAnnoncement();
        BindNews();
        
    }
    private void BindAnnoncement()
    {
        try
        {
            string strtype = "Annoncement";
            string straction = "BNA";
            DataTable dt = objService.BindNewsEventData(straction, strtype);
            if (dt.Rows.Count > 0)
            {
                //string sValueTobeShowninDiv = dt.Rows[0]["vchContent"].ToString();
                //divabout.InnerHtml = sValueTobeShowninDiv;
                RepAnnouncement.DataSource = dt;
                RepAnnouncement.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    private void BindNews()
    {
        try
        {
            string strtype = "News";
            string straction = "BN";
            DataTable dtnews = objService.BindNewsEventData(straction, strtype);
            if (dtnews.Rows.Count > 0)
            {
                //string sValueTobeShowninDiv = dt.Rows[0]["vchContent"].ToString();
                //divabout.InnerHtml = sValueTobeShowninDiv;
                RepNews.DataSource = dtnews;
                RepNews.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    protected void RepAnnouncement_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string NewsID = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "INT_ID").ToString());
            HtmlControl liNews = (HtmlControl)e.Item.FindControl("liNews");
            if (NewsID == Convert.ToString(Request.QueryString["annid"]))
            {
                liNews.Attributes.Add("class", "active");
            }

        }
    }
}