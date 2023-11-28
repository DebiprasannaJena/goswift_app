//******************************************************************************************************************
//File Name          : TopHeader.ascx.cs
//Description        : Header Page
//Created by         : Amrita Nayak
//Created On         : 6th-July-2010 
//Modification History:

//                        <CR no.>                      <Date>             <Modified by>          <Modification Summary>                                                         
//                         1                              6-oct-2010       Biswaranjan            To kill the session and track the logout time in iptracking table   
// PDK Function Name :   
// Include files     :           
// Style sheet       :
// *******************************************************************************************************************

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
//using CSMPDK_3_0;
//using Admin.CommonFunction;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
public partial class TopHeader : System.Web.UI.UserControl
{
    public string strCFLL3Name, strCFLL1Name, strCFLL2Name, strfullname;
    public string strTblBgColor, strTblBgColor1, strTblBorderGreen, strMenuLightColor, strLclUsername, strShowAdmin;
    public string strTblWidth, strCompanyName, strLogo, strFooter, strLogoInner, strPortalText, strPortalInner, strSchemeid;
    public string strPageColor, strhedtextC, strdateColor;
    public int intTemplateId;
    public string str, strRanNum;
    AdminApp.Model.IPTrack objiptrack = null;
    // CommonDLL objCmndl = new CommonDLL();
    AdminAppService objBAL = new AdminAppService();
    public bool isnull(string str)
    {
        if ((str == String.Empty))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }
        strFooter = "Admin Footer";
        strfullname = Session["fullName"].ToString();
        strTblBgColor = "117118";
        strTblBgColor1 = "666666";
        strTblBorderGreen = "666666";
        strMenuLightColor = "999999";
        strLclUsername = "Admin Console";
        strCFLL1Name = Session["Department"].ToString();
        if (Session["SubDept"].ToString() != "0")
            strCFLL2Name = Session["SubDept"].ToString();
        else
            strCFLL2Name = "";

        intTemplateId = 1;
        if (Session["UserId"] != null)
        {
           //CommonFunction. CreateUsersXML(Convert.ToInt32(Session["UserId"]));
        }

        if (Session["adminstat"].ToString() != "")
        {
            strShowAdmin = "true";
            strTblWidth = "470";
            //strTblWidth = 570
        }
        else
        {
            strShowAdmin = "false";
            strTblWidth = "370";
            // strTblWidth = 470
        }
       

    }
   
    /// <summary>
    /// Function Created By Biswaranjan on 6-oct-2010
    /// Purpose:To track the IP including logout and login time of indivicual users
    /// </summary>
    protected void TrackIP()
    {
        objiptrack = new AdminApp.Model.IPTrack();
        objiptrack.ActionCode = "E";
        objiptrack.UserId = Convert.ToInt32(HttpContext.Current.Session["userid"]);
        int intTrackid = objBAL.IpTracking(objiptrack);
        objiptrack.ActionCode = "U";
        objiptrack.Id = intTrackid;
        objiptrack.UserName = Convert.ToString(HttpContext.Current.Session["userName"]);
        string strClientIp = null;
        strClientIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FOWARDED_FOR"];
        if (strClientIp == string.Empty || strClientIp == null)
        {
            strClientIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        objiptrack.IpAddress = strClientIp;
        objiptrack.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
        objBAL.IpTracking(objiptrack);

    }
    protected void logout_Click(object sender, EventArgs e)
    {
        //code added By Dilip Kumar Tripathy on dated 26-Mar-2012
        TrackIP();
        Session.Abandon();
       // Response.Write("<script>top.location.href='~/Default.aspx'</script>");
        Response.Redirect("~/AdminDefault.aspx");

    }
  
}
