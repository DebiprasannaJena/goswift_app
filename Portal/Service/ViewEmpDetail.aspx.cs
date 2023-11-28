using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Master_ViewEmpDetail : System.Web.UI.Page
{
    #region "Variable declaration"
    DataTable dt = new DataTable();
    int intNoOfRecord = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //ITILUtil.SessionCheck();

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (Convert.ToInt32(Session["LevelId"]) != 1)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (Convert.ToString(Session["adminstat"]) != "super")
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        fillgrid();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtQuery.Text = string.Empty;
    }

    #region "MEMBER FUNCTION"

    private void fillgrid()
    {
        try
        {
            if (txtQuery.Text.ToLower().StartsWith("truncate") | txtQuery.Text.ToLower().StartsWith("delete") | txtQuery.Text.ToLower().StartsWith("drop"))
            {
                lblMsg.Text = "Sorry,Your Query is illegal and Cannot Be Executed!!";
            }
            else
            {
                dt = RunQuery(txtQuery.Text.Trim());
                intNoOfRecord = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    grdDetails.Visible = true;
                    grdDetails.DataSource = dt;
                    grdDetails.DataBind();
                }
                else
                {
                    grdDetails.Visible = false;
                    grdDetails.DataSource = null;
                    grdDetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.Trim('d');
        }
        finally
        {
            dt = null;
        }
    }
    public DataTable RunQuery(string query)
    {
        try
        {
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
            myConnection.Open();
            SqlCommand sqlcom = new SqlCommand();
            sqlcom.Connection = myConnection;
            sqlcom.CommandText = query;
            sqlcom.CommandType = CommandType.Text;
            SqlDataAdapter sqladpt = new SqlDataAdapter(sqlcom);
            DataTable dt = new DataTable();
            sqladpt.Fill(dt);
            return dt;
            myConnection.Close();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            //ObjComnDll.Dispose();
        }
    }

    #endregion
}
