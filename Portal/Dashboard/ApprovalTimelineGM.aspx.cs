#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using System.Web;
using System.Web.UI;

#endregion

public partial class Portal_Dashboard_ApprovalTimelineGM : SessionCheck
{
    #region Global variable
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
        if (Request.QueryString["ServiceStatus"].ToString() != "")
        {
            BindExceedService();
        }
    }
    private void BindExceedService()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);            
            objDashboard.intDeptId = 0;
            objDashboard.intDistrictid = Convert.ToInt32(Request.QueryString["Dist"]);
            objDashboard.intMonthId = Convert.ToInt32(Request.QueryString["Month"]);
            //objDashboard.intYearId = Convert.ToInt32(Request.QueryString["Year"]);
            objDashboard.strFinacialYear = Request.QueryString["Year"];
            
            objDashboard.strAction = "DGI";
            objDashboard.StrStatus = Request.QueryString["ServiceStatus"].ToString();
            objDashboard.intDaysPass = ddlDays.SelectedValue;
            List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
            gvService.DataSource = objServiceStatus;
            gvService.DataBind();
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
    protected void ddlDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindExceedService();
    }
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Request.QueryString["ServiceStatus"].ToString() == "P")
            {
                dv1.Visible = true;
            }
            else
            {
                dv1.Visible = false;
            }
            Label lblUniqID = (Label)e.Row.FindControl("lblUniq");
            lblUniqID.Text = "'" + lblUniqID.Text;
        }
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        BindExceedService();
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