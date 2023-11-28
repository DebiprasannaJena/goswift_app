using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Login;
using BusinessLogicLayer.Login;

public partial class Portal_admin_ManageUser : System.Web.UI.Page
{
    public enum enAction
    {
        unlock = 1,
        userlock = 2,
        pwdReset = 3
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillUserDropDown();
        }
    }

    protected void rdBtnAction_SelectedIndexChanged(object sender, EventArgs e)
    {
        divUnlockUser.Visible = false;
        divPassword.Visible = false;
        divConfirmPassword.Visible = false;
        btnCancel.Visible = false;
        btnLockUser.Visible = false;
        btnUpdatePassword.Visible = false;

        int intAction = Convert.ToInt32(rdBtnAction.SelectedValue);
        if (intAction == (int)enAction.unlock)
        {
            ManageUserDetails objUserDetails = new ManageUserDetails()
            {
                strAction = "un",
                intUserId = Convert.ToInt32(ddlUser.SelectedValue)
            };
            int intRetValue = UserDetailsServices.ManageUser_AED(objUserDetails);
            if (intRetValue == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Account of User - " + ddlUser.SelectedItem.Text + " has been unlocked successfully', '" + Messages.TitleOfProject + "');   </script>", false);
            }
            else if (intRetValue == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Account of User - " + ddlUser.SelectedItem.Text + " has not been locked. Please check.', '" + Messages.TitleOfProject + "');   </script>", false);
            }
        }
        else if (intAction == (int)enAction.userlock)
        {
            divUnlockUser.Visible = true;
            btnLockUser.Visible = true;
            btnCancel.Visible = true;
        }
        else if (intAction == (int)enAction.pwdReset)
        {
            divConfirmPassword.Visible = true;
            divPassword.Visible = true;
            btnUpdatePassword.Visible = true;
            btnCancel.Visible = true;
        }
    }

    protected void btnLockUser_Click(object sender, EventArgs e)
    {
        ManageUserDetails objUserDetails = new ManageUserDetails()
        {
            strAction = "lock",
            intUserId = Convert.ToInt32(ddlUser.SelectedValue),
            intNoOfLockHours = Convert.ToInt32(txtNoOfHours.Text.Trim())
        };

        int intRetValue = UserDetailsServices.ManageUser_AED(objUserDetails);
        if (intRetValue == 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Account of User - " + ddlUser.SelectedItem.Text + " has been locked successfully', '" + Messages.TitleOfProject + "');   </script>", false);
        }
    }

    protected void btnUpdatePassword_Click(object sender, EventArgs e)
    {
        LoginDetails objLogin = new LoginDetails();
        objLogin.strAction = "UP";
        string  newPsw = CommonHelperCls.GenerateHash(txtPassword.Text);
        objLogin.strNewPassword = newPsw;
        objLogin.strUserID = ddlUser.SelectedValue;
        LoginBusinessLayer objService = new LoginBusinessLayer();
        string strOutMsg = objService.ManageDeptChngPwd(objLogin);
        if (strOutMsg == "2")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Password Changed Successfully !', '" + Messages.TitleOfProject + "'); </script>", false);
        }
    }


    private void FillUserDropDown()
    {
        ManageUserDetails objUser = new ManageUserDetails()
        {
            strAction = "user"
        };
        Dictionary<int, string> dcUser = new Dictionary<int, string>();
        dcUser = UserDetailsServices.GetUserList(objUser);
        ddlUser.DataSource = dcUser;
        ddlUser.DataTextField = "Value";
        ddlUser.DataValueField = "key";
        ddlUser.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlUser.Items.Insert(0, list);
    }


}