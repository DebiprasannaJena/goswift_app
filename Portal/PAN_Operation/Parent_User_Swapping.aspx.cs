using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using DWHServiceReference;
using System.Configuration;

public partial class Portal_PAN_Operation_Parent_User_Swapping : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    int intRetVal = 0;
    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                FillGrid();
                FillUnitName();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "UserSwap");
            }
        }
    }

    #region FunctionUsed

    /////// Fill Gridview
    private void FillGrid()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        try
        {
            int intInvestorId = 0;
            if (DrpDwn_Unit_Name.SelectedIndex > 0)
            {
                intInvestorId = Convert.ToInt32(DrpDwn_Unit_Name.SelectedValue);
            }

            /*------------------------------------------------------*/

            string strPAN = "";
            if (Txt_PAN.Text.Trim() != "")
            {
                strPAN = Txt_PAN.Text.Trim();
            }

            /*------------------------------------------------------*/

            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataTable objds = new DataTable();

            objCommand.CommandText = "USP_V2_PARENT_USER_SWAPPING";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", 'V');
            objCommand.Parameters.AddWithValue("@P_INT_INVESTOR_ID", intInvestorId);
            objCommand.Parameters.AddWithValue("@P_VCH_PAN", strPAN);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            GridView1.DataSource = objds;
            GridView1.DataBind();

            intRetVal = objds.Rows.Count;
            if (objds.Rows.Count > 0)
            {
                DisplayPaging();
            }
            else
            {
                LblPaging.Visible = false;
                LbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    /////// Fill Parent Unit Name
    private void FillUnitName()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        try
        {
            SqlCommand objCommand = new SqlCommand();
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataTable objds = new DataTable();

            objCommand.CommandText = "USP_V2_PARENT_USER_SWAPPING";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", 'F');

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            if (objds.Rows.Count > 0)
            {
                DrpDwn_Unit_Name.DataTextField = "VCH_INV_NAME";
                DrpDwn_Unit_Name.DataValueField = "INT_INVESTOR_ID";
                DrpDwn_Unit_Name.DataSource = objds;
                DrpDwn_Unit_Name.DataBind();
            }

            DrpDwn_Unit_Name.Items.Insert(0, new ListItem("-Select Unit-", "0"));
        }
        catch (Exception ex)
        {
          throw ex.InnerException;
        }
    }

    #endregion

    int intColorType = 1;
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           if (e.Row.RowType == DataControlRowType.DataRow)
           {
            Label Lbl_Parent_Id = (Label)e.Row.FindControl("Lbl_Parent_Id");
            Button Btn_Action = (Button)e.Row.FindControl("Btn_Action");

            if (Lbl_Parent_Id.Text == "0")
            {
                Btn_Action.Visible = false;

                if (intColorType == 1)
                {
                    intColorType = 2;
                }
                else if (intColorType == 2)
                {
                    intColorType = 1;
                }
            }

            if (intColorType == 1)
            {
                e.Row.BackColor = System.Drawing.Color.LightGray;
            }
            else if (intColorType == 2)
            {
                e.Row.BackColor = System.Drawing.Color.Transparent;
            }

            e.Row.Attributes.Add("onmouseover", "this.originalcolor=this.style.backgroundColor;this.style.backgroundColor='#FDCB0A';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalcolor;");
            e.Row.Attributes["style"] = "cursor:pointer";
           }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "UserSwap");
        }
    }

    #region ButtonClickEvents

    /////// Button Swap Action
    protected void Btn_Action_Click(object sender, EventArgs e)
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            Label Lbl_Invetsor_Id = (Label)row.FindControl("Lbl_Invetsor_Id");
            Label Lbl_New_User_Id = (Label)row.FindControl("Lbl_New_User_Id");

            /*-----------------------------------------------------------------*/
            /////// Service Initialization (Push data to Data Warehouse)
            /*-----------------------------------------------------------------*/
            DWHServiceHostClient objSrvRef = new DWHServiceHostClient();

            /*-----------------------------------------------------------------*/
            /////// Generate Encryption Key (Security key to access Data Warehouse service methods)
            string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
            string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

            /*-----------------------------------------------------------------*/
            /////// DML Opertion (To Data Warehouse)
            string strReturnVal = objSrvRef.SwapUserName(Lbl_New_User_Id.Text, strSecurityKey);
            if (strReturnVal == "5")
            {
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandText = "USP_V2_PARENT_USER_SWAPPING";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", 'U');
                objCommand.Parameters.AddWithValue("@P_INT_INVESTOR_ID", Convert.ToInt32(Lbl_Invetsor_Id.Text));

                int x = objCommand.ExecuteNonQuery();

                if (x > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Unit swapped successfully !</strong>','" + strProjName + "')", true);
                    FillGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Internal server error,Please try again !</strong>','" + strProjName + "')", true);
                }
            }
            else if (strReturnVal == "11")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>This user does not exists in SSO database !</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserSwap");
        }
    }

    /////// Button Search
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserSwap");
        }
    }

    #endregion

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserSwap");
        }
    }
    //// Function for Display Paging
    private void DisplayPaging()
    {
        try
        {
            if (this.GridView1.Rows.Count > 0)
            {
                this.LblPaging.Visible = true;
                LbtnAll.Visible = true;
            }
            if (this.GridView1.PageIndex + 1 == this.GridView1.PageCount)
            {
                this.LblPaging.Text = "Results <b>" + ((Label)GridView1.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
            }
            else
            {
                this.LblPaging.Text = "Results <b>" + ((Label)GridView1.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)GridView1.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((GridView1.PageSize - 1))) + "</b> of <b>" + intRetVal + "</b>";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserSwap");
        }

    }

    protected void LbtnAll_Click(object sender, EventArgs e)
    {
        try
        {
            if (LbtnAll.Text == "All")
            {
                LbtnAll.Text = "Paging";
                GridView1.PageIndex = 0;
                GridView1.AllowPaging = false;
                FillGrid();
            }
            else
            {
                LbtnAll.Text = "All";
                GridView1.AllowPaging = true;
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserSwap");
        }
    }
}