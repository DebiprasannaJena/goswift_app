#region  PageInfo
//******************************************************************************************************************
// File Name             :   otpValidation.aspx.cs
// Description           :   Validation of OTP
// Created by            :   AMit Sahoo
// Created On            :   16 July 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>               <Modification Summary>'                                                         
//
// FUNCTION NAME         :   1                          04-Sep-2018             Sushant Jena                OTP validation issue resolve and recoded.Remove data push to SSO database     
//******************************************************************************************************************
#endregion

#region Namespace
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.Services;
using System.Data;
using System.Linq;
using EntityLayer.Investor;
using BusinessLogicLayer.Investor;
using System.Net.Mail;
using System.Configuration;
#endregion

public partial class otpValidation : SessionCheck
{
    #region GlobalVariables

    InvestorBusinessLayer objService = new InvestorBusinessLayer();
    InvestorDetails objInvDet = new InvestorDetails();



    #endregion

    #region PageLoad

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["InvRegUser"] != null)
                {
                    string str = getOTPTime(Convert.ToString(Session["UserIdDD"]));
                    
                }
                else
                {
                    Response.Redirect("InvestorRegistrationUser.aspx");
                }
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "OTPValidation");
            }
        }
    }

    #endregion

    #region ButtonClickEvents

    ///// Button Confirm OTP
    protected void Btn_Confirm_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["otp"] != null)
            {
                string strOTP = getOTPTime(Convert.ToString(Session["UserIdDD"]));

                if (txtToken.Text == strOTP)
                {
                    /*--------------------------------------------------------------------------*/
                    ////// Update OTP status in login table
                    /*--------------------------------------------------------------------------*/
                    InvestorInfo objInvestorInfo = (InvestorInfo)Session["InvRegUser"];
                    string straction = "CHKO";
                    string strInvid = objInvestorInfo.VCH_INV_USERID;
                    string strResult = objService.strOTRStatus(objInvDet, straction, strInvid);

                    /*--------------------------------------------------------------------------*/
                    ////// After sucessfully OTP validation, redirect to thankYou page
                    /*--------------------------------------------------------------------------*/
                    Response.Redirect("thankYou.aspx");
                    Session["InvRegUser"] = null;
                    Session["otp"] = null;
                }
                else
                {
                    Lbl_Msg.Text = "Invalid OTP !!";
                }
            }
            else
            {
                Lbl_Msg.Text = "Your OTP is expired !!";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OTPValidation");
        }
    }
    ///// Button Re-Generate and Resend OTP
    protected void Btn_Resend_OTP_Click(object sender, EventArgs e)
    {
        try
        {
            InvestorInfo objInvestorInfo = (InvestorInfo)Session["InvRegUser"];
            string result = GenerateOTP(objInvestorInfo.VCH_INV_NAME, objInvestorInfo.VCH_OFF_MOBILE, "I", objInvestorInfo.VCH_INV_USERID, objInvestorInfo.VCH_EMAIL);
            Session["otp"] = result.Split('|')[1].ToString();
            Response.Redirect("otpValidation.aspx");
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "OTPValidation");
        }
    }

    #endregion

    #region CommonMethods


    /// <summary>
    /// Generate new OTP
    /// Send the OTP through mail and SMS
    /// </summary>
    /// <param name="strUnitName"></param>
    /// <param name="strMobileNo"></param>
    /// <param name="strAction"></param>
    /// <param name="strUserId"></param>
    /// <param name="strEmailId"></param>
    /// <returns></returns>
    public static string GenerateOTP(string strUnitName, string strMobileNo, string strAction, string strUserId, string strEmailId)
    {
        CommonHelperCls objComm = new CommonHelperCls();
        InvestorDetails objInvDetent = new InvestorDetails();
        InvestorRegistration objInvService = new InvestorRegistration();

        try
        {
            string[] InvToEmail = new string[1];
            InvToEmail[0] = strEmailId;

            ////// Generate OTP
            string result = objComm.AddOTP(strUnitName, strMobileNo, "I", strUserId);

            string[] arrResult = result.Split('|');
            if (arrResult[0] == "1" && arrResult[1] != "")
            {
                

                objInvDetent.strAction = "M";
                DataTable dtcontent = objInvService.GetSMSContent(objInvDetent);
                if (dtcontent.Rows.Count > 0)
                {
                    /*----------------------------------------------------------------*/
                    ////// Prepare SMS and Mail Content
                    /*----------------------------------------------------------------*/
                    string strSubject = dtcontent.Rows[0]["vchEvent"].ToString();
                    string strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[OTPCODE]", arrResult[1].ToString());
                    strSMSContent = strSMSContent.Replace("[UNITNAME]", strUnitName.ToString());
                    string strTemplateId = dtcontent.Rows[0]["vchTemplateId"].ToString();
                    string strMsgType = dtcontent.Rows[0]["vchMsgType"].ToString();

                    /*----------------------------------------------------------------*/
                    ///// Send Mail
                    /*----------------------------------------------------------------*/
                   
                    bool mailStatus = false;
                    /*----------------------------------------------------------------*/
                    ///// Send SMS
                    /*----------------------------------------------------------------*/
                    
                    bool smsStatus = objComm.SendSmsWithTemplate(strMobileNo, strSMSContent, strTemplateId, strMsgType);

                    /*----------------------------------------------------------------*/
                    ////// Update SMS and Email Status in Transaction Table
                    /*----------------------------------------------------------------*/
                    string str = objComm.UpdateMailSMSStaus("OTP", strMobileNo, InvToEmail[0], strSubject, "0", "0", 0, "0", strSMSContent, strSMSContent, smsStatus, mailStatus);
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        finally
        {
            objComm = null;
            objInvDetent = null;
            objInvService = null;
        }
    }

    /// <summary>
    /// Check OTP time
    /// </summary>
    /// <param name="UserId"></param>
    /// <returns></returns>
    public string getOTPTime(string UserId)
    {
        try
        {
            string strOTP = "";

           
            DataTable dt = CommonHelperCls.getOTPTIME(UserId, "GT");
            if (dt.Rows.Count > 0)
            {
                DateTime OTPTime = Convert.ToDateTime(dt.Rows[0]["DTM_CREATED_ON"].ToString());
                DateTime currentTime = DateTime.Now;
                DateTime Time = OTPTime.AddMinutes(10);
                if (currentTime > Time)
                {
                    Hid_Time_Left.Value = "0:01";
                    Lbl_Msg.Text = "OTP Time expired !";
                }
                else
                {
                    int strTimeDiff = (int)currentTime.Subtract(OTPTime).TotalSeconds;

                    string minute = (strTimeDiff / 60).ToString();
                    string second = (strTimeDiff % 60).ToString();

                    if (Convert.ToInt32(second) < 10)
                    {
                        second = "0" + second;
                    }

                    string strTimeElapsed = minute + "." + second;
                    decimal decTimeLeft = Convert.ToDecimal(9.60) - Convert.ToDecimal(strTimeElapsed); //// 9.60 is 9 minutes and 60 seconds which is equal to 10 minutes
                    string[] strTimeLeft = decTimeLeft.ToString().Split('.');
                    Hid_Time_Left.Value = strTimeLeft[0] + ":" + strTimeLeft[1];

                    strOTP = Convert.ToString(dt.Rows[0]["VCH_MOB_OTP"]);
                }
            }

            return strOTP;
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
        
    }

    #endregion
}