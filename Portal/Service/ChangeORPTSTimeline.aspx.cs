using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Portal_Service_ChangeORPTSTimeline : System.Web.UI.Page
{
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Key"] == "" && Request.QueryString["Sid"] == null)
            {
                save.Visible = false;
                error.Visible = true;
            }
            else
            {
                save.Visible = true;
                error.Visible = false;
            }
        }       
    }
    protected void btnchange_Click(object sender, EventArgs e)
    {
        if (ddltype.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Select Industry Type'); </script>", false);
            return;
        }
        else
        {
            DataTable PnlDt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("USP_SERVICE_UPDATE_MANUAL_ORTPSTIMELINE"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@P_VCH_APPLICATION_UNQ_KEY", SqlDbType.VarChar).Value = DycrptQueryString(Request.QueryString["Key"].ToString());
                        cmd.Parameters.Add("@INTSERVICENO", SqlDbType.Int).Value = Convert.ToInt32(DycrptQueryString(Request.QueryString["Sid"].ToString()));
                        cmd.Parameters.Add("@intOLTPTimeindays", SqlDbType.Int).Value = Convert.ToInt32(ddltype.SelectedValue);                       
                        cmd.Connection = con;
                        con.Open();
                        PnlDt.Load(cmd.ExecuteReader());
                        con.Close();
                    }
                    if (PnlDt.Rows.Count > 0)
                    {
                        ddltype.SelectedIndex = 0;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Saved Successfully'); </script>", false);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Error Occured'); </script>", false);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                }
            }
        }
    }
    public string DycrptQueryString(string strQueryString)
    {
        EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
        return objEDQueryString.Decrypt(strQueryString, "m8s3e3k5");
    }
}