using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class ServicesCheck : System.Web.UI.Page
{
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillService(ddlService);
        }
    }
    public void FillService(DropDownList ddlService)
    {

        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        string Qury = "select INT_SERVICEID,VCH_SERVICENAME from M_SERVICEMASTER_TBL where INT_DELETED_FLAG=0";
        SqlCommand cmd = new SqlCommand(Qury, con); // table name 
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);  // fill dataset
        ddlService.DataTextField = ds.Tables[0].Columns["VCH_SERVICENAME"].ToString(); // text field name of table dispalyed in dropdown
        ddlService.DataValueField = ds.Tables[0].Columns["INT_SERVICEID"].ToString();             // to retrive specific  textfield name 
        ddlService.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
        ddlService.DataBind();  //binding dropdownlist
        ddlService.Items.Insert(0, new ListItem("Select", "0"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlService.SelectedValue != null)
        {
            Response.Redirect("FormView.aspx?FormId=" + Convert.ToInt32(ddlService.SelectedValue) + "");
        }
    }
}