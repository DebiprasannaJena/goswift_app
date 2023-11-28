/********************************************************************************************************************
' File Name             :   AdminShiftMaster.aspx.cs
' Description           :   To Add/edit/activate/inactivate Shift Master
' Created by            :   Subhasis Kumar dash
' Created On            :   03-Aug-2010
' Modification History  :
'                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
'                            1                             25-June-2012       Dilip Tripathy               To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                                                    
'                         
' Function Name         :  AddShiftMaster(),EditShiftMaster(),DeleteShift(),Checkstatus(),showActiveGrid() 
' Procedures Used       :  usp_ShiftMaster_Manage,usp_ShiftMaster_View  
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
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
public partial class Admin_Office_Timing_AdminShiftMaster : System.Web.UI.Page
{
    #region "Variable"
    /// <summary>
    /// Declaring Variables
    /// </summary>
    AdminAppService ObjAdminBal = new AdminAppService();
    Shift objShiftMaster = new Shift();
    int returnvalue = 0;
    string strmsg;
    public string strbtntext, strDefault;
    public string strShiftname, strLocname;
    IList<Shift> lstShift;
    //IList<IShiftMaster> lstShift;
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
        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabShiftMaster.ActiveTab.HeaderText;

        if (!IsPostBack)
        {
            IList<Designation> objlstplink = ObjAdminBal.FillLocationDesig(Session["UserId"].ToString());
            ddlLocation.DataSource = objlstplink;
            ddlLocation.DataValueField = "LocationId";
            ddlLocation.DataTextField = "LocationName";
            ddlLocation.DataBind();
            //ddlLocation.Items.Insert(0, "--Select--");
            ddlLoc.DataSource = objlstplink;
            ddlLoc.DataValueField = "LocationId";
            ddlLoc.DataTextField = "LocationName";
            ddlLoc.DataBind();
            //ddlLoc.Items.Insert(0, "--Select--");
            //**********************End*****************************
            TabShiftMaster.ActiveTabIndex = 1;
            showActiveGrid("V");
            if (Request.QueryString["SID"] != null)
            {
                FillShift();
                TabCreateShift.HeaderText = "UPDATE";
                btnAdd.Text = "Update";
                btnReset.Text = "Cancel";
                strbtntext = btnAdd.Text;
                TabShiftMaster.ActiveTabIndex = 0;
                AdminConsoleNavigation.strNewLink = ">>" + "UPDATE";
            }
            else
            {
                TabCreateShift.HeaderText = "CREATE";
                btnAdd.Text = "Save";
                btnReset.Text = "Reset";
                strbtntext = btnAdd.Text;
            }
            btnAdd.Focus();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "javascript", strscript, true);//Commented by Priyabrat Routray 30the Nov 2011
        }

        btnReset.Attributes.Add("onclick", "return CallReset();");
        this.txtDesc.Attributes.Add("onkeyup", "return TextCounter('" + txtDesc.ClientID + "','" + lblMaxCounter.ClientID + "',100);");
        this.txtDesc.Attributes.Add("onkeydown", "return AllowEnter();");

    }
    #endregion

    #region "Button Events"
    /// <summary>
    /// To Add/edit Shift Master.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["SID"] != null)
        {
            EditShiftMaster();
        }
        else
        {
            AddShiftMaster();
        }
    }
    /// <summary>
    /// To delete Shift Master.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btndelete_Click(object sender, EventArgs e)
    {
        DeleteShift();
    }
    #endregion

    #region "Grid Events"
    /// <summary>
    /// To show the Serial number of gridview rows and Assign default tag.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GVShift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Convert.ToString((GVShift.PageSize * GVShift.PageIndex) + (e.Row.RowIndex + 1));

            if (Convert.ToString(GVShift.DataKeys[e.Row.RowIndex].Values[1]) == "Y")
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text + "<b> <font color='Maroon'>&nbsp;&nbsp;(Default)</font></b>";
            }
            if (Convert.ToInt32(GVShift.DataKeys[e.Row.RowIndex].Values[2]) == 1)
            {
                e.Row.Cells[3].Text = e.Row.Cells[3].Text + "<b> <font color='Maroon'>&nbsp;&nbsp;(Cross Shift)</font></b>";
            }
        }
    }
    #endregion

    #region "User functions"
    /// <summary>
    /// To Add shift master.
    /// </summary>
    protected void AddShiftMaster()
    {
        try
        {
            objShiftMaster.ActionCode = "A";
            objShiftMaster.ShiftID = 0;
            objShiftMaster.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            objShiftMaster.ShiftName = txtShiftName.Text.Trim();
            objShiftMaster.ShiftDescription = txtDesc.Text.Trim();
            if (chkView.Visible == true)
            {
                if (chkView.Checked == true)
                {
                    objShiftMaster.DefaultShift = "Y";
                }
                else
                {
                    objShiftMaster.DefaultShift = "N";
                }
            }
            else
            {
                objShiftMaster.DefaultShift = "N";
            }

            if (cbShiftType.Checked == true)
            {
                objShiftMaster.ShiftType = 1;
            }
            else
            {
                objShiftMaster.ShiftType = 0;
            }

            objShiftMaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
            returnvalue = Convert.ToInt32(ObjAdminBal.AddShift(objShiftMaster));
            strmsg = StaticValues.message(returnvalue, "Shift Details");
            ScriptManager.RegisterStartupScript(btnAdd, GetType(), "", "alert('" + strmsg + "');document.location.href='AdminShiftMaster.aspx';", true);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    /// <summary>
    /// To Edit shift master.
    /// </summary>
    protected void EditShiftMaster()
    {
        try
        {
            objShiftMaster.ActionCode = "U";
            objShiftMaster.ShiftID = Convert.ToInt32(Request.QueryString["SID"]);
            objShiftMaster.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            objShiftMaster.ShiftName = txtShiftName.Text.Trim();
            objShiftMaster.ShiftDescription = txtDesc.Text.Trim();
            if (chkView.Checked == true)
            {
                objShiftMaster.DefaultShift = "Y";
            }
            else
            {
                objShiftMaster.DefaultShift = "N";
            }
            if (cbShiftType.Checked == true)
            {
                objShiftMaster.ShiftType = 1;
            }
            else
            {
                objShiftMaster.ShiftType = 0;
            }
            objShiftMaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
            returnvalue = Convert.ToInt32(ObjAdminBal.UpdateShift(objShiftMaster));
            strmsg = StaticValues.message(returnvalue, "Shift Details");
            ScriptManager.RegisterStartupScript(btnAdd, GetType(), "", "alert('" + strmsg + "');document.location.href='AdminShiftMaster.aspx';", true);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// To delete Shift Master.
    /// </summary>
    protected void DeleteShift()
    {
        int rcount = GVShift.Rows.Count;
        int strdata;
        for (int i = 0; i < rcount; i++)
        {
            if (((CheckBox)GVShift.Rows[i].Cells[0].FindControl("cbItem")).Checked == true)
            {
                strdata = Convert.ToInt32(GVShift.DataKeys[i].Values[0].ToString());
                objShiftMaster.ActionCode = "D";
                objShiftMaster.ShiftID = strdata;
                objShiftMaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                returnvalue = Convert.ToInt32(ObjAdminBal.DeleteShift(objShiftMaster));

            }
        }
        strmsg = StaticValues.message(returnvalue, "Shift Details");
        if (returnvalue == 7)
        {
            ScriptManager.RegisterStartupScript(btndelete, GetType(), "", "alert('Shift is currently used for a office time.So can not be deleted.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(btndelete, GetType(), "", "alert('" + strmsg + "');", true);
        }
        TabShiftMaster.ActiveTabIndex = 1;
        if (ddlLoc.SelectedItem.Text == "All Location")
        {
            showActiveGrid("V");
        }
        else
        {
            showActiveGrid("F");
        }
    }
    /// <summary>
    /// To check if the default value is set or not.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    protected int Checkstatus(int status)
    {
        objShiftMaster.ActionCode = "F";
        objShiftMaster.LocationID = status;
        lstShift = ObjAdminBal.GetAllShift(objShiftMaster);
        int intsetval = 0;
        foreach (var i in lstShift)
        {
            if (i.DefaultShift == "Y")
            {
                intsetval = 1;
                strLocname = i.LocationName;
                strShiftname = i.ShiftName;
                break;
            }
        }
        return intsetval;
    }
    /// <summary>
    /// To show gridview.
    /// </summary>
    /// <param name="Acode"></param>
    protected void showActiveGrid(string Acode)
    {
        objShiftMaster.ActionCode = Acode;
        if (Acode == "F")
        {
            if (ddlLoc.SelectedItem.Text != "--Select--")
            {
                objShiftMaster.LocationID = Convert.ToInt32(ddlLoc.SelectedValue);
            }
            else
            {
                objShiftMaster.LocationID = 0;
            }
            
        }
        lstShift = ObjAdminBal.GetAllShift(objShiftMaster);
        GVShift.DataSource = lstShift;
        GVShift.DataBind();
        DisplayPaging(GVShift, lstShift.Count);       
        if (GVShift.Rows.Count > 0)
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
    /// To Fill Shift Details of indivisual record.
    /// </summary>
    protected void FillShift()
    {
        IList<Shift> objShiftList = new List<Shift>();
        objShiftMaster.ActionCode = "E";
        objShiftMaster.ShiftID = Convert.ToInt32(Request.QueryString["SID"]);
        objShiftList = ObjAdminBal.GetAllShift(objShiftMaster);
        foreach (var i in objShiftList)
        {
            objShiftMaster.ShiftID = i.ShiftID;
            ddlLocation.Items.FindByValue(Convert.ToString(i.LocationID)).Selected = true;
            txtShiftName.Text = i.ShiftName;
            txtDesc.Text = i.ShiftDescription;
            lblMaxCounter.Text = (int.Parse(lblMaxCounter.Text) - txtDesc.Text.Length).ToString();
            if (Checkstatus(i.LocationID) == 1)
            {

                if (i.DefaultShift == "Y")
                {
                    chkView.Checked = true;
                }
                else
                {
                    chkView.Visible = false;
                    chkView.Checked = false;
                    lblDefaultMsg.Visible = true;
                    lblDefaultMsg.Text = "The" + "<b> <font color='Maroon'>&nbsp;'" + strShiftname + "'</font></b>" + " is Already assigned as the Default Shift for" + "<b> <font color='Maroon'>&nbsp;'" + strLocname + "'</font></b>" + " To change the default shift,go to Edit and Remove the Current default Shift.";
                }
            }
            else
            {
                chkView.Visible = true;
                chkView.Checked = false;
                lblDefaultMsg.Visible = false;
            }
            if (i.ShiftType == 1)
            {
                cbShiftType.Checked = true;
            }
            else
            {
                cbShiftType.Checked = false;
            }
        }
    }
    #endregion

    #region"Dropdown Events"
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
    /// <summary>
    /// To make the Visibility True/false According To location dropdown.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Checkstatus(Convert.ToInt32(ddlLocation.SelectedValue)) == 1)
        {
            chkView.Visible = false;
            lblDefaultMsg.Visible = true;
            lblDefaultMsg.Text = "The" + "<b> <font color='Maroon'>&nbsp;'" + strShiftname + "'</font></b>" + " is Already assigned as the Default Shift for" + "<b> <font color='Maroon'>&nbsp;'" + strLocname + "'</font></b>" + " To change the default shift,go to Edit and Remove the Current default Shift.";

        }
        else
        {
            chkView.Visible = true;
            lblDefaultMsg.Visible = false;

        }
    }
    #endregion

    protected void TabShiftMaster_ActiveTabChanged(object sender, EventArgs e)
    {
        ddlLocation.SelectedIndex = 0;
        txtShiftName.Text = "";
        txtDesc.Text = "";
        chkView.Checked = false;
        lblDefaultMsg.Text = "";
        TabCreateShift.HeaderText = "Create";
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMaxCounter.Text = "100";
        if (chkView.Visible == false)
        {
            lblDefaultMsg.Visible = false;
            chkView.Visible = true;
            chkView.Checked = false;
        }
        else
        {
            chkView.Checked = false;
        }

    }
    protected void lnkBtnAll_Click(object sender, EventArgs e)
    {
        if (lnkBtnAll.Text == "All")
        {
            lnkBtnAll.Text = "Paging";

            this.GVShift.PageIndex = 0;
            GVShift.AllowPaging = false;
            showActiveGrid("V");
            if (GVShift.Rows.Count > 0)
            {
                this.lblPaging.Text = "1-" + GVShift.Rows.Count.ToString() + " Of " + GVShift.Rows.Count.ToString();
            }

        }
        else
        {
            lnkBtnAll.Text = "All";
            GVShift.AllowPaging = true;
            showActiveGrid("V");

        }
    }
    /// <summary>
    /// Created By   : Dilip Kumar Tripathy.
    /// Created Date : 14-Jun-2013
    /// Purpose      : To show  Paging details of gridview
    /// </summary>
    private void DisplayPaging(GridView gridviewone, int totalRowCount)
    {
        if (gridviewone.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lnkBtnAll.Visible = true;
            lblPaging.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(gridviewone, gridviewone.PageSize, gridviewone.PageIndex, totalRowCount);
        }
        else
        {
            this.lblPaging.Visible = false;
            lnkBtnAll.Visible = false;
        }
    }
    protected void GVShift_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVShift.PageIndex = e.NewPageIndex;
        showActiveGrid("V");
    }

}
