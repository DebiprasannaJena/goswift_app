#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using System.Web.UI.WebControls;
using System.Data;
using System.Web;
using System.Web.UI;
#endregion

public partial class Portal_Dashboard_IncentiveQueryDetails :SessionCheck
{
    int intRecCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["Status"] != null)
            {

                BindQueryIncentive();
            }
        }
    }
    private void BindQueryIncentive()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            int IncentiveQueryStatus = Convert.ToInt16(Request.QueryString["Status"].ToString());
            objDashboard.strAction = "QINCTV";
            objDashboard.Year = Request.QueryString["Year"].ToString();
            objDashboard.intStatus = IncentiveQueryStatus;
            objDashboard.intDistrictid = Convert.ToInt16(Request.QueryString["District"].ToString());
            //objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
            //if (Request.QueryString["Dept"] != null)
            //{
            //    objDashboard.intDeptId = Convert.ToInt32(Request.QueryString["Dept"]);
            //    objDashboard.intServiceId = Convert.ToInt32(Request.QueryString["Servc"]);
            //}
            //List<SWPDashboard> objServiceStatus = objDashboardBal.GetPEALQueryDetails(objDashboard).ToList();
            DataTable objServiceStatus = objDashboardBal.GetPEALQueryDetails(objDashboard);
            if (objServiceStatus.Rows.Count > 0)
            {
                intRecCount = objServiceStatus.Rows.Count;
                gvIncentive.DataSource = objServiceStatus;
                gvIncentive.DataBind();
                DisplayPaging();
                //if (IncentiveQueryStatus == 1)
                //{
                //    gvIncentive.Columns[8].Visible = false;
                //    gvIncentive.Columns[9].Visible = false;
                //    gvIncentive.Columns[10].Visible = false;
                //    gvIncentive.Columns[11].Visible = false;
                //}

                //if (IncentiveQueryStatus == 2)
                //{
                //    gvIncentive.Columns[4].Visible = false;
                //    gvIncentive.Columns[5].Visible = false;
                //    gvIncentive.Columns[6].Visible = false;
                //    gvIncentive.Columns[7].Visible = false;
                //}

                //if (IncentiveQueryStatus == 3 || IncentiveQueryStatus == 4 || IncentiveQueryStatus == 5)
                //{
                //    gvIncentive.Columns[12].Visible = true;
                //}
                //else
                //{
                //    gvIncentive.Columns[12].Visible = false;
                //}
                //if (IncentiveQueryStatus == 4)
                //{
                //    gvIncentive.Columns[8].Visible = false;
                //    gvIncentive.Columns[9].Visible = false;
                //    gvIncentive.Columns[10].Visible = false;
                //    gvIncentive.Columns[11].Visible = false;
                //}
                //if (IncentiveQueryStatus == 5)
                //{
                //    //gvIncentive.Columns[5].Visible = false;
                //    gvIncentive.Columns[6].Visible = false;
                //    gvIncentive.Columns[7].Visible = false;
                //    //gvIncentive.Columns[9].Visible = false;
                //    gvIncentive.Columns[10].Visible = false;
                //    gvIncentive.Columns[11].Visible = false;
                //}
                if (IncentiveQueryStatus == 5)
                {
                    gvIncentive.Columns[13].Visible = true;
                }
                else
                {
                    gvIncentive.Columns[13].Visible = false;
                }
            }
            else
            {
                gvIncentive.DataSource = null;
                gvIncentive.DataBind();
            }
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
    protected void gvIncentive_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvIncentive.PageIndex = e.NewPageIndex;
        BindQueryIncentive();
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
    protected void gvIncentive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvIncentive.PageIndex * this.gvIncentive.PageSize) + e.Row.RowIndex + 1);
        }
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvIncentive.PageIndex = 0;
            gvIncentive.AllowPaging = false;
            BindQueryIncentive();
        }
        else
        {
            lbtnAll.Text = "All";
            gvIncentive.AllowPaging = true;
            BindQueryIncentive();
        }
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        BindQueryIncentive();
        tbldv.Visible = true;
        lblCaption.Text = "Query Details in " + Request.QueryString["Year"];
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