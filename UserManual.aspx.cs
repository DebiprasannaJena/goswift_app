using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
public partial class UserManual : System.Web.UI.Page
{
    CmsBusinesslayer objCms = new CmsBusinesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGrid();
        }
    }
    public void bindGrid()
    { 
         List<EntityLayer.CMS.CMSDetails> CmsDetail = objCms.GetServiceManual().ToList();
        if(CmsDetail.Count>0)
        {
            grdVwUsrManual.DataSource = CmsDetail;
            grdVwUsrManual.DataBind();
        }
    }
    protected void grdVwUsrManual_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hyprDownload = (HyperLink)e.Row.FindControl("HyprDownload");
            string usrDownld = grdVwUsrManual.DataKeys[e.Row.RowIndex].Values[0].ToString();
            if (usrDownld != "NA")
            {
                hyprDownload.NavigateUrl = usrDownld;
            }
            else
            {
                hyprDownload.Text = usrDownld;
            }
        }
    }
    //protected void lbtnAll_Click(object sender, EventArgs e)
    //{
    //    if (lbtnAll.Text == "All")
    //    {
    //        lbtnAll.Text = "Paging";
    //        this.grdVwUsrManual.PageIndex = 0;
    //        this.grdVwUsrManual.AllowPaging = false;
    //    }
    //    else
    //    {
    //        lbtnAll.Text = "All";
    //        this.grdVwUsrManual.AllowPaging = true;
    //    }
    //    bindGrid();
    //}
    protected void grdVwUsrManual_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdVwUsrManual.PageIndex = e.NewPageIndex;
            bindGrid();
        }
        catch (Exception ex)
        {

        }
    }
}