using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;

public partial class thankYou : System.Web.UI.Page
{
    InvestorBusinessLayer objService = new InvestorBusinessLayer();
    InvestorDetails objInvestor = new InvestorDetails();

    CommonHelperCls ob = new CommonHelperCls();

    bool smsStatus;
    bool mailStatus;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strUserId = Convert.ToString(Session["UserIdDD"]);
        string strUnitName = Convert.ToString(Session["UnitName"]);
        string strName = Session["UserName"].ToString();
        string strMobile = Session["MobileNo"].ToString();

        string[] strEmailId = new string[1];
        strEmailId[0] = Convert.ToString(Session["EmailId"]);

        ///// Email and SMS to Admin
        //CommonHelperCls comm = new CommonHelperCls();
        //if (System.Configuration.ConfigurationManager.AppSettings["SMSTOADMIN"] != "")
        //{
        //    string SMSContent = "A new investor (" + strname + ") has been Registered in Single Window Portal !";
        //    //comm.SendSms(webvalue, "A new investor (" + strname + ") has been Registered in Single Window Portal ! ");
        //    smsStatus = ob.SendSmsNew(webvalue, SMSContent);
        //    string str = ob.UpdateMailSMSStaus("InvestorRegistration", webvalue, "", "", "0", "0", 0, "0", SMSContent, SMSContent, smsStatus, mailStatus);
        //}

        ///// Send SMS
        string strSMSContent = "Your user id " + strUserId + " and profile have been created successfully.";
        smsStatus = ob.SendSmsNew(strMobile, strSMSContent);

        ///// Send Email
        string strMailSubject = "GOSWIFT Registration";
        string strMailBody = "Your application of unit name " + strUnitName + " and user id " + strUserId + " have been registered successfully.";
        mailStatus = ob.sendMail(strMailSubject, strMailBody, strEmailId, true);

        ///// Update Message Information
        string str = ob.UpdateMailSMSStaus("InvestorRegistration", strMobile, "", "Registration", "0", "0", 0, "0", strSMSContent, strSMSContent, smsStatus, mailStatus);

        Session["UserIdDD"] = null;
        Session["UserName"] = null;
        Session["UnitName"] = null;
    }
}