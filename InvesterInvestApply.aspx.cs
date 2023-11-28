#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   InvestorRegistrationUser.aspx.cs
// Description           :   Manage Investor Registration details
// Created by            :   AMit Sahoo
// Created On            :   18 July 2017
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

public partial class InvesterInvestApply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("PromoterDetails.aspx");
    }
   
}