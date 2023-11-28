using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Data;


#pragma warning disable
public partial class ThirdPartyTrackPPC : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void fillGridview()
    {
        if (txtApplicationNo.Text.Trim() == "")
        {
            txtApplicationNo.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application no can not be blank !</strong>', '" + strProjName + "'); </script>", false);
            return;
        }

        if (txtCaptcha.Text.Trim() == "")
        {
            txtCaptcha.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Captcha can not be blank !</strong>', '" + strProjName + "'); </script>", false);
            return;
        }

        if ((txtCaptcha.Text).Any(char.IsLower) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid Captcha !</strong>', '" + strProjName + "'); </script>", false);
            txtCaptcha.Text = "";
            txtCaptcha.Focus();
            return;
        }

        Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
        Boolean bt = Captcha1.UserValidated;
        if (bt == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid Captcha !</strong>', '" + strProjName + "'); </script>", false);
            txtCaptcha.Text = "";
            txtCaptcha.Focus();
            return;
        }
        else
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {
                SqlCommand objCommand = new SqlCommand();
                SqlDataAdapter objDa = new SqlDataAdapter();
                DataTable objdt = new DataTable();

                objCommand.CommandText = "USP_TrackServiceAppStatus";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_CHAR_ACTION", "TINC");
                objCommand.Parameters.AddWithValue("@P_VCH_APPNO", txtApplicationNo.Text.Trim());

                objDa.SelectCommand = objCommand;
                objDa.Fill(objdt);

                if (objdt.Rows.Count > 0)
                {
                    grdService.DataSource = objdt;
                    grdService.DataBind();
                    grdService.Visible = true;
                }
                else
                {
                    grdService.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>No Records Found !</strong>', '" + strProjName + "'); </script>", false);
                }

                txtCaptcha.Text = "";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            fillGridview();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ThirdPartyVerification");
        }

    }
}