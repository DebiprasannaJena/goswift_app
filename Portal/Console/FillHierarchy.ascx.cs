/********************************************************************************************************************
' File Name             :   FillHierarchy.ascx
' Description           :   To fill the hirarchy
' Created by            :   Subrat Kumar Hota
' Created On            :   09-jun-2010
' Modification History  :   <CR no.>    <Date>             <Modified by>       <Modification Summary>'                                                          
 *                              1        8-oct-2010         Pratik              To add the paramater '_shidLevels' in Getdata() to hide and show if there is data available on the selected value of the concern dropdownlist (line No 153-175)  
'                               2      1-Nov-2010            Priyabat           To Remove the code from postback block and add outside postback block at line no 72
 '                              3      9-Nov-2010            Biswaranjan        To uncomment the code which is comented by priyabat sir and commented the code which is added by him as this condition is satisfied for all users
                             
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
public partial class Admin_FillHierarchy : System.Web.UI.UserControl
{
    #region Variable Declaration
    public string PageURL, listboxid, btntext, hidval;
    string strxmlFilePath = null;
    string strPosID;
    static int CountVal = 0;
    private DataSet _objDS;
   // CommonDLL objComnDll = new CommonDLL();
    XmlDocument objXmlDoc = new XmlDocument();
    int intHirId;
    string strConnection = "";
   
    string strData = null;
    AdminAppService objBAL = new AdminAppService();
    IList<AdminApp.Model.UserHierarchyControl> listHierarchy = null;
    AdminApp.Model.UserHierarchyControl objHierUserCtrl = new UserHierarchyControl();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        intHirId = Convert.ToInt32(CommonProperties.HierachyId);
        PageURL = CommonProperties.PageUrl;
        Session["chkfill"] = 1;

        //for Group Management Page

        if (CommonProperties.Type == "0")
        {
            if (GroupMasterProperties.hidlstid != null)
            {
                listboxid = GroupMasterProperties.hidlstid;
            }
            if (GroupMasterProperties.hidbtnid != null)
            {
                btntext = GroupMasterProperties.hidbtnid;
            }
            if (GroupMasterProperties.hidnval != null)
            {
                hidval = GroupMasterProperties.hidnval;
            }
        }

        if (!IsPostBack)
        {
            CountVal = 0;
            if (Session["locadmin"] != null)
            {
                intHirId = objBAL.GetHierarchyId(Convert.ToInt32(Session["UserId"]));
            }
            FillLocation(strxmlFilePath, strConnection, intHirId);
            FillAllControl();

            if (intHirId != 0)
            {
                if (CountVal == 0)
                {
                    FillFirstHirarchy(Convert.ToInt32(sdrplayers0.SelectedValue), intHirId);
                }
                CountVal = CountVal + 1;
            }
            string str = Request.QueryString["UId"];
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
        if (Request.Params["DptId"] != null)
        {
            CommonProperties.DId = Convert.ToInt32(Request.Params["DptId"]);
            FillUser(Convert.ToInt32(Request.Params["DptId"]), intHirId);
        }
        if (Request.Params["DptId2"] != null)
        {
            CommonProperties.DId = Convert.ToInt32(Request.Params["DptId2"]);
            FillUser2(Convert.ToInt32(Request.Params["DptId2"]), intHirId);
        }
        if (Request.Params["AID"] != null)
        {
            int Admindept = Convert.ToInt32(Request.Params["AID"]);
            if (AssignAdminProperties.hidadmin != "")
            {
                GetAdminUser(Admindept);
            }
        }
        if (Request.Params["LvlDtlsId"] != null)
        {
            FillLevelName(Convert.ToInt32(Request.Params["LvlDtlsId"]), intHirId);
        }

    }
    /// <summary>
    /// it is use to fill the child dropdown after the change of the Parent dropdown
    /// </summary>
    private void FillAllControl()
    {
        if (CommonProperties.Type == "0")
        {
            sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');FillUser('" + sdrplayers0.ClientID + "','" + listboxid + "','" + PageURL + "?LID=','" + btntext + "','" + hidval + "');");
        }
        else if (CommonProperties.Type == "3" || CommonProperties.Type == "2" || CommonProperties.Type == "1")
        {
            if (CommonProperties.PageUrl == "AdminGroupManagement.aspx")
            {
                if (CommonProperties.UserControlId == null)
                {
                    sdrplayers0.Attributes.Add("onchange", "ClearLabels(this); ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID='); ");
                }
                else
                {
                    sdrplayers0.Attributes.Add("onchange", "ClearLabels(this); ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId='); ");
                }
            }
            else
            {
                if (CommonProperties.UserControlId == null)
                {
                    if (CommonProperties.PageUrl == "AdminUserProfile.aspx" || CommonProperties.PageUrl == "adminLocationUserReport.aspx")
                    {
                        sdrplayers0.Attributes.Add("onchange", "ClearLabels(this); ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');MakeHidBlank(this,'1')");
                    }
                    else
                    {
                        sdrplayers0.Attributes.Add("onchange", "ClearLabels(this); ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');");
                    }
                }
                else
                {
                    sdrplayers0.Attributes.Add("onchange", "ClearLabels(this); ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                }
            }
        }
        else
        {
            sdrplayers0.Attributes.Add("onchange", "ClearLabels(this);ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');MakeHidBlank(this,'1')"); ;
        }
        if (CommonProperties.UserControlId != null)
        {
            if (CommonProperties.UserControlId2 != null)
            {
                if (CommonProperties.PageUrl == "admin_AssignLink.aspx")
                {
                    sdrplayers0.Attributes.Add("onchange", "ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=');GetLevelName(this,'" + CommonProperties.UserControlId4 + "','" + PageURL + "?LvlDtlsId=');ShowHideAssignLinkTr(this,'" + CommonProperties.UserControlId5 + "','" + CommonProperties.UserControlId6 + "');ClearListBox('" + CommonProperties.UserControlId3 + "');ClearListBox('" + CommonProperties.UserControlId7 + "');");
                }
                else if (CommonProperties.PageUrl == "SetPermission.aspx")
                {
                    sdrplayers0.Attributes.Add("onchange", "ClearLabels(this); ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                }
                else
                {
                    sdrplayers0.Attributes.Add("onchange", "ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=');");
                }
            }
            else
            {
                if (CommonProperties.PageUrl == "AdminShiftAssignment.aspx")
                {
                    sdrplayers0.Attributes.Add("onchange", "ClearLabels(this); ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');MakeHidBlank(this,'1')");
                }
                else
                {
                    sdrplayers0.Attributes.Add("onchange", "ClearLabels(this); ClearDropdown(this);GetDataForUser(this,'_sdrplayers1','_shidIDs0','_shidLevels','_Labels2','" + PageURL + "?CID=');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                }
            }

            if (CommonProperties.PageUrl == "AdminUserProfile.aspx" || CommonProperties.PageUrl == "AdminShiftAssignment.aspx" || CommonProperties.PageUrl == "adminLocationUserReport.aspx")
            {
                sdrplayers1.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers2','_shidIDs1','_Labels3','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'2');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                sdrplayers2.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers3','_shidIDs2','_Labels4','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'3');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                sdrplayers3.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers4','_shidIDs3','_Labels5','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'4');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                sdrplayers4.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers5','_shidIDs4','_Labels6','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'5');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                sdrplayers5.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers6','_shidIDs5','_Labels7','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'5');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");

            }
            else if (CommonProperties.PageUrl == "admin_AssignLink.aspx")
            {
                sdrplayers1.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers2','_shidIDs1','_Labels3','" + PageURL + "?CID=','_shidLevels'); GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetLevelName(this,'" + CommonProperties.UserControlId4 + "','" + PageURL + "?LvlDtlsId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=');ClearListBox('" + CommonProperties.UserControlId3 + "');ClearListBox('" + CommonProperties.UserControlId7 + "');");
                sdrplayers2.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers3','_shidIDs2','_Labels4','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetLevelName(this,'" + CommonProperties.UserControlId4 + "','" + PageURL + "?LvlDtlsId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=');ClearListBox('" + CommonProperties.UserControlId3 + "');ClearListBox('" + CommonProperties.UserControlId7 + "');");
                sdrplayers3.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers4','_shidIDs3','_Labels5','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetLevelName(this,'" + CommonProperties.UserControlId4 + "','" + PageURL + "?LvlDtlsId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=');ClearListBox('" + CommonProperties.UserControlId3 + "');ClearListBox('" + CommonProperties.UserControlId7 + "');");
                sdrplayers4.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers5','_shidIDs4','_Labels6','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetLevelName(this,'" + CommonProperties.UserControlId4 + "','" + PageURL + "?LvlDtlsId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=');ClearListBox('" + CommonProperties.UserControlId3 + "');ClearListBox('" + CommonProperties.UserControlId7 + "');");
                sdrplayers5.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers6','_shidIDs5','_Labels7','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetLevelName(this,'" + CommonProperties.UserControlId4 + "','" + PageURL + "?LvlDtlsId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=');ClearListBox('" + CommonProperties.UserControlId3 + "');ClearListBox('" + CommonProperties.UserControlId7 + "');");

            }
            else
            {
                if (CommonProperties.UserControlId != null)
                {
                    if (CommonProperties.UserControlId2 != null)
                    {
                        if (CommonProperties.PageUrl == "SetPermission.aspx")
                        {
                            sdrplayers1.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers2','_shidIDs1','_Labels3','" + PageURL + "?CID=','_shidLevels'); GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                            sdrplayers2.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers3','_shidIDs2','_Labels4','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                            sdrplayers3.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers4','_shidIDs3','_Labels5','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                            sdrplayers4.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers5','_shidIDs4','_Labels6','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                            sdrplayers5.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers6','_shidIDs5','_Labels7','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                            sdrplayers6.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers7','_shidIDs6','_Labels8','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");

                        }
                        else
                        {
                            sdrplayers1.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers2','_shidIDs1','_Labels3','" + PageURL + "?CID=','_shidLevels'); GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=')");
                            sdrplayers2.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers3','_shidIDs2','_Labels4','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=')");
                            sdrplayers3.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers4','_shidIDs3','_Labels5','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=')");
                            sdrplayers4.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers5','_shidIDs4','_Labels6','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=')");
                            sdrplayers5.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers6','_shidIDs5','_Labels7','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=')");
                            sdrplayers6.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers7','_shidIDs6','_Labels8','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=')");

                        }
                    }
                    else
                    {
                        sdrplayers1.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers2','_shidIDs1','_Labels3','" + PageURL + "?CID=','_shidLevels'); GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                        sdrplayers2.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers3','_shidIDs2','_Labels4','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                        sdrplayers3.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers4','_shidIDs3','_Labels5','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                        sdrplayers4.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers5','_shidIDs4','_Labels6','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                        sdrplayers5.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers6','_shidIDs5','_Labels7','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                        sdrplayers6.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers7','_shidIDs6','_Labels8','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId=');");
                    }
                }
            }
            sdrplayers7.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers8','_shidIDs7','_Labels9','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId='); GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=')");
            sdrplayers8.Attributes.Add("onchange", "ClearDropdown(this);GetData(this,'_sdrplayers9','_shidIDs8','_Labels10','" + PageURL + "?CID=','_shidLevels');GetUserData(this,'" + CommonProperties.UserControlId + "','" + PageURL + "?DptId='); GetUserData(this,'" + CommonProperties.UserControlId2 + "','" + PageURL + "?DptId2=')");
        }
        else
        {
            if (CommonProperties.PageUrl == "AdminUserProfile.aspx" || CommonProperties.PageUrl == "adminLocationUserReport.aspx")
            {
                sdrplayers1.Attributes.Add("onchange", "GetData(this,'_sdrplayers2','_shidIDs1','_Labels3','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'2')");
                sdrplayers2.Attributes.Add("onchange", "GetData(this,'_sdrplayers3','_shidIDs2','_Labels4','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'3')");
                sdrplayers3.Attributes.Add("onchange", "GetData(this,'_sdrplayers4','_shidIDs3','_Labels5','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'4')");
                sdrplayers4.Attributes.Add("onchange", "GetData(this,'_sdrplayers5','_shidIDs4','_Labels6','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'5')");
                sdrplayers5.Attributes.Add("onchange", "GetData(this,'_sdrplayers6','_shidIDs5','_Labels7','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'6')");
                sdrplayers6.Attributes.Add("onchange", "GetData(this,'_sdrplayers7','_shidIDs6','_Labels8','" + PageURL + "?CID=','_shidLevels');MakeHidBlank(this,'7')");
            }
            else
            {
                sdrplayers1.Attributes.Add("onchange", "GetData(this,'_sdrplayers2','_shidIDs1','_Labels3','" + PageURL + "?CID=','_shidLevels');");
                sdrplayers2.Attributes.Add("onchange", "GetData(this,'_sdrplayers3','_shidIDs2','_Labels4','" + PageURL + "?CID=','_shidLevels');");
                sdrplayers3.Attributes.Add("onchange", "GetData(this,'_sdrplayers4','_shidIDs3','_Labels5','" + PageURL + "?CID=','_shidLevels');");
                sdrplayers4.Attributes.Add("onchange", "GetData(this,'_sdrplayers5','_shidIDs4','_Labels6','" + PageURL + "?CID=','_shidLevels');");
                sdrplayers5.Attributes.Add("onchange", "GetData(this,'_sdrplayers6','_shidIDs5','_Labels7','" + PageURL + "?CID=','_shidLevels');");
                sdrplayers6.Attributes.Add("onchange", "GetData(this,'_sdrplayers7','_shidIDs6','_Labels8','" + PageURL + "?CID=','_shidLevels');");

            }
            sdrplayers7.Attributes.Add("onchange", "GetData(this,'_sdrplayers8','_shidIDs7','_Labels9','" + PageURL + "?CID=','_shidLevels');");
            sdrplayers8.Attributes.Add("onchange", "GetData(this,'_sdrplayers9','_shidIDs8','_Labels10','" + PageURL + "?CID=','_shidLevels');");
        }
    }
    /// <summary>
    /// Fill the Location which is the first hirachy
    /// </summary>
    /// <param name="strxmlFilePath"></param>
    /// <param name="Constr"></param>
    /// <param name="intHierarchyId"></param>
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
                //    objComnDll.PopupDropDown(Constr, sdrplayers0, "usp_GetUserLocation", "", "userId", int.Parse(Session["UserId"].ToString()));

                //    sdrplayers0.SelectedIndex = 0;
                //}
                //else
                //{
                //    StrQuery = "SELECT A.intLevelDetailId,A.nvchLevelName from M_ADM_LevelDetails A,M_ADM_Level B where  A.intLevelId=B.intLevelId AND A.intParentId=0 AND A.bitStatus=1 And B.bitStatus=1 AND B.intHierarchyId=" + intHirId + " And B.intPosition=1";

                //    if (Session["locadmin"] != null)
                //    {
                //        objComnDll.PopupDropDown(Constr, StrQuery, sdrplayers0, 1);
                //    }
                //    else
                //    {
                //        objComnDll.PopupDropDown(Constr, StrQuery, sdrplayers0, 0);
                //    }
                //    sdrplayers0.SelectedValue = intHirId.ToString();
                //}

            }
            if (CommonProperties.Type == "U")
            {
                if (CommonProperties.PageUrl == "AdminUserProfile.aspx" || CommonProperties.PageUrl == "AdminFlexiTime.aspx")
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
                        object retpid = objBAL.GetParentId(CommonProperties.DId);
                        sdrplayers0.SelectedValue = retpid.ToString();
                    }
                }
                else
                {
                    //StrQuery = "SELECT A.intLevelDetailId,A.nvchLevelName from M_ADM_LevelDetails A,M_ADM_Level B where  A.bitStatus=1 And B.bitStatus=1 AND A.intLevelId=B.intLevelId AND intParentId=0";
                    //objComnDll.PopupDropDown(Constr, StrQuery, sdrplayers0, 1);
                    listHierarchy = objBAL.BindDropdownHierarchy();
                    sdrplayers0.DataSource = listHierarchy;
                    sdrplayers0.DataValueField = "LevelDetailId";
                    sdrplayers0.DataTextField = "LevelDetailName";
                    sdrplayers0.DataBind();
                    sdrplayers0.Items.Insert(0, new ListItem { Text = "--Select--", Value = "0" });
                    if (Session["rpAid"].ToString() != "No")
                    {
                        //StrQuery = "select dbo.UDF_GetparentId(" + Session["rpAid"].ToString() + ")";
                        //object retpid = objComnDll.ExeScalar(Constr, StrQuery, 0);
                        object retpid = objBAL.GetParentId(Convert.ToInt32(Session["rpAid"]));
                        sdrplayers0.SelectedValue = retpid.ToString();
                    }
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
        //string StrQuery = "";
        strData = "0|--Select--";
        if (intCID > 0)
        {
           
            objHierUserCtrl.HierarchyId = HierarchyId;
            objHierUserCtrl.ParentId = intCID;
            listHierarchy = objBAL.FillFirstHirarchy(objHierUserCtrl);
            sdrplayers1.DataSource = listHierarchy;
            sdrplayers1.DataValueField = "LevelDetailId";
            sdrplayers1.DataTextField = "LevelDetailName";
            sdrplayers1.DataBind();
            sdrplayers1.Items.Insert(0, new ListItem { Text = "--Select--", Value = "0" });
            sdrplayers2.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers3.Items.Add(new ListItem("-Select-", "0"));
            sdrplayers4.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers5.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers6.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers7.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers8.Items.Add(new ListItem("-Select-", "0"));

            sdrplayers9.Items.Add(new ListItem("-Select-", "0"));
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
           
            if (intHirachyId == 0)
            {
                strData = strData + '`' + strLevel;

                if (strData == "`0")
                {
                    strData = "";
                }
            }
           

            if (shidIDs1.Value == "")
            {
                Response.Write(strData);
                Response.End();

            }
            else
            {
                if (intCID > 0)
                {
                    for (int i = 1; i <= 8; i++)
                    {
                        DropDownList ddlToFill = (DropDownList)FindControl("sdrplayers" + (i + 1));
                        if (((HiddenField)FindControl("shidIDs" + i)).Value != "" && ((HiddenField)FindControl("shidIDs" + (i + 1))).Value != "")
                        {
                            
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
    /// <summary>
    /// To fill the user name in dropdown
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

    private void FillUser2(int intDeptId, int intHirachyId)
    {
        strData = "";
        int intStat = 0;
        string strQry = string.Empty;
        if (PageURL != "AdminShiftAssignment.aspx")
        {
            if (CommonProperties.PageUrl == "admin_AssignLink.aspx")
            {
                intStat = 1;
            }
        }
        objHierUserCtrl.StatusFlag = intStat;
        objHierUserCtrl.ParentId = intDeptId;
        listHierarchy = objBAL.FillUser2(objHierUserCtrl);
        
        if (listHierarchy.Count() > 0)
        {
            foreach (UserHierarchyControl objHier in listHierarchy)
            {
                if (intStat == 1)
                {
                    if (strData == "")
                    {
                        strData = objHier.LevelDetailId + "|" + objHier.LevelDetailName + "|" + objHier.LevelName;
                    }
                    else
                    {
                        strData = strData.Trim() + "~" + objHier.LevelDetailId + "|" + objHier.LevelDetailName + "|" + objHier.LevelName;
                    }
                }
                else
                {
                    if (strData == "")
                    {
                        strData = objHier.UserId + "|" + objHier.UserName + "|" + objHier.Symbol;
                    }
                    else
                    {
                        strData = strData.Trim() + "~" + objHier.UserId + "|" + objHier.UserName + "|" + objHier.Symbol;
                    }
                }

            }
        }
        if (intHirachyId == 0)
        {
            strData = strData + '`' + '?';
        }
        Response.Write(strData);
        Response.End();
    }
    private void FillLevelName(int intDeptId, int intHirachyId)
    {
        try
        {
            strData = "";
            string strQry = string.Empty;

            if (PageURL == "admin_AssignLink.aspx")
            {
                //strQry = "select  nvchLabel from M_Adm_Level where  intLevelId=(select distinct intLevelId from M_Adm_LevelDetails where intLevelDetailId in (select intLevelDetailId  from M_Adm_LevelDetails where intParentId=" + intDeptId + "))";
                //strData = Convert.ToString(objComnDll.ExeScalar(strConnection, strQry, 0));
                objHierUserCtrl.ParentId = intDeptId;
                strData=objBAL.FillLevelName(objHierUserCtrl);
               
                if (strData == "")
                {
                    strData = "No Levels";
                }
                Response.Write(strData);
                Response.End();
            }

        }
        catch (Exception e)
        {
            Response.Write("<script>window.alert('" + e.Message + "')</script>");
        }
    }
    /// <summary>
    /// it is use for assign admin to show the the administrator name
    /// </summary>
    /// <param name="intDeptId"></param>
    private void GetAdminUser(int intDeptId)
    {
        try
        {
            
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


}

