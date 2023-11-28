using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BusinessLogicLayer.HelpDesk;

public partial class Portal_HelpDesk_ViewIssue_SubCategory : SessionCheck
{
    #region "Global Variable"
    /// <summary>
    /// Pradeep kumar sahoo
    /// All global variable declared here
    /// </summary>
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    string ApplicationPath = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"];
    DataTable dt = new DataTable();
    DataSet ds = new DataSet();
    HelpDeskBusinessLayer objlayer = new HelpDeskBusinessLayer();
    IssueRegistration objswp = new IssueRegistration();
    string FormHeader = "";
    string FormFooter = "";
    int intAllignment = 0;
    int intRetVal = 0;
    string strUnqId = "";
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridDetails();

        }
    }

    private void BindGridDetails()
    {
        try
        {
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objlist = new List<IssueRegistration>();
            objswp.Action = "V";
            objlist = objlayer.ViewIssueSubCategory(objswp);
            gvService.DataSource = objlist;
            gvService.DataBind();

            intRetVal = objlist.Count();
            DisplayPaging();
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }

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
                BindGridDetails();
            }
            else
            {
                lbtnAll.Text = "All";
                gvService.AllowPaging = true;
                BindGridDetails();
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }

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

            Util.LogError(ex, "Helpdesk");
        }
    }
    protected void gvService_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvService.PageIndex = e.NewPageIndex;
        BindGridDetails();
    }

    protected void lbtnAction_Click(object sender, EventArgs e)
    {
        Response.Redirect("Issue_SubCategory.aspx");
    }
    protected void gvService_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string s = gvService.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("Issue_SubCategory.aspx?id=" + s);
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
}