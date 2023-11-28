/********************************************************************************************************************
' File Name             :   AddUser.aspx.cs
' Description           :   To Add/edit/delete/activate/inactivate Admin User Details.
' Created by            :   Subhasis Dash
' Created On            :   09-jun-2010
' Modification History  :   <CR no.>    <Date>             <Modified by>       <Modification Summary>'                                                          
'                              1.      08-Jul-2010         Pratik Sarangi      Coding for Add button
'                              2.      14-Jul-2010         Pratik Sarangi      Modifications due to change in database
'                              3.      15-Jul-2010         Pratik Sarangi      Coding for View/Delete
'                              4.      20-Jul-2010         Pratik Sarangi      Coding for edit
'                              5.      12-Aug-2010         Subrat Acharya      Writing code for generation of XML file
'                              6.      29-Sep-2010         Pratik Sarangi      Writing code for generation of XML file
                               7       04-Oct-2010         Biswarnajan         Writiing code to set the default focus button of the page 
'                              8       16-Nov-2010         Biswaranjan         For Bugfixing 
'                              9       25-Jun-2012         Dilip Tripathy      To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                        
' Function Name         :   
' Procedures Used       :    
' User Defined Namespace:   KWAdminConsole.Manage_User
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Web.UI.HtmlControls;
using Admin.CommonFunction;
// CSMPDK_3_0;
using KWAdminConsole.Messages;
using Manage_Usercontrol_Property;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;

public partial class Admin_Manage_User_AdminAddUser : System.Web.UI.Page
{
    #region "Variables"
    /// <summary>
    /// Declaring Variables
    /// </summary>
    User objuser = new User();
    AdminAppService ObjAdminBal = new AdminAppService();
    IList<User> lstUser;
    Level objLevelDetails = new Level();
    DataTable dtRetLevldept = new DataTable();
    //CommonDLL objComnDll = new CommonDLL();
    Byte[] hashedBytes;
    UTF8Encoding encoder = new UTF8Encoding();
    string pwdString = null;
    public string strNodePID, strUid;
    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
    HiddenField hidprevTabindex = new HiddenField();
    public string UID;
    public string strUserAttribute;
    #endregion

    #region "Page_Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.DefaultButton = btnAdd.UniqueID;
        
        //txtfullname.Focus();
        Page.Form.DefaultFocus = txtfullname.UniqueID;
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }
        if (Request.QueryString["att"] != null)
        {
            string strAtt = CommonFunction.DecryptData(Request.QueryString["att"].ToString());
        }
        //Code Added By : Dilip Kumar Tripathy on dated 10-May-2013
        //Purpose : To clear the browser cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");

        Session["hidcid"] = 0;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        CommonProperties.UserControlId = ListRUser.ClientID;
        CommonProperties.UserControlId2 = null;
        CommonProperties.UserControlId3 = ddlDesg.ClientID;//code added by dilip on dated 07-Apr-2012
        CommonProperties.UserControlId4 = ddlGrade.ClientID;//code added by dilip on dated 13-Apr-2012
        GroupMasterProperties.hidlstid = "";
        GroupMasterProperties.hidbtnid = "";
        GroupMasterProperties.hidnval = "";
        CommonProperties.Type = "1";
        AssignAdminProperties.hidadmin = "";
        CommonProperties.HierachyId = 0;
        CommonProperties.LId = 1;
        CommonProperties.PageUrl = "AdminAddUser.aspx";
        txtDOB.Attributes.Add("readonly", "readonly");
        txtDOJ.Attributes.Add("readonly", "readonly");
        txtPCD.Attributes.Add("readonly", "readonly");
        CommonProperties.PId = 0;
        strUserAttribute = Session["adminstat"].ToString();
        DropDownList ddloc = new DropDownList();

        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabContainer1.ActiveTab.HeaderText;

        if (!Page.IsPostBack)
        {
            AssignCheckBoxes();
            ConfigPageControls();
            ViewState["subAdminStat"] = 0;
            FillOffice(ddlOfficeType);
            BindDist();
            //PopUpDropDown1()  objComnDll.PopupDropDown("ConnectionString", "select intLocationId,vchLocation from M_ADM_Location where bitStatus=1 ORDER BY vchLocation", ddlLocation);
            lstUser= ObjAdminBal.PopUpDropDown1();
            ddlLocation.DataSource = lstUser;
            ddlLocation.DataTextField = "Location";
            ddlLocation.DataValueField="intLocation";
            ddlLocation.DataBind();
            if (Request.QueryString["UID"] != null)
            {
                hidUID.Value = CommonFunction.DecryptData(Request.QueryString["UID"].ToString());
                if (PreviousPage != null && PreviousPage.IsCrossPagePostBack)
                {
                    hidprevTabindex = (HiddenField)(PreviousPage.FindControl("hidTabindex"));
                }
                CommonProperties.Action = "U";
                CommonProperties.Type = "U";
                CommonProperties.UserControlId2 = null;
                CommonProperties.UserControlId3 = ddlDesg.ClientID;
                CommonProperties.UserControlId4 = ddlGrade.ClientID;
                ShowData();
                hdnUser.Value = CommonFunction.DecryptData(Request.QueryString["UID"].ToString());
                trPass.Visible = false;
                trCPass.Visible = false;
                hdnAction.Value = "U";
                btnReset.Text = "Cancel";
                btnUpdate.Focus();
            }
            else
            {
                CommonProperties.Action = "A";
                trPass.Visible = true;
                trCPass.Visible = true;
                hdnAction.Value = "A";
                btnReset.Text = "Reset";
                btnAdd.Focus();

            }
        }
        if (Request.QueryString["UID"] == null)
        {
            this.btnAdd.Visible = true;
            this.btnUpdate.Visible = false;
            this.btnReset.Visible = true;
        }
        else
        {
            this.btnAdd.Visible = false;
            this.btnUpdate.Visible = true;
            this.btnReset.Visible = true;
        }
        Session["pid"] = 0;
        //Folowing codes are added by Dilip Tripathy on dated 10-May-2013
        // this.txtPaddress.Attributes.Add("onkeyup", "return isSpecialChar1st('" + txtPaddress.ClientID + "');TextCounter('" + txtPaddress.ClientID + "','" + lblMaxcounter.ClientID + "',500);");
        
       // (getUsers1.FindControl("Label1") as Label).Text = "";
    }
    #endregion

    #region "Button Events"
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddUpdateUser("A", 0);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        AddUpdateUser("U", Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["UID"].ToString())));
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.Text == "Reset")
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "document.location.href='AdminAddUser.aspx';", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "document.location.href='AdminUserProfile.aspx?arg=" + hdnLevelid.Value + "&rindex=" + Request.QueryString["Pindex"].ToString() + "';", true);
        }
    }
    #endregion

    #region "Member Functions"
    /// <summary>
    /// To get Department Id Of User from dropdownlist
    /// </summary>
    /// <returns></returns>
    private string DepartmentOfUser()
    {
        string strDept = "";
        int intLayer = Convert.ToInt32(((HiddenField)HierarchyForAllLocation1.FindControl("shidLevels")).Value);
        HiddenField hdnDeptOfUser = null;
        int counts;
        counts = intLayer;
        for (int i = counts; i > 0; i--)
        {
            HiddenField hdnLevel1 = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs" + Convert.ToString(i));
            if (hdnLevel1.Value == "")
            {
                counts = counts - 1;
                HiddenField hdnLevel2 = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs" + Convert.ToString(intLayer));
                hdnLevel2.Value = null;
            }
        }
        for (int i = 0; i <= counts; i++)
        {
            hdnDeptOfUser = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs" + Convert.ToString(i));
            if (hdnDeptOfUser.Value != "" && hdnDeptOfUser.Value != "0")
            {
                strDept = hdnDeptOfUser.Value;
            }
        }
        return strDept;
    }
    /// <summary>
    /// To get Department Id Of User from dropdownlist
    /// </summary>
    /// <returns></returns>
    private string DepartmentOfRptAuth()
    {
        string strDept = "";
        if (((HiddenField)getUsers1.FindControl("shidLevels")).Value != "")
        {
            int intLayer = Convert.ToInt32(((HiddenField)getUsers1.FindControl("shidLevels")).Value);
            for (int i = 0; i < intLayer; i++)
            {
                HiddenField hdnDeptOfUser = (HiddenField)getUsers1.FindControl("shidIDs" + Convert.ToString(i));
                if (hdnDeptOfUser.Value != "" && hdnDeptOfUser.Value != "0")
                {
                    strDept = hdnDeptOfUser.Value;
                }
            }
        }
        return strDept;
    }
    private int LevelId()
    {
        int intLayer, intLvlDtlId = 0, intLevelId = 0, count;
        intLayer = Convert.ToInt32(((HiddenField)HierarchyForAllLocation1.FindControl("shidLevels")).Value);
        count = intLayer;
        for (int i = count; i > 0; i--)
        {
            HiddenField hdnLevel1 = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs" + Convert.ToString(i));
            if (hdnLevel1.Value == "")
            {
                count = count - 1;
                HiddenField hdnLevel2 = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs" + Convert.ToString(intLayer));
                hdnLevel2.Value = null;
            }
        }
        HiddenField hdnLevel = null;
        for (int i = 0; i <= count; i++)
        {
            hdnLevel = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs" + Convert.ToString(i));
            if (hdnLevel.Value != "" && hdnLevel.Value != "0")
            {
                intLvlDtlId = Convert.ToInt32(hdnLevel.Value);
            }
        }
      
        // PopUpDropDown2() string StrQryLevel = "select intLevelDetailId from M_ADM_LevelDetails where intLevelDetailId=" + intLvlDtlId + "";
        //IDataReader objDrLevel = (IDataReader)objComnDll.ExeReader("ConnectionString", StrQryLevel, 0);
        intLevelId = ObjAdminBal.GetLevelDTlID(intLvlDtlId.ToString());

        //while (objDrLevel.Read())
        //{
        //    intLevelId = Convert.ToInt32(objDrLevel["intLevelDetailId"]);
        //}
        //objDrLevel.Close();
        //string StrQryLevel = "select intLevelDetailId from M_ADM_LevelDetails where intLevelDetailId=" + intLvlDtlId + "";
        //IDataReader objDrLevel = (IDataReader)objComnDll.ExeReader("ConnectionString", StrQryLevel, 0);
        //while (objDrLevel.Read())
        //{
        //    intLevelId = Convert.ToInt32(objDrLevel["intLevelDetailId"]);
        //}
        //objDrLevel.Close();
        return intLevelId;
        
    }
    /// <summary>
    /// To Add/Update User
    /// </summary>
    /// <param name="strActionCode"></param>
    /// <param name="intUserId"></param>
    /// <returns></returns>
    private void AddUpdateUser(string strActionCode, int intUserId)
    {
        int intOutPut, intLvlDtlId;
        objuser.ActionCode = strActionCode;
        objuser.GetID = intUserId;
        intLvlDtlId = Convert.ToInt32(((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).SelectedValue);
        objuser.HierarchyId = getHierarchyId(intLvlDtlId);
        int intLvlId = LevelId();
        if (intLvlId == 0)
        {
            //dtRetLevldept = objComnDll.GetDataTable("ConnectionString", "select intLevelDetailId,intLevelDetailId from M_POR_User where intUserId=" + intUserId + "");
            objuser.LevelId = Convert.ToInt32(hdnLevelid.Value);
        }
        else
        {
            objuser.LevelId = intLvlId;
        }
       
        objuser.UserName = txtUserName.Text.Trim();
        hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(txtPassword.Text));
        pwdString = BitConverter.ToString(hashedBytes);
        pwdString = pwdString.Replace("-", null);
        objuser.UserPasswaord = pwdString;
        objuser.FullName = txtfullname.Text.Trim();
        string strDeptId = DepartmentOfUser();
        if (strDeptId == "")
        {
            objuser.DepartmentID = hdnLevelid.Value;
        }
        else
        {
            objuser.DepartmentID = strDeptId;
        }
        objuser.intLocation = int.Parse(ddlLocation.SelectedValue);
        objuser.intdistrict = int.Parse(ddldistrict.SelectedValue);
        if (hidDesigId.Value.ToString() != "")
        {
            objuser.intDesignationID = Convert.ToInt32(hidDesigId.Value.ToString());
        }
        else
        {
            objuser.intDesignationID = 0;
        }
        if (hidGradeId.Value.ToString() != "")
        {
            objuser.GradeId = Convert.ToInt32(hidGradeId.Value.ToString());
        }
        else
        {
            objuser.GradeId = 0;
        }
        objuser.OfficeID = ddlOfficeType.SelectedValue.ToString();

        if (txtDOJ.Text != "")
        {
            objuser.DateOfJoing = Convert.ToDateTime(txtDOJ.Text);//===============
        }
        else
        {
            objuser.DateOfJoing = Convert.ToDateTime("1-Jan-1900");//===============
        }
        if (txtDOB.Text != "")
        {
            objuser.DateOfBirth = Convert.ToDateTime(txtDOB.Text);//===============
        }
        else
        {
            objuser.DateOfBirth = Convert.ToDateTime("1-Jan-1900");//===============
        }
        objuser.security = "";
        objuser.AdditionalDepartment = "";
        objuser.PrsentAddress = txtPaddress.Text.Trim();
        objuser.OfficeTelephone = txtCountryCode.Text.Trim() + "-" + txtStdCode.Text.Trim() + "-" + txtOfficeTel.Text.Trim();
        objuser.Mobile = txtMob.Text.Trim();
        objuser.email = txtEmail.Text.Trim();
        #region  for ReportDepartment of OfficePortal
        if (strActionCode == "A")
        {
            if (hidPriority1.Value != "")
            {
                objuser.ReportDepartment = DepartmentOfRptAuth();
            }
            else
            {
                objuser.ReportDepartment = "";
            }
        }
        else
        {
            string rptid = "";
            if (hidPriority1.Value != "")
            {
                rptid = DepartmentOfRptAuth();
            }
            if (rptid == "")
            {
                if (hidPriority1.Value != "")
                {
                    objuser.ReportDepartment = Convert.ToString(ViewState["RptDept"]);
                }
                else
                {
                    objuser.ReportDepartment = "";
                }
            }
            else
            {
                objuser.ReportDepartment = rptid;
            }
        }
        #endregion
        if (hidPriority1.Value != "")
        {
            objuser.ReportingUserId = Convert.ToInt32(hidPriority1.Value);
        }
        if (hidPriority2.Value != "")
        {
            objuser.TempReportingUserId = Convert.ToInt32(hidPriority2.Value);
        }
        objuser.status = "Yes";
        if (chkSupAdminPrevil.Checked == true)
        {
            objuser.AdminPrevilliage = "1";
        }
        else
        {
            objuser.AdminPrevilliage = "0";
        }
        if (ViewState["subAdminStat"].ToString() == "0")
        {
            objuser.SubAdminPrevillage = "0";
        }
        else
        {
            objuser.SubAdminPrevillage = ViewState["subAdminStat"].ToString();
        }

        if (chkAttendance.Checked == true)
        {
            objuser.Attendance = "1";
        }
        else
        {
            objuser.Attendance = "0";
        }
        objuser.Gender = rbtGender.SelectedValue;
        //if (hidPriority3.Value != "")
        //{
        //    objuser.OptionalReportingUserId = Convert.ToInt32(hidPriority3.Value);
        //}
        //objuser.Type = ddlType.SelectedValue;
        objuser.DomainUserName = txtDomainName.Text.Trim();
        if (txtPCD.Text != "")
        {
            objuser.ProbationCompleteDate = Convert.ToDateTime(txtPCD.Text);
        }
        else
        {
            objuser.ProbationCompleteDate = Convert.ToDateTime("1-Jan-1900");
        }
        objuser.LeavingDate = Convert.ToDateTime("1-Jan-1900");
        if (chkPayroll.Checked == true)
        {
            objuser.Payroll = "1";
        }
        else
        {
            objuser.Payroll = "0";
        }
        if (objuser.Payroll == "1")
        {
            objuser.EPF = Convert.ToInt32(rbtnLstEPF.SelectedValue);
        }
        else
        {
            objuser.EPF = 0;
        }
        objuser.CreatedBy = 1;
        objuser.FatherNameEnglish = txtFatherNameInEnglish.Text.Trim();
        objuser.FatherNameAmharic = txtFatherNameInAmharic.Text.Trim();
        objuser.GrandFatherNameEnglish = txtGrandFatherNameInEnglish.Text.Trim();
        objuser.GrandFatherNameAmharic = txtGrandFatherNameInAmharic.Text.Trim();

        if (FileUploadImage.HasFile)//For File UpLoad
        {
            string extension = System.IO.Path.GetExtension(FileUploadImage.PostedFile.FileName);
            if (extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".PNG" || extension == ".JPG" || extension == ".JPEG")
            {


                if (FileUploadImage.PostedFile.ContentLength > 1024000)
                {
                    string strmsg11 = "<script>alert('The file has to be less than 1MB!')</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg11, false);
                    return;
                }

                string filename = Path.GetFileName(FileUploadImage.PostedFile.FileName);
                string date = DateTime.Now.Ticks.ToString();
                FileUploadImage.PostedFile.SaveAs(MapPath("User_Image/" + date + filename));
                objuser.UserImage = date + filename;
                GenerateThumbnails(Server.MapPath("User_Image/") + date + filename, Server.MapPath("User_Image/") + date + filename);
            }
            else
            {
                string strmsg11 = "<script>alert('Only jpg/png/bmp/gif file accepted!')</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg11, false);
                return;
            }
        }
        else
        {
            objuser.UserImage = hidUserImage.Value;
        }
        intOutPut = ObjAdminBal.CreateUser(objuser);



        string strOutmsg = "";
        if (intOutPut == 15 || intOutPut == 16 || intOutPut == 17)
        {
            strOutmsg = StaticValues.message(intOutPut, "");
        }
        else
        {
            strOutmsg = StaticValues.message(intOutPut, "User");
        }

        int intUser;
        intUser = Convert.ToInt32(Session["UserId"]);
        if (strActionCode.ToLower() == "u")
        {
            ViewState["levelId"] = intLvlDtlId.ToString();
            if (intOutPut == 15 || intOutPut == 16 || intOutPut == 17)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');document.location.href='AdminUserProfile.aspx?arg=" + intLvlDtlId.ToString() + "&rindex=" + Request.QueryString["Pindex"].ToString() + "';", true);
            }
        }
        else
        {
            if (intOutPut == 1)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');document.location.href='AdminAddUser.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');", true);

                DropDownList ddlLoc = HierarchyForAllLocation1.FindControl("sdrplayers0") as DropDownList;
                string strHierId = ddlLoc.SelectedValue;
                BindDesigGrid(strHierId);
                ddlDesg.SelectedValue = objuser.intDesignationID.ToString();
                ddlGrade.SelectedValue = objuser.GradeId.ToString();
            }
        }

    }
    private void GenerateThumbnails(string sourcePath, string targetPath)
    {
        Stream fs = null;
        System.Drawing.Image image = null;
        try
        {
            fs = File.OpenRead(sourcePath);
            image = System.Drawing.Image.FromStream(fs);
        }
        catch { }
        finally { fs.Close(); }
        int newWidth = 0;
        int newHeight = 0;
        newWidth = 70;
        newHeight = 70;
        Bitmap thumbnailImg = new Bitmap(newWidth, newHeight);
        Graphics thumbGraph = Graphics.FromImage(thumbnailImg);
        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
        Rectangle imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
        thumbGraph.DrawImage(image, imageRectangle);
        thumbnailImg.Save(targetPath, image.RawFormat);
    }
    /// <summary>
    /// Method to show data for edit
    /// </summary>
    private void ShowData()
    {
        hdnLevelid.Value = "0";
        IList<User> lstUser = new List<User>();
        objuser.ActionCode = "V";
        objuser.UserID = CommonFunction.DecryptData(Request.QueryString["UID"].ToString());
        strUid = objuser.UserID;
        lstUser = ObjAdminBal.GetAllUsers(objuser);
        foreach (User objUsr in lstUser)
        {
            //Code Added By Dilip Kumar Tripathy on dated 11-Apr-2012
            //Code Modified By Dilip on dated 12-Apr-2012
            if (objUsr.DepartmentID == "")
            {
                Session["deptId"] = "No";
            }
            else
            {
                Session["deptId"] = objUsr.DepartmentID;
            }
            if (objUsr.ReportDepartment == null)
            {
                Session["rpAid"] = "No";
            }
            else
            {
                Session["rpAid"] = objUsr.ReportDepartment;
            }
            ViewState["RptDept"] = objUsr.ReportDepartment;
            CommonProperties.HierachyId = objUsr.HierarchyId;
            ViewState["levelId"] = objUsr.HierarchyId;
            // GetPosition() string strQry = "select intPosition from M_ADM_Level where intLevelId=" + objUsr.LevelId;
            hdnLevelid.Value = objUsr.DepartmentID.ToString();
            CommonProperties.PositionId = ObjAdminBal.GetPosition(objUsr.LevelId.ToString());
            CommonProperties.DId = int.Parse(objUsr.DepartmentID);
            //-----------Code Ended By Dilip--------------------------
            txtfullname.Text = objUsr.FullName;
            txtUserName.Text = objUsr.UserName;
            txtDomainName.Text = objUsr.DomainUserName;
            //GetSubAdminPrev() ViewState["subAdminStat"] = objComnDll.ExeScalar("ConnectionString", "select vchSubAdminPrevil from M_POR_User where intUserId=" + CommonFunction.DecryptData(Request.QueryString["UID"].ToString()), 0);

            ddldistrict.SelectedValue =Convert.ToString(objUsr.intdistrict);

            ddlOfficeType.SelectedValue = objUsr.officecordid;
            //Code added By Dilip Kumar Tripathy on Dated 25-Feb-2012
            // PopUpDropDown3() objComnDll.PopupDropDown("ConnectionString", ddlDesg, "usp_GetDesignationByLoc", "", "int_HierarchyId", objUsr.HierarchyId);
            //PopUpDropDown4()  objComnDll.PopupDropDown("ConnectionString", ddlGrade, "usp_GetGradeByLoc", "", "int_HierarchyId", objUsr.HierarchyId);
            txtMob.Text = objUsr.Mobile;
            txtPaddress.Text = objUsr.PrsentAddress;
            if (objUsr.OfficeTelephone != null)
            {
                if (objUsr.OfficeTelephone.Contains("-"))
                {
                    string[] strOffTel = objUsr.OfficeTelephone.Split('-');
                    txtCountryCode.Text = strOffTel[0].ToString();
                    divHifen1.InnerText = "-";
                    txtStdCode.Text = strOffTel[1].ToString();
                    divHifen2.InnerText = "-";
                    if (strOffTel.Length > 2)
                    {
                        txtOfficeTel.Text = strOffTel[2].ToString();
                    }
                }
                else
                {
                    txtOfficeTel.Text = objUsr.OfficeTelephone;
                }
            }
            else
            {
                txtOfficeTel.Text = "";
            }
            IList<User> objlstplink = ObjAdminBal.PopUpDropDown3(objUsr.HierarchyId.ToString());
            ddlDesg.DataSource = objlstplink;
            ddlDesg.DataValueField = "intDesignationID";
            ddlDesg.DataTextField = "FullName";
            ddlDesg.DataBind();
            ddlDesg.Items.Insert(0, "--Select--");
            ddlDesg.SelectedValue = objUsr.DesignationID;
            hidDesigId.Value = objUsr.DesignationID;
            hidGradeId.Value = objUsr.GradeId.ToString();
            ddlGrade.SelectedValue = Convert.ToString(objUsr.GradeId);
            ddlLocation.Items.FindByValue(Convert.ToString(objUsr.intLocation)).Selected = true;
            lblMaxcounter.Text = (int.Parse(lblMaxcounter.Text) - txtPaddress.Text.Length).ToString();
            // ddlType.SelectedValue = objUsr.Type;
            txtDOJ.Text = objUsr.DateOfJoing.ToString("dd-MMM-yyyy");
            if (txtDOJ.Text == "01-Jan-1900")
            {
                txtDOJ.Text = "";
            }
            txtPCD.Text = objUsr.ProbationCompleteDate.ToString("dd-MMM-yyyy");
            if (txtPCD.Text == "01-Jan-1900")
            {
                txtPCD.Text = "";
            }
            if (objUsr.AdminPrevilliage == "True")
            {
                chkSupAdminPrevil.Checked = true;
            }
          
            if (objUsr.Attendance == "True")
            {
                chkAttendance.Checked = true;
            }
            if (objUsr.Payroll == "True")
            {
                chkPayroll.Checked = true;
                rbtnLstEPF.SelectedValue = objUsr.EPF.ToString();
            }
            rbtGender.SelectedValue = objUsr.Gender;
            txtDOB.Text = objUsr.DateOfBirth.ToString("dd-MMM-yyyy");
            if (txtDOB.Text == "01-Jan-1900")
            {
                txtDOB.Text = "";
            }
            txtEmail.Text = objUsr.email;
            if (objUsr.ReportingUserId == 0)
            {
                imgCollapse.ImageUrl = "~/Console/images/collapse.gif";
                divCont.Style.Add("display", "none");
            }
            else
            {
                imgCollapse.ImageUrl = "~/Console/images/expand.gif";
                divCont.Style.Add("display", "block");
            }
            //txtFatherNameInEnglish.Text = objUsr.FatherNameEnglish.ToString();
            //txtFatherNameInAmharic.Text = objUsr.FatherNameAmharic.ToString();
            //txtGrandFatherNameInEnglish.Text = objUsr.GrandFatherNameEnglish.ToString();
            //txtGrandFatherNameInAmharic.Text = objUsr.GrandFatherNameAmharic.ToString();
            ShowUserImage.ImageUrl = "User_Image/" + objUsr.UserImage;
            hidUserImage.Value = objUsr.UserImage;

            string[] strPrAuth = ObjAdminBal.GetFullNameFromId(objUsr.ReportingUserId).Split('~');
            hidPriority1.Value = strPrAuth[0];
            txtPAuthor.Text = strPrAuth[1];
            string[] strSecAuth = ObjAdminBal.GetFullNameFromId(objUsr.TempReportingUserId).Split('~');
            hidPriority2.Value = strSecAuth[0];
            txtSAuthor.Text = strSecAuth[1];
            string[] strOpAuth = ObjAdminBal.GetFullNameFromId(objUsr.OptionalReportingUserId).Split('~');
            hidPriority3.Value = strOpAuth[0];
            txtOAuthor.Text = strOpAuth[1];

            ((HiddenField)HierarchyForAllLocation1.FindControl("shidLevels")).Value = getLevel(Convert.ToInt32(objUsr.DepartmentID));
            SetpermissionProperties.hidLevel = ((HiddenField)HierarchyForAllLocation1.FindControl("shidLevels")).ClientID;
            CommonProperties.PId = Convert.ToInt32(getPosId(Convert.ToInt32(objUsr.DepartmentID))) - 2;

        }
    }
    private void BindDesigGrid(string strHierId)
    {
        //GetDesigInfo() string strQry = "select M.intDesigId,M.nvchDesigName from M_ADM_Designation M join T_ADM_Designation T on M.intDesigId=T.intDesignationId where intHierarchyId=" + strHierId.ToString();
        //DataTable objDt = objComnDll.GetDataTable("ConnectionString", strQry);
        lstUser = ObjAdminBal.GetDesigInfo(strHierId.ToString());
        ddlDesg.DataSource = lstUser;
        ddlDesg.DataTextField = "FullName";
        ddlDesg.DataValueField = "intDesignationID";
        ddlDesg.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlDesg.Items.Insert(0, list);
        //GetTGradeDetail() strQry = "SELECT M.intGradeId,M.vchGradeName from M_ADM_Grade M join T_ADM_Grade T on M.intGradeId=T.intGradeId where intHierarchyId=" + strHierId.ToString();
        //objDt = objComnDll.GetDataTable("ConnectionString", strQry);
        lstUser = ObjAdminBal.GetTGradeDetail(strHierId.ToString());
        ddlGrade.DataSource = lstUser;
        ddlGrade.DataTextField = "GradeName";
        ddlGrade.DataValueField = "GradeId";
        ddlGrade.DataBind();
        ddlGrade.Items.Insert(0, list);
    }
    /// <summary>
    /// To get No.Of levels
    /// </summary>
    /// <param name="intLvlDtlId"></param>
    /// <returns></returns>
    public string getLevel(int intLvlDtlId)
    {
        string strNoOfLevel = "";
        //GetLevel() string StrQryLevel = "SELECT intNolevel from M_Adm_Hierarchy where bitStatus=1 and  inthierarchyid=(Select inthierarchyid from M_Adm_Level where bitStatus=1 and  intLevelId=(Select intLevelId from M_Adm_LevelDetails where bitStatus=1 and  intleveldetailid=" + intLvlDtlId +")) ";
        //IDataReader objDrLevel = (IDataReader)objComnDll.ExeReader("ConnectionString", StrQryLevel, 0);
        lstUser = ObjAdminBal.UserGetLevel(intLvlDtlId.ToString());
        strNoOfLevel = lstUser[0].GradeId.ToString();
        //while (objDrLevel.Read())
        //{
        //    strNoOfLevel = Convert.ToString(objDrLevel["intNolevel"]);
        //}
        //objDrLevel.Close();
        return strNoOfLevel;
    }
    /// <summary>
    /// Method to get positionid from leveldetail id
    /// </summary>
    /// <param name="intLvlDtlId"></param>
    /// <returns></returns>
    private string getPosId(int intLvlDtlId)
    {
        string strPosId = "";
        //GetPosition2() string StrQryLevel = "SELECT intPosition from M_Adm_Level where bitStatus=1 and intLevelId=(Select intLevelId from M_Adm_LevelDetails where bitStatus=1 and  intleveldetailid=" + intLvlDtlId + ")";
        //IDataReader objDrLevel = (IDataReader)objComnDll.ExeReader("ConnectionString", StrQryLevel, 0);
        lstUser = ObjAdminBal.GetPosition2(intLvlDtlId.ToString());
        strPosId = lstUser[0].PositionId.ToString();
        //while (objDrLevel.Read())
        //{
        //    strPosId = Convert.ToString(objDrLevel["intPosition"]);
        //}
        //objDrLevel.Close();
        return strPosId;
    }
    private int getHierarchyId(int intLvlDtlId)
    {
        int HierarchyId = 0;
        //GetHierarchyID() string StrQryLevel = " select intHierarchyId from M_Adm_Hierarchy where intHierarchyId in (select intHierarchyId from M_Adm_Level where intLevelId in (select intLevelId from M_Adm_LevelDetails where intLevelDetailId=" + intLvlDtlId + " ))";
        //IDataReader objDrLevel = (IDataReader)objComnDll.ExeReader("ConnectionString", StrQryLevel, 0);
        lstUser = ObjAdminBal.UserGetHierarchyID(intLvlDtlId.ToString());
        HierarchyId = lstUser[0].PositionId;
        //while (objDrLevel.Read())
        //{
        //    HierarchyId = Convert.ToInt32(objDrLevel["intHierarchyId"]);
        //}
        //objDrLevel.Close();
        return HierarchyId;
    }
    /// <summary>
    /// To get Parent level of a level
    /// </summary>
    /// <param name="intLvlDtlId"></param>
    /// <returns></returns>
    private string getParentId(int intLvlDtlId)
    {
        string strPldId = "";
        //GetParentID() string StrQryLevel = "SELECT intParentId from M_Adm_LevelDetails where intleveldetailid=" + intLvlDtlId + " and bitStatus=1 ";
        //IDataReader objDrLevel = (IDataReader)objComnDll.ExeReader("ConnectionString", StrQryLevel, 0);
        lstUser = ObjAdminBal.UserGetParentID(intLvlDtlId.ToString());
        strPldId = lstUser[0].ParentId.ToString();
        //while (objDrLevel.Read())
        //{
        //    strPldId = Convert.ToString(objDrLevel["intParentId"]);
        //}
        //objDrLevel.Close();
        return strPldId;
    }
    /// <summary>
    /// To populate
    /// </summary>
    /// <param name="intparentId"></param>
    /// <param name="ddlToFill"></param>
    private void fillDropdown(int intparentId, DropDownList ddlToFill)
    {
        //PopUpDropDown5() string StrQryLevel = "SELECT intleveldetailid,nvchLevelName from M_Adm_LevelDetails where intParentId=" + intparentId + " and bitStatus=1 ";
        //objComnDll.PopupDropDown("ConnectionString", StrQryLevel, ddlToFill, 1);
        lstUser = ObjAdminBal.PopUpDropDown5(intparentId.ToString());
        ddlToFill.DataSource = lstUser;
        ddlToFill.DataTextField = "LevelName";
        ddlToFill.DataValueField = "LevelId";
        ddlToFill.DataBind();
    }
    private void FillOffice(DropDownList ddlOfficeType)
    {
        //PopUpDropDown6() string StrQryLevel = "SELECT intOfficeId, vchName FROM M_OfficeDirectory WHERE bitStatus=1 ORDER BY vchName ASC";
        //objComnDll.PopupDropDown("ConnectionString", StrQryLevel, ddlOfficeType, 1);
        lstUser = ObjAdminBal.PopUpDropDown6();
        ddlOfficeType.DataSource = lstUser;
        ddlOfficeType.DataTextField = "FullName";
        ddlOfficeType.DataValueField = "intLocation";
        ddlOfficeType.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlOfficeType.Items.Insert(0, list);
    }
    /// <summary>
    /// To fill hierarchy
    /// </summary>
    /// <param name="intDepartmentId"></param>
    private void FillControls(int intDepartmentId)
    {
        if (Convert.ToInt32(getParentId(intDepartmentId)) != 0)
        {
            DropDownList ddlTofill = (DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(getPosId(intDepartmentId)) - 1));
            HiddenField hidId = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(getPosId(intDepartmentId)) - 1));
            fillDropdown(Convert.ToInt32(getParentId(intDepartmentId)), ddlTofill);
            ddlTofill.SelectedValue = Convert.ToString(intDepartmentId);
            hidId.Value = Convert.ToString(intDepartmentId);
            Label lblId = (Label)HierarchyForAllLocation1.FindControl("Labels" + Convert.ToString(Convert.ToInt32(getPosId(intDepartmentId))));
            lblId.Text = getLevelNames(intDepartmentId);
        }
        else
        {
            //PopUpDropDown7() string StrQuery = "SELECT A.intLevelDetailId,A.nvchLevelName from M_Admin_LevelDetails A,M_Admin_Level B where  A.intLevelId=B.intLevelId AND A.intParentId=0";
            //objComnDll.PopupDropDown("ConnectionString", StrQuery, ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")), 1);
            lstUser = ObjAdminBal.PopUpDropDown7();
            ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).DataSource = lstUser;
            ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).DataTextField = "LevelName";
            ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).DataValueField = "LevelId";
            ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).DataBind();
            ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).SelectedValue = Convert.ToString(intDepartmentId);
            HiddenField hidId = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs0");
            hidId.Value = Convert.ToString(intDepartmentId);
        }

    }
    private void FillControls2(int intDepartmentId)
    {
        //if (Convert.ToInt32(getParentId(intDepartmentId)) != 0)
        //{
        //    DropDownList ddlTofill = (DropDownList)getUsers1.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(getPosId(intDepartmentId)) - 1));
        //    HiddenField hidId = (HiddenField)getUsers1.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(getPosId(intDepartmentId)) - 1));
        //    fillDropdown(Convert.ToInt32(getParentId(intDepartmentId)), ddlTofill);
        //    ddlTofill.SelectedValue = Convert.ToString(intDepartmentId);
        //    hidId.Value = Convert.ToString(intDepartmentId);
        //    Label lblId = (Label)getUsers1.FindControl("Labels" + Convert.ToString(Convert.ToInt32(getPosId(intDepartmentId))));
        //    lblId.Text = getLevelNames(intDepartmentId);
        //}
        //else
        //{
        //    //PopUpDropDown8() string StrQuery = "SELECT A.intLevelDetailId,A.nvchLevelName from M_Adm_LevelDetails A,M_Adm_Level B where  A.intLevelId=B.intLevelId AND intParentId=0";
        //    //objComnDll.PopupDropDown("ConnectionString", StrQuery, ((DropDownList)getUsers1.FindControl("sdrplayers0")), 1);
        //    lstUser = ObjAdminBal.PopUpDropDown8();
        //    ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).DataSource = lstUser;
        //    ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).DataTextField = "LevelName";
        //    ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).DataValueField = "LevelId";
        //    ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).DataBind();
        //    ((DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers0")).SelectedValue = Convert.ToString(intDepartmentId);
        //    ((DropDownList)getUsers1.FindControl("sdrplayers0")).SelectedValue = Convert.ToString(intDepartmentId);
        //    HiddenField hidId = (HiddenField)getUsers1.FindControl("shidIDs0");
        //    hidId.Value = Convert.ToString(intDepartmentId);
        //}

    }
    /// <summary>
    /// To get level names
    /// </summary>
    /// <param name="intDepartmentId"></param>
    private string getLevelNames(int intDepartmentId)
    {
        string strLvlName = null;
        //PopUpDropDown9() string strQryLevelName = "Select nvchLabel from M_Adm_Level where bitStatus=1 and intLevelId = (Select intLevelId from M_Adm_LevelDetails where bitStatus=1 and intLevelDetailId=" + intDepartmentId + ")";
        //IDataReader objDrLevelName = (IDataReader)objComnDll.ExeReader("ConnectionString", strQryLevelName, 0);
        lstUser = ObjAdminBal.PopUpDropDown9(intDepartmentId.ToString());
        strLvlName = lstUser[0].LevelName;
        //while (objDrLevelName.Read())
        //{
        //    strLvlName = Convert.ToString(objDrLevelName["nvchLabel"]);
        //}
        //objDrLevelName.Close();
        return strLvlName;
    }
    protected void layer4(string lay1, string lay2)
    {
        XmlDocument objDom = new XmlDocument();
        XmlNode objRoot;
        XmlNode objChild1;
        XmlNode objChild2;
        XmlNode objChild3;
        XmlNode objChild4;
        string errstring = null;
        IList<Level> LocList = new List<Level>();
        IList<Level> ListDiv = new List<Level>();
        string strGLBLevel_0 = lay1;
        string strGLBLevel_1 = lay2;
        try
        {
            XmlProcessingInstruction xlsNode = objDom.CreateProcessingInstruction("xml", "version='1.0'");
            objDom.InsertBefore(xlsNode, objDom.DocumentElement);
            objRoot = objDom.CreateElement("ddlevel0");
            objDom.AppendChild(objRoot);
            XmlAttribute createName = objDom.CreateAttribute("name");
            createName.Value = "geo";
            objRoot.Attributes.Append(createName);
            XmlAttribute createValue = objDom.CreateAttribute("value");
            createValue.Value = "geo";
            objRoot.Attributes.Append(createValue);
            XmlAttribute createcaption = objDom.CreateAttribute("caption");
            createcaption.Value = "Select a " + strGLBLevel_0;
            objRoot.Attributes.Append(createcaption);
            objLevelDetails.ActionCode = "A";
            LocList = ObjAdminBal.GetLocationDetails(objLevelDetails);
            foreach (Level LevLoc in LocList)
            {
                objChild1 = objDom.CreateElement("ddlevel1");
                objRoot.AppendChild(objChild1);
                XmlAttribute createLocName = objDom.CreateAttribute("name");
                //strDivName = DrDiv.GetString(1);
                createLocName.Value = LevLoc.LocName;
                objChild1.Attributes.Append(createLocName);
                XmlAttribute createLoccaption = objDom.CreateAttribute("caption");
                createLoccaption.Value = "Select a " + strGLBLevel_1;
                objChild1.Attributes.Append(createLoccaption);
                XmlAttribute createLocValue = objDom.CreateAttribute("value");
                createLocValue.Value = LevLoc.LocId;
                objChild1.Attributes.Append(createLocValue);
                string strLocationId;
                strLocationId = LevLoc.LocId;
                ListDiv = ObjAdminBal.GetDivisionDetails(objLevelDetails, LevLoc.LocId);
                foreach (Level Levdiv in ListDiv)
                {
                    objChild1 = objDom.CreateElement("ddlevel2");
                    objRoot.AppendChild(objChild1);
                    XmlAttribute createDivName = objDom.CreateAttribute("name");
                    createDivName.Value = Levdiv.DivName;
                    objChild1.Attributes.Append(createDivName);
                    XmlAttribute createDivcaption = objDom.CreateAttribute("caption");
                    createDivcaption.Value = "Select a Department";
                    objChild1.Attributes.Append(createDivcaption);
                    XmlAttribute createDivValue = objDom.CreateAttribute("value");
                    createDivValue.Value = Levdiv.DivId;
                    objChild1.Attributes.Append(createDivValue);
                    IList<Level> ListDept = new List<Level>();
                    string divid;
                    divid = Levdiv.DivId;
                    ListDept = ObjAdminBal.GetDepartmentDetails(objLevelDetails, divid);
                    foreach (Level LevDept in ListDept)
                    {
                        objChild2 = objDom.CreateElement("ddlevel3");
                        objChild1.AppendChild(objChild2);
                        errstring = "";
                        errstring = "dept " + LevDept.DeptName;
                        XmlAttribute createDeptName = objDom.CreateAttribute("name");
                        createDeptName.Value = LevDept.DeptName;
                        objChild2.Attributes.Append(createDeptName);
                        XmlAttribute createDeptcaption = objDom.CreateAttribute("caption");
                        createDeptcaption.Value = "Select a Section";
                        objChild2.Attributes.Append(createDeptcaption);
                        XmlAttribute createDeptValue = objDom.CreateAttribute("value");
                        createDeptValue.Value = LevDept.DeptId;
                        objChild2.Attributes.Append(createDeptValue);
                        XmlAttribute createDeptAdmin = objDom.CreateAttribute("admin");
                        createDeptAdmin.Value = LevDept.AdminUserName;
                        objChild2.Attributes.Append(createDeptAdmin);
                        if (LevDept.AdminUserName == "NA")
                        {
                            XmlAttribute DeptadminFullName = objDom.CreateAttribute("adminFullName");
                            DeptadminFullName.Value = "NA";
                            objChild2.Attributes.Append(DeptadminFullName);
                        }
                        else
                        {
                            IList<Level> ListName = new List<Level>();
                            string userid;
                            userid = LevDept.AdminUserName;
                            ListName = ObjAdminBal.GetAdminUserFullName(objLevelDetails, userid);
                            foreach (Level LevAdmin in ListName)
                            {
                                XmlAttribute DeptadminFullName = objDom.CreateAttribute("adminFullName");
                                DeptadminFullName.Value = LevAdmin.AdminUserFullName;
                                objChild2.Attributes.Append(DeptadminFullName);
                            }
                        }
                        IList<Level> ListSec = new List<Level>();
                        string strDeptid;
                        strDeptid = LevDept.DeptId;
                        ListSec = ObjAdminBal.GetSectionDetails(objLevelDetails, strDeptid);
                        foreach (Level LevSec in ListSec)
                        {
                            objChild3 = objDom.CreateElement("ddlevel4");
                            objChild2.AppendChild(objChild3);
                            errstring = "";
                            errstring = "Sec " + LevSec.SecName;
                            XmlAttribute createSecname = objDom.CreateAttribute("name");
                            createSecname.Value = LevSec.SecName;
                            objChild3.Attributes.Append(createSecname);
                            XmlAttribute createSeccaption = objDom.CreateAttribute("caption");
                            createSeccaption.Value = "Select a " + Levdiv.LayerTwo;
                            objChild3.Attributes.Append(createSeccaption);
                            XmlAttribute createSecValue = objDom.CreateAttribute("value");
                            createSecValue.Value = LevSec.SecId;
                            objChild3.Attributes.Append(createSecValue);
                            IList<Level> ListUser = new List<Level>();
                            string strSecid;
                            strSecid = LevSec.SecId;
                            char[] MyChar = { ' ', 'S' };
                            strSecid = strSecid.TrimStart(MyChar);
                            ListUser = ObjAdminBal.GetUserDetails(objLevelDetails, strSecid);
                            foreach (Level LevUser in ListUser)
                            {
                                objChild4 = objDom.CreateElement("ddlevel5");
                                objChild3.AppendChild(objChild4);
                                errstring = "";
                                errstring = "User " + LevUser.UserFullName;
                                XmlAttribute createUsername = objDom.CreateAttribute("name");
                                createUsername.Value = LevUser.UserFullName;
                                objChild4.Attributes.Append(createUsername);
                                XmlAttribute createUserValue = objDom.CreateAttribute("value");
                                createUserValue.Value = LevUser.UserName;
                                objChild4.Attributes.Append(createUserValue);
                                XmlAttribute createusercaption = objDom.CreateAttribute("caption");
                                createusercaption.Value = "Select User";
                                objChild4.Attributes.Append(createusercaption);
                                XmlAttribute createuserid = objDom.CreateAttribute("id");
                                createuserid.Value = LevUser.UserId;
                                objChild4.Attributes.Append(createuserid);
                            }
                        }
                    }
                }
            }
            objDom.Save("C:\\XML_5LEVEL.xml");
        }
        catch (Exception ex)
        {
            Response.Write("<Script> alert(" + ex.Message + "," + errstring + ")</Script>");
        }
    }

    /// <summary>
    /// Creted By : Dilip Kumar Tripathy on dated 30-Oct-2013
    /// DuplicateValueCheck() Method to check duplicate username, domain name and email address.
    /// </summary>
    /// <param name="strActionCode"></param>
    /// <param name="strDuplicVal"></param>
    /// <returns> string </returns>
    [System.Web.Services.WebMethod]
    public static string DuplicateValueCheck(string strActionCode, string strDuplicVal, string strUserId)
    {
       // CommonDLL objComnDll = new CommonDLL();
        AdminAppService ObjAdminBal = new AdminAppService();
        string strResult = "";
        try
        {
            //Message() object[] objParam = { "P_CHARACTIONTYPE", strActionCode, "P_DUPLICATETEXT", strDuplicVal, "@P_USERID", strUserId };

            //strResult = objComnDll.ExeScalar("ConnectionString", "USP_CHECKDUPLIC_USERNAME_EMAIL_DOMAIN", objParam).ToString();
            strResult = ObjAdminBal.Message(strActionCode, strDuplicVal, strUserId); 

        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write("<Script> alert(" + ex.Message + ")</Script>");
        }
        finally
        {
            //objComnDll = null;
        }
        return strResult;
    }
    private void ConfigPageControls()
    {
        TrVisibility();
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml"));
        XmlNode xNode = xmldoc.DocumentElement.SelectSingleNode("/PageConfig");
        XmlNodeList xNodelist;
        if (xNode.HasChildNodes)
        {
            xNodelist = xNode.ChildNodes;
            for (int i = 0; i < xNodelist.Count; i++)
            {
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "DomainUser" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    trDomainUser.Visible = true;
                }
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "OfficeType" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    trOfficType.Visible = true;
                }
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "ProbComp" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    trProbaComp.Visible = true;
                }
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "Grade" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    trGrade.Visible = true;
                }
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "SuperPrevil" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    rAdminprevil.Visible = true;
                }
                    if (xNode.ChildNodes[i].Attributes["Name"].Value == "Attendance" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                    {
                        trAttendance.Visible = true;
                    }
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "Payroll" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    trPayroll.Visible = true;
                }
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "EPF" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    trEpf.Visible = true;
                }
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "Telephone" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    trTelephone.Visible = true;
                }
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "Mobile" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    trMobile.Visible = true;
                }
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "PAddr" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    trPaddr.Visible = true;
                }
                if (xNode.ChildNodes[i].Attributes["Name"].Value == "RA" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                {
                    divRA.Visible = true;
                }
            }
        }

    }
    private void TrVisibility()
    {
        trDomainUser.Visible = false;
        trOfficType.Visible = false;
        trProbaComp.Visible = false;
        trGrade.Visible = false;
        rAdminprevil.Visible = false;
        trAttendance.Visible = false;
        trPayroll.Visible = false;
        trEpf.Visible = false;
        trTelephone.Visible = false;
        trMobile.Visible = false;
        trPaddr.Visible = false;
        divRA.Visible = false;
    }

    private void UncheckAddUser()
    {
        chkDomainUser.Checked = false;
        chkOfficeType.Checked = false;
        chkProbComp.Checked = false;
        chkGrade.Checked = false;
        chkSuperPrevil.Checked = false;
        chkAttend.Checked = false;
        chkProll.Checked = false;
        chkEpf.Checked = false;
        chkTelephone.Checked = false;
        chkMobile.Checked = false;
        chkPAddr.Checked = false;
        chkRA.Checked = false;
    }
    #endregion

    protected void btnSubmitModal_Click(object sender, EventArgs e)
    {
        try
        {
            ConfigAddUser();
            ConfigPageControls();
            pnlPreview_ModalPopupExtender.Hide();
            lblMsgModal.Text = "Fields are Configured Successfully.";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
        }

    }

    protected void btnResetModal_Click(object sender, EventArgs e)
    {
        try
        {
            UncheckAddUser();
            pnlPreview_ModalPopupExtender.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
        }

    }

    private void AssignCheckBoxes()
    {
        try
        {
            if (File.Exists(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml")))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml"));
                XmlNode xNode = xmldoc.DocumentElement.SelectSingleNode("/PageConfig");
                XmlNodeList xNodelist;
                if (xNode.HasChildNodes)
                {
                    xNodelist = xNode.ChildNodes;
                    for (int i = 0; i < xNodelist.Count; i++)
                    {
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "DomainUser" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkDomainUser.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "OfficeType" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkOfficeType.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "ProbComp" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkProbComp.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "Grade" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkGrade.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "SuperPrevil" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkSuperPrevil.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "Attendance" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkAttend.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "Payroll" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkProll.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "EPF" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkEpf.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "Telephone" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkTelephone.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "Mobile" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkMobile.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "PAddr" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkPAddr.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "RA" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkRA.Checked = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
        }
    }

    private void ConfigAddUser()
    {
        try
        {
            if (!Directory.Exists(Server.MapPath("~/Console/ConfigXml")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Console/ConfigXml"));
            }
            if (!File.Exists(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml")))
            {
                File.Delete(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml"));
            }
            XmlElement xChild1;
            XmlDocument objXdoc = new XmlDocument();
            XmlDeclaration declaration = objXdoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            objXdoc.AppendChild(declaration);
            XmlElement xRoot = objXdoc.CreateElement("PageConfig");
            objXdoc.AppendChild(xRoot);
            xChild1 = objXdoc.CreateElement("DomainUser");
            xChild1.SetAttribute("Name", "DomainUser");
            xChild1.SetAttribute("Value", chkDomainUser.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("OfficeType");
            xChild1.SetAttribute("Name", "OfficeType");
            xChild1.SetAttribute("Value", chkOfficeType.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("ProbComp");
            xChild1.SetAttribute("Name", "ProbComp");
            xChild1.SetAttribute("Value", chkProbComp.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("Grade");
            xChild1.SetAttribute("Name", "Grade");
            xChild1.SetAttribute("Value", chkGrade.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("SuperPrevil");
            xChild1.SetAttribute("Name", "SuperPrevil");
            xChild1.SetAttribute("Value", chkSuperPrevil.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("Attendance");
            xChild1.SetAttribute("Name", "Attendance");
            xChild1.SetAttribute("Value", chkAttend.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("Payroll");
            xChild1.SetAttribute("Name", "Payroll");
            xChild1.SetAttribute("Value", chkProll.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("EPF");
            xChild1.SetAttribute("Name", "EPF");
            xChild1.SetAttribute("Value", chkEpf.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("Telephone");
            xChild1.SetAttribute("Name", "Telephone");
            xChild1.SetAttribute("Value", chkTelephone.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("Mobile");
            xChild1.SetAttribute("Name", "Mobile");
            xChild1.SetAttribute("Value", chkMobile.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("PAddr");
            xChild1.SetAttribute("Name", "PAddr");
            xChild1.SetAttribute("Value", chkPAddr.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("RA");
            xChild1.SetAttribute("Name", "RA");
            xChild1.SetAttribute("Value", chkRA.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            objXdoc.Save(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml"));

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
        }
    }


    private void BindDist()
    {
        //GetDesigInfo() string strQry = "select M.intDesigId,M.nvchDesigName from M_ADM_Designation M join T_ADM_Designation T on M.intDesigId=T.intDesignationId where intHierarchyId=" + strHierId.ToString();
        //DataTable objDt = objComnDll.GetDataTable("ConnectionString", strQry);
        lstUser = ObjAdminBal.GetDistrict();
        ddldistrict.DataSource = lstUser;
        ddldistrict.DataTextField = "FullName";
        ddldistrict.DataValueField = "intDesignationID";
        ddldistrict.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddldistrict.Items.Insert(0, list);
      
    }


}
