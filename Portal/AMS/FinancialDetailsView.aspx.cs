//******************************************************************************************************************
// File Name             :   SingleWindow/FinanceDetailsView.aspx.cs
// Description           :   To View Finance Details to Accountant
// Created by            :   Surya Prakash Barik
// Created on            :   30-OCT-2017
// Modification History  :
//       <CR no.>                      <Date>                    <Modified by>                <Modification Summary>'                                                          

//********************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_FinancialDetailsView : System.Web.UI.Page
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
            FEtxtComment.ValidChars = FEtxtComment.ValidChars + "\r\n";
        }
       
    }
    #endregion

    private string TextCheck1(TextBox txt, Label lbl, int Length)
    {
        try
        {
            int count = Convert.ToInt32(txt.Text.Length);
            double diff = Length - Convert.ToInt32(count);
            lbl.Text = Convert.ToString(diff);
            return lbl.Text;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally {}
    }

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
            if (e.CommandName == "Comment")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                int id = Convert.ToInt32(grdProjmst.DataKeys[rowIndex].Values[0]);

                ViewState["Agenda_Id"] = id;
                               
                objams.Action = "VAC";
                objams.ProjectId = id;
                dt = new DataTable();
                dt = AMServices.FillAccountantComment(objams);
               
                if (dt.Rows[0]["VCH_ACCOUNTANT_REMARK"].ToString() != "")
                    txtComment.Text = dt.Rows[0]["VCH_ACCOUNTANT_REMARK"].ToString();
                else
                    txtComment.Text="";
                string strCnt=TextCheck1(txtComment, lblCnt, 500);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "CModal('" + id + "' , 'Accountant Comment','" + txtComment.Text + "','" + strCnt + "') ;", true);

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
                int intFinDoc = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[1]);

                if (intFinDoc == 0)
                    e.Row.Cells[5].Text = "";
            } 
        }

    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AMS objams = new AMS();

        objams.Action = "AC";
        objams.OfficerId = Convert.ToInt32(Session["UserId"]);
        objams.ProjectId = Convert.ToInt32(hdnId.Value);
        objams.Remark = txtComment.Text.Trim();

        strVal = AMServices.InsertAccountantComment(objams);
        if(strVal=="2")
             ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "<script>alert('Comment Saved Successfully.');location.href='FinancialDetailsView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "'</script>", false);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "<script>alert('Error Occurred!!!');location.href='FinancialDetailsView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&RNum=" + Request.QueryString["RNum"] + "'</script>", false);

    }
}