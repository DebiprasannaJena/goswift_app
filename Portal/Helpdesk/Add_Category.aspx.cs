using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAcessLayer.HelpDeskDataLayer;
using BusinessLogicLayer.HelpDesk;

public partial class Portal_HelpDesk_Add_Category : SessionCheck
{
    string str_Retvalue = "";
    int retval = 0;
    string Uploadname = "";
    IssueRegistration objswp = new IssueRegistration();
    HelpDeskBusinessLayer objlayer = new HelpDeskBusinessLayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindType();
            btnSave.Text = "Save";
            if (Request.QueryString["id"] != null)
            {
                editData(Convert.ToInt32(Request.QueryString["id"]));
            }

        }

    }
    public void editData(int id)
    {
        try
        {
            objlayer = new HelpDeskBusinessLayer();
            objswp = new IssueRegistration();
            List<IssueRegistration> objlist = new List<IssueRegistration>();
            objswp.Action = "E";
            objswp.int_CategoryId = id;
            objlist = objlayer.ViewIssueCategory(objswp);
            if (objlist.Count>0)
            {
                ddlType.SelectedValue = objlist[0].intTypeId.ToString();
                txtCategory.Text = objlist[0].vch_CategoryName;
                ViewState["id"] = id;
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Helpdesk");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
	    {
            objswp = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            if (btnSave.Text == "Update")
            {
                objswp.Action = "U";
                objswp.int_CategoryId = Convert.ToInt32(ViewState["id"].ToString());
            }
            else
            {
                objswp.Action = "A";
            }
            objswp.vch_Type = ddlType.SelectedValue;
            objswp.vch_CategoryName = txtCategory.Text.Trim();

            str_Retvalue = objlayer.AddIssueCategory(objswp);
            if (str_Retvalue == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('Data Saved successfully !');", true);
                clear();
            }
            if (str_Retvalue == "2")
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>jAlert('Data update successfully!');window.location='ViewCategory.aspx';</script>'");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Updated successfully', '" + Messages.TitleOfProject + "', function () {location.href = 'ViewCategory.aspx';}); </script>", false);
            }
	    }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
 
    public void clear()
    {
        try
        {
            ddlType.SelectedValue = "0";
            txtCategory.Text = "";
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clear();
    }
    public void BindType()
    {
        try
        {
            objswp = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
            objswp.Action = "T";
            objHelpdesk = objlayer.BindType(objswp).ToList();
            ddlType.DataTextField = "strTypeName";
            ddlType.DataValueField = "intTypeId";
            ddlType.DataSource = objHelpdesk;
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
}