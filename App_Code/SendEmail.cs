using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using MailEngine;
using CSMPDK_3_0;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for SendEmail
/// </summary>
public class SendEmail
{
    string ConnectionString = "AdminAppConnectionProd";
    public SqlDataReader gSqlDataReader;
    public int gCount;
    //SqlCommand cmd;
    SqlConnection con;
	public SendEmail()
	{
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
	}

    public string PHeader { get; set; }
    public string Message1 { get; set; }
    public string Message2 { get; set; }
    public string Grid { get; set; }
    public string ToMailId { get; set; }
    public string CcMailId { get; set; }
    public string FromMailId { get; set; }
    public string Subject { get; set; }
    public string ids { get; set; }
    public string status { get; set; }

    #region Email Configure

    private string PopulateBody()
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/EmailTemplate.htm")))
        {
            body = reader.ReadToEnd();
        }

        body = body.Replace("{Header}", PHeader);
        body = body.Replace("{Message1}", Message1);
        body = body.Replace("{Message2}", Message2);
        body = body.Replace("{Grid}", Grid);
        //if(status=="1")
            body = body.Replace("{Url}", "http://ams.investodisha.org/Login.aspx");
        //else
        //    body = body.Replace("{Url}", "http://ams.investodisha.org/ILogin.aspx");
        return body;
    }

    public void ConfigureMail(EmailMsg listMsg)
    {
       
        CommonMail objMail = new CommonMail();
        CSMPDK_3_0.CommonDLL ObjCmnDll = new CSMPDK_3_0.CommonDLL();
        FromMailId = listMsg.FromMailId;
        ToMailId = listMsg.ToMailId;
        if (!string.IsNullOrEmpty(listMsg.CcMailId))
            CcMailId = listMsg.CcMailId;
        else
            CcMailId = null;
        Subject = listMsg.Subject;
        PHeader = listMsg.PHeader;
        Message1 = listMsg.Message1;
        Message2 = listMsg.Message2;
        Grid = listMsg.Grid;
        status=listMsg.status;
        ids = listMsg.ids;
        try
        {
           // int mail = 0;            
            string body = this.PopulateBody();
            if (ToMailId != "")
            {
                //mail = objMail.SendMail(Subject, FromMailId, ToMailId,CcMailId, body);
                CommonMail.sendEmail(Subject, FromMailId, ToMailId, body, "", "", "GO-SMILE");    
                string retval = string.Empty;
                object[] objParam = { "@vchEmail", ToMailId, "@vchMailText", Message2, "@intstatus", status, "@intUserId", ids, "@vchMailSubject", Subject,"@vchCCId",CcMailId, "@VCHOUTPUT", "out" };
                retval = (string)ObjCmnDll.DbExecuteNonQuery(ConnectionString, "USP_IIMS_MAIL_LOG", objParam);
               
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objMail = null;
            //ObjCmnDll.Dispose(); 
        }
      
    }
   
    #endregion

}

public class EmailMsg
{
    public string PHeader { get; set; }
    public string Message1 { get; set; }
    public string Message2 { get; set; }
    public string Grid { get; set; }
    public string ToMailId { get; set; }
    public string CcMailId { get; set; }
    public string FromMailId { get; set; }
    public string Subject { get; set; }
    public string ids { get; set; }
    public string status { get; set; }
}