using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Reflection;

public partial class LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        GenerateNewSessionId();
        Response.Redirect("~/Default.aspx");
    }
    private void GenerateNewSessionId()
    {
        //Get the unique session identifier for the session.
        System.Web.SessionState.SessionIDManager manager = new System.Web.SessionState.SessionIDManager();
        //Get the old session ID (Current)
        string oldId = manager.GetSessionID(Context);
        //Create new session Id
        string newId = manager.CreateSessionID(Context);
        bool isAdd = false, isRedir = false;
        //Save the newly created session identifier to the HTTP response
        manager.SaveSessionID(Context, newId, out isRedir, out isAdd);
        //Gets the object for current HTTP request
        HttpApplication ctx = (HttpApplication)HttpContext.Current.ApplicationInstance;
        //Gets the collection of modules for the current application
        HttpModuleCollection mods = ctx.Modules;
        //Get the System.Web.IHttpModule object with the specified name from the System.Web.HttpModuleCollection
        System.Web.SessionState.SessionStateModule ssm = (SessionStateModule)mods.Get("Session");
        //Searche for the fields defined for the current System.Type, using the specified binding constraints
        System.Reflection.FieldInfo[] fields = ssm.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        SessionStateStoreProviderBase store = null;
        System.Reflection.FieldInfo rqIdField = null, rqLockIdField = null, rqStateNotFoundField = null;

        //Override the session field value with the newly created value.
        foreach (System.Reflection.FieldInfo field in fields)
        {
            if (field.Name.Equals("_store")) store = (SessionStateStoreProviderBase)field.GetValue(ssm);
            if (field.Name.Equals("_rqId")) rqIdField = field;
            if (field.Name.Equals("_rqLockId")) rqLockIdField = field;
            if (field.Name.Equals("_rqSessionStateNotFound")) rqStateNotFoundField = field;
        }
        object lockId = rqLockIdField.GetValue(ssm);
        if ((lockId != null) && (oldId != null)) store.ReleaseItemExclusive(Context, oldId, lockId);
        rqStateNotFoundField.SetValue(ssm, true);
        rqIdField.SetValue(ssm, newId);
    }
}