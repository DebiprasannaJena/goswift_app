using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Portal_MISReport_DeptLoginRpt : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/portal/SessionRedirect.aspx", false);
            return;
        }

        if (!IsPostBack)
        {
            txtFromdate.Attributes.Add("readonly", "readonly");
            txtTodate.Attributes.Add("readonly", "readonly");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FillGridView();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DeptLoginReport");
        }
    }

    public void FillGridView()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }

        try
        {
            SqlDataAdapter objDa = new SqlDataAdapter();
            DataTable objtable = new DataTable();

            SqlCommand objCommand = new SqlCommand();
            objCommand.Connection = conn;
            objCommand.CommandText = "USP_LOGIN_MIS_REPORT";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Parameters.AddWithValue("@P_VCH_FROMDATE", Convert.ToString(txtFromdate.Text));
            objCommand.Parameters.AddWithValue("@P_VCH_TODATE", Convert.ToString(txtTodate.Text));
            objCommand.Parameters.AddWithValue("@P_VCH_USERNAME", Convert.ToString(Txt_Username.Text));

            if (DrpDwn_Logintype.SelectedItem.Value == "1")
            {
                objCommand.Parameters.AddWithValue("@vchAction", "AUR");
            }
            else if (DrpDwn_Logintype.SelectedItem.Value == "2")
            {
                objCommand.Parameters.AddWithValue("@vchAction", "ALUR");
            }

            objDa.SelectCommand = objCommand;
            objDa.Fill(objtable);
            GridDeptLoginRpt.DataSource = objtable;
            GridDeptLoginRpt.DataBind();

        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    protected void BtnReset_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "onload", "<script>setDateValues();</script>", false);
        Txt_Username.Text = "";
        DrpDwn_Logintype.SelectedIndex = 0;
    }
    protected void LnkBtnPdf_Click(object sender, EventArgs e)
    {
        try
        {
            IncentiveCommonFunctions.CreatePdf("DepartmentLoginReport", GridDeptLoginRpt);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DeptLoginReport");
        }
    }
    protected void LnkBtnExcel_Click(object sender, EventArgs e)
    {
        try
        {
           

            if (GridDeptLoginRpt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DepartmentLoginReportCont.xls"));
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                System.IO.StringWriter sw = new System.IO.StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GridDeptLoginRpt.RenderControl(htw);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>No record found to export.</strong>');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "DeptLoginReport");
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}