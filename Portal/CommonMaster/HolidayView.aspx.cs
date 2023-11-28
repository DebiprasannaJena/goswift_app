//******************************************************************************************************************
// File Name         : HolidayView.aspx
// Description       : This page is to view all holiday details 
// Created by        : Dharmasis sahoo
// Created On        : 06th Dec 2021
// Modification History:
//                                                          
//<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>
//
//********************************************************************************************************************
#region namespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

#endregion
public partial class Portal_CommonMaster_HolidayView : System.Web.UI.Page
{
    #region for global veriable
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
    DataTable dt1 = new DataTable();
    decimal Total = 0;
    #endregion
    #region for page load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillYearDropdown();
            FillGrid();
        }
    }
    #endregion


    /// <summary>
    /// This function is used to fill gridview data
    /// </summary>
    private void FillGrid()
    {
        dt1 = new DataTable();
        try
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_HOLIDAY_MASTER_VIEW";

            cmd.Parameters.AddWithValue("@P_VCH_YEAR", DdlYear.SelectedValue);
            cmd.Parameters.AddWithValue("@P_VCH_ACTION", "A");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt1);

            Gv_Holiday_Details.DataSource = dt1;
            Gv_Holiday_Details.DataBind();

            LblTotalHolidays.Text = "Total holiday(s) for the year " + DdlYear.SelectedItem.Text + " :- " + "<span style='color: red;font-size:16px;font-weight:900;'>" + Total + "</span>";
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Holiday");
        }
    }

    /// <summary>
    /// This function is used to fill year starting from year 2017 to till current year + 1;
    /// </summary>
    private void FillYearDropdown()
    {
        try
        {
            for (int year = DateTime.Now.Year + 1; year >= 2017; year--)
            {
                DdlYear.Items.Add(new ListItem(year.ToString()));
            }

            DdlYear.SelectedValue = DateTime.Now.Year.ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Holiday");
        }
    }



    #region For edit , update and delete the gridview data
    protected void Gv_Holiday_Details_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            Gv_Holiday_Details.EditIndex = e.NewEditIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Holiday");
        }
    }

    protected void Gv_Holiday_Details_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            Gv_Holiday_Details.EditIndex = -1;
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Holiday");
        }
    }

    /// <summary>
    /// This function is use to update the gridview data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void Gv_Holiday_Details_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int intHolidayId = Convert.ToInt16(Gv_Holiday_Details.DataKeys[e.RowIndex].Values["HolidayId"].ToString());
            TextBox txtHolidayTitle = Gv_Holiday_Details.Rows[e.RowIndex].FindControl("TxtHolidayTitle") as TextBox;
            TextBox txtHolidayFrom = Gv_Holiday_Details.Rows[e.RowIndex].FindControl("TxtHolidayFrom") as TextBox;
            TextBox txtHolidayTo = Gv_Holiday_Details.Rows[e.RowIndex].FindControl("TxtHolidayTo") as TextBox;
            TextBox txtDescription = Gv_Holiday_Details.Rows[e.RowIndex].FindControl("TxtDescription") as TextBox;

            #region Validation

            if (txtHolidayTitle.Text.Trim() == "")
            {
                txtHolidayTitle.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter holiday title.</strong>');", true);
                return;
            }
            if (txtHolidayFrom.Text.Trim() == "")
            {
                txtHolidayFrom.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter holiday from date.</strong>');", true);
                return;
            }
            if (txtHolidayTo.Text.Trim() == "")
            {
                txtHolidayTo.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter holiday to date .</strong>');", true);
                return;
            }
            if (Convert.ToDateTime(txtHolidayFrom.Text.Trim()) > Convert.ToDateTime(txtHolidayTo.Text.Trim()))
            {
                txtHolidayFrom.Focus();
                txtHolidayTo.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong> Holiday from date should not be greater than holiday to date .</strong>');", true);
                return;
            }
            if (txtDescription.Text.Trim() == "")
            {
                txtDescription.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter holiday Description.</strong>');", true);
                return;
            }

            #endregion


            /*---------------------------------------------------------*/
            ///Data updation process started.
            /*---------------------------------------------------------*/
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_HOLIDAY_MASTER_AED";

            cmd.Parameters.AddWithValue("@P_VCH_HOLIDAY_TITLE", txtHolidayTitle.Text.Trim());
            cmd.Parameters.AddWithValue("@P_VCH_HOLIDAY_FROM", txtHolidayFrom.Text.Trim());
            cmd.Parameters.AddWithValue("@P_VCH_HOLIDAY_TO", txtHolidayTo.Text.Trim());
            cmd.Parameters.AddWithValue("@P_VCH_DESCRIPTION", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@P_INT_HOLIDAY_ID", intHolidayId);
            cmd.Parameters.AddWithValue("@P_VCH_ACTION", "U");

            SqlParameter par = new SqlParameter("@P_VCH_MSG_OUT", SqlDbType.VarChar, 104);
            par.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par);

            cmd.ExecuteNonQuery();

            string result = cmd.Parameters["@P_VCH_MSG_OUT"].Value.ToString();
            if (result == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Holiday already exists .</strong>');", true);
                return;
            }
            else if (result == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Holiday updated Successfully</strong>');", true);
                Gv_Holiday_Details.EditIndex = -1;
                FillGrid();
                return;
            }
            else if (result == "3")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Something went wrong. Please try again.</strong>');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Holiday");
        }
    }

    /// <summary>
    /// This function is used to delete data from gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            int intHolidayId = Convert.ToInt16(Gv_Holiday_Details.DataKeys[row.RowIndex].Values["HolidayId"].ToString());

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_HOLIDAY_MASTER_AED";

            cmd.Parameters.AddWithValue("@P_VCH_ACTION", "D");
            cmd.Parameters.AddWithValue("@P_INT_HOLIDAY_ID", intHolidayId);

            SqlParameter par = new SqlParameter("@P_VCH_MSG_OUT", SqlDbType.VarChar, 104);
            par.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par);

            cmd.ExecuteNonQuery();

            string result = cmd.Parameters["@P_VCH_MSG_OUT"].Value.ToString();
            if (result == "2")
            {
                FillGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Holiday removed successfully</strong>');", true);
                return;
            }
            else if (result == "3")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Something went wrong. Please try again.</strong>');", true);
                return;
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Holiday");
        }
    }
    #endregion



    /// <summary>
    ///  for filling gridview according to respective year
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DdlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Gv_Holiday_Details.EditIndex = -1;
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Holiday");
        }
    }

    protected void Gv_Holiday_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                TextBox TxtHolidayFrom = (TextBox)e.Row.FindControl("TxtHolidayFrom");
                TextBox TxtHolidayTo = (TextBox)e.Row.FindControl("TxtHolidayTo");

                TxtHolidayFrom.Attributes.Add("readonly", "readonly");
                TxtHolidayTo.Attributes.Add("readonly", "readonly");
            }

            Label LblNoOfDays = e.Row.FindControl("LblNoOfDays") as Label;
            int intNoOfDays = Convert.ToInt32(LblNoOfDays.Text);

            if (intNoOfDays > 1 || intNoOfDays <= 0)
            {
                LblNoOfDays.ForeColor = System.Drawing.Color.Red;
            }

            Total += Convert.ToDecimal(LblNoOfDays.Text);
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "<b>Total</b>";
            e.Row.Cells[4].Text = string.Format("{0}", Total);
            e.Row.Cells[4].Font.Bold = true;
        }
    }
}