#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   AddServiceCMS.aspx.cs
// Description           :   Add Service Instruction Content
// Created by            :   Prasun Kali
// Created On            :   20 September 2017
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
using System.IO;
#endregion

public partial class Portal_CMS_AddServiceCMS : System.Web.UI.Page
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
            FillServices();
            //if (Request.QueryString["id"] != null)
            //{
            //    editData(Convert.ToInt32(Request.QueryString["id"]));
            //}
        }
    }
    #endregion

    #region Common_Functions
    /// <summary>
    /// Fill Menu Dropdownlist
    /// </summary>
    private void FillServices()
    {
        IList<CMSDetails> objIndList = new List<CMSDetails>();
        ddlServices.DataSource = objService.GetServices();
        ddlServices.DataTextField = "StrServicename";
        ddlServices.DataValueField = "IntServiceId";
        ddlServices.DataBind();
        ddlServices.Items.Insert(0, new ListItem("-Select-", "0"));
        

    }
    //public void editData(int id)
    //{
    //    try
    //    {
    //        EntityLayer.CMS.CMSDetails objdata = new EntityLayer.CMS.CMSDetails();
    //        objdata = objService.EditCMS(id);
    //        if (objdata != null)
    //        {
    //            ddlServices.SelectedValue = objdata.IntMenuId.ToString();
    //            ddlServices.Enabled = false;
    //            txtRemark.Text = objdata.StrContent;
    //            ViewState["id"] = id;
    //            btnSave.Text = "Update";
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "alert('" + ex.ToString().Replace("'", "") + "');", true);
    //    }
    //}
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
                objServiceEntity.StrAction = "USC";
                objServiceEntity.IntServiceId = Convert.ToInt32(ddlServices.SelectedValue);
                

            }
            else
            {
                objServiceEntity.StrAction = "ADS";
                objServiceEntity.IntServiceId = Convert.ToInt32(ddlServices.SelectedValue);
            }
            objServiceEntity.IntCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objServiceEntity.StrContent = txtRemark.Text;

            string filename1 = "";
          
            string folderPath = "";

            if (FileUploadManual.HasFile)
            {
                filename1 = string.Format("Manual_" + objServiceEntity.IntServiceId + "_{0:yyyy_MM_dd_hh_mm_ss_tt_}_ .pdf", DateTime.Now);

                folderPath = Server.MapPath("~/Document/UserManual/");

                //Check whether Directory (Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }
                objServiceEntity.strAttachment = "~/Document/UserManual/" + filename1;
                //Save the File to the Directory (Folder).
                FileUploadManual.SaveAs(folderPath + filename1);
            }
            else
            {

                objServiceEntity.strAttachment = HyprDownload.NavigateUrl;
            }
           

            str_Retvalue = objService.ManageServiceCMS(objServiceEntity);
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
        btnSave.Text = "Save";
        ddlServices.SelectedValue = "0";
        txtRemark.Text = "";
        HyprDownload.NavigateUrl = "";
        HyprDownload.Visible = false;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddServiceCMS.aspx");
    }
    protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            EntityLayer.CMS.CMSDetails objServiceEntity = new EntityLayer.CMS.CMSDetails();
            int intServiceid = Convert.ToInt32(ddlServices.SelectedValue);
            objServiceEntity.StrAction= "GCD";
            objServiceEntity.IntServiceId = intServiceid;
            IList < CMSDetails > obj= objService.GetCMSData(objServiceEntity);
            if (obj.Count > 0)
            {
                btnSave.Text = "Update";
                //ddlServices.SelectedValue = (dt.Rows[0]["intMenuId"]).ToString();
                txtRemark.Text = obj[0].StrContent;
                if (obj[0].strAttachment != "")
                {
                    HyprDownload.NavigateUrl = obj[0].strAttachment;
                    HyprDownload.Visible = true;
                    LnkRemove.Visible = true;
                }
            }
            else
            {
                txtRemark.Text = "";
                HyprDownload.Visible = false;
                LnkRemove.Visible = false;
                btnSave.Text = "Submit";
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    protected void LnkRemove_Click(object sender, EventArgs e)
    {
        HyprDownload.NavigateUrl = "";
        HyprDownload.Visible = false;
        LnkRemove.Visible = false;
    }
}