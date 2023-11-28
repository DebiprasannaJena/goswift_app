using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using KWAdminConsole.Messages;

public partial class AdminApp_UI_Console_Manage_LogIn: System.Web.UI.Page
{
    #region "Variable Declaration"
    int intreturnval = 0;
    public int RecCount;
    AdminAppService ObjAdminBal = new AdminAppService();
    AdminApp.Model.Login objlogin = new AdminApp.Model.Login();
    #endregion

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
        //Purpose : To clear the browser cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
        AdminConsoleNavigation.strNewLink =">>" +TabadminConfigMaster.ActiveTab.HeaderText;        
        if (!IsPostBack)
        {
            TabCreateConfig.HeaderText = "Create";
            btnsave.Text = "Save";
            btncancel.Text = "Reset";
            btnsave.Focus();//code added by Dilip Tripathy on dated 10-Apr-2012
            Filleditcase();
        }
        btnsave.Attributes.Add("onclick", "return checkvalidation();");

    }

    #region Button Events
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            //*********************If condition for Add*************************

                objlogin.ActionCode = "A";
                objlogin.IntExpiry = Convert.ToInt32(txtExp.Text);
                objlogin.intAlert = Convert.ToInt32(txtAlert.Text);
                objlogin.intAttempt = Convert.ToInt32(txtUns.Text);
                objlogin.intLock = Convert.ToInt32(txtLock.Text);
                objlogin.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                intreturnval = ObjAdminBal.ManageConfiguration(objlogin);
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Configuration") + "');", true);
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            objlogin = null;
        }
    }
   

    protected void btncancel_Click(object sender, EventArgs e)
    {
        txtExp.Text = "";
        txtAlert.Text = "";
        txtUns.Text = "";
        txtLock.Text = "";
    }

    protected void TabadminConfigMaster_ActiveTabChanged(object sender, EventArgs e)
    {
        TabCreateConfig.HeaderText = "CREATE";
        btnsave.Text = "Save";
    }

    #endregion

    #region "Member Function"
    /// <summary>
    /// function Created By Biswaranjan on 27-sept-2010 to clear the data.
    /// </summary>
  
    /// <summary>
    /// Function Designed by Biswaranjan on 23-july-2010 to fill the data in editcase.
    /// </summary>
    /// <param name="intdesigId"></param>
    protected void Filleditcase()
    {
        try
        {
            IList<AdminApp.Model.Login> objlogin = ObjAdminBal.ViewConfiguartion("E");
            foreach (AdminApp.Model.Login data in objlogin)
            {
                txtExp.Text = data.IntExpiry.ToString();
                txtAlert.Text = data.intAlert.ToString();
                txtUns.Text = data.intAttempt.ToString();
                txtLock.Text = data.intLock.ToString();

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        finally
        {
        }
    }
   
    #endregion
}