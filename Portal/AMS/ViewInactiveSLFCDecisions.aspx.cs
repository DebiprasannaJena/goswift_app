using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SingleWindow_ViewInactiveSLFCDecisions : System.Web.UI.Page
{
    DataTable dt = null;
    Agenda objcs = new Agenda();
    AMS objams = new AMS();
    String strval = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                fillGrid();
            }
        }
    }
    private void fillGrid()
    {
        try
        {
            dt = new DataTable();
            dt = AMServices.FillInactiveTermsConditions();
            grdViwComments.DataSource = dt;
            grdViwComments.DataBind();

            if (grdViwComments.Rows.Count > 0)
            {
                btnSubmit.Enabled = true;
                Label1.Visible = false;
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Enabled = false;
                Label1.Text = "No Records found.";
                btnSubmit.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objcs = null; dt = null; }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        string URL = "ViewSLFCDecisionMaster.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "";
        Response.Redirect(URL);
    }
    #region All Button Click...
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            grdViwComments.PageIndex = 0;
            grdViwComments.AllowPaging = false;
            fillGrid();
        }
        else
        {
            lbtnAll.Text = "All";
            grdViwComments.AllowPaging = true;
            fillGrid();
        }
    }
    #endregion
    #region Page Index
    protected void gdvTheme_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdViwComments.PageIndex = e.NewPageIndex;
        fillGrid();
    }
    #endregion

    #region Display Paging....
    protected void DisplayPaging(int intRecCount)
    {
        if (grdViwComments.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (grdViwComments.PageIndex + 1 == grdViwComments.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + grdViwComments.Rows[0].Cells[0].Text + "</b> - <b>" + intRecCount + "</b> Of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + grdViwComments.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(grdViwComments.Rows[0].Cells[0].Text) + (grdViwComments.PageSize - 1)) + "</b> Of <b>" + intRecCount + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
    }
    #endregion
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string cbSelect = "";
        for (int i = 0; i <= grdViwComments.Rows.Count - 1; i++)
        {
            CheckBox chkSel = (CheckBox)grdViwComments.Rows[i].FindControl("chkSel");
            if (chkSel.Checked == true)
                cbSelect += grdViwComments.DataKeys[i].Values[0].ToString() + "_";

        }
        foreach (GridViewRow row in grdViwComments.Rows)
        {
            CheckBox chkStatus = (CheckBox)row.FindControl("chkSel");
            if (chkStatus.Checked == true)
            {
                int id = Convert.ToInt32(grdViwComments.DataKeys[row.RowIndex].Values[1]);
                objcs.Id = id;
                objcs.Action = "UIC";//Update Inactive Comments 
                strval = AMServices.ActiveSLFCComments(objcs);
               
            }
        }
        if (strval == "9")
        {
            ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Condition Activated Successfully.');location.href='ViewInactiveSLFCDecisions.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&RNum=" + Request.QueryString["RNum"] +"'</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Please Select a Condition.');</script>", false);
        }
    }
}