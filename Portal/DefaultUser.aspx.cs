using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using Admin.CommonFunction;

public partial class Portal_DefaultUser : System.Web.UI.Page
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
    string strUserId, strUserName, strDeptName, strSubDept, strFullName, strEmail, strDept, strImage, designationname, strDesination;
    int LocationID, LevelID, LogFlag, designatioid, intDesID;
    string pwdString, strLclSalt, strLclPwd1;
    string adminstatus, isAdmin, isRAAllowed, strType, accl;
    public static string strHeaderColor, strPageBgColor, strlogo, strFlashSrc1, strFlashSrc2, strstripcolor, UserType, LockUserType;
    public DateTime dtmValid, dtmLock;
    public int Alert, DayDiff, Attempt;
    public static int intCounter = 1;

    #region Page Initialisation
    protected void Page_Init(object Sender, EventArgs e)
    {
        Dictionary<string, bool> dicConfigAddUser = new Dictionary<string, bool>();
        dicConfigAddUser = (Dictionary<string, bool>)Session["ConfigAddUser"];

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
    }
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
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
        if (ConfigurationManager.AppSettings["ProxyLogin"] != null)
        {
            if (ConfigurationManager.AppSettings["ProxyLogin"].ToString() == "N")
            {
                //trUser.Visible = false;
            }
        }
        //CodeNumberTextBox.Attributes.Add("autocomplete", "off");
        this.btnSubmit.Attributes.Add("onClick", "return validation();");
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
        if (rbtSelf.Checked == true)
        {
            UserType = "1";
        }
        else
        {
            UserType = "2";
        }
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
        if ((this.txtpwd.Text == ""))
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
            if (rbtSelf.Checked == true)
            {
                objloginlist = objBAL.LoginUser("V", txtusr.Text, strPwd, 0, strClientIp);
            }
            else
            {
                objloginlist = objBAL.LoginUser("P", txtusr.Text, strPwd, 0, strClientIp);
            }


            //if (objloginlist.Count > 0)
            //{
            //    foreach (AdminApp.Model.Login objlist in objloginlist)
            //    {
            //        strLclPwd1 = objlist.Password;
            //        hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(strLclPwd1 + strLclSalt));
            //        pwdString = BitConverter.ToString(hashedBytes);
            //        pwdString = pwdString.Replace("-", null);
            //    }
            //}
        }

        else
        {
            this.hidmsg.Value = "Invalid User Name or Password";
        }
        if (pwdString == null)
        {

            //if (pwdString.ToUpper().Trim() == pwdString.ToUpper().Trim())
            //{
            if (objloginlist.Count > 0)
            {
                IList<AdminApp.Model.Login> objloginlist3 = objBAL.LoginUser("A", txtusr.Text, strPwd, Convert.ToInt32(UserType), "");
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
                        designatioid = objlist.DesigID;
                        designationname = objlist.DesigName;
                        strEmail = objlist.email;
                        accl = objlist.AccessLev;
                        isAdmin = objlist.AdminPrev;
                        isRAAllowed = objlist.Security;
                        strType = objlist.Type;
                        strImage = objlist.UserImage;
                        LogFlag = objlist.intAlert;
                        strDesination = objlist.DesigName;
                        intDesID = objlist.DesigID;
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
                        else if (objlist.SubAdminPrev == "True")
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

                //}


                if (LogFlag == 1)//----Password Expiry and Change Password Status Check-------
                {
                    IList<AdminApp.Model.Login> objloginlist1 = objBAL.LoginUser("E", txtusr.Text, strPwd, 0, "");
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
                        if (rbtSelf.Checked == true)
                        {
                            Response.Write("<script>alert('Your Password will be expire after " + DayDiff + " days !');document.location.href='WelCome/NewHome.aspx'</script>");
                        }
                        else
                        {
                            Response.Redirect("~/WelCome/NewHome.aspx");
                        }
                    }
                    else if (DayDiff == 0)
                    {
                        if (rbtSelf.Checked == true)
                        {
                            Response.Write("<script>alert('Your Password has been expired. Please contact administrator to reset password !');document.location.href='WelCome/PasswordExpiry.aspx'</script>");
                        }
                        else
                        {
                            Response.Redirect("~/WelCome/NewHome.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/WelCome/NewHome.aspx");
                    }

                }
                else
                {
                    Response.Redirect("Dashboard/Default.aspx");
                    //if (dtmLock <= DateTime.Now)
                    //{
                    //    if (rbtSelf.Checked == true)
                    //    {
                    //        Response.Redirect("ChangePassword.aspx");
                    //    }
                    //    else
                    //    {
                    //        Response.Redirect("~/WelCome/NewHome.aspx");
                    //    }

                    //}
                    //else
                    //{

                    //}

                }
            }
            else
            {
                if (intCounter == Attempt)
                {
                    IList<AdminApp.Model.Login> objloginlist2 = objBAL.LoginUser("A", txtusr.Text, strPwd, Convert.ToInt32(UserType), "");
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

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    #endregion

    #region User functions

    private void CreateSession()
    {
        Session["LevelID"] = LevelID;
        Session["locId"] = LocationID;
        Session["location"] = strLocation;
        Session["DeptId"] = strDept;
        Session["Department"] = strDeptName;
        Session["SubDept"] = strSubDept;
        Session["UserId"] = strUserId;
        Session["userName"] = strUserName;
        Session["fullName"] = strFullName;
        Session["adminstat"] = adminstatus;
        Session["menuCnt"] = 0;
        Session["strImage"] = strImage;

        Session["desination"] = strDesination;
        Session["desId"] = intDesID;
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
}