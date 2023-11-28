using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.CMS;
using System.Data;
using BusinessLogicLayer.CMS;
using System.Web.UI.HtmlControls;
public partial class allEvents : System.Web.UI.Page
{
      CmsBusinesslayer objService = new CmsBusinesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["annid"] != null)
        {
            BindCondwiseNews();
        }
        else
        {

            FillContent();
        }
    }
    //private void FillContent()
    //{
    //    try
    //    {
    //        int intmenuid = Convert.ToInt32(Request.QueryString["id"]);
    //        string straction = "CHK";
    //        DataTable dt = objService.ChkCMSData(straction, intmenuid);
    //        if (dt.Rows.Count > 0)
    //        {
    //            string sValueTobeShowninDiv = dt.Rows[0]["vchContent"].ToString();
    //            divabout.InnerHtml = sValueTobeShowninDiv;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
    //    }
    //}
    private void FillContent()
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
                repsidenews.DataSource = dtnews;
                repsidenews.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    private void BindCondwiseNews()
    {
        try
        {
            string straction = "BAC";
            int id = Convert.ToInt32(Request.QueryString["annid"]);
            DataTable dt = objService.BindCondData(straction, id);
            string strtype = "News";
            string strnewsaction = "BN";
            DataTable dtleftnews = objService.BindNewsEventData(strnewsaction, strtype);
            if (dt.Rows.Count > 0)
            {                
                RepNews.DataSource = dt;
                RepNews.DataBind();
                repsidenews.DataSource = dtleftnews;
                repsidenews.DataBind();
            }
            else
            {
                RepNews.DataSource = null;
                RepNews.DataBind();
                repsidenews.DataSource = null;
                repsidenews.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    protected void repsidenews_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
     
    }
    protected void repsidenews_OnItemDataBound(object sender, RepeaterItemEventArgs e)
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