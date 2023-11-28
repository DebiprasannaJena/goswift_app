using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SingleWindow_ForwardProjectNodal : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    DataTable dt = null;
    int gIntRowsCount;
    #endregion

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }
    }

    #endregion

    #region "Fill Grid"

    private void FillGrid()
    {
        AMS objcs = new AMS();
        dt = new DataTable();
        try
        {
            objcs.Action = "VFP";
            dt = new DataTable();
            dt = AMServices.FillForwardProject(objcs);
            grdForwardProj.DataSource = dt;
            grdForwardProj.DataBind();
            gIntRowsCount = dt.Rows.Count;
            if (gIntRowsCount > 0)
            {
                DisplayPaging();
                lblMessage.Visible = false;
            }
            else
            {
                lblMessage.Visible = true;
                lbtnAll.Visible = false;
                lblPaging.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objcs = null; dt = null; }
    }

    #endregion

    #region "Paging"

    private void DisplayPaging()
    {
        if (this.grdForwardProj.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
        }
        if (this.grdForwardProj.PageIndex + 1 == this.grdForwardProj.PageCount)
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdForwardProj.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + gIntRowsCount + "</b> of <b>" + gIntRowsCount + "</b>";
        }
        else
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdForwardProj.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)grdForwardProj.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((grdForwardProj.PageSize - 1))) + "</b> of <b>" + gIntRowsCount + "</b>";
        }

    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "View Paging";
            grdForwardProj.AllowPaging = false;
            grdForwardProj.PageIndex = 0;
        }
        else
        {
            lbtnAll.Text = "All";
            grdForwardProj.AllowPaging = true;
        }
        FillGrid();
    }

    protected void grdProjmst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdForwardProj.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    #endregion


}