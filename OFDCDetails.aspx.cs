#region  Page Info
//******************************************************************************************************************
// File Name             :   OFDCDetails.aspx.cs
// Description           :   Manage Film Information Details of OFDC
// Created by            :   Manoj Kumar Behera
// Created On            :   11-Sep-2019                
//******************************************************************************************************************
#endregion
#region Namespace
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
using System.IO;



#endregion

public partial class OFDC_Details : System.Web.UI.Page
{
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OFDC");
        }
    }
    #endregion
    protected void ddlFirmType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFirmType.SelectedValue == "1")
        {
            Proprietary.Visible = true;
            Partnership.Visible = false;
            Company.Visible = false;
        }
        else if (ddlFirmType.SelectedValue == "2")
        {
            Proprietary.Visible = false;
            Partnership.Visible = true;
            Company.Visible = false;
        }
        else if (ddlFirmType.SelectedValue == "3")
        {
            Proprietary.Visible = false;
            Partnership.Visible = false;
            Company.Visible = true;
        }
        else
        {
            Proprietary.Visible = false;
            Partnership.Visible = false;
            Company.Visible = false;
        }
    }
    protected void ddlImpa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlImpa.SelectedValue == "1")
        {
            reg.Visible = true;
        }
        else
        {
            reg.Visible = false;
        }
    }
    private void Reset()
    {
        txtDirectorName.Text = "";
        txtDirectorNumber.Text = "";
        txtEstimateCost.Text = "";
        txtFilmDetails.Text = "";
        txtFilmLanguage.Text = "";
        txtFilmName.Text = "";
        txtManagerAddress.Text = "";
        txtManagerName.Text = "";
        txtManagerNumber.Text = "";
        txtProducerAddress.Text = "";
        txtProducerEmail.Text = "";
        txtProducerNumber.Text = "";
        txtProductionHouse.Text = "";
        txtProprietorAddress.Text = "";
        txtProprietorName.Text = "";
        txtProprietorNumber.Text = "";
        txtRegdDate.Text = "";
        txtRegdNo.Text = "";
        txtShareholdersAddress.Text = "";
        txtTentativeDuration.Text = "";
        ddlFirmType.SelectedIndex = 0;
        ddlImpa.SelectedIndex = 0;
        reg.Visible = false;
        Proprietary.Visible = false;
        Partnership.Visible = false;
        Company.Visible = false;
    }  
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txtFilmName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Film Name.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtFilmLanguage.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Film Language.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtProductionHouse.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Production House(s) Name.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtEstimateCost.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Estimated Cost.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtTentativeDuration.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Tentative Duration Of Film.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlImpa.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Select Film Subsidary Bodies.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtRegdNo.Text == "" && ddlImpa.SelectedValue=="1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Registration Number.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtRegdDate.Text == "" && ddlImpa.SelectedValue=="1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Select Registration Date.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtProducerAddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Full Address Of Registered Office.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtProducerNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Registered Office Telephone Number.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtProducerEmail.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Registered Office Email Id.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Select producer's firm.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "1" && txtProprietorName.Text=="")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Name Of The Proprietor.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "1" && txtProprietorAddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Address Of The Proprietor.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "1" && txtProprietorNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Phone Number Of The Proprietor.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "1" && fluProprietorDoc.HasFile == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Upload An Affidavit.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "2" && txtManagerName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Name Of The Managing Partner.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "2" && txtManagerAddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Address Of The Managing Partner.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "2" && txtManagerNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Phone Number Of The Managing Partner.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "2" && fluManagerDoc.HasFile == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Upload Partnership Deed.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "3" && txtDirectorName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Name Of The Managing Director.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "3" && txtShareholdersAddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Address Of The Shareholder.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "3" && txtDirectorNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Phone Number Of The Managing Director.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "3" && fluDirectorDoc.HasFile == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Upload Articles Of Association.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtFilmDetails.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Previous Film Details.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else
        {
            try
            {
               //sendMail("Data Capture For OFDC", "", "go.swift@abmindia.com", true);
            }
            catch(Exception ex) {
                Util.LogError(ex, "OFDCSave");
            }
            Reset();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Thank You.You have successfully registered in GO SWIFT.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
    }
    protected void btndraft_Click(object sender, EventArgs e)
    {
        if (txtFilmName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Film Name.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtFilmLanguage.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Film Language.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtProductionHouse.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Production House(s) Name.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtEstimateCost.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Estimated Cost.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtTentativeDuration.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Tentative Duration Of Film.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlImpa.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Select Film Subsidary Bodies.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtRegdNo.Text == "" && ddlImpa.SelectedValue == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Registration Number.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtRegdDate.Text == "" && ddlImpa.SelectedValue == "1")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Select Registration Date.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtProducerAddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Full Address Of Registered Office.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtProducerNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Registered Office Telephone Number.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtProducerEmail.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Registered Office Email Id.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Select producer's firm.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "1" && txtProprietorName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Name Of The Proprietor.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "1" && txtProprietorAddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Address Of The Proprietor.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "1" && txtProprietorNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Phone Number Of The Proprietor.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "1" && fluProprietorDoc.HasFile == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Upload An Affidavit.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "2" && txtManagerName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Name Of The Managing Partner.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "2" && txtManagerAddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Address Of The Managing Partner.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "2" && txtManagerNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Phone Number Of The Managing Partner.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "2" && fluManagerDoc.HasFile == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Upload Partnership Deed.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "3" && txtDirectorName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Name Of The Managing Director.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "3" && txtShareholdersAddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Address Of The Shareholder.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "3" && txtDirectorNumber.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Phone Number Of The Managing Director.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (ddlFirmType.SelectedValue == "3" && fluDirectorDoc.HasFile == false)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Upload Articles Of Association.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else if (txtFilmDetails.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please Enter Previous Film Details.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
        }
        else
        {
            try
            {
               // sendMail("Data Capture For OFDC", "", "go.swift@abmindia.com", true);
            }
            catch(Exception ex) {
                Util.LogError(ex, "OFDCDraft");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Thank You.You have successfully registered in GO SWIFT.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
            return;
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
            mail.Bcc.Add("manojbehera020@gmail.com");
            mail.Bcc.Add("sushant.jena007@gmail.com");
            mail.Bcc.Add("ramarao.teki@csm.co.in");
            mail.Subject = strSubject;
            strBody = "Application for Enrollment<br/>";
            strBody += "-----------------------------------<br/><br/>";
            strBody += "Name of the film :  " + txtFilmName.Text + "<br/><br/>Language of the film :  " + txtFilmLanguage.Text + "<br/><br/>Name of the production house(s) :  " + txtProductionHouse.Text + "<br/><br/>Estimated cost of the film :  " + txtEstimateCost.Text + "<br/><br/>Tentative duration of the film :  " + txtTentativeDuration.Text + "<br/><br/>";
            strBody += "IMPA :  " + ddlImpa.SelectedItem.Text + "<br/><br/>";           
            if (ddlImpa.SelectedValue == "1")
            {
                strBody += "Registration No :  " + txtRegdNo.Text + "<br/><br/>Registration Date :  " + txtRegdDate.Text + "<br/><br/>";
            }
            strBody += "Full address of registered office of the producer(s) :  " + txtProducerAddress.Text + "<br/><br/>Office details Telephone Number :  " + txtProducerNumber.Text + "<br/><br/>Office details Email Id :  " + txtProducerEmail.Text + "<br/><br/>";
            strBody += "Type of producer's firm :  " + ddlFirmType.SelectedItem.Text + "<br/><br/>";
            if (ddlFirmType.SelectedValue == "1")
            {
                strBody += "Name of the Proprietor :  " + txtProprietorName.Text + "<br/><br/>Address of the Proprietor :  " + txtProprietorAddress.Text + "<br/><br/>Telephone Number of the Proprietor :  " + txtProprietorNumber.Text + "<br/><br/>";
            }
            if (ddlFirmType.SelectedValue == "2")
            {
                strBody += "Names of the Managing Partner :  " + txtManagerName.Text + "<br/><br/>Address of the Managing Partner :  " + txtManagerAddress.Text + "<br/><br/>Telephone Number of the Managing Partner :  " + txtManagerNumber.Text + "<br/><br/>";
            }
            if (ddlFirmType.SelectedValue == "3")
            {
                strBody += "Name of the Managing Director :  " + txtDirectorName.Text + "<br/><br/>Address of Shareholders :  " + txtShareholdersAddress.Text + "<br/><br/>Telephone Number of the Managing Director :  " + txtDirectorNumber.Text + "<br/><br/>";
            }
            strBody += "Details of previous film produced by the producer concern :  " + txtFilmDetails.Text + "";
            mail.Body = strBody;
            SmtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smptpPort"].ToString());
            SmtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["FromEmailPassword"].ToString());
            SmtpServer.EnableSsl = false;
            mail.IsBodyHtml = true;



            //Check for the attchment

            if (fluProprietorDoc.HasFile==true)
            {
                HttpPostedFile file = fluProprietorDoc.PostedFile;
                string Name = "OFDC_Proprietor";
                string fileName = Name + Guid.NewGuid().ToString("N").Substring(0, 4);
                file.SaveAs(Server.MapPath("~/OFDC/") + fileName);
                mail.Attachments.Add(new Attachment(file.InputStream, fileName));          
            }
            if (fluManagerDoc.HasFile == true)
            {
                HttpPostedFile file = fluManagerDoc.PostedFile;
                string Name = "OFDC_Partnership";
                string fileName = Name + Guid.NewGuid().ToString("N").Substring(0, 4);
                file.SaveAs(Server.MapPath("~/OFDC/") + fileName);
                mail.Attachments.Add(new Attachment(file.InputStream, fileName));
            }
            if (fluDirectorDoc.HasFile == true)
            {
                HttpPostedFile file = fluDirectorDoc.PostedFile;
                string Name = "OFDC_Company";
                string fileName = Name + Guid.NewGuid().ToString("N").Substring(0, 4);
                file.SaveAs(Server.MapPath("~/OFDC/") + fileName);
                mail.Attachments.Add(new Attachment(file.InputStream, fileName));
            }


            //Warning: do not use this in production code!
            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

            //These are need tobe comment in PROD server
            // END 
            SmtpServer.Send(mail);
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "OFDCMail");
            return;
        }
    }
}