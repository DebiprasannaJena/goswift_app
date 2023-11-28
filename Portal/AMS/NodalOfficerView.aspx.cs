//******************************************************************************************************************
// File Name             :   SingleWindow/NodalOfficerView.aspx
// Description           :   To View Tagged Nodal Officers & SLFC Member
// Created by            :   Tapan Kumar Mishra
// Created on            :   18-July-2016
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_NodalOfficerView : System.Web.UI.Page
{
    #region "Member Variable"
    Agenda objcs = null;
    DataTable dt;
    public int intRecCount { get; set; }

    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["Userid"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }
    }
    #endregion

    #region "Fill Grid"

    private void FillGrid()
    {
        objcs = new Agenda();
        try
        {
            objcs.Action = "V";
            //objcs.OfficerType = 0;
            
            dt = new DataTable();
            dt = AMServices.ViewOfficers(objcs);
            grdOfficers.DataSource = dt;
            grdOfficers.DataBind();
            intRecCount = dt.Rows.Count;
            if (grdOfficers.Rows.Count == 0)
            {
                divpaging.Visible = false;
                lblMessage.Visible = true;
            }
            else
            {
                divpaging.Visible = true;
                lblMessage.Visible = false;
                DisplayPaging();
            }

        }
        catch (Exception)
        {

            Response.Redirect("~/CustomError.aspx", false);
        }
        finally
        {
            dt = null;
            objcs = null;
        }


    }
    #endregion

    #region "GRID VIEW EVENTS"

    protected void grdOfficers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "E")
            {
                String url = "";
                url = "NodalOfficerAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + e.CommandArgument + "&PgIndex=" + Admin.CommonFunction.CommonFunction.EncryptData(grdOfficers.PageIndex.ToString()) + "";
                Response.Redirect(url, false);
                Context.ApplicationInstance.CompleteRequest();
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {           
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        int[] a = new int[3] { 1, 2, 3 };
        for (int i = grdOfficers.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = grdOfficers.Rows[i];
            GridViewRow previousRow = grdOfficers.Rows[i - 1];
            foreach (var j in a)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        if (j == 1)
                        {
                            if (row.Cells[4].RowSpan == 0)
                            {
                                previousRow.Cells[4].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[4].RowSpan = row.Cells[4].RowSpan + 1;
                            }
                            row.Cells[4].Visible = false;

                        }

                        row.Cells[j].Visible = false;
                    }
                }
            }
        }
    }

    #endregion

    //protected void btnShow_Click(object sender, EventArgs e)
    //{
    //    if (txtFromDate.Text != "")
    //    {
    //        FillGrid();
    //    }
    //    else
    //        ScriptManager.RegisterStartupScript(btnShow, this.GetType(), "UR", "alert('Please Choose Date')", true);
    //}
      
    #region "Paging"
    #region "All button click event"
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            lbtnAll.ToolTip = "Paging";
            this.grdOfficers.PageIndex = 0;
            grdOfficers.AllowPaging = false;
        }
        else
        {
            lbtnAll.Text = "All";
            lbtnAll.ToolTip = "All";
            grdOfficers.AllowPaging = true;
        }
        FillGrid();
    }
    #endregion

    #region "Display Paging...."
    protected void DisplayPaging()
    {
        try
        {

            if (this.grdOfficers.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                lbtnAll.Visible = true;
            }
            if (this.grdOfficers.PageIndex + 1 == this.grdOfficers.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + ((Label)grdOfficers.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + intRecCount + "</b> of <b>" + intRecCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + ((Label)grdOfficers.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)grdOfficers.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((grdOfficers.PageSize - 1))) + "</b> of <b>" + intRecCount + "</b>";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region "GridView pageindex changing event"
    protected void grdOfficers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdOfficers.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    #endregion
    #endregion

   

    
}