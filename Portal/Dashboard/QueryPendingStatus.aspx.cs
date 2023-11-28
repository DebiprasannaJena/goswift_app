using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
public partial class Portal_Dashboard_QueryPendingStatus : SessionCheck
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
            Fillgrid();
        }
    }
    private void Fillgrid()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
        //{
        //    objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
           
                objDashboard.intDeptId = 0;
                //objDashboard.intServiceId = Convert.ToInt32(Request.QueryString["Servc"]);
                objDashboard.intMonthId = Convert.ToInt16(Request.QueryString["Month"].ToString());
            objDashboard.strAction = "PC2";
            objDashboard.strOption = DropDownList1.SelectedValue;
            List<SWPDashboard> objServiceStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard).ToList();
            intRecCount = objServiceStatus.Count;
            if (objServiceStatus.Count > 0)
            {
                gvService.DataSource = objServiceStatus;
                gvService.DataBind();
                DisplayPaging();
            }
            else
            {
                gvService.DataSource = null;
                gvService.DataBind();
                DisplayPaging();
            }
         
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDashboard = null;
            objDashboardBal = null;
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
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvService.PageIndex = 0;
            gvService.AllowPaging = false;
            Fillgrid();
        }
        else
        {
            lbtnAll.Text = "All";
            gvService.AllowPaging = true;
            Fillgrid();
        }
    }
  
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        Fillgrid();
    }

    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);
        }
    }
}