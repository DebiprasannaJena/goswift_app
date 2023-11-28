using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;

public partial class Portal_CMS_ViewUseFulink : System.Web.UI.Page
{
    #region Variable Declaration
    string str_Retvalue = "";
    int retval = 0;
    string fileNM = "";
    CMSDetails obj = new CMSDetails();
    List<CMSDetails> newlist = new List<CMSDetails>();
    CmsBusinesslayer objbusiness = new CmsBusinesslayer();
    int intOutput = 0, gIntretval = 0;
    string strShowMsg = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FILLGRID();
        }
    }
    #region FillGrid
    protected void FILLGRID()
    {
        try
        {
            obj.actioncode = "V";
            newlist = objbusiness.UseFulLinkDetails(obj);
            GrdViewData.DataSource = newlist;
            GrdViewData.DataBind();
            retval = newlist.Count();
            DisplayPaging();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    #endregion
    #region GridView Row Command
    protected void GrdViewData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "delete")
            {
                GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                obj.actioncode = "D";
                obj.intlinkId = Convert.ToInt32(GrdViewData.DataKeys[gvr.RowIndex].Values["intlinkId"].ToString());
                str_Retvalue = objbusiness.AddUseFulinkDetails(obj);
                if (str_Retvalue == "3")
                {
                    Response.Write("<script>alert('Data Deleted successfully')</script>");

                }
                FILLGRID();
            }
        }
        catch (Exception ex)
        {

            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    #endregion
    #region GridView Event
    protected void GrdViewData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hypEdit = (HyperLink)e.Row.FindControl("hypEdit");
                hypEdit.NavigateUrl = "AddUseFulink.aspx?ID=" + GrdViewData.DataKeys[e.Row.RowIndex].Value;
                e.Row.Cells[0].Text = Convert.ToString((this.GrdViewData.PageIndex * this.GrdViewData.PageSize) + e.Row.RowIndex + 1);
            }
        }
        catch (Exception ex)
        {

            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    #endregion
    #region Labal button CLick
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                GrdViewData.PageIndex = 0;
                GrdViewData.AllowPaging = false;
                FILLGRID();
            }
            else
            {
                lbtnAll.Text = "All";
                GrdViewData.AllowPaging = true;
                FILLGRID();
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    #endregion
    #region Paging
    private void DisplayPaging()
    {
        try
        {
            if (GrdViewData.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                lbtnAll.Visible = true;
                if (GrdViewData.PageIndex + 1 == GrdViewData.PageCount)
                {
                    this.lblPaging.Text = "Results" + " <b>" + GrdViewData.Rows[0].Cells[0].Text + "</b> - <b>" + retval + "</b> Of <b>" + retval + "</b>";
                }
                else
                {
                    this.lblPaging.Text = "Results" + " <b>" + GrdViewData.Rows[0].Cells[0].Text + "</b> - <b>" + Convert.ToInt32(Convert.ToInt32(GrdViewData.Rows[0].Cells[0].Text) + Convert.ToInt32((GrdViewData.PageSize - 1))) + "</b> Of <b>" + retval + "</b>";
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
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    #endregion
    #region Deleting Method
    protected void GrdViewData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    #endregion
    #region Page Event Chnging
    protected void GrdViewData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GrdViewData.PageIndex = e.NewPageIndex;
            FILLGRID();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    #endregion

}
