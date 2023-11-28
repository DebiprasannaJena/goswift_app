using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.HelpDesk;
using System.Configuration;

public partial class Portal_Report_HelpDeskStatusReport : SessionCheck
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
            objswp = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objProjList = new List<IssueRegistration>();
            objswp.Action = Request.QueryString["status"].ToString();
            objswp.int_CategoryId = Convert.ToInt32(Request.QueryString["CID"].ToString());
            objProjList = objlayer.Helpdesk_Status_Report(objswp).ToList();
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
                HyperLink hyRcvd1 = (HyperLink)e.Row.FindControl("hlnkTkn");
                hyRcvd1.NavigateUrl = "ServiceDetailsView.aspx?vchIssueNo=" + gvService.DataKeys[e.Row.RowIndex].Values[3];
               
                if (hyRcvd1.Text == "0")
                {
                    hyRcvd1.Enabled = false;
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