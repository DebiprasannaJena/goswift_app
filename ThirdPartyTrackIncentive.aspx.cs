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
public partial class ThirdPartyTrackIncentive : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"].ToString() == "101")//101 means Incentive
        {
            lblmainheader.InnerText = "Third Party Incentive Verification";            
            lblheader.InnerText = "Track Incentive Details";
            lblsubheader.InnerText = "Incentive  Number";
        }
        else if(Request.QueryString["id"].ToString() == "102")//102 means Provisional Priority Certificate
        {
            lblmainheader.InnerText = "Third Party Provisional Priority Certificate Verification";
            lblheader.InnerText = "Track Provisional Priority Certificate Details";
            lblsubheader.InnerText = "Provisional Priority Certificate Number";
        }
    }


    public void fillGridview()//Bind Gridview
    {

        //Start Validation Part
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
        //End Validation 
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