#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion
public partial class Portal_Dashboard_EnergyApprovalTimeline : SessionCheck
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
            BindExceedService();
            tbldv.Visible = false;
        }
    }
    private void BindExceedService()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            //if (Convert.ToInt32(Request.QueryString["Servc"]) == 16) ///// New Power Connection
            //{
            //    objDashboard.strAction = "TEUCDP"; //// Detailed Utility Count for ORTPSA Crossed
            //}
            //else
            //{
            //    objDashboard.strAction = "DE";
            //}
            objDashboard.strAction = "TEUCDP";
            objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
            if (Request.QueryString["Dept"] != null)
            {
                objDashboard.intDeptId = Convert.ToInt32(Request.QueryString["Dept"]);
                objDashboard.intServiceId = Convert.ToInt32(Request.QueryString["Servc"]);
            }

            List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
            intRecCount = objServiceStatus.Count();
            gvService.DataSource = objServiceStatus;
            gvService.DataBind();
            DisplayPaging();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objDashboard = null;
            objDashboardBal = null;
        }
    }
    private void BindExceedService1()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
            if (Request.QueryString["Dept"] != null)
            {
                objDashboard.intDeptId = Convert.ToInt32(Request.QueryString["Dept"]);
                objDashboard.intServiceId = Convert.ToInt32(Request.QueryString["Servc"]);
            }
            objDashboard.strAction = "DE";

            List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
            intRecCount = objServiceStatus.Count();
            GridView1.DataSource = objServiceStatus;
            GridView1.DataBind();
            DisplayPaging();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objDashboard = null;
            objDashboardBal = null;
        }
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        BindExceedService1();
        tbldv.Visible = true;
        lblCaption.Text = "Applications past ORTPSA timelines Details";
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
    protected void gvService_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);
            Label lblUniqID = (Label)e.Row.FindControl("lblUniq");
            lblUniqID.Text = "'" + lblUniqID.Text;
        }

    }
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        BindExceedService();
    }
    protected void GridView1_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.GridView1.PageIndex * this.GridView1.PageSize) + e.Row.RowIndex + 1);
            Label lblUniqID = (Label)e.Row.FindControl("lblUniq");
            lblUniqID.Text = "'" + lblUniqID.Text;
        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindExceedService1();
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvService.PageIndex = 0;
            gvService.AllowPaging = false;
            BindExceedService();
        }
        else
        {
            lbtnAll.Text = "All";
            gvService.AllowPaging = true;
            BindExceedService();
        }
    }
    private void DisplayPaging()
    {
        if (gvService.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvService.PageIndex + 1 == gvService.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvService.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvService.Rows[0].Cells[0].Text) + (gvService.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }

}