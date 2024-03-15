using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.SessionState;
using System.Reflection;
using DWHServiceReference;
using RestSharp;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;

public partial class LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LogId"] != null && Session["LogId"].ToString() != "NA")
        {
            DWHServiceHostClient validate = new DWHServiceHostClient();
            validate.CheckLog("U", validate.URLEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"]), Convert.ToInt32(Session["LogId"]));
        }

        /*---------------------------------------------------------------------------------*/
        ///The following section is utilized for logging out users who have accessed the NSWS portal.
        ///This action will terminate the session on the Keycloak server and subsequently log out the user. 
        ///If a refresh token is present for NSWS users, the logout API will be invoked to conclude the session on the Keycloak server.
        /*---------------------------------------------------------------------------------*/
        if (Session["NswsRefreshToken"] != null)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            /*---------------------------------------------------------------------------------*/
            ///Get the API address and other credentials from the web config file.
            /*---------------------------------------------------------------------------------*/
            string strLogoutUrl = ConfigurationManager.AppSettings["NswsRevLogoutUrl"].ToString();
            string strTokenClientId = ConfigurationManager.AppSettings["NswsRevTokenClientId"].ToString();
            string strTokenClientSecret = ConfigurationManager.AppSettings["NswsRevTokenClientSecret"].ToString();

            /*---------------------------------------------------------------------------------*/
            ///Logout the token from keycloak server.
            /*---------------------------------------------------------------------------------*/
            var client1 = new RestClient(strLogoutUrl)
            {
                Timeout = -1,
                FollowRedirects = false
            };
            var request1 = new RestRequest(Method.POST);
            request1.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request1.AddParameter("refresh_token", Convert.ToString(Session["NswsRefreshToken"]));
            request1.AddParameter("client_id", strTokenClientId);
            request1.AddParameter("client_secret", strTokenClientSecret);
            IRestResponse responseStatus = client1.Execute(request1);
            string strLogoutStatusCode = responseStatus.StatusCode.ToString();

            /*---------------------------------------------------------------------------------*/
            ///Log the logout respose code received from logout API.
            /*---------------------------------------------------------------------------------*/
            Util.LogRequestResponse("SamlLogout", "LogoutFromKeycloak", "[LogoutStatusCodeReceived]:- " + strLogoutStatusCode);
        }

        /*---------------------------------------------------------------------------------*/

        Session.Abandon();
        Session.Clear();
        GenerateNewSessionId();
        Response.Redirect("Default.aspx");
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