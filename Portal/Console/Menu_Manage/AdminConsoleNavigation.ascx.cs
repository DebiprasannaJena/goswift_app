/********************************************************************************************************************
' File Name             :   AdminConsoleNavigation.ascx.cs
' Description           :   To Show the navigation dynamically
' Created by            :   Biswaranjan Das
' Created On            :   24-sept-2010
' Modification History  :
'                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
'                             1                         8-Nov-2010            Biswaranjan                 To add  strParent for parent nvaigation value for Manage Hierarchy
'                                                       
' Function Name         : Shownavigaiton  
' Procedures Used       :  
' User Defined Namespace:   
' Inherited classes     :                                              
**********************************************************************************************************************/

using System;
using System.Linq;
//using CSMPDK_3_0;
using Admin.CommonFunction;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
public partial class AdminConsoleNavigation : System.Web.UI.UserControl
{
   

    #region Variable Declaration
    AdminAppService objBAL = new AdminAppService();
    string[] strAttribute = null;
    public static string strParent, strChild, strRoot,strtabactive = null,strNewLink=null;
   
    //CommonDLL objcom = new CommonDLL();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack == true)
        {
            try
            {

                if (Request.QueryString["att"].ToString() != null && Request.QueryString["att"].ToString() != "")
                {
                    {
                        Shownavigaiton(Request.QueryString["att"]);
                    }
                }
            }
            catch 
            {

            }
            
        }
       
    }
    /// <summary>
    /// function To show the navigation in each adminconsole navigation page.
    /// </summary>
    /// <param name="querstringval"></param>
    protected void Shownavigaiton(string querstringval)
    {
        try
        {
            querstringval = CommonFunction.DecryptData(querstringval);
            if (querstringval.Contains('~'))
            {
                strAttribute = querstringval.Split('~');
               // strParent = strAttribute[0];
                strParent = "";
                strChild = strAttribute[1];
                strRoot = ">>" + strAttribute[2];
            }
            else
            {
                strAttribute=querstringval.Split('|');
                strParent = " Manage Hierarchy >>";//Added By Biswaranjan on 8-Nov-2010 
               // strRoot =">>" + strAttribute[0]; //Commented By Biswaranjan on 8-Nov-2010
                strRoot = "";
                strChild = objBAL.GetHiearchy(Convert.ToInt32(strAttribute[1]));
            }
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
