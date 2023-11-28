﻿#region Namespace
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Dashboard;
using BusinessLogicLayer.Proposal;
using System.Web;
using System.Web.UI;
#endregion

public partial class Portal_Dashboard_FramepealStatusDistrict : SessionCheck
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
            if (Request.QueryString["Status"] != null)
            {
                if (Request.QueryString["stateid"] != null)
                {
                    if (Request.QueryString["stateid"] == "11")
                    {
                        ddlType.SelectedValue = "1";
                        ddlType.Enabled = false;
                    }
                    else
                    {
                        ddlType.SelectedValue = "2";
                        ddlType.Enabled = false;
                    }
                }
                Fillgrid();
            }
        }
    }
    protected void gvPeal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPeal.PageIndex = e.NewPageIndex;
        Fillgrid();
    }

    public void Fillgrid()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.strAction = "PGM";
        objSWP.intPealstatus = Convert.ToInt16(Request.QueryString["Status"].ToString());
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //objSWP.intType = Convert.ToInt16(Request.QueryString["PealType"].ToString());
        objSWP.intDistrictid = Convert.ToInt16(Request.QueryString["Pealdistrict"].ToString());
        //objSWP.intQuarter = Convert.ToInt16(Request.QueryString["PealType"].ToString());
        //objSWP.intPealdistwise = Convert.ToInt32(DropDownList1.SelectedValue);
        objSWP.intPealdistwise = Convert.ToInt32(ddlType.SelectedValue);
        objSWP.intPealuseroption = Convert.ToInt16(ddldays.SelectedValue);
        objSWP.intPealuserstatus = Convert.ToInt16(Request.QueryString["PealUserStatus"].ToString());
        if (Request.QueryString["PealYear"].ToString() != null)
        {
            objSWP.strFinacialYear = Request.QueryString["PealYear"].ToString();
        }
        else
        {
            objSWP.intYearId = 0;
        }
        if (Convert.ToInt16(Request.QueryString["PealQuarter"].ToString()) != 0)
        {
            objSWP.intQuarter = Convert.ToInt16(Request.QueryString["PealQuarter"].ToString());
        }
        else
        {
            objSWP.intQuarter = 0;
        }

        objlist = objserviceDashboard.GetDashboardServiceStatusDtls(objSWP);
        

        if (objlist.Count > 0)
        {
            intRecCount = objlist.Count;
            gvPeal.DataSource = objlist;
            gvPeal.DataBind();
            div1.Visible = true;
            DisplayPaging();
            
        }
        else
        {
            div1.Visible = false;
            gvPeal.DataSource = null;
            gvPeal.DataBind();
        }
    }

    private void DisplayPaging()
    {
        if (gvPeal.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (gvPeal.PageIndex + 1 == gvPeal.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + gvPeal.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + gvPeal.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(gvPeal.Rows[0].Cells[0].Text) + (gvPeal.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
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
            gvPeal.PageIndex = 0;
            gvPeal.AllowPaging = false;
            Fillgrid();
        }
        else
        {
            lbtnAll.Text = "All";
            gvPeal.AllowPaging = true;
            Fillgrid();
        }
    }

    protected void gvPeal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.gvPeal.PageIndex * this.gvPeal.PageSize) + e.Row.RowIndex + 1);
        }
    }
   
    protected void ddldays_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    public void Fillgrid1()
    {
        objSWP = new SWPDashboard();
        objserviceDashboard = new DashboardBusinessLayer();
        List<SWPDashboard> objlist = new List<SWPDashboard>();
        objSWP.strAction = "PGM";
        objSWP.intPealstatus = Convert.ToInt16(Request.QueryString["Status"].ToString());
        objSWP.intUserid = Convert.ToInt32(Session["Userid"]);
        //objSWP.intType = Convert.ToInt16(Request.QueryString["PealType"].ToString());
        objSWP.intDistrictid = Convert.ToInt16(Request.QueryString["Pealdistrict"].ToString());
        //objSWP.intQuarter = Convert.ToInt16(Request.QueryString["PealType"].ToString());
        //objSWP.intPealdistwise = Convert.ToInt32(DropDownList1.SelectedValue);
        objSWP.intPealdistwise = Convert.ToInt32(ddlType.SelectedValue);
        objSWP.intPealuseroption = Convert.ToInt16(ddldays.SelectedValue);
        objSWP.intPealuserstatus = Convert.ToInt16(Request.QueryString["PealUserStatus"].ToString());
        if (Request.QueryString["PealYear"].ToString() != null)
        {
            objSWP.strFinacialYear = Request.QueryString["PealYear"].ToString();
        }
        else
        {
            objSWP.intYearId = 0;
        }
        if (Convert.ToInt16(Request.QueryString["PealQuarter"].ToString()) != 0)
        {
            objSWP.intQuarter = Convert.ToInt16(Request.QueryString["PealQuarter"].ToString());
        }
        else
        {
            objSWP.intQuarter = 0;
        }

        objlist = objserviceDashboard.GetDashboardServiceStatusDtls(objSWP);


        if (objlist.Count > 0)
        {
            intRecCount = objlist.Count;
            GridView1.DataSource = objlist;
            GridView1.DataBind();
            div1.Visible = true;
           

        }
        else
        {
            div1.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((DataControlRowType)e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.GridView1.PageIndex * this.GridView1.PageSize) + e.Row.RowIndex + 1);
        }
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["Status"] != null)
        {
            if (Request.QueryString["stateid"] != null)
            {
                if (Request.QueryString["stateid"] == "11")
                {
                    ddlType.SelectedValue = "1";
                    ddlType.Enabled = false;
                }
                else
                {
                    ddlType.SelectedValue = "2";
                    ddlType.Enabled = false;
                }
            }
            Fillgrid1();
        }
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