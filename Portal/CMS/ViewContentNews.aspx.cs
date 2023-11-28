#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Data;
#endregion


public partial class Portal_ViewContentNews :SessionCheck
{
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    DataTable dt = null;
    int intcount = 0, intID=0;  
    EntityLayer.CMS.CMSDetails objProp = new EntityLayer.CMS.CMSDetails();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
              BindGridview();
    }
    public void BindGridview()
    {
        dt = new DataTable();
        dt = objService.BindNewsEventData("VA", "");
        if (dt.Rows.Count > 0)
        {
            grdContent.DataSource = dt;
            grdContent.DataBind();
            intcount = dt.Rows.Count;          
            DisplayPaging(intcount);
        }
        else
        {
            grdContent.DataSource = null;
            grdContent.DataBind();
        }
    }
    #region GridView_Events
    protected void grdContent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdContent.PageIndex = e.NewPageIndex;
        BindGridview();
    }
    #endregion
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            grdContent.PageIndex = 0;
            grdContent.AllowPaging = false;
            BindGridview();
        }
        else
        {
            lbtnAll.Text = "All";
            grdContent.AllowPaging = true;
            BindGridview();
            DisplayPaging(intcount);
        }
    }
    #region "Display Paging"
    protected void DisplayPaging(int intRecCount)//Disply Paging of Gridview
    {
        try
        {
            if (grdContent.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                grdContent.Visible = true;
                if (grdContent.PageIndex + 1 == grdContent.PageCount)
                {
                    this.lblPaging.Text = "Results " + "<b>" + (Convert.ToInt32((grdContent.PageIndex * grdContent.PageSize)) + 1) + "</b> - <b>" + intRecCount + " " + "of" + " " + intcount + "</b>";
                }
                else
                {
                    this.lblPaging.Text = "Results " + "<b>" + (Convert.ToInt32((grdContent.PageIndex * grdContent.PageSize)) + 1) + "</b> - <b>" + ((grdContent.PageIndex + 1) * grdContent.PageSize) + " " + "of" + " " + intcount + "</b>";
                }
            }
            else
            {
                this.lblPaging.Visible = false;
                lbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "alert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    #endregion
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtfilter = new DataTable();
        dtfilter = objService.BindNewsEventData("FN", ddltype.SelectedValue.ToString());
        if (dtfilter.Rows.Count > 0)
        {
            grdContent.DataSource = dtfilter;
            grdContent.DataBind();
            intcount = dtfilter.Rows.Count;
            DisplayPaging(intcount);
        }
        else
        {
            grdContent.DataSource = null;
            grdContent.DataBind();
        }
    }
  
}