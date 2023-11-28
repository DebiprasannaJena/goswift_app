//******************************************************************************************************************
// File Name             :   User_Management.aspx.cs
// Description           :   User approval and set permission for child users.
// Created by            :   Sushant Kumar Jena
// Created on            :   09-Aug-2018
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
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;

public partial class InvestorPortal_User_Management : SessionCheck
{
    #region GlobalVariables

    static int investorid;
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    #endregion

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] != null)
        {
            investorid = Convert.ToInt32(Session["InvestorId"]);
        }

        if (!IsPostBack)
        {
            fillGrid();
        }
    }

    #region FunctionUsed

    /// <summary>
    /// Fill the child users against the parent user
    /// </summary>
    private void fillGrid()
    {
        InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        InvestorDetails objEntity = new InvestorDetails();

        DataTable dt = new DataTable();
        try
        {
            objEntity.strAction = "V";
            objEntity.IntInvestorId = investorid; ///// Here investor id is the parent investor

            /////// Select Data
            dt = objBAL.UserManagementView(objEntity);

            GrdChildUser.DataSource = dt;
            GrdChildUser.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserPermission");
        }
        finally
        {
            dt = null;
            objBAL = null;
            objEntity = null;
        }
    }
    /// <summary>
    /// Fill child users for which the permission to be granted.
    /// </summary>
    /// <param name="intInvestorId"></param>
    /// <param name="intParentId"></param>
    /// <param name="strInvestorName"></param>
    /// <param name="strUserId"></param>
    private void fillUserToBePermitted(int intInvestorId, int intParentId, string strInvestorName, string strUserId)
    {
        InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        InvestorDetails objEntity = new InvestorDetails();

        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        try
        {
            objEntity.strAction = "V1";
            objEntity.IntInvestorId = intInvestorId;
            objEntity.IntParentId = intParentId;

            /////// Select Data
            dt1 = objBAL.UserManagementView(objEntity);

            /*-------------------------------------------------------------------*/
            //// Fill the users to check whether the permission has already been granted or not.
            //// If granted then autofill the permission type
            /*-------------------------------------------------------------------*/

            objEntity.strAction = "V2";
            objEntity.IntInvestorId = intInvestorId;

            /////// Select Data
            dt2 = objBAL.UserManagementView(objEntity);
            if (dt2.Rows.Count > 0)
            {
                ViewState["readuser"] = Convert.ToString(dt2.Rows[0]["VCH_READ_USER_PERMISSION"]);
                //ViewState["writeuser"] = Convert.ToString(dt2.Rows[0]["VCH_WRITE_USER_PERMISSION"]);
            }
            else
            {
                ViewState["readuser"] = null;
                //ViewState["writeuser"] = null;
            }

            /*-------------------------------------------------------------------*/

            GrdPermission.DataSource = dt1;
            GrdPermission.DataBind();

            Lbl_Investor_Name_Assign.Text = strInvestorName;
            Lbl_User_Id_Assign.Text = strUserId;
            Hid_Investor_Id_Assign.Value = intInvestorId.ToString();

            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserPermission");
        }
        finally
        {
            dt1 = null;
            objBAL = null;
            objEntity = null;
        }
    }

    #endregion

    #region GridViewEvenets

    /////// RowDataBound Event for Child User List Gridview
    protected void GrdChildUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Lbl_Approval_Status = (Label)e.Row.FindControl("Lbl_Approval_Status");
            HiddenField Hid_Approval_Status = (HiddenField)e.Row.FindControl("Hid_Approval_Status");
            Button Btn_Action = (Button)e.Row.FindControl("Btn_Action");

            if (Hid_Approval_Status.Value == "0")
            {
                Btn_Action.Visible = false;
                Lbl_Approval_Status.ForeColor = System.Drawing.Color.Red;
            }
            else if (Hid_Approval_Status.Value == "1")
            {
                Btn_Action.Visible = true;
                Btn_Action.Text = "Assign";
                Btn_Action.CssClass = "btn btn-success";
                Lbl_Approval_Status.ForeColor = System.Drawing.Color.Green;
            }
        }
    }

    /////// RowDataBound Event for Grant Permission Gridview
    protected void GrdPermission_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_Investor_Id_2 = (HiddenField)e.Row.FindControl("Hid_Investor_Id_2");
            CheckBox ChkBx_Select_User = (CheckBox)e.Row.FindControl("ChkBx_Select_User");
            //RadioButtonList RadBtn_Permission_Type = (RadioButtonList)e.Row.FindControl("RadBtn_Permission_Type");

            if (ViewState["readuser"] != null)
            {
                string strReadUser = ViewState["readuser"].ToString();

                if (strReadUser.Contains(Hid_Investor_Id_2.Value))
                {
                    //RadBtn_Permission_Type.SelectedValue = "1";
                    ChkBx_Select_User.Checked = true;
                }
            }

            //if (ViewState["writeuser"] != null)
            //{
            //    string strWriteUser = ViewState["writeuser"].ToString();

            //    if (strWriteUser.Contains(Hid_Investor_Id_2.Value))
            //    {
            //        RadBtn_Permission_Type.SelectedValue = "2";
            //    }
            //}
        }
    }

    #endregion

    /////// Button Action for Approval and Assign Permission
    protected void Btn_Action_Click(object sender, EventArgs e)
    {
        InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        InvestorDetails objEntity = new InvestorDetails();

        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            /*--------------------------------------------------------------------------*/

            HiddenField Hid_Approval_Status = (HiddenField)row.FindControl("Hid_Approval_Status");
            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");
            HiddenField Hid_Parent_Id = (HiddenField)row.FindControl("Hid_Parent_Id");
            Label Lbl_Investor_Name = (Label)row.FindControl("Lbl_Investor_Name");
            Label Lbl_User_Id = (Label)row.FindControl("Lbl_User_Id");

            /*--------------------------------------------------------------------------*/

            if (Hid_Approval_Status.Value == "0") ///// Pending (So Approve action to be fired)
            {
                //objEntity.strAction = "AP";
                //objEntity.IntInvestorId = Convert.ToInt32(Hid_Investor_Id.Value);

                ////////// DML Operation
                //string strReturnStatus = objBAL.UserManagementAED(objEntity);
                //if (strReturnStatus == "1")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>User Approved Successfully !</strong>', '" + strProjName + "'); </script>", false);
                //    fillGrid();
                //}
                //else if (strReturnStatus == "2")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Internal Server Error,Please Try After Sometime !</strong>', '" + strProjName + "'); </script>", false);
                //}
            }
            else if (Hid_Approval_Status.Value == "1") ///// Approved (So Assign Action to be fired)
            {
                fillUserToBePermitted(Convert.ToInt32(Hid_Investor_Id.Value), Convert.ToInt32(Hid_Parent_Id.Value), Lbl_Investor_Name.Text, Lbl_User_Id.Text);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserPermission");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }

    /////// Button Submit for Set Permission
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();

        InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        InvestorDetails objEntity = new InvestorDetails();
        try
        {
            string strReadUserPermission = "";
            //string strWriteUserPermission = "";

            if (GrdPermission.Rows.Count > 0)
            {
                int intCheckCount = 0;

                for (int i = 0; i < GrdPermission.Rows.Count; i++)
                {
                    //RadioButtonList radBtn = (RadioButtonList)GrdPermission.Rows[i].FindControl("RadBtn_Permission_Type");
                    HiddenField Hid_Child_Investor_Id = (HiddenField)GrdPermission.Rows[i].FindControl("Hid_Investor_Id_2");
                    CheckBox chkBxSelect = (CheckBox)GrdPermission.Rows[i].FindControl("ChkBx_Select_User");

                    if (chkBxSelect.Checked == true)
                    {
                        intCheckCount = 1;
                        strReadUserPermission = strReadUserPermission + "," + Hid_Child_Investor_Id.Value;
                    }

                    //if (radBtn.SelectedValue == "1") ///// Read Permission
                    //{
                    //    strReadUserPermission = strReadUserPermission + "," + Hid_Child_Investor_Id.Value;
                    //}
                    //else if (radBtn.SelectedValue == "2") ///// Write Permission
                    //{
                    //    strWriteUserPermission = strWriteUserPermission + "," + Hid_Child_Investor_Id.Value;
                    //}
                }

                /*--------------------------------------------------------------------------*/
                ///// Alert if No check box selected
                if (intCheckCount == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please select at least one record to provide permission !</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }

                /*--------------------------------------------------------------------------*/

                objEntity.strAction = "SP";
                objEntity.IntInvestorId = Convert.ToInt32(Hid_Investor_Id_Assign.Value);
                objEntity.strReadUserPermission = strReadUserPermission.TrimStart(',');
                objEntity.strWriteUserPermission = null;// strWriteUserPermission.TrimStart(',');
                objEntity.IntCreatedBy = investorid;

                //////// DML Operation
                string strReturnStatus = objBAL.UserManagementAED(objEntity);
                if (strReturnStatus == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Permission Granted Successfully !</strong>', '" + strProjName + "'); </script>", false);
                    ModalPopupExtender1.Hide();
                }
                else if (strReturnStatus == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Internal Server Error,Please Try After Sometime !</strong>', '" + strProjName + "'); </script>", false);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserPermission");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }
}