#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
#endregion

public partial class Portal_Dashboard_PealQueryDetailTourismIt : System.Web.UI.Page
{
    int intRecCount = 0;
    int itSectorId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdIT"]);
    int tourismSectorId = Convert.ToInt32(ConfigurationManager.AppSettings["SectorIdTOURISM"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
        if (Request.QueryString["Status"] != null)
        {

            BindQueryPeal();
        }
    }
    private void BindQueryPeal()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            int PealQueryStatus = Convert.ToInt16(Request.QueryString["Status"].ToString());
            objDashboard.strAction = "QPEALVT";
            objDashboard.Year = Request.QueryString["Year"].ToString();
            objDashboard.intStatus = PealQueryStatus;
            objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);

            if (Convert.ToInt32(Session["Userid"]) == 167 || Convert.ToInt32(Session["Userid"]) == 1196)
            {
                objDashboard.intSecId = itSectorId;
                objDashboard.intType = 2;
            }
            else if (Convert.ToInt32(Session["Userid"]) == 166 || Convert.ToInt32(Session["Userid"]) == 1197)
            {
                objDashboard.intSecId = tourismSectorId;
                objDashboard.intType = 2;

            }
            //if (Request.QueryString["Dept"] != null)
            //{
            //    objDashboard.intDeptId = Convert.ToInt32(Request.QueryString["Dept"]);
            //    objDashboard.intServiceId = Convert.ToInt32(Request.QueryString["Servc"]);
            //}
            //List<SWPDashboard> objServiceStatus = objDashboardBal.GetPEALQueryDetails(objDashboard).ToList();
            DataTable objServiceStatus = objDashboardBal.GetPEALQueryDetails(objDashboard);
            if (objServiceStatus.Rows.Count > 0)
            {
                gvpeal.DataSource = objServiceStatus;
                gvpeal.DataBind();
                if (PealQueryStatus == 1 || PealQueryStatus == 5)
                {
                    gvpeal.Columns[8].Visible = false;
                    gvpeal.Columns[9].Visible = false;
                    gvpeal.Columns[10].Visible = false;
                    gvpeal.Columns[11].Visible = false;
                }

                if (PealQueryStatus == 2)
                {
                    gvpeal.Columns[4].Visible = false;
                    gvpeal.Columns[5].Visible = false;
                    gvpeal.Columns[6].Visible = false;
                    gvpeal.Columns[7].Visible = false;
                }

                if (PealQueryStatus == 3 || PealQueryStatus == 4 || PealQueryStatus == 5)
                {
                    gvpeal.Columns[12].Visible = true;
                }
                else
                {
                    gvpeal.Columns[12].Visible = false;
                }
                if (PealQueryStatus == 4)
                {
                    gvpeal.Columns[8].Visible = false;
                    gvpeal.Columns[9].Visible = false;
                    gvpeal.Columns[10].Visible = false;
                    gvpeal.Columns[11].Visible = false;
                }
                if (PealQueryStatus == 5)
                {
                    //gvpeal.Columns[5].Visible = false;
                    gvpeal.Columns[6].Visible = false;
                    gvpeal.Columns[7].Visible = false;
                    gvpeal.Columns[9].Visible = false;
                    gvpeal.Columns[10].Visible = false;
                    gvpeal.Columns[11].Visible = false;
                }
            }
            else
            {
                gvpeal.DataSource = null;
                gvpeal.DataBind();
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
    protected void gvPeal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvpeal.PageIndex = e.NewPageIndex;
        BindQueryPeal();
    }
    private void DisplayPaging()
    {
        if (gvpeal.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvpeal.PageIndex + 1 == gvpeal.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvpeal.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvpeal.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvpeal.Rows[0].Cells[0].Text) + (gvpeal.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    protected void gvPeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvpeal.PageIndex * this.gvpeal.PageSize) + e.Row.RowIndex + 1);
        }
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvpeal.PageIndex = 0;
            gvpeal.AllowPaging = false;
            BindQueryPeal();
        }
        else
        {
            lbtnAll.Text = "All";
            gvpeal.AllowPaging = true;
            BindQueryPeal();
        }
    }
    //protected void gvpeal_PreRender(object sender, EventArgs e)
    //{
    //    GridDecorator.MergeRows(gvpeal);
    //}
    //public class GridDecorator
    //{
    //    public static void MergeRows(GridView gridView)
    //    {
    //        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
    //        {
    //            GridViewRow row = gridView.Rows[rowIndex];
    //            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

    //            if ((((HiddenField)row.FindControl("HiddenField1")).Value.Trim() == ((HiddenField)previousRow.FindControl("HiddenField1")).Value.Trim()))
    //            {
    //                row.Cells[1].RowSpan = previousRow.Cells[1].RowSpan < 2 ? 2 :
    //                                      previousRow.Cells[1].RowSpan + 1;
    //                row.Cells[1].Style.Add("vertical-align", "middle");
    //                previousRow.Cells[1].Visible = false;
    //            }
    //        }
    //    }
    //}
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        BindQueryPeal();
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