#region Namespace
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using System.Web;
using System.Web.UI;

#endregion

public partial class Portal_Dashboard_FrameYearWiseInvestment : System.Web.UI.Page
{
    #region Global variable
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    ProposalBAL objService = new ProposalBAL();
    int intRecCount = 0;
    decimal total = 0;
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
        tbldv.Visible = false;
    }

    private void Fillgrid()
    {
        SWPDashboard objDashboard = new SWPDashboard();
        List<SWPDashboard> objCSRStatus = new List<SWPDashboard>();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "PID";
            objDashboard.strFinacialYear = Request.QueryString["Pealyear"].ToString();
            // objDashboard.intYearId = Convert.ToInt16(Pealyear);
            //objDashboard.intDistrictid = Convert.ToInt16(Pealdistrict);
            //Request.QueryString["Pealdistrictdtls"].ToString();
            if (Request.QueryString["Pealdistrictdtls"].ToString() != "")
            {
                objDashboard.strDistrictDtl = Request.QueryString["Pealdistrictdtls"].ToString();
            }
            else
            {
                objDashboard.strDistrictDtl = "";
            }
            objCSRStatus = objDashboardBal.GetDashboardServiceStatusDtls(objDashboard);
            intRecCount = objCSRStatus.Count;
            if (objCSRStatus.Count > 0)
            {
                GridView1.DataSource = objCSRStatus;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
       
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.GridView1.PageIndex * this.GridView1.PageSize) + e.Row.RowIndex + 1);
            total = Convert.ToDecimal(total) + Convert.ToDecimal(e.Row.Cells[3].Text);
        }
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.Footer)
        {
             e.Row.Cells[0].Text = "Total";
             e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
             e.Row.Cells[0].Font.Bold = true;

             e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
             e.Row.Cells[3].Text = total.ToString("0.00");
        }
          
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        Fillgrid();
        tbldv.Visible = true;
        lblCaption.Text = "Year wise investment Details in " + Request.QueryString["Pealyear"].ToString();
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