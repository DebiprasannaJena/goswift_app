using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Data;
using EntityLayer.Common;
using BusinessLogicLayer.Common;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;

public partial class aboutus : System.Web.UI.Page
{
    CommonBusinessLayer objService = new CommonBusinessLayer();
    Feedback objServiceEntity = new Feedback();

    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    string str_Retvalue = "";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtCaptcha.Text).Any(char.IsLower))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid Captcha !</strong>', '" + strProjName + "'); </script>", false);
                txtCaptcha.Text = "";
                txtCaptcha.Focus();
                return;
            }

            Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
            Boolean bt = Captcha1.UserValidated;
            if (!bt)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid Captcha !</strong>', '" + strProjName + "'); </script>", false);
                txtCaptcha.Text = "";
                txtCaptcha.Focus();
                return;
            }
            else
            {
                /*---------------------------------------------------------------------*/
                ///// Server side validation
                /*---------------------------------------------------------------------*/
                #region Validation

                string strAlphaSpacePattern = @"^[a-zA-Z ]+$"; ///// Alphabets and Space
                string strEmailPattern = @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$"; ///// Email Format
                string strNumericPattern = @"^[0-9]+$"; ///// Only Numbers
                string strMsgBoxPattern = @"^[A-Za-z0-9-. ]+$"; ///// Alphabets with space,dot(). and hypen(-)

                if (txtFirstName.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>First name can not be blank !</strong>','" + strProjName + "');</script>", false);
                    txtFirstName.Focus();
                    return;
                }
                if (!Regex.IsMatch(txtFirstName.Text.Trim(), strAlphaSpacePattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>First name should be only alphabets !</strong>','" + strProjName + "');</script>", false);
                    txtFirstName.Focus();
                    return;
                }

                if (txtLastName.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Last name can not be blank !</strong>','" + strProjName + "');</script>", false);
                    txtLastName.Focus();
                    return;
                }
                if (! Regex.IsMatch(txtLastName.Text.Trim(), strAlphaSpacePattern) )
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Last name should be only alphabets !</strong>','" + strProjName + "');</script>", false);
                    txtLastName.Focus();
                    return;
                }

                if (txtEmail.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Email id can not be blank !</strong>','" + strProjName + "');</script>", false);
                    txtEmail.Focus();
                    return;
                }
                if (! Regex.IsMatch(txtEmail.Text.Trim(), strEmailPattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Invalid email id !</strong>','" + strProjName + "');</script>", false);
                    txtEmail.Focus();
                    return;
                }

                if (txtMobileNumber.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Mobile number can not be blank !</strong>','" + strProjName + "');</script>", false);
                    txtMobileNumber.Focus();
                    return;
                }
                if (txtMobileNumber.Text.Trim().Length != 10)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Mobile number must be 10 digits !</strong>','" + strProjName + "');</script>", false);
                    txtMobileNumber.Focus();
                    return;
                }
                if (! Regex.IsMatch(txtMobileNumber.Text.Trim(), strNumericPattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Only numeric value allowed in the phone number !</strong>','" + strProjName + "');</script>", false);
                    txtMobileNumber.Focus();
                    return;
                }

                if (txtSubject.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Subject can not be blank !</strong>','" + strProjName + "');</script>", false);
                    txtSubject.Focus();
                    return;
                }
                if (! Regex.IsMatch(txtSubject.Text.Trim(), strMsgBoxPattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Invalid characters in the subject !</strong>','" + strProjName + "');</script>", false);
                    txtSubject.Focus();
                    return;
                }

                if (txtFeedback.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Feedback can not be blank !</strong>','" + strProjName + "');</script>", false);
                    txtFeedback.Focus();
                    return;
                }
                if (txtFeedback.Text.Trim().Length > 250)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Your feedback should be a maximum of 250 characters !</strong>','" + strProjName + "');</script>", false);
                    txtFeedback.Focus();
                    return;
                }
                if ( !Regex.IsMatch(txtFeedback.Text.Trim(), strMsgBoxPattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>jAlert('<strong>Invalid characters in the feedback box !</strong>','" + strProjName + "');</script>", false);
                    txtFeedback.Focus();
                    return;
                }

                #endregion

                /*---------------------------------------------------------------------*/

                objServiceEntity.strAction = "A";
                objServiceEntity.vchFirstName = txtFirstName.Text.Trim();
                objServiceEntity.vchLastName = txtLastName.Text.Trim();
                objServiceEntity.vchEmail = txtEmail.Text.Trim();
                objServiceEntity.vchMobileNo = txtMobileNumber.Text.Trim();
                objServiceEntity.vchSubject = txtSubject.Text.Trim();
                objServiceEntity.vchFeedback = txtFeedback.Text.Trim();
                objServiceEntity.intCreatedBy = 1;

                str_Retvalue = objService.ManageFeedback(objServiceEntity);
                if (str_Retvalue == "1")
                {
                    ClearFields();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Feedback Submitted Successfully !</strong>');", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Feedback");
        }
    }

    public void ClearFields()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtEmail.Text = "";
        txtMobileNumber.Text = "";
        txtSubject.Text = "";
        txtFeedback.Text = "";
        txtCaptcha.Text = "";
    }
}