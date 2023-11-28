using System;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
//using CSM.Mail.Extension;
using MailEngine.TemplateParser;
using System.Configuration;
namespace MailEngine
{

    /// <summary>
    /// Summary description for CommonMail
    /// </summary>
    /// <createdby>Manas Bej</createdby>
    public class CommonMail : ICommonMail
    {
        /// <summary>
        /// Return status
        /// </summary>
        private int returnStatus;
        /// <summary>
        /// SMTP configuration setting
        /// </summary>
        private SmtpSection SmtpSetting { get; set; }
        /// <summary>
        /// Email message
        /// </summary>
        public MailMessage EmailMessage { get; set; }
        /// <summary>
        /// Email template
        /// </summary>
        public MailTemplate Template { get; set; }
        /// <summary>
        /// Default constructor
        /// </summary>
        public CommonMail()
        {
            returnStatus = 1;
            //Read the SMTP setting from configuration file
            this.SmtpSetting = new SmtpSection();
            if (SmtpSetting == null)
                throw new Exception("system.net/mailSettings required.");
        }
        /// <summary>
        /// Send email
        /// </summary>
        public int SendMail()
        {
            if (EmailMessage.From == null)
                throw new Exception("Sender address is not provided");
            if (EmailMessage.To == null)
                throw new Exception("Receiver address is not provided");
            
            try
            {
                returnStatus = 0;
                //Initialize the SMTP with host and port
                SmtpClient client = new SmtpClient(SmtpSetting.Network.Host, SmtpSetting.Network.Port);
                client.UseDefaultCredentials = SmtpSetting.Network.DefaultCredentials;
                //if the client use Network credential, then we have to provide the username and password
                if (!client.UseDefaultCredentials)
                {
                    //Providing the network credential
                    client.Credentials = new System.Net.NetworkCredential(SmtpSetting.Network.UserName, SmtpSetting.Network.Password);
                }
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //Sending the mail
                client.Send(EmailMessage);
                returnStatus = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnStatus;
        }
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="message">MailMessage object</param>
        public int SendMail(MailMessage message)
        {
            this.EmailMessage = message;
            return returnStatus;
        }
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="from">Sender email address</param>
        /// <param name="to">Receipent email adddress</param>
        /// <param name="body">email content</param>
        public int SendMail(string subject, string from, string to, string cc, string body)
        {
            //Sending the mail
            return SendMail(subject, from, to,cc, body, null);
        }
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="from">Sender email address</param>
        /// <param name="to">Receipent email adddress</param>
        /// <param name="body">email content</param>
        /// <param name="attachmentPath">Attachment file path</param>
        public int SendMail(string subject, string from, string to, string cc, string body, string attachmentPath)
        {
            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
            {
                //Initializing mail message with sender and receiver
                MailMessage mail = new MailMessage(from, to);
                //Add CC
                if (cc != null)
                    mail.CC.Add(cc);
                //Assigning the subject
                mail.Subject = subject;
                //Assigning the body content
                mail.Body = body;
                mail.IsBodyHtml = true;
                //Check for the attchment
                if (attachmentPath != null)
                {
                    //If any file is there, attach it 
                    mail.Attachments.Add(new Attachment(attachmentPath));
                }
                this.EmailMessage = mail;
                //Send the mail
                return this.SendMail();
            }
            else
                return 0;
        }
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="from">Sender email address</param>
        /// <param name="to">Receipent email adddress</param>
        /// <param name="template">Email template</param>
        public int SendMail(string subject, string from, string to, MailTemplate template)
        {
            return  this.SendMail(subject, from, to, template, null);
        }
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="from">Sender email address</param>
        /// <param name="to">Receipent email adddress</param>
        /// <param name="template">Email template</param>
        /// <param name="attachmentPath">Attachment file path</param>
        public int SendMail(string subject, string from, string to, MailTemplate template, string attachmentPath)
        {
            MailMessage mail = new MailMessage(from, to);
            mail.Subject = subject;
            Parser parser = new Parser(HttpContext.Current.Server.MapPath(template.TemplatePath), template.Verbs);
            mail.IsBodyHtml = true;
            mail.Body = parser.Parse();
            if (attachmentPath != null)
            {
                mail.Attachments.Add(new Attachment(attachmentPath));
            }
            this.EmailMessage = mail;
            return this.SendMail();
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="subject">Subject</param>
        /// <param name="strFrom">Sender email address</param>
        /// <param name="strTo">Receipent email adddress</param>
        /// <param name="strBody">Email template</param>
        /// <param name="strCc">CC</param>
        /// <param name="strBcc">BCC</param>
        /// <param name="displayName">Display name</param>
        public static bool sendEmail(string strSubject, string strFrom,
           string strTo, string strBody, string strCc,
           string strBcc, string displayName)
        {
            SmtpClient Mail = new SmtpClient();
            Mail.Host = ConfigurationManager.AppSettings["WebmailHost"].ToString();


            string username = null;
            string Password = null;
            if (ConfigurationManager.AppSettings["UseDefaultCredentials"].ToString() + "" == "false")
            {
                Mail.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                username = ConfigurationManager.AppSettings["WebmailUsername"].ToString();
                Password = ConfigurationManager.AppSettings["WebmailPassword"].ToString();
                System.Net.NetworkCredential basicAuthenticationInfo =
                  new System.Net.NetworkCredential(username, Password);
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["WebmailPort"].ToString()))
                {
                    Mail.Port = Convert.ToInt16(ConfigurationManager.AppSettings["WebmailPort"].ToString());
                }
                Mail.UseDefaultCredentials = false;
                Mail.Credentials = basicAuthenticationInfo;
                if (ConfigurationManager.AppSettings["EnableSsl"].ToString() == "True")
                    Mail.EnableSsl = true;
            }

            System.Net.Mail.MailMessage myMail = new System.Net.Mail.MailMessage();
            myMail.Subject = strSubject;

            myMail.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["WebmailUsername"].ToString());
            myMail.To.Add(new System.Net.Mail.MailAddress(strTo));
            if (!string.IsNullOrEmpty(strCc))
            {
                myMail.CC.Add(new System.Net.Mail.MailAddress(strCc));
            }
            if (!string.IsNullOrEmpty(strBcc))
            {
                myMail.Bcc.Add(new System.Net.Mail.MailAddress(strBcc));
            }
            if (ConfigurationManager.AppSettings["sendBccTest"].ToString() == "True")
                myMail.Bcc.Add(new System.Net.Mail.MailAddress(
                ConfigurationManager.AppSettings["sendBccTestEmailAddress"].ToString()));


            myMail.IsBodyHtml = true;
            myMail.Body = strBody;
            try
            {
                Mail.Send(myMail);

                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;

            }
        }

    }

    
}