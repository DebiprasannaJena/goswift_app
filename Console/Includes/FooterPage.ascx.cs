//******************************************************************************************************************
//File Name          : FooterPage.ascx.cs
//Description        : Footer Page
//Created by         : Amrita Nayak
//Created On         : 6th-July-2010 
//Modification History:

//                        <CR no.>                      <Date>             <Modified by>          <Modification Summary>                                                         
//                         
// PDK Function Name :   
// Include files     :           
// Style sheet       :
// *******************************************************************************************************************

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
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
public partial class FooterPage : System.Web.UI.UserControl
{
    public string intVisitorNo,intNoofLogIn,intVisitNo,intTVisitNo, strLclUsername, strCurrentDate, companyname, visitorNo,CompanyURL;
    public int intMailNotify = 0;
    //CommonDLL objCmnDll = new CommonDLL();
    AdminAppService objBAL = new AdminAppService();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            lblFooter.Text = objBAL.GetLoginFooter();
            string s = Session["userid"].ToString();
        }        
    }
  }

