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

public partial class Portal_Dashboard_ServiceQueryDetails : SessionCheck
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
                BindQueryService();
            }
            tbldv.Visible = false;
        }
    }
    private void BindQueryService()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            int ServiceStatus = Convert.ToInt16(Request.QueryString["Status"].ToString());
            objDashboard.strAction = "QSERVICEV";
            objDashboard.Year = Request.QueryString["Year"].ToString();
            objDashboard.intStatus = ServiceStatus;
            if (Request.QueryString["District"].ToString() != null)
            {
                objDashboard.intDistrictid = Convert.ToInt16(Request.QueryString["District"].ToString());
            }
            else
            {
                objDashboard.intDistrictid = 0;
            }
            // objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
            if (Request.QueryString["deptId"].ToString() != null)
            {
                objDashboard.intDeptId = Convert.ToInt32(Request.QueryString["deptId"].ToString());
                // objDashboard.intServiceId = Convert.ToInt32(Request.QueryString["Servc"]);
            }
            else
            {
                objDashboard.intDeptId = 0;
            }
            //List<SWPDashboard> objServiceStatus = objDashboardBal.GetServicesQueryDetails(objDashboard).ToList();

            DataTable objServiceStatus = objDashboardBal.GetServicesQueryDetails(objDashboard);
            #region Pivot
            // Pivot pt = new Pivot(objServiceStatus);
           //// string[] PARAM = {  "APPN dATE", "QUERY_DATE", "VCH_INVESTOR_NAME", "Query", "Avg_time"};
           // string[] PARAM1 = { "VCH_INVESTOR_NAME", "VCH_APPLICATION_UNQ_KEY", "APPN_dATE"};
           // string[] PARAM = { "Query1"};
           // //gvService.DataSource = pt.PivotData("VCH_INVESTOR_NAME", AggregateFunction.Last, PARAM1, PARAM);
            //gvService.DataSource = pt.PivotData("VCH_INVESTOR_NAME", "vchRemarks", AggregateFunction.Last, PARAM);
            //gvService.DataSource = pt.PivotData("vchRemarks", AggregateFunction.First, PARAM1, PARAM);
            #endregion

            if (objServiceStatus.Rows.Count > 0)
            {
                intRecCount = objServiceStatus.Rows.Count; 
                gvService.DataSource = objServiceStatus;
                gvService.DataBind();
                DisplayPaging();
                //if (ServiceStatus == 1)
                //{
                //    gvService.Columns[9].Visible = false;
                //    gvService.Columns[10].Visible = false;
                //    gvService.Columns[11].Visible = false;
                //    gvService.Columns[12].Visible = false;
                //}

                //if (ServiceStatus == 2)
                //{
                //    gvService.Columns[5].Visible = false;
                //    gvService.Columns[6].Visible = false;
                //    gvService.Columns[7].Visible = false;
                //    gvService.Columns[8].Visible = false;
                //}
                
                // if (ServiceStatus == 3 || ServiceStatus == 4 || ServiceStatus == 5)
                //{
                //    gvService.Columns[13].Visible = true;
                //}
                //else
                //{
                //    gvService.Columns[13].Visible = false;
                //}
                //if (ServiceStatus == 4)
                //{
                //    gvService.Columns[9].Visible = false;
                //    gvService.Columns[10].Visible = false;
                //    gvService.Columns[11].Visible = false;
                //    gvService.Columns[12].Visible = false;
                //}
                //if (ServiceStatus == 5)
                //{
                //   // gvService.Columns[6].Visible = false;
                //    gvService.Columns[7].Visible = false;
                //    gvService.Columns[8].Visible = false;
                //    //gvService.Columns[9].Visible = false;
                //    //gvService.Columns[10].Visible = false;
                //    gvService.Columns[11].Visible = false;
                //    gvService.Columns[12].Visible = false;
                //}
                if (ServiceStatus == 5)
                {
                    gvService.Columns[13].Visible = true;
                }
                else
                {
                    gvService.Columns[13].Visible = false;
                }

            }
            else
            {
                gvService.DataSource = null;
                gvService.DataBind();
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
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        BindQueryService();
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
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvService.PageIndex * this.gvService.PageSize) + e.Row.RowIndex + 1);
            Label lblUniqID = (Label)e.Row.FindControl("Label1det");
            lblUniqID.Text = "'" + lblUniqID.Text;
           
        }
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvService.PageIndex = 0;
            gvService.AllowPaging = false;
            BindQueryService();
        }
        else
        {
            lbtnAll.Text = "All";
            gvService.AllowPaging = true;
            BindQueryService();
        }
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        BindQueryService();
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