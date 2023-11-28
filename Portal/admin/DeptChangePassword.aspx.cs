using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SWP_Service;
//using EntityLayer.Login;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;

using BusinessLogicLayer.Login;
using EntityLayer.Login;
using System.Text.RegularExpressions;

public partial class Portal_admin_DeptChangePassword : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        Util.SessionCheck();
    }

    #region ButtonClickEvents

    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        LoginBusinessLayer objService = new LoginBusinessLayer();
        LoginDetails objLogin = new LoginDetails();
        List<LoginDetails> objloginlist = new List<LoginDetails>();

        try
        {
            /*---------------------------------------------------------------------*/
            ///// Server side validation
            /*---------------------------------------------------------------------*/
            #region Validation

            string strPwdPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$"; ///// Password Format

            if (txtOldPwd.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Old password can not be blank !</strong>','" + strProjName + "'); </script>", false);
                txtOldPwd.Focus();
                return;
            }
            if (txtNewPwd.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>New password can not be blank !</strong>','" + strProjName + "'); </script>", false);
                txtNewPwd.Focus();
                return;
            }
            if (txtNewPwd.Text.Trim().Length < 8)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Your password must be at least 8 characters long !</strong>','" + strProjName + "'); </script>", false);
                txtNewPwd.Focus();
                return;
            }
            if (txtOldPwd.Text.Trim() == txtNewPwd.Text.Trim())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>New password cannot be same as old password !</strong>','" + strProjName + "'); </script>", false);
                txtNewPwd.Text = "";
                txtRetypPwd.Text = "";
                txtNewPwd.Focus();
                return;
            }
            if (Regex.IsMatch(txtNewPwd.Text.Trim(), strPwdPattern) == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The password must contain at least one uppercase letter,one lowercase letter,one numeral and one special character. !</strong>','" + strProjName + "'); </script>", false);
                txtNewPwd.Focus();
                return;
            }
            if (txtNewPwd.Text.Trim() != txtRetypPwd.Text.Trim())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>New password and Confirm password does not match !</strong>','" + strProjName + "'); </script>", false);
                txtNewPwd.Text = "";
                txtRetypPwd.Text = "";
                txtNewPwd.Focus();
                return;
            }

            #endregion

            /*---------------------------------------------------------------------*/
            ///// Generate MD5 Hash code for old password.
            /*---------------------------------------------------------------------*/
            string strOldPwd = CommonHelperCls.GenerateHash(txtOldPwd.Text.Trim());

            /*---------------------------------------------------------------------*/
            ///// Get existing password of user.
            /*---------------------------------------------------------------------*/
            objLogin.strAction = "VP";
            objLogin.strUserID = Session["UserId"].ToString();
            objloginlist = objService.ViewDeptChngPwd(objLogin).ToList();

            if (objloginlist.Count > 0)
            {
                string strMobile = "";
                string pwdString = "";

                foreach (LoginDetails objlist in objloginlist)
                {
                    pwdString = objlist.strOldPassword;
                    strMobile = objlist.strMobile;
                }

                /*---------------------------------------------------------------------*/
                ///// If old password match with new password then go for password change.
                /*---------------------------------------------------------------------*/
                if (strOldPwd == pwdString.ToUpper().Trim())
                {
                    ///// Generate MD5 Hash code for new password.
                    string newPwd = CommonHelperCls.GenerateHash(txtNewPwd.Text);

                    objLogin.strAction = "UP";
                    objLogin.strOldPassword = strOldPwd;
                    objLogin.strNewPassword = newPwd;
                    objLogin.strUserID = Session["UserId"].ToString();

                    string strOutMsg = objService.ManageDeptChngPwd(objLogin);
                    if (strOutMsg == "2")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Password Changed Successfully !</strong>','" + strProjName + "' ); </script>", false);
                        return;
                    }
                    else if (strOutMsg == "1")
                    {                      
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The password given by you match with your last 3 password !</strong>', '" + strProjName + "'); </script>", false);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The old password given by you is incorrect !</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChangePwdDept");
        }
    }
    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        txtOldPwd.Text = "";
        txtNewPwd.Text = "";
        txtRetypPwd.Text = "";
    }

    #endregion
}