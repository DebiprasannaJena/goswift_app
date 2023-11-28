using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;

public partial class SingleWindow_ViewClarification : System.Web.UI.Page
{
    AMS objams = new AMS();
    string strval = null;
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
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    ViewComments();
                }
            }
        }
      
    }
    #region for view comments
    public void ViewComments()
    {
        try
        {
            objams.Action = "V";
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            DataSet ds = new DataSet();
            ds = AMServices.ViewComments(objams);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdComments.DataSource = ds.Tables[0];
                    grdComments.DataBind();
                   
                }
                
                if (ds.Tables[1].Rows.Count > 0)
                {
                    grdClarification.DataSource = ds.Tables[1];
                    grdClarification.DataBind();
                }
                
            }
            else
            {
                
            }
        }
        catch (Exception m) { Response.Write(m.Message); }
    }
    #endregion
    //protected void BtnUpdateFeedback_Click(object sender, EventArgs e)
    //{
    //    string Feedback = "";
    //    for (int i = 0; i < grdComments.Rows.Count; i++)
    //    {
    //        TextBox txtFeedback = (TextBox)grdComments.Rows[i].FindControl("TxtFeedback");
    //        Feedback = Feedback + txtFeedback.Text + "~";
    //    }
    //    objams.COMMENT = Feedback;
    //    objams.Action = "UF";
    //    objams.ProjectId = Convert.ToInt32(Request.QueryString["ProjectID"]);
    //    strval = AMServices.UpdateFeedback(objams);
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert(' Feedback Details Updated Successfully');", true);

    //}
    //protected void BtnUpdate_Click(object sender, EventArgs e)
    //{
    //    string Comment = "";
    //    for (int i = 0; i < grdClarification.Rows.Count; i++)
    //    {
    //        TextBox txtComment = (TextBox)grdClarification.Rows[i].FindControl("TxtComments");
    //        Comment = Comment + txtComment.Text + "~";
    //    }
    //    objams.COMMENT = Comment;
    //    objams.Action = "U";
    //    objams.ProjectId = Convert.ToInt32(Request.QueryString["ProjectID"]);
    //    strval = AMServices.UpdateClarification(objams);
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert(' Clarification Details Updated Successfully ');", true);
    //}

    protected void grdComments_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdComments.EditIndex = e.NewEditIndex;
        ViewComments();
    }
    protected void grdComments_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int Id = Convert.ToInt32(grdComments.DataKeys[e.RowIndex].Value.ToString());
        GridViewRow row = (GridViewRow)grdComments.Rows[e.RowIndex];
        TextBox txtFeedback = (TextBox)row.FindControl("txtFeedback");
        objams.Action = "UF";
        objams.Feedback = txtFeedback.Text;     
        objams.FeedbackId = Id;
        strval = AMServices.UpdateFeedback(objams);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert(' Feedback Details Updated Successfully');", true);
        grdComments.EditIndex = -1;
        ViewComments();
    }
    protected void grdComments_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdComments.EditIndex = -1;
        ViewComments();
    }

    protected void grdComments_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Type = Convert.ToInt32(grdComments.DataKeys[e.Row.RowIndex].Values[1]);
            LinkButton EditButton = (LinkButton)e.Row.Cells[3].Controls[0];
            if (Type == 1)
            {
                EditButton.Visible = false;
            }
            else
            {
                EditButton.Visible = true;
            }
        }   

    }

    protected void grdClarification_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Type = Convert.ToInt32(grdClarification.DataKeys[e.Row.RowIndex].Values[1]);
            LinkButton EditButton = (LinkButton)e.Row.Cells[3].Controls[0];
            if (Type == 1)
            {
                EditButton.Visible = false;
            }
            else
            {
                EditButton.Visible = true;
            }
        }

    }

    protected void grdClarification_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdClarification.EditIndex = e.NewEditIndex;
        ViewComments();
    }
    protected void grdClarification_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        int Id = Convert.ToInt32(grdClarification.DataKeys[e.RowIndex].Value.ToString());
        GridViewRow row = (GridViewRow)grdClarification.Rows[e.RowIndex];
        TextBox TxtClarification1 = (TextBox)row.FindControl("TxtClarification");
        objams.Action = "U";
        objams.Clarification = TxtClarification1.Text;
        objams.ClarificationId = Id;
        strval = AMServices.UpdateClarificationGM(objams);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert(' Clarification Details Updated Successfully');", true);
        grdClarification.EditIndex = -1;
        ViewComments();
    }
    protected void grdClarification_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdClarification.EditIndex = -1;
        ViewComments();
    }
}