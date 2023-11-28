﻿using System;
using System.Data;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Net;


public partial class SingleWindow_AddFinancingDtls : System.Web.UI.Page
{
    DataTable dt = null;
    AMS objams = new AMS();
    Agenda objcs = new Agenda();
    string strval = null;
    static int rowIndex = -1;
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Save")
        {
            objams.Action = "AF";
            objams.FinDtls = txtFinDtls.Text;
            objams.FinID =0;
            objams.CreatedBy = Convert.ToInt32(Session["UserId"]);
            strval = AMServices.AddFinDtls(objams);
            Response.Write("<script>alert('Data Added Successfully.');document.location.href='AddFinancingDtls.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&pageIndex=" + Request.QueryString["PageNo"] + "'</script>");
        }
        if (btnSubmit.Text == "Update")
        {
            objams.Action = "S";
            objams.FinDtls = txtFinDtls.Text;
            objams.FinID = Convert.ToInt32(hdnKey.Value);
            objams.CreatedBy = Convert.ToInt32(Session["UserId"]);
            strval = AMServices.AddFinDtls(objams);
            Response.Write("<script>alert('Data Updated Successfully.');document.location.href='AddFinancingDtls.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&pageIndex=" + Request.QueryString["PageNo"] + "'</script>");
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        string URL = "AddFinancingDtls.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "";
        Response.Redirect(URL);
    }
    private void fillGrid()
    {
        try
        {
            dt = new DataTable();
            dt = AMServices.FillFinDtls();
            grdViwCostDtls.DataSource = dt;
            grdViwCostDtls.DataBind();

            if (grdViwCostDtls.Rows.Count > 0)
                BtnInactive.Visible = true;
            else
                BtnInactive.Visible = false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { dt = null; }
    }

    protected void grdViwCostDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int status = Convert.ToInt32(grdViwCostDtls.DataKeys[e.Row.RowIndex].Value);
            if (status == 1)
            {
                CheckBox chkStatus = (CheckBox)e.Row.FindControl("chkSel");
                chkStatus.Checked = true;
            }
        }
    }
    protected void BtnInactive_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdViwCostDtls.Rows)
        {
            CheckBox chkStatus = (CheckBox)row.FindControl("chkSel");
            if (chkStatus.Checked == true)
            {
                int id = Convert.ToInt32(grdViwCostDtls.DataKeys[row.RowIndex].Values[1]);
                objcs.Id = id;
                objcs.Action = "FC";
                strval = AMServices.ActiveFinDescription(objcs);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Exists", "alert('Data Saved Successfully');", true);
            }
            else
            {
                int id = Convert.ToInt32(grdViwCostDtls.DataKeys[row.RowIndex].Values[1]);
                objcs.Id = id;
                objcs.Action = "UF";
                strval = AMServices.ActiveFinDescription(objcs);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Exists", "alert('Data Saved Successfully');", false);
            }
        }
    }

    protected void grdViwCostDtls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objams = new AMS();
        try
        {
            if (e.CommandName == "E")
            {
                rowIndex = Convert.ToInt32(e.CommandArgument.ToString());

                txtFinDtls.Text = grdViwCostDtls.Rows[rowIndex - 1].Cells[2].Text.ToString();
                hdnKey.Value = rowIndex.ToString();
                btnSubmit.Text = "Update";
            }
        }
        catch
        {
        }
    }
}