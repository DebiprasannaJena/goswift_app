#region Namespaces
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Script.Serialization;
using System.Net;
using BusinessLogicLayer.Dashboard;
using System.Data.SqlClient;
using System.Text;
using System.Security.Cryptography;

#endregion

public partial class GrievanceInvestorDetails : SessionCheck
{
    #region global variable

    int intRecCount = 0;
   
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
    DataTable dt = new DataTable();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["GRVStatus"].ToString() != "")
        {
            GRVGridSatus();
        }
    }

    private void GRVGridSatus()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "GRVD";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            objSWP.StrStatus = Request.QueryString["GRVStatus"].ToString();
            dt = objserviceDashboard.getGrievanceDetails(objSWP);
            intRecCount = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                grdGRVDtl.DataSource = dt;
                grdGRVDtl.DataBind();
                DisplayPaging();
            }
            else
            {
                grdGRVDtl.DataSource = null;
                grdGRVDtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Dashboard");
        }
        finally
        {
            objSWP = null;
            objserviceDashboard = null;
        }

    }

    protected void grdGRVDtl_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdGRVDtl.PageIndex = e.NewPageIndex;
        GRVGridSatus();
    }
    protected void grdGRVDtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.grdGRVDtl.PageIndex * this.grdGRVDtl.PageSize) + e.Row.RowIndex + 1);

            HyperLink HypLnk_Griv_Id = (HyperLink)e.Row.FindControl("HypLnk_Griv_Id");
            HypLnk_Griv_Id.NavigateUrl = "~/GRIEVANCE/GrievanceDetails.aspx?StrGrievanceNo=" + grdGRVDtl.DataKeys[e.Row.RowIndex].Values["vchGrivId"];
        }
    }

    #region "Display Paging in Gridview..."

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            grdGRVDtl.PageIndex = 0;
            grdGRVDtl.AllowPaging = false;
            GRVGridSatus();
        }
        else
        {
            lbtnAll.Text = "All";
            grdGRVDtl.AllowPaging = true;
            GRVGridSatus();
        }
    }
    protected void DisplayPaging()
    {
        if (grdGRVDtl.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (grdGRVDtl.PageIndex + 1 == grdGRVDtl.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + grdGRVDtl.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + grdGRVDtl.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(grdGRVDtl.Rows[0].Cells[0].Text) + (grdGRVDtl.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }

    #endregion
}