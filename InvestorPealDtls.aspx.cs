#region Namespace
using System;
using System.Collections.Generic;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
#endregion

public partial class InvestorPealDtls : SessionCheck
{
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    int intRecCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ProposalNo"] != null)
            {
                GRIDBIND();
            }
        }
    }

    private void GRIDBIND()
    {
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        objSWP.strAction = "PCS";
        objSWP.strPealProposalno = Request.QueryString["ProposalNo"].ToString();
        objSWP.strPealQuerystatus = Request.QueryString["QueryStatus"].ToString();
        objlist = objserviceDashboard.GetInvestorPealDtls(objSWP);
        if (objlist.Count > 0)
        {
            grdPEALStatus.DataSource = objlist;
            grdPEALStatus.DataBind();
        }
        else
        {
            grdPEALStatus.DataSource = null;
            grdPEALStatus.DataBind();
        }
        intRecCount = objlist.Count;
    }
}