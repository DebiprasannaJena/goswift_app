using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_AgendaFeedback : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    DataTable dt = null;
    string strVal = "";

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
        AMS objams = new AMS();

        objams.Action = "MF";
        dt = new DataTable();
        dt = AMServices.ViewProjectMaster(objams);
        grdFeedback.DataSource = dt;
        grdFeedback.DataBind();
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

    #endregion
    #region "Paging"
    private void DisplayPaging()
    {
        if (this.grdFeedback.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
        }
        if (this.grdFeedback.PageIndex + 1 == this.grdFeedback.PageCount)
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdFeedback.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + gIntRowsCount + "</b> of <b>" + gIntRowsCount + "</b>";
        }
        else
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdFeedback.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)grdFeedback.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((grdFeedback.PageSize - 1))) + "</b> of <b>" + gIntRowsCount + "</b>";
        }

    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "View Paging";
            grdFeedback.AllowPaging = false;
            grdFeedback.PageIndex = 0;
        }
        else
        {
            lbtnAll.Text = "All";
            grdFeedback.AllowPaging = true;
        }
        FillGrid();
    }

    protected void grdProjmst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdFeedback.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    #endregion
    #region "Gridview Command"
    protected void grdFeedback_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objams = new AMS();
        try
        {
            if (e.CommandName == "E")
            {
                objams.ProjectId = Convert.ToInt32(e.CommandArgument);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Feedback", "$(document).ready(function () { LoadFeedBack('" + objams.ProjectId + "') });", true);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null; }

    }
    #endregion
    protected void grdFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdFeedback.PageIndex = e.NewPageIndex;
        FillGrid();
    }
}