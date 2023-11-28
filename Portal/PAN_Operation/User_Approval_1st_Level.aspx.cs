#region  PageInfo
//******************************************************************************************************************
// File Name             :   User_Approval_1st_Level.aspx.cs
// Description           :   1st level approval for those MSME unit which has not provided their EIN/PC details during registration
// Created by            :   Sushant Jena
// Created On            :   04-Sep-2019
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

public partial class Portal_PAN_Operation_User_Approval_1st_Level : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillGrid();
        }
    }

    #region FunctionUsed

    ///// Fill Pending Units   
    private void fillGrid()
    {
        InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        InvestorDetails objEntity = new InvestorDetails();

        DataTable dt = new DataTable();
        try
        {
            objEntity.strAction = "V7";
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
    private void clearField()
    {
        Txt_Rejection_Cause.Text = "";
        Lbl_Investor_Name_Reject.Text = "";
        Lbl_User_Id_Reject.Text = "";
        Hid_Investor_Id_Reject.Value = "";
    }

    ///// Send Email and SMS
    private void sendEmailSms(string strEmailId, string strMobileNo, string strUnitName)
    {
        CommonHelperCls objComm = new CommonHelperCls();
        try
        {
            /*------------------------------------------------------*/
            ////// Mail Section
            /*------------------------------------------------------*/
            string strSubject = "GOSWIFT || Unit Registration Approval";
            string strEmailContent = "The registration for unit " + strUnitName + " has been approved."
                                    + "</br>"
                                    + "Now you can login to your account by using your login credentials.";

            string[] InvToEmail = new string[1];
            InvToEmail[0] = strEmailId;

            bool mailStatus = objComm.sendMail(strSubject, strEmailContent, InvToEmail, true);

            /*------------------------------------------------------*/
            ////// SMS Section
            /*------------------------------------------------------*/
            string strSMSContent = "The registration for unit " + strUnitName + " has been approved.Now you can login to your account by using your login credentials.";
            bool smsStatus = objComm.SendSmsNew(strMobileNo, strSMSContent);
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
                string strSiteLocation = Convert.ToString(dt.Rows[0]["VCH_SITELOCATION"]);
                string strApplicantFName = Convert.ToString(dt.Rows[0]["VCH_CONTACT_FIRSTNAME"]);
                string strApplicantMName = Convert.ToString(dt.Rows[0]["VCH_CONTACT_MIDDLENAME"]);
                string strApplicantLName = Convert.ToString(dt.Rows[0]["VCH_CONTACT_LASTNAME"]);
                string strApplicantAddress = Convert.ToString(dt.Rows[0]["VCH_ADDRESS"]);
                int intIndustryCategory = Convert.ToInt32(dt.Rows[0]["INT_CATEGORY"]);
                int intDistrictId = Convert.ToInt32(dt.Rows[0]["INT_DISTRICT"]);
                int intBlockId = Convert.ToInt32(dt.Rows[0]["INT_BLOCK"]);
                int intSectorId = Convert.ToInt32(dt.Rows[0]["INT_SECTOR"]);
                int intSubSectorId = Convert.ToInt32(dt.Rows[0]["INT_SUBSECTOR"]);
                string strPan = Convert.ToString(dt.Rows[0]["VCH_PAN"]);
                string strUserId = Convert.ToString(dt.Rows[0]["VCH_INV_USERID"]);
                string strGSTIN = Convert.ToString(dt.Rows[0]["VCH_GSTIN"]);
                string strLicenceNoType = Convert.ToString(dt.Rows[0]["VCH_LICENCE_NO_TYPE"]);
                string strLicenceNo = Convert.ToString(dt.Rows[0]["VCH_EIN_IEM"]);

                objEnt.VCHINDUSTRYNAME = strUnitName;
                objEnt.VCHEMAILID = strEmailId;
                objEnt.VCHMOBILENO = strMobileNo;
                objEnt.INTSALUTATION = Convert.ToInt32(dt.Rows[0]["INT_SALUTATION"]);
                objEnt.VCHPROMOTERFNAME = strApplicantFName;
                objEnt.VCHPROMOTERMNAME = strApplicantMName;
                objEnt.VCHPROMOTERLNAME = strApplicantLName;
                objEnt.VCHADDRESS = strApplicantAddress;
                objEnt.VCHUSERNAME = strUserId;
                objEnt.VCHPASSWORD = Convert.ToString(dt.Rows[0]["VCH_INV_PASSWORD"]);
                objEnt.INTDISTRICT = intDistrictId;
                objEnt.INTBLOCK = intBlockId;
                objEnt.INTSECTOR = intSectorId;
                objEnt.INTSUBSECTOR = intSubSectorId;
                objEnt.INTPARENTID = Convert.ToInt32(dt.Rows[0]["INT_PARENT_ID"]);
                objEnt.VCHPANNO = strPan;
                objEnt.VCHEINIEM = strLicenceNo;
                objEnt.VCHLICENCENOTYPE = strLicenceNoType;
                objEnt.VCHLICENCEDOC = Convert.ToString(dt.Rows[0]["VCH_LICENCE_DOC"]);
                objEnt.INTUSERLEVEL = Convert.ToInt32(dt.Rows[0]["INT_USER_LEVEL"]);
                objEnt.VCHUSERUNIQUEID = Convert.ToString(dt.Rows[0]["VCH_UNIQUE_ID_PARENT"]);
                objEnt.VCHCORADDRESS = strSiteLocation;
                objEnt.VCHTINNO = strGSTIN;
                objEnt.INTINDUSTRYCATEGORY = intIndustryCategory;
                objEnt.INTAPPROVALLEVEL = 1; //// 1-Refers to 1st Level Approval

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
                    else if (strArrRetVal[0] == "4") //// Successfully data pushed to DWH
                    {
                        string strUniqueId = strArrRetVal[1];

                        /*-----------------------------------------------------------------*/
                        ///// After successfully pushing data to DWH, Update the UNIQUE ID returned by DWH, in GOSWIFT db.
                        ///// In this case unique id along with 1st Level approval status will be updated.
                        /*-----------------------------------------------------------------*/
                        objEntity.strAction = "APF"; //// Approved Application for 1st Level
                        objEntity.IntInvestorId = Convert.ToInt32(Hid_Investor_Id.Value);
                        objEntity.IntCreatedBy = Convert.ToInt32(Session["Userid"]);
                        objEntity.strUniqueId = strUniqueId;

                        //////// DML Operation
                        string strReturnStatus = objBAL.UserManagementAED(objEntity);
                        if (strReturnStatus == "1") //// Successfully updated at GOSWIFT
                        {
                            /*-----------------------------------------------------------------*/
                            //// After successfully updating approval status and unique id in GOSWIFT,push data to AIM portal
                            //// Use "insertrecord" method to push data into AIM portal
                            /*-----------------------------------------------------------------*/
                            AIMServiceReference.UserRegistrationClient objAimRegd = new AIMServiceReference.UserRegistrationClient();
                            AIMServiceReference.userregistration_propEntity objAimEnity = new AIMServiceReference.userregistration_propEntity();

                            objAimEnity.SITELOCATION = strSiteLocation;
                            objAimEnity.CATEGORY = intIndustryCategory.ToString();
                            objAimEnity.INV_NAME = strUnitName;
                            objAimEnity.Enterprenenur_Name = strApplicantFName + " " + strApplicantMName + " " + strApplicantLName;
                            objAimEnity.DISTRICT = intDistrictId.ToString();
                            objAimEnity.BLOCK = intBlockId.ToString();
                            objAimEnity.SECTOR = intSectorId.ToString();
                            objAimEnity.SUBSECTOR = intSubSectorId.ToString();
                            objAimEnity.ADDRESS = strApplicantAddress;
                            objAimEnity.CELLPHONE_NO = strMobileNo;
                            objAimEnity.E_MAIL = strEmailId;
                            objAimEnity.INV_USERID = strUserId;
                            objAimEnity.PAN = strPan;
                            objAimEnity.GSTIN = strGSTIN;
                            objAimEnity.UNIQUEID = strUniqueId;
                            objAimEnity.LICENCE_NO_TYPE = strLicenceNoType;
                            objAimEnity.EIN_PC = strLicenceNo;
                            objAimEnity.Status = "F"; //// For 1st Level Approval


                            //// Call AIM Insert Method
                            int intReturnValAIM = objAimRegd.insertrecord(objAimEnity);
                            if (intReturnValAIM == 1)
                            {
                                //////// Send Email and SMS for User Approval Confirmation
                                //sendEmailSms(strEmailId, strMobileNo, strUnitName);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>User Approved Successfully !</strong>', '" + strProjName + "'); </script>", false);
                                fillGrid();
                            }
                            else if (intReturnValAIM == 2)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Duplicate user found at AIM portal !</strong>', '" + strProjName + "'); </script>", false);
                                fillGrid();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong at AIM portal !</strong>', '" + strProjName + "'); </script>", false);
                            }
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
            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");
            LinkButton LnkBtn_Inv_Name = (LinkButton)row.FindControl("LnkBtn_Inv_Name");

            Lbl_Investor_Name_Reject.Text = LnkBtn_Inv_Name.Text;
            Lbl_User_Id_Reject.Text = Lbl_User_Id.Text;
            Hid_Investor_Id_Reject.Value = Hid_Investor_Id.Value;

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
                clearField();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Unit Rejected Successfully !</strong>', '" + strProjName + "'); </script>", false);
                fillGrid();
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
        fillGrid();
    }

    /////// View Investor Details
    protected void LnkBtn_Inv_Name_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            HiddenField Hid_Investor_Id = (HiddenField)row.FindControl("Hid_Investor_Id");

            Response.Redirect("View_Investor_Regd_Details.aspx?val=" + Hid_Investor_Id.Value + "~4", false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UserEnquiry");
        }
    }

    #endregion
}