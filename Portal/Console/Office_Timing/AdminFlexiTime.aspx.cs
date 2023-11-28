using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
//using CSMPDK_3_0;
using KWAdminConsole.Messages;
using Manage_Usercontrol_Property;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;

public partial class Admin_Office_Timing_AdminFlexiTime : System.Web.UI.Page
{
    //CommonDLL objcmndll = new CommonDLL();
    /// <summary>
    /// Declaring Variables
    /// </summary>
    AdminAppService ObjAdminBal = new AdminAppService();
    OT objFlexiTime = new OT();
    int returnvalue = 0;
    string strmsg;
    public string strbtntext;

    public static string strDept = null;
    public static string strDeptid = null;
    public static int intUser = 0;
    public string strHidvalue = null;
    public static int intval = 0;
    IList<OT> lstFlexiTime;
    AdminApp.Model.UserHierarchyControl objHierUserCtrl = new UserHierarchyControl();
    IList<AdminApp.Model.UserHierarchyControl> listHierarchy = null;
    PopHierarchy objPop = new PopHierarchy();
    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        // Code added by Dilip Tripathy on dated 25-June-2012
        //To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                        

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }
        if (Request.QueryString["att"] != null)
        {
            string strAtt = CommonFunction.DecryptData(Request.QueryString["att"].ToString());
        }


        GroupMasterProperties.hidlstid = "";
        GroupMasterProperties.hidbtnid = "";
        GroupMasterProperties.hidnval = "";
        CommonProperties.Type = "1";
        AssignAdminProperties.hidadmin = "";
        CommonProperties.HierachyId = 0;
        CommonProperties.LId = 1;
        CommonProperties.PageUrl = "AdminFlexiTime.aspx";
        CommonProperties.UserControlId = null;
        CommonProperties.UserControlId2 = ddlUser.ClientID;
        GroupMasterProperties.hidlstid = "";
        GroupMasterProperties.hidbtnid = "";
        GroupMasterProperties.hidnval = "";
        Session["PosId"] = null;
        Session["hidLevel"] = null;
        ((HiddenField)getuser1.FindControl("hidadmin")).Value = "";
        ((HiddenField)getuser1.FindControl("hidType")).Value = "1";
        if (Request.QueryString["UID"] != null && Request.QueryString["UID"] != "")
        {
            FillPhysicalLocation(Convert.ToInt32(Request.QueryString["UID"]));
        }
        if (Request.QueryString["DptId"] != null)
        {
            filluser(Convert.ToInt32(Request.QueryString["DptId"]));
        }
        txtStartTime.Attributes.Add("readonly", "readonly");
        txtEndTime.Attributes.Add("readonly", "readonly");
        txtEarlyETime.Attributes.Add("readonly", "readonly");
        txtNLateEnd.Attributes.Add("readonly", "readonly");
        txtRecessFrom.Attributes.Add("readonly", "readonly");
        txtRecessTo.Attributes.Add("readonly", "readonly");
        txtLateETime.Attributes.Add("readonly", "readonly");
        txtDateTo.Attributes.Add("readonly", "readonly");
        txtHalfETime.Attributes.Add("readonly", "readonly");
        txtHalfLateETime.Attributes.Add("readonly", "readonly");
        txtExLateExitTime.Attributes.Add("readonly", "readonly");
        txtHfStartTime.Attributes.Add("readonly", "readonly");
        txtHfEETime.Attributes.Add("readonly", "readonly");
        txtHfLETime.Attributes.Add("readonly", "readonly");

        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabFlexiTime.ActiveTab.HeaderText;
        string strDate = DateTime.Now.ToString("dd-MMM-yyyy");
        txtDateFrom.Text = "01" + strDate.Substring(2, strDate.Length - 2);//Code added by Dilip Tripathy on dated 10-May-2013
        txtDateFrom.Attributes.Add("readonly", "readonly");

        if (!IsPostBack)
        {
            showActiveGrid();
            if (Request.QueryString["FID"] != null)
            {
                CommonProperties.Action = "U";
                CommonProperties.Type = "U";
                CommonProperties.UserControlId = ddlUser.ClientID;
                FillFlexiTime();
                TabCreateFlexiTime.HeaderText = "UPDATE";
                TabFlexiTime.ActiveTabIndex = 0;
                BtnAdd.Text = "Update";
                strbtntext = BtnAdd.Text;
                btnReset.Text = "Cancel";
                AdminConsoleNavigation.strNewLink = ">>" + "UPDATE";
            }
            else
            {
                CommonProperties.Action = "A";
                TabCreateFlexiTime.HeaderText = "CREATE";
                BtnAdd.Text = "Save";
                strbtntext = BtnAdd.Text;
                btnReset.Text = "Reset";
            }
            BtnAdd.Focus();
        }
        //   ddlUser.Attributes.Add("onchange", "return SetPhysicalLocation('" + ddlUser.ClientID + "','AdminFlexiTime.aspx?UID=','" + trPhyLoc.ClientID + "','" + lblPhylocation.ClientID + "');");

    }
    #endregion
    #region "User Functions"
    /// <summary>
    /// To Add the flexi Time.
    /// </summary>
    protected void AddFlexiTime()
    {
        objFlexiTime.ActionCode = "A";
        objFlexiTime.OffTimeID = 0;

        objFlexiTime.UserID = Convert.ToInt32(hiduserid.Value);
        //Date From and Date To
        objFlexiTime.DateFrom = Convert.ToDateTime(txtDateFrom.Text);
        objFlexiTime.DateTo = Convert.ToDateTime(txtDateTo.Text);
        //Start time
        objFlexiTime.StartTime = Convert.ToString(txtStartTime.Text.Trim());
        //grace time
        objFlexiTime.GraceTime = Convert.ToDecimal(ddlgracetime.SelectedItem.Text);
        //Recess From time
        objFlexiTime.RecessFrom = Convert.ToString(txtRecessFrom.Text.Trim());
        //Recess End time
        objFlexiTime.RecessTo = Convert.ToString(txtRecessTo.Text.Trim());
        //End Time
        objFlexiTime.EndTime = Convert.ToString(txtEndTime.Text.Trim());
        //late exit time
        objFlexiTime.LateEndTime = Convert.ToString(txtNLateEnd.Text.Trim());
        //early Entry Time
        objFlexiTime.EarlyESTime = Convert.ToString(txtEarlyETime.Text.Trim());
        //Late Entry time
        objFlexiTime.LateEETime = Convert.ToString(txtLateETime.Text.Trim());
        //extra late exit time
        objFlexiTime.ExtraLESTime = Convert.ToString(txtExLateExitTime.Text.Trim());
        //Weekly halfday
        if (cbWeekHalf.Checked == true)
        {
            objFlexiTime.HalfEndTime = Convert.ToString(txtHalfETime.Text.Trim());
            objFlexiTime.HalfLExitTime = Convert.ToString(txtHalfLateETime.Text.Trim());
            objFlexiTime.HalfStartTime = Convert.ToString(txtHfStartTime.Text.Trim());
            objFlexiTime.HalfEETime = Convert.ToString(txtHfEETime.Text.Trim());
            objFlexiTime.HalfLETime = Convert.ToString(txtHfLETime.Text.Trim());
        }
        else
        {
            objFlexiTime.HalfEndTime = "";
            objFlexiTime.HalfLExitTime = "";
            objFlexiTime.HalfStartTime = "";
            objFlexiTime.HalfEETime = "";
            objFlexiTime.HalfLETime = "";
        }
        //Time Zone
        objFlexiTime.TimeZone = 0;
        objFlexiTime.CreatedBy = Convert.ToInt32(Session["UserId"]);

        returnvalue = Convert.ToInt32(ObjAdminBal.AddFlexiTiming(objFlexiTime));
        strmsg = StaticValues.message(returnvalue, "Flexi Time");
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');document.location.href='AdminFlexiTime.aspx';", true);

    }
    /// <summary>
    /// To Update Flexi Time.
    /// </summary>
    protected void EditFlexiTime()
    {
        objFlexiTime.ActionCode = "U";
        objFlexiTime.OffTimeID = Convert.ToInt32(Request.QueryString["FID"]);
        objFlexiTime.LocationID = 0;
        objFlexiTime.UserID = Convert.ToInt32(ddlUser.SelectedValue);
        //Date From and Date To
        objFlexiTime.DateFrom = Convert.ToDateTime(txtDateFrom.Text);
        objFlexiTime.DateTo = Convert.ToDateTime(txtDateTo.Text);
        //Start time
        objFlexiTime.StartTime = Convert.ToString(txtStartTime.Text.Trim());
        //grace time
        objFlexiTime.GraceTime = Convert.ToDecimal(ddlgracetime.SelectedItem.Text);
        //Recess From time
        objFlexiTime.RecessFrom = Convert.ToString(txtRecessFrom.Text.Trim());
        //Recess End time
        objFlexiTime.RecessTo = Convert.ToString(txtRecessTo.Text.Trim());
        //End Time
        objFlexiTime.EndTime = Convert.ToString(txtEndTime.Text.Trim());
        //late exit time
        objFlexiTime.LateEndTime = Convert.ToString(txtNLateEnd.Text.Trim());
        //early Entry Time
        objFlexiTime.EarlyESTime = Convert.ToString(txtEarlyETime.Text.Trim());
        //Late Entry time
        objFlexiTime.LateEETime = Convert.ToString(txtLateETime.Text.Trim());
        //extra late exit time
        objFlexiTime.ExtraLESTime = Convert.ToString(txtExLateExitTime.Text.Trim());
        //Weekly halfday
        if (cbWeekHalf.Checked == true)
        {
            objFlexiTime.HalfEndTime = Convert.ToString(txtHalfETime.Text.Trim());
            objFlexiTime.HalfLExitTime = Convert.ToString(txtHalfLateETime.Text.Trim());
            objFlexiTime.HalfStartTime = Convert.ToString(txtHfStartTime.Text.Trim());
            objFlexiTime.HalfEETime = Convert.ToString(txtHfEETime.Text.Trim());
            objFlexiTime.HalfLETime = Convert.ToString(txtHfLETime.Text.Trim());
        }
        else
        {
            objFlexiTime.HalfEndTime = "";
            objFlexiTime.HalfLExitTime = "";
            objFlexiTime.HalfStartTime = "";
            objFlexiTime.HalfEETime = "";
            objFlexiTime.HalfLETime = "";
        }
        //Time Zone
        objFlexiTime.TimeZone = 0;
        objFlexiTime.CreatedBy = Convert.ToInt32(Session["UserId"]); ;

        returnvalue = Convert.ToInt32(ObjAdminBal.UpdateFlexiTiming(objFlexiTime));
        strmsg = StaticValues.message(returnvalue, "Flexi Time");
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');document.location.href='AdminFlexiTime.aspx';", true);
    }
    /// <summary>
    /// To Delete Flexi Time.
    /// </summary>
    protected void DeleteFlexiTime()
    {
        try
        {
            int rcount = GVFlexiTime.Rows.Count;
            int strdata;
            for (int i = 0; i < rcount; i++)
            {
                if (((CheckBox)GVFlexiTime.Rows[i].Cells[0].FindControl("cbItem")).Checked == true)
                {
                    strdata = Convert.ToInt32(GVFlexiTime.DataKeys[i].Values[0].ToString());
                    objFlexiTime.ActionCode = "D";
                    objFlexiTime.OffTimeID = strdata;
                    objFlexiTime.CreatedBy = Convert.ToInt32(Session["UserId"]);
                    returnvalue = Convert.ToInt32(ObjAdminBal.DeleteFlexiTiming(objFlexiTime));

                }
            }
            strmsg = StaticValues.message(returnvalue, "Flexi Time");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');", true);
            TabFlexiTime.ActiveTabIndex = 1;//Added By Biswaranjan on 12-Nov-2010
            //FillFlexiTime();
            showActiveGrid();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// To show the Gridview of Active Flexi Time user.
    /// </summary>
    protected void showActiveGrid()
    {
        objFlexiTime.ActionCode = "V";
        lstFlexiTime = ObjAdminBal.GetAllFlexiTiming(objFlexiTime);
        GVFlexiTime.DataSource = lstFlexiTime;
        GVFlexiTime.DataBind();
        if (GVFlexiTime.Rows.Count > 0)
        {
            btndelete.Visible = true;
            DisplayPaging(GVFlexiTime, lstFlexiTime.Count);
        }
        else
        {
            btndelete.Visible = false;
        }
    }
    private void DisplayPaging(GridView gridToDisplay, int totalRowCount)
    {
        if (gridToDisplay.Rows.Count > 0)
        {

            this.lblpage.Visible = true;
            LnkbtnAllin.Visible = true;
            lblpage.Text = CommonFunction.ShowGridPaging(gridToDisplay, gridToDisplay.PageSize, gridToDisplay.PageIndex, totalRowCount);

        }
        else
        {
            this.lblpage.Visible = false;
            LnkbtnAllin.Visible = false;
        }
    }
    //*******************Summery****************************
    //Function Name             : FillPhysicalLocation()
    //Purpose                   : To Show the physical location for a concern user
    //InPut Parameters Name     : intUserid
    //InPut Parameters DataType : Int
    //OutPut Parameters Name    : None
    //OutPut Parameters DataType: None
    //Retun  Value              : None
    //Retun Datatype            : None
    //Created By                : Biswaranjan Das
    //Created Date              : 2-Nov-2010
    //*****************************************************
    protected void FillPhysicalLocation(int intUserid)
    {
        try
        {
            string strVal="";
            IList<Location> objLoc = new List<Location>();
            objLoc = ObjAdminBal.GetAllPhysicalLocation("F", intUserid);
            foreach (Location objPLoc in objLoc)
            {
                strVal = objPLoc.PhysicalLocationName;
               
            }
            Context.Response.Write(strVal);
            Response.End();
            return;
        }
        catch
        {
        }
    }
    #endregion
    protected void filluser(int val)
    {
        try
        {
            objHierUserCtrl.LevelDetailId = val;
            objHierUserCtrl.StatusFlag = 1;
            listHierarchy = ObjAdminBal.FillUser1(objHierUserCtrl);
            ddlUser.DataSource = listHierarchy;
            ddlUser.DataValueField = "UserId";
            ddlUser.DataTextField = "UserName";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }


    }

    public string getLevel(int intLvlDtlId)
    {
        string strNoOfLevel = "";
        IList<User> objLoc = new List<User>();
        objLoc = ObjAdminBal.UserGetLevel(intLvlDtlId.ToString());
        foreach (User objPU in objLoc)
            {
                strNoOfLevel = objPU.GradeId.ToString();
               
            }
        return strNoOfLevel;
    }

    protected string getPosId(int intLvlDtlId)
    {
        string strPosId = "";
        objPop.LocationId = Convert.ToInt32(intLvlDtlId.ToString());
        strPosId = ObjAdminBal.GetPOSId(objPop).ToString();
        return strPosId;
    }




    //*******************Summery****************************
    //Function Name             : DepartmentOfUser()
    //Purpose                   : To Get the department id form the last dropdownlist chnage event  of FillUserHierarchy usercontrol
    //InPut Parameters Name     : None
    //InPut Parameters DataType : None
    //OutPut Parameters Name    : None
    //OutPut Parameters DataType: None
    //Retun  Value              : None
    //Retun Datatype            : None
    //Created By                : Biswaranjan Das
    //Created Date              : 29-OCT-2010
    //*****************************************************
    private string DepartmentOfUser()
    {
        string strDept = "";
        if (((DropDownList)getuser1.FindControl("sdrplayers0")).SelectedIndex >= 0)
        {
            int intLayer;
            intLayer = Convert.ToInt32(((HiddenField)getuser1.FindControl("shidLevels")).Value);
            HiddenField hdnDeptOfUser = null;

            for (int intCounter = 0; intCounter < intLayer; intCounter++)
            {
                hdnDeptOfUser = (HiddenField)getuser1.FindControl("shidIDs" + Convert.ToString(intCounter));
                if (hdnDeptOfUser.Value != "" && hdnDeptOfUser.Value != "0")
                {
                    strDept = hdnDeptOfUser.Value;
                }
            }
        }

        return strDept;
    }

    //*******************Summery****************************
    //Function Name             : FillControlAfterPostback()
    //Purpose                   : To Fill all the dropdownlist of usercontrol after postback
    //InPut Parameters Name     : None
    //InPut Parameters DataType : None
    //OutPut Parameters Name    : None
    //OutPut Parameters DataType: None
    //Retun  Value              : None
    //Retun Datatype            : None
    //Created By                : Biswaranjan Das
    //Created Date              : 29-OCT-2010
    //*****************************************************
    public void FillControlAfterPostback()
    {

        try
        {
            strDeptid = DepartmentOfUser();
            SetpermissionProperties.hidLevels = ((HiddenField)getuser1.FindControl("shidLevels")).ClientID;
            CommonProperties.PIds = Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(strDeptid))) - 2;
            DropDownList ddlLayer1 = ((DropDownList)getuser1.FindControl("sdrplayers0"));
            HiddenField hidId1 = (HiddenField)getuser1.FindControl("shidIDs0");
            int IntParId = Convert.ToInt32(strDeptid);
            for (int intCounter = 0; intCounter < Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(strDeptid))); intCounter++)
            {
                if (intCounter == 0)
                {
                    DropDownList ddlTofill = (DropDownList)getuser1.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(strDeptid))) - 1));
                    HiddenField hidId = (HiddenField)getuser1.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(strDeptid))) - 1));
                    Label lblId = (Label)getuser1.FindControl("Labels" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(strDeptid)))));
                    CommonFunction.FillControls(Convert.ToInt32(strDeptid), ddlTofill, hidId, lblId, ddlLayer1, hidId1);
                }
                else
                {
                    DropDownList ddlTofill = (DropDownList)getuser1.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(IntParId)) - 1));
                    HiddenField hidId = (HiddenField)getuser1.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(IntParId)) - 1));
                    Label lblId = (Label)getuser1.FindControl("Labels" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(IntParId))));
                    CommonFunction.FillControls(IntParId, ddlTofill, hidId, lblId, ddlLayer1, hidId1);
                }
                IntParId = Convert.ToInt32(ObjAdminBal.GetParentId(IntParId));
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void FillControls(int intDepartmentId)
    {
        if (Convert.ToInt32(ObjAdminBal.GetParentId(intDepartmentId)) != 0)
        {

            DropDownList ddlTofill = (DropDownList)getuser1.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId)) - 1));
            HiddenField hidId = (HiddenField)getuser1.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId)) - 1));

            CommonFunction.fillDropdown(Convert.ToInt32(ObjAdminBal.GetParentId(intDepartmentId)), ddlTofill);
            ddlTofill.SelectedValue = Convert.ToString(intDepartmentId);
            hidId.Value = Convert.ToString(intDepartmentId);
            Label lblId = (Label)getuser1.FindControl("Labels" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId))));
            lblId.Text = getLevelNames(intDepartmentId);
        }
        else
        {
            IList<User> lstUser = ObjAdminBal.PopUpDropDown8();
            ((DropDownList)getuser1.FindControl("sdrplayers0")).DataSource = lstUser;
            ((DropDownList)getuser1.FindControl("sdrplayers0")).DataTextField = "LevelName";
            ((DropDownList)getuser1.FindControl("sdrplayers0")).DataValueField = "LevelId";
            ((DropDownList)getuser1.FindControl("sdrplayers0")).DataBind();
            ((DropDownList)getuser1.FindControl("sdrplayers0")).SelectedValue = Convert.ToString(intDepartmentId);
            HiddenField hidId = (HiddenField)getuser1.FindControl("shidIDs0");
            hidId.Value = Convert.ToString(intDepartmentId);
        }

    }



    protected string getLevelNames(int intDepartmentId)
    {
        string strLvlName = null;
        IList<User> objLoc = new List<User>();
        objLoc = ObjAdminBal.PopUpDropDown9(intDepartmentId.ToString());
        foreach (User objPU in objLoc)
        {
            strLvlName = objPU.LevelName;

        }
        return strLvlName;
    }



    #region "Button Events"
    /// <summary>
    /// To Add/Update Flexi time
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["FID"] != null)
        {
            EditFlexiTime();
        }
        else
        {
            AddFlexiTime();
        }
    }
    /// <summary>
    /// To Delete Flexi time
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btndelete_Click(object sender, EventArgs e)
    {
        DeleteFlexiTime();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.Text.Trim().ToLower() == "cancel")
        {
            TabFlexiTime.ActiveTabIndex = 1;
            TabCreateFlexiTime.HeaderText = "CREATE";
            btnReset.Text = "Reset";
            BtnAdd.Text = "Save";
            showActiveGrid();
        }
        else
        {
            ResetUsercontrolddl(getuser1);
            ddlgracetime.SelectedValue = "0";
            cbWeekHalf.Checked = false;
            ddlUser.SelectedIndex = 0;
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            txtEarlyETime.Text = "";
            txtEndTime.Text = "";
            txtExLateExitTime.Text = "";
            txtHalfETime.Text = "";
            txtHalfLateETime.Text = "";
            txtLateETime.Text = "";
            txtNLateEnd.Text = "";
            txtRecessFrom.Text = "";
            txtStartTime.Text = "";
            txtRecessTo.Text = "";
            txtHfEETime.Text = "";
            txtHfLETime.Text = "";
            txtHfStartTime.Text = "";
        }
    }
    #endregion

    #region "Gridview Events"
    /// <summary>
    /// To Show The Sl No of gridview.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GVFlexiTime_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Convert.ToString((GVFlexiTime.PageSize * GVFlexiTime.PageIndex) + (e.Row.RowIndex + 1));
        }
    }
    /// <summary>
    /// To Merge the header of gridview.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GVFlexiTime_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);//1
            //HeaderCell.Style.Add("border-bottom-color", "#2d4e9f");
            HeaderCell.Style.Add("background-color", "#e6e6e6");
            HeaderCell.Style.Add("border-right-color", "#cccccc");

            HeaderCell = new TableCell();
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);//2
            HeaderCell.Style.Add("background-color", "#e6e6e6");
            HeaderCell.Style.Add("border-right-color", "#cccccc");

            HeaderCell = new TableCell();
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);//3
            HeaderCell.Style.Add("background-color", "#e6e6e6");
            HeaderCell.Style.Add("border-right-color", "#cccccc");

            HeaderCell = new TableCell();
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);//4
            HeaderCell.Style.Add("background-color", "#e6e6e6");
            HeaderCell.Style.Add("border-right-color", "#cccccc");

            HeaderCell = new TableCell();
            HeaderCell.Text = "Flexi Period";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.ForeColor = Color.Black;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);//5
            HeaderCell.Style.Add("border-bottom-color", "#cccccc");
            HeaderCell.Style.Add("background-color", "#e6e6e6");
            HeaderCell.Style.Add("border-right-color", "#cccccc");

            HeaderCell = new TableCell();
            HeaderCell.Text = "Office Time";
            HeaderCell.ColumnSpan = 2;
            HeaderCell.ForeColor = Color.Black;
            HeaderCell.Font.Bold = true;
            HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            HeaderGridRow.Cells.Add(HeaderCell);//6
            HeaderCell.Style.Add("border-bottom-color", "#cccccc");
            HeaderCell.Style.Add("background-color", "#e6e6e6");
            HeaderCell.Style.Add("border-right-color", "#cccccc");

            HeaderCell = new TableCell();
            HeaderCell.ColumnSpan = 1;
            HeaderGridRow.Cells.Add(HeaderCell);
            HeaderCell.Style.Add("background-color", "#e6e6e6");
            HeaderCell.Style.Add("border-right-color", "#cccccc");

            GVFlexiTime.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    #endregion
    private void ResetUsercontrolddl(Control UsercontrolToReset)
    {
        try
        {
            CommonProperties.PIds = 0;
            SetpermissionProperties.hidLevels = "";
            DropDownList ddlLayer1 = ((DropDownList)UsercontrolToReset.FindControl("sdrplayers0"));
            ddlLayer1.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    #region "Fill/Show Functions"
    /// <summary>
    /// To Fill the flexi time information on clicking the Edit button.
    /// </summary>
    protected void FillFlexiTime()
    {
        try
        {
            IList<OT> objFlexiTimeList = new List<OT>();
            objFlexiTime.ActionCode = "E";
            objFlexiTime.OffTimeID = Convert.ToInt32(Request.QueryString["FID"]);
            objFlexiTimeList = ObjAdminBal.GetAllFlexiTiming(objFlexiTime);
            foreach (var i in objFlexiTimeList)
            {
                objFlexiTime.OffTimeID = i.OffTimeID;
                filluser(i.LocationID);
                ddlUser.ClearSelection();
                ddlUser.Items.FindByValue(Convert.ToString(i.UserID)).Selected = true;
                IList<Location> objLoc = new List<Location>();
                objLoc = ObjAdminBal.GetAllPhysicalLocation("F", i.UserID);
                foreach (Location objPLoc in objLoc)
                {
                    lblPhylocation.Text = objPLoc.PhysicalLocationName;

                }
                CommonProperties.HierachyId = i.HierarchyID;
                CommonProperties.PositionId = i.PosID;
                CommonProperties.DId = i.LocationID;
                txtDateFrom.Text = i.DateFrom.ToString("dd-MMM-yyy");
                txtDateTo.Text = i.DateTo.ToString("dd-MMM-yyy");
                txtStartTime.Text = i.StartTime.TrimStart();
                ddlgracetime.Items.FindByText(Convert.ToString(i.GraceTime)).Selected = true;
                txtRecessFrom.Text = i.RecessFrom.TrimStart();
                txtRecessTo.Text = i.RecessTo.TrimStart();
                txtEndTime.Text = i.EndTime.TrimStart();
                txtNLateEnd.Text = i.LateEndTime.TrimStart();
                txtEarlyETime.Text = i.EarlyESTime.TrimStart();
                txtLateETime.Text = i.LateEETime.TrimStart();
                txtExLateExitTime.Text = i.ExtraLESTime.TrimStart();

                txtHalfETime.Text = i.HalfEndTime.TrimStart();
                txtHalfLateETime.Text = i.HalfLExitTime.TrimStart();
                txtHfStartTime.Text = i.HalfStartTime.TrimStart();
                txtHfEETime.Text = i.HalfEETime.TrimStart();
                txtHfLETime.Text = i.HalfLETime.TrimStart();
                if (txtHalfETime.Text != "" && txtHalfLateETime.Text != "" && txtHfStartTime.Text != "" && txtHfEETime.Text != "" && txtHfLETime.Text != "")
                {
                    cbWeekHalf.Checked = true;
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "ShowHide();", true);
                }
                else
                {
                    cbWeekHalf.Checked = false;
                }
                ((HiddenField)getuser1.FindControl("shidLevels")).Value = getLevel(Convert.ToInt32(i.LocationID));
                Session["hidLevel"] = ((HiddenField)getuser1.FindControl("shidLevels")).ClientID;
                Session["PosId"] = Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(i.LocationID))) - 2;
                int IntParId = Convert.ToInt32(i.LocationID);
                for (int j = 0; j < Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(i.LocationID))); j++)
                {
                    if (j == 0)
                    {
                        FillControls(Convert.ToInt32(i.LocationID));
                    }
                    else
                    {
                        FillControls(IntParId);
                    }
                    IntParId = Convert.ToInt32(ObjAdminBal.GetParentId(IntParId));
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
    #region "Tab Events"
    protected void TabFlexiTime_ActiveTabChanged(object sender, EventArgs e)
    {
        SetpermissionProperties.hidLevels = null;//To Reset the Usercontorl 
        CommonProperties.PIds = 0;//To show only first hiearchy I.e location dropdownlist 
        //txtDateFrom.Text = "";
        txtDateTo.Text = "";
        txtEarlyETime.Text = "";
        txtEndTime.Text = "";
        txtExLateExitTime.Text = "";
        txtHalfETime.Text = "";
        txtHalfLateETime.Text = "";
        txtLateETime.Text = "";
        txtNLateEnd.Text = "";
        txtRecessFrom.Text = "";
        txtStartTime.Text = "";
        txtRecessTo.Text = "";
        BtnAdd.Text = "Save";
        btnReset.Text = "Reset";
        ddlgracetime.SelectedIndex = 0;
        ddlUser.SelectedIndex = 0;
        TabCreateFlexiTime.HeaderText = "CREATE";
    }
    #endregion

    protected void LnkbtnAllin_Click(object sender, EventArgs e)
    {
        if (LnkbtnAllin.Text == "All")
        {
            LnkbtnAllin.Text = "Paging";
            this.GVFlexiTime.PageIndex = 0;
            GVFlexiTime.AllowPaging = false;
            showActiveGrid();
            if (GVFlexiTime.Rows.Count > 0)
            {
                this.lblpage.Text = "1-" + GVFlexiTime.Rows.Count.ToString() + " Of " + GVFlexiTime.Rows.Count.ToString();
            }
        }
        else
        {
            LnkbtnAllin.Text = "All";
            GVFlexiTime.AllowPaging = true;
            showActiveGrid();
        }

    }
    protected void GVFlexiTime_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVFlexiTime.PageIndex = e.NewPageIndex;
        showActiveGrid();
    }
    [System.Web.Services.WebMethod]
    public static string GetUserPhysicalLoc(string strUserId)
    {
        AdminAppService ObjAdminBal = new AdminAppService();
        string strVal = "";
        IList<Location> objLoc = new List<Location>();
        objLoc = ObjAdminBal.GetLocationByUser(strUserId);
        foreach (Location objPLoc in objLoc)
        {
            strVal = objPLoc.PhysicalLocationName;

        }
        return strVal;
    }
}

