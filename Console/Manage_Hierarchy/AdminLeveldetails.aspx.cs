/********************************************************************************************************************
' File Name             :   AdminFunctionMaster.aspx.cs
' Description           :   To Add/edit/Delete Level Details of Hierarchy.
' Created by            :   Subhasis Kumar dash
' Created On            :   11-Jul-2010
' Modification History  :
'                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
'                            1                             29-sept-2010       Biswaranjan                  gridview pageing and html cancel button in update case.                                    
'                            2                             1-oct-2010         Biswaranjan                  code to set the default button of the page.
'                            3                             19-oct-2010        Biswaranjan                  Create the function ShowDetails().  modify the funtion Fillhierachy() to store the dropdown and hidval in a static variable and 
'
'                            4                             6-Nov-2010         Biswaranjan                  for modification required by bugs report
'                            5                             8-Nov-2010         Biswaranjan                  To modify the  function ResetUsercontrolDDL() by passing parameter to the function and migrate the common function
' Function Name         : AddFunctionmaster(),EditFunctionMaster(),ToInActivate(),fillFunction(),showActiveGrid  
' Procedures Used       : usp_Function_Manage,usp_Function_View   
' User Defined Namespace:   
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Admin.CommonFunction;
//using CSMPDK_3_0;
using KWAdminConsole.Messages;
using Manage_Usercontrol_Property;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using System.Reflection; 
public partial class Admin_Manage_Hierarchy_AdminLeveldetails : System.Web.UI.Page
{
    # region "Varaiable"
    AdminAppService Objadminbal = new AdminAppService();
    Level objLevelDetails = new Level();
    //CommonDLL objcmndll = new CommonDLL();
    int intLeveldetailid = 0;
    DropDownList drp1 = new DropDownList();
    HiddenField hid1 = new HiddenField();
    public string strName;
    public static string strPosition;
    /// <summary>
    /// Declaring Variables
    /// </summary>

   
    int returnvalue = 0;
    string strmsg = null;

    string strNdName = "";
    string strNodeHID = "";
    string strNodeLID = "";
    public string strNodePID = "";
    public string strAtt = "";
   
    Admin_PopulateHierarchy objpophier = new Admin_PopulateHierarchy();
    public static string statDropdownval, statHidval = null;
    public int RecCount;
# endregion
   
    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        //Code Added By : Dilip Kumar Tripathy on dated  9-Feb-2012
        //Purpose : To clear the browser cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
        lbtnAll.Visible = false;
        lblPaging.Visible = false;
        if (Session["userName"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }


        
        if (TabLevelDetails.ActiveTabIndex == 0)
        {
            PopulateHierarchyProperty.Requests = "";
        }
        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabLevelDetails.ActiveTab.HeaderText;
        if (!IsPostBack)
        {

            TabLevelDetails.ActiveTabIndex = 1;
            btnDelete.Visible = false;
            PopulateHierarchyProperty.PageUrl = "AdminLeveldetails.aspx";
            if (Request.QueryString["LvlID"] != null)
            {
                TabLevelDetails.ActiveTabIndex = 0;
                FillLevelData();
                TabCreateLevel.HeaderText = "UPDATE";
                AdminConsoleNavigation.strNewLink = ">>" + "UPDATE";
                btnAdd.Text = "Update";
                //btnid.Visible = true;
                // btnid.Value = "Cancel";
                btnReset.Text = "Cancel";
            }
            else
            {
                //btnid.Visible = false;
                TabCreateLevel.HeaderText = "CREATE";
                btnAdd.Text = "Save";
                //TabLevelDetails.ActiveTabIndex = 0;
            }
            if (Request.QueryString["att"] != null)
            {
                hidAtt.Value = CommonFunction.DecryptData(Request.QueryString["att"].ToString());
                string[] objAr = hidAtt.Value.Split('|');
                strNdName = objAr[0].ToString();
                strNodeHID = objAr[1].ToString();
                strNodeLID = objAr[2].ToString();
                strNodePID = objAr[3].ToString();
                PopulateHierarchyProperty.HierachyId = Convert.ToInt32(strNodeHID);
                PopulateHierarchyProperty.PositionId = Convert.ToInt32(strNodePID);
                PopulateHierarchyProperty.LocationId = Convert.ToInt32(strNodeLID);//added by Dilip Tripathy on dated 7-Apr-2012
                ViewState["posId"] = strNodePID;
            }

        }


        //Code add by biswaranjan on 1-oct-2010 to  set the default button of the page
        if ((TabLevelDetails.ActiveTabIndex == 0) && (btnAdd.Enabled == true))
        {
            this.btnAdd.Focus();

            //strscript = "<script>DefaultFocus('" + this.btnAdd.ClientID + "');</script>";

        }
        else if (TabLevelDetails.ActiveTabIndex == 1)
        {
            //this.btnShow.Focus(); //commented by Dilip Tripathy from true to false on 7-Mar-2012
            //strscript = "<script>DefaultFocus('" + this.btnShow.ClientID + "');</script>";
        }
        //Page.RegisterClientScriptBlock("javascript", strscript);
        if (strNdName != null)
        {
            strName = strNdName;
        }
        else
        {
            strName = "Test";
        }

    }
    #endregion

    #region "Button Events"
    /// <summary>
    /// To Add Level details Record.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (btnAdd.Text == "Update")
        {
            EditLevelData();
            ShowDetails();
            AdminConsoleNavigation.strNewLink = ">>" + "VIEW";
        }
        else if (btnAdd.Text == "Save")
        {
            AddLeveldetails();
        }
    }
    /// <summary>
    /// To Clear all fields for insert and update.
    /// Created Date : 13-Jun-2012 
    /// Created By : Dilip Kumar Tripathy.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ClearFields()
    {
        ResetUsercontrolDDL(PopulateHirarchy2);
        txtName.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtAddress3.Text = "";
        txtTel.Text = "";
        txtFax.Text = "";
    }
    /// <summary>
    /// To Delete Level details Record.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TabLevelDetails.ActiveTabIndex = 1;
        DeleteLevel();
    }
    /// <summary>
    /// To Show Level details Record.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShow_Click(object sender, EventArgs e)
    {
        Fillhierachy();
    }
    /// <summary>
    /// To Reset/clear Level details Record.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        //code added by Dilip Kumar Tripathy On dated 3-3-2012
        if (btnReset.Text == "Reset")
        {
            ResetControl();
        }
        else
        {
            ShowDetails();
            AdminConsoleNavigation.strNewLink = ">>" + "VIEW";

        }

    }
    #endregion

    #region "Grid Events"
    /// <summary>
    /// To Increment the Serial number in gridview.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GVLvldetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.Cells[2].FindControl("hypName") as HyperLink).Text = CommonFunction.GetDecodedData((e.Row.Cells[2].FindControl("hypName") as HyperLink).Text);
            e.Row.Cells[1].Text = Convert.ToString((GVLvldetails.PageSize * GVLvldetails.PageIndex) + (e.Row.RowIndex + 1));
            
        }
    }
    protected void GVLvldetails_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        GVLvldetails.PageIndex = e.NewPageIndex;
        Fillhierachy();
    }
    #endregion

    #region "User functions"
    /// <summary>
    /// To Add the Level Details record.
    /// </summary>
    protected void AddLeveldetails()
    {
        try
        {
            if (strNodePID == "1")
            {
                objLevelDetails.Parentid = 0;
            }
            else
            {
                DropDownList drp = new DropDownList();
                drp = null;
                HiddenField hid = new HiddenField();
                hid.Value = "";
                if (PopulateHierarchyProperty.PositionId == 2)
                {
                    drp = (DropDownList)PopulateHirarchys.FindControl("drpLocation");
                }
                else if (PopulateHierarchyProperty.PositionId == 3)
                {
                    drp = (DropDownList)PopulateHirarchys.FindControl("drplayer1");
                }
                else
                {
                    drp = (DropDownList)PopulateHirarchys.FindControl("drplayer" + (PopulateHierarchyProperty.PositionId - 2).ToString());
                    //hid = (HiddenField)PopulateHirarchys.FindControl("hidID" + (PopulateHierarchyProperty.PositionId - 2));
                }
                objLevelDetails.Parentid = Convert.ToInt32(drp.SelectedValue);
                //if (PopulateHierarchyProperty.PositionId > 3)
                //{
                //    objLevelDetails.Parentid = Convert.ToInt32(hid.Value);
                //    // objLevelDetails.Parentid = Convert.ToInt32(drp.SelectedValue); 
                //}
                //else
                //{
                //    objLevelDetails.Parentid = Convert.ToInt32(drp.SelectedValue);
                //}
            }
            objLevelDetails.ActionCode = "A";
            objLevelDetails.LeveldetailsID = 0;
            objLevelDetails.LevelName = CommonFunction.GetEncodedData(txtName.Text.Trim());
            objLevelDetails.Levelid = PopulateHierarchyProperty.LocationId;
            string straddress = txtAddress1.Text.Trim() + "~" + txtAddress2.Text.Trim() + "~" + txtAddress3.Text.Trim() + "~";
            straddress = CommonFunction.GetEncodedData(straddress);//Added by Dilip Tripathy on 9/1/2012
            objLevelDetails.Address = straddress;
            objLevelDetails.TelNo = txtTel.Text.Trim();
            objLevelDetails.FaxNo = txtFax.Text.Trim();
            objLevelDetails.DISECode = txtHierarchyCode.Text;
            objLevelDetails.Strflag = ConfigurationManager.AppSettings["SetFlag"];//"Y";
            objLevelDetails.CreatedBy = Convert.ToInt32(Session["UserId"]);
            returnvalue = Convert.ToInt32(Objadminbal.AddLevelDetails(objLevelDetails));
            strmsg = StaticValues.message(returnvalue, "Level Details");
            //Begin : Added by Subrat Acharaya on 14/10/2010
            int intUserId;
            intUserId = Convert.ToInt32(Session["UserId"]);
            //intHierRarchyLevel = CommonFunction.intHierachyLevel(intUserId);
            //// ****************Commented By Biswaranjan on 6-Nov-2010 instructed by Subrat Acharya sir*******************
            //// Purpose:Not Required
            //// ******************Begin*******************************
            //if (intHierRarchyLevel == 4)
            //{
            //    layer4("Location", "Divison");
            //}
            //if (intHierRarchyLevel == 3)
            //{
            //    layer3("Location", "Divison");
            //}
            //******************Begin*******************************
            //Ended By Subrat Acharya
            ResetControl();
            string strtemp = "alert('" + strmsg + "');";
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "onshow", strtemp, true);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        TabLevelDetails.ActiveTabIndex = 0;
    }
    /// <summary>
    /// To Delete the Level Details record.
    /// </summary>
    protected void DeleteLevel()
    {
        int rcount = GVLvldetails.Rows.Count;
        int strdata;
        for (int i = 0; i < rcount; i++)
        {
            if (((CheckBox)GVLvldetails.Rows[i].Cells[0].FindControl("cbItem")).Checked == true)
            {
                strdata = Convert.ToInt32(GVLvldetails.DataKeys[i].Values[0].ToString());
                objLevelDetails.ActionCode = "D";
                objLevelDetails.LeveldetailsID = strdata;
                objLevelDetails.CreatedBy = Convert.ToInt32(Session["UserId"]);
                returnvalue = Convert.ToInt32(Objadminbal.DeleteLevelDetails(objLevelDetails));
            }
        }


        if (returnvalue == 3)
        {
            strmsg = StaticValues.message(returnvalue, "Level Details");
            //Commneted By Amrita on 22 Sep 2010 to load the view after every deletion//
            //ClientScript.RegisterStartupScript(GetType(), "", "alert('" + strmsg + "');document.location.href('AdminLeveldetails.aspx');", true);
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');", true);
        }
        else
        {
            if (returnvalue == 20)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Level can not be deleted.Child items are present');", true);
            }
            else if (returnvalue == 21)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('User is present.So can not be deleted.');", true);

            }
        }
        Fillhierachy();
    }
    /// <summary>
    /// To Edit the Level Details record.
    /// </summary>
    protected void EditLevelData()
    {
        try
        {
            string straddress = null;
            objLevelDetails.ActionCode = "U";

            DropDownList drp = new DropDownList();
            drp = null;
            HiddenField hid = new HiddenField();
            hid.Value = "";
            if (PopulateHierarchyProperty.PositionId == 2)
            {
                drp = (DropDownList)PopulateHirarchys.FindControl("drpLocation");
            }
            else if (PopulateHierarchyProperty.PositionId == 3)
            {
                drp = (DropDownList)PopulateHirarchys.FindControl("drplayer1");
            }
            else
            {
                drp = (DropDownList)PopulateHirarchys.FindControl("drplayer" + (PopulateHierarchyProperty.PositionId - 2).ToString());
                // hid = (HiddenField)PopulateHirarchys.FindControl("hidID" + (PopulateHierarchyProperty.PositionId - 2));
            }
            objLevelDetails.Parentid = Convert.ToInt32(drp.SelectedValue);
            //if (PopulateHierarchyProperty.PositionId > 3)
            //{
            //    objLevelDetails.Parentid = Convert.ToInt32(hid.Value);
            //    // objLevelDetails.Parentid = Convert.ToInt32(drp.SelectedValue); 
            //}
            //else
            //{
            //    objLevelDetails.Parentid = Convert.ToInt32(drp.SelectedValue);
            //}
            objLevelDetails.Levelid = PopulateHierarchyProperty.LocationId;
            objLevelDetails.LeveldetailsID = Convert.ToInt32(Request.QueryString["LvlID"]);
            objLevelDetails.LevelName = txtName.Text.Trim();
            straddress = txtAddress1.Text.Trim() + "~" + txtAddress2.Text.Trim() + "~" + txtAddress3.Text.Trim() + "~";
            objLevelDetails.Address = straddress;
            objLevelDetails.TelNo = txtTel.Text.Trim();
            objLevelDetails.FaxNo = txtFax.Text.Trim();
            objLevelDetails.DISECode = txtHierarchyCode.Text.Trim();
            objLevelDetails.Strflag = ConfigurationManager.AppSettings["SetFlag"];
            objLevelDetails.CreatedBy = Convert.ToInt32(Session["UserId"]);
            returnvalue = Convert.ToInt32(Objadminbal.EditLevelDetails(objLevelDetails));
            strmsg = StaticValues.message(returnvalue, "Level Details");
            //Begin : Added by Subrat Acharaya on 14/10/2010
            int intUserId;
            intUserId = Convert.ToInt32(Session["UserId"]);
            //intHierRarchyLevel = CommonFunction.intHierachyLevel(intUserId);
            ////****************Commented By Biswaranjan on 6-Nov-2010 instructed by Subrat Acharya sir*******************
            ////Purpose:Not Required
            //// ******************Begin*******************************
            //if (intHierRarchyLevel == 4)
            //{
            //    layer4("Location", "Divison");
            //}
            //if (intHierRarchyLevel == 3)
            //{
            //    layer3("Location", "Divison");
            //}
            // ******************End*******************************
            //Ended By Subrat Acharya
            ClearFields();
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strmsg + "');", true);


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    #region Commented By Biswaranjan on 8-Nov-2010
    //*******************Commented By Biswaranjan on 8-Nov-2010**************
    //Purpose:To use the commonfunction
    //*******************Begin*************************
    //protected string getPosId(int intLvlDtlId)
    //{
    //    string strPosId = "";
    //    string StrQryLevel = "SELECT int_Position from M_Admin_Level where int_DeletedFlag<>1 and int_LevelId=(Select int_LevelId from M_Admin_LevelDetails where int_DeletedFlag<>1 and  int_leveldetailid=" + intLvlDtlId + ")";
    //    IDataReader objDrLevel = (IDataReader)objcmndll.ExeReader("ConnectionString", StrQryLevel, 0);
    //    while (objDrLevel.Read())
    //    {
    //        strPosId = Convert.ToString(objDrLevel["int_Position"]);
    //    }
    //    objDrLevel.Close();
    //    return strPosId;
    //}
    //*******************End*************************
    //*******************Commented By Biswaranjan on 8-Nov-2010**************
    //Purpose:To use the commonfunction
    //*******************Begin*************************
    //protected string getParentId(int intLvlDtlId)
    //{
    //    string strPldId = "";
    //    string StrQryLevel = "SELECT int_PldId from M_Admin_LevelDetails where int_leveldetailid=" + intLvlDtlId + " and int_DeletedFlag<>1 ";
    //    IDataReader objDrLevel = (IDataReader)objcmndll.ExeReader("ConnectionString", StrQryLevel, 0);
    //    while (objDrLevel.Read())
    //    {
    //        strPldId = Convert.ToString(objDrLevel["int_PldId"]);
    //    }
    //    objDrLevel.Close();
    //    return strPldId;
    //}
    //*******************End*************************
    //******************************Commented By Biswaranjan on 6-Nov-2010********************
    //Purose:To Use Common function
    //***********************************Begin************************************************
    //private void fillDropdown(int intparentId, DropDownList ddlToFill)
    //{
    //    string StrQryLevel = "SELECT int_leveldetailid,nvch_LevelName from M_Admin_LevelDetails where int_PldId=" + intparentId + " and int_DeletedFlag<>1 ";
    //    objcmndll.PopupDropDown("ConnectionString", StrQryLevel, ddlToFill, 1);
    //}
    //***********************************End************************************************
    #endregion
    private void FillControls(int intDepartmentId)
    {
        if (Convert.ToInt32(Objadminbal.GetParentId(intDepartmentId)) != 0)
        {
            DropDownList ddlTofill = (DropDownList)PopulateHirarchys.FindControl("drplayer" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId)) - 1));
            HiddenField hidId = (HiddenField)PopulateHirarchys.FindControl("hidID" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId)) - 1));
            //fillDropdown(Convert.ToInt32(getParentId(intDepartmentId)), ddlTofill);
            CommonFunction.fillDropdown(Convert.ToInt32(Objadminbal.GetParentId(intDepartmentId)), ddlTofill);
            ddlTofill.SelectedValue = Convert.ToString(intDepartmentId);
            hidId.Value = Convert.ToString(intDepartmentId);
            Label lblId = (Label)PopulateHirarchys.FindControl("Label" + Convert.ToString(Convert.ToInt32(CommonFunction.getPosId(intDepartmentId))));
            lblId.Text = CommonFunction.getLevelNames(intDepartmentId);
            if (CommonFunction.getPosId(intDepartmentId) == "2")
            {
                PopulateHierarchyProperty.Requests = ddlTofill.SelectedValue;
            }
        }
        else
        {
            IList<Level> objlstplink = Objadminbal.FillHierachyControl();
            ((DropDownList)PopulateHirarchys.FindControl("drpLocation")).DataValueField = "SecId";
            ((DropDownList)PopulateHirarchys.FindControl("drpLocation")).DataTextField = "LevelName";
            ((DropDownList)PopulateHirarchys.FindControl("drpLocation")).DataSource = objlstplink;
            ((DropDownList)PopulateHirarchys.FindControl("drpLocation")).DataBind();
            ((DropDownList)PopulateHirarchys.FindControl("drpLocation")).SelectedValue = Convert.ToString(intDepartmentId);
            HiddenField hidId = (HiddenField)PopulateHirarchys.FindControl("hidID0");
            hidId.Value = Convert.ToString(intDepartmentId);
        }
    }
    #region Commented By Biswaranjan on 8-Nov-2010
    //*******************Commented By Biswaranjan on 8-Nov-2010**************
    //Purpose:To use the commonfunction
    //*******************Begin*************************
    //protected string getLevelNames(int intDepartmentId)
    //{
    //    string strLvlName = null;
    //    string strQryLevelName = "Select nvch_Label from M_Admin_Level where int_DeletedFlag <> 1 and int_LevelId = (Select int_LevelId from M_Admin_LevelDetails where int_DeletedFlag <> 1 and int_LevelDetailId=" + intDepartmentId + ")";
    //    IDataReader objDrLevelName = (IDataReader)objcmndll.ExeReader("ConnectionString", strQryLevelName, 0);
    //    while (objDrLevelName.Read())
    //    {
    //        strLvlName = Convert.ToString(objDrLevelName["nvch_Label"]);
    //    }
    //    objDrLevelName.Close();
    //    return strLvlName;
    //}
    //*******************Begin*************************
    #endregion
    //***************Summery********************************
    //Function Name :ShowDetails()
    //Purpose : To navigate to the view tab after update the record with hiearchy selected and gridview fill data
    //Parameters Name :None
    //Parameters Datatype : None
    //Returns:None
    //Retun Datatype : None
    //Created By : Biswaranjan Dash
    //Created Date : 19-oct-2010
    //*****************************************************

    protected void ShowDetails()
    {
        try
        {
            TabLevelDetails.ActiveTabIndex = 1;
            TabCreateLevel.HeaderText = "CREATE";
            // FillLevelData();
            GetAllHierarchyParents();
            Fillhierachy();
            //Code Added By Dilip Kumar Tripathy on dated 4-Apr-2012
            //Purpose : To bind the dropdown list properly after update button clicked.
            if (Session["dtAllPrents"] != null)
            {
                DataTable dtAllParents = (DataTable)Session["dtAllPrents"];
                if (dtAllParents.Rows.Count != 0)
                {
                    for (int i = 0; i < dtAllParents.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            DropDownList ddlToBind = PopulateHirarchy2.FindControl("drplayer1") as DropDownList;
                            ddlToBind.SelectedValue = dtAllParents.Rows[i][0].ToString();
                        }
                        else
                        {
                            DropDownList ddlLayer = new DropDownList();
                            string strNum = (i + 1).ToString();
                            ddlLayer = PopulateHirarchy2.FindControl("drplayer" + (i + 1)) as DropDownList;
                            ddlLayer.Items.Clear();
                            IList<Level> objlstplink = Objadminbal.FillHierachyControl2(Convert.ToInt32(dtAllParents.Rows[i - 1][0]));
                            ddlLayer.DataValueField = "SecId";
                            ddlLayer.DataTextField = "LevelName";
                            ddlLayer.DataSource = objlstplink;
                            ddlLayer.DataBind();
                            ddlLayer.SelectedValue = dtAllParents.Rows[i][0].ToString();
                        }
                    }
                }
            }
            Session["dtAllPrents"] = null;
            //-----------------Code End by Dilip------------------------------------------
            if (GVLvldetails.Rows.Count > 0)
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }
            ResetControl();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //***************Summery********************************
    //Function Name       : RsetUsercontrolDDL()
    //Purpose             : To Reset the Usercontrol's dropdownlist
    //Parameters Name     : usercontrolid
    //Parameters Datatype : Control
    //Returns             : None
    //Retun Datatype      : None
    //Created By          : Biswaranjan Dash
    //Created Date        : 19-oct-2010
    //*****************************************************
    protected void ResetUsercontrolDDL(Control usercontrolid)
    {
        try
        {
            for (int intCounter = 1; intCounter <= (PopulateHierarchyProperty.PositionId - 2); intCounter++)
            {
                if (intCounter != 1)
                {
                    DropDownList ddlToReset = (DropDownList)usercontrolid.FindControl("drplayer" + intCounter);
                    ddlToReset.Items.Clear();
                    ddlToReset.Items.Insert(0, "-Select-");
                    ddlToReset.SelectedIndex = 0;
                }
                else
                {
                    DropDownList ddlToReset = (DropDownList)usercontrolid.FindControl("drplayer" + intCounter);
                    ddlToReset.SelectedIndex = 0;
                }

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void ResetControl()
    {
        //objcmndll.ResetFunction(txtAddress1, txtAddress2, txtAddress3, txtFax, txtName, txtTel, txtHierarchyCode);
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtAddress3.Text = "";
        txtFax.Text = "";
        txtName.Text = "";
        txtTel.Text = "";
        txtHierarchyCode.Text = "";
        ResetUsercontrolDDL(PopulateHirarchys);
    }
    #endregion

    #region "Fill/Show Function"
    /// <summary>
    /// To fill The Hierarchy record in Gridview.
    /// </summary>
    public void Fillhierachy()
    {
        GVLvldetails.Visible = true;//Added By Biswaranjan on 6-Nov-2010
        if (PopulateHierarchyProperty.PositionId == 2)
        {
            drp1 = (DropDownList)PopulateHirarchy2.FindControl("drpLocation");
            if (drp1.SelectedValue != "" && drp1.SelectedValue != null)
            {
                statDropdownval = drp1.SelectedValue;
            }
        }
        else if (PopulateHierarchyProperty.PositionId == 3)
        {
            drp1 = (DropDownList)PopulateHirarchy2.FindControl("drplayer1");
            if (drp1.SelectedValue != "" && drp1.SelectedValue != null)
            {
                statDropdownval = drp1.SelectedValue;
            }
        }
        else
        {
            hid1 = (HiddenField)PopulateHirarchy2.FindControl("hidID" + (PopulateHierarchyProperty.PositionId - 2));
            if (hid1.Value != "" && hid1.Value != null)
            {
                statHidval = hid1.Value;
            }
        }
        if (PopulateHierarchyProperty.PositionId > 3)
        {
            intLeveldetailid = (hid1.Value != "" && hid1.Value != null) ? Convert.ToInt32(hid1.Value) : Convert.ToInt32(statHidval);
        }
        else
        {
            intLeveldetailid = (drp1.SelectedValue != "--Select--" && drp1.SelectedValue != null) ? Convert.ToInt32(drp1.SelectedValue) : 0;
            if (intLeveldetailid == 0 )
            {
                intLeveldetailid = int.Parse(ViewState["immediateParent"].ToString());
            }
        }
        IList<Level> objLevelList = new List<Level>();
        objLevelDetails.LeveldetailsID = intLeveldetailid;
        objLevelList = Objadminbal.FillLevelGrid(objLevelDetails);
        RecCount = objLevelList.Count;
        GVLvldetails.DataSource = objLevelList;
        GVLvldetails.DataBind();
        if (GVLvldetails.Rows.Count == 0)
        {
            btnDelete.Visible = false;
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;
        }
        else
        {
            btnDelete.Visible = true;
            //RecCount = GVLvldetails.Rows.Count;
            DisplayPaging(RecCount);
        }
    }
    /// <summary>
    /// To fill The Hierarchy record in Respected fields when clicking on Edit Button.
    /// </summary>
    /// 
    private void GetAllHierarchyParents()
    {
        objLevelDetails.Levelid = Convert.ToInt32(Request.QueryString["LvlID"].ToString());
        IList<Level> objlstplink = Objadminbal.GetAllHierarchyParents(objLevelDetails);
        DataTable dtAllParents = ListToDataTable(objlstplink);
        if (dtAllParents.Rows.Count > 0)
        {
            ViewState["immediateParent"] = dtAllParents.Rows[dtAllParents.Rows.Count - 1][0].ToString();
        }
        Session["dtAllPrents"] = dtAllParents;
    }
    public static DataTable ListToDataTable(IList<Level> list)
    {
        DataTable dt = new DataTable();

        foreach (PropertyInfo info in typeof(Level).GetProperties())
        {
            dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
        }
       
        foreach (Level t in list)
        {
            DataRow row = dt.NewRow();
            foreach (PropertyInfo info in typeof(Level).GetProperties())
            {
                row[info.Name] = info.GetValue(t, null);
            }
            dt.Rows.Add(row);
        }
        return dt;
    }
    protected void FillLevelData()
    {
        //Code Added By Dilip Kumar Tripathy on dated 05-Mar-2012
        GetAllHierarchyParents();
        objLevelDetails.ActionCode = "E";
        objLevelDetails.LeveldetailsID = Convert.ToInt32(Request.QueryString["LvlID"]);
        Session["LvlID"] = Request.QueryString["LvlID"];
        IList<Level> objLvlList = Objadminbal.GetLevelDetailsById(objLevelDetails);
        int intPid = 0;
        foreach (var i in objLvlList)
        {
            //string a = (PopulateHirarchys.FindControl("drplayer1") as DropDownList).SelectedValue;
            //string b = (PopulateHirarchys.FindControl("drplayer2") as DropDownList).SelectedValue;
            string[] arrAddress = null;
            objLevelDetails.LeveldetailsID = i.LeveldetailsID;
            objLevelDetails.Levelid = i.Levelid;
            intPid = i.Levelid;//Added By Dilip Kumar Tripathy on dated 13-Feb-2012
            objLevelDetails.Parentid = i.Parentid;
            txtName.Text = CommonFunction.GetDecodedData(i.LevelName);
            arrAddress = i.Address.Split('~');
            txtAddress1.Text = CommonFunction.GetDecodedData(arrAddress[0].ToString()).Trim();
            if (arrAddress.Length > 1)
                txtAddress2.Text = CommonFunction.GetDecodedData(arrAddress[1].ToString()).Trim();
            if (arrAddress.Length > 2)
                txtAddress3.Text = CommonFunction.GetDecodedData(arrAddress[2].ToString()).Trim();
            txtTel.Text = i.TelNo;
            txtFax.Text = i.FaxNo;
            txtHierarchyCode.Text = i.DISECode;
            //Commented By Dilip Kumar Tripathy on dated 13-Feb-2012
            int intCount = intPid;// Convert.ToInt32(CommonFunction.getParentId(i.LeveldetailsID));

            int IntParId = Convert.ToInt32(intCount);
            //Commented By Dilip Kumar Tripathy on dated 13-Feb-2012
            //for (int intInnerCount = 0; intInnerCount < Convert.ToInt32(CommonFunction.getPosId(Convert.ToInt32(intCount))); intInnerCount++)
            //{
            //    if (intInnerCount == 0)
            //    {
            //        FillControls(Convert.ToInt32(intCount));
            //    }
            //    else
            //    {
            //        FillControls(IntParId);
            //    }
            //   
            //    //IntParId  =Convert.ToInt32(CommonFunction.getParentId(IntParId));
            //    IntParId = intPid;
            //}

        }
    }
    #endregion

    //***************Summery********************************
    //Function Name       : RsetUsercontrolDDL()
    //Purpose             : To Reset the Usercontrol's dropdownlist
    //Parameters Name     : usercontrolid
    //Parameters Datatype : Control
    //Returns             : None
    //Retun Datatype      : None
    //Created By          : Biswaranjan Dash
    //Created Date        : 19-oct-2010
    //Modified By         : Dilip Kumar Tripathy
    //Modifued On         :27/01/2011

    protected void TabLevelDetails_ActiveTabChanged(object sender, EventArgs e)
    {
        if (TabLevelDetails.ActiveTabIndex == 0)
        {
            ResetUsercontrolDDL(PopulateHirarchys);
            btnReset.Visible = true;//added by Dilip Kumar Tripathy on dated 27/1/2011
            // btnid.Visible = false;//added by Dilip Kumar Tripathy on dated 27/1/2011
        }
        else
        {
            ResetUsercontrolDDL(PopulateHirarchy2);
            lbtnAll.Visible = false;
            lblPaging.Visible = false;
        }

        txtName.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtAddress3.Text = "";
        txtTel.Text = "";
        txtFax.Text = "";
        TabCreateLevel.HeaderText = "CREATE";
        btnAdd.Text = "Save";
        btnReset.Text = "Reset";
        btnDelete.Visible = false;
        GVLvldetails.Visible = false;
        txtHierarchyCode.Text = string.Empty;

    }

    #region "Google style paging format"
    private void DisplayPaging(int intRowCnt)
    {
        if (GVLvldetails.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
            //added by Dilip Kumar Tripathy on dated 27/01/2012
            
            if (lbtnAll.Text == "All")
            {
                lblPaging.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(GVLvldetails,10 , GVLvldetails.PageIndex, intRowCnt);
            }
            else
            {
                lblPaging.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(GVLvldetails, GVLvldetails.Rows.Count, GVLvldetails.PageIndex, intRowCnt);
            }
        }
        else
        {
            this.lblPaging.Visible = false;
            lbtnAll.Visible = false;

        }

    }
    #endregion
    protected void lbtnAll_Click(object sender, System.EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            this.GVLvldetails.PageIndex = 0;
            GVLvldetails.AllowPaging = false;
        }
        else
        {
            lbtnAll.Text = "All";
            GVLvldetails.AllowPaging = true;
        }
        //FillGrid();
        Fillhierachy();
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
            LocList=Objadminbal.GetLocationDetails(objLevelDetails);








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

                ///////////////////////////////////////////////

                string strLocationId;
                strLocationId = LevLoc.LocId;

                ListDiv = Objadminbal.GetDivisionDetails(objLevelDetails, LevLoc.LocId);
                foreach (Level Levdiv in ListDiv)
                {
                    objChild1 = objDom.CreateElement("ddlevel2");
                    objRoot.AppendChild(objChild1);
                    XmlAttribute createDivName = objDom.CreateAttribute("name");
                    //strDivName = DrDiv.GetString(1);
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
                    ListDept = Objadminbal.GetDepartmentDetails(objLevelDetails, divid);
                    foreach (Level LevDept in ListDept)
                    {
                        objChild2 = objDom.CreateElement("ddlevel3");
                        objChild1.AppendChild(objChild2);

                        errstring = "";
                        errstring = "dept " + LevDept.DeptName;

                        XmlAttribute createDeptName = objDom.CreateAttribute("name");
                        //strDivName = DrDiv.GetString(1);
                        createDeptName.Value = LevDept.DeptName;
                        objChild2.Attributes.Append(createDeptName);


                        XmlAttribute createDeptcaption = objDom.CreateAttribute("caption");
                        createDeptcaption.Value = "Select a Section"; //+ Levdiv.LayerOne;
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
                            ListName = Objadminbal.GetAdminUserFullName(objLevelDetails, userid);
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
                        ListSec = Objadminbal.GetSectionDetails(objLevelDetails, strDeptid);
                        foreach (Level LevSec in ListSec)
                        {
                            objChild3 = objDom.CreateElement("ddlevel4");
                            objChild2.AppendChild(objChild3);
                            errstring = "";
                            errstring = "Sec " + LevSec.SecName;
                            //Response.Write("<Script>alert(" + LevSec.SecName + ")</Script>");

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
                            ListUser = Objadminbal.GetUserDetails(objLevelDetails, strSecid);

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

            String Strdata = Server.MapPath("~/XML_5LEVEL.xml");

            // objDom.Save("D:\\XML_5LEVEL.xml");
            // objDom.Save(Server.MapPath +("//"+ "XML_5LEVEL.xml"));
            objDom.Save(Strdata);
            //Response.Write("<Script>alert('XML File generated')</Script>");



        }
        catch
        {

            //Response.Write("<Script> alert(" + errstring + ")</Script>");
            //Response.Write("<Script> alert('" + ex.Message + "')</Script>");
            //Response.Write("<Script> alert('Error')</Script>");
        }
    }
    protected void layer3(string lay1, string lay2)
    {
        XmlDocument objDom = new XmlDocument();
        XmlNode objRoot;
        XmlNode objChild1;
        XmlNode objChild2;
        XmlNode objChild3;
        XmlNode objChild4;
        string errstring = null;

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
            ListDiv = Objadminbal.GetDivisionDetails(objLevelDetails, "A");

            foreach (Level Levdiv in ListDiv)
            {
                objChild1 = objDom.CreateElement("ddlevel1");
                objRoot.AppendChild(objChild1);
                XmlAttribute createDivName = objDom.CreateAttribute("name");
                //strDivName = DrDiv.GetString(1);
                createDivName.Value = Levdiv.DivName;
                objChild1.Attributes.Append(createDivName);


                XmlAttribute createDivcaption = objDom.CreateAttribute("caption");
                createDivcaption.Value = "Select a " + strGLBLevel_1;
                objChild1.Attributes.Append(createDivcaption);

                XmlAttribute createDivValue = objDom.CreateAttribute("value");
                createDivValue.Value = Levdiv.DivId;
                objChild1.Attributes.Append(createDivValue);

                IList<Level> ListDept = new List<Level>();
                string divid;
                divid = Levdiv.DivId;
                ListDept = Objadminbal.GetDepartmentDetails(objLevelDetails, divid);
                foreach (Level LevDept in ListDept)
                {
                    objChild2 = objDom.CreateElement("ddlevel2");
                    objChild1.AppendChild(objChild2);

                    errstring = "";
                    errstring = "dept " + LevDept.DeptName;

                    XmlAttribute createDeptName = objDom.CreateAttribute("name");
                    //strDivName = DrDiv.GetString(1);
                    createDeptName.Value = LevDept.DeptName;
                    objChild2.Attributes.Append(createDeptName);


                    XmlAttribute createDeptcaption = objDom.CreateAttribute("caption");
                    createDeptcaption.Value = "Select a " + Levdiv.LayerOne;
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
                        ListName = Objadminbal.GetAdminUserFullName(objLevelDetails, userid);
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
                    ListSec = Objadminbal.GetSectionDetails(objLevelDetails, strDeptid);
                    foreach (Level LevSec in ListSec)
                    {
                        objChild3 = objDom.CreateElement("ddlevel3");
                        objChild2.AppendChild(objChild3);
                        errstring = "";
                        errstring = "Sec " + LevSec.SecName;
                        //Response.Write("<Script>alert(" + LevSec.SecName + ")</Script>");

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
                        //char[] MyChar = { ' ', 'S' };
                        //strSecid = strSecid.TrimStart(MyChar);
                        ListUser = Objadminbal.GetUserDetails(objLevelDetails, strSecid);

                        foreach (Level LevUser in ListUser)
                        {
                            objChild4 = objDom.CreateElement("ddlevel4");
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

            //objDom.Save("C:\\dropdownxml3Level.xml");
            String Strdata = Server.MapPath("~/dropdownxml3Level.xml");
            objDom.Save(Strdata);



        }
        catch
        {

            //Response.Write("<Script> alert(" + errstring + ")</Script>");
            //Response.Write("<Script> alert(" + ex.Message + "," + errstring + ")</Script>");
        }
    }

}