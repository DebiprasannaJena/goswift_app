using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;

public partial class IncentiveInvestorStatus : SessionCheck
{
    #region Global variable
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    int intRecCount = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Action"] != null)
            {
                Fillgrid();
            }
        }
    }

    private void Fillgrid()
    {
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.strAction = Request.QueryString["Action"].ToString();
        objSWP.intUserid = Convert.ToInt32(Session["InvestorId"]);
        objSWP.strFilterMode= Request.QueryString["FilterMode"].ToString();
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
    
}