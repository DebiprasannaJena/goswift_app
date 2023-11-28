using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using EntityLayer.Proposal;
using BusinessLogicLayer.Proposal;
using Ionic.Zip;
using EntityLayer.Service;
using Common;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using BusinessLogicLayer.HelpDesk;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public partial class Portal_HelpDesk_MISReport : System.Web.UI.Page
{
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    string ApplicationPath = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"];
    DataTable dt = new DataTable();
    HelpDeskBusinessLayer objlayer = new HelpDeskBusinessLayer();
    IssueRegistration objswp = new IssueRegistration();
    ServiceDetails objService1 = new ServiceDetails();
    CommonHelperCls comm = new CommonHelperCls();
    DepartmentSMSClass objDepartmntSms = new DepartmentSMSClass();
    int totalNumberofCalls = 0;
    int totalIssuesResolved = 0;
    int totalIssuesPending = 0;
    int totalIssueResolvedSLA = 0;
    int totalOpenSLA = 0;
    int totalIssuePastSLA = 0;
    int totalIssueAvgHour = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindGridView();
            }
            catch (Exception ex)
            {

                Util.LogError(ex, "Helpdesk");
            }
        }
    }
    private void BindGridView()
    {
        try
        {
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objlist = new List<IssueRegistration>();
            objswp.Action = "V";
            objswp.vchFromDate = txtFromdate.Text.Trim();
            objswp.vchToDate = txtTodate.Text.Trim();
            objlist = objlayer.ViewIssueRegistrationMIS(objswp);
            gvService.DataSource = objlist;
            gvService.DataBind();
            txtFromdate.Text = txtFromdate.Text;
            txtTodate.Text = txtTodate.Text;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Helpdesk");
        }
    }
    private void BindGridView1()
    {
        try
        {
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objlist = new List<IssueRegistration>();
            objswp.Action = "V";
            objswp.vchFromDate = txtFromdate.Text.Trim();
            objswp.vchToDate = txtTodate.Text.Trim();
            objlist = objlayer.ViewIssueRegistrationMIS(objswp);
            GridView1.DataSource = objlist;
            GridView1.DataBind();
            txtFromdate.Text = txtFromdate.Text;
            txtTodate.Text = txtTodate.Text;

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Helpdesk");
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyCate = (HyperLink)e.Row.FindControl("hyCate");
                HyperLink hyCatepending = (HyperLink)e.Row.FindControl("hyCatepending");
                HyperLink hyPastSLA = (HyperLink)e.Row.FindControl("hyPastSLA");
                HyperLink hyPastSLA3 = (HyperLink)e.Row.FindControl("hyPastSLA3");
               
                hyCate.NavigateUrl = "MISIssuePendingDetails.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + GridView1.DataKeys[e.Row.RowIndex].Values[1] + "&MID=0" + "&FrmDate=" + txtFromdate.Text.Trim() + "&ToDate=" + txtTodate.Text.Trim();
                hyCatepending.NavigateUrl = "MISIssuePendingDetails.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + GridView1.DataKeys[e.Row.RowIndex].Values[1] + "&MID=1" + "&FrmDate=" + txtFromdate.Text.Trim() + "&ToDate=" + txtTodate.Text.Trim();
                hyPastSLA.NavigateUrl = "MISIssuePendingDetails.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + GridView1.DataKeys[e.Row.RowIndex].Values[1] + "&MID=2" + "&FrmDate=" + txtFromdate.Text.Trim() + "&ToDate=" + txtTodate.Text.Trim();
                hyPastSLA3.NavigateUrl = "MISIssuePendingDetails.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + GridView1.DataKeys[e.Row.RowIndex].Values[1] + "&MID=3" + "&FrmDate=" + txtFromdate.Text.Trim() + "&ToDate=" + txtTodate.Text.Trim();
                if (hyCate.Text == "0")
                {
                    hyCate.Enabled = false;
                }
                if (hyCatepending.Text == "0")
                {
                    hyCatepending.Enabled = false;
                }

                totalNumberofCalls = totalNumberofCalls + System.Convert.ToInt32(e.Row.Cells[4].Text);
                totalIssuesResolved = totalIssuesResolved + System.Convert.ToInt32(e.Row.Cells[5].Text);
                totalIssuesPending = totalIssuesPending + System.Convert.ToInt32(hyCatepending.Text);
                totalIssueResolvedSLA = totalIssueResolvedSLA + System.Convert.ToInt32(e.Row.Cells[7].Text);
                totalOpenSLA = totalOpenSLA + System.Convert.ToInt32(hyCate.Text);
                totalIssuePastSLA = totalIssuePastSLA + System.Convert.ToInt32(hyPastSLA.Text);
                totalIssueAvgHour = totalIssueAvgHour + System.Convert.ToInt32(hyPastSLA3.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "Total";
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Font.Bold = true;

                e.Row.Cells[4].Text = totalNumberofCalls.ToString("0");
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Font.Bold = true;

                e.Row.Cells[5].Text = totalIssuesResolved.ToString("0");
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Font.Bold = true;

                e.Row.Cells[6].Text = totalIssuesPending.ToString("0");
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Font.Bold = true;

                e.Row.Cells[7].Text = totalIssueResolvedSLA.ToString("0");
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Font.Bold = true;

                e.Row.Cells[8].Text = totalOpenSLA.ToString("0");
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].Font.Bold = true;

                e.Row.Cells[9].Text = totalIssuePastSLA.ToString("0");
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].Font.Bold = true;

                e.Row.Cells[10].Text = totalIssueAvgHour.ToString("0");
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].Font.Bold = true;
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Report");
        }
    }
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyCate = (HyperLink)e.Row.FindControl("hyCate");
                HyperLink hyCatepending = (HyperLink)e.Row.FindControl("hyCatepending");
                HyperLink hyPastSLA = (HyperLink)e.Row.FindControl("hyPastSLA");
                HyperLink hyPastSLA3 = (HyperLink)e.Row.FindControl("hyPastSLA3");
                hyCate.NavigateUrl = "MISIssuePendingDetails.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&MID=0"+ "&FrmDate="+ txtFromdate.Text.Trim() + "&ToDate=" + txtTodate.Text.Trim();
                hyCatepending.NavigateUrl = "MISIssuePendingDetails.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&MID=1" + "&FrmDate=" + txtFromdate.Text.Trim() + "&ToDate=" + txtTodate.Text.Trim();
                hyPastSLA.NavigateUrl = "MISIssuePendingDetails.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&MID=2" + "&FrmDate=" + txtFromdate.Text.Trim() + "&ToDate=" + txtTodate.Text.Trim();
                hyPastSLA3.NavigateUrl = "MISIssuePendingDetails.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&MID=3" + "&FrmDate=" + txtFromdate.Text.Trim() + "&ToDate=" + txtTodate.Text.Trim();
                if (hyCate.Text == "0")
                {
                    hyCate.Enabled = false;
                }
                if (hyCatepending.Text == "0")
                {
                    hyCatepending.Enabled = false;
                }

                totalNumberofCalls = totalNumberofCalls + System.Convert.ToInt32(e.Row.Cells[4].Text);
                totalIssuesResolved = totalIssuesResolved + System.Convert.ToInt32(e.Row.Cells[5].Text);
                totalIssuesPending = totalIssuesPending + System.Convert.ToInt32(hyCatepending.Text);
                totalIssueResolvedSLA = totalIssueResolvedSLA + System.Convert.ToInt32(e.Row.Cells[7].Text);
                totalOpenSLA = totalOpenSLA + System.Convert.ToInt32(hyCate.Text);
                totalIssuePastSLA = totalIssuePastSLA + System.Convert.ToInt32(hyPastSLA.Text);
                totalIssueAvgHour = totalIssueAvgHour + System.Convert.ToInt32(hyPastSLA3.Text);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "Total";
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].Font.Bold = true;

                e.Row.Cells[4].Text = totalNumberofCalls.ToString("0");
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Font.Bold = true;

                e.Row.Cells[5].Text = totalIssuesResolved.ToString("0");
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].Font.Bold = true;

                e.Row.Cells[6].Text = totalIssuesPending.ToString("0");
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].Font.Bold = true;

                e.Row.Cells[7].Text = totalIssueResolvedSLA.ToString("0");
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].Font.Bold = true;

                e.Row.Cells[8].Text = totalOpenSLA.ToString("0");
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].Font.Bold = true;

                e.Row.Cells[9].Text = totalIssuePastSLA.ToString("0");
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].Font.Bold = true;

                e.Row.Cells[10].Text = totalIssueAvgHour.ToString("0");
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].Font.Bold = true;
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Report");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void gvService_PreRender(object sender, System.EventArgs e)
    {
        int i = gvService.Rows.Count - 2;
        while (i >= 0)
        {
            if (gvService.Rows[i].Cells[2].Text == gvService.Rows[i + 1].Cells[2].Text)
            {
                gvService.Rows[i].Cells[2].RowSpan = gvService.Rows[i + 1].Cells[2].RowSpan < 2 ? 2 : gvService.Rows[i + 1].Cells[2].RowSpan + 1;
                gvService.Rows[i + 1].Cells[2].Visible = false;
            }
            if (gvService.Rows[i].Cells[1].Text == gvService.Rows[i + 1].Cells[1].Text)
            {
                gvService.Rows[i].Cells[1].RowSpan = gvService.Rows[i + 1].Cells[1].RowSpan < 2 ? 2 : gvService.Rows[i + 1].Cells[1].RowSpan + 1;
                gvService.Rows[i + 1].Cells[1].Visible = false;
            }
            i += -1;
        }
    }
    protected void GridView1_PreRender(object sender, System.EventArgs e)
    {
        int i = GridView1.Rows.Count - 2;
        while (i >= 0)
        {
            if (GridView1.Rows[i].Cells[2].Text == GridView1.Rows[i + 1].Cells[2].Text)
            {
                GridView1.Rows[i].Cells[2].RowSpan = GridView1.Rows[i + 1].Cells[2].RowSpan < 2 ? 2 : GridView1.Rows[i + 1].Cells[2].RowSpan + 1;
                GridView1.Rows[i + 1].Cells[2].Visible = false;
            }
            if (GridView1.Rows[i].Cells[1].Text == GridView1.Rows[i + 1].Cells[1].Text)
            {
                GridView1.Rows[i].Cells[1].RowSpan = GridView1.Rows[i + 1].Cells[1].RowSpan < 2 ? 2 : GridView1.Rows[i + 1].Cells[1].RowSpan + 1;
                GridView1.Rows[i + 1].Cells[1].Visible = false;
            }
            i += -1;
        }
    }
    protected void lnkExport_Click(object sender, EventArgs e)
    {
        BindGridView1();
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