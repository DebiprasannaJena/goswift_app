using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Login;
using BusinessLogicLayer.Login;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Admin.CommonFunction;
using System.Text;

public partial class Portal_SuperAdmin_ManageUserLogin : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        ///// This page can only be accessed by goadmin.
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        if (Convert.ToInt32(Session["UserId"]) != 1)
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            BindUser();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error" + ex);
        }
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        txtuser.Text = "";
        DrpDwn_Search_Type.SelectedIndex = 0;
        BindUser();
    }

    private void BindUser()
    {
        SqlCommand cmd = new SqlCommand();
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }

        try
        {
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_BINDUSER";

            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@VCH_SEARCH", txtuser.Text);
            cmd.Parameters.AddWithValue("@INT_SEARCH_TYPE", Convert.ToInt32(DrpDwn_Search_Type.SelectedValue));
            cmd.Parameters.AddWithValue("@VCH_ACTION", "FS");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                GrdUser.DataSource = dt;
                GrdUser.DataBind();
            }
            else
            {
                GrdUser.DataSource = null;
                GrdUser.DataBind();
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            cmd = null;
            conn.Close();
        }
    }
    public string GetDisplayName(int UserId)
    {
        string strDisplayname = "";
        string strQuery = "";
        strQuery = "select vchDomainUName from M_Por_user where intUserId=" + UserId;
        string cs = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand(strQuery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                strDisplayname = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        return strDisplayname;
    }

    protected void GrdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdUser.PageIndex = e.NewPageIndex;
        BindUser();
    }
    protected void GrdUser_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string Vchusername = GrdUser.DataKeys[e.NewEditIndex].Values[1].ToString();

        SqlCommand cmd = new SqlCommand();
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }

        try
        {
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_BINDUSER";
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@VCH_SEARCH", "");
            cmd.Parameters.AddWithValue("@VCH_USERNAME", Vchusername);
            cmd.Parameters.AddWithValue("@VCH_ACTION", "LD");

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["DEPT"].ToString() == null)
                {
                    Session["Department"] = "";
                }
                else
                {
                    Session["Department"] = dt.Rows[0]["DEPT"].ToString();
                }

                if (dt.Rows[0]["PID"].ToString() == null)
                {
                    Session["SubDept"] = "";
                }
                else
                {
                    Session["SubDept"] = dt.Rows[0]["PID"].ToString();
                }

                if (dt.Rows[0]["bitAdminPrevil"].ToString() == "True")
                {
                    Session["adminstat"] = "super";
                }
                else if (dt.Rows[0]["bitSubAdminPrevil"].ToString() == "True")
                {
                    Session["adminstat"] = "loc";
                }
                else
                {
                    Session["adminstat"] = "";
                }

                Session["LevelID"] = dt.Rows[0]["INTLEVELID"].ToString();
                Session["locId"] = dt.Rows[0]["LOCATION"].ToString();
                Session["location"] = dt.Rows[0]["LOCATION"].ToString();
                Session["DeptId"] = dt.Rows[0]["DEPTID"].ToString();

                Session["UserId"] = dt.Rows[0]["INTUSERID"].ToString();
                Session["userName"] = dt.Rows[0]["vchUserName"].ToString();
                Session["fullName"] = dt.Rows[0]["vchFullName"].ToString();

                Session["menuCnt"] = 0;
                Session["strImage"] = dt.Rows[0]["vchUserImage"].ToString();
                Session["desination"] = dt.Rows[0]["Designation"].ToString();
                Session["desId"] = dt.Rows[0]["INTDESIGNATIONID"].ToString();
                Session["displayname"] = GetDisplayName(Convert.ToInt32(dt.Rows[0]["INTUSERID"].ToString()));
                Session["ChangePwd"] = 0;

                CommonFunction.CreateUsersXML(Convert.ToInt32(Session["UserId"]));
                CommonFunction.CreateHierarchyXml();
                Session["RandomNo"] = CommonFunction.GenerateRandomNum();
                string url = "../Dashboard/Default.aspx";
                StringBuilder sb = new StringBuilder();
                sb.Append("<script type = 'text/javascript'>");
                sb.Append("window.open('");
                sb.Append(url);
                sb.Append("');");
                sb.Append("</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error" + ex);
        }
        finally
        {
            cmd = null;
            conn.Close();
        }
    } 

}