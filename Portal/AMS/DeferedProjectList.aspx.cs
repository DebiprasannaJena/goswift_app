using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_DeferedProjectList : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    DataTable dt = null;
    string strVal = "";
    static int intType = 0;
    int gIntRowsCount;
    #endregion

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

    #region "Fill Grid"

    private void FillGrid()
    {
        AMS objams = new AMS();
        dt = new DataTable();
        try
        {

            objams.Action = "PR"; // Rejected Projects
            dt = AMServices.ViewProjectMaster(objams);
            grdProjmst.DataSource = dt;
            grdProjmst.DataBind();

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
        catch (Exception)
        {
        }
        finally { objams = null; dt = null; }
    }

    #endregion

    #region "Paging"
    private void DisplayPaging()
    {
        if (this.grdProjmst.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
        }
        if (this.grdProjmst.PageIndex + 1 == this.grdProjmst.PageCount)
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + gIntRowsCount + "</b> of <b>" + gIntRowsCount + "</b>";
        }
        else
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((grdProjmst.PageSize - 1))) + "</b> of <b>" + gIntRowsCount + "</b>";
        }

    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "View Paging";
            grdProjmst.AllowPaging = false;
            grdProjmst.PageIndex = 0;
        }
        else
        {
            lbtnAll.Text = "All";
            grdProjmst.AllowPaging = true;
        }
        FillGrid();
    }

    protected void grdProjmst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdProjmst.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    #endregion
}