using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.GrievanceEntity;

public partial class Portal_Grievance_ViewGrievanceSubType : System.Web.UI.Page
{

    #region for global veriable 

    GrievanceServices obj = new GrievanceServices();
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
                BindGrievanceType();
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
        DataTable dt = new DataTable();
        try
        {
            GrievanceServices objBAL = new GrievanceServices();
            GrievanceEntity objGrivEntity = new GrievanceEntity();
            objGrivEntity.StrAction = "VGST";

            dt = objBAL.ViewGrivSubTypeDetails(objGrivEntity);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            intRecordCount = Convert.ToInt32(dt.Rows.Count);
            DisplayPaging();
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            dt = null;
        }
    }

    #endregion


    #region Dropdown for Grievance type 
    private void BindGrievanceType()
    {
        try
        {
            GrievanceEntity objSearch = new GrievanceEntity()
            {
                StrAction = "BGTF"
            };
            DataTable Dt = obj.ViewGrivSubTypeDetails(objSearch);
            ddl_griv_type.DataSource = Dt;
            ddl_griv_type.DataTextField = "vchGrivType";
            ddl_griv_type.DataValueField = "intGrivTypeId";
            ddl_griv_type.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddl_griv_type.Items.Insert(0, list);
        }
        catch (Exception X)
        {
            throw X;
        }


    }

    #endregion

    protected void btn_griv_edit_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Response.Redirect("AddGrievanceSubType.aspx?intGrivSubTypeId=" + btn.CommandArgument);
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            GrievanceServices objBAL = new GrievanceServices();
            GrievanceEntity objGrivEntity = new GrievanceEntity();
            objGrivEntity.StrAction = "VGST";
            objGrivEntity.intGrivTypeId = Convert.ToInt32(string.IsNullOrEmpty(ddl_griv_type.SelectedItem.Value) ? null : ddl_griv_type.SelectedItem.Value); ;
            dt = objBAL.ViewGrivSubTypeDetails(objGrivEntity);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            intRecordCount = Convert.ToInt32(dt.Rows.Count);
            DisplayPaging();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region FOR  PAGING 

    private void DisplayPaging()
    {
        if (GridView1.Rows.Count > 0)
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
            Util.LogError(ex, "Grievance");
        }
    }

    #endregion
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = Convert.ToString((this.GridView1.PageIndex * this.GridView1.PageSize) + e.Row.RowIndex + 1);

            HiddenField subtype_status = (HiddenField)e.Row.FindControl("Hid_Griev_SubType_Status");
            Label grid_status = (Label)e.Row.FindControl("Lbl_Griv_stat");
            if(subtype_status.Value.ToUpper()=="ACTIVE")
            {
                grid_status.ForeColor = System.Drawing.Color.Green;
            }
            else if(subtype_status.Value.ToUpper() == "INACTIVE")
            {
                grid_status.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}