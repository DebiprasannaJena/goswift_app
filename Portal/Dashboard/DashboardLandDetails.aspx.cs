#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Net;
using BusinessLogicLayer.Dashboard;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.UI;
#endregion

public partial class Portal_Dashboard_DashboardLandDetails :SessionCheck
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
                BindLandDetails();
            }
            tbldv.Visible = false;
           // gvLand.Columns[2].HeaderText = "hello";
        }
    }
    private void BindLandDetails()
    {

        SWPDashboard objDashboard = new SWPDashboard();
        DashboardBusinessLayer objDashboardBal = new DashboardBusinessLayer();
        try
        {
            objDashboard.strAction = "LANDVDet";
            objDashboard.Year = Request.QueryString["Year"].ToString();
            objDashboard.intStatus = Convert.ToInt16(Request.QueryString["Status"].ToString());

            if (Request.QueryString["Type"].ToString() != null)
            {
                objDashboard.intDeptId =Convert.ToInt16(Request.QueryString["Type"].ToString());
            }
            else
            {
                objDashboard.intDeptId = 0;
            }
            if (Request.QueryString["Dist"].ToString() != null)
            {
                objDashboard.intDistrictid = Convert.ToInt16(Request.QueryString["Dist"].ToString());
            }
            else
            {
                 objDashboard.intDistrictid=0;
            }
            //objDashboard.intUserid = Convert.ToInt32(Session["Userid"]);
            //if (Request.QueryString["Dept"] != null)
            //{
            //    objDashboard.intDeptId = Convert.ToInt32(Request.QueryString["Dept"]);
            //    objDashboard.intServiceId = Convert.ToInt32(Request.QueryString["Servc"]);
            //}
            //List<SWPDashboard> objServiceStatus = objDashboardBal.GetPEALQueryDetails(objDashboard).ToList();
            List<SWPDashboard> objServiceStatus = objDashboardBal.GETLandDetails(objDashboard);
           
            gvLand.DataSource = objServiceStatus;
            gvLand.DataBind();
            if (gvLand.Rows.Count > 0)
            {
                if ((Convert.ToInt16(Request.QueryString["Status"].ToString()) == 2) || (Convert.ToInt16(Request.QueryString["Status"].ToString()) == 4))
                {
                    gvLand.HeaderRow.Cells[4].Text = "Payment Date";
                }
                else
                {
                    gvLand.HeaderRow.Cells[4].Text = "Application Date";
                }
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
    protected void gvLand_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLand.PageIndex = e.NewPageIndex;
        BindLandDetails();
    }
    private void DisplayPaging()
    {
        if (gvLand.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvLand.PageIndex + 1 == gvLand.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvLand.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvLand.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvLand.Rows[0].Cells[0].Text) + (gvLand.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    protected void gvLand_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvLand.PageIndex * this.gvLand.PageSize) + e.Row.RowIndex + 1);
        }
        
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvLand.PageIndex = 0;
            gvLand.AllowPaging = false;
            BindLandDetails();
        }
        else
        {
            lbtnAll.Text = "All";
            gvLand.AllowPaging = true;
            BindLandDetails();
        }
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        BindLandDetails();
        tbldv.Visible = true;
        lblCaption.Text = "Land Allotment Details in " + Request.QueryString["Year"];
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