using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
//using CSMPDK_3_0;
using KWAdminConsole.Messages;
//using OfficePortal;
//using OfficePortal.Kwantify;
//using OfficePortal.Links.NM_TabMaster;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
namespace KwantifyPortalV3._2_App.Admin.Manage_application
{
    public partial class adminTabmaster : System.Web.UI.Page
    {
        #region "Variable Declaration"
        int intreturnval, intdatakey = 0; //inttabavail,
        public string strbutton = null;
        string strRight = null;
      
     
        AdminAppService ObjAdminBal = new AdminAppService();
        Tab objTabmaster = new Tab();
       
        public string StrOL = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //Code Added By Dilip Tripathy on dated 25-Jun-2012
            //To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                        

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

            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
            this.lblpage.Visible = false;
            LnkbtnAllin.Visible = false;

            this.txtDesc.Attributes.Add("onkeyup", "return TextCounter('" + txtDesc.ClientID + "','" + lblchar.ClientID + "',500);");
           
            //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
            AdminConsoleNavigation.strNewLink = ">>" + TabadminTabMaster.ActiveTab.HeaderText;
            if (Request.QueryString["pdx"] != null)
            {
                GridTabActive.PageIndex = Convert.ToInt32(Request.QueryString["pdx"]);
            }
            if (!IsPostBack)
            {
                if (ConfigurationManager.AppSettings["OL"] != null)
                {
                    StrOL = ConfigurationManager.AppSettings["OL"].ToString();
                    trOL.Visible = true;
                }
                TabadminTabMaster.ActiveTabIndex = 1;
                if (ddlselFun.Items.Count == 0)
                {

                    PopupDropdown(ddlselFun, 0, 1);
                }
                if (ddlActivefun.Items.Count == 0)
                {
                    PopupDropdown(ddlActivefun, 0, 1);
                }
                if (ddlinactivefun.Items.Count == 0)
                {
                    PopupDropdown(ddlinactivefun, 0, 1);
                }
                if (Request.QueryString["Tabid"] != null)
                {
                    //******************** Added By Biswarnjan on28-oct-2010*********************
                    //Purpose:To show the htmlcancel button for back button and set the visibility false of asp button btnreset in update case
                    //************************Begin**************************************************
                    //btnCancel.Visible = true;
                    //btnReset.Visible = false;
                    //************************Begin**************************************************
                    btnsave.Text = "Update";
                    btnReset.Text = "Cancel";
                    strbutton = btnsave.Text;
                    ViewState["intdatakey"] = Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["Tabid"].ToString()));
                    FillEditcase(Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["Tabid"].ToString())));
                    TabadminTabMaster.ActiveTabIndex = 0;
                    TabCreateTab.HeaderText = "UPDATE";
                    AdminConsoleNavigation.strNewLink = ">>" + "UPDATE";

                }
                else
                {
                    //******************** Added By Biswarnjan on28-oct-2010*********************
                    //Purpose:To show the asp button btnreset and set visibility of  htmlcancel button false for back button  for add case
                    //************************Begin**************************************************
                    //btnCancel.Visible = false;
                    //btnReset.Visible = true;
                    //************************Begin**************************************************

                    btnsave.Text = "Save";
                    strbutton = btnsave.Text;
                    //TabadminTabMaster.ActiveTabIndex = 0;
                    TabCreateTab.HeaderText = "CREATE";
                }
                if (Request.QueryString["fId"] != null && Request.QueryString["bId"] != null)
                {
                    TabadminTabMaster.ActiveTabIndex = 1;
                    ddlActivefun.Items.FindByValue(Request.QueryString["fId"].ToString()).Selected = true;
                    ddlActivefun_SelectedIndexChanged(sender, e);
                    ddlactivebtn.Items.FindByValue(Request.QueryString["bId"].ToString()).Selected = true;
                    btnview_Click(sender, e);
                }
                //Code to set the default button of the page
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "javascript", strscript, true);//Commented by Priyabrat Routray 30th Nov 2011
            }
            if (TabadminTabMaster.ActiveTabIndex == 0)
                //strscript = "<script>DefaultFocus('" + btnsave.ClientID + "');</script>";
                btnsave.Focus();
            else if (TabadminTabMaster.ActiveTabIndex == 1)
                // strscript = "<script>DefaultFocus('" + btnview.ClientID + "');</script>";
                btnview.Focus();
            else if (TabadminTabMaster.ActiveTabIndex == 2)
                btninactiveview.Focus();
            // strscript = "<script>DefaultFocus('" + btninactiveview.ClientID + "');</script>";
            // Page.RegisterClientScriptBlock("javascript", strscript);
        }

        #region "Member Function"
        /// <summary>
        /// Function Created By Biswaranjan on 17-july-2010 to fill the serial number of Tab
        /// </summary>
        /// <param name="intfunid"></param>
        /// <returns></returns>
        protected object FillSlno(int intbtnid)
        {
            try
            {
                objTabmaster.ActionCode = "S";
                objTabmaster.ButtonId = intbtnid;
                int intslno = ObjAdminBal.GetSlno(objTabmaster);
                return intslno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Function Created By Biswaranjan on 17-july-2010 to popup function in dropdownlist
        /// </summary>
        /// <param name="ddldropdown"></param>
        protected void PopupDropdown(DropDownList ddldropdown, int selectedval, int flag)
        {
            try
            {
                //flag:1 for fill function,flag:2 for fill Button
                if (flag == 1)
                {
           
                    IList<Primary> objlstplink = ObjAdminBal.FillFunctionType();
                    ddldropdown.DataSource = objlstplink;
                    ddldropdown.DataValueField = "PlinkId";
                    ddldropdown.DataTextField = "PlinkName";
                    ddldropdown.DataBind();
                    ddldropdown.Items.Insert(0, "--Select--");
                }
                if (flag == 2)
                {
                 
                    IList<Tab> objButton = ObjAdminBal.FillButtonList(selectedval);
                    ddldropdown.DataSource = objButton;
                    ddldropdown.DataValueField = "ButtonId";
                    ddldropdown.DataTextField = "ButtonName";
                    ddldropdown.DataBind();
                    ddldropdown.Items.Insert(0, "--Select--");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// function Created By Biswaranjan on 16-july-2010 to Clear the DATA.
        /// </summary>
        protected void ClearTabmaster()
        {
            if (btnsave.Text.ToLower().Trim() == "save")
            {
                TabadminTabMaster.ActiveTabIndex = 0;
                ResetControls();
            }
            else
            {
                TabadminTabMaster.ActiveTabIndex = 1;
                ResetControls();
            }

        }
        public void ResetControls()
        {
            TabCreateTab.HeaderText = "CREATE";
            btnsave.Text = "Save";
            ddlselFun.SelectedIndex = 0;
            txtTabNameInAmharic.Text = "";
            ddlselbtn.Items.Clear();
            ddlselbtn.Items.Add("-Select-");
            txtTabname.Text = "";
            txtFileName.Text = "";
            txtDesc.Text = "";
            lblSlno.Text = "***";
            chkView.Checked = false;
            chkAdd.Checked = false;
            chkMng.Checked = false;
            rbtnNo.Checked = false;
            rbtnYes.Checked = true;
            cbReCXml.Checked = false;

        }
        /// <summary>
        /// funciton Created By Biswaranjan on 16-july-2010 to get the add,vew,manage rights of each Tab
        /// </summary>
        /// <param name="chkRights"></param>
        /// <returns></returns>
        protected string GetRights(CheckBox chkRights)
        {
            try
            {
                if (chkRights.Checked == true)
                {
                    strRight = "Y";
                }
                else
                {
                    strRight = "N";
                }

                return strRight;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void FillGridview(GridView Grdviewname, string actioncode, int intBtnid)
        {
            IList<Tab> objLoc = ObjAdminBal.FillTabGridview(actioncode, intBtnid);
            Grdviewname.DataSource = objLoc;
            Grdviewname.DataBind();
            if (actioncode == "T")
            {
                DisplayPaging(Grdviewname, objLoc.Count, "Active");
            }
            else
            {
                DisplayPaging(Grdviewname, objLoc.Count, "Inactive");
            }


        }
        protected void FillEditcase(int intTabid)
        {
            try
            {
                objTabmaster.ActionCode = "E";
                objTabmaster.TabId = intTabid.ToString();
                IList<Tab> objtablst = ObjAdminBal.GetTabById(objTabmaster);
                foreach (Tab tabdata in objtablst)
                {
                    ddlselFun.Items.FindByValue(Convert.ToString(tabdata.FunctionID)).Selected = true;
                    PopupDropdown(ddlselbtn, Convert.ToInt32(tabdata.FunctionID), 2);
                    ddlselbtn.Items.FindByValue(Convert.ToString(tabdata.ButtonId)).Selected = true;
                    txtTabname.Text = tabdata.TabName;
                    txtTabNameInAmharic.Text = tabdata.TabNameinAmharic;
                    txtFileName.Text = tabdata.URL;
                    txtDesc.Text = tabdata.Description;
                    lblSlno.Text = Convert.ToString(tabdata.ShotNum);
                    cbReCXml.Checked = true;
                    if (tabdata.ADDR == Convert.ToString('Y'))
                    {
                        chkAdd.Checked = true;
                    }
                    else
                    {
                        chkAdd.Checked = false;
                    }
                    if (tabdata.ManageR == Convert.ToString('Y'))
                    {
                        chkMng.Checked = true;
                    }
                    else
                    {
                        chkMng.Checked = false;
                    }
                    if (tabdata.ViewR == Convert.ToString('Y'))
                    {
                        chkView.Checked = true;
                    }
                    else
                    {
                        chkView.Checked = false;
                    }
                    if (tabdata.Deletedflag == 3)
                    {
                        rbtnNo.Checked = true;
                    }
                    if (tabdata.Deletedflag == 0 || tabdata.Deletedflag == 2)
                    {
                        rbtnYes.Checked = true;
                    }
                    btnsave.Text = "Update";
                    lblchar.Text = (Convert.ToInt32(lblchar.Text) - txtDesc.Text.Length).ToString();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region "Dropdownlist Events"
        protected void ddlselFun_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopupDropdown(ddlselbtn, Convert.ToInt32(ddlselFun.SelectedValue), 2);
        }
        protected void ddlActivefun_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopupDropdown(ddlactivebtn, Convert.ToInt32(ddlActivefun.SelectedValue), 2);
        }
        protected void ddlinactivefun_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopupDropdown(ddlinactivebtn, Convert.ToInt32(ddlinactivefun.SelectedValue), 2);
        }
        protected void ddlselbtn_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSlno.Text = Convert.ToString(FillSlno(Convert.ToInt32(ddlselbtn.SelectedValue)));
        }
        #endregion

        #region "Button Events"
        protected void btnactive_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= GridTabinActive.Rows.Count - 1; i++)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)GridTabinActive.Rows[i].FindControl("cbItem");
                    if (chk.Checked == true)
                    {
                        intdatakey = Convert.ToInt32(GridTabinActive.DataKeys[GridTabinActive.Rows[i].RowIndex].Value);
                        objTabmaster.ActionCode = "T";
                        objTabmaster.TabId = intdatakey.ToString();
                        objTabmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                        intreturnval = ObjAdminBal.ActiveTab(objTabmaster);
                    }
                }
                //******************** commented By Biswarnjan on28-oct-2010*********************
                //Purpose:To navigate to the inactive tab after inactive the record and fill with update data in grid.
                //************************Begin**************************************************
                //Response.Write("<script>alert('" + StaticValues.message(intreturnval, "Tab") + "');</script>");
                if (intreturnval == 6)
                {
                    if (ConfigurationManager.AppSettings["XmlDel"].ToString() == "Y")
                    {
                        CommonFunction.DeleteUserXMLFile();
                        CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
                    }
                    string strUrl = "adminTabmaster.aspx?fId=" + ddlinactivefun.SelectedValue + "&bId=" + ddlinactivebtn.SelectedValue;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Tab") + "');document.location.href='" + strUrl + "';", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Tab") + "');", true);
                }
                //TabadminTabMaster.ActiveTabIndex = 1;
                //ddlActivefun.SelectedValue = ddlinactivefun.SelectedValue;
                //PopupDropdown(ddlactivebtn, Convert.ToInt32(ddlActivefun.SelectedValue), 2);
                //ddlactivebtn.SelectedValue = ddlinactivebtn.SelectedValue;
                ////************************Begin***********************************************
                //GridTabActive.Visible = true;
                //FillGridview(GridTabActive, "T", Convert.ToInt32(ddlactivebtn.SelectedValue));
                //if (GridTabActive.Rows.Count > 0)
                //{
                //    btninActive.Visible = true;
                //    btnmodify.Visible = true;
                //}
                //else
                //{
                //    btninActive.Visible = false;
                //    btnmodify.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            AdminConsoleNavigation.strNewLink = ">>" + "ACTIVE";
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnsave.Text.ToLower().Trim() == "save")
                {

                    objTabmaster.ActionCode = "A";
                    objTabmaster.TabId = "0";
                    objTabmaster.ButtonId = Convert.ToInt32(ddlselbtn.SelectedValue);
                    objTabmaster.TabName = txtTabname.Text;
                    objTabmaster.TabNameinAmharic = txtTabNameInAmharic.Text.Trim();
                    objTabmaster.URL = txtFileName.Text;
                    objTabmaster.Description = txtDesc.Text;
                    objTabmaster.ADDR = GetRights(chkAdd);
                    objTabmaster.ViewR = GetRights(chkView);
                    objTabmaster.ManageR = GetRights(chkMng);
                    objTabmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    objTabmaster.Deletedflag = objTabmaster.Deletedflag = rbtnYes.Checked == true ? 0 : 3;
                    objTabmaster.ShotNum = Convert.ToInt32(lblSlno.Text);
                    intreturnval = Convert.ToInt32(ObjAdminBal.AddTab(objTabmaster));
                    if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
                    {
                        CommonFunction.DeleteUserXMLFile();
                        CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
                    }
                    ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Tab") + "');", true);
                    ResetControls();
                }
                //**************************Else Condition for Update*******************************
                else
                {
                    if (ViewState["intdatakey"] != null)
                    {
                        intdatakey = Convert.ToInt32(ViewState["intdatakey"]);//id for active tab
                    }
                    else
                    {
                        intdatakey = Convert.ToInt32(ViewState["intinactdatakey"]);//id for inactive tab
                    }

                    ViewState["intdatakey"] = null;
                    ViewState["intinactdatakey"] = null;
                    // Code to Update Tab 
                    objTabmaster.ActionCode = "U";
                    objTabmaster.TabId = intdatakey.ToString();
                    objTabmaster.ButtonId = Convert.ToInt32(ddlselbtn.SelectedValue);
                    objTabmaster.TabName = txtTabname.Text;
                    objTabmaster.TabNameinAmharic = txtTabNameInAmharic.Text.Trim();
                    objTabmaster.URL = txtFileName.Text;
                    objTabmaster.Description = txtDesc.Text;
                    objTabmaster.ADDR = GetRights(chkAdd);
                    objTabmaster.ViewR = GetRights(chkView);
                    objTabmaster.ManageR = GetRights(chkMng);
                    objTabmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    objTabmaster.Deletedflag = rbtnYes.Checked == true ? 0 : 3;
                    objTabmaster.ShotNum = Convert.ToInt32(lblSlno.Text);
                    intreturnval = Convert.ToInt32(ObjAdminBal.EditTab(objTabmaster));
                    if (ConfigurationManager.AppSettings["XmlDel"].ToString() == "Y")
                    {
                        if (cbReCXml.Checked == true)
                        {
                            CommonFunction.DeleteUserXMLFile();
                            CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
                        }
                    }
                }
                if (intreturnval == 2)
                {
                    string strUrl = "adminTabmaster.aspx?fId=" + ddlselFun.SelectedValue + "&bId=" + ddlselbtn.Text + "&pdx=" + Request.QueryString["pindex"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Tab") + "');document.location.href='" + strUrl + "';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Tab") + "');", true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void btnview_Click(object sender, EventArgs e)
        {
            GridTabActive.Visible = true;//Added By Biswaranjan on 28-oct-2010
            LnkbtnAllin.Text = "All";
            FillGridview(GridTabActive, "T", Convert.ToInt32(ddlactivebtn.SelectedValue));
            if (GridTabActive.Rows.Count > 0)
            {
                btninActive.Visible = true;
                btnmodify.Visible = true;
            }
            else
            {
                btninActive.Visible = false;
                btnmodify.Visible = false;
            }
        }
        protected void btninActive_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= GridTabActive.Rows.Count - 1; i++)
                {
                    CheckBox chk = new CheckBox();
                    chk = (CheckBox)GridTabActive.Rows[i].FindControl("cbItem");
                    if (chk.Checked == true)
                    {
                        intdatakey = Convert.ToInt32(GridTabActive.DataKeys[i].Value);

                        objTabmaster.ActionCode = "I";
                        objTabmaster.TabId = intdatakey.ToString();
                        objTabmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                        intreturnval = ObjAdminBal.InActiveTab(objTabmaster);
                    }
                }

                //******************** commented By Biswarnjan on28-oct-2010*********************
                //Purpose:To navigate to the inactive tab after inactive the record and fill with update data in grid.
                //************************Begin**************************************************
                // Response.Write("<script>alert('" + StaticValues.message(intreturnval, "Tab") + "');document.location.href='adminTabmaster.aspx'</script>");
                if (intreturnval == 7)
                {
                    if (ConfigurationManager.AppSettings["XmlDel"].ToString() == "Y")
                    {
                        CommonFunction.DeleteUserXMLFile();
                        CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
                    }
                    ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Tab") + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Tab") + "');", true);
                }
                TabadminTabMaster.ActiveTabIndex = 1;
                //************************Begin***********************************************
                FillGridview(GridTabActive, "T", Convert.ToInt32(ddlactivebtn.SelectedValue));
                if (GridTabActive.Rows.Count > 0)
                {
                    btninActive.Visible = true;
                    btnmodify.Visible = true;
                }
                else
                {
                    btninActive.Visible = false;
                    btnmodify.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void btninactiveview_Click(object sender, EventArgs e)
        {
            GridTabinActive.Visible = true;//Added By Biswaranjan on 28-oct-2010
            FillGridview(GridTabinActive, "I", Convert.ToInt32(ddlinactivebtn.SelectedValue));
            if (GridTabinActive.Rows.Count > 0)
            {
                btnactive.Visible = true;
                btnmodify.Visible = true;
            }
            else
            {
                btnactive.Visible = false;
                btnmodify.Visible = false;
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            if (btnReset.Text == "Reset")
            {
                ResetControls();
            }
            else
            {
                ddlActivefun.SelectedValue = ddlselFun.SelectedValue;
                PopupDropdown(ddlactivebtn, Convert.ToInt32(ddlActivefun.SelectedValue), 2);
                ddlactivebtn.SelectedValue = ddlselbtn.SelectedValue;
                TabadminTabMaster.ActiveTabIndex = 1;
                GridTabActive.PageIndex = Convert.ToInt32(Request.QueryString["pindex"]);
                GridTabActive.Visible = true;//Added By Biswaranjan on 28-oct-2010
                FillGridview(GridTabActive, "T", Convert.ToInt32(ddlactivebtn.SelectedValue));
                if (GridTabActive.Rows.Count > 0)
                {
                    btninActive.Visible = true;
                    btnmodify.Visible = true;
                }
                else
                {
                    btninActive.Visible = false;
                    btnmodify.Visible = false;
                }
                ResetControls();
                AdminConsoleNavigation.strNewLink = ">>" + "ACTIVE";
            }
        }
        #endregion
        protected void TabadminTabMaster_ActiveTabChanged(object sender, EventArgs e)
        {
            //******************** commented By Biswarnjan on28-oct-2010*********************
            //Purpose:To navigate to the inactive tab after inactive the record and fill with update data in grid.
            //************************Begin**************************************************
            //ddlactivebtn.SelectedIndex = 0;
            //ddlinactivebtn.SelectedIndex = 0;
            //ddlActivefun.SelectedIndex = 0;
            //ddlinactivefun.SelectedIndex = 0;
            GridTabActive.Visible = false;
            GridTabinActive.Visible = false;
            btnactive.Visible = false;
            btnmodify.Visible = false;
            btninActive.Visible = false;
            //************************Begin***********************************************
            //ddlselFun.SelectedValue = "0";
            //ddlselbtn.SelectedValue = "0";
            txtTabname.Text = "";
            txtFileName.Text = "";
            txtDesc.Text = "";
            lblSlno.Text = "***";
            rbtnYes.Checked = true;
            rbtnNo.Checked = false;
            chkAdd.Checked = false;
            chkMng.Checked = false;
            chkView.Checked = false;
            TabCreateTab.HeaderText = "CREATE";
            LnkbtnAllin.Text = "All";
            lbtnAll.Text = "All";
            GridTabActive.AllowPaging = true;
            GridTabinActive.AllowPaging = true;
        }
        //Modified By   : Dilip Kumar Tripathy
        //Modified Date :10-Jun-2013
        //Purpose       : To make paging for two types of gridview
        private void DisplayPaging(GridView gridviewone, int count, string status)
        {
            if (gridviewone.Rows.Count > 0)
            {
                if (status == "Active")
                {
                    this.lblpage.Visible = true;
                    LnkbtnAllin.Visible = true;
                    this.lblpage.Text = CommonFunction.ShowGridPaging(gridviewone, gridviewone.PageSize, gridviewone.PageIndex, count);
                }

                else
                {
                    this.lblPaging.Visible = true;
                    lbtnAll.Visible = true;
                    this.lblPaging.Text = CommonFunction.ShowGridPaging(gridviewone, gridviewone.PageSize, gridviewone.PageIndex, count);
                }
            }
            else
            {
                this.lblPaging.Visible = false;
                lbtnAll.Visible = false;
                this.lblpage.Visible = false;
                LnkbtnAllin.Visible = false;

            }

        }

        protected void LnkbtnAllin_Click(object sender, EventArgs e)
        {
            this.lblpage.Visible = true;
            LnkbtnAllin.Visible = true;

            if (LnkbtnAllin.Text == "All")
            {
                LnkbtnAllin.Text = "Paging";
                this.GridTabActive.PageIndex = 0;
                GridTabActive.AllowPaging = false;
                FillGridview(GridTabActive, "T", Convert.ToInt32(ddlactivebtn.SelectedValue));
                if (GridTabActive.Rows.Count > 0)
                {
                    this.lblpage.Text = "1-" + GridTabActive.Rows.Count.ToString() + " Of " + GridTabActive.Rows.Count.ToString();
                }
            }
            else
            {
                LnkbtnAllin.Text = "All";
                GridTabActive.AllowPaging = true;
                FillGridview(GridTabActive, "T", Convert.ToInt32(ddlactivebtn.SelectedValue));
            }
        }

        protected void lbtnAll_Click(object sender, EventArgs e)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;

            if (lbtnAll.Text == "All")
            {
                lbtnAll.Text = "Paging";
                this.GridTabinActive.PageIndex = 0;
                GridTabinActive.AllowPaging = false;


                FillGridview(GridTabinActive, "I", Convert.ToInt32(ddlinactivefun.SelectedValue));

                if (GridTabinActive.Rows.Count > 0)
                {
                    this.lblPaging.Text = "1-" + GridTabinActive.Rows.Count.ToString() + " Of " + GridTabinActive.Rows.Count.ToString();
                }
            }
            else
            {
                lbtnAll.Text = "All";
                GridTabinActive.AllowPaging = true;
                FillGridview(GridTabinActive, "I", Convert.ToInt32(ddlinactivefun.SelectedValue));
            }
        }

        protected void btnmodify_click(object sender, EventArgs e)
        {
            int intOutput = 0;
            for (int i = 0; i < GridTabActive.Rows.Count; i++)
            {
                int buttonid = 0;
                CheckBox chkTab = (CheckBox)GridTabActive.Rows[i].FindControl("cbItem");
                System.Web.UI.WebControls.TextBox txtButtonSlno = (System.Web.UI.WebControls.TextBox)GridTabActive.Rows[i].FindControl("txtslno");

                if (chkTab.Checked == true)
                {
                    objTabmaster.ActionCode = "E";
                    objTabmaster.TabId = Convert.ToString(GridTabActive.DataKeys[i].Value);
                    IList<Tab> objbtnlst = ObjAdminBal.GetAllTab(objTabmaster);
                    foreach (Tab data in objbtnlst)
                    {
                        buttonid = data.ButtonId;
                    }
                    objTabmaster.ActionCode = "S";
                    objTabmaster.TabId = Convert.ToString(GridTabActive.DataKeys[i].Value);
                    objTabmaster.ButtonId = buttonid;
                    objTabmaster.ShotNum = Convert.ToInt32(txtButtonSlno.Text);
                    objTabmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    intOutput = ObjAdminBal.TabUpdateSLNO(objTabmaster);
                }
            }
            if (intOutput == 9)
            {
                if (ConfigurationManager.AppSettings["XmlDel"].ToString() == "Y")
                {
                    CommonFunction.DeleteUserXMLFile();
                    CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
                }
                string strOutmsg = StaticValues.message(intOutput, "Tab(s)");
                ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + strOutmsg + "');", true);
            }
            else
            {
                string strOutmsg = StaticValues.message(intOutput, "Tab(s)");
                ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + strOutmsg + "');", true);
            }
            FillGridview(GridTabActive, "T", Convert.ToInt32(ddlactivebtn.SelectedValue));
        }

        protected void GridTabActive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (StrOL == "")
                {
                    GridTabActive.Columns[3].Visible = false;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string strTabId = GridTabActive.DataKeys[e.Row.RowIndex].Value.ToString();
                    HyperLink hypEdit = e.Row.FindControl("Hypedit") as HyperLink;
                    hypEdit.NavigateUrl = "adminTabmaster.aspx?Tabid=" + CommonFunction.EncryptData(strTabId) + "&pindex=" + GridTabActive.PageIndex;
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[3].Text = "Tab Name(In " + StrOL + ")";

                }
            }

            catch (Exception e1)
            {
                throw new Exception(e1.Message, e1);
            }
        }
        protected void GridTabInActive_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (StrOL == "")
                {
                    GridTabinActive.Columns[3].Visible = false;
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[3].Text = "Tab Name(In " + StrOL + ")";

                }
            }

            catch (Exception e1)
            {
                throw new Exception(e1.Message, e1);
            }
        }
        

        protected void GridTabActive_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridTabActive.PageIndex = e.NewPageIndex;
            btnview_Click(sender, e);
        }
        protected void GridTabinActive_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridTabinActive.PageIndex = e.NewPageIndex;
            btninactiveview_Click(sender, e);
        }

    }

}