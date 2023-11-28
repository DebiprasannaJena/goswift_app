#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;

using BusinessLogicLayer.Login;
using EntityLayer.Login;
using DWHServiceReference;
#endregion

public partial class ChangePasswordNonInvestor : System.Web.UI.Page
{
    #region Variables
   
    LoginBusinessLayer objService = new LoginBusinessLayer();
    LoginDetails objLogin = new LoginDetails();
    List<LoginDetails> objloginlist = new List<LoginDetails>();
    string pwdString, strOutMsg = "";
    string strprojname = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    #endregion

    #region Page_load

    protected void Page_Load(object sender, EventArgs e)
    {
        Util.SessionCheck();
    }

    #endregion

    #region Button_Click

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            /*-----------------------------------------------------------------------------------*/
            /// Generate the hash code for the supplied old password and compare it to the old password that already exists.         
            /// If the supplied old password matches the current old password, the password will be changed.
            /*-----------------------------------------------------------------------------------*/

            string strInvestorId = Session["InvestorId"].ToString();

            string strOldPwd = CommonHelperCls.GenerateHash(Txt_Old_Pwd.Text);
            objLogin.strAction = "V";
            objLogin.strUserID = strInvestorId;

            objloginlist = objService.ViewChngPwd(objLogin).ToList();
            if (objloginlist.Count > 0)
            {
                
                foreach (LoginDetails objlist in objloginlist)
                {
                    pwdString = objlist.strOldPassword;
                   
                }

                if (strOldPwd == pwdString.ToUpper().Trim())
                {
                    string strNewPwd = CommonHelperCls.GenerateHash(Txt_New_Pwd.Text);
                    string strIndustryType = Convert.ToString(Session["IndustryType"]);


                    if (strIndustryType == "2") /////Non-Industrial User
                    {
                        #region Non-Industrial User

                        /// For Non-Industrial user change the password only at GOSWIFT end.
                        objLogin.strAction = "U";
                        objLogin.strOldPassword = strOldPwd;
                        objLogin.strNewPassword = strNewPwd;
                        objLogin.strUserID = strInvestorId;

                        strOutMsg = objService.ManageChngPwd(objLogin);

                        if (strOutMsg == "2")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Password changed successfully !</strong>', '" + Messages.TitleOfProject + "', function () {location.href = 'Login.aspx';}); </script>", false);
                            return;
                        }

                        #endregion
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Old password is not the correct one!</strong>', '" + strprojname + "'); </script>", false);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ChangePwd");
        }
    }
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        Txt_Old_Pwd.Text = "";
        Txt_New_Pwd.Text = "";
        Txt_Retype_Pwd.Text = "";
    }

    #endregion
}