#region  PageInfo
//******************************************************************************************************************
// File Name             :   View_Investor_Regd_Details.aspx.cs
// Description           :   View Investor Registration Details
// Created by            :   Sushant Jena
// Created On            :   11-Dec-2018
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>         <Modification Summary>'                                                         
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
using System.Drawing;

using System.Net;
using System.IO;
using DWHServiceReference;
using System.Configuration;


public partial class Portal_PAN_Operation_View_Investor_Regd_Details : System.Web.UI.Page
{
    int intInvetsorId = 0;
    int intRequestId = 0;
    string strUserId = "";

    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    ////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["val"] != null)
            {
                string[] strArr = new string[2];
                strArr = Request.QueryString["val"].Split('~');

                intInvetsorId = Convert.ToInt32(strArr[0]);
                intRequestId = Convert.ToInt32(strArr[1]);
                strUserId = Convert.ToString(strArr[2]);

                if (!IsPostBack)
                {
                    FillDetails(intInvetsorId, intRequestId , strUserId);
                }

                //// intRequestId=1 means Request coming from User_Enquire.aspx Page and M_INVESTOR_DETAILS data to be dispalyed.
                //// intRequestId=2 means Request coming from User_Approval.aspx Page and M_INVESTOR_DETAILS data to be dispalyed.
                //// intRequestId=3 means Request coming from User_Enquire.aspx Page and T_INVESTOR_DETAILS_LOG (Rejected Investor Details) data to be dispalyed.
                //// intRequestId=4 means Request coming from User_Approval_1st_Level.aspx Page and M_INVESTOR_DETAILS data to be dispalyed.
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ViewDetails");
        }
    }

    #region FunctionUsed

    ////// Fill Investor Details
    private void FillDetails(int intInvetsorId, int intRequestId ,string strUserId)
    {
        InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        InvestorDetails objEntity = new InvestorDetails();

        DataTable dt = new DataTable();
        try
        {
            if (intRequestId == 1 || intRequestId == 2 || intRequestId == 4) //// Approve or Pending Investors Details
            {
                objEntity.strAction = "V5"; //// View investor registration details
                objEntity.strUserID = strUserId;
            }
            else if (intRequestId == 3) ///// Rejected Investors Details
            {
                objEntity.strAction = "V6";
                objEntity.strUserID = strUserId;
            }


            objEntity.IntInvestorId = intInvetsorId;

            /////// Select Data
            dt = objBAL.UserManagementView(objEntity);

            if (dt.Rows.Count > 0)
            {
                Lbl_Unit_Name.Text = Convert.ToString(dt.Rows[0]["VCH_INV_NAME"]);
                string strSalutation = Convert.ToString(dt.Rows[0]["VCH_SALUTATION"]);
                string strFirstName = Convert.ToString(dt.Rows[0]["VCH_CONTACT_FIRSTNAME"]);
                string strMiddleName = Convert.ToString(dt.Rows[0]["VCH_CONTACT_MIDDLENAME"]);
                string strLastName = Convert.ToString(dt.Rows[0]["VCH_CONTACT_LASTNAME"]);
                Lbl_Applicant_Name.Text = strSalutation + " " + strFirstName + " " + strMiddleName + " " + strLastName;
                Lbl_Address.Text = Convert.ToString(dt.Rows[0]["VCH_ADDRESS"]);
                Lbl_Mobile_No.Text = Convert.ToString(dt.Rows[0]["VCH_OFF_MOBILE"]);
                Lbl_Email_Id.Text = Convert.ToString(dt.Rows[0]["VCH_EMAIL"]);

                Lbl_Site_Location.Text = Convert.ToString(dt.Rows[0]["VCH_SITELOCATION"]);
                Lbl_District.Text = Convert.ToString(dt.Rows[0]["vchDistrictName"]);
                Lbl_Block.Text = Convert.ToString(dt.Rows[0]["vchBlockName"]);
                Lbl_Sector.Text = Convert.ToString(dt.Rows[0]["VCH_SECTOR"]);
                Lbl_Sub_Sector.Text = Convert.ToString(dt.Rows[0]["vchSubSectorName"]);
                Lbl_Industry_Type.Text = Convert.ToString(dt.Rows[0]["INT_INDUSTRY_TYPE"]);

                /*-------------------------------------------------------------------*/

                string strDocType = Convert.ToString(dt.Rows[0]["VCH_LICENCE_NO_TYPE"]);
                if (strDocType != "")
                {
                    Lbl_EIN_IEM_No.Text = Convert.ToString(dt.Rows[0]["VCH_EIN_IEM"]);
                    Lbl_Doc_Type.Text = strDocType + " Number";

                    if (Convert.ToString(dt.Rows[0]["VCH_LICENCE_DOC"]) != "")
                    {
                        HyperLink1.NavigateUrl = "~/Document/RegdDoc/" + Convert.ToString(dt.Rows[0]["VCH_LICENCE_DOC"]);
                        HyperLink1.Visible = true;
                        Lbl_Doc_Text.Text = "";
                        Lbl_Doc_Text.Visible = false;
                        LnkBtn_Download_Doc.Visible = false;
                    }
                    else
                    {
                        HyperLink1.NavigateUrl = "";
                        HyperLink1.Visible = false;
                        Lbl_Doc_Text.Text = "-NA-";
                        Lbl_Doc_Text.Visible = true;

                        if (Convert.ToString(dt.Rows[0]["VCH_UNIQUEID"]) != "")
                        {
                            LnkBtn_Download_Doc.Visible = true;
                        }
                        else
                        {
                            LnkBtn_Download_Doc.Visible = false;
                        }
                    }
                }
                else
                {
                    Lbl_EIN_IEM_No.Text = "-NA-";
                    Lbl_EIN_IEM_No.ForeColor = Color.Red;
                    Lbl_Doc_Type.Text = "EIN/IEM Number";
                    HyperLink1.NavigateUrl = "";
                    HyperLink1.Visible = false;
                    Lbl_Doc_Text.Text = "-NA-";
                    Lbl_Doc_Text.Visible = true;
                    LnkBtn_Download_Doc.Visible = false;
                }

                /*-------------------------------------------------------------------*/

                Lbl_Unit_Type.Text = Convert.ToString(dt.Rows[0]["INT_PARENT_ID"]) == "0" ? "Main Unit" : "Subsidiary Unit";
                Lbl_GSTIN.Text = Convert.ToString(dt.Rows[0]["VCH_GSTIN"]);
                Lbl_PAN.Text = Convert.ToString(dt.Rows[0]["VCH_PAN"]);
                Lbl_Investment_Level.Text = Convert.ToString(dt.Rows[0]["INT_CATEGORY"]) == "1" ? "Project Cost >= 50 Crore" : "Project Cost < 50 Crore";

                Lbl_User_Id.Text = Convert.ToString(dt.Rows[0]["VCH_INV_USERID"]);
                Lbl_User_Level.Text = Convert.ToString(dt.Rows[0]["INT_USER_LEVEL"]);
                Lbl_Alias_Name.Text = Convert.ToString(dt.Rows[0]["VCH_INV_USERID_ALIAS"]);

                Lbl_AppBy.Text= Convert.ToString(dt.Rows[0]["INT_APPROVED_BY"]);
                /*-------------------------------------------------------------------*/

                string strOTPStatus = Convert.ToString(dt.Rows[0]["INT_OTP_STATUS"]);//add by debiprasanna
                if (strOTPStatus == "1")
                {
                    Lbl_OTP_Status.Text = "Verified";
                    Lbl_OTP_Status.ForeColor = Color.Green;
                }
                else
                {
                    Lbl_OTP_Status.Text = "Pending";
                    Lbl_OTP_Status.ForeColor = Color.Red;
                }

                /*-------------------------------------------------------------------*/

                string strApprovalStatus = Convert.ToString(dt.Rows[0]["INT_APPROVAL_STATUS"]);

                if (intRequestId == 1 || intRequestId == 2 || intRequestId == 4)
                {
                    if (strApprovalStatus == "1")
                    {
                        Lbl_Approval_Status.Text = "Approved";
                        Lbl_Approval_Status.ForeColor = Color.Green;
                    }
                    else
                    {
                        Lbl_Approval_Status.Text = "Pending";
                        Lbl_Approval_Status.ForeColor = Color.Red;
                    }
                }
                else if (intRequestId == 3)
                {
                    Lbl_Approval_Status.Text = "Rejected";
                    Lbl_Approval_Status.ForeColor = Color.Red;
                }

                /*-------------------------------------------------------------------*/

                Lbl_Parent_Unit_Name.Text = Convert.ToString(dt.Rows[0]["VCH_INV_NAME_PARENT"]);
                if (Convert.ToString(dt.Rows[0]["VCH_INV_NAME_PARENT"]) != "")
                {
                    Lbl_Parent_Unit_Name.Text = Convert.ToString(dt.Rows[0]["VCH_INV_NAME_PARENT"]);
                }
                else
                {
                    Lbl_Parent_Unit_Name.Text = "-NA-";
                }

                //Satya Added
                Lbl_RegDate.Text = DateTime.Parse(dt.Rows[0]["dtm_created_on"].ToString()).ToString("dd-MMM-yyyy HH:mm tt");
                if (Convert.ToString(dt.Rows[0]["DTM_APPROVED_ON"]) != "")
                {
                    Lbl_AppDate.Text = DateTime.Parse(dt.Rows[0]["DTM_APPROVED_ON"].ToString()).ToString("dd-MMM-yyyy HH:mm tt");
                }
                else
                {
                    Lbl_AppDate.Text = "-NA-";
                }
                //Satya Added

                /*-----------------------------------------------------------*/
                //Add by Debiprasanna
                Lbl_GSTIN.Text = Convert.ToString(dt.Rows[0]["VCH_GSTIN"]);
                if (Convert.ToString(dt.Rows[0]["VCH_GSTIN"]) != "")
                {
                    Lbl_GSTIN.Text = Convert.ToString(dt.Rows[0]["VCH_GSTIN"]);
                }
                else
                {
                    Lbl_GSTIN.Text = "-NA-";
                }

                Lbl_Alias_Name.Text = Convert.ToString(dt.Rows[0]["VCH_INV_USERID_ALIAS"]);
                if (Convert.ToString(dt.Rows[0]["VCH_INV_USERID_ALIAS"]) != "")
                {
                    Lbl_Alias_Name.Text = Convert.ToString(dt.Rows[0]["VCH_INV_USERID_ALIAS"]);
                }
                else
                {
                    Lbl_Alias_Name.Text = "-NA-";
                }
               

                Lbl_Sector.Text = Convert.ToString(dt.Rows[0]["VCH_SECTOR"]);
                if (Convert.ToString(dt.Rows[0]["VCH_SECTOR"]) != "")
                {
                    Lbl_Sector.Text = Convert.ToString(dt.Rows[0]["VCH_SECTOR"]);
                }
                else
                {
                    Lbl_Sector.Text = "-NA-";
                }
                Lbl_Sub_Sector.Text = Convert.ToString(dt.Rows[0]["vchSubSectorName"]);
                if (Convert.ToString(dt.Rows[0]["vchSubSectorName"]) != "")
                {
                    Lbl_Sub_Sector.Text = Convert.ToString(dt.Rows[0]["vchSubSectorName"]);
                }
                else
                {
                    Lbl_Sub_Sector.Text = "-NA-";
                }               
                //Add by Debiprasanna
                string strApprovalStatuss = Convert.ToString(dt.Rows[0]["INT_APPROVAL_STATUS"]);

                if (intRequestId == 1 || intRequestId == 2 || intRequestId == 4)
                {
                    if (strApprovalStatuss == "1")
                    {
                        Lbl_AppBy.Text = Convert.ToString(dt.Rows[0]["vch_Approveby"]);
                        Lbl_AppBy.ForeColor = Color.Green;
                    }
                    else
                    {
                        Lbl_AppBy.Text = "-NA-";
                        Lbl_AppBy.ForeColor = Color.Red;
                    }
                }
                else if (intRequestId == 3)
                {
                    Lbl_ApprovalName.InnerText = "Rejected By";
                    Lbl_AppBy.Text= Convert.ToString(dt.Rows[0]["vch_RejectedBy"]);
                    Lbl_AppBy.ForeColor = Color.Red;
                }
                else
                {
                    Lbl_AppBy.Text = "-NA-";
                }
                
                /*-------------------------------------------------------------------*/
                ///// First Level Approval Details

                string strApprovalDate1stLevel = Convert.ToString(dt.Rows[0]["DTM_APPROVAL_DATE_1ST_LEVEL"]);
                if (strApprovalDate1stLevel != "")
                {
                    strApprovalDate1stLevel = DateTime.Parse(strApprovalDate1stLevel).ToString("dd-MMM-yyyy HH:mm tt");
                }
                else
                {
                    Lbl_Approval_Date_1st_Level.Text = "-NA-";                   
                }            
                string strApprovalStatus1stLevel = Convert.ToString(dt.Rows[0]["INT_APPROVAL_STATUS_1ST_LEVEL"]);
                if (strApprovalStatus1stLevel == "1")
                {
                    if (strApprovalDate1stLevel == "")
                    {
                        Lbl_App_Req.Text = "No";
                        Lbl_App_Req.ForeColor = Color.Green;

                        Lbl_Approval_Status_1st_Level.Text = "Auto Approved";
                        Lbl_Approval_Status_1st_Level.ForeColor = Color.Orange;
                    }
                    else
                    {
                        Lbl_App_Req.Text = "Yes";
                        Lbl_App_Req.ForeColor = Color.Red;

                        Lbl_Approval_Status_1st_Level.Text = "Approved";
                        Lbl_Approval_Status_1st_Level.ForeColor = Color.Green;
                    }
                }
                else
                {
                    Lbl_Approval_Status_1st_Level.Text = "Pending";
                    Lbl_Approval_Status_1st_Level.ForeColor = Color.Red;
                    Lbl_App_Req.Text = "Yes";
                }

                /*-------------------------------------------------------------------*/
                ///// NSWS Registration Details
                //Add by Debiprasanna
                Lbl_Regd_Source.Text = Convert.ToString(dt.Rows[0]["VCH_REGD_SOURCE"]);
                if (Lbl_Regd_Source.Text == "NSWS")
                {
                    Lbl_SWS_Id.Text = Convert.ToString(dt.Rows[0]["VCH_INV_SWS_ID_NSWS"]);
                    Lbl_Redirection_Url_Sent_Status.Text = Convert.ToString(dt.Rows[0]["INT_URL_SENT_STATUS_NSWS"]) == "0" ? "No" : "Yes";
                    Lbl_Redirection_Url.Text = Convert.ToString(dt.Rows[0]["VCH_REDIRECT_URL_NSWS"]);
                }
                else
                {
                    Lbl_SWS_Id.Text = "-NA-";
                    Lbl_Redirection_Url_Sent_Status.Text = "-NA-";
                    Lbl_Redirection_Url.Text = "-NA-";
                    
                }

                /*------------------------------------------------------------------------*/
                ///approved by or rejected by 
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt = null;
            objBAL = null;
            objEntity = null;
        }
    }

    #endregion

    ////// Button Back     
    protected void Btn_Back_Click(object sender, EventArgs e)
    {
        if (intRequestId == 1 || intRequestId == 3)
        {
            Response.Redirect("User_Enquire.aspx");
        }
        else if (intRequestId == 2)
        {
            Response.Redirect("User_Approval.aspx");
        }
        else if (intRequestId == 4)
        {
            Response.Redirect("User_Approval_1st_Level.aspx");
        }
    }

    protected void LnkBtn_Download_Doc_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    string strUniqueId = Convert.ToString(ViewState["UniqueId"]);
        //    //string strUniqueId = "B973C51D-7DF4-408B-8FC1-2BD91443AF15";
        //    string strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_LICNDOC.pdf";
        //    string strPath = "~/Document/RegdDoc/";

        //    if (!Directory.Exists(Server.MapPath(strPath)))
        //    {
        //        Directory.CreateDirectory(Server.MapPath(strPath));
        //    }

        //    ///// AIM File Handler 
        //    #region    Below this line use for AIM integration proposed (till now  not use) need to commmnet befor update on live
        //    AIMServiceReference.UserRegistrationClient objAimRegd = new AIMServiceReference.UserRegistrationClient();
        //    string ENCRUNIQUEIDKey = objAimRegd.Encrypt(strUniqueId, "AZBY19LXaghkLM12WX");

        //    ///// Send request to download file
        //    byte[] data;
        //    using (WebClient client = new WebClient())
        //    {
        //        string strAddress = "http://164.100.141.243/DownloadPDF.ashx?PARAM=" + ENCRUNIQUEIDKey;

        //        /*----------------------------------------*/
        //        //// to be removed during live
        //        HiddenField1.Value = strAddress;
        //        HiddenField2.Value = strUniqueId;
        //        /*----------------------------------------*/

        //        data = client.DownloadData(strAddress);
        //        //data = client.DownloadData("http://localhost/swp_new/TestDownloadFileHandler.ashx?id=1");

        //    }
        //    #endregion

        //    if (data.Length > 0)
        //    {
        //        if (IsFileValid(data))
        //        {
        //            /////Copy the file to destination folder
        //            File.WriteAllBytes(Server.MapPath(strPath + strFileName), data);

        //            //// Save file to database(GOSWIFT)
        //            InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        //            InvestorDetails objEntity = new InvestorDetails();

        //            objEntity.strAction = "UD";
        //            objEntity.strUniqueId = strUniqueId;
        //            objEntity.strLicenceDoc = strFileName;

        //            ///////DML Operation
        //            string strResult = objBAL.UserManagementAED(objEntity);
        //            if (strResult == "1")
        //            {
        //                /*-----------------------------------------------------------------*/
        //                ///// Once updated in GOSWIFT,update the document name in DWH     
        //                /*-----------------------------------------------------------------*/
        //                /////// Service Initialization
        //                DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
        //                EINModel objEnt = new EINModel
        //                {
        //                    ///// Assign value to property
        //                    ///// intRequestType = 2 Means it will only update document Name
        //                    ///// intRequestType = 1 Means it will only update EIN No and EIN Type
        //                    intRequestType = 2,
        //                    strDocument = strFileName,
        //                    strUniqueId = strUniqueId
        //                };

        //                /////// Generate Encryption Key (Security key to access Data Warehouse servce methods)
        //                string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
        //                string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

        //                /////// DML opertion through service
        //                string strReturnVal = objSrvRef.EinUpdate(objEnt, strSecurityKey);
        //                if (strReturnVal == "11")
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid user !</strong>', '" + strProjName + "'); </script>", false);
        //                    return;
        //                }
        //                else if (strReturnVal == "2")
        //                {
        //                    //// After downloading file, Fill all the details again
        //                    if (Request.QueryString["val"] != null)
        //                    {
        //                        string[] strArr = new string[2];
        //                        strArr = Request.QueryString["val"].Split('~');

        //                        intInvetsorId = Convert.ToInt32(strArr[0]);
        //                        intRequestId = Convert.ToInt32(strArr[1]);
        //                        strUserId = Convert.ToString(strArr[2]);
        //                        FillDetails(intInvetsorId, intRequestId, strUserId);
        //                    }

        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Document downloaded successfully !</strong>', '" + strProjName + "'); </script>", false);
        //                    return;
        //                }
        //                else
        //                {
        //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong,Please try again !</strong>', '" + strProjName + "'); </script>", false);
        //                    return;
        //                }
        //            }
        //            else if (strResult == "2")
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Something went wrong,Please try again !</strong>', '" + strProjName + "'); </script>", false);
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid or corrupted file found !</strong>', '" + strProjName + "'); </script>", false);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>No file found for download !</strong>', '" + strProjName + "'); </script>", false);
        //        return;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Util.LogError(ex, "UpdateLicenceDoc");
        //}
    }

    private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
    private bool IsFileValid(byte[] filebyte)
    {
        if (filebyte.Take(7).SequenceEqual(PDF))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}