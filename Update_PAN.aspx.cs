//******************************************************************************************************************
// File Name             :   Update_PAN.aspx.cs
// Description           :   This page is used to update PAN of investors, which have not provided during registration.
// Created by            :   Sushant Kumar Jena
// Created on            :   31-May-2018
// Modification History  :
//       <CR no.>       <Date>             <Modified by>                <Modification Summary>'                                                          
//          1           03-Aug-2018         Sushant Jena         Update EIN/IEM Number and Creation of Child user in case of duplicate PAN.
//                                                               If the PAN already exists then prompt user to make this child user.
//          2           21-Sep-2018         Sushant Jena         Update EIN/IEM type and supporting document for EIN/IEM.
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Login;
using EntityLayer.Login;
using System.Globalization;
using DWHServiceReference;
using System.Configuration;
using System.IO;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
//using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Net;
using System.Globalization;
using System.Web;
using BusinessLogicLayer.Investor;
using EntityLayer.Investor;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

public partial class Update_PAN : System.Web.UI.Page
{
    ////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    ////// Page Load  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["userid"] != null)
        {
            if (Convert.ToString(Session["userPan"]) == Request.QueryString["userid"].ToString())
            {
                if (!IsPostBack)
                {
                    Lbl_User_Id.Text = Request.QueryString["userid"].ToString();

                    string strPAN = Convert.ToString(Session["PAN"]);
                    string strEINIEM = Convert.ToString(Session["EINIEM"]);

                    /*---------------------------------------------*/

                    if (strPAN != "")
                    {
                        Txt_PAN.Text = strPAN;
                        Txt_PAN.Enabled = false;
                    }
                    else
                    {
                        Txt_PAN.Text = "";
                        Txt_PAN.Enabled = true;
                    }

                    /*---------------------------------------------*/

                    if (strEINIEM != "")
                    {
                        Txt_EIN_IEM.Text = strEINIEM;
                        Txt_EIN_IEM.Enabled = false;
                    }
                    else
                    {
                        Txt_EIN_IEM.Text = "";
                        Txt_EIN_IEM.Enabled = true;
                    }

                    /////Get Industry Details
                    GetIndustryDetails(Convert.ToInt32(Session["InvestorId"]));
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }

    ////// FunctionUsed
    #region FunctionUsed

    ////// Generate Random Numner    
    private string MakeRandom(int intLength)
    {
        string possibles = "0123456789";
        char[] passwords = new char[intLength];
        Random rd = new Random();

        for (int i = 0; i < intLength; i++)
        {
            passwords[i] = possibles[rd.Next(0, possibles.Length)];
        }
        return new string(passwords);
    }

    ////// Method to Upload Document
    private bool UploadDocument(FileUpload FileUpload_Doc, string strFileName, string strFoldername)
    {
        bool bReturnValue = true;
        try
        {
            string strMainFolderPath = Server.MapPath(string.Format("~/Document/{0}/", strFoldername));
            if (!Directory.Exists(strMainFolderPath))
            {
                Directory.CreateDirectory(strMainFolderPath);
            }

            if (FileUpload_Doc.HasFile)
            {
                if (Path.GetExtension(FileUpload_Doc.FileName).ToLower() != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please upload only .pdf file !!</strong>', '" + strProjName + "'); </script>", false);
                    bReturnValue = false;
                    return bReturnValue;
                }

                if (!IsFileValid(FileUpload_Doc))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid file type (or) File name may contain dots !!</strong>', '" + strProjName + "'); </script>", false);
                    bReturnValue = false;
                    return bReturnValue;
                }

                int fileSize = FileUpload_Doc.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>File size is too large. Maximum file size permitted is 4 MB !!</strong>', '" + strProjName + "'); </script>", false);
                    bReturnValue = false;
                    return bReturnValue;
                }

                FileUpload_Doc.SaveAs(strMainFolderPath + strFileName);
            }
            else
            {
                bReturnValue = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdatePanEin");
        }

        return bReturnValue;
    }

    ////// Method to Check File MimeType
    private bool IsFileValid(FileUpload FileUpload1)
    {
        string[] allowedImageTyps = { "application/pdf" };
        string[] allowedExtension = { ".pdf" };
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        imageTypes.AddRange(allowedImageTyps);
        imageExtension.AddRange(allowedExtension);
        string strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
        string fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);// 
        int count = FileUpload1.FileName.Count(f => f == '.');

        string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
        CommonFunctions cmmf = new CommonFunctions();

        if (imageTypes.Contains(strFiletype) && imageExtension.Contains(fileExt) && count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    ////// Update PAN and EIN/IEM in Data Warehouse
    private string UpdatePanDWH(string strOldUserId, string strNewUserId, string strPAN, string strEinNo, string strEinType, string strFileName, int intParentId, string strParentUnqId)
    {
        string strReturnVal = "";
        try
        {
            /*-----------------------------------------------------------------*/
            /////// Service Initialization (Push data to Data Warehouse)
            /*-----------------------------------------------------------------*/
            DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
            DWH_Model objSrvEntity = new DWH_Model();

            objSrvEntity.VCHUSERNAME = strOldUserId;
            objSrvEntity.VCHUSERNAME_NEW = strNewUserId;
            objSrvEntity.VCHPANNO = strPAN;
            objSrvEntity.VCHEINIEM = strEinNo;
            objSrvEntity.VCHLICENCENOTYPE = strEinType;
            objSrvEntity.VCHLICENCEDOC = strFileName;
            objSrvEntity.INTPARENTID = intParentId;
            objSrvEntity.VCHUSERUNIQUEID = strParentUnqId;

            /*-----------------------------------------------------------------*/
            /////// Generate Encryption Key (Security key to access Data Warehouse service methods)
            string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
            string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);

            /*-----------------------------------------------------------------*/
            /////// DML Opertion (To Data Warehouse)
            strReturnVal = objSrvRef.PanEinUpdate(objSrvEntity, strSecurityKey);
        }
        catch (Exception)
        {
            throw;
        }

        return strReturnVal;
    }

    ////// Update PAN and EIN/IEM in GOSWIFT
    private void UpdateEINandPAN()
    {
        LoginDetails objEntity = new LoginDetails();
        LoginBusinessLayer objBAL = new LoginBusinessLayer();

        try
        {
            bool bRetVal = true;
            /*---------------------------------------------------------------*/
            string strFileName = "";
            if (FileUpload_Licence_Doc.HasFile)
            {
                string strFileExt = Path.GetExtension(FileUpload_Licence_Doc.FileName);
                strFileName = string.Format("{0:yyyyMMddhhmmss}", DateTime.Now) + "_LICNDOC" + strFileExt;
                ViewState["fileName"] = strFileName;
                string strFolderName = "RegdDoc";
                bRetVal = UploadDocument(FileUpload_Licence_Doc, strFileName, strFolderName);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please upload document in support of EIN/IEM !</strong>');", true);
                bRetVal = false;
                return;
            }

            ///*---------------------------------------------------------------*/

            if (bRetVal == true)
            {
                objEntity.strAction = "U";
                objEntity.strPAN = Txt_PAN.Text.ToUpper();
                objEntity.strEINIEM = Txt_EIN_IEM.Text.ToUpper();
                objEntity.strLicenseNoType = DrpDwn_License_Type.SelectedValue;
                objEntity.strLicenseDoc = strFileName;
                objEntity.strUserID = Lbl_User_Id.Text;

                /////// DML Operation
                string strResult = objBAL.PAN_AED(objEntity);
                string[] arrResult = strResult.Split('~');

                if (arrResult[0] == "1")
                {
                    string strNewUserId = arrResult[1].ToString();

                    /////// Update PAN and EIN/IEM in Data Warehouse
                    string strReturnVal = UpdatePanDWH(objEntity.strUserID, strNewUserId, objEntity.strPAN, objEntity.strEINIEM, objEntity.strLicenseNoType, strFileName, 0, "");
                    if (strReturnVal == "5")
                    {
                        string message = @"<strong>Data updated successfully.</br>Please note your new user is <span style=" + "color:red;" + ">" + strNewUserId + "</span> !</strong>.</br>Please note in order to create your own user id, log in using this id in GOSWIFT and create new user id in Edit Profile section.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + message + "');</script>", false);
                    }
                    else if (strReturnVal == "11")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid User !</strong>', '" + strProjName + "'); </script>", false);
                    }
                    else if (strReturnVal == "17")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The EIN/IEM number provided by you is already exists in SSO. Please try with a different EIN/IEM number !</strong>', '" + strProjName + "'); </script>", false);
                    }
                }
                else if (arrResult[0] == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Internal Server Error,Please Try After Sometime !</strong>', '" + strProjName + "'); </script>", false);
                }
                else if (arrResult[0] == "3")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The EIN/IEM number provided by you is already exists.Please try with a different EIN/IEM number !</strong>', '" + strProjName + "'); </script>", false);
                }
                else if (arrResult[0] == "4") /////// PAN is already exist for other user and it will prompt to make child user.
                {
                    Hid_Investor_Id.Value = Convert.ToString(arrResult[1]);
                    Lbl_Unit_Name.Text = Convert.ToString(arrResult[2]);
                    Lbl_Unit_Address.Text = Convert.ToString(arrResult[3]);
                    Lbl_PAN.Text = Txt_PAN.Text;

                    ViewState["parentUnqId"] = Convert.ToString(arrResult[4]);

                    ModalPopupExtender1.Show();
                }
                else if (arrResult[0] == "5")
                {
                    /////// Update PAN and EIN/IEM in Data Warehouse
                    string strReturnVal = UpdatePanDWH(objEntity.strUserID, "", objEntity.strPAN, objEntity.strEINIEM, objEntity.strLicenseNoType, strFileName, 0, "");

                    string message = "<strong>EIN/IEM Number Updated Sucessfully !</strong>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + message + "');</script>", false);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdatePanEin");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    ////// Fill Investor Details
    private void GetIndustryDetails(int intInvetsorId)
    {
        InvestorBusinessLayer objBAL = new InvestorBusinessLayer();
        InvestorDetails objEntity = new InvestorDetails();

        DataTable dt = new DataTable();
        try
        {
            objEntity.strAction = "V4";
            objEntity.IntInvestorId = intInvetsorId;

            /////// Select Data
            dt = objBAL.UserManagementView(objEntity);

            if (dt.Rows.Count > 0)
            {
                int intIndustryType = Convert.ToInt32(dt.Rows[0]["INT_CATEGORY"]);
                Hid_Industry_Type.Value = intIndustryType.ToString();

                ArrayList list = new ArrayList();
                list.Add(new ListItem("-Select-", "0"));

                LnkBtn_EIN.Visible = false;
                spnIEMNo.Visible = false;
                spnUAadhaarNo.Visible = false;

                if (intIndustryType == 1) //// Large Industry
                {
                    list.Add(new ListItem("IEM"));
                    list.Add(new ListItem("Udyog Aadhaar"));

                    spnIEMNo.Visible = true;
                    spnUAadhaarNo.Visible = true;
                    Lbl_Doc_Name.Text = "Upload IEM/Udyog Aadhaar Document";
                }
                else if (intIndustryType == 2) //// MSME Industry
                {
                    list.Add(new ListItem("EIN"));
                    list.Add(new ListItem("Production Certificate"));

                    LnkBtn_EIN.Visible = true;
                    Lbl_Doc_Name.Text = "Upload EIN/Production Certificate Document";
                }
                else
                {
                    if (intIndustryType == 1)
                    {
                        list.Add(new ListItem("IEM"));
                        list.Add(new ListItem("Udyog Aadhaar"));

                        spnIEMNo.Visible = true;
                        spnUAadhaarNo.Visible = true;
                        Lbl_Doc_Name.Text = "Upload IEM/Udyog Aadhaar Document";
                    }
                    else if (intIndustryType == 2)
                    {
                        list.Add(new ListItem("EIN"));
                        list.Add(new ListItem("Production Certificate"));

                        LnkBtn_EIN.Visible = true;
                        Lbl_Doc_Name.Text = "Upload EIN/Production Certificate Document";
                    }
                }

                DrpDwn_License_Type.DataSource = list;
                DrpDwn_License_Type.DataTextField = "Text";
                DrpDwn_License_Type.DataValueField = "Value";
                DrpDwn_License_Type.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdatePanEin");
        }
        finally
        {
            dt = null;
            objBAL = null;
            objEntity = null;
        }
    }

    #endregion

    //////ButtonClickEvent
    #region ButtonClickEvents

    ////// Button for Update PAN and EIN/IEM
    protected void Btn_Update_Click(object sender, EventArgs e)
    {
        try
        {
            #region InputValidation

            if (Txt_PAN.Text.Trim() == "")
            {
                Txt_PAN.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter PAN.</strong>');", true);
                return;
            }
            if (Txt_PAN.Text.Trim().Length != 10)
            {
                Txt_PAN.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>PAN should be 10 digits.</strong>');", true);
                return;
            }
            //// Reqular expression validation for valid PAN format
            Regex rePan = new Regex(@"^[A-Z]{5}\d{4}[A-Z]{1}$");
            if (!rePan.IsMatch(Txt_PAN.Text.Trim()))
            {
                Txt_PAN.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid PAN card number.</strong>');", true);
                return;
            }
            if (DrpDwn_License_Type.SelectedIndex == 0)
            {
                DrpDwn_License_Type.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select license document type.</strong>');", true);
                return;
            }
            if (Txt_EIN_IEM.Text.Trim() == "")
            {
                Txt_EIN_IEM.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter license number.</strong>');", true);
                return;
            }
            if (FileUpload_Licence_Doc.HasFile != true)
            {
                FileUpload_Licence_Doc.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please upload license document.</strong>');", true);
                return;
            }

            #endregion

            /*-------------------------------------------------------------------*/

            ////// Get NSDL validation enable status (ON/OFF) from web config.          
            string strValidateNSDL = ConfigurationManager.AppSettings["ValidateNSDLPAN"].ToString();

            if (strValidateNSDL == "OFF") //// PAN will not go for NSDL validation
            {
                UpdateEINandPAN();
            }
            //else if (strValidateNSDL == "ON") //// PAN will go for NSDL validation
            //{
            //    ///// Validate PAN with NSDL and then Update
            //    string strVal = NSDLResponse(Txt_PAN.Text).Trim();
            //    if (strVal == "2")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>System Error !</strong>');", true);
            //    }
            //    else if (strVal == "3")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Authentication Failure !</strong>');", true);
            //        return;
            //    }
            //    else if (strVal == "4")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>User not authorized !</strong>');", true);
            //    }
            //    else if (strVal == "5")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>No PANs Entered !</strong>');", true);
            //    }
            //    else if (strVal == "6")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>User validity has expired !</strong>');", true);
            //    }
            //    else if (strVal == "7")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Number of PANs exceeds the limit (5) !</strong>');", true);
            //    }
            //    else if (strVal == "8")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Not enough balance !</strong>');", true);
            //    }
            //    else if (strVal == "9")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Not an HTTPs request !</strong>');", true);
            //    }
            //    else if (strVal == "10")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>POST method not used !</strong>');", true);
            //    }
            //    else
            //    {
            //        string[] strValueArr = strVal.Split('^');

            //        if (strValueArr[2] == "E")
            //        {
            //            //Code for Update PAN & EIN
            //            UpdateEINandPAN();
            //        }
            //        else if (strValueArr[2] == "F")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Fake PAN No !</strong>');", true);
            //        }
            //        else if (strValueArr[2] == "N")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid PAN !</strong>');", true);
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>No Response !</strong>');", true);
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdatePanEin");
        }
    }

    ////// Button for Update PAN and EIN/IEM (Make the user as child user)
    protected void Btn_Yes_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();

        LoginDetails objEntity = new LoginDetails();
        LoginBusinessLayer objBAL = new LoginBusinessLayer();

        try
        {
            /*---------------------------------------------------------------*/
            string strFileName = Convert.ToString(ViewState["fileName"]);

            ///*---------------------------------------------------------------*/

            ////// Here PAN is not supplied from frontend.It is manged in procedure level
            objEntity.strAction = "Y";
            objEntity.strPAN = Lbl_PAN.Text;
            objEntity.intInvestorId = Convert.ToInt32(Hid_Investor_Id.Value);
            objEntity.strUserID = Lbl_User_Id.Text;
            objEntity.strEINIEM = Txt_EIN_IEM.Text.ToUpper();
            objEntity.strLicenseNoType = DrpDwn_License_Type.SelectedValue;
            objEntity.strLicenseDoc = strFileName;

            /////// DML Operation (To GOSWIFT)
            string strResult = objBAL.PAN_AED(objEntity);
            string[] arrResult = strResult.Split('~');
            if (arrResult[0] == "1")
            {
                ModalPopupExtender1.Hide();
                string strNewUserId = arrResult[1].ToString();

                /////// Update PAN and EIN/IEM in Data Warehouse
                string strReturnVal = UpdatePanDWH(objEntity.strUserID, strNewUserId, objEntity.strPAN, objEntity.strEINIEM, objEntity.strLicenseNoType, strFileName, Convert.ToInt32(Hid_Investor_Id.Value), Convert.ToString(ViewState["parentUnqId"]));
                if (strReturnVal == "5")
                {
                    string message = @"<strong>Data updated successfully.</br>Please note your new user is <span style=" + "color:red;" + ">" + strNewUserId + "</span> !</strong>.</br>Please note in order to create your own user id, log in using this id in GOSWIFT and create new user id in Edit Profile section.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + message + "');</script>", false);
                }
                else if (strReturnVal == "11")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Invalid User !</strong>', '" + strProjName + "'); </script>", false);
                }
                else if (strReturnVal == "17")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The EIN/IEM/Udyog Aadhar/PC number provided by you is already exists in SSO. Please try with a different EIN/IEM/Udyog Aadhar/PC number !</strong>', '" + strProjName + "'); </script>", false);
                }
            }
            else if (strResult == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Internal Server Error,Please Try After Sometime !</strong>', '" + strProjName + "'); </script>", false);
            }
            else if (strResult == "3")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>The EIN/IEM/Udyog Aadhar/PC number provided by you is already exists.Please try with a different EIN/IEM/Udyog Aadhar/PC number !</strong>', '" + strProjName + "'); </script>", false);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdatePanEin");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }

    ////// Button click for Apply EIN/PC
    protected void LnkBtn_EIN_Click(object sender, EventArgs e)
    {
        try
        {
            /*-----------------------------------------------------------------*/
            //// Check whether EIN/PC already applied or exist at AIM portal.            
            /*-----------------------------------------------------------------*/
            AIMServiceReference.UserRegistrationClient objAimRegd = new AIMServiceReference.UserRegistrationClient();
            AIMServiceReference.userregistration_propEntity objAimEnity = new AIMServiceReference.userregistration_propEntity();

            objAimEnity.UNIQUEID = Session["UID"].ToString();

            //// Call AIM Check Method
            int intReturnValAIM = objAimRegd.CheckEINPC(objAimEnity);
            if (intReturnValAIM == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>You have already applied for EIN/PC.Please wait till auto updation.</strong>', '" + strProjName + "'); </script>", false);
                return;
            }
            else if (intReturnValAIM == 2)
            {
                DWHServiceHostClient objSrvRef = new DWHServiceHostClient();

                /*-----------------------------------------------------------------*/
                /////// Generate Encryption Key (Security key to access Data Warehouse service methods)
                string strEncryptionKey = ConfigurationManager.AppSettings["DWHEncryptionKey"];
                string strSecurityKey = objSrvRef.KeyEncryption(strEncryptionKey);
                string strApplicationKey = objSrvRef.GetApplicationKey(9, strSecurityKey);

                string strRedirectUrl = objSrvRef.AppRedirectURL(strApplicationKey, Session["UID"].ToString(), strSecurityKey, Session["LogId"].ToString(), Session["SSOUserId"].ToString());

                ///// Hid_Temp_Used is temporarily used for testing.It need to removed after testing
                Hid_Temp_Used.Value = strRedirectUrl;

                string message = @"<strong>This link will take you to an external web site !</strong>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirectEinPc('" + message + "','" + strRedirectUrl + "');</script>", false);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdatePanEin");
        }
    }

    #endregion

    //////NSDLIntegration
    #region NSDL Integration
    //1^ACYPC7626G^E^CHELLIAH^SINNAPPU^^Shri^22/01/2015^^^

    //public static byte[] Sign(byte[] data, X509Certificate2 certificate)
    //{
    //    if (data == null)
    //        throw new ArgumentNullException("data");
    //    if (certificate == null)
    //        throw new ArgumentNullException("certificate");

    //    // setup the data to sign
    //    ContentInfo content = new ContentInfo(data);
    //    SignedCms signedCms = new SignedCms(content, false);
    //    CmsSigner signer = new CmsSigner(SubjectIdentifierType.IssuerAndSerialNumber, certificate);
    //    // create the signature
    //    signedCms.ComputeSignature(signer);
    //    return signedCms.Encode();
    //}
    //public string NSDLResponse(string strPan)
    //{
    //    string strData = ConfigurationManager.AppSettings["PanUserID"].ToString() + "^" + strPan;
    //    string URL = ConfigurationManager.AppSettings["PanURL"];
    //    string PFXPassword = ConfigurationManager.AppSettings["PanPWD"];
    //    string Certificatename = ConfigurationManager.AppSettings["Certificatename"];
    //    string Response = "";
    //    System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
    //    WriteTextFileLog("Application Started");
    //    try
    //    {
    //        X509Certificate2 m = new X509Certificate2(HttpContext.Current.Server.MapPath("PFX/") + Certificatename, PFXPassword);
    //        byte[] bytes = encoding.GetBytes(strData);
    //        byte[] sig = Sign(bytes, m);
    //        String sigi = Convert.ToBase64String(sig);

    //        //lblsignature.Enabled = true;
    //        //lblsignature.Text = sigi;


    //        //Post data
    //        try
    //        {

    //            StringBuilder postData = new StringBuilder();
    //            postData.Append("data=" + strData);
    //            postData.Append("&signature=" + System.Web.HttpUtility.UrlEncode(sigi));
    //            postData.Append("&version=" + 2);
    //            byte[] data = encoding.GetBytes(postData.ToString());
    //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
    //                                                | SecurityProtocolType.Tls11
    //                                                | SecurityProtocolType.Tls12
    //                                                | SecurityProtocolType.Ssl3;
    //            // HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://14.140.81.154/TIN/PanInquiryBackEnd");
    //            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(URL);
    //            myRequest.Method = "POST";
    //            myRequest.ContentType = "application/x-www-form-urlencoded";
    //            myRequest.ContentLength = data.Length;
    //            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
    //            ServicePointManager.Expect100Continue = true;


    //            Stream newStream = myRequest.GetRequestStream();
    //            // Send the data.
    //            newStream.Write(data, 0, data.Length);
    //            //Console.WriteLine("Send");



    //            //Now, we read the response (the string), and output it.  
    //            HttpWebResponse WebResp = (HttpWebResponse)myRequest.GetResponse();
    //            Stream Answer = WebResp.GetResponseStream();
    //            StreamReader _Answer = new StreamReader(Answer);
    //            //Console.WriteLine("Received");
    //            Response = _Answer.ReadToEnd();
    //            //lblresponse.Text = Response;
    //            newStream.Close();
    //        }
    //        catch (Exception ex)
    //        {
    //            Response = "NA^NA^NA";
    //            WriteTextFileLog(ex.Message);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response = "NA^NA^NA";
    //        WriteTextFileLog(ex.Message);
    //    }
    //    WriteTextFileLog("Application Ended");
    //    return Response;
    //}
    //public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    //{
    //    return true;
    //}
    public static void WriteTextFileLog(string errorMessage, string type = "")
    {
        try
        {
            string path = HttpContext.Current.Server.MapPath("/Logs/") + DateTime.Today.ToString("dd-MM-yy") + ".txt";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            using (StreamWriter w = File.AppendText(path))
            {
                string err = "Message : " + errorMessage;
                if (string.IsNullOrEmpty(type))
                {
                    w.WriteLine("Log Entry : " + DateTime.Now.ToString(CultureInfo.InvariantCulture) + " | " + err);
                    //w.WriteLine(err)
                    if (errorMessage.Contains("Requested item not found."))
                    {
                        //HttpContext.Current.Session["Error"] = "Access Denied"
                    }
                }
                else if (type == "E")
                {
                    w.WriteLine("______________________________________________________________________________");
                }
                w.Flush();
                w.Close();
            }
        }
        catch (System.Exception ex)
        {
            //WriteLog(ex.Message)
        }
    }

    #endregion
}