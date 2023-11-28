using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using KWAdminConsole.Messages;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Reflection;
namespace AdminApp_UI.Console.Manage_LogIn
{
    public partial class ProxyLogin : System.Web.UI.Page
    {
        #region "Variable Declaration"
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        UTF8Encoding encoder = new UTF8Encoding();
        public int RecCount;
        AdminAppService ObjAdminBal = new AdminAppService();
        AdminApp.Model.Login objlogin = new AdminApp.Model.Login();
        int intreturnval = 0;
        string OriginalUserEmail, OriginalMobNo, OriginalUserName, OriginalUser = "";
        string ProxyUserEmail, ProxyMobNo, ProxyUserName, ProxyUser = "";
        IList<AdminApp.Model.Login> objloginlist = new List<AdminApp.Model.Login>();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/SessionRedirect.aspx");
            }
            if (Request.QueryString["att"] != null)
            {
                string strAtt = CommonFunction.DecryptData(Request.QueryString["att"].ToString());
            }
            txtDTMFrom.Attributes.Add("readonly", "readonly");
            txtDTMTo.Attributes.Add("readonly", "readonly");
            txtPass.Attributes.Add("readonly", "readonly");
            //Purpose : To clear the browser cache
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
            AdminConsoleNavigation.strNewLink = ">>" + TabadminConfigMaster.ActiveTab.HeaderText;
            if (!IsPostBack)
            {
                FillGridview();
            }

        }
        protected void FillGridview()
        {
            try
            {
                IList<AdminApp.Model.Login> objdesiglst = new List<AdminApp.Model.Login>();
                objdesiglst = ObjAdminBal.ViewProxyLogIn("V");
                GridDesignation.DataSource = objdesiglst;
                GridDesignation.DataBind();
                DisplayPaging(GridDesignation, objdesiglst.Count);
                if (GridDesignation.Rows.Count == 0)
                {
                    btninDel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
            finally
            {
                //objdesignation = null;
            }
        }
        protected void GridDesignation_PageIndexChanging(Object sender, GridViewPageEventArgs e)
        {
            GridDesignation.PageIndex = e.NewPageIndex;
            FillGridview();
        }
        private void DisplayPaging(GridView grdToPaging, int totalRowCount)
        {
            if (grdToPaging.Rows.Count > 0)
            {
                this.lblpage.Visible = true;
                LnkbtnAllin.Visible = true;
                lblpage.Text = CommonFunction.ShowGridPaging(grdToPaging, grdToPaging.PageSize, grdToPaging.PageIndex, totalRowCount);
            }
            else
            {
                this.lblpage.Visible = false;
                LnkbtnAllin.Visible = false;
            }
        }
        #region Button Events
      


        
        #endregion
        protected void TabadminConfigMaster_ActiveTabChanged(object sender, EventArgs e)
        {
            TabCreateConfig.HeaderText = "CREATE";
            btnsave.Text = "Submit";
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            objlogin.ActionCode = "A";
            objlogin.MainUser = Convert.ToInt32(hdnOrg.Value);
            objlogin.ProxyUser = Convert.ToInt32(hdnProxy.Value);
            objlogin.Password = GenerateHash(txtPass.Text);
            objlogin.dtStartTime = Convert.ToDateTime(txtDTMFrom.Text);
            objlogin.dtEndTime = Convert.ToDateTime(txtDTMTo.Text);
            objlogin.intCreatedBy = Convert.ToInt32(Session["UserId"]);
            intreturnval = ObjAdminBal.ManageProxyLogIn(objlogin);
            if (intreturnval == 1)
            {
                IList<CommonFun> list1 = new List<CommonFun>();
                list1 = ObjAdminBal.GetRptUserName(Convert.ToInt32(hdnOrg.Value));
                IList<AdminApp.Model.Login> objloginlist1 = new List<AdminApp.Model.Login>();
                objloginlist1 = ObjAdminBal.LoginUser("V", list1[0].UserName, "", 0, "127.0.0.1");
                if (objloginlist1.Count > 0)
                {
                    foreach (AdminApp.Model.Login objlist in objloginlist1)
                    {
                        OriginalUserEmail = objlist.email;
                        OriginalMobNo = objlist.Type;
                        OriginalUserName = objlist.FullName;
                        OriginalUser = objlist.UserName;
                    }
                }
                IList<CommonFun> list2 = new List<CommonFun>();
                list2 = ObjAdminBal.GetRptUserName(Convert.ToInt32(hdnProxy.Value));
                IList<AdminApp.Model.Login> objloginlist2 = new List<AdminApp.Model.Login>();
                objloginlist2 = ObjAdminBal.LoginUser("V", list2[0].UserName, "", 0, "127.0.0.1");
                if (objloginlist2.Count > 0)
                {
                    foreach (AdminApp.Model.Login objlist in objloginlist2)
                    {
                        ProxyUserEmail = objlist.email;
                        ProxyMobNo = objlist.Type;
                        ProxyUserName = objlist.FullName;
                        ProxyUser = objlist.UserName;
                    }
                }
                SendSMSOrg(OriginalUserName, OriginalMobNo, txtPass.Text, hdnOrg.Value, ProxyUser, ProxyUserName);
                SendSMSProxy(ProxyUserName, ProxyMobNo, txtPass.Text, hdnProxy.Value, OriginalUser, OriginalUserName);
                sendMailToOrgUser(OriginalUserName, OriginalUserEmail, txtPass.Text, hdnOrg.Value, ProxyUser, ProxyUserName);
                sendMailToProxyUser(ProxyUserName, ProxyUserEmail, txtPass.Text, hdnProxy.Value, OriginalUser, OriginalUserName);
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Proxy User") + "');document.location.href='ProxyLogin.aspx';", true);
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            txtOrg.Text = "";
            txtProxy.Text = "";
            txtPass.Text = "";
            txtDTMFrom.Text = "";
            txtDTMTo.Text = "";
        }
        protected void btnGen_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            StringBuilder strbuilder = new StringBuilder();
            char ch = '\0';
            int intCounter = 0;
            for (intCounter = 0; intCounter <= 7; intCounter++)
            {
                ch = Convert.ToChar(random.Next(65, 90));
                //Generate a random character between A To Z
                strbuilder.Append(ch);
            }
            string strRandomPwd = Convert.ToString(strbuilder);
            txtPass.Text = strRandomPwd; 
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
        #region "Member Function"
        /// <summary>
        /// function Created By Biswaranjan on 27-sept-2010 to clear the data.
        /// </summary>

        /// <summary>
        /// Function Designed by Biswaranjan on 23-july-2010 to fill the data in editcase.
        /// </summary>
        /// <param name="intdesigId"></param>


        #endregion
        protected void LnkbtnAllin_Click1(object sender, EventArgs e)
        {
            if (LnkbtnAllin.Text == "All")
            {
                LnkbtnAllin.Text = "Paging";
                this.GridDesignation.PageIndex = 0;
                GridDesignation.AllowPaging = false;
                FillGridview();
                if (GridDesignation.Rows.Count > 0)
                {
                    this.lblpage.Text = "1-" + GridDesignation.Rows.Count.ToString() + " Of " + GridDesignation.Rows.Count.ToString();
                }
            }
            else
            {
                LnkbtnAllin.Text = "All";
                GridDesignation.AllowPaging = true;
                FillGridview();
            }
        }
        protected void GridDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                   
                    (e.Row.FindControl("lblSlno") as Label).Text = (GridDesignation.PageIndex * GridDesignation.PageSize + e.Row.RowIndex + 1).ToString();
                }
            }
            catch (Exception e1)
            {
                throw new Exception(e1.Message, e1);
            }

        }
        protected void btninDel_Click(object sender, EventArgs e)
        {
            int intdatakey = 0;
            try
            {
                for (int i = 0; i <= GridDesignation.Rows.Count - 1; i++)
                {
                    CheckBox objchk = new CheckBox();
                    objchk = (CheckBox)GridDesignation.Rows[i].FindControl("cbItem");
                    if (objchk.Checked == true)
                    {
                        intdatakey = Convert.ToInt32(GridDesignation.DataKeys[i].Value);
                        objlogin.ActionCode = "D";
                        objlogin.GetID = intdatakey;
                        objlogin.dtStartTime = DateTime.Now;
                        objlogin.dtEndTime = DateTime.Now;
                        intreturnval = Convert.ToInt32(ObjAdminBal.ManageProxyLogIn(objlogin));
                    }

                }
                string strOutmsg = StaticValues.message(intreturnval, "Proxy User");
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');", true);
                FillGridview();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                objlogin = null;
            }
        }
        public void SendSMSOrg(string Name, string Mobile, string Password, string userid, string username,string proxy)
        {
            try
            {
              
                string strReceiverTelNo = string.Empty;
                //Receiver TelNo
                strReceiverTelNo = Mobile;
                string STRMESSAGEBODY = "";
                STRMESSAGEBODY = "Dear " + Name + " , "+proxy+" has been choosen for proxy log in  with  password-" + Password + " on behalf of your account.";
                string strSenderTelNo = string.Empty;
                //Sender TelNo
                Type t = Type.GetType(ConfigurationManager.AppSettings["AssemblyClass"].ToString());
                if (t != null)
                {
                    object classInstance = Activator.CreateInstance(t);
                    MethodInfo methods = t.GetMethod(ConfigurationManager.AppSettings["AssemblyMethod"].ToString());
                    object retVal = methods.Invoke(classInstance, new object[] { strReceiverTelNo, STRMESSAGEBODY });
                }
            }
            catch { }

        }
        public void SendSMSProxy(string Name, string Mobile, string Password, string userid,string username,string org)
        {
            try
            {
                string strReceiverTelNo = string.Empty;
                //Receiver TelNo
                strReceiverTelNo = Mobile;
                string STRMESSAGEBODY = "";
                STRMESSAGEBODY = "Dear "+Name+" you have set for proxy log in for "+org+" with username- "+username+" and password-"+Password+".";
                string strSenderTelNo = string.Empty;
                //Sender TelNo
                Type t = Type.GetType(ConfigurationManager.AppSettings["AssemblyClass"].ToString());
                if (t != null)
                {
                    object classInstance = Activator.CreateInstance(t);
                    MethodInfo methods = t.GetMethod(ConfigurationManager.AppSettings["AssemblyMethod"].ToString());
                    object retVal = methods.Invoke(classInstance, new object[] { strReceiverTelNo, STRMESSAGEBODY });
                }
            }
            catch { }

        }
        public void sendMailToOrgUser(string name, string mail, string Password, string userid, string username, string proxy)
        {
            try
            {
                string strMessage = "";
                string strTo = mail;
                string strFrom = "admin@kwantify.com";
                string strCc = string.Empty;
                string strSubject = "";
                strSubject = "Proxy LogIn";
                string sentDate = DateTime.Now.ToString();
                strMessage = "Dear " + name + ",<br><br>";
                strMessage = strMessage + "" + proxy + " has been choosen for proxy log in  with  password-" + Password + " on behalf of your account.";
                strMessage = strMessage + "Thank you,";
                strMessage = strMessage + "Administrator. ";
                strMessage = strMessage + "Note:: This is a system generated mail.Please do not send any reply.";
                strTo = mail;
                string PortIP = "";
                string CC = "";
                if (!string.IsNullOrEmpty(strTo) & !string.IsNullOrEmpty(strFrom))
                {

                    try
                    {
                        using (
                        MailMessage mailMessage = new MailMessage())
                        {
                            SmtpClient smtp = new SmtpClient();
                            mailMessage.IsBodyHtml = true;
                            mailMessage.Priority = MailPriority.High;
                            MailAddress @from = new MailAddress(strFrom);
                            mailMessage.From = @from;
                            mailMessage.To.Add(mail);
                            if (!string.IsNullOrEmpty(CC))
                            {
                                mailMessage.CC.Add(CC);
                            }
                            mailMessage.Body = strMessage.ToString();
                            mailMessage.Subject = strSubject;
                            smtp.Host = "127.0.0.1";
                            smtp.UseDefaultCredentials = true;
                            if (string.IsNullOrEmpty(PortIP))
                            {
                                smtp.Send(mailMessage);
                            }
                            else
                            {
                                smtp.Send(mailMessage);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                ObjAdminBal.ChangePasswordDtl("", "", userid);
            }


        }
        public void sendMailToProxyUser(string name, string mail, string Password, string userid, string username, string proxy)
        {
            try
            {
                string strMessage = "";
                string strTo = mail;
                string strFrom = "admin@kwantify.com";
                string strCc = string.Empty;
                string strSubject = "";
                strSubject = "Proxy LogIn";
                string sentDate = DateTime.Now.ToString();
                strMessage = "Dear " + name + ",<br><br>";
                strMessage = strMessage + "you have set for proxy log in for " + proxy + " with username- " + username + " and password-" + Password + ".";
                strMessage = strMessage + "Thank you,";
                strMessage = strMessage + "Administrator. ";
                strMessage = strMessage + "Note:: This is a system generated mail.Please do not send any reply.";
                strTo = mail;
                string PortIP = "";
                string CC = "";
                if (!string.IsNullOrEmpty(strTo) & !string.IsNullOrEmpty(strFrom))
                {

                    try
                    {
                        using (
                        MailMessage mailMessage = new MailMessage())
                        {
                            SmtpClient smtp = new SmtpClient();
                            mailMessage.IsBodyHtml = true;
                            mailMessage.Priority = MailPriority.High;
                            MailAddress @from = new MailAddress(strFrom);
                            mailMessage.From = @from;
                            mailMessage.To.Add(mail);
                            if (!string.IsNullOrEmpty(CC))
                            {
                                mailMessage.CC.Add(CC);
                            }
                            mailMessage.Body = strMessage.ToString();
                            mailMessage.Subject = strSubject;
                            smtp.Host = "127.0.0.1";
                            smtp.UseDefaultCredentials = true;
                            if (string.IsNullOrEmpty(PortIP))
                            {
                                smtp.Send(mailMessage);
                            }
                            else
                            {
                                smtp.Send(mailMessage);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                ObjAdminBal.ChangePasswordDtl("", "", userid);
            }


        }
        [System.Web.Services.WebMethod]

        public static IList<User> BindUserList(string strUserName)
        {
            AdminAppService ObjAdminBal = new AdminAppService();
            User objuser = new User();
            IList<User> List = new List<User>();
            objuser.ActionCode = "F";
            objuser.FullName = strUserName;
            List = ObjAdminBal.GetAllUsers(objuser);
            return List;
        }
    }
}