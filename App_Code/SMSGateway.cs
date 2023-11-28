using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using smsservice;
using System.Xml;
using System.Xml.Linq;
using CSMPDK_3_0;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for SMSGateway
/// </summary>
/// 
//#region "Connection"

//    public Test()
//    {
//        con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
//        if (con.State == ConnectionState.Closed)
//        {
//            con.Open();
//        }
//    }
//    #endregion
public class SMSGateway
{
    string ConnectionString = "AdminAppConnectionProd";  
    public SqlDataReader gSqlDataReader;
    public int gCount;
    //SqlCommand cmd;
    SqlConnection con;
	public SMSGateway()
	{
		//
		// TODO: Add constructor logic here
		//
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
	}
    public string sendBulkSMS(String mobileNos, String message,int status,string ids)
    {
        CSMPDK_3_0.CommonDLL ObjCmnDll = new CSMPDK_3_0.CommonDLL();
        smsconf smsserv = new smsconf();
        AuthenticationHeader clsv = new AuthenticationHeader();
        
        string responseFromServer = string.Empty;
        string mysms = string.Empty;
        string strReceiverTelNo = string.Empty;
        string strSMSBody = string.Empty;
        int intSchemeId = 0;
        string strIp = string.Empty;
        try
        {
            clsv.intDepartmentId = 15;
            clsv.intProjectId = 43;
            clsv.usernm = ids;

            strReceiverTelNo = mobileNos; //Receiver Telephone No
            strSMSBody = message;//"SMS contains will be mentioned here"
            intSchemeId = 36;
            strIp = "120.10.11.198";
            //building the sms string with separtor @.
            mysms = strReceiverTelNo + "@" + strSMSBody + "@" + intSchemeId.ToString() + "@" + strIp.ToString();
            //authentication value is passed to the webservice
            smsserv.AuthenticationHeaderValue = clsv;
            //The sms string is passed to sendmessage methods
            responseFromServer = smsserv.SendMessage(mysms);
            //Save to database for sms tracking
            string retval = string.Empty;
            object[] objParam = { "@vchMobileNo", mobileNos, "@vchMessageText", message, "@intstatus", status, "@intUserId",ids, "@VCHOUTPUT", "out" };
            retval = (string)ObjCmnDll.DbExecuteNonQuery(ConnectionString, "USP_IIMS_SMS_LOG", objParam);
            //object is disposed
            smsserv.Dispose();

        }
        catch (Exception ex)
        {
            responseFromServer = ex.Message;
            // ScriptManager.RegisterStartupScript(//(this, GetType(), "Online Self Seeding", "alert('"+er.Message.ToString()+"');", true);
        }
        //finally { ObjCmnDll.Dispose(); }
        return responseFromServer;
    }
    

}