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
using System.Web;
using System.Web.UI;
using System.Configuration;

#endregion

public partial class Portal_Dashboard_GrievanceGridStatus : SessionCheck
{
    #region global variable
    int intRecCount = 0;
    SWPDashboard objSWP = new SWPDashboard();
    DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
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
            if (Request.QueryString["Status"].ToString() != "")
            {
                FillGridGrivDetails();
            }
        }
    }

    private void FillGridGrivDetails()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        DataTable dt = new DataTable();
        try
        {
            objSWP.strAction = "DGRVD";
            objSWP.strFinacialYear = Request.QueryString["Year"].ToString();
            if (Request.QueryString["dist"].ToString() != null)
            {
                objSWP.intDistrictId = Convert.ToInt32(Request.QueryString["dist"].ToString());
            }
            else
            {
                objSWP.intDistrictId = 0;
            }
            objSWP.StrStatus = Request.QueryString["Status"].ToString();
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            dt = objserviceDashboard.DepartmentGrievanceDetails(objSWP);
            intRecCount = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                GrdGrivDetails.DataSource = dt;
                GrdGrivDetails.DataBind();
                DisplayPaging();
            }
            else
            {
                GrdGrivDetails.DataSource = null;
                GrdGrivDetails.DataBind();
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
    private void FillGridForExcelExport()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        DataTable dt = new DataTable();
        try
        {
            objSWP.strAction = "DGRVD";
            objSWP.strFinacialYear = Request.QueryString["Year"].ToString();

            if (Request.QueryString["dist"].ToString() != null)
            {
                objSWP.intDistrictId = Convert.ToInt32(Request.QueryString["dist"].ToString());
            }
            else
            {
                objSWP.intDistrictId = 0;
            }

            objSWP.StrStatus = Request.QueryString["Status"].ToString();
            objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
            dt = objserviceDashboard.DepartmentGrievanceDetails(objSWP);

            if (dt.Rows.Count > 0)
            {
                GrdExcel.DataSource = dt;
                GrdExcel.DataBind();

            }
            else
            {
                GrdExcel.DataSource = null;
                GrdExcel.DataBind();
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

    protected void GrdGrivDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdGrivDetails.PageIndex = e.NewPageIndex;
        FillGridGrivDetails();
    }
    protected void GrdGrivDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.GrdGrivDetails.PageIndex * this.GrdGrivDetails.PageSize) + e.Row.RowIndex + 1);
            Label lblstatus = (Label)e.Row.FindControl("lblstatus");
            if(lblstatus.Text== "InProgress")
            {
                lblstatus.ForeColor = System.Drawing.Color.Blue;
            }
            else if(lblstatus.Text == "Deferred")
            {
                lblstatus.ForeColor = System.Drawing.Color.DeepPink;
            }
            else if (lblstatus.Text == "Forwarded")
            {
                lblstatus.ForeColor = System.Drawing.Color.Orange;
            }
            else if (lblstatus.Text == "Resolved")
            {
                lblstatus.ForeColor = System.Drawing.Color.Green;
            }
            else if (lblstatus.Text == "Rejected")
            {
                lblstatus.ForeColor = System.Drawing.Color.Red;
            }

            HyperLink HypLnk_Griv_Id = (HyperLink)e.Row.FindControl("HypLnk_Griv_Id");
            HypLnk_Griv_Id.NavigateUrl = "~/Portal/GRIEVANCE/GrievanceApplicationDetails.aspx?GrivId=" + GrdGrivDetails.DataKeys[e.Row.RowIndex].Values["vchGrivId"] + "&RequestId=3";
        }
    }
    protected void LnkBtnAll_Click(object sender, EventArgs e)
    {
        if (LnkBtnAll.Text == "All")
        {
            LnkBtnAll.Text = "Paging";
            GrdGrivDetails.PageIndex = 0;
            GrdGrivDetails.AllowPaging = false;
            FillGridGrivDetails();
        }
        else
        {
            LnkBtnAll.Text = "All";
            GrdGrivDetails.AllowPaging = true;
            FillGridGrivDetails();
        }
    }


    #region "Display Paging in Gridview..."
    protected void DisplayPaging()
    {
        if (GrdGrivDetails.Rows.Count > 0)
        {
            this.LblPaging.Visible = true;
            LnkBtnAll.Visible = true;
            if (GrdGrivDetails.PageIndex + 1 == GrdGrivDetails.PageCount)
            {
                this.LblPaging.Text = "Results <b>" + GrdGrivDetails.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.LblPaging.Text = "Results <b>" + GrdGrivDetails.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(GrdGrivDetails.Rows[0].Cells[0].Text) + (GrdGrivDetails.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.LblPaging.Visible = false;
            LnkBtnAll.Visible = false;
        }
    }
    #endregion

    protected void LnkBtnExcelExport_Click(object sender, EventArgs e)
    {
        FillGridForExcelExport();

        LblExcelCaption.Text = "Grievance Details in  " + Request.QueryString["Year"];
        string strFileName = "GrievanceDetails" + string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + ".xls";

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", strFileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        DivForExcel.RenderControl(htw);
        HttpContext.Current.Response.Write(sw.ToString());
        HttpContext.Current.Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }
}