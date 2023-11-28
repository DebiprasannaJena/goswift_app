#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   AddCMS.aspx.cs
// Description           :   Add Content
// Created by            :   AMit Sahoo
// Created On            :   21 August 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion

#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Data;
#endregion

public partial class CMS_AddCMS : SessionCheck
{
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    string str_Retvalue = "";
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillMenu();
             if (Request.QueryString["id"] != null)
            {
                editData(Convert.ToInt32(Request.QueryString["id"]));
            }
        }
    }
    #endregion

    #region Common_Functions
    /// <summary>
    /// Fill Menu Dropdownlist
    /// </summary>
    private void FillMenu()
    {
        IList<CMSDetails> objIndList = new List<CMSDetails>();
        ddlMenu.DataSource = objService.PopulateMenu();
        ddlMenu.DataTextField = "StrMenuName";
        ddlMenu.DataValueField = "IntMenuId";
        ddlMenu.DataBind();
        ddlMenu.Items.Insert(0, new ListItem("-Select-", "0"));
        //ListItem list = new ListItem();
        //list.Text = "--Select--";
        //list.Value = "0";
        //ddlMenu.Items.Insert(0, list);

    }
    public void editData(int id)
    {
        try
        {
            EntityLayer.CMS.CMSDetails objdata = new EntityLayer.CMS.CMSDetails();
            objdata = objService.EditCMS(id);
            if (objdata != null)
            {
                ddlMenu.SelectedValue = objdata.IntMenuId.ToString();
                ddlMenu.Enabled = false;
               txtRemark.Text = objdata.StrContent;
                ViewState["id"] = id;
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "alert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    #endregion

    #region Button_Click
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //CMSService.CmsBusinesslayerClient objService = new CMSService.CmsBusinesslayerClient();
        EntityLayer.CMS.CMSDetails objServiceEntity = new EntityLayer.CMS.CMSDetails();
        try
        {
            if (btnSave.Text == "Update")
            {
                objServiceEntity.StrAction = "U";
                objServiceEntity.IntMenuId = Convert.ToInt32(ddlMenu.SelectedValue);
                if (ViewState["id"] == "" || ViewState["id"] == null)
                {
                    objServiceEntity.IntCmsId = Convert.ToInt32(ddlMenu.SelectedValue);
                }
                else
                {
                    objServiceEntity.IntCmsId = Convert.ToInt32(ViewState["id"].ToString());   
                }
                           
            }
            else
            {
                objServiceEntity.StrAction = "A";
                objServiceEntity.IntMenuId = Convert.ToInt16(ddlMenu.SelectedValue);               
            }
            objServiceEntity.IntCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            //objServiceEntity.StrContent = txtRemark.Text;
            objServiceEntity.StrContent = MimeType.GetHTMLtext(txtRemark.Text.ToString());
            str_Retvalue = objService.ManageCMS(objServiceEntity);
            if (str_Retvalue == "1")
            {
                Clear();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('Data Added Successfully !');", true);
            }
            if (str_Retvalue == "2")
            {
                Clear();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('Data Updated Successfully !');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }

    }
    #endregion
    public void Clear()
    {
        ddlMenu.SelectedValue = "0";
        txtRemark.Text = "";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewCMS.aspx");
    }
    protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           int intmenuid = Convert.ToInt32(ddlMenu.SelectedValue);
            string straction = "CHK";
            DataTable dt = objService.ChkCMSData(straction, intmenuid);
            if (dt.Rows.Count > 0)
            {
                btnSave.Text = "Update";
                ddlMenu.SelectedValue = (dt.Rows[0]["intMenuId"]).ToString();
                txtRemark.Text = dt.Rows[0]["vchContent"].ToString();
            }
            else
            {
                txtRemark.Text = "";
                btnSave.Text = "Submit";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
}