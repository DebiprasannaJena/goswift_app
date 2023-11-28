/********************************************************************************************************************
' File Name             :   SessionRedirect.aspx.cs
' Description           :   This page is shown to the user when session is kill or un authorized login
' Created by            :   Biswaranjan Das
' Created On            :   09-Sept-2010
' Modification History  :
'                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
'                            
'                         
' Function Name         :   
' Procedures Used       :  
' User Defined Namespace:  
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Web;

public partial class SessionRedirect : System.Web.UI.Page
{
    private void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();

        Response.Cookies["LastVisit"].Expires = DateTime.Now.AddSeconds(-1);
        Session.Abandon();
    }
}
