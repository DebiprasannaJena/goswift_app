using BusinessLogicLayer.Login;
using EntityLayer.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_Application : System.Web.UI.MasterPage
{
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/portal/SessionRedirect.aspx");
            return;
        }
        System.Web.HttpBrowserCapabilities browser = Request.Browser;
        string s = "Browser Capabilities\n"
            + "Type = " + browser.Type + "\n"
            + "Name = " + browser.Browser + "\n"
            + "Version = " + browser.Version + "\n"
            + "Major Version = " + browser.MajorVersion + "\n"
            + "Minor Version = " + browser.MinorVersion + "\n"
            + "Platform = " + browser.Platform + "\n"
            + "Is Beta = " + browser.Beta + "\n"
            + "Is Crawler = " + browser.Crawler + "\n"
            + "Is AOL = " + browser.AOL + "\n"
            + "Is Win16 = " + browser.Win16 + "\n"
            + "Is Win32 = " + browser.Win32 + "\n"
            + "Supports Frames = " + browser.Frames + "\n"
            + "Supports Tables = " + browser.Tables + "\n"
            + "Supports Cookies = " + browser.Cookies + "\n"
            + "Supports VBScript = " + browser.VBScript + "\n"
            + "Supports JavaScript = " +
                browser.EcmaScriptVersion.ToString() + "\n"
            + "Supports Java Applets = " + browser.JavaApplets + "\n"
            + "Supports ActiveX Controls = " + browser.ActiveXControls
                  + "\n"
            + "Supports JavaScript Version = " +
                browser["JavaScriptVersion"] + "\n";

        try
        {
            if (Session["brs"].ToString() != s)
            {
                Response.Redirect("~/LogOut.aspx", false);
            }
        }
        catch (Exception ex)
        {
        }
        if (Session["fullname"] != null)
        {
            lblUserName.Text = Session["fullname"].ToString();
            if (Session["adminstat"].ToString() != "super" || Session["desId"].ToString() != "1" || Session["DeptId"].ToString() != "1")
            {
                adminconsolelink.Visible = false;
            }
            if (Session["UserId"].ToString() == "1232")
            {
                adminconsolelink.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Portal/SessionRedirect.aspx");
        }

        if (Session["displayname"] != null)
        {
            if (Session["displayname"].ToString() != "")
            {
                lblUserName.Text = Session["displayname"].ToString();

                if (Session["adminstat"].ToString() != "super")
                {
                    adminconsolelink.Visible = false;
                }
            }
            else
            {
                if (Session["desination"] != null)
                {
                    lblUserName.Text = Session["desination"].ToString();

                    if (Session["adminstat"].ToString() != "super")
                    {
                        adminconsolelink.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("../SessionRedirect.aspx");
                }
            }
        }

        if (!IsPostBack)
        {
            OpenModalpopup();
        }

    }
    private void OpenModalpopup()
    {
        if (Session["ChangePwd"].ToString() == "0")
        {
            txtOldPwd.Text = "";
            txtNewPwd.Text = "";
            txtRetypPwd.Text = "";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString()))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "USP_DEPARTMENT_PWD_UPDATE";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@PINT_USER_ID", Convert.ToInt32(Session["UserId"].ToString()));
                        cmd.Parameters.AddWithValue("@PVCH_ACTION", "PWDCOUNT");
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "ChangePwdDeptCheck");
                }
            }
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["OUTPUT"].ToString() == "0")
                {
                    mp1.Show();                   
                }
                else if (dt.Rows[0]["OUTPUT"].ToString() == "1")
                {
                    Session["ChangePwd"] = 1;                    
                }
                else
                {
                    Session["ChangePwd"] = 1;                    
                }
            }
            else
            {
                Session["ChangePwd"] = 1;               
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
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
                mp1.Show();
                return;
            }
            if (txtNewPwd.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>New password can not be blank !</strong>','" + strProjName + "'); </script>", false);
                txtNewPwd.Focus();
                mp1.Show();
                return;
            }
            if (txtNewPwd.Text.Trim().Length < 8)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Your password must be at least 8 characters long !</strong>','" + strProjName + "'); </script>", false);
                txtNewPwd.Focus();
                mp1.Show();
                return;
            }
            if (txtOldPwd.Text.Trim() == txtNewPwd.Text.Trim())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>New password cannot be same as old password !</strong>','" + strProjName + "'); </script>", false);
                txtNewPwd.Text = "";
                txtRetypPwd.Text = "";
                txtNewPwd.Focus();
                mp1.Show();
                return;
            }
            if (Regex.IsMatch(txtNewPwd.Text.Trim(), strPwdPattern) == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The password must contain at least one uppercase letter,one lowercase letter,one numeral and one special character. !</strong>','" + strProjName + "'); </script>", false);
                txtNewPwd.Focus();
                mp1.Show();
                return;
            }
            if (txtNewPwd.Text.Trim() != txtRetypPwd.Text.Trim())
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>New password and Confirm password does not match !</strong>','" + strProjName + "'); </script>", false);
                txtNewPwd.Text = "";
                txtRetypPwd.Text = "";
                txtNewPwd.Focus();
                mp1.Show();
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
                        Session["ChangePwd"] = 1;
                        mp1.Hide();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Password changed successfully !</strong>','" + strProjName + "' ); </script>", false);
                        return;
                    }
                    else if (strOutMsg == "1")
                    {
                        mp1.Show();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The password given by you match with your last 3 password !</strong>', '" + strProjName + "'); </script>", false);
                        return;
                    }
                }
                else
                {
                    mp1.Show();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The old password given by you is incorrect !</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChangePwdDeptPopup");
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LogOut.aspx", false);
    }
}
