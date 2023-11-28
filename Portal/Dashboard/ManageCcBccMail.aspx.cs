//******************************************************************************************************************
// File Name             :   ManageCcBccMail.aspx.cs
// Description           :   Configure Cc and Bcc Mail for Weekly Tracker
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
using System.Configuration;

public partial class Portal_Dashboard_ManageCcBccMail : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    readonly string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["userid"].ToString() ==null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
            
        }

        if (!IsPostBack)
        {
            Div1.Visible = false;
            Lbl_Msg.Text = "";

            try
            {
                FillDesignation();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "MailConfig");
            }
        }
    }

    /////// Function Used
    #region FunctionUsed

    /////// Bind Designation
    private void FillDesignation()
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
            throw ex;
        }
        finally
        {
        }
    }

    /////// Bind Gridview
    private void FillGrid()
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
            throw ex;
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    /////// Clear Controls Text
    private void ClearFields()
    {
        Txt_Bcc_Mail_Id.Text = "";
        Txt_Cc_Mail_Id.Text = "";
        DrpDwn_Bcc_Enable_Status.SelectedIndex = 1;
        DrpDwn_Cc_Enable_Status.SelectedIndex = 1;
    }

    #endregion

    /////// Search Users
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }

    }
    /////// Update Cc and Bcc Mail
    protected void Btn_Update_Click(object sender, EventArgs e)
    {
        ManageMailBusinessLayer objLayer = new ManageMailBusinessLayer();
        ManageMailEntityLayer objBU_Entity = new ManageMailEntityLayer();
        try
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                HiddenField Hid_Desg_Id = (HiddenField)GridView1.Rows[i].FindControl("Hid_Designation_Id");
                HiddenField Hid_User_Id = (HiddenField)GridView1.Rows[i].FindControl("Hid_User_Id");

                objBU_Entity.strAction = "B";
                objBU_Entity.strSubAction = "cc";
                objBU_Entity.intDesgId = Convert.ToInt32(Hid_Desg_Id.Value);
                objBU_Entity.intUserId = Convert.ToInt32(Hid_User_Id.Value);
                objBU_Entity.strCcMailId = Txt_Cc_Mail_Id.Text.Trim() == "" ? null : Txt_Cc_Mail_Id.Text.Trim();
                objBU_Entity.strCcEnableStatus = DrpDwn_Cc_Enable_Status.SelectedValue;

                objBU_Entity.intCreatedBy = Convert.ToInt32(Session["userid"].ToString());

                string strRetval = objLayer.Manage_Mail_AED(objBU_Entity);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Cc Record Updated Successfully !</strong>','" + strProjName + "')", true);

            FillGrid();
            ClearFields();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
        finally
        {
        }
    }

    /////// Gridview RowDataBound
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

            if (GridView1.EditIndex != e.Row.RowIndex)
            {
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
            }
        }
    }

    protected void Btn_Test_Mail_Click(object sender, EventArgs e)
    {
        string strToMailId =ConfigurationManager.AppSettings["InternalMail2"].ToString();

        if (Txt_Test_Mail_Id.Text.Trim() != "")
        {
            strToMailId = Txt_Test_Mail_Id.Text.Trim();
        }

        SendMail(strToMailId);
    }

    private void SendMail(string strToMailId)
    {
        try
        {
            string strSubject = "Test Mail On " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            string textBody = "This is the mail body for testing";

            string[] arrToMailId = new string[1];
            arrToMailId[0] = strToMailId;

            string[] arrCcMail = new string[0];
            string[] arrBccMail = new string[0];

            CommonHelperCls objComm = new CommonHelperCls();
            bool bMailStatus = objComm.sendMailScheduler(strSubject, textBody, arrToMailId, arrCcMail, arrBccMail, true);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
    }

    protected void Btn_UpdateBcc_Click(object sender, EventArgs e)
    {
        ManageMailBusinessLayer objLayer = new ManageMailBusinessLayer();
        ManageMailEntityLayer objBU_Entity = new ManageMailEntityLayer();
        try
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                HiddenField Hid_Desg_Id = (HiddenField)GridView1.Rows[i].FindControl("Hid_Designation_Id");
                HiddenField Hid_User_Id = (HiddenField)GridView1.Rows[i].FindControl("Hid_User_Id");

                objBU_Entity.strAction = "B";
                objBU_Entity.strSubAction = "bcc";
                objBU_Entity.intDesgId = Convert.ToInt32(Hid_Desg_Id.Value);
                objBU_Entity.intUserId = Convert.ToInt32(Hid_User_Id.Value);
                objBU_Entity.strBccMailId = Txt_Bcc_Mail_Id.Text.Trim() == "" ? null : Txt_Bcc_Mail_Id.Text.Trim();
                objBU_Entity.strBccEnableStatus = DrpDwn_Bcc_Enable_Status.SelectedValue;
                objBU_Entity.intCreatedBy = Convert.ToInt32(Session["userid"].ToString());

                string strRetval = objLayer.Manage_Mail_AED(objBU_Entity);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Bcc Record Updated Successfully !</strong>','" + strProjName + "')", true);

            FillGrid();
            ClearFields();
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

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = e.NewEditIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView1.EditIndex = -1;
            FillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ManageMailBusinessLayer objLayer = new ManageMailBusinessLayer();
        ManageMailEntityLayer objBU_Entity = new ManageMailEntityLayer();
        try
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            HiddenField Hid_User_Id = (HiddenField)row.FindControl("Hid_User_Id");
            DropDownList DrpDwn_Cc_Enable_Sta = (DropDownList)row.FindControl("DrpDwn_Cc_Enable_Sta");
            DropDownList DrpDwn_Bcc_Enable_Status = (DropDownList)row.FindControl("DrpDwn_Bcc_Enable_Status");
            TextBox Txt_Cc_Mail_Id =(TextBox)row.FindControl("Txt_Cc_Mail_Id");
            TextBox Txt_Bcc_Mail_Id = (TextBox)row.FindControl("Txt_Bcc_Mail_Id");

            #region Validation

            if (DrpDwn_Cc_Enable_Sta.SelectedValue == "Y")
            {
                if (Txt_Cc_Mail_Id.Text.Trim() == "")
                {
                    Txt_Cc_Mail_Id.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Cc email id can not be blank !</strong>')", true);
                    return;
                }
            }

            if (DrpDwn_Bcc_Enable_Status.SelectedValue == "Y")
            {
                if (Txt_Bcc_Mail_Id.Text.Trim() == "")
                {
                    Txt_Bcc_Mail_Id.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Bcc email can not be blank !</strong>')", true);
                    return;
                }
            }

            #endregion


            objBU_Entity.strAction = "ED";
            objBU_Entity.intUserId = Convert.ToInt32(Hid_User_Id.Value);
            objBU_Entity.intCreatedBy = Convert.ToInt32(Session["userid"].ToString());
            objBU_Entity.strCcEnableStatus = Convert.ToString(DrpDwn_Cc_Enable_Sta.SelectedItem.Value);
            objBU_Entity.strBccEnableStatus = Convert.ToString(DrpDwn_Bcc_Enable_Status.SelectedItem.Value);
            objBU_Entity.strCcMailId = Convert.ToString(Txt_Cc_Mail_Id.Text);
            objBU_Entity.strBccMailId = Convert.ToString(Txt_Bcc_Mail_Id.Text);

            string strRetval = objLayer.Edit_MailData(objBU_Entity);
            if (strRetval == "1")
            {
                GridView1.EditIndex = -1;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Data Updated Successfully !</strong>','" + strProjName + "')", true);
                FillGrid();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "MailConfig");
        }
    }
}