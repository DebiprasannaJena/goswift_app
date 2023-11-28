#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   InvestorProxyLogin.aspx.cs
// Description           :   Investor Proxy Login
// Created by            :   Sanghamitra Samal   
// Created On            :   10-Oct-2017    
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//                                                                           
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion
#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Data;
using System.Net.Mail;
using System.Text;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using BusinessLogicLayer.Login;
using EntityLayer.Login;
using System.Web.Services;
using System.Web;
using System.Xml.Linq;
#endregion

public partial class InvestorProxyLogin : System.Web.UI.Page
{
    #region Variables
    LoginBusinessLayer objService = new LoginBusinessLayer();
    LoginDetails objLogin = new LoginDetails();
    List<LoginDetails> objloginlist = new List<LoginDetails>();  
    string strUserId;   
    string strssomsg = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
          objloginlist = objService.SWPLogin("PL", txtEmailID.Text,"").ToList();
          if (objloginlist.Count > 0)
          {
              foreach (LoginDetails objLogin in objloginlist)
              {
                  strUserId = objLogin.strUserID;
                  Session["UserId"] = strUserId;
                  Session["InvestorId"] = objLogin.intInvestorId;
                  Session["UserName"] = objLogin.strUserName;
                  Session["RegDate"] = objLogin.strRegDate;
                  Session["LastLoginTime"] = string.Format("{0:hh:mm:ss tt}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                  Session["GSTIN"] = objLogin.strGSTIN;
                  //Session["UID"] = objLogin.strUID;
                  //Session["LogId"] = "NA";
                  //Session["SSOUserId"] = 0;
                  Session["IndustryName"] = objLogin.strInvName;
                  Response.Redirect("InvesterDashboard.aspx");
              }
          }
          else
          {
              ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('Invalid Email Id !');", true);
              txtEmailID.Text = "";
          }
    }
}