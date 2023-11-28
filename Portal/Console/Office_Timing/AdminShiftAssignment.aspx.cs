/********************************************************************************************************************
' File Name             :   AdminShiftAssignment.aspx.cs
' Description           :   To Add/Edit/Delete Shift Assignment for users.
' Created by            :   Subhasis Kumar dash
' Created On            :   09-Aug-2010
' Modification History  :
'                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
'                            1                            25-June-2012       Dilip Tripathy               To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                                                    
'                         
' Function Name         :    AddShiftAssign(),MoveShitAssignment(),GetHierarchy(),SearchEmployeeDetails()
' Procedures Used       :    usp_ShiftAssignment_View,usp_ShiftAssignment_Manage
' User Defined Namespace:   
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
//using CSMPDK_3_0;
using KWAdminConsole.Messages;
using Manage_Usercontrol_Property;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using System.ComponentModel;

public partial class Admin_Office_Timing_AdminShiftAssignment : System.Web.UI.Page
{
    /// <summary>
    /// Declaring Variables
    /// </summary>
    Shift objAssignShift = new Shift();
    int returnvalue = 0;
    string strmsg = null;
    public string strbtntext, strExbtntext;
    IList<Shift> lstAssignShift;
    AdminAppService ObjAdminBal = new AdminAppService();
    PopHierarchy objPop = new PopHierarchy();
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

        CommonProperties.PageUrl = "AdminShiftAssignment.aspx";
        CommonProperties.UserControlId = selRUser.ClientID;
        CommonProperties.UserControlId2 = null;
        Session["PosId"] = null;
        Session["hidLevel"] = null;
        CommonProperties.Action = "A";
        CommonProperties.Type = "2";
        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabShiftAssign.ActiveTab.HeaderText;        
        
        if (!IsPostBack)
        {
            FillLocation(ddlLocation);
            ddlEshift.Visible = false;
            btnMove.Visible = false;
            btnDelete.Visible = false;
        }
    }
    #endregion

    #region "Button Events"
    /// <summary>
    /// To Add/Update the shift to users.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddShiftAssign();
    }
    /// <summary>
    /// To search User and other deatils by selecting the Hierachy.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LnkbtnAllin.Text = "All";
        GrdShiftAssign.AllowPaging = true;
        SearchEmployeeDetails();
        DropDownList ddlFillShift = (DropDownList)HierarchyForAllLocation2.FindControl("sdrplayers0");
        //FillShiftName(GetHierarchy(Convert.ToInt32(ddlFillShift.SelectedValue)), ddlEshift);
        FillShiftName(Convert.ToInt32(ddlFillShift.SelectedValue), ddlEshift);
        // Commented By Priyabrat not to clear the Hidden Field value of getUser2 
        //for (int i = 0; i < 8; i++)
        //{
        //    claerval = (HiddenField)HierarchyForAllLocation2.FindControl("shidIDs" + Convert.ToString(i));
        //    if (claerval.Value != "")
        //    {
        //        claerval.Value = "";
        //    }
        //    else
        //    {
        //        break;
        //    }
        //}

    }
    /// <summary>
    /// To Move the Shift of Users to other shift
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMove_Click(object sender, EventArgs e)
    {
        MoveShitAssignment("M");
    }
    /// <summary>
    /// To Delete the Shift of Users.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        MoveShitAssignment("D");
    }
    #endregion

    #region "User Functions"
    /// <summary>
    /// To Add/Update Shift To User.
    /// </summary>
    protected void AddShiftAssign()
    {
        objAssignShift.ActionCode = "N";
        objAssignShift.UID = hiduser.Value;
        objAssignShift.RUID = hidRuser.Value;
        objAssignShift.ShiftID = Convert.ToInt32(ddlShift.SelectedValue);
        objAssignShift.UpdatedBy = Convert.ToInt32(Session["UserId"]);
        returnvalue = Convert.ToInt32(ObjAdminBal.AddAssignShift(objAssignShift));
        strmsg = StaticValues.message(returnvalue, "Shift Assign");
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, typeof(string), "", "alert('" + strmsg + "');document.location.href='AdminShiftAssignment.aspx';", true);
    }
    /// <summary>
    /// To move and Delete shift of Users.
    /// </summary>
    /// <param name="MoveDelete"></param>
    protected void MoveShitAssignment(string MoveDelete)
    {
        try
        {
            int rcount = GrdShiftAssign.Rows.Count;
            int strdata;
            for (int i = 0; i < rcount; i++)
            {
                if (((CheckBox)GrdShiftAssign.Rows[i].Cells[0].FindControl("cbItem")).Checked == true)
                {
                    strdata = Convert.ToInt32(GrdShiftAssign.DataKeys[i].Values[0].ToString());
                    if (MoveDelete == "M")
                    {
                        objAssignShift.ActionCode = "M";
                        objAssignShift.ShiftID = Convert.ToInt32(ddlEshift.SelectedValue);
                    }
                    else if (MoveDelete == "D")
                    {
                        objAssignShift.ActionCode = "D";
                        objAssignShift.ShiftID = 0;
                    }
                    objAssignShift.UserId = strdata;
                    objAssignShift.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                    returnvalue = Convert.ToInt32(ObjAdminBal.MoveDeleteShift(objAssignShift));
                }
            }
            strmsg = StaticValues.message(returnvalue, "Shift");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');", true);
            SearchEmployeeDetails();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// To Get The Hierachy from Level Details id of usercontrol
    /// </summary>
    /// <param name="lvlDetails"></param>
    /// <returns></returns>
    protected int GetHierarchy(int lvlDetails)
    {
        int inthierar = 0;

        IList<User>  lstUser = ObjAdminBal.UserGetHierarchyID(lvlDetails.ToString());
        inthierar = lstUser[0].PositionId;
        return inthierar;
    }
    /// <summary>
    /// To Search user Details of particular Hierarchy
    /// </summary>
    protected void SearchEmployeeDetails()
    {

        objAssignShift.ActionCode = "V";
        objAssignShift.UID = DepartmentOfUser("V");
        objAssignShift.Fullname = txtsearch.Text;
        lstAssignShift = ObjAdminBal.GetAllAssignedShift(objAssignShift);
        GrdShiftAssign.DataSource = lstAssignShift;
        GrdShiftAssign.DataBind();
        if (GrdShiftAssign.Rows.Count > 0)
        {
            lblSelectShift.Visible = true;
            ddlEshift.Visible = true;
            btnDelete.Visible = true;
            btnMove.Visible = true;
            ddlEshift.Visible = true;
            btnMove.Visible = true;
            btnDelete.Visible = true;
            GrdShiftAssign.Visible = true;
            DisplayPaging(GrdShiftAssign, lstAssignShift.Count);
        }
        else
        {
            lblSelectShift.Visible = false;
            ddlEshift.Visible = false;
            btnMove.Visible = false;
            btnDelete.Visible = false;
            lblpage.Visible = false;
            LnkbtnAllin.Visible = false;
            GrdShiftAssign.Visible = false;
        }

        //FillControl(strDeptid); by dilip
    }
    /// <summary>
    /// Method to get positionid from leveldetail id
    /// Added By Pratik On 20-Jul-2010
    /// </summary>
    /// <param name="intLvlDtlId"></param>
    /// <returns></returns>
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
                string[] levelArray = new string[objPop.Count];
                for (int k = 0; k < objPop.Count; k++)
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


        return strDept;
    }

    private string getPosId(int intLvlDtlId)
    {
        string strPosId = "";
        objPop.LocationId = Convert.ToInt32(intLvlDtlId.ToString());
        strPosId = ObjAdminBal.GetPOSId(objPop).ToString();
        return strPosId;
    }
   

    #endregion

    #region "Dropdown Events"
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedIndex > 0)
        {
            FillShiftName(Convert.ToInt32(ddlLocation.SelectedValue), ddlShift);
            UserList(Convert.ToInt32(ddlLocation.SelectedValue), 0);
        }
    }
    protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
    {
        UserList(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlShift.SelectedValue));
    }
    #endregion

    #region "Fill Function"
    /// <summary>
    /// To Fill the list box With user name.
    /// </summary>
    /// <param name="locId"></param>
    /// <param name="shiftid"></param>
    protected void UserList(int locId, int shiftid)
    {
        objShiftMaster.LocationID = locId;
        objShiftMaster.ShiftID = shiftid;
        IList<Shift> objlstplink = ObjAdminBal.FillUserShiftWise(objShiftMaster);
        selRUser.DataSource = objlstplink;
        selRUser.DataValueField = "UserId";
        selRUser.DataTextField = "Fullname";
        selRUser.DataBind();
        selRUser.Items.Insert(0, "--Select--");
    }
    /// <summary>
    /// To Fill The Location.
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
            if (ddl.ID == "ddlLoc")
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
    /// To Fill the Shift name.
    /// </summary>
    /// <param name="LocId"></param>
    /// <param name="ddl"></param>
    protected void FillShiftName(int LocId, DropDownList ddl)
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
    #endregion

    #region "Gridview Events"
    /// <summary>
    /// To show the sL No and To change the color of Shift Name. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GrdShiftAssign_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Convert.ToString((GrdShiftAssign.PageSize * GrdShiftAssign.PageIndex) + (e.Row.RowIndex + 1));

            if (e.Row.Cells[5].Text == "Shift Not Assigned")
            {
                e.Row.Cells[5].Text = "<b> <font color='Maroon'>&nbsp;&nbsp;" + e.Row.Cells[5].Text + "</font></b>";
            }
            else
            {
                e.Row.Cells[5].Text = "<font color='Maroon'>&nbsp;&nbsp;" + e.Row.Cells[5].Text + "</font>";
            }

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
    /// <summary>
    /// function Created By Biswaranjanon 4-oct-2010
    /// Purpose:To show the layers after the search button is clicked.
    /// </summary>
    public void FillControl(string strDeptID)
    {
        if (strDeptID != "0")
        {

            Session["intid"] = strDeptID;//DepartmentOfUser();
            SetpermissionProperties.hidLevels = ((HiddenField)HierarchyForAllLocation2.FindControl("shidLevels")).ClientID;
            CommonProperties.PIds = Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"]))) - 2;
            //Session["hidLevel"]=((HiddenField)getUsers.FindControl("shidLevels")).ClientID;        
            //Session["PosId"] = Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"]))) - 2;
            DropDownList ddlLayer1 = ((DropDownList)HierarchyForAllLocation2.FindControl("sdrplayers0"));
            HiddenField hidId1 = (HiddenField)HierarchyForAllLocation2.FindControl("shidIDs0");

            int IntParId = Convert.ToInt32(Session["intid"]);
            for (int k = 0; k < Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"]))); k++)
            {
                if (k == 0)
                {
                    DropDownList ddlTofill = (DropDownList)HierarchyForAllLocation2.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"]))) - 1));
                    HiddenField hidId = (HiddenField)HierarchyForAllLocation2.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"]))) - 1));
                    Label lblId = (Label)HierarchyForAllLocation2.FindControl("Labels" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(Session["intid"])))));
                    CommonFunction.FillControls(Convert.ToInt32(Session["intid"]), ddlTofill, hidId, lblId, ddlLayer1, hidId1);
                }
                else
                {
                    DropDownList ddlTofill = (DropDownList)HierarchyForAllLocation2.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(IntParId)) - 1));
                    HiddenField hidId = (HiddenField)HierarchyForAllLocation2.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(IntParId)) - 1));
                    Label lblId = (Label)HierarchyForAllLocation2.FindControl("Labels" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(IntParId))));
                    CommonFunction.FillControls(IntParId, ddlTofill, hidId, lblId, ddlLayer1, hidId1);
                }
                IntParId = Convert.ToInt32(ObjAdminBal.GetParentId(IntParId));
            }
        }
    }

    protected void LnkbtnAllin_Click(object sender, EventArgs e)
    {
        if (LnkbtnAllin.Text == "All")
        {
            LnkbtnAllin.Text = "Paging";
            this.GrdShiftAssign.PageIndex = 0;
            GrdShiftAssign.AllowPaging = false;
            SearchEmployeeDetails();
            if (GrdShiftAssign.Rows.Count > 0)
            {
                this.lblpage.Text = "1-" + GrdShiftAssign.Rows.Count.ToString() + " Of " + GrdShiftAssign.Rows.Count.ToString();
            }
        }
        else
        {
            LnkbtnAllin.Text = "All";
            GrdShiftAssign.AllowPaging = true;
            SearchEmployeeDetails();
        }
      
    }

    protected void GrdShiftAssign_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdShiftAssign.PageIndex = e.NewPageIndex;
        SearchEmployeeDetails();
    }

    protected void TabShiftAssign_ActiveTabChanged(object sender, EventArgs e)
    {
        ResetUsercontrolddl(HierarchyForAllLocation2);
        ResetUsercontrolddl(getUsers1);
        GrdShiftAssign.Visible = false;
        ddlEshift.Visible = false;
        btnMove.Visible = false;
        btnDelete.Visible = false;
        lblSelectShift.Visible = false;
        LnkbtnAllin.Text = "All";
        GrdShiftAssign.AllowPaging = true;
        LnkbtnAllin.Visible = false;
        lblpage.Visible = false;
        ddlShift.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        selRUser.Items.Clear();
        selUser.Items.Clear();
        selUser.Items.Insert(0, "--Select--");
        txtsearch.Text = string.Empty;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetUsercontrolddl(getUsers1);
        ddlShift.SelectedIndex = 0;
        ddlLocation.SelectedIndex = 0;
        selRUser.Items.Clear();
        selUser.Items.Clear();
        selUser.Items.Insert(0, "--Select--");
    }

    protected void btnSearchReset_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "document.location.href='AdminShiftAssignment.aspx';", true);
    }
}
