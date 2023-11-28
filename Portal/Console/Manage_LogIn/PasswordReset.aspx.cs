using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using KWAdminConsole.Messages;
using Manage_Usercontrol_Property;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using System.ComponentModel;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Reflection;
public partial class AdminApp_UI_Console_Manage_Password : System.Web.UI.Page
    {
        #region "Variables"
        /// <summary>
        /// Declaring Variables
        /// </summary>
        User objuser = new User();
        AdminAppService ObjAdminBal = new AdminAppService();
        IList<User> lstUser;
        public int RecCount;
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        Byte[] hashedBytes;
        UTF8Encoding encoder = new UTF8Encoding();
        IList<AdminApp.Model.Login> objloginlist = new List<AdminApp.Model.Login>();
        string pwdString, strLclSalt, strLclPwd1;
        string strPwd="";
        string AdminEmail = "";
        string UserEmail, MobNo,UserName = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            if (Request.QueryString["lnkNm"] != null)
            {
                //Code added by Dilip Kumar Tripathy on dated 03-May-2013 to change the navigate url as per creent tab name
                AdminConsoleNavigation.strNewLink = ">>" + Request.QueryString["lnkNm"].ToString();
            }
            if (!Page.IsPostBack)
            {
                GroupMasterProperties.hidlstid = "";
                GroupMasterProperties.hidbtnid = "";
                GroupMasterProperties.hidnval = "";
                CommonProperties.PageUrl = "PasswordReset.aspx";
                CommonProperties.PId = 0;
                CommonProperties.Type = "1";
                AssignAdminProperties.hidadmin = "";
                SetpermissionProperties.hidLevel = null;
                CommonProperties.UserControlId = null;
                CommonProperties.UserControlId2 = null;
                CommonProperties.Action = "A";
            }
            (HierarchyForAllLocation2.FindControl("sdrplayers0") as DropDownList).Focus();
            Page.Form.DefaultButton = btnShow.ClientID;
        }

        #region User Defined Methods

        private void FillGrid(string strActionCode, GridView grdToFill)
        {
            grdToFill.Visible = true;
            objuser.ActionCode = strActionCode;
            objuser.FullName = "";
            objuser.DepartmentID = DepartmentOfUser("V");
            objuser.FullName = txtSearch.Text.Trim();
            lstUser = ObjAdminBal.GetAllUsers(objuser);
            grdToFill.DataSource = null;
            grdToFill.DataBind();
            grdToFill.DataSource = lstUser;
            grdToFill.DataBind();
            RecCount = lstUser.Count();
            if (RecCount > 0)
            {
                btnReset.Visible = true;
                lblAlert.Visible = true;
                chkEmail.Visible = true;
                chkSMS.Visible = true;
            }
            else 
            {
                btnReset.Visible = false;
                lblAlert.Visible = false;
                chkEmail.Visible = false;
                chkSMS.Visible = false;
            }

            DisplayPaging(grdToFill, RecCount);

            if (strActionCode == "F")
            {
                SetpermissionProperties.hidLevels = ((HiddenField)HierarchyForAllLocation2.FindControl("shidLevels")).ClientID;
            }
        }

        private string DepartmentOfUser(string strUCtrl)
        {

            string strDept = "";
            int intLayer = 0;

            if (((HiddenField)HierarchyForAllLocation2.FindControl("shidLevels")).Value != "")
            {
                intLayer = Convert.ToInt32(((HiddenField)HierarchyForAllLocation2.FindControl("shidLevels")).Value);
            }
            else
            {
                intLayer = 1;
            }


            HiddenField hdnDeptOfUser = null;
            if (strUCtrl == "A")
            {
                for (int i = 0; i < intLayer; i++)
                {

                    hdnDeptOfUser = (HiddenField)HierarchyForAllLocation2.FindControl("shidIDs" + Convert.ToString(i));

                    if (hdnDeptOfUser.Value != "" && hdnDeptOfUser.Value != "0")
                    {
                        strDept = hdnDeptOfUser.Value;
                    }
                }
            }
            else
            {
                for (int i = 0; i < intLayer; i++)
                {

                    hdnDeptOfUser = (HiddenField)HierarchyForAllLocation2.FindControl("shidIDs" + Convert.ToString(i));
                    string s = (HierarchyForAllLocation2.FindControl("sdrplayers" + Convert.ToString(i)) as DropDownList).SelectedValue;
                    if (i == 0)
                    {
                        ViewState["level1"] = hdnDeptOfUser.Value;
                    }
                    if (i == 1)
                    {
                        IList<PopHierarchy> objPop = ObjAdminBal.FillLevelFromParent(Convert.ToInt32(int.Parse(ViewState["level1"].ToString())));
                        DataTable dtLevel1 = ConvertToDataTable(objPop);
                        string[] levelArray = new string[dtLevel1.Rows.Count];
                        for (int k = 0; k < dtLevel1.Rows.Count; k++)
                        {
                            levelArray[k] = dtLevel1.Rows[k][2].ToString();
                        }
                        if (!levelArray.Contains(hdnDeptOfUser.Value))
                        {
                            hdnDeptOfUser.Value = "";
                        }
                    }


                    if (hdnDeptOfUser.Value != "" && hdnDeptOfUser.Value != "0")
                    {
                        strDept = hdnDeptOfUser.Value;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return strDept;
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        private void DisplayPaging(GridView grdUsers, int totalRowCount)
        {
            if (grdUsers.Rows.Count > 0)
            {
                this.lblpage.Visible = true;
                LnkbtnAllin.Visible = true;
                lblpage.Text = CommonFunction.ShowGridPaging(grdUsers, grdUsers.PageSize, grdUsers.PageIndex, totalRowCount);
            }
            else
            {
                this.lblpage.Visible = false;
                LnkbtnAllin.Visible = false;
            }
        }

        #endregion

        protected void btnShow_Click(object sender, EventArgs e)
        {
            string AdminPerv="";
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

                objloginlist = ObjAdminBal.LoginUser("V", txtusr.Text, strPwd, 0, strClientIp);

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
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Invalid User Name or Password !');", true);
            }
            if (pwdString != null)
            {

                if (this.txtpwd.Text.ToUpper().Trim() == pwdString.ToUpper().Trim())
                {
                    if (objloginlist.Count > 0)
                    {
                        foreach (AdminApp.Model.Login objlist in objloginlist)
                        {
                            //-----Admin Can View----
                            AdminPerv = objlist.AdminPrev;
                            if (AdminPerv == "True")
                            {
                                AdminEmail = objlist.email;
                                DropDownList ddlLoc = HierarchyForAllLocation2.FindControl("sdrplayers0") as DropDownList;
                                lblLocatioName.Text = ddlLoc.SelectedItem.Text;
                                lblLoc.Visible = true;
                                lblLocatioName.Visible = true;
                                LnkbtnAllin.Text = "All";
                                FillGrid("F", grdUsersInfo);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Invalid Credential For Administrator!');", true);
                            }
                         
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Invalid User Name or Password !');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Invalid User Name or Password !');", true);
            }
           
        }

        protected void LnkbtnAllin_Click(object sender, EventArgs e)
        {
            if (LnkbtnAllin.Text == "All")
            {
                LnkbtnAllin.Text = "Paging";
                this.grdUsersInfo.PageIndex = 0;
                grdUsersInfo.AllowPaging = false;
                FillGrid("F", grdUsersInfo);
                if (grdUsersInfo.Rows.Count > 0)
                {
                    this.lblpage.Text = "1-" + grdUsersInfo.Rows.Count.ToString() + " Of " + grdUsersInfo.Rows.Count.ToString();
                }

            }
            else
            {
                LnkbtnAllin.Text = "All";
                grdUsersInfo.AllowPaging = true;
                FillGrid("F", grdUsersInfo);
            }
        }
        protected void grdUsersInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdUsersInfo.PageIndex = e.NewPageIndex;
            FillGrid("F", grdUsersInfo);
        }

        protected void grdUsersInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Text = (grdUsersInfo.PageIndex * grdUsersInfo.PageSize + e.Row.RowIndex + 1).ToString();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
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
            string strEncrptPwd = GenerateHash(strRandomPwd);
            int rcount = grdUsersInfo.Rows.Count;
            for (int i = 0; i < rcount; i++)
            {
                if (((RadioButton)grdUsersInfo.Rows[i].Cells[0].FindControl("rbtChk")).Checked == true)
                {
                    string userid = grdUsersInfo.DataKeys[i].Values[0].ToString();
                    ObjAdminBal.ChangePasswordDtl("", strEncrptPwd, userid);
                    objloginlist = ObjAdminBal.LoginUser("V", grdUsersInfo.DataKeys[i].Values[1].ToString(), "", 0, "127.0.0.1");
                    if (objloginlist.Count > 0)
                    {
                        foreach (AdminApp.Model.Login objlist in objloginlist)
                        {
                            UserEmail = objlist.email;
                            MobNo = objlist.Type;
                            UserName = objlist.FullName;
                        }
                    }
                    if (chkEmail.Checked == true && chkSMS.Checked == true)
                    {
                        sendMailToUser(UserEmail, UserName, strRandomPwd, userid);
                        SendSMS(UserName, MobNo, strRandomPwd, userid);
                    }
                    else if (chkEmail.Checked == true)
                    {
                        sendMailToUser(UserEmail, UserName, strRandomPwd, userid);
                    }
                    else if (chkSMS.Checked == true)
                    {
                        SendSMS(UserName, MobNo, strRandomPwd, userid);
                    }
                    else
                    {
                        ObjAdminBal.ChangePasswordDtl("", "", userid);
                    }
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Password Reset Successfully ! Temp Password Is:-" + strRandomPwd + " !');document.location.href='PasswordReset.aspx'", true);
                }
            }
           
           
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
        public void SendSMS(string Name,string Mobile,string Password,string userid)
        {
            try
            {
                string strReceiverTelNo = string.Empty;
                //Receiver TelNo
                strReceiverTelNo = Mobile;
                string STRMESSAGEBODY = "";
                STRMESSAGEBODY = "Your password has been reset by admin.Your Temporary Password Is:"+Password+".Change your password, as soon as you receive it.";
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
            catch { ObjAdminBal.ChangePasswordDtl("", "", userid); }
            
        }
        public void sendMailToUser(string strEmailId, string strFullName, string strPassword, string userid)
        {
            try
            {
                string strMessage = "";
                string strTo = strEmailId;
                string strFrom = AdminEmail;

                string strCc = string.Empty;
                string strSubject = "";
                strSubject = "Password Reminder";
                string sentDate = DateTime.Now.ToString();
                strMessage = "Dear " + strFullName + ",<br><br>";
                strMessage = strMessage + "Your password has been reset by admin. You can kindly log in to your mail ID with ";
                strMessage = strMessage + "Password\t:: " + strPassword;
                strMessage = strMessage + " However, it is advisable to change your password, as soon as you receive it, to enable security at your level.";
                strMessage = strMessage + "Thank you,";
                strMessage = strMessage + "Administrator. ";
                strMessage = strMessage + "Note:: This is a system generated mail.Please do not send any reply.";
                strTo = strEmailId;
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
                            mailMessage.To.Add(strEmailId);
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
    }