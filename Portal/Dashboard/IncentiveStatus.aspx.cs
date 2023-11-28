using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using System.Web;
using System.Web.UI;

public partial class Portal_Dashboard_IncentiveStatus : SessionCheck
{
    #region Global variable
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    int intRecCount = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["Action"] != null & Request.QueryString["IncentiveType"] != null & Request.QueryString["IncentiveYear"] != null)
            {
                Fillgrid();
            }
            tbldv.Visible = false;
        }
    }

    private void Fillgrid()
    {
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.strAction = Request.QueryString["Action"].ToString();
        objSWP.strFinacialYear = Request.QueryString["IncentiveYear"];
        objSWP.intQuarter = Convert.ToInt16(Request.QueryString["IncentiveType"]);
        objSWP.intUserid = Convert.ToInt16(string.IsNullOrEmpty(Request.QueryString["Userid"]) ? default(string) : "0");
        objSWP.intDistrictid = Convert.ToInt16(Request.QueryString["Distid"]);
        objlist = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP);
        intRecCount = objlist.Count;

        if (objlist.Count > 0)
        {

            gvIncentive.DataSource = objlist;
            gvIncentive.DataBind();
            DisplayPaging();
        }
        else
        {
            gvIncentive.DataSource = null;
            gvIncentive.DataBind();
        }
    }
    protected void gvIncentive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvIncentive.PageIndex * this.gvIncentive.PageSize) + e.Row.RowIndex + 1);
        }
    }

    private void DisplayPaging()
    {
        if (gvIncentive.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvIncentive.PageIndex + 1 == gvIncentive.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvIncentive.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvIncentive.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvIncentive.Rows[0].Cells[0].Text) + (gvIncentive.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvIncentive.PageIndex = 0;
            gvIncentive.AllowPaging = false;
            Fillgrid();
        }
        else
        {
            lbtnAll.Text = "All";
            gvIncentive.AllowPaging = true;
            Fillgrid();
        }
    }
    protected void gvIncentive_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncentive.PageIndex = e.NewPageIndex;
        Fillgrid();
    }
    private void Fillgrid1()
    {
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.strAction = Request.QueryString["Action"].ToString();
        objSWP.strFinacialYear = Request.QueryString["IncentiveYear"];
        objSWP.intQuarter = Convert.ToInt16(Request.QueryString["IncentiveType"]);
        objSWP.intUserid = Convert.ToInt16(string.IsNullOrEmpty(Request.QueryString["Userid"]) ? default(string) : "0");
        objSWP.intDistrictid = Convert.ToInt16(Request.QueryString["Distid"]);
        objlist = objserviceDashboard.GetDashboardServiceIncentiveDtls(objSWP);
        intRecCount = objlist.Count;

        if (objlist.Count > 0)
        {

            GridView1.DataSource = objlist;
            GridView1.DataBind();
           
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.GridView1.PageIndex * this.GridView1.PageSize) + e.Row.RowIndex + 1);
        }
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        Fillgrid1();
        tbldv.Visible = true;
        lblCaption.Text = "Incentive Details in " + Request.QueryString["IncentiveYear"];
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Details.xls"));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        viewTable.RenderControl(htw);
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}