using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_ViewApprovedProjects : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    DataTable dt = null;
    string strVal = "";
    int gIntRowsCount;
    static int intType = 0;
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
                intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
                if (grdProjmst.AllowPaging == true)
                {
                    grdProjmst.PageIndex = string.IsNullOrEmpty(Request.QueryString["PIndex"]) ? 0 : Convert.ToInt32(Request.QueryString["PIndex"]);
                }
                FillGrid();
            }
        }
    }

    #endregion

    #region "Fill Grid"

    private void FillGrid()
    {
        AMS objams = new AMS();

        objams.Action = "VACMD";
        objams.OfficerId = Convert.ToInt32(Session["UserId"]);
        objams.TypeId = intType;
        dt = new DataTable();
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

    protected void grdProjmst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int intFinDoc = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[2]);
            int intReOpenStatus = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[3]);

            if (intFinDoc == 0)
                e.Row.Cells[7].Text = "";
            if (intReOpenStatus == 1)
            {
                e.Row.Style.Add("background-color", "#ff4348");
            }
        }

    }
    #endregion

  
}
