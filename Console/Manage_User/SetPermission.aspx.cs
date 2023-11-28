/******************************************************************************************************************
'File Name          : SetPermission.aspx.cs
'Description        : Page to set permission of users
'Created by         : Amrita Nayak
'Created On         : 12th-July-2010 
'Modification History:

'                        <CR no.>                      <Date>               <Modified by>          <Modification Summary>                                                         
'                         1                          13th-July-2010          Amrita Nayak        Add the usercontrol to find the loaction,department etc
'                         2                          1st -oct-2010           Biswaranjan         Write the code to set the default button of the page.
'                         3                          25-June-2012            Dilip Tripathy      To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                        
'PDK Function Name : ClearAllCheck(),FillGrid(),fillCopyuser(),filluser()  
' Include files     :           
' Style sheet       :
*******************************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using KWAdminConsole.Messages;
using Manage_Usercontrol_Property;

namespace KwantifyPortalV3._2_App.Admin.Manage_User
{
    public partial class SetPermission : System.Web.UI.Page
    {
        #region "Member Variables"
        User objUser = new User();
        IList<Set_Permission> objLink = new List<Set_Permission>();
        IList<User> objUserList = new List<User>();
        AdminAppService ObjAdminBal = new AdminAppService();
        Set_Permission objPermission = new Set_Permission();
        DataTable dt = new DataTable();
        public int slno = 0;
        public int intType;
        public int CType = 0;
        public int n = 0;
        int a, i, j, rowcount;
        Label lblglink;
        CheckBox chk;
        GridView innergridview;
        RadioButton optbtn;
        RadioButton optbtnAdd;
        RadioButton optbtnView;
        RadioButton optbtnManage;
        CheckBox chkplink;
        string add, view, manage, FunctionId, glinks, plinkadd, plinkview, plinkmanage, DeptId;
        ArrayList strglink = new ArrayList();
        ArrayList strplinkadd = new ArrayList();
        ArrayList strplinkmview = new ArrayList();
        ArrayList srplinkmanage = new ArrayList();
        string[] strDeptId;
        string[] tempArray;
        string strFullPath = "";
        #endregion

        #region "Page Load"
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
            //Code Added By : Dilip Kumar Tripathy on dated 10-May-2013
            //Purpose : To clear the browser cache
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");

            //objAdmin = objKwantify.CreateAdminConsole();
            //ObjMannageUser = objAdmin.CreateUser();
            //objPermission = ObjMannageUser.CreateSetPermission();
            //objUser = ObjMannageUser.CreateUser();
            //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
            AdminConsoleNavigation.strNewLink = ">>" + TabContainer1.ActiveTab.HeaderText;

            if (!IsPostBack)
            {
                GroupMasterProperties.hidlstid = "";
                GroupMasterProperties.hidbtnid = "";
                GroupMasterProperties.hidnval = "";
                AssignAdminProperties.hidadmin = "";
                CommonProperties.PId = 0;
                CommonProperties.HierachyId = 0;
                CommonProperties.Type = "1";
                CommonProperties.UserControlId2 = drpCopyUserList.ClientID;
                CommonProperties.UserControlId = ddlUsers.ClientID;
                CommonProperties.PageUrl = "SetPermission.aspx";
                Page.ClientScript.RegisterStartupScript(GetType(), "", "hideall();", true);
                //if (Session["chkfill"] == null)
                //{
                FillGridView();
                //}
                txtSearch.Focus();
            }
        }
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            intType = 0;
            ViewState["Cnt"] = "1";
        }
        #endregion

        #region "Button Events"
        [System.Web.Services.WebMethod]
        public static string GetUsers(string searchText)
        {
            string strUserIdNames = "";
            AdminAppService ObjAdminBal = new AdminAppService();
            strUserIdNames = ObjAdminBal.GetUserID(searchText);

            return strUserIdNames;
        }
        protected void btnSearchUser_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "hideall();showChk();", true);
            FillCopyUserList();
            txtSearch.Text = "";
            //Commented By Dilip Kumar Tripathy on dated 12-Nov-2013
            //drpUserSearch.Items.Clear();
            //drpUserSearch.Items.Insert(0, "--Select--");
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string glinks;
            CheckBox chk1 = new CheckBox();
            glinks = null;
            plinkadd = null;
            plinkview = null;
            plinkmanage = null;
            int intRec = 0;
            if (hidUserId.Value != "")
            {
                for (i = 0; i <= grd.Rows.Count - 1; i++)
                {
                    lblglink = (Label)(grd.Rows[i].Cells[0].FindControl("lblglink"));
                    glinks = (glinks + (lblglink.Text));
                    innergridview = (GridView)(grd.Rows[i].Cells[0].FindControl("gvwplink"));
                    chk1 = (CheckBox)(grd.Rows[i].Cells[0].FindControl("lnk"));
                    if (chk1.Checked)
                    {

                        for (j = 0; j <= innergridview.Rows.Count - 1; j++)
                        {
                            chk = (CheckBox)(innergridview.Rows[j].Cells[0].FindControl("chk"));
                            objPermission.ActionCode = "AU";
                            objPermission.PlinkId = Convert.ToInt32(innergridview.Rows[j].Cells[2].Text);
                            objPermission.UserID = Convert.ToInt32(hidUserId.Value);
                            objPermission.UpdatedBy = int.Parse(Session["UserId"].ToString());
                            if (chk.Checked)
                            {

                                optbtnAdd = (RadioButton)(innergridview.Rows[j].Cells[4].FindControl("optadd"));
                                optbtnManage = (RadioButton)(innergridview.Rows[j].Cells[4].FindControl("optmanage"));
                                optbtnView = (RadioButton)(innergridview.Rows[j].Cells[4].FindControl("optview"));
                                if (optbtnAdd.Checked)
                                {
                                    objPermission.PermissionType = "1";//Add

                                }
                                optbtn = (RadioButton)(innergridview.Rows[j].Cells[4].FindControl("optview"));
                                if (optbtnView.Checked)
                                {
                                    objPermission.PermissionType = "2";//View
                                }
                                if (optbtnManage.Checked)
                                {
                                    objPermission.PermissionType = "3";//Manage
                                }
                            }
                            else
                            {
                                objPermission.PermissionType = "0";
                            }
                            intRec = ObjAdminBal.AddPermission(objPermission);
                        }
                    }
                    else
                    {
                        objPermission.ActionCode = "PU";
                        objPermission.PlinkId = Convert.ToInt16(lblglink.Text);
                        objPermission.UserID = Convert.ToInt32(hidUserId.Value);
                        objPermission.UpdatedBy = int.Parse(Session["UserId"].ToString());
                        intRec = ObjAdminBal.AddPermission(objPermission);
                    }

                }
                string strOutmsg = StaticValues.message(intRec, "Permission");
                if (ConfigurationManager.AppSettings["XmlDel"].ToString() == "Y")
                {
                    if (ConfigurationManager.AppSettings["AC_UserXML"] != null)
                    {
                        strFullPath = ConfigurationManager.AppSettings["AC_UserXML"] + "/" + objPermission.UserID.ToString() + ".XML";
                        File.Delete((strFullPath + objPermission.UserID + ".xml"));
                    }
                    else
                    {
                        strFullPath = HttpContext.Current.Server.MapPath("~/UserXML/") + objPermission.UserID.ToString() + ".XML";
                        File.Delete(Server.MapPath(strFullPath + objPermission.UserID + ".xml"));
                    }

                    
                    CommonFunction.CreateUsersXML(objPermission.UserID);

                }
                ScriptManager.RegisterStartupScript(this.UpdatePanel2, typeof(string), "", "alert('" + strOutmsg + "');document.location.href='SetPermission.aspx';", true);
            }
            //---------------Added On 23rd Sep to give permission to multiple user--------------------//
            else
            {
                if (hidSelectedValue.Value != "")
                {
                    for (i = 0; i <= grd.Rows.Count - 1; i++)
                    {
                        lblglink = (Label)(grd.Rows[i].Cells[0].FindControl("lblglink"));
                        glinks = (glinks + (lblglink.Text));
                        innergridview = (GridView)(grd.Rows[i].Cells[0].FindControl("gvwplink"));
                        chk1 = (CheckBox)(grd.Rows[i].Cells[0].FindControl("lnk"));
                        if (chk1.Checked)
                        {

                            for (j = 0; j <= innergridview.Rows.Count - 1; j++)
                            {
                                chk = (CheckBox)(innergridview.Rows[j].Cells[0].FindControl("chk"));
                                objPermission.ActionCode = "AU";
                                objPermission.PlinkId = Convert.ToInt32(innergridview.Rows[j].Cells[2].Text);
                                objPermission.UserID = Convert.ToInt32(hidSelectedValue.Value);
                                objPermission.UpdatedBy = int.Parse(Session["UserId"].ToString());
                                if (chk.Checked)
                                {

                                    optbtnAdd = (RadioButton)(innergridview.Rows[j].Cells[4].FindControl("optadd"));
                                    optbtnManage = (RadioButton)(innergridview.Rows[j].Cells[4].FindControl("optmanage"));
                                    optbtnView = (RadioButton)(innergridview.Rows[j].Cells[4].FindControl("optview"));
                                    if (optbtnAdd.Checked)
                                    {
                                        objPermission.PermissionType = "1";//Add

                                    }
                                    optbtn = (RadioButton)(innergridview.Rows[j].Cells[4].FindControl("optview"));
                                    if (optbtnView.Checked)
                                    {
                                        objPermission.PermissionType = "2";//View
                                    }
                                    if (optbtnManage.Checked)
                                    {
                                        objPermission.PermissionType = "3";//Manage
                                    }


                                }
                                else
                                {
                                    objPermission.PermissionType = "0";
                                }
                                intRec = ObjAdminBal.AddPermission(objPermission);
                            }
                        }
                        else
                        {
                            objPermission.ActionCode = "PU";
                            objPermission.PlinkId = Convert.ToInt16(lblglink.Text);
                            objPermission.UserID = Convert.ToInt32(hidSelectedValue.Value);
                            objPermission.UpdatedBy = int.Parse(Session["UserId"].ToString());
                            intRec = ObjAdminBal.AddPermission(objPermission);
                        }

                    }
                    string strOutmsg = StaticValues.message(intRec, "Permission");
                    if (ConfigurationManager.AppSettings["XmlDel"].ToString() == "Y")
                    {
                        if (ConfigurationManager.AppSettings["AC_UserXML"] != null)
                        {
                            strFullPath = ConfigurationManager.AppSettings["AC_UserXML"] + "/" + objPermission.UserID.ToString() + ".XML";
                            File.Delete((strFullPath + objPermission.UserID + ".xml"));
                        }
                        else
                        {
                            strFullPath = HttpContext.Current.Server.MapPath("~/UserXML/") + objPermission.UserID.ToString() + ".XML";
                            File.Delete(Server.MapPath(strFullPath + objPermission.UserID + ".xml"));
                        }

                        
                        CommonFunction.CreateUsersXML(objPermission.UserID);

                    }

                    ScriptManager.RegisterStartupScript(this.UpdatePanel2, typeof(string), "", "alert('" + strOutmsg + "');document.location.href='SetPermission.aspx';", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel2, typeof(string), "alert('Select a user to assign role')", "alert('Select a user to assign role1');", true);
                }
            }

        }
        protected void btnShowUser_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "showChk();", true);
            objPermission.Action = "V";
            objPermission.ID = Convert.ToInt32(hidSelectedValue.Value.ToString());
            objLink = ObjAdminBal.GetPermissionOfPerticularUser(objPermission);
            FillGrid();
            ddlUsers.Items.Insert(0, "--Select--");
            txtSearch.Text = "";
            Page.ClientScript.RegisterStartupScript(GetType(), "", "hideall();", true);

        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            // Session["chkfill"] = null;
            ScriptManager.RegisterStartupScript(this.UpdatePanel2, typeof(string), "", "document.location.href='SetPermission.aspx';", true);
        }
        protected void btnShowCopyUser_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "hideall();showChk();", true);
            intType = 1;
            txtSearch.Text = "";
            //Commented By Dilip Kumar Tripathy on dated 12-Nov-2013
            //drpUserSearch.Items.Clear();
            //drpUserSearch.Items.Insert(0, "--Select--");
            int j = grd.Rows.Count;
            objPermission.Action = "V";
            objPermission.ID = Convert.ToInt32(hidCopyUser.Value);
            objLink = ObjAdminBal.GetPermissionOfPerticularUser(objPermission);
            FillGrid();
            txtSearch.Text = "";
            Page.ClientScript.RegisterStartupScript(GetType(), "", "hideall();showChk();ClearSearchText();", true);
            ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "", "ClearSearchText();", true);
        }
        protected void btnShowSearchUser_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "showChk();", true);
            ddlUsers.Items.Clear();
            ddlUsers.Items.Insert(0, "--Select--");
            drpCopyUserList.Items.Clear();
            drpCopyUserList.Items.Insert(0, "--Select--");
            objPermission.Action = "V";
            //Commented By Dilip Kumar Tripathy on dated 12-Nov-2013
            //objPermission.ID = Convert.ToInt32(hidSelectedValue.Value);
            objPermission.ID = Convert.ToInt32(hidUserId.Value);
            objLink = ObjAdminBal.GetPermissionOfPerticularUser(objPermission);
            FillGrid();
            CommonProperties.PId = 0;
            CommonProperties.PIds = 0;
            SetpermissionProperties.hidLevels = "";
            SetpermissionProperties.hidLevel = "";
            DropDownList ddlLayer2 = ((DropDownList)getUsers.FindControl("sdrplayers0"));
            ddlLayer2.SelectedIndex = 0;
            Page.ClientScript.RegisterStartupScript(GetType(), "", "hideall();", true);
        }

        #endregion

        #region "Grid Events"
        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string name, name1;
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                slno = 1;
                lblglink = ((Label)(e.Row.FindControl("lblglink")));
                GridView grd = ((GridView)(e.Row.FindControl("gvwplink")));
                CheckBox lnk = e.Row.FindControl("lnk") as CheckBox;
                //Commented by Dilip Kumar Tripathy on dated 12-Nov-2013
                // if (drpUserSearch.SelectedValue == "0" || drpCopyUserList.SelectedValue == "0")
                if (hidUserId.Value == "" || drpCopyUserList.SelectedValue == "0")
                {
                    objPermission.Action = "P";
                    objPermission.ID = Convert.ToInt32(lblglink.Text);
                    objLink = ObjAdminBal.GetPermissionOfPerticularUser(objPermission);
                }
                else
                {
                    //strQry = "SELECT DeptID From primarylinkmaster where glinkid='" + lblglink.Text + "' and intDeletedflag<>1";
                    //dt = objComm.GetDataTable("ConnectionString", strQry);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {

                            DeptId = Convert.ToString(dt.Rows[i]["DeptID"]);

                            strDeptId = DeptId.Split(',');
                            for (a = 0; a <= strDeptId.Length - 1; a++)
                            {
                                objPermission.Action = "P";
                                objPermission.ID = Convert.ToInt32(lblglink.Text);
                                objLink = ObjAdminBal.GetPermissionOfPerticularUser(objPermission);
                            }
                        }
                    }
                }
                grd.DataSource = objLink;
                grd.DataBind();
                grd.Visible = true;
                if ((grd.Rows.Count > 0))
                {
                    string grid = grd.ClientID;
                    name = ("grd_gvwplink_" + (e.Row.RowIndex));
                    if (grid.Contains("ctl"))
                    {
                        rowcount = e.Row.RowIndex;
                        if ((e.Row.RowIndex + 2 < 10))
                        {
                            name = ("grd_ctl0" + ((e.Row.RowIndex + 2) + "_gvwplink"));
                            name1 = ("grd_ctl0" + ((e.Row.RowIndex + 2) + "_lnk"));
                        }
                        else
                        {
                            name = ("grd_ctl" + ((e.Row.RowIndex + 2) + "_gvwplink"));
                            name1 = ("grd_ctl" + ((e.Row.RowIndex + 2) + "_lnk"));
                        }
                        Button btnshow = ((Button)(e.Row.FindControl("btnshow")));
                        btnshow.Attributes["onClick"] = ("return show('" + name + "','" + name1 + "')");
                        Button btnhide = ((Button)(e.Row.FindControl("btnhide")));
                        btnhide.Attributes["onClick"] = ("return hide('" + name + "')");
                        chk = ((CheckBox)(grd.HeaderRow.Cells[0].FindControl("chkall")));
                        chk.Attributes["onClick"] = ("return checkall('" + grd.Rows.Count + "','" + name + "')");
                        lnk.Attributes["onClick"] = ("return CheckInnerGridCheckbox ('" + name + "','" + name1 + "')");

                        
                    }
                    else
                    {
                        name1 = ("grd__lnk_" + (e.Row.RowIndex));
                        Button btnshow = ((Button)(e.Row.FindControl("btnshow")));
                        btnshow.Attributes["onClick"] = ("return show('" + name + "','" + name1 + "')");
                        Button btnhide = ((Button)(e.Row.FindControl("btnhide")));
                        btnhide.Attributes["onClick"] = ("return hide('" + name + "')");
                        chk = ((CheckBox)(grd.HeaderRow.Cells[0].FindControl("chkall")));
                        chk.Attributes["onClick"] = ("return checkall('" + grd.Rows.Count + "','" + name + "','" + lnk.ClientID + "')");
                        lnk.Attributes["onClick"] = ("return CheckInnerGridCheckbox ('" + name + "','" + lnk.ClientID + "')");
                        rowcount = rowcount + 2;
                    }

                }
                else
                {
                    Button btnshow = ((Button)(e.Row.FindControl("btnshow")));
                    btnshow.Attributes["onClick"] = "alert('No Primary Link To Show'); return false;";
                    Button btnhide = ((Button)(e.Row.FindControl("btnhide")));
                    btnhide.Attributes["onClick"] = "alert('No Primary Link To Hide'); return false;";
                }
            }
        }
        protected void gvwplink_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                e.Row.Cells[2].Visible = false;//Code Added by Dilip Kumar Tripathy on dated 20-Mar-2012
                slno++;
                objPermission.Action = "F";
                objPermission.ID = Convert.ToInt32(e.Row.Cells[2].Text);
                objLink = ObjAdminBal.GetPermissionOfPerticularUser(objPermission);
                if (objLink.Count > 0)
                {
                    foreach (Set_Permission objlist in objLink)
                    {
                        FunctionId = Convert.ToString(objlist.FunctionId);
                    }
                }
                chkplink = ((CheckBox)(e.Row.Cells[0].FindControl("chk")));
                chkplink.Attributes["onClick"] = "uncheckheader(this.id);";
                objPermission.Action = "M";
                objPermission.ID = Convert.ToInt32(FunctionId);
                objLink = ObjAdminBal.GetPermissionOfPerticularUser(objPermission);
                if (objLink.Count > 0)
                {
                    foreach (Set_Permission objlist in objLink)
                    {
                        add = objlist.PlinkAdd;
                        view = objlist.PlinkView;
                        manage = objlist.PlinkManage;
                    }
                }
                RadioButton optbtnView;
                RadioButton optbtndd;
                RadioButton optbtnManage;
                optbtnView = ((RadioButton)(e.Row.Cells[4].FindControl("optview")));
                optbtnView.Attributes["onclick"] = "makeCheck(this.id,'V')";
                optbtnView.Text = view;
                if ((view == ""))
                {
                    optbtnView.Enabled = false;
                    optbtnView.Text = "View";
                    optbtnView.ToolTip = "Selected Primary Link Has No View Permission";
                }
                else
                {
                    optbtnView.Enabled = true;
                }
                optbtndd = ((RadioButton)(e.Row.Cells[5].FindControl("optadd")));
                optbtndd.Attributes["onClick"] = "makeCheck(this.id,'A')";
                optbtndd.Text = add;
                if ((add == ""))
                {
                    optbtndd.Enabled = false;
                    optbtndd.Text = "Add";
                    optbtndd.ToolTip = "Selected Primary Link Has No Add Permission";
                }
                else
                {
                    optbtndd.Enabled = true;
                }
                optbtnManage = ((RadioButton)(e.Row.Cells[6].FindControl("optmanage")));
                optbtnManage.Attributes["onClick"] = "makeCheck(this.id,'M')";
                optbtnManage.Text = manage;
                if ((manage == ""))
                {
                    optbtnManage.Enabled = false;
                    optbtnManage.Text = "Manage";
                    optbtnManage.ToolTip = "Selected Primary Link Has No Manage Permission";
                }
                else
                {
                    optbtnManage.Enabled = true;
                }

            }
            if ((e.Row.RowType == DataControlRowType.Header))
            {
                ((CheckBox)(e.Row.Cells[0].FindControl("chkall"))).Checked = false;
                e.Row.Cells[2].Visible = false;//Code Added by Dilip Kumar Tripathy on dated 20-Mar-2012
            }
        }
        protected void gvwplink_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.Header))
            {
                e.Row.Cells.RemoveAt(4);
                e.Row.Cells.RemoveAt(4);
                e.Row.Cells[4].ColumnSpan = 3;
                e.Row.Cells[4].Text = "Access Permission";
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            }
        }
        #endregion

        #region "User functions"
        /// <summary>
        /// To uncheck all cheked items.
        /// </summary>
        private void ClearAllCheck()
        {
            int i, j;

            for (i = 0; i <= grd.Rows.Count - 1; i++)
            {
                chk = (CheckBox)(grd.Rows[i].FindControl("lnk"));
                chk.Checked = false;
                innergridview = (GridView)(grd.Rows[i].FindControl("gvwplink"));
                for (j = 0; j <= innergridview.Rows.Count - 1; j++)
                {
                    chk = (CheckBox)(innergridview.Rows[j].Cells[0].FindControl("chk"));
                    optbtn = (RadioButton)(innergridview.Rows[j].Cells[3].FindControl("optadd"));
                    optbtn.Checked = false;
                    optbtn = (RadioButton)(innergridview.Rows[j].Cells[3].FindControl("optview"));
                    optbtn.Checked = false;
                    optbtn = (RadioButton)(innergridview.Rows[j].Cells[3].FindControl("optmanage"));
                    optbtn.Checked = false;
                    chk.Checked = false;
                }
            }
        }
        /// <summary>
        /// To Fill the Grid while selecting user to copy there permission.
        /// </summary>
        private void FillGrid()
        {
            CommonProperties.Type = "0";
            bool IsDataExists = false;
            if (objLink.Count > 0)
            {
                foreach (Set_Permission objlist in objLink)
                {
                    glinks = objlist.GlinkId;
                    plinkadd = objlist.PlinkAdd;
                    plinkmanage = objlist.PlinkManage;
                    plinkview = objlist.PlinkView;
                }
            }
            int i;
            if (glinks != "" && glinks != null)
            {
                IsDataExists = true;
                tempArray = ((glinks.TrimStart(',')).TrimEnd(',')).Split(',');
                foreach (string s in tempArray)
                {
                    strglink.Add(s);
                }
                tempArray = ((plinkadd.TrimStart(',')).TrimEnd(',')).Split(',');
                foreach (string s in tempArray)
                {
                    strplinkadd.Add(s);
                }
                tempArray = ((plinkview.TrimStart(',')).TrimEnd(',')).Split(',');
                foreach (string s in tempArray)
                {
                    strplinkmview.Add(s);
                }
                tempArray = ((plinkmanage.TrimStart(',')).TrimEnd(',')).Split(',');
                foreach (string s in tempArray)
                {
                    srplinkmanage.Add(s);
                }
            }
            bool plinkExists = false;
            bool glinkExists = false;
            if (IsDataExists)
            {
                ClearAllCheck();
                for (i = 0; i <= grd.Rows.Count - 1; i++)
                {
                    lblglink = (Label)(grd.Rows[i].FindControl("lblglink"));
                    chk = (CheckBox)(grd.Rows[i].FindControl("lnk"));
                    if (strglink.Contains(lblglink.Text.ToString()))
                    {
                        //IF THE GLOBAL LINK IS ASSIGNED
                        chk.Checked = IsDataExists;
                        innergridview = (GridView)(grd.Rows[i].FindControl("gvwplink"));
                        for (j = 0; j <= innergridview.Rows.Count - 1; j++)
                        {
                            CheckBox innerchk = (CheckBox)(innergridview.Rows[j].Cells[0].FindControl("chk"));
                            if (strplinkadd.Contains(innergridview.Rows[j].Cells[2].Text.ToString()))
                            {
                                optbtn = (RadioButton)(innergridview.Rows[j].Cells[3].FindControl("optadd"));
                                innerchk.Checked = IsDataExists;
                                optbtn.Checked = IsDataExists;
                                plinkExists = true;
                            }
                            if (strplinkmview.Contains(innergridview.Rows[j].Cells[2].Text.ToString()))
                            {
                                optbtn = (RadioButton)(innergridview.Rows[j].Cells[3].FindControl("optview"));
                                innerchk.Checked = IsDataExists;
                                optbtn.Checked = IsDataExists;
                                plinkExists = true;
                            }
                            if (srplinkmanage.Contains(innergridview.Rows[j].Cells[2].Text.ToString()))
                            {
                                optbtn = (RadioButton)(innergridview.Rows[j].Cells[3].FindControl("optmanage"));
                                innerchk.Checked = IsDataExists;
                                optbtn.Checked = IsDataExists;
                                plinkExists = true;
                            }
                            if (plinkExists == false)
                            {
                                innerchk.Checked = false;
                            }
                        }
                        glinkExists = true;
                    }
                    if (glinkExists == false)
                    {
                        chk.Checked = false;
                        innergridview = (GridView)(grd.Rows[i].FindControl("gvwplink"));
                        for (j = 0; j <= innergridview.Rows.Count - 1; j++)
                        {
                            chk = (CheckBox)(innergridview.Rows[j].Cells[0].FindControl("chk"));
                            optbtn = (RadioButton)(innergridview.Rows[j].Cells[3].FindControl("optadd"));
                            optbtn.Checked = false;
                            optbtn = (RadioButton)(innergridview.Rows[j].Cells[3].FindControl("optview"));
                            optbtn.Checked = false;
                            optbtn = (RadioButton)(innergridview.Rows[j].Cells[3].FindControl("optmanage"));
                            optbtn.Checked = false;
                            chk.Checked = false;
                        }
                    }
                }
                Page.ClientScript.RegisterStartupScript(GetType(), "", "hideall();", true);
            }
            else
            {
                ClearAllCheck();
            }
        }
        /// <summary>
        /// To Fill the grid with users permission.
        /// </summary>
        protected void FillGridView()
        {
            objPermission.Action = "G";
            objPermission.ID = 0;
            objLink = ObjAdminBal.GetPermissionOfPerticularUser(objPermission);
            grd.DataSource = objLink;
            grd.DataBind();
            if (grd.Rows.Count > 0)
            {
                btnsubmit.Visible = true;
                btnReset.Visible = true;
            }
            else
            {
                btnsubmit.Visible = false;
                btnReset.Visible = false;
            }
        }
        /// <summary>
        /// To find the department id of the user.
        /// </summary>
        private string DepartmentOfUser()
        {
            string strDept = "";
            if (((DropDownList)getUsers.FindControl("sdrplayers0")).SelectedIndex > 0)
            {
                int intLayer = Convert.ToInt32(((HiddenField)getUsers.FindControl("shidLevels")).Value);
                HiddenField hdnDeptOfUser = null;
                for (int i = 0; i < intLayer; i++)
                {
                    hdnDeptOfUser = (HiddenField)getUsers.FindControl("shidIDs" + Convert.ToString(i));
                    if (hdnDeptOfUser.Value != "" && hdnDeptOfUser.Value != "0")
                    {
                        strDept = hdnDeptOfUser.Value;
                    }
                }
            }
            return strDept;
        }
        /// <summary>
        /// To Fill the user list to set the same permissions to another user.
        /// </summary>
        protected void fillCopyuser()
        {
            if (DepartmentOfUser() != "")
            {
                objUser.ActionCode = "U";
                objUser.HierarchyId = Convert.ToInt32(DepartmentOfUser());
                objUserList = ObjAdminBal.GetDetails(objUser);
                drpCopyUserList.DataSource = objUserList.ToList();
                drpCopyUserList.DataTextField = "FullName";
                drpCopyUserList.DataValueField = "UserID";
                drpCopyUserList.DataBind();
            }
        }
        /// <summary>
        /// To retain the user control details of user
        /// </summary> 
        /// 
        public void FillCopyUserList()
        {
            if (DepartmentOfUser() != "")
            {
                intType = 1;
                Session["intid"] = DepartmentOfUser();
                SetpermissionProperties.hidLevel = ((HiddenField)getUsers.FindControl("shidLevels")).ClientID;
                CommonProperties.PId = Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"]))) - 2;
                DropDownList ddlLayer1 = ((DropDownList)getUsers.FindControl("sdrplayers0"));
                HiddenField hidId1 = (HiddenField)getUsers.FindControl("shidIDs0");
                int IntParId = Convert.ToInt32(Session["intid"]);
                for (int k = 0; k < Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"]))); k++)
                {
                    if (k == 0)
                    {
                        DropDownList ddlTofill = (DropDownList)getUsers.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"]))) - 1));
                        HiddenField hidId = (HiddenField)getUsers.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"]))) - 1));
                        Label lblId = (Label)getUsers.FindControl("Labels" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"])))));
                        CommonFunction.FillControls(Convert.ToInt32(Session["intid"]), ddlTofill, hidId, lblId, ddlLayer1, hidId1);
                    }
                    else
                    {
                        DropDownList ddlTofill = (DropDownList)getUsers.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(IntParId)) - 1));
                        HiddenField hidId = (HiddenField)getUsers.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(IntParId)) - 1));
                        Label lblId = (Label)getUsers.FindControl("Labels" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(IntParId))));
                        CommonFunction.FillControls(IntParId, ddlTofill, hidId, lblId, ddlLayer1, hidId1);
                    }
                    IntParId = Convert.ToInt32(ObjAdminBal.GetParentId(IntParId));
                }
            }
        }
        public void showUser()
        {
            if (IsPostBack)
            {
                drpCopyUserList.Items.Clear();
                drpCopyUserList.Items.Insert(0, new ListItem("-Select-", "0"));
                if (drpCopyUserList.Items.Count == 1)
                {
                    fillCopyuser();
                    string sel1 = Request.Form["drpCopyUserList"];
                    if (drpCopyUserList.Items.Count > 0 && DepartmentOfUser() != "")
                    {
                        drpCopyUserList.Items.Insert(0, new ListItem("-Select-", "0"));
                        drpCopyUserList.Items.FindByValue(sel1.Trim()).Selected = true;
                    }
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        // [System.Web.Services.WebMethod]
        //public static string[] GetUsersName(string prefixText)
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString());
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("select vchFullName as fullname from M_por_user where bitStatus=0  and vchFullName like  @P_userName+'%' order by vchFullName ", con);
        //    cmd.Parameters.AddWithValue("@P_userName", prefixText);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    string[] VehicleNo = new string[dt.Rows.Count];
        //    int i = 0;
        //    try
        //    {
        //        foreach (DataRow rdr in dt.Rows)
        //        {
        //            VehicleNo.SetValue(rdr["fullname"].ToString(), i);
        //            i++;
        //        }
        //    }
        //    catch { }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return VehicleNo;
        //}
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
        #endregion
    }
}

