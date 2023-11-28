#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   webfooter.ascx.cs
// Description           :   Apply Feedback Development
// Created by            :   Sanghamitra Samal
// Created On            :   05 Sep 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Common;
using EntityLayer.Common;
using EntityLayer.CMS;
using BusinessLogicLayer.CMS;

public partial class Application_includes_footer : System.Web.UI.UserControl
{
    #region Variable Declaration

    CMSDetails objServiceEntity = new CMSDetails();
    CmsBusinesslayer objService2 = new CmsBusinesslayer();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        BindRepeater();
        dynamicFooterMenu();
    } 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      

    }
    private void BindRepeater()
    {
        
    }
    
    public void Clear()
    {
      
    }
    public void dynamicFooterMenu()
    {
        string strHtml = " <ul>";

        CMSDetails obj = new CMSDetails();
        obj.actioncode = "F";
        List<CMSDetails> tblHeader = objService2.Dynamicheaderfooterview(obj);
        if (tblHeader.Count > 0)
        {
            for (int j = 0; j < tblHeader.Count; j++)
            {
                strHtml = strHtml + "<li><a target=\"_blank\" href=" + tblHeader[j].vchURL.ToString() + ">" + tblHeader[j].StrMenuName + "</a></li>";
            }
        }
        strHtml = strHtml + "</ul>  </div>";
        divFooterId.InnerHtml = strHtml.ToString();
        //Added on 13-10-2022 by Arabinda Tripathy
        DateTime crdate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        divLastUpdate.InnerText = "Last reviewed and updated on : " + crdate.ToString("dd-MMM-yyyy");


        
    }
    
        //    <li><a href="#">Home</a></li>
        //    <li><a href="#">FAQ</a></li>
        //    <li><a href="#">Feedback</a></li>
        //    <li><a href="#">Disclaimer</a></li>
        //    <li><a href="#">Contact Us</a></li>
        //    <li><a href="#">Privacy Statement</a></li>
        //</ul>
}