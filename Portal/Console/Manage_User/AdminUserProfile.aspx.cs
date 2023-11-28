/********************************************************************************************************************
' File Name             :   AdminUserProfile.aspx.cs 
' Description           :   To Manage UserProfile
' Created by            :   Biswaranjan Das
' Created On            :   04-july-2010
' Modification History  :
'                           <CR no.>                     <Date>             <Modified by>                <Modification Summary>'                                                          
'                            1.                          02-May-2013         Dilip Tripathy               To change the navigate url as per creent tab name.                         
**********************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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
public partial class Admin_Manage_User_AdminUserProfile : System.Web.UI.Page
{
    #region "Variables"
    /// <summary>
    /// Declaring Variables
    /// </summary>
    User objuser = new User();
    AdminAppService ObjAdminBal = new AdminAppService();
   // CommonDLL objComnDll = new CommonDLL();
    IList<User> lstUser;
    public int RecCount;
    #endregion
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

        //Code Added By : Dilip Kumar Tripathy on dated 8-Mar-2012
        //Purpose : To clear the browser cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");

        



        GroupMasterProperties.hidlstid = "";
        GroupMasterProperties.hidbtnid = "";
        GroupMasterProperties.hidnval = "";
        CommonProperties.PageUrl = "AdminUserProfile.aspx";
        CommonProperties.PId = 0;
        CommonProperties.Type = "1";
        CommonProperties.PositionId = 0;
        CommonProperties.HierachyId = 0;
        AssignAdminProperties.hidadmin = "";
        SetpermissionProperties.hidLevel = null;
        CommonProperties.UserControlId = null;
        CommonProperties.UserControlId2 = null;

        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabContainer1.ActiveTab.HeaderText;

        if (!Page.IsPostBack)
        {

            //********Added By Dilip Kumar Tripathy on 12-Apr-2012********
            //******************Begin*****************************
            if (Request.QueryString["arg"] != null)
            {

                CommonProperties.DId = int.Parse(Request.QueryString["arg"].ToString());
                TabContainer1.ActiveTabIndex = 1;
                FillEditData("F", grdNewEmp, "", Request.QueryString["arg"].ToString());
                CommonProperties.Action = "U";
                CommonProperties.Type = "U";
                CommonProperties.PageUrl = "AdminUserProfile.aspx";
                CommonProperties.UserControlId2 = null;

            }
            else
            {
                CommonProperties.Action = "A";
                //btnInactive.Visible = false;
                btnDelete.Visible = false;
            }
            //******************End on Same Date*******************************
        }

    }
    protected void btnSearchActive_Click(object sender, EventArgs e)
    {
        FillGrid("T", grdActiveUser, txtEmpName);
    }
    protected void btnSearchNew_Click(object sender, EventArgs e)
    {
        FillGrid("F", grdNewEmp, txtSearchNew);//Active Employee
    }
    protected void btnSearchInactive_Click(object sender, EventArgs e)
    {
        FillGrid("I", grdInactiveUser, txtSearchInactive);//Ex_employee
    }
    //protected void btnInactive_Click(object sender, EventArgs e)
    //{
    //    int intOutput = 0;
    //    for (int i = 0; i < grdActiveUser.Rows.Count; i++)
    //    {
    //        CheckBox chkUser = (CheckBox)grdActiveUser.Rows[i].FindControl("chkUser");
    //        if (chkUser.Checked == true)
    //        {
    //            objuser.ActionCode = "I";
    //            objuser.GetID = Convert.ToInt32(grdActiveUser.DataKeys[i].Value);
    //            objuser.CreatedBy = Convert.ToInt32(Session["Userid"]);
    //            intOutput = Convert.ToInt32(objuser.DeActivateUser(objuser));
    //        }
    //    }
    //    string strOutmsg = StaticValues.message(intOutput, "User(s)");
    //    ScriptManager.RegisterStartupScript(this.upActiveTab, typeof(string), "", "alert('" + strOutmsg + "');", true);
    //    ClientScript.RegisterStartupScript(GetType(), "", "alert('" + strOutmsg + "');", true);
    //    FillGrid("T", grdActiveUser, txtEmpName);
    //}

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int intOutput = 0;
        for (int i = 0; i < grdNewEmp.Rows.Count; i++)
        {
            CheckBox chkUser = (CheckBox)grdNewEmp.Rows[i].FindControl("chkUser");
            if (chkUser.Checked == true)
            {
                objuser.ActionCode = "D";
                objuser.GetID = Convert.ToInt32(grdNewEmp.DataKeys[i].Value);
                objuser.CreatedBy = Convert.ToInt32(Session["Userid"]);
                intOutput = ObjAdminBal.DeleteUser(objuser);
            }
        }
        string strOutmsg = StaticValues.message(intOutput, "User(s)");
        ScriptManager.RegisterStartupScript(this.upEditTab, typeof(string), "", "alert('" + strOutmsg + "');", true);
        FillGrid("F", grdNewEmp, txtSearchNew);
    }

    #region "Member Functions"
    /// <summary>
    /// Method To Fill Gridview
    /// </summary>
    private void FillGrid(string strActionCode, GridView grdToFill, TextBox txtSearch)
    {
        //actionCode is "F"
        grdToFill.Visible = true;//Added By Biswaranjan on 9-Nov-2010
        objuser.ActionCode = strActionCode;
        objuser.FullName = txtSearch.Text.Trim();
        objuser.DepartmentID = DepartmentOfUser("V");
        if (objuser.DepartmentID == "")
        {
            objuser.DepartmentID = Request.QueryString["arg"].ToString();
        }
        lstUser = ObjAdminBal.GetAllUsers(objuser);
       
        grdToFill.DataSource = lstUser;
        grdToFill.DataBind();
        RecCount = lstUser.Count();
        DisplayPaging(grdToFill, RecCount, strActionCode);

        if (grdToFill.Rows.Count > 0)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                //btnInactive.Visible = true;

            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                btnDelete.Visible = true;
                btnEditInactive.Visible = true;
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                btnActive.Visible = true;
            }
        }
        else
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                // btnInactive.Visible = false;
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                btnDelete.Visible = false;
                btnEditInactive.Visible = false;
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                btnActive.Visible = false;
            }
        }
        //Added By Pratik On 20-Jul-2010
        if (strActionCode == "T")
        {
            SetpermissionProperties.hidLevels = ((HiddenField)HierarchyForAllLocation1.FindControl("shidLevels")).ClientID;

        }
        else if (strActionCode == "F")
        {
            SetpermissionProperties.hidLevels = ((HiddenField)HierarchyForAllLocation2.FindControl("shidLevels")).ClientID;

        }
        else if (strActionCode == "I")
        {
            SetpermissionProperties.hidLevels = ((HiddenField)HierarchyForAllLocation3.FindControl("shidLevels")).ClientID;

        }

    }

    private void FillEditData(string strActionCode, GridView grdToFill, string searchText, string deptId)
    {
        grdToFill.Visible = true;//Added By Biswaranjan on 9-Nov-2010
        objuser.ActionCode = strActionCode;
        objuser.FullName = searchText;
        objuser.DepartmentID = deptId;

        lstUser = ObjAdminBal.GetAllUsers(objuser);
        grdToFill.DataSource = null;
        grdToFill.DataBind();
        grdToFill.PageIndex = Convert.ToInt32(Request.QueryString["rindex"].ToString());
        grdToFill.DataSource = lstUser;
        grdToFill.DataBind();
        RecCount = lstUser.Count();
        DisplayPaging(grdToFill, RecCount, strActionCode);

        if (grdToFill.Rows.Count > 0)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                //btnInactive.Visible = true;

            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                btnEditInactive.Visible = true;
                btnDelete.Visible = true;
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                btnActive.Visible = true;
            }
        }
        else
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                // btnInactive.Visible = false;
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                btnDelete.Visible = false;
            }
        }
        //Added By Pratik On 20-Jul-2010
        if (strActionCode == "T")
        {
            SetpermissionProperties.hidLevels = ((HiddenField)HierarchyForAllLocation1.FindControl("shidLevels")).ClientID;

        }
        else if (strActionCode == "F")
        {
            SetpermissionProperties.hidLevels = ((HiddenField)HierarchyForAllLocation2.FindControl("shidLevels")).ClientID;

        }
        else if (strActionCode == "I")
        {
            SetpermissionProperties.hidLevels = ((HiddenField)HierarchyForAllLocation3.FindControl("shidLevels")).ClientID;

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
    /// <summary>
    /// To get Department Id Of User from dropdownlist 
    /// Added By Pratik On 20-Jul-2010
    /// </summary>
    /// <returns></returns>
    private string DepartmentOfUser(string strUCtrl)
    {

        string strDept = "";
        int intLayer = 0;
        if (TabContainer1.ActiveTabIndex == 0)
        {
            intLayer = Convert.ToInt32(((HiddenField)HierarchyForAllLocation1.FindControl("shidLevels")).Value);
        }
        else if (TabContainer1.ActiveTabIndex == 1)
        {
            if (((HiddenField)HierarchyForAllLocation2.FindControl("shidLevels")).Value != "")
            {
                intLayer = Convert.ToInt32(((HiddenField)HierarchyForAllLocation2.FindControl("shidLevels")).Value);
            }
            else
            {
                intLayer = 1;
            }
        }
        else if (TabContainer1.ActiveTabIndex == 2)
        {
            intLayer = Convert.ToInt32(((HiddenField)HierarchyForAllLocation3.FindControl("shidLevels")).Value);
        }
        HiddenField hdnDeptOfUser = null;
        if (strUCtrl == "A")
        {
            for (int i = 0; i < intLayer; i++)
            {
                if (TabContainer1.ActiveTabIndex == 0)
                {
                    hdnDeptOfUser = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs" + Convert.ToString(i));
                }
                else if (TabContainer1.ActiveTabIndex == 1)
                {
                    hdnDeptOfUser = (HiddenField)HierarchyForAllLocation2.FindControl("shidIDs" + Convert.ToString(i));
                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    hdnDeptOfUser = (HiddenField)HierarchyForAllLocation3.FindControl("shidIDs" + Convert.ToString(i));
                }
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
                if (TabContainer1.ActiveTabIndex == 0)
                {
                    hdnDeptOfUser = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs" + Convert.ToString(i));
                }
                else if (TabContainer1.ActiveTabIndex == 1)
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
                            levelArray[k] = dtLevel1.Rows[k][5].ToString();
                        }
                        if (!levelArray.Contains(hdnDeptOfUser.Value))
                        {
                            hdnDeptOfUser.Value = "";
                        }
                    }

                }
                else if (TabContainer1.ActiveTabIndex == 2)
                {
                    hdnDeptOfUser = (HiddenField)HierarchyForAllLocation3.FindControl("shidIDs" + Convert.ToString(i));
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

    /// <summary>
    /// Method to populate dropdownlist controls
    /// Added By Pratik On 20-Jul-2010
    /// </summary>
    /// <param name="intDeptId"></param>
    /// <returns></returns>
    private void FillControls(string strActionCode, int intDepartmentId)
    {
        if (Convert.ToInt32(ObjAdminBal.GetParentId(intDepartmentId)) != 0)
        {
            DropDownList ddlTofill = null;
            HiddenField hidId = null;
            Label lblId = null;
            if (strActionCode == "T")
            {
                ddlTofill = (DropDownList)HierarchyForAllLocation1.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId)) - 1));
                hidId = (HiddenField)HierarchyForAllLocation1.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId)) - 1));
                lblId = (Label)HierarchyForAllLocation1.FindControl("Labels" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId))));
            }
            else if (strActionCode == "F")
            {
                ddlTofill = (DropDownList)HierarchyForAllLocation2.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId)) - 1));
                hidId = (HiddenField)HierarchyForAllLocation2.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId)) - 1));
                lblId = (Label)HierarchyForAllLocation2.FindControl("Labels" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId))));
            }
            else if (strActionCode == "I")
            {
                ddlTofill = (DropDownList)HierarchyForAllLocation3.FindControl("sdrplayers" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId)) - 1));
                hidId = (HiddenField)HierarchyForAllLocation3.FindControl("shidIDs" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId)) - 1));
                lblId = (Label)HierarchyForAllLocation3.FindControl("Labels" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId))));
            }
            CommonFunction.fillDropdown(Convert.ToInt32(ObjAdminBal.GetParentId(intDepartmentId)), ddlTofill);
            ddlTofill.SelectedValue = Convert.ToString(intDepartmentId);
            hidId.Value = Convert.ToString(intDepartmentId);
            lblId.Text = CommonFunction.getLevelNames(intDepartmentId);
        }
    }
    #region***********Commented By Biswaranjan on 9-Nov-2010*****************
    //Purpose:To use the common function
    //************************Begin**************************************
    //private string getPosId(int intLvlDtlId)
    //{
    //    string strPosId = "";
    //    string StrQryLevel = "SELECT int_Position from M_Admin_Level where int_DeletedFlag<>1 and int_LevelId=(Select int_LevelId from M_Admin_LevelDetails where int_DeletedFlag<>1 and  int_leveldetailid=" + intLvlDtlId + ")";
    //    IDataReader objDrLevel = (IDataReader)objComnDll.ExeReader("ConnectionString", StrQryLevel, 0);
    //    while (objDrLevel.Read())
    //    {
    //        strPosId = Convert.ToString(objDrLevel["int_Position"]);
    //    }
    //    objDrLevel.Close();
    //    return strPosId;
    //}
    ///// <summary>
    ///// To get level names
    ///// Added By Pratik On 20-Jul-2010
    ///// </summary>
    ///// <param name="intDepartmentId"></param>
    //private string getLevelNames(int intDepartmentId)
    //{
    //    string strLvlName = null;
    //    string strQryLevelName = "Select nvch_Label from M_Admin_Level where int_DeletedFlag <> 1 and int_LevelId = (Select int_LevelId from M_Admin_LevelDetails where int_DeletedFlag <> 1 and int_LevelDetailId=" + intDepartmentId + ")";
    //    IDataReader objDrLevelName = (IDataReader)objComnDll.ExeReader("ConnectionString", strQryLevelName, 0);
    //    while (objDrLevelName.Read())
    //    {
    //        strLvlName = Convert.ToString(objDrLevelName["nvch_Label"]);
    //    }
    //    objDrLevelName.Close();
    //    return strLvlName;
    //}
    ///// <summary>
    ///// To populate dropdownlist
    ///// Added By Pratik On 20-Jul-2010
    ///// </summary>
    ///// <param name="intparentId"></param>
    ///// <param name="ddlToFill"></param>
    //private void fillDropdown(int intparentId, DropDownList ddlToFill)
    //{
    //    string StrQryLevel = "SELECT int_leveldetailid,nvch_LevelName from M_Admin_LevelDetails where int_PldId=" + intparentId + " and int_DeletedFlag<>1 ";
    //    objComnDll.PopupDropDown("ConnectionString", StrQryLevel, ddlToFill, 1);
    //}
    /// <summary>
    /// To get Parent level of a level
    /// Added By Pratik On 20-Jul-2010
    /// </summary>
    /// <param name="intLvlDtlId"></param>
    /// <returns></returns>
    //private string getParentId(int intLvlDtlId)
    //{
    //    string strPldId = "";
    //    string StrQryLevel = "SELECT int_PldId from M_Admin_LevelDetails where int_leveldetailid=" + intLvlDtlId + " and int_DeletedFlag<>1 ";
    //    IDataReader objDrLevel = (IDataReader)objComnDll.ExeReader("ConnectionString", StrQryLevel, 0);
    //    while (objDrLevel.Read())
    //    {
    //        strPldId = Convert.ToString(objDrLevel["int_PldId"]);
    //    }
    //    objDrLevel.Close();
    //    return strPldId;
    //}
    #endregion
    //***************Summery********************************
    //Function Name                 : ResetUsercontrolddl()
    //Purpose                       : To Reset The usercontrols dropdownlist
    //Parameters Name               : UsercontrolToReset
    //Parameters Datatype           : Control
    //Out Parameters Name           : None
    //Out Parameters Name Datatype  : None
    //Returns                       : None
    //Retun Datatype                : None
    //Created By                    : Biswaranjan Dash
    //Created Date                  : 9-Nov-2010
    //*****************************************************
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
    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {
        ResetUsercontrolddl(HierarchyForAllLocation2);
        ResetUsercontrolddl(HierarchyForAllLocation1);
        ResetUsercontrolddl(HierarchyForAllLocation3);
        txtEmpName.Text = "";
        txtSearchInactive.Text = "";
        txtSearchNew.Text = "";
        //********************Commented/Added By Biswaranjan on 9-Nov-2010*************
        //****************************Begin********************************************
        //grdActiveUser.DataSource = null;
        //grdActiveUser.DataBind();
        grdActiveUser.Visible = false;
        //btnInactive.Visible = false;
        //grdInactiveUser.DataSource = null;
        //grdInactiveUser.DataBind();
        grdInactiveUser.Visible = false;
        //grdNewEmp.DataSource = null;
        // grdNewEmp.DataBind();
        grdNewEmp.Visible = false;
        //****************************Begin********************************************
        btnDelete.Visible = false;
        btnEditInactive.Visible = false;
        hidTabindex.Value = Convert.ToString(TabContainer1.ActiveTabIndex);//Added By Biswaranjan on 16-Nov-2010

        if (TabContainer1.ActiveTabIndex == 0)
        {
            this.lblActivePaging.Visible = false;
            lnkActiveAll.Visible = false;
        }
        else if (TabContainer1.ActiveTabIndex == 1)
        {
            this.lblpage.Visible = false;
            LnkbtnAllin.Visible = false;
        }
        else if (TabContainer1.ActiveTabIndex == 2)
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
            btnActive.Visible = false;
        }
    }
    #endregion

    private void DisplayPaging(GridView gridviewone, int totalRowCount, string actionCode)
    {
        if (gridviewone.Rows.Count > 0)
        {
            if (actionCode == "T")
            {
                this.lblActivePaging.Visible = true;
                lnkActiveAll.Visible = true;
                lblActivePaging.Text = CommonFunction.ShowGridPaging(gridviewone, gridviewone.PageSize, gridviewone.PageIndex, totalRowCount);
            }
            else if (actionCode == "F")
            {
                this.lblpage.Visible = true;
                LnkbtnAllin.Visible = true;
                lblpage.Text = CommonFunction.ShowGridPaging(gridviewone, gridviewone.PageSize, gridviewone.PageIndex, totalRowCount);
            }
            else if (actionCode == "I")
            {
                this.lblPaging.Visible = true;
                lbtnAll.Visible = true;
                lblPaging.Text = CommonFunction.ShowGridPaging(gridviewone, gridviewone.PageSize, gridviewone.PageIndex, totalRowCount);
            }
        }
        else
        {
            if (actionCode == "T")
            {
                this.lblActivePaging.Visible = false;
                lnkActiveAll.Visible = false;
            }
            else if (actionCode == "F")
            {
                this.lblpage.Visible = false;
                LnkbtnAllin.Visible = false;
            }
            else if (actionCode == "I")
            {
                this.lblPaging.Visible = false;
                lbtnAll.Visible = false;
            }
        }
    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            this.grdInactiveUser.PageIndex = 0;
            grdInactiveUser.AllowPaging = false;
            FillGrid("I", grdInactiveUser, txtSearchInactive);

            if (grdInactiveUser.Rows.Count > 0)
            {
                this.lblPaging.Text = "1-" + grdInactiveUser.Rows.Count.ToString() + " Of " + grdInactiveUser.Rows.Count.ToString();
            }
        }
        else
        {
            lbtnAll.Text = "All";
            grdInactiveUser.AllowPaging = true;
            FillGrid("I", grdInactiveUser, txtSearchInactive);

        }
 
    }

    protected void lnkActiveAll_Click(object sender, EventArgs e)
    {
        if (lnkActiveAll.Text == "All")
        {
            lnkActiveAll.Text = "Paging";
            this.grdActiveUser.PageIndex = 0;
            grdActiveUser.AllowPaging = false;
            FillGrid("T", grdActiveUser, txtEmpName);
            if (grdActiveUser.Rows.Count > 0)
            {
                this.lblActivePaging.Text = "1-" + grdActiveUser.Rows.Count.ToString() + " Of " + grdActiveUser.Rows.Count.ToString();
            }
        }
        else
        {
            lnkActiveAll.Text = "All";
            grdActiveUser.AllowPaging = true;
            FillGrid("T", grdActiveUser, txtEmpName);
        }
        
    }

    protected void LnkbtnAllin_Click(object sender, EventArgs e)
    {
        if (LnkbtnAllin.Text == "All")
        {
            LnkbtnAllin.Text = "Paging";
            this.grdNewEmp.PageIndex = 0;
            grdNewEmp.AllowPaging = false;
            FillGrid("F", grdNewEmp, txtSearchNew);

            if (grdNewEmp.Rows.Count > 0)
            {
                this.lblpage.Text = "1-" + grdNewEmp.Rows.Count.ToString() + " Of " + grdNewEmp.Rows.Count.ToString();
            }
        }
        else
        {
            LnkbtnAllin.Text = "All";
            grdNewEmp.AllowPaging = true;
            FillGrid("F", grdNewEmp, txtSearchNew);

        }
    }

    protected void grdActiveUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
 
            (e.Row.FindControl("lblSlno2") as Label).Text = (grdActiveUser.PageIndex * grdActiveUser.PageSize + e.Row.RowIndex + 1).ToString();
        }

    }

    protected void grdActiveUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdActiveUser.PageIndex = e.NewPageIndex;
        FillGrid("T", grdActiveUser, txtEmpName);
    }

    protected void grdNewEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblSlno") as Label).Text = (grdNewEmp.PageIndex * grdNewEmp.PageSize + e.Row.RowIndex + 1).ToString();
            string userId = grdNewEmp.DataKeys[e.Row.RowIndex].Value.ToString();
            HyperLink objHyper = e.Row.FindControl("hypEdit") as HyperLink;
            objHyper.NavigateUrl = "AdminAddUser.aspx?UID=" + CommonFunction.EncryptData(userId) + "&Pindex=" + grdNewEmp.PageIndex;
        }
    }

    protected void grdNewEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdNewEmp.PageIndex = e.NewPageIndex;
        FillGrid("F", grdNewEmp, txtSearchNew);
    }

    protected void grdInactiveUser_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grdInactiveUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblSlno3") as Label).Text = (grdInactiveUser.PageIndex * grdInactiveUser.PageSize + e.Row.RowIndex + 1).ToString();
            //FillGrid("I", grdInactiveUser, txtSearchInactive);
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        int intOutput = 0;
        for (int i = 0; i < grdInactiveUser.Rows.Count; i++)
        {
            CheckBox chkUser = (CheckBox)grdInactiveUser.Rows[i].FindControl("chkInactiveUser");
            if (chkUser.Checked == true)
            {
                objuser.ActionCode = "T";
                objuser.GetID = Convert.ToInt32(grdInactiveUser.DataKeys[i].Value);
                objuser.CreatedBy = Convert.ToInt32(Session["Userid"]);
                intOutput = ObjAdminBal.ActivateUser(objuser);
            }
        }
        string strOutmsg = StaticValues.message(intOutput, "User(s)");
        ScriptManager.RegisterStartupScript(this.upTabInactive, typeof(string), "", "alert('" + strOutmsg + "');", true);
        FillGrid("I", grdInactiveUser, txtEmpName);
    }
    protected void btnEditInactive_Click1(object sender, EventArgs e)
    {
        int intOutput = 0;
        for (int i = 0; i < grdNewEmp.Rows.Count; i++)
        {
            CheckBox chkUser = (CheckBox)grdNewEmp.Rows[i].FindControl("chkUser");
            if (chkUser.Checked == true)
            {
                objuser.ActionCode = "I";
                objuser.GetID = Convert.ToInt32(grdNewEmp.DataKeys[i].Value);
                objuser.CreatedBy = Convert.ToInt32(Session["Userid"]);
                intOutput = ObjAdminBal.DeActivateUser(objuser);
            }
        }
        string strOutmsg = StaticValues.message(intOutput, "User(s)");
        ScriptManager.RegisterStartupScript(this.upEditTab, typeof(string), "", "alert('User Successfully Modified To Ex-Employee');", true);
        FillGrid("F", grdNewEmp, txtEmpName);
    }

    protected void grdInactiveUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdInactiveUser.PageIndex = e.NewPageIndex;
        FillGrid("I", grdInactiveUser, txtEmpName);
    }

    protected void grdActiveUser_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        grdActiveUser.PageIndex = e.NewPageIndex;
        FillGrid("T", grdActiveUser, txtEmpName);
    }

    protected void grdNewEmp_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        grdNewEmp.PageIndex = e.NewPageIndex;
        FillGrid("F", grdNewEmp, txtEmpName);
    }




}

