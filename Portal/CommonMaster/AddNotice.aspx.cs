using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Portal_CommonMaster_AddNotice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
           

            SelectData();

        }
    }

    void SelectData()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        SqlDataReader     sqlReader =  null;
        try
        {
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "USP_AddNotice";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Action","S");
            cmd.Parameters.AddWithValue("@Notice", txtNotice.Text);
            cmd.Parameters.AddWithValue("@dtmfrm", txtFromDate.Text);
            cmd.Parameters.AddWithValue("@dtmto", txtTodate.Text);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
              sqlReader = cmd.ExecuteReader();
                
                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        txtNotice.Text = sqlReader["VCH_NOTIFICATION"].ToString();
                        txtFromDate.Text =  Convert.ToDateTime( sqlReader["DTM_FROM"].ToString()).ToString("dd-MMM-yyyy");
                        txtTodate.Text = Convert.ToDateTime( sqlReader["DTM_END"].ToString()).ToString("dd-MMM-yyyy");
                    
                    }
                }

        }
        catch
        {
        }



    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtNotice.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + "Please enter notice contents" + "', '" + Messages.TitleOfProject + "', function () {});   </script>", false);


            return;
        }
        else if (txtFromDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + "Please enter from date" + "', '" + Messages.TitleOfProject + "', function () {});   </script>", false);
            return;
        }
        else if (txtTodate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + "Please enter to date" + "', '" + Messages.TitleOfProject + "', function () {});   </script>", false);
            return;
        }
        else if (Convert.ToDateTime(txtTodate.Text) < Convert.ToDateTime(txtFromDate.Text))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + "To date can not be less than from date" + "', '" + Messages.TitleOfProject + "', function () {});   </script>", false);
            return;
        }
        else
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
             SqlCommand cmd = new SqlCommand();
             try
             {
                 cmd.Connection = con;
                 cmd.CommandType = System.Data.CommandType.StoredProcedure;
                 cmd.CommandText = "USP_AddNotice";
                 cmd.Parameters.Clear();
                 cmd.Parameters.AddWithValue("@Action", "A");
                 cmd.Parameters.AddWithValue("@Notice", txtNotice.Text);
                 cmd.Parameters.AddWithValue("@dtmfrm", txtFromDate.Text);
                 cmd.Parameters.AddWithValue("@dtmto", txtTodate.Text);
                 if (con.State == ConnectionState.Closed)
                 {
                     con.Open();
                 }
                 cmd.ExecuteNonQuery();
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + "Data saved sucessfully " + "', '" + Messages.TitleOfProject + "', function () {});   </script>", false);
             }
             catch
             {
             }

        }
    }
}