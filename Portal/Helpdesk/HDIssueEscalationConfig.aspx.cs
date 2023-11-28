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
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.HelpDesk;
using System.Web;

public partial class Portal_HelpDesk_HDIssueEscalationConfig : System.Web.UI.Page
{
    #region "Variable Declaration"
    int intOutput = 0;
    string strShowMsg = string.Empty;
    public string strRes = string.Empty;
    public DataTable dt = new DataTable();
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
            if (Request.QueryString["CONID"] != null)
            {
                Getdata();
            }
        }
    }

    #region Grid Events
    protected void gvEscalation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DdlDesg = (DropDownList)e.Row.FindControl("DdlDesg");
            hdObject.Action = "A";
            List<IssueRegistration> lst = objlayer.FillAuthority(hdObject).ToList();
            DdlDesg.DataValueField = "int_UserId";
            DdlDesg.DataTextField = "vch_UserName";
            DdlDesg.DataSource = lst;
            DdlDesg.DataBind();
            DdlDesg.Items.Insert(0, new ListItem("-Select-", "0"));
            if (!string.IsNullOrEmpty(Request.QueryString["CONID"]))
            {
                HiddenField hdnDesg = (HiddenField)e.Row.FindControl("hdnDesg");
                DdlDesg.SelectedValue = hdnDesg.Value;
            }
        }
    }
    #endregion

    #region button event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvEscalation.Rows.Count > 0)
            {

                DataTable objDt = new DataTable();
                objDt = CreateHelpDeskTable();
                objDt.TableName = "tblHelpDesk";
                hdObject.Action = "I";
                hdObject.intUpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                for (int i = 0; i < gvEscalation.Rows.Count; i++)
                {
                    DataRow objRow =  objDt.NewRow();
                    objRow["int_CategoryId"] = Convert.ToInt32(ddlCategory.SelectedValue);
                    objRow["int_SubcategoryId"] = Convert.ToInt32(ddlSubcategory.SelectedValue);
                    GridViewRow gvr =  (GridViewRow)gvEscalation.Rows[i];
                    objRow["VCH_STANDARD_DAYS"] = ((TextBox)gvr.Cells[3].FindControl("txtStndDay")).Text;
                    objRow["intLevelid"] = Convert.ToInt32(((Label)gvr.Cells[0].FindControl("lblSlno")).Text);
                    objRow["int_UserId"] = Convert.ToInt32(((DropDownList)gvr.Cells[1].FindControl("DdlDesg")).SelectedValue.ToString());
                    objRow["VchMobile"] = ((TextBox)gvr.Cells[5].FindControl("txtNotifyPhoneNo")).Text;
                    objRow["Email"] = ((TextBox)gvr.Cells[4].FindControl("txtNotifyEmail")).Text;
                    TextBox txtNotifyMailContent = (TextBox)gvr.Cells[6].FindControl("txtNotifyMailContent");
                    objRow["vchEmailContent"] = txtNotifyMailContent.Text.Trim();
                    TextBox txtNotifyPhoneContent = (TextBox)gvr.Cells[7].FindControl("txtNotifyPhoneContent");
                    objRow["vchMobileContent"] = txtNotifyPhoneContent.Text.Trim();
                    objDt.Rows.Add(objRow);
                }

                string strXml= string.Empty;
                CommonFunctions obj = new CommonFunctions();
                strXml = obj.GetSTRXMLResult(objDt);
                hdObject.strXmlHelpDesk = strXml;
                intOutput = Convert.ToInt32(objlayer.AddHDEscalationConfiguration(hdObject));
                if (intOutput == 2)
                {
                    strShowMsg = Messages.ShowMessage(intOutput.ToString());
                }
                else
                {
                    strShowMsg = "Escalation level(s) has already been added for this Category and Sub-Category";
                }

                if (string.Equals(btnSave.Text, "Update", StringComparison.OrdinalIgnoreCase))
                {
                    strShowMsg = Messages.ShowMessage(intOutput.ToString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = 'ViewEscalationConfig.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&index=" + Request.QueryString["index"] + "';});   </script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = 'HDIssueEscalationConfig.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "';});   </script>", false);
                    ddlType.SelectedValue = "0";
                    ddlCategory.Items.Clear();
                    ddlCategory.Items.Add(new ListItem("--Select--", "0"));
                    ddlSubcategory.Items.Clear();
                    ddlSubcategory.Items.Add(new ListItem("--Select--", "0"));
                    gvEscalation.DataSource = null;
                    gvEscalation.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "HelpDesk");
        }
    }
    #endregion

    #region dropdown
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

    protected void ddlSubcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            hdObject.Action = "C";
            hdObject.int_SubcategoryId = Convert.ToInt32(ddlSubcategory.SelectedValue);
            int objHelpdesk = Convert.ToInt32(objlayer.CountEscalationLevel(hdObject).ToString());
            if (objHelpdesk == 20)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Escalation level(s) has already been added for this category type', '" + Messages.TitleOfProject + "');   </script>", false);
            }
            else
            {
                fillGrid(objHelpdesk);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Helpdesk");
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubCategory(Convert.ToInt32(ddlCategory.SelectedValue));
    }
    #endregion

    #region "Fill Gridview with escalation level to insert"
    public void fillGrid(int intCount)
    {
        try
        {
            int i = 0;
            #region "Creating Datatable to bind with gridview"
            DataColumn slno = new DataColumn("slno", typeof(string));
            DataColumn desg = new DataColumn("desg", typeof(string));
            DataColumn desglvl = new DataColumn("desglvl", typeof(string));
            DataColumn loc = new DataColumn("loc", typeof(string));
            DataColumn loclvl = new DataColumn("loclvl", typeof(string));
            DataColumn stdP = new DataColumn("stdP", typeof(string));
            DataColumn vchEmail = new DataColumn("vchEmail", typeof(string));
            DataColumn vchEmailContent = new DataColumn("vchEmailContent", typeof(string));
            DataColumn vchMobile = new DataColumn("vchMobile", typeof(string));
            DataColumn vchMobileContent = new DataColumn("vchMobileContent", typeof(string));
            dt.Columns.Add(slno);
            dt.Columns.Add(desg);
            dt.Columns.Add(desglvl);
            dt.Columns.Add(loc);
            dt.Columns.Add(loclvl);
            dt.Columns.Add(stdP);
            dt.Columns.Add(vchEmail);
            dt.Columns.Add(vchEmailContent);
            dt.Columns.Add(vchMobile);
            dt.Columns.Add(vchMobileContent);
            DataRow dr = null;
            #endregion
            if (intCount > 0)
            {
                while (i < intCount)
                {
                    dr = dt.NewRow();
                    dr["slno"] = i + 1; //stores level id
                    dr["desglvl"] = "0";
                    dr["desg"] = "--Select--";
                    dr["loc"] = "0";
                    dr["loclvl"] = "--Select--";
                    dr["stdP"] = string.Empty;
                    dr["vchEmail"] = string.Empty;
                    dr["vchEmailContent"] = string.Empty;
                    dr["vchMobile"] = string.Empty;
                    dr["vchMobileContent"] = string.Empty;
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            gvEscalation.DataSource = dt;
            gvEscalation.DataBind();
            HiddenField1.Value = gvEscalation.Rows.Count.ToString();
            gvEscalation.Visible = true;

            ViewState["DynamicTbl"] = dt;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region user defined functions
    public void BindCategory(int intTypeId)
    {
        hdObject = new IssueRegistration();
        objlayer = new HelpDeskBusinessLayer();
        List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();
        try
        {
            hdObject.Action = "C";
            hdObject.intTypeId = intTypeId;
            objHelpdesk = objlayer.BindCategory(hdObject).ToList();
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

    public void BindSubCategory(int intCategory)
    {

        hdObject = new IssueRegistration();
        objlayer = new HelpDeskBusinessLayer();
        List<IssueRegistration> objHelpdesk = new List<IssueRegistration>();

        try
        {
            hdObject.Action = "S";
            hdObject.int_CategoryId = intCategory;
            objHelpdesk = objlayer.BindSubCategory(hdObject).ToList();
            ddlSubcategory.DataTextField = "vch_SubCategoryName";
            ddlSubcategory.DataValueField = "int_SubcategoryId";
            ddlSubcategory.DataSource = objHelpdesk;
            ddlSubcategory.DataBind();
            ddlSubcategory.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {

            Util.LogError(ex, "Helpdesk");
        }
    }

    private DataTable CreateHelpDeskTable()
    {
        DataTable objDt = new DataTable();
        objDt.Columns.Add(new DataColumn("int_CategoryId"));
        objDt.Columns.Add(new DataColumn("int_SubcategoryId"));
        objDt.Columns.Add(new DataColumn("VCH_STANDARD_DAYS"));
        objDt.Columns.Add(new DataColumn("intLevelid"));
        objDt.Columns.Add(new DataColumn("int_UserId"));
        objDt.Columns.Add(new DataColumn("VchMobile"));
        objDt.Columns.Add(new DataColumn("Email"));
        objDt.Columns.Add(new DataColumn("vchEmailContent"));
        objDt.Columns.Add(new DataColumn("vchMobileContent"));
        return objDt;
    }

    #region "Get Data From Database For Edit"
    private void Getdata()
    {
        hdObject.int_SubcategoryId = Convert.ToInt32(Request.QueryString["CONID"].ToString());
        hdObject.Action = "EE";
        List<IssueRegistration> lst = objlayer.EditViewConfigEscalation(hdObject).ToList();

        string strTcount = string.Empty;
        if (lst.Count > 0)
        {

            ddlType.SelectedValue = lst[0].intTypeId.ToString();

            ddlType.Enabled = false;

            BindCategory(Convert.ToInt32(ddlType.SelectedValue));
            ListItem lstItem = ddlCategory.Items.FindByValue(lst[0].int_CategoryId.ToString());
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(lstItem);
            ddlCategory.SelectedValue = lst[0].int_CategoryId.ToString();
            ddlCategory.Enabled = false;

            BindSubCategory(Convert.ToInt32(ddlCategory.SelectedValue));
            ListItem lstSubItem = ddlSubcategory.Items.FindByValue(lst[0].int_SubcategoryId.ToString());
            ddlSubcategory.Items.Clear();
            ddlSubcategory.Items.Add(lstSubItem);
            ddlSubcategory.SelectedValue = lst[0].int_SubcategoryId.ToString();
            ddlSubcategory.Enabled = false;


            DataTable dtEscalation= new DataTable();
            DataColumn slno = new DataColumn("slno", typeof(string));
            DataColumn desg = new DataColumn("desg", typeof(string));
            DataColumn desglvl = new DataColumn("desglvl", typeof(string));
            DataColumn loc = new DataColumn("loc", typeof(string));
            DataColumn loclvl = new DataColumn("loclvl", typeof(string));
            DataColumn stdP = new DataColumn("stdP", typeof(string));
            DataColumn vchEmail = new DataColumn("vchEmail", typeof(string));
            DataColumn vchEmailContent = new DataColumn("vchEmailContent", typeof(string));
            DataColumn vchMobile = new DataColumn("vchMobile", typeof(string));
            DataColumn vchMobileContent = new DataColumn("vchMobileContent", typeof(string));
            dtEscalation.Columns.Add(slno);
            dtEscalation.Columns.Add(desg);
            dtEscalation.Columns.Add(desglvl);
            dtEscalation.Columns.Add(loc);
            dtEscalation.Columns.Add(loclvl);
            dtEscalation.Columns.Add(stdP);
            dtEscalation.Columns.Add(vchEmail);
            dtEscalation.Columns.Add(vchEmailContent);
            dtEscalation.Columns.Add(vchMobile);
            dtEscalation.Columns.Add(vchMobileContent);

            for (int i=0; i < lst.Count; i++)
            {
                DataRow  dRow = dtEscalation.NewRow();
                dRow["slno"] = lst[i].intLevelid;
                dRow["desglvl"] = lst[i].int_UserId;
                dRow["stdP"] = lst[i].VCH_STANDARD_DAYS;
                dRow["vchEmail"] = lst[i].Email;
                dRow["vchEmailContent"] = lst[i].vchEmailContent;
                dRow["vchMobile"] = lst[i].VchMobile;
                dRow["vchMobileContent"] = lst[i].vchMobileContent;
                dtEscalation.Rows.Add(dRow);
            }

            gvEscalation.DataSource = dtEscalation;
            gvEscalation.DataBind();
            btnSave.Text = "Update";
        }
    }
    #endregion
    #endregion
}