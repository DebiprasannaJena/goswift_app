using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Messages
/// </summary>
public class Messages
{
    public static string TitleOfProject = ConfigurationManager.AppSettings["TitleOfProject"].ToString();

    public static string ShowMessage(string intMsg)
    {
        string functionReturnValue = string.Empty;
        switch (intMsg)
        {
            case "1":
                functionReturnValue = "Record Saved Successfully";
                break;
            case "2":
                functionReturnValue = "Record Updated Successfully";
                break;
            case "3":
                functionReturnValue = "Record Deleted Successfully";
                break;
            case "4":
                functionReturnValue = "Error Occured";
                break;
            case "5":
                functionReturnValue = "Duplicate Record Found";
                break;
            case "6":
                functionReturnValue = "The Code already Exist";
                break;
            case "7":
                functionReturnValue = "password Changed Successfully";
                break;
            case "8":
                functionReturnValue = "Approved Successfully";
                break;
            case "9":
                functionReturnValue = "Rejected Successfully";
                break;
            case "10":
                functionReturnValue = "Cancelled Successfully";
                break;
            case "11":
                //Modified by Niharika Sethi on 10-05-2013
                //correcting the message as deleted to updated
                functionReturnValue = "Due to data dependency record cannot be Updated";
                break;
            case "12":
                functionReturnValue = "Data Published Successfully";
                break;
            case "13":
                functionReturnValue = "Complaint Registered Successfully";
                break;
            case "14":
                functionReturnValue = "Your Password has been Reset and the updated Password sent to your MailId.";
                break;
            case "15":
                functionReturnValue = "Forwarded Successfully";
                break;
            case "16":
                functionReturnValue = "Escalation Not set for this complaint type";
                break;
            case "17":
                functionReturnValue = "Record Rejected Successfully";
                break;
            default:
                functionReturnValue = "Nothing";

                break;
        }
        return functionReturnValue;
    }

}
