#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   ViewCMS.aspx.cs
// Description           :   View content added
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
#endregion

public partial class CMS_ViewCMS : SessionCheck
{
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    EntityLayer.CMS.CMSDetails objProp = new EntityLayer.CMS.CMSDetails();
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDelete.Attributes.Add("onclick", "return CheckAuthenticate();");
            BindGridview();
        }
    }
    #endregion

    #region Common Function

    public void BindGridview()
    {
        List<CMSDetails> objList = new List<CMSDetails>();       
        try
        {
            objProp.StrAction = "V";
            objList = objService.ViewCMS(objProp).ToList();
            gvCMS.DataSource = objList;
            gvCMS.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "alert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    #endregion

    #region GridView_Events
    protected void gvCMS_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string s = gvCMS.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("AddCMS.aspx?id=" + s);
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "alert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    #endregion
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox chkDelete = new CheckBox();
        string retval;
        try
        {
            for (int counter = 0; counter <= gvCMS.Rows.Count - 1; counter++)
            {
                chkDelete = (CheckBox)(gvCMS.Rows[counter].FindControl("chkItem"));
                if (chkDelete.Checked == true)
                {
                    objProp.IntCmsId = Convert.ToInt32(gvCMS.DataKeys[counter].Value.ToString());
                    //objProp.vchImgDescription = gvGallery.Rows[counter].Cells[2].Text;
                    objProp.StrAction = "DC";
                    objProp.IntCreatedBy = Convert.ToInt32(Session["UserId"]);
                    retval = objService.DeleteContentData(objProp);
                    if (retval == "3")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('Data Deleted Successfully');", true);
                        BindGridview();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
}