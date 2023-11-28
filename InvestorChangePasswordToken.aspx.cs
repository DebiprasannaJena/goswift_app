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
using System.Text.RegularExpressions;
using BusinessLogicLayer.Login;
using EntityLayer.Login;
using DWHServiceReference;

public partial class InvestorChangePasswordToken : System.Web.UI.Page
{
    LoginBusinessLayer objService = new LoginBusinessLayer();
    LoginDetails objLogin = new LoginDetails();
    List<LoginDetails> objloginlist = new List<LoginDetails>();

    string stroldpwd = null;
    string uniqueid = null;
    string strmobile = null;
    string strUserID = null;
    int intinvestorid = 0;
    int intIndustryType = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                divSuc.Visible = false;
                divchange.Visible = true;
                divexpire.Visible = false;
                DataTable dt = objService.GetTokenDetails("GT", Request.QueryString["token"].ToString());
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["int_tokenstatus"].ToString() == "1")
                    {
                        divchange.Visible = false;
                        divSuc.Visible = false;
                        divexpire.Visible = true;
                        return;
                    }
                    else
                    {
                        DateTime dtmtokentime = Convert.ToDateTime(dt.Rows[0]["VCH_TOKENTIME"].ToString());
                        DateTime dtmtime = Convert.ToDateTime(DateTime.Now);
                        if (dtmtime > dtmtokentime)
                        {
                            divchange.Visible = false;
                            divSuc.Visible = false;
                            divexpire.Visible = true;
                            return;
                        }
                    }
                }
                else
                {
                    divchange.Visible = false;
                    divSuc.Visible = false;
                    divexpire.Visible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "ForgotPassword");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region Validation

        /*-----------------------------------------------------------------*/
        ///// Captcha Validation
        /*-----------------------------------------------------------------*/

        if ((txtCaptcha.Text).Any(char.IsLower) == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid Captcha !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            txtCaptcha.Text = "";
            txtCaptcha.Focus();
            return;
        }

        Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
        Boolean bt = Captcha1.UserValidated;
        if (bt == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid Captcha !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            txtCaptcha.Text = "";
            txtCaptcha.Focus();
            return;
        }

        /*-----------------------------------------------------------------*/
        ///// Validate Password
        /*-----------------------------------------------------------------*/

        if (txtNewPsw.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>New password can not be blank !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            txtNewPsw.Focus();
            return;
        }
        if (txtRetypPsw.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Confirm password can not be blank !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            txtRetypPsw.Focus();
            return;
        }
        string strNewPwd = txtNewPsw.Text;
        Regex reg = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
        if (reg.IsMatch(strNewPwd) == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Password must contain atleast one uppercase letter,one lowercase letter,one number and one special character and length must be between 8-14 characters !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        if (txtNewPsw.Text.ToString().Trim() != txtRetypPsw.Text.ToString().Trim())
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>New password and Confirm password does not match !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }

        #endregion

        try
        {
            /*-----------------------------------------------------------------*/
            ///// Get recodrs against token number
            /*-----------------------------------------------------------------*/
            objLogin.strAction = "MNT";
            objLogin.strTokenno = Request.QueryString["token"].ToString();
            objloginlist = objService.GetInvDetails(objLogin).ToList();
            if (objloginlist.Count > 0)
            {
                foreach (LoginDetails objlist in objloginlist)
                {
                    stroldpwd = objlist.strOldPassword;
                    uniqueid = objlist.strUID;
                    strmobile = objlist.strMobile;
                    strUserID = objlist.strUserID;
                    intinvestorid = objlist.intInvestorId;
                    intIndustryType = objlist.IntIndustryType;
                }

                string newPsw = CommonHelperCls.GenerateHash(txtRetypPsw.Text);

                /*-------------------------------------------------------------------------------------------------------------*/
                /// If the user is an INDUSTRIAL user, then change the password on both the GOSWIFT and DWH end.
                /// If the user is an NON-INDUSTRIAL user, then change the password only at GOSWIFT end.
                /*-------------------------------------------------------------------------------------------------------------*/
                if (intIndustryType == 1) ///// Industrial User
                {
                    #region Industrial User

                    /*-----------------------------------------------------------------*/
                    /////// Service Initialization
                    /*-----------------------------------------------------------------*/
                    DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                    DWH_Model objSrvEntity = new DWH_Model();

                    /*-----------------------------------------------------------------*/
                    /// Generate Encryption Key (Security key to access Data Warehouse servce methods)  
                    /*-----------------------------------------------------------------*/
                    string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                    string strAccessKey = objSrvRef.KeyEncryption(strEncryptionKey);

                    /*-----------------------------------------------------------------*/
                    /// Password Reset Process
                    /*-----------------------------------------------------------------*/
                    objSrvEntity.VCHPASSWORD = newPsw;
                    objSrvEntity.VCHUSERUNIQUEID = uniqueid;

                    /*-----------------------------------------------------------------*/
                    /// DML Operation
                    /*-----------------------------------------------------------------*/
                    string strReturnVal = objSrvRef.ResetPassword(objSrvEntity, strAccessKey);
                    if (strReturnVal == "5")
                    {
                        /*-----------------------------------------------------------------*/
                        /// Update password in GOSWIFT Database
                        /*-----------------------------------------------------------------*/
                        objLogin.strAction = "UT";
                        objLogin.strOldPassword = stroldpwd;
                        objLogin.strNewPassword = newPsw;
                        objLogin.strUserID = intinvestorid.ToString();

                        string strOutMsg = objService.ManageChngPwd(objLogin);
                        if (strOutMsg == "2")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>Your password has been reset successfully.</strong>');</script>", false);
                            divSuc.Visible = true;
                            divchange.Visible = false;
                            divexpire.Visible = false;
                            return;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong.Try after some times !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                            return;
                        }
                    }
                    else if (strReturnVal == "11")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Unique Id Mismatch !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Internal Server Error,Please Try After Sometime !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                    }

                    #endregion
                }
                else if (intIndustryType == 2)///// Non-Industrial User
                {
                    #region Non-Industrial User

                    /*-----------------------------------------------------------------*/
                    /// Update password in GOSWIFT Database.
                    /*-----------------------------------------------------------------*/
                    objLogin.strAction = "UT";
                    objLogin.strOldPassword = stroldpwd;
                    objLogin.strNewPassword = newPsw;
                    objLogin.strUserID = intinvestorid.ToString();

                    string strOutMsg = objService.ManageChngPwd(objLogin);
                    if (strOutMsg == "2")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>Your password has been reset successfully.</strong>');</script>", false);
                        divSuc.Visible = true;
                        divchange.Visible = false;
                        divexpire.Visible = false;
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong.Try after some times !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                        return;
                    }

                    #endregion
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong.Try after some times !</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
                return;
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ForgotPassword");
        }
        finally
        {
            objService = null;
        }
    }
}