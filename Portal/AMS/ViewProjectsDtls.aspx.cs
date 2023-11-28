using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SingleWindow_ViewProjectsDtls : System.Web.UI.Page
{
    int gIntRowsCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        FillProjectDtls();
    }
    public void FillProjectDtls()
    {
        AMS objams = new AMS();
        DataTable ObjDt = new DataTable();
       
            objams = new AMS();
            objams.Action = "V";
            DataSet ds = new DataSet();
            ds = AMServices.GetProjectCnt(objams);
            if (Request.QueryString["Status"] == "1")
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GrdProjects.DataSource = ds.Tables[0];
                    GrdProjects.DataBind();
                    gIntRowsCount = ds.Tables[0].Rows.Count;
                }
            }
            if (Request.QueryString["Status"] == "2")
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GrdProjects.DataSource = ds.Tables[1];
                    GrdProjects.DataBind();
                    gIntRowsCount = ds.Tables[1].Rows.Count;

                }
            }
            if (Request.QueryString["Status"] == "3")
            {
                if (ds.Tables[2].Rows.Count > 0)
                {
                    GrdProjects.DataSource = ds.Tables[2];
                    GrdProjects.DataBind();
                    gIntRowsCount = ds.Tables[2].Rows.Count;

                }
            }
            if (Request.QueryString["Status"] == "4")
            {
                if (ds.Tables[3].Rows.Count > 0)
                {
                    GrdProjects.DataSource = ds.Tables[3];
                    GrdProjects.DataBind();
                    gIntRowsCount = ds.Tables[3].Rows.Count;

                }
            }
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
    private void DisplayPaging()
    {
        if (this.GrdProjects.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
        }
        if (this.GrdProjects.PageIndex + 1 == this.GrdProjects.PageCount)
        {
            this.lblPaging.Text = "Results <b>" + ((Label)GrdProjects.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + gIntRowsCount + "</b> of <b>" + gIntRowsCount + "</b>";
        }
        else
        {
            this.lblPaging.Text = "Results <b>" + ((Label)GrdProjects.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)GrdProjects.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((GrdProjects.PageSize - 1))) + "</b> of <b>" + gIntRowsCount + "</b>";
        }

    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "View Paging";
            GrdProjects.AllowPaging = false;
            GrdProjects.PageIndex = 0;
        }
        else
        {
            lbtnAll.Text = "All";
            GrdProjects.AllowPaging = true;
        }
        FillProjectDtls();
    }
    protected void GrdProjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GrdProjects.PageIndex = e.NewPageIndex;
        FillProjectDtls();
    }
}