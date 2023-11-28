#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   InvestorProfile.aspx.cs
// Description           :   View Investor Profile Data
// Created by            :   AMit Sahoo
// Created On            :   24 July 2017
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer.Investor;
#endregion

public partial class InvesterProfile : System.Web.UI.Page
{
    InvestorBusinessLayer objService = new InvestorBusinessLayer();

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
         Util.SessionCheck();
         
         string UserId = Session["InvestorId"].ToString();
         if (!IsPostBack)
         {
             FillInvestorInfo(UserId);
         }
         
    }
    #endregion

    #region Common Methods
    public void FillInvestorInfo(string UserId)
    {
        try
        {
            DataTable dt = CommonHelperCls.ViewInvestorDetails(Session["InvestorId"].ToString(), "V");
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["VCH_INV_NAME"].ToString();
               
                lblFirstName.Text = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                lblMiddleName.Text = dt.Rows[0]["VCH_CONTACT_MIDDLENAME"].ToString();
                lblLastName.Text = dt.Rows[0]["VCH_CONTACT_LASTNAME"].ToString();
                lblEmail.Text = dt.Rows[0]["VCH_EMAIL"].ToString();
                lblMobile.Text = dt.Rows[0]["VCH_OFF_MOBILE"].ToString();
                lblAddress.Text = dt.Rows[0]["VCH_ADDRESS"].ToString();
                lblUserId.Text = dt.Rows[0]["VCH_INV_USERID"].ToString();
                imgInvestor.Src = dt.Rows[0]["VCH_INV_PHOTO"].ToString() == "" ? "~/images/default_user.png" : "~/InvestorImage/" + dt.Rows[0]["VCH_INV_PHOTO"].ToString();
                lblRegDate.Text = Session["RegDate"].ToString();
                lblLastlogin.Text = Session["LastLoginTime"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    #endregion

    #region ButtonClick
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditInvestorProfile.aspx");
    }
    #endregion
}