//******************************************************************************************************************
// File Name             :   ManageWeeklyMail.aspx.cs
// Description           :   Manage Weekly Mail,Send weekly mail manually,Manage internal user for mail testing
// Created by            :   Sushant Kumar Jena
// Created on            :   30-Mar-2018
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

public partial class Portal_Dashboard_ManageWeeklyMail : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        Div1.Visible = false;
        DivInternalUser.Visible = false;

        Lbl_Msg.Text = "";

        if (!IsPostBack)
        {
            fillDesignation();
            checkInternalMailStatus();
            fillGridSpamStatus();
        }
    }

    /////// Function Used
    #region FunctionUsed

    /////// Bind Designation
    private void fillDesignation()
    {
        ManageMailEntityLayer objEntity = new ManageMailEntityLayer();
        ManageMailBusinessLayer objBAL = new ManageMailBusinessLayer();
        try
        {
            objEntity.strAction = "A";
            DataSet ds = new DataSet();
            ds = objBAL.Manage_Mail_View(objEntity);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DrpDwn_Designation.DataTextField = "nvchDesigName";
                DrpDwn_Designation.DataValueField = "intDesigId";
                DrpDwn_Designation.DataSource = ds.Tables[0];
                DrpDwn_Designation.DataBind();
                DrpDwn_Designation.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    /////// Bind Designation
    private void checkInternalMailStatus()
    {
        ManageMailEntityLayer objEntity = new ManageMailEntityLayer();
        ManageMailBusinessLayer objBAL = new ManageMailBusinessLayer();
        try
        {
            objEntity.strAction = "E";
            DataSet ds = new DataSet();
            ds = objBAL.Manage_Mail_View(objEntity);

            if (ds.Tables[0].Rows.Count > 0)
            {
                int intCount = Convert.ToInt32(ds.Tables[0].Rows[0]["ActiveCount"]);
                if (intCount > 0)
                {
                    Lbl_Internal_Mail_Status.Text = "<span style='font-family:Verdana;font-size:12px;'>Internal mail is in <span style='color:#17f213;font-weight:800;'>ACTIVE</span> stage.So all the mails will be sent to internal users.<span>";
                }
                else
                {
                    Lbl_Internal_Mail_Status.Text = "<span style='font-family:Verdana;font-size:12px;'>Internal mail is in <span style='color:red;font-weight:800;'>INACTIVE</span> stage.So all the mails will be sent to respective live users.<span>";
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    /////// Bind Gridview for Valid Users
    private void fillGrid()
    {
        ManageMailEntityLayer objEntity = new ManageMailEntityLayer();
        ManageMailBusinessLayer objBAL = new ManageMailBusinessLayer();
        try
        {
            objEntity.strAction = "B";

            if (DrpDwn_Designation.SelectedIndex > 0)
            {
                objEntity.intDesgId = Convert.ToInt32(DrpDwn_Designation.SelectedValue);
            }

            if (DrpDwn_Mail_Status.SelectedIndex > 0)
            {
                objEntity.strMailStatus = DrpDwn_Mail_Status.SelectedValue;
            }

            DataSet ds = new DataSet();
            ds = objBAL.Manage_Mail_View(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();

                Lbl_Msg.Text = "";
                Div1.Visible = true;
            }
            else
            {
                Lbl_Msg.Text = "No Records Found !!";
                Div1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    /////// Bind Gridview for Internal User
    private void fillGridInternalUser()
    {
        ManageMailEntityLayer objEntity = new ManageMailEntityLayer();
        ManageMailBusinessLayer objBAL = new ManageMailBusinessLayer();
        try
        {
            objEntity.strAction = "D";
            DataSet ds = new DataSet();
            ds = objBAL.Manage_Mail_View(objEntity);

            Grd_Internal_User.DataSource = ds.Tables[0];
            Grd_Internal_User.DataBind();

            DivInternalUser.Visible = true;
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    /////// Bind Gridview for Valid Users
    private void fillGridSpamStatus()
    {
        ManageMailEntityLayer objEntity = new ManageMailEntityLayer();
        ManageMailBusinessLayer objBAL = new ManageMailBusinessLayer();
        try
        {
            objEntity.strAction = "F";

            DataSet ds = new DataSet();
            ds = objBAL.Manage_Mail_View(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grd_Mail_Config.DataSource = ds.Tables[0];
                Grd_Mail_Config.DataBind();
                Grd_Mail_Config.Visible = true;
            }
            else
            {
                Grd_Mail_Config.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }


    #endregion

    /////// Manage User Section
    #region ManageUserSection

    /////// View Users
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        fillGrid();
    }

    /////// Update Mail Enable Status
    protected void Btn_Update_Click(object sender, EventArgs e)
    {
        ManageMailBusinessLayer objLayer = new ManageMailBusinessLayer();
        ManageMailEntityLayer objBU_Entity = new ManageMailEntityLayer();
        string strId = string.Empty;
        try
        {
            objBU_Entity.strAction = "A";
            objBU_Entity.strMailStatus = DrpDwn_Enable_Status.SelectedValue;
            foreach (GridViewRow gvRow in GridView1.Rows)
            {
                CheckBox chkBoxID = (CheckBox)gvRow.FindControl("chkSelectSingle");
                if (chkBoxID.Checked == true)
                {
                    HiddenField Hid_User_Id = (HiddenField)gvRow.FindControl("Hid_User_Id");
                    strId += Hid_User_Id.Value + ",";
                }
            }
            objBU_Entity.strIds = strId.TrimEnd(',');

            string strRetval = objLayer.Manage_Mail_AED(objBU_Entity);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Record Updated Successfully !</strong>','" + strProjName + "')", true);
            fillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objLayer = null;
            objBU_Entity = null;
        }
    }

    /////// GridView RowDataBound
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            /*------------------------------------------------------------------*/
            ///// To Mail Id

            Label Lbl_Mail_Status = (Label)e.Row.FindControl("Lbl_Mail_Status");
            if (Lbl_Mail_Status.Text == "Y")
            {
                Lbl_Mail_Status.ForeColor = System.Drawing.Color.GreenYellow;
                Lbl_Mail_Status.Font.Bold = true;
            }
            else if (Lbl_Mail_Status.Text == "N")
            {
                Lbl_Mail_Status.ForeColor = System.Drawing.Color.Red;
            }

            /*------------------------------------------------------------------*/
            ///// Cc Mail Id

            Label Lbl_Cc_Enable_Status = (Label)e.Row.FindControl("Lbl_Cc_Enable_Status");
            if (Lbl_Cc_Enable_Status.Text == "Y")
            {
                Lbl_Cc_Enable_Status.ForeColor = System.Drawing.Color.GreenYellow;
                Lbl_Cc_Enable_Status.Font.Bold = true;
            }
            else if (Lbl_Cc_Enable_Status.Text == "N")
            {
                Lbl_Cc_Enable_Status.ForeColor = System.Drawing.Color.Red;
            }

            HiddenField Hid_Cc_Mail_Ids = (HiddenField)e.Row.FindControl("Hid_Cc_Mail_Ids");
            Lbl_Cc_Enable_Status.ToolTip = Hid_Cc_Mail_Ids.Value;

            /*------------------------------------------------------------------*/
            ///// Bcc Mail Id

            Label Lbl_Bcc_Enable_Status = (Label)e.Row.FindControl("Lbl_Bcc_Enable_Status");
            if (Lbl_Bcc_Enable_Status.Text == "Y")
            {
                Lbl_Bcc_Enable_Status.ForeColor = System.Drawing.Color.GreenYellow;
                Lbl_Bcc_Enable_Status.Font.Bold = true;
            }
            else if (Lbl_Bcc_Enable_Status.Text == "N")
            {
                Lbl_Bcc_Enable_Status.ForeColor = System.Drawing.Color.Red;
            }

            HiddenField Hid_Bcc_Mail_Ids = (HiddenField)e.Row.FindControl("Hid_Bcc_Mail_Ids");
            Lbl_Bcc_Enable_Status.ToolTip = Hid_Bcc_Mail_Ids.Value;
        }
    }

    #endregion

    /////// Internal User Section
    #region ManageInternalUserSection

    /////// View Internal Mails
    protected void Btn_View_Internal_Mail_Click(object sender, EventArgs e)
    {
        fillGridInternalUser();
    }

    /////// GridView RowDatabound for Internal Mail
    protected void Grd_Internal_User_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Lbl_Mail_Status_Internal = (Label)e.Row.FindControl("Lbl_Mail_Status_Internal");
            Button Btn_Action = (Button)e.Row.FindControl("Btn_Action");

            if (Lbl_Mail_Status_Internal.Text.ToUpper() == "ACTIVE")
            {
                Lbl_Mail_Status_Internal.ForeColor = System.Drawing.Color.GreenYellow;
                Btn_Action.Text = "De-Activate";
                Btn_Action.CssClass = "btn btn-success";
            }
            else if (Lbl_Mail_Status_Internal.Text.ToUpper() == "INACTIVE")
            {
                Lbl_Mail_Status_Internal.ForeColor = System.Drawing.Color.Red;
                Btn_Action.Text = "Activate";
                Btn_Action.CssClass = "btn btn-danger";
            }
        }
    }

    /////// Activate and Deactivate Internal Mails
    protected void Btn_Action_Click(object sender, EventArgs e)
    {
        ManageMailBusinessLayer objLayer = new ManageMailBusinessLayer();
        ManageMailEntityLayer objBU_Entity = new ManageMailEntityLayer();
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            Label Lbl_Mail_Status_Internal = (Label)row.FindControl("Lbl_Mail_Status_Internal");
            HiddenField Hid_Serial_No = (HiddenField)row.FindControl("Hid_Serial_No");

            objBU_Entity.strAction = "D";
            if (Lbl_Mail_Status_Internal.Text.ToUpper() == "ACTIVE")
            {
                objBU_Entity.strMailStatus = "I";
            }
            else
            {
                objBU_Entity.strMailStatus = "A";
            }

            objBU_Entity.intSerialNo = Convert.ToInt32(Hid_Serial_No.Value);

            string strRetval = objLayer.Manage_Mail_AED(objBU_Entity);
            if (strRetval == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Action Taken Successfully !</strong>','" + strProjName + "')", true);
                fillGridInternalUser();
                checkInternalMailStatus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Internal Server Error,Please Try Again !</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objLayer = null;
            objBU_Entity = null;
        }
    }

    /////// Delete Internal Mails
    protected void LnkBtn_Delete_Click(object sender, EventArgs e)
    {
        ManageMailBusinessLayer objLayer = new ManageMailBusinessLayer();
        ManageMailEntityLayer objBU_Entity = new ManageMailEntityLayer();
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            HiddenField Hid_Serial_No = (HiddenField)row.FindControl("Hid_Serial_No");

            objBU_Entity.strAction = "E";
            objBU_Entity.intSerialNo = Convert.ToInt32(Hid_Serial_No.Value);

            string strRetval = objLayer.Manage_Mail_AED(objBU_Entity);
            if (strRetval == "3")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Record Deleted Successfully !</strong>','" + strProjName + "')", true);
                fillGridInternalUser();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Internal Server Error,Please Try Again !</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objLayer = null;
            objBU_Entity = null;
        }
    }

    /////// Add Internal Mails
    protected void Btn_Add_Click(object sender, EventArgs e)
    {
        ManageMailBusinessLayer objLayer = new ManageMailBusinessLayer();
        ManageMailEntityLayer objBU_Entity = new ManageMailEntityLayer();
        try
        {
            objBU_Entity.strAction = "C";
            objBU_Entity.strMailId = Txt_Mail_Id.Text.Trim();

            string strRetval = objLayer.Manage_Mail_AED(objBU_Entity);
            if (strRetval == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Mail is already exists !</strong>','" + strProjName + "')", true);
                DivInternalUser.Visible = true;
            }
            else if (strRetval == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Mail Added Successfully !</strong>','" + strProjName + "')", true);
                fillGridInternalUser();
                Txt_Mail_Id.Text = "";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Internal server error,Please try again !</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objLayer = null;
            objBU_Entity = null;
        }
    }

    #endregion

    /////// Send Mail Manually
    protected void Btn_Manual_Mail_Click(object sender, EventArgs e)
    {
        MailMasterTracker c1 = new MailMasterTracker();
        c1.mailScheduleManual();
        Lbl_Msg.Text = "Mail Sent Sucessfully !!";
    }

    #region ManageSpamMsgSection

    protected void Grd_Mail_Config_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            /*------------------------------------------------------------------*/
            ///// To Mail Id

            Label Lbl_Spam_Mode = (Label)e.Row.FindControl("Lbl_Spam_Mode");
            Button Btn_Update_Spam_Mode = (Button)e.Row.FindControl("Btn_Update_Spam_Mode");
            if (Lbl_Spam_Mode.Text == "ON")
            {
                Btn_Update_Spam_Mode.Text = "OFF";
                Btn_Update_Spam_Mode.CssClass = "btn btn-success";
                Lbl_Spam_Mode.ForeColor = Color.Red;
            }
            else if (Lbl_Spam_Mode.Text == "OFF")
            {
                Btn_Update_Spam_Mode.Text = "ON";
                Btn_Update_Spam_Mode.CssClass = "btn btn-danger";
                Lbl_Spam_Mode.ForeColor = Color.Green;
            }
        }
    }
    protected void Btn_Update_Spam_Mode_Click(object sender, EventArgs e)
    {
        ManageMailBusinessLayer objLayer = new ManageMailBusinessLayer();
        ManageMailEntityLayer objBU_Entity = new ManageMailEntityLayer();
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            Button Btn_Update_Spam_Mode = (Button)row.FindControl("Btn_Update_Spam_Mode");

            objBU_Entity.strAction = "F";
            objBU_Entity.strSpamMode = Btn_Update_Spam_Mode.Text;

            string strRetval = objLayer.Manage_Mail_AED(objBU_Entity);
            if (strRetval == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Data Updated Successfully !</strong>','" + strProjName + "')", true);
                fillGridSpamStatus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Internal Server Error,Please Try Again !</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objLayer = null;
            objBU_Entity = null;
        }
    }

    protected void Grd_Mail_Config_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        Grd_Mail_Config.EditIndex = -1;
        fillGridSpamStatus();
    }
    protected void Grd_Mail_Config_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        Grd_Mail_Config.EditIndex = e.NewEditIndex;
        fillGridSpamStatus();
    }
    protected void Grd_Mail_Config_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        ManageMailBusinessLayer objLayer = new ManageMailBusinessLayer();
        ManageMailEntityLayer objBU_Entity = new ManageMailEntityLayer();

        try
        {
            GridViewRow row = Grd_Mail_Config.Rows[e.RowIndex];
            TextBox Txt_Spam_Text = (TextBox)row.FindControl("Txt_Spam_Text");

            objBU_Entity.strAction = "G";
            objBU_Entity.strSpamText = Txt_Spam_Text.Text.Trim();

            string strRetval = objLayer.Manage_Mail_AED(objBU_Entity);
            if (strRetval == "1")
            {
                Grd_Mail_Config.EditIndex = -1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Data Updated Successfully !</strong>','" + strProjName + "')", true);
                fillGridSpamStatus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Internal Server Error,Please Try Again !</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
            objLayer = null;
            objBU_Entity = null;
        }
    }

    #endregion
}