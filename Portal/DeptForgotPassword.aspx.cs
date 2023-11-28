using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Data;
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

public partial class Portal_DeptForgotPassword : System.Web.UI.Page
{
    #region Variables
    LoginBusinessLayer objService = new LoginBusinessLayer();
    LoginDetails objLogin = new LoginDetails();
    List<LoginDetails> objloginlist = new List<LoginDetails>();
    string strUserId, strPassword, strRandomPassword, strsendpassword;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            objLogin.strAction = "F";
            //objLogin.strUserID = txtuserID.Text.Trim();
            objLogin.strEmail = txtEmailID.Text.Trim();
            objloginlist = objService.getDeptUserDetails(objLogin).ToList();
            dt = CommonHelperCls.ConvertToDataTable(objloginlist);
            if (dt.Rows.Count > 0)
            {
                //if (dt.Rows[0]["strUserID"].ToString() != txtuserID.Text.Trim())
                //{
                //    txtuserID.Text = "";   
                //    ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>jAlert('Invalid User Name');</script>", false);
                //    return;
                //}
                //else if (dt.Rows[0]["VCH_EMAIL"].ToString() != txtEmailID.Text.Trim())
                //{
                //    //strPassword = DynamicPwd();
                //    //strRandomPassword = CommonHelperCls.GenerateHash(strPassword);
                //    //updatePassword(txtuserID.Text, txtEmailID.Text, "amit.sahoo@csmpl.com", strRandomPassword, strPassword); 
                //    txtEmailID.Text = "";  
                //    ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>jAlert('Invalid Email Id');</script>", false);
                //    return;                     
                //}  
                string strMobileNo = dt.Rows[0]["strMobile"].ToString();
                strUserId = dt.Rows[0]["strUserID"].ToString();
                strPassword = DynamicPwd();
                strRandomPassword = CommonHelperCls.GenerateHash(strPassword);
                updatePassword(strUserId, txtEmailID.Text, strRandomPassword, strPassword, strMobileNo);
                ClearData();
            }
            else
            {
                ClearData();
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>jAlert('User Name and Email Id does not match !');</script>", false);
                return;
            }
        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string DynamicPwd()
    {
        Random random = new Random();
        StringBuilder strbuilder = new StringBuilder();
        char ch = '\0';
        int intCounter = 0;
        for (intCounter = 0; intCounter <= 5; intCounter++)
        {
            ch = Convert.ToChar(random.Next(65, 90));
            //Generate a random character between A To Z
            strbuilder.Append(ch);
        }
        object strRandomPwd = Convert.ToString(strbuilder);
        strsendpassword = strRandomPwd.ToString();
        //Store the radom  pwd
        return Convert.ToString(strRandomPwd);

    }

    protected void updatePassword(string userID, string toMail, string password, string orgPassword, string MobileNo)
    {
        string result = "";
        try
        {
            objLogin.strAction = "FP";
            objLogin.strUserID = strUserId;
            objLogin.strEmail = txtEmailID.Text.Trim();
            objLogin.strPassword = password;
            result = objService.UpdateDeptPassword(objLogin);
            string strSubject = "Single Window Portal - Password Request";
            string strBody = "SWP: Your password reset request has been acknowledged.Kindly log into investodisha,gov.in with the new password-" + orgPassword;
 
            //System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(attachdpath);
            string[] toEmail = new string[1];
            toEmail[0] = txtEmailID.Text.ToString().Trim();
            CommonHelperCls comm = new CommonHelperCls();
          bool mailStatus = comm.sendMail(strSubject, strBody, toEmail, true);
          bool smsStatus = comm.SendSmsNew(MobileNo, strBody);
            /*----------------------------------------------------------------*/
            ///Update SMS and Email Status in Transaction Table
            /*----------------------------------------------------------------*/
            string str = comm.UpdateMailSMSStaus("DeptForgotPassword", MobileNo, toEmail[0], "Password Request", "0", "0", 0, "0", strBody, strBody, smsStatus, mailStatus);
            //ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>jAlert('Password has been sent to your mail. Please check !');</script>", false);
            //Response.Redirect("inestorlogin.aspx");
            ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>jAlert('Password has been sent to your mail. Please check !');</script>", false);
            //Response.Redirect("inestorlogin.aspx");
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
   
    public void ClearData()
    {
        txtEmailID.Text = "";
    }
}