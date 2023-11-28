/**'******************************************************************************
 File Name             :   Login.aspx
 Description           :   To Check Login validity of  User.
 Created by            :   Tapan Kumar Mishra
 Created On            :   03-Dec-2014
 Modification History  :
                           <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         

 Procedure Name       :   
'**************************************************************************************/
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using Admin.CommonFunction;
using AdminApp.Business;
using AdminApp.Model;
using System.Data;
public partial class Login : System.Web.UI.Page
{
    AdminAppService objBAL = new AdminAppService();

    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
    Byte[] hashedBytes;
    UTF8Encoding encoder = new UTF8Encoding();

    AdminApp.Model.IPTrack objiptrack = null;
    List<AdminApp.Model.IPTrack> lstobjtrack = new List<IPTrack>();
    IList<AdminApp.Model.Login> objloginlist = new List<AdminApp.Model.Login>();

    string strLocation = "";
    string strPwd = "";
    string strUserId, strUserName, strDeptName, strSubDept, strFullName, strEmail, strDept, strImage;
    int LocationID, LevelID, LogFlag;
    string pwdString, strLclSalt, strLclPwd1;
    string adminstatus, isAdmin, isRAAllowed, strType, accl;
    public static string strHeaderColor, strPageBgColor, strlogo, strFlashSrc1, strFlashSrc2, strstripcolor, UserType, LockUserType;
    public DateTime dtmValid, dtmLock;
    public int Alert, DayDiff, Attempt;
    public static int intCounter = 1;
    IList<AdminApp.Model.Login> objlogins=new List<AdminApp.Model.Login>();
    #region Page Initialisation
    protected void Page_Init(object Sender, EventArgs e)
    {
        Dictionary<string, bool> dicConfigAddUser = new Dictionary<string, bool>();
        dicConfigAddUser = (Dictionary<string, bool>)Session["ConfigAddUser"];

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        if (Request.Cookies["__AntiXsrfToken"] != null)
        {
            HttpCookie myCookie = new HttpCookie("__AntiXsrfToken");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }
    }
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        txtusr.Text = Util.CheckValidValueInject(txtusr.Text);
        txtpwd.Text = Util.CheckValidValueInject(txtpwd.Text);

        if (ConfigurationManager.AppSettings["Config"] != null)
        {
            if (ConfigurationManager.AppSettings["Config"].ToString() == "Y")
            {
                Response.Redirect("~/Console/ConfigPage.aspx");
            }
        }
        else
        {
            Response.Redirect("~/Console/ConfigPage.aspx");
        }
        
        this.btnSubmit.Attributes.Add("onClick", "return CheckValidation();");
        this.txtusr.Focus();
        Page.Form.DefaultButton = btnSubmit.ClientID;
    }
    #endregion

    #region "Button Events"

    /// <summary>
    /// To submit to check user id and password for login.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        var s = Util.CheckValidValueInject(txtpwd.Text);
        UserType = "1";
        if (Session["CurUser"] == null)
        {
            intCounter = 1;
        }
        else if (Session["CurUser"].ToString() != txtusr.Text)
        {
            intCounter = 1;
        }
        IList<AdminApp.Model.Login> objlogins = objBAL.ViewConfiguartion("E");
        foreach (AdminApp.Model.Login data in objlogins)
        {
            Attempt = data.intAttempt;//No Of Unsuccessful Attempt

        }
        if (Attempt == 0)
        {
            Attempt = 3;
        }
        this.hidmsg.Value = "";
        strLclSalt = Request.Form["hidSlt"];
        if ((this.txtpwd.Text != ""))
        {
            string strClientIp = null;
            strClientIp = Request.ServerVariables["HTTP_X_FOWARDED_FOR"];
            if (strClientIp == string.Empty || strClientIp == null)
            {
                strClientIp = Request.ServerVariables["REMOTE_ADDR"];
                if (strClientIp == "::1")
                {
                    strClientIp = "127.0.0.1";
                }
            }
            objloginlist = objBAL.LoginUser("V", HttpUtility.HtmlEncode(txtusr.Text), strPwd, 0, strClientIp);

            if (objloginlist.Count > 0)
            {
                foreach (AdminApp.Model.Login objlist in objloginlist)
                {
                    strLclPwd1 = objlist.Password;
                    hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(strLclPwd1 + strLclSalt));
                    pwdString = BitConverter.ToString(hashedBytes);
                    pwdString = pwdString.Replace("-", null);
                }
            }
        }

        else
        {
            this.hidmsg.Value = "Invalid User Name or Password";
        }
        if (pwdString != null)
        {

            if (this.txtpwd.Text.ToUpper().Trim() == pwdString.ToUpper().Trim())
            {
                if (objloginlist.Count > 0)
                {
                    IList<AdminApp.Model.Login> objloginlist3 = objBAL.LoginUser("A", HttpUtility.HtmlEncode(txtusr.Text), strPwd, Convert.ToInt32(UserType), "");
                    if (objloginlist3.Count > 0)
                    {
                        foreach (AdminApp.Model.Login objlist in objloginlist3)
                        {
                            LockUserType = objlist.Type;
                        }
                        if (UserType == LockUserType)
                        {
                            foreach (AdminApp.Model.Login objlist in objloginlist3)
                            {
                                dtmLock = objlist.dtStartTime;//--Account Lock Period
                            }
                        }

                    }


                    if (dtmLock <= DateTime.Now)
                    {
                        foreach (AdminApp.Model.Login objlist in objloginlist)
                        {
                            intCounter = 1;
                            LocationID = objlist.Locid;
                            LevelID = objlist.LevelId;
                            strUserId = objlist.UserID;
                            strUserName = objlist.UserName;
                            strFullName = objlist.FullName;
                            strDept = objlist.DepartmentID;
                            strEmail = objlist.email;
                            accl = objlist.AccessLev;
                            isAdmin = objlist.AdminPrev;
                            isRAAllowed = objlist.Security;
                            strType = objlist.Type;
                            strImage = objlist.UserImage;
                            LogFlag = objlist.intAlert;
                           // EmpType = objlist.EmpType;
                            if (objlist.strDepartment == null)
                            {
                                strDeptName = "";
                            }
                            else
                            {
                                strDeptName = objlist.strDepartment;
                            }
                            if (objlist.strPID == null)
                            {
                                strSubDept = "";
                            }
                            else
                            {
                                strSubDept = objlist.strPID;
                            }
                            if (isAdmin == "True")
                            {
                                adminstatus = "super";
                            }
                            else if (objlist.SubAdminPrev == "1")
                            {
                                adminstatus = "loc";
                            }
                            else
                            {
                                adminstatus = "";
                            }
                        }
                        CreateSession();
                       
                        IpTracking();
                        //if (Session["adminstat"].ToString().ToUpper() == "SUPER")
                        //{
                        CommonFunction.CreateUsersXML(Convert.ToInt32(Session["UserId"]));
                        CommonFunction.CreateHierarchyXml();
                        //}
                        Session["RandomNo"] = CommonFunction.GenerateRandomNum();
                    }
                    else
                    {
                        this.hidmsg.Value = "You system is locked upto " + dtmLock + ".";
                    }

                }


                if (LogFlag == 1)//----Password Expiry and Change Password Status Check-------
                {
                    IList<AdminApp.Model.Login> objloginlist1 = objBAL.LoginUser("E", HttpUtility.HtmlEncode(txtusr.Text), strPwd, 0, "");
                    if (objloginlist1.Count > 0)
                    {
                        foreach (AdminApp.Model.Login objlist in objloginlist1)
                        {
                            dtmValid = objlist.dtEndTime;//--Password Expiry Date
                        }
                    }
                    IList<AdminApp.Model.Login> objlogin = objBAL.ViewConfiguartion("E");
                    foreach (AdminApp.Model.Login data in objlogin)
                    {
                        Alert = data.intAlert;//Alert Before

                    }
                    TimeSpan timespan = (dtmValid.AddDays(1) - DateTime.Now);
                    DayDiff = timespan.Days;
                    if (DayDiff > 0 && DayDiff <= Alert)
                    {
                        Response.Write("<script>alert('Your Password will be expire after " + DayDiff + " days, change your password as soon as possible !');document.location.href='Dashboard/dashboard.aspx?linkm=1&linkn=0&ranNum=" + Session["RandomNo"].ToString() +"'</script>");
                    }
                    else if (DayDiff == 0)
                    {
                        if (Session["adminstat"].ToString().ToUpper() == "SUPER")
                            Response.Redirect("Dashboard/dashboard.aspx?linkm=1&linkn=0&ranNum=" + Session["RandomNo"].ToString());
                        else
                            Response.Write("<script>alert('Your Password has been expired. Please contact administrator to reset password !');document.location.href='PasswordExpiry.aspx'</script>");
                    }
                    else
                    {
                        Response.Redirect("Dashboard/dashboard.aspx?linkm=1&linkn=0&ranNum=" + Session["RandomNo"].ToString());
                    }

                }
                else
                {
                    if (dtmLock <= DateTime.Now)
                    {
                        Response.Redirect("Dashboard/dashboard.aspx?linkm=1&linkn=0&ranNum=" + Session["RandomNo"].ToString());
                    }
                    else
                    {

                    }

                }
            }
            else
            {
                if (intCounter == Attempt)
                {
                    IList<AdminApp.Model.Login> objloginlist2 = objBAL.LoginUser("A", HttpUtility.HtmlEncode(txtusr.Text), strPwd, Convert.ToInt32(UserType), "");
                    if (objloginlist2.Count > 0)
                    {
                        foreach (AdminApp.Model.Login objlist in objloginlist2)
                        {
                            dtmLock = objlist.dtStartTime;//--Account Lock Period
                        }
                    }
                    this.hidmsg.Value = "You have no attempts remaining and the system is locked.";

                    objBAL.ChangePasswordDtl(UserType, txtusr.Text, "");
                }
                else
                {
                    string str = Convert.ToString(Attempt - intCounter);
                    this.hidmsg.Value = "Invalid User Name or Password.You have " + str + " tries left.";
                    intCounter = intCounter + 1;
                    Session["CurUser"] = txtusr.Text;
                }
            }
        }
        else
        {
            this.hidmsg.Value = "Invalid User Name or Password";
        }
    }

    #endregion

    #region User functions

    private void CreateSession()
    {
        Session["LevelID"] = LevelID;
        Session["locId"] = LocationID;
        Session["location"] = strLocation;
        Session["DeptId"] = strDept;//Stores Level details Id
        Session["Department"] = strDeptName;
        Session["SubDept"] = strSubDept;
        Session["UserId"] = strUserId;
        Session["userName"] = strUserName;
        Session["fullName"] = strFullName;
        Session["adminstat"] = adminstatus;
        Session["menuCnt"] = 0;
        Session["strImage"] = strImage;
        Session["UsrName"] = strUserName;
        Session["UserTypeId"] = 1;
        Session["EmailId"] = strEmail;
        //Session["EmpType"] = EmpType;
         
    }
    
    private string GenerateHash(string SourceText)
    {
        UTF8Encoding Ue = new UTF8Encoding();
        string pwdString = null;
        MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
        byte[] ByteHash = Md5.ComputeHash(Ue.GetBytes(SourceText));
        pwdString = BitConverter.ToString(ByteHash);
        pwdString = pwdString.Replace("-", null);
        return pwdString;
    }
    private void IpTracking()
    {

        objiptrack = new AdminApp.Model.IPTrack();
        objiptrack.ActionCode = "A";
        objiptrack.Id = 0;
        objiptrack.UserId = Convert.ToInt32(HttpContext.Current.Session["userid"]);
        objiptrack.UserName = Convert.ToString(HttpContext.Current.Session["userName"]);
        string strClientIp = null;
        strClientIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FOWARDED_FOR"];
        if (strClientIp == string.Empty || strClientIp == null)
        {
            strClientIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        objiptrack.IpAddress = strClientIp;
        objiptrack.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
        objBAL.IpTracking(objiptrack);
    }
    #endregion
    protected void lbtnForgot_Click(object sender, EventArgs e)
    {
        Server.Transfer("ForgotPassword.aspx");
    }
}

    

