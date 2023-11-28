
/********************************************************************************************************************
' File Name             :   adminbuttonmaster.cs
' Description           :   To Add/edit/delete/activate/inactivate Button
' Created by            :   Biswaranjan Das
' Created On            :   16-jul-2010
' Modification History  :
'                           <CR no.>       <Date>               <Modified by>                <Modification Summary>'                                                          
'                            1.            10-Nov-2010           Biswaranjan                 For BugFixing
'                            2.            25-Jun-2012           Dilip Tripathy              To Manage The CSRF security error added the code to check the querystring value of 'att' in page load   
' Function Name         :   FillEditcase(),FillSlno(),FillGridview(),FillFunctionType(), GetRights(),AddButtonMaster(),EditButtonMaster()
' Procedures Used       :   usp_M_Admin_Button_Manage,usp_M_Admin_Button_View
' User Defined Namespace:  
' Inherited classes     :                                              
**********************************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using KWAdminConsole.Messages;
public partial class Admin_Manage_application_adminbuttonmaster : System.Web.UI.Page
{
    #region "Variable Declaration"
    int intreturnval, intdatakey = 0;
    public string strbutton = null;
    string strRight = null;
    // string strcommand = null;
   
    ////CommonDLL objCmnDll = new CommonDLL();
    AdminAppService ObjAdminBal = new AdminAppService();
    int strCounterRemarks = 50;
    ACButton objbuttonmaster = new ACButton();
  
    int RecCount;
    public static string strddlActivefunval, strddlinactivefunval = null;//Added By Biswaranjan on 10-Nov-2010
    public string StrOL = "";
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

       

        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabadminButtonMaster.ActiveTab.HeaderText;
        if (Request.QueryString["pdx"] != null)
        {
            GridBtnActive.PageIndex = Convert.ToInt32(Request.QueryString["pdx"]);
        }
        txtFilename.Attributes.Add("onkeyup", "return TextCounterArea('" + txtFilename.ClientID + "','" + lblRemarks.ClientID + "','" + strCounterRemarks + "');");
        txtDesc.Attributes.Add("onkeyup", "return TextCounterArea('" + txtDesc.ClientID + "','" + lblchar.ClientID + "',500);");//Added By Biswaranjan  on 10-Nov-2010
        if (!IsPostBack)
        {
            if (ConfigurationManager.AppSettings["OL"] != null)
            {
                StrOL = ConfigurationManager.AppSettings["OL"].ToString();
                trOL.Visible = true;
            }
            btnsave.Text = "Save";
            FillFunction(ddlselFun);
            FillFunction(ddlActivefun);
            FillFunction(ddlinactivefun);
            if (Request.QueryString["btnid"] != null)
            {
                TabCreateButton.HeaderText = "UPDATE";
                AdminConsoleNavigation.strNewLink = ">>" + "UPDATE";
                btnsave.Text = "Update";
                btnReset.Text = "Cancel";
                strbutton = btnsave.Text;
                ViewState["intdatakey"] = Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["btnid"].ToString()));
                FillEditcase(Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["btnid"].ToString())));
                TabadminButtonMaster.ActiveTabIndex = 0;
            }

            //************************End**************************************************
            else
            {
                btnsave.Text = "Save";
                strbutton = btnsave.Text;
                //TabadminButtonMaster.ActiveTabIndex = 0;
            }
            if (Request.QueryString["funcId"] != null)
            {
                ddlActivefun.Items.FindByValue(Request.QueryString["funcId"].ToString()).Selected = true;
                btnActiveView_Click(sender, e);
            }

        }
        btnActive.Visible = false;
        if (TabadminButtonMaster.ActiveTabIndex == 0)
            btnsave.Focus();

        else if (TabadminButtonMaster.ActiveTabIndex == 1)
            btnActiveView.Focus();
        else if (TabadminButtonMaster.ActiveTabIndex == 2)
            btninactiveview.Focus();
        

    }
    #endregion

    #region "Member Function"
    /// <summary>
    /// Function Created By Biswarnjan on 16-july-2010 to fill the data in edit case
    /// </summary>
    /// <param name="intbtnid"></param>
    protected void FillEditcase(int intbtnid)
    {
        try
        {
            //object[] param = { "chr_ActionCode", "E", "int_ButtonId", intbtnid, "intFunctionid", 0 };
            //DbDr = (DbDataReader)objCmnDll.ExeReader(strcon, "usp_M_Admin_Button_View", "P_RECORDSET", param);
            IList<ACButton> ObjButton = ObjAdminBal.FillEdit(intbtnid.ToString());

            if (ObjButton.Count>0)
            {
                TabCreateButton.HeaderText = "UPDATE";
                ddlselFun.Items.FindByValue(Convert.ToString(ObjButton[0].FunctionID.ToString())).Selected = true;
                txtBtnname.Text = ObjButton[0].ButtonName.ToString();
                txtBtnnameOL.Text = ObjButton[0].ButtonNameOL.ToString();
                txtFilename.Text = ObjButton[0].URL.ToString();
                lblRemarks.Text = (50 - (txtFilename.Text.Length)).ToString();
                txtDesc.Text = ObjButton[0].Description.ToString();
                lblchar.Text = (Convert.ToInt32(lblchar.Text) - txtDesc.Text.Length).ToString();
                lblSlno.Text = ObjButton[0].ShotNum.ToString();
                cbReCXml.Checked = true;
                if (Convert.ToInt32(ObjButton[0].TabAvail) == 1)
                {
                    rbtTabyes.Checked = true;
                }
                else
                {
                    rbtTabNo.Checked = true;
                }
                if (ObjButton[0].ADDR.ToString() == Convert.ToString('Y'))
                {
                    chkAdd.Checked = true;
                }
                else
                {
                    chkAdd.Checked = false;
                }
                if (ObjButton[0].ManageR.ToString() == Convert.ToString('Y'))
                {
                    chkMng.Checked = true;
                }
                else
                {
                    chkMng.Checked = false;
                }
                if (ObjButton[0].ViewR.ToString() == Convert.ToString('Y'))
                {
                    chkView.Checked = true;
                }
                else
                {
                    chkView.Checked = false;
                }

                if (Convert.ToInt32(ObjButton[0].DeletedFlag) == 3)
                {
                    rbtnNo.Checked = true;
                }
                if (Convert.ToInt32(ObjButton[0].DeletedFlag) == 0 || Convert.ToInt32(ObjButton[0].DeletedFlag) == 2)
                {
                    rbtnYes.Checked = true;
                }

                btnsave.Text = "Update";
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    /// <summary>
    /// Function Created By Biswaranjan on 16-july-2010 to fill Gridveiw.
    /// </summary>
    /// <param name="Gridview"></param>
    /// <param name="intfunctionid"></param>
    /// <param name="flag"></param>
    protected void FillGridview(GridView Gridview, string actioncode, int intfunctionid)
    {
        Gridview.Visible = true;
        
        //Gridview = objCmnDll.FillGridview(strcon, Gridview, "usp_M_Admin_Button_View", "P_RECORDSET", "chr_ActionCode", actioncode, "intButtonId", 0, "intFunctionid", intfunctionid);
       // DataTable dt = objCmnDll.GetDataTable(strcon, "usp_M_Admin_Button_View", "P_RECORDSET", "chr_ActionCode", actioncode, "int_ButtonId", 0, "intFunctionid", intfunctionid);
        IList<ACButton> ObjButton = ObjAdminBal.GetDataTable(actioncode, intfunctionid);
        RecCount = ObjButton.Count;
        //Code Added By Dilip Kumar Tripathy on dated 14-Mar-2012
        //Prupose: To separate the DisplayPaging method for two types of gridview
        //Gridview = objCmnDll.FillGridview(dt, Gridview);
        Gridview.DataSource = ObjButton;
        Gridview.DataBind();
        if (Gridview.Rows.Count > 0)
        {
            btnActive.Visible = true;
            btnmodify.Visible = true;
            btninActive.Visible = true;
        }
        if (actioncode == "T")
        {
            DisplayPaging(Gridview, RecCount, "Active");
        }
        else
        {
            DisplayPaging(Gridview, RecCount, "Inactive");
        }
    }
    /// <summary>
    /// Function Created By Biswaranjan on 16-july-2010 to popup function in dropdownlist
    /// </summary>
    /// <param name="ddldropdown"></param>
    /// 
    protected void FillFunction(DropDownList ddlselFunction)
    {
        
        try
        {
            IList<Primary> objlstplink =ObjAdminBal.FillFunctionType();       
            ddlselFunction.DataSource = objlstplink;
            ddlselFunction.DataValueField = "PlinkId";
            ddlselFunction.DataTextField = "PlinkName";          
            ddlselFunction.DataBind();
            ddlselFunction.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// funciton Created By Biswaranjan on 16-july-2010 to get the add,vew,manage rights of each button
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

    //***************Added By Biswaranjan on 10-Nov-2010***************
    //**********************Begin**************************************
    //***********************Summery*****************************
    //Function Name          : AddButtonMaster()
    //Purpose                : To Add Buttonmaster details
    //Parameters Name        : None
    //Parameters Datatype    : None
    //Out Parameters Name    : None
    //Out Parameters Datatype: None
    //Retun Datatype         : None
    //Created By             : Biswaranjan Dash
    //Created Date           : 10-Nov-2010
    //*****************************************************
    private void AddButtonMaster()
    {
        try
        {
            objbuttonmaster.ActionCode = "A";
            objbuttonmaster.ButtonId = "0";
            objbuttonmaster.FunctionID = Convert.ToInt32(ddlselFun.SelectedValue);
            objbuttonmaster.ButtonName = txtBtnname.Text.Trim();
            objbuttonmaster.ButtonNameOL = txtBtnnameOL.Text.Trim();
            objbuttonmaster.URL = txtFilename.Text;
            objbuttonmaster.Description = txtDesc.Text;
            objbuttonmaster.TabAvail = rbtTabyes.Checked == true ? 1 : 0;
            objbuttonmaster.ADDR = GetRights(chkAdd);
            objbuttonmaster.ViewR = GetRights(chkView);
            objbuttonmaster.ManageR = GetRights(chkMng);
            objbuttonmaster.ShotNum = Convert.ToInt32(lblSlno.Text);
            objbuttonmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
            objbuttonmaster.DeletedFlag = rbtnYes.Checked == true ? 0 : 3;
            intreturnval = Convert.ToInt32(ObjAdminBal.AddButton(objbuttonmaster));
            ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Button") + "');", true);
            if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
            {
                Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
            }
            ResetControls();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //***********************Summery*****************************
    //Function Name          : EditButtonMaster()
    //Purpose                : To Add Buttonmaster details
    //Parameters Name        : None
    //Parameters Datatype    : None
    //Out Parameters Name    : None
    //Out Parameters Datatype: None
    //Retun Datatype         : None
    //Created By             : Biswaranjan Dash
    //Created Date           : 10-Nov-2010
    //*****************************************************
    private void EditButtonMaster()
    {
        try
        {
            if (ViewState["intdatakey"] != null)
            {
                intdatakey = Convert.ToInt32(ViewState["intdatakey"]);
            }
            else
            {
                intdatakey = Convert.ToInt32(ViewState["inactivedatakey"]);
            }
            //  Code to Update Button 
            objbuttonmaster.ActionCode = "U";
            objbuttonmaster.ButtonId = intdatakey.ToString();
            objbuttonmaster.FunctionID = Convert.ToInt32(ddlselFun.SelectedValue);
            objbuttonmaster.ButtonName = txtBtnname.Text;
            objbuttonmaster.ButtonNameOL = txtBtnnameOL.Text;
            objbuttonmaster.URL = txtFilename.Text;
            objbuttonmaster.Description = txtDesc.Text;
            objbuttonmaster.TabAvail = rbtTabyes.Checked == true ? 1 : 0;
            objbuttonmaster.ADDR = GetRights(chkAdd);
            objbuttonmaster.ViewR = GetRights(chkView);
            objbuttonmaster.ManageR = GetRights(chkMng);
            objbuttonmaster.ShotNum = Convert.ToInt32(lblSlno.Text);
            objbuttonmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
            objbuttonmaster.DeletedFlag = rbtnYes.Checked == true ? 0 : 3;
            intreturnval = Convert.ToInt32(ObjAdminBal.EditButton(objbuttonmaster));
            if (intreturnval == 2)
            {
                if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
                {
                    if (cbReCXml.Checked == true)
                    {
                        Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                        CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
                    }
                }
                string strUrl = "adminbuttonmaster.aspx?funcId=" + ddlselFun.SelectedValue + "&pdx=" + Request.QueryString["pindex"].ToString();
                ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Button") + "');document.location.href='" + strUrl + "';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Button") + "');", true);
            }

            if (ViewState["intdatakey"] != null)//Condition for active case
            {
                TabadminButtonMaster.ActiveTabIndex = 1;
                ddlActivefun.SelectedValue = strddlActivefunval;
                strddlinactivefunval = null;
                FillGridview(GridBtnActive, "T", Convert.ToInt32(ddlActivefun.SelectedValue));

            }
            else if (ViewState["inactivedatakey"] != null)//Condition for inactive case
            {
                TabadminButtonMaster.ActiveTabIndex = 2;
                ddlinactivefun.SelectedValue = strddlinactivefunval;
                strddlActivefunval = null;
                FillGridview(GridinActivebutton, "I", Convert.ToInt32(ddlinactivefun.SelectedValue));
            }
            ViewState["intdatakey"] = null;
            ViewState["inactivedatakey"] = null;
            TabCreateButton.HeaderText = "Create";
            btnsave.Text = "Save";
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //**********************End**************************************   
    #endregion



    #region "Button Events"
    /// <summary>
    /// To Save Button 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnsave.Text.ToLower().Trim() == "save")
            {
                AddButtonMaster();
            }
            else
            {
                EditButtonMaster();
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            objbuttonmaster = null;
        }
    }

    ///<Summary>
    ///Modify Button
    /// 
    /// </Summary>


    /// <summary>
    /// To reset Button 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.Text == "Reset")
        {
            ResetControls();
        }
        else
        {
            TabadminButtonMaster.ActiveTabIndex = 1;
            ddlActivefun.SelectedValue = ddlselFun.SelectedValue;
            GridBtnActive.Visible = true;
            GridBtnActive.PageIndex = Convert.ToInt32(Request.QueryString["pindex"]);
            FillGridview(GridBtnActive, "T", Convert.ToInt32(ddlActivefun.SelectedValue));
            if (GridBtnActive.Rows.Count > 0)
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
            //Code added by Dilip Kumar Tripathy on dated 07-May-2013
            btnReset.Text = "Reset";
            AdminConsoleNavigation.strNewLink = ">>" + "ACTIVE";
        }
    }
    public void ResetControls()
    {
        TabCreateButton.HeaderText = "CREATE";
        btnsave.Text = "Save";
        ddlselFun.SelectedIndex = 0;
        txtBtnname.Text = string.Empty;
        txtBtnnameOL.Text = string.Empty;
        txtFilename.Text = string.Empty;
        txtDesc.Text = string.Empty;
        lblSlno.Text = "***";
        rbtTabyes.Checked = true;
        rbtTabNo.Checked = false;
        rbtnYes.Checked = true;
        rbtnNo.Checked = false;
        chkAdd.Checked = false;
        chkMng.Checked = false;
        chkView.Checked = false;
        cbReCXml.Checked = false;
    }
    /// <summary>
    /// To show Active buttons.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnActiveView_Click(object sender, EventArgs e)
    {
        LnkbtnAllin.Text = "All";
        FillGridview(GridBtnActive, "T", Convert.ToInt32(ddlActivefun.SelectedValue));
        if (GridBtnActive.Rows.Count > 0)
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
    /// <summary>
    /// To inactivate button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btninActive_Click(object sender, EventArgs e)
    {
        for (int i = 0; i <= GridBtnActive.Rows.Count - 1; i++)
        {
            CheckBox chk = new CheckBox();
            chk = (CheckBox)GridBtnActive.Rows[i].FindControl("cbItem");
            if (chk.Checked == true)
            {
                intdatakey = Convert.ToInt32(GridBtnActive.DataKeys[i].Value);
                // Code to Inactive Button
                objbuttonmaster.ActionCode = "I";
                objbuttonmaster.ButtonId = intdatakey.ToString();
                objbuttonmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                intreturnval = ObjAdminBal.InActiveButton(objbuttonmaster);
            }
        }
        if (intreturnval == 7)
        {
            if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
            {
                Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
            }
            ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Button") + "');", true);
        }
        FillGridview(GridBtnActive, "T", Convert.ToInt32(ddlActivefun.SelectedValue));
        if (GridBtnActive.Rows.Count > 0)
        {
            btninActive.Visible = true;
            btnmodify.Visible = true;
        }
        else
        {
            btninActive.Visible = false;
            btnmodify.Visible = false;
        }
        TabadminButtonMaster.ActiveTabIndex = 1;//Added By Biswaranjan on 10-Nov-2010
    }
    /// <summary>
    /// To Activate buttons.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnActive_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < GridinActivebutton.Rows.Count; i++)
            {
                CheckBox chk = new CheckBox();
                chk = (CheckBox)GridinActivebutton.Rows[i].FindControl("cbItem");
                if (chk.Checked == true)
                {

                    intdatakey = Convert.ToInt32(GridinActivebutton.DataKeys[i].Value);
                    // Code to active Button
                    objbuttonmaster.ActionCode = "T";
                    objbuttonmaster.ButtonId = intdatakey.ToString();
                    objbuttonmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    intreturnval = ObjAdminBal.InActiveButton(objbuttonmaster);
                }
            }
            if (intreturnval == 6)
            {
                if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
                {
                    Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                    CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
                }
                string strUrl = "adminbuttonmaster.aspx?funcId=" + ddlinactivefun.SelectedValue;
                ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Button") + "');document.location.href='" + strUrl + "';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Button") + "');", true);
            }
            //ddlActivefun.SelectedValue = ddlinactivefun.SelectedValue;
            //FillGridview(GridBtnActive, "T", Convert.ToInt32(ddlActivefun.SelectedValue));
            //if (GridBtnActive.Rows.Count > 0)
            //{
            //    btninActive.Visible = true;
            //    btnmodify.Visible = true;
            //}
            //else
            //{
            //    btninActive.Visible = false;
            //    btnmodify.Visible = false;
            //}
            //TabadminButtonMaster.ActiveTabIndex = 1;//Added By Biswaranjan on 10-Nov-2010
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            objbuttonmaster = null;
        }
        AdminConsoleNavigation.strNewLink = ">>" + "ACTIVE";
    }
    /// <summary>
    /// To show Iactive Button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btninactiveview_Click(object sender, EventArgs e)
    {
        FillGridview(GridinActivebutton, "I", Convert.ToInt32(ddlinactivefun.SelectedValue));
        if (GridinActivebutton.Rows.Count > 0)
        {
            btnActive.Visible = true;
            btnmodify.Visible = true;
        }
        else
        {
            btnActive.Visible = false;
            btnmodify.Visible = false;
        }
    }
    #endregion

    #region "Dropdown Events"
    /// <summary>
    /// To fill Sl no.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlselFun_SelectedIndexChanged(object sender, EventArgs e)
    {
        objbuttonmaster.ActionCode = "F";
        objbuttonmaster.ButtonId = "0";
        objbuttonmaster.FunctionID = Convert.ToInt32(ddlselFun.SelectedValue);
        lblSlno.Text = ObjAdminBal.ButtonFillSLno(objbuttonmaster);
        objbuttonmaster = null;
    }
    //*********************Added By Biswaranjan on10-Nov-2010*********************
    //************************Begin***********************************************
    protected void ddlinactivefun_SelectedIndexChanged(object sender, EventArgs e)
    {
        strddlinactivefunval = ddlinactivefun.SelectedValue;
    }
    protected void ddlActivefun_SelectedIndexChanged(object sender, EventArgs e)
    {
        strddlActivefunval = ddlActivefun.SelectedValue;
    }
    //************************End***********************************************
    #endregion
    protected void TabadminButtonMaster_ActiveTabChanged(object sender, EventArgs e)
    {
        ddlselFun.SelectedIndex = 0;
        txtBtnname.Text = "";
        txtFilename.Text = "";
        lblSlno.Text = "***";
        //*********Added By Biswaranjan on 10-Nov-2010*********
        //*************************Begin***********************
        txtDesc.Text = "";
        ddlActivefun.SelectedIndex = 0;
        ddlinactivefun.SelectedIndex = 0;
        ddlselFun.SelectedIndex = 0;
        GridBtnActive.Visible = false;
        GridinActivebutton.Visible = false;
        btnActive.Visible = false;
        btninActive.Visible = false;
        btnmodify.Visible = false;
        btnsave.Text = "Save";
        //btnCancel.Visible = false;
        btnReset.Visible = true;
        //*************************Begin***********************
        rbtnYes.Checked = true;
        rbtnNo.Checked = false;
        rbtTabyes.Checked = true;
        rbtTabNo.Checked = false;
        chkAdd.Checked = false;
        chkMng.Checked = false;
        chkView.Checked = false;
        TabCreateButton.HeaderText = "CREATE";
        this.lblPaging.Visible = false;
        lbtnAll.Visible = false;
        this.lblpage.Visible = false;
        LnkbtnAllin.Visible = false;
        LnkbtnAllin.Text = "All";
        lbtnAll.Text = "All";
        GridBtnActive.AllowPaging = true;
        GridinActivebutton.AllowPaging = true;

        if (TabadminButtonMaster.ActiveTabIndex == 0)
            btnsave.Focus();
        else if (TabadminButtonMaster.ActiveTabIndex == 1)
        {
            btnActiveView.Focus();
        }
        else if (TabadminButtonMaster.ActiveTabIndex == 2)
            btninactiveview.Focus();
    }
    //Modified By: Dilip Kumar Tripathy
    //Modified Date :14-Mar-2012
    //Purpose : To make paging for two types of gridview
    private void DisplayPaging(GridView gridviewone, int count, string status)
    {
        if (gridviewone.Rows.Count > 0)
        {
            if (status == "Active")
            {
                this.lblpage.Visible = true;
                LnkbtnAllin.Visible = true;
                this.lblpage.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(gridviewone, gridviewone.PageSize, gridviewone.PageIndex, count);
            }

            else
            {
                this.lblPaging.Visible = true;
                lbtnAll.Visible = true;
                this.lblPaging.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(gridviewone, gridviewone.PageSize, gridviewone.PageIndex, count);
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
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            this.GridinActivebutton.PageIndex = 0;
            GridinActivebutton.AllowPaging = false;
            FillGridview(GridinActivebutton, "I", Convert.ToInt32(ddlinactivefun.SelectedValue));

            if (GridinActivebutton.Rows.Count > 0)
            {
                this.lblPaging.Text = "1-" + GridinActivebutton.Rows.Count.ToString() + " Of " + GridinActivebutton.Rows.Count.ToString();
            }
        }
        else
        {
            lbtnAll.Text = "All";
            GridinActivebutton.AllowPaging = true;
            FillGridview(GridinActivebutton, "I", Convert.ToInt32(ddlinactivefun.SelectedValue));
        }
    }


    protected void LnkbtnAllin_Click(object sender, EventArgs e)
    {
        if (LnkbtnAllin.Text == "All")
        {
            LnkbtnAllin.Text = "Paging";
            this.GridBtnActive.PageIndex = 0;
            GridBtnActive.AllowPaging = false;
            FillGridview(GridBtnActive, "T", Convert.ToInt32(ddlActivefun.SelectedValue));
            if (GridBtnActive.Rows.Count > 0)
            {
                this.lblpage.Text = "1-" + GridBtnActive.Rows.Count.ToString() + " Of " + GridBtnActive.Rows.Count.ToString();
            }
        }
        else
        {
            LnkbtnAllin.Text = "All";
            GridBtnActive.AllowPaging = true;
            FillGridview(GridBtnActive, "T", Convert.ToInt32(ddlActivefun.SelectedValue));

        }
    }

    protected void btnmodify_click(object sender, EventArgs e)
    {
        int intOutput = 0;
        for (int i = 0; i < GridBtnActive.Rows.Count; i++)
        {
            int funcid = 0;
            CheckBox chkButton = (CheckBox)GridBtnActive.Rows[i].FindControl("cbItem");
            System.Web.UI.WebControls.TextBox txtButtonSlno = (System.Web.UI.WebControls.TextBox)GridBtnActive.Rows[i].FindControl("txtslno");

            if (chkButton.Checked == true)
            {
                objbuttonmaster.ActionCode = "S";
                objbuttonmaster.FunctionID = Convert.ToInt32(GridBtnActive.DataKeys[i].Value);
                IList<ACButton> objbtnlst = ObjAdminBal.GetAllButton(objbuttonmaster);
                foreach (ACButton data in objbtnlst)
                {
                    funcid = data.FunctionID;
                }
                objbuttonmaster.ButtonId = Convert.ToString(GridBtnActive.DataKeys[i].Value);
                objbuttonmaster.FunctionID = funcid;
                objbuttonmaster.ShotNum = Convert.ToInt32(txtButtonSlno.Text);
                objbuttonmaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                intOutput = ObjAdminBal.UdpdateSLNO(objbuttonmaster);
            }
        }

        if (intreturnval == 9)
        {
            if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
            {
                Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
            }
            string strOutmsg = StaticValues.message(intOutput, "Button(s)");
            ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + strOutmsg + "');", true);
        }
        else
        {
            string strOutmsg = StaticValues.message(intOutput, "Button(s)");
            ScriptManager.RegisterStartupScript(this, typeof(string), "", "alert('" + strOutmsg + "');", true);
        }
        FillGridview(GridBtnActive, "T", Convert.ToInt32(ddlActivefun.SelectedValue));
    }

    protected void GridBtnActive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (StrOL == "")
            {
                GridBtnActive.Columns[3].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strBtnId = GridBtnActive.DataKeys[e.Row.RowIndex].Value.ToString();
                HyperLink hypEdit = (HyperLink)e.Row.FindControl("Hypedit");
                hypEdit.NavigateUrl = "adminbuttonmaster.aspx?btnid=" + CommonFunction.EncryptData(strBtnId) + "&pindex=" + GridBtnActive.PageIndex;
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].Text = "Button Name(In " + StrOL + ")";

            }
        }
        catch (Exception e1)
        {
            throw new Exception(e1.Message, e1);
        }
    }
    protected void GridBtnInActive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (StrOL == "")
            {
                GridinActivebutton.Columns[3].Visible = false;
            }
           
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].Text = "Button Name(In " + StrOL + ")";

            }
        }
        catch (Exception e1)
        {
            throw new Exception(e1.Message, e1);
        }
    }
   

    protected void GridBtnActive_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridBtnActive.PageIndex = e.NewPageIndex;
        btnActiveView_Click(sender, e);
    }

    protected void GridinActivebutton_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridinActivebutton.PageIndex = e.NewPageIndex;
        btninactiveview_Click(sender, e);

    }

}
