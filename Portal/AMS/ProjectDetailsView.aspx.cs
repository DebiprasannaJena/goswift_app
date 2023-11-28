//******************************************************************************************************************
// File Name             :   SingleWindow/ProjectDetailsView.aspx
// Description           :   To Add project details against a project by Nodal Officer
// Created by            :   Tapan Kumar Mishra
// Created on            :   20-July-2016
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_ProjectDetailsView : System.Web.UI.Page
{
    #region "Member Variable"

    Agenda objcs = null;
    DataTable dt = null;
    Double x = 0;
    Double y = 0;

    #endregion

    #region "Page Load"
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
                FillDetails();
                FillFinDetails();
                FillMaterialDetails();

            }
        }
    }
    #endregion

    #region "Fill Details"

    public void FillDetails()
    {
        objcs = new Agenda();
        objcs.Action = "V";
        objcs.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
        dt = new DataTable();
        dt = AMServices.ViewProjectDetails(objcs);

        if (dt.Rows.Count > 0)
        {
            lblName.Text = dt.Rows[0]["VCHPROJCT_NAME"].ToString();
            lblFinDescription.Text = dt.Rows[0]["vchFinanceDescription"].ToString();
            lblLand.Text = dt.Rows[0]["vchLand"].ToString();
            lblWater.Text = dt.Rows[0]["vchWater"].ToString();
            lblPower.Text = dt.Rows[0]["vchPower"].ToString();
            if ((Convert.ToString(dt.Rows[0]["intPowerSource"].ToString()) == "1"))
                lblSource.Text = "CPP";
            else if ((Convert.ToString(dt.Rows[0]["intPowerSource"].ToString()) == "2"))
            {
                lblSource.Text = "GRID";
            }
            else if ((Convert.ToString(dt.Rows[0]["intPowerSource"].ToString()) == "3"))
            {
                lblSource.Text = "CPP & GRID";
            }
            lblMonths.Text = dt.Rows[0]["vchImplementPeriod"].ToString();
            lblDirectEmployment.Text = dt.Rows[0]["intEmployement"].ToString() + " Numbers";
            lblContractual.Text = dt.Rows[0]["intContractual"].ToString() + " Numbers";
            //Added by Monalisa nayak on 29-Dec-2016 to bind project cost details 
            GrdProjectCostDtls.DataSource = dt;
            GrdProjectCostDtls.DataBind();
            GrdProjectCostDtls.FooterRow.Cells[0].Text = "Total";
            var total = GrdProjectCostDtls.FooterRow.FindControl("lblGrandTotal") as Label;
            total.Text = x.ToString("N2");
        }
    }
    #endregion
    protected void GrdProjectCostDtls_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGrandTotal = (Label)e.Row.FindControl("lblGrandTotal");
            Label lblGrand = (Label)e.Row.FindControl("lblCost");
            x = x + Convert.ToDouble(lblGrand.Text);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Style["text-align"] = "right";
        }
    }
    public void FillFinDetails()
    {
        objcs = new Agenda();
        objcs.Action = "FD";
        objcs.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
        dt = new DataTable();
        dt = AMServices.ViewFinanceDetails(objcs);
        if (dt.Rows.Count > 0)
        {
            GrdFinDtls.DataSource = dt;
            GrdFinDtls.DataBind();
            GrdFinDtls.FooterRow.Cells[0].Text = "Total";
            var total = GrdFinDtls.FooterRow.FindControl("lblGTotal") as Label;
            total.Text = y.ToString("N2");
        }
    }
    protected void GrdFinDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGTotal = (Label)e.Row.FindControl("lblGTotal");
            Label lblGrand = (Label)e.Row.FindControl("lblAmount");
            y = y + Convert.ToDouble(lblGrand.Text);
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Style["text-align"] = "right";
        }
    }
    public void FillMaterialDetails()
    {
        objcs = new Agenda();
        objcs.Action = "R";
        objcs.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
        DataTable dt1 = new DataTable();
        dt1 = AMServices.ViewFinanceDetails(objcs);
        if (dt1.Rows.Count > 0)
        {
            GrvSource.DataSource = dt1;
            GrvSource.DataBind();
        }
    }
}