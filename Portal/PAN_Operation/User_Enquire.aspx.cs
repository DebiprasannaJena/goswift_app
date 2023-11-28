#region  PageInfo
//******************************************************************************************************************
// File Name             :   User_Enquiry.aspx.cs
// Description           :   Enquiry User Details
// Created by            :   Sushant Jena
// Created On            :   17-Nov-2018
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
using System.Data.SqlClient;
using System.Data;
using DWHServiceReference;
using System.Configuration;

public partial class Portal_PAN_Operation_User_Enquire : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /////// Function Used
    #region FunctionUsed

    ///// Fill Pending Units   
    private void fillGrid()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }

        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataTable objdt = new DataTable();
        try
        {
            objCommand.CommandText = "USP_V2_PARENT_USER_SWAPPING";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", 'S');
            objCommand.Parameters.AddWithValue("@P_VCH_EMAIL_ID", Txt_Email_Id.Text);
            objCommand.Parameters.AddWithValue("@P_VCH_PAN", Txt_PAN.Text);
            objCommand.Parameters.AddWithValue("@P_VCH_INV_NAME", Txt_Unit_Name.Text);
            objCommand.Parameters.AddWithValue("@P_VCH_INV_SWS_ID", Txt_SWS_Id.Text);
            objCommand.Parameters.AddWithValue("@P_VCH_OFF_MOBILE", Txt_MobileNo.Text);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objdt);

            GridView1.DataSource = objdt;
            GridView1.DataBind();

            GridView1.Visible = true;
            GridView2.Visible = false;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objdt = null;
            objCommand = null;
            objDa = null;
        }
    }

    private void fillGridRejected()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }

        SqlCommand objCommand = new SqlCommand();
        SqlDataAdapter objDa = new SqlDataAdapter();
        DataTable objdt = new DataTable();
        try
        {
            objCommand.CommandText = "USP_V2_PARENT_USER_SWAPPING";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "VR"); //// View Rejected User
            objCommand.Parameters.AddWithValue("@P_VCH_EMAIL_ID", Txt_Email_Id.Text);
            objCommand.Parameters.AddWithValue("@P_VCH_PAN", Txt_PAN.Text);
            objCommand.Parameters.AddWithValue("@P_VCH_INV_NAME", Txt_Unit_Name.Text);
            objCommand.Parameters.AddWithValue("@P_VCH_OFF_MOBILE", Txt_MobileNo.Text);

            objDa.SelectCommand = objCommand;
            objDa.Fill(objdt);

            GridView2.DataSource = objdt;
            GridView2.DataBind();

            GridView1.Visible = false;
            GridView2.Visible = true;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            objdt = null;
            objCommand = null;
            objDa = null;
        }
    }

    ///// Clear Fields
    private void clearField()
    {
        Txt_New_Pwd.Text = "";
        Txt_Confirm_Pwd.Text = "";

        Lbl_Application_Status.Text = "";
        Lbl_Email_Id_OTP.Text = "";
        Lbl_Email_Id_ResetPwd.Text = "";
        Lbl_Investor_Name_OTP.Text = "";
        Lbl_Investor_Name_ResetPwd.Text = "";
        Lbl_OTP.Text = "";
        Lbl_OTP_Status.Text = "";
        Lbl_OTP_Time.Text = "";
        Lbl_PAN_OTP.Text = "";
        Lbl_PAN_ResetPwd.Text = "";
        Lbl_User_Id_OTP.Text = "";
        Lbl_User_Id_ResetPwd.Text = "";

        Hid_Application_Status.Value = "";
        Hid_Investor_Id_OTP.Value = "";
        Hid_Investor_Id_ResetPwd.Value = "";
        Hid_OTP_Status.Value = "";
    }

    #endregion

    /////// Gridview RowDataBound
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Lbl_Status = (Label)e.Row.FindControl("Lbl_Status");

            if (Lbl_Status.Text == "Approved")
            {
                Lbl_Status.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                Lbl_Status.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    /////// Button Click Evenets
    #region ButtonClickEvents

    /////// Button Search
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txt_Email_Id.Text.Trim() == "" && Txt_PAN.Text.Trim() == "" && Txt_Unit_Name.Text.Trim() == "" && Txt_SWS_Id.Text.Trim() == "" && Txt_MobileNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please enter any one field !</strong>', '" + strProjName + "'); </script>", false);
                return;
            }

            fillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
    }

    /////// Button Reset
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        Txt_Unit_Name.Text = "";
        Txt_PAN.Text = "";
        Txt_Email_Id.Text = "";
        Txt_SWS_Id.Text = "";
        Txt_MobileNo.Text = "";
    }

    /////// Confirm Before Reset Password
    protected void LnkBtn_Reset_Pwd_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            //Label Lbl_Investor_Name = (Label)row.FindControl("Lbl_Investor_Name");
            Label Lbl_New_User_Id = (Label)row.FindControl("Lbl_New_User_Id");
            Label Lbl_Email_Id = (Label)row.FindControl("Lbl_Email_Id");
            Label Lbl_PAN = (Label)row.FindControl("Lbl_PAN");
            HiddenField Hid_Unique_Id = (HiddenField)row.FindControl("Hid_Unique_Id");
            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");
            LinkButton LnkBtn_Inv_Name = (LinkButton)row.FindControl("LnkBtn_Inv_Name");
            HiddenField Hid_Regd_Source = (HiddenField)row.FindControl("Hid_Regd_Source");

            Lbl_Investor_Name_ResetPwd.Text = LnkBtn_Inv_Name.Text;
            Lbl_User_Id_ResetPwd.Text = Lbl_New_User_Id.Text;
            Lbl_Email_Id_ResetPwd.Text = Lbl_Email_Id.Text;
            Lbl_PAN_ResetPwd.Text = Lbl_PAN.Text;
            Hid_Unique_Id_ResetPwd.Value = Hid_Unique_Id.Value;
            Hid_Investor_Id_ResetPwd.Value = Hid_Investor_Id.Value;

            if (Hid_Regd_Source.Value == "NSWS")
            {
                ModalPopupExtender1.Hide();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>You cannot reset the password for this user because, It is registered through NSWS portal !</strong>', '" + strProjName + "'); </script>", false);
                return;
            }
            else
            {
                ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
    }

    /////// Reset Password
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        /*-----------------------------------------------------------------*/
        /////// Service Initialization
        DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
        DWH_Model objSrvEntity = new DWH_Model();
        try
        {
            ModalPopupExtender1.Show();
            /*-----------------------------------------------------------------*/
            /////// Server side validation
            /*-----------------------------------------------------------------*/
            if (Txt_New_Pwd.Text.Trim() == "")
            {
                Txt_New_Pwd.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please enter new password !</strong>', '" + strProjName + "'); </script>", false);
                return;
            }
            if (Txt_Confirm_Pwd.Text.Trim() == "")
            {
                Txt_Confirm_Pwd.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please confirm your password !</strong>', '" + strProjName + "'); </script>", false);
                return;
            }
            if (Txt_New_Pwd.Text.Trim() != Txt_Confirm_Pwd.Text.Trim())
            {
                Txt_New_Pwd.Text = "";
                Txt_Confirm_Pwd.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>New password and confirm password mismatch !</strong>', '" + strProjName + "'); </script>", false);
                return;
            }

            /*-----------------------------------------------------------------*/
            /////// Generate Encryption Key (Security key to access Data Warehouse servce methods)  
            /*-----------------------------------------------------------------*/
            string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
            string strAccessKey = objSrvRef.KeyEncryption(strEncryptionKey);

            /*-----------------------------------------------------------------*/
            /////// Password Reset Process
            /*-----------------------------------------------------------------*/
            string strPasswordEnc = CommonHelperCls.GenerateHash(Txt_New_Pwd.Text);
            objSrvEntity.VCHPASSWORD = strPasswordEnc;
            objSrvEntity.VCHUSERUNIQUEID = Hid_Unique_Id_ResetPwd.Value;

            /*-----------------------------------------------------------------*/
            /////// DML Operation
            string strReturnVal = objSrvRef.ResetPassword(objSrvEntity, strAccessKey);
            if (strReturnVal == "5")
            {
                SqlCommand objCommand = new SqlCommand();

                objCommand.CommandText = "USP_V2_PARENT_USER_SWAPPING";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "RP");
                objCommand.Parameters.AddWithValue("@P_VCH_INV_PASSWORD", strPasswordEnc);
                objCommand.Parameters.AddWithValue("@P_INT_INVESTOR_ID", Hid_Investor_Id_ResetPwd.Value);

                objCommand.ExecuteNonQuery();

                clearField();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Password Reset Successfully !</strong>', '" + strProjName + "'); </script>", false);
                ModalPopupExtender1.Hide();
            }
            else if (strReturnVal == "11")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Unique Id Mismatch !</strong>', '" + strProjName + "'); </script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Internal Server Error,Please Try After Sometime !</strong>', '" + strProjName + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
        finally
        {
            objSrvRef.Close();
            objSrvEntity = null;
            conn.Close();
            conn.Dispose();
        }
    }

    /////// Verify OTP
    protected void LnkBtn_Verify_OTP_Click(object sender, EventArgs e)
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            //Label Lbl_Investor_Name = (Label)row.FindControl("Lbl_Investor_Name");
            Label Lbl_New_User_Id = (Label)row.FindControl("Lbl_New_User_Id");
            Label Lbl_Email_Id = (Label)row.FindControl("Lbl_Email_Id");
            Label Lbl_PAN = (Label)row.FindControl("Lbl_PAN");
            HiddenField Hid_Unique_Id = (HiddenField)row.FindControl("Hid_Unique_Id");
            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");
            LinkButton LnkBtn_Inv_Name = (LinkButton)row.FindControl("LnkBtn_Inv_Name");
            HiddenField Hid_Regd_Source = (HiddenField)row.FindControl("Hid_Regd_Source");

            Lbl_Investor_Name_OTP.Text = LnkBtn_Inv_Name.Text;
            Lbl_User_Id_OTP.Text = Lbl_New_User_Id.Text;
            Lbl_Email_Id_OTP.Text = Lbl_Email_Id.Text;
            Lbl_PAN_OTP.Text = Lbl_PAN.Text;
            Hid_Investor_Id_OTP.Value = Hid_Investor_Id.Value;

            if (Hid_Regd_Source.Value == "NSWS")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>You cannot verify the OTP for this user because, It is registered through NSWS portal !</strong>', '" + strProjName + "'); </script>", false);
                return;
            }
            else
            {
                /*---------------------------------------------------------------*/
                ////// Get OTP 
                /*---------------------------------------------------------------*/
                SqlCommand objCommand = new SqlCommand();
                SqlDataAdapter objDa = new SqlDataAdapter();
                DataTable objds = new DataTable();

                objCommand.CommandText = "USP_V2_PARENT_USER_SWAPPING";
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Connection = conn;

                objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "VO"); ///// View OTP
                objCommand.Parameters.AddWithValue("@P_VCH_INV_USERID", Lbl_New_User_Id.Text);

                objDa.SelectCommand = objCommand;
                objDa.Fill(objds);

                if (objds.Rows.Count > 0)
                {
                    Lbl_OTP.Text = Convert.ToString(objds.Rows[0]["VCH_MOB_OTP"]);
                    Lbl_OTP_Time.Text = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", Convert.ToDateTime(objds.Rows[0]["DTM_CREATED_ON"]));
                    Lbl_OTP_Status.Text = Convert.ToString(objds.Rows[0]["VCH_OTP_STATUS"]);
                    Lbl_Application_Status.Text = Convert.ToString(objds.Rows[0]["VCH_APPROVAL_STATUS"]);
                    Hid_OTP_Status.Value = Convert.ToString(objds.Rows[0]["INT_OTP_STATUS"]);
                    Hid_Application_Status.Value = Convert.ToString(objds.Rows[0]["INT_APPROVAL_STATUS"]);

                    /*---------------------------------------------------------------*/

                    if (Hid_OTP_Status.Value == "1")
                    {
                        Lbl_OTP_Status.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        Lbl_OTP_Status.ForeColor = System.Drawing.Color.Red;
                    }

                    /*---------------------------------------------------------------*/

                    if (Hid_Application_Status.Value == "1")
                    {
                        Lbl_Application_Status.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        Lbl_Application_Status.ForeColor = System.Drawing.Color.Red;
                    }

                    /*---------------------------------------------------------------*/

                    ModalPopupExtender2.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>OTP not found for verification !</strong>', '" + strProjName + "'); </script>", false);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
    }

    /////// Update OTP
    protected void Btn_OTP_Update_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();

        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        try
        {
            if (Hid_OTP_Status.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>OTP has already verified for this user !</strong>', '" + strProjName + "'); </script>", false);
                return;
            }

            if (Hid_Application_Status.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Application has already been approved,No need to update OTP again !</strong>', '" + strProjName + "'); </script>", false);
                return;
            }

            /*-----------------------------------------------------------------*/
            ///// DML Operation for OTP Update
            /*-----------------------------------------------------------------*/
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandText = "USP_V2_PARENT_USER_SWAPPING";
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.Connection = conn;

            objCommand.Parameters.AddWithValue("@P_VCH_ACTION", "UO"); ///// Update OTP
            objCommand.Parameters.AddWithValue("@P_INT_INVESTOR_ID", Hid_Investor_Id_OTP.Value);

            int x = objCommand.ExecuteNonQuery();
            if (x > 0)
            {
                clearField();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>OTP Updated Successfully !</strong>', '" + strProjName + "'); </script>", false);
                ModalPopupExtender2.Hide();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong,Please try after sometime !</strong>', '" + strProjName + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
    }

    /////// View Investor Details
    protected void LnkBtn_Inv_Name_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");
            HiddenField Hid_Investor_UserId = (HiddenField)row.FindControl("Hid_Investor_UserId");

            Response.Redirect("View_Investor_Regd_Details.aspx?val=" + Hid_Investor_Id.Value + "~1"+"~" + Hid_Investor_UserId.Value, false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
    }
    protected void LnkBtn_Inv_Name_Reject_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");
            HiddenField Hid_Investor_UserId = (HiddenField)row.FindControl("Hid_Investor_UserId");

            Response.Redirect("View_Investor_Regd_Details.aspx?val=" + Hid_Investor_Id.Value + "~3"+"~"+ Hid_Investor_UserId.Value, false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
    }

    /////// View Rejected Investor Details
    protected void Btn_Rejected_User_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txt_Email_Id.Text.Trim() == "" && Txt_PAN.Text.Trim() == "" && Txt_Unit_Name.Text.Trim() == "" && Txt_MobileNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please enter either Email Id,PAN or Unit Name !</strong>', '" + strProjName + "'); </script>", false);
                return;
            }

            fillGridRejected();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
    }

    protected void LnkBtn_View_Transaction_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");
            Response.Redirect("View_Investor_Transaction_Details.aspx?val=" + Hid_Investor_Id.Value, false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
    }

    #endregion
}