using System;
using System.Data;
using System.Configuration;

public partial class SingleWindow_ReminderEMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (true)
        {
            RemiderToMember();
        }
    }

    public void RemiderToMember()
    {
        AMS objams = new AMS();
        DataTable ObjDt = new DataTable();
        try
        {
            objams = new AMS();           
            objams.Action = "A";
            ObjDt = AMServices.GetDefaultMember(objams);

            foreach (DataRow item in ObjDt.Rows)
            {
                SendReminderMail(Convert.ToString(item["USERID"]), Convert.ToString(item["EMAILID"]), Convert.ToString(item["USERNAME"]), Convert.ToString(item["PROJECTNAME"]), Convert.ToString(item["MOBILENO"]), Convert.ToString(item["NODALOFFICER"]));
            }
        }
        catch (Exception m) { Response.Write(m.Message); }
        finally { objams = null; ObjDt = null; }

    }

    #region Mail
    public void SendReminderMail(string UserId, string To, string MemberName, string ProjectName, string MobileNo, string NodalOfficer)
    {
        EmailMsg objMsg = new EmailMsg();
        SendEmail objEmail = new SendEmail();
        SMSGateway objsms = new SMSGateway();
        try
        {
            string SenderName = string.Empty;
            string ToName = string.Empty;

            string strMessage2 = string.Empty;
            strMessage2 = "Dear " + MemberName + ",</br></br>";
            strMessage2 += "<div> Please look after the agenda note prepared by Mr./Mrs./Miss. " + NodalOfficer +". Two days remain for your feedback as per the following details </div><br/><br/>";
            strMessage2 = strMessage2 + "<div><b>Company Name : </b>" + ProjectName + "</div><br>";            
            strMessage2 += "<div>Please login to Agenda Portal for more details.</div>";

            objMsg.PHeader = "";
            objMsg.FromMailId = ConfigurationManager.AppSettings["FromMail"].ToString();
            objMsg.Message1 = "";
            objMsg.Message2 = strMessage2;
            objMsg.Grid = "";
            objMsg.Subject = "Agenda || " + " Reminder !! two days left for feedback";            

            objMsg.ToMailId = To;            
            objMsg.status = "1";
            objMsg.ids = UserId;
            if (!string.IsNullOrEmpty(To) & !string.IsNullOrEmpty(objMsg.FromMailId))
            {
                objEmail.ConfigureMail(objMsg);
                if (MobileNo != "")
                {
                    string status = objsms.sendBulkSMS(MobileNo, "You are requested to give feedback against agenda of " + ProjectName , 1, UserId);                   
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            objEmail = null;
            objMsg = null;
            objsms = null;
        }
    }

    #endregion

}