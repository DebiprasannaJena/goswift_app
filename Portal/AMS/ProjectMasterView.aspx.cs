//******************************************************************************************************************
// File Name             :   SingleWindow/ProjectMasterView.aspx
// Description           :   To View project details
// Created by            :   
// Created on            :   
// Modification History  :
//       <CR no.>                      <Date>                    <Modified by>                <Modification Summary>'                                                          
//         1.                          20-July-2016              Tapan Kumar Mishra             Create Row Databound event and details page
//********************************************************************************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_ProjectMasterView : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    DataTable dt = null;
    string strVal = "";
    int gIntRowsCount;
    static int intType = 0;
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
                intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
                if (grdProjmst.AllowPaging == true)
                {
                    grdProjmst.PageIndex = string.IsNullOrEmpty(Request.QueryString["PIndex"]) ? 0 : Convert.ToInt32(Request.QueryString["PIndex"]);
                }
                FillGrid();
            }
        }
    }

    #endregion

    #region "Fill Grid"

    private void FillGrid()
    {
        AMS objams = new AMS();

        objams.Action = "V";
        objams.OfficerId = Convert.ToInt32(Session["UserId"]);
        objams.TypeId = intType;
        dt = new DataTable();
        dt = AMServices.ViewProjectMaster(objams);
        grdProjmst.DataSource = dt;
        grdProjmst.DataBind();
        gIntRowsCount = dt.Rows.Count;
        if (gIntRowsCount > 0)
        {
            DisplayPaging();
            lblMessage.Visible = false;
        }
        else
        {
            lblMessage.Visible = true;
            lbtnAll.Visible = false;
            lblPaging.Visible = false;
        }
    }

    #endregion
    
    #region "Paging"
    private void DisplayPaging()
    {
        if (this.grdProjmst.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
        }
        if (this.grdProjmst.PageIndex + 1 == this.grdProjmst.PageCount)
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + gIntRowsCount + "</b> of <b>" + gIntRowsCount + "</b>";
        }
        else
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((grdProjmst.PageSize - 1))) + "</b> of <b>" + gIntRowsCount + "</b>";
        }

    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "View Paging";
            grdProjmst.AllowPaging = false;
            grdProjmst.PageIndex = 0;
        }
        else
        {
            lbtnAll.Text = "All";
            grdProjmst.AllowPaging = true;
        }
        FillGrid();
    }
    protected void grdProjmst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdProjmst.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    #endregion

    #region "Gridview Command"

    protected void grdProjmst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objams = new AMS();
        try
        {

            if (e.CommandName == "D")
            {
                objams.Action = "D";
                objams.CreatedBy = Convert.ToInt32(Session["UserId"]);
                objams.ProjectId = Convert.ToInt32(e.CommandArgument);
                objams.ApplicationDate = DateTime.Now;

                strVal = AMServices.AddProjectMaster(objams);
                if (strVal == "3")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "alert('Project deleted successfully');", true);
                    FillGrid();
                }
            }
            else if (e.CommandName == "E")
            {
                var s = Convert.ToString(e.CommandArgument).Split(',');
                if (s.Count() == 1)
                {
                    objams.ProjectId = Convert.ToInt32(0);
                }
                Session["PType"] = null;
                objams.ProjectId = Convert.ToInt32(s[0]);
                Response.Redirect("ProjectMasterAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + objams.ProjectId + "&PIndex=" + grdProjmst.PageIndex.ToString() + "", false);
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null; }

    }
       
    protected void grdProjmst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //e.Row.Cells[1].Text = (this.grdProjmst.PageIndex * this.grdProjmst.PageSize) + e.Row.RowIndex + 1;
            int intProposalId = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[0]);
            int intDetailId = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[1]);
            int intFinanceId = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[2]);
            int intStaus = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[3]);
            int intDecisionId = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[4]);
            int intFinDoc = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[5]);
         
            int intReOpenStatus = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[6]);

            if (intProposalId == 0)
                e.Row.Cells[6].Text = "";

            if (intFinanceId == 0)
                e.Row.Cells[7].Text = "";

            if (intFinDoc == 0)
                e.Row.Cells[8].Text = "";

            if (intDetailId == 0)
                e.Row.Cells[9].Text = "";

            if (intDecisionId == 0)
                e.Row.Cells[10].Text = "";

            if(intType==1 & intStaus != 0)
                e.Row.Cells[11].Text = "";  
            else if(intType!=1 & intStaus == 1)
                 e.Row.Cells[11].Text = "";
            else if (intType == 4 & (intStaus == 5 || intStaus>=7))
                e.Row.Cells[11].Text = "";
      
            if (intReOpenStatus == 1)
            {
                e.Row.Style.Add("border-left", "4px solid #ff4348");
            }
           
        }
    }

    #endregion
}
