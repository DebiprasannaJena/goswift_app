using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.CMS;
using System.Data;
using BusinessLogicLayer.CMS;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;

public partial class contactus : System.Web.UI.Page
{
    CmsBusinesslayer objService = new CmsBusinesslayer();
    EntityLayer.CMS.CMSDetails objServiceEntity = new EntityLayer.CMS.CMSDetails();

    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            FillContent();
        }
    }
    private void FillContent()
    {
        try
        {
            int intmenuid = Convert.ToInt32(Request.QueryString["id"]);
            string straction = "CHK";
            DataTable dt = objService.ChkCMSData(straction, intmenuid);
            if (dt.Rows.Count > 0)
            {
                string sValueTobeShowninDiv = dt.Rows[0]["vchContent"].ToString();
                divabout.InnerHtml = sValueTobeShowninDiv;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
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

                if (txtName.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Name can not be blank !</strong>','" + strProjName + "')", true);
                    txtName.Focus();
                    return;
                }
                if (!Regex.IsMatch(txtName.Text.Trim(), strAlphaSpacePattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Name should be only alphabets !</strong>','" + strProjName + "')", true);
                    txtName.Focus();
                    return;
                }

                if (txtEmail.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Email id can not be blank !</strong>','" + strProjName + "')", true);
                    txtEmail.Focus();
                    return;
                }
                if ( !Regex.IsMatch(txtEmail.Text.Trim(), strEmailPattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Invalid email id !</strong>','" + strProjName + "')", true);
                    txtEmail.Focus();
                    return;
                }

                if (txtMobileNumber.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Phone number can not be blank !</strong>','" + strProjName + "')", true);
                    txtMobileNumber.Focus();
                    return;
                }
                if (txtMobileNumber.Text.Trim().Length != 10)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Phone number must be 10 digits !</strong>','" + strProjName + "')", true);
                    txtMobileNumber.Focus();
                    return;
                }
                if (!Regex.IsMatch(txtMobileNumber.Text.Trim(), strNumericPattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Only numeric value allowed in the phone number !</strong>','" + strProjName + "')", true);
                    txtMobileNumber.Focus();
                    return;
                }

                if (txtCompany.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Company name can not be blank !</strong>','" + strProjName + "')", true);
                    txtCompany.Focus();
                    return;
                }
                if (!Regex.IsMatch(txtCompany.Text.Trim(), strAlphaSpacePattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Company name should be only alphabets !</strong>','" + strProjName + "')", true);
                    txtCompany.Focus();
                    return;
                }

                if (txtMsg.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Message can not be blank !</strong>','" + strProjName + "')", true);
                    txtMsg.Focus();
                    return;
                }
                if (txtMsg.Text.Trim().Length > 500)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Your message should be a maximum of 500 characters !</strong>','" + strProjName + "')", true);
                    txtMsg.Focus();
                    return;
                }
                if (! Regex.IsMatch(txtMsg.Text.Trim(), strMsgBoxPattern))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Invalid characters in the message box !</strong>','" + strProjName + "')", true);
                    txtMsg.Focus();
                    return;
                }

                #endregion

                /*---------------------------------------------------------------------*/

                objServiceEntity.StrAction = "CMSCU";
                objServiceEntity.Strusername = txtName.Text.Trim().ToString();
                objServiceEntity.Strmail = txtEmail.Text.Trim().ToString();
                objServiceEntity.Strmobileno = txtMobileNumber.Text.Trim().ToString();
                objServiceEntity.Strcompanyname = txtCompany.Text.Trim().ToString();
                objServiceEntity.StrDescription = txtMsg.Text.Trim().ToString();
                objServiceEntity.IntCreatedBy = Convert.ToInt32(Session["UserId"]);

                string strmsg = objService.AddContact(objServiceEntity);
                if (strmsg == "1")
                {
                    CommonHelperCls comm = new CommonHelperCls();

                    string webvalue = System.Configuration.ConfigurationManager.AppSettings["SMSTOADMIN"];
                    if (System.Configuration.ConfigurationManager.AppSettings["SMSTOADMIN"] != "")
                    {
                        comm.SendSms(webvalue, "An User wants to connect in Single Window Portal ! ");  
                    }

                    if (System.Configuration.ConfigurationManager.AppSettings["MAILADMIN"] != "")
                    {
                        string strmail = System.Configuration.ConfigurationManager.AppSettings["MAILADMIN"];
                        sendMail("An User wants to connect in Single Window Portal", "", strmail, true);
                    }

                    ClearFields();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Message Sent Successfully !</strong>');", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ContactUs");
        }
    }

    protected void sendMail(string strSubject, string strBody, string toEmail, bool enbleSSl)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(ConfigurationManager.AppSettings["smptp"].ToString());
            mail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"].ToString());
            mail.To.Add(toEmail);
            mail.Subject = strSubject;
            strBody += "Name:" + txtName.Text + "<br/>Email:" + txtEmail.Text + "<br/>Phone Number:" + txtMobileNumber.Text + "<br/>Company:" + txtCompany.Text + "<br/>Message:" + txtMsg.Text + "";
            mail.Body = strBody;
            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smptpPort"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromEmailPassword"].ToString());
            SmtpServer.EnableSsl = false;
            mail.IsBodyHtml = true;

            //Warning: do not use this in production code!
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            //These are need tobe comment in PROD server
            // END 
            SmtpServer.Send(mail);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ContactUs");
        }
    }

    private void ClearFields()
    {
        txtName.Text = "";
        txtEmail.Text = "";
        txtMobileNumber.Text = "";
        txtCompany.Text = "";
        txtMsg.Text = "";
        txtCaptcha.Text = "";
    }
}