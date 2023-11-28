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

public partial class Portal_admin_UpdateProfile : System.Web.UI.Page
{
    #region Variables

    LoginBusinessLayer objService = new LoginBusinessLayer();
    LoginDetails objLogin = new LoginDetails();
    List<LoginDetails> objloginlist = new List<LoginDetails>();


    #endregion

    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        Util.SessionCheck();

        if (!IsPostBack)
        {
            FillData();
        }
    }

    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        try
        {
            /*---------------------------------------------------------------------*/
            ///// Server side validation
            /*---------------------------------------------------------------------*/
            #region Validation

            string strEmailPattern = @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"; ///// Email Format
            string strNumericPattern = @"^[0-9]+$"; ///// Only Numbers 

            if (txtEmail.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Email id can not be blank !</strong>', '" + strProjName + "'); </script>", false);
                txtEmail.Focus();
                return;
            }
            if (Regex.IsMatch(txtEmail.Text.Trim(), strEmailPattern) == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid email id !</strong>', '" + strProjName + "'); </script>", false);
                txtEmail.Focus();
                return;
            }

            if (txtMobile.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Mobile number can not be blank !</strong>', '" + strProjName + "'); </script>", false);
                txtMobile.Focus();
                return;
            }
            if (Regex.IsMatch(txtMobile.Text.Trim(), strNumericPattern) == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Only numeric value allowed in the mobile number !</strong>', '" + strProjName + "'); </script>", false);
                txtMobile.Focus();
                return;
            }
            if (txtMobile.Text.Trim().Substring(0, 1) == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Mobile number should not be start with zero !</strong>', '" + strProjName + "'); </script>", false);
                txtMobile.Focus();
                return;
            }
            if (txtMobile.Text.Trim().Length != 10)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Mobile number must be 10 digits !</strong>', '" + strProjName + "'); </script>", false);
                txtMobile.Focus();
                return;
            }

            #endregion

            /*---------------------------------------------------------------------*/
            //// Update user email id and mobile number
            /*---------------------------------------------------------------------*/
            objLogin.strAction = "V";
            objLogin.strUserName = Session["userName"].ToString();
            objloginlist = objService.ViewEmailAndMobile(objLogin).ToList();

            if (objloginlist.Count > 0)
            {
                objLogin.strAction = "UE";
                objLogin.strEmail = txtEmail.Text.ToString();
                objLogin.strMobile = txtMobile.Text.ToString();
                objLogin.strUserName = Session["userName"].ToString();
                string strOutMsg = objService.ManageMobileAndEmail(objLogin);
                if (strOutMsg == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Profile Update Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = 'UpdateProfile.aspx';}); </script>", false);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateProfile");
        }
    }
    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        txtEmail.Text = "";
        txtMobile.Text = "";
    }

    public void FillData()
    {
        try
        {
            objLogin.strAction = "V";
            objLogin.strUserName = Session["userName"].ToString();
            objloginlist = objService.ViewEmailAndMobile(objLogin).ToList();

            if (objloginlist.Count > 0)
            {
                foreach (LoginDetails objlist in objloginlist)
                {
                    txtEmail.Text = objlist.strEmail;
                    txtMobile.Text = objlist.strMobile;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateProfile");
        }
    }
}