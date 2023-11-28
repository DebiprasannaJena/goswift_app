#region  PageInfo
//******************************************************************************************************************
// File Name             :   ChangePasswordInvestor.aspx.cs
// Description           :   ChangePassword
// Created by            :   AMit Sahoo
// Created On            :   21 July 2017
// Modification History  :
//                          <CR no.>               <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   1                  21-Sep-2018                Sushant Jena                   Change password service integration with Data Warehouse
//******************************************************************************************************************
#endregion

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

public partial class ChangePasswordInvestor : System.Web.UI.Page
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
            /*---------------------------------------------------------------------------------------------------------------*/
            /// Both Industrial and Non-Industrial user can change their own password.
            /// For Non-Industrial user (means the value Session["IndustryType"] is 2), the password will be changed only at GOSWIFT end.
            /// For Industrial user (means the value Session["IndustryType"] is 1), the password will be changed both at GOSWIFT and DWH ends. 
            /*---------------------------------------------------------------------------------------------------------------*/

            string strUserId = "";
            string strInvestorId = "";

            if (Convert.ToString(Session["UserLevel"]) == "2")//// Person, Not an Unit 
            {
                /*---------------------------------------------------------------------------------------------------------------*/
                /// The value of Session["UserLevel"]=2 indicates that this user has been assigned an user credential and has access to its parent unit.   
                /// Here the Session["UserId"] carries the invetsor id of its parent unit, and Session["OwnUniqueId"] contains the unique id of its own user id.
                /// So before changing it's password, first get the investor id using the unique id and then change password.
                /*---------------------------------------------------------------------------------------------------------------*/
                LoginDetails objLogindt = new LoginDetails();
                LoginBusinessLayer objBAL = new LoginBusinessLayer();

                objLogindt.strAction = "V2";
                objLogindt.strUniqueId = Convert.ToString(Session["OwnUniqueId"]);

               
                DataTable dt = objBAL.viewInvestorDetails(objLogindt);
                if (dt.Rows.Count > 0)
                {
                    strInvestorId = dt.Rows[0]["INT_INVESTOR_ID"].ToString();
                    strUserId = dt.Rows[0]["VCH_INV_USERID"].ToString();
                }
            }
            else
            {
                strInvestorId = Session["InvestorId"].ToString();
                strUserId = Session["UserId"].ToString();
            }

            /*-----------------------------------------------------------------------------------*/
            /// Generate the hash code for the supplied old password and compare it to the old password that already exists.         
            /// If the supplied old password matches the current old password, the password will be changed.
            /*-----------------------------------------------------------------------------------*/

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

                    if (strIndustryType == "1") ////Industrial User
                    {
                        #region Industrial User

                        /*-----------------------------------------------------------------*/
                        /////// Service Initialization (Push data to Data Warehouse)
                        /*-----------------------------------------------------------------*/
                        DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                        DWH_Model objSrvEntity = new DWH_Model();

                        objSrvEntity.VCHUSERNAME = strUserId;
                        objSrvEntity.VCHOLDPASSWORD = strOldPwd;
                        objSrvEntity.VCHPASSWORD = strNewPwd;

                        /*-----------------------------------------------------------------*/
                        /////// Generate Encryption Key (Security key to access Data Warehouse service methods)
                        /*-----------------------------------------------------------------*/
                        string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                        string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

                        string strReturnVal = objSrvRef.ChangePassword(objSrvEntity, strSecurityKey);
                        if (strReturnVal == "11")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid user id !</strong>', '" + strprojname + "'); </script>", false);
                            return;
                        }
                        else if (strReturnVal == "16")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Old password is not the correct one !</strong>', '" + strprojname + "'); </script>", false);
                            return;
                        }
                        else if (strReturnVal == "5")
                        {
                            objLogin.strAction = "U";
                            objLogin.strOldPassword = strOldPwd;
                            objLogin.strNewPassword = strNewPwd;
                            objLogin.strUserID = strInvestorId; 

                            strOutMsg = objService.ManageChngPwd(objLogin);
                            if (strOutMsg == "2")
                            {
                                if (Request.QueryString["app"] != null)
                                {
                                    if (Convert.ToString(Request.QueryString["app"]) == "CICG")
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Password changed successfully !</strong>', '" + Messages.TitleOfProject + "', function () {location.href = '" + ConfigurationManager.AppSettings["CICGReturnUrl"] + "';}); </script>", false);
                                        return;
                                    }
                                    else if (Convert.ToString(Request.QueryString["app"]) == "GOiPAS")
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Password changed successfully !</strong>', '" + Messages.TitleOfProject + "', function () {location.href = '" + ConfigurationManager.AppSettings["GOIPASReturnUrl"] + "';}); </script>", false);
                                        return;
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Password changed successfully !</strong>', '" + Messages.TitleOfProject + "', function () {location.href = 'Login.aspx';}); </script>", false);
                                        return;
                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Password changed successfully !</strong>', '" + Messages.TitleOfProject + "', function () {location.href = 'Login.aspx';}); </script>", false);
                                    return;
                                }
                            }
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