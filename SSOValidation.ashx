<%@ WebHandler Language="C#" Class="SSOValidation" %>

using System;
using System.Web;
using System.Configuration;
using SSOService;
using System.Data.SqlClient;
using System.Collections.Generic;
using CSMPDK_3_0;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using BusinessLogicLayer.Login;
using EntityLayer.Login;
using System.Web.Services;
using System.Linq;
using DWHServiceReference;

public class SSOValidation : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    List<LoginDetails> objloginlist = new List<LoginDetails>();
    LoginBusinessLayer objService = new LoginBusinessLayer();
    InvestorBusinessLayer objRegInvService = new InvestorBusinessLayer();
    String url = "";

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Write("Hello World");
        string param = context.Request.QueryString["param"];
        if (param == "" || param == null)
        {
            context.Response.Redirect("Login.aspx", true);
        }
        else
        {
            try
            {
                DWHServiceHostClient validate = new DWHServiceHostClient();
                string token = validate.GetTokeFromQueryString(param, validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]));
                DWH_Model[] user;
                user = validate.GetIndustrtyDetails(token, validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]));
                string id = validate.GetLogIdFromQueryString(param, validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]));
                string USERid = validate.GetUserIdFromQueryString(param, validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]));
                if (user != null)
                {
                    if (user.Length != 0)
                    {
                          DWH_Model[] activeuser;
                          activeuser = user;
                          ValidateLoginVal(activeuser[0].VCHUSERUNIQUEID, id, USERid);
                          url = validate.GetSecureURL("InvesterDashboard.aspx?Token=" + token, validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]));
                          context.Response.Redirect(url, true);
                    }
                    else
                    {
                         context.Response.Redirect("Login.aspx", true);
                    }
                }
                else
                {
                    context.Response.Redirect("Login.aspx", true);
                }

            }
            catch(Exception ex) {
                Util.LogError(ex, "Applicationmoved");
            }
        }
    }
    public void ValidateLoginVal(String uid, string id, string UserId)
    {
        CSMPDK_3_0.CommonDLL objCmndll = new CSMPDK_3_0.CommonDLL();
        SqlDataReader drUserDet;
        string strQuery = "";
        try
        {
            strQuery = "SELECT ISNULL(VCH_INV_NAME,'') AS VCH_INV_NAME,INT_INVESTOR_ID,ISNULL(VCH_INV_USERID,'') AS VCH_INV_USERID,ISNULL(VCH_CONTACT_FIRSTNAME,'')+' '+ISNULL(VCH_CONTACT_MIDDLENAME,'')+' '+ISNULL(VCH_CONTACT_LASTNAME,'')as USERNAME,Convert(varchar(50),DTM_CREATED_ON,106) AS REGDATE,isnull(VCH_GSTIN,0)as VCH_GSTIN,isnull(VCH_UNIQUEID,'') AS UNIQUEID from M_INVESTOR_DETAILS where VCH_UNIQUEID='" + uid + "'";
            drUserDet = (SqlDataReader)objCmndll.ExeReader("AdminAppConnectionProd", strQuery);
            if (drUserDet.Read())
            {
                HttpContext.Current.Session["UserId"] = drUserDet["VCH_INV_USERID"].ToString();
                HttpContext.Current.Session["UserName"] = drUserDet["USERNAME"].ToString();
                HttpContext.Current.Session["UID"] = drUserDet["UNIQUEID"].ToString();
                HttpContext.Current.Session["LogId"] = id;
                HttpContext.Current.Session["SSOUserId"] = UserId;
                HttpContext.Current.Session["InvestorId"] = drUserDet["INT_INVESTOR_ID"].ToString();
                HttpContext.Current.Session["RegDate"] = drUserDet["REGDATE"].ToString();
                HttpContext.Current.Session["LastLoginTime"] = string.Format("{0:hh:mm:ss tt}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                HttpContext.Current.Session["GSTIN"] = drUserDet["VCH_GSTIN"].ToString();
                HttpContext.Current.Session["IndustryName"] = drUserDet["VCH_INV_NAME"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}