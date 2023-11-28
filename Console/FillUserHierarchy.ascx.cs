
/********************************************************************************************************************
' File Name             :   FillUserHierarchy.ascx
' Description           :   To fill the hirarchy
' Created by            :   Subrat Kumar Hota
' Created On            :   09-jun-2010
' Modification History  :   <CR no.>    <Date>             <Modified by>       <Modification Summary>'                                                          
*                              1         8-oct-2010           Pratik             To add the paramater '_shidLevels' in Getdata() to hide and show if there is data available on the selected value of the concern dropdownlist (Line No 205-224)  
                               2         1-Nov-2010           Priyabat           To Remove the code from postback block and add outside postback block at line no 72
'                              3         9-Nov-2010           Biswaranjan        To uncomment the code which is comented by priyabat sir and commented the code which is added by him as this condition is satisfied for all users
' Function Name         :   Fill the User control  
' Procedures Used       :    
' User Defined Namespace:   
' Inherited classes     :                                              
**********************************************************************************************************************/

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//using CSMPDK_3_0;
using System.Xml;
using Manage_Usercontrol_Property;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using System.Collections.Generic;
using System.ComponentModel;

public partial class Admin_FillUserHierarchy : System.Web.UI.UserControl
{

    #region Variable Declaration
    private DataSet _objDS;
    public string PageURL, listboxid, btntext, hidval;
    //Variable declaration for Setpermission
    //public string hidlstid, listboxid, hidbtnid, hidnval;
    string strxmlFilePath = null;
    string strPosID;
    static int CountVal;
   // CommonDLL objComnDll = new CommonDLL();
    XmlDocument objXmlDoc = new XmlDocument();
    int intHirId;
    string strConnection = "ConnectionString";
    AdminAppService objBAL = new AdminAppService();
   
    string strData = null;
    int cnt = 1;
    int cnts = 0;
    AdminApp.Model.UserHierarchyControl objHierUserCtrl = new UserHierarchyControl();
    IList<AdminApp.Model.UserHierarchyControl> listHierarchy = null;
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        //string s = Getlistboxid;
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        intHirId = CommonProperties.HierachyId;
        PageURL = CommonProperties.PageUrl;

        //======================Group Master
        if (CommonProperties.Type == "0")
        {
            if (GroupMasterProperties.hidlstid != null)
            {
                listboxid = GroupMasterProperties.hidlstid; //hidlstid.Value;
            }
            if (GroupMasterProperties.hidbtnid != null)
            {
                btntext = GroupMasterProperties.hidbtnid; //hidbtnid.Value;
            }
            if (GroupMasterProperties.hidnval != null)
            {
                hidval = GroupMasterProperties.hidnval; //hidnval.Value;
            }
        }
        #region Commented By Biswaranjan on 9-Nov-2010
        //****************************Added By Priyabat Sir on 1-Nov-2010 ******************
        //****************************************Begin*************************************
        //if (Session["locadmin"] != null)
        //{
        //    string strQr = "select distinct Isnull(A.int_HierarchyId,0) as hirid  from M_Admin_Level A,M_Admin_LevelDetails B Where  A.int_DeletedFlag<>1 And B.int_DeletedFlag<>1 AND A.int_LevelId=B.int_LevelId and B.int_LevelDetailId in (Select int_LevelDetailId from T_Admin_AssignAdmin where int_UserId=" + Session["UserId"].ToString() + ")";
        //    intHirId = (int)objComnDll.ExeScalar("ConnectionString", strQr, 0);
        //}
        ////Fill the First Hirarchy
        //FillLocation(strxmlFilePath, strConnection, intHirId);
        //****************************************End***************************************
        #endregion
        if (!IsPostBack)
        {
            CountVal = 0;
            //  strxmlFilePath = Server.MapPath("Upload file//" + "Kwantify.xml");
            //***********************Commented By Priyabat Sir on 1-Nov-2010 ********************

            if (Session["locadmin"] != null)
            {
                //string strQr = "select distinct Isnull(A.intHierarchyId,0) as hirid  from M_ADM_Level A,M_ADM_LevelDetails B Where  A.bitStatus=1 And B.bitStatus=1 AND A.intLevelId=B.intLevelId and B.intLevelDetailId in (Select intLevelDetailId from T_Admin_AssignAdmin where intUserId=" + Session["UserId"].ToString() + ")";
                //intHirId = (int)objComnDll.ExeScalar("ConnectionString", strQr, 0);
                intHirId = objBAL.GetHierarchyId(Convert.ToInt32(Session["UserId"]));
            }
            //Fill the First Hirarchy
            FillLocation(strxmlFilePath, strConnection, intHirId);
            //****************************************End**************************************
            FillAllControl();
            if (CommonProperties.Type == "U" && CommonProperties.PageUrl == "AdminAddUser.aspx" || CommonProperties.PageUrl == "AdminFlexiTime.aspx")
            {
                ShowPosition(CommonProperties.PositionId);
            }
            if (intHirId != 0)
            {
                if (CountVal == 0)
                {
                    FillFirstHirarchy(Convert.ToInt32(sdrplayers0.SelectedValue), intHirId);
                }
                CountVal = CountVal + 1;
            }
            string str = Request.QueryString["UId"];
            HttpContext.Current.Items["loc"] = sdrplayers0.ClientID;
        }
        if (intHirId != 0)
        {
            if (shidIDs1.Value == "")
            {
                if (CountVal > 0)
                {
                    if (Request.Params["CID"] != null)
                    {
                        FillHirarchy(Convert.ToInt32(Request.Params["CID"].ToString()), intHirId);
                    }
                }
            }
            else if (shidIDs1.Value != "")
            {
                FillHirarchy(Convert.ToInt32(shidIDs1.Value), intHirId);
            }
        }
        else
        {
            if (Request.Params["CID"] != null)
            {
                FillHirarchy(Convert.ToInt32(Request.Params["CID"].ToString()), intHirId);
            }
        }
        if (Request.Params["DptIdFUH"] != null)
        {
            FillUser(Convert.ToInt32(Request.Params["DptIdFUH"]), intHirId);
        }

        #region Below Code Info
        //code added by dilip on dated 07-Apr-2012
        //Purpose     : To Bind UserControl3 
        //Modify Date : 13-Apr-2012
        //Modify By   : Dilip Kumar Tripathy
        //Description : Adding if condition for Request.Params["DptIdFUH4"] to bind Usercontrol4
        #endregion

        if (Request.Params["DptIdFUH3"] != null)
        {
            FillUser3(Convert.ToInt32(Request.Params["DptIdFUH3"]), intHirId);
        }
        if (Request.Params["DptIdFUH4"] != null)
        {
            FillUser4(Convert.ToInt32(Request.Params["DptIdFUH4"]), intHirId);
        }
        //---code ended on 07-Apr-2012 By Dilip--------------
        if (Request.Params["AID"] != null)
        {
            int Admindept = Convert.ToInt32(Request.Params["AID"]);
            //if (hidadmin.Value != "")
            if (AssignAdminProperties.hidadmin != "")
            {
                GetAdminUser(Admindept);
            }
        }
    }
    /// <summary>
    /// To fill the dropdownlist Contols after the Change of the parent Dropdownlist controls
    /// </summary>
    private void FillAllControl()
    {
        //if (hidType.Value == "0") //For Groupmaster
        //CommonProperties.Type == "U" is added by pratik in else condition
        //Purpose:To show the the no of  hiearchy in  adduser page while edit from the userprofile page by searching same no of hierachy.
        if (CommonProperties.Type == "0" || CommonProperties.Type == "3")
        {
            sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');FillUser('" + sdrplayers0.ClientID + "','" + listboxid + "','" + PageURL + "?LID=','" + btntext + "','" + hidval + "');");
        }
        else if (AssignAdminProperties.hidadmin == "" && CommonProperties.Type == "1" && CommonProperties.UserControlId2 != null)
        {
            sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH=');");
        }
        //Code added by dilip on dated 13-Apr-2012--------
        //Purpose : To Bind UserControlId3 and UserControlId4
        else if (AssignAdminProperties.hidadmin == "" && CommonProperties.Type == "1" && CommonProperties.UserControlId3 != null)
        {
            if (CommonProperties.UserControlId4 != null)
            {
                sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId3 + "','" + PageURL + "?DptIdFUH3=');GetUserData(this,'" + CommonProperties.UserControlId4 + "','" + PageURL + "?DptIdFUH4=')");
            }
            else
            {
                sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId3 + "','" + PageURL + "?DptIdFUH3=');");
            }
        }
        
        //---------------ended by dilip--------------------
        else if (CommonProperties.Type == "2")
        {
            sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');");
        }

        else
        {
            if (AssignAdminProperties.hidadmin == "" && (CommonProperties.Type == "1" || CommonProperties.Type == "U") && CommonProperties.UserControlId2 == null)
            {
                sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');");
            }
            //code added by Dilip Kumar Tripathy on dated 29-Feb-2012
            if (CommonProperties.Type == "U" && CommonProperties.UserControlId2 != null)
            {
                if (CommonProperties.PageUrl == "AdminAddUser.aspx")
                {
                    sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId3 + "','" + PageURL + "?DptIdFUH4=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH3=')");
                }
                else
                {
                    sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH=');");
                }
            }
            else
            {
                sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');");
            }
        }

        //It is for ASSign Adim  and Hid Type is only 2 for Assign Admin
        //if (hidadmin.Value != null && hidType.Value == "2")
        if (AssignAdminProperties.hidadmin != null && CommonProperties.Type == "2")
        {
            //if (hidadmin.Value != "" && hidType.Value == "2")
            if (AssignAdminProperties.hidadmin != "" && CommonProperties.Type == "2")
            {
                //string LblId = hidadmin.Value;
                string LblId = AssignAdminProperties.hidadmin;

                string intLevel = "0";
                Page mainPage = this.Page;
                //HiddenField hdl;
                Label lbluser;

                lbluser = (Label)mainPage.FindControl("lbladminuser");
                //LblId = hidadmin.Value;
                LblId = AssignAdminProperties.hidadmin;
                sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUserHierarchy(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + intLevel + "','" + PageURL + "?CID=');GetHierarchyAdmin(this,'" + LblId + "','" + PageURL + "?AID=');");
                sdrplayers1.Attributes.Add("onchange", "ClearDropdown(this);GetDataHierarchy(this,'_sdrplayers2','_shidIDs1','_Labels3','" + intLevel + "','" + PageURL + "?CID=');GetHierarchyAdmin(this,'" + LblId + "','" + PageURL + "?AID=');");
                sdrplayers2.Attributes.Add("onchange", "ClearDropdown(this);GetDataHierarchy(this,'_sdrplayers3','_shidIDs2','_Labels4','" + intLevel + "','" + PageURL + "?CID=');GetHierarchyAdmin(this,'" + LblId + "','" + PageURL + "?AID=');");
                sdrplayers3.Attributes.Add("onchange", "ClearDropdown(this);GetDataHierarchy(this,'_sdrplayers4','_shidIDs3','_Labels5','" + intLevel + "','" + PageURL + "?CID=');GetHierarchyAdmin(this,'" + LblId + "','" + PageURL + "?AID=');");
                sdrplayers4.Attributes.Add("onchange", "ClearDropdown(this);GetDataHierarchy(this,'_sdrplayers5','_shidIDs4','_Labels6','" + intLevel + "','" + PageURL + "?CID=');GetHierarchyAdmin(this,'" + LblId + "','" + PageURL + "?AID=');");
                sdrplayers5.Attributes.Add("onchange", "ClearDropdown(this);GetDataHierarchy(this,'_sdrplayers6','_shidIDs5','_Labels7','" + intLevel + "','" + PageURL + "?CID=');GetHierarchyAdmin(this,'" + LblId + "','" + PageURL + "?AID=');");
                sdrplayers6.Attributes.Add("onchange", "ClearDropdown(this);GetDataHierarchy(this,'_sdrplayers7','_shidIDs6','_Labels8','" + intLevel + "','" + PageURL + "?CID=');GetHierarchyAdmin(this,'" + LblId + "','" + PageURL + "?AID=');");
                sdrplayers7.Attributes.Add("onchange", "ClearDropdown(this);GetDataHierarchy(this,'_sdrplayers8','_shidIDs7','_Labels9','" + intLevel + "','" + PageURL + "?CID=');GetHierarchyAdmin(this,'" + LblId + "','" + PageURL + "?AID=');");
                sdrplayers8.Attributes.Add("onchange", "ClearDropdown(this);GetDataHierarchy(this,'_sdrplayers9','_shidIDs8','_Labels10','" + intLevel + "','" + PageURL + "?CID=');GetHierarchyAdmin(this,'" + LblId + "','" + PageURL + "?AID=');");
            }
        }

        //It is use for Add user 
        else if (CommonProperties.UserControlId2 != null)
        {
            sdrplayers1.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers2','_shidIDs1','_Labels3','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH=');");
            sdrplayers2.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers3','_shidIDs2','_Labels4','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH=');");
            sdrplayers3.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers4','_shidIDs3','_Labels5','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH=');");
            sdrplayers4.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers5','_shidIDs4','_Labels6','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH=');");
            sdrplayers5.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers6','_shidIDs5','_Labels7','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH=');");
            sdrplayers6.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers7','_shidIDs6','_Labels8','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH=');");
            sdrplayers7.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers8','_shidIDs7','_Labels9','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH=');");
            sdrplayers8.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers9','_shidIDs8','_Labels10','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptIdFUH=');");
        }
        else
        {
            sdrplayers1.Attributes.Add("onchange", "GetData(this,'_sdrplayers2','_shidIDs1','_Labels3','" + PageURL + "?CID=','_shidLevels');");
            sdrplayers2.Attributes.Add("onchange", "GetData(this,'_sdrplayers3','_shidIDs2','_Labels4','" + PageURL + "?CID=','_shidLevels');");
            sdrplayers3.Attributes.Add("onchange", "GetData(this,'_sdrplayers4','_shidIDs3','_Labels5','" + PageURL + "?CID=','_shidLevels');");
            sdrplayers4.Attributes.Add("onchange", "GetData(this,'_sdrplayers5','_shidIDs4','_Labels6','" + PageURL + "?CID=','_shidLevels');");
            sdrplayers5.Attributes.Add("onchange", "GetData(this,'_sdrplayers6','_shidIDs5','_Labels7','" + PageURL + "?CID=','_shidLevels');");
            sdrplayers6.Attributes.Add("onchange", "GetData(this,'_sdrplayers7','_shidIDs6','_Labels8','" + PageURL + "?CID=','_shidLevels');");
            sdrplayers7.Attributes.Add("onchange", "GetData(this,'_sdrplayers8','_shidIDs7','_Labels9','" + PageURL + "?CID=','_shidLevels');");
        }
    }
    /// <summary>
    /// Fill the Location which is the First hirarchy
    /// </summary>
    /// <param name="strxmlFilePath"></param>
    /// <param name="Constr"></param>
    /// <param name="intHierarchyId"></param>
    /// 
    public DataSet ReadXmlToDataSet(string strXmlFilePath)
    {
        DataSet set;
        this._objDS = new DataSet();
        try
        {
            this._objDS.ReadXml(strXmlFilePath);
            set = this._objDS;
        }
        catch (Exception exception)
        {
            throw new ApplicationException("Data Error" + exception.Message);
        }
        return set;
    }
    private void FillLocation(string strxmlFilePath, string Constr, int intHierarchyId)
    {

        if (strxmlFilePath != null)
        {
            objXmlDoc.Load(strxmlFilePath);
            DataSet ds = ReadXmlToDataSet(strxmlFilePath);
            string firstItem = ds.Tables[0].ToString();
            string secondItem = ds.Tables[1].ToString();

            XmlNodeList lstNode = objXmlDoc.GetElementsByTagName(secondItem);
            string strID = null;
            string strName = null;
            foreach (XmlNode node in lstNode)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    strID = node.ChildNodes[i].Attributes["ID"].Value;
                    strName = node.ChildNodes[i].Attributes["NAME"].Value;
                    strPosID = node.ChildNodes[i].Attributes["PID"].Value;
                    sdrplayers0.Items.Add(new ListItem(strName, strID));
                    Labels1.Text = strName;
                }
            }
        }
        else
        {
            if (CommonProperties.Action != "U")
            {
                objHierUserCtrl.HierarchyId = intHirId;
                objHierUserCtrl.UserId = Convert.ToInt32(Session["UserId"]);
                listHierarchy = objBAL.BindLocation(objHierUserCtrl);
                sdrplayers0.DataSource = listHierarchy;
                sdrplayers0.DataValueField = "LevelDetailId";
                sdrplayers0.DataTextField = "LevelDetailName";
                sdrplayers0.DataBind();
                sdrplayers0.Items.Insert(0, new ListItem { Text = "--Select--", Value = "0" });
                if (intHirId != 0)
                {
                    sdrplayers0.SelectedValue = intHirId.ToString();
                }
                //if (intHirId == 0)
                //{
                //    //code added by Dilip Kumar Tripathy on dated 23-Feb-2012 4:30 pm
                //    objComnDll.PopupDropDown(Constr, sdrplayers0, "usp_GetUserLocation", "", "userId", int.Parse(Session["UserId"].ToString()));
                //}
                //else
                //{
                //    StrQuery = "SELECT A.intLevelDetailId,A.nvchLevelName from M_ADM_LevelDetails A,M_ADM_Level B where  A.bitStatus=1 And B.bitStatus=1 AND A.intLevelId=B.intLevelId AND intParentId=0 AND B.intHierarchyId=" + intHirId + " And B.intPosition=1 ORDER BY A.nvchLevelName";

                //    if (Session["locadmin"] != null)
                //    {
                //        objComnDll.PopupDropDown(Constr, StrQuery, sdrplayers0, 1);//'0'--"--Select--" will not come in the dropdown                        
                //    }
                //    else
                //    {
                //        objComnDll.PopupDropDown(Constr, StrQuery, sdrplayers0, 0);                        
                //    }
                //    sdrplayers0.SelectedValue = intHirId.ToString();
                //}
            }
            //code added by Dilip Kumar Tripathy on dated 29-Feb-2012
            if (CommonProperties.Type == "U")
            {
                //StrQuery = "SELECT A.intLevelDetailId,A.nvchLevelName from M_ADM_LevelDetails A,M_ADM_Level B where  A.bitStatus=1 And B.bitStatus=1 AND A.intLevelId=B.intLevelId AND intParentId=0";
                //objComnDll.PopupDropDown(Constr, StrQuery, sdrplayers0, 1);
                listHierarchy = objBAL.BindDropdownHierarchy();
                sdrplayers0.DataSource = listHierarchy;
                sdrplayers0.DataValueField = "LevelDetailId";
                sdrplayers0.DataTextField = "LevelDetailName";
                sdrplayers0.DataBind();
                sdrplayers0.Items.Insert(0, new ListItem { Text = "--Select--", Value = "0" });
                if (CommonProperties.DId != 0)
                {
                    //StrQuery = "select dbo.UDF_GetparentId(" + CommonProperties.DId + ")";
                    //object retpid = objComnDll.ExeScalar(Constr, StrQuery, 0);
                    //sdrplayers0.SelectedValue = retpid.ToString();
                    object retpid = objBAL.GetParentId(CommonProperties.DId);
                    sdrplayers0.SelectedValue = retpid.ToString();
                }
                if (CommonProperties.PageUrl == "AdminAddUser.aspx" || CommonProperties.PageUrl == "AdminFlexiTime.aspx")
                {
                    FillLabels(Constr, intHierarchyId);
                }
            }

        }
    }

    /// <summary>
    /// If it has been choose specific locaton and the Location is display in diasble mode  then the First  layer  will populate indepently and other layer depend on it
    /// </summary>
    /// <param name="intCID">it is the int_PldId</param>
    /// <param name="HierarchyId"> it is hirarchy id</param>
    /// <param name="HierarchyId"> it is location id</param>
    private void FillFirstHirarchy(int intCID, int HierarchyId)
    {

     
        strData = "0|--Select--";
        if (intCID > 0)
        {
            DataTable objDtLevel;
            //StrQuery = "SELECT A.intLevelDetailId,nvchLevelName,B.nvchLabel from M_ADM_LevelDetails A,M_ADM_Level B where  A.bitStatus=1 And B.bitStatus=1 AND  A.intLevelId=B.intLevelId And B.intHierarchyId=" + HierarchyId + " AND intParentId=" + intCID + " order by B.nvchLabel ,A.nvchLevelName asc";
            DataTable objDt = new DataTable();
            //objDt.Columns.Add("intLevelDetailId", typeof(int));
            //objDt.Columns.Add("nvchLevelName", typeof(string));
            //DataTable objDtLevel = objComnDll.GetDataTable(strConnection, StrQuery);
            objHierUserCtrl.HierarchyId = HierarchyId;
            objHierUserCtrl.ParentId = intCID;
            objHierUserCtrl.ActionCode = "14";
            objDtLevel = ConvertToDataTable(objBAL.FillFstHirarchy(objHierUserCtrl));
          
            //if (objDtLevel.Rows.Count > 0)
            //{
            //    foreach (DataRow objDr in objDtLevel.Rows)
            //    {
            //        if (strLebelNm != objDr["LevelName"].ToString())
            //        {
            //            strLebelNm = objDr["LevelName"].ToString();
            //            objDt.Rows.Add("0", objDr["LevelName"].ToString());
            //        }
            //        objDt.Rows.Add(objDr["LevelDetailId"].ToString(), objDr["LevelDetailName"].ToString());
            //    }
            //}
            sdrplayers1.DataSource = objDtLevel;
            sdrplayers1.DataTextField = "LevelDetailName";
            sdrplayers1.DataValueField = "LevelDetailId";
            sdrplayers1.DataBind();
            foreach (ListItem list in sdrplayers1.Items)
            {
                if (list.Value == "-1")
                {                    
                    list.Attributes.Add("disabled", "disabled");
                    list.Attributes.Add("style", "background-color: silver");      
                }
            }
            sdrplayers1.Items.Insert(0, new ListItem("-Select-", "0"));

            sdrplayers2.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers3.Items.Add(new ListItem("-Select-", "0"));
            sdrplayers4.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers5.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers6.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers7.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers8.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers9.Items.Add(new ListItem("-Select-", "0"));
        }
        //Code Added By DIlip Kumar Tripathy on dated 11-Apr-2012
        //Purpose   : To assign the all hierarchy dropdown belongs to the levelid
        //Updated By  Dilip on dated 12-Apr-2012 to 
        if (CommonProperties.Type == "U" && CommonProperties.PageUrl == "AdminAddUser.aspx" || CommonProperties.PageUrl == "AdminFlexiTime.aspx")
        {
            if (CommonProperties.PageUrl == "AdminFlexiTime.aspx")
            {
                sdrplayers0.Enabled = false;
            }
            objHierUserCtrl.ActionCode = "15";
            objHierUserCtrl.LevelDetailId = CommonProperties.DId;
            DataTable dtTtlParents = ConvertToDataTable(objBAL.TotalparentId(objHierUserCtrl));
            //DataTable dtTtlParents = objComnDll.GetDataTable("ConnectionString", "select * from dbo.UDF_TotalParentIds(" + CommonProperties.DId.ToString() + ") order by id asc");
            if (dtTtlParents.Rows.Count != 0)
            {
                for (int i = 0; i < dtTtlParents.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        if (CommonProperties.PageUrl == "AdminFlexiTime.aspx")
                        {
                            sdrplayers1.Enabled = false;
                        }
                        sdrplayers1.SelectedValue = dtTtlParents.Rows[i][2].ToString();
                    }
                    else
                    {
                        //StrQuery = "SELECT A.intLevelDetailId, A.nvchLevelName  from M_ADM_LevelDetails A,M_ADM_Level B where  A.intLevelId=B.intLevelId And A.intParentId=" + dtTtlParents.Rows[i - 1][0].ToString() + " AND A.bitStatus=1 And B.bitStatus=1  order by A.nvchLevelName asc";
                        DropDownList ddlLayer = new DropDownList();
                        string strNum = (i + 1).ToString();
                        ddlLayer = (DropDownList)this.FindControl("sdrplayers" + strNum);
                        ddlLayer.Items.Clear();

                        objHierUserCtrl.LevelDetailId = Convert.ToInt32(dtTtlParents.Rows[i - 1][2].ToString());
                        listHierarchy = objBAL.LevelId(objHierUserCtrl);
                        ddlLayer.DataSource = listHierarchy;
                        ddlLayer.DataValueField = "LevelDetailId";
                        ddlLayer.DataTextField = "LevelDetailName";
                        ddlLayer.DataBind();
                        ddlLayer.Items.Insert(0, "--Select--");


                        //objComnDll.PopupDropDown(strConnection, StrQuery, ddlLayer, 1);
                        ddlLayer.SelectedValue = dtTtlParents.Rows[i][2].ToString();
                        if (CommonProperties.PageUrl == "AdminFlexiTime.aspx")
                        {
                            ddlLayer.Enabled = false;
                        }
                    }
                }
            }
            CommonProperties.DId = 0;
            CommonProperties.HierachyId = 0;
            CommonProperties.PositionId = 0;
        }

    }
    /// <summary>
    /// If it has been choose All locaton/Specific location  other  layer  will populate in ajax depending on the parent Dropdown

    /// </summary>
    /// <param name="intCID">it is int_PldId of table </param>
    /// <param name="intHirachyId"> it is Hirarchy id</param>
    private void FillHirarchy(int intCID, int intHirachyId)
    {
        strData = "";
        string strLevel = "";
        if (intCID > 0)
        {
            //if (intHirachyId == 0)
            //{
            //    StrQuery = "SELECT A.intLevelDetailId, nvchLevelName,B.nvchLabel from M_ADM_LevelDetails A,M_ADM_Level B where   A.bitStatus=1 And B.bitStatus=1 AND A.intLevelId=B.intLevelId  AND A.intParentId=" + intCID + " order by B.nvchLabel ,A.nvchLevelName asc";
            //    strLevel = getLevel(intCID);
            //}
            //else
            //{
            //    StrQuery = "SELECT A.intLevelDetailId, nvchLevelName,B.nvchLabel from M_ADM_LevelDetails A,M_ADM_Level B where  A.bitStatus=1 And B.bitStatus=1 AND  A.intLevelId=B.intLevelId And B.intHierarchyId=" + intHirachyId + " AND A.intParentId=" + intCID + " order by B.nvchLabel ,A.nvchLevelName asc";
            //}
            //objReader = (IDataReader)objComnDll.ExeReader(strConnection, StrQuery, 0);
            objHierUserCtrl.HierarchyId = intHirachyId;
            objHierUserCtrl.ParentId = intCID;
            objHierUserCtrl.ActionCode = "6";
            listHierarchy = objBAL.FillHierarchy(objHierUserCtrl);
            if (intHirachyId == 0)
            {
                strLevel = Convert.ToString(getLevel(intCID));
            }
            string strLvlNm = "";
            if (listHierarchy.Count > 0)
            {
                foreach (UserHierarchyControl objHier in listHierarchy)
                {
                    if (strData == "")
                    {
                        strData = "0|" + objHier.LevelName + "|" + objHier.LevelName;
                        strLvlNm = objHier.LevelName;
                        strData = strData.Trim() + "~" + objHier.LevelDetailId.ToString() + "|" + objHier.LevelDetailName + "|" + objHier.LevelName;
                    }
                    else
                    {
                        if (strLvlNm != objHier.LevelName)
                        {
                            strLvlNm = objHier.LevelName;
                            strData = strData.Trim() + "~" + "0|" + objHier.LevelName + "|" + objHier.LevelName;
                        }
                        strData = strData.Trim() + "~" + objHier.LevelDetailId.ToString() + "|" + objHier.LevelDetailName + "|" + objHier.LevelName;
                    }
                }
            }
            //while (objReader.Read())
            //{
            //    if (strData == "")
            //    {
            //        strData ="0|" + objReader[2].ToString() + "|" + objReader[2].ToString();
            //        strLvlNm = objReader[2].ToString();
            //        strData = strData.Trim() + "~" + objReader[0].ToString() + "|" + objReader[1].ToString() + "|" + objReader[2].ToString();
            //    }
            //    else
            //    {
            //        if (strLvlNm != objReader[2].ToString())
            //        {
            //            strLvlNm = objReader[2].ToString();
            //            strData = strData.Trim() + "~" + "0|" + objReader[2].ToString() + "|" + objReader[2].ToString();
            //        }
            //        strData = strData.Trim() + "~" + objReader[0].ToString() + "|" + objReader[1].ToString() + "|" + objReader[2].ToString();
            //    }
            //}
            if (intHirachyId == 0)
            {
                strData = strData + '`' + strLevel + '`' + GetPosition(intCID);
                ////Code added By Dilip Kumar Tripathy on dated 13-Apr-2012
                ////purpose : To clear unwanted dropdown after selecting a dropdown
                //if (strData == "`0")
                //{
                //    strData = "";
                //}
                strData = strData + '`' + strLevel;

                if (strData == "`0")
                {
                    strData = "";
                }
            }
           // objReader.Close();


            if (shidIDs1.Value == "")
            {
                Response.Write(strData);
                Response.End();

            }
            else
            {
                sdrplayers2.Items.Clear();
                sdrplayers3.Items.Clear();
                sdrplayers4.Items.Clear();
                sdrplayers5.Items.Clear();
                sdrplayers6.Items.Clear();
                sdrplayers7.Items.Clear();
                sdrplayers8.Items.Clear();

                if (intCID > 0)
                {

                    for (int i = 1; i <= 8; i++)
                    {
                        DropDownList ddlToFill = (DropDownList)FindControl("sdrplayers" + (i + 1));
                        if (((HiddenField)FindControl("shidIDs" + i)).Value != "" && ((HiddenField)FindControl("shidIDs" + (i + 1))).Value != "")
                        {

                            //StrQuery = "SELECT A.int_LevelDetailId,A.nvch_LevelName from M_Admin_LevelDetails A,M_Admin_Level B where  A.int_DeletedFlag<>1 And B.int_DeletedFlag<>1 AND A.int_LevelId=B.int_LevelId And B.int_HierarchyId=" + intHirachyId + " AND int_PldId=" + ((HiddenField)FindControl("shidIDs" + i)).Value + "";
                            //objComnDll.PopupDropDown(strConnection, StrQuery, ddlToFill, 1);
                            objHierUserCtrl.HierarchyId = intHirachyId;
                            objHierUserCtrl.ParentId = Convert.ToInt32(((HiddenField)FindControl("shidIDs" + i)).Value);
                            objHierUserCtrl.ActionCode = "7";
                            listHierarchy = objBAL.FillHierarchy(objHierUserCtrl);
                            ddlToFill.DataTextField = "LevelDetailName";
                            ddlToFill.DataValueField = "LevelDetailId";
                            ddlToFill.DataBind();
                            ddlToFill.Items.Insert(0, new ListItem { Text = "--Select--", Value = "0" });
                            //---------------------------------
                            objHierUserCtrl.LevelDetailId = Convert.ToInt32(((HiddenField)FindControl("shidIDs" + (i + 1))).Value);
                            objHierUserCtrl.ParentId = Convert.ToInt32(((HiddenField)FindControl("shidIDs" + i)).Value);
                            string strLevelName = objBAL.GetLevelName(objHierUserCtrl);
                            //string strQryLevel = "SELECT  nvch_LevelName from M_Admin_LevelDetails where int_PldId=" + Convert.ToInt32(((HiddenField)FindControl("shidIDs" + i)).Value) + " and int_LevelDetailId  =" + Convert.ToInt32(((HiddenField)FindControl("shidIDs" + (i + 1))).Value) + "";
                            //string strLevelName = objComnDll.ExeScalar(strConnection, strQryLevel);
                            if (ddlToFill.Items.Count > 0)
                            {
                                ddlToFill.Items.FindByText(strLevelName).Selected = true;
                            }
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// To get No.Of levels
    /// </summary>
    /// <param name="intLvlDtlId"></param>
    /// <returns></returns>
    private int getLevel(int intLvlDtlId)
    {
        int intNoOfLevel;
        //string StrQryLevel = "SELECT intNolevel from M_Adm_Hierarchy where bitStatus=1 and  inthierarchyid=(Select inthierarchyid from M_Adm_Level where bitStatus=1 and  intLevelId=(Select intLevelId from M_Adm_LevelDetails where bitStatus=1 and  intleveldetailid=" + intLvlDtlId + "))";
        //IDataReader objDrLevel = (IDataReader)objComnDll.ExeReader(strConnection, StrQryLevel, 0);
        //while (objDrLevel.Read())
        //{
        //    strNoOfLevel = Convert.ToString(objDrLevel["intNolevel"]);
        //}
        //objDrLevel.Close();

        intNoOfLevel = objBAL.GetHierLevelNo(intLvlDtlId);
        return intNoOfLevel;
    }
    private string GetPosition(int intLvlDtlId)
    {
        string strPosition = "";
        //string StrQry = "Select intPosition from M_Adm_Level where bitStatus=1 and  intLevelId=(Select intLevelId from M_Adm_LevelDetails where bitStatus=1 and  intleveldetailid=" + intLvlDtlId + ")";
        //IDataReader objDrLevel2 = (IDataReader)objComnDll.ExeReader(strConnection, StrQry, 0);
        //while (objDrLevel2.Read())
        //{
        //    strPosition = Convert.ToString(objDrLevel2["intPosition"]);
        //}
        //objDrLevel2.Close();

       strPosition=Convert.ToString( objBAL.GetPositionValue(intLvlDtlId));
        return strPosition;
    }
    /// <summary>
    /// it is use to display the User name and it is use for ajax
    /// </summary>
    /// <param name="intDeptId"></param>
    /// <param name="intHirachyId"></param>
    private void FillUser(int intDeptId, int intHirachyId)
    {
        strData = "";
        string strQry = string.Empty;
        int intStatus = 0;
        if (PageURL != "AdminShiftAssignment.aspx")
        {
            intStatus = 1;
        }
        objHierUserCtrl.LevelDetailId = intDeptId;
        objHierUserCtrl.StatusFlag = intStatus;
        listHierarchy = objBAL.FillUser1(objHierUserCtrl);
        //if (PageURL != "AdminShiftAssignment.aspx")
        //{
        //    strQry = "select intUserId,vchFullName,'^' from M_POR_user where bitStatus=1 and intLevelDetailId='" + intDeptId + "'  order by vchFullName";
        //}
        //else
        //{
        //    strQry = "select intUserId,vchFullName,'^' from M_POR_user where bitStatus=1 and intLevelDetailId=" + intDeptId + " And intShiftId=0  order by vchFullName";
        //}

        if (listHierarchy.Count() > 0)
        {
            foreach (UserHierarchyControl objHier in listHierarchy)
            {
                if (strData == "")
                {
                    strData = objHier.UserId.ToString() + "|" + objHier.UserName + "|" + objHier.Symbol;
                }
                else
                {
                    strData = strData.Trim() + "~" + objHier.UserId.ToString() + "|" + objHier.UserName + "|" + objHier.Symbol;
                }
            }
        }

        //objReader = (IDataReader)objComnDll.ExeReader(strConnection, strQry, 0);

        //while (objReader.Read())
        //{
        //    if (strData == "")
        //    {
        //        strData = objReader[0].ToString() + "|" + objReader[1].ToString() + "|" + objReader[2].ToString();
        //    }
        //    else
        //    {
        //        strData = strData.Trim() + "~" + objReader[0].ToString() + "|" + objReader[1].ToString() + "|" + objReader[2].ToString();
        //    }
        //}
        if (intHirachyId == 0)
        {
            strData = strData + '`' + '?';
        }
        Response.Write(strData);
        Response.End();
    }
    /// <summary>
    /// It is use for Assign Amin to show the admin user in Label
    /// </summary>
    /// <param name="intDeptId"></param>
    private void GetAdminUser(int intDeptId)
    {
         try
        {
        //strData = "";
        //string strQry = "SELECT A.vchFullName from M_POR_User A,T_Admin_AssignAdmin B where A.intUserId=B.intUserId and a.bitStatus=1 And B.bitStatus=1 and B.intLevelDetailId=" + intDeptId + "";
        //objReader = (IDataReader)objComnDll.ExeReader(strConnection, strQry, 0);

        //while (objReader.Read())
        //{

        //    strData = objReader[0].ToString();

        //}
        objHierUserCtrl.ParentId = intDeptId;
        strData = objBAL.GetAdminUser(objHierUserCtrl);
        Response.Write(strData);
        Response.End();
        }
         catch (Exception e)
         {
             Response.Write("<script>window.alert('" + e.Message + "')</script>");
         }
    }
    //Method Created By : Dilip Kumar Tripathy
    //Created Date      : 07-Apr-2012
    //Purpose           : To bind the grade data location wise
    private void FillUser4(int intDeptId, int intHirachyId)
    {
        strData = "";
        string strQry = string.Empty;

        if (PageURL == "AdminAddUser.aspx")
        {
           // object p_id = objComnDll.ExeScalar(strConnection, "select intParentId from M_Adm_LevelDetails where intLevelDetailId=" + intDeptId, 0);
            object p_id = objBAL.GetParentIdForLvlDtls(intDeptId);
            if (Convert.ToInt32(p_id) == 0)
            {
                objHierUserCtrl.HierarchyId = intDeptId;
                listHierarchy= objBAL.BindGradeData(objHierUserCtrl);
            }
            if (listHierarchy.Count() > 0)
            {
                foreach (UserHierarchyControl objHier in listHierarchy)
                {
                    if (strData == "")
                    {
                        strData = objHier.GradeId + "|" + objHier.GradeName + "|" + objHier.Symbol;
                    }
                    else
                    {
                        strData = strData.Trim() + "~" + objHier.GradeId + "|" + objHier.GradeName + "|" + objHier.Symbol;
                    }
                }
            }

            //if (Convert.ToInt32(p_id) == 0)
            //{
            //    strQry = "SELECT M.intGradeId,M.vchGradeName,'^' from M_ADM_Grade M join T_ADM_Grade T on M.intGradeId=T.intGradeId where intHierarchyId=" + intDeptId.ToString();
            //}           
            //objReader = (IDataReader)objComnDll.ExeReader(strConnection, strQry, 0);
            //while (objReader.Read())
            //{
            //    if (strData == "")
            //    {
            //        strData = objReader[0].ToString() + "|" + objReader[1].ToString() + "|" + objReader[2].ToString();
            //    }
            //    else
            //    {
            //        strData = strData.Trim() + "~" + objReader[0].ToString() + "|" + objReader[1].ToString() + "|" + objReader[2].ToString();
            //    }
            //}
            if (intHirachyId == 0)
            {
                strData = strData + '`' + '?';
            }
            Response.Write(strData);
            Response.End();
        }
    }
    //Method Created By : Dilip Kumar Tripathy
    //Created Date      : 13-Apr-2012
    //Purpose           : To bind the Designation data location wise
    private void FillUser3(int intDeptId, int intHirachyId)
    {
        strData = "";
        string strQry = string.Empty;
        
        if (PageURL == "AdminAddUser.aspx")
        {
            object p_id = objBAL.GetParentIdForLvlDtls(intDeptId);
            if (Convert.ToInt32(p_id) == 0)
            {
                objHierUserCtrl.HierarchyId = intDeptId;
                listHierarchy = objBAL.BindDesignationData(objHierUserCtrl);
            }
            if (listHierarchy.Count() > 0)
            {
                foreach (UserHierarchyControl objHier in listHierarchy)
                {
                    if (strData == "")
                    {
                        strData = objHier.DesigId + "|" + objHier.DesigName + "|" + objHier.Symbol;
                    }
                    else
                    {
                        strData = strData.Trim() + "~" + objHier.DesigId + "|" + objHier.DesigName + "|" + objHier.Symbol;
                    }
                }
            }
            //object p_id = objComnDll.ExeScalar(strConnection, "select intParentId from M_Adm_LevelDetails where intLevelDetailId=" + intDeptId, 0);
            //if (Convert.ToInt32(p_id) == 0)
            //{
            //    strQry = "select M.intDesigId,M.nvchDesigName,'^' from M_ADM_Designation M join T_ADM_Designation T on M.intDesigId=T.intDesignationId where intHierarchyId=" + intDeptId.ToString();
            //}
            
            //objReader = (IDataReader)objComnDll.ExeReader(strConnection, strQry, 0);
            //while (objReader.Read())
            //{
            //    if (strData == "")
            //    {
            //        strData = objReader[0].ToString() + "|" + objReader[1].ToString() + "|" + objReader[2].ToString();
            //    }
            //    else
            //    {
            //        strData = strData.Trim() + "~" + objReader[0].ToString() + "|" + objReader[1].ToString() + "|" + objReader[2].ToString();
            //    }
            //}
            if (intHirachyId == 0)
            {
                strData = strData + '`' + '?';
            }
            Response.Write(strData);
            Response.End();
        }
    }
    //Method Created By : Dilip Kumar Tripathy
    //Created Date      : 11-Apr-2012
    //Purpose           : To bind the grade data location wise
    private void ShowPosition(int pos)
    {
        switch (pos)
        {
            case 1:
                tr1.Style.Add("display", "");                       
                tr2.Style.Add("display", "none");
                tr3.Style.Add("display", "none");
                tr4.Style.Add("display", "none");
                tr5.Style.Add("display", "none");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 2:
                tr1.Style.Add("display", "");
                tr2.Style.Add("display", "");  
                tr3.Style.Add("display", "none");
                tr4.Style.Add("display", "none");
                tr5.Style.Add("display", "none");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 3:
                tr1.Style.Add("display", "");
                tr2.Style.Add("display", "");
                tr3.Style.Add("display", "");
                tr4.Style.Add("display", "none");
                tr5.Style.Add("display", "none");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 4:
                tr1.Style.Add("display", "");
                tr2.Style.Add("display", "");
                tr3.Style.Add("display", "");
                tr4.Style.Add("display", "");
                tr5.Style.Add("display", "none");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 5:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 6:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "block");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 7:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "block");
                tr7.Style.Add("display", "block");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 8:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "block");
                tr7.Style.Add("display", "block");
                tr8.Style.Add("display", "block");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 9:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "block");
                tr7.Style.Add("display", "block");
                tr8.Style.Add("display", "block");
                tr9.Style.Add("display", "block");
                tr10.Style.Add("display", "none");
                break;
            case 10:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "block");
                tr7.Style.Add("display", "block");
                tr8.Style.Add("display", "block");
                tr9.Style.Add("display", "block");
                tr10.Style.Add("display", "block");
                break;
        }
    }

    private void FillLabels(string Constr, int HiracrcyId)
    {
        //changed
        ////string StrQuerys = "SELECT count(nvch_Label) from M_Admin_Level where int_HierarchyId=" + HiracrcyId + " and int_DeletedFlag<>1 and int_Position<= " + Convert.ToInt32(Session["PID"]) + " ";
        //string StrQuerys = " SELECT count(*) FROM UDF_UPPERPARENTIDS(convert(int,(select intLevelId from m_adm_leveldetails where intLevelDetailId=" + CommonProperties.DId.ToString() + ")))";
        object value;

        //value = objComnDll.ExeScalar(Constr, StrQuerys, 0);
        value = objBAL.CountLevelid(CommonProperties.DId);
        cnt = (int)value;
        


        ////string StrQuery = "SELECT nvch_Label from M_Admin_Level where int_HierarchyId=" + HiracrcyId + " and int_DeletedFlag<>1 and int_Position< " + Convert.ToInt32(Session["PID"]) + " order by int_Position asc";
       // string StrQuery = " SELECT NVCHLABEL FROM M_ADM_LEVEL WHERE INTLEVELID IN(SELECT * FROM UDF_UPPERPARENTIDS(convert(int,(select intLevelId from m_adm_leveldetails where intLevelDetailId=" + CommonProperties.DId.ToString() + ")))) ORDER BY INTPOSITION ASC";

        //dr = (IDataReader)objComnDll.ExeReader(Constr, StrQuery);
       listHierarchy= objBAL.GetLevelNameUc2(CommonProperties.DId);
       if (listHierarchy.Count() > 0)
       {
           foreach (UserHierarchyControl objHier in listHierarchy)
           {
               cnts = cnts + 1;

               if (cnts == 1)
               {
                   if (objHier.LevelName != null)
                   {
                       Labels1.Text =objHier.LevelName;
                   }
               }
               if (cnts == 2)
               {
                   if (objHier.LevelName != null)
                   {
                       Labels2.Text =objHier.LevelName;
                   }
               }
               if (cnts == 3)
               {
                   if (objHier.LevelName != null)
                   {
                       Labels3.Text =objHier.LevelName;
                   }
               }
               if (cnts == 4)
               {
                   if (objHier.LevelName != null)
                   {
                       Labels4.Text =objHier.LevelName;
                   }
               }
               if (cnts == 5)
               {
                   if (objHier.LevelName != null)
                   {
                       Labels5.Text =objHier.LevelName;
                   }
               }
               if (cnts == 6)
               {
                   if (objHier.LevelName != null)
                   {
                       Labels6.Text =objHier.LevelName;
                   }
               }
               if (cnts == 7)
               {
                   if (objHier.LevelName != null)
                   {
                       Labels7.Text =objHier.LevelName;
                   }
               }
               if (cnts == 8)
               {
                   if (objHier.LevelName != null)
                   {
                       Labels8.Text =objHier.LevelName;
                   }
               }
               if (cnts == 9)
               {
                   if (objHier.LevelName != null)
                   {
                       Labels9.Text =objHier.LevelName;
                   }
               }
               if (cnts == 10)
               {
                   if (objHier.LevelName != null)
                   {
                       Labels10.Text =objHier.LevelName;
                   }
               }  
           }
       }
        //while (dr.Read())
        //{
        //    cnts = cnts + 1;
        //    if (cnts == 1)
        //    {
        //        if (dr[0].ToString() != null)
        //        {
        //            Labels1.Text = dr[0].ToString();
        //        }
        //    }
        //    if (cnts == 2)
        //    {
        //        if (dr[0].ToString() != null)
        //        {
        //            Labels2.Text = dr[0].ToString();
        //        }
        //    }
        //    if (cnts == 3)
        //    {
        //        if (dr[0].ToString() != null)
        //        {
        //            Labels3.Text = dr[0].ToString();
        //        }
        //    }
        //    if (cnts == 4)
        //    {
        //        if (dr[0].ToString() != null)
        //        {
        //            Labels4.Text = dr[0].ToString();
        //        }
        //    }
        //    if (cnts == 5)
        //    {
        //        if (dr[0].ToString() != null)
        //        {
        //            Labels5.Text = dr[0].ToString();
        //        }
        //    }
        //    if (cnts == 6)
        //    {
        //        if (dr[0].ToString() != null)
        //        {
        //            Labels6.Text = dr[0].ToString();
        //        }
        //    }
        //    if (cnts == 7)
        //    {
        //        if (dr[0].ToString() != null)
        //        {
        //            Labels7.Text = dr[0].ToString();
        //        }
        //    }
        //    if (cnts == 8)
        //    {
        //        if (dr[0].ToString() != null)
        //        {
        //            Labels8.Text = dr[0].ToString();
        //        }
        //    }
        //    if (cnts == 9)
        //    {
        //        if (dr[0].ToString() != null)
        //        {
        //            Labels9.Text = dr[0].ToString();
        //        }
        //    }
        //    if (cnts == 10)
        //    {
        //        if (dr[0].ToString() != null)
        //        {
        //            Labels10.Text = dr[0].ToString();
        //        }
        //    }
        //}
    }
    #region ConvertToDataTable
    /// <summary>
    /// Purpose:Convert List To DataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
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
    #endregion

}

