//****************************************************************************************************
//File Name              :  ConfigEscalationAdd.aspx
// Description           :   Escalation configuration for users
// Created by            :   Radhika Rani Patri
// Created on            :  04-12-2018
// Modification History  :
//                           <CR no.>                      <Date>          <Modified by>                <Modification Summary>'                                                          
//                             
// Function Name         :   
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ComponentModel;
using BusinessLogicLayer.HelpDesk;
using DataAcessLayer.HelpDeskDataLayer;
using BusinessLogicLayer.HelpDesk;

public partial class Portal_HelpDesk_ViewEscalationConfig : System.Web.UI.Page
{

    #region "Variable Declaration"


    int intOutput = 0;
    string strShowMsg = string.Empty;
    public string strRes = string.Empty;
    public DataTable dt = new DataTable();
    public DataTable dt1 = new DataTable();
    DataSet ds = new DataSet();
    string strShwmsg = string.Empty;
    string strLvlCount = string.Empty;
    IssueRegistration hdObject = new IssueRegistration();
    HelpDeskBusinessLayer objlayer = new HelpDeskBusinessLayer();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindType();
            BindGridDetails();
        }
    }
  
    private void BindGridDetails()
    {
        objlayer = new HelpDeskBusinessLayer();
        List<IssueRegistration> objlist = new List<IssueRegistration>();
        try
        {
            hdObject.Action = "V";
            hdObject.intTypeId=Convert.ToInt32(ddlType.SelectedValue);
            hdObject.int_CategoryId=Convert.ToInt32(ddlCategory.SelectedValue);
            hdObject.int_SubcategoryId=Convert.ToInt32(ddlSubCategory.SelectedValue);
            objlist = objlayer.ViewConfigEscalation(hdObject);
            gvEscalation.DataSource = objlist;
            gvEscalation.DataBind();

           int intRetVal = objlist.Count();
           DisplayPaging(intRetVal);
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }

    }
    private void DisplayPaging(int intRetVal)
    {
        try
        {
            if (gvEscalation.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                lbtnAll.Visible = true;
                if (gvEscalation.PageIndex + 1 == gvEscalation.PageCount)
                {
                    lblPaging.Text = "Results <b>" + ((Label)gvEscalation.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + intRetVal + "</b> of <b>" + intRetVal + "</b>";
                }
                else
                {
                    this.lblPaging.Text = "Results <b>" + ((Label)gvEscalation.Rows[0].FindControl("lblsl")).Text + "</b> - <b>" + Convert.ToString(Convert.ToInt32(((Label)gvEscalation.Rows[0].FindControl("lblsl")).Text) + gvEscalation.PageSize - 1) + "</b> of <b>" + intRetVal + "</b>";
                }
            }
            else
            {
                this.lblPaging.Visible = false;
                lbtnAll.Visible = false;
            }
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    protected void gvEscalation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEscalation.PageIndex = e.NewPageIndex;
        BindGridDetails();
    }
    protected void gvEscalation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string s = gvEscalation.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("HDIssueEscalationConfig.aspx?id=" + s);
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
            hdObject = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
            hdObject.Action = "T";
            objHelpdesk = objlayer.BindType(hdObject).ToList();
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

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindSubCategory(Convert.ToInt32(ddlCategory.SelectedValue));
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    public void BindCategory(int intTypeId)
    {
        try
        {
            IssueRegistration hdObject1 = new IssueRegistration();
            objlayer = new HelpDeskBusinessLayer();
            List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
            hdObject1.Action = "C";
            hdObject1.intTypeId = intTypeId;
            objHelpdesk = objlayer.BindCategory(hdObject1).ToList();
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
    public void BindSubCategory( int intCategory)
    {

        hdObject = new IssueRegistration();
        objlayer = new HelpDeskBusinessLayer();
        List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
        
        try
        {
            hdObject.Action = "S";
            hdObject.int_CategoryId = Convert.ToInt32(intCategory);
            objHelpdesk = objlayer.BindSubCategory(hdObject).ToList();
            ddlSubCategory.DataTextField = "vch_SubCategoryName";
            ddlSubCategory.DataValueField = "int_SubcategoryId";
            ddlSubCategory.DataSource = objHelpdesk;
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BindGridDetails();
    }
    
    protected void gvEscalation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = ((gvEscalation.PageIndex * gvEscalation.PageSize) + e.Row.RowIndex + 1).ToString();
                HyperLink hypEdit = (HyperLink)e.Row.FindControl("hlnkEdit");
                hypEdit.NavigateUrl = "HDIssueEscalationConfig.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&index=" + gvEscalation.PageIndex + "&CONID=" + gvEscalation.DataKeys[e.Row.RowIndex].Value;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
}