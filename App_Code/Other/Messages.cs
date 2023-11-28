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
    public static string TitleOfProject = string.Empty;
    //public static string ShowMessage(string intMsg)
    //{
    //    string functionReturnValue = string.Empty;
    //    switch (intMsg)
    //    {
    //        case "1":
    //            functionReturnValue = "Record Saved Successfully";
    //            break;
    //        case "2":
    //            functionReturnValue = "Record Updated Successfully";
    //            break;
    //        case "3":
    //            functionReturnValue = "Record Deleted Successfully";
    //            break;
    //        case "4":
    //            functionReturnValue = "Error Occured";
    //            break;
    //        case "5":
    //            functionReturnValue = "Duplicate Record Found";
    //            break;
    //        case "6":
    //            functionReturnValue = "The Code already Exist";
    //            break;
    //        case "7":
    //            functionReturnValue = "password Changed Successfully";
    //            break;
    //        case "8":
    //            functionReturnValue = "Approved Successfully";
    //            break;
    //        case "9":
    //            functionReturnValue = "Rejected Successfully";
    //            break;
    //        case "10":
    //            functionReturnValue = "Cancelled Successfully";
    //            break;
    //        case "11":
    //            //Modified by Niharika Sethi on 10-05-2013
    //            //correcting the message as deleted to updated
    //            functionReturnValue = "Due To Data Dependency Record Cannot Be Deleted";
    //            break;
    //        case "12":
    //            functionReturnValue = "Data Published Successfully";
    //            break;
    //        case "13":
    //            functionReturnValue = "Complaint Registered Successfully";
    //            break;
    //        case "14":
    //            functionReturnValue = "Your Password has been Reset and the updated Password sent to your MailId.";
    //            break;
    //        case "15":
    //            functionReturnValue = "Forwarded Successfully";
    //            break;
    //        case "16":
    //            functionReturnValue = "Escalation Not set for this complaint type";
    //            break;
    //        case "20":
    //            functionReturnValue = "Status already updated by the system";
    //            break;
    //        default:
    //            functionReturnValue = "Nothing";

    //            break;
    //    }
    //    return functionReturnValue;
    //}
    public static string ShowMessage(string intMsg)
    {
        string functionReturnValue = string.Empty;
        switch (intMsg)
        {
            case "0":
                functionReturnValue = "No Changes ";
                break;
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
                functionReturnValue = "Due to data dependency record cannot be Deleted";
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
                functionReturnValue = "Escalation Not set for this complainttype";
                break;
            case "17":
                functionReturnValue = "As you have already submitted an offline PC, you cannot submit another one";
                break;
            case "18":
                functionReturnValue = "Your PC has been submitted successfully. You cannot apply for another PC until your current application is approved.";
                break;

            //added by Ritika lath for AMS
            case "19":
                functionReturnValue = "Officers Tagged Sucessfully";
                break;
            case "20":
                functionReturnValue = "The selected user has already been tagged to locations.To add or remove the location,update the user record.";
                break;
            case "21":
                functionReturnValue = "Record Unlocked Successfully";
                break;
            case "22":
                functionReturnValue = "Your Account has been Locked";
                break;
            case "23":
                functionReturnValue = "Your Account Deactivated Successfully";
                break;
            case "24":
                functionReturnValue = "Request Approved Successfully";
                break;
            case "25":
                functionReturnValue = "User Activated Successfully";
                break;
            case "26":
                functionReturnValue = "Deaprtment Tagged Successfully";
                break;
            case "27":
                functionReturnValue = "Project Name already Exists";
                break;
            case "28":
                functionReturnValue = "PAN No. already exists";
                break;
            case "29":
                functionReturnValue = "Division Name already Exists";
                break;
            case "30":
                functionReturnValue = "Industry TIN No. is already exists";
                break;
            case "31":
                functionReturnValue = "Schedule Already Generated For The Financial Year ";
                break;
            case "32":
                functionReturnValue = "Please Prepare Holiday Calender Before Schedule";
                break;
            case "33":
                functionReturnValue = "No Inspection During this period";
                break;
            case "34":
                functionReturnValue = "Scheudle already generated";
                break;
            case "35":
                functionReturnValue = "Inspector already tagged";
                break;
            case "37":
                functionReturnValue = "Inspector already assigned for this date";
                break;
            case "38":
                functionReturnValue = "Registration No. already Exists";
                break;
            case "39":
                functionReturnValue = "Duplicate Record Found";
                break;
            case "40":
                functionReturnValue = "Request Added Sucessfully";
                break;
            //added by Ritika lath for AMS
            default:
                functionReturnValue = "Nothing";

                break;
        }
        return functionReturnValue;
    }
}
