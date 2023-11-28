#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   ViewImageGallery.aspx.cs
// Description           :   View ImageGallery
// Created by            :   Sanghamitra Samal
// Created On            :   05 Sep 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Common;
using EntityLayer.Common;

public partial class Miscellaneous_ViewImageGallery : SessionCheck
{
    #region Variable Declaration
    CommonBusinessLayer objService = new CommonBusinessLayer();
    EntityLayer.Common.Gallery objProp = new EntityLayer.Common.Gallery();
    int intcount = 0;
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
        List<Gallery> objList = new List<Gallery>();
        try
        {
            objProp.strAction = "V";
            objList = objService.ViewGallery(objProp).ToList();
            gvGallery.DataSource = objList;
            intcount = objList.Count;
            gvGallery.DataBind();
            DisplayPaging(intcount);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    #endregion

    #region GridView_Events
    protected void gvGallery_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string str = gvGallery.DataKeys[e.NewEditIndex].Value.ToString();
            Response.Redirect("AddImageGallery.aspx?id=" + str);
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    #endregion
    #region "Display Paging"
    protected void DisplayPaging(int intRecCount)//Disply Paging of Gridview
    {
        try
        {
            if (gvGallery.Rows.Count > 0)
            {
                this.lblPaging.Visible = true;
                gvGallery.Visible = true;
                if (gvGallery.PageIndex + 1 == gvGallery.PageCount)
                {
                    this.lblPaging.Text = "Results " + "<b>" + (Convert.ToInt32((gvGallery.PageIndex * gvGallery.PageSize)) + 1) + "</b> - <b>" + intRecCount + " " + "of" + " " + intcount + "</b>";
                }
                else
                {
                    this.lblPaging.Text = "Results " + "<b>" + (Convert.ToInt32((gvGallery.PageIndex * gvGallery.PageSize)) + 1) + "</b> - <b>" + ((gvGallery.PageIndex + 1) * gvGallery.PageSize) + " " + "of" + " " + intcount + "</b>";
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
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    #endregion
    #region Button_Click
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CheckBox chkDelete = new CheckBox();
        string retval;
        try
        {
            for (int counter = 0; counter <= gvGallery.Rows.Count - 1; counter++)
            {
                chkDelete = (CheckBox)(gvGallery.Rows[counter].FindControl("chkItem"));
                if (chkDelete.Checked == true)
                {
                    objProp.intImageId = Convert.ToInt32(gvGallery.DataKeys[counter].Value.ToString());
                    objProp.vchImgDescription = gvGallery.Rows[counter].Cells[2].Text;
                    objProp.strAction = "D";
                    objProp.intUpdatedBy = 1;//Convert.ToInt32(Session["UserId"]);                   
                    retval = objService.DeleteGalleryData(objProp);
                    if (retval == "3")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('Data Deleted Successfully');", true);
                    }
                }
            }

            BindGridview();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            gvGallery.PageIndex = 0;
            gvGallery.AllowPaging = false;
            BindGridview();
        }
        else
        {
            lbtnAll.Text = "All";
            gvGallery.AllowPaging = true;
            BindGridview();
        }

    }
    #endregion
    protected void gvGallery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGallery.PageIndex = e.NewPageIndex;
        BindGridview();
    }

    protected void LnkBtn_View_Image_Small_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

        Label Lbl_Img_Name_G = (Label)row.FindControl("Lbl_Img_Name_G");
        HiddenField Hid_Image_File_Name = (HiddenField)row.FindControl("Hid_Image_File_Name");

        string strFileName = Hid_Image_File_Name.Value;
        Lbl_Img_Desc.Text = Lbl_Img_Name_G.Text;

        Img_View.ImageUrl = "~/Portal/ImageGallery/" + "S_" + strFileName;
        ModalPopupExtender1.Show();
    }
    protected void LnkBtn_View_Image_Medium_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

        Label Lbl_Img_Name_G = (Label)row.FindControl("Lbl_Img_Name_G");
        HiddenField Hid_Image_File_Name = (HiddenField)row.FindControl("Hid_Image_File_Name");

        string strFileName = Hid_Image_File_Name.Value;
        Lbl_Img_Desc.Text = Lbl_Img_Name_G.Text;

        Img_View.ImageUrl = "~/Portal/ImageGallery/" + "M_" + strFileName;
        ModalPopupExtender1.Show();
    }
    protected void LnkBtn_View_Image_Big_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

        Label Lbl_Img_Name_G = (Label)row.FindControl("Lbl_Img_Name_G");
        HiddenField Hid_Image_File_Name = (HiddenField)row.FindControl("Hid_Image_File_Name");

        string strFileName = Hid_Image_File_Name.Value;
        Lbl_Img_Desc.Text = Lbl_Img_Name_G.Text;

        Img_View.ImageUrl = "~/Portal/ImageGallery/" + "B_" + strFileName;
        ModalPopupExtender1.Show();
    }
}