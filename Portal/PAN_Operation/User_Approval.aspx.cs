#region  PageInfo
//******************************************************************************************************************
// File Name             :   User_Approval.aspx.cs
// Description           :   Approval of child user (Child Unit)
// Created by            :   Sushant Jena
// Created On            :   27-Aug-2018
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
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.Data;
using System.Configuration;
using DWHServiceReference;

public partial class Portal_PAN_Operation_User_Approval : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillGrid();
        }
    }

    #region FunctionUsed

    ///// Fill Pending Units   
    private void FillGrid()
    {
        InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        InvestorDetails objEntity = new InvestorDetails();

        DataTable dt = new DataTable();
        try
        {
            objEntity.strAction = "V3";
            objEntity.strPanno = Txt_PAN.Text.Trim();
            objEntity.strIndName = Txt_Unit_Name.Text.Trim();
            objEntity.IntInvestorId = Convert.ToInt32(Session["Userid"]);

            /////// Select Data
            dt = objBAL.UserManagementView(objEntity);

            GrdChildUser.DataSource = dt;
            GrdChildUser.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserApproval");
        }
        finally
        {
            dt = null;
            objBAL = null;
            objEntity = null;
        }
    }

    ///// Clear Input Fields
    private void ClearField()
    {
        Txt_Rejection_Cause.Text = "";
        Lbl_Investor_Name_Reject.Text = "";
        Lbl_User_Id_Reject.Text = "";
        Hid_Investor_Id_Reject.Value = "";
        Hid_Email_Id_Reject.Value = "";
        Hid_Mobile_No_Reject.Value = "";
    }

    ///// Send Email and SMS
    private void SendEmailSms(string strEmailId, string strMobileNo, string strUnitName, string strRejectionCause, int intMsgType)
    {
        CommonHelperCls objComm = new CommonHelperCls();
        InvestorDetails objInvDet = new InvestorDetails();
        InvestorRegistration objInvService = new InvestorRegistration();

        try
        {
            if (intMsgType == 1)//// Approval Message
            {
                /*------------------------------------------------------*/
                ////// Mail Section
                /*------------------------------------------------------*/
                string strSubject = "GOSWIFT || Unit Registration Approval";
                string strEmailContent = "The registration of your unit " + strUnitName + " is successfully activated by department for your use."
                                        + "</br>"
                                        + "For further clarification you may contact in toll free number: 1800 345 7157 (Timing 10.00 AM to 6.00 PM on working days) and email: support.investodisha@nic.in";

                string[] InvToEmail = new string[1];
                InvToEmail[0] = strEmailId;

                ////Send Email
                bool mailStatus = objComm.sendMail(strSubject, strEmailContent, InvToEmail, true);

                /*------------------------------------------------------*/
                ////// SMS Section
                /*------------------------------------------------------*/
                //string strSMSContent = "The registration of your unit " + strUnitName + " is successfully activated by department for your use.";
                //bool smsStatus = objComm.SendSmsNew(strMobileNo, strSMSContent);
                //The registration of your unit [INDUSTRY NAME] is successfully activated by department for your use. 

                objInvDet.strAction = "ST2";
                DataTable dtcontent = objInvService.GetSMSContent(objInvDet);
                if (dtcontent.Rows.Count > 0)
                {
                    /*----------------------------------------------------------------*/
                    ////// Prepare SMS and Mail Content
                    /*----------------------------------------------------------------*/
                    strSubject = dtcontent.Rows[0]["vchEvent"].ToString();
                    string strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[INDUSTRY NAME]", strUnitName);
                    string strTemplateId = dtcontent.Rows[0]["vchTemplateId"].ToString();
                    string strMsgType = dtcontent.Rows[0]["vchMsgType"].ToString();

                    /*----------------------------------------------------------------*/
                    ///// Send SMS
                    /*----------------------------------------------------------------*/
                    bool smsStatus = objComm.SendSmsWithTemplate(strMobileNo, strSMSContent, strTemplateId, strMsgType);

                    /*----------------------------------------------------------------*/
                    ////// Update SMS and Email Status in Transaction Table
                    /*----------------------------------------------------------------*/
                    string str = objComm.UpdateMailSMSStaus("UnitRegistrationApproval", strMobileNo, InvToEmail[0], strSubject, "0", "0", 0, "0", strSMSContent, strEmailContent, smsStatus, mailStatus);
                }

                /*----------------------------------------------------------------*/
                ////// Send Promotional SMS after Registration.
                /*----------------------------------------------------------------*/
                objComm.SendPromotionalSMS(strMobileNo, "AFTER_REGISTRATION");
            }
            else if (intMsgType == 2)//// Rejection Message
            {
                /*------------------------------------------------------*/
                ////// Mail Section
                /*------------------------------------------------------*/
                string strSubject = "GOSWIFT || Unit Registration Rejection";
                string strEmailContent = "The registration of your unit " + strUnitName + " is not activated by the department for following reason:"
                                        + "</br>"
                                        + "<b>" + strRejectionCause + "</b>"
                                        + "</br>"
                                        + "For further clarification you may contact in toll free number: 1800 345 7157 (Timing 10.00 AM to 6.00 PM on working days) and email: support.investodisha@nic.in";

                string[] InvToEmail = new string[1];
                InvToEmail[0] = strEmailId;

                ////Send Email
                bool mailStatus = objComm.sendMail(strSubject, strEmailContent, InvToEmail, true);

                /*------------------------------------------------------*/
                ////// SMS Section
                /*------------------------------------------------------*/
                //string strSMSContent = "The registration of your unit " + strUnitName + " is not activated by the department.";
                //bool smsStatus = objComm.SendSmsNew(strMobileNo, strSMSContent);
                //The registration of your unit[INDUSTRY NAME] is not activated by the department.

                objInvDet.strAction = "ST3";
                DataTable dtcontent = objInvService.GetSMSContent(objInvDet);
                if (dtcontent.Rows.Count > 0)
                {
                    /*----------------------------------------------------------------*/
                    ////// Prepare SMS and Mail Content
                    /*----------------------------------------------------------------*/
                    strSubject = dtcontent.Rows[0]["vchEvent"].ToString();
                    string strSMSContent = dtcontent.Rows[0]["vchSMSContent"].ToString().Replace("[INDUSTRY NAME]", strUnitName);
                    string strTemplateId = dtcontent.Rows[0]["vchTemplateId"].ToString();
                    string strMsgType = dtcontent.Rows[0]["vchMsgType"].ToString();

                    /*----------------------------------------------------------------*/
                    ///// Send SMS
                    /*----------------------------------------------------------------*/
                    bool smsStatus = objComm.SendSmsWithTemplate(strMobileNo, strSMSContent, strTemplateId, strMsgType);

                    /*----------------------------------------------------------------*/
                    ////// Update SMS and Email Status in Transaction Table
                    /*----------------------------------------------------------------*/
                    string str = objComm.UpdateMailSMSStaus("UnitRegistrationRejection", strMobileNo, InvToEmail[0], strSubject, "0", "0", 0, "0", strSMSContent, strEmailContent, smsStatus, mailStatus);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserApproval");
        }
        finally
        {
            objComm = null;
        }
    }

    #endregion

    #region ButtonClickEvents

    /////// Approve Unit
    protected void LnkBtn_Approve_Click(object sender, EventArgs e)
    {
        InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        InvestorDetails objEntity = new InvestorDetails();

        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            /*--------------------------------------------------------------------------*/

            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");
            HiddenField Hid_Industry_Type = (HiddenField)row.FindControl("Hid_Industry_Type");

            if (Hid_Industry_Type.Value == "1") ///// Industry
            {
                DataTable dt = new DataTable();
                objEntity.strAction = "V4";
                objEntity.IntInvestorId = Convert.ToInt32(Hid_Investor_Id.Value);

                /////// Select Data
                dt = objBAL.UserManagementView(objEntity);
                if (dt.Rows.Count > 0)
                {
                    /*-----------------------------------------------------------------*/
                    /////// Service Initialization
                    DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                    DWH_Model objEnt = new DWH_Model();

                    /*-----------------------------------------------------------------*/
                    /////// Assign value to property

                    string strUnitName = Convert.ToString(dt.Rows[0]["VCH_INV_NAME"]);
                    string strEmailId = Convert.ToString(dt.Rows[0]["VCH_EMAIL"]);
                    string strMobileNo = Convert.ToString(dt.Rows[0]["VCH_OFF_MOBILE"]);

                    objEnt.VCHINDUSTRYNAME = strUnitName;
                    objEnt.VCHEMAILID = strEmailId;
                    objEnt.VCHMOBILENO = strMobileNo;
                    objEnt.INTSALUTATION = Convert.ToInt32(dt.Rows[0]["INT_SALUTATION"]);
                    objEnt.VCHPROMOTERFNAME = Convert.ToString(dt.Rows[0]["VCH_CONTACT_FIRSTNAME"]);
                    objEnt.VCHPROMOTERMNAME = Convert.ToString(dt.Rows[0]["VCH_CONTACT_MIDDLENAME"]);
                    objEnt.VCHPROMOTERLNAME = Convert.ToString(dt.Rows[0]["VCH_CONTACT_LASTNAME"]);
                    objEnt.VCHADDRESS = Convert.ToString(dt.Rows[0]["VCH_ADDRESS"]);
                    objEnt.VCHUSERNAME = Convert.ToString(dt.Rows[0]["VCH_INV_USERID"]);
                    objEnt.VCHPASSWORD = Convert.ToString(dt.Rows[0]["VCH_INV_PASSWORD"]);
                    objEnt.INTDISTRICT = Convert.ToInt32(dt.Rows[0]["INT_DISTRICT"]);
                    objEnt.INTBLOCK = Convert.ToInt32(dt.Rows[0]["INT_BLOCK"]);
                    objEnt.INTSECTOR = Convert.ToInt32(dt.Rows[0]["INT_SECTOR"]);
                    objEnt.INTSUBSECTOR = Convert.ToInt32(dt.Rows[0]["INT_SUBSECTOR"]);
                    objEnt.INTPARENTID = Convert.ToInt32(dt.Rows[0]["INT_PARENT_ID"]);
                    objEnt.VCHPANNO = Convert.ToString(dt.Rows[0]["VCH_PAN"]);
                    objEnt.VCHEINIEM = Convert.ToString(dt.Rows[0]["VCH_EIN_IEM"]);
                    objEnt.VCHLICENCENOTYPE = Convert.ToString(dt.Rows[0]["VCH_LICENCE_NO_TYPE"]);
                    objEnt.VCHLICENCEDOC = Convert.ToString(dt.Rows[0]["VCH_LICENCE_DOC"]);
                    objEnt.INTUSERLEVEL = Convert.ToInt32(dt.Rows[0]["INT_USER_LEVEL"]);
                    objEnt.VCHUSERUNIQUEID = Convert.ToString(dt.Rows[0]["VCH_UNIQUE_ID_PARENT"]);
                    objEnt.VCHCORADDRESS = Convert.ToString(dt.Rows[0]["VCH_SITELOCATION"]);
                    objEnt.VCHTINNO = Convert.ToString(dt.Rows[0]["VCH_GSTIN"]);
                    objEnt.INTINDUSTRYCATEGORY = Convert.ToInt32(dt.Rows[0]["INT_CATEGORY"]);
                    objEnt.INTENTITYTYPE = Convert.ToInt32(dt.Rows[0]["INT_ENTITY_TYPE"]);  // add by anil
                    objEnt.VCHCINNUMBER = Convert.ToString(dt.Rows[0]["VCH_CIN_NUMBER"]);  // add by anil

                    //objList.intsecur = Convert.ToString(dt.Rows[0]["INT_SEC_QUES"]);
                    //objList.vchan = Convert.ToString(dt.Rows[0]["VCH_SEC_ANSWER"]);
                    //objList.intcre = Convert.ToString(dt.Rows[0]["INT_CREATED_BY"]);
                    //objList.vchcrea = Convert.ToString(dt.Rows[0]["DTM_CREATED_ON"]);
                    //objList.VCHINDUSTRYNAME = Convert.ToString(dt.Rows[0]["INT_UPDATED_BY"]);
                    //objList.VCHINDUSTRYNAME = Convert.ToString(dt.Rows[0]["DTM_UPDATED_ON"]);
                    //objList.intsts = Convert.ToString(dt.Rows[0]["INT_STATUS"]);
                    //objList.intot = Convert.ToString(dt.Rows[0]["INT_OTP_STATUS"]);
                    //objList.VCHREm = Convert.ToString(dt.Rows[0]["VCH_REMARKS"]);
                    //objList.vch = Convert.ToString(dt.Rows[0]["INT_COUNTRY"]); 

                    /*-----------------------------------------------------------------*/
                    /////// Generate Encryption Key (Security key to access Data Warehouse servce methods)
                    string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                    string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

                    /*-----------------------------------------------------------------*/
                    /////// DML opertion through service
                    string strReturnVal = objSrvRef.UserRegistration(objEnt, strSecurityKey);
                    if (strReturnVal != "")
                    {
                        string[] strArrRetVal = strReturnVal.Split('_');

                        if (strArrRetVal[0] == "1")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>User name already exists !</strong>', '" + strProjName + "'); </script>", false);
                            return;
                        }
                        else if (strArrRetVal[0] == "2")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Mobile number already exists !</strong>', '" + strProjName + "'); </script>", false);
                            return;
                        }
                        else if (strArrRetVal[0] == "3")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Email id already exists !</strong>', '" + strProjName + "'); </script>", false);
                            return;
                        }
                        else if (strArrRetVal[0] == "5")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>CIN number already exists !</strong>', '" + strProjName + "'); </script>", false);
                            return;
                        }
                        else if (strArrRetVal[0] == "4")
                        {
                            string strUniqueId = strArrRetVal[1];

                            /*-----------------------------------------------------------------*/
                            objEntity.strAction = "AP";
                            objEntity.IntInvestorId = Convert.ToInt32(Hid_Investor_Id.Value);
                            objEntity.IntCreatedBy = Convert.ToInt32(Session["Userid"]);
                            objEntity.strUniqueId = strUniqueId;

                            //////// DML Operation
                            string strReturnStatus = objBAL.UserManagementAED(objEntity);
                            if (strReturnStatus == "1")
                            {
                                ////// Send Email and SMS for User Approval Confirmation
                                SendEmailSms(strEmailId, strMobileNo, strUnitName, "", 1);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>User Approved Successfully !</strong>', '" + strProjName + "'); </script>", false);
                                FillGrid();
                            }
                            else if (strReturnStatus == "2")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Internal Server Error,Please Try After Sometime !</strong>', '" + strProjName + "'); </script>", false);
                            }
                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong,Please try again !</strong>', '" + strProjName + "'); </script>", false);
                }
            }
            else if (Hid_Industry_Type.Value == "2") ///// Non Industry Type .by anil sahoo
            {
                objEntity.strAction = "AP";
                objEntity.IntInvestorId = Convert.ToInt32(Hid_Investor_Id.Value);
                objEntity.IntCreatedBy = Convert.ToInt32(Session["Userid"]);
                Guid Uniqe_id = Guid.NewGuid(); /// for generate unique id localy . by anil sahoo
                objEntity.strUniqueId = Convert.ToString(Uniqe_id);

                //////// DML Operation
                string strReturnStatus = objBAL.UserManagementAED(objEntity);
                if (strReturnStatus == "1")// approve
                {

                    Label Lbl_Mobil_No = (Label)row.FindControl("Lbl_Mobile_No");
                    Label Lbl_Email_id = (Label)row.FindControl("Lbl_Email_Id");
                    //HiddenField Hid_Investor_Name = (HiddenField)row.FindControl("Hid_Investor_Name");
                    LinkButton LnkBtn_Inv_Name = (LinkButton)row.FindControl("LnkBtn_Inv_Name");

                    ////// Send Email and SMS for User Approval Confirmation
                    SendEmailSms(Lbl_Email_id.Text, Lbl_Mobil_No.Text, LnkBtn_Inv_Name.Text, "", 1);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>User Approved Successfully !</strong>', '" + strProjName + "'); </script>", false);
                    FillGrid();
                }
                else if (strReturnStatus == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Internal Server Error,Please Try After Sometime !</strong>', '" + strProjName + "'); </script>", false);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserApproval");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }

    /////// Confirm Before Rejection of Unit
    protected void LnkBtn_Reject_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            Label Lbl_User_Id = (Label)row.FindControl("Lbl_User_Id");
            Label Lbl_Email_Id = (Label)row.FindControl("Lbl_Email_Id");
            Label Lbl_Mobile_No = (Label)row.FindControl("Lbl_Mobile_No");
            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");
            LinkButton LnkBtn_Inv_Name = (LinkButton)row.FindControl("LnkBtn_Inv_Name");

            Lbl_Investor_Name_Reject.Text = LnkBtn_Inv_Name.Text;
            Lbl_User_Id_Reject.Text = Lbl_User_Id.Text;
            Hid_Investor_Id_Reject.Value = Hid_Investor_Id.Value;
            Hid_Email_Id_Reject.Value = Lbl_Email_Id.Text;
            Hid_Mobile_No_Reject.Value = Lbl_Mobile_No.Text;

            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserApproval");
        }
    }

    /////// Reject Unit
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        InvestorDetails objEntity = new InvestorDetails();

        try
        {
            objEntity.strAction = "RJ";
            objEntity.IntInvestorId = Convert.ToInt32(Hid_Investor_Id_Reject.Value);
            objEntity.strRejectionCause = Txt_Rejection_Cause.Text;
            objEntity.IntCreatedBy = Convert.ToInt32(Session["Userid"]);

            //////// DML Operation
            string strReturnStatus = objBAL.UserManagementAED(objEntity);
            if (strReturnStatus == "1")
            {
                ////// Send Email and SMS for User Approval Confirmation
                SendEmailSms(Hid_Email_Id_Reject.Value, Hid_Mobile_No_Reject.Value, Lbl_Investor_Name_Reject.Text, Txt_Rejection_Cause.Text, 2);
                ClearField();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Unit Rejected Successfully !</strong>', '" + strProjName + "'); </script>", false);
                FillGrid();
            }
            else if (strReturnStatus == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Internal Server Error,Please Try After Sometime !</strong>', '" + strProjName + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserApproval");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }

    /////// Button Search
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        FillGrid();
    }

    /////// View Investor Details
    protected void LnkBtn_Inv_Name_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");
            HiddenField Hid_Investor_UserId  = (HiddenField)row.FindControl("Hid_Investor_UserId");

            Response.Redirect("View_Investor_Regd_Details.aspx?val=" + Hid_Investor_Id.Value + "~2" + "~" + Hid_Investor_UserId.Value, false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
    }

    #endregion
}