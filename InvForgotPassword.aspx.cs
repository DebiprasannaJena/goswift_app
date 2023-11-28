#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   InvForgotPassword.aspx.cs
// Description           :   Investor Forgot Password
// Created by            :   Sanghamitra Samal
// Created On            :   03 Sep 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//     
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion
#region Namespace
using System.Net.Mail;
using System.Text;
using BusinessLogicLayer.Login;
using EntityLayer.Login;
using System.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using BusinessLogicLayer.Investor;
using BusinessLogicLayer.Service;
using EntityLayer.Investor;
using System.Data;
using System.Security.Cryptography;
#endregion
public partial class InvForgotPassword : System.Web.UI.Page
{
    #region Variables
    LoginBusinessLayer objService = new LoginBusinessLayer();
    LoginDetails objLogin = new LoginDetails();
    List<LoginDetails> objloginlist = new List<LoginDetails>();
    CommonHelperCls objComm = new CommonHelperCls();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        

    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        
        try
        {
            objLogin.strAction = "F";
            objLogin.strUserID = Txt_User_Id.Text.Trim();
            objloginlist = objService.getUserDetails(objLogin).ToList();
           DataTable  dt = CommonHelperCls.ConvertToDataTable(objloginlist);
            if (dt.Rows.Count > 0)
            {
                string strMobileNo = dt.Rows[0]["strMobile"].ToString();               
                string strEmailId = dt.Rows[0]["strEmail"].ToString();
                string strInvName = dt.Rows[0]["strInvName"].ToString();

                updatePassword( strEmailId, strMobileNo, strInvName);
                ClearData();
            }
            else
            {
                ClearData();
                Txt_User_Id.Focus();
                ScriptManager.RegisterStartupScript(Btn_Submit, this.GetType(), "OnClick", "<script>jAlert('User Id does not match !');</script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ForgotPassword");
        }
        
    }

    #region CommonFunction

    private void updatePassword( string strToMailId, string MobileNo, string strInvName)
    {
        string strSubject = "";
        string strSMSContent = "";
        try
        {
            ///// Generate a Token Number and Update in Login table
            string strtoken = GenerateToken();
            string strdatetime = Convert.ToString(DateTime.Now.AddMinutes(10));
            objService.ManageTokenNumber("MT", Txt_User_Id.Text.ToString().Trim(), strtoken, strdatetime);

            InvestorRegistration objInvService = new InvestorRegistration();
            InvestorDetails objInvDet = new InvestorDetails();

            ///// Get Investor Name
          

            ///// Get SMS and Email Content Format
            objInvDet.strAction = "FM";
            DataTable dtcontent = objInvService.GetSMSContent(objInvDet);
            if (dtcontent.Rows.Count > 0)
            {
                strSubject = dtcontent.Rows[0]["vchEvent"].ToString();
                strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[Token]", strtoken.ToString());
                strSMSContent = strSMSContent.Replace("[UNITNAME]", strInvName.ToString());
            }

            ///// Send SMS
            
            bool smsStatus = false;

            //// Send Email
            string[] toEmail = new string[1];
            toEmail[0] = strToMailId;
            bool mailStatus = objComm.sendMail(strSubject, strSMSContent, toEmail, true);

            ///// Update SMS and Mail Log
             objComm.UpdateMailSMSStaus("ForgotPassword", MobileNo, toEmail[0], strSubject, "0", "0", 0, "0", strSMSContent, strSMSContent, smsStatus, mailStatus);

            ScriptManager.RegisterStartupScript(Btn_Submit, this.GetType(), "OnClick", "<script>jAlert('<strong>To reset your new password,a link has been sent to your email id.Please check your email !</strong>');</script>", false);
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    private void ClearData()
    {
        Txt_User_Id.Text = "";
    }

    #endregion

    public string MakeRandom(int pl)
    {
        string possibles = "0123456789";
        char[] passwords = new char[pl];
        Random rd = new Random();

        for (int i = 0; i < pl; i++)
        {
            passwords[i] = possibles[rd.Next(0, possibles.Length)];
        }
        return new string(passwords);
    }
    public string GenerateToken()
    {
        MD5 md5 = System.Security.Cryptography.MD5.Create();
       
        string strrand = MakeRandom(16);

        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(strrand);
        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X4"));
        }
        return sb.ToString();
    }
}