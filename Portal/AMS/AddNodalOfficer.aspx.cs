using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SingleWindow_AddNodalOfficer : System.Web.UI.Page
{
    AMS objams = new AMS();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                   ClrData();
                }
            }
        }
    }

    private void ClrData()
    {
        Populate_Nodal_Officer();
        txtRemark.Text = string.Empty;
    }

    private void Populate_Nodal_Officer()
    {
        try
        {
            Agenda objA = new Agenda();

            objA.Action = "E";
            objA.OfficerType = 1;
            DataTable dt = new DataTable();
            dt = AMServices.ViewOfficers(objA);
            if (dt.Rows.Count > 0)
            {
                ddlName.DataSource = dt;
                ddlName.DataValueField = dt.Columns["intUserId"].ColumnName;
                ddlName.DataTextField = dt.Columns["Fullname"].ColumnName;
                ddlName.DataBind();
                ddlName.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }
        catch (Exception ex)
        { Response.Write(ex.Message); }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlName.SelectedValue == "0") 
        {
            string strmsg = "<script>alert('Please Select Nodal Officer Name.')</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg, false);
            return;
        }
        else if (txtRemark.Text.Trim() == "")
        {
            string strmsg = "<script>alert('Please Enter Remark.')</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg, false);
            return;
        }

        try
        {
            objams.Action = "UFP";
            objams.ProjectId=Convert.ToInt32(Request.QueryString["id"]);
            objams.OfficerId = Convert.ToInt32(ddlName.SelectedValue);
            objams.Remark = txtRemark.Text.Trim();

            string strRes = AMServices.UpdateForwardProject(objams);

            if (strRes == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('Agenda Successfully Allocated to Nodal Officer.');top.$('#pageModal').modal('hide');top.location.reload();", true);
                ClrData();
            }
            
        }
        catch (Exception ex)
        { Response.Write(ex.Message); }
    }


}