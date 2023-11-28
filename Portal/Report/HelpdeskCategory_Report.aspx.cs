using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.HelpDesk;
using System.Configuration;

public partial class Portal_Report_HelpdeskCategory_Report : SessionCheck
{
  
    IssueRegistration objswp = new IssueRegistration();
    HelpDeskBusinessLayer objlayer = new HelpDeskBusinessLayer();
    string strRetval = "";
    string filepath = "";
    string PEALCertificatefilepath = "";
    int intRetVal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGridview();

        }
    }
    public void fillGridview()
    {
        try
        {
            objlayer = new HelpDeskBusinessLayer();
          
            List<IssueRegistration> objProjList = new List<IssueRegistration>();
            objswp = new IssueRegistration();
            objswp.Action = "A";
            objProjList = objlayer.HelpdeskCategory_Report(objswp).ToList();
            if (objProjList != null && objProjList.Count > 0)
            gvService.DataSource = objProjList; ;
            gvService.DataBind();
            intRetVal = objProjList.Count;
            DisplayPaging();
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Report");
        }
        
    }
    #region "Display Google Paging"

    private void DisplayPaging()
    {
        try
        {
            if (gvService.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                lbtnAll.Visible = true;
                if (gvService.PageIndex + 1 == gvService.PageCount)
                {
                    lblPaging.Text = "Results <b>" + ((Label)gvService.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
                }
                else
                {
                    this.lblPaging.Text = "Results <b>" + ((Label)gvService.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)gvService.Rows[0].FindControl("lblsl")).Text) + gvService.PageSize - 1) + "</b> of <b>" + intRetVal + "</b>";
                }
            }
            else
            {
                this.lblPaging.Visible = false;
                lbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Report");
        }
    }
    #endregion
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        fillGridview();
    }
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                gvService.PageIndex = 0;
                gvService.AllowPaging = false;
                fillGridview();
            }
            else
            {
                lbtnAll.Text = "All";
                gvService.AllowPaging = true;
                fillGridview();
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Report");
        }
    }
    #region "Gridview RowDataBound"
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Text = ((gvService.PageIndex * gvService.PageSize) + e.Row.RowIndex + 1).ToString();
                HyperLink hyCate = (HyperLink)e.Row.FindControl("hyCate");
                hyCate.NavigateUrl = "HelpdeskSubCategory_Report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=C";


                HyperLink hyRcvd = (HyperLink)e.Row.FindControl("hyRcvd");
                hyRcvd.NavigateUrl = "HelpdeskSubCategory_Report.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=RS";

                HyperLink hyAppl = (HyperLink)e.Row.FindControl("hyAppl");
                hyAppl.NavigateUrl = "HelpDeskStatusReport.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=A1";


                HyperLink hydis = (HyperLink)e.Row.FindControl("hydis");
                hydis.NavigateUrl = "HelpDeskStatusReport.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=R";

                HyperLink hyApprove = (HyperLink)e.Row.FindControl("hyApprove");
                hyApprove.NavigateUrl = "HelpDeskStatusReport.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=AP";

                
                HyperLink hyPending = (HyperLink)e.Row.FindControl("hyPending");
                hyPending.NavigateUrl = "HelpDeskStatusReport.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&CID=" + gvService.DataKeys[e.Row.RowIndex].Values[0] + "&SID=" + gvService.DataKeys[e.Row.RowIndex].Values[1] + "&status=P";

                if (hyRcvd.Text == "0")
                {
                    hyRcvd.Enabled = false;
                }
                if (hyAppl.Text == "0")
                {
                    hyAppl.Enabled = false;
                }
                if (hydis.Text == "0")
                {
                    hydis.Enabled = false;
                }
                if (hyApprove.Text == "0")
                {
                    hyApprove.Enabled = false;
                }
                if (hyPending.Text == "0")
                {
                    hyPending.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Report");
        }
    }
    #endregion

}