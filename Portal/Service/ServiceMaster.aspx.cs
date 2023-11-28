using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EntityLayer.Service;
using BusinessLogicLayer.Service;

public partial class Portal_Service_ServiceMaster : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    readonly string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDepartment();
            Search();
        }
        
    }
  
    void FillDepartment()
    {
        SqlCommand cmd = new SqlCommand("SELECT intLevelDetailId, nvchLevelName FROM M_ADM_LevelDetails WHERE intLevelId=2 ", conn);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
           ddlDepartment.DataSource=ds.Tables[0];
           ddlDepartment.DataTextField = "nvchLevelName";
           ddlDepartment.DataValueField = "intLevelDetailId";
           ddlDepartment.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "All";
            lst.Value = "0";
           ddlDepartment.Items.Insert(0, lst);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {      
            Search();
    }

    void Search()
    {
        ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
        ServiceDetails objservice = new ServiceDetails();
        DataSet ds = new DataSet();

        try
        {
            objservice.strAction = "VA";
            objservice.intdeptid = Convert.ToInt32(ddlDepartment.SelectedValue);
            ds = objServiceDet.ViewServiceMasterByDepartMentID(objservice);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
   
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ServiceBusinessLayer objServiceDet = new ServiceBusinessLayer();
        ServiceDetails objservice = new ServiceDetails();
        GridViewRow row = GridView1.Rows[e.RowIndex];
        TextBox Txt_ServiceName = (TextBox)row.FindControl("txtServiceName");
        HiddenField hdf_ServiceId = (HiddenField)row.FindControl("Hid_ServicedId");
        DropDownList ddl_Category = (DropDownList)row.FindControl("ddlCategory");       
        #region Validation
        if (Txt_ServiceName.Text.Trim() == "")
        {
            Txt_ServiceName.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Service Name can not be blank !</strong>')", true);
            return;
        }
        #endregion
        try
        {
            string X = hdf_ServiceId.Value;
            objservice.strAction = "UD";
            objservice.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            objservice.strServiceName =Convert.ToString( Txt_ServiceName.Text);
            objservice.intServiceCategory = Convert.ToInt32(ddl_Category.SelectedItem.Value);
            objservice.intServiceId = Convert.ToInt32(hdf_ServiceId.Value);
            int strRetval = objServiceDet.UpdateServiceByServiceId(objservice);
            if (strRetval == 1)
            {

                GridView1.EditIndex = -1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Data Updated Successfully !</strong>','" + strProjName + "')", true);
                Search();
            }
            else
            {
                GridView1.EditIndex = -1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>No data Updated Successfully !</strong>','" + strProjName + "')", true);
                Search();
            }
        }
       
        catch (Exception ex)
        { 
            throw ex.InnerException; 
        }             
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;
            Search();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Sevice Update");
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            Search();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Sevice Update");
        }
    }
}