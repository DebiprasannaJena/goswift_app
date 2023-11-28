using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Portal_Dashboard_AMSDashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }
        if (Session["userid"] != null)
        {
            if (!IsPostBack)
            {
                divFeedback.Visible = false;
                divAgendaRequest.Visible = false;
                divPendingProjStatus.Visible = false;
                int intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));

                if (intType == 2) //SLFC Members
                {
                    divFeedback.Visible = true;
                    FillGrid();
                }
                if (intType == 1) //Nodal officers
                {
                    divAgendaRequest.Visible = true;
                    divPendingProjStatus.Visible = true;
                    FillForwardProj();
                }
            }
        }
        else
        {
            Response.Redirect("../Default.aspx");
        }
    }

    private void FillGrid()
    {
        AMS objams = new AMS();
        objams.Action = "DB";
        objams.OfficerId = Convert.ToInt32(Session["UserId"]);
        DataTable dt = new DataTable();
        dt = AMServices.ViewProjectMaster(objams);
        grdFeedback.DataSource = dt;
        grdFeedback.DataBind();
        if (dt.Rows.Count > 0)
        {
            lblMessage.Visible = false;
        }
        else
        {
            lblMessage.Visible = true;
        }
    }

    private void FillForwardProj()
    {
        AMS objams = new AMS();

        objams.Action = "VFPN";
        objams.OfficerId = Convert.ToInt32(Session["UserId"]);

        DataTable dt = new DataTable();
        dt = AMServices.FillForwardProject(objams);
        grdForwardProj.DataSource = dt;
        grdForwardProj.DataBind();
        if (dt.Rows.Count > 0)
        {
            lblMessage1.Visible = false;
        }
        else
        {
            lblMessage1.Visible = true;
        }
    }

    private void FillProjStatus()
    {
        AMS objams = new AMS();

        objams.Action = "VPS";
        objams.OfficerId = Convert.ToInt32(Session["UserId"]);
        int intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
        objams.TypeId = intType;
        DataSet ds = new DataSet();
        ds = AMServices.ViewProjectSts(objams);
        grdProjSts.DataSource = ds.Tables[0];
        grdProjSts.DataBind();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsgSts.Visible = false;
        }
        else
        {
            lblMsgSts.Visible = true;
        }
    }


    protected void grdFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdFeedback.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void grdFeedback_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int intFinDoc = Convert.ToInt32(grdFeedback.DataKeys[e.Row.RowIndex].Values[0]);
            int intReOpenStatus = Convert.ToInt32(grdFeedback.DataKeys[e.Row.RowIndex].Values[1]);
            if (intFinDoc == 0)
                e.Row.Cells[6].Text = string.Empty;
            if (intReOpenStatus == 1)
            {
                e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[7].BackColor = System.Drawing.Color.Red;

            }
        }

    }

    protected void grdForwardProj_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdForwardProj.PageIndex = e.NewPageIndex;
        FillForwardProj();
    }

    protected void grdProjSts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdProjSts.PageIndex = e.NewPageIndex;
        FillProjStatus();
    }

    protected void grdForwardProj_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        AMS objams = new AMS();
        try
        {
            if (e.CommandName == "E")
            {
                objams.ProjectId = Convert.ToInt32(e.CommandArgument);
                Session["PType"] = "2";
                Response.Redirect("../AMS/ProjectMasterAdd.aspx?ID=" + objams.ProjectId + "&PIndex=" + grdForwardProj.PageIndex.ToString() + "", false);
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null; }

    }
}