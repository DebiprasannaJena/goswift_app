/********************************************************************************************************************
' File Name             :   AdminOfficeTime.aspx.cs
' Description           :   To Add/edit Office Time.
' Created by            :   Subhasis Kumar dash
' Created On            :   04-Aug-2010
' Modification History  :
'                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
'                            1                             1-oct-2010         Biswaranjan                 Code to set the default button of the page                         
'                            2                             25-June-2012       Dilip Tripathy              To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                        
' Function Name         :   AddOfficeTime(),EditOfficeTime(),DeleteOfficeTime(),showActiveGrid()
' Procedures Used       :   [usp_OfficeTimeMaster_Manage],[usp_OfficeTimeMaster_View]
' User Defined Namespace:   
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
//using CSMPDK_3_0;
using KWAdminConsole.Messages;
using System.Drawing.Imaging;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
public partial class Admin_Office_Timing_AdminOfficeTime : System.Web.UI.Page
{
    //CommonDLL objcmndll = new CommonDLL();
    /// <summary>
    /// Declaring Variables
    /// </summary>
    AdminAppService ObjAdminBal = new AdminAppService();
    OT objOfficeTimeMaster = new OT();
    OT objExpOfficeTime = new OT();
    int returnvalue = 0;
    string strmsg;
    public string DtContrl;
    public string strNextFromdate, strNextTodate;
    public string strPreFromdate, strPreTodate;
    IList<OT> lstOfficeTime;
    IList<OT> lstOfficeTimeExp;
    Shift objShiftMaster = new Shift();
    IList<Shift> lstShift;
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
        //Call Validation for Exp Office time

        //btnAdd.Attributes.Add("onclick", "return conformation();");
        BtnExAdd.Attributes.Add("onclick", "return Expconformation();");
        txtStartTime.Attributes.Add("readonly", "readonly");
        txtEndTime.Attributes.Add("readonly", "readonly");
        txtEarlyETime.Attributes.Add("readonly", "readonly");
        txtExEarlyETime.Attributes.Add("readonly", "readonly");
        txtExEndTime.Attributes.Add("readonly", "readonly");
        txtExHalfETime.Attributes.Add("readonly", "readonly");
        txtExHalfLateETime.Attributes.Add("readonly", "readonly");
        txtExLateETime.Attributes.Add("readonly", "readonly");
        txtExRecessFrom.Attributes.Add("readonly", "readonly");
        txtExRecessTo.Attributes.Add("readonly", "readonly");
        txtNLateEnd.Attributes.Add("readonly", "readonly");
        txtRecessFrom.Attributes.Add("readonly", "readonly");
        txtRecessTo.Attributes.Add("readonly", "readonly");
        txtLateETime.Attributes.Add("readonly", "readonly");
        txtDateFrom.Attributes.Add("readonly", "readonly");
        txtDateTo.Attributes.Add("readonly", "readonly");
        txtHalfETime.Attributes.Add("readonly", "readonly");
        txtHalfLateETime.Attributes.Add("readonly", "readonly");
        txtExLateExitTime.Attributes.Add("readonly", "readonly");
        txtExStartTime.Attributes.Add("readonly", "readonly");
        txtExNLateEnd.Attributes.Add("readonly", "readonly");
        txtExpExLateExitTime.Attributes.Add("readonly", "readonly");
        txtHfStartTime.Attributes.Add("readonly", "readonly");
        txtHfEETime.Attributes.Add("readonly", "readonly");
        txtHfLETime.Attributes.Add("readonly", "readonly");
        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabOfficeTime.ActiveTab.HeaderText;

        if (!IsPostBack)
        {
            btndelete.Focus();
            FillLocation(ddlLocation);
            FillLocation(ddlLoc);
            FillLocation(ddlExplocation);
            FillLocation(ddlViewExplocation);
            showActiveGrid("V");
            shoExpwActiveGrid("V");
            if (Request.QueryString["OID"] != null)
            {
                FillOfficeTime();
                TabCreateOfficeTime.HeaderText = "UPDATE";
                btnAdd.Text = "Update";
                btnReset.Text = "Cancel";
                ViewState["Off"] = BtnExAdd.Text;
                TabOfficeTime.ActiveTabIndex = 0;
               
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "ShowHide1();", true);
                AdminConsoleNavigation.strNewLink = ">>" + "UPDATE";
            }
            else
            {
                TabCreateOfficeTime.HeaderText = "CREATE";
                btnAdd.Text = "Add";
                ViewState["Off"] = BtnExAdd.Text;
            }
            if (Request.QueryString["EID"] != null)
            {
                FillExpOfficeTime();
                FillCompareDates();
                TabExceptionalOffice.HeaderText = "Update Exceptional Office Time";
                BtnExAdd.Text = "Update";
                ViewState["Exp"] = BtnExAdd.Text;
                ddlExplocation.Enabled = false;
                TabOfficeTime.ActiveTabIndex = 2;
                btnExReset.Text = "Cancel"; 
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "ShowHide2();", true);
            }
            else
            {
                TabExceptionalOffice.HeaderText = "Add Exceptional Office Time";
                BtnExAdd.Text = "Add";
                btnExReset.Text = "Reset";
                ViewState["Exp"] = BtnExAdd.Text;
                ddlExplocation.Enabled = true;
            }
            if (Request.QueryString["locId"] != null)
            {
                TabOfficeTime.ActiveTabIndex = 1;
                ddlLoc.Items.FindByValue(Request.QueryString["locId"].ToString()).Selected = true;
                ddlLoc_SelectedIndexChanged(sender, e);
            }
            if (Request.QueryString["ExtabId"] != null)
            {
                TabOfficeTime.ActiveTabIndex = Convert.ToInt32(Request.QueryString["ExtabId"]);
            }

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "javascript", strscript, true);//Commented by Priyabrat Routray 30the Nov 2011
        }
        //btnReset.Attributes.Add("onclick", "return checkvalidation();");
        
    }
    #endregion

    #region "Office Time"

    #region "Button Events"
    /// <summary>
    /// To Add Office time.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["OID"] != null)
        {
            EditOfficeTime();
        }
        else
        {
            AddOfficeTime();
        }
    }
    /// <summary>
    /// To delete Office Time.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btndelete_Click(object sender, EventArgs e)
    {
        DeleteOfficeTime();
    }
    #endregion

    #region "Grid Events"
    /// <summary>
    /// To show the Serial number of gridview rows.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GVOfficeTime_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Convert.ToString((GVOfficeTime.PageSize * GVOfficeTime.PageIndex) + (e.Row.RowIndex + 1));
        }
    }
    #endregion

    #region "User functions"
    /// <summary>
    /// To Add Office Time.
    /// </summary>
    protected void AddOfficeTime()
    {
        try
        {
            objOfficeTimeMaster.ActionCode = "A";
            objOfficeTimeMaster.OfficeTimeID = 0;
            objOfficeTimeMaster.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            objOfficeTimeMaster.ShiftID = Convert.ToInt32(ddlShift.SelectedValue);
            //Start time
            objOfficeTimeMaster.StartTime = Convert.ToString(txtStartTime.Text.Trim());
            //grace time
            objOfficeTimeMaster.GraceTime = Convert.ToDecimal(ddlgracetime.SelectedItem.Text);
            //Recess From time
            objOfficeTimeMaster.RecessFrom = Convert.ToString(txtRecessFrom.Text.Trim());
            //Recess End time
            objOfficeTimeMaster.RecessTo = Convert.ToString(txtRecessTo.Text.Trim());
            //End Time
            objOfficeTimeMaster.EndTime = Convert.ToString(txtEndTime.Text.Trim());
            //late exit time
            objOfficeTimeMaster.LateEndTime = Convert.ToString(txtNLateEnd.Text.Trim());
            //early Entry Time
            objOfficeTimeMaster.EarlyESTime = Convert.ToString(txtEarlyETime.Text.Trim());
            //Late Entry time
            objOfficeTimeMaster.LateEETime = Convert.ToString(txtLateETime.Text.Trim());
            //extra late exit time
            objOfficeTimeMaster.ExtraLESTime = Convert.ToString(txtExLateExitTime.Text.Trim());
            //Weekly halfday
            if (cbWeekHalf1.Checked == true)
            {
                objOfficeTimeMaster.HalfEndTime = Convert.ToString(txtHalfETime.Text.Trim());
                objOfficeTimeMaster.HalfLExitTime = Convert.ToString(txtHalfLateETime.Text.Trim());
                objOfficeTimeMaster.HalfStartTime = Convert.ToString(txtHfStartTime.Text.Trim());
                objOfficeTimeMaster.HalfEETime = Convert.ToString(txtHfEETime.Text.Trim());
                objOfficeTimeMaster.HalfLETime = Convert.ToString(txtHfLETime.Text.Trim());

            }
            else
            {
                objOfficeTimeMaster.HalfEndTime = "";
                objOfficeTimeMaster.HalfLExitTime = "";
                objOfficeTimeMaster.HalfStartTime ="";
                objOfficeTimeMaster.HalfEETime = "";
                objOfficeTimeMaster.HalfLETime = "";
            }
            //Time Zone
            objOfficeTimeMaster.TimeZone = 0;
            objOfficeTimeMaster.CreatedBy = 1;

            returnvalue = Convert.ToInt32(ObjAdminBal.AddOfficeTime(objOfficeTimeMaster));
            strmsg = StaticValues.message(returnvalue, "Office Time");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');document.location.href='AdminOfficeTime.aspx';", true);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// To Edit Office Time
    /// </summary>
    protected void EditOfficeTime()
    {
        try
        {
            objOfficeTimeMaster.ActionCode = "U";
            objOfficeTimeMaster.OfficeTimeID = Convert.ToInt32(Request.QueryString["OID"]);
            objOfficeTimeMaster.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            objOfficeTimeMaster.ShiftID = Convert.ToInt32(ddlShift.SelectedValue);
            //Start time
            objOfficeTimeMaster.StartTime = Convert.ToString(txtStartTime.Text.Trim());
            //grace time
            objOfficeTimeMaster.GraceTime = Convert.ToDecimal(ddlgracetime.SelectedItem.Text);
            //Recess From time
            objOfficeTimeMaster.RecessFrom = Convert.ToString(txtRecessFrom.Text.Trim());
            //Recess End time
            objOfficeTimeMaster.RecessTo = Convert.ToString(txtRecessTo.Text.Trim());
            //End Time
            objOfficeTimeMaster.EndTime = Convert.ToString(txtEndTime.Text.Trim());
            //late exit time
            objOfficeTimeMaster.LateEndTime = Convert.ToString(txtNLateEnd.Text.Trim());
            //early Entry Time
            objOfficeTimeMaster.EarlyESTime = Convert.ToString(txtEarlyETime.Text.Trim());
            //Late Entry time
            objOfficeTimeMaster.LateEETime = Convert.ToString(txtLateETime.Text.Trim());
            //extra late exit time
            objOfficeTimeMaster.ExtraLESTime = Convert.ToString(txtExLateExitTime.Text.Trim());
            //Weekly halfday
 
            if (cbWeekHalf1.Checked == true)
            {
                objOfficeTimeMaster.HalfEndTime = Convert.ToString(txtHalfETime.Text.Trim());
                objOfficeTimeMaster.HalfLExitTime = Convert.ToString(txtHalfLateETime.Text.Trim());
                objOfficeTimeMaster.HalfStartTime = Convert.ToString(txtHfStartTime.Text.Trim());
                objOfficeTimeMaster.HalfEETime = Convert.ToString(txtHfEETime.Text.Trim());
                objOfficeTimeMaster.HalfLETime = Convert.ToString(txtHfLETime.Text.Trim());

            }
            else
            {
                objOfficeTimeMaster.HalfEndTime = "";
                objOfficeTimeMaster.HalfLExitTime = "";
                objOfficeTimeMaster.HalfStartTime = "";
                objOfficeTimeMaster.HalfEETime = "";
                objOfficeTimeMaster.HalfLETime = "";
            }
            //Time Zone
            objOfficeTimeMaster.TimeZone = 0;
            objOfficeTimeMaster.CreatedBy = Convert.ToInt32(Session["UserId"]);

            returnvalue = Convert.ToInt32(ObjAdminBal.UpdateOfficeTime(objOfficeTimeMaster));
            strmsg = StaticValues.message(returnvalue, "Office Time");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');document.location.href='AdminOfficeTime.aspx?locId=" + ddlLocation.SelectedValue + "';", true);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// To Delete Office Time.
    /// </summary>
    protected void DeleteOfficeTime()
    {
        try
        {
            int rcount = GVOfficeTime.Rows.Count;
            int strdata;
            for (int i = 0; i < rcount; i++)
            {
                if (((CheckBox)GVOfficeTime.Rows[i].Cells[0].FindControl("cbItem")).Checked == true)
                {
                    strdata = Convert.ToInt32(GVOfficeTime.DataKeys[i].Values[0].ToString());
                    objOfficeTimeMaster.ActionCode = "D";
                    objOfficeTimeMaster.OfficeTimeID = strdata;
                    objOfficeTimeMaster.CreatedBy = Convert.ToInt32(Session["UserId"]); ;
                    returnvalue = Convert.ToInt32(ObjAdminBal.DeleteOfficeTime(objOfficeTimeMaster));

                }
            }
            strmsg = StaticValues.message(returnvalue, "Office Time");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');document.location.href='AdminOfficeTime.aspx';", true);
            if (ddlLoc.SelectedItem.Text == "All Location")
            {
                showActiveGrid("V");
            }
            else
            {
                showActiveGrid("F");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// To show Gridview.
    /// </summary>
    /// <param name="Acode"></param>
    protected void showActiveGrid(string Acode)
    {
        objOfficeTimeMaster.ActionCode = Acode;
        if (Acode == "F")
        {
            objOfficeTimeMaster.LocationID = Convert.ToInt32(ddlLoc.SelectedValue);
        }
        lstOfficeTime = ObjAdminBal.GetAllOfficeTime(objOfficeTimeMaster);
        GVOfficeTime.DataSource = lstOfficeTime;
        GVOfficeTime.DataBind();
        if (GVOfficeTime.Rows.Count > 0)
        {
            btndelete.Visible = true;
        }
        else
        {
            btndelete.Visible = false;
        }
    }

    #endregion

    #region "Fill/Show Function"
    /// <summary>
    /// To Fill Location Dropdown.
    /// </summary>
    /// <param name="ddl"></param>
    protected void FillLocation(DropDownList ddl)
    {
        try
        {
            IList<Designation> objlstplink = ObjAdminBal.FillLocationDesig(Session["UserId"].ToString());
            ddl.DataSource = objlstplink;
            ddl.DataValueField = "LocationId";
            ddl.DataTextField = "LocationName";
            ddl.DataBind();
            //ddl.Items.Insert(0, "--Select--");
            if (ddl.ID == "ddlLoc" || ddl.ID == "ddlViewExplocation")
            {
                ddl.Items.Remove(ddl.Items[0]);
                ddl.Items.Insert(0, "All Location");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// To Fill Shift Dropdown with Location id.
    /// </summary>
    /// <param name="LocId"></param>
    /// <param name="ddl"></param>
    protected void FillShift(int LocId, DropDownList ddl)
    {
        try
        {
            objShiftMaster.ActionCode = "F";
            objShiftMaster.LocationID = LocId;
            lstShift = ObjAdminBal.GetAllShift(objShiftMaster);
            ddl.DataSource = lstShift;
            ddl.DataValueField = "ShiftID";
            ddl.DataTextField = "ShiftName";
            ddl.DataBind();
            ddl.Items.Insert(0, "--Select--");

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// To Fill The Office Time Details by clicking the edit Button.
    /// </summary>
    protected void FillOfficeTime()
    {
        IList<OT> objOfficeTimeList = new List<OT>();
        objOfficeTimeMaster.ActionCode = "E";
        objOfficeTimeMaster.OfficeTimeID = Convert.ToInt32(Request.QueryString["OID"]);
        objOfficeTimeList = ObjAdminBal.GetAllOfficeTime(objOfficeTimeMaster);
        foreach (var i in objOfficeTimeList)
        {
            objOfficeTimeMaster.OfficeTimeID = i.OfficeTimeID;
            ddlLocation.Items.FindByValue(Convert.ToString(i.LocationID)).Selected = true;
            FillShift(i.LocationID, ddlShift);
            ddlShift.Items.FindByValue(Convert.ToString(i.ShiftID)).Selected = true;
            //Start time
            txtStartTime.Text = i.StartTime;
            //grace time
            ddlgracetime.Items.FindByText(Convert.ToString(i.GraceTime)).Selected = true;
            //Recess From time
            txtRecessFrom.Text = i.RecessFrom;
            //Recess To time
            txtRecessTo.Text = i.RecessTo;
            //End Time
            txtEndTime.Text = i.EndTime;
            //late exit time
            txtNLateEnd.Text = i.LateEndTime;
            //early Entry Time
            txtEarlyETime.Text = i.EarlyESTime;
            //Late Entry time
            txtLateETime.Text = i.LateEETime;
            //extra late exit time
            txtExLateExitTime.Text = i.ExtraLESTime;
            //Weekly halfday
            txtHalfETime.Text = i.HalfEndTime;
            txtHalfLateETime.Text = i.HalfLExitTime;
            txtHfStartTime.Text = i.HalfStartTime;
            txtHfEETime.Text = i.HalfEETime;
            txtHfLETime.Text = i.HalfLETime;
            if (txtHalfETime.Text != "" && txtHalfLateETime.Text != "" && txtHfStartTime.Text != "" && txtHfEETime.Text != "" && txtHfEETime.Text != "" && txtHfLETime.Text != "")
            {
                cbWeekHalf1.Checked = true;
            }
            else
            {
                cbWeekHalf1.Checked = false;
            }
        }
    }
    #endregion

    #region"Dropdown Events"
    /// <summary>
    /// To fill the shift Dropdown from location dropdown.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillShift(Convert.ToInt32(ddlLocation.SelectedValue), ddlShift);
    }
    /// <summary>
    /// To fill the gridview from location dropdown In Edit Page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlLoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLoc.SelectedItem.Text == "All Location")
        {
            showActiveGrid("V");
        }
        else
        {
            showActiveGrid("F");
        }
    }
    #endregion

    #endregion

    #region "Exceptional Office Time"

    #region "Button Events"
    /// <summary>
    /// To Add/Update Exceptional Office Time
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnExAdd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["EID"] != null)
        {
            EditExpOfficeTime();
        }
        else
        {
            AddExpOfficeTime();
        }
    }
    /// <summary>
    /// To Delete Exceptional Office Time
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnVDelete_Click(object sender, EventArgs e)
    {
        DeleteExpOfficeTime();
    }
    #endregion

    #region "User Functions"
    /// <summary>
    /// To Add Exceptional Office Time
    /// </summary>
    protected void AddExpOfficeTime()
    {
        try
        {
            objExpOfficeTime.ActionCode = "A";
            objExpOfficeTime.OfficeTimeID = 0;
            objExpOfficeTime.LocationID = Convert.ToInt32(ddlExplocation.SelectedValue);
            objExpOfficeTime.DateFrom = Convert.ToDateTime(txtDateFrom.Text);

            objExpOfficeTime.DateTo = Convert.ToDateTime(txtDateTo.Text);
            //Start time
            objExpOfficeTime.StartTime = Convert.ToString(txtExStartTime.Text.Trim());
            //grace time
            objExpOfficeTime.GraceTime = Convert.ToDecimal(ddlExgracetime.SelectedItem.Text);
            //Recess From time
            objExpOfficeTime.RecessFrom = Convert.ToString(txtExRecessFrom.Text.Trim());
            //Recess End time
            objExpOfficeTime.RecessTo = Convert.ToString(txtExRecessTo.Text.Trim());
            //End Time
            objExpOfficeTime.EndTime = Convert.ToString(txtExEndTime.Text.Trim());
            //late exit time
            objExpOfficeTime.LateEndTime = Convert.ToString(txtExNLateEnd.Text.Trim());
            //early Entry Time
            objExpOfficeTime.EarlyESTime = Convert.ToString(txtExEarlyETime.Text.Trim());
            //Late Entry time
            objExpOfficeTime.LateEETime = Convert.ToString(txtExLateETime.Text.Trim());
            //extra late exit time
            objExpOfficeTime.ExtraLESTime = Convert.ToString(txtExpExLateExitTime.Text.Trim());
            //Religion
            objExpOfficeTime.Religion = Convert.ToInt32(ddlReligion.SelectedValue);
            //Weekly halfday
            if (cbWeekHalf2.Checked == true)
            {
                objExpOfficeTime.HalfEndTime = Convert.ToString(txtExHalfETime.Text.Trim());
                objExpOfficeTime.HalfLExitTime = Convert.ToString(txtExHalfLateETime.Text.Trim());
            }
            else
            {
                objExpOfficeTime.HalfEndTime = "";
                objExpOfficeTime.HalfLExitTime = "";
            }
            //Time Zone
            objExpOfficeTime.TimeZone = 0;
            objExpOfficeTime.CreatedBy = Convert.ToInt32(Session["UserId"]);

            returnvalue = Convert.ToInt32(ObjAdminBal.AddOfficeTimeExp(objExpOfficeTime));
            strmsg = StaticValues.message(returnvalue, "Exceptional Office Time");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');document.location.href='AdminOfficeTime.aspx';", true);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// To update Exceptional Office Time
    /// </summary>
    protected void EditExpOfficeTime()
    {
        try
        {
            objExpOfficeTime.ActionCode = "U";
            objExpOfficeTime.OfficeTimeID = Convert.ToInt32(Request.QueryString["EID"]);
            objExpOfficeTime.LocationID = Convert.ToInt32(ddlExplocation.SelectedValue);
            objExpOfficeTime.DateFrom = Convert.ToDateTime(txtDateFrom.Text);
            objExpOfficeTime.DateTo = Convert.ToDateTime(txtDateTo.Text);
            //Start time
            objExpOfficeTime.StartTime = Convert.ToString(txtExStartTime.Text.Trim());
            //grace time
            objExpOfficeTime.GraceTime = Convert.ToDecimal(ddlExgracetime.SelectedItem.Text);
            //Recess From time
            objExpOfficeTime.RecessFrom = Convert.ToString(txtExRecessFrom.Text.Trim());
            //Recess End time
            objExpOfficeTime.RecessTo = Convert.ToString(txtExRecessTo.Text.Trim());
            //End Time
            objExpOfficeTime.EndTime = Convert.ToString(txtExEndTime.Text.Trim());
            //late exit time
            objExpOfficeTime.LateEndTime = Convert.ToString(txtExNLateEnd.Text.Trim());
            //early Entry Time
            objExpOfficeTime.EarlyESTime = Convert.ToString(txtExEarlyETime.Text.Trim());
            //Late Entry time
            objExpOfficeTime.LateEETime = Convert.ToString(txtExLateETime.Text.Trim());
            //extra late exit time
            objExpOfficeTime.ExtraLESTime = Convert.ToString(txtExpExLateExitTime.Text.Trim());
            //Religion
            objExpOfficeTime.Religion = Convert.ToInt32(ddlReligion.SelectedValue);
            //Weekly halfday
            if (cbWeekHalf2.Checked == true)
            {
                objExpOfficeTime.HalfEndTime = Convert.ToString(txtExHalfETime.Text.Trim());
                objExpOfficeTime.HalfLExitTime = Convert.ToString(txtExHalfLateETime.Text.Trim());

            }
            else
            {
                objExpOfficeTime.HalfEndTime = "";
                objExpOfficeTime.HalfLExitTime = "";
            }
            //Time Zone
            objExpOfficeTime.TimeZone = 0;
            objExpOfficeTime.CreatedBy = Convert.ToInt32(Session["UserId"]);

            returnvalue = Convert.ToInt32(ObjAdminBal.UpdateOfficeTimeExp(objExpOfficeTime));
            strmsg = StaticValues.message(returnvalue, "Exceptional Office Time");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');document.location.href='AdminOfficeTime.aspx?ExtabId=3'", true);
         }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// To Delete Exceptional Office Time
    /// </summary>
    protected void DeleteExpOfficeTime()
    {
        int rcount = GvExpOffView.Rows.Count;
        int strdata;
        for (int i = 0; i < rcount; i++)
        {
            if (((CheckBox)GvExpOffView.Rows[i].Cells[0].FindControl("cbItem")).Checked == true)
            {
                strdata = Convert.ToInt32(GvExpOffView.DataKeys[i].Values[0].ToString());
                objExpOfficeTime.ActionCode = "D";
                objExpOfficeTime.OfficeTimeID = strdata;
                objExpOfficeTime.CreatedBy = Convert.ToInt32(Session["UserId"]);
                returnvalue = Convert.ToInt32(ObjAdminBal.DeleteOfficeTimeExp(objExpOfficeTime));

            }
        }
        strmsg = StaticValues.message(returnvalue, "Exceptional Office Time");
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');document.location.href='AdminOfficeTime.aspx';", true);
        if (ddlViewExplocation.SelectedItem.Text == "All Location")
        {
            shoExpwActiveGrid("V");
        }
        else
        {
            shoExpwActiveGrid("F");
        }
    }
    /// <summary>
    /// To Clear the Office Time.
    /// </summary>
    protected void ClearData()
    {
        txtDateFrom.Text = "";
        txtDateTo.Text = "";
        //Start time
        txtExStartTime.Text = "";
        //grace time
        ddlExgracetime.ClearSelection();
        ddlExgracetime.Items.FindByText(Convert.ToString(0)).Selected = true;
        //Recess From time
        txtExRecessFrom.Text = "";
        //Recess To time
        txtExRecessTo.Text = "";
        //End Time
        txtExEndTime.Text = "";
        //late exit time
        txtExNLateEnd.Text = "";
        //early Entry Time
        txtExEarlyETime.Text = "";
        //Late Entry time
        txtExLateETime.Text = "";
        //extra late exit time
        txtExpExLateExitTime.Text = "";
        //religion
        ddlReligion.ClearSelection();
        ddlReligion.Items.FindByValue(Convert.ToString(0)).Selected = true;
        //Weekly halfday
        txtExHalfETime.Text = "";
        txtExHalfLateETime.Text = "";
    }
    /// <summary>
    /// To show the grid of exceptional office Time.
    /// </summary>
    /// <param name="Acode"></param>
    protected void shoExpwActiveGrid(string Acode)
    {
        objExpOfficeTime.ActionCode = Acode;
        if (Acode == "F")
        {
            objExpOfficeTime.LocationID = Convert.ToInt32(ddlViewExplocation.SelectedValue);
        }
        lstOfficeTimeExp = ObjAdminBal.GetAllOfficeTimeExp(objExpOfficeTime);
        GvExpOffView.DataSource = lstOfficeTimeExp;
        GvExpOffView.DataBind();
        if (GvExpOffView.Rows.Count > 0)
        {
            btnVDelete.Visible = true;
        }
        else
        {
            btnVDelete.Visible = false;
        }
    }
    /// <summary>
    /// To Compare the date for Adding.
    /// </summary>
    /// <returns></returns>
    protected string DtCompare()
    {
        DateTime dt;
        objExpOfficeTime.ActionCode = "C";
        objExpOfficeTime.LocationID = Convert.ToInt32(ddlExplocation.SelectedValue);
        dt = Convert.ToDateTime(ObjAdminBal.DateCompare(objExpOfficeTime));
        return dt.ToString("dd-MMM-yyyy");
    }
    /// <summary>
    ///  To Compare the date for Updating
    /// </summary>
    protected void FillCompareDates()
    {
        try
        {
            IList<OT> objExpTimeCompare = new List<OT>();
            objExpOfficeTime.ActionCode = "G";
            objExpOfficeTime.OfficeTimeID = Convert.ToInt32(Request.QueryString["EID"]);
            objExpOfficeTime.LocationID = Convert.ToInt32(hidExpval.Value);
            objExpTimeCompare = ObjAdminBal.GetAllCompareList(objExpOfficeTime);

            foreach (var i in objExpTimeCompare)
            {
                strPreFromdate = i.DateFrom.ToString("dd-MMM-yyyy");
                strPreTodate = i.DateTo.ToString("dd-MMM-yyyy");
                strNextFromdate = i.NextDateFrom.ToString("dd-MMM-yyyy");
                strNextTodate = i.NextDateTo.ToString("dd-MMM-yyyy");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region "Fill/Show Functions"
    /// <summary>
    /// To Fill The Exceptional Office Time details By selecting the Location.
    /// </summary>
    protected void FillExpOfficeTime()
    {
        try
        {
            IList<OT> objExpOfficeTimeList = new List<OT>();
            objExpOfficeTime.ActionCode = "E";
            objExpOfficeTime.OfficeTimeID = Convert.ToInt32(Request.QueryString["EID"]);
            objExpOfficeTimeList = ObjAdminBal.GetAllOfficeTimeExp(objExpOfficeTime);
            foreach (var i in objExpOfficeTimeList)
            {
                //objExpOfficeTime.OfficeTimeID = i.OfficeTimeID;

                ddlExplocation.Items.FindByValue(Convert.ToString(i.LocationID)).Selected = true;
                hidExpval.Value = Convert.ToString(i.LocationID);
                txtDateFrom.Text = i.DateFrom.ToString("dd-MMM-yyy");
                txtDateTo.Text = i.DateTo.ToString("dd-MMM-yyy");
                //Start time
                txtExStartTime.Text = i.StartTime;
                //grace time
                ddlExgracetime.ClearSelection();
                ddlExgracetime.Items.FindByText(i.GraceTime.ToString()).Selected = true;
                //Recess From time
                txtExRecessFrom.Text = i.RecessFrom;
                //Recess To time
                txtExRecessTo.Text = i.RecessTo;
                //End Time
                txtExEndTime.Text = i.EndTime;
                //late exit time
                txtExNLateEnd.Text = i.LateEndTime;
                //early Entry Time
                txtExEarlyETime.Text = i.EarlyESTime;
                //Late Entry time
                txtExLateETime.Text = i.LateEETime;
                //extra late exit time
                txtExpExLateExitTime.Text = i.ExtraLESTime;
                //religion
                ddlReligion.ClearSelection();
                ddlReligion.Items.FindByValue(Convert.ToString(i.Religion)).Selected = true;
                //Weekly halfday
                txtExHalfETime.Text = i.HalfEndTime;
                txtExHalfLateETime.Text = i.HalfLExitTime;
                if (txtExHalfETime.Text != "" && txtExHalfLateETime.Text != "")
                {
                    cbWeekHalf2.Checked = true;
                }
                else
                {
                    cbWeekHalf2.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region "Dropdown Events"
    /// <summary>
    /// Fill Gridview on dropdown selected index changed. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlViewExplocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlViewExplocation.SelectedItem.Text == "All Location")
        {
            shoExpwActiveGrid("V");
        }
        else
        {
            shoExpwActiveGrid("F");
        }

    }
    /// <summary>
    /// Comapre date When Adding  new record.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlExplocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        DtContrl = DtCompare();

    }
    #endregion

    #region "GridView Events"
    /// <summary>
    /// To Fill the SL nummber
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GvExpOffView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Convert.ToString((GvExpOffView.PageSize * GvExpOffView.PageIndex) + (e.Row.RowIndex + 1));
        }
    }
    #endregion

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.Text == "Reset")
        {
            ResetControls();
            ddlLocation.Focus();

        }
        else
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "document.location.href='AdminOfficeTime.aspx?locId=" + ddlLocation.SelectedValue + "';", true);
        }
    }
    protected void btnExReset_Click(object sender, EventArgs e)
    {
        if (btnExReset.Text == "Reset")
        {
            ExResetControls();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "document.location.href='AdminOfficeTime.aspx?ExtabId=3'", true);            
        }
    }
    #endregion

    protected void TabOfficeTime_ActiveTabChanged(object sender, EventArgs e)
    {
        ResetControls(); 
        if (TabOfficeTime.ActiveTabIndex == 0)
        {
            ddlLocation.Focus(); 
        }
        else if (TabOfficeTime.ActiveTabIndex == 1)
        {
            btndelete.Focus();
        }
        else if (TabOfficeTime.ActiveTabIndex == 2)
        {
            ddlExplocation.Focus(); 
        }
        else if (TabOfficeTime.ActiveTabIndex == 3)
        {
            btnVDelete.Focus();
        }
    }

    private void ResetControls()
    {
        ddlLocation.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        ddlgracetime.SelectedIndex = 0;
        cbWeekHalf1.Checked = false;
        CommonFunction.ResetTextBox(txtStartTime, txtEndTime, txtEarlyETime, txtExEarlyETime, txtRecessFrom, txtRecessTo, txtNLateEnd, txtExLateExitTime, txtLateETime, txtDateFrom, txtDateTo, txtHalfETime, txtHalfLateETime, txtHfStartTime, txtHfEETime, txtHfLETime);
    }
    private void ExResetControls()
    {
        ddlExplocation.SelectedIndex = 0;
        ddlExgracetime.SelectedIndex = 0;
        ddlExgracetime.SelectedIndex = 0;
        ddlReligion.SelectedIndex=0;
        cbWeekHalf2.Checked = false;
        CommonFunction.ResetTextBox(txtDateFrom, txtDateTo, txtExStartTime, txtExRecessFrom, txtExRecessTo, txtExEndTime, txtExNLateEnd, txtExLateETime, txtExpExLateExitTime, txtExStartTime, txtExRecessFrom, txtExRecessTo, txtExNLateEnd, txtExEarlyETime, txtExLateETime, txtExpExLateExitTime, txtExHalfETime, txtExHalfLateETime);
    }
}
