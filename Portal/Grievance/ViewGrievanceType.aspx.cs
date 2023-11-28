using EntityLayer.GrievanceEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_Grievance_ViewGrievanceType : System.Web.UI.Page
{

    #region for global veriable 

    int intRecordCount = 0;

    #endregion


    #region for page load 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx", false);
            return;
        }

        if (!IsPostBack)
        {
            try
            {
                BindGridView();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Grievance");
            }
        }
    }

    #endregion


    #region for Grid view data bind 
    private void BindGridView()
    {
        try
        {
            GrievanceServices objBAL = new GrievanceServices();
            GrievanceEntity objGrivEntity = new GrievanceEntity();
            objGrivEntity.StrAction = "VGT";
            DataTable dt = new DataTable();
            dt = objBAL.ViewGrivTypeDetails(objGrivEntity);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region For Gridview edit  button click 
    protected void btn_griv_edit_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Response.Redirect("AddGrievanceType.aspx?intGrivTypeId=" + btn.CommandArgument);
    }

    #endregion

    #region for Gridview pageing
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    #endregion


    #region For Gridview row Data bound
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.GridView1.PageIndex * this.GridView1.PageSize) + e.Row.RowIndex + 1); // to asiend  no of row count values 

            int intGrivId = Convert.ToInt32(GridView1.DataKeys[e.Row.RowIndex].Values["intGrivTypeId"]);
            Button BtnEdit = (Button)e.Row.FindControl("btn_griv_edit");
            HiddenField griv_status = (HiddenField)e.Row.FindControl("Hid_Griev_Type_Status");
            Label lab_griv_status = (Label)e.Row.FindControl("Lbl_Griv_stat");

            if (griv_status.Value.ToUpper() == "ACTIVE")
            {
                lab_griv_status.ForeColor = System.Drawing.Color.Green;
            }
            else if (griv_status.Value.ToUpper() == "INACTIVE")
            {
                lab_griv_status.ForeColor = System.Drawing.Color.Red;
            }

            if (intGrivId == 1 || intGrivId == 2)
            {
                BtnEdit.Visible = false;
            }
            else
            {
                BtnEdit.Visible = true;
            }
        }
    }

    #endregion

    #region  for pagging
    private void DisplayPaging()
    {
        if(GridView1.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            if (GridView1.PageIndex + 1 == GridView1.PageCount)
            {
                this.lblPaging.Text = "Results <b>" + GridView1.Rows[0].Cells[0].Text + "</b> - <b>" + intRecordCount + "</b> Of <b>" + intRecordCount + "</b>";
            }
            else
            {
                this.lblPaging.Text = "Results <b>" + GridView1.Rows[0].Cells[0].Text + "</b> - <b>" + (int.Parse(GridView1.Rows[0].Cells[0].Text) + (GridView1.PageSize - 1)) + "</b> Of <b>" + (intRecordCount) + "</b>";
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
        
    }


    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                GridView1.PageIndex = 0;
                GridView1.AllowPaging = false;
                BindGridView();
            }
            else
            {
                lbtnAll.Text = "All";
                GridView1.AllowPaging = true;
                BindGridView();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "viewGrievanceType");
        }
    }




    #endregion
}