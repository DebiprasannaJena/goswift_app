using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class incentives_WUC_Inct_FY : System.Web.UI.UserControl
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataSet objds = new DataSet();

        try
        {
            string strUserId = "1022";// Session["InvestorId"].ToString();
            string strInctId =  "10100109"; //Session["IncentiveNo"]; ////"10100109"; ////Session["InctNo"].ToString();

            objCommand.CommandText = "USP_INCT_LOAD_FY_WUC";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_INT_USER_ID", strUserId);
            objCommand.Parameters.AddWithValue("@P_INT_INCT_ID", strInctId);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objds);

            GridView1.DataSource = null;
            GridView1.DataBind();


            if (objds.Tables[1].Rows.Count > 0)
            {
                GridView1.DataSource = objds.Tables[1];
                GridView1.DataBind();

                if (objds.Tables[0].Rows.Count > 0)
                {
                    string strFy = objds.Tables[0].Rows[0]["fy"].ToString();

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        HiddenField Hid_FY = (HiddenField)GridView1.Rows[i].FindControl("Hid_FY");
                        RadioButton RadBtn_FY = (RadioButton)GridView1.Rows[i].FindControl("RadBtn_FY");

                        if (Hid_FY.Value == strFy)
                        {
                            RadBtn_FY.Checked = true;
                        }
                        else
                        {
                            RadBtn_FY.Checked = false;
                            GridView1.Rows[i].Enabled = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            //throw ex;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('User Control Encounter Some Error,Please Reload the Page !!');", true);
        }
        finally
        {
            objCommand = null;
        }
    }
}