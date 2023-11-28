using System.Net.Mail;
namespace MailEngine
{
    /// <summary>
    /// Summary description for ICommonMail
    /// </summary>
    public interface ICommonMail
    {
        int SendMail();
        int SendMail(MailMessage message);
        int SendMail(string subject, string from, string to, string cc, string body);
        int SendMail(string subject, string from, string to, string cc, string body, string attachmentPath);
        int SendMail(string subject, string from, string to, MailTemplate template);
        int SendMail(string subject, string from, string to, MailTemplate template, string attachmentPath);
    } 
}