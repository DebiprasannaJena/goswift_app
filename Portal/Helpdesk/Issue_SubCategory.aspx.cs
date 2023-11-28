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

public partial class Portal_HelpDesk_Issue_SubCategory : SessionCheck
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
           // BindCategory();
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
            objswp.int_SubcategoryId = id;
            objlist = objlayer.ViewIssueSubCategory(objswp);
            if (objlist.Count > 0)
            {
                ddlType.SelectedValue = objlist[0].intTypeId.ToString();
                if (objlist[0].intTypeId != 0)
                {
                    BindCategory(Convert.ToInt32(objlist[0].intTypeId));
                    ddlCategory.SelectedValue = objlist[0].int_CategoryId.ToString(); 
                }
                
                txtSubCategory.Text = objlist[0].vch_SubCategoryName;
                ddlEscLvl.SelectedValue = objlist[0].int_EscLevel.ToString();
                ViewState["id"] = id;
                ViewState["Typeid"] = objlist[0].intTypeId.ToString();
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
                objswp.int_SubcategoryId = Convert.ToInt32(ViewState["id"].ToString());
                objswp.int_EscLevel = Convert.ToInt32(ddlEscLvl.SelectedValue);
            }
            else
            {
                objswp.Action = "A";
            }
            objswp.int_CategoryId =Convert.ToInt32(ddlCategory.SelectedValue);
            objswp.vch_SubCategoryName = txtSubCategory.Text.Trim();
            objswp.int_EscLevel = Convert.ToInt32(ddlEscLvl.SelectedValue);
            str_Retvalue = objlayer.AddIssueSubCategory(objswp);
            if (str_Retvalue == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('Data Saved successfully !');", true);
                clear();
            }
            if (str_Retvalue == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Updated successfully', '" + Messages.TitleOfProject + "', function () {location.href = 'ViewIssue_SubCategory.aspx';}); </script>", false);
               // ClientScript.RegisterStartupScript(this.GetType(), "Success", "<script type='text/javascript'>alert('Data update successfully!');window.location='ViewCategory.aspx';</script>'");
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
            ddlCategory.SelectedValue = "0";
            txtSubCategory.Text = "";
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
    public void BindCategory( int intTypeId)
    {
        try
        {
            objswp = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
            objswp.Action = "C";
            objswp.intTypeId = intTypeId;
            objHelpdesk = objlayer.BindCategory(objswp).ToList();
            ddlCategory.DataTextField = "vch_CategoryName";
            ddlCategory.DataValueField = "int_CategoryId";
            ddlCategory.DataSource = objHelpdesk;
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
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
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindCategory(Convert.ToInt32(ddlType.SelectedValue));
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
}