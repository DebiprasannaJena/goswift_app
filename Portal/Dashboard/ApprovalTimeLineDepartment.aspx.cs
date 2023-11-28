#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;

#endregion

public partial class Portal_Dashboard_ApprovalTimeLineDepartment : SessionCheck
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

        BindExceedService();
    }
    private void BindExceedService()
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
            //objDashboard.intDaysPass = ddlDays.SelectedValue;
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
}