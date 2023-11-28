#region Page Information
//******************************************************************************************************************
// File Name             : InvForgotUserId.aspx
// Description           : Investor Forgot User Id
// Created by            : Sushant Kumar Jena
// Created On            : 24-Aug-2018
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
using System.Configuration;
using System.Data;
using BusinessLogicLayer.Login;
using EntityLayer.Login;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;

public partial class InvForgotUserId : System.Web.UI.Page
{
    #region Variables
   
    LoginBusinessLayer objService = new LoginBusinessLayer();
    LoginDetails objLogin = new LoginDetails();
    CommonHelperCls objComm = new CommonHelperCls();

    #endregion

    /////Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Div_OTP.Visible = false;
            Div_Input.Visible = true;
        }
    }

    #region ButtonClick

    /// <summary>
    /// Button summit to check valid PAN or Email id existance
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        Div_OTP.Visible = false;
        Div_Input.Visible = true;
        ModalPopupExtender1.Hide();

        /*--------------------------------------------------------*/

        DataTable dt = new DataTable();
        try
        {
            if (Txt_Email_Id.Text.Trim() != "")
            {
                objLogin.strAction = "FUIDEMAIL"; //// Forget User Id,Recovery Through Email Id
            }
            else if (Txt_PAN.Text.Trim() != "")
            {
                objLogin.strAction = "FUIDPAN"; //// Forget User Id,Recovery Through PAN
            }

            objLogin.strEmail = Txt_Email_Id.Text.Trim();
            objLogin.strPAN = Txt_PAN.Text.Trim();

            /////// Check Existance
            dt = objService.forgotUserId(objLogin);
            if (dt.Rows.Count > 0)
            {
                string strReturnStatus = dt.Rows[0]["RETURN_STATUS"].ToString();

                if (strReturnStatus == "1")
                {
                    if (Txt_Email_Id.Text.Trim() != "") //// If validated through mail id.
                    {
                        string strMobileNo = Convert.ToString(dt.Rows[0]["VCH_OFF_MOBILE"]);
                        string strEmailId = Convert.ToString(dt.Rows[0]["VCH_EMAIL"]);
                        string strUserId = Convert.ToString(dt.Rows[0]["VCH_INV_USERID"]);
                        string strPAN = Convert.ToString(dt.Rows[0]["VCH_PAN"]);

                        ViewState["PAN"] = strPAN;
                        ViewState["EmailId"] = strEmailId;
                        ViewState["UserId"] = strUserId;

                        generateOTP(strMobileNo, strEmailId);
                    }
                    else if (Txt_PAN.Text.Trim() != "") //// If validated through PAN.
                    {
                        GrdUserList.DataSource = dt;
                        GrdUserList.DataBind();

                        ModalPopupExtender1.Show();
                    }
                }
                else if (strReturnStatus == "2")
                {
                    if (Txt_Email_Id.Text.Trim() != "")
                    {
                        ScriptManager.RegisterStartupScript(Btn_Submit, this.GetType(), "OnClick", "<script>jAlert('<strong>The Email id does not exist !</strong>');</script>", false);
                        return;
                    }
                    else if (Txt_PAN.Text.Trim() != "")
                    {
                        ScriptManager.RegisterStartupScript(Btn_Submit, this.GetType(), "OnClick", "<script>jAlert('<strong>The PAN does not exist !</strong>');</script>", false);
                        return;
                    }
                }
            }
            else
            {
                ClearData();
                ScriptManager.RegisterStartupScript(Btn_Submit, this.GetType(), "OnClick", "<script>jAlert('<strong>Invalid email id or PAN provided !</strong>');</script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ForgotUserId");
        }
        finally
        {
            dt = null;
        }
    }

    /// <summary>
    /// Submit Button in Modal PopUp
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Submit_Popup_Click(object sender, EventArgs e)
    {
        try
        {
            int intChkCount = 0;
            for (int i = 0; i < GrdUserList.Rows.Count; i++)
            {
                RadioButton RadBtn = (RadioButton)GrdUserList.Rows[i].FindControl("RadBtn_User_Select");
                if (RadBtn.Checked)
                {
                    intChkCount = 1;

                    string strMobileNo = GrdUserList.DataKeys[GrdUserList.Rows[i].RowIndex]["VCH_OFF_MOBILE_ACTUAL"].ToString();
                    string strEmailId = GrdUserList.DataKeys[GrdUserList.Rows[i].RowIndex]["VCH_EMAIL_ACTUAL"].ToString();
                    string strUserId = GrdUserList.DataKeys[GrdUserList.Rows[i].RowIndex]["VCH_INV_USERID"].ToString();
                    string strPAN = GrdUserList.DataKeys[GrdUserList.Rows[i].RowIndex]["VCH_PAN"].ToString();

                    ViewState["PAN"] = strPAN;
                    ViewState["EmailId"] = strEmailId;
                    ViewState["UserId"] = strUserId;

                    generateOTP(strMobileNo, strEmailId);
                    ModalPopupExtender1.Hide();

                    break;
                }
            }

            if (intChkCount == 0) ///// If no record selected.
            {
                ModalPopupExtender1.Show();
                ScriptManager.RegisterStartupScript(Btn_Submit, this.GetType(), "OnClick", "<script>jAlert('<strong>Please select one record to recover user id !</strong>');</script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ForgotUserId");
        }
    }

    /// <summary>
    /// Validate OTP and send user id to their respective email id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Btn_Validate_OTP_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["OTPFUID"] != null)
            {
                if (Convert.ToString(Session["OTPFUID"]) == Txt_OTP.Text)  //// If OTP matched
                {
                    string[] strEmailId = new string[1];

                    string strPAN = Convert.ToString(ViewState["PAN"]);
                    strEmailId[0] = Convert.ToString(ViewState["EmailId"]);
                    string strUserId = Convert.ToString(ViewState["UserId"]);

                    string strSubject = "GOSWIFT || Forgot User Id";

                    string strBody = "";
                    if (strPAN != "")
                    {
                        strBody = "Dear User"
                            + "<br>"
                            + "Your user id is " + strUserId
                            + ".";
                    }
                    else
                    {
                        strBody = "Dear User"
                           + "<br>"
                           + "Your user id is " + strUserId
                           + ".<br>"
                           + "Please note that you have not update you PAN yet.Please update your PAN during login.";
                    }

                    ////// Send Mail
                   objComm.sendMail(strSubject, strBody, strEmailId, true);

                    Session["OTPFUID"] = null;
                    string message = @"<strong>Your user id has been sent to your email id,Please check your email !</strong>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + message + "');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Btn_Submit, this.GetType(), "OnClick", "<script>jAlert('<strong>OTP does not match !</strong>');</script>", false);
                }
            }
            else
            {
                Session["OTPFUID"] = null;
                Response.Redirect("Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ForgotUserId");
        }
    }

    #endregion

    #region CommonFunction

    /// <summary>
    /// Generate OTP and send to respective mobile.
    /// </summary>
    /// <param name="strMobileNo"></param>
    private void generateOTP(string strMobileNo, string strEmailId)
    {
        InvestorDetails objInvDet = new InvestorDetails();
        InvestorRegistration objInvService = new InvestorRegistration();

        try
        {
            /*----------------------------------------------------------------*/
            ////// Generate 4 digit OTP
            /*----------------------------------------------------------------*/
            string strOTP = MakeRandom(4);
            Session["OTPFUID"] = strOTP;

            /*----------------------------------------------------------------*/
            ////// Send SMS
            /*----------------------------------------------------------------*/
            objInvDet.strAction = "ST1";
            DataTable dtcontent = objInvService.GetSMSContent(objInvDet);
            if (dtcontent.Rows.Count > 0)
            {
                /*----------------------------------------------------------------*/
                ////// Prepare SMS and Mail Content
                /*----------------------------------------------------------------*/

                string strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[OTPCODE]", strOTP);
                string strTemplateId = dtcontent.Rows[0]["vchTemplateId"].ToString();
                string strMsgType = dtcontent.Rows[0]["vchMsgType"].ToString();


                bool smsStatus = objComm.SendSmsWithTemplate(strMobileNo, strSMSContent, strTemplateId, strMsgType);


                /*----------------------------------------------------------------*/
                ///// Send Mail
                /*----------------------------------------------------------------*/
                string[] strArrEmailId = new string[1];
                strArrEmailId[0] = strEmailId;

                string strSubject = "GOSWIFT || OTP for recovery of user id";
                string strEmailContent = "Dear User"
                                        + "<br>"
                                        + "Your One Time Password for recovery of user id is " + strOTP + ".Please donot share with anybody."
                                        + "<br>";
                bool mailStatus = objComm.sendMail(strSubject, strEmailContent, strArrEmailId, true);
                /*----------------------------------------------------------------*/
                ///Update SMS and Email Status in Transaction Table
                /*----------------------------------------------------------------*/
                string str = objComm.UpdateMailSMSStaus("ForgotUserId", strMobileNo, strArrEmailId[0], "OTP for recovery of user id", "0", "0", 0, "0", strSMSContent, strEmailContent, smsStatus, mailStatus);

                Div_OTP.Visible = true;
                Div_Input.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

    /// <summary>
    /// Clear Fields
    /// </summary>
    public void ClearData()
    {
        Txt_Email_Id.Text = "";
        Txt_PAN.Text = "";
    }

    /// <summary>
    /// To generate random number.
    /// It will generate numeric random number as per the length provided
    /// </summary>
    /// <param name="digitLength"></param>
    /// <returns></returns>
    public string MakeRandom(int digitLength)
    {
        string possibles = "0123456789";
        char[] passwords = new char[digitLength];
        Random rd = new Random();

        for (int i = 0; i < digitLength; i++)
        {
            passwords[i] = possibles[rd.Next(0, possibles.Length)];
        }
        return new string(passwords);
    }

    #endregion
}