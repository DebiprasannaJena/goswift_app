/********************************************************************************************************************
' File Name             :   adminUserLogReport.aspx
' Description           :   To search  User login Details location wise.
' Created by            :   Dilip Kumar Tripathy
' Created On            :   30-Mar-2012
' Modification History  :   <CR no.>    <Date>            <Modified by>       <Modification Summary>'                                                          
'                            1.        25-Jun-2012        Dilip Tripathy       To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                         
'                            2.        02-May-2013        Dilip Tripathy       To change the navigate url as per creent tab name.
' Function Name         :   
' Procedures Used       :    
' User Defined Namespace:   
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
//using CSMPDK_3_0;
using KWAdminConsole.Messages;
using Manage_Usercontrol_Property;
using System.Collections;
using System.Data.SqlClient;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
public partial class Admin_Manage_Links_admin_AssignLink : System.Web.UI.Page
{
    #region Variables and Object Declaration
    Set_Permission objGroupPermission = new Set_Permission();
    AdminAppService ObjAdminBal = new AdminAppService();
    #endregion
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        (FillLocationHierarchy.FindControl("Label1") as Label).Visible = false;
        (FillLocationHierarchy.FindControl("Label2") as Label).Visible = true;
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }
        if (Request.QueryString["att"] != null)
        {
            string strAtt = CommonFunction.DecryptData(Request.QueryString["att"].ToString());
        }

        #region Clean Cache Memory
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
        #endregion
       

        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabContainer1.ActiveTab.HeaderText;

        if (!IsPostBack)
        {
            GroupMasterProperties.hidlstid = "";
            GroupMasterProperties.hidbtnid = "";
            GroupMasterProperties.hidnval = "";
            CommonProperties.PageUrl = "admin_AssignLink.aspx";
            CommonProperties.PId = 0;
            CommonProperties.Type = "1";
            AssignAdminProperties.hidadmin = "";
            SetpermissionProperties.hidLevel = null;
            CommonProperties.UserControlId = lbShowUser.ClientID;
            CommonProperties.UserControlId2 = lbShowLevels.ClientID;
            CommonProperties.UserControlId3 = lbSelectLevels.ClientID;
            CommonProperties.UserControlId4 = lblLevelName.ClientID;
            CommonProperties.UserControlId5 = lvlTr.ClientID;
            CommonProperties.UserControlId6 = userTr.ClientID;
            CommonProperties.UserControlId7 = lbSelectUser.ClientID;
            CommonProperties.Action = "A";

            BindGlobalLink(ddlGlobalLink);
            btnUpdate.Focus();
        }

    }
    #endregion
    #region User Defined Methods
    public void BindGlobalLink(DropDownList ddlToBind)
    {
        IList<Primary> objlstplink = ObjAdminBal.FillGlink(Session["UserId"].ToString());
        ddlToBind.DataSource = objlstplink;
        ddlToBind.DataValueField = "GlinkId";
        ddlToBind.DataTextField = "GLinkName";
        ddlToBind.DataBind();
        ddlToBind.Items.Insert(0, "--Select--");
    }
    #endregion
   
    //[System.Web.Services.WebMethod]
    //public static string BindPrimaryLinks(string globalLinkId)
    //{
    //    string strPrimaryLinks = "--Select--,0,";
    //    if (globalLinkId != "0")
    //    {
    //       // CommonDLL objComnDll = new CommonDLL();
    //        string StrQry = "select nvchPlinkName,intPLinkId from M_ADM_PrimaryLink where bitStatus <> 0 and intGlinkId=" + globalLinkId + " ";
    //        DataTable dtPrimaryLink = objComnDll.GetDataTable("ConnectionString", StrQry);

    //        if (dtPrimaryLink.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dtPrimaryLink.Rows.Count; i++)
    //            {
    //                strPrimaryLinks = strPrimaryLinks + dtPrimaryLink.Rows[i][0].ToString() + "," + dtPrimaryLink.Rows[i][1].ToString() + ",";
    //            }
    //            return strPrimaryLinks.TrimEnd(',');
    //        }
    //        else
    //        {
    //            return strPrimaryLinks.TrimEnd(',');
    //        }
    //    }
    //    else
    //    {
    //        return strPrimaryLinks.TrimEnd(',');
    //    }
    //}
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string strGroupLevelId = null;
        string strGroupUserId = null;
        if (hidSelectedLevels.Value != "")
        {
            string[] strLevelArray = hidSelectedLevels.Value.Split('~');
            for (int i = 0; i < strLevelArray.Length - 1; i++)
            {
                string[] strLevelIdsArr = strLevelArray[i].ToString().Split(',');
                strGroupLevelId += strLevelIdsArr[1].ToString() + ",";
            }
        }
        if (hidSelectUser.Value != "")
        {
            string[] strUserArr = hidSelectUser.Value.Split('~');
            for (int i = 0; i < strUserArr.Length - 1; i++)
            {
                string[] strUserIdArr = strUserArr[i].ToString().Split(',');
                strGroupUserId += strUserIdArr[1].ToString() + ",";
            }
        }
        objGroupPermission.ActionCode = "P";
        objGroupPermission.PermissionStatus = rbtBtnPermitDeny.SelectedItem.Text.Trim();
        objGroupPermission.PlinkId = Convert.ToInt32(hidPlinkId.Value.TrimStart(','));
        if (strGroupLevelId != null)
        {
            objGroupPermission.GroupDeptId = strGroupLevelId.TrimEnd(',');
        }
        else
        {
            objGroupPermission.GroupDeptId = null;
        }
        if (strGroupUserId != null)
        {
            objGroupPermission.GroupUserId = strGroupUserId.TrimEnd(',');
        }
        else
        {
            objGroupPermission.GroupUserId = null;
        }
        objGroupPermission.FunctionId = Convert.ToInt32(rbtBtnPtype.SelectedValue);
        objGroupPermission.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
        string strOutPut = ObjAdminBal.UpdateGroupPermission(objGroupPermission);
        string strMsg="";
        if (strOutPut == "0")
        {
            strMsg="No user/group having this Permission to deny.";
        }
        else if (strOutPut == "3")
        {
            strMsg="No user present under selected criteria.";
        }
        else
        {
            strMsg = StaticValues.message(Convert.ToInt32(strOutPut), "Assigned Link");
        }
         
        
        ClientScript.RegisterStartupScript(GetType(), "", "alert('" + strMsg + "');", true);
        IList<Set_Permission> objPop = ObjAdminBal.GetAllusers_Assignlink(objGroupPermission.GroupUserId,objGroupPermission.GroupDeptId);
        DataTable dtAllUser = ConvertToDataTable(objPop);
        string strFullPath = "";
       
        if (dtAllUser.Rows.Count > 0)
        {
            if (objGroupPermission.GroupUserId == null)
            {
                for (int i = 1; i < dtAllUser.Rows.Count; i++)
                {
                    if (ConfigurationManager.AppSettings["AC_UserXML"] != null)
                    {
                        strFullPath = ConfigurationManager.AppSettings["AC_UserXML"] + "/" + dtAllUser.Rows[i][6].ToString() + ".XML";
                        if (File.Exists((strFullPath + dtAllUser.Rows[i][6].ToString() + ".xml")))
                        {
                            File.Delete((strFullPath + dtAllUser.Rows[i][6].ToString() + ".xml"));
                        }
                    }
                    else
                    {
                        strFullPath = HttpContext.Current.Server.MapPath("~/UserXML/") + dtAllUser.Rows[i][6].ToString() + ".XML";
                        if (File.Exists(Server.MapPath(strFullPath + dtAllUser.Rows[i][6].ToString() + ".xml")))
                        {
                            File.Delete(Server.MapPath(strFullPath + dtAllUser.Rows[i][6].ToString() + ".xml"));
                        }
                    }
                    
                   CommonFunction .CreateUsersXML(int.Parse(dtAllUser.Rows[i][6].ToString()));
                }
            }
            else
            {
                for (int i = 0; i < dtAllUser.Rows.Count; i++)
                {
                    if (ConfigurationManager.AppSettings["AC_UserXML"] != null)
                    {
                        strFullPath = ConfigurationManager.AppSettings["AC_UserXML"] + "/" + dtAllUser.Rows[i][6].ToString() + ".XML";
                        File.Delete((strFullPath + dtAllUser.Rows[i][6].ToString() + ".xml"));
                    }
                    else
                    {
                        strFullPath = HttpContext.Current.Server.MapPath("~/UserXML/") + dtAllUser.Rows[i][6].ToString() + ".XML";
                        File.Delete(Server.MapPath(strFullPath + dtAllUser.Rows[i][6].ToString() + ".xml"));
                    }
                    
                    CommonFunction.CreateUsersXML(int.Parse(dtAllUser.Rows[i][6].ToString()));
                }
            }
        }
        ResetControls();
    }
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
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ResetControls();
    }
    private void ResetControls()
    {
        hidSelectedLevels.Value = null;
        hidSelectUser.Value = null;
        ddlGlobalLink.SelectedIndex = 0;
        ddlPrimaryLink.SelectedIndex = 0;
        ResetUsercontrolddl(FillLocationHierarchy);
        foreach (ListItem list in rbtBtnPtype.Items)
        {
            if (list.Text == "Add")
            {
                list.Selected = true;
            }
            else
            {
                list.Selected = false;
            }
        }
        foreach (ListItem list in rbtBtnPermitDeny.Items)
        {
            if (list.Text.Trim() == "Permit")
            {
                list.Selected = true;
            }
            else
            {
                list.Selected = false;
            }
        }
        hidSelectedLevels.Value = "";
        hidSelectUser.Value = "";
        lblPermissionDeny.Text = "Permission For";
        rbtDept.Checked = true;
        rbtDesig.Checked = false;
        rbtLoc.Checked = false;
        divDesig.Style.Add("display", "none");
        divLoc.Style.Add("display", "none");
        divDept.Style.Add("display", "block");

    }
    [System.Web.Services.WebMethod]
    public static IList<Designation> FillLocationCombo(string strUserId)
    {
        AdminAppService ObjAdminBal = new AdminAppService();
        IList<Designation> List = new List<Designation>();
        List = ObjAdminBal.FillLocationDesig(strUserId);
        return List;
       
    }
    [System.Web.Services.WebMethod]
    public static IList<UserHierarchyControl> FillDesignation(string strLocId)
    {
        AdminAppService ObjAdminBal = new AdminAppService();
        IList<UserHierarchyControl> List = new List<UserHierarchyControl>();
        UserHierarchyControl objHierUserCtrl = new UserHierarchyControl();
        objHierUserCtrl.HierarchyId = Convert.ToInt32(strLocId);
        List = ObjAdminBal.BindDesignationData(objHierUserCtrl);
        return List;
    }
    [System.Web.Services.WebMethod]
    public static IList<UserHierarchyControl> GetUserListByDesig(string strLocId, string strDesigId)
    {
        AdminAppService ObjAdminBal = new AdminAppService();
        IList<UserHierarchyControl> List = new List<UserHierarchyControl>();
        List = ObjAdminBal.FillUserByDesig(Convert.ToInt32(strLocId), Convert.ToInt32(strDesigId));
        return List;
        
    }
    [System.Web.Services.WebMethod]
    public static IList<Primary> GetPlink(string glink)
    {
        AdminAppService ObjAdminBal = new AdminAppService();
        IList<Primary> List = new List<Primary>();
        List = ObjAdminBal.FillPlinkReport(Convert.ToInt32(glink), 1);
        return List;

    }
    protected void rbtDept_CheckedChanged(object sender, EventArgs e)
    {
        ResetControls(); 
    }
    protected void ddlGlobalLink_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlGlobalLink.SelectedIndex > 0)
        {
            IList<Primary> objPLK = ObjAdminBal.FillPlinkReport(Convert.ToInt32(ddlGlobalLink.SelectedValue), 1);
            ddlPrimaryLink.DataSource = objPLK;
            ddlPrimaryLink.DataValueField = "PlinkId";
            ddlPrimaryLink.DataTextField = "PlinkName";
            ddlPrimaryLink.DataBind();
            ddlPrimaryLink.Items.Insert(0, "--Select--");
        }
    }
}
public class clsFillLocCombo
{
    public string ID { get; set; }
    public string NAME { get; set; }

}

