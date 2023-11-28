using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using KWAdminConsole.Messages;
using Manage_Usercontrol_Property;
using System.Web;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using System.Collections.Generic;
public partial class Admin_Manage_Hierarchy_AssignAdmin : System.Web.UI.Page
{
    #region Variables
    AdminAppService ObjAdminBal = new AdminAppService();
    LocAdmin HierarchyAdmin = new LocAdmin();
    User objuser = new User();
    AdminApp.Model.UserHierarchyControl objHierUserCtrl = new UserHierarchyControl();
    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        //Code Added By Dilip Tripathy on dated 25-Jun-2012
        //To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                        

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

      
        drpUserlist.Attributes.Add("OnChange", "getlevelvalue();");
        #region Comment Area
        //Session["listboxid"] = "";
        //Session["btnid"] = "";
        //Session["hidval"] = "";
        //Session["LblAdmin"] = lbladminuser.ClientID;

        //HiddenField hidddmins = (HiddenField)getUsers2.FindControl("hidadmin");
        //HiddenField hidddmin = (HiddenField)Hierrachy.FindControl("hidadmin");
        //hidddmin.Value = lbladminuser.ClientID;
        //hidddmins.Value = lbladminuser.ClientID;
        //HiddenField Types = (HiddenField)getUsers2.FindControl("hidType");
        //HiddenField Type = (HiddenField)Hierrachy.FindControl("hidType");
        #endregion
        GroupMasterProperties.hidlstid = "";
        GroupMasterProperties.hidbtnid = "";
        GroupMasterProperties.hidnval = "";
        CommonProperties.Type = "1";        
        CommonProperties.HierachyId = 0;
        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = "";        
        if (!Page.IsPostBack)
        {
            HdlhirLevel.Value = "3";//added by Subrat Acharya             
            CommonProperties.PageUrl = "AssignAdmin.aspx";
            CommonProperties.UserControlId = drpUserlist.ClientID;
            CommonProperties.UserControlId2 = null;           
            btnSubmit.Focus(); //Added By Dilip Kumar Tripathy on dated 2-Mar-2012
            IList<Designation> objlstplink = ObjAdminBal.FillLocationDesig(Session["UserId"].ToString());
            ddlLocationAdmin.DataSource = objlstplink;
            ddlLocationAdmin.DataValueField = "LocationId";
            ddlLocationAdmin.DataTextField = "LocationName";
            ddlLocationAdmin.DataBind();
            //ddlLocationAdmin.Items.Insert(0, "--Select--");
            
        }
        drpUserlist.Attributes.Add("onchange", "GetSelectedDdl(" + drpUserlist.ClientID + ");");

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string deptid;
        string userid;
        int intOutput;
        deptid = ddlLocationAdmin.SelectedValue;// DepartmentOfUser();
        objuser.DepartmentID = DepartmentOfUser();
        try
        {
            userid = hidSelectedValue.Value;// drpUserlist.SelectedValue;
            HierarchyAdmin.UserID = userid;
            HierarchyAdmin.ActionCode = "A";
            HierarchyAdmin.AssigniD = 0;
            HierarchyAdmin.LocationID = Convert.ToInt32(deptid);
            HierarchyAdmin.CreatedBy = Convert.ToInt32(Session["UserID"].ToString());
            intOutput = Convert.ToInt32(ObjAdminBal.AddLocationAdmin(HierarchyAdmin));
            string strOutmsg = "";
            if (intOutput != 5)
            {
                  strOutmsg = StaticValues.message(intOutput, "");
            }
            else
            {
                strOutmsg = "A super administrator can not be assigned for a specific location.";
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1,typeof(string), "", "alert('" + strOutmsg + "');document.location.href='AssignAdmin.aspx';", true);
          
        }
        catch (Exception Ex)
        {
            Response.Write("<Script>alert('" + Ex.Message + "');</Script>");
        }

    }
    private string DepartmentOfUser()
    {
        string strDept = "";
        int intLayer = Convert.ToInt32(((HiddenField)getUsers2.FindControl("shidLevels")).Value);
        HiddenField hdnDeptOfUser = null;
        int counts;
        counts = intLayer;
        for (int i = counts; i > 0; i--)
        {
            HiddenField hdnLevel1 = (HiddenField)getUsers2.FindControl("shidIDs" + Convert.ToString(i));
            if (hdnLevel1.Value == "")
            {
                counts = counts - 1;
                HiddenField hdnLevel2 = (HiddenField)getUsers2.FindControl("shidIDs" + Convert.ToString(intLayer));
                hdnLevel2.Value = null;
            }
        }
        for (int i = 0; i <= counts; i++)
        {
            hdnDeptOfUser = (HiddenField)getUsers2.FindControl("shidIDs" + Convert.ToString(i));
            if (hdnDeptOfUser.Value != "" && hdnDeptOfUser.Value != "0")
            {
                strDept = hdnDeptOfUser.Value;
            }
        }
        return strDept;
    }
    protected void ddlLocationAdmin_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocationAdmin.SelectedIndex > 0)
        {
            trCurAdmin.Style.Add("display", "");
            lbladminuser.Text=CommonFunction.GetLocationAdminiStrator(int.Parse(ddlLocationAdmin.SelectedValue));
            ((DropDownList)getUsers2.FindControl("sdrplayers0")).SelectedValue = "0";
            tbluser.Visible = true;
        }
        else
        {
            trCurAdmin.Style.Add("display", "none");
            tbluser.Visible = false;
            lbladminuser.Text = "";
        }
    }

    #endregion

    #region User Defind Methods

  

    private string UserOfDept()
    {
        string strUserDept = "";
        if (((HiddenField)getUsers2.FindControl("shidLevels")).Value != "")
        {
            int intLayer = Convert.ToInt32(((HiddenField)getUsers2.FindControl("shidLevels")).Value);
            HiddenField hdnDeptOfUser = null;

            for (int i = 0; i < intLayer; i++)
            {
                hdnDeptOfUser = (HiddenField)getUsers2.FindControl("shidIDs" + Convert.ToString(i));
                if (hdnDeptOfUser.Value != "")
                {
                    strUserDept = hdnDeptOfUser.Value;
                }
            }
        }

        return strUserDept;
    }

  

   

    #endregion

}
